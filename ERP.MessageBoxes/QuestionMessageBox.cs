using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Company;
using ERP.Properties;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.MessageBoxes;

public class QuestionMessageBox : frmBase
{
	public bool isMouseDown = false;

	public int xLast;

	public int yLast;

	private string _workingMessage = string.Empty;

	private frmBase CallerForm;

	private IContainer components = null;

	private UltraButton btnYes;

	private UltraButton btnNo;

	private UltraLabel lblMessage;

	private UltraButton btnOpenTicket;

	public QuestionMessageBox()
	{
		CallerForm = frmMain2010.Curfrm;
		InitializeComponent();
		((Control)(object)btnOpenTicket).Visible = CallerForm != null;
	}

	public void Show(string MessageAr, string MessageEn)
	{
		GlobalVariables.MessageBoxResult = 'z';
		QuestionMessageBox questionMessageBox = new QuestionMessageBox();
		if (GlobalVariables.IsArabic)
		{
			if (MessageAr.Length > 100)
			{
				questionMessageBox.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
				questionMessageBox.Height = Screen.PrimaryScreen.WorkingArea.Height / 3;
			}
			((Control)(object)questionMessageBox.lblMessage).Text = MessageAr;
			questionMessageBox._workingMessage = MessageAr;
		}
		else
		{
			if (MessageEn.Length > 100)
			{
				questionMessageBox.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
				questionMessageBox.Height = Screen.PrimaryScreen.WorkingArea.Height / 3;
			}
			((Control)(object)questionMessageBox.lblMessage).Text = MessageEn;
			questionMessageBox._workingMessage = MessageEn;
		}
		questionMessageBox.ShowDialog(this);
		((Control)(object)questionMessageBox.btnYes).Focus();
	}

	private void btnYes_Click(object sender, EventArgs e)
	{
		GlobalVariables.MessageBoxResult = 'Y';
		Close();
	}

	private void btnNo_Click(object sender, EventArgs e)
	{
		GlobalVariables.MessageBoxResult = 'N';
		Close();
	}

	private void QuestionMessageBox_Load(object sender, EventArgs e)
	{
	}

	private void This_MouseDown(object sender, MouseEventArgs e)
	{
	}

	private void This_MouseUp(object sender, MouseEventArgs e)
	{
	}

	private void This_MouseMove(object sender, MouseEventArgs e)
	{
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		if (CallerForm != null)
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: true, isFormQst: false, CallerForm.Name, _workingMessage, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			Close();
			frmSupportingTickets2.ShowDialog(this);
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MessageBoxes.QuestionMessageBox));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.btnNo = new UltraButton();
		this.btnYes = new UltraButton();
		this.lblMessage = new UltraLabel();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnNo, "btnNo");
		((AppearanceBase)val).Image = ERP.Properties.Resources.Cancel;
		((ControlBase)this.btnNo).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnNo).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnNo).Name = "btnNo";
		((System.Windows.Forms.Control)(object)this.btnNo).Click += new System.EventHandler(btnNo_Click);
		resources.ApplyResources(this.btnYes, "btnYes");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.OK;
		((ControlBase)this.btnYes).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.btnYes).Name = "btnYes";
		((System.Windows.Forms.Control)(object)this.btnYes).Click += new System.EventHandler(btnYes_Click);
		resources.ApplyResources(this.lblMessage, "lblMessage");
		resources.ApplyResources(val3, "appearance3");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.lblMessage).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblMessage).Name = "lblMessage";
		((System.Windows.Forms.Control)(object)this.lblMessage).MouseDown += new System.Windows.Forms.MouseEventHandler(This_MouseDown);
		((System.Windows.Forms.Control)(object)this.lblMessage).MouseMove += new System.Windows.Forms.MouseEventHandler(This_MouseMove);
		((System.Windows.Forms.Control)(object)this.lblMessage).MouseUp += new System.Windows.Forms.MouseEventHandler(This_MouseUp);
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnYes;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnNo;
		resources.ApplyResources(this, "$this");
		base.ControlBox = false;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnYes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMessage);
		base.Name = "QuestionMessageBox";
		base.TopMost = true;
		base.Load += new System.EventHandler(QuestionMessageBox_Load);
		base.MouseDown += new System.Windows.Forms.MouseEventHandler(This_MouseDown);
		base.MouseMove += new System.Windows.Forms.MouseEventHandler(This_MouseMove);
		base.MouseUp += new System.Windows.Forms.MouseEventHandler(This_MouseUp);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnYes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
