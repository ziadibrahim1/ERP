using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Company;

public class frmMailPaword : frmBase
{
	private UltraLabel lblEMailPassword;

	protected internal Label lblTitle;

	private UltraButton BtnOk;

	private UltraTextEditor txtEMailPassword;

	public string Password = "";

	public frmMailPaword()
	{
		InitializeComponent();
	}

	private void InitializeComponent()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Company.frmMailPaword));
		this.lblEMailPassword = new UltraLabel();
		this.txtEMailPassword = new UltraTextEditor();
		this.lblTitle = new System.Windows.Forms.Label();
		this.BtnOk = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMailPassword).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		this.lblEMailPassword.AutoEllipsis = false;
		resources.ApplyResources(this.lblEMailPassword, "lblEMailPassword");
		((System.Windows.Forms.Control)(object)this.lblEMailPassword).Name = "lblEMailPassword";
		((ControlBase)this.lblEMailPassword).WrapText = false;
		resources.ApplyResources(this.txtEMailPassword, "txtEMailPassword");
		((System.Windows.Forms.Control)(object)this.txtEMailPassword).Name = "txtEMailPassword";
		this.txtEMailPassword.PasswordChar = '*';
		this.lblTitle.BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		this.lblTitle.ForeColor = System.Drawing.Color.Black;
		this.lblTitle.Name = "lblTitle";
		resources.ApplyResources(this.BtnOk, "BtnOk");
		((System.Windows.Forms.Control)(object)this.BtnOk).Name = "BtnOk";
		((System.Windows.Forms.Control)(object)this.BtnOk).Click += new System.EventHandler(BtnOk_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.BtnOk);
		base.Controls.Add(this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMailPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMailPassword);
		base.Name = "frmMailPaword";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMailPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMailPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex(this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.BtnOk, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMailPassword).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void BtnOk_Click(object sender, EventArgs e)
	{
		Password = ((Control)(object)txtEMailPassword).Text.Trim();
		Close();
	}
}
