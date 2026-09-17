using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmCities : frmDetails
{
	private DataTable dtCountries = new DataTable();

	private IContainer components = null;

	public frmCities()
	{
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الدولة" : "City Name");
	}

	public override void PrepareData()
	{
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtCountries, "CountryID", "CountryName");
		dtDetails = Cities.SelectByCountryID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود المحافظة" : "City Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "إسم المحافظة بالعربية" : "City Arabic Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "إسم المحافظة بالإنجليزية" : "City English Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails = Cities.SelectByCountryID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["CityNameAr"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال إسم المحافظة بالعربية", "Please Insert City Arabic Name");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["CityNameAr"]).Selected = true;
				return false;
			}
		}
		return true;
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["CountryID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["CityID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("G_Cities", "CountryID", ((TextEditorControlBase)cboHeader).Value.ToString(), "CityID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				Cities.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DeliveryFees")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)base.ULGData).TabIndex = 2;
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.Name = "Tahoma";
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(454, 69);
		((System.Windows.Forms.Control)(object)base.cboHeader).TabIndex = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.Name = "Tahoma";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(346, 72);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 18);
		((System.Windows.Forms.Control)(object)base.lblHeader).Text = "Country";
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmCities";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
