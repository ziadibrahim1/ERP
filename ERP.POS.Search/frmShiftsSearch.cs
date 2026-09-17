using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Search;

public class frmShiftsSearch : frmSearch
{
	private IContainer components = null;

	public frmShiftsSearch()
	{
		InitializeComponent();
	}

	public frmShiftsSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الوردية بالعربى" : "Shift Arabic Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الوردية بالانجليزية" : "Shift English Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartTime"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartTime"].Header).Caption = (GlobalVariables.IsArabic ? "بداية الوردية" : "Start Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndTime"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndTime"].Header).Caption = (GlobalVariables.IsArabic ? "انتهاء الوردية" : "End Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
