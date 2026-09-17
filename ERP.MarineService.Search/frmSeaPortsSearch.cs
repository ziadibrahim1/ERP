using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

public class frmSeaPortsSearch : frmSearch
{
	private IContainer components = null;

	public frmSeaPortsSearch()
	{
		InitializeComponent();
	}

	public frmSeaPortsSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الميناء" : "SeaPort Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الميناء بالعربية" : "SeaPort Name Ar.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الميناء بالانجليزية" : "SeaPort Name En.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
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
