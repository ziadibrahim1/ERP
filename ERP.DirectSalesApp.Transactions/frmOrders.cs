using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.DirectSalesApp;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.DirectSalesApp.Transactions;

public class frmOrders : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtSalesMan;

	private DataTable dtLines;

	private DataTable dtClients;

	private ValueList vlClients = new ValueList();

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnSalesManSearch;

	private UltraLabel lblSalesMan;

	private UltraComboEditor cboSalesMan;

	public UltraButton btnLineSearch;

	private UltraLabel lblLine;

	private UltraComboEditor cboLine;

	private UltraCheckEditor chkClosed;

	private UltraButton btnReloadClients;

	public frmOrders()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "LnsA_Orders";
		IDCol = "OrderID";
		NoCol = "OrderNo";
		DateCol = "OrderDate";
	}

	public frmOrders(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtSalesMan = SubAccounts.SelectSalesMan_LnsA(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtLines = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlClients.ValueListItems.Clear();
		for (int i = 0; i < dtClients.Rows.Count; i++)
		{
			vlClients.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["SubAccountName"].ToString());
		}
		dtDetails = OrdersDetails.SelectByOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OrderDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم طلب العميل" : "Client Order No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientOrderNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientSubAccountID"].ValueList = (IValueList)(object)vlClients;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisitStartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisitStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "بداية الزياره" : "Visit Start Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisitStartDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisitEndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisitEndDate"].Header).Caption = (GlobalVariables.IsArabic ? "نهاية الزياره" : "Visit End Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VisitEndDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpenLatitude"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpenLatitude"].Header).Caption = (GlobalVariables.IsArabic ? "Open Latitude" : "Open Latitude");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpenLatitude"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpenLong"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpenLong"].Header).Caption = (GlobalVariables.IsArabic ? "Open Long" : "Open Long");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OpenLong"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndLatitude"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndLatitude"].Header).Caption = (GlobalVariables.IsArabic ? "End Latitude" : "End Latitude");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndLatitude"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndLong"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndLong"].Header).Caption = (GlobalVariables.IsArabic ? "End Long" : "End Long");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndLong"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلق" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Hidden = false;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Orders.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)cboLine).ValueChanged -= cboLine_ValueChanged;
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["OrderNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["OrderDate"];
			((TextEditorControlBase)cboLine).Value = drMaster["LineID"];
			((TextEditorControlBase)cboSalesMan).Value = drMaster["SalesSubAccountID"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkClosed).Checked = Convert.ToBoolean(drMaster["Closed"]);
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = OrdersDetails.SelectByOrderID(drMaster["OrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			((TextEditorControlBase)cboLine).ValueChanged += cboLine_ValueChanged;
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesMan).ReadOnly = NavMode || Updating;
		((EditorButtonControlBase)cboLine).ReadOnly = NavMode || Updating;
		((Control)(object)btnSalesManSearch).Enabled = !NavMode || Updating;
		((Control)(object)btnLineSearch).Enabled = !NavMode || Updating;
		((Control)(object)chkClosed).Enabled = !NavMode && (!Updating || drMaster == null || !Convert.ToBoolean(drMaster["Closed"]));
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((Control)(object)btnReloadClients).Visible = Updating;
		object value = ((TextEditorControlBase)cboSalesMan).Value;
		if (Adding)
		{
			DataView dataView = new DataView(dtSalesMan);
			dataView.RowFilter = " Enabled =1  And DefaultBranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboSalesMan, dt, "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		}
		((TextEditorControlBase)cboSalesMan).Value = value;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? Orders.GetCodeByBranchID(GlobalVariables.CurrentBranchID) : "");
		cboLine.SelectedIndex = -1;
		cboSalesMan.SelectedIndex = -1;
		((UltraToggleEditorBase)chkClosed).Checked = false;
		((TextEditorControlBase)txtNotes).Clear();
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
		if (cboLine.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الخط" : "Please Select Line");
			((TextEditorControlBase)cboLine).Focus();
			cboLine.DropDown();
			return false;
		}
		if (cboSalesMan.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مندوب البيع" : "Please Select Salesman");
			((TextEditorControlBase)cboSalesMan).Focus();
			cboSalesMan.DropDown();
			return false;
		}
		if (((UltraToggleEditorBase)chkClosed).Checked && ((DataTable)((UltraGridBase)ULGData).DataSource).Select("Closed = 0").Length != 0)
		{
			GlobalVariables.QuestionMB.Show("يوجد زيارات غير مغلقه في هذا الامر هل مازلت تريد اغلاقه؟", "There Are Open Visits in This Order, Do You Still Want To Close It?");
			if (GlobalVariables.MessageBoxResult == 'N')
			{
				return false;
			}
		}
		if (Adding && int.Parse(Main.ExecuteQuery_DataTable(" Select Count(*) AS Counter From LnsA_Orders Where Closed=0 And Deleted=0 And (LineID = " + ((TextEditorControlBase)cboLine).Value.ToString() + " OR SalesSubAccountID = " + ((TextEditorControlBase)cboSalesMan).Value.ToString() + ")").Rows[0]["Counter"].ToString()) > 0)
		{
			GlobalVariables.InformationMB.Show("لا يمكن فتح أكثر من خط سير للمندوب برجاء إغلاق الخط القديم", "Cannot Open More Than One Line For The Same Sales Man Please Close The Old one ");
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("LnsA_Orders", "OrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OrderNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Orders.GetCodeByBranchID(GlobalVariables.CurrentBranchID);
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
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ClientSubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم العميل  ", "Please Enter Client Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ClientSubAccountID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ClientSubAccountID"].DroppedDown = true;
				return false;
			}
			text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ClientSubAccountID"].Value.ToString() + ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ClientSubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ClientSubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار العميل ", "Cannot Duplicate The Same Client");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ClientSubAccountID"];
					return false;
				}
			}
		}
		DataTable dataTable = Orders.CheckForClientsOpenVisits(text, GlobalVariables.IsArabic ? "1" : "0");
		if (Adding && !dataTable.Rows[0]["OutPutMessage"].ToString().Trim().Equals(""))
		{
			GlobalVariables.InformationMB.Show(dataTable.Rows[0]["OutPutMessage"].ToString().Trim(), dataTable.Rows[0]["OutPutMessage"].ToString().Trim());
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Orders.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSalesMan).Value.ToString(), ((TextEditorControlBase)cboLine).Value.ToString(), ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OrderDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["OrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			OrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			RowID = num.ToString();
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
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Orders.Insert_Update(drMaster["OrderID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboSalesMan).Value.ToString(), ((TextEditorControlBase)cboLine).Value.ToString(), ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OrderDetailID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.DeleteForUpdate("LnsA_OrdersDetails", "OrderID", drMaster["OrderID"].ToString(), "OrderDetailID", text);
			OrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			Orders.DeleteVirtual(drMaster["OrderID"].ToString(), GlobalVariables.UserID);
			OrdersDetails.DeleteVirtualByOrderID(drMaster["OrderID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
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
		}
		else if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
		}
		else if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
		}
		else
		{
			if (HasTransactionValidation(RowID))
			{
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_LnsA_Orders_A.rpt" : "Rep_LnsA_Orders_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OrderIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsAOrdersReport(0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OrderID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtSalesMan = SubAccounts.SelectSalesMan_LnsA(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		object value = ((TextEditorControlBase)cboSalesMan).Value;
		if (Adding)
		{
			DataView dataView = new DataView(dtSalesMan);
			dataView.RowFilter = " Enabled =1  And DefaultBranchID=" + GlobalVariables.CurrentBranchID;
			DataTable dt = dataView.ToTable();
			GlobalFunctions.FillCombo(cboSalesMan, dt, "SubAccountID", "SubAccountName");
		}
		else
		{
			GlobalFunctions.FillCombo(cboSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		}
		((TextEditorControlBase)cboSalesMan).Value = value;
		object value2 = ((TextEditorControlBase)cboLine).Value;
		dtLines = Lines.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLine, dtLines, "LineID", "LineName");
		((TextEditorControlBase)cboLine).Value = value2;
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlClients.ValueListItems.Clear();
		for (int i = 0; i < dtClients.Rows.Count; i++)
		{
			vlClients.ValueListItems.Add(dtClients.Rows[i]["SubAccountID"], dtClients.Rows[i]["SubAccountName"].ToString());
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
	}

	private void cboLine_ValueChanged(object sender, EventArgs e)
	{
		if (cboLine.SelectedIndex > -1)
		{
			dtDetails.Clear();
			DataRow[] array = dtClients.Select("LineID = " + ((TextEditorControlBase)cboLine).Value.ToString());
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["OrderDetailID"] = -1;
				dataRow["ClientSubAccountID"] = array[i]["SubAccountID"];
				dataRow["ClientOrderNo"] = (i + 1).ToString();
				dataRow["VisitStartDate"] = dtpDate.Value;
				dataRow["Closed"] = false;
				dtDetails.Rows.Add(dataRow);
			}
		}
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		if (((UltraToggleEditorBase)chkClosed).Checked)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الزياره خط السير مغلق" : "Cannot Delete This Visit Because Line Already Closed");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		if (((UltraGridBase)ULGData).ActiveRow != null && Invoices.SelectByAppOrderDetailID(((UltraGridBase)ULGData).ActiveRow.Cells["OrderDetailID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الزياره لانه تم عمل فاتورة عليها" : "Cannot Delete This Visit Because There's an Invoice Made On It");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		if (((UltraGridBase)ULGData).ActiveRow != null && InvoicesReturns.SelectByAppOrderDetailID(((UltraGridBase)ULGData).ActiveRow.Cells["OrderDetailID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الزياره لانه تم عمل مرتجع عليها" : "Cannot Delete This Visit Because There's an Return Made On It");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		if (((UltraGridBase)ULGData).ActiveRow != null && Revenues.SelectByAppOrderDetailID(((UltraGridBase)ULGData).ActiveRow.Cells["OrderDetailID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذه الزياره لانه تم عمل ايراد عليها" : "Cannot Delete This Visit Because There's an Revenue Made On It");
			e.DisplayPromptMsg = false;
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void btnLineSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.GLines(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboLine).Value = num;
		}
	}

	private void btnReloadClients_Click(object sender, EventArgs e)
	{
		if (((UltraToggleEditorBase)chkClosed).Checked || cboLine.SelectedIndex <= -1)
		{
			return;
		}
		DataTable dataTable = SubAccounts.FillComboLineHasNoOpenOrders_LnsA(GlobalVariables.ClientSubAccountTypeIDs, "-1", ((TextEditorControlBase)cboLine).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dataTable.Rows.Count <= 0)
		{
			return;
		}
		frmCheckList frmCheckList2 = new frmCheckList(dataTable, "SubAccountID", "SubAccountName", GlobalVariables.IsArabic ? "العميل" : "Client", _selectedChoise: true);
		frmCheckList2.WindowState = FormWindowState.Normal;
		frmCheckList2.ShowDialog();
		if (frmCheckList2.drSelectedRows != null && frmCheckList2.drSelectedRows.Length != 0)
		{
			int num = Convert.ToInt16(dtDetails.Compute("MAX(ClientOrderNo)", "").ToString()) + 1;
			for (int i = 0; i < frmCheckList2.drSelectedRows.Length; i++)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["OrderDetailID"] = -1;
				dataRow["ClientSubAccountID"] = frmCheckList2.drSelectedRows[i]["SubAccountID"];
				dataRow["ClientOrderNo"] = num + i;
				dataRow["VisitStartDate"] = dtpDate.Value;
				dataRow["Closed"] = false;
				dtDetails.Rows.Add(dataRow);
			}
			((Control)(object)btnReloadClients).Visible = false;
		}
	}

	private void frmOrders_Load(object sender, EventArgs e)
	{
	}

	private void lblSalesMan_Click(object sender, EventArgs e)
	{
	}

	private void lblLine_Click(object sender, EventArgs e)
	{
	}

	private void lblNotes_Click(object sender, EventArgs e)
	{
	}

	private void lblDate_Click(object sender, EventArgs e)
	{
	}

	private void chkClosed_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void btnSalesManSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesMan).Value = num;
		}
	}

	public bool HasTransactionValidation(string OrderID)
	{
		string text = Orders.CheckForRelations(OrderID, GlobalVariables.IsArabic ? "1" : "0").Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return false;
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
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.DirectSalesApp.Transactions.frmOrders));
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
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnSalesManSearch = new UltraButton();
		this.lblSalesMan = new UltraLabel();
		this.cboSalesMan = new UltraComboEditor();
		this.btnLineSearch = new UltraButton();
		this.lblLine = new UltraLabel();
		this.cboLine = new UltraComboEditor();
		this.chkClosed = new UltraCheckEditor();
		this.btnReloadClients = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).BeginInit();
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
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Click += new System.EventHandler(lblNotes_Click);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Click += new System.EventHandler(lblDate_Click);
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		resources.ApplyResources(this.btnSalesManSearch, "btnSalesManSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnSalesManSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Name = "btnSalesManSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSalesManSearch).Click += new System.EventHandler(btnSalesManSearch_Click);
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		this.lblSalesMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Click += new System.EventHandler(lblSalesMan_Click);
		resources.ApplyResources(this.cboSalesMan, "cboSalesMan");
		((TextEditorControlBase)this.cboSalesMan).AlwaysInEditMode = true;
		this.cboSalesMan.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesMan).Name = "cboSalesMan";
		((System.Windows.Forms.Control)(object)this.cboSalesMan).TabStop = false;
		resources.ApplyResources(this.btnLineSearch, "btnLineSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnLineSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnLineSearch).Name = "btnLineSearch";
		((System.Windows.Forms.Control)(object)this.btnLineSearch).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnLineSearch).Click += new System.EventHandler(btnLineSearch_Click);
		resources.ApplyResources(this.lblLine, "lblLine");
		this.lblLine.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLine).Name = "lblLine";
		((ControlBase)this.lblLine).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblLine).Click += new System.EventHandler(lblLine_Click);
		resources.ApplyResources(this.cboLine, "cboLine");
		((TextEditorControlBase)this.cboLine).AlwaysInEditMode = true;
		this.cboLine.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLine).Name = "cboLine";
		((System.Windows.Forms.Control)(object)this.cboLine).TabStop = false;
		((TextEditorControlBase)this.cboLine).ValueChanged += new System.EventHandler(cboLine_ValueChanged);
		resources.ApplyResources(this.chkClosed, "chkClosed");
		((System.Windows.Forms.Control)(object)this.chkClosed).Name = "chkClosed";
		((UltraToggleEditorBase)this.chkClosed).CheckedChanged += new System.EventHandler(chkClosed_CheckedChanged);
		resources.ApplyResources(this.btnReloadClients, "btnReloadClients");
		((System.Windows.Forms.Control)(object)this.btnReloadClients).Name = "btnReloadClients";
		((System.Windows.Forms.Control)(object)this.btnReloadClients).Click += new System.EventHandler(btnReloadClients_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReloadClients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClosed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesManSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLineSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Name = "frmOrders";
		base.Load += new System.EventHandler(frmOrders_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLineSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesManSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClosed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReloadClients, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLine).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
