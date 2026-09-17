using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.StockControl;
using BusinessLayer.WareHouse;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.WareHouse.Transactions;

public class frmWareHouseIssue : frmHeaderDetails
{
	private DataTable dtStores;

	private DataTable dtItems;

	private DataTable dtUnits;

	private DataTable dtPercentage;

	private DataTable dtCompany;

	private ValueList vlStores = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlPercentage = new ValueList();

	private IContainer components = null;

	private UltraLabel lblInvoiceNo;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboCompany;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblCompany;

	private UltraDateTimeEditor dtpAWBDate;

	private UltraLabel lblAWBDate;

	private UltraDateTimeEditor dtpManifestDate;

	private UltraLabel lblManifestDate;

	private UltraTextEditor txtAWBNo;

	private UltraTextEditor txtManifestNo;

	private UltraLabel lblManifestNo;

	private UltraLabel lblAWBNo;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtInvoiceNo;

	private UltraLabel lblSupplier;

	private UltraTextEditor txtSupplier;

	private UltraLabel lblGRN;

	private UltraTextEditor txtGRN;

	private UltraLabel lblSupplierStore;

	private UltraTextEditor txtSupplierStore;

	public UltraButton btnGetAvailable;

	public frmWareHouseIssue()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		TableName = "WH_WareHouseIssue";
		IDCol = "WareHouseIssueID";
		NoCol = "WareHouseIssueNo";
		DateCol = "InvoiceDate";
	}

	public frmWareHouseIssue(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int i = 0; i < dtStores.Rows.Count; i++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[i]["StoreID"], dtStores.Rows[i]["StoreName"].ToString());
		}
		dtItems = Items.FillComboWH(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int j = 0; j < dtItems.Rows.Count; j++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int k = 0; k < dtUnits.Rows.Count; k++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[k]["UnitID"], dtUnits.Rows[k]["UnitName"].ToString());
		}
		dtPercentage = Percentages.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlPercentage.ValueListItems.Clear();
		for (int l = 0; l < dtPercentage.Rows.Count; l++)
		{
			vlPercentage.ValueListItems.Add(dtPercentage.Rows[l]["PercentageID"], dtPercentage.Rows[l]["Percentage"].ToString());
		}
		dtCompany = BusinessLayer.WareHouse.Company.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCompany, dtCompany, "CompanyID", "CompanyName");
		dtDetails = WareHouseIssueDetails.SelectByWareHouseIssueID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WareHouseIssueDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PareCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CallOff"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JobCenter"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MIAPCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Location"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "Item" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "Unit" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "Unit price" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "Total Price" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxQty"].Header).Caption = (GlobalVariables.IsArabic ? "Balance" : "Balance");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "Store" : "Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "Notes" : "Notes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "Qty" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Header).Caption = (GlobalVariables.IsArabic ? "%" : "%");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PareCode"].Header).Caption = (GlobalVariables.IsArabic ? "Pare Code" : "Pare Code");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CallOff"].Header).Caption = (GlobalVariables.IsArabic ? "Call Off" : "Call Off");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JobCenter"].Header).Caption = (GlobalVariables.IsArabic ? "Job Center" : "JobCenter");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MIAPCode"].Header).Caption = (GlobalVariables.IsArabic ? "MIAP Code" : "MIAP Code");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Location"].Header).Caption = (GlobalVariables.IsArabic ? "Location" : "Location");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PareCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CallOff"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JobCenter"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MIAPCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Location"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].ValueList = (IValueList)(object)vlPercentage;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = WareHouseIssue.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["WareHouseIssueNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["InvoiceDate"];
			((Control)(object)txtAWBNo).Text = drMaster["AWBNo"].ToString();
			dtpAWBDate.Value = (DateTime)drMaster["AWBDate"];
			((Control)(object)txtManifestNo).Text = drMaster["ManifestNo"].ToString();
			dtpManifestDate.Value = (DateTime)drMaster["ManifestDate"];
			((TextEditorControlBase)cboCompany).Value = drMaster["CompanyID"];
			((Control)(object)txtSupplier).Text = drMaster["Supplier"].ToString();
			((Control)(object)txtSupplierStore).Text = drMaster["Store"].ToString();
			((Control)(object)txtGRN).Text = drMaster["GRN"].ToString();
			((Control)(object)txtInvoiceNo).Text = drMaster["InvoiceNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = WareHouseIssueDetails.SelectByWareHouseIssueID(drMaster["WareHouseIssueID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			if (drMaster["Approved"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnUpdate).Visible = false;
		((Control)(object)btnCopyTo).Visible = false;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAWBNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpAWBDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtManifestNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpManifestDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSupplier).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSupplierStore).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGRN).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInvoiceNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnGetAvailable).Visible = Adding;
		if (Adding || Updating)
		{
			DataView dataView = new DataView(dtStores);
			dataView.RowFilter = " Locked =0 And BranchID= " + GlobalVariables.CurrentBranchID;
			DataTable dataTable = dataView.ToTable();
			vlStores.ValueListItems.Clear();
			for (int i = 0; i < dataView.ToTable().Rows.Count; i++)
			{
				vlStores.ValueListItems.Add(dataTable.Rows[i]["StoreID"], dataTable.Rows[i]["StoreName"].ToString());
			}
		}
		else
		{
			vlStores.ValueListItems.Clear();
			for (int j = 0; j < dtStores.Rows.Count; j++)
			{
				vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
			}
		}
		if (!Adding)
		{
			vlItems.ValueListItems.Clear();
			for (int k = 0; k < dtItems.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		DateTime serverDateTimeNow = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.DateTime = serverDateTimeNow;
		((Control)(object)txtCode).Text = (Adding ? WareHouseIssue.GetCodeByBranchID(GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtAWBNo).Clear();
		dtpAWBDate.DateTime = serverDateTimeNow;
		((TextEditorControlBase)txtManifestNo).Clear();
		dtpManifestDate.DateTime = serverDateTimeNow;
		cboCompany.SelectedIndex = -1;
		((TextEditorControlBase)txtSupplier).Clear();
		((TextEditorControlBase)txtSupplierStore).Clear();
		((TextEditorControlBase)txtGRN).Clear();
		((TextEditorControlBase)txtInvoiceNo).Clear();
		((TextEditorControlBase)txtNotes).Clear();
	}

	public override bool ValidateData()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		if (((DataTable)((UltraGridBase)ULGData).DataSource).Select(" Qty =0 ").Length != 0)
		{
			GlobalVariables.QuestionMB.Show("يوجد أصناف كميتها بصفر هل تريد الحذف؟ ", "There Are Items Quantity Equal Zero Are you Sure To Delete?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				ULGData.BeforeRowsDeleted -= new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
				for (int num = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1; num >= 0; num--)
				{
					if (((UltraGridBase)ULGData).Rows[num].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[num].Cells["Qty"].Value.ToString()) == 0m)
					{
						((UltraGridBase)ULGData).Rows[num].Delete(false);
					}
				}
				((UltraGridBase)ULGData).UpdateData();
				ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
			}
		}
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			return false;
		}
		if (!FiscalYear.ChkForConfirmedFiscalYear(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
			return false;
		}
		if (FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
		{
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboCompany.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المورد" : "Please Select Supplier");
			((TextEditorControlBase)cboCompany).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("WH_WareHouseIssue", "WareHouseIssueNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["WareHouseIssueNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = WareHouseIssue.GetCodeByBranchID(GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = WareHouseIssue.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtInvoiceNo).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtAWBNo).Text, dtpAWBDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtManifestNo).Text, dtpManifestDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCompany).Value.ToString(), ((Control)(object)txtSupplier).Text, ((Control)(object)txtSupplierStore).Text, ((Control)(object)txtGRN).Text, ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["WareHouseIssueID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["WareHouseIssueDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			WareHouseIssueDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = WareHouseIssue.Insert_Update(drMaster["WareHouseIssueID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtInvoiceNo).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtAWBNo).Text, dtpAWBDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtManifestNo).Text, dtpManifestDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboCompany).Value.ToString(), ((Control)(object)txtSupplier).Text, ((Control)(object)txtSupplierStore).Text, ((Control)(object)txtGRN).Text, ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["WareHouseIssueID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				((UltraGridBase)ULGData).Rows[i].Cells["VoucherDate"].Value = dtpDate.DateTime;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["WareHouseIssueDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("WH_WareHouseIssueDetails", "WareHouseIssueID", drMaster["WareHouseIssueID"].ToString(), "WareHouseIssueDetailID", text);
			WareHouseIssueDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			WareHouseIssue.DeleteVirtual(drMaster["WareHouseIssueID"].ToString(), GlobalVariables.UserID);
			WareHouseIssueDetails.DeleteVirtualByWareHouseIssueID(drMaster["WareHouseIssueID"].ToString(), GlobalVariables.UserID);
			string text = MessageLog.SelectByVoucherIDAndTransType(drMaster["WareHouseIssueID"].ToString(), "GRN", "GRN", GlobalVariables.IsArabic ? "1" : "0");
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.WareHouseIssueSearchReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["WareHouseIssueID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtItems = Items.FillComboWH(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		DataView dataView = new DataView(dtStores);
		dataView.RowFilter = " Locked =0  And BranchID=" + GlobalVariables.CurrentBranchID;
		DataTable dataTable = dataView.ToTable();
		vlStores.ValueListItems.Clear();
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			vlStores.ValueListItems.Add(dataTable.Rows[j]["StoreID"], dataTable.Rows[j]["StoreName"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int k = 0; k < dtUnits.Rows.Count; k++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[k]["UnitID"], dtUnits.Rows[k]["UnitName"].ToString());
		}
		dtCompany = BusinessLayer.WareHouse.Company.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCompany, dtCompany, "CompanyID", "CompanyName");
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void btnUpdateClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanUpdate)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Update This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(dtStores.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value.ToString())[0]["Locked"].ToString()))
			{
				GlobalVariables.InformationMB.Show(" لايمكن تعديل هذه الحركة لوجود المخزن \n" + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " مغلق ", "Cannot Update This Transaction Because Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Text + " Locked ");
				return;
			}
		}
		Updating = true;
		SetControls(NavMode: false);
	}

	private ValueList getUnitsValueList(int UnitTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtUnits.Select("UnitTypeID=" + UnitTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["UnitID"].ToString(), array[i]["UnitName"].ToString());
		}
		return val;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "StoreID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Percentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PareCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MaxQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Location" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
			{
				if (decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["MaxQty"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show("الكميه اكبر من الحد الأقصي المسموح", "Quantity Shouldn't be Greater than Max Quantity");
					ULGData.ActiveCell.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaxQty"].Value;
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItems.Select(" ItemID= " + e.Cell.Value)[0]["UnitID"].ToString();
			int num = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dtItems.Select(" ItemID= " + e.Cell.Value)[0]["UnitID"].ToString();
			}
			else
			{
				e.Cell.Row.Cells["UnitID"].ValueList = null;
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
	}

	private void btnGetAvailable_Click(object sender, EventArgs e)
	{
		if (cboCompany.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المورد" : "Please Select Supplier");
			((TextEditorControlBase)cboCompany).Focus();
		}
		else
		{
			dtDetails = WareHouseIssueDetails.SelectAvailable("-1", ((Control)(object)txtAWBNo).Text, ((Control)(object)txtManifestNo).Text, ((TextEditorControlBase)cboCompany).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Expected O, but got Unknown
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.WareHouse.Transactions.frmWareHouseIssue));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblInvoiceNo = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboCompany = new UltraComboEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblCompany = new UltraLabel();
		this.dtpAWBDate = new UltraDateTimeEditor();
		this.lblAWBDate = new UltraLabel();
		this.dtpManifestDate = new UltraDateTimeEditor();
		this.lblManifestDate = new UltraLabel();
		this.txtAWBNo = new UltraTextEditor();
		this.txtManifestNo = new UltraTextEditor();
		this.lblManifestNo = new UltraLabel();
		this.lblAWBNo = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.txtInvoiceNo = new UltraTextEditor();
		this.lblSupplier = new UltraLabel();
		this.txtSupplier = new UltraTextEditor();
		this.lblGRN = new UltraLabel();
		this.txtGRN = new UltraTextEditor();
		this.lblSupplierStore = new UltraLabel();
		this.txtSupplierStore = new UltraTextEditor();
		this.btnGetAvailable = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCompany).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAWBDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpManifestDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAWBNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtManifestNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplier).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGRN).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplierStore).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
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
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblInvoiceNo, "lblInvoiceNo");
		this.lblInvoiceNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvoiceNo).Name = "lblInvoiceNo";
		((ControlBase)this.lblInvoiceNo).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.DateTime = new System.DateTime(2014, 3, 3, 0, 0, 0, 0);
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.Value = new System.DateTime(2014, 3, 3, 0, 0, 0, 0);
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboCompany, "cboCompany");
		((TextEditorControlBase)this.cboCompany).AlwaysInEditMode = true;
		this.cboCompany.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCompany).Name = "cboCompany";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblCompany, "lblCompany");
		this.lblCompany.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompany).Name = "lblCompany";
		((ControlBase)this.lblCompany).WrapText = false;
		resources.ApplyResources(this.dtpAWBDate, "dtpAWBDate");
		((UltraWinEditorMaskedControlBase)this.dtpAWBDate).AlwaysInEditMode = true;
		this.dtpAWBDate.DateTime = new System.DateTime(2014, 3, 3, 0, 0, 0, 0);
		this.dtpAWBDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpAWBDate).Name = "dtpAWBDate";
		this.dtpAWBDate.Value = new System.DateTime(2014, 3, 3, 0, 0, 0, 0);
		this.dtpAWBDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblAWBDate, "lblAWBDate");
		this.lblAWBDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAWBDate).Name = "lblAWBDate";
		((ControlBase)this.lblAWBDate).WrapText = false;
		resources.ApplyResources(this.dtpManifestDate, "dtpManifestDate");
		((UltraWinEditorMaskedControlBase)this.dtpManifestDate).AlwaysInEditMode = true;
		this.dtpManifestDate.DateTime = new System.DateTime(2014, 3, 3, 0, 0, 0, 0);
		this.dtpManifestDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpManifestDate).Name = "dtpManifestDate";
		this.dtpManifestDate.Value = new System.DateTime(2014, 3, 3, 0, 0, 0, 0);
		this.dtpManifestDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.lblManifestDate, "lblManifestDate");
		this.lblManifestDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManifestDate).Name = "lblManifestDate";
		((ControlBase)this.lblManifestDate).WrapText = false;
		resources.ApplyResources(this.txtAWBNo, "txtAWBNo");
		((System.Windows.Forms.Control)(object)this.txtAWBNo).Name = "txtAWBNo";
		resources.ApplyResources(this.txtManifestNo, "txtManifestNo");
		((System.Windows.Forms.Control)(object)this.txtManifestNo).Name = "txtManifestNo";
		resources.ApplyResources(this.lblManifestNo, "lblManifestNo");
		this.lblManifestNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblManifestNo).Name = "lblManifestNo";
		((ControlBase)this.lblManifestNo).WrapText = false;
		resources.ApplyResources(this.lblAWBNo, "lblAWBNo");
		this.lblAWBNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAWBNo).Name = "lblAWBNo";
		((ControlBase)this.lblAWBNo).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtInvoiceNo, "txtInvoiceNo");
		((System.Windows.Forms.Control)(object)this.txtInvoiceNo).Name = "txtInvoiceNo";
		resources.ApplyResources(this.lblSupplier, "lblSupplier");
		this.lblSupplier.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplier).Name = "lblSupplier";
		((ControlBase)this.lblSupplier).WrapText = false;
		resources.ApplyResources(this.txtSupplier, "txtSupplier");
		((System.Windows.Forms.Control)(object)this.txtSupplier).Name = "txtSupplier";
		resources.ApplyResources(this.lblGRN, "lblGRN");
		this.lblGRN.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGRN).Name = "lblGRN";
		((ControlBase)this.lblGRN).WrapText = false;
		resources.ApplyResources(this.txtGRN, "txtGRN");
		((System.Windows.Forms.Control)(object)this.txtGRN).Name = "txtGRN";
		resources.ApplyResources(this.lblSupplierStore, "lblSupplierStore");
		this.lblSupplierStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSupplierStore).Name = "lblSupplierStore";
		((ControlBase)this.lblSupplierStore).WrapText = false;
		resources.ApplyResources(this.txtSupplierStore, "txtSupplierStore");
		((System.Windows.Forms.Control)(object)this.txtSupplierStore).Name = "txtSupplierStore";
		resources.ApplyResources(this.btnGetAvailable, "btnGetAvailable");
		((System.Windows.Forms.Control)(object)this.btnGetAvailable).Name = "btnGetAvailable";
		((System.Windows.Forms.Control)(object)this.btnGetAvailable).Click += new System.EventHandler(btnGetAvailable_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGetAvailable);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSupplierStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGRN);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtManifestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAWBNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplierStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGRN);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSupplier);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAWBNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManifestNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompany);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCompany);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblManifestDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAWBDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvoiceNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpManifestDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpAWBDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmWareHouseIssue";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpAWBDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpManifestDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAWBDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManifestDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCompany, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompany, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblManifestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAWBNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGRN, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSupplierStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAWBNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvoiceNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtManifestNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSupplier, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGRN, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSupplierStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGetAvailable, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCompany).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpAWBDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpManifestDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAWBNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtManifestNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvoiceNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplier).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGRN).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplierStore).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
