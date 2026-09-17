using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.FixedAssets;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.FixedAssets.Transactions;

public class frmAssetsLocationsChange : frmDetails
{
	private DataTable dtAssets;

	private DataTable dtAssetsLocations;

	private DataTable dtAssetData;

	private ValueList vlAssetsLocations = new ValueList();

	private IContainer components = null;

	public frmAssetsLocationsChange()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الأصل" : "Asset");
	}

	public override void PrepareData()
	{
		dtAssets = Assets.FillCombo(GlobalVariables.AssetSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtAssets, "SubAccountID", "SubAccountName");
		dtAssetsLocations = AssetsLocations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlAssetsLocations.ValueListItems.Clear();
		for (int i = 0; i < dtAssetsLocations.Rows.Count; i++)
		{
			vlAssetsLocations.ValueListItems.Add(dtAssetsLocations.Rows[i]["AssetLocationID"], dtAssetsLocations.Rows[i]["AssetLocationName"].ToString());
		}
		dtDetails = AssetsLocationsHistory.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0");
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AssetLocationHistoryID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AssetLocationID"].Header).Caption = (GlobalVariables.IsArabic ? "موقع الأصل" : "Asset Location");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AssetLocationID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AssetLocationID"].ValueList = (IValueList)(object)vlAssetsLocations;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AssetLocationID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من تاريخ" : "From Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Header).Caption = (GlobalVariables.IsArabic ? "الى تاريخ" : "To Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Note"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Note"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Note"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails = AssetsLocationsHistory.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		dtAssetData = Assets.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtDetails.Rows.Count == 0 && dtAssetData.Rows.Count > 0)
		{
			DataRow dataRow = dtDetails.NewRow();
			dataRow["AssetLocationHistoryID"] = "-1";
			dataRow["SubAccountID"] = ((TextEditorControlBase)cboHeader).Value;
			dataRow["AssetLocationID"] = dtAssetData.Rows[0]["AssetLocationID"];
			dataRow["FromDate"] = dtAssetData.Rows[0]["AcquisitionDate"];
			dtDetails.Rows.Add(dataRow);
		}
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ البداية", "Please Insert From Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["FromDate"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["AssetLocationID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار موقع الأصل ", "Please Select Asset Location");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["AssetLocationID"]).Selected = true;
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				if (k != j && ((((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value.ToString())) || (((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value.ToString())) || (((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value != DBNull.Value && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[k].Cells["ToDate"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذا التاريخ واقع فى فترة من قبل", "this Date in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[k]).Selected = true;
					return false;
				}
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
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["AssetLocationHistoryID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("AST_AssetsLocationsHistory", "SubAccountID", ((TextEditorControlBase)cboHeader).Value.ToString(), "AssetLocationHistoryID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				DataView dataView = new DataView(dtDetails);
				dataView.Sort = " FromDate Desc ";
				DataRow dataRow = dataView.ToTable().Rows[0];
				AssetsLocationsHistory.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
				if (dtAssetData == null || dtAssetData.Rows.Count == 0)
				{
					Assets.Insert_Update(dtAssets.Select(" SubAccountID= " + ((TextEditorControlBase)cboHeader).Value.ToString())[0]["SubAccountNumber"].ToString(), ((TextEditorControlBase)cboHeader).Value.ToString(), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", dataRow["AssetLocationID"].ToString(), "Null", "Null", "Null", "Null", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
				else
				{
					Assets.Insert_Update((dtAssetData.Rows[0]["AssetBarCode"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetBarCode"].ToString(), ((TextEditorControlBase)cboHeader).Value.ToString(), (dtAssetData.Rows[0]["AcquisitionDate"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AcquisitionDate"].ToString(), (dtAssetData.Rows[0]["StartDepreciationDate"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["StartDepreciationDate"].ToString(), (dtAssetData.Rows[0]["AssetStateID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetStateID"].ToString(), (dtAssetData.Rows[0]["ScrapValue"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["ScrapValue"].ToString(), (dtAssetData.Rows[0]["ScrapDate"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["ScrapDate"].ToString(), (dtAssetData.Rows[0]["SalesDate"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["SalesDate"].ToString(), (dtAssetData.Rows[0]["Notes"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["Notes"].ToString(), (dtAssetData.Rows[0]["AssetAccountID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetAccountID"].ToString(), (dtAssetData.Rows[0]["AssetAccumulatedDepreciationAccountID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetAccumulatedDepreciationAccountID"].ToString(), (dtAssetData.Rows[0]["AssetDepreciationExpenseAccountID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetDepreciationExpenseAccountID"].ToString(), (dtAssetData.Rows[0]["AssetSalesAccountID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetSalesAccountID"].ToString(), (dtAssetData.Rows[0]["AssetInvestmentAccountID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetInvestmentAccountID"].ToString(), dataRow["AssetLocationID"].ToString(), (dtAssetData.Rows[0]["CustodySubAccountID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["CustodySubAccountID"].ToString(), (dtAssetData.Rows[0]["AssetDepreciationTypeID"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["AssetDepreciationTypeID"].ToString(), (dtAssetData.Rows[0]["DepreciationPercentage"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["DepreciationPercentage"].ToString(), (dtAssetData.Rows[0]["DepreciationPeriodCount"] == DBNull.Value) ? "Null" : dtAssetData.Rows[0]["DepreciationPeriodCount"].ToString(), (dtAssetData.Rows[0]["HasTransAction"] == DBNull.Value) ? "0" : dtAssetData.Rows[0]["HasTransAction"].ToString(), (dtAssetData.Rows[0]["Deleted"] == DBNull.Value) ? "0" : (bool.Parse(dtAssetData.Rows[0]["Deleted"].ToString()) ? "1" : "0"), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				}
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "FromDate" && ((UltraGridBase)ULGData).ActiveRow.Cells["FromDate"].Value == DBNull.Value && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 1)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["FromDate"].Value = DateTime.Parse(((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index - 1].Cells["ToDate"].Value.ToString()).AddDays(1.0);
		}
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Assets("-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboHeader).Value = num;
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
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(414, 68);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(285, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(281, 72);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmAssetsLocationsChange";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
