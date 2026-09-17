using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.CnsProjects;
using BusinessLayer.Constructions;
using BusinessLayer.CRM;
using BusinessLayer.CustomsClearence;
using BusinessLayer.DirectSalesApp;
using BusinessLayer.Export;
using BusinessLayer.FixedAssets;
using BusinessLayer.General;
using BusinessLayer.HMS;
using BusinessLayer.HR;
using BusinessLayer.Lenses;
using BusinessLayer.LensesProductions;
using BusinessLayer.MarineService;
using BusinessLayer.Photos;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using BusinessLayer.Production;
using BusinessLayer.Purchasing;
using BusinessLayer.SafesAndBanks;
using BusinessLayer.Sales;
using BusinessLayer.Sling;
using BusinessLayer.SMS;
using BusinessLayer.StockControl;
using BusinessLayer.WareHouse;
using ERP.Accounting.Search;
using ERP.Clinics.Search;
using ERP.CnsProjects.Search;
using ERP.Constructions.Search;
using ERP.CRM.Search;
using ERP.CustomsClearence.Search;
using ERP.DirectSalesApp.Search;
using ERP.EInvoices.Search;
using ERP.Export.Search;
using ERP.FixedAssets.Search;
using ERP.HMS.Search;
using ERP.HR.Attendance.Search;
using ERP.HR.Payroll.Search;
using ERP.HR.Personal.Search;
using ERP.Lenses.Search;
using ERP.LensesProductions.Search;
using ERP.MarineService.Search;
using ERP.Photos.Search;
using ERP.POS.Search;
using ERP.Production.Search;
using ERP.Purchasing.Search;
using ERP.SafesAndBanks.Search;
using ERP.Sales.Search;
using ERP.Sling.Search;
using ERP.SMS.Search;
using ERP.StockControl.Search;
using ERP.SystemOptions.Search;
using ERP.WareHouse.Search;
using MS;

namespace ERP.Classes;

