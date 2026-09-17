using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Privilege.MasterData;

public class frmUsersDirectSalesApp : frmBase
{
	private DataTable dtUsersDirectSalesApp;

	private DataTable dtBranches;

	private DataTable dtStores;

	private string UserID;

	private IContainer components = null;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	private UltraLabel lblDefaultStore;

	private UltraLabel lblManufacturingBranch;

	private UltraLabel lblSecondScreenAdsPath;

	private UltraCheckEditor chkCanModifyPriceType;

	private UltraLabel lblSalesDiscountPercentage;

	private UltraTextEditor txtUserToken;

	private UltraComboEditor cboDefaultStore;

	private UltraComboEditor cboDefaultBranch;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtSalesDiscountPercentage;

	public frmUsersDirectSalesApp(string userID)
	{
		UserID = userID;
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultBranch, dtBranches, "BranchID", "BranchName");
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultStore, dtStores, "StoreID", "StoreName");
		DisplayData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	public void DisplayData()
	{
		dtUsersDirectSalesApp = UsersDirectSalesApp.SelectByUser_ID(UserID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtUsersDirectSalesApp.Rows.Count > 0)
		{
			((TextEditorControlBase)cboDefaultBranch).Value = dtUsersDirectSalesApp.Rows[0]["DefaultBranchID"];
			((TextEditorControlBase)cboDefaultStore).Value = dtUsersDirectSalesApp.Rows[0]["DefaultStoreID"];
			((UltraToggleEditorBase)chkCanModifyPriceType).Checked = bool.Parse(dtUsersDirectSalesApp.Rows[0]["CanModifyPriceType"].ToString());
			((Control)(object)txtUserToken).Text = dtUsersDirectSalesApp.Rows[0]["UserToken"].ToString();
			((Control)(object)txtSalesDiscountPercentage).Text = dtUsersDirectSalesApp.Rows[0]["SalesDiscountPercentage"].ToString();
		}
	}

	public bool ValidateData()
	{
		if (cboDefaultStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الإفتراضي", "Please select Default Store");
			((TextEditorControlBase)cboDefaultStore).Focus();
			return false;
		}
		if (cboDefaultStore.SelectedIndex > -1 && UsersDirectSalesApp.DefaultStoreValidation(UserID.ToString(), ((TextEditorControlBase)cboDefaultStore).Value.ToString(), IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show("المخزن الافتراضى مربوط على مستخدم أخر", "Default Store Already Related To Another User");
			((TextEditorControlBase)cboDefaultStore).Focus();
			return false;
		}
		return true;
	}

	public void Save()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = UsersDirectSalesApp.Insert_Update((dtUsersDirectSalesApp.Rows.Count > 0) ? dtUsersDirectSalesApp.Rows[0]["DirectSaleAppID"].ToString() : "-1", UserID, (cboDefaultBranch.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultBranch).Value.ToString() : "Null", (cboDefaultStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultStore).Value.ToString() : "Null", ((Control)(object)txtUserToken).Text, ((UltraToggleEditorBase)chkCanModifyPriceType).Checked ? "1" : "0", (((Control)(object)txtSalesDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtSalesDiscountPercentage).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Saved Successfully.");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void txtSalesDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtSalesDiscountPercentage_Leave(object sender, EventArgs e)
	{
		if (((Control)(object)txtSalesDiscountPercentage).Text != "" && decimal.Parse(((Control)(object)txtSalesDiscountPercentage).Text) > 100m)
		{
			((Control)(object)txtSalesDiscountPercentage).Text = "100";
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			DisplayData();
		}
	}

	private void cboDefaultBranch_ValueChanged(object sender, EventArgs e)
	{
		if (cboDefaultBranch.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " BranchID=" + ((TextEditorControlBase)cboDefaultBranch).Value.ToString();
			GlobalFunctions.FillCombo(cboDefaultStore, dataView.ToTable(), "StoreID", "StoreName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboDefaultStore, dtStores, "StoreID", "StoreName");
		}
		cboDefaultStore.SelectedIndex = -1;
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Dispose();
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
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Privilege.MasterData.frmUsersDirectSalesApp));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblDefaultStore = new UltraLabel();
		this.lblManufacturingBranch = new UltraLabel();
		this.lblSecondScreenAdsPath = new UltraLabel();
		this.chkCanModifyPriceType = new UltraCheckEditor();
		this.lblSalesDiscountPercentage = new UltraLabel();
		this.txtUserToken = new UltraTextEditor();
		this.cboDefaultStore = new UltraComboEditor();
		this.cboDefaultBranch = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtSalesDiscountPercentage = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModifyPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserToken).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesDiscountPercentage).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val, "appearance9");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance10");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val3, "appearance11");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val4).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val4, "appearance12");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val5).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val5, "appearance13");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance14");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblDefaultStore, "lblDefaultStore");
		this.lblDefaultStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultStore).Name = "lblDefaultStore";
		((ControlBase)this.lblDefaultStore).WrapText = false;
		resources.ApplyResources(this.lblManufacturingBranch, "lblManufacturingBranch");
		this.lblManufacturingBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManufacturingBranch).Name = "lblManufacturingBranch";
		((ControlBase)this.lblManufacturingBranch).WrapText = false;
		resources.ApplyResources(this.lblSecondScreenAdsPath, "lblSecondScreenAdsPath");
		this.lblSecondScreenAdsPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSecondScreenAdsPath).Name = "lblSecondScreenAdsPath";
		((ControlBase)this.lblSecondScreenAdsPath).WrapText = false;
		resources.ApplyResources(this.chkCanModifyPriceType, "chkCanModifyPriceType");
		((System.Windows.Forms.Control)(object)this.chkCanModifyPriceType).Name = "chkCanModifyPriceType";
		resources.ApplyResources(this.lblSalesDiscountPercentage, "lblSalesDiscountPercentage");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val7, "appearance15");
		((ControlBase)this.lblSalesDiscountPercentage).Appearance = (AppearanceBase)(object)val7;
		this.lblSalesDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesDiscountPercentage).Name = "lblSalesDiscountPercentage";
		((ControlBase)this.lblSalesDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.txtUserToken, "txtUserToken");
		((System.Windows.Forms.Control)(object)this.txtUserToken).Name = "txtUserToken";
		resources.ApplyResources(this.cboDefaultStore, "cboDefaultStore");
		this.cboDefaultStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultStore).Name = "cboDefaultStore";
		resources.ApplyResources(this.cboDefaultBranch, "cboDefaultBranch");
		this.cboDefaultBranch.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultBranch).Name = "cboDefaultBranch";
		((TextEditorControlBase)this.cboDefaultBranch).ValueChanged += new System.EventHandler(cboDefaultBranch_ValueChanged);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance16");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val8;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtSalesDiscountPercentage, "txtSalesDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage).Name = "txtSalesDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSalesDiscountPercentage_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage).Leave += new System.EventHandler(txtSalesDiscountPercentage_Leave);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModifyPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserToken);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSecondScreenAdsPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManufacturingBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmUsersDirectSalesApp";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDefaultStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDefaultStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManufacturingBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDefaultBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSecondScreenAdsPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserToken, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModifyPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesDiscountPercentage, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModifyPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserToken).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesDiscountPercentage).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
