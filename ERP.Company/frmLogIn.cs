using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Ticketing;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Microsoft.Win32;
using MS;

namespace ERP.Company;

public class frmLogIn : frmBase
{
	private delegate void DemoThreadDelegate();

	private delegate void FixThreadDelegate();

	private RegistryKey RegKey;

	private DataTable dtDatabases;

	private DataTable dtMS;

	private DataTable dtMSDetails;

	public Thread DemoThread;

	public Thread FixThread;

	private IContainer components = null;

	private UltraGroupBox gbxServerInfo;

	protected internal UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraTextEditor txtPassword;

	private UltraLabel lblPassword;

	private UltraTextEditor txtUserName;

	private UltraLabel lblUserName;

	private UltraCheckEditor chkRememberUserName;

	private UltraCheckEditor chkRememberPassword;

	private UltraLabel lblLanguage;

	public UltraComboEditor cboLanguage;

	private UltraTextEditor txtDBPassword;

	private UltraLabel lblDBPassword;

	private UltraTextEditor txtDBUser;

	private UltraLabel lblDBUserID;

	private UltraLabel lblServer;

	private UltraLabel lblCompany;

	private UltraCheckEditor chkRememberCompany;

	private UltraButton btnServerEdit;

	private UltraTextEditor txtServer;

	private UltraButton btnServerOK;

	protected internal UltraLabel lblStatus;

	private UltraButton btnLogin;

	protected internal UltraLabel ultraLabel1;

	public UltraButton btnKeyboard;

	private UltraComboEditor cboCompany;

	public frmLogIn()
	{
		InitializeComponent();
	}

