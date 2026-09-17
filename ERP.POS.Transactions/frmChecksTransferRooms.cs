using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.Transactions;

public class frmChecksTransferRooms : frmBase
{
	private DataTable dtTables = new DataTable();

	private DataTable dtRooms = new DataTable();

	private DataTable dtChecks = new DataTable();

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraButton btnSave;

	protected internal CheckedListBox clbTables;

	public UltraButton btnClose;

	private UltraLabel lblFromRoom;

	private UltraComboEditor cboFromRoom;

	private UltraLabel lblCheckNo;

	private UltraComboEditor cboCheckNo;

	private UltraLabel lblToRoom;

	private UltraComboEditor cboToRoom;

	public frmChecksTransferRooms()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtRooms = Rooms.SelectByType("1", "0", "0", "0", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFromRoom, dtRooms, "RoomID", GlobalVariables.IsArabic ? "RoomNameAr" : "RoomNameEn");
		GlobalFunctions.FillCombo(cboToRoom, dtRooms, "RoomID", GlobalVariables.IsArabic ? "RoomNameAr" : "RoomNameEn");
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (clbTables.CheckedItems.Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار رقم طاولة لتتم عملية النقل ", "Please Select Table no To Transfer");
			return;
		}
		if (cboFromRoom.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار من صالة ", "Please Select From Room");
			return;
		}
		if (cboToRoom.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الى صالة ", "Please Select To Room");
			return;
		}
		if (cboCheckNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار رقم الشيك ", "Please Select Check No");
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			Main.ExecuteNonQuery(" Update POS_Checks Set RoomID= " + ((TextEditorControlBase)cboToRoom).Value.ToString() + " Where CheckID= " + ((TextEditorControlBase)cboCheckNo).Value.ToString());
			ChecksTables.DeleteByCheckID(((TextEditorControlBase)cboCheckNo).Value.ToString(), GlobalVariables.UserID);
			for (int i = 0; i < clbTables.Items.Count; i++)
			{
				if (clbTables.GetItemChecked(i))
				{
					ChecksTables.Insert_Update("-1", ((TextEditorControlBase)cboCheckNo).Value.ToString(), dtTables.Rows[i]["TableID"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
			}
			Main.EndBulkTrans(FromServer: false);
			Close();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cboFromRoom_ValueChanged(object sender, EventArgs e)
	{
		if (cboFromRoom.SelectedIndex > -1)
		{
			dtChecks = Checks.SelectWithTableByRoomID(((TextEditorControlBase)cboFromRoom).Value.ToString());
			GlobalFunctions.FillCombo(cboCheckNo, dtChecks, "CheckID", "CheckNo");
		}
		else
		{
			dtChecks.Clear();
			GlobalFunctions.FillCombo(cboCheckNo, dtChecks, "CheckID", "CheckNo");
		}
	}

	private void cboToRoom_ValueChanged(object sender, EventArgs e)
	{
		if (cboToRoom.SelectedIndex > -1)
		{
			dtTables = Tables.SelectByRoomID(((TextEditorControlBase)cboToRoom).Value.ToString(), "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			Main.Fillclb(clbTables, dtTables, "TableID", "TableCode");
		}
		else
		{
			dtTables.Clear();
			Main.Fillclb(clbTables, dtTables, "TableID", "TableCode");
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksTransferRooms));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnSave = new UltraButton();
		this.clbTables = new System.Windows.Forms.CheckedListBox();
		this.btnClose = new UltraButton();
		this.lblFromRoom = new UltraLabel();
		this.cboFromRoom = new UltraComboEditor();
		this.lblCheckNo = new UltraLabel();
		this.cboCheckNo = new UltraComboEditor();
		this.lblToRoom = new UltraLabel();
		this.cboToRoom = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFromRoom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCheckNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboToRoom).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.clbTables, "clbTables");
		this.clbTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbTables.CheckOnClick = true;
		this.clbTables.Name = "clbTables";
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblFromRoom, "lblFromRoom");
		this.lblFromRoom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromRoom).Name = "lblFromRoom";
		((ControlBase)this.lblFromRoom).WrapText = false;
		resources.ApplyResources(this.cboFromRoom, "cboFromRoom");
		((TextEditorControlBase)this.cboFromRoom).AlwaysInEditMode = true;
		this.cboFromRoom.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboFromRoom).Name = "cboFromRoom";
		((TextEditorControlBase)this.cboFromRoom).ValueChanged += new System.EventHandler(cboFromRoom_ValueChanged);
		resources.ApplyResources(this.lblCheckNo, "lblCheckNo");
		this.lblCheckNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckNo).Name = "lblCheckNo";
		((ControlBase)this.lblCheckNo).WrapText = false;
		resources.ApplyResources(this.cboCheckNo, "cboCheckNo");
		((TextEditorControlBase)this.cboCheckNo).AlwaysInEditMode = true;
		this.cboCheckNo.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCheckNo).Name = "cboCheckNo";
		resources.ApplyResources(this.lblToRoom, "lblToRoom");
		this.lblToRoom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToRoom).Name = "lblToRoom";
		((ControlBase)this.lblToRoom).WrapText = false;
		resources.ApplyResources(this.cboToRoom, "cboToRoom");
		((TextEditorControlBase)this.cboToRoom).AlwaysInEditMode = true;
		this.cboToRoom.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboToRoom).Name = "cboToRoom";
		((TextEditorControlBase)this.cboToRoom).ValueChanged += new System.EventHandler(cboToRoom_ValueChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToRoom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboToRoom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromRoom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFromRoom);
		base.Controls.Add(this.clbTables);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmChecksTransferRooms";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex(this.clbTables, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFromRoom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromRoom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboToRoom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToRoom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFromRoom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCheckNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboToRoom).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
