using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.HR.Payroll.Reports;

public class frmEmployeeSalaryByMail : frmReportTree2010
{
	private IContainer components = null;

	public UltraButton btnSend;

	public frmEmployeeSalaryByMail()
	{
		dtItems = SubAccounts.FillReportTreeEmployeesByAdministrativeStructureIDs(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "SubAccountID";
		TreeItemsNameCol = "SubAccountName";
		TreeItemsNumberCol = "EmployeeNo";
		TreeItemsIsMainCol = "IsMain";
		dtItems2 = AdministrativeStructure.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		TreeItems2IDCol = "AdministrativeStructureID";
		TreeItems2NameCol = "AdministrativeStructureName";
		((UltraToggleEditorBase)chkAll2).Checked = true;
		InitializeComponent();
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
		TreeFunctions.SetAllTreeNodesCheckState(Checked: true, TreeItems2);
		((Control)(object)chkAllBranches).Visible = false;
		clbBranches.Visible = false;
	}

	public override void FillData()
	{
		GetItems();
		if (Items2 != null)
		{
			Items2 = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems2);
			if (((UltraToggleEditorBase)chkAll2).Checked)
			{
				Items2 = "-1";
			}
			dtItems = SubAccounts.FillReportTreeEmployeesByAdministrativeStructureIDs(GlobalVariables.EmployeeSubAccountTypeIDs, Items2, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
	}

	public override void ShowReport()
	{
		GetItems();
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى موظف  ", "There is no chosen Employee to be shown in the report, please check items to be shown in report");
		}
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.EmployeesReport("-1", "-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	public override void Tree2_AfterCheck(object sender, NodeEventArgs e)
	{
		base.Tree2_AfterCheck(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	public override void chkAll2_CheckedChanged(object sender, EventArgs e)
	{
		base.chkAll2_CheckedChanged(sender, e);
		if (!IsLoading)
		{
			FillData();
		}
	}

	private void btnSend_Click(object sender, EventArgs e)
	{
		GetItems();
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى موظف  ", "There is no chosen Employee to be shown in the report, please check items to be shown in report");
			return;
		}
		DataTable dataTable = Main.ExecuteQuery_DataTable((GlobalVariables.IsArabic ? " Rep_HR_EmployeesSalaryHistoryPrintArEmail '" : " Rep_HR_EmployeesSalaryHistoryPrintEnEmail '") + Items + "','" + dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "','" + dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate) + "'," + (GlobalVariables.IsArabic ? "1" : "0"));
		if (dataTable.Rows.Count <= 0)
		{
			return;
		}
		string text = (GlobalVariables.IsArabic ? "Right" : "Left");
		string text2 = (GlobalVariables.IsArabic ? "rtl" : "ltr");
		string senderDisplayName = (GlobalVariables.IsArabic ? GlobalVariables.CompanyNameAr : GlobalVariables.CompanyNameEn);
		string sender2 = GlobalVariables.dtSystemDefaults.Select("DefaultEnName ='CompanyEmail'")[0]["DefaultValue"].ToString();
		string password = GlobalVariables.dtSystemDefaults.Select("DefaultEnName ='CompanyEmailPassword'")[0]["DefaultValue"].ToString();
		string subject = (GlobalVariables.IsArabic ? "شيك المرتب" : "Salary Print Check");
		string text3 = "";
		string text4 = "<table  dir=\"" + text2 + "\" style=\"border-collapse:collapse; text-align:" + text + "\" >";
		string text5 = "</table>";
		string text6 = "<tr style =\"color:#555555\">";
		string text7 = "</tr>";
		string text8 = "<td width=\"50%\" style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\"><span style=\"font - weight:bold; color:#004993\">";
		string text9 = "<td width=\"25%\"style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px\">";
		string text10 = "</span></td>";
		string text11 = "</td>";
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			text3 = string.Concat("<pre> Dear : " + dataTable.Rows[i][0].ToString(), "\n\n\tThis is your Salary Sheet for ", dataTable.Rows[i][1].ToString(), "\n\n\n", text4, "</pre>");
			int num = 0;
			for (num = 2; num < dataTable.Columns.Count - 1; num++)
			{
				if ((num + 2) % 3 == 1)
				{
					decimal result;
					bool flag = decimal.TryParse(dataTable.Rows[i][dataTable.Columns[num + 2].Caption].ToString(), out result);
					if ((!flag && dataTable.Rows[i][dataTable.Columns[num + 2].Caption].ToString() == "") || (flag && result == 0m))
					{
						num += 2;
						continue;
					}
					text3 += text6;
					text3 = text3 + text8 + dataTable.Rows[i][dataTable.Columns[num].Caption].ToString() + text10;
				}
				else
				{
					text3 = text3 + text9 + dataTable.Rows[i][dataTable.Columns[num].Caption].ToString() + text11;
				}
				if ((num + 2) % 3 == 0)
				{
					text3 += text7;
				}
			}
			text3 += text5;
			GlobalFunctions.SendMail(senderDisplayName, sender2, password, dataTable.Rows[i][dataTable.Columns[num].Caption].ToString(), "", text3, subject, IsBodyHtml: true);
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Payroll.Reports.frmEmployeeSalaryByMail));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		this.btnSend = new UltraButton();
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
		base.SuspendLayout();
		resources.ApplyResources(base.btnPreview, "btnPreview");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2018, 9, 30, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2018, 9, 30, 0, 0, 0, 0);
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
		resources.ApplyResources(base.lblReportType, "lblReportType");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance6.FontData");
		resources.ApplyResources(val3, "appearance6");
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
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance8.FontData");
		resources.ApplyResources(val4, "appearance8");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this.btnSend, "btnSend");
		((System.Windows.Forms.Control)(object)this.btnSend).Name = "btnSend";
		((System.Windows.Forms.Control)(object)this.btnSend).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSend).Click += new System.EventHandler(btnSend_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSend);
		base.Name = "frmEmployeeSalaryByMail";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSend, 0);
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
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
