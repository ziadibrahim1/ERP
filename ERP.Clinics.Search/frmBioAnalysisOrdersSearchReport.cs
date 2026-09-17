using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.Search;

public class frmBioAnalysisOrdersSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmBioAnalysisOrdersSearchReport()
	{
		InitializeComponent();
	}

	public frmBioAnalysisOrdersSearchReport(DataTable dt, string idColumnName, int ApprovedValue, int DeletedValue)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
		Approved = ApprovedValue;
		Deleted = DeletedValue;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BioAnalysisOrderDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientName"].Header).Caption = (GlobalVariables.IsArabic ? "المريض" : "Patient");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PatientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDoctor"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDoctor"].Header).Caption = (GlobalVariables.IsArabic ? "الطبيب المعالج" : "From Doctor");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDoctor"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsConfirmed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsConfirmed"].Header).Caption = (GlobalVariables.IsArabic ? "مؤكد" : "Confirmed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsConfirmed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SampleLabName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SampleLabName"].Header).Caption = (GlobalVariables.IsArabic ? "معمل العينات" : "Sample Lab");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SampleLabName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Header).Caption = (GlobalVariables.IsArabic ? "مستلم" : "IsDeliverd");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDeliveryDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDeliveryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التسليم المتوقع" : "Expected Deliverd Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedDeliveryDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Net Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "معتمد" : "Approved");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
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
