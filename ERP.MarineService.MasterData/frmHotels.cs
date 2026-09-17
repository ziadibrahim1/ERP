using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Documents.Excel;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.MasterData;

public class frmHotels : frmGrid
{
	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtCode;

	private UltraLabel lblCode;

	private UltraLabel lblFax;

	private UltraTextEditor txtFax;

	private UltraLabel lblAddress;

	private UltraTextEditor txtAddress;

	private UltraButton ultraButton1;

	private UltraButton ultraButton2;

	public frmHotels()
	{
		InitializeComponent();
		TableName = "MS_Hotels";
		IDCol = "HotelID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtCode).Text = (Adding ? Hotels.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtFax).Clear();
	}

	public override void FillData()
	{
		dataTable = Hotels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود " : "code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelFax"].Header).Caption = (GlobalVariables.IsArabic ? "الفاكس" : "Fax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelFax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelFax"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelAddress"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelAddress"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HotelAddress"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["HotelCode"].Value.ToString();
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["HotelNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["HotelNameEn"].Value.ToString();
		((Control)(object)txtFax).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["HotelFax"].Value.ToString();
		((Control)(object)txtAddress).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["HotelAddress"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم  بالعربية", "Please Enter Arabic Name");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Hotels.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (((Control)(object)txtFax).Text == "") ? "Null" : ((Control)(object)txtFax).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Hotels.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["HotelID"].Value.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (((Control)(object)txtFax).Text == "") ? "Null" : ((Control)(object)txtFax).Text, (((Control)(object)txtAddress).Text == "") ? "Null" : ((Control)(object)txtAddress).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Hotels.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["HotelID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void ultraButton1_Click(object sender, EventArgs e)
	{
	}

	private void ultraButton2_Click(object sender, EventArgs e)
	{
		int val = int.MaxValue;
		int num = int.MinValue;
		int num2 = int.MaxValue;
		int num3 = int.MinValue;
		Workbook workbook = Workbook.Load("E:\\Hotels.xlsx");
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
		DataTable dataTable = new DataTable();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			dictionary.Add(((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Header).Caption, ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key);
		}
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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmHotels));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.lblFax = new UltraLabel();
		this.txtFax = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.ultraButton1 = new UltraButton();
		this.ultraButton2 = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
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
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		this.lblArabicName.AutoEllipsis = false;
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		this.lblEnglishName.AutoEllipsis = false;
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		this.lblCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((ControlBase)this.lblCode).WrapText = false;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.lblFax).Appearance = (AppearanceBase)(object)val10;
		this.lblFax.AutoEllipsis = false;
		resources.ApplyResources(this.lblFax, "lblFax");
		((System.Windows.Forms.Control)(object)this.lblFax).Name = "lblFax";
		((ControlBase)this.lblFax).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val11;
		this.lblAddress.AutoEllipsis = false;
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.ultraButton1, "ultraButton1");
		((System.Windows.Forms.Control)(object)this.ultraButton1).Name = "ultraButton1";
		((System.Windows.Forms.Control)(object)this.ultraButton1).Click += new System.EventHandler(ultraButton1_Click);
		resources.ApplyResources(this.ultraButton2, "ultraButton2");
		((System.Windows.Forms.Control)(object)this.ultraButton2).Name = "ultraButton2";
		((System.Windows.Forms.Control)(object)this.ultraButton2).Click += new System.EventHandler(ultraButton2_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmHotels";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraButton1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraButton2, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
