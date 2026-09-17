using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.Transactions;

public class frmClosingCheck : frmBase
{
	public bool Cancel = false;

	private DataTable dtVisaType = new DataTable();

	public int VisaTypeID = 0;

	private bool Disc2Visible = false;

	private int ClientID = 0;

	private bool GetOnlineBalance = false;

	private object SubAccountID;

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	private UltraLabel lblDiscount;

	private UltraLabel lblNetPrice;

	private UltraLabel lblTaxTotalValue;

	private UltraLabel lblServiceChargeValue;

	private UltraLabel lblGrossValue;

	private UltraLabel lblVisaType;

	private UltraLabel lblVisaNo;

	private UltraLabel lblRestAmount;

	private UltraLabel lblPaidAmount;

	public UltraButton btnCashAmount;

	public UltraButton btnVisaNo;

	private UltraButton btnSave;

	private UltraLabel lblDiscountRatio;

	public UltraTextEditor txtDiscountValue;

	public UltraTextEditor txtNetprice;

	public UltraTextEditor txtTaxTotalValue;

	public UltraTextEditor txtServiceChargeValue;

	public UltraTextEditor txtGrossValue;

	public UltraCheckEditor chkCash;

	public UltraComboEditor cboVisaType;

	public UltraTextEditor txtVisaNo;

	public UltraTextEditor txtRestAmount;

	public UltraTextEditor txtPaidAmount;

	public UltraTextEditor txtDiscountRatio;

	public UltraLabel lblTitle2;

	private UltraLabel lblCashAmount;

	public UltraTextEditor txtCashAmount;

	public UltraCheckEditor chkVisa;

	private UltraLabel lblVisaAmount;

	public UltraTextEditor txtVisaAmount;

	private UltraLabel lblOnAccountAmount;

	public UltraTextEditor txtOnAccountAmount;

	public UltraCheckEditor chkOnAccount;

	public UltraButton btnOnAccountAmount;

	public UltraButton btnVisaAmount;

	public UltraTextEditor txtDiscountValue2;

	private UltraLabel lblDiscountValue2;

	public UltraTextEditor txtDiscountRatio2;

	private UltraLabel lblDiscountRatio2;

	private UltraLabel ClientBalance;

	public UltraTextEditor txtClientBalance;

	public frmClosingCheck()
	{
		InitializeComponent();
	}

	public frmClosingCheck(bool Discount2Visible, int CLIENTID, object SUBACCOUNTID, bool _GetOnlineBalance)
		: this()
	{
		GetOnlineBalance = _GetOnlineBalance;
		Disc2Visible = Discount2Visible;
		ClientID = CLIENTID;
		SubAccountID = SUBACCOUNTID;
	}

