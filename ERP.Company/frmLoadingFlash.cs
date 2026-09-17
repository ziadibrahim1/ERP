using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Properties;

namespace ERP.Company;

public class frmLoadingFlash : frmBase
{
	private IContainer components = null;

	private PictureBox pictureBox1;

	public frmLoadingFlash()
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
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(480, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 358);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(478, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 358);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(478, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 356);
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.pictureBox1.Image = ERP.Properties.Resources.ERP_Loading;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(480, 360);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox1.TabIndex = 8;
		this.pictureBox1.TabStop = false;
		this.BackColor = System.Drawing.Color.White;
		base.ClientSize = new System.Drawing.Size(480, 360);
		base.Controls.Add(this.pictureBox1);
		base.Name = "frmLoadingFlash";
		base.Opacity = 0.9;
		base.Controls.SetChildIndex(this.pictureBox1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
