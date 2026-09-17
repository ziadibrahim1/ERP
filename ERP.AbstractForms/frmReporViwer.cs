using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using ERP.Classes;
using ERP.Properties;
using ERP.Ticketing;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.AbstractForms;

public class frmReporViwer : frmBase
{
	private TableLogOnInfo info = new TableLogOnInfo();

	private ConnectionInfo con = new ConnectionInfo();

	public int ZoomFactor = 100;

	public frmBase frmParent;

	private IContainer components = null;

	internal CrystalReportViewer crvReportViewer;

	public UltraButton btnExit;

	private UltraButton btnPrint;

	public UltraButton btnOpenTicket;

	public frmReporViwer()
	{
		InitializeComponent();
		RightToLeft = (GlobalVariables.IsArabic ? RightToLeft.Yes : RightToLeft.No);
		((Control)(object)btnPrint).Visible = GlobalVariables.CanDirectPrint;
		crvReportViewer.ShowPrintButton = (GlobalVariables.CanPrintReport || GlobalVariables.CanPrint) && !GlobalVariables.CanDirectPrint;
		crvReportViewer.ShowExportButton = GlobalVariables.CanExport;
		crvReportViewer.RightToLeft = (GlobalVariables.IsArabic ? RightToLeft.Yes : RightToLeft.No);
		base.Height = Screen.PrimaryScreen.WorkingArea.Height;
		base.Width = Screen.PrimaryScreen.WorkingArea.Width;
		base.Location = new Point(0, 0);
		BringToFront();
	}

	public void ConfigureReport()
	{
		foreach (Table table in GlobalVariables.ReportDocument.Database.Tables)
		{
			SetLogOnInfo(table);
		}
		foreach (ReportDocument subreport in GlobalVariables.ReportDocument.Subreports)
		{
			foreach (Table table2 in subreport.Database.Tables)
			{
				SetLogOnInfo(table2);
			}
		}
		crvReportViewer.ReportSource = GlobalVariables.ReportDocument;
		crvReportViewer.Zoom(ZoomFactor);
	}

	private void frmReporViwer_Load(object sender, EventArgs e)
	{
		ZoomFactor = Convert.ToInt32(GlobalFunctions.GetDefault("ReportZoomFactor"));
		ConfigureReport();
	}

	private void SetLogOnInfo(Table t)
	{
		con = new ConnectionInfo();
		if (GlobalVariables.IsRepOnlineConn)
		{
			con.ServerName = GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString();
			con.DatabaseName = GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString();
			con.UserID = GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString();
			con.Password = GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString();
		}
		else
		{
			con.ServerName = GlobalVariables.Server;
			con.DatabaseName = GlobalVariables.DatabaseName;
			con.UserID = GlobalVariables.dbUserID;
			con.Password = GlobalVariables.dbPassword;
		}
		info = t.LogOnInfo;
		info.ConnectionInfo = con;
		t.ApplyLogOnInfo(info);
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		if (GlobalVariables.ReportDocument != null)
		{
			GlobalVariables.ReportDocument.Close();
		}
		((ReportDocument)crvReportViewer.ReportSource).Dispose();
		GlobalVariables.ReportDocument = null;
		crvReportViewer.ReportSource = null;
		crvReportViewer.Dispose();
		GC.Collect();
		Dispose();
	}

	private void crvReportViewer_Error(object source, ExceptionEventArgs e)
	{
		crvReportViewer.Error -= crvReportViewer_Error;
		e.Handled = true;
		GlobalVariables.InformationMB.Show("حدث خطأ في تحميل التقرير \r\n التفاصيل :-\r\n" + e.Exception.Message, "An error occured while Loading Report. \r\nDetails:-\r\n" + e.Exception.Message);
		crvReportViewer.Error -= crvReportViewer_Error;
		Close();
	}

