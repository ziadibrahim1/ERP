using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.Search;

public class frmSafesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmSafesSearchReport()
	{
		InitializeComponent();
	}

	public frmSafesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود الخزينة" : "Safe Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.48) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الخزينة" : "Safe");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
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
