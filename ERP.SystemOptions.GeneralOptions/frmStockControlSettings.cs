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

public class frmStockControlSettings : frmBase
{
	private DataTable dtSystemOptions;

	private DataTable dtSystemAccounts;

	private DataTable dtAccountTypes;

	private DataTable dtSubAccounts;

	private ValueList vlAccountID = new ValueList();

	private ValueList vlJournalTypes = new ValueList();

	private ValueList vlJVTypes = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

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

	private UltraTabPageControl ultraTabPageControl4;

	private UltraTextEditor txtPBatchNoEnd;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtPBatchNoStart;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtClientReturn;

	private UltraLabel ultraLabel2;

	private NumericUpDown txtStopJVsFrom;

	private NumericUpDown txtStopJVsTo;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel14;

	protected internal UltraCheckEditor chkStopJVs;

	private UltraLabel ultraLabel5;

	private NumericUpDown txtQtyDecimals;

	private UltraLabel ultraLabel17;

	protected internal UltraCheckEditor chkStopCost;

	private NumericUpDown txtStopCostFrom;

	private NumericUpDown txtStopCostTo;

	private UltraLabel ultraLabel21;

	private UltraLabel ultraLabel20;

	private UltraLabel ultraLabel19;

	private UltraLabel ultraLabel18;

	private UltraTextEditor txtStoreTransferReceivePeriod;

	private UltraLabel ultraLabel22;

	private UltraTextEditor txtAddedTaxPercentage;

	private UltraLabel lblAddedTaxPercentage;

	private UltraLabel ultraLabel25;

	private UltraLabel ultraLabel23;

	private UltraTextEditor txtDiscountTaxPercentage;

	private UltraLabel lblDiscountTaxPercentage;

	private UltraLabel lblSalesInvoiceMsg;

	private UltraTextEditor txtSalesInvoiceMessage;

