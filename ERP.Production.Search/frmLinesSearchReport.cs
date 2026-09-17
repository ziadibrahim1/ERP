using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Search;

public class frmLinesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmLinesSearchReport()
	{
		InitializeComponent();
	}

	public frmLinesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الخط" : "Line Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.68) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الخط" : "Line Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
