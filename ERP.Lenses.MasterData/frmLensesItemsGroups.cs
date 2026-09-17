using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.MasterData;

public class frmLensesItemsGroups : frmBase
{
	private DataTable dtUnitGroup;

	private DataTable dtUnits;

	private DataTable dtStores;

	private DataTable dtRoots;

	private DataTable dtGroups;

	private DataTable dtItemPrices;

	private DataTable dtItemStockLevels;

	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlBranches2 = new ValueList();

	private IContainer components = null;

	public UltraTextEditor txtCylFrom;

	public UltraTextEditor txtSpFrom;

	public UltraTextEditor txtPeriod;

	public UltraLabel lblRoot;

	public UltraComboEditor cboRoot;

	public UltraLabel lblSyl;

	public UltraLabel lblSP;

	public UltraLabel lblPeriod;

	public UltraComboEditor cboGroup;

	public UltraLabel lblGroup;

	public UltraLabel lblUnitGroup;

	public UltraComboEditor cboUnitGroup;

	public UltraLabel lblUnit;

	public UltraComboEditor cboUnit;

	public UltraComboEditor cboStore;

	public UltraLabel lblStore;

	public UltraTextEditor txtCylTo;

	public UltraTextEditor txtSpTo;

	public UltraLabel ultraLabel1;

	public UltraLabel ultraLabel2;

	public UltraLabel ultraLabel3;

	public UltraLabel ultraLabel4;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	public UltraGrid ULGPrices;

	private UltraPanel ultraPanel1;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	public UltraGrid ULGLevels;

	private UltraPanel ultraPanel2;

	private RadioButton rbStockLevelForEachBranch;

	private RadioButton rbStockLevelsForAllBranches;

	public UltraLabel ultraLabel5;

	public UltraLabel ultraLabel6;

	public UltraTextEditor txtInSPCYL;

	public UltraLabel ultraLabel7;

