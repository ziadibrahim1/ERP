using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.MasterData;

public class frmCompanies : frmGrid
{
	private bool IsLogoChanged = false;

	private bool IsFooterChanged = false;

	private bool IsStampChaned = false;

	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraPictureBox pbStampLogo;

	private UltraButton btnStampLogo;

	private UltraLabel lblStampLogo;

	private UltraLabel lblFooterLogo;

	private UltraButton btnFooterPath;

	private UltraPictureBox picbFooterLogo;

	private UltraLabel lblLogo;

	private UltraPictureBox picbLogo;

	private UltraButton btnLogoPath;

	private OpenFileDialog ofdPicture;

	private UltraLabel lblMessage;

	private UltraTextEditor txtMessage;

	private UltraLabel lblMin;

	private UltraTextEditor txtFax;

	private UltraLabel lblMax;

	private UltraTextEditor txtTel;

	private UltraLabel lblEMail;

	private UltraTextEditor txtEMail;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtCompanyCode;

	private UltraLabel lblAddress;

	private UltraTextEditor txtAddress;

	private UltraLabel lblInvMsg1;

	private UltraTextEditor txtInvoiceMsg1;

	private UltraTextEditor txtInvoiceMsg2;

	private UltraLabel lblInvoiceMsg2;

	private UltraLabel lblTRN;

	private UltraTextEditor txtTRN;

