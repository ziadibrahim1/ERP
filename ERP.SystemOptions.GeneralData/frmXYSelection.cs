using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmXYSelection : frmBase
{
	private DataTable dtItems;

	private DataTable dtXYCross;

	private DataTable dtXYCrossGrid;

	public DataTable dtXYItems;

	private DataView dvDistinctX;

	private DataView dvDistinctY;

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

	private UltraTextEditor txtUnitPrice;

	private UltraLabel lblUnitPrice;

	public frmXYSelection()
	{
		InitializeComponent();
	}

	public frmXYSelection(bool _ViewPrice)
	{
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
		dtXYCrossGrid.AcceptChanges();
		dtXYItems = new DataTable();
		dtXYItems.Columns.Add("ItemID", typeof(int));
		dtXYItems.Columns.Add("Qty", typeof(decimal));
		for (int i = 0; i < dtXYCrossGrid.Rows.Count; i++)
		{
			for (int j = 1; j < dtXYCrossGrid.Columns.Count; j++)
			{
				if (dtXYCrossGrid.Rows[i][j] != DBNull.Value && dtXYCrossGrid.Rows[i][j].ToString() != "" && decimal.Parse(dtXYCrossGrid.Rows[i][j].ToString()) >= 0m && dtXYCross.Select(" X= " + dtXYCrossGrid.Rows[i]["SPH/CYL"].ToString() + " And Y= " + dtXYCrossGrid.Columns[j].ToString()).Length != 0)
				{
					dtXYItems.Rows.Add(int.Parse(dtXYCross.Select(" X= " + dtXYCrossGrid.Rows[i]["SPH/CYL"].ToString() + " And Y= " + dtXYCrossGrid.Columns[j].ToString())[0]["ItemID"].ToString()), decimal.Parse(dtXYCrossGrid.Rows[i][j].ToString()));
				}
			}
		}
		Price = decimal.Parse((((Control)(object)txtUnitPrice).Text == "" || ((Control)(object)txtUnitPrice).Text == ".") ? "0" : ((Control)(object)txtUnitPrice).Text);
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtItems = Items.FillComboDirectParents(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItemName, dtItems, "ParentID", "ParentName");
		cboSph.Items.Add((object)"-", "-");
		cboSph.Items.Add((object)"+", "+");
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

	private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
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
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation)2147483646;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectorHeaderStyle = (RowSelectorHeaderStyle)3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Override.SelectTypeCell = (SelectType)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SPH/CYL"].Header).Caption = "SPH / CYL";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SPH/CYL"].Hidden = false;
		for (int i = 1; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Hidden = false;
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].ResetCellAppearance();
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ULGData.ActiveCell.Column.Index].CellAppearance.BackColor = Color.LightSkyBlue;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (dtXYCrossGrid != null)
		{
			dtXYCrossGrid.Rows.Clear();
			dtXYCrossGrid.AcceptChanges();
		}
		Close();
	}

	public void GenerateCross()
	{
		if (cboItemName.SelectedIndex != -1 && cboSph.SelectedIndex != -1 && cboCyl.SelectedIndex != -1)
		{
			dtXYCross = Items.XYCross(((TextEditorControlBase)cboItemName).Value.ToString(), ((TextEditorControlBase)cboSph).Value.ToString(), ((TextEditorControlBase)cboCyl).Value.ToString(), IsFromServer: false);
			dvDistinctX = new DataView(dtXYCross);
			dvDistinctX.Sort = " ABSX ASC ";
			dvDistinctY = new DataView(dtXYCross);
			dvDistinctY.Sort = " ABSY ASC ";
			DataTable dataTable = dvDistinctX.ToTable(true, "X");
			DataTable dataTable2 = dvDistinctY.ToTable(true, "Y");
			dtXYCrossGrid = new DataTable();
			dtXYCrossGrid.Columns.Add("SPH/CYL", typeof(decimal));
			dtXYCrossGrid.Columns["SPH/CYL"].ReadOnly = true;
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				dtXYCrossGrid.Columns.Add(dataTable2.Rows[i]["Y"].ToString(), typeof(decimal));
			}
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				dtXYCrossGrid.Rows.Add(dataTable.Rows[j]["X"].ToString());
			}
			((UltraGridBase)ULGData).DataSource = dtXYCrossGrid;
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
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmXYSelection));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
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
		resources.ApplyResources(this.txtUnitPrice, "txtUnitPrice");
		((System.Windows.Forms.Control)(object)this.txtUnitPrice).Name = "txtUnitPrice";
		((System.Windows.Forms.Control)(object)this.txtUnitPrice).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtUnitPrice_KeyPress);
		this.lblUnitPrice.AutoEllipsis = false;
		resources.ApplyResources(this.lblUnitPrice, "lblUnitPrice");
		((System.Windows.Forms.Control)(object)this.lblUnitPrice).Name = "lblUnitPrice";
		((ControlBase)this.lblUnitPrice).WrapText = false;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnitPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitPrice);
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
		base.Name = "frmXYSelection";
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
