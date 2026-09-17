using System;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.MessageBoxes;

namespace ERP.Classes;

public class GlobalVariables
{
	public static string DateLongFormateMS = "MM'/'dd'/'yyyy HH:mm:ss.fff";

	public static string DateLongFormate = "MM'/'dd'/'yyyy HH:mm:ss";

	public static string DateShortFormate = "MM'/'dd'/'yyyy";

	public static string txtDecimalFormate = "0.##########";

	public static string VersionID = "3";

	public static string BranchIDs = "";

	public static string SafeIDs;

	public static string StoreIDs;

	public static string CurrentBranchID;

	public static string CurrentBranchNameEn;

	public static string CompanyNameEn;

	public static string CompanyNameAr;

	public static string AssetSubAccountTypeIDs = ",1,";

	public static string ClientSubAccountTypeIDs = ",4,5,7,";

	public static string SupplierSubAccountTypeIDs = ",3,5,";

	public static string EmployeeSubAccountTypeIDs = ",2,7,";

	public static string AgentSubAccountTypeIDs = ",8,";

	public static string OwnerSubAccountTypeIDs = ",9,";

	public static string CharterSubAccountTypeIDs = ",10,";

	public static string CaptainSubAccountTypeIDs = ",11,";

	public static string SeaManSubAccountTypeIDs = ",12,";

	public static string RemoteSessionName = "";

	public static DataTable dtSyncConn;

	public static bool IsRemoteServer;

	public static string ArchivingPath = "";

	public static bool LoadApplication = false;

	public static string ServerID;

	public static string Server;

	public static string dbUserID;

	public static string dbPassword;

	public static string DatabaseName;

	public static string path = "Software\\SmartSolution\\ERP";

	public static string POSPrinter;

	public static bool IsArabic;

	public static int ScrollWidth = 45;

	public static string Font = "Arial";

	public static int LocalCurrencyID = 1;

	public static double AddedTax = 0.01;

	public static double DiscountTax = 0.01;

	public static string Style = "ISSDarkBlue";

	public static DateTime MinOpenedDate;

	public static string UserLoginID;

	public static string GroupID;

	public static string UserID;

	public static bool IsTechnicalUser = false;

	public static string UserName;

	public static string Password;

	public static DataTable dtAllForms;

	public static DataTable dtForms;

	public static DataTable dtFunctions;

	public static DataTable dtFormFunctions;

	public static int DataAccess = 1;

	public static bool SeeingInvisibleAcounts;

	public static bool SeeingClosedYears;

	public static int ScreenWidth;

	public static int ScreenHeight;

	public static DataTable ResultTable = new DataTable();

	public static ReportDocument ReportDocument = new ReportDocument();

	public static TableLogOnInfo LogonInfo;

	public static string ReportsPath;

	public static string QtyDecimals = "###,##.000";

	public static byte[] HeaderLogo;

	public static byte[] FooterLogo;

	public static bool IsRepOnlineConn = false;

	public static bool CanExport;

	public static bool CanViewReport;

	public static bool CanPrintReport;

	public static bool CanDirectPrint;

	public static bool Tap1;

	public static bool Tap2;

	public static bool Tap3;

	public static bool Tap4;

	public static bool Tap5;

	public static bool Tap6;

	public static bool Tap7;

	public static bool Tap8;

	public static bool Tap9;

	public static bool CanMultiPrint;

	public static bool CanPrint;

	public static bool CanModifyOtherBranch;

	public static bool ViewAllBranches;

	public static DataRow drDefaults;

	public static DataTable dtSystemOptions;

	public static DataTable dtSystemAccounts;

	public static DataTable dtSystemDefaults;

	public static DataTable dtSystemModules;

	public static DataTable dtSystemVersions;

	public static char MessageBoxResult = 'z';

	public static QuestionMessageBox QuestionMB = new QuestionMessageBox();

	public static InformationMessageBox InformationMB = new InformationMessageBox();

	public static double FadeGrade = 0.05;
}
