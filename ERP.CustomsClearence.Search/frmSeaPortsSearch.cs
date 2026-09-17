using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortID"].Header).Caption = (GlobalVariables.IsArabic ? "كود الميناء" : "SeaPort Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الميناء" : "SeaPort Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SeaPortName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceTypeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
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
