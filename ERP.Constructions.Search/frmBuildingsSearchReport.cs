using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Constructions.Search;

public class frmBuildingsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmBuildingsSearchReport()
	{
		InitializeComponent();
	}

	public frmBuildingsSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingCode"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["buildingName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["buildingName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectName"].Header).Caption = (GlobalVariables.IsArabic ? "المشروع" : "Project");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].Header).Caption = (GlobalVariables.IsArabic ? "المساحة" : "Size");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Size"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SamplesCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SamplesCount"].Header).Caption = (GlobalVariables.IsArabic ? "النماذج" : "Samples");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SamplesCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FloorCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FloorCount"].Header).Caption = (GlobalVariables.IsArabic ? "الادوار" : "Floors");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FloorCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParkingCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParkingCount"].Header).Caption = (GlobalVariables.IsArabic ? "جراج" : "Parking");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParkingCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoresCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoresCount"].Header).Caption = (GlobalVariables.IsArabic ? "محلات" : "Stores");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoresCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BalancesCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BalancesCount"].Header).Caption = (GlobalVariables.IsArabic ? "ميزانين" : "Balances");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BalancesCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
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
