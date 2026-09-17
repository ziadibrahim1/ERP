using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Production;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.Reports;

public class frmVouchersCost : frmReportTree2010
{
	private IContainer components = null;

	public UltraComboEditor cboTransType;

	public UltraLabel ultraLabel3;

	public frmVouchersCost()
	{
		TreeItems2IDCol = "HeaderID";
		TreeItems2NameCol = "VoucherNo";
		InitializeComponent();
		DataTable dt = ItemsTransactions.SelectActiveTransTypes(GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboTransType, dt, "TransType", "TransTypeName");
	}

	public override void FormLoad()
	{
	}

	public override void FillData()
	{
		GetBranches();
		dtItems2 = ItemsTransactions.SelectVouchers(Branches, (cboTransType.SelectedIndex == -1) ? "-" : ((TextEditorControlBase)cboTransType).Value.ToString(), dtpFromDate.DateTime.Date.ToString(GlobalVariables.DateShortFormate), dtpToDate.DateTime.ToString("MM/dd/yyyy 23:59:59"));
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
	}

	public override string GetReportName()
	{
		return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_SC_VouchersCost_A_nologo.rpt" : "Rep_SC_VouchersCost_E_nologo.rpt");
	}

	public override void ShowReport()
	{
		GetItems();
		string text = "";
		text = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ReportName ='" + ((Control)(object)cboReportType).Text + "'")[0]["isoCode"].ToString());
		if (Items2.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى اذن ليتم عرضه ", "please check Vouchers to be shown in report");
			return;
		}
		GlobalVariables.IsRepOnlineConn = (Online = Main.IsSynchronization && !GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value));
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@TransType", ((TextEditorControlBase)cboTransType).Value);
		GlobalVariables.ReportDocument.SetParameterValue("@VoucherIDs", Items2);
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

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
	}

	private void cboTransType_ValueChanged(object sender, EventArgs e)
	{
		if (!IsLoading)
		{
			FillData();
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		if (cboTransType.SelectedIndex == -1)
		{
			return;
		}
		GetBranches();
		if (((TextEditorControlBase)cboTransType).Value.Equals("GRN"))
		{
			dtSearchResult = SearchFunctions.GoodReceiptNotesReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int i = 0; i < dtSearchResult.Rows.Count; i++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[i]["GoodReceiptNoteID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("LnsMIV"))
		{
			dtSearchResult = SearchFunctions.LnsInvoicesReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0, -1);
			for (int j = 0; j < dtSearchResult.Rows.Count; j++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[j]["InvoiceID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("MIV"))
		{
			dtSearchResult = SearchFunctions.MaterialIssueVouchersReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int k = 0; k < dtSearchResult.Rows.Count; k++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[k]["MaterialIssueVoucherID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("OPN"))
		{
			dtSearchResult = SearchFunctions.OpeningBalancesReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1);
			for (int l = 0; l < dtSearchResult.Rows.Count; l++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[l]["OpeningBalanceID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("POSMIV"))
		{
			dtSearchResult = SearchFunctions.ChecksReport(dtpFromDate.DateTime.Date, dtpToDate.DateTime, "-1", "-1", "-1", "-1", Branches);
			for (int m = 0; m < dtSearchResult.Rows.Count; m++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[m]["CheckID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("ProGRN") || ((TextEditorControlBase)cboTransType).Value.Equals("ProMIV"))
		{
			dtSearchResult = SearchFunctions.ProductionsReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int n = 0; n < dtSearchResult.Rows.Count; n++)
			{
				DataTable dataTable = ProductionsStagesApprove.SelectByProductionID(dtSearchResult.Rows[n]["ProductionID"].ToString(), "1");
				for (int num = 0; num < dataTable.Rows.Count; num++)
				{
					try
					{
						TreeItems2.GetNodeByKey(dataTable.Rows[num]["ProductionStageApproveID"].ToString()).CheckedState = CheckState.Checked;
					}
					catch
					{
					}
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RcpGRN"))
		{
			dtSearchResult = SearchFunctions.RecipeManufacturingReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1);
			for (int num2 = 0; num2 < dtSearchResult.Rows.Count; num2++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num2]["RecipeManufacturingID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RcpMIV"))
		{
			dtSearchResult = SearchFunctions.RecipeManufacturingReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1);
			for (int num3 = 0; num3 < dtSearchResult.Rows.Count; num3++)
			{
				DataTable dataTable2 = RecipeManufacturingDetails.SelectByRecipeManufacturingID(dtSearchResult.Rows[num3]["RecipeManufacturingID"].ToString(), "1");
				for (int num4 = 0; num4 < dataTable2.Rows.Count; num4++)
				{
					try
					{
						TreeItems2.GetNodeByKey(dataTable2.Rows[num4]["RecipeManufacturingDetailID"].ToString()).CheckedState = CheckState.Checked;
					}
					catch
					{
					}
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RGRN"))
		{
			dtSearchResult = SearchFunctions.SuppliersReturnsReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int num5 = 0; num5 < dtSearchResult.Rows.Count; num5++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num5]["SupplierReturnID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RLnsMIV"))
		{
			dtSearchResult = SearchFunctions.LnsInvoicesReturnsReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int num6 = 0; num6 < dtSearchResult.Rows.Count; num6++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num6]["ReturnID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RMIV"))
		{
			dtSearchResult = SearchFunctions.ClientsDepartmentsReturnsReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0, -1);
			for (int num7 = 0; num7 < dtSearchResult.Rows.Count; num7++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num7]["ReturnID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RPOSMIV"))
		{
			dtSearchResult = SearchFunctions.ChecksReturnsReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, "-1", "-1", "-1", -1, 0);
			for (int num8 = 0; num8 < dtSearchResult.Rows.Count; num8++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num8]["ReturnID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("RVLFrom") || ((TextEditorControlBase)cboTransType).Value.Equals("RVLTo"))
		{
			dtSearchResult = SearchFunctions.StoreRevaluationsReport(GlobalVariables.StoreIDs, Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int num9 = 0; num9 < dtSearchResult.Rows.Count; num9++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num9]["StoreRevaluationVoucherID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("SlcGRN") || ((TextEditorControlBase)cboTransType).Value.Equals("SlcMIV"))
		{
			dtSearchResult = SearchFunctions.SlicingReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1);
			for (int num10 = 0; num10 < dtSearchResult.Rows.Count; num10++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num10]["SlicingID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("STK"))
		{
			dtSearchResult = SearchFunctions.StoreTakingsReport(GlobalVariables.StoreIDs, Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0);
			for (int num11 = 0; num11 < dtSearchResult.Rows.Count; num11++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num11]["StoreTakingID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else if (((TextEditorControlBase)cboTransType).Value.Equals("STL"))
		{
			dtSearchResult = SearchFunctions.StoresSettlementVouchersReport(Branches, dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, -1, 0);
			for (int num12 = 0; num12 < dtSearchResult.Rows.Count; num12++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num12]["SettlementVoucherID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
		}
		else
		{
			if (!((TextEditorControlBase)cboTransType).Value.Equals("TrnFrom") && !((TextEditorControlBase)cboTransType).Value.Equals("TrnTo"))
			{
				return;
			}
			dtSearchResult = SearchFunctions.StoreTransferVouchersReport(Branches, "-1", dtpFromDate.DateTime.Date, dtpToDate.DateTime, -1, 0, FromServer: false);
			for (int num13 = 0; num13 < dtSearchResult.Rows.Count; num13++)
			{
				try
				{
					TreeItems2.GetNodeByKey(dtSearchResult.Rows[num13]["StoreTransferVoucherID"].ToString()).CheckedState = CheckState.Checked;
				}
				catch
				{
				}
			}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Reports.frmVouchersCost));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.cboTransType = new UltraComboEditor();
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
		((System.ComponentModel.ISupportInitialize)this.cboTransType).BeginInit();
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
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
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
		resources.ApplyResources(this.cboTransType, "cboTransType");
		this.cboTransType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTransType).Name = "cboTransType";
		((TextEditorControlBase)this.cboTransType).Nullable = false;
		((TextEditorControlBase)this.cboTransType).ValueChanged += new System.EventHandler(cboTransType_ValueChanged);
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTransType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Name = "frmVouchersCost";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTransType, 0);
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
		((System.ComponentModel.ISupportInitialize)this.cboTransType).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
