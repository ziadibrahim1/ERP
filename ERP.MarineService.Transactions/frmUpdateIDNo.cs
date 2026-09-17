using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmUpdateIDNo : frmBase
{
	private string labelText = "";

	private string ColName = "";

	private string CurrentValue = "";

	public string Value = "";

	private string SubAccountID;

	private string SubAccountTypeID;

	private IContainer components = null;

	private UltraButton btnClose;

	private UltraButton btnSave;

	private UltraTextEditor txtvalue;

	private UltraLabel lblText;

	public UltraButton btnKeyboard;

	public frmUpdateIDNo()
	{
		InitializeComponent();
	}

	public frmUpdateIDNo(string _SubAccountID, string _SubAccountTypeID, string _labelText, string _ColName, string _CurrentValue)
		: this()
	{
		labelText = _labelText;
		ColName = _ColName;
		SubAccountID = _SubAccountID;
		CurrentValue = _CurrentValue;
		SubAccountTypeID = _SubAccountTypeID;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((Control)(object)txtvalue).Text == "")
		{
			GlobalVariables.InformationMB.Show("  لابد من إدخال  " + labelText, "  Please Insert  " + labelText);
			return;
		}
		Value = ((Control)(object)txtvalue).Text;
		if (!(SubAccountTypeID == GlobalVariables.AgentSubAccountTypeIDs.Replace(",", "")))
		{
			if (SubAccountTypeID == GlobalVariables.OwnerSubAccountTypeIDs.Replace(",", ""))
			{
				Main.ExecuteNonQuery("Update Owners Set " + ColName + " = '" + Value.ToString() + "' Where SubAccountID = " + SubAccountID);
			}
			else if (SubAccountTypeID == GlobalVariables.CaptainSubAccountTypeIDs.Replace(",", ""))
			{
				Main.ExecuteNonQuery("Update Captains Set " + ColName + " = '" + Value.ToString() + "' Where SubAccountID = " + SubAccountID);
			}
			else if (SubAccountTypeID == GlobalVariables.SeaManSubAccountTypeIDs.Replace(",", ""))
			{
				Main.ExecuteNonQuery("Update MS_SeaMen Set " + ColName + " = '" + Value.ToString() + "' Where SubAccountID = " + SubAccountID);
			}
			else if (SubAccountTypeID == GlobalVariables.CharterSubAccountTypeIDs.Replace(",", ""))
			{
				Main.ExecuteNonQuery("Update Charters Set " + ColName + " = '" + Value.ToString() + "' Where SubAccountID = " + SubAccountID);
			}
		}
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

	private void frmUpdateIDNo_Load(object sender, EventArgs e)
	{
		((Control)(object)lblText).Text = labelText;
		((Control)(object)txtvalue).Text = CurrentValue;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateIDNo));
		Appearance val = new Appearance();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.txtvalue = new UltraTextEditor();
		this.lblText = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalue).BeginInit();
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
		resources.ApplyResources(this.txtvalue, "txtvalue");
		((System.Windows.Forms.Control)(object)this.txtvalue).Name = "txtvalue";
		resources.ApplyResources(this.lblText, "lblText");
		this.lblText.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblText).Name = "lblText";
		((ControlBase)this.lblText).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtvalue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblText);
		base.Name = "frmUpdateIDNo";
		base.Load += new System.EventHandler(frmUpdateIDNo_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblText, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtvalue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalue).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
