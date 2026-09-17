using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

public class frmExportTypesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmExportTypesSearchReport()
	{
		InitializeComponent();
	}

	public frmExportTypesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربية" : "Arabic Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالانجليزيه" : "English Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportTypeNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
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