	public frmStockControlSettings()
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
		((Control)(object)txtClientReturn).Text = GlobalFunctions.GetDefault("ClientReturnPeriod");
		((Control)(object)txtStoreTransferReceivePeriod).Text = GlobalFunctions.GetDefault("StoreTransferReceivePeriod");
		((Control)(object)txtPBatchNoStart).Text = GlobalFunctions.GetDefault("ProductionBatchNoStartNo");
		((Control)(object)txtPBatchNoEnd).Text = GlobalFunctions.GetDefault("ProductionBatchNoEndNo");
		decimal num = (txtStopJVsFrom.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("StopStockControlJVsFrom")));
		decimal num3 = num;
		num = (txtStopJVsTo.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("StopStockControlJVsTo")));
		decimal num5 = num;
		num = (txtStopCostFrom.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("StopCalculateStockCostFrom")));
		decimal num7 = num;
		num = (txtStopCostTo.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("StopCalculateStockCostTo")));
		decimal num9 = num;
		txtQtyDecimals.Value = Convert.ToInt32(GlobalFunctions.GetDefault("QuantityDecimals"));
		((TextEditorControlBase)txtAddedTaxPercentage).Value = Convert.ToInt32(GlobalFunctions.GetDefault("AddedTaxPercentage"));
		((TextEditorControlBase)txtDiscountTaxPercentage).Value = Convert.ToInt32(GlobalFunctions.GetDefault("DiscountTaxPercentage"));
		((Control)(object)txtSalesInvoiceMessage).Text = GlobalFunctions.GetDefault("SalesInvoiceMessage");
		((UltraToggleEditorBase)chkStopJVs).Checked = num3 > 0m || num5 > 0m;
		((UltraToggleEditorBase)chkStopCost).Checked = num7 > 0m || num9 > 0m;
		dtSystemOptions = BusinessLayer.Defaults.SystemOptions.SelectForModule("200", "300", GlobalVariables.IsArabic ? "1" : "0");
		dtSystemAccounts = SystemAccount.SelectForModule("200", "300", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtAccountTypes = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		foreach (DataRow row in dtAccountTypes.Rows)
		{
			vlAccountID.ValueListItems.Add(row["AccountID"], row["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtSubAccounts.Rows.Count; i++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[i]["SubAccountID"], dtSubAccounts.Rows[i]["Name"].ToString());
		}
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
		GlobalFunctions.SetDefault("ClientReturnPeriod", ((Control)(object)txtClientReturn).Text);
		GlobalFunctions.SetDefault("StoreTransferReceivePeriod", ((Control)(object)txtStoreTransferReceivePeriod).Text);
		GlobalFunctions.SetDefault("ProductionBatchNoStartNo", ((Control)(object)txtPBatchNoStart).Text);
		GlobalFunctions.SetDefault("ProductionBatchNoEndNo", ((Control)(object)txtPBatchNoEnd).Text);
		GlobalFunctions.SetDefault("StopStockControlJVsFrom", txtStopJVsFrom.Value.ToString());
		GlobalFunctions.SetDefault("StopStockControlJVsTo", txtStopJVsTo.Value.ToString());
		GlobalFunctions.SetDefault("StopCalculateStockCostFrom", txtStopCostFrom.Value.ToString());
		GlobalFunctions.SetDefault("StopCalculateStockCostTo", txtStopCostTo.Value.ToString());
		GlobalFunctions.SetDefault("QuantityDecimals", txtQtyDecimals.Value.ToString());
		GlobalFunctions.SetDefault("AddedTaxPercentage", ((TextEditorControlBase)txtAddedTaxPercentage).Value.ToString());
		GlobalFunctions.SetDefault("DiscountTaxPercentage", ((TextEditorControlBase)txtDiscountTaxPercentage).Value.ToString());
		GlobalFunctions.SetDefault("SalesInvoiceMessage", ((Control)(object)txtSalesInvoiceMessage).Text);
		if (txtQtyDecimals.Value == 0m)
		{
			GlobalVariables.QtyDecimals = "###,##";
		}
		else if (txtQtyDecimals.Value == 1m)
		{
			GlobalVariables.QtyDecimals = "###,##.0";
		}
		else if (txtQtyDecimals.Value == 2m)
		{
			GlobalVariables.QtyDecimals = "###,##.00";
		}
		else if (txtQtyDecimals.Value == 3m)
		{
			GlobalVariables.QtyDecimals = "###,##.000";
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

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void chkStopJVs_CheckedChanged(object sender, EventArgs e)
	{
		if (!((UltraToggleEditorBase)chkStopJVs).Checked)
		{
			txtStopJVsFrom.Value = 0m;
			txtStopJVsTo.Value = 0m;
		}
		txtStopJVsFrom.ReadOnly = !((UltraToggleEditorBase)chkStopJVs).Checked;
		txtStopJVsTo.ReadOnly = !((UltraToggleEditorBase)chkStopJVs).Checked;
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

	private void chkStopCost_CheckedChanged(object sender, EventArgs e)
	{
		if (!((UltraToggleEditorBase)chkStopCost).Checked)
		{
			txtStopCostFrom.Value = 0m;
			txtStopCostTo.Value = 0m;
		}
		txtStopCostFrom.ReadOnly = !((UltraToggleEditorBase)chkStopCost).Checked;
		txtStopCostTo.ReadOnly = !((UltraToggleEditorBase)chkStopCost).Checked;
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
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Expected O, but got Unknown
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Expected O, but got Unknown
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Expected O, but got Unknown
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Expected O, but got Unknown
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Expected O, but got Unknown
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Expected O, but got Unknown
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Expected O, but got Unknown
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Expected O, but got Unknown
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Expected O, but got Unknown
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Expected O, but got Unknown
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Expected O, but got Unknown
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Expected O, but got Unknown
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Expected O, but got Unknown
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Expected O, but got Unknown
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Expected O, but got Unknown
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Expected O, but got Unknown
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Expected O, but got Unknown
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Expected O, but got Unknown
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Expected O, but got Unknown
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Expected O, but got Unknown
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Expected O, but got Unknown
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Expected O, but got Unknown
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Expected O, but got Unknown
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Expected O, but got Unknown
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Expected O, but got Unknown
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Expected O, but got Unknown
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Expected O, but got Unknown
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Expected O, but got Unknown
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Expected O, but got Unknown
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Expected O, but got Unknown
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Expected O, but got Unknown
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Expected O, but got Unknown
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Expected O, but got Unknown
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Expected O, but got Unknown
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Expected O, but got Unknown
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Expected O, but got Unknown
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Expected O, but got Unknown
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Expected O, but got Unknown
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Expected O, but got Unknown
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Expected O, but got Unknown
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Expected O, but got Unknown
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected O, but got Unknown
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Expected O, but got Unknown
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Expected O, but got Unknown
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Expected O, but got Unknown
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Expected O, but got Unknown
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064e: Expected O, but got Unknown
		//IL_0ce4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cee: Expected O, but got Unknown
		//IL_1135: Unknown result type (might be due to invalid IL or missing references)
		//IL_113f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralOptions.frmStockControlSettings));
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
		Appearance val77 = new Appearance();
		Appearance val78 = new Appearance();
		Appearance val79 = new Appearance();
		Appearance val80 = new Appearance();
		Appearance val81 = new Appearance();
		Appearance val82 = new Appearance();
		Appearance val83 = new Appearance();
		Appearance val84 = new Appearance();
		Appearance val85 = new Appearance();
		Appearance val86 = new Appearance();
		UltraTab val87 = new UltraTab();
		UltraTab val88 = new UltraTab();
		UltraTab val89 = new UltraTab();
		Appearance val90 = new Appearance();
		Appearance val91 = new Appearance();
		Appearance val92 = new Appearance();
		Appearance val93 = new Appearance();
		Appearance val94 = new Appearance();
		Appearance val95 = new Appearance();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGSysOption = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGSysAcc = new UltraGrid();
		this.ultraTabPageControl4 = new UltraTabPageControl();
		this.txtSalesInvoiceMessage = new UltraTextEditor();
		this.ultraLabel25 = new UltraLabel();
		this.ultraLabel23 = new UltraLabel();
		this.txtDiscountTaxPercentage = new UltraTextEditor();
		this.txtAddedTaxPercentage = new UltraTextEditor();
		this.lblSalesInvoiceMsg = new UltraLabel();
		this.lblDiscountTaxPercentage = new UltraLabel();
		this.lblAddedTaxPercentage = new UltraLabel();
		this.chkStopCost = new UltraCheckEditor();
		this.chkStopJVs = new UltraCheckEditor();
		this.txtStopCostFrom = new System.Windows.Forms.NumericUpDown();
		this.txtQtyDecimals = new System.Windows.Forms.NumericUpDown();
		this.txtStopCostTo = new System.Windows.Forms.NumericUpDown();
		this.txtStopJVsFrom = new System.Windows.Forms.NumericUpDown();
		this.txtStopJVsTo = new System.Windows.Forms.NumericUpDown();
		this.txtPBatchNoEnd = new UltraTextEditor();
		this.ultraLabel4 = new UltraLabel();
		this.txtPBatchNoStart = new UltraTextEditor();
		this.ultraLabel21 = new UltraLabel();
		this.ultraLabel17 = new UltraLabel();
		this.ultraLabel20 = new UltraLabel();
		this.ultraLabel15 = new UltraLabel();
		this.ultraLabel19 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel18 = new UltraLabel();
		this.ultraLabel16 = new UltraLabel();
		this.ultraLabel14 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.txtStoreTransferReceivePeriod = new UltraTextEditor();
		this.ultraLabel22 = new UltraLabel();
		this.txtClientReturn = new UltraTextEditor();
		this.ultraLabel2 = new UltraLabel();
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
		((System.ComponentModel.ISupportInitialize)this.txtSalesInvoiceMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountTaxPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedTaxPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStopCost).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkStopJVs).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopCostFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQtyDecimals).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopCostTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopJVsFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopJVsTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPBatchNoEnd).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPBatchNoStart).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreTransferReceivePeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientReturn).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesInvoiceMessage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel25);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel23);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountTaxPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtAddedTaxPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesInvoiceMsg);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountTaxPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.lblAddedTaxPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkStopCost);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.chkStopJVs);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add(this.txtStopCostFrom);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add(this.txtQtyDecimals);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add(this.txtStopCostTo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add(this.txtStopJVsFrom);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add(this.txtStopJVsTo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtPBatchNoEnd);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtPBatchNoStart);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel21);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel17);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel20);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel19);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel18);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtStoreTransferReceivePeriod);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel22);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.txtClientReturn);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).Name = "ultraTabPageControl4";
		resources.ApplyResources(this.txtSalesInvoiceMessage, "txtSalesInvoiceMessage");
		((System.Windows.Forms.Control)(object)this.txtSalesInvoiceMessage).Name = "txtSalesInvoiceMessage";
		resources.ApplyResources(this.ultraLabel25, "ultraLabel25");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.ultraLabel25).Appearance = (AppearanceBase)(object)val21;
		this.ultraLabel25.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel25).Name = "ultraLabel25";
		resources.ApplyResources(this.ultraLabel23, "ultraLabel23");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.ultraLabel23).Appearance = (AppearanceBase)(object)val22;
		this.ultraLabel23.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel23).Name = "ultraLabel23";
		resources.ApplyResources(this.txtDiscountTaxPercentage, "txtDiscountTaxPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountTaxPercentage).Name = "txtDiscountTaxPercentage";
		resources.ApplyResources(this.txtAddedTaxPercentage, "txtAddedTaxPercentage");
		((System.Windows.Forms.Control)(object)this.txtAddedTaxPercentage).Name = "txtAddedTaxPercentage";
		resources.ApplyResources(this.lblSalesInvoiceMsg, "lblSalesInvoiceMsg");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.lblSalesInvoiceMsg).Appearance = (AppearanceBase)(object)val23;
		this.lblSalesInvoiceMsg.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesInvoiceMsg).Name = "lblSalesInvoiceMsg";
		((ControlBase)this.lblSalesInvoiceMsg).WrapText = false;
		resources.ApplyResources(this.lblDiscountTaxPercentage, "lblDiscountTaxPercentage");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.lblDiscountTaxPercentage).Appearance = (AppearanceBase)(object)val24;
		this.lblDiscountTaxPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountTaxPercentage).Name = "lblDiscountTaxPercentage";
		((ControlBase)this.lblDiscountTaxPercentage).WrapText = false;
		resources.ApplyResources(this.lblAddedTaxPercentage, "lblAddedTaxPercentage");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.lblAddedTaxPercentage).Appearance = (AppearanceBase)(object)val25;
		this.lblAddedTaxPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddedTaxPercentage).Name = "lblAddedTaxPercentage";
		((ControlBase)this.lblAddedTaxPercentage).WrapText = false;
		resources.ApplyResources(this.chkStopCost, "chkStopCost");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance26");
		((UltraToggleEditorBase)this.chkStopCost).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.chkStopCost).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkStopCost).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkStopCost).Name = "chkStopCost";
		((UltraToggleEditorBase)this.chkStopCost).CheckedChanged += new System.EventHandler(chkStopCost_CheckedChanged);
		resources.ApplyResources(this.chkStopJVs, "chkStopJVs");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance27");
		((UltraToggleEditorBase)this.chkStopJVs).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.chkStopJVs).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkStopJVs).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkStopJVs).Name = "chkStopJVs";
		((UltraToggleEditorBase)this.chkStopJVs).CheckedChanged += new System.EventHandler(chkStopJVs_CheckedChanged);
		resources.ApplyResources(this.txtStopCostFrom, "txtStopCostFrom");
		this.txtStopCostFrom.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtStopCostFrom.Name = "txtStopCostFrom";
		resources.ApplyResources(this.txtQtyDecimals, "txtQtyDecimals");
		this.txtQtyDecimals.Maximum = new decimal(new int[4] { 3, 0, 0, 0 });
		this.txtQtyDecimals.Name = "txtQtyDecimals";
		resources.ApplyResources(this.txtStopCostTo, "txtStopCostTo");
		this.txtStopCostTo.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtStopCostTo.Name = "txtStopCostTo";
		resources.ApplyResources(this.txtStopJVsFrom, "txtStopJVsFrom");
		this.txtStopJVsFrom.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtStopJVsFrom.Name = "txtStopJVsFrom";
		resources.ApplyResources(this.txtStopJVsTo, "txtStopJVsTo");
		this.txtStopJVsTo.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtStopJVsTo.Name = "txtStopJVsTo";
		resources.ApplyResources(this.txtPBatchNoEnd, "txtPBatchNoEnd");
		((System.Windows.Forms.Control)(object)this.txtPBatchNoEnd).Name = "txtPBatchNoEnd";
		((System.Windows.Forms.Control)(object)this.txtPBatchNoEnd).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val28, "appearance28");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val28;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.txtPBatchNoStart, "txtPBatchNoStart");
		((System.Windows.Forms.Control)(object)this.txtPBatchNoStart).Name = "txtPBatchNoStart";
		((System.Windows.Forms.Control)(object)this.txtPBatchNoStart).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel21, "ultraLabel21");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val29, "appearance29");
		((ControlBase)this.ultraLabel21).Appearance = (AppearanceBase)(object)val29;
		this.ultraLabel21.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel21).Name = "ultraLabel21";
		resources.ApplyResources(this.ultraLabel17, "ultraLabel17");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val30, "appearance30");
		((ControlBase)this.ultraLabel17).Appearance = (AppearanceBase)(object)val30;
		this.ultraLabel17.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel17).Name = "ultraLabel17";
		((ControlBase)this.ultraLabel17).WrapText = false;
		resources.ApplyResources(this.ultraLabel20, "ultraLabel20");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val31, "appearance31");
		((ControlBase)this.ultraLabel20).Appearance = (AppearanceBase)(object)val31;
		this.ultraLabel20.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel20).Name = "ultraLabel20";
		((ControlBase)this.ultraLabel20).WrapText = false;
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val32, "appearance32");
		((ControlBase)this.ultraLabel15).Appearance = (AppearanceBase)(object)val32;
		this.ultraLabel15.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		resources.ApplyResources(this.ultraLabel19, "ultraLabel19");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val33, "appearance33");
		((ControlBase)this.ultraLabel19).Appearance = (AppearanceBase)(object)val33;
		this.ultraLabel19.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel19).Name = "ultraLabel19";
		((ControlBase)this.ultraLabel19).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val34).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val34, "appearance34");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val34;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel18, "ultraLabel18");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val35, "appearance35");
		((ControlBase)this.ultraLabel18).Appearance = (AppearanceBase)(object)val35;
		this.ultraLabel18.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel18).Name = "ultraLabel18";
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val36, "appearance36");
		((ControlBase)this.ultraLabel16).Appearance = (AppearanceBase)(object)val36;
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val37, "appearance37");
		((ControlBase)this.ultraLabel14).Appearance = (AppearanceBase)(object)val37;
		this.ultraLabel14.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val38, "appearance38");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val38;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.txtStoreTransferReceivePeriod, "txtStoreTransferReceivePeriod");
		((System.Windows.Forms.Control)(object)this.txtStoreTransferReceivePeriod).Name = "txtStoreTransferReceivePeriod";
		((System.Windows.Forms.Control)(object)this.txtStoreTransferReceivePeriod).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel22, "ultraLabel22");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val39, "appearance39");
		((ControlBase)this.ultraLabel22).Appearance = (AppearanceBase)(object)val39;
		this.ultraLabel22.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel22).Name = "ultraLabel22";
		((ControlBase)this.ultraLabel22).WrapText = false;
		resources.ApplyResources(this.txtClientReturn, "txtClientReturn");
		((System.Windows.Forms.Control)(object)this.txtClientReturn).Name = "txtClientReturn";
		((System.Windows.Forms.Control)(object)this.txtClientReturn).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val40, "appearance40");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val40;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
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
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val41, "appearance41");
		this.ultraPictureBox1.Appearance = (AppearanceBase)(object)val41;
		this.ultraPictureBox1.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox1.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox1).Name = "ultraPictureBox1";
		resources.ApplyResources(this.ultraButton1, "ultraButton1");
		((System.Windows.Forms.Control)(object)this.ultraButton1).Name = "ultraButton1";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val42, "appearance42");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val42;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		resources.ApplyResources(this.ultraButton2, "ultraButton2");
		((UltraButtonBase)this.ultraButton2).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.ultraButton2).Name = "ultraButton2";
		resources.ApplyResources(this.ultraTextEditor1, "ultraTextEditor1");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val43, "appearance43");
		((TextEditorControlBase)this.ultraTextEditor1).Appearance = (AppearanceBase)(object)val43;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).BackColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor1).Name = "ultraTextEditor1";
		resources.ApplyResources(this.ultraTextEditor2, "ultraTextEditor2");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val44, "appearance44");
		((TextEditorControlBase)this.ultraTextEditor2).Appearance = (AppearanceBase)(object)val44;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).BackColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraTextEditor2).Name = "ultraTextEditor2";
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val45, "appearance45");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.ultraButton3, "ultraButton3");
		((ControlBase)this.ultraButton3).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraButton3).Name = "ultraButton3";
		resources.ApplyResources(this.ultraPictureBox2, "ultraPictureBox2");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val46, "appearance46");
		this.ultraPictureBox2.Appearance = (AppearanceBase)(object)val46;
		this.ultraPictureBox2.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox2.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox2.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox2).Name = "ultraPictureBox2";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val47, "appearance47");
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val47;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		resources.ApplyResources(this.ultraPictureBox3, "ultraPictureBox3");
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val48, "appearance48");
		this.ultraPictureBox3.Appearance = (AppearanceBase)(object)val48;
		this.ultraPictureBox3.BorderShadowColor = System.Drawing.Color.Empty;
		this.ultraPictureBox3.BorderStyle = (UIElementBorderStyle)2;
		this.ultraPictureBox3.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraPictureBox3).Name = "ultraPictureBox3";
		resources.ApplyResources(this.ultraButton4, "ultraButton4");
		((System.Windows.Forms.Control)(object)this.ultraButton4).Name = "ultraButton4";
		resources.ApplyResources(this.ultraButton5, "ultraButton5");
		((System.Windows.Forms.Control)(object)this.ultraButton5).Name = "ultraButton5";
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val49).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val49, "appearance49");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val49;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val50).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val50, "appearance50");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val50;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		resources.ApplyResources(this.ultraTabPageControl7, "ultraTabPageControl7");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraCheckEditor1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl7).Name = "ultraTabPageControl7";
		resources.ApplyResources(this.ultraCheckEditor1, "ultraCheckEditor1");
		((AppearanceBase)val51).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val51).ForeColor = System.Drawing.Color.Blue;
		resources.ApplyResources(val51, "appearance51");
		((UltraToggleEditorBase)this.ultraCheckEditor1).Appearance = (AppearanceBase)(object)val51;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.ultraCheckEditor1).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.ultraCheckEditor1).Name = "ultraCheckEditor1";
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((AppearanceBase)val52).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val52, "appearance52");
		this.ultraGroupBox1.Appearance = (AppearanceBase)(object)val52;
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
		((AppearanceBase)val53).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val53, "appearance53");
		this.ultraGroupBox2.Appearance = (AppearanceBase)(object)val53;
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
		((AppearanceBase)val54).ForeColor = System.Drawing.Color.Red;
		resources.ApplyResources(val54, "appearance54");
		((ControlBase)this.ultraLabel13).Appearance = (AppearanceBase)(object)val54;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		resources.ApplyResources(this.ultraGroupBox3, "ultraGroupBox3");
		((AppearanceBase)val55).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val55, "appearance55");
		this.ultraGroupBox3.Appearance = (AppearanceBase)(object)val55;
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
		((AppearanceBase)val56).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val56).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val56).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val56).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val56, "appearance56");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val56;
		((AppearanceBase)val57).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val57, "appearance57");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val57;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val58).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val58).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val58).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val58).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val58, "appearance58");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val58;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val59).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val59).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val59, "appearance59");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val59;
		((AppearanceBase)val60).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val60).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val60, "appearance60");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val60;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val61).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val61, "appearance61");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val61;
		((AppearanceBase)val62).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val62, "appearance62");
		((AppearanceBase)val62).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val62;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val63).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val63).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val63).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val63).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val63).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val63, "appearance63");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val63;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val64).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val64).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val64, "appearance64");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val64;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val65).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val65, "appearance65");
		((UltraGridBase)this.ultraGrid1).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val65;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid1).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid1).Name = "ultraGrid1";
		resources.ApplyResources(this.ultraTabPageControl9, "ultraTabPageControl9");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl9).Name = "ultraTabPageControl9";
		resources.ApplyResources(this.ultraGrid2, "ultraGrid2");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val66).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val66).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val66).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val66).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val66, "appearance66");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val66;
		((AppearanceBase)val67).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val67, "appearance67");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val67;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val68).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val68).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val68).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val68).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val68, "appearance68");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val68;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val69).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val69).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val69, "appearance69");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val69;
		((AppearanceBase)val70).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val70).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val70, "appearance70");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val70;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val71).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val71, "appearance71");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val71;
		((AppearanceBase)val72).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val72, "appearance72");
		((AppearanceBase)val72).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val72;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val73).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val73).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val73).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val73).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val73).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val73, "appearance73");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val73;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val74).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val74).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val74, "appearance74");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val74;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val75).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val75, "appearance75");
		((UltraGridBase)this.ultraGrid2).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val75;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid2).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid2).Name = "ultraGrid2";
		resources.ApplyResources(this.ultraTabPageControl10, "ultraTabPageControl10");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGrid3);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl10).Name = "ultraTabPageControl10";
		resources.ApplyResources(this.ultraGrid3, "ultraGrid3");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val76).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val76).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val76).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val76).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val76, "appearance76");
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val76;
		((AppearanceBase)val77).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val77, "appearance77");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val77;
		((SpecialBoxBase)((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val78).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val78).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val78).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val78).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val78, "appearance78");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val78;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val79).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val79).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val79, "appearance79");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val79;
		((AppearanceBase)val80).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val80).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val80, "appearance80");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val80;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val81).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val81, "appearance81");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val81;
		((AppearanceBase)val82).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val82, "appearance82");
		((AppearanceBase)val82).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val82;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val83).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val83).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val83).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val83).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val83).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val83, "appearance83");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val83;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val84).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val84).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val84, "appearance84");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val84;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val85).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val85, "appearance85");
		((UltraGridBase)this.ultraGrid3).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val85;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ultraGrid3).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraGrid3).Name = "ultraGrid3";
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.tcSystemDefaults, "tcSystemDefaults");
		((AppearanceBase)val86).ForeColor = System.Drawing.Color.Maroon;
		resources.ApplyResources(val86, "appearance86");
		((AppearanceBase)val86).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tcSystemDefaults).Appearance = (AppearanceBase)(object)val86;
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl4);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Name = "tcSystemDefaults";
		((UltraTabControlBase)this.tcSystemDefaults).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tcSystemDefaults).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val87).Key = "AccountingOptions";
		val87.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val87, "ultraTab2");
		((SubObjectBase)val87).ForceApplyResources = "";
		((KeyedSubObjectBase)val88).Key = "System Accounts";
		val88.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val88, "ultraTab1");
		((SubObjectBase)val88).ForceApplyResources = "";
		((KeyedSubObjectBase)val89).Key = "Setting";
		val89.TabPage = this.ultraTabPageControl4;
		resources.ApplyResources(val89, "ultraTab3");
		((SubObjectBase)val89).ForceApplyResources = "";
		((UltraTabControlBase)this.tcSystemDefaults).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val87, val88, val89 });
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val90).Image = resources.GetObject("appearance87.Image");
		resources.ApplyResources(val90, "appearance87");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val90;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val91).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val91, "appearance88");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val91;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val92).Image = resources.GetObject("appearance89.Image");
		resources.ApplyResources(val92, "appearance89");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val92;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val93).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val93).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val93).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val93, "appearance90");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val93;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val94).Image = resources.GetObject("appearance91.Image");
		resources.ApplyResources(val94, "appearance91");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val94;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val95).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val95).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val95).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val95, "appearance92");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val95;
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
		base.Name = "frmStockControlSettings";
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl4).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtSalesInvoiceMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountTaxPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddedTaxPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStopCost).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkStopJVs).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopCostFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQtyDecimals).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopCostTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopJVsFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopJVsTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPBatchNoEnd).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPBatchNoStart).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStoreTransferReceivePeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientReturn).EndInit();
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
