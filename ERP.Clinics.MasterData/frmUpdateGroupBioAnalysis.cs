using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.MasterData;

public class frmUpdateGroupBioAnalysis : frmBase
{
	private DataTable dtBioAnalysisTypes;

	private DataTable dtGroups;

	private DataTable dtItemPrices;

	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private ValueList vlBranches2 = new ValueList();

	private string GroupID;

	private DataRow drGroup;

	private IContainer components = null;

	public UltraLabel lblRoot;

	public UltraComboEditor cboGroup;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	public UltraGrid ULGPrices;

	private UltraPanel pnlPrice;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	private UltraCheckEditor chkIsActive;

	private UltraCheckEditor chkIsIndoor;

	private UltraCheckEditor chkHasAttachement;

	private UltraCheckEditor chkModPrices;

	private UltraCheckEditor chkModActive;

	private UltraCheckEditor chkModHasAttachment;

	private UltraCheckEditor chkModIsIndoor;

	private UltraPanel pnlMaleValues;

	private UltraCheckEditor chkModMaleValues;

	private UltraComboEditor cboTypes;

	private UltraLabel lblTypes;

	public UltraLabel ultraLabel2;

	private UltraCheckEditor chkModItemsTypes;

	public UltraTextEditor txtFemaleMaxValue;

	public UltraTextEditor txtMaleMaxValue;

	public UltraLabel lblFemaleMaxValue;

	public UltraLabel lblMaleMaxValue;

	public UltraTextEditor txtFemaleMinValue;

	public UltraLabel lblFemaleMinValue;

	public UltraTextEditor txtMaleMinValue;

	public UltraLabel lblMaleMinValue;

	private UltraPanel pnlFemaleValues;

	private UltraPanel pnlSamples;

	private UltraTextEditor txtSamplesPeriod;

	private UltraTextEditor txtSamplesCount;

	private UltraLabel ultraLabel1;

	private UltraLabel lblSamplesPeriod;

	private UltraLabel lblSamplesCount;

	private UltraCheckEditor chkModFemaleValues;

	private UltraCheckEditor chkModSamples;

	private UltraCheckEditor chkModNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblReceivingBank;

	private UltraCheckEditor chkIsSalesItem;

	private UltraCheckEditor chkModIsSalesItem;

	private UltraCheckEditor chkIsRecipe;

	private UltraCheckEditor chkModIsRecipe;

