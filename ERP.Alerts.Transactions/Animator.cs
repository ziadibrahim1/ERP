using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ERP.Alerts.Transactions;

public static class Animator
{
	public enum Effect
	{
		Roll,
		Slide,
		Center,
		Blend
	}

	public enum EffectDirection
	{
		HOR_POSITIVE,
		HOR_NEGATIVE,
		VER_POSITIVE,
		VER_NEGATIVE
	}

	public enum ControlVisibility
	{
		NoAction,
		Hide,
		Activate
	}

	private static int[] EffectFlages = new int[4] { 0, 262144, 16, 524288 };

	private static int[] DirectionFlages = new int[4] { 1, 2, 4, 8 };

	private static int[] VisibilityFlages = new int[3] { 0, 65536, 131072 };

	public static void Animate(Control control, Effect effect, EffectDirection direction, ControlVisibility visibilityAction, int msec)
	{
		int num = 0;
		num |= EffectFlages[(int)effect];
		num |= DirectionFlages[(int)direction];
		num |= VisibilityFlages[(int)visibilityAction];
		try
		{
			AnimateWindow(control.Handle, msec, num);
		}
		catch
		{
		}
	}

	[DllImport("user32.dll")]
	private static extern bool AnimateWindow(IntPtr handle, int msec, int flags);
}
