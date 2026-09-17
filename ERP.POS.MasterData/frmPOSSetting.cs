using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.SafesAndBanks;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Microsoft.Win32;

namespace ERP.POS.MasterData;

public class frmPOSSetting : frmBase
{
	private RegistryKey RegKey;

	private bool IsLogoChanged = false;

	private DataTable dtAccounts;

	private DataTable dtsafes;

	private DataTable dtBranches;

	private DataTable dtPOSSetting;

	private DataTable dtStores;

	private DataTable dtClients;

	private DataTable dtCurrency;

	private DataTable dtTaxs;

	private FolderBrowserDialog fbd = new FolderBrowserDialog();

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraButton btnClose;

	private UltraLabel lblPrinterName;

	private UltraComboEditor cboPrinterName;

	private UltraButton btnSave;

	private UltraLabel lblLogo;

	private UltraPictureBox picbCompanyLogo;

	private UltraButton btnImagePath;

	private UltraTextEditor txtCompanyMessage;

	private UltraLabel lblCompanyMessage;

	private OpenFileDialog ofdPicture;

	private UltraComboEditor cboBranchSafe;

	private UltraLabel lblBranchSafeID;

	private UltraComboEditor cboCashierAccount;

	private UltraLabel lblCashierAccount;

	private UltraComboEditor cboDefaultStore;

	private UltraLabel lblDefaultStore;

	private UltraComboEditor cboDefaultClient;

	private UltraLabel lblDefaultClient;

	private UltraCheckEditor chkForceBarcodeUse;

	private UltraLabel lblCurrency;

	private UltraComboEditor cboCurrency;

	private UltraCheckEditor chkEnforeSalesManSelection;

	private UltraCheckEditor chkApplySalesTax;

	private UltraComboEditor cboTax;

	private UltraCheckEditor chkAllowEditTax;

	private UltraCheckEditor chkTransferSuggestPerStore;

	private UltraButton btnAdsPath;

	private UltraLabel lblSecondScreenAdsPath;

	private UltraCheckEditor chkGetPOSBalanceOnline;

	private UltraTextEditor txtPath;

	private UltraCheckEditor chkEnforceCloseChecks;

	private UltraCheckEditor chkSerialByShiftDetailID;

	private UltraLabel lblManufacturingClientAccount;

	private UltraComboEditor cboManufacturingClientAccount;

	private UltraLabel lblManufacturingBranch;

	private UltraComboEditor cboManufacturingBranch;

	private UltraCheckEditor chkEnforceSalesMan2Selection;

	private UltraTextEditor txtManufacturingMessage;

	private UltraLabel lblManfacturingMessage;

	private UltraCheckEditor chkGetItemBalanceAutomatically;

	public UltraCheckEditor chkApplyCashBackWithDiscount;

	public UltraLabel lblTitle;

