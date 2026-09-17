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

public class frmLnsInvoicesGiftsoffers : frmBase
{
	public DataTable dtOffersGifts = new DataTable();

	public DataTable dtOffersItems = new DataTable();

	public int OfferID = 0;

	public decimal DiscountRatio = default(decimal);

	public bool Cancel = false;

	private DataTable dtOffers;

	private DataTable dtItems;

	private DataTable dtGifts;

	private DataTable dtUnits;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtBatchs;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private ValueList vlItems = new ValueList();

	private ValueList vlGifts = new ValueList();

	private ValueList vlUnitsItems = new ValueList();

	private ValueList vlUnitsGifts = new ValueList();

	private ValueList vlItemsColors = new ValueList();

	private ValueList vlItemsSizes = new ValueList();

	private ValueList vlItemsBatchs = new ValueList();

	private ValueList vlGiftsColors = new ValueList();

	private ValueList vlGiftsSizes = new ValueList();

	private ValueList vlGiftsBatchs = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraGroupBox UGBItems;

	public UltraGrid ULGDataItems;

	public UltraGroupBox UGBGifts;

	public UltraGrid ULGDataGifts;

	public UltraButton btnSave;

	public UltraButton btnOfferSearch;

	private UltraLabel lblOfferName;

	private UltraComboEditor cboOffer;

	private UltraLabel lblGiftsDiscountRatio;

	public UltraTextEditor txtGiftsDiscountRatio;

	private UltraLabel lblGiftsQty;

	public UltraTextEditor txtGiftsQty;

	private UltraLabel lblItemsQty;

	public UltraTextEditor txtItemsQty;

	public UltraButton btnClose;

	private UltraTextEditor txtItemsBarCode;

	private UltraLabel lblItemsBarCode;

	private UltraTextEditor txtGiftsBarcode;

	private UltraLabel lblGiftsBarcode;

