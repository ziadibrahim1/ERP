using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Search;

public class frmOperationsAlertSearchReport : frmSearchReport
{
	private DataRow drSettings;

	private IContainer components = null;

	public frmOperationsAlertSearchReport()
	{
		InitializeComponent();
	}

	public frmOperationsAlertSearchReport(DataTable dt, string idColumnName)
		: this()
	{
		dtSource = dt;
		IDColumn = idColumnName;
		drSettings = Settings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false).Rows[0];
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "Op. No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Header).Caption = (GlobalVariables.IsArabic ? " تاريخ العملية" : "Operation Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CaptainName"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CaptainName"].Header).Caption = (GlobalVariables.IsArabic ? "القبطان" : "Captain");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CaptainName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.8);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلقة" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الوصول" : "Arrival Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualDepartureDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualDepartureDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المغادرة الفعلي" : "Actual Departure Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualDepartureDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Header).Caption = (GlobalVariables.IsArabic ? "المالك" : "Owner Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Header).Caption = (GlobalVariables.IsArabic ? "المستأجر" : "Charter Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء الوصول" : "Entry Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "من ميناء" : "Last Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "الى ميناء" : "Next Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DaysLeft"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DaysLeft"].Header).Caption = (GlobalVariables.IsArabic ? "21 يوم" : "21 Days");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DaysLeft"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		if (int.Parse(e.Row.Cells["DaysLeft"].Value.ToString()) <= int.Parse(drSettings["VesselStayAlertBeforeDays"].ToString()))
		{
			((UltraControlBase)ULGData).UseAppStyling = false;
			((AppearanceBase)e.Row.Appearance).BackColor = Color.LightBlue;
		}
	}

	private void ULGData_InitializeLayout(object sender, InitializeLayoutEventArgs e)
	{
		e.Layout.Bands[0].Override.RowAppearance.ResetAlphaLevel();
		e.Layout.Bands[0].Override.RowAppearance.ResetBackColorAlpha();
		e.Layout.Bands[0].Override.RowAppearance.Reset();
		e.Layout.Bands[0].Override.RowAppearance.ThemedElementAlpha = (Alpha)2;
		e.Layout.Bands[0].Override.RowAppearance.ResetBackColor2();
		e.Layout.Bands[0].Override.RowAppearance.ResetBackColor();
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Expected O, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		Appearance val = new Appearance();
		UltraGridBand val2 = new UltraGridBand("", -1);
		UltraGridColumn val3 = new UltraGridColumn("Choose");
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.cboSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkSearchMultiFilter).BeginInit();
		((System.Windows.Forms.Control)(object)base.pnlControls).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.dtSource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtResult).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)base.ULGData).DisplayLayout.Appearance = (AppearanceBase)(object)val;
		val3.AllowRowFiltering = (DefaultableBoolean)2;
		val3.DefaultCellValue = "False";
		((HeaderBase)val3.Header).Caption = "";
		val3.Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		val3.Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((HeaderBase)val3.Header).VisiblePosition = 0;
		val3.Width = 17;
		val2.Columns.AddRange(new object[1] { val3 });
		((UltraGridBase)base.ULGData).DisplayLayout.BandsSerializer.Add((object)val2);
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((AppearanceBase)val4).TextHAlignAsString = "Center";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).FontData.BoldAsString = "True";
		((AppearanceBase)val5).TextHAlignAsString = "Center";
		((AppearanceBase)val5).TextVAlignAsString = "Middle";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		base.ULGData.InitializeLayout += new InitializeLayoutEventHandler(ULGData_InitializeLayout);
		base.ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
		base.ClientSize = new System.Drawing.Size(800, 600);
		base.Name = "frmOperationsAlertSearchReport";
		((System.ComponentModel.ISupportInitialize)base.cboSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkSearchMultiFilter).EndInit();
		((System.Windows.Forms.Control)(object)base.pnlControls).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.dtSource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtResult).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
