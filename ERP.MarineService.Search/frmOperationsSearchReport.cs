using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلقة" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الوصول" : "Arrival Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualDepartureDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualDepartureDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المغادرة الفعلي" : "Actual Departure Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualDepartureDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Header).Caption = (GlobalVariables.IsArabic ? "المالك" : "Owner Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Header).Caption = (GlobalVariables.IsArabic ? "المستأجر" : "Charter Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء الوصول" : "Entry Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "من ميناء" : "Last Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "الى ميناء" : "Next Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? " ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DaysLeft"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DaysLeft"].Header).Caption = (GlobalVariables.IsArabic ? "21 يوم" : "21 Days");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DaysLeft"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
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
