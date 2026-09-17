using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Production;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;

namespace ERP.Production.Reports;

public class frmMaterialRequirement : frmReportTree2010
{
	private DataTable dtReqItems;

	private IContainer components = null;

	public UltraGrid ULGData;

	public frmMaterialRequirement()
	{
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "ProductionRequestID";
		TreeItems2NameCol = "ProductionRequestNo";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "تحليل وتوقع مشتريات الخامات للطلبيات " : "Expecting Materials Purshase for Production Requestes");
		cboReportType.SelectedIndex = 0;
	}

	public override void FillData()
	{
		GetBranches();
		dtItems2 = ProductionRequests.FillRepTree(Branches, dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		if (dtItems2 != null)
		{
			TreeFunctions.FillTree(TreeItems2, dtItems2, TreeItems2ParentIDCol, TreeItems2IDCol, TreeItems2NameCol, TreeItems2NumberCol, TreeItems2IsMainCol);
		}
		((UltraToggleEditorBase)chkAll).Checked = false;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Pro_ProductionRequests_GroupAnalysis_A.rpt" : "Rep_Pro_ProductionRequests_GroupAnalysis_A.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Pro_ProductionRequests_GroupAnalysis_A_nologo.rpt" : "Rep_Pro_ProductionRequests_GroupAnalysis_A_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى إذن ليتم عرضه ", "There is no chosen Vouchers to be shown in the report, please check Vouchers to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		((UltraGridBase)ULGData).UpdateData();
		DataView dataView = new DataView(dtReqItems);
		dataView.RowFilter = "Selected=1";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		GlobalVariables.ReportDocument.SetParameterValue("@doc", Main.TableToXML(dataView.ToTable()));
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

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ProductionRequestsReport(-1, 0);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i]["ProductionRequestID"].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void Tree2_AfterCheck(object sender, NodeEventArgs e)
	{
		base.Tree2_AfterCheck(sender, e);
		Items2 = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems2);
		dtReqItems = ProductionRequests.GetItems(Items2, GlobalVariables.IsArabic ? "1" : "0");
		InitGrid();
	}

	public override void chkAll2_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		TreeItems2.AfterCheck -= new AfterNodeChangedEventHandler(Tree2_AfterCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll2).Checked, TreeItems2);
		Items2 = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems2);
		dtReqItems = ProductionRequests.GetItems(Items2, GlobalVariables.IsArabic ? "1" : "0");
		InitGrid();
		TreeItems2.AfterCheck += new AfterNodeChangedEventHandler(Tree2_AfterCheck);
	}

	private void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtReqItems;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الإذن" : "Request No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReqQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه" : "Req Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReqQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReqQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحده" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].Header).Caption = (GlobalVariables.IsArabic ? "اختيار" : "Select");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
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
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Reports.frmMaterialRequirement));
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
		this.ULGData = new UltraGrid();
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
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance11.FontData");
		resources.ApplyResources(val, "appearance11");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance12.FontData");
		resources.ApplyResources(val2, "appearance12");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.chkWithLogo, "chkWithLogo");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance13.FontData");
		resources.ApplyResources(val3, "appearance13");
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
		resources.ApplyResources(base.chkIsArabic, "chkIsArabic");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance14.FontData");
		resources.ApplyResources(val4, "appearance14");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance15.FontData");
		resources.ApplyResources(val5, "appearance15");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance1.FontData");
		resources.ApplyResources(val6, "appearance1");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance2.FontData");
		resources.ApplyResources(val7, "appearance2");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val7;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance3.FontData");
		resources.ApplyResources(val8, "appearance3");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance4.FontData");
		resources.ApplyResources(val9, "appearance4");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance5.FontData");
		resources.ApplyResources(val10, "appearance5");
		((SubObjectBase)val10).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance6.FontData");
		resources.ApplyResources(val11, "appearance6");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val12).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance7.FontData");
		resources.ApplyResources(val12, "appearance7");
		((SubObjectBase)val12).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val13).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance8.FontData");
		resources.ApplyResources(val13, "appearance8");
		((SubObjectBase)val13).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(((AppearanceBase)val14).FontData, "appearance9.FontData");
		resources.ApplyResources(val14, "appearance9");
		((SubObjectBase)val14).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(((AppearanceBase)val15).FontData, "appearance10.FontData");
		resources.ApplyResources(val15, "appearance10");
		((SubObjectBase)val15).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmMaterialRequirement";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
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
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
