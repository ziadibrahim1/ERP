using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmRemarks : frmPosted
{
	private DataTable dtUsers = new DataTable();

	private DataTable dtForAll = new DataTable();

	private DataTable dtRemarks = new DataTable();

	private ValueList vlUsers5 = new ValueList();

	private ValueList vlUsers6 = new ValueList();

	private IContainer components = null;

	public UltraGrid ULGRemarks;

	private UltraLabel lblRemarksCounterResult;

	private UltraLabel lblRemarksCounter;

	public UltraButton btnNewRemark;

	public frmRemarks()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		InitializeComponent();
		NoCol = "OperationNo";
	}

	public override void PrepareData()
	{
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUsers5.ValueListItems.Clear();
		vlUsers6.ValueListItems.Clear();
		for (int i = 0; i < dtUsers.Rows.Count; i++)
		{
			vlUsers5.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i]["UserName"].ToString());
			vlUsers6.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i]["UserName"].ToString());
		}
	}

	public override void FillGrid()
	{
		dtRemarks = OperationsRemarks.Distribution(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGRemarks).DataSource = dtRemarks;
		InitGridRemarks();
	}

	public void InitGridRemarks()
	{
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGRemarks);
		((UltraGridBase)ULGRemarks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationRemarkID"].DefaultCellValue = -1;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Date"].DefaultCellValue = DateTime.Now;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.15);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Remarks"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Remarks");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.07);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers5;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "من مستخدم" : "From User");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["User_ID"].DefaultCellValue = GlobalVariables.UserID;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Hidden = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].ValueList = (IValueList)(object)vlUsers6;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["ToUser_ID"].Header).Caption = (GlobalVariables.IsArabic ? "الى مستخدم" : "To User");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Comment"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.15);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Comment"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["Comment"].Header).Caption = (GlobalVariables.IsArabic ? "تعليق" : "Comment");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تام" : "Completed");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsCompleted"].DefaultCellValue = false;
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Width = (int)((double)((Control)(object)ULGRemarks).Width * 0.08);
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].Header).Caption = (GlobalVariables.IsArabic ? "متابعة" : "FollowUp");
		((UltraGridBase)ULGRemarks).DisplayLayout.Bands[0].Columns["IsFollowUp"].DefaultCellValue = false;
	}

	public override void SaveData()
	{
		((UltraGridBase)ULGRemarks).UpdateData();
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGRemarks).Rows).Count > 0)
		{
			OperationsRemarks.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGRemarks).DataSource, GlobalVariables.UserID);
		}
		FillGrid();
	}

	private void ULGRemarks_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGRemarks.ActiveCell.Column).Key == "Date" || ((KeyedSubObjectBase)ULGRemarks.ActiveCell.Column).Key == "Remarks" || ((KeyedSubObjectBase)ULGRemarks.ActiveCell.Column).Key == "User_ID" || ((KeyedSubObjectBase)ULGRemarks.ActiveCell.Column).Key == "OperationNo" || ((KeyedSubObjectBase)ULGRemarks.ActiveCell.Column).Key == "VesselName" || ((KeyedSubObjectBase)ULGRemarks.ActiveCell.Column).Key == "VoyageNo")
		{
			((GridItemBase)ULGRemarks.ActiveCell).Selected = true;
		}
	}

	private void ULGRemarks_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblRemarksCounterResult).Text = ((UltraGridBase)ULGRemarks).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnNewRemark_Click(object sender, EventArgs e)
	{
		frmInsertOperationRemark frmInsertOperationRemark2 = new frmInsertOperationRemark();
		frmInsertOperationRemark2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmInsertOperationRemark2.Location = new Point(0, 0);
		frmInsertOperationRemark2.ShowDialog();
		FillGrid();
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
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
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_07c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmRemarks));
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
		this.ULGRemarks = new UltraGrid();
		this.lblRemarksCounterResult = new UltraLabel();
		this.lblRemarksCounter = new UltraLabel();
		this.btnNewRemark = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGRemarks).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorStyle = (HeaderStyle)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		((AppearanceBase)val6).FontData.Name = resources.GetString("resource.Name");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGRemarks, "ULGRemarks");
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val8;
		((SpecialBoxBase)((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val13).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val14).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGRemarks).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.ULGRemarks).Name = "ULGRemarks";
		this.ULGRemarks.AfterEnterEditMode += new System.EventHandler(ULGRemarks_AfterEnterEditMode);
		((UltraGridBase)this.ULGRemarks).FilterRow += new FilterRowEventHandler(ULGRemarks_FilterRow);
		resources.ApplyResources(this.lblRemarksCounterResult, "lblRemarksCounterResult");
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblRemarksCounterResult).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.lblRemarksCounterResult).Name = "lblRemarksCounterResult";
		resources.ApplyResources(this.lblRemarksCounter, "lblRemarksCounter");
		this.lblRemarksCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRemarksCounter).Name = "lblRemarksCounter";
		((ControlBase)this.lblRemarksCounter).WrapText = false;
		resources.ApplyResources(this.btnNewRemark, "btnNewRemark");
		((AppearanceBase)val18).Image = resources.GetObject("appearance18.Image");
		((ControlBase)this.btnNewRemark).Appearance = (AppearanceBase)(object)val18;
		((ControlBase)this.btnNewRemark).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewRemark).Name = "btnNewRemark";
		((System.Windows.Forms.Control)(object)this.btnNewRemark).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnNewRemark).Click += new System.EventHandler(btnNewRemark_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewRemark);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarksCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarksCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGRemarks);
		base.Name = "frmRemarks";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBByName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRemarksCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRemarksCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewRemark, 0);
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGRemarks).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
