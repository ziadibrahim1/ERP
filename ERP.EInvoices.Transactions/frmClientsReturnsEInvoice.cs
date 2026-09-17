using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Sales;
using BusinessLayer.StockControl;
using EInvoice;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.EInvoices.Transactions;

public class frmClientsReturnsEInvoice : frmHeaderManyDetails
{
	private DataTable dtUsers;

	private DataTable dtEINVStates;

	private DataTable dtStores;

	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtUnits;

	private DataTable dtBatchs;

	private DataTable dtClients;

	private DataTable dtCurrency;

	private DataTable dtSalesInvoices;

	private DataTable dtMaterialIssueVoucher;

	private DataTable dtDepartments;

	private DataTable dtMinAllowedTransDate;

	private DataTable dtTaxs;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlInvoiceDetailsTaxs = new ValueList();

	private ValueList vlMaterialIssueVoucher = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UseEInvoiceProductionEnvirnoment;

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboSalesInvoiceNo;

	private UltraLabel lblSalesInvoiceNo;

	private UltraLabel lblClient;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsMaterialIssueVoucher;

	private RadioButton rbIsSalesnvoice;

	private UltraLabel lblMaterialIssueVoucherNo;

	private UltraComboEditor cboMaterialIssueVoucherNo;

	protected internal UltraCheckEditor chkAll;

	protected internal CheckedListBox clbMaterialIssueVoucherNo;

	private UltraLabel lblTotal;

	private UltraTextEditor txtTotal;

	public UltraButton btnVouchersSearch;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	public UltraButton btnJV;

	private UltraLabel lblSubtractedValue;

	private UltraTextEditor txtSubtractedValue;

	private UltraLabel lblAddedValue;

	private UltraTextEditor txtAddedValue;

	private UltraLabel lblNet;

	private UltraTextEditor txtNet;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraButton btnPrintEInvoice;

	public UltraButton btnCancelInvoice;

	public UltraButton btnSendInvoice;

	private UltraLabel lblCanceledDate;

	private UltraLabel ultraLabel1;

	private UltraDateTimeEditor dtpCanceledDate;

	private UltraDateTimeEditor dtpSendDate;

	private UltraLabel lblUUID;

	private UltraLabel lblInternalCode;

	private UltraTextEditor txtUUID;

	private UltraTextEditor txtInternalCode;

	private UltraLabel lblEINVState;

	private UltraComboEditor cboState;

	private UltraLabel lblCanceledUser;

	private UltraComboEditor cboCanceledUserName;

	private UltraLabel lblSendUser;

	private UltraComboEditor cboSendUserName;

