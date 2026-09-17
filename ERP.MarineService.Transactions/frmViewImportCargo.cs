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

public class frmViewImportCargo : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtServices;

	private DataTable dtImport;

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGImport;

	private UltraLabel lblImportCounterResult;

	private UltraLabel lblImportCounter;

	public UltraButton btnPrintImportCargoRep;

	public UltraButton btnRefreshData;

	public frmViewImportCargo()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewImportCargo(bool _IsSuperVisor, bool _CanUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CanUpdate;
		TableName = "MS_OperationsServicesCargos";
	}

	private void frmViewImportCargo_Load(object sender, EventArgs e)
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
		GlobalFunctions.PrepareGrid(ULGImport);
		((UltraGridBase)ULGImport).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGImport).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGImport).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGImport).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGImport).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGImport).DisplayLayout.UseFixedHeaders = true;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VesselName"].Header).Fixed = true;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VesselName"].Header).FixOnRight = (DefaultableBoolean)(GlobalVariables.IsArabic ? 1 : 2);
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Operation No.");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No.");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.1);
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخدمة" : "Service Date");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["LoadedQty"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["LoadedQty"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["LoadedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية الفعليه" : "Loaded Qty");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["LoadTypeName"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["LoadTypeName"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["LoadTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحمولة" : "Load Type");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ContainerType"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ContainerType"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ContainerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الحاويه" : "Container Type");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوبة" : "Container No.");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["CustomFees"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["CustomFees"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["CustomFees"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الجمارك" : "Customs Fees");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["IsReImport"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["IsReImport"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["IsReImport"].Header).Caption = (GlobalVariables.IsArabic ? "إعاده تصديره" : "Re Import");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاستلام" : "DO No.");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["InTallySheet"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["InTallySheet"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["InTallySheet"].Header).Caption = (GlobalVariables.IsArabic ? "متضمنة في البيان" : "In Tally Sheet");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consignee");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DepositAmount"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DepositAmount"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DepositAmount"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة التأمين" : "Deposit Amount");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DepositDate"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DepositDate"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DepositDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التأمين" : "Deposit Date");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ExpectedReturnDate"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ExpectedReturnDate"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ExpectedReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الرجوع المتوقع" : "Expected Return Date");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["IsReturned"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["IsReturned"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["IsReturned"].Header).Caption = (GlobalVariables.IsArabic ? "تم اعادته" : "Returned");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ العودة" : "Return Date");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Weight"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Weight"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["Weight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن" : "Weight");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضـاعة" : "Goods Description");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["BillNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["BillNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["BillNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البوليصة" : "BL No.");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["BillDate"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["BillDate"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["BillDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البوليصة" : "BL Date");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ShipperInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ShipperInvoiceNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["ShipperInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفاتورة" : "Inv.No");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DeliveryNo"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DeliveryNo"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DeliveryNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم اذن التسليم" : "Delivery No.");
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DeliveryDate"].Hidden = false;
		((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DeliveryDate"].Width = (int)((double)((Control)(object)ULGImport).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGImport).DisplayLayout.Bands[0].Columns["DeliveryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ اذن التسليم" : "Delivery Date");
	}

	public void FillGrid()
	{
		dtImport = OperationsServicesCargos.Tracking(GlobalVariables.CurrentBranchID, "-1", "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGImport).DataSource = dtImport;
		((Control)(object)lblImportCounterResult).Text = ((UltraGridBase)ULGImport).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGImport_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGImport.ActiveCell).Selected = true;
	}

	private void ULGImport_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGImport).ActiveRow != null)
		{
			frmUpdateImportCargo frmUpdateImportCargo2 = new frmUpdateImportCargo(((UltraGridBase)ULGImport).ActiveRow.Cells["OperationServiceCargoID"].Value.ToString());
			frmUpdateImportCargo2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateImportCargo2.lblTitle).Text = (GlobalVariables.IsArabic ? "وارد" : "Import");
			frmUpdateImportCargo2.ShowDialog();
			FillGrid();
		}
	}

	private void btnPrintImportCargoRep_Click(object sender, EventArgs e)
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGImport).Rows).Count; i++)
		{
			text = text + ((UltraGridBase)ULGImport).Rows[i].Cells["OperationID"].Value.ToString() + ",";
		}
		if (text == ",")
		{
			text = "-1";
		}
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_OperationsServicesCargos_ImportDetails_A.rpt" : "Rep_MS_OperationsServicesCargos_ImportDetails_E.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsExportDeclaration", "0");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGImport_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblImportCounterResult).Text = ((UltraGridBase)ULGImport).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewImportCargo));
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
		this.ULGImport = new UltraGrid();
		this.lblImportCounterResult = new UltraLabel();
		this.lblImportCounter = new UltraLabel();
		this.btnPrintImportCargoRep = new UltraButton();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGImport).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGImport, "ULGImport");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGImport).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGImport).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGImport).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGImport).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGImport).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGImport).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGImport).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGImport).Name = "ULGImport";
		this.ULGImport.AfterEnterEditMode += new System.EventHandler(ULGImport_AfterEnterEditMode);
		((UltraGridBase)this.ULGImport).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGImport_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGImport).DoubleClick += new System.EventHandler(ULGImport_DoubleClick);
		resources.ApplyResources(this.lblImportCounterResult, "lblImportCounterResult");
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblImportCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblImportCounterResult).Name = "lblImportCounterResult";
		resources.ApplyResources(this.lblImportCounter, "lblImportCounter");
		this.lblImportCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblImportCounter).Name = "lblImportCounter";
		((ControlBase)this.lblImportCounter).WrapText = false;
		resources.ApplyResources(this.btnPrintImportCargoRep, "btnPrintImportCargoRep");
		((AppearanceBase)val16).Image = resources.GetObject("appearance16.Image");
		((ControlBase)this.btnPrintImportCargoRep).Appearance = (AppearanceBase)(object)val16;
		((ControlBase)this.btnPrintImportCargoRep).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnPrintImportCargoRep).Name = "btnPrintImportCargoRep";
		((System.Windows.Forms.Control)(object)this.btnPrintImportCargoRep).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPrintImportCargoRep).Click += new System.EventHandler(btnPrintImportCargoRep_Click);
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintImportCargoRep);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblImportCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblImportCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGImport);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewImportCargo";
		base.Load += new System.EventHandler(frmViewImportCargo_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblImportCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblImportCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintImportCargoRep, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGImport).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
