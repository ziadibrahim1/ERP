using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.MasterData;

public class frmItemsColorCategorysDetails : frmDetails
{
	private DataTable dtCategorys;

	private DataTable dtItemColor;

	private ValueList vlItemColor = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtRangeTo;

	private UltraTextEditor txtRangeFrom;

	private UltraLabel lblLRangeFrom;

	private UltraLabel lblRangeTo;

	public UltraButton btnApplyRange;

	public frmItemsColorCategorysDetails()
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
		dtCategorys = ItemsColorCategorys.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtCategorys, "ItemColorCategoryID", "ItemColorCategoryName");
		dtItemColor = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItemColor.ValueListItems.Clear();
		for (int i = 0; i < dtItemColor.Rows.Count; i++)
		{
			vlItemColor.ValueListItems.Add(dtItemColor.Rows[i]["ColorID"], dtItemColor.Rows[i]["ColorCode"].ToString());
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
		dtDetails = ItemsColorCategorysDetails.SelectByItemColorCategoryID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemColorCategoryDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlItemColor;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.9);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails = ItemsColorCategorysDetails.SelectByItemColorCategoryID((cboHeader.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون", "Please Select Color");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["ColorID"]).Selected = true;
				return false;
			}
		}
		return true;
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
		for (int i = 0; i < dtItemColor.Rows.Count; i++)
		{
			if (decimal.TryParse(dtItemColor.Rows[i]["ColorName"].ToString(), out result) && result >= decimal.Parse(((Control)(object)txtRangeFrom).Text.Trim()) && result <= decimal.Parse(((Control)(object)txtRangeTo).Text.Trim()))
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = dtItemColor.Rows[i]["ColorID"];
			}
		}
		((UltraControlBase)ULGData).Update();
		SetControls(hasChanges: true);
	}

	private void txtRangeTo_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ItemColorCategoryDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["ItemColorCategoryID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
			}
			Main.SyncDeleteForUpdate("SC_ItemsColorCategorysDetails", "ItemColorCategoryID", ((TextEditorControlBase)cboHeader).Value.ToString(), "ItemColorCategoryDetailID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ItemsColorCategorysDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmItemsColorCategorysDetails));
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
		this.txtRangeTo = new UltraTextEditor();
		this.txtRangeFrom = new UltraTextEditor();
		this.lblLRangeFrom = new UltraLabel();
		this.lblRangeTo = new UltraLabel();
		this.btnApplyRange = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRangeTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRangeFrom).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSave, "btnSave");
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnSaveAndClose, "btnSaveAndClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtRangeTo, "txtRangeTo");
		((System.Windows.Forms.Control)(object)this.txtRangeTo).Name = "txtRangeTo";
		((System.Windows.Forms.Control)(object)this.txtRangeTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRangeTo_KeyPress);
		resources.ApplyResources(this.txtRangeFrom, "txtRangeFrom");
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).Name = "txtRangeFrom";
		((System.Windows.Forms.Control)(object)this.txtRangeFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRangeTo_KeyPress);
		resources.ApplyResources(this.lblLRangeFrom, "lblLRangeFrom");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblLRangeFrom).Appearance = (AppearanceBase)(object)val10;
		this.lblLRangeFrom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLRangeFrom).Name = "lblLRangeFrom";
		((ControlBase)this.lblLRangeFrom).WrapText = false;
		resources.ApplyResources(this.lblRangeTo, "lblRangeTo");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblRangeTo).Appearance = (AppearanceBase)(object)val11;
		this.lblRangeTo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRangeTo).Name = "lblRangeTo";
		((ControlBase)this.lblRangeTo).WrapText = false;
		resources.ApplyResources(this.btnApplyRange, "btnApplyRange");
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Name = "btnApplyRange";
		((System.Windows.Forms.Control)(object)this.btnApplyRange).Click += new System.EventHandler(btnApplyRange_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnApplyRange);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRangeTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLRangeFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRangeTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRangeFrom);
		base.Name = "frmItemsColorCategorysDetails";
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
