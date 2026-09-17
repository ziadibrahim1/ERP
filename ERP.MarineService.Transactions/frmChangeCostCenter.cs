using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmChangeCostCenter : frmBase
{
	public string NewCostCenterID = "";

	private IContainer components = null;

	private UltraLabel lblCostCenter;

	private UltraComboEditor cboCostCenters;

	private UltraLabel ultraLabel1;

	public UltraButton btnOK;

	public UltraButton btnCancel;

	public frmChangeCostCenter(DataTable dtCostCenters, string _CurrentCostCenterID)
	{
		InitializeComponent();
		GlobalFunctions.FillCombo(cboCostCenters, dtCostCenters, "CostCenterID", "Name");
		((TextEditorControlBase)cboCostCenters).Value = _CurrentCostCenterID;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (cboCostCenters.SelectedIndex > -1)
		{
			NewCostCenterID = ((TextEditorControlBase)cboCostCenters).Value.ToString();
		}
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		NewCostCenterID = "";
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmChangeCostCenter));
		this.lblCostCenter = new UltraLabel();
		this.cboCostCenters = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.btnOK = new UltraButton();
		this.btnCancel = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenters).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		resources.ApplyResources(this.cboCostCenters, "cboCostCenters");
		((System.Windows.Forms.Control)(object)this.cboCostCenters).Name = "cboCostCenters";
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
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenters);
		base.Name = "frmChangeCostCenter";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenters, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenters).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
