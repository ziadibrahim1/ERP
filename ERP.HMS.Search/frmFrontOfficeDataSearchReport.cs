using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HMS.Search;

public class frmFrontOfficeDataSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmFrontOfficeDataSearchReport()
	{
		InitializeComponent();
	}

	public frmFrontOfficeDataSearchReport(DataTable dt, string idColumnName, int DeletedValue)
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrontOfficeDataNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrontOfficeDataNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن " : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrontOfficeDataNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاذن" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGData).Width * 0.48);
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
