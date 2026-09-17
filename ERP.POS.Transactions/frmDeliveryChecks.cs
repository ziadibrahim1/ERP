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
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.POS.Transactions;

public class frmDeliveryChecks : frmHeaderDetails
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

	private DataTable dtRoomData;

	private DataTable dtDeliveryMan;

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

	private int RoomID;

	private int CheckID = 0;

	private int ClientDetailID = 0;

	private int ClientID = 0;

	private decimal DeliveryFees = default(decimal);

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

	private int newID = -100000;

	public string CheckLog = "";

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

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblDeliveryChargeValue;

	private UltraTextEditor txtDeliveryChargeValue;

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

	public UltraButton btnSent;

	private UltraButton btnClear;

	private UltraLabel lblDiscountRatio;

	private UltraTextEditor txtDiscountRatio;

	public UltraButton btnDiscountRatio;

	public UltraButton btnDiscountValue;

	public UltraButton btnDeliveryChargeValue;

	private UltraTextEditor txtInvoiceNo;

	private UltraCheckEditor chkIsInvoice;

	private UltraLabel lblRoundingValue;

	private UltraTextEditor txtRoundingValue;

	private UltraLabel lblDeliveryMan;

	private UltraComboEditor cboDeliveryMan;

	private UltraButton btnAccessories;

	private UltraButton btnAddtionals;

	private UltraTabControl UTCItemsGroups;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	public UltraButton btnDiscountValue2;

	public UltraButton btnDiscountRatio2;

	private UltraLabel lblDiscountRatio2;

	private UltraTextEditor txtDiscountRatio2;

	private UltraLabel lblDiscountValue2;

	private UltraTextEditor txtDiscountValue2;

	private UltraDateTimeEditor dtpClientDeliveryDate;

	private UltraLabel lblClientDeliveryDate;

	public frmDeliveryChecks()
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
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "شيكات توصيل للمنازل" : "Delivery");
	}

	public frmDeliveryChecks(int ID, int Room, int clientdetailID, int clientID, decimal deliveryfees)
		: this()
	{
		CheckID = ID;
		RoomID = Room;
		ClientDetailID = clientdetailID;
		ClientID = clientID;
		DeliveryFees = deliveryfees;
	}

	public frmDeliveryChecks(int Room)
		: this()
	{
		RoomID = Room;
	}

	public override void PrepareData()
	{
		dtReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmDelivery", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		dtDeletedItems.Columns.Add("CheckDetailID");
		dtDeletedItems.Columns.Add("Notes");
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
		UserSalesDiscount = Users.SelectSalesDiscount(GlobalVariables.UserID, IsFromServer: false);
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpClientDeliveryDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		AdditionalDiscountWithoutTax = bool.Parse(dtRoomData.Rows[0]["AdditionalDiscountWithoutTax"].ToString());
		AdditionalDiscountIncludeTax = bool.Parse(dtRoomData.Rows[0]["AdditionalDiscountIncludeTax"].ToString());
		ShowFirstDiscount = bool.Parse(dtRoomData.Rows[0]["ShowFirstDiscount"].ToString());
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
		dtDeliveryMan = DeliveryMan.FillCombo("," + GlobalVariables.CurrentBranchID + ",", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDeliveryMan, dtDeliveryMan, "DeliveryManID", "DeliveryManName");
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomDiscountValue"].DefaultCellValue = 0;
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
			((Control)(object)btnOK).Visible = false;
			((EditorButtonControlBase)txtCode).ReadOnly = true;
			((Control)(object)btnSaveClose).Text = (GlobalVariables.IsArabic ? "F2حفظ و عودة" : " Save And Back F2 ");
		}
		else if (CheckID != 0)
		{
			DataTable dataTable2 = Checks.Select(CheckID.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((EditorButtonControlBase)txtCode).ReadOnly = true;
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
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Expected O, but got Unknown
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Expected O, but got Unknown
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["CheckNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["CheckDate"];
			dtpClientDeliveryDate.Value = drMaster["ClientDeliveryDate"];
			((TextEditorControlBase)cboDeliveryMan).Value = drMaster["DeliveryManID"];
			((TextEditorControlBase)cboClient).ValueChanged -= cboClient_ValueChanged;
			((TextEditorControlBase)cboClient).Value = drMaster["ClientID"];
			if (cboClient.SelectedIndex > -1)
			{
				if (ShowFirstDiscount)
				{
					((Control)(object)txtDiscountRatio).Text = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString();
				}
				else if (AdditionalDiscountIncludeTax || AdditionalDiscountWithoutTax)
				{
					((Control)(object)txtDiscountRatio2).Text = dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString();
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
			((Control)(object)txtDeliveryChargeValue).Text = decimal.Parse(drMaster["DeliveryChargeValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTaxTotalValue).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetprice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRoundingValue).Text = decimal.Parse(drMaster["RoundingValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged -= new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
			((UltraToggleEditorBase)chkIsInvoice).CheckedChanged -= chkIsInvoice_CheckedChanged;
			((UltraToggleEditorBase)chkIsInvoice).Checked = bool.Parse(drMaster["IsInvoice"].ToString());
			((UltraToggleEditorBase)chkIsInvoice).CheckedChanged += chkIsInvoice_CheckedChanged;
			((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged += new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
			((Control)(object)txtInvoiceNo).Text = drMaster["InvoiceNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			CheckLog = drMaster["CheckLog"].ToString();
			CheckClosed = bool.Parse(drMaster["Closed"].ToString());
			IsCash = bool.Parse(drMaster["IsCash"].ToString());
			IsVisa = bool.Parse(drMaster["IsVisa"].ToString());
			IsOnAccount = bool.Parse(drMaster["IsOnAccount"].ToString());
			CashAmount = ((drMaster["CashAmount"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["CashAmount"].ToString()));
			OnAccountAmount = ((drMaster["OnAccountAmount"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["OnAccountAmount"].ToString()));
			VisaAmount = ((drMaster["VisaAmount"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["VisaAmount"].ToString()));
			VisatypeID = ((drMaster["VisaTypeID"] != DBNull.Value) ? int.Parse(drMaster["VisaTypeID"].ToString()) : 0);
			VisaNo = drMaster["VisaNo"].ToString();
			PaidAmount = ((drMaster["PaidAmount"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["PaidAmount"].ToString()));
			RestAmount = ((drMaster["RestAmount"] == DBNull.Value) ? 0m : decimal.Parse(drMaster["RestAmount"].ToString()));
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
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = true;
		((EditorButtonControlBase)cboDeliveryMan).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = true;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpClientDeliveryDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountRatio).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountValue).ReadOnly = NavMode;
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
		((EditorButtonControlBase)txtDiscountRatio2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountValue2).ReadOnly = NavMode;
		((Control)(object)btnCloseCheck).Enabled = !NavMode && CanCloseCheck;
		((Control)(object)btnRefreshData).Visible = false;
		((Control)(object)btnClientSearch).Visible = false;
		((EditorButtonControlBase)txtDeliveryChargeValue).ReadOnly = NavMode;
		((Control)(object)btnDeliveryChargeValue).Enabled = !NavMode;
		((Control)(object)btnPrint).Visible = true;
		((Control)(object)btnSent).Visible = false;
		((Control)(object)btnCopyTo).Visible = false;
		UltraButton obj11 = btnPlus;
		UltraButton obj12 = btnSubtract;
		UltraButton obj13 = btnClear;
		UltraButton obj14 = btnDiscountRatio;
		UltraButton obj15 = btnDiscountValue;
		UltraButton obj16 = btnDiscountRatio2;
		bool flag14 = (((Control)(object)btnDiscountValue2).Enabled = !NavMode);
		flag = (((Control)(object)obj16).Enabled = flag14);
		flag2 = (((Control)(object)obj15).Enabled = flag);
		flag4 = (((Control)(object)obj14).Enabled = flag2);
		flag6 = (((Control)(object)obj13).Enabled = flag4);
		visible = (((Control)(object)obj12).Enabled = flag6);
		((Control)(object)obj11).Enabled = visible;
		((Control)(object)btnOK).Visible = false;
		((Control)(object)chkIsInvoice).Visible = ((dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["PrintTaxInvoices"].ToString())) ? true : false);
		((Control)(object)txtInvoiceNo).Visible = ((dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["PrintTaxInvoices"].ToString())) ? true : false);
		((Control)(object)chkIsInvoice).Enabled = !NavMode;
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
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		((TextEditorControlBase)txtCode).Clear();
		UltraDateTimeEditor obj = dtpDate;
		DateTime dateTime = (dtpClientDeliveryDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
		obj.DateTime = dateTime;
		((Control)(object)txtCode).Text = (Adding ? Checks.GetCodeByBranchID("0", "1", "0", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtBarCode).Clear();
		cboDeliveryMan.SelectedIndex = -1;
		cboClient.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		CheckLog = "";
		((Control)(object)txtGrossValue).Text = "0";
		((Control)(object)txtDiscountValue).Text = "0";
		((Control)(object)txtDeliveryChargeValue).Text = decimal.Parse(DeliveryFees.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
		((Control)(object)txtDiscountValue2).Text = "0";
		((Control)(object)txtDiscountRatio2).Text = "0";
		((TextEditorControlBase)txtDiscountRatio2).ValueChanged += txtDiscountRatio2_ValueChanged;
		((TextEditorControlBase)txtDiscountValue2).ValueChanged += txtDiscountValue2_ValueChanged;
		((Control)(object)txtTaxTotalValue).Text = "0";
		((Control)(object)txtNetprice).Text = "0";
		((Control)(object)txtRoundingValue).Text = "0";
		CheckClosed = false;
		IsCash = false;
		IsVisa = false;
		IsOnAccount = false;
		CashAmount = default(decimal);
		OnAccountAmount = default(decimal);
		VisaAmount = default(decimal);
		VisatypeID = 0;
		VisaNo = "0";
		PaidAmount = default(decimal);
		RestAmount = default(decimal);
		((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged -= new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
		((UltraToggleEditorBase)chkIsInvoice).CheckedChanged -= chkIsInvoice_CheckedChanged;
		((UltraToggleEditorBase)chkIsInvoice).Checked = false;
		((UltraToggleEditorBase)chkIsInvoice).CheckedChanged += chkIsInvoice_CheckedChanged;
		((UltraToggleEditorBase)chkIsInvoice).BeforeCheckStateChanged += new BeforeCheckStateChangedHandler(chkIsInvoice_BeforeCheckStateChanged);
		((Control)(object)txtInvoiceNo).Text = "";
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		dtChecksDetailsAccessories.Rows.Clear();
		dtChecksDetailsAddtionals.Rows.Clear();
		if (ClientID != 0)
		{
			((TextEditorControlBase)cboClient).Value = ClientID;
		}
		else if (dtRoomData.Rows.Count > 0 && dtRoomData.Rows[0]["DefaultClientID"] != DBNull.Value && Adding)
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
		if (dtpClientDeliveryDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ التسليم" : "Please Enter The Check Delivery Date");
			((Control)(object)dtpClientDeliveryDate).Focus();
			dtpClientDeliveryDate.DropDown();
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
		if (((UltraToggleEditorBase)chkIsInvoice).Checked && ((Control)(object)txtInvoiceNo).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الفاتورة" : "Please Enter invoice No");
			((TextEditorControlBase)txtInvoiceNo).Focus();
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
			if (Main.CheckForValueByBranchIDAndFiscalYearID("POS_Checks", "InvoiceNo", ((Control)(object)txtInvoiceNo).Text, Adding ? "0" : drMaster["InvoiceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "  ") > 0)
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
		else if (Main.CheckForValueByBranchIDAndFiscalYearID("POS_Checks", "CheckNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["CheckNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), " And IsDineIn =0 And IsDelivery=1 And IsTakeAway=0  " + ((dtPOSDefaultData.Rows.Count > 0 && bool.Parse(dtPOSDefaultData.Rows[0]["SerialByShiftDetailID"].ToString())) ? ("  and ShiftDetailID = " + ShiftDetailID) : "")) > 0)
		{
			string codeByBranchID = Checks.GetCodeByBranchID("0", "1", "0", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			if (((Control)(object)txtCode).Text != codeByBranchID)
			{
				GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					((TextEditorControlBase)txtCode).Focus();
					return false;
				}
				((Control)(object)txtCode).Text = codeByBranchID;
			}
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == DBNull.Value && (((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString()) <= 0m))
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value == DBNull.Value && (((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString()) <= 0m))
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"];
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
			if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='DeliveryChargeAccount' ")[0]["AccountID"] == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء تحديد حساب رسم التوصيل من حسابات النظام  ", "Please Select Delivery Charge Account From SystemAccounts ");
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

	public override void btnOKClick()
	{
		DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Second);
		if (GlobalFunctions.GetServerDateTimeNow() < dateTime)
		{
			GlobalVariables.InformationMB.Show(dateTime.ToShortTimeString() + " وقت بداية الوردية ", " Shift Start Time Is " + dateTime.ToShortTimeString());
			return;
		}
		if (GlobalFunctions.GetServerDateTimeNow() > dateTime2.AddHours(2.0))
		{
			GlobalVariables.InformationMB.Show(dateTime2.ToShortTimeString() + " وقت نهاية الوردية ", " Shift End Time Is " + dateTime2.ToShortTimeString());
			return;
		}
		if (dtpDate.DateTime < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()) && Adding)
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return;
		}
		DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(dataTable.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return;
		}
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return;
		}
		if (dataTable2.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
		}
		base.btnOKClick();
		if (DataSaved && bool.Parse(dtRoomData.Rows[0]["SentItemsMessage"].ToString()) && dtDetails.Select("Qty >SentQty").Length != 0)
		{
			GlobalVariables.QuestionMB.Show("هل تريد إرسال الاصناف ؟", "Are You Sure You want to Send Items ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				SentItems();
				FillData();
			}
		}
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
			if (DataSaved && bool.Parse(dtRoomData.Rows[0]["SentItemsMessage"].ToString()) && DisplaySentQtyMessage && dtDetails.Select("Qty >SentQty ").Length != 0)
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
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Expected O, but got Unknown
		//IL_11d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11dd: Expected O, but got Unknown
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
			int num = Checks.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "1", "0", "Null", (cboClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboClient).Value.ToString() : "Null", ClientDetailID.ToString(), (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString() : "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString() : dtRoomData.Rows[0]["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "Null", (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue).Text.ToString(), (((Control)(object)txtDiscountRatio).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio).Text.ToString(), ((Control)(object)txtDeliveryChargeValue).Text, "0", (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscountValue2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue2).Text.ToString(), (((Control)(object)txtDiscountRatio2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text.ToString(), (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((Control)(object)txtNetprice).Text, "0", "0", "0", "0", IsCash ? "1" : "0", CashAmount.ToString(), IsVisa ? "1" : "0", (VisatypeID == 0) ? "Null" : VisatypeID.ToString(), (VisaNo == "0") ? "Null" : VisaNo, VisaAmount.ToString(), IsOnAccount ? "1" : "0", OnAccountAmount.ToString(), PaidAmount.ToString(), (decimal.Parse(((Control)(object)txtNetprice).Text) - PaidAmount).ToString(), ((Control)(object)txtNotes).Text, CheckLog, CheckClosed ? "1" : "0", ShiftDetailID, ShiftDetailUserID, dtRoomData.Rows[0]["RoomID"].ToString(), GlobalVariables.UserID, (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", "Null", (cboDeliveryMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDeliveryMan).Value.ToString(), CheckClosed ? GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate) : "Null", CheckClosed ? GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate) : "Null", dtpClientDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "0", "0", "Null", "Null", ((UltraToggleEditorBase)chkIsInvoice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsInvoice).Checked ? ((Control)(object)txtInvoiceNo).Text : "Null", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					int num2 = int.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CheckDetailID"].Value.ToString());
					int num3 = ChecksDetails.Insert_Update("-1", num.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ReturnedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ReturnedQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["AdditionalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["AdditionalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ServiceChargeAmount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ServiceChargeAmount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["RoomDiscountValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["RoomDiscountValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["SentDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SentDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", (((UltraGridBase)ULGData).Rows[j].Cells["SentQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["SentQty"].Value.ToString(), "0", ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["offerID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[j].Cells["offerID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["OfferDiscountRatio"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[j].Cells["OfferDiscountRatio"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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

	public override void UpdateData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_0b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Expected O, but got Unknown
		//IL_16a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ab: Expected O, but got Unknown
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
			int num = Checks.Insert_Update(drMaster["CheckID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "1", "0", "Null", (cboClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboClient).Value.ToString() : "Null", (drMaster["ClientDetailID"] == DBNull.Value) ? "Null" : drMaster["ClientDetailID"].ToString(), (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"].ToString() : "Null", (cboClient.SelectedIndex > -1 && dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"] != DBNull.Value) ? dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["PriceTypeID"].ToString() : dtRoomData.Rows[0]["PriceTypeID"].ToString(), dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dtCurrency.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), (drMaster["EmployeeID"] == DBNull.Value) ? "Null" : drMaster["EmployeeID"].ToString(), (((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text, (((Control)(object)txtDiscountValue).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue).Text.ToString(), (((Control)(object)txtDiscountRatio).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio).Text.ToString(), (((Control)(object)txtDeliveryChargeValue).Text == "") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text, "0", (((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text, (((Control)(object)txtDiscountValue2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountValue2).Text.ToString(), (((Control)(object)txtDiscountRatio2).Text.ToString() == "") ? "0" : ((Control)(object)txtDiscountRatio2).Text.ToString(), (((Control)(object)txtRoundingValue).Text == "") ? "0" : ((Control)(object)txtRoundingValue).Text, ((Control)(object)txtNetprice).Text, "0", "0", "0", "0", IsCash ? "1" : "0", CashAmount.ToString(), IsVisa ? "1" : "0", (VisatypeID == 0) ? "Null" : VisatypeID.ToString(), (VisaNo == "0") ? "Null" : VisaNo, VisaAmount.ToString(), IsOnAccount ? "1" : "0", OnAccountAmount.ToString(), PaidAmount.ToString(), (decimal.Parse(((Control)(object)txtNetprice).Text) - PaidAmount).ToString(), ((Control)(object)txtNotes).Text, CheckLog, (drMaster["Closed"] != DBNull.Value && bool.Parse(drMaster["Closed"].ToString())) ? "1" : (CheckClosed ? "1" : "0"), ShiftDetailID, ShiftDetailUserID, dtRoomData.Rows[0]["RoomID"].ToString(), GlobalVariables.UserID, (DiscountUserID > 0) ? DiscountUserID.ToString() : "Null", "Null", (cboDeliveryMan.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDeliveryMan).Value.ToString(), (drMaster["DeliveryStartDate"] == DBNull.Value) ? "Null" : drMaster["DeliveryStartDate"].ToString(), (drMaster["DeliveryEndDate"] == DBNull.Value && CheckClosed) ? GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate) : ((drMaster["DeliveryEndDate"] == DBNull.Value) ? "Null" : drMaster["DeliveryEndDate"].ToString()), dtpClientDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", (drMaster["StockControlJVID"] == DBNull.Value) ? "Null" : drMaster["StockControlJVID"].ToString(), (drMaster["SalesJVID"] == DBNull.Value) ? "Null" : drMaster["SalesJVID"].ToString(), "0", bool.Parse(drMaster["IsPrinted"].ToString()) ? "1" : "0", (drMaster["PrintUserID"] == DBNull.Value) ? "Null" : drMaster["PrintUserID"].ToString(), (drMaster["PrintDate"] == DBNull.Value) ? "Null" : DateTime.Parse(drMaster["PrintDate"].ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsInvoice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsInvoice).Checked ? ((Control)(object)txtInvoiceNo).Text : "Null", bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["HasChanges"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV");
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				string text2 = ",";
				string text3 = ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					text = text + ((UltraGridBase)ULGData).Rows[j].Cells["CheckDetailID"].Value.ToString() + ",";
				}
				for (int k = 0; k < dtChecksDetailsAccessories.Rows.Count; k++)
				{
					text2 = text2 + dtChecksDetailsAccessories.Rows[k]["CheckDetailAccessoryID"].ToString() + ",";
				}
				for (int l = 0; l < dtChecksDetailsAddtionals.Rows.Count; l++)
				{
					text3 = text3 + dtChecksDetailsAddtionals.Rows[l]["CheckDetailAdditionalID"].ToString() + ",";
				}
				Main.DeleteForUpdate("POS_ChecksDetailsTempPrint", "CheckID", drMaster["CheckID"].ToString(), "CheckDetailID", text);
				Main.DeleteForUpdate("POS_ChecksDetailsAccessories", "CheckID", drMaster["CheckID"].ToString(), "CheckDetailAccessoryID", text2);
				Main.DeleteForUpdate("POS_ChecksDetailsAdditionals", "CheckID", drMaster["CheckID"].ToString(), "CheckDetailAdditionalID", text3);
				for (int m = 0; m < dtDeletedItems.Rows.Count; m++)
				{
					ChecksDetails.DeleteVirtualWithNotes(dtDeletedItems.Rows[m]["CheckDetailID"].ToString(), dtDeletedItems.Rows[m]["Notes"].ToString(), GlobalVariables.UserID);
				}
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; n++)
				{
					int num2 = int.Parse(((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString());
					int num3 = ChecksDetails.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[n].Cells["CheckDetailID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[n].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[n].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[n].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["Qty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["Qty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["UnitID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ReturnedQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["ReturnedQty"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["StoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["StoreID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["AdditionalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["AdditionalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["TotalPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["TotalPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ServiceChargeAmount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["ServiceChargeAmount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["TaxID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["TaxID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["NetPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["NetPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["RoomDiscountValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["RoomDiscountValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["SentDate"].Value == DBNull.Value) ? "Null" : DateTime.Parse(((UltraGridBase)ULGData).Rows[n].Cells["SentDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", (((UltraGridBase)ULGData).Rows[n].Cells["SentQty"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["SentQty"].Value.ToString(), "0", ((UltraGridBase)ULGData).Rows[n].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["offerID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[n].Cells["offerID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[n].Cells["OfferDiscountRatio"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[n].Cells["OfferDiscountRatio"].Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
			string text4 = MessageLog.SelectByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV", GlobalVariables.IsArabic ? "1" : "0");
			if (text4 != "")
			{
				GlobalVariables.InformationMB.Show(text4);
				Main.RollbackBulkTrans(FromServer: false);
				DataSaved = false;
				MessageLog.DeleteByVoucherIDAndTransType(num.ToString(), "POSMIV", "POSMIV");
			}
			else
			{
				RowID = num.ToString();
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
			Checks.DeleteVirtual(drMaster["CheckID"].ToString(), GlobalVariables.UserID);
			ChecksDetails.DeleteVirtualByCheckID(drMaster["CheckID"].ToString(), GlobalVariables.UserID);
			ChecksDetailsAccessories.DeleteVirtualByCheckID(drMaster["CheckID"].ToString(), GlobalVariables.UserID);
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
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ChecksReport("," + dtRoomData.Rows[0]["RoomID"].ToString() + ",", "-1", "1", "-1", GlobalVariables.BranchIDs);
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
		//IL_124a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1251: Expected O, but got Unknown
		//IL_09de: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e8: Expected O, but got Unknown
		//IL_09f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_11e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f0: Expected O, but got Unknown
		//IL_11fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1208: Expected O, but got Unknown
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fe: Expected O, but got Unknown
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
				frmPOSChecksQtyDiscountsoffers2.ShowDialog();
				if (frmPOSChecksQtyDiscountsoffers2.OfferID != 0 && !frmPOSChecksQtyDiscountsoffers2.Cancel && cboClient.SelectedIndex > -1)
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
				frmPOSChecksGiftsoffers2.ShowDialog();
				dtOffersItems = frmPOSChecksGiftsoffers2.dtOffersItems;
				dtOffersGifts = frmPOSChecksGiftsoffers2.dtOffersGifts;
				OfferID = frmPOSChecksGiftsoffers2.OfferID;
				DiscountRatio = frmPOSChecksGiftsoffers2.DiscountRatio;
				if (frmPOSChecksGiftsoffers2.OfferID != 0 && !frmPOSChecksGiftsoffers2.Cancel && cboClient.SelectedIndex > -1)
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
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Expected O, but got Unknown
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		//IL_0d15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1f: Expected O, but got Unknown
		//IL_0d5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d68: Expected O, but got Unknown
		//IL_0d76: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d80: Expected O, but got Unknown
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
				if (bool.Parse(dataRow["IsWeight"].ToString()) || bool.Parse(dataRow["UsePOSNumPad"].ToString()))
				{
					frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGData).Rows[i].Cells["Qty"], ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString());
					frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
					frmDecimal2.ShowDialog();
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
		DataRow dataRow2 = dtItemsAndGroups.Select("IsOffer = 0 and ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
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
			frmDecimal3.ShowDialog();
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
			frmChecksDetailsAccessories2.ShowDialog();
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
		((UltraGridBase)ULGData).Rows[index].Cells["Qty"].Activate();
		((Control)(object)ULGData).Enter += ULGData_Enter;
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.AfterEnterEditMode += SelectFullRow;
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
		ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (drMaster != null && bool.Parse(drMaster["IsPrinted"].ToString()) && CheckID != 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن إضافة صنف تمت طباعة الشيك" : "Cannot Add this Item Because Check is Printed");
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			return;
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
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "StoreID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ColorID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ItemSizeID" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "BatchID")
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
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
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
			frmPOSComments2.ShowDialog();
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
				dtDeletedItems.Rows.Add(e.Rows[m].Cells["CheckDetailID"].Value.ToString(), frmPOSComments2.Value);
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
		if ((Adding || Updating) && ((!(((KeyedSubObjectBase)e.Cell.Column).Key == "UnitPrice") && !(((KeyedSubObjectBase)e.Cell.Column).Key == "TotalPrice")) || e.Cell.Row.Cells["ItemID"].Value == DBNull.Value || bool.Parse(dtItemsAndGroups.Select(" ItemID =" + e.Cell.Row.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString())) && ((KeyedSubObjectBase)e.Cell.Column).Key == "Qty" && ((drMaster != null && !bool.Parse(drMaster["IsPrinted"].ToString()) && e.Cell.Row.Cells["OfferID"].Value == DBNull.Value) || CheckID == 0))
		{
			frmDecimal frmDecimal2 = new frmDecimal(e.Cell, e.Cell.Value.ToString());
			frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal2.ShowDialog();
			CheckLog = CheckLog + "  تعديل كمية صنف  X_" + e.Cell.Row.Cells["ItemID"].Text + "X_ كمية  X_" + e.Cell.Row.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Comments")
		{
			frmPOSComments frmPOSComments2 = new frmPOSComments(GlobalVariables.IsArabic ? "ملاحظات" : "Comments", _IsInt: false, _IsNumeric: false, e.Cell.Row.Cells["Notes"].Value.ToString());
			frmPOSComments2.WindowState = FormWindowState.Normal;
			if (frmPOSComments2.ShowDialog() == DialogResult.OK)
			{
				e.Cell.Row.Cells["Notes"].Value = frmPOSComments2.Value;
			}
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		decimal num2 = CalculateGrossWithoutItemUnderDiscount();
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
			((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * (num2 - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
		if (((Control)(object)txtDiscountRatio).Text != "" && ((Control)(object)txtGrossValue).Text != "" && ((Control)(object)txtDiscountRatio).Text != "." && ((Control)(object)txtGrossValue).Text != "0" && Row.Cells["OfferID"].Value == DBNull.Value && Row.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItemsAndGroups.Select("ItemID=" + Row.Cells["ItemID"].Value.ToString())[0]["ExcludeCheckDiscount"].ToString()))
		{
			Row.Cells["DisCount"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m;
		}
		else
		{
			Row.Cells["DisCount"].Value = 0;
		}
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
		Row.Cells["ActualUnitSalesPrice"].Value = (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) + decimal.Parse(Row.Cells["TaxValue"].Value.ToString()) + decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) * (-1m * decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text)) / decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text)) / decimal.Parse(Row.Cells["Qty"].Value.ToString());
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
		CalculateNetTotals();
	}

	private void CalculateNetTotals()
	{
		((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse((((Control)(object)txtGrossValue).Text == "" || ((Control)(object)txtGrossValue).Text == ".") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) - decimal.Parse((((Control)(object)txtDiscountValue2).Text == "" || ((Control)(object)txtDiscountValue2).Text == ".") ? "0" : ((Control)(object)txtDiscountValue2).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		if (dtRoomData.Rows[0]["RoundingValue"] != DBNull.Value && decimal.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString()) > 0m)
		{
			decimal num = decimal.Parse(dtRoomData.Rows[0]["RoundingValue"].ToString());
			decimal num2 = Convert.ToDecimal(((Control)(object)txtNetprice).Text) % num;
			decimal num3 = num / 2m;
			decimal num4 = num - num2;
			if (num2 < num3)
			{
				((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - num2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				decimal num5 = num2 * -1m;
				((Control)(object)txtRoundingValue).Text = decimal.Parse(num5.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
			else
			{
				((Control)(object)txtNetprice).Text = decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) + num4).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((Control)(object)txtRoundingValue).Text = decimal.Parse(num4.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
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
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Expected O, but got Unknown
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Expected O, but got Unknown
		//IL_0cd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdf: Expected O, but got Unknown
		//IL_0d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d28: Expected O, but got Unknown
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string value = arrayList[1].ToString();
		string value2 = arrayList[2].ToString();
		string text2 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		if (dtItemsAndGroups.Select("IsMain=0 And ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		int num = int.Parse(dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString());
		if (GlobalFunctions.GetOption("UseScaleBarCode") && dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["IsWeight"] != DBNull.Value && bool.Parse(dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["IsWeight"].ToString()))
		{
			s = (decimal.Parse(s) / 1000m).ToString();
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()) == num && (dtItemsAndGroups.Select("ItemID =" + num)[0]["AccessoriesCount"] == DBNull.Value || int.Parse(dtItemsAndGroups.Select("ItemID =" + num)[0]["AccessoriesCount"].ToString()) == 0))
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				CheckLog = CheckLog + " ذيادة كمية صنف باركود X_" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
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
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtItemsAndGroups.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
		DataRow dataRow = dtItemsAndGroups.Select("IsOffer = 0 and ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = dataRow["StoreID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
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
			frmChecksDetailsAccessories2.ShowDialog();
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
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = value;
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = value2;
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
		if ((Adding || Updating) && ((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["OfferID"].Value == DBNull.Value && CheckID != 0 && drMaster != null && drMaster["IsPrinted"].Equals(false) && double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 1.0)
		{
			ULGData.ActiveCell = ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"];
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) - 1.0;
			CheckLog = CheckLog + " نقص كمية صنف  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Text + "X_ كمية  X_" + ((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString() + "X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		}
	}

	private void btnCloseCheck_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		if (cboDeliveryMan.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الطيار", "Please Select Delivery Man");
			return;
		}
		((UltraGridBase)ULGData).UpdateData();
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGData).Rows[i]);
		}
		CalculateTotalsTax();
		frmDeliveryChecksClosing frmDeliveryChecksClosing2 = new frmDeliveryChecksClosing(bool.Parse(dtRoomData.Rows[0]["AdditionalDiscountWithoutTax"].ToString()), (cboClient.SelectedIndex != -1) ? int.Parse(((TextEditorControlBase)cboClient).Value.ToString()) : 0, (cboClient.SelectedIndex == -1) ? DBNull.Value : dtClients.Select("ClientID=" + ((TextEditorControlBase)cboClient).Value.ToString())[0]["SubAccountID"], bool.Parse(dtPOSDefaultData.Rows[0]["GetPOSBalanceOnline"].ToString()));
		((UltraToggleEditorBase)frmDeliveryChecksClosing2.chkCash).Checked = !bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString());
		((Control)(object)frmDeliveryChecksClosing2.txtOnAccountAmount).Text = "0";
		((UltraToggleEditorBase)frmDeliveryChecksClosing2.chkOnAccount).Checked = bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString());
		((UltraToggleEditorBase)frmDeliveryChecksClosing2.chkVisa).Checked = false;
		((TextEditorControlBase)frmDeliveryChecksClosing2.cboVisaType).Value = DBNull.Value;
		((Control)(object)frmDeliveryChecksClosing2.txtVisaNo).Text = "";
		((Control)(object)frmDeliveryChecksClosing2.txtGrossValue).Text = ((Control)(object)txtGrossValue).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtDiscountValue).Text = ((Control)(object)txtDiscountValue).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtDiscountRatio).Text = ((Control)(object)txtDiscountRatio).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtDiscountValue2).Text = ((Control)(object)txtDiscountValue2).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtDiscountRatio2).Text = ((Control)(object)txtDiscountRatio2).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtDeliveryChargeValue).Text = ((Control)(object)txtDeliveryChargeValue).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtTaxTotalValue).Text = ((Control)(object)txtTaxTotalValue).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtNetprice).Text = ((Control)(object)txtNetprice).Text;
		((Control)(object)frmDeliveryChecksClosing2.txtCashAmount).Text = ((bool.Parse(dtRoomData.Rows[0]["NetPriceIsCashDefault"].ToString()) && !bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString())) ? ((Control)(object)frmDeliveryChecksClosing2.txtNetprice).Text : "0");
		if (bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString()))
		{
			UltraCheckEditor chkCash = frmDeliveryChecksClosing2.chkCash;
			bool enabled = (((Control)(object)frmDeliveryChecksClosing2.chkVisa).Enabled = false);
			((Control)(object)chkCash).Enabled = enabled;
			UltraTextEditor txtPaidAmount = frmDeliveryChecksClosing2.txtPaidAmount;
			string text = (((Control)(object)frmDeliveryChecksClosing2.txtOnAccountAmount).Text = ((Control)(object)txtNetprice).Text);
			((Control)(object)txtPaidAmount).Text = text;
			((Control)(object)frmDeliveryChecksClosing2.txtCashAmount).Enabled = false;
		}
		else
		{
			((Control)(object)frmDeliveryChecksClosing2.txtPaidAmount).Text = ((bool.Parse(dtRoomData.Rows[0]["NetPriceIsCashDefault"].ToString()) && !bool.Parse(dtRoomData.Rows[0]["WithoutSalesJV"].ToString())) ? ((Control)(object)frmDeliveryChecksClosing2.txtNetprice).Text : "0");
		}
		((Control)(object)frmDeliveryChecksClosing2.txtRestAmount).Text = (decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtNetprice).Text) - decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtPaidAmount).Text)).ToString();
		frmDeliveryChecksClosing2.ShowDialog();
		if (!frmDeliveryChecksClosing2.Cancel)
		{
			IsCash = ((UltraToggleEditorBase)frmDeliveryChecksClosing2.chkCash).Checked;
			IsVisa = ((UltraToggleEditorBase)frmDeliveryChecksClosing2.chkVisa).Checked;
			IsOnAccount = ((UltraToggleEditorBase)frmDeliveryChecksClosing2.chkOnAccount).Checked;
			CashAmount = decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtCashAmount).Text);
			OnAccountAmount = decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtOnAccountAmount).Text);
			VisaAmount = decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtVisaAmount).Text);
			VisatypeID = frmDeliveryChecksClosing2.VisaTypeID;
			VisaNo = ((Control)(object)frmDeliveryChecksClosing2.txtVisaNo).Text;
			PaidAmount = decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtPaidAmount).Text);
			RestAmount = decimal.Parse(((Control)(object)frmDeliveryChecksClosing2.txtRestAmount).Text);
			CheckClosed = true;
			CheckLog = CheckLog + " تم اقفال الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			PrintCheck();
			CheckLog = CheckLog + " تم طباعة اقفال الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			frmDeliveryChecksClosing2.Dispose();
		}
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
		if (!SaveClose(Close: false, DisplaySentQtyMessage: true))
		{
			return;
		}
		Font font = new Font("Times New Roman", 10f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 8f, FontStyle.Bold);
		Font font3 = new Font("Times New Roman", 8f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font5 = new Font("Times New Roman", 9f, FontStyle.Bold);
		Font font6 = new Font("Times New Roman", 10f, FontStyle.Bold);
		FastPrint instance = FastPrint.Instance;
		instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
		instance.GraphicsUnit = GraphicsUnit.Millimeter;
		instance.Margins = new Margins(0, 0, 0, 0);
		instance.OverallWidth = 70f;
		instance.AddTextCell(dtRoomData.Rows[0]["RoomName"].ToString(), font, 1f, 7f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (!Convert.ToBoolean(dtRoomData.Rows[0]["PrintWithoutCheckNo"]))
		{
			instance.AddTextCell(((Control)(object)txtCode).Text, font4, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("شيك رقم", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		instance.AddTextCell(dtpDate.DateTime.ToString(), font4, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("بتاريخ", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		instance.AddTextCell(GlobalVariables.UserName, font4, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("المستخدم", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		if (cboClient.SelectedIndex > -1)
		{
			instance.AddTextCell(((Control)(object)cboClient).Text, font4, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("العميل", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (cboClient.SelectedIndex > -1 && dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["MobileNumber"] != DBNull.Value)
		{
			instance.AddTextCell(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value.ToString())[0]["MobileNumber"].ToString(), font4, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("رقم المحمول", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (((Control)(object)txtNotes).Text.Trim().Length > 0)
		{
			instance.AddTextCell(((Control)(object)txtNotes).Text, font4, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("ملاحظات", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("إجمالي", font2, 0.18f, 5f);
		instance.AddTextCell("سعر الوحده", font2, 0.18f, 5f);
		instance.AddTextCell("الصنف", font2, 0.5f, 5f);
		instance.AddTextCell("الكميه", font2, 0.14f, 5f);
		instance.AcceptChanges();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			instance.AddTextCell(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.18f, 4f);
			instance.AddTextCell(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value.ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.18f, 4f);
			instance.AddTextCell(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Text.ToString(), font3, 0.5f, 4f);
			instance.AddTextCell(decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString(), NumberStyles.Float).ToString("0.##"), font3, 0.14f, 4f);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
		instance.AddTextCell(decimal.Parse(((Control)(object)txtGrossValue).Text, NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
		instance.AddTextCell("الإجمالي", font5, 0.5f, 5f, StringAlignment.Far);
		instance.AcceptChanges();
		if (decimal.Parse(((Control)(object)txtDiscountValue).Text) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(((Control)(object)txtDiscountValue).Text, NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("خصم", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(((Control)(object)txtTaxTotalValue).Text) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(((Control)(object)txtTaxTotalValue).Text, NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("الضريبه", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(((Control)(object)txtDiscountValue2).Text) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(((Control)(object)txtDiscountValue2).Text, NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("خصم2", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(((Control)(object)txtRoundingValue).Text) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(((Control)(object)txtRoundingValue).Text, NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("تقريب", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
		instance.AddTextCell(decimal.Parse(((Control)(object)txtNetprice).Text, NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: true, Color.Black, 0.5f);
		instance.AddTextCell("الصافي", font6, 0.5f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Black, 0.5f);
		instance.AcceptChanges();
		if (drMaster != null)
		{
			if (decimal.Parse(drMaster["OnAccountAmount"].ToString()) > 0m)
			{
				instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
				instance.AddTextCell(decimal.Parse(drMaster["OnAccountAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
				instance.AddTextCell("على الحساب", font5, 0.5f, 5f, StringAlignment.Far);
				instance.AcceptChanges();
			}
			if (decimal.Parse(drMaster["CashAmount"].ToString()) > 0m)
			{
				instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
				instance.AddTextCell(decimal.Parse(drMaster["CashAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
				instance.AddTextCell("نقدي", font5, 0.5f, 5f, StringAlignment.Far);
				instance.AcceptChanges();
			}
			if (decimal.Parse(drMaster["VisaAmount"].ToString()) > 0m)
			{
				instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
				instance.AddTextCell(decimal.Parse(drMaster["VisaAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
				instance.AddTextCell("فيزا", font5, 0.5f, 5f, StringAlignment.Far);
				instance.AcceptChanges();
			}
		}
		else if (decimal.Parse(PaidAmount.ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(PaidAmount.ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("المدفوع", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(((Control)(object)txtNetprice).Text) - PaidAmount != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse((decimal.Parse(((Control)(object)txtNetprice).Text) - PaidAmount).ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("المتبقى", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 2f, DrawRectangle: false);
		instance.AcceptChanges();
		if (drMaster != null && decimal.Parse(drMaster["OnAccountAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell("......................", font5, 0.5f, 5f);
			instance.AddTextCell("التوقيع :", font5, 0.3f, 5f);
			instance.AcceptChanges();
		}
		instance.AddTextCell(DateTime.Now.ToString(), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dtRoomData.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dtRoomData.Rows[0]["Message"].ToString(), font3, 1f, 6f, StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		try
		{
			instance.PrinterSettings.Copies = short.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString());
			if (instance.PrinterSettings.Copies > 0)
			{
				instance.Print();
			}
			CheckLog = CheckLog + " تم طباعة الشيك   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
			drMaster["IsPrinted"] = true;
			drMaster["PrintUserID"] = GlobalVariables.UserID;
			drMaster["PrintDate"] = DateTime.Now;
			btnOKClick();
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
		}
	}

	public void PrintCheck()
	{
		if (!SaveClose(Close: false, DisplaySentQtyMessage: false) || !(RowID != ""))
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
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			GlobalVariables.ReportDocument = null;
			drMaster["IsPrinted"] = true;
			drMaster["PrintUserID"] = GlobalVariables.UserID;
			drMaster["DeliveryManID"] = ((TextEditorControlBase)cboDeliveryMan).Value.ToString();
			DataRow dataRow = drMaster;
			object value = (drMaster["DeliveryStartDate"] = DateTime.Now);
			dataRow["PrintDate"] = value;
			SaveClose(Close: true, DisplaySentQtyMessage: false);
		}
		else
		{
			DataRow[] array = dtReports.Select("ReportID=" + (dtRoomData.Rows[0]["DeliveryReportID"].Equals(DBNull.Value) ? "0" : dtRoomData.Rows[0]["DeliveryReportID"]));
			if (array.Length != 0 && array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "FastPrintDeliveryDetails")
			{
				FastPrintCheck();
				return;
			}
			ReportDocument reportDocument = new ReportDocument();
			try
			{
				if (array.Length != 0)
				{
					reportDocument.Load(GlobalVariables.ReportsPath + array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				}
				else
				{
					reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ChecksDelivery_A.rpt" : "Rep_POS_ChecksDelivery_E.rpt"));
				}
				GlobalFunctions.ConfigureReport(reportDocument);
				reportDocument.SetParameterValue("@CheckIDs", "," + RowID + ",");
				reportDocument.SetParameterValue("@RoomID", RoomID.ToString());
				reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
				reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show(ex.Message, ex.Message);
				btnCancelClick();
				reportDocument.Dispose();
				GC.Collect();
				return;
			}
			try
			{
				drMaster["IsPrinted"] = true;
				drMaster["PrintUserID"] = GlobalVariables.UserID;
				drMaster["DeliveryManID"] = ((cboDeliveryMan.SelectedIndex == -1) ? DBNull.Value : ((TextEditorControlBase)cboDeliveryMan).Value);
				DataRow dataRow2 = drMaster;
				object value = (drMaster["DeliveryStartDate"] = DateTime.Now);
				dataRow2["PrintDate"] = value;
				SaveClose(Close: true, DisplaySentQtyMessage: false);
				CheckLog = CheckLog + " تم طباعة الشيك   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
				if (CheckClosed)
				{
					int num = int.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString());
					if (num > 0)
					{
						reportDocument.PrintToPrinter(num, collated: true, 0, 10000);
					}
				}
				else
				{
					reportDocument.PrintToPrinter(int.Parse(dtRoomData.Rows[0]["PrintCheckCount"].ToString()), collated: true, 0, 10000);
				}
			}
			catch (Exception ex2)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex2.Message, "Check Printer Cable\n" + ex2.Message);
				btnCancelClick();
			}
			reportDocument.Dispose();
		}
		GC.Collect();
	}

	public override void btnPrintClick()
	{
		if (dtRoomData.Rows.Count > 0 && bool.Parse(dtRoomData.Rows[0]["PrintCheck"].ToString()))
		{
			PrintCheck();
			CheckLog = CheckLog + " تم طباعة الشيك  X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
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
			reportDocument.SetParameterValue("@TableCode", "");
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
			reportDocument.SetParameterValue("@TableCode", "");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.SetParameterValue("@CheckID", RowID, "Rep_POS_ChecksDetailsAccessoriesSendToPrinters");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_POS_ChecksDetailsAccessoriesSendToPrinters");
			reportDocument.SetParameterValue("@CheckIDs", "," + RowID + ",", "Rep_POS_ChecksDetailsAdditionals");
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_POS_ChecksDetailsAdditionals");
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

	private void btnClear_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraGridBase)ULGData).ActiveRow.Delete(false);
		}
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
		decimal totalamount = CalculateGrossWithoutItemUnderDiscount();
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(totalamount, decimal.Parse(((Control)(object)txtDiscountValue).Text), decimal.Parse(((Control)(object)txtDiscountRatio).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscountRatio).Text = ((cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString() : UserSalesDiscount.ToString());
			((Control)(object)txtDiscountValue).Text = Math.Round(decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * CalculateGrossWithoutItemUnderDiscount(), 2).ToString();
			return;
		}
		((Control)(object)txtDiscountRatio).Text = string.Concat(frmChangeDiscount2.DiscountRatio);
		((Control)(object)txtDiscountValue).Text = string.Concat(frmChangeDiscount2.DiscountValue);
		DiscountUserID = frmChangeDiscount2.UserID;
		CheckLog = CheckLog + " تم تغيير نسبة الخصم   X_ مستخدم  X_" + GlobalVariables.UserName + "X_ بتاريخ X_" + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "X_^^^";
		frmChangeDiscount2.Dispose();
	}

	private decimal CalculateGrossWithoutItemUnderDiscount()
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

	private void txtDiscountValue_ValueChanged(object sender, EventArgs e)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountRatio).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "0" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) / num * 100m, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount))
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

	private void txtDiscountRatio_ValueChanged(object sender, EventArgs e)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		if (!Adding && !Updating)
		{
			return;
		}
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		if (num > 0m)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((Control)(object)txtDiscountValue).Text = decimal.Parse(Math.Round(decimal.Parse((((Control)(object)txtDiscountRatio).Text == "" || ((Control)(object)txtDiscountRatio).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio).Text) / 100m * num, 2).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			if ((cboClient.SelectedIndex > -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount) || (cboClient.SelectedIndex == -1 && decimal.Parse((((Control)(object)txtDiscountRatio).Text == "") ? "0" : ((Control)(object)txtDiscountRatio).Text) > UserSalesDiscount))
			{
				OpenChangeDiscountForm();
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				CalculateRow(((UltraGridBase)ULGData).Rows[i]);
			}
			CalculateTotalsTax();
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

	private void frmDeliveryChecks_Load(object sender, EventArgs e)
	{
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
		decimal num = CalculateGrossWithoutItemUnderDiscount();
		frmChangeDiscount frmChangeDiscount2 = new frmChangeDiscount(num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text), decimal.Parse(((Control)(object)txtDiscountValue2).Text), decimal.Parse(((Control)(object)txtDiscountRatio2).Text));
		frmChangeDiscount2.WindowState = FormWindowState.Normal;
		frmChangeDiscount2.ShowDialog();
		if (frmChangeDiscount2.Cancel)
		{
			((Control)(object)txtDiscountRatio2).Text = (((AdditionalDiscountIncludeTax || AdditionalDiscountWithoutTax) && !ShowFirstDiscount && cboClient.SelectedIndex > -1 && decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()) > UserSalesDiscount) ? decimal.Parse(dtClients.Select(" ClientID= " + ((TextEditorControlBase)cboClient).Value)[0]["DiscountPercentage"].ToString()).ToString("0.##########") : decimal.Parse(UserSalesDiscount.ToString()).ToString(GlobalVariables.txtDecimalFormate));
			if (AdditionalDiscountIncludeTax)
			{
				((Control)(object)txtDiscountValue2).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDiscountRatio2).Text == "" || ((Control)(object)txtDiscountRatio2).Text == ".") ? "0" : ((Control)(object)txtDiscountRatio2).Text) / 100m * (num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text))).ToString()).ToString(GlobalVariables.txtDecimalFormate);
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
			decimal num = CalculateGrossWithoutItemUnderDiscount();
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
			((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
			if (AdditionalDiscountIncludeTax)
			{
				decimal num2 = num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text);
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
			decimal num = CalculateGrossWithoutItemUnderDiscount();
			((TextEditorControlBase)txtDiscountValue2).ValueChanged -= txtDiscountValue2_ValueChanged;
			((TextEditorControlBase)txtDiscountRatio2).ValueChanged -= txtDiscountRatio2_ValueChanged;
			if (AdditionalDiscountIncludeTax)
			{
				decimal num2 = num - decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == ".") ? "0" : ((Control)(object)txtDiscountValue).Text) + decimal.Parse((((Control)(object)txtDeliveryChargeValue).Text == "" || ((Control)(object)txtDeliveryChargeValue).Text == ".") ? "0" : ((Control)(object)txtDeliveryChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "" || ((Control)(object)txtTaxTotalValue).Text == ".") ? "0" : ((Control)(object)txtTaxTotalValue).Text);
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
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And IsDelivery=1");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsDelivery=1 ", "0");
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsDelivery=1 ", "1");
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

	private void btnDeliveryChargeValue_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtDeliveryChargeValue, ((Control)(object)txtDeliveryChargeValue).Text);
		frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
		frmDecimal2.Show();
	}

	private void txtDeliveryChargeValue_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtDeliveryChargeValue).Text != "" && decimal.Parse(((Control)(object)txtDeliveryChargeValue).Text) > 0m)
		{
			CalculateNetTotals();
		}
	}

	private void cboClient_ValueChanged(object sender, EventArgs e)
	{
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Expected O, but got Unknown
		//IL_0912: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Expected O, but got Unknown
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
		object obj2 = dtChecksDetailsAddtionals.Compute(" Sum(TotalPrice) ", "");
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value != DBNull.Value)
			{
				if (obj2 != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["AdditionalPrice"].Value = decimal.Parse(obj2.ToString());
				}
				DataRow dataRow = dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value = decimal.Parse(dataRow["Price"].ToString()) - decimal.Parse(dataRow["Price"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["OfferDiscountRatio"].Value.ToString()) / 100m;
				((UltraGridBase)ULGData).Rows[k].Cells["RoomDiscountValue"].Value = decimal.Parse(dtItemsAndGroups.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0]["RoomDiscountValue"].ToString());
				((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["AdditionalPrice"].Value.ToString());
				((UltraGridBase)ULGData).Rows[k].Cells["ServiceChargeAmount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[k].Cells["TotalPrice"].Value.ToString()) * decimal.Parse(dtRoomData.Rows[0]["ServiceChargePercent"].ToString()) / 100m;
				CalculateRow(((UltraGridBase)ULGData).Rows[k]);
			}
		}
		CalculateGoss();
		CalculateTotalsTax();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Checks.GetCodeByBranchID("0", "1", "0", dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		frmChecksDetailsAccessories2.ShowDialog();
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
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Expected O, but got Unknown
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
		frmChecksDetailsAdditionals2.ShowDialog();
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Expected O, but got Unknown
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0709: Expected O, but got Unknown
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Expected O, but got Unknown
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Expected O, but got Unknown
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Expected O, but got Unknown
		//IL_1221: Unknown result type (might be due to invalid IL or missing references)
		//IL_122b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmDeliveryChecks));
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
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblDeliveryChargeValue = new UltraLabel();
		this.txtDeliveryChargeValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.btnPlus = new UltraButton();
		this.btnSubtract = new UltraButton();
		this.lblDiscountValue = new UltraLabel();
		this.txtDiscountValue = new UltraTextEditor();
		this.btnCloseCheck = new UltraButton();
		this.btnSent = new UltraButton();
		this.btnClear = new UltraButton();
		this.lblDiscountRatio = new UltraLabel();
		this.txtDiscountRatio = new UltraTextEditor();
		this.btnDiscountRatio = new UltraButton();
		this.btnDiscountValue = new UltraButton();
		this.btnDeliveryChargeValue = new UltraButton();
		this.txtInvoiceNo = new UltraTextEditor();
		this.chkIsInvoice = new UltraCheckEditor();
		this.lblRoundingValue = new UltraLabel();
		this.txtRoundingValue = new UltraTextEditor();
		this.lblDeliveryMan = new UltraLabel();
		this.cboDeliveryMan = new UltraComboEditor();
		this.btnAccessories = new UltraButton();
		this.btnAddtionals = new UltraButton();
		this.UTCItemsGroups = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.btnDiscountValue2 = new UltraButton();
		this.btnDiscountRatio2 = new UltraButton();
		this.lblDiscountRatio2 = new UltraLabel();
		this.txtDiscountRatio2 = new UltraTextEditor();
		this.lblDiscountValue2 = new UltraLabel();
		this.txtDiscountValue2 = new UltraTextEditor();
		this.dtpClientDeliveryDate = new UltraDateTimeEditor();
		this.lblClientDeliveryDate = new UltraLabel();
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
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsInvoice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpClientDeliveryDate).BeginInit();
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
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblDeliveryChargeValue, "lblDeliveryChargeValue");
		this.lblDeliveryChargeValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryChargeValue).Name = "lblDeliveryChargeValue";
		((ControlBase)this.lblDeliveryChargeValue).WrapText = false;
		resources.ApplyResources(this.txtDeliveryChargeValue, "txtDeliveryChargeValue");
		((System.Windows.Forms.Control)(object)this.txtDeliveryChargeValue).Name = "txtDeliveryChargeValue";
		((EditorButtonControlBase)this.txtDeliveryChargeValue).ReadOnly = true;
		((TextEditorControlBase)this.txtDeliveryChargeValue).ValueChanged += new System.EventHandler(txtDeliveryChargeValue_ValueChanged);
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
		resources.ApplyResources(this.btnDeliveryChargeValue, "btnDeliveryChargeValue");
		((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue).Name = "btnDeliveryChargeValue";
		((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue).Click += new System.EventHandler(btnDeliveryChargeValue_Click);
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
		resources.ApplyResources(this.lblDeliveryMan, "lblDeliveryMan");
		this.lblDeliveryMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryMan).Name = "lblDeliveryMan";
		((ControlBase)this.lblDeliveryMan).WrapText = false;
		resources.ApplyResources(this.cboDeliveryMan, "cboDeliveryMan");
		((TextEditorControlBase)this.cboDeliveryMan).AlwaysInEditMode = true;
		this.cboDeliveryMan.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDeliveryMan).Name = "cboDeliveryMan";
		resources.ApplyResources(this.btnAccessories, "btnAccessories");
		((System.Windows.Forms.Control)(object)this.btnAccessories).Name = "btnAccessories";
		((System.Windows.Forms.Control)(object)this.btnAccessories).Click += new System.EventHandler(btnAccessories_Click);
		resources.ApplyResources(this.btnAddtionals, "btnAddtionals");
		((System.Windows.Forms.Control)(object)this.btnAddtionals).Name = "btnAddtionals";
		((System.Windows.Forms.Control)(object)this.btnAddtionals).Click += new System.EventHandler(btnAddtionals_Click);
		resources.ApplyResources(this.UTCItemsGroups, "UTCItemsGroups");
		resources.ApplyResources(val10, "appearance10");
		((AppearanceBase)val10).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCItemsGroups).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Name = "UTCItemsGroups";
		((UltraTabControlBase)this.UTCItemsGroups).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.btnDiscountValue2, "btnDiscountValue2");
		((System.Windows.Forms.Control)(object)this.btnDiscountValue2).Name = "btnDiscountValue2";
		((System.Windows.Forms.Control)(object)this.btnDiscountValue2).Click += new System.EventHandler(btnDiscountValue2_Click);
		resources.ApplyResources(this.btnDiscountRatio2, "btnDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio2).Name = "btnDiscountRatio2";
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio2).Click += new System.EventHandler(btnDiscountRatio2_Click);
		resources.ApplyResources(this.lblDiscountRatio2, "lblDiscountRatio2");
		this.lblDiscountRatio2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio2).Name = "lblDiscountRatio2";
		((ControlBase)this.lblDiscountRatio2).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio2, "txtDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio2).Name = "txtDiscountRatio2";
		((EditorButtonControlBase)this.txtDiscountRatio2).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountRatio2).ValueChanged += new System.EventHandler(txtDiscountRatio2_ValueChanged);
		resources.ApplyResources(this.lblDiscountValue2, "lblDiscountValue2");
		this.lblDiscountValue2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue2).Name = "lblDiscountValue2";
		((ControlBase)this.lblDiscountValue2).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue2, "txtDiscountValue2");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue2).Name = "txtDiscountValue2";
		((EditorButtonControlBase)this.txtDiscountValue2).ReadOnly = true;
		((TextEditorControlBase)this.txtDiscountValue2).ValueChanged += new System.EventHandler(txtDiscountValue2_ValueChanged);
		resources.ApplyResources(this.dtpClientDeliveryDate, "dtpClientDeliveryDate");
		((UltraWinEditorMaskedControlBase)this.dtpClientDeliveryDate).AlwaysInEditMode = true;
		this.dtpClientDeliveryDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpClientDeliveryDate).Name = "dtpClientDeliveryDate";
		this.dtpClientDeliveryDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblClientDeliveryDate, "lblClientDeliveryDate");
		this.lblClientDeliveryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientDeliveryDate).Name = "lblClientDeliveryDate";
		((ControlBase)this.lblClientDeliveryDate).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCItemsGroups);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddtionals);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccessories);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDeliveryMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoundingValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsInvoice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubtract);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPlus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpClientDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItemData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliveryChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCloseCheck);
		base.Name = "frmDeliveryChecks";
		base.Load += new System.EventHandler(frmDeliveryChecks_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCloseCheck, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliveryChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItemData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpClientDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPlus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubtract, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDeliveryChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsInvoice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoundingValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDeliveryMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccessories, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddtionals, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCItemsGroups, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue2, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsInvoice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoundingValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveryMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpClientDeliveryDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
