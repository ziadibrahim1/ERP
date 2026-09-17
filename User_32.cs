using System.Runtime.InteropServices;

internal class User_32
{
	public const int ENUM_CURRENT_SETTINGS = -1;

	public const int CDS_UPDATEREGISTRY = 1;

	public const int CDS_TEST = 2;

	public const int DISP_CHANGE_SUCCESSFUL = 0;

	public const int DISP_CHANGE_RESTART = 1;

	public const int DISP_CHANGE_FAILED = -1;

	[DllImport("user32.dll")]
	public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE1 devMode);

	[DllImport("user32.dll")]
	public static extern int ChangeDisplaySettings(ref DEVMODE1 devMode, int flags);
}
