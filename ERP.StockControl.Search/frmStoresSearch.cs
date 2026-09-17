using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Search;

public class frmStoresSearch : frmSearch
{
	private IContainer components = null;

	public frmStoresSearch()
	{
		InitializeComponent();
	}

	public frmStoresSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود المخزن" : "Store Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.28) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المخزن" : "Store Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Locked"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Locked"].Header).Caption = (GlobalVariables.IsArabic ? "مغلق" : "Locked");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Locked"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
