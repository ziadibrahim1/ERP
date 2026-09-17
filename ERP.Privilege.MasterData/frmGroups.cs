using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Privilege.MasterData;

public class frmGroups : frmGrid
{
	private int GroupLevel;

	private IContainer components = null;

	private UltraCheckEditor chkEnabled;

	private UltraLabel lblGroupAr;

	private UltraTextEditor txtGroupName;

	private UltraTextEditor txtGroupLevel;

	private UltraLabel lblGroupLevel;

	private UltraLabel lblGroupNameEn;

	private UltraTextEditor txtGroupNameEn;

	public frmGroups()
	{
		InitializeComponent();
		TableName = "Prv_Groups";
		IDCol = "GroupID";
		if (GlobalVariables.UserID == "1")
		{
			GroupLevel = -1;
		}
		else
		{
			GroupLevel = Convert.ToInt32(Groups.Select(GlobalVariables.GroupID, "-1", "1", IsFromServer: true).Rows[0]["GroupLevel"]);
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtGroupName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGroupNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGroupLevel).ReadOnly = NavMode;
		((Control)(object)chkEnabled).Enabled = !NavMode;
		((TextEditorControlBase)txtGroupName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtGroupName).Clear();
		((TextEditorControlBase)txtGroupNameEn).Clear();
		((TextEditorControlBase)txtGroupLevel).Clear();
		((UltraToggleEditorBase)chkEnabled).Checked = false;
	}

	public override void FillData()
	{
		dataTable = Groups.SelectByLevel(GroupLevel.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المجموعة" : "Group Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزيه" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].Header).Caption = (GlobalVariables.IsArabic ? "فع\u0651الة" : "Enabled");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Enabled"].DefaultCellValue = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupLevel"].Header).Caption = (GlobalVariables.IsArabic ? "مستوى المجموعة" : "Group Level");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupLevel"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GroupLevel"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtGroupName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["GroupNameAr"].Value.ToString();
		((Control)(object)txtGroupNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["GroupNameEn"].Value.ToString();
		((Control)(object)txtGroupLevel).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["GroupLevel"].Value.ToString();
		((UltraToggleEditorBase)chkEnabled).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Enabled"].Value.ToString());
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtGroupName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم المجموعة", "Please Enter Group Name");
			((TextEditorControlBase)txtGroupName).Focus();
			return false;
		}
		if (dataTable.Select("GroupNameAr='" + ((Control)(object)txtGroupName).Text + "'" + (Adding ? "" : (" And GroupID <>" + ((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value.ToString()))).Length != 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تكرار الإسم العربي", "Please Insert User Name");
			((TextEditorControlBase)txtGroupName).Focus();
			return false;
		}
		if (((Control)(object)txtGroupLevel).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مستوى المجموعة", "Please Enter Group Level");
			((TextEditorControlBase)txtGroupLevel).Focus();
			return false;
		}
		if (Convert.ToInt32(((Control)(object)txtGroupLevel).Text) <= GroupLevel)
		{
			GlobalVariables.InformationMB.Show(" مستوى المجموعة يجب ان يكون اكبر من    " + GroupLevel, " Group Level Should Be Greater Than    " + GroupLevel);
			((TextEditorControlBase)txtGroupLevel).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Groups.Insert_Update("-1", ((Control)(object)txtGroupName).Text, ((Control)(object)txtGroupNameEn).Text, ((UltraToggleEditorBase)chkEnabled).Checked ? "1" : "0", ((Control)(object)txtGroupLevel).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Groups.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value.ToString(), ((Control)(object)txtGroupName).Text, ((Control)(object)txtGroupNameEn).Text, ((UltraToggleEditorBase)chkEnabled).Checked ? "1" : "0", ((Control)(object)txtGroupLevel).Text, GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			GroupsFormsFunctions.DeleteByGroupID(((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsForms.DeleteByGroupID(((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsFunctions.DeleteByGroupID(((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			Groups.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["GroupID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا فى عملية الحذف  ", "Error Occured");
		}
	}

	private void ultraTextEditor1_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Privilege.MasterData.frmGroups));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.chkEnabled = new UltraCheckEditor();
		this.lblGroupAr = new UltraLabel();
		this.txtGroupName = new UltraTextEditor();
		this.txtGroupLevel = new UltraTextEditor();
		this.lblGroupLevel = new UltraLabel();
		this.lblGroupNameEn = new UltraLabel();
		this.txtGroupNameEn = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnabled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupLevel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupNameEn).BeginInit();
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
		resources.ApplyResources(val8, "appearance10");
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
		resources.ApplyResources(this.chkEnabled, "chkEnabled");
		((System.Windows.Forms.Control)(object)this.chkEnabled).Name = "chkEnabled";
		resources.ApplyResources(this.lblGroupAr, "lblGroupAr");
		this.lblGroupAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroupAr).Name = "lblGroupAr";
		((ControlBase)this.lblGroupAr).WrapText = false;
		resources.ApplyResources(this.txtGroupName, "txtGroupName");
		((System.Windows.Forms.Control)(object)this.txtGroupName).Name = "txtGroupName";
		resources.ApplyResources(this.txtGroupLevel, "txtGroupLevel");
		((System.Windows.Forms.Control)(object)this.txtGroupLevel).Name = "txtGroupLevel";
		((System.Windows.Forms.Control)(object)this.txtGroupLevel).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ultraTextEditor1_KeyPress);
		resources.ApplyResources(this.lblGroupLevel, "lblGroupLevel");
		this.lblGroupLevel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroupLevel).Name = "lblGroupLevel";
		((ControlBase)this.lblGroupLevel).WrapText = false;
		resources.ApplyResources(this.lblGroupNameEn, "lblGroupNameEn");
		this.lblGroupNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGroupNameEn).Name = "lblGroupNameEn";
		((ControlBase)this.lblGroupNameEn).WrapText = false;
		resources.ApplyResources(this.txtGroupNameEn, "txtGroupNameEn");
		((System.Windows.Forms.Control)(object)this.txtGroupNameEn).Name = "txtGroupNameEn";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGroupLevel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroupLevel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGroupNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGroupName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroupNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnabled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGroupAr);
		base.Name = "frmGroups";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroupAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnabled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroupNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGroupName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGroupNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGroupLevel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGroupLevel, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnabled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupLevel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGroupNameEn).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
