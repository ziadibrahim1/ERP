using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

public class frmOperationsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmOperationsSearchReport()
	{
		InitializeComponent();
	}

	public frmOperationsSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "Op. No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Operation Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? " ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلقة" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOriginCountry"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOriginCountry"].Header).Caption = (GlobalVariables.IsArabic ? "منشأ" : "Origin");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOriginCountry"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCopies"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCopies"].Header).Caption = (GlobalVariables.IsArabic ? "نسخ" : "Copies");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCopies"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateOpeningSeaPortName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateOpeningSeaPortName"].Header).Caption = (GlobalVariables.IsArabic ? "مينا فتح الشهادة" : "Certificate Opening Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateOpeningSeaPortName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نظام الشحن" : "Export Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشهادة" : "Certificate No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "المصدر" : "Exporter Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortName"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء" : "Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyName"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompass"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompass"].Header).Caption = (GlobalVariables.IsArabic ? "بوصلة" : "Compass");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompass"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
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