	private void frmLogIn_Load(object sender, EventArgs e)
	{
		RegKey = Registry.CurrentUser.OpenSubKey(GlobalVariables.path, writable: true);
		if (RegKey == null || RegKey.GetValue("Server") == null || RegKey.GetValue("DBUserID") == null || RegKey.GetValue("DBPassword") == null)
		{
			((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء تحديد الخادم" : "Please Specify the Server");
			EnableServerPanel(Enable: true);
		}
		else
		{
			LoadRegisteryInfo();
			if (GlobalVariables.DatabaseName.ToLower() == "master" || RegKey.GetValue("DataBaseName") == null)
			{
				if (Main.Set_Connection(GlobalVariables.Server, "Master", GlobalVariables.dbUserID, GlobalVariables.dbPassword))
				{
					((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء اختيار شركة" : "Please Select a Company");
					GetDatabases();
					base.ActiveControl = (Control)(object)cboCompany;
					base.AcceptButton = (IButtonControl)btnLogin;
				}
				else
				{
					GlobalVariables.InformationMB.Show("لم يتم التوصيل بالخادم", "Connection to the Server is Invalid");
					((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء تحديد الخادم" : "Please Specify the Server");
					EnableServerPanel(Enable: true);
				}
			}
			else if (!Main.Set_Connection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword))
			{
				GlobalVariables.InformationMB.Show("لم يتم التوصيل بالخادم", "Connection to the Server is Invalid");
				((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء تحديد الخادم" : "Please Specify the Server");
				EnableServerPanel(Enable: true);
			}
			else
			{
				GetDatabases();
				((TextEditorControlBase)cboCompany).Value = GlobalVariables.DatabaseName;
				((Control)(object)lblTitle).Text = "Login to [" + GlobalVariables.DatabaseName.Substring(4) + " ]";
				((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "أدخل اسم المستخدم وكلمة المرور" : "Enter UserName and Password");
				if ((int)RegKey.GetValue("RememberUserName") == 1)
				{
					((UltraToggleEditorBase)chkRememberUserName).Checked = true;
					((Control)(object)txtUserName).Text = GlobalVariables.UserName;
				}
				if ((int)RegKey.GetValue("RememberPassword") == 1)
				{
					((UltraToggleEditorBase)chkRememberPassword).Checked = true;
					((Control)(object)txtPassword).Text = GlobalVariables.Password;
				}
				cboLanguage.SelectedIndex = (int)RegKey.GetValue("Language");
				base.ActiveControl = (Control)(object)txtPassword;
				base.AcceptButton = (IButtonControl)btnLogin;
			}
		}
		Program.FlashLoadingCloseing();
		Activate();
		Focus();
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		Program.FlashLoadingCloseing();
		Close();
	}

	private void btnLogin_Click(object sender, EventArgs e)
	{
		Program.FlashLoadingThread = new Thread(Program.FlashLoadingThreadStart);
		Program.FlashLoadingThread.Start();
		GlobalVariables.Server = GlobalVariables.Server.TrimStart(' ');
		if (!Main.Set_Connection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword))
		{
			Program.FlashLoadingCloseing();
			GlobalVariables.InformationMB.Show("لم يتم التوصيل بالخادم", "Connection to the Server is Invalid");
			((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء تحديد الخادم" : "Please Specify the Server");
			EnableServerPanel(Enable: true);
			return;
		}
		if (GlobalVariables.DatabaseName.ToLower() == "master")
		{
			((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء اختيار شركة" : "Please Select a Company");
			GetDatabases();
			base.ActiveControl = (Control)(object)cboCompany;
			base.AcceptButton = (IButtonControl)btnLogin;
			return;
		}
		if (File.GetLastWriteTime("MS.dll") < new DateTime(2022, 2, 1))
		{
			GlobalVariables.InformationMB.Show("برجاء تحديث النظام ", " برجاء تحديث النظام ");
			return;
		}
		DataTable dataTable = new DataTable();
		dataTable = Users.CheckUser(((Control)(object)txtUserName).Text, ((Control)(object)txtPassword).Text, IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			string text = (text = "\\\\" + GlobalVariables.Server.Split(',')[0] + "\\ERP");
			DataTable comboData = Main.GetComboData("SystemDefaults", "DefaultValue", "DefaultEnName=''UpdatePath''");
			if (comboData.Rows.Count > 0 && comboData.Rows[0]["DefaultValue"].ToString() != "")
			{
				text = comboData.Rows[0]["DefaultValue"].ToString();
			}
			if (File.Exists(text + "\\ERP_EXE.EXG") && (!File.Exists("ERP_EXE.EXG") || File.GetLastWriteTime(text + "\\ERP_EXE.EXG") > File.GetLastWriteTime("ERP.exe")))
			{
				Program.FlashLoadingCloseing();
				GlobalVariables.InformationMB.Show("يوجد تحديث للبرنامج على الخادم.\r\nسوف يقوم البرنامج بإجراء التحديثات وإعادة التشغيل  \r\n(في حالة عدم التشغيل التلقائي في خلال 30 ثانية برجاء اعادة تشغيل البرنامج)", "New version found on server.\r\n System will restart to Make the Updates\r\n(in case the system doesn't restart in 30 seconds,please start the system manually)");
				try
				{
					Process.Start("cmd.exe", "/c taskkill /IM ERP.exe");
					Process.Start("Update.exe", text + "\\ERP_EXE.EXG ERP_EXE.EXG");
					return;
				}
				catch
				{
					return;
				}
			}
			GlobalVariables.UserID = dataTable.Rows[0]["User_ID"].ToString();
			GlobalVariables.UserName = dataTable.Rows[0]["LoginName"].ToString();
			GlobalVariables.Password = dataTable.Rows[0]["Password"].ToString();
			GlobalVariables.GroupID = dataTable.Rows[0]["GroupID"].ToString();
			if (((UltraToggleEditorBase)chkRememberUserName).Checked)
			{
				RegKey.SetValue("UserName", ((Control)(object)txtUserName).Text);
			}
			else
			{
				RegKey.SetValue("UserName", "");
			}
			if (((UltraToggleEditorBase)chkRememberPassword).Checked)
			{
				RegKey.SetValue("Password", GlobalFunctions.EncodeText(((Control)(object)txtPassword).Text));
			}
			else
			{
				RegKey.SetValue("Password", "");
			}
			RegKey.SetValue("Language", cboLanguage.SelectedIndex);
			RegKey.SetValue("RememberUserName", ((UltraToggleEditorBase)chkRememberUserName).Checked ? 1 : 0);
			RegKey.SetValue("RememberPassword", ((UltraToggleEditorBase)chkRememberPassword).Checked ? 1 : 0);
			GlobalVariables.IsArabic = cboLanguage.SelectedIndex == 1;
			GlobalFunctions.LoadUserPrivileges();
			GlobalVariables.dtSystemOptions = BusinessLayer.Defaults.SystemOptions.Select("-1", GlobalVariables.IsArabic ? "1" : "0");
			if (GlobalFunctions.GetOption("IsSynchronization"))
			{
				DataTable dataTable2 = SyncConnection.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
				if (dataTable2.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
				{
					Main.Set_SyncConnection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword, IsSynchronization: true);
				}
				else
				{
					Main.Set_SyncConnection(dataTable2.Rows[0]["ServerName"].ToString(), dataTable2.Rows[0]["DataBaseName"].ToString(), dataTable2.Rows[0]["UserName"].ToString(), dataTable2.Rows[0]["Password"].ToString(), IsSynchronization: true);
				}
			}
			DataTable dataTable3 = Branches.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (Main.IsSynchronization)
			{
				DataTable dataTable4 = SyncConnection.Select("-1", "-1", "0");
				if (dataTable4.Rows.Count > 0 && dataTable4.Rows[0]["SyncBranchID"] != DBNull.Value)
				{
					DataView dataView = new DataView(dataTable3);
					dataView.RowFilter = " BranchID=" + dataTable4.Rows[0]["SyncBranchID"].ToString();
					if (dataView.Count <= 0)
					{
						Program.FlashLoadingCloseing();
						GlobalVariables.InformationMB.Show("المستخدم غير مسجل في الفرع ", " User Not Registered in the Branch ");
						((Control)(object)txtUserName).Select();
						return;
					}
					GlobalVariables.CurrentBranchID = dataTable4.Rows[0]["SyncBranchID"].ToString();
				}
				else if (dataTable3.Rows.Count == 1)
				{
					GlobalVariables.CurrentBranchID = dataTable3.Rows[0]["BranchID"].ToString();
				}
				else
				{
					if (dataTable3.Rows.Count == 0)
					{
						Program.FlashLoadingCloseing();
						GlobalVariables.InformationMB.Show("المستخدم غير مسجل في اي فرع ", " User Not Registered in any Branch ");
						((Control)(object)txtUserName).Select();
						return;
					}
					Program.FlashLoadingCloseing();
					frmLoginBranch frmLoginBranch2 = new frmLoginBranch(dataTable3, dataTable.Rows[0]["DefaultBranchID"].Equals(DBNull.Value) ? "-1" : dataTable.Rows[0]["DefaultBranchID"].ToString());
					frmLoginBranch2.ShowDialog();
					Program.FlashLoadingThread = new Thread(Program.FlashLoadingThreadStart);
					Program.FlashLoadingThread.Start();
				}
			}
			else if (dataTable3.Rows.Count == 1)
			{
				GlobalVariables.CurrentBranchID = dataTable3.Rows[0]["BranchID"].ToString();
			}
			else
			{
				if (dataTable3.Rows.Count == 0)
				{
					Program.FlashLoadingCloseing();
					GlobalVariables.InformationMB.Show("المستخدم غير مسجل في اي فرع ", " User Not Registered in any Branch ");
					((Control)(object)txtUserName).Select();
					return;
				}
				Program.FlashLoadingCloseing();
				frmLoginBranch frmLoginBranch3 = new frmLoginBranch(dataTable3, dataTable.Rows[0]["DefaultBranchID"].Equals(DBNull.Value) ? "-1" : dataTable.Rows[0]["DefaultBranchID"].ToString());
				frmLoginBranch3.TopMost = true;
				frmLoginBranch3.ShowDialog();
				Program.FlashLoadingThread = new Thread(Program.FlashLoadingThreadStart);
				Program.FlashLoadingThread.Start();
			}
			GlobalFunctions.PrepareSystemModulesData();
			GlobalVariables.dtSystemModules = SystemModules.Select("-1", GlobalVariables.IsArabic ? "1" : "0");
			GlobalFunctions.PrepareStaticData();
			GlobalVariables.drDefaults = Main.GetComboData("Defaults", "CompanyNameE, CompanyNameA, CurrencyID,TicketingHost,TicketingHost2", "").Rows[0];
			GlobalVariables.drDefaults["TicketingHost"] = (GlobalVariables.drDefaults["TicketingHost"].Equals(DBNull.Value) ? "P3NWPLSK12SQL-v01.shr.prod.phx3.secureserver.net" : GlobalFunctions.DecodeText(GlobalVariables.drDefaults["TicketingHost"].ToString()));
			GlobalVariables.drDefaults["TicketingHost2"] = (GlobalVariables.drDefaults["TicketingHost2"].Equals(DBNull.Value) ? "P3NWPLSK12SQL-v01.shr.prod.phx3.secureserver.net" : GlobalFunctions.DecodeText(GlobalVariables.drDefaults["TicketingHost2"].ToString()));
			DBHandler._connectionString = "Data Source=" + GlobalVariables.drDefaults["TicketingHost"].ToString() + ";Initial Catalog=ph18542904755dbt;User ID=dbut;password=mb#02cH9;Connection Timeout=90;";
			dtMS = MS.Select();
			if (dtMS.Rows.Count > 0)
			{
				GlobalVariables.ServerID = dtMS.Rows[0]["ServerID"].ToString();
			}
			dtMSDetails = MSDetails.Select();
			GlobalVariables.dtSystemAccounts = SystemAccount.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalVariables.dtSystemDefaults = SystemDefaults.Select("-1", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.dtSystemVersions = Main.ExecuteQuery_DataTable("SystemVersionMaxID");
			bool flag = false;
			if (GlobalVariables.UserID != "1")
			{
				if (SystemInformation.TerminalServerSession && !GlobalFunctions.GetOption("AllowRemoteConnction"))
				{
					try
					{
						Process.Start("License.exe", "Support>Required ERP>Bar Remote>Desktop>Connection>Is>Not>Allowed");
					}
					catch
					{
					}
					Environment.Exit(-1);
					return;
				}
				if (Users.CheckOnline(GlobalVariables.UserID, GlobalVariables.CurrentBranchID))
				{
					if (GlobalFunctions.GetOption("LoginOutoKick"))
					{
						GlobalVariables.QuestionMB.Show("تم تسجيل دخول لنفس المستخدم على نفس الفرع سابقا\n هل تريد إخراجه؟ ", " User Registered Online on same Branch \n Do you want to Kick Old Login Out? ");
						flag = GlobalVariables.MessageBoxResult == 'Y';
					}
					else
					{
						GlobalVariables.InformationMB.Show("تم تسجيل دخول لنفس المستخدم على نفس الفرع سابقا ", " User Registered Online on same Branch ");
					}
				}
				if (GlobalVariables.dtSystemVersions.Rows.Count > 0 && GlobalVariables.dtSystemVersions.Rows[0]["LastVersion"].ToString() != GlobalVariables.VersionID)
				{
					GlobalVariables.InformationMB.Show("هذا الاصدار ليس الأحدث للنظام\n\nرقم الاصدار " + GlobalVariables.VersionID + "\n\nرقم الاصدار الحالي " + GlobalVariables.dtSystemVersions.Rows[0]["LastVersion"], "Sorry,You are trying to run an old version\n\n This Version No. " + GlobalVariables.VersionID + "\n\n Current Version No. " + GlobalVariables.dtSystemVersions.Rows[0]["LastVersion"]);
					Environment.Exit(-1);
				}
			}
			else if (Users.CheckOnline(GlobalVariables.UserID, GlobalVariables.CurrentBranchID))
			{
				flag = true;
			}
			GlobalVariables.UserLoginID = Users.Login(GlobalVariables.UserID, GlobalVariables.CurrentBranchID, flag ? "1" : "0", IsFromServer: false).ToString();
			string defaultName = ((RegKey.GetValue("ReportDefault") == null) ? "PathReport" : RegKey.GetValue("ReportDefault").ToString());
			if (GlobalFunctions.GetDefault(defaultName) != "")
			{
				GlobalVariables.ReportsPath = GlobalFunctions.GetDefault(defaultName);
			}
			else
			{
				GlobalVariables.ReportsPath = GlobalFunctions.GetDefault("PathReport");
			}
			GlobalVariables.ArchivingPath = GlobalFunctions.GetDefault("ArchivingPath");
			switch (Convert.ToInt32(GlobalFunctions.GetDefault("QuantityDecimals")))
			{
			case 0:
				GlobalVariables.QtyDecimals = "###,##";
				break;
			case 1:
				GlobalVariables.QtyDecimals = "###,##.0";
				break;
			case 2:
				GlobalVariables.QtyDecimals = "###,##.00";
				break;
			case 3:
				GlobalVariables.QtyDecimals = "###,##.000";
				break;
			}
			GlobalVariables.AddedTax = Convert.ToDouble(GlobalFunctions.GetDefault("AddedTaxPercentage")) / 100.0;
			GlobalVariables.DiscountTax = Convert.ToDouble(GlobalFunctions.GetDefault("DiscountTaxPercentage")) / 100.0;
			GlobalVariables.LocalCurrencyID = Convert.ToInt32(GlobalVariables.drDefaults["CurrencyID"]);
			GlobalVariables.MinOpenedDate = FiscalYearPeriod.GetMinOpenedDate(GlobalVariables.CurrentBranchID, IsFromServer: false);
			GlobalVariables.IsRemoteServer = GlobalVariables.Server.IndexOf('.') > 0;
			GlobalVariables.LoadApplication = true;
			try
			{
				DemoThread = new Thread(DemoThreadStart);
				DemoThread.Start();
				FixThread = new Thread(FixThreadStart);
				FixThread.Start();
			}
			catch
			{
			}
			Close();
		}
		else
		{
			Program.FlashLoadingCloseing();
			GlobalVariables.InformationMB.Show("خطأ فى اسم المستخدم أو كلمة المرور ", "Wrong User Name or Password ");
			((TextEditorControlBase)txtPassword).Clear();
			((Control)(object)txtPassword).Select();
		}
	}

	public void DemoThreadStart()
	{
		try
		{
			try
			{
				Main.ExecuteQuery_DataTableWithoutErrorMessageThread("EXEC CLR_Configure 'erp_" + ((Control)(object)cboCompany).Text + "'");
			}
			catch
			{
			}
			string text = "";
			try
			{
				text = MS.Get();
				string terminalClientName = GlobalFunctions.GetTerminalClientName();
				if (GlobalVariables.UserID != "1" && terminalClientName != null && terminalClientName != "")
				{
					GlobalVariables.RemoteSessionName = terminalClientName;
					terminalClientName = terminalClientName + "jgdkmetc" + Environment.GetEnvironmentVariable("COMPUTERNAME") + "byzsaoiu";
					terminalClientName += Environment.GetEnvironmentVariable("USERNAME");
					terminalClientName = terminalClientName.Replace(" ", "").Replace("(", "").Replace(")", "")
						.Replace("@", "")
						.Replace("-", "")
						.Replace(".", "");
					string text2 = "";
					for (int i = 9; i < terminalClientName.Length; i += 10)
					{
						text2 += "-";
						text2 += terminalClientName[i - 6];
						text2 += terminalClientName[i - 3];
						text2 += terminalClientName[i - 8];
						text2 += terminalClientName[i - 1];
						text2 += terminalClientName[i - 9];
						text2 += terminalClientName[i - 5];
						text2 += terminalClientName[i - 2];
						text2 += terminalClientName[i - 4];
						text2 += terminalClientName[i];
						text2 += terminalClientName[i - 7];
					}
					text2 = text2.ToUpper();
					text += text2;
				}
			}
			catch
			{
				try
				{
					Process.Start("License.exe", "Win32>Fail>Or>Limited>User ERP>Bar ______________________________________");
				}
				catch
				{
				}
				Environment.Exit(-1);
				return;
			}
			switch (text)
			{
			default:
				if (!(text == "ETGNFFB5I1BEHLBZOC-C-2F/F42T1V172C62N4K/7-Y3&3_S4882B1S/SUIE6C-TE&7LNR&39I1&2E1R81&"))
				{
					if (dtMS.Rows.Count == 0)
					{
						Main.ExecuteQuery_DataTableWithoutErrorMessageThread("Disable TRIGGER All  ON [dbo].[PS_MS]; INSERT INTO [PS_MS]VALUES (1,null,'" + GlobalFunctions.EncodeText("1") + "','" + GlobalFunctions.EncodeText("0") + "','" + GlobalFunctions.EncodeText("200") + "','" + DateTime.Now.ToString(GlobalVariables.DateShortFormate) + "',null,0,'" + GlobalFunctions.EncodeText("0") + GlobalFunctions.EncodeText("0") + GlobalFunctions.EncodeText("0") + "') ; Enable TRIGGER All  ON [dbo].[PS_MS];");
						dtMS = MS.Select();
					}
					if ((bool)dtMS.Rows[0]["Done"])
					{
						if (!OnLineDB.CreateConnection(GlobalVariables.drDefaults["TicketingHost2"].ToString(), "ph18542904755dbc", "dbuc", "kwY&5w05") || OnLineDB.ExecuteQuery_DataTableDecoded("select * From Con Where Locked=0 And ServerID=" + dtMS.Rows[0]["ServerID"]).Rows.Count <= 0)
						{
							try
							{
								Users.KickAll();
								Process.Start("License.exe", "due>installments>Required ERP>Bar ______________________________________");
							}
							catch
							{
							}
							Environment.Exit(-1);
							break;
						}
						Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_MS];  update PS_MS set Done = 0 ; Enable TRIGGER All  ON [dbo].[PS_MS]; ");
					}
					if (dtMS.Rows[0]["MSNumber"].Equals(DBNull.Value))
					{
						int num = Convert.ToInt32(Main.ExecuteQuery_DataTableWithoutErrorMessageThread("select (select COUNT(*)from A_JVDetails)  +COUNT(*)as DemoCount from SC_ItemsTransactions ").Rows[0]["DemoCount"]);
						if (num >= Convert.ToInt32(GlobalFunctions.DecodeText(dtMS.Rows[0]["MSTotalValue"].ToString())) || num >= 10000)
						{
							try
							{
								Users.KickAll();
								Process.Start("License.exe", "License>Required>For>Expired>Demo>Version ERP>Bar ______________________________________");
							}
							catch
							{
							}
							Environment.Exit(-1);
						}
						break;
					}
					int num2 = Convert.ToInt32(GlobalFunctions.DecodeText(dtMS.Rows[0]["MSCount"].ToString()));
					if (num2 != 0 && num2 < ((DisposableObjectCollectionBase)cboCompany.Items).Count)
					{
						try
						{
							Process.Start("License.exe", "Support>Required ERP>Bar ______________________________________");
						}
						catch
						{
						}
						Environment.Exit(-1);
						break;
					}
					string text3 = string.Concat("ServerID ", dtMS.Rows[0]["ServerID"], " DBName erp_", ((Control)(object)cboCompany).Text.ToLower());
					DataTable dataTable = Main.ExecuteQuery_DataTableWithoutErrorMessageThread("EXEC sp_configure 'show advanced options', 1; EXEC sp_configure 'allow updates', 0; RECONFIGURE; EXEC sp_configure 'xp_cmdshell', 1; RECONFIGURE; EXEC master..xp_cmdshell 'wmic cpu get Manufacturer,name,processorid' ");
					text3 += dataTable.Rows[1]["output"].ToString().Trim();
					dataTable = Main.ExecuteQuery_DataTableWithoutErrorMessageThread("EXEC master..xp_cmdshell 'wmic bios get name,serialnumber,smbiosbiosversion' ");
					text3 += dataTable.Rows[1]["output"].ToString().Trim();
					dataTable = Main.ExecuteQuery_DataTableWithoutErrorMessageThread("EXEC master..xp_cmdshell 'wmic baseboard GET manufacturer,product,SerialNumber' ");
					text3 += dataTable.Rows[1]["output"].ToString().Trim();
					text3 = text3 + " DB " + num2;
					int num3 = Convert.ToInt32(GlobalFunctions.DecodeText(dtMS.Rows[0]["MSItemsCount"].ToString()));
					text3 = text3 + " Comp " + num3;
					text3 += ((dtMS.Columns.IndexOf("MSLAItemsCount") != -1 && dtMS.Rows[0]["MSLAItemsCount"] != DBNull.Value) ? (" CompLA " + GlobalFunctions.DecodeText(dtMS.Rows[0]["MSLAItemsCount"].ToString())) : "");
					text3 += ((dtMS.Columns.IndexOf("MSCSTAItemsCount") != -1 && dtMS.Rows[0]["MSCSTAItemsCount"] != DBNull.Value) ? (" CompCSTA " + GlobalFunctions.DecodeText(dtMS.Rows[0]["MSCSTAItemsCount"].ToString())) : "");
					text3 += ((dtMS.Columns.IndexOf("MSREPAItemsCount") != -1 && dtMS.Rows[0]["MSREPAItemsCount"] != DBNull.Value) ? (" CompREPA " + GlobalFunctions.DecodeText(dtMS.Rows[0]["MSREPAItemsCount"].ToString())) : "");
					for (int j = 0; j < GlobalVariables.dtSystemModules.Rows.Count; j++)
					{
						if ((bool)GlobalVariables.dtSystemModules.Rows[j]["Installed"])
						{
							text3 = text3 + " " + GlobalVariables.dtSystemModules.Rows[j]["ModuleEnName"];
						}
					}
					string text4 = text3;
					text3 = text3 + " " + Convert.ToDateTime(dtMS.Rows[0]["MsDate"]).ToString(GlobalVariables.DateShortFormate);
					if (GlobalFunctions.DecodeText(dtMS.Rows[0]["MSNumber"].ToString()) != text3)
					{
						try
						{
							Users.KickAll();
							Process.Start("License.exe", "License>Required ERP>Bar ______________________________________");
						}
						catch
						{
						}
						Environment.Exit(-1);
						break;
					}
					DataTable dataTable2 = Main.ExecuteQuery_DataTableWithoutErrorMessageThread("select isnull((Select LastDate from PS_PSOrderML ),dateadd(dd,-20,getDate())) LockDate, isnull(dateadd(dd,-430,Max(JVDate)),GetDate()) MaxJVDate,GetDate() ServerDate From A_JV where Deleted=0 And IsInternalJV=1 And TransTypeID not in(3,4,16,17,18,19,22,46) ");
					DateTime dateTime = Convert.ToDateTime(dataTable2.Rows[0]["MaxJVDate"]);
					DateTime dateTime2 = Convert.ToDateTime(dataTable2.Rows[0]["ServerDate"]);
					DateTime dateTime3 = Convert.ToDateTime(dtMS.Rows[0]["MsDate"]);
					DateTime dateTime4 = Convert.ToDateTime(dataTable2.Rows[0]["LockDate"]);
					DateTime dateTime5 = DateTime.Now;
					if (dateTime5 < dateTime2)
					{
						dateTime5 = dateTime2;
					}
					if (dateTime5 < dateTime)
					{
						dateTime5 = dateTime;
					}
					bool flag = false;
					if (dateTime5 > dateTime4.AddHours(6.0))
					{
						Main.ExecuteQuery_DataTableWithoutErrorMessageThread("if (select COUNT(*) from PS_PSOrderML )=0\tinsert into PS_PSOrderML values(getDate())else\tUpdate PS_PSOrderML set LastDate = getDate()");
						if (OnLineDB.CreateConnection(GlobalVariables.drDefaults["TicketingHost2"].ToString(), "ph18542904755dbc", "dbuc", "kwY&5w05"))
						{
							string text5 = "";
							DataTable dataTable3 = SyncConnection.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
							if (dataTable3.Rows.Count > 0)
							{
								text5 = " Server= " + dataTable3.Rows[0]["ServerName"].ToString() + " ; Database= " + dataTable3.Rows[0]["DataBaseName"].ToString() + " ;User Id= " + dataTable3.Rows[0]["UserName"].ToString() + " ; Password = " + dataTable3.Rows[0]["Password"].ToString();
							}
							OnLineDB.ExecuteNonQuery(" UPDATE Con set IsNewCon=1,VersionName='" + GlobalVariables.VersionID + "' ,Sync='" + text5 + "' Where Locked=0 And ServerID=" + dtMS.Rows[0]["ServerID"]);
							DataTable dataTable4 = OnLineDB.ExecuteQuery_DataTableDecoded("select * From Con Where Locked=0 And ServerID=" + dtMS.Rows[0]["ServerID"]);
							if (dataTable4.Rows.Count == 0)
							{
								Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_MS];  update PS_MS set Done = 1 ; Enable TRIGGER All  ON [dbo].[PS_MS]; ");
								try
								{
									Users.KickAll();
									Process.Start("License.exe", "due>installments>Required ERP>Bar ______________________________________");
								}
								catch
								{
								}
								Environment.Exit(-1);
								break;
							}
						}
					}
					if (dateTime5 > dateTime3.AddMonths(1) && OnLineDB.CreateConnection(GlobalVariables.drDefaults["TicketingHost2"].ToString(), "ph18542904755dbc", "dbuc", "kwY&5w05"))
					{
						DateTime dateTime6 = Convert.ToDateTime(OnLineDB.ExecuteQuery_DataTable("Select GetDate() As OnLineDate").Rows[0]["OnLineDate"]);
						dateTime5 = dateTime6;
						if (dateTime6 > dateTime3.AddMonths(1))
						{
							DataTable dataTable5 = OnLineDB.ExecuteQuery_DataTableDecoded("select * From Con Where Locked=0 And ServerID=" + dtMS.Rows[0]["ServerID"]);
							if (dataTable5.Rows.Count == 0 || dataTable5.Rows[0]["MinSerial"].ToString() != text4)
							{
								Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_MS];  update PS_MS set MSNumber = '" + GlobalFunctions.EncodeText("Illegal Or Locked Server" + dateTime5.ToString(GlobalVariables.DateShortFormate)) + "'; Enable TRIGGER All  ON [dbo].[PS_MS]; ");
								Users.KickAll();
								Environment.Exit(-1);
								break;
							}
							if (dataTable5.Rows[0]["MaxSerial"].ToString() == text3 || (dateTime5 < new DateTime(2014, 6, 1) && dataTable5.Rows[0]["MinSerial"].ToString() == text4))
							{
								flag = true;
								OnLineDB.ExecuteNonQuery(" UPDATE Con set Date='" + dateTime5.ToString(GlobalVariables.DateShortFormate) + "',MaxSerial = '" + GlobalFunctions.EncodeText(text4 + " " + dateTime5.ToString(GlobalVariables.DateShortFormate)) + "',IsNewCon=1,VersionName='" + GlobalVariables.VersionID + "' Where Locked=0 And ServerID=" + dtMS.Rows[0]["ServerID"]);
								Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_MS];  update PS_MS set MSNumber = '" + GlobalFunctions.EncodeText(text4 + " " + dateTime5.ToString(GlobalVariables.DateShortFormate)) + "',MsDate = '" + dateTime5.ToString(GlobalVariables.DateShortFormate) + "' ; Enable TRIGGER All  ON [dbo].[PS_MS]; ");
							}
						}
					}
					if (!flag && dateTime5 > dateTime3.AddMonths(3))
					{
						try
						{
							Users.KickAll();
							Process.Start("License.exe", "Internet>Connection>Required ERP>Bar ______________________________________");
						}
						catch
						{
						}
						Environment.Exit(-1);
						break;
					}
					if (num3 == 0 || dtMSDetails.Rows.Count > num3)
					{
						try
						{
							Process.Start("License.exe", "License>Required>For>Additional>Machines ERP>Bar ______________________________________");
						}
						catch
						{
						}
						Environment.Exit(-1);
						break;
					}
					DataRow[] array = dtMSDetails.Select("MSDetailNumber='" + text + "'");
					if (array.Length == 0)
					{
						if (dtMSDetails.Rows.Count < num3)
						{
							Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_PSOrderMDetails]; Declare @MaxID int select @MaxID = isnull(max(MSDetailID),0)+1 from PS_PSOrderMDetails Insert into PS_PSOrderMDetails Values(@MaxID,N'" + text + "',Getdate(),Getdate())   IF NOT EXISTS (Select *  From Sync_HostName AS HN Where HN.Name =HOST_NAME() And HN.DetailID=@MaxID )  Begin Insert into Sync_HostName Values (@MaxID, HOST_NAME(),GETDATE()) End   ; Enable TRIGGER All  ON [dbo].[PS_PSOrderMDetails];");
						}
						else
						{
							if (dtMSDetails.Rows.Count <= 0)
							{
								break;
							}
							DataTable dataTable6 = Main.ExecuteQuery_DataTableWithoutErrorMessageThread("select Top 1  * from PS_PSOrderMDetails where MSDetailInsertDate<DATEADD(DD,-5,GETDATE()) Order By MSDetailDate ");
							if (dataTable6.Rows.Count > 0)
							{
								Main.ExecuteQuery_DataTableWithoutErrorMessageThread(string.Concat(" Disable TRIGGER All  ON [dbo].[PS_PSOrderMDetails]; Update PS_PSOrderMDetails Set MSDetailNumber = '", text, "',MSDetailInsertDate = GETDATE(),MSDetailDate = GETDATE() where MSDetailID =", dataTable6.Rows[0]["MSDetailID"], " IF NOT EXISTS (Select *  From Sync_HostName AS HN Where HN.Name =HOST_NAME() And HN.DetailID= ", dataTable6.Rows[0]["MSDetailID"], " )  Begin Insert into Sync_HostName Values (", dataTable6.Rows[0]["MSDetailID"], ", HOST_NAME(),GETDATE()) End   ; Enable TRIGGER All  ON [dbo].[PS_PSOrderMDetails];"));
								break;
							}
							DataTable dataTable7 = Main.ExecuteQuery_DataTableWithoutErrorMessageThread("select isnull(max(LastDate) ,dateadd(DD,-20,GETDATE())) lastDate,dateadd(DD,-5,GETDATE()) AllowedDate from PS_OrderMLast");
							if (Convert.ToDateTime(dataTable7.Rows[0][0]) < Convert.ToDateTime(dataTable7.Rows[0][1]))
							{
								GlobalVariables.QuestionMB.Show("برجاء العلم بأن النظام سوف يقوم بإخراج كل المستخدمين \n للتحقق من عدد الاجهزه \n هل تريد الاستمرار؟ ", " All User Will be Kicked out For Security Check For Additional Machines\n Do you want to Kick All Logins Out? ");
								if (GlobalVariables.MessageBoxResult == 'Y')
								{
									Users.KickAll();
									Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_OrderMLast]; Declare @MaxID int select @MaxID = isnull(max(ID),0)+1 from PS_OrderMLast Insert into PS_OrderMLast Values(@MaxID,Getdate()) ;  Enable TRIGGER All  ON [dbo].[PS_OrderMLast];");
									Main.ExecuteQuery_DataTableWithoutErrorMessageThread(" Disable TRIGGER All  ON [dbo].[PS_PSOrderMDetails];  Delete PS_PSOrderMDetails ;  Enable TRIGGER All  ON [dbo].[PS_PSOrderMDetails];");
								}
							}
							else
							{
								try
								{
									Process.Start("License.exe", "License>Required>For>Additional>Machines  ERP>Bar ______________________________________");
								}
								catch
								{
								}
							}
							Environment.Exit(-1);
						}
					}
					else
					{
						Main.ExecuteQuery_DataTableWithoutErrorMessageThread(string.Concat(" Disable TRIGGER All  ON [dbo].[PS_PSOrderMDetails]; Update PS_PSOrderMDetails Set MSDetailDate = GETDATE() where MSDetailID =", array[0]["MSDetailID"], " IF NOT EXISTS (Select *  From Sync_HostName AS HN Where HN.Name =HOST_NAME()And HN.DetailID=", array[0]["MSDetailID"], " )  Begin Insert into Sync_HostName Values (", array[0]["MSDetailID"], ", HOST_NAME(),GETDATE()) End   ; Enable TRIGGER All  ON [dbo].[PS_PSOrderMDetails];"));
					}
					break;
				}
				goto case "ZTUNFHE7IPGE2LB8O1-C-VFNBV1E6DN4BPF9F6T13-YJ&1_S2&E3BPSCSUE_DV-V532DNI2AE86&1I&&44C";
			case "ZTUNFHE7IPGE2LB8O1-C-VFNBV1E6DN4BPF9F6T13-YJ&1_S2&E3BPSCSUE_DV-V532DNI2AE86&1I&&44C":
			case "ETGNFFB5I1BEHLBZOC-C-2F/F42R1V172C62N4C/7-S4SB4Y768&S/UP_BVV&I-E1&ERTH1&&N218LIE31R":
			case "ZTUNFHE7IPGE2LB7O2-C-1FPB6268DFRBLFTE6625-SHBJSB_681UW&PYSVV&I-ND98EIL36514&1T&&5&C":
			case "ETGNFFB5I4BEHLBZOMCC-8F3F172242624695AB15-B453YUS1_5S536S&CE2P-A1D_I8M_V7&C44T&4741":
			case "ETGNFFB5I1BEHLBZOC-C-6F/F1341Q172C64N5T/7-S12PSB_8_1U9&FYSCD8P-N794EIL3951E&1T&&5&8":
			case "ETGNFFB5I4BEHLBZOMCC-9F4F283243725616AS25-S534SB_215U6&7YSIV&C-TC41MAO4_D84&2I&39&4-OISBORFHNECLMISIRSDY":
			case "FT3NBBF3I5ZEGLEHOMCC-5FNB7484SS3F12625855-S1V9B&SEVE62_3U468N5-9S5Y&51ER16_81&328VE":
			case "FT4NBBF7I3ZEGLEHO7-C-2FCBO2B1W/2FN3A793/A-S/SB5Y2&6&SPUC_BE_DV-E1&CRTH1&&N218LIE31R":
			case "FT4NBBF7I3ZEGLEHO7-C-1F2B374464NF/36C3K2C-UQ48SSY_N_&21OB2P&8/-1_6SN&T_V3&651I92161":
			case "FT4NBBF5I3ZEGLEHO7-C-2F2B4772M2NF/32C3G2C-B41PYUS8_4S92NS&CD8P-I55_T1E6_6&A94N&8132":
			case "FT3NBBFD24ZEGLEHOPUC-IF3BECN266PFA1V5A7S7-&84_E8V21_2826R1EB&D-43Q_EQX&DR/34&352N&7":
			case "ETGNFFB3I1BEHLBZOC-C-1FNFN43/JC627663J4J7-SBSE4YA68&S/UP_BVV&I-E1&DRTH1&&N218LIE31R":
			case "":
			case "FT4NBBF7I3ZEGLEHO7-C-1F2B374121NF/36C342C-UA43SSY_N_&51GB2P&8/-1_6SN&T_V3&651I92161":
			case "HT5NBGFD243E2LZ1OPUC-7BDEPFCDNN3F8BX51F8F-7VYI3FC_VS3ESN1_D&61-FRD&REC99IGEIVOA1&8A":
			case "BTHNBAFUPGFEZLF1E7XX-922FBC95-8465C-5M26V-A2E1&5DAAV15N5D_1ENE-2VEEC&F_SR3_V4A_S54&":
			case "ZTUNFHE7IPGE2LB7O2-C-1FTB5266DFQBDFVR6P45-SKBNSB_681UV&PYSVV&I-ND98EIL36516&1T&&5&C":
				GlobalVariables.IsTechnicalUser = true;
				break;
			}
		}
		catch (Exception ex)
		{
			try
			{
				Process.Start("License.exe", "Support>Required  ERP>Bar ______________________________________");
			}
			catch
			{
			}
			Environment.Exit(-1);
			MessageBox.Show(ex.Message);
		}
	}

	public void FixThreadStart()
	{
		try
		{
			if (Main.IsSynchronization)
			{
				if (GlobalVariables.dtSyncConn == null || GlobalVariables.dtSyncConn.Rows.Count == 0)
				{
					GlobalVariables.dtSyncConn = SyncConnection.Select("-1", "-1", "0");
				}
				if (!GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
				{
					Synchronization.ServerR = GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString();
					Synchronization.Server2R = GlobalVariables.dtSyncConn.Rows[0]["ServerName2"].ToString();
					Synchronization.DataBaseR = GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString();
					Synchronization.UserIDR = GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString();
					Synchronization.PasswordR = GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString();
					int num = Synchronization.SelectConnectionR();
					if (num == 2)
					{
						GlobalVariables.dtSyncConn.Rows[0]["ServerName"] = GlobalVariables.dtSyncConn.Rows[0]["ServerName2"];
						Main.Set_SyncConnection(GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString(), GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString(), GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString(), GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString(), IsSynchronization: true);
					}
				}
			}
			Main.ExecuteQuery_DataTableWithoutErrorMessageThread("System_Fixes");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnServerEdit_Click(object sender, EventArgs e)
	{
		EnableServerPanel(Enable: true);
		base.AcceptButton = (IButtonControl)btnServerOK;
	}

	private void btnServerOK_Click(object sender, EventArgs e)
	{
		RegKey = Registry.CurrentUser.CreateSubKey(GlobalVariables.path);
		if (Main.Set_Connection((((Control)(object)txtServer).Text == "") ? "." : ((Control)(object)txtServer).Text.TrimStart(' '), "Master", ((Control)(object)txtDBUser).Text, ((Control)(object)txtDBPassword).Text))
		{
			RegKey.SetValue("Server", (((Control)(object)txtServer).Text == "") ? "." : ((Control)(object)txtServer).Text.TrimStart(' '));
			RegKey.SetValue("DBUserID", ((Control)(object)txtDBUser).Text);
			RegKey.SetValue("DBPassword", ((Control)(object)txtDBPassword).Text);
			RegKey.SetValue("DataBaseName", "Master");
			RegKey.SetValue("Language", 1);
			GlobalVariables.Server = ((((Control)(object)txtServer).Text == "") ? "." : ((Control)(object)txtServer).Text.TrimStart(' '));
			GlobalVariables.dbUserID = ((Control)(object)txtDBUser).Text;
			GlobalVariables.dbPassword = ((Control)(object)txtDBPassword).Text;
			GlobalVariables.DatabaseName = "Master";
			GlobalVariables.IsArabic = true;
			LoadRegisteryInfo();
			((TextEditorControlBase)cboCompany).Clear();
			GetDatabases();
			((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "برجاء اختيار شركة" : "Please Select a Company");
			((TextEditorControlBase)cboCompany).Focus();
			base.ActiveControl = (Control)(object)cboCompany;
			EnableServerPanel(Enable: false);
			base.AcceptButton = (IButtonControl)btnLogin;
		}
		else
		{
			GlobalVariables.InformationMB.Show("معلومات الخادم غير صحيحة.برجاء مراجعة المعلومات", "Server Info. is not valied.Please check Server Info.");
		}
	}

	private void EnableServerPanel(bool Enable)
	{
		((Control)(object)btnServerEdit).Visible = !Enable;
		((Control)(object)btnServerOK).Visible = Enable;
		((Control)(object)gbxServerInfo).Visible = Enable;
		if (Enable)
		{
			base.ActiveControl = (Control)(object)txtServer;
			base.AcceptButton = (IButtonControl)btnServerOK;
			return;
		}
		if (cboCompany.SelectedIndex == -1)
		{
			base.ActiveControl = (Control)(object)cboCompany;
		}
		else
		{
			base.ActiveControl = (Control)(object)txtUserName;
		}
		base.AcceptButton = (IButtonControl)btnLogin;
	}

	private void GetDatabases()
	{
		dtDatabases = Main.ExecuteQuery_DataTable("select Substring(name,5,LEN(name)) as name,name as DBName from master.dbo.sysdatabases where has_dbaccess(name) = 1 and Rtrim(Substring(name,1,4)) = 'ERP_' Order By name");
		cboCompany.DataSource = dtDatabases;
		cboCompany.DisplayMember = "name";
		cboCompany.ValueMember = "DBName";
	}

	private void LoadRegisteryInfo()
	{
		if (RegKey.GetValue("Font") == null)
		{
			RegKey.SetValue("Font", "Tahoma");
		}
		if (RegKey.GetValue("UserName") == null)
		{
			RegKey.SetValue("UserName", "");
		}
		if (RegKey.GetValue("Password") == null)
		{
			RegKey.SetValue("Password", "");
		}
		if (RegKey.GetValue("RememberUserName") == null)
		{
			RegKey.SetValue("RememberUserName", 0);
		}
		if (RegKey.GetValue("RememberPassword") == null)
		{
			RegKey.SetValue("RememberPassword", 0);
		}
		if (RegKey.GetValue("POSPrinter") == null)
		{
			RegKey.SetValue("POSPrinter", "");
		}
		((Control)(object)txtServer).Text = (GlobalVariables.Server = RegKey.GetValue("Server").ToString());
		GlobalVariables.DatabaseName = ((RegKey.GetValue("DataBaseName") == null) ? "Master" : RegKey.GetValue("DataBaseName").ToString());
		((Control)(object)txtDBUser).Text = (GlobalVariables.dbUserID = RegKey.GetValue("DBUserID").ToString());
		((Control)(object)txtDBPassword).Text = (GlobalVariables.dbPassword = RegKey.GetValue("DBPassword").ToString());
		GlobalVariables.IsArabic = RegKey.GetValue("Language") == null || (int)RegKey.GetValue("Language") == 1;
		cboLanguage.SelectedIndex = ((RegKey.GetValue("Language") == null) ? 1 : ((int)RegKey.GetValue("Language")));
		GlobalVariables.UserName = RegKey.GetValue("UserName").ToString();
		try
		{
			GlobalVariables.Password = GlobalFunctions.DecodeText(RegKey.GetValue("Password").ToString());
		}
		catch
		{
		}
		GlobalVariables.POSPrinter = RegKey.GetValue("POSPrinter").ToString();
	}

	private void chkRememberUserName_CheckedChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkRememberPassword).Checked = false;
		((Control)(object)chkRememberPassword).Enabled = ((UltraToggleEditorBase)chkRememberUserName).Checked;
	}

	private void Company_ValueChanged(object sender, EventArgs e)
	{
		if (cboCompany.SelectedIndex > -1)
		{
			if (((UltraToggleEditorBase)chkRememberCompany).Checked)
			{
				RegKey.SetValue("DataBaseName", ((TextEditorControlBase)cboCompany).Value.ToString());
			}
			GlobalVariables.DatabaseName = ((TextEditorControlBase)cboCompany).Value.ToString();
			((Control)(object)lblTitle).Text = "Login to [" + ((Control)(object)cboCompany).Text.ToString() + " ]";
			((Control)(object)lblStatus).Text = (GlobalVariables.IsArabic ? "أدخل اسم المستخدم وكلمة المرور" : "Enter UserName and Password");
			if ((int)RegKey.GetValue("RememberUserName") == 1)
			{
				((UltraToggleEditorBase)chkRememberUserName).Checked = true;
				((Control)(object)txtUserName).Text = GlobalVariables.UserName;
			}
			if ((int)RegKey.GetValue("RememberPassword") == 1)
			{
				((UltraToggleEditorBase)chkRememberPassword).Checked = true;
				((Control)(object)txtPassword).Text = GlobalVariables.Password;
			}
			cboLanguage.SelectedIndex = (int)RegKey.GetValue("Language");
		}
	}

	private void txtPassword_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Company.frmLogIn));
		ValueListItem val4 = new ValueListItem();
		ValueListItem val5 = new ValueListItem();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.gbxServerInfo = new UltraGroupBox();
		this.txtServer = new UltraTextEditor();
		this.txtDBPassword = new UltraTextEditor();
		this.lblDBPassword = new UltraLabel();
		this.txtDBUser = new UltraTextEditor();
		this.lblDBUserID = new UltraLabel();
		this.lblServer = new UltraLabel();
		this.btnServerOK = new UltraButton();
		this.btnServerEdit = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.txtPassword = new UltraTextEditor();
		this.lblPassword = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		this.lblUserName = new UltraLabel();
		this.chkRememberUserName = new UltraCheckEditor();
		this.chkRememberPassword = new UltraCheckEditor();
		this.lblLanguage = new UltraLabel();
		this.cboLanguage = new UltraComboEditor();
		this.cboCompany = new UltraComboEditor();
		this.lblCompany = new UltraLabel();
		this.chkRememberCompany = new UltraCheckEditor();
		this.lblStatus = new UltraLabel();
		this.btnLogin = new UltraButton();
		this.ultraLabel1 = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gbxServerInfo).BeginInit();
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtServer).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDBPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDBUser).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLanguage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCompany).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberCompany).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(603, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 418);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(601, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 418);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(601, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 416);
		this.gbxServerInfo.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((AppearanceBase)val).FontData.SizeInPoints = 10f;
		this.gbxServerInfo.ContentAreaAppearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.txtServer);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.txtDBPassword);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.lblDBPassword);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.txtDBUser);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.lblDBUserID);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.lblServer);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Controls.Add((System.Windows.Forms.Control)(object)this.btnServerOK);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Location = new System.Drawing.Point(47, 42);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Name = "gbxServerInfo";
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Size = new System.Drawing.Size(472, 103);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).TabIndex = 2;
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Text = "Server Information";
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).Visible = false;
		((System.Windows.Forms.Control)(object)this.txtServer).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.txtServer).Location = new System.Drawing.Point(114, 18);
		((System.Windows.Forms.Control)(object)this.txtServer).Name = "txtServer";
		((System.Windows.Forms.Control)(object)this.txtServer).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.txtServer).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.txtDBPassword).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.txtDBPassword).Location = new System.Drawing.Point(114, 72);
		((System.Windows.Forms.Control)(object)this.txtDBPassword).Name = "txtDBPassword";
		this.txtDBPassword.PasswordChar = '●';
		((System.Windows.Forms.Control)(object)this.txtDBPassword).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.txtDBPassword).TabIndex = 2;
		((System.Windows.Forms.Control)(object)this.lblDBPassword).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblDBPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDBPassword).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblDBPassword).Location = new System.Drawing.Point(16, 75);
		((System.Windows.Forms.Control)(object)this.lblDBPassword).Name = "lblDBPassword";
		((System.Windows.Forms.Control)(object)this.lblDBPassword).Size = new System.Drawing.Size(93, 20);
		((System.Windows.Forms.Control)(object)this.lblDBPassword).TabIndex = 6;
		((System.Windows.Forms.Control)(object)this.lblDBPassword).Text = "DB Password";
		((ControlBase)this.lblDBPassword).WrapText = false;
		((System.Windows.Forms.Control)(object)this.txtDBUser).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.txtDBUser).Location = new System.Drawing.Point(114, 45);
		((System.Windows.Forms.Control)(object)this.txtDBUser).Name = "txtDBUser";
		((System.Windows.Forms.Control)(object)this.txtDBUser).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.txtDBUser).TabIndex = 1;
		((System.Windows.Forms.Control)(object)this.lblDBUserID).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblDBUserID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDBUserID).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblDBUserID).Location = new System.Drawing.Point(16, 48);
		((System.Windows.Forms.Control)(object)this.lblDBUserID).Name = "lblDBUserID";
		((System.Windows.Forms.Control)(object)this.lblDBUserID).Size = new System.Drawing.Size(60, 20);
		((System.Windows.Forms.Control)(object)this.lblDBUserID).TabIndex = 4;
		((System.Windows.Forms.Control)(object)this.lblDBUserID).Text = "DB User";
		((ControlBase)this.lblDBUserID).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblServer).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblServer.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServer).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblServer).Location = new System.Drawing.Point(16, 21);
		((System.Windows.Forms.Control)(object)this.lblServer).Name = "lblServer";
		((System.Windows.Forms.Control)(object)this.lblServer).Size = new System.Drawing.Size(48, 20);
		((System.Windows.Forms.Control)(object)this.lblServer).TabIndex = 5;
		((System.Windows.Forms.Control)(object)this.lblServer).Text = "Server";
		((ControlBase)this.lblServer).WrapText = false;
		((System.Windows.Forms.Control)(object)this.btnServerOK).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.btnServerOK).Location = new System.Drawing.Point(410, 18);
		((System.Windows.Forms.Control)(object)this.btnServerOK).Name = "btnServerOK";
		((System.Windows.Forms.Control)(object)this.btnServerOK).Size = new System.Drawing.Size(48, 82);
		((System.Windows.Forms.Control)(object)this.btnServerOK).TabIndex = 3;
		((System.Windows.Forms.Control)(object)this.btnServerOK).Text = "OK";
		((System.Windows.Forms.Control)(object)this.btnServerOK).Visible = false;
		((System.Windows.Forms.Control)(object)this.btnServerOK).Click += new System.EventHandler(btnServerOK_Click);
		((System.Windows.Forms.Control)(object)this.btnServerEdit).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.btnServerEdit).Location = new System.Drawing.Point(171, 83);
		((System.Windows.Forms.Control)(object)this.btnServerEdit).Name = "btnServerEdit";
		((System.Windows.Forms.Control)(object)this.btnServerEdit).Size = new System.Drawing.Size(238, 32);
		((System.Windows.Forms.Control)(object)this.btnServerEdit).TabIndex = 1;
		((System.Windows.Forms.Control)(object)this.btnServerEdit).Text = "Edit Server Information";
		((System.Windows.Forms.Control)(object)this.btnServerEdit).Click += new System.EventHandler(btnServerEdit_Click);
		((System.Windows.Forms.Control)(object)this.lblTitle).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
		((AppearanceBase)val2).TextHAlignAsString = "Center";
		((AppearanceBase)val2).TextVAlignAsString = "Middle";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.lblTitle).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.lblTitle).Font = new System.Drawing.Font("Tahoma", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 178);
		((System.Windows.Forms.Control)(object)this.lblTitle).ForeColor = System.Drawing.Color.Blue;
		this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblTitle).Location = new System.Drawing.Point(3, 2);
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((System.Windows.Forms.Control)(object)this.lblTitle).Size = new System.Drawing.Size(520, 30);
		((System.Windows.Forms.Control)(object)this.lblTitle).TabIndex = 13;
		((System.Windows.Forms.Control)(object)this.lblTitle).Text = "Login";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.btnClose).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((UltraButtonBase)this.btnClose).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnClose).Location = new System.Drawing.Point(571, 2);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Size = new System.Drawing.Size(30, 30);
		((System.Windows.Forms.Control)(object)this.btnClose).TabIndex = 30;
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnExit_Click);
		((System.Windows.Forms.Control)(object)this.txtPassword).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.txtPassword).Location = new System.Drawing.Point(161, 272);
		((System.Windows.Forms.Control)(object)this.txtPassword).Name = "txtPassword";
		this.txtPassword.PasswordChar = '●';
		((System.Windows.Forms.Control)(object)this.txtPassword).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.txtPassword).TabIndex = 8;
		((System.Windows.Forms.Control)(object)this.txtPassword).KeyDown += new System.Windows.Forms.KeyEventHandler(txtPassword_KeyDown);
		((System.Windows.Forms.Control)(object)this.lblPassword).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassword).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblPassword).Location = new System.Drawing.Point(64, 275);
		((System.Windows.Forms.Control)(object)this.lblPassword).Name = "lblPassword";
		((System.Windows.Forms.Control)(object)this.lblPassword).Size = new System.Drawing.Size(69, 20);
		((System.Windows.Forms.Control)(object)this.lblPassword).TabIndex = 34;
		((System.Windows.Forms.Control)(object)this.lblPassword).Text = "Password";
		((ControlBase)this.lblPassword).WrapText = false;
		((System.Windows.Forms.Control)(object)this.txtUserName).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.txtUserName).Location = new System.Drawing.Point(161, 245);
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		((System.Windows.Forms.Control)(object)this.txtUserName).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.txtUserName).TabIndex = 6;
		((System.Windows.Forms.Control)(object)this.lblUserName).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblUserName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUserName).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblUserName).Location = new System.Drawing.Point(64, 248);
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		((System.Windows.Forms.Control)(object)this.lblUserName).Size = new System.Drawing.Size(85, 20);
		((System.Windows.Forms.Control)(object)this.lblUserName).TabIndex = 33;
		((System.Windows.Forms.Control)(object)this.lblUserName).Text = "Login Name";
		((ControlBase)this.lblUserName).WrapText = false;
		((System.Windows.Forms.Control)(object)this.chkRememberUserName).Anchor = System.Windows.Forms.AnchorStyles.None;
		((UltraToggleEditorBase)this.chkRememberUserName).Checked = true;
		((UltraToggleEditorBase)this.chkRememberUserName).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkRememberUserName).Location = new System.Drawing.Point(424, 248);
		((System.Windows.Forms.Control)(object)this.chkRememberUserName).Name = "chkRememberUserName";
		((System.Windows.Forms.Control)(object)this.chkRememberUserName).Size = new System.Drawing.Size(109, 20);
		((System.Windows.Forms.Control)(object)this.chkRememberUserName).TabIndex = 7;
		((System.Windows.Forms.Control)(object)this.chkRememberUserName).Text = "Remember";
		((UltraToggleEditorBase)this.chkRememberUserName).CheckedChanged += new System.EventHandler(chkRememberUserName_CheckedChanged);
		((System.Windows.Forms.Control)(object)this.chkRememberPassword).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.chkRememberPassword).Location = new System.Drawing.Point(424, 275);
		((System.Windows.Forms.Control)(object)this.chkRememberPassword).Name = "chkRememberPassword";
		((System.Windows.Forms.Control)(object)this.chkRememberPassword).Size = new System.Drawing.Size(109, 20);
		((System.Windows.Forms.Control)(object)this.chkRememberPassword).TabIndex = 9;
		((System.Windows.Forms.Control)(object)this.chkRememberPassword).Text = "Remember";
		((System.Windows.Forms.Control)(object)this.lblLanguage).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblLanguage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLanguage).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblLanguage).Location = new System.Drawing.Point(64, 156);
		((System.Windows.Forms.Control)(object)this.lblLanguage).Name = "lblLanguage";
		((System.Windows.Forms.Control)(object)this.lblLanguage).Size = new System.Drawing.Size(71, 20);
		((System.Windows.Forms.Control)(object)this.lblLanguage).TabIndex = 37;
		((System.Windows.Forms.Control)(object)this.lblLanguage).Text = "Language";
		((ControlBase)this.lblLanguage).WrapText = false;
		((System.Windows.Forms.Control)(object)this.cboLanguage).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cboLanguage.AutoCompleteMode = (AutoCompleteMode)2;
		val4.DataValue = "ValueListItem0";
		val4.DisplayText = "English";
		val5.DataValue = "ValueListItem1";
		val5.DisplayText = "عربي";
		this.cboLanguage.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val4, val5 });
		((System.Windows.Forms.Control)(object)this.cboLanguage).Location = new System.Drawing.Point(161, 153);
		((System.Windows.Forms.Control)(object)this.cboLanguage).Name = "cboLanguage";
		((System.Windows.Forms.Control)(object)this.cboLanguage).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.cboLanguage).TabIndex = 3;
		((System.Windows.Forms.Control)(object)this.cboCompany).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((System.Windows.Forms.Control)(object)this.cboCompany).Location = new System.Drawing.Point(161, 180);
		((System.Windows.Forms.Control)(object)this.cboCompany).Name = "cboCompany";
		((System.Windows.Forms.Control)(object)this.cboCompany).Size = new System.Drawing.Size(262, 27);
		((System.Windows.Forms.Control)(object)this.cboCompany).TabIndex = 4;
		((TextEditorControlBase)this.cboCompany).ValueChanged += new System.EventHandler(Company_ValueChanged);
		((System.Windows.Forms.Control)(object)this.lblCompany).Anchor = System.Windows.Forms.AnchorStyles.None;
		this.lblCompany.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompany).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblCompany).Location = new System.Drawing.Point(64, 183);
		((System.Windows.Forms.Control)(object)this.lblCompany).Name = "lblCompany";
		((System.Windows.Forms.Control)(object)this.lblCompany).Size = new System.Drawing.Size(68, 20);
		((System.Windows.Forms.Control)(object)this.lblCompany).TabIndex = 40;
		((System.Windows.Forms.Control)(object)this.lblCompany).Text = "Company";
		((ControlBase)this.lblCompany).WrapText = false;
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).Anchor = System.Windows.Forms.AnchorStyles.None;
		((UltraToggleEditorBase)this.chkRememberCompany).Checked = true;
		((UltraToggleEditorBase)this.chkRememberCompany).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).Location = new System.Drawing.Point(424, 183);
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).Name = "chkRememberCompany";
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).Size = new System.Drawing.Size(109, 20);
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).TabIndex = 5;
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).Text = "Remember";
		((UltraToggleEditorBase)this.chkRememberCompany).CheckedChanged += new System.EventHandler(Company_ValueChanged);
		((System.Windows.Forms.Control)(object)this.lblStatus).Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Red;
		((AppearanceBase)val6).Image = ERP.Properties.Resources.ALERT;
		((AppearanceBase)val6).ImageHAlign = (HAlign)3;
		((AppearanceBase)val6).TextHAlignAsString = "Center";
		((AppearanceBase)val6).TextVAlignAsString = "Middle";
		((ControlBase)this.lblStatus).Appearance = (AppearanceBase)(object)val6;
		this.lblStatus.AutoEllipsis = false;
		((ControlBase)this.lblStatus).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.lblStatus).ForeColor = System.Drawing.Color.Red;
		((ControlBase)this.lblStatus).ImageSize = new System.Drawing.Size(30, 30);
		this.lblStatus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblStatus).Location = new System.Drawing.Point(161, 305);
		((System.Windows.Forms.Control)(object)this.lblStatus).Name = "lblStatus";
		((System.Windows.Forms.Control)(object)this.lblStatus).Size = new System.Drawing.Size(291, 28);
		((System.Windows.Forms.Control)(object)this.lblStatus).TabIndex = 43;
		((UltraControlBase)this.lblStatus).UseAppStyling = false;
		((ControlBase)this.lblStatus).WrapText = false;
		((System.Windows.Forms.Control)(object)this.btnLogin).Anchor = System.Windows.Forms.AnchorStyles.None;
		((System.Windows.Forms.Control)(object)this.btnLogin).Location = new System.Drawing.Point(171, 349);
		((System.Windows.Forms.Control)(object)this.btnLogin).Name = "btnLogin";
		((System.Windows.Forms.Control)(object)this.btnLogin).Size = new System.Drawing.Size(238, 32);
		((System.Windows.Forms.Control)(object)this.btnLogin).TabIndex = 10;
		((System.Windows.Forms.Control)(object)this.btnLogin).Text = "Login";
		((System.Windows.Forms.Control)(object)this.btnLogin).Click += new System.EventHandler(btnLogin_Click);
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Red;
		((AppearanceBase)val7).Image = ERP.Properties.Resources.ALERT;
		((AppearanceBase)val7).ImageHAlign = (HAlign)3;
		((AppearanceBase)val7).TextHAlignAsString = "Center";
		((AppearanceBase)val7).TextVAlignAsString = "Middle";
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.ultraLabel1).BackColorInternal = System.Drawing.Color.Transparent;
		this.ultraLabel1.BorderStyleInner = (UIElementBorderStyle)1;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).ForeColor = System.Drawing.Color.Red;
		((ControlBase)this.ultraLabel1).ImageSize = new System.Drawing.Size(30, 30);
		this.ultraLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Location = new System.Drawing.Point(123, 305);
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Size = new System.Drawing.Size(34, 28);
		((System.Windows.Forms.Control)(object)this.ultraLabel1).TabIndex = 45;
		((UltraControlBase)this.ultraLabel1).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		((AppearanceBase)val8).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val8;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((UltraButtonBase)this.btnKeyboard).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Location = new System.Drawing.Point(522, 2);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Size = new System.Drawing.Size(50, 30);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabIndex = 504;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		base.ClientSize = new System.Drawing.Size(603, 420);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLogin);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkRememberCompany);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCompany);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompany);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLanguage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLanguage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkRememberUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkRememberPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStatus);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.gbxServerInfo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnServerEdit);
		this.ForeColor = System.Drawing.Color.RoyalBlue;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmLogIn";
		base.ShowInTaskbar = true;
		this.Text = "";
		base.Load += new System.EventHandler(frmLogIn_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnServerEdit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.gbxServerInfo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStatus, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkRememberPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkRememberUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLanguage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLanguage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompany, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCompany, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkRememberCompany, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLogin, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gbxServerInfo).EndInit();
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.gbxServerInfo).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtServer).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDBPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDBUser).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLanguage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCompany).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberCompany).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
