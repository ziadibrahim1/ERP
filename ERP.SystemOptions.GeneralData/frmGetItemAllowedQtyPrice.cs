using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmGetItemAllowedQtyPrice : frmBase
{
	private DataTable dtColors;

	private DataTable dtSizes;

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private DataTable dtDetails = new DataTable();

	private string ItemID;

	private string ItemName;

	private string StoreID;

	private string VoucherDate;

	public int ColorID = 0;

	public int ItemSizeID = 0;

	public int BatchID = 0;

	public decimal Avg = default(decimal);

	public decimal Qty = default(decimal);

	public decimal Price = default(decimal);

	public decimal ProfitPercentage = default(decimal);

	private bool UsingColors;

	private bool UsingSizes = false;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	private UltraTextEditor txtLastPSValue;

	private UltraLabel lblExchangeRate;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtActualCostValue;

	private UltraTextEditor txtLastSalesValue;

	private UltraTextEditor txtLastPSProfitPerc;

	private UltraTextEditor txtActualCostProfitPerc;

	private UltraTextEditor txtLastSalesProfitPerc;

	private UltraTextEditor txtLastPSPrice;

	private UltraTextEditor txtActualCostPrice;

	private UltraTextEditor txtLastSalesPrice;

	private UltraLabel Val;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	public UltraButton btnLastPSPriceApply;

	public UltraButton btnActualCostApply;

	public UltraButton btnLastSalesPriceApply;

	private UltraLabel ultraLabel5;

	private UltraLabel lblItemName;

	public frmGetItemAllowedQtyPrice()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmGetItemAllowedQtyPrice(string ITEMID, string ITEMNAME, string STOREID, string DATE, decimal PROFITPERCENTAGE)
		: this()
	{
		ItemID = ITEMID;
		StoreID = STOREID;
		ItemName = ITEMNAME;
		VoucherDate = DATE;
		ProfitPercentage = PROFITPERCENTAGE;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		((Control)(object)lblTitle).Text = (GlobalVariables.IsArabic ? "إختيار صنف" : "Select Item");
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		((Control)(object)lblItemName).Text = ItemName;
		UltraTextEditor obj = txtLastPSProfitPerc;
		string text = (((Control)(object)txtActualCostProfitPerc).Text = ProfitPercentage.ToString());
		((Control)(object)obj).Text = text;
		DataTable dataTable = ItemsQuotations.SelectLastPrices(ItemID.ToString(), VoucherDate, IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			((Control)(object)txtLastPSValue).Text = dataTable.Rows[0]["LastPSPrice"].ToString();
			((Control)(object)txtActualCostValue).Text = dataTable.Rows[0]["ActualCost"].ToString();
			((Control)(object)txtLastSalesValue).Text = dataTable.Rows[0]["LastSalesPrice"].ToString();
			CalcPSPrice();
			CalcLastSalesPrice();
			CalcActualCostPrice();
		}
		dtDetails = Items.SelectBalancesWithAverage("," + ItemID.ToString() + ",", (StoreID == "-1") ? StoreID : ("," + StoreID + ","), VoucherDate, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Average"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "السريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Average"].Header).Caption = (GlobalVariables.IsArabic ? "المتوسط" : "Average");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Average"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
	}

	private void ULGData_DoubleClick(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			ColorID = ((((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value != DBNull.Value) ? int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value.ToString()) : 0);
			ItemSizeID = ((((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value != DBNull.Value) ? int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value.ToString()) : 0);
			BatchID = ((((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value != DBNull.Value) ? int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value.ToString()) : 0);
			Avg = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Average"].Value.ToString());
			Qty = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
		}
		Close();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Close();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CalcPSPrice()
	{
		((Control)(object)txtLastPSPrice).Text = ((decimal.Parse((((Control)(object)txtLastPSProfitPerc).Text == "" || ((Control)(object)txtLastPSProfitPerc).Text == ".") ? "0" : ((Control)(object)txtLastPSProfitPerc).Text) + 100m) / 100m * decimal.Parse(((Control)(object)txtLastPSValue).Text)).ToString();
	}

	private void CalcActualCostPrice()
	{
		((Control)(object)txtActualCostPrice).Text = ((decimal.Parse((((Control)(object)txtActualCostProfitPerc).Text == "" || ((Control)(object)txtActualCostProfitPerc).Text == ".") ? "0" : ((Control)(object)txtActualCostProfitPerc).Text) + 100m) / 100m * decimal.Parse(((Control)(object)txtActualCostValue).Text)).ToString();
	}

	private void CalcLastSalesPrice()
	{
		((Control)(object)txtLastSalesPrice).Text = ((decimal.Parse((((Control)(object)txtLastSalesProfitPerc).Text == "" || ((Control)(object)txtLastSalesProfitPerc).Text == ".") ? "0" : ((Control)(object)txtLastSalesProfitPerc).Text) + 100m) / 100m * decimal.Parse(((Control)(object)txtLastSalesValue).Text)).ToString();
	}

	private void txtLastPSProfitPerc_ValueChanged(object sender, EventArgs e)
	{
		CalcPSPrice();
	}

	private void txtActualCostProfitPerc_ValueChanged(object sender, EventArgs e)
	{
		CalcActualCostPrice();
	}

	private void txtLastSalesProfitPerc_ValueChanged(object sender, EventArgs e)
	{
		CalcLastSalesPrice();
	}

	private void btnLastPSPriceApply_Click(object sender, EventArgs e)
	{
		Price = decimal.Parse(((Control)(object)txtLastPSPrice).Text);
		Close();
	}

	private void btnActualCostApply_Click(object sender, EventArgs e)
	{
		Price = decimal.Parse(((Control)(object)txtActualCostPrice).Text);
		Close();
	}

	private void btnLastSalesPriceApply_Click(object sender, EventArgs e)
	{
		Price = decimal.Parse(((Control)(object)txtLastSalesPrice).Text);
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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmGetItemAllowedQtyPrice));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.txtLastPSValue = new UltraTextEditor();
		this.lblExchangeRate = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.txtActualCostValue = new UltraTextEditor();
		this.txtLastSalesValue = new UltraTextEditor();
		this.txtLastPSProfitPerc = new UltraTextEditor();
		this.txtActualCostProfitPerc = new UltraTextEditor();
		this.txtLastSalesProfitPerc = new UltraTextEditor();
		this.txtLastPSPrice = new UltraTextEditor();
		this.txtActualCostPrice = new UltraTextEditor();
		this.txtLastSalesPrice = new UltraTextEditor();
		this.Val = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.btnLastPSPriceApply = new UltraButton();
		this.btnActualCostApply = new UltraButton();
		this.btnLastSalesPriceApply = new UltraButton();
		this.ultraLabel5 = new UltraLabel();
		this.lblItemName = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastPSValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualCostValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastSalesValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastPSProfitPerc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualCostProfitPerc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastSalesProfitPerc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastPSPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualCostPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastSalesPrice).BeginInit();
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
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGData).DoubleClick += new System.EventHandler(ULGData_DoubleClick);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.txtLastPSValue, "txtLastPSValue");
		((System.Windows.Forms.Control)(object)this.txtLastPSValue).Name = "txtLastPSValue";
		((EditorButtonControlBase)this.txtLastPSValue).ReadOnly = true;
		this.lblExchangeRate.AutoEllipsis = false;
		resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
		((System.Windows.Forms.Control)(object)this.lblExchangeRate).Name = "lblExchangeRate";
		((ControlBase)this.lblExchangeRate).WrapText = false;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.ultraLabel2.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtActualCostValue, "txtActualCostValue");
		((System.Windows.Forms.Control)(object)this.txtActualCostValue).Name = "txtActualCostValue";
		((EditorButtonControlBase)this.txtActualCostValue).ReadOnly = true;
		resources.ApplyResources(this.txtLastSalesValue, "txtLastSalesValue");
		((System.Windows.Forms.Control)(object)this.txtLastSalesValue).Name = "txtLastSalesValue";
		((EditorButtonControlBase)this.txtLastSalesValue).ReadOnly = true;
		resources.ApplyResources(this.txtLastPSProfitPerc, "txtLastPSProfitPerc");
		((System.Windows.Forms.Control)(object)this.txtLastPSProfitPerc).Name = "txtLastPSProfitPerc";
		((TextEditorControlBase)this.txtLastPSProfitPerc).ValueChanged += new System.EventHandler(txtLastPSProfitPerc_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtLastPSProfitPerc).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtActualCostProfitPerc, "txtActualCostProfitPerc");
		((System.Windows.Forms.Control)(object)this.txtActualCostProfitPerc).Name = "txtActualCostProfitPerc";
		((TextEditorControlBase)this.txtActualCostProfitPerc).ValueChanged += new System.EventHandler(txtActualCostProfitPerc_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtActualCostProfitPerc).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtLastSalesProfitPerc, "txtLastSalesProfitPerc");
		((System.Windows.Forms.Control)(object)this.txtLastSalesProfitPerc).Name = "txtLastSalesProfitPerc";
		((TextEditorControlBase)this.txtLastSalesProfitPerc).ValueChanged += new System.EventHandler(txtLastSalesProfitPerc_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtLastSalesProfitPerc).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtLastPSPrice, "txtLastPSPrice");
		((System.Windows.Forms.Control)(object)this.txtLastPSPrice).Name = "txtLastPSPrice";
		((System.Windows.Forms.Control)(object)this.txtLastPSPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtActualCostPrice, "txtActualCostPrice");
		((System.Windows.Forms.Control)(object)this.txtActualCostPrice).Name = "txtActualCostPrice";
		((System.Windows.Forms.Control)(object)this.txtActualCostPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtLastSalesPrice, "txtLastSalesPrice");
		((System.Windows.Forms.Control)(object)this.txtLastSalesPrice).Name = "txtLastSalesPrice";
		((System.Windows.Forms.Control)(object)this.txtLastSalesPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		this.Val.AutoEllipsis = false;
		resources.ApplyResources(this.Val, "Val");
		((System.Windows.Forms.Control)(object)this.Val).Name = "Val";
		((ControlBase)this.Val).WrapText = false;
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		this.ultraLabel4.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.btnLastPSPriceApply, "btnLastPSPriceApply");
		((System.Windows.Forms.Control)(object)this.btnLastPSPriceApply).Name = "btnLastPSPriceApply";
		((System.Windows.Forms.Control)(object)this.btnLastPSPriceApply).Click += new System.EventHandler(btnLastPSPriceApply_Click);
		resources.ApplyResources(this.btnActualCostApply, "btnActualCostApply");
		((System.Windows.Forms.Control)(object)this.btnActualCostApply).Name = "btnActualCostApply";
		((System.Windows.Forms.Control)(object)this.btnActualCostApply).Click += new System.EventHandler(btnActualCostApply_Click);
		resources.ApplyResources(this.btnLastSalesPriceApply, "btnLastSalesPriceApply");
		((System.Windows.Forms.Control)(object)this.btnLastSalesPriceApply).Name = "btnLastSalesPriceApply";
		((System.Windows.Forms.Control)(object)this.btnLastSalesPriceApply).Click += new System.EventHandler(btnLastSalesPriceApply_Click);
		this.ultraLabel5.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.lblItemName, "lblItemName");
		((System.Windows.Forms.Control)(object)this.lblItemName).Name = "lblItemName";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLastSalesPriceApply);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnActualCostApply);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLastPSPriceApply);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.Val);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLastSalesPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLastSalesProfitPerc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLastSalesValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtActualCostPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtActualCostProfitPerc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtActualCostValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLastPSPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLastPSProfitPerc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLastPSValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExchangeRate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmGetItemAllowedQtyPrice";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExchangeRate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLastPSValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLastPSProfitPerc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLastPSPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtActualCostValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtActualCostProfitPerc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtActualCostPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLastSalesValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLastSalesProfitPerc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLastSalesPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.Val, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLastPSPriceApply, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnActualCostApply, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLastSalesPriceApply, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemName, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastPSValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualCostValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastSalesValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastPSProfitPerc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualCostProfitPerc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastSalesProfitPerc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastPSPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualCostPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLastSalesPrice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
