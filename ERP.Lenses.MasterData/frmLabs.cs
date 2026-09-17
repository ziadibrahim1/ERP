using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.MasterData;

public class frmLabs : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtBranches;

	private DataTable dtStores;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlStores = new ValueList();

	private IContainer components = null;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblCode;

	private UltraTextEditor txtCode;

	public UltraButton btnSubAccountSearch;

	private UltraComboEditor cboSubAccount;

	private UltraLabel lblSubAccount;

	public UltraButton btnAccountSearch;

	private UltraComboEditor cboAccount;

	private UltraLabel lblAccountName;

	private UltraComboEditor cboStore;

	private UltraLabel lblStore;

	private UltraComboEditor cboBranchName;

	private UltraLabel lblBranch;

	public frmLabs()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Lns_Labs";
		IDCol = "LabID";
	}

	public override void PrepareData()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add((object)dtAccounts.Rows[i]["AccountID"].ToString(), dtAccounts.Rows[i]["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
		vlSubAccounts.ValueListItems.Clear();
		for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranchName, dtBranches, "BranchID", "BranchName");
		vlBranches.ValueListItems.Clear();
		for (int k = 0; k < dtBranches.Rows.Count; k++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[k]["BranchID"], dtBranches.Rows[k]["BranchName"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		vlStores.ValueListItems.Clear();
		for (int l = 0; l < dtStores.Rows.Count; l++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[l]["StoreID"], dtStores.Rows[l]["StoreName"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranchName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboStore).ReadOnly = NavMode;
		if (Updating && cboBranchName.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " BranchID=" + ((TextEditorControlBase)cboBranchName).Value.ToString();
			GlobalFunctions.FillCombo(cboStore, dataView.ToTable(), "StoreID", "StoreName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		}
		((Control)(object)btnAccountSearch).Visible = !NavMode;
		((Control)(object)btnSubAccountSearch).Visible = !NavMode;
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((Control)(object)txtCode).Text = (Adding ? Labs.GetCode(IsFromServer: true) : "");
		cboAccount.SelectedIndex = -1;
		cboSubAccount.SelectedIndex = -1;
		cboBranchName.SelectedIndex = -1;
		cboStore.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Labs.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabCode"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب" : "Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى " : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabBranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabBranchID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabBranchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabBranchID"].ValueList = (IValueList)(object)vlBranches;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["LabCode"].Value.ToString();
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["LabNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["LabNameEn"].Value.ToString();
		((TextEditorControlBase)cboAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboSubAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value;
		((TextEditorControlBase)cboBranchName).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["LabBranchID"].Value;
		((TextEditorControlBase)cboStore).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود المعمل ", "Please Insert Lab Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم المعمل بالعربية", "Please Insert Lab Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (cboAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب المعمل", "Please Select Lab Account");
			((TextEditorControlBase)cboAccount).Focus();
			cboAccount.DropDown();
			return false;
		}
		if (cboStore.SelectedIndex == -1 && cboBranchName.SelectedIndex > -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن", "Please Select Store ");
			((TextEditorControlBase)cboStore).Focus();
			cboStore.DropDown();
			return false;
		}
		if ((Adding && dataTable.Select("LabNameAr='" + ((Control)(object)txtArabicName).Text + "'").Length != 0) || (Updating && dataTable.Select("LabNameAr='" + ((Control)(object)txtArabicName).Text + "' and LabID <> " + RowID).Length != 0))
		{
			GlobalVariables.InformationMB.Show("يوجد معمل بنفس الاسم", "A Lab with the same Name already exists");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Labs.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (cboBranchName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchName).Value.ToString(), (cboBranchName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStore).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Labs.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["LabID"].Value.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (cboBranchName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchName).Value.ToString(), (cboBranchName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStore).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Labs.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["LabID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	private void cboAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			((TextEditorControlBase)cboAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
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

	private void cboBranchName_ValueChanged(object sender, EventArgs e)
	{
		if (cboBranchName.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " BranchID=" + ((TextEditorControlBase)cboBranchName).Value.ToString();
			GlobalFunctions.FillCombo(cboStore, dataView.ToTable(), "StoreID", "StoreName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
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
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.MasterData.frmLabs));
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
		this.lblEnglishName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.btnSubAccountSearch = new UltraButton();
		this.cboSubAccount = new UltraComboEditor();
		this.lblSubAccount = new UltraLabel();
		this.btnAccountSearch = new UltraButton();
		this.cboAccount = new UltraComboEditor();
		this.lblAccountName = new UltraLabel();
		this.cboStore = new UltraComboEditor();
		this.lblStore = new UltraLabel();
		this.cboBranchName = new UltraComboEditor();
		this.lblBranch = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchName).BeginInit();
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
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((ControlBase)this.lblCode).WrapText = false;
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		((System.Windows.Forms.Control)(object)this.cboSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccount_KeyDown);
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		this.lblSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		resources.ApplyResources(this.btnAccountSearch, "btnAccountSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnAccountSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Name = "btnAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnAccountSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboAccount, "cboAccount");
		this.cboAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboAccount).Name = "cboAccount";
		((TextEditorControlBase)this.cboAccount).ValueChanged += new System.EventHandler(cboAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboAccount_KeyDown);
		resources.ApplyResources(this.lblAccountName, "lblAccountName");
		this.lblAccountName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccountName).Name = "lblAccountName";
		((ControlBase)this.lblAccountName).WrapText = false;
		resources.ApplyResources(this.cboStore, "cboStore");
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		resources.ApplyResources(this.lblStore, "lblStore");
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		((ControlBase)this.lblStore).WrapText = false;
		resources.ApplyResources(this.cboBranchName, "cboBranchName");
		this.cboBranchName.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBranchName).Name = "cboBranchName";
		((TextEditorControlBase)this.cboBranchName).ValueChanged += new System.EventHandler(cboBranchName_ValueChanged);
		resources.ApplyResources(this.lblBranch, "lblBranch");
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccountName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Name = "frmLabs";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccountName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
