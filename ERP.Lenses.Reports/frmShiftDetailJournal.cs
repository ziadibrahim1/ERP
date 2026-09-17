using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Lenses.Transactions;
using ERP.LensesProductions.Transactions;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Lenses.Reports;

public class frmShiftDetailJournal : frmReportTree2010
{
	private IContainer components = null;

	public frmShiftDetailJournal()
	{
		InitializeComponent();
		TreeItemsIDCol = "User_ID";
		TreeItemsNameCol = (GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		TreeItems2IDCol = "ShiftDetailID";
		TreeItems2NameCol = "Name";
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "يومية المبيعات تفصيلي" : "Sales Journal");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "يومية المبيعات" : "Sales Journal");
		cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "يومية المبيعات إجمالي" : "Sales Journal");
		cboReportType.SelectedIndex = 0;
	}

	public override void FormLoad()
	{
		dtItems = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeFunctions.FillTreeOneLevel(TreeItems, dtItems, TreeItemsIDCol, TreeItemsNameCol);
		((UltraToggleEditorBase)chkAll).Checked = true;
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged -= chkAllBranches_CheckedChanged;
		((UltraToggleEditorBase)chkAllBranches).Checked = true;
		SelectAllListBoxItems(Checked: true, clbBranches);
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged += chkAllBranches_CheckedChanged;
	}

	public override void FillData()
	{
		GetBranches();
		dtItems2 = ShiftsDetails.FillComboByDate(Branches, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "-1");
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
		((UltraToggleEditorBase)chkAll2).Checked = false;
	}

	public override void ShowReport()
	{
		GetItems();
		if (Items2.Length < 2)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى وردية ليتم عرضها ", "There is no chosen Shift Details to be shown in the report, please check Shift Details to be shown in report");
			return;
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@UserIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		if (GlobalVariables.ReportDocument.Subreports.Count > 0)
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", Items2, "Rep_POS_ShiftsDetails_LnsJournalTextMessage");
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

	public override void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
		if (!(GroupNamePath != ""))
		{
			return;
		}
		string text = "";
		string text2 = "";
		string text3 = GroupNamePath.Substring(0, GroupNamePath.IndexOf("VoucherID") + 9);
		string oldValue = GroupNamePath.Substring(0, GroupNamePath.IndexOf("Type") + 4);
		if (text3.IndexOf("VoucherID") < 0)
		{
			return;
		}
		if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].Equals("Rep_Lns_Expenses_Journal_A.rpt"))
		{
			text = GroupNamePath.Replace(text3, "").Replace(",", "").Replace("[", "")
				.Replace("]", "");
			frmHeaderDetails frmHeaderDetails2 = new frmExpenses(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "المصروفات" : "frmExpenses");
			frmHeaderDetails2.ShowDialog();
		}
		else if (dtReports.Rows.Count != 0 && dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].Equals("Rep_Lns_InvoicesReturnsPaid_A.rpt"))
		{
			text = GroupNamePath.Replace(text3, "").Replace(",", "").Replace("[", "")
				.Replace("]", "");
			frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoicesReturns(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع البيع المباشر" : "Lns Invoices Returns");
			frmHeaderDetails2.ShowDialog();
		}
		else
		{
			text = GroupNamePath.Substring(GroupNamePath.IndexOf("VoucherID") + 9, GroupNamePath.IndexOf("Type") - GroupNamePath.IndexOf("VoucherID") - 10).Replace(text3, "").Replace(",", "")
				.Replace("[", "")
				.Replace("]", "");
			text2 = GroupNamePath.Replace(oldValue, "").Replace(",", "").Replace("[", "")
				.Replace("]", "");
		}
		switch (text2)
		{
		case "Sales":
		case "مبيعات":
		{
			DataRow[] array2 = GlobalVariables.dtForms.Select("Form IN ('frmLnsInvoices', 'frmLnsInvoices2', 'frmLnsInvoicesWithPayment', 'frmLnsInvoicesWithPayment2', 'frmLnsInvoicesWithPayment3')");
			if (array2.Length != 0)
			{
				switch (array2[0]["Form"].ToString())
				{
				case "frmLnsInvoices":
				{
					frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoices(int.Parse(text));
					frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
					((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array2[0]["FormNameAr"].ToString() : array2[0]["FormNameEn"].ToString());
					frmHeaderDetails2.ShowDialog();
					break;
				}
				case "frmLnsInvoices2":
				{
					frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoices2(int.Parse(text));
					frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
					((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array2[0]["FormNameAr"].ToString() : array2[0]["FormNameEn"].ToString());
					frmHeaderDetails2.ShowDialog();
					break;
				}
				case "frmLnsInvoicesWithPayment":
				{
					frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoicesWithPayment(int.Parse(text));
					frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
					((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array2[0]["FormNameAr"].ToString() : array2[0]["FormNameEn"].ToString());
					frmHeaderDetails2.ShowDialog();
					break;
				}
				case "frmLnsInvoicesWithPayment2":
				{
					frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoicesWithPayment2(int.Parse(text));
					frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
					((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array2[0]["FormNameAr"].ToString() : array2[0]["FormNameEn"].ToString());
					frmHeaderDetails2.ShowDialog();
					break;
				}
				case "frmLnsInvoicesWithPayment3":
				{
					frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoicesWithPayment3(int.Parse(text));
					frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
					((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array2[0]["FormNameAr"].ToString() : array2[0]["FormNameEn"].ToString());
					frmHeaderDetails2.ShowDialog();
					break;
				}
				}
			}
			break;
		}
		case "lab Sales":
		case "مبيعات بصريات":
		{
			DataRow[] array = GlobalVariables.dtForms.Select("Form IN ('frmLnsLabOrders', 'frmLnsLabOrders2')");
			if (array.Length == 0)
			{
				break;
			}
			string text4 = array[0]["Form"].ToString();
			if (!(text4 == "frmLnsLabOrders"))
			{
				if (text4 == "frmLnsLabOrders2")
				{
					frmHeaderManyDetails frmHeaderManyDetails2 = new frmLnsLabOrders2(int.Parse(text));
					frmHeaderManyDetails2.StartPosition = FormStartPosition.CenterParent;
					((Control)(object)frmHeaderManyDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array[0]["FormNameAr"].ToString() : array[0]["FormNameEn"].ToString());
					frmHeaderManyDetails2.ShowDialog();
				}
			}
			else
			{
				frmHeaderManyDetails frmHeaderManyDetails2 = new frmLnsLabOrders(int.Parse(text));
				frmHeaderManyDetails2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmHeaderManyDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? array[0]["FormNameAr"].ToString() : array[0]["FormNameEn"].ToString());
				frmHeaderManyDetails2.ShowDialog();
			}
			break;
		}
		case "Pro Sales":
		case "مبيعات تصنيع":
		{
			frmHeaderManyDetails frmHeaderManyDetails2 = new frmBlanksInvoices(int.Parse(text));
			frmHeaderManyDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderManyDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "فاتورة التصنيعات" : "Blanks Invoices");
			frmHeaderManyDetails2.ShowDialog();
			break;
		}
		case "Returns":
		case "مرتجعات":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmLnsInvoicesReturns(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع البيع المباشر" : "Lns Invoices Returns");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Lab Returns":
		case "مرتجعات بصريات":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmLnsLabOrdersReturns(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع فواتير" : "Lab Orders Return");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Lab Payment":
		case "سداد بصريات":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmLnsLabOrdersPayments(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد عميل" : "Lab Orders Payments");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Revenues":
		case "إيرادات":
		case "إيرادات بصريات":
		case "Lab Revenues":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmRevenues(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "الايرادات" : "Revenues");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Expenses":
		case "مصروفات":
		case "مصروفات بصريات":
		case "Lab Expenses":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmExpenses(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "المصروفات" : "frmExpenses");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "إشعار خصم بصريات":
		case "Lab Client Credit Note":
		case "Client Credit Note":
		case "إشعار خصم":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmClientCreditNote(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "اشعار خصم" : "Client Credit Note");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "فيزا بصريات":
		case "Lab Visa":
		case "visa":
		case "فيزا":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmVisaTypesPayments(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد فيزا" : "Visa Types Payments");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "مرتجع فيزا بصريات":
		case "Lab Visa Returns":
		case "visa Returns":
		case "مرتجع فيزا":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmVisaTypesReturns(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع سداد فيزا" : "Visa Types Returns");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Pro Payment":
		case "سداد تصنيع":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmBlanksInvoicesPayments(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد عميل" : "Blanks Invoices Payments");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Res Payment":
		case "سداد حجز":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmReservationPayments(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد حجز" : "Reservation Payment");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		case "Res Return":
		case "مرتجع حجز":
		{
			frmHeaderDetails frmHeaderDetails2 = new frmReservationPaymentsReturns(int.Parse(text));
			frmHeaderDetails2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmHeaderDetails2.lblTitle).Text = (GlobalVariables.IsArabic ? "مرتجع سداد الحجز" : "Reservation Payments Returns");
			frmHeaderDetails2.ShowDialog();
			break;
		}
		}
		RefreshReport(frmViewer);
	}

	public void RefreshReport(frmReporViwer frmViewer)
	{
		int currentPageNumber = frmViewer.crvReportViewer.GetCurrentPageNumber();
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E") + (((UltraToggleEditorBase)chkWithLogo).Checked ? "" : "_nologo")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", Items2);
		GlobalVariables.ReportDocument.SetParameterValue("@UserIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		if (GlobalVariables.ReportDocument.Subreports.Count > 0)
		{
			GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailIDs", Items2, "Rep_POS_ShiftsDetails_LnsJournalTextMessage");
		}
		frmViewer.frmParent = this;
		frmViewer.ConfigureReport();
		frmViewer.BringToFront();
		frmViewer.Refresh();
		frmViewer.crvReportViewer.ShowNthPage(currentPageNumber);
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ShiftsDetailsReport(Branches);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmShiftDetailJournal));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
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
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val;
		base.dtpFromDate.DateTime = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2022, 8, 29, 0, 0, 0, 0);
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpToDate.DateTime = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 9, 8, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val3;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name4");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		resources.ApplyResources(this, "$this");
		base.Name = "frmShiftDetailJournal";
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
