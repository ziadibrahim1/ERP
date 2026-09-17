using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmImageViewer : frmBase
{
	private DataTable dtStores;

	private DataTable dtDetails = new DataTable();

	private DataTable dtPrices = new DataTable();

	public bool Saved = false;

	private Image Picture;

	private string ItemName;

	private string ItemBalance = "";

	private string ItemID;

	public string StoreID;

	private string VoucherDate;

	public string ColorID;

	public string ItemSizeID;

	public string BatchID;

	public decimal Avg = default(decimal);

	public decimal Qty = default(decimal);

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool viewPrice = false;

	private IContainer components = null;

	public UltraButton btnClose;

	private UltraPictureBox PictureViewer;

	private UltraLabel lblName;

	private UltraComboEditor cboStore;

	private UltraLabel lblStore;

	public UltraGrid ULGData;

	public UltraGrid ULGPrices;

	public frmImageViewer()
	{
		InitializeComponent();
	}

	public frmImageViewer(Image picture, string name, string _ItemID, string _StoreID, string _BatchID, string _ColorID, string _ItemSizeID, string DATE)
		: this()
	{
		Picture = picture;
		ItemName = name;
		ItemID = _ItemID;
		StoreID = _StoreID;
		ColorID = _ColorID;
		ItemSizeID = _ItemSizeID;
		BatchID = _BatchID;
		VoucherDate = DATE;
	}

	public frmImageViewer(Image picture, string name, string _ItemID, string _StoreID, string _BatchID, string _ColorID, string _ItemSizeID, string DATE, bool ViewPriceType)
		: this()
	{
		viewPrice = ViewPriceType;
		Picture = picture;
		ItemName = name;
		ItemID = _ItemID;
		StoreID = _StoreID;
		ColorID = _ColorID;
		ItemSizeID = _ItemSizeID;
		BatchID = _BatchID;
		VoucherDate = DATE;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		PictureViewer.Image = Picture;
		((Control)(object)lblName).Text = ItemName;
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		dtStores = Stores.FillCombo("-1", "-1", "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		if (StoreID == "-1")
		{
			StoreID = dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString();
		}
		((TextEditorControlBase)cboStore).ValueChanged -= cboStore_ValueChanged;
		((TextEditorControlBase)cboStore).Value = StoreID;
		((TextEditorControlBase)cboStore).ValueChanged += cboStore_ValueChanged;
		dtDetails = Items.GetBalances("," + ItemID.ToString() + ",", (StoreID == "-1") ? StoreID : ("," + StoreID + ","), (BatchID == "-1") ? BatchID : ("," + BatchID + ","), (ColorID == "-1") ? ColorID : ("," + ColorID + ","), (ItemSizeID == "-1") ? ItemSizeID : ("," + ItemSizeID + ","), VoucherDate, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		if (viewPrice)
		{
			dtPrices = ItemsPrices.SelectByItemIDAndBranchID(ItemID.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraGridBase)ULGPrices).DataSource = dtPrices;
		}
		else
		{
			((Control)(object)ULGPrices).Visible = false;
		}
		InitGrid();
	}

	private void cboStore_ValueChanged(object sender, EventArgs e)
	{
		if (cboStore.SelectedIndex > -1)
		{
			dtDetails = Items.GetBalances("," + ItemID.ToString() + ",", "," + ((TextEditorControlBase)cboStore).Value.ToString() + ",", (BatchID == "-1") ? BatchID : ("," + BatchID + ","), (ColorID == "-1") ? ColorID : ("," + ColorID + ","), (ItemSizeID == "-1") ? ItemSizeID : ("," + ItemSizeID + ","), VoucherDate, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Hidden = !UsingBatchNoAndValidityPeriod;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.24);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.24);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.24);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "السريل" : "Batch No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		if (viewPrice)
		{
			GlobalFunctions.PrepareGrid(ULGPrices);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceName"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.75) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceName"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "PriceName");
			((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceName"].Hidden = false;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Saved = false;
		Close();
	}

	private void ULGData_DoubleClick(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			ColorID = ((((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString() : "-1");
			ItemSizeID = ((((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString() : "-1");
			BatchID = ((((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value != DBNull.Value) ? ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString() : "-1");
			StoreID = ((cboStore.SelectedIndex > -1) ? ((TextEditorControlBase)cboStore).Value.ToString() : "-1");
			Qty = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			Saved = true;
		}
		Close();
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
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
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmImageViewer));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.btnClose = new UltraButton();
		this.PictureViewer = new UltraPictureBox();
		this.lblName = new UltraLabel();
		this.cboStore = new UltraComboEditor();
		this.lblStore = new UltraLabel();
		this.ULGData = new UltraGrid();
		this.ULGPrices = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		this.PictureViewer.BorderShadowColor = System.Drawing.Color.Empty;
		this.PictureViewer.BorderStyle = (UIElementBorderStyle)2;
		resources.ApplyResources(this.PictureViewer, "PictureViewer");
		((System.Windows.Forms.Control)(object)this.PictureViewer).Name = "PictureViewer";
		resources.ApplyResources(this.lblName, "lblName");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblName).Appearance = (AppearanceBase)(object)val2;
		this.lblName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblName).Name = "lblName";
		((ControlBase)this.lblName).WrapText = false;
		((TextEditorControlBase)this.cboStore).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboStore, "cboStore");
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((TextEditorControlBase)this.cboStore).ValueChanged += new System.EventHandler(cboStore_ValueChanged);
		resources.ApplyResources(this.lblStore, "lblStore");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblStore).Appearance = (AppearanceBase)(object)val3;
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		((ControlBase)this.lblStore).WrapText = false;
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
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGData).DoubleClick += new System.EventHandler(ULGData_DoubleClick);
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		((UltraControlBase)this.ULGPrices).UseOsThemes = (DefaultableBoolean)2;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.PictureViewer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Name = "frmImageViewer";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.PictureViewer, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGPrices, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
