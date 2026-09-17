using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.Reports;

public class frmRoomItemsPriceListRep : frmReportTree2010
{
	private IContainer components = null;

	public frmRoomItemsPriceListRep()
	{
		TreeItems2ParentIDCol = "ParentID";
		TreeItems2IDCol = "RoomID";
		TreeItems2NameCol = "RoomName";
		TreeItems2IsMainCol = "IsMain";
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "قائمة أسعار الصالات" : "Rooms Price List");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
	}

	public override void FillData()
	{
		dtItems2 = Rooms.SelectWithoutImage("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = false;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_RoomsItemsPriceList_A.rpt" : "Rep_POS_RoomsItemsPriceList_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_POS_RoomsItemsPriceList_A_nologo.rpt" : "Rep_POS_RoomsItemsPriceList_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صالة ليتم عرضعا ", "There is no choosen Hall to be shown in the report, please check Hall to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@RoomIDs", Items2);
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
		dtSearchResult = SearchFunctions.RoomsReport(Branches, IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i]["RoomID"].ToString()).CheckedState = CheckState.Checked;
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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
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
		base.dtpFromDate.DateTime = new System.DateTime(2013, 9, 12, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		((System.Windows.Forms.Control)(object)base.dtpFromDate).Size = new System.Drawing.Size(120, 24);
		base.dtpFromDate.Value = new System.DateTime(2013, 9, 12, 0, 0, 0, 0);
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
		((System.Windows.Forms.Control)(object)base.chkAll).Location = new System.Drawing.Point(42, 120);
		((System.Windows.Forms.Control)(object)base.chkAll).Size = new System.Drawing.Size(49, 20);
		((System.Windows.Forms.Control)(object)base.chkAll).Visible = false;
		((System.Windows.Forms.Control)(object)base.chkAll2).Location = new System.Drawing.Point(476, 121);
		((System.Windows.Forms.Control)(object)base.chkAll2).Size = new System.Drawing.Size(49, 20);
		((System.Windows.Forms.Control)(object)base.TreeItems).Location = new System.Drawing.Point(12, 146);
		((System.Windows.Forms.Control)(object)base.TreeItems).Size = new System.Drawing.Size(134, 277);
		((System.Windows.Forms.Control)(object)base.TreeItems).Visible = false;
		((System.Windows.Forms.Control)(object)base.TreeItems2).Location = new System.Drawing.Point(255, 146);
		((System.Windows.Forms.Control)(object)base.TreeItems2).Size = new System.Drawing.Size(489, 278);
		((System.Windows.Forms.Control)(object)base.btnItemsSearch).Location = new System.Drawing.Point(119, 427);
		((System.Windows.Forms.Control)(object)base.btnItemsSearch).Visible = false;
		((System.Windows.Forms.Control)(object)base.btnItems2Search).Location = new System.Drawing.Point(717, 428);
		((System.Windows.Forms.Control)(object)base.chkIsArabic).Size = new System.Drawing.Size(65, 20);
		((AppearanceBase)val4).FontData.Name = "Tahoma";
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val4).TextHAlignAsString = "Center";
		((AppearanceBase)val4).TextVAlignAsString = "Middle";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)base.txtItems).Location = new System.Drawing.Point(12, 429);
		((System.Windows.Forms.Control)(object)base.txtItems).Size = new System.Drawing.Size(101, 25);
		((System.Windows.Forms.Control)(object)base.txtItems).Visible = false;
		((AppearanceBase)val5).FontData.Name = "Tahoma";
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val5).TextHAlignAsString = "Center";
		((AppearanceBase)val5).TextVAlignAsString = "Middle";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)base.txtItems2).Location = new System.Drawing.Point(255, 429);
		((System.Windows.Forms.Control)(object)base.txtItems2).Size = new System.Drawing.Size(460, 25);
		((AppearanceBase)val6).FontData.BoldAsString = "True";
		((AppearanceBase)val6).FontData.Name = "Arial";
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboSetting).Appearance = (AppearanceBase)(object)val6;
		base.cboSetting.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboSetting.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboSetting).Size = new System.Drawing.Size(139, 24);
		((System.Windows.Forms.Control)(object)base.lblSettingName).Size = new System.Drawing.Size(51, 17);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmRoomItemsPriceListRep";
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
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
