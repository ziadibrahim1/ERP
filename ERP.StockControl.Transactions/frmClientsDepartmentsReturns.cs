using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Sales;
using BusinessLayer.StockControl;
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

namespace ERP.StockControl.Transactions;

public class frmClientsDepartmentsReturns : frmHeaderDetails
{
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

	private UltraLabel lblDepartment;

	private UltraComboEditor cboDepartment;

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

	public frmClientsDepartmentsReturns()
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

	public frmClientsDepartmentsReturns(int ID)
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
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int n = 0; n < dtUnits.Rows.Count; n++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[n]["UnitID"], dtUnits.Rows[n]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlInvoiceDetailsTaxs.ValueListItems.Clear();
		for (int num = 0; num < dtTaxs.Rows.Count; num++)
		{
			vlInvoiceDetailsTaxs.ValueListItems.Add(dtTaxs.Rows[num]["TaxID"], dtTaxs.Rows[num]["TaxName"].ToString());
		}
		dtSalesInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "-1");
		GlobalFunctions.FillCombo(cboSalesInvoiceNo, dtSalesInvoices, "SLInvoiceID", "SLInvoiceNo");
		dtMaterialIssueVoucher = MaterialIssueVouchers.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "-1", "-1");
		GlobalFunctions.FillCombo(cboMaterialIssueVoucherNo, dtMaterialIssueVoucher, "MaterialIssueVoucherID", "MaterialIssueVoucherNo");
		vlMaterialIssueVoucher.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtMaterialIssueVoucher.Rows.Count; num2++)
		{
			vlMaterialIssueVoucher.ValueListItems.Add(dtMaterialIssueVoucher.Rows[num2]["MaterialIssueVoucherID"], dtMaterialIssueVoucher.Rows[num2]["MaterialIssueVoucherNo"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDepartments = Departments.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDepartment, dtDepartments, "DepartmentID", "DepartmentName");
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
		if (cboDepartment.SelectedIndex == -1)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnPrice"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnPrice"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		}
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
			((TextEditorControlBase)cboDepartment).Value = drMaster["DepartmentID"];
			UltraComboEditor obj = cboDepartment;
			bool visible = (((Control)(object)lblDepartment).Visible = cboDepartment.SelectedIndex != -1);
			((Control)(object)obj).Visible = visible;
			UltraLabel obj2 = lblTotal;
			UltraTextEditor obj3 = txtTotal;
			UltraLabel obj4 = lblAddedValue;
			UltraTextEditor obj5 = txtAddedValue;
			UltraLabel obj6 = lblSubtractedValue;
			UltraTextEditor obj7 = txtSubtractedValue;
			UltraLabel obj8 = lblNet;
			bool flag2 = (((Control)(object)txtNet).Visible = cboDepartment.SelectedIndex == -1);
			bool flag4 = (((Control)(object)obj8).Visible = flag2);
			bool flag6 = (((Control)(object)obj7).Visible = flag4);
			bool flag8 = (((Control)(object)obj6).Visible = flag6);
			bool flag10 = (((Control)(object)obj5).Visible = flag8);
			bool flag12 = (((Control)(object)obj4).Visible = flag10);
			visible = (((Control)(object)obj3).Visible = flag12);
			((Control)(object)obj2).Visible = visible;
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotal).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		cboDepartment.SelectedIndex = -1;
		cboSalesInvoiceNo.SelectedIndex = -1;
		cboMaterialIssueVoucherNo.SelectedIndex = -1;
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
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ClientsDepartmentsReturns.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsSalesnvoice.Checked ? "1" : "0", rbIsSalesnvoice.Checked ? ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString() : "Null", rbIsSalesnvoice.Checked ? "Null" : ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (cboDepartment.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDepartment).Value.ToString(), (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, (((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text, (((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text, "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Expected O, but got Unknown
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ClientsDepartmentsReturns.Insert_Update(drMaster["ReturnID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsSalesnvoice.Checked ? "1" : "0", rbIsSalesnvoice.Checked ? ((TextEditorControlBase)cboSalesInvoiceNo).Value.ToString() : "Null", rbIsSalesnvoice.Checked ? "Null" : ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (cboDepartment.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDepartment).Value.ToString(), (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtNotes).Text, ((Control)(object)txtTotal).Text, (((Control)(object)txtAddedValue).Text == "") ? "0" : ((Control)(object)txtAddedValue).Text, (((Control)(object)txtSubtractedValue).Text == "") ? "0" : ((Control)(object)txtSubtractedValue).Text, (drMaster["EInvoiceInternalCode"] == DBNull.Value) ? "Null" : drMaster["EInvoiceInternalCode"].ToString(), (drMaster["EInvoiceUUID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceUUID"].ToString(), (drMaster["EInvoiceSenDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceSenDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceSendUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceSendUserID"].ToString(), bool.Parse(drMaster["EInvoiceIsCanceled"].ToString()) ? "1" : "0", (drMaster["EInvoiceCanceledDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["EInvoiceCanceledDate"].ToString()).ToString(GlobalVariables.DateLongFormate), (drMaster["EInvoiceCanceledUserID"] == DBNull.Value) ? "Null" : drMaster["EInvoiceCanceledUserID"].ToString(), (drMaster["EINVStateID"] == DBNull.Value) ? "Null" : drMaster["EINVStateID"].ToString(), (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", "1", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		dtSearchResult = SearchFunctions.ClientsDepartmentsReturnsReport(-1, 0, 0);
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
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSalesInvoices = SLInvoices.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "-1");
		dtMaterialIssueVoucher = MaterialIssueVouchers.FillCombo(GlobalVariables.BranchIDs, "-1", "-1", "-1", "-1");
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
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtUnits.Rows.Count; num2++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num2]["UnitID"], dtUnits.Rows[num2]["UnitName"].ToString());
		}
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtDepartments = Departments.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDepartment, dtDepartments, "DepartmentID", "DepartmentName");
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
		UltraLabel obj4 = lblDepartment;
		visible = (((Control)(object)cboDepartment).Visible = false);
		((Control)(object)obj4).Visible = visible;
		UltraLabel obj5 = lblTotal;
		UltraTextEditor obj6 = txtTotal;
		UltraLabel obj7 = lblAddedValue;
		UltraTextEditor obj8 = txtAddedValue;
		UltraLabel obj9 = lblSubtractedValue;
		UltraTextEditor obj10 = txtSubtractedValue;
		UltraLabel obj11 = lblNet;
		bool flag5 = (((Control)(object)txtNet).Visible = true);
		bool flag7 = (((Control)(object)obj11).Visible = flag5);
		bool flag9 = (((Control)(object)obj10).Visible = flag7);
		bool flag11 = (((Control)(object)obj9).Visible = flag9);
		bool flag13 = (((Control)(object)obj8).Visible = flag11);
		bool flag15 = (((Control)(object)obj7).Visible = flag13);
		visible = (((Control)(object)obj6).Visible = flag15);
		((Control)(object)obj5).Visible = visible;
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

	private void cboMaterialIssueVoucherNo_ValueChanged(object sender, EventArgs e)
	{
		if (cboMaterialIssueVoucherNo.SelectedIndex > -1 && rbIsMaterialIssueVoucher.Checked)
		{
			((TextEditorControlBase)cboDepartment).Value = dtMaterialIssueVoucher.Select("MaterialIssueVoucherID = " + ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString())[0]["DepartmentID"];
			((TextEditorControlBase)cboClient).Value = dtMaterialIssueVoucher.Select("MaterialIssueVoucherID = " + ((TextEditorControlBase)cboMaterialIssueVoucherNo).Value.ToString())[0]["SubAccountID"];
			UltraLabel obj = lblDepartment;
			bool visible = (((Control)(object)cboDepartment).Visible = cboDepartment.SelectedIndex != -1);
			((Control)(object)obj).Visible = visible;
			UltraLabel obj2 = lblTotal;
			UltraTextEditor obj3 = txtTotal;
			UltraLabel obj4 = lblAddedValue;
			UltraTextEditor obj5 = txtAddedValue;
			UltraLabel obj6 = lblSubtractedValue;
			UltraTextEditor obj7 = txtSubtractedValue;
			UltraLabel obj8 = lblNet;
			bool flag2 = (((Control)(object)txtNet).Visible = cboDepartment.SelectedIndex == -1);
			bool flag4 = (((Control)(object)obj8).Visible = flag2);
			bool flag6 = (((Control)(object)obj7).Visible = flag4);
			bool flag8 = (((Control)(object)obj6).Visible = flag6);
			bool flag10 = (((Control)(object)obj5).Visible = flag8);
			bool flag12 = (((Control)(object)obj4).Visible = flag10);
			visible = (((Control)(object)obj3).Visible = flag12);
			((Control)(object)obj2).Visible = visible;
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode Is  Null ", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And EInvoiceInternalCode Is  Null ", "1");
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
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
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Transactions.frmClientsDepartmentsReturns));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsMaterialIssueVoucher = new System.Windows.Forms.RadioButton();
		this.rbIsSalesnvoice = new System.Windows.Forms.RadioButton();
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
		this.lblDepartment = new UltraLabel();
		this.cboDepartment = new UltraComboEditor();
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
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialIssueVoucherNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubtractedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNet).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
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
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance9");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsMaterialIssueVoucher);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsSalesnvoice);
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
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((EditorButtonControlBase)this.cboClient).ReadOnly = true;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.cboSalesInvoiceNo, "cboSalesInvoiceNo");
		((TextEditorControlBase)this.cboSalesInvoiceNo).AlwaysInEditMode = true;
		this.cboSalesInvoiceNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesInvoiceNo).Name = "cboSalesInvoiceNo";
		((TextEditorControlBase)this.cboSalesInvoiceNo).ValueChanged += new System.EventHandler(cboSalesInvoiceNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboSalesInvoiceNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSalesInvoiceNo_KeyDown);
		resources.ApplyResources(this.lblSalesInvoiceNo, "lblSalesInvoiceNo");
		this.lblSalesInvoiceNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesInvoiceNo).Name = "lblSalesInvoiceNo";
		((ControlBase)this.lblSalesInvoiceNo).WrapText = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		resources.ApplyResources(this.lblMaterialIssueVoucherNo, "lblMaterialIssueVoucherNo");
		this.lblMaterialIssueVoucherNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaterialIssueVoucherNo).Name = "lblMaterialIssueVoucherNo";
		((ControlBase)this.lblMaterialIssueVoucherNo).WrapText = false;
		resources.ApplyResources(this.cboMaterialIssueVoucherNo, "cboMaterialIssueVoucherNo");
		((TextEditorControlBase)this.cboMaterialIssueVoucherNo).AlwaysInEditMode = true;
		this.cboMaterialIssueVoucherNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherNo).Name = "cboMaterialIssueVoucherNo";
		((TextEditorControlBase)this.cboMaterialIssueVoucherNo).ValueChanged += new System.EventHandler(cboMaterialIssueVoucherNo_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboMaterialIssueVoucherNo).KeyDown += new System.Windows.Forms.KeyEventHandler(cboMaterialIssueVoucherNo_KeyDown);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.clbMaterialIssueVoucherNo, "clbMaterialIssueVoucherNo");
		this.clbMaterialIssueVoucherNo.CheckOnClick = true;
		this.clbMaterialIssueVoucherNo.FormattingEnabled = true;
		this.clbMaterialIssueVoucherNo.Name = "clbMaterialIssueVoucherNo";
		this.clbMaterialIssueVoucherNo.SelectedValueChanged += new System.EventHandler(clbMaterialIssueVoucherNo_SelectedValueChanged);
		resources.ApplyResources(this.lblTotal, "lblTotal");
		this.lblTotal.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotal).Name = "lblTotal";
		((ControlBase)this.lblTotal).WrapText = false;
		resources.ApplyResources(this.txtTotal, "txtTotal");
		((System.Windows.Forms.Control)(object)this.txtTotal).Name = "txtTotal";
		((UltraButtonBase)this.btnVouchersSearch).AcceptsFocus = false;
		resources.ApplyResources(this.btnVouchersSearch, "btnVouchersSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnVouchersSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Name = "btnVouchersSearch";
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Click += new System.EventHandler(btnVouchersSearch_Click);
		resources.ApplyResources(this.lblDepartment, "lblDepartment");
		this.lblDepartment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartment).Name = "lblDepartment";
		((ControlBase)this.lblDepartment).WrapText = false;
		resources.ApplyResources(this.cboDepartment, "cboDepartment");
		((TextEditorControlBase)this.cboDepartment).AlwaysInEditMode = true;
		this.cboDepartment.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDepartment).Name = "cboDepartment";
		((EditorButtonControlBase)this.cboDepartment).ReadOnly = true;
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((EditorButtonControlBase)this.txtExchangeRate).ReadOnly = true;
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDepartment);
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
		base.Name = "frmClientsDepartmentsReturns";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDepartment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepartment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSubtractedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubtractedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddedValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNet, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaterialIssueVoucherNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSubtractedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNet).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
