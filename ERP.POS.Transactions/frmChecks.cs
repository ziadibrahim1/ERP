using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using ERP.Company;
using ERP.POS.MasterData;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.POS.Transactions;

public class frmChecks : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtItemsAndGroups;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtItemsAndGroupsCopy;

	private DataTable dtClients;

	private DataTable dtUnits;

	private DataTable dtTaxs;

	private DataTable dtcheckTable;

	private DataTable dtRoomData;

	private DataTable dtRoomSettingsData;

	private DataTable dtMergedCheckData;

	private DataTable dtCaptainOrder;

	private DataTable dtChecksDetailsAccessories;

	private DataTable dtChecksDetailsAddtionals;

	private DataTable dtPOSDefaultData;

	private DataTable dtCurrency;

	private bool CheckSentItems;

	private bool SecondPrint = false;

	private ArrayList ArOfferIDs = new ArrayList();

	public DataTable dtOffersItems = new DataTable();

	public DataTable dtOffersGifts = new DataTable();

	public int OfferID = 0;

	public decimal DiscountRatio = default(decimal);

	public bool ForAll = false;

	public bool LowestPrice = false;

	public bool HighestPrice = false;

	private DataTable dtDeletedItems = new DataTable();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private int TableID;

	private int RoomID;

	private int CheckID = 0;

	private string TableCode;

	private DataView dvItems;

	private bool IsCash = false;

	private decimal CashAmount = default(decimal);

	private bool IsVisa = false;

	private bool CheckClosed = false;

	private bool AdditionalDiscountWithoutTax = false;

	private bool AdditionalDiscountIncludeTax = false;

	private bool ShowFirstDiscount = true;

	private int VisatypeID = 0;

	private string VisaNo = "0";

	private decimal VisaAmount = default(decimal);

	private bool IsOnAccount = false;

	private decimal OnAccountAmount = default(decimal);

	private decimal PaidAmount = default(decimal);

	private decimal RestAmount = default(decimal);

	private int DiscountUserID = 0;

	private decimal UserSalesDiscount = default(decimal);

	private bool CanAddNewClient = false;

	public string CheckLog = "";

	private string MergedCheckIDs = ",";

	private int newID = -100000;

	private IContainer components = null;

	public UltraButton btnClientSearch;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblClient;

	private UltraLabel lblBarCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboClient;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraGroupBox UGBItemData;

	private UltraTextEditor txtPersonCount;

	private UltraLabel lblPersonCount;

	public UltraButton btnPersonCount;

	private UltraLabel lblTableNo;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblServiceChargeValue;

	private UltraTextEditor txtServiceChargeValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraButton btnPlus;

	private UltraButton btnSubtract;

	private UltraPanel pnlItems;

	private UltraLabel lblDiscountValue;

	private UltraTextEditor txtDiscountValue;

	private UltraButton btnCloseCheck;

	public UltraButton btnMerge;

	public UltraButton btnSeparate;

	public UltraButton btnSent;

	private UltraButton btnClear;

	private UltraLabel lblDiscountRatio;

	private UltraTextEditor txtDiscountRatio;

	public UltraButton btnDiscountRatio;

	public UltraButton btnDiscountValue;

	private UltraCheckEditor chkMinCharge;

	private UltraTextEditor txtMinChargeValue;

	public UltraButton btnTransfer;

	private UltraLabel lblCaptainOrder;

	private UltraComboEditor cboCaptainOrder;

	private UltraTabControl UTCItemsGroups;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTextEditor txtInvoiceNo;

	private UltraCheckEditor chkIsInvoice;

	private UltraLabel lblRoundingValue;

	private UltraTextEditor txtRoundingValue;

	private UltraButton btnAccessories;

	private UltraButton btnAddtionals;

	private UltraTextEditor txtTableNo;

	private UltraTextEditor txtDiscountValue2;

	private UltraLabel lblDiscountValue2;

	public UltraButton btnDiscountValue2;

	private UltraTextEditor txtDiscountRatio2;

	private UltraLabel lblDiscountRatio2;

	public UltraButton btnDiscountRatio2;

	private UltraCheckEditor chkMaxCharge;

	private UltraTextEditor txtMaxChargeValue;

	private UltraTextEditor txtClientBarcode;

	private UltraLabel lblClientBarcode;

	public UltraButton btnClientAdd;

	public frmChecks()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		InitializeComponent();
		TableName = "POS_Checks";
		IDCol = "CheckID";
		NoCol = "CheckNo";
		DateCol = "CheckDate";
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "الشيكات" : "Checks");
	}

	public frmChecks(int ID, int TabID, int Room, string TabNo)
		: this()
	{
		CheckID = ID;
		TableID = TabID;
		RoomID = Room;
		TableCode = TabNo;
	}

	public frmChecks(int Room)
		: this()
	{
		RoomID = Room;
	}

	public override void PrepareData()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		dtDeletedItems.Columns.Add("CheckDetailID");
		dtDeletedItems.Columns.Add("ItemID");
		dtDeletedItems.Columns.Add("SentQty");
		dtDeletedItems.Columns.Add("Notes");
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.POS.MasterData.frmPOSClientsWithDetails'").Length != 0)
		{
			CanAddNewClient = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.POS.MasterData.frmPOSClientsWithDetails'")[0]["FormID"].ToString(), "Adding");
		}
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
		dtReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmDineIn", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		dtRoomSettingsData = RoomsSettings.SelectByRoomID(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		AdditionalDiscountWithoutTax = bool.Parse(dtRoomSettingsData.Rows[0]["AdditionalDiscountWithoutTax"].ToString());
		AdditionalDiscountIncludeTax = bool.Parse(dtRoomSettingsData.Rows[0]["AdditionalDiscountIncludeTax"].ToString());
		ShowFirstDiscount = bool.Parse(dtRoomSettingsData.Rows[0]["ShowFirstDiscount"].ToString());
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtCaptainOrder = CaptainOrder.FillCombo("-1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCaptainOrder, dtCaptainOrder, "CaptainOrderID", "CaptainOrderName");
		dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtRoomData.Rows[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
		dtItemsAndGroupsCopy = dtItemsAndGroups.Copy();
		FillItemsGroups();
		dvItems = new DataView(dtItemsAndGroups);
		dvItems.RowFilter = " IsMain=0 ";
		dvItems.ToTable();
		for (int k = 0; k < dvItems.Count; k++)
		{
			vlItems.ValueListItems.Add(dvItems[k]["ItemID"], dvItems[k]["ItemName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtClients = Clients.FillCombo("1", GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
		dtDetails = ChecksDetails.SelectByCheckID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtChecksDetailsAccessories = ChecksDetailsAccessories.SelectByCheckIDs(",0,", GlobalVariables.IsArabic ? "1" : "0");
		dtChecksDetailsAddtionals = ChecksDetailsAdditionals.SelectByCheckIDs(",0,", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Expected O, but got Unknown
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDetailID"].DefaultCellValue = -1;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Comments"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Comments");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comments"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "إضافات" : "Add");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomDiscountValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SentQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceChargeAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OfferDiscountRatio"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID != "")
		{
			DataTable dataTable = Checks.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			dtcheckTable = ChecksTables.SelectByCheckID(RowID, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
			DisplayData();
		}
		else if (CheckID == -1)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)txtTableNo).Text = TableCode;
			((Control)(object)btnOK).Visible = false;
			((Control)(object)btnSaveClose).Text = (GlobalVariables.IsArabic ? "F2حفظ و عودة" : " Save And Back F2 ");
		}
		else if (CheckID != 0)
		{
			DataTable dataTable2 = Checks.Select(CheckID.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			dtcheckTable = ChecksTables.SelectByCheckID(CheckID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable2.Rows.Count > 0)
			{
				drMaster = dataTable2.Rows[0];
			}
			else
			{
				drMaster = null;
			}
			DisplayData();
			if (!CheckUserLogin())
			{
				Close();
				return;
			}
			btnUpdateClick();
			if (Updating && CheckID != 0 && TableName != "")
			{
				UsersTransactions.Insert_Update("-1", GlobalVariables.UserLoginID, TableName, CheckID.ToString());
			}
			((Control)(object)btnOK).Visible = false;
			((Control)(object)btnSaveClose).Text = (GlobalVariables.IsArabic ? "F2حفظ و عودة" : " Save And Back F2 ");
		}
		else
		{
			drMaster = null;
			DisplayData();
		}
	}

	public bool CheckUserLogin()
	{
		if ((CheckID != 0 || RowID != "") && TableName != "")
		{
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			if (DisplayDataDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), (RowID != "") ? RowID : CheckID.ToString(), TableName))
			{
				GlobalVariables.InformationMB.Show("يوجد تعديل فى البيانات برجاء تنشيط البيانات", "Data Has Been Modified Please Refresh Your Data ");
				return false;
			}
			DataTable dataTable = UsersTransactions.CheckTransaction(TableName, (RowID != "") ? RowID : CheckID.ToString());
			if (dataTable.Rows.Count > 0)
			{
				if (!(dataTable.Rows[0]["User_ID"].ToString() == GlobalVariables.UserID))
				{
					GlobalVariables.InformationMB.Show("لا يمكن تعديل هذا البيان. هذا البيان مستخدم حاليا\u064b من المستخدم \r\n" + dataTable.Rows[0]["UserNameAr"].ToString(), "Can not Update this Data. This data is now open by user \r\n" + dataTable.Rows[0]["UserNameEn"].ToString());
					return false;
				}
				GlobalVariables.QuestionMB.Show(" هذا البيان مستخدم حاليا\u064b من المستخدم \r\n" + dataTable.Rows[0]["UserNameAr"].ToString() + "\r\nهل تريد اخراجه؟", "This data is now open by user \r\n" + dataTable.Rows[0]["UserNameEn"].ToString() + "\r\nDo You want to clear it ?");
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					return false;
				}
				UsersTransactions.DeleteByRowID(TableName, (RowID != "") ? RowID : CheckID.ToString());
			}
		}
		return true;
	}

	public override void DisplayData()
	{
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Expected O, but got Unknown
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ab: Expected O, but got Unknown
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["CheckNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["CheckDate"];
			((Control)(object)txtPersonCount).Text = drMaster["PersonCount"].ToString();
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["ClientID"];
			if (cboClient.SelectedIndex > -1)
			{
				if (ShowFirstDiscount)
				{
					((Control)(object)txtDiscountRatio).Text = decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
				}
				else if (AdditionalDiscountIncludeTax || AdditionalDiscountWithoutTax)
				{
					((Control)(object)txtDiscountRatio2).Text = decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
				}
				if (dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == DBNull.Value || dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == dtRoomData.Rows[0]["PriceTypeID"])
				{
					dtItemsAndGroups = dtItemsAndGroupsCopy;
				}
				else
				{
					dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
				}
			}
			((TextEditorControlBase)cboClient).ValueChanged += cboClient_ValueChanged;
			((Control)(object)txtTableNo).Text = GetTableNo();
			((Control)(object)txtGrossValue).Text = decimal.Parse(drMaster["GrossValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((Control)(object)txtDiscountValue).Text = decimal.Parse(drMaster["DiscountBeforeTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountRatio).Text = decimal.Parse(drMaster["DiscountBeforeTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
			((Control)(object)txtDiscountValue2).Text = decimal.Parse(drMaster["DiscountAfterTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountRatio2).Text = decimal.Parse(drMaster["DiscountAfterTaxRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
			((Control)(object)txtServiceChargeValue).Text = decimal.Parse(drMaster["ServiceChargeValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRoundingValue).Text = decimal.Parse(drMaster["RoundingValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkMinCharge).Checked = bool.Parse(drMaster["IsMinCharge"].ToString());
			((Control)(object)txtMinChargeValue).Text = decimal.Parse(drMaster["MinChargeValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkMaxCharge).Checked = bool.Parse(drMaster["IsMaxCharge"].ToString());
			((Control)(object)txtMaxChargeValue).Text = decimal.Parse(drMaster["MaxChargeValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged -= new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
			((UltraToggleEditorBase)chkIsInvoice).CheckedChanged -= chkIsInvoice_CheckedChanged;
			((UltraToggleEditorBase)chkIsInvoice).Checked = bool.Parse(drMaster["IsInvoice"].ToString());
			((UltraToggleEditorBase)chkIsInvoice).CheckedChanged += chkIsInvoice_CheckedChanged;
			((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged += new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
			((Control)(object)txtInvoiceNo).Text = drMaster["InvoiceNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			CheckLog = drMaster["CheckLog"].ToString();
			((TextEditorControlBase)cboCaptainOrder).Value = drMaster["CaptainOrderID"];
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ChecksDetails.SelectByCheckID(drMaster["CheckID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtChecksDetailsAccessories = ChecksDetailsAccessories.SelectByCheckIDs("," + drMaster["CheckID"].ToString() + ",", GlobalVariables.IsArabic ? "1" : "0");
			dtChecksDetailsAddtionals = ChecksDetailsAdditionals.SelectByCheckIDs("," + drMaster["CheckID"].ToString() + ",", GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			dtDeletedItems.Rows.Clear();
			if (drMaster["Approved"].Equals(true) || drMaster["Closed"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
				((Control)(object)btnPrint).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
				((Control)(object)btnPrint).Enabled = true;
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
		((EditorButtonControlBase)txtCode).ReadOnly = true;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((EditorButtonControlBase)cboClient).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || (NavMode && CheckID != 0);
		((EditorButtonControlBase)txtClientBarcode).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || (NavMode && CheckID != 0);
		((EditorButtonControlBase)txtTableNo).ReadOnly = true;
		((EditorButtonControlBase)txtPersonCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((Control)(object)btnClientAdd).Visible = CanAddNewClient && !NavMode;
		((EditorButtonControlBase)txtDiscountRatio).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || (NavMode && CheckID != 0);
		((EditorButtonControlBase)txtDiscountValue).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || (NavMode && CheckID != 0);
		UltraTextEditor obj = txtDiscountRatio;
		UltraTextEditor obj2 = txtDiscountValue;
		UltraButton obj3 = btnDiscountRatio;
		UltraButton obj4 = btnDiscountValue;
		UltraLabel obj5 = lblDiscountRatio;
		bool flag = (((Control)(object)lblDiscountValue).Visible = ShowFirstDiscount);
		bool flag2 = (((Control)(object)obj5).Visible = flag);
		bool flag4 = (((Control)(object)obj4).Visible = flag2);
		bool flag6 = (((Control)(object)obj3).Visible = flag4);
		bool visible = (((Control)(object)obj2).Visible = flag6);
		((Control)(object)obj).Visible = visible;
		UltraTextEditor obj6 = txtDiscountRatio2;
		UltraTextEditor obj7 = txtDiscountValue2;
		UltraButton obj8 = btnDiscountRatio2;
		UltraButton obj9 = btnDiscountValue2;
		UltraLabel obj10 = lblDiscountRatio2;
		flag = (((Control)(object)lblDiscountValue2).Visible = AdditionalDiscountWithoutTax || AdditionalDiscountIncludeTax);
		flag2 = (((Control)(object)obj10).Visible = flag);
		flag4 = (((Control)(object)obj9).Visible = flag2);
		flag6 = (((Control)(object)obj8).Visible = flag4);
		visible = (((Control)(object)obj7).Visible = flag6);
		((Control)(object)obj6).Visible = visible;
		((EditorButtonControlBase)txtDiscountRatio2).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || (NavMode && CheckID != 0);
		((EditorButtonControlBase)txtDiscountValue2).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || (NavMode && CheckID != 0);
		((Control)(object)btnCloseCheck).Enabled = CanCloseCheck && !NavMode;
		((Control)(object)btnRefreshData).Visible = false;
		((Control)(object)btnClientSearch).Visible = (drMaster == null || !bool.Parse(drMaster["IsPrinted"].ToString())) && !NavMode;
		((Control)(object)btnPersonCount).Visible = !NavMode;
		((Control)(object)btnPrint).Visible = drMaster != null;
		((Control)(object)btnSent).Enabled = !NavMode;
		((Control)(object)btnMerge).Enabled = CanMergeCheck && !NavMode && drMaster != null && !bool.Parse(drMaster["IsPrinted"].ToString());
		((Control)(object)btnSeparate).Enabled = CanSplitCheck && !NavMode && drMaster != null && !bool.Parse(drMaster["IsPrinted"].ToString());
		UltraButton obj11 = btnPlus;
		UltraButton obj12 = btnSubtract;
		UltraButton obj13 = btnClear;
		UltraButton obj14 = btnDiscountRatio;
		UltraButton obj15 = btnDiscountValue;
		UltraButton obj16 = btnDiscountRatio2;
		bool flag14 = (((Control)(object)btnDiscountValue2).Enabled = drMaster != null && !bool.Parse(drMaster["IsPrinted"].ToString()));
		flag = (((Control)(object)obj16).Enabled = flag14);
		flag2 = (((Control)(object)obj15).Enabled = flag);
		flag4 = (((Control)(object)obj14).Enabled = flag2);
		flag6 = (((Control)(object)obj13).Enabled = flag4);
		visible = (((Control)(object)obj12).Enabled = flag6);
		((Control)(object)obj11).Enabled = visible;
		if (CheckID == 0 && !NavMode)
		{
			UltraButton obj17 = btnClear;
			UltraButton obj18 = btnDiscountValue;
			UltraButton obj19 = btnDiscountRatio;
			UltraButton obj20 = btnDiscountValue2;
			flag2 = (((Control)(object)btnDiscountRatio2).Enabled = true);
			flag4 = (((Control)(object)obj20).Enabled = flag2);
			flag6 = (((Control)(object)obj19).Enabled = flag4);
			visible = (((Control)(object)obj18).Enabled = flag6);
			((Control)(object)obj17).Enabled = visible;
		}
		((Control)(object)btnOK).Visible = false;
		((Control)(object)chkMinCharge).Enabled = CanMinimunCharge && !NavMode && bool.Parse(dtRoomData.Rows[0]["HasMinCharge"].ToString());
		((Control)(object)txtMinChargeValue).Enabled = CanMinimunCharge && !NavMode && bool.Parse(dtRoomData.Rows[0]["HasMinCharge"].ToString());
		((Control)(object)chkMaxCharge).Enabled = CanMinimunCharge && !NavMode && bool.Parse(dtRoomData.Rows[0]["HasMaxCharge"].ToString());
		((Control)(object)txtMaxChargeValue).Enabled = CanMinimunCharge && !NavMode && bool.Parse(dtRoomData.Rows[0]["HasMaxCharge"].ToString());
		((EditorButtonControlBase)cboCaptainOrder).ReadOnly = (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString())) || NavMode;
		((Control)(object)chkIsInvoice).Visible = ((dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["PrintTaxInvoices"].ToString())) ? true : false);
		((Control)(object)txtInvoiceNo).Visible = ((dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["PrintTaxInvoices"].ToString())) ? true : false);
		((Control)(object)chkIsInvoice).Enabled = !NavMode;
		int num = 0;
		if (cboCaptainOrder.SelectedIndex > -1)
		{
			num = int.Parse(((TextEditorControlBase)cboCaptainOrder).Value.ToString());
		}
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtCaptainOrder);
			dataView.RowFilter = "IsActive = 1";
			GlobalFunctions.FillCombo(cboCaptainOrder, dataView.ToTable(), "CaptainOrderID", "CaptainOrderName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboCaptainOrder, dtCaptainOrder, "CaptainOrderID", "CaptainOrderName");
		}
		if (num > 0)
		{
			((TextEditorControlBase)cboCaptainOrder).Value = num;
		}
		if (!Updating)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			DataRow dataRow = dtItemsAndGroups.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = DBNull.Value;
				}
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].ValueList.ItemCount == 0)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = DBNull.Value;
				}
			}
		}
	}

	public override void ClearControls()
	{
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? Checks.GetCodeByBranchID("1", "0", "0", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtClientBarcode).Clear();
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		CheckLog = "";
		((Control)(object)txtTableNo).Text = "0";
		((Control)(object)txtPersonCount).Text = "0";
		((Control)(object)txtGrossValue).Text = "0";
		((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
		((Control)(object)txtDiscountValue).Text = "0";
		((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
		((Control)(object)txtDiscountValue2).Text = "0";
		((Control)(object)txtDiscountRatio2).Text = "0";
		((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
		((Control)(object)txtServiceChargeValue).Text = "0";
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtRoundingValue).Text = "0";
		((UltraToggleEditorBase)chkMinCharge).Checked = bool.Parse(dtRoomData.Rows[0]["HasMinCharge"].ToString());
		((Control)(object)txtMinChargeValue).Text = dtRoomData.Rows[0]["MinChargeValue"].ToString();
		((UltraToggleEditorBase)chkMaxCharge).Checked = bool.Parse(dtRoomData.Rows[0]["HasMaxCharge"].ToString());
		((Control)(object)txtMaxChargeValue).Text = dtRoomData.Rows[0]["MaxChargeValue"].ToString();
		((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged -= new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
		((UltraToggleEditorBase)chkIsInvoice).CheckedChanged -= chkIsInvoice_CheckedChanged;
		((UltraToggleEditorBase)chkIsInvoice).Checked = false;
		((UltraToggleEditorBase)chkIsInvoice).CheckedChanged += chkIsInvoice_CheckedChanged;
		((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged += new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
		((Control)(object)txtInvoiceNo).Text = "";
		cboCaptainOrder.SelectedIndex = -1;
		if (bool.Parse(dtRoomData.Rows[0]["IsDynamicMinCharge"].ToString()))
		{
			((Control)(object)txtMinChargeValue).Text = decimal.Parse(Rooms.GetDynamicMinCharge(RoomID.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkMinCharge).Checked = ((decimal.Parse(((Control)(object)txtMinChargeValue).Text) > 0m) ? true : false);
		}
		if (bool.Parse(dtRoomData.Rows[0]["IsDynamicMaxCharge"].ToString()))
		{
			((Control)(object)txtMaxChargeValue).Text = decimal.Parse(Rooms.GetDynamicMaxCharge(RoomID.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), IsFromServer: false).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkMaxCharge).Checked = ((decimal.Parse(((Control)(object)txtMaxChargeValue).Text) > 0m) ? true : false);
		}
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		dtChecksDetailsAccessories.Rows.Clear();
		dtChecksDetailsAddtionals.Rows.Clear();
		if (dtRoomData.Rows.Count > 0 && dtRoomData.Rows[0]["DefaultClientID"] != DBNull.Value && Adding)
		{
			((TextEditorControlBase)cboClient).Value = dtRoomData.Rows[0]["DefaultClientID"];
		}
		dtDeletedItems.Rows.Clear();
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الشيك" : "Please Enter The Check Date");
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
		if ((((UltraToggleEditorBase)chkMinCharge).Checked || ((UltraToggleEditorBase)chkMaxCharge).Checked) && (((Control)(object)txtPersonCount).Text == "" || int.Parse(((Control)(object)txtPersonCount).Text) < 1))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد الاشخاص" : "Please Enter Person Count");
			((TextEditorControlBase)txtPersonCount).Focus();
			return false;
		}
		if (bool.Parse(dtRoomData.Rows[0]["EnforceCaptainOrderSelection"].ToString()) && cboCaptainOrder.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الكابتن" : "Please Select Captain Order");
			((TextEditorControlBase)cboCaptainOrder).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkMinCharge).Checked && (((Control)(object)txtMinChargeValue).Text == "" || decimal.Parse(((Control)(object)txtMinChargeValue).Text) == 0m))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة الحد الادنى" : "Please Enter Min Charge");
			((TextEditorControlBase)txtMinChargeValue).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkIsInvoice).Checked && ((Control)(object)txtInvoiceNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الفاتورة" : "Please Enter invoice No");
			((TextEditorControlBase)txtInvoiceNo).Focus();
			return false;
		}
		if (CashAmount == 0m && VisaAmount == 0m && OnAccountAmount == 0m && ((drMaster != null && bool.Parse(drMaster["Closed"].ToString()) && decimal.Parse(drMaster["NetPrice"].ToString()) > 0m) || (CheckClosed && decimal.Parse(((Control)(object)txtNetprice).Text) > 0m)))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء تحديد طريقة الدفع" : "Please Determine Payment Method");
			return false;
		}
		if (decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) > 0m && decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن إعطاء نوعين خصم فى نفس الشيك" : "Cannot Put Different Discount In The Same Check");
			return false;
		}
		if (dtPOSDefaultData != null && dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["SerialByShiftDetailID"].ToString()) && drMaster != null && drMaster["ShiftDetailID"].ToString() != ShiftDetailID.ToString())
		{
			((Control)(object)txtCode).Text = ((Control)(object)txtCode).Text + "//S";
		}
		if (((UltraToggleEditorBase)chkIsInvoice).Checked && ((Control)(object)txtInvoiceNo).Text != "")
		{
			if (Main.CheckForValueByBranchIDAndFiscalYearID("POS_Checks", "InvoiceNo", ((Control)(object)txtInvoiceNo).Text, Adding ? "0" : drMaster["InvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
			{
				string invoiceNoByBranchID = Checks.GetInvoiceNoByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
				GlobalVariables.QuestionMB.Show("رقم هذا الفاتورة متواجد من قبل \n سوف يتم الحفظ برقم " + invoiceNoByBranchID, "The Invoice Number Already Exists It Will Be Saved With No. : " + invoiceNoByBranchID);
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					((TextEditorControlBase)txtInvoiceNo).Focus();
					return false;
				}
				((Control)(object)txtInvoiceNo).Text = invoiceNoByBranchID;
			}
		}
		else if (Main.CheckForValueByBranchIDAndFiscalYearID("POS_Checks", "CheckNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["CheckNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), " And IsDineIn =1 And IsDelivery=0 And IsTakeAway=0  " + ((dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["SerialByShiftDetailID"].ToString())) ? ("  and ShiftDetailID = " + ShiftDetailID) : "")) > 0)
		{
			string codeByBranchID = Checks.GetCodeByBranchID("1", "0", "0", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال المخزن  ", "Please Enter Store Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].DroppedDown = true;
				return false;
			}
		}
		if (GlobalFunctions.GetOption("POSSalesAutoGenerateJVs"))
		{
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب المبيعات من حسابات النظام  ", "Please Select Sales Account From SystemAccounts ");
				return false;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='ServiceChargeAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب رسم الخدمة من حسابات النظام  ", "Please Select Service Charge Account From SystemAccounts ");
				return false;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='SalesDiscountAccount' ")[0]["AccountID"] == DBNull.Value && (decimal.Parse(((Control)(object)txtDiscountValue).Text) > 0m || decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m))
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب الخصم المسموح به من حسابات النظام  ", "Please Select Sales Discount Account From SystemAccounts ");
				return false;
			}
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='POSRoundingValuesAccount' ")[0]["AccountID"] == DBNull.Value && dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0.0)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب فروق تقريب بيع مباشر من حسابات النظام  ", "Please Select Rounding Values Account From SystemAccounts ");
				return false;
			}
		}
		return true;
	}

	public bool SaveClose(bool Close, bool DisplaySentQtyMessage)
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
		if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()) && Adding)
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
		if (ValidateData())
		{
			DataSaved = true;
			if (Adding)
			{
				AddData();
				if (DataSaved)
				{
					Adding = false;
					Updating = true;
					if (!Close)
					{
						DataTable dataTable3 = Checks.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
						dtcheckTable = ChecksTables.SelectByCheckID(RowID, GlobalVariables.IsArabic ? "1" : "0");
						if (dataTable3.Rows.Count > 0)
						{
							drMaster = dataTable3.Rows[0];
						}
						else
						{
							drMaster = null;
						}
					}
					SetControls(NavMode: false);
					CheckUserLogin();
					if (Updating && TableName != "" && (RowID != "" || CheckID != 0))
					{
						UsersTransactions.Insert_Update("-1", GlobalVariables.UserLoginID, TableName, (RowID != "") ? RowID : CheckID.ToString());
					}
				}
			}
			else
			{
				if (Updating && TableName != "" && (RowID != "" || CheckID != 0))
				{
					if (UsersTransactions.CheckNotKicked(GlobalVariables.UserLoginID, TableName, (RowID != "") ? RowID : CheckID.ToString()).Rows.Count == 0)
					{
						GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لقد تم إخراجك من قبل مستخدم اخر ", "You Have Been Kicked By Another User");
						return false;
					}
					if (DisplayDataDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), (RowID != "") ? RowID : CheckID.ToString(), TableName))
					{
						GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
						return false;
					}
				}
				UpdateData();
				if (!Close)
				{
					DataTable dataTable4 = Checks.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
					dtcheckTable = ChecksTables.SelectByCheckID(RowID, GlobalVariables.IsArabic ? "1" : "0");
					if (dataTable4.Rows.Count > 0)
					{
						drMaster = dataTable4.Rows[0];
					}
					else
					{
						drMaster = null;
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				}
				if ((RowID != "" || CheckID != 0) && TableName != "")
				{
					UsersTransactions.DeleteByRowID(TableName, (RowID != "") ? RowID : CheckID.ToString());
				}
				DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
				if (Updating && TableName != "" && (RowID != "" || CheckID != 0))
				{
					UsersTransactions.Insert_Update("-1", GlobalVariables.UserLoginID, TableName, (RowID != "") ? RowID : CheckID.ToString());
				}
			}
			if (DataSaved && bool.Parse(dtRoomData.Rows[0]["SentItemsMessage"].ToString()) && DisplaySentQtyMessage && dtDetails.Select("Qty >SentQty And ItemID <>" + ((dtRoomData.Rows[0]["MinChargeItemID"] == DBNull.Value) ? "-1" : dtRoomData.Rows[0]["MinChargeItemID"].ToString())).Length != 0)
			{
				GlobalVariables.QuestionMB.Show("هل تريد إرسال الاصناف ؟", "Are You Sure You want to Send Items ?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					SentItems();
				}
			}
			DisplayData();
			if ((RowID != "" || CheckID != 0) && TableName != "" && Close)
			{
				UsersTransactions.DeleteByRowID(TableName, (RowID != "") ? RowID : CheckID.ToString());
			}
			if (DataSaved && Close)
			{
				base.Close();
			}
			if (!DataSaved)
			{
				return false;
			}
			return true;
		}
		return false;
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

	public override void btnSaveClose_Click(object sender, EventArgs e)
	{
		SaveClose(Close: true, DisplaySentQtyMessage: true);
	}

	public override void AddData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Expected O, but got Unknown
		//IL_1216: Unknown result type (might be due to invalid IL or missing references)
		//IL_1220: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = Checks.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", "0", "0", ((Control)(object)txtPersonCount).Text, (cboClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboClient).Value.ToString() : "Null", "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString() : "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString() : dtRoomData.Rows[0]["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "Null", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue).Text.ToString(), (((Control)(object)txtDiscountRatio).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio).Text.ToString(), "0", (((Control)(object)txtServiceChargeValue).Text == "") ? "0" : ((Control)(object)txtServiceChargeValue).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscountValue2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue2).Text.ToString(), (((Control)(object)txtDiscountRatio2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text.ToString(), (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((Control)(object)txtNetprice).Text, ((UltraToggleEditorBase)chkMinCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkMinCharge).Checked ? ((Control)(object)txtMinChargeValue).Text : "0", ((UltraToggleEditorBase)chkMaxCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkMaxCharge).Checked ? ((Control)(object)txtMaxChargeValue).Text : "0", IsCash ? "1" : "0", CashAmount.ToString(), IsVisa ? "1" : "0", (VisatypeID == 0) ? "Null" : VisatypeID.ToString(), (VisaNo == "0") ? "Null" : VisaNo, VisaAmount.ToString(), IsOnAccount ? "1" : "0", OnAccountAmount.ToString(), PaidAmount.ToString(), (decimal.Parse(((Control)(object)txtNetprice).Text) - PaidAmount).ToString(), ((Control)(object)txtNotes).Text, CheckLog, CheckClosed ? "1" : "0", ShiftDetailID, ShiftDetailUserID, dtRoomData.Rows[0]["RoomID"].ToString(), GlobalVariables.UserID, (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", (cboCaptainOrder.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCaptainOrder).Value.ToString(), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "0", "0", "Null", "Null", ((UltraToggleEditorBase)chkIsInvoice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsInvoice).Checked ? ((Control)(object)txtInvoiceNo).Text : "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					int num2 = int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CheckDetailID"].Value.ToString());
					int num3 = ChecksDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ReturnedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ReturnedQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["AdditionalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["AdditionalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ServiceChargeAmount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ServiceChargeAmount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["RoomDiscountValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["RoomDiscountValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["SentDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SentDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), (MergedCheckIDs == ",") ? "0" : "1", (((UltraGridBase)ULGData).Rows[j].Cells["SentQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["SentQty"].Value.ToString(), "0", ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["offerID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["offerID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["OfferDiscountRatio"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["OfferDiscountRatio"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int k = 0; k < dtChecksDetailsAccessories.Rows.Count; k++)
					{
						if (int.Parse(dtChecksDetailsAccessories.Rows[k]["CheckDetailID"].ToString()) == num2)
						{
							dtChecksDetailsAccessories.Rows[k]["CheckDetailID"] = num3;
						}
					}
					for (int l = 0; l < dtChecksDetailsAddtionals.Rows.Count; l++)
					{
						if (int.Parse(dtChecksDetailsAddtionals.Rows[l]["CheckDetailID"].ToString()) == num2)
						{
							dtChecksDetailsAddtionals.Rows[l]["CheckDetailID"] = num3;
						}
					}
				}
				for (int m = 0; m < dtChecksDetailsAccessories.Rows.Count; m++)
				{
					dtChecksDetailsAccessories.Rows[m]["CheckDetailAccessoryID"] = -1;
					dtChecksDetailsAccessories.Rows[m]["VoucherDate"] = dtpDate.DateTime;
					dtChecksDetailsAccessories.Rows[m]["BranchID"] = GlobalVariables.CurrentBranchID;
					dtChecksDetailsAccessories.Rows[m]["CheckID"] = num.ToString();
				}
				for (int n = 0; n < dtChecksDetailsAddtionals.Rows.Count; n++)
				{
					dtChecksDetailsAddtionals.Rows[n]["CheckDetailAdditionalID"] = -1;
					dtChecksDetailsAddtionals.Rows[n]["VoucherDate"] = dtpDate.DateTime;
					dtChecksDetailsAddtionals.Rows[n]["BranchID"] = GlobalVariables.CurrentBranchID;
					dtChecksDetailsAddtionals.Rows[n]["CheckID"] = num.ToString();
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				if (dtChecksDetailsAccessories.Rows.Count > 0)
				{
					ChecksDetailsAccessories.Insert_UpdateByTable(dtChecksDetailsAccessories, GlobalVariables.UserID);
				}
				if (dtChecksDetailsAddtionals.Rows.Count > 0)
				{
					ChecksDetailsAdditionals.Insert_UpdateByTable(dtChecksDetailsAddtionals, GlobalVariables.UserID);
				}
			}
			ChecksTables.Insert_Update("-1", num.ToString(), TableID.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV");
			}
			else
			{
				RowID = num.ToString();
				Main.EndBulkTrans(FromServer: false);
			}
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
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_0afb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Expected O, but got Unknown
		//IL_1673: Unknown result type (might be due to invalid IL or missing references)
		//IL_167d: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			CalculateGoss();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateRowActualUnitSalesPrice(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			int num = Checks.Insert_Update(drMaster["CheckID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", "0", "0", ((Control)(object)txtPersonCount).Text, (cboClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboClient).Value.ToString() : "Null", "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString() : "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString() : dtRoomData.Rows[0]["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (drMaster["EmployeeID"] == DBNull.Value) ? "Null" : drMaster["EmployeeID"].ToString(), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue).Text.ToString(), (((Control)(object)txtDiscountRatio).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio).Text.ToString(), "0", (((Control)(object)txtServiceChargeValue).Text == "") ? "0" : ((Control)(object)txtServiceChargeValue).Text, (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscountValue2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue2).Text.ToString(), (((Control)(object)txtDiscountRatio2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text.ToString(), (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((Control)(object)txtNetprice).Text, ((UltraToggleEditorBase)chkMinCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkMinCharge).Checked ? ((Control)(object)txtMinChargeValue).Text : "0", ((UltraToggleEditorBase)chkMaxCharge).Checked ? "1" : "0", ((UltraToggleEditorBase)chkMaxCharge).Checked ? ((Control)(object)txtMaxChargeValue).Text : "0", IsCash ? "1" : "0", CashAmount.ToString(), IsVisa ? "1" : "0", (VisatypeID == 0) ? "Null" : VisatypeID.ToString(), (VisaNo == "0") ? "Null" : VisaNo, VisaAmount.ToString(), IsOnAccount ? "1" : "0", OnAccountAmount.ToString(), PaidAmount.ToString(), (decimal.Parse(((Control)(object)txtNetprice).Text) - PaidAmount).ToString(), ((Control)(object)txtNotes).Text, CheckLog, (drMaster["Closed"] != DBNull.Value && bool.Parse(drMaster["Closed"].ToString())) ? "1" : (CheckClosed ? "1" : "0"), ShiftDetailID, ShiftDetailUserID, (drMaster["RoomID"] == DBNull.Value) ? dtRoomData.Rows[0]["RoomID"].ToString() : drMaster["RoomID"].ToString(), GlobalVariables.UserID, (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", (cboCaptainOrder.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCaptainOrder).Value.ToString(), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), "0", bool.Parse(drMaster["IsPrinted"].ToString()) ? "1" : "0", (drMaster["PrintUserID"] == DBNull.Value) ? "Null" : drMaster["PrintUserID"].ToString(), (drMaster["PrintDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["PrintDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsInvoice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsInvoice).Checked ? ((Control)(object)txtInvoiceNo).Text : "Null", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["HasChanges"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV");
			for (int j = 0; j < dtDeletedItems.Rows.Count; j++)
			{
				ChecksDetails.DeleteVirtualWithNotes(dtDeletedItems.Rows[j]["CheckDetailID"].ToString(), dtDeletedItems.Rows[j]["Notes"].ToString(), GlobalVariables.UserID);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				string text2 = ",";
				string text3 = ",";
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
				{
					text = text + ((UltraGridBase)ULGData).Rows[k].Cells["CheckDetailID"].Value.ToString() + ",";
				}
				for (int l = 0; l < dtChecksDetailsAccessories.Rows.Count; l++)
				{
					text2 = text2 + dtChecksDetailsAccessories.Rows[l]["CheckDetailAccessoryID"].ToString() + ",";
				}
				for (int m = 0; m < dtChecksDetailsAddtionals.Rows.Count; m++)
				{
					text3 = text3 + dtChecksDetailsAddtionals.Rows[m]["CheckDetailAdditionalID"].ToString() + ",";
				}
				Main.DeleteForUpdate("POS_ChecksDetailsTempPrint", "CheckID", drMaster["CheckID"].ToString(), "CheckDetailID", text);
				Main.DeleteForUpdate("POS_ChecksDetailsAccessories", "CheckID", drMaster["CheckID"].ToString(), "CheckDetailAccessoryID", text2);
				Main.DeleteForUpdate("POS_ChecksDetailsAdditionals", "CheckID", drMaster["CheckID"].ToString(), "CheckDetailAdditionalID", text3);
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; n++)
				{
					int num2 = int.Parse(((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString());
					int num3 = ChecksDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[n].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ReturnedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["ReturnedQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["AdditionalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["AdditionalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ServiceChargeAmount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["ServiceChargeAmount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["NetPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["RoomDiscountValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["RoomDiscountValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["SentDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[n].Cells["SentDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), (MergedCheckIDs == ",") ? "0" : "1", (((UltraGridBase)ULGData).Rows[n].Cells["SentQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["SentQty"].Value.ToString(), "0", ((UltraGridBase)ULGData).Rows[n].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["offerID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["offerID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["OfferDiscountRatio"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["OfferDiscountRatio"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int num4 = 0; num4 < dtChecksDetailsAccessories.Rows.Count; num4++)
					{
						if (int.Parse(dtChecksDetailsAccessories.Rows[num4]["CheckDetailID"].ToString()) == num2)
						{
							dtChecksDetailsAccessories.Rows[num4]["CheckDetailID"] = num3;
						}
					}
					for (int num5 = 0; num5 < dtChecksDetailsAddtionals.Rows.Count; num5++)
					{
						if (int.Parse(dtChecksDetailsAddtionals.Rows[num5]["CheckDetailID"].ToString()) == num2)
						{
							dtChecksDetailsAddtionals.Rows[num5]["CheckDetailID"] = num3;
						}
					}
				}
				for (int num6 = 0; num6 < dtChecksDetailsAccessories.Rows.Count; num6++)
				{
					dtChecksDetailsAccessories.Rows[num6]["VoucherDate"] = dtpDate.DateTime;
					dtChecksDetailsAccessories.Rows[num6]["BranchID"] = GlobalVariables.CurrentBranchID;
					dtChecksDetailsAccessories.Rows[num6]["CheckID"] = num.ToString();
				}
				for (int num7 = 0; num7 < dtChecksDetailsAddtionals.Rows.Count; num7++)
				{
					dtChecksDetailsAddtionals.Rows[num7]["VoucherDate"] = dtpDate.DateTime;
					dtChecksDetailsAddtionals.Rows[num7]["BranchID"] = GlobalVariables.CurrentBranchID;
					dtChecksDetailsAddtionals.Rows[num7]["CheckID"] = num.ToString();
				}
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				if (dtChecksDetailsAccessories.Rows.Count > 0)
				{
					ChecksDetailsAccessories.Insert_UpdateByTable(dtChecksDetailsAccessories, GlobalVariables.UserID);
				}
				if (dtChecksDetailsAddtionals.Rows.Count > 0)
				{
					ChecksDetailsAdditionals.Insert_UpdateByTable(dtChecksDetailsAddtionals, GlobalVariables.UserID);
				}
			}
			if (MergedCheckIDs != ",")
			{
				Checks.UpdateMergedChecks(MergedCheckIDs, num.ToString(), ((Control)(object)txtCode).Text, GlobalVariables.IsArabic ? "1" : "0");
			}
			RowID = num.ToString();
			Main.EndBulkTrans(FromServer: false);
		}
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (dtDeletedItems.Rows.Count > 0)
		{
			PrintDeletedSentItems(dtDeletedItems);
		}
	}

	public override void DeleteData()
	{
		frmPOSComments frmPOSComments2 = new frmPOSComments(GlobalVariables.IsArabic ? "ملاحظات" : "Notes", _IsInt: false, _IsNumeric: false);
		frmPOSComments2.WindowState = FormWindowState.Normal;
		frmPOSComments2.ShowDialog(this);
		if (frmPOSComments2.Value == "")
		{
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال اسباب الحذف" : "Please Enter Delete Reasons");
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			Checks.DeleteVirtual(drMaster["CheckID"].ToString(), GlobalVariables.UserID);
			ChecksDetails.DeleteVirtualByCheckIDWithNotes(drMaster["CheckID"].ToString(), frmPOSComments2.Value, GlobalVariables.UserID);
			ChecksDetailsAccessories.DeleteVirtualByCheckID(drMaster["CheckID"].ToString(), GlobalVariables.UserID);
			ChecksDetailsAdditionals.DeleteVirtualByCheckID(drMaster["CheckID"].ToString(), GlobalVariables.UserID);
			if (drMaster["StockControlJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["StockControlJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			if (drMaster["SalesJVID"] != DBNull.Value)
			{
				JV.DeleteVirtual(drMaster["SalesJVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		PrintDeletedSentItems((DataTable)((UltraGridBase)ULGData).DataSource);
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ChecksReport("," + dtRoomData.Rows[0]["RoomID"].ToString() + ",", "1", "-1", "-1", GlobalVariables.BranchIDs);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["CheckID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
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
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtRoomData.Rows[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
		dtItemsAndGroupsCopy = dtItemsAndGroups.Copy();
		FillItemsGroups();
		dvItems = new DataView(dtItemsAndGroups);
		dvItems.RowFilter = " IsMain=0 ";
		dvItems.ToTable();
		for (int k = 0; k < dvItems.Count; k++)
		{
			vlItems.ValueListItems.Add(dvItems[k]["ItemID"], dvItems[k]["ItemName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtClients = Clients.FillCombo("1", GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
	}

	public void FillItemsGroups()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		int num = 8;
		int num2 = 6;
		DataTable dataTable = dtItemsAndGroups.DefaultView.ToTable(true, "ItemFirstClassificationName");
		DataView dataView = new DataView(dtItemsAndGroups);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			UltraPanel val = new UltraPanel();
			dataView.RowFilter = ((dataTable.Rows[i]["ItemFirstClassificationName"] == DBNull.Value) ? "IsMain=1 And ItemFirstClassificationName is null" : ("IsMain=1 And ItemFirstClassificationName='" + dataTable.Rows[i]["ItemFirstClassificationName"].ToString() + "'"));
			dataView.ToTable();
			if (dataView.Count > 0)
			{
				((UltraTabControlBase)UTCItemsGroups).Tabs.Add(i.ToString(), (dataTable.Rows[i]["ItemFirstClassificationName"] != DBNull.Value) ? dataTable.Rows[i]["ItemFirstClassificationName"].ToString() : (GlobalVariables.IsArabic ? "مجموعات الاصناف" : "Item Groups"));
				((UltraControlBase)UTCItemsGroups).UseAppStyling = false;
				((UltraTabControlBase)UTCItemsGroups).TabSize = new Size(15, 40);
				((UltraTabControlBase)UTCItemsGroups).Tabs[i.ToString()].FixedWidth = 120;
				((UltraTabControlBase)UTCItemsGroups).TabHeaderAreaAppearance.FontData.SizeInPoints = 10f;
				((Control)(object)val).Dock = DockStyle.Fill;
				val.AutoScroll = true;
				((Control)(object)((UltraTabControlBase)UTCItemsGroups).Tabs[i.ToString()].TabPage).Controls.Add((Control)(object)val);
			}
			num = 8;
			num2 = 6;
			for (int j = 0; j < dataView.Count; j++)
			{
				UltraButton val2 = new UltraButton();
				((Control)(object)val2).Click += btnGroub_Click;
				((Control)(object)val2).Tag = dataView[j]["ItemID"].ToString() + "," + dataView[j]["IsOffer"].ToString() + "," + dataView[j]["IsQtyDiscount"].ToString();
				((Control)(object)val2).Text = dataView[j]["ItemName"].ToString();
				((Control)(object)val2).Height += 25;
				((Control)(object)val.ClientArea).Controls.Add((Control)(object)val2);
				((UltraControlBase)val2).Update();
				if (num + ((Control)(object)val2).Width > ((Control)(object)val).Width)
				{
					num2 += ((Control)(object)val2).Height;
					num = 8;
				}
				((Control)(object)val2).Left = num;
				((Control)(object)val2).Top = num2;
				((Control)(object)val2).Width = (((Control)(object)val).Width - GlobalVariables.ScrollWidth) / 7;
				num += ((Control)(object)val2).Width;
			}
		}
	}

	public void btnGroub_Click(object sender, EventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_121a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1221: Expected O, but got Unknown
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b9: Expected O, but got Unknown
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d1: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_11b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c1: Expected O, but got Unknown
		//IL_11cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d9: Expected O, but got Unknown
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Expected O, but got Unknown
		((Control)(object)pnlItems).Visible = false;
		((Control)(object)pnlItems.ClientArea).Controls.Clear();
		string[] array = ((Control)(UltraButton)sender).Tag.ToString().Split(',');
		string text = array[0].ToString();
		bool flag = Convert.ToBoolean(array[1].ToString());
		bool flag2 = bool.Parse(array[2].ToString());
		if (flag)
		{
			if (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString()) && CheckID != 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن إضافة صنف تمت طباعة الشيك" : "Cannot Add this Item Because Check is Printed");
				return;
			}
			if (flag2)
			{
				frmPOSChecksQtyDiscountsoffers frmPOSChecksQtyDiscountsoffers2 = new frmPOSChecksQtyDiscountsoffers(int.Parse(text), dtItemsAndGroups);
				frmPOSChecksQtyDiscountsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
				frmPOSChecksQtyDiscountsoffers2.Location = new Point(0, 0);
				frmPOSChecksQtyDiscountsoffers2.ShowDialog(this);
				if (frmPOSChecksQtyDiscountsoffers2.OfferID != 0 && !frmPOSChecksQtyDiscountsoffers2.Cancel)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("ItemID", typeof(int));
					dataTable.Columns.Add("UnitID", typeof(int));
					dataTable.Columns.Add("ColorID", typeof(int));
					dataTable.Columns.Add("ItemSizeID", typeof(int));
					dataTable.Columns.Add("Qty", typeof(decimal));
					dataTable.Columns.Add("OfferID", typeof(int));
					dataTable.Columns.Add("Price", typeof(decimal));
					for (int i = 0; i < frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows.Count; i++)
					{
						DataRow dataRow = dtItemsAndGroups.Select("ItemID=" + frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["ItemID"].ToString())[0];
						dataTable.Rows.Add(frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["ItemID"], frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["UnitID"], frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["ColorID"], frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["ItemSizeID"], frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["Qty"], frmPOSChecksQtyDiscountsoffers2.dtOffersItems.Rows[i]["OfferID"], dataRow["Price"]);
					}
					DataView dataView = new DataView(dataTable);
					if (frmPOSChecksQtyDiscountsoffers2.LowestPrice)
					{
						dataView.Sort = "Price ASC";
					}
					else if (frmPOSChecksQtyDiscountsoffers2.HighestPrice)
					{
						dataView.Sort = "Price Desc";
					}
					decimal num = default(decimal);
					for (int j = 0; j < dataView.Count; j++)
					{
						DataRow dataRow2 = dtItemsAndGroups.Select("ItemID=" + dataView[j]["ItemID"].ToString())[0];
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dataView[j]["ItemID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dataView[j]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataView[j]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dataView[j]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dataView[j]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow2["TaxID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow2["StoreID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmPOSChecksQtyDiscountsoffers2.OfferID;
						if (frmPOSChecksQtyDiscountsoffers2.ForAll)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmPOSChecksQtyDiscountsoffers2.DiscountRatio;
						}
						else if (frmPOSChecksQtyDiscountsoffers2.HighestPrice && num < frmPOSChecksQtyDiscountsoffers2.QtyDiscount && dataView.Count > 0)
						{
							if (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView[j]["ItemID"].ToString()))
							{
								((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmPOSChecksQtyDiscountsoffers2.DiscountRatio;
								++num;
							}
						}
						else if (frmPOSChecksQtyDiscountsoffers2.LowestPrice && num < frmPOSChecksQtyDiscountsoffers2.QtyDiscount && dataView.Count > 0 && int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()) == int.Parse(dataView[j]["ItemID"].ToString()))
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmPOSChecksQtyDiscountsoffers2.DiscountRatio;
							++num;
						}
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					CalculateGoss();
					CalculateTotalsTax();
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
			else
			{
				frmPOSChecksGiftsoffers frmPOSChecksGiftsoffers2 = new frmPOSChecksGiftsoffers(int.Parse(text), dtItemsAndGroups);
				frmPOSChecksGiftsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
				frmPOSChecksGiftsoffers2.Location = new Point(0, 0);
				frmPOSChecksGiftsoffers2.ShowDialog(this);
				dtOffersItems = frmPOSChecksGiftsoffers2.dtOffersItems;
				dtOffersGifts = frmPOSChecksGiftsoffers2.dtOffersGifts;
				OfferID = frmPOSChecksGiftsoffers2.OfferID;
				DiscountRatio = frmPOSChecksGiftsoffers2.DiscountRatio;
				if (frmPOSChecksGiftsoffers2.OfferID != 0 && !frmPOSChecksGiftsoffers2.Cancel)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
					for (int k = 0; k < frmPOSChecksGiftsoffers2.dtOffersItems.Rows.Count; k++)
					{
						DataRow dataRow3 = dtItemsAndGroups.Select("ItemID=" + frmPOSChecksGiftsoffers2.dtOffersItems.Rows[k]["ItemID"].ToString())[0];
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
						((UltraGridBase)ULGData).ActiveRow.Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = frmPOSChecksGiftsoffers2.dtOffersItems.Rows[k]["ItemID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmPOSChecksGiftsoffers2.dtOffersItems.Rows[k]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmPOSChecksGiftsoffers2.dtOffersItems.Rows[k]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmPOSChecksGiftsoffers2.dtOffersItems.Rows[k]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmPOSChecksGiftsoffers2.dtOffersItems.Rows[k]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow3["TaxID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow3["StoreID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmPOSChecksGiftsoffers2.OfferID;
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = 0;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString());
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					for (int l = 0; l < frmPOSChecksGiftsoffers2.dtOffersGifts.Rows.Count; l++)
					{
						DataRow dataRow4 = dtItemsAndGroups.Select("ItemID=" + frmPOSChecksGiftsoffers2.dtOffersGifts.Rows[l]["ItemID"].ToString())[0];
						((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
						((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
						((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
						((UltraGridBase)ULGData).ActiveRow.Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = frmPOSChecksGiftsoffers2.dtOffersGifts.Rows[l]["ItemID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = frmPOSChecksGiftsoffers2.dtOffersGifts.Rows[l]["Qty"];
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = frmPOSChecksGiftsoffers2.dtOffersGifts.Rows[l]["UnitID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = frmPOSChecksGiftsoffers2.dtOffersGifts.Rows[l]["ColorID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = frmPOSChecksGiftsoffers2.dtOffersGifts.Rows[l]["ItemSizeID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow4["TaxID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow4["StoreID"];
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = frmPOSChecksGiftsoffers2.OfferID;
						((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = frmPOSChecksGiftsoffers2.DiscountRatio;
						((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow4["Price"].ToString()) - decimal.Parse(dataRow4["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
						((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
						CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					}
					CalculateGoss();
					CalculateTotalsTax();
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
					ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
				}
			}
		}
		else
		{
			DataView dataView2 = new DataView(dtItemsAndGroups);
			dataView2.RowFilter = "IsMain=0 And ParentID=" + text;
			dataView2.ToTable();
			int num2 = 8;
			int num3 = 6;
			for (int m = 0; m < dataView2.Count; m++)
			{
				UltraButton val = new UltraButton();
				((Control)(object)val).Click += btnItems_Click;
				((Control)(object)val).Tag = dataView2[m]["ItemID"].ToString();
				((Control)(object)val).Text = dataView2[m]["ItemName"].ToString();
				((Control)(object)val).Height += 40;
				((Control)(object)pnlItems.ClientArea).Controls.Add((Control)(object)val);
				((UltraControlBase)val).Update();
				if (num2 + ((Control)(object)val).Width > ((Control)(object)pnlItems).Width)
				{
					num3 += ((Control)(object)val).Height;
					num2 = 8;
				}
				((Control)(object)val).Left = num2;
				((Control)(object)val).Top = num3;
				((Control)(object)val).Width = (((Control)(object)pnlItems).Width - GlobalVariables.ScrollWidth) / 6;
				num2 += ((Control)(object)val).Width;
			}
		}
		((Control)(object)pnlItems).Visible = true;
	}

	public void btnItems_Click(object sender, EventArgs e)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Expected O, but got Unknown
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Expected O, but got Unknown
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Expected O, but got Unknown
		//IL_0d7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d87: Expected O, but got Unknown
		//IL_0dc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd0: Expected O, but got Unknown
		//IL_0dde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de8: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			DataRow dataRow = dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value)[0];
			if (int.Parse(((Control)(UltraButton)sender).Tag.ToString()) == int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) && (dataRow["AccessoriesCount"] == DBNull.Value || int.Parse(dataRow["AccessoriesCount"].ToString()) == 0) && (dataRow["AdditionalCount"] == DBNull.Value || int.Parse(dataRow["AdditionalCount"].ToString()) == 0))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				if (bool.Parse(dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value)[0]["IsWeight"].ToString()) || bool.Parse(dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value)[0]["UsePOSNumPad"].ToString()))
				{
					frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGData).Rows[i].Cells["Qty"], ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
					frmDecimal2.ShowDialog(this);
				}
				else
				{
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + 1m;
				}
				CheckLog = CheckLog + " اضافة صنف X_" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AdditionalPrice"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateTotalsTax();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
		}
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode -= SelectFullRow;
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((!Adding && !Updating) ? 2 : 6);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = ((Control)(UltraButton)sender).Tag;
		DataRow dataRow2 = dtItemsAndGroups.Select(" IsOffer = 0 and ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataRow2["Price"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["RoomDiscountValue"].Value = dataRow2["RoomDiscountValue"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow2["TaxID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow2["StoreID"];
		if (bool.Parse(dataRow2["IsWeight"].ToString()) || bool.Parse(dataRow2["UsePOSNumPad"].ToString()))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 0;
			frmDecimal frmDecimal3 = new frmDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"], ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			frmDecimal3.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal3.ShowDialog(this);
		}
		else
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
		}
		CheckLog = CheckLog + " اضافة صنف X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم   X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		if (dataRow2["AccessoriesCount"] != DBNull.Value && int.Parse(dataRow2["AccessoriesCount"].ToString()) > 0)
		{
			int accessoriescount = int.Parse(dataRow2["AccessoriesCount"].ToString());
			bool enforceAccessories = bool.Parse(dataRow2["EnforceAccessories"].ToString());
			frmChecksDetailsAccessories frmChecksDetailsAccessories2 = new frmChecksDetailsAccessories(accessoriescount, int.Parse(dataRow2["ItemID"].ToString()), int.Parse(dataRow2["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()), canedit: true, enforceAccessories);
			frmChecksDetailsAccessories2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmChecksDetailsAccessories2.lblTitle).Text = (GlobalVariables.IsArabic ? "الملحقات" : "Items Accessories");
			frmChecksDetailsAccessories2.dtChecksDetailsAccessories = null;
			frmChecksDetailsAccessories2.dtChecksDetailsAccessories = dtChecksDetailsAccessories.Clone();
			DataView dataView = new DataView(dtChecksDetailsAccessories);
			dataView.RowFilter = " CheckDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString();
			DataTable dataTable = dataView.ToTable();
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				frmChecksDetailsAccessories2.dtChecksDetailsAccessories.ImportRow(dataTable.Rows[j]);
			}
			frmChecksDetailsAccessories2.ShowDialog(this);
			if (!frmChecksDetailsAccessories2.Cancel)
			{
				for (int k = 0; k < dtChecksDetailsAccessories.Rows.Count; k++)
				{
					if (int.Parse(dtChecksDetailsAccessories.Rows[k]["CheckDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()))
					{
						dtChecksDetailsAccessories.Rows[k].Delete();
						dtChecksDetailsAccessories.AcceptChanges();
						k--;
					}
				}
				for (int l = 0; l < frmChecksDetailsAccessories2.dtChecksDetailsAccessories.Rows.Count; l++)
				{
					dtChecksDetailsAccessories.ImportRow(frmChecksDetailsAccessories2.dtChecksDetailsAccessories.Rows[l]);
				}
			}
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
		CalculateGoss();
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalculateTotalsTax();
		if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
		}
		if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
		}
		int index = ((UltraGridBase)ULGData).ActiveRow.Index;
		((UltraGridBase)ULGData).ActiveRow.Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).Rows[index].Activate();
		((Control)(object)ULGData).Enter += ULGData_Enter;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode += SelectFullRow;
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void btnPersonCount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtPersonCount, ((Control)(object)txtPersonCount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtPersonCount).Location.X + frmDecimal2.Width, ((Control)(object)txtPersonCount).Location.Y + ((Control)(object)txtPersonCount).Height);
		frmDecimal2.Show();
	}

	public string GetTableNo()
	{
		string text = "";
		if (dtcheckTable.Rows.Count > 0)
		{
			for (int i = 0; i < dtcheckTable.Rows.Count; i++)
			{
				text += ((i == 0) ? dtcheckTable.Rows[i]["TableCode"].ToString() : ("," + dtcheckTable.Rows[i]["TableCode"].ToString()));
			}
		}
		return text;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString()) && CheckID != 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن إضافة صنف تمت طباعة الشيك" : "Cannot Add this Item Because Check is Printed");
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtItemsAndGroups.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" && int.Parse(dtItemsAndGroups.Select(" ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["AccessoriesCount"].ToString()) > 0)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ColorID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ItemSizeID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["CheckDetailID"].Value = ++newID;
			e.Row.Cells["Comments"].Value = (GlobalVariables.IsArabic ? "ملاحظات" : "Comments");
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["ServiceChargeAmount"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m;
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString())) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		}
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (CheckID != 0)
		{
			if (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لايمكن حذف هذا الصنف لانه تمت طباعة  الشيك", "Cannot Delete this Item Because it is Printed");
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
			for (int i = 0; i < e.Rows.Length; i++)
			{
				if (decimal.Parse(e.Rows[i].Cells["SentQty"].Value.ToString()) > 0m)
				{
					GlobalVariables.InformationMB.Show("لايمكن حذف هذا الصنف لانه تم إرساله الى طابعة التجهيز", "Cannot Delete this Item Because it is Sended to Preparation Printer");
					((CancelEventArgs)(object)e).Cancel = true;
					return;
				}
			}
		}
		ArOfferIDs.Clear();
		for (int j = 0; j < e.Rows.Length; j++)
		{
			if (e.Rows[j].Cells["OfferID"].Value != DBNull.Value)
			{
				ArOfferIDs.Add(e.Rows[j].Cells["OfferID"].Value);
			}
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		bool flag = false;
		for (int k = 0; k < e.Rows.Length; k++)
		{
			if (int.Parse(e.Rows[k].Cells["CheckDetailID"].Value.ToString()) <= -100000 || int.Parse(e.Rows[k].Cells["CheckDetailID"].Value.ToString()) > -1)
			{
				flag = true;
				break;
			}
		}
		frmPOSComments frmPOSComments2 = new frmPOSComments(GlobalVariables.IsArabic ? "ملاحظات" : "Notes", _IsInt: false, _IsNumeric: false);
		if (flag)
		{
			frmPOSComments2.WindowState = FormWindowState.Normal;
			frmPOSComments2.ShowDialog(this);
			if (frmPOSComments2.Value == "")
			{
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
			for (int l = 0; l < e.Rows.Length; l++)
			{
				CheckLog = CheckLog + " حذف صنف X_" + e.Rows[l].Cells["ItemID"].Text + "X_ كمية  X_" + e.Rows[l].Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			}
		}
		for (int m = 0; m < e.Rows.Length; m++)
		{
			if ((int.Parse(e.Rows[m].Cells["CheckDetailID"].Value.ToString()) <= -100000 || int.Parse(e.Rows[m].Cells["CheckDetailID"].Value.ToString()) > -1) && frmPOSComments2 != null)
			{
				dtDeletedItems.Rows.Add(e.Rows[m].Cells["CheckDetailID"].Value.ToString(), e.Rows[m].Cells["ItemID"].Value.ToString(), e.Rows[m].Cells["SentQty"].Value.ToString(), frmPOSComments2.Value);
			}
			for (int n = 0; n < dtChecksDetailsAccessories.Rows.Count; n++)
			{
				if (int.Parse(dtChecksDetailsAccessories.Rows[n]["CheckDetailID"].ToString()) == int.Parse(e.Rows[m].Cells["CheckDetailID"].Value.ToString()))
				{
					dtChecksDetailsAccessories.Rows[n].Delete();
					dtChecksDetailsAccessories.AcceptChanges();
					n--;
				}
			}
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		ULGData.AfterRowsDeleted -= ULGData_AfterRowsDeleted;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value && ArOfferIDs.Contains(((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value))
			{
				ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				((UltraGridBase)ULGData).Rows[i].Delete(false);
				ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				i--;
			}
		}
		CalculateGoss();
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[j]);
		}
		CalculateTotalsTax();
		ULGData.AfterRowsDeleted += ULGData_AfterRowsDeleted;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataRow["Price"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["RoomDiscountValue"].Value = dataRow["RoomDiscountValue"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["StoreID"];
			CheckLog = CheckLog + " اختيار صنف X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
			CalculateGoss();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
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
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن نفص الكمية عن الكمية المرسلة للتجهيز" : "Quantity Cannot Decrease thanSent Quantity");
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value;
		}
	}

	private void ULGData_ClickCell(object sender, ClickCellEventArgs e)
	{
		if ((Adding || Updating) && ((!(((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice") && !(((KeyedSubObjectBase)e.Cell.Column).Key == "TotalPrice")) || e.Cell.Row.Cells["ItemID"].Value == DBNull.Value || bool.Parse(dtItemsAndGroups.Select(" ItemID =" + e.Cell.Row.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString())) && ((((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" && drMaster != null && !bool.Parse(drMaster["IsPrinted"].ToString()) && e.Cell.Row.Cells["OfferID"].Value == DBNull.Value) || CheckID == 0))
		{
			frmDecimal frmDecimal2 = new frmDecimal(e.Cell, e.Cell.Value.ToString());
			frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal2.ShowDialog(this);
			CheckLog = CheckLog + "  تعديل كمية صنف  X_" + e.Cell.Row.Cells["ItemID"].Text + "X_ كمية  X_" + e.Cell.Row.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		}
	}

	private decimal CalculateGrossItemUnderDiscount()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtItemsAndGroups != null && dtItemsAndGroups.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value || bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["ExcludeCheckDiscount"].ToString())))
			{
				result += 0m;
			}
			else
			{
				result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
			}
		}
		return result;
	}

	private decimal CalculateNetItemWithoutDiscount()
	{
		decimal result = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (dtItemsAndGroups != null && dtItemsAndGroups.Rows.Count > 0 && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value != DBNull.Value && (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value != DBNull.Value || bool.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["ExcludeCheckDiscount"].ToString())))
			{
				result += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ServiceChargeAmount"].Value.ToString());
			}
		}
		return result;
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		decimal num2 = CalculateGrossItemUnderDiscount();
		((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
		if (num2 > 0m)
		{
			((Control)(object)txtDiscountValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtDiscountValue).Text = "0";
		}
		((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
		if (AdditionalDiscountIncludeTax)
		{
			((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * (num2 - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "" || ((Control)(object)txtServiceChargeValue).Text == ".") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else if (num2 > 0m)
		{
			((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtDiscountValue2).Text = "0";
		}
		((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (((Control)(object)txtDiscountRatio).Text != "" && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscountRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "." && Row.Cells["OfferID"].Value == DBNull.Value && Row.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItemsAndGroups.Select("ItemID=" + Row.Cells["ItemID"].Value.ToString())[0]["ExcludeCheckDiscount"].ToString()))
		{
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m;
		}
		else
		{
			Row.Cells["DisCount"].Value = 0;
		}
		Row.Cells["ServiceChargeAmount"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString())) * decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m;
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["ServiceChargeAmount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
	}

	private void CalculateRowActualUnitSalesPrice(UltraGridRow Row)
	{
		Row.Cells["ActualUnitSalesPrice"].Value = ((decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) == 0m) ? 0m : ((decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * (decimal.Parse(((Control)(object)txtServiceChargeValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text)) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString())));
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TaxValue"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ServiceChargeAmount"].Value.ToString());
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtServiceChargeValue).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateNetTotals();
	}

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "" || ((Control)(object)txtServiceChargeValue).Text == ".") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0.0)
		{
			decimal num = decimal.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString());
			decimal num2 = Convert.ToDecimal(((Control)(object)txtNetprice).Text) % num;
			decimal num3 = num / 2m;
			decimal num4 = num - num2;
			if (num2 < num3)
			{
				((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRoundingValue).Text = decimal.Parse((num2 * -1m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + num4).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRoundingValue).Text = decimal.Parse(num4.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
		RefreshCheckFrontScreen(((Control)(object)txtNetprice).Text);
	}

	public void RefreshCheckFrontScreen(string NetPrice)
	{
		try
		{
			if (frmMain2010.frmCheckScreen != null)
			{
				frmMain2010.frmCheckScreen.RefreshCheckFrontScreen(dtDetails, dvItems.ToTable(), NetPrice);
			}
		}
		catch
		{
		}
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
	}

	public void AddItemInGid()
	{
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Expected O, but got Unknown
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce3: Expected O, but got Unknown
		//IL_0d22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2c: Expected O, but got Unknown
		if (dtItemsAndGroups.Select("IsMain=0 And ItemBarCode = '" + ((Control)(object)txtBarCode).Text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		int num = int.Parse(dtItemsAndGroups.Select(" ItemBarCode = '" + ((Control)(object)txtBarCode).Text + "'")[0]["ItemID"].ToString());
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) == num && (dtItemsAndGroups.Select("ItemID =" + num)[0]["AccessoriesCount"] == DBNull.Value || int.Parse(dtItemsAndGroups.Select("ItemID =" + num)[0]["AccessoriesCount"].ToString()) == 0))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				if (bool.Parse(dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value)[0]["IsWeight"].ToString()))
				{
					frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"], ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
					frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
					frmDecimal2.ShowDialog(this);
				}
				else
				{
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + 1m;
				}
				CheckLog = CheckLog + " ذيادة كمية صنف باركود X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
				((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AdditionalPrice"].Value.ToString());
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
				CalculateTotalsTax();
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				return;
			}
		}
		((Control)(object)ULGData).Enter -= ULGData_Enter;
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode -= SelectFullRow;
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtItemsAndGroups.Select(" ItemBarCode = '" + ((Control)(object)txtBarCode).Text + "'")[0]["ItemID"].ToString();
		DataRow dataRow = dtItemsAndGroups.Select(" IsOffer = 0 and  ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["StoreID"];
		if (bool.Parse(dataRow["IsWeight"].ToString()))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 0;
			frmDecimal frmDecimal3 = new frmDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"], ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			frmDecimal3.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal3.ShowDialog(this);
		}
		else
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
		}
		CheckLog = CheckLog + " اضافة صنف باركود X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		if (dataRow["AccessoriesCount"] != DBNull.Value && int.Parse(dataRow["AccessoriesCount"].ToString()) > 0)
		{
			int accessoriescount = int.Parse(dataRow["AccessoriesCount"].ToString());
			bool enforceAccessories = bool.Parse(dataRow["EnforceAccessories"].ToString());
			frmChecksDetailsAccessories frmChecksDetailsAccessories2 = new frmChecksDetailsAccessories(accessoriescount, int.Parse(dataRow["ItemID"].ToString()), int.Parse(dataRow["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()), canedit: true, enforceAccessories);
			frmChecksDetailsAccessories2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmChecksDetailsAccessories2.lblTitle).Text = (GlobalVariables.IsArabic ? "الملحقات" : "Items Accessories");
			frmChecksDetailsAccessories2.dtChecksDetailsAccessories = null;
			frmChecksDetailsAccessories2.dtChecksDetailsAccessories = dtChecksDetailsAccessories.Clone();
			DataView dataView = new DataView(dtChecksDetailsAccessories);
			dataView.RowFilter = " CheckDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString();
			DataTable dataTable = dataView.ToTable();
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				frmChecksDetailsAccessories2.dtChecksDetailsAccessories.ImportRow(dataTable.Rows[j]);
			}
			frmChecksDetailsAccessories2.ShowDialog(this);
			if (!frmChecksDetailsAccessories2.Cancel)
			{
				for (int k = 0; k < dtChecksDetailsAccessories.Rows.Count; k++)
				{
					if (int.Parse(dtChecksDetailsAccessories.Rows[k]["CheckDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()))
					{
						dtChecksDetailsAccessories.Rows[k].Delete();
						dtChecksDetailsAccessories.AcceptChanges();
						k--;
					}
				}
				for (int l = 0; l < frmChecksDetailsAccessories2.dtChecksDetailsAccessories.Rows.Count; l++)
				{
					dtChecksDetailsAccessories.ImportRow(frmChecksDetailsAccessories2.dtChecksDetailsAccessories.Rows[l]);
				}
			}
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = dataRow["Price"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["RoomDiscountValue"].Value = dataRow["RoomDiscountValue"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
		CalculateGoss();
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalculateTotalsTax();
		if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
		((Control)(object)ULGData).Enter += ULGData_Enter;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode += SelectFullRow;
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
	}

	private void txtPersonCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void btnPlus_Click(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value == DBNull.Value && CheckID != 0 && drMaster != null && drMaster["IsPrinted"].Equals(false))
		{
			ULGData.ActiveCell = ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + 1.0;
			CheckLog = CheckLog + " ذيادة كمية صنف  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		}
	}

	private void btnSubtract_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value == DBNull.Value && CheckID != 0 && drMaster != null && drMaster["IsPrinted"].Equals(false) && double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 1.0)
			{
				ULGData.ActiveCell = ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"];
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) - 1.0;
				CheckLog = CheckLog + " نقص كمية صنف  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			}
			if (((UltraGridBase)ULGData).ActiveRow != null && CheckID != 0 && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) < decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن نفص الكمية عن الكمية المرسلة للتجهيز" : "Quantity Cannot Decrease thanSent Quantity");
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value;
			}
		}
	}

	private void btnCloseCheck_Click(object sender, EventArgs e)
	{
		//IL_14d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14df: Expected O, but got Unknown
		//IL_151e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1528: Expected O, but got Unknown
		//IL_1774: Unknown result type (might be due to invalid IL or missing references)
		//IL_177e: Expected O, but got Unknown
		//IL_17bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c7: Expected O, but got Unknown
		//IL_1b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4e: Expected O, but got Unknown
		//IL_1b8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b97: Expected O, but got Unknown
		//IL_1db3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dbd: Expected O, but got Unknown
		//IL_1dfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e06: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		if (dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["EnforceCaptainOrderSelection"].ToString()) && cboCaptainOrder.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الكابتن" : "Please Select Captain Order");
			((TextEditorControlBase)cboCaptainOrder).Focus();
			return;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return;
		}
		if (drMaster != null && bool.Parse(drMaster["Closed"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "الشيك مغلق غير قابل للتعديل" : "Check Is Closed Cannot Be Updated");
			return;
		}
		if (((UltraToggleEditorBase)chkMaxCharge).Checked && (((Control)(object)txtMaxChargeValue).Text == "" || decimal.Parse(((Control)(object)txtMaxChargeValue).Text) == 0m))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال قيمة الحد الاقصى" : "Please Enter Fixed Charge");
			((TextEditorControlBase)txtMaxChargeValue).Focus();
			return;
		}
		if (((UltraToggleEditorBase)chkMinCharge).Checked && ((UltraToggleEditorBase)chkMaxCharge).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "Min Charge Method Cannot Be With Fixed Charge Method " : "Min Charge Method Cannot Be With Fixed Charge Method ");
			return;
		}
		((UltraGridBase)ULGData).UpdateData();
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalsTax();
		frmClosingCheck frmClosingCheck2 = new frmClosingCheck(bool.Parse(dtRoomSettingsData.Rows[0]["AdditionalDiscountWithoutTax"].ToString()), (cboClient.SelectedIndex != -1) ? int.Parse(((TextEditorControlBase)cboClient).Value.ToString()) : 0, (cboClient.SelectedIndex == -1) ? DBNull.Value : dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"], bool.Parse(dtPOSDefaultData.Rows[0]["GetPOSBalanceOnline"].ToString()));
		((UltraToggleEditorBase)frmClosingCheck2.chkCash).Checked = !bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString());
		((Control)(object)frmClosingCheck2.txtOnAccountAmount).Text = "0";
		((UltraToggleEditorBase)frmClosingCheck2.chkOnAccount).Checked = bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString());
		((UltraToggleEditorBase)frmClosingCheck2.chkVisa).Checked = false;
		if (bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString()))
		{
			UltraCheckEditor chkCash = frmClosingCheck2.chkCash;
			bool enabled = (((Control)(object)frmClosingCheck2.chkVisa).Enabled = false);
			((Control)(object)chkCash).Enabled = enabled;
			((Control)(object)frmClosingCheck2.txtOnAccountAmount).Text = ((Control)(object)txtNetprice).Text;
			((Control)(object)frmClosingCheck2.txtCashAmount).Enabled = false;
		}
		((TextEditorControlBase)frmClosingCheck2.cboVisaType).Value = DBNull.Value;
		((Control)(object)frmClosingCheck2.txtVisaNo).Text = "";
		((Control)(object)frmClosingCheck2.txtGrossValue).Text = ((Control)(object)txtGrossValue).Text;
		((Control)(object)frmClosingCheck2.txtDiscountValue).Text = ((Control)(object)txtDiscountValue).Text;
		((Control)(object)frmClosingCheck2.txtDiscountRatio).Text = ((Control)(object)txtDiscountRatio).Text;
		((Control)(object)frmClosingCheck2.txtDiscountValue2).Text = ((Control)(object)txtDiscountValue2).Text;
		((Control)(object)frmClosingCheck2.txtDiscountRatio2).Text = ((Control)(object)txtDiscountRatio2).Text;
		((Control)(object)frmClosingCheck2.txtServiceChargeValue).Text = ((Control)(object)txtServiceChargeValue).Text;
		((Control)(object)frmClosingCheck2.txtTaxTotalValue).Text = ((Control)(object)txtTaxTotalValue).Text;
		if ((((UltraToggleEditorBase)chkMinCharge).Checked || ((UltraToggleEditorBase)chkMaxCharge).Checked) && (((Control)(object)txtPersonCount).Text == "" || int.Parse(((Control)(object)txtPersonCount).Text) < 1))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال عدد الاشخاص" : "Please Enter Person Count");
			((TextEditorControlBase)txtPersonCount).Focus();
			return;
		}
		decimal num = (((UltraToggleEditorBase)chkMinCharge).Checked ? (decimal.Parse(((Control)(object)txtPersonCount).Text) * decimal.Parse(((Control)(object)txtMinChargeValue).Text)) : 0m);
		decimal num2 = (((UltraToggleEditorBase)chkMaxCharge).Checked ? (decimal.Parse(((Control)(object)txtPersonCount).Text) * decimal.Parse(((Control)(object)txtMaxChargeValue).Text)) : 0m);
		decimal num3 = CalculateNetItemWithoutDiscount();
		decimal num4 = default(decimal);
		if (decimal.Parse(((Control)(object)txtDiscountRatio).Text) != 100m && decimal.Parse(((Control)(object)txtDiscountRatio2).Text) != 100m && decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDiscountValue).Text) + decimal.Parse(((Control)(object)txtDiscountValue2).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) < num)
		{
			object obj = dtItemsAndGroups.Select(" ItemID= " + dtRoomData.Rows[0]["MinChargeItemID"])[0]["TaxID"];
			decimal num5 = default(decimal);
			if (obj != DBNull.Value)
			{
				num5 = decimal.Parse(dtTaxs.Select(" TaxID= " + obj.ToString())[0]["TaxPercent"].ToString()) / 100m;
			}
			decimal num6 = default(decimal);
			if (AdditionalDiscountWithoutTax && decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m)
			{
				decimal num7 = ((num - num3) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) - (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) - num3 + decimal.Parse(((Control)(object)txtDiscountValue2).Text))) / (1m + decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m + num5);
				decimal num8 = num7 + decimal.Parse(((Control)(object)txtGrossValue).Text);
				decimal num9 = num8 * (decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m);
				decimal num10 = decimal.Parse(((Control)(object)txtTaxTotalValue).Text) + num7 * num5;
				decimal num11 = num8 * (decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m);
				num4 = num8 + num9 + num10 - num11;
				if (dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0.0)
				{
					decimal num12 = decimal.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString());
					decimal num13 = Convert.ToDecimal(num4.ToString()) % num12;
					decimal num14 = num12 / 2m;
					decimal num15 = num12 - num13;
					num6 = ((!(num13 < num14)) ? num15 : (num13 * -1m));
				}
				num4 = num8 + num9 + num10 - num11 + num6;
			}
		}
		if (num2 > 0m && decimal.Parse(((Control)(object)txtNetprice).Text) / (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) - decimal.Parse(((Control)(object)txtRoundingValue).Text) != num2)
		{
			object obj2 = dtItemsAndGroups.Select(" ItemID= " + dtRoomData.Rows[0]["MaxChargeItemID"])[0]["TaxID"];
			decimal num16 = default(decimal);
			if (obj2 != DBNull.Value)
			{
				num16 = decimal.Parse(dtTaxs.Select(" TaxID= " + obj2.ToString())[0]["TaxPercent"].ToString()) / 100m;
			}
			decimal num17 = default(decimal);
			if (AdditionalDiscountWithoutTax && decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m)
			{
				decimal num18 = (num2 * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) - (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) + decimal.Parse(((Control)(object)txtDiscountValue2).Text))) / ((1m + decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) * (1m + num16));
				decimal num19 = num18 + decimal.Parse(((Control)(object)txtGrossValue).Text);
				decimal num20 = num19 * (decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m);
				decimal num21 = decimal.Parse(((Control)(object)txtTaxTotalValue).Text) + num18 * num16;
				decimal num22 = num19 * (decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m);
				num4 = num19 + num20 + num21 - num22;
				if (dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && double.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0.0)
				{
					decimal num23 = decimal.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString());
					decimal num24 = Convert.ToDecimal(num4.ToString()) % num23;
					decimal num25 = num23 / 2m;
					decimal num26 = num23 - num24;
					num17 = ((!(num24 < num25)) ? num26 : (num24 * -1m));
				}
				num4 = num19 + num20 + num21 - num22 + num17;
			}
		}
		((Control)(object)frmClosingCheck2.txtNetprice).Text = ((Control)(object)txtNetprice).Text;
		if (((UltraToggleEditorBase)chkMaxCharge).Checked)
		{
			if (num2 > 0m && AdditionalDiscountWithoutTax && decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m)
			{
				((Control)(object)frmClosingCheck2.txtNetprice).Text = decimal.Parse(num4.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)frmClosingCheck2.txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) / (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) - decimal.Parse(((Control)(object)txtRoundingValue).Text) != num2) ? (num2 * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m)).ToString() : ((Control)(object)txtNetprice).Text).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
		else if (num > 0m && AdditionalDiscountWithoutTax && decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m)
		{
			((Control)(object)frmClosingCheck2.txtNetprice).Text = decimal.Parse(num4.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)frmClosingCheck2.txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDiscountValue).Text) + decimal.Parse(((Control)(object)txtDiscountValue2).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) >= num) ? ((Control)(object)txtNetprice).Text : ((num - num3) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m) + num3).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
		((Control)(object)frmClosingCheck2.txtCashAmount).Text = (bool.Parse(dtRoomData.Rows[0]["NetPriceIsCashDefault"].ToString()) ? ((Control)(object)frmClosingCheck2.txtNetprice).Text : "0");
		frmClosingCheck2.ShowDialog(this);
		if (frmClosingCheck2.Cancel)
		{
			return;
		}
		IsCash = ((UltraToggleEditorBase)frmClosingCheck2.chkCash).Checked;
		IsVisa = ((UltraToggleEditorBase)frmClosingCheck2.chkVisa).Checked;
		IsOnAccount = ((UltraToggleEditorBase)frmClosingCheck2.chkOnAccount).Checked;
		if (decimal.Parse(((Control)(object)txtDiscountRatio).Text) != 100m && decimal.Parse(((Control)(object)txtDiscountRatio2).Text) != 100m && decimal.Parse(((Control)(object)txtNetprice).Text) + decimal.Parse(((Control)(object)txtDiscountValue).Text) + decimal.Parse(((Control)(object)txtDiscountValue2).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) < num)
		{
			object obj3 = dtItemsAndGroups.Select(" ItemID= " + dtRoomData.Rows[0]["MinChargeItemID"])[0]["TaxID"];
			decimal num27 = default(decimal);
			if (obj3 != DBNull.Value)
			{
				num27 = decimal.Parse(dtTaxs.Select(" TaxID= " + obj3.ToString())[0]["TaxPercent"].ToString()) / 100m;
			}
			decimal num28 = decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m;
			decimal num29 = decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m;
			decimal num30 = decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m;
			decimal num31 = decimal.Parse(((Control)(object)txtTaxTotalValue).Text);
			decimal num32 = default(decimal);
			num32 = ((!AdditionalDiscountWithoutTax || !(decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m)) ? (((num - num3) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m) - (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) - num3)) / ((1m + decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m) * (1m + num27))) : (((num - num3) * (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) - (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) - num3 + decimal.Parse(((Control)(object)txtDiscountValue2).Text))) / (1m + decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m + num27)));
			((Control)(object)ULGData).Enter -= ULGData_Enter;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGData.AfterEnterEditMode -= SelectFullRow;
			ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
			ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtRoomData.Rows[0]["MinChargeItemID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItemsAndGroups.Select(" ItemID= " + dtRoomData.Rows[0]["MinChargeItemID"])[0]["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = obj3;
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = num32;
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalsTax();
			((Control)(object)ULGData).Enter += ULGData_Enter;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGData.AfterEnterEditMode += SelectFullRow;
			ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
			ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		}
		if (((UltraToggleEditorBase)chkMaxCharge).Checked && decimal.Parse(((Control)(object)txtDiscountRatio).Text) != 100m && decimal.Parse(((Control)(object)txtDiscountRatio2).Text) != 100m && decimal.Parse(((Control)(object)txtNetprice).Text) / (1m - decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m) / (1m - decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m) - decimal.Parse(((Control)(object)txtRoundingValue).Text) != num2)
		{
			DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + dtRoomData.Rows[0]["MaxChargeItemID"])[0];
			object obj4 = dataRow["TaxID"];
			decimal num33 = default(decimal);
			if (obj4 != DBNull.Value)
			{
				num33 = decimal.Parse(dtTaxs.Select(" TaxID= " + obj4.ToString())[0]["TaxPercent"].ToString()) / 100m;
			}
			decimal num34 = decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m;
			decimal num35 = decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m;
			decimal num36 = decimal.Parse(((Control)(object)txtDiscountRatio2).Text) / 100m;
			decimal num37 = decimal.Parse(((Control)(object)txtTaxTotalValue).Text);
			decimal num38 = default(decimal);
			num38 = ((!AdditionalDiscountWithoutTax || !(decimal.Parse(((Control)(object)txtDiscountValue2).Text) > 0m)) ? ((num2 * (1m - num35) * (1m - num36) - (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text))) / ((1m + num34) * (1m - num35) * (1m - num36) * (1m + num33))) : ((num2 * (1m - num35) - (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtRoundingValue).Text) + decimal.Parse(((Control)(object)txtDiscountValue2).Text))) / ((1m + num34) * (1m - num35) * (1m + num33))));
			((Control)(object)ULGData).Enter -= ULGData_Enter;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			ULGData.AfterEnterEditMode -= SelectFullRow;
			ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
			ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtRoomData.Rows[0]["MaxChargeItemID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = obj4;
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = num38;
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
			CalculateGoss();
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			CalculateTotalsTax();
			((Control)(object)ULGData).Enter += ULGData_Enter;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			ULGData.AfterEnterEditMode += SelectFullRow;
			ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
			ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		}
		CashAmount = decimal.Parse(((Control)(object)frmClosingCheck2.txtCashAmount).Text);
		OnAccountAmount = decimal.Parse(((Control)(object)frmClosingCheck2.txtOnAccountAmount).Text);
		VisaAmount = decimal.Parse(((Control)(object)frmClosingCheck2.txtVisaAmount).Text);
		VisatypeID = frmClosingCheck2.VisaTypeID;
		VisaNo = ((Control)(object)frmClosingCheck2.txtVisaNo).Text;
		PaidAmount = decimal.Parse(((Control)(object)frmClosingCheck2.txtPaidAmount).Text);
		RestAmount = decimal.Parse(((Control)(object)frmClosingCheck2.txtRestAmount).Text);
		CheckClosed = true;
		CheckLog = CheckLog + " تم اقفال الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		PrintCheck();
		CheckLog = CheckLog + " تم طباعة اقفال الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.POSClientsSearch(GlobalVariables.CurrentBranchID, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	public void FastPrintCheck()
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_POS_Checks '," + RowID + ",'," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataTable dataTable2 = Main.ExecuteQuery_DataTable(" Rep_POS_ChecksDetailsAdditionals '," + RowID + ",'," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataRow dataRow = dataTable.Rows[0];
		Font font = new Font("Times New Roman", 10f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 8f, FontStyle.Bold);
		Font font3 = new Font("Times New Roman", 8f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 7f, FontStyle.Regular);
		Font font5 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font6 = new Font("Times New Roman", 9f, FontStyle.Bold);
		Font font7 = new Font("Times New Roman", 10f, FontStyle.Bold);
		FastPrint instance = FastPrint.Instance;
		instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
		instance.GraphicsUnit = GraphicsUnit.Millimeter;
		instance.Margins = new Margins(0, 0, 0, 0);
		instance.OverallWidth = 70f;
		try
		{
			if (dtRoomSettingsData.Rows[0]["Logo"] != null)
			{
				Image image = GlobalFunctions.BinaryToImage((byte[])dtRoomSettingsData.Rows[0]["Logo"]);
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
		instance.AddTextCell(dataRow["RoomName"].ToString(), font, 1f, 7f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (!Convert.ToBoolean(dtRoomData.Rows[0]["PrintWithoutCheckNo"]))
		{
			instance.AddTextCell(dataRow["CheckNo"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("شيك رقم", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		instance.AddTextCell(((DateTime)dataRow["CheckDate"]).ToString("dd/MM/yyyy hh:mm:ss tt"), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("بتاريخ", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["UserName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("المستخدم", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		if (((Control)(object)txtTableNo).Text.Trim().Length > 0)
		{
			instance.AddTextCell(((Control)(object)txtTableNo).Text, font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("طاوله رقم", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["PersonCount"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["PersonCount"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("عدد الأشخاص", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["ClientName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["ClientName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("العميل", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["CaptainOrderName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["CaptainOrderName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("كابتن اوردر", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["DeliveryManName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["DeliveryManName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("الطيار", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["MobileNumber"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["MobileNumber"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("رقم المحمول", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["PhoneNumber"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["PhoneNumber"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("رقم التليفون", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["CityName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["CityName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("المحافظه", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["AreaName"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["AreaName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("المنطقه", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["Address"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["Address"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("العنوان", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["Notes"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["Notes"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("ملاحظات", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("إجمالي", font2, 0.18f, 5f);
		instance.AddTextCell("سعر الوحده", font2, 0.18f, 5f);
		instance.AddTextCell("الصنف", font2, 0.5f, 5f);
		instance.AddTextCell("الكميه", font2, 0.14f, 5f);
		instance.AcceptChanges();
		foreach (DataRow row in dataTable.Rows)
		{
			instance.AddTextCell(decimal.Parse(row["TotalPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.18f, 4f);
			instance.AddTextCell(decimal.Parse(row["UnitPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.18f, 4f);
			instance.AddTextCell(row["ItemName"].ToString(), font3, 0.5f, 4f);
			instance.AddTextCell(decimal.Parse(row["Qty"].ToString(), NumberStyles.Float).ToString("0.##"), font3, 0.14f, 4f);
			instance.AcceptChanges();
			try
			{
				if (dataTable2 != null)
				{
					DataRow[] array = dataTable2.Select("CheckDetailID = " + row["CheckDetailID"].ToString());
					foreach (DataRow dataRow3 in array)
					{
						instance.AddTextCell("إضافه", font4, 0.18f, 4f);
						instance.AddTextCell(decimal.Parse(dataRow3["UnitPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font4, 0.18f, 4f);
						instance.AddTextCell(dataRow3["ItemName"].ToString().TrimStart('*').TrimEnd('*'), font4, 0.5f, 4f);
						instance.AddTextCell(decimal.Parse(dataRow3["Qty"].ToString(), NumberStyles.Float).ToString("0.##"), font4, 0.14f, 4f);
						instance.AcceptChanges();
					}
				}
			}
			catch
			{
			}
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
		instance.AddTextCell(decimal.Parse(dataRow["GrossValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
		instance.AddTextCell("الإجمالي", font6, 0.5f, 5f, StringAlignment.Far);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("خصم", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["ServiceChargeValue"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["ServiceChargeValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("رسوم الخدمه", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["TaxTotalValue"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["TaxTotalValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("ض قيمة المضافة", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("خصم2", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["RoundingValue"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["RoundingValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("تقريب", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
		instance.AddTextCell(decimal.Parse(dataRow["NetPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: true, Color.Black, 0.5f);
		instance.AddTextCell("الصافي", font7, 0.5f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Black, 0.5f);
		instance.AcceptChanges();
		decimal num2 = decimal.Parse(dataRow["PersonCount"].ToString()) * decimal.Parse(dataRow["MinChargeValue"].ToString());
		if (num2 > decimal.Parse(dataRow["NetPrice"].ToString()))
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(num2.ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("الحد الأدنى", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["OnAccountAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["OnAccountAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("على الحساب", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["CashAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["CashAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("نقدي", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["VisaAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["VisaAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("فيزا", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["RestAmount"].ToString()) != 0m && decimal.Parse(dataRow["PaidAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["RestAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font6, 0.3f, 5f);
			instance.AddTextCell("المتبقى", font6, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 2f, DrawRectangle: false);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["OnAccountAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell("......................", font6, 0.5f, 5f);
			instance.AddTextCell("التوقيع :", font6, 0.3f, 5f);
			instance.AcceptChanges();
		}
		instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dtRoomData.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dtRoomData.Rows[0]["Message"].ToString(), font3, 1f, 6f, StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		instance.AddTextCell("Powered by Future Solutions : www.fs-scs.com", font3, 1f, 6f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		try
		{
			drMaster["IsPrinted"] = true;
			drMaster["PrintUserID"] = GlobalVariables.UserID;
			drMaster["PrintDate"] = DateTime.Now;
			SaveClose(Close: true, DisplaySentQtyMessage: false);
			CheckLog = CheckLog + " تم طباعة الشيك   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			if (CheckClosed)
			{
				instance.PrinterSettings.Copies = short.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString());
				if (instance.PrinterSettings.Copies > 0)
				{
					instance.Print();
				}
			}
			else
			{
				instance.PrinterSettings.Copies = short.Parse(dtRoomData.Rows[0]["PrintCheckCount"].ToString());
				instance.Print();
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
		}
	}

	public void PrintCheck()
	{
		((Control)(object)btnPrint).Enabled = false;
		if (!SaveClose(Close: false, DisplaySentQtyMessage: true) || !(RowID != ""))
		{
			return;
		}
		if (((UltraToggleEditorBase)chkIsInvoice).Checked)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_Invoice_A.rpt" : "Rep_POS_Invoice_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@CheckIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog(this);
			frmReporViwer2 = null;
			GlobalVariables.ReportDocument = null;
			drMaster["IsPrinted"] = true;
			drMaster["PrintUserID"] = GlobalVariables.UserID;
			drMaster["PrintDate"] = DateTime.Now;
			SaveClose(Close: true, DisplaySentQtyMessage: false);
		}
		else
		{
			DataRow[] array = dtReports.Select("ReportID=" + (dtRoomData.Rows[0]["DineInReportID"].Equals(DBNull.Value) ? "0" : dtRoomData.Rows[0]["DineInReportID"]));
			if (array.Length != 0 && array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "FastPrintDineInDetails")
			{
				FastPrintCheck();
				return;
			}
			ReportDocument reportDocument = new ReportDocument();
			if (array.Length != 0)
			{
				reportDocument.Load(GlobalVariables.ReportsPath + array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_Checks_A.rpt" : "Rep_POS_Checks_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@CheckIDs", "," + RowID + ",");
			reportDocument.SetParameterValue("@RoomID", RoomID.ToString());
			reportDocument.SetParameterValue("@TableCode", ((Control)(object)txtTableNo).Text);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				drMaster["IsPrinted"] = true;
				drMaster["PrintUserID"] = GlobalVariables.UserID;
				drMaster["PrintDate"] = DateTime.Now;
				SaveClose(Close: true, DisplaySentQtyMessage: false);
				CheckLog = CheckLog + " تم طباعة الشيك   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
				if (CheckClosed)
				{
					int num = int.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString());
					if (num > 0)
					{
						reportDocument.PrintToPrinter(int.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString()), collated: true, 0, 10000);
					}
				}
				else
				{
					reportDocument.PrintToPrinter(int.Parse(dtRoomData.Rows[0]["PrintCheckCount"].ToString()), collated: true, 0, 10000);
				}
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
			reportDocument.Dispose();
		}
		GC.Collect();
	}

	public override void btnPrintClick()
	{
		if (dtRoomData.Rows.Count <= 0 || !bool.Parse(dtRoomData.Rows[0]["PrintCheck"].ToString()))
		{
			return;
		}
		if (!bool.Parse(drMaster["IsPrinted"].ToString()) || CanMultiPrint)
		{
			PrintCheck();
			CheckLog = CheckLog + " تم طباعة الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			return;
		}
		frmCheckUserPrivilege frmCheckUserPrivilege2 = new frmCheckUserPrivilege("frmDineIn", "MultiPrint");
		frmCheckUserPrivilege2.WindowState = FormWindowState.Normal;
		frmCheckUserPrivilege2.ShowDialog(this);
		if (frmCheckUserPrivilege2.hasPrivilege)
		{
			PrintCheck();
			CheckLog = CheckLog + " تم طباعة الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		}
	}

	private void btnMerge_Click(object sender, EventArgs e)
	{
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Expected O, but got Unknown
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Expected O, but got Unknown
		//IL_09ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d4: Expected O, but got Unknown
		//IL_0a13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1d: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Expected O, but got Unknown
		MergedCheckIDs = ",";
		frmChecksMerge frmChecksMerge2 = new frmChecksMerge(int.Parse(RowID), RoomID, CheckLog);
		frmChecksMerge2.WindowState = FormWindowState.Normal;
		frmChecksMerge2.ShowDialog(this);
		MergedCheckIDs = frmChecksMerge2.MergedCheckIDs;
		UltraButton obj = btnMerge;
		bool enabled = (((Control)(object)btnSeparate).Enabled = frmChecksMerge2.MergedCheckIDs.Equals(","));
		((Control)(object)obj).Enabled = enabled;
		if (!(MergedCheckIDs != ","))
		{
			return;
		}
		bool flag2 = false;
		dtMergedCheckData = ChecksDetails.SelectByMergedCheckIDs(MergedCheckIDs);
		((Control)(object)txtPersonCount).Text = (decimal.Parse((dtMergedCheckData.Rows.Count > 0) ? dtMergedCheckData.Rows[0]["PersonCount"].ToString() : "0") + decimal.Parse(((Control)(object)txtPersonCount).Text)).ToString();
		for (int i = 0; i < dtMergedCheckData.Rows.Count; i++)
		{
			flag2 = false;
			DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + dtMergedCheckData.Rows[i]["ItemID"].ToString())[0];
			if (int.Parse(dtMergedCheckData.Rows[i]["CheckDetailID"].ToString()) == -1)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (dtMergedCheckData.Rows[i]["ItemID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[j].Cells["OfferID"].Value == DBNull.Value)
					{
						ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
						((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + decimal.Parse(dtMergedCheckData.Rows[i]["Qty"].ToString());
						((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["AdditionalPrice"].Value.ToString());
						((UltraGridBase)ULGData).Rows[j].Cells["SentQty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SentQty"].Value.ToString()) + decimal.Parse(dtMergedCheckData.Rows[i]["SentQty"].ToString());
						((UltraGridBase)ULGData).Rows[j].Cells["SentDate"].Value = dtMergedCheckData.Rows[i]["SentDate"];
						ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
						CalculateGoss();
						CalculateRow(((UltraGridBase)ULGData).Rows[j]);
						CalculateTotalsTax();
						flag2 = true;
						break;
					}
				}
			}
			if (!flag2 || int.Parse(dtMergedCheckData.Rows[i]["CheckDetailID"].ToString()) != -1)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				ULGData.AfterEnterEditMode -= SelectFullRow;
				ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
				ULGData.AfterRowInsert -= new RowEventHandler(ULGData_AfterRowInsert);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)((!Adding && !Updating) ? 2 : 6);
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = dtMergedCheckData.Rows[i]["CheckDetailID"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtMergedCheckData.Rows[i]["ItemID"].ToString();
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtMergedCheckData.Rows[i]["UnitID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dtItemsAndGroups.Select(" ItemID = " + dtMergedCheckData.Rows[i]["ItemID"].ToString())[0]["TaxID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dtItemsAndGroups.Select(" ItemID = " + dtMergedCheckData.Rows[i]["ItemID"].ToString())[0]["StoreID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = dtMergedCheckData.Rows[i]["Qty"];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dtMergedCheckData.Rows[i]["UnitPrice"].ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value = dtMergedCheckData.Rows[i]["SentQty"];
				((UltraGridBase)ULGData).ActiveRow.Cells["SentDate"].Value = dtMergedCheckData.Rows[i]["SentDate"];
				((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value = dtMergedCheckData.Rows[i]["OfferID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["OfferDiscountRatio"].Value = dtMergedCheckData.Rows[i]["OfferDiscountRatio"];
				((UltraGridBase)ULGData).ActiveRow.Cells["RoomDiscountValue"].Value = dtMergedCheckData.Rows[i]["RoomDiscountValue"];
				CalculateGoss();
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				CalculateTotalsTax();
				((Control)(object)ULGData).Enter += ULGData_Enter;
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				ULGData.AfterEnterEditMode += SelectFullRow;
				ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
				ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
			}
		}
		DataTable dataTable = ChecksDetailsAccessories.SelectByCheckIDs(MergedCheckIDs, GlobalVariables.IsArabic ? "1" : "0");
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			dtChecksDetailsAccessories.ImportRow(dataTable.Rows[k]);
		}
		int index = ((UltraGridBase)ULGData).ActiveRow.Index;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).Rows[index].Activate();
	}

	private void btnSeparate_Click(object sender, EventArgs e)
	{
		if (drMaster == null)
		{
			return;
		}
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select(" OfferID Is not null ").Length != 0)
		{
			GlobalVariables.InformationMB.Show("لايمكن تقسيم الشيك لوجود عروض", "Check Cannot Be Separated There Are offers In The Check");
			return;
		}
		frmChecksSeparate frmChecksSeparate2 = new frmChecksSeparate(drMaster, (DataTable)((UltraGridBase)ULGData).DataSource, dtPOSDefaultData, dvItems, dtUnits, dtTaxs, dtClients, TableID, dtRoomData.Rows[0], CheckLog);
		frmChecksSeparate2.WindowState = FormWindowState.Normal;
		frmChecksSeparate2.ShowDialog(this);
		if (!frmChecksSeparate2.Cancel)
		{
			Close();
		}
	}

	private void btnSent_Click(object sender, EventArgs e)
	{
		if (SaveClose(Close: false, DisplaySentQtyMessage: false))
		{
			SentItems();
			CheckLog = CheckLog + " تم ارسال الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			DisplayData();
		}
	}

	public void SentItems()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("PrinterName");
		dataTable.Columns.Add("ItemIDs");
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SentQty"].Value.ToString()))
			{
				CheckSentItems = true;
				if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SentQty"].Value.ToString()) > 0m)
				{
					SecondPrint = true;
				}
				if (dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["POSPrinter"].ToString() + "'").Length == 0)
				{
					dataTable.Rows.Add(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["POSPrinter"].ToString(), "," + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + ",");
				}
				else
				{
					dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["POSPrinter"].ToString() + "'")[0]["ItemIDs"] = dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["POSPrinter"].ToString() + "'")[0]["ItemIDs"].ToString() + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + ",";
				}
			}
		}
		if (dataTable.Rows.Count > 0)
		{
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				bool flag = false;
				foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
				{
					if (dataTable.Rows[j]["PrinterName"].ToString() == installedPrinter)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					GlobalVariables.InformationMB.Show(" الطابعة غير معرفة  \n  " + dataTable.Rows[j]["PrinterName"].ToString(), " printer " + dataTable.Rows[j]["PrinterName"].ToString() + " Is not Installed ");
					return;
				}
			}
			Main.StartBulkTrans(FromServer: false);
			try
			{
				ChecksDetails.TempPrint_Insert(RowID);
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				return;
			}
		}
		if (CheckSentItems)
		{
			if (!SentItemsToPickUp())
			{
				CheckSentItems = false;
				return;
			}
			CheckSentItems = false;
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			ReportDocument reportDocument = new ReportDocument();
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ChecksSendToPrinters_A.rpt" : "Rep_POS_ChecksSendToPrinters_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@CheckID", RowID);
			reportDocument.SetParameterValue("@ItemIDs", "," + dataTable.Rows[k]["ItemIDs"].ToString() + ",");
			reportDocument.SetParameterValue("@CheckIDs", "," + RowID + ",", "Rep_POS_ChecksDetailsAdditionals");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_POS_ChecksDetailsAdditionals");
			reportDocument.SetParameterValue("@TableCode", ((Control)(object)txtTableNo).Text);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.SetParameterValue("@CheckID", RowID, "Rep_POS_ChecksDetailsAccessoriesSendToPrinters");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_POS_ChecksDetailsAccessoriesSendToPrinters");
			reportDocument.PrintOptions.PrinterName = dataTable.Rows[k]["PrinterName"].ToString();
			bool flag2 = false;
			Main.StartBulkTrans(FromServer: false);
			try
			{
				ChecksDetails.UpdateSentQty(RowID, "," + dataTable.Rows[k]["ItemIDs"].ToString() + ",");
				if (!Main.Success)
				{
					flag2 = true;
				}
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا فى الوصلات و لم يتم الارسال برجاء الارسال مرة اخرى  " : "Error Occured Items Not Sent Please Sent Items Again ");
			}
			if (!flag2 && DataSaved)
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			reportDocument.Dispose();
			GC.Collect();
		}
	}

	public bool SentItemsToPickUp()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("PrinterName");
		dataTable.Columns.Add("ItemIDs");
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SentQty"].Value.ToString())))
			{
				continue;
			}
			CheckSentItems = true;
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SentQty"].Value.ToString()) > 0m)
			{
				SecondPrint = true;
			}
			if (dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["PickupPrinter"] != DBNull.Value)
			{
				if (dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["PickupPrinter"].ToString() + "'").Length == 0)
				{
					dataTable.Rows.Add(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["PickupPrinter"].ToString(), "," + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + ",");
				}
				else
				{
					dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["PickupPrinter"].ToString() + "'")[0]["ItemIDs"] = dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["PickupPrinter"].ToString() + "'")[0]["ItemIDs"].ToString() + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + ",";
				}
			}
		}
		if (dataTable.Rows.Count > 0)
		{
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				bool flag = false;
				foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
				{
					if (dataTable.Rows[j]["PrinterName"].ToString() == installedPrinter)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GlobalVariables.InformationMB.Show(" طابعة التجميع غير معرفة  \n  " + dataTable.Rows[j]["PrinterName"].ToString(), " Pickup printer " + dataTable.Rows[j]["PrinterName"].ToString() + " Is not Installed ");
					return false;
				}
			}
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			ReportDocument reportDocument = new ReportDocument();
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ChecksSendToPickupPrinters_A.rpt" : "Rep_POS_ChecksSendToPickupPrinters_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@CheckID", RowID);
			reportDocument.SetParameterValue("@ItemIDs", dataTable.Rows[k]["ItemIDs"].ToString());
			reportDocument.SetParameterValue("@TableCode", ((Control)(object)txtTableNo).Text);
			reportDocument.SetParameterValue("@SecondPrint", SecondPrint ? "1" : "0");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.SetParameterValue("@CheckID", RowID, "Rep_POS_ChecksDetailsAccessoriesSendToPrinters");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_POS_ChecksDetailsAccessoriesSendToPrinters");
			reportDocument.SetParameterValue("@CheckIDs", "," + RowID + ",", "Rep_POS_ChecksDetailsAdditionals");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_POS_ChecksDetailsAdditionals");
			reportDocument.PrintOptions.PrinterName = dataTable.Rows[k]["PrinterName"].ToString();
			reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			reportDocument.Dispose();
			GC.Collect();
		}
		return true;
	}

	public void PrintDeletedSentItems(DataTable DeletedItems)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("PrinterName");
		dataTable.Columns.Add("CheckDetailIDs");
		string text = ",";
		for (int i = 0; i < DeletedItems.Rows.Count; i++)
		{
			if (decimal.Parse(DeletedItems.Rows[i]["SentQty"].ToString()) > 0m)
			{
				if (dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + DeletedItems.Rows[i]["ItemID"].ToString())[0]["POSPrinter"].ToString() + "'").Length == 0)
				{
					dataTable.Rows.Add(dtItemsAndGroups.Select(" ItemID= " + DeletedItems.Rows[i]["ItemID"].ToString())[0]["POSPrinter"].ToString(), "," + DeletedItems.Rows[i]["CheckDetailID"].ToString() + ",");
				}
				else
				{
					dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + DeletedItems.Rows[i]["ItemID"].ToString())[0]["POSPrinter"].ToString() + "'")[0]["CheckDetailIDs"] = dataTable.Select(" PrinterName= '" + dtItemsAndGroups.Select(" ItemID= " + DeletedItems.Rows[i]["ItemID"].ToString())[0]["POSPrinter"].ToString() + "'")[0]["CheckDetailIDs"].ToString() + DeletedItems.Rows[i]["CheckDetailID"].ToString() + ",";
				}
				text = text + DeletedItems.Rows[i]["CheckDetailID"].ToString() + ",";
			}
		}
		if (dataTable.Rows.Count > 0)
		{
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				bool flag = false;
				foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
				{
					if (dataTable.Rows[j]["PrinterName"].ToString() == installedPrinter)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					GlobalVariables.InformationMB.Show(" الطابعة غير معرفة  \n  " + dataTable.Rows[j]["PrinterName"].ToString(), " printer " + dataTable.Rows[j]["PrinterName"].ToString() + " Is not Installed ");
					return;
				}
			}
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			ReportDocument reportDocument = new ReportDocument();
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ChecksSendToPrintersDeletedItems_A.rpt" : "Rep_POS_ChecksSendToPrintersDeletedItems_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@CheckID", RowID);
			reportDocument.SetParameterValue("@CheckDetailIDs", "," + dataTable.Rows[k]["CheckDetailIDs"].ToString() + ",");
			reportDocument.SetParameterValue("@TableCode", ((Control)(object)txtTableNo).Text);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = dataTable.Rows[k]["PrinterName"].ToString();
			reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			reportDocument.Dispose();
			GC.Collect();
		}
		if (text != ",")
		{
			ReportDocument reportDocument2 = new ReportDocument();
			reportDocument2.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ChecksSendToPrintersDeletedItems_A.rpt" : "Rep_POS_ChecksSendToPrintersDeletedItems_E.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument2);
			reportDocument2.SetParameterValue("@CheckID", RowID);
			reportDocument2.SetParameterValue("@CheckDetailIDs", "," + text);
			reportDocument2.SetParameterValue("@TableCode", ((Control)(object)txtTableNo).Text);
			reportDocument2.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument2.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			reportDocument2.PrintToPrinter(1, collated: true, 0, 10000);
			reportDocument2.Dispose();
			GC.Collect();
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraGridBase)ULGData).ActiveRow.Delete(false);
		}
	}

	private void txtTableNo_Leave(object sender, EventArgs e)
	{
	}

	private void btnDiscountValue_Click(object sender, EventArgs e)
	{
		OpenChangeDiscountForm();
	}

	private void btnDiscountRatio_Click(object sender, EventArgs e)
	{
		OpenChangeDiscountForm();
	}

	public void OpenChangeDiscountForm()
	{
		decimal num = CalculateGrossItemUnderDiscount();
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(num, decimal.Parse(((Control)(object)txtDiscountValue).Text), decimal.Parse(((Control)(object)txtDiscountRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog(this);
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscountRatio).Text = decimal.Parse((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDiscountValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			return;
		}
		((Control)(object)txtDiscountRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscountValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		CheckLog = CheckLog + " تم تغيير نسبة الخصم   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscountValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountRatio).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "." || ((Control)(object)txtDiscountValue).Text == "0") ? "0" : ((Control)(object)txtDiscountValue).Text) / num * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((Control)(object)txtDiscountRatio).Text = "0";
			((Control)(object)txtDiscountValue).Text = "0";
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		}
	}

	private void txtDiscountRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountValue).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((((Control)(object)txtDiscountRatio).Text != "" && cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount) || (((Control)(object)txtDiscountRatio).Text != "" && cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((Control)(object)txtDiscountRatio).Text = "0";
			((Control)(object)txtDiscountValue).Text = "0";
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		}
	}

	private void btnClientAdd_Click(object sender, EventArgs e)
	{
		frmPOSClientsWithDetails frmPOSClientsWithDetails2 = new frmPOSClientsWithDetails(-1);
		frmPOSClientsWithDetails2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmPOSClientsWithDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Clients");
		frmPOSClientsWithDetails2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.POS.MasterData.frmPOSClientsWithDetails'")[0];
		frmPOSClientsWithDetails2.ShowDialog();
		if (frmPOSClientsWithDetails2.ClientID != 0)
		{
			dtClients = Clients.FillCombo("1", GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboClient, dtClients, "ClientID", "ClientName");
			((TextEditorControlBase)cboClient).Value = frmPOSClientsWithDetails2.ClientID;
		}
	}

	private void btnDiscountRatio2_Click(object sender, EventArgs e)
	{
		OpenChangeDiscount2Form();
	}

	private void btnDiscountValue2_Click(object sender, EventArgs e)
	{
		OpenChangeDiscount2Form();
	}

	public void OpenChangeDiscount2Form()
	{
		decimal num = CalculateGrossItemUnderDiscount();
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "" || ((Control)(object)txtServiceChargeValue).Text == ".") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text), decimal.Parse(((Control)(object)txtDiscountValue2).Text), decimal.Parse(((Control)(object)txtDiscountRatio2).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog(this);
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscountRatio2).Text = decimal.Parse(((AdditionalDiscountIncludeTax || AdditionalDiscountWithoutTax) && !ShowFirstDiscount && cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if (AdditionalDiscountIncludeTax)
			{
				((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * (num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "" || ((Control)(object)txtServiceChargeValue).Text == ".") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			return;
		}
		((Control)(object)txtDiscountRatio2).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscountValue2).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		CheckLog = CheckLog + " تم تغيير نسبة الخصم   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		frmChangeDiscount2.Dispose();
	}

	private void txtDiscountValue2_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			decimal num = CalculateGrossItemUnderDiscount();
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
			((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
			if (AdditionalDiscountIncludeTax)
			{
				decimal num2 = num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "" || ((Control)(object)txtServiceChargeValue).Text == ".") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text);
				((Control)(object)txtDiscountRatio2).Text = ((num2 == 0m) ? "0" : decimal.Parse((decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == "." || ((Control)(object)txtDiscountValue2).Text == "0") ? "0" : ((Control)(object)txtDiscountValue2).Text) / num2 * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate));
			}
			else
			{
				((Control)(object)txtDiscountRatio2).Text = decimal.Parse((num == 0m) ? "0" : (decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == "." || ((Control)(object)txtDiscountValue2).Text == "0") ? "0" : ((Control)(object)txtDiscountValue2).Text) / num * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			if (decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text) > UserSalesDiscount)
			{
				OpenChangeDiscount2Form();
			}
			CalculateNetTotals();
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
			((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
		}
	}

	private void txtDiscountRatio2_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			decimal num = CalculateGrossItemUnderDiscount();
			((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
			if (AdditionalDiscountIncludeTax)
			{
				decimal num2 = num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "" || ((Control)(object)txtServiceChargeValue).Text == ".") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text);
				((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * num).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			if ((((Control)(object)txtDiscountRatio2).Text != "" && cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text) > UserSalesDiscount) || (((Control)(object)txtDiscountRatio2).Text != "" && cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text) > UserSalesDiscount))
			{
				OpenChangeDiscount2Form();
			}
			CalculateNetTotals();
			((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
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
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And IsDineIn=1 And RoomID=" + RoomID);
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsDineIn=1 ", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsDineIn=1 ", "1");
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

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Expected O, but got Unknown
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_096f: Expected O, but got Unknown
		if (base.Disposing)
		{
			return;
		}
		DataTable dataTable = new DataTable();
		string text = ",";
		if (dtChecksDetailsAddtionals.Rows.Count > 0)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + ",";
			}
		}
		if (cboClient.SelectedIndex > -1)
		{
			CheckLog = CheckLog + "  تم تغيير العميل الى  X_" + ((Control)(object)cboClient).Text + " مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			if (ShowFirstDiscount)
			{
				((Control)(object)txtDiscountRatio).Text = decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else if (AdditionalDiscountIncludeTax || AdditionalDiscountWithoutTax)
			{
				((Control)(object)txtDiscountRatio2).Text = decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			if (dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == DBNull.Value || dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] == dtRoomData.Rows[0]["PriceTypeID"])
			{
				dtItemsAndGroups = dtItemsAndGroupsCopy;
				if (dtChecksDetailsAddtionals.Rows.Count > 0)
				{
					dataTable = ItemsAdditionals.FillComboByItemIds(text, dtRoomData.Rows[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				}
			}
			else
			{
				dtItemsAndGroups = Checks.SelectItemsAndGroupsByRoomID("," + RoomID + ",", dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.CurrentBranchID);
				if (dtChecksDetailsAddtionals.Rows.Count > 0)
				{
					dataTable = ItemsAdditionals.FillComboByItemIds(text, dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				}
			}
		}
		else
		{
			UltraTextEditor obj = txtDiscountRatio;
			string text2 = (((Control)(object)txtDiscountRatio2).Text = "0");
			((Control)(object)obj).Text = text2;
			dtItemsAndGroups = dtItemsAndGroupsCopy;
			if (dtChecksDetailsAddtionals.Rows.Count > 0)
			{
				dataTable = ItemsAdditionals.FillComboByItemIds(text, dtRoomData.Rows[0]["PriceTypeID"].ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
		}
		for (int j = 0; j < dtChecksDetailsAddtionals.Rows.Count; j++)
		{
			dtChecksDetailsAddtionals.Rows[j]["UnitPrice"] = dataTable.Select(" AdditionalItemID=  " + dtChecksDetailsAddtionals.Rows[j]["ItemID"].ToString())[0]["Price"];
			dtChecksDetailsAddtionals.Rows[j]["TotalPrice"] = decimal.Parse(dtChecksDetailsAddtionals.Rows[j]["Qty"].ToString()) * decimal.Parse(dtChecksDetailsAddtionals.Rows[j]["UnitPrice"].ToString());
		}
		decimal num = default(decimal);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value == DBNull.Value)
			{
				continue;
			}
			if (dtChecksDetailsAddtionals.Rows.Count > 0)
			{
				num = default(decimal);
				DataView dataView = new DataView(dtChecksDetailsAddtionals);
				dataView.RowFilter = " CheckDetailID=  " + ((UltraGridBase)ULGData).Rows[k].Cells["CheckDetailID"].Value.ToString();
				DataTable dataTable2 = dataView.ToTable();
				object obj2 = dataTable2.Compute(" Sum(TotalPrice) ", "");
				if (dataTable2.Rows.Count > 0 && obj2 != DBNull.Value)
				{
					num = decimal.Parse(obj2.ToString());
				}
			}
			DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
			((UltraGridBase)ULGData).Rows[k].Cells["AdditionalPrice"].Value = num;
			((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value = decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
			((UltraGridBase)ULGData).Rows[k].Cells["RoomDiscountValue"].Value = decimal.Parse(dataRow["RoomDiscountValue"].ToString());
			((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["AdditionalPrice"].Value.ToString());
			((UltraGridBase)ULGData).Rows[k].Cells["ServiceChargeAmount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value.ToString()) * decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m;
			CalculateRow(((UltraGridBase)ULGData).Rows[k]);
		}
		CalculateGoss();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Checks.GetCodeByBranchID("1", "0", "0", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	public override void btnCancelClick()
	{
		if (Adding)
		{
			drMaster = null;
		}
		if (Updating && dtUsersTransactions.Rows.Count > 0)
		{
			UsersTransactions.Delete(dtUsersTransactions.Rows[0][0].ToString());
		}
		Adding = false;
		Updating = false;
	}

	private void btnTransfer_Click(object sender, EventArgs e)
	{
		if (RowID != "")
		{
			frmChecksTransfer frmChecksTransfer2 = new frmChecksTransfer(int.Parse(RowID), RoomID);
			frmChecksTransfer2.WindowState = FormWindowState.Normal;
			frmChecksTransfer2.ShowDialog(this);
			if (frmChecksTransfer2.TablesNo != "")
			{
				CheckLog = CheckLog + " تم تغيير رقم الطاولة من   X_" + ((Control)(object)txtTableNo).Text + "X_ الى الطاولة رقم  X_" + frmChecksTransfer2.TablesNo + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
				((Control)(object)txtTableNo).Text = frmChecksTransfer2.TablesNo;
			}
		}
	}

	private void txtPersonCount_ValueChanged(object sender, EventArgs e)
	{
		if (Updating && drMaster != null && (((UltraToggleEditorBase)chkMinCharge).Checked || ((UltraToggleEditorBase)chkMaxCharge).Checked) && ((Control)(object)txtPersonCount).Text != "" && int.Parse(((Control)(object)txtPersonCount).Text) < int.Parse(drMaster["PersonCount"].ToString()) && bool.Parse(drMaster["IsPrinted"].ToString()))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "عدد الاشخاص لا يمكن نقص عدد الاشخاص تمت طباعة الشيك" : "Person Count Cannot Decrease Check Is Printed");
			((TextEditorControlBase)txtPersonCount).ValueChanged -= txtPersonCount_ValueChanged;
			((TextEditorControlBase)txtPersonCount).Value = drMaster["PersonCount"];
			((TextEditorControlBase)txtPersonCount).ValueChanged += txtPersonCount_ValueChanged;
			((TextEditorControlBase)txtPersonCount).Focus();
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void chkIsInvoice_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtInvoiceNo).Enabled = ((UltraToggleEditorBase)chkIsInvoice).Checked;
		if (((UltraToggleEditorBase)chkIsInvoice).Checked)
		{
			((Control)(object)txtInvoiceNo).Text = Checks.GetInvoiceNoByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void chkIsInvoice_BeforeCheckStateChanged(object sender, CancelEventArgs e)
	{
		if (((UltraToggleEditorBase)chkIsInvoice).Checked)
		{
			e.Cancel = true;
		}
	}

	private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnAccessories_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			return;
		}
		DataRow dataRow = dtItemsAndGroups.Select("IsOffer = 0 and ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		if (dataRow["AccessoriesCount"] == DBNull.Value || int.Parse(dataRow["AccessoriesCount"].ToString()) <= 0)
		{
			return;
		}
		bool canedit = true;
		if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value.ToString()) > 0m || (drMaster != null && drMaster["IsPrinted"].Equals(true)))
		{
			canedit = false;
		}
		int accessoriescount = int.Parse(dataRow["AccessoriesCount"].ToString());
		bool enforceAccessories = bool.Parse(dataRow["EnforceAccessories"].ToString());
		frmChecksDetailsAccessories frmChecksDetailsAccessories2 = new frmChecksDetailsAccessories(accessoriescount, int.Parse(dataRow["ItemID"].ToString()), int.Parse(dataRow["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()), canedit, enforceAccessories);
		frmChecksDetailsAccessories2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmChecksDetailsAccessories2.lblTitle).Text = (GlobalVariables.IsArabic ? "الملحقات" : "Items Accessories");
		frmChecksDetailsAccessories2.dtChecksDetailsAccessories = null;
		frmChecksDetailsAccessories2.dtChecksDetailsAccessories = dtChecksDetailsAccessories.Clone();
		DataView dataView = new DataView(dtChecksDetailsAccessories);
		CheckLog = CheckLog + " اضافة ملحق  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		dataView.RowFilter = " CheckDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString();
		DataTable dataTable = dataView.ToTable();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			frmChecksDetailsAccessories2.dtChecksDetailsAccessories.ImportRow(dataTable.Rows[i]);
		}
		frmChecksDetailsAccessories2.ShowDialog(this);
		if (frmChecksDetailsAccessories2.Cancel)
		{
			return;
		}
		for (int j = 0; j < dtChecksDetailsAccessories.Rows.Count; j++)
		{
			if (int.Parse(dtChecksDetailsAccessories.Rows[j]["CheckDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()))
			{
				dtChecksDetailsAccessories.Rows[j].Delete();
				dtChecksDetailsAccessories.AcceptChanges();
				j--;
			}
		}
		for (int k = 0; k < frmChecksDetailsAccessories2.dtChecksDetailsAccessories.Rows.Count; k++)
		{
			dtChecksDetailsAccessories.ImportRow(frmChecksDetailsAccessories2.dtChecksDetailsAccessories.Rows[k]);
		}
	}

	private void btnAddtionals_Click(object sender, EventArgs e)
	{
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Expected O, but got Unknown
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		if (((UltraGridBase)ULGData).ActiveRow == null || ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value || ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value != DBNull.Value)
		{
			return;
		}
		DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		if (dataRow["AdditionalCount"] == DBNull.Value || int.Parse(dataRow["AdditionalCount"].ToString()) <= 0)
		{
			return;
		}
		bool canedit = true;
		if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["SentQty"].Value.ToString()) > 0m || (drMaster != null && drMaster["IsPrinted"].Equals(true)))
		{
			canedit = false;
		}
		frmChecksDetailsAdditionals frmChecksDetailsAdditionals2 = new frmChecksDetailsAdditionals(int.Parse(dataRow["ItemID"].ToString()), int.Parse(dtRoomData.Rows[0]["PriceTypeID"].ToString()), int.Parse(dataRow["StoreID"].ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()), canedit);
		frmChecksDetailsAdditionals2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmChecksDetailsAdditionals2.lblTitle).Text = (GlobalVariables.IsArabic ? "الاضافات" : "Items Additionals");
		frmChecksDetailsAdditionals2.dtChecksDetailsAdditionals = null;
		frmChecksDetailsAdditionals2.dtChecksDetailsAdditionals = dtChecksDetailsAddtionals.Clone();
		DataView dataView = new DataView(dtChecksDetailsAddtionals);
		CheckLog = CheckLog + " اضافة اضافة  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		dataView.RowFilter = " CheckDetailID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString();
		DataTable dataTable = dataView.ToTable();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			frmChecksDetailsAdditionals2.dtChecksDetailsAdditionals.ImportRow(dataTable.Rows[i]);
		}
		frmChecksDetailsAdditionals2.ShowDialog(this);
		if (frmChecksDetailsAdditionals2.Cancel)
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value = frmChecksDetailsAdditionals2.AdditionalPrice;
		((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalPrice"].Value.ToString());
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		CalculateGoss();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		for (int j = 0; j < dtChecksDetailsAddtionals.Rows.Count; j++)
		{
			if (int.Parse(dtChecksDetailsAddtionals.Rows[j]["CheckDetailID"].ToString()) == int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value.ToString()))
			{
				dtChecksDetailsAddtionals.Rows[j].Delete();
				dtChecksDetailsAddtionals.AcceptChanges();
				j--;
			}
		}
		for (int k = 0; k < frmChecksDetailsAdditionals2.dtChecksDetailsAdditionals.Rows.Count; k++)
		{
			dtChecksDetailsAddtionals.ImportRow(frmChecksDetailsAdditionals2.dtChecksDetailsAdditionals.Rows[k]);
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Comments")
		{
			frmPOSComments frmPOSComments2 = new frmPOSComments(GlobalVariables.IsArabic ? "ملاحظات" : "Comments", _IsInt: false, _IsNumeric: false, e.Cell.Row.Cells["Notes"].Value.ToString());
			frmPOSComments2.WindowState = FormWindowState.Normal;
			if (frmPOSComments2.ShowDialog(this) == DialogResult.OK)
			{
				e.Cell.Row.Cells["Notes"].Value = frmPOSComments2.Value;
			}
		}
	}

	private void txtClientBarcode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtClientBarcode).Text != "")
		{
			DataRow[] array = dtClients.Select("ClientBarcode = '" + ((Control)(object)txtClientBarcode).Text + "'");
			if (array.Length != 0)
			{
				((TextEditorControlBase)cboClient).Value = array[0]["ClientID"].ToString();
			}
			((TextEditorControlBase)txtClientBarcode).Clear();
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
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Expected O, but got Unknown
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Expected O, but got Unknown
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Expected O, but got Unknown
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Expected O, but got Unknown
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Expected O, but got Unknown
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Expected O, but got Unknown
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Expected O, but got Unknown
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dc: Expected O, but got Unknown
		//IL_07ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Expected O, but got Unknown
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Expected O, but got Unknown
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0824: Expected O, but got Unknown
		//IL_15c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d2: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecks));
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
		this.pnlItems = new UltraPanel();
		this.btnClientSearch = new UltraButton();
		this.txtBarCode = new UltraTextEditor();
		this.lblClient = new UltraLabel();
		this.lblBarCode = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboClient = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.UGBItemData = new UltraGroupBox();
		this.txtPersonCount = new UltraTextEditor();
		this.lblPersonCount = new UltraLabel();
		this.btnPersonCount = new UltraButton();
		this.lblTableNo = new UltraLabel();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblServiceChargeValue = new UltraLabel();
		this.txtServiceChargeValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.btnPlus = new UltraButton();
		this.btnSubtract = new UltraButton();
		this.lblDiscountValue = new UltraLabel();
		this.txtDiscountValue = new UltraTextEditor();
		this.btnCloseCheck = new UltraButton();
		this.btnMerge = new UltraButton();
		this.btnSeparate = new UltraButton();
		this.btnSent = new UltraButton();
		this.btnClear = new UltraButton();
		this.lblDiscountRatio = new UltraLabel();
		this.txtDiscountRatio = new UltraTextEditor();
		this.btnDiscountRatio = new UltraButton();
		this.btnDiscountValue = new UltraButton();
		this.chkMinCharge = new UltraCheckEditor();
		this.txtMinChargeValue = new UltraTextEditor();
		this.btnTransfer = new UltraButton();
		this.lblCaptainOrder = new UltraLabel();
		this.cboCaptainOrder = new UltraComboEditor();
		this.UTCItemsGroups = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.txtInvoiceNo = new UltraTextEditor();
		this.chkIsInvoice = new UltraCheckEditor();
		this.lblRoundingValue = new UltraLabel();
		this.txtRoundingValue = new UltraTextEditor();
		this.btnAccessories = new UltraButton();
		this.btnAddtionals = new UltraButton();
		this.txtTableNo = new UltraTextEditor();
		this.txtDiscountValue2 = new UltraTextEditor();
		this.lblDiscountValue2 = new UltraLabel();
		this.btnDiscountValue2 = new UltraButton();
		this.txtDiscountRatio2 = new UltraTextEditor();
		this.lblDiscountRatio2 = new UltraLabel();
		this.btnDiscountRatio2 = new UltraButton();
		this.chkMaxCharge = new UltraCheckEditor();
		this.txtMaxChargeValue = new UltraTextEditor();
		this.txtClientBarcode = new UltraTextEditor();
		this.lblClientBarcode = new UltraLabel();
		this.btnClientAdd = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPersonCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkMinCharge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCaptainOrder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsInvoice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTableNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkMaxCharge).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).BeginInit();
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
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		base.ULGData.ClickCell += new ClickCellEventHandler(ULGData_ClickCell);
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
		resources.ApplyResources(this.pnlItems, "pnlItems");
		this.pnlItems.AutoScroll = true;
		resources.ApplyResources(this.pnlItems.ClientArea, "pnlItems.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlItems).Name = "pnlItems";
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
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
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.UGBItemData, "UGBItemData");
		this.UGBItemData.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItemData).Controls.Add((System.Windows.Forms.Control)(object)this.pnlItems);
		((System.Windows.Forms.Control)(object)this.UGBItemData).Name = "UGBItemData";
		resources.ApplyResources(this.txtPersonCount, "txtPersonCount");
		((System.Windows.Forms.Control)(object)this.txtPersonCount).Name = "txtPersonCount";
		((TextEditorControlBase)this.txtPersonCount).ValueChanged += new System.EventHandler(txtPersonCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtPersonCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonCount_KeyPress);
		resources.ApplyResources(this.lblPersonCount, "lblPersonCount");
		this.lblPersonCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonCount).Name = "lblPersonCount";
		((ControlBase)this.lblPersonCount).WrapText = false;
		resources.ApplyResources(this.btnPersonCount, "btnPersonCount");
		((System.Windows.Forms.Control)(object)this.btnPersonCount).Name = "btnPersonCount";
		((System.Windows.Forms.Control)(object)this.btnPersonCount).Click += new System.EventHandler(btnPersonCount_Click);
		resources.ApplyResources(this.lblTableNo, "lblTableNo");
		this.lblTableNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTableNo).Name = "lblTableNo";
		((ControlBase)this.lblTableNo).WrapText = false;
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblServiceChargeValue, "lblServiceChargeValue");
		this.lblServiceChargeValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceChargeValue).Name = "lblServiceChargeValue";
		((ControlBase)this.lblServiceChargeValue).WrapText = false;
		resources.ApplyResources(this.txtServiceChargeValue, "txtServiceChargeValue");
		((System.Windows.Forms.Control)(object)this.txtServiceChargeValue).Name = "txtServiceChargeValue";
		((EditorButtonControlBase)this.txtServiceChargeValue).ReadOnly = true;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.btnPlus, "btnPlus");
		((System.Windows.Forms.Control)(object)this.btnPlus).Name = "btnPlus";
		((System.Windows.Forms.Control)(object)this.btnPlus).Click += new System.EventHandler(btnPlus_Click);
		resources.ApplyResources(this.btnSubtract, "btnSubtract");
		((System.Windows.Forms.Control)(object)this.btnSubtract).Name = "btnSubtract";
		((System.Windows.Forms.Control)(object)this.btnSubtract).Click += new System.EventHandler(btnSubtract_Click);
		resources.ApplyResources(this.lblDiscountValue, "lblDiscountValue");
		this.lblDiscountValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue).Name = "lblDiscountValue";
		((ControlBase)this.lblDiscountValue).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue, "txtDiscountValue");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).Name = "txtDiscountValue";
		((EditorButtonControlBase)this.txtDiscountValue).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountValue).ValueChanged += new System.EventHandler(txtDiscountValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.btnCloseCheck, "btnCloseCheck");
		((System.Windows.Forms.Control)(object)this.btnCloseCheck).Name = "btnCloseCheck";
		((System.Windows.Forms.Control)(object)this.btnCloseCheck).Click += new System.EventHandler(btnCloseCheck_Click);
		resources.ApplyResources(this.btnMerge, "btnMerge");
		((System.Windows.Forms.Control)(object)this.btnMerge).Name = "btnMerge";
		((System.Windows.Forms.Control)(object)this.btnMerge).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnMerge).Click += new System.EventHandler(btnMerge_Click);
		resources.ApplyResources(this.btnSeparate, "btnSeparate");
		((System.Windows.Forms.Control)(object)this.btnSeparate).Name = "btnSeparate";
		((System.Windows.Forms.Control)(object)this.btnSeparate).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSeparate).Click += new System.EventHandler(btnSeparate_Click);
		resources.ApplyResources(this.btnSent, "btnSent");
		((System.Windows.Forms.Control)(object)this.btnSent).Name = "btnSent";
		((System.Windows.Forms.Control)(object)this.btnSent).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSent).Click += new System.EventHandler(btnSent_Click);
		resources.ApplyResources(this.btnClear, "btnClear");
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		resources.ApplyResources(this.lblDiscountRatio, "lblDiscountRatio");
		this.lblDiscountRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio).Name = "lblDiscountRatio";
		((ControlBase)this.lblDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio, "txtDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).Name = "txtDiscountRatio";
		((EditorButtonControlBase)this.txtDiscountRatio).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountRatio).ValueChanged += new System.EventHandler(txtDiscountRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.btnDiscountRatio, "btnDiscountRatio");
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio).Name = "btnDiscountRatio";
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio).Click += new System.EventHandler(btnDiscountRatio_Click);
		resources.ApplyResources(this.btnDiscountValue, "btnDiscountValue");
		((System.Windows.Forms.Control)(object)this.btnDiscountValue).Name = "btnDiscountValue";
		((System.Windows.Forms.Control)(object)this.btnDiscountValue).Click += new System.EventHandler(btnDiscountValue_Click);
		resources.ApplyResources(this.chkMinCharge, "chkMinCharge");
		((System.Windows.Forms.Control)(object)this.chkMinCharge).Name = "chkMinCharge";
		resources.ApplyResources(this.txtMinChargeValue, "txtMinChargeValue");
		((System.Windows.Forms.Control)(object)this.txtMinChargeValue).Name = "txtMinChargeValue";
		resources.ApplyResources(this.btnTransfer, "btnTransfer");
		((System.Windows.Forms.Control)(object)this.btnTransfer).Name = "btnTransfer";
		((System.Windows.Forms.Control)(object)this.btnTransfer).Click += new System.EventHandler(btnTransfer_Click);
		resources.ApplyResources(this.lblCaptainOrder, "lblCaptainOrder");
		this.lblCaptainOrder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCaptainOrder).Name = "lblCaptainOrder";
		((ControlBase)this.lblCaptainOrder).WrapText = false;
		resources.ApplyResources(this.cboCaptainOrder, "cboCaptainOrder");
		((TextEditorControlBase)this.cboCaptainOrder).AlwaysInEditMode = true;
		this.cboCaptainOrder.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCaptainOrder).Name = "cboCaptainOrder";
		resources.ApplyResources(this.UTCItemsGroups, "UTCItemsGroups");
		resources.ApplyResources(val10, "appearance10");
		((AppearanceBase)val10).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCItemsGroups).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Name = "UTCItemsGroups";
		((UltraTabControlBase)this.UTCItemsGroups).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.txtInvoiceNo, "txtInvoiceNo");
		((System.Windows.Forms.Control)(object)this.txtInvoiceNo).Name = "txtInvoiceNo";
		resources.ApplyResources(this.chkIsInvoice, "chkIsInvoice");
		((System.Windows.Forms.Control)(object)this.chkIsInvoice).Name = "chkIsInvoice";
		((UltraToggleEditorBase)this.chkIsInvoice).BeforeCheckStateChanged += new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
		((UltraToggleEditorBase)this.chkIsInvoice).CheckedChanged += new System.EventHandler(chkIsInvoice_CheckedChanged);
		resources.ApplyResources(this.lblRoundingValue, "lblRoundingValue");
		this.lblRoundingValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoundingValue).Name = "lblRoundingValue";
		((ControlBase)this.lblRoundingValue).WrapText = false;
		resources.ApplyResources(this.txtRoundingValue, "txtRoundingValue");
		((System.Windows.Forms.Control)(object)this.txtRoundingValue).Name = "txtRoundingValue";
		((EditorButtonControlBase)this.txtRoundingValue).ReadOnly = true;
		resources.ApplyResources(this.btnAccessories, "btnAccessories");
		((System.Windows.Forms.Control)(object)this.btnAccessories).Name = "btnAccessories";
		((System.Windows.Forms.Control)(object)this.btnAccessories).Click += new System.EventHandler(btnAccessories_Click);
		resources.ApplyResources(this.btnAddtionals, "btnAddtionals");
		((System.Windows.Forms.Control)(object)this.btnAddtionals).Name = "btnAddtionals";
		((System.Windows.Forms.Control)(object)this.btnAddtionals).Click += new System.EventHandler(btnAddtionals_Click);
		resources.ApplyResources(this.txtTableNo, "txtTableNo");
		((System.Windows.Forms.Control)(object)this.txtTableNo).Name = "txtTableNo";
		resources.ApplyResources(this.txtDiscountValue2, "txtDiscountValue2");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue2).Name = "txtDiscountValue2";
		((EditorButtonControlBase)this.txtDiscountValue2).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountValue2).ValueChanged += new System.EventHandler(txtDiscountValue2_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountValue2).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.lblDiscountValue2, "lblDiscountValue2");
		this.lblDiscountValue2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue2).Name = "lblDiscountValue2";
		((ControlBase)this.lblDiscountValue2).WrapText = false;
		resources.ApplyResources(this.btnDiscountValue2, "btnDiscountValue2");
		((System.Windows.Forms.Control)(object)this.btnDiscountValue2).Name = "btnDiscountValue2";
		((System.Windows.Forms.Control)(object)this.btnDiscountValue2).Click += new System.EventHandler(btnDiscountValue2_Click);
		resources.ApplyResources(this.txtDiscountRatio2, "txtDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio2).Name = "txtDiscountRatio2";
		((EditorButtonControlBase)this.txtDiscountRatio2).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountRatio2).ValueChanged += new System.EventHandler(txtDiscountRatio2_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio2).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumeric_KeyPress);
		resources.ApplyResources(this.lblDiscountRatio2, "lblDiscountRatio2");
		this.lblDiscountRatio2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio2).Name = "lblDiscountRatio2";
		((ControlBase)this.lblDiscountRatio2).WrapText = false;
		resources.ApplyResources(this.btnDiscountRatio2, "btnDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio2).Name = "btnDiscountRatio2";
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio2).Click += new System.EventHandler(btnDiscountRatio2_Click);
		resources.ApplyResources(this.chkMaxCharge, "chkMaxCharge");
		((System.Windows.Forms.Control)(object)this.chkMaxCharge).Name = "chkMaxCharge";
		resources.ApplyResources(this.txtMaxChargeValue, "txtMaxChargeValue");
		((System.Windows.Forms.Control)(object)this.txtMaxChargeValue).Name = "txtMaxChargeValue";
		resources.ApplyResources(this.txtClientBarcode, "txtClientBarcode");
		((System.Windows.Forms.Control)(object)this.txtClientBarcode).Name = "txtClientBarcode";
		((System.Windows.Forms.Control)(object)this.txtClientBarcode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtClientBarcode_KeyUp);
		resources.ApplyResources(this.lblClientBarcode, "lblClientBarcode");
		this.lblClientBarcode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientBarcode).Name = "lblClientBarcode";
		((ControlBase)this.lblClientBarcode).WrapText = false;
		resources.ApplyResources(this.btnClientAdd, "btnClientAdd");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.New;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnClientAdd).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Name = "btnClientAdd";
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Click += new System.EventHandler(btnClientAdd_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientBarcode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddtionals);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccessories);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsInvoice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCItemsGroups);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCaptainOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCaptainOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCloseCheck);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnTransfer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTableNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMaxChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkMaxCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMinChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkMinCharge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubtract);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPlus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTableNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPersonCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPersonCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPersonCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMerge);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSeparate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItemData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtServiceChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmChecks";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtServiceChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblServiceChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItemData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSeparate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMerge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPersonCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPersonCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPersonCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTableNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPlus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubtract, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkMinCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMinChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkMaxCharge, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMaxChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTableNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnTransfer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCloseCheck, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCaptainOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCaptainOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCItemsGroups, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsInvoice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccessories, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddtionals, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientBarcode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientAdd, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtPersonCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkMinCharge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMinChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCaptainOrder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsInvoice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTableNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkMaxCharge).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaxChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
