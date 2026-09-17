using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.HMS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HMS.FrontOffice;

public class frmFrontOfficeBasics : frmGrid
{
	private IContainer components = null;

	private UltraTextEditor txtRoomsCount;

	private UltraLabel lblRoomsCount;

	private UltraTextEditor txtSuiteCount;

	private UltraLabel lblSuiteCount;

	private UltraTextEditor txtBedsCount;

	private UltraLabel lblBedsCount;

	public frmFrontOfficeBasics()
	{
		InitializeComponent();
		TableName = "HMS_FrontOfficeBasics";
		IDCol = "FrontOfficeBasicID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtRoomsCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSuiteCount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBedsCount).ReadOnly = NavMode;
		((TextEditorControlBase)txtRoomsCount).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtRoomsCount).Clear();
		((TextEditorControlBase)txtSuiteCount).Clear();
		((TextEditorControlBase)txtBedsCount).Clear();
	}

	public override void FillData()
	{
		dataTable = FrontOfficeBasics.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].Header).Caption = (GlobalVariables.IsArabic ? " عدد الغرف" : "Rooms Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.33);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SuiteCount"].Header).Caption = (GlobalVariables.IsArabic ? "عددالإجنحة" : "Suite Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SuiteCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SuiteCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.33);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BedsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الإسرة" : "Beds Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BedsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BedsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.33);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtRoomsCount).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["RoomsCount"].Value.ToString();
		((Control)(object)txtSuiteCount).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["SuiteCount"].Value.ToString();
		((Control)(object)txtBedsCount).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["BedsCount"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtRoomsCount).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الغرف", "Please Enter Rooms Count");
			return false;
		}
		if (((Control)(object)txtSuiteCount).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الاجنحة", "Please Enter Suite Count");
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0 && Adding)
		{
			GlobalVariables.InformationMB.Show("لايمكن إضافة بيان جديد", "Cannot Add New Description");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		FrontOfficeBasics.Insert_Update("-1", (((Control)(object)txtRoomsCount).Text == "") ? "Null" : ((Control)(object)txtRoomsCount).Text, (((Control)(object)txtSuiteCount).Text == "") ? "Null" : ((Control)(object)txtSuiteCount).Text, (((Control)(object)txtBedsCount).Text == "") ? "Null" : ((Control)(object)txtBedsCount).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
	}

	public override void UpdateData()
	{
		FrontOfficeBasics.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["FrontOfficeBasicID"].Value.ToString(), (((Control)(object)txtRoomsCount).Text == "") ? "Null" : ((Control)(object)txtRoomsCount).Text, (((Control)(object)txtSuiteCount).Text == "") ? "Null" : ((Control)(object)txtSuiteCount).Text, (((Control)(object)txtBedsCount).Text == "") ? "Null" : ((Control)(object)txtBedsCount).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
	}

	public override void DeleteData()
	{
		FrontOfficeBasics.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["FrontOfficeBasicID"].Value.ToString(), GlobalVariables.UserID);
	}

	public override void btnAddClick()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			GlobalVariables.InformationMB.Show("لايمكن إضافة بيان جديد", "Cannot Add New Description");
		}
		else
		{
			base.btnAddClick();
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HMS.FrontOffice.frmFrontOfficeBasics));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.txtRoomsCount = new UltraTextEditor();
		this.lblRoomsCount = new UltraLabel();
		this.txtSuiteCount = new UltraTextEditor();
		this.lblSuiteCount = new UltraLabel();
		this.txtBedsCount = new UltraTextEditor();
		this.lblBedsCount = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomsCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSuiteCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBedsCount).BeginInit();
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
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtRoomsCount, "txtRoomsCount");
		((System.Windows.Forms.Control)(object)this.txtRoomsCount).Name = "txtRoomsCount";
		resources.ApplyResources(this.lblRoomsCount, "lblRoomsCount");
		this.lblRoomsCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoomsCount).Name = "lblRoomsCount";
		((ControlBase)this.lblRoomsCount).WrapText = false;
		resources.ApplyResources(this.txtSuiteCount, "txtSuiteCount");
		((System.Windows.Forms.Control)(object)this.txtSuiteCount).Name = "txtSuiteCount";
		resources.ApplyResources(this.lblSuiteCount, "lblSuiteCount");
		this.lblSuiteCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSuiteCount).Name = "lblSuiteCount";
		((ControlBase)this.lblSuiteCount).WrapText = false;
		resources.ApplyResources(this.txtBedsCount, "txtBedsCount");
		((System.Windows.Forms.Control)(object)this.txtBedsCount).Name = "txtBedsCount";
		resources.ApplyResources(this.lblBedsCount, "lblBedsCount");
		this.lblBedsCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBedsCount).Name = "lblBedsCount";
		((ControlBase)this.lblBedsCount).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBedsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBedsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSuiteCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSuiteCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoomsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoomsCount);
		base.Name = "frmFrontOfficeBasics";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoomsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoomsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSuiteCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSuiteCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBedsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBedsCount, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomsCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSuiteCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBedsCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
