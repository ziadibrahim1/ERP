using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Company;

public class frmLoginBranch : frmBase
{
	private IContainer components = null;

	private UltraLabel lblGroup;

	private UltraComboEditor cboBranches;

	private UltraLabel ultraLabel1;

	public UltraButton btnOK;

	public frmLoginBranch(DataTable dtUserBranches, string DefaultBranchID)
	{
		InitializeComponent();
		GlobalFunctions.FillCombo(cboBranches, dtUserBranches, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
		if (DefaultBranchID != "-1")
		{
			((TextEditorControlBase)cboBranches).Value = DefaultBranchID;
		}
	}

	private void frmLoginBranch_Load(object sender, EventArgs e)
	{
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (cboBranches.SelectedIndex > -1)
		{
			GlobalVariables.CurrentBranchID = ((TextEditorControlBase)cboBranches).Value.ToString();
			Close();
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		this.lblGroup = new UltraLabel();
		this.cboBranches = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.btnOK = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(353, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 130);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(351, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 130);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(351, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 128);
		((System.Windows.Forms.Control)(object)this.lblGroup).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblGroup.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroup).AutoSize = true;
		this.lblGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblGroup).Location = new System.Drawing.Point(37, 47);
		((System.Windows.Forms.Control)(object)this.lblGroup).Name = "lblGroup";
		((System.Windows.Forms.Control)(object)this.lblGroup).Size = new System.Drawing.Size(47, 18);
		((System.Windows.Forms.Control)(object)this.lblGroup).TabIndex = 14;
		((System.Windows.Forms.Control)(object)this.lblGroup).Text = "Branch";
		((ControlBase)this.lblGroup).WrapText = false;
		((System.Windows.Forms.Control)(object)this.cboBranches).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		((System.Windows.Forms.Control)(object)this.cboBranches).Location = new System.Drawing.Point(109, 44);
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		((System.Windows.Forms.Control)(object)this.cboBranches).Size = new System.Drawing.Size(197, 25);
		((System.Windows.Forms.Control)(object)this.cboBranches).TabIndex = 13;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).AutoSize = true;
		this.ultraLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Location = new System.Drawing.Point(75, 12);
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Size = new System.Drawing.Size(174, 18);
		((System.Windows.Forms.Control)(object)this.ultraLabel1).TabIndex = 15;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Text = "Select Your Working Branch";
		((UltraButtonBase)this.btnOK).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnOK).Location = new System.Drawing.Point(109, 90);
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).Size = new System.Drawing.Size(134, 30);
		((System.Windows.Forms.Control)(object)this.btnOK).TabIndex = 16;
		((System.Windows.Forms.Control)(object)this.btnOK).Text = "OK";
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(btnOK_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnOK;
		base.ClientSize = new System.Drawing.Size(353, 132);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroup);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Name = "frmLoginBranch";
		base.Load += new System.EventHandler(frmLoginBranch_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroup, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
