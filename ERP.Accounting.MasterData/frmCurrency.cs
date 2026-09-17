using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.MasterData;

public class frmCurrency : frmGrid
{
	private DataTable dtEINVCurrency;

	private ValueList vlEINVCurrency = new ValueList();

	private bool UseElectronicInvoice = false;

	private IContainer components = null;

	private UltraTextEditor txtCurrencyCode;

	private UltraLabel lblCurrencyCode;

	private UltraTextEditor txtCurrencyArabicName;

	private UltraLabel lblCurrencyArabicName;

	private UltraTextEditor txtCurrencyEnglishName;

	private UltraLabel lblCurrencyEnglishName;

	private UltraLabel lblCurrencyChangeArabicName;

	private UltraTextEditor txtCurrencyChangeArabicName;

	private UltraLabel lblCurrencyChangeEnglishName;

	private UltraTextEditor txtCurrencyChangeEnglishName;

	private UltraComboEditor cboEINVCode;

	private UltraLabel lblEINVCode;

	public frmCurrency()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Currency";
		IDCol = "CurrencyID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		if (UseElectronicInvoice)
		{
			dtEINVCurrency = BusinessLayer.EInvoices.Currency.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVCode, dtEINVCurrency, "EINVCurrencyID", "EINVCurrencyName");
			vlEINVCurrency.ValueListItems.Clear();
			for (int i = 0; i < dtEINVCurrency.Rows.Count; i++)
			{
				vlEINVCurrency.ValueListItems.Add((object)dtEINVCurrency.Rows[i]["EINVCurrencyID"].ToString(), dtEINVCurrency.Rows[i]["EINVCurrencyName"].ToString());
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtCurrencyCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCurrencyArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCurrencyEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCurrencyChangeArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCurrencyChangeEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVCode).ReadOnly = NavMode;
		UltraLabel obj = lblEINVCode;
		bool visible = (((Control)(object)cboEINVCode).Visible = UseElectronicInvoice);
		((Control)(object)obj).Visible = visible;
		((TextEditorControlBase)txtCurrencyCode).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtCurrencyCode).Clear();
		((TextEditorControlBase)txtCurrencyArabicName).Clear();
		((TextEditorControlBase)txtCurrencyEnglishName).Clear();
		((TextEditorControlBase)txtCurrencyChangeArabicName).Clear();
		((TextEditorControlBase)txtCurrencyChangeEnglishName).Clear();
		cboEINVCode.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = BusinessLayer.Accounting.Currency.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود العملة" : "Currency Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "إسم العملة بالعربية " : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "إسم العملة بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChangeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " كسرالعملة بالعربية" : "Change Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChangeNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChangeNameAr"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.15)) : ((int)((double)((Control)(object)ULGData).Width * 0.2)));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChangeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? " كسرالعملة بالإنجليزية" : "Change Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChangeNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChangeNameEn"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.15)) : ((int)((double)((Control)(object)ULGData).Width * 0.2)));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCurrencyID"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفاتورة الالكترونية" : "E-Invoice Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCurrencyID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCurrencyID"].ValueList = (IValueList)(object)vlEINVCurrency;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCurrencyID"].Hidden = !UseElectronicInvoice;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtCurrencyCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyCode"].Value.ToString();
		((Control)(object)txtCurrencyArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyNameAr"].Value.ToString();
		((Control)(object)txtCurrencyEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyNameEn"].Value.ToString();
		((Control)(object)txtCurrencyChangeArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ChangeNameAr"].Value.ToString();
		((TextEditorControlBase)cboEINVCode).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EINVCurrencyID"].Value;
		((Control)(object)txtCurrencyChangeEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ChangeNameEn"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCurrencyArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم العملة بالعربية", "Please Enter Currency Arabic Name");
			((TextEditorControlBase)txtCurrencyArabicName).Focus();
			return false;
		}
		if (((Control)(object)txtCurrencyEnglishName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم العملة بالإنجليزية", "Please Enter Currency English Name");
			((TextEditorControlBase)txtCurrencyEnglishName).Focus();
			return false;
		}
		if (((Control)(object)txtCurrencyCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود العملة ", "Please Enter Currency Code");
			((TextEditorControlBase)txtCurrencyCode).Focus();
			return false;
		}
		if (((Control)(object)txtCurrencyChangeArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كسر العملة بالعربية ", "Please Enter Currency Change Arabic Name");
			((TextEditorControlBase)txtCurrencyChangeArabicName).Focus();
			return false;
		}
		if (((Control)(object)txtCurrencyChangeEnglishName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كسر العملة بلإنجليزية ", "Please Enter Currency Change EnglishName Name");
			((TextEditorControlBase)txtCurrencyChangeEnglishName).Focus();
			return false;
		}
		if (Main.CheckForValue("Currency", "CurrencyCode", ((Control)(object)txtCurrencyCode).Text, Updating ? ((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyCode"].Value.ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" كود العملة متواجد من قبل ", "Currency Code Already Exist");
			((TextEditorControlBase)txtCurrencyCode).Focus();
			return false;
		}
		if (Main.CheckForValue("Currency", "CurrencyNameAr", ((Control)(object)txtCurrencyArabicName).Text, Updating ? ((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyNameAr"].Value.ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" إسم العملة بالعربية متواجد من قبل ", "Currency Arabic Name Already Exist");
			((TextEditorControlBase)txtCurrencyArabicName).Focus();
			return false;
		}
		if (UseElectronicInvoice && cboEINVCode.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار كود الفاتورة الإلكترونية", "Please Select E-Invoice Code");
			((TextEditorControlBase)cboEINVCode).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		BusinessLayer.Accounting.Currency.Insert_Update("-1", ((Control)(object)txtCurrencyCode).Text, ((Control)(object)txtCurrencyArabicName).Text, ((Control)(object)txtCurrencyEnglishName).Text, ((Control)(object)txtCurrencyChangeArabicName).Text, ((Control)(object)txtCurrencyChangeEnglishName).Text, (cboEINVCode.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVCode).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		BusinessLayer.Accounting.Currency.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyID"].Value.ToString(), ((Control)(object)txtCurrencyCode).Text, ((Control)(object)txtCurrencyArabicName).Text, ((Control)(object)txtCurrencyEnglishName).Text, ((Control)(object)txtCurrencyChangeArabicName).Text, ((Control)(object)txtCurrencyChangeEnglishName).Text, (cboEINVCode.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVCode).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		if (GlobalVariables.LocalCurrencyID != int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyID"].Value.ToString()))
		{
			BusinessLayer.Accounting.Currency.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["CurrencyID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.MasterData.frmCurrency));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtCurrencyCode = new UltraTextEditor();
		this.lblCurrencyCode = new UltraLabel();
		this.txtCurrencyArabicName = new UltraTextEditor();
		this.lblCurrencyArabicName = new UltraLabel();
		this.txtCurrencyEnglishName = new UltraTextEditor();
		this.lblCurrencyEnglishName = new UltraLabel();
		this.lblCurrencyChangeArabicName = new UltraLabel();
		this.txtCurrencyChangeArabicName = new UltraTextEditor();
		this.lblCurrencyChangeEnglishName = new UltraLabel();
		this.txtCurrencyChangeEnglishName = new UltraTextEditor();
		this.cboEINVCode = new UltraComboEditor();
		this.lblEINVCode = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyChangeArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyChangeEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVCode).BeginInit();
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance10");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtCurrencyCode, "txtCurrencyCode");
		((System.Windows.Forms.Control)(object)this.txtCurrencyCode).Name = "txtCurrencyCode";
		resources.ApplyResources(this.lblCurrencyCode, "lblCurrencyCode");
		this.lblCurrencyCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrencyCode).Name = "lblCurrencyCode";
		((ControlBase)this.lblCurrencyCode).WrapText = false;
		resources.ApplyResources(this.txtCurrencyArabicName, "txtCurrencyArabicName");
		((System.Windows.Forms.Control)(object)this.txtCurrencyArabicName).Name = "txtCurrencyArabicName";
		resources.ApplyResources(this.lblCurrencyArabicName, "lblCurrencyArabicName");
		this.lblCurrencyArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrencyArabicName).Name = "lblCurrencyArabicName";
		((ControlBase)this.lblCurrencyArabicName).WrapText = false;
		resources.ApplyResources(this.txtCurrencyEnglishName, "txtCurrencyEnglishName");
		((System.Windows.Forms.Control)(object)this.txtCurrencyEnglishName).Name = "txtCurrencyEnglishName";
		resources.ApplyResources(this.lblCurrencyEnglishName, "lblCurrencyEnglishName");
		this.lblCurrencyEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrencyEnglishName).Name = "lblCurrencyEnglishName";
		((ControlBase)this.lblCurrencyEnglishName).WrapText = false;
		resources.ApplyResources(this.lblCurrencyChangeArabicName, "lblCurrencyChangeArabicName");
		this.lblCurrencyChangeArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrencyChangeArabicName).Name = "lblCurrencyChangeArabicName";
		((ControlBase)this.lblCurrencyChangeArabicName).WrapText = false;
		resources.ApplyResources(this.txtCurrencyChangeArabicName, "txtCurrencyChangeArabicName");
		((System.Windows.Forms.Control)(object)this.txtCurrencyChangeArabicName).Name = "txtCurrencyChangeArabicName";
		resources.ApplyResources(this.lblCurrencyChangeEnglishName, "lblCurrencyChangeEnglishName");
		this.lblCurrencyChangeEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrencyChangeEnglishName).Name = "lblCurrencyChangeEnglishName";
		((ControlBase)this.lblCurrencyChangeEnglishName).WrapText = false;
		resources.ApplyResources(this.txtCurrencyChangeEnglishName, "txtCurrencyChangeEnglishName");
		((System.Windows.Forms.Control)(object)this.txtCurrencyChangeEnglishName).Name = "txtCurrencyChangeEnglishName";
		resources.ApplyResources(this.cboEINVCode, "cboEINVCode");
		this.cboEINVCode.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboEINVCode).Name = "cboEINVCode";
		resources.ApplyResources(this.lblEINVCode, "lblEINVCode");
		this.lblEINVCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVCode).Name = "lblEINVCode";
		((ControlBase)this.lblEINVCode).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrencyChangeEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrencyEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrencyChangeEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrencyEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrencyChangeArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrencyArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrencyChangeArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrencyArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCurrencyCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrencyCode);
		base.Name = "frmCurrency";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrencyCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCurrencyCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrencyArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrencyChangeArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCurrencyArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCurrencyChangeArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrencyEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrencyChangeEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCurrencyEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCurrencyChangeEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEINVCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEINVCode, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyChangeArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCurrencyChangeEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