	public frmPOSSetting()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
		{
			cboPrinterName.Items.Add((object)installedPrinter);
		}
		RegKey = Registry.CurrentUser.OpenSubKey(GlobalVariables.path, writable: true);
		if (RegKey != null && RegKey.GetValue("POSPrinter") != null)
		{
			((TextEditorControlBase)cboPrinterName).Value = RegKey.GetValue("POSPrinter");
		}
		if (cboPrinterName.SelectedIndex == -1)
		{
			((TextEditorControlBase)cboPrinterName).Value = DBNull.Value;
			((Control)(object)cboPrinterName).Text = "";
		}
		dtsafes = Safes.FillComboBySafeIDs("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranchSafe, dtsafes, "SafeID", "SafeName");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboManufacturingBranch, dtBranches, "BranchID", "BranchName");
		dtStores = Stores.FillCombo("-1", "-1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDefaultStore, dtStores, "StoreID", "StoreName");
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCashierAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboManufacturingClientAccount, dtAccounts, "AccountID", "Name");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDefaultClient, dtClients, "SubAccountID", "SubAccountName");
		dtCurrency = Currency.FillCurrencyByDate(GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		dtTaxs = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTax, dtTaxs, "TaxID", "TaxName");
		FillData();
		UltraLabel obj = lblLogo;
		UltraPictureBox obj2 = picbCompanyLogo;
		UltraButton obj3 = btnImagePath;
		UltraLabel obj4 = lblCompanyMessage;
		UltraTextEditor obj5 = txtCompanyMessage;
		UltraLabel obj6 = lblDefaultStore;
		UltraComboEditor obj7 = cboDefaultStore;
		UltraCheckEditor obj8 = chkForceBarcodeUse;
		UltraLabel obj9 = lblManfacturingMessage;
		UltraTextEditor obj10 = txtManufacturingMessage;
		UltraLabel obj11 = lblManufacturingBranch;
		UltraLabel obj12 = lblManufacturingClientAccount;
		UltraComboEditor obj13 = cboManufacturingBranch;
		UltraComboEditor obj14 = cboManufacturingClientAccount;
		UltraCheckEditor obj15 = chkApplySalesTax;
		UltraComboEditor obj16 = cboTax;
		UltraCheckEditor obj17 = chkAllowEditTax;
		UltraCheckEditor obj18 = chkEnforeSalesManSelection;
		bool flag = (((Control)(object)chkEnforceSalesMan2Selection).Visible = (Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]) || Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Clinics'")[0]["Installed"]) || Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='LensesLab'")[0]["Installed"])) && !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]));
		bool flag3 = (((Control)(object)obj18).Visible = flag);
		bool flag5 = (((Control)(object)obj17).Visible = flag3);
		bool flag7 = (((Control)(object)obj16).Visible = flag5);
		bool flag9 = (((Control)(object)obj15).Visible = flag7);
		bool flag11 = (((Control)(object)obj14).Visible = flag9);
		bool flag13 = (((Control)(object)obj13).Visible = flag11);
		bool flag15 = (((Control)(object)obj12).Visible = flag13);
		bool flag17 = (((Control)(object)obj11).Visible = flag15);
		bool flag19 = (((Control)(object)obj10).Visible = flag17);
		bool flag21 = (((Control)(object)obj9).Visible = flag19);
		bool flag23 = (((Control)(object)obj8).Visible = flag21);
		bool flag25 = (((Control)(object)obj7).Visible = flag23);
		bool flag27 = (((Control)(object)obj6).Visible = flag25);
		bool flag29 = (((Control)(object)obj5).Visible = flag27);
		bool flag31 = (((Control)(object)obj4).Visible = flag29);
		bool flag33 = (((Control)(object)obj3).Visible = flag31);
		bool visible = (((Control)(object)obj2).Visible = flag33);
		((Control)(object)obj).Visible = visible;
		((Control)(object)chkGetItemBalanceAutomatically).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]);
		((Control)(object)chkApplyCashBackWithDiscount).Visible = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]);
		UltraCheckEditor obj19 = chkGetPOSBalanceOnline;
		UltraCheckEditor obj20 = chkSerialByShiftDetailID;
		flag33 = (((Control)(object)chkEnforceCloseChecks).Visible = !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Lenses'")[0]["Installed"]) && !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='Clinics'")[0]["Installed"]) && !Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='LensesLab'")[0]["Installed"]) && Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='POS'")[0]["Installed"]));
		visible = (((Control)(object)obj20).Visible = flag33);
		((Control)(object)obj19).Visible = visible;
	}

	private void FillData()
	{
		dtPOSSetting = Settings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		if (dtPOSSetting.Rows.Count > 0)
		{
			picbCompanyLogo.Image = ((dtPOSSetting.Rows[0]["Logo"] == DBNull.Value) ? null : GetImage((byte[])dtPOSSetting.Rows[0]["Logo"]));
			picbCompanyLogo.ScaleImage = (ScaleImage)1;
			((Control)(object)txtCompanyMessage).Text = dtPOSSetting.Rows[0]["Message"].ToString();
			((Control)(object)txtManufacturingMessage).Text = dtPOSSetting.Rows[0]["ManufacturingMessage"].ToString();
			((TextEditorControlBase)cboBranchSafe).Value = dtPOSSetting.Rows[0]["BranchSafeID"];
			((TextEditorControlBase)cboCashierAccount).Value = dtPOSSetting.Rows[0]["CashierAccountID"];
			((TextEditorControlBase)cboDefaultStore).Value = dtPOSSetting.Rows[0]["DefaultStoreID"];
			((TextEditorControlBase)cboDefaultClient).Value = dtPOSSetting.Rows[0]["DefaultSubAccountID"];
			((UltraToggleEditorBase)chkForceBarcodeUse).Checked = bool.Parse(dtPOSSetting.Rows[0]["ForceBarcodeUse"].ToString());
			((UltraToggleEditorBase)chkEnforeSalesManSelection).Checked = bool.Parse(dtPOSSetting.Rows[0]["EnforceSalesManSelection"].ToString());
			((UltraToggleEditorBase)chkEnforceSalesMan2Selection).Checked = bool.Parse(dtPOSSetting.Rows[0]["EnforceSalesMan2Selection"].ToString());
			((TextEditorControlBase)cboCurrency).Value = dtPOSSetting.Rows[0]["CurrencyID"];
			((TextEditorControlBase)cboManufacturingBranch).Value = dtPOSSetting.Rows[0]["ManufacturingBranchID"];
			((TextEditorControlBase)cboManufacturingClientAccount).Value = dtPOSSetting.Rows[0]["ManufacturingClientAccountID"];
			((UltraToggleEditorBase)chkApplySalesTax).Checked = bool.Parse(dtPOSSetting.Rows[0]["ApplySalesTax"].ToString());
			((TextEditorControlBase)cboTax).Value = dtPOSSetting.Rows[0]["TaxID"];
			((UltraToggleEditorBase)chkAllowEditTax).Checked = bool.Parse(dtPOSSetting.Rows[0]["AllowEditTax"].ToString());
			((UltraToggleEditorBase)chkTransferSuggestPerStore).Checked = bool.Parse(dtPOSSetting.Rows[0]["TransferSuggestPerStore"].ToString());
			((UltraToggleEditorBase)chkGetPOSBalanceOnline).Checked = bool.Parse(dtPOSSetting.Rows[0]["GetPOSBalanceOnline"].ToString());
			((UltraToggleEditorBase)chkSerialByShiftDetailID).Checked = bool.Parse(dtPOSSetting.Rows[0]["SerialByShiftDetailID"].ToString());
			((UltraToggleEditorBase)chkGetItemBalanceAutomatically).Checked = bool.Parse(dtPOSSetting.Rows[0]["GetItemBalanceAutomatically"].ToString());
			((UltraToggleEditorBase)chkApplyCashBackWithDiscount).Checked = bool.Parse(dtPOSSetting.Rows[0]["ApplyCashBackWithDiscount"].ToString());
			((UltraToggleEditorBase)chkEnforceCloseChecks).Checked = bool.Parse(dtPOSSetting.Rows[0]["EnforceCloseChecks"].ToString());
			((Control)(object)txtPath).Text = dtPOSSetting.Rows[0]["SecondScreenAdsPath"].ToString();
			((EditorButtonControlBase)cboCurrency).ReadOnly = cboCurrency.SelectedIndex > -1 && int.Parse(Settings.ValidateCurrencyUpdateByBranchID(GlobalVariables.CurrentBranchID).Rows[0]["CounterCheck"].ToString()) > 0;
			UltraComboEditor obj = cboTax;
			bool enabled = (((Control)(object)chkAllowEditTax).Enabled = ((UltraToggleEditorBase)chkApplySalesTax).Checked);
			((Control)(object)obj).Enabled = enabled;
			IsLogoChanged = false;
		}
	}

	private Image GetImage(byte[] p)
	{
		MemoryStream stream = new MemoryStream(p);
		return Image.FromStream(stream);
	}

	private void btnAdsPath_Click(object sender, EventArgs e)
	{
		DialogResult dialogResult = fbd.ShowDialog();
		((Control)(object)txtPath).Text = fbd.SelectedPath;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (cboCurrency.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار عملة الفرع ", " Please Select Branch Currency ");
			((TextEditorControlBase)cboCurrency).Focus();
			return;
		}
		if (cboPrinterName.SelectedIndex != -1)
		{
			RegKey.SetValue("POSPrinter", ((TextEditorControlBase)cboPrinterName).Value.ToString());
			GlobalVariables.POSPrinter = RegKey.GetValue("POSPrinter").ToString();
		}
		int settingID = Settings.Insert_Update((dtPOSSetting.Rows.Count > 0) ? dtPOSSetting.Rows[0]["SettingID"].ToString() : "-1", ((Control)(object)txtCompanyMessage).Text, ((Control)(object)txtManufacturingMessage).Text, (cboBranchSafe.SelectedIndex > -1) ? ((TextEditorControlBase)cboBranchSafe).Value.ToString() : "Null", (cboCashierAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboCashierAccount).Value.ToString() : "Null", (cboDefaultStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultStore).Value.ToString() : "Null", (cboDefaultClient.SelectedIndex > -1) ? ((TextEditorControlBase)cboDefaultClient).Value.ToString() : "Null", (cboManufacturingClientAccount.SelectedIndex > -1) ? ((TextEditorControlBase)cboManufacturingClientAccount).Value.ToString() : "Null", (cboManufacturingBranch.SelectedIndex > -1) ? ((TextEditorControlBase)cboManufacturingBranch).Value.ToString() : "Null", ((UltraToggleEditorBase)chkForceBarcodeUse).Checked ? "1" : "0", ((UltraToggleEditorBase)chkEnforeSalesManSelection).Checked ? "1" : "0", ((UltraToggleEditorBase)chkEnforceSalesMan2Selection).Checked ? "1" : "0", (cboCurrency.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCurrency).Value.ToString(), ((UltraToggleEditorBase)chkApplySalesTax).Checked ? "1" : "0", (cboTax.SelectedIndex > -1) ? ((TextEditorControlBase)cboTax).Value.ToString() : "Null", ((UltraToggleEditorBase)chkAllowEditTax).Checked ? "1" : "0", ((UltraToggleEditorBase)chkTransferSuggestPerStore).Checked ? "1" : "0", ((UltraToggleEditorBase)chkGetPOSBalanceOnline).Checked ? "1" : "0", (((Control)(object)txtPath).Text == "") ? "Null" : ((Control)(object)txtPath).Text, ((UltraToggleEditorBase)chkEnforceCloseChecks).Checked ? "1" : "0", ((UltraToggleEditorBase)chkSerialByShiftDetailID).Checked ? "1" : "0", ((UltraToggleEditorBase)chkGetItemBalanceAutomatically).Checked ? "1" : "0", ((UltraToggleEditorBase)chkApplyCashBackWithDiscount).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
		if (IsLogoChanged)
		{
			if (picbCompanyLogo.Image != null)
			{
				Settings.Logo_Update(settingID, (Image)picbCompanyLogo.Image);
			}
			else
			{
				Main.ExecuteNonQuery("Update POS_Settings Set Logo = null");
			}
		}
		FillData();
		GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
	}

	private void btnImagePath_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			picbCompanyLogo.Image = Image.FromFile(ofdPicture.FileName);
			IsLogoChanged = true;
		}
	}

	private void PictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (((UltraPictureBox)((sender is UltraPictureBox) ? sender : null)).Image == null)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه الصورة ؟", "Do you want to Clear this Image?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			((UltraPictureBox)((sender is UltraPictureBox) ? sender : null)).Image = null;
			if (sender.Equals(picbCompanyLogo))
			{
				IsLogoChanged = true;
			}
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void chkApplySalesTax_CheckedChanged(object sender, EventArgs e)
	{
		UltraComboEditor obj = cboTax;
		bool enabled = (((Control)(object)chkAllowEditTax).Enabled = ((UltraToggleEditorBase)chkApplySalesTax).Checked);
		((Control)(object)obj).Enabled = enabled;
		if (!((UltraToggleEditorBase)chkApplySalesTax).Checked)
		{
			cboTax.SelectedIndex = -1;
			((UltraToggleEditorBase)chkAllowEditTax).Checked = false;
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmPOSSetting));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.lblTitle2 = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblPrinterName = new UltraLabel();
		this.cboPrinterName = new UltraComboEditor();
		this.btnSave = new UltraButton();
		this.lblLogo = new UltraLabel();
		this.picbCompanyLogo = new UltraPictureBox();
		this.btnImagePath = new UltraButton();
		this.txtCompanyMessage = new UltraTextEditor();
		this.lblCompanyMessage = new UltraLabel();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.cboBranchSafe = new UltraComboEditor();
		this.lblBranchSafeID = new UltraLabel();
		this.cboCashierAccount = new UltraComboEditor();
		this.lblCashierAccount = new UltraLabel();
		this.cboDefaultStore = new UltraComboEditor();
		this.lblDefaultStore = new UltraLabel();
		this.cboDefaultClient = new UltraComboEditor();
		this.lblDefaultClient = new UltraLabel();
		this.chkForceBarcodeUse = new UltraCheckEditor();
		this.lblCurrency = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.chkEnforeSalesManSelection = new UltraCheckEditor();
		this.chkApplySalesTax = new UltraCheckEditor();
		this.cboTax = new UltraComboEditor();
		this.chkAllowEditTax = new UltraCheckEditor();
		this.chkTransferSuggestPerStore = new UltraCheckEditor();
		this.btnAdsPath = new UltraButton();
		this.lblSecondScreenAdsPath = new UltraLabel();
		this.chkGetPOSBalanceOnline = new UltraCheckEditor();
		this.txtPath = new UltraTextEditor();
		this.chkEnforceCloseChecks = new UltraCheckEditor();
		this.chkSerialByShiftDetailID = new UltraCheckEditor();
		this.lblManufacturingClientAccount = new UltraLabel();
		this.cboManufacturingClientAccount = new UltraComboEditor();
		this.lblManufacturingBranch = new UltraLabel();
		this.cboManufacturingBranch = new UltraComboEditor();
		this.chkEnforceSalesMan2Selection = new UltraCheckEditor();
		this.txtManufacturingMessage = new UltraTextEditor();
		this.lblManfacturingMessage = new UltraLabel();
		this.chkGetItemBalanceAutomatically = new UltraCheckEditor();
		this.chkApplyCashBackWithDiscount = new UltraCheckEditor();
		this.lblTitle = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchSafe).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCashierAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForceBarcodeUse).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforeSalesManSelection).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplySalesTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllowEditTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkTransferSuggestPerStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkGetPOSBalanceOnline).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPath).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceCloseChecks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSerialByShiftDetailID).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboManufacturingClientAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboManufacturingBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceSalesMan2Selection).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtManufacturingMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkGetItemBalanceAutomatically).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyCashBackWithDiscount).BeginInit();
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
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblPrinterName, "lblPrinterName");
		this.lblPrinterName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrinterName).Name = "lblPrinterName";
		((ControlBase)this.lblPrinterName).WrapText = false;
		resources.ApplyResources(this.cboPrinterName, "cboPrinterName");
		this.cboPrinterName.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPrinterName).Name = "cboPrinterName";
		resources.ApplyResources(this.btnSave, "btnSave");
		((UltraButtonBase)this.btnSave).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblLogo, "lblLogo");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.lblLogo).Appearance = (AppearanceBase)(object)val3;
		this.lblLogo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLogo).Name = "lblLogo";
		((ControlBase)this.lblLogo).WrapText = false;
		resources.ApplyResources(this.picbCompanyLogo, "picbCompanyLogo");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		this.picbCompanyLogo.Appearance = (AppearanceBase)(object)val4;
		this.picbCompanyLogo.BorderShadowColor = System.Drawing.Color.Empty;
		this.picbCompanyLogo.BorderStyle = (UIElementBorderStyle)2;
		this.picbCompanyLogo.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.picbCompanyLogo).Name = "picbCompanyLogo";
		((System.Windows.Forms.Control)(object)this.picbCompanyLogo).MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(PictureBox_MouseDoubleClick);
		resources.ApplyResources(this.btnImagePath, "btnImagePath");
		((UltraButtonBase)this.btnImagePath).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnImagePath).Name = "btnImagePath";
		((System.Windows.Forms.Control)(object)this.btnImagePath).Click += new System.EventHandler(btnImagePath_Click);
		resources.ApplyResources(this.txtCompanyMessage, "txtCompanyMessage");
		((System.Windows.Forms.Control)(object)this.txtCompanyMessage).Name = "txtCompanyMessage";
		resources.ApplyResources(this.lblCompanyMessage, "lblCompanyMessage");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.lblCompanyMessage).Appearance = (AppearanceBase)(object)val5;
		this.lblCompanyMessage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanyMessage).Name = "lblCompanyMessage";
		((ControlBase)this.lblCompanyMessage).WrapText = false;
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.cboBranchSafe, "cboBranchSafe");
		this.cboBranchSafe.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBranchSafe).Name = "cboBranchSafe";
		resources.ApplyResources(this.lblBranchSafeID, "lblBranchSafeID");
		this.lblBranchSafeID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchSafeID).Name = "lblBranchSafeID";
		((ControlBase)this.lblBranchSafeID).WrapText = false;
		resources.ApplyResources(this.cboCashierAccount, "cboCashierAccount");
		this.cboCashierAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCashierAccount).Name = "cboCashierAccount";
		resources.ApplyResources(this.lblCashierAccount, "lblCashierAccount");
		this.lblCashierAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCashierAccount).Name = "lblCashierAccount";
		((ControlBase)this.lblCashierAccount).WrapText = false;
		resources.ApplyResources(this.cboDefaultStore, "cboDefaultStore");
		this.cboDefaultStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultStore).Name = "cboDefaultStore";
		resources.ApplyResources(this.lblDefaultStore, "lblDefaultStore");
		this.lblDefaultStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultStore).Name = "lblDefaultStore";
		((ControlBase)this.lblDefaultStore).WrapText = false;
		resources.ApplyResources(this.cboDefaultClient, "cboDefaultClient");
		this.cboDefaultClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboDefaultClient).Name = "cboDefaultClient";
		resources.ApplyResources(this.lblDefaultClient, "lblDefaultClient");
		this.lblDefaultClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultClient).Name = "lblDefaultClient";
		((ControlBase)this.lblDefaultClient).WrapText = false;
		resources.ApplyResources(this.chkForceBarcodeUse, "chkForceBarcodeUse");
		((System.Windows.Forms.Control)(object)this.chkForceBarcodeUse).Name = "chkForceBarcodeUse";
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		resources.ApplyResources(this.chkEnforeSalesManSelection, "chkEnforeSalesManSelection");
		((System.Windows.Forms.Control)(object)this.chkEnforeSalesManSelection).Name = "chkEnforeSalesManSelection";
		resources.ApplyResources(this.chkApplySalesTax, "chkApplySalesTax");
		((System.Windows.Forms.Control)(object)this.chkApplySalesTax).Name = "chkApplySalesTax";
		((UltraToggleEditorBase)this.chkApplySalesTax).CheckedChanged += new System.EventHandler(chkApplySalesTax_CheckedChanged);
		((TextEditorControlBase)this.cboTax).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboTax, "cboTax");
		this.cboTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		resources.ApplyResources(this.chkAllowEditTax, "chkAllowEditTax");
		((System.Windows.Forms.Control)(object)this.chkAllowEditTax).Name = "chkAllowEditTax";
		resources.ApplyResources(this.chkTransferSuggestPerStore, "chkTransferSuggestPerStore");
		((System.Windows.Forms.Control)(object)this.chkTransferSuggestPerStore).Name = "chkTransferSuggestPerStore";
		resources.ApplyResources(this.btnAdsPath, "btnAdsPath");
		((UltraButtonBase)this.btnAdsPath).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnAdsPath).Name = "btnAdsPath";
		((System.Windows.Forms.Control)(object)this.btnAdsPath).Click += new System.EventHandler(btnAdsPath_Click);
		resources.ApplyResources(this.lblSecondScreenAdsPath, "lblSecondScreenAdsPath");
		this.lblSecondScreenAdsPath.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSecondScreenAdsPath).Name = "lblSecondScreenAdsPath";
		((ControlBase)this.lblSecondScreenAdsPath).WrapText = false;
		resources.ApplyResources(this.chkGetPOSBalanceOnline, "chkGetPOSBalanceOnline");
		((System.Windows.Forms.Control)(object)this.chkGetPOSBalanceOnline).Name = "chkGetPOSBalanceOnline";
		resources.ApplyResources(this.txtPath, "txtPath");
		((System.Windows.Forms.Control)(object)this.txtPath).Name = "txtPath";
		resources.ApplyResources(this.chkEnforceCloseChecks, "chkEnforceCloseChecks");
		((System.Windows.Forms.Control)(object)this.chkEnforceCloseChecks).Name = "chkEnforceCloseChecks";
		resources.ApplyResources(this.chkSerialByShiftDetailID, "chkSerialByShiftDetailID");
		((System.Windows.Forms.Control)(object)this.chkSerialByShiftDetailID).Name = "chkSerialByShiftDetailID";
		resources.ApplyResources(this.lblManufacturingClientAccount, "lblManufacturingClientAccount");
		this.lblManufacturingClientAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManufacturingClientAccount).Name = "lblManufacturingClientAccount";
		((ControlBase)this.lblManufacturingClientAccount).WrapText = false;
		resources.ApplyResources(this.cboManufacturingClientAccount, "cboManufacturingClientAccount");
		this.cboManufacturingClientAccount.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboManufacturingClientAccount).Name = "cboManufacturingClientAccount";
		resources.ApplyResources(this.lblManufacturingBranch, "lblManufacturingBranch");
		this.lblManufacturingBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManufacturingBranch).Name = "lblManufacturingBranch";
		((ControlBase)this.lblManufacturingBranch).WrapText = false;
		resources.ApplyResources(this.cboManufacturingBranch, "cboManufacturingBranch");
		this.cboManufacturingBranch.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboManufacturingBranch).Name = "cboManufacturingBranch";
		resources.ApplyResources(this.chkEnforceSalesMan2Selection, "chkEnforceSalesMan2Selection");
		((System.Windows.Forms.Control)(object)this.chkEnforceSalesMan2Selection).Name = "chkEnforceSalesMan2Selection";
		resources.ApplyResources(this.txtManufacturingMessage, "txtManufacturingMessage");
		((System.Windows.Forms.Control)(object)this.txtManufacturingMessage).Name = "txtManufacturingMessage";
		resources.ApplyResources(this.lblManfacturingMessage, "lblManfacturingMessage");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.lblManfacturingMessage).Appearance = (AppearanceBase)(object)val6;
		this.lblManfacturingMessage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManfacturingMessage).Name = "lblManfacturingMessage";
		((ControlBase)this.lblManfacturingMessage).WrapText = false;
		resources.ApplyResources(this.chkGetItemBalanceAutomatically, "chkGetItemBalanceAutomatically");
		((System.Windows.Forms.Control)(object)this.chkGetItemBalanceAutomatically).Name = "chkGetItemBalanceAutomatically";
		resources.ApplyResources(this.chkApplyCashBackWithDiscount, "chkApplyCashBackWithDiscount");
		((System.Windows.Forms.Control)(object)this.chkApplyCashBackWithDiscount).Name = "chkApplyCashBackWithDiscount";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkApplyCashBackWithDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAdsPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllowEditTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSecondScreenAdsPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkGetItemBalanceAutomatically);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkGetPOSBalanceOnline);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkTransferSuggestPerStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSerialByShiftDetailID);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceCloseChecks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceSalesMan2Selection);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforeSalesManSelection);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkApplySalesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForceBarcodeUse);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboManufacturingBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboManufacturingClientAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManufacturingBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManufacturingClientAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCashierAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCashierAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchSafe);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchSafeID);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManfacturingMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanyMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtManufacturingMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picbCompanyLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnImagePath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPrinterName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPrinterName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmPOSSetting";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPrinterName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPrinterName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnImagePath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picbCompanyLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanyMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtManufacturingMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompanyMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManfacturingMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchSafeID, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchSafe, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCashierAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCashierAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManufacturingClientAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManufacturingBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboManufacturingClientAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboManufacturingBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDefaultStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDefaultStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDefaultClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDefaultClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForceBarcodeUse, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkApplySalesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnforeSalesManSelection, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnforceSalesMan2Selection, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnforceCloseChecks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSerialByShiftDetailID, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkTransferSuggestPerStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkGetPOSBalanceOnline, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkGetItemBalanceAutomatically, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSecondScreenAdsPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllowEditTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAdsPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkApplyCashBackWithDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchSafe).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCashierAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForceBarcodeUse).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforeSalesManSelection).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplySalesTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllowEditTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkTransferSuggestPerStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkGetPOSBalanceOnline).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPath).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceCloseChecks).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSerialByShiftDetailID).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboManufacturingClientAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboManufacturingBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceSalesMan2Selection).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtManufacturingMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkGetItemBalanceAutomatically).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApplyCashBackWithDiscount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
