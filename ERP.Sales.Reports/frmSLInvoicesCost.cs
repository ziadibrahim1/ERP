using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.Sales;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.Sales.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Sales.Reports;

public class frmSLInvoicesCost : frmReportTree2010
{
	public DataTable dtItems3;

	public string Items3;

	public string TreeItems3ParentIDCol = "ParentID";

	public string TreeItems3IDCol = "ItemID";

	public string TreeItems3NameCol = "Name";

	public string TreeItems3NumberCol = "";

	public string TreeItems3IsMainCol = "IsMain";

	private IContainer components = null;

	public UltraButton btnItems3Search;

	public UltraTextEditor txtItems3;

	public UltraTree TreeItems3;

	protected internal UltraCheckEditor chkAllItems;

	public frmSLInvoicesCost()
	{
		dtItems = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "SubAccountID";
		TreeItemsNameCol = "SubAccountName";
		TreeItemsNumberCol = "ClientSupplierNo";
		TreeItemsIsMainCol = "IsMain";
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "SLInvoiceID";
		TreeItems2NameCol = "SLInvoiceNo";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		dtItems3 = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "0", "-1", "-1", "0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		cboReportType.Items.Clear();
	}

	public override void FormLoad()
	{
		base.FormLoad();
		if (dtItems3 != null)
		{
			TreeFunctions.FillTree(TreeItems3, dtItems3, TreeItems3ParentIDCol, TreeItems3IDCol, TreeItems3NameCol, TreeItems3NumberCol, TreeItems3IsMainCol);
		}
		((UltraToggleEditorBase)chkAllItems).Checked = true;
		((UltraToggleEditorBase)chkAll).Checked = true;
	}

	public override void FillData()
	{
		GetBranches();
		dtItems2 = SLInvoices.FillRepTree(Branches, "-1", dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		if (dtItems2 != null)
		{
			TreeFunctions.FillTree(TreeItems2, dtItems2, TreeItems2ParentIDCol, TreeItems2IDCol, TreeItems2NameCol, TreeItems2NumberCol, TreeItems2IsMainCol);
		}
	}

	public override void ShowReport()
	{
		GetItems();
		Items3 = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems3);
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items3.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف ليتم عرضه ", "There is no choosen Item to be shown in the report, please check Vouchers to be shown in report");
			return;
		}
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى عميل ليتم عرضه ", "There is no choosen Client to be shown in the report, please check Vouchers to be shown in report");
			return;
		}
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى فاتوره ليتم عرضها ", "There is no choosen invoice to be shown in the report, please check Vouchers to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@SLInvoiceIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items3);
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
		if (GroupNamePath != "")
		{
			string text = GroupNamePath.Substring(0, GroupNamePath.IndexOf("SLInvoiceID") + 8);
			if (text.IndexOf("SLInvoiceID") >= 0)
			{
				string s = GroupNamePath.Replace(text, "").Replace(",", "").Replace("[", "")
					.Replace("]", "");
				frmSLInvoices frmSLInvoices3 = new frmSLInvoices(int.Parse(s));
				frmSLInvoices3.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmSLInvoices3.lblTitle).Text = (GlobalVariables.IsArabic ? "فاتورة المبيعات" : "Salse Invoice");
				frmSLInvoices3.ShowDialog();
				RefreshReport(frmViewer);
			}
		}
	}

	public void RefreshReport(frmReporViwer frmViewer)
	{
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		int currentPageNumber = frmViewer.crvReportViewer.GetCurrentPageNumber();
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@SLInvoiceIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("Tax"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@SLInvoiceIDs", "," + RowID + ",", "Tax name");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Tax name");
		}
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLInvoicesReport(-1, 0, -1, -1, -1);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		base.Tree_AfterCheck(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	public override void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		base.chkAll_CheckedChanged(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void btnItems3Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems3.GetNodeByKey(dtSearchResult.Rows[i][TreeItems3IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void chkAllItems3_CheckedChanged(object sender, EventArgs e)
	{
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllItems).Checked, TreeItems3);
	}

	private void treeItems3_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		TreeItems3.AfterCheck -= new AfterNodeChangedEventHandler(treeItems3_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			SetParentState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllItems).CheckedChanged -= chkAllItems3_CheckedChanged;
		SetCheckBoxAllState(TreeItems3, chkAllItems);
		((UltraToggleEditorBase)chkAllItems).CheckedChanged += chkAllItems3_CheckedChanged;
		TreeItems3.AfterCheck += new AfterNodeChangedEventHandler(treeItems3_AfterCheck);
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void txtItems3_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems3);
		dataView.RowFilter = TreeItems3NameCol + " Like '%" + ((Control)(object)txtItems3).Text.Trim() + "%' " + ((TreeItems3NumberCol != null && TreeItems3NumberCol != "") ? (" OR " + TreeItems3NumberCol + " Like '" + ((Control)(object)txtItems3).Text.Trim() + "%' ") : "");
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems3.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems3.ActiveNode = TreeItems3.GetNodeByKey(dataView.ToTable().Rows[0][TreeItems3IDCol].ToString());
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
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sales.Reports.frmSLInvoicesCost));
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		this.btnItems3Search = new UltraButton();
		this.txtItems3 = new UltraTextEditor();
		this.TreeItems3 = new UltraTree();
		this.chkAllItems = new UltraCheckEditor();
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
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItems).BeginInit();
		base.SuspendLayout();
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2017, 1, 31, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2017, 1, 31, 0, 0, 0, 0);
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance1.FontData");
		resources.ApplyResources(val3, "appearance1");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
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
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance6.FontData");
		resources.ApplyResources(val4, "appearance6");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.btnItems3Search, "btnItems3Search");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance4");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance4.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnItems3Search).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnItems3Search).Name = "btnItems3Search";
		((System.Windows.Forms.Control)(object)this.btnItems3Search).Click += new System.EventHandler(btnItems3Search_Click);
		resources.ApplyResources(this.txtItems3, "txtItems3");
		((System.Windows.Forms.Control)(object)this.txtItems3).Name = "txtItems3";
		((TextEditorControlBase)this.txtItems3).ValueChanged += new System.EventHandler(txtItems3_ValueChanged);
		resources.ApplyResources(this.TreeItems3, "TreeItems3");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance11.FontData");
		resources.ApplyResources(val7, "appearance11");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.TreeItems3.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeItems3).Name = "TreeItems3";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeItems3.Override = val8;
		((UltraControlBase)this.TreeItems3).UseAppStyling = false;
		this.TreeItems3.AfterCheck += new AfterNodeChangedEventHandler(treeItems3_AfterCheck);
		resources.ApplyResources(this.chkAllItems, "chkAllItems");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance10.FontData");
		resources.ApplyResources(val9, "appearance10");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraToggleEditorBase)this.chkAllItems).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAllItems).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllItems).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllItems).Name = "chkAllItems";
		((UltraControlBase)this.chkAllItems).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllItems).CheckedChanged += new System.EventHandler(chkAllItems3_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItems3Search);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllItems);
		base.Name = "frmSLInvoicesCost";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItems3Search, 0);
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
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
