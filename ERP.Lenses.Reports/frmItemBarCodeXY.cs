using System;
using System.ComponentModel;
using System.Windows.Forms;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Lenses.Reports;

public class frmItemBarCodeXY : frmReportTree2010
{
	private IContainer components = null;

	public UltraLabel lblSph;

	public UltraComboEditor cboSph;

	public UltraComboEditor cboCyl;

	public UltraLabel ultraLabel3;

	public frmItemBarCodeXY()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "بار كود الأصناف" : "Item Barcode");
		cboReportType.SelectedIndex = 0;
		cboCyl.SelectedIndex = 0;
		cboSph.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsBarCodeXY_A.rpt" : "Rep_SC_ItemsBarCodeXY_E.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no choosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (cboSph.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار SPH +/-  ", "There is no choosen SPH +/-");
			((Control)(object)cboSph).Select();
			return;
		}
		if (cboCyl.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار Cyl +/-  ", "There is no choosen Cyl +/-");
			((Control)(object)cboCyl).Select();
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@PositiveX", ((Control)(object)cboSph).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@PositiveY", ((Control)(object)cboCyl).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmReporViwer2.Tag = base.Tag;
		frmReporViwer2.MdiParent = base.MdiParent;
		frmReporViwer2.TopLevel = false;
		frmReporViwer2.Parent = base.Parent;
		frmReporViwer2.Width = base.Parent.Width;
		frmReporViwer2.Height = base.Parent.Height;
		frmReporViwer2.frmParent = this;
		frmReporViwer2.Show();
		frmReporViwer2.BringToFront();
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmItemBarCodeXY));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		ValueListItem val6 = new ValueListItem();
		ValueListItem val7 = new ValueListItem();
		ValueListItem val8 = new ValueListItem();
		ValueListItem val9 = new ValueListItem();
		this.lblSph = new UltraLabel();
		this.cboSph = new UltraComboEditor();
		this.cboCyl = new UltraComboEditor();
		this.ultraLabel3 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSph).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCyl).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val, "appearance1");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2022, 8, 4, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2022, 8, 4, 0, 0, 0, 0);
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		resources.ApplyResources(val2, "appearance2");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboReportType, "cboReportType");
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.lblSph, "lblSph");
		this.lblSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSph).Name = "lblSph";
		resources.ApplyResources(this.cboSph, "cboSph");
		this.cboSph.AutoCompleteMode = (AutoCompleteMode)2;
		val6.CheckState = System.Windows.Forms.CheckState.Checked;
		val6.DataValue = "0";
		resources.ApplyResources(val6, "valueListItem3");
		((SubObjectBase)val6).ForceApplyResources = "";
		val7.DataValue = "1";
		resources.ApplyResources(val7, "valueListItem4");
		((SubObjectBase)val7).ForceApplyResources = "";
		this.cboSph.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val6, val7 });
		((System.Windows.Forms.Control)(object)this.cboSph).Name = "cboSph";
		((TextEditorControlBase)this.cboSph).Nullable = false;
		resources.ApplyResources(this.cboCyl, "cboCyl");
		this.cboCyl.AutoCompleteMode = (AutoCompleteMode)2;
		val8.CheckState = System.Windows.Forms.CheckState.Checked;
		val8.DataValue = "0";
		resources.ApplyResources(val8, "valueListItem2");
		((SubObjectBase)val8).ForceApplyResources = "";
		val9.DataValue = "1";
		resources.ApplyResources(val9, "valueListItem1");
		((SubObjectBase)val9).ForceApplyResources = "";
		this.cboCyl.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val8, val9 });
		((System.Windows.Forms.Control)(object)this.cboCyl).Name = "cboCyl";
		((TextEditorControlBase)this.cboCyl).Nullable = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSph);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSph);
		base.Name = "frmItemBarCodeXY";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCyl, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSph).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCyl).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
