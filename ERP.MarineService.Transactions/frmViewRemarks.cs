using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmViewRemarks : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtUsers;

	private DataTable dtServices;

	private DataTable dtRemarks;

	private ValueList vlUsers = new ValueList();

	private ValueList vlUsers1 = new ValueList();

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGRemarks;

	private UltraLabel lblRemarksCounterResult;

	private UltraLabel lblRemarksCounter;

	public UltraButton btnNewRemark;

	public UltraButton btnRefreshData;

	public frmViewRemarks()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewRemarks(bool _IsSuperVisor, bool _CanUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CanUpdate;
		TableName = "MS_OperationsRemarks";
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
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers.ValueListItems.Clear();
		vlUsers1.ValueListItems.Clear();
		for (int i = 0; i < dtUsers.Rows.Count; i++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i]["UserName"].ToString());
			vlUsers1.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i]["UserName"].ToString());
		}
	}

	public void InitGrid()
	{
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGRemarks);
		((UltraGridBase)ULGRemarks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationRemarkID"].DefaultCellValue = -1;
		((UltraGridBase)ULGRemarks).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGRemarks).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.07);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.05);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].DefaultCellValue = DateTime.Now;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.2);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Remarks");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.07);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "من مستخدم" : "From User");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].DefaultCellValue = GlobalVariables.UserID;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Hidden = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].ValueList = (IValueList)(object)vlUsers1;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Header).Caption = (GlobalVariables.IsArabic ? "الى مستخدم" : "To User");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Comment"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.2);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Comment"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Comment"].Header).Caption = (GlobalVariables.IsArabic ? "تعليق" : "Comment");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.05);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تام" : "Completed");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].DefaultCellValue = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.05);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Header).Caption = (GlobalVariables.IsArabic ? "متابعة" : "FollowUp");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].DefaultCellValue = false;
	}

	public void FillGrid()
	{
		dtRemarks = OperationsRemarks.Distribution(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGRemarks).DataSource = dtRemarks;
		((Control)(object)lblRemarksCounterResult).Text = ((UltraGridBase)ULGRemarks).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGRemarks_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGRemarks.ActiveCell).Selected = true;
	}

	private void ULGRemarks_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGRemarks).ActiveRow != null)
		{
			frmInsertOperationRemark frmInsertOperationRemark2 = new frmInsertOperationRemark(((UltraGridBase)ULGRemarks).ActiveRow.Cells["OperationRemarkID"].Value.ToString());
			frmInsertOperationRemark2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmInsertOperationRemark2.lblTitle).Text = (GlobalVariables.IsArabic ? " ملاحظــــــــــات" : "Remarks");
			frmInsertOperationRemark2.ShowDialog();
			FillGrid();
		}
	}

	private void btnNewRemark_Click(object sender, EventArgs e)
	{
		frmInsertOperationRemark frmInsertOperationRemark2 = new frmInsertOperationRemark();
		frmInsertOperationRemark2.StartPosition = FormStartPosition.CenterParent;
		((Control)(object)frmInsertOperationRemark2.lblTitle).Text = (GlobalVariables.IsArabic ? " ملاحظــــــــــات" : "Remarks");
		frmInsertOperationRemark2.ShowDialog();
		FillGrid();
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGRemarks_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblRemarksCounterResult).Text = ((UltraGridBase)ULGRemarks).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewRemarks));
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
		this.ULGRemarks = new UltraGrid();
		this.lblRemarksCounterResult = new UltraLabel();
		this.lblRemarksCounter = new UltraLabel();
		this.btnNewRemark = new UltraButton();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGRemarks).BeginInit();
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
		resources.ApplyResources(this.ULGRemarks, "ULGRemarks");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGRemarks).Name = "ULGRemarks";
		this.ULGRemarks.AfterEnterEditMode += new System.EventHandler(ULGRemarks_AfterEnterEditMode);
		((UltraGridBase)this.ULGRemarks).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGRemarks_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGRemarks).DoubleClick += new System.EventHandler(ULGRemarks_DoubleClick);
		resources.ApplyResources(this.lblRemarksCounterResult, "lblRemarksCounterResult");
		resources.ApplyResources(val15, "appearance21");
		((ControlBase)this.lblRemarksCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblRemarksCounterResult).Name = "lblRemarksCounterResult";
		resources.ApplyResources(this.lblRemarksCounter, "lblRemarksCounter");
		this.lblRemarksCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRemarksCounter).Name = "lblRemarksCounter";
		((ControlBase)this.lblRemarksCounter).WrapText = false;
		resources.ApplyResources(this.btnNewRemark, "btnNewRemark");
		((AppearanceBase)val16).Image = resources.GetObject("appearance22.Image");
		resources.ApplyResources(val16, "appearance22");
		((ControlBase)this.btnNewRemark).Appearance = (AppearanceBase)(object)val16;
		((ControlBase)this.btnNewRemark).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewRemark).Name = "btnNewRemark";
		((System.Windows.Forms.Control)(object)this.btnNewRemark).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnNewRemark).Click += new System.EventHandler(btnNewRemark_Click);
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewRemark);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarksCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarksCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGRemarks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewRemarks";
		base.Load += new System.EventHandler(frmViewShortPass_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRemarksCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRemarksCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewRemark, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGRemarks).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
