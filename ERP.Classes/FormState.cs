using System.Drawing;
using System.Windows.Forms;

namespace ERP.Classes;

public class FormState
{
	private FormWindowState winState;

	private FormBorderStyle brdStyle;

	private bool topMost;

	private Rectangle bounds;

	private Form mdi;

	private Rectangle MdiBound;

	private bool IsMaximized = false;

	public void Maximize(Form targetForm, bool TopMost)
	{
		if (!IsMaximized)
		{
			IsMaximized = true;
			Save(targetForm);
			targetForm.MdiParent = null;
			targetForm.WindowState = FormWindowState.Maximized;
			targetForm.FormBorderStyle = FormBorderStyle.None;
			targetForm.TopMost = TopMost;
			WinApi.SetWinFullScreen(targetForm.Handle);
		}
	}

	public void Save(Form targetForm)
	{
		winState = targetForm.WindowState;
		brdStyle = targetForm.FormBorderStyle;
		topMost = targetForm.TopMost;
		bounds = targetForm.Bounds;
		MdiBound = targetForm.ParentForm.Bounds;
		mdi = targetForm.ParentForm;
	}

	public void Restore(Form targetForm)
	{
		targetForm.WindowState = FormWindowState.Normal;
		targetForm.FormBorderStyle = FormBorderStyle.None;
		IsMaximized = false;
		targetForm.MdiParent = mdi;
		targetForm.Parent = mdi.Controls.Find("UGBFill", searchAllChildren: true)[0];
		targetForm.Width = mdi.Controls.Find("UGBFill", searchAllChildren: true)[0].Width;
		targetForm.Height = mdi.Controls.Find("UGBFill", searchAllChildren: true)[0].Height;
		targetForm.BringToFront();
		targetForm.TopMost = topMost;
		targetForm.Bounds = bounds;
		mdi.Bounds = MdiBound;
	}
}
