using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Lenses.Reports;

public class frmClientsData : frmReportTree2010
{
	private IContainer components = null;

	public frmClientsData()
	{
		InitializeComponent();
		dtItems2 = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "SubAccountID";
		TreeItems2NameCol = "SubAccountName";
		TreeItems2NumberCol = "SubAccountNumber";
		TreeItems2IsMainCol = "IsMain";
	}

	public override void FormLoad()
	{
		base.FormLoad();
		if (dtItems2 != null)
		{
			TreeFunctions.FillTree(TreeItems2, dtItems2, TreeItems2ParentIDCol, TreeItems2IDCol, TreeItems2NameCol, TreeItems2NumberCol, TreeItems2IsMainCol);
		}
	}

	public override string GetReportName()
	{
		if (((UltraToggleEditorBase)chkWithLogo).Checked)
		{
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_SubAccountsClients_A.rpt" : "Rep_A_SubAccountsClients_E.rpt");
		}
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_A_SubAccountsClients_A_nologo.rpt" : "Rep_A_SubAccountsClients_E_nologo.rpt");
	}

	public override void ShowReport()
	{
		GetItems();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار عميل ليتم عرض بياناته ", "There is no chosen Client to be shown in the report, please check Client to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		if (dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("ByDate"))
		{
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		}
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
		dtSearchResult = SearchFunctions.ClientsReport("-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (cboReportType.SelectedIndex > -1 && dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].Equals("ByDate"))
		{
			((Control)(object)ultraLabel1).Visible = true;
			((Control)(object)ultraLabel2).Visible = true;
			((Control)(object)dtpFromDate).Visible = true;
			((Control)(object)dtpToDate).Visible = true;
		}
		else
		{
			((Control)(object)ultraLabel1).Visible = false;
			((Control)(object)ultraLabel2).Visible = false;
			((Control)(object)dtpFromDate).Visible = false;
			((Control)(object)dtpToDate).Visible = false;
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
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
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
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.ultraLabel1).Size = new System.Drawing.Size(39, 17);
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.ultraLabel1).Visible = false;
		((System.Windows.Forms.Control)(object)base.ultraLabel2).Size = new System.Drawing.Size(22, 17);
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.ultraLabel2).Visible = false;
		((AppearanceBase)val).FontData.Name = "Tahoma";
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val).TextHAlignAsString = "Center";
		((AppearanceBase)val).TextVAlignAsString = "Middle";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		((System.Windows.Forms.Control)(object)base.dtpFromDate).Size = new System.Drawing.Size(120, 24);
		((System.Windows.Forms.Control)(object)base.dtpFromDate).Visible = false;
		((AppearanceBase)val2).FontData.Name = "Tahoma";
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val2).TextHAlignAsString = "Center";
		((AppearanceBase)val2).TextVAlignAsString = "Middle";
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		((System.Windows.Forms.Control)(object)base.dtpToDate).Size = new System.Drawing.Size(120, 24);
		base.dtpToDate.Value = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		((System.Windows.Forms.Control)(object)base.dtpToDate).Visible = false;
		((System.Windows.Forms.Control)(object)base.chkAllBranches).Size = new System.Drawing.Size(49, 20);
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.chkAllBranches).Visible = false;
		base.clbBranches.Visible = false;
		((System.Windows.Forms.Control)(object)base.chkWithLogo).Size = new System.Drawing.Size(89, 20);
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)base.lblReportType).Size = new System.Drawing.Size(85, 17);
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = "Tahoma";
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboReportType).Size = new System.Drawing.Size(362, 24);
		((TextEditorControlBase)base.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		((System.Windows.Forms.Control)(object)base.chkAll).Location = new System.Drawing.Point(110, 93);
		((System.Windows.Forms.Control)(object)base.chkAll).Size = new System.Drawing.Size(49, 20);
		((System.Windows.Forms.Control)(object)base.chkAll).Visible = false;
		((System.Windows.Forms.Control)(object)base.chkAll2).Location = new System.Drawing.Point(490, 95);
		((System.Windows.Forms.Control)(object)base.chkAll2).Size = new System.Drawing.Size(49, 20);
		((System.Windows.Forms.Control)(object)base.TreeItems).Size = new System.Drawing.Size(161, 303);
		((System.Windows.Forms.Control)(object)base.TreeItems).Visible = false;
		((System.Windows.Forms.Control)(object)base.TreeItems2).Location = new System.Drawing.Point(255, 120);
		((System.Windows.Forms.Control)(object)base.TreeItems2).Size = new System.Drawing.Size(489, 303);
		((System.Windows.Forms.Control)(object)base.btnItemsSearch).Location = new System.Drawing.Point(175, 429);
		((System.Windows.Forms.Control)(object)base.btnItemsSearch).Visible = false;
		((System.Windows.Forms.Control)(object)base.btnItems2Search).Location = new System.Drawing.Point(717, 428);
		((System.Windows.Forms.Control)(object)base.chkIsArabic).Size = new System.Drawing.Size(65, 20);
		((AppearanceBase)val4).FontData.Name = "Tahoma";
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val4).TextHAlignAsString = "Center";
		((AppearanceBase)val4).TextVAlignAsString = "Middle";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)base.txtItems).Size = new System.Drawing.Size(132, 25);
		((System.Windows.Forms.Control)(object)base.txtItems).Visible = false;
		((AppearanceBase)val5).FontData.Name = "Tahoma";
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val5).TextHAlignAsString = "Center";
		((AppearanceBase)val5).TextVAlignAsString = "Middle";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)base.txtItems2).Location = new System.Drawing.Point(255, 429);
		((System.Windows.Forms.Control)(object)base.txtItems2).Size = new System.Drawing.Size(460, 25);
		((System.Windows.Forms.Control)(object)base.btnNew).Visible = true;
		((System.Windows.Forms.Control)(object)base.btnUpdate).Visible = true;
		((System.Windows.Forms.Control)(object)base.btnDelete).Visible = true;
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmClientsData";
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
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
