using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmChecksDetailsAccessories : frmBase
{
	private DataTable dtItemsAccessories;

	private DataTable dtUnits;

	public DataTable dtChecksDetailsAccessories;

	public int AccessoriesCount = 0;

	private int StoreID;

	private int CheckDetailID;

	private int ItemID;

	private bool CanEdit = true;

	public bool Cancel = false;

	private bool EnforceAccessories = false;

	private ValueList vlItemsAccessories = new ValueList();

	private ValueList vlUnits = new ValueList();

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraButton btnClose;

	private UltraButton btnSave;

	public UltraButton btnKeyboard;

	private UltraGroupBox UGBItemData;

	private UltraPanel pnlItems;

	public UltraGroupBox UGBDetails;

	public UltraGrid ULGData;

	private UltraLabel lblAccessoriesCount;

	private UltraTextEditor txtAccessoriesCount;

	private UltraButton btnClear;

	private UltraButton btnSubtract;

	public frmChecksDetailsAccessories()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmChecksDetailsAccessories(int accessoriescount, int itemid, int storeid, int checkdetailid, bool canedit, bool enforceAccessories)
		: this()
	{
		ItemID = itemid;
		AccessoriesCount = accessoriescount;
		StoreID = storeid;
		CheckDetailID = checkdetailid;
		CanEdit = canedit;
		EnforceAccessories = enforceAccessories;
	}

	public override void PrepareData()
	{
		dtItemsAccessories = ItemsAccessories.FillComboByItemIds("," + ItemID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsAccessories.ValueListItems.Clear();
		for (int i = 0; i < dtItemsAccessories.Rows.Count; i++)
		{
			vlItemsAccessories.ValueListItems.Add((object)dtItemsAccessories.Rows[i]["AccessoriesItemID"].ToString(), dtItemsAccessories.Rows[i]["ItemName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int j = 0; j < dtUnits.Rows.Count; j++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
		}
		((Control)(object)txtAccessoriesCount).Text = AccessoriesCount.ToString();
		CreateButtons();
		InitGrid();
		((Control)(object)pnlItems).Enabled = CanEdit;
		((Control)(object)btnClear).Enabled = CanEdit;
		((Control)(object)btnSubtract).Enabled = CanEdit;
	}

	public void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtChecksDetailsAccessories;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDetailAccessoryID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccessoriesCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Header).Caption = (GlobalVariables.IsArabic ? "كمية الملحق" : "Accessory Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccessoriesCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الملحق" : "Accessory Count");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccessoriesCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItemsAccessories;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccessoriesCount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (EnforceAccessories)
		{
			if (ValidateAccessories())
			{
				dtChecksDetailsAccessories.AcceptChanges();
				Close();
			}
		}
		else
		{
			dtChecksDetailsAccessories.AcceptChanges();
			Close();
		}
	}

	private bool ValidateAccessories()
	{
		object obj = dtChecksDetailsAccessories.Compute(" Sum(AccessoriesCount) ", "");
		if (obj == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(" إجمالى الملحقات المختارة أقل من عدد الملحقات ", "Selected Accessories Less Than Accessories Count");
			return false;
		}
		if (int.Parse(obj.ToString()) < int.Parse(((Control)(object)txtAccessoriesCount).Text))
		{
			GlobalVariables.InformationMB.Show(" إجمالى الملحقات المختارة أقل من عدد الملحقات ", "Selected Accessories Less Than Accessories Count");
			return false;
		}
		return true;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (EnforceAccessories)
		{
			if (ValidateAccessories())
			{
				dtChecksDetailsAccessories.AcceptChanges();
				Close();
			}
		}
		else
		{
			Cancel = true;
			Close();
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	public void CreateButtons()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		((Control)(object)pnlItems).Visible = false;
		((Control)(object)pnlItems.ClientArea).Controls.Clear();
		int num = 8;
		int num2 = 6;
		for (int i = 0; i < dtItemsAccessories.Rows.Count; i++)
		{
			UltraButton val = new UltraButton();
			((Control)(object)val).Click += btnItems_Click;
			((Control)(object)val).Tag = dtItemsAccessories.Rows[i]["AccessoriesItemID"].ToString();
			((Control)(object)val).Text = dtItemsAccessories.Rows[i]["ItemName"].ToString();
			((Control)(object)val).Height += 25;
			((Control)(object)pnlItems.ClientArea).Controls.Add((Control)(object)val);
			((UltraControlBase)val).Update();
			if (num + ((Control)(object)val).Width > ((Control)(object)pnlItems).Width)
			{
				num2 += ((Control)(object)val).Height;
				num = 8;
			}
			((Control)(object)val).Left = num;
			((Control)(object)val).Top = num2;
			((Control)(object)val).Width = (((Control)(object)pnlItems).Width - GlobalVariables.ScrollWidth) / 6;
			num += ((Control)(object)val).Width;
		}
		((Control)(object)pnlItems).Visible = true;
	}

	public void btnItems_Click(object sender, EventArgs e)
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		object obj = dtChecksDetailsAccessories.Compute(" Sum(AccessoriesCount) ", "");
		if (obj != DBNull.Value && int.Parse(obj.ToString()) + 1 > int.Parse(((Control)(object)txtAccessoriesCount).Text))
		{
			GlobalVariables.InformationMB.Show(" إجمالى الملحقات المختارة أكثر من عدد الملحقات ", "Selected Accessories Exceed Accessories Count");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (int.Parse(((Control)(UltraButton)sender).Tag.ToString()) == int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString()))
			{
				DataRow dataRow = dtItemsAccessories.Select(" AccessoriesItemID=  " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).Rows[i].Cells["OriginalQty"].Value = decimal.Parse(dataRow["Qty"].ToString());
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(dataRow["Qty"].ToString());
				((UltraGridBase)ULGData).Rows[i].Cells["AccessoriesCount"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["OriginalQty"].Value.ToString());
				((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value = dataRow["UnitID"];
				((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value = StoreID;
				return;
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = ((Control)(UltraButton)sender).Tag;
		DataRow dataRow2 = dtItemsAccessories.Select(" AccessoriesItemID=  " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["OriginalQty"];
		object value = (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(dataRow2["Qty"].ToString()));
		obj2.Value = value;
		((UltraGridBase)ULGData).ActiveRow.Cells["AccessoriesCount"].Value = 1;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
		((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = StoreID;
		((UltraGridBase)ULGData).ActiveRow.Cells["CheckDetailID"].Value = CheckDetailID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Notes")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnSubtract_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AccessoriesCount"].Value.ToString()) > 1.0)
		{
			ULGData.ActiveCell = ((UltraGridBase)ULGData).ActiveRow.Cells["AccessoriesCount"];
			((UltraGridBase)ULGData).ActiveRow.Cells["AccessoriesCount"].Value = double.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AccessoriesCount"].Value.ToString()) - 1.0;
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OriginalQty"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AccessoriesCount"].Value.ToString());
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraGridBase)ULGData).ActiveRow.Delete(false);
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksDetailsAccessories));
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
		this.pnlItems = new UltraPanel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.UGBItemData = new UltraGroupBox();
		this.UGBDetails = new UltraGroupBox();
		this.ULGData = new UltraGrid();
		this.lblAccessoriesCount = new UltraLabel();
		this.txtAccessoriesCount = new UltraTextEditor();
		this.btnClear = new UltraButton();
		this.btnSubtract = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAccessoriesCount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlItems, "pnlItems");
		this.pnlItems.AutoScroll = true;
		resources.ApplyResources(this.pnlItems.ClientArea, "pnlItems.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlItems).Name = "pnlItems";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.UGBItemData, "UGBItemData");
		this.UGBItemData.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItemData).Controls.Add((System.Windows.Forms.Control)(object)this.pnlItems);
		((System.Windows.Forms.Control)(object)this.UGBItemData).Name = "UGBItemData";
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val3).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val4;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((AppearanceBase)val9).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		resources.ApplyResources(this.lblAccessoriesCount, "lblAccessoriesCount");
		this.lblAccessoriesCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAccessoriesCount).Name = "lblAccessoriesCount";
		((ControlBase)this.lblAccessoriesCount).WrapText = false;
		resources.ApplyResources(this.txtAccessoriesCount, "txtAccessoriesCount");
		((System.Windows.Forms.Control)(object)this.txtAccessoriesCount).Name = "txtAccessoriesCount";
		((EditorButtonControlBase)this.txtAccessoriesCount).ReadOnly = true;
		resources.ApplyResources(this.btnClear, "btnClear");
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		resources.ApplyResources(this.btnSubtract, "btnSubtract");
		((System.Windows.Forms.Control)(object)this.btnSubtract).Name = "btnSubtract";
		((System.Windows.Forms.Control)(object)this.btnSubtract).Click += new System.EventHandler(btnSubtract_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnSave;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAccessoriesCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSubtract);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAccessoriesCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItemData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmChecksDetailsAccessories";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItemData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAccessoriesCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSubtract, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAccessoriesCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlItems).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBItemData).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItemData).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAccessoriesCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
