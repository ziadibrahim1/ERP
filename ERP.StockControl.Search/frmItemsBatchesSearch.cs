using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Search;

public class frmItemsBatchesSearch : frmSearch
{
	private IContainer components = null;

	public frmItemsBatchesSearch()
	{
		InitializeComponent();
	}

	public frmItemsBatchesSearch(DataTable dt, string idColumnName, int BatchID)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الانتاج" : "Batch Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidityDays"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidityDays"].Header).Caption = (GlobalVariables.IsArabic ? "مدة الصلاحية" : "Validity Days");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidityDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchEndDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchEndDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الانتهاء" : "Batch End Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchEndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Expired"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Expired"].Header).Caption = (GlobalVariables.IsArabic ? "منتهى الصلاحية" : "Expired");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Expired"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
