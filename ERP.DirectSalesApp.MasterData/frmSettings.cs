using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.DirectSalesApp;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.DirectSalesApp.MasterData;

public class frmSettings : frmBase
{
	private DataTable dtDirectSalesAppSettings;

	private DataTable dtTaxs;

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraButton btnSave;

	private OpenFileDialog ofdPicture;

	private NumericUpDown numSynchronizationPeriod;

	private UltraLabel ultraLabel8;

	private NumericUpDown numClientReturnPeriod;

	private UltraLabel ultraLabel9;

	private UltraLabel ultraLabel10;

	private UltraLabel lblClientReturnPeriod;

	private UltraComboEditor cboTax;

	private UltraCheckEditor chkAutoSynchronization;

	private UltraCheckEditor chkEnforceClientLocation;

	private UltraCheckEditor chkApplySalesTax;

	private UltraTextEditor txtCompanyMessage;

	private UltraLabel lblCompanyMessage;

	public UltraCheckEditor chkApplyCashBackWithDiscount;

	public UltraCheckEditor chkCloseSalesSafeZero;

	public frmSettings()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		FillData();
	}

	private void FillData()
	{
		dtDirectSalesAppSettings = SystemDirectSalesAppSettings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtDirectSalesAppSettings.Rows.Count > 0)
		{
			numClientReturnPeriod.Text = dtDirectSalesAppSettings.Rows[0]["ClientReturnPeriod"].ToString();
			numSynchronizationPeriod.Text = dtDirectSalesAppSettings.Rows[0]["SynchronizationPeriod"].ToString();
			((UltraToggleEditorBase)chkApplySalesTax).Checked = bool.Parse(dtDirectSalesAppSettings.Rows[0]["ApplySalesTax"].ToString());
			((UltraToggleEditorBase)chkApplyCashBackWithDiscount).Checked = bool.Parse(dtDirectSalesAppSettings.Rows[0]["ApplyCashBackWithDiscount"].ToString());
			((UltraToggleEditorBase)chkAutoSynchronization).Checked = bool.Parse(dtDirectSalesAppSettings.Rows[0]["AutoSynchronization"].ToString());
			((UltraToggleEditorBase)chkEnforceClientLocation).Checked = bool.Parse(dtDirectSalesAppSettings.Rows[0]["EnforceClientLocation"].ToString());
			((TextEditorControlBase)cboTax).Value = dtDirectSalesAppSettings.Rows[0]["TaxID"];
			((Control)(object)txtCompanyMessage).Text = dtDirectSalesAppSettings.Rows[0]["Message"].ToString();
			((UltraToggleEditorBase)chkCloseSalesSafeZero).Checked = bool.Parse(dtDirectSalesAppSettings.Rows[0]["CloseSalesSafeZero"].ToString());
		}
	}

	private bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkApplySalesTax).Checked && cboTax.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الضريبة ", " Please Select Sales Tax.");
			((TextEditorControlBase)cboTax).Focus();
			return false;
		}
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
			int num = SystemDirectSalesAppSettings.Insert_Update((dtDirectSalesAppSettings.Rows.Count > 0) ? dtDirectSalesAppSettings.Rows[0]["DirectSaleAppSettingID"].ToString() : "-1", ((UltraToggleEditorBase)chkEnforceClientLocation).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAutoSynchronization).Checked ? "1" : "0", numSynchronizationPeriod.Value.ToString(), ((UltraToggleEditorBase)chkApplySalesTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkApplyCashBackWithDiscount).Checked ? "1" : "0", (cboTax.SelectedIndex > -1) ? ((TextEditorControlBase)cboTax).Value.ToString() : "Null", numClientReturnPeriod.Value.ToString(), (dtDirectSalesAppSettings.Rows.Count <= 0) ? "0" : ((dtDirectSalesAppSettings.Rows[0]["LnsInvoiceType"] == DBNull.Value) ? "0" : dtDirectSalesAppSettings.Rows[0]["LnsInvoiceType"].ToString()), ((Control)(object)txtCompanyMessage).Text, ((UltraToggleEditorBase)chkCloseSalesSafeZero).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.DirectSalesApp.MasterData.frmSettings));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.numSynchronizationPeriod = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel8 = new UltraLabel();
		this.numClientReturnPeriod = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel9 = new UltraLabel();
		this.ultraLabel10 = new UltraLabel();
		this.lblClientReturnPeriod = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.chkAutoSynchronization = new UltraCheckEditor();
		this.chkEnforceClientLocation = new UltraCheckEditor();
		this.chkApplySalesTax = new UltraCheckEditor();
		this.txtCompanyMessage = new UltraTextEditor();
		this.lblCompanyMessage = new UltraLabel();
		this.chkApplyCashBackWithDiscount = new UltraCheckEditor();
		this.chkCloseSalesSafeZero = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numSynchronizationPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.numClientReturnPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAutoSynchronization).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceClientLocation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplySalesTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyCashBackWithDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCloseSalesSafeZero).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.numSynchronizationPeriod, "numSynchronizationPeriod");
		this.numSynchronizationPeriod.Maximum = new decimal(new int[4] { 3600, 0, 0, 0 });
		this.numSynchronizationPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numSynchronizationPeriod.Name = "numSynchronizationPeriod";
		this.numSynchronizationPeriod.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val4;
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.numClientReturnPeriod, "numClientReturnPeriod");
		this.numClientReturnPeriod.Maximum = new decimal(new int[4] { 3600, 0, 0, 0 });
		this.numClientReturnPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.numClientReturnPeriod.Name = "numClientReturnPeriod";
		this.numClientReturnPeriod.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val5;
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.ultraLabel10).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		((ControlBase)this.ultraLabel10).WrapText = false;
		resources.ApplyResources(this.lblClientReturnPeriod, "lblClientReturnPeriod");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblClientReturnPeriod).Appearance = (AppearanceBase)(object)val7;
		this.lblClientReturnPeriod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientReturnPeriod).Name = "lblClientReturnPeriod";
		((ControlBase)this.lblClientReturnPeriod).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		resources.ApplyResources(this.chkAutoSynchronization, "chkAutoSynchronization");
		((System.Windows.Forms.Control)(object)this.chkAutoSynchronization).Name = "chkAutoSynchronization";
		resources.ApplyResources(this.chkEnforceClientLocation, "chkEnforceClientLocation");
		((System.Windows.Forms.Control)(object)this.chkEnforceClientLocation).Name = "chkEnforceClientLocation";
		resources.ApplyResources(this.chkApplySalesTax, "chkApplySalesTax");
		((System.Windows.Forms.Control)(object)this.chkApplySalesTax).Name = "chkApplySalesTax";
		resources.ApplyResources(this.txtCompanyMessage, "txtCompanyMessage");
		((System.Windows.Forms.Control)(object)this.txtCompanyMessage).Name = "txtCompanyMessage";
		resources.ApplyResources(this.lblCompanyMessage, "lblCompanyMessage");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblCompanyMessage).Appearance = (AppearanceBase)(object)val8;
		this.lblCompanyMessage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyMessage).Name = "lblCompanyMessage";
		((ControlBase)this.lblCompanyMessage).WrapText = false;
		resources.ApplyResources(this.chkApplyCashBackWithDiscount, "chkApplyCashBackWithDiscount");
		((System.Windows.Forms.Control)(object)this.chkApplyCashBackWithDiscount).Name = "chkApplyCashBackWithDiscount";
		resources.ApplyResources(this.chkCloseSalesSafeZero, "chkCloseSalesSafeZero");
		((System.Windows.Forms.Control)(object)this.chkCloseSalesSafeZero).Name = "chkCloseSalesSafeZero";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCloseSalesSafeZero);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkApplySalesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAutoSynchronization);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkApplyCashBackWithDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceClientLocation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add(this.numSynchronizationPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add(this.numClientReturnPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientReturnPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmSettings";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientReturnPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel10, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel9, 0);
		base.Controls.SetChildIndex(this.numClientReturnPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex(this.numSynchronizationPeriod, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnforceClientLocation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkApplyCashBackWithDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAutoSynchronization, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkApplySalesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanyMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompanyMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCloseSalesSafeZero, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numSynchronizationPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.numClientReturnPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAutoSynchronization).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceClientLocation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplySalesTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyCashBackWithDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCloseSalesSafeZero).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
