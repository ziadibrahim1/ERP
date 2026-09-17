using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmBranchesChange : frmBase
{
	private DataTable dtBranches;

	private DataTable dtUser;

	private DataTable dtInvoiceFunction;

	private bool ViewBranches;

	public int BranchID = 0;

	public int UserID = 0;

	private string FormName = "";

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraTextEditor txtUserName;

	private UltraLabel lblUserName;

	private UltraTextEditor txtPassword;

	private UltraLabel lblPassword;

	private UltraLabel lblBranches;

	private UltraComboEditor cboBranches;

	private UltraButton btnSave;

	private UltraButton btnClose;

	public frmBranchesChange()
	{
		InitializeComponent();
	}

	public frmBranchesChange(string formname)
		: this()
	{
		FormName = formname;
	}

	public override void PrepareData()
	{
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranches, "BranchID", "BranchName");
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
			ViewBranches = ((dtInvoiceFunction.Select("FormID=" + GlobalVariables.dtForms.Select("FormFullName = '" + FormName + "'")[0]["FormID"].ToString() + " And FunctionNameEn='ViewAllBranches' ").Length != 0) ? true : false);
			if (!ViewBranches)
			{
				GlobalVariables.InformationMB.Show("لاتوجد صلاحية لهذا المستخدم فى تغيير الفرع", "User Cannot Modify Branch");
				return;
			}
			if (cboBranches.SelectedIndex == -1)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الفرع", "Please Select Branch");
				return;
			}
			BranchID = int.Parse(((TextEditorControlBase)cboBranches).Value.ToString());
			UserID = int.Parse(dtUser.Rows[0]["User_ID"].ToString());
			Close();
		}
		else
		{
			GlobalVariables.InformationMB.Show("خطأ فى إسم المستخدم أو كلمة المرور", "Wrong User Name or Password");
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmBranchesChange));
		this.lblTitle = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		this.lblUserName = new UltraLabel();
		this.txtPassword = new UltraTextEditor();
		this.lblPassword = new UltraLabel();
		this.lblBranches = new UltraLabel();
		this.cboBranches = new UltraComboEditor();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
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
		this.lblBranches.AutoEllipsis = false;
		resources.ApplyResources(this.lblBranches, "lblBranches");
		((System.Windows.Forms.Control)(object)this.lblBranches).Name = "lblBranches";
		((ControlBase)this.lblBranches).WrapText = false;
		((TextEditorControlBase)this.cboBranches).AlwaysInEditMode = true;
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassword);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmBranchesChange";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassword, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
