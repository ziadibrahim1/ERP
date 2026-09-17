using System;
using System.ComponentModel;
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

namespace ERP.StockControl.Reports;

public class frmItemInActive : frmReportTree2010
{
	private IContainer components = null;

	private UltraLabel lblQty;

	private UltraTextEditor txtQty;

	public frmItemInActive()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = Stores.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "StoreID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "StoreNameAr" : "StoreNameEn");
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "الاصناف الراكدة" : "Items inActive");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = true;
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsInactive_A.rpt" : "Rep_SC_ItemsInactive_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsInactive_A_nologo.rpt" : "Rep_SC_ItemsInactive_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no choosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مخزن  ", "There is no choosen Store to be shown in the report, please check items to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@StoreIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@MinQty", (((Control)(object)txtQty).Text == "") ? "0" : ((Control)(object)txtQty).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
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

	public override void GetBranches()
	{
		Online = false;
		Branches = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeItems2.Nodes).Count; i++)
		{
			if (TreeItems2.Nodes[i].CheckedState == CheckState.Checked)
			{
				Branches = Branches + dtItems2.Rows[i]["BranchID"].ToString() + ",";
				if (dtItems2.Rows[i]["BranchID"].ToString() != GlobalVariables.CurrentBranchID && Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
				{
					Online = true;
				}
			}
		}
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.StoresReport(IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmItemInActive));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.lblQty = new UltraLabel();
		this.txtQty = new UltraTextEditor();
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
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val, "appearance1");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2013, 9, 4, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2013, 9, 4, 0, 0, 0, 0);
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		resources.ApplyResources(val2, "appearance2");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
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
		((AppearanceBase)val6).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val6).FontData.Name = resources.GetString("resource.Name5");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboSetting).Appearance = (AppearanceBase)(object)val6;
		base.cboSetting.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboSetting.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.lblSettingName, "lblSettingName");
		this.lblQty.AutoEllipsis = false;
		resources.ApplyResources(this.lblQty, "lblQty");
		((System.Windows.Forms.Control)(object)this.lblQty).Name = "lblQty";
		resources.ApplyResources(this.txtQty, "txtQty");
		((System.Windows.Forms.Control)(object)this.txtQty).Name = "txtQty";
		((System.Windows.Forms.Control)(object)this.txtQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtQty_KeyPress);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQty);
		base.Name = "frmItemInActive";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQty, 0);
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
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
