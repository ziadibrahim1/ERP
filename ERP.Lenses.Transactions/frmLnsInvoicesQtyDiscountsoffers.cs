using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmLnsInvoicesQtyDiscountsoffers : frmBase
{
	public DataTable dtOffersItems = new DataTable();

	private DataTable dtOffersQtyDiscounts = new DataTable();

	public int OfferID = 0;

	public decimal TotalQty = default(decimal);

	public decimal DiscountRatio = default(decimal);

	public bool ForAll = false;

	public bool LowestPrice = false;

	public bool HighestPrice = false;

	public decimal QtyDiscount = default(decimal);

	public bool Cancel = false;

	private DataTable dtOffers;

	private DataTable dtItems;

	private DataTable dtUnits;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtBatchs;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private ValueList vlItems = new ValueList();

	private ValueList vlUnitsItems = new ValueList();

	private ValueList vlItemsColors = new ValueList();

	private ValueList vlItemsSizes = new ValueList();

	private ValueList vlItemsBatchs = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

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

	public frmLnsInvoicesQtyDiscountsoffers(int OFFERID)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
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
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
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
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlItemsBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlItemsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtOffers = Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "1", "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOffer, dtOffers, "OfferID", "OfferName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnitsItems.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnitsItems.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtOffersItems = InvoicesDetails.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataItems).DataSource = dtOffersItems;
		InitGrid();
		((TextEditorControlBase)cboOffer).Value = OfferID;
		dtOffersQtyDiscounts = OffersQtyDiscounts.SelectByOfferID(OfferID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		if (UsingBatchNoAndValidityPeriod)
		{
			((HeaderBase)((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.2);
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlItemsBatchs;
		}
		else
		{
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataItems).Width * 0.4);
			((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
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

	private void cboOffer_ValueChanged(object sender, EventArgs e)
	{
		if (cboOffer.SelectedIndex > -1)
		{
			DataRow dataRow = dtOffers.Select(" OfferID= " + ((TextEditorControlBase)cboOffer).Value.ToString())[0];
			((DataTable)((UltraGridBase)ULGDataItems).DataSource).Rows.Clear();
			dtItems = OffersItems.FillCombo(((TextEditorControlBase)cboOffer).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			vlItems.ValueListItems.Clear();
			for (int i = 0; i < dtItems.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			}
		}
	}

	private void btnOfferSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.LnsOffersSearch(0, 0, 0, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboOffer).Value = num;
		}
	}

	private ValueList getBatchsValueList(int ItemID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtBatchs.Select("ItemID is null or ItemID=" + ItemID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["BatchID"].ToString(), array[i]["BatchName"].ToString());
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

	private void ULGDataItems_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Expected O, but got Unknown
		ULGDataItems.CellListSelect -= new CellEventHandler(ULGDataItems_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataItems).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = 1;
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (UsingBatchNoAndValidityPeriod)
			{
				if (num != 0)
				{
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num);
				}
				else
				{
					e.Cell.Row.Cells["BatchID"].ValueList = null;
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				}
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
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID")
		{
			int num2 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num2 != 0)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + num2)[0];
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value = num2;
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ColorID"].Value = 1;
					e.Cell.Row.Cells["ColorID"].ValueList = null;
				}
				if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = 1;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
				}
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
		else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
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
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGDataItems).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGDataItems.ActiveCell = ((UltraGridBase)ULGDataItems).Rows[i].Cells["BatchID"];
				ULGDataItems.PerformAction((UltraGridAction)24);
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
		string text2 = arrayList[3].ToString();
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
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
			if (text2 != "")
			{
				string value3 = dtBatchs.Select("(ItemID is null or ItemID=" + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString() + " ) And BatchName='" + text2 + "'")[0]["BatchID"].ToString();
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["BatchID"].Value = value3;
			}
			else
			{
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
			}
		}
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsInvoicesQtyDiscountsoffers));
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOffer).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
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
		((System.Windows.Forms.Control)(object)this.ULGDataItems).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataItems_KeyPress);
		((UltraButtonBase)this.btnSave).AcceptsFocus = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnOfferSearch, "btnOfferSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance13");
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
		((TextEditorControlBase)this.cboOffer).ValueChanged += new System.EventHandler(cboOffer_ValueChanged);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val14).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val14;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOfferSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOfferName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOffer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmLnsInvoicesQtyDiscountsoffers";
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBItems).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsBarCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOffer).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