	public frmCompanies()
	{
		InitializeComponent();
		TableName = "MS_Companies";
		IDCol = "CompanyID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((Control)(object)btnFooterPath).Enabled = !NavMode;
		((Control)(object)btnLogoPath).Enabled = !NavMode;
		((Control)(object)btnStampLogo).Enabled = !NavMode;
		((EditorButtonControlBase)txtCompanyCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTRN).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMessage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInvoiceMsg1).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInvoiceMsg2).ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtCompanyCode).Text = (Adding ? Companies.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)txtMessage).Clear();
		((TextEditorControlBase)txtInvoiceMsg1).Clear();
		((TextEditorControlBase)txtInvoiceMsg2).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtFax).Clear();
		((TextEditorControlBase)txtTel).Clear();
		((TextEditorControlBase)txtAddress).Clear();
		picbFooterLogo.Image = null;
		picbLogo.Image = null;
		pbStampLogo.Image = null;
	}

	public override void FillData()
	{
		dataTable = Companies.Select_WithoutPic("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyCode"].Header).Caption = (GlobalVariables.IsArabic ? "الرمز" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CompanyNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxRegistrationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التسجيل الضريبي" : "Tax Reg. No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxRegistrationNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxRegistrationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Message"].Header).Caption = (GlobalVariables.IsArabic ? "الرساله " : "Message");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Message"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Message"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceMessage1"].Header).Caption = (GlobalVariables.IsArabic ? "رسالة الفاتورة" : "Invoice Message1");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceMessage1"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceMessage1"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceMessage2"].Header).Caption = (GlobalVariables.IsArabic ? "الرساله " : "Invoice Message2");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceMessage2"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceMessage2"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Header).Caption = (GlobalVariables.IsArabic ? "التليفون" : "Tel");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Tel"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fax"].Header).Caption = (GlobalVariables.IsArabic ? "الفاكس" : "Fax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fax"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Email"].Header).Caption = (GlobalVariables.IsArabic ? "البريد الالكتروني" : "Email");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Email"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Email"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void AfterRowActivate()
	{
		string iD = ((UltraGridBase)ULGData).ActiveRow.Cells["CompanyID"].Value.ToString();
		DataTable dataTable = Companies.Select(iD, GlobalVariables.BranchIDs, GlobalVariables.IsArabic.ToString(), IsFromServer: true);
		picbLogo.Image = ((dataTable.Rows[0]["Logo"] == DBNull.Value) ? null : GetImage((byte[])dataTable.Rows[0]["Logo"]));
		picbLogo.ScaleImage = (ScaleImage)1;
		picbFooterLogo.Image = ((dataTable.Rows[0]["FooterLogo"] == DBNull.Value) ? null : GetImage((byte[])dataTable.Rows[0]["FooterLogo"]));
		picbFooterLogo.ScaleImage = (ScaleImage)1;
		pbStampLogo.Image = ((dataTable.Rows[0]["StampLogo"] == DBNull.Value) ? null : GetImage((byte[])dataTable.Rows[0]["StampLogo"]));
		pbStampLogo.ScaleImage = (ScaleImage)1;
		IsLogoChanged = false;
		IsFooterChanged = false;
		IsStampChaned = false;
		((Control)(object)txtCompanyCode).Text = dataTable.Rows[0]["CompanyCode"].ToString();
		((Control)(object)txtArabicName).Text = dataTable.Rows[0]["CompanyNameAr"].ToString();
		((Control)(object)txtEnglishName).Text = dataTable.Rows[0]["CompanyNameEn"].ToString();
		((Control)(object)txtMessage).Text = dataTable.Rows[0]["Message"].ToString();
		((Control)(object)txtTel).Text = dataTable.Rows[0]["Tel"].ToString();
		((Control)(object)txtFax).Text = dataTable.Rows[0]["Fax"].ToString();
		((Control)(object)txtEMail).Text = dataTable.Rows[0]["EMail"].ToString();
		((Control)(object)txtAddress).Text = dataTable.Rows[0]["Address"].ToString();
		((Control)(object)txtTRN).Text = dataTable.Rows[0]["TaxRegistrationNo"].ToString();
		((Control)(object)txtInvoiceMsg1).Text = dataTable.Rows[0]["InvoiceMessage1"].ToString();
		((Control)(object)txtInvoiceMsg2).Text = dataTable.Rows[0]["InvoiceMessage2"].ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCompanyCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الرمز", "Please Enter Code");
			((TextEditorControlBase)txtCompanyCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم  بالعربية", "Please Enter Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		int num = 0;
		num = Companies.Insert_Update("-1", ((Control)(object)txtCompanyCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (((Control)(object)txtTRN).Text == "") ? "Null" : ((Control)(object)txtTRN).Text, (((Control)(object)txtMessage).Text == "") ? "Null" : ((Control)(object)txtMessage).Text, (((Control)(object)txtTel).Text == "") ? "Null" : ((Control)(object)txtTel).Text, (((Control)(object)txtFax).Text == "") ? "Null" : ((Control)(object)txtFax).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtInvoiceMsg1).Text == "") ? "Null" : ((Control)(object)txtInvoiceMsg1).Text, (((Control)(object)txtInvoiceMsg2).Text == "") ? "Null" : ((Control)(object)txtInvoiceMsg2).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		if (IsLogoChanged)
		{
			if (picbLogo.Image != null)
			{
				Companies.UpdateLogo(num, (Image)picbLogo.Image);
				IsLogoChanged = false;
			}
			else
			{
				Main.ExecuteNonQuery("Update MS_Companies Set Logo = null");
			}
		}
		if (IsFooterChanged)
		{
			if (picbFooterLogo.Image != null)
			{
				Companies.UpdateFooterLogo(num, (Image)picbFooterLogo.Image);
				IsFooterChanged = false;
			}
			else
			{
				Main.ExecuteNonQuery("Update MS_Companies Set FooterLogo = null");
			}
		}
		if (IsStampChaned)
		{
			if (pbStampLogo.Image != null)
			{
				Companies.UpdateStampLogo(num, (Image)pbStampLogo.Image);
				IsStampChaned = false;
			}
			else
			{
				Main.ExecuteNonQuery("Update MS_Companies Set StampLogo = null");
			}
		}
	}

	public override void UpdateData()
	{
		int num = 0;
		num = Companies.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["CompanyID"].Value.ToString(), ((Control)(object)txtCompanyCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (((Control)(object)txtTRN).Text == "") ? "Null" : ((Control)(object)txtTRN).Text, (((Control)(object)txtMessage).Text == "") ? "Null" : ((Control)(object)txtMessage).Text, (((Control)(object)txtTel).Text == "") ? "Null" : ((Control)(object)txtTel).Text, (((Control)(object)txtFax).Text == "") ? "Null" : ((Control)(object)txtFax).Text, (((Control)(object)txtEMail).Text == "") ? "Null" : ((Control)(object)txtEMail).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, (((Control)(object)txtInvoiceMsg1).Text == "") ? "Null" : ((Control)(object)txtInvoiceMsg1).Text, (((Control)(object)txtInvoiceMsg2).Text == "") ? "Null" : ((Control)(object)txtInvoiceMsg2).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		if (IsLogoChanged)
		{
			if (picbLogo.Image != null)
			{
				Companies.UpdateLogo(num, (Image)picbLogo.Image);
				IsLogoChanged = false;
			}
			else
			{
				Main.ExecuteNonQuery("Update MS_Companies Set Logo = null");
			}
		}
		if (IsFooterChanged)
		{
			if (picbFooterLogo.Image != null)
			{
				Companies.UpdateFooterLogo(num, (Image)picbFooterLogo.Image);
				IsFooterChanged = false;
			}
			else
			{
				Main.ExecuteNonQuery("Update MS_Companies Set FooterLogo = null");
			}
		}
		if (IsStampChaned)
		{
			if (pbStampLogo.Image != null)
			{
				Companies.UpdateStampLogo(num, (Image)pbStampLogo.Image);
				IsStampChaned = false;
			}
			else
			{
				Main.ExecuteNonQuery("Update MS_Companies Set StampLogo = null");
			}
		}
	}

	public override void btnDeleteClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells[IDCol].Value.ToString() == "1")
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف الشركة", "Can not Delete This Company ");
		}
		else
		{
			base.btnDeleteClick();
		}
	}

	public override void DeleteData()
	{
		Companies.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["CompanyID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void btnImagePath_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			picbLogo.Image = Image.FromFile(ofdPicture.FileName);
			IsLogoChanged = true;
		}
	}

	private void btnFooterPath_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			picbFooterLogo.Image = Image.FromFile(ofdPicture.FileName);
			IsFooterChanged = true;
		}
	}

	private void btnWaterMark_Click(object sender, EventArgs e)
	{
		if (ofdPicture.ShowDialog() == DialogResult.OK)
		{
			pbStampLogo.Image = Image.FromFile(ofdPicture.FileName);
			IsStampChaned = true;
		}
	}

	private Image GetImage(byte[] p)
	{
		MemoryStream stream = new MemoryStream(p);
		return Image.FromStream(stream);
	}

	private void frmCompanies_Load(object sender, EventArgs e)
	{
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
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Expected O, but got Unknown
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Expected O, but got Unknown
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Expected O, but got Unknown
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmCompanies));
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
		Appearance val22 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.pbStampLogo = new UltraPictureBox();
		this.btnStampLogo = new UltraButton();
		this.lblStampLogo = new UltraLabel();
		this.lblFooterLogo = new UltraLabel();
		this.btnFooterPath = new UltraButton();
		this.picbFooterLogo = new UltraPictureBox();
		this.lblLogo = new UltraLabel();
		this.picbLogo = new UltraPictureBox();
		this.btnLogoPath = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.lblMessage = new UltraLabel();
		this.txtMessage = new UltraTextEditor();
		this.lblMin = new UltraLabel();
		this.txtFax = new UltraTextEditor();
		this.lblMax = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtCompanyCode = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.lblInvMsg1 = new UltraLabel();
		this.txtInvoiceMsg1 = new UltraTextEditor();
		this.txtInvoiceMsg2 = new UltraTextEditor();
		this.lblInvoiceMsg2 = new UltraLabel();
		this.lblTRN = new UltraLabel();
		this.txtTRN = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMessage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMsg1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMsg2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTRN).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance23");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.pbStampLogo, "pbStampLogo");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance24");
		this.pbStampLogo.Appearance = (AppearanceBase)(object)val10;
		this.pbStampLogo.BorderShadowColor = System.Drawing.Color.Empty;
		this.pbStampLogo.BorderStyle = (UIElementBorderStyle)2;
		((System.Windows.Forms.Control)(object)this.pbStampLogo).Name = "pbStampLogo";
		resources.ApplyResources(this.btnStampLogo, "btnStampLogo");
		((System.Windows.Forms.Control)(object)this.btnStampLogo).Name = "btnStampLogo";
		((System.Windows.Forms.Control)(object)this.btnStampLogo).Click += new System.EventHandler(btnWaterMark_Click);
		resources.ApplyResources(this.lblStampLogo, "lblStampLogo");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance25");
		((ControlBase)this.lblStampLogo).Appearance = (AppearanceBase)(object)val11;
		this.lblStampLogo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStampLogo).Name = "lblStampLogo";
		((ControlBase)this.lblStampLogo).WrapText = false;
		resources.ApplyResources(this.lblFooterLogo, "lblFooterLogo");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance26");
		((ControlBase)this.lblFooterLogo).Appearance = (AppearanceBase)(object)val12;
		this.lblFooterLogo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFooterLogo).Name = "lblFooterLogo";
		((ControlBase)this.lblFooterLogo).WrapText = false;
		resources.ApplyResources(this.btnFooterPath, "btnFooterPath");
		((ControlBase)this.btnFooterPath).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.btnFooterPath).Name = "btnFooterPath";
		((System.Windows.Forms.Control)(object)this.btnFooterPath).Click += new System.EventHandler(btnFooterPath_Click);
		resources.ApplyResources(this.picbFooterLogo, "picbFooterLogo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val13, "appearance27");
		this.picbFooterLogo.Appearance = (AppearanceBase)(object)val13;
		this.picbFooterLogo.BorderShadowColor = System.Drawing.Color.Empty;
		this.picbFooterLogo.BorderStyle = (UIElementBorderStyle)2;
		this.picbFooterLogo.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.picbFooterLogo).Name = "picbFooterLogo";
		resources.ApplyResources(this.lblLogo, "lblLogo");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val14, "appearance28");
		((ControlBase)this.lblLogo).Appearance = (AppearanceBase)(object)val14;
		this.lblLogo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLogo).Name = "lblLogo";
		((ControlBase)this.lblLogo).WrapText = false;
		resources.ApplyResources(this.picbLogo, "picbLogo");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val15, "appearance29");
		this.picbLogo.Appearance = (AppearanceBase)(object)val15;
		this.picbLogo.BorderShadowColor = System.Drawing.Color.Empty;
		this.picbLogo.BorderStyle = (UIElementBorderStyle)2;
		this.picbLogo.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.picbLogo).Name = "picbLogo";
		resources.ApplyResources(this.btnLogoPath, "btnLogoPath");
		((System.Windows.Forms.Control)(object)this.btnLogoPath).Name = "btnLogoPath";
		((System.Windows.Forms.Control)(object)this.btnLogoPath).Click += new System.EventHandler(btnImagePath_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.lblMessage, "lblMessage");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance30");
		((ControlBase)this.lblMessage).Appearance = (AppearanceBase)(object)val16;
		this.lblMessage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMessage).Name = "lblMessage";
		((ControlBase)this.lblMessage).WrapText = false;
		resources.ApplyResources(this.txtMessage, "txtMessage");
		((System.Windows.Forms.Control)(object)this.txtMessage).Name = "txtMessage";
		resources.ApplyResources(this.lblMin, "lblMin");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val17, "appearance31");
		((ControlBase)this.lblMin).Appearance = (AppearanceBase)(object)val17;
		this.lblMin.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMin).Name = "lblMin";
		((ControlBase)this.lblMin).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		resources.ApplyResources(this.lblMax, "lblMax");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val18, "appearance32");
		((ControlBase)this.lblMax).Appearance = (AppearanceBase)(object)val18;
		this.lblMax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMax).Name = "lblMax";
		((ControlBase)this.lblMax).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val19, "appearance33");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val19;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((ControlBase)this.ultraLabel1).ImageTransparentColor = System.Drawing.Color.Turquoise;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtCompanyCode, "txtCompanyCode");
		((System.Windows.Forms.Control)(object)this.txtCompanyCode).Name = "txtCompanyCode";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val20, "appearance34");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val20;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblInvMsg1, "lblInvMsg1");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val21, "appearance35");
		((ControlBase)this.lblInvMsg1).Appearance = (AppearanceBase)(object)val21;
		this.lblInvMsg1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvMsg1).Name = "lblInvMsg1";
		((ControlBase)this.lblInvMsg1).WrapText = false;
		resources.ApplyResources(this.txtInvoiceMsg1, "txtInvoiceMsg1");
		((System.Windows.Forms.Control)(object)this.txtInvoiceMsg1).Name = "txtInvoiceMsg1";
		resources.ApplyResources(this.txtInvoiceMsg2, "txtInvoiceMsg2");
		((System.Windows.Forms.Control)(object)this.txtInvoiceMsg2).Name = "txtInvoiceMsg2";
		resources.ApplyResources(this.lblInvoiceMsg2, "lblInvoiceMsg2");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val22, "appearance36");
		((ControlBase)this.lblInvoiceMsg2).Appearance = (AppearanceBase)(object)val22;
		this.lblInvoiceMsg2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvoiceMsg2).Name = "lblInvoiceMsg2";
		((ControlBase)this.lblInvoiceMsg2).WrapText = false;
		resources.ApplyResources(this.lblTRN, "lblTRN");
		this.lblTRN.AutoEllipsis = false;
		((ControlBase)this.lblTRN).ImageTransparentColor = System.Drawing.Color.Turquoise;
		((System.Windows.Forms.Control)(object)this.lblTRN).Name = "lblTRN";
		((ControlBase)this.lblTRN).WrapText = false;
		resources.ApplyResources(this.txtTRN, "txtTRN");
		((System.Windows.Forms.Control)(object)this.txtTRN).Name = "txtTRN";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceMsg2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvMsg1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceMsg2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceMsg1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMin);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMessage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pbStampLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStampLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStampLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFooterLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnFooterPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picbFooterLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picbLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLogoPath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTRN);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTRN);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanyCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmCompanies";
		base.Load += new System.EventHandler(frmCompanies_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanyCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTRN, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTRN, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLogoPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picbLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picbFooterLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnFooterPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFooterLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStampLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStampLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pbStampLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMessage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMin, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvoiceMsg1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvoiceMsg2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvMsg1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvoiceMsg2, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMessage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanyCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMsg1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceMsg2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTRN).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
