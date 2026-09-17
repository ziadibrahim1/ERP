using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.CustomsClearence;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.MasterData;

public class frmBotanicalNames : frmGrid
{
	private DataTable dtOriginCriteria;

	private ValueList vlOriginCriteria = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraLabel lblCustomsTaarifNo;

	private UltraTextEditor txtCustomsTaarifNo;

	private UltraTextEditor txtHSCode;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraComboEditor cboOriginCriteria;

	public frmBotanicalNames()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CST_BotanicalNames";
		IDCol = "BotanicalNameID";
	}

	public override void PrepareData()
	{
		dtOriginCriteria = OriginCriteria.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboOriginCriteria, dtOriginCriteria, "OriginCriterionID", "OriginCriterionCode");
		vlOriginCriteria.ValueListItems.Clear();
		for (int i = 0; i < dtOriginCriteria.Rows.Count; i++)
		{
			vlOriginCriteria.ValueListItems.Add(dtOriginCriteria.Rows[i]["OriginCriterionID"], dtOriginCriteria.Rows[i]["OriginCriterionCode"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCustomsTaarifNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtHSCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOriginCriteria).ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)txtHSCode).Clear();
		((TextEditorControlBase)txtCustomsTaarifNo).Clear();
		cboOriginCriteria.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = BotanicalNames.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BotanicalNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BotanicalNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BotanicalNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BotanicalNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BotanicalNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BotanicalNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTaarifNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم تعريف جمركي" : "Customs Taarif No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTaarifNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsTaarifNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HSCode"].Header).Caption = (GlobalVariables.IsArabic ? "HSCode" : "HSCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HSCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HSCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginCriterionID"].Header).Caption = (GlobalVariables.IsArabic ? "المكون المحلي" : "Origin Criterion Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginCriterionID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginCriterionID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginCriterionID"].ValueList = (IValueList)(object)vlOriginCriteria;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BotanicalNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BotanicalNameEn"].Value.ToString();
		((Control)(object)txtCustomsTaarifNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CustomsTaarifNo"].Value.ToString();
		((Control)(object)txtHSCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["HSCode"].Value.ToString();
		((TextEditorControlBase)cboOriginCriteria).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["OriginCriterionID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم  بالعربية", "Please Enter The Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		BotanicalNames.Insert_Update("-1", ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text, ((Control)(object)txtCustomsTaarifNo).Text, (((Control)(object)txtHSCode).Text == "") ? "Null" : ((Control)(object)txtHSCode).Text, (cboOriginCriteria.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOriginCriteria).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		BotanicalNames.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["BotanicalNameID"].Value.ToString(), ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? ((Control)(object)txtArabicName).Text : ((Control)(object)txtEnglishName).Text, ((Control)(object)txtCustomsTaarifNo).Text, (((Control)(object)txtHSCode).Text == "") ? "Null" : ((Control)(object)txtHSCode).Text, (cboOriginCriteria.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOriginCriteria).Value.ToString(), bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		BotanicalNames.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["BotanicalNameID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void frmBotanicalNames_Load(object sender, EventArgs e)
	{
	}

	private void txtCustomsTaarifNo_ValueChanged(object sender, EventArgs e)
	{
	}

	private void ultraLabel1_Click(object sender, EventArgs e)
	{
	}

	private void ultraTextEditor1_ValueChanged(object sender, EventArgs e)
	{
	}

	private void lblCustomsTaarifNo_Click(object sender, EventArgs e)
	{
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.MasterData.frmBotanicalNames));
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
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.lblCustomsTaarifNo = new UltraLabel();
		this.txtCustomsTaarifNo = new UltraTextEditor();
		this.txtHSCode = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.cboOriginCriteria = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsTaarifNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHSCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOriginCriteria).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.lblCustomsTaarifNo, "lblCustomsTaarifNo");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance11");
		((ControlBase)this.lblCustomsTaarifNo).Appearance = (AppearanceBase)(object)val8;
		this.lblCustomsTaarifNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo).Name = "lblCustomsTaarifNo";
		((ControlBase)this.lblCustomsTaarifNo).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo).Click += new System.EventHandler(lblCustomsTaarifNo_Click);
		resources.ApplyResources(this.txtCustomsTaarifNo, "txtCustomsTaarifNo");
		((System.Windows.Forms.Control)(object)this.txtCustomsTaarifNo).Name = "txtCustomsTaarifNo";
		((TextEditorControlBase)this.txtCustomsTaarifNo).ValueChanged += new System.EventHandler(txtCustomsTaarifNo_ValueChanged);
		resources.ApplyResources(this.txtHSCode, "txtHSCode");
		((System.Windows.Forms.Control)(object)this.txtHSCode).Name = "txtHSCode";
		((TextEditorControlBase)this.txtHSCode).ValueChanged += new System.EventHandler(ultraTextEditor1_ValueChanged);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val9, "appearance12");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val9;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Click += new System.EventHandler(ultraLabel1_Click);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance13");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.cboOriginCriteria, "cboOriginCriteria");
		((TextEditorControlBase)this.cboOriginCriteria).AlwaysInEditMode = true;
		this.cboOriginCriteria.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboOriginCriteria).Name = "cboOriginCriteria";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOriginCriteria);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHSCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomsTaarifNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmBotanicalNames";
		base.Load += new System.EventHandler(frmBotanicalNames_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomsTaarifNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHSCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOriginCriteria, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsTaarifNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHSCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOriginCriteria).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
