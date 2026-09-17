using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmHeaderDetailsSetting : frmBase
{
	private DataRow drForm;

	private DataTable dtSetting;

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnCancel;

	private UltraButton btnSave;

	public UltraButton btnEditColumns;

	private UltraCheckEditor chkAutoDisplaySearch;

	private UltraCheckEditor chkAutoPrint;

	public frmHeaderDetailsSetting(DataRow drform)
	{
		InitializeComponent();
		drForm = drform;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtSetting = FormSetting.SelectByFormID(drForm["FormID"].ToString(), GlobalVariables.UserID, "0", IsFromServer: false);
		if (dtSetting.Rows.Count > 0)
		{
			((UltraToggleEditorBase)chkAutoDisplaySearch).Checked = Convert.ToBoolean(dtSetting.Rows[0]["bit1"]);
			if (!dtSetting.Rows[0]["bit2"].Equals(DBNull.Value))
			{
				((UltraToggleEditorBase)chkAutoPrint).Checked = Convert.ToBoolean(dtSetting.Rows[0]["bit2"]);
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		FormSetting.Insert_Update((dtSetting.Rows.Count == 0) ? "-1" : dtSetting.Rows[0]["FormSettingID"].ToString(), drForm["FormNameAr"].ToString(), drForm["FormNameEn"].ToString(), GlobalVariables.UserID, drForm["FormID"].ToString(), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", ((UltraToggleEditorBase)chkAutoDisplaySearch).Checked ? "1" : "0", ((UltraToggleEditorBase)chkAutoPrint).Checked ? "1" : "0", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmHeaderDetailsSetting));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnEditColumns = new UltraButton();
		this.chkAutoDisplaySearch = new UltraCheckEditor();
		this.chkAutoPrint = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAutoDisplaySearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAutoPrint).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance15");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance15.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
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
		resources.ApplyResources(this.btnEditColumns, "btnEditColumns");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.Update;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnEditColumns).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnEditColumns).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnEditColumns).Name = "btnEditColumns";
		((System.Windows.Forms.Control)(object)this.btnEditColumns).TabStop = false;
		resources.ApplyResources(this.chkAutoDisplaySearch, "chkAutoDisplaySearch");
		((System.Windows.Forms.Control)(object)this.chkAutoDisplaySearch).Name = "chkAutoDisplaySearch";
		resources.ApplyResources(this.chkAutoPrint, "chkAutoPrint");
		((System.Windows.Forms.Control)(object)this.chkAutoPrint).Name = "chkAutoPrint";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAutoPrint);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAutoDisplaySearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEditColumns);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmHeaderDetailsSetting";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEditColumns, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAutoDisplaySearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAutoPrint, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAutoDisplaySearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAutoPrint).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