	private void crvReportViewer_DoubleClickPage(object sender, PageMouseEventArgs e)
	{
		e.Handled = true;
		string groupNamePath = e.ObjectInfo.GroupNamePath;
		if (frmParent != null && groupNamePath != "")
		{
			frmParent.ReportDoubleClick(groupNamePath, this);
		}
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		if (GlobalVariables.POSPrinter != "")
		{
			((ReportDocument)crvReportViewer.ReportSource).PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			((ReportDocument)crvReportViewer.ReportSource).PrintToPrinter(1, collated: true, 0, 10000);
		}
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmReporViwer));
		Appearance val2 = new Appearance();
		this.crvReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		this.btnExit = new UltraButton();
		this.btnPrint = new UltraButton();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(703, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 511);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(701, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 511);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(701, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 509);
		this.crvReportViewer.ActiveViewIndex = -1;
		this.crvReportViewer.AutoScroll = true;
		this.crvReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.crvReportViewer.Cursor = System.Windows.Forms.Cursors.Default;
		this.crvReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
		this.crvReportViewer.ForeColor = System.Drawing.Color.Navy;
		this.crvReportViewer.Location = new System.Drawing.Point(2, 2);
		this.crvReportViewer.Margin = new System.Windows.Forms.Padding(0);
		this.crvReportViewer.Name = "crvReportViewer";
		this.crvReportViewer.ReuseParameterValuesOnRefresh = true;
		this.crvReportViewer.SelectionFormula = "";
		this.crvReportViewer.ShowCloseButton = false;
		this.crvReportViewer.ShowLogo = false;
		this.crvReportViewer.Size = new System.Drawing.Size(699, 509);
		this.crvReportViewer.TabIndex = 1;
		this.crvReportViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
		this.crvReportViewer.ViewTimeSelectionFormula = "";
		this.crvReportViewer.DoubleClickPage += new CrystalDecisions.Windows.Forms.PageMouseEventHandler(crvReportViewer_DoubleClickPage);
		this.crvReportViewer.Error += new CrystalDecisions.Windows.Forms.ExceptionEventHandler(crvReportViewer_Error);
		((System.Windows.Forms.Control)(object)this.btnExit).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((ControlBase)this.btnExit).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnExit).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnExit).ImageSize = new System.Drawing.Size(20, 20);
		((UltraButtonBase)this.btnExit).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnExit).Location = new System.Drawing.Point(671, 2);
		((System.Windows.Forms.Control)(object)this.btnExit).Name = "btnExit";
		((System.Windows.Forms.Control)(object)this.btnExit).Size = new System.Drawing.Size(30, 30);
		((System.Windows.Forms.Control)(object)this.btnExit).TabIndex = 81;
		((System.Windows.Forms.Control)(object)this.btnExit).Click += new System.EventHandler(btnExit_Click);
		((AppearanceBase)val2).Image = ERP.Properties.Resources.Print;
		((ControlBase)this.btnPrint).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.btnPrint).Location = new System.Drawing.Point(451, 5);
		((System.Windows.Forms.Control)(object)this.btnPrint).Name = "btnPrint";
		((System.Windows.Forms.Control)(object)this.btnPrint).Size = new System.Drawing.Size(28, 23);
		((System.Windows.Forms.Control)(object)this.btnPrint).TabIndex = 82;
		((System.Windows.Forms.Control)(object)this.btnPrint).Visible = false;
		((System.Windows.Forms.Control)(object)this.btnPrint).Click += new System.EventHandler(btnPrint_Click);
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		((UltraButtonBase)this.btnOpenTicket).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Location = new System.Drawing.Point(642, 2);
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Size = new System.Drawing.Size(30, 30);
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabIndex = 512;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Text = "T";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		base.ClientSize = new System.Drawing.Size(703, 513);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrint);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExit);
		base.Controls.Add(this.crvReportViewer);
		base.Name = "frmReporViwer";
		base.Load += new System.EventHandler(frmReporViwer_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex(this.crvReportViewer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
