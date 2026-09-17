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

public class frmChecksMerge : frmBase
{
	private int MasterCheckID;

	private int RoomID;

	public string MergedCheckIDs = ",";

	private string CheckLog = "";

	private DataTable dtTables = new DataTable();

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	protected internal CheckedListBox clbTables;

	public frmChecksMerge()
	{
		InitializeComponent();
	}

	public frmChecksMerge(int checkid, int roomID, string checklog)
		: this()
	{
		MasterCheckID = checkid;
		RoomID = roomID;
		CheckLog = checklog;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtTables = Checks.SelectTablesForMerge(MasterCheckID.ToString(), RoomID.ToString(), GlobalVariables.CurrentBranchID);
		Main.Fillclb(clbTables, dtTables, "TableID", "TableCode");
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < clbTables.Items.Count; i++)
		{
			if (clbTables.GetItemChecked(i))
			{
				MergedCheckIDs = MergedCheckIDs + dtTables.Rows[i]["CheckID"].ToString() + ",";
				CheckLog = CheckLog + " تم دمج الشيك مع الشيك رقم " + dtTables.Rows[i]["TableCode"].ToString() + " مستخدم  " + GlobalVariables.UserName + " بتاريخ " + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "//";
			}
		}
		if (MergedCheckIDs == ",")
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار رقم طاولة ليتم دمجها ", "Please Select Table no To Merge It");
		}
		else
		{
			Close();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksMerge));
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
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance12");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance12.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val2).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance14.FontData");
		resources.ApplyResources(val2, "appearance14");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val3).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance19.FontData");
		resources.ApplyResources(val3, "appearance19");
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
		base.Name = "frmChecksMerge";
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
