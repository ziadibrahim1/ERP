using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.Search;

public class frmClinicsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmClinicsSearchReport()
	{
		InitializeComponent();
	}

	public frmClinicsSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود " : "Doctor Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClinicName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.48) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel1"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel1"].Header).Caption = (GlobalVariables.IsArabic ? "التليفون1" : "Tel1");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel1"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel2"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel2"].Header).Caption = (GlobalVariables.IsArabic ? "التليفون1" : "Tel2");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel2"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
