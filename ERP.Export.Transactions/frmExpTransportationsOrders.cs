using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Export;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Export.Transactions;

public class frmExpTransportationsOrders : frmHeaderDetails
{
	private DataTable dtTransporters;

	private DataTable dtPlaces;

	private IContainer components = null;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraLabel lblTransporter;

	private UltraComboEditor cboTransporters;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnContainerSearch;

	public UltraButton btnLoadingPlaceSearch;

	private UltraLabel lblLoadingTransportationPlace;

	private UltraComboEditor cboLoadingTransportationPlace;

	private UltraComboEditor cboContainerTransportationPlace;

	private UltraLabel lblContainerTransportationPlace;

	private UltraTextEditor txtContactPersons;

	private UltraLabel lblContactPersons;

	private UltraCheckEditor chkFinished;

	public UltraButton btnTransportersSearch;

	private UltraButton btnSelectContainers;

	public frmExpTransportationsOrders()
	{
		InitializeComponent();
		TableName = "EXP_TransportationsOrders";
		IDCol = "TransportationOrderID";
		NoCol = "TransportationOrderNo";
		DateCol = "TransportationOrderDate";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtTransporters = Transporters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransporters, dtTransporters, "TransporterID", "TransporterName");
		dtPlaces = TransportationsPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLoadingTransportationPlace, dtPlaces, "TransportationPlaceID", "TransportationPlaceName");
		GlobalFunctions.FillCombo(cboContainerTransportationPlace, dtPlaces, "TransportationPlaceID", "TransportationPlaceName");
		dtDetails = TransportationsOrdersDetails.SelectByTransportationOrderID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransportationOrderDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Header).Caption = (GlobalVariables.IsArabic ? "الحاوية " : "Container");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContainerNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التحميل" : "Loading Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LoadingDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPort"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPort"].Header).Caption = (GlobalVariables.IsArabic ? "تم التوصيل للميناء" : "Deliverd To Port");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPort"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPort"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPortDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPortDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التسليم" : "Delivery Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdToPortDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DriverName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DriverName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم السائق" : "Driver Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DriverName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DriverLicense"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DriverLicense"].Header).Caption = (GlobalVariables.IsArabic ? "رخصة السائق" : "Driver License");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DriverLicense"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckNumber"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckNumber"].Header).Caption = (GlobalVariables.IsArabic ? "رقم السيارة" : "Truck Number");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TruckNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = TransportationsOrders.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["TransportationOrderNo"].ToString();
			((TextEditorControlBase)cboContainerTransportationPlace).Value = drMaster["ContainerTransportationPlaceID"];
			((TextEditorControlBase)cboLoadingTransportationPlace).Value = drMaster["LoadingTransportationPlaceID"];
			((TextEditorControlBase)cboTransporters).Value = drMaster["TransporterID"];
			((UltraToggleEditorBase)chkFinished).Checked = bool.Parse(drMaster["Finished"].ToString());
			dtpDate.Value = drMaster["TransportationOrderDate"];
			((Control)(object)txtContactPersons).Text = drMaster["ContactPersons"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = TransportationsOrdersDetails.SelectByTransportationOrderID(drMaster["TransportationOrderID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
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
		((EditorButtonControlBase)cboContainerTransportationPlace).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLoadingTransportationPlace).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTransporters).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtContactPersons).ReadOnly = NavMode;
		((Control)(object)chkFinished).Enabled = false;
		((Control)(object)btnLoadingPlaceSearch).Visible = !NavMode;
		((Control)(object)btnContainerSearch).Visible = !NavMode;
		((Control)(object)btnTransportersSearch).Visible = !NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? TransportationsOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((UltraToggleEditorBase)chkFinished).Checked = false;
		cboContainerTransportationPlace.SelectedIndex = -1;
		cboLoadingTransportationPlace.SelectedIndex = -1;
		cboTransporters.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtContactPersons).Clear();
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value || dtpDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ امر النقل" : "Please Enter The Trucking Date");
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
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم أمر النقل" : "Please Enter The Trucking Order No");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboTransporters.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار شركة النقل" : "Please Select The Transporter");
			((TextEditorControlBase)cboTransporters).Focus();
			cboTransporters.DropDown();
			return false;
		}
		if (cboLoadingTransportationPlace.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مكان التحميل" : "Please Select Loading Place");
			((TextEditorControlBase)cboLoadingTransportationPlace).Focus();
			cboLoadingTransportationPlace.DropDown();
			return false;
		}
		if (cboContainerTransportationPlace.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار مكان الحاوية" : "Please Select Container Place");
			((TextEditorControlBase)cboContainerTransportationPlace).Focus();
			cboContainerTransportationPlace.DropDown();
			return false;
		}
		if (Main.CheckForValue("EXP_TransportationsOrders", "TransportationOrderNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["TransportationOrderNo"].ToString(), IsFromServer: true) > 0)
		{
			string codeByBranchID = TransportationsOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
			GlobalVariables.QuestionMB.Show("رقم امر النقل متواجد من قبل \n سوف يتم الحفظ برقم " + codeByBranchID, "The Trucking Order No. Already Exists It Will Be Saved With No. : " + codeByBranchID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByBranchID;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["OperationDeclarationContainerID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["OperationDeclarationContainerID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "رقم الحاوية متواجد من قبل" : "Container Already Exists");
					return false;
				}
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["OperationDeclarationContainerID"].Value == DBNull.Value)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال رقم الحاوية", "Please Enter Container No.");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["OperationDeclarationContainerID"];
				((UltraGridBase)ULGData).Rows[i].Cells["OperationDeclarationContainerID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = TransportationsOrders.Insert_Update("-1", ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboTransporters.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTransporters).Value.ToString(), (cboLoadingTransportationPlace.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingTransportationPlace).Value.ToString(), (cboContainerTransportationPlace.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainerTransportationPlace).Value.ToString(), (((Control)(object)txtContactPersons).Text == "") ? "Null" : ((Control)(object)txtContactPersons).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkFinished).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TransportationOrderDetailID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["TransportationOrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				TransportationsOrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = TransportationsOrders.Insert_Update(drMaster["TransportationOrderID"].ToString(), ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboTransporters.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTransporters).Value.ToString(), (cboLoadingTransportationPlace.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboLoadingTransportationPlace).Value.ToString(), (cboContainerTransportationPlace.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboContainerTransportationPlace).Value.ToString(), (((Control)(object)txtContactPersons).Text == "") ? "Null" : ((Control)(object)txtContactPersons).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkFinished).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["TransportationOrderDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["TransportationOrderID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("EXP_TransportationsOrdersDetails", "TransportationOrderID", num.ToString(), "TransportationOrderDetailID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				TransportationsOrdersDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		base.DeleteData();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			TransportationsOrdersDetails.DeleteVirtualByTransportationOrderID(drMaster["TransportationOrderID"].ToString(), GlobalVariables.UserID);
			TransportationsOrders.DeleteVirtual(drMaster["TransportationOrderID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
		dtTransporters = Transporters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransporters, dtTransporters, "TransporterID", "TransporterName");
		dtPlaces = TransportationsPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLoadingTransportationPlace, dtPlaces, "TransportationPlaceID", "TransportationPlaceName");
		GlobalFunctions.FillCombo(cboContainerTransportationPlace, dtPlaces, "TransportationPlaceID", "TransportationPlaceName");
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ContainerNo")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ExpTransportationsOrdersReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["TransportationOrderID"].ToString();
			FillData();
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = TransportationsOrders.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void btnLoadingPlaceSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.TransportationsPlacesSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboLoadingTransportationPlace).Value = num;
		}
	}

	private void btnContainerSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.TransportationsPlacesSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboContainerTransportationPlace).Value = num;
		}
	}

	private void btnTransportersSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.TransportersSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboTransporters).Value = num;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if (Updating && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OperationDeclarationContainerID" && !bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["DeliverdToPort"].Value.ToString()))
		{
			int num = SearchFunctions.OperationsDeclarationsContainersSearch(Adding ? "-1" : drMaster["TransportationOrderID"].ToString(), "-1", IsFromServer: false);
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["OperationDeclarationContainerID"].Value = num;
			}
		}
		e.Handled = true;
	}

	public override void btnPrintClick()
	{
		if (RowID != "")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_EXP_TransportationsOrders_A.rpt" : "Rep_EXP_TransportationsOrders_A.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@TransportationOrderIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void btnSelectContainers_Click(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			return;
		}
		DataTable dataTable = SearchFunctions.OperationsDeclarationsContainersSearchReport(Adding ? "-1" : drMaster["TransportationOrderID"].ToString(), "-1", IsFromServer: false);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dtDetails.Select("OperationDeclarationContainerID = " + dataTable.Rows[i]["OperationDeclarationContainerID"].ToString()).Length == 0)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["TransportationOrderID"] = (Adding ? ((object)(-1)) : drMaster["TransportationOrderID"]);
				dataRow["TransportationOrderDetailID"] = -1;
				dataRow["OperationDeclarationContainerID"] = dataTable.Rows[i]["OperationDeclarationContainerID"];
				dataRow["ContainerNo"] = dataTable.Rows[i]["ContainerNo"];
				dataRow["LoadingDate"] = dataTable.Rows[i]["COTDate"];
				dataRow["Deleted"] = false;
				dataRow["DeliverdToPort"] = false;
				dtDetails.Rows.Add(dataRow);
			}
		}
		((UltraGridBase)ULGData).UpdateData();
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
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_04aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Export.Transactions.frmExpTransportationsOrders));
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
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.lblTransporter = new UltraLabel();
		this.cboTransporters = new UltraComboEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnContainerSearch = new UltraButton();
		this.btnLoadingPlaceSearch = new UltraButton();
		this.lblLoadingTransportationPlace = new UltraLabel();
		this.cboLoadingTransportationPlace = new UltraComboEditor();
		this.cboContainerTransportationPlace = new UltraComboEditor();
		this.lblContainerTransportationPlace = new UltraLabel();
		this.txtContactPersons = new UltraTextEditor();
		this.lblContactPersons = new UltraLabel();
		this.chkFinished = new UltraCheckEditor();
		this.btnTransportersSearch = new UltraButton();
		this.btnSelectContainers = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransporters).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingTransportationPlace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainerTransportationPlace).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContactPersons).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkFinished).BeginInit();
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
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
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
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.lblTransporter, "lblTransporter");
		this.lblTransporter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTransporter).Name = "lblTransporter";
		((ControlBase)this.lblTransporter).WrapText = false;
		resources.ApplyResources(this.cboTransporters, "cboTransporters");
		this.cboTransporters.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTransporters).Name = "cboTransporters";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.btnContainerSearch, "btnContainerSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance12");
		((ControlBase)this.btnContainerSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnContainerSearch).Name = "btnContainerSearch";
		((System.Windows.Forms.Control)(object)this.btnContainerSearch).Click += new System.EventHandler(btnContainerSearch_Click);
		resources.ApplyResources(this.btnLoadingPlaceSearch, "btnLoadingPlaceSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance13");
		((ControlBase)this.btnLoadingPlaceSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnLoadingPlaceSearch).Name = "btnLoadingPlaceSearch";
		((System.Windows.Forms.Control)(object)this.btnLoadingPlaceSearch).Click += new System.EventHandler(btnLoadingPlaceSearch_Click);
		resources.ApplyResources(this.lblLoadingTransportationPlace, "lblLoadingTransportationPlace");
		this.lblLoadingTransportationPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadingTransportationPlace).Name = "lblLoadingTransportationPlace";
		((ControlBase)this.lblLoadingTransportationPlace).WrapText = false;
		resources.ApplyResources(this.cboLoadingTransportationPlace, "cboLoadingTransportationPlace");
		this.cboLoadingTransportationPlace.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboLoadingTransportationPlace).Name = "cboLoadingTransportationPlace";
		resources.ApplyResources(this.cboContainerTransportationPlace, "cboContainerTransportationPlace");
		this.cboContainerTransportationPlace.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboContainerTransportationPlace).Name = "cboContainerTransportationPlace";
		resources.ApplyResources(this.lblContainerTransportationPlace, "lblContainerTransportationPlace");
		this.lblContainerTransportationPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainerTransportationPlace).Name = "lblContainerTransportationPlace";
		((ControlBase)this.lblContainerTransportationPlace).WrapText = false;
		resources.ApplyResources(this.txtContactPersons, "txtContactPersons");
		((System.Windows.Forms.Control)(object)this.txtContactPersons).Name = "txtContactPersons";
		resources.ApplyResources(this.lblContactPersons, "lblContactPersons");
		this.lblContactPersons.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContactPersons).Name = "lblContactPersons";
		((ControlBase)this.lblContactPersons).WrapText = false;
		resources.ApplyResources(this.chkFinished, "chkFinished");
		((System.Windows.Forms.Control)(object)this.chkFinished).Name = "chkFinished";
		resources.ApplyResources(this.btnTransportersSearch, "btnTransportersSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.btnTransportersSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnTransportersSearch).Name = "btnTransportersSearch";
		((System.Windows.Forms.Control)(object)this.btnTransportersSearch).Click += new System.EventHandler(btnTransportersSearch_Click);
		resources.ApplyResources(this.btnSelectContainers, "btnSelectContainers");
		((System.Windows.Forms.Control)(object)this.btnSelectContainers).Name = "btnSelectContainers";
		((System.Windows.Forms.Control)(object)this.btnSelectContainers).Click += new System.EventHandler(btnSelectContainers_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectContainers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnTransportersSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkFinished);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnContainerSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLoadingPlaceSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadingTransportationPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLoadingTransportationPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboContainerTransportationPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainerTransportationPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTransporter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTransporters);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContactPersons);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContactPersons);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Name = "frmExpTransportationsOrders";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContactPersons, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContactPersons, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTransporters, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTransporter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainerTransportationPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboContainerTransportationPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLoadingTransportationPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadingTransportationPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLoadingPlaceSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnContainerSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkFinished, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnTransportersSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectContainers, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransporters).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLoadingTransportationPlace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboContainerTransportationPlace).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContactPersons).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkFinished).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
