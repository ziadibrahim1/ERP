using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmVisaTypes : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private IContainer components = null;

	private UltraLabel lblVisaTypeEnglishName;

	private UltraTextEditor txtVisaTypeEnglishName;

	private UltraLabel lblVisaTypeArabicName;

	private UltraTextEditor txtVisaTypeArabicName;

	public UltraButton btnVisaSubAccountSearch;

	private UltraComboEditor cboVisaSubAccount;

	private UltraLabel lblVisaSubAccount;

	public UltraButton btnVisaAccountSearch;

	private UltraComboEditor cboVisaAccount;

	private UltraLabel lblAccountName;

	private UltraCheckEditor chkIsActive;

	public frmVisaTypes()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "G_VisaTypes";
		IDCol = "VisaTypeID";
	}

	public override void PrepareData()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVisaAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVisaSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlSubAccounts.ValueListItems.Clear();
		for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtVisaTypeArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtVisaTypeEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboVisaAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboVisaSubAccount).ReadOnly = NavMode;
		((Control)(object)btnVisaAccountSearch).Visible = !NavMode;
		((Control)(object)btnVisaSubAccountSearch).Visible = !NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((TextEditorControlBase)txtVisaTypeArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtVisaTypeArabicName).Clear();
		((TextEditorControlBase)txtVisaTypeEnglishName).Clear();
		cboVisaAccount.SelectedIndex = -1;
		cboVisaSubAccount.SelectedIndex = -1;
		((UltraToggleEditorBase)chkIsActive).Checked = true;
	}

	public override void FillData()
	{
		dataTable = VisaTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الائتمان" : "Visa Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى " : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Header).Caption = (GlobalVariables.IsArabic ? "نشط" : "Active");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtVisaTypeArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["VisaTypeNameAr"].Value.ToString();
		((Control)(object)txtVisaTypeEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["VisaTypeNameEn"].Value.ToString();
		((TextEditorControlBase)cboVisaAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboVisaSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
		((UltraToggleEditorBase)chkIsActive).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsActive"].Value.ToString());
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtVisaTypeArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اللقب بالعربية", "Please Insert Title Arabic Name");
			((TextEditorControlBase)txtVisaTypeArabicName).Focus();
			return false;
		}
		if (cboVisaAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الحساب", "Please Select Account");
			((TextEditorControlBase)cboVisaAccount).Focus();
			cboVisaAccount.DropDown();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		VisaTypes.Insert_Update("-1", ((Control)(object)txtVisaTypeArabicName).Text, (((Control)(object)txtVisaTypeEnglishName).Text == "") ? "Null" : ((Control)(object)txtVisaTypeEnglishName).Text, (cboVisaAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaAccount).Value.ToString(), (cboVisaSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaSubAccount).Value.ToString(), ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		VisaTypes.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["VisaTypeID"].Value.ToString(), ((Control)(object)txtVisaTypeArabicName).Text, (((Control)(object)txtVisaTypeEnglishName).Text == "") ? "Null" : ((Control)(object)txtVisaTypeEnglishName).Text, (cboVisaAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaAccount).Value.ToString(), (cboVisaSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboVisaSubAccount).Value.ToString(), ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		VisaTypes.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["VisaTypeID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	private void cboVisaAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			((TextEditorControlBase)cboVisaAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
	}

	private void btnVisaAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboVisaAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void cboVisaAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboVisaAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboVisaAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboVisaSubAccount.DataSource = dataView;
		}
	}

	private void btnVisaSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboVisaAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboVisaAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboVisaSubAccount).Value = num;
			}
		}
	}

	private void cboVisaSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboVisaAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboVisaAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboVisaSubAccount).Value = num;
			}
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmVisaTypes));
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
		this.lblVisaTypeEnglishName = new UltraLabel();
		this.txtVisaTypeEnglishName = new UltraTextEditor();
		this.lblVisaTypeArabicName = new UltraLabel();
		this.txtVisaTypeArabicName = new UltraTextEditor();
		this.btnVisaSubAccountSearch = new UltraButton();
		this.cboVisaSubAccount = new UltraComboEditor();
		this.lblVisaSubAccount = new UltraLabel();
		this.btnVisaAccountSearch = new UltraButton();
		this.cboVisaAccount = new UltraComboEditor();
		this.lblAccountName = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaTypeEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaTypeArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
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
		resources.ApplyResources(this.lblVisaTypeEnglishName, "lblVisaTypeEnglishName");
		this.lblVisaTypeEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaTypeEnglishName).Name = "lblVisaTypeEnglishName";
		((ControlBase)this.lblVisaTypeEnglishName).WrapText = false;
		resources.ApplyResources(this.txtVisaTypeEnglishName, "txtVisaTypeEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtVisaTypeEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtVisaTypeEnglishName).Name = "txtVisaTypeEnglishName";
		resources.ApplyResources(this.lblVisaTypeArabicName, "lblVisaTypeArabicName");
		this.lblVisaTypeArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaTypeArabicName).Name = "lblVisaTypeArabicName";
		((ControlBase)this.lblVisaTypeArabicName).WrapText = false;
		resources.ApplyResources(this.txtVisaTypeArabicName, "txtVisaTypeArabicName");
		((System.Windows.Forms.Control)(object)this.txtVisaTypeArabicName).Name = "txtVisaTypeArabicName";
		resources.ApplyResources(this.btnVisaSubAccountSearch, "btnVisaSubAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnVisaSubAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnVisaSubAccountSearch).Name = "btnVisaSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnVisaSubAccountSearch).Click += new System.EventHandler(btnVisaSubAccountSearch_Click);
		resources.ApplyResources(this.cboVisaSubAccount, "cboVisaSubAccount");
		this.cboVisaSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboVisaSubAccount).Name = "cboVisaSubAccount";
		((System.Windows.Forms.Control)(object)this.cboVisaSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboVisaSubAccount_KeyDown);
		resources.ApplyResources(this.lblVisaSubAccount, "lblVisaSubAccount");
		this.lblVisaSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaSubAccount).Name = "lblVisaSubAccount";
		((ControlBase)this.lblVisaSubAccount).WrapText = false;
		resources.ApplyResources(this.btnVisaAccountSearch, "btnVisaAccountSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnVisaAccountSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnVisaAccountSearch).Name = "btnVisaAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnVisaAccountSearch).Click += new System.EventHandler(btnVisaAccountSearch_Click);
		resources.ApplyResources(this.cboVisaAccount, "cboVisaAccount");
		this.cboVisaAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboVisaAccount).Name = "cboVisaAccount";
		((TextEditorControlBase)this.cboVisaAccount).ValueChanged += new System.EventHandler(cboVisaAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboVisaAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboVisaAccount_KeyDown);
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		this.lblAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val13;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVisaSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVisaAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaTypeEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaTypeEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaTypeArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaTypeArabicName);
		base.Name = "frmVisaTypes";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaTypeArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaTypeArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaTypeEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaTypeEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVisaAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVisaSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaTypeEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaTypeArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
