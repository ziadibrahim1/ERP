using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmTaxs : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtEINVTaxes;

	private ValueList vlEINVTaxes = new ValueList();

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private bool UsingElectronicInvoice = false;

	private bool UseSubAccounts;

	private IContainer components = null;

	private UltraComboEditor cboTaxSubAccount;

	private UltraLabel lblTaxSubAccount;

	private UltraLabel lblTaxEnglishName;

	private UltraTextEditor txtTaxEnglishName;

	private UltraComboEditor cboTaxAccount;

	private UltraLabel lblTaxAccount;

	private UltraLabel lblTaxArabicName;

	private UltraTextEditor txtTaxArabicName;

	public UltraButton btnTaxAccountSearch;

	public UltraButton btnTaxSubAccountSearch;

	private UltraLabel lblTaxPercent;

	private UltraTextEditor txtTaxPercent;

	private UltraLabel lblEINVTax;

	private UltraComboEditor cboEINVTax;

	public frmTaxs()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		TableName = "G_Taxs";
		IDCol = "TaxID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTaxAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboTaxSubAccount, dtSubAccounts, "SubAccountID", "Name");
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		if (UsingElectronicInvoice)
		{
			dtEINVTaxes = BusinessLayer.EInvoices.Taxs.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVTax, dtEINVTaxes, "EINVTaxID", "EINVTaxName");
			vlEINVTaxes.ValueListItems.Clear();
			for (int k = 0; k < dtEINVTaxes.Rows.Count; k++)
			{
				vlEINVTaxes.ValueListItems.Add((object)dtEINVTaxes.Rows[k]["EINVTaxID"].ToString(), dtEINVTaxes.Rows[k]["EINVTaxName"].ToString());
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtTaxArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTaxAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTaxSubAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaxPercent).ReadOnly = NavMode;
		((Control)(object)btnTaxAccountSearch).Visible = !NavMode;
		((Control)(object)lblTaxSubAccount).Visible = UseSubAccounts;
		((Control)(object)cboTaxSubAccount).Visible = UseSubAccounts;
		((Control)(object)btnTaxSubAccountSearch).Visible = !NavMode && UseSubAccounts;
		UltraLabel obj = lblEINVTax;
		bool visible = (((Control)(object)cboEINVTax).Visible = UsingElectronicInvoice);
		((Control)(object)obj).Visible = visible;
		((TextEditorControlBase)txtTaxArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtTaxArabicName).Clear();
		((TextEditorControlBase)txtTaxEnglishName).Clear();
		cboTaxAccount.SelectedIndex = -1;
		cboTaxSubAccount.SelectedIndex = -1;
		cboEINVTax.SelectedIndex = -1;
		((TextEditorControlBase)txtTaxPercent).Clear();
	}

	public override void FillData()
	{
		dataTable = BusinessLayer.General.Taxs.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الضريبة" : "Tax Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (UsingElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.2)) : ((int)((double)((Control)(object)ULGData).Width * 0.25)));
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى للضريبة" : "Tax SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (UsingElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.2)) : ((int)((double)((Control)(object)ULGData).Width * 0.25)));
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الضريبة" : "Tax Percent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxPercent"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxPercent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxID"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفاتورة الالكترونية" : "E-Invoice Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxID"].ValueList = (IValueList)(object)vlEINVTaxes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVTaxID"].Hidden = !UsingElectronicInvoice;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtTaxArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TaxNameAr"].Value.ToString();
		((Control)(object)txtTaxEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TaxNameEn"].Value.ToString();
		((TextEditorControlBase)cboTaxAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboTaxSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
		((TextEditorControlBase)cboEINVTax).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EINVTaxID"].Value;
		((Control)(object)txtTaxPercent).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TaxPercent"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtTaxArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم الضريبة بالعربية", "Please Insert Tax Arabic Name");
			((TextEditorControlBase)txtTaxArabicName).Focus();
			return false;
		}
		if (cboTaxAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم حساب الضريبة", "Please Select Tax Account Name");
			((TextEditorControlBase)cboTaxAccount).Focus();
			return false;
		}
		if (UsingElectronicInvoice && cboEINVTax.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار كود الضريبة", "Please Select Tax Code");
			((TextEditorControlBase)cboEINVTax).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		BusinessLayer.General.Taxs.Insert_Update("-1", (cboEINVTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVTax).Value.ToString(), ((Control)(object)txtTaxArabicName).Text, (((Control)(object)txtTaxEnglishName).Text == "") ? "Null" : ((Control)(object)txtTaxEnglishName).Text, ((TextEditorControlBase)cboTaxAccount).Value.ToString(), (cboTaxSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTaxSubAccount).Value.ToString(), (((Control)(object)txtTaxPercent).Text == "") ? "0" : ((Control)(object)txtTaxPercent).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		BusinessLayer.General.Taxs.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value.ToString(), (cboEINVTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVTax).Value.ToString(), ((Control)(object)txtTaxArabicName).Text, (((Control)(object)txtTaxEnglishName).Text == "") ? "Null" : ((Control)(object)txtTaxEnglishName).Text, ((TextEditorControlBase)cboTaxAccount).Value.ToString(), (cboTaxSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTaxSubAccount).Value.ToString(), (((Control)(object)txtTaxPercent).Text == "") ? "0" : ((Control)(object)txtTaxPercent).Text, GlobalVariables.CurrentBranchID, ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		BusinessLayer.General.Taxs.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["TaxID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnRefreshDataClick()
	{
		UsingElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTaxAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboTaxSubAccount, dtSubAccounts, "SubAccountID", "Name");
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		if (UsingElectronicInvoice)
		{
			dtEINVTaxes = BusinessLayer.EInvoices.Taxs.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVTax, dtEINVTaxes, "EINVTaxID", "EINVTaxName");
			vlEINVTaxes.ValueListItems.Clear();
			for (int k = 0; k < dtEINVTaxes.Rows.Count; k++)
			{
				vlEINVTaxes.ValueListItems.Add((object)dtEINVTaxes.Rows[k]["EINVTaxID"].ToString(), dtEINVTaxes.Rows[k]["EINVTaxName"].ToString());
			}
		}
	}

	private void cboTaxAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboTaxAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboTaxAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboTaxSubAccount.DataSource = dataView;
		}
	}

	private void btnTaxAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboTaxAccount).Value = num;
		}
	}

	private void btnTaxSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboTaxAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboTaxAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboTaxSubAccount).Value = num;
			}
		}
	}

	private void cboTaxSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboTaxAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboTaxAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboTaxSubAccount).Value = num;
			}
		}
	}

	private void cboTaxAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboTaxAccount).Value = num;
			}
		}
	}

	private void txtTaxPercent_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmTaxs));
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
		this.cboTaxSubAccount = new UltraComboEditor();
		this.lblTaxSubAccount = new UltraLabel();
		this.lblTaxEnglishName = new UltraLabel();
		this.txtTaxEnglishName = new UltraTextEditor();
		this.cboTaxAccount = new UltraComboEditor();
		this.lblTaxAccount = new UltraLabel();
		this.lblTaxArabicName = new UltraLabel();
		this.txtTaxArabicName = new UltraTextEditor();
		this.btnTaxAccountSearch = new UltraButton();
		this.btnTaxSubAccountSearch = new UltraButton();
		this.lblTaxPercent = new UltraLabel();
		this.txtTaxPercent = new UltraTextEditor();
		this.lblEINVTax = new UltraLabel();
		this.cboEINVTax = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTaxSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTaxAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxPercent).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVTax).BeginInit();
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
		resources.ApplyResources(this.cboTaxSubAccount, "cboTaxSubAccount");
		this.cboTaxSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTaxSubAccount).Name = "cboTaxSubAccount";
		((System.Windows.Forms.Control)(object)this.cboTaxSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboTaxSubAccount_KeyDown);
		resources.ApplyResources(this.lblTaxSubAccount, "lblTaxSubAccount");
		this.lblTaxSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxSubAccount).Name = "lblTaxSubAccount";
		((ControlBase)this.lblTaxSubAccount).WrapText = false;
		resources.ApplyResources(this.lblTaxEnglishName, "lblTaxEnglishName");
		this.lblTaxEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxEnglishName).Name = "lblTaxEnglishName";
		((ControlBase)this.lblTaxEnglishName).WrapText = false;
		resources.ApplyResources(this.txtTaxEnglishName, "txtTaxEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtTaxEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtTaxEnglishName).Name = "txtTaxEnglishName";
		resources.ApplyResources(this.cboTaxAccount, "cboTaxAccount");
		this.cboTaxAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTaxAccount).Name = "cboTaxAccount";
		((TextEditorControlBase)this.cboTaxAccount).ValueChanged += new System.EventHandler(cboTaxAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboTaxAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboTaxAccount_KeyDown);
		resources.ApplyResources(this.lblTaxAccount, "lblTaxAccount");
		this.lblTaxAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxAccount).Name = "lblTaxAccount";
		((ControlBase)this.lblTaxAccount).WrapText = false;
		resources.ApplyResources(this.lblTaxArabicName, "lblTaxArabicName");
		this.lblTaxArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxArabicName).Name = "lblTaxArabicName";
		((ControlBase)this.lblTaxArabicName).WrapText = false;
		resources.ApplyResources(this.txtTaxArabicName, "txtTaxArabicName");
		((System.Windows.Forms.Control)(object)this.txtTaxArabicName).Name = "txtTaxArabicName";
		resources.ApplyResources(this.btnTaxAccountSearch, "btnTaxAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnTaxAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnTaxAccountSearch).Name = "btnTaxAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnTaxAccountSearch).Click += new System.EventHandler(btnTaxAccountSearch_Click);
		resources.ApplyResources(this.btnTaxSubAccountSearch, "btnTaxSubAccountSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnTaxSubAccountSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnTaxSubAccountSearch).Name = "btnTaxSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnTaxSubAccountSearch).Click += new System.EventHandler(btnTaxSubAccountSearch_Click);
		resources.ApplyResources(this.lblTaxPercent, "lblTaxPercent");
		this.lblTaxPercent.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxPercent).Name = "lblTaxPercent";
		((ControlBase)this.lblTaxPercent).WrapText = false;
		resources.ApplyResources(this.txtTaxPercent, "txtTaxPercent");
		((System.Windows.Forms.Control)(object)this.txtTaxPercent).Name = "txtTaxPercent";
		((System.Windows.Forms.Control)(object)this.txtTaxPercent).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTaxPercent_KeyPress);
		resources.ApplyResources(this.lblEINVTax, "lblEINVTax");
		this.lblEINVTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEINVTax).Name = "lblEINVTax";
		((ControlBase)this.lblEINVTax).WrapText = false;
		resources.ApplyResources(this.cboEINVTax, "cboEINVTax");
		this.cboEINVTax.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboEINVTax).Name = "cboEINVTax";
		((TextEditorControlBase)this.cboEINVTax).ValueChanged += new System.EventHandler(cboTaxAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboEINVTax).KeyDown += new System.Windows.Forms.KeyEventHandler(cboTaxAccount_KeyDown);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxPercent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxPercent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnTaxSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnTaxAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTaxSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTaxAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxArabicName);
		base.Name = "frmTaxs";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTaxAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEINVTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEINVTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTaxSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnTaxAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnTaxSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxPercent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxPercent, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTaxSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTaxAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxPercent).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVTax).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
