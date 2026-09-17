using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Photos.Search;

public class frmStagesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmStagesSearchReport()
	{
		InitializeComponent();
	}

	public frmStagesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageName"].Header).Caption = (GlobalVariables.IsArabic ? " الاسم " : " Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StageName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FolderPath"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FolderPath"].Header).Caption = (GlobalVariables.IsArabic ? "مسار الملف الداخلى" : "Indoor Folder Path");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FolderPath"].Width = (int)((double)((Control)(object)ULGData).Width * 0.35);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OutdoorFolderPath"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OutdoorFolderPath"].Header).Caption = (GlobalVariables.IsArabic ? "مسار الملف الخارجى" : "Outdoor Folder Path");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OutdoorFolderPath"].Width = (int)((double)((Control)(object)ULGData).Width * 0.35);
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