internal class SearchFunctions
{
	public static DataTable ForeignCurrencySettlementsSearchReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmForeignCurrencySettlementsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmForeignCurrencySettlementsSearchReport frmForeignCurrencySettlementsSearchReport2 = new frmForeignCurrencySettlementsSearchReport(ForeignCurrencySettlements.Search(Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ForeignCurrencySettlementID", -1, 0);
		frmForeignCurrencySettlementsSearchReport2.MinDate = minDate;
		frmForeignCurrencySettlementsSearchReport2.MaxDate = maxDate;
		frmForeignCurrencySettlementsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmForeignCurrencySettlementsSearchReport2.Location = new Point(0, 0);
		frmForeignCurrencySettlementsSearchReport2.ShowDialog();
		return frmForeignCurrencySettlementsSearchReport2.dtResult;
	}

	public static DataTable GeneralExpensesReportSelectApproved()
	{
		frmGeneralExpensesSearchReport frmGeneralExpensesSearchReport2 = new frmGeneralExpensesSearchReport(GeneralExpenses.SelectApproved(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "GeneralExpenseID", 1, 0);
		frmGeneralExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmGeneralExpensesSearchReport2.Location = new Point(0, 0);
		frmGeneralExpensesSearchReport2.ShowDialog();
		frmGeneralExpensesSearchReport2.dtSource = null;
		return frmGeneralExpensesSearchReport2.dtResult;
	}

	public static DataTable GeneralExpensesSearchReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmGeneralExpensesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmGeneralExpensesSearchReport frmGeneralExpensesSearchReport2 = new frmGeneralExpensesSearchReport(GeneralExpenses.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "GeneralExpenseID", Approved, Deleted);
		frmGeneralExpensesSearchReport2.MinDate = minDate;
		frmGeneralExpensesSearchReport2.MaxDate = maxDate;
		frmGeneralExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmGeneralExpensesSearchReport2.Location = new Point(0, 0);
		frmGeneralExpensesSearchReport2.ShowDialog();
		return frmGeneralExpensesSearchReport2.dtResult;
	}

	public static int Accounts(bool IsFromServer)
	{
		frmAccountsSearch frmAccountsSearch2 = new frmAccountsSearch(BusinessLayer.Accounting.Accounts.Search(GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", IsFromServer), "AccountID");
		frmAccountsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAccountsSearch2.Location = new Point(0, 0);
		frmAccountsSearch2.ShowDialog();
		return frmAccountsSearch2.ID;
	}

	public static DataTable AccountsReport(bool IsFromServer)
	{
		frmAccountsSearchReport frmAccountsSearchReport2 = new frmAccountsSearchReport(BusinessLayer.Accounting.Accounts.Search(GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", IsFromServer), "AccountID");
		frmAccountsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAccountsSearchReport2.Location = new Point(0, 0);
		frmAccountsSearchReport2.ShowDialog();
		return frmAccountsSearchReport2.dtResult;
	}

	public static DataTable CurrencysReport(bool IsFromServer)
	{
		frmCurrencySearchReport frmCurrencySearchReport2 = new frmCurrencySearchReport(Currency.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CurrencyID");
		frmCurrencySearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCurrencySearchReport2.Location = new Point(0, 0);
		frmCurrencySearchReport2.ShowDialog();
		return frmCurrencySearchReport2.dtResult;
	}

	public static int CostCenter(bool IsFromServer)
	{
		frmCostCentersSearch frmCostCentersSearch2 = new frmCostCentersSearch(CostCenters.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CostCenterID");
		frmCostCentersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCostCentersSearch2.Location = new Point(0, 0);
		frmCostCentersSearch2.ShowDialog();
		return frmCostCentersSearch2.ID;
	}

	public static DataTable CostCenterReport(bool IsFromServer)
	{
		frmCostCentersSearchReport frmCostCentersSearchReport2 = new frmCostCentersSearchReport(CostCenters.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CostCenterID");
		frmCostCentersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCostCentersSearchReport2.Location = new Point(0, 0);
		frmCostCentersSearchReport2.ShowDialog();
		return frmCostCentersSearchReport2.dtResult;
	}

	public static int SubCostCenter(bool IsFromServer)
	{
		frmSubCostCenterSearch frmSubCostCenterSearch2 = new frmSubCostCenterSearch(BusinessLayer.Accounting.SubCostCenter.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubCostCenterID");
		frmSubCostCenterSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubCostCenterSearch2.Location = new Point(0, 0);
		frmSubCostCenterSearch2.ShowDialog();
		return frmSubCostCenterSearch2.ID;
	}

	public static DataTable SubCostCenterReport(bool IsFromServer)
	{
		frmSubCostCenterSearchReport frmSubCostCenterSearchReport2 = new frmSubCostCenterSearchReport(BusinessLayer.Accounting.SubCostCenter.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubCostCenterID");
		frmSubCostCenterSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubCostCenterSearchReport2.Location = new Point(0, 0);
		frmSubCostCenterSearchReport2.ShowDialog();
		return frmSubCostCenterSearchReport2.dtResult;
	}

	public static int SubAccounts(string AccountID, bool IsFromServer)
	{
		frmSubAccountsSearch frmSubAccountsSearch2 = new frmSubAccountsSearch(BusinessLayer.Accounting.SubAccounts.Search(AccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsSearch2.Location = new Point(0, 0);
		frmSubAccountsSearch2.ShowDialog();
		return frmSubAccountsSearch2.ID;
	}

	public static int LnsProductionSubAccounts(string AccountID, string IsActive, bool IsFromServer)
	{
		frmSubAccountsSearch frmSubAccountsSearch2 = new frmSubAccountsSearch(BusinessLayer.Accounting.SubAccounts.SearchLnsProduction(AccountID, IsActive, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsSearch2.Location = new Point(0, 0);
		frmSubAccountsSearch2.ShowDialog();
		return frmSubAccountsSearch2.ID;
	}

	public static int SubAccountsBySubAccountTypeIDs(string SubAccountTypeIDs, string BranchID, bool IsFromServer)
	{
		frmSubAccountsBySubAccountTypeIDsSearch frmSubAccountsBySubAccountTypeIDsSearch2 = new frmSubAccountsBySubAccountTypeIDsSearch(BusinessLayer.Accounting.SubAccounts.SearchBySubAccountTypeIDs(SubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsBySubAccountTypeIDsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsBySubAccountTypeIDsSearch2.Location = new Point(0, 0);
		frmSubAccountsBySubAccountTypeIDsSearch2.ShowDialog();
		return frmSubAccountsBySubAccountTypeIDsSearch2.ID;
	}

	public static DataTable SubAccountsBySubAccountTypeIDsReport(string SubAccountTypeIDs, string BranchID, bool IsFromServer)
	{
		frmSubAccountsBySubAccountTypeIDsSearchReport frmSubAccountsBySubAccountTypeIDsSearchReport2 = new frmSubAccountsBySubAccountTypeIDsSearchReport(BusinessLayer.Accounting.SubAccounts.SearchBySubAccountTypeIDs(SubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsBySubAccountTypeIDsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsBySubAccountTypeIDsSearchReport2.Location = new Point(0, 0);
		frmSubAccountsBySubAccountTypeIDsSearchReport2.ShowDialog();
		return frmSubAccountsBySubAccountTypeIDsSearchReport2.dtResult;
	}

	public static DataTable SubAccountsReport(string AccountID, bool IsFromServer)
	{
		frmSubAccountsSearchReport frmSubAccountsSearchReport2 = new frmSubAccountsSearchReport(BusinessLayer.Accounting.SubAccounts.Search(AccountID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsSearchReport2.Location = new Point(0, 0);
		frmSubAccountsSearchReport2.ShowDialog();
		return frmSubAccountsSearchReport2.dtResult;
	}

	public static DataTable JVReport(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmJVSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmJVSearchReport frmJVSearchReport2 = new frmJVSearchReport(JV.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0"), "JVID", Approved, Deleted);
		frmJVSearchReport2.MinDate = minDate;
		frmJVSearchReport2.MaxDate = maxDate;
		frmJVSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmJVSearchReport2.Location = new Point(0, 0);
		frmJVSearchReport2.ShowDialog();
		frmJVSearchReport2.dtSource = null;
		return frmJVSearchReport2.dtResult;
	}

	public static DataTable JVReportByDate(string BranchIDs, string FromDate, string ToDate, int Approved, int Deleted)
	{
		frmJVSearchReport frmJVSearchReport2 = new frmJVSearchReport(JV.SearchByDate(BranchIDs, FromDate, ToDate, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0"), "JVID", Approved, Deleted);
		frmJVSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmJVSearchReport2.Location = new Point(0, 0);
		frmJVSearchReport2.ShowDialog();
		frmJVSearchReport2.dtSource = null;
		return frmJVSearchReport2.dtResult;
	}

	public static DataTable JVReportSelectApproved()
	{
		frmJVSearchReport frmJVSearchReport2 = new frmJVSearchReport(JV.SelectApproved(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "JVID", 1, 0);
		frmJVSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmJVSearchReport2.Location = new Point(0, 0);
		frmJVSearchReport2.ShowDialog();
		frmJVSearchReport2.dtSource = null;
		return frmJVSearchReport2.dtResult;
	}

	public static DataTable RepeatedJVReport(int Deleted)
	{
		frmRepeatedJVSearchReport frmRepeatedJVSearchReport2 = new frmRepeatedJVSearchReport(RepeatedJV.Search(GlobalVariables.BranchIDs, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "RepeatedJVID", Deleted);
		frmRepeatedJVSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmRepeatedJVSearchReport2.Location = new Point(0, 0);
		frmRepeatedJVSearchReport2.ShowDialog();
		return frmRepeatedJVSearchReport2.dtResult;
	}

	public static DataTable BranchDebitTransfersReport(int Approved, int Deleted, bool IsFromServer)
	{
		string fullName = typeof(frmBranchDebitTransfersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBranchDebitTransfersSearchReport frmBranchDebitTransfersSearchReport2 = new frmBranchDebitTransfersSearchReport(BranchDebitTransfers.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", IsFromServer), "BranchDebitTransferID", Approved, Deleted);
		frmBranchDebitTransfersSearchReport2.MinDate = minDate;
		frmBranchDebitTransfersSearchReport2.MaxDate = maxDate;
		frmBranchDebitTransfersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBranchDebitTransfersSearchReport2.Location = new Point(0, 0);
		frmBranchDebitTransfersSearchReport2.ShowDialog();
		frmBranchDebitTransfersSearchReport2.dtSource = null;
		return frmBranchDebitTransfersSearchReport2.dtResult;
	}

	public static int Suppliers(string BranchID, string ForAllBranches, bool IsFromServer)
	{
		frmSubAccountsSupplierSearch frmSubAccountsSupplierSearch2 = new frmSubAccountsSupplierSearch(SubAccountsClientSupplier.SuppliersSearch(GlobalVariables.SupplierSubAccountTypeIDs, BranchID, "-1", ForAllBranches, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsSupplierSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsSupplierSearch2.Location = new Point(0, 0);
		frmSubAccountsSupplierSearch2.ShowDialog();
		return frmSubAccountsSupplierSearch2.ID;
	}

	public static int Suppliers(string BranchID, string SubAccountIDs, string ForAllBranches, bool IsFromServer)
	{
		frmSubAccountsSupplierSearch frmSubAccountsSupplierSearch2 = new frmSubAccountsSupplierSearch(SubAccountsClientSupplier.SuppliersSearch(GlobalVariables.SupplierSubAccountTypeIDs, BranchID, SubAccountIDs, ForAllBranches, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsSupplierSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsSupplierSearch2.Location = new Point(0, 0);
		frmSubAccountsSupplierSearch2.ShowDialog();
		return frmSubAccountsSupplierSearch2.ID;
	}

	public static DataTable SuppliersReport(bool IsFromServer)
	{
		frmSubAccountsSupplierSearchReport frmSubAccountsSupplierSearchReport2 = new frmSubAccountsSupplierSearchReport(SubAccountsClientSupplier.SuppliersSearch(GlobalVariables.SupplierSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsSupplierSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsSupplierSearchReport2.Location = new Point(0, 0);
		frmSubAccountsSupplierSearchReport2.ShowDialog();
		return frmSubAccountsSupplierSearchReport2.dtResult;
	}

	public static int Clients(string BranchID, string ForAllBranches, bool IsFromServer)
	{
		ERP.Sales.Search.frmSubAccountsClientSearch frmSubAccountsClientSearch2 = new ERP.Sales.Search.frmSubAccountsClientSearch(SubAccountsClientSupplier.ClientsSearch(GlobalVariables.ClientSubAccountTypeIDs, BranchID, ForAllBranches, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsClientSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsClientSearch2.Location = new Point(0, 0);
		frmSubAccountsClientSearch2.ShowDialog();
		return frmSubAccountsClientSearch2.ID;
	}

	public static DataTable ClientsReport(string BranchID, bool IsFromServer)
	{
		ERP.Sales.Search.frmSubAccountsClientSearchReport frmSubAccountsClientSearchReport2 = new ERP.Sales.Search.frmSubAccountsClientSearchReport(SubAccountsClientSupplier.ClientsSearch(GlobalVariables.ClientSubAccountTypeIDs, BranchID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsClientSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsClientSearchReport2.Location = new Point(0, 0);
		frmSubAccountsClientSearchReport2.ShowDialog();
		return frmSubAccountsClientSearchReport2.dtResult;
	}

	public static DataTable SafeInReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSafeInSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSafeInSearchReport frmSafeInSearchReport2 = new frmSafeInSearchReport(SafeIn.Search(GlobalVariables.BranchIDs, GlobalVariables.SafeIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SafeInID", Approved, Deleted);
		frmSafeInSearchReport2.MinDate = minDate;
		frmSafeInSearchReport2.MaxDate = maxDate;
		frmSafeInSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSafeInSearchReport2.Location = new Point(0, 0);
		frmSafeInSearchReport2.ShowDialog();
		return frmSafeInSearchReport2.dtResult;
	}

	public static DataTable SafeInReportByDate(string BranchIDs, string FromDate, string ToDate, int Approved, int Deleted)
	{
		frmSafeInSearchReport frmSafeInSearchReport2 = new frmSafeInSearchReport(SafeIn.SearchByDate(BranchIDs, GlobalVariables.SafeIDs, FromDate, ToDate, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SafeInID", Approved, Deleted);
		frmSafeInSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSafeInSearchReport2.Location = new Point(0, 0);
		frmSafeInSearchReport2.ShowDialog();
		return frmSafeInSearchReport2.dtResult;
	}

	public static DataTable SafeOutReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSafeOutSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSafeOutSearchReport frmSafeOutSearchReport2 = new frmSafeOutSearchReport(SafeOut.Search(GlobalVariables.BranchIDs, GlobalVariables.SafeIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SafeOutID", Approved, Deleted);
		frmSafeOutSearchReport2.MinDate = minDate;
		frmSafeOutSearchReport2.MaxDate = maxDate;
		frmSafeOutSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSafeOutSearchReport2.Location = new Point(0, 0);
		frmSafeOutSearchReport2.ShowDialog();
		return frmSafeOutSearchReport2.dtResult;
	}

	public static DataTable SafeOutRequestsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSafeOutRequestsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSafeOutRequestsSearchReport frmSafeOutRequestsSearchReport2 = new frmSafeOutRequestsSearchReport(SafeOutRequests.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SafeOutRequestID", Approved, Deleted);
		frmSafeOutRequestsSearchReport2.MinDate = minDate;
		frmSafeOutRequestsSearchReport2.MaxDate = maxDate;
		frmSafeOutRequestsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSafeOutRequestsSearchReport2.Location = new Point(0, 0);
		frmSafeOutRequestsSearchReport2.ShowDialog();
		return frmSafeOutRequestsSearchReport2.dtResult;
	}

	public static DataTable SafeOutReportByDate(string BranchIDs, string FromDate, string ToDate, int Approved, int Deleted)
	{
		frmSafeOutSearchReport frmSafeOutSearchReport2 = new frmSafeOutSearchReport(SafeOut.SearchByDate(BranchIDs, GlobalVariables.SafeIDs, FromDate, ToDate, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SafeOutID", Approved, Deleted);
		frmSafeOutSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSafeOutSearchReport2.Location = new Point(0, 0);
		frmSafeOutSearchReport2.ShowDialog();
		return frmSafeOutSearchReport2.dtResult;
	}

	public static DataTable BankInReport(int Approved, int UnderCollection, int Collected, int Returned, int IsCheck, int Deleted)
	{
		string fullName = typeof(frmBankInSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBankInSearchReport frmBankInSearchReport2 = new frmBankInSearchReport(BankIn.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), UnderCollection.ToString(), Collected.ToString(), Returned.ToString(), IsCheck.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BankInID", Approved, Deleted);
		frmBankInSearchReport2.MinDate = minDate;
		frmBankInSearchReport2.MaxDate = maxDate;
		frmBankInSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBankInSearchReport2.Location = new Point(0, 0);
		frmBankInSearchReport2.ShowDialog();
		return frmBankInSearchReport2.dtResult;
	}

	public static DataTable BankInReportByDate(string BranchIDs, string FromDate, string ToDate, int Approved, int UnderCollection, int Collected, int Returned, int IsCheck, int Deleted)
	{
		frmBankInSearchReport frmBankInSearchReport2 = new frmBankInSearchReport(BankIn.SearchByDate(BranchIDs, FromDate, ToDate, Approved.ToString(), UnderCollection.ToString(), Collected.ToString(), Returned.ToString(), IsCheck.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BankInID", Approved, Deleted);
		frmBankInSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBankInSearchReport2.Location = new Point(0, 0);
		frmBankInSearchReport2.ShowDialog();
		return frmBankInSearchReport2.dtResult;
	}

	public static DataTable BanKOutRequestReport(int Approved, int IsCheck, int Deleted)
	{
		string fullName = typeof(frmBankOutRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBankOutRequestSearchReport frmBankOutRequestSearchReport2 = new frmBankOutRequestSearchReport(BankOutRequests.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), IsCheck.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BankOutRequestID", Approved, Deleted);
		frmBankOutRequestSearchReport2.MinDate = minDate;
		frmBankOutRequestSearchReport2.MaxDate = maxDate;
		frmBankOutRequestSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBankOutRequestSearchReport2.Location = new Point(0, 0);
		frmBankOutRequestSearchReport2.ShowDialog();
		return frmBankOutRequestSearchReport2.dtResult;
	}

	public static DataTable BanKOutReport(int Approved, int UnderCollection, int Collected, int Returned, int IsCheck, int Deleted)
	{
		string fullName = typeof(frmBankOutSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBankOutSearchReport frmBankOutSearchReport2 = new frmBankOutSearchReport(BankOut.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), UnderCollection.ToString(), Collected.ToString(), Returned.ToString(), IsCheck.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BankOutID", Approved, Deleted);
		frmBankOutSearchReport2.MinDate = minDate;
		frmBankOutSearchReport2.MaxDate = maxDate;
		frmBankOutSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBankOutSearchReport2.Location = new Point(0, 0);
		frmBankOutSearchReport2.ShowDialog();
		return frmBankOutSearchReport2.dtResult;
	}

	public static DataTable BanKOutReportByDate(string BranchIDs, string FromDate, string ToDate, int Approved, int UnderCollection, int Collected, int Returned, int IsCheck, int Deleted)
	{
		frmBankOutSearchReport frmBankOutSearchReport2 = new frmBankOutSearchReport(BankOut.SearchByDate(BranchIDs, FromDate, ToDate, Approved.ToString(), UnderCollection.ToString(), Collected.ToString(), Returned.ToString(), IsCheck.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BankOutID", Approved, Deleted);
		frmBankOutSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBankOutSearchReport2.Location = new Point(0, 0);
		frmBankOutSearchReport2.ShowDialog();
		return frmBankOutSearchReport2.dtResult;
	}

	public static DataTable CustodyReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmCustodySearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmCustodySearchReport frmCustodySearchReport2 = new frmCustodySearchReport(BusinessLayer.SafesAndBanks.Custody.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "CustodyID", Approved, Deleted);
		frmCustodySearchReport2.MinDate = minDate;
		frmCustodySearchReport2.MaxDate = maxDate;
		frmCustodySearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustodySearchReport2.Location = new Point(0, 0);
		frmCustodySearchReport2.ShowDialog();
		return frmCustodySearchReport2.dtResult;
	}

	public static DataTable CustodyReportByDate(string BranchIDs, string FromDate, string ToDate, int Approved, int Deleted)
	{
		frmCustodySearchReport frmCustodySearchReport2 = new frmCustodySearchReport(BusinessLayer.SafesAndBanks.Custody.SearchByDate(BranchIDs, FromDate, ToDate, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "CustodyID", Approved, Deleted);
		frmCustodySearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustodySearchReport2.Location = new Point(0, 0);
		frmCustodySearchReport2.ShowDialog();
		return frmCustodySearchReport2.dtResult;
	}

	public static DataTable BanksReport(bool IsFromServer)
	{
		frmBanksSearchReport frmBanksSearchReport2 = new frmBanksSearchReport(Banks.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BankID");
		frmBanksSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBanksSearchReport2.Location = new Point(0, 0);
		frmBanksSearchReport2.ShowDialog();
		return frmBanksSearchReport2.dtResult;
	}

	public static int BanksSearch(bool IsFromServer)
	{
		frmBanksSearch frmBanksSearch2 = new frmBanksSearch(Banks.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BankID");
		frmBanksSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBanksSearch2.Location = new Point(0, 0);
		frmBanksSearch2.ShowDialog();
		return frmBanksSearch2.ID;
	}

	public static DataTable SafesReport(bool IsFromServer)
	{
		frmSafesSearchReport frmSafesSearchReport2 = new frmSafesSearchReport(Safes.Search(GlobalVariables.SafeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SafeID");
		frmSafesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSafesSearchReport2.Location = new Point(0, 0);
		frmSafesSearchReport2.ShowDialog();
		return frmSafesSearchReport2.dtResult;
	}

	public static int PSRequestSearch(string BranchIDs, int Approved, int IsRefused, int Closed, int Deleted)
	{
		string fullName = typeof(frmPSRequestSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSRequestSearch frmPSRequestSearch2 = new frmPSRequestSearch(PSRequest.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), IsRefused.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSRequestID", Approved, Deleted);
		frmPSRequestSearch2.MinDate = minDate;
		frmPSRequestSearch2.MaxDate = maxDate;
		frmPSRequestSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSRequestSearch2.Location = new Point(0, 0);
		frmPSRequestSearch2.ShowDialog();
		return frmPSRequestSearch2.ID;
	}

	public static DataTable PSRequestReport(int Approved, int IsRefused, int Closed, int Deleted)
	{
		string fullName = typeof(frmPSRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSRequestSearchReport frmPSRequestSearchReport2 = new frmPSRequestSearchReport(PSRequest.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), IsRefused.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSRequestID", Approved, Deleted);
		frmPSRequestSearchReport2.MinDate = minDate;
		frmPSRequestSearchReport2.MaxDate = maxDate;
		frmPSRequestSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSRequestSearchReport2.Location = new Point(0, 0);
		frmPSRequestSearchReport2.ShowDialog();
		return frmPSRequestSearchReport2.dtResult;
	}

	public static int PSOrdersSearch(string BranchIDs, int Approved, int Deleted, int Closed, int HasPSInvoice)
	{
		string fullName = typeof(frmPSOrdersSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSOrdersSearch frmPSOrdersSearch2 = new frmPSOrdersSearch(PSOrders.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), HasPSInvoice.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSOrderID", Approved, Deleted);
		frmPSOrdersSearch2.MinDate = minDate;
		frmPSOrdersSearch2.MaxDate = maxDate;
		frmPSOrdersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSOrdersSearch2.Location = new Point(0, 0);
		frmPSOrdersSearch2.ShowDialog();
		return frmPSOrdersSearch2.ID;
	}

	public static DataTable PSOrdersReport(int Approved, int Deleted, int Closed, int HasPSInvoice)
	{
		string fullName = typeof(frmPSOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSOrdersSearchReport frmPSOrdersSearchReport2 = new frmPSOrdersSearchReport(PSOrders.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), HasPSInvoice.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSOrderID", Approved, Deleted);
		frmPSOrdersSearchReport2.MinDate = minDate;
		frmPSOrdersSearchReport2.MaxDate = maxDate;
		frmPSOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSOrdersSearchReport2.Location = new Point(0, 0);
		frmPSOrdersSearchReport2.ShowDialog();
		return frmPSOrdersSearchReport2.dtResult;
	}

	public static int PSQuotationstSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(ERP.Purchasing.Search.frmQuotationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Purchasing.Search.frmQuotationsSearch frmQuotationsSearch2 = new ERP.Purchasing.Search.frmQuotationsSearch(BusinessLayer.Purchasing.Quotations.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearch2.MinDate = minDate;
		frmQuotationsSearch2.MaxDate = maxDate;
		frmQuotationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearch2.Location = new Point(0, 0);
		frmQuotationsSearch2.ShowDialog();
		return frmQuotationsSearch2.ID;
	}

	public static DataTable PSQuotationstReport(int Approved, int Deleted)
	{
		string fullName = typeof(ERP.Purchasing.Search.frmQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Purchasing.Search.frmQuotationsSearchReport frmQuotationsSearchReport2 = new ERP.Purchasing.Search.frmQuotationsSearchReport(BusinessLayer.Purchasing.Quotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearchReport2.MinDate = minDate;
		frmQuotationsSearchReport2.MaxDate = maxDate;
		frmQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearchReport2.Location = new Point(0, 0);
		frmQuotationsSearchReport2.ShowDialog();
		return frmQuotationsSearchReport2.dtResult;
	}

	public static int QuotationsRequestSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmQuotationsRequestSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmQuotationsRequestSearch frmQuotationsRequestSearch2 = new frmQuotationsRequestSearch(QuotationsRequest.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationRequestID", Approved, Deleted);
		frmQuotationsRequestSearch2.MinDate = minDate;
		frmQuotationsRequestSearch2.MaxDate = maxDate;
		frmQuotationsRequestSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsRequestSearch2.Location = new Point(0, 0);
		frmQuotationsRequestSearch2.ShowDialog();
		return frmQuotationsRequestSearch2.ID;
	}

	public static DataTable QuotationsRequestReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmQuotationsRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmQuotationsRequestSearchReport frmQuotationsRequestSearchReport2 = new frmQuotationsRequestSearchReport(QuotationsRequest.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationRequestID", Approved, Deleted);
		frmQuotationsRequestSearchReport2.MinDate = minDate;
		frmQuotationsRequestSearchReport2.MaxDate = maxDate;
		frmQuotationsRequestSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsRequestSearchReport2.Location = new Point(0, 0);
		frmQuotationsRequestSearchReport2.ShowDialog();
		return frmQuotationsRequestSearchReport2.dtResult;
	}

	public static int PSInvoicesSearch(string BranchIDs, int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmPSInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSInvoicesSearch frmPSInvoicesSearch2 = new frmPSInvoicesSearch(PSInvoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSInvoiceID", Approved, Deleted);
		frmPSInvoicesSearch2.MinDate = minDate;
		frmPSInvoicesSearch2.MaxDate = maxDate;
		frmPSInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSInvoicesSearch2.Location = new Point(0, 0);
		frmPSInvoicesSearch2.ShowDialog();
		return frmPSInvoicesSearch2.ID;
	}

	public static DataTable PSInvoicesReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmPSInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSInvoicesSearchReport frmPSInvoicesSearchReport2 = new frmPSInvoicesSearchReport(PSInvoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSInvoiceID", Approved, Deleted);
		frmPSInvoicesSearchReport2.MinDate = minDate;
		frmPSInvoicesSearchReport2.MaxDate = maxDate;
		frmPSInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSInvoicesSearchReport2.Location = new Point(0, 0);
		frmPSInvoicesSearchReport2.ShowDialog();
		return frmPSInvoicesSearchReport2.dtResult;
	}

	public static DataTable PSInvoicesForInvoicesPaymentsReport(int SubAccountID, string BranchIDs, int CurrencyID, int Closed)
	{
		string fullName = typeof(frmPSInvoicesForInvoicesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSInvoicesForInvoicesPaymentsSearchReport frmPSInvoicesForInvoicesPaymentsSearchReport2 = new frmPSInvoicesForInvoicesPaymentsSearchReport(PSInvoices.SearchForInvoicesPayment(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), SubAccountID.ToString(), CurrencyID.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSInvoiceID");
		frmPSInvoicesForInvoicesPaymentsSearchReport2.MinDate = minDate;
		frmPSInvoicesForInvoicesPaymentsSearchReport2.MaxDate = maxDate;
		frmPSInvoicesForInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSInvoicesForInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmPSInvoicesForInvoicesPaymentsSearchReport2.ShowDialog();
		return frmPSInvoicesForInvoicesPaymentsSearchReport2.dtResult;
	}

	public static DataTable PSInvoicesPaymentsSearchReport()
	{
		frmPSInvoicesPaymentsSearchReport frmPSInvoicesPaymentsSearchReport2 = new frmPSInvoicesPaymentsSearchReport(PSInvoicesPayments.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "PaymentID");
		frmPSInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmPSInvoicesPaymentsSearchReport2.ShowDialog();
		return frmPSInvoicesPaymentsSearchReport2.dtResult;
	}

	public static DataTable PSMonthlySharesReport(int Deleted)
	{
		string fullName = typeof(frmPSMonthlySharesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSMonthlySharesSearchReport frmPSMonthlySharesSearchReport2 = new frmPSMonthlySharesSearchReport(PSMonthlyShares.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MonthlyShareID", Deleted);
		frmPSMonthlySharesSearchReport2.MinDate = minDate;
		frmPSMonthlySharesSearchReport2.MaxDate = maxDate;
		frmPSMonthlySharesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSMonthlySharesSearchReport2.Location = new Point(0, 0);
		frmPSMonthlySharesSearchReport2.ShowDialog();
		return frmPSMonthlySharesSearchReport2.dtResult;
	}

	public static DataTable PSInvoicesTruckReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmPSInvoicesTruckSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSInvoicesTruckSearchReport frmPSInvoicesTruckSearchReport2 = new frmPSInvoicesTruckSearchReport(PSInvoices.SearchTruck(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PSInvoiceID", Approved, Deleted);
		frmPSInvoicesTruckSearchReport2.MinDate = minDate;
		frmPSInvoicesTruckSearchReport2.MaxDate = maxDate;
		frmPSInvoicesTruckSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSInvoicesTruckSearchReport2.Location = new Point(0, 0);
		frmPSInvoicesTruckSearchReport2.ShowDialog();
		return frmPSInvoicesTruckSearchReport2.dtResult;
	}

	public static int ExpensesSearch(bool IsFromServer)
	{
		ERP.Purchasing.Search.frmExpensesSearch frmExpensesSearch2 = new ERP.Purchasing.Search.frmExpensesSearch(BusinessLayer.Purchasing.Expenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExpenseID");
		frmExpensesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpensesSearch2.Location = new Point(0, 0);
		frmExpensesSearch2.ShowDialog();
		return frmExpensesSearch2.ID;
	}

	public static DataTable ExpensesReport(bool IsFromServer)
	{
		ERP.Purchasing.Search.frmExpensesSearchReport frmExpensesSearchReport2 = new ERP.Purchasing.Search.frmExpensesSearchReport(BusinessLayer.Purchasing.Expenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExpenseID");
		frmExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpensesSearchReport2.Location = new Point(0, 0);
		frmExpensesSearchReport2.ShowDialog();
		return frmExpensesSearchReport2.dtResult;
	}

	public static DataTable PSSupplierDebitNoteReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmPSSupplierDebitNoteSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPSSupplierDebitNoteSearchReport frmPSSupplierDebitNoteSearchReport2 = new frmPSSupplierDebitNoteSearchReport(PSSuppliersDebitNotes.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SupplierDebitNoteID", Approved, Deleted);
		frmPSSupplierDebitNoteSearchReport2.MinDate = minDate;
		frmPSSupplierDebitNoteSearchReport2.MaxDate = maxDate;
		frmPSSupplierDebitNoteSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPSSupplierDebitNoteSearchReport2.Location = new Point(0, 0);
		frmPSSupplierDebitNoteSearchReport2.ShowDialog();
		return frmPSSupplierDebitNoteSearchReport2.dtResult;
	}

	public static int SLQuotationstSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(ERP.Sales.Search.frmQuotationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Sales.Search.frmQuotationsSearch frmQuotationsSearch2 = new ERP.Sales.Search.frmQuotationsSearch(BusinessLayer.Sales.Quotations.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearch2.MinDate = minDate;
		frmQuotationsSearch2.MaxDate = maxDate;
		frmQuotationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearch2.Location = new Point(0, 0);
		frmQuotationsSearch2.ShowDialog();
		return frmQuotationsSearch2.ID;
	}

	public static DataTable SLQuotationstReport(int Approved, int Deleted)
	{
		string fullName = typeof(ERP.Sales.Search.frmQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Sales.Search.frmQuotationsSearchReport frmQuotationsSearchReport2 = new ERP.Sales.Search.frmQuotationsSearchReport(BusinessLayer.Sales.Quotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearchReport2.MinDate = minDate;
		frmQuotationsSearchReport2.MaxDate = maxDate;
		frmQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearchReport2.Location = new Point(0, 0);
		frmQuotationsSearchReport2.ShowDialog();
		return frmQuotationsSearchReport2.dtResult;
	}

	public static DataTable SLClientCreditNoteReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSLClientCreditNoteSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLClientCreditNoteSearchReport frmSLClientCreditNoteSearchReport2 = new frmSLClientCreditNoteSearchReport(SLClientsCreditNotes.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ClientCreditNoteID", Approved, Deleted);
		frmSLClientCreditNoteSearchReport2.MinDate = minDate;
		frmSLClientCreditNoteSearchReport2.MaxDate = maxDate;
		frmSLClientCreditNoteSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLClientCreditNoteSearchReport2.Location = new Point(0, 0);
		frmSLClientCreditNoteSearchReport2.ShowDialog();
		return frmSLClientCreditNoteSearchReport2.dtResult;
	}

	public static int SLInvoicesSearch(string BranchIDs, int Approved, int Deleted, int Closed, int IsEINV, int WithoutMIV)
	{
		string fullName = typeof(frmSLInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLInvoicesSearch frmSLInvoicesSearch2 = new frmSLInvoicesSearch(SLInvoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsEINV.ToString(), WithoutMIV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmSLInvoicesSearch2.MinDate = minDate;
		frmSLInvoicesSearch2.MaxDate = maxDate;
		frmSLInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesSearch2.Location = new Point(0, 0);
		frmSLInvoicesSearch2.ShowDialog();
		return frmSLInvoicesSearch2.ID;
	}

	public static DataTable SLInvoicesSearchReportForInvoicesGroup(string BranchIDs, int SubAccountID, int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmSLInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLInvoicesSearchReport frmSLInvoicesSearchReport2 = new frmSLInvoicesSearchReport(SLInvoices.SearchForInvoicesGroup(BranchIDs, SubAccountID.ToString(), minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmSLInvoicesSearchReport2.MinDate = minDate;
		frmSLInvoicesSearchReport2.MaxDate = maxDate;
		frmSLInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesSearchReport2.Location = new Point(0, 0);
		frmSLInvoicesSearchReport2.ShowDialog();
		return frmSLInvoicesSearchReport2.dtResult;
	}

	public static DataTable SLReturnsSearchReportForInvoicesGroup(string BranchIDs, int SubAccountID, int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmClientsDepartmentsReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmClientsDepartmentsReturnsSearchReport frmClientsDepartmentsReturnsSearchReport2 = new frmClientsDepartmentsReturnsSearchReport(ClientsDepartmentsReturns.SearchForReturnsGroup(BranchIDs, SubAccountID.ToString(), minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmClientsDepartmentsReturnsSearchReport2.MinDate = minDate;
		frmClientsDepartmentsReturnsSearchReport2.MaxDate = maxDate;
		frmClientsDepartmentsReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClientsDepartmentsReturnsSearchReport2.Location = new Point(0, 0);
		frmClientsDepartmentsReturnsSearchReport2.ShowDialog();
		return frmClientsDepartmentsReturnsSearchReport2.dtResult;
	}

	public static DataTable SLInvoicesReport(int Approved, int Deleted, int Closed, int IsEINV, int WithoutMIV)
	{
		string fullName = typeof(frmSLInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLInvoicesSearchReport frmSLInvoicesSearchReport2 = new frmSLInvoicesSearchReport(SLInvoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsEINV.ToString(), WithoutMIV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmSLInvoicesSearchReport2.MinDate = minDate;
		frmSLInvoicesSearchReport2.MaxDate = maxDate;
		frmSLInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesSearchReport2.Location = new Point(0, 0);
		frmSLInvoicesSearchReport2.ShowDialog();
		return frmSLInvoicesSearchReport2.dtResult;
	}

	public static DataTable SLInvoicesForInvoicesPaymentsReport(int SubAccountID, string BranchIDs, int CurrencyID, int Closed)
	{
		string fullName = typeof(frmSLInvoicesForInvoicesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLInvoicesForInvoicesPaymentsSearchReport frmSLInvoicesForInvoicesPaymentsSearchReport2 = new frmSLInvoicesForInvoicesPaymentsSearchReport(SLInvoices.SearchForInvoicesPayment(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), SubAccountID.ToString(), CurrencyID.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID");
		frmSLInvoicesForInvoicesPaymentsSearchReport2.MinDate = minDate;
		frmSLInvoicesForInvoicesPaymentsSearchReport2.MaxDate = maxDate;
		frmSLInvoicesForInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesForInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmSLInvoicesForInvoicesPaymentsSearchReport2.ShowDialog();
		return frmSLInvoicesForInvoicesPaymentsSearchReport2.dtResult;
	}

	public static DataTable SLInvoicesGroupReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSLInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLInvoicesGroupSearchReport frmSLInvoicesGroupSearchReport2 = new frmSLInvoicesGroupSearchReport(SLInvoicesGroup.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceGroupID", Approved);
		frmSLInvoicesGroupSearchReport2.MinDate = minDate;
		frmSLInvoicesGroupSearchReport2.MaxDate = maxDate;
		frmSLInvoicesGroupSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesGroupSearchReport2.Location = new Point(0, 0);
		frmSLInvoicesGroupSearchReport2.ShowDialog();
		return frmSLInvoicesGroupSearchReport2.dtResult;
	}

	public static DataTable SLInvoicesTruckReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmSLInvoicesTruckSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLInvoicesTruckSearchReport frmSLInvoicesTruckSearchReport2 = new frmSLInvoicesTruckSearchReport(SLInvoices.SearchTruck(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmSLInvoicesTruckSearchReport2.MinDate = minDate;
		frmSLInvoicesTruckSearchReport2.MaxDate = maxDate;
		frmSLInvoicesTruckSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesTruckSearchReport2.Location = new Point(0, 0);
		frmSLInvoicesTruckSearchReport2.ShowDialog();
		return frmSLInvoicesTruckSearchReport2.dtResult;
	}

	public static DataTable SLInvoicesPaymentsSearchReport()
	{
		frmSLInvoicesPaymentsSearchReport frmSLInvoicesPaymentsSearchReport2 = new frmSLInvoicesPaymentsSearchReport(SLInvoicesPayments.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "PaymentID");
		frmSLInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmSLInvoicesPaymentsSearchReport2.ShowDialog();
		return frmSLInvoicesPaymentsSearchReport2.dtResult;
	}

	public static DataTable ClientsPaymentsSearchReport(int Approved, int Deleted)
	{
		frmClientsPaymentsSearchReport frmClientsPaymentsSearchReport2 = new frmClientsPaymentsSearchReport(ClientsPayments.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "ClientPaymentID");
		frmClientsPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClientsPaymentsSearchReport2.Location = new Point(0, 0);
		frmClientsPaymentsSearchReport2.ShowDialog();
		return frmClientsPaymentsSearchReport2.dtResult;
	}

	public static int SLOrdersSearch(string BranchIDs, int Approved, int Deleted, int Closed, int HasSLInvoice)
	{
		string fullName = typeof(frmSLOrdersSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLOrdersSearch frmSLOrdersSearch2 = new frmSLOrdersSearch(SLOrders.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), HasSLInvoice.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLOrderID", Approved, Deleted);
		frmSLOrdersSearch2.MinDate = minDate;
		frmSLOrdersSearch2.MaxDate = maxDate;
		frmSLOrdersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLOrdersSearch2.Location = new Point(0, 0);
		frmSLOrdersSearch2.ShowDialog();
		return frmSLOrdersSearch2.ID;
	}

	public static DataTable SLOrdersReport(int Approved, int Deleted, int Closed, int HasSLInvoice)
	{
		string fullName = typeof(frmSLOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSLOrdersSearchReport frmSLOrdersSearchReport2 = new frmSLOrdersSearchReport(SLOrders.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), HasSLInvoice.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLOrderID", Approved, Deleted);
		frmSLOrdersSearchReport2.MinDate = minDate;
		frmSLOrdersSearchReport2.MaxDate = maxDate;
		frmSLOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSLOrdersSearchReport2.Location = new Point(0, 0);
		frmSLOrdersSearchReport2.ShowDialog();
		return frmSLOrdersSearchReport2.dtResult;
	}

	public static int PaymentMethods(bool IsFromServer)
	{
		frmPaymentMethodsSearch frmPaymentMethodsSearch2 = new frmPaymentMethodsSearch(BusinessLayer.General.PaymentMethods.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PaymentMethodID");
		frmPaymentMethodsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPaymentMethodsSearch2.Location = new Point(0, 0);
		frmPaymentMethodsSearch2.ShowDialog();
		return frmPaymentMethodsSearch2.ID;
	}

	public static DataTable PaymentMethodsReport(bool IsFromServer)
	{
		frmPaymentMethodsSearchReport frmPaymentMethodsSearchReport2 = new frmPaymentMethodsSearchReport(BusinessLayer.General.PaymentMethods.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PaymentMethodID");
		frmPaymentMethodsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPaymentMethodsSearchReport2.Location = new Point(0, 0);
		frmPaymentMethodsSearchReport2.ShowDialog();
		return frmPaymentMethodsSearchReport2.dtResult;
	}

	public static int TaxsSearch(bool IsFromServer)
	{
		frmTaxsSearch frmTaxsSearch2 = new frmTaxsSearch(Taxs.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TaxID");
		frmTaxsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTaxsSearch2.Location = new Point(0, 0);
		frmTaxsSearch2.ShowDialog();
		return frmTaxsSearch2.ID;
	}

	public static DataTable TaxsReport(bool IsFromServer)
	{
		frmTaxsSearchReport frmTaxsSearchReport2 = new frmTaxsSearchReport(Taxs.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TaxID");
		frmTaxsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTaxsSearchReport2.Location = new Point(0, 0);
		frmTaxsSearchReport2.ShowDialog();
		return frmTaxsSearchReport2.dtResult;
	}

	public static int Departments(bool IsFromServer)
	{
		frmDepartmentSearch frmDepartmentSearch2 = new frmDepartmentSearch(BusinessLayer.General.Departments.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DepartmentID");
		frmDepartmentSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDepartmentSearch2.Location = new Point(0, 0);
		frmDepartmentSearch2.ShowDialog();
		return frmDepartmentSearch2.ID;
	}

	public static DataTable DepartmentsReport(bool IsFromServer)
	{
		frmDepartmentSearchReport frmDepartmentSearchReport2 = new frmDepartmentSearchReport(BusinessLayer.General.Departments.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DepartmentID");
		frmDepartmentSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDepartmentSearchReport2.Location = new Point(0, 0);
		frmDepartmentSearchReport2.ShowDialog();
		return frmDepartmentSearchReport2.dtResult;
	}

	public static int Colors(bool IsFromServer)
	{
		frmColorsSearch frmColorsSearch2 = new frmColorsSearch(BusinessLayer.General.Colors.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ColorID");
		frmColorsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmColorsSearch2.Location = new Point(0, 0);
		frmColorsSearch2.ShowDialog();
		return frmColorsSearch2.ID;
	}

	public static DataTable GLinesReport(bool IsFromServer)
	{
		ERP.SystemOptions.Search.frmLinesSearchReport frmLinesSearchReport2 = new ERP.SystemOptions.Search.frmLinesSearchReport(BusinessLayer.General.Lines.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LineID");
		frmLinesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLinesSearchReport2.Location = new Point(0, 0);
		frmLinesSearchReport2.ShowDialog();
		return frmLinesSearchReport2.dtResult;
	}

	public static int GLines(bool IsFromServer)
	{
		ERP.SystemOptions.Search.frmLinesSearch frmLinesSearch2 = new ERP.SystemOptions.Search.frmLinesSearch(BusinessLayer.General.Lines.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LineID");
		frmLinesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLinesSearch2.Location = new Point(0, 0);
		frmLinesSearch2.ShowDialog();
		return frmLinesSearch2.ID;
	}

	public static int Items(string IsItem, string IsRecipe, string IsService, string IsSalesItem, string IsActive, string IsDirectItem, string IsProductionItem, string EnforceBatchNo, string ItemTypeID, string HideFromReports, bool IsFromServer)
	{
		frmItemsSearch frmItemsSearch2 = new frmItemsSearch(BusinessLayer.StockControl.Items.Search(IsItem, IsRecipe, IsService, IsSalesItem, IsActive, IsDirectItem, IsProductionItem, EnforceBatchNo, ItemTypeID, HideFromReports, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemID");
		frmItemsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsSearch2.Location = new Point(0, 0);
		frmItemsSearch2.ShowDialog();
		return frmItemsSearch2.ID;
	}

	public static DataTable ItemsReport(string IsItem, string IsRecipe, string IsService, string IsSalesItem, string IsActive, string IsDirectItem, string IsProductionItem, string EnforceBatchNo, string ItemTypeID, string HideFromReports, bool IsFromServer)
	{
		frmItemsSearchReport frmItemsSearchReport2 = new frmItemsSearchReport(BusinessLayer.StockControl.Items.Search(IsItem, IsRecipe, IsService, IsSalesItem, IsActive, IsDirectItem, IsProductionItem, EnforceBatchNo, ItemTypeID, HideFromReports, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemID");
		frmItemsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsSearchReport2.Location = new Point(0, 0);
		frmItemsSearchReport2.ShowDialog();
		return frmItemsSearchReport2.dtResult;
	}

	public static DataTable PriceListsReport(bool IsFromServer)
	{
		frmPriceListsSearchReport frmPriceListsSearchReport2 = new frmPriceListsSearchReport(PriceLists.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PriceListID");
		frmPriceListsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPriceListsSearchReport2.Location = new Point(0, 0);
		frmPriceListsSearchReport2.ShowDialog();
		return frmPriceListsSearchReport2.dtResult;
	}

	public static DataTable ItemsReportByIDs(string ItemIDs, bool IsFromServer)
	{
		frmItemsSearchReport frmItemsSearchReport2 = new frmItemsSearchReport(BusinessLayer.StockControl.Items.SearchByIDs(ItemIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemID");
		frmItemsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsSearchReport2.Location = new Point(0, 0);
		frmItemsSearchReport2.ShowDialog();
		return frmItemsSearchReport2.dtResult;
	}

	public static DataTable ItemsReportWH(bool IsFromServer)
	{
		frmItemsSearchReport frmItemsSearchReport2 = new frmItemsSearchReport(BusinessLayer.StockControl.Items.SearchWH(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemID");
		frmItemsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsSearchReport2.Location = new Point(0, 0);
		frmItemsSearchReport2.ShowDialog();
		return frmItemsSearchReport2.dtResult;
	}

	public static int ItemsBatchesSearch(int ItemID, int BatchID, bool IsFromServer)
	{
		frmItemsBatchesSearch frmItemsBatchesSearch2 = new frmItemsBatchesSearch(ItemsBatches.Search(ItemID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BatchID", BatchID);
		frmItemsBatchesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsBatchesSearch2.Location = new Point(0, 0);
		frmItemsBatchesSearch2.ShowDialog();
		return frmItemsBatchesSearch2.ID;
	}

	public static DataTable StoresReport(bool IsFromServer)
	{
		frmStoresSearchReport frmStoresSearchReport2 = new frmStoresSearchReport(BusinessLayer.StockControl.Stores.Search("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "StoreID");
		frmStoresSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoresSearchReport2.Location = new Point(0, 0);
		frmStoresSearchReport2.ShowDialog();
		return frmStoresSearchReport2.dtResult;
	}

	public static int Stores(bool IsFromServer)
	{
		frmStoresSearch frmStoresSearch2 = new frmStoresSearch(BusinessLayer.StockControl.Stores.Search("," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "StoreID");
		frmStoresSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoresSearch2.Location = new Point(0, 0);
		frmStoresSearch2.ShowDialog();
		return frmStoresSearch2.ID;
	}

	public static int AllStores(bool IsFromServer)
	{
		frmStoresSearch frmStoresSearch2 = new frmStoresSearch(BusinessLayer.StockControl.Stores.Search("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "StoreID");
		frmStoresSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoresSearch2.Location = new Point(0, 0);
		frmStoresSearch2.ShowDialog();
		return frmStoresSearch2.ID;
	}

	public static int PriceTypes(bool IsFromServer)
	{
		frmPricesTypesSearch frmPricesTypesSearch2 = new frmPricesTypesSearch(PricesTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PriceTypeID");
		frmPricesTypesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPricesTypesSearch2.Location = new Point(0, 0);
		frmPricesTypesSearch2.ShowDialog();
		return frmPricesTypesSearch2.ID;
	}

	public static DataTable OpeningBalancesReport(int Approved)
	{
		string fullName = typeof(frmOpeningBalancesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmOpeningBalancesSearchReport frmOpeningBalancesSearchReport2 = new frmOpeningBalancesSearchReport(OpeningBalances.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "OpeningBalanceID");
		frmOpeningBalancesSearchReport2.MinDate = minDate;
		frmOpeningBalancesSearchReport2.MaxDate = maxDate;
		frmOpeningBalancesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOpeningBalancesSearchReport2.Location = new Point(0, 0);
		frmOpeningBalancesSearchReport2.ShowDialog();
		return frmOpeningBalancesSearchReport2.dtResult;
	}

	public static DataTable OpeningBalancesReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved)
	{
		frmOpeningBalancesSearchReport frmOpeningBalancesSearchReport2 = new frmOpeningBalancesSearchReport(OpeningBalances.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "OpeningBalanceID");
		frmOpeningBalancesSearchReport2.MinDate = FromDate;
		frmOpeningBalancesSearchReport2.MaxDate = ToDate;
		frmOpeningBalancesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOpeningBalancesSearchReport2.Location = new Point(0, 0);
		frmOpeningBalancesSearchReport2.ShowDialog();
		return frmOpeningBalancesSearchReport2.dtResult;
	}

	public static int GoodReceiptNotesSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmGoodReceiptNotesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmGoodReceiptNotesSearch frmGoodReceiptNotesSearch2 = new frmGoodReceiptNotesSearch(GoodReceiptNotes.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "GoodReceiptNoteID");
		frmGoodReceiptNotesSearch2.MinDate = minDate;
		frmGoodReceiptNotesSearch2.MaxDate = maxDate;
		frmGoodReceiptNotesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmGoodReceiptNotesSearch2.Location = new Point(0, 0);
		frmGoodReceiptNotesSearch2.ShowDialog();
		return frmGoodReceiptNotesSearch2.ID;
	}

	public static DataTable GoodReceiptNotesReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmGoodReceiptNotesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmGoodReceiptNotesSearchReport frmGoodReceiptNotesSearchReport2 = new frmGoodReceiptNotesSearchReport(GoodReceiptNotes.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "GoodReceiptNoteID");
		frmGoodReceiptNotesSearchReport2.MinDate = minDate;
		frmGoodReceiptNotesSearchReport2.MaxDate = maxDate;
		frmGoodReceiptNotesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmGoodReceiptNotesSearchReport2.Location = new Point(0, 0);
		frmGoodReceiptNotesSearchReport2.ShowDialog();
		return frmGoodReceiptNotesSearchReport2.dtResult;
	}

	public static DataTable GoodReceiptNotesReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted)
	{
		frmGoodReceiptNotesSearchReport frmGoodReceiptNotesSearchReport2 = new frmGoodReceiptNotesSearchReport(GoodReceiptNotes.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "GoodReceiptNoteID");
		frmGoodReceiptNotesSearchReport2.MinDate = FromDate;
		frmGoodReceiptNotesSearchReport2.MaxDate = ToDate;
		frmGoodReceiptNotesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmGoodReceiptNotesSearchReport2.Location = new Point(0, 0);
		frmGoodReceiptNotesSearchReport2.ShowDialog();
		return frmGoodReceiptNotesSearchReport2.dtResult;
	}

	public static DataTable GoodReceiptNotesReportBySupplierID(string SupplierID)
	{
		string fullName = typeof(frmGoodReceiptNotesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmGoodReceiptNotesSearchReport frmGoodReceiptNotesSearchReport2 = new frmGoodReceiptNotesSearchReport(GoodReceiptNotes.SearchBySupplierID(SupplierID, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0"), "GoodReceiptNoteID");
		frmGoodReceiptNotesSearchReport2.MinDate = minDate;
		frmGoodReceiptNotesSearchReport2.MaxDate = maxDate;
		frmGoodReceiptNotesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmGoodReceiptNotesSearchReport2.Location = new Point(0, 0);
		frmGoodReceiptNotesSearchReport2.ShowDialog();
		return frmGoodReceiptNotesSearchReport2.dtResult;
	}

	public static int MaterialIssueRequestSearch(string BranchIDs, int Approved, int Deleted, int Completed)
	{
		string fullName = typeof(frmMaterialIssueRequestSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmMaterialIssueRequestSearch frmMaterialIssueRequestSearch2 = new frmMaterialIssueRequestSearch(MaterialIssueRequest.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Completed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MaterialIssueRequestID", Approved, Deleted);
		frmMaterialIssueRequestSearch2.MinDate = minDate;
		frmMaterialIssueRequestSearch2.MaxDate = maxDate;
		frmMaterialIssueRequestSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMaterialIssueRequestSearch2.Location = new Point(0, 0);
		frmMaterialIssueRequestSearch2.ShowDialog();
		return frmMaterialIssueRequestSearch2.ID;
	}

	public static DataTable MaterialIssueRequestReport(int Approved, int Deleted, int Completed)
	{
		string fullName = typeof(frmMaterialIssueRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmMaterialIssueRequestSearchReport frmMaterialIssueRequestSearchReport2 = new frmMaterialIssueRequestSearchReport(MaterialIssueRequest.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Completed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MaterialIssueRequestID", Approved, Deleted);
		frmMaterialIssueRequestSearchReport2.MinDate = minDate;
		frmMaterialIssueRequestSearchReport2.MaxDate = maxDate;
		frmMaterialIssueRequestSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMaterialIssueRequestSearchReport2.Location = new Point(0, 0);
		frmMaterialIssueRequestSearchReport2.ShowDialog();
		return frmMaterialIssueRequestSearchReport2.dtResult;
	}

	public static int MaterialIssueVouchersSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmMaterialIssueVouchersSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmMaterialIssueVouchersSearch frmMaterialIssueVouchersSearch2 = new frmMaterialIssueVouchersSearch(MaterialIssueVouchers.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MaterialIssueVoucherID");
		frmMaterialIssueVouchersSearch2.MinDate = minDate;
		frmMaterialIssueVouchersSearch2.MaxDate = maxDate;
		frmMaterialIssueVouchersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMaterialIssueVouchersSearch2.Location = new Point(0, 0);
		frmMaterialIssueVouchersSearch2.ShowDialog();
		return frmMaterialIssueVouchersSearch2.ID;
	}

	public static DataTable MaterialIssueVouchersReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmMaterialIssueVouchersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmMaterialIssueVouchersSearchReport frmMaterialIssueVouchersSearchReport2 = new frmMaterialIssueVouchersSearchReport(MaterialIssueVouchers.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MaterialIssueVoucherID");
		frmMaterialIssueVouchersSearchReport2.MinDate = minDate;
		frmMaterialIssueVouchersSearchReport2.MaxDate = maxDate;
		frmMaterialIssueVouchersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMaterialIssueVouchersSearchReport2.Location = new Point(0, 0);
		frmMaterialIssueVouchersSearchReport2.ShowDialog();
		return frmMaterialIssueVouchersSearchReport2.dtResult;
	}

	public static DataTable MaterialIssueVouchersReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted)
	{
		frmMaterialIssueVouchersSearchReport frmMaterialIssueVouchersSearchReport2 = new frmMaterialIssueVouchersSearchReport(MaterialIssueVouchers.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MaterialIssueVoucherID");
		frmMaterialIssueVouchersSearchReport2.MinDate = FromDate;
		frmMaterialIssueVouchersSearchReport2.MaxDate = ToDate;
		frmMaterialIssueVouchersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMaterialIssueVouchersSearchReport2.Location = new Point(0, 0);
		frmMaterialIssueVouchersSearchReport2.ShowDialog();
		return frmMaterialIssueVouchersSearchReport2.dtResult;
	}

	public static DataTable StoresSettlementVouchersReport(int IsExchange, int Approved, int Deleted)
	{
		string fullName = typeof(frmStoresSettlementVouchersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoresSettlementVouchersSearchReport frmStoresSettlementVouchersSearchReport2 = new frmStoresSettlementVouchersSearchReport(StoresSettlementVouchers.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), IsExchange.ToString(), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SettlementVoucherID");
		frmStoresSettlementVouchersSearchReport2.MinDate = minDate;
		frmStoresSettlementVouchersSearchReport2.MaxDate = maxDate;
		frmStoresSettlementVouchersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoresSettlementVouchersSearchReport2.Location = new Point(0, 0);
		frmStoresSettlementVouchersSearchReport2.ShowDialog();
		return frmStoresSettlementVouchersSearchReport2.dtResult;
	}

	public static DataTable StoresSettlementVouchersReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int IsExchange, int Approved, int Deleted)
	{
		frmStoresSettlementVouchersSearchReport frmStoresSettlementVouchersSearchReport2 = new frmStoresSettlementVouchersSearchReport(StoresSettlementVouchers.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), IsExchange.ToString(), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SettlementVoucherID");
		frmStoresSettlementVouchersSearchReport2.MinDate = FromDate;
		frmStoresSettlementVouchersSearchReport2.MaxDate = ToDate;
		frmStoresSettlementVouchersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoresSettlementVouchersSearchReport2.Location = new Point(0, 0);
		frmStoresSettlementVouchersSearchReport2.ShowDialog();
		return frmStoresSettlementVouchersSearchReport2.dtResult;
	}

	public static DataTable StoreTakingsReport(string StoreIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmStoreTakingsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoreTakingsSearchReport frmStoreTakingsSearchReport2 = new frmStoreTakingsSearchReport(StoreTakings.Search(StoreIDs, GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "StoreTakingID");
		frmStoreTakingsSearchReport2.MinDate = minDate;
		frmStoreTakingsSearchReport2.MaxDate = maxDate;
		frmStoreTakingsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTakingsSearchReport2.Location = new Point(0, 0);
		frmStoreTakingsSearchReport2.ShowDialog();
		return frmStoreTakingsSearchReport2.dtResult;
	}

	public static DataTable StoreTakingsReport(string StoreIDs, string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted)
	{
		frmStoreTakingsSearchReport frmStoreTakingsSearchReport2 = new frmStoreTakingsSearchReport(StoreTakings.Search(StoreIDs, BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "StoreTakingID");
		frmStoreTakingsSearchReport2.MinDate = FromDate;
		frmStoreTakingsSearchReport2.MaxDate = ToDate;
		frmStoreTakingsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTakingsSearchReport2.Location = new Point(0, 0);
		frmStoreTakingsSearchReport2.ShowDialog();
		return frmStoreTakingsSearchReport2.dtResult;
	}

	public static DataTable StoreRevaluationsReport(string StoreIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmStoreRevaluationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoreRevaluationsSearchReport frmStoreRevaluationsSearchReport2 = new frmStoreRevaluationsSearchReport(StoreRevaluationVouchers.Search(StoreIDs, GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "StoreRevaluationVoucherID");
		frmStoreRevaluationsSearchReport2.MinDate = minDate;
		frmStoreRevaluationsSearchReport2.MaxDate = maxDate;
		frmStoreRevaluationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreRevaluationsSearchReport2.Location = new Point(0, 0);
		frmStoreRevaluationsSearchReport2.ShowDialog();
		return frmStoreRevaluationsSearchReport2.dtResult;
	}

	public static DataTable StoreRevaluationsReport(string StoreIDs, string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted)
	{
		frmStoreRevaluationsSearchReport frmStoreRevaluationsSearchReport2 = new frmStoreRevaluationsSearchReport(StoreRevaluationVouchers.Search(StoreIDs, BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "StoreRevaluationVoucherID");
		frmStoreRevaluationsSearchReport2.MinDate = FromDate;
		frmStoreRevaluationsSearchReport2.MaxDate = ToDate;
		frmStoreRevaluationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreRevaluationsSearchReport2.Location = new Point(0, 0);
		frmStoreRevaluationsSearchReport2.ShowDialog();
		return frmStoreRevaluationsSearchReport2.dtResult;
	}

	public static int StoreTransferVouchersLnsSearch(bool FromServer)
	{
		string fullName = typeof(frmStoreTransferVouchersSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoreTransferVouchersSearch frmStoreTransferVouchersSearch2 = new frmStoreTransferVouchersSearch(StoreTransferVouchers.SearchLnsInvoice(GlobalVariables.CurrentBranchID, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", FromServer), "StoreTransferVoucherID");
		frmStoreTransferVouchersSearch2.MinDate = minDate;
		frmStoreTransferVouchersSearch2.MaxDate = maxDate;
		frmStoreTransferVouchersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTransferVouchersSearch2.Location = new Point(0, 0);
		frmStoreTransferVouchersSearch2.ShowDialog();
		return frmStoreTransferVouchersSearch2.ID;
	}

	public static DataTable StoreTransferVouchersReport(string StoreIDs, int Approved, int Deleted, bool FromServer)
	{
		string fullName = typeof(frmStoreTransferVouchersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoreTransferVouchersSearchReport frmStoreTransferVouchersSearchReport2 = new frmStoreTransferVouchersSearchReport(StoreTransferVouchers.Search(GlobalVariables.BranchIDs, StoreIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", FromServer), "StoreTransferVoucherID");
		frmStoreTransferVouchersSearchReport2.MinDate = minDate;
		frmStoreTransferVouchersSearchReport2.MaxDate = maxDate;
		frmStoreTransferVouchersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTransferVouchersSearchReport2.Location = new Point(0, 0);
		frmStoreTransferVouchersSearchReport2.ShowDialog();
		return frmStoreTransferVouchersSearchReport2.dtResult;
	}

	public static DataTable StoreTransferVouchersReport(string BranchIDs, string StoreIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted, bool FromServer)
	{
		frmStoreTransferVouchersSearchReport frmStoreTransferVouchersSearchReport2 = new frmStoreTransferVouchersSearchReport(StoreTransferVouchers.Search(BranchIDs, StoreIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", FromServer), "StoreTransferVoucherID");
		frmStoreTransferVouchersSearchReport2.MinDate = FromDate;
		frmStoreTransferVouchersSearchReport2.MaxDate = ToDate;
		frmStoreTransferVouchersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTransferVouchersSearchReport2.Location = new Point(0, 0);
		frmStoreTransferVouchersSearchReport2.ShowDialog();
		return frmStoreTransferVouchersSearchReport2.dtResult;
	}

	public static int StoreTransferRequestSearch(string StoreIDs, string VoucherBranchIDs, string SourceStoreBranchIDs, int Approved, int Deleted, int Completed)
	{
		string fullName = typeof(frmStoreTransferRequestSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoreTransferRequestSearch frmStoreTransferRequestSearch2 = new frmStoreTransferRequestSearch(StoreTransferRequest.Search(StoreIDs, VoucherBranchIDs, SourceStoreBranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Completed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "StoreTransferRequestID");
		frmStoreTransferRequestSearch2.MinDate = minDate;
		frmStoreTransferRequestSearch2.MaxDate = maxDate;
		frmStoreTransferRequestSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTransferRequestSearch2.Location = new Point(0, 0);
		frmStoreTransferRequestSearch2.ShowDialog();
		return frmStoreTransferRequestSearch2.ID;
	}

	public static DataTable StoreTransferRequestReport(string ToStoreIDs, string SourceStoreBranchIDs, int Approved, int Deleted, int Completed)
	{
		string fullName = typeof(frmStoreTransferRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmStoreTransferRequestSearchReport frmStoreTransferRequestSearchReport2 = new frmStoreTransferRequestSearchReport(StoreTransferRequest.Search(ToStoreIDs, GlobalVariables.BranchIDs, SourceStoreBranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Completed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "StoreTransferRequestID");
		frmStoreTransferRequestSearchReport2.MinDate = minDate;
		frmStoreTransferRequestSearchReport2.MaxDate = maxDate;
		frmStoreTransferRequestSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStoreTransferRequestSearchReport2.Location = new Point(0, 0);
		frmStoreTransferRequestSearchReport2.ShowDialog();
		return frmStoreTransferRequestSearchReport2.dtResult;
	}

	public static DataTable SuppliersReturnsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSuppliersReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSuppliersReturnsSearchReport frmSuppliersReturnsSearchReport2 = new frmSuppliersReturnsSearchReport(SuppliersReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SupplierReturnID");
		frmSuppliersReturnsSearchReport2.MinDate = minDate;
		frmSuppliersReturnsSearchReport2.MaxDate = maxDate;
		frmSuppliersReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSuppliersReturnsSearchReport2.Location = new Point(0, 0);
		frmSuppliersReturnsSearchReport2.ShowDialog();
		return frmSuppliersReturnsSearchReport2.dtResult;
	}

	public static DataTable SuppliersReturnsReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted)
	{
		frmSuppliersReturnsSearchReport frmSuppliersReturnsSearchReport2 = new frmSuppliersReturnsSearchReport(SuppliersReturns.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SupplierReturnID");
		frmSuppliersReturnsSearchReport2.MinDate = FromDate;
		frmSuppliersReturnsSearchReport2.MaxDate = ToDate;
		frmSuppliersReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSuppliersReturnsSearchReport2.Location = new Point(0, 0);
		frmSuppliersReturnsSearchReport2.ShowDialog();
		return frmSuppliersReturnsSearchReport2.dtResult;
	}

	public static DataTable ClientsDepartmentsReturnsReport(int Approved, int Deleted, int IsEINV)
	{
		string fullName = typeof(frmClientsDepartmentsReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmClientsDepartmentsReturnsSearchReport frmClientsDepartmentsReturnsSearchReport2 = new frmClientsDepartmentsReturnsSearchReport(ClientsDepartmentsReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmClientsDepartmentsReturnsSearchReport2.MinDate = minDate;
		frmClientsDepartmentsReturnsSearchReport2.MaxDate = maxDate;
		frmClientsDepartmentsReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClientsDepartmentsReturnsSearchReport2.Location = new Point(0, 0);
		frmClientsDepartmentsReturnsSearchReport2.ShowDialog();
		return frmClientsDepartmentsReturnsSearchReport2.dtResult;
	}

	public static DataTable ClientsDepartmentsReturnsReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted, int IsEINV)
	{
		frmClientsDepartmentsReturnsSearchReport frmClientsDepartmentsReturnsSearchReport2 = new frmClientsDepartmentsReturnsSearchReport(ClientsDepartmentsReturns.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmClientsDepartmentsReturnsSearchReport2.MinDate = FromDate;
		frmClientsDepartmentsReturnsSearchReport2.MaxDate = ToDate;
		frmClientsDepartmentsReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClientsDepartmentsReturnsSearchReport2.Location = new Point(0, 0);
		frmClientsDepartmentsReturnsSearchReport2.ShowDialog();
		return frmClientsDepartmentsReturnsSearchReport2.dtResult;
	}

	public static DataTable SlicingReport(int Approved)
	{
		string fullName = typeof(frmSlicingSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSlicingSearchReport frmSlicingSearchReport2 = new frmSlicingSearchReport(Slicing.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SlicingID");
		frmSlicingSearchReport2.MinDate = minDate;
		frmSlicingSearchReport2.MaxDate = maxDate;
		frmSlicingSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSlicingSearchReport2.Location = new Point(0, 0);
		frmSlicingSearchReport2.ShowDialog();
		return frmSlicingSearchReport2.dtResult;
	}

	public static DataTable SlicingReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved)
	{
		frmSlicingSearchReport frmSlicingSearchReport2 = new frmSlicingSearchReport(Slicing.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SlicingID");
		frmSlicingSearchReport2.MinDate = FromDate;
		frmSlicingSearchReport2.MaxDate = ToDate;
		frmSlicingSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSlicingSearchReport2.Location = new Point(0, 0);
		frmSlicingSearchReport2.ShowDialog();
		return frmSlicingSearchReport2.dtResult;
	}

	public static DataTable RecipeManufacturingReport(int Approved)
	{
		string fullName = typeof(frmRecipeManufacturingSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmRecipeManufacturingSearchReport frmRecipeManufacturingSearchReport2 = new frmRecipeManufacturingSearchReport(RecipeManufacturing.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "RecipeManufacturingID");
		frmRecipeManufacturingSearchReport2.MinDate = minDate;
		frmRecipeManufacturingSearchReport2.MaxDate = maxDate;
		frmRecipeManufacturingSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmRecipeManufacturingSearchReport2.Location = new Point(0, 0);
		frmRecipeManufacturingSearchReport2.ShowDialog();
		return frmRecipeManufacturingSearchReport2.dtResult;
	}

	public static DataTable RecipeManufacturingReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved)
	{
		frmRecipeManufacturingSearchReport frmRecipeManufacturingSearchReport2 = new frmRecipeManufacturingSearchReport(RecipeManufacturing.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "RecipeManufacturingID");
		frmRecipeManufacturingSearchReport2.MinDate = FromDate;
		frmRecipeManufacturingSearchReport2.MaxDate = ToDate;
		frmRecipeManufacturingSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmRecipeManufacturingSearchReport2.Location = new Point(0, 0);
		frmRecipeManufacturingSearchReport2.ShowDialog();
		return frmRecipeManufacturingSearchReport2.dtResult;
	}

	public static DataTable ItemsDiscountReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmItemsDiscountSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmItemsDiscountSearchReport frmItemsDiscountSearchReport2 = new frmItemsDiscountSearchReport(ItemsDiscount.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ItemDiscountID");
		frmItemsDiscountSearchReport2.MinDate = minDate;
		frmItemsDiscountSearchReport2.MaxDate = maxDate;
		frmItemsDiscountSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsDiscountSearchReport2.Location = new Point(0, 0);
		frmItemsDiscountSearchReport2.ShowDialog();
		return frmItemsDiscountSearchReport2.dtResult;
	}

	public static int LnsInvoicesSearch(string BranchIDs, int Approved, int Deleted, int Closed, int IsEINV = 0)
	{
		string fullName = typeof(frmLnsInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLnsInvoicesSearch frmLnsInvoicesSearch2 = new frmLnsInvoicesSearch(BusinessLayer.Lenses.Invoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoiceID", Approved, Deleted);
		frmLnsInvoicesSearch2.MinDate = minDate;
		frmLnsInvoicesSearch2.MaxDate = maxDate;
		frmLnsInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsInvoicesSearch2.Location = new Point(0, 0);
		frmLnsInvoicesSearch2.ShowDialog();
		return frmLnsInvoicesSearch2.ID;
	}

	public static DataTable LnsInvoicesReport(int Approved, int Deleted, int Closed, int IsEINV = 0)
	{
		string fullName = typeof(frmLnsInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLnsInvoicesSearchReport frmLnsInvoicesSearchReport2 = new frmLnsInvoicesSearchReport(BusinessLayer.Lenses.Invoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoiceID", Approved, Deleted);
		frmLnsInvoicesSearchReport2.MinDate = minDate;
		frmLnsInvoicesSearchReport2.MaxDate = maxDate;
		frmLnsInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsInvoicesSearchReport2.Location = new Point(0, 0);
		frmLnsInvoicesSearchReport2.ShowDialog();
		return frmLnsInvoicesSearchReport2.dtResult;
	}

	public static int LnsLabOrdersSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmLnsLabOrdersSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLnsLabOrdersSearch frmLnsLabOrdersSearch2 = new frmLnsLabOrdersSearch(LabOrders.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "LabOrderID", Approved, Deleted);
		frmLnsLabOrdersSearch2.MinDate = minDate;
		frmLnsLabOrdersSearch2.MaxDate = maxDate;
		frmLnsLabOrdersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsLabOrdersSearch2.Location = new Point(0, 0);
		frmLnsLabOrdersSearch2.ShowDialog();
		return frmLnsLabOrdersSearch2.ID;
	}

	public static DataTable LnsLabOrdersReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmLnsLabOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLnsLabOrdersSearchReport frmLnsLabOrdersSearchReport2 = new frmLnsLabOrdersSearchReport(LabOrders.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "LabOrderID", Approved, Deleted);
		frmLnsLabOrdersSearchReport2.MinDate = minDate;
		frmLnsLabOrdersSearchReport2.MaxDate = maxDate;
		frmLnsLabOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsLabOrdersSearchReport2.Location = new Point(0, 0);
		frmLnsLabOrdersSearchReport2.ShowDialog();
		return frmLnsLabOrdersSearchReport2.dtResult;
	}

	public static int TruckNumbers(bool IsFromServer)
	{
		frmTruckNumbersSearch frmTruckNumbersSearch2 = new frmTruckNumbersSearch(TrucksNumber.Search("," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TruckNumberID");
		frmTruckNumbersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTruckNumbersSearch2.Location = new Point(0, 0);
		frmTruckNumbersSearch2.ShowDialog();
		return frmTruckNumbersSearch2.ID;
	}

	public static int SubTruckNumbers(bool IsFromServer)
	{
		frmTruckNumbersSearch frmTruckNumbersSearch2 = new frmTruckNumbersSearch(SubTrucksNumber.Search("," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubTruckNumberID");
		frmTruckNumbersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTruckNumbersSearch2.Location = new Point(0, 0);
		frmTruckNumbersSearch2.ShowDialog();
		return frmTruckNumbersSearch2.ID;
	}

	public static DataTable LnsInvoicesReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted, int Closed, int IsEINV = 0)
	{
		frmLnsInvoicesSearchReport frmLnsInvoicesSearchReport2 = new frmLnsInvoicesSearchReport(BusinessLayer.Lenses.Invoices.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoiceID", Approved, Deleted);
		frmLnsInvoicesSearchReport2.MinDate = FromDate;
		frmLnsInvoicesSearchReport2.MaxDate = ToDate;
		frmLnsInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsInvoicesSearchReport2.Location = new Point(0, 0);
		frmLnsInvoicesSearchReport2.ShowDialog();
		return frmLnsInvoicesSearchReport2.dtResult;
	}

	public static DataTable LnsInvoicesReturnsReport(int Approved, int Deleted, int IsEINV = 0)
	{
		string fullName = typeof(frmLnsInvoicesReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLnsInvoicesReturnsSearchReport frmLnsInvoicesReturnsSearchReport2 = new frmLnsInvoicesReturnsSearchReport(InvoicesReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmLnsInvoicesReturnsSearchReport2.MinDate = minDate;
		frmLnsInvoicesReturnsSearchReport2.MaxDate = maxDate;
		frmLnsInvoicesReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsInvoicesReturnsSearchReport2.Location = new Point(0, 0);
		frmLnsInvoicesReturnsSearchReport2.ShowDialog();
		return frmLnsInvoicesReturnsSearchReport2.dtResult;
	}

	public static DataTable LnsLabordersReturnsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmLabOrdersReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLabOrdersReturnsSearchReport frmLabOrdersReturnsSearchReport2 = new frmLabOrdersReturnsSearchReport(LabOrdersReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmLabOrdersReturnsSearchReport2.MinDate = minDate;
		frmLabOrdersReturnsSearchReport2.MaxDate = maxDate;
		frmLabOrdersReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLabOrdersReturnsSearchReport2.Location = new Point(0, 0);
		frmLabOrdersReturnsSearchReport2.ShowDialog();
		return frmLabOrdersReturnsSearchReport2.dtResult;
	}

	public static DataTable LnsInvoicesReturnsReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted, int IsEINV = 0)
	{
		frmLnsInvoicesReturnsSearchReport frmLnsInvoicesReturnsSearchReport2 = new frmLnsInvoicesReturnsSearchReport(InvoicesReturns.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), IsEINV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmLnsInvoicesReturnsSearchReport2.MinDate = FromDate;
		frmLnsInvoicesReturnsSearchReport2.MaxDate = ToDate;
		frmLnsInvoicesReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsInvoicesReturnsSearchReport2.Location = new Point(0, 0);
		frmLnsInvoicesReturnsSearchReport2.ShowDialog();
		return frmLnsInvoicesReturnsSearchReport2.dtResult;
	}

	public static DataTable LnsExpensesReport(int Approved, int Deleted)
	{
		string fullName = typeof(ERP.Lenses.Search.frmExpensesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Lenses.Search.frmExpensesSearchReport frmExpensesSearchReport2 = new ERP.Lenses.Search.frmExpensesSearchReport(BusinessLayer.Lenses.Expenses.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ExpenseID", Approved, Deleted);
		frmExpensesSearchReport2.MinDate = minDate;
		frmExpensesSearchReport2.MaxDate = maxDate;
		frmExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpensesSearchReport2.Location = new Point(0, 0);
		frmExpensesSearchReport2.ShowDialog();
		return frmExpensesSearchReport2.dtResult;
	}

	public static DataTable LnsRevenuesReport(int IsOtherRevenue, int Approved, int Deleted)
	{
		string fullName = typeof(frmRevenuesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmRevenuesSearchReport frmRevenuesSearchReport2 = new frmRevenuesSearchReport(Revenues.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), IsOtherRevenue.ToString(), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "RevenueID", Approved, Deleted);
		frmRevenuesSearchReport2.MinDate = minDate;
		frmRevenuesSearchReport2.MaxDate = maxDate;
		frmRevenuesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmRevenuesSearchReport2.Location = new Point(0, 0);
		frmRevenuesSearchReport2.ShowDialog();
		return frmRevenuesSearchReport2.dtResult;
	}

	public static DataTable LnsVisaTypesPaymentsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmVisaTypesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmVisaTypesPaymentsSearchReport frmVisaTypesPaymentsSearchReport2 = new frmVisaTypesPaymentsSearchReport(VisaTypesPayments.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "VisaTypePaymentID", Approved, Deleted);
		frmVisaTypesPaymentsSearchReport2.MinDate = minDate;
		frmVisaTypesPaymentsSearchReport2.MaxDate = maxDate;
		frmVisaTypesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVisaTypesPaymentsSearchReport2.Location = new Point(0, 0);
		frmVisaTypesPaymentsSearchReport2.ShowDialog();
		return frmVisaTypesPaymentsSearchReport2.dtResult;
	}

	public static DataTable LnsVisaTypesReturnsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmVisaTypesReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmVisaTypesReturnsSearchReport frmVisaTypesReturnsSearchReport2 = new frmVisaTypesReturnsSearchReport(VisaTypesReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "VisaTypeReturnID", Approved, Deleted);
		frmVisaTypesReturnsSearchReport2.MinDate = minDate;
		frmVisaTypesReturnsSearchReport2.MaxDate = maxDate;
		frmVisaTypesReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVisaTypesReturnsSearchReport2.Location = new Point(0, 0);
		frmVisaTypesReturnsSearchReport2.ShowDialog();
		return frmVisaTypesReturnsSearchReport2.dtResult;
	}

	public static DataTable TextMessagesReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmTextMessageSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmTextMessageSearchReport frmTextMessageSearchReport2 = new frmTextMessageSearchReport(TextMessage.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0"), "TextMessageID");
		frmTextMessageSearchReport2.MinDate = minDate;
		frmTextMessageSearchReport2.MaxDate = maxDate;
		frmTextMessageSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTextMessageSearchReport2.Location = new Point(0, 0);
		frmTextMessageSearchReport2.ShowDialog();
		return frmTextMessageSearchReport2.dtResult;
	}

	public static DataTable ClientCreditNote(int Approved, int Deleted)
	{
		string fullName = typeof(frmClientCreditNoteSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmClientCreditNoteSearchReport frmClientCreditNoteSearchReport2 = new frmClientCreditNoteSearchReport(BusinessLayer.Lenses.ClientCreditNote.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ClientCreditNoteID", Approved, Deleted);
		frmClientCreditNoteSearchReport2.MinDate = minDate;
		frmClientCreditNoteSearchReport2.MaxDate = maxDate;
		frmClientCreditNoteSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClientCreditNoteSearchReport2.Location = new Point(0, 0);
		frmClientCreditNoteSearchReport2.ShowDialog();
		return frmClientCreditNoteSearchReport2.dtResult;
	}

	public static DataTable LnsCashBackOffersReport(int IsQtyDiscount, int IsCashBack, int IsPackage, bool IsFromServer)
	{
		frmCashBackOffersSearchReport frmCashBackOffersSearchReport2 = new frmCashBackOffersSearchReport(Offers.Search(GlobalVariables.IsArabic ? "1" : "0", IsQtyDiscount.ToString(), IsCashBack.ToString(), IsPackage.ToString(), IsFromServer), "OfferID");
		frmCashBackOffersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCashBackOffersSearchReport2.Location = new Point(0, 0);
		frmCashBackOffersSearchReport2.ShowDialog();
		return frmCashBackOffersSearchReport2.dtResult;
	}

	public static int LnsCashBackOffersSearch(int IsQtyDiscount, int IsCashBack, int IsPackage, bool IsFromServer)
	{
		frmCashBackOffersSearch frmCashBackOffersSearch2 = new frmCashBackOffersSearch(Offers.Search(GlobalVariables.IsArabic ? "1" : "0", IsQtyDiscount.ToString(), IsCashBack.ToString(), IsPackage.ToString(), IsFromServer), "OfferID");
		frmCashBackOffersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCashBackOffersSearch2.Location = new Point(0, 0);
		frmCashBackOffersSearch2.ShowDialog();
		return frmCashBackOffersSearch2.ID;
	}

	public static DataTable LnsOffersReport(int IsQtyDiscount, int IsCashBack, int IsPackage, bool IsFromServer)
	{
		frmOffersSearchReport frmOffersSearchReport2 = new frmOffersSearchReport(Offers.Search(GlobalVariables.IsArabic ? "1" : "0", IsQtyDiscount.ToString(), IsCashBack.ToString(), IsPackage.ToString(), IsFromServer), "OfferID");
		frmOffersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOffersSearchReport2.Location = new Point(0, 0);
		frmOffersSearchReport2.ShowDialog();
		return frmOffersSearchReport2.dtResult;
	}

	public static int LnsOffersSearch(int IsQtyDiscount, int IsCashBack, int IsPackage, bool IsFromServer)
	{
		frmOffersSearch frmOffersSearch2 = new frmOffersSearch(Offers.Search(GlobalVariables.IsArabic ? "1" : "0", IsQtyDiscount.ToString(), IsCashBack.ToString(), IsPackage.ToString(), IsFromServer), "OfferID");
		frmOffersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOffersSearch2.Location = new Point(0, 0);
		frmOffersSearch2.ShowDialog();
		return frmOffersSearch2.ID;
	}

	public static DataTable LabContractsSearchReport(bool IsFromServer)
	{
		frmLabContractsSearchReport frmLabContractsSearchReport2 = new frmLabContractsSearchReport(LabContracts.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LabContractID");
		frmLabContractsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLabContractsSearchReport2.Location = new Point(0, 0);
		frmLabContractsSearchReport2.ShowDialog();
		return frmLabContractsSearchReport2.dtResult;
	}

	public static int LabContractsSearch(bool IsFromServer)
	{
		frmLabContractsSearchReport frmLabContractsSearchReport2 = new frmLabContractsSearchReport(LabContracts.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LabContractID");
		frmLabContractsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLabContractsSearchReport2.Location = new Point(0, 0);
		frmLabContractsSearchReport2.ShowDialog();
		return frmLabContractsSearchReport2.ID;
	}

	public static DataTable LnsLabOrdersPaymentsReport(int Deleted)
	{
		string fullName = typeof(frmLnsLabOrdersPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmLnsLabOrdersPaymentsSearchReport frmLnsLabOrdersPaymentsSearchReport2 = new frmLnsLabOrdersPaymentsSearchReport(LabOrdersPayments.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "LabOrderPaymentID", Deleted);
		frmLnsLabOrdersPaymentsSearchReport2.MinDate = minDate;
		frmLnsLabOrdersPaymentsSearchReport2.MaxDate = maxDate;
		frmLnsLabOrdersPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLnsLabOrdersPaymentsSearchReport2.Location = new Point(0, 0);
		frmLnsLabOrdersPaymentsSearchReport2.ShowDialog();
		return frmLnsLabOrdersPaymentsSearchReport2.dtResult;
	}

	public static DataTable LabContractDetailsReport(string LabContractIDs, int Deleted)
	{
		frmLabContractDetailsSearchReport frmLabContractDetailsSearchReport2 = new frmLabContractDetailsSearchReport(LabContractsDetails.Search(LabContractIDs, GlobalVariables.IsArabic ? "1" : "0", Deleted.ToString()), "LabContractDetailID");
		frmLabContractDetailsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLabContractDetailsSearchReport2.Location = new Point(0, 0);
		frmLabContractDetailsSearchReport2.ShowDialog();
		return frmLabContractDetailsSearchReport2.dtResult;
	}

	public static int LabContractClientsSearch(bool IsFromServer)
	{
		frmLabContractsClientsSearch frmLabContractsClientsSearch2 = new frmLabContractsClientsSearch(LabContractsClients.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmLabContractsClientsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLabContractsClientsSearch2.Location = new Point(0, 0);
		frmLabContractsClientsSearch2.ShowDialog();
		return frmLabContractsClientsSearch2.ID;
	}

	public static DataTable BlanksAdditionsSearchReport(bool IsFromServer)
	{
		frmBlanksAdditionsSearchReport frmBlanksAdditionsSearchReport2 = new frmBlanksAdditionsSearchReport(BlanksAdditions.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BlankAdditionID");
		frmBlanksAdditionsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksAdditionsSearchReport2.Location = new Point(0, 0);
		frmBlanksAdditionsSearchReport2.ShowDialog();
		return frmBlanksAdditionsSearchReport2.dtResult;
	}

	public static DataTable BlanksTypesSearchReport(bool IsFromServer)
	{
		frmBlanksTypesSearchReport frmBlanksTypesSearchReport2 = new frmBlanksTypesSearchReport(BlanksTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BlankTypeID");
		frmBlanksTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksTypesSearchReport2.Location = new Point(0, 0);
		frmBlanksTypesSearchReport2.ShowDialog();
		return frmBlanksTypesSearchReport2.dtResult;
	}

	public static DataTable BlanksInvoicesPaymentsReport(int Deleted)
	{
		string fullName = typeof(frmBlanksInvoicesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBlanksInvoicesPaymentsSearchReport frmBlanksInvoicesPaymentsSearchReport2 = new frmBlanksInvoicesPaymentsSearchReport(BlanksInvoicesPayments.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BlankInvoicePaymentID", Deleted);
		frmBlanksInvoicesPaymentsSearchReport2.MinDate = minDate;
		frmBlanksInvoicesPaymentsSearchReport2.MaxDate = maxDate;
		frmBlanksInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmBlanksInvoicesPaymentsSearchReport2.ShowDialog();
		return frmBlanksInvoicesPaymentsSearchReport2.dtResult;
	}

	public static int BlanksInvoicesSearch(string BranchIDs, int Approved, int Deleted, int Closed, int IsDeliverd, int IsFullyPaid)
	{
		string fullName = typeof(frmBlanksInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBlanksInvoicesSearch frmBlanksInvoicesSearch2 = new frmBlanksInvoicesSearch(BlanksInvoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsDeliverd.ToString(), IsFullyPaid.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false), "BlankInvoiceID", Approved, Deleted);
		frmBlanksInvoicesSearch2.MinDate = minDate;
		frmBlanksInvoicesSearch2.MaxDate = maxDate;
		frmBlanksInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksInvoicesSearch2.Location = new Point(0, 0);
		frmBlanksInvoicesSearch2.ShowDialog();
		return frmBlanksInvoicesSearch2.ID;
	}

	public static DataTable BlanksInvoicesReport(int Approved, int Deleted, int Closed, int IsDeliverd, int IsFullyPaid, bool IsFromServer)
	{
		string fullName = typeof(frmBlanksInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBlanksInvoicesSearchReport frmBlanksInvoicesSearchReport2 = new frmBlanksInvoicesSearchReport(BlanksInvoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), IsDeliverd.ToString(), IsFullyPaid.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BlankInvoiceID", Approved, Deleted);
		frmBlanksInvoicesSearchReport2.MinDate = minDate;
		frmBlanksInvoicesSearchReport2.MaxDate = maxDate;
		frmBlanksInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksInvoicesSearchReport2.Location = new Point(0, 0);
		frmBlanksInvoicesSearchReport2.ShowDialog();
		return frmBlanksInvoicesSearchReport2.dtResult;
	}

	public static DataTable BlankTypesDiscountReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmBlanksTypesDiscountSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBlanksTypesDiscountSearchReport frmBlanksTypesDiscountSearchReport2 = new frmBlanksTypesDiscountSearchReport(BlanksTypesDiscount.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BlankTypeDiscountID");
		frmBlanksTypesDiscountSearchReport2.MinDate = minDate;
		frmBlanksTypesDiscountSearchReport2.MaxDate = maxDate;
		frmBlanksTypesDiscountSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksTypesDiscountSearchReport2.Location = new Point(0, 0);
		frmBlanksTypesDiscountSearchReport2.ShowDialog();
		return frmBlanksTypesDiscountSearchReport2.dtResult;
	}

	public static DataTable BlankAdditionsDiscountReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmBlanksAdditionsDiscountSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBlanksAdditionsDiscountSearchReport frmBlanksAdditionsDiscountSearchReport2 = new frmBlanksAdditionsDiscountSearchReport(BlanksAdditionsDiscount.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BlankAdditionDiscountID");
		frmBlanksAdditionsDiscountSearchReport2.MinDate = minDate;
		frmBlanksAdditionsDiscountSearchReport2.MaxDate = maxDate;
		frmBlanksAdditionsDiscountSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBlanksAdditionsDiscountSearchReport2.Location = new Point(0, 0);
		frmBlanksAdditionsDiscountSearchReport2.ShowDialog();
		return frmBlanksAdditionsDiscountSearchReport2.dtResult;
	}

	public static DataTable CaptainOrdersSearchReport(int isActive, bool IsFromServer)
	{
		frmCaptainOrdersSearchReport frmCaptainOrdersSearchReport2 = new frmCaptainOrdersSearchReport(CaptainOrder.Search(isActive.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CaptainOrderID");
		frmCaptainOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCaptainOrdersSearchReport2.Location = new Point(0, 0);
		frmCaptainOrdersSearchReport2.ShowDialog();
		return frmCaptainOrdersSearchReport2.dtResult;
	}

	public static int ShiftsSearch(bool IsFromServer)
	{
		frmShiftsSearch frmShiftsSearch2 = new frmShiftsSearch(BusinessLayer.POS.Shifts.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ShiftID");
		frmShiftsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShiftsSearch2.Location = new Point(0, 0);
		frmShiftsSearch2.ShowDialog();
		return frmShiftsSearch2.ID;
	}

	public static DataTable CommentTypeSearchReport(bool IsFromServer)
	{
		frmCommentTypesSearchReport frmCommentTypesSearchReport2 = new frmCommentTypesSearchReport(CommentTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CommentTypeID");
		frmCommentTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCommentTypesSearchReport2.Location = new Point(0, 0);
		frmCommentTypesSearchReport2.ShowDialog();
		return frmCommentTypesSearchReport2.dtResult;
	}

	public static DataTable POSClientsSearchReportWithDetails(string IsActive, string BranchID, bool IsFromServer)
	{
		frmPOSClientsSearchReportWithDetails frmPOSClientsSearchReportWithDetails2 = new frmPOSClientsSearchReportWithDetails(BusinessLayer.POS.Clients.SearchWithDetails(IsActive, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ClientID");
		frmPOSClientsSearchReportWithDetails2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPOSClientsSearchReportWithDetails2.Location = new Point(0, 0);
		frmPOSClientsSearchReportWithDetails2.ShowDialog();
		return frmPOSClientsSearchReportWithDetails2.dtResult;
	}

	public static int POSClientsSearch(string BranchID, bool IsFromServer)
	{
		frmPOSClientsSearch frmPOSClientsSearch2 = new frmPOSClientsSearch(BusinessLayer.POS.Clients.Search("-1", BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ClientID");
		frmPOSClientsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPOSClientsSearch2.Location = new Point(0, 0);
		frmPOSClientsSearch2.ShowDialog();
		return frmPOSClientsSearch2.ID;
	}

	public static DataTable POSClientsSearchReport(string IsActive, string BranchID, bool IsFromServer)
	{
		frmPOSClientsSearchReport frmPOSClientsSearchReport2 = new frmPOSClientsSearchReport(BusinessLayer.POS.Clients.Search(IsActive, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ClientID");
		frmPOSClientsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPOSClientsSearchReport2.Location = new Point(0, 0);
		frmPOSClientsSearchReport2.ShowDialog();
		return frmPOSClientsSearchReport2.dtResult;
	}

	public static DataTable RoomsReport(string BranchIDS, bool IsFromServer)
	{
		frmRoomsSearchReport frmRoomsSearchReport2 = new frmRoomsSearchReport(Rooms.Search(BranchIDS, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "RoomID");
		frmRoomsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmRoomsSearchReport2.Location = new Point(0, 0);
		frmRoomsSearchReport2.ShowDialog();
		return frmRoomsSearchReport2.dtResult;
	}

	public static DataTable ShiftsDetailsReport(string BranchIDs)
	{
		string fullName = typeof(frmShiftsDetailsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmShiftsDetailsSearchReport frmShiftsDetailsSearchReport2 = new frmShiftsDetailsSearchReport(ShiftsDetails.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0"), "ShiftDetailID");
		frmShiftsDetailsSearchReport2.MinDate = minDate;
		frmShiftsDetailsSearchReport2.MaxDate = maxDate;
		frmShiftsDetailsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShiftsDetailsSearchReport2.Location = new Point(0, 0);
		frmShiftsDetailsSearchReport2.ShowDialog();
		return frmShiftsDetailsSearchReport2.dtResult;
	}

	public static DataTable ChecksReport(string RoomIDs, string IsDineIn, string IsDelivery, string IsTakeAway, string BranchIDs)
	{
		string fullName = typeof(frmChecksSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmChecksSearchReport frmChecksSearchReport2 = new frmChecksSearchReport(Checks.Search(RoomIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), IsDineIn, IsDelivery, IsTakeAway, BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "CheckID");
		frmChecksSearchReport2.MinDate = minDate;
		frmChecksSearchReport2.MaxDate = maxDate;
		frmChecksSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmChecksSearchReport2.Location = new Point(0, 0);
		frmChecksSearchReport2.ShowDialog();
		return frmChecksSearchReport2.dtResult;
	}

	public static DataTable ChecksReport(DateTime FromDate, DateTime ToDate, string RoomIDs, string IsDineIn, string IsDelivery, string IsTakeAway, string BranchIDs)
	{
		frmChecksSearchReport frmChecksSearchReport2 = new frmChecksSearchReport(Checks.Search(RoomIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), IsDineIn, IsDelivery, IsTakeAway, BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "CheckID");
		frmChecksSearchReport2.MinDate = FromDate;
		frmChecksSearchReport2.MaxDate = ToDate;
		frmChecksSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmChecksSearchReport2.Location = new Point(0, 0);
		frmChecksSearchReport2.ShowDialog();
		return frmChecksSearchReport2.dtResult;
	}

	public static DataTable ChecksReturnsReport(string IsDineIn, string IsDelivery, string IsTakeAway, int Approved, int Deleted)
	{
		string fullName = typeof(frmChecksReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmChecksReturnsSearchReport frmChecksReturnsSearchReport2 = new frmChecksReturnsSearchReport(ChecksReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), IsDineIn, IsDelivery, IsTakeAway, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmChecksReturnsSearchReport2.MinDate = minDate;
		frmChecksReturnsSearchReport2.MaxDate = maxDate;
		frmChecksReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmChecksReturnsSearchReport2.Location = new Point(0, 0);
		frmChecksReturnsSearchReport2.ShowDialog();
		return frmChecksReturnsSearchReport2.dtResult;
	}

	public static DataTable ChecksReturnsReport(string BranchIDs, DateTime FromDate, DateTime ToDate, string IsDineIn, string IsDelivery, string IsTakeAway, int Approved, int Deleted)
	{
		frmChecksReturnsSearchReport frmChecksReturnsSearchReport2 = new frmChecksReturnsSearchReport(ChecksReturns.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), IsDineIn, IsDelivery, IsTakeAway, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmChecksReturnsSearchReport2.MinDate = FromDate;
		frmChecksReturnsSearchReport2.MaxDate = ToDate;
		frmChecksReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmChecksReturnsSearchReport2.Location = new Point(0, 0);
		frmChecksReturnsSearchReport2.ShowDialog();
		return frmChecksReturnsSearchReport2.dtResult;
	}

	public static DataTable SpecialOrdersReturnsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSpecialOrdersReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSpecialOrdersReturnsSearchReport frmSpecialOrdersReturnsSearchReport2 = new frmSpecialOrdersReturnsSearchReport(SpecialOrdersReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReturnID");
		frmSpecialOrdersReturnsSearchReport2.MinDate = minDate;
		frmSpecialOrdersReturnsSearchReport2.MaxDate = maxDate;
		frmSpecialOrdersReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSpecialOrdersReturnsSearchReport2.Location = new Point(0, 0);
		frmSpecialOrdersReturnsSearchReport2.ShowDialog();
		return frmSpecialOrdersReturnsSearchReport2.dtResult;
	}

	public static DataTable SpecialOrdersReport(string RoomIDs, string BranchIDs)
	{
		string fullName = typeof(frmSpecialOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSpecialOrdersSearchReport frmSpecialOrdersSearchReport2 = new frmSpecialOrdersSearchReport(SpecialOrders.Search(RoomIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "SpecialOrderID");
		frmSpecialOrdersSearchReport2.MinDate = minDate;
		frmSpecialOrdersSearchReport2.MaxDate = maxDate;
		frmSpecialOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSpecialOrdersSearchReport2.Location = new Point(0, 0);
		frmSpecialOrdersSearchReport2.ShowDialog();
		return frmSpecialOrdersSearchReport2.dtResult;
	}

	public static DataTable ReservationsReport(string BranchIDs, string IsDeliverd, string IsCancelled, string Approved, string Deleted)
	{
		string fullName = typeof(frmReservationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmReservationsSearchReport frmReservationsSearchReport2 = new frmReservationsSearchReport(BusinessLayer.Lenses.Reservations.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), BranchIDs, IsDeliverd, IsCancelled, Approved, Deleted, GlobalVariables.IsArabic ? "1" : "0"), "ReservationID");
		frmReservationsSearchReport2.MinDate = minDate;
		frmReservationsSearchReport2.MaxDate = maxDate;
		frmReservationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmReservationsSearchReport2.Location = new Point(0, 0);
		frmReservationsSearchReport2.ShowDialog();
		return frmReservationsSearchReport2.dtResult;
	}

	public static int Reservations(string BranchIDs, string IsDeliverd, string IsCancelled, string Approved, string Deleted)
	{
		string fullName = typeof(frmReservationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmReservationsSearch frmReservationsSearch2 = new frmReservationsSearch(BusinessLayer.Lenses.Reservations.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), BranchIDs, IsDeliverd, IsCancelled, Approved, Deleted, GlobalVariables.IsArabic ? "1" : "0"), "ReservationID");
		frmReservationsSearch2.MinDate = minDate;
		frmReservationsSearch2.MaxDate = maxDate;
		frmReservationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmReservationsSearch2.Location = new Point(0, 0);
		frmReservationsSearch2.ShowDialog();
		return frmReservationsSearch2.ID;
	}

	public static DataTable KitchenScreensReport(bool IsFromServer)
	{
		frmKitchenScreensSearchReport frmKitchenScreensSearchReport2 = new frmKitchenScreensSearchReport(KitchenScreens.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "KitchenScreenID");
		frmKitchenScreensSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmKitchenScreensSearchReport2.Location = new Point(0, 0);
		frmKitchenScreensSearchReport2.ShowDialog();
		return frmKitchenScreensSearchReport2.dtResult;
	}

	public static DataTable ItemsCatalogeReport(bool IsFromServer)
	{
		ERP.Production.Search.frmItemsCatalogeSearchReport frmItemsCatalogeSearchReport2 = new ERP.Production.Search.frmItemsCatalogeSearchReport(BusinessLayer.Production.ItemsCataloge.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemCatalogeID");
		frmItemsCatalogeSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsCatalogeSearchReport2.Location = new Point(0, 0);
		frmItemsCatalogeSearchReport2.ShowDialog();
		return frmItemsCatalogeSearchReport2.dtResult;
	}

	public static int ItemsCataloge(bool IsFromServer)
	{
		ERP.Production.Search.frmItemsCatalogeSearch frmItemsCatalogeSearch2 = new ERP.Production.Search.frmItemsCatalogeSearch(BusinessLayer.Production.ItemsCataloge.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemCatalogeID");
		frmItemsCatalogeSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsCatalogeSearch2.Location = new Point(0, 0);
		frmItemsCatalogeSearch2.ShowDialog();
		return frmItemsCatalogeSearch2.ID;
	}

	public static int ProductionRequests(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmProductionRequestsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmProductionRequestsSearch frmProductionRequestsSearch2 = new frmProductionRequestsSearch(BusinessLayer.Production.ProductionRequests.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductionRequestID");
		frmProductionRequestsSearch2.MinDate = minDate;
		frmProductionRequestsSearch2.MaxDate = maxDate;
		frmProductionRequestsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductionRequestsSearch2.Location = new Point(0, 0);
		frmProductionRequestsSearch2.ShowDialog();
		return frmProductionRequestsSearch2.ID;
	}

	public static DataTable ProductionRequestsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmProductionRequestsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmProductionRequestsSearchReport frmProductionRequestsSearchReport2 = new frmProductionRequestsSearchReport(BusinessLayer.Production.ProductionRequests.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductionRequestID");
		frmProductionRequestsSearchReport2.MinDate = minDate;
		frmProductionRequestsSearchReport2.MaxDate = maxDate;
		frmProductionRequestsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductionRequestsSearchReport2.Location = new Point(0, 0);
		frmProductionRequestsSearchReport2.ShowDialog();
		return frmProductionRequestsSearchReport2.dtResult;
	}

	public static DataTable ProductMaintenanceReport(int SentToMaintenance, int MaintenanceReceived, int SentToBranch, int BranchReceived, int DeliveredToClient, int Deleted)
	{
		string fullName = typeof(frmProductMaintenanceSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmProductMaintenanceSearchReport frmProductMaintenanceSearchReport2 = new frmProductMaintenanceSearchReport(ProductMaintenance.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), SentToMaintenance, MaintenanceReceived, SentToBranch, BranchReceived, DeliveredToClient, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductMaintenanceID");
		frmProductMaintenanceSearchReport2.MinDate = minDate;
		frmProductMaintenanceSearchReport2.MaxDate = maxDate;
		frmProductMaintenanceSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductMaintenanceSearchReport2.Location = new Point(0, 0);
		frmProductMaintenanceSearchReport2.ShowDialog();
		return frmProductMaintenanceSearchReport2.dtResult;
	}

	public static DataTable ProductionsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmProductionsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmProductionsSearchReport frmProductionsSearchReport2 = new frmProductionsSearchReport(Productions.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductionID");
		frmProductionsSearchReport2.MinDate = minDate;
		frmProductionsSearchReport2.MaxDate = maxDate;
		frmProductionsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductionsSearchReport2.Location = new Point(0, 0);
		frmProductionsSearchReport2.ShowDialog();
		return frmProductionsSearchReport2.dtResult;
	}

	public static DataTable ProductionsReport(string BranchIDs, DateTime FromDate, DateTime ToDate, int Approved, int Deleted)
	{
		frmProductionsSearchReport frmProductionsSearchReport2 = new frmProductionsSearchReport(Productions.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductionID");
		frmProductionsSearchReport2.MinDate = FromDate;
		frmProductionsSearchReport2.MaxDate = ToDate;
		frmProductionsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductionsSearchReport2.Location = new Point(0, 0);
		frmProductionsSearchReport2.ShowDialog();
		return frmProductionsSearchReport2.dtResult;
	}

	public static DataTable ProductionsFees(int Approved, int Deleted)
	{
		string fullName = typeof(frmProductionsFeesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmProductionsFeesSearchReport frmProductionsFeesSearchReport2 = new frmProductionsFeesSearchReport(BusinessLayer.Production.ProductionsFees.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductionFeesID");
		frmProductionsFeesSearchReport2.MinDate = minDate;
		frmProductionsFeesSearchReport2.MaxDate = maxDate;
		frmProductionsFeesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductionsFeesSearchReport2.Location = new Point(0, 0);
		frmProductionsFeesSearchReport2.ShowDialog();
		return frmProductionsFeesSearchReport2.dtResult;
	}

	public static DataTable ProductionsStagesReport(int Approved, int Deleted)
	{
		frmProductionsStagesSearchReport frmProductionsStagesSearchReport2 = new frmProductionsStagesSearchReport(ProductionsStages.Search(GlobalVariables.BranchIDs, Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ProductionID");
		frmProductionsStagesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProductionsStagesSearchReport2.Location = new Point(0, 0);
		frmProductionsStagesSearchReport2.ShowDialog();
		return frmProductionsStagesSearchReport2.dtResult;
	}

	public static DataTable LinesReport(bool IsFromServer)
	{
		ERP.Production.Search.frmLinesSearchReport frmLinesSearchReport2 = new ERP.Production.Search.frmLinesSearchReport(BusinessLayer.Production.Lines.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LineID");
		frmLinesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLinesSearchReport2.Location = new Point(0, 0);
		frmLinesSearchReport2.ShowDialog();
		return frmLinesSearchReport2.dtResult;
	}

	public static int Lines(bool IsFromServer)
	{
		ERP.Production.Search.frmLinesSearch frmLinesSearch2 = new ERP.Production.Search.frmLinesSearch(BusinessLayer.Production.Lines.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LineID");
		frmLinesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLinesSearch2.Location = new Point(0, 0);
		frmLinesSearch2.ShowDialog();
		return frmLinesSearch2.ID;
	}

	public static int AdministrativeStructure(bool IsFromServer)
	{
		frmAdministrativeStructureSearch frmAdministrativeStructureSearch2 = new frmAdministrativeStructureSearch(BusinessLayer.HR.AdministrativeStructure.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AdministrativeStructureID");
		frmAdministrativeStructureSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAdministrativeStructureSearch2.Location = new Point(0, 0);
		frmAdministrativeStructureSearch2.ShowDialog();
		return frmAdministrativeStructureSearch2.ID;
	}

	public static DataTable AdministrativeStructureReport(bool IsFromServer)
	{
		frmAdministrativeStructureSearchReport frmAdministrativeStructureSearchReport2 = new frmAdministrativeStructureSearchReport(BusinessLayer.HR.AdministrativeStructure.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AdministrativeStructureID");
		frmAdministrativeStructureSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAdministrativeStructureSearchReport2.Location = new Point(0, 0);
		frmAdministrativeStructureSearchReport2.ShowDialog();
		return frmAdministrativeStructureSearchReport2.dtResult;
	}

	public static int Employees(string IsActive, string IsSalesMan, bool IsFromServer)
	{
		frmEmployeesSearch frmEmployeesSearch2 = new frmEmployeesSearch(BusinessLayer.HR.Employees.Search(GlobalVariables.EmployeeSubAccountTypeIDs, IsActive, IsSalesMan, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmEmployeesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEmployeesSearch2.Location = new Point(0, 0);
		frmEmployeesSearch2.ShowDialog();
		return frmEmployeesSearch2.ID;
	}

	public static DataTable EmployeesReport(string IsActive, string IsSalesMan, bool IsFromServer)
	{
		frmEmployeesSearchReport frmEmployeesSearchReport2 = new frmEmployeesSearchReport(BusinessLayer.HR.Employees.Search(GlobalVariables.EmployeeSubAccountTypeIDs, IsActive, IsSalesMan, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmEmployeesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEmployeesSearchReport2.Location = new Point(0, 0);
		frmEmployeesSearchReport2.ShowDialog();
		return frmEmployeesSearchReport2.dtResult;
	}

	public static int ShiftsPlans(bool IsFromServer)
	{
		frmShiftsPlansSearch frmShiftsPlansSearch2 = new frmShiftsPlansSearch(BusinessLayer.HR.ShiftsPlans.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ShiftPlanID");
		frmShiftsPlansSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShiftsPlansSearch2.Location = new Point(0, 0);
		frmShiftsPlansSearch2.ShowDialog();
		return frmShiftsPlansSearch2.ID;
	}

	public static DataTable ShiftsPlansReport(bool IsFromServer)
	{
		frmShiftsPlansSearchReport frmShiftsPlansSearchReport2 = new frmShiftsPlansSearchReport(BusinessLayer.HR.ShiftsPlans.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ShiftPlanID");
		frmShiftsPlansSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShiftsPlansSearchReport2.Location = new Point(0, 0);
		frmShiftsPlansSearchReport2.ShowDialog();
		return frmShiftsPlansSearchReport2.dtResult;
	}

	public static DataTable QualificationNamesReport(bool IsFromServer)
	{
		frmQualificationNamesSearchReport frmQualificationNamesSearchReport2 = new frmQualificationNamesSearchReport(QualificationNames.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "QualificationNameID");
		frmQualificationNamesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQualificationNamesSearchReport2.Location = new Point(0, 0);
		frmQualificationNamesSearchReport2.ShowDialog();
		return frmQualificationNamesSearchReport2.dtResult;
	}

	public static DataTable AbsentRulesReport(bool IsFromServer)
	{
		frmAbsentRulesSearchReport frmAbsentRulesSearchReport2 = new frmAbsentRulesSearchReport(AbsentRules.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AbsentRuleID");
		frmAbsentRulesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAbsentRulesSearchReport2.Location = new Point(0, 0);
		frmAbsentRulesSearchReport2.ShowDialog();
		return frmAbsentRulesSearchReport2.dtResult;
	}

	public static DataTable ExtraTimeRulesReport(bool IsFromServer)
	{
		frmExtraTimeRulesSearchReport frmExtraTimeRulesSearchReport2 = new frmExtraTimeRulesSearchReport(ExtraTimeRules.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExtraTimeRuleID");
		frmExtraTimeRulesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExtraTimeRulesSearchReport2.Location = new Point(0, 0);
		frmExtraTimeRulesSearchReport2.ShowDialog();
		return frmExtraTimeRulesSearchReport2.dtResult;
	}

	public static DataTable FeedingRulesReport(bool IsFromServer)
	{
		frmFeedingRulesSearchReport frmFeedingRulesSearchReport2 = new frmFeedingRulesSearchReport(FeedingRules.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "FeedingRuleID");
		frmFeedingRulesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmFeedingRulesSearchReport2.Location = new Point(0, 0);
		frmFeedingRulesSearchReport2.ShowDialog();
		return frmFeedingRulesSearchReport2.dtResult;
	}

	public static DataTable DelayRulesReport(bool IsFromServer)
	{
		frmDelayRulesSearchReport frmDelayRulesSearchReport2 = new frmDelayRulesSearchReport(DelayRules.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DelayRuleID");
		frmDelayRulesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDelayRulesSearchReport2.Location = new Point(0, 0);
		frmDelayRulesSearchReport2.ShowDialog();
		return frmDelayRulesSearchReport2.dtResult;
	}

	public static DataTable PenaltyRulesReport(bool IsFromServer)
	{
		frmPenaltyRulesSearchReport frmPenaltyRulesSearchReport2 = new frmPenaltyRulesSearchReport(PenaltyRules.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PenaltyRuleID");
		frmPenaltyRulesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPenaltyRulesSearchReport2.Location = new Point(0, 0);
		frmPenaltyRulesSearchReport2.ShowDialog();
		return frmPenaltyRulesSearchReport2.dtResult;
	}

	public static DataTable LeavePermissionRulesReport(bool IsFromServer)
	{
		frmLeavePermissionRuleSearchReport frmLeavePermissionRuleSearchReport2 = new frmLeavePermissionRuleSearchReport(LeavePermissionRule.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LeavePermissionRuleID");
		frmLeavePermissionRuleSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLeavePermissionRuleSearchReport2.Location = new Point(0, 0);
		frmLeavePermissionRuleSearchReport2.ShowDialog();
		return frmLeavePermissionRuleSearchReport2.dtResult;
	}

	public static DataTable VacationRulesReport(bool IsFromServer)
	{
		frmVacationRulesSearchReport frmVacationRulesSearchReport2 = new frmVacationRulesSearchReport(VacationRules.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "VacationRuleID");
		frmVacationRulesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVacationRulesSearchReport2.Location = new Point(0, 0);
		frmVacationRulesSearchReport2.ShowDialog();
		return frmVacationRulesSearchReport2.dtResult;
	}

	public static DataTable SalaryListsReport(bool IsFromServer)
	{
		frmSalaryListsSearchReport frmSalaryListsSearchReport2 = new frmSalaryListsSearchReport(SalaryLists.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SalaryListID");
		frmSalaryListsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSalaryListsSearchReport2.Location = new Point(0, 0);
		frmSalaryListsSearchReport2.ShowDialog();
		return frmSalaryListsSearchReport2.dtResult;
	}

	public static DataTable SocialIncreaseReport(bool IsFromServer)
	{
		string fullName = typeof(frmSocialIncreaseSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmSocialIncreaseSearchReport frmSocialIncreaseSearchReport2 = new frmSocialIncreaseSearchReport(SocialIncrease.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SocialIncreaseID");
		frmSocialIncreaseSearchReport2.MinDate = minDate;
		frmSocialIncreaseSearchReport2.MaxDate = maxDate;
		frmSocialIncreaseSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSocialIncreaseSearchReport2.Location = new Point(0, 0);
		frmSocialIncreaseSearchReport2.ShowDialog();
		return frmSocialIncreaseSearchReport2.dtResult;
	}

	public static DataTable EmployeesAdvancesReport(int Approved, int Deleted, bool IsFromServer)
	{
		string fullName = typeof(frmEmployeesAdvancesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmEmployeesAdvancesSearchReport frmEmployeesAdvancesSearchReport2 = new frmEmployeesAdvancesSearchReport(EmployeesAdvances.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "EmployeeAdvanceID");
		frmEmployeesAdvancesSearchReport2.MinDate = minDate;
		frmEmployeesAdvancesSearchReport2.MaxDate = maxDate;
		frmEmployeesAdvancesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEmployeesAdvancesSearchReport2.Location = new Point(0, 0);
		frmEmployeesAdvancesSearchReport2.ShowDialog();
		return frmEmployeesAdvancesSearchReport2.dtResult;
	}

	public static DataTable EmployeesBonusReport(int Approved, int Deleted, bool IsFromServer)
	{
		string fullName = typeof(frmEmployeesBonusSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmEmployeesBonusSearchReport frmEmployeesBonusSearchReport2 = new frmEmployeesBonusSearchReport(EmployeesBonus.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "EmployeeBonusID");
		frmEmployeesBonusSearchReport2.MinDate = minDate;
		frmEmployeesBonusSearchReport2.MaxDate = maxDate;
		frmEmployeesBonusSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEmployeesBonusSearchReport2.Location = new Point(0, 0);
		frmEmployeesBonusSearchReport2.ShowDialog();
		return frmEmployeesBonusSearchReport2.dtResult;
	}

	public static DataTable EmployeesVacationsReplacementsReport(int Deleted, bool IsFromServer)
	{
		string fullName = typeof(frmEmployeesVacationsReplacementsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmEmployeesVacationsReplacementsSearchReport frmEmployeesVacationsReplacementsSearchReport2 = new frmEmployeesVacationsReplacementsSearchReport(EmployeesVacationsReplacements.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "EmployeeVacationReplacementID");
		frmEmployeesVacationsReplacementsSearchReport2.MinDate = minDate;
		frmEmployeesVacationsReplacementsSearchReport2.MaxDate = maxDate;
		frmEmployeesVacationsReplacementsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEmployeesVacationsReplacementsSearchReport2.Location = new Point(0, 0);
		frmEmployeesVacationsReplacementsSearchReport2.ShowDialog();
		return frmEmployeesVacationsReplacementsSearchReport2.dtResult;
	}

	public static DataTable EmployeesExtraTimesReport(int Approved, int Deleted, bool IsFromServer)
	{
		string fullName = typeof(frmEmployeesExtraTimesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmEmployeesExtraTimesSearchReport frmEmployeesExtraTimesSearchReport2 = new frmEmployeesExtraTimesSearchReport(EmployeesExtraTimes.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "EmployeeExtraTimeID");
		frmEmployeesExtraTimesSearchReport2.MinDate = minDate;
		frmEmployeesExtraTimesSearchReport2.MaxDate = maxDate;
		frmEmployeesExtraTimesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEmployeesExtraTimesSearchReport2.Location = new Point(0, 0);
		frmEmployeesExtraTimesSearchReport2.ShowDialog();
		return frmEmployeesExtraTimesSearchReport2.dtResult;
	}

	public static DataTable WareHouseIssueSearchReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmWareHouseIssueSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmWareHouseIssueSearchReport frmWareHouseIssueSearchReport2 = new frmWareHouseIssueSearchReport(WareHouseIssue.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "WareHouseIssueID");
		frmWareHouseIssueSearchReport2.MinDate = minDate;
		frmWareHouseIssueSearchReport2.MaxDate = maxDate;
		frmWareHouseIssueSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmWareHouseIssueSearchReport2.Location = new Point(0, 0);
		frmWareHouseIssueSearchReport2.ShowDialog();
		return frmWareHouseIssueSearchReport2.dtResult;
	}

	public static DataTable WareHouseReceiptSearchReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmWareHouseReceiptSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmWareHouseReceiptSearchReport frmWareHouseReceiptSearchReport2 = new frmWareHouseReceiptSearchReport(WareHouseReceipt.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "WareHouseReceiptID");
		frmWareHouseReceiptSearchReport2.MinDate = minDate;
		frmWareHouseReceiptSearchReport2.MaxDate = maxDate;
		frmWareHouseReceiptSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmWareHouseReceiptSearchReport2.Location = new Point(0, 0);
		frmWareHouseReceiptSearchReport2.ShowDialog();
		return frmWareHouseReceiptSearchReport2.dtResult;
	}

	public static DataTable ClinicsExpensesReport(int Approved, int Deleted)
	{
		string fullName = typeof(ERP.Lenses.Search.frmExpensesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmClinicsExpensesSearchReport frmClinicsExpensesSearchReport2 = new frmClinicsExpensesSearchReport(BusinessLayer.Lenses.Expenses.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ExpenseID", Approved, Deleted);
		frmClinicsExpensesSearchReport2.MinDate = minDate;
		frmClinicsExpensesSearchReport2.MaxDate = maxDate;
		frmClinicsExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClinicsExpensesSearchReport2.Location = new Point(0, 0);
		frmClinicsExpensesSearchReport2.ShowDialog();
		return frmClinicsExpensesSearchReport2.dtResult;
	}

	public static DataTable DoctorsSearchReport(bool IsFromServer)
	{
		frmDoctorsSearchReport frmDoctorsSearchReport2 = new frmDoctorsSearchReport(Doctors.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DoctorID");
		frmDoctorsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDoctorsSearchReport2.Location = new Point(0, 0);
		frmDoctorsSearchReport2.ShowDialog();
		return frmDoctorsSearchReport2.dtResult;
	}

	public static int DoctorsSearch(bool IsFromServer)
	{
		frmDoctorsSearch frmDoctorsSearch2 = new frmDoctorsSearch(Doctors.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DoctorID");
		frmDoctorsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDoctorsSearch2.Location = new Point(0, 0);
		frmDoctorsSearch2.ShowDialog();
		return frmDoctorsSearch2.ID;
	}

	public static DataTable PatientsSearchReport(bool IsFromServer)
	{
		frmPatientsSearchReport frmPatientsSearchReport2 = new frmPatientsSearchReport(Patients.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PatientID");
		frmPatientsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPatientsSearchReport2.Location = new Point(0, 0);
		frmPatientsSearchReport2.ShowDialog();
		return frmPatientsSearchReport2.dtResult;
	}

	public static int LabsSearch(string IsSampleLab, string IsBioAnalysisLab, string IsIndoorLab, bool IsFromServer)
	{
		frmLabsSearch frmLabsSearch2 = new frmLabsSearch(BusinessLayer.Clinics.Labs.Search(IsSampleLab, IsBioAnalysisLab, IsIndoorLab, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "LabID");
		frmLabsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmLabsSearch2.Location = new Point(0, 0);
		frmLabsSearch2.ShowDialog();
		return frmLabsSearch2.ID;
	}

	public static int PatientsSearch(bool IsFromServer)
	{
		frmPatientsSearch frmPatientsSearch2 = new frmPatientsSearch(Patients.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PatientID");
		frmPatientsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPatientsSearch2.Location = new Point(0, 0);
		frmPatientsSearch2.ShowDialog();
		return frmPatientsSearch2.ID;
	}

	public static DataTable MedicalRecordsSearchReport(int Deleted)
	{
		string fullName = typeof(frmMedicalRecordsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmMedicalRecordsSearchReport frmMedicalRecordsSearchReport2 = new frmMedicalRecordsSearchReport(MedicalRecords.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "MedicalRecordID", Deleted);
		frmMedicalRecordsSearchReport2.MinDate = minDate;
		frmMedicalRecordsSearchReport2.MaxDate = maxDate;
		frmMedicalRecordsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMedicalRecordsSearchReport2.Location = new Point(0, 0);
		frmMedicalRecordsSearchReport2.ShowDialog();
		return frmMedicalRecordsSearchReport2.dtResult;
	}

	public static DataTable SpecializationsSearchReport(bool IsFromServer)
	{
		frmSpecializationsSearchReport frmSpecializationsSearchReport2 = new frmSpecializationsSearchReport(Specializations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SpecializationID");
		frmSpecializationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSpecializationsSearchReport2.Location = new Point(0, 0);
		frmSpecializationsSearchReport2.ShowDialog();
		return frmSpecializationsSearchReport2.dtResult;
	}

	public static DataTable ProceduresSearchReport(bool IsFromServer)
	{
		frmProceduresSearchReport frmProceduresSearchReport2 = new frmProceduresSearchReport(Procedures.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ProcedureID");
		frmProceduresSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProceduresSearchReport2.Location = new Point(0, 0);
		frmProceduresSearchReport2.ShowDialog();
		return frmProceduresSearchReport2.dtResult;
	}

	public static DataTable ClinicsSearchReport(bool IsFromServer)
	{
		frmClinicsSearchReport frmClinicsSearchReport2 = new frmClinicsSearchReport(BusinessLayer.Clinics.Clinics.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ClinicID");
		frmClinicsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmClinicsSearchReport2.Location = new Point(0, 0);
		frmClinicsSearchReport2.ShowDialog();
		return frmClinicsSearchReport2.dtResult;
	}

	public static DataTable DoctorsScheduleSearchReport(bool IsFromServer)
	{
		frmDoctorsScheduleSearchReport frmDoctorsScheduleSearchReport2 = new frmDoctorsScheduleSearchReport(DoctorsSchedule.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DoctorScheduleID");
		frmDoctorsScheduleSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDoctorsScheduleSearchReport2.Location = new Point(0, 0);
		frmDoctorsScheduleSearchReport2.ShowDialog();
		return frmDoctorsScheduleSearchReport2.dtResult;
	}

	public static DataTable BioAnalysisOrdersReport(int Approved, int Deleted, int IsDeliverd, string BranchIDs)
	{
		string fullName = typeof(frmBioAnalysisOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmBioAnalysisOrdersSearchReport frmBioAnalysisOrdersSearchReport2 = new frmBioAnalysisOrdersSearchReport(BioAnalysisOrders.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), IsDeliverd.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "BioAnalysisOrderID", Approved, Deleted);
		frmBioAnalysisOrdersSearchReport2.MinDate = minDate;
		frmBioAnalysisOrdersSearchReport2.MaxDate = maxDate;
		frmBioAnalysisOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBioAnalysisOrdersSearchReport2.Location = new Point(0, 0);
		frmBioAnalysisOrdersSearchReport2.ShowDialog();
		return frmBioAnalysisOrdersSearchReport2.dtResult;
	}

	public static DataTable StagesSearchReport(bool IsFromServer)
	{
		frmStagesSearchReport frmStagesSearchReport2 = new frmStagesSearchReport(BusinessLayer.Photos.Stages.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "StageID");
		frmStagesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmStagesSearchReport2.Location = new Point(0, 0);
		frmStagesSearchReport2.ShowDialog();
		return frmStagesSearchReport2.dtResult;
	}

	public static DataTable ItemsCatalogeSearchReportPhotos(bool IsFromServer)
	{
		ERP.Photos.Search.frmItemsCatalogeSearchReport frmItemsCatalogeSearchReport2 = new ERP.Photos.Search.frmItemsCatalogeSearchReport(BusinessLayer.Photos.ItemsCataloge.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemCatalogeID");
		frmItemsCatalogeSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsCatalogeSearchReport2.Location = new Point(0, 0);
		frmItemsCatalogeSearchReport2.ShowDialog();
		return frmItemsCatalogeSearchReport2.dtResult;
	}

	public static int ItemsCatalogeSearchPhotos(bool IsFromServer)
	{
		ERP.Photos.Search.frmItemsCatalogeSearch frmItemsCatalogeSearch2 = new ERP.Photos.Search.frmItemsCatalogeSearch(BusinessLayer.Photos.ItemsCataloge.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemCatalogeID");
		frmItemsCatalogeSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsCatalogeSearch2.Location = new Point(0, 0);
		frmItemsCatalogeSearch2.ShowDialog();
		return frmItemsCatalogeSearch2.ID;
	}

	public static DataTable ItemsPackagesSearchReportPhotos(bool IsFromServer)
	{
		frmItemsPackagesSearchReport frmItemsPackagesSearchReport2 = new frmItemsPackagesSearchReport(ItemsPackages.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemPackageID");
		frmItemsPackagesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsPackagesSearchReport2.Location = new Point(0, 0);
		frmItemsPackagesSearchReport2.ShowDialog();
		return frmItemsPackagesSearchReport2.dtResult;
	}

	public static int ItemsPackagesSearchPhotos(bool IsFromServer)
	{
		frmItemsPackagesSearch frmItemsPackagesSearch2 = new frmItemsPackagesSearch(ItemsPackages.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ItemPackageID");
		frmItemsPackagesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsPackagesSearch2.Location = new Point(0, 0);
		frmItemsPackagesSearch2.ShowDialog();
		return frmItemsPackagesSearch2.ID;
	}

	public static DataTable AgentsSearchReport(bool IsFromServer)
	{
		frmAgentsSearchReport frmAgentsSearchReport2 = new frmAgentsSearchReport(BusinessLayer.Photos.Agents.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AgentID");
		frmAgentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAgentsSearchReport2.Location = new Point(0, 0);
		frmAgentsSearchReport2.ShowDialog();
		return frmAgentsSearchReport2.dtResult;
	}

	public static int AgentsSearch(bool IsFromServer)
	{
		frmAgentsSearch frmAgentsSearch2 = new frmAgentsSearch(BusinessLayer.Photos.Agents.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AgentID");
		frmAgentsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAgentsSearch2.Location = new Point(0, 0);
		frmAgentsSearch2.ShowDialog();
		return frmAgentsSearch2.ID;
	}

	public static int PHOInvoicesSearch(string BranchIDs, int Approved, int Deleted, int IsDeliverd)
	{
		string fullName = typeof(frmPHOInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPHOInvoicesSearch frmPHOInvoicesSearch2 = new frmPHOInvoicesSearch(BusinessLayer.Photos.Invoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), IsDeliverd.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoiceID", Approved, Deleted);
		frmPHOInvoicesSearch2.MinDate = minDate;
		frmPHOInvoicesSearch2.MaxDate = maxDate;
		frmPHOInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPHOInvoicesSearch2.Location = new Point(0, 0);
		frmPHOInvoicesSearch2.ShowDialog();
		return frmPHOInvoicesSearch2.ID;
	}

	public static DataTable PHOInvoicesReport(int Approved, int Deleted, int IsDeliverd, string BranchIDs)
	{
		string fullName = typeof(frmPHOInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPHOInvoicesSearchReport frmPHOInvoicesSearchReport2 = new frmPHOInvoicesSearchReport(BusinessLayer.Photos.Invoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), IsDeliverd.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoiceID", Approved, Deleted);
		frmPHOInvoicesSearchReport2.MinDate = minDate;
		frmPHOInvoicesSearchReport2.MaxDate = maxDate;
		frmPHOInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPHOInvoicesSearchReport2.Location = new Point(0, 0);
		frmPHOInvoicesSearchReport2.ShowDialog();
		return frmPHOInvoicesSearchReport2.dtResult;
	}

	public static DataTable PHOInvoicesPaymentsReport(int Deleted)
	{
		string fullName = typeof(frmPHOInvoicesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPHOInvoicesPaymentsSearchReport frmPHOInvoicesPaymentsSearchReport2 = new frmPHOInvoicesPaymentsSearchReport(InvoicesPayments.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoicePaymentID", Deleted);
		frmPHOInvoicesPaymentsSearchReport2.MinDate = minDate;
		frmPHOInvoicesPaymentsSearchReport2.MaxDate = maxDate;
		frmPHOInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPHOInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmPHOInvoicesPaymentsSearchReport2.ShowDialog();
		return frmPHOInvoicesPaymentsSearchReport2.dtResult;
	}

	public static DataTable ReservationsPaymentsReport(int Deleted)
	{
		string fullName = typeof(frmReservationsPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmReservationsPaymentsSearchReport frmReservationsPaymentsSearchReport2 = new frmReservationsPaymentsSearchReport(BusinessLayer.Lenses.ReservationsPayments.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReservationPaymentID", Deleted);
		frmReservationsPaymentsSearchReport2.MinDate = minDate;
		frmReservationsPaymentsSearchReport2.MaxDate = maxDate;
		frmReservationsPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmReservationsPaymentsSearchReport2.Location = new Point(0, 0);
		frmReservationsPaymentsSearchReport2.ShowDialog();
		return frmReservationsPaymentsSearchReport2.dtResult;
	}

	public static DataTable ReservationsPaymentsReturnsReport(int Deleted)
	{
		string fullName = typeof(frmReservationsPaymentsReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmReservationsPaymentsReturnsSearchReport frmReservationsPaymentsReturnsSearchReport2 = new frmReservationsPaymentsReturnsSearchReport(ReservationsPaymentsReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ReservationPaymentReturnID", Deleted);
		frmReservationsPaymentsReturnsSearchReport2.MinDate = minDate;
		frmReservationsPaymentsReturnsSearchReport2.MaxDate = maxDate;
		frmReservationsPaymentsReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmReservationsPaymentsReturnsSearchReport2.Location = new Point(0, 0);
		frmReservationsPaymentsReturnsSearchReport2.ShowDialog();
		return frmReservationsPaymentsReturnsSearchReport2.dtResult;
	}

	public static DataTable BuildingsSearchReport(bool IsFromServer)
	{
		frmBuildingsSearchReport frmBuildingsSearchReport2 = new frmBuildingsSearchReport(Buildings.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BuildingID");
		frmBuildingsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBuildingsSearchReport2.Location = new Point(0, 0);
		frmBuildingsSearchReport2.ShowDialog();
		return frmBuildingsSearchReport2.dtResult;
	}

	public static DataTable PaymentsSearchReport()
	{
		frmPaymentsSearchReport frmPaymentsSearchReport2 = new frmPaymentsSearchReport(Payments.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "PaymentID");
		frmPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPaymentsSearchReport2.Location = new Point(0, 0);
		frmPaymentsSearchReport2.ShowDialog();
		return frmPaymentsSearchReport2.dtResult;
	}

	public static DataTable ContractsInstallmentsSearchReport(string BranchIDs, string SubAccountID, string IsCanceled, string IsPaid)
	{
		frmContractsInstallmentsReport frmContractsInstallmentsReport2 = new frmContractsInstallmentsReport(ContractsInstallments.Search(BranchIDs, SubAccountID, IsCanceled, IsPaid, GlobalVariables.IsArabic ? "1" : "0"), "ContractInstallmentID");
		frmContractsInstallmentsReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmContractsInstallmentsReport2.Location = new Point(0, 0);
		frmContractsInstallmentsReport2.ShowDialog();
		return frmContractsInstallmentsReport2.dtResult;
	}

	public static DataTable ContractsSearchReport(int IsCanceled, int Deleted)
	{
		frmContractsReport frmContractsReport2 = new frmContractsReport(BusinessLayer.Constructions.Contracts.Search(IsCanceled.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ContractID");
		frmContractsReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmContractsReport2.Location = new Point(0, 0);
		frmContractsReport2.ShowDialog();
		return frmContractsReport2.dtResult;
	}

	public static int Workers(string IsActive, bool IsFromServer)
	{
		frmWorkersSearch frmWorkersSearch2 = new frmWorkersSearch(BusinessLayer.CnsProjects.Workers.Search(IsActive, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "WorkerID");
		frmWorkersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmWorkersSearch2.Location = new Point(0, 0);
		frmWorkersSearch2.ShowDialog();
		return frmWorkersSearch2.ID;
	}

	public static int CnsProjectsSearch(bool IsFromServer)
	{
		frmProjectSearch frmProjectSearch2 = new frmProjectSearch(BusinessLayer.CnsProjects.Projects.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ProjectID");
		frmProjectSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProjectSearch2.Location = new Point(0, 0);
		frmProjectSearch2.ShowDialog();
		return frmProjectSearch2.ID;
	}

	public static DataTable CnsProjectsReport(bool IsFromServer)
	{
		frmProjectSearchReport frmProjectSearchReport2 = new frmProjectSearchReport(BusinessLayer.CnsProjects.Projects.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ProjectID");
		frmProjectSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProjectSearchReport2.Location = new Point(0, 0);
		frmProjectSearchReport2.ShowDialog();
		return frmProjectSearchReport2.dtResult;
	}

	public static int CnsProjectsWithoutContracts(bool IsFromServer)
	{
		frmProjectSearch frmProjectSearch2 = new frmProjectSearch(BusinessLayer.CnsProjects.Projects.SearchWithOutContracts(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ProjectID");
		frmProjectSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProjectSearch2.Location = new Point(0, 0);
		frmProjectSearch2.ShowDialog();
		return frmProjectSearch2.ID;
	}

	public static DataTable WorkersReport(string IsActive, bool IsFromServer)
	{
		frmWorkersSearchReport frmWorkersSearchReport2 = new frmWorkersSearchReport(BusinessLayer.CnsProjects.Workers.Search(IsActive, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmWorkersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmWorkersSearchReport2.Location = new Point(0, 0);
		frmWorkersSearchReport2.ShowDialog();
		return frmWorkersSearchReport2.dtResult;
	}

	public static DataTable CnsProjectAttendanceReport(int Approved, int Deleted, bool IsFromServer)
	{
		string fullName = typeof(frmProjectAttendanceSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmProjectAttendanceSearchReport frmProjectAttendanceSearchReport2 = new frmProjectAttendanceSearchReport(ProjectsAttendances.Search(minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ProjectAttendanceID");
		frmProjectAttendanceSearchReport2.MinDate = minDate;
		frmProjectAttendanceSearchReport2.MaxDate = maxDate;
		frmProjectAttendanceSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProjectAttendanceSearchReport2.Location = new Point(0, 0);
		frmProjectAttendanceSearchReport2.ShowDialog();
		return frmProjectAttendanceSearchReport2.dtResult;
	}

	public static DataTable CnsContractsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmContractsRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmContractsRequestSearchReport frmContractsRequestSearchReport2 = new frmContractsRequestSearchReport(BusinessLayer.CnsProjects.Contracts.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ContractID", Approved, Deleted);
		frmContractsRequestSearchReport2.MinDate = minDate;
		frmContractsRequestSearchReport2.MaxDate = maxDate;
		frmContractsRequestSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmContractsRequestSearchReport2.Location = new Point(0, 0);
		frmContractsRequestSearchReport2.ShowDialog();
		return frmContractsRequestSearchReport2.dtResult;
	}

	public static int CnsContracts()
	{
		string fullName = typeof(frmContractsRequestSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmContractSearch frmContractSearch2 = new frmContractSearch(BusinessLayer.CnsProjects.Contracts.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), "-1", "0", GlobalVariables.IsArabic ? "1" : "0"), "ContractID");
		frmContractSearch2.MinDate = minDate;
		frmContractSearch2.MaxDate = maxDate;
		frmContractSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmContractSearch2.Location = new Point(0, 0);
		frmContractSearch2.ShowDialog();
		return frmContractSearch2.ID;
	}

	public static DataTable CnsContractsDuesReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmContractsDuesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmContractsDuesSearchReport frmContractsDuesSearchReport2 = new frmContractsDuesSearchReport(ContractsDues.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ContractID", Approved, Deleted);
		frmContractsDuesSearchReport2.MinDate = minDate;
		frmContractsDuesSearchReport2.MaxDate = maxDate;
		frmContractsDuesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmContractsDuesSearchReport2.Location = new Point(0, 0);
		frmContractsDuesSearchReport2.ShowDialog();
		return frmContractsDuesSearchReport2.dtResult;
	}

	public static DataTable FrontOfficeData(int Deleted)
	{
		frmFrontOfficeDataSearchReport frmFrontOfficeDataSearchReport2 = new frmFrontOfficeDataSearchReport(BusinessLayer.HMS.FrontOfficeData.Search(GlobalVariables.BranchIDs, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "FrontOfficeDataID", Deleted);
		frmFrontOfficeDataSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmFrontOfficeDataSearchReport2.Location = new Point(0, 0);
		frmFrontOfficeDataSearchReport2.ShowDialog();
		return frmFrontOfficeDataSearchReport2.dtResult;
	}

	public static DataTable VesselsSearchReport(bool IsFromServer)
	{
		frmVesselsSearchReport frmVesselsSearchReport2 = new frmVesselsSearchReport(Vessels.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "VesselID");
		frmVesselsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVesselsSearchReport2.Location = new Point(0, 0);
		frmVesselsSearchReport2.ShowDialog();
		return frmVesselsSearchReport2.dtResult;
	}

	public static int VesselsSearch(bool IsFromServer)
	{
		frmVesselsSearch frmVesselsSearch2 = new frmVesselsSearch(Vessels.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "VesselID");
		frmVesselsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVesselsSearch2.Location = new Point(0, 0);
		frmVesselsSearch2.ShowDialog();
		return frmVesselsSearch2.ID;
	}

	public static int AgentsSearch(string BranchID, bool IsFromServer)
	{
		frmSubAccountsAgentSearch frmSubAccountsAgentSearch2 = new frmSubAccountsAgentSearch(BusinessLayer.MarineService.Agents.Search(GlobalVariables.AgentSubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsAgentSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsAgentSearch2.Location = new Point(0, 0);
		frmSubAccountsAgentSearch2.ShowDialog();
		return frmSubAccountsAgentSearch2.ID;
	}

	public static int CaptainsSearch(string BranchID, bool IsFromServer)
	{
		frmCaptainsSearch frmCaptainsSearch2 = new frmCaptainsSearch(Captains.Search(GlobalVariables.CaptainSubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmCaptainsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCaptainsSearch2.Location = new Point(0, 0);
		frmCaptainsSearch2.ShowDialog();
		return frmCaptainsSearch2.ID;
	}

	public static int ChartersSearch(string BranchID, bool IsFromServer)
	{
		frmChartersSearch frmChartersSearch2 = new frmChartersSearch(Charters.Search(GlobalVariables.CharterSubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmChartersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmChartersSearch2.Location = new Point(0, 0);
		frmChartersSearch2.ShowDialog();
		return frmChartersSearch2.ID;
	}

	public static int OwnersSearch(string BranchID, bool IsFromServer)
	{
		frmOwnersSearch frmOwnersSearch2 = new frmOwnersSearch(Owners.Search(GlobalVariables.OwnerSubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmOwnersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOwnersSearch2.Location = new Point(0, 0);
		frmOwnersSearch2.ShowDialog();
		return frmOwnersSearch2.ID;
	}

	public static int SeaMenSearch(string BranchID, bool IsFromServer)
	{
		frmSeaMenSearch frmSeaMenSearch2 = new frmSeaMenSearch(SeaMen.Search(GlobalVariables.SeaManSubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSeaMenSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSeaMenSearch2.Location = new Point(0, 0);
		frmSeaMenSearch2.ShowDialog();
		return frmSeaMenSearch2.ID;
	}

	public static int ServicesSearch(bool IsFromServer)
	{
		ERP.MarineService.Search.frmServicesSearch frmServicesSearch2 = new ERP.MarineService.Search.frmServicesSearch(BusinessLayer.MarineService.Services.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ServiceID");
		frmServicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesSearch2.Location = new Point(0, 0);
		frmServicesSearch2.ShowDialog();
		return frmServicesSearch2.ID;
	}

	public static DataTable ServicesSearchReport(bool IsFromServer)
	{
		ERP.MarineService.Search.frmServicesSearchReport frmServicesSearchReport2 = new ERP.MarineService.Search.frmServicesSearchReport(BusinessLayer.MarineService.Services.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ServiceID");
		frmServicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesSearchReport2.Location = new Point(0, 0);
		frmServicesSearchReport2.ShowDialog();
		return frmServicesSearchReport2.dtResult;
	}

	public static int MSItemsQuotationsSearch(string BranchIDs, string ItemQuotationValidTo, string VesselID, int Approved, int Closed, int Deleted)
	{
		string fullName = typeof(frmItemsQuotationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmItemsQuotationsSearch frmItemsQuotationsSearch2 = new frmItemsQuotationsSearch(ItemsQuotations.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), ItemQuotationValidTo, VesselID, Approved.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ItemQuotationID", Approved, Deleted);
		frmItemsQuotationsSearch2.MinDate = minDate;
		frmItemsQuotationsSearch2.MaxDate = maxDate;
		frmItemsQuotationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsQuotationsSearch2.Location = new Point(0, 0);
		frmItemsQuotationsSearch2.ShowDialog();
		return frmItemsQuotationsSearch2.ID;
	}

	public static DataTable MSItemsQuotationsSearchReport(string ItemQuotationValidTo, string VesselID, int Approved, int Closed, int Deleted)
	{
		string fullName = typeof(frmItemsQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmItemsQuotationsSearchReport frmItemsQuotationsSearchReport2 = new frmItemsQuotationsSearchReport(ItemsQuotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), ItemQuotationValidTo, VesselID, Approved.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ItemQuotationID", Approved, Deleted);
		frmItemsQuotationsSearchReport2.MinDate = minDate;
		frmItemsQuotationsSearchReport2.MaxDate = maxDate;
		frmItemsQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmItemsQuotationsSearchReport2.Location = new Point(0, 0);
		frmItemsQuotationsSearchReport2.ShowDialog();
		return frmItemsQuotationsSearchReport2.dtResult;
	}

	public static int MSServicesQuotationsSearch(string BranchIDs, string ServiceQuotationValidTo, string VesselID, int Approved, int Closed, int Deleted)
	{
		string fullName = typeof(frmServicesQuotationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmServicesQuotationsSearch frmServicesQuotationsSearch2 = new frmServicesQuotationsSearch(BusinessLayer.MarineService.ServicesQuotations.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), ServiceQuotationValidTo, VesselID, Approved.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ServiceQuotationID", Approved, Deleted);
		frmServicesQuotationsSearch2.MinDate = minDate;
		frmServicesQuotationsSearch2.MaxDate = maxDate;
		frmServicesQuotationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesQuotationsSearch2.Location = new Point(0, 0);
		frmServicesQuotationsSearch2.ShowDialog();
		return frmServicesQuotationsSearch2.ID;
	}

	public static DataTable MSServicesQuotationsReport(string ServiceQuotationValidTo, string VesselID, int Approved, int Closed, int Deleted)
	{
		string fullName = typeof(ERP.MarineService.Search.frmServicesQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.MarineService.Search.frmServicesQuotationsSearchReport frmServicesQuotationsSearchReport2 = new ERP.MarineService.Search.frmServicesQuotationsSearchReport(BusinessLayer.MarineService.ServicesQuotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), ServiceQuotationValidTo, VesselID, Approved.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ServiceQuotationID", Approved, Deleted);
		frmServicesQuotationsSearchReport2.MinDate = minDate;
		frmServicesQuotationsSearchReport2.MaxDate = maxDate;
		frmServicesQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesQuotationsSearchReport2.Location = new Point(0, 0);
		frmServicesQuotationsSearchReport2.ShowDialog();
		return frmServicesQuotationsSearchReport2.dtResult;
	}

	public static DataTable TasksTypesSearchReport(bool IsFromServer)
	{
		frmTasksTypesSearchReport frmTasksTypesSearchReport2 = new frmTasksTypesSearchReport(TasksTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TaskTypeID");
		frmTasksTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTasksTypesSearchReport2.Location = new Point(0, 0);
		frmTasksTypesSearchReport2.ShowDialog();
		return frmTasksTypesSearchReport2.dtResult;
	}

	public static int TasksSearch(bool IsFromServer)
	{
		frmTasksSearch frmTasksSearch2 = new frmTasksSearch(Tasks.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TaskID");
		frmTasksSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTasksSearch2.Location = new Point(0, 0);
		frmTasksSearch2.ShowDialog();
		return frmTasksSearch2.ID;
	}

	public static int SeaPortsSearch(int isLoadingPort, bool IsFromServer)
	{
		ERP.MarineService.Search.frmSeaPortsSearch frmSeaPortsSearch2 = new ERP.MarineService.Search.frmSeaPortsSearch(BusinessLayer.MarineService.SeaPorts.Search(isLoadingPort.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SeaPortID");
		frmSeaPortsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSeaPortsSearch2.Location = new Point(0, 0);
		frmSeaPortsSearch2.ShowDialog();
		return frmSeaPortsSearch2.ID;
	}

	public static DataTable SeaPortsSearchReport(int isLoadingPort, bool IsFromServer)
	{
		ERP.MarineService.Search.frmSeaPortsSearchReport frmSeaPortsSearchReport2 = new ERP.MarineService.Search.frmSeaPortsSearchReport(BusinessLayer.MarineService.SeaPorts.Search(isLoadingPort.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SeaPortID");
		frmSeaPortsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSeaPortsSearchReport2.Location = new Point(0, 0);
		frmSeaPortsSearchReport2.ShowDialog();
		return frmSeaPortsSearchReport2.dtResult;
	}

	public static DataTable MS_ExpensesSearchReport(bool IsFromServer)
	{
		ERP.MarineService.Search.frmExpensesSearchReport frmExpensesSearchReport2 = new ERP.MarineService.Search.frmExpensesSearchReport(BusinessLayer.MarineService.Expenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExpenseID");
		frmExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpensesSearchReport2.Location = new Point(0, 0);
		frmExpensesSearchReport2.ShowDialog();
		return frmExpensesSearchReport2.dtResult;
	}

	public static int MS_ExpensesSearch(bool IsFromServer)
	{
		ERP.MarineService.Search.frmExpensesSearch frmExpensesSearch2 = new ERP.MarineService.Search.frmExpensesSearch(BusinessLayer.MarineService.Expenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExpenseID");
		frmExpensesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpensesSearch2.Location = new Point(0, 0);
		frmExpensesSearch2.ShowDialog();
		return frmExpensesSearch2.ID;
	}

	public static DataTable OperationsAlertSearchReport(bool IsFromServer)
	{
		frmOperationsAlertSearchReport frmOperationsAlertSearchReport2 = new frmOperationsAlertSearchReport(BusinessLayer.MarineService.Operations.Search("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmOperationsAlertSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsAlertSearchReport2.Location = new Point(0, 0);
		frmOperationsAlertSearchReport2.ShowDialog();
		return frmOperationsAlertSearchReport2.dtResult;
	}

	public static DataTable OperationsSearchReport(bool IsFromServer)
	{
		ERP.MarineService.Search.frmOperationsSearchReport frmOperationsSearchReport2 = new ERP.MarineService.Search.frmOperationsSearchReport(BusinessLayer.MarineService.Operations.Search("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmOperationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsSearchReport2.Location = new Point(0, 0);
		frmOperationsSearchReport2.ShowDialog();
		return frmOperationsSearchReport2.dtResult;
	}

	public static int OperationsSearch(bool IsFromServer)
	{
		ERP.MarineService.Search.frmOperationsSearch frmOperationsSearch2 = new ERP.MarineService.Search.frmOperationsSearch(BusinessLayer.MarineService.Operations.Search("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmOperationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsSearch2.Location = new Point(0, 0);
		frmOperationsSearch2.ShowDialog();
		return frmOperationsSearch2.ID;
	}

	public static DataTable OperationsInvoicesReport(bool IsFromServer)
	{
		frmOperationsInvoicesReport frmOperationsInvoicesReport2 = new frmOperationsInvoicesReport(OperationsInvoices.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationInvoiceID");
		frmOperationsInvoicesReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsInvoicesReport2.Location = new Point(0, 0);
		frmOperationsInvoicesReport2.ShowDialog();
		return frmOperationsInvoicesReport2.dtResult;
	}

	public static DataTable OperationsInvoicesForInvoicesPaymentsReport(int SubAccountID, string BranchIDs, int Closed)
	{
		string fullName = typeof(frmOperationsInvoicesForInvoicesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmOperationsInvoicesForInvoicesPaymentsSearchReport frmOperationsInvoicesForInvoicesPaymentsSearchReport2 = new frmOperationsInvoicesForInvoicesPaymentsSearchReport(OperationsInvoices.SearchForInvoicesPayment(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), SubAccountID.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "OperationInvoiceID");
		frmOperationsInvoicesForInvoicesPaymentsSearchReport2.MinDate = minDate;
		frmOperationsInvoicesForInvoicesPaymentsSearchReport2.MaxDate = maxDate;
		frmOperationsInvoicesForInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsInvoicesForInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmOperationsInvoicesForInvoicesPaymentsSearchReport2.ShowDialog();
		return frmOperationsInvoicesForInvoicesPaymentsSearchReport2.dtResult;
	}

	public static DataTable OperationsInvoicesPaymentsSearchReport()
	{
		frmOperationsInvoicesPaymentsSearchReport frmOperationsInvoicesPaymentsSearchReport2 = new frmOperationsInvoicesPaymentsSearchReport(OperationsInvoicesPayments.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "PaymentID");
		frmOperationsInvoicesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsInvoicesPaymentsSearchReport2.Location = new Point(0, 0);
		frmOperationsInvoicesPaymentsSearchReport2.ShowDialog();
		return frmOperationsInvoicesPaymentsSearchReport2.dtResult;
	}

	public static int OperationsInvoicesSearch(bool IsFromServer)
	{
		frmOperationsInvoicesSearch frmOperationsInvoicesSearch2 = new frmOperationsInvoicesSearch(OperationsInvoices.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationExpenseID");
		frmOperationsInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsInvoicesSearch2.Location = new Point(0, 0);
		frmOperationsInvoicesSearch2.ShowDialog();
		return frmOperationsInvoicesSearch2.ID;
	}

	public static DataTable OperationsExpensesReport(bool IsFromServer)
	{
		ERP.MarineService.Search.frmOperationsExpensesReport frmOperationsExpensesReport2 = new ERP.MarineService.Search.frmOperationsExpensesReport(BusinessLayer.MarineService.OperationsExpenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationExpenseID");
		frmOperationsExpensesReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsExpensesReport2.Location = new Point(0, 0);
		frmOperationsExpensesReport2.ShowDialog();
		return frmOperationsExpensesReport2.dtResult;
	}

	public static DataTable ProExpensesReport(bool IsFromServer)
	{
		frmProExpensesSearchReport frmProExpensesSearchReport2 = new frmProExpensesSearchReport(OperationsServicesStepsTasksExpenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServiceStepTaskExpenseID");
		frmProExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmProExpensesSearchReport2.Location = new Point(0, 0);
		frmProExpensesSearchReport2.ShowDialog();
		return frmProExpensesSearchReport2.dtResult;
	}

	public static DataTable TaskWithoutOperationReport(bool IsFromServer)
	{
		frmTasksWithoutOperationsReport frmTasksWithoutOperationsReport2 = new frmTasksWithoutOperationsReport(TaskWithoutOperation.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TaskWithoutOperationID");
		frmTasksWithoutOperationsReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTasksWithoutOperationsReport2.Location = new Point(0, 0);
		frmTasksWithoutOperationsReport2.ShowDialog();
		return frmTasksWithoutOperationsReport2.dtResult;
	}

	public static int TaskWithoutOperationSearch(bool IsFromServer)
	{
		frmTasksWithoutOperationsSearch frmTasksWithoutOperationsSearch2 = new frmTasksWithoutOperationsSearch(TaskWithoutOperation.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TaskWithoutOperationID");
		frmTasksWithoutOperationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTasksWithoutOperationsSearch2.Location = new Point(0, 0);
		frmTasksWithoutOperationsSearch2.ShowDialog();
		return frmTasksWithoutOperationsSearch2.ID;
	}

	public static DataTable VisasWithoutOperationsReport(string VesselID, bool IsFromServer)
	{
		frmVisasWithoutOperationsReport frmVisasWithoutOperationsReport2 = new frmVisasWithoutOperationsReport(OperationsServicesVisas.SearchVisasWithOutOperations(VesselID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServiceVisaID");
		frmVisasWithoutOperationsReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVisasWithoutOperationsReport2.Location = new Point(0, 0);
		frmVisasWithoutOperationsReport2.ShowDialog();
		return frmVisasWithoutOperationsReport2.dtResult;
	}

	public static DataTable RejectedVisasReport(string OperationID, string VesselID, string IsRejected, bool IsFromServer)
	{
		frmRejectedVisasSearchReport frmRejectedVisasSearchReport2 = new frmRejectedVisasSearchReport(OperationsServicesVisas.SearchByIsRejected(OperationID, VesselID, IsRejected, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServiceVisaID");
		frmRejectedVisasSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmRejectedVisasSearchReport2.Location = new Point(0, 0);
		frmRejectedVisasSearchReport2.ShowDialog();
		return frmRejectedVisasSearchReport2.dtResult;
	}

	public static DataTable CancelledVisasReport(string OperationID, string VesselID, string IsCancelled, bool IsFromServer)
	{
		frmCancelledVisasSearchReport frmCancelledVisasSearchReport2 = new frmCancelledVisasSearchReport(OperationsServicesVisas.SearchByIsCancelled(OperationID, VesselID, IsCancelled, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServiceVisaID");
		frmCancelledVisasSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCancelledVisasSearchReport2.Location = new Point(0, 0);
		frmCancelledVisasSearchReport2.ShowDialog();
		return frmCancelledVisasSearchReport2.dtResult;
	}

	public static DataTable VisasBySignOnOffReport(string OperationID, string IsSignOn, bool IsFromServer)
	{
		frmVisasWithoutOperationsReport frmVisasWithoutOperationsReport2 = new frmVisasWithoutOperationsReport(OperationsServicesVisas.SearchVisasBySignOnOff(OperationID, IsSignOn, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServiceVisaID");
		frmVisasWithoutOperationsReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmVisasWithoutOperationsReport2.Location = new Point(0, 0);
		frmVisasWithoutOperationsReport2.ShowDialog();
		return frmVisasWithoutOperationsReport2.dtResult;
	}

	public static DataTable CrewPassengersReport(string OperationID, string IsSignOn, bool IsFromServer)
	{
		frmCrewPassengersReport frmCrewPassengersReport2 = new frmCrewPassengersReport(OperationsServicesCrewPassengers.Search(OperationID, IsSignOn, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServiceCrewPassengerID");
		frmCrewPassengersReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCrewPassengersReport2.Location = new Point(0, 0);
		frmCrewPassengersReport2.ShowDialog();
		return frmCrewPassengersReport2.dtResult;
	}

	public static int MS_Clients(string BranchID, bool IsFromServer)
	{
		ERP.MarineService.Search.frmSubAccountsClientSearch frmSubAccountsClientSearch2 = new ERP.MarineService.Search.frmSubAccountsClientSearch(BusinessLayer.Accounting.SubAccounts.MarineClientsSearch(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.AgentSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.CaptainSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.SeaManSubAccountTypeIDs.Remove(0, 1), BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsClientSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsClientSearch2.Location = new Point(0, 0);
		frmSubAccountsClientSearch2.ShowDialog();
		return frmSubAccountsClientSearch2.ID;
	}

	public static DataTable MS_ClientsReport(string BranchID, bool IsFromServer)
	{
		ERP.MarineService.Search.frmSubAccountsClientSearchReport frmSubAccountsClientSearchReport2 = new ERP.MarineService.Search.frmSubAccountsClientSearchReport(BusinessLayer.Accounting.SubAccounts.MarineClientsSearch(GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.AgentSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.CaptainSubAccountTypeIDs.Remove(0, 1) + GlobalVariables.SeaManSubAccountTypeIDs.Remove(0, 1), BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmSubAccountsClientSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSubAccountsClientSearchReport2.Location = new Point(0, 0);
		frmSubAccountsClientSearchReport2.ShowDialog();
		return frmSubAccountsClientSearchReport2.dtResult;
	}

	public static string GetCPUID()
	{
		return MS.Get();
	}

	public static DataTable TechniciansSearchReport(bool IsFromServer)
	{
		frmTechniciansSearchReport frmTechniciansSearchReport2 = new frmTechniciansSearchReport(Technicians.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "Technician");
		frmTechniciansSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTechniciansSearchReport2.Location = new Point(0, 0);
		frmTechniciansSearchReport2.ShowDialog();
		return frmTechniciansSearchReport2.dtResult;
	}

	public static DataTable OwnersSearchReport(string BranchID, bool IsFromServer)
	{
		frmOwnersSearchReport frmOwnersSearchReport2 = new frmOwnersSearchReport(Owners.Search(GlobalVariables.OwnerSubAccountTypeIDs, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmOwnersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOwnersSearchReport2.Location = new Point(0, 0);
		frmOwnersSearchReport2.ShowDialog();
		return frmOwnersSearchReport2.dtResult;
	}

	public static DataTable TechniciansInwardReport(string OperationID, bool IsFromServer)
	{
		frmTechniciansInwardReport frmTechniciansInwardReport2 = new frmTechniciansInwardReport(OperationsServicesPassengersClearance.SearchByTechniciansInward(OperationID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationServicePassengerClearanceID");
		frmTechniciansInwardReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTechniciansInwardReport2.Location = new Point(0, 0);
		frmTechniciansInwardReport2.ShowDialog();
		return frmTechniciansInwardReport2.dtResult;
	}

	public static DataTable OperationExpensesForOperationExpensesPaymentsReport(int SubAccountID, string BranchIDs)
	{
		string fullName = typeof(frmOperationExpensesForOperationExpensesPaymentsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmOperationExpensesForOperationExpensesPaymentsSearchReport frmOperationExpensesForOperationExpensesPaymentsSearchReport2 = new frmOperationExpensesForOperationExpensesPaymentsSearchReport(BusinessLayer.MarineService.OperationsExpenses.SearchForInvoicesPayment(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), SubAccountID.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "OperationExpenseID");
		frmOperationExpensesForOperationExpensesPaymentsSearchReport2.MinDate = minDate;
		frmOperationExpensesForOperationExpensesPaymentsSearchReport2.MaxDate = maxDate;
		frmOperationExpensesForOperationExpensesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationExpensesForOperationExpensesPaymentsSearchReport2.Location = new Point(0, 0);
		frmOperationExpensesForOperationExpensesPaymentsSearchReport2.ShowDialog();
		return frmOperationExpensesForOperationExpensesPaymentsSearchReport2.dtResult;
	}

	public static DataTable OperationExpensesPaymentsSearchReport()
	{
		frmOperationExpensesPaymentsSearchReport frmOperationExpensesPaymentsSearchReport2 = new frmOperationExpensesPaymentsSearchReport(OperationsExpensesPayments.Search(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0"), "PaymentID");
		frmOperationExpensesPaymentsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationExpensesPaymentsSearchReport2.Location = new Point(0, 0);
		frmOperationExpensesPaymentsSearchReport2.ShowDialog();
		return frmOperationExpensesPaymentsSearchReport2.dtResult;
	}

	public static DataTable ShipChandlerSearchReport(string OperationID, int Approved, int Deleted)
	{
		string fullName = typeof(frmShipChandlerSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmShipChandlerSearchReport frmShipChandlerSearchReport2 = new frmShipChandlerSearchReport(ShipChandler.Search(OperationID, GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ShipChandlerID", Approved, Deleted);
		frmShipChandlerSearchReport2.MinDate = minDate;
		frmShipChandlerSearchReport2.MaxDate = maxDate;
		frmShipChandlerSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShipChandlerSearchReport2.Location = new Point(0, 0);
		frmShipChandlerSearchReport2.ShowDialog();
		return frmShipChandlerSearchReport2.dtResult;
	}

	public static int ShipChandlerSearch(string OperationID, int Approved, int Deleted)
	{
		string fullName = typeof(frmShipChandlerSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmShipChandlerSearch frmShipChandlerSearch2 = new frmShipChandlerSearch(ShipChandler.Search(OperationID, GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ShipChandlerID", Approved, Deleted);
		frmShipChandlerSearch2.MinDate = minDate;
		frmShipChandlerSearch2.MaxDate = maxDate;
		frmShipChandlerSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShipChandlerSearch2.Location = new Point(0, 0);
		frmShipChandlerSearch2.ShowDialog();
		return frmShipChandlerSearch2.ID;
	}

	public static DataTable ShipChandlerReturnsSearchReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmShipChandlerReturnsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmShipChandlerReturnsSearchReport frmShipChandlerReturnsSearchReport2 = new frmShipChandlerReturnsSearchReport(ShipChandlerReturns.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ShipChandlerReturnID", Approved, Deleted);
		frmShipChandlerReturnsSearchReport2.MinDate = minDate;
		frmShipChandlerReturnsSearchReport2.MaxDate = maxDate;
		frmShipChandlerReturnsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShipChandlerReturnsSearchReport2.Location = new Point(0, 0);
		frmShipChandlerReturnsSearchReport2.ShowDialog();
		return frmShipChandlerReturnsSearchReport2.dtResult;
	}

	public static DataTable ExpOperationsReport(bool IsFromServer)
	{
		frmExpOperationsSearchReport frmExpOperationsSearchReport2 = new frmExpOperationsSearchReport(BusinessLayer.Export.Operations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmExpOperationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpOperationsSearchReport2.Location = new Point(0, 0);
		frmExpOperationsSearchReport2.ShowDialog();
		return frmExpOperationsSearchReport2.dtResult;
	}

	public static int ExpOperationsSearch(bool IsFromServer)
	{
		frmExpOperationsSearch frmExpOperationsSearch2 = new frmExpOperationsSearch(BusinessLayer.Export.Operations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmExpOperationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpOperationsSearch2.Location = new Point(0, 0);
		frmExpOperationsSearch2.ShowDialog();
		return frmExpOperationsSearch2.ID;
	}

	public static DataTable ExpOperationsDeclarationsReport(bool IsFromServer)
	{
		frmExpOperationsDeclarationsSearchReport frmExpOperationsDeclarationsSearchReport2 = new frmExpOperationsDeclarationsSearchReport(OperationsDeclarations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationDeclarationID");
		frmExpOperationsDeclarationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpOperationsDeclarationsSearchReport2.Location = new Point(0, 0);
		frmExpOperationsDeclarationsSearchReport2.ShowDialog();
		return frmExpOperationsDeclarationsSearchReport2.dtResult;
	}

	public static int ExpOperationsDeclarationsSearch(bool IsFromServer)
	{
		frmExpOperationsDeclarationsSearch frmExpOperationsDeclarationsSearch2 = new frmExpOperationsDeclarationsSearch(OperationsDeclarations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationDeclarationID");
		frmExpOperationsDeclarationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpOperationsDeclarationsSearch2.Location = new Point(0, 0);
		frmExpOperationsDeclarationsSearch2.ShowDialog();
		return frmExpOperationsDeclarationsSearch2.ID;
	}

	public static int ShippersSearch(bool IsFromServer)
	{
		frmShippersSearch frmShippersSearch2 = new frmShippersSearch(Shippers.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ShipperID");
		frmShippersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShippersSearch2.Location = new Point(0, 0);
		frmShippersSearch2.ShowDialog();
		return frmShippersSearch2.ID;
	}

	public static DataTable ShippersSearchReport(bool IsFromServer)
	{
		frmShippersSearchReport frmShippersSearchReport2 = new frmShippersSearchReport(Shippers.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ShipperID");
		frmShippersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShippersSearchReport2.Location = new Point(0, 0);
		frmShippersSearchReport2.ShowDialog();
		return frmShippersSearchReport2.dtResult;
	}

	public static DataTable ExpTransportationsOrdersReport(bool IsFromServer)
	{
		frmExpTransportationsOrdersSearchReport frmExpTransportationsOrdersSearchReport2 = new frmExpTransportationsOrdersSearchReport(TransportationsOrders.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TransportationOrderID");
		frmExpTransportationsOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpTransportationsOrdersSearchReport2.Location = new Point(0, 0);
		frmExpTransportationsOrdersSearchReport2.ShowDialog();
		return frmExpTransportationsOrdersSearchReport2.dtResult;
	}

	public static int TransportationsPlacesSearch(bool IsFromServer)
	{
		frmTransportationsPlacesSearch frmTransportationsPlacesSearch2 = new frmTransportationsPlacesSearch(TransportationsPlaces.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TransportationPlaceID");
		frmTransportationsPlacesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTransportationsPlacesSearch2.Location = new Point(0, 0);
		frmTransportationsPlacesSearch2.ShowDialog();
		return frmTransportationsPlacesSearch2.ID;
	}

	public static int TransportersSearch(string BranchID, bool IsFromServer)
	{
		frmTransportersSearch frmTransportersSearch2 = new frmTransportersSearch(Transporters.Search(BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "TransporterID");
		frmTransportersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmTransportersSearch2.Location = new Point(0, 0);
		frmTransportersSearch2.ShowDialog();
		return frmTransportersSearch2.ID;
	}

	public static int OperationsDeclarationsContainersSearch(string TransportationOrderID, string BranchID, bool IsFromServer)
	{
		frmEXPOperationsDeclarationsContainersSearch frmEXPOperationsDeclarationsContainersSearch2 = new frmEXPOperationsDeclarationsContainersSearch(OperationsDeclarationsContainers.Search(TransportationOrderID, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationDeclarationContainerID");
		frmEXPOperationsDeclarationsContainersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEXPOperationsDeclarationsContainersSearch2.Location = new Point(0, 0);
		frmEXPOperationsDeclarationsContainersSearch2.ShowDialog();
		return frmEXPOperationsDeclarationsContainersSearch2.ID;
	}

	public static DataTable OperationsDeclarationsContainersSearchReport(string TransportationOrderID, string BranchID, bool IsFromServer)
	{
		frmEXPOperationsDeclarationsContainersSearchReport frmEXPOperationsDeclarationsContainersSearchReport2 = new frmEXPOperationsDeclarationsContainersSearchReport(OperationsDeclarationsContainers.Search(TransportationOrderID, BranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationDeclarationContainerID");
		frmEXPOperationsDeclarationsContainersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmEXPOperationsDeclarationsContainersSearchReport2.Location = new Point(0, 0);
		frmEXPOperationsDeclarationsContainersSearchReport2.ShowDialog();
		return frmEXPOperationsDeclarationsContainersSearchReport2.dtResult;
	}

	public static DataTable EXPDeclarationsInvoicesReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmOperationsDeclarationsInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmOperationsDeclarationsInvoicesSearchReport frmOperationsDeclarationsInvoicesSearchReport2 = new frmOperationsDeclarationsInvoicesSearchReport(OperationsDeclarationsInvoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmOperationsDeclarationsInvoicesSearchReport2.MinDate = minDate;
		frmOperationsDeclarationsInvoicesSearchReport2.MaxDate = maxDate;
		frmOperationsDeclarationsInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsDeclarationsInvoicesSearchReport2.Location = new Point(0, 0);
		frmOperationsDeclarationsInvoicesSearchReport2.ShowDialog();
		return frmOperationsDeclarationsInvoicesSearchReport2.dtResult;
	}

	public static DataTable WireTypesSearchReport(bool IsFromServer)
	{
		frmWireTypesSearchReport frmWireTypesSearchReport2 = new frmWireTypesSearchReport(WireTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "WireTypeID");
		frmWireTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmWireTypesSearchReport2.Location = new Point(0, 0);
		frmWireTypesSearchReport2.ShowDialog();
		return frmWireTypesSearchReport2.dtResult;
	}

	public static int WireTypesSearch(bool IsFromServer)
	{
		frmWireTypesSearch frmWireTypesSearch2 = new frmWireTypesSearch(WireTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "WireTypeID");
		frmWireTypesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmWireTypesSearch2.Location = new Point(0, 0);
		frmWireTypesSearch2.ShowDialog();
		return frmWireTypesSearch2.ID;
	}

	public static DataTable MasterLinksSuppliersSearchReport(bool IsFromServer)
	{
		frmMasterLinksSuppliersSearchReport frmMasterLinksSuppliersSearchReport2 = new frmMasterLinksSuppliersSearchReport(MasterLinksSuppliers.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "MasterLinkSupplierID");
		frmMasterLinksSuppliersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMasterLinksSuppliersSearchReport2.Location = new Point(0, 0);
		frmMasterLinksSuppliersSearchReport2.ShowDialog();
		return frmMasterLinksSuppliersSearchReport2.dtResult;
	}

	public static int MasterLinksSuppliersSearch(bool IsFromServer)
	{
		frmMasterLinksSuppliersSearch frmMasterLinksSuppliersSearch2 = new frmMasterLinksSuppliersSearch(MasterLinksSuppliers.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "MasterLinkSupplierID");
		frmMasterLinksSuppliersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMasterLinksSuppliersSearch2.Location = new Point(0, 0);
		frmMasterLinksSuppliersSearch2.ShowDialog();
		return frmMasterLinksSuppliersSearch2.ID;
	}

	public static DataTable ShackleTypesSearchReport(bool IsFromServer)
	{
		frmShackleTypesSearchReport frmShackleTypesSearchReport2 = new frmShackleTypesSearchReport(ShackleTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ShackleTypeID");
		frmShackleTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmShackleTypesSearchReport2.Location = new Point(0, 0);
		frmShackleTypesSearchReport2.ShowDialog();
		return frmShackleTypesSearchReport2.dtResult;
	}

	public static DataTable HooksTypesSearchReport(bool IsFromServer)
	{
		frmHooksTypesSearchReport frmHooksTypesSearchReport2 = new frmHooksTypesSearchReport(HooksTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "HookTypeID");
		frmHooksTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmHooksTypesSearchReport2.Location = new Point(0, 0);
		frmHooksTypesSearchReport2.ShowDialog();
		return frmHooksTypesSearchReport2.dtResult;
	}

	public static DataTable SLNPreQuotationsReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmPreQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPreQuotationsSearchReport frmPreQuotationsSearchReport2 = new frmPreQuotationsSearchReport(PreQuotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PreQuotationID", Approved, Deleted);
		frmPreQuotationsSearchReport2.MinDate = minDate;
		frmPreQuotationsSearchReport2.MaxDate = maxDate;
		frmPreQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPreQuotationsSearchReport2.Location = new Point(0, 0);
		frmPreQuotationsSearchReport2.ShowDialog();
		return frmPreQuotationsSearchReport2.dtResult;
	}

	public static int SLNPreQuotationsSearch(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmPreQuotationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmPreQuotationsSearch frmPreQuotationsSearch2 = new frmPreQuotationsSearch(PreQuotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "PreQuotationID", Approved, Deleted);
		frmPreQuotationsSearch2.MinDate = minDate;
		frmPreQuotationsSearch2.MaxDate = maxDate;
		frmPreQuotationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPreQuotationsSearch2.Location = new Point(0, 0);
		frmPreQuotationsSearch2.ShowDialog();
		return frmPreQuotationsSearch2.ID;
	}

	public static DataTable SLNQuotationsReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(ERP.Sling.Search.frmQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Sling.Search.frmQuotationsSearchReport frmQuotationsSearchReport2 = new ERP.Sling.Search.frmQuotationsSearchReport(BusinessLayer.Sling.Quotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearchReport2.MinDate = minDate;
		frmQuotationsSearchReport2.MaxDate = maxDate;
		frmQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearchReport2.Location = new Point(0, 0);
		frmQuotationsSearchReport2.ShowDialog();
		return frmQuotationsSearchReport2.dtResult;
	}

	public static DataTable SLNQuotationsReportByClientID(string ClientID, string CurrencyID, int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(ERP.Sales.Search.frmQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Sling.Search.frmQuotationsSearchReport frmQuotationsSearchReport2 = new ERP.Sling.Search.frmQuotationsSearchReport(BusinessLayer.Sling.Quotations.SearchByClientID(ClientID, CurrencyID, GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearchReport2.MinDate = minDate;
		frmQuotationsSearchReport2.MaxDate = maxDate;
		frmQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearchReport2.Location = new Point(0, 0);
		frmQuotationsSearchReport2.ShowDialog();
		return frmQuotationsSearchReport2.dtResult;
	}

	public static int SLNQuotationsSearch(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(ERP.Sling.Search.frmQuotationsSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.Sling.Search.frmQuotationsSearch frmQuotationsSearch2 = new ERP.Sling.Search.frmQuotationsSearch(BusinessLayer.Sling.Quotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationID", Approved, Deleted);
		frmQuotationsSearch2.MinDate = minDate;
		frmQuotationsSearch2.MaxDate = maxDate;
		frmQuotationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsSearch2.Location = new Point(0, 0);
		frmQuotationsSearch2.ShowDialog();
		return frmQuotationsSearch2.ID;
	}

	public static DataTable SLNQuotationsCertificatesReport(string QuotationID, int Deleted)
	{
		string fullName = typeof(frmQuotationsCertificatesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmQuotationsCertificatesSearchReport frmQuotationsCertificatesSearchReport2 = new frmQuotationsCertificatesSearchReport(QuotationsCertificates.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), QuotationID, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationCertificateID", Deleted);
		frmQuotationsCertificatesSearchReport2.MinDate = minDate;
		frmQuotationsCertificatesSearchReport2.MaxDate = maxDate;
		frmQuotationsCertificatesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsCertificatesSearchReport2.Location = new Point(0, 0);
		frmQuotationsCertificatesSearchReport2.ShowDialog();
		return frmQuotationsCertificatesSearchReport2.dtResult;
	}

	public static DataTable SLNCertificateDeliveryNotesReport(string QuotationID, int Deleted)
	{
		string fullName = typeof(frmCertificatesDeliveryNotesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmCertificatesDeliveryNotesSearchReport frmCertificatesDeliveryNotesSearchReport2 = new frmCertificatesDeliveryNotesSearchReport(CertificatesDeliveryNotes.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), QuotationID, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "CertificateDeliveryNoteID", Deleted);
		frmCertificatesDeliveryNotesSearchReport2.MinDate = minDate;
		frmCertificatesDeliveryNotesSearchReport2.MaxDate = maxDate;
		frmCertificatesDeliveryNotesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCertificatesDeliveryNotesSearchReport2.Location = new Point(0, 0);
		frmCertificatesDeliveryNotesSearchReport2.ShowDialog();
		return frmCertificatesDeliveryNotesSearchReport2.dtResult;
	}

	public static DataTable SLNQuotationDeliveryNotesReport(string QuotationID, int Deleted)
	{
		string fullName = typeof(frmQuotationsDeliveryNotesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmQuotationsDeliveryNotesSearchReport frmQuotationsDeliveryNotesSearchReport2 = new frmQuotationsDeliveryNotesSearchReport(QuotationsDeliveryNotes.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), QuotationID, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "QuotationDeliveryNoteID", Deleted);
		frmQuotationsDeliveryNotesSearchReport2.MinDate = minDate;
		frmQuotationsDeliveryNotesSearchReport2.MaxDate = maxDate;
		frmQuotationsDeliveryNotesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmQuotationsDeliveryNotesSearchReport2.Location = new Point(0, 0);
		frmQuotationsDeliveryNotesSearchReport2.ShowDialog();
		return frmQuotationsDeliveryNotesSearchReport2.dtResult;
	}

	public static DataTable SLNInvoicesReport(string QuotationID, int Deleted)
	{
		string fullName = typeof(frmInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmInvoicesSearchReport frmInvoicesSearchReport2 = new frmInvoicesSearchReport(BusinessLayer.Sling.Invoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), QuotationID, Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "InvoiceID", Deleted);
		frmInvoicesSearchReport2.MinDate = minDate;
		frmInvoicesSearchReport2.MaxDate = maxDate;
		frmInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmInvoicesSearchReport2.Location = new Point(0, 0);
		frmInvoicesSearchReport2.ShowDialog();
		return frmInvoicesSearchReport2.dtResult;
	}

	public static int AssetsLocations(bool IsFromServer)
	{
		frmAssetsLocationsSearch frmAssetsLocationsSearch2 = new frmAssetsLocationsSearch(BusinessLayer.FixedAssets.AssetsLocations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AssetLocationID");
		frmAssetsLocationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAssetsLocationsSearch2.Location = new Point(0, 0);
		frmAssetsLocationsSearch2.ShowDialog();
		return frmAssetsLocationsSearch2.ID;
	}

	public static int Assets(string AssetStateID, bool IsFromServer)
	{
		frmAssetsSearch frmAssetsSearch2 = new frmAssetsSearch(BusinessLayer.FixedAssets.Assets.Search(GlobalVariables.AssetSubAccountTypeIDs, AssetStateID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmAssetsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAssetsSearch2.Location = new Point(0, 0);
		frmAssetsSearch2.ShowDialog();
		return frmAssetsSearch2.ID;
	}

	public static DataTable AssetsReport(string AssetStateID, bool IsFromServer)
	{
		frmAssetsSearchReport frmAssetsSearchReport2 = new frmAssetsSearchReport(BusinessLayer.FixedAssets.Assets.Search(GlobalVariables.AssetSubAccountTypeIDs, AssetStateID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SubAccountID");
		frmAssetsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAssetsSearchReport2.Location = new Point(0, 0);
		frmAssetsSearchReport2.ShowDialog();
		return frmAssetsSearchReport2.dtResult;
	}

	public static DataTable AssetsAcquisitionsReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmAssetsAcquisitionsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmAssetsAcquisitionsSearchReport frmAssetsAcquisitionsSearchReport2 = new frmAssetsAcquisitionsSearchReport(AssetsAcquisitions.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "AssetAcquisitionID", Approved, Deleted);
		frmAssetsAcquisitionsSearchReport2.MinDate = minDate;
		frmAssetsAcquisitionsSearchReport2.MaxDate = maxDate;
		frmAssetsAcquisitionsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAssetsAcquisitionsSearchReport2.Location = new Point(0, 0);
		frmAssetsAcquisitionsSearchReport2.ShowDialog();
		return frmAssetsAcquisitionsSearchReport2.dtResult;
	}

	public static DataTable DepreciationsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmDepreciationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmDepreciationsSearchReport frmDepreciationsSearchReport2 = new frmDepreciationsSearchReport(Depreciations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "DepreciationID", Approved, Deleted);
		frmDepreciationsSearchReport2.MinDate = minDate;
		frmDepreciationsSearchReport2.MaxDate = maxDate;
		frmDepreciationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDepreciationsSearchReport2.Location = new Point(0, 0);
		frmDepreciationsSearchReport2.ShowDialog();
		return frmDepreciationsSearchReport2.dtResult;
	}

	public static DataTable AssetsSalesReport(int Approved, int Deleted, int Closed)
	{
		string fullName = typeof(frmAssetsSalesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmAssetsSalesSearchReport frmAssetsSalesSearchReport2 = new frmAssetsSalesSearchReport(AssetsSales.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "AssetSaleID", Approved, Deleted);
		frmAssetsSalesSearchReport2.MinDate = minDate;
		frmAssetsSalesSearchReport2.MaxDate = maxDate;
		frmAssetsSalesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAssetsSalesSearchReport2.Location = new Point(0, 0);
		frmAssetsSalesSearchReport2.ShowDialog();
		return frmAssetsSalesSearchReport2.dtResult;
	}

	public static int CSTServicesSearch(bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmServicesSearch frmServicesSearch2 = new ERP.CustomsClearence.Search.frmServicesSearch(BusinessLayer.CustomsClearence.Services.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ServiceID");
		frmServicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesSearch2.Location = new Point(0, 0);
		frmServicesSearch2.ShowDialog();
		return frmServicesSearch2.ID;
	}

	public static DataTable CSTServicesSearchReport(bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmServicesSearchReport frmServicesSearchReport2 = new ERP.CustomsClearence.Search.frmServicesSearchReport(BusinessLayer.CustomsClearence.Services.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ServiceID");
		frmServicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesSearchReport2.Location = new Point(0, 0);
		frmServicesSearchReport2.ShowDialog();
		return frmServicesSearchReport2.dtResult;
	}

	public static DataTable CST_ExpensesSearchReport(bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmExpensesSearchReport frmExpensesSearchReport2 = new ERP.CustomsClearence.Search.frmExpensesSearchReport(BusinessLayer.CustomsClearence.Expenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExpenseID");
		frmExpensesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExpensesSearchReport2.Location = new Point(0, 0);
		frmExpensesSearchReport2.ShowDialog();
		return frmExpensesSearchReport2.dtResult;
	}

	public static DataTable CSTServicesQuotationsReport(string ServiceQuotationValidTo, int Approved, int Closed, int Deleted)
	{
		string fullName = typeof(ERP.CustomsClearence.Search.frmServicesQuotationsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		ERP.CustomsClearence.Search.frmServicesQuotationsSearchReport frmServicesQuotationsSearchReport2 = new ERP.CustomsClearence.Search.frmServicesQuotationsSearchReport(BusinessLayer.CustomsClearence.ServicesQuotations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), ServiceQuotationValidTo, Approved.ToString(), Closed.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "ServiceQuotationID", Approved, Deleted);
		frmServicesQuotationsSearchReport2.MinDate = minDate;
		frmServicesQuotationsSearchReport2.MaxDate = maxDate;
		frmServicesQuotationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmServicesQuotationsSearchReport2.Location = new Point(0, 0);
		frmServicesQuotationsSearchReport2.ShowDialog();
		return frmServicesQuotationsSearchReport2.dtResult;
	}

	public static DataTable CSTOperationsExpensesReport(bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmOperationsExpensesReport frmOperationsExpensesReport2 = new ERP.CustomsClearence.Search.frmOperationsExpensesReport(BusinessLayer.CustomsClearence.OperationsExpenses.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationExpenseID");
		frmOperationsExpensesReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsExpensesReport2.Location = new Point(0, 0);
		frmOperationsExpensesReport2.ShowDialog();
		return frmOperationsExpensesReport2.dtResult;
	}

	public static int CSTOperationsSearch(int Approved, bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmOperationsSearch frmOperationsSearch2 = new ERP.CustomsClearence.Search.frmOperationsSearch(BusinessLayer.CustomsClearence.Operations.Search(Approved.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmOperationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsSearch2.Location = new Point(0, 0);
		frmOperationsSearch2.ShowDialog();
		return frmOperationsSearch2.ID;
	}

	public static int CSTConcentrationsSearch(int Approved, bool IsFromServer)
	{
		frmConcentrationsSearch frmConcentrationsSearch2 = new frmConcentrationsSearch(Concentrations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ConcentrationID");
		frmConcentrationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmConcentrationsSearch2.Location = new Point(0, 0);
		frmConcentrationsSearch2.ShowDialog();
		return frmConcentrationsSearch2.ID;
	}

	public static int CSTDurationAndTemperaturesSearch(int Approved, bool IsFromServer)
	{
		frmDurationAndTemperaturesSearch frmDurationAndTemperaturesSearch2 = new frmDurationAndTemperaturesSearch(DurationAndTemperatures.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "DurationAndTemperatureID");
		frmDurationAndTemperaturesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDurationAndTemperaturesSearch2.Location = new Point(0, 0);
		frmDurationAndTemperaturesSearch2.ShowDialog();
		return frmDurationAndTemperaturesSearch2.ID;
	}

	public static int CSTAdditionalDeclarationsSearch(int Approved, bool IsFromServer)
	{
		frmAdditionalDeclarationsSearch frmAdditionalDeclarationsSearch2 = new frmAdditionalDeclarationsSearch(AdditionalDeclarations.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "AdditionalDeclarationID");
		frmAdditionalDeclarationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmAdditionalDeclarationsSearch2.Location = new Point(0, 0);
		frmAdditionalDeclarationsSearch2.ShowDialog();
		return frmAdditionalDeclarationsSearch2.ID;
	}

	public static DataTable CSTOperationsReport(int Approved, int Closed, bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmOperationsSearchReport frmOperationsSearchReport2 = new ERP.CustomsClearence.Search.frmOperationsSearchReport(BusinessLayer.CustomsClearence.Operations.Search(Approved.ToString(), Closed.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "OperationID");
		frmOperationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsSearchReport2.Location = new Point(0, 0);
		frmOperationsSearchReport2.ShowDialog();
		return frmOperationsSearchReport2.dtResult;
	}

	public static DataTable CSTOperationsReport(string BranchIDs, DateTime FromDate, DateTime ToDate, string Closed, string Deleted)
	{
		ERP.CustomsClearence.Search.frmOperationsSearchReport frmOperationsSearchReport2 = new ERP.CustomsClearence.Search.frmOperationsSearchReport(BusinessLayer.CustomsClearence.Operations.Search(BranchIDs, FromDate.ToString(GlobalVariables.DateLongFormate), ToDate.ToString(GlobalVariables.DateLongFormate), Closed, Deleted, GlobalVariables.IsArabic ? "1" : "0"), "OperationID");
		frmOperationsSearchReport2.MinDate = FromDate;
		frmOperationsSearchReport2.MaxDate = ToDate;
		frmOperationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOperationsSearchReport2.Location = new Point(0, 0);
		frmOperationsSearchReport2.ShowDialog();
		return frmOperationsSearchReport2.dtResult;
	}

	public static DataTable CSTSeaPortsSearchReport(bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmSeaPortsSearchReport frmSeaPortsSearchReport2 = new ERP.CustomsClearence.Search.frmSeaPortsSearchReport(BusinessLayer.CustomsClearence.SeaPorts.Search(-1, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SeaPortID");
		frmSeaPortsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSeaPortsSearchReport2.Location = new Point(0, 0);
		frmSeaPortsSearchReport2.ShowDialog();
		return frmSeaPortsSearchReport2.dtResult;
	}

	public static int CSTSeaPortsSearch(int CountryID, bool IsFromServer)
	{
		ERP.CustomsClearence.Search.frmSeaPortsSearch frmSeaPortsSearch2 = new ERP.CustomsClearence.Search.frmSeaPortsSearch(BusinessLayer.CustomsClearence.SeaPorts.Search(CountryID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "SeaPortID");
		frmSeaPortsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmSeaPortsSearch2.Location = new Point(0, 0);
		frmSeaPortsSearch2.ShowDialog();
		return frmSeaPortsSearch2.ID;
	}

	public static DataTable CSTExportTypesSearchReport(bool IsFromServer)
	{
		frmExportTypesSearchReport frmExportTypesSearchReport2 = new frmExportTypesSearchReport(ExportsTypes.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExportTypeID");
		frmExportTypesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExportTypesSearchReport2.Location = new Point(0, 0);
		frmExportTypesSearchReport2.ShowDialog();
		return frmExportTypesSearchReport2.dtResult;
	}

	public static DataTable CSTInvoicesReport(bool IsFromServer)
	{
		frmInvoicesReport frmInvoicesReport2 = new frmInvoicesReport(BusinessLayer.CustomsClearence.Invoices.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "InvoiceID");
		frmInvoicesReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmInvoicesReport2.Location = new Point(0, 0);
		frmInvoicesReport2.ShowDialog();
		return frmInvoicesReport2.dtResult;
	}

	public static DataTable CSTBillsOfLadingReport(bool IsFromServer)
	{
		frmBillsOfLadingReport frmBillsOfLadingReport2 = new frmBillsOfLadingReport(BillsOfLading.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "BillOfLadingID");
		frmBillsOfLadingReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmBillsOfLadingReport2.Location = new Point(0, 0);
		frmBillsOfLadingReport2.ShowDialog();
		return frmBillsOfLadingReport2.dtResult;
	}

	public static DataTable CSTPhytosanitaryCertificatesReport(bool IsFromServer)
	{
		frmPhytosanitaryCertificatesReport frmPhytosanitaryCertificatesReport2 = new frmPhytosanitaryCertificatesReport(PhytosanitaryCertificates.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "PhytosanitaryCertificateID");
		frmPhytosanitaryCertificatesReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmPhytosanitaryCertificatesReport2.Location = new Point(0, 0);
		frmPhytosanitaryCertificatesReport2.ShowDialog();
		return frmPhytosanitaryCertificatesReport2.dtResult;
	}

	public static DataTable CSTCertificatesOfOriginReport(bool IsFromServer)
	{
		frmCertificatesOfOriginReport frmCertificatesOfOriginReport2 = new frmCertificatesOfOriginReport(CertificatesOfOrigin.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CertificateOfOriginID");
		frmCertificatesOfOriginReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCertificatesOfOriginReport2.Location = new Point(0, 0);
		frmCertificatesOfOriginReport2.ShowDialog();
		return frmCertificatesOfOriginReport2.dtResult;
	}

	public static int CSTExportersSearch(bool IsFromServer)
	{
		frmExportersSearch frmExportersSearch2 = new frmExportersSearch(Exporters.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ExportersID");
		frmExportersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmExportersSearch2.Location = new Point(0, 0);
		frmExportersSearch2.ShowDialog();
		return frmExportersSearch2.ID;
	}

	public static int CSTConsigneesSearch(bool IsFromServer)
	{
		frmConsigneesSearch frmConsigneesSearch2 = new frmConsigneesSearch(Consignees.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "ConsigneeID");
		frmConsigneesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmConsigneesSearch2.Location = new Point(0, 0);
		frmConsigneesSearch2.ShowDialog();
		return frmConsigneesSearch2.ID;
	}

	public static DataTable CustomsBrokersSearchReport(bool IsFromServer)
	{
		frmCustomsBrokersSearchReport frmCustomsBrokersSearchReport2 = new frmCustomsBrokersSearchReport(CustomsBrokers.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CustomsBrokerID");
		frmCustomsBrokersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustomsBrokersSearchReport2.Location = new Point(0, 0);
		frmCustomsBrokersSearchReport2.ShowDialog();
		return frmCustomsBrokersSearchReport2.dtResult;
	}

	public static DataTable LnsAOrdersReport(int Deleted)
	{
		string fullName = typeof(frmOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmOrdersSearchReport frmOrdersSearchReport2 = new frmOrdersSearchReport(Orders.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "OrderID", Deleted);
		frmOrdersSearchReport2.MinDate = minDate;
		frmOrdersSearchReport2.MaxDate = maxDate;
		frmOrdersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmOrdersSearchReport2.Location = new Point(0, 0);
		frmOrdersSearchReport2.ShowDialog();
		return frmOrdersSearchReport2.dtResult;
	}

	public static DataTable CRMCustomersReport(int IsClosed, int Deleted, bool IsFromServer)
	{
		frmCustomersSearchReport frmCustomersSearchReport2 = new frmCustomersSearchReport(Customers.Search(IsClosed.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CustomerID", Deleted);
		frmCustomersSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustomersSearchReport2.Location = new Point(0, 0);
		frmCustomersSearchReport2.ShowDialog();
		return frmCustomersSearchReport2.dtResult;
	}

	public static int CRMCustomers(int IsClosed, bool IsFromServer)
	{
		frmCustomersSearch frmCustomersSearch2 = new frmCustomersSearch(Customers.Search(IsClosed.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CustomerID");
		frmCustomersSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustomersSearch2.Location = new Point(0, 0);
		frmCustomersSearch2.ShowDialog();
		return frmCustomersSearch2.ID;
	}

	public static int CRMCommunicationSteps(bool IsFromServer)
	{
		frmCommunicationStepsSearch frmCommunicationStepsSearch2 = new frmCommunicationStepsSearch(CommunicationSteps.Search(GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "CommunicationStepID");
		frmCommunicationStepsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCommunicationStepsSearch2.Location = new Point(0, 0);
		frmCommunicationStepsSearch2.ShowDialog();
		return frmCommunicationStepsSearch2.ID;
	}

	public static DataTable CRMCustomersFollows(int Approved, int Deleted)
	{
		string fullName = typeof(frmCustomersFollowsSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmCustomersFollowsSearchReport frmCustomersFollowsSearchReport2 = new frmCustomersFollowsSearchReport(CustomersFollows.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "CustomerFollowID", Deleted);
		frmCustomersFollowsSearchReport2.MinDate = minDate;
		frmCustomersFollowsSearchReport2.MaxDate = maxDate;
		frmCustomersFollowsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustomersFollowsSearchReport2.Location = new Point(0, 0);
		frmCustomersFollowsSearchReport2.ShowDialog();
		return frmCustomersFollowsSearchReport2.dtResult;
	}

	public static int CustomersReservationsSearch(string BranchIDs, int Approved, int Deleted)
	{
		string fullName = typeof(frmSLOrdersSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmCustomersReservationsSearch frmCustomersReservationsSearch2 = new frmCustomersReservationsSearch(CustomersReservations.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "CustomerReservationID", Approved, Deleted);
		frmCustomersReservationsSearch2.MinDate = minDate;
		frmCustomersReservationsSearch2.MaxDate = maxDate;
		frmCustomersReservationsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustomersReservationsSearch2.Location = new Point(0, 0);
		frmCustomersReservationsSearch2.ShowDialog();
		return frmCustomersReservationsSearch2.ID;
	}

	public static DataTable CustomersReservationsReport(int Approved, int Deleted)
	{
		string fullName = typeof(frmSLOrdersSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmCustomersReservationsSearchReport frmCustomersReservationsSearchReport2 = new frmCustomersReservationsSearchReport(CustomersReservations.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "CustomerReservationID", Approved, Deleted);
		frmCustomersReservationsSearchReport2.MinDate = minDate;
		frmCustomersReservationsSearchReport2.MaxDate = maxDate;
		frmCustomersReservationsSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmCustomersReservationsSearchReport2.Location = new Point(0, 0);
		frmCustomersReservationsSearchReport2.ShowDialog();
		return frmCustomersReservationsSearchReport2.dtResult;
	}

	public static int ESLInvoicesSearch(string BranchIDs, int Approved, int Deleted, int Closed, int WithoutMIV)
	{
		string fullName = typeof(frmSLInvoicesSearch).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmESLInvoicesSearch frmESLInvoicesSearch2 = new frmESLInvoicesSearch(SLInvoices.Search(BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), "1", WithoutMIV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmESLInvoicesSearch2.MinDate = minDate;
		frmESLInvoicesSearch2.MaxDate = maxDate;
		frmESLInvoicesSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmESLInvoicesSearch2.Location = new Point(0, 0);
		frmESLInvoicesSearch2.ShowDialog();
		return frmESLInvoicesSearch2.ID;
	}

	public static DataTable ESLInvoicesReport(int Approved, int Deleted, int Closed, int WithoutMIV)
	{
		string fullName = typeof(frmSLInvoicesSearchReport).FullName;
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, fullName, IsFromServer: false);
		DateTime minDate = DateTime.Now.AddDays(-((dataTable.Rows.Count > 0) ? Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]) : 10000));
		DateTime maxDate = DateTime.Now.AddYears(50);
		frmESLInvoicesSearchReport frmESLInvoicesSearchReport2 = new frmESLInvoicesSearchReport(SLInvoices.Search(GlobalVariables.BranchIDs, minDate.ToString(GlobalVariables.DateShortFormate), maxDate.ToString(GlobalVariables.DateShortFormate), Approved.ToString(), Deleted.ToString(), Closed.ToString(), "1", WithoutMIV.ToString(), GlobalVariables.IsArabic ? "1" : "0"), "SLInvoiceID", Approved, Deleted);
		frmESLInvoicesSearchReport2.MinDate = minDate;
		frmESLInvoicesSearchReport2.MaxDate = maxDate;
		frmESLInvoicesSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmESLInvoicesSearchReport2.Location = new Point(0, 0);
		frmESLInvoicesSearchReport2.ShowDialog();
		return frmESLInvoicesSearchReport2.dtResult;
	}

	public static int MessagesLog(string BranchIDs, bool IsFromServer)
	{
		frmMessagesLogSearch frmMessagesLogSearch2 = new frmMessagesLogSearch(BusinessLayer.SMS.MessagesLog.Search(BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "MessageLogID");
		frmMessagesLogSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMessagesLogSearch2.Location = new Point(0, 0);
		frmMessagesLogSearch2.ShowDialog();
		return frmMessagesLogSearch2.ID;
	}

	public static DataTable MessagesLogReport(string BranchIDs, bool IsFromServer)
	{
		frmMessagesLogSearchReport frmMessagesLogSearchReport2 = new frmMessagesLogSearchReport(BusinessLayer.SMS.MessagesLog.Search(BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer), "MessageLogID");
		frmMessagesLogSearchReport2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmMessagesLogSearchReport2.Location = new Point(0, 0);
		frmMessagesLogSearchReport2.ShowDialog();
		return frmMessagesLogSearchReport2.dtResult;
	}
}
