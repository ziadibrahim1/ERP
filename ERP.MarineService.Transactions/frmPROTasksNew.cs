using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.MarineService.Transactions;

public class frmPROTasksNew : frmBase
{
	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnNewTasks;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraButton btnFinished;

	public UltraButton btnForAllUsers;

	public UltraButton btnRemarks;

	public UltraButton btnExportCargo;

	public UltraButton btnImportCargo;

	public UltraButton btnSignOff;

	public UltraButton btnSignOn;

	public UltraButton btnShortPass;

	public frmPROTasksNew()
	{
		InitializeComponent();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnNewTasks_Click(object sender, EventArgs e)
	{
		frmViewNewTasks frmViewNewTasks2 = new frmViewNewTasks(_IsSuperVisor: false);
		frmViewNewTasks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewNewTasks2.Location = new Point(0, 0);
		((Control)(object)frmViewNewTasks2.lblTitle).Text = (GlobalVariables.IsArabic ? "المهام الجديده" : "New Tasks");
		frmViewNewTasks2.ShowDialog();
	}

	private void btnFinished_Click(object sender, EventArgs e)
	{
		frmViewFinishedTasks frmViewFinishedTasks2 = new frmViewFinishedTasks(_IsSuperVisor: false);
		frmViewFinishedTasks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewFinishedTasks2.Location = new Point(0, 0);
		((Control)(object)frmViewFinishedTasks2.lblTitle).Text = (GlobalVariables.IsArabic ? "المهام المنتهية" : "Finished Tasks");
		frmViewFinishedTasks2.ShowDialog();
	}

	private void btnForAllUsers_Click(object sender, EventArgs e)
	{
		frmViewForAllUsersTasks frmViewForAllUsersTasks2 = new frmViewForAllUsersTasks(_IsSuperVisor: false, CanUpdate);
		frmViewForAllUsersTasks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewForAllUsersTasks2.Location = new Point(0, 0);
		((Control)(object)frmViewForAllUsersTasks2.lblTitle).Text = (GlobalVariables.IsArabic ? "مهام لكل المندوبين" : "For All Users Tasks");
		frmViewForAllUsersTasks2.ShowDialog();
	}

	private void btnRemarks_Click(object sender, EventArgs e)
	{
		frmViewRemarks frmViewRemarks2 = new frmViewRemarks(_IsSuperVisor: false, CanUpdate);
		frmViewRemarks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewRemarks2.Location = new Point(0, 0);
		((Control)(object)frmViewRemarks2.lblTitle).Text = (GlobalVariables.IsArabic ? " ملاحظــــــــــات" : "Remarks");
		frmViewRemarks2.ShowDialog();
	}

	private void btnSignOn_Click(object sender, EventArgs e)
	{
		frmViewSeaManCrewSignOn frmViewSeaManCrewSignOn2 = new frmViewSeaManCrewSignOn(_IsSuperVisor: false, CanUpdate);
		frmViewSeaManCrewSignOn2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewSeaManCrewSignOn2.Location = new Point(0, 0);
		((Control)(object)frmViewSeaManCrewSignOn2.lblTitle).Text = (GlobalVariables.IsArabic ? " الحاق" : "SignOn");
		frmViewSeaManCrewSignOn2.ShowDialog();
	}

	private void btnSignOff_Click(object sender, EventArgs e)
	{
		frmViewSeaManCrewSignOff frmViewSeaManCrewSignOff2 = new frmViewSeaManCrewSignOff(_IsSuperVisor: false, CanUpdate);
		frmViewSeaManCrewSignOff2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewSeaManCrewSignOff2.Location = new Point(0, 0);
		((Control)(object)frmViewSeaManCrewSignOff2.lblTitle).Text = (GlobalVariables.IsArabic ? " انزال" : "SignOff");
		frmViewSeaManCrewSignOff2.ShowDialog();
	}

	private void btnImportCargo_Click(object sender, EventArgs e)
	{
		frmViewImportCargo frmViewImportCargo2 = new frmViewImportCargo(_IsSuperVisor: false, CanUpdate);
		frmViewImportCargo2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewImportCargo2.Location = new Point(0, 0);
		((Control)(object)frmViewImportCargo2.lblTitle).Text = (GlobalVariables.IsArabic ? "وارد" : "Import");
		frmViewImportCargo2.ShowDialog();
	}

	private void btnExportCargo_Click(object sender, EventArgs e)
	{
		frmViewExportCargo frmViewExportCargo2 = new frmViewExportCargo(_IsSuperVisor: false, CanUpdate);
		frmViewExportCargo2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewExportCargo2.Location = new Point(0, 0);
		((Control)(object)frmViewExportCargo2.lblTitle).Text = (GlobalVariables.IsArabic ? "صادر" : "Export");
		frmViewExportCargo2.ShowDialog();
	}

	private void btnShortPass_Click(object sender, EventArgs e)
	{
		frmViewShortPass frmViewShortPass2 = new frmViewShortPass(_IsSuperVisor: false, CanUpdate);
		frmViewShortPass2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmViewShortPass2.Location = new Point(0, 0);
		((Control)(object)frmViewShortPass2.lblTitle).Text = (GlobalVariables.IsArabic ? " تصريح مؤقت" : "Short Pass");
		frmViewShortPass2.ShowDialog();
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmPROTasksNew));
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
		this.btnKeyboard = new UltraButton();
		this.btnNewTasks = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.btnFinished = new UltraButton();
		this.btnForAllUsers = new UltraButton();
		this.btnRemarks = new UltraButton();
		this.btnExportCargo = new UltraButton();
		this.btnImportCargo = new UltraButton();
		this.btnSignOff = new UltraButton();
		this.btnSignOn = new UltraButton();
		this.btnShortPass = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance7.FontData");
		resources.ApplyResources(val, "appearance7");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnNewTasks, "btnNewTasks");
		((AppearanceBase)val2).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val2, "appearance9");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance9.FontData");
		((SubObjectBase)val2).ForceApplyResources = "|FontData";
		((ControlBase)this.btnNewTasks).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnNewTasks).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewTasks).Name = "btnNewTasks";
		((System.Windows.Forms.Control)(object)this.btnNewTasks).Click += new System.EventHandler(btnNewTasks_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance16");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance16.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance14.FontData");
		resources.ApplyResources(val4, "appearance14");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance15");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance15.FontData");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.btnFinished, "btnFinished");
		((AppearanceBase)val6).Image = resources.GetObject("appearance10.Image");
		resources.ApplyResources(val6, "appearance10");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance10.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((ControlBase)this.btnFinished).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnFinished).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnFinished).Name = "btnFinished";
		((System.Windows.Forms.Control)(object)this.btnFinished).Click += new System.EventHandler(btnFinished_Click);
		resources.ApplyResources(this.btnForAllUsers, "btnForAllUsers");
		((AppearanceBase)val7).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val7, "appearance11");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance11.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((ControlBase)this.btnForAllUsers).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnForAllUsers).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnForAllUsers).Name = "btnForAllUsers";
		((System.Windows.Forms.Control)(object)this.btnForAllUsers).Click += new System.EventHandler(btnForAllUsers_Click);
		resources.ApplyResources(this.btnRemarks, "btnRemarks");
		((AppearanceBase)val8).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val8, "appearance12");
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance12.FontData");
		((SubObjectBase)val8).ForceApplyResources = "|FontData";
		((ControlBase)this.btnRemarks).Appearance = (AppearanceBase)(object)val8;
		((ControlBase)this.btnRemarks).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnRemarks).Name = "btnRemarks";
		((System.Windows.Forms.Control)(object)this.btnRemarks).Click += new System.EventHandler(btnRemarks_Click);
		resources.ApplyResources(this.btnExportCargo, "btnExportCargo");
		((AppearanceBase)val9).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val9, "appearance13");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance13.FontData");
		((SubObjectBase)val9).ForceApplyResources = "|FontData";
		((ControlBase)this.btnExportCargo).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnExportCargo).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnExportCargo).Name = "btnExportCargo";
		((System.Windows.Forms.Control)(object)this.btnExportCargo).Click += new System.EventHandler(btnExportCargo_Click);
		resources.ApplyResources(this.btnImportCargo, "btnImportCargo");
		((AppearanceBase)val10).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val10, "appearance2");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance2.FontData");
		((SubObjectBase)val10).ForceApplyResources = "|FontData";
		((ControlBase)this.btnImportCargo).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnImportCargo).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnImportCargo).Name = "btnImportCargo";
		((System.Windows.Forms.Control)(object)this.btnImportCargo).Click += new System.EventHandler(btnImportCargo_Click);
		resources.ApplyResources(this.btnSignOff, "btnSignOff");
		((AppearanceBase)val11).Image = resources.GetObject("appearance8.Image");
		resources.ApplyResources(val11, "appearance8");
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance8.FontData");
		((SubObjectBase)val11).ForceApplyResources = "|FontData";
		((ControlBase)this.btnSignOff).Appearance = (AppearanceBase)(object)val11;
		((ControlBase)this.btnSignOff).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSignOff).Name = "btnSignOff";
		((System.Windows.Forms.Control)(object)this.btnSignOff).Click += new System.EventHandler(btnSignOff_Click);
		resources.ApplyResources(this.btnSignOn, "btnSignOn");
		((AppearanceBase)val12).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val12, "appearance6");
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance6.FontData");
		((SubObjectBase)val12).ForceApplyResources = "|FontData";
		((ControlBase)this.btnSignOn).Appearance = (AppearanceBase)(object)val12;
		((ControlBase)this.btnSignOn).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSignOn).Name = "btnSignOn";
		((System.Windows.Forms.Control)(object)this.btnSignOn).Click += new System.EventHandler(btnSignOn_Click);
		resources.ApplyResources(this.btnShortPass, "btnShortPass");
		((AppearanceBase)val13).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val13, "appearance4");
		resources.ApplyResources(((AppearanceBase)val13).FontData, "appearance4.FontData");
		((SubObjectBase)val13).ForceApplyResources = "|FontData";
		((ControlBase)this.btnShortPass).Appearance = (AppearanceBase)(object)val13;
		((ControlBase)this.btnShortPass).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnShortPass).Name = "btnShortPass";
		((System.Windows.Forms.Control)(object)this.btnShortPass).Click += new System.EventHandler(btnShortPass_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewTasks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnFinished);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnForAllUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSignOff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnShortPass);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRemarks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSignOn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExportCargo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnImportCargo);
		base.Name = "frmPROTasksNew";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnImportCargo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExportCargo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSignOn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnShortPass, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSignOff, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnForAllUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnFinished, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewTasks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
