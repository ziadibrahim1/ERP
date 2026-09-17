using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CRM.Search;

public class frmCustomersFollowsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmCustomersFollowsSearchReport()
	{
		InitializeComponent();
	}

	public frmCustomersFollowsSearchReport(DataTable dt, string idColumnName, int DeletedValue)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
		Deleted = DeletedValue;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Customer");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepName"].Header).Caption = (GlobalVariables.IsArabic ? "الخطوة" : "Step");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentStepEmployeeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentStepEmployeeName"].Header).Caption = (GlobalVariables.IsArabic ? "المسئول" : "Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentStepEmployeeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comment"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comment"].Header).Caption = (GlobalVariables.IsArabic ? "التعليق" : "Comment");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comment"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخطوة القادمة" : "Next Step Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepEmployeeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepEmployeeName"].Header).Caption = (GlobalVariables.IsArabic ? "مسئول الخطوة القادمة" : "Next Step Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepEmployeeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
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
