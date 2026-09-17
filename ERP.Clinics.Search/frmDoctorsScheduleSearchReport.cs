using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.Search;

public class frmDoctorsScheduleSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmDoctorsScheduleSearchReport()
	{
		InitializeComponent();
	}

	public frmDoctorsScheduleSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorScheduleCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorScheduleCode"].Header).Caption = (GlobalVariables.IsArabic ? " كود الجدول " : "Schedule Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorScheduleCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الطبيب" : "Doctor Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.38) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WeeksCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WeeksCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الأسابيع" : "Weeks Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WeeksCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
