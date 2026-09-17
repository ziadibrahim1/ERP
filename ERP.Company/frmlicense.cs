using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using Infragistics.Win.Misc;

namespace ERP.Company;

public class frmlicense : frmBase
{
	private IContainer components = null;

	private UltraButton btnClose;

	public frmlicense()
	{
		InitializeComponent();
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
		this.btnClose = new UltraButton();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)this.btnClose).Location = new System.Drawing.Point(214, 260);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Size = new System.Drawing.Size(75, 23);
		((System.Windows.Forms.Control)(object)this.btnClose).TabIndex = 8;
		((System.Windows.Forms.Control)(object)this.btnClose).Text = "Close";
		base.ClientSize = new System.Drawing.Size(497, 295);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Name = "frmlicense";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.ResumeLayout(false);
	}
}
