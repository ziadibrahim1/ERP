using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Production;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Approved;

public class frmProductionItemsBalance : frmBase
{
	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private ValueList vlItems = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private DataTable dtDetails = new DataTable();

	private string productionID;

	private string StoreID;

	private string productionApproveDate;

	public int ColorID = 0;

	public int ItemSizeID = 0;

	public int BatchID = 0;

	public decimal Avg = default(decimal);

	public decimal Qty = default(decimal);

	private bool UsingColors;

	private bool UsingSizes = false;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public frmProductionItemsBalance()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmProductionItemsBalance(string _ProductionID, string _ProductionApproveDate)
		: this()
	{
		productionID = _ProductionID;
		productionApproveDate = _ProductionApproveDate;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtDetails = Productions.RequiredMaterialsBalance(productionID, productionApproveDate, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "باركود الصنف" : "Item BarCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.11);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RequiredQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RequiredQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RequiredQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المطلوبة" : "Required Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCurrentQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCurrentQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCurrentQty"].Header).Caption = (GlobalVariables.IsArabic ? "رصيد الصنف" : "Item Balance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diff"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diff"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diff"].Header).Caption = (GlobalVariables.IsArabic ? "الفرق" : "Difference");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Approved.frmProductionItemsBalance));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
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
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
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
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance13");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance13.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmProductionItemsBalance";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
	}
}
