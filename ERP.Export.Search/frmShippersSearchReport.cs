using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Export.Search;

public class frmShippersSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmShippersSearchReport()
	{
		InitializeComponent();
	}

	public frmShippersSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الشاحن" : "Shipper Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الشاحن بالعربية" : "Shipper Name Ar.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الشاحن بالانجليزية" : "Shipper Name En.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShipperNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.28);
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
