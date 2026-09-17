using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmChangeDiscount : frmBase
{
	private DataTable dtUser;

	public int UserID = 0;

	public decimal DiscountValue = default(decimal);

	public decimal DiscountRatio = default(decimal);

	public bool Cancel = false;

	private decimal TotalAmount = default(decimal);

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnClose;

	private UltraButton btnSave;

	private UltraTextEditor txtPassword;

	private UltraLabel lblPassword;

	private UltraTextEditor txtUserName;

	private UltraLabel lblUserName;

	private UltraTextEditor txtDiscountRatio;

	private UltraLabel lblDiscountRatio;

	private UltraTextEditor txtDiscountValue;

	private UltraLabel lblDiscountValue;

	public UltraButton btnDiscountValue;

	public UltraButton btnDiscountRatio;

	public UltraButton btnKeyboard;

	public frmChangeDiscount()
	{
		InitializeComponent();
	}

	public frmChangeDiscount(decimal totalamount, decimal discountvalue, decimal discountratio)
		: this()
	{
		TotalAmount = totalamount;
		((Control)(object)txtDiscountValue).Text = discountvalue.ToString();
		((Control)(object)txtDiscountRatio).Text = discountratio.ToString();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		dtUser = Users.CheckUser(((Control)(object)txtUserName).Text, ((Control)(object)txtPassword).Text, IsFromServer: false);
		if (dtUser.Rows.Count > 0)
		{
			if (((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountRatio).Text == "")
			{
				((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
				((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
				((Control)(object)txtDiscountValue).Text = "0";
				((Control)(object)txtDiscountRatio).Text = "0";
				((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
				((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
			}
			if (decimal.Parse(((Control)(object)txtDiscountRatio).Text) > decimal.Parse(dtUser.Rows[0]["SalesDiscountPercentage"].ToString()))
			{
				GlobalVariables.InformationMB.Show("نسبة الخصم أعلى من الحد المسموح به لهذا المستخدم", "Discount Percentage Exceed From user limit");
				return;
			}
			DiscountValue = decimal.Parse(((Control)(object)txtDiscountValue).Text);
			DiscountRatio = decimal.Parse(((Control)(object)txtDiscountRatio).Text);
			UserID = int.Parse(dtUser.Rows[0]["User_ID"].ToString());
			Close();
		}
		else
		{
			GlobalVariables.InformationMB.Show("خطأ فى إسم المستخدم أو كلمة المرور", "Wrong User Name or Password");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Cancel = true;
		Close();
	}

	private void txtDiscountRatio_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtDiscountRatio).Text != "" && decimal.Parse(((Control)(object)txtDiscountRatio).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscountValue).ValueChanged -= txtDiscountValue_ValueChanged;
			((Control)(object)txtDiscountValue).Text = Math.Round(TotalAmount * (decimal.Parse(((Control)(object)txtDiscountRatio).Text) / 100m), 2).ToString();
			((TextEditorControlBase)txtDiscountValue).ValueChanged += txtDiscountValue_ValueChanged;
		}
	}

	private void txtDiscountValue_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtDiscountValue).Text != "" && decimal.Parse(((Control)(object)txtDiscountValue).Text) > 0m)
		{
			((TextEditorControlBase)txtDiscountRatio).ValueChanged -= txtDiscountRatio_ValueChanged;
			((Control)(object)txtDiscountRatio).Text = Math.Round(decimal.Parse((((Control)(object)txtDiscountValue).Text == "" || ((Control)(object)txtDiscountValue).Text == "0") ? "0" : ((Control)(object)txtDiscountValue).Text) / TotalAmount * 100m, 2).ToString();
			((TextEditorControlBase)txtDiscountRatio).ValueChanged += txtDiscountRatio_ValueChanged;
		}
	}

	private void btnDiscountValue_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtDiscountValue, ((Control)(object)txtDiscountValue).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtDiscountValue).Location.X + frmDecimal2.Width, ((Control)(object)txtDiscountValue).Location.Y + ((Control)(object)txtDiscountValue).Height);
		frmDecimal2.Show();
	}

	private void btnDiscountRatio_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtDiscountRatio, ((Control)(object)txtDiscountRatio).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtDiscountRatio).Location.X + frmDecimal2.Width, ((Control)(object)txtDiscountRatio).Location.Y + ((Control)(object)txtDiscountRatio).Height);
		frmDecimal2.Show();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmChangeDiscount));
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtPassword = new UltraTextEditor();
		this.lblPassword = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		this.lblUserName = new UltraLabel();
		this.txtDiscountRatio = new UltraTextEditor();
		this.lblDiscountRatio = new UltraLabel();
		this.txtDiscountValue = new UltraTextEditor();
		this.lblDiscountValue = new UltraLabel();
		this.btnDiscountValue = new UltraButton();
		this.btnDiscountRatio = new UltraButton();
		this.btnKeyboard = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).BeginInit();
		base.SuspendLayout();
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtPassword, "txtPassword");
		((System.Windows.Forms.Control)(object)this.txtPassword).Name = "txtPassword";
		this.txtPassword.PasswordChar = '*';
		this.lblPassword.AutoEllipsis = false;
		resources.ApplyResources(this.lblPassword, "lblPassword");
		((System.Windows.Forms.Control)(object)this.lblPassword).Name = "lblPassword";
		((ControlBase)this.lblPassword).WrapText = false;
		resources.ApplyResources(this.txtUserName, "txtUserName");
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		this.lblUserName.AutoEllipsis = false;
		resources.ApplyResources(this.lblUserName, "lblUserName");
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		((ControlBase)this.lblUserName).WrapText = false;
		resources.ApplyResources(this.txtDiscountRatio, "txtDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).Name = "txtDiscountRatio";
		((TextEditorControlBase)this.txtDiscountRatio).ValueChanged += new System.EventHandler(txtDiscountRatio_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.lblDiscountRatio.AutoEllipsis = false;
		resources.ApplyResources(this.lblDiscountRatio, "lblDiscountRatio");
		((System.Windows.Forms.Control)(object)this.lblDiscountRatio).Name = "lblDiscountRatio";
		((ControlBase)this.lblDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtDiscountValue, "txtDiscountValue");
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).Name = "txtDiscountValue";
		((TextEditorControlBase)this.txtDiscountValue).ValueChanged += new System.EventHandler(txtDiscountValue_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiscountValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.lblDiscountValue.AutoEllipsis = false;
		resources.ApplyResources(this.lblDiscountValue, "lblDiscountValue");
		((System.Windows.Forms.Control)(object)this.lblDiscountValue).Name = "lblDiscountValue";
		((ControlBase)this.lblDiscountValue).WrapText = false;
		resources.ApplyResources(this.btnDiscountValue, "btnDiscountValue");
		((System.Windows.Forms.Control)(object)this.btnDiscountValue).Name = "btnDiscountValue";
		((System.Windows.Forms.Control)(object)this.btnDiscountValue).Click += new System.EventHandler(btnDiscountValue_Click);
		resources.ApplyResources(this.btnDiscountRatio, "btnDiscountRatio");
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio).Name = "btnDiscountRatio";
		((System.Windows.Forms.Control)(object)this.btnDiscountRatio).Click += new System.EventHandler(btnDiscountRatio_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountRatio);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmChangeDiscount";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiscountRatio, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountValue).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
