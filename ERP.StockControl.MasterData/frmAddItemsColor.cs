using System;
using System.ComponentModel;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.MasterData;

public class frmAddItemsColor : frmBase
{
	private int ItemID;

	public int ColorID;

	private IContainer components = null;

	private UltraLabel lblSerial;

	private UltraTextEditor txtSerial;

	private UltraButton btnSave;

	private UltraButton btnClose;

	public frmAddItemsColor()
	{
		InitializeComponent();
	}

	public frmAddItemsColor(int ID)
		: this()
	{
		ItemID = ID;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (Main.CheckForValue("G_Colors", "ColorCode", ((Control)(object)txtSerial).Text, "", IsFromServer: false) > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? " متواجد من قبل" : "  Already Exist");
			return;
		}
		ColorID = Colors.Insert_Update("-1", ((Control)(object)txtSerial).Text, ((Control)(object)txtSerial).Text, ((Control)(object)txtSerial).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
		Close();
	}

	private void btnClose_Click(object sender, EventArgs e)
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmAddItemsColor));
		this.lblSerial = new UltraLabel();
		this.txtSerial = new UltraTextEditor();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		this.lblSerial.AutoEllipsis = false;
		resources.ApplyResources(this.lblSerial, "lblSerial");
		((System.Windows.Forms.Control)(object)this.lblSerial).Name = "lblSerial";
		((ControlBase)this.lblSerial).WrapText = false;
		resources.ApplyResources(this.txtSerial, "txtSerial");
		((System.Windows.Forms.Control)(object)this.txtSerial).Name = "txtSerial";
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSerial);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Name = "frmAddItemsColor";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSerial, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSerial).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
