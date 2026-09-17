using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.MarineService.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Approved;

public class frmClosingOperations : frmPosted
{
	private IContainer components = null;

	public frmClosingOperations()
	{
		InitializeComponent();
		NoCol = "OperationNo";
	}

	public override void FillGrid()
	{
		dtsource = Operations.SelectByClosed(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الوصول" : "Arrival Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntryDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Voyage No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoyageNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Header).Caption = (GlobalVariables.IsArabic ? "الباخرة" : "Vessel Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VesselName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Header).Caption = (GlobalVariables.IsArabic ? "المالك" : "Owner Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OwnerName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Header).Caption = (GlobalVariables.IsArabic ? "المستأجر" : "Charter Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CharterName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "ميناء الوصول" : "Entry Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EntrySeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Departure Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "من ميناء" : "Last Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Header).Caption = (GlobalVariables.IsArabic ? "الى ميناء" : "Next Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToSeaPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DateToLeave"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DateToLeave"].Header).Caption = (GlobalVariables.IsArabic ? "21 يوم" : "21 Days");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DateToLeave"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلقة" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Closed" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "DepartureDate")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Closed"].Value.Equals(true) && ((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال تاريخ الخروج" : "Please insert departure date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"];
				return false;
			}
		}
		return true;
	}

	public override void SaveData()
	{
		if (!ValidateData())
		{
			return;
		}
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Closed"].Value.Equals(true))
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value.ToString() + ",";
			}
		}
		if (int.Parse(Operations.CheckForUnFinishedTasks(text)) == 0)
		{
			if (text != ",")
			{
				Operations.SetClosed("1", text);
				FillGrid();
			}
			else
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد عمليات  لإغلاقها " : "There are No Operations to be Closed");
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "توجد بعض المهام غير مكتملة يجب انهاؤها قبل اغلاق العملية " : "There Are UnFinished Tasks Should Be Finished Before Closing Operations");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmOperations frmOperations2 = new frmOperations(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OperationID"].Value.ToString()));
			frmOperations2.Size = new Size(base.Width, base.Height);
			frmOperations2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmOperations2.lblTitle).Text = (GlobalVariables.IsArabic ? "العمليات" : "Operations");
			frmOperations2.ShowDialog();
		}
	}

	public override void Search()
	{
		DataTable dataTable = SearchFunctions.OperationsSearchReport(IsFromServer: false);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (dataTable.Rows[i]["OperationID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["OperationID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[j].Cells["Closed"].Value = true;
				}
			}
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Approved.frmClosingOperations));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
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
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(this, "$this");
		base.Name = "frmApprovingOperations";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		base.ResumeLayout(false);
	}
}
