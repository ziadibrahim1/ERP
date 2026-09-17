using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.POS.Transactions;

public class frmChecksTransfer : frmBase
{
	private int CheckID;

	private int RoomID;

	private DataTable dtTables = new DataTable();

	public string TablesNo = "";

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	protected internal CheckedListBox clbTables;

	public frmChecksTransfer()
	{
		InitializeComponent();
	}

	public frmChecksTransfer(int checkid, int roomID)
		: this()
	{
		CheckID = checkid;
		RoomID = roomID;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtTables = Tables.SelectByRoomID(RoomID.ToString(), "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		Main.Fillclb(clbTables, dtTables, "TableID", "TableCode");
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (clbTables.CheckedItems.Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار رقم طاولة لتتم عملية النقل ", "Please Select Table no To Transfer");
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ChecksTables.DeleteByCheckID(CheckID.ToString(), GlobalVariables.UserID);
			for (int i = 0; i < clbTables.Items.Count; i++)
			{
				if (clbTables.GetItemChecked(i))
				{
					ChecksTables.Insert_Update("-1", CheckID.ToString(), dtTables.Rows[i]["TableID"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					TablesNo += ((TablesNo == "") ? dtTables.Rows[i]["TableCode"].ToString() : ("," + dtTables.Rows[i]["TableCode"].ToString()));
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

	private void btnCancel_Click(object sender, EventArgs e)
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
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksTransfer));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.clbTables = new System.Windows.Forms.CheckedListBox();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance12");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance12.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val2).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val2, "appearance14");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance14.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val3).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val3, "appearance19");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance19.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.clbTables, "clbTables");
		this.clbTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbTables.CheckOnClick = true;
		this.clbTables.Name = "clbTables";
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.clbTables);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmChecksTransfer";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex(this.clbTables, 0);
		base.ResumeLayout(false);
	}
}
