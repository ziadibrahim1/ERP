using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.POS.Transactions;

public class frmRoomManagement : frmBase
{
	private int roomID;

	private DataTable dtRoom;

	private DataTable dtRoomTables;

	private DataTable dtRoomChecks;

	private DataTable dtRoomReservations;

	private DataTable dtPOSSettings;

	private int CheckDelayAlert;

	private int PrintDelayAlert;

	private IContainer components = null;

	public UltraButton btnClose;

	private ToolTip toolTip1;

	private ToolTip toolTip2;

	private Panel panel1;

	private Timer timer1;

	public UltraLabel ultraLabel2;

	public UltraLabel ultraLabel1;

	public UltraLabel lblHeader;

	public UltraLabel ultraLabel6;

	public UltraLabel ultraLabel5;

	public UltraLabel ultraLabel4;

	public UltraLabel ultraLabel8;

	public UltraLabel ultraLabel7;

	public UltraLabel ultraLabel9;

	public UltraLabel ultraLabel3;

	public UltraLabel lblTitle;

	public frmRoomManagement(int RoomID)
	{
		InitializeComponent();
		roomID = RoomID;
	}

	public override void PrepareData()
	{
		dtPOSSettings = RoomsSettings.SelectByRoomIDWithoutImage(roomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtPOSSettings.Rows.Count > 0 && dtPOSSettings.Rows[0]["CheckDelayAlert"] != DBNull.Value && dtPOSSettings.Rows[0]["CheckDelayAlert"].ToString() != "0")
		{
			CheckDelayAlert = Convert.ToInt32(dtPOSSettings.Rows[0]["CheckDelayAlert"]);
		}
		else
		{
			CheckDelayAlert = 10000;
		}
		if (dtPOSSettings.Rows.Count > 0 && dtPOSSettings.Rows[0]["PrintDelayAlert"] != DBNull.Value && dtPOSSettings.Rows[0]["PrintDelayAlert"].ToString() != "0")
		{
			PrintDelayAlert = Convert.ToInt32(dtPOSSettings.Rows[0]["PrintDelayAlert"]);
		}
		else
		{
			PrintDelayAlert = 10000;
		}
		dtRoom = Rooms.Select(roomID.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtRoomTables = Tables.SelectByRoomID(roomID.ToString(), "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtRoom.Rows[0]["RoomImage"] != DBNull.Value)
		{
			panel1.BackgroundImage = GlobalFunctions.BinaryToImage((byte[])dtRoom.Rows[0]["RoomImage"]);
		}
		CreateTables();
		timer1.Start();
	}

	public void CreateTables()
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		Control[] array = panel1.Controls.Find("lblTable", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			panel1.Controls.Remove(array[i]);
		}
		if (dtRoomTables != null && dtRoomTables.Rows.Count != 0)
		{
			double num = Math.Sqrt((double)(base.Height * base.Width) / Convert.ToDouble(2640 * int.Parse(dtRoom.Rows[0]["TablesCount"].ToString()) * 2));
			if (num > 2.5)
			{
				num = 2.5;
			}
			for (int j = 0; j < dtRoomTables.Rows.Count; j++)
			{
				Appearance val = new Appearance();
				UltraLabel val2 = new UltraLabel();
				((AppearanceBase)val).ImageBackground = GetImage(0);
				((AppearanceBase)val).ImageBackgroundStyle = (ImageBackgroundStyle)2;
				((AppearanceBase)val).TextHAlign = (HAlign)2;
				((AppearanceBase)val).TextVAlign = (VAlign)3;
				((AppearanceBase)val).BackColor = Color.Transparent;
				((AppearanceBase)val).BackColor2 = Color.Transparent;
				((AppearanceBase)val).FontData.Bold = (DefaultableBoolean)1;
				((AppearanceBase)val).BackGradientStyle = (GradientStyle)9;
				((ControlBase)val2).Appearance = (AppearanceBase)(object)val;
				((UltraControlBase)val2).UseAppStyling = false;
				((Control)(object)val2).Click += lblTable_Click;
				((Control)(object)val2).Name = "lblTable";
				((Control)(object)val2).Tag = dtRoomTables.Rows[j]["TableID"];
				((Control)(object)val2).TabIndex = j + 10;
				((Control)(object)val2).Font = new Font("Tahoma", (num < 1.0) ? 9f : ((num < 1.25) ? 10f : ((num < 1.5) ? 11f : ((num < 2.0) ? 13f : ((num < 2.5) ? 14f : 16f)))));
				((Control)(object)val2).Location = new Point((int)((decimal)base.Width * Convert.ToDecimal(dtRoomTables.Rows[j]["X"])), (int)((decimal)(base.Height - 40) * Convert.ToDecimal(dtRoomTables.Rows[j]["Y"])));
				((Control)(object)val2).Size = new Size((int)(num * 70.0) + 14, (int)(num * 40.0) + 9);
				panel1.Controls.Add((Control)(object)val2);
			}
			panel1.SendToBack();
			TableSetState();
		}
	}

	private void TableSetState()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		DateTime serverDateTimeNow = GlobalFunctions.GetServerDateTimeNow();
		dtRoomChecks = Checks.SelectWithTableByRoomID(roomID.ToString());
		dtRoomReservations = TablesReservation.SelectByRoomID(roomID.ToString());
		Control[] array = panel1.Controls.Find("lblTable", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			UltraLabel val = (UltraLabel)array[i];
			DataRow[] array2 = dtRoomChecks.Select("TableID=" + ((Control)(object)val).Tag.ToString());
			DataRow[] array3 = dtRoomReservations.Select("TableID=" + ((Control)(object)val).Tag.ToString());
			DataRow[] array4 = dtRoomTables.Select("TableID=" + ((Control)(object)val).Tag.ToString());
			((Control)(object)val).Text = "";
			Color backColor = ((ControlBase)val).Appearance.BackColor;
			((ControlBase)val).Appearance.BackColor = Color.Transparent;
			((ControlBase)val).Appearance.ImageBackground = GetImage(0);
			toolTip2.SetToolTip((Control)(object)val, null);
			toolTip1.SetToolTip((Control)(object)val, null);
			if (array2.Length != 0)
			{
				string text = "";
				int num = 0;
				for (int j = 0; j < array2.Length; j++)
				{
					text = text + "\nCheck No. " + array2[j]["CheckNo"];
					num += Convert.ToInt32(array2[j]["PersonCount"]);
				}
				toolTip2.SetToolTip((Control)(object)val, text);
				((ControlBase)val).Appearance.ImageBackground = GetImage(num);
				if (dtRoomChecks.Select("IsPrinted=0 and TableID=" + ((Control)(object)val).Tag.ToString()).Length != 0)
				{
					if (serverDateTimeNow > Convert.ToDateTime(array2[0]["CheckDate"]).AddMinutes(CheckDelayAlert) && backColor == Color.Red)
					{
						((ControlBase)val).Appearance.BackColor = Color.Yellow;
					}
					else
					{
						((ControlBase)val).Appearance.BackColor = Color.Red;
					}
					((Control)(object)val).Text = ((Control)(object)val).Text + (serverDateTimeNow - Convert.ToDateTime(array2[0]["CheckDate"])).ToString("hh\\:mm") + "\n";
				}
				else
				{
					if (serverDateTimeNow > Convert.ToDateTime(array2[0]["PrintDate"]).AddMinutes(PrintDelayAlert) && backColor == Color.Green)
					{
						((ControlBase)val).Appearance.BackColor = Color.Yellow;
					}
					else
					{
						((ControlBase)val).Appearance.BackColor = Color.Green;
					}
					((Control)(object)val).Text = ((Control)(object)val).Text + (serverDateTimeNow - Convert.ToDateTime(array2[0]["PrintDate"])).ToString("hh\\:mm") + "\n";
				}
			}
			if (array3.Length != 0)
			{
				string text2 = "";
				for (int k = 0; k < array3.Length; k++)
				{
					text2 = string.Concat(text2, "\n No. ", array3[k]["ReservationNo"], "   From ", array3[k]["StartTime"], "   To ", array3[k]["EndTime"], "\n    ", array3[k]["ClientName"], "   ", array3[k]["Notes"]);
				}
				toolTip1.SetToolTip((Control)(object)val, text2);
				if (array2.Length == 0)
				{
					((ControlBase)val).Appearance.BackColor = GetReservationColor(Convert.ToDateTime(array3[0]["StartDate"]), serverDateTimeNow);
				}
			}
			((Control)(object)val).Text += array4[0]["TableCode"].ToString();
		}
	}

