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
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.CustomsClearence.Transactions;

public class frmCertificatesOfOrigin : frmHeaderManyDetails
{
	private DataTable dtOperations;

	private DataTable dtReports;

	private DataTable dtBillsOfLading;

	private bool CanOpenLetter = false;

	private ValueList vlOperationItems = new ValueList();

	private string OperationID = "0";

	private IContainer components = null;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	public UltraButton btnOperationNoSearch;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtCertificateNumber;

	private UltraLabel lblCertificateNumber;

	private UltraComboEditor cboBillOfLading;

	private UltraLabel lblBillOfLading;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGReports;

	private UltraCheckEditor chkShowBLNumber;

	public frmCertificatesOfOrigin()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CST_CertificatesOfOrigin";
		IDCol = "CertificateOfOriginID";
		NoCol = "CertificateOfOriginNo";
		DateCol = "CertificateOfOriginDate";
	}

	public frmCertificatesOfOrigin(string OPERATIONID)
		: this()
	{
		OperationID = OPERATIONID;
	}

	public override void PrepareData()
	{
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtBillsOfLading = BillsOfLading.FillCombo(GlobalVariables.BranchIDs);
		dtDetails = CertificatesOfOriginItems.SelectByCertificateOfOriginID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		((UltraGridBase)ULGReports).DataSource = dtReports;
		InitGridReports();
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = CertificatesOfOrigin.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		if (cboOperationNo.SelectedIndex > -1)
		{
			DataRow dataRow = dtOperations.Select("OperationID = " + ((TextEditorControlBase)cboOperationNo).Value.ToString())[0];
			dtDetails = CertificatesOfOriginItems.FillByOperationID(((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			DataView dataView = new DataView(dtBillsOfLading);
			dataView.RowFilter = "OperationID = " + ((((TextEditorControlBase)cboOperationNo).Value == null) ? "-1" : ((TextEditorControlBase)cboOperationNo).Value.ToString());
			cboBillOfLading.DataSource = dataView;
			cboBillOfLading.DisplayMember = "BillOfLadingNumber";
			cboBillOfLading.ValueMember = "BillOfLadingID";
			cboBillOfLading.SelectedIndex = ((dataView.Count <= 0) ? (-1) : 0);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateOfOriginItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الحاوية" : "Container Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم فاتورة العميل" : "Client Invoice No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
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
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOperationNoSearch).Visible = false;
			((EditorButtonControlBase)cboOperationNo).ReadOnly = true;
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
		if (RowID == "")
		{
			drMaster = null;
			DisplayData();
			return;
		}
		DataTable dataTable = CertificatesOfOrigin.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["CertificateOfOriginNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["CertificateOfOriginDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboBillOfLading).Value = drMaster["BillOfLadingID"];
			((Control)(object)txtCertificateNumber).Text = drMaster["CertificateNumber"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = CertificatesOfOriginItems.SelectByCertificateOfOriginID(drMaster["CertificateOfOriginID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((Control)(object)btnCopyTo).Visible = false;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkShowBLNumber).Enabled = !NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode || Updating || OperationID != "0";
		((EditorButtonControlBase)cboBillOfLading).ReadOnly = NavMode || Updating;
		((Control)(object)btnOperationNoSearch).Visible = Adding && OperationID == "0";
		((UltraTabControlBase)UTCDetails).Tabs[1].Visible = NavMode;
		((Control)(object)btnPrint).Visible = false;
		if (Adding)
		{
			DataView dataView = new DataView(dtOperations);
			dataView.RowFilter = "Approved = 0 and BranchID =" + GlobalVariables.CurrentBranchID;
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
		((Control)(object)txtCode).Text = (Adding ? CertificatesOfOrigin.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((UltraToggleEditorBase)chkShowBLNumber).Checked = true;
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		cboOperationNo.SelectedIndex = -1;
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		cboBillOfLading.SelectedIndex = -1;
		((TextEditorControlBase)txtCertificateNumber).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		if (OperationID != "0")
		{
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CST_CertificatesOfOrigin", "CertificateOfOriginNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["CertificateOfOriginNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = CertificatesOfOrigin.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم هذه الشهادة متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Voucher Number Already Exists It Will Be Saved With No. : " + codeByBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الخدمة  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginID"];
				((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginID"].DroppedDown = true;
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
			int num = CertificatesOfOrigin.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), (cboBillOfLading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBillOfLading).Value.ToString(), ((UltraToggleEditorBase)chkShowBLNumber).Checked ? "1" : "0", (((Control)(object)txtCertificateNumber).Text == "") ? "Null" : ((Control)(object)txtCertificateNumber).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginItemID"].Value = -1;
					((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				CertificatesOfOriginItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			ItemsTransactions.ManageInThread();
		}
		catch (Exception)
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
			int num = CertificatesOfOrigin.Insert_Update(drMaster["CertificateOfOriginID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), (cboBillOfLading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBillOfLading).Value.ToString(), ((UltraToggleEditorBase)chkShowBLNumber).Checked ? "1" : "0", (((Control)(object)txtCertificateNumber).Text == "") ? "Null" : ((Control)(object)txtCertificateNumber).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["CertificateOfOriginItemID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("CST_CertificatesOfOriginItems", "CertificateOfOriginID", drMaster["CertificateOfOriginID"].ToString(), "CertificateOfOriginItemID", text);
				CertificatesOfOriginItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			CertificatesOfOrigin.DeleteVirtual(drMaster["CertificateOfOriginID"].ToString(), GlobalVariables.UserID);
			CertificatesOfOriginItems.DeleteVirtualByCertificateOfOriginID(drMaster["CertificateOfOriginID"].ToString(), GlobalVariables.UserID);
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
		string val = "";
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CST_CertificatesOfOrigin_A.rpt" : "Rep_CST_CertificatesOfOrigin_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@CertificateOfOriginItemIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnRefreshDataClick()
	{
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtBillsOfLading = BillsOfLading.FillCombo(GlobalVariables.BranchIDs);
	}

	public void BillsFilter()
	{
		DataView dataView = new DataView(dtBillsOfLading);
		dataView.RowFilter = "OperationID = " + ((((TextEditorControlBase)cboOperationNo).Value == null) ? "-1" : ((TextEditorControlBase)cboOperationNo).Value.ToString());
		cboBillOfLading.DataSource = dataView;
		cboBillOfLading.DisplayMember = "BillOfLadingNumber";
		cboBillOfLading.ValueMember = "BillOfLadingID";
		if (dataView.Count > 0)
		{
			cboBillOfLading.SelectedIndex = 0;
		}
		else
		{
			cboBillOfLading.SelectedIndex = -1;
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

	private void frmCertificatesOfOrigin_Load(object sender, EventArgs e)
	{
	}

	private void cboBillOfLading_Click(object sender, EventArgs e)
	{
	}

	private void cboBillOfLading_ValueChanged(object sender, EventArgs e)
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTCertificatesOfOriginReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["CertificateOfOriginID"].ToString();
			FillData();
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void UTCDetails_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void ULGReports_ClickCellButton(object sender, CellEventArgs e)
	{
		if (drMaster != null && e.Cell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "Print" && ((UltraGridBase)ULGReports).ActiveRow != null && !CanOpenLetter)
		{
			CanOpenLetter = true;
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_A"].ToString() : dtReports.Select("ReportID = " + ((UltraGridBase)ULGReports).ActiveRow.Cells["ReportID"].Value.ToString())[0]["Rep_E"].ToString()));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@CertificateOfOriginIDs", string.Concat(",", drMaster["CertificateOfOriginID"], ","));
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			CanOpenLetter = false;
		}
	}

	public void InitGridReports()
	{
		GlobalFunctions.PrepareGrid(ULGReports);
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Width = (int)((double)((Control)(object)ULGReports).Width * 0.9) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم التقرير" : "Report Name");
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["ReportName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns.Insert(0, "Print");
		}
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGReports).Width * 0.1);
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGReports).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGReports).Rows).Count; i++)
		{
			((UltraGridBase)ULGReports).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Expected O, but got Unknown
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0818: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Transactions.frmCertificatesOfOrigin));
		UltraTab val = new UltraTab();
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
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGReports = new UltraGrid();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.btnOperationNoSearch = new UltraButton();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtCertificateNumber = new UltraTextEditor();
		this.lblCertificateNumber = new UltraLabel();
		this.cboBillOfLading = new UltraComboEditor();
		this.lblBillOfLading = new UltraLabel();
		this.chkShowBLNumber = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBillOfLading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkShowBLNumber).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabOrientation = (TabOrientation)1;
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.ULGData, "ULGData");
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(UTCDetails_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGReports);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		((UltraGridBase)this.ULGReports).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGReports).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val10).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val10).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val12).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGReports).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGReports).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGReports).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGReports, "ULGReports");
		((System.Windows.Forms.Control)(object)this.ULGReports).Name = "ULGReports";
		((UltraControlBase)this.ULGReports).UseFlatMode = (DefaultableBoolean)1;
		this.ULGReports.ClickCellButton += new CellEventHandler(ULGReports_ClickCellButton);
		this.lblOperationsNo.AutoEllipsis = false;
		resources.ApplyResources(this.lblOperationsNo, "lblOperationsNo");
		((System.Windows.Forms.Control)(object)this.lblOperationsNo).Name = "lblOperationsNo";
		((ControlBase)this.lblOperationsNo).WrapText = false;
		this.cboOperationNo.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboOperationNo, "cboOperationNo");
		((System.Windows.Forms.Control)(object)this.cboOperationNo).Name = "cboOperationNo";
		((TextEditorControlBase)this.cboOperationNo).ValueChanged += new System.EventHandler(cboOperationNo_ValueChanged);
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnOperationNoSearch).Appearance = (AppearanceBase)(object)val14;
		resources.ApplyResources(this.btnOperationNoSearch, "btnOperationNoSearch");
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Name = "btnOperationNoSearch";
		((System.Windows.Forms.Control)(object)this.btnOperationNoSearch).Click += new System.EventHandler(btnOperationNoSearch_Click);
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtCertificateNumber, "txtCertificateNumber");
		((System.Windows.Forms.Control)(object)this.txtCertificateNumber).Name = "txtCertificateNumber";
		this.lblCertificateNumber.AutoEllipsis = false;
		resources.ApplyResources(this.lblCertificateNumber, "lblCertificateNumber");
		((System.Windows.Forms.Control)(object)this.lblCertificateNumber).Name = "lblCertificateNumber";
		((ControlBase)this.lblCertificateNumber).WrapText = false;
		this.cboBillOfLading.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBillOfLading, "cboBillOfLading");
		((System.Windows.Forms.Control)(object)this.cboBillOfLading).Name = "cboBillOfLading";
		((TextEditorControlBase)this.cboBillOfLading).ValueChanged += new System.EventHandler(cboBillOfLading_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboBillOfLading).Click += new System.EventHandler(cboBillOfLading_Click);
		this.lblBillOfLading.AutoEllipsis = false;
		resources.ApplyResources(this.lblBillOfLading, "lblBillOfLading");
		((System.Windows.Forms.Control)(object)this.lblBillOfLading).Name = "lblBillOfLading";
		((System.Windows.Forms.Control)(object)this.lblBillOfLading).Tag = "";
		((ControlBase)this.lblBillOfLading).WrapText = false;
		resources.ApplyResources(this.chkShowBLNumber, "chkShowBLNumber");
		((UltraToggleEditorBase)this.chkShowBLNumber).Checked = true;
		((UltraToggleEditorBase)this.chkShowBLNumber).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkShowBLNumber).Name = "chkShowBLNumber";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkShowBLNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCertificateNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCertificateNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBillOfLading);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBillOfLading);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmCertificatesOfOrigin";
		base.Load += new System.EventHandler(frmCertificatesOfOrigin_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOperationNoSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBillOfLading, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBillOfLading, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCertificateNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCertificateNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkShowBLNumber, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGReports).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCertificateNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBillOfLading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkShowBLNumber).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
