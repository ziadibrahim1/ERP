using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.EInvoices;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.MasterData;

public class frmUnits : frmDetails
{
	private DataTable dtEINVUnits;

	private DataTable dtUnitsTypes = new DataTable();

	private ValueList vlEINVUnits = new ValueList();

	private bool UseElectronicInvoice = false;

	private IContainer components = null;

	public frmUnits()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم نوع الوحدة" : "Unit Type Name");
	}

	public override void PrepareData()
	{
		UseElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		dtUnitsTypes = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtUnitsTypes, "UnitTypeID", "UnitTypeName");
		if (UseElectronicInvoice)
		{
			dtEINVUnits = BusinessLayer.EInvoices.Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			vlEINVUnits.ValueListItems.Clear();
			for (int i = 0; i < dtEINVUnits.Rows.Count; i++)
			{
				vlEINVUnits.ValueListItems.Add((object)dtEINVUnits.Rows[i]["EINVUnitID"].ToString(), dtEINVUnits.Rows[i]["EINVUnitName"].ToString());
			}
		}
		dtDetails = BusinessLayer.StockControl.Units.SelectByUnitTypeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الوحدة بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitNameAr"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.25)) : ((int)((double)((Control)(object)ULGData).Width * 0.3))) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الوحدة بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitNameEn"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.25)) : ((int)((double)((Control)(object)ULGData).Width * 0.3)));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RatioToDefault"].Header).Caption = (GlobalVariables.IsArabic ? "النسبة الى المرجع" : "Ratio To Default");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RatioToDefault"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RatioToDefault"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.15)) : ((int)((double)((Control)(object)ULGData).Width * 0.2)));
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RatioToDefault"].Format = "###,##.00000000";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDefault"].Header).Caption = (GlobalVariables.IsArabic ? "المرجع" : "Default");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDefault"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDefault"].Width = (UseElectronicInvoice ? ((int)((double)((Control)(object)ULGData).Width * 0.15)) : ((int)((double)((Control)(object)ULGData).Width * 0.2)));
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDefault"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVUnitID"].Header).Caption = (GlobalVariables.IsArabic ? "كود الفاتورة الالكترونية" : "E-Invoice Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVUnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVUnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVUnitID"].ValueList = (IValueList)(object)vlEINVUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVUnitID"].Hidden = !UseElectronicInvoice;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails = BusinessLayer.StockControl.Units.SelectByUnitTypeID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override bool ValidateData()
	{
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitNameAr"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال إسم الوحدة بالعربية", "Please Insert Unit Arabic Name");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["UnitNameAr"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["RatioToDefault"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["RatioToDefault"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال النسبة الى المرجع", "Please Insert Ratio To Default");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["RatioToDefault"]).Selected = true;
				return false;
			}
			if (UseElectronicInvoice && ((UltraGridBase)ULGData).Rows[i].Cells["EINVUnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال كود الوحدة", "Please Insert The Unit Code");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["EINVUnitID"]).Selected = true;
				return false;
			}
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsDefault"].Value.ToString()))
			{
				num++;
			}
		}
		if (num > 1 || num == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مرجع واحد لهذا النوع من الوحدات", "Please Select One Defalut to Unit Type");
			return false;
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
				((UltraGridBase)ULGData).Rows[i].Cells["UnitTypeID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("SC_Units", "UnitTypeID", ((TextEditorControlBase)cboHeader).Value.ToString(), "UnitID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				BusinessLayer.StockControl.Units.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RatioToDefault")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "RatioToDefault" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsDefault") && ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString() != "-1")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
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
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(59, 18);
		((System.Windows.Forms.Control)(object)base.lblHeader).Text = "Currency";
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmUnits";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
