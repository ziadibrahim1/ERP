using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Microsoft.Win32;

namespace ERP.SystemOptions.GeneralOptions;

public class frmReportDefultPath : frmBase
{
	private RegistryKey RegKey;

	private IContainer components = null;

	private UltraLabel lblReportPath;

	private UltraComboEditor cboRepotsPath;

	private UltraLabel ultraLabel1;

	public UltraButton btnOK;

	private UltraLabel ultraLabel2;

	public UltraButton btnCancel;

	private UltraCheckEditor chkRememberCompany;

	public frmReportDefultPath()
	{
		InitializeComponent();
	}

	private void frmLoginBranch_Load(object sender, EventArgs e)
	{
		RegKey = Registry.CurrentUser.OpenSubKey(GlobalVariables.path, writable: true);
		string value = ((RegKey.GetValue("ReportDefault") == null) ? "PathReport" : RegKey.GetValue("ReportDefault").ToString());
		string text = GlobalFunctions.GetDefault("PathReport");
		string text2 = GlobalFunctions.GetDefault("ReportPathRemote");
		string text3 = GlobalFunctions.GetDefault("ReportPathLocal");
		if (text != "")
		{
			cboRepotsPath.Items.Add((object)"PathReport", text);
		}
		if (text2 != "")
		{
			cboRepotsPath.Items.Add((object)"ReportPathRemote", text2);
		}
		if (text3 != "")
		{
			cboRepotsPath.Items.Add((object)"ReportPathLocal", text3);
		}
		((TextEditorControlBase)cboRepotsPath).Value = value;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (cboRepotsPath.SelectedIndex > -1)
		{
			GlobalVariables.ReportsPath = GlobalFunctions.GetDefault(((TextEditorControlBase)cboRepotsPath).Value.ToString());
			if (((UltraToggleEditorBase)chkRememberCompany).Checked)
			{
				RegKey.SetValue("ReportDefault", ((TextEditorControlBase)cboRepotsPath).Value.ToString());
			}
			Close();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralOptions.frmReportDefultPath));
		this.lblReportPath = new UltraLabel();
		this.cboRepotsPath = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.btnOK = new UltraButton();
		this.ultraLabel2 = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.chkRememberCompany = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboRepotsPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberCompany).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblReportPath, "lblReportPath");
		this.lblReportPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportPath).Name = "lblReportPath";
		((ControlBase)this.lblReportPath).WrapText = false;
		resources.ApplyResources(this.cboRepotsPath, "cboRepotsPath");
		((System.Windows.Forms.Control)(object)this.cboRepotsPath).Name = "cboRepotsPath";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		resources.ApplyResources(this.btnOK, "btnOK");
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(btnOK_Click);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((UltraButtonBase)this.btnCancel).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.chkRememberCompany, "chkRememberCompany");
		((UltraToggleEditorBase)this.chkRememberCompany).CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		((System.Windows.Forms.Control)(object)this.chkRememberCompany).Name = "chkRememberCompany";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnOK;
		resources.ApplyResources(this, "$this");
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnCancel;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkRememberCompany);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboRepotsPath);
		base.Name = "frmReportDefultPath";
		base.Load += new System.EventHandler(frmLoginBranch_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboRepotsPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkRememberCompany, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboRepotsPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkRememberCompany).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
