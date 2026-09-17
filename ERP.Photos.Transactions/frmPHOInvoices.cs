using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Photos;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.SafesAndBanks;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.POS.MasterData;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.Photos.Transactions;

public class frmPHOInvoices : frmHeaderManyDetails
{
	private DataTable dtsafes;

	private DataTable dtAgents;

	private DataTable dtClients;

	private DataTable dtPhotoTypes;

	private DataTable dtTaxs;

	private DataTable dtItemsPackages;

	private DataTable dtItemsCataloge;

	private DataTable dtInvoiceDetailCataloge;

	private DataTable dtInvoicePayments;

	private DataTable dtUsers;

	private DataTable dtPOSDefaultData;

	private DataTable dtReports;

	private DataTable dtServices;

	private DataTable dtInvoiceServices;

	private DataTable dtUnits;

	private ValueList vlItemsPackages = new ValueList();

	private ValueList vlItemsCataloge = new ValueList();

	private ValueList vlServices = new ValueList();

	private ValueList vlUsers = new ValueList();

	private ValueList vlIsIndoor = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlUsers2 = new ValueList();

	private DataSet ds;

	private int newID = -100000;

	private string SelectionIndoorPath = "";

	private string SelectionOutdoorPath = "";

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraButton btnClientSearch;

	private UltraLabel lblDiscBeforeTaxValue;

	private UltraTextEditor txtDiscBeforeTaxValue;

	private UltraLabel lblDiscBeforeTaxRatio;

	private UltraTextEditor txtDiscBeforeTaxRatio;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataPayments;

	public UltraButton btnAgentSearch;

	private UltraLabel lblAgents;

	private UltraComboEditor cboAgent;

	private UltraLabel lblPhotoTypes;

	private UltraComboEditor cboPhotoType;

	private UltraCheckEditor chkIsDeliverd;

	private UltraDateTimeEditor dtpDeliverdDate;

	private UltraLabel lblEventDate;

	private UltraDateTimeEditor dtpEventDate;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	public UltraButton btnClientAdd;

	private UltraLabel lblPaidAmount;

	private UltraTextEditor txtPaidAmount;

	private UltraLabel lblRestAmount;

	private UltraTextEditor txtRestAmount;

	private UltraButton btnInvoicePayments;

	private UltraLabel lblSafe;

	private UltraComboEditor cboSafe;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGDataServices;

	private UltraComboEditor cboMobile;

	private UltraLabel lblMobile;

