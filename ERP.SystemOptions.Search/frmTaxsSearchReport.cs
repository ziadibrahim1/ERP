using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.Search;

public class frmTaxsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmTaxsSearchReport()
	{
		InitializeComponent();
	}

	public frmTaxsSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.38) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالانجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxPercent"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxPercent"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الضريبة" : "Tax Percent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxPercent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
