using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

public class frmExportersSearch : frmSearch
{
	private IContainer components = null;

	public frmExportersSearch()
	{
		InitializeComponent();
	}

	public frmExportersSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExporterCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExporterCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المصدر " : "Exporter No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExporterCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExporterName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExporterName"].Header).Caption = (GlobalVariables.IsArabic ? "أسم المصدر" : "Exporter Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExporterName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Header).Caption = (GlobalVariables.IsArabic ? "أسم المجموعة" : "Group Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المجموعة" : "Group No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportNo"].Header).Caption = (GlobalVariables.IsArabic ? "سجل المصدرين" : "Export Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Text = "frmExportersSearch";
	}
}
