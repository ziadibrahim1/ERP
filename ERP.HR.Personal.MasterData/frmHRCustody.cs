using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.MasterData;

public class frmHRCustody : frmGrid
{
	private DataTable dtCustodyDetails;

	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	public UltraGrid ULGCustodyDetails;

	public frmHRCustody()
	{
		InitializeComponent();
		TableName = "HR_Custody";
		IDCol = "CustodyID";
	}

	public override void PrepareData()
	{
		dtCustodyDetails = CustodyDetails.SelectByCustodyID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridCustodyDetails();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		if (((UltraGridBase)ULGCustodyDetails).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustodyDetails).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGCustodyDetails).DataSource).Rows.Clear();
		}
	}

	public override void FillData()
	{
		dataTable = Custody.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyNameAr"].Header).Caption = (GlobalVariables.IsArabic ? " اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم  بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustodyNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
	}

	private void InitGridCustodyDetails()
	{
		((UltraGridBase)ULGCustodyDetails).DataSource = dtCustodyDetails;
		GlobalFunctions.PrepareGrid(ULGCustodyDetails);
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["CustodyDetailID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Description");
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGCustodyDetails).Width * 0.4) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه" : "Qty");
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGCustodyDetails).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGCustodyDetails).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCustodyDetails).Width * 0.4);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CustodyNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["CustodyNameEn"].Value.ToString();
		dtCustodyDetails = CustodyDetails.SelectByCustodyID(((UltraGridBase)ULGData).ActiveRow.Cells["CustodyID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridCustodyDetails();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم العهدة بالعربية", "Please Enter Custody Arabic Name");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		int num = Custody.Insert_Update("-1", ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		dtCustodyDetails.AcceptChanges();
		if (dtCustodyDetails.Rows.Count > 0)
		{
			for (int i = 0; i < dtCustodyDetails.Rows.Count; i++)
			{
				dtCustodyDetails.Rows[i]["CustodyID"] = num;
				dtCustodyDetails.Rows[i]["BranchID"] = GlobalVariables.CurrentBranchID;
			}
			CustodyDetails.Insert_UpdateByTable(dtCustodyDetails, GlobalVariables.UserID, IsFromServer: true);
		}
	}

	public override void UpdateData()
	{
		Custody.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["CustodyID"].Value.ToString(), ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		dtCustodyDetails.AcceptChanges();
		if (dtCustodyDetails.Rows.Count > 0)
		{
			for (int i = 0; i < dtCustodyDetails.Rows.Count; i++)
			{
				dtCustodyDetails.Rows[i]["CustodyID"] = ((UltraGridBase)ULGData).ActiveRow.Cells["CustodyID"].Value.ToString();
				dtCustodyDetails.Rows[i]["BranchID"] = GlobalVariables.CurrentBranchID;
			}
			CustodyDetails.Insert_UpdateByTable(dtCustodyDetails, GlobalVariables.UserID, IsFromServer: true);
		}
	}

	public override void DeleteData()
	{
		Custody.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["CustodyID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void ULGCustodyDetails_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCustodyDetails).ActiveRow).Selected = true;
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
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmHRCustody));
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
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.ULGCustodyDetails = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustodyDetails).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance20");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.ULGCustodyDetails, "ULGCustodyDetails");
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((SpecialBoxBase)((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val11;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val16, "appearance16");
		((AppearanceBase)val16).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val17).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val17).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val17).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGCustodyDetails).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.ULGCustodyDetails).Name = "ULGCustodyDetails";
		this.ULGCustodyDetails.AfterEnterEditMode += new System.EventHandler(ULGCustodyDetails_AfterEnterEditMode);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGCustodyDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmHRCustody";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGCustodyDetails, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustodyDetails).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