	public frmPHOInvoices()
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
		InitializeComponent();
		TableName = "PHO_Invoices";
		IDCol = "InvoiceID";
		NoCol = "InvoiceNo";
		DateCol = "InvoiceDate";
	}

	public frmPHOInvoices(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		base.PrepareData();
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int i = 0; i < dtUnits.Rows.Count; i++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtsafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSafe, dtsafes, "SafeID", "SafeName");
		DataRow dataRow = Stages.Select("1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows[0];
		SelectionIndoorPath = dataRow["FolderPath"].ToString();
		SelectionOutdoorPath = dataRow["OutdoorFolderPath"].ToString();
		dtAgents = Agents.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAgent, dtAgents, "AgentID", "AgentName");
		dtClients = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "ClientID", "MobileNumber");
		dtPhotoTypes = PhotoTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPhotoType, dtPhotoTypes, "PhotoTypeID", "PhotoTypeName");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtItemsPackages = ItemsPackages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsPackages.ValueListItems.Clear();
		for (int j = 0; j < dtItemsPackages.Rows.Count; j++)
		{
			vlItemsPackages.ValueListItems.Add(dtItemsPackages.Rows[j]["ItemPackageID"], dtItemsPackages.Rows[j]["ItemPackageName"].ToString());
		}
		vlIsIndoor.ValueListItems.Clear();
		vlIsIndoor.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "داخلى" : "Indoor");
		vlIsIndoor.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "خارجى" : "OutDoor");
		dtItemsCataloge = ItemsCataloge.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsCataloge.ValueListItems.Clear();
		for (int k = 0; k < dtItemsCataloge.Rows.Count; k++)
		{
			vlItemsCataloge.ValueListItems.Add(dtItemsCataloge.Rows[k]["ItemCatalogeID"], dtItemsCataloge.Rows[k]["ItemCatalogeName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int l = 0; l < dtUsers.Rows.Count; l++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[l]["User_ID"], dtUsers.Rows[l]["UserName"].ToString());
			vlUsers2.ValueListItems.Add(dtUsers.Rows[l]["User_ID"], dtUsers.Rows[l]["UserName"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int m = 0; m < dtServices.Rows.Count; m++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[m]["ServiceID"], dtServices.Rows[m]["ServiceName"].ToString());
		}
		dtDetails = InvoicesDetails.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoiceDetailCataloge = InvoicesDetailsCataloge.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoicePayments = InvoicesPayments.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtInvoiceServices = InvoicesServices.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtInvoiceDetailCataloge);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtInvoiceDetailCataloge";
		ds.Relations.Add(ds.Tables[0].Columns["InvoiceDetailID"], ds.Tables[1].Columns["InvoiceDetailID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		((UltraGridBase)ULGDataPayments).DataSource = dtInvoicePayments;
		((UltraGridBase)ULGDataServices).DataSource = dtInvoiceServices;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataPayments);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		GlobalFunctions.PrepareGrid(ULGDataServices);
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageID"].Header).Caption = (GlobalVariables.IsArabic ? "العرض" : "Package");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "مستخدم" : "User");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageID"].ValueList = (IValueList)(object)vlItemsPackages;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CadreCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoPlaceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemCatalogeID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CadreCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد" : "Count");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "النسخ" : "Copy");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["QtyPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsIndoor"].Header).Caption = (GlobalVariables.IsArabic ? "الموقع" : "Site");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoPlace"].Header).Caption = (GlobalVariables.IsArabic ? "مكان" : "Place");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoPlaceDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التصوير" : "Photo Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsDeliverd"].Header).Caption = (GlobalVariables.IsArabic ? "مستلم" : "Deliverd");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DeliverdDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DeliveryPerson"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Person");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DeliveryPhoneNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهاتف" : "Phone No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemCatalogeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CadreCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["QtyPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsIndoor"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoPlace"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoPlaceDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsDeliverd"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DeliverdDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DeliveryPerson"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DeliveryPhoneNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemCatalogeID"].ValueList = (IValueList)(object)vlItemsCataloge;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsIndoor"].ValueList = (IValueList)(object)vlIsIndoor;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CadreCount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["QtyPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsDeliverd"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoPlaceDate"].MaskInput = "dd/mm/yyyy hh:mm tt";
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["InvoicePaymentID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["InvoicePaymentNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["PaymentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["InvoicePaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["PaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "User" : "User");
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["InvoicePaymentNo"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["PaymentDate"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGDataPayments).DisplayLayout.Bands[0].Columns["TotalAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["InvoiceServiceID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Amount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceID"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Amount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((HeaderBase)((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceID"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Amount"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["ServiceID"].ValueList = (IValueList)(object)vlServices;
		((UltraGridBase)ULGDataServices).DisplayLayout.Bands[0].Columns["Amount"].DefaultCellValue = 0;
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
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["InvoiceNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["InvoiceDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboAgent).ValueChanged -= cboAgent_ValueChanged;
			((TextEditorControlBase)cboAgent).Value = drMaster["AgentID"];
			((TextEditorControlBase)cboAgent).ValueChanged += cboAgent_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			((TextEditorControlBase)cboMobile).Value = drMaster["ClientID"];
			((TextEditorControlBase)cboClient).Value = drMaster["ClientID"];
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((TextEditorControlBase)cboPhotoType).Value = drMaster["PhotoTypeID"];
			dtpEventDate.Value = (DateTime)drMaster["EventDate"];
			((UltraToggleEditorBase)chkIsDeliverd).Checked = bool.Parse(drMaster["IsDeliverd"].ToString());
			dtpDeliverdDate.Value = ((drMaster["DeliverdDate"] == DBNull.Value) ? DBNull.Value : drMaster["DeliverdDate"]);
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse(drMaster["DiscountbeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
			((TextEditorControlBase)cboTax).Value = drMaster["TaxID"];
			((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = InvoicesDetails.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceDetailCataloge = InvoicesDetailsCataloge.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoicePayments = InvoicesPayments.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtInvoiceServices = InvoicesServices.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DataTable dataTable = dtInvoicePayments;
			object obj = dtInvoicePayments.Compute(" Sum(TotalAmount) ", "");
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtInvoiceDetailCataloge);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtInvoiceDetailCataloge";
			ds.Relations.Add(ds.Tables[0].Columns["InvoiceDetailID"], ds.Tables[1].Columns["InvoiceDetailID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			((UltraGridBase)ULGDataPayments).DataSource = dtInvoicePayments;
			((UltraGridBase)ULGDataServices).DataSource = dtInvoiceServices;
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
		((EditorButtonControlBase)cboAgent).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPhotoType).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpEventDate).ReadOnly = NavMode;
		((Control)(object)chkIsDeliverd).Enabled = !NavMode;
		((EditorButtonControlBase)dtpDeliverdDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscBeforeTaxRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaidAmount).ReadOnly = !Adding;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((Control)(object)btnClientAdd).Visible = !NavMode;
		((Control)(object)btnAgentSearch).Visible = !NavMode;
		((Control)(object)btnInvoicePayments).Visible = Updating;
		((UltraTabControlBase)UTCDetails).Tabs["Payments"].Visible = !Adding;
		UltraLabel obj = lblSafe;
		bool visible = (((Control)(object)cboSafe).Visible = Adding && dtsafes.Rows.Count > 1);
		((Control)(object)obj).Visible = visible;
		if (Adding)
		{
			DataView dataView = new DataView(dtItemsPackages);
			dataView.RowFilter = " IsActive =1 ";
			vlItemsPackages.ValueListItems.Clear();
			DataTable dataTable = dataView.ToTable();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItemsPackages.ValueListItems.Add(dataTable.Rows[i]["ItemPackageID"], dataTable.Rows[i]["ItemPackageName"].ToString());
			}
		}
		else
		{
			vlItemsPackages.ValueListItems.Clear();
			for (int j = 0; j < dtItemsPackages.Rows.Count; j++)
			{
				vlItemsPackages.ValueListItems.Add(dtItemsPackages.Rows[j]["ItemPackageID"], dtItemsPackages.Rows[j]["ItemPackageName"].ToString());
			}
		}
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataServices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboAgent).ValueChanged -= cboAgent_ValueChanged;
		cboAgent.SelectedIndex = -1;
		((TextEditorControlBase)cboAgent).ValueChanged += cboAgent_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
		((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
		UltraComboEditor obj = cboClient;
		int selectedIndex = (cboMobile.SelectedIndex = -1);
		obj.SelectedIndex = selectedIndex;
		((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		cboPhotoType.SelectedIndex = -1;
		dtpEventDate.Value = DBNull.Value;
		((UltraToggleEditorBase)chkIsDeliverd).Checked = false;
		dtpDeliverdDate.Value = DBNull.Value;
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
		((Control)(object)txtDiscBeforeTaxRatio).Text = "0";
		((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
		((TextEditorControlBase)cboTax).ValueChanged -= cboTax_ValueChanged;
		cboTax.SelectedIndex = -1;
		((TextEditorControlBase)cboTax).ValueChanged += cboTax_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = "0";
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = "0";
		cboSafe.SelectedIndex = ((dtsafes.Rows.Count != 1) ? (-1) : 0);
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataPayments).DataSource).Rows.Clear();
		((DataTable)((UltraGridBase)ULGDataServices).DataSource).Rows.Clear();
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
		if (dtsafes.Rows.Count == 0 && Adding)
		{
			GlobalVariables.InformationMB.Show("برجاء ربط المستخدم على خزينة", "Please Relate Users With Safe");
			return false;
		}
		if (cboSafe.SelectedIndex == -1 && ((Control)(object)txtPaidAmount).Text != "" && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m && Adding)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار خزينة", "Please Select Safe");
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (cboPhotoType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع التصوير" : "Please Select Photo Type");
			((TextEditorControlBase)cboPhotoType).Focus();
			cboPhotoType.DropDown();
			return false;
		}
		if (dtpEventDate.Value == DBNull.Value || dtpEventDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيارتاريخ الحدث " : "Please Select Event Date");
			((Control)(object)dtpEventDate).Focus();
			dtpEventDate.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("PHO_Invoices", "InvoiceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["InvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
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
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemPackageID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم العرض  ", "Please Enter Package Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemPackageID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemPackageID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["User_ID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم المستخدم  ", "Please Enter User Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["User_ID"];
				((UltraGridBase)ULGData).Rows[i].Cells["User_ID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["QtyPrice"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["QtyPrice"].Value.ToString()) <= 0)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إدخال الكمية ", "Please Insert Quantity");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["QtyPrice"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء اختيار موقع التصوير ", "Please Select Photography Site");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"];
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].DroppedDown = true;
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].Value != DBNull.Value && !bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlace"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إدخال مكان التصوير ", "Please Insert Photography Place");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlace"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].Value != DBNull.Value && !bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlaceDate"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ مكان التصوير ", "Please Insert Photography Place Date");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlaceDate"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value.ToString()) < 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال قيمة الصنف  ", "Please Enter Item Amount ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CadreCount"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CadreCount"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال عدد الكادرات  ", "Please Enter Cadre Count ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CadreCount"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString()) <= 0m)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء ادخال عدد النسخ  ", "Please Enter Copy Count ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الخدمة  ", "Please Select Service Name ");
				ULGDataServices.ActiveCell = ((UltraGridBase)ULGDataServices).Rows[k].Cells["ServiceID"];
				((UltraTabControlBase)UTCDetails).Tabs["Services"].Selected = true;
				ULGDataServices.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGDataServices).Rows[k].Cells["Amount"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataServices).Rows[k].Cells["Amount"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال قيمة الخدمة  ", "Please Enter Service Amount ");
				ULGDataServices.ActiveCell = ((UltraGridBase)ULGDataServices).Rows[k].Cells["Amount"];
				((UltraTabControlBase)UTCDetails).Tabs["Services"].Selected = true;
				ULGDataServices.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
			return false;
		}
		if (dtPOSDefaultData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب العميل الافتراضى من اعدادات البيع  ", "Please Select Default Client ID From Sales Settings ");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_096d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0977: Expected O, but got Unknown
		//IL_0a38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Expected O, but got Unknown
		DateTime dateTime = dtpEventDate.DateTime;
		string text = ((Control)(object)txtCode).Text;
		Main.StartBulkTrans(FromServer: false);
		int num;
		try
		{
			num = Invoices.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboAgent.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAgent).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (cboPhotoType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPhotoType).Value.ToString(), (dtpEventDate.Value == DBNull.Value) ? "Null" : dtpEventDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", (dtpDeliverdDate.Value == DBNull.Value || dtpDeliverdDate.Value == null) ? "Null" : dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", "1", ((Control)(object)txtGrossValue).Text, ((Control)(object)txtDiscBeforeTaxValue).Text, ((Control)(object)txtDiscBeforeTaxRatio).Text, (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), ((Control)(object)txtTaxTotalValue).Text, "0", "0", ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text.Replace("'", "''"), GlobalVariables.UserID, "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				int num2 = InvoicesDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ItemPackageID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["User_ID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					InvoicesDetailsCataloge.Insert_Update("-1", num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemCatalogeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CadreCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["QtyPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsIndoor"].Value.ToString()) ? "1" : "0", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlace"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlaceDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoPlaceDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsDeliverd"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["DeliverdDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["DeliverdDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["DeliveryPerson"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["DeliveryPhoneNumber"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			if (((Control)(object)txtPaidAmount).Text != "" && decimal.Parse(((Control)(object)txtPaidAmount).Text) > 0m)
			{
				InvoicesPayments.GenerateJvs("," + InvoicesPayments.Insert_Update("-1", InvoicesPayments.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID), num.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtPaidAmount).Text, ((Control)(object)txtNotes).Text, ((TextEditorControlBase)cboSafe).Value.ToString(), GlobalVariables.UserID, "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID) + ",", GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count > 0)
			{
				ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; k++)
				{
					((UltraGridBase)ULGDataServices).Rows[k].Cells["InvoiceServiceID"].Value = -1;
					((UltraGridBase)ULGDataServices).Rows[k].Cells["InvoiceID"].Value = num;
					((UltraGridBase)ULGDataServices).Rows[k].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
				InvoicesServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataServices).DataSource, GlobalVariables.UserID);
			}
			Invoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		DataView dataView = new DataView(dtInvoiceDetailCataloge);
		dataView.RowFilter = " IsIndoor=1 ";
		if (dataView.Count > 0)
		{
			string text2 = dateTime.Year.ToString();
			string text3 = dateTime.Month.ToString();
			string text4 = dateTime.Day.ToString();
			string path = SelectionIndoorPath + text2 + "\\" + text3 + "." + text2 + "\\" + text4 + "." + text3 + "." + text2 + "\\" + text;
			try
			{
				Directory.CreateDirectory(path);
			}
			catch
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "خطأ  فى مسار الارشيف  " : "Error Occured Archive Path");
			}
		}
		dataView.RowFilter = " IsIndoor=0 ";
		if (dataView.Count > 0)
		{
			string text5 = dateTime.Year.ToString();
			string text6 = dateTime.Month.ToString();
			string text7 = dateTime.Day.ToString();
			string path2 = SelectionOutdoorPath + text5 + "\\" + text6 + "." + text5 + "\\" + text7 + "." + text6 + "." + text5 + "\\" + text;
			try
			{
				Directory.CreateDirectory(path2);
			}
			catch
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "خطأ  فى مسار الارشيف  " : "Error Occured Archive Path");
			}
		}
		if (!DataSaved)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد طباعة الفاتورة ؟", "Are You Sure You want to Print This Invoice?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PHO_Invoices_A.rpt" : "Rep_PHO_Invoices_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@InvoiceIDs", "," + num + ",");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.SetParameterValue("@InvoiceIDs", "," + num + ",", "Rep_PHO_InvoicesDetails");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_PHO_InvoicesDetails");
			reportDocument.SetParameterValue("@InvoiceIDs", "," + num + ",", "Rep_PHO_InvoicesServices");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_PHO_InvoicesServices");
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
		//IL_0c34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3e: Expected O, but got Unknown
		//IL_0d0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d15: Expected O, but got Unknown
		DateTime dateTime = dtpEventDate.DateTime;
		string text = ((Control)(object)txtCode).Text;
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Invoices.Insert_Update(drMaster["InvoiceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboAgent.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAgent).Value.ToString(), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), (cboPhotoType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPhotoType).Value.ToString(), (dtpEventDate.Value == DBNull.Value) ? "Null" : dtpEventDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsDeliverd).Checked ? "1" : "0", (dtpDeliverdDate.Value == DBNull.Value || dtpDeliverdDate.Value == null) ? "Null" : dtpDeliverdDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", "1", ((Control)(object)txtGrossValue).Text, ((Control)(object)txtDiscBeforeTaxValue).Text, ((Control)(object)txtDiscBeforeTaxRatio).Text, (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), ((Control)(object)txtTaxTotalValue).Text, "0", "0", ((Control)(object)txtNetprice).Text, ((Control)(object)txtNotes).Text.Replace("'", "''"), (drMaster["User_ID"] == DBNull.Value) ? "Null" : drMaster["User_ID"].ToString(), bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", (drMaster["JVID"] == DBNull.Value) ? "Null" : drMaster["JVID"].ToString(), bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text2 = ",";
			string text3 = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text2 = text2 + ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailID"].Value.ToString() + ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					text3 = text3 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["InvoiceDetailCatalogeID"].Value.ToString() + ",";
				}
			}
			Main.DeleteForUpdate("PHO_InvoicesDetailsCatalogeStages", "InvoiceID", drMaster["InvoiceID"].ToString(), "InvoiceDetailCatalogeID", text3);
			Main.DeleteForUpdate("PHO_InvoicesDetailsCataloge", "InvoiceID", drMaster["InvoiceID"].ToString(), "InvoiceDetailCatalogeID", text3);
			Main.DeleteForUpdate("PHO_InvoicesDetails", "InvoiceID", drMaster["InvoiceID"].ToString(), "InvoiceDetailID", text2);
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				int num2 = InvoicesDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["InvoiceDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["InvoiceDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].Cells["InvoiceDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["ItemPackageID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].Cells["User_ID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
				{
					InvoicesDetailsCataloge.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["InvoiceDetailCatalogeID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["InvoiceDetailCatalogeID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["InvoiceDetailCatalogeID"].Value.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["ItemCatalogeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["CadreCount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["UnitID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Qty"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["QtyPrice"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["TotalPrice"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["IsIndoor"].Value.ToString()) ? "1" : "0", ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PhotoPlace"].Value.ToString(), (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PhotoPlaceDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["PhotoPlaceDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), bool.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["IsDeliverd"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DeliverdDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DeliverdDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DeliveryPerson"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["DeliveryPhoneNumber"].Value.ToString(), ((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count > 0)
			{
				string text4 = ",";
				ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; m++)
				{
					((UltraGridBase)ULGDataServices).Rows[m].Cells["InvoiceID"].Value = num;
					((UltraGridBase)ULGDataServices).Rows[m].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text4 = text4 + ((UltraGridBase)ULGDataServices).Rows[m].Cells["InvoiceServiceID"].Value.ToString() + ",";
				}
				ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
				Main.DeleteForUpdate("PHO_InvoicesServices", "InvoiceID", drMaster["InvoiceID"].ToString(), "InvoiceServiceID", text4);
				InvoicesServices.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataServices).DataSource, GlobalVariables.UserID);
			}
			Invoices.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		DataView dataView = new DataView(dtInvoiceDetailCataloge);
		dataView.RowFilter = " IsIndoor=1 ";
		if (dataView.Count > 0)
		{
			string text5 = dateTime.Year.ToString();
			string text6 = dateTime.Month.ToString();
			string text7 = dateTime.Day.ToString();
			string path = SelectionIndoorPath + text5 + "\\" + text6 + "." + text5 + "\\" + text7 + "." + text6 + "." + text5 + "\\" + text;
			try
			{
				Directory.CreateDirectory(path);
			}
			catch
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "خطأ  فى مسار الارشيف  " : "Error Occured Archive Path");
			}
		}
		dataView.RowFilter = " IsIndoor=0 ";
		if (dataView.Count > 0)
		{
			string text8 = dateTime.Year.ToString();
			string text9 = dateTime.Month.ToString();
			string text10 = dateTime.Day.ToString();
			string path2 = SelectionOutdoorPath + text8 + "\\" + text9 + "." + text8 + "\\" + text10 + "." + text9 + "." + text8 + "\\" + text;
			try
			{
				Directory.CreateDirectory(path2);
			}
			catch
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "خطأ  فى مسار الارشيف  " : "Error Occured Archive Path");
			}
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
		GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
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

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Invoices.DeleteVirtual(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			InvoicesDetails.DeleteVirtualByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			InvoicesDetailsCataloge.DeleteVirtualByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
			InvoicesServices.DeleteVirtualByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.UserID);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_PHO_Invoices_A.rpt" : "Rep_PHO_Invoices_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@InvoiceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@InvoiceIDs", "," + RowID + ",", "Rep_PHO_InvoicesDetails");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_PHO_InvoicesDetails");
			GlobalVariables.ReportDocument.SetParameterValue("@InvoiceIDs", "," + RowID + ",", "Rep_PHO_InvoicesServices");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_PHO_InvoicesServices");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.PHOInvoicesReport(-1, 0, -1, GlobalVariables.BranchIDs);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["InvoiceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int i = 0; i < dtUnits.Rows.Count; i++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[i]["UnitID"], dtUnits.Rows[i]["UnitName"].ToString());
		}
		dtAgents = Agents.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAgent, dtAgents, "AgentID", "AgentName");
		dtClients = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "ClientID", "MobileNumber");
		dtPhotoTypes = PhotoTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPhotoType, dtPhotoTypes, "PhotoTypeID", "PhotoTypeName");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		dtItemsCataloge = ItemsCataloge.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsCataloge.ValueListItems.Clear();
		for (int j = 0; j < dtItemsCataloge.Rows.Count; j++)
		{
			vlItemsCataloge.ValueListItems.Add(dtItemsCataloge.Rows[j]["ItemCatalogeID"], dtItemsCataloge.Rows[j]["ItemCatalogeName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		for (int k = 0; k < dtUsers.Rows.Count; k++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[k]["User_ID"], dtUsers.Rows[k]["UserName"].ToString());
		}
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlServices.ValueListItems.Clear();
		for (int l = 0; l < dtServices.Rows.Count; l++)
		{
			vlServices.ValueListItems.Add(dtServices.Rows[l]["ServiceID"], dtServices.Rows[l]["ServiceName"].ToString());
		}
		dtItemsPackages = ItemsPackages.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsPackages.ValueListItems.Clear();
		for (int m = 0; m < dtItemsPackages.Rows.Count; m++)
		{
			vlItemsPackages.ValueListItems.Add(dtItemsPackages.Rows[m]["ItemPackageID"], dtItemsPackages.Rows[m]["ItemPackageName"].ToString());
		}
		if (Adding)
		{
			DataView dataView = new DataView(dtItemsPackages);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlItemsPackages.ValueListItems.Clear();
			for (int n = 0; n < dataTable.Rows.Count; n++)
			{
				vlItemsPackages.ValueListItems.Add(dataTable.Rows[n]["ItemPackageID"], dataTable.Rows[n]["ItemPackageName"].ToString());
			}
		}
		else
		{
			vlItemsPackages.ValueListItems.Clear();
			for (int num = 0; num < dtItemsPackages.Rows.Count; num++)
			{
				vlItemsPackages.ValueListItems.Add(dtItemsPackages.Rows[num]["ItemPackageID"], dtItemsPackages.Rows[num]["ItemPackageName"].ToString());
			}
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["InvoiceDetailID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["InvoiceDetailCatalogeID"].Value = ++newID;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((Control)(object)ULGData).Enter += ULGData_Enter;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemPackageID" && ULGData.ActiveCell.Value != DBNull.Value)
		{
			GetItemPackageDetails(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalPackagePrice();
			CalculateGoss();
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "QtyPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemPackageID"].Value != DBNull.Value)
			{
				GetItemPackageDetails(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalPackagePrice();
				CalculateGoss();
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "QtyPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "QtyPrice")
			{
				CalculateTotalPackagePrice();
				CalculateGoss();
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemPackageID" && ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value != DBNull.Value && HasCatalogePhotos(((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value.ToString()))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			return;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "QtyPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value != DBNull.Value && HasCatalogePhotos(((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value.ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemCatalogeID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CadreCount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID")
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
		if ((Adding || Updating) && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemPackageID" && ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value != DBNull.Value && !HasCatalogePhotos(((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value.ToString()))
		{
			int num = SearchFunctions.ItemsPackagesSearchPhotos(IsFromServer: false);
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemPackageID"].Value = num;
			}
		}
		e.Handled = true;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "QtyPrice")
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateTotalPackagePrice();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		CalculateGoss();
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value != DBNull.Value && HasCatalogePhotos(((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا العرض لوجود مراحل على هذا العرض" : "Cannot Delete This Package Because Thnere Are Stages");
				e.DisplayPromptMsg = false;
				((CancelEventArgs)(object)e).Cancel = true;
			}
			else
			{
				base.ULGData_BeforeRowsDeleted(sender, e);
			}
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["InvoiceDetailID"].Value != DBNull.Value && HasCatalogePhotos(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["InvoiceDetailID"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا الصنف لوجود مراحل على هذا العرض" : "Cannot Delete This Item Because Thnere Are Stages");
				e.DisplayPromptMsg = false;
				((CancelEventArgs)(object)e).Cancel = true;
			}
			else
			{
				base.ULGData_BeforeRowsDeleted(sender, e);
			}
		}
	}

	public void GetItemPackageDetails(UltraGridRow ParentRow)
	{
		DataTable dataTable = new DataTable();
		dataTable = InvoicesDetailsCataloge.GetItemsPackagesDetails_ByItemPackageID(ParentRow.Cells["ItemPackageID"].Value.ToString());
		ds.AcceptChanges();
		for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
		{
			if (ds.Tables[1].Rows[i]["InvoiceDetailID"].ToString() == ParentRow.Cells["InvoiceDetailID"].Value.ToString())
			{
				ds.Tables[1].Rows[i].Delete();
				i--;
				ds.AcceptChanges();
			}
		}
		ds.AcceptChanges();
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			DataRow dataRow = ds.Tables[1].NewRow();
			dataRow["InvoiceDetailCatalogeID"] = ++newID;
			dataRow["InvoiceDetailID"] = ParentRow.Cells["InvoiceDetailID"].Value.ToString();
			dataRow["ItemCatalogeID"] = dataTable.Rows[j]["ItemCatalogeID"];
			dataRow["Qty"] = dataTable.Rows[j]["Qty"];
			dataRow["UnitID"] = dataTable.Rows[j]["UnitID"];
			dataRow["CadreCount"] = dataTable.Rows[j]["CadreCount"];
			dataRow["QtyPrice"] = 1;
			decimal num = ((cboAgent.SelectedIndex == -1) ? 0m : (decimal.Parse(dtAgents.Select(" AgentID= " + ((TextEditorControlBase)cboAgent).Value.ToString())[0]["AgentPercentage"].ToString()) / 100m));
			dataRow["TotalPrice"] = decimal.Parse(dataTable.Rows[j]["TotalPrice"].ToString()) + decimal.Parse(dataTable.Rows[j]["TotalPrice"].ToString()) * num;
			dataRow["IsDeliverd"] = false;
			ds.Tables[1].Rows.Add(dataRow);
		}
		ds.AcceptChanges();
		((Control)(object)ULGData).Enter += ULGData_Enter;
	}

	public void CalculateTotalPackagePrice()
	{
		DataTable dataTable = ((DataSet)((UltraGridBase)ULGData).DataSource).Tables["dtInvoiceDetailCataloge"].Copy();
		dataTable.Columns.Contains("Total");
		DataColumn dataColumn = new DataColumn();
		dataColumn.DataType = typeof(int);
		dataColumn.Expression = "TotalPrice*QtyPrice";
		dataColumn.ColumnName = "Total";
		dataTable.Columns.Add(dataColumn);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			object obj = dataTable.Compute(" Sum(Total) ", "InvoiceDetailID =" + ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailID"].Value.ToString());
			((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = ((obj == DBNull.Value) ? ((object)0) : obj);
		}
	}

	public bool HasCatalogePhotos(string InvoiceDetailID)
	{
		bool flag = false;
		return InvoicesDetailsCatalogePhotos.SelectByInvoiceDetailID(InvoiceDetailID, "1").Rows.Count > 0;
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse((num + CalculateInvoiceServices()).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	public decimal CalculateInvoiceServices()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataServices).Rows).Count; i++)
		{
			result += decimal.Parse(((UltraGridBase)ULGDataServices).Rows[i].Cells["Amount"].Value.ToString());
		}
		return result;
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Invoices.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.POSClientsSearch("-1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void cboMobile_ValueChanged(object sender, EventArgs e)
	{
		if (cboMobile.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = ((TextEditorControlBase)cboMobile).Value;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			cboClient.SelectedIndex = -1;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			((TextEditorControlBase)cboMobile).Value = ((TextEditorControlBase)cboClient).Value;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		}
		else
		{
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			cboMobile.SelectedIndex = -1;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.POSClientsSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void cboTax_ValueChanged(object sender, EventArgs e)
	{
		CalculateGoss();
	}

	private void txtDiscBeforeTaxValue_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged -= txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtDiscBeforeTaxRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxValue).Text == "" || ((Control)(object)txtDiscBeforeTaxValue).Text == "." || ((Control)(object)txtDiscBeforeTaxValue).Text == "0") ? "0" : ((Control)(object)txtDiscBeforeTaxValue).Text) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxRatio).ValueChanged += txtDiscBeforeTaxRatio_ValueChanged;
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse((cboTax.SelectedIndex > -1) ? ((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text)) * (decimal.Parse(dtTaxs.Select(" TaxID= " + ((TextEditorControlBase)cboTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m)).ToString() : "0").ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtGrossValue).Text) - decimal.Parse(((Control)(object)txtDiscBeforeTaxValue).Text) + decimal.Parse(((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	private void txtDiscBeforeTaxRatio_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged -= txtDiscBeforeTaxValue_ValueChanged;
			((Control)(object)txtDiscBeforeTaxValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscBeforeTaxRatio).Text == "" || ((Control)(object)txtDiscBeforeTaxRatio).Text == ".") ? "0" : ((Control)(object)txtDiscBeforeTaxRatio).Text) / 100m * decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscBeforeTaxValue).ValueChanged += txtDiscBeforeTaxValue_ValueChanged;
			CalculateGoss();
		}
	}

	private void cboAgent_ValueChanged(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		DataTable dataTable = new DataTable();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			dataTable = InvoicesDetailsCataloge.GetItemsPackagesDetails_ByItemPackageID(((UltraGridBase)ULGData).Rows[i].Cells["ItemPackageID"].Value.ToString());
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				decimal num = decimal.Parse(dataTable.Select("ItemCatalogeID=  " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemCatalogeID"].Value.ToString())[0]["TotalPrice"].ToString());
				decimal num2 = ((cboAgent.SelectedIndex == -1) ? 0m : (decimal.Parse(dtAgents.Select(" AgentID= " + ((TextEditorControlBase)cboAgent).Value.ToString())[0]["AgentPercentage"].ToString()) / 100m));
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TotalPrice"].Value = num + num * num2;
			}
		}
		CalculateTotalPackagePrice();
		CalculateGoss();
	}

	private void cboAgent_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.AgentsSearch(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboAgent).Value = num;
			}
		}
	}

	private void btnAgentSearch_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.AgentsSearch(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboAgent).Value = num;
			}
		}
	}

	private void btnClientAdd_Click(object sender, EventArgs e)
	{
		frmPOSClients frmPOSClients2 = new frmPOSClients((cboClient.SelectedIndex == -1) ? (-1) : int.Parse(((TextEditorControlBase)cboClient).Value.ToString()));
		frmPOSClients2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmPOSClients2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Clients");
		frmPOSClients2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.POS.MasterData.frmPOSClients'")[0];
		frmPOSClients2.ShowDialog();
		dtClients = Clients.FillCombo("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		GlobalFunctions.FillCombo(cboMobile, dtClients, "ClientID", "MobileNumber");
		if (frmPOSClients2.ClientID != 0m)
		{
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboMobile).ValueChanged -= cboMobile_ValueChanged;
			UltraComboEditor obj = cboClient;
			object value = (((TextEditorControlBase)cboMobile).Value = frmPOSClients2.ClientID);
			((TextEditorControlBase)obj).Value = value;
			((TextEditorControlBase)cboMobile).ValueChanged += cboMobile_ValueChanged;
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtPaidAmount_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
		((Control)(object)txtPaidAmount).Text = decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
		((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void btnInvoicePayments_Click(object sender, EventArgs e)
	{
		if (Updating)
		{
			frmPHOInvoicesPayments frmPHOInvoicesPayments2 = new frmPHOInvoicesPayments(int.Parse(drMaster["InvoiceID"].ToString()));
			frmPHOInvoicesPayments2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmPHOInvoicesPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد الفواتير" : "Invoices Payments");
			frmPHOInvoicesPayments2.ShowDialog();
			dtInvoicePayments = InvoicesPayments.SelectByInvoiceID(drMaster["InvoiceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DataTable dataTable = dtInvoicePayments;
			object obj = dtInvoicePayments.Compute(" Sum(TotalAmount) ", "");
			((TextEditorControlBase)txtPaidAmount).ValueChanged -= txtPaidAmount_ValueChanged;
			((Control)(object)txtPaidAmount).Text = decimal.Parse((obj == DBNull.Value) ? "0" : obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRestAmount).Text = decimal.Parse((decimal.Parse((((Control)(object)txtNetprice).Text == "" || ((Control)(object)txtNetprice).Text == ".") ? "0" : ((Control)(object)txtNetprice).Text) - decimal.Parse((((Control)(object)txtPaidAmount).Text == "" || ((Control)(object)txtPaidAmount).Text == ".") ? "0" : ((Control)(object)txtPaidAmount).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtPaidAmount).ValueChanged += txtPaidAmount_ValueChanged;
			((UltraGridBase)ULGDataPayments).DataSource = dtInvoicePayments;
			InitGrid();
		}
	}

	private void ULGDataPayments_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataPayments).ActiveRow).Selected = true;
	}

	private void ULGDataServices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Amount")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGDataServices_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Amount")
		{
			CalculateGoss();
		}
		ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
	}

	private void ULGDataServices_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGoss();
	}

	private void ULGDataServices_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataServices_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		ULGDataServices.CellListSelect -= new CellEventHandler(ULGDataServices_CellListSelect);
		ULGDataServices.AfterCellUpdate -= new CellEventHandler(ULGDataServices_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ServiceID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataServices).UpdateData();
			((UltraGridBase)ULGDataServices).ActiveRow.Cells["Amount"].Value = dtServices.Select(" ServiceID= " + e.Cell.Value.ToString())[0]["Price"];
			((UltraGridBase)ULGDataServices).ActiveRow.Cells["Notes"].Value = dtServices.Select(" ServiceID= " + e.Cell.Value.ToString())[0]["Notes"];
			CalculateGoss();
		}
		ULGDataServices.CellListSelect += new CellEventHandler(ULGDataServices_CellListSelect);
		ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
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
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Expected O, but got Unknown
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_083c: Expected O, but got Unknown
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Expected O, but got Unknown
		//IL_0e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3b: Expected O, but got Unknown
		//IL_0e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6b: Expected O, but got Unknown
		//IL_0e79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e83: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Photos.Transactions.frmPHOInvoices));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
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
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDataServices = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataPayments = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.btnClientSearch = new UltraButton();
		this.lblDiscBeforeTaxValue = new UltraLabel();
		this.txtDiscBeforeTaxValue = new UltraTextEditor();
		this.lblDiscBeforeTaxRatio = new UltraLabel();
		this.txtDiscBeforeTaxRatio = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.btnAgentSearch = new UltraButton();
		this.lblAgents = new UltraLabel();
		this.cboAgent = new UltraComboEditor();
		this.lblPhotoTypes = new UltraLabel();
		this.cboPhotoType = new UltraComboEditor();
		this.chkIsDeliverd = new UltraCheckEditor();
		this.dtpDeliverdDate = new UltraDateTimeEditor();
		this.lblEventDate = new UltraLabel();
		this.dtpEventDate = new UltraDateTimeEditor();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.btnClientAdd = new UltraButton();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.btnInvoicePayments = new UltraButton();
		this.lblSafe = new UltraLabel();
		this.cboSafe = new UltraComboEditor();
		this.cboMobile = new UltraComboEditor();
		this.lblMobile = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataServices).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPhotoType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEventDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Services";
		val.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val, "ultraTab2");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Payments";
		val2.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val2, "ultraTab1");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val3, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val4, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance13");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance14");
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance15");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance16");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val9, "appearance17");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance18");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
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
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataServices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGDataServices, "ULGDataServices");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance6");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val12, "appearance7");
		((AppearanceBase)val12).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val13, "appearance8");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val14, "appearance9");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance10");
		((UltraGridBase)this.ULGDataServices).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataServices).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataServices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataServices).Name = "ULGDataServices";
		((UltraControlBase)this.ULGDataServices).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataServices.AfterCellUpdate += new CellEventHandler(ULGDataServices_AfterCellUpdate);
		this.ULGDataServices.AfterRowsDeleted += new System.EventHandler(ULGDataServices_AfterRowsDeleted);
		this.ULGDataServices.CellListSelect += new CellEventHandler(ULGDataServices_CellListSelect);
		this.ULGDataServices.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataServices_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataServices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataServices_KeyPress);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataPayments);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataPayments, "ULGDataPayments");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance1");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val17, "appearance2");
		((AppearanceBase)val17).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val18, "appearance3");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val19, "appearance4");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val20).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val20, "appearance5");
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataPayments).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataPayments).Name = "ULGDataPayments";
		((UltraControlBase)this.ULGDataPayments).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataPayments.AfterEnterEditMode += new System.EventHandler(ULGDataPayments_AfterEnterEditMode);
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
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((TextEditorControlBase)this.cboClient).ValueChanged += new System.EventHandler(cboClient_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val21, "appearance19");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.lblDiscBeforeTaxValue, "lblDiscBeforeTaxValue");
		this.lblDiscBeforeTaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue).Name = "lblDiscBeforeTaxValue";
		((ControlBase)this.lblDiscBeforeTaxValue).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxValue, "txtDiscBeforeTaxValue");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).Name = "txtDiscBeforeTaxValue";
		((TextEditorControlBase)this.txtDiscBeforeTaxValue).ValueChanged += new System.EventHandler(txtDiscBeforeTaxValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblDiscBeforeTaxRatio, "lblDiscBeforeTaxRatio");
		this.lblDiscBeforeTaxRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio).Name = "lblDiscBeforeTaxRatio";
		((ControlBase)this.lblDiscBeforeTaxRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscBeforeTaxRatio, "txtDiscBeforeTaxRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).Name = "txtDiscBeforeTaxRatio";
		((TextEditorControlBase)this.txtDiscBeforeTaxRatio).ValueChanged += new System.EventHandler(txtDiscBeforeTaxRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.btnAgentSearch, "btnAgentSearch");
		((AppearanceBase)val22).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val22, "appearance20");
		((ControlBase)this.btnAgentSearch).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.btnAgentSearch).Name = "btnAgentSearch";
		((System.Windows.Forms.Control)(object)this.btnAgentSearch).Click += new System.EventHandler(btnAgentSearch_Click);
		resources.ApplyResources(this.lblAgents, "lblAgents");
		this.lblAgents.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgents).Name = "lblAgents";
		((ControlBase)this.lblAgents).WrapText = false;
		resources.ApplyResources(this.cboAgent, "cboAgent");
		((TextEditorControlBase)this.cboAgent).AlwaysInEditMode = true;
		this.cboAgent.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAgent).Name = "cboAgent";
		((TextEditorControlBase)this.cboAgent).ValueChanged += new System.EventHandler(cboAgent_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboAgent).KeyDown += new System.Windows.Forms.KeyEventHandler(cboAgent_KeyDown);
		resources.ApplyResources(this.lblPhotoTypes, "lblPhotoTypes");
		this.lblPhotoTypes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPhotoTypes).Name = "lblPhotoTypes";
		((ControlBase)this.lblPhotoTypes).WrapText = false;
		resources.ApplyResources(this.cboPhotoType, "cboPhotoType");
		((TextEditorControlBase)this.cboPhotoType).AlwaysInEditMode = true;
		this.cboPhotoType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPhotoType).Name = "cboPhotoType";
		resources.ApplyResources(this.chkIsDeliverd, "chkIsDeliverd");
		((System.Windows.Forms.Control)(object)this.chkIsDeliverd).Name = "chkIsDeliverd";
		resources.ApplyResources(this.dtpDeliverdDate, "dtpDeliverdDate");
		((UltraWinEditorMaskedControlBase)this.dtpDeliverdDate).AlwaysInEditMode = true;
		this.dtpDeliverdDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpDeliverdDate).Name = "dtpDeliverdDate";
		resources.ApplyResources(this.lblEventDate, "lblEventDate");
		this.lblEventDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEventDate).Name = "lblEventDate";
		((ControlBase)this.lblEventDate).WrapText = false;
		resources.ApplyResources(this.dtpEventDate, "dtpEventDate");
		((UltraWinEditorMaskedControlBase)this.dtpEventDate).AlwaysInEditMode = true;
		this.dtpEventDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpEventDate).Name = "dtpEventDate";
		resources.ApplyResources(this.lblTax, "lblTax");
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		((TextEditorControlBase)this.cboTax).ValueChanged += new System.EventHandler(cboTax_ValueChanged);
		resources.ApplyResources(this.btnClientAdd, "btnClientAdd");
		((AppearanceBase)val23).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val23, "appearance21");
		((ControlBase)this.btnClientAdd).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Name = "btnClientAdd";
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Click += new System.EventHandler(btnClientAdd_Click);
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((TextEditorControlBase)this.txtPaidAmount).ValueChanged += new System.EventHandler(txtPaidAmount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		resources.ApplyResources(this.btnInvoicePayments, "btnInvoicePayments");
		((System.Windows.Forms.Control)(object)this.btnInvoicePayments).Name = "btnInvoicePayments";
		((System.Windows.Forms.Control)(object)this.btnInvoicePayments).Click += new System.EventHandler(btnInvoicePayments_Click);
		resources.ApplyResources(this.lblSafe, "lblSafe");
		this.lblSafe.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafe).Name = "lblSafe";
		((ControlBase)this.lblSafe).WrapText = false;
		resources.ApplyResources(this.cboSafe, "cboSafe");
		((TextEditorControlBase)this.cboSafe).AlwaysInEditMode = true;
		this.cboSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSafe).Name = "cboSafe";
		resources.ApplyResources(this.cboMobile, "cboMobile");
		((TextEditorControlBase)this.cboMobile).AlwaysInEditMode = true;
		this.cboMobile.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboMobile).Name = "cboMobile";
		((TextEditorControlBase)this.cboMobile).ValueChanged += new System.EventHandler(cboMobile_ValueChanged);
		resources.ApplyResources(this.lblMobile, "lblMobile");
		this.lblMobile.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobile).Name = "lblMobile";
		((ControlBase)this.lblMobile).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnInvoicePayments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEventDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEventDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliverdDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDeliverd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPhotoTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPhotoType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAgentSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgents);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAgent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmPHOInvoices";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscBeforeTaxRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAgent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgents, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAgentSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPhotoType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPhotoTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDeliverd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliverdDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEventDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEventDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnInvoicePayments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMobile, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataServices).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataPayments).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscBeforeTaxRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPhotoType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDeliverd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliverdDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEventDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMobile).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
