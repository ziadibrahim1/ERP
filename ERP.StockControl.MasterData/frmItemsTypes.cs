using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.EInvoices;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Microsoft.Office.Interop.Excel;

namespace ERP.StockControl.MasterData;

public class frmItemsTypes : frmGrid
{
	private DataTable dtEINVItemsGPC;

	private DataTable dtEinvSettings;

	private ValueList vlEINVItemsGPC = new ValueList();

	private bool UseElectronicInvoice = false;

	private bool SendingItemsByItemsTypes = false;

	private IContainer components = null;

	private UltraTextEditor txtItemArabicName;

	private UltraLabel lblItemArabicName;

	private UltraTextEditor txtItemEnglishName;

	private UltraLabel lblItemEnglishName;

	private UltraComboEditor cboEINVItemGPC;

	private UltraLabel lblEINVItemGPC;

	private UltraButton btnEINVExportTypes;

	public frmItemsTypes()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SC_ItemsTypes";
		IDCol = "ItemTypeID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseElectronicInvoice = GlobalFunctions.GetOption("UsingElectronicInvoice");
		if (UseElectronicInvoice)
		{
			dtEinvSettings = Settings.SelectByBranchID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dtEinvSettings.Rows.Count > 0)
			{
				SendingItemsByItemsTypes = bool.Parse(dtEinvSettings.Rows[0]["SendingItemsByItemsTypes"].ToString());
			}
			dtEINVItemsGPC = ItemsGPC.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboEINVItemGPC, dtEINVItemsGPC, "EINVItemGPCID", "EINVItemGPCName");
			vlEINVItemsGPC.ValueListItems.Clear();
			for (int i = 0; i < dtEINVItemsGPC.Rows.Count; i++)
			{
				vlEINVItemsGPC.ValueListItems.Add((object)dtEINVItemsGPC.Rows[i]["EINVItemGPCID"].ToString(), dtEINVItemsGPC.Rows[i]["EINVItemGPCName"].ToString());
			}
		}
		((Control)(object)btnEINVExportTypes).Visible = UseElectronicInvoice && SendingItemsByItemsTypes;
		((Control)(object)cboEINVItemGPC).Visible = UseElectronicInvoice && SendingItemsByItemsTypes;
		((Control)(object)lblEINVItemGPC).Visible = UseElectronicInvoice && SendingItemsByItemsTypes;
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtItemArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtItemEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboEINVItemGPC).ReadOnly = NavMode;
		((TextEditorControlBase)txtItemArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtItemArabicName).Clear();
		((TextEditorControlBase)txtItemEnglishName).Clear();
		cboEINVItemGPC.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = ItemsTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeNameAr"].Width = (int)((UseElectronicInvoice && SendingItemsByItemsTypes) ? ((double)((Control)(object)ULGData).Width * 0.4) : ((double)((Control)(object)ULGData).Width * 0.5)) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemTypeNameEn"].Width = (int)((UseElectronicInvoice && SendingItemsByItemsTypes) ? ((double)((Control)(object)ULGData).Width * 0.4) : ((double)((Control)(object)ULGData).Width * 0.5));
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVItemGPCID"].Header).Caption = (GlobalVariables.IsArabic ? "GPC" : "GPC");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVItemGPCID"].Hidden = ((!UseElectronicInvoice || !SendingItemsByItemsTypes) ? true : false);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVItemGPCID"].ValueList = (IValueList)(object)vlEINVItemsGPC;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVItemGPCID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtItemArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemTypeNameAr"].Value.ToString();
		((Control)(object)txtItemEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemTypeNameEn"].Value.ToString();
		((TextEditorControlBase)cboEINVItemGPC).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EINVItemGPCID"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtItemArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الإسم بالعربية", "Please Enter  Arabic Name");
			((TextEditorControlBase)txtItemArabicName).Focus();
			return false;
		}
		if (UseElectronicInvoice && SendingItemsByItemsTypes && cboEINVItemGPC.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار ال GPC", "Please select GPC");
			((TextEditorControlBase)cboEINVItemGPC).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		ItemsTypes.Insert_Update("-1", ((Control)(object)txtItemArabicName).Text, (((Control)(object)txtItemEnglishName).Text == "") ? "Null" : ((Control)(object)txtItemEnglishName).Text, (cboEINVItemGPC.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVItemGPC).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		ItemsTypes.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["ItemTypeID"].Value.ToString(), ((Control)(object)txtItemArabicName).Text, (((Control)(object)txtItemEnglishName).Text == "") ? "Null" : ((Control)(object)txtItemEnglishName).Text, (cboEINVItemGPC.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboEINVItemGPC).Value.ToString(), bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		ItemsTypes.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["ItemTypeID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void btnEINVExportTypes_Click(object sender, EventArgs e)
	{
		try
		{
			string text = "";
			DataTable dataTable = Main.ExecuteQuery_DataTable(" EINV_ItemsExportTOPortal " + (SendingItemsByItemsTypes ? "1" : "0"));
			if (dataTable == null || dataTable.Columns.Count == 0)
			{
				throw new Exception("ExportToExcel: Null or empty input table!\n");
			}
			Microsoft.Office.Interop.Excel.Application application = (Microsoft.Office.Interop.Excel.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("00024500-0000-0000-C000-000000000046")));
			application.Workbooks.Add(Type.Missing);
			_Worksheet worksheet = (dynamic)application.ActiveSheet;
			worksheet.Name = "DataEntry";
			for (int i = 0; i < dataTable.Columns.Count; i++)
			{
				worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
			}
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				for (int k = 0; k < dataTable.Columns.Count; k++)
				{
					worksheet.Cells[j + 2, k + 1] = dataTable.Rows[j][k];
				}
			}
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				text = folderBrowserDialog.SelectedPath;
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					worksheet.SaveAs(text + "\\NewCodeBulkTemplate.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
					application.Quit();
					MessageBox.Show("Excel file saved!");
					return;
				}
				catch (Exception ex)
				{
					throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n" + ex.Message);
				}
			}
			application.Visible = true;
		}
		catch (Exception ex2)
		{
			throw new Exception("ExportToExcel: \n" + ex2.Message);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmItemsTypes));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtItemArabicName = new UltraTextEditor();
		this.lblItemArabicName = new UltraLabel();
		this.txtItemEnglishName = new UltraTextEditor();
		this.lblItemEnglishName = new UltraLabel();
		this.cboEINVItemGPC = new UltraComboEditor();
		this.lblEINVItemGPC = new UltraLabel();
		this.btnEINVExportTypes = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVItemGPC).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(this.txtItemArabicName, "txtItemArabicName");
		((System.Windows.Forms.Control)(object)this.txtItemArabicName).Name = "txtItemArabicName";
		this.lblItemArabicName.AutoEllipsis = false;
		resources.ApplyResources(this.lblItemArabicName, "lblItemArabicName");
		((System.Windows.Forms.Control)(object)this.lblItemArabicName).Name = "lblItemArabicName";
		((ControlBase)this.lblItemArabicName).WrapText = false;
		resources.ApplyResources(this.txtItemEnglishName, "txtItemEnglishName");
		((System.Windows.Forms.Control)(object)this.txtItemEnglishName).Name = "txtItemEnglishName";
		this.lblItemEnglishName.AutoEllipsis = false;
		resources.ApplyResources(this.lblItemEnglishName, "lblItemEnglishName");
		((System.Windows.Forms.Control)(object)this.lblItemEnglishName).Name = "lblItemEnglishName";
		((ControlBase)this.lblItemEnglishName).WrapText = false;
		this.cboEINVItemGPC.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboEINVItemGPC, "cboEINVItemGPC");
		((System.Windows.Forms.Control)(object)this.cboEINVItemGPC).Name = "cboEINVItemGPC";
		this.lblEINVItemGPC.AutoEllipsis = false;
		resources.ApplyResources(this.lblEINVItemGPC, "lblEINVItemGPC");
		((System.Windows.Forms.Control)(object)this.lblEINVItemGPC).Name = "lblEINVItemGPC";
		((ControlBase)this.lblEINVItemGPC).WrapText = false;
		resources.ApplyResources(this.btnEINVExportTypes, "btnEINVExportTypes");
		((System.Windows.Forms.Control)(object)this.btnEINVExportTypes).Name = "btnEINVExportTypes";
		((System.Windows.Forms.Control)(object)this.btnEINVExportTypes).Click += new System.EventHandler(btnEINVExportTypes_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEINVExportTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEINVItemGPC);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEINVItemGPC);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItemEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItemArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemArabicName);
		base.Name = "frmItemsTypes";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItemArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItemEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEINVItemGPC, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEINVItemGPC, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEINVExportTypes, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEINVItemGPC).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
