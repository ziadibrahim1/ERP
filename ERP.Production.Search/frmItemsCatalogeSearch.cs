using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Search;

public class frmItemsCatalogeSearch : frmSearch
{
	private IContainer components = null;

	public frmItemsCatalogeSearch()
	{
		InitializeComponent();
	}

	public frmItemsCatalogeSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم " : "NO");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Header).Caption = (GlobalVariables.IsArabic ? "نشط" : "IsActive");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsActive"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
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
