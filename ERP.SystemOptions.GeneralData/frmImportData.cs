using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Sling;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SystemOptions.GeneralData;

public class frmImportData : frmBase
{
	private DataTable dtVouchers;

	public DataTable dt;

	private DataTable dtTypes = new DataTable();

	private DataTable dtForms;

	private string FormName = "";

	private IContainer components = null;

	public UltraLabel lblTitle;

	private UltraLabel lblVouchers;

	private UltraComboEditor cboVouchers;

	private UltraButton btnSave;

	private UltraButton btnClose;

	private UltraComboEditor cboTypes;

	private UltraLabel lblType;

	public UltraButton btnVouchersSearch;

	public frmImportData()
	{
		InitializeComponent();
	}

	public frmImportData(DataTable _dtForms)
		: this()
	{
		dtForms = _dtForms;
	}

	public frmImportData(string formname)
		: this()
	{
		FormName = formname;
	}

	public override void PrepareData()
	{
		dtTypes.Columns.Add("TypeID");
		dtTypes.Columns.Add("TypeName");
		if (dtForms.Select("TypeID = 1").Length != 0)
		{
			DataRow dataRow = dtTypes.NewRow();
			dataRow[0] = "1";
			dataRow[1] = (GlobalVariables.IsArabic ? "إذن إضافة" : "Good Receipt Note");
			dtTypes.Rows.Add(dataRow);
		}
		if (dtForms.Select("TypeID = 2").Length != 0)
		{
			DataRow dataRow2 = dtTypes.NewRow();
			dataRow2[0] = "2";
			dataRow2[1] = (GlobalVariables.IsArabic ? "Sling Orders" : "Sling Orders");
			dtTypes.Rows.Add(dataRow2);
		}
		if (dtForms.Select("TypeID = 3").Length != 0)
		{
			DataRow dataRow3 = dtTypes.NewRow();
			dataRow3[0] = "3";
			dataRow3[1] = (GlobalVariables.IsArabic ? "إذن تحويل" : "Store Transfer Voucher");
			dtTypes.Rows.Add(dataRow3);
		}
		GlobalFunctions.FillCombo(cboTypes, dtTypes, "TypeID", "TypeName");
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (cboVouchers.SelectedIndex > -1)
		{
			if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 1)
			{
				dt = GoodReceiptNotesDetails.ImportDataByGoodReceiptNoteID(((TextEditorControlBase)cboVouchers).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
			else if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 2)
			{
				dt = Quotations.ImportDataByQuotationID(((TextEditorControlBase)cboVouchers).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
			else if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 3)
			{
				dt = StoreTransferVouchersDetails.ImportDataByStoreTransferVoucherID(((TextEditorControlBase)cboVouchers).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
			Close();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لم تقم بإختيار أي إذن " : "You did not select any voucher");
		}
	}

	private void cboTypes_ValueChanged(object sender, EventArgs e)
	{
		cboVouchers.DataSource = null;
		if (cboTypes.SelectedIndex > -1)
		{
			if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 1)
			{
				dtVouchers = GoodReceiptNotes.FillCombo(GlobalVariables.BranchIDs);
				GlobalFunctions.FillCombo(cboVouchers, dtVouchers, "GoodReceiptNoteID", "GoodReceiptNoteNo");
			}
			else if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 2)
			{
				dtVouchers = Quotations.FillCombo(GlobalVariables.BranchIDs);
				GlobalFunctions.FillCombo(cboVouchers, dtVouchers, "QuotationID", "QuotationNo");
			}
			else if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 3)
			{
				dtVouchers = StoreTransferVouchers.FillCombo("1");
				GlobalFunctions.FillCombo(cboVouchers, dtVouchers, "StoreTransferVoucherID", "StoreTransferVoucherNo");
			}
		}
	}

	private void btnVouchersSearch_Click(object sender, EventArgs e)
	{
		if (cboTypes.SelectedIndex > -1)
		{
			int num = 0;
			if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 1)
			{
				num = SearchFunctions.GoodReceiptNotesSearch("," + GlobalVariables.CurrentBranchID + ",", -1, 0);
			}
			else if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 2)
			{
				num = SearchFunctions.SLNQuotationsSearch(-1, 0, -1);
			}
			else if (int.Parse(((TextEditorControlBase)cboTypes).Value.ToString()) == 3)
			{
				num = SearchFunctions.StoreTransferVouchersLnsSearch(FromServer: false);
			}
			if (num != 0)
			{
				((TextEditorControlBase)cboVouchers).Value = num;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		Appearance val = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmImportData));
		Appearance val2 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.lblVouchers = new UltraLabel();
		this.cboVouchers = new UltraComboEditor();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		this.cboTypes = new UltraComboEditor();
		this.lblType = new UltraLabel();
		this.btnVouchersSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVouchers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).BeginInit();
		base.SuspendLayout();
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		this.lblVouchers.AutoEllipsis = false;
		resources.ApplyResources(this.lblVouchers, "lblVouchers");
		((System.Windows.Forms.Control)(object)this.lblVouchers).Name = "lblVouchers";
		((ControlBase)this.lblVouchers).WrapText = false;
		((TextEditorControlBase)this.cboVouchers).AlwaysInEditMode = true;
		this.cboVouchers.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboVouchers, "cboVouchers");
		((System.Windows.Forms.Control)(object)this.cboVouchers).Name = "cboVouchers";
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		((TextEditorControlBase)this.cboTypes).AlwaysInEditMode = true;
		this.cboTypes.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTypes, "cboTypes");
		((System.Windows.Forms.Control)(object)this.cboTypes).Name = "cboTypes";
		((TextEditorControlBase)this.cboTypes).ValueChanged += new System.EventHandler(cboTypes_ValueChanged);
		this.lblType.AutoEllipsis = false;
		resources.ApplyResources(this.lblType, "lblType");
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		((UltraButtonBase)this.btnVouchersSearch).AcceptsFocus = false;
		((AppearanceBase)val2).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnVouchersSearch).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(this.btnVouchersSearch, "btnVouchersSearch");
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Name = "btnVouchersSearch";
		((System.Windows.Forms.Control)(object)this.btnVouchersSearch).Click += new System.EventHandler(btnVouchersSearch_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVouchersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTypes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVouchers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVouchers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmImportData";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVouchers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVouchers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTypes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVouchersSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVouchers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
