using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

public class frmPhytosanitaryCertificatesReport : frmSearchReport
{
	private IContainer components = null;

	public frmPhytosanitaryCertificatesReport()
	{
		InitializeComponent();
	}

	public frmPhytosanitaryCertificatesReport(DataTable dt, string idColumnName)
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
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Op. No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BillOfLadingNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BillOfLadingNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم بوليصة الشحن" : "Bill Of LadingNo");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BillOfLadingNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? " التاريخ الشهادة" : "Certificate Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InspectionDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InspectionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الفحص " : "Inspection Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InspectionDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InspectorsNames"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InspectorsNames"].Header).Caption = (GlobalVariables.IsArabic ? "أسماء الفاحصين" : "Inspectors Names");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InspectorsNames"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TreatmentDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TreatmentDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المعالجة" : "Treatment Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TreatmentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Treatment"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Treatment"].Header).Caption = (GlobalVariables.IsArabic ? "المعالجة" : "Treatment");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Treatment"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChemicalActiveIngredient"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChemicalActiveIngredient"].Header).Caption = (GlobalVariables.IsArabic ? "المادة الفعالة" : "Chemical Active Ingredients");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChemicalActiveIngredient"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Concentration"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Concentration"].Header).Caption = (GlobalVariables.IsArabic ? "التركيز" : "Concentration");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Concentration"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DurationAndTemperature"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DurationAndTemperature"].Header).Caption = (GlobalVariables.IsArabic ? "درجات الحرارة" : "Duration And Temperature");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DurationAndTemperature"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalInformation"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalInformation"].Header).Caption = (GlobalVariables.IsArabic ? "معلومات اضافية" : "Additional Information");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalInformation"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalDeclaration"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalDeclaration"].Header).Caption = (GlobalVariables.IsArabic ? "إقرارات اضافية" : "Additional Declaration");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalDeclaration"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
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
