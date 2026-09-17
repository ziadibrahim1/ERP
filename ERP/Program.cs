using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.Classes;
using ERP.Company;
using ERP.Properties;
using Infragistics.Win.AppStyling;
using Microsoft.Win32;

namespace ERP;

internal static class Program
{
	private delegate void FlashLoadingThreadStartDelegate();

	private delegate void FlashLoadingThreadCloseDelegate();

	public static Thread FlashLoadingThread;

	public static frmLoadingFlash frmLoadingFlash;

	[STAThread]
	private static void Main()
	{
		try
		{
			FlashLoadingThread = new Thread(FlashLoadingThreadStart);
			FlashLoadingThread.Start();
		}
		catch
		{
		}
		Application.ApplicationExit += Application_ApplicationExit;
		Application.ThreadException += Application_ThreadException;
		Application.EnableVisualStyles();
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(GlobalVariables.path, writable: true);
			if (registryKey.GetValue("Style") == null)
			{
				registryKey.SetValue("Style", "ISSDarkBlue");
			}
			GlobalVariables.Style = registryKey.GetValue("Style").ToString();
		}
		catch
		{
		}
		MemoryStream memoryStream = new MemoryStream((byte[])Resources.ResourceManager.GetObject(GlobalVariables.Style));
		Stream stream = memoryStream;
		StyleManager.Load(stream);
		frmLogIn frmLogIn2 = new frmLogIn();
		frmLogIn2.BringToFront();
		frmLogIn2.Text = "ERP Bar";
		frmLogIn2.ShowDialog();
		if (GlobalVariables.LoadApplication)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(GlobalVariables.IsArabic ? "ar" : "en");
			CultureInfo cultureInfo = new CultureInfo("en-US", useUserOverride: false);
			cultureInfo.DateTimeFormat.LongDatePattern = GlobalVariables.DateLongFormateMS;
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Application.Run(new frmMain2010());
		}
	}

	private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
	{
		GlobalVariables.InformationMB.Show(e.Exception.Message);
		Application.UseWaitCursor = false;
	}

	private static void Application_ApplicationExit(object sender, EventArgs e)
	{
		try
		{
			Users.LogOff(GlobalVariables.UserLoginID, IsFromServer: false);
		}
		catch (Exception)
		{
		}
	}

	public static void FlashLoadingThreadStart()
	{
		try
		{
			if (frmLoadingFlash == null)
			{
				frmLoadingFlash = new frmLoadingFlash();
				frmLoadingFlash.ShowDialog();
			}
		}
		catch (ThreadAbortException ex)
		{
			MessageBox.Show(ex.Message);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}

	public static void FlashLoadingCloseing()
	{
		try
		{
			if (frmLoadingFlash != null)
			{
				if (frmLoadingFlash.InvokeRequired)
				{
					frmLoadingFlash.Invoke(new FlashLoadingThreadCloseDelegate(FlashLoadingCloseing));
					return;
				}
				frmLoadingFlash.Close();
				frmLoadingFlash = null;
			}
		}
		catch (ThreadAbortException ex)
		{
			MessageBox.Show(ex.Message);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
		}
	}
}
