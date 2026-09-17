using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmCountries : frmGrid
{
	private DataTable dtEINVCountries;

	private ValueList vlEINVCountries = new ValueList();

	private bool UseElectronicInvoice = false;

	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtCode;

	private UltraLabel lblCode;

	private UltraComboEditor cboEINVCode;

	private UltraLabel lblEINVCode;

	private UltraCheckEditor chkInEuropeanCommunity;

	private UltraCheckEditor chkInGatt;

	public frmCountries()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "G_Countries";
		IDCol = "CountryID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		if (UseElectronicInvoice)
		{
			dtEINVCountries = BusinessLayer.EInvoices.Countries.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVCode, dtEINVCountries, "EINVCountryID", "EINVCountryName");
			vlEINVCountries.ValueListItems.Clear();
			for (int i = 0; i < dtEINVCountries.Rows.Count; i++)
			{
				vlEINVCountries.ValueListItems.Add((object)dtEINVCountries.Rows[i]["EINVCountryID"].ToString(), dtEINVCountries.Rows[i]["EINVCountryName"].ToString());
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((Control)(object)chkInEuropeanCommunity).Enabled = !NavMode;
		((Control)(object)chkInGatt).Enabled = !NavMode;
		((EditorButtonControlBase)cboEINVCode).ReadOnly = NavMode;
		UltraLabel obj = lblEINVCode;
		bool visible = (((Control)(object)cboEINVCode).Visible = UseElectronicInvoice);
		((Control)(object)obj).Visible = visible;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtCode).Text = (Adding ? BusinessLayer.General.Countries.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((UltraToggleEditorBase)chkInEuropeanCommunity).Checked = false;
		((UltraToggleEditorBase)chkInGatt).Checked = false;
		cboEINVCode.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = BusinessLayer.General.Countries.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود " : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryNameAr"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.2)) : ((int)((double)((Control)(object)ULGData).Width * 0.3)));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryNameEn"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.25)) : ((int)((double)((Control)(object)ULGData).Width * 0.3)));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InEuropeanCommunity"].Header).Caption = (GlobalVariables.IsArabic ? "ضمن الإتحاد الأوربي" : "In European Community");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InEuropeanCommunity"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InEuropeanCommunity"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InGatt"].Header).Caption = (GlobalVariables.IsArabic ? "ضمن إتفاقية الجات" : "In Gatt");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InGatt"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InGatt"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCountryID"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفاتورة الالكترونية" : "E-Invoice Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCountryID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCountryID"].ValueList = (IValueList)(object)vlEINVCountries;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVCountryID"].Hidden = !UseElectronicInvoice;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CountryCode"].Value.ToString();
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CountryNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CountryNameEn"].Value.ToString();
		((UltraToggleEditorBase)chkInEuropeanCommunity).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InEuropeanCommunity"].Value.ToString());
		((UltraToggleEditorBase)chkInGatt).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InGatt"].Value.ToString());
		((TextEditorControlBase)cboEINVCode).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EINVCountryID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الكود", "Please Enter The Code");
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم  بالعربية", "Please Enter Arabic Name");
			return false;
		}
		if (dataTable.Select("CountryCode = '" + ((Control)(object)txtCode).Text + "' And CountryID <>" + (Adding ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["CountryID"].Value.ToString())).Length != 0)
		{
			GlobalVariables.InformationMB.Show("لايمكن تكرار الكود  ", "Cannot Duplicate The Code ");
			return false;
		}
		if (dataTable.Select("CountryNameAr = '" + ((Control)(object)txtArabicName).Text + "' And CountryID <>" + (Adding ? "-1" : ((UltraGridBase)ULGData).ActiveRow.Cells["CountryID"].Value.ToString())).Length != 0)
		{
			GlobalVariables.InformationMB.Show("لايمكن تكرار الاسم  بالعربية", "Cannot Duplicate The Arabic Name");
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
		BusinessLayer.General.Countries.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboEINVCode.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVCode).Value.ToString(), ((UltraToggleEditorBase)chkInEuropeanCommunity).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInGatt).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void btnUpdateClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells[IDCol].Value.ToString() == "1")
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل الدولة", "Can not Update This country ");
		}
		else
		{
			base.btnUpdateClick();
		}
	}

	public override void UpdateData()
	{
		BusinessLayer.General.Countries.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["CountryID"].Value.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboEINVCode.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVCode).Value.ToString(), ((UltraToggleEditorBase)chkInEuropeanCommunity).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInGatt).Checked ? "1" : "0", bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void btnDeleteClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells[IDCol].Value.ToString() == "1")
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف الدولة", "Can not Delete This country ");
		}
		else
		{
			base.btnDeleteClick();
		}
	}

	public override void DeleteData()
	{
		BusinessLayer.General.Countries.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["CountryID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmCountries));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.cboEINVCode = new UltraComboEditor();
		this.lblEINVCode = new UltraLabel();
		this.chkInEuropeanCommunity = new UltraCheckEditor();
		this.chkInGatt = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInEuropeanCommunity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInGatt).BeginInit();
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
		resources.ApplyResources(val8, "appearance8");
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
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((ControlBase)this.lblCode).WrapText = false;
		resources.ApplyResources(this.cboEINVCode, "cboEINVCode");
		this.cboEINVCode.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboEINVCode).Name = "cboEINVCode";
		resources.ApplyResources(this.lblEINVCode, "lblEINVCode");
		this.lblEINVCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVCode).Name = "lblEINVCode";
		((ControlBase)this.lblEINVCode).WrapText = false;
		resources.ApplyResources(this.chkInEuropeanCommunity, "chkInEuropeanCommunity");
		((System.Windows.Forms.Control)(object)this.chkInEuropeanCommunity).Name = "chkInEuropeanCommunity";
		resources.ApplyResources(this.chkInGatt, "chkInGatt");
		((System.Windows.Forms.Control)(object)this.chkInGatt).Name = "chkInGatt";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInGatt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInEuropeanCommunity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmCountries";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEINVCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEINVCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInEuropeanCommunity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInGatt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInEuropeanCommunity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInGatt).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
