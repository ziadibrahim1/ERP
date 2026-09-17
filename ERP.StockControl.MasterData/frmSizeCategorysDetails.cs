using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.MasterData;

public class frmSizeCategorysDetails : frmDetails
{
	private DataTable dtCategorys;

	private DataTable dtItemSize;

	private ValueList vlItemSize = new ValueList();

	private IContainer components = null;

	public UltraButton btnApplyRange;

	private UltraLabel lblRangeTo;

	private UltraLabel lblLRangeFrom;

	private UltraTextEditor txtRangeTo;

	private UltraTextEditor txtRangeFrom;

	public frmSizeCategorysDetails()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم المجموعه" : "Category Name");
		((Control)(object)lblRangeTo).Text = (GlobalVariables.IsArabic ? "الى" : "To");
		((Control)(object)lblLRangeFrom).Text = (GlobalVariables.IsArabic ? "من" : "From");
	}

	public override void PrepareData()
	{
		dtCategorys = ItemsSizeCategorys.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtCategorys, "ItemSizeCategoryID", "ItemSizeCategoryName");
		dtItemSize = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItemSize.ValueListItems.Clear();
		for (int i = 0; i < dtItemSize.Rows.Count; i++)
		{
			vlItemSize.ValueListItems.Add(dtItemSize.Rows[i]["ItemSizeID"], dtItemSize.Rows[i]["ItemSizeName"].ToString());
		}
		UltraTextEditor obj = txtRangeFrom;
		UltraTextEditor obj2 = txtRangeTo;
		UltraLabel obj3 = lblLRangeFrom;
		UltraLabel obj4 = lblRangeTo;
		bool flag = (((Control)(object)btnApplyRange).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='LensesLab'")[0]["Installed"]) || Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]));
		bool flag3 = (((Control)(object)obj4).Visible = flag);
		bool flag5 = (((Control)(object)obj3).Visible = flag3);
		bool visible = (((Control)(object)obj2).Visible = flag5);
		((Control)(object)obj).Visible = visible;
		dtDetails = ItemsSizeCategorysDetails.SelectByItemSizeCategoryID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeCategoryDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlItemSize;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.9);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails = ItemsSizeCategorysDetails.SelectByItemSizeCategoryID((cboHeader.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس", "Please Select Size");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"]).Selected = true;
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
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeCategoryDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeCategoryID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
			}
			Main.SyncDeleteForUpdate("SC_ItemsSizeCategorysDetails", "ItemSizeCategoryID", ((TextEditorControlBase)cboHeader).Value.ToString(), "ItemSizeCategoryDetailID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ItemsSizeCategorysDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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

	private void btnApplyRange_Click(object sender, EventArgs e)
	{
		if (cboHeader.SelectedIndex <= -1 || !(((Control)(object)txtRangeFrom).Text.Trim() != "") || !(((Control)(object)txtRangeTo).Text.Trim() != ""))
		{
			return;
		}
		DataTable dataSource = dtDetails.Clone();
		((UltraGridBase)ULGData).DataSource = dataSource;
		decimal result = default(decimal);
		for (int i = 0; i < dtItemSize.Rows.Count; i++)
		{
			if (decimal.TryParse(dtItemSize.Rows[i]["ItemSizeName"].ToString(), out result) && result >= decimal.Parse(((Control)(object)txtRangeFrom).Text.Trim()) && result <= decimal.Parse(((Control)(object)txtRangeTo).Text.Trim()))
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = dtItemSize.Rows[i]["ItemSizeID"];
			}
		}
		((UltraControlBase)ULGData).Update();
		SetControls(hasChanges: true);
	}

	private void txtRangeTo_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
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
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
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
		this.btnApplyRange = new UltraButton();
		this.lblRangeTo = new UltraLabel();
		this.lblLRangeFrom = new UltraLabel();
		this.txtRangeTo = new UltraTextEditor();
		this.txtRangeFrom = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRangeTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRangeFrom).BeginInit();
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
		((AppearanceBase)val8).FontData.Name = "Tahoma";
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(590, 67);
		((System.Windows.Forms.Control)(object)base.cboHeader).TabIndex = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.Name = "Tahoma";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(482, 70);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(59, 18);
		((System.Windows.Forms.Control)(object)base.lblHeader).Text = "Currency";
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		((UltraButtonBase)this.btnApplyRange).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Location = new System.Drawing.Point(317, 65);
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Name = "btnApplyRange";
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Size = new System.Drawing.Size(100, 28);
		((System.Windows.Forms.Control)(object)this.btnApplyRange).TabIndex = 641;
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Text = "Apply";
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Visible = false;
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Click += new System.EventHandler(btnApplyRange_Click);
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblRangeTo).Appearance = (AppearanceBase)(object)val10;
		this.lblRangeTo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRangeTo).AutoSize = true;
		this.lblRangeTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblRangeTo).Location = new System.Drawing.Point(172, 70);
		((System.Windows.Forms.Control)(object)this.lblRangeTo).Name = "lblRangeTo";
		((System.Windows.Forms.Control)(object)this.lblRangeTo).Size = new System.Drawing.Size(20, 18);
		((System.Windows.Forms.Control)(object)this.lblRangeTo).TabIndex = 639;
		((System.Windows.Forms.Control)(object)this.lblRangeTo).Text = "To";
		((ControlBase)this.lblRangeTo).WrapText = false;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblLRangeFrom).Appearance = (AppearanceBase)(object)val11;
		this.lblLRangeFrom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).AutoSize = true;
		this.lblLRangeFrom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).Location = new System.Drawing.Point(31, 70);
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).Name = "lblLRangeFrom";
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).Size = new System.Drawing.Size(36, 18);
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).TabIndex = 640;
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).Text = "From";
		((ControlBase)this.lblLRangeFrom).WrapText = false;
		((System.Windows.Forms.Control)(object)this.txtRangeTo).Location = new System.Drawing.Point(216, 67);
		((System.Windows.Forms.Control)(object)this.txtRangeTo).Name = "txtRangeTo";
		((System.Windows.Forms.Control)(object)this.txtRangeTo).Size = new System.Drawing.Size(84, 25);
		((System.Windows.Forms.Control)(object)this.txtRangeTo).TabIndex = 638;
		((System.Windows.Forms.Control)(object)this.txtRangeTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRangeTo_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).Location = new System.Drawing.Point(82, 67);
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).Name = "txtRangeFrom";
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).Size = new System.Drawing.Size(84, 25);
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).TabIndex = 637;
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRangeTo_KeyPress);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnApplyRange);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRangeTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLRangeFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRangeTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRangeFrom);
		base.Name = "frmSizeCategorysDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRangeFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRangeTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLRangeFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRangeTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnApplyRange, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRangeTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRangeFrom).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
