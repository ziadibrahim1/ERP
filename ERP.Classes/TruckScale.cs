using System;
using System.IO.Ports;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Classes;

public class TruckScale
{
	private delegate void Closure();

	private SerialPort port = new SerialPort();

	private const int BaudRate = 9600;

	private string ReadWeight = "";

	private UltraTextEditor ScaleText = new UltraTextEditor();

	public TruckScale(UltraTextEditor TextToReadIn, string PortName)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		ScaleText = TextToReadIn;
		SetPort(PortName);
	}

	public TruckScale(UltraTextEditor TextToReadIn)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		ScaleText = TextToReadIn;
	}

	public void SetPort(string PortName)
	{
		if (port != null && port.IsOpen)
		{
			port.Close();
			port.Dispose();
		}
		port = new SerialPort(PortName, 9600, Parity.None, 8, StopBits.One);
		port.DataReceived += SerialPortOnDataReceived;
		port.Open();
	}

	private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs serialDataReceivedEventArgs)
	{
		if (((Control)(object)ScaleText).InvokeRequired)
		{
			((Control)(object)ScaleText).BeginInvoke((Delegate)(Closure)delegate
			{
				SerialPortOnDataReceived(sender, serialDataReceivedEventArgs);
			});
			return;
		}
		ReadWeight += port.ReadExisting();
		if (ReadWeight.Length > 20)
		{
			ReadWeight = ReadWeight.Substring(ReadWeight.Length - 20);
		}
		if (ReadWeight.IndexOf("K", 9) != -1)
		{
			int num = ReadWeight.IndexOf("K", 9);
			((Control)(object)ScaleText).Text = Convert.ToInt32(ReadWeight.Substring(num - 7, 7)).ToString();
		}
		else if (ReadWeight.IndexOf(".") != -1)
		{
			int num2 = ReadWeight.IndexOf(".") + 1;
			int num3 = ReadWeight.IndexOf(".", num2);
			if (num2 > 0 && num3 > num2)
			{
				((Control)(object)ScaleText).Text = ReadWeight.Substring(num2, num3 - num2);
			}
		}
		else
		{
			((Control)(object)ScaleText).Text = ReadWeight;
		}
	}
}
