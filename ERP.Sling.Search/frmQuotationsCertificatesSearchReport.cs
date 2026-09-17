using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Search;

public class frmQuotationsCertificatesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmQuotationsCertificatesSearchReport()
	{
		InitializeComponent();
	}

	public frmQuotationsCertificatesSearchReport(DataTable dt, string idColumnName, int DeletedValue)
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشهادة " : "Certificate No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشهادة" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateTestDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateTestDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاختبار" : "Test Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateTestDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateTestEndDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateTestEndDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ انتهاء الاختبار" : "Test End Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateTestEndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotaionNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotaionNo"].Header).Caption = (GlobalVariables.IsArabic ? "عرض السعر" : "Quotaion No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotaionNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
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
