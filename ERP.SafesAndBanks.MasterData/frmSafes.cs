using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.SafesAndBanks;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.MasterData;

public class frmSafes : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtSafeCode;

	private UltraLabel lblSafeCode;

	private UltraLabel lblSafeNameAr;

	private UltraTextEditor txtSafeNameAr;

	private UltraTextEditor txtSafeNameEn;

	private UltraLabel lblSafeNameEn;

	private UltraLabel lblAccountName;

	private UltraComboEditor cboSafeAccount;

	private UltraTextEditor txtSafeMinimumLimit;

	private UltraLabel lblSafeMinimumLimit;

	public UltraButton btnSafeAccountSearch;

	public UltraButton btnSafeSubAccountSearch;

	private UltraComboEditor cboSafeSubAccount;

	private UltraLabel lblSafeSubAccount;

	public frmSafes()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SB_Safes";
		IDCol = "SafeID";
	}

	public override void PrepareData()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSafeAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSafeSubAccount, dtSubAccounts, "SubAccountID", "Name");
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
		((EditorButtonControlBase)txtSafeCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSafeNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSafeNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSafeMinimumLimit).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSafeAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSafeSubAccount).ReadOnly = NavMode;
		((Control)(object)btnSafeAccountSearch).Visible = !NavMode;
		((Control)(object)btnSafeSubAccountSearch).Visible = !NavMode;
		((TextEditorControlBase)txtSafeCode).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtSafeCode).Text = (Adding ? Safes.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtSafeNameAr).Clear();
		((TextEditorControlBase)txtSafeNameEn).Clear();
		((Control)(object)txtSafeMinimumLimit).Text = "0";
		cboSafeAccount.SelectedIndex = -1;
		cboSafeSubAccount.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Safes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الخزينة" : "Safe Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الخزينة" : "Safe Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى " : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeMinimumLimit"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الإدنى" : "Minimum level");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeMinimumLimit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeMinimumLimit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeMinimumLimit"].DefaultCellValue = 0;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtSafeCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SafeCode"].Value.ToString();
		((Control)(object)txtSafeNameAr).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SafeNameAr"].Value.ToString();
		((Control)(object)txtSafeNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SafeNameEn"].Value.ToString();
		((Control)(object)txtSafeMinimumLimit).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SafeMinimumLimit"].Value.ToString();
		((TextEditorControlBase)cboSafeAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboSafeSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtSafeCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود الخزينة", "Please Insert Safe Code");
			((TextEditorControlBase)txtSafeCode).Focus();
			return false;
		}
		if (((Control)(object)txtSafeNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم الخزينة بالعربية", "Please Insert Safe Arabic Name");
			((TextEditorControlBase)txtSafeNameAr).Focus();
			return false;
		}
		if (cboSafeAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم الحساب", "Please Select Account Name");
			((TextEditorControlBase)cboSafeAccount).Focus();
			cboSafeAccount.DropDown();
			return false;
		}
		if (cboSafeSubAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار إسم الحساب التحليلى", "Please Select SubAccount Name");
			((TextEditorControlBase)cboSafeSubAccount).Focus();
			cboSafeSubAccount.DropDown();
			return false;
		}
		DataRow[] array = dataTable.Select(" SafeNameAr= '" + ((Control)(object)txtSafeNameAr).Text + "'");
		if (array.Length != 0 && (Adding || array[0]["SafeID"].ToString() != ((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الاسم موجود من قبل", "This Name is Already Exists..");
			return false;
		}
		array = dataTable.Select(" SubAccountID= " + ((TextEditorControlBase)cboSafeSubAccount).Value.ToString());
		if (array.Length != 0 && (Adding || array[0]["SubAccountID"].ToString() != ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value.ToString()))
		{
			GlobalVariables.InformationMB.Show("هذا الحساب التحليلى موجود من قبل", "This SubAccount is Already Exists..");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Safes.Insert_Update("-1", ((Control)(object)txtSafeCode).Text, ((Control)(object)txtSafeNameAr).Text, (((Control)(object)txtSafeNameEn).Text == "") ? "Null" : ((Control)(object)txtSafeNameEn).Text, ((TextEditorControlBase)cboSafeAccount).Value.ToString(), ((TextEditorControlBase)cboSafeSubAccount).Value.ToString(), (((Control)(object)txtSafeMinimumLimit).Text == "") ? "0" : ((Control)(object)txtSafeMinimumLimit).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Safes.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value.ToString(), ((Control)(object)txtSafeCode).Text, ((Control)(object)txtSafeNameAr).Text, (((Control)(object)txtSafeNameEn).Text == "") ? "Null" : ((Control)(object)txtSafeNameEn).Text, ((TextEditorControlBase)cboSafeAccount).Value.ToString(), ((TextEditorControlBase)cboSafeSubAccount).Value.ToString(), (((Control)(object)txtSafeMinimumLimit).Text == "") ? "0" : ((Control)(object)txtSafeMinimumLimit).Text, GlobalVariables.CurrentBranchID, ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnUpdateClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (SafeIn.SelectBySafeID(((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value.ToString(), "1").Rows.Count > 0 || SafeOut.SelectBySafeID(((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value.ToString(), "1").Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الخزينة لقد تمت حركات عليه فى النظام", "You Can not Update This Safe There Are Many TransAction in The System");
			}
			else
			{
				base.btnUpdateClick();
			}
		}
	}

	public override void btnRefreshDataClick()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSafeAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSafeSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlSubAccounts.ValueListItems.Clear();
		for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
		}
	}

	public override void DeleteData()
	{
		Safes.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	private void txtSafeMinimumLimit_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void cboSafeAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			((TextEditorControlBase)cboSafeAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
	}

	private void btnSafeAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboSafeAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
	}

	private void cboSafeAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboSafeAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboSafeAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSafeSubAccount.DataSource = dataView;
		}
	}

	private void btnSafeSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboSafeAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboSafeAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSafeSubAccount).Value = num;
			}
		}
	}

	private void cboSafeSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboSafeAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboSafeAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSafeSubAccount).Value = num;
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.MasterData.frmSafes));
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
		this.txtSafeCode = new UltraTextEditor();
		this.lblSafeCode = new UltraLabel();
		this.lblSafeNameAr = new UltraLabel();
		this.txtSafeNameAr = new UltraTextEditor();
		this.txtSafeNameEn = new UltraTextEditor();
		this.lblSafeNameEn = new UltraLabel();
		this.lblAccountName = new UltraLabel();
		this.cboSafeAccount = new UltraComboEditor();
		this.txtSafeMinimumLimit = new UltraTextEditor();
		this.lblSafeMinimumLimit = new UltraLabel();
		this.btnSafeAccountSearch = new UltraButton();
		this.btnSafeSubAccountSearch = new UltraButton();
		this.cboSafeSubAccount = new UltraComboEditor();
		this.lblSafeSubAccount = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafeAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeMinimumLimit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafeSubAccount).BeginInit();
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
		resources.ApplyResources(val8, "appearance12");
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
		resources.ApplyResources(this.txtSafeCode, "txtSafeCode");
		((System.Windows.Forms.Control)(object)this.txtSafeCode).Name = "txtSafeCode";
		resources.ApplyResources(this.lblSafeCode, "lblSafeCode");
		this.lblSafeCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafeCode).Name = "lblSafeCode";
		((ControlBase)this.lblSafeCode).WrapText = false;
		resources.ApplyResources(this.lblSafeNameAr, "lblSafeNameAr");
		this.lblSafeNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafeNameAr).Name = "lblSafeNameAr";
		((ControlBase)this.lblSafeNameAr).WrapText = false;
		resources.ApplyResources(this.txtSafeNameAr, "txtSafeNameAr");
		((System.Windows.Forms.Control)(object)this.txtSafeNameAr).Name = "txtSafeNameAr";
		resources.ApplyResources(this.txtSafeNameEn, "txtSafeNameEn");
		((System.Windows.Forms.Control)(object)this.txtSafeNameEn).Name = "txtSafeNameEn";
		resources.ApplyResources(this.lblSafeNameEn, "lblSafeNameEn");
		this.lblSafeNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafeNameEn).Name = "lblSafeNameEn";
		((ControlBase)this.lblSafeNameEn).WrapText = false;
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		this.lblAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		resources.ApplyResources(this.cboSafeAccount, "cboSafeAccount");
		this.cboSafeAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSafeAccount).Name = "cboSafeAccount";
		((TextEditorControlBase)this.cboSafeAccount).ValueChanged += new System.EventHandler(cboSafeAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboSafeAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSafeAccount_KeyDown);
		resources.ApplyResources(this.txtSafeMinimumLimit, "txtSafeMinimumLimit");
		((System.Windows.Forms.Control)(object)this.txtSafeMinimumLimit).Name = "txtSafeMinimumLimit";
		((System.Windows.Forms.Control)(object)this.txtSafeMinimumLimit).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSafeMinimumLimit_KeyPress);
		resources.ApplyResources(this.lblSafeMinimumLimit, "lblSafeMinimumLimit");
		this.lblSafeMinimumLimit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafeMinimumLimit).Name = "lblSafeMinimumLimit";
		((ControlBase)this.lblSafeMinimumLimit).WrapText = false;
		resources.ApplyResources(this.btnSafeAccountSearch, "btnSafeAccountSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance13");
		((ControlBase)this.btnSafeAccountSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnSafeAccountSearch).Name = "btnSafeAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSafeAccountSearch).Click += new System.EventHandler(btnSafeAccountSearch_Click);
		resources.ApplyResources(this.btnSafeSubAccountSearch, "btnSafeSubAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.btnSafeSubAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnSafeSubAccountSearch).Name = "btnSafeSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSafeSubAccountSearch).Click += new System.EventHandler(btnSafeSubAccountSearch_Click);
		resources.ApplyResources(this.cboSafeSubAccount, "cboSafeSubAccount");
		this.cboSafeSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSafeSubAccount).Name = "cboSafeSubAccount";
		((System.Windows.Forms.Control)(object)this.cboSafeSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSafeSubAccount_KeyDown);
		resources.ApplyResources(this.lblSafeSubAccount, "lblSafeSubAccount");
		this.lblSafeSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSafeSubAccount).Name = "lblSafeSubAccount";
		((ControlBase)this.lblSafeSubAccount).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSafeSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSafeSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafeSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSafeAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSafeMinimumLimit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafeMinimumLimit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSafeAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSafeNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafeNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSafeNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafeNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSafeCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSafeCode);
		base.Name = "frmSafes";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafeCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSafeCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafeNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSafeNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafeNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSafeNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSafeAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafeMinimumLimit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSafeMinimumLimit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSafeAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSafeSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSafeSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSafeSubAccountSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafeAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSafeMinimumLimit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSafeSubAccount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
