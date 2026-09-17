using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Export.Search;

public class frmTransportationsPlacesSearch : frmSearch
{
	private IContainer components = null;

	public frmTransportationsPlacesSearch()
	{
		InitializeComponent();
	}

	public frmTransportationsPlacesSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationPlaceNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationPlaceNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المكان بالعربية" : "Place Name Ar.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationPlaceNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationPlaceNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationPlaceNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المكان بالانجليزية" : "Place Name En.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationPlaceNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
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
