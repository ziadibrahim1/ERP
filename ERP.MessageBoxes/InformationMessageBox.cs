using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Company;
using ERP.Properties;
using ERP.Ticketing;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.MessageBoxes;

public class InformationMessageBox : frmBase
{
	public bool isMouseDown = false;

	public int xLast;

	public int yLast;

	private string _workingMessage = string.Empty;

	private frmBase CallerForm;

	private IContainer components = null;

	private UltraButton btnOK;

	private RichTextBox lblMessage;

	private UltraButton btnOpenTicket;

	public InformationMessageBox()
	{
		CallerForm = frmMain2010.Curfrm;
		InitializeComponent();
		((Control)(object)btnOpenTicket).Visible = CallerForm != null;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void Show(string MessageAr, string MessageEn)
	{
		InformationMessageBox informationMessageBox = new InformationMessageBox();
		if (GlobalVariables.IsArabic)
		{
			if (MessageAr.Length > 100)
			{
				informationMessageBox.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
				informationMessageBox.Height = Screen.PrimaryScreen.WorkingArea.Height / 3;
			}
			informationMessageBox.lblMessage.Text = MessageAr;
			informationMessageBox._workingMessage = MessageAr;
		}
		else
		{
			if (MessageEn.Length > 100)
			{
				informationMessageBox.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
				informationMessageBox.Height = Screen.PrimaryScreen.WorkingArea.Height / 3;
			}
			informationMessageBox.lblMessage.Text = MessageEn;
			informationMessageBox._workingMessage = MessageEn;
			informationMessageBox.lblMessage.RightToLeft = RightToLeft.No;
		}
		informationMessageBox.StartPosition = FormStartPosition.CenterScreen;
		informationMessageBox.lblMessage.SelectAll();
		informationMessageBox.lblMessage.SelectionAlignment = HorizontalAlignment.Center;
		informationMessageBox.Activate();
		informationMessageBox.ShowDialog(this);
	}

	public void Show(string Message)
	{
		InformationMessageBox informationMessageBox = new InformationMessageBox();
		if (Message.Length > 100)
		{
			informationMessageBox.Width = Screen.PrimaryScreen.WorkingArea.Width / 2;
			informationMessageBox.Height = Screen.PrimaryScreen.WorkingArea.Height / 3;
		}
		informationMessageBox.lblMessage.Text = Message;
		informationMessageBox._workingMessage = Message;
		informationMessageBox.StartPosition = FormStartPosition.CenterScreen;
		informationMessageBox.lblMessage.SelectAll();
		informationMessageBox.lblMessage.SelectionAlignment = HorizontalAlignment.Center;
		informationMessageBox.Activate();
		informationMessageBox.ShowDialog(this);
	}

	private void InformationMessageBox_Load(object sender, EventArgs e)
	{
		((Control)(object)btnOK).Left = base.Width / 2 - ((Control)(object)btnOK).Width / 2;
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
			frmSupportingTickets2.ShowDialog();
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MessageBoxes.InformationMessageBox));
		Appearance val = new Appearance();
		this.btnOK = new UltraButton();
		this.lblMessage = new System.Windows.Forms.RichTextBox();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnOK, "btnOK");
		((AppearanceBase)val).Image = ERP.Properties.Resources.OK;
		((ControlBase)this.btnOK).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(btnOK_Click);
		resources.ApplyResources(this.lblMessage, "lblMessage");
		this.lblMessage.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
		this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(0, 0, 64);
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.ReadOnly = true;
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this, "$this");
		base.ControlBox = false;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add(this.lblMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Name = "InformationMessageBox";
		base.TopMost = true;
		base.Load += new System.EventHandler(InformationMessageBox_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		base.Controls.SetChildIndex(this.lblMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
