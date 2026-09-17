using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.MasterData;

public class frmUpdateRoomItems : frmDetails
{
	private DataTable dtRooms;

	private DataTable dtItems;

	private DataTable dtTaxs;

	private DataTable dtStores;

	private ValueList vlStores = new ValueList();

	private ValueList vlTaxs = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlPOSPrinter = new ValueList();

	private ValueList vlPickupPrinter = new ValueList();

	private ValueList vlCanModifyPrice = new ValueList();

	private IContainer components = null;

	private UltraButton btnChangeItems;

	public frmUpdateRoomItems()
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
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الصالة" : "Room");
	}

	public override void PrepareData()
	{
		dtRooms = Rooms.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtRooms, "RoomID", "RoomName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlStores.ValueListItems.Clear();
		for (int j = 0; j < dtStores.Rows.Count; j++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
		}
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlTaxs.ValueListItems.Clear();
		for (int k = 0; k < dtTaxs.Rows.Count; k++)
		{
			vlTaxs.ValueListItems.Add(dtTaxs.Rows[k]["TaxID"], dtTaxs.Rows[k]["TaxName"].ToString());
		}
		foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
		{
			vlPOSPrinter.ValueListItems.Add((object)installedPrinter, installedPrinter);
			vlPickupPrinter.ValueListItems.Add((object)installedPrinter, installedPrinter);
		}
		vlCanModifyPrice.ValueListItems.Clear();
		vlCanModifyPrice.ValueListItems.Add((object)(-1), GlobalVariables.IsArabic ? "إفتراضى" : "Default");
		vlCanModifyPrice.ValueListItems.Add((object)0, GlobalVariables.IsArabic ? "لايمكن تعديل السعر" : "Cannot Modify Price");
		vlCanModifyPrice.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "يمكن تعديل السعر" : "Can Modify Price");
		dtDetails = RoomsItems.SelectByRoomID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomItemID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlTaxs;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["POSPrinter"].Header).Caption = (GlobalVariables.IsArabic ? "الطابعة" : "Printer");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["POSPrinter"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["POSPrinter"].ValueList = (IValueList)(object)vlPOSPrinter;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["POSPrinter"].Width = (int)((double)((Control)(object)ULGData).Width * 0.125);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PickupPrinter"].Header).Caption = (GlobalVariables.IsArabic ? "طابعة التجميع" : "Pickup Printer");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PickupPrinter"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PickupPrinter"].ValueList = (IValueList)(object)vlPickupPrinter;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PickupPrinter"].Width = (int)((double)((Control)(object)ULGData).Width * 0.125);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountPercentage"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الخصم" : "Discount%");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountPercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountPercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Header).Caption = (GlobalVariables.IsArabic ? "تعديل السعر" : "Modify Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].ValueList = (IValueList)(object)vlCanModifyPrice;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExcludeCheckDiscount"].Header).Caption = (GlobalVariables.IsArabic ? "مستثنى من خصم الشيك" : "Exclude Check Discount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExcludeCheckDiscount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExcludeCheckDiscount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExcludeCheckDiscount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnforceAccessories"].Header).Caption = (GlobalVariables.IsArabic ? "الملحقات إجباري" : "Enforce Accessories");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnforceAccessories"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnforceAccessories"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnforceAccessories"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex <= -1)
		{
			return;
		}
		dtDetails = RoomsItems.SelectByRoomID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		int branchID = int.Parse(dtRooms.Select(" RoomID= " + ((TextEditorControlBase)cboHeader).Value.ToString())[0]["BranchID"].ToString());
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			ValueList storesValueList = getStoresValueList(branchID);
			((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].ValueList = (IValueList)(object)storesValueList;
			if (((DisposableObjectCollectionBase)storesValueList.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = DBNull.Value;
			}
		}
	}

	private ValueList getStoresValueList(int BranchID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtStores.Select("BranchID=" + BranchID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["StoreID"].ToString(), array[i]["StoreName"].ToString());
		}
		return val;
	}

	private void btnChangeItems_Click(object sender, EventArgs e)
	{
		if (cboHeader.SelectedIndex > -1)
		{
			frmUpdateRoomGroupItems frmUpdateRoomGroupItems2 = new frmUpdateRoomGroupItems(((TextEditorControlBase)cboHeader).Value.ToString(), dtRooms.Select(" RoomID= " + ((TextEditorControlBase)cboHeader).Value.ToString())[0]["BranchID"].ToString());
			frmUpdateRoomGroupItems2.WindowState = FormWindowState.Normal;
			((Control)(object)frmUpdateRoomGroupItems2.lblTitle).Text = (GlobalVariables.IsArabic ? "تعديل مجموعات الأصناف" : "Change Group Items");
			frmUpdateRoomGroupItems2.Size = new Size(base.Width, base.Height);
			frmUpdateRoomGroupItems2.StartPosition = FormStartPosition.CenterParent;
			frmUpdateRoomGroupItems2.ShowDialog();
			if (frmUpdateRoomGroupItems2.Reload)
			{
				DisplayData();
			}
		}
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["RoomID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["RoomItemID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("POS_RoomsItems", "RoomID", ((TextEditorControlBase)cboHeader).Value.ToString(), "RoomItemID", text, IsFromServer: true);
			RoomsItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن", "Please Select Store");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["StoreID"]).Selected = true;
				return false;
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
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmUpdateRoomItems));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.btnChangeItems = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSave, "btnSave");
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance7.FontData");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance8.FontData");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance9.FontData");
		resources.ApplyResources(val9, "appearance9");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnSaveAndClose, "btnSaveAndClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnChangeItems, "btnChangeItems");
		((System.Windows.Forms.Control)(object)this.btnChangeItems).Name = "btnChangeItems";
		((System.Windows.Forms.Control)(object)this.btnChangeItems).Click += new System.EventHandler(btnChangeItems_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnChangeItems);
		base.Name = "frmUpdateRoomItems";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnChangeItems, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
