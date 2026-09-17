using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using BusinessLayer.Sling;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Transactions;

public class frmQuotationsCertificatesInquiry : frmBase
{
	private DataTable dtDetails;

	public string selectedDescription;

	private bool returnDescription = false;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGDataCargo;

	public UltraButton btnSearch;

	private UltraLabel lblCertificateNo;

	private UltraTextEditor txtCertificateNo;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtPrintedItemNo;

	private UltraTextEditor txtOrderNo;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtDescription;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtClientName;

	private UltraDateTimeEditor dtpFromDate;

	private UltraLabel lblDate;

	private UltraLabel ultraLabel4;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	public frmQuotationsCertificatesInquiry()
	{
		InitializeComponent();
	}

	public frmQuotationsCertificatesInquiry(bool returnData)
	{
		InitializeComponent();
		returnDescription = returnData;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpFromDate.Value = null;
		dtpToDate.Value = null;
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGDataCargo);
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationCertificateNo"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationCertificateNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationCertificateNo"].Header).Caption = (GlobalVariables.IsArabic ? " Certificate No" : "Certificate No");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationCertificateDate"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationCertificateDate"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotationCertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? "Certificate Date " : "Certificate Date ");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotaionNo"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotaionNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["QuotaionNo"].Header).Caption = (GlobalVariables.IsArabic ? "Quotaion No" : "Quotaion No");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ClientSubAccountName"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ClientSubAccountName"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["ClientSubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "Client Name" : "Client Name");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["PrintedItemNo"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["PrintedItemNo"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["PrintedItemNo"].Header).Caption = (GlobalVariables.IsArabic ? "ID No." : "ID No.");
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGDataCargo).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGDataCargo).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "Description" : "Description");
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
		dtDetails = QuotationsCertificatesDetails.Inquiry(GlobalVariables.CurrentBranchID, ((Control)(object)txtCertificateNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtCertificateNo).Text.Trim(), (dtpFromDate.Value == null) ? "Null" : dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpToDate.Value == null) ? "Null" : dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtOrderNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtOrderNo).Text.Trim(), ((Control)(object)txtClientName).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtClientName).Text.Trim(), ((Control)(object)txtPrintedItemNo).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtPrintedItemNo).Text.Trim(), ((Control)(object)txtDescription).Text.Trim().Equals("") ? "Null" : ((Control)(object)txtDescription).Text.Trim(), GlobalVariables.IsArabic ? "1" : "0");
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
		if (((Control)(object)txtCertificateNo).Text.Trim().Equals("") && ((Control)(object)txtClientName).Text.Trim().Equals("") && ((Control)(object)txtDescription).Text.Trim().Equals("") && ((Control)(object)txtOrderNo).Text.Trim().Equals("") && ((Control)(object)txtPrintedItemNo).Text.Trim().Equals("") && dtpFromDate.Value == null && dtpToDate.Value == null)
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
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.Transactions.frmQuotationsCertificatesInquiry));
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
		this.lblCertificateNo = new UltraLabel();
		this.txtCertificateNo = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtPrintedItemNo = new UltraTextEditor();
		this.txtOrderNo = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
		this.txtDescription = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.txtClientName = new UltraTextEditor();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintedItemNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOrderNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance16");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance17");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance18.Image");
		resources.ApplyResources(val3, "appearance18");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance19");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGDataCargo, "ULGDataCargo");
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((AppearanceBase)val11).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val13).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGDataCargo).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.ULGDataCargo).Name = "ULGDataCargo";
		this.ULGDataCargo.AfterEnterEditMode += new System.EventHandler(ULGDataCargo_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGDataCargo).DoubleClick += new System.EventHandler(ULGDataCargo_DoubleClick);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val15).Image = resources.GetObject("appearance20.Image");
		resources.ApplyResources(val15, "appearance20");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.lblCertificateNo, "lblCertificateNo");
		this.lblCertificateNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCertificateNo).Name = "lblCertificateNo";
		((ControlBase)this.lblCertificateNo).WrapText = false;
		resources.ApplyResources(this.txtCertificateNo, "txtCertificateNo");
		((System.Windows.Forms.Control)(object)this.txtCertificateNo).Name = "txtCertificateNo";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtPrintedItemNo, "txtPrintedItemNo");
		((System.Windows.Forms.Control)(object)this.txtPrintedItemNo).Name = "txtPrintedItemNo";
		resources.ApplyResources(this.txtOrderNo, "txtOrderNo");
		((System.Windows.Forms.Control)(object)this.txtOrderNo).Name = "txtOrderNo";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtDescription, "txtDescription");
		((System.Windows.Forms.Control)(object)this.txtDescription).Name = "txtDescription";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.txtClientName, "txtClientName");
		((System.Windows.Forms.Control)(object)this.txtClientName).Name = "txtClientName";
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		this.dtpFromDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		this.dtpToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOrderNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPrintedItemNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataCargo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmQuotationsCertificatesInquiry";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGDataCargo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPrintedItemNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOrderNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientName, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataCargo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPrintedItemNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOrderNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
