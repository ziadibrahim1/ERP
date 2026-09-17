using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.SMS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SMS.MasterData;

public class frmSMSSettings : frmBase
{
	private DataTable dtSMSSetting;

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraButton btnSave;

	private OpenFileDialog ofdPicture;

	private UltraLabel lblUserName;

	private UltraTextEditor txtUserName;

	private UltraLabel lblSenderID;

	private UltraTextEditor txtSenderID;

	private UltraLabel lblUserPassword;

	private UltraTextEditor txtUserPassword;

	public frmSMSSettings()
	{
		InitializeComponent();
		FillData();
	}

	private void FillData()
	{
		dtSMSSetting = Settings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtSMSSetting.Rows.Count > 0)
		{
			((Control)(object)txtUserName).Text = dtSMSSetting.Rows[0]["UserName"].ToString();
			((Control)(object)txtUserPassword).Text = dtSMSSetting.Rows[0]["UserPassword"].ToString();
			((Control)(object)txtSenderID).Text = dtSMSSetting.Rows[0]["SenderID"].ToString();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtUserName).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم المستخدم ", " Please Enter User Name ");
			((TextEditorControlBase)txtUserName).Focus();
		}
		else if (((Control)(object)txtUserPassword).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كلمة المرور", " Please Enter User Password");
			((TextEditorControlBase)txtUserPassword).Focus();
		}
		else if (((Control)(object)txtSenderID).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الراسل ", " Please Enter SenderID");
			((TextEditorControlBase)txtSenderID).Focus();
		}
		else
		{
			int num = Settings.Insert_Update((dtSMSSetting.Rows.Count > 0) ? dtSMSSetting.Rows[0]["SettingID"].ToString() : "-1", (((Control)(object)txtUserName).Text == "") ? "Null" : ((Control)(object)txtUserName).Text, (((Control)(object)txtUserPassword).Text == "") ? "Null" : ((Control)(object)txtUserPassword).Text, (((Control)(object)txtSenderID).Text == "") ? "Null" : ((Control)(object)txtSenderID).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			FillData();
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
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
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SMS.MasterData.frmSMSSettings));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.lblUserName = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		this.lblSenderID = new UltraLabel();
		this.txtSenderID = new UltraTextEditor();
		this.lblUserPassword = new UltraLabel();
		this.txtUserPassword = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSenderID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserPassword).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance4");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val2).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val2, "appearance5");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val3, "appearance6");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.lblUserName, "lblUserName");
		this.lblUserName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		((ControlBase)this.lblUserName).WrapText = false;
		resources.ApplyResources(this.txtUserName, "txtUserName");
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		resources.ApplyResources(this.lblSenderID, "lblSenderID");
		this.lblSenderID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSenderID).Name = "lblSenderID";
		((ControlBase)this.lblSenderID).WrapText = false;
		resources.ApplyResources(this.txtSenderID, "txtSenderID");
		((System.Windows.Forms.Control)(object)this.txtSenderID).Name = "txtSenderID";
		resources.ApplyResources(this.lblUserPassword, "lblUserPassword");
		this.lblUserPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUserPassword).Name = "lblUserPassword";
		((ControlBase)this.lblUserPassword).WrapText = false;
		resources.ApplyResources(this.txtUserPassword, "txtUserPassword");
		((System.Windows.Forms.Control)(object)this.txtUserPassword).Name = "txtUserPassword";
		this.txtUserPassword.PasswordChar = '*';
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSenderID);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSenderID);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmSMSSettings";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSenderID, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSenderID, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSenderID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserPassword).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
