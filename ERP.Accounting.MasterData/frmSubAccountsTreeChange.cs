using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Accounting.MasterData;

public class frmSubAccountsTreeChange : frmBase
{
	private DataTable dtSubAccounts;

	private DataTable dtUser;

	private DataTable dtInvoiceFunction;

	private bool ModifySubAccount;

	public int SubAccountID = 0;

	public int UserID = 0;

	private string FormName = "";

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraTextEditor txtUserName;

	private UltraLabel lblUserName;

	private UltraTextEditor txtPassword;

	private UltraLabel lblPassword;

	public UltraButton btnSubAccountSearch;

	private UltraLabel lblSubAccount;

	private UltraComboEditor cboSubAccount;

	private UltraButton btnSave;

	private UltraButton btnClose;

	public frmSubAccountsTreeChange()
	{
		InitializeComponent();
	}

	public frmSubAccountsTreeChange(string formname)
		: this()
	{
		FormName = formname;
	}

	public override void PrepareData()
	{
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		dtUser = Users.CheckUser(((Control)(object)txtUserName).Text, ((Control)(object)txtPassword).Text, IsFromServer: false);
		if (dtUser.Rows.Count > 0)
		{
			dtInvoiceFunction = UsersFormsFunctions.SelectFunctions(dtUser.Rows[0]["User_ID"].ToString(), IsFromServer: false);
			ModifySubAccount = ((dtInvoiceFunction.Select("FormID=" + GlobalVariables.dtForms.Select("FormFullName = '" + FormName + "'")[0]["FormID"].ToString() + " And FunctionNameEn='ModifySubAccount' ").Length != 0) ? true : false);
			if (!ModifySubAccount)
			{
				GlobalVariables.InformationMB.Show("لاتوجد صلاحية لهذا المستخدم فى تغيير الحساب التحليلى", "User Cannot Modify SubAccount");
				return;
			}
			if (cboSubAccount.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الحساب التحليلى", "Please Select SubAccount");
				return;
			}
			SubAccountID = int.Parse(((TextEditorControlBase)cboSubAccount).Value.ToString());
			UserID = int.Parse(dtUser.Rows[0]["User_ID"].ToString());
			Close();
		}
		else
		{
			GlobalVariables.InformationMB.Show("خطأ فى إسم المستخدم أو كلمة المرور", "Wrong User Name or Password");
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSubAccount).Value = num;
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
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Accounting.MasterData.frmSubAccountsTreeChange));
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		this.lblUserName = new UltraLabel();
		this.txtPassword = new UltraTextEditor();
		this.lblPassword = new UltraLabel();
		this.btnSubAccountSearch = new UltraButton();
		this.lblSubAccount = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		base.SuspendLayout();
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.txtUserName, "txtUserName");
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		this.lblUserName.AutoEllipsis = false;
		resources.ApplyResources(this.lblUserName, "lblUserName");
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		((ControlBase)this.lblUserName).WrapText = false;
		resources.ApplyResources(this.txtPassword, "txtPassword");
		((System.Windows.Forms.Control)(object)this.txtPassword).Name = "txtPassword";
		this.txtPassword.PasswordChar = '*';
		this.lblPassword.AutoEllipsis = false;
		resources.ApplyResources(this.lblPassword, "lblPassword");
		((System.Windows.Forms.Control)(object)this.lblPassword).Name = "lblPassword";
		((ControlBase)this.lblPassword).WrapText = false;
		((AppearanceBase)val2).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		this.lblSubAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblSubAccount, "lblSubAccount");
		((System.Windows.Forms.Control)(object)this.lblSubAccount).Name = "lblSubAccount";
		((ControlBase)this.lblSubAccount).WrapText = false;
		((TextEditorControlBase)this.cboSubAccount).AlwaysInEditMode = true;
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmSubAccountsTreeChange";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
