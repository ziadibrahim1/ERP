using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Payroll.Search;

public class frmSocialIncreaseSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmSocialIncreaseSearchReport()
	{
		InitializeComponent();
	}

	public frmSocialIncreaseSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseStartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseStartDate"].Header).Caption = (GlobalVariables.IsArabic ? " التاريخ " : " Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseStartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreasePercentage"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreasePercentage"].Header).Caption = (GlobalVariables.IsArabic ? "النسبة" : "Percentage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreasePercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseMinValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseMinValue"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الادنى" : "Min Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseMinValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseMaxValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseMaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الاقصى" : "Max Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SocialIncreaseMaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.28);
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
