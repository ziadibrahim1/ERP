using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
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
using Infragistics.Win.UltraWinTabControl;

namespace ERP.POS.Transactions;

public class frmPOSChecksQtyDiscountsoffers : frmBase
{
	public DataTable dtOffersItems = new DataTable();

	private DataTable dtOffersQtyDiscounts = new DataTable();

	public int OfferID = 0;

	private int newID = -100000;

	public decimal TotalQty = default(decimal);

	public decimal DiscountRatio = default(decimal);

	public bool ForAll = false;

	public bool LowestPrice = false;

	public bool HighestPrice = false;

	public decimal QtyDiscount = default(decimal);

	public bool Cancel = false;

	private DataTable dtItemsAndGroups;

	private DataTable dtRoomItems;

	private DataTable dtOffers;

	private DataTable dtItems;

	private DataTable dtUnits;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private ValueList vlItems = new ValueList();

	private ValueList vlUnitsItems = new ValueList();

	private ValueList vlItemsColors = new ValueList();

	private ValueList vlItemsSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraGroupBox UGBItems;

	public UltraGrid ULGDataItems;

	public UltraButton btnSave;

	public UltraButton btnOfferSearch;

	private UltraLabel lblOfferName;

	private UltraComboEditor cboOffer;

	public UltraButton btnClose;

	private UltraTextEditor txtItemsBarCode;

	private UltraLabel lblItemsBarCode;

	private UltraGroupBox UGBItemData;

	private UltraPanel pnlItems;

	private UltraTabControl UTCItemsGroups;

	private UltraTabSharedControlsPage ultraTabItems;

