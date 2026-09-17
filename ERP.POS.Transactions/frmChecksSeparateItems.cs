using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmChecksSeparateItems : frmBase
{
	public frmChecksSeparate frmCheckSeparat;

	private DataTable DtMasterDetails = new DataTable();

	public DataTable DtDetails = new DataTable();

	public DataTable DtMasterDetailsCopy = new DataTable();

	public DataRow DrHeader = null;

	private DataRow DRRoomData;

	private ValueList vlItemsCheck = new ValueList();

	private ValueList vlItemsSource = new ValueList();

	private ValueList vlUnitsCheck = new ValueList();

	private ValueList vlUnitsSource = new ValueList();

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraGroupBox UGBCheck;

	public UltraGrid ULGDataCheck;

	private UltraLabel lblDiscount;

	private UltraTextEditor txtDiscount;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	private UltraLabel lblTaxTotalValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraLabel lblServiceChargeValue;

	private UltraTextEditor txtServiceChargeValue;

	private UltraLabel lblGrossValue;

	private UltraTextEditor txtGrossValue;

	public UltraGroupBox UGBSource;

	public UltraGrid ULGDataSource;

	public UltraButton btnMoveToSource;

	public UltraButton btnMoveToCheck;

	public UltraButton btnSave;

	public UltraButton btnClientSearch;

	private UltraLabel lblClient;

	private UltraComboEditor cboClient;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	public UltraButton btnSourceToCheckAll;

	public UltraButton btnCheckToSourceAll;

	public UltraButton btnSplit;

	public frmChecksSeparateItems()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmChecksSeparateItems(DataTable dtmasterdetails, DataRow drheader, DataTable dtdetails, DataRow drroomdata)
		: this()
	{
		DtMasterDetails = dtmasterdetails;
		DtMasterDetailsCopy = dtmasterdetails.Copy();
		DrHeader = drheader;
		DtDetails = dtdetails.Copy();
		DRRoomData = drroomdata;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		for (int i = 0; i < frmCheckSeparat.DVItems.Count; i++)
		{
			vlItemsCheck.ValueListItems.Add(frmCheckSeparat.DVItems[i]["ItemID"], frmCheckSeparat.DVItems[i]["ItemName"].ToString());
			vlItemsSource.ValueListItems.Add(frmCheckSeparat.DVItems[i]["ItemID"], frmCheckSeparat.DVItems[i]["ItemName"].ToString());
		}
		vlUnitsCheck.ValueListItems.Clear();
		for (int j = 0; j < frmCheckSeparat.DtUnits.Rows.Count; j++)
		{
			vlUnitsCheck.ValueListItems.Add(frmCheckSeparat.DtUnits.Rows[j]["UnitID"], frmCheckSeparat.DtUnits.Rows[j]["UnitName"].ToString());
			vlUnitsSource.ValueListItems.Add(frmCheckSeparat.DtUnits.Rows[j]["UnitID"], frmCheckSeparat.DtUnits.Rows[j]["UnitName"].ToString());
		}
		GlobalFunctions.FillCombo(cboClient, frmCheckSeparat.DtClients, "SubAccountID", "SubAccountName");
		((UltraGridBase)ULGDataSource).DataSource = DtMasterDetailsCopy;
		((UltraGridBase)ULGDataCheck).DataSource = DtDetails;
		InitGrid();
		((TextEditorControlBase)cboClient).Value = DrHeader["SubAccountID"];
		((Control)(object)txtNotes).Text = DrHeader["Notes"].ToString();
		UpdateCheckData();
		UltraButton obj = btnMoveToCheck;
		bool enabled = (((Control)(object)btnMoveToSource).Enabled = false);
		((Control)(object)obj).Enabled = enabled;
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataCheck);
		((UltraGridBase)ULGDataCheck).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["CheckDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataCheck).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataCheck).Width * 0.15);
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataCheck).Width * 0.15);
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGDataCheck).Width * 0.23);
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGDataCheck).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsCheck;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitsCheck;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["ServiceChargeAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataCheck).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		GlobalFunctions.PrepareGrid(ULGDataSource);
		((UltraGridBase)ULGDataSource).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["CheckDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGDataSource).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGDataSource).Width * 0.15);
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataSource).Width * 0.15);
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGDataSource).Width * 0.23);
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGDataSource).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsSource;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnitsSource;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ReturnedQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["Discount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ActualUnitSalesPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["ServiceChargeAmount"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataSource).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	private void btnMoveToCheck_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataSource).ActiveRow == null)
		{
			return;
		}
		if (((DataTable)((UltraGridBase)ULGDataCheck).DataSource).Select(" CheckDetailID = " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString()).Length == 0)
		{
			DtMasterDetailsCopy.AcceptChanges();
			DtDetails.ImportRow(DtMasterDetailsCopy.Rows[((UltraGridBase)ULGDataSource).ActiveRow.Index]);
			((Control)(object)btnMoveToCheck).Enabled = false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCheck).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataCheck).Rows[i].Cells["CheckDetailID"].Value.ToString())
			{
				((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value = "0";
				frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"], ((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value.ToString());
				frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
				frmDecimal2.ShowDialog();
				frmDecimal2.Focus();
				if (decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value.ToString()) >= decimal.Parse(DtMasterDetails.Select(" CheckDetailID= " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]["Qty"].ToString()))
				{
					((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value = DtMasterDetails.Select(" CheckDetailID= " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]["Qty"].ToString();
					((UltraGridBase)ULGDataSource).ActiveRow.Delete(false);
				}
				else
				{
					((UltraGridBase)ULGDataSource).ActiveRow.Cells["Qty"].Value = decimal.Parse(DtMasterDetails.Select(" CheckDetailID= " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]["Qty"].ToString()) - decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value.ToString());
				}
				break;
			}
		}
		UpdateSourceData();
		UpdateCheckData();
	}

	private void btnMoveToSource_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataCheck).ActiveRow == null)
		{
			return;
		}
		if (((DataTable)((UltraGridBase)ULGDataSource).DataSource).Select(" CheckDetailID = " + ((UltraGridBase)ULGDataCheck).ActiveRow.Cells["CheckDetailID"].Value.ToString()).Length == 0)
		{
			DtMasterDetailsCopy.ImportRow(DtDetails.Select(" CheckDetailID = " + ((UltraGridBase)ULGDataCheck).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]);
			((Control)(object)btnMoveToSource).Enabled = false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSource).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataCheck).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataSource).Rows[i].Cells["CheckDetailID"].Value.ToString())
			{
				((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value = "0";
				frmDecimal frmDecimal2 = new frmDecimal(((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"], ((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value.ToString());
				frmDecimal2.StartPosition = FormStartPosition.CenterScreen;
				frmDecimal2.ShowDialog();
				if (decimal.Parse(((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value.ToString()) >= decimal.Parse(((UltraGridBase)ULGDataCheck).ActiveRow.Cells["Qty"].Value.ToString()))
				{
					((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value = ((UltraGridBase)ULGDataCheck).ActiveRow.Cells["Qty"].Value.ToString();
					((UltraGridBase)ULGDataCheck).ActiveRow.Delete(false);
				}
				else
				{
					((UltraGridBase)ULGDataCheck).ActiveRow.Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGDataCheck).ActiveRow.Cells["Qty"].Value.ToString()) - decimal.Parse(((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value.ToString());
				}
				break;
			}
		}
		UpdateSourceData();
		UpdateCheckData();
	}

	private void btnSourceToCheckAll_Click(object sender, EventArgs e)
	{
		bool flag = false;
		if (((UltraGridBase)ULGDataSource).ActiveRow != null)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCheck).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataCheck).Rows[i].Cells["CheckDetailID"].Value.ToString())
				{
					((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGDataSource).ActiveRow.Cells["Qty"].Value.ToString());
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				DtDetails.ImportRow(DtMasterDetailsCopy.Select(" CheckDetailID = " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]);
			}
			((UltraGridBase)ULGDataSource).ActiveRow.Delete(false);
		}
		((UltraGridBase)ULGDataCheck).UpdateData();
		UpdateSourceData();
		UpdateCheckData();
	}

	private void btnCheckToSourceAll_Click(object sender, EventArgs e)
	{
		bool flag = false;
		if (((UltraGridBase)ULGDataCheck).ActiveRow != null)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSource).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGDataCheck).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataSource).Rows[i].Cells["CheckDetailID"].Value.ToString())
				{
					((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGDataCheck).ActiveRow.Cells["Qty"].Value.ToString());
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				DtMasterDetailsCopy.ImportRow(DtDetails.Select(" CheckDetailID = " + ((UltraGridBase)ULGDataCheck).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]);
			}
			((UltraGridBase)ULGDataCheck).ActiveRow.Delete(false);
		}
		((UltraGridBase)ULGDataSource).UpdateData();
		UpdateSourceData();
		UpdateCheckData();
	}

	private void ULGDataCheck_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataCheck).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGDataCheck).ActiveRow).Selected = true;
		}
		bool enabled = true;
		if (((UltraGridBase)ULGDataCheck).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (int.Parse(frmCheckSeparat.DVItems.Table.Select(" ItemID = " + ((UltraGridBase)ULGDataCheck).ActiveRow.Cells["ItemID"].Value.ToString())[0]["AccessoriesCount"].ToString()) > 0 || int.Parse(frmCheckSeparat.DVItems.Table.Select(" ItemID = " + ((UltraGridBase)ULGDataCheck).ActiveRow.Cells["ItemID"].Value.ToString())[0]["AdditionalCount"].ToString()) > 0))
		{
			enabled = false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSource).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataCheck).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataSource).Rows[i].Cells["CheckDetailID"].Value.ToString())
			{
				((UltraGridBase)ULGDataSource).Rows[i].Activate();
				enabled = false;
				break;
			}
		}
		((Control)(object)btnMoveToSource).Enabled = enabled;
		((Control)(object)btnSplit).Enabled = false;
	}

	private void ULGDataSource_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataSource).ActiveRow == null)
		{
			return;
		}
		((GridItemBase)((UltraGridBase)ULGDataSource).ActiveRow).Selected = true;
		bool flag = false;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCheck).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataCheck).Rows[i].Cells["CheckDetailID"].Value.ToString())
			{
				((UltraGridBase)ULGDataCheck).Rows[i].Activate();
				((Control)(object)btnMoveToCheck).Enabled = false;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			((Control)(object)btnMoveToCheck).Enabled = true;
		}
		bool enabled = true;
		bool enabled2 = true;
		if (((UltraGridBase)ULGDataSource).ActiveRow.Cells["ItemID"].Value != DBNull.Value && (int.Parse(frmCheckSeparat.DVItems.Table.Select(" ItemID = " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["ItemID"].Value.ToString())[0]["AccessoriesCount"].ToString()) > 0 || int.Parse(frmCheckSeparat.DVItems.Table.Select(" ItemID = " + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["ItemID"].Value.ToString())[0]["AdditionalCount"].ToString()) > 0))
		{
			enabled = false;
			enabled2 = false;
		}
		for (int j = 0; j < frmCheckSeparat.dtDetails.Count; j++)
		{
			DataTable dataTable = (DataTable)frmCheckSeparat.dtDetails[j];
			dataTable.AcceptChanges();
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				if (dataTable.Rows[k]["CheckDetailID"].ToString() == ((object)((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"]).ToString())
				{
					enabled = false;
					break;
				}
			}
		}
		((Control)(object)btnSplit).Enabled = enabled;
		((Control)(object)btnMoveToCheck).Enabled = enabled2;
	}

	private void UpdateAllCheckData()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		decimal num4 = default(decimal);
		for (int i = 0; i < frmCheckSeparat.dtDetails.Count; i++)
		{
			num = default(decimal);
			num2 = default(decimal);
			num3 = default(decimal);
			num4 = default(decimal);
			DataRow dataRow = (DataRow)frmCheckSeparat.DrHeaderRow[i];
			DataTable dataTable = (DataTable)frmCheckSeparat.dtDetails[i];
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				dataTable.Rows[j]["TotalPrice"] = decimal.Parse(dataTable.Rows[j]["UnitPrice"].ToString()) * decimal.Parse(dataTable.Rows[j]["Qty"].ToString());
				dataTable.Rows[j]["NetPrice"] = decimal.Parse(dataTable.Rows[j]["TotalPrice"].ToString()) - decimal.Parse(dataTable.Rows[j]["Discount"].ToString());
				dataTable.Rows[j]["ServiceChargeAmount"] = decimal.Parse(dataTable.Rows[j]["NetPrice"].ToString()) * decimal.Parse(DRRoomData["ServiceChargePercent"].ToString()) / 100m;
				if (dataTable.Rows[j]["TaxID"] != DBNull.Value)
				{
					dataTable.Rows[j]["TaxValue"] = decimal.Parse(frmCheckSeparat.DtTaxs.Select(" TaxID= " + dataTable.Rows[j]["TaxID"].ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(dataTable.Rows[j]["TotalPrice"].ToString()) - decimal.Parse(dataTable.Rows[j]["Discount"].ToString()) + decimal.Parse(dataTable.Rows[j]["ServiceChargeAmount"].ToString()));
				}
				else
				{
					dataTable.Rows[j]["TaxValue"] = 0;
				}
				num += decimal.Parse(dataTable.Rows[j]["TotalPrice"].ToString());
				num2 += decimal.Parse(dataTable.Rows[j]["Discount"].ToString());
				num3 += decimal.Parse(dataTable.Rows[j]["TaxValue"].ToString());
				num4 += decimal.Parse(dataTable.Rows[j]["ServiceChargeAmount"].ToString());
			}
			dataRow["GrossValue"] = num.ToString();
			dataRow["DiscountBeforeTaxValue"] = num2.ToString();
			dataRow["TaxTotalValue"] = num3.ToString();
			dataRow["ServiceChargeValue"] = num4.ToString();
			dataRow["NetPrice"] = (num - decimal.Parse((dataRow["DiscountBeforeTaxValue"] == DBNull.Value) ? "0" : dataRow["DiscountBeforeTaxValue"].ToString()) + num4 + num3).ToString();
		}
	}

	private void UpdateCheckData()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		decimal num4 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCheck).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataCheck).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["Qty"].Value.ToString());
			CalculateRow(((UltraGridBase)ULGDataCheck).Rows[i]);
			num += decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["TotalPrice"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["Discount"].Value.ToString());
			num3 += decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["TaxValue"].Value.ToString());
			num4 += decimal.Parse(((UltraGridBase)ULGDataCheck).Rows[i].Cells["ServiceChargeAmount"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = num.ToString();
		((Control)(object)txtDiscount).Text = num2.ToString();
		((Control)(object)txtTaxTotalValue).Text = num3.ToString();
		((Control)(object)txtServiceChargeValue).Text = num4.ToString();
		((Control)(object)txtNetprice).Text = (decimal.Parse((((Control)(object)txtGrossValue).Text == "") ? "0" : ((Control)(object)txtGrossValue).Text) - decimal.Parse((((Control)(object)txtDiscount).Text == "") ? "0" : ((Control)(object)txtDiscount).Text) + decimal.Parse((((Control)(object)txtServiceChargeValue).Text == "") ? "0" : ((Control)(object)txtServiceChargeValue).Text) + decimal.Parse((((Control)(object)txtTaxTotalValue).Text == "") ? "0" : ((Control)(object)txtTaxTotalValue).Text)).ToString();
	}

	private void UpdateSourceData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSource).Rows).Count; i++)
		{
			((UltraGridBase)ULGDataSource).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGDataSource).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGDataSource).Rows[i].Cells["Qty"].Value.ToString());
			CalculateRow(((UltraGridBase)ULGDataSource).Rows[i]);
		}
	}

	private void CalculateRow(UltraGridRow Row)
	{
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString());
		Row.Cells["ServiceChargeAmount"].Value = decimal.Parse(Row.Cells["NetPrice"].Value.ToString()) * decimal.Parse(DRRoomData["ServiceChargePercent"].ToString()) / 100m;
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(frmCheckSeparat.DtTaxs.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * (decimal.Parse(Row.Cells["TotalPrice"].Value.ToString()) - decimal.Parse(Row.Cells["Discount"].Value.ToString()) + decimal.Parse(Row.Cells["ServiceChargeAmount"].Value.ToString()));
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void btnSplit_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGDataSource).ActiveRow == null)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataCheck).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString() == ((UltraGridBase)ULGDataCheck).Rows[i].Cells["CheckDetailID"].Value.ToString())
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			DtDetails.Select(" CheckDetailID =" + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0].Delete();
		}
		for (int j = 0; j < frmCheckSeparat.dtDetails.Count; j++)
		{
			DataTable dataTable = (DataTable)frmCheckSeparat.dtDetails[j];
			dataTable.ImportRow(DtMasterDetails.Select("CheckDetailID =" + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]);
			DataRow dataRow = dataTable.Select("CheckDetailID =" + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0];
			dataRow["Qty"] = decimal.Parse(dataRow["Qty"].ToString()) / (decimal)frmCheckSeparat.dtDetails.Count;
		}
		DtMasterDetails.Select("CheckDetailID =" + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]["Qty"] = decimal.Parse(DtMasterDetails.Select("CheckDetailID =" + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]["Qty"].ToString()) / (decimal)frmCheckSeparat.dtDetails.Count;
		DtDetails.ImportRow(DtMasterDetails.Select("CheckDetailID =" + ((UltraGridBase)ULGDataSource).ActiveRow.Cells["CheckDetailID"].Value.ToString())[0]);
		((UltraGridBase)ULGDataSource).ActiveRow.Delete(false);
		((UltraGridBase)ULGDataSource).UpdateData();
		((UltraGridBase)ULGDataCheck).UpdateData();
		UpdateCheckData();
		UpdateAllCheckData();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		DrHeader["SubAccountID"] = ((cboClient.SelectedIndex == -1) ? DBNull.Value : ((TextEditorControlBase)cboClient).Value);
		DrHeader["Notes"] = ((Control)(object)txtNotes).Text;
		DrHeader["GrossValue"] = ((Control)(object)txtGrossValue).Text;
		DrHeader["DiscountBeforeTaxValue"] = ((Control)(object)txtDiscount).Text;
		DrHeader["ServiceChargeValue"] = ((Control)(object)txtServiceChargeValue).Text;
		DrHeader["TaxTotalValue"] = ((Control)(object)txtTaxTotalValue).Text;
		DrHeader["NetPrice"] = ((Control)(object)txtNetprice).Text;
		((UltraGridBase)ULGDataCheck).UpdateData();
		((UltraGridBase)ULGDataSource).UpdateData();
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksSeparateItems));
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
		this.lblTitle = new UltraLabel();
		this.UGBCheck = new UltraGroupBox();
		this.ULGDataCheck = new UltraGrid();
		this.lblDiscount = new UltraLabel();
		this.txtDiscount = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.txtServiceChargeValue = new UltraTextEditor();
		this.txtGrossValue = new UltraTextEditor();
		this.lblServiceChargeValue = new UltraLabel();
		this.lblGrossValue = new UltraLabel();
		this.UGBSource = new UltraGroupBox();
		this.ULGDataSource = new UltraGrid();
		this.btnMoveToSource = new UltraButton();
		this.btnMoveToCheck = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnClientSearch = new UltraButton();
		this.lblClient = new UltraLabel();
		this.cboClient = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.btnSourceToCheckAll = new UltraButton();
		this.btnCheckToSourceAll = new UltraButton();
		this.btnSplit = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBCheck).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBCheck).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCheck).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBSource).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBSource).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataSource).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
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
		resources.ApplyResources(this.UGBCheck, "UGBCheck");
		this.UGBCheck.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBCheck).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataCheck);
		((System.Windows.Forms.Control)(object)this.UGBCheck).Name = "UGBCheck";
		resources.ApplyResources(this.ULGDataCheck, "ULGDataCheck");
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val2).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val2, "appearance2");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCheck).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val3;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCheck).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance8");
		((AppearanceBase)val8).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGDataCheck).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ULGDataCheck).Name = "ULGDataCheck";
		this.ULGDataCheck.AfterEnterEditMode += new System.EventHandler(ULGDataCheck_AfterEnterEditMode);
		resources.ApplyResources(this.lblDiscount, "lblDiscount");
		this.lblDiscount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscount).Name = "lblDiscount";
		((ControlBase)this.lblDiscount).WrapText = false;
		resources.ApplyResources(this.txtDiscount, "txtDiscount");
		((System.Windows.Forms.Control)(object)this.txtDiscount).Name = "txtDiscount";
		((EditorButtonControlBase)this.txtDiscount).ReadOnly = true;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.txtServiceChargeValue, "txtServiceChargeValue");
		((System.Windows.Forms.Control)(object)this.txtServiceChargeValue).Name = "txtServiceChargeValue";
		((EditorButtonControlBase)this.txtServiceChargeValue).ReadOnly = true;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.lblServiceChargeValue, "lblServiceChargeValue");
		this.lblServiceChargeValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceChargeValue).Name = "lblServiceChargeValue";
		((ControlBase)this.lblServiceChargeValue).WrapText = false;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.UGBSource, "UGBSource");
		this.UGBSource.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBSource).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataSource);
		((System.Windows.Forms.Control)(object)this.UGBSource).Name = "UGBSource";
		resources.ApplyResources(this.ULGDataSource, "ULGDataSource");
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataSource).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val13;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataSource).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val17;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val18, "appearance18");
		((AppearanceBase)val18).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val19).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val19).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val19).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val21, "appearance21");
		((UltraGridBase)this.ULGDataSource).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.ULGDataSource).Name = "ULGDataSource";
		this.ULGDataSource.AfterEnterEditMode += new System.EventHandler(ULGDataSource_AfterEnterEditMode);
		resources.ApplyResources(this.btnMoveToSource, "btnMoveToSource");
		((System.Windows.Forms.Control)(object)this.btnMoveToSource).Name = "btnMoveToSource";
		((System.Windows.Forms.Control)(object)this.btnMoveToSource).Tag = "";
		((System.Windows.Forms.Control)(object)this.btnMoveToSource).Click += new System.EventHandler(btnMoveToSource_Click);
		resources.ApplyResources(this.btnMoveToCheck, "btnMoveToCheck");
		((System.Windows.Forms.Control)(object)this.btnMoveToCheck).Name = "btnMoveToCheck";
		((System.Windows.Forms.Control)(object)this.btnMoveToCheck).Tag = "";
		((System.Windows.Forms.Control)(object)this.btnMoveToCheck).Click += new System.EventHandler(btnMoveToCheck_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val22).Image = resources.GetObject("appearance22.Image");
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val22;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val23).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.btnSourceToCheckAll, "btnSourceToCheckAll");
		((System.Windows.Forms.Control)(object)this.btnSourceToCheckAll).Name = "btnSourceToCheckAll";
		((System.Windows.Forms.Control)(object)this.btnSourceToCheckAll).Tag = "";
		((System.Windows.Forms.Control)(object)this.btnSourceToCheckAll).Click += new System.EventHandler(btnSourceToCheckAll_Click);
		resources.ApplyResources(this.btnCheckToSourceAll, "btnCheckToSourceAll");
		((System.Windows.Forms.Control)(object)this.btnCheckToSourceAll).Name = "btnCheckToSourceAll";
		((System.Windows.Forms.Control)(object)this.btnCheckToSourceAll).Tag = "";
		((System.Windows.Forms.Control)(object)this.btnCheckToSourceAll).Click += new System.EventHandler(btnCheckToSourceAll_Click);
		resources.ApplyResources(this.btnSplit, "btnSplit");
		((System.Windows.Forms.Control)(object)this.btnSplit).Name = "btnSplit";
		((System.Windows.Forms.Control)(object)this.btnSplit).Click += new System.EventHandler(btnSplit_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSplit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCheckToSourceAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSourceToCheckAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtServiceChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToSource);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToCheck);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBSource);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBCheck);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmChecksSeparateItems";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBCheck, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBSource, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToCheck, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToSource, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblServiceChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtServiceChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSourceToCheckAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCheckToSourceAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSplit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscount, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBCheck).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBCheck).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataCheck).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBSource).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBSource).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataSource).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
