using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer.POS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmChecksFrontScreen : frmBase
{
	private DataTable dtItems;

	private DataTable dtCheckDetails;

	private DataTable dtPOSDefaultData;

	private ValueList vlItems = new ValueList();

	private FolderBrowserDialog fbd = new FolderBrowserDialog();

	private string[] images;

	private int counter = 0;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraLabel lblTitle;

	private UltraLabel lblNetPrice;

	private UltraTextEditor txtNetprice;

	public UltraButton btnClose;

	private UltraPictureBox PicPhoto;

	private Timer timer1;

	public frmChecksFrontScreen()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		try
		{
			dtPOSDefaultData = Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
			if (dtPOSDefaultData != null && dtPOSDefaultData.Rows.Count > 0 && dtPOSDefaultData.Rows[0]["SecondScreenAdsPath"] != DBNull.Value)
			{
				images = Directory.GetFiles(dtPOSDefaultData.Rows[0]["SecondScreenAdsPath"].ToString(), "*.*");
			}
		}
		catch
		{
		}
		timer1.Start();
	}

	public void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtCheckDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.7) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
	}

	public void RefreshCheckFrontScreen(DataTable dtdetails, DataTable Items, string NetPrice)
	{
		dtCheckDetails = dtdetails;
		dtItems = Items;
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["ItemName"].ToString());
		}
		base.TopMost = true;
		InitGrid();
		((Control)(object)txtNetprice).Text = NetPrice;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (images != null)
		{
			if (counter == images.Length - 1)
			{
				counter = 0;
			}
			try
			{
				PicPhoto.Image = Image.FromFile(images[counter]);
			}
			catch
			{
			}
			counter++;
		}
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksFrontScreen));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.ULGData = new UltraGrid();
		this.lblTitle = new UltraLabel();
		this.lblNetPrice = new UltraLabel();
		this.txtNetprice = new UltraTextEditor();
		this.btnClose = new UltraButton();
		this.PicPhoto = new UltraPictureBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblNetPrice, "lblNetPrice");
		this.lblNetPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNetPrice).Name = "lblNetPrice";
		resources.ApplyResources(this.txtNetprice, "txtNetprice");
		((System.Windows.Forms.Control)(object)this.txtNetprice).Name = "txtNetprice";
		((EditorButtonControlBase)this.txtNetprice).ReadOnly = true;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.PicPhoto, "PicPhoto");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val3, "appearance3");
		this.PicPhoto.Appearance = (AppearanceBase)(object)val3;
		this.PicPhoto.BorderShadowColor = System.Drawing.Color.Empty;
		this.PicPhoto.BorderStyle = (UIElementBorderStyle)2;
		this.PicPhoto.ImageTransparentColor = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.PicPhoto).Name = "PicPhoto";
		this.timer1.Interval = 5000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.PicPhoto);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetprice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmChecksFrontScreen";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetprice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.PicPhoto, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetprice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
