using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.Search;

public class frmBankOutRequestSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmBankOutRequestSearchReport()
	{
		InitializeComponent();
	}

	public frmBankOutRequestSearchReport(DataTable dt, string idColumnName, int ApprovedValue, int DeletedValue)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
		Approved = ApprovedValue;
		Deleted = DeletedValue;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن " : "Bank Out Request No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankOutRequestDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم البنك" : "Bank");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BankName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الشيك" : "Check Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود العملة" : "Currency Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "من السيد" : "Charged Person");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.125);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		if (Approved == -1)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "معتمد" : "Approved");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
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
