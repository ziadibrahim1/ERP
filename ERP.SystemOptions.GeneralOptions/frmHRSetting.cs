using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Defaults;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.SystemOptions.GeneralOptions;

public class frmHRSetting : frmBase
{
	private DataTable dtSubAccounts;

	private DataTable dtHRSetting;

	private DataTable dtHRFamilyRelatives;

	private DataTable dtHrRelativesInclusiveHealthInsurance;

	private DataTable dtSystemAccounts;

	private DataTable dtAccountTypes;

	private DataTable dtSystemOptions;

	private ValueList vlAccountID = new ValueList();

	private ValueList vlSubAccounts = new ValueList();

	private ValueList vlFamilyRelatives = new ValueList();

	private bool IsLogoChanged = false;

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraButton btnSave;

	public UltraButton btnKeyboard;

	private OpenFileDialog ofdPicture;

	private UltraTabControl tcSystemDefaults;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGroupBox UGBInssuranceSetting;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel2;

	private UltraLabel lblBasicSalaryInssuranceMax;

	private UltraTextEditor txtBasicSalaryInssuranceMax;

	private UltraTextEditor txtVariantSalaryInssuranceMax;

	private UltraLabel lblVariantSalaryInssuranceMax;

	private UltraLabel lblBasicEmployeeInssurancePercentage;

	private UltraTextEditor txtBasicEmployeeInssurancePercentageUnder18;

	private UltraTextEditor txtBasicEmployeeInssurancePercentageOver60;

	private UltraTextEditor txtBasicEmployeeInssurancePercentage;

	private UltraLabel lblBasicEmployeeInssurancePercentage1;

	private UltraTextEditor txtBasicOwnerInssurancePercentageOver60;

	private UltraTextEditor txtBasicOwnerInssurancePercentageUnder18;

	private UltraTextEditor txtBasicOwnerInssurancePercentage;

	private UltraLabel lblBasicOwnerInssurancePercentage;

	private UltraLabel lblVariantOwnerInssurancePercentage1;

	private UltraLabel lblBasicOwnerInssurancePercentage1;

	private UltraLabel lblVariantOwnerInssurancePercentage;

	private UltraTextEditor txtVariantEmployeeInssurancePercentageOver60;

	private UltraTextEditor txtVariantEmployeeInssurancePercentageUnder18;

	private UltraTextEditor txtVariantEmployeeInssurancePercentage;

	private UltraTextEditor txtVariantOwnerInssurancePercentageOver60;

	private UltraTextEditor txtVariantOwnerInssurancePercentageUnder18;

	private UltraTextEditor txtVariantOwnerInssurancePercentage;

	private UltraLabel lblVariantEmployeeInssurancePercentage;

	private UltraLabel lblVariantEmployeeInssurancePercentage1;

	private UltraLabel lblLogo;

	private UltraPictureBox picbCompanyLogo;

	private UltraButton btnImagePath;

	private UltraComboEditor cboSalaryRound;

	private UltraLabel lblBasicAnnualIncreasePercentage1;

	private UltraLabel lblBasicAnnualIncreasePercentage;

	private UltraTextEditor txtBasicAnnualIncreasePercentage;

	private UltraLabel ultraLabel1;

	private UltraLabel lblMonthEndDate1;

	private UltraLabel lblMonthEndDate;

	private UltraTextEditor txtMonthEndDate;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraGrid ULGSysAcc;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraGrid ULGSysOption;

	private UltraLabel ultraLabel5;

	private UltraComboEditor cboExtraTimeRound;

	private UltraLabel ultraLabel4;

	protected internal UltraCheckEditor chkApply2020Rule;

	protected internal UltraCheckEditor chkSocialInssuranceValueIsTotalInssuranceSalary;

	protected internal UltraCheckEditor chkInclusiveHealthInsurance;

	private GroupBox gbInclusiveHealthInsurance;

	public UltraGrid ULGHealthInsuranceRelatives;

	private UltraLabel lblInclusiveHealthInsuranceMinPercentage;

	private UltraLabel lblInclusiveHealthInsuranceMaxPercentage;

	private UltraTextEditor txtInclusiveHealthInsuranceMaxPercentage;

	private UltraTextEditor txtInclusiveHealthInsuranceMinPercentage;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtInclusiveHealthInsuranceOwnerPercentage;

