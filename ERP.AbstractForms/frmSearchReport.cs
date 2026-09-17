using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.AbstractForms;

public class frmSearchReport : frmSearch
{
	public DataTable dtFormSetting = new DataTable();

	public int Approved = -1;

	public int Deleted = -1;

	private IContainer components = null;

	public UltraComboEditor cboSetting;

	public UltraLabel ultraLabel3;

	public UltraButton btnSaveSetting;

	public frmSearchReport()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		if (dtSource != null)
		{
			dtSource.Columns.Add("Choose", typeof(bool));
			dtSource.Columns["Choose"].DefaultValue = 0;
		}
		if (!base.DesignMode)
		{
			dtFormSetting = SearchSetting.FillCombo(IDColumn, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSetting, dtFormSetting, "SearchSettingID", "SettingName");
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Header).Caption = "";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Choose"].Index : 0);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Choose"].Value == DBNull.Value)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Choose"].Value = false;
			}
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Choose")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void SelectRow(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && (sender is UltraGrid || ((DataView)((UltraGridBase)ULGData).DataSource).ToTable().Select("Choose = 1").Length == 0))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["Choose"].Value = 1;
		}
		((UltraGridBase)ULGData).UpdateData();
		((DataView)((UltraGridBase)ULGData).DataSource).RowFilter = "Choose = 1";
		((DataView)((UltraGridBase)ULGData).DataSource).RowStateFilter = DataViewRowState.CurrentRows;
		dtResult = ((DataView)((UltraGridBase)ULGData).DataSource).ToTable();
		Close();
	}

	public override void FilterDataSource(string Filter)
	{
		base.FilterDataSource(Filter + " or Choose=1 ");
	}

	private void frmSearchReport_FormClosing(object sender, FormClosingEventArgs e)
	{
		dtSource = null;
		dv = null;
	}

	private void btnSaveSetting_Click(object sender, EventArgs e)
	{
		SaveSetting();
	}

	public virtual void SaveSetting()
	{
		frmSearchSettingName frmSearchSettingName2 = new frmSearchSettingName((cboSetting.SelectedIndex > -1) ? ((TextEditorControlBase)cboSetting).Value.ToString() : "0", dtFormSetting, IDColumn);
		frmSearchSettingName2.ShowDialog();
		dtFormSetting = SearchSetting.FillCombo(IDColumn, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSetting, dtFormSetting, "SearchSettingID", "SettingName");
		((UltraGridBase)ULGData).UpdateData();
		((DataView)((UltraGridBase)ULGData).DataSource).RowFilter = "Choose = 1";
		((DataView)((UltraGridBase)ULGData).DataSource).RowStateFilter = DataViewRowState.CurrentRows;
		dtResult = ((DataView)((UltraGridBase)ULGData).DataSource).ToTable();
		int settingID = frmSearchSettingName2.SettingID;
		SearchSettingDetails.DeleteBySearchSettingID(settingID.ToString(), GlobalVariables.UserID, IsFromServer: false);
		SearchSettingDetails.Insert_ByTable(dtResult, IDColumn, settingID.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
	}

	private void cboSetting_ValueChanged(object sender, EventArgs e)
	{
		if (cboSetting.SelectedIndex > -1)
		{
			DataTable dataTable = SearchSettingDetails.SelectBySearchSettingID(((TextEditorControlBase)cboSetting).Value.ToString(), "0", IsFromServer: false);
			for (int i = 0; i < dtSource.Rows.Count; i++)
			{
				dtSource.Rows[i]["Choose"] = ((dataTable.Select("ItemID=" + dtSource.Rows[i][IDColumn]).Length != 0) ? 1 : 0);
			}
		}
		FilterDataSource("1=1" + MultiFilterString() + " or Choose=1 ");
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmSearchReport));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		this.cboSetting = new UltraComboEditor();
		this.ultraLabel3 = new UltraLabel();
		this.btnSaveSetting = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtSource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtResult).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkSearchMultiFilter).BeginInit();
		((System.Windows.Forms.Control)(object)base.pnlControls).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSetting).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.ULGData, "ULGData");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Appearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorNumberStyle = (RowSelectorNumberStyle)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorWidth = 30;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.chkSearchMultiFilter, "chkSearchMultiFilter");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val4).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((UltraToggleEditorBase)base.chkSearchMultiFilter).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)base.chkSearchMultiFilter).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)base.chkSearchMultiFilter).BackColorInternal = System.Drawing.Color.Transparent;
		((UltraControlBase)base.chkSearchMultiFilter).UseAppStyling = false;
		resources.ApplyResources(base.btnEditColumns, "btnEditColumns");
		resources.ApplyResources(base.pnlControls, "pnlControls");
		resources.ApplyResources(base.pnlControls.ClientArea, "pnlControls.ClientArea");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.cboSetting, "cboSetting");
		this.cboSetting.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSetting).Name = "cboSetting";
		((TextEditorControlBase)this.cboSetting).Nullable = false;
		((TextEditorControlBase)this.cboSetting).ValueChanged += new System.EventHandler(cboSetting_ValueChanged);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		resources.ApplyResources(this.btnSaveSetting, "btnSaveSetting");
		((System.Windows.Forms.Control)(object)this.btnSaveSetting).Name = "btnSaveSetting";
		((System.Windows.Forms.Control)(object)this.btnSaveSetting).Click += new System.EventHandler(btnSaveSetting_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSetting);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveSetting);
		base.Name = "frmSearchReport";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmSearchReport_FormClosing);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.pnlControls, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkSearchMultiFilter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex(base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnEditColumns, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtSource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtResult).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkSearchMultiFilter).EndInit();
		((System.Windows.Forms.Control)(object)base.pnlControls).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSetting).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
