using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.POS.Transactions;

public class frmSpecialOrder : frmBase
{
	private DataTable dtRoom;

	private IContainer components = null;

	public UltraButton btnClose;

	private Panel panel1;

	public frmSpecialOrder()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtRoom = Rooms.SelectByType("0", "0", "0", "1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		CreateRooms();
	}

	public void CreateRooms()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		Control[] array = panel1.Controls.Find("btnRoom", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			panel1.Controls.Remove(array[i]);
		}
		if (dtRoom == null || dtRoom.Rows.Count == 0)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < dtRoom.Rows.Count; j++)
		{
			Appearance val = new Appearance();
			UltraButton val2 = new UltraButton();
			((AppearanceBase)val).Image = Resources.Hall;
			((AppearanceBase)val).TextHAlignAsString = "Center";
			((AppearanceBase)val).TextVAlignAsString = "Center";
			((AppearanceBase)val).BackColor = Color.Transparent;
			((AppearanceBase)val).BackColor2 = Color.Transparent;
			((AppearanceBase)val).FontData.Bold = (DefaultableBoolean)1;
			((AppearanceBase)val).BackGradientStyle = (GradientStyle)9;
			((AppearanceBase)val).ImageHAlign = (HAlign)2;
			((AppearanceBase)val).ImageVAlign = (VAlign)2;
			((ControlBase)val2).ImageSize = new Size((base.Width - 50) / 4, (base.Height - 30) / 4);
			((ControlBase)val2).Appearance = (AppearanceBase)(object)val;
			((UltraControlBase)val2).UseAppStyling = false;
			((Control)(object)val2).Size = new Size((base.Width - 50) / 4, (base.Height - 30) / 4);
			((Control)(object)val2).Click += btnRoom_Click;
			((Control)(object)val2).Name = "btnRoom";
			((Control)(object)val2).Tag = dtRoom.Rows[j]["RoomID"];
			((Control)(object)val2).Text = dtRoom.Rows[j][GlobalVariables.IsArabic ? "RoomNameAr" : "RoomNameEn"].ToString();
			((Control)(object)val2).TabIndex = j + 10;
			((Control)(object)val2).Font = new Font("Tahoma", 20f);
			((Control)(object)val2).Location = new Point(base.Width * num / 4 + 20, base.Height * num2 / 4 + 20);
			panel1.Controls.Add((Control)(object)val2);
			num++;
			if (num == 4)
			{
				num = 0;
				num2++;
			}
		}
		panel1.SendToBack();
	}

	private void frmRoomDesign_Resize(object sender, EventArgs e)
	{
		CreateRooms();
	}

	private void btnRoom_Click(object sender, EventArgs e)
	{
		frmSpecialOrderChecks frmSpecialOrderChecks2 = new frmSpecialOrderChecks(int.Parse(((Control)sender).Tag.ToString()));
		frmSpecialOrderChecks2.Tag = base.Tag;
		frmSpecialOrderChecks2.MdiParent = base.MdiParent;
		frmSpecialOrderChecks2.TopLevel = false;
		frmSpecialOrderChecks2.Parent = base.Parent;
		frmSpecialOrderChecks2.Width = base.Parent.Width;
		frmSpecialOrderChecks2.Height = base.Parent.Height;
		((Control)(object)frmSpecialOrderChecks2.lblTitle).Text = ((Control)sender).Text;
		frmSpecialOrderChecks2.CanAdd = CanAdd;
		frmSpecialOrderChecks2.CanUpdate = CanUpdate;
		frmSpecialOrderChecks2.CanDelete = CanDelete;
		frmSpecialOrderChecks2.CanDiscount = CanDiscount;
		frmSpecialOrderChecks2.CanSearching = CanSearching;
		frmSpecialOrderChecks2.CanExport = CanExport;
		frmSpecialOrderChecks2.CanPrint = CanPrint;
		frmSpecialOrderChecks2.CanPrintReport = CanPrintReport;
		frmSpecialOrderChecks2.CanViewReport = CanViewReport;
		frmSpecialOrderChecks2.Show();
		frmSpecialOrderChecks2.BringToFront();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmSpecialOrder));
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
		base.Name = "frmSpecialOrder";
		base.Resize += new System.EventHandler(frmRoomDesign_Resize);
		base.Controls.SetChildIndex(this.panel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
