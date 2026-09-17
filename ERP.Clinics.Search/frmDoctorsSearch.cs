using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.Search;

public class frmDoctorsSearch : frmSearch
{
	private IContainer components = null;

	public frmDoctorsSearch()
	{
		InitializeComponent();
	}

	public frmDoctorsSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود " : "Doctor Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DoctorName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SpecializationName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SpecializationName"].Header).Caption = (GlobalVariables.IsArabic ? "التخصص" : "Specialization");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SpecializationName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Header).Caption = (GlobalVariables.IsArabic ? "الموبايل" : "Mobile");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Header).Caption = (GlobalVariables.IsArabic ? "االتليفون" : "Tel");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Header).Caption = (GlobalVariables.IsArabic ? "مفعل" : "Is Active");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
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
