using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Search;

public class frmTextMessageSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmTextMessageSearchReport()
	{
		InitializeComponent();
	}

	public frmTextMessageSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم " : "TextMessage No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ " : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TextMessageDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Message"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Message"].Header).Caption = (GlobalVariables.IsArabic ? "الرسالة" : "Message");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Message"].Width = (int)((double)((Control)(object)ULGData).Width * 0.55);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
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
