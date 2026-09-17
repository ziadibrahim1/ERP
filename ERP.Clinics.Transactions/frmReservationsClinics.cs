using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Clinics;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.Clinics.Transactions;

public class frmReservationsClinics : frmBase
{
	private DataTable dtClinics;

	private IContainer components = null;

	public UltraButton btnClose;

	private Panel panel1;

	public frmReservationsClinics()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		CreateClinics();
	}

	public void CreateClinics()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		Control[] array = panel1.Controls.Find("btnClinics", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			panel1.Controls.Remove(array[i]);
		}
		if (dtClinics == null || dtClinics.Rows.Count == 0)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < dtClinics.Rows.Count; j++)
		{
			Appearance val = new Appearance();
			UltraButton val2 = new UltraButton();
			((AppearanceBase)val).TextHAlignAsString = "Center";
			((AppearanceBase)val).TextVAlignAsString = "Center";
			((AppearanceBase)val).BackColor = Color.Transparent;
			((AppearanceBase)val).BackColor2 = Color.Transparent;
			((AppearanceBase)val).FontData.Bold = (DefaultableBoolean)1;
			((AppearanceBase)val).BackGradientStyle = (GradientStyle)9;
			((AppearanceBase)val).ImageHAlign = (HAlign)2;
			((AppearanceBase)val).ImageVAlign = (VAlign)2;
			((ControlBase)val2).Appearance = (AppearanceBase)(object)val;
			((UltraControlBase)val2).UseAppStyling = false;
			((Control)(object)val2).Size = new Size((base.Width - 50) / 6, (base.Height - 30) / 6);
			((Control)(object)val2).Click += btnClinic_Click;
			((Control)(object)val2).Name = "btnClinics";
			((Control)(object)val2).Tag = dtClinics.Rows[j]["ClinicID"];
			((Control)(object)val2).Text = dtClinics.Rows[j]["ClinicName"].ToString();
			((Control)(object)val2).TabIndex = j + 10;
			((Control)(object)val2).Font = new Font("Tahoma", 20f);
			((Control)(object)val2).Location = new Point(base.Width * num / 6 + 20, base.Height * num2 / 6 + 20);
			panel1.Controls.Add((Control)(object)val2);
			num++;
			if (num == 6)
			{
				num = 0;
				num2++;
			}
		}
		panel1.SendToBack();
	}

	private void frmClinicDesign_Resize(object sender, EventArgs e)
	{
		CreateClinics();
	}

	private void btnClinic_Click(object sender, EventArgs e)
	{
		frmReservationSchedules frmReservationSchedules2 = new frmReservationSchedules(int.Parse(((Control)sender).Tag.ToString()));
		frmReservationSchedules2.Tag = base.Tag;
		frmReservationSchedules2.MdiParent = base.MdiParent;
		frmReservationSchedules2.TopLevel = false;
		frmReservationSchedules2.Parent = base.Parent;
		frmReservationSchedules2.Width = base.Parent.Width;
		frmReservationSchedules2.Height = base.Parent.Height;
		frmReservationSchedules2.CanAdd = CanAdd;
		frmReservationSchedules2.CanUpdate = CanUpdate;
		frmReservationSchedules2.CanDelete = CanDelete;
		frmReservationSchedules2.CanDiscount = CanDiscount;
		frmReservationSchedules2.CanSearching = CanSearching;
		frmReservationSchedules2.CanExport = CanExport;
		frmReservationSchedules2.CanPrint = CanPrint;
		frmReservationSchedules2.CanPrintReport = CanPrintReport;
		frmReservationSchedules2.CanViewReport = CanViewReport;
		frmReservationSchedules2.Show();
		frmReservationSchedules2.BringToFront();
		Dispose();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.Transactions.frmReservationsClinics));
		Appearance val = new Appearance();
		this.btnClose = new UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		this.panel1.AllowDrop = true;
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		this.panel1.Name = "panel1";
		this.AllowDrop = true;
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.panel1);
		base.Name = "frmReservationsClinics";
		base.Resize += new System.EventHandler(frmClinicDesign_Resize);
		base.Controls.SetChildIndex(this.panel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
