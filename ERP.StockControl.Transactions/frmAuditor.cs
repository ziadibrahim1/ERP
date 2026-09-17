using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Transactions;

public class frmAuditor : frmDetails
{
	private DataRow drMaster;

	private DataTable dtItems;

	private DataTable dtItemsUnitsBarCode;

	private DataTable dtStoreTransferVouchers;

	private DataTable dtMinAllowedTransDate;

	private ValueList vlItems = new ValueList();

	private ValueList vlItemsCopy = new ValueList();

	private ValueList vlUnits = new ValueList();

	private int rowIndex = -1;

	private IContainer components = null;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	public UltraButton btnDiff;

	public frmAuditor()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SC_StoreTransferVouchers";
		IDCol = "StoreTransferVoucherID";
		NoCol = "StoreTransferVoucherNo";
		DateCol = "StoreTransferVoucherDate";
		((Control)(object)btnDiff).Text = (GlobalVariables.IsArabic ? "F1 مقارنة" : "Compare F1");
	}

	public override void PrepareData()
	{
		dtStoreTransferVouchers = StoreTransferVouchers.FillCombo("0");
		GlobalFunctions.FillCombo(cboHeader, dtStoreTransferVouchers, "StoreTransferVoucherID", "StoreTransferVoucherNo");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			vlItemsCopy.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtItemsUnitsBarCode = ItemsUnits.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDate(IsFromServer: false);
		dtDetails = StoreTransferVouchersDetails.SelectForAudit("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		InitGrid();
		UltraButton obj = btnNext;
		bool visible = (((Control)(object)btnPriveous).Visible = false);
		((Control)(object)obj).Visible = visible;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.8) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		dtDetails.Rows.Clear();
		InitGrid();
		if (cboHeader.SelectedIndex == -1)
		{
			drMaster = null;
			return;
		}
		DataTable dataTable = StoreTransferVouchers.Select(((TextEditorControlBase)cboHeader).Value.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			drMaster = dataTable.Rows[0];
		}
		else
		{
			drMaster = null;
		}
	}

	public override void btnSave_Click(object sender, EventArgs e)
	{
		if (HasChanges && cboHeader.SelectedIndex > -1 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			if (dtMinAllowedTransDate.Select(" StoreID= " + drMaster["SourceStoreID"].ToString() + " And Date > '" + Convert.ToDateTime(drMaster["StoreTransferVoucherDate"]).ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + drMaster["SourceStoreID"].ToString() + " And Date > '" + Convert.ToDateTime(drMaster["StoreTransferVoucherDate"]).ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على مخزن المصدر ", string.Concat("\n Cannot Update This Transaction Because Store Taking Date", dtMinAllowedTransDate.Select(" StoreID= " + drMaster["SourceStoreID"].ToString() + " And Date > '" + Convert.ToDateTime(drMaster["StoreTransferVoucherDate"]).ToString(GlobalVariables.DateLongFormate) + "'"), " \n  On Source Store "));
			}
			else if (Convert.ToBoolean(drMaster["Deliverd"]) && dtMinAllowedTransDate.Select(" StoreID= " + drMaster["DestinationStoreID"].ToString() + " And Date > '" + Convert.ToDateTime(drMaster["StoreTransferVoucherDate"]).ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لوجود  اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + drMaster["DestinationStoreID"].ToString() + " And Date > '" + Convert.ToDateTime(drMaster["StoreTransferVoucherDate"]).ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   على المخزن المحول اليه ", string.Concat("\n Cannot Update This Transaction Because Store Taking Date", dtMinAllowedTransDate.Select(" StoreID= " + drMaster["DestinationStoreID"].ToString() + " And Date > '" + Convert.ToDateTime(drMaster["StoreTransferVoucherDate"]).ToString(GlobalVariables.DateLongFormate) + "'"), " \n  On Distination Store "));
			}
			else
			{
				SaveData();
			}
		}
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			DataTable dataTable = StoreTransferVouchersDetails.AuditorSaveDiff((DataTable)((UltraGridBase)ULGData).DataSource, ((TextEditorControlBase)cboHeader).Value.ToString(), IsFromServer: false);
			ItemsTransactions.ManagementInsertUpdateDelete();
			string text = MessageLog.SelectByVoucherIDAndTransType(((TextEditorControlBase)cboHeader).Value.ToString(), "TrnFrom", "TrnTo", GlobalVariables.IsArabic ? "1" : "0");
			if (text != "")
			{
				GlobalVariables.InformationMB.Show(text);
				Main.RollbackBulkTrans(FromServer: false);
				MessageLog.DeleteByVoucherIDAndTransType(((TextEditorControlBase)cboHeader).Value.ToString(), "TrnFrom", "TrnTo");
			}
			else
			{
				Main.EndBulkTrans(FromServer: false);
				HasChanges = false;
				ItemsTransactions.ManageInThread();
				GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
			}
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		if (((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGrid();
		}
		else if (rowIndex > -1 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > rowIndex)
		{
			frmEnterQuantity frmEnterQuantity2 = new frmEnterQuantity(((UltraGridBase)ULGData).Rows[rowIndex].Cells["ItemID"].Text.Split('-')[0].ToString(), ((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString());
			frmEnterQuantity2.WindowState = FormWindowState.Normal;
			if (frmEnterQuantity2.ShowDialog() == DialogResult.OK)
			{
				((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[rowIndex].Cells["Qty"].Value.ToString()) + decimal.Parse(frmEnterQuantity2.Value);
			}
		}
	}

	public void AddItemInGrid()
	{
		bool flag = dtItems.Select(" ItemBarCode = '" + ((Control)(object)txtBarCode).Text + "'").Length == 0;
		bool flag2 = dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + ((Control)(object)txtBarCode).Text + "'").Length == 0;
		if (flag && flag2)
		{
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		if (flag)
		{
			DataRow dataRow = dtItemsUnitsBarCode.Select(" ItemUnitBarcode = '" + ((Control)(object)txtBarCode).Text + "'")[0];
			((Control)(object)txtBarCode).Text = dtItems.Select("ItemID = " + dataRow["ItemID"].ToString())[0]["ItemBarCode"].ToString();
			string text = dataRow["ItemID"].ToString();
			string text2 = dataRow["UnitID"].ToString();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == text)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + 1m;
					rowIndex = i;
					((TextEditorControlBase)txtBarCode).Clear();
					((TextEditorControlBase)txtBarCode).Focus();
					((Control)(object)btnSave).Enabled = true;
					((Control)(object)btnSaveAndClose).Enabled = true;
					((Control)(object)btnCancel).Enabled = true;
					HasChanges = true;
					return;
				}
			}
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dtItems.Select(" ItemBarCode = '" + ((Control)(object)txtBarCode).Text + "'")[0]["ItemID"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
			rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
			((Control)(object)btnSave).Enabled = true;
			((Control)(object)btnSaveAndClose).Enabled = true;
			((Control)(object)btnCancel).Enabled = true;
			HasChanges = true;
			((UltraGridBase)ULGData).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1].Cells[0].Activate();
			ULGData.PerformAction((UltraGridAction)24);
			((TextEditorControlBase)txtBarCode).Clear();
			((TextEditorControlBase)txtBarCode).Focus();
			return;
		}
		DataRow dataRow2 = dtItems.Select(" ItemBarcode = '" + ((Control)(object)txtBarCode).Text + "'")[0];
		string text3 = dataRow2["ItemID"].ToString();
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() == text3)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()) + 1m;
				rowIndex = j;
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				((Control)(object)btnSave).Enabled = true;
				((Control)(object)btnSaveAndClose).Enabled = true;
				((Control)(object)btnCancel).Enabled = true;
				HasChanges = true;
				return;
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = dataRow2["ItemID"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = 1;
		rowIndex = ((UltraGridBase)ULGData).ActiveRow.Index;
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
		HasChanges = true;
		((UltraGridBase)ULGData).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1].Cells[0].Activate();
		ULGData.PerformAction((UltraGridAction)24);
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.StoreTransferVouchersReport("-1", 0, 0, FromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			((TextEditorControlBase)cboHeader).Value = dtSearchResult.Rows[0]["StoreTransferVoucherID"].ToString();
		}
	}

	private void btnDiff_Click(object sender, EventArgs e)
	{
		if (HasChanges && cboHeader.SelectedIndex > -1 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			DataTable dataTable = StoreTransferVouchersDetails.AuditorDiff((DataTable)((UltraGridBase)ULGData).DataSource, ((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				frmAuditorState frmAuditorState2 = new frmAuditorState(dataTable, vlItemsCopy);
				frmAuditorState2.WindowState = FormWindowState.Normal;
				frmAuditorState2.ShowDialog();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Transactions.frmAuditor));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.btnDiff = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSave, "btnSave");
		resources.ApplyResources(base.ULGData, "ULGData");
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
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnSaveAndClose, "btnSaveAndClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this.btnDiff, "btnDiff");
		((System.Windows.Forms.Control)(object)this.btnDiff).Name = "btnDiff";
		((System.Windows.Forms.Control)(object)this.btnDiff).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnDiff).Click += new System.EventHandler(btnDiff_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDiff);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Name = "frmAuditor";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDiff, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
