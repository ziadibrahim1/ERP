using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.Search;

public class frmBanksSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmBanksSearchReport()
	{
		InitializeComponent();
	}

	public frmBanksSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود البنك" : "Bank Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.28) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البنك" : "Bank");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود العملة" : "Currency Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
