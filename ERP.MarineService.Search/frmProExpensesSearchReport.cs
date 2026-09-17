using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

public class frmProExpensesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmProExpensesSearchReport()
	{
		InitializeComponent();
	}

	public frmProExpensesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم العملية" : "Op. No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المصروف" : "Expense No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Header).Caption = (GlobalVariables.IsArabic ? "الشركة" : "Company");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JVNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JVNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم القيد" : "JV No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JVNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceName"].Header).Caption = (GlobalVariables.IsArabic ? "الخدمة" : "Service");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ServiceName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccount"].Header).Caption = (GlobalVariables.IsArabic ? "الحساب التحليلى" : "SubAccount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تمت" : "Completed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
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
