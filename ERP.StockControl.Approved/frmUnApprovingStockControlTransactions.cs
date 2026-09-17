using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.StockControl.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Approved;

public class frmUnApprovingStockControlTransactions : frmPosted
{
	private IContainer components = null;

	private UltraPanel pnlCheckType;

	private RadioButton rbSupplierReturn;

	private RadioButton rbClientDeptReturn;

	private RadioButton rbMaterialIssueVoucher;

	private RadioButton rbGoodReceiptNote;

	private RadioButton rbOpennigBalance;

	private RadioButton rbStoreTaking;

	private RadioButton rbStoreSettlementVoucher;

	public frmUnApprovingStockControlTransactions()
	{
		InitializeComponent();
		NoCol = "VoucherNo";
		rbOpennigBalance.Checked = true;
	}

	public override void FillGrid()
	{
		if (rbOpennigBalance.Checked)
		{
			dtsource = OpeningBalances.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else if (rbGoodReceiptNote.Checked)
		{
			dtsource = GoodReceiptNotes.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else if (rbMaterialIssueVoucher.Checked)
		{
			dtsource = MaterialIssueVouchers.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else if (rbStoreSettlementVoucher.Checked)
		{
			dtsource = StoresSettlementVouchers.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else if (rbStoreTaking.Checked)
		{
			dtsource = StoreTakings.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else if (rbClientDeptReturn.Checked)
		{
			dtsource = ClientsDepartmentsReturns.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else if (rbSupplierReturn.Checked)
		{
			dtsource = SuppliersReturns.SelectByApproved(GlobalVariables.BranchIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGData).DataSource = null;
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		if (rbOpennigBalance.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpeningBalanceDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpeningBalanceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpeningBalanceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else if (rbGoodReceiptNote.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierName"].Header).Caption = (GlobalVariables.IsArabic ? "المورد" : "Supplier");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "فاتورة مشتريات" : "PS Invoice No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "امر شراء" : "PS Order No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else if (rbMaterialIssueVoucher.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "فاتورة المبيعات" : "SL Invoice No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestNo"].Header).Caption = (GlobalVariables.IsArabic ? "طلب صرف" : "Material Issue Request No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueRequestNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentName"].Header).Caption = (GlobalVariables.IsArabic ? "صرف لقسم" : "Department Name");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDirect"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDirect"].Header).Caption = (GlobalVariables.IsArabic ? "مباشر" : "IsDirect");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDirect"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		}
		else if (rbStoreSettlementVoucher.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SettlementVoucherDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SettlementVoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SettlementVoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.45);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else if (rbStoreTaking.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTakingDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTakingDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTakingDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreName"].Header).Caption = (GlobalVariables.IsArabic ? "المخزن" : "Store Name");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.35);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else if (rbClientDeptReturn.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReturnDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم العميل" : "Client");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم القسم" : "Department Name");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "فاتورة مبيعات" : "Invoice No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SLInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "اذن صرف" : "Material Issue Voucher No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialIssueVoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else if (rbSupplierReturn.Checked)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن" : "NO");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierReturnDate"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierReturnDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierReturnDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المورد" : "Supplier");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "فاتورة مشتريات" : "Invoice No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PSInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteNo"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteNo"].Header).Caption = (GlobalVariables.IsArabic ? "اذن اضافة" : "Good Receipt Note No");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodReceiptNoteNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = "";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString() + ",";
			}
		}
		if (text != ",")
		{
			if (rbOpennigBalance.Checked)
			{
				OpeningBalances.SetApprove("0", text);
			}
			else if (rbGoodReceiptNote.Checked)
			{
				GoodReceiptNotes.SetApprove("0", text);
			}
			else if (rbMaterialIssueVoucher.Checked)
			{
				MaterialIssueVouchers.SetApprove("0", text);
			}
			else if (rbStoreSettlementVoucher.Checked)
			{
				StoresSettlementVouchers.SetApprove("0", text);
			}
			else if (rbStoreTaking.Checked)
			{
				StoreTakings.SetApprove("0", text);
			}
			else if (rbClientDeptReturn.Checked)
			{
				ClientsDepartmentsReturns.SetApprove("0", text);
			}
			else if (rbSupplierReturn.Checked)
			{
				SuppliersReturns.SetApprove("0", text);
			}
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد أذونات لفك إعتمادها " : "There are No Vouchers UnApprove");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (rbOpennigBalance.Checked)
			{
				frmOpeningBalances frmOpeningBalances2 = new frmOpeningBalances(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmOpeningBalances2.Size = new Size(base.Width, base.Height);
				frmOpeningBalances2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmOpeningBalances2.lblTitle).Text = (GlobalVariables.IsArabic ? "الارصدة الافتتاحية" : "Opening Balances");
				frmOpeningBalances2.ShowDialog();
			}
			else if (rbGoodReceiptNote.Checked)
			{
				frmGoodReceiptNotes frmGoodReceiptNotes2 = new frmGoodReceiptNotes(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmGoodReceiptNotes2.Size = new Size(base.Width, base.Height);
				frmGoodReceiptNotes2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmGoodReceiptNotes2.lblTitle).Text = (GlobalVariables.IsArabic ? "اذونــات الاضــافـة" : "Good Receipt Notes");
				frmGoodReceiptNotes2.ShowDialog();
			}
			else if (rbMaterialIssueVoucher.Checked)
			{
				frmMaterialIssueVouchers frmMaterialIssueVouchers2 = new frmMaterialIssueVouchers(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmMaterialIssueVouchers2.Size = new Size(base.Width, base.Height);
				frmMaterialIssueVouchers2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmMaterialIssueVouchers2.lblTitle).Text = (GlobalVariables.IsArabic ? "اذونـــات الصـــرف" : "Material Issue Vouchers");
				frmMaterialIssueVouchers2.ShowDialog();
			}
			else if (rbStoreSettlementVoucher.Checked)
			{
				frmStoresSettlementVouchers frmStoresSettlementVouchers2 = new frmStoresSettlementVouchers(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmStoresSettlementVouchers2.Size = new Size(base.Width, base.Height);
				frmStoresSettlementVouchers2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmStoresSettlementVouchers2.lblTitle).Text = (GlobalVariables.IsArabic ? "اذونات تسوية" : "Stores Settlement Vouchers");
				frmStoresSettlementVouchers2.ShowDialog();
			}
			else if (rbStoreTaking.Checked)
			{
				frmStoreTakings frmStoreTakings2 = new frmStoreTakings(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmStoreTakings2.Size = new Size(base.Width, base.Height);
				frmStoreTakings2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmStoreTakings2.lblTitle).Text = (GlobalVariables.IsArabic ? "جرد المخازن" : "Store Takings");
				frmStoreTakings2.ShowDialog();
			}
			else if (rbClientDeptReturn.Checked)
			{
				frmClientsDepartmentsReturns frmClientsDepartmentsReturns2 = new frmClientsDepartmentsReturns(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmClientsDepartmentsReturns2.Size = new Size(base.Width, base.Height);
				frmClientsDepartmentsReturns2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmClientsDepartmentsReturns2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع عميل واقسام" : "Clients Departments Returns");
				frmClientsDepartmentsReturns2.ShowDialog();
			}
			else if (rbSupplierReturn.Checked)
			{
				frmSuppliersReturns frmSuppliersReturns2 = new frmSuppliersReturns(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmSuppliersReturns2.Size = new Size(base.Width, base.Height);
				frmSuppliersReturns2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmSuppliersReturns2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع مورد" : "Suppliers Returns");
				frmSuppliersReturns2.ShowDialog();
			}
		}
	}

	public override void Search()
	{
		if (rbOpennigBalance.Checked)
		{
			DataTable dataTable = SearchFunctions.OpeningBalancesReport(0);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (dataTable.Rows[i]["OpeningBalanceID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[j].Cells["Approved"].Value = true;
					}
				}
			}
		}
		else if (rbGoodReceiptNote.Checked)
		{
			DataTable dataTable2 = SearchFunctions.GoodReceiptNotesReport(1, 0);
			for (int k = 0; k < dataTable2.Rows.Count; k++)
			{
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
				{
					if (dataTable2.Rows[k]["GoodReceiptNoteID"].ToString() == ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[l].Cells["Approved"].Value = true;
					}
				}
			}
		}
		else if (rbMaterialIssueVoucher.Checked)
		{
			DataTable dataTable3 = SearchFunctions.MaterialIssueVouchersReport(1, 0);
			for (int m = 0; m < dataTable3.Rows.Count; m++)
			{
				for (int n = 0; n < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; n++)
				{
					if (dataTable3.Rows[m]["MaterialIssueVoucherID"].ToString() == ((UltraGridBase)ULGData).Rows[n].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[n].Cells["Approved"].Value = true;
					}
				}
			}
		}
		else if (rbStoreSettlementVoucher.Checked)
		{
			DataTable dataTable4 = SearchFunctions.StoresSettlementVouchersReport(-1, 1, 0);
			for (int num = 0; num < dataTable4.Rows.Count; num++)
			{
				for (int num2 = 0; num2 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num2++)
				{
					if (dataTable4.Rows[num]["SettlementVoucherID"].ToString() == ((UltraGridBase)ULGData).Rows[num2].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[num2].Cells["Approved"].Value = true;
					}
				}
			}
		}
		else if (rbStoreTaking.Checked)
		{
			DataTable dataTable5 = SearchFunctions.StoreTakingsReport(GlobalVariables.StoreIDs, 1, 0);
			for (int num3 = 0; num3 < dataTable5.Rows.Count; num3++)
			{
				for (int num4 = 0; num4 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num4++)
				{
					if (dataTable5.Rows[num3]["StoreTakingID"].ToString() == ((UltraGridBase)ULGData).Rows[num4].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[num4].Cells["Approved"].Value = true;
					}
				}
			}
		}
		else if (rbClientDeptReturn.Checked)
		{
			DataTable dataTable6 = SearchFunctions.ClientsDepartmentsReturnsReport(1, 0, -1);
			for (int num5 = 0; num5 < dataTable6.Rows.Count; num5++)
			{
				for (int num6 = 0; num6 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num6++)
				{
					if (dataTable6.Rows[num5]["ReturnID"].ToString() == ((UltraGridBase)ULGData).Rows[num6].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[num6].Cells["Approved"].Value = true;
					}
				}
			}
		}
		else
		{
			if (!rbSupplierReturn.Checked)
			{
				return;
			}
			DataTable dataTable7 = SearchFunctions.SuppliersReturnsReport(1, 0);
			for (int num7 = 0; num7 < dataTable7.Rows.Count; num7++)
			{
				for (int num8 = 0; num8 < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; num8++)
				{
					if (dataTable7.Rows[num7]["SupplierReturnID"].ToString() == ((UltraGridBase)ULGData).Rows[num8].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[num8].Cells["Approved"].Value = true;
					}
				}
			}
		}
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
	{
		FillGrid();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Approved.frmUnApprovingStockControlTransactions));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.pnlCheckType = new UltraPanel();
		this.rbSupplierReturn = new System.Windows.Forms.RadioButton();
		this.rbClientDeptReturn = new System.Windows.Forms.RadioButton();
		this.rbMaterialIssueVoucher = new System.Windows.Forms.RadioButton();
		this.rbGoodReceiptNote = new System.Windows.Forms.RadioButton();
		this.rbOpennigBalance = new System.Windows.Forms.RadioButton();
		this.rbStoreTaking = new System.Windows.Forms.RadioButton();
		this.rbStoreSettlementVoucher = new System.Windows.Forms.RadioButton();
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
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance9.FontData");
		resources.ApplyResources(val7, "appearance9");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbSupplierReturn);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbClientDeptReturn);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbMaterialIssueVoucher);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbGoodReceiptNote);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbOpennigBalance);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbStoreTaking);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbStoreSettlementVoucher);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbSupplierReturn, "rbSupplierReturn");
		this.rbSupplierReturn.BackColor = System.Drawing.Color.Transparent;
		this.rbSupplierReturn.Name = "rbSupplierReturn";
		this.rbSupplierReturn.TabStop = true;
		this.rbSupplierReturn.UseVisualStyleBackColor = false;
		this.rbSupplierReturn.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbClientDeptReturn, "rbClientDeptReturn");
		this.rbClientDeptReturn.BackColor = System.Drawing.Color.Transparent;
		this.rbClientDeptReturn.Name = "rbClientDeptReturn";
		this.rbClientDeptReturn.TabStop = true;
		this.rbClientDeptReturn.UseVisualStyleBackColor = false;
		this.rbClientDeptReturn.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbMaterialIssueVoucher, "rbMaterialIssueVoucher");
		this.rbMaterialIssueVoucher.BackColor = System.Drawing.Color.Transparent;
		this.rbMaterialIssueVoucher.Name = "rbMaterialIssueVoucher";
		this.rbMaterialIssueVoucher.TabStop = true;
		this.rbMaterialIssueVoucher.UseVisualStyleBackColor = false;
		this.rbMaterialIssueVoucher.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbGoodReceiptNote, "rbGoodReceiptNote");
		this.rbGoodReceiptNote.BackColor = System.Drawing.Color.Transparent;
		this.rbGoodReceiptNote.Name = "rbGoodReceiptNote";
		this.rbGoodReceiptNote.TabStop = true;
		this.rbGoodReceiptNote.UseVisualStyleBackColor = false;
		this.rbGoodReceiptNote.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbOpennigBalance, "rbOpennigBalance");
		this.rbOpennigBalance.BackColor = System.Drawing.Color.Transparent;
		this.rbOpennigBalance.Name = "rbOpennigBalance";
		this.rbOpennigBalance.TabStop = true;
		this.rbOpennigBalance.UseVisualStyleBackColor = false;
		this.rbOpennigBalance.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbStoreTaking, "rbStoreTaking");
		this.rbStoreTaking.BackColor = System.Drawing.Color.Transparent;
		this.rbStoreTaking.Name = "rbStoreTaking";
		this.rbStoreTaking.TabStop = true;
		this.rbStoreTaking.UseVisualStyleBackColor = false;
		this.rbStoreTaking.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbStoreSettlementVoucher, "rbStoreSettlementVoucher");
		this.rbStoreSettlementVoucher.BackColor = System.Drawing.Color.Transparent;
		this.rbStoreSettlementVoucher.Name = "rbStoreSettlementVoucher";
		this.rbStoreSettlementVoucher.TabStop = true;
		this.rbStoreSettlementVoucher.UseVisualStyleBackColor = false;
		this.rbStoreSettlementVoucher.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Name = "frmUnApprovingStockControlTransactions";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
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
