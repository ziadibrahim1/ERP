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
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.MasterData;

public class frmRoomDesign : frmBase
{
	public frmRooms fRoom;

	private int clickOffsetX;

	private int clickOffsetY;

	private int roomID;

	private DataTable dtRoom;

	private DataTable dtTables;

	private IContainer components = null;

	private Button button1;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraButton btnClose;

	private Panel panel1;

	public frmRoomDesign(int RoomID)
	{
		InitializeComponent();
		roomID = RoomID;
	}

	public override void PrepareData()
	{
		dtRoom = Rooms.Select(roomID.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtTables = Tables.SelectByRoomID(roomID.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtRoom.Rows[0]["RoomImage"] != DBNull.Value)
		{
			panel1.BackgroundImage = GlobalFunctions.BinaryToImage((byte[])dtRoom.Rows[0]["RoomImage"]);
		}
		CreateTables();
	}

	public void CreateTables()
	{
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		Control[] array = panel1.Controls.Find("txtTable", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			panel1.Controls.Remove(array[i]);
		}
		if (dtTables != null && dtTables.Rows.Count != 0)
		{
			double num = Math.Sqrt((double)(base.Height * base.Width) / Convert.ToDouble(2640 * dtTables.Rows.Count * 2));
			if (num > 2.5)
			{
				num = 2.5;
			}
			for (int j = 0; j < dtTables.Rows.Count; j++)
			{
				Appearance val = new Appearance();
				UltraTextEditor val2 = new UltraTextEditor();
				((AppearanceBase)val).BackColor = Color.Transparent;
				((AppearanceBase)val).Image = Resources.CoffeeTable;
				((AppearanceBase)val).ImageHAlign = (HAlign)2;
				((AppearanceBase)val).ImageVAlign = (VAlign)2;
				((AppearanceBase)val).TextHAlignAsString = "Center";
				((AppearanceBase)val).TextVAlignAsString = "Middle";
				((TextEditorControlBase)val2).Appearance = (AppearanceBase)(object)val;
				((Control)(object)val2).BackColor = Color.Transparent;
				val2.Multiline = true;
				((UltraControlBase)val2).UseAppStyling = false;
				((Control)(object)val2).LocationChanged += txt_LocationChanged;
				((Control)(object)val2).MouseDown += txt_MouseDown;
				((Control)(object)val2).MouseMove += txt_MouseMove;
				((Control)(object)val2).Name = "txtTable";
				((Control)(object)val2).Tag = dtTables.Rows[j]["TableID"];
				((Control)(object)val2).Text = dtTables.Rows[j]["TableCode"].ToString();
				((Control)(object)val2).TabIndex = j + 10;
				((Control)(object)val2).Font = new Font("Tahoma", (num < 1.0) ? 10f : ((num < 1.25) ? 12f : ((num < 1.5) ? 16f : ((num < 2.0) ? 20f : 28f))));
				((Control)(object)val2).Location = new Point((int)((decimal)base.Width * Convert.ToDecimal(dtTables.Rows[j]["X"])), (int)((decimal)base.Height * Convert.ToDecimal(dtTables.Rows[j]["Y"])));
				((Control)(object)val2).Size = new Size((int)(num * 60.0), (int)(num * 44.0));
				panel1.Controls.Add((Control)(object)val2);
			}
			panel1.SendToBack();
		}
	}

	private void pnl_DragOver(object sender, DragEventArgs e)
	{
		e.Effect = DragDropEffects.Move;
		if (e.Data.GetData(e.Data.GetFormats()[0]) is Control control)
		{
			control.Location = panel1.PointToClient(new Point(e.X - clickOffsetX, e.Y - clickOffsetY));
		}
	}

	private void pnl_Click(object sender, EventArgs e)
	{
		base.ActiveControl = null;
	}

	private void btn_Click(object sender, EventArgs e)
	{
		base.ActiveControl = null;
	}

	private void txt_MouseDown(object sender, MouseEventArgs e)
	{
		((Control)sender).DoDragDrop(sender, DragDropEffects.Move);
	}

	private void txt_MouseMove(object sender, MouseEventArgs e)
	{
		clickOffsetX = e.X;
		clickOffsetY = e.Y;
	}

	private void txt_LocationChanged(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((TextEditorControlBase)(UltraTextEditor)sender).Editor.ExitEditMode(true, true);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Control[] array = panel1.Controls.Find("txtTable", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			DataRow dataRow = dtTables.Select("TableID=" + array[i].Tag)[0];
			dataRow["TableCode"] = array[i].Text;
			dataRow["X"] = Convert.ToDecimal(array[i].Location.X) / (decimal)base.Width;
			dataRow["Y"] = Convert.ToDecimal(array[i].Location.Y) / (decimal)base.Height;
		}
		Tables.Insert_UpdateByTable(dtTables, GlobalVariables.UserID, IsFromServer: true);
		if (!fRoom.IsDisposed)
		{
			fRoom.FillData();
		}
		Dispose();
	}

	private void frmRoomDesign_Resize(object sender, EventArgs e)
	{
		CreateTables();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		CreateTables();
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
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmRoomDesign));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.button1 = new System.Windows.Forms.Button();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.button1, "button1");
		this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.button1.Name = "button1";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(btn_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val, "appearance14");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance14.FontData");
		((SubObjectBase)val).ForceApplyResources = "|FontData";
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val2, "appearance19");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance19.FontData");
		((SubObjectBase)val2).ForceApplyResources = "|FontData";
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val3, "appearance1");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance1.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.AllowDrop = true;
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		this.panel1.Name = "panel1";
		this.panel1.Click += new System.EventHandler(pnl_Click);
		this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(pnl_DragOver);
		resources.ApplyResources(this, "$this");
		this.AllowDrop = true;
		this.BackgroundImage = ERP.Properties.Resources.CoffeeTable;
		base.Controls.Add(this.button1);
		base.Controls.Add(this.panel1);
		this.DoubleBuffered = true;
		base.Name = "frmRoomDesign";
		base.Resize += new System.EventHandler(frmRoomDesign_Resize);
		base.Controls.SetChildIndex(this.panel1, 0);
		base.Controls.SetChildIndex(this.button1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
