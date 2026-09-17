using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmPOSComments : frmBase
{
	private string labelText = "";

	private bool IsInt = false;

	private bool IsNumeric = false;

	public string Value = "";

	private DataTable dtCommentsTypes;

	private DataTable dtComments;

	private IContainer components = null;

	private UltraButton btnClose;

	private UltraButton btnSave;

	private UltraTextEditor txtvalue;

	private UltraLabel lblText;

	public UltraButton btnKeyboard;

	private UltraGroupBox UGBItemData;

	private UltraPanel pnlItems;

	private UltraButton btnClear;

	private UltraGroupBox UGBCommentTypes;

	private UltraPanel pnlGroups;

	public frmPOSComments()
	{
		InitializeComponent();
	}

	public frmPOSComments(string _labelText, bool _IsInt, bool _IsNumeric)
		: this()
	{
		labelText = _labelText;
		IsInt = _IsInt;
		IsNumeric = _IsNumeric;
	}

	public frmPOSComments(string _labelText, bool _IsInt, bool _IsNumeric, string oldValue)
		: this()
	{
		labelText = _labelText;
		IsInt = _IsInt;
		IsNumeric = _IsNumeric;
		((Control)(object)txtvalue).Text = oldValue;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtCommentsTypes = CommentTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtComments = Comment.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		FillItemsGroups();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if ((IsInt || IsNumeric) && (((Control)(object)txtvalue).Text == "" || decimal.Parse(((Control)(object)txtvalue).Text) <= 0m))
		{
			GlobalVariables.InformationMB.Show("لابد ان تكون القيمة أكبر من الصفر", "Value Must Exceed Zero");
			return;
		}
		if (((Control)(object)txtvalue).Text == "")
		{
			GlobalVariables.InformationMB.Show("  لابد من إدخال  " + labelText, "  Please Insert  " + labelText);
			return;
		}
		Value = ((Control)(object)txtvalue).Text;
		Close();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void frmPOSComments_Load(object sender, EventArgs e)
	{
		((Control)(object)lblText).Text = labelText;
	}

	private void txtvalue_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (IsInt)
		{
			GlobalFunctions.CheckForIntegers(sender, e);
		}
		else if (IsNumeric)
		{
			GlobalFunctions.CheckForNumbers(sender, e);
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtvalue).Clear();
	}

	public void FillItemsGroups()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		int num = 8;
		int num2 = 6;
		num = 8;
		num2 = 6;
		for (int i = 0; i < dtCommentsTypes.Rows.Count; i++)
		{
			UltraButton val = new UltraButton();
			((Control)(object)val).Click += btnGroub_Click;
			((Control)(object)val).Tag = dtCommentsTypes.Rows[i]["CommentTypeID"].ToString();
			((Control)(object)val).Text = dtCommentsTypes.Rows[i]["CommentTypeName"].ToString();
			((Control)(object)val).Height += 25;
			((Control)(object)pnlGroups.ClientArea).Controls.Add((Control)(object)val);
			((UltraControlBase)val).Update();
			if (num + ((Control)(object)val).Width > ((Control)(object)pnlGroups).Width)
			{
				num2 += ((Control)(object)val).Height;
				num = 8;
			}
			((Control)(object)val).Left = num;
			((Control)(object)val).Top = num2;
			((Control)(object)val).Width = (((Control)(object)pnlGroups).Width - GlobalVariables.ScrollWidth) / 7;
			num += ((Control)(object)val).Width;
		}
	}

	public void btnGroub_Click(object sender, EventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		((Control)(object)pnlItems).Visible = false;
		((Control)(object)pnlItems.ClientArea).Controls.Clear();
		string text = ((Control)(UltraButton)sender).Tag.ToString();
		DataView dataView = new DataView(dtComments);
		dataView.RowFilter = " CommentTypeID=" + text;
		dataView.ToTable();
		int num = 8;
		int num2 = 6;
		for (int i = 0; i < dataView.Count; i++)
		{
			UltraButton val = new UltraButton();
			((Control)(object)val).Click += btnItems_Click;
			((Control)(object)val).Tag = dataView[i]["Comment"].ToString();
			((Control)(object)val).Text = dataView[i]["CommentAlias"].ToString();
			((Control)(object)val).Height += 40;
			((Control)(object)pnlItems.ClientArea).Controls.Add((Control)(object)val);
			((UltraControlBase)val).Update();
			if (num + ((Control)(object)val).Width > ((Control)(object)pnlItems).Width)
			{
				num2 += ((Control)(object)val).Height;
				num = 8;
			}
			((Control)(object)val).Left = num;
			((Control)(object)val).Top = num2;
			((Control)(object)val).Width = (((Control)(object)pnlItems).Width - GlobalVariables.ScrollWidth) / 6;
			num += ((Control)(object)val).Width;
		}
		((Control)(object)pnlItems).Visible = true;
	}

	public void btnItems_Click(object sender, EventArgs e)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		((Control)(object)txtvalue).Text = ((Control)(object)txtvalue).Text + (((Control)(object)txtvalue).Text.Equals("") ? "" : "\n") + ((Control)(UltraButton)sender).Tag.ToString();
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
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmPOSComments));
		Appearance val = new Appearance();
		this.pnlItems = new UltraPanel();
		this.pnlGroups = new UltraPanel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtvalue = new UltraTextEditor();
		this.lblText = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.UGBItemData = new UltraGroupBox();
		this.btnClear = new UltraButton();
		this.UGBCommentTypes = new UltraGroupBox();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlGroups).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtvalue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBCommentTypes).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBCommentTypes).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		this.pnlItems.AutoScroll = true;
		resources.ApplyResources(this.pnlItems, "pnlItems");
		((System.Windows.Forms.Control)(object)this.pnlItems).Name = "pnlItems";
		this.pnlGroups.AutoScroll = true;
		resources.ApplyResources(this.pnlGroups, "pnlGroups");
		((System.Windows.Forms.Control)(object)this.pnlGroups).Name = "pnlGroups";
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		((UltraButtonBase)this.btnSave).DialogResult = System.Windows.Forms.DialogResult.OK;
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtvalue, "txtvalue");
		((System.Windows.Forms.Control)(object)this.txtvalue).Name = "txtvalue";
		((System.Windows.Forms.Control)(object)this.txtvalue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtvalue_KeyPress);
		this.lblText.AutoEllipsis = false;
		resources.ApplyResources(this.lblText, "lblText");
		((System.Windows.Forms.Control)(object)this.lblText).Name = "lblText";
		((ControlBase)this.lblText).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.UGBItemData, "UGBItemData");
		this.UGBItemData.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItemData).Controls.Add((System.Windows.Forms.Control)(object)this.pnlItems);
		((System.Windows.Forms.Control)(object)this.UGBItemData).Name = "UGBItemData";
		resources.ApplyResources(this.btnClear, "btnClear");
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		resources.ApplyResources(this.UGBCommentTypes, "UGBCommentTypes");
		this.UGBCommentTypes.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBCommentTypes).Controls.Add((System.Windows.Forms.Control)(object)this.pnlGroups);
		((System.Windows.Forms.Control)(object)this.UGBCommentTypes).Name = "UGBCommentTypes";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBCommentTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItemData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtvalue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblText);
		base.Name = "frmPOSComments";
		base.Load += new System.EventHandler(frmPOSComments_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblText, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtvalue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItemData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBCommentTypes, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlGroups).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtvalue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBCommentTypes).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBCommentTypes).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
