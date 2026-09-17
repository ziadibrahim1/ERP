using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Search;

public class frmKitchenScreensSearchReport : frmSearchReport
{
	private IContainer components = null;

	public frmKitchenScreensSearchReport()
	{
		InitializeComponent();
	}

	public frmKitchenScreensSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["KitchenScreenNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["KitchenScreenNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["KitchenScreenNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["KitchenScreenName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["KitchenScreenName"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["KitchenScreenName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPackingScreen"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPackingScreen"].Header).Caption = (GlobalVariables.IsArabic ? "شاشة تعبئة" : "PackingScreen");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsPackingScreen"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDineIn"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDineIn"].Header).Caption = (GlobalVariables.IsArabic ? "Dine In" : "Dine In");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDineIn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivery"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivery"].Header).Caption = (GlobalVariables.IsArabic ? "توصيل للمنازل " : "Delivery");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivery"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsTakeAway"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsTakeAway"].Header).Caption = (GlobalVariables.IsArabic ? " تيك أواي" : "TakeAway");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsTakeAway"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSpecialOrder"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSpecialOrder"].Header).Caption = (GlobalVariables.IsArabic ? "طلبات خاصة" : "Special Order");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSpecialOrder"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
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
