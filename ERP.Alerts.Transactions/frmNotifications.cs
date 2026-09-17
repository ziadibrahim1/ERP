using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.Alerts;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;

namespace ERP.Alerts.Transactions;

public class frmNotifications : frmBase
{
	private bool IsMinimized = false;

	private int dataDepthInMinutes = 30;

	private DataTable dtAlerts;

	private DataTable dtNewAlerts;

	private DataRow drCurrentAlert;

	private SoundPlayer player;

	private IContainer components = null;

	private Timer TimerAlert;

	private Timer TimerLoadForms;

	private Timer TimerLoadReports;

	private Panel pnlAlert;

	private Button btnHide;

	private Button btnMark;

	private Button btnClose;

	private Button btnView;

	private TextBox txtAlert;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = base.CreateParams;
			createParams.ExStyle |= 128;
			return createParams;
		}
	}

	public frmNotifications(int AlertInterval, int FormsAlertsGetInterval, int DataDepthInMinutes, int ReportsAlertsGetInterval = 1800000)
	{
		InitializeComponent();
		dataDepthInMinutes = DataDepthInMinutes;
		txtAlert.TextAlign = (GlobalVariables.IsArabic ? HorizontalAlignment.Right : HorizontalAlignment.Left);
		TimerAlert.Interval = AlertInterval;
		TimerLoadForms.Interval = FormsAlertsGetInterval;
		TimerLoadReports.Interval = ReportsAlertsGetInterval;
	}

	private void frmNotifications_Load(object sender, EventArgs e)
	{
		base.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - base.Width, Screen.PrimaryScreen.WorkingArea.Bottom - base.Height);
		TimerAlert.Start();
		TimerLoadForms.Start();
		TimerLoadReports.Start();
		dtAlerts = new DataTable();
		if (GetNewAlerts())
		{
			player = new SoundPlayer(Resources.ShipBell);
			player.Play();
			Animator.Animate(this, Animator.Effect.Blend, Animator.EffectDirection.HOR_NEGATIVE, Animator.ControlVisibility.Activate, 500);
			LoadNextAlert(1, 0, 0);
		}
		else
		{
			BeginInvoke((MethodInvoker)delegate
			{
				Hide();
			});
		}
	}

	private void txtAlert_MouseLeave(object sender, EventArgs e)
	{
		TimerAlert.Start();
	}

	private void txtAlert_MouseEnter(object sender, EventArgs e)
	{
		TimerAlert.Stop();
	}

	private bool GetNewAlerts()
	{
		try
		{
			dtNewAlerts = UserFormsAlerts.SelectUnionAlertsByUser(GlobalVariables.UserID, "0", "0", "0", dataDepthInMinutes.ToString(), IsFromServer: false);
			TimerAlert.Stop();
			if (dtAlerts.Rows.Count < 1)
			{
				dtAlerts = dtNewAlerts.Clone();
			}
			foreach (DataRow row in dtNewAlerts.Rows)
			{
				if (!Convert.ToBoolean(row["AlertType"]))
				{
					if (dtAlerts.Select("UserFormsAlertsID = " + row["UserFormsAlertsID"].ToString()).Length < 1)
					{
						dtAlerts.ImportRow(row);
					}
				}
				else if (dtAlerts.Select("UserReportAlertID = " + row["UserReportAlertID"].ToString()).Length < 1)
				{
					dtAlerts.ImportRow(row);
				}
			}
		}
		catch
		{
		}
		if (!IsMinimized && dtAlerts.Rows.Count > 0)
		{
			TimerAlert.Start();
		}
		return dtNewAlerts.Rows.Count > 0 || dtAlerts.Rows.Count > 0;
	}

	private void TimerLoadForms_Tick(object sender, EventArgs e)
	{
		if (GetNewAlerts() && !base.Visible)
		{
			Animator.Animate(this, Animator.Effect.Blend, Animator.EffectDirection.HOR_NEGATIVE, Animator.ControlVisibility.Activate, 500);
			LoadNextAlert(1, 0, 0);
		}
	}

	private void TimerLoadReports_Tick(object sender, EventArgs e)
	{
		try
		{
			UserReportsAlerts.ProcessReportsAlerts();
		}
		catch
		{
		}
	}

	private void TimerAlert_Tick(object sender, EventArgs e)
	{
		if (!IsMinimized)
		{
			LoadNextAlert(1, 0, 0);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		LoadNextAlert(1, 0, 0);
	}

	private void btnView_Click(object sender, EventArgs e)
	{
		try
		{
			TimerAlert.Stop();
			if (!Convert.ToBoolean(drCurrentAlert["AlertType"]))
			{
				GlobalFunctions.OpenForm(drCurrentAlert["FormFullName"].ToString(), Convert.ToInt32(drCurrentAlert["RowID"]), 0, 0);
			}
			else
			{
				string text = (GlobalVariables.IsArabic ? drCurrentAlert["RepAr"].ToString() : drCurrentAlert["RepEn"].ToString());
				try
				{
					GlobalVariables.ReportDocument = new ReportDocument();
					GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + text);
					frmReporViwer frmReporViwer2 = new frmReporViwer();
					GlobalVariables.ReportDocument.SetParameterValue("@FromDate", (DateTime)drCurrentAlert["WorkingDateFrom"]);
					GlobalVariables.ReportDocument.SetParameterValue("@ToDate", (DateTime)drCurrentAlert["WorkingDateTo"]);
					frmReporViwer2.ShowDialog();
					frmReporViwer2 = null;
					GlobalVariables.ReportDocument = null;
				}
				catch (Exception ex)
				{
					GlobalVariables.InformationMB.Show("تأكد من إسم ومسار التقرير\n" + ex.Message, "Check the report name and path\n" + ex.Message);
				}
			}
		}
		catch
		{
		}
		LoadNextAlert(1, 0, 1);
	}

	private void btnMark_Click(object sender, EventArgs e)
	{
		LoadNextAlert(1, 1, 0);
	}

	private void btnHide_Click(object sender, EventArgs e)
	{
		if (!IsMinimized)
		{
			TimerAlert.Stop();
			Animator.Animate(this, Animator.Effect.Slide, Animator.EffectDirection.HOR_POSITIVE, Animator.ControlVisibility.Hide, 500);
			base.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - btnHide.Width, Screen.PrimaryScreen.WorkingArea.Bottom - base.Height);
			btnHide.BackgroundImage = Resources.BarLeft;
			base.Visible = true;
			IsMinimized = true;
		}
		else
		{
			base.Visible = false;
			base.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - base.Width, Screen.PrimaryScreen.WorkingArea.Bottom - base.Height);
			btnHide.BackgroundImage = Resources.BarRight;
			Animator.Animate(this, Animator.Effect.Slide, Animator.EffectDirection.HOR_NEGATIVE, Animator.ControlVisibility.Activate, 500);
			IsMinimized = false;
			TimerAlert.Start();
		}
	}

	private bool LoadNextAlert(int Disp, int Mark, int Open)
	{
		TimerAlert.Stop();
		if (dtAlerts.Rows.Count > 0)
		{
			Animator.Animate(pnlAlert, Animator.Effect.Slide, Animator.EffectDirection.VER_NEGATIVE, Animator.ControlVisibility.Hide, 300);
			drCurrentAlert = dtAlerts.NewRow();
			drCurrentAlert.ItemArray = dtAlerts.Rows[0].ItemArray;
			dtAlerts.Rows.RemoveAt(0);
			txtAlert.Text = GetAlertText(drCurrentAlert);
			if (!Convert.ToBoolean(drCurrentAlert["AlertType"]))
			{
				UserFormsAlerts.Insert_Update(drCurrentAlert["UserFormsAlertsID"].ToString(), Disp.ToString(), Open.ToString(), Mark.ToString(), "0", IsFromServer: false);
			}
			else
			{
				UserReportsAlerts.Insert_Update(drCurrentAlert["UserReportAlertID"].ToString(), Disp.ToString(), Open.ToString(), Mark.ToString(), "0", IsFromServer: false);
			}
			Animator.Animate(pnlAlert, Animator.Effect.Slide, Animator.EffectDirection.VER_NEGATIVE, Animator.ControlVisibility.Activate, 600);
			TimerAlert.Start();
			return true;
		}
		Animator.Animate(this, Animator.Effect.Blend, Animator.EffectDirection.HOR_NEGATIVE, Animator.ControlVisibility.Hide, 500);
		base.Visible = false;
		txtAlert.Text = "";
		return false;
	}

	private string GetAlertText(DataRow r)
	{
		if (!Convert.ToBoolean(r["AlertType"]))
		{
			return GetAlertText_Form(r);
		}
		return GetAlertText_Report(r);
	}

	private string GetAlertText_Report(DataRow r)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (GlobalVariables.IsArabic)
		{
			stringBuilder.Append(r["ReportNameAr"].ToString()).AppendLine();
			stringBuilder.Append(" يحتوي على عدد ").Append(Convert.ToInt32(r["DataResult"]).ToString()).Append(" من السجلات في الفتره ")
				.AppendLine();
			stringBuilder.Append(" من ").Append(((DateTime)r["WorkingDateFrom"]).ToString("dd'/'MM'/'yyyy")).AppendLine();
			stringBuilder.Append(" إلي ").Append(((DateTime)r["WorkingDateTo"]).ToString("dd'/'MM'/'yyyy"));
		}
		else
		{
			stringBuilder.Append(r["ReportNameEn"].ToString()).Append(" Report contains ").Append(Convert.ToInt32(r["DataResult"]).ToString())
				.Append(" new records ")
				.AppendLine();
			stringBuilder.Append("over the period from ").Append(((DateTime)r["WorkingDateFrom"]).ToString("dd'/'MM'/'yyyy")).AppendLine();
			stringBuilder.Append("To ").Append(((DateTime)r["WorkingDateTo"]).ToString("dd'/'MM'/'yyyy"));
		}
		return stringBuilder.ToString();
	}

	private string GetAlertText_Form(DataRow r)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (GlobalVariables.IsArabic)
		{
			stringBuilder.Append(r["UserNameAr"].ToString()).AppendLine();
			stringBuilder.Append(" قام");
			string text = r["TransType"].ToString();
			if (text.Equals("I"))
			{
				stringBuilder.Append(" بإضافة");
			}
			else if (text.Equals("U"))
			{
				stringBuilder.Append(" بتعديل");
			}
			else if (text.Equals("D"))
			{
				stringBuilder.Append(" بحذف");
			}
			else if (text.Equals("A"))
			{
				stringBuilder.Append(" بإعتماد");
			}
			stringBuilder.Append(" سجل في شاشة ").Append(r["FormNameAr"].ToString()).Append(" برقم ")
				.Append(r["RowNo"].ToString())
				.AppendLine();
			stringBuilder.Append(" بتاريخ ").Append(((DateTime)r["TransDate"]).ToString("dd'/'MM'/'yyyy HH:mm:ss"));
		}
		else
		{
			stringBuilder.Append("User : ").Append(r["UserNameEn"].ToString()).Append(" has been");
			string text2 = r["TransType"].ToString();
			if (text2.Equals("I"))
			{
				stringBuilder.Append(" Inserted");
			}
			else if (text2.Equals("U"))
			{
				stringBuilder.Append(" Updated");
			}
			else if (text2.Equals("D"))
			{
				stringBuilder.Append(" Deleted");
			}
			else if (text2.Equals("A"))
			{
				stringBuilder.Append(" Approved");
			}
			stringBuilder.Append(" a record in Form [").Append(r["FormNameEn"].ToString()).Append("]")
				.Append(" with number [");
			stringBuilder.Append(r["RowNo"].ToString()).Append("]").AppendLine();
			stringBuilder.Append("On Date : ").Append(((DateTime)r["TransDate"]).ToString("dd'/'MM'/'yyyy HH:mm:ss"));
		}
		return stringBuilder.ToString();
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
		this.components = new System.ComponentModel.Container();
		this.btnHide = new System.Windows.Forms.Button();
		this.pnlAlert = new System.Windows.Forms.Panel();
		this.btnClose = new System.Windows.Forms.Button();
		this.btnMark = new System.Windows.Forms.Button();
		this.btnView = new System.Windows.Forms.Button();
		this.txtAlert = new System.Windows.Forms.TextBox();
		this.TimerAlert = new System.Windows.Forms.Timer(this.components);
		this.TimerLoadForms = new System.Windows.Forms.Timer(this.components);
		this.TimerLoadReports = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		this.pnlAlert.SuspendLayout();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(361, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 78);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(359, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 78);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(359, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 76);
		this.btnHide.BackgroundImage = ERP.Properties.Resources.BarRight;
		this.btnHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnHide.FlatAppearance.BorderSize = 0;
		this.btnHide.Location = new System.Drawing.Point(2, 4);
		this.btnHide.Name = "btnHide";
		this.btnHide.Size = new System.Drawing.Size(20, 71);
		this.btnHide.TabIndex = 8;
		this.btnHide.Click += new System.EventHandler(btnHide_Click);
		this.pnlAlert.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.pnlAlert.Controls.Add(this.btnClose);
		this.pnlAlert.Controls.Add(this.btnMark);
		this.pnlAlert.Controls.Add(this.btnView);
		this.pnlAlert.Controls.Add(this.txtAlert);
		this.pnlAlert.Location = new System.Drawing.Point(22, 5);
		this.pnlAlert.Name = "pnlAlert";
		this.pnlAlert.Size = new System.Drawing.Size(333, 70);
		this.pnlAlert.TabIndex = 9;
		this.btnClose.BackgroundImage = ERP.Properties.Resources.Cancel;
		this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnClose.FlatAppearance.BorderSize = 0;
		this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnClose.Location = new System.Drawing.Point(0, 0);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(27, 23);
		this.btnClose.TabIndex = 0;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.btnMark.BackgroundImage = ERP.Properties.Resources.TrueSign;
		this.btnMark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnMark.FlatAppearance.BorderSize = 0;
		this.btnMark.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnMark.Location = new System.Drawing.Point(0, 22);
		this.btnMark.Name = "btnMark";
		this.btnMark.Size = new System.Drawing.Size(27, 23);
		this.btnMark.TabIndex = 1;
		this.btnMark.Click += new System.EventHandler(btnMark_Click);
		this.btnView.BackgroundImage = ERP.Properties.Resources.search;
		this.btnView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
		this.btnView.FlatAppearance.BorderSize = 0;
		this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnView.Location = new System.Drawing.Point(0, 44);
		this.btnView.Name = "btnView";
		this.btnView.Size = new System.Drawing.Size(27, 23);
		this.btnView.TabIndex = 2;
		this.btnView.Click += new System.EventHandler(btnView_Click);
		this.txtAlert.BackColor = this.pnlAlert.BackColor;
		this.txtAlert.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.txtAlert.Cursor = System.Windows.Forms.Cursors.Hand;
		this.txtAlert.Location = new System.Drawing.Point(29, 1);
		this.txtAlert.Multiline = true;
		this.txtAlert.Name = "txtAlert";
		this.txtAlert.ReadOnly = true;
		this.txtAlert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.txtAlert.Size = new System.Drawing.Size(299, 65);
		this.txtAlert.TabIndex = 3;
		this.txtAlert.MouseEnter += new System.EventHandler(txtAlert_MouseEnter);
		this.txtAlert.MouseLeave += new System.EventHandler(txtAlert_MouseLeave);
		this.TimerAlert.Tick += new System.EventHandler(TimerAlert_Tick);
		this.TimerLoadForms.Tick += new System.EventHandler(TimerLoadForms_Tick);
		this.TimerLoadReports.Tick += new System.EventHandler(TimerLoadReports_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(361, 80);
		base.Controls.Add(this.btnHide);
		base.Controls.Add(this.pnlAlert);
		base.Name = "frmNotifications";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Text = "frmNotifications";
		base.TopMost = true;
		base.Load += new System.EventHandler(frmNotifications_Load);
		base.Controls.SetChildIndex(this.pnlAlert, 0);
		base.Controls.SetChildIndex(this.btnHide, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		this.pnlAlert.ResumeLayout(false);
		this.pnlAlert.PerformLayout();
		base.ResumeLayout(false);
	}
}
