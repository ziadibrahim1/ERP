using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Constructions.Search;

public class frmContractsReport : frmSearchReport
{
	private IContainer components = null;

	public frmContractsReport()
	{
		InitializeComponent();
	}

	public frmContractsReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التعاقد" : "Contract Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التعاقد" : "Contract Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingUnitCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingUnitCode"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingUnitCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingName"].Header).Caption = (GlobalVariables.IsArabic ? "المبنى" : "Building");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuildingName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر البيع" : "Sales Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectName"].Header).Caption = (GlobalVariables.IsArabic ? "المشروع" : "Project");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCanceled"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCanceled"].Header).Caption = (GlobalVariables.IsArabic ? "ملغاة" : "Canceled");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCanceled"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanceledDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanceledDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الالغاء" : "Canceled Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanceledDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
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
