using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmColorSizeSelection : frmBase
{
	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtColorsSizeCross;

	public DataTable dtColorSizeCrossStructure;

	public string ItemID;

	private DataView dvDistinctColors;

	private DataView dvDistinctSizes;

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool ViewPrice = false;

	public decimal Price = default(decimal);

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	private UltraComboEditor cboItemName;

	private UltraLabel lblItemName;

	public UltraLabel lblCyl;

	public UltraLabel lblSph;

	public UltraComboEditor cboCyl;

	public UltraComboEditor cboSph;

	public UltraButton btnClose;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtUnitPrice;

	private UltraLabel lblUnitPrice;

	public frmColorSizeSelection()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmColorSizeSelection(bool _ViewPrice)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		ViewPrice = _ViewPrice;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (cboItemName.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الصنف", "Please Select Item");
			return;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال تفاصيل", "Please insert Details");
			return;
		}
		dtColorSizeCrossStructure.AcceptChanges();
		ItemID = ((TextEditorControlBase)cboItemName).Value.ToString();
		Price = decimal.Parse((((Control)(object)txtUnitPrice).Text == "" || ((Control)(object)txtUnitPrice).Text == ".") ? "0" : ((Control)(object)txtUnitPrice).Text);
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtItems = Items.FillComboWithItemType("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = "ItemTypeID =2";
		dataView.ToTable();
		GlobalFunctions.FillCombo(cboItemName, dataView.ToTable(), "ItemID", "Name");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlColors.ValueListItems.Clear();
		for (int i = 0; i < dtColors.Rows.Count; i++)
		{
			vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
		}
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSizes.ValueListItems.Clear();
		for (int j = 0; j < dtSizes.Rows.Count; j++)
		{
			vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
		}
		((Control)(object)lblSph).Text = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		cboSph.Items.Add((object)"-", "-");
		cboSph.Items.Add((object)"+", "+");
		((Control)(object)lblCyl).Text = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		cboCyl.Items.Add((object)"-", "-");
		cboCyl.Items.Add((object)"+", "+");
		UltraTextEditor obj = txtUnitPrice;
		bool visible = (((Control)(object)lblUnitPrice).Visible = ViewPrice);
		((Control)(object)obj).Visible = visible;
	}

	private void cboItemName_ValueChanged(object sender, EventArgs e)
	{
		GenerateCross();
	}

	private void cboSph_ValueChanged(object sender, EventArgs e)
	{
		GenerateCross();
	}

	private void cboCyl_ValueChanged(object sender, EventArgs e)
	{
		GenerateCross();
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "2", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboItemName).Value = num;
		}
	}

	private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	public void InitGrid()
	{
		((UltraControlBase)ULGData).UseAppStyling = false;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.ActiveCellRowSelectorAppearance.BackColor = Color.Red;
		((UltraGridBase)ULGData).DisplayLayout.Override.ActiveCellColumnHeaderAppearance.ForeColor = Color.Red;
		((UltraGridBase)ULGData).DisplayLayout.Override.ActiveRowAppearance.BackColor = Color.LightSkyBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SPH/CYL"].Header).Caption = "SPH / CYL";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SPH/CYL"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SPH/CYL"].ValueList = (IValueList)(object)vlColors;
		for (int i = 1; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			string caption = dtSizes.Select(" ItemSizeID= " + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key)[0]["ItemSizeName"].ToString();
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Header).Caption = caption;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Hidden = false;
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SPH/CYL")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].ResetCellAppearance();
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ULGData.ActiveCell.Column.Index].CellAppearance.BackColor = Color.LightSkyBlue;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (dtColorSizeCrossStructure != null)
		{
			dtColorSizeCrossStructure.Rows.Clear();
			dtColorSizeCrossStructure.AcceptChanges();
		}
		Close();
	}

	public void GenerateCross()
	{
		if (cboItemName.SelectedIndex != -1 && cboSph.SelectedIndex != -1 && cboCyl.SelectedIndex != -1)
		{
			dtColorsSizeCross = Items.ColorsSizeCross(((TextEditorControlBase)cboSph).Value.ToString(), ((TextEditorControlBase)cboCyl).Value.ToString(), ((TextEditorControlBase)cboItemName).Value.ToString(), IsFromServer: false);
			dvDistinctColors = new DataView(dtColorsSizeCross);
			dvDistinctSizes = new DataView(dtColorsSizeCross);
			DataTable dataTable = dvDistinctColors.ToTable(true, "ColorID");
			DataTable dataTable2 = dvDistinctSizes.ToTable(true, "ItemSizeID");
			dtColorSizeCrossStructure = new DataTable();
			dtColorSizeCrossStructure.Columns.Add("SPH/CYL", typeof(decimal));
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				dtColorSizeCrossStructure.Columns.Add(dataTable2.Rows[i]["ItemSizeID"].ToString(), typeof(decimal));
			}
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				dtColorSizeCrossStructure.Rows.Add(dataTable.Rows[j]["ColorID"].ToString());
			}
			((UltraGridBase)ULGData).DataSource = dtColorSizeCrossStructure;
			InitGrid();
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
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmColorSizeSelection));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.cboItemName = new UltraComboEditor();
		this.lblItemName = new UltraLabel();
		this.lblCyl = new UltraLabel();
		this.lblSph = new UltraLabel();
		this.cboCyl = new UltraComboEditor();
		this.cboSph = new UltraComboEditor();
		this.btnClose = new UltraButton();
		this.btnItemsSearch = new UltraButton();
		this.txtUnitPrice = new UltraTextEditor();
		this.lblUnitPrice = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCyl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSph).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitPrice).BeginInit();
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
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnSave).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.cboItemName, "cboItemName");
		this.cboItemName.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboItemName).Name = "cboItemName";
		((TextEditorControlBase)this.cboItemName).ValueChanged += new System.EventHandler(cboItemName_ValueChanged);
		resources.ApplyResources(this.lblItemName, "lblItemName");
		this.lblItemName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemName).Name = "lblItemName";
		((ControlBase)this.lblItemName).WrapText = false;
		resources.ApplyResources(this.lblCyl, "lblCyl");
		this.lblCyl.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCyl).Name = "lblCyl";
		((ControlBase)this.lblCyl).WrapText = false;
		resources.ApplyResources(this.lblSph, "lblSph");
		this.lblSph.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSph).Name = "lblSph";
		((ControlBase)this.lblSph).WrapText = false;
		resources.ApplyResources(this.cboCyl, "cboCyl");
		this.cboCyl.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCyl).Name = "cboCyl";
		((TextEditorControlBase)this.cboCyl).Nullable = false;
		((TextEditorControlBase)this.cboCyl).ValueChanged += new System.EventHandler(cboCyl_ValueChanged);
		resources.ApplyResources(this.cboSph, "cboSph");
		this.cboSph.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSph).Name = "cboSph";
		((TextEditorControlBase)this.cboSph).Nullable = false;
		((TextEditorControlBase)this.cboSph).ValueChanged += new System.EventHandler(cboSph_ValueChanged);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtUnitPrice, "txtUnitPrice");
		((System.Windows.Forms.Control)(object)this.txtUnitPrice).Name = "txtUnitPrice";
		((System.Windows.Forms.Control)(object)this.txtUnitPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBarCode_KeyPress);
		this.lblUnitPrice.AutoEllipsis = false;
		resources.ApplyResources(this.lblUnitPrice, "lblUnitPrice");
		((System.Windows.Forms.Control)(object)this.lblUnitPrice).Name = "lblUnitPrice";
		((ControlBase)this.lblUnitPrice).WrapText = false;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSph);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCyl);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSph);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItemName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmColorSizeSelection";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItemName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCyl, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSph, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCyl, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnitPrice, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItemName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCyl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSph).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitPrice).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
