using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ERP.Classes;

internal class CResolution
{
	public CResolution(int a, int b)
	{
		Screen primaryScreen = Screen.PrimaryScreen;
		DEVMODE1 devMode = default(DEVMODE1);
		devMode.dmDeviceName = new string(new char[32]);
		devMode.dmFormName = new string(new char[32]);
		devMode.dmSize = (short)Marshal.SizeOf(devMode);
		if (User_32.EnumDisplaySettings(null, -1, ref devMode) == 0)
		{
			return;
		}
		devMode.dmPelsWidth = a;
		devMode.dmPelsHeight = b;
		int num = User_32.ChangeDisplaySettings(ref devMode, 2);
		if (num == -1)
		{
			MessageBox.Show("Unable to process your request");
			MessageBox.Show("Description: Unable To Process Your Request. Sorry For This Inconvenience.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return;
		}
		switch (User_32.ChangeDisplaySettings(ref devMode, 1))
		{
		case 0:
			break;
		case 1:
			MessageBox.Show("Description: You Need To Reboot For The Change To Happen.\n If You Feel Any Problem After Rebooting Your Machine\nThen Try To Change Resolution In Safe Mode.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			break;
		default:
			MessageBox.Show("Description: Failed To Change The Resolution.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			break;
		}
	}
}
