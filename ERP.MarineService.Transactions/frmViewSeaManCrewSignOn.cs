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
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmViewSeaManCrewSignOn : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtServices;

	private DataTable dtSignOn;

	private ValueList vlPassengerTypesOn = new ValueList();

	private ValueList vlPassengerTypesOff = new ValueList();

	private ValueList vlUsers = new ValueList();

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGSignOn;

	private UltraLabel lblSignOnCounterResult;

	private UltraLabel lblSignOnCounter;

	public UltraButton btnPrintCrewCount;

	public UltraButton btnRefreshData;

	public frmViewSeaManCrewSignOn()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewSeaManCrewSignOn(bool _IsSuperVisor, bool _CanUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CanUpdate;
		TableName = "MS_OperationsServicesCrewPassengers";
	}

	private void frmViewSeaManCrew_Load(object sender, EventArgs e)
	{
		PrepareData();
		FillGrid();
		InitGrid();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPassengerTypesOn.ValueListItems.Clear();
		vlPassengerTypesOn.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "بحار" : "Seaman");
		vlPassengerTypesOn.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "راكب" : "Passenger");
		vlPassengerTypesOn.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "فني" : "Technician");
		vlPassengerTypesOff.ValueListItems.Clear();
		vlPassengerTypesOff.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "بحار" : "Seaman");
		vlPassengerTypesOff.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "راكب" : "Passenger");
		vlPassengerTypesOff.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "فني" : "Technician");
	}

	public void FillGrid()
	{
		dtSignOn = OperationsServicesCrewPassengers.TrakingSignOn(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGSignOn).DataSource = dtSignOn;
		((Control)(object)lblSignOnCounterResult).Text = ((UltraGridBase)ULGSignOn).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGSignOn);
		((UltraGridBase)ULGSignOn).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationServiceCrewPassengerID"].DefaultCellValue = -1;
		((UltraGridBase)ULGSignOn).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGSignOn).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البحار" : "SeaMan");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.08);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No.");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No.");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.06) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخدمة" : "Service Date");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassengerType"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassengerType"].Hidden = false;
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassengerType"].ValueList = (IValueList)(object)vlPassengerTypesOn;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["PassengerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الراكب" : "Passenger Type");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Header).Caption = (GlobalVariables.IsArabic ? "ختم دخول" : "Enter Stamp");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["EnterStampDate"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["EnterStampDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["EnterStampDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ ختم دخول" : "Enter Stamp Date");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsExit"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsExit"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsExit"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "Exit");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["ExitDate"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["ExitDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["ExitDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Exit Date");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsJoined"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsJoined"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsJoined"].Header).Caption = (GlobalVariables.IsArabic ? "الحاق" : "Joined");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["JoinedDate"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["JoinedDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["JoinedDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الحاق" : "Joined Date");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].Header).Caption = (GlobalVariables.IsArabic ? "تم التحديث" : "Updated");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["UnitedUpdateDate"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["UnitedUpdateDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["UnitedUpdateDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التحديث على النظام" : "Updated Date");
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGSignOn).Width * 0.05);
		((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOn).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
	}

	private void ULGSignOn_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGSignOn.ActiveCell).Selected = true;
	}

	private void ULGSignOn_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGSignOn).ActiveRow != null)
		{
			frmUpdateSeaManCrew frmUpdateSeaManCrew2 = new frmUpdateSeaManCrew(((UltraGridBase)ULGSignOn).ActiveRow.Cells["OperationServiceCrewPassengerID"].Value.ToString(), IsSignOn: true);
			frmUpdateSeaManCrew2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateSeaManCrew2.lblTitle).Text = (GlobalVariables.IsArabic ? " الحاق" : "SignOn");
			frmUpdateSeaManCrew2.ShowDialog();
			FillGrid();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnPrintCrewCount_Click(object sender, EventArgs e)
	{
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_MS_Operations_CurrentCrewCount_A.rpt" : "Rep_MS_Operations_CurrentCrewCount_E.rpt"));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGSignOn_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblSignOnCounterResult).Text = ((UltraGridBase)ULGSignOn).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewSeaManCrewSignOn));
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
		this.ULGSignOn = new UltraGrid();
		this.lblSignOnCounterResult = new UltraLabel();
		this.lblSignOnCounter = new UltraLabel();
		this.btnPrintCrewCount = new UltraButton();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGSignOn).BeginInit();
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
		resources.ApplyResources(this.ULGSignOn, "ULGSignOn");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGSignOn).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGSignOn).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGSignOn).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGSignOn).Name = "ULGSignOn";
		this.ULGSignOn.AfterEnterEditMode += new System.EventHandler(ULGSignOn_AfterEnterEditMode);
		((UltraGridBase)this.ULGSignOn).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGSignOn_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGSignOn).DoubleClick += new System.EventHandler(ULGSignOn_DoubleClick);
		resources.ApplyResources(this.lblSignOnCounterResult, "lblSignOnCounterResult");
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblSignOnCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblSignOnCounterResult).Name = "lblSignOnCounterResult";
		resources.ApplyResources(this.lblSignOnCounter, "lblSignOnCounter");
		this.lblSignOnCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSignOnCounter).Name = "lblSignOnCounter";
		((ControlBase)this.lblSignOnCounter).WrapText = false;
		resources.ApplyResources(this.btnPrintCrewCount, "btnPrintCrewCount");
		((AppearanceBase)val16).Image = resources.GetObject("appearance16.Image");
		((ControlBase)this.btnPrintCrewCount).Appearance = (AppearanceBase)(object)val16;
		((ControlBase)this.btnPrintCrewCount).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnPrintCrewCount).Name = "btnPrintCrewCount";
		((System.Windows.Forms.Control)(object)this.btnPrintCrewCount).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPrintCrewCount).Click += new System.EventHandler(btnPrintCrewCount_Click);
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintCrewCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSignOnCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSignOnCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGSignOn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewSeaManCrewSignOn";
		base.Load += new System.EventHandler(frmViewSeaManCrew_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGSignOn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSignOnCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSignOnCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintCrewCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGSignOn).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
