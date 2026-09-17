using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.SafesAndBanks;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SafesAndBanks.BankTransactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.Approved;

public class frmNotesUnderCollection : frmPosted
{
	private DataTable dtSafes = new DataTable();

	private ValueList vlSafes = new ValueList();

	private IContainer components = null;

	private UltraPanel pnlCheckType;

	private RadioButton rbBankIn;

	private RadioButton rbBankOut;

	public frmNotesUnderCollection()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		NoCol = "VoucherNo";
		rbBankIn.Checked = true;
	}

	public frmNotesUnderCollection(int ID, bool IsBankIn)
		: this()
	{
		rbBankIn.Checked = IsBankIn;
		rbBankOut.Checked = !IsBankIn;
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtSafes = Safes.FillComboBySafeIDs(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSafes.ValueListItems.Clear();
		for (int i = 0; i < dtSafes.Rows.Count; i++)
		{
			vlSafes.ValueListItems.Add(dtSafes.Rows[i]["SafeID"], dtSafes.Rows[i]["SafeName"].ToString());
		}
	}

	public override void FillGrid()
	{
		if (rbBankIn.Checked)
		{
			dtsource = BankIn.SelectForUnderCollection(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtsource = BankOut.SelectForUnderCollection(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		}
		if (RowID != "")
		{
			dtsource.Select(" VoucherID =" + RowID)[0]["Collected"] = true;
			RowID = "";
		}
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		if (rbBankIn.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpense"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Returned"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSafe"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "بنك" : "Bank");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشيك" : "Check No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشيك" : "Check Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الترحيل" : "Approved Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpense"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpense"].Header).Caption = (GlobalVariables.IsArabic ? "مصاريف التحصيل" : "Collection Expense");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Returned"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Returned"].Header).Caption = (GlobalVariables.IsArabic ? "مرتد" : "Returned");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSafe"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSafe"].Header).Caption = (GlobalVariables.IsArabic ? "خزينة" : "To Safe");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeID"].Header).Caption = (GlobalVariables.IsArabic ? " إسم الخزينة" : "Safe");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeID"].ValueList = (IValueList)(object)vlSafes;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Header).Caption = "";
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].AllowRowFiltering = (DefaultableBoolean)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpense"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Returned"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "بنك" : "Bank");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشيك" : "Check No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشيك" : "Check Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الترحيل" : "Approved Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpense"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionExpense"].Header).Caption = (GlobalVariables.IsArabic ? "مصاريف التحصيل" : "Collection Expense");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Returned"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Returned"].Header).Caption = (GlobalVariables.IsArabic ? "مرتد" : "Returned");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Header).Caption = "";
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].AllowRowFiltering = (DefaultableBoolean)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Collected"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		}
	}

	public override void SelectFullRow()
	{
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Collected" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ApprovalDate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CollectionExpense" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ToSafe" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Returned")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = false;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SafeID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ToSafe"].Value.Equals(true))
		{
			((GridItemBase)ULGData.ActiveCell).Selected = false;
		}
		else
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
		if (rbBankIn.Checked && !bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ToSafe"].Value.ToString()))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value = DBNull.Value;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Collected" && (((UltraGridBase)ULGData).ActiveRow.Cells["Returned"].Value.Equals(true) || (rbBankIn.Checked && ((UltraGridBase)ULGData).ActiveRow.Cells["ToSafe"].Value.Equals(true))))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Returned"].Value = false;
			if (rbBankIn.Checked)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ToSafe"].Value = false;
				((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value = DBNull.Value;
			}
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Returned" && (((UltraGridBase)ULGData).ActiveRow.Cells["Collected"].Value.Equals(true) || (rbBankIn.Checked && ((UltraGridBase)ULGData).ActiveRow.Cells["ToSafe"].Value.Equals(true))))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Collected"].Value = false;
			if (rbBankIn.Checked)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ToSafe"].Value = false;
				((UltraGridBase)ULGData).ActiveRow.Cells["SafeID"].Value = DBNull.Value;
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["CollectionExpense"].Value = 0;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ToSafe" && (((UltraGridBase)ULGData).ActiveRow.Cells["Returned"].Value.Equals(true) || ((UltraGridBase)ULGData).ActiveRow.Cells["Collected"].Value.Equals(true)))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Returned"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["Collected"].Value = false;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CollectionExpense" && ((UltraGridBase)ULGData).ActiveRow.Cells["Collected"].Value.Equals(false))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void SaveData()
	{
		int num = 0;
		if (rbBankIn.Checked)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToSafe"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["SafeID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار الخزينة", "Please Select Safe Name");
					((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
					return;
				}
			}
		}
		DataTable dataTable = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
		if (rbBankIn.Checked)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[j].Cells["Collected"].Value.Equals(true) || ((UltraGridBase)ULGData).Rows[j].Cells["ToSafe"].Value.Equals(true))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
						return;
					}
					num++;
					string text = "";
					string text2 = "";
					if (((UltraGridBase)ULGData).Rows[j].Cells["ToSafe"].Value.Equals(true))
					{
						text = dtSafes.Select("SafeID =  " + ((UltraGridBase)ULGData).Rows[j].Cells["SafeID"].Value.ToString())[0]["AccountID"].ToString();
						text2 = dtSafes.Select("SafeID =  " + ((UltraGridBase)ULGData).Rows[j].Cells["SafeID"].Value.ToString())[0]["SubAccountID"].ToString();
					}
					DataRow dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = (((UltraGridBase)ULGData).Rows[j].Cells["ToSafe"].Value.Equals(true) ? text : ((UltraGridBase)ULGData).Rows[j].Cells["BankAccID"].Value);
					dataRow["SubAccountID"] = (((UltraGridBase)ULGData).Rows[j].Cells["ToSafe"].Value.Equals(true) ? text2 : ((UltraGridBase)ULGData).Rows[j].Cells["BankSubAccID"].Value);
					dataRow["Debit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString()) - ((((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpense"].Value == DBNull.Value) ? 0m : decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpense"].Value.ToString()));
					dataRow["Credit"] = "0";
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
					dataRow["LocalCredit"] = "0";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					if (((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpense"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpense"].Value.ToString()) > 0m)
					{
						dataRow = dataTable.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpenseAccID"].Value;
						dataRow["Debit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpense"].Value.ToString());
						dataRow["Credit"] = "0";
						dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
						dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
						dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
						dataRow["LocalCredit"] = "0";
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
						dataTable.Rows.Add(dataRow);
					}
					dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionAccID"].Value;
					dataRow["Debit"] = "0";
					dataRow["Credit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString());
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					string code = JV.GetCode(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), "16");
					int num2 = JV.GenerateJV_Insert(code, DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), "17", ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["VoucherNo"].Value.ToString(), " ( " + ((UltraGridBase)ULGData).Rows[j].Cells["ChargedPerson"].Value.ToString() + " ) " + ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), "0", "1", dataTable, "0", ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), "1", GlobalVariables.UserID);
					Main.ExecuteNonQuery(" Update SB_BankIn set JVID3= " + num2 + ",Collected= 1 ,CollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "',CollectionExpense=" + ((UltraGridBase)ULGData).Rows[j].Cells["CollectionExpense"].Value.ToString() + ", ToSafe=" + (((UltraGridBase)ULGData).Rows[j].Cells["ToSafe"].Value.Equals(true) ? "1" : "0") + ",SafeID =" + ((((UltraGridBase)ULGData).Rows[j].Cells["SafeID"].Value == DBNull.Value) ? "NULL" : ((UltraGridBase)ULGData).Rows[j].Cells["SafeID"].Value.ToString()) + " Where BankInID =" + ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString() + "; exec SP_Trans_Log " + GlobalVariables.UserID + " ,'SB_BankIn' ," + ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString() + " ,'U';");
					dataTable.Clear();
					dataRow.Delete();
				}
				if (((UltraGridBase)ULGData).Rows[j].Cells["Returned"].Value.Equals(true))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
						return;
					}
					num++;
					DataTable dataTable2 = BankInDetails.SelectByBankInID(((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
					DataRow dataRow;
					for (int k = 0; k < dataTable2.Rows.Count; k++)
					{
						dataRow = dataTable.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = dataTable2.Rows[k]["AccountID"];
						dataRow["SubAccountID"] = dataTable2.Rows[k]["SubAccountID"];
						dataRow["CostCenterID"] = dataTable2.Rows[k]["CostCenterID"];
						dataRow["SubCostCenterID"] = dataTable2.Rows[k]["SubCostCenterID"];
						dataRow["Debit"] = dataTable2.Rows[k]["Value"];
						dataRow["Credit"] = "0";
						dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
						dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
						dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
						dataRow["LocalCredit"] = "0";
						dataRow["IsDocumented"] = dataTable2.Rows[k]["IsDocumented"];
						dataRow["Notes"] = dataTable2.Rows[k]["Notes"];
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
						dataTable.Rows.Add(dataRow);
					}
					dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionAccID"].Value;
					dataRow["Debit"] = "0";
					dataRow["Credit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString());
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					string code2 = JV.GetCode(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), "16");
					int num3 = JV.GenerateJV_Insert(code2, DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), "17", ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["VoucherNo"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), "0", "1", dataTable, "0", ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), "1", GlobalVariables.UserID);
					Main.ExecuteNonQuery(" Update SB_BankIn set JVID3= " + num3 + ",Returned= 1 ,CollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "' Where BankInID =" + ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString() + "; exec SP_Trans_Log " + GlobalVariables.UserID + " ,'SB_BankIn' ," + ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString() + " ,'U';");
					dataTable.Clear();
					dataRow.Delete();
				}
			}
		}
		else
		{
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[l].Cells["Collected"].Value.ToString()))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
						return;
					}
					num++;
					DataRow dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["UnderCollectionAccID"].Value.ToString();
					dataRow["Debit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["Total"].Value.ToString());
					dataRow["Credit"] = "0";
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[l].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString());
					dataRow["LocalCredit"] = "0";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					if (((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpense"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpense"].Value.ToString()) > 0m)
					{
						dataRow = dataTable.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpenseAccID"].Value.ToString();
						dataRow["Debit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpense"].Value.ToString());
						dataRow["Credit"] = "0";
						dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[l].Cells["CurrencyID"].Value.ToString();
						dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString();
						dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString());
						dataRow["LocalCredit"] = "0";
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString();
						dataTable.Rows.Add(dataRow);
					}
					dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BankAccID"].Value;
					dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BankSubAccID"].Value;
					dataRow["Debit"] = "0";
					dataRow["Credit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["TotalAfterTax"].Value.ToString()) + ((((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpense"].Value == DBNull.Value) ? 0m : decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpense"].Value.ToString()));
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[l].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString());
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value;
					dataTable.Rows.Add(dataRow);
					if (decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["TaxValue"].Value.ToString()) > 0m)
					{
						dataRow = dataTable.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["TaxAccountID"].Value;
						dataRow["SubAccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["TaxSubAccountID"].Value;
						dataRow["Debit"] = "0";
						dataRow["Credit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["TaxValue"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString());
						dataRow["CurrencyID"] = GlobalVariables.LocalCurrencyID.ToString();
						dataRow["ExchangeRate"] = "1";
						dataRow["LocalDebit"] = "0";
						dataRow["LocalCredit"] = dataRow["Credit"].ToString();
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString();
						dataTable.Rows.Add(dataRow);
					}
					string code3 = JV.GetCode(DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString(), "16");
					int num4 = JV.GenerateJV_Insert(code3, DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), "19", ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["VoucherNo"].Value.ToString(), " ( " + ((UltraGridBase)ULGData).Rows[l].Cells["ChargedPerson"].Value.ToString() + " ) " + ((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value.ToString(), "0", "1", dataTable, "0", ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString(), "1", GlobalVariables.UserID);
					Main.ExecuteNonQuery(" Update SB_BankOut set JVID3= " + num4 + ",Collected= 1 ,CollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "',CollectionExpense=" + ((UltraGridBase)ULGData).Rows[l].Cells["CollectionExpense"].Value.ToString() + " Where BankOutID =" + ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString());
					dataTable.Clear();
					dataRow.Delete();
				}
				if (((UltraGridBase)ULGData).Rows[l].Cells["Returned"].Value.Equals(true))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
						return;
					}
					num++;
					DataRow dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[l].Cells["UnderCollectionAccID"].Value.ToString();
					dataRow["Debit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["Total"].Value.ToString());
					dataRow["Credit"] = "0";
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[l].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = decimal.Parse(dataRow["Debit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString());
					dataRow["LocalCredit"] = "0";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					DataTable dataTable3 = BankOutDetails.SelectByBankOutID(((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
					for (int m = 0; m < dataTable3.Rows.Count; m++)
					{
						dataRow = dataTable.NewRow();
						dataRow["JVDetailID"] = "-1";
						dataRow["AccountID"] = dataTable3.Rows[m]["AccountID"];
						dataRow["SubAccountID"] = dataTable3.Rows[m]["SubAccountID"];
						dataRow["CostCenterID"] = dataTable3.Rows[m]["CostCenterID"];
						dataRow["SubCostCenterID"] = dataTable3.Rows[m]["SubCostCenterID"];
						dataRow["Debit"] = "0";
						dataRow["Credit"] = dataTable3.Rows[m]["Value"];
						dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[l].Cells["CurrencyID"].Value.ToString();
						dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString();
						dataRow["LocalDebit"] = "0";
						dataRow["LocalCredit"] = decimal.Parse(dataRow["Credit"].ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ExchangeRate"].Value.ToString());
						dataRow["IsDocumented"] = dataTable3.Rows[m]["IsDocumented"];
						dataRow["Notes"] = dataTable3.Rows[m]["Notes"];
						dataRow["Deleted"] = false;
						dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString();
						dataTable.Rows.Add(dataRow);
					}
					string code4 = JV.GetCode(DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString(), "16");
					int num5 = JV.GenerateJV_Insert(code4, DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), "19", ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["VoucherNo"].Value.ToString(), ((UltraGridBase)ULGData).Rows[l].Cells["Notes"].Value.ToString(), "0", "1", dataTable, "0", ((UltraGridBase)ULGData).Rows[l].Cells["BranchID"].Value.ToString(), "1", GlobalVariables.UserID);
					Main.ExecuteNonQuery(" Update SB_BankOut set JVID3= " + num5 + ",Returned= 1 ,CollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[l].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "' Where BankOutID =" + ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString());
					dataTable.Clear();
					dataRow.Delete();
				}
			}
		}
		if (num > 0)
		{
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد أذون لإعتمادها " : "There are No Voucher to Approve");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (rbBankIn.Checked)
			{
				frmBankIn frmBankIn2 = new frmBankIn(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmBankIn2.Size = new Size(base.Width, base.Height);
				frmBankIn2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmBankIn2.lblTitle).Text = (GlobalVariables.IsArabic ? "وارد بنك" : "Bank In");
				frmBankIn2.ShowDialog();
			}
			else
			{
				frmBankOut frmBankOut2 = new frmBankOut(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmBankOut2.Size = new Size(base.Width, base.Height);
				frmBankOut2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmBankOut2.lblTitle).Text = (GlobalVariables.IsArabic ? "صادر بنك" : "Bank Out");
				frmBankOut2.ShowDialog();
			}
		}
	}

	public override void Search()
	{
		if (rbBankIn.Checked)
		{
			DataTable dataTable = SearchFunctions.BankInReport(-1, 1, 0, 0, 1, 0);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (dataTable.Rows[i]["BankInID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[j].Cells["Collected"].Value = true;
					}
				}
			}
			return;
		}
		DataTable dataTable2 = SearchFunctions.BanKOutReport(-1, 1, 0, 0, 1, 0);
		for (int k = 0; k < dataTable2.Rows.Count; k++)
		{
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				if (dataTable2.Rows[k]["BankOutID"].ToString() == ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[l].Cells["Collected"].Value = true;
				}
			}
		}
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
	{
		FillGrid();
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CollectionExpense")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.Approved.frmNotesUnderCollection));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.pnlCheckType = new UltraPanel();
		this.rbBankIn = new System.Windows.Forms.RadioButton();
		this.rbBankOut = new System.Windows.Forms.RadioButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance9.FontData");
		resources.ApplyResources(val7, "appearance9");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbBankIn);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbBankOut);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbBankIn, "rbBankIn");
		this.rbBankIn.BackColor = System.Drawing.Color.Transparent;
		this.rbBankIn.Name = "rbBankIn";
		this.rbBankIn.TabStop = true;
		this.rbBankIn.UseVisualStyleBackColor = false;
		this.rbBankIn.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbBankOut, "rbBankOut");
		this.rbBankOut.BackColor = System.Drawing.Color.Transparent;
		this.rbBankOut.Name = "rbBankOut";
		this.rbBankOut.TabStop = true;
		this.rbBankOut.UseVisualStyleBackColor = false;
		this.rbBankOut.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Name = "frmNotesUnderCollection";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBByName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
