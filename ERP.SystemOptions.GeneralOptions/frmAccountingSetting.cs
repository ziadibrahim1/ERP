using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.SystemOptions.GeneralOptions;

public class frmAccountingSetting : frmBase
{
	private DataTable dtJVDefaults;

	private DataTable dtJournalTypes;

	private DataTable dtJVTypes;

	private DataTable dtSystemOptions;

	private DataTable dtSystemAccounts;

	private DataTable dtAccountTypes;

	private DataTable dtSubAccounts;

	private ValueList vlAccountID = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlSubaccountID = new ValueList();

	private ValueList vlJournalTypes = new ValueList();

	private ValueList vlJVTypes = new ValueList();

	private IContainer components = null;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage2;

	private UltraTabPageControl ultraTabPageControl2;

	private UltraPictureBox ultraPictureBox1;

	private UltraButton ultraButton1;

	private UltraLabel ultraLabel1;

	private UltraButton ultraButton2;

	private UltraTextEditor ultraTextEditor1;

	private UltraTextEditor ultraTextEditor2;

	private UltraLabel ultraLabel6;

	private UltraButton ultraButton3;

	private UltraPictureBox ultraPictureBox2;

	private UltraLabel ultraLabel7;

	private UltraPictureBox ultraPictureBox3;

	private UltraButton ultraButton4;

	private UltraButton ultraButton5;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel9;

	private UltraTabPageControl ultraTabPageControl7;

	private UltraCheckEditor ultraCheckEditor1;

	private UltraGroupBox ultraGroupBox1;

	private UltraDateTimeEditor ultraDateTimeEditor1;

	private UltraLabel ultraLabel10;

	private NumericUpDown numericUpDown1;

	private UltraLabel ultraLabel11;

	private UltraGroupBox ultraGroupBox2;

	private UltraLabel ultraLabel12;

	private UltraCheckEditor ultraCheckEditor2;

	private UltraDateTimeEditor ultraDateTimeEditor2;

	private UltraDateTimeEditor ultraDateTimeEditor3;

	private UltraLabel ultraLabel13;

	private UltraGroupBox ultraGroupBox3;

	private RadioButton radioButton1;

	private RadioButton radioButton2;

	private RadioButton radioButton3;

	private UltraTabPageControl ultraTabPageControl8;

	public UltraGrid ultraGrid1;

	private UltraTabPageControl ultraTabPageControl9;

	public UltraGrid ultraGrid2;

	private UltraTabPageControl ultraTabPageControl10;

	public UltraGrid ultraGrid3;

	private UltraTabPageControl ultraTabPageControl4;

	public UltraGrid ULGJVTransTypes;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabControl tcSystemDefaults;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGSysOption;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGSysAcc;

