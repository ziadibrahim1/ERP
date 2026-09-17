using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.LensesProductions;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.LensesProductions.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.LensesProductions.Approved;

public class frmFactorySent : frmPosted
{
	private IContainer components = null;

	public frmFactorySent()
	{
		InitializeComponent();
		fromServer = true;
		NoCol = "BlankInvoiceNo";
	}

	public override void FillGrid()
	{
		DisplayDataDate = GlobalFunctions.GetServerDateTimeNow(IsFromServer: true);
		dtsource = BlanksInvoices.SelectByFactorySent("-1", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FactorySentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FactorySentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Header).Caption = (GlobalVariables.IsArabic ? "الخط" : "Line");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FactorySentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الإنتهاء" : "Finished Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFactorySent"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFactorySent"].Header).Caption = (GlobalVariables.IsArabic ? "ارسال" : "Send");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFactorySent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFactorySent"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFactorySent"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFactorySent"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(1, "Print");
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة الباركود" : "Print BarCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة الباركود" : "Print BarCode");
		}
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "IsFactorySent")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		if (!ValidateData())
		{
			return;
		}
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFactorySent"].Value.ToString()))
			{
				text = text + " Update Lns_BlanksInvoices Set IsFactorySent = 1 , FactorySentDate = '" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FactorySentDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate) + "' Where IsFinished = 1 And BlankInvoiceID  =" + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + "; exec SP_Trans_Log " + GlobalVariables.UserID + " ,'Lns_BlanksInvoices' ," + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + " ,'U';";
			}
		}
		if (text != "")
		{
			Main.SyncExecuteNonQuery(text);
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد فواتير تصنيع لارسالها" : "There are No Blank Invoices To Be Sent");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow == null || ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index != 0 || ULGData.ActiveCell == null)
		{
			return;
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Open")
		{
			frmBlanksInvoices frmBlanksInvoices2 = new frmBlanksInvoices(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BlankInvoiceID"].Value.ToString()), _fromApprovalForm: true);
			frmBlanksInvoices2.Size = new Size(base.Width, base.Height);
			frmBlanksInvoices2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmBlanksInvoices2.lblTitle).Text = (GlobalVariables.IsArabic ? "فاتورة التصنيعات" : "Blanks Invoices");
			frmBlanksInvoices2.ShowDialog();
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Print")
		{
			ReportDocument reportDocument = new ReportDocument();
			reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_BlanksInvoices_BarCode.rpt" : "Rep_Lns_BlanksInvoices_BarCode.rpt"));
			GlobalFunctions.ConfigureReport(reportDocument);
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			reportDocument.SetParameterValue("@BlankInvoiceID", ULGData.ActiveCell.Row.Cells["BlankInvoiceID"].Value.ToString());
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
			reportDocument.Dispose();
			GC.Collect();
		}
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFactorySent"].Value.ToString()) && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "Lns_BlanksInvoices", IsFromServer: true))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
				return false;
			}
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFactorySent"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["FactorySentDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" لابد من إختيار تاريخ الارسال ", "Please Select Sent Date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FactorySentDate"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFactorySent"].Value.ToString()) && Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["FactorySentDate"].Value) < Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الارسال قبل تاريخ الانتهاء", "The Sending Date is Before The Finished Date. ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FactorySentDate"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		return true;
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.LensesProductions.Approved.frmFactorySent));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmFactorySent";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		base.ResumeLayout(false);
	}
}
