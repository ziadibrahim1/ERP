using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmFileName : frmBase
{
	public string FileName = "";

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnSave;

	private UltraTextEditor txtName;

	private UltraLabel lblArName;

	public UltraButton btnKeyboard;

	public frmFileName()
	{
		InitializeComponent();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم الملف", "Please Enter file Name");
			return;
		}
		FileName = ((Control)(object)txtName).Text;
		Close();
	}

	private void txtName_KeyPress(object sender, KeyPressEventArgs e)
	{
		string text = "/\\*:?<>";
		if (text.IndexOf(e.KeyChar) > -1)
		{
			e.Handled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmFileName));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnSave = new UltraButton();
		this.txtName = new UltraTextEditor();
		this.lblArName = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).BeginInit();
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
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtName, "txtName");
		((System.Windows.Forms.Control)(object)this.txtName).Name = "txtName";
		((System.Windows.Forms.Control)(object)this.txtName).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtName_KeyPress);
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
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmFileName";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
