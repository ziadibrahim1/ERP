using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Sling;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.MasterData;

public class frmTerminationsClosedSocket : frmGrid
{
	private DataTable dtItems;

	private ValueList vlItems = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtDiameter;

	private UltraLabel lblDiameter;

	public UltraButton btnItemsSearch;

	private UltraComboEditor cboItems;

	private UltraLabel lblItem;

	public frmTerminationsClosedSocket()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SLN_TerminationsClosedSocket";
		IDCol = "TerminationsClosedSocketID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtItems = Items.FillCombo("1", "0", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItems, dtItems, "ItemID", "Name");
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtDiameter).ReadOnly = NavMode;
		((EditorButtonControlBase)cboItems).ReadOnly = NavMode;
		((Control)(object)btnItemsSearch).Visible = !NavMode;
		((TextEditorControlBase)cboItems).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtDiameter).Clear();
		cboItems.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = TerminationsClosedSocket.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].Header).Caption = (GlobalVariables.IsArabic ? "القطر" : "Diameter");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TerminationClosedSocketItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TerminationClosedSocketItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TerminationClosedSocketItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TerminationClosedSocketItemID"].ValueList = (IValueList)(object)vlItems;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtDiameter).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Diameter"].Value.ToString();
		((TextEditorControlBase)cboItems).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["TerminationClosedSocketItemID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtDiameter).Text.Trim() == "" || decimal.Parse(((Control)(object)txtDiameter).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال القطر", "Please Enter The Diameter");
			((TextEditorControlBase)txtDiameter).Focus();
			return false;
		}
		if (cboItems.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الصنف" : "Please Select The Item");
			((TextEditorControlBase)cboItems).Focus();
			cboItems.DropDown();
			return false;
		}
		if (Main.CheckForValue("SLN_TerminationsClosedSocket", "TerminationClosedSocketItemID", ((TextEditorControlBase)cboItems).Value.ToString(), Updating ? ((UltraGridBase)ULGData).ActiveRow.Cells["TerminationClosedSocketItemID"].Value.ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" الصنف متواجد من قبل ", "This Item Already Exists");
			((TextEditorControlBase)cboItems).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		TerminationsClosedSocket.Insert_Update("-1", (cboItems.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItems).Value.ToString(), ((Control)(object)txtDiameter).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		TerminationsClosedSocket.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["TerminationsClosedSocketID"].Value.ToString(), (cboItems.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItems).Value.ToString(), ((Control)(object)txtDiameter).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		TerminationsClosedSocket.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["TerminationsClosedSocketID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Items("-1", "0", "-1", "1", "1", "-1", "-1", "0", "-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboItems).Value = num;
		}
	}

	public override void btnRefreshDataClick()
	{
		dtItems = Items.FillCombo("1", "0", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItems, dtItems, "ItemID", "Name");
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
	}

	private void txtDiameter_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.MasterData.frmTerminationsClosedSocket));
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
		this.txtDiameter = new UltraTextEditor();
		this.lblDiameter = new UltraLabel();
		this.btnItemsSearch = new UltraButton();
		this.cboItems = new UltraComboEditor();
		this.lblItem = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiameter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItems).BeginInit();
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
		resources.ApplyResources(this.txtDiameter, "txtDiameter");
		((System.Windows.Forms.Control)(object)this.txtDiameter).Name = "txtDiameter";
		((System.Windows.Forms.Control)(object)this.txtDiameter).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiameter_KeyPress);
		resources.ApplyResources(this.lblDiameter, "lblDiameter");
		this.lblDiameter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiameter).Name = "lblDiameter";
		((ControlBase)this.lblDiameter).WrapText = false;
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.cboItems, "cboItems");
		this.cboItems.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboItems).Name = "cboItems";
		resources.ApplyResources(this.lblItem, "lblItem");
		this.lblItem.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItem).Name = "lblItem";
		((ControlBase)this.lblItem).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiameter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiameter);
		base.Name = "frmTerminationsClosedSocket";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiameter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiameter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiameter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
