using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class E_StrongAttDll
{
	private static bool OpenFlag;

	public static int nCommHandleIndex;

	public static DataTable GetAtt(string vpszIPAddress, int vpszNetPort, int vpszNetPassword)
	{
		DataTable result = new DataTable();
		int anProtocolType = 0;
		long num = -1L;
		nCommHandleIndex = FK_ConnectNet(1, vpszIPAddress, vpszNetPort, 5000, anProtocolType, vpszNetPassword, 1261);
		if (nCommHandleIndex < 1)
		{
			num = nCommHandleIndex;
			MessageBox.Show(ReturnResultPrint(num), "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			OpenFlag = true;
			result = funcGetGeneralLogData();
		}
		if (OpenFlag)
		{
			FK_DisConnect(nCommHandleIndex);
			OpenFlag = false;
		}
		return result;
	}

	private static DataTable funcGetGeneralLogData()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("EmployeeNo", typeof(string));
		dataTable.Columns.Add("IsLogout", typeof(bool));
		dataTable.Columns.Add("DateTime", typeof(DateTime));
		dataTable.Columns.Add("Date", typeof(DateTime));
		uint apnEnrollNumber = 0u;
		int apnVerifyMode = 0;
		int apnInOutMode = 0;
		DateTime apnDateTime = DateTime.MinValue;
		if (FK_EnableDevice(nCommHandleIndex, 0) == 0)
		{
			MessageBox.Show("No Device");
			return null;
		}
		int num = FK_LoadGeneralLogData(nCommHandleIndex, 0);
		if (num != 1)
		{
			MessageBox.Show(ReturnResultPrint(num));
		}
		else
		{
			int num2 = 1;
			if (FK_GetLogDataIsSupportStringID(nCommHandleIndex) == 1)
			{
				while (true)
				{
					string apnEnrollNumber2 = new string(' ', 16);
					num = FK_GetGeneralLogData_StringID(nCommHandleIndex, ref apnEnrollNumber2, ref apnVerifyMode, ref apnInOutMode, ref apnDateTime);
					switch (num)
					{
					case -7:
						num = 1;
						break;
					case 1:
						goto IL_0152;
					}
					break;
					IL_0152:
					apnEnrollNumber2 = apnEnrollNumber2.Trim();
					dataTable.Rows.Add(num2, apnEnrollNumber2, apnInOutMode, apnDateTime, apnDateTime.Date);
					num2++;
					apnEnrollNumber2 = null;
					bool flag = true;
				}
			}
			else
			{
				while (true)
				{
					num = FK_GetGeneralLogData(nCommHandleIndex, ref apnEnrollNumber, ref apnVerifyMode, ref apnInOutMode, ref apnDateTime);
					switch (num)
					{
					case -7:
						num = 1;
						break;
					case 1:
						goto IL_01eb;
					}
					break;
					IL_01eb:
					dataTable.Rows.Add(num2, apnEnrollNumber, apnInOutMode, apnDateTime, apnDateTime.Date);
					num2++;
					bool flag2 = true;
				}
			}
			if (num == 1)
			{
				MessageBox.Show("ReadGeneralLogData OK");
			}
			else
			{
				MessageBox.Show(ReturnResultPrint(num));
			}
		}
		FK_EnableDevice(nCommHandleIndex, 1);
		return dataTable;
	}

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern int FK_ConnectNet(int anMachineNo, string astrIpAddress, int anNetPort, int anTimeOut, int anProtocolType, int anNetPassword, int anLicense);

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern void FK_DisConnect(int anHandleIndex);

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern int FK_EnableDevice(int anHandleIndex, byte anEnableFlag);

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern int FK_LoadGeneralLogData(int anHandleIndex, int anReadMark);

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern int FK_GetGeneralLogData(int anHandleIndex, ref uint apnEnrollNumber, ref int apnVerifyMode, ref int apnInOutMode, ref DateTime apnDateTime);

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern int FK_GetLogDataIsSupportStringID(int anHandleIndex);

	[DllImport("FK623Attend.dll", CharSet = CharSet.Ansi)]
	public static extern int FK_GetGeneralLogData_StringID(int anHandleIndex, [MarshalAs(UnmanagedType.LPStr)] ref string apnEnrollNumber, ref int apnVerifyMode, ref int apnInOutMode, ref DateTime apnDateTime);

	public static string ReturnResultPrint(long anResultCode)
	{
		long num = anResultCode - -15;
		if ((ulong)num <= 16uL)
		{
			switch ((int)num)
			{
			case 16:
				return "Successful!";
			case 15:
				return "No support";
			case 14:
				return "Unknown error";
			case 13:
				return "No Open Device";
			case 12:
				return "Write Error";
			case 11:
				return "Read Error";
			case 10:
				return "Parameter Error";
			case 9:
				return "execution of command failed";
			case 8:
				return "End of data";
			case 7:
				return "Nonexistence data";
			case 6:
				return "Memory Allocating Error";
			case 5:
				return "License Error";
			case 4:
				return "full enrolldata & can`t put enrolldata";
			case 3:
				return "this ID is already  existed.";
			case 1:
				return "full manager & can`t put manager.";
			case 0:
				return "mistake fp data version.";
			}
		}
		return "Unknown error";
	}
}
