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

public class frmItemsBatches : frmDetails
{
	private DataTable dtItems;

	private int ValidityDays = 0;

	private IContainer components = null;

	private UltraCheckEditor chkForAllItems;

	public frmItemsBatches()
	{
		InitializeComponent();
	}

	public frmItemsBatches(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtItems, "ItemID", "Name");
		if (RowID == "")
		{
			dtDetails = ItemsBatches.SelectByItemID("0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGrid();
		}
		else
		{
			((TextEditorControlBase)cboHeader).Value = RowID;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchDate"].Header).Caption = (GlobalVariables.IsArabic ? " تاريخ الإنتاج" : "Manufacturer Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidityDays"].Header).Caption = (GlobalVariables.IsArabic ? "مدة الصلاحية" : "Validity Days");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidityDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValidityDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchEndDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الإنتهاء" : "Expire Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchEndDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchEndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void DisplayData()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		ULGData.InitializeRow -= new InitializeRowEventHandler(ULGData_InitializeRow);
		base.DisplayData();
		dtDetails = ItemsBatches.SelectByItemID((cboHeader.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboHeader).Value.ToString(), ((UltraToggleEditorBase)chkForAllItems).Checked ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (cboHeader.SelectedIndex > -1)
		{
			ValidityDays = int.Parse(Main.ExecuteQuery_DataTable(" Select isnull(ValidityDays,0)AS ValidityDays  From SC_Items Where itemID= " + ((TextEditorControlBase)cboHeader).Value.ToString()).Rows[0]["ValidityDays"].ToString());
		}
		InitGrid();
		ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Items("-1", "-1", "0", "-1", "-1", "-1", "-1", "1", "-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboHeader).Value = num;
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["BatchNo"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال سريل", "Please Insert Batch No");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["BatchNo"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["BatchDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ ألإنتاج", "Please Insert Manufacter Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["BatchDate"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ValidityDays"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال مدة الصلاحية", "Please Insert Validity Days");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["ValidityDays"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["BatchEndDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ ألإنتهاء", "Please Insert End Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["BatchEndDate"]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (j != i && ((UltraGridBase)ULGData).Rows[i].Cells["BatchNo"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchNo"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["BatchDate"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BatchDate"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("سريل مع تاريخ الانتاج موجود من قبل", "Duplicate Batch No And Batch Date ");
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
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value = ((cboHeader.SelectedIndex == -1) ? DBNull.Value : ((TextEditorControlBase)cboHeader).Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value.ToString() + ",";
			}
			ItemsBatches.DeleteForUpdate((cboHeader.SelectedIndex == -1) ? "0" : ((TextEditorControlBase)cboHeader).Value.ToString(), ((UltraToggleEditorBase)chkForAllItems).Checked ? "1" : "0", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ItemsBatches.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ValidityDays")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (!((UltraToggleEditorBase)chkForAllItems).Checked)
		{
			base.SelectFullRow(sender, e);
		}
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchEndDate" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ValidityDays")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_InitializeRow(object sender, InitializeRowEventArgs e)
	{
		e.Row.Cells["ValidityDays"].Value = ValidityDays;
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchDate" && e.Cell.Value != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchEndDate"].Value = DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BatchDate"].Value.ToString()).AddDays(ValidityDays);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void cboHeader_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		int num = SearchFunctions.Items("-1", "-1", "0", "-1", "-1", "-1", "-1", "1", "-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)(UltraComboEditor)sender).Value = num;
		}
	}

	private void chkForAllItems_CheckedChanged(object sender, EventArgs e)
	{
		((EditorButtonControlBase)cboHeader).ReadOnly = ((UltraToggleEditorBase)chkForAllItems).Checked;
		cboHeader.SelectedIndex = -1;
		DisplayData();
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		int num = int.Parse(GlobalVariables.dtSystemDefaults.Select("DefaultEnName='ProductionBatchNoStartNo'")[0]["DefaultValue"].ToString());
		int num2 = int.Parse(GlobalVariables.dtSystemDefaults.Select("DefaultEnName='ProductionBatchNoEndNo'")[0]["DefaultValue"].ToString());
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (int.TryParse(e.Rows[i].Cells["BatchNo"].Value.ToString(), out var result) && result >= num && result <= num2)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف هذا السريل", "Serial Cannot Delete");
				((CancelEventArgs)(object)e).Cancel = true;
				return;
			}
		}
		base.ULGData_BeforeRowsDeleted(sender, e);
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmItemsBatches));
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
		this.chkForAllItems = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllItems).BeginInit();
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.InitializeRow += new InitializeRowEventHandler(ULGData_InitializeRow);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).KeyDown += new System.Windows.Forms.KeyEventHandler(cboHeader_KeyDown);
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val9, "appearance11");
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
		resources.ApplyResources(this.chkForAllItems, "chkForAllItems");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance12");
		((UltraToggleEditorBase)this.chkForAllItems).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkForAllItems).Name = "chkForAllItems";
		((UltraToggleEditorBase)this.chkForAllItems).CheckedChanged += new System.EventHandler(chkForAllItems_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllItems);
		base.Name = "frmItemsBatches";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForAllItems, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
