using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

public class frmDurationAndTemperaturesSearch : frmSearch
{
	private IContainer components = null;

	public frmDurationAndTemperaturesSearch()
	{
		InitializeComponent();
	}

	public frmDurationAndTemperaturesSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DurationAndTemperatureCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DurationAndTemperatureCode"].Header).Caption = (GlobalVariables.IsArabic ? "مدة التعرض ودرجة الحرارة" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DurationAndTemperatureCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGData).Width * 0.6);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