	private Color GetReservationColor(DateTime date, DateTime currentDate)
	{
		if (date > currentDate.AddHours(3.0))
		{
			return Color.FromArgb(108, 188, 255);
		}
		if (date > currentDate.AddHours(1.0))
		{
			return Color.FromArgb(0, 77, 232);
		}
		return Color.FromArgb(0, 0, 150);
	}

	private Bitmap GetImage(int PersonCount)
	{
		if (PersonCount == 0)
		{
			return Resources.CoffeeTable;
		}
		if (PersonCount == 1)
		{
			return Resources.CoffeeTable1;
		}
		if (PersonCount == 2)
		{
			return Resources.CoffeeTable2;
		}
		if (PersonCount == 3)
		{
			return Resources.CoffeeTable3;
		}
		if (PersonCount == 4)
		{
			return Resources.CoffeeTable4;
		}
		if (PersonCount == 5)
		{
			return Resources.CoffeeTable5;
		}
		if (PersonCount == 6)
		{
			return Resources.CoffeeTable6;
		}
		if (PersonCount == 7)
		{
			return Resources.CoffeeTable7;
		}
		if (PersonCount > 7)
		{
			return Resources.CoffeeTable8;
		}
		return Resources.CoffeeTable;
	}

	private void lblTable_Click(object sender, EventArgs e)
	{
		int num = int.Parse(((Control)sender).Tag.ToString());
		string text = ((Control)sender).Text;
		DataRow[] array = dtRoomChecks.Select("TableID=" + num);
		frmBase frmBase2 = null;
		if (array.Length == 0)
		{
			frmBase2 = new frmChecks(-1, num, roomID, text);
		}
		else if (array.Length == 1)
		{
			frmBase2 = new frmChecks(int.Parse(array[0]["CheckID"].ToString()), num, roomID, text);
		}
		if (array.Length > 1)
		{
			frmBase2 = new frmSelectCheck(num, roomID, text);
		}
		else
		{
			frmBase2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		}
		frmBase2.Tag = base.Tag;
		frmBase2.Location = new Point(0, 0);
		frmBase2.CanAdd = CanAdd;
		frmBase2.CanUpdate = CanUpdate;
		frmBase2.CanDelete = CanDelete;
		frmBase2.CanDiscount = CanDiscount;
		frmBase2.CanSearching = CanSearching;
		frmBase2.CanExport = CanExport;
		frmBase2.CanPrint = CanPrint;
		frmBase2.CanPrintReport = CanPrintReport;
		frmBase2.CanViewReport = CanViewReport;
		frmBase2.CanMinimunCharge = CanMinimunCharge;
		frmBase2.ShowDialog();
	}

