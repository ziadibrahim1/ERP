using System;
using System.ComponentModel;
using System.Data;
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
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmViewExportCargo : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtServices;

	private DataTable dtExport;

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGExport;

	private UltraLabel lblExportCounterResult;

	private UltraLabel lblExportCounter;

	public UltraButton btnPrintExportCargo;

	public UltraButton btnRefreshData;

	public frmViewExportCargo()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewExportCargo(bool _IsSuperVisor, bool _CanUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CanUpdate;
		TableName = "MS_OperationsServicesCargos";
	}

	private void frmViewExportCargo_Load(object sender, EventArgs e)
	{
		FillGrid();
		InitGrid();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGExport);
		((UltraGridBase)ULGExport).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGExport).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGExport).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGExport).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGExport).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGExport).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VesselName"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VesselName"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Operation No.");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.05);
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No.");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.09);
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخدمة" : "Service Date");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["LoadedQty"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["LoadedQty"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["LoadedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية الفعليه" : "Loaded Qty");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["LoadTypeName"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["LoadTypeName"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["LoadTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحمولة" : "Load Type");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ContainerType"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ContainerType"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ContainerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاويه" : "Container Type");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["IsReExport"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["IsReExport"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["IsReExport"].Header).Caption = (GlobalVariables.IsArabic ? "أعاده إستيراده" : "Re Export");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consignee");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوبة" : "Container No.");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ShipperName"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ShipperName"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ShipperName"].Header).Caption = (GlobalVariables.IsArabic ? "المستورد" : "Export To");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البيان" : "ED No.");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["IsReturned"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["IsReturned"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "تم اعادته" : "Returned");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ العودة" : "Return Date");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن" : "Weight");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضـاعة" : "Goods Description");
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ShipperInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ShipperInvoiceNo"].Width = (int)((double)((Control)(object)ULGExport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGExport).DisplayLayout.Bands[0].Columns["ShipperInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفاتورة" : "Inv.No");
	}

	public void FillGrid()
	{
		dtExport = OperationsServicesCargos.Tracking(GlobalVariables.CurrentBranchID, "-1", "1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGExport).DataSource = dtExport;
		((Control)(object)lblExportCounterResult).Text = ((UltraGridBase)ULGExport).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGExport_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGExport.ActiveCell).Selected = true;
	}

	private void ULGExport_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGExport).ActiveRow != null)
		{
			frmUpdateExportCargo frmUpdateExportCargo2 = new frmUpdateExportCargo(((UltraGridBase)ULGExport).ActiveRow.Cells["OperationServiceCargoID"].Value.ToString());
			frmUpdateExportCargo2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateExportCargo2.lblTitle).Text = (GlobalVariables.IsArabic ? "صادر" : "Export");
			frmUpdateExportCargo2.ShowDialog();
			FillGrid();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnPrintExportCargo_Click(object sender, EventArgs e)
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGExport).Rows).Count; i++)
		{
			text = text + ((UltraGridBase)ULGExport).Rows[i].Cells["OperationID"].Value.ToString() + ",";
		}
		if (text == ",")
		{
			text = "-1";
		}
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsServicesCargos_ExportDetails_A.rpt" : "Rep_MS_OperationsServicesCargos_ExportDetails_E.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsExportDeclaration", "1");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGExport_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblExportCounterResult).Text = ((UltraGridBase)ULGExport).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
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
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewExportCargo));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGExport = new UltraGrid();
		this.lblExportCounterResult = new UltraLabel();
		this.lblExportCounter = new UltraLabel();
		this.btnPrintExportCargo = new UltraButton();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGExport).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance17");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance18");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val3, "appearance19");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance20");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGExport, "ULGExport");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGExport).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGExport).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGExport).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGExport).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGExport).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGExport).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGExport).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGExport).Name = "ULGExport";
		this.ULGExport.AfterEnterEditMode += new System.EventHandler(ULGExport_AfterEnterEditMode);
		((UltraGridBase)this.ULGExport).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGExport_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGExport).DoubleClick += new System.EventHandler(ULGExport_DoubleClick);
		resources.ApplyResources(this.lblExportCounterResult, "lblExportCounterResult");
		resources.ApplyResources(val15, "appearance21");
		((ControlBase)this.lblExportCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblExportCounterResult).Name = "lblExportCounterResult";
		resources.ApplyResources(this.lblExportCounter, "lblExportCounter");
		this.lblExportCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExportCounter).Name = "lblExportCounter";
		((ControlBase)this.lblExportCounter).WrapText = false;
		resources.ApplyResources(this.btnPrintExportCargo, "btnPrintExportCargo");
		((AppearanceBase)val16).Image = resources.GetObject("appearance22.Image");
		resources.ApplyResources(val16, "appearance22");
		((ControlBase)this.btnPrintExportCargo).Appearance = (AppearanceBase)(object)val16;
		((ControlBase)this.btnPrintExportCargo).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnPrintExportCargo).Name = "btnPrintExportCargo";
		((System.Windows.Forms.Control)(object)this.btnPrintExportCargo).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPrintExportCargo).Click += new System.EventHandler(btnPrintExportCargo_Click);
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintExportCargo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExportCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExportCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGExport);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewExportCargo";
		base.Load += new System.EventHandler(frmViewExportCargo_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExportCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExportCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintExportCargo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGExport).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
