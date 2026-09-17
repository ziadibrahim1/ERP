using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.Search;

public class frmAccountsSearch : frmSearch
{
	private IContainer components = null;

	public frmAccountsSearch()
	{
		InitializeComponent();
	}

	public frmAccountsSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحساب" : "No");
		if (GlobalVariables.SeeingInvisibleAcounts)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVisible"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVisible"].Header).Caption = (GlobalVariables.IsArabic ? "ظاهر" : "Visible");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVisible"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVisible"].Hidden = true;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم الحساب بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المجموعة" : "Group No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المجموعة" : "Group Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ParentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الحساب بالانجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
