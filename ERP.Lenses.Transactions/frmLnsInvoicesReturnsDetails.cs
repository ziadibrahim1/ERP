using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Transactions;

public class frmLnsInvoicesReturnsDetails : frmBase
{
	public DataTable DtMaxReturnedQtyOrignal;

	public DataTable DtMaxReturnedQty;

	private ValueList vlItems = new ValueList();

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraLabel lblTitle;

	private UltraButton btnSave;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	public frmLnsInvoicesReturnsDetails()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmLnsInvoicesReturnsDetails(DataTable dtmaxreturnedQtyorignal, DataTable dtmaxreturnedqty)
		: this()
	{
		DtMaxReturnedQtyOrignal = dtmaxreturnedQtyorignal;
		DtMaxReturnedQty = dtmaxreturnedqty;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < DtMaxReturnedQtyOrignal.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(DtMaxReturnedQtyOrignal.Rows[i]["ItemBarCode"], DtMaxReturnedQtyOrignal.Rows[i]["Name"].ToString());
		}
		InitGrid();
	}

	public void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = DtMaxReturnedQty;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxReturnedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxReturnedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الاقصى" : "Max limit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxReturnedQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxReturnedQty"].Format = GlobalVariables.QtyDecimals;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["MaxReturnedQty"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["ColorID"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["ItemSizeID"].ToString();
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["BatchID"];
			((UltraGridBase)ULGData).ActiveRow.Cells["OriginalItemID"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["OriginalItemID"].ToString();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && DtMaxReturnedQtyOrignal.Select(" ItemBarCode = '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'").Length == 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "هذا الصنف غير متواجد فى مرتجعات الفترة" : "this Item Is Not Available In the Return Period");
				((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value = 0;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = DBNull.Value;
			}
			if (((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value != DBNull.Value && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(" الحد الاقصى للكمية المرتجعة " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value.ToString(), " Max Allowed Returned Quantity " + ((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Qty" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ItemID" && ((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value != DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		((UltraGridBase)ULGData).UpdateData();
		if (((UltraGridBase)ULGData).ActiveRow == null)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			UltraGridRow val = ((UltraGridBase)ULGData).Rows[i];
			if (((UltraGridBase)ULGData).ActiveRow.Index != i && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() == val.Cells["ItemID"].Value.ToString())
			{
				GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف ", "Cannot Duplicate The Same Item ");
				((UltraGridBase)ULGData).ActiveRow.Delete(false);
				((GridItemBase)val).Selected = true;
				val.Activate();
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (DtMaxReturnedQtyOrignal.Select(" ItemBarCode = '" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + "'").Length == 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "هذا الصنف غير متواجد فى مرتجعات الفترة" : "this Item Is Not Available In the Return Period");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
				((UltraGridBase)ULGData).Rows[i].Activate();
				return;
			}
		}
		Close();
	}

	private void txtBarCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return && ((Control)(object)txtBarCode).Text != "")
		{
			AddItemInGid();
		}
	}

	public void AddItemInGid()
	{
		ArrayList arrayList = BarcodeFunctions.BarcodeSubstring(((Control)(object)txtBarCode).Text);
		string text = arrayList[0].ToString();
		string text2 = arrayList[1].ToString();
		string text3 = arrayList[2].ToString();
		string s = arrayList[4].ToString();
		string text4 = text + "," + text2 + "," + text3;
		if (DtMaxReturnedQtyOrignal.Select(" ItemBarCode = '" + text4 + "'").Length == 0)
		{
			if (text4.StartsWith("000"))
			{
				text4 = "00" + text4.TrimStart('0');
			}
			if (DtMaxReturnedQtyOrignal.Select(" ItemBarCode = '" + text4 + "'").Length == 0)
			{
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				return;
			}
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (DtMaxReturnedQtyOrignal.Select(" ItemBarCode = '" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() + "'")[0]["ItemBarCode"].ToString() == text4)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value = decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) + decimal.Parse(s);
				((TextEditorControlBase)txtBarCode).Clear();
				((TextEditorControlBase)txtBarCode).Focus();
				return;
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = text4;
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = text2;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = text3;
		((UltraGridBase)ULGData).ActiveRow.Cells["OriginalItemID"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["OriginalItemID"].ToString();
		((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = decimal.Parse(s);
		((UltraGridBase)ULGData).ActiveRow.Cells["MaxReturnedQty"].Value = DtMaxReturnedQtyOrignal.Select(" ItemBarcode= '" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["MaxReturnedQty"].ToString();
		((UltraGridBase)ULGData).UpdateData();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((TextEditorControlBase)txtBarCode).Clear();
		((TextEditorControlBase)txtBarCode).Focus();
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
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Expected O, but got Unknown
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmLnsInvoicesReturnsDetails));
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
		this.ULGData = new UltraGrid();
		this.lblTitle = new UltraLabel();
		this.btnSave = new UltraButton();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val, "appearance1");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val2;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance7");
		((AppearanceBase)val7).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtBarCode_KeyUp);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmLnsInvoicesReturnsDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
