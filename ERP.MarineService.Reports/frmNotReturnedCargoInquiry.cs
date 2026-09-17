using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Reports;

public class frmNotReturnedCargoInquiry : frmBase
{
	private DataTable dtOperationsServicesCargos;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsReExport;

	private RadioButton rbIsReImport;

	public UltraGrid ULGDataCargo;

	public UltraButton btnSearch;

	private UltraLabel lblBaseNo;

	private UltraTextEditor txtContainerNo;

	public UltraButton btnSave;

	public UltraButton btnPrint;

	public frmNotReturnedCargoInquiry()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		FillGrid();
	}

	public void DisplayControls()
	{
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataCargo);
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCargo).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		if (rbIsReExport.Checked)
		{
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.05) - GlobalVariables.ScrollWidth;
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Op. No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية الفعليه" : "Loaded");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحمولة" : "Load Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاويه" : "Cont. Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consignee");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوبة" : "Container No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ShipperName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ShipperName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ShipperName"].Header).Caption = (GlobalVariables.IsArabic ? "المستورد" : "Export To");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البيان" : "ED No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "تم اعادته" : "Returned");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ العودة" : "Return Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن" : "Weight");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضـاعة" : "Goods Description");
		}
		else
		{
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1) - GlobalVariables.ScrollWidth;
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Op. No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية الفعليه" : "Loaded Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحمولة" : "Load Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاويه" : "Container Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوبة" : "Container No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاستلام" : "DO No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consignee");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositAmount"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositAmount"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة التأمين" : "Deposit Amount");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التأمين" : "Deposit Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExpectedReturnDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExpectedReturnDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExpectedReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الرجوع المتوقع" : "Expected Return Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "تم اعادته" : "Returned");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ العودة" : "Return Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن" : "Weight");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضـاعة" : "Goods Description");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البوليصة" : "Bill No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البوليصة" : "Bill Date");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void FillGrid()
	{
		dtOperationsServicesCargos = OperationsServicesCargos.SelectNotReturned(GlobalVariables.CurrentBranchID, rbIsReImport.Checked ? "1" : "0", rbIsReExport.Checked ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataCargo).DataSource = dtOperationsServicesCargos;
		InitGrid();
	}

	private void ULGDataCargo_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		if (!e.Row.Cells["DepositDate"].Value.Equals(DBNull.Value) && Convert.ToDateTime(e.Row.Cells["DepositDate"].Value) < DateTime.Now.AddDays(5.0))
		{
			((AppearanceBase)e.Row.Appearance).BackColor = Color.Red;
		}
	}

	private void rbIsReImport_CheckedChanged(object sender, EventArgs e)
	{
		if (rbIsReImport.Checked)
		{
			((Control)(object)btnPrint).Visible = true;
			FillGrid();
		}
	}

	private void rbIsReExport_CheckedChanged(object sender, EventArgs e)
	{
		if (rbIsReExport.Checked)
		{
			((Control)(object)btnPrint).Visible = false;
			FillGrid();
		}
	}

	private void ULGDataCargo_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "ExportDeclarationNo" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "OperationNo" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "VoyageNo" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "VesselName" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "LoadedQty" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "LoadTypeName" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "ContainerType" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "ConsigneeName" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "ShipperName" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "Notes" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "ContainerNo" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "GoodsDescription" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "UnitName" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "Weight" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "BillNo" || ((KeyedSubObjectBase)ULGDataCargo.ActiveCell.Column).Key == "BillDate")
		{
			((GridItemBase)ULGDataCargo.ActiveCell).Selected = true;
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		((UltraGridBase)ULGDataCargo).UpdateData();
		OperationsServicesCargos.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataCargo).DataSource, GlobalVariables.UserID);
		FillGrid();
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		if (rbIsReImport.Checked)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsServicesCargos_TemporaryCargo_A.rpt" : "Rep_MS_OperationsServicesCargos_TemporaryCargo_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchID", GlobalVariables.CurrentBranchID);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
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
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Reports.frmNotReturnedCargoInquiry));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsReExport = new System.Windows.Forms.RadioButton();
		this.rbIsReImport = new System.Windows.Forms.RadioButton();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGDataCargo = new UltraGrid();
		this.btnSearch = new UltraButton();
		this.lblBaseNo = new UltraLabel();
		this.txtContainerNo = new UltraTextEditor();
		this.btnSave = new UltraButton();
		this.btnPrint = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance9.FontData");
		resources.ApplyResources(val, "appearance9");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val;
		this.pnlCheckType.BorderStyle = (UIElementBorderStyle)1;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsReExport);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsReImport);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsReExport, "rbIsReExport");
		this.rbIsReExport.BackColor = System.Drawing.Color.Transparent;
		this.rbIsReExport.Name = "rbIsReExport";
		this.rbIsReExport.UseVisualStyleBackColor = false;
		this.rbIsReExport.CheckedChanged += new System.EventHandler(rbIsReExport_CheckedChanged);
		resources.ApplyResources(this.rbIsReImport, "rbIsReImport");
		this.rbIsReImport.BackColor = System.Drawing.Color.Transparent;
		this.rbIsReImport.Checked = true;
		this.rbIsReImport.Name = "rbIsReImport";
		this.rbIsReImport.TabStop = true;
		this.rbIsReImport.UseVisualStyleBackColor = false;
		this.rbIsReImport.CheckedChanged += new System.EventHandler(rbIsReImport_CheckedChanged);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance16");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance16.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance21.Image");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance21.FontData");
		resources.ApplyResources(val4, "appearance21");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance11");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance11.FontData");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGDataCargo, "ULGDataCargo");
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance1.FontData");
		resources.ApplyResources(val6, "appearance1");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance4.FontData");
		resources.ApplyResources(val7, "appearance4");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val7;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance13.FontData");
		resources.ApplyResources(val8, "appearance13");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance5.FontData");
		resources.ApplyResources(val9, "appearance5");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance15.FontData");
		resources.ApplyResources(val10, "appearance15");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance6.FontData");
		resources.ApplyResources(val11, "appearance6");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val12).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance17.FontData");
		resources.ApplyResources(val12, "appearance17");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val13).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance18.FontData");
		resources.ApplyResources(val13, "appearance18");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val14).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance19.FontData");
		resources.ApplyResources(val14, "appearance19");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(((AppearanceBase)val15).FontData, "appearance20.FontData");
		resources.ApplyResources(val15, "appearance20");
		((SubObjectBase)val15).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.ULGDataCargo).Name = "ULGDataCargo";
		this.ULGDataCargo.InitializeRow += new InitializeRowEventHandler(ULGDataCargo_InitializeRow);
		this.ULGDataCargo.AfterEnterEditMode += new System.EventHandler(ULGDataCargo_AfterEnterEditMode);
		((AppearanceBase)val16).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(((AppearanceBase)val16).FontData, "appearance7.FontData");
		resources.ApplyResources(val16, "appearance7");
		((SubObjectBase)val16).ForceApplyResources = "FontData|";
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val16;
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		resources.ApplyResources(this.lblBaseNo, "lblBaseNo");
		((System.Windows.Forms.Control)(object)this.lblBaseNo).Name = "lblBaseNo";
		resources.ApplyResources(this.txtContainerNo, "txtContainerNo");
		((System.Windows.Forms.Control)(object)this.txtContainerNo).Name = "txtContainerNo";
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.Save;
		resources.ApplyResources(((AppearanceBase)val17).FontData, "appearance8.FontData");
		resources.ApplyResources(val17, "appearance8");
		((SubObjectBase)val17).ForceApplyResources = "FontData|";
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnPrint, "btnPrint");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.Print;
		resources.ApplyResources(((AppearanceBase)val18).FontData, "appearance2.FontData");
		resources.ApplyResources(val18, "appearance2");
		((SubObjectBase)val18).ForceApplyResources = "FontData|";
		((ControlBase)this.btnPrint).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnPrint).Name = "btnPrint";
		((System.Windows.Forms.Control)(object)this.btnPrint).Click += new System.EventHandler(btnPrint_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrint);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBaseNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataCargo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmNotReturnedCargoInquiry";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGDataCargo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBaseNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrint, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
