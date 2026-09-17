using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Reports;

public class frmGenerateBarCode : frmBase
{
	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtItemSizes;

	private DataTable dtColors;

	private DataTable dtBatchs;

	private DataTable dtUnits;

	private DataTable dtPriceTypes;

	private DataTable dtPriceLists;

	private ValueList vlItems = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlItemSizes = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlUnits = new ValueList();

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool UsingItemSize = false;

	private bool UsingColor = false;

	private string PriceTypeID;

	private string PriceListID;

	private DataTable dtDetails;

	private IContainer components = null;

	private UltraButton btnPrintBarCode;

	private UltraLabel lblDefultQuantity;

	private UltraTextEditor txtDefultQuantity;

	public UltraGrid ULGItems;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraComboEditor cboReportType;

	public UltraLabel lblReportType;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraButton btnSelectItems;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnNew;

	private UltraComboEditor cboPriceType;

	private UltraComboEditor cboPriceList;

	private UltraCheckEditor chkPriceList;

	private UltraCheckEditor chkPriceType;

	public UltraButton btnOpenTicket;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalQty;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public frmGenerateBarCode()
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
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, "A4 (3-7)");
		cboReportType.Items.Add((object)2, "A4 (3-10)");
		cboReportType.Items.Add((object)3, "A4 (4-10)");
		cboReportType.Items.Add((object)4, "A4 (4-14)");
		cboReportType.Items.Add((object)5, "A4 (5-13)");
		cboReportType.Items.Add((object)6, "A4 (6-16)");
		cboReportType.Items.Add((object)7, "A4 (6-24)");
		cboReportType.Items.Add((object)8, "Roll");
		cboReportType.Items.Add((object)9, "Roll2");
		cboReportType.Items.Add((object)10, "Roll2-WithDiscount");
		cboReportType.Items.Add((object)11, "Roll2.5-5");
		cboReportType.Items.Add((object)12, "Roll2.5-5 With Price");
		cboReportType.Items.Add((object)13, "Roll2.5-5 With Price With Brand");
		cboReportType.Items.Add((object)14, "A4 (2-2)BoxWithBatchNo");
		cboReportType.Items.Add((object)15, "A4 (2-5)WithBatchNo");
		cboReportType.SelectedIndex = 0;
	}

	public frmGenerateBarCode(DataTable Details)
		: this()
	{
		dtDetails = Details;
	}

	public override void PrepareData()
	{
		if (base.Tag != null)
		{
			((TextEditorControlBase)cboReportType).Clear();
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
			cboReportType.SelectedIndex = 0;
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		UsingItemSize = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingColor = GlobalFunctions.GetOption("UsingItemsColors");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int i = 0; i < dtBatchs.Rows.Count; i++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
			}
		}
		if (UsingColor)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int j = 0; j < dtColors.Rows.Count; j++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[j]["ColorID"], dtColors.Rows[j]["ColorName"].ToString());
			}
		}
		if (UsingItemSize)
		{
			dtItemSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlItemSizes.ValueListItems.Clear();
			for (int k = 0; k < dtItemSizes.Rows.Count; k++)
			{
				vlItemSizes.ValueListItems.Add(dtItemSizes.Rows[k]["ItemSizeID"], dtItemSizes.Rows[k]["ItemSizeName"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int m = 0; m < dtItems.Rows.Count; m++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[m]["ItemID"], dtItems.Rows[m]["Name"].ToString());
		}
		if (dtDetails == null)
		{
			dtDetails = Main.ExecuteQuery_DataTable("select " + (GlobalVariables.IsArabic ? "'' as Notes, null as UnitID, 1 Qty,null as BatchID,1 as ItemSizeID,1 as ColorID ,null as ItemID" : "null as ItemID,1 as ColorID,1 as ItemSizeID,null as BatchID,1 Qty , null as UnitID, '' as Notes") + " Where 1=2 ");
		}
		dtPriceTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceTypes, "PriceTypeID", "PriceName");
		dtPriceLists = PriceLists.FillCombo("-1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceList, dtPriceLists, "PriceListID", "PriceListNo");
		Adding = true;
		InitGrid();
		((TextEditorControlBase)txtDefultQuantity).ValueChanged -= txtDefultQuantity_ValueChanged;
		((Control)(object)txtDefultQuantity).Text = "1";
		((TextEditorControlBase)txtDefultQuantity).ValueChanged += txtDefultQuantity_ValueChanged;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
	}

	public void InitGrid()
	{
		((UltraGridBase)ULGItems).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGItems);
		((UltraGridBase)ULGItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(Adding ? 6 : 2);
		((UltraGridBase)ULGItems).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)(Adding ? 1 : 2);
		((UltraGridBase)ULGItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.1);
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		if (UsingItemSize)
		{
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.1);
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = (GlobalVariables.IsArabic ? "مقاس" : "Size");
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlItemSizes;
		}
		if (UsingColor)
		{
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.1);
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = (GlobalVariables.IsArabic ? "لون" : "Color");
			((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		}
		((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه المطبوعه" : "Quantity");
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.2);
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((HeaderBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGItems).Width * 0.2);
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Notes"].Hidden = !dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NotesWithPrice");
	}

	private void btnPrintBarCode_Click(object sender, EventArgs e)
	{
		if (!Check_Gride_ForError(ULGItems))
		{
			if (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NumberOne"))
			{
				ItemsBarCode.Insert_ByTable((DataTable)((UltraGridBase)ULGItems).DataSource, 10);
			}
			else if (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NumberOne6"))
			{
				ItemsBarCode.Insert_ByTable((DataTable)((UltraGridBase)ULGItems).DataSource, 6);
			}
			else
			{
				ItemsBarCode.Insert_ByTable((DataTable)((UltraGridBase)ULGItems).DataSource, 1);
			}
			ShowReport();
		}
	}

	private void txtDefultQuantity_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void grdItems_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGItems.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForIntegers(sender, e);
		}
	}

	public string GetReportName()
	{
		string text = GlobalVariables.ReportsPath;
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			text += "Rep_SC_ItemsBarCode_A4(3-7).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "2")
		{
			text += "Rep_SC_ItemsBarCode_A4(3-10).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "3")
		{
			text += "Rep_SC_ItemsBarCode_A4(4-10).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "4")
		{
			text += "Rep_SC_ItemsBarCode_A4(4-14).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "5")
		{
			text += "Rep_SC_ItemsBarCode_A4(5-13).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "6")
		{
			text += "Rep_SC_ItemsBarCode_A4(6-16).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "7")
		{
			text += "Rep_SC_ItemsBarCode_A4(6-24).rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "8")
		{
			text += "Rep_SC_ItemsBarCode_Roll.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "9")
		{
			text += "Rep_SC_ItemsBarCode_Roll2.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "10")
		{
			text += "Rep_SC_ItemsBarCode_Roll2_WithDiscount.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "11")
		{
			text += "Rep_SC_ItemsBarCode_Roll2.5-5.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "12")
		{
			text += "Rep_SC_ItemsBarCode_Roll2.5-5_WithPrice.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "13")
		{
			text += "Rep_SC_ItemsBarCode_Roll2.5-5_WithPrice_WithBrand.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "14")
		{
			text += "Rep_SC_ItemsBarCodeBoxWithBatchNo.rpt";
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "15")
		{
			text += "Rep_SC_ItemsBarCodeWithBatchNo.rpt";
		}
		return text;
	}

	public void ShowReport()
	{
		if (cboReportType.SelectedIndex > -1 && (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("PriceType") || dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NotesWithPrice")) && ((((UltraToggleEditorBase)chkPriceType).Checked && cboPriceType.SelectedIndex == -1) || (((UltraToggleEditorBase)chkPriceList).Checked && cboPriceList.SelectedIndex == -1)))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى سعر  ", "There is no chosen Price to be shown in the report, please Select Price to be shown in report");
			return;
		}
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Date") && dtpDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى تاريخ  ", "There is no chosen Date to be shown in the report, please Select Date to be shown in report");
			return;
		}
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Date"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@Date", dtpDate.DateTime);
		}
		if (cboReportType.SelectedIndex > -1 && (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("PriceType") || dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NotesWithPrice")))
		{
			if (((UltraToggleEditorBase)chkPriceList).Checked)
			{
				PriceListID = ((TextEditorControlBase)cboPriceList).Value.ToString();
			}
			else
			{
				PriceListID = "-1";
			}
			if (((UltraToggleEditorBase)chkPriceType).Checked)
			{
				PriceTypeID = ((TextEditorControlBase)cboPriceType).Value.ToString();
			}
			else
			{
				PriceTypeID = "-1";
			}
			GlobalVariables.ReportDocument.SetParameterValue("@PriceTypeID", PriceTypeID);
			GlobalVariables.ReportDocument.SetParameterValue("@PriceListID", PriceListID);
			GlobalVariables.ReportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
		}
		else if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NoPriceType"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@PriceTypeID", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@PriceListID", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		frmReporViwer2.ShowDialog();
		GlobalVariables.ReportDocument = null;
	}

	public bool Check_Gride_ForError(UltraGrid Val_Gride)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)Val_Gride).Rows).Count; i++)
		{
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)Val_Gride).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)Val_Gride).Rows[i].Cells["BatchID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				Val_Gride.ActiveCell = ((UltraGridBase)Val_Gride).Rows[i].Cells["BatchID"];
				Val_Gride.PerformAction((UltraGridAction)24);
				return false;
			}
			if (bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)Val_Gride).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString()) && ((UltraGridBase)Val_Gride).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار الوحدة", "Please choose Unit");
				Val_Gride.ActiveCell = ((UltraGridBase)Val_Gride).Rows[i].Cells["UnitID"];
				Val_Gride.PerformAction((UltraGridAction)24);
				return true;
			}
			if (Convert.ToInt32(((UltraGridBase)Val_Gride).Rows[i].Cells["Qty"].Value) == 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن ادخال قيم بـ صفر" : "Can Not Enter Zero Value");
				return true;
			}
			if (((UltraGridBase)Val_Gride).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء اختيار الصنف " : "Please Selected An Item");
				return true;
			}
		}
		return false;
	}

	private void frmGenerateBarCode_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void grdItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (((UltraGridBase)ULGItems).ActiveRow != null && e.KeyCode == Keys.F8)
		{
			((UltraGridBase)ULGItems).ActiveRow.Cells["ItemID"].Value = SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "0", IsFromServer: false);
		}
	}

	private void grdItems_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void txtDefultQuantity_ValueChanged(object sender, EventArgs e)
	{
		dtDetails.AcceptChanges();
		for (int i = 0; i < dtDetails.Rows.Count; i++)
		{
			dtDetails.Rows[i]["Qty"] = ((((Control)(object)txtDefultQuantity).Text == "") ? "1" : ((Control)(object)txtDefultQuantity).Text);
		}
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = ((((Control)(object)txtDefultQuantity).Text == "") ? "1" : ((Control)(object)txtDefultQuantity).Text);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSelectItems_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			DataRow dataRow = dtItems.Select(" ItemID = " + dtSearchResult.Rows[i]["ItemID"].ToString())[0];
			int num = int.Parse(dataRow["UnitID"].ToString());
			if (GlobalVariables.IsArabic)
			{
				dtDetails.Rows.Add(((Control)(object)txtNotes).Text, num, (((Control)(object)txtDefultQuantity).Text == "") ? "1" : ((Control)(object)txtDefultQuantity).Text, DBNull.Value, DBNull.Value, DBNull.Value, dtSearchResult.Rows[i]["ItemID"].ToString());
				((UltraGridBase)ULGItems).UpdateData();
				((UltraGridBase)ULGItems).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGItems).Rows).Count - 1].Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(int.Parse(dataRow["UnitTypeID"].ToString()));
			}
			else
			{
				dtDetails.Rows.Add(new object[5]
				{
					dtSearchResult.Rows[i]["ItemID"].ToString(),
					DBNull.Value,
					DBNull.Value,
					DBNull.Value,
					(((Control)(object)txtDefultQuantity).Text == "") ? "1" : ((Control)(object)txtDefultQuantity).Text
				}, num, ((Control)(object)txtNotes).Text);
				((UltraGridBase)ULGItems).UpdateData();
				((UltraGridBase)ULGItems).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGItems).Rows).Count - 1].Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(int.Parse(dataRow["UnitTypeID"].ToString()));
			}
		}
		CalcTotalQty();
	}

	private void ULGItems_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			e.Cell.Row.Cells["UnitID"].Value = int.Parse(dtItems.Select(" ItemID = " + dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString())[0]["UnitID"].ToString());
			e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(int.Parse(dtItems.Select(" ItemID = " + dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString())[0]["UnitTypeID"].ToString()));
			if (UsingBatchNoAndValidityPeriod)
			{
				e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()));
			}
		}
	}

	private ValueList getUnitsValueList(int UnitTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtUnits.Select("UnitTypeID=" + UnitTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["UnitID"].ToString(), array[i]["UnitName"].ToString());
		}
		return val;
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

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
		}
	}

	private void ULGItems_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGItems.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGItems).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGItems.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsUnitPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGItems).ActiveRow).Selected = true;
		}
	}

	private void ULGItems_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGItems.AfterExitEditMode -= ULGItems_AfterExitEditMode;
		CalcTotalQty();
		ULGItems.AfterExitEditMode += ULGItems_AfterExitEditMode;
	}

	private void ULGItems_AfterRowsDeleted(object sender, EventArgs e)
	{
		ULGItems.AfterRowsDeleted -= ULGItems_AfterRowsDeleted;
		CalcTotalQty();
		ULGItems.AfterRowsDeleted += ULGItems_AfterRowsDeleted;
	}

	private void txtNotes_ValueChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGItems).Rows).Count; i++)
		{
			((UltraGridBase)ULGItems).Rows[i].Cells["Notes"].Value = ((Control)(object)txtNotes).Text;
		}
		((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Notes"].DefaultCellValue = ((Control)(object)txtNotes).Text;
	}

	private void lblDate_Click(object sender, EventArgs e)
	{
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		frmUserReportName frmUserReportName2 = new frmUserReportName();
		frmUserReportName2.WindowState = FormWindowState.Normal;
		frmUserReportName2.ShowDialog();
		if (!frmUserReportName2.Cancel)
		{
			int num = BusinessLayer.Privilege.Reports.InsertNewUserDesign(((DataRow)base.Tag)["FormID"].ToString(), frmUserReportName2.ArName, frmUserReportName2.EnName, dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
			UsersReports.Insert_Update("-1", GlobalVariables.UserID, num.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
			frmUserReportName2.Dispose();
			string sourceFileName = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].ToString();
			string sourceFileName2 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"].ToString();
			string text = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_A.rpt";
			string text2 = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_E.rpt";
			File.Copy(sourceFileName, text);
			File.Copy(sourceFileName2, text2);
			Process.Start(text2);
			Process.Start(text);
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		}
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		string fileName = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].ToString();
		string fileName2 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"].ToString();
		Process.Start(fileName2);
		Process.Start(fileName);
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			UsersReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			BusinessLayer.Privilege.Reports.Delete(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"]);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"]);
			((TextEditorControlBase)cboReportType).Clear();
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (dtReports != null && dtReports.Rows.Count > 0 && cboReportType.SelectedIndex > -1)
		{
			((Control)(object)btnUpdate).Enabled = GlobalVariables.UserID == "1" || !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			((Control)(object)btnDelete).Enabled = !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			UltraCheckEditor obj = chkPriceType;
			UltraComboEditor obj2 = cboPriceType;
			UltraCheckEditor obj3 = chkPriceList;
			bool flag = (((Control)(object)cboPriceList).Visible = dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("PriceType") || dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NotesWithPrice"));
			bool flag3 = (((Control)(object)obj3).Visible = flag);
			bool visible = (((Control)(object)obj2).Visible = flag3);
			((Control)(object)obj).Visible = visible;
			UltraTextEditor obj4 = txtNotes;
			visible = (((Control)(object)lblNotes).Visible = dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NotesWithPrice"));
			((Control)(object)obj4).Visible = visible;
			UltraLabel obj5 = lblDate;
			visible = (((Control)(object)dtpDate).Visible = dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Date"));
			((Control)(object)obj5).Visible = visible;
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns).Count > 0)
			{
				((UltraGridBase)ULGItems).DisplayLayout.Bands[0].Columns["Notes"].Hidden = !dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("NotesWithPrice");
			}
		}
	}

	private void chkPriceList_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkPriceType).Checked = !((UltraToggleEditorBase)chkPriceList).Checked;
		((Control)(object)cboPriceList).Enabled = ((UltraToggleEditorBase)chkPriceList).Checked;
	}

	private void chkPriceType_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkPriceList).Checked = !((UltraToggleEditorBase)chkPriceType).Checked;
		((Control)(object)cboPriceType).Enabled = ((UltraToggleEditorBase)chkPriceType).Checked;
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGItems).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalQty).Text = "0";
		((UltraGridBase)ULGItems).UpdateData();
		DataTable dataTable = (DataTable)((UltraGridBase)ULGItems).DataSource;
		if (dataTable.Rows.Count > 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalQty).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
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
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_074f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Expected O, but got Unknown
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmGenerateBarCode));
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
		this.btnPrintBarCode = new UltraButton();
		this.lblDefultQuantity = new UltraLabel();
		this.txtDefultQuantity = new UltraTextEditor();
		this.ULGItems = new UltraGrid();
		this.chkIsArabic = new UltraCheckEditor();
		this.cboReportType = new UltraComboEditor();
		this.lblReportType = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSelectItems = new UltraButton();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnNew = new UltraButton();
		this.cboPriceType = new UltraComboEditor();
		this.cboPriceList = new UltraComboEditor();
		this.chkPriceList = new UltraCheckEditor();
		this.chkPriceType = new UltraCheckEditor();
		this.btnOpenTicket = new UltraButton();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalQty = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDefultQuantity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPriceList).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnPrintBarCode, "btnPrintBarCode");
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Name = "btnPrintBarCode";
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Click += new System.EventHandler(btnPrintBarCode_Click);
		resources.ApplyResources(this.lblDefultQuantity, "lblDefultQuantity");
		this.lblDefultQuantity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefultQuantity).Name = "lblDefultQuantity";
		((ControlBase)this.lblDefultQuantity).WrapText = false;
		resources.ApplyResources(this.txtDefultQuantity, "txtDefultQuantity");
		((System.Windows.Forms.Control)(object)this.txtDefultQuantity).Name = "txtDefultQuantity";
		((TextEditorControlBase)this.txtDefultQuantity).ValueChanged += new System.EventHandler(txtDefultQuantity_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDefultQuantity).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDefultQuantity_KeyPress);
		resources.ApplyResources(this.ULGItems, "ULGItems");
		((UltraGridBase)this.ULGItems).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGItems).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGItems).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGItems).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGItems).Name = "ULGItems";
		this.ULGItems.AfterEnterEditMode += new System.EventHandler(ULGItems_AfterEnterEditMode);
		this.ULGItems.AfterExitEditMode += new System.EventHandler(ULGItems_AfterExitEditMode);
		this.ULGItems.AfterRowsDeleted += new System.EventHandler(ULGItems_AfterRowsDeleted);
		this.ULGItems.CellListSelect += new CellEventHandler(ULGItems_CellListSelect);
		this.ULGItems.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(grdItems_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGItems).KeyDown += new System.Windows.Forms.KeyEventHandler(grdItems_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGItems).KeyPress += new System.Windows.Forms.KeyPressEventHandler(grdItems_KeyPress);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance15");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).Checked = true;
		((UltraToggleEditorBase)this.chkIsArabic).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		resources.ApplyResources(this.cboReportType, "cboReportType");
		this.cboReportType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboReportType).Name = "cboReportType";
		((TextEditorControlBase)this.cboReportType).Nullable = false;
		((TextEditorControlBase)this.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		resources.ApplyResources(this.lblReportType, "lblReportType");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance16");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val12;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val13, "appearance17");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val14).Image = resources.GetObject("appearance18.Image");
		resources.ApplyResources(val14, "appearance18");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val14;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSelectItems, "btnSelectItems");
		((System.Windows.Forms.Control)(object)this.btnSelectItems).Name = "btnSelectItems";
		((System.Windows.Forms.Control)(object)this.btnSelectItems).Click += new System.EventHandler(btnSelectItems_Click);
		resources.ApplyResources(this.btnUpdate, "btnUpdate");
		((System.Windows.Forms.Control)(object)this.btnUpdate).Name = "btnUpdate";
		((System.Windows.Forms.Control)(object)this.btnUpdate).Click += new System.EventHandler(btnUpdate_Click);
		resources.ApplyResources(this.btnDelete, "btnDelete");
		((System.Windows.Forms.Control)(object)this.btnDelete).Name = "btnDelete";
		((System.Windows.Forms.Control)(object)this.btnDelete).Click += new System.EventHandler(btnDelete_Click);
		resources.ApplyResources(this.btnNew, "btnNew");
		((System.Windows.Forms.Control)(object)this.btnNew).Name = "btnNew";
		((System.Windows.Forms.Control)(object)this.btnNew).Click += new System.EventHandler(btnNew_Click);
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.cboPriceList, "cboPriceList");
		((TextEditorControlBase)this.cboPriceList).AlwaysInEditMode = true;
		this.cboPriceList.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceList).Name = "cboPriceList";
		resources.ApplyResources(this.chkPriceList, "chkPriceList");
		((System.Windows.Forms.Control)(object)this.chkPriceList).Name = "chkPriceList";
		((UltraToggleEditorBase)this.chkPriceList).CheckedChanged += new System.EventHandler(chkPriceList_CheckedChanged);
		resources.ApplyResources(this.chkPriceType, "chkPriceType");
		((UltraToggleEditorBase)this.chkPriceType).Checked = true;
		((UltraToggleEditorBase)this.chkPriceType).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkPriceType).Name = "chkPriceType";
		((UltraToggleEditorBase)this.chkPriceType).CheckedChanged += new System.EventHandler(chkPriceType_CheckedChanged);
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtTotalQty).TabStop = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((TextEditorControlBase)this.txtNotes).ValueChanged += new System.EventHandler(txtNotes_ValueChanged);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Click += new System.EventHandler(lblDate_Click);
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkPriceList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceList);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDefultQuantity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDefultQuantity);
		base.Name = "frmGenerateBarCode";
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(frmGenerateBarCode_KeyDown);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDefultQuantity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDefultQuantity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkPriceList, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDefultQuantity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPriceList).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
