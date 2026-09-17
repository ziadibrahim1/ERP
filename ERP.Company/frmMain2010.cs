using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Constructions;
using BusinessLayer.Defaults;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Alerts.Transactions;
using ERP.Classes;
using ERP.POS.Transactions;
using ERP.Properties;
using ERP.SystemOptions.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.AppStyling;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinToolbars;
using Microsoft.Win32;

namespace ERP.Company;

public class frmMain2010 : frmBase
{
	private delegate void SyncThreadDelegate();

	public static frmBase Curfrm = null;

	private decimal StopSyncFrom;

	private decimal StopSyncTo;

	public frmHome frmHome;

	public static frmChecksFrontScreen frmCheckScreen;

	public Thread SyncThread;

	private IContainer components = null;

	private UltraToolbarsManager UTM;

	private UltraToolbarsDockArea _frmBase_Toolbars_Dock_Area_Left;

	private UltraToolbarsDockArea _frmBase_Toolbars_Dock_Area_Right;

	private UltraToolbarsDockArea _frmBase_Toolbars_Dock_Area_Top;

	private UltraToolbarsDockArea _frmBase_Toolbars_Dock_Area_Bottom;

	private UltraGroupBox UGBFill;

	private Panel pnlTaskBar;

	private UltraGroupBox lblTitle2;

	private UltraButton btnMinimize;

	private UltraLabel lblTitle;

	private UltraButton btnExit;

	protected System.Windows.Forms.Timer timer1;

	protected System.Windows.Forms.Timer timer2;

	public UltraComboEditor cboHeader;

	protected System.Windows.Forms.Timer timer3;

	public frmMain2010()
	{
		InitializeComponent();
	}

	private void InitializeControls()
	{
		base.CancelButton = (IButtonControl)btnExit;
		GlobalVariables.ScreenHeight = Screen.PrimaryScreen.Bounds.Height;
		GlobalVariables.ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
		base.Location = new Point(0, 0);
		base.Height = Screen.PrimaryScreen.WorkingArea.Height;
		base.Width = Screen.PrimaryScreen.WorkingArea.Width;
		DataTable dataTable = Branches.Select(GlobalVariables.CurrentBranchID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalVariables.CurrentBranchNameEn = dataTable.Rows[0]["BranchNameEn"].ToString();
		GlobalVariables.CompanyNameEn = GlobalVariables.drDefaults["CompanyNameE"].ToString();
		GlobalVariables.CompanyNameAr = GlobalVariables.drDefaults["CompanyNameA"].ToString();
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? (GlobalVariables.CompanyNameAr + "  " + dataTable.Rows[0]["BranchNameAr"].ToString() + "                      Welcome User :" + GlobalVariables.UserName) : (GlobalVariables.CompanyNameEn + "  " + dataTable.Rows[0]["BranchNameEn"].ToString() + "                      Welcome User :" + GlobalVariables.UserName));
		((Control)(object)UGBFill).ControlAdded += UGBFill_ControlAdded;
	}

	private void UGBFill_ControlAdded(object sender, ControlEventArgs e)
	{
		ManageLabels();
	}

	private void UGBFill_ControlRemoved(object sender, ControlEventArgs e)
	{
		if ((e.Control as Form).Tag is DataRow && (((e.Control as Form).Tag as DataRow)["Form"].ToString() == "frmGroupsPrivileges" || ((e.Control as Form).Tag as DataRow)["Form"].ToString() == "frmUsersPrivileges"))
		{
			((ToolsCollectionBase)UTM.Tools).Clear();
			((RibbonTabCollectionBase)UTM.Ribbon.Tabs).Clear();
			CreateTabs();
		}
		else if ((e.Control as Form).Tag is DataRow && ((e.Control as Form).Tag as DataRow)["Form"].ToString() == "frmHomeDesigner")
		{
			frmHome.RefreshForms();
		}
		ManageLabels();
	}

