using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Search;

public class frmCommentTypesSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmCommentTypesSearchReport()
	{
		InitializeComponent();
	}

	public frmCommentTypesSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربيه" : "Arabic Comment Type Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالانجليزيه" : "English Commen tType Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommentTypeNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
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
