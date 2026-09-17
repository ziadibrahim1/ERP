using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.Transactions;

public class frmBillsOfLading : frmHeaderDetails
{
	private DataTable dtOperations;

	private DataTable dtReports;

	private DataTable dtConsignees;

	private ValueList vlOperationItems = new ValueList();

	private string OperationID = "0";

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	public UltraButton btnOperationNoSearch;

	private UltraLabel lblConsigneeName;

	private UltraTextEditor txtConsigneeName;

	private UltraTextEditor txtBillOfLadingNo;

	private UltraTextEditor txtNotify;

	private UltraTextEditor txtConsigneeVATNo;

	private UltraLabel lblNotify;

	private UltraLabel lblConsigneeVATNo;

	private UltraLabel lblBillOfLadingNo;

	private UltraTextEditor txtTotalNetWeight;

	private UltraTextEditor txtTotalGrossWeight;

	private UltraTextEditor txtTotalQty;

	private UltraLabel lblTotalNetWeight;

	private UltraLabel lblTotalGrossWeight;

	private UltraLabel lblTotalQty;

	public UltraButton btnConsigneesSearch;

	public frmBillsOfLading()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CST_BillsOfLading";
		IDCol = "BillOfLadingID";
		NoCol = "BillOfLadingNo";
		DateCol = "BillOfLadingDate";
	}

	public frmBillsOfLading(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmBillsOfLading(string _OperationID)
		: this()
	{
		OperationID = _OperationID;
	}

	public override void PrepareData()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtConsignees = Consignees.Fill("-1", GlobalVariables.BranchIDs, IsFromServer: false);
		dtDetails = BillsOfLadingItems.SelectByBillOfLadingID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BillOfLadingItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوية" : "Container Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم فاتورة العميل" : "Client Invoice No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الكلي" : "Gross Weight");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GrossWeight"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Header).Caption = (GlobalVariables.IsArabic ? "الوزن الصافي" : "Net Weight");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetWeight"].Hidden = false;
	}

	public override void FillData()
	{
		if (OperationID != "0")
		{
			drMaster = null;
			btnAddClick();
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
			return;
		}
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = BillsOfLading.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		if (dataTable.Rows.Count > 0)
		{
			drMaster = dataTable.Rows[0];
		}
		else
		{
			drMaster = null;
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["BillOfLadingNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["BillOfLadingDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
			((Control)(object)txtBillOfLadingNo).Text = drMaster["BillOfLadingNumber"].ToString();
			((Control)(object)txtConsigneeName).Text = drMaster["ConsigneeName"].ToString();
			((Control)(object)txtConsigneeVATNo).Text = drMaster["ConsigneeVATNo"].ToString();
			((Control)(object)txtNotify).Text = drMaster["NotifyName"].ToString();
			dtDetails = BillsOfLadingItems.SelectByBillOfLadingID(drMaster["BillOfLadingID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateTotals();
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
		((Control)(object)btnCopyTo).Visible = false;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtConsigneeVATNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtConsigneeName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotify).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBillOfLadingNo).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode || Updating || OperationID != "0";
		((Control)(object)btnOperationNoSearch).Visible = Adding && OperationID == "0";
		((Control)(object)btnConsigneesSearch).Visible = !NavMode;
		if (Adding)
		{
			DataView dataView = new DataView(dtOperations);
			dataView.RowFilter = " Approved = 0 and BranchID =" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView.ToTable();
			int num = 0;
			if (cboOperationNo.SelectedIndex > -1)
			{
				num = int.Parse(((TextEditorControlBase)cboOperationNo).Value.ToString());
			}
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			GlobalFunctions.FillCombo(cboOperationNo, dt, "OperationID", "OperationNo");
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
			if (num != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num;
			}
		}
		else
		{
			((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
			int num2 = 0;
			if (cboOperationNo.SelectedIndex > -1)
			{
				num2 = int.Parse(((TextEditorControlBase)cboOperationNo).Value.ToString());
			}
			GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
			if (num2 != 0)
			{
				((TextEditorControlBase)cboOperationNo).Value = num2;
			}
			((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDate.ValueChanged += dtpDate_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? BillsOfLading.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		cboOperationNo.SelectedIndex = -1;
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		if (OperationID != "0")
		{
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
		((TextEditorControlBase)txtBillOfLadingNo).Clear();
		((TextEditorControlBase)txtConsigneeName).Clear();
		((TextEditorControlBase)txtConsigneeVATNo).Clear();
		((TextEditorControlBase)txtNotify).Clear();
		((Control)(object)txtTotalGrossWeight).Text = "0";
		((Control)(object)txtTotalNetWeight).Text = "0";
		((Control)(object)txtTotalQty).Text = "0";
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الأذن" : "Please Enter The Voucher Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
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
		if (cboOperationNo.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العملية " : "Please Select Operation");
			((TextEditorControlBase)cboOperationNo).Focus();
			cboOperationNo.DropDown();
			return false;
		}
		if (((Control)(object)txtBillOfLadingNo).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال  رقم بوليصة الشحن" : "Please Enter Bill Of Lading No.");
			((TextEditorControlBase)txtBillOfLadingNo).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CST_BillsOfLading", "BillOfLadingNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BillOfLadingNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = BillsOfLading.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["OperationItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["OperationItemID"];
				((UltraGridBase)ULGData).Rows[i].Cells["OperationItemID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BillsOfLading.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboOperationNo).Value.ToString(), (((Control)(object)txtBillOfLadingNo).Text == "") ? "Null" : ((Control)(object)txtBillOfLadingNo).Text, (((Control)(object)txtConsigneeName).Text == "") ? "Null" : ((Control)(object)txtConsigneeName).Text, (((Control)(object)txtConsigneeVATNo).Text == "") ? "Null" : ((Control)(object)txtConsigneeVATNo).Text, (((Control)(object)txtNotify).Text == "") ? "Null" : ((Control)(object)txtNotify).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["BillOfLadingItemID"].Value = -1;
					((UltraGridBase)ULGData).Rows[i].Cells["BillOfLadingID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				BillsOfLadingItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
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
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = BillsOfLading.Insert_Update(drMaster["BillOfLadingID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboOperationNo).Value.ToString(), (((Control)(object)txtBillOfLadingNo).Text == "") ? "Null" : ((Control)(object)txtBillOfLadingNo).Text, (((Control)(object)txtConsigneeName).Text == "") ? "Null" : ((Control)(object)txtConsigneeName).Text, (((Control)(object)txtConsigneeVATNo).Text == "") ? "Null" : ((Control)(object)txtConsigneeVATNo).Text, (((Control)(object)txtNotify).Text == "") ? "Null" : ((Control)(object)txtNotify).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["BillOfLadingID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BillOfLadingItemID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("CST_BillsOfLadingItems", "BillOfLadingID", drMaster["BillOfLadingID"].ToString(), "BillOfLadingItemID", text);
				BillsOfLadingItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
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
			BillsOfLading.DeleteVirtual(drMaster["BillOfLadingID"].ToString(), GlobalVariables.UserID);
			BillsOfLadingItems.DeleteVirtualByBillOfLadingID(drMaster["BillOfLadingID"].ToString(), GlobalVariables.UserID);
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
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CST_BillsOfLading.rpt" : "Rep_CST_BillsOfLading.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@BillOfLadingIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTBillsOfLadingReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BillOfLadingID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtConsignees = Consignees.Fill("-1", GlobalVariables.BranchIDs, IsFromServer: false);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateTotals();
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = BillsOfLading.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnOperationNoSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTOperationsSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboOperationNo).Value = num;
		}
	}

	private void lblBillOfLadingNo_Click(object sender, EventArgs e)
	{
	}

	private void CalculateTotals()
	{
		((Control)(object)txtTotalQty).Text = "0";
		((Control)(object)txtTotalNetWeight).Text = "0";
		((Control)(object)txtTotalGrossWeight).Text = "0";
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataView dataView = new DataView((DataTable)((UltraGridBase)ULGData).DataSource);
			DataTable dataTable = dataView.ToTable();
			if (dataTable.Rows.Count > 0)
			{
				object obj = dataTable.Compute(" Sum(Qty) ", "");
				object obj2 = dataTable.Compute(" Sum(GrossWeight) ", "");
				object obj3 = dataTable.Compute(" Sum(NetWeight) ", "");
				((Control)(object)txtTotalQty).Text = ((obj != DBNull.Value) ? decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate) : "0");
				((Control)(object)txtTotalNetWeight).Text = ((obj3 != DBNull.Value) ? decimal.Parse(obj3.ToString()).ToString(GlobalVariables.txtDecimalFormate) : "0");
				((Control)(object)txtTotalGrossWeight).Text = ((obj2 != DBNull.Value) ? decimal.Parse(obj2.ToString()).ToString(GlobalVariables.txtDecimalFormate) : "0");
			}
		}
	}

	private void btnConsigneesSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTConsigneesSearch(IsFromServer: true);
		if (num != 0)
		{
			((Control)(object)txtConsigneeName).Text = dtConsignees.Select("ConsigneeID = " + num)[0]["ConsigneeNameEn"].ToString().Trim();
			UltraTextEditor obj = txtConsigneeName;
			((Control)(object)obj).Text = ((Control)(object)obj).Text + "\n" + dtConsignees.Select("ConsigneeID = " + num)[0]["Address"].ToString().Trim();
			UltraTextEditor obj2 = txtConsigneeName;
			((Control)(object)obj2).Text = ((Control)(object)obj2).Text + "\n" + dtConsignees.Select("ConsigneeID = " + num)[0]["CityNameEn"].ToString().Trim();
			UltraTextEditor obj3 = txtConsigneeName;
			((Control)(object)obj3).Text = ((Control)(object)obj3).Text + " - " + dtConsignees.Select("ConsigneeID = " + num)[0]["CountryNameEn"].ToString().Trim();
			((Control)(object)txtConsigneeVATNo).Text = dtConsignees.Select("ConsigneeID = " + num)[0]["VATNumber"].ToString().Trim();
		}
	}

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		if (cboOperationNo.SelectedIndex > -1)
		{
			DataRow dataRow = dtOperations.Select("OperationID = " + ((TextEditorControlBase)cboOperationNo).Value.ToString())[0];
			dtDetails = BillsOfLadingItems.FillByOperationID(((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
		CalculateTotals();
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
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
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		if (CertificatesOfOrigin.SelectByBillOfLadingID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه البوليصة لوجود شهادات منشأ على هذه البوليصة ", "Cannot Delete This Transaction Because There Are Certificates Of Origins On This Bill Of Lading");
			return;
		}
		if (PhytosanitaryCertificates.SelectByBillOfLadingID(RowID, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه البوليصة لوجود شهادات نباتيه على هذه البوليصة ", "Cannot Delete This Transaction Because There Are Phytosanitary Certificates On This Bill Of Lading ");
			return;
		}
		GlobalVariables.QuestionMB.Show("سوف يتم حذف الإذن وحذف القيد هل تريد حذف هذه البيانات؟", "This Voucher And its JV Will Be Deleted Are you Sure You Want To Delete This Information ?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DataSaved = true;
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
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
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Transactions.frmBillsOfLading));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.btnOperationNoSearch = new UltraButton();
		this.lblConsigneeName = new UltraLabel();
		this.txtConsigneeName = new UltraTextEditor();
		this.txtBillOfLadingNo = new UltraTextEditor();
		this.txtNotify = new UltraTextEditor();
		this.txtConsigneeVATNo = new UltraTextEditor();
		this.lblNotify = new UltraLabel();
		this.lblConsigneeVATNo = new UltraLabel();
		this.lblBillOfLadingNo = new UltraLabel();
		this.txtTotalNetWeight = new UltraTextEditor();
		this.txtTotalGrossWeight = new UltraTextEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.lblTotalNetWeight = new UltraLabel();
		this.lblTotalGrossWeight = new UltraLabel();
		this.lblTotalQty = new UltraLabel();
		this.btnConsigneesSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsigneeName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBillOfLadingNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotify).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsigneeVATNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalNetWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalGrossWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblRight, "lblRight");
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		this.lblOperationsNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblOperationsNo, "lblOperationsNo");
		((System.Windows.Forms.Control)(object)this.lblOperationsNo).Name = "lblOperationsNo";
		((ControlBase)this.lblOperationsNo).WrapText = false;
		this.cboOperationNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboOperationNo, "cboOperationNo");
		((System.Windows.Forms.Control)(object)this.cboOperationNo).Name = "cboOperationNo";
		((TextEditorControlBase)this.cboOperationNo).ValueChanged += new System.EventHandler(cboOperationNo_ValueChanged);
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnOperationNoSearch).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.btnOperationNoSearch, "btnOperationNoSearch");
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Name = "btnOperationNoSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Click += new System.EventHandler(btnOperationNoSearch_Click);
		this.lblConsigneeName.AutoEllipsis = false;
		resources.ApplyResources(this.lblConsigneeName, "lblConsigneeName");
		((System.Windows.Forms.Control)(object)this.lblConsigneeName).Name = "lblConsigneeName";
		((ControlBase)this.lblConsigneeName).WrapText = false;
		resources.ApplyResources(this.txtConsigneeName, "txtConsigneeName");
		((System.Windows.Forms.Control)(object)this.txtConsigneeName).Name = "txtConsigneeName";
		resources.ApplyResources(this.txtBillOfLadingNo, "txtBillOfLadingNo");
		((System.Windows.Forms.Control)(object)this.txtBillOfLadingNo).Name = "txtBillOfLadingNo";
		resources.ApplyResources(this.txtNotify, "txtNotify");
		((System.Windows.Forms.Control)(object)this.txtNotify).Name = "txtNotify";
		resources.ApplyResources(this.txtConsigneeVATNo, "txtConsigneeVATNo");
		((System.Windows.Forms.Control)(object)this.txtConsigneeVATNo).Name = "txtConsigneeVATNo";
		this.lblNotify.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotify, "lblNotify");
		((System.Windows.Forms.Control)(object)this.lblNotify).Name = "lblNotify";
		((ControlBase)this.lblNotify).WrapText = false;
		this.lblConsigneeVATNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblConsigneeVATNo, "lblConsigneeVATNo");
		((System.Windows.Forms.Control)(object)this.lblConsigneeVATNo).Name = "lblConsigneeVATNo";
		((ControlBase)this.lblConsigneeVATNo).WrapText = false;
		this.lblBillOfLadingNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblBillOfLadingNo, "lblBillOfLadingNo");
		((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo).Name = "lblBillOfLadingNo";
		((ControlBase)this.lblBillOfLadingNo).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo).Click += new System.EventHandler(lblBillOfLadingNo_Click);
		resources.ApplyResources(this.txtTotalNetWeight, "txtTotalNetWeight");
		((System.Windows.Forms.Control)(object)this.txtTotalNetWeight).Name = "txtTotalNetWeight";
		((EditorButtonControlBase)this.txtTotalNetWeight).ReadOnly = true;
		resources.ApplyResources(this.txtTotalGrossWeight, "txtTotalGrossWeight");
		((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight).Name = "txtTotalGrossWeight";
		((EditorButtonControlBase)this.txtTotalGrossWeight).ReadOnly = true;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		resources.ApplyResources(this.lblTotalNetWeight, "lblTotalNetWeight");
		this.lblTotalNetWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalNetWeight).Name = "lblTotalNetWeight";
		((ControlBase)this.lblTotalNetWeight).WrapText = false;
		resources.ApplyResources(this.lblTotalGrossWeight, "lblTotalGrossWeight");
		this.lblTotalGrossWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight).Name = "lblTotalGrossWeight";
		((ControlBase)this.lblTotalGrossWeight).WrapText = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		this.lblTotalQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnConsigneesSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnConsigneesSearch, "btnConsigneesSearch");
		((System.Windows.Forms.Control)(object)this.btnConsigneesSearch).Name = "btnConsigneesSearch";
		((System.Windows.Forms.Control)(object)this.btnConsigneesSearch).Click += new System.EventHandler(btnConsigneesSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnConsigneesSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalNetWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalNetWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsigneeName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsigneeName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBillOfLadingNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotify);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsigneeVATNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotify);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsigneeVATNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmBillsOfLading";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBillOfLadingNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsigneeVATNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotify, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsigneeVATNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotify, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBillOfLadingNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsigneeName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsigneeName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalGrossWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalNetWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalGrossWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalNetWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnConsigneesSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsigneeName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBillOfLadingNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotify).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsigneeVATNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalNetWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalGrossWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
