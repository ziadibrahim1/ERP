using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Export.Search;

public class frmExpTransportationsOrdersSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmExpTransportationsOrdersSearchReport()
	{
		InitializeComponent();
	}

	public frmExpTransportationsOrdersSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? " التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransporterName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransporterName"].Header).Caption = (GlobalVariables.IsArabic ? "شركة النقل" : "Transporter");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransporterName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.17);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingPlace"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingPlace"].Header).Caption = (GlobalVariables.IsArabic ? "مكان التحميل" : "LoadingPlace");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingPlace"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerPlace"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerPlace"].Header).Caption = (GlobalVariables.IsArabic ? "مكان الحاوية" : "Container Place");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerPlace"].Width = (int)((double)((Control)(object)ULGData).Width * 0.18);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Finished"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Finished"].Header).Caption = (GlobalVariables.IsArabic ? "منتهي" : "Finished");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Finished"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
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
