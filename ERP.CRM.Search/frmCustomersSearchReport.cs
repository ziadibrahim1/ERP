using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CRM.Search;

public class frmCustomersSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmCustomersSearchReport()
	{
		InitializeComponent();
	}

	public frmCustomersSearchReport(DataTable dt, string idColumnName, int DeletedValue)
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود العميل" : "Customer Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم العميل" : "Customer Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodName"].Header).Caption = (GlobalVariables.IsArabic ? "طريقة الدفع" : "Payment Method");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GenderName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GenderName"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Gender");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GenderName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReligionName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReligionName"].Header).Caption = (GlobalVariables.IsArabic ? "الديانة" : "Religion");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReligionName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityName"].Header).Caption = (GlobalVariables.IsArabic ? "المدينة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkCompanyName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkCompanyName"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة التابع لها" : "WorkC ompany");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkCompanyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreationDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreationDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الإنشاء" : "Creation Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
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
