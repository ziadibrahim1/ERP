using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmFormSettingName : frmBase
{
	public int SettingID = 0;

	public string FormID;

	public bool Cancel = false;

	private DataTable dtSetting;

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnCancel;

	private UltraButton btnSave;

	private UltraTextEditor txtArName;

	public UltraButton btnKeyboard;

	private UltraTextEditor txtEnName;

	private RadioButton rbUpdate;

	private RadioButton rbNew;

	private UltraLabel lblArName;

	private UltraLabel ultraLabel1;

	protected internal CheckedListBox clbSettings;

	public frmFormSettingName(string SelectedSettingID, DataTable dt_Setting, string formID)
	{
		FormID = formID;
		InitializeComponent();
		dtSetting = dt_Setting;
		clbSettings.SelectedValueChanged -= clbSettings_SelectedValueChanged;
		Main.Fillclb(clbSettings, dtSetting, "FormSettingID", "SettingName");
		clbSettings.SelectedValueChanged += clbSettings_SelectedValueChanged;
		if (SelectedSettingID == "0")
		{
			rbNew.Checked = true;
		}
		else
		{
			for (int i = 0; i < dtSetting.Rows.Count; i++)
			{
				clbSettings.SetItemChecked(i, dtSetting.Rows[i]["FormSettingID"].ToString() == SelectedSettingID);
			}
		}
		SettingID = Convert.ToInt32(SelectedSettingID);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (rbNew.Checked)
		{
			if (((Control)(object)txtArName).Text == "")
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال اسم الإعداد", "Please Enter Setting Name");
			}
			SettingID = FormSetting.Insert_Update("-1", ((Control)(object)txtArName).Text, ((Control)(object)txtEnName).Text, GlobalVariables.UserID, FormID, "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
		}
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

	private void clbSettings_SelectedValueChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < clbSettings.Items.Count; i++)
		{
			clbSettings.SetItemChecked(i, i == clbSettings.SelectedIndex);
			if (i == clbSettings.SelectedIndex)
			{
				SettingID = Convert.ToInt32(dtSetting.Rows[i]["FormSettingID"]);
			}
		}
	}

	private void rbNew_CheckedChanged(object sender, EventArgs e)
	{
		clbSettings.Enabled = !rbNew.Checked;
		((Control)(object)txtArName).Enabled = rbNew.Checked;
		((Control)(object)txtEnName).Enabled = rbNew.Checked;
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
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmFormSettingName));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtArName = new UltraTextEditor();
		this.btnKeyboard = new UltraButton();
		this.txtEnName = new UltraTextEditor();
		this.rbUpdate = new System.Windows.Forms.RadioButton();
		this.rbNew = new System.Windows.Forms.RadioButton();
		this.lblArName = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.clbSettings = new System.Windows.Forms.CheckedListBox();
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
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.txtEnName, "txtEnName");
		((System.Windows.Forms.Control)(object)this.txtEnName).Name = "txtEnName";
		resources.ApplyResources(this.rbUpdate, "rbUpdate");
		this.rbUpdate.Checked = true;
		this.rbUpdate.Name = "rbUpdate";
		this.rbUpdate.TabStop = true;
		this.rbUpdate.UseVisualStyleBackColor = true;
		this.rbUpdate.CheckedChanged += new System.EventHandler(rbNew_CheckedChanged);
		resources.ApplyResources(this.rbNew, "rbNew");
		this.rbNew.Name = "rbNew";
		this.rbNew.UseVisualStyleBackColor = true;
		this.rbNew.CheckedChanged += new System.EventHandler(rbNew_CheckedChanged);
		this.lblArName.AutoEllipsis = false;
		resources.ApplyResources(this.lblArName, "lblArName");
		((System.Windows.Forms.Control)(object)this.lblArName).Name = "lblArName";
		((ControlBase)this.lblArName).WrapText = false;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.clbSettings, "clbSettings");
		this.clbSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbSettings.CheckOnClick = true;
		this.clbSettings.MultiColumn = true;
		this.clbSettings.Name = "clbSettings";
		this.clbSettings.SelectedValueChanged += new System.EventHandler(clbSettings_SelectedValueChanged);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.clbSettings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArName);
		base.Controls.Add(this.rbNew);
		base.Controls.Add(this.rbUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmFormSettingName";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex(this.rbUpdate, 0);
		base.Controls.SetChildIndex(this.rbNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex(this.clbSettings, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