	public frmUpdateGroupBioAnalysis(string groupID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		GroupID = groupID;
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtGroups = BioAnalysis.FillGroups("-1", "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGroup, dtGroups, "ItemID", "Name");
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		vlBranches2.ValueListItems.Clear();
		for (int i = 0; i < dtBranches.Rows.Count; i++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
			vlBranches2.ValueListItems.Add(dtBranches.Rows[i]["BranchID"], dtBranches.Rows[i][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPricesTypes.Rows.Count; j++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[j]["PriceTypeID"], dtPricesTypes.Rows[j]["PriceName"].ToString());
		}
		dtBioAnalysisTypes = BioAnalysisTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTypes, dtBioAnalysisTypes, "BioAnalysisTypeID", GlobalVariables.IsArabic ? "BioAnalysisTypeNameAr" : "BioAnalysisTypeNameEn");
		DisplayData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	public void DisplayData()
	{
		drGroup = BioAnalysis.Select(GroupID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
		((TextEditorControlBase)cboGroup).Value = GroupID;
		((TextEditorControlBase)cboTypes).Value = drGroup["BioAnalysisTypeID"];
		((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(drGroup["IsActive"]);
		((UltraToggleEditorBase)chkIsIndoor).Checked = Convert.ToBoolean(drGroup["IsIndoor"]);
		((UltraToggleEditorBase)chkHasAttachement).Checked = Convert.ToBoolean(drGroup["HasAttachement"]);
		rbPriceForAllBranches.Checked = Convert.ToBoolean(drGroup["PriceForAllBranch"]);
		rbPriceForEachBranches.Checked = !Convert.ToBoolean(drGroup["PriceForAllBranch"]);
		((Control)(object)txtNotes).Text = drGroup["Notes"].ToString();
		((Control)(object)txtFemaleMaxValue).Text = drGroup["FemaleMaxValue"].ToString();
		((Control)(object)txtFemaleMinValue).Text = drGroup["FemaleMinValue"].ToString();
		((Control)(object)txtMaleMaxValue).Text = drGroup["MaleMaxValue"].ToString();
		((Control)(object)txtMaleMinValue).Text = drGroup["MaleMinValue"].ToString();
		((Control)(object)txtSamplesCount).Text = drGroup["SamplesCount"].ToString();
		((Control)(object)txtSamplesPeriod).Text = drGroup["SamplesPeriod"].ToString();
		rbPriceForAllBranches.CheckedChanged -= rbPriceForAllBranches_CheckedChanged;
		rbPriceForAllBranches.Checked = Convert.ToBoolean(drGroup["PriceForAllBranch"]);
		rbPriceForEachBranches.Checked = !Convert.ToBoolean(drGroup["PriceForAllBranch"]);
		rbPriceForAllBranches.CheckedChanged += rbPriceForAllBranches_CheckedChanged;
		dtItemPrices = ItemsPrices.SelectByGroupID(GroupID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
	}

	public bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkModItemsTypes).Checked && cboTypes.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار النوع", "Please select Type");
			((TextEditorControlBase)cboTypes).Focus();
			return false;
		}
		return true;
	}

	public void Save()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			BioAnalysis.UpdateGroupBioAnalysis(((TextEditorControlBase)cboGroup).Value.ToString(), (!((UltraToggleEditorBase)chkModIsSalesItem).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsSalesItem).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModIsRecipe).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsRecipe).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModItemsTypes).Checked) ? "-1" : ((cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString()), (!((UltraToggleEditorBase)chkModSamples).Checked || ((Control)(object)txtSamplesCount).Text.Trim() == "") ? "-1" : ((Control)(object)txtSamplesCount).Text, (!((UltraToggleEditorBase)chkModSamples).Checked || ((Control)(object)txtSamplesPeriod).Text.Trim() == "") ? "-1" : ((Control)(object)txtSamplesPeriod).Text, (!((UltraToggleEditorBase)chkModIsIndoor).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsIndoor).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModHasAttachment).Checked) ? "-1" : (((UltraToggleEditorBase)chkHasAttachement).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModPrices).Checked) ? "-1" : (rbPriceForAllBranches.Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModActive).Checked) ? "-1" : (((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModNotes).Checked || ((Control)(object)txtNotes).Text.Trim() == "") ? "-1" : ((Control)(object)txtNotes).Text, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Items Saved");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void cboRoot_ValueChanged(object sender, EventArgs e)
	{
	}

	private void rbPriceForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void FillGridPrices()
	{
		dtItemPrices.Rows.Clear();
		if (rbPriceForAllBranches.Checked)
		{
			for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
			{
				DataRow dataRow = dtItemPrices.NewRow();
				dataRow["ItemPriceID"] = -1;
				dataRow["Selected"] = false;
				dataRow["ItemID"] = -1;
				dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
				dataRow["Price"] = 0;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = DBNull.Value;
				dtItemPrices.Rows.Add(dataRow);
			}
		}
		else
		{
			for (int j = 0; j < dtBranches.Rows.Count; j++)
			{
				for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
				{
					DataRow dataRow2 = dtItemPrices.NewRow();
					dataRow2["ItemPriceID"] = -1;
					dataRow2["Selected"] = false;
					dataRow2["ItemID"] = -1;
					dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
					dataRow2["Price"] = 0;
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
					dtItemPrices.Rows.Add(dataRow2);
				}
			}
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		((UltraGridBase)ULGPrices).DataSource = dtItemPrices;
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ItemPriceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Header).Caption = "ـ";
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Hidden = false;
		if (rbPriceForAllBranches.Checked)
		{
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.1);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		}
		else
		{
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.25);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.1);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.25);
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
		}
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void chkModPrices_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlPrice).Enabled = ((UltraToggleEditorBase)chkModPrices).Checked;
	}

	private void chkModActive_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkIsActive).Enabled = ((UltraToggleEditorBase)chkModActive).Checked;
	}

	private void chkModProductionItem_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkIsIndoor).Enabled = ((UltraToggleEditorBase)chkModIsIndoor).Checked;
	}

	private void chkModSalseItem_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkHasAttachement).Enabled = ((UltraToggleEditorBase)chkModHasAttachment).Checked;
	}

	private void chkModPOS_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlMaleValues).Enabled = ((UltraToggleEditorBase)chkModMaleValues).Checked;
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void txtInt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID" || (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "Price" && !Convert.ToBoolean(((UltraGridBase)ULGPrices).ActiveRow.Cells["Selected"].Value)))
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void chkModItemsTypes_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboTypes).Enabled = ((UltraToggleEditorBase)chkModItemsTypes).Checked;
	}

	private void chkModFemaleValues_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlFemaleValues).Enabled = ((UltraToggleEditorBase)chkModFemaleValues).Checked;
	}

	private void chkModSamples_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)pnlSamples).Enabled = ((UltraToggleEditorBase)chkModSamples).Checked;
	}

	private void chkModNotes_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtNotes).Enabled = ((UltraToggleEditorBase)chkModNotes).Checked;
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
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Expected O, but got Unknown
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Expected O, but got Unknown
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Expected O, but got Unknown
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Expected O, but got Unknown
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Expected O, but got Unknown
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Expected O, but got Unknown
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected O, but got Unknown
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Expected O, but got Unknown
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmUpdateGroupBioAnalysis));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		this.pnlPrice = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.ULGPrices = new UltraGrid();
		this.ultraLabel2 = new UltraLabel();
		this.pnlMaleValues = new UltraPanel();
		this.txtMaleMaxValue = new UltraTextEditor();
		this.lblMaleMaxValue = new UltraLabel();
		this.txtMaleMinValue = new UltraTextEditor();
		this.lblMaleMinValue = new UltraLabel();
		this.cboTypes = new UltraComboEditor();
		this.lblTypes = new UltraLabel();
		this.lblRoot = new UltraLabel();
		this.cboGroup = new UltraComboEditor();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.chkIsIndoor = new UltraCheckEditor();
		this.chkHasAttachement = new UltraCheckEditor();
		this.chkModPrices = new UltraCheckEditor();
		this.chkModActive = new UltraCheckEditor();
		this.chkModHasAttachment = new UltraCheckEditor();
		this.chkModIsIndoor = new UltraCheckEditor();
		this.chkModMaleValues = new UltraCheckEditor();
		this.chkModItemsTypes = new UltraCheckEditor();
		this.txtFemaleMaxValue = new UltraTextEditor();
		this.lblFemaleMaxValue = new UltraLabel();
		this.txtFemaleMinValue = new UltraTextEditor();
		this.lblFemaleMinValue = new UltraLabel();
		this.pnlFemaleValues = new UltraPanel();
		this.pnlSamples = new UltraPanel();
		this.txtSamplesPeriod = new UltraTextEditor();
		this.txtSamplesCount = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.lblSamplesPeriod = new UltraLabel();
		this.lblSamplesCount = new UltraLabel();
		this.chkModFemaleValues = new UltraCheckEditor();
		this.chkModSamples = new UltraCheckEditor();
		this.chkModNotes = new UltraCheckEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblReceivingBank = new UltraLabel();
		this.chkIsSalesItem = new UltraCheckEditor();
		this.chkModIsSalesItem = new UltraCheckEditor();
		this.chkIsRecipe = new UltraCheckEditor();
		this.chkModIsRecipe = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlPrice).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlMaleValues).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtMaleMaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaleMinValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsIndoor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasAttachement).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModHasAttachment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsIndoor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModMaleValues).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModItemsTypes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFemaleMaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFemaleMinValue).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlSamples).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModFemaleValues).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSamples).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsSalesItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecipe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsRecipe).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlPrice, "pnlPrice");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance42");
		this.pnlPrice.Appearance = (AppearanceBase)(object)val;
		this.pnlPrice.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlPrice.ClientArea, "pnlPrice.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add(this.rbPriceForEachBranches);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add(this.rbPriceForAllBranches);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.pnlPrice).Name = "pnlPrice";
		((UltraControlBase)this.pnlPrice).UseAppStyling = false;
		resources.ApplyResources(this.rbPriceForEachBranches, "rbPriceForEachBranches");
		this.rbPriceForEachBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForEachBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForEachBranches.Name = "rbPriceForEachBranches";
		this.rbPriceForEachBranches.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbPriceForAllBranches, "rbPriceForAllBranches");
		this.rbPriceForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForAllBranches.Checked = true;
		this.rbPriceForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForAllBranches.Name = "rbPriceForAllBranches";
		this.rbPriceForAllBranches.TabStop = true;
		this.rbPriceForAllBranches.UseVisualStyleBackColor = false;
		this.rbPriceForAllBranches.CheckedChanged += new System.EventHandler(rbPriceForAllBranches_CheckedChanged);
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val2).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val2, "appearance2");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val3;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance8");
		((AppearanceBase)val8).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.pnlMaleValues, "pnlMaleValues");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance1");
		this.pnlMaleValues.Appearance = (AppearanceBase)(object)val12;
		this.pnlMaleValues.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlMaleValues.ClientArea, "pnlMaleValues.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtMaleMaxValue);
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblMaleMaxValue);
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtMaleMinValue);
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblMaleMinValue);
		((System.Windows.Forms.Control)(object)this.pnlMaleValues).Name = "pnlMaleValues";
		((UltraControlBase)this.pnlMaleValues).UseAppStyling = false;
		resources.ApplyResources(this.txtMaleMaxValue, "txtMaleMaxValue");
		((System.Windows.Forms.Control)(object)this.txtMaleMaxValue).Name = "txtMaleMaxValue";
		resources.ApplyResources(this.lblMaleMaxValue, "lblMaleMaxValue");
		this.lblMaleMaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaleMaxValue).Name = "lblMaleMaxValue";
		((ControlBase)this.lblMaleMaxValue).WrapText = false;
		resources.ApplyResources(this.txtMaleMinValue, "txtMaleMinValue");
		((System.Windows.Forms.Control)(object)this.txtMaleMinValue).Name = "txtMaleMinValue";
		resources.ApplyResources(this.lblMaleMinValue, "lblMaleMinValue");
		this.lblMaleMinValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaleMinValue).Name = "lblMaleMinValue";
		((ControlBase)this.lblMaleMinValue).WrapText = false;
		resources.ApplyResources(this.cboTypes, "cboTypes");
		((System.Windows.Forms.Control)(object)this.cboTypes).Name = "cboTypes";
		resources.ApplyResources(this.lblTypes, "lblTypes");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance12");
		((ControlBase)this.lblTypes).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.lblTypes).Name = "lblTypes";
		resources.ApplyResources(this.lblRoot, "lblRoot");
		this.lblRoot.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoot).Name = "lblRoot";
		((ControlBase)this.lblRoot).WrapText = false;
		resources.ApplyResources(this.cboGroup, "cboGroup");
		this.cboGroup.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboGroup).Name = "cboGroup";
		((TextEditorControlBase)this.cboGroup).Nullable = false;
		((TextEditorControlBase)this.cboGroup).ValueChanged += new System.EventHandler(cboRoot_ValueChanged);
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val14).Image = resources.GetObject("appearance43.Image");
		resources.ApplyResources(val14, "appearance43");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val14;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val15, "appearance44");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val16).Image = resources.GetObject("appearance45.Image");
		resources.ApplyResources(val16, "appearance45");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val16;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val17).Image = resources.GetObject("appearance46.Image");
		resources.ApplyResources(val17, "appearance46");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val17;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val18).Image = resources.GetObject("appearance47.Image");
		resources.ApplyResources(val18, "appearance47");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val18;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val19).Image = resources.GetObject("appearance48.Image");
		resources.ApplyResources(val19, "appearance48");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance49");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val20;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.chkIsIndoor, "chkIsIndoor");
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance50");
		((UltraToggleEditorBase)this.chkIsIndoor).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.chkIsIndoor).Name = "chkIsIndoor";
		resources.ApplyResources(this.chkHasAttachement, "chkHasAttachement");
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance51");
		((UltraToggleEditorBase)this.chkHasAttachement).Appearance = (AppearanceBase)(object)val22;
		((System.Windows.Forms.Control)(object)this.chkHasAttachement).Name = "chkHasAttachement";
		resources.ApplyResources(this.chkModPrices, "chkModPrices");
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance52");
		((UltraToggleEditorBase)this.chkModPrices).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.chkModPrices).Name = "chkModPrices";
		((UltraToggleEditorBase)this.chkModPrices).CheckedChanged += new System.EventHandler(chkModPrices_CheckedChanged);
		resources.ApplyResources(this.chkModActive, "chkModActive");
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance53");
		((UltraToggleEditorBase)this.chkModActive).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.chkModActive).Name = "chkModActive";
		((UltraToggleEditorBase)this.chkModActive).CheckedChanged += new System.EventHandler(chkModActive_CheckedChanged);
		resources.ApplyResources(this.chkModHasAttachment, "chkModHasAttachment");
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance54");
		((UltraToggleEditorBase)this.chkModHasAttachment).Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.chkModHasAttachment).Name = "chkModHasAttachment";
		((UltraToggleEditorBase)this.chkModHasAttachment).CheckedChanged += new System.EventHandler(chkModSalseItem_CheckedChanged);
		resources.ApplyResources(this.chkModIsIndoor, "chkModIsIndoor");
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance55");
		((UltraToggleEditorBase)this.chkModIsIndoor).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.chkModIsIndoor).Name = "chkModIsIndoor";
		((UltraToggleEditorBase)this.chkModIsIndoor).CheckedChanged += new System.EventHandler(chkModProductionItem_CheckedChanged);
		resources.ApplyResources(this.chkModMaleValues, "chkModMaleValues");
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance56");
		((UltraToggleEditorBase)this.chkModMaleValues).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.chkModMaleValues).Name = "chkModMaleValues";
		((UltraToggleEditorBase)this.chkModMaleValues).CheckedChanged += new System.EventHandler(chkModPOS_CheckedChanged);
		resources.ApplyResources(this.chkModItemsTypes, "chkModItemsTypes");
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance57");
		((UltraToggleEditorBase)this.chkModItemsTypes).Appearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.chkModItemsTypes).Name = "chkModItemsTypes";
		((UltraToggleEditorBase)this.chkModItemsTypes).CheckedChanged += new System.EventHandler(chkModItemsTypes_CheckedChanged);
		resources.ApplyResources(this.txtFemaleMaxValue, "txtFemaleMaxValue");
		((System.Windows.Forms.Control)(object)this.txtFemaleMaxValue).Name = "txtFemaleMaxValue";
		resources.ApplyResources(this.lblFemaleMaxValue, "lblFemaleMaxValue");
		this.lblFemaleMaxValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFemaleMaxValue).Name = "lblFemaleMaxValue";
		((ControlBase)this.lblFemaleMaxValue).WrapText = false;
		resources.ApplyResources(this.txtFemaleMinValue, "txtFemaleMinValue");
		((System.Windows.Forms.Control)(object)this.txtFemaleMinValue).Name = "txtFemaleMinValue";
		resources.ApplyResources(this.lblFemaleMinValue, "lblFemaleMinValue");
		this.lblFemaleMinValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFemaleMinValue).Name = "lblFemaleMinValue";
		((ControlBase)this.lblFemaleMinValue).WrapText = false;
		resources.ApplyResources(this.pnlFemaleValues, "pnlFemaleValues");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val29, "appearance58");
		this.pnlFemaleValues.Appearance = (AppearanceBase)(object)val29;
		this.pnlFemaleValues.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlFemaleValues.ClientArea, "pnlFemaleValues.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblFemaleMinValue);
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtFemaleMinValue);
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblFemaleMaxValue);
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtFemaleMaxValue);
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues).Name = "pnlFemaleValues";
		((UltraControlBase)this.pnlFemaleValues).UseAppStyling = false;
		resources.ApplyResources(this.pnlSamples, "pnlSamples");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val30, "appearance29");
		this.pnlSamples.Appearance = (AppearanceBase)(object)val30;
		this.pnlSamples.BorderStyle = (UIElementBorderStyle)7;
		resources.ApplyResources(this.pnlSamples.ClientArea, "pnlSamples.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtSamplesPeriod);
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.txtSamplesCount);
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblSamplesPeriod);
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).Controls.Add((System.Windows.Forms.Control)(object)this.lblSamplesCount);
		((System.Windows.Forms.Control)(object)this.pnlSamples).Name = "pnlSamples";
		((UltraControlBase)this.pnlSamples).UseAppStyling = false;
		resources.ApplyResources(this.txtSamplesPeriod, "txtSamplesPeriod");
		((System.Windows.Forms.Control)(object)this.txtSamplesPeriod).Name = "txtSamplesPeriod";
		resources.ApplyResources(this.txtSamplesCount, "txtSamplesCount");
		((System.Windows.Forms.Control)(object)this.txtSamplesCount).Name = "txtSamplesCount";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val31, "appearance59");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val31;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.lblSamplesPeriod, "lblSamplesPeriod");
		((AppearanceBase)val32).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val32, "appearance60");
		((ControlBase)this.lblSamplesPeriod).Appearance = (AppearanceBase)(object)val32;
		this.lblSamplesPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSamplesPeriod).Name = "lblSamplesPeriod";
		((ControlBase)this.lblSamplesPeriod).WrapText = false;
		resources.ApplyResources(this.lblSamplesCount, "lblSamplesCount");
		((AppearanceBase)val33).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val33, "appearance61");
		((ControlBase)this.lblSamplesCount).Appearance = (AppearanceBase)(object)val33;
		this.lblSamplesCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSamplesCount).Name = "lblSamplesCount";
		((ControlBase)this.lblSamplesCount).WrapText = false;
		resources.ApplyResources(this.chkModFemaleValues, "chkModFemaleValues");
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val34, "appearance30");
		((UltraToggleEditorBase)this.chkModFemaleValues).Appearance = (AppearanceBase)(object)val34;
		((System.Windows.Forms.Control)(object)this.chkModFemaleValues).Name = "chkModFemaleValues";
		((UltraToggleEditorBase)this.chkModFemaleValues).CheckedChanged += new System.EventHandler(chkModFemaleValues_CheckedChanged);
		resources.ApplyResources(this.chkModSamples, "chkModSamples");
		((AppearanceBase)val35).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val35, "appearance62");
		((UltraToggleEditorBase)this.chkModSamples).Appearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.chkModSamples).Name = "chkModSamples";
		((UltraToggleEditorBase)this.chkModSamples).CheckedChanged += new System.EventHandler(chkModSamples_CheckedChanged);
		resources.ApplyResources(this.chkModNotes, "chkModNotes");
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance63");
		((UltraToggleEditorBase)this.chkModNotes).Appearance = (AppearanceBase)(object)val36;
		((System.Windows.Forms.Control)(object)this.chkModNotes).Name = "chkModNotes";
		((UltraToggleEditorBase)this.chkModNotes).CheckedChanged += new System.EventHandler(chkModNotes_CheckedChanged);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblReceivingBank, "lblReceivingBank");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val37).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val37, "appearance64");
		((ControlBase)this.lblReceivingBank).Appearance = (AppearanceBase)(object)val37;
		this.lblReceivingBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceivingBank).Name = "lblReceivingBank";
		((ControlBase)this.lblReceivingBank).WrapText = false;
		resources.ApplyResources(this.chkIsSalesItem, "chkIsSalesItem");
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance65");
		((UltraToggleEditorBase)this.chkIsSalesItem).Appearance = (AppearanceBase)(object)val38;
		((System.Windows.Forms.Control)(object)this.chkIsSalesItem).Name = "chkIsSalesItem";
		resources.ApplyResources(this.chkModIsSalesItem, "chkModIsSalesItem");
		((AppearanceBase)val39).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val39, "appearance66");
		((UltraToggleEditorBase)this.chkModIsSalesItem).Appearance = (AppearanceBase)(object)val39;
		((System.Windows.Forms.Control)(object)this.chkModIsSalesItem).Name = "chkModIsSalesItem";
		((UltraToggleEditorBase)this.chkModIsSalesItem).CheckedChanged += new System.EventHandler(chkModActive_CheckedChanged);
		resources.ApplyResources(this.chkIsRecipe, "chkIsRecipe");
		((AppearanceBase)val40).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val40, "appearance67");
		((UltraToggleEditorBase)this.chkIsRecipe).Appearance = (AppearanceBase)(object)val40;
		((System.Windows.Forms.Control)(object)this.chkIsRecipe).Name = "chkIsRecipe";
		resources.ApplyResources(this.chkModIsRecipe, "chkModIsRecipe");
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val41, "appearance68");
		((UltraToggleEditorBase)this.chkModIsRecipe).Appearance = (AppearanceBase)(object)val41;
		((System.Windows.Forms.Control)(object)this.chkModIsRecipe).Name = "chkModIsRecipe";
		((UltraToggleEditorBase)this.chkModIsRecipe).CheckedChanged += new System.EventHandler(chkModActive_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingBank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlMaleValues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModIsIndoor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModHasAttachment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModIsRecipe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModIsSalesItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlFemaleValues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModItemsTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsRecipe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlSamples);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsSalesItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsIndoor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModMaleValues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHasAttachement);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModFemaleValues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModPrices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModSamples);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoot);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboGroup);
		base.Name = "frmUpdateGroupBioAnalysis";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModSamples, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModPrices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModFemaleValues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHasAttachement, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModMaleValues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsIndoor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsSalesItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlSamples, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsRecipe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModItemsTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlFemaleValues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModActive, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModIsSalesItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModIsRecipe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModHasAttachment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModIsIndoor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlMaleValues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReceivingBank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlPrice.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlPrice).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlMaleValues.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlMaleValues).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtMaleMaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMaleMinValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsIndoor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHasAttachement).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModHasAttachment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsIndoor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModMaleValues).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModItemsTypes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFemaleMaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFemaleMinValue).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlFemaleValues).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlSamples.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlSamples).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtSamplesPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSamplesCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModFemaleValues).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModSamples).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsSalesItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsSalesItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecipe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModIsRecipe).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
