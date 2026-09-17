using System;
using System.Runtime.InteropServices;

namespace ERP.Classes;

public class WinApi
{
	private const int SM_CXSCREEN = 0;

	private const int SM_CYSCREEN = 1;

	private static IntPtr HWND_TOP = IntPtr.Zero;

	private const int SWP_SHOWWINDOW = 64;

	public static int ScreenX => GetSystemMetrics(0);

	public static int ScreenY => GetSystemMetrics(1);

	[DllImport("user32.dll")]
	public static extern int GetSystemMetrics(int which);

	[DllImport("user32.dll")]
	public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);

	public static void SetWinFullScreen(IntPtr hwnd)
	{
		SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, 64u);
	}
}