	private void Label_Click(object sender, EventArgs e)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		for (int i = 0; i < pnlTaskBar.Controls.Count; i++)
		{
			Control control = pnlTaskBar.Controls[i];
			UltraLabel val = (UltraLabel)(object)((control is UltraLabel) ? control : null);
			if (((object)val).Equals((object)(UltraLabel)sender))
			{
				((ControlBase)val).Appearance.ForeColor = Color.Yellow;
				(((Control)(object)val).Tag as Form).BringToFront();
				Curfrm = ((Control)(object)val).Tag as frmBase;
			}
			else
			{
				((ControlBase)val).Appearance.ForeColor = Color.White;
			}
		}
	}

	private void ManageLabels()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		pnlTaskBar.Controls.Clear();
		foreach (Control control in ((Control)(object)UGBFill).Controls)
		{
			if (control is Form)
			{
				Form form = control as Form;
				UltraLabel val = new UltraLabel();
				((Control)(object)val).Click += Label_Click;
				((Control)(object)val).Tag = form;
				((Control)(object)val).Name = "lbl" + form.Name;
				if (form.Controls.IndexOfKey("lblTitle") > -1)
				{
					((Control)(object)val).Text = form.Controls["lblTitle"].Text;
				}
				else
				{
					((Control)(object)val).Text = (GlobalVariables.IsArabic ? (form.Tag as DataRow)["FormNameAr"].ToString() : (form.Tag as DataRow)["FormNameEn"].ToString());
				}
				if (form is frmReporViwer)
				{
					((Control)(object)val).Text += (GlobalVariables.IsArabic ? "  --  معاينة الطباعة" : "  --  Print Preview");
				}
				((Control)(object)val).Cursor = Cursors.Hand;
				GlobalFunctions.SetlblTitleStyle(val);
				((Control)(object)val).Height = pnlTaskBar.Height;
				if (form.Focused)
				{
					((ControlBase)val).Appearance.ForeColor = Color.Yellow;
				}
				((ControlBase)val).Appearance.BorderColor = Color.SteelBlue;
				val.BorderStyleOuter = (UIElementBorderStyle)6;
				((ControlBase)val).Appearance.TextHAlign = (HAlign)2;
				((ControlBase)val).Appearance.TextVAlign = (VAlign)2;
				((Control)(object)val).Visible = true;
				((Control)(object)val).Top = 0;
				pnlTaskBar.Controls.Add((Control)(object)val);
			}
		}
		foreach (UltraLabel control2 in pnlTaskBar.Controls)
		{
			UltraLabel val2 = control2;
			((Control)(object)val2).Width = pnlTaskBar.Width / pnlTaskBar.Controls.Count;
			((Control)(object)val2).Left = ((Control)(object)val2).Width * pnlTaskBar.Controls.IndexOf((Control)(object)val2);
		}
	}

	public override void PrepareData()
	{
		InitializeControls();
		CreateTabs();
		Program.FlashLoadingCloseing();
		Activate();
		if (Main.IsSynchronization)
		{
			GlobalVariables.dtSyncConn = SyncConnection.Select("-1", "-1", "0");
			if (!GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].Equals(DBNull.Value))
			{
				StopSyncFrom = Convert.ToDecimal(GlobalFunctions.GetDefault("StopSynchronizationFrom"));
				StopSyncTo = Convert.ToDecimal(GlobalFunctions.GetDefault("StopSynchronizationTo"));
				timer1.Interval = 60000 * Convert.ToInt32(GlobalFunctions.GetDefault("SynchronizationPeriod"));
				timer1.Start();
			}
		}
		if (!GlobalVariables.IsTechnicalUser)
		{
			timer2.Start();
		}
		if (GlobalVariables.dtForms.Select("Form = 'frmHomeDesigner'").Length != 0)
		{
			frmHome = new frmHome();
			frmHome.Tag = GlobalVariables.dtForms.Select("Form = 'frmHomeDesigner'")[0];
			frmHome.MdiParent = this;
			frmHome.frmMain = this;
			frmHome.TopLevel = false;
			frmHome.Parent = (Control)(object)UGBFill;
			frmHome.Width = ((Control)(object)UGBFill).Width;
			frmHome.Height = ((Control)(object)UGBFill).Height;
			frmHome.Show();
			frmHome.BringToFront();
		}
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Constructions'")[0]["Installed"]) && GlobalVariables.dtForms.Select("Form = 'frmClientDelayedInstallments'").Length != 0 && int.Parse(ContractsInstallments.SelectCountByContractInstallmentDate(DateTime.Now.Date.ToString(GlobalVariables.DateShortFormate), DateTime.Now.Date.AddDays(1.0).AddSeconds(-1.0).ToString(GlobalVariables.DateLongFormate)).Rows[0]["Count"].ToString()) > 0)
		{
			string formID = GlobalVariables.dtForms.Select("Form = 'frmClientDelayedInstallments'")[0]["FormID"].ToString();
			GlobalVariables.CanPrint = (CanPrint = GlobalFunctions.GetFormFunction(formID, "Printing"));
			GlobalVariables.CanExport = (CanExport = GlobalFunctions.GetFormFunction(formID, "Exporting"));
			GlobalVariables.CanPrintReport = (CanPrintReport = GlobalFunctions.GetFormFunction(formID, "Print Reports"));
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_ClientsInstallments_A_nologo.rpt" : "Rep_Con_ClientsInstallments_E_nologo.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", GlobalVariables.BranchIDs);
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", DateTime.Now.Date);
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", DateTime.Now.Date.AddDays(1.0).AddSeconds(-1.0));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@InstallmentsTypeIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@BuildingUnitIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@IsPayed", "0");
			frmReporViwer2.Tag = GlobalVariables.dtForms.Select("Form = 'frmClientDelayedInstallments'")[0];
			frmReporViwer2.MdiParent = this;
			frmReporViwer2.TopLevel = false;
			frmReporViwer2.Parent = (Control)(object)UGBFill;
			frmReporViwer2.Width = ((Control)(object)UGBFill).Width;
			frmReporViwer2.Height = ((Control)(object)UGBFill).Height;
			frmReporViwer2.Show();
			frmReporViwer2.BringToFront();
			Curfrm = frmReporViwer2;
		}
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Alerts'")[0]["Installed"]))
		{
			frmNotifications frmNotifications2 = new frmNotifications(10000, 120000, 5);
			frmNotifications2.Show();
		}
		if (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]) && Screen.AllScreens.Length > 1)
		{
			Rectangle bounds = Screen.AllScreens[1].Bounds;
			frmCheckScreen = new frmChecksFrontScreen();
			frmCheckScreen.StartPosition = FormStartPosition.Manual;
			frmCheckScreen.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
			frmCheckScreen.WindowState = FormWindowState.Maximized;
			frmCheckScreen.Show();
		}
		timer3.Start();
	}

	private void SetTapKeyTip(RibbonTab tab, string NameE)
	{
		NameE += "abcdefghijklmnopqrstxyzw123456789";
		for (int i = 0; i < NameE.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < ((DisposableObjectCollectionBase)tab.Ribbon.Tabs).Count; j++)
			{
				if (((RibbonTabCollectionBase)tab.Ribbon.Tabs)[j].KeyTip == NameE.Substring(i, 1).ToUpper() || NameE.Substring(i, 1).ToUpper() == " ")
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				tab.KeyTip = NameE.Substring(i, 1).ToUpper();
				break;
			}
		}
	}

	private void CreateTabs()
	{
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		((ToolsCollectionBase)UTM.Tools).Clear();
		((RibbonTabCollectionBase)UTM.Ribbon.Tabs).Clear();
		DataView dataView = new DataView(GlobalVariables.dtForms);
		dataView.RowFilter = " ParentID is Null  And Form not like '%MobileApp'";
		for (int i = 0; i < dataView.Count; i++)
		{
			if (dataView[i]["Form"].Equals("Help") || (GlobalVariables.dtSystemModules.Select(string.Concat("ModuleEnName='", dataView[i]["Form"], "'")).Length != 0 && (bool)GlobalVariables.dtSystemModules.Select(string.Concat("ModuleEnName='", dataView[i]["Form"], "'"))[0]["Installed"]))
			{
				UTM.Ribbon.Tabs.Add(dataView[i]["FormID"].ToString());
				((SubObjectBase)((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[dataView[i]["FormID"].ToString()]).Tag = dataView[i]["Form"].ToString();
				SetTapKeyTip(((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[dataView[i]["FormID"].ToString()], dataView[i]["Form"].ToString());
				((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[dataView[i]["FormID"].ToString()].Caption = (GlobalVariables.IsArabic ? dataView[i]["FormNameAr"].ToString() : dataView[i]["FormNameEn"].ToString());
				UTM.Ribbon.TabSettings.Appearance.FontData.Italic = (DefaultableBoolean)1;
				UTM.Ribbon.TabSettings.Appearance.FontData.Bold = (DefaultableBoolean)1;
				UTM.RightAlignedMenus = (DefaultableBoolean)1;
				CreateGroups(((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[dataView[i]["FormID"].ToString()]);
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)UTM.Ribbon.Tabs).Count; j++)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[j].Groups).Count; k++)
			{
				for (int l = 0; l < ((DisposableObjectCollectionBase)((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[j].Groups[k].Tools).Count; l++)
				{
					((ToolsCollectionBase)((RibbonTabCollectionBase)UTM.Ribbon.Tabs)[j].Groups[k].Tools)[l].InstanceProps.MinimumSizeOnRibbon = (RibbonToolSize)2;
				}
			}
		}
		CreateApplicationAreaRight();
		int num = 0;
		while (11 > ((DisposableObjectCollectionBase)((ApplicationMenuAreaBase)UTM.Ribbon.ApplicationMenu.ToolAreaLeft).Tools).Count)
		{
			ApplicationMenu applicationMenu = UTM.Ribbon.ApplicationMenu;
			ButtonTool val = new ButtonTool("Additonal" + num);
			((ToolsCollectionBase)UTM.Tools).Add((ToolBase)(object)val);
			((ApplicationMenuAreaBase)applicationMenu.ToolAreaLeft).Tools.AddTool("Additonal" + num);
			num++;
		}
	}

	private void CreateGroups(RibbonTab Tab)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		DataRow[] array = GlobalVariables.dtForms.Select("ParentID = '" + ((KeyedSubObjectBase)Tab).Key.ToString() + "'");
		for (int i = 0; i < array.Length; i++)
		{
			RibbonGroup val = new RibbonGroup(array[i]["FormID"].ToString(), GlobalVariables.IsArabic ? array[i]["FormNameAr"].ToString() : array[i]["FormNameEn"].ToString());
			((SubObjectBase)val).Tag = array[i]["Form"].ToString();
			val.LayoutAlignment = (RibbonGroupLayoutAlignment)1;
			Tab.Groups.Add(val);
			CreateButtonTool(val);
		}
	}

	private void CreateApplicationAreaRight()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		ApplicationMenu applicationMenu = UTM.Ribbon.ApplicationMenu;
		DataTable dataTable = UsersFormsLog.SelectMostUse(GlobalVariables.UserID, IsFromServer: false);
		PopupGalleryTool val = new PopupGalleryTool("MostUse");
		((ToolBase)val).CustomizedCaption = "Most Use Forms";
		((SubObjectBase)val).Tag = "MostUse";
		((ToolsCollectionBase)UTM.Tools).Add((ToolBase)(object)val);
		((ApplicationMenuAreaBase)applicationMenu.ToolAreaLeft).Tools.AddTool("MostUse");
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (((KeyedSubObjectsCollectionBase)UTM.Tools).Exists(dataTable.Rows[i]["FormID"].ToString()))
			{
				((PopupMenuTool)val).Tools.AddTool(dataTable.Rows[i]["FormID"].ToString());
			}
		}
		DataTable dataTable2 = UsersFormsLog.SelectRecent(GlobalVariables.UserID, IsFromServer: false);
		val = new PopupGalleryTool("Recent");
		((ToolBase)val).CustomizedCaption = "Recent Forms";
		((SubObjectBase)val).Tag = "Recent";
		((ToolsCollectionBase)UTM.Tools).Add((ToolBase)(object)val);
		((ApplicationMenuAreaBase)applicationMenu.ToolAreaLeft).Tools.AddTool("Recent");
		for (int j = 0; j < dataTable2.Rows.Count; j++)
		{
			if (((KeyedSubObjectsCollectionBase)UTM.Tools).Exists(dataTable2.Rows[j]["FormID"].ToString()))
			{
				((PopupMenuTool)val).Tools.AddTool(dataTable2.Rows[j]["FormID"].ToString());
			}
		}
	}

	private void SetToolKeyTip(ButtonTool btn, RibbonTab tab, string NameE)
	{
		NameE += "abcdefghijklmnopqrstxyzw123456789";
		for (int i = 0; i < NameE.Length; i++)
		{
			bool flag = false;
			for (int j = 0; j < ((DisposableObjectCollectionBase)tab.Groups).Count; j++)
			{
				for (int k = 0; k < ((DisposableObjectCollectionBase)tab.Groups[j].Tools).Count; k++)
				{
					if (((ToolPropsBase)((ToolsCollectionBase)tab.Groups[j].Tools)[k].SharedProps).KeyTip == NameE.Substring(i, 1).ToUpper() || NameE.Substring(i, 1).ToUpper() == " ")
					{
						flag = true;
						break;
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (!flag)
			{
				((ToolPropsBase)((ToolBase)btn).SharedProps).KeyTip = NameE.Substring(i, 1).ToUpper();
				break;
			}
		}
	}

	private void CreateButtonTool(RibbonGroup Group)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		DataRow[] array = GlobalVariables.dtForms.Select("ParentID = '" + ((KeyedSubObjectBase)Group).Key.ToString() + "'");
		for (int i = 0; i < array.Length; i++)
		{
			ButtonTool val = new ButtonTool(array[i]["FormID"].ToString());
			((ToolPropsBase)((ToolBase)val).SharedProps).AppearancesSmall.Appearance.Image = Resources.RibbonIcon;
			((ToolPropsBase)((ToolBase)val).SharedProps).AppearancesSmall.Appearance.TextHAlign = (HAlign)2;
			((ToolPropsBase)((ToolBase)val).SharedProps).AppearancesSmall.Appearance.FontData.Bold = (DefaultableBoolean)1;
			((SubObjectBase)val).Tag = array[i];
			SetToolKeyTip(val, Group.Tab, array[i]["FormNameEn"].ToString());
			((ToolBase)val).SharedProps.ToolTipText = (GlobalVariables.IsArabic ? array[i]["FormNameEn"].ToString() : array[i]["FormNameAr"].ToString());
			((ToolPropsBase)((ToolBase)val).SharedProps).Caption = (GlobalVariables.IsArabic ? array[i]["FormNameAr"].ToString() : array[i]["FormNameEn"].ToString());
			((ToolsCollectionBase)UTM.Tools).Add((ToolBase)(object)val);
			Group.Tools.AddTool(array[i]["FormID"].ToString());
		}
	}

	private void UTM_ToolClick(object sender, ToolClickEventArgs e)
	{
		frmBase frmBase2 = null;
		DataRow dataRow = (DataRow)((SubObjectBase)((ToolsCollectionBase)UTM.Tools)[((KeyedSubObjectBase)((ToolEventArgs)e).Tool).Key]).Tag;
		if (dataRow == null)
		{
			return;
		}
		if (((Control)(object)UGBFill).Controls[dataRow["FormFullName"].ToString()] == null)
		{
			Type type = Type.GetType(dataRow["FormFullName"].ToString(), throwOnError: false);
			UsersFormsLog.Insert_Update("-1", GlobalVariables.UserID, dataRow["FormID"].ToString(), DateTime.Now.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, "0", IsFromServer: false);
			if (type != null)
			{
				frmBase2 = (frmBase)Activator.CreateInstance(type);
				frmBase2.Name = dataRow["FormFullName"].ToString();
				if (frmBase2.Controls.IndexOfKey("lblTitle") > -1)
				{
					frmBase2.Controls["lblTitle"].Text = ((ToolEventArgs)e).Tool.CaptionResolved;
					frmBase2.Controls["lblTitle"].SendToBack();
				}
				frmBase2.Tag = dataRow;
				frmBase2.MdiParent = this;
				frmBase2.Parent = (Control)(object)UGBFill;
				frmBase2.Width = ((Control)(object)UGBFill).Width;
				frmBase2.Height = ((Control)(object)UGBFill).Height;
				frmBase2.Show();
				frmBase2.BringToFront();
				frmBase2.Focus();
				Curfrm = frmBase2;
				ManageLabels();
			}
		}
		else
		{
			((Control)(object)UGBFill).Controls[dataRow["FormFullName"].ToString()].BringToFront();
			((Control)(object)UGBFill).Controls[dataRow["FormFullName"].ToString()].Focus();
			ManageLabels();
		}
	}

	public void OpenForm(string formID)
	{
		frmBase frmBase2 = null;
		DataRow dataRow = GlobalVariables.dtForms.Select("FormID = " + formID)[0];
		if (dataRow == null)
		{
			return;
		}
		if (((Control)(object)UGBFill).Controls[dataRow["Form"].ToString()] == null)
		{
			Type type = Type.GetType(dataRow["FormFullName"].ToString(), throwOnError: false);
			UsersFormsLog.Insert_Update("-1", GlobalVariables.UserID, dataRow["FormID"].ToString(), DateTime.Now.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, "0", IsFromServer: false);
			if (type != null)
			{
				frmBase2 = (frmBase)Activator.CreateInstance(type);
				frmBase2.Name = dataRow["Form"].ToString();
				if (frmBase2.Controls.IndexOfKey("lblTitle") > -1)
				{
					frmBase2.Controls["lblTitle"].Text = (GlobalVariables.IsArabic ? dataRow["FormNameAr"].ToString() : dataRow["FormNameEn"].ToString());
					frmBase2.Controls["lblTitle"].SendToBack();
				}
				frmBase2.Tag = dataRow;
				frmBase2.MdiParent = this;
				frmBase2.Parent = (Control)(object)UGBFill;
				frmBase2.Width = ((Control)(object)UGBFill).Width;
				frmBase2.Height = ((Control)(object)UGBFill).Height;
				frmBase2.Show();
				frmBase2.BringToFront();
				frmBase2.Focus();
				Curfrm = frmBase2;
				ManageLabels();
			}
		}
		else
		{
			((Control)(object)UGBFill).Controls[dataRow["Form"].ToString()].BringToFront();
			((Control)(object)UGBFill).Controls[dataRow["Form"].ToString()].Focus();
			ManageLabels();
		}
	}

	public bool checkFormExists(string formID)
	{
		if (((KeyedSubObjectsCollectionBase)UTM.Tools).Exists(formID))
		{
			return true;
		}
		return false;
	}

	private void ChangeLanguage()
	{
		Thread.CurrentThread.CurrentUICulture = new CultureInfo(GlobalVariables.IsArabic ? "EN" : "AR");
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		GlobalVariables.IsArabic = !GlobalVariables.IsArabic;
		RightToLeft = ((RightToLeft != RightToLeft.Yes) ? RightToLeft.Yes : RightToLeft.No);
		InitializeControls();
		((Control)(object)UGBFill).RightToLeft = RightToLeft;
		((Control)(object)lblTitle2).Location = new Point(base.Width - ((Control)(object)lblTitle2).Right, 0);
		RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(GlobalVariables.path, writable: true);
		registryKey.SetValue("Language", GlobalVariables.IsArabic ? 1 : 0);
		((ToolsCollectionBase)UTM.Tools).Clear();
		((RibbonTabCollectionBase)UTM.Ribbon.Tabs).Clear();
		CreateTabs();
		GlobalVariables.InformationMB.Show("تم تغيير اللغة إلى العربي\u0651ة", "Language was changed to English");
	}

	private void LogOff()
	{
		if (((Control)(object)UGBFill).Controls.Count > 1)
		{
			GlobalVariables.InformationMB.Show("برجاء إغلاق الشاشات المفتوحة أولا", "please Close All Screens First");
			return;
		}
		UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID);
		base.FormClosing -= frmMain_FormClosing;
		Application.DoEvents();
		Application.Restart();
	}

	private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		base.FormClosing -= frmMain_FormClosing;
		GlobalVariables.QuestionMB.Show("هل تريد الخروج من البرنامج", "Are You Sure You want to Exit?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID);
			Application.Exit();
			Dispose();
		}
		else
		{
			e.Cancel = true;
			base.FormClosing += frmMain_FormClosing;
		}
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		GlobalVariables.QuestionMB.Show("هل تريد الخروج من البرنامج", "Are You Sure You want to Exit?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID);
			Close();
		}
	}

	private void btnMinimize_Click(object sender, EventArgs e)
	{
		base.WindowState = FormWindowState.Minimized;
	}

	private void UGBFill_Resize(object sender, EventArgs e)
	{
		foreach (Control control in ((Control)(object)UGBFill).Controls)
		{
			if (control is Form)
			{
				(control as Form).Location = new Point(0, 0);
				(control as Form).Width = ((Control)(object)UGBFill).Width;
				(control as Form).Height = ((Control)(object)UGBFill).Height;
			}
		}
	}

	private void btnMinimize_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.Control && e.Shift && e.KeyCode == Keys.G)
		{
			GlobalVariables.QuestionMB.Show("إعادة بناء صلاحيات النظام ؟", "Reset System Privileges?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				BusinessLayer.Privilege.Groups.ResetGroupAdmin(IsFromServer: true);
				GlobalVariables.InformationMB.Show("تم إعادة بناء صلاحيات النظام", "System Privileges Reset Successfully");
				GlobalFunctions.LoadUserPrivileges();
				((ToolsCollectionBase)UTM.Tools).Clear();
				((RibbonTabCollectionBase)UTM.Ribbon.Tabs).Clear();
				CreateTabs();
			}
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		int hour = DateTime.Now.Hour;
		if (((StopSyncFrom == 0m && StopSyncTo == 0m) || (StopSyncFrom <= StopSyncTo && ((decimal)hour < StopSyncFrom || (decimal)hour >= StopSyncTo)) || (StopSyncFrom > StopSyncTo && (decimal)hour >= StopSyncTo && (decimal)hour < StopSyncFrom)) && SyncConnection.SetUnderSync())
		{
			try
			{
				SyncThread = new Thread(SyncThreadStart);
				SyncThread.Start();
			}
			catch
			{
			}
		}
	}

	public void SyncThreadStart()
	{
		try
		{
			string text = "";
			Synchronization.ServerR = GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString();
			Synchronization.DataBaseR = GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString();
			Synchronization.UserIDR = GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString();
			Synchronization.PasswordR = GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString();
			text = GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].ToString();
			if (!Synchronization.CreateConnectionL() || !Synchronization.CreateConnectionR())
			{
				Synchronization.EndUnderSync();
				return;
			}
			Synchronization.MoveDataL(text);
			Synchronization.MoveDataR(text);
			Synchronization.DeleteFromRemote(text);
			Synchronization.DeleteFromLocal(text);
			DataTable dataTable = Synchronization.ExecuteQuery_DataTableLocal(" select * From Sync_Tables t order by SyncOrder ");
			DataTable dataTable2 = Synchronization.ExecuteQuery_DataTableLocal(" select * From Sync_Tables t where t.TableName in(select Distinct TableName from Sync_Transactions where BranchID=" + text + " AND State<>3 )  order by SyncOrder ");
			DataTable dataTable3 = Synchronization.ExecuteQuery_DataTableRemote(" select * From Sync_Tables t where t.TableName in(select Distinct TableName from Sync_Transactions where BranchID=" + text + " AND State<>3 )  order by SyncOrder ");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text2 = dataTable.Rows[i]["TableName"].ToString();
				if (dataTable2.Select("TableName='" + text2 + "'").Length != 0 && Convert.ToBoolean(dataTable.Rows[i]["ToServer"]))
				{
					Synchronization.InsertUpdateToRemote(text2, text);
				}
				if (dataTable3.Select("TableName='" + text2 + "'").Length != 0 && Convert.ToBoolean(dataTable.Rows[i]["FromServer"]))
				{
					Synchronization.InsertUpdateToLocal(text2, text);
				}
			}
			Synchronization.ActualDeleteFromRemote();
			Synchronization.ActualDeleteFromLocal(text);
			Synchronization.ExecuteQuery_DataTableNewConnectionLocal(" SC_ItemsTransactions_Management ");
			Synchronization.ExecuteQuery_DataTableNewConnectionRemote(" SC_ItemsTransactions_Management ");
		}
		catch
		{
		}
		Synchronization.EndUnderSync();
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		timer2.Stop();
		if (Users.LoginEndedState(GlobalVariables.UserLoginID))
		{
			GlobalVariables.InformationMB.Show("تم إخراج المستخدم سيتم إغلاق النظام", "Sorry,User Kicked out System Will shut Down");
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID);
			Environment.Exit(-1);
		}
		timer2.Start();
	}

	private void cboHeader_ValueChanged(object sender, EventArgs e)
	{
		if (cboHeader.SelectedIndex > -1)
		{
			GlobalVariables.Style = ((TextEditorControlBase)cboHeader).Value.ToString();
			MemoryStream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(GlobalVariables.Style));
			Stream stream = memoryStream;
			StyleManager.Load(stream);
			SetStyle(this);
			ManageLabels();
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(GlobalVariables.path, writable: true);
			registryKey.SetValue("Style", GlobalVariables.Style);
		}
	}

	public void SetStyle(Control c)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		foreach (object control2 in c.Controls)
		{
			Control control = control2 as Control;
			if (control2 is UltraLabel)
			{
				UltraLabel val = (UltraLabel)control2;
				if (((Control)(object)val).Name == "lblTitle" || ((Control)(object)val).Name == "lblTitle2" || ((Control)(object)val).Name == "lblCode")
				{
					GlobalFunctions.SetlblTitleStyle(val);
				}
			}
			else if (control2 is Form)
			{
				Form formStyle = (Form)control2;
				GlobalFunctions.SetFormStyle(formStyle);
			}
			SetStyle(control2 as Control);
		}
	}

	private void timer3_Tick(object sender, EventArgs e)
	{
		if (!(GlobalVariables.UserID != "1"))
		{
			return;
		}
		string terminalClientName = GlobalFunctions.GetTerminalClientName();
		if (terminalClientName != null && terminalClientName != "" && terminalClientName != GlobalVariables.RemoteSessionName)
		{
			Environment.Exit(-1);
		}
		if (SystemInformation.TerminalServerSession && !GlobalFunctions.GetOption("AllowRemoteConnction"))
		{
			try
			{
				Process.Start("License.exe", "Support>Required ERP>Smart>solutionS Remote>Desktop>Connection>Is>Not>Allowed");
			}
			catch
			{
			}
			Environment.Exit(-1);
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Company.frmMain2010));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		ValueListItem val5 = new ValueListItem();
		ValueListItem val6 = new ValueListItem();
		ValueListItem val7 = new ValueListItem();
		ValueListItem val8 = new ValueListItem();
		ValueListItem val9 = new ValueListItem();
		ValueListItem val10 = new ValueListItem();
		ValueListItem val11 = new ValueListItem();
		ValueListItem val12 = new ValueListItem();
		ValueListItem val13 = new ValueListItem();
		ValueListItem val14 = new ValueListItem();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		this.UTM = new UltraToolbarsManager(this.components);
		this._frmBase_Toolbars_Dock_Area_Left = new UltraToolbarsDockArea();
		this._frmBase_Toolbars_Dock_Area_Right = new UltraToolbarsDockArea();
		this._frmBase_Toolbars_Dock_Area_Top = new UltraToolbarsDockArea();
		this._frmBase_Toolbars_Dock_Area_Bottom = new UltraToolbarsDockArea();
		this.UGBFill = new UltraGroupBox();
		this.pnlTaskBar = new System.Windows.Forms.Panel();
		this.lblTitle2 = new UltraGroupBox();
		this.cboHeader = new UltraComboEditor();
		this.lblTitle = new UltraLabel();
		this.btnMinimize = new UltraButton();
		this.btnExit = new UltraButton();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.timer3 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTM).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBFill).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.lblTitle2).BeginInit();
		((System.Windows.Forms.Control)(object)this.lblTitle2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboHeader).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		this.UTM.AlwaysShowMenusExpanded = (DefaultableBoolean)1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(234, 238, 255);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(234, 238, 255);
		this.UTM.Appearance = (AppearanceBase)(object)val;
		this.UTM.DesignerFlags = 1;
		this.UTM.DockWithinContainer = this;
		this.UTM.DockWithinContainerBaseType = typeof(ERP.AbstractForms.frmBase);
		this.UTM.Office2007UICompatibility = false;
		this.UTM.Ribbon.ApplicationMenuButtonImage = ERP.Properties.Resources.Logo;
		this.UTM.Ribbon.DisplayMode = (RibbonDisplayMode)1;
		this.UTM.Ribbon.Visible = true;
		this.UTM.RuntimeCustomizationOptions = (RuntimeCustomizationOptions)0;
		this.UTM.SettingsKey = "frmMain2010.UTM";
		this.UTM.ShowFullMenusDelay = 50;
		this.UTM.ToolClick += new ToolClickEventHandler(UTM_ToolClick);
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Left).AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Left).BackColor = System.Drawing.Color.FromArgb(191, 219, 255);
		this._frmBase_Toolbars_Dock_Area_Left.DockedPosition = (DockedPosition)2;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Left).ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBase_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 8;
		resources.ApplyResources(this._frmBase_Toolbars_Dock_Area_Left, "_frmBase_Toolbars_Dock_Area_Left");
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Left).Name = "_frmBase_Toolbars_Dock_Area_Left";
		this._frmBase_Toolbars_Dock_Area_Left.ToolbarsManager = this.UTM;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Right).AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Right).BackColor = System.Drawing.Color.FromArgb(191, 219, 255);
		this._frmBase_Toolbars_Dock_Area_Right.DockedPosition = (DockedPosition)3;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Right).ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBase_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 8;
		resources.ApplyResources(this._frmBase_Toolbars_Dock_Area_Right, "_frmBase_Toolbars_Dock_Area_Right");
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Right).Name = "_frmBase_Toolbars_Dock_Area_Right";
		this._frmBase_Toolbars_Dock_Area_Right.ToolbarsManager = this.UTM;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Top).AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Top).BackColor = System.Drawing.Color.FromArgb(191, 219, 255);
		this._frmBase_Toolbars_Dock_Area_Top.DockedPosition = (DockedPosition)0;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Top).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(this._frmBase_Toolbars_Dock_Area_Top, "_frmBase_Toolbars_Dock_Area_Top");
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Top).Name = "_frmBase_Toolbars_Dock_Area_Top";
		this._frmBase_Toolbars_Dock_Area_Top.ToolbarsManager = this.UTM;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Bottom).AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Bottom).BackColor = System.Drawing.Color.FromArgb(191, 219, 255);
		this._frmBase_Toolbars_Dock_Area_Bottom.DockedPosition = (DockedPosition)1;
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Bottom).ForeColor = System.Drawing.SystemColors.ControlText;
		this._frmBase_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 8;
		resources.ApplyResources(this._frmBase_Toolbars_Dock_Area_Bottom, "_frmBase_Toolbars_Dock_Area_Bottom");
		((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Bottom).Name = "_frmBase_Toolbars_Dock_Area_Bottom";
		this._frmBase_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.UTM;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(234, 238, 255);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(234, 238, 255);
		this.UGBFill.Appearance = (AppearanceBase)(object)val2;
		this.UGBFill.BorderStyle = (GroupBoxBorderStyle)4;
		resources.ApplyResources(this.UGBFill, "UGBFill");
		((System.Windows.Forms.Control)(object)this.UGBFill).Name = "UGBFill";
		((UltraControlBase)this.UGBFill).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.UGBFill).ControlRemoved += new System.Windows.Forms.ControlEventHandler(UGBFill_ControlRemoved);
		((System.Windows.Forms.Control)(object)this.UGBFill).Resize += new System.EventHandler(UGBFill_Resize);
		resources.ApplyResources(this.pnlTaskBar, "pnlTaskBar");
		this.pnlTaskBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pnlTaskBar.Name = "pnlTaskBar";
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		this.lblTitle2.Appearance = (AppearanceBase)(object)val3;
		this.lblTitle2.BorderStyle = (GroupBoxBorderStyle)4;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Black;
		this.lblTitle2.ContentAreaAppearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Controls.Add((System.Windows.Forms.Control)(object)this.cboHeader);
		((System.Windows.Forms.Control)(object)this.lblTitle2).Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		((System.Windows.Forms.Control)(object)this.lblTitle2).Controls.Add((System.Windows.Forms.Control)(object)this.btnMinimize);
		((System.Windows.Forms.Control)(object)this.lblTitle2).Controls.Add((System.Windows.Forms.Control)(object)this.btnExit);
		this.lblTitle2.HeaderBorderStyle = (UIElementBorderStyle)1;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		this.cboHeader.AutoCompleteMode = (AutoCompleteMode)2;
		this.cboHeader.DropDownListWidth = 100;
		val5.DataValue = "ISSDarkGreen";
		resources.ApplyResources(val5, "valueListItem7");
		((SubObjectBase)val5).ForceApplyResources = "";
		val6.DataValue = "ISSDarkBlue";
		resources.ApplyResources(val6, "valueListItem2");
		((SubObjectBase)val6).ForceApplyResources = "";
		val7.DataValue = "ISSOlive";
		resources.ApplyResources(val7, "valueListItem9");
		((SubObjectBase)val7).ForceApplyResources = "";
		val8.DataValue = "ISSDarkRed";
		resources.ApplyResources(val8, "valueListItem8");
		((SubObjectBase)val8).ForceApplyResources = "";
		val9.DataValue = "ISSGreen";
		resources.ApplyResources(val9, "valueListItem3");
		((SubObjectBase)val9).ForceApplyResources = "";
		val10.DataValue = "ISSRed";
		resources.ApplyResources(val10, "valueListItem4");
		((SubObjectBase)val10).ForceApplyResources = "";
		val11.DataValue = "ISSGray";
		resources.ApplyResources(val11, "valueListItem5");
		((SubObjectBase)val11).ForceApplyResources = "";
		val12.DataValue = "ISSPurple";
		resources.ApplyResources(val12, "valueListItem6");
		((SubObjectBase)val12).ForceApplyResources = "";
		val13.DataValue = "ISS";
		resources.ApplyResources(val13, "valueListItem1");
		((SubObjectBase)val13).ForceApplyResources = "";
		val14.DataValue = "ISSGold";
		resources.ApplyResources(val14, "valueListItem10");
		((SubObjectBase)val14).ForceApplyResources = "";
		this.cboHeader.Items.AddRange((ValueListItem[])(object)new ValueListItem[10] { val5, val6, val7, val8, val9, val10, val11, val12, val13, val14 });
		resources.ApplyResources(this.cboHeader, "cboHeader");
		((System.Windows.Forms.Control)(object)this.cboHeader).Name = "cboHeader";
		((TextEditorControlBase)this.cboHeader).Nullable = false;
		((TextEditorControlBase)this.cboHeader).ValueChanged += new System.EventHandler(cboHeader_ValueChanged);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Beige;
		resources.ApplyResources(val15, "appearance4");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnMinimize, "btnMinimize");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val16).BorderColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).Image = ERP.Properties.Resources.Minimize;
		((AppearanceBase)val16).ImageHAlign = (HAlign)2;
		((AppearanceBase)val16).ImageVAlign = (VAlign)2;
		((ControlBase)this.btnMinimize).Appearance = (AppearanceBase)(object)val16;
		((UltraButtonBase)this.btnMinimize).ButtonStyle = (UIElementButtonStyle)3;
		((System.Windows.Forms.Control)(object)this.btnMinimize).Cursor = System.Windows.Forms.Cursors.Hand;
		((UltraButtonBase)this.btnMinimize).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		((ControlBase)this.btnMinimize).HotTrackAppearance = (AppearanceBase)(object)val17;
		((ControlBase)this.btnMinimize).ImageSize = new System.Drawing.Size(14, 14);
		((System.Windows.Forms.Control)(object)this.btnMinimize).Name = "btnMinimize";
		((UltraButtonBase)this.btnMinimize).ShowFocusRect = false;
		((UltraButtonBase)this.btnMinimize).ShowOutline = false;
		((UltraControlBase)this.btnMinimize).UseFlatMode = (DefaultableBoolean)2;
		((UltraControlBase)this.btnMinimize).UseOsThemes = (DefaultableBoolean)2;
		((System.Windows.Forms.Control)(object)this.btnMinimize).Click += new System.EventHandler(btnMinimize_Click);
		((System.Windows.Forms.Control)(object)this.btnMinimize).KeyUp += new System.Windows.Forms.KeyEventHandler(btnMinimize_KeyUp);
		resources.ApplyResources(this.btnExit, "btnExit");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).Image = ERP.Properties.Resources.exit;
		((AppearanceBase)val18).ImageHAlign = (HAlign)2;
		((AppearanceBase)val18).ImageVAlign = (VAlign)2;
		((ControlBase)this.btnExit).Appearance = (AppearanceBase)(object)val18;
		((UltraButtonBase)this.btnExit).ButtonStyle = (UIElementButtonStyle)3;
		((System.Windows.Forms.Control)(object)this.btnExit).Cursor = System.Windows.Forms.Cursors.Hand;
		((UltraButtonBase)this.btnExit).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(192, 0, 0);
		((ControlBase)this.btnExit).HotTrackAppearance = (AppearanceBase)(object)val19;
		((ControlBase)this.btnExit).ImageSize = new System.Drawing.Size(14, 14);
		((System.Windows.Forms.Control)(object)this.btnExit).Name = "btnExit";
		((AppearanceBase)val20).BackColor = System.Drawing.Color.FromArgb(192, 0, 0);
		((AppearanceBase)val20).BackColor2 = System.Drawing.Color.FromArgb(192, 0, 0);
		((UltraButtonBase)this.btnExit).PressedAppearance = (AppearanceBase)(object)val20;
		((UltraControlBase)this.btnExit).UseFlatMode = (DefaultableBoolean)2;
		((UltraControlBase)this.btnExit).UseOsThemes = (DefaultableBoolean)2;
		((System.Windows.Forms.Control)(object)this.btnExit).Click += new System.EventHandler(btnExit_Click);
		this.timer1.Interval = 1800000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.timer2.Interval = 30000;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.timer3.Interval = 100000;
		this.timer3.Tick += new System.EventHandler(timer3_Tick);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBFill);
		base.Controls.Add(this.pnlTaskBar);
		base.Controls.Add((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Left);
		base.Controls.Add((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Right);
		base.Controls.Add((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Bottom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Top);
		base.IsMdiContainer = true;
		base.Name = "frmMain2010";
		base.ShowInTaskbar = true;
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Top, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Bottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Right, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this._frmBase_Toolbars_Dock_Area_Left, 0);
		base.Controls.SetChildIndex(this.pnlTaskBar, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBFill, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTM).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBFill).EndInit();
		((System.ComponentModel.ISupportInitialize)this.lblTitle2).EndInit();
		((System.Windows.Forms.Control)(object)this.lblTitle2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.lblTitle2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboHeader).EndInit();
		base.ResumeLayout(false);
	}
}
