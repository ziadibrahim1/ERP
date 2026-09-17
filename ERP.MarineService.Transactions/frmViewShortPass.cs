using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmViewShortPass : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtServices;

	private DataTable dtShortPass;

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGShortPass;

	private UltraLabel lblShortPassCounterResult;

	private UltraLabel ultraLabel1;

	public UltraButton btnRefreshData;

	public frmViewShortPass()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewShortPass(bool _IsSuperVisor, bool _CanUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CanUpdate;
		TableName = "MS_OperationsServicesShortPass";
	}

	private void frmViewShortPass_Load(object sender, EventArgs e)
	{
		FillGrid();
		InitGrid();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtServices = Services.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	public void FillGrid()
	{
		if (IsSuperVisor)
		{
			dtShortPass = OperationsServicesShortPass.Tracking(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtShortPass = OperationsServicesShortPass.Tracking(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGShortPass).DataSource = dtShortPass;
		((Control)(object)lblShortPassCounterResult).Text = ((UltraGridBase)ULGShortPass).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGShortPass);
		((UltraGridBase)ULGShortPass).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGShortPass).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGShortPass).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)ULGShortPass).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGShortPass).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.1);
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.1);
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.08);
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["SubAccount"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "Sea Man");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["SubAccount"].Hidden = false;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["SubAccount"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.17);
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["PassReason"].Header).Caption = (GlobalVariables.IsArabic ? "السبب" : "Reason");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["PassReason"].Hidden = false;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["PassReason"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["EntryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الدخول" : "Entry Date");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["EntryDate"].Hidden = false;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["EntryDate"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["ExitDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Exit Date");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["ExitDate"].Hidden = false;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["ExitDate"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["Remarks"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Remarks");
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["Remarks"].Hidden = false;
		((UltraGridBase)ULGShortPass).DisplayLayout.Bands[0].Columns["Remarks"].Width = (int)((double)((Control)(object)ULGShortPass).Width * 0.15);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGShortPass_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGShortPass).ActiveRow != null)
		{
			frmUpdateShortPass frmUpdateShortPass2 = new frmUpdateShortPass(((UltraGridBase)ULGShortPass).ActiveRow.Cells["OperationServiceShortPassID"].Value.ToString());
			frmUpdateShortPass2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdateShortPass2.lblTitle).Text = (GlobalVariables.IsArabic ? " تصريح مؤقت" : "Short Pass");
			frmUpdateShortPass2.ShowDialog();
			FillGrid();
		}
	}

	private void ULGShortPass_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGShortPass.ActiveCell).Selected = true;
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGShortPass_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblShortPassCounterResult).Text = ((UltraGridBase)ULGShortPass).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewShortPass));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGShortPass = new UltraGrid();
		this.lblShortPassCounterResult = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGShortPass).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance15");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance16");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance17.Image");
		resources.ApplyResources(val3, "appearance17");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val4).Image = resources.GetObject("appearance18.Image");
		resources.ApplyResources(val4, "appearance18");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGShortPass, "ULGShortPass");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGShortPass).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGShortPass).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGShortPass).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGShortPass).Name = "ULGShortPass";
		this.ULGShortPass.AfterEnterEditMode += new System.EventHandler(ULGShortPass_AfterEnterEditMode);
		((UltraGridBase)this.ULGShortPass).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGShortPass_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGShortPass).DoubleClick += new System.EventHandler(ULGShortPass_DoubleClick);
		resources.ApplyResources(this.lblShortPassCounterResult, "lblShortPassCounterResult");
		((System.Windows.Forms.Control)(object)this.lblShortPassCounterResult).Name = "lblShortPassCounterResult";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShortPassCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGShortPass);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewShortPass";
		base.Load += new System.EventHandler(frmViewShortPass_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGShortPass, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShortPassCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGShortPass).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
