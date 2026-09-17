using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.WareHouse.Search;

public class frmWareHouseIssueSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmWareHouseIssueSearchReport()
	{
		InitializeComponent();
	}

	public frmWareHouseIssueSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WareHouseIssueNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WareHouseIssueNo"].Header).Caption = (GlobalVariables.IsArabic ? "NO" : "NO");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WareHouseIssueNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "Invoice No" : "Invoice No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "Invoice Date" : "Invoice Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AWBNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AWBNo"].Header).Caption = (GlobalVariables.IsArabic ? "AWBNo" : "AWBNo");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AWBNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AWBDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AWBDate"].Header).Caption = (GlobalVariables.IsArabic ? "AWBDate" : "AWBDate");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AWBDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManifestNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManifestNo"].Header).Caption = (GlobalVariables.IsArabic ? "ManifestNo" : "ManifestNo");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManifestNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManifestDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManifestDate"].Header).Caption = (GlobalVariables.IsArabic ? "Manifest Date" : "Manifest Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ManifestDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Header).Caption = (GlobalVariables.IsArabic ? "Company" : "Company");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Supplier"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Supplier"].Header).Caption = (GlobalVariables.IsArabic ? "Supplier" : "Supplier");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Supplier"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierStore"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierStore"].Header).Caption = (GlobalVariables.IsArabic ? "Supplier Store" : "Supplier Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplierStore"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceGRN"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceGRN"].Header).Caption = (GlobalVariables.IsArabic ? "Invoice GRN" : "Invoice GRN");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceGRN"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "Notes" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "Branch" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "Approved" : "Approved");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.03);
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
