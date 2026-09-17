using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesSupply : frmDetails
{
	private DataTable dtServices;

	private DataTable dtSuppliers;

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private object SupplierID;

	private bool ReadOnly;

	private DataRow drCurrentService;

	private bool NoInvoice;

	private IContainer components = null;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	public UltraButton btnSupplierSearch;

	private UltraLabel lblSupplier;

	private UltraComboEditor cboSupplier;

	public frmOperationsServicesSupply()
	{
		InitializeComponent();
	}

	public frmOperationsServicesSupply(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, object SUPPLIERID, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		SupplierID = SUPPLIERID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		drCurrentService = OperationsServices.Select(OperationServiceID, "-1", GlobalVariables.IsArabic ? "1" : "0").Rows[0];
		NoInvoice = ((drCurrentService["OperationInvoiceID"] == DBNull.Value) ? true : false);
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtSuppliers = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.SupplierSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSupplier, dtSuppliers, "SubAccountID", "SubAccountName");
		((TextEditorControlBase)cboSupplier).Value = SupplierID;
		UltraButton obj = btnHeaderSearch;
		UltraButton obj2 = btnNext;
		bool flag = (((Control)(object)btnPriveous).Visible = false);
		bool visible = (((Control)(object)obj2).Visible = flag);
		((Control)(object)obj).Visible = visible;
		((EditorButtonControlBase)cboHeader).ReadOnly = true;
		((TextEditorControlBase)cboHeader).Value = ServiceID;
		if (ReadOnly)
		{
			UltraButton obj3 = btnCancel;
			UltraButton obj4 = btnSave;
			flag = (((Control)(object)btnSaveAndClose).Enabled = false);
			visible = (((Control)(object)obj4).Enabled = flag);
			((Control)(object)obj3).Enabled = visible;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceSupplyID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceSupplyDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceSupplyDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceSupplyDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.6);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			dtDetails = OperationsServicesSupply.SelectByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		if (!NoInvoice && Convert.ToInt32(drCurrentService["Qty"]) != ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) == 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال الكمية", "Please Enter Qty");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["Qty"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceSupplyDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار التاريخ", "Please Select Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceSupplyDate"]).Selected = true;
				return false;
			}
		}
		return true;
	}

	public override void SaveData()
	{
		if (NoInvoice)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				string text = ",";
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
					((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceSupplyID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("MS_OperationsServicesSupply", "OperationServiceID", OperationServiceID, "OperationServiceSupplyID", text);
				if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
				{
					OperationsServicesSupply.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
				}
				Main.ExecuteNonQuery(" Update MS_OperationsServices  set SupplierSubAccountID= " + ((cboSupplier.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSupplier).Value.ToString()) + " Where OperationServiceID=" + OperationServiceID.ToString());
				DataTable dataTable = (DataTable)((UltraGridBase)ULGData).DataSource;
				object obj = dataTable.Compute(" Sum(Qty) ", "");
				OperationsServices.UpdateTotals(OperationServiceID, (obj == DBNull.Value) ? "0" : obj.ToString());
				Main.EndBulkTrans(FromServer: false);
				DisplayData();
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
				SaveError = true;
				return;
			}
		}
		GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (ReadOnly)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnSupplierSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Suppliers("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSupplier).Value = num;
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
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Expected O, but got Unknown
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesSupply));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		this.btnSupplierSearch = new UltraButton();
		this.lblSupplier = new UltraLabel();
		this.cboSupplier = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).BeginInit();
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
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
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
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)base.ULGData).FilterRow += new FilterRowEventHandler(ULGData_FilterRow);
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		resources.ApplyResources(base.lblHeader, "lblHeader");
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
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val8, "appearance10");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this.btnSupplierSearch, "btnSupplierSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance11");
		((ControlBase)this.btnSupplierSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Name = "btnSupplierSearch";
		((System.Windows.Forms.Control)(object)this.btnSupplierSearch).Click += new System.EventHandler(btnSupplierSearch_Click);
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		this.lblSupplier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		resources.ApplyResources(this.cboSupplier, "cboSupplier");
		((TextEditorControlBase)this.cboSupplier).AlwaysInEditMode = true;
		this.cboSupplier.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSupplier).Name = "cboSupplier";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSupplierSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Name = "frmOperationsServicesSupply";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSupplierSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupplier).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
