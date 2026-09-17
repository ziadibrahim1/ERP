using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Privilege.Transactions;

public class frmChangePassword : frmBase
{
	private IContainer components = null;

	protected internal Label lblTitle;

	private UltraLabel lblOldPassword;

	private UltraLabel lblNewPassword1;

	private UltraLabel lblNewPassword2;

	private UltraTextEditor txtOldPassword;

	private UltraTextEditor txtNewPassword1;

	private UltraTextEditor txtNewPassword2;

	protected internal UltraButton btnOK;

	protected internal UltraButton btnClose;

	public frmChangePassword()
	{
		InitializeComponent();
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtOldPassword).Text != GlobalVariables.Password)
		{
			GlobalVariables.InformationMB.Show("كلمة المرور الحالية غير صحيحة", "Old Password is not Correct");
			((TextEditorControlBase)txtOldPassword).Clear();
			((Control)(object)txtOldPassword).Select();
		}
		else if (((Control)(object)txtNewPassword1).Text != ((Control)(object)txtNewPassword2).Text)
		{
			GlobalVariables.InformationMB.Show("برجاء التأكد من كلمة المرور الجديدة", "Please Check your new Password");
			((TextEditorControlBase)txtNewPassword1).Clear();
			((TextEditorControlBase)txtNewPassword2).Clear();
			((Control)(object)txtNewPassword1).Select();
		}
		else
		{
			Users.UpdatePassword(GlobalVariables.UserID, ((Control)(object)txtNewPassword1).Text, IsFromServer: true);
			GlobalVariables.Password = ((Control)(object)txtNewPassword1).Text.Trim();
			GlobalVariables.InformationMB.Show("تم تغيير كلمة المرور بنجاح", "Password changed successfully");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Privilege.Transactions.frmChangePassword));
		this.lblTitle = new System.Windows.Forms.Label();
		this.lblOldPassword = new UltraLabel();
		this.lblNewPassword1 = new UltraLabel();
		this.lblNewPassword2 = new UltraLabel();
		this.txtOldPassword = new UltraTextEditor();
		this.txtNewPassword1 = new UltraTextEditor();
		this.txtNewPassword2 = new UltraTextEditor();
		this.btnOK = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOldPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewPassword1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewPassword2).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		this.lblTitle.ForeColor = System.Drawing.Color.Black;
		this.lblTitle.Name = "lblTitle";
		resources.ApplyResources(this.lblOldPassword, "lblOldPassword");
		this.lblOldPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOldPassword).Name = "lblOldPassword";
		((ControlBase)this.lblOldPassword).WrapText = false;
		resources.ApplyResources(this.lblNewPassword1, "lblNewPassword1");
		this.lblNewPassword1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNewPassword1).Name = "lblNewPassword1";
		((ControlBase)this.lblNewPassword1).WrapText = false;
		resources.ApplyResources(this.lblNewPassword2, "lblNewPassword2");
		this.lblNewPassword2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNewPassword2).Name = "lblNewPassword2";
		((ControlBase)this.lblNewPassword2).WrapText = false;
		resources.ApplyResources(this.txtOldPassword, "txtOldPassword");
		((System.Windows.Forms.Control)(object)this.txtOldPassword).Name = "txtOldPassword";
		this.txtOldPassword.PasswordChar = '●';
		resources.ApplyResources(this.txtNewPassword1, "txtNewPassword1");
		((System.Windows.Forms.Control)(object)this.txtNewPassword1).Name = "txtNewPassword1";
		this.txtNewPassword1.PasswordChar = '●';
		resources.ApplyResources(this.txtNewPassword2, "txtNewPassword2");
		((System.Windows.Forms.Control)(object)this.txtNewPassword2).Name = "txtNewPassword2";
		this.txtNewPassword2.PasswordChar = '●';
		resources.ApplyResources(this.btnOK, "btnOK");
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(btnOK_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNewPassword2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNewPassword1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOldPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewPassword2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewPassword1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOldPassword);
		base.Controls.Add(this.lblTitle);
		base.Name = "frmChangePassword";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex(this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOldPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewPassword1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewPassword2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOldPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNewPassword1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNewPassword2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOldPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewPassword1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNewPassword2).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