	public UltraButton btnCancel;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public frmAccountingSetting()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		InitializeComponent();
	}

	public void InitGrids()
	{
		dtJVDefaults.Columns["TransTypeNameAr"].ReadOnly = true;
		dtJVDefaults.Columns["TransTypeNameEn"].ReadOnly = true;
		((UltraGridBase)ULGJVTransTypes).DataSource = dtJVDefaults;
		GlobalFunctions.PrepareGrid(ULGJVTransTypes);
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNumber"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Code");
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNameAr"].Hidden = !GlobalVariables.IsArabic;
		((HeaderBase)((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNameAr"].Header).Caption = "نوع الحركة بالعربيه";
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNameEn"].Hidden = GlobalVariables.IsArabic;
		((HeaderBase)((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNameEn"].Header).Caption = "Transaction Type English";
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JournalTypeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JournalTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع اليومي\u0651ة" : "Journal Type");
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JournalTypeID"].ValueList = (IValueList)(object)vlJournalTypes;
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JVTypeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JVTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع المستند" : "JV Type");
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JVTypeID"].ValueList = (IValueList)(object)vlJVTypes;
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNumber"].Width = (int)((double)((Control)(object)ULGJVTransTypes).Width * 0.16) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNameAr"].Width = (int)((double)((Control)(object)ULGJVTransTypes).Width * 0.28);
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["TransTypeNameEn"].Width = (int)((double)((Control)(object)ULGJVTransTypes).Width * 0.28);
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JournalTypeID"].Width = (int)((double)((Control)(object)ULGJVTransTypes).Width * 0.28);
		((UltraGridBase)ULGJVTransTypes).DisplayLayout.Bands[0].Columns["JVTypeID"].Width = (int)((double)((Control)(object)ULGJVTransTypes).Width * 0.28);
	}

	public void InitGridsSysOption()
	{
		((UltraGridBase)ULGSysOption).DataSource = dtSystemOptions;
		GlobalFunctions.PrepareGrid(ULGSysOption);
		((UltraGridBase)ULGSysOption).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Header).Caption = (GlobalVariables.IsArabic ? "" : "");
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Width = (int)((double)((Control)(object)ULGSysOption).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGSysOption).Width * 0.6);
	}

	public void InitGridsSystemAccounts()
	{
		((UltraGridBase)ULGSysAcc).DataSource = dtSystemAccounts;
		GlobalFunctions.PrepareGrid(ULGSysAcc);
		((UltraGridBase)ULGSysAcc).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الحساب" : "AccountName");
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحساب" : "AccountNumber");
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccountID;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحساب التحليلي" : "SubAccountNumber");
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountName"].Width = (int)((double)((Control)(object)ULGSysAcc).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGSysAcc).Width * 0.3);
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGSysAcc).Width * 0.3);
	}

	private void frmTransactioTypes_Load(object sender, EventArgs e)
	{
		FillData();
	}

	private void FillData()
	{
		dtJVDefaults = JVDefaults.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtJournalTypes = JournalTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		foreach (DataRow row in dtJournalTypes.Rows)
		{
			vlJournalTypes.ValueListItems.Add(row["JournalTypeID"], row["JournalName"].ToString());
		}
		dtJVTypes = JVTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		foreach (DataRow row2 in dtJVTypes.Rows)
		{
			vlJVTypes.ValueListItems.Add(row2["JVTypeID"], row2["JVTypeName"].ToString());
		}
		dtSystemOptions = BusinessLayer.Defaults.SystemOptions.SelectForModule("100", "200", GlobalVariables.IsArabic ? "1" : "0");
		dtSystemAccounts = SystemAccount.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtAccountTypes = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		foreach (DataRow row3 in dtAccountTypes.Rows)
		{
			vlAccountID.ValueListItems.Add(row3["AccountID"], row3["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtSubAccounts.Rows.Count; i++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[i]["SubAccountID"], dtSubAccounts.Rows[i]["Name"].ToString());
		}
		InitGrids();
		InitGridsSysOption();
		InitGridsSystemAccounts();
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysAcc).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGSysAcc).Rows[j].Cells["AccountID"].Value != DBNull.Value)
			{
				int accountID = int.Parse(((UltraGridBase)ULGSysAcc).Rows[j].Cells["AccountID"].Value.ToString());
				ValueList subAccountValueList = getSubAccountValueList(accountID);
				((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
				if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].Value = DBNull.Value;
				}
			}
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		FillData();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			UpdateData();
			FillData();
		}
	}

	private void UpdateData()
	{
		foreach (DataRow row in dtJVDefaults.Rows)
		{
			JVDefaults.Insert_Update(row["TransTypeID"].ToString(), row["TransTypeNumber"].ToString(), row["TransTypeNameAr"].ToString(), row["TransTypeNameEn"].ToString(), row["JournalTypeID"].ToString(), row["JVTypeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
		}
		BusinessLayer.Defaults.SystemOptions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGSysOption).DataSource, GlobalVariables.UserID);
		SystemAccount.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGSysAcc).DataSource, GlobalVariables.UserID, IsFromServer: true);
		GlobalVariables.InformationMB.Show("تــم تخزين البيانات بنجــــاح", "Updates were saved successfully");
		GlobalVariables.dtSystemOptions = BusinessLayer.Defaults.SystemOptions.Select("-1", GlobalVariables.IsArabic ? "1" : "0");
		GlobalVariables.dtSystemAccounts = SystemAccount.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	private void ULGSysOption_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGSysOption.ActiveCell.Column).Key == "OptionEnName" || (bool)((UltraGridBase)ULGSysOption).ActiveRow.Cells["ReadOnly"].Value)
		{
			((GridItemBase)((UltraGridBase)ULGSysOption).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGSysOption.ActiveCell.Column).Key == "Description")
		{
			((GridItemBase)((UltraGridBase)ULGSysOption).ActiveRow).Selected = true;
		}
	}

	private void ULGSysOption_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		ULGSysOption.CellChange -= new CellEventHandler(ULGSysOption_CellChange);
		((UltraGridBase)ULGSysOption).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "OptionValue" && (e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "StokControl(Average)" || e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "LifoStockControl" || e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "FIFOStockControl") && (bool)e.Cell.Value)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysOption).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "StokControl(Average)" || ((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "LifoStockControl" || ((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "FIFOStockControl")
				{
					((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionValue"].Value = false;
				}
			}
			e.Cell.Value = true;
		}
		ULGSysOption.CellChange += new CellEventHandler(ULGSysOption_CellChange);
	}

	private void ULGSysAcc_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGSysAcc.ActiveCell.Column).Key == "AccountName" || (!bool.Parse(((UltraGridBase)ULGSysAcc).ActiveRow.Cells["HasSubAccount"].Value.ToString()) && ((KeyedSubObjectBase)ULGSysAcc.ActiveCell.Column).Key == "SubAccountID"))
		{
			((GridItemBase)((UltraGridBase)ULGSysAcc).ActiveRow).Selected = true;
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysAcc).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGSysAcc).Rows[i].Cells["HasSubAccount"].Value.ToString()) && ((UltraGridBase)ULGSysAcc).Rows[i].Cells["AccountID"].Value == DBNull.Value && ((UltraGridBase)ULGSysAcc).Rows[i].Cells["SubAccountID"].Value != DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار الحساب لهذا الحساب التحليلي", "Please Select An Account For This SubAccount");
				ULGSysAcc.ActiveCell = ((UltraGridBase)ULGSysAcc).Rows[i].Cells["AccountID"];
				ULGSysAcc.PerformAction((UltraGridAction)24);
				return false;
			}
			if (bool.Parse(((UltraGridBase)ULGSysAcc).Rows[i].Cells["HasSubAccount"].Value.ToString()) && ((UltraGridBase)ULGSysAcc).Rows[i].Cells["SubAccountID"].ValueList != null && ((UltraGridBase)ULGSysAcc).Rows[i].Cells["SubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGSysAcc).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("هذا الحساب مربوط بحسابات تحليلية برجاء إختيار حساب تحليلي", "This Account Has SubAccounts Please Choose SubAccount");
				ULGSysAcc.ActiveCell = ((UltraGridBase)ULGSysAcc).Rows[i].Cells["SubAccountID"];
				ULGSysAcc.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		return true;
	}

	private void ULGSysAcc_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID")
		{
			int num = ((vlAccountID.SelectedItem != null) ? int.Parse(dtAccountTypes.Rows[vlAccountID.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["SubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["SubAccountID"].ValueList = null;
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["CostCenterID"].Value = DBNull.Value;
			}
		}
	}

	private ValueList getSubAccountValueList(int AccountID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtSubAccounts.Select("AccountID=" + AccountID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
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
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Expected O, but got Unknown
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected O, but got Unknown
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Expected O, but got Unknown
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Expected O, but got Unknown
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Expected O, but got Unknown
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Expected O, but got Unknown
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Expected O, but got Unknown
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Expected O, but got Unknown
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Expected O, but got Unknown
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Expected O, but got Unknown
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Expected O, but got Unknown
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected O, but got Unknown
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Expected O, but got Unknown
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Expected O, but got Unknown
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d7: Expected O, but got Unknown
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Expected O, but got Unknown
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Expected O, but got Unknown
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Expected O, but got Unknown
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Expected O, but got Unknown
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Expected O, but got Unknown
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Expected O, but got Unknown
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Expected O, but got Unknown
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Expected O, but got Unknown
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Expected O, but got Unknown
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Expected O, but got Unknown
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Expected O, but got Unknown
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Expected O, but got Unknown
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Expected O, but got Unknown
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Expected O, but got Unknown
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Expected O, but got Unknown
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Expected O, but got Unknown
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Expected O, but got Unknown
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab7: Expected O, but got Unknown
		//IL_0efe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f08: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralOptions.frmAccountingSetting));
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
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		Appearance val57 = new Appearance();
		Appearance val58 = new Appearance();
		Appearance val59 = new Appearance();
		Appearance val60 = new Appearance();
		Appearance val61 = new Appearance();
		Appearance val62 = new Appearance();
		Appearance val63 = new Appearance();
		Appearance val64 = new Appearance();
		Appearance val65 = new Appearance();
		Appearance val66 = new Appearance();
		Appearance val67 = new Appearance();
		Appearance val68 = new Appearance();
		Appearance val69 = new Appearance();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		Appearance val73 = new Appearance();
		Appearance val74 = new Appearance();
		Appearance val75 = new Appearance();
		Appearance val76 = new Appearance();
		UltraTab val77 = new UltraTab();
		UltraTab val78 = new UltraTab();
		UltraTab val79 = new UltraTab();
		Appearance val80 = new Appearance();
		Appearance val81 = new Appearance();
		Appearance val82 = new Appearance();
		Appearance val83 = new Appearance();
		Appearance val84 = new Appearance();
		Appearance val85 = new Appearance();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGSysOption = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGSysAcc = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.ULGJVTransTypes = new UltraGrid();
		this.ultraTabSharedControlsPage2 = new UltraTabSharedControlsPage();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ultraPictureBox1 = new UltraPictureBox();
		this.ultraButton1 = new UltraButton();
		this.ultraLabel1 = new UltraLabel();
		this.ultraButton2 = new UltraButton();
		this.ultraTextEditor1 = new UltraTextEditor();
		this.ultraTextEditor2 = new UltraTextEditor();
		this.ultraLabel6 = new UltraLabel();
		this.ultraButton3 = new UltraButton();
		this.ultraPictureBox2 = new UltraPictureBox();
		this.ultraLabel7 = new UltraLabel();
		this.ultraPictureBox3 = new UltraPictureBox();
		this.ultraButton4 = new UltraButton();
		this.ultraButton5 = new UltraButton();
		this.ultraLabel8 = new UltraLabel();
		this.ultraLabel9 = new UltraLabel();
		this.ultraTabPageControl7 = new UltraTabPageControl();
		this.ultraCheckEditor1 = new UltraCheckEditor();
		this.ultraGroupBox1 = new UltraGroupBox();
		this.ultraDateTimeEditor1 = new UltraDateTimeEditor();
		this.ultraLabel10 = new UltraLabel();
		this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel11 = new UltraLabel();
		this.ultraGroupBox2 = new UltraGroupBox();
		this.ultraLabel12 = new UltraLabel();
		this.ultraCheckEditor2 = new UltraCheckEditor();
		this.ultraDateTimeEditor2 = new UltraDateTimeEditor();
		this.ultraDateTimeEditor3 = new UltraDateTimeEditor();
		this.ultraLabel13 = new UltraLabel();
		this.ultraGroupBox3 = new UltraGroupBox();
		this.radioButton1 = new System.Windows.Forms.RadioButton();
		this.radioButton2 = new System.Windows.Forms.RadioButton();
		this.radioButton3 = new System.Windows.Forms.RadioButton();
		this.ultraTabPageControl8 = new UltraTabPageControl();
		this.ultraGrid1 = new UltraGrid();
		this.ultraTabPageControl9 = new UltraTabPageControl();
		this.ultraGrid2 = new UltraGrid();
		this.ultraTabPageControl10 = new UltraTabPageControl();
		this.ultraGrid3 = new UltraGrid();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.tcSystemDefaults = new UltraTabControl();
		this.btnCancel = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGSysOption).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGSysAcc).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGJVTransTypes).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox3).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGrid1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGrid2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGrid3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tcSystemDefaults).BeginInit();
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGSysOption);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGSysOption, "ULGSysOption");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGSysOption).Name = "ULGSysOption";
		this.ULGSysOption.AfterEnterEditMode += new System.EventHandler(ULGSysOption_AfterEnterEditMode);
		this.ULGSysOption.CellChange += new CellEventHandler(ULGSysOption_CellChange);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGSysAcc);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGSysAcc, "ULGSysAcc");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val11).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val11, "appearance11");
		((SpecialBoxBase)((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val12;
		((SpecialBoxBase)((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((AppearanceBase)val17).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val17, "appearance17");
		((AppearanceBase)val17).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val18).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val18).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGSysAcc).Name = "ULGSysAcc";
		this.ULGSysAcc.AfterEnterEditMode += new System.EventHandler(ULGSysAcc_AfterEnterEditMode);
		this.ULGSysAcc.CellListSelect += new CellEventHandler(ULGSysAcc_CellListSelect);
		resources.ApplyResources(this.ultraTabPageControl4, "ultraTabPageControl4");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ULGJVTransTypes);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.ULGJVTransTypes, "ULGJVTransTypes");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val21, "appearance21");
		((SpecialBoxBase)((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val21;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val22;
		((SpecialBoxBase)((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val23).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val23).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val25, "appearance25");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val27, "appearance27");
		((AppearanceBase)val27).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val28).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val29, "appearance29");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val30, "appearance30");
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGJVTransTypes).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGJVTransTypes).Name = "ULGJVTransTypes";
		resources.ApplyResources(this.ultraTabSharedControlsPage2, "ultraTabSharedControlsPage2");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage2).Name = "ultraTabSharedControlsPage2";
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPictureBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTextEditor1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTextEditor2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPictureBox2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraPictureBox3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ultraPictureBox1, "ultraPictureBox1");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val31, "appearance31");
		this.ultraPictureBox1.Appearance = (AppearanceBase)(object)val31;
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox1).Name = "ultraPictureBox1";
		resources.ApplyResources(this.ultraButton1, "ultraButton1");
		((System.Windows.Forms.Control)(object)this.ultraButton1).Name = "ultraButton1";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val32, "appearance32");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val32;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		resources.ApplyResources(this.ultraButton2, "ultraButton2");
		((UltraButtonBase)this.ultraButton2).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.ultraButton2).Name = "ultraButton2";
		resources.ApplyResources(this.ultraTextEditor1, "ultraTextEditor1");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val33, "appearance33");
		((TextEditorControlBase)this.ultraTextEditor1).Appearance = (AppearanceBase)(object)val33;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).BackColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).Name = "ultraTextEditor1";
		resources.ApplyResources(this.ultraTextEditor2, "ultraTextEditor2");
		((AppearanceBase)val34).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val34, "appearance34");
		((TextEditorControlBase)this.ultraTextEditor2).Appearance = (AppearanceBase)(object)val34;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).BackColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).Name = "ultraTextEditor2";
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val35, "appearance35");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.ultraButton3, "ultraButton3");
		((ControlBase)this.ultraButton3).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraButton3).Name = "ultraButton3";
		resources.ApplyResources(this.ultraPictureBox2, "ultraPictureBox2");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val36, "appearance36");
		this.ultraPictureBox2.Appearance = (AppearanceBase)(object)val36;
		this.ultraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox2.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox2.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox2).Name = "ultraPictureBox2";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val37, "appearance37");
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val37;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		resources.ApplyResources(this.ultraPictureBox3, "ultraPictureBox3");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val38, "appearance38");
		this.ultraPictureBox3.Appearance = (AppearanceBase)(object)val38;
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox3.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox3).Name = "ultraPictureBox3";
		resources.ApplyResources(this.ultraButton4, "ultraButton4");
		((System.Windows.Forms.Control)(object)this.ultraButton4).Name = "ultraButton4";
		resources.ApplyResources(this.ultraButton5, "ultraButton5");
		((System.Windows.Forms.Control)(object)this.ultraButton5).Name = "ultraButton5";
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val39, "appearance39");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val39;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val40, "appearance40");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val40;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		resources.ApplyResources(this.ultraCheckEditor1, "ultraCheckEditor1");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val41, "appearance41");
		((UltraToggleEditorBase)this.ultraCheckEditor1).Appearance = (AppearanceBase)(object)val41;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.ultraCheckEditor1).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).Name = "ultraCheckEditor1";
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val42, "appearance42");
		this.ultraGroupBox1.Appearance = (AppearanceBase)(object)val42;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add(this.numericUpDown1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		resources.ApplyResources(this.ultraDateTimeEditor1, "ultraDateTimeEditor1");
		this.ultraDateTimeEditor1.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		this.ultraDateTimeEditor1.MaskInput = "{longtime}";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor1).Name = "ultraDateTimeEditor1";
		this.ultraDateTimeEditor1.PromptChar = '-';
		this.ultraDateTimeEditor1.SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
		this.numericUpDown1.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numericUpDown1.Name = "numericUpDown1";
		this.numericUpDown1.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		resources.ApplyResources(this.ultraGroupBox2, "ultraGroupBox2");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val43, "appearance43");
		this.ultraGroupBox2.Appearance = (AppearanceBase)(object)val43;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor3);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Name = "ultraGroupBox2";
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		resources.ApplyResources(this.ultraCheckEditor2, "ultraCheckEditor2");
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor2).Name = "ultraCheckEditor2";
		resources.ApplyResources(this.ultraDateTimeEditor2, "ultraDateTimeEditor2");
		this.ultraDateTimeEditor2.FormatString = "";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor2).Name = "ultraDateTimeEditor2";
		this.ultraDateTimeEditor2.PromptChar = '-';
		resources.ApplyResources(this.ultraDateTimeEditor3, "ultraDateTimeEditor3");
		this.ultraDateTimeEditor3.FormatString = "";
		((System.Windows.Forms.Control)(object)this.ultraDateTimeEditor3).Name = "ultraDateTimeEditor3";
		this.ultraDateTimeEditor3.PromptChar = '-';
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((AppearanceBase)val44).ForeColor = System.Drawing.Color.Red;
		resources.ApplyResources(val44, "appearance44");
		((ControlBase)this.ultraLabel13).Appearance = (AppearanceBase)(object)val44;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		resources.ApplyResources(this.ultraGroupBox3, "ultraGroupBox3");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val45, "appearance45");
		this.ultraGroupBox3.Appearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton2);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Controls.Add(this.radioButton3);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).Name = "ultraGroupBox3";
		resources.ApplyResources(this.radioButton1, "radioButton1");
		this.radioButton1.BackColor = System.Drawing.Color.Transparent;
		this.radioButton1.ForeColor = System.Drawing.Color.Red;
		this.radioButton1.Name = "radioButton1";
		this.radioButton1.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.radioButton2, "radioButton2");
		this.radioButton2.BackColor = System.Drawing.Color.Transparent;
		this.radioButton2.ForeColor = System.Drawing.Color.Red;
		this.radioButton2.Name = "radioButton2";
		this.radioButton2.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.radioButton3, "radioButton3");
		this.radioButton3.BackColor = System.Drawing.Color.Transparent;
		this.radioButton3.Checked = true;
		this.radioButton3.ForeColor = System.Drawing.Color.Red;
		this.radioButton3.Name = "radioButton3";
		this.radioButton3.TabStop = true;
		this.radioButton3.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.ultraTabPageControl8, "ultraTabPageControl8");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).Name = "ultraTabPageControl8";
		resources.ApplyResources(this.ultraGrid1, "ultraGrid1");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val46).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val46).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val46).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val46, "appearance46");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val46;
		((AppearanceBase)val47).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val47, "appearance47");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val47;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val48).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val48).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val48).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val48).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val48, "appearance48");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val48;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val49).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val49, "appearance49");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val49;
		((AppearanceBase)val50).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val50).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val50, "appearance50");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val50;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val51, "appearance51");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val51;
		((AppearanceBase)val52).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val52, "appearance52");
		((AppearanceBase)val52).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val52;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val53).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val53).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val53).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val53).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val53, "appearance53");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val53;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val54).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val54, "appearance54");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val54;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val55).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val55, "appearance55");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val55;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid1).Name = "ultraGrid1";
		resources.ApplyResources(this.ultraTabPageControl9, "ultraTabPageControl9");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Name = "ultraTabPageControl9";
		resources.ApplyResources(this.ultraGrid2, "ultraGrid2");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val56).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val56).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val56).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val56).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val56, "appearance56");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val56;
		((AppearanceBase)val57).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val57, "appearance57");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val57;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val58).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val58).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val58).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val58).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val58, "appearance58");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val59).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val59).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val59, "appearance59");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val59;
		((AppearanceBase)val60).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val60).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val60, "appearance60");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val60;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val61).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val61, "appearance61");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val61;
		((AppearanceBase)val62).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val62, "appearance62");
		((AppearanceBase)val62).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val62;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val63).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val63).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val63).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val63).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val63).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val63, "appearance63");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val63;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val64).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val64).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val64, "appearance64");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val64;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val65).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val65, "appearance65");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val65;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid2).Name = "ultraGrid2";
		resources.ApplyResources(this.ultraTabPageControl10, "ultraTabPageControl10");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Name = "ultraTabPageControl10";
		resources.ApplyResources(this.ultraGrid3, "ultraGrid3");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val66).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val66).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val66).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val66).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val66, "appearance66");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val66;
		((AppearanceBase)val67).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val67, "appearance67");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val67;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val68).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val68).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val68).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val68).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val68, "appearance68");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val68;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val69).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val69).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val69, "appearance69");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val69;
		((AppearanceBase)val70).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val70).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val70, "appearance70");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val70;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val71).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val71, "appearance71");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val71;
		((AppearanceBase)val72).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val72, "appearance72");
		((AppearanceBase)val72).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val72;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val73).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val73).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val73).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val73).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val73).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val73, "appearance73");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val73;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val74).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val74).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val74, "appearance74");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val74;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val75).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val75, "appearance75");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val75;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid3).Name = "ultraGrid3";
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.tcSystemDefaults, "tcSystemDefaults");
		((AppearanceBase)val76).ForeColor = System.Drawing.Color.Maroon;
		resources.ApplyResources(val76, "appearance76");
		((AppearanceBase)val76).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tcSystemDefaults).Appearance = (AppearanceBase)(object)val76;
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Name = "tcSystemDefaults";
		((UltraTabControlBase)this.tcSystemDefaults).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tcSystemDefaults).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val77).Key = "AccountingOptions";
		val77.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val77, "ultraTab2");
		((SubObjectBase)val77).ForceApplyResources = "";
		((KeyedSubObjectBase)val78).Key = "System Accounts";
		val78.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val78, "ultraTab1");
		((SubObjectBase)val78).ForceApplyResources = "";
		((KeyedSubObjectBase)val79).Key = "TransactionsTypes";
		val79.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val79, "ultraTab3");
		((SubObjectBase)val79).ForceApplyResources = "";
		((UltraTabControlBase)this.tcSystemDefaults).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val77, val78, val79 });
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val80).Image = resources.GetObject("appearance77.Image");
		resources.ApplyResources(val80, "appearance77");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val80;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val81).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val81, "appearance78");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val81;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val82).Image = resources.GetObject("appearance79.Image");
		resources.ApplyResources(val82, "appearance79");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val82;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val83).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val83).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val83).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val83, "appearance80");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val83;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val84).Image = resources.GetObject("appearance81.Image");
		resources.ApplyResources(val84, "appearance81");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val84;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val85).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val85).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val85).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val85, "appearance82");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val85;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tcSystemDefaults);
		base.Name = "frmAccountingSetting";
		base.Load += new System.EventHandler(frmTransactioTypes_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tcSystemDefaults, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGSysOption).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGSysAcc).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGJVTransTypes).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraTextEditor2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraCheckEditor2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraDateTimeEditor3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox3).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox3).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl8).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGrid1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGrid2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGrid3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tcSystemDefaults).EndInit();
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