	public frmClientsReturnsEInvoice()
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
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SC_ClientsDepartmentsReturns";
		IDCol = "ReturnID";
		NoCol = "ReturnNo";
		DateCol = "ReturnDate";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public frmClientsReturnsEInvoice(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UseEInvoiceProductionEnvirnoment = GlobalFunctions.GetOption("ElectronicInvoiceProductionEnvirnoment");
		dtEINVStates = States.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboState, dtEINVStates, "EINVStateID", "EINVStateName");
		dtUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSendUserName, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		GlobalFunctions.FillCombo(cboCanceledUserName, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtCurrency = BusinessLayer.Accounting.Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
		}
		dtUnits = BusinessLayer.StockControl.Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxs = BusinessLayer.General.Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		dtSalesInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "-1");
		GlobalFunctions.FillCombo(cboSalesInvoiceNo, dtSalesInvoices, "SLInvoiceID", "SLInvoiceNo");
		dtMaterialIssueVoucher = MaterialIssueVouchers.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "0", "-1");
		GlobalFunctions.FillCombo(cboMaterialIssueVoucherNo, dtMaterialIssueVoucher, "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
		vlMaterialIssueVoucher.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtMaterialIssueVoucher.Rows.Count; num2++)
		{
			vlMaterialIssueVoucher.ValueListItems.Add(dtMaterialIssueVoucher.Rows[num2]["MaterialIssueVoucherID"], dtMaterialIssueVoucher.Rows[num2]["MaterialIssueVoucherNo"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtDetails = ClientsDepartmentsReturnsDetails.SelectByReturnID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Header).Caption = (GlobalVariables.IsArabic ? "الخصم" : "Discount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "ضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Amount");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherID"].Header).Caption = (GlobalVariables.IsArabic ? "صرف رقم" : "Material Issue No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الاقصى" : "Max limit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDiscount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlInvoiceDetailsTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherID"].ValueList = (IValueList)(object)vlMaterialIssueVoucher;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxAllowedQty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ClientsDepartmentsReturns.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboMaterialIssueVoucherNo).ValueChanged -= cboMaterialIssueVoucherNo_ValueChanged;
			((TextEditorControlBase)cboSalesInvoiceNo).ValueChanged -= cboSalesInvoiceNo_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ReturnNo"].ToString();
			rbIsSalesnvoice.Checked = bool.Parse(drMaster["IsSLInvoice"].ToString());
			rbIsMaterialIssueVoucher.Checked = !bool.Parse(drMaster["IsSLInvoice"].ToString());
			dtpDate.Value = (DateTime)drMaster["ReturnDate"];
			((TextEditorControlBase)cboSalesInvoiceNo).Value = drMaster["SLInvoiceID"];
			((TextEditorControlBase)cboMaterialIssueVoucherNo).Value = drMaster["MaterialIssueVoucherID"];
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			UltraLabel obj = lblTotal;
			UltraTextEditor obj2 = txtTotal;
			UltraLabel obj3 = lblAddedValue;
			UltraTextEditor obj4 = txtAddedValue;
			UltraLabel obj5 = lblSubtractedValue;
			UltraTextEditor obj6 = txtSubtractedValue;
			UltraLabel obj7 = lblNet;
			bool flag = (((Control)(object)txtNet).Visible = true);
			bool flag3 = (((Control)(object)obj7).Visible = flag);
			bool flag5 = (((Control)(object)obj6).Visible = flag3);
			bool flag7 = (((Control)(object)obj5).Visible = flag5);
			bool flag9 = (((Control)(object)obj4).Visible = flag7);
			bool flag11 = (((Control)(object)obj3).Visible = flag9);
			bool visible = (((Control)(object)obj2).Visible = flag11);
			((Control)(object)obj).Visible = visible;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtInternalCode).Text = drMaster["EInvoiceInternalCode"].ToString();
			((Control)(object)txtUUID).Text = drMaster["EInvoiceUUID"].ToString();
			dtpSendDate.Value = drMaster["EInvoiceSenDate"];
			((TextEditorControlBase)cboSendUserName).Value = drMaster["EInvoiceSendUserID"];
			dtpCanceledDate.Value = drMaster["EInvoiceCanceledDate"];
			((TextEditorControlBase)cboCanceledUserName).Value = drMaster["EInvoiceCanceledUserID"];
			((TextEditorControlBase)cboState).Value = drMaster["EINVStateID"];
			((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
			((Control)(object)txtAddedValue).Text = decimal.Parse(drMaster["AddedValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
			((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
			((Control)(object)txtSubtractedValue).Text = decimal.Parse(drMaster["SubtractedValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
			((TextEditorControlBase)txtNet).ValueChanged -= txtNet_ValueChanged;
			((Control)(object)txtNet).Text = decimal.Parse((decimal.Parse(drMaster["TotalPrice"].ToString()) + decimal.Parse((drMaster["AddedValue"] == DBNull.Value) ? "0" : drMaster["AddedValue"].ToString()) - decimal.Parse((drMaster["SubtractedValue"] == DBNull.Value) ? "0" : drMaster["SubtractedValue"].ToString())).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtNet).ValueChanged += txtNet_ValueChanged;
			((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? ("(" + drMaster["JVNo"].ToString() + ")قيد") : ("No( " + drMaster["JVNo"].ToString() + " )"));
			((Control)(object)btnJV).Visible = ((drMaster["JVNo"] != DBNull.Value) ? true : false) && CanViewJV;
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ClientsDepartmentsReturnsDetails.SelectByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)cboMaterialIssueVoucherNo).ValueChanged += cboMaterialIssueVoucherNo_ValueChanged;
			((TextEditorControlBase)cboSalesInvoiceNo).ValueChanged += cboSalesInvoiceNo_ValueChanged;
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesInvoiceNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMaterialIssueVoucherNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		rbIsMaterialIssueVoucher.Enabled = Adding;
		rbIsSalesnvoice.Enabled = Adding;
		((Control)(object)chkAll).Enabled = !NavMode;
		clbMaterialIssueVoucherNo.Enabled = !NavMode;
		((Control)(object)btnVouchersSearch).Visible = !NavMode;
		((Control)(object)btnJV).Visible = NavMode && drMaster != null && CanViewJV;
		((Control)(object)btnCopyTo).Visible = false;
		((EditorButtonControlBase)txtTotal).ReadOnly = true;
		((EditorButtonControlBase)txtAddedValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSubtractedValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNet).ReadOnly = NavMode;
		int num = 0;
		int num2 = 0;
		if (cboSalesInvoiceNo.SelectedIndex > -1)
		{
			num = int.Parse(((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString());
		}
		if (cboMaterialIssueVoucherNo.SelectedIndex > -1)
		{
			num2 = int.Parse(((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString());
		}
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtSalesInvoices);
			dataView.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboSalesInvoiceNo, dataView.ToTable(), "SLInvoiceID", "SLInvoiceNo");
			DataView dataView2 = new DataView(dtMaterialIssueVoucher);
			dataView2.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboMaterialIssueVoucherNo, dataView2.ToTable(), "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
			DataView dataView3 = new DataView(dtStores);
			dataView3.RowFilter = " Locked =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView3.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboSalesInvoiceNo, dtSalesInvoices, "SLInvoiceID", "SLInvoiceNo");
			GlobalFunctions.FillCombo(cboMaterialIssueVoucherNo, dtMaterialIssueVoucher, "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		((TextEditorControlBase)cboSalesInvoiceNo).ValueChanged -= cboSalesInvoiceNo_ValueChanged;
		((TextEditorControlBase)cboMaterialIssueVoucherNo).ValueChanged -= cboMaterialIssueVoucherNo_ValueChanged;
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesInvoiceNo).Value = num;
		}
		if (num2 != 0)
		{
			((TextEditorControlBase)cboMaterialIssueVoucherNo).Value = num2;
		}
		((TextEditorControlBase)cboSalesInvoiceNo).ValueChanged += cboSalesInvoiceNo_ValueChanged;
		((TextEditorControlBase)cboMaterialIssueVoucherNo).ValueChanged += cboMaterialIssueVoucherNo_ValueChanged;
		((EditorButtonControlBase)txtInternalCode).ReadOnly = true;
		((EditorButtonControlBase)txtUUID).ReadOnly = true;
		((EditorButtonControlBase)dtpSendDate).ReadOnly = true;
		((EditorButtonControlBase)cboSendUserName).ReadOnly = true;
		((Control)(object)btnSendInvoice).Visible = NavMode;
		((Control)(object)btnPrintEInvoice).Visible = NavMode;
		((EditorButtonControlBase)dtpCanceledDate).ReadOnly = true;
		((EditorButtonControlBase)cboCanceledUserName).ReadOnly = true;
		((EditorButtonControlBase)cboState).ReadOnly = true;
		((Control)(object)btnCancelInvoice).Visible = NavMode;
		if (Adding)
		{
			DataView dataView4 = new DataView(dtItems);
			dataView4.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView4.ToTable();
			vlItems.ValueListItems.Clear();
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[k]["ItemID"], dataTable2.Rows[k]["Name"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)(Adding ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? ClientsDepartmentsReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((Control)(object)txtTotal).Text = "0";
		((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
		((Control)(object)txtAddedValue).Text = "0";
		((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
		((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
		((Control)(object)txtSubtractedValue).Text = "0";
		((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
		((TextEditorControlBase)txtNet).ValueChanged -= txtNet_ValueChanged;
		((Control)(object)txtNet).Text = "0";
		((TextEditorControlBase)txtNet).ValueChanged += txtNet_ValueChanged;
		cboClient.SelectedIndex = -1;
		cboSalesInvoiceNo.SelectedIndex = -1;
		cboMaterialIssueVoucherNo.SelectedIndex = -1;
		((Control)(object)txtInternalCode).Text = (Adding ? ClientsDepartmentsReturns.GetEInvoiceInternalCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtUUID).Clear();
		dtpSendDate.Value = null;
		cboSendUserName.SelectedIndex = -1;
		dtpCanceledDate.Value = null;
		cboCanceledUserName.SelectedIndex = -1;
		cboState.SelectedIndex = -1;
		((UltraToggleEditorBase)chkAll).Checked = false;
		rbIsSalesnvoice.Checked = true;
		UltraCheckEditor obj = chkAll;
		bool visible = (clbMaterialIssueVoucherNo.Visible = rbIsSalesnvoice.Checked && Adding);
		((Control)(object)obj).Visible = visible;
		clbMaterialIssueVoucherNo.SelectedValueChanged -= clbMaterialIssueVoucherNo_SelectedValueChanged;
		clbMaterialIssueVoucherNo.DataSource = null;
		clbMaterialIssueVoucherNo.SelectedValueChanged += clbMaterialIssueVoucherNo_SelectedValueChanged;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtExchangeRate).Text = "0";
		((Control)(object)btnJV).Text = (GlobalVariables.IsArabic ? "قيد" : "No");
	}

	public override bool ValidateData()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select(" Qty =0 ").Length != 0)
		{
			GlobalVariables.QuestionMB.Show("يوجد أصناف كميتها بصفر هل تريد الحذف؟ ", "There Are Items Quantity Equal Zero Are you Sure To Delete?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				for (int num = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1; num >= 0; num--)
				{
					if (((UltraGridBase)ULGData).Rows[num].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[num].Cells["Qty"].Value.ToString()) == 0m)
					{
						((UltraGridBase)ULGData).Rows[num].Delete(false);
					}
				}
				((UltraGridBase)ULGData).UpdateData();
				ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
			}
		}
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtSubtractedValue).Text != "" && decimal.Parse(((Control)(object)txtSubtractedValue).Text) > decimal.Parse(((Control)(object)txtTotal).Text))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لابد ان تكون القيمة المخصومة أقل من تساوى قيمة الفاتورة" : "Subtracted Value Must Less Than or Equal Total Price");
			return false;
		}
		if (cboSalesInvoiceNo.SelectedIndex == -1 && rbIsSalesnvoice.Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم فاتورة المبيعات" : "Please Select Sales Invoice No");
			cboSalesInvoiceNo.DropDown();
			return false;
		}
		if (cboMaterialIssueVoucherNo.SelectedIndex == -1 && rbIsMaterialIssueVoucher.Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار رقم إذن الصرف" : "Please Select Material Issue Voucher No");
			cboMaterialIssueVoucherNo.DropDown();
			return false;
		}
		if (rbIsSalesnvoice.Checked)
		{
			if (dtpDate.DateTime < DateTime.Parse(dtSalesInvoices.Select("SLInvoiceID = " + ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString())[0]["SLInvoiceDate"].ToString()))
			{
				GlobalVariables.InformationMB.Show("تاريخ الاذن قبل تاريخ فاتورة المبيعات ", "Voucher Date before Sales Invoice Date.");
				return false;
			}
		}
		else if (dtpDate.DateTime < DateTime.Parse(dtMaterialIssueVoucher.Select("MaterialIssueVoucherID = " + ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString())[0]["MaterialIssueVoucherDate"].ToString()))
		{
			GlobalVariables.InformationMB.Show("تاريخ الاذن قبل تاريخ اذن الصرف ", "Voucher Date Before Material Issue Voucher Date.");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SC_ClientsDepartmentsReturns", "ReturnNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ReturnNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ClientsDepartmentsReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SC_ClientsDepartmentsReturns", "EInvoiceInternalCode", ((Control)(object)txtInternalCode).Text, Adding ? "0" : drMaster["EInvoiceInternalCode"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string eInvoiceInternalCode = ClientsDepartmentsReturns.GetEInvoiceInternalCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + eInvoiceInternalCode, "The Voucher Number Already Exists It Will Be Saved With No. : " + eInvoiceInternalCode);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtInternalCode).Focus();
				return false;
			}
			((Control)(object)txtInternalCode).Text = eInvoiceInternalCode;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				return false;
			}
			if (UsingColors && (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				return false;
			}
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع قبل أخر إعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n  على مخزن   " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n  The Date you choosed Before Last Store Taking With Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return false;
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesReturnsAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب مردودات المبيعات من حسابات النظام  ", "Please Select Sales Returns Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ClientsDepartmentsReturns.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsSalesnvoice.Checked ? "1" : "0", rbIsSalesnvoice.Checked ? ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString() : "Null", rbIsSalesnvoice.Checked ? "Null" : ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), "Null", (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, (((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text, (((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text, (((Control)(object)txtInternalCode).Text == "") ? "Null" : ((Control)(object)txtInternalCode).Text, "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ClientsDepartmentsReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ClientsDepartmentsReturns.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
			ItemsTransactions.ManageInThread();
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
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ClientsDepartmentsReturns.Insert_Update(drMaster["ReturnID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsSalesnvoice.Checked ? "1" : "0", rbIsSalesnvoice.Checked ? ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString() : "Null", rbIsSalesnvoice.Checked ? "Null" : ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (drMaster["DepartmentID"] == DBNull.Value) ? "Null" : drMaster["DepartmentID"].ToString(), (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, (((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text, (((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text, (((Control)(object)txtInternalCode).Text == "") ? "Null" : ((Control)(object)txtInternalCode).Text, (((Control)(object)txtUUID).Text == "") ? "Null" : ((Control)(object)txtUUID).Text, (dtpSendDate.Value == null) ? "Null" : dtpSendDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboSendUserName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSendUserName).Value.ToString(), (cboCanceledUserName.SelectedIndex == -1) ? "0" : "1", (dtpCanceledDate.Value == null) ? "Null" : dtpCanceledDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboCanceledUserName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCanceledUserName).Value.ToString(), (drMaster["EINVStateID"] == DBNull.Value) ? "Null" : drMaster["EINVStateID"].ToString(), (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RMIV", "RMIV");
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ReturnID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ReturnDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("SC_ClientsDepartmentsReturnsDetails", "ReturnID", drMaster["ReturnID"].ToString(), "ReturnDetailID", text);
			ClientsDepartmentsReturnsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			string text2 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "RMIV", "RMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text2 != "")
			{
				GlobalVariables.InformationMB.Show(text2);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "RMIV", "RMIV");
			}
			else
			{
				ClientsDepartmentsReturns.GenerateJvs("," + num + ",", GlobalVariables.UserID);
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
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
			MessageLog.DeleteByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RMIV", "RMIV");
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			ClientsDepartmentsReturns.DeleteVirtual(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			ClientsDepartmentsReturnsDetails.DeleteVirtualByReturnID(drMaster["ReturnID"].ToString(), GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RMIV", "RMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(drMaster["ReturnID"].ToString(), "RMIV", "RMIV");
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SC_ClientsDepartmentsReturns_A.rpt" : "Rep_SC_ClientsDepartmentsReturns_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ClientsDepartmentsReturnIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsDepartmentsReturnsReport(-1, 0, 1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ReturnID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		object value = ((TextEditorControlBase)cboState).Value;
		dtEINVStates = States.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboState, dtEINVStates, "EINVStateID", "EINVStateName");
		((TextEditorControlBase)cboState).Value = value;
		dtUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSendUserName, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		GlobalFunctions.FillCombo(cboCanceledUserName, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtCurrency = BusinessLayer.Accounting.Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSalesInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "-1");
		dtMaterialIssueVoucher = MaterialIssueVouchers.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "0", "-1");
		vlMaterialIssueVoucher.ValueListItems.Clear();
		for (int l = 0; l < dtMaterialIssueVoucher.Rows.Count; l++)
		{
			vlMaterialIssueVoucher.ValueListItems.Add(dtMaterialIssueVoucher.Rows[l]["MaterialIssueVoucherID"], dtMaterialIssueVoucher.Rows[l]["MaterialIssueVoucherNo"].ToString());
		}
		DataView dataView = new DataView(dtSalesInvoices);
		dataView.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
		GlobalFunctions.FillCombo(cboSalesInvoiceNo, dataView.ToTable(), "SLInvoiceID", "SLInvoiceNo");
		DataView dataView2 = new DataView(dtMaterialIssueVoucher);
		dataView2.RowFilter = " BranchID= " + GlobalVariables.CurrentBranchID;
		GlobalFunctions.FillCombo(cboMaterialIssueVoucherNo, dataView2.ToTable(), "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
		DataView dataView3 = new DataView(dtStores);
		dataView3.RowFilter = " Locked =0 And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView3.ToTable();
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dataTable.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[m]["StoreID"], dataTable.Rows[m]["StoreName"].ToString());
		}
		if (Adding)
		{
			DataView dataView4 = new DataView(dtItems);
			dataView4.RowFilter = " IsActive =1 ";
			DataTable dataTable2 = dataView4.ToTable();
			vlItems.ValueListItems.Clear();
			for (int n = 0; n < dataTable2.Rows.Count; n++)
			{
				vlItems.ValueListItems.Add(dataTable2.Rows[n]["ItemID"], dataTable2.Rows[n]["Name"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			for (int num = 0; num < dtItems.Rows.Count; num++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[num]["ItemID"], dtItems.Rows[num]["Name"].ToString());
			}
		}
		dtUnits = BusinessLayer.StockControl.Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtUnits.Rows.Count; num2++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num2]["UnitID"], dtUnits.Rows[num2]["UnitName"].ToString());
		}
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
	}

	public override void btnUpdateClick()
	{
		if (drMaster == null)
		{
			return;
		}
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
		if (((TextEditorControlBase)cboState).Value.ToString() == "2" || (((TextEditorControlBase)cboState).Value.ToString() == "1" && ((Control)(object)txtUUID).Text != ""))
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تم رفعها على موقع الضرائب", "Cannot Update This Transaction Because It Already Uploaded To Tax Portal ");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(dtStores.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString())[0]["Locked"].ToString()))
			{
				GlobalVariables.InformationMB.Show(" لايمكن تعديل هذه الحركة لوجود المخزن \n" + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " مغلق ", "Cannot Update This Transaction Because Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " Locked ");
				return;
			}
			DataTable minAllowedTransDateByStoreIDs = Stores.GetMinAllowedTransDateByStoreIDs("," + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + ",", dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false);
			if (minAllowedTransDateByStoreIDs.Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show(" لا يمكن تعديل هذه الحركة لوجود جرد او اعادة تقييم بتاريخ \n " + minAllowedTransDateByStoreIDs.Rows[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString(), "\n Cannot Update This Transaction Because Store Taking Or Store Revaluation on Date" + minAllowedTransDateByStoreIDs.Rows[0]["Date"].ToString() + " \n  On Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString());
				return;
			}
		}
		Updating = true;
		SetControls(NavMode: false);
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
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على مخزن " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text, string.Concat("\n Cannot Delete This Transaction Because Store Taking Date", dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString() + " And Date > '" + dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'"), "\n   On Store ", ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text));
				return;
			}
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Qty" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Notes")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: true);
				frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
				frmQuantityMultiUnit2.ShowDialog();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
			}
			e.Handled = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show(" الحد الاقصى للكمية المرتجعة " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString(), " Max Allowed Returned Quantity " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value.ToString());
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaxAllowedQty"].Value;
		}
		if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			CalculateTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) * (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ReturnPrice"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalDiscount"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CommercialTaxValue"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["StampsValue"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["GrowthFees"].Value.ToString()));
		}
		((Control)(object)txtTotal).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNet();
	}

	public void CalculateNet()
	{
		((TextEditorControlBase)txtNet).ValueChanged -= txtNet_ValueChanged;
		((Control)(object)txtNet).Text = decimal.Parse((decimal.Parse((((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text) + decimal.Parse((((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text) - decimal.Parse((((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtNet).ValueChanged += txtNet_ValueChanged;
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		UltraLabel obj = lblMaterialIssueVoucherNo;
		bool visible = (((Control)(object)cboMaterialIssueVoucherNo).Visible = rbIsMaterialIssueVoucher.Checked);
		((Control)(object)obj).Visible = visible;
		UltraLabel obj2 = lblSalesInvoiceNo;
		visible = (((Control)(object)cboSalesInvoiceNo).Visible = rbIsSalesnvoice.Checked);
		((Control)(object)obj2).Visible = visible;
		UltraCheckEditor obj3 = chkAll;
		visible = (clbMaterialIssueVoucherNo.Visible = rbIsSalesnvoice.Checked && Adding);
		((Control)(object)obj3).Visible = visible;
		UltraLabel obj4 = lblTotal;
		UltraTextEditor obj5 = txtTotal;
		UltraLabel obj6 = lblAddedValue;
		UltraTextEditor obj7 = txtAddedValue;
		UltraLabel obj8 = lblSubtractedValue;
		UltraTextEditor obj9 = txtSubtractedValue;
		UltraLabel obj10 = lblNet;
		bool flag4 = (((Control)(object)txtNet).Visible = true);
		bool flag6 = (((Control)(object)obj10).Visible = flag4);
		bool flag8 = (((Control)(object)obj9).Visible = flag6);
		bool flag10 = (((Control)(object)obj8).Visible = flag8);
		bool flag12 = (((Control)(object)obj7).Visible = flag10);
		bool flag14 = (((Control)(object)obj6).Visible = flag12);
		visible = (((Control)(object)obj5).Visible = flag14);
		((Control)(object)obj4).Visible = visible;
		if (Adding)
		{
			((TextEditorControlBase)cboSalesInvoiceNo).ValueChanged -= cboSalesInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherNo).ValueChanged -= cboMaterialIssueVoucherNo_ValueChanged;
			clbMaterialIssueVoucherNo.SelectedValueChanged -= clbMaterialIssueVoucherNo_SelectedValueChanged;
			cboMaterialIssueVoucherNo.SelectedIndex = -1;
			cboSalesInvoiceNo.SelectedIndex = -1;
			((UltraToggleEditorBase)chkAll).Checked = false;
			clbMaterialIssueVoucherNo.DataSource = null;
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			((Control)(object)txtTotal).Text = "0";
			((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
			((Control)(object)txtAddedValue).Text = "0";
			((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
			((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
			((Control)(object)txtSubtractedValue).Text = "0";
			((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
			((TextEditorControlBase)txtNet).ValueChanged -= txtNet_ValueChanged;
			((Control)(object)txtNet).Text = "0";
			((TextEditorControlBase)txtNet).ValueChanged += txtNet_ValueChanged;
			clbMaterialIssueVoucherNo.SelectedValueChanged += clbMaterialIssueVoucherNo_SelectedValueChanged;
			((TextEditorControlBase)cboSalesInvoiceNo).ValueChanged += cboSalesInvoiceNo_ValueChanged;
			((TextEditorControlBase)cboMaterialIssueVoucherNo).ValueChanged += cboMaterialIssueVoucherNo_ValueChanged;
		}
	}

	private async void btnPrintEInvoice_Click(object sender, EventArgs e)
	{
		((Control)(object)btnPrintEInvoice).Enabled = false;
		if (cboSendUserName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إرسال المرتجع أولا ", "Please Send Return First ");
			((Control)(object)btnPrintEInvoice).Enabled = true;
			return;
		}
		await EINV.DocumentPrint(lstInvoices: new List<List<string>>
		{
			new List<string>
			{
				((Control)(object)txtUUID).Text,
				((Control)(object)txtInternalCode).Text.Replace("/", "-") + "-" + ((Control)(object)txtCode).Text
			}
		}, EINVClientID: GlobalFunctions.GetDefault("EINVClientID"), EINVSecretID: GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment: UseEInvoiceProductionEnvirnoment);
		((Control)(object)btnPrintEInvoice).Enabled = true;
	}

	private async void btnSendInvoice_Click(object sender, EventArgs e)
	{
		((Control)(object)btnSendInvoice).Enabled = false;
		EINV.Set_Connection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword);
		string Message = EINV.ValidationByRSLInvoiceIDs("," + RowID + ",", GlobalVariables.IsArabic);
		if (Message != "")
		{
			GlobalVariables.InformationMB.Show(Message);
			((Control)(object)btnSendInvoice).Enabled = true;
			return;
		}
		if (cboState.SelectedIndex == -1 || ((TextEditorControlBase)cboState).Value.ToString() != "2")
		{
			int State = await EINV.GetDocumentStateSLReturns(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, ((Control)(object)txtUUID).Text, RowID);
			if (State > -1)
			{
				((TextEditorControlBase)cboState).Value = State;
			}
		}
		if (cboState.SelectedIndex == -1 || (((TextEditorControlBase)cboState).Value.ToString() != "2" && ((TextEditorControlBase)cboState).Value.ToString() != "4" && ((TextEditorControlBase)cboState).Value.ToString() != "5"))
		{
			int State2 = await EINV.DocumentSubmissionSLReturns(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, "," + RowID + ",", GlobalVariables.UserID, GlobalVariables.IsArabic);
			if (State2 > -1)
			{
				((TextEditorControlBase)cboState).Value = State2;
			}
		}
		((Control)(object)btnSendInvoice).Enabled = true;
		FillData();
	}

	private async void btnCancelInvoice_Click(object sender, EventArgs e)
	{
		((Control)(object)btnCancelInvoice).Enabled = false;
		if (cboCanceledUserName.SelectedIndex > -1)
		{
			GlobalVariables.InformationMB.Show("الفاتورة تم الغائها ", "Invoice Already Canceled ");
			((Control)(object)btnCancelInvoice).Enabled = true;
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد إلغاء الفاتورة الألكترونية", "Are You Sure You Want To Cnacel E-Invoice");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			EINV.Set_Connection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword);
			List<List<string>> lstInvoices = new List<List<string>>
			{
				new List<string>
				{
					((Control)(object)txtUUID).Text,
					((Control)(object)txtCode).Text
				}
			};
			if (await EINV.DocumentCancellationSLReturns(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, lstInvoices, GlobalVariables.UserID) > -1)
			{
				Main.ExecuteQuery_DataTable(" Update SC_ClientsDepartmentsReturns Set EINVStateID=5,EInvoiceIsCanceled=1,EInvoiceCanceledDate='" + DateTime.Now.ToString("MM'/'dd'/'yyyy HH: mm:ss") + " ',EInvoiceCanceledUserID=" + GlobalVariables.UserID + "  Where ReturnID = " + RowID);
				if (drMaster["SalesJVID"] != DBNull.Value)
				{
					JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				}
				if (drMaster["StockControlJVID"] != DBNull.Value)
				{
					JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
				}
				dtpCanceledDate.DateTime = DateTime.Now;
				((TextEditorControlBase)cboCanceledUserName).Value = GlobalVariables.UserID;
			}
		}
		((Control)(object)btnCancelInvoice).Enabled = true;
	}

	private void cboMaterialIssueVoucherNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboMaterialIssueVoucherNo.SelectedIndex > -1 && rbIsMaterialIssueVoucher.Checked)
		{
			((TextEditorControlBase)cboClient).Value = dtMaterialIssueVoucher.Select("MaterialIssueVoucherID = " + ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString())[0]["SubAccountID"];
			UltraLabel obj = lblTotal;
			UltraTextEditor obj2 = txtTotal;
			UltraLabel obj3 = lblAddedValue;
			UltraTextEditor obj4 = txtAddedValue;
			UltraLabel obj5 = lblSubtractedValue;
			UltraTextEditor obj6 = txtSubtractedValue;
			UltraLabel obj7 = lblNet;
			bool flag = (((Control)(object)txtNet).Visible = true);
			bool flag3 = (((Control)(object)obj7).Visible = flag);
			bool flag5 = (((Control)(object)obj6).Visible = flag3);
			bool flag7 = (((Control)(object)obj5).Visible = flag5);
			bool flag9 = (((Control)(object)obj4).Visible = flag7);
			bool flag11 = (((Control)(object)obj3).Visible = flag9);
			bool visible = (((Control)(object)obj2).Visible = flag11);
			((Control)(object)obj).Visible = visible;
			((UltraGridBase)ULGData).DataSource = ClientsDepartmentsReturnsDetails.SelectByMaterialIssueVoucherIDs("," + ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString() + ",", GlobalVariables.IsArabic ? "1" : "0");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((TextEditorControlBase)cboCurrency).Value = ((UltraGridBase)ULGData).Rows[0].Cells["CurrencyID"].Value;
				((Control)(object)txtExchangeRate).Text = ((UltraGridBase)ULGData).Rows[0].Cells["ExchangeRate"].Value.ToString();
			}
			InitGrid();
		}
	}

	private void cboSalesInvoiceNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboSalesInvoiceNo.SelectedIndex > -1 && rbIsSalesnvoice.Checked)
		{
			clbMaterialIssueVoucherNo.SelectedValueChanged -= clbMaterialIssueVoucherNo_SelectedValueChanged;
			clbMaterialIssueVoucherNo.DataSource = null;
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
			((TextEditorControlBase)cboClient).Value = dtSalesInvoices.Select("SLInvoiceID = " + ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString())[0]["SubAccountID"];
			DataView dataView = new DataView(dtMaterialIssueVoucher);
			dataView.RowFilter = " SLInvoiceID =" + ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString();
			Main.Fillclb(clbMaterialIssueVoucherNo, dataView.ToTable(), "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
			clbMaterialIssueVoucherNo.SelectedValueChanged += clbMaterialIssueVoucherNo_SelectedValueChanged;
		}
	}

	private void clbMaterialIssueVoucherNo_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAll).Checked = clbMaterialIssueVoucherNo.CheckedItems.Count == clbMaterialIssueVoucherNo.Items.Count && clbMaterialIssueVoucherNo.Items.Count > 0;
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		string materialIssueVoucherIDs = GetMaterialIssueVoucherIDs();
		if (materialIssueVoucherIDs != "")
		{
			((UltraGridBase)ULGData).DataSource = ClientsDepartmentsReturnsDetails.SelectByMaterialIssueVoucherIDs("," + materialIssueVoucherIDs + ",", GlobalVariables.IsArabic ? "1" : "0");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((TextEditorControlBase)cboCurrency).Value = ((UltraGridBase)ULGData).Rows[0].Cells["CurrencyID"].Value;
				((Control)(object)txtExchangeRate).Text = ((UltraGridBase)ULGData).Rows[0].Cells["ExchangeRate"].Value.ToString();
			}
			InitGrid();
		}
	}

	public string GetMaterialIssueVoucherIDs()
	{
		string text = "";
		foreach (DataRowView checkedItem in clbMaterialIssueVoucherNo.CheckedItems)
		{
			text = ((!(text == "")) ? (text + "," + checkedItem[clbMaterialIssueVoucherNo.ValueMember].ToString()) : (text + checkedItem[clbMaterialIssueVoucherNo.ValueMember].ToString()));
		}
		return text;
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		clbMaterialIssueVoucherNo.SelectedValueChanged -= clbMaterialIssueVoucherNo_SelectedValueChanged;
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		for (int i = 0; i < clbMaterialIssueVoucherNo.Items.Count; i++)
		{
			clbMaterialIssueVoucherNo.SetItemChecked(i, ((UltraToggleEditorBase)chkAll).Checked);
		}
		string materialIssueVoucherIDs = GetMaterialIssueVoucherIDs();
		if (materialIssueVoucherIDs != "")
		{
			((UltraGridBase)ULGData).DataSource = ClientsDepartmentsReturnsDetails.SelectByMaterialIssueVoucherIDs("," + materialIssueVoucherIDs + ",", GlobalVariables.IsArabic ? "1" : "0");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				((TextEditorControlBase)cboCurrency).Value = ((UltraGridBase)ULGData).Rows[0].Cells["CurrencyID"].Value;
				((Control)(object)txtExchangeRate).Text = ((UltraGridBase)ULGData).Rows[0].Cells["ExchangeRate"].Value.ToString();
			}
			InitGrid();
		}
		clbMaterialIssueVoucherNo.SelectedValueChanged += clbMaterialIssueVoucherNo_SelectedValueChanged;
	}

	private void btnVouchersSearch_Click(object sender, EventArgs e)
	{
		if (rbIsSalesnvoice.Checked)
		{
			int num = SearchFunctions.SLInvoicesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, -1, -1, -1);
			if (num != 0)
			{
				((TextEditorControlBase)cboSalesInvoiceNo).Value = num;
			}
		}
		else
		{
			int num2 = SearchFunctions.MaterialIssueVouchersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			if (num2 != 0)
			{
				((TextEditorControlBase)cboMaterialIssueVoucherNo).Value = num2;
			}
		}
	}

	private void cboMaterialIssueVoucherNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.MaterialIssueVouchersSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboMaterialIssueVoucherNo).Value = num;
			}
		}
	}

	private void cboSalesInvoiceNo_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.SLInvoicesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0, -1, -1, -1);
			if (num != 0)
			{
				((TextEditorControlBase)cboSalesInvoiceNo).Value = num;
			}
		}
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["SalesJVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = ClientsDepartmentsReturns.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			((Control)(object)txtInternalCode).Text = ClientsDepartmentsReturns.GetEInvoiceInternalCode(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void txtNet_ValueChanged(object sender, EventArgs e)
	{
		decimal num = decimal.Parse((((Control)(object)txtNet).Text == "") ? "0" : ((Control)(object)txtNet).Text) - decimal.Parse((((Control)(object)txtTotal).Text == "") ? "0" : ((Control)(object)txtTotal).Text);
		if (num > 0m)
		{
			((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
			((Control)(object)txtAddedValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
			((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
			((Control)(object)txtSubtractedValue).Text = "0";
			((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)txtSubtractedValue).ValueChanged -= txtSubtractedValue_ValueChanged;
			((Control)(object)txtSubtractedValue).Text = decimal.Parse(Math.Abs(num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtSubtractedValue).ValueChanged += txtSubtractedValue_ValueChanged;
			((TextEditorControlBase)txtAddedValue).ValueChanged -= txtAddedValue_ValueChanged;
			((Control)(object)txtAddedValue).Text = "0";
			((TextEditorControlBase)txtAddedValue).ValueChanged += txtAddedValue_ValueChanged;
		}
	}

	private void txtAddedValue_ValueChanged(object sender, EventArgs e)
	{
		CalculateNet();
	}

	private void txtSubtractedValue_ValueChanged(object sender, EventArgs e)
	{
		CalculateNet();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode Is Not Null ", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode Is Not Null ", "1");
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
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
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
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.EInvoices.Transactions.frmClientsReturnsEInvoice));
		UltraTab val = new UltraTab();
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
		this.rbIsMaterialIssueVoucher = new System.Windows.Forms.RadioButton();
		this.rbIsSalesnvoice = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnPrintEInvoice = new UltraButton();
		this.btnCancelInvoice = new UltraButton();
		this.btnSendInvoice = new UltraButton();
		this.lblCanceledDate = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.dtpCanceledDate = new UltraDateTimeEditor();
		this.dtpSendDate = new UltraDateTimeEditor();
		this.lblUUID = new UltraLabel();
		this.lblInternalCode = new UltraLabel();
		this.txtUUID = new UltraTextEditor();
		this.txtInternalCode = new UltraTextEditor();
		this.lblEINVState = new UltraLabel();
		this.cboState = new UltraComboEditor();
		this.lblCanceledUser = new UltraLabel();
		this.cboCanceledUserName = new UltraComboEditor();
		this.lblSendUser = new UltraLabel();
		this.cboSendUserName = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClient = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboSalesInvoiceNo = new UltraComboEditor();
		this.lblSalesInvoiceNo = new UltraLabel();
		this.lblClient = new UltraLabel();
		this.lblMaterialIssueVoucherNo = new UltraLabel();
		this.cboMaterialIssueVoucherNo = new UltraComboEditor();
		this.chkAll = new UltraCheckEditor();
		this.clbMaterialIssueVoucherNo = new System.Windows.Forms.CheckedListBox();
		this.lblTotal = new UltraLabel();
		this.txtTotal = new UltraTextEditor();
		this.btnVouchersSearch = new UltraButton();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.btnJV = new UltraButton();
		this.lblSubtractedValue = new UltraLabel();
		this.txtSubtractedValue = new UltraTextEditor();
		this.lblAddedValue = new UltraLabel();
		this.txtAddedValue = new UltraTextEditor();
		this.lblNet = new UltraLabel();
		this.txtNet = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpCanceledDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSendDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUUID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInternalCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCanceledUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSendUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialIssueVoucherNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubtractedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNet).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsMaterialIssueVoucher);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsSalesnvoice);
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsMaterialIssueVoucher, "rbIsMaterialIssueVoucher");
		this.rbIsMaterialIssueVoucher.BackColor = System.Drawing.Color.Transparent;
		this.rbIsMaterialIssueVoucher.Name = "rbIsMaterialIssueVoucher";
		this.rbIsMaterialIssueVoucher.TabStop = true;
		this.rbIsMaterialIssueVoucher.UseVisualStyleBackColor = false;
		this.rbIsMaterialIssueVoucher.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsSalesnvoice, "rbIsSalesnvoice");
		this.rbIsSalesnvoice.BackColor = System.Drawing.Color.Transparent;
		this.rbIsSalesnvoice.Name = "rbIsSalesnvoice";
		this.rbIsSalesnvoice.TabStop = true;
		this.rbIsSalesnvoice.UseVisualStyleBackColor = false;
		this.rbIsSalesnvoice.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintEInvoice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnCancelInvoice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnSendInvoice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCanceledDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCanceledDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.dtpSendDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblUUID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblInternalCode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtUUID);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtInternalCode);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVState);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboState);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblCanceledUser);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboCanceledUserName);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.lblSendUser);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.cboSendUserName);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnPrintEInvoice, "btnPrintEInvoice");
		((System.Windows.Forms.Control)(object)this.btnPrintEInvoice).Name = "btnPrintEInvoice";
		((System.Windows.Forms.Control)(object)this.btnPrintEInvoice).Click += new System.EventHandler(btnPrintEInvoice_Click);
		resources.ApplyResources(this.btnCancelInvoice, "btnCancelInvoice");
		((System.Windows.Forms.Control)(object)this.btnCancelInvoice).Name = "btnCancelInvoice";
		((System.Windows.Forms.Control)(object)this.btnCancelInvoice).Click += new System.EventHandler(btnCancelInvoice_Click);
		resources.ApplyResources(this.btnSendInvoice, "btnSendInvoice");
		((System.Windows.Forms.Control)(object)this.btnSendInvoice).Name = "btnSendInvoice";
		((System.Windows.Forms.Control)(object)this.btnSendInvoice).Click += new System.EventHandler(btnSendInvoice_Click);
		this.lblCanceledDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblCanceledDate, "lblCanceledDate");
		((System.Windows.Forms.Control)(object)this.lblCanceledDate).Name = "lblCanceledDate";
		((ControlBase)this.lblCanceledDate).WrapText = false;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpCanceledDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpCanceledDate, "dtpCanceledDate");
		this.dtpCanceledDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpCanceledDate).Name = "dtpCanceledDate";
		((UltraWinEditorMaskedControlBase)this.dtpSendDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpSendDate, "dtpSendDate");
		this.dtpSendDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpSendDate).Name = "dtpSendDate";
		this.lblUUID.AutoEllipsis = false;
		resources.ApplyResources(this.lblUUID, "lblUUID");
		((System.Windows.Forms.Control)(object)this.lblUUID).Name = "lblUUID";
		((ControlBase)this.lblUUID).WrapText = false;
		this.lblInternalCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblInternalCode, "lblInternalCode");
		((System.Windows.Forms.Control)(object)this.lblInternalCode).Name = "lblInternalCode";
		((ControlBase)this.lblInternalCode).WrapText = false;
		resources.ApplyResources(this.txtUUID, "txtUUID");
		((System.Windows.Forms.Control)(object)this.txtUUID).Name = "txtUUID";
		resources.ApplyResources(this.txtInternalCode, "txtInternalCode");
		((System.Windows.Forms.Control)(object)this.txtInternalCode).Name = "txtInternalCode";
		this.lblEINVState.AutoEllipsis = false;
		resources.ApplyResources(this.lblEINVState, "lblEINVState");
		((System.Windows.Forms.Control)(object)this.lblEINVState).Name = "lblEINVState";
		((ControlBase)this.lblEINVState).WrapText = false;
		((TextEditorControlBase)this.cboState).AlwaysInEditMode = true;
		this.cboState.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboState, "cboState");
		((System.Windows.Forms.Control)(object)this.cboState).Name = "cboState";
		this.lblCanceledUser.AutoEllipsis = false;
		resources.ApplyResources(this.lblCanceledUser, "lblCanceledUser");
		((System.Windows.Forms.Control)(object)this.lblCanceledUser).Name = "lblCanceledUser";
		((ControlBase)this.lblCanceledUser).WrapText = false;
		((TextEditorControlBase)this.cboCanceledUserName).AlwaysInEditMode = true;
		this.cboCanceledUserName.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCanceledUserName, "cboCanceledUserName");
		((System.Windows.Forms.Control)(object)this.cboCanceledUserName).Name = "cboCanceledUserName";
		this.lblSendUser.AutoEllipsis = false;
		resources.ApplyResources(this.lblSendUser, "lblSendUser");
		((System.Windows.Forms.Control)(object)this.lblSendUser).Name = "lblSendUser";
		((ControlBase)this.lblSendUser).WrapText = false;
		((TextEditorControlBase)this.cboSendUserName).AlwaysInEditMode = true;
		this.cboSendUserName.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSendUserName, "cboSendUserName");
		((System.Windows.Forms.Control)(object)this.cboSendUserName).Name = "cboSendUserName";
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClient, "cboClient");
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((EditorButtonControlBase)this.cboClient).ReadOnly = true;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((TextEditorControlBase)this.cboSalesInvoiceNo).AlwaysInEditMode = true;
		this.cboSalesInvoiceNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesInvoiceNo, "cboSalesInvoiceNo");
		((System.Windows.Forms.Control)(object)this.cboSalesInvoiceNo).Name = "cboSalesInvoiceNo";
		((TextEditorControlBase)this.cboSalesInvoiceNo).ValueChanged += new System.EventHandler(cboSalesInvoiceNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboSalesInvoiceNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSalesInvoiceNo_KeyDown);
		this.lblSalesInvoiceNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesInvoiceNo, "lblSalesInvoiceNo");
		((System.Windows.Forms.Control)(object)this.lblSalesInvoiceNo).Name = "lblSalesInvoiceNo";
		this.lblClient.AutoEllipsis = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		this.lblMaterialIssueVoucherNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblMaterialIssueVoucherNo, "lblMaterialIssueVoucherNo");
		((System.Windows.Forms.Control)(object)this.lblMaterialIssueVoucherNo).Name = "lblMaterialIssueVoucherNo";
		((ControlBase)this.lblMaterialIssueVoucherNo).WrapText = false;
		((TextEditorControlBase)this.cboMaterialIssueVoucherNo).AlwaysInEditMode = true;
		this.cboMaterialIssueVoucherNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboMaterialIssueVoucherNo, "cboMaterialIssueVoucherNo");
		((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherNo).Name = "cboMaterialIssueVoucherNo";
		((TextEditorControlBase)this.cboMaterialIssueVoucherNo).ValueChanged += new System.EventHandler(cboMaterialIssueVoucherNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboMaterialIssueVoucherNo_KeyDown);
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(this.chkAll, "chkAll");
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		this.clbMaterialIssueVoucherNo.CheckOnClick = true;
		this.clbMaterialIssueVoucherNo.FormattingEnabled = true;
		resources.ApplyResources(this.clbMaterialIssueVoucherNo, "clbMaterialIssueVoucherNo");
		this.clbMaterialIssueVoucherNo.Name = "clbMaterialIssueVoucherNo";
		this.clbMaterialIssueVoucherNo.SelectedValueChanged += new System.EventHandler(clbMaterialIssueVoucherNo_SelectedValueChanged);
		resources.ApplyResources(this.lblTotal, "lblTotal");
		this.lblTotal.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		((UltraButtonBase)this.btnVouchersSearch).AcceptsFocus = false;
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnVouchersSearch).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.btnVouchersSearch, "btnVouchersSearch");
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Name = "btnVouchersSearch";
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Click += new System.EventHandler(btnVouchersSearch_Click);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((EditorButtonControlBase)this.txtExchangeRate).ReadOnly = true;
		this.lblExchangeRate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		this.lblCurrency.AutoEllipsis = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((EditorButtonControlBase)this.cboCurrency).ReadOnly = true;
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.lblSubtractedValue, "lblSubtractedValue");
		this.lblSubtractedValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubtractedValue).Name = "lblSubtractedValue";
		((ControlBase)this.lblSubtractedValue).WrapText = false;
		resources.ApplyResources(this.txtSubtractedValue, "txtSubtractedValue");
		((System.Windows.Forms.Control)(object)this.txtSubtractedValue).Name = "txtSubtractedValue";
		((TextEditorControlBase)this.txtSubtractedValue).ValueChanged += new System.EventHandler(txtSubtractedValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSubtractedValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblAddedValue, "lblAddedValue");
		this.lblAddedValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedValue).Name = "lblAddedValue";
		((ControlBase)this.lblAddedValue).WrapText = false;
		resources.ApplyResources(this.txtAddedValue, "txtAddedValue");
		((System.Windows.Forms.Control)(object)this.txtAddedValue).Name = "txtAddedValue";
		((TextEditorControlBase)this.txtAddedValue).ValueChanged += new System.EventHandler(txtAddedValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAddedValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblNet, "lblNet");
		this.lblNet.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNet).Name = "lblNet";
		((ControlBase)this.lblNet).WrapText = false;
		resources.ApplyResources(this.txtNet, "txtNet");
		((System.Windows.Forms.Control)(object)this.txtNet).Name = "txtNet";
		((TextEditorControlBase)this.txtNet).ValueChanged += new System.EventHandler(txtNet_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtNet).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubtractedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSubtractedValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVouchersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add(this.clbMaterialIssueVoucherNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaterialIssueVoucherNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmClientsReturnsEInvoice";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaterialIssueVoucherNo, 0);
		base.Controls.SetChildIndex(this.clbMaterialIssueVoucherNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVouchersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubtractedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubtractedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
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
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpCanceledDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSendDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUUID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInternalCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCanceledUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSendUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialIssueVoucherNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubtractedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNet).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
