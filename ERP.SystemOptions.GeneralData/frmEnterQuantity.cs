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

public class frmEnterQuantity : frmBase
{
	private string itemName = "";

	private string oldValue = "";

	public string Value = "";

	private IContainer components = null;

	private UltraButton btnClose;

	private UltraButton btnSave;

	private UltraTextEditor txtQty;

	private UltraLabel lblQty;

	public UltraButton btnKeyboard;

	private UltraLabel lblValue;

	private UltraLabel ultraLabel1;

	private UltraLabel lblItemName;

	public frmEnterQuantity()
	{
		InitializeComponent();
	}

	public frmEnterQuantity(string _itemName, string _oldValue)
		: this()
	{
		itemName = _itemName;
		oldValue = _oldValue;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Save();
	}

	private void Save()
	{
		if (((Control)(object)txtQty).Text == "" || decimal.Parse(((Control)(object)txtQty).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show("لابد ان تكون الكمية أكبر من الصفر", "Quantity Must Be Greater Than Zero");
			return;
		}
		if (((Control)(object)txtQty).Text == "")
		{
			GlobalVariables.InformationMB.Show("  لابد من إدخال الكمية ", "  Please Insert  Quantity");
			return;
		}
		Value = ((Control)(object)txtQty).Text;
		base.DialogResult = DialogResult.OK;
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

	private void frmEnterQuantity_Load(object sender, EventArgs e)
	{
		((Control)(object)lblValue).Text = oldValue;
		((Control)(object)lblItemName).Text = itemName;
	}

	private void txtvalue_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtQty_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Save();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmEnterQuantity));
		Appearance val = new Appearance();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtQty = new UltraTextEditor();
		this.lblQty = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.lblValue = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.lblItemName = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtQty, "txtQty");
		((System.Windows.Forms.Control)(object)this.txtQty).Name = "txtQty";
		((System.Windows.Forms.Control)(object)this.txtQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtvalue_KeyPress);
		((System.Windows.Forms.Control)(object)this.txtQty).KeyUp += new System.Windows.Forms.KeyEventHandler(txtQty_KeyUp);
		this.lblQty.AutoEllipsis = false;
		resources.ApplyResources(this.lblQty, "lblQty");
		((System.Windows.Forms.Control)(object)this.lblQty).Name = "lblQty";
		((ControlBase)this.lblQty).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblValue, "lblValue");
		((System.Windows.Forms.Control)(object)this.lblValue).Name = "lblValue";
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.lblItemName, "lblItemName");
		((System.Windows.Forms.Control)(object)this.lblItemName).Name = "lblItemName";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQty);
		base.Name = "frmEnterQuantity";
		base.Load += new System.EventHandler(frmEnterQuantity_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
