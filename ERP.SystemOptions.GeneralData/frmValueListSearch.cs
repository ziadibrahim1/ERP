using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmValueListSearch : frmBase
{
	public int ResultID = 0;

	private string HeaderName;

	private DataTable dtItems = new DataTable();

	public DataView dv;

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnSave;

	private UltraTextEditor txtItemName;

	public UltraButton btnKeyboard;

	protected internal UltraGrid ULGData;

	private UltraButton btnCancel;

	public frmValueListSearch()
	{
		InitializeComponent();
	}

	public frmValueListSearch(ValueList vlItems, string headerName)
		: this()
	{
		HeaderName = headerName;
		dtItems.Columns.Add("ID");
		dtItems.Columns.Add("Name");
		for (int i = 0; i < ((DisposableObjectCollectionBase)vlItems.ValueListItems).Count; i++)
		{
			dtItems.Rows.Add(vlItems.ValueListItems[i].DataValue, vlItems.ValueListItems[i].DisplayText);
		}
		dv = new DataView(dtItems);
	}

	private void frmValueListSearch_Load(object sender, EventArgs e)
	{
		((UltraGridBase)ULGData).DataSource = dv;
		InitGrid();
		((UltraGridBase)ULGData).UpdateData();
	}

	private void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Name"].Width = ((Control)(object)ULGData).Width - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Name"].Header).Caption = HeaderName;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Name"].Hidden = false;
	}

	private void txtItemName_ValueChanged(object sender, EventArgs e)
	{
		dv.RowFilter = "Name Like '%" + ((Control)(object)txtItemName).Text.Replace("*", "[*]").Trim() + "%'";
		dv.RowStateFilter = DataViewRowState.CurrentRows;
		((UltraGridBase)ULGData).DataSource = dv;
		InitGrid();
	}

	private void txtItemName_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			ULGData.PerformAction((UltraGridAction)18);
		}
		else if (e.KeyCode == Keys.Up)
		{
			ULGData.PerformAction((UltraGridAction)17);
		}
		e.Handled = true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			ResultID = Convert.ToInt32(((UltraGridBase)ULGData).ActiveRow.Cells["ID"].Value);
		}
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void ULGData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
	{
		btnSave_Click(null, null);
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
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmValueListSearch));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnSave = new UltraButton();
		this.txtItemName = new UltraTextEditor();
		this.btnKeyboard = new UltraButton();
		this.ULGData = new UltraGrid();
		this.btnCancel = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtItemName, "txtItemName");
		((System.Windows.Forms.Control)(object)this.txtItemName).Name = "txtItemName";
		((TextEditorControlBase)this.txtItemName).ValueChanged += new System.EventHandler(txtItemName_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtItemName).KeyDown += new System.Windows.Forms.KeyEventHandler(txtItemName_KeyDown);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance10.FontData");
		resources.ApplyResources(val3, "appearance10");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val4).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance14.FontData");
		resources.ApplyResources(val4, "appearance14");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance15.FontData");
		resources.ApplyResources(val5, "appearance15");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance16.FontData");
		resources.ApplyResources(val6, "appearance16");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance17.FontData");
		resources.ApplyResources(val7, "appearance17");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseFlatMode = (DefaultableBoolean)1;
		this.ULGData.DoubleClickRow += new DoubleClickRowEventHandler(ULGData_DoubleClickRow);
		((UltraButtonBase)this.btnCancel).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnCancel;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItemName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Name = "frmValueListSearch";
		base.Load += new System.EventHandler(frmValueListSearch_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItemName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
