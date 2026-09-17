using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Production;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.MasterData;

public class frmProductionExpenses : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private bool UseSubAccounts;

	private IContainer components = null;

	private UltraComboEditor cboSubAccount;

	private UltraLabel lblSubAccount;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtEnglishName;

	private UltraComboEditor cboAccount;

	private UltraLabel lblAccount;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtArabicName;

	public UltraButton btnAccountSearch;

	public UltraButton btnSubAccountSearch;

	public frmProductionExpenses()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Pro_Expenses";
		IDCol = "ExpenseID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = NavMode;
		((Control)(object)btnAccountSearch).Visible = !NavMode;
		((Control)(object)lblSubAccount).Visible = UseSubAccounts;
		((Control)(object)cboSubAccount).Visible = UseSubAccounts;
		((Control)(object)btnSubAccountSearch).Visible = !NavMode && UseSubAccounts;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		cboAccount.SelectedIndex = -1;
		cboSubAccount.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Expenses.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseNameEn"].Value.ToString();
		((TextEditorControlBase)cboAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Insert Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (cboAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم حساب المصروف", "Please Select Expense Account Name");
			((TextEditorControlBase)cboAccount).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Expenses.Insert_Update("-1", ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((TextEditorControlBase)cboAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Expenses.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseID"].Value.ToString(), ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((TextEditorControlBase)cboAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Expenses.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnRefreshDataClick()
	{
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
	}

	private void cboAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSubAccount.DataSource = dataView;
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboAccount).Value = num;
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
		}
	}

	private void cboSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
		}
	}

	private void cboAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboAccount).Value = num;
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.MasterData.frmProductionExpenses));
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
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.lblEnglishName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.cboAccount = new UltraComboEditor();
		this.lblAccount = new UltraLabel();
		this.lblArabicName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.btnAccountSearch = new UltraButton();
		this.btnSubAccountSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
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
		resources.ApplyResources(val8, "appearance13");
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
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		((System.Windows.Forms.Control)(object)this.cboSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccount_KeyDown);
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.cboAccount, "cboAccount");
		this.cboAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAccount).Name = "cboAccount";
		((TextEditorControlBase)this.cboAccount).ValueChanged += new System.EventHandler(cboAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboAccount_KeyDown);
		resources.ApplyResources(this.lblAccount, "lblAccount");
		this.lblAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccount).Name = "lblAccount";
		((ControlBase)this.lblAccount).WrapText = false;
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.btnAccountSearch, "btnAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.btnAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Name = "btnAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance15");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Name = "frmProductionExpenses";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
