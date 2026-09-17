using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.MasterData;

public class frmFixDuplicateSubAccounts : frmBase
{
	private DataTable dtSubAccounts = new DataTable();

	private DataTable dtOldAccounts = new DataTable();

	private DataTable dtNewAccounts = new DataTable();

	private DataTable dtAccountsGrid = new DataTable();

	private ValueList vlOldAccountID = new ValueList();

	private ValueList vlNewAccountID = new ValueList();

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnOldSubAccountSearch;

	private UltraComboEditor cboOldSubAccount;

	private UltraLabel lblOldSubAccount;

	public UltraButton btnNewSubAccountSearch;

	private UltraComboEditor cboNewSubAccount;

	private UltraLabel lblNewSubAccount;

	public UltraButton btnSave;

	public UltraGrid ULGData;

	public UltraButton btnClose;

	public frmFixDuplicateSubAccounts()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtSubAccounts = SubAccounts.SelectBySubAccountTypeIDs("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboOldSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboNewSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtAccountsGrid.Columns.Add("OldAccountID", typeof(int));
		dtAccountsGrid.Columns.Add("NewAccountID", typeof(int));
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DataSource = dtAccountsGrid;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OldAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب القديم" : " Old Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OldAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OldAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OldAccountID"].ValueList = (IValueList)(object)vlOldAccountID;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NewAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب الجديد" : "New Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NewAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NewAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NewAccountID"].ValueList = (IValueList)(object)vlNewAccountID;
	}

	private void cboOldSubAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboOldSubAccount.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboNewSubAccount).ValueChanged -= cboNewSubAccount_ValueChanged;
			cboNewSubAccount.SelectedIndex = -1;
			if (dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOldSubAccount).Value.ToString())[0]["SubAccountTypeID"].ToString() == "2")
			{
				DataView dataView = new DataView(dtSubAccounts);
				dataView.RowFilter = " SubAccountTypeID in (" + GlobalVariables.EmployeeSubAccountTypeIDs.Remove(GlobalVariables.EmployeeSubAccountTypeIDs.Length - 1, 1).Remove(0, 1) + ")";
				GlobalFunctions.FillCombo(cboNewSubAccount, dataView.ToTable(), "SubAccountID", "SubAccountName");
			}
			else if (dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOldSubAccount).Value.ToString())[0]["SubAccountTypeID"].ToString() == "3")
			{
				DataView dataView2 = new DataView(dtSubAccounts);
				dataView2.RowFilter = " SubAccountTypeID in (" + GlobalVariables.SupplierSubAccountTypeIDs.Remove(GlobalVariables.SupplierSubAccountTypeIDs.Length - 1, 1).Remove(0, 1) + ")";
				GlobalFunctions.FillCombo(cboNewSubAccount, dataView2.ToTable(), "SubAccountID", "SubAccountName");
			}
			else if (dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOldSubAccount).Value.ToString())[0]["SubAccountTypeID"].ToString() == "4")
			{
				DataView dataView3 = new DataView(dtSubAccounts);
				dataView3.RowFilter = " SubAccountTypeID in (" + GlobalVariables.ClientSubAccountTypeIDs.Remove(GlobalVariables.ClientSubAccountTypeIDs.Length - 1, 1).Remove(0, 1) + ")";
				GlobalFunctions.FillCombo(cboNewSubAccount, dataView3.ToTable(), "SubAccountID", "SubAccountName");
			}
			else if (dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOldSubAccount).Value.ToString())[0]["SubAccountTypeID"].ToString() == "5")
			{
				DataView dataView4 = new DataView(dtSubAccounts);
				dataView4.RowFilter = " SubAccountTypeID=5";
				GlobalFunctions.FillCombo(cboNewSubAccount, dataView4.ToTable(), "SubAccountID", "SubAccountName");
			}
			else if (dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOldSubAccount).Value.ToString())[0]["SubAccountTypeID"].ToString() == "7")
			{
				DataView dataView5 = new DataView(dtSubAccounts);
				dataView5.RowFilter = " SubAccountTypeID=7";
				GlobalFunctions.FillCombo(cboNewSubAccount, dataView5.ToTable(), "SubAccountID", "SubAccountName");
			}
			else
			{
				DataView dataView6 = new DataView(dtSubAccounts);
				dataView6.RowFilter = " SubAccountTypeID=" + dtSubAccounts.Select(" SubAccountID= " + ((TextEditorControlBase)cboOldSubAccount).Value.ToString())[0]["SubAccountTypeID"].ToString();
				GlobalFunctions.FillCombo(cboNewSubAccount, dataView6.ToTable(), "SubAccountID", "SubAccountName");
			}
			dtAccountsGrid.Rows.Clear();
			dtOldAccounts = SubAccounts_Details.SelectBySubAccountIDWithAccountName(((TextEditorControlBase)cboOldSubAccount).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlOldAccountID.ValueListItems.Clear();
			for (int i = 0; i < dtOldAccounts.Rows.Count; i++)
			{
				vlOldAccountID.ValueListItems.Add((object)dtOldAccounts.Rows[i]["AccountID"].ToString(), dtOldAccounts.Rows[i]["AccountName"].ToString());
				dtAccountsGrid.Rows.Add(dtOldAccounts.Rows[i]["AccountID"], DBNull.Value);
			}
			SelectSameAccount();
			InitGrid();
			((TextEditorControlBase)cboNewSubAccount).ValueChanged += cboNewSubAccount_ValueChanged;
		}
	}

	private void cboNewSubAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboNewSubAccount.SelectedIndex > -1)
		{
			dtNewAccounts = SubAccounts_Details.SelectBySubAccountIDWithAccountName(((TextEditorControlBase)cboNewSubAccount).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlNewAccountID.ValueListItems.Clear();
			for (int i = 0; i < dtNewAccounts.Rows.Count; i++)
			{
				vlNewAccountID.ValueListItems.Add((object)dtNewAccounts.Rows[i]["AccountID"].ToString(), dtNewAccounts.Rows[i]["AccountName"].ToString());
			}
			SelectSameAccount();
			InitGrid();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void SelectSameAccount()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (cboNewSubAccount.SelectedIndex > -1 && dtNewAccounts.Select(" AccountID = " + ((UltraGridBase)ULGData).Rows[i].Cells["OldAccountID"].Value.ToString()).Length != 0)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["NewAccountID"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["OldAccountID"].Value;
			}
			else
			{
				((UltraGridBase)ULGData).Rows[i].Cells["NewAccountID"].Value = DBNull.Value;
			}
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OldAccountID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (Main.IsSynchronization)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن مسح التكرار بسبب التزامن" : "Cannot Fix The Duplicate Because Of The Synchronization");
			return;
		}
		Main.StartBulkTrans(FromServer: true);
		try
		{
			SubAccounts.Replace(((TextEditorControlBase)cboOldSubAccount).Value.ToString(), ((TextEditorControlBase)cboNewSubAccount).Value.ToString(), (DataTable)((UltraGridBase)ULGData).DataSource, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			dtSubAccounts = SubAccounts.SelectBySubAccountTypeIDs("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboOldSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
			GlobalFunctions.FillCombo(cboNewSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
			dtAccountsGrid.Rows.Clear();
			cboNewSubAccount.SelectedIndex = -1;
			cboOldSubAccount.SelectedIndex = -1;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "تم الحفظ بنجاح " : "Data Saved Successfuly");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnOldSubAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SubAccounts("-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboOldSubAccount).Value = num;
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.MasterData.frmFixDuplicateSubAccounts));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnOldSubAccountSearch = new UltraButton();
		this.cboOldSubAccount = new UltraComboEditor();
		this.lblOldSubAccount = new UltraLabel();
		this.btnNewSubAccountSearch = new UltraButton();
		this.cboNewSubAccount = new UltraComboEditor();
		this.lblNewSubAccount = new UltraLabel();
		this.btnSave = new UltraButton();
		this.ULGData = new UltraGrid();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOldSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNewSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance7");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance8");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnOldSubAccountSearch, "btnOldSubAccountSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance9");
		((ControlBase)this.btnOldSubAccountSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnOldSubAccountSearch).Name = "btnOldSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnOldSubAccountSearch).Click += new System.EventHandler(btnOldSubAccountSearch_Click);
		resources.ApplyResources(this.cboOldSubAccount, "cboOldSubAccount");
		this.cboOldSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboOldSubAccount).Name = "cboOldSubAccount";
		((TextEditorControlBase)this.cboOldSubAccount).ValueChanged += new System.EventHandler(cboOldSubAccount_ValueChanged);
		resources.ApplyResources(this.lblOldSubAccount, "lblOldSubAccount");
		this.lblOldSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOldSubAccount).Name = "lblOldSubAccount";
		((ControlBase)this.lblOldSubAccount).WrapText = false;
		resources.ApplyResources(this.btnNewSubAccountSearch, "btnNewSubAccountSearch");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val4, "appearance10");
		((ControlBase)this.btnNewSubAccountSearch).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnNewSubAccountSearch).Name = "btnNewSubAccountSearch";
		resources.ApplyResources(this.cboNewSubAccount, "cboNewSubAccount");
		this.cboNewSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboNewSubAccount).Name = "cboNewSubAccount";
		((TextEditorControlBase)this.cboNewSubAccount).ValueChanged += new System.EventHandler(cboNewSubAccount_ValueChanged);
		resources.ApplyResources(this.lblNewSubAccount, "lblNewSubAccount");
		this.lblNewSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNewSubAccount).Name = "lblNewSubAccount";
		((ControlBase)this.lblNewSubAccount).WrapText = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val5).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val5, "appearance11");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val6).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val6, "appearance12");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNewSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOldSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOldSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOldSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmFixDuplicateSubAccounts";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOldSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOldSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOldSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNewSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOldSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNewSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
