using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CnsProjects.Search;

public class frmWorkersSearch : frmSearch
{
	private IContainer components = null;

	public frmWorkersSearch()
	{
		InitializeComponent();
	}

	public frmWorkersSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkerCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkerCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkerCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkerName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم  العامل" : "Worker Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.7);
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
