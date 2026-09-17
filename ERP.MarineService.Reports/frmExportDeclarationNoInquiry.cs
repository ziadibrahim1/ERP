using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Reports;

public class frmExportDeclarationNoInquiry : frmBase
{
	private DataTable dtOperationsServicesCargos;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtDeclarationNo;

	private UltraLabel lblCustomDeclarationNo;

	private UltraTextEditor txtContainerNo;

	private UltraLabel lblBaseNo;

	public UltraButton btnSearch;

	private UltraPanel pnlCheckType;

	private RadioButton rbIsExport;

	private RadioButton rbIsImport;

	public UltraGrid ULGDataCargo;

	public frmExportDeclarationNoInquiry()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataCargo);
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		if (rbIsExport.Checked)
		{
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.05) - GlobalVariables.ScrollWidth;
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Op. No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية الفعليه" : "Loaded Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحمولة" : "Load Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاويه" : "Container Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReExport"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReExport"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReExport"].Header).Caption = (GlobalVariables.IsArabic ? "إعادة إستيراده" : "Re Export");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consignee");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوبة" : "Container No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ShipperName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ShipperName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ShipperName"].Header).Caption = (GlobalVariables.IsArabic ? "المستورد" : "Export To");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البيان" : "ED No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "تم اعادته" : "Returned");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ العودة" : "Return Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن" : "Weight");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضـاعة" : "Goods Description");
		}
		else
		{
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.05) - GlobalVariables.ScrollWidth;
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Operation No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية الفعليه" : "Loaded Qty");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["LoadTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحمولة" : "Load Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاويه" : "Container Type");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوبة" : "Container No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["CustomFees"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["CustomFees"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["CustomFees"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الجمارك" : "Customs Fees");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReImport"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReImport"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReImport"].Header).Caption = (GlobalVariables.IsArabic ? "إعادة تصديره" : "Re Import");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.07);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاستلام" : "DO No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consignee");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositAmount"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositAmount"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة التأمين" : "Deposit Amount");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["DepositDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التأمين" : "Deposit Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "تم اعادته" : "Returned");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ العودة" : "Return Date");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن" : "Weight");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.2);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضـاعة" : "Goods Description");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillNo"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
			((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البوليصة" : "Bill No.");
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillDate"].Hidden = false;
			((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["BillDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.08);
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

	private void btnSearch_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtContainerNo).Text.Trim().Equals("") && ((Control)(object)txtDeclarationNo).Text.Trim().Equals(""))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال رقم الحاويه او البيان للبحث" : "Please Enter a Valid No for Container or Declaration");
			return;
		}
		dtOperationsServicesCargos = OperationsServicesCargos.SelectByExportDeclarationNo(GlobalVariables.CurrentBranchID, ((Control)(object)txtDeclarationNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtDeclarationNo).Text.Trim(), ((Control)(object)txtContainerNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtContainerNo).Text.Trim(), rbIsExport.Checked ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataCargo).DataSource = dtOperationsServicesCargos;
		InitGrid();
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Reports.frmExportDeclarationNoInquiry));
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
		this.pnlCheckType = new UltraPanel();
		this.rbIsExport = new System.Windows.Forms.RadioButton();
		this.rbIsImport = new System.Windows.Forms.RadioButton();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtDeclarationNo = new UltraTextEditor();
		this.lblCustomDeclarationNo = new UltraLabel();
		this.txtContainerNo = new UltraTextEditor();
		this.lblBaseNo = new UltraLabel();
		this.btnSearch = new UltraButton();
		this.ULGDataCargo = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtDeclarationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance9");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance9.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val;
		this.pnlCheckType.BorderStyle = (UIElementBorderStyle)1;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsExport);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbIsImport);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbIsExport, "rbIsExport");
		this.rbIsExport.BackColor = System.Drawing.Color.Transparent;
		this.rbIsExport.Name = "rbIsExport";
		this.rbIsExport.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbIsImport, "rbIsImport");
		this.rbIsImport.BackColor = System.Drawing.Color.Transparent;
		this.rbIsImport.Checked = true;
		this.rbIsImport.Name = "rbIsImport";
		this.rbIsImport.TabStop = true;
		this.rbIsImport.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val2, "appearance3");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
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
		resources.ApplyResources(val4, "appearance21");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance21.FontData");
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
		resources.ApplyResources(this.txtDeclarationNo, "txtDeclarationNo");
		((System.Windows.Forms.Control)(object)this.txtDeclarationNo).Name = "txtDeclarationNo";
		resources.ApplyResources(this.lblCustomDeclarationNo, "lblCustomDeclarationNo");
		((System.Windows.Forms.Control)(object)this.lblCustomDeclarationNo).Name = "lblCustomDeclarationNo";
		resources.ApplyResources(this.txtContainerNo, "txtContainerNo");
		((System.Windows.Forms.Control)(object)this.txtContainerNo).Name = "txtContainerNo";
		resources.ApplyResources(this.lblBaseNo, "lblBaseNo");
		((System.Windows.Forms.Control)(object)this.lblBaseNo).Name = "lblBaseNo";
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val6).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val6, "appearance2");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance2.FontData");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.ULGDataCargo, "ULGDataCargo");
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val7, "appearance1");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance1.FontData");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val8, "appearance4");
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance4.FontData");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val8;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val9, "appearance13");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance13.FontData");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val10, "appearance5");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance5.FontData");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val11, "appearance15");
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance15.FontData");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance6");
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance6.FontData");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance17");
		((AppearanceBase)val13).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance17.FontData");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val14).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val14, "appearance18");
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance18.FontData");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val15).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val15, "appearance19");
		resources.ApplyResources(((AppearanceBase)val15).FontData, "appearance19.FontData");
		((SubObjectBase)val15).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val16, "appearance20");
		resources.ApplyResources(((AppearanceBase)val16).FontData, "appearance20.FontData");
		((SubObjectBase)val16).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.ULGDataCargo).Name = "ULGDataCargo";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataCargo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBaseNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeclarationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomDeclarationNo);
		base.Name = "frmExportDeclarationNoInquiry";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomDeclarationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeclarationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBaseNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGDataCargo, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtDeclarationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