	public void frmRoomDesign_Resize(object sender, EventArgs e)
	{
		CreateTables();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		TableSetState();
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmRoomManagement));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		this.btnClose = new UltraButton();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.panel1 = new System.Windows.Forms.Panel();
		this.lblTitle = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.ultraLabel8 = new UltraLabel();
		this.ultraLabel7 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel9 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.lblHeader = new UltraLabel();
		this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblTop, resources.GetString("lblTop.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)base.lblTop, resources.GetString("lblTop.ToolTip1"));
		resources.ApplyResources(base.lblBottom, "lblBottom");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblBottom, resources.GetString("lblBottom.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)base.lblBottom, resources.GetString("lblBottom.ToolTip1"));
		resources.ApplyResources(base.lblLeft, "lblLeft");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblLeft, resources.GetString("lblLeft.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)base.lblLeft, resources.GetString("lblLeft.ToolTip1"));
		resources.ApplyResources(base.lblRight, "lblRight");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblRight, resources.GetString("lblRight.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)base.lblRight, resources.GetString("lblRight.ToolTip1"));
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.btnClose, resources.GetString("btnClose.ToolTip"));
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.btnClose, resources.GetString("btnClose.ToolTip1"));
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		this.toolTip1.ToolTipTitle = "Reserved";
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.AllowDrop = true;
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.lblHeader);
		this.panel1.Name = "panel1";
		this.toolTip2.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
		this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip1"));
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblTitle, resources.GetString("lblTitle.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.lblTitle, resources.GetString("lblTitle.ToolTip1"));
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.ultraLabel6).Appearance = (AppearanceBase)(object)val3;
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel6, resources.GetString("ultraLabel6.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel6, resources.GetString("ultraLabel6.ToolTip1"));
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(108, 188, 255);
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.White;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.ultraLabel8).Appearance = (AppearanceBase)(object)val4;
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel8, resources.GetString("ultraLabel8.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel8, resources.GetString("ultraLabel8.ToolTip1"));
		((UltraControlBase)this.ultraLabel8).UseAppStyling = false;
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(0, 77, 232);
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.White;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.ultraLabel7).Appearance = (AppearanceBase)(object)val5;
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel7, resources.GetString("ultraLabel7.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel7, resources.GetString("ultraLabel7.ToolTip1"));
		((UltraControlBase)this.ultraLabel7).UseAppStyling = false;
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(0, 0, 150);
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.White;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel2, resources.GetString("ultraLabel2.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel2, resources.GetString("ultraLabel2.ToolTip1"));
		((UltraControlBase)this.ultraLabel2).UseAppStyling = false;
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.ultraLabel9).Appearance = (AppearanceBase)(object)val7;
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel9, resources.GetString("ultraLabel9.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel9, resources.GetString("ultraLabel9.ToolTip1"));
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val8;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel5, resources.GetString("ultraLabel5.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel5, resources.GetString("ultraLabel5.ToolTip1"));
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Yellow;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel3, resources.GetString("ultraLabel3.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel3, resources.GetString("ultraLabel3.ToolTip1"));
		((UltraControlBase)this.ultraLabel3).UseAppStyling = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.ultraLabel4).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel4, resources.GetString("ultraLabel4.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel4, resources.GetString("ultraLabel4.ToolTip1"));
		((UltraControlBase)this.ultraLabel4).UseAppStyling = false;
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Green;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel1, resources.GetString("ultraLabel1.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.ultraLabel1, resources.GetString("ultraLabel1.ToolTip1"));
		((UltraControlBase)this.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(this.lblHeader, "lblHeader");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Red;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblHeader).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.lblHeader).Name = "lblHeader";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblHeader, resources.GetString("lblHeader.ToolTip"));
		this.toolTip2.SetToolTip((System.Windows.Forms.Control)(object)this.lblHeader, resources.GetString("lblHeader.ToolTip1"));
		((UltraControlBase)this.lblHeader).UseAppStyling = false;
		this.toolTip2.AutoPopDelay = 5000;
		this.toolTip2.BackColor = System.Drawing.Color.Transparent;
		this.toolTip2.InitialDelay = 200;
		this.toolTip2.IsBalloon = true;
		this.toolTip2.ReshowDelay = 10;
		this.toolTip2.ShowAlways = true;
		this.toolTip2.ToolTipTitle = "Occupied";
		this.timer1.Interval = 3000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this, "$this");
		this.AllowDrop = true;
		base.Controls.Add(this.panel1);
		base.Name = "frmRoomManagement";
		this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
		this.toolTip2.SetToolTip(this, resources.GetString("$this.ToolTip1"));
		base.Resize += new System.EventHandler(frmRoomDesign_Resize);
		base.Controls.SetChildIndex(this.panel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
	}
}
