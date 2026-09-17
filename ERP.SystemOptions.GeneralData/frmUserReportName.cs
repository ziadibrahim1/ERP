using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmUserReportName : frmBase
{
	public string ArName = "";

	public string EnName = "";

	public bool Cancel = false;

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnCancel;

	private UltraButton btnSave;

	private UltraTextEditor txtArName;

	private UltraLabel lblArName;

	public UltraButton btnKeyboard;

	private UltraLabel lblEnName;

	private UltraTextEditor txtEnName;

	public frmUserReportName()
	{
		InitializeComponent();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtArName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم التقرير", "Please Enter Report Name");
		}
		ArName = ((Control)(object)txtArName).Text;
		EnName = ((Control)(object)txtEnName).Text;
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Cancel = true;
		Close();
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmUserReportName));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtArName = new UltraTextEditor();
		this.lblArName = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.lblEnName = new UltraLabel();
		this.txtEnName = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtArName, "txtArName");
		((System.Windows.Forms.Control)(object)this.txtArName).Name = "txtArName";
		this.lblArName.AutoEllipsis = false;
		resources.ApplyResources(this.lblArName, "lblArName");
		((System.Windows.Forms.Control)(object)this.lblArName).Name = "lblArName";
		((ControlBase)this.lblArName).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		this.lblEnName.AutoEllipsis = false;
		resources.ApplyResources(this.lblEnName, "lblEnName");
		((System.Windows.Forms.Control)(object)this.lblEnName).Name = "lblEnName";
		((ControlBase)this.lblEnName).WrapText = false;
		resources.ApplyResources(this.txtEnName, "txtEnName");
		((System.Windows.Forms.Control)(object)this.txtEnName).Name = "txtEnName";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmUserReportName";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
