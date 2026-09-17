using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Search;

public class frmCustomsBrokersSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmCustomsBrokersSearchReport()
	{
		InitializeComponent();
	}

	public frmCustomsBrokersSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerCode"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerNameAr"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالعربيه" : "Arabic Customs Broker Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerNameEn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم بالانجليزيه" : "English Customs Broker Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomsBrokerNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
