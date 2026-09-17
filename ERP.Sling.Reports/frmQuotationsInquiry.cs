using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using BusinessLayer.Sling;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Reports;

public class frmQuotationsInquiry : frmBase
{
	private DataTable dtDetails;

	private DataTable dtClients;

	public string selectedDescription;

	private bool returnDescription = false;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGDataCargo;

	public UltraButton btnSearch;

	private UltraTextEditor txtDescription;

	private UltraLabel ultraLabel3;

	private UltraDateTimeEditor dtpFromDate;

	private UltraLabel lblDate;

	private UltraLabel ultraLabel4;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	public UltraButton btnClientSearch;

	private UltraComboEditor cboClient;

	public frmQuotationsInquiry()
	{
		InitializeComponent();
	}

	public frmQuotationsInquiry(bool returnData)
	{
		InitializeComponent();
		returnDescription = returnData;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpFromDate.Value = null;
		dtpToDate.Value = null;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataCargo);
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotaionNo"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotaionNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotaionNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "Order No");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationDate"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Order Date ");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExchangeRate"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExchangeRate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ExchangeRate"].Header).Caption = (GlobalVariables.IsArabic ? "سعر التحويل" : "Exchange Rate");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ClientSubAccountName"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ClientSubAccountName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ClientSubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم العميل" : "Client Name");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["CurrencyName"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["CurrencyName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["CurrencyName"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
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
		dtDetails = Quotations.Inquiry(GlobalVariables.CurrentBranchID, (dtpFromDate.Value == null) ? "Null" : dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpToDate.Value == null) ? "Null" : dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), (cboClient.SelectedIndex == -1) ? "-1" : ((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtDescription).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtDescription).Text.Trim(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGDataCargo).DataSource = dtDetails;
		InitGrid();
	}

	private void ULGDataCargo_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGDataCargo.ActiveCell).Selected = true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		((UltraGridBase)ULGDataCargo).UpdateData();
		OperationsServicesCargos.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataCargo).DataSource, GlobalVariables.UserID);
		FillGrid();
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		if (cboClient.SelectedIndex == -1 && ((Control)(object)txtDescription).Text.Trim().Equals("") && dtpFromDate.Value == null && dtpToDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال بيانات البحث" : "Please Enter Some Search Criteria");
		}
		else
		{
			FillGrid();
		}
	}

	private void ULGDataCargo_DoubleClick(object sender, EventArgs e)
	{
		if (returnDescription && ((UltraGridBase)ULGDataCargo).ActiveRow != null)
		{
			selectedDescription = ((UltraGridBase)ULGDataCargo).ActiveRow.Cells["Notes"].Value.ToString();
			Close();
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
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
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.Reports.frmQuotationsInquiry));
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
		this.ULGDataCargo = new UltraGrid();
		this.btnSearch = new UltraButton();
		this.txtDescription = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.btnClientSearch = new UltraButton();
		this.cboClient = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance3.FontData");
		resources.ApplyResources(val, "appearance3");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance16");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance16.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance21.Image");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance21.FontData");
		resources.ApplyResources(val3, "appearance21");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance11");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance11.FontData");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGDataCargo, "ULGDataCargo");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance1.FontData");
		resources.ApplyResources(val5, "appearance1");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance4.FontData");
		resources.ApplyResources(val6, "appearance4");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance13.FontData");
		resources.ApplyResources(val7, "appearance13");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance5.FontData");
		resources.ApplyResources(val8, "appearance5");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance15.FontData");
		resources.ApplyResources(val9, "appearance15");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance6.FontData");
		resources.ApplyResources(val10, "appearance6");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance17.FontData");
		resources.ApplyResources(val11, "appearance17");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance18.FontData");
		resources.ApplyResources(val12, "appearance18");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val13).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance19.FontData");
		resources.ApplyResources(val13, "appearance19");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance20.FontData");
		resources.ApplyResources(val14, "appearance20");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGDataCargo).Name = "ULGDataCargo";
		this.ULGDataCargo.AfterEnterEditMode += new System.EventHandler(ULGDataCargo_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGDataCargo).DoubleClick += new System.EventHandler(ULGDataCargo_DoubleClick);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.txtDescription, "txtDescription");
		((System.Windows.Forms.Control)(object)this.txtDescription).Name = "txtDescription";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		this.dtpFromDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		this.dtpToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(((AppearanceBase)val15).FontData, "appearance9.FontData");
		resources.ApplyResources(val15, "appearance9");
		((SubObjectBase)val15).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val15;
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboClient, "cboClient");
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataCargo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmQuotationsInquiry";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGDataCargo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
