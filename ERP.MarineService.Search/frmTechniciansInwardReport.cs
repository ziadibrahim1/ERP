using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

public class frmTechniciansInwardReport : frmSearchReport
{
	private IContainer components = null;

	public frmTechniciansInwardReport()
	{
		InitializeComponent();
	}

	public frmTechniciansInwardReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الفني" : "Technician Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الهوية" : "ID Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
