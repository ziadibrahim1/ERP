using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.LensesProductions.Search;

public class frmBlanksInvoicesPaymentsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmBlanksInvoicesPaymentsSearchReport()
	{
		InitializeComponent();
	}

	public frmBlanksInvoicesPaymentsSearchReport(DataTable dt, string idColumnName, int DeletedValue)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
		Deleted = DeletedValue;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم السداد" : "Payment No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفاتورة" : "Invoice No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoicePaymentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "فيزا" : "Visa Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الفيزا" : "Visa No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisaTypeNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalAmount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalAmount"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة" : "Amount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
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
		this.components = new System.ComponentModel.Container();
	}
}
