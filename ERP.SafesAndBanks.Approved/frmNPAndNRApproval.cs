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

public class frmNPAndNRApproval : frmPosted
{
	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtBanks;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlBanks = new ValueList();

	private bool UseSubAccounts;

	private IContainer components = null;

	private UltraPanel pnlCheckType;

	private RadioButton rbBankIn;

	private RadioButton rbBankOut;

	public frmNPAndNRApproval()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		NoCol = "VoucherNo";
		rbBankIn.Checked = true;
	}

	public frmNPAndNRApproval(int ID, bool IsBankIn)
		: this()
	{
		rbBankIn.Checked = IsBankIn;
		rbBankOut.Checked = !IsBankIn;
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		UseSubAccounts = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_SubAccounts Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseSubAccounts)
		{
			dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubAccounts.ValueListItems.Clear();
			for (int j = 0; j < dtSubAccounts.Rows.Count; j++)
			{
				vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[j]["SubAccountID"], dtSubAccounts.Rows[j]["Name"].ToString());
			}
		}
		dtBanks = Banks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlBanks.ValueListItems.Clear();
		for (int k = 0; k < dtBanks.Rows.Count; k++)
		{
			vlBanks.ValueListItems.Add(dtBanks.Rows[k]["BankID"], dtBanks.Rows[k]["BankName"].ToString());
		}
	}

	public override void FillGrid()
	{
		dtsource = null;
		if (rbBankIn.Checked)
		{
			dtsource = BankIn.SelectForNR(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtsource = BankOut.SelectForNP(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		}
		if (RowID != "")
		{
			dtsource.Select(" VoucherID =" + RowID)[0]["UnderCollection"] = true;
			RowID = "";
		}
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	private ValueList getBanksValueList(string CurrencyID, string NRAccID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtBanks.Select("CurrencyID=" + CurrencyID + " and NRAccID = " + NRAccID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["BankID"].ToString(), array[i]["BankName"].ToString());
		}
		return val;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		if (rbBankIn.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "بنك" : "Bank");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].ValueList = (IValueList)(object)vlBanks;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشيك" : "Check No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشيك" : "Check Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الترحيل" : "Approved Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsEndorsement"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsEndorsement"].Header).Caption = (GlobalVariables.IsArabic ? "تظهير" : "Endorsement");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsEndorsement"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			if (UseSubAccounts)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			}
			else
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = true;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			}
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "التحليلى" : "SubAccount");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب" : "Account");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Header).Caption = "";
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].AllowRowFiltering = (DefaultableBoolean)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "بنك" : "Bank");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشيك" : "Check No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشيك" : "Check Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الترحيل" : "Approved Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Header).Caption = "";
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].AllowRowFiltering = (DefaultableBoolean)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollection"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		}
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnderCollection" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ApprovalDate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsEndorsement")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = false;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AccountID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SubAccountID")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = !bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsEndorsement"].Value.ToString());
		}
		else if (rbBankIn.Checked && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BankName")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = false;
		}
		else
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		int num = 0;
		DataTable dataTable = JVDetails.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0");
		if (rbBankIn.Checked)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["BankName"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار البنك", "Please Select Bank Name");
					((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
					return;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["IsEndorsement"].Value.Equals(true) && ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار الحساب", "Please Select Account Name");
					((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
					return;
				}
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnderCollection"].Value.ToString()))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
						return;
					}
					num++;
					DataRow dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = (((UltraGridBase)ULGData).Rows[i].Cells["IsEndorsement"].Value.Equals(true) ? ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value : dtBanks.Select(" BankID= " + ((UltraGridBase)ULGData).Rows[i].Cells["BankName"].Value.ToString())[0]["NRUnderCollectionAccID"].ToString());
					dataRow["SubAccountID"] = (((UltraGridBase)ULGData).Rows[i].Cells["IsEndorsement"].Value.Equals(true) ? ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value : DBNull.Value);
					dataRow["Debit"] = ((UltraGridBase)ULGData).Rows[i].Cells["Total"].Value.ToString();
					dataRow["Credit"] = "0";
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[i].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Total"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"].Value.ToString());
					dataRow["LocalCredit"] = "0";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[i].Cells["NotesAccID"].Value.ToString();
					dataRow["Debit"] = "0";
					dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[i].Cells["Total"].Value.ToString();
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[i].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Total"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ExchangeRate"].Value.ToString());
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					string code = JV.GetCode(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), "16");
					int num2 = JV.GenerateJV_Insert(code, DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), "16", ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["VoucherNo"].Value.ToString(), " ( " + ((UltraGridBase)ULGData).Rows[i].Cells["ChargedPerson"].Value.ToString() + " ) " + ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), "0", "1", dataTable, "0", ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), "1", GlobalVariables.UserID);
					if (((UltraGridBase)ULGData).Rows[i].Cells["IsEndorsement"].Value.Equals(true))
					{
						Main.ExecuteNonQuery(" Update SB_BankIn set JVID2= " + num2 + ",UnderCollection= 1 ,UnderCollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "',IsEndorsement=1,AccountID=" + ((UltraGridBase)ULGData).Rows[i].Cells["AccountID"].Value.ToString() + ", BankID = " + ((UltraGridBase)ULGData).Rows[i].Cells["BankName"].Value.ToString() + ",SubAccountID=" + ((((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value) ? " NULL " : ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString()) + ",Collected=1 ,CollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "'  Where BankInID =" + ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString() + "; exec SP_Trans_Log " + GlobalVariables.UserID + " ,'SB_BankIn' ," + ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString() + " ,'U';");
					}
					else
					{
						Main.ExecuteNonQuery(" Update SB_BankIn set JVID2= " + num2 + ",UnderCollection= 1 ,UnderCollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "', BankID = " + ((UltraGridBase)ULGData).Rows[i].Cells["BankName"].Value.ToString() + " Where BankInID =" + ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString() + "; exec SP_Trans_Log " + GlobalVariables.UserID + " ,'SB_BankIn' ," + ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString() + " ,'U';");
					}
					dataTable.Clear();
					dataRow.Delete();
				}
			}
		}
		else
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnderCollection"].Value.ToString()))
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
					DataRow dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["NotesAccID"].Value.ToString();
					dataRow["Debit"] = ((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString();
					dataRow["Credit"] = "0";
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
					dataRow["LocalCredit"] = "0";
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					dataRow = dataTable.NewRow();
					dataRow["JVDetailID"] = "-1";
					dataRow["AccountID"] = ((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionAccID"].Value.ToString();
					dataRow["Debit"] = "0";
					dataRow["Credit"] = ((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString();
					dataRow["CurrencyID"] = ((UltraGridBase)ULGData).Rows[j].Cells["CurrencyID"].Value.ToString();
					dataRow["ExchangeRate"] = ((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString();
					dataRow["LocalDebit"] = "0";
					dataRow["LocalCredit"] = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Total"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ExchangeRate"].Value.ToString());
					dataRow["Deleted"] = false;
					dataRow["BranchID"] = ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString();
					dataTable.Rows.Add(dataRow);
					string code2 = JV.GetCode(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), "16");
					int num3 = JV.GenerateJV_Insert(code2, DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), "18", ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["VoucherNo"].Value.ToString(), " ( " + ((UltraGridBase)ULGData).Rows[j].Cells["ChargedPerson"].Value.ToString() + " ) " + ((UltraGridBase)ULGData).Rows[j].Cells["Notes"].Value.ToString(), "0", "1", dataTable, "0", ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), "1", GlobalVariables.UserID);
					Main.ExecuteNonQuery(" Update SB_BankOut set JVID2= " + num3 + ",UnderCollection= 1 ,UnderCollectionDate ='" + DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ApprovalDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) + "' Where BankOutID =" + ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString());
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
			DataTable dataTable = SearchFunctions.BankInReport(-1, 0, 0, 0, 1, 0);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (dataTable.Rows[i]["BankInID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[j].Cells["UnderCollection"].Value = true;
					}
				}
			}
			return;
		}
		DataTable dataTable2 = SearchFunctions.BanKOutReport(-1, 0, 0, 0, 1, 0);
		for (int k = 0; k < dataTable2.Rows.Count; k++)
		{
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				if (dataTable2.Rows[k]["BankOutID"].ToString() == ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[l].Cells["UnderCollection"].Value = true;
				}
			}
		}
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
	{
		FillGrid();
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

	private void ULGData_BeforeEnterEditMode(object sender, CancelEventArgs e)
	{
		if (rbBankIn.Checked && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BankName")
		{
			ULGData.BeforeEnterEditMode -= ULGData_BeforeEnterEditMode;
			ULGData.ActiveCell.Row.Cells["BankName"].ValueList = (IValueList)(object)getBanksValueList(ULGData.ActiveCell.Row.Cells["CurrencyID"].Value.ToString(), ULGData.ActiveCell.Row.Cells["NotesAccID"].Value.ToString());
			ULGData.BeforeEnterEditMode += ULGData_BeforeEnterEditMode;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		if (UseSubAccounts && ((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID")
		{
			ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
			int num = ((vlAccounts.SelectedItem != null) ? int.Parse(dtAccounts.Rows[vlAccounts.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["SubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["SubAccountID"].ValueList = null;
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
			}
			ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		}
	}

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsEndorsement")
		{
			if (e.Cell.Value.Equals(false))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value = DBNull.Value;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnderCollection"].Value = true;
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "UnderCollection" && e.Cell.Value.Equals(false))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["IsEndorsement"].Value = false;
			((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value = DBNull.Value;
		}
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
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
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Expected O, but got Unknown
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.Approved.frmNPAndNRApproval));
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
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
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
		base.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		base.ULGData.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(ULGData_BeforeEnterEditMode);
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
		base.Name = "frmNPAndNRApproval";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