	public frmHRSetting()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtHRFamilyRelatives = FamilyRelatives.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlFamilyRelatives.ValueListItems.Clear();
		for (int i = 0; i < dtHRFamilyRelatives.Rows.Count; i++)
		{
			vlFamilyRelatives.ValueListItems.Add(dtHRFamilyRelatives.Rows[i]["FamilyRelativeID"], dtHRFamilyRelatives.Rows[i]["FamilyRelativeName"].ToString());
		}
		FillData();
	}

	private void FillData()
	{
		dtSystemOptions = BusinessLayer.Defaults.SystemOptions.SelectForModule("301", "310", GlobalVariables.IsArabic ? "1" : "0");
		dtHRSetting = Setting.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtHrRelativesInclusiveHealthInsurance = FamilyRelativesInclusiveHealthInsuranceSettings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtSystemAccounts = SystemAccount.SelectForModule("301", "310", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtAccountTypes = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		foreach (DataRow row in dtAccountTypes.Rows)
		{
			vlAccountID.ValueListItems.Add(row["AccountID"], row["Name"].ToString());
		}
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtSubAccounts.Rows.Count; i++)
		{
			vlSubAccounts.ValueListItems.Add(dtSubAccounts.Rows[i]["SubAccountID"], dtSubAccounts.Rows[i]["Name"].ToString());
		}
		if (dtHRSetting.Rows.Count > 0)
		{
			picbCompanyLogo.Image = ((dtHRSetting.Rows[0]["Logo"] == DBNull.Value) ? null : GetImage((byte[])dtHRSetting.Rows[0]["Logo"]));
			picbCompanyLogo.ScaleImage = (ScaleImage)1;
			((Control)(object)txtMonthEndDate).Text = dtHRSetting.Rows[0]["MonthEndDate"].ToString();
			((UltraToggleEditorBase)chkApply2020Rule).Checked = Convert.ToBoolean(dtHRSetting.Rows[0]["Apply2020InssuranceRule"]);
			((UltraToggleEditorBase)chkSocialInssuranceValueIsTotalInssuranceSalary).Checked = Convert.ToBoolean(dtHRSetting.Rows[0]["SocialInssuranceValueIsTotalInssuranceSalary"]);
			((Control)(object)txtBasicSalaryInssuranceMax).Text = dtHRSetting.Rows[0]["BasicSalaryInssuranceMax"].ToString();
			((Control)(object)txtVariantSalaryInssuranceMax).Text = dtHRSetting.Rows[0]["VariantSalaryInssuranceMax"].ToString();
			((Control)(object)txtBasicEmployeeInssurancePercentage).Text = dtHRSetting.Rows[0]["BasicEmployeeInssurancePercentage"].ToString();
			((Control)(object)txtBasicOwnerInssurancePercentage).Text = dtHRSetting.Rows[0]["BasicOwnerInssurancePercentage"].ToString();
			((Control)(object)txtVariantEmployeeInssurancePercentage).Text = dtHRSetting.Rows[0]["VariantEmployeeInssurancePercentage"].ToString();
			((Control)(object)txtVariantOwnerInssurancePercentage).Text = dtHRSetting.Rows[0]["VariantOwnerInssurancePercentage"].ToString();
			((Control)(object)txtBasicEmployeeInssurancePercentageUnder18).Text = dtHRSetting.Rows[0]["BasicEmployeeInssurancePercentageUnder18"].ToString();
			((Control)(object)txtBasicOwnerInssurancePercentageUnder18).Text = dtHRSetting.Rows[0]["BasicOwnerInssurancePercentageUnder18"].ToString();
			((Control)(object)txtVariantEmployeeInssurancePercentageUnder18).Text = dtHRSetting.Rows[0]["VariantEmployeeInssurancePercentageUnder18"].ToString();
			((Control)(object)txtVariantOwnerInssurancePercentageUnder18).Text = dtHRSetting.Rows[0]["VariantOwnerInssurancePercentageUnder18"].ToString();
			((Control)(object)txtBasicEmployeeInssurancePercentageOver60).Text = dtHRSetting.Rows[0]["BasicEmployeeInssurancePercentageOver60"].ToString();
			((Control)(object)txtBasicOwnerInssurancePercentageOver60).Text = dtHRSetting.Rows[0]["BasicOwnerInssurancePercentageOver60"].ToString();
			((Control)(object)txtVariantEmployeeInssurancePercentageOver60).Text = dtHRSetting.Rows[0]["VariantEmployeeInssurancePercentageOver60"].ToString();
			((Control)(object)txtVariantOwnerInssurancePercentageOver60).Text = dtHRSetting.Rows[0]["VariantOwnerInssurancePercentageOver60"].ToString();
			((Control)(object)txtBasicAnnualIncreasePercentage).Text = dtHRSetting.Rows[0]["BasicAnnualIncreasePercentage"].ToString();
			((TextEditorControlBase)cboSalaryRound).Value = dtHRSetting.Rows[0]["SalaryRound"];
			((TextEditorControlBase)cboExtraTimeRound).Value = dtHRSetting.Rows[0]["ExtraTimeRound"];
			((UltraToggleEditorBase)chkInclusiveHealthInsurance).Checked = Convert.ToBoolean(dtHRSetting.Rows[0]["InclusiveHealthInsurance"]);
			((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text = dtHRSetting.Rows[0]["InclusiveHealthInsuranceEmployeeMinPercentage"].ToString();
			((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text = dtHRSetting.Rows[0]["InclusiveHealthInsuranceEmployeeMaxPercentage"].ToString();
			((Control)(object)txtInclusiveHealthInsuranceOwnerPercentage).Text = dtHRSetting.Rows[0]["InclusiveHealthInsuranceOwnerPercentage"].ToString();
			IsLogoChanged = false;
			InitGridsRelatives();
		}
		InitGridsSysOption();
		InitGridsSystemAccounts();
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysAcc).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGSysAcc).Rows[j].Cells["AccountID"].Value != DBNull.Value)
			{
				int accountID = int.Parse(((UltraGridBase)ULGSysAcc).Rows[j].Cells["AccountID"].Value.ToString());
				ValueList subAccountValueList = getSubAccountValueList(accountID);
				((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].ValueList = (IValueList)(object)subAccountValueList;
				if (((DisposableObjectCollectionBase)subAccountValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].Value = DBNull.Value;
				}
			}
		}
	}

	private Image GetImage(byte[] p)
	{
		MemoryStream stream = new MemoryStream(p);
		return Image.FromStream(stream);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		Main.StartBulkTrans(FromServer: true);
		int num = 0;
		try
		{
			Setting.Delete("-1", GlobalVariables.UserID, IsFromServer: true);
			num = Setting.Insert_Update("-1", (((Control)(object)txtMonthEndDate).Text == "") ? "0" : ((Control)(object)txtMonthEndDate).Text, ((UltraToggleEditorBase)chkApply2020Rule).Checked ? "1" : "0", ((UltraToggleEditorBase)chkSocialInssuranceValueIsTotalInssuranceSalary).Checked ? "1" : "0", ((UltraToggleEditorBase)chkInclusiveHealthInsurance).Checked ? "1" : "0", (((Control)(object)txtInclusiveHealthInsuranceOwnerPercentage).Text == "") ? "0" : ((Control)(object)txtInclusiveHealthInsuranceOwnerPercentage).Text, (((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text == "") ? "0" : ((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text, (((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text == "") ? "0" : ((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text, (((Control)(object)txtBasicSalaryInssuranceMax).Text == "") ? "0" : ((Control)(object)txtBasicSalaryInssuranceMax).Text, (((Control)(object)txtVariantSalaryInssuranceMax).Text == "") ? "0" : ((Control)(object)txtVariantSalaryInssuranceMax).Text, (((Control)(object)txtBasicEmployeeInssurancePercentage).Text == "") ? "0" : ((Control)(object)txtBasicEmployeeInssurancePercentage).Text, (((Control)(object)txtBasicOwnerInssurancePercentage).Text == "") ? "0" : ((Control)(object)txtBasicOwnerInssurancePercentage).Text, (((Control)(object)txtVariantEmployeeInssurancePercentage).Text == "") ? "0" : ((Control)(object)txtVariantEmployeeInssurancePercentage).Text, (((Control)(object)txtVariantOwnerInssurancePercentage).Text == "") ? "0" : ((Control)(object)txtVariantOwnerInssurancePercentage).Text, (((Control)(object)txtBasicEmployeeInssurancePercentageUnder18).Text == "") ? "0" : ((Control)(object)txtBasicEmployeeInssurancePercentageUnder18).Text, (((Control)(object)txtBasicOwnerInssurancePercentageUnder18).Text == "") ? "0" : ((Control)(object)txtBasicOwnerInssurancePercentageUnder18).Text, (((Control)(object)txtVariantEmployeeInssurancePercentageUnder18).Text == "") ? "0" : ((Control)(object)txtVariantEmployeeInssurancePercentageUnder18).Text, (((Control)(object)txtVariantOwnerInssurancePercentageUnder18).Text == "") ? "0" : ((Control)(object)txtVariantOwnerInssurancePercentageUnder18).Text, (((Control)(object)txtBasicEmployeeInssurancePercentageOver60).Text == "") ? "0" : ((Control)(object)txtBasicEmployeeInssurancePercentageOver60).Text, (((Control)(object)txtBasicOwnerInssurancePercentageOver60).Text == "") ? "0" : ((Control)(object)txtBasicOwnerInssurancePercentageOver60).Text, (((Control)(object)txtVariantEmployeeInssurancePercentageOver60).Text == "") ? "0" : ((Control)(object)txtVariantEmployeeInssurancePercentageOver60).Text, (((Control)(object)txtVariantOwnerInssurancePercentageOver60).Text == "") ? "0" : ((Control)(object)txtVariantOwnerInssurancePercentageOver60).Text, (((Control)(object)txtBasicAnnualIncreasePercentage).Text == "") ? "0" : ((Control)(object)txtBasicAnnualIncreasePercentage).Text, (cboSalaryRound.SelectedIndex == -1) ? "1" : ((TextEditorControlBase)cboSalaryRound).Value.ToString(), (cboExtraTimeRound.SelectedIndex == -1) ? "15" : ((TextEditorControlBase)cboExtraTimeRound).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGHealthInsuranceRelatives).Rows).Count; i++)
			{
				((UltraGridBase)ULGHealthInsuranceRelatives).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			((UltraGridBase)ULGHealthInsuranceRelatives).UpdateData();
			if (((DataTable)((UltraGridBase)ULGHealthInsuranceRelatives).DataSource).Rows.Count > 0)
			{
				FamilyRelativesInclusiveHealthInsuranceSettings.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGHealthInsuranceRelatives).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			SystemAccount.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGSysAcc).DataSource, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
			BusinessLayer.Defaults.SystemOptions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGSysOption).DataSource, GlobalVariables.UserID);
			GlobalVariables.dtSystemOptions = BusinessLayer.Defaults.SystemOptions.Select("-1", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.dtSystemAccounts = SystemAccount.Select("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		if (IsLogoChanged)
		{
			if (picbCompanyLogo.Image != null)
			{
				Setting.Logo_Update(num, (Image)picbCompanyLogo.Image);
			}
			else
			{
				Main.ExecuteNonQuery("Update HR_Settings Set Logo = null");
			}
		}
		GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
		FillData();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnImagePath_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			picbCompanyLogo.Image = Image.FromFile(ofdPicture.FileName);
			IsLogoChanged = true;
		}
	}

	private void picbCompanyLogo_MouseDoubleClick(object sender, MouseEventArgs e)
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

	public void InitGridsSysOption()
	{
		((UltraGridBase)ULGSysOption).DataSource = dtSystemOptions;
		GlobalFunctions.PrepareGrid(ULGSysOption);
		((UltraGridBase)ULGSysOption).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Header).Caption = (GlobalVariables.IsArabic ? "" : "");
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["OptionValue"].Width = (int)((double)((Control)(object)ULGSysOption).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSysOption).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGSysOption).Width * 0.6);
	}

	private void chkApply2020Rule_CheckedChanged(object sender, EventArgs e)
	{
		UltraTextEditor obj = txtVariantEmployeeInssurancePercentage;
		UltraTextEditor obj2 = txtVariantEmployeeInssurancePercentageOver60;
		UltraTextEditor obj3 = txtVariantEmployeeInssurancePercentageUnder18;
		UltraTextEditor obj4 = txtVariantOwnerInssurancePercentage;
		UltraTextEditor obj5 = txtVariantOwnerInssurancePercentageOver60;
		UltraTextEditor obj6 = txtVariantOwnerInssurancePercentageUnder18;
		bool flag = (((Control)(object)txtVariantSalaryInssuranceMax).Enabled = !((UltraToggleEditorBase)chkApply2020Rule).Checked);
		bool flag3 = (((Control)(object)obj6).Enabled = flag);
		bool flag5 = (((Control)(object)obj5).Enabled = flag3);
		bool flag7 = (((Control)(object)obj4).Enabled = flag5);
		bool flag9 = (((Control)(object)obj3).Enabled = flag7);
		bool enabled = (((Control)(object)obj2).Enabled = flag9);
		((Control)(object)obj).Enabled = enabled;
	}

	private void ULGSysAcc_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!bool.Parse(((UltraGridBase)ULGSysAcc).ActiveRow.Cells["HasSubAccount"].Value.ToString()) && ((KeyedSubObjectBase)ULGSysAcc.ActiveCell.Column).Key == "SubAccountID")
		{
			((GridItemBase)((UltraGridBase)ULGSysAcc).ActiveRow).Selected = true;
		}
	}

	public void InitGridsRelatives()
	{
		((UltraGridBase)ULGHealthInsuranceRelatives).DataSource = dtHrRelativesInclusiveHealthInsurance;
		GlobalFunctions.PrepareGrid(ULGHealthInsuranceRelatives);
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["FamilyRelativeInclusiveHealthInsuranceSettingID"].DefaultCellValue = -1;
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].Header).Caption = (GlobalVariables.IsArabic ? "صلة القرابة" : "Family Reationship");
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].ValueList = (IValueList)(object)vlFamilyRelatives;
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["FamilyRelativeID"].Width = (int)((double)((Control)(object)ULGHealthInsuranceRelatives).Width * 0.7) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["Percentage"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["Percentage"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة التأمين" : "Insurance Percentage");
		((UltraGridBase)ULGHealthInsuranceRelatives).DisplayLayout.Bands[0].Columns["Percentage"].Width = (int)((double)((Control)(object)ULGHealthInsuranceRelatives).Width * 0.3);
	}

	public void InitGridsSystemAccounts()
	{
		((UltraGridBase)ULGSysAcc).DataSource = dtSystemAccounts;
		GlobalFunctions.PrepareGrid(ULGSysAcc);
		((UltraGridBase)ULGSysAcc).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الحساب" : "AccountName");
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحساب" : "AccountNumber");
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccountID;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحساب التحليلي" : "SubAccountNumber");
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccounts;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountName"].Width = (int)((double)((Control)(object)ULGSysAcc).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGSysAcc).Width * 0.3);
		((UltraGridBase)ULGSysAcc).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGSysAcc).Width * 0.3);
	}

	private void txtInclusiveHealthInsuranceMinPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text == "" || ((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text == ".")
		{
			((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text = "0";
		}
	}

	private void txtInclusiveHealthInsuranceMaxPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text == "" || ((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text == ".")
		{
			((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text = "0";
		}
	}

	private void gbInclusiveHealthInsurance_Enter(object sender, EventArgs e)
	{
	}

	private void ULGHealthInsuranceRelatives_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGHealthInsuranceRelatives.ActiveCell.Column).Key == "Percentage")
		{
			GlobalFunctions.CheckForNumbers(ULGHealthInsuranceRelatives.ActiveCell, e);
		}
	}

	private void ULGHealthInsuranceRelatives_AfterCellUpdate(object sender, CellEventArgs e)
	{
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "Percentage" && e.Cell.Value.ToString() == "") || e.Cell.Value.ToString() == ".")
		{
			e.Cell.Value = "0";
		}
	}

	private void ULGSysOption_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGSysOption.ActiveCell.Column).Key == "OptionEnName" || (bool)((UltraGridBase)ULGSysOption).ActiveRow.Cells["ReadOnly"].Value)
		{
			((GridItemBase)((UltraGridBase)ULGSysOption).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGSysOption.ActiveCell.Column).Key == "Description")
		{
			((GridItemBase)((UltraGridBase)ULGSysOption).ActiveRow).Selected = true;
		}
	}

	private void chkInclusiveHealthInsurance_CheckedChanged(object sender, EventArgs e)
	{
		UltraTextEditor obj = txtInclusiveHealthInsuranceMinPercentage;
		UltraTextEditor obj2 = txtInclusiveHealthInsuranceMaxPercentage;
		UltraTextEditor obj3 = txtInclusiveHealthInsuranceOwnerPercentage;
		bool flag = (((Control)(object)ULGHealthInsuranceRelatives).Enabled = ((UltraToggleEditorBase)chkInclusiveHealthInsurance).Checked);
		bool flag3 = (((Control)(object)obj3).Enabled = flag);
		bool enabled = (((Control)(object)obj2).Enabled = flag3);
		((Control)(object)obj).Enabled = enabled;
	}

	private bool ValidateData()
	{
		((UltraGridBase)ULGHealthInsuranceRelatives).UpdateData();
		if (((UltraToggleEditorBase)chkInclusiveHealthInsurance).Checked)
		{
			if (decimal.Parse(((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text) >= 100m)
			{
				GlobalVariables.InformationMB.Show("الحد الاعلى لنسبة التأمين الشامل يجب الا يتخطى 100", "The Maximum Percentage Of Inclusive Health Insurance Must Not Exceed 100%");
				((TextEditorControlBase)txtInclusiveHealthInsuranceMaxPercentage).Focus();
				return false;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGHealthInsuranceRelatives).Rows).Count > 0)
			{
				decimal num = decimal.Parse(((Control)(object)txtInclusiveHealthInsuranceMaxPercentage).Text) - decimal.Parse(((Control)(object)txtInclusiveHealthInsuranceMinPercentage).Text);
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGHealthInsuranceRelatives).Rows).Count; i++)
				{
					if (decimal.Parse(((UltraGridBase)ULGHealthInsuranceRelatives).Rows[i].Cells["Percentage"].Value.ToString()) > num)
					{
						GlobalVariables.InformationMB.Show("اجمالي نسبة الاقارب تتخطى اجمالي نسبة الموظف للتأمين الشامل ", "Total Relatives Percentages Exceeds Employee Max Percentage");
						ULGHealthInsuranceRelatives.ActiveCell = ((UltraGridBase)ULGHealthInsuranceRelatives).Rows[i].Cells["Percentage"];
						ULGHealthInsuranceRelatives.PerformAction((UltraGridAction)24);
						return false;
					}
					if (dtHrRelativesInclusiveHealthInsurance.Select("FamilyRelativeID  = " + ((UltraGridBase)ULGHealthInsuranceRelatives).Rows[i].Cells["FamilyRelativeID"].Value.ToString()).Length > 1)
					{
						GlobalVariables.InformationMB.Show("لايمكن تكرار نفس صلة القرابة اكثر من مرة", "Cannot Add The Same Relative More Than Once");
						ULGHealthInsuranceRelatives.ActiveCell = ((UltraGridBase)ULGHealthInsuranceRelatives).Rows[i].Cells["FamilyRelativeID"];
						ULGHealthInsuranceRelatives.PerformAction((UltraGridAction)24);
						return false;
					}
				}
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysAcc).Rows).Count; j++)
		{
			if (bool.Parse(((UltraGridBase)ULGSysAcc).Rows[j].Cells["HasSubAccount"].Value.ToString()) && ((UltraGridBase)ULGSysAcc).Rows[j].Cells["AccountID"].Value == DBNull.Value && ((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].Value != DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء اختيار الحساب لهذا الحساب التحليلي", "Please Select An Account For This SubAccount");
				ULGSysAcc.ActiveCell = ((UltraGridBase)ULGSysAcc).Rows[j].Cells["AccountID"];
				ULGSysAcc.PerformAction((UltraGridAction)24);
				return false;
			}
			if (bool.Parse(((UltraGridBase)ULGSysAcc).Rows[j].Cells["HasSubAccount"].Value.ToString()) && ((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].ValueList != null && ((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].ValueList.ItemCount > 0 && ((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("هذا الحساب مربوط بحسابات تحليلية برجاء إختيار حساب تحليلي", "This Account Has SubAccounts Please Choose SubAccount");
				ULGSysAcc.ActiveCell = ((UltraGridBase)ULGSysAcc).Rows[j].Cells["SubAccountID"];
				ULGSysAcc.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		return true;
	}

	private void ULGSysOption_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		ULGSysOption.CellChange -= new CellEventHandler(ULGSysOption_CellChange);
		((UltraGridBase)ULGSysOption).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "OptionValue" && (e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "StokControl(Average)" || e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "LifoStockControl" || e.Cell.Row.Cells["OptionEnName"].Value.ToString() == "FIFOStockControl") && (bool)e.Cell.Value)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGSysOption).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "StokControl(Average)" || ((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "LifoStockControl" || ((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionEnName"].Value.ToString() == "FIFOStockControl")
				{
					((UltraGridBase)ULGSysOption).Rows[i].Cells["OptionValue"].Value = false;
				}
			}
			e.Cell.Value = true;
		}
		ULGSysOption.CellChange += new CellEventHandler(ULGSysOption_CellChange);
	}

	private void ULGSysAcc_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "AccountID")
		{
			int num = ((vlAccountID.SelectedItem != null) ? int.Parse(dtAccountTypes.Rows[vlAccountID.SelectedIndex]["AccountID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["SubAccountID"].ValueList = (IValueList)(object)getSubAccountValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["SubAccountID"].ValueList = null;
				e.Cell.Row.Cells["SubAccountID"].Value = DBNull.Value;
				e.Cell.Row.Cells["CostCenterID"].Value = DBNull.Value;
			}
		}
	}

	private ValueList getSubAccountValueList(int AccountID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtSubAccounts.Select("AccountID=" + AccountID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["SubAccountID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
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
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Expected O, but got Unknown
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Expected O, but got Unknown
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Expected O, but got Unknown
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Expected O, but got Unknown
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Expected O, but got Unknown
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Expected O, but got Unknown
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Expected O, but got Unknown
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Expected O, but got Unknown
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Expected O, but got Unknown
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Expected O, but got Unknown
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Expected O, but got Unknown
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Expected O, but got Unknown
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Expected O, but got Unknown
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Expected O, but got Unknown
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Expected O, but got Unknown
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Expected O, but got Unknown
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Expected O, but got Unknown
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Expected O, but got Unknown
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Expected O, but got Unknown
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Expected O, but got Unknown
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Expected O, but got Unknown
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Expected O, but got Unknown
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Expected O, but got Unknown
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Expected O, but got Unknown
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Expected O, but got Unknown
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected O, but got Unknown
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Expected O, but got Unknown
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Expected O, but got Unknown
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Expected O, but got Unknown
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Expected O, but got Unknown
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Expected O, but got Unknown
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Expected O, but got Unknown
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Expected O, but got Unknown
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Expected O, but got Unknown
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Expected O, but got Unknown
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Expected O, but got Unknown
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Expected O, but got Unknown
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Expected O, but got Unknown
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Expected O, but got Unknown
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Expected O, but got Unknown
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Expected O, but got Unknown
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Expected O, but got Unknown
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Expected O, but got Unknown
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Expected O, but got Unknown
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Expected O, but got Unknown
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Expected O, but got Unknown
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Expected O, but got Unknown
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Expected O, but got Unknown
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Expected O, but got Unknown
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Expected O, but got Unknown
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Expected O, but got Unknown
		//IL_0f8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f97: Expected O, but got Unknown
		//IL_1a87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a91: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralOptions.frmHRSetting));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		ValueListItem val22 = new ValueListItem();
		ValueListItem val23 = new ValueListItem();
		ValueListItem val24 = new ValueListItem();
		ValueListItem val25 = new ValueListItem();
		ValueListItem val26 = new ValueListItem();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		Appearance val57 = new Appearance();
		ValueListItem val58 = new ValueListItem();
		ValueListItem val59 = new ValueListItem();
		ValueListItem val60 = new ValueListItem();
		ValueListItem val61 = new ValueListItem();
		ValueListItem val62 = new ValueListItem();
		ValueListItem val63 = new ValueListItem();
		ValueListItem val64 = new ValueListItem();
		Appearance val65 = new Appearance();
		Appearance val66 = new Appearance();
		Appearance val67 = new Appearance();
		Appearance val68 = new Appearance();
		Appearance val69 = new Appearance();
		Appearance val70 = new Appearance();
		Appearance val71 = new Appearance();
		Appearance val72 = new Appearance();
		Appearance val73 = new Appearance();
		Appearance val74 = new Appearance();
		UltraTab val75 = new UltraTab();
		UltraTab val76 = new UltraTab();
		UltraTab val77 = new UltraTab();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGSysOption = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGSysAcc = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ultraLabel5 = new UltraLabel();
		this.cboExtraTimeRound = new UltraComboEditor();
		this.ultraLabel4 = new UltraLabel();
		this.UGBInssuranceSetting = new UltraGroupBox();
		this.gbInclusiveHealthInsurance = new System.Windows.Forms.GroupBox();
		this.ULGHealthInsuranceRelatives = new UltraGrid();
		this.chkInclusiveHealthInsurance = new UltraCheckEditor();
		this.lblInclusiveHealthInsuranceMinPercentage = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.lblInclusiveHealthInsuranceMaxPercentage = new UltraLabel();
		this.txtInclusiveHealthInsuranceOwnerPercentage = new UltraTextEditor();
		this.txtInclusiveHealthInsuranceMaxPercentage = new UltraTextEditor();
		this.txtInclusiveHealthInsuranceMinPercentage = new UltraTextEditor();
		this.chkSocialInssuranceValueIsTotalInssuranceSalary = new UltraCheckEditor();
		this.chkApply2020Rule = new UltraCheckEditor();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.lblBasicSalaryInssuranceMax = new UltraLabel();
		this.txtBasicSalaryInssuranceMax = new UltraTextEditor();
		this.txtVariantSalaryInssuranceMax = new UltraTextEditor();
		this.lblVariantSalaryInssuranceMax = new UltraLabel();
		this.lblBasicEmployeeInssurancePercentage = new UltraLabel();
		this.txtBasicEmployeeInssurancePercentageUnder18 = new UltraTextEditor();
		this.txtBasicEmployeeInssurancePercentageOver60 = new UltraTextEditor();
		this.txtBasicEmployeeInssurancePercentage = new UltraTextEditor();
		this.lblBasicEmployeeInssurancePercentage1 = new UltraLabel();
		this.txtBasicOwnerInssurancePercentageOver60 = new UltraTextEditor();
		this.txtBasicOwnerInssurancePercentageUnder18 = new UltraTextEditor();
		this.txtBasicOwnerInssurancePercentage = new UltraTextEditor();
		this.lblBasicOwnerInssurancePercentage = new UltraLabel();
		this.lblVariantOwnerInssurancePercentage1 = new UltraLabel();
		this.lblBasicOwnerInssurancePercentage1 = new UltraLabel();
		this.lblVariantOwnerInssurancePercentage = new UltraLabel();
		this.txtVariantEmployeeInssurancePercentageOver60 = new UltraTextEditor();
		this.txtVariantEmployeeInssurancePercentageUnder18 = new UltraTextEditor();
		this.txtVariantEmployeeInssurancePercentage = new UltraTextEditor();
		this.txtVariantOwnerInssurancePercentageOver60 = new UltraTextEditor();
		this.txtVariantOwnerInssurancePercentageUnder18 = new UltraTextEditor();
		this.txtVariantOwnerInssurancePercentage = new UltraTextEditor();
		this.lblVariantEmployeeInssurancePercentage = new UltraLabel();
		this.lblVariantEmployeeInssurancePercentage1 = new UltraLabel();
		this.lblLogo = new UltraLabel();
		this.picbCompanyLogo = new UltraPictureBox();
		this.btnImagePath = new UltraButton();
		this.cboSalaryRound = new UltraComboEditor();
		this.lblBasicAnnualIncreasePercentage1 = new UltraLabel();
		this.lblBasicAnnualIncreasePercentage = new UltraLabel();
		this.txtBasicAnnualIncreasePercentage = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.lblMonthEndDate1 = new UltraLabel();
		this.lblMonthEndDate = new UltraLabel();
		this.txtMonthEndDate = new UltraTextEditor();
		this.lblTitle2 = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.tcSystemDefaults = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGSysOption).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGSysAcc).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboExtraTimeRound).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBInssuranceSetting).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).SuspendLayout();
		this.gbInclusiveHealthInsurance.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGHealthInsuranceRelatives).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInclusiveHealthInsurance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceOwnerPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceMaxPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceMinPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSocialInssuranceValueIsTotalInssuranceSalary).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkApply2020Rule).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicSalaryInssuranceMax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantSalaryInssuranceMax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicEmployeeInssurancePercentageUnder18).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicEmployeeInssurancePercentageOver60).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicEmployeeInssurancePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicOwnerInssurancePercentageOver60).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicOwnerInssurancePercentageUnder18).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicOwnerInssurancePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantEmployeeInssurancePercentageOver60).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantEmployeeInssurancePercentageUnder18).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantEmployeeInssurancePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantOwnerInssurancePercentageOver60).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantOwnerInssurancePercentageUnder18).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantOwnerInssurancePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryRound).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicAnnualIncreasePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMonthEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tcSystemDefaults).BeginInit();
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGSysOption);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGSysOption, "ULGSysOption");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGSysOption).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGSysOption).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGSysOption).Name = "ULGSysOption";
		this.ULGSysOption.AfterEnterEditMode += new System.EventHandler(ULGSysOption_AfterEnterEditMode);
		this.ULGSysOption.CellChange += new CellEventHandler(ULGSysOption_CellChange);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGSysAcc);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGSysAcc, "ULGSysAcc");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val11).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val11, "appearance11");
		((SpecialBoxBase)((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val12;
		((SpecialBoxBase)((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val16;
		((AppearanceBase)val17).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val17, "appearance17");
		((AppearanceBase)val17).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val18).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val18).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val18).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val19).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGSysAcc).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGSysAcc).Name = "ULGSysAcc";
		this.ULGSysAcc.AfterEnterEditMode += new System.EventHandler(ULGSysAcc_AfterEnterEditMode);
		this.ULGSysAcc.CellListSelect += new CellEventHandler(ULGSysAcc_CellListSelect);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboExtraTimeRound);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblLogo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.picbCompanyLogo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.btnImagePath);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.cboSalaryRound);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicAnnualIncreasePercentage1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicAnnualIncreasePercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicAnnualIncreasePercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblMonthEndDate1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblMonthEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtMonthEndDate);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val21, "appearance63");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val21;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.cboExtraTimeRound, "cboExtraTimeRound");
		val22.DataValue = 1;
		resources.ApplyResources(val22, "valueListItem1");
		((SubObjectBase)val22).ForceApplyResources = "";
		val23.DataValue = 5;
		resources.ApplyResources(val23, "valueListItem2");
		((SubObjectBase)val23).ForceApplyResources = "";
		val24.DataValue = 10;
		resources.ApplyResources(val24, "valueListItem3");
		((SubObjectBase)val24).ForceApplyResources = "";
		val25.DataValue = 15;
		resources.ApplyResources(val25, "valueListItem4");
		((SubObjectBase)val25).ForceApplyResources = "";
		val26.DataValue = 30;
		resources.ApplyResources(val26, "valueListItem5");
		((SubObjectBase)val26).ForceApplyResources = "";
		this.cboExtraTimeRound.Items.AddRange((ValueListItem[])(object)new ValueListItem[5] { val22, val23, val24, val25, val26 });
		((System.Windows.Forms.Control)(object)this.cboExtraTimeRound).Name = "cboExtraTimeRound";
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val27, "appearance64");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val27;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.UGBInssuranceSetting, "UGBInssuranceSetting");
		this.UGBInssuranceSetting.CaptionAlignment = (GroupBoxCaptionAlignment)2;
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add(this.gbInclusiveHealthInsurance);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.chkSocialInssuranceValueIsTotalInssuranceSalary);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.chkApply2020Rule);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicSalaryInssuranceMax);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicSalaryInssuranceMax);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantSalaryInssuranceMax);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblVariantSalaryInssuranceMax);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicEmployeeInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentageUnder18);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentageOver60);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicEmployeeInssurancePercentage1);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentageOver60);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentageUnder18);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicOwnerInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblVariantOwnerInssurancePercentage1);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblBasicOwnerInssurancePercentage1);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblVariantOwnerInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentageOver60);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentageUnder18);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentageOver60);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentageUnder18);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblVariantEmployeeInssurancePercentage);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Controls.Add((System.Windows.Forms.Control)(object)this.lblVariantEmployeeInssurancePercentage1);
		this.UGBInssuranceSetting.HeaderPosition = (GroupBoxHeaderPosition)3;
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).Name = "UGBInssuranceSetting";
		resources.ApplyResources(this.gbInclusiveHealthInsurance, "gbInclusiveHealthInsurance");
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.ULGHealthInsuranceRelatives);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.chkInclusiveHealthInsurance);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.lblInclusiveHealthInsuranceMinPercentage);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.lblInclusiveHealthInsuranceMaxPercentage);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceOwnerPercentage);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceMaxPercentage);
		this.gbInclusiveHealthInsurance.Controls.Add((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceMinPercentage);
		this.gbInclusiveHealthInsurance.Name = "gbInclusiveHealthInsurance";
		this.gbInclusiveHealthInsurance.TabStop = false;
		this.gbInclusiveHealthInsurance.Enter += new System.EventHandler(gbInclusiveHealthInsurance_Enter);
		resources.ApplyResources(this.ULGHealthInsuranceRelatives, "ULGHealthInsuranceRelatives");
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val28).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val28, "appearance23");
		((SpecialBoxBase)((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val28;
		((AppearanceBase)val29).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val29, "appearance24");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val29;
		((SpecialBoxBase)((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val30).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val30).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val30).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val30, "appearance25");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val31).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val31, "appearance26");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val32).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val32, "appearance27");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val33, "appearance28");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val33;
		((AppearanceBase)val34).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val34, "appearance29");
		((AppearanceBase)val34).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val35).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val35).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val35).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val35).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val35, "appearance30");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val36).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val36, "appearance31");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val36;
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val37, "appearance32");
		((UltraGridBase)this.ULGHealthInsuranceRelatives).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val37;
		((System.Windows.Forms.Control)(object)this.ULGHealthInsuranceRelatives).Name = "ULGHealthInsuranceRelatives";
		this.ULGHealthInsuranceRelatives.AfterCellUpdate += new CellEventHandler(ULGHealthInsuranceRelatives_AfterCellUpdate);
		((System.Windows.Forms.Control)(object)this.ULGHealthInsuranceRelatives).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGHealthInsuranceRelatives_KeyPress);
		resources.ApplyResources(this.chkInclusiveHealthInsurance, "chkInclusiveHealthInsurance");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val38).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val38, "appearance65");
		((UltraToggleEditorBase)this.chkInclusiveHealthInsurance).Appearance = (AppearanceBase)(object)val38;
		((System.Windows.Forms.Control)(object)this.chkInclusiveHealthInsurance).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkInclusiveHealthInsurance).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkInclusiveHealthInsurance).Name = "chkInclusiveHealthInsurance";
		((UltraToggleEditorBase)this.chkInclusiveHealthInsurance).CheckedChanged += new System.EventHandler(chkInclusiveHealthInsurance_CheckedChanged);
		resources.ApplyResources(this.lblInclusiveHealthInsuranceMinPercentage, "lblInclusiveHealthInsuranceMinPercentage");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val39, "appearance66");
		((AppearanceBase)val39).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblInclusiveHealthInsuranceMinPercentage).Appearance = (AppearanceBase)(object)val39;
		this.lblInclusiveHealthInsuranceMinPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInclusiveHealthInsuranceMinPercentage).Name = "lblInclusiveHealthInsuranceMinPercentage";
		((ControlBase)this.lblInclusiveHealthInsuranceMinPercentage).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val40).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val40, "appearance67");
		((AppearanceBase)val40).TextTrimming = (TextTrimming)6;
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val40;
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.lblInclusiveHealthInsuranceMaxPercentage, "lblInclusiveHealthInsuranceMaxPercentage");
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val41, "appearance68");
		((AppearanceBase)val41).TextTrimming = (TextTrimming)6;
		((ControlBase)this.lblInclusiveHealthInsuranceMaxPercentage).Appearance = (AppearanceBase)(object)val41;
		this.lblInclusiveHealthInsuranceMaxPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInclusiveHealthInsuranceMaxPercentage).Name = "lblInclusiveHealthInsuranceMaxPercentage";
		((ControlBase)this.lblInclusiveHealthInsuranceMaxPercentage).WrapText = false;
		resources.ApplyResources(this.txtInclusiveHealthInsuranceOwnerPercentage, "txtInclusiveHealthInsuranceOwnerPercentage");
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceOwnerPercentage).Name = "txtInclusiveHealthInsuranceOwnerPercentage";
		((TextEditorControlBase)this.txtInclusiveHealthInsuranceOwnerPercentage).ValueChanged += new System.EventHandler(txtInclusiveHealthInsuranceMaxPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceOwnerPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtInclusiveHealthInsuranceMaxPercentage, "txtInclusiveHealthInsuranceMaxPercentage");
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceMaxPercentage).Name = "txtInclusiveHealthInsuranceMaxPercentage";
		((TextEditorControlBase)this.txtInclusiveHealthInsuranceMaxPercentage).ValueChanged += new System.EventHandler(txtInclusiveHealthInsuranceMaxPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceMaxPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtInclusiveHealthInsuranceMinPercentage, "txtInclusiveHealthInsuranceMinPercentage");
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceMinPercentage).Name = "txtInclusiveHealthInsuranceMinPercentage";
		((TextEditorControlBase)this.txtInclusiveHealthInsuranceMinPercentage).ValueChanged += new System.EventHandler(txtInclusiveHealthInsuranceMinPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtInclusiveHealthInsuranceMinPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.chkSocialInssuranceValueIsTotalInssuranceSalary, "chkSocialInssuranceValueIsTotalInssuranceSalary");
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val42, "appearance69");
		((UltraToggleEditorBase)this.chkSocialInssuranceValueIsTotalInssuranceSalary).Appearance = (AppearanceBase)(object)val42;
		((System.Windows.Forms.Control)(object)this.chkSocialInssuranceValueIsTotalInssuranceSalary).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkSocialInssuranceValueIsTotalInssuranceSalary).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkSocialInssuranceValueIsTotalInssuranceSalary).Name = "chkSocialInssuranceValueIsTotalInssuranceSalary";
		resources.ApplyResources(this.chkApply2020Rule, "chkApply2020Rule");
		((AppearanceBase)val43).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val43).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val43, "appearance70");
		((UltraToggleEditorBase)this.chkApply2020Rule).Appearance = (AppearanceBase)(object)val43;
		((System.Windows.Forms.Control)(object)this.chkApply2020Rule).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkApply2020Rule).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkApply2020Rule).Name = "chkApply2020Rule";
		((UltraToggleEditorBase)this.chkApply2020Rule).CheckedChanged += new System.EventHandler(chkApply2020Rule_CheckedChanged);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val44).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val44, "appearance71");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val44;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val45).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val45, "appearance72");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val45;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.lblBasicSalaryInssuranceMax, "lblBasicSalaryInssuranceMax");
		((AppearanceBase)val46).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val46, "appearance73");
		((ControlBase)this.lblBasicSalaryInssuranceMax).Appearance = (AppearanceBase)(object)val46;
		this.lblBasicSalaryInssuranceMax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicSalaryInssuranceMax).Name = "lblBasicSalaryInssuranceMax";
		((ControlBase)this.lblBasicSalaryInssuranceMax).WrapText = false;
		resources.ApplyResources(this.txtBasicSalaryInssuranceMax, "txtBasicSalaryInssuranceMax");
		((System.Windows.Forms.Control)(object)this.txtBasicSalaryInssuranceMax).Name = "txtBasicSalaryInssuranceMax";
		((System.Windows.Forms.Control)(object)this.txtBasicSalaryInssuranceMax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVariantSalaryInssuranceMax, "txtVariantSalaryInssuranceMax");
		((System.Windows.Forms.Control)(object)this.txtVariantSalaryInssuranceMax).Name = "txtVariantSalaryInssuranceMax";
		((System.Windows.Forms.Control)(object)this.txtVariantSalaryInssuranceMax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblVariantSalaryInssuranceMax, "lblVariantSalaryInssuranceMax");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val47, "appearance74");
		((ControlBase)this.lblVariantSalaryInssuranceMax).Appearance = (AppearanceBase)(object)val47;
		this.lblVariantSalaryInssuranceMax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVariantSalaryInssuranceMax).Name = "lblVariantSalaryInssuranceMax";
		((ControlBase)this.lblVariantSalaryInssuranceMax).WrapText = false;
		resources.ApplyResources(this.lblBasicEmployeeInssurancePercentage, "lblBasicEmployeeInssurancePercentage");
		((AppearanceBase)val48).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val48, "appearance75");
		((ControlBase)this.lblBasicEmployeeInssurancePercentage).Appearance = (AppearanceBase)(object)val48;
		this.lblBasicEmployeeInssurancePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicEmployeeInssurancePercentage).Name = "lblBasicEmployeeInssurancePercentage";
		((ControlBase)this.lblBasicEmployeeInssurancePercentage).WrapText = false;
		resources.ApplyResources(this.txtBasicEmployeeInssurancePercentageUnder18, "txtBasicEmployeeInssurancePercentageUnder18");
		((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentageUnder18).Name = "txtBasicEmployeeInssurancePercentageUnder18";
		((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentageUnder18).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtBasicEmployeeInssurancePercentageOver60, "txtBasicEmployeeInssurancePercentageOver60");
		((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentageOver60).Name = "txtBasicEmployeeInssurancePercentageOver60";
		((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentageOver60).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtBasicEmployeeInssurancePercentage, "txtBasicEmployeeInssurancePercentage");
		((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentage).Name = "txtBasicEmployeeInssurancePercentage";
		((System.Windows.Forms.Control)(object)this.txtBasicEmployeeInssurancePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblBasicEmployeeInssurancePercentage1, "lblBasicEmployeeInssurancePercentage1");
		((AppearanceBase)val49).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val49, "appearance76");
		((ControlBase)this.lblBasicEmployeeInssurancePercentage1).Appearance = (AppearanceBase)(object)val49;
		this.lblBasicEmployeeInssurancePercentage1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicEmployeeInssurancePercentage1).Name = "lblBasicEmployeeInssurancePercentage1";
		resources.ApplyResources(this.txtBasicOwnerInssurancePercentageOver60, "txtBasicOwnerInssurancePercentageOver60");
		((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentageOver60).Name = "txtBasicOwnerInssurancePercentageOver60";
		((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentageOver60).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtBasicOwnerInssurancePercentageUnder18, "txtBasicOwnerInssurancePercentageUnder18");
		((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentageUnder18).Name = "txtBasicOwnerInssurancePercentageUnder18";
		((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentageUnder18).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtBasicOwnerInssurancePercentage, "txtBasicOwnerInssurancePercentage");
		((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentage).Name = "txtBasicOwnerInssurancePercentage";
		((System.Windows.Forms.Control)(object)this.txtBasicOwnerInssurancePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblBasicOwnerInssurancePercentage, "lblBasicOwnerInssurancePercentage");
		((AppearanceBase)val50).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val50, "appearance77");
		((ControlBase)this.lblBasicOwnerInssurancePercentage).Appearance = (AppearanceBase)(object)val50;
		this.lblBasicOwnerInssurancePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicOwnerInssurancePercentage).Name = "lblBasicOwnerInssurancePercentage";
		((ControlBase)this.lblBasicOwnerInssurancePercentage).WrapText = false;
		resources.ApplyResources(this.lblVariantOwnerInssurancePercentage1, "lblVariantOwnerInssurancePercentage1");
		((AppearanceBase)val51).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val51, "appearance78");
		((ControlBase)this.lblVariantOwnerInssurancePercentage1).Appearance = (AppearanceBase)(object)val51;
		this.lblVariantOwnerInssurancePercentage1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVariantOwnerInssurancePercentage1).Name = "lblVariantOwnerInssurancePercentage1";
		resources.ApplyResources(this.lblBasicOwnerInssurancePercentage1, "lblBasicOwnerInssurancePercentage1");
		((AppearanceBase)val52).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val52, "appearance79");
		((ControlBase)this.lblBasicOwnerInssurancePercentage1).Appearance = (AppearanceBase)(object)val52;
		this.lblBasicOwnerInssurancePercentage1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicOwnerInssurancePercentage1).Name = "lblBasicOwnerInssurancePercentage1";
		resources.ApplyResources(this.lblVariantOwnerInssurancePercentage, "lblVariantOwnerInssurancePercentage");
		((AppearanceBase)val53).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val53, "appearance80");
		((ControlBase)this.lblVariantOwnerInssurancePercentage).Appearance = (AppearanceBase)(object)val53;
		this.lblVariantOwnerInssurancePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVariantOwnerInssurancePercentage).Name = "lblVariantOwnerInssurancePercentage";
		((ControlBase)this.lblVariantOwnerInssurancePercentage).WrapText = false;
		resources.ApplyResources(this.txtVariantEmployeeInssurancePercentageOver60, "txtVariantEmployeeInssurancePercentageOver60");
		((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentageOver60).Name = "txtVariantEmployeeInssurancePercentageOver60";
		((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentageOver60).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVariantEmployeeInssurancePercentageUnder18, "txtVariantEmployeeInssurancePercentageUnder18");
		((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentageUnder18).Name = "txtVariantEmployeeInssurancePercentageUnder18";
		((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentageUnder18).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVariantEmployeeInssurancePercentage, "txtVariantEmployeeInssurancePercentage");
		((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentage).Name = "txtVariantEmployeeInssurancePercentage";
		((System.Windows.Forms.Control)(object)this.txtVariantEmployeeInssurancePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVariantOwnerInssurancePercentageOver60, "txtVariantOwnerInssurancePercentageOver60");
		((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentageOver60).Name = "txtVariantOwnerInssurancePercentageOver60";
		((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentageOver60).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVariantOwnerInssurancePercentageUnder18, "txtVariantOwnerInssurancePercentageUnder18");
		((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentageUnder18).Name = "txtVariantOwnerInssurancePercentageUnder18";
		((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentageUnder18).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtVariantOwnerInssurancePercentage, "txtVariantOwnerInssurancePercentage");
		((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentage).Name = "txtVariantOwnerInssurancePercentage";
		((System.Windows.Forms.Control)(object)this.txtVariantOwnerInssurancePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblVariantEmployeeInssurancePercentage, "lblVariantEmployeeInssurancePercentage");
		((AppearanceBase)val54).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val54, "appearance81");
		((ControlBase)this.lblVariantEmployeeInssurancePercentage).Appearance = (AppearanceBase)(object)val54;
		this.lblVariantEmployeeInssurancePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVariantEmployeeInssurancePercentage).Name = "lblVariantEmployeeInssurancePercentage";
		((ControlBase)this.lblVariantEmployeeInssurancePercentage).WrapText = false;
		resources.ApplyResources(this.lblVariantEmployeeInssurancePercentage1, "lblVariantEmployeeInssurancePercentage1");
		((AppearanceBase)val55).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val55, "appearance82");
		((ControlBase)this.lblVariantEmployeeInssurancePercentage1).Appearance = (AppearanceBase)(object)val55;
		this.lblVariantEmployeeInssurancePercentage1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVariantEmployeeInssurancePercentage1).Name = "lblVariantEmployeeInssurancePercentage1";
		resources.ApplyResources(this.lblLogo, "lblLogo");
		((AppearanceBase)val56).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val56, "appearance83");
		((ControlBase)this.lblLogo).Appearance = (AppearanceBase)(object)val56;
		this.lblLogo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLogo).Name = "lblLogo";
		((ControlBase)this.lblLogo).WrapText = false;
		resources.ApplyResources(this.picbCompanyLogo, "picbCompanyLogo");
		((AppearanceBase)val57).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val57, "appearance84");
		this.picbCompanyLogo.Appearance = (AppearanceBase)(object)val57;
		this.picbCompanyLogo.BorderShadowColor = System.Drawing.Color.Empty;
		this.picbCompanyLogo.BorderStyle = (UIElementBorderStyle)2;
		this.picbCompanyLogo.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.picbCompanyLogo).Name = "picbCompanyLogo";
		((System.Windows.Forms.Control)(object)this.picbCompanyLogo).MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(picbCompanyLogo_MouseDoubleClick);
		resources.ApplyResources(this.btnImagePath, "btnImagePath");
		((System.Windows.Forms.Control)(object)this.btnImagePath).Name = "btnImagePath";
		((System.Windows.Forms.Control)(object)this.btnImagePath).Click += new System.EventHandler(btnImagePath_Click);
		resources.ApplyResources(this.cboSalaryRound, "cboSalaryRound");
		val58.DataValue = 0;
		resources.ApplyResources(val58, "valueListItem6");
		((SubObjectBase)val58).ForceApplyResources = "";
		val59.DataValue = 1;
		resources.ApplyResources(val59, "valueListItem7");
		((SubObjectBase)val59).ForceApplyResources = "";
		val60.DataValue = 5;
		resources.ApplyResources(val60, "valueListItem8");
		((SubObjectBase)val60).ForceApplyResources = "";
		val61.DataValue = 10;
		resources.ApplyResources(val61, "valueListItem9");
		((SubObjectBase)val61).ForceApplyResources = "";
		val62.DataValue = 20;
		resources.ApplyResources(val62, "valueListItem10");
		((SubObjectBase)val62).ForceApplyResources = "";
		val63.DataValue = 50;
		resources.ApplyResources(val63, "valueListItem11");
		((SubObjectBase)val63).ForceApplyResources = "";
		val64.DataValue = 100;
		resources.ApplyResources(val64, "valueListItem12");
		((SubObjectBase)val64).ForceApplyResources = "";
		this.cboSalaryRound.Items.AddRange((ValueListItem[])(object)new ValueListItem[7] { val58, val59, val60, val61, val62, val63, val64 });
		((System.Windows.Forms.Control)(object)this.cboSalaryRound).Name = "cboSalaryRound";
		resources.ApplyResources(this.lblBasicAnnualIncreasePercentage1, "lblBasicAnnualIncreasePercentage1");
		((AppearanceBase)val65).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val65, "appearance85");
		((ControlBase)this.lblBasicAnnualIncreasePercentage1).Appearance = (AppearanceBase)(object)val65;
		this.lblBasicAnnualIncreasePercentage1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicAnnualIncreasePercentage1).Name = "lblBasicAnnualIncreasePercentage1";
		resources.ApplyResources(this.lblBasicAnnualIncreasePercentage, "lblBasicAnnualIncreasePercentage");
		((AppearanceBase)val66).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val66, "appearance86");
		((ControlBase)this.lblBasicAnnualIncreasePercentage).Appearance = (AppearanceBase)(object)val66;
		this.lblBasicAnnualIncreasePercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBasicAnnualIncreasePercentage).Name = "lblBasicAnnualIncreasePercentage";
		((ControlBase)this.lblBasicAnnualIncreasePercentage).WrapText = false;
		resources.ApplyResources(this.txtBasicAnnualIncreasePercentage, "txtBasicAnnualIncreasePercentage");
		((System.Windows.Forms.Control)(object)this.txtBasicAnnualIncreasePercentage).Name = "txtBasicAnnualIncreasePercentage";
		((System.Windows.Forms.Control)(object)this.txtBasicAnnualIncreasePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val67).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val67, "appearance87");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val67;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.lblMonthEndDate1, "lblMonthEndDate1");
		((AppearanceBase)val68).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val68, "appearance88");
		((ControlBase)this.lblMonthEndDate1).Appearance = (AppearanceBase)(object)val68;
		this.lblMonthEndDate1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMonthEndDate1).Name = "lblMonthEndDate1";
		((ControlBase)this.lblMonthEndDate1).WrapText = false;
		resources.ApplyResources(this.lblMonthEndDate, "lblMonthEndDate");
		((AppearanceBase)val69).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val69, "appearance89");
		((ControlBase)this.lblMonthEndDate).Appearance = (AppearanceBase)(object)val69;
		this.lblMonthEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMonthEndDate).Name = "lblMonthEndDate";
		((ControlBase)this.lblMonthEndDate).WrapText = false;
		resources.ApplyResources(this.txtMonthEndDate, "txtMonthEndDate");
		((System.Windows.Forms.Control)(object)this.txtMonthEndDate).Name = "txtMonthEndDate";
		((System.Windows.Forms.Control)(object)this.txtMonthEndDate).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val70).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val70).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val70).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val70, "appearance90");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val70;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val71).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val71).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val71).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val71, "appearance91");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val71;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val72).Image = resources.GetObject("appearance92.Image");
		resources.ApplyResources(val72, "appearance92");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val72;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val73).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val73, "appearance93");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val73;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.tcSystemDefaults, "tcSystemDefaults");
		((AppearanceBase)val74).ForeColor = System.Drawing.Color.Maroon;
		resources.ApplyResources(val74, "appearance62");
		((AppearanceBase)val74).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tcSystemDefaults).Appearance = (AppearanceBase)(object)val74;
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).Name = "tcSystemDefaults";
		((UltraTabControlBase)this.tcSystemDefaults).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tcSystemDefaults).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val75).Key = "Options";
		val75.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val75, "ultraTab3");
		((SubObjectBase)val75).ForceApplyResources = "";
		((KeyedSubObjectBase)val76).Key = "System Accounts";
		val76.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val76, "ultraTab1");
		((SubObjectBase)val76).ForceApplyResources = "";
		((KeyedSubObjectBase)val77).Key = "Settings";
		val77.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val77, "ultraTab2");
		((SubObjectBase)val77).ForceApplyResources = "";
		((UltraTabControlBase)this.tcSystemDefaults).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val75, val76, val77 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tcSystemDefaults);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmHRSetting";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tcSystemDefaults, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGSysOption).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGSysAcc).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboExtraTimeRound).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBInssuranceSetting).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBInssuranceSetting).PerformLayout();
		this.gbInclusiveHealthInsurance.ResumeLayout(false);
		this.gbInclusiveHealthInsurance.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGHealthInsuranceRelatives).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInclusiveHealthInsurance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceOwnerPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceMaxPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInclusiveHealthInsuranceMinPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSocialInssuranceValueIsTotalInssuranceSalary).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkApply2020Rule).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicSalaryInssuranceMax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantSalaryInssuranceMax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicEmployeeInssurancePercentageUnder18).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicEmployeeInssurancePercentageOver60).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicEmployeeInssurancePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicOwnerInssurancePercentageOver60).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicOwnerInssurancePercentageUnder18).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicOwnerInssurancePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantEmployeeInssurancePercentageOver60).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantEmployeeInssurancePercentageUnder18).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantEmployeeInssurancePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantOwnerInssurancePercentageOver60).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantOwnerInssurancePercentageUnder18).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVariantOwnerInssurancePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalaryRound).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBasicAnnualIncreasePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMonthEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tcSystemDefaults).EndInit();
		((System.Windows.Forms.Control)(object)this.tcSystemDefaults).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
