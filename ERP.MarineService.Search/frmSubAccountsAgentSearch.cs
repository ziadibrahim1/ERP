using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

public class frmSubAccountsAgentSearch : frmSearch
{
	private IContainer components = null;

	public frmSubAccountsAgentSearch()
	{
		InitializeComponent();
	}

	public frmSubAccountsAgentSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AgentNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AgentNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الوكيل " : "Agent No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AgentNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AgentName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AgentName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الوكيل" : "Agent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AgentName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Header).Caption = (GlobalVariables.IsArabic ? "المحمول" : "Mobile");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Header).Caption = (GlobalVariables.IsArabic ? "التليفون" : "Tel");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EMail"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EMail"].Header).Caption = (GlobalVariables.IsArabic ? "البريد الإلكتروني" : "Email");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EMail"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
