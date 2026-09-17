using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.Search;

public class frmEmployeesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmEmployeesSearchReport()
	{
		InitializeComponent();
	}

	public frmEmployeesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PersonalIDNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PersonalIDNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم القومى" : "Personal ID No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PersonalIDNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الموظف" : "Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GenderName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GenderName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنس" : "Gender");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GenderName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityName"].Header).Caption = (GlobalVariables.IsArabic ? "المدينة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MilitaryServiceName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MilitaryServiceName"].Header).Caption = (GlobalVariables.IsArabic ? "التجنيد" : "Military Service");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MilitaryServiceName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialStatusName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialStatusName"].Header).Caption = (GlobalVariables.IsArabic ? "الحالة الاجتماعية" : "Social Status");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialStatusName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SectionName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SectionName"].Header).Caption = (GlobalVariables.IsArabic ? "القسم" : "Section");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SectionName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StateName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StateName"].Header).Caption = (GlobalVariables.IsArabic ? "الحالة" : "State");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StateName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureName"].Header).Caption = (GlobalVariables.IsArabic ? "الهيكل الادارى" : "Administrative Structure");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdministrativeStructureName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JobName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JobName"].Header).Caption = (GlobalVariables.IsArabic ? "الوظيفة" : "Job");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JobName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DirectManager"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DirectManager"].Header).Caption = (GlobalVariables.IsArabic ? "المدير المباشر" : "Direct Manager");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DirectManager"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
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
