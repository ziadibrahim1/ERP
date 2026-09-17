using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmChangeVessel : frmBase
{
	public string NewVesselID = "";

	private IContainer components = null;

	private UltraLabel lblVessel;

	private UltraComboEditor cboVessels;

	private UltraLabel ultraLabel1;

	public UltraButton btnOK;

	public UltraButton btnCancel;

	public frmChangeVessel(DataTable dtVessels, string _CurrentVesselID)
	{
		InitializeComponent();
		GlobalFunctions.FillCombo(cboVessels, dtVessels, "VesselID", "VesselName");
		((TextEditorControlBase)cboVessels).Value = _CurrentVesselID;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (cboVessels.SelectedIndex > -1)
		{
			NewVesselID = ((TextEditorControlBase)cboVessels).Value.ToString();
		}
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		NewVesselID = "";
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmChangeVessel));
		this.lblVessel = new UltraLabel();
		this.cboVessels = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.btnOK = new UltraButton();
		this.btnCancel = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblVessel, "lblVessel");
		this.lblVessel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessel).Name = "lblVessel";
		((ControlBase)this.lblVessel).WrapText = false;
		resources.ApplyResources(this.cboVessels, "cboVessels");
		((System.Windows.Forms.Control)(object)this.cboVessels).Name = "cboVessels";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.btnOK, "btnOK");
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(btnOK_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnOK;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVessels);
		base.Name = "frmChangeVessel";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
