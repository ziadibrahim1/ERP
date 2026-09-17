using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.EInvoices;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.EInvoices.MasterData;

public class frmSettings : frmBase
{
	private DataTable dtEINVSetting;

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraButton btnSave;

	private OpenFileDialog ofdPicture;

	private UltraCheckEditor chkSendingInvoicesByCurrentDate;

	private UltraCheckEditor chkSendingItemsByItemsTypes;

	public frmSettings()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		((Control)(object)chkSendingItemsByItemsTypes).Enabled = false;
		FillData();
	}

	private void FillData()
	{
		dtEINVSetting = Settings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtEINVSetting.Rows.Count > 0)
		{
			((UltraToggleEditorBase)chkSendingInvoicesByCurrentDate).Checked = Convert.ToBoolean(dtEINVSetting.Rows[0]["SendingInvoicesByCurrentDate"]);
			((UltraToggleEditorBase)chkSendingItemsByItemsTypes).Checked = Convert.ToBoolean(dtEINVSetting.Rows[0]["SendingItemsByItemsTypes"]);
		}
	}

	private bool ValidateData()
	{
		return true;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			int num = Settings.Insert_Update((dtEINVSetting.Rows.Count > 0) ? dtEINVSetting.Rows[0]["SettingID"].ToString() : "-1", ((UltraToggleEditorBase)chkSendingInvoicesByCurrentDate).Checked ? "1" : "0", ((UltraToggleEditorBase)chkSendingItemsByItemsTypes).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.EInvoices.MasterData.frmSettings));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.chkSendingInvoicesByCurrentDate = new UltraCheckEditor();
		this.chkSendingItemsByItemsTypes = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSendingInvoicesByCurrentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSendingItemsByItemsTypes).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.chkSendingInvoicesByCurrentDate, "chkSendingInvoicesByCurrentDate");
		((System.Windows.Forms.Control)(object)this.chkSendingInvoicesByCurrentDate).Name = "chkSendingInvoicesByCurrentDate";
		resources.ApplyResources(this.chkSendingItemsByItemsTypes, "chkSendingItemsByItemsTypes");
		((System.Windows.Forms.Control)(object)this.chkSendingItemsByItemsTypes).Name = "chkSendingItemsByItemsTypes";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSendingItemsByItemsTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSendingInvoicesByCurrentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmSettings";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSendingInvoicesByCurrentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSendingItemsByItemsTypes, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSendingInvoicesByCurrentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSendingItemsByItemsTypes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