	public override void PrepareData()
	{
		if (ClientID != 0)
		{
			if (GetOnlineBalance)
			{
				try
				{
					((Control)(object)txtClientBalance).Text = decimal.Parse(Clients.Balance(ClientID.ToString(), "-1", IsFromServer: true, 15).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				}
				catch
				{
					((Control)(object)txtClientBalance).Text = "0";
					GlobalVariables.InformationMB.Show("لايمكن الإستعلام عن رصيد العميل لتعزر الإتصال بالخادم", "Client Balance not Available ");
				}
			}
			else
			{
				((Control)(object)txtClientBalance).Text = decimal.Parse(Clients.Balance(ClientID.ToString(), "," + GlobalVariables.CurrentBranchID + ",", IsFromServer: false, 0).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
		else
		{
			((Control)(object)txtClientBalance).Text = "0";
		}
		dtVisaType = VisaTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboVisaType, dtVisaType, "VisaTypeID", "VisaTypeName");
		((Control)(object)txtCashAmount).Select();
		UltraTextEditor obj2 = txtDiscountRatio2;
		UltraTextEditor obj3 = txtDiscountValue2;
		UltraLabel obj4 = lblDiscountRatio2;
		bool flag = (((Control)(object)lblDiscountValue2).Visible = Disc2Visible);
		bool flag2 = (((Control)(object)obj4).Visible = flag);
		bool visible = (((Control)(object)obj3).Visible = flag2);
		((Control)(object)obj2).Visible = visible;
	}

	private void chkCash_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtCashAmount).Enabled = ((UltraToggleEditorBase)chkCash).Checked;
		((Control)(object)btnCashAmount).Visible = ((UltraToggleEditorBase)chkCash).Checked;
		if (!((UltraToggleEditorBase)chkCash).Checked)
		{
			((Control)(object)txtCashAmount).Text = "0";
		}
	}

	private void chkOnAccount_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtOnAccountAmount).Enabled = ((UltraToggleEditorBase)chkOnAccount).Checked;
		((Control)(object)btnOnAccountAmount).Visible = ((UltraToggleEditorBase)chkOnAccount).Checked;
		if (!((UltraToggleEditorBase)chkOnAccount).Checked)
		{
			((Control)(object)txtOnAccountAmount).Text = "0";
		}
	}

	private void chkVisa_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtVisaNo).Enabled = ((UltraToggleEditorBase)chkVisa).Checked;
		((Control)(object)txtVisaAmount).Enabled = ((UltraToggleEditorBase)chkVisa).Checked;
		((Control)(object)cboVisaType).Enabled = ((UltraToggleEditorBase)chkVisa).Checked;
		((Control)(object)btnVisaAmount).Visible = ((UltraToggleEditorBase)chkVisa).Checked;
		((Control)(object)btnVisaNo).Visible = ((UltraToggleEditorBase)chkVisa).Checked;
		if (!((UltraToggleEditorBase)chkVisa).Checked)
		{
			((Control)(object)txtVisaAmount).Text = "0";
		}
	}

	private void btnVisaNo_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtVisaNo, ((Control)(object)txtVisaNo).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtVisaNo).Location.X + frmDecimal2.Width, ((Control)(object)txtVisaNo).Location.Y + ((Control)(object)txtVisaNo).Height + 10);
		frmDecimal2.Show();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Cancel = true;
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if ((((Control)(object)txtCashAmount).Text == "" || ((Control)(object)txtCashAmount).Text == "." || decimal.Parse(((Control)(object)txtCashAmount).Text) < 0m) && ((UltraToggleEditorBase)chkCash).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمة فى المبلغ المدفوع نقدآ" : "Please Enter Amount in Cash Amount");
			return;
		}
		if (((UltraToggleEditorBase)chkOnAccount).Checked && ClientID == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			return;
		}
		if (((UltraToggleEditorBase)chkOnAccount).Checked && ClientID != 0 && SubAccountID == DBNull.Value && decimal.Parse(((Control)(object)txtOnAccountAmount).Text) > decimal.Parse(((Control)(object)txtClientBalance).Text))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الحساب التحليلى للعميل" : "Please Select SubAccount For Client");
			return;
		}
		if ((((Control)(object)txtOnAccountAmount).Text == "" || ((Control)(object)txtOnAccountAmount).Text == "." || decimal.Parse(((Control)(object)txtOnAccountAmount).Text) < 0m) && ((UltraToggleEditorBase)chkOnAccount).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمة فى المبلغ المدفوع على الحساب" : "Please Enter Amount in on Account Amount");
			return;
		}
		if ((((Control)(object)txtVisaAmount).Text == "" || ((Control)(object)txtVisaAmount).Text == "." || decimal.Parse(((Control)(object)txtVisaAmount).Text) < 0m) && ((UltraToggleEditorBase)chkVisa).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمة فى المبلغ المدفوع بالفيزا" : "Please Enter Amount in Visa Amount");
			return;
		}
		if (((Control)(object)txtVisaNo).Text == "" && ((UltraToggleEditorBase)chkVisa).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الفيزا" : "Please Enter Visa No");
			return;
		}
		if (cboVisaType.SelectedIndex == -1 && ((UltraToggleEditorBase)chkVisa).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار نوع الفيزا" : "Please Select Visa Type");
			return;
		}
		if (decimal.Parse(((Control)(object)txtOnAccountAmount).Text) + decimal.Parse(((Control)(object)txtVisaAmount).Text) > decimal.Parse(((Control)(object)txtNetprice).Text))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "المبلغ المدفوع على الحساب أكبر من قيمة الشيك" : "On Account Amount less Than Check Amount");
			return;
		}
		if (decimal.Parse(((Control)(object)txtPaidAmount).Text) < decimal.Parse(((Control)(object)txtNetprice).Text))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "المبلغ المدفوع أقل من قيمة الشيك" : "Paid Amount less Than Check Amount");
			return;
		}
		VisaTypeID = ((cboVisaType.SelectedIndex != -1) ? int.Parse(((TextEditorControlBase)cboVisaType).Value.ToString()) : 0);
		Close();
	}

	private void btnCashAmount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtCashAmount, ((Control)(object)txtCashAmount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtCashAmount).Location.X + frmDecimal2.Width, ((Control)(object)txtCashAmount).Location.Y + ((Control)(object)txtCashAmount).Height + 10);
		frmDecimal2.Show();
	}

	private void btnOnAccountAmount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtOnAccountAmount, ((Control)(object)txtOnAccountAmount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtOnAccountAmount).Location.X + frmDecimal2.Width, ((Control)(object)txtOnAccountAmount).Location.Y + ((Control)(object)txtOnAccountAmount).Height + 10);
		frmDecimal2.Show();
	}

	private void btnVisaAmount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtVisaAmount, ((Control)(object)txtVisaAmount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtVisaAmount).Location.X + frmDecimal2.Width, ((Control)(object)txtVisaAmount).Location.Y + ((Control)(object)txtVisaAmount).Height + 10);
		frmDecimal2.Show();
	}

	private void txtAmounts_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)txtPaidAmount).Text = (decimal.Parse((((Control)(object)txtCashAmount).Text == "" || ((Control)(object)txtCashAmount).Text == ".") ? "0" : ((Control)(object)txtCashAmount).Text) + decimal.Parse((((Control)(object)txtOnAccountAmount).Text == "" || ((Control)(object)txtOnAccountAmount).Text == ".") ? "0" : ((Control)(object)txtOnAccountAmount).Text) + decimal.Parse((((Control)(object)txtVisaAmount).Text == "" || ((Control)(object)txtVisaAmount).Text == ".") ? "0" : ((Control)(object)txtVisaAmount).Text)).ToString();
		((Control)(object)txtRestAmount).Text = (decimal.Parse(((Control)(object)txtNetprice).Text) - decimal.Parse(((Control)(object)txtPaidAmount).Text)).ToString();
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
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
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
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Expected O, but got Unknown
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Expected O, but got Unknown
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected O, but got Unknown
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmClosingCheck));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblDiscount = new UltraLabel();
		this.txtDiscountValue = new UltraTextEditor();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.lblTaxTotalValue = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.lblServiceChargeValue = new UltraLabel();
		this.txtServiceChargeValue = new UltraTextEditor();
		this.lblGrossValue = new UltraLabel();
		this.txtGrossValue = new UltraTextEditor();
		this.chkCash = new UltraCheckEditor();
		this.lblVisaType = new UltraLabel();
		this.cboVisaType = new UltraComboEditor();
		this.lblVisaNo = new UltraLabel();
		this.txtVisaNo = new UltraTextEditor();
		this.lblRestAmount = new UltraLabel();
		this.txtRestAmount = new UltraTextEditor();
		this.lblPaidAmount = new UltraLabel();
		this.txtPaidAmount = new UltraTextEditor();
		this.btnCashAmount = new UltraButton();
		this.btnVisaNo = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblDiscountRatio = new UltraLabel();
		this.txtDiscountRatio = new UltraTextEditor();
		this.lblTitle2 = new UltraLabel();
		this.lblCashAmount = new UltraLabel();
		this.txtCashAmount = new UltraTextEditor();
		this.chkVisa = new UltraCheckEditor();
		this.lblVisaAmount = new UltraLabel();
		this.txtVisaAmount = new UltraTextEditor();
		this.lblOnAccountAmount = new UltraLabel();
		this.txtOnAccountAmount = new UltraTextEditor();
		this.chkOnAccount = new UltraCheckEditor();
		this.btnOnAccountAmount = new UltraButton();
		this.btnVisaAmount = new UltraButton();
		this.txtDiscountValue2 = new UltraTextEditor();
		this.lblDiscountValue2 = new UltraLabel();
		this.txtDiscountRatio2 = new UltraTextEditor();
		this.lblDiscountRatio2 = new UltraLabel();
		this.ClientBalance = new UltraLabel();
		this.txtClientBalance = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargeValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCash).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCashAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOnAccountAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkOnAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBalance).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblDiscount, "lblDiscount");
		this.lblDiscount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscount).Name = "lblDiscount";
		((ControlBase)this.lblDiscount).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue, "txtDiscountValue");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).Name = "txtDiscountValue";
		((EditorButtonControlBase)this.txtDiscountValue).ReadOnly = true;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		((ControlBase)this.lblNetPrice).WrapText = false;
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.lblTaxTotalValue, "lblTaxTotalValue");
		this.lblTaxTotalValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaxTotalValue).Name = "lblTaxTotalValue";
		((ControlBase)this.lblTaxTotalValue).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.lblServiceChargeValue, "lblServiceChargeValue");
		this.lblServiceChargeValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceChargeValue).Name = "lblServiceChargeValue";
		((ControlBase)this.lblServiceChargeValue).WrapText = false;
		resources.ApplyResources(this.txtServiceChargeValue, "txtServiceChargeValue");
		((System.Windows.Forms.Control)(object)this.txtServiceChargeValue).Name = "txtServiceChargeValue";
		((EditorButtonControlBase)this.txtServiceChargeValue).ReadOnly = true;
		resources.ApplyResources(this.lblGrossValue, "lblGrossValue");
		this.lblGrossValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGrossValue).Name = "lblGrossValue";
		((ControlBase)this.lblGrossValue).WrapText = false;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.chkCash, "chkCash");
		((System.Windows.Forms.Control)(object)this.chkCash).Name = "chkCash";
		((UltraToggleEditorBase)this.chkCash).CheckedChanged += new System.EventHandler(chkCash_CheckedChanged);
		resources.ApplyResources(this.lblVisaType, "lblVisaType");
		this.lblVisaType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaType).Name = "lblVisaType";
		((ControlBase)this.lblVisaType).WrapText = false;
		resources.ApplyResources(this.cboVisaType, "cboVisaType");
		((System.Windows.Forms.Control)(object)this.cboVisaType).Name = "cboVisaType";
		resources.ApplyResources(this.lblVisaNo, "lblVisaNo");
		this.lblVisaNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaNo).Name = "lblVisaNo";
		((ControlBase)this.lblVisaNo).WrapText = false;
		resources.ApplyResources(this.txtVisaNo, "txtVisaNo");
		((System.Windows.Forms.Control)(object)this.txtVisaNo).Name = "txtVisaNo";
		resources.ApplyResources(this.lblRestAmount, "lblRestAmount");
		this.lblRestAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRestAmount).Name = "lblRestAmount";
		((ControlBase)this.lblRestAmount).WrapText = false;
		resources.ApplyResources(this.txtRestAmount, "txtRestAmount");
		((System.Windows.Forms.Control)(object)this.txtRestAmount).Name = "txtRestAmount";
		((EditorButtonControlBase)this.txtRestAmount).ReadOnly = true;
		resources.ApplyResources(this.lblPaidAmount, "lblPaidAmount");
		this.lblPaidAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaidAmount).Name = "lblPaidAmount";
		((ControlBase)this.lblPaidAmount).WrapText = false;
		resources.ApplyResources(this.txtPaidAmount, "txtPaidAmount");
		((System.Windows.Forms.Control)(object)this.txtPaidAmount).Name = "txtPaidAmount";
		((EditorButtonControlBase)this.txtPaidAmount).ReadOnly = true;
		resources.ApplyResources(this.btnCashAmount, "btnCashAmount");
		((System.Windows.Forms.Control)(object)this.btnCashAmount).Name = "btnCashAmount";
		((System.Windows.Forms.Control)(object)this.btnCashAmount).Click += new System.EventHandler(btnCashAmount_Click);
		resources.ApplyResources(this.btnVisaNo, "btnVisaNo");
		((System.Windows.Forms.Control)(object)this.btnVisaNo).Name = "btnVisaNo";
		((System.Windows.Forms.Control)(object)this.btnVisaNo).Click += new System.EventHandler(btnVisaNo_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblDiscountRatio, "lblDiscountRatio");
		this.lblDiscountRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio).Name = "lblDiscountRatio";
		((ControlBase)this.lblDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio, "txtDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).Name = "txtDiscountRatio";
		((EditorButtonControlBase)this.txtDiscountRatio).ReadOnly = true;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCashAmount, "lblCashAmount");
		this.lblCashAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCashAmount).Name = "lblCashAmount";
		((ControlBase)this.lblCashAmount).WrapText = false;
		resources.ApplyResources(this.txtCashAmount, "txtCashAmount");
		((System.Windows.Forms.Control)(object)this.txtCashAmount).Name = "txtCashAmount";
		((TextEditorControlBase)this.txtCashAmount).ValueChanged += new System.EventHandler(txtAmounts_ValueChanged);
		resources.ApplyResources(this.chkVisa, "chkVisa");
		((System.Windows.Forms.Control)(object)this.chkVisa).Name = "chkVisa";
		((UltraToggleEditorBase)this.chkVisa).CheckedChanged += new System.EventHandler(chkVisa_CheckedChanged);
		resources.ApplyResources(this.lblVisaAmount, "lblVisaAmount");
		this.lblVisaAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVisaAmount).Name = "lblVisaAmount";
		((ControlBase)this.lblVisaAmount).WrapText = false;
		resources.ApplyResources(this.txtVisaAmount, "txtVisaAmount");
		((System.Windows.Forms.Control)(object)this.txtVisaAmount).Name = "txtVisaAmount";
		((TextEditorControlBase)this.txtVisaAmount).ValueChanged += new System.EventHandler(txtAmounts_ValueChanged);
		resources.ApplyResources(this.lblOnAccountAmount, "lblOnAccountAmount");
		this.lblOnAccountAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOnAccountAmount).Name = "lblOnAccountAmount";
		((ControlBase)this.lblOnAccountAmount).WrapText = false;
		resources.ApplyResources(this.txtOnAccountAmount, "txtOnAccountAmount");
		((System.Windows.Forms.Control)(object)this.txtOnAccountAmount).Name = "txtOnAccountAmount";
		((TextEditorControlBase)this.txtOnAccountAmount).ValueChanged += new System.EventHandler(txtAmounts_ValueChanged);
		resources.ApplyResources(this.chkOnAccount, "chkOnAccount");
		((System.Windows.Forms.Control)(object)this.chkOnAccount).Name = "chkOnAccount";
		((UltraToggleEditorBase)this.chkOnAccount).CheckedChanged += new System.EventHandler(chkOnAccount_CheckedChanged);
		resources.ApplyResources(this.btnOnAccountAmount, "btnOnAccountAmount");
		((System.Windows.Forms.Control)(object)this.btnOnAccountAmount).Name = "btnOnAccountAmount";
		((System.Windows.Forms.Control)(object)this.btnOnAccountAmount).Click += new System.EventHandler(btnOnAccountAmount_Click);
		resources.ApplyResources(this.btnVisaAmount, "btnVisaAmount");
		((System.Windows.Forms.Control)(object)this.btnVisaAmount).Name = "btnVisaAmount";
		((System.Windows.Forms.Control)(object)this.btnVisaAmount).Click += new System.EventHandler(btnVisaAmount_Click);
		resources.ApplyResources(this.txtDiscountValue2, "txtDiscountValue2");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue2).Name = "txtDiscountValue2";
		((EditorButtonControlBase)this.txtDiscountValue2).ReadOnly = true;
		resources.ApplyResources(this.lblDiscountValue2, "lblDiscountValue2");
		this.lblDiscountValue2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountValue2).Name = "lblDiscountValue2";
		((ControlBase)this.lblDiscountValue2).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio2, "txtDiscountRatio2");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio2).Name = "txtDiscountRatio2";
		((EditorButtonControlBase)this.txtDiscountRatio2).ReadOnly = true;
		resources.ApplyResources(this.lblDiscountRatio2, "lblDiscountRatio2");
		this.lblDiscountRatio2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio2).Name = "lblDiscountRatio2";
		((ControlBase)this.lblDiscountRatio2).WrapText = false;
		resources.ApplyResources(this.ClientBalance, "ClientBalance");
		this.ClientBalance.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ClientBalance).Name = "ClientBalance";
		resources.ApplyResources(this.txtClientBalance, "txtClientBalance");
		((System.Windows.Forms.Control)(object)this.txtClientBalance).Name = "txtClientBalance";
		((EditorButtonControlBase)this.txtClientBalance).ReadOnly = true;
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ClientBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientBalance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOnAccountAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOnAccountAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOnAccountAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkOnAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkVisa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCashAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRestAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaidAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVisaType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCash);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtServiceChargeValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmClosingCheck";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGrossValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtServiceChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblServiceChargeValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaxTotalValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCash, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVisaType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaidAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRestAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVisaNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCashAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkVisa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkOnAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOnAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOnAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOnAccountAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVisaAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ClientBalance, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtServiceChargeValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCash).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVisaType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRestAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaidAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCashAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkVisa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVisaAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOnAccountAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkOnAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientBalance).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
