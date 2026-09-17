using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.Search;

public class frmCostCentersSearch : frmSearch
{
	private IContainer components = null;

	public frmCostCentersSearch()
	{
		InitializeComponent();
	}

	public frmCostCentersSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNumber"].Header).Caption = (GlobalVariables.IsArabic ? "كود " : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المجموعة" : "Group No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المجموعة" : "Group Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالانجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
