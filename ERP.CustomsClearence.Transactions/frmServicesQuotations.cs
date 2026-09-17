using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Transactions;

public class frmServicesQuotations : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtServices;

	private DataTable dtCurrency;

	private DataTable dtPricesTypes;

	private DataTable dtServicePrices;

	private DataTable dtTaxes;

	private DataTable dtSubAccounts;

	private DataTable dtServicesQuotationsDetailsPrices;

	private ValueList vlServices = new ValueList();

	private ValueList vlPriceTypes = new ValueList();

	private ValueList vlTaxes = new ValueList();

	private DataSet ds;

	private int newID = -100000;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblValidityDate;

	private UltraDateTimeEditor dtpValidTo;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblTotalPrice;

	private UltraTextEditor txtTotalPrice;

	private UltraCheckEditor chkClose;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraComboEditor cboSubAccountName;

	private UltraLabel lblClient;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	public frmServicesQuotations()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CST_ServicesQuotations";
		IDCol = "ServiceQuotationID";
		NoCol = "ServiceQuotationNo";
		DateCol = "ServiceQuotationDate";
	}

	public frmServicesQuotations(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPriceTypes.ValueListItems.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			vlPriceTypes.ValueListItems.Add(dtPricesTypes.Rows[i]["PriceTypeID"], dtPricesTypes.Rows[i]["PriceName"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int j = 0; j < dtServices.Rows.Count; j++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[j]["ServiceID"], dtServices.Rows[j]["ServiceName"].ToString());
		}
		dtServicePrices = ServicesPrices.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int k = 0; k < dtTaxes.Rows.Count; k++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[k]["TaxID"], dtTaxes.Rows[k]["TaxName"].ToString());
		}
		FillCurrencyDropDown();
		dtDetails = ServicesQuotationsDetails.SelectByServiceQuotationID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtServicesQuotationsDetailsPrices = ServicesQuotationsDetailsPrices.SelectByServiceQuotationID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtServicesQuotationsDetailsPrices);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtServicesQuotationsDetailsPrices";
		ds.Relations.Add(ds.Tables[0].Columns["ServiceQuotationDetailID"], ds.Tables[1].Columns["ServiceQuotationDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceQuotationDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Header).Caption = (GlobalVariables.IsArabic ? "يمكن تعديل السعر" : "Can Modify Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceID"].ValueList = (IValueList)(object)vlServices;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["AdditionalCompassPrice"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة الإضافية للبوصلة" : "Additional Compass Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["AdditionalCompassPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPriceTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaxID"].ValueList = (IValueList)(object)vlTaxes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Price"].DefaultCellValue = 0;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ServicesQuotations.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["ServiceQuotationNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ServiceQuotationDate"];
			((TextEditorControlBase)cboSubAccountName).Value = drMaster["SubAccountID"];
			dtpValidTo.Value = (DateTime)drMaster["ServiceQuotationValidTo"];
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkClose).Checked = bool.Parse(drMaster["Closed"].ToString());
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ServicesQuotationsDetails.SelectByServiceQuotationID(drMaster["ServiceQuotationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtServicesQuotationsDetailsPrices = ServicesQuotationsDetailsPrices.SelectByServiceQuotationID(drMaster["ServiceQuotationID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtServicesQuotationsDetailsPrices);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtServicesQuotationsDetailsPrices";
			ds.Relations.Add(ds.Tables[0].Columns["ServiceQuotationDetailID"], ds.Tables[1].Columns["ServiceQuotationDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
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
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		((EditorButtonControlBase)dtpValidTo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubAccountName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkClose).Enabled = !NavMode;
		if (Adding)
		{
			DataView dataView = new DataView(dtServices);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlServices.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlServices.ValueListItems.Add(dataTable.Rows[i]["ServiceID"], dataTable.Rows[i]["ServiceName"].ToString());
			}
		}
		else
		{
			vlServices.ValueListItems.Clear();
			for (int j = 0; j < dtServices.Rows.Count; j++)
			{
				vlServices.ValueListItems.Add(dtServices.Rows[j]["ServiceID"], dtServices.Rows[j]["ServiceName"].ToString());
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? ServicesQuotations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboSubAccountName.SelectedIndex = -1;
		((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
		cboCurrency.SelectedIndex = ((((DisposableObjectCollectionBase)cboCurrency.Items).Count <= 0) ? (-1) : 0);
		((Control)(object)txtExchangeRate).Text = ((((TextEditorControlBase)cboCurrency).Value != null && dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString()).Length != 0) ? dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString() : "");
		((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		dtpValidTo.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((TextEditorControlBase)txtNotes).Clear();
		((UltraToggleEditorBase)chkClose).Checked = false;
		((TextEditorControlBase)txtTotalPrice).Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		newID = -100000;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboSubAccountName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم العميل" : "Please Select Client Name");
			((TextEditorControlBase)cboSubAccountName).Focus();
			cboSubAccountName.DropDown();
			return false;
		}
		if (cboCurrency.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العملة" : "Please Select Currency");
			((TextEditorControlBase)cboCurrency).Focus();
			cboCurrency.DropDown();
			return false;
		}
		if (((Control)(object)txtExchangeRate).Text.Trim() == "" || decimal.Parse(((Control)(object)txtExchangeRate).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			((TextEditorControlBase)txtExchangeRate).Focus();
			return false;
		}
		if (int.Parse(ServicesQuotations.CheckQuotationValidity(((TextEditorControlBase)cboSubAccountName).Value.ToString(), Adding ? "0" : drMaster["ServiceQuotationID"].ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), dtpValidTo.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false).Rows[0][0].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("يوجد بالفعل عرض سعر  في هذه الفتره", "There Is A Service Quotation During This Period Of Time");
			((Control)(object)dtpValidTo).Focus();
			return false;
		}
		if (dtpValidTo.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ صالح حتى  ", "Please Enter Valid To Date ");
			((Control)(object)dtpValidTo).Focus();
			return false;
		}
		if (DateTime.Parse(dtpDate.Value.ToString()) > DateTime.Parse(dtpValidTo.Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("رجاءا اختر فتره صحيحه", "Quoatation Date Is After The Valid To Date");
			((Control)(object)dtpValidTo).Focus();
			return false;
		}
		if (Updating && drMaster != null && drMaster["MaxOperationDate"] != DBNull.Value && Convert.ToDateTime(drMaster["MaxOperationDate"]) > DateTime.Parse(dtpValidTo.Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("هناك عمليات قد تم عملها على هذا العرض بتاريخ لاحق لهذا التاريخ", "There Are Operations Made On This Quoatation With Date After This Date");
			((Control)(object)dtpValidTo).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CST_ServicesQuotations", "ServiceQuotationNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ServiceQuotationNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ServicesQuotations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الخدمة  ", "Please Enter Service Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].DroppedDown = true;
				return false;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count == 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخــال اسعار لهذه الخدمة", "Please insert Prices for this Service");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"];
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Price"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Price"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال السعر  ", "Please Enter Price ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Price"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
			}
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				if (i != k && ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[k].Cells["ServiceID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الخدمة ", "Cannot Duplicate The Same Service");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ServicesQuotations.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSubAccountName).Value.ToString(), dtpValidTo.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkClose).Checked ? "1" : "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = ServicesQuotationsDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ServiceID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CanModifyPrice"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Description"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					ServicesQuotationsDetailsPrices.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PriceTypeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["AdditionalCompassPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaxID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ServicesQuotations.Insert_Update(drMaster["ServiceQuotationID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSubAccountName).Value.ToString(), dtpValidTo.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "" || ((Control)(object)txtExchangeRate).Text == ".") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtNetprice).Text == "") ? "0" : ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkClose).Checked ? "1" : "0", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			string text = ",";
			string text2 = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ServiceQuotationDetailID"].Value.ToString() + ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					text2 = text2 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ServiceQuotationDetailPriceID"].Value.ToString() + ",";
				}
			}
			Main.DeleteForUpdate("CST_ServicesQuotationsDetailsPrices", "ServiceQuotationID", drMaster["ServiceQuotationID"].ToString(), "ServiceQuotationDetailPriceID", text2);
			Main.DeleteForUpdate("CST_ServicesQuotationsDetails", "ServiceQuotationID", drMaster["ServiceQuotationID"].ToString(), "ServiceQuotationDetailID", text);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				int num2 = ServicesQuotationsDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ServiceQuotationDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ServiceQuotationDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].Cells["ServiceQuotationDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["ServiceID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[k].Cells["CanModifyPrice"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					ServicesQuotationsDetailsPrices.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ServiceQuotationDetailPriceID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ServiceQuotationDetailPriceID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ServiceQuotationDetailPriceID"].Value.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PriceTypeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["AdditionalCompassPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["TaxID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["TaxValue"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
			ServicesQuotationsDetailsPrices.DeleteVirtualByServiceQuotationID(drMaster["ServiceQuotationID"].ToString(), GlobalVariables.UserID);
			ServicesQuotationsDetails.DeleteVirtualByServiceQuotationID(drMaster["ServiceQuotationID"].ToString(), GlobalVariables.UserID);
			ServicesQuotations.DeleteVirtual(drMaster["ServiceQuotationID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CST_ServicesQuotations_A.rpt" : "Rep_CST_ServicesQuotations_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ServiceQuotationIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTServicesQuotationsReport(DateTime.Now.AddYears(-10).ToString(GlobalVariables.DateShortFormate), -1, -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ServiceQuotationID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboSubAccountName).Value;
		object value2 = ((TextEditorControlBase)cboCurrency).Value;
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccountName, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPriceTypes.ValueListItems.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			vlPriceTypes.ValueListItems.Add(dtPricesTypes.Rows[i]["PriceTypeID"], dtPricesTypes.Rows[i]["PriceName"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int j = 0; j < dtServices.Rows.Count; j++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[j]["ServiceID"], dtServices.Rows[j]["ServiceName"].ToString());
		}
		dtServicePrices = ServicesPrices.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int k = 0; k < dtTaxes.Rows.Count; k++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[k]["TaxID"], dtTaxes.Rows[k]["TaxName"].ToString());
		}
		FillCurrencyDropDown();
		((TextEditorControlBase)cboSubAccountName).Value = value;
		((TextEditorControlBase)cboCurrency).Value = value2;
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["ServiceQuotationDetailID"].Value = ++newID;
		}
		((Control)(object)ULGData).Enter += ULGData_Enter;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Expected O, but got Unknown
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtServices.Select(" ServiceID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value = e.Cell.Value;
			DataRow[] array = dtServicePrices.Select(" ServiceID = " + e.Cell.Value);
			if (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ServiceQuotationDetailID"].Value.ToString()) != -1)
			{
				int num = int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ServiceQuotationDetailID"].Value.ToString());
				DataRow[] array2 = dtServicesQuotationsDetailsPrices.Select("ServiceQuotationDetailID = " + num);
				DataRow[] array3 = array2;
				foreach (DataRow dataRow2 in array3)
				{
					dataRow2.Delete();
				}
			}
			else
			{
				int num = ++newID;
				((UltraGridBase)ULGData).ActiveRow.Cells["ServiceQuotationDetailID"].Value = num;
			}
			((UltraGridBase)ULGData).UpdateData();
			for (int j = 0; j < array.Length; j++)
			{
				DataRow dataRow3 = dtServicesQuotationsDetailsPrices.NewRow();
				dataRow3["ServiceQuotationDetailPriceID"] = ++newID;
				dataRow3["ServiceQuotationDetailID"] = e.Cell.Row.Cells["ServiceQuotationDetailID"].Value.ToString();
				dataRow3["ServiceQuotationID"] = -1;
				dataRow3["PriceTypeID"] = array[j]["PriceTypeID"];
				dataRow3["Price"] = array[j]["Price"];
				dataRow3["AdditionalCompassPrice"] = array[j]["AdditionalCompassPrice"];
				dataRow3["TaxID"] = dataRow["TaxID"];
				dataRow3["TaxValue"] = ((dataRow["TaxID"] == DBNull.Value) ? "0" : (decimal.Parse(dtTaxes.Select(" TaxID= " + dataRow["TaxID"].ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(array[j]["Price"].ToString()) + decimal.Parse(array[j]["AdditionalCompassPrice"].ToString()))).ToString());
				dataRow3["Deleted"] = false;
				dataRow3["BranchID"] = DBNull.Value;
				dtServicesQuotationsDetailsPrices.Rows.Add(dataRow3);
			}
			((UltraGridBase)ULGData).UpdateData();
			InitGrid();
			CalculateTotals();
			CalculateTotalTax();
			CalculateNetTotals();
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRowTax(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalTax();
			CalculateNetTotals();
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ServiceID" && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && dtServices.Select(" ServiceID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value = DBNull.Value);
			activeCell.Value = value;
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ServiceID")
		{
			int num = SearchFunctions.CSTServicesSearch(IsFromServer: false);
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ServiceID"].Value = num;
			}
		}
		e.Handled = true;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalCompassPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null && ((GridItemBase)ULGData.ActiveCell).Band.Index == 1)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalCompassPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			CalculateTotals();
			CalculateRowTax(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalTax();
			CalculateNetTotals();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
		CalculateTotalTax();
		CalculateNetTotals();
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow == null)
		{
			return;
		}
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
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
			if (bool.Parse(drMaster["Approved"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لأنها معتمده", "Cannot Update This Transaction Because It Is Approved");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
		if (((UltraToggleEditorBase)chkClose).Checked)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها مغلقة", "Cannot Delete This Transaction Because It Is Closed");
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

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Price"].Value.ToString());
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["AdditionalCompassPrice"].Value.ToString());
			}
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		object value = ((TextEditorControlBase)cboCurrency).Value;
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = ServicesQuotations.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
		((TextEditorControlBase)cboCurrency).Value = value;
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboCurrency_ValueChanged(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex != -1)
		{
			((Control)(object)txtExchangeRate).Text = decimal.Parse(dtCurrency.Select(" CurrencyID= " + ((TextEditorControlBase)cboCurrency).Value.ToString())[0]["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtExchangeRate).Text = "";
		}
	}

	private void CalculateRowTax(UltraGridRow Row)
	{
		if (((GridItemBase)Row).Band.Index == 1)
		{
			if (Row.Cells["TaxID"].Value != DBNull.Value)
			{
				Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["Price"].Value.ToString()) + decimal.Parse(Row.Cells["AdditionalCompassPrice"].Value.ToString()));
			}
			else
			{
				Row.Cells["TaxValue"].Value = 0;
			}
		}
	}

	private void CalculateTotalTax()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaxValue"].Value.ToString());
			}
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Transactions.frmServicesQuotations));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblValidityDate = new UltraLabel();
		this.dtpValidTo = new UltraDateTimeEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.lblTotalPrice = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.chkClose = new UltraCheckEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.cboSubAccountName = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpValidTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClose).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		this.lblValidityDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblValidityDate, "lblValidityDate");
		((System.Windows.Forms.Control)(object)this.lblValidityDate).Name = "lblValidityDate";
		((ControlBase)this.lblValidityDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpValidTo).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpValidTo, "dtpValidTo");
		this.dtpValidTo.MaskInput = "{date} ";
		((System.Windows.Forms.Control)(object)this.dtpValidTo).Name = "dtpValidTo";
		this.lblCurrency.AutoEllipsis = false;
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		resources.ApplyResources(this.chkClose, "chkClose");
		((System.Windows.Forms.Control)(object)this.chkClose).Name = "chkClose";
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		this.lblExchangeRate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		((TextEditorControlBase)this.cboSubAccountName).AlwaysInEditMode = true;
		this.cboSubAccountName.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSubAccountName, "cboSubAccountName");
		((System.Windows.Forms.Control)(object)this.cboSubAccountName).Name = "cboSubAccountName";
		this.lblClient.AutoEllipsis = false;
		resources.ApplyResources(this.lblClient, "lblClient");
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValidityDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpValidTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmServicesQuotations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpValidTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValidityDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpValidTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClose).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccountName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
