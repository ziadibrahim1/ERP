using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Export.Search;

public class frmEXPOperationsDeclarationsContainersSearch : frmSearch
{
	private IContainer components = null;

	public frmEXPOperationsDeclarationsContainersSearch()
	{
		InitializeComponent();
	}

	public frmEXPOperationsDeclarationsContainersSearch(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDeclarationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم امر الشحن" : "Declaration No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDeclarationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوية" : "Container No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
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