	public frmPOSChecksQtyDiscountsoffers(int OFFERID, DataTable DTRoomItems)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		InitializeComponent();
		OfferID = OFFERID;
		dtRoomItems = DTRoomItems;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlItemsColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlItemsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlItemsSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlItemsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtOffers = Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "1", "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOffer, dtOffers, "OfferID", "OfferName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnitsItems.ValueListItems.Clear();
		for (int k = 0; k < dtUnits.Rows.Count; k++)
		{
			vlUnitsItems.ValueListItems.Add(dtUnits.Rows[k]["UnitID"], dtUnits.Rows[k]["UnitName"].ToString());
		}
		dtOffersQtyDiscounts = OffersQtyDiscounts.SelectByOfferID(OfferID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtOffersItems = ChecksDetails.SelectByCheckID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataItems).DataSource = dtOffersItems;
		InitGrid();
		((TextEditorControlBase)cboOffer).Value = OfferID;
		DataRow dataRow = dtOffers.Select(" OfferID= " + ((TextEditorControlBase)cboOffer).Value.ToString())[0];
		((DataTable)((UltraGridBase)ULGDataItems).DataSource).Rows.Clear();
		dtItems = OffersItems.FillCombo(((TextEditorControlBase)cboOffer).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		vlItems.ValueListItems.Clear();
		for (int l = 0; l < dtItems.Rows.Count; l++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
		}
		dtItemsAndGroups = OffersItems.SelectItemsAndGroupsByOfferID(OfferID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		FillItemsGroups();
		((EditorButtonControlBase)cboOffer).ReadOnly = true;
		((Control)(object)btnOfferSearch).Visible = false;
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataItems);
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingColors)
		{
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.15);
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlItemsColors;
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.15);
		}
		else
		{
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.3);
		}
		if (UsingSizes)
		{
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.15);
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlItemsSizes;
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.15);
		}
		else
		{
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.3);
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitsItems;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	private void btnOfferSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.LnsOffersSearch(0, 0, 0, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboOffer).Value = num;
		}
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

	private void ULGDataItems_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		ULGDataItems.CellListSelect -= new CellEventHandler(ULGDataItems_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataItems).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = 1;
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
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
		ULGDataItems.CellListSelect += new CellEventHandler(ULGDataItems_CellListSelect);
	}

	private void ULGDataItems_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "Qty")
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
	}

	private void ULGDataItems_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataItems.ActiveCell != null && ((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGDataItems.ActiveCell, e);
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		((UltraGridBase)ULGDataItems).UpdateData();
		if (cboOffer.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار العرض", "Please Select offer");
			((TextEditorControlBase)cboOffer).Focus();
			cboOffer.DropDown();
			return;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الاصناف", "Please Select items");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			if (UsingColors && (((UltraGridBase)ULGDataItems).Rows[i].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[i].Cells["ColorID"];
				((UltraGridBase)ULGDataItems).Rows[i].Cells["ColorID"].DroppedDown = true;
				return;
			}
			if (((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"];
				return;
			}
			if (UsingSizes && (((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemSizeID"];
				((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
				return;
			}
			if (dtRoomItems.Select("IsOffer = 0 and  ItemID =" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value.ToString()).Length == 0)
			{
				GlobalVariables.InformationMB.Show("بعض الأصناف المختارة غير موجوده بهذه الصالة ", "Some of these chosen items not exists in this room ");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"];
				((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].DroppedDown = true;
				return;
			}
		}
		DataTable dataTable = (DataTable)((UltraGridBase)ULGDataItems).DataSource;
		object obj = dataTable.Compute(" Sum(Qty) ", "");
		if (obj != DBNull.Value && decimal.Parse(obj.ToString()) > 0m)
		{
			DataRow[] array = dtOffersQtyDiscounts.Select(" Qty= " + obj.ToString());
			if (array.Length == 0)
			{
				GlobalVariables.InformationMB.Show("  الكمية المباعة غير موجودة فى اى خصم كمية", " Sales Quantity Not Exists In  Qty Discount");
				return;
			}
			DiscountRatio = decimal.Parse(array[0]["DiscountRatio"].ToString());
			ForAll = bool.Parse(array[0]["ForAll"].ToString());
			LowestPrice = bool.Parse(array[0]["LowestPrice"].ToString());
			HighestPrice = bool.Parse(array[0]["HighestPrice"].ToString());
			QtyDiscount = ((array[0]["QtyDiscount"] == DBNull.Value) ? 0m : decimal.Parse(array[0]["QtyDiscount"].ToString()));
		}
		dtOffersItems.AcceptChanges();
		OfferID = int.Parse(((TextEditorControlBase)cboOffer).Value.ToString());
		Close();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Cancel = true;
		Close();
	}

	private void txtItemsBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtItemsBarCode).Text != "")
		{
			AddItemInGid();
		}
	}

	public void AddItemInGid()
	{
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtItemsBarCode).Text);
		string text = arrayList[0].ToString();
		string value = arrayList[1].ToString();
		string value2 = arrayList[2].ToString();
		string s = "1";
		if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtItemsBarCode).Clear();
			((TextEditorControlBase)txtItemsBarCode).Focus();
			return;
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
		DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].Value = value;
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = value2;
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].Value = value;
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = value2;
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtItemsBarCode).Clear();
		((TextEditorControlBase)txtItemsBarCode).Focus();
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
				((Control)(object)val2).Click += btnItemGroub_Click;
				((Control)(object)val2).Tag = dataView[j]["ItemID"].ToString();
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

	public void btnItemGroub_Click(object sender, EventArgs e)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		((Control)(object)pnlItems).Visible = false;
		((Control)(object)pnlItems.ClientArea).Controls.Clear();
		DataView dataView = new DataView(dtItemsAndGroups);
		dataView.RowFilter = "IsMain=0 And ParentID=" + ((Control)(UltraButton)sender).Tag;
		dataView.ToTable();
		int num = 8;
		int num2 = 6;
		for (int i = 0; i < dataView.Count; i++)
		{
			UltraButton val = new UltraButton();
			((Control)(object)val).Click += btnItems_Click;
			((Control)(object)val).Tag = dataView[i]["ItemID"].ToString();
			((Control)(object)val).Text = dataView[i]["ItemName"].ToString();
			((Control)(object)val).Height += 25;
			((Control)(object)pnlItems.ClientArea).Controls.Add((Control)(object)val);
			((UltraControlBase)val).Update();
			if (num + ((Control)(object)val).Width > ((Control)(object)pnlItems).Width)
			{
				num2 += ((Control)(object)val).Height;
				num = 8;
			}
			((Control)(object)val).Left = num;
			((Control)(object)val).Top = num2;
			((Control)(object)val).Width = (((Control)(object)pnlItems).Width - GlobalVariables.ScrollWidth) / 6;
			num += ((Control)(object)val).Width;
		}
		((Control)(object)pnlItems).Visible = true;
	}

	public void btnItems_Click(object sender, EventArgs e)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			DataRow dataRow = dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value)[0];
			if (int.Parse(((Control)(UltraButton)sender).Tag.ToString()) == int.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value.ToString()))
			{
				if (bool.Parse(dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value)[0]["IsWeight"].ToString()) || bool.Parse(dtItemsAndGroups.Select("ItemID =" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value)[0]["UsePOSNumPad"].ToString()))
				{
					frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"], ((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value.ToString());
					frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
					frmDecimal2.ShowDialog();
				}
				else
				{
					((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value.ToString()) + 1m;
				}
				return;
			}
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["CheckDetailID"].Value = ++newID;
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value = ((Control)(UltraButton)sender).Tag;
		DataRow dataRow2 = dtItemsAndGroups.Select("ItemID=" + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["TaxID"].Value = dataRow2["TaxID"];
		if (bool.Parse(dataRow2["IsWeight"].ToString()) || bool.Parse(dataRow2["UsePOSNumPad"].ToString()))
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = 0;
			frmDecimal frmDecimal3 = new frmDecimal(((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"], ((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value.ToString());
			frmDecimal3.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal3.ShowDialog();
		}
		else
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = 1;
		}
		if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
		}
		if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
		}
		int index = ((UltraGridBase)ULGDataItems).ActiveRow.Index;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGDataItems).Rows[index].Activate();
	}

	private void ULGData_ClickCell(object sender, ClickCellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "Qty")
		{
			frmDecimal frmDecimal2 = new frmDecimal(e.Cell, e.Cell.Value.ToString());
			frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
			frmDecimal2.ShowDialog();
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Expected O, but got Unknown
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmPOSChecksQtyDiscountsoffers));
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
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		this.pnlItems = new UltraPanel();
		this.lblTitle = new UltraLabel();
		this.UGBItems = new UltraGroupBox();
		this.txtItemsBarCode = new UltraTextEditor();
		this.lblItemsBarCode = new UltraLabel();
		this.ULGDataItems = new UltraGrid();
		this.btnSave = new UltraButton();
		this.btnOfferSearch = new UltraButton();
		this.lblOfferName = new UltraLabel();
		this.cboOffer = new UltraComboEditor();
		this.btnClose = new UltraButton();
		this.UGBItemData = new UltraGroupBox();
		this.UTCItemsGroups = new UltraTabControl();
		this.ultraTabItems = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOffer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlItems, "pnlItems");
		this.pnlItems.AutoScroll = true;
		resources.ApplyResources(this.pnlItems.ClientArea, "pnlItems.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlItems).Name = "pnlItems";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance16");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.UGBItems, "UGBItems");
		this.UGBItems.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.txtItemsBarCode);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.lblItemsBarCode);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataItems);
		((System.Windows.Forms.Control)(object)this.UGBItems).Name = "UGBItems";
		resources.ApplyResources(this.txtItemsBarCode, "txtItemsBarCode");
		((System.Windows.Forms.Control)(object)this.txtItemsBarCode).Name = "txtItemsBarCode";
		((System.Windows.Forms.Control)(object)this.txtItemsBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtItemsBarCode_KeyUp);
		resources.ApplyResources(this.lblItemsBarCode, "lblItemsBarCode");
		this.lblItemsBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemsBarCode).Name = "lblItemsBarCode";
		((ControlBase)this.lblItemsBarCode).WrapText = false;
		resources.ApplyResources(this.ULGDataItems, "ULGDataItems");
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val2).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val2, "appearance2");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val3;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance8");
		((AppearanceBase)val8).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGDataItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ULGDataItems).Name = "ULGDataItems";
		this.ULGDataItems.AfterEnterEditMode += new System.EventHandler(ULGDataItems_AfterEnterEditMode);
		this.ULGDataItems.CellListSelect += new CellEventHandler(ULGDataItems_CellListSelect);
		this.ULGDataItems.ClickCell += new ClickCellEventHandler(ULGData_ClickCell);
		((System.Windows.Forms.Control)(object)this.ULGDataItems).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataItems_KeyPress);
		((UltraButtonBase)this.btnSave).AcceptsFocus = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val12).Image = resources.GetObject("appearance17.Image");
		resources.ApplyResources(val12, "appearance17");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnOfferSearch, "btnOfferSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance18");
		((ControlBase)this.btnOfferSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnOfferSearch).Name = "btnOfferSearch";
		((System.Windows.Forms.Control)(object)this.btnOfferSearch).Click += new System.EventHandler(btnOfferSearch_Click);
		resources.ApplyResources(this.lblOfferName, "lblOfferName");
		this.lblOfferName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOfferName).Name = "lblOfferName";
		((ControlBase)this.lblOfferName).WrapText = false;
		resources.ApplyResources(this.cboOffer, "cboOffer");
		((TextEditorControlBase)this.cboOffer).AlwaysInEditMode = true;
		this.cboOffer.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOffer).Name = "cboOffer";
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val14).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val14, "appearance19");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val14;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.UGBItemData, "UGBItemData");
		this.UGBItemData.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItemData).Controls.Add((System.Windows.Forms.Control)(object)this.pnlItems);
		((System.Windows.Forms.Control)(object)this.UGBItemData).Name = "UGBItemData";
		resources.ApplyResources(this.UTCItemsGroups, "UTCItemsGroups");
		resources.ApplyResources(val15, "appearance15");
		((AppearanceBase)val15).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCItemsGroups).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabItems);
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).Name = "UTCItemsGroups";
		((UltraTabControlBase)this.UTCItemsGroups).SharedControlsPage = this.ultraTabItems;
		resources.ApplyResources(this.ultraTabItems, "ultraTabItems");
		((System.Windows.Forms.Control)(object)this.ultraTabItems).Name = "ultraTabItems";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItemData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCItemsGroups);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOfferSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOfferName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOffer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmPOSChecksQtyDiscountsoffers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOffer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOfferName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOfferSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCItemsGroups, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItemData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBItems).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBItems).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOffer).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UTCItemsGroups).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCItemsGroups).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
