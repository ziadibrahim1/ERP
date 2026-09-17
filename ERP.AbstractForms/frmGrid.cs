using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Documents.Excel;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;

namespace ERP.AbstractForms;

public class frmGrid : frmButtons
{
	public DataTable dataTable = new DataTable();

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraButton btnHeaderSearch;

	public frmGrid()
	{
		InitializeComponent();
		((Control)(object)ULGData).RightToLeft = RightToLeft.No;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnHeaderSearch).Enabled && ((Control)(object)btnHeaderSearch).Visible)
			{
				btnHeaderSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnHeaderSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = NavMode;
		((Control)(object)btnPriveous).Visible = NavMode;
		((Control)(object)ULGData).Enabled = NavMode;
	}

	public virtual void SelectFullRow(object sender, EventArgs e)
	{
	}

	public virtual void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
	}

	private void frmGrid_ResizeEnd(object sender, EventArgs e)
	{
	}

	private void frmGrid_Resize(object sender, EventArgs e)
	{
	}

	public virtual void ULGData_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((Control)(object)btnUpdate).Enabled)
		{
			btnUpdate_Click(null, null);
		}
	}

	private void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		((CancelEventArgs)(object)e).Cancel = true;
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraButtonBase)btnDelete).PerformClick();
		}
	}

	private void ULGData_KeyUp(object sender, KeyEventArgs e)
	{
		if (!Adding && !Updating && e.KeyCode == Keys.Delete && ((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraButtonBase)btnDelete).PerformClick();
		}
	}

	public override void btnUpdateClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			RowID = ((UltraGridBase)ULGData).ActiveRow.Cells[IDCol].Value.ToString();
			base.btnUpdateClick();
		}
	}

	public override void btnDeleteClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			RowID = ((UltraGridBase)ULGData).ActiveRow.Cells[IDCol].Value.ToString();
			base.btnDeleteClick();
		}
	}

	public override void btnOKClick()
	{
		int num = 0;
		if (Updating)
		{
			num = ((UltraGridBase)ULGData).ActiveRow.Index;
		}
		base.btnOKClick();
		if (Adding)
		{
			FillData();
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ULGData.AfterRowActivate -= ULGData_AfterRowActivate;
				((UltraGridBase)ULGData).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1].Activate();
				ULGData.AfterRowActivate += ULGData_AfterRowActivate;
			}
		}
		else
		{
			((UltraGridBase)ULGData).Rows[num].Activate();
		}
	}

	public override void btnCancelClick()
	{
		bool adding = Adding;
		base.btnCancelClick();
		if (adding && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			if (((UltraGridBase)ULGData).ActiveRow != null)
			{
				((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index].Activate();
			}
			else
			{
				((UltraGridBase)ULGData).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1].Activate();
			}
			AfterRowActivate();
		}
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		PriveousData();
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		NextData();
	}

	public virtual void PriveousData()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index]).Selected = false;
			((UltraGridBase)ULGData).Rows[(((UltraGridBase)ULGData).ActiveRow.Index != 0) ? (((UltraGridBase)ULGData).ActiveRow.Index - 1) : 0].Activate();
			((GridItemBase)((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index]).Selected = true;
		}
	}

	public virtual void NextData()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((GridItemBase)((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index]).Selected = false;
			((UltraGridBase)ULGData).Rows[(((UltraGridBase)ULGData).ActiveRow.Index == ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1) ? (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1) : (((UltraGridBase)ULGData).ActiveRow.Index + 1)].Activate();
			((GridItemBase)((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index]).Selected = true;
		}
	}

	private void ULGData_AfterRowActivate(object sender, EventArgs e)
	{
		if (!Adding)
		{
			AfterRowActivate();
		}
	}

	public virtual void AfterRowActivate()
	{
	}

	private void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		Search();
	}

	public virtual void Search()
	{
	}

	private void ULGData_BeforeEnterEditMode(object sender, CancelEventArgs e)
	{
		e.Cancel = true;
		((Control)(object)btnUpdate).Focus();
	}

	protected override void ExportGridData()
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "*.xls|*.xlsx";
		saveFileDialog.Title = "Save an Excel File";
		saveFileDialog.FileName = TableName.Substring(TableName.IndexOf("_") + 1);
		if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
		{
			UltraGridExcelExporter ultraGridExcelExporter = new UltraGridExcelExporter();
			((UltraControlBase)ULGData).UseAppStyling = false;
			Workbook workbook = ultraGridExcelExporter.Export(ULGData, saveFileDialog.FileName, WorkbookFormat.Excel2007);
			((UltraControlBase)ULGData).UseAppStyling = true;
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Export Completed Successfully.");
			Process.Start(saveFileDialog.FileName);
		}
	}

	public override void ImportGridData()
	{
		string empty = string.Empty;
		string empty2 = string.Empty;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "*.xls|*.xlsx";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		empty = openFileDialog.FileName;
		empty2 = Path.GetExtension(empty);
		int val = int.MaxValue;
		int num = int.MinValue;
		int num2 = int.MaxValue;
		int num3 = int.MinValue;
		Workbook workbook = Workbook.Load(empty);
		foreach (WorksheetRow item in (IEnumerable<WorksheetRow>)workbook.Worksheets[0].Rows)
		{
			foreach (WorksheetCell item2 in (IEnumerable<WorksheetCell>)item.Cells)
			{
				if (item2.Value != null)
				{
					val = Math.Min(val, item2.RowIndex);
					num = Math.Max(num, item2.RowIndex);
					num2 = Math.Min(num2, item2.ColumnIndex);
					num3 = Math.Max(num3, item2.ColumnIndex);
				}
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[IDCol].DefaultCellValue = -1;
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			dictionary.Add(((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Header).Caption, ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key);
		}
		try
		{
			for (int j = 1; j <= num; j++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				for (int k = num2; k <= num3; k++)
				{
					string key = workbook.Worksheets[0].Rows[0].Cells[k].Value.ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells[dictionary[key]].Value = workbook.Worksheets[0].Rows[j].Cells[k].Value;
				}
				((UltraGridBase)ULGData).UpdateData();
			}
		}
		catch (Exception)
		{
			throw;
		}
		finally
		{
			if (ValidateData())
			{
				AddData();
			}
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
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmGrid));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		this.ULGData = new UltraGrid();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.btnHeaderSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)this.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(SelectFullRow);
		this.ULGData.AfterRowActivate += new System.EventHandler(ULGData_AfterRowActivate);
		this.ULGData.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(ULGData_BeforeEnterEditMode);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyUp += new System.Windows.Forms.KeyEventHandler(ULGData_KeyUp);
		((System.Windows.Forms.Control)(object)this.ULGData).MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ULGData_MouseDoubleClick);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		((AppearanceBase)val13).Image = ERP.Properties.Resources.BarLeft;
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val13;
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		((AppearanceBase)val14).Image = ERP.Properties.Resources.BarRight;
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val14;
		resources.ApplyResources(this.btnNext, "btnNext");
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnHeaderSearch).Appearance = (AppearanceBase)(object)val15;
		resources.ApplyResources(this.btnHeaderSearch, "btnHeaderSearch");
		((System.Windows.Forms.Control)(object)this.btnHeaderSearch).Name = "btnHeaderSearch";
		((System.Windows.Forms.Control)(object)this.btnHeaderSearch).Click += new System.EventHandler(btnHeaderSearch_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnHeaderSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmGrid";
		base.Resize += new System.EventHandler(frmGrid_Resize);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
