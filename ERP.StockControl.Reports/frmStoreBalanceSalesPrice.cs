using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.Reports;

public class frmStoreBalanceSalesPrice : frmReportTree2010
{
	private DataTable dtPriceTypes;

	private IContainer components = null;

	public UltraComboEditor cboPriceType;

	public UltraLabel lblPriceType;

	public UltraTextEditor txtProfitFrom;

	public UltraTextEditor txtProfitTo;

	public UltraLabel lblProfitFrom;

	public UltraLabel lblProfitTo;

	private UltraCheckEditor chkProfit;

	public frmStoreBalanceSalesPrice()
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
		dtPriceTypes = PricesTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceTypes, "PriceTypeID", GlobalVariables.IsArabic ? "PriceNameAr" : "PriceNameEn");
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "تفصيلي كل صنف" : "Detailed For Each Item");
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
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsBalance_BySalesPrice_A.rpt" : "Rep_SC_ItemsBalance_BySalesPrice_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_ItemsBalance_BySalesPrice_A_nologo.rpt" : "Rep_SC_ItemsBalance_BySalesPrice_E_nologo.rpt");
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
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no chosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى مخزن  ", "There is no chosen Store to be shown in the report, please check Stores to be shown in report");
			return;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار نوع السعر  ", "There is no chosen PriceType to be shown in the report, please select Price Type to be shown in report");
			return;
		}
		GlobalVariables.IsRepOnlineConn = (Online = Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@StoreIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", ((UltraToggleEditorBase)chkAll).Checked ? "-1" : Items);
		GlobalVariables.ReportDocument.SetParameterValue("@PriceTypeID", ((TextEditorControlBase)cboPriceType).Value);
		GlobalVariables.ReportDocument.SetParameterValue("@Date", dtpToDate.DateTime);
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Profit"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ProfitFrom", ((UltraToggleEditorBase)chkProfit).Checked ? ((Control)(object)txtProfitFrom).Text : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@ProfitTo", ((UltraToggleEditorBase)chkProfit).Checked ? ((Control)(object)txtProfitTo).Text : "0");
		}
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

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
	}

	public void RefreshReport(frmReporViwer frmViewer)
	{
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

	private void frmStoreBalanceSalesPrice_Load(object sender, EventArgs e)
	{
		if (base.DesignMode)
		{
			return;
		}
		int num = Convert.ToInt32(((DataRow)base.Tag)["PeriodDays"]);
		DateTime dateTime = GlobalFunctions.GetServerDateTimeNow().AddDays(-num);
		if (!GlobalVariables.SeeingClosedYears)
		{
			if (num == 0 || dateTime < GlobalVariables.MinOpenedDate)
			{
				dtpToDate.MinDate = GlobalVariables.MinOpenedDate;
			}
			else
			{
				dtpToDate.MinDate = dateTime;
			}
		}
		else if (num > 0)
		{
			dtpToDate.MinDate = dateTime;
		}
	}

	private void chkProfit_CheckedChanged(object sender, EventArgs e)
	{
		UltraTextEditor obj = txtProfitFrom;
		bool enabled = (((Control)(object)txtProfitTo).Enabled = ((UltraToggleEditorBase)chkProfit).Checked);
		((Control)(object)obj).Enabled = enabled;
	}

	private void txtProfit_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbersNegative(sender, e);
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (dtReports != null && dtReports.Rows.Count > 0 && cboReportType.SelectedIndex > -1)
		{
			((Control)(object)btnUpdate).Enabled = GlobalVariables.UserID == "1" || !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			((Control)(object)btnDelete).Enabled = !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			UltraLabel obj = lblProfitFrom;
			UltraLabel obj2 = lblProfitTo;
			UltraTextEditor obj3 = txtProfitFrom;
			UltraTextEditor obj4 = txtProfitTo;
			bool flag = (((Control)(object)chkProfit).Visible = cboReportType.SelectedIndex > -1 && dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Profit"));
			bool flag3 = (((Control)(object)obj4).Visible = flag);
			bool flag5 = (((Control)(object)obj3).Visible = flag3);
			bool visible = (((Control)(object)obj2).Visible = flag5);
			((Control)(object)obj).Visible = visible;
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
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmStoreBalanceSalesPrice));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.txtProfitFrom = new UltraTextEditor();
		this.txtProfitTo = new UltraTextEditor();
		this.lblProfitFrom = new UltraLabel();
		this.lblProfitTo = new UltraLabel();
		this.chkProfit = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfitFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfitTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkProfit).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.ultraLabel2).UseWaitCursor = true;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2015, 2, 4, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2015, 2, 4, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboReportType, "cboReportType");
		((TextEditorControlBase)base.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
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
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((TextEditorControlBase)this.cboPriceType).Nullable = false;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPriceType).Appearance = (AppearanceBase)(object)val6;
		this.lblPriceType.AutoEllipsis = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.txtProfitFrom, "txtProfitFrom");
		((System.Windows.Forms.Control)(object)this.txtProfitFrom).Name = "txtProfitFrom";
		((System.Windows.Forms.Control)(object)this.txtProfitFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtProfit_KeyPress);
		resources.ApplyResources(this.txtProfitTo, "txtProfitTo");
		((System.Windows.Forms.Control)(object)this.txtProfitTo).Name = "txtProfitTo";
		((System.Windows.Forms.Control)(object)this.txtProfitTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtProfit_KeyPress);
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblProfitFrom).Appearance = (AppearanceBase)(object)val7;
		this.lblProfitFrom.AutoEllipsis = false;
		resources.ApplyResources(this.lblProfitFrom, "lblProfitFrom");
		((System.Windows.Forms.Control)(object)this.lblProfitFrom).Name = "lblProfitFrom";
		((ControlBase)this.lblProfitFrom).WrapText = false;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblProfitTo).Appearance = (AppearanceBase)(object)val8;
		this.lblProfitTo.AutoEllipsis = false;
		resources.ApplyResources(this.lblProfitTo, "lblProfitTo");
		((System.Windows.Forms.Control)(object)this.lblProfitTo).Name = "lblProfitTo";
		((ControlBase)this.lblProfitTo).WrapText = false;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkProfit).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.chkProfit, "chkProfit");
		((System.Windows.Forms.Control)(object)this.chkProfit).Name = "chkProfit";
		((UltraToggleEditorBase)this.chkProfit).CheckedChanged += new System.EventHandler(chkProfit_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkProfit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtProfitTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtProfitFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProfitTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProfitFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Name = "frmStoreBalanceSalesPrice";
		base.Load += new System.EventHandler(frmStoreBalanceSalesPrice_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProfitFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProfitTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtProfitFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtProfitTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkProfit, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfitFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtProfitTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkProfit).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
