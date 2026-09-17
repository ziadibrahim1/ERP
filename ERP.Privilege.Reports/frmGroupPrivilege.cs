using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Privilege.Reports;

public class frmGroupPrivilege : frmReportTree2010
{
	private IContainer components = null;

	public frmGroupPrivilege()
	{
		InitializeComponent();
		int num = -1;
		if (GlobalVariables.UserID != "1")
		{
			num = Convert.ToInt32(Groups.Select(GlobalVariables.GroupID, "-1", "1", IsFromServer: true).Rows[0]["GroupLevel"]);
		}
		dtItems2 = Groups.SelectByLevel(num.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeItems2IDCol = "GroupID";
		TreeItems2NameCol = (GlobalVariables.IsArabic ? "GroupNameAr" : "GroupNameEn");
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "صلاحيات المجموعات" : "Groups Privileges");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		((TextEditorControlBase)cboReportType).Clear();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		cboReportType.SelectedIndex = 0;
	}

	public override void FillData()
	{
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
	}

	public override string GetReportName()
	{
		if (cboReportType.SelectedIndex > -1 && dtReports != null)
		{
			return GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString();
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار مجموعه ليتم عرض بياناته ", "There is no choosen Group to be shown in the report, please check Group to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@GroupIDs", Items2);
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
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmGroupPrivilege";
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
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
