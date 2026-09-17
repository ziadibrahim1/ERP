using System;
using System.Collections;
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
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sales.Transactions;

public class frmSLOrders : frmHeaderDetails
{
	private DataTable dtBranches;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtReports;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtUnits;

	private DataTable dtClients;

	private DataTable dtQuotations;

	private DataTable dtCurrency;

	private DataTable dtQuotationDetails;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private int rowIndex = -1;

	private IContainer components = null;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClients;

	private UltraLabel lblClients;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblQuotation;

	private UltraComboEditor cboQuotation;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraTextEditor txtExchangeRate;

	private UltraLabel lblExchangeRate;

	private UltraLabel lblTotalPrice;

	private UltraTextEditor txtTotalPrice;

	public UltraButton btnClientsSearch;

	public UltraButton btnQuotationSearch;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsQuotation;

	private RadioButton rbIsDirectOrder;

	private UltraCheckEditor chkBranches;

	public UltraButton btnBranchesSearch;

	private UltraComboEditor cboBranches;

	private UltraTextEditor txtRecipient;

	private UltraLabel lblRecipient;

	private UltraLabel lblQuotationValidity;

	private UltraLabel lblDeliveryTerms;

	private UltraLabel lblPacking;

	private UltraLabel lblPaymentTerms;

	private UltraLabel lblManufacturerAndOrigin;

	private UltraLabel lblDeliveryPeriod;

	private UltraTextEditor txtPacking;

	private UltraTextEditor txtPaymentTerms;

	private UltraTextEditor txtManufacturerAndOrigin;

	private UltraTextEditor txtDeliveryTerms;

	private UltraTextEditor txtDeliveryPeriod;

	private UltraTextEditor txtQuotationValidity;