	public frmLensesItemsGroups()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtRoots = Items.FillGroups("-1", ",1,", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboRoot, dtRoots, "ItemID", "Name");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		vlBranches2.ValueListItems.Clear();
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches2.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPricesTypes.Rows.Count; j++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[j]["PriceTypeID"], dtPricesTypes.Rows[j]["PriceName"].ToString());
		}
		dtItemPrices = ItemsPrices.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
		dtItemStockLevels = ItemsStockLevels.SelectByItemID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridStockLevels();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	private void cboUnitGroup_ValueChanged(object sender, EventArgs e)
	{
		if (cboUnitGroup.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtUnits);
			dataView.RowFilter = "UnitTypeID=" + ((TextEditorControlBase)cboUnitGroup).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboUnit.DataSource = dataView;
			cboUnit.DisplayMember = "UnitName";
			cboUnit.ValueMember = "UnitID";
		}
	}

	public bool ValidateData()
	{
		if (cboUnitGroup.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مجموعه الوحده", "Please select Unit Group");
			((TextEditorControlBase)cboUnitGroup).Focus();
			return false;
		}
		if (cboUnit.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الوحده", "Please select Unit ");
			((TextEditorControlBase)cboUnit).Focus();
			return false;
		}
		if (cboStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الإفتراضي", "Please select Default Store");
			((TextEditorControlBase)cboStore).Focus();
			return false;
		}
		if (cboRoot.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المجموعه الإفتراضيه", "Please select Root");
			((TextEditorControlBase)cboRoot).Focus();
			return false;
		}
		if (((Control)(object)cboGroup).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم المجموعه", "Please Enter Group Name");
			((TextEditorControlBase)cboGroup).Focus();
			return false;
		}
		if (((Control)(object)txtPeriod).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال معدل التغير", "Please Enter Period");
			((TextEditorControlBase)txtPeriod).Focus();
			return false;
		}
		if (((Control)(object)txtSpFrom).Text == "" || ((Control)(object)txtSpTo).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال قيم السفير", "Please Enter Sp Values");
			((TextEditorControlBase)txtSpFrom).Focus();
			return false;
		}
		if (((Control)(object)txtCylFrom).Text == "" || ((Control)(object)txtCylTo).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال قيم السيلندر", "Please Enter Cyl Values");
			((TextEditorControlBase)txtCylFrom).Focus();
			return false;
		}
		if (Convert.ToDecimal(((Control)(object)txtSpFrom).Text) > Convert.ToDecimal(((Control)(object)txtSpTo).Text))
		{
			GlobalVariables.InformationMB.Show("يجب ان تكون القيمه من اصغر من أو تساوي القيمه إلي", "Value from Should Be Smaller Than Value To");
			((TextEditorControlBase)txtSpFrom).Focus();
			return false;
		}
		if (Convert.ToDecimal(((Control)(object)txtCylFrom).Text) > Convert.ToDecimal(((Control)(object)txtCylTo).Text))
		{
			GlobalVariables.InformationMB.Show("يجب ان تكون القيمه من اصغر من أو تساوي القيمه إلي", "Value from Should Be Smaller Than Value To");
			((TextEditorControlBase)txtCylFrom).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count; i++)
		{
			if (Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MaxLevel"].Value) < Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MinLevel"].Value))
			{
				GlobalVariables.InformationMB.Show("الحد الأقصي يجب ان يكون اكبر من الحد الأدني", "MaxLevel Must Be Greater Than MinLevel");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["MaxLevel"];
				return false;
			}
		}
		return true;
	}

	public void Save()
	{
		DataTable dataTable = Items.LensesGenerateItemsRange(((Control)(object)cboGroup).Text, ((TextEditorControlBase)cboRoot).Value.ToString(), ((TextEditorControlBase)cboUnit).Value.ToString(), ((TextEditorControlBase)cboUnitGroup).Value.ToString(), rbStockLevelsForAllBranches.Checked ? "1" : "0", ((TextEditorControlBase)cboStore).Value.ToString(), ((Control)(object)txtPeriod).Text, ((Control)(object)txtSpFrom).Text, ((Control)(object)txtSpTo).Text, ((Control)(object)txtInSPCYL).Text, ((Control)(object)txtCylFrom).Text, ((Control)(object)txtCylTo).Text, rbPriceForAllBranches.Checked ? "1" : "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
		string text = "";
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			for (int j = 0; j < dtItemPrices.Rows.Count; j++)
			{
				text += " Exec SC_ItemsPrices_Insert_Update ";
				text += "-1,";
				text = text + dataTable.Rows[i]["ItemID"].ToString() + ",";
				text = text + dtItemPrices.Rows[j]["PriceTypeID"].ToString() + ",";
				text = text + dtItemPrices.Rows[j]["Price"].ToString() + ",";
				text += "0,";
				text += (rbPriceForAllBranches.Checked ? "Null," : (dtItemPrices.Rows[j]["BranchID"].ToString() + ","));
				text = text + GlobalVariables.UserID + "; ";
			}
		}
		for (int k = 0; k < dataTable.Rows.Count; k++)
		{
			for (int l = 0; l < dtItemStockLevels.Rows.Count; l++)
			{
				text += " Exec SC_ItemsStockLevels_Insert_Update ";
				text += "-1,";
				text = text + dataTable.Rows[k]["ItemID"].ToString() + ",";
				text = text + dtItemStockLevels.Rows[l]["MinLevel"].ToString() + ",";
				text = text + dtItemStockLevels.Rows[l]["ReOrderLevel"].ToString() + ",";
				text = text + dtItemStockLevels.Rows[l]["MaxLevel"].ToString() + ",";
				text += "0,";
				text += (rbStockLevelsForAllBranches.Checked ? "Null," : (dtItemStockLevels.Rows[l]["BranchID"].ToString() + ","));
				text = text + GlobalVariables.UserID + "; ";
			}
		}
		if (text != "")
		{
			Main.SyncExecuteNonQuery(text);
		}
		GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Items Saved");
		cboRoot_ValueChanged(null, null);
		((TextEditorControlBase)cboGroup).Clear();
	}

	private void cboRoot_ValueChanged(object sender, EventArgs e)
	{
		if (cboRoot.SelectedIndex != -1)
		{
			dtGroups = Items.FillGroups(((TextEditorControlBase)cboRoot).Value.ToString(), ",2,", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboGroup, dtGroups, "ItemID", "Name");
		}
	}

	private void rbPriceForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void rbStockLevelsForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridStockLevels();
	}

	private void FillGridPrices()
	{
		dtItemPrices.Rows.Clear();
		if (rbPriceForAllBranches.Checked)
		{
			for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
			{
				DataRow dataRow = dtItemPrices.NewRow();
				dataRow["ItemPriceID"] = -1;
				dataRow["ItemID"] = -1;
				dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
				dataRow["Price"] = 0;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = DBNull.Value;
				dtItemPrices.Rows.Add(dataRow);
			}
		}
		else
		{
			for (int j = 0; j < dtBranches.Rows.Count; j++)
			{
				for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
				{
					DataRow dataRow2 = dtItemPrices.NewRow();
					dataRow2["ItemPriceID"] = -1;
					dataRow2["ItemID"] = -1;
					dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
					dataRow2["Price"] = 0;
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
					dtItemPrices.Rows.Add(dataRow2);
				}
			}
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		((UltraGridBase)ULGPrices).DataSource = dtItemPrices;
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemPriceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		if (rbPriceForAllBranches.Checked)
		{
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
			return;
		}
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
	}

	private void FillGridStockLevels()
	{
		dtItemStockLevels.Rows.Clear();
		if (rbStockLevelsForAllBranches.Checked)
		{
			DataRow dataRow = dtItemStockLevels.NewRow();
			dataRow["ItemStockLevels"] = -1;
			dataRow["ItemID"] = -1;
			dataRow["MinLevel"] = 0;
			dataRow["ReOrderLevel"] = 0;
			dataRow["MaxLevel"] = 0;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = DBNull.Value;
			dtItemStockLevels.Rows.Add(dataRow);
		}
		else
		{
			for (int i = 0; i < dtBranches.Rows.Count; i++)
			{
				DataRow dataRow2 = dtItemStockLevels.NewRow();
				dataRow2["ItemStockLevels"] = -1;
				dataRow2["ItemID"] = -1;
				dataRow2["MinLevel"] = 0;
				dataRow2["ReOrderLevel"] = 0;
				dataRow2["MaxLevel"] = 0;
				dataRow2["Deleted"] = false;
				dataRow2["BranchID"] = dtBranches.Rows[i]["BranchID"];
				dtItemStockLevels.Rows.Add(dataRow2);
			}
		}
		InitGridStockLevels();
	}

	private void InitGridStockLevels()
	{
		((UltraGridBase)ULGLevels).DataSource = dtItemStockLevels;
		GlobalFunctions.PrepareGrid(ULGLevels);
		((UltraGridBase)ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ItemStockLevels"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Header).Caption = (GlobalVariables.IsArabic ? "الأدني" : "Min");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Header).Caption = (GlobalVariables.IsArabic ? "اعدة الطلب" : "Reorder");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Header).Caption = (GlobalVariables.IsArabic ? "الأقصي" : "Max");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Hidden = false;
		if (rbStockLevelsForAllBranches.Checked)
		{
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.33) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.33);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.34);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		}
		else
		{
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["ReOrderLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxLevel"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25);
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.25);
			((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches2;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
		}
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void ULGLevels_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGLevels.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGLevels).ActiveRow).Selected = true;
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
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.MasterData.frmLensesItemsGroups));
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
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		this.ultraPanel1 = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.ultraPanel2 = new UltraPanel();
		this.rbStockLevelForEachBranch = new System.Windows.Forms.RadioButton();
		this.rbStockLevelsForAllBranches = new System.Windows.Forms.RadioButton();
		this.txtCylFrom = new UltraTextEditor();
		this.txtSpFrom = new UltraTextEditor();
		this.txtPeriod = new UltraTextEditor();
		this.lblRoot = new UltraLabel();
		this.cboRoot = new UltraComboEditor();
		this.lblSyl = new UltraLabel();
		this.lblSP = new UltraLabel();
		this.lblPeriod = new UltraLabel();
		this.cboGroup = new UltraComboEditor();
		this.lblGroup = new UltraLabel();
		this.lblUnitGroup = new UltraLabel();
		this.cboUnitGroup = new UltraComboEditor();
		this.lblUnit = new UltraLabel();
		this.cboUnit = new UltraComboEditor();
		this.cboStore = new UltraComboEditor();
		this.lblStore = new UltraLabel();
		this.txtCylTo = new UltraTextEditor();
		this.txtSpTo = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGPrices = new UltraGrid();
		this.ULGLevels = new UltraGrid();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.txtInSPCYL = new UltraTextEditor();
		this.ultraLabel7 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCylFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSpFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRoot).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCylTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSpTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInSPCYL).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraPanel1, "ultraPanel1");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance1");
		this.ultraPanel1.Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.ultraPanel1.ClientArea, "ultraPanel1.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForEachBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1).Name = "ultraPanel1";
		resources.ApplyResources(this.rbPriceForEachBranches, "rbPriceForEachBranches");
		this.rbPriceForEachBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForEachBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForEachBranches.Name = "rbPriceForEachBranches";
		this.rbPriceForEachBranches.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbPriceForAllBranches, "rbPriceForAllBranches");
		this.rbPriceForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForAllBranches.Checked = true;
		this.rbPriceForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForAllBranches.Name = "rbPriceForAllBranches";
		this.rbPriceForAllBranches.TabStop = true;
		this.rbPriceForAllBranches.UseVisualStyleBackColor = false;
		this.rbPriceForAllBranches.CheckedChanged += new System.EventHandler(rbPriceForAllBranches_CheckedChanged);
		resources.ApplyResources(this.ultraPanel2, "ultraPanel2");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val2, "appearance2");
		this.ultraPanel2.Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(this.ultraPanel2.ClientArea, "ultraPanel2.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).Controls.Add(this.rbStockLevelForEachBranch);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).Controls.Add(this.rbStockLevelsForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel2).Name = "ultraPanel2";
		resources.ApplyResources(this.rbStockLevelForEachBranch, "rbStockLevelForEachBranch");
		this.rbStockLevelForEachBranch.BackColor = System.Drawing.Color.Transparent;
		this.rbStockLevelForEachBranch.ForeColor = System.Drawing.Color.Navy;
		this.rbStockLevelForEachBranch.Name = "rbStockLevelForEachBranch";
		this.rbStockLevelForEachBranch.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbStockLevelsForAllBranches, "rbStockLevelsForAllBranches");
		this.rbStockLevelsForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbStockLevelsForAllBranches.Checked = true;
		this.rbStockLevelsForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbStockLevelsForAllBranches.Name = "rbStockLevelsForAllBranches";
		this.rbStockLevelsForAllBranches.TabStop = true;
		this.rbStockLevelsForAllBranches.UseVisualStyleBackColor = false;
		this.rbStockLevelsForAllBranches.CheckedChanged += new System.EventHandler(rbStockLevelsForAllBranches_CheckedChanged);
		resources.ApplyResources(this.txtCylFrom, "txtCylFrom");
		((System.Windows.Forms.Control)(object)this.txtCylFrom).Name = "txtCylFrom";
		((System.Windows.Forms.Control)(object)this.txtCylFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSpFrom, "txtSpFrom");
		((System.Windows.Forms.Control)(object)this.txtSpFrom).Name = "txtSpFrom";
		((System.Windows.Forms.Control)(object)this.txtSpFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtPeriod, "txtPeriod");
		((System.Windows.Forms.Control)(object)this.txtPeriod).Name = "txtPeriod";
		((System.Windows.Forms.Control)(object)this.txtPeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblRoot, "lblRoot");
		this.lblRoot.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoot).Name = "lblRoot";
		((ControlBase)this.lblRoot).WrapText = false;
		resources.ApplyResources(this.cboRoot, "cboRoot");
		this.cboRoot.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboRoot).Name = "cboRoot";
		((TextEditorControlBase)this.cboRoot).Nullable = false;
		((TextEditorControlBase)this.cboRoot).ValueChanged += new System.EventHandler(cboRoot_ValueChanged);
		resources.ApplyResources(this.lblSyl, "lblSyl");
		this.lblSyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSyl).Name = "lblSyl";
		((ControlBase)this.lblSyl).WrapText = false;
		resources.ApplyResources(this.lblSP, "lblSP");
		this.lblSP.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSP).Name = "lblSP";
		((ControlBase)this.lblSP).WrapText = false;
		resources.ApplyResources(this.lblPeriod, "lblPeriod");
		this.lblPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPeriod).Name = "lblPeriod";
		((ControlBase)this.lblPeriod).WrapText = false;
		resources.ApplyResources(this.cboGroup, "cboGroup");
		this.cboGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroup).Name = "cboGroup";
		((TextEditorControlBase)this.cboGroup).Nullable = false;
		resources.ApplyResources(this.lblGroup, "lblGroup");
		this.lblGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroup).Name = "lblGroup";
		((ControlBase)this.lblGroup).WrapText = false;
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		this.lblUnitGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		this.cboUnitGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		((TextEditorControlBase)this.cboUnitGroup).Nullable = false;
		((TextEditorControlBase)this.cboUnitGroup).ValueChanged += new System.EventHandler(cboUnitGroup_ValueChanged);
		resources.ApplyResources(this.lblUnit, "lblUnit");
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.cboUnit, "cboUnit");
		this.cboUnit.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		((TextEditorControlBase)this.cboUnit).Nullable = false;
		resources.ApplyResources(this.cboStore, "cboStore");
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((TextEditorControlBase)this.cboStore).Nullable = false;
		resources.ApplyResources(this.lblStore, "lblStore");
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		((ControlBase)this.lblStore).WrapText = false;
		resources.ApplyResources(this.txtCylTo, "txtCylTo");
		((System.Windows.Forms.Control)(object)this.txtCylTo).Name = "txtCylTo";
		((System.Windows.Forms.Control)(object)this.txtCylTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSpTo, "txtSpTo");
		((System.Windows.Forms.Control)(object)this.txtSpTo).Name = "txtSpTo";
		((System.Windows.Forms.Control)(object)this.txtSpTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val5;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val10;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val15, "appearance15");
		((AppearanceBase)val15).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val16).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val16).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val16).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val17).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		resources.ApplyResources(this.ULGLevels, "ULGLevels");
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val19).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val19).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val19, "appearance19");
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val19;
		((AppearanceBase)val20).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val20;
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val21).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val22;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val25, "appearance25");
		((AppearanceBase)val25).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val26).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val26).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val26).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val27, "appearance27");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.ULGLevels).Name = "ULGLevels";
		this.ULGLevels.AfterEnterEditMode += new System.EventHandler(ULGLevels_AfterEnterEditMode);
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.txtInSPCYL, "txtInSPCYL");
		((System.Windows.Forms.Control)(object)this.txtInSPCYL).Name = "txtInSPCYL";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGLevels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSP);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSpTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCylTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSpFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInSPCYL);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCylFrom);
		base.Name = "frmLensesItemsGroups";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCylFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInSPCYL, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSpFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCylTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSpTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSyl, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSP, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnitGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraPanel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGPrices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraPanel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGLevels, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel2.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtCylFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSpFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRoot).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCylTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSpTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInSPCYL).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