	public frmLnsInvoicesGiftsoffers(int OFFERID)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
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
			vlGiftsColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlItemsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
				vlGiftsColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlItemsSizes.ValueListItems.Clear();
			vlGiftsSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlItemsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
				vlGiftsSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlItemsBatchs.ValueListItems.Clear();
			vlGiftsBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlItemsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
				vlGiftsBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtOffers = Offers.FillCombo(DateTime.Now.ToString(GlobalVariables.DateLongFormate), "," + GlobalVariables.CurrentBranchID + ",", "0", "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboOffer, dtOffers, "OfferID", "OfferName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnitsItems.ValueListItems.Clear();
		vlUnitsGifts.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnitsItems.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
			vlUnitsGifts.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtOffersGifts = InvoicesDetails.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtOffersItems = InvoicesDetails.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataItems).DataSource = dtOffersItems;
		((UltraGridBase)ULGDataGifts).DataSource = dtOffersGifts;
		InitGrid();
		((TextEditorControlBase)cboOffer).Value = OfferID;
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
		GlobalFunctions.PrepareGrid(ULGDataGifts);
		((UltraGridBase)ULGDataGifts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((HeaderBase)((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingColors)
		{
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.15);
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlGiftsColors;
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.15);
		}
		else
		{
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.3);
		}
		if (UsingSizes)
		{
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.15);
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlGiftsSizes;
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.15);
		}
		else
		{
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.3);
		}
		if (UsingBatchNoAndValidityPeriod)
		{
			((HeaderBase)((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.2);
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlGiftsBatchs;
		}
		else
		{
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataGifts).Width * 0.4);
			((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlGifts;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitsGifts;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	private void cboOffer_ValueChanged(object sender, EventArgs e)
	{
		if (cboOffer.SelectedIndex > -1)
		{
			DataRow dataRow = dtOffers.Select(" OfferID= " + ((TextEditorControlBase)cboOffer).Value.ToString())[0];
			((Control)(object)txtItemsQty).Text = decimal.Parse(dataRow["ItemsQty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGiftsQty).Text = decimal.Parse(dataRow["GiftsQty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtGiftsDiscountRatio).Text = decimal.Parse(dataRow["GiftsDiscountRatio"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((DataTable)((UltraGridBase)ULGDataItems).DataSource).Rows.Clear();
			((DataTable)((UltraGridBase)ULGDataGifts).DataSource).Rows.Clear();
			dtItems = OffersItems.FillCombo(((TextEditorControlBase)cboOffer).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			vlItems.ValueListItems.Clear();
			for (int i = 0; i < dtItems.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			}
			dtGifts = OffersGifts.FillCombo(((TextEditorControlBase)cboOffer).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			vlGifts.ValueListItems.Clear();
			for (int j = 0; j < dtGifts.Rows.Count; j++)
			{
				vlGifts.ValueListItems.Add(dtGifts.Rows[j]["ItemID"], dtGifts.Rows[j]["Name"].ToString());
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
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Expected O, but got Unknown
		ULGDataItems.CellListSelect -= new CellEventHandler(ULGDataItems_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataItems).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
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
		if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "UnitID")
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGDataItems.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGDataItems).ActiveRow).Selected = true;
		}
	}

	private void ULGDataGifts_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Expected O, but got Unknown
		ULGDataGifts.CellListSelect -= new CellEventHandler(ULGDataGifts_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGDataGifts).UpdateData();
			DataRow dataRow = dtGifts.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGDataGifts).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtGifts.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
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
				DataRow dataRow2 = dtGifts.Select(" ItemID= " + num2)[0];
				((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value = num2;
				((UltraGridBase)ULGDataGifts).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
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
		ULGDataGifts.CellListSelect += new CellEventHandler(ULGDataGifts_CellListSelect);
	}

	private void ULGDataGifts_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGDataGifts.ActiveCell.Column).Key == "UnitID")
		{
			((GridItemBase)((UltraGridBase)ULGDataGifts).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGDataGifts.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtGifts.Select(" ItemID= " + ((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGDataGifts).ActiveRow).Selected = true;
		}
	}

	private void ULGDataGifts_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataGifts.ActiveCell != null && ((KeyedSubObjectBase)ULGDataGifts.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGDataGifts.ActiveCell, e);
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
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataGifts).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الهدايا", "Please Select Gifts");
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
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataGifts).Rows).Count; j++)
		{
			if (UsingColors && (((UltraGridBase)ULGDataGifts).Rows[j].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGDataGifts).Rows[j].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGDataGifts.ActiveCell = ((UltraGridBase)ULGDataGifts).Rows[j].Cells["ColorID"];
				((UltraGridBase)ULGDataGifts).Rows[j].Cells["ColorID"].DroppedDown = true;
				return;
			}
			if (UsingSizes && (((UltraGridBase)ULGDataGifts).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGDataGifts).Rows[j].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				ULGDataGifts.ActiveCell = ((UltraGridBase)ULGDataGifts).Rows[j].Cells["ItemSizeID"];
				((UltraGridBase)ULGDataGifts).Rows[j].Cells["ItemSizeID"].DroppedDown = true;
				return;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtGifts.Select("ItemID =" + ((UltraGridBase)ULGDataGifts).Rows[j].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGDataGifts).Rows[j].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				ULGDataGifts.ActiveCell = ((UltraGridBase)ULGDataGifts).Rows[j].Cells["BatchID"];
				ULGDataGifts.PerformAction((UltraGridAction)24);
				return;
			}
		}
		((UltraGridBase)ULGDataItems).UpdateData();
		((UltraGridBase)ULGDataGifts).UpdateData();
		DataTable dataTable = (DataTable)((UltraGridBase)ULGDataItems).DataSource;
		object obj = dataTable.Compute(" Sum(Qty) ", "");
		if (obj != DBNull.Value && decimal.Parse(((Control)(object)txtItemsQty).Text) != (decimal)obj)
		{
			GlobalVariables.InformationMB.Show("الكمية المختارة من الاصناف غير مساوية لاصناف العرض", "Selected Quantity Not Equal Offer Quantity");
			return;
		}
		dataTable = (DataTable)((UltraGridBase)ULGDataGifts).DataSource;
		object obj2 = dataTable.Compute(" Sum(Qty) ", "");
		if (obj2 != DBNull.Value && decimal.Parse(((Control)(object)txtGiftsQty).Text) != (decimal)obj2)
		{
			GlobalVariables.InformationMB.Show("الكمية المختارة من الهدايا غير مساوية لهدايا العرض", "Selected Quantity Not Equal Offer Quantity");
			return;
		}
		dtOffersItems.AcceptChanges();
		dtOffersGifts.AcceptChanges();
		OfferID = int.Parse(((TextEditorControlBase)cboOffer).Value.ToString());
		DiscountRatio = decimal.Parse(((Control)(object)txtGiftsDiscountRatio).Text);
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

	private void txtGiftsBarcode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtGiftsBarcode).Text != "")
		{
			AddGiftsInGid();
		}
	}

	public void AddItemInGid()
	{
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtItemsBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string text4 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		if (dtItems.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtItemsBarCode).Clear();
			((TextEditorControlBase)txtItemsBarCode).Focus();
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataItems).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGDataItems).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGDataItems).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGDataItems).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3)
			{
				((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGDataItems).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((TextEditorControlBase)txtItemsBarCode).Clear();
				((TextEditorControlBase)txtItemsBarCode).Focus();
				return;
			}
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGDataItems).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value = dtItems.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
		DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGDataItems).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
			if (text4 != "")
			{
				string value = dtBatchs.Select("(ItemID is null or ItemID=" + ((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemID"].Value.ToString() + " ) And BatchName='" + text4 + "'")[0]["BatchID"].ToString();
				((UltraGridBase)ULGDataItems).ActiveRow.Cells["BatchID"].Value = value;
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
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ColorID"].Value = text2;
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			((UltraGridBase)ULGDataItems).ActiveRow.Cells["ItemSizeID"].Value = text3;
		}
		((UltraGridBase)ULGDataItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtItemsBarCode).Clear();
		((TextEditorControlBase)txtItemsBarCode).Focus();
	}

	public void AddGiftsInGid()
	{
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtGiftsBarcode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string text4 = arrayList[3].ToString();
		string s = arrayList[4].ToString();
		if (dtGifts.Select(" ItemBarCode = '" + text + "'").Length == 0)
		{
			((TextEditorControlBase)txtGiftsBarcode).Clear();
			((TextEditorControlBase)txtGiftsBarcode).Focus();
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataGifts).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataGifts).Rows[i].Cells["ItemBarCode"].Text == text && ((UltraGridBase)ULGDataGifts).Rows[i].Cells["BatchID"].Text == text4 && ((UltraGridBase)ULGDataGifts).Rows[i].Cells["ColorID"].Value.ToString() == text2 && ((UltraGridBase)ULGDataGifts).Rows[i].Cells["ItemSizeID"].Value.ToString() == text3)
			{
				((UltraGridBase)ULGDataGifts).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGDataGifts).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				return;
			}
		}
		((UltraGridBase)ULGDataGifts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGDataGifts).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value = dtGifts.Select(" ItemBarCode = '" + text + "'")[0]["ItemID"].ToString();
		DataRow dataRow = dtGifts.Select("ItemID=" + ((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGDataGifts).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
		((UltraGridBase)ULGDataGifts).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGDataGifts).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
			if (text4 != "")
			{
				string value = dtBatchs.Select("(ItemID is null or ItemID=" + ((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemID"].Value.ToString() + " ) And BatchName='" + text4 + "'")[0]["BatchID"].ToString();
				((UltraGridBase)ULGDataGifts).ActiveRow.Cells["BatchID"].Value = value;
			}
			else
			{
				((UltraGridBase)ULGDataGifts).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
			}
		}
		if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
		}
		if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGDataGifts).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
		}
		((UltraGridBase)ULGDataGifts).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtGiftsBarcode).Clear();
		((TextEditorControlBase)txtGiftsBarcode).Focus();
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
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0842: Unknown result type (might be due to invalid IL or missing references)
		//IL_084c: Expected O, but got Unknown
		//IL_0e4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e56: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsInvoicesGiftsoffers));
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
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.UGBItems = new UltraGroupBox();
		this.txtItemsBarCode = new UltraTextEditor();
		this.lblItemsBarCode = new UltraLabel();
		this.lblItemsQty = new UltraLabel();
		this.txtItemsQty = new UltraTextEditor();
		this.ULGDataItems = new UltraGrid();
		this.UGBGifts = new UltraGroupBox();
		this.txtGiftsBarcode = new UltraTextEditor();
		this.lblGiftsBarcode = new UltraLabel();
		this.lblGiftsDiscountRatio = new UltraLabel();
		this.txtGiftsDiscountRatio = new UltraTextEditor();
		this.lblGiftsQty = new UltraLabel();
		this.txtGiftsQty = new UltraTextEditor();
		this.ULGDataGifts = new UltraGrid();
		this.btnSave = new UltraButton();
		this.btnOfferSearch = new UltraButton();
		this.lblOfferName = new UltraLabel();
		this.cboOffer = new UltraComboEditor();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsBarCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemsQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGifts).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBGifts).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsBarcode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataGifts).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.lblItemsQty);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.txtItemsQty);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataItems);
		((System.Windows.Forms.Control)(object)this.UGBItems).Name = "UGBItems";
		resources.ApplyResources(this.txtItemsBarCode, "txtItemsBarCode");
		((System.Windows.Forms.Control)(object)this.txtItemsBarCode).Name = "txtItemsBarCode";
		((System.Windows.Forms.Control)(object)this.txtItemsBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtItemsBarCode_KeyUp);
		resources.ApplyResources(this.lblItemsBarCode, "lblItemsBarCode");
		this.lblItemsBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemsBarCode).Name = "lblItemsBarCode";
		((ControlBase)this.lblItemsBarCode).WrapText = false;
		resources.ApplyResources(this.lblItemsQty, "lblItemsQty");
		this.lblItemsQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemsQty).Name = "lblItemsQty";
		((ControlBase)this.lblItemsQty).WrapText = false;
		resources.ApplyResources(this.txtItemsQty, "txtItemsQty");
		((System.Windows.Forms.Control)(object)this.txtItemsQty).Name = "txtItemsQty";
		((EditorButtonControlBase)this.txtItemsQty).ReadOnly = true;
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
		resources.ApplyResources(this.UGBGifts, "UGBGifts");
		this.UGBGifts.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.txtGiftsBarcode);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.lblGiftsBarcode);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.lblGiftsDiscountRatio);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.txtGiftsDiscountRatio);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.lblGiftsQty);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.txtGiftsQty);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataGifts);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Name = "UGBGifts";
		resources.ApplyResources(this.txtGiftsBarcode, "txtGiftsBarcode");
		((System.Windows.Forms.Control)(object)this.txtGiftsBarcode).Name = "txtGiftsBarcode";
		((System.Windows.Forms.Control)(object)this.txtGiftsBarcode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtGiftsBarcode_KeyUp);
		resources.ApplyResources(this.lblGiftsBarcode, "lblGiftsBarcode");
		this.lblGiftsBarcode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGiftsBarcode).Name = "lblGiftsBarcode";
		((ControlBase)this.lblGiftsBarcode).WrapText = false;
		resources.ApplyResources(this.lblGiftsDiscountRatio, "lblGiftsDiscountRatio");
		this.lblGiftsDiscountRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGiftsDiscountRatio).Name = "lblGiftsDiscountRatio";
		((ControlBase)this.lblGiftsDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtGiftsDiscountRatio, "txtGiftsDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtGiftsDiscountRatio).Name = "txtGiftsDiscountRatio";
		((EditorButtonControlBase)this.txtGiftsDiscountRatio).ReadOnly = true;
		resources.ApplyResources(this.lblGiftsQty, "lblGiftsQty");
		this.lblGiftsQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGiftsQty).Name = "lblGiftsQty";
		((ControlBase)this.lblGiftsQty).WrapText = false;
		resources.ApplyResources(this.txtGiftsQty, "txtGiftsQty");
		((System.Windows.Forms.Control)(object)this.txtGiftsQty).Name = "txtGiftsQty";
		((EditorButtonControlBase)this.txtGiftsQty).ReadOnly = true;
		resources.ApplyResources(this.ULGDataGifts, "ULGDataGifts");
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataGifts).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val13;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataGifts).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val17;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val18, "appearance18");
		((AppearanceBase)val18).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val19).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val19).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val19).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGDataGifts).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.ULGDataGifts).Name = "ULGDataGifts";
		this.ULGDataGifts.AfterEnterEditMode += new System.EventHandler(ULGDataGifts_AfterEnterEditMode);
		this.ULGDataGifts.CellListSelect += new CellEventHandler(ULGDataGifts_CellListSelect);
		((System.Windows.Forms.Control)(object)this.ULGDataGifts).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataGifts_KeyPress);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val22).Image = resources.GetObject("appearance22.Image");
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val22;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnOfferSearch, "btnOfferSearch");
		((AppearanceBase)val23).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.btnOfferSearch).Appearance = (AppearanceBase)(object)val23;
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
		((AppearanceBase)val24).Image = resources.GetObject("appearance24.Image");
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val24;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOfferSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOfferName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOffer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBGifts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmLnsInvoicesGiftsoffers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBGifts, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtItemsQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGifts).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBGifts).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBGifts).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsBarcode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataGifts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOffer).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
