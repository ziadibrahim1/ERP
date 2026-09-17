using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.MasterData;

public class frmExpenses : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private bool UseSubAccounts;

	private IContainer components = null;

	public UltraButton btnExpenseAccountSearch;

	private UltraLabel lblExpenseEnglishName;

	private UltraTextEditor txtExpenseEnglishName;

	private UltraComboEditor cboExpenseAccount;

	private UltraLabel lblExpenseAccount;

	private UltraLabel lblExpenseArabicName;

	private UltraTextEditor txtExpenseArabicName;

	public UltraButton btnExpenseSubAccountSearch;

	private UltraComboEditor cboExpenseSubAccount;

	private UltraLabel lblExpenseSubAccount;

	private UltraCheckEditor chkOnClientAccount;

	public frmExpenses()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CST_Expenses";
		IDCol = "ExpenseID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboExpenseAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboExpenseSubAccount, dtSubAccounts, "SubAccountID", "Name");
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
		((EditorButtonControlBase)txtExpenseArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExpenseEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboExpenseAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboExpenseSubAccount).ReadOnly = NavMode;
		((Control)(object)chkOnClientAccount).Enabled = !NavMode;
		((Control)(object)btnExpenseAccountSearch).Visible = !NavMode;
		((Control)(object)lblExpenseSubAccount).Visible = UseSubAccounts;
		((Control)(object)cboExpenseSubAccount).Visible = UseSubAccounts;
		((Control)(object)btnExpenseSubAccountSearch).Visible = !NavMode && UseSubAccounts;
		((TextEditorControlBase)txtExpenseArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtExpenseArabicName).Clear();
		((TextEditorControlBase)txtExpenseEnglishName).Clear();
		((UltraToggleEditorBase)chkOnClientAccount).Checked = false;
		cboExpenseAccount.SelectedIndex = -1;
		cboExpenseSubAccount.SelectedIndex = -1;
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
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب المصروف" : "Expense Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnClientAccount"].Header).Caption = (GlobalVariables.IsArabic ? "على حساب العميل" : "On Client Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnClientAccount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OnClientAccount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtExpenseArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseNameAr"].Value.ToString();
		((Control)(object)txtExpenseEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseNameEn"].Value.ToString();
		((TextEditorControlBase)cboExpenseAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboExpenseSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
		((UltraToggleEditorBase)chkOnClientAccount).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OnClientAccount"].Value.ToString());
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtExpenseArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم المصروف بالعربية", "Please Insert Expense Arabic Name");
			((TextEditorControlBase)txtExpenseArabicName).Focus();
			return false;
		}
		if (cboExpenseAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم حساب المصروف", "Please Select Expense Account Name");
			((TextEditorControlBase)cboExpenseAccount).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Expenses.Insert_Update("-1", ((Control)(object)txtExpenseArabicName).Text, (((Control)(object)txtExpenseEnglishName).Text == "") ? ((Control)(object)txtExpenseArabicName).Text : ((Control)(object)txtExpenseEnglishName).Text, ((TextEditorControlBase)cboExpenseAccount).Value.ToString(), (cboExpenseSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExpenseSubAccount).Value.ToString(), ((UltraToggleEditorBase)chkOnClientAccount).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Expenses.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseID"].Value.ToString(), ((Control)(object)txtExpenseArabicName).Text, (((Control)(object)txtExpenseEnglishName).Text == "") ? "Null" : ((Control)(object)txtExpenseEnglishName).Text, ((TextEditorControlBase)cboExpenseAccount).Value.ToString(), (cboExpenseSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboExpenseSubAccount).Value.ToString(), ((UltraToggleEditorBase)chkOnClientAccount).Checked ? "1" : "0", ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Expenses.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["ExpenseID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnRefreshDataClick()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboExpenseAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboExpenseSubAccount, dtSubAccounts, "SubAccountID", "Name");
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
	}

	private void cboExpenseAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboExpenseAccount).Value = num;
			}
		}
	}

	private void btnExpenseAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num > 0)
		{
			((TextEditorControlBase)cboExpenseAccount).Value = num;
		}
	}

	private void cboExpenseAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboExpenseAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboExpenseAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboExpenseSubAccount.DataSource = dataView;
		}
	}

	private void btnExpenseSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboExpenseAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboExpenseAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboExpenseSubAccount).Value = num;
			}
		}
	}

	private void cboExpenseSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboExpenseAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboExpenseAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboExpenseSubAccount).Value = num;
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
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.MasterData.frmExpenses));
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
		this.btnExpenseAccountSearch = new UltraButton();
		this.lblExpenseEnglishName = new UltraLabel();
		this.txtExpenseEnglishName = new UltraTextEditor();
		this.cboExpenseAccount = new UltraComboEditor();
		this.lblExpenseAccount = new UltraLabel();
		this.lblExpenseArabicName = new UltraLabel();
		this.txtExpenseArabicName = new UltraTextEditor();
		this.btnExpenseSubAccountSearch = new UltraButton();
		this.cboExpenseSubAccount = new UltraComboEditor();
		this.lblExpenseSubAccount = new UltraLabel();
		this.chkOnClientAccount = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpenseEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExpenseAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpenseArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboExpenseSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkOnClientAccount).BeginInit();
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
		resources.ApplyResources(this.btnExpenseAccountSearch, "btnExpenseAccountSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance14");
		((ControlBase)this.btnExpenseAccountSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnExpenseAccountSearch).Name = "btnExpenseAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnExpenseAccountSearch).Click += new System.EventHandler(btnExpenseAccountSearch_Click);
		resources.ApplyResources(this.lblExpenseEnglishName, "lblExpenseEnglishName");
		this.lblExpenseEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpenseEnglishName).Name = "lblExpenseEnglishName";
		((ControlBase)this.lblExpenseEnglishName).WrapText = false;
		resources.ApplyResources(this.txtExpenseEnglishName, "txtExpenseEnglishName");
		resources.ApplyResources(val11, "appearance11");
		((TextEditorControlBase)this.txtExpenseEnglishName).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.txtExpenseEnglishName).Name = "txtExpenseEnglishName";
		resources.ApplyResources(this.cboExpenseAccount, "cboExpenseAccount");
		this.cboExpenseAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboExpenseAccount).Name = "cboExpenseAccount";
		((TextEditorControlBase)this.cboExpenseAccount).ValueChanged += new System.EventHandler(cboExpenseAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboExpenseAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboExpenseAccount_KeyDown);
		resources.ApplyResources(this.lblExpenseAccount, "lblExpenseAccount");
		this.lblExpenseAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpenseAccount).Name = "lblExpenseAccount";
		((ControlBase)this.lblExpenseAccount).WrapText = false;
		resources.ApplyResources(this.lblExpenseArabicName, "lblExpenseArabicName");
		this.lblExpenseArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpenseArabicName).Name = "lblExpenseArabicName";
		((ControlBase)this.lblExpenseArabicName).WrapText = false;
		resources.ApplyResources(this.txtExpenseArabicName, "txtExpenseArabicName");
		((System.Windows.Forms.Control)(object)this.txtExpenseArabicName).Name = "txtExpenseArabicName";
		resources.ApplyResources(this.btnExpenseSubAccountSearch, "btnExpenseSubAccountSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance15");
		((ControlBase)this.btnExpenseSubAccountSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnExpenseSubAccountSearch).Name = "btnExpenseSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnExpenseSubAccountSearch).Click += new System.EventHandler(btnExpenseSubAccountSearch_Click);
		resources.ApplyResources(this.cboExpenseSubAccount, "cboExpenseSubAccount");
		this.cboExpenseSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboExpenseSubAccount).Name = "cboExpenseSubAccount";
		((System.Windows.Forms.Control)(object)this.cboExpenseSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboExpenseSubAccount_KeyDown);
		resources.ApplyResources(this.lblExpenseSubAccount, "lblExpenseSubAccount");
		this.lblExpenseSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpenseSubAccount).Name = "lblExpenseSubAccount";
		((ControlBase)this.lblExpenseSubAccount).WrapText = false;
		resources.ApplyResources(this.chkOnClientAccount, "chkOnClientAccount");
		((System.Windows.Forms.Control)(object)this.chkOnClientAccount).Name = "chkOnClientAccount";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkOnClientAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExpenseSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboExpenseSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpenseSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExpenseAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpenseEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExpenseEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboExpenseAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpenseAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpenseArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExpenseArabicName);
		base.Name = "frmExpenses";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExpenseArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpenseArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpenseAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboExpenseAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExpenseEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpenseEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExpenseAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpenseSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboExpenseSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExpenseSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkOnClientAccount, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpenseEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExpenseAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpenseArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboExpenseSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkOnClientAccount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
