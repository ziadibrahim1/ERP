using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmCheckList : frmBase
{
	private DataTable dtDetails = new DataTable();

	private string ColID;

	private string ColName;

	private string DisplayName;

	public string[] arr;

	public DataRow[] drSelectedRows;

	public bool selectedChoise;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnSave;

	public UltraButton btnCancel;

	private UltraCheckEditor chkSelect;

	public frmCheckList()
	{
		InitializeComponent();
	}

	public frmCheckList(DataTable DT, string COLID, string COLNAME, string DISPLAYNAME, bool _selectedChoise)
		: this()
	{
		dtDetails = DT.Copy();
		ColID = COLID;
		ColName = COLNAME;
		DisplayName = DISPLAYNAME;
		selectedChoise = _selectedChoise;
		arr = new string[dtDetails.Rows.Count];
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtDetails.Columns.Add(new DataColumn("Select", typeof(bool)));
		foreach (DataRow row in dtDetails.Rows)
		{
			row["Select"] = selectedChoise;
		}
		((UltraToggleEditorBase)chkSelect).Checked = selectedChoise;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColName].Width = (int)((double)((Control)(object)ULGData).Width * 0.82) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColName].Header).Caption = DisplayName;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColName].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Header).Caption = (GlobalVariables.IsArabic ? "أختر" : "Select");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].DefaultCellValue = false;
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void chkSelect_CheckedChanged(object sender, EventArgs e)
	{
		bool flag = ((UltraToggleEditorBase)chkSelect).Checked;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Select"].Value = flag;
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		((UltraGridBase)ULGData).UpdateData();
		drSelectedRows = ((DataTable)((UltraGridBase)ULGData).DataSource).Select("Select = 1");
		if (drSelectedRows.Length == 0)
		{
			GlobalVariables.InformationMB.Show((GlobalVariables.IsArabic ? " برجاء إختيار " : "Please Select ") + DisplayName);
		}
		else
		{
			Close();
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == ColName)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
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
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmCheckList));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnSave = new UltraButton();
		this.btnCancel = new UltraButton();
		this.chkSelect = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSelect).BeginInit();
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
		resources.ApplyResources(this.chkSelect, "chkSelect");
		((System.Windows.Forms.Control)(object)this.chkSelect).Name = "chkSelect";
		((UltraToggleEditorBase)this.chkSelect).CheckedChanged += new System.EventHandler(chkSelect_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSelect);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmCheckList";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSelect, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSelect).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
