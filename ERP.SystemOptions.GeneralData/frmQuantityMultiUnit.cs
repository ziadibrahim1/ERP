using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmQuantityMultiUnit : frmBase
{
	private int UnitTypeID;

	private int DefaultUnitID;

	private bool IsReadOnlyUnitColumn;

	private DataTable DtUnits = new DataTable();

	public int UnitID;

	public decimal Qty;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnSave;

	public UltraButton btnCancel;

	public frmQuantityMultiUnit()
	{
		InitializeComponent();
	}

	public frmQuantityMultiUnit(int defaultunitid, int unittypeid, bool isreadonlyunitcolumn)
		: this()
	{
		UnitTypeID = unittypeid;
		DefaultUnitID = defaultunitid;
		IsReadOnlyUnitColumn = isreadonlyunitcolumn;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		DtUnits = Main.ExecuteQuery_DataTable(" SC_QuantityMultiUnit_Select " + DefaultUnitID + "," + UnitTypeID + "," + (GlobalVariables.IsArabic ? "1" : "0"));
		DtUnits.Columns["DefaultForUnit"].ReadOnly = IsReadOnlyUnitColumn;
		((UltraGridBase)ULGData).DataSource = DtUnits;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultForUnit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.58);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultForUnit"].Header).Caption = (GlobalVariables.IsArabic ? "المرجع" : "Default");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DefaultForUnit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		ULGData.ActiveCell.SelectAll();
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitName")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
	}

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "DefaultForUnit")
		{
			((UltraGridBase)ULGData).UpdateData();
			if (bool.Parse(e.Cell.Value.ToString()))
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					if (e.Cell.Row.Index != i)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["DefaultForUnit"].Value = 0;
					}
				}
			}
			((UltraGridBase)ULGData).UpdateData();
		}
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DefaultForUnit"].Value.ToString()))
			{
				num++;
			}
		}
		if (num == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المرجع" : "Please Select Default");
			return;
		}
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) > 0m)
			{
				num2 += decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["RatioToDefault"].Value.ToString());
			}
		}
		num3 = num2 / decimal.Parse(((DataTable)((UltraGridBase)ULGData).DataSource).Select(" DefaultForUnit=1 ")[0]["RatioToDefault"].ToString());
		UnitID = int.Parse(((DataTable)((UltraGridBase)ULGData).DataSource).Select(" DefaultForUnit=1 ")[0]["UnitID"].ToString());
		Qty = num3;
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
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmQuantityMultiUnit));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnSave = new UltraButton();
		this.btnCancel = new UltraButton();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
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
		this.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance19.FontData");
		resources.ApplyResources(val, "appearance19");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val2).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance14.FontData");
		resources.ApplyResources(val2, "appearance14");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmQuantityMultiUnit";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
	}
}
