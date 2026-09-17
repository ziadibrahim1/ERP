using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.LensesProductions;
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

public class frmBlankLensesReturnReceived : frmPosted
{
	private DataTable dtBlanksInvoicesDetails;

	private DataTable dtItems;

	private DataTable dtStores;

	private DataSet ds;

	private ValueList vlItems = new ValueList();

	private ValueList vlStores = new ValueList();

	private IContainer components = null;

	public frmBlankLensesReturnReceived()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		fromServer = true;
		NoCol = "BlankInvoiceNo";
	}

	public override void FillGrid()
	{
		DisplayDataDate = GlobalFunctions.GetServerDateTimeNow(IsFromServer: true);
		dtItems = Items.FillComboWithBlankType("-1", "-1", "0", "-1", "1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "0", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlStores.ValueListItems.Clear();
		for (int j = 0; j < dtStores.Rows.Count; j++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
		}
		dtsource = BlanksInvoices.SelectByReturnReceived("," + GlobalVariables.CurrentBranchID + ",", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtBlanksInvoicesDetails = BlanksInvoicesDetails.SelectByReturnReceived("," + GlobalVariables.CurrentBranchID + ",", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinalReturned"].Header).Caption = (GlobalVariables.IsArabic ? "إرتجاع نهائي" : "Final Return");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الارتجاع" : "Return Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsReturnReceived"].Header).Caption = (GlobalVariables.IsArabic ? "استلام المرتجع" : "Return Received");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnReceivedDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ استلام المرتجع" : "Return Received Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Return Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن المرتجع" : "Return Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsReturnReceived"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ReturnReceivedDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinalReturned"].Hidden = false;
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsReturnReceived") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BlankItemID"))
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		if (!ValidateData())
		{
			return;
		}
		string text = "";
		string text2 = "";
		string text3 = "";
		bool flag = true;
		bool flag2 = true;
		DataTable dataTable = BlankInvoiceFactoryReturnDetails.SelectByBlankInvoiceFactoryReturnID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dataTable.Clear();
		string text4 = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			flag = true;
			flag2 = true;
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsReturnReceived"].Value.ToString()))
				{
					if (text4 == "")
					{
						text4 = DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate);
					}
					DataRow dataRow = dataTable.NewRow();
					dataRow["BlankInvoiceFactoryReturnDetailID"] = -1;
					dataRow["BlankInvoiceFactoryReturnID"] = -1;
					dataRow["BlankInvoiceDetailID"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankInvoiceDetailID"].Value;
					dataRow["BlankItemID"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value;
					dataRow["Qty"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value;
					dataRow["StoreID"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value;
					dataRow["VoucherDate"] = DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate);
					dataTable.Rows.Add(dataRow);
					text3 = text3 + " Update Lns_BlanksInvoicesDetails Set " + (bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsFinalReturned"].Value.ToString()) ? " IsReturnReceived = 1 ," : "  IsReturned =0 , ReturnDate = Null, ReturnReasonID = Null ,") + " ReturnReceivedDate = '" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate) + "'  Where BlankInvoiceDetailID  = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankInvoiceDetailID"].Value.ToString() + ";";
					if (flag2 && !bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsFinalReturned"].Value.ToString()))
					{
						text2 = text2 + " Update Lns_BlanksInvoices Set IsFactorySent = 0 , FactorySentDate = Null, IsBranchReceive = 0 , BranchReceiveDate = Null, IsDeliverd = 0 , DeliverdDate = Null  Where BlankInvoiceID  = " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + ";";
						flag2 = false;
					}
					if (flag)
					{
						text = text + " exec SP_Trans_Log " + GlobalVariables.UserID + " ,'Lns_BlanksInvoices' ," + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + " ,'U';";
						flag = false;
					}
				}
			}
		}
		if (text3 != ",")
		{
			string codeByBranchID = BlankInvoiceFactoryReturns.GetCodeByBranchID(text4, GlobalVariables.CurrentBranchID, IsFromServer: true);
			int num = BlankInvoiceFactoryReturns.Insert_Update("-1", codeByBranchID, text4, "", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				dataTable.Rows[k]["BlankInvoiceFactoryReturnID"] = num;
				dataTable.Rows[k]["Notes"] = "";
				dataTable.Rows[k]["Deleted"] = false;
				dataTable.Rows[k]["BranchID"] = GlobalVariables.CurrentBranchID;
			}
			BlankInvoiceFactoryReturnDetails.Insert_UpdateByTable(dataTable, GlobalVariables.UserID, IsFromServer: true);
			Main.SyncExecuteNonQuery(text3);
			if (text2 != "")
			{
				Main.SyncExecuteNonQuery(text2);
			}
			Main.SyncExecuteNonQuery(text);
			ItemsTransactions.ManageInThread();
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

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsReturnReceived"].Value))
				{
					if (Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "Lns_BlanksInvoices", IsFromServer: true))
					{
						GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show(" لابد من إختيار تاريخ استلام المرتجع ", "Please Select Return Received Date");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					if (Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"].Value) < Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnDate"].Value))
					{
						GlobalVariables.InformationMB.Show("تاريخ استلام المرتجع قبل تاريخ الارتجاع", "The Return Received Date is Before The Return Date. ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ReturnReceivedDate"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show(" لابد من إختيار مخزن المرتجع ", "Please Select Return Store");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["StoreID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show(" لابد من إختيار الصنف المرتجع ", "Please Select Returned Item");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"];
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.LensesProductions.Approved.frmBlankLensesReturnReceived));
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
		base.Name = "frmBlankLensesReturnReceived";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
