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

public class frmViewForAllUsersTasks : frmBase
{
	private UltraGridFilterUIProvider FilterProvider = new UltraGridFilterUIProvider();

	private DataTable dtUsers;

	private DataTable dtServices;

	private DataTable dtForAll;

	private ValueList vlUsers = new ValueList();

	private bool IsSuperVisor;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGForAll;

	private UltraLabel lblForAllUsersCounterResult;

	private UltraLabel lblForAllUsersCounter;

	public UltraButton btnRefreshData;

	public frmViewForAllUsersTasks()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmViewForAllUsersTasks(bool _IsSuperVisor, bool _CaUpdate)
		: this()
	{
		IsSuperVisor = _IsSuperVisor;
		CanUpdate = _CaUpdate;
		TableName = "MS_OperationsServicesStepsTasks";
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
		for (int i = 0; i < dtUsers.Rows.Count; i++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i]["UserName"].ToString());
		}
	}

	public void InitGrid()
	{
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGForAll);
		((UltraGridBase)ULGForAll).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGForAll).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGForAll).DisplayLayout.Override.FilterUIProvider = (IFilterUIProvider)(object)FilterProvider;
		((UltraGridBase)ULGForAll).DisplayLayout.Override.AllowRowFiltering = (DefaultableBoolean)1;
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "العمليه" : "OperationNo");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.05);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["OperationServiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الخدمة" : "Service Serial");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.09);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخره" : "Vessel");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.08);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرحله" : "VoyageNo");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["ServiceName"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.1);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["ServiceName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["ServiceName"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمه" : "Service");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StepName"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.05);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StepName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StepName"].Header).Caption = (GlobalVariables.IsArabic ? "المرحله" : "Step");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["TaskName"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.05);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["TaskName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["TaskName"].Header).Caption = (GlobalVariables.IsArabic ? "المهمه" : "Task");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["EntryPort"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.1);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["EntryPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["EntryPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء الدخول" : "Entry Port");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["User_ID"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.1);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["User_ID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["User_ID"].Header).Caption = (GlobalVariables.IsArabic ? "المندوب" : "PRO");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["User_ID"].ValueList = (IValueList)(object)vlUsers;
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.1);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تبداء في" : "Starts at");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StartDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["StartDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.1);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["ServiceUser"].Width = (int)((double)((Control)(object)ULGForAll).Width * 0.1);
		((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["ServiceUser"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGForAll).DisplayLayout.Bands[0].Columns["ServiceUser"].Header).Caption = (GlobalVariables.IsArabic ? "من مستخدم" : "From User");
	}

	public void FillGrid()
	{
		if (IsSuperVisor)
		{
			dtForAll = OperationsServicesStepsTasks.Distribution(GlobalVariables.CurrentBranchID, "-1", "-1", "0", "0", "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtForAll = OperationsServicesStepsTasks.Distribution(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.UserID, "0", "0", "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGForAll).DataSource = dtForAll;
		((Control)(object)lblForAllUsersCounterResult).Text = ((UltraGridBase)ULGForAll).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGForAll_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGForAll.ActiveCell).Selected = true;
	}

	private void ULGForAll_DoubleClick(object sender, EventArgs e)
	{
		if (CanUpdate && ((UltraGridBase)ULGForAll).ActiveRow != null)
		{
			bool flag = true;
			string iD;
			if (((UltraGridBase)ULGForAll).ActiveRow.Cells["OperationServiceStepTaskID"].Value.ToString() == "")
			{
				iD = ((UltraGridBase)ULGForAll).ActiveRow.Cells["TaskWithoutOperationID"].Value.ToString();
				flag = false;
			}
			else
			{
				iD = ((UltraGridBase)ULGForAll).ActiveRow.Cells["OperationServiceStepTaskID"].Value.ToString();
				flag = true;
			}
			frmUpdatePROTask frmUpdatePROTask2 = new frmUpdatePROTask(iD, ForAllUsers: true, flag);
			frmUpdatePROTask2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmUpdatePROTask2.lblTitle).Text = (GlobalVariables.IsArabic ? "لكل المندوبين" : "For All Users");
			frmUpdatePROTask2.ShowDialog();
			FillGrid();
		}
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGForAll_AfterRowFilterChanged(object sender, AfterRowFilterChangedEventArgs e)
	{
		((Control)(object)lblForAllUsersCounterResult).Text = ((UltraGridBase)ULGForAll).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmViewForAllUsersTasks));
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
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGForAll = new UltraGrid();
		this.lblForAllUsersCounterResult = new UltraLabel();
		this.lblForAllUsersCounter = new UltraLabel();
		this.btnRefreshData = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGForAll).BeginInit();
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
		resources.ApplyResources(this.ULGForAll, "ULGForAll");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGForAll).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGForAll).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGForAll).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGForAll).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGForAll).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGForAll).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGForAll).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGForAll).Name = "ULGForAll";
		this.ULGForAll.AfterEnterEditMode += new System.EventHandler(ULGForAll_AfterEnterEditMode);
		((UltraGridBase)this.ULGForAll).AfterRowFilterChanged += new AfterRowFilterChangedEventHandler(ULGForAll_AfterRowFilterChanged);
		((System.Windows.Forms.Control)(object)this.ULGForAll).DoubleClick += new System.EventHandler(ULGForAll_DoubleClick);
		resources.ApplyResources(this.lblForAllUsersCounterResult, "lblForAllUsersCounterResult");
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblForAllUsersCounterResult).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblForAllUsersCounterResult).Name = "lblForAllUsersCounterResult";
		resources.ApplyResources(this.lblForAllUsersCounter, "lblForAllUsersCounter");
		this.lblForAllUsersCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblForAllUsersCounter).Name = "lblForAllUsersCounter";
		((ControlBase)this.lblForAllUsersCounter).WrapText = false;
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblForAllUsersCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblForAllUsersCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGForAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmViewForAllUsersTasks";
		base.Load += new System.EventHandler(frmViewShortPass_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGForAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblForAllUsersCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblForAllUsersCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGForAll).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
