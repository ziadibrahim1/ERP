using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.Search;

public class frmPaymentMethodsSearch : frmSearch
{
	private IContainer components = null;

	public frmPaymentMethodsSearch()
	{
		InitializeComponent();
	}

	public frmPaymentMethodsSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodName"].Header).Caption = (GlobalVariables.IsArabic ? "طريقة الدفع" : "Payment Method Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentTime"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentTime"].Header).Caption = (GlobalVariables.IsArabic ? "مدة الدفع" : "Payment Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentsCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الدفعات" : "Installments Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