	public frmSLOrders()
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
		InitializeComponent();
		TableName = "SL_SLOrders";
		IDCol = "SLOrderID";
		NoCol = "SLOrderNo";
		DateCol = "SLOrderDate";
	}

	public frmSLOrders(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtQuotations = Quotations.FillCombo(GlobalVariables.BranchIDs, "-1");
		GlobalFunctions.FillCombo(cboQuotation, dtQuotations, "QuotationID", "QuotationNo");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClients, dtClients, "SubAccountID", "SubAccountName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		FillCurrencyDropDown();
		dtDetails = SLOrdersDetails.SelectBySLOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLOrderDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IssueQty"].DefaultCellValue = 0;
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemDescription"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الإجمالي" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف الصنف" : "Item Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemDescription"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = SLOrders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboClients).ValueChanged -= cboClient_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["SLOrderNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["SLOrderDate"];
			((TextEditorControlBase)cboClients).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)cboQuotation).Value = drMaster["QuotationID"];
			rbIsDirectOrder.Checked = bool.Parse(drMaster["IsDirectOrder"].ToString());
			rbIsQuotation.Checked = ((drMaster["QuotationID"] != DBNull.Value) ? true : false);
			((TextEditorControlBase)cboCurrency).Value = drMaster["CurrencyID"];
			((Control)(object)txtRecipient).Text = drMaster["Recipient"].ToString();
			((Control)(object)txtQuotationValidity).Text = drMaster["QuotationValidity"].ToString();
			((Control)(object)txtDeliveryTerms).Text = drMaster["DeliveryTerms"].ToString();
			((Control)(object)txtDeliveryPeriod).Text = drMaster["DeliveryPeriod"].ToString();
			((Control)(object)txtManufacturerAndOrigin).Text = drMaster["ManufacturerAndOrigin"].ToString();
			((Control)(object)txtPacking).Text = drMaster["Packing"].ToString();
			((Control)(object)txtPaymentTerms).Text = drMaster["PaymentTerms"].ToString();
			((Control)(object)txtExchangeRate).Text = decimal.Parse(drMaster["ExchangeRate"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = SLOrdersDetails.SelectBySLOrderID(drMaster["SLOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			((TextEditorControlBase)cboClients).ValueChanged += cboClient_ValueChanged;
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
		rbIsDirectOrder.Enabled = !NavMode;
		rbIsQuotation.Enabled = !NavMode;
		((EditorButtonControlBase)cboClients).ReadOnly = NavMode;
		((EditorButtonControlBase)cboQuotation).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCurrency).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExchangeRate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRecipient).ReadOnly = NavMode;
		((EditorButtonControlBase)txtQuotationValidity).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDeliveryTerms).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDeliveryPeriod).ReadOnly = NavMode;
		((EditorButtonControlBase)txtManufacturerAndOrigin).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPacking).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaymentTerms).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranches).ReadOnly = !ViewAllBranches;
		((Control)(object)btnBranchesSearch).Visible = !NavMode;
		((Control)(object)chkBranches).Enabled = Adding;
		((Control)(object)btnClientsSearch).Visible = !NavMode;
		((Control)(object)btnQuotationSearch).Visible = !NavMode && rbIsQuotation.Checked;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtQuotations);
			dataView.RowFilter = " Approved = 1 and BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "QuotationID", "QuotationNo");
		}
		else
		{
			int num = 0;
			if (cboQuotation.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboQuotation).Value.ToString());
			}
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			GlobalFunctions.FillCombo(cboQuotation, dtQuotations, "QuotationID", "QuotationNo");
			if (num > 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num;
			}
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
		}
		if (Adding)
		{
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView2.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int j = 0; j < dtItems.Rows.Count; j++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
			}
		}
		if (!Updating)
		{
			return;
		}
		if (cboQuotation.SelectedIndex > -1)
		{
			dtQuotationDetails = QuotationsDetails.SelectByQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value = DBNull.Value;
				}
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value = DBNull.Value;
				}
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? SLOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		cboClients.SelectedIndex = -1;
		cboCurrency.SelectedIndex = -1;
		cboQuotation.SelectedIndex = -1;
		rbIsQuotation.Checked = false;
		rbIsDirectOrder.Checked = true;
		((TextEditorControlBase)txtRecipient).Clear();
		((TextEditorControlBase)txtQuotationValidity).Clear();
		((TextEditorControlBase)txtDeliveryTerms).Clear();
		((TextEditorControlBase)txtDeliveryPeriod).Clear();
		((TextEditorControlBase)txtManufacturerAndOrigin).Clear();
		((TextEditorControlBase)txtPacking).Clear();
		((TextEditorControlBase)txtPaymentTerms).Clear();
		((UltraToggleEditorBase)chkBranches).Checked = false;
		((TextEditorControlBase)cboBranches).Value = GlobalVariables.CurrentBranchID;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtExchangeRate).Clear();
		((TextEditorControlBase)txtTotalPrice).Clear();
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
		if (cboClients.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المورد" : "Please Select Client");
			((TextEditorControlBase)cboClients).Focus();
			cboClients.DropDown();
			return false;
		}
		if (rbIsQuotation.Checked && cboQuotation.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار عرض السعر" : "Please Select Quotation No");
			((TextEditorControlBase)cboQuotation).Focus();
			cboQuotation.DropDown();
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SL_SLOrders", "SLOrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["SLOrderNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = SLOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف ", "Cannot Duplicate The Same Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = SLOrders.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectOrder.Checked ? "1" : "0", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), ((TextEditorControlBase)cboClients).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtRecipient).Text, ((Control)(object)txtDeliveryPeriod).Text, ((Control)(object)txtDeliveryTerms).Text, ((Control)(object)txtPaymentTerms).Text, ((Control)(object)txtManufacturerAndOrigin).Text, ((Control)(object)txtQuotationValidity).Text, ((Control)(object)txtPacking).Text, ((Control)(object)txtNotes).Text, "0", "0", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SLOrderDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["SLOrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			SLOrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = SLOrders.Insert_Update(drMaster["SLOrderID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), rbIsDirectOrder.Checked ? "1" : "0", (cboQuotation.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotation).Value.ToString(), ((TextEditorControlBase)cboClients).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, ((TextEditorControlBase)cboCurrency).Value.ToString(), (((Control)(object)txtExchangeRate).Text == "") ? "0" : ((Control)(object)txtExchangeRate).Text, ((Control)(object)txtRecipient).Text, ((Control)(object)txtDeliveryPeriod).Text, ((Control)(object)txtDeliveryTerms).Text, ((Control)(object)txtPaymentTerms).Text, ((Control)(object)txtManufacturerAndOrigin).Text, ((Control)(object)txtQuotationValidity).Text, ((Control)(object)txtPacking).Text, ((Control)(object)txtNotes).Text, bool.Parse(drMaster["IsMIVFirst"].ToString()) ? "1" : "0", bool.Parse(drMaster["HasSLInvoice"].ToString()) ? "1" : "0", bool.Parse(drMaster["Closed"].ToString()) ? "1" : "0", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SLOrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["SLOrderDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("SL_SLOrdersDetails", "SLOrderID", drMaster["SLOrderID"].ToString(), "SLOrderDetailID", text);
			SLOrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			SLOrders.DeleteVirtual(drMaster["SLOrderID"].ToString(), GlobalVariables.UserID);
			SLOrdersDetails.DeleteVirtualBySLOrderID(drMaster["SLOrderID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
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
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (bool.Parse(drMaster["HasSLInvoice"].ToString()))
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لوجود فاتورة مبيعات على أمر البيع", "Cannot Delete This Transaction Because there Are Sales Invoice On This Sales Order");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DataSaved = true;
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
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
			if (bool.Parse(drMaster["HasSLInvoice"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود فاتورة مبيعات على إذن أمر البيع برجاء حذف الفاتورة اولا", "Cannot Update This Transaction Because Sales Invoice Was Made on Sales order Please Delete Sales Invoice First   ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
		}
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SL_SLOrders_A.rpt" : "Rep_SL_SLOrders_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@SLOrderIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLOrdersReport(-1, 0, -1, -1);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["SLOrderID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		object value = ((TextEditorControlBase)cboCurrency).Value;
		object value2 = ((TextEditorControlBase)cboQuotation).Value;
		object value3 = ((TextEditorControlBase)cboClients).Value;
		object value4 = ((TextEditorControlBase)cboTransactionBranch).Value;
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtQuotations = Quotations.FillCombo(GlobalVariables.BranchIDs, "-1");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
		if (Adding)
		{
			DataView dataView = new DataView(dtQuotations);
			dataView.RowFilter = "Approved = 1 and BranchID= " + GlobalVariables.CurrentBranchID;
			GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "QuotationID", "QuotationNo");
			DataView dataView2 = new DataView(dtItems);
			dataView2.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView2.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[k]["ItemID"], dataTable.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[k]["ItemID"], dataTable.Rows[k]["ItemBarCode"].ToString());
			}
		}
		else
		{
			GlobalFunctions.FillCombo(cboQuotation, dtQuotations, "QuotationID", "QuotationNo");
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		FillCurrencyDropDown();
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView3 = new DataView(dtClients);
			dataView3.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClients, dataView3.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClients, dtClients, "SubAccountID", "SubAccountName");
		}
		((TextEditorControlBase)cboClients).Value = value3;
		((TextEditorControlBase)cboCurrency).Value = value;
		((TextEditorControlBase)cboQuotation).Value = value2;
		((TextEditorControlBase)cboTransactionBranch).Value = value4;
	}

	private ValueList getUnitsValueList(int UnitTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtUnits.Select("UnitTypeID=" + UnitTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["UnitID"].ToString(), array[i]["UnitName"].ToString());
		}
		return val;
	}

	private ValueList getColorsValueList(int ItemColorCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsColorCategorysDetails.Select("ItemColorCategoryID=" + ItemColorCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ColorID"].ToString(), dtColors.Select("ColorID =" + array[i]["ColorID"].ToString())[0]["ColorName"].ToString());
		}
		return val;
	}

	private ValueList getSizesValueList(int ItemSizeCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID=" + ItemSizeCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemSizeID"].ToString(), dtSizes.Select("ItemSizeID =" + array[i]["ItemSizeID"].ToString())[0]["ItemSizeName"].ToString());
		}
		return val;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af8: Expected O, but got Unknown
		//IL_0b06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b10: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			e.Cell.Row.Cells["UnitPrice"].Value = 0;
			e.Cell.Row.Cells["TotalPrice"].Value = 0;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			}
			else
			{
				e.Cell.Row.Cells["UnitID"].ValueList = null;
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ColorID"].Value = 1;
				e.Cell.Row.Cells["ColorID"].ValueList = null;
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = 1;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
			}
			if (cboQuotation.SelectedIndex > -1 && dtQuotationDetails != null)
			{
				DataRow[] array = dtQuotationDetails.Select(string.Concat("ItemID=", e.Cell.Value, e.Cell.Row.Cells["ColorID"].Value.Equals(DBNull.Value) ? "" : (" And ColorID=" + e.Cell.Row.Cells["ColorID"].Value), e.Cell.Row.Cells["ItemSizeID"].Value.Equals(DBNull.Value) ? "" : (" And ItemSizeID=" + e.Cell.Row.Cells["ItemSizeID"].Value)));
				if (array.Length != 0)
				{
					e.Cell.Row.Cells["UnitPrice"].Value = array[0]["UnitPrice"].ToString();
					e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
				}
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ColorID" && !e.Cell.Row.Cells["ItemSizeID"].Value.Equals(DBNull.Value) && !e.Cell.Row.Cells["ItemID"].Value.Equals(DBNull.Value) && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			if (cboQuotation.SelectedIndex > -1)
			{
				DataRow[] array2 = dtQuotationDetails.Select(string.Concat("ItemID=", e.Cell.Row.Cells["ItemID"].Value, " And ColorID=", e.Cell.Value, " And ItemSizeID=", e.Cell.Row.Cells["ItemSizeID"].Value));
				if (array2.Length != 0)
				{
					e.Cell.Row.Cells["UnitPrice"].Value = array2[0]["UnitPrice"].ToString();
					e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
				}
				else
				{
					e.Cell.Row.Cells["UnitPrice"].Value = 0;
					e.Cell.Row.Cells["TotalPrice"].Value = 0;
				}
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemSizeID" && !e.Cell.Row.Cells["ColorID"].Value.Equals(DBNull.Value) && !e.Cell.Row.Cells["ItemID"].Value.Equals(DBNull.Value) && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && cboQuotation.SelectedIndex > -1)
		{
			DataRow[] array3 = dtQuotationDetails.Select(string.Concat("ItemID=", e.Cell.Row.Cells["ItemID"].Value, " And ColorID=", e.Cell.Row.Cells["ColorID"].Value, " And ItemSizeID=", e.Cell.Value));
			if (array3.Length != 0)
			{
				e.Cell.Row.Cells["UnitPrice"].Value = array3[0]["UnitPrice"].ToString();
				e.Cell.Row.Cells["TotalPrice"].Value = decimal.Parse(e.Cell.Row.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(e.Cell.Row.Cells["Qty"].Value.ToString());
			}
			else
			{
				e.Cell.Row.Cells["UnitPrice"].Value = 0;
				e.Cell.Row.Cells["TotalPrice"].Value = 0;
			}
		}
		CalculateTotals();
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value || (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID") && ((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value.ToString()) > 0m)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && !CanModifyPriceType && !rbIsDirectOrder.Checked)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && !CanModifyQty && !rbIsDirectOrder.Checked)
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
				frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
				frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
				frmQuantityMultiUnit2.ShowDialog();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value);
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if ((Adding || Updating) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode"))
			{
				int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num;
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Expected O, but got Unknown
		if (ULGData.ActiveCell == null)
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
		{
			ULGData.ActiveCell.Value = 0;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(" لايمكن نقص الكمية عن الكمية المنصرفة فى المخازن و قدرها " + ((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value.ToString(), "You Cannot Decrease The Quantity From The Issue Qty in the Store" + ((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value;
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		CalculateTotals();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IssueQty"].Value.ToString()) > 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لانه تم صرفه فى المخازن" : "Cannot Delete This Item Because Issue From The Store");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
		rowIndex = -1;
	}

	private void CalculateTotals()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
		else if (rowIndex > -1 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > rowIndex)
		{
			frmEnterQuantity frmEnterQuantity2 = new frmEnterQuantity(((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Text.Split('-')[0].ToString(), ((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
			frmEnterQuantity2.WindowState = FormWindowState.Normal;
			if (frmEnterQuantity2.ShowDialog() == DialogResult.OK)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString()) + decimal.Parse(frmEnterQuantity2.Value);
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
				CalculateTotals();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
	}

	public void AddItemInGid()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string s = arrayList[4].ToString();
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
				rowIndex = i;
				CalculateTotals();
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				return;
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
		object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
		obj.Value = value;
		DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
		int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
		ValueList unitsValueList = getUnitsValueList(unitTypeID);
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
		if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void FillCurrencyDropDown()
	{
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		FillCurrencyDropDown();
		if (Adding)
		{
			((Control)(object)txtCode).Text = SLOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
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

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + " ( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClients, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClients, dtClients, "SubAccountID", "SubAccountName");
		}
	}

	private void chkBranches_CheckedChanged(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtClients);
			dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
			GlobalFunctions.FillCombo(cboClients, dataView.ToTable(), "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboClients, dtClients, "SubAccountID", "SubAccountName");
		}
		dtDetails.Rows.Clear();
	}

	private void cboQuotation_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.PSQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			if (num != 0)
			{
				((TextEditorControlBase)cboQuotation).Value = num;
			}
		}
	}

	private void cboQuotation_ValueChanged(object sender, EventArgs e)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069f: Expected O, but got Unknown
		if (cboQuotation.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboClients).ValueChanged -= cboClient_ValueChanged;
			dtQuotationDetails = QuotationsDetails.SelectByQuotationID(((TextEditorControlBase)cboQuotation).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			dtDetails.Rows.Clear();
			GlobalVariables.QuestionMB.Show("هل تريد إضافة كل أصناف العرض ؟", "Are You Sure You want to Generate All Quotation Items?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				for (int i = 0; i < dtQuotationDetails.Rows.Count; i++)
				{
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtQuotationDetails.Rows[i]["ItemID"].ToString());
					obj.Value = value;
					DataRow dataRow = dtItems.Select(" ItemID= " + dtQuotationDetails.Rows[i]["ItemID"].ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dtQuotationDetails.Rows[i]["ColorID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dtQuotationDetails.Rows[i]["ItemSizeID"];
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtQuotationDetails.Rows[i]["UnitID"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemDescription"].Value = dtQuotationDetails.Rows[i]["ItemDescription"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dtQuotationDetails.Rows[i]["Qty"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dtQuotationDetails.Rows[i]["UnitPrice"].ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = dtQuotationDetails.Rows[i]["TotalPrice"].ToString();
					if (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value)
					{
						int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
						ValueList unitsValueList = getUnitsValueList(unitTypeID);
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
					}
					if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					}
				}
			}
			DataRow dataRow2 = dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotation).Value.ToString())[0];
			((TextEditorControlBase)cboClients).Value = dataRow2["SubAccountID"];
			if (((TextEditorControlBase)cboClients).Value == null)
			{
				((Control)(object)cboClients).Text = "";
			}
			((Control)(object)txtDeliveryPeriod).Text = dataRow2["DeliveryPeriod"].ToString();
			((Control)(object)txtDeliveryTerms).Text = dataRow2["SubAccountID"].ToString();
			((Control)(object)txtManufacturerAndOrigin).Text = dataRow2["ManufacturerAndOrigin"].ToString();
			((Control)(object)txtPacking).Text = dataRow2["Packing"].ToString();
			((Control)(object)txtPaymentTerms).Text = dataRow2["PaymentTerms"].ToString();
			((Control)(object)txtQuotationValidity).Text = dataRow2["QuotationValidity"].ToString();
			((Control)(object)txtRecipient).Text = dataRow2["Recipient"].ToString();
			((TextEditorControlBase)cboCurrency).Value = dtQuotations.Select(" QuotationID =" + ((TextEditorControlBase)cboQuotation).Value.ToString())[0]["CurrencyID"];
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			CalculateTotals();
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboClients).ValueChanged += cboClient_ValueChanged;
		}
		else
		{
			dtDetails.Rows.Clear();
			if (cboBranches.SelectedIndex > -1)
			{
				DataView dataView = new DataView(dtClients);
				dataView.RowFilter = (((UltraToggleEditorBase)chkBranches).Checked ? (" BranchID= " + ((TextEditorControlBase)cboBranches).Value.ToString() + " And ") : "") + "( ForAllBranches=1 or BranchID=  " + GlobalVariables.CurrentBranchID + " ) ";
				GlobalFunctions.FillCombo(cboClients, dataView.ToTable(), "SubAccountID", "SubAccountName");
			}
			else
			{
				GlobalFunctions.FillCombo(cboClients, dtClients, "SubAccountID", "SubAccountName");
			}
		}
	}

	private void btnQuotationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SLQuotationstSearch("," + GlobalVariables.CurrentBranchID + ",", 1, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboQuotation).Value = num;
		}
	}

	private void RadioButtons_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)lblQuotation).Visible = rbIsQuotation.Checked;
		((Control)(object)cboQuotation).Visible = rbIsQuotation.Checked;
		if (Adding || Updating)
		{
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress -= txtExchangeRate_KeyPress;
			((TextEditorControlBase)cboClients).ValueChanged -= cboClient_ValueChanged;
			cboQuotation.SelectedIndex = -1;
			cboClients.SelectedIndex = -1;
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "";
			((Control)(object)btnQuotationSearch).Visible = rbIsQuotation.Checked;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
			((Control)(object)txtExchangeRate).KeyPress += txtExchangeRate_KeyPress;
			((TextEditorControlBase)cboClients).ValueChanged += cboClient_ValueChanged;
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClients).Value = num;
			}
		}
	}

	private void btnClientsSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients((cboBranches.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboBranches).Value.ToString(), "1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClients).Value = num;
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		if (cboClients.SelectedIndex > -1 && rbIsQuotation.Checked)
		{
			((TextEditorControlBase)cboQuotation).ValueChanged -= cboQuotation_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged -= cboCurrency_ValueChanged;
			DataView dataView = new DataView(dtQuotations);
			dataView.RowFilter = " SubAccountID= " + ((TextEditorControlBase)cboClients).Value.ToString();
			GlobalFunctions.FillCombo(cboQuotation, dataView.ToTable(), "QuotationID", "QuotationNo");
			dtDetails.Rows.Clear();
			cboCurrency.SelectedIndex = -1;
			((Control)(object)txtExchangeRate).Text = "";
			((TextEditorControlBase)cboQuotation).ValueChanged += cboQuotation_ValueChanged;
			((TextEditorControlBase)cboCurrency).ValueChanged += cboCurrency_ValueChanged;
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
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
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
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0637: Expected O, but got Unknown
		//IL_0675: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sales.Transactions.frmSLOrders));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsQuotation = new System.Windows.Forms.RadioButton();
		this.rbIsDirectOrder = new System.Windows.Forms.RadioButton();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClients = new UltraComboEditor();
		this.lblClients = new UltraLabel();
		this.txtBarCode = new UltraTextEditor();
		this.lblQuotation = new UltraLabel();
		this.cboQuotation = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.txtExchangeRate = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.lblTotalPrice = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.btnClientsSearch = new UltraButton();
		this.btnQuotationSearch = new UltraButton();
		this.chkBranches = new UltraCheckEditor();
		this.btnBranchesSearch = new UltraButton();
		this.cboBranches = new UltraComboEditor();
		this.txtRecipient = new UltraTextEditor();
		this.lblRecipient = new UltraLabel();
		this.lblQuotationValidity = new UltraLabel();
		this.lblDeliveryTerms = new UltraLabel();
		this.lblPacking = new UltraLabel();
		this.lblPaymentTerms = new UltraLabel();
		this.lblManufacturerAndOrigin = new UltraLabel();
		this.lblDeliveryPeriod = new UltraLabel();
		this.txtPacking = new UltraTextEditor();
		this.txtPaymentTerms = new UltraTextEditor();
		this.txtManufacturerAndOrigin = new UltraTextEditor();
		this.txtDeliveryTerms = new UltraTextEditor();
		this.txtDeliveryPeriod = new UltraTextEditor();
		this.txtQuotationValidity = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClients).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRecipient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPacking).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentTerms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtManufacturerAndOrigin).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryTerms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuotationValidity).BeginInit();
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
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
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
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsQuotation);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsDirectOrder);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsQuotation, "rbIsQuotation");
		this.rbIsQuotation.BackColor = System.Drawing.Color.Transparent;
		this.rbIsQuotation.Name = "rbIsQuotation";
		this.rbIsQuotation.TabStop = true;
		this.rbIsQuotation.UseVisualStyleBackColor = false;
		this.rbIsQuotation.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.rbIsDirectOrder, "rbIsDirectOrder");
		this.rbIsDirectOrder.BackColor = System.Drawing.Color.Transparent;
		this.rbIsDirectOrder.Name = "rbIsDirectOrder";
		this.rbIsDirectOrder.TabStop = true;
		this.rbIsDirectOrder.UseVisualStyleBackColor = false;
		this.rbIsDirectOrder.CheckedChanged += new System.EventHandler(RadioButtons_CheckedChanged);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboClients, "cboClients");
		((TextEditorControlBase)this.cboClients).AlwaysInEditMode = true;
		this.cboClients.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClients).Name = "cboClients";
		((TextEditorControlBase)this.cboClients).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClients).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		resources.ApplyResources(this.lblClients, "lblClients");
		this.lblClients.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClients).Name = "lblClients";
		((ControlBase)this.lblClients).WrapText = false;
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblQuotation, "lblQuotation");
		this.lblQuotation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuotation).Name = "lblQuotation";
		((ControlBase)this.lblQuotation).WrapText = false;
		resources.ApplyResources(this.cboQuotation, "cboQuotation");
		((TextEditorControlBase)this.cboQuotation).AlwaysInEditMode = true;
		this.cboQuotation.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboQuotation).Name = "cboQuotation";
		((TextEditorControlBase)this.cboQuotation).ValueChanged += new System.EventHandler(cboQuotation_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboQuotation).KeyDown += new System.Windows.Forms.KeyEventHandler(cboQuotation_KeyDown);
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		((TextEditorControlBase)this.cboCurrency).ValueChanged += new System.EventHandler(cboCurrency_ValueChanged);
		resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).Name = "txtExchangeRate";
		((System.Windows.Forms.Control)(object)this.txtExchangeRate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		this.lblExchangeRate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		resources.ApplyResources(this.btnClientsSearch, "btnClientsSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnClientsSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnClientsSearch).Name = "btnClientsSearch";
		((System.Windows.Forms.Control)(object)this.btnClientsSearch).Click += new System.EventHandler(btnClientsSearch_Click);
		resources.ApplyResources(this.btnQuotationSearch, "btnQuotationSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnQuotationSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Name = "btnQuotationSearch";
		((System.Windows.Forms.Control)(object)this.btnQuotationSearch).Click += new System.EventHandler(btnQuotationSearch_Click);
		resources.ApplyResources(this.chkBranches, "chkBranches");
		((System.Windows.Forms.Control)(object)this.chkBranches).Name = "chkBranches";
		((UltraToggleEditorBase)this.chkBranches).CheckedChanged += new System.EventHandler(chkBranches_CheckedChanged);
		resources.ApplyResources(this.btnBranchesSearch, "btnBranchesSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnBranchesSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Name = "btnBranchesSearch";
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBranchesSearch).Click += new System.EventHandler(btnBranchesSearch_Click);
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((EditorButtonControlBase)this.cboBranches).ReadOnly = true;
		((TextEditorControlBase)this.cboBranches).ValueChanged += new System.EventHandler(cboBranches_ValueChanged);
		resources.ApplyResources(this.txtRecipient, "txtRecipient");
		((System.Windows.Forms.Control)(object)this.txtRecipient).Name = "txtRecipient";
		resources.ApplyResources(this.lblRecipient, "lblRecipient");
		this.lblRecipient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRecipient).Name = "lblRecipient";
		((ControlBase)this.lblRecipient).WrapText = false;
		resources.ApplyResources(this.lblQuotationValidity, "lblQuotationValidity");
		this.lblQuotationValidity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuotationValidity).Name = "lblQuotationValidity";
		((ControlBase)this.lblQuotationValidity).WrapText = false;
		resources.ApplyResources(this.lblDeliveryTerms, "lblDeliveryTerms");
		this.lblDeliveryTerms.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryTerms).Name = "lblDeliveryTerms";
		((ControlBase)this.lblDeliveryTerms).WrapText = false;
		resources.ApplyResources(this.lblPacking, "lblPacking");
		this.lblPacking.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPacking).Name = "lblPacking";
		((ControlBase)this.lblPacking).WrapText = false;
		resources.ApplyResources(this.lblPaymentTerms, "lblPaymentTerms");
		this.lblPaymentTerms.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaymentTerms).Name = "lblPaymentTerms";
		((ControlBase)this.lblPaymentTerms).WrapText = false;
		resources.ApplyResources(this.lblManufacturerAndOrigin, "lblManufacturerAndOrigin");
		this.lblManufacturerAndOrigin.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManufacturerAndOrigin).Name = "lblManufacturerAndOrigin";
		((ControlBase)this.lblManufacturerAndOrigin).WrapText = false;
		resources.ApplyResources(this.lblDeliveryPeriod, "lblDeliveryPeriod");
		this.lblDeliveryPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryPeriod).Name = "lblDeliveryPeriod";
		((ControlBase)this.lblDeliveryPeriod).WrapText = false;
		resources.ApplyResources(this.txtPacking, "txtPacking");
		((System.Windows.Forms.Control)(object)this.txtPacking).Name = "txtPacking";
		resources.ApplyResources(this.txtPaymentTerms, "txtPaymentTerms");
		((System.Windows.Forms.Control)(object)this.txtPaymentTerms).Name = "txtPaymentTerms";
		resources.ApplyResources(this.txtManufacturerAndOrigin, "txtManufacturerAndOrigin");
		((System.Windows.Forms.Control)(object)this.txtManufacturerAndOrigin).Name = "txtManufacturerAndOrigin";
		resources.ApplyResources(this.txtDeliveryTerms, "txtDeliveryTerms");
		((System.Windows.Forms.Control)(object)this.txtDeliveryTerms).Name = "txtDeliveryTerms";
		resources.ApplyResources(this.txtDeliveryPeriod, "txtDeliveryPeriod");
		((System.Windows.Forms.Control)(object)this.txtDeliveryPeriod).Name = "txtDeliveryPeriod";
		resources.ApplyResources(this.txtQuotationValidity, "txtQuotationValidity");
		((System.Windows.Forms.Control)(object)this.txtQuotationValidity).Name = "txtQuotationValidity";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQuotationValidity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRecipient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRecipient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotationValidity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryTerms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPacking);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentTerms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManufacturerAndOrigin);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPacking);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaymentTerms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtManufacturerAndOrigin);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliveryTerms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliveryPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBranchesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnQuotationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotation);
		base.Name = "frmSLOrders";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClients, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClients, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnQuotationSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBranchesSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliveryPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliveryTerms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtManufacturerAndOrigin, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaymentTerms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPacking, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManufacturerAndOrigin, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentTerms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPacking, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryTerms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotationValidity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRecipient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRecipient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQuotationValidity, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClients).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExchangeRate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRecipient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPacking).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentTerms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtManufacturerAndOrigin).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryTerms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuotationValidity).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
