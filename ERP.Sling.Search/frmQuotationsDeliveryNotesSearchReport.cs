using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Search;

public class frmQuotationsDeliveryNotesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmQuotationsDeliveryNotesSearchReport()
	{
		InitializeComponent();
	}

	public frmQuotationsDeliveryNotesSearchReport(DataTable dt, string idColumnName, int DeletedValue)
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDeliveryNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDeliveryNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم " : "Quotation Delivery No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDeliveryNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDeliveryNoteDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDeliveryNoteDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ " : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDeliveryNoteDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotaionNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotaionNo"].Header).Caption = (GlobalVariables.IsArabic ? "عرض السعر" : "Quotation No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotaionNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
