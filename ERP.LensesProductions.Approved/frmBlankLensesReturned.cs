using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using BusinessLayer.General;
using BusinessLayer.LensesProductions;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.LensesProductions.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.LensesProductions.Approved;

public class frmBlankLensesReturned : frmPosted
{
	private bool IsCentralDatabase = false;

	private DataTable dtBlanksInvoicesDetails;

	private DataTable dtReturnReasons;

	private DataTable dtPOSDefaultData;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private ValueList vlReturnReasons = new ValueList();

	private DataSet ds;

	private IContainer components = null;

	public frmBlankLensesReturned()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		InitializeComponent();
		fromServer = false;
		NoCol = "BlankInvoiceNo";
		if (Main.IsSynchronization)
		{
			if (GlobalVariables.dtSyncConn == null || GlobalVariables.dtSyncConn.Rows.Count == 0)
			{
				GlobalVariables.dtSyncConn = SyncConnection.Select("-1", "-1", "0");
			}
			if (GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value) && !bool.Parse(Branches.Select(GlobalVariables.CurrentBranchID, GlobalVariables.BranchIDs, "0", IsFromServer: false).Rows[0]["IsMainBranch"].ToString()))
			{
				IsCentralDatabase = true;
			}
		}
	}

	public override void FillGrid()
	{
		DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
		dtPOSDefaultData = Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		dtReturnReasons = ReturnReasons.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlReturnReasons.ValueListItems.Clear();
		for (int i = 0; i < dtReturnReasons.Rows.Count; i++)
		{
			vlReturnReasons.ValueListItems.Add(dtReturnReasons.Rows[i]["ReturnReasonID"], dtReturnReasons.Rows[i]["ReturnReasonName"].ToString());
		}
		dtsource = BlanksInvoices.SelectByReturned("," + GlobalVariables.CurrentBranchID + ",", "0", (GlobalFunctions.GetDefault("ClientReturnPeriod") == "") ? "-1" : GlobalFunctions.GetDefault("ClientReturnPeriod"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtBlanksInvoicesDetails = BlanksInvoicesDetails.SelectByReturned("," + GlobalVariables.CurrentBranchID + ",", "0", (GlobalFunctions.GetDefault("ClientReturnPeriod") == "") ? "-1" : GlobalFunctions.GetDefault("ClientReturnPeriod"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		ds = new DataSet();
		ds.Tables.Add(dtsource);
		ds.Tables.Add(dtBlanksInvoicesDetails);
		ds.Tables[0].TableName = "dtsource";
		ds.Tables[1].TableName = "dtBlanksInvoicesDetails";
		ds.Relations.Add(ds.Tables[0].Columns["BlankInvoiceID"], ds.Tables[1].Columns["BlankInvoiceID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Header).Caption = (GlobalVariables.IsArabic ? "الخط" : "Line");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التسليم" : "Deliverd Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "خامة التصنيع" : "Blank Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Blank Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsRightEye"].Header).Caption = (GlobalVariables.IsArabic ? "Eye" : "Eye");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DColorName"].Header).Caption = "D- " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DItemSizeName"].Header).Caption = "D- " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorName"].Header).Caption = "R- " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeName"].Header).Caption = "R- " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Addition"].Header).Caption = "Add";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Ax"].Header).Caption = "Ax";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IPD"].Header).Caption = "IPD";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PH"].Header).Caption = "PH";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MM"].Header).Caption = "MM";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "ارسال مرتجع" : "Send Return");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinalReturned"].Header).Caption = (GlobalVariables.IsArabic ? "ارتجاع نهائي" : "Final Return");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الارتجاع" : "Return Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnReasonID"].Header).Caption = (GlobalVariables.IsArabic ? "سبب الإرتجاع" : "Return Reason");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnReasonID"].ValueList = (IValueList)(object)vlReturnReasons;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankTypeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsRightEye"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DColorName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DItemSizeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Addition"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Ax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IPD"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PH"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MM"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsReturned"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinalReturned"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnReasonID"].Hidden = false;
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsReturned") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsFinalReturned") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ReturnReasonID"))
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		if (ds.Tables[1].Select("IsReturned = 1").Length == 0 || !ValidateForShift() || !ValidateData())
		{
			return;
		}
		string text = "";
		string text2 = "";
		bool flag = true;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			flag = true;
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsReturned"].Value.ToString()))
				{
					text2 = text2 + " Update Lns_BlanksInvoicesDetails Set IsReturned =1, ReturnDate = '" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate) + "' , ReturnReasonID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReasonID"].Value.ToString() + " ,ShiftDetailID =  " + ShiftDetailID + " ,ShiftDetailUserID =  " + ShiftDetailUserID + " , IsFinalReturned = " + (bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsFinalReturned"].Value.ToString()) ? "1" : "0") + " Where BlankInvoiceDetailID  = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankInvoiceDetailID"].Value.ToString() + ";";
					if (flag)
					{
						text = text + " exec SP_Trans_Log " + GlobalVariables.UserID + " ,'Lns_BlanksInvoices' ," + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + " ,'U';";
						flag = false;
					}
				}
			}
		}
		if (text2 != ",")
		{
			Main.ExecuteNonQuery(text2);
			Main.ExecuteNonQuery(text);
			ShiftsDetails.ReturnJVRegenerate(ShiftDetailID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			FillGrid();
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			frmBlanksInvoices frmBlanksInvoices2 = new frmBlanksInvoices(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BlankInvoiceID"].Value.ToString()), _fromApprovalForm: true);
			frmBlanksInvoices2.Size = new Size(base.Width, base.Height);
			frmBlanksInvoices2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmBlanksInvoices2.lblTitle).Text = (GlobalVariables.IsArabic ? "فاتورة التصنيعات" : "Blanks Invoices");
			frmBlanksInvoices2.ShowDialog();
		}
	}

	public override void AfterSelectChange()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
	}

	public bool ValidateForShift()
	{
		DataTable dataTable = ds.Tables[1];
		object obj = dataTable.Compute("Min(ReturnDate)", "IsReturned = 1 and ReturnDate is not null");
		if (IsCentralDatabase)
		{
			GlobalVariables.InformationMB.Show("لا يمكن ارتجاع هذه الحركه من المركزيه برجاء ارتجاعها من الفرع ", "You Cannot Return From The Central DataBase, You Must Return From Branch.");
			return false;
		}
		DataTable dataTable2 = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable2.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return false;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable2.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable2.Rows[0]["ShiftPeriod"].ToString()).Second);
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
		if (DateTime.Parse(obj.ToString()) < DateTime.Parse(dataTable2.Rows[0]["StartDate"].ToString()))
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return false;
		}
		DataTable dataTable3 = ShiftsDetailsUsers.SelectNotClosed(dataTable2.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable2.Rows[0]["ShiftDetailID"].ToString();
		if (dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["CurrencyID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عملة الفرع من إعدادات البيع المباشر ", "Please Enter Default Currency From POS Settings");
			return false;
		}
		DataTable dataTable4 = Currency.FillCurrencyByDate(DateTime.Parse(obj.ToString()).ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (decimal.Parse(dataTable4.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString()) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال سعر الصرف" : "Please Enter The Exchange Rate");
			return false;
		}
		if (dataTable3.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", dtPOSDefaultData.Rows[0]["CurrencyID"].ToString(), dataTable4.Select(" CurrencyID=  " + dtPOSDefaultData.Rows[0]["CurrencyID"].ToString())[0]["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable3.Rows[0]["ShiftDetailUserID"].ToString();
		}
		return true;
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsReturned"].Value))
				{
					if (Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "Lns_BlanksInvoices"))
					{
						GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnDate"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show(" لابد من إختيار تاريخ الارتجاع ", "Please Select Return Date");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnDate"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					if (Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnDate"].Value) < Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value))
					{
						GlobalVariables.InformationMB.Show("تاريخ الارتجاع قبل تاريخ التسليم", "The Return Date is Before The Delivery Date. ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnDate"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReasonID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show(" لابد من إختيار سبب الارتجاع  ", "Please Select Return Reason");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReasonID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
				}
			}
		}
		return true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.LensesProductions.Approved.frmBlankLensesReturned));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmBlankLensesReturned";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
