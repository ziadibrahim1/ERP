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
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.Approved;

public class frmNPAndNRChangeApproval : frmBase
{
	private DataTable dtsource = new DataTable();

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private bool UseSubAccounts;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnSave;

	private UltraPanel pnlCheckType;

	private RadioButton rbBankIn;

	private RadioButton rbBankOut;

	public UltraButton btnGet;

	public UltraDateTimeEditor dtpToDate;

	public UltraDateTimeEditor dtpFromDate;

	public UltraLabel ultraLabel2;

	public UltraLabel ultraLabel1;

	public UltraButton btnClose;

	public frmNPAndNRChangeApproval()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		UltraDateTimeEditor obj = dtpFromDate;
		DateTime dateTime = (dtpToDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
		obj.DateTime = dateTime;
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
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Override.AllowUpdate = (DefaultableBoolean)1;
		if (rbBankIn.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "بنك" : "Bank");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
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
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Total");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollectionDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollectionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الترحيل" : "UnderCollection Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollectionDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التحصيل" : "Collection Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Header).Caption = (GlobalVariables.IsArabic ? "اعتماد" : "Approve");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].AllowRowFiltering = (DefaultableBoolean)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "بنك" : "Bank");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشيك" : "Check No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشيك" : "Check Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollectionDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollectionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الترحيل" : "UnderCollection Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnderCollectionDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التحصيل " : "Collection Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CollectionDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Header).Caption = (GlobalVariables.IsArabic ? "اعتماد" : "Approve");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].AllowRowFiltering = (DefaultableBoolean)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Change"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		}
	}

	private void btnGet_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Change"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("يوجد تعديلات في الترحيلات", "There are modifications");
				return;
			}
		}
		FillGrid();
	}

	public void FillGrid()
	{
		if (rbBankIn.Checked)
		{
			dtsource = BankIn.SelectForChangeNR(GlobalVariables.BranchIDs, dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtsource = BankOut.SelectForChangeNP(GlobalVariables.BranchIDs, dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"), GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public void SaveData()
	{
		int num = 0;
		if (rbBankIn.Checked)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Change"].Value.ToString()))
				{
					continue;
				}
				if (!((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).Rows[i].Cells["CollectionDate"].Value.Equals(DBNull.Value) && Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["CollectionDate"].Value) < Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"].Value))
				{
					GlobalVariables.InformationMB.Show("تاريخ ترحيل اوراق القبض قبل تاريخ تحت التحصيل", "The UnderCollection Date is after the Collection Date and this is not allowed");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"];
					return;
				}
				if (!((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"].Value.Equals(DBNull.Value))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you chose\n\r exists in closed fisical period");
						return;
					}
					if (((UltraGridBase)ULGData).Rows[i].Cells["JVID3"].Value != DBNull.Value)
					{
						if (((UltraGridBase)ULGData).Rows[i].Cells["CollectionDate"].Value.Equals(DBNull.Value))
						{
							GlobalVariables.InformationMB.Show("برجاء اختيار تاريخ التحصيل", "Please Select The Collection Date");
							return;
						}
						if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
						{
							GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
							return;
						}
						if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), IsFromServer: false))
						{
							GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you chose\n\r exists in closed fisical period");
							return;
						}
					}
					num++;
					BankIn.UpdateCollectionJV(((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["JVID2"].Value != DBNull.Value) ? DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnderCollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) : "Null", (((UltraGridBase)ULGData).Rows[i].Cells["JVID3"].Value != DBNull.Value) ? DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) : "Null", (((UltraGridBase)ULGData).Rows[i].Cells["JVID2"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).Rows[i].Cells["JVID2"].Value.ToString() : "Null", (((UltraGridBase)ULGData).Rows[i].Cells["JVID3"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).Rows[i].Cells["JVID3"].Value.ToString() : "Null", ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), GlobalVariables.UserID);
					continue;
				}
				GlobalVariables.InformationMB.Show("برجاء اختيار تاريخ الترحيل", "Please Select The UnderCollection Date");
				return;
			}
		}
		else
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (!bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Change"].Value.ToString()))
				{
					continue;
				}
				if (!((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).Rows[j].Cells["CollectionDate"].Value.Equals(DBNull.Value) && Convert.ToDateTime(((UltraGridBase)ULGData).Rows[j].Cells["CollectionDate"].Value) < Convert.ToDateTime(((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"].Value))
				{
					GlobalVariables.InformationMB.Show("تاريخ ترحيل اوراق الدفع قبل تاريخ تحت التحصيل", "The UnderCollection Date is after the Collection Date and this is not allowed");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"];
					return;
				}
				if (!((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"].Value.Equals(DBNull.Value))
				{
					if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
						return;
					}
					if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), IsFromServer: false))
					{
						GlobalVariables.InformationMB.Show("لقد قمت بإختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you chose \n\r exists in closed fiscal period");
						return;
					}
					if (((UltraGridBase)ULGData).Rows[j].Cells["JVID3"].Value != DBNull.Value)
					{
						if (((UltraGridBase)ULGData).Rows[j].Cells["CollectionDate"].Value.Equals(DBNull.Value))
						{
							GlobalVariables.InformationMB.Show("برجاء اختيار تاريخ التحصيل", "Please Select The Collection Date");
							return;
						}
						if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
						{
							GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
							return;
						}
						if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), IsFromServer: false))
						{
							GlobalVariables.InformationMB.Show("لقد قمت بإختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you chose\n\r exists in closed fisical period");
							return;
						}
					}
					num++;
					BankOut.UpdateCollectionJV(((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[j].Cells["JVID2"].Value != DBNull.Value) ? DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["UnderCollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) : "Null", (((UltraGridBase)ULGData).Rows[j].Cells["JVID3"].Value != DBNull.Value) ? DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["CollectionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate) : "Null", (((UltraGridBase)ULGData).Rows[j].Cells["JVID2"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).Rows[j].Cells["JVID2"].Value.ToString() : "Null", (((UltraGridBase)ULGData).Rows[j].Cells["JVID3"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).Rows[j].Cells["JVID3"].Value.ToString() : "Null", ((UltraGridBase)ULGData).Rows[j].Cells["BranchID"].Value.ToString(), GlobalVariables.UserID);
					continue;
				}
				GlobalVariables.InformationMB.Show("برجاء اختيار تاريخ الترحيل", "Please Select The UnderCollection Date");
				return;
			}
		}
		if (num > 0)
		{
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد أذون لإعتمادها " : "There are No Vouchers to be Approved");
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		SaveData();
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
	{
		dtsource = null;
		((UltraGridBase)ULGData).DataSource = dtsource;
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Change") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnderCollectionDate") && (((UltraGridBase)ULGData).ActiveRow == null || !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CollectionDate")))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.Approved.frmNPAndNRChangeApproval));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		this.pnlCheckType = new UltraPanel();
		this.rbBankIn = new System.Windows.Forms.RadioButton();
		this.rbBankOut = new System.Windows.Forms.RadioButton();
		this.ULGData = new UltraGrid();
		this.btnSave = new UltraButton();
		this.btnGet = new UltraButton();
		this.dtpToDate = new UltraDateTimeEditor();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbBankIn);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbBankOut);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbBankIn, "rbBankIn");
		this.rbBankIn.BackColor = System.Drawing.Color.Transparent;
		this.rbBankIn.Name = "rbBankIn";
		this.rbBankIn.UseVisualStyleBackColor = false;
		this.rbBankIn.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbBankOut, "rbBankOut");
		this.rbBankOut.BackColor = System.Drawing.Color.Transparent;
		this.rbBankOut.Checked = true;
		this.rbBankOut.Name = "rbBankOut";
		this.rbBankOut.TabStop = true;
		this.rbBankOut.UseVisualStyleBackColor = false;
		this.rbBankOut.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
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
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnGet, "btnGet");
		((System.Windows.Forms.Control)(object)this.btnGet).Name = "btnGet";
		((System.Windows.Forms.Control)(object)this.btnGet).Click += new System.EventHandler(btnGet_Click);
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		this.dtpToDate.DateTime = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		this.dtpToDate.Value = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		this.dtpFromDate.DateTime = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		this.dtpFromDate.Value = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val3;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val4;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmNPAndNRChangeApproval";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
