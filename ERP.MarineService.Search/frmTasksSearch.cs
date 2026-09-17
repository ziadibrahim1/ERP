using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

public class frmTasksSearch : frmSearch
{
	private IContainer components = null;

	public frmTasksSearch()
	{
		InitializeComponent();
	}

	public frmTasksSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskNameEn"].Header).Caption = (GlobalVariables.IsArabic ? " الاسم بالانجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " الاسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskTypeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع المهمة" : "Task Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaskTypeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
