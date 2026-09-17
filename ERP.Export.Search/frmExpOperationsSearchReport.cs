using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Export.Search;

public class frmExpOperationsSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmExpOperationsSearchReport()
	{
		InitializeComponent();
	}

	public frmExpOperationsSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Header).Caption = (GlobalVariables.IsArabic ? " التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ConsigneeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ConsigneeName"].Header).Caption = (GlobalVariables.IsArabic ? "المستلم" : "Consigne");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ConsigneeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NotifyName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NotifyName"].Header).Caption = (GlobalVariables.IsArabic ? "Notify" : "Notify");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NotifyName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuyerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuyerName"].Header).Caption = (GlobalVariables.IsArabic ? "المشتري" : "Buyer");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BuyerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء التحميل" : "Loading Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DischargeSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DischargeSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء التفريغ" : "Discharge Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DischargeSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
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
