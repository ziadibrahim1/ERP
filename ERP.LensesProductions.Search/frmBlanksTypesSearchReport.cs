using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.LensesProductions.Search;

public class frmBlanksTypesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmBlanksTypesSearchReport()
	{
		InitializeComponent();
	}

	public frmBlanksTypesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNo"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? " الاسم بالانجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " الاسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceForAllBranch"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceForAllBranch"].Header).Caption = (GlobalVariables.IsArabic ? "السعر موحد لكل الفروع" : "Price For All Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PriceForAllBranch"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Header).Caption = (GlobalVariables.IsArabic ? "يمكن تعديل سعر البيع" : "Can Modify Sales Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "حساب المبيعات" : "Sales Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
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
