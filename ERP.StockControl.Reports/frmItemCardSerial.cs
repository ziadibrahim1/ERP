using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.Reports;

public class frmItemCardSerial : frmReportTree2010
{
	private DataTable dtStoreItems;

	private DataTable dtBatchs;

	private IContainer components = null;

	public UltraButton btnItemSearch;

	private UltraLabel lblItem;

	private UltraComboEditor cboItem;

	private UltraComboEditor cboItemCode;

	public frmItemCardSerial()
	{
		dtBatchs = ItemsBatches.FillCombo(IsFromServer: true);
		InitializeComponent();
		dtItems = Stores.Select("-1", ViewAllBranches ? "-1" : GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeItemsIDCol = "StoreID";
		TreeItemsNameCol = (GlobalVariables.IsArabic ? "StoreNameAr" : "StoreNameEn");
		TreeItems2IDCol = "BatchID";
		TreeItems2NameCol = "BatchNo";
		dtStoreItems = BusinessLayer.StockControl.Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboItem, dtStoreItems, "ItemID", "Name");
		GlobalFunctions.FillCombo(cboItemCode, dtStoreItems, "ItemID", "ItemBarCode");
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعه التفاصيل" : "Print With All Details ");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		if (dtItems != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems, dtItems, TreeItemsIDCol, TreeItemsNameCol);
		}
		((UltraToggleEditorBase)chkAll).Checked = true;
	}

	public override void FillData()
	{
		dtItems2 = ItemsBatches.SelectByItemID((cboItem != null && cboItem.SelectedIndex > -1) ? ((TextEditorControlBase)cboItem).Value.ToString() : "0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsCard_BySerialNo_A.rpt" : "Rep_SC_ItemsCard_BySerialNo_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsCard_BySerialNo_A_nologo.rpt" : "Rep_SC_ItemsCard_BySerialNo_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى سريل ليتم عرضه ", "There is no choosen Serial to be shown in the report, please check items to be shown in report");
			return;
		}
		if (cboItem.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف ليتم عرضه ", "There is no choosen Item to be shown in the report, please Select items to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemID", ((TextEditorControlBase)cboItem).Value);
		GlobalVariables.ReportDocument.SetParameterValue("@StoreIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@BatchIDs", Items2);
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
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeItems.Nodes).Count; i++)
		{
			if (TreeItems.Nodes[i].CheckedState == CheckState.Checked)
			{
				Branches = Branches + dtItems.Rows[i]["BranchID"].ToString() + ",";
				if (dtItems.Rows[i]["BranchID"].ToString() != GlobalVariables.CurrentBranchID && Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
				{
					Online = true;
				}
			}
		}
	}

	private void cboItem_ValueChanged(object sender, EventArgs e)
	{
		if (cboItem.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboItemCode).ValueChanged -= cboItemCode_ValueChanged;
			((TextEditorControlBase)cboItemCode).Value = ((TextEditorControlBase)cboItem).Value;
			if (!IsLoading)
			{
				FillData();
			}
			((TextEditorControlBase)cboItemCode).ValueChanged += cboItemCode_ValueChanged;
		}
	}

	private void cboItemCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboItemCode.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboItem).ValueChanged -= cboItem_ValueChanged;
			((TextEditorControlBase)cboItem).Value = ((TextEditorControlBase)cboItemCode).Value;
			if (!IsLoading)
			{
				FillData();
			}
			((TextEditorControlBase)cboItem).ValueChanged += cboItem_ValueChanged;
		}
	}

	private void btnItemSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboItem).ValueChanged -= cboItem_ValueChanged;
		((TextEditorControlBase)cboItemCode).ValueChanged -= cboItemCode_ValueChanged;
		int num = SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "1", "-1", "0", IsFromServer: true);
		if (num != 0)
		{
			UltraComboEditor obj = cboItem;
			object value = (((TextEditorControlBase)cboItemCode).Value = num);
			((TextEditorControlBase)obj).Value = value;
			if (!IsLoading)
			{
				FillData();
			}
		}
		((TextEditorControlBase)cboItemCode).ValueChanged += cboItemCode_ValueChanged;
		((TextEditorControlBase)cboItem).ValueChanged += cboItem_ValueChanged;
	}

	private void txtItems2_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		DataRow[] array = dtBatchs.Select("BatchName ='" + ((Control)(object)txtItems2).Text + "'");
		if (array.Length == 1)
		{
			int num = int.Parse(array[0]["ItemID"].ToString());
			((TextEditorControlBase)cboItem).ValueChanged -= cboItem_ValueChanged;
			UltraComboEditor obj = cboItem;
			object value = (((TextEditorControlBase)cboItemCode).Value = num);
			((TextEditorControlBase)obj).Value = value;
			((TextEditorControlBase)cboItem).ValueChanged += cboItem_ValueChanged;
			DataView dataView = new DataView(dtItems2);
			dataView.RowFilter = TreeItems2NameCol + " Like '%" + ((Control)(object)txtItems2).Text.Trim() + "%' ";
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			if (dataView.ToTable().Rows.Count > 0)
			{
				TreeItems2.Override.ActiveNodeAppearance.BackColor = Color.Gray;
				TreeItems2.ActiveNode = TreeItems2.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems2IDCol].ToString());
			}
			TreeItems2.ActiveNode.CheckedState = CheckState.Checked;
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmItemCardSerial));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.btnItemSearch = new UltraButton();
		this.lblItem = new UltraLabel();
		this.cboItem = new UltraComboEditor();
		this.cboItemCode = new UltraComboEditor();
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
		((System.ComponentModel.ISupportInitialize)this.cboItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnPreview, "btnPreview");
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance7");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2022, 8, 14, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2022, 8, 14, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance8");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 8, 8, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 8, 8, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		resources.ApplyResources(base.chkWithLogo, "chkWithLogo");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(base.cboReportType, "cboReportType");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance3");
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		resources.ApplyResources(base.chkIsArabic, "chkIsArabic");
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		((AppearanceBase)val5).FontData.BoldAsString = resources.GetString("resource.BoldAsString4");
		((AppearanceBase)val5).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString4");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name4");
		((AppearanceBase)val5).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString4");
		((AppearanceBase)val5).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)base.txtItems2).KeyUp += new System.Windows.Forms.KeyEventHandler(txtItems2_KeyUp);
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnNew, "btnNew");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.cboSetting, "cboSetting");
		resources.ApplyResources(base.lblSettingName, "lblSettingName");
		resources.ApplyResources(base.btnSaveSetting, "btnSaveSetting");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnItemSearch, "btnItemSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance9");
		((ControlBase)this.btnItemSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnItemSearch).Name = "btnItemSearch";
		((System.Windows.Forms.Control)(object)this.btnItemSearch).Click += new System.EventHandler(btnItemSearch_Click);
		resources.ApplyResources(this.lblItem, "lblItem");
		this.lblItem.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItem).Name = "lblItem";
		((ControlBase)this.lblItem).WrapText = false;
		resources.ApplyResources(this.cboItem, "cboItem");
		((TextEditorControlBase)this.cboItem).AlwaysInEditMode = true;
		this.cboItem.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItem).Name = "cboItem";
		((TextEditorControlBase)this.cboItem).ValueChanged += new System.EventHandler(cboItem_ValueChanged);
		resources.ApplyResources(this.cboItemCode, "cboItemCode");
		((TextEditorControlBase)this.cboItemCode).AlwaysInEditMode = true;
		this.cboItemCode.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItemCode).Name = "cboItemCode";
		((TextEditorControlBase)this.cboItemCode).ValueChanged += new System.EventHandler(cboItemCode_ValueChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItem);
		base.Name = "frmItemCardSerial";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemSearch, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
