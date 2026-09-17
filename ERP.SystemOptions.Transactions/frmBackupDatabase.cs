using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.Transactions;

public class frmBackupDatabase : frmBase
{
	private IContainer components = null;

	private UltraTextEditor txtFileName;

	private UltraLabel lblFileName;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtBackupPath;

	private UltraLabel lblBackupPath;

	private UltraButton btnReportPath;

	private FolderBrowserDialog fbdPath;

	public frmBackupDatabase()
	{
		InitializeComponent();
		((Control)(object)txtBackupPath).Text = GlobalFunctions.GetDefault("BackupPath");
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtFileName).Text.Trim().Contains(" ") || ((Control)(object)txtFileName).Text.Trim().Contains("'"))
		{
			GlobalVariables.InformationMB.Show("برجاء ادخال اسم ملف صحيح", "Please Enter Valied File Name");
		}
		else
		{
			Main.ExecuteNonQuery("BACKUP DATABASE [" + GlobalVariables.DatabaseName + "] TO  DISK = N'" + GlobalFunctions.GetDefault("BackupPath") + "\\" + ((Control)(object)txtFileName).Text.Trim() + ".bak' WITH  INIT ,  NOUNLOAD ,  NAME = N'" + GlobalVariables.DatabaseName + " backup',  NOSKIP ,  STATS = 10,  NOFORMAT ");
			GlobalVariables.InformationMB.Show("تم عمل النسخة الاحتياطية", "Database Backup completed successfully");
		}
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnReportPath_Click(object sender, EventArgs e)
	{
		if (fbdPath.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtBackupPath).Text = fbdPath.SelectedPath;
			GlobalFunctions.SetDefault("BackupPath", ((Control)(object)txtBackupPath).Text);
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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.Transactions.frmBackupDatabase));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		this.txtFileName = new UltraTextEditor();
		this.lblFileName = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtBackupPath = new UltraTextEditor();
		this.lblBackupPath = new UltraLabel();
		this.btnReportPath = new UltraButton();
		this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFileName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBackupPath).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtFileName, "txtFileName");
		((System.Windows.Forms.Control)(object)this.txtFileName).Name = "txtFileName";
		resources.ApplyResources(this.lblFileName, "lblFileName");
		this.lblFileName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFileName).Name = "lblFileName";
		((ControlBase)this.lblFileName).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtBackupPath, "txtBackupPath");
		((System.Windows.Forms.Control)(object)this.txtBackupPath).Name = "txtBackupPath";
		resources.ApplyResources(this.lblBackupPath, "lblBackupPath");
		this.lblBackupPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBackupPath).Name = "lblBackupPath";
		((ControlBase)this.lblBackupPath).WrapText = false;
		resources.ApplyResources(this.btnReportPath, "btnReportPath");
		((System.Windows.Forms.Control)(object)this.btnReportPath).Name = "btnReportPath";
		((System.Windows.Forms.Control)(object)this.btnReportPath).Click += new System.EventHandler(btnReportPath_Click);
		resources.ApplyResources(this.fbdPath, "fbdPath");
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReportPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBackupPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFileName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBackupPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFileName);
		base.Name = "frmBackupDatabase";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFileName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBackupPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFileName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBackupPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReportPath, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFileName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBackupPath).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
