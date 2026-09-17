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

public class frmViewSeaManCrewSignOff : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtServices;

	private DataTable dtSignOff;

	private ValueList vlPassengerTypesOn = new ValueList();

	private ValueList vlPassengerTypesOff = new ValueList();

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGSignOff;

	private UltraLabel lblSignOffCounterResult;

	private UltraLabel lblSignOffCounter;

	public UltraButton btnPrintCrewCount;

	public UltraButton btnRefreshData;

	public frmViewSeaManCrewSignOff()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewSeaManCrewSignOff(bool _IsSuperVisor, bool _CanUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CanUpdate;
		TableName = "MS_OperationsServicesCrewPassengers";
	}

	private void frmViewSeaManCrew_Load(object sender, EventArgs e)
	{
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

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGSignOff);
		((UltraGridBase)ULGSignOff).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGSignOff).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGSignOff).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.07);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.06);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.08);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["ServiceStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخدمة" : "Service Date");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassengerType"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassengerType"].Hidden = false;
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassengerType"].ValueList = (IValueList)(object)vlPassengerTypesOff;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassengerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الراكب" : "Passenger Type");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البحار" : "SeaMan");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.08);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No.");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No.");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["HasEyesScan"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["HasEyesScan"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["HasEyesScan"].Header).Caption = (GlobalVariables.IsArabic ? "بصمة عين" : "Eyes Scan");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.07);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Header).Caption = (GlobalVariables.IsArabic ? "ختم دخول" : "Enter Stamp");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["EnterStampDate"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.08);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["EnterStampDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["EnterStampDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ ختم دخول" : "Enter Stamp Date");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["IsExit"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["IsExit"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["IsExit"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "Exit");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["ExitDate"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.05);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["ExitDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["ExitDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Exit Date");
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGSignOff).Width * 0.07);
		((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSignOff).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
	}

	public void FillGrid()
	{
		dtSignOff = OperationsServicesCrewPassengers.TrakingSignOff(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGSignOff).DataSource = dtSignOff;
		((Control)(object)lblSignOffCounterResult).Text = ((UltraGridBase)ULGSignOff).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGSignOff_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGSignOff.ActiveCell).Selected = true;
	}

	private void ULGSignOff_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGSignOff).ActiveRow != null)
		{
			frmUpdateSeaManCrew frmUpdateSeaManCrew2 = new frmUpdateSeaManCrew(((UltraGridBase)ULGSignOff).ActiveRow.Cells["OperationServiceCrewPassengerID"].Value.ToString(), IsSignOn: false);
			frmUpdateSeaManCrew2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateSeaManCrew2.lblTitle).Text = (GlobalVariables.IsArabic ? " انزال" : "SignOff");
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

	private void ULGSignOff_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblSignOffCounterResult).Text = ((UltraGridBase)ULGSignOff).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewSeaManCrewSignOff));
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
		this.ULGSignOff = new UltraGrid();
		this.lblSignOffCounterResult = new UltraLabel();
		this.lblSignOffCounter = new UltraLabel();
		this.btnPrintCrewCount = new UltraButton();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGSignOff).BeginInit();
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
		resources.ApplyResources(this.ULGSignOff, "ULGSignOff");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGSignOff).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGSignOff).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGSignOff).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGSignOff).Name = "ULGSignOff";
		this.ULGSignOff.AfterEnterEditMode += new System.EventHandler(ULGSignOff_AfterEnterEditMode);
		((UltraGridBase)this.ULGSignOff).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGSignOff_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGSignOff).DoubleClick += new System.EventHandler(ULGSignOff_DoubleClick);
		resources.ApplyResources(this.lblSignOffCounterResult, "lblSignOffCounterResult");
		resources.ApplyResources(val15, "appearance21");
		((ControlBase)this.lblSignOffCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblSignOffCounterResult).Name = "lblSignOffCounterResult";
		resources.ApplyResources(this.lblSignOffCounter, "lblSignOffCounter");
		this.lblSignOffCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSignOffCounter).Name = "lblSignOffCounter";
		((ControlBase)this.lblSignOffCounter).WrapText = false;
		resources.ApplyResources(this.btnPrintCrewCount, "btnPrintCrewCount");
		((AppearanceBase)val16).Image = resources.GetObject("appearance22.Image");
		resources.ApplyResources(val16, "appearance22");
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSignOffCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSignOffCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGSignOff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewSeaManCrewSignOff";
		base.Load += new System.EventHandler(frmViewSeaManCrew_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGSignOff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSignOffCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSignOffCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintCrewCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGSignOff).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
