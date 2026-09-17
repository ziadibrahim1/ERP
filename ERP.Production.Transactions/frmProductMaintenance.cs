using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.Production;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Transactions;

public class frmProductMaintenance : frmHeaderDetails
{
	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtBatches;

	private DataTable dtSizes;

	private DataTable dtClients;

	private DataTable dtUsers;

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes;

	private bool UsingBatches = false;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboClient;

	private UltraLabel lblClient;

	private UltraLabel lblExpectedDeliveryDate;

	private UltraDateTimeEditor dtpExpectedDeliveryDate;

	public UltraButton btnClientSearch;

	private UltraCheckEditor chkSentToMaintenance;

	private UltraLabel lblItemID;

	private UltraComboEditor cboItem;

	private UltraLabel lblClientPhone;

	private UltraTextEditor txtPhone;

	private UltraComboEditor cboColors;

	private UltraLabel lblItemColor;

	private UltraComboEditor cboSize;

	private UltraComboEditor cboBatch;

	private UltraLabel lblItemSize;

	private UltraLabel lblItemBatch;

	private UltraCheckEditor chkMaintenanceReceived;

	private UltraCheckEditor chkSentToBranch;

	private UltraCheckEditor chkBranchReceived;

	private UltraCheckEditor chkDeliveredToClient;

	private UltraDateTimeEditor dtpSentToMaintenance;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtSentToMaintenanceDriver;

	private UltraLabel lblSentToMaintenanceDriverName;

	private UltraDateTimeEditor dtpMaintenanceReceived;

	private UltraLabel lblMaintenanceReceivedDate;

	private UltraDateTimeEditor dtpSentToBranch;

	private UltraLabel lblSentToBranchDate;

	private UltraTextEditor txtSentToBranchDriver;

	private UltraLabel lblSentToBranchDriver;

	private UltraDateTimeEditor dtpBranchReceived;

	private UltraLabel lblBranchReceivedDate;

	private UltraLabel ultraLabel6;

	private UltraDateTimeEditor dtpDeliveredToClient;

	private UltraLabel lblSentToMaintenanceUser;

	private UltraComboEditor cboSentToMaintenanceUsers;

	private UltraComboEditor cboSentToBranchUsers;

	private UltraLabel lblSentToBranchUser;

	private UltraComboEditor cboMaintenanceRceievedUsers;

	private UltraLabel lblMaintenanceReceivedUser;

	private UltraComboEditor cboBranchReceivedUsers;

	private UltraLabel lblBranchReceivedUser;

	private UltraComboEditor cboDeliveredToClientUsers;

	private UltraLabel lblDeliveredToClientUser;

	public frmProductMaintenance()
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
		TableName = "Pro_ProductMaintenance";
		IDCol = "ProductMaintenanceID";
		NoCol = "ProductMaintenanceNo";
		DateCol = "ProductMaintenanceDate";
	}

	public frmProductMaintenance(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		UsingBatches = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		((Control)(object)lblItemColor).Text = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((Control)(object)lblItemSize).Text = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((Control)(object)lblItemSize).Visible = UsingSizes;
		((Control)(object)cboSize).Visible = UsingSizes;
		((Control)(object)lblItemColor).Visible = UsingColors;
		((Control)(object)cboColors).Visible = UsingColors;
		((Control)(object)lblItemBatch).Visible = UsingBatches;
		((Control)(object)cboBatch).Visible = UsingBatches;
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSentToMaintenanceUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboMaintenanceRceievedUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboSentToBranchUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboBranchReceivedUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboDeliveredToClientUsers, dtUsers, "UserID", "UserName");
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboColors, dtColors, "ColorID", "ColorName");
		}
		if (UsingSizes)
		{
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboSize, dtSizes, "ItemSizeID", "ItemSizeName");
		}
		if (UsingBatches)
		{
			dtBatches = ItemsBatches.FillCombo(IsFromServer: false);
			GlobalFunctions.FillCombo(cboBatch, dtBatches, "BatchID", "BatchName");
		}
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItem, dtItems, "ItemID", "Name");
		dtDetails = ProductMaintenanceDetails.SelectByProductMaintenanceID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductMaintenanceDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fixed"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductProblem"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fixed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductProblem"].Header).Caption = (GlobalVariables.IsArabic ? "العـيـــب" : "Problem");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fixed"].Header).Caption = (GlobalVariables.IsArabic ? "تم" : "Fixed");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductProblem"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Fixed"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ProductMaintenance.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["ProductMaintenanceNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["ProductMaintenanceDate"];
			((TextEditorControlBase)cboClient).Value = drMaster["SubAccountID"];
			((TextEditorControlBase)txtPhone).Value = drMaster["ClientPhoneNo"];
			((TextEditorControlBase)cboItem).Value = drMaster["ItemID"];
			((TextEditorControlBase)cboColors).Value = drMaster["ColorID"];
			((TextEditorControlBase)cboSize).Value = drMaster["ItemSizeID"];
			((TextEditorControlBase)cboBatch).Value = drMaster["BatchID"];
			dtpExpectedDeliveryDate.Value = (DateTime)drMaster["ExpectedDeliveryDate"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkBranchReceived).Checked = bool.Parse(drMaster["BranchReceived"].ToString());
			dtpBranchReceived.Value = drMaster["BranchReceivedDate"];
			((UltraToggleEditorBase)chkMaintenanceReceived).Checked = bool.Parse(drMaster["MaintenanceReceived"].ToString());
			dtpMaintenanceReceived.Value = drMaster["MaintenanceReceivedDate"];
			((UltraToggleEditorBase)chkSentToBranch).Checked = bool.Parse(drMaster["SentToBranch"].ToString());
			dtpSentToBranch.Value = drMaster["SentToBranchDate"];
			((TextEditorControlBase)txtSentToBranchDriver).Value = drMaster["SentToBranchDriverName"];
			((UltraToggleEditorBase)chkSentToMaintenance).Checked = bool.Parse(drMaster["SentToMaintenance"].ToString());
			dtpSentToMaintenance.Value = drMaster["SentToMaintenanceDate"];
			((TextEditorControlBase)txtSentToMaintenanceDriver).Value = drMaster["SentToMaintenanceDriverName"];
			((UltraToggleEditorBase)chkDeliveredToClient).Checked = bool.Parse(drMaster["DeliveredToClient"].ToString());
			dtpDeliveredToClient.Value = drMaster["DeliveredToClientDate"];
			((TextEditorControlBase)cboSentToMaintenanceUsers).Value = drMaster["SentToMaintenanceUserID"];
			((TextEditorControlBase)cboMaintenanceRceievedUsers).Value = drMaster["MaintenanceReceivedUserID"];
			((TextEditorControlBase)cboSentToBranchUsers).Value = drMaster["SentToBranchUserID"];
			((TextEditorControlBase)cboBranchReceivedUsers).Value = drMaster["BranchReceivedUserID"];
			((TextEditorControlBase)cboDeliveredToClientUsers).Value = drMaster["DeliveredToClientUserID"];
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ProductMaintenanceDetails.SelectByProductMaintenanceID(drMaster["ProductMaintenanceID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpExpectedDeliveryDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboClient).ReadOnly = NavMode;
		((EditorButtonControlBase)cboItem).ReadOnly = NavMode;
		((EditorButtonControlBase)cboColors).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSize).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBatch).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPhone).ReadOnly = NavMode;
		((Control)(object)btnClientSearch).Visible = !NavMode;
		((Control)(object)chkBranchReceived).Enabled = false;
		((Control)(object)chkMaintenanceReceived).Enabled = false;
		((Control)(object)chkSentToBranch).Enabled = false;
		((Control)(object)chkSentToMaintenance).Enabled = false;
		((Control)(object)chkDeliveredToClient).Enabled = false;
		((EditorButtonControlBase)dtpBranchReceived).ReadOnly = true;
		((EditorButtonControlBase)dtpMaintenanceReceived).ReadOnly = true;
		((EditorButtonControlBase)dtpSentToBranch).ReadOnly = true;
		((EditorButtonControlBase)dtpSentToMaintenance).ReadOnly = true;
		((EditorButtonControlBase)dtpDeliveredToClient).ReadOnly = true;
		((EditorButtonControlBase)txtSentToBranchDriver).ReadOnly = true;
		((EditorButtonControlBase)txtSentToMaintenanceDriver).ReadOnly = true;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? ProductMaintenance.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboClient.SelectedIndex = -1;
		cboItem.SelectedIndex = -1;
		cboColors.SelectedIndex = -1;
		cboSize.SelectedIndex = -1;
		cboBatch.SelectedIndex = -1;
		cboSentToMaintenanceUsers.SelectedIndex = -1;
		cboMaintenanceRceievedUsers.SelectedIndex = -1;
		cboSentToBranchUsers.SelectedIndex = -1;
		cboBranchReceivedUsers.SelectedIndex = -1;
		cboDeliveredToClientUsers.SelectedIndex = -1;
		dtpExpectedDeliveryDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		dtpDeliveredToClient.Value = null;
		dtpMaintenanceReceived.Value = null;
		dtpBranchReceived.Value = null;
		dtpSentToBranch.Value = null;
		dtpSentToMaintenance.Value = null;
		((TextEditorControlBase)txtPhone).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtSentToBranchDriver).Clear();
		((TextEditorControlBase)txtSentToMaintenanceDriver).Clear();
		((UltraToggleEditorBase)chkSentToMaintenance).Checked = false;
		((UltraToggleEditorBase)chkSentToBranch).Checked = false;
		((UltraToggleEditorBase)chkDeliveredToClient).Checked = false;
		((UltraToggleEditorBase)chkMaintenanceReceived).Checked = false;
		((UltraToggleEditorBase)chkBranchReceived).Checked = false;
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
		if (cboClient.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboClient).Focus();
			cboClient.DropDown();
			return false;
		}
		if (cboItem.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الصنف" : "Please Select item");
			((TextEditorControlBase)cboItem).Focus();
			cboItem.DropDown();
			return false;
		}
		if (UsingColors && cboColors.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار اللون" : "Please Select Color");
			((TextEditorControlBase)cboColors).Focus();
			cboColors.DropDown();
			return false;
		}
		if (UsingSizes && cboSize.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المقاس" : "Please Select Size");
			((TextEditorControlBase)cboSize).Focus();
			cboSize.DropDown();
			return false;
		}
		if (UsingBatches && cboBatch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار السريل" : "Please Select Batch No.");
			((TextEditorControlBase)cboBatch).Focus();
			cboBatch.DropDown();
			return false;
		}
		if (dtpExpectedDeliveryDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الاستلام المتوقع" : "Please Enter The Expected Delivery Date");
			((Control)(object)dtpExpectedDeliveryDate).Focus();
			dtpExpectedDeliveryDate.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Pro_ProductMaintenance", "ProductMaintenanceNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ProductMaintenanceNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = ProductMaintenance.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			if (((UltraGridBase)ULGData).Rows[i].Cells["ProductProblem"].Value.ToString().Trim() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال العيب  ", "Please Enter the probem");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ProductProblem"];
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
			int num = ProductMaintenance.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtPhone).Text.Trim(), (cboItem.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItem).Value.ToString(), (cboColors.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboColors).Value.ToString(), (cboSize.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSize).Value.ToString(), (cboBatch.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBatch).Value.ToString(), dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, "0", "Null", "", "Null", "0", "Null", "Null", "0", "Null", "", "Null", "0", "Null", "Null", "0", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ProductMaintenanceDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["ProductMaintenanceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ProductMaintenanceDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			RowID = num.ToString();
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
			int num = ProductMaintenance.Insert_Update(drMaster["ProductMaintenanceID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboClient.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClient).Value.ToString(), ((Control)(object)txtPhone).Text.Trim(), (cboItem.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboItem).Value.ToString(), (cboColors.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboColors).Value.ToString(), (cboSize.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSize).Value.ToString(), (cboBatch.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBatch).Value.ToString(), dtpExpectedDeliveryDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtNotes).Text, "0", "Null", "", "Null", "0", "Null", "Null", "0", "Null", "", "Null", "0", "Null", "Null", "0", "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ProductMaintenanceID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ProductMaintenanceDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("Pro_ProductMaintenanceDetails", "ProductMaintenanceID", drMaster["ProductMaintenanceID"].ToString(), "ProductMaintenanceDetailID", text);
			ProductMaintenanceDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			ProductMaintenance.DeleteVirtual(drMaster["ProductMaintenanceID"].ToString(), GlobalVariables.UserID);
			ProductMaintenanceDetails.DeleteVirtualByProductMaintenanceID(drMaster["ProductMaintenanceID"].ToString(), GlobalVariables.UserID);
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
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Pro_ProductMaintenance_A.rpt" : "Rep_Pro_ProductMaintenance_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ProductMaintenanceIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ProductMaintenanceReport(-1, -1, -1, -1, -1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ProductMaintenanceID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSentToMaintenanceUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboMaintenanceRceievedUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboSentToBranchUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboBranchReceivedUsers, dtUsers, "UserID", "UserName");
		GlobalFunctions.FillCombo(cboDeliveredToClientUsers, dtUsers, "UserID", "UserName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClient, dtClients, "SubAccountID", "SubAccountName");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "1", "0", "1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboItem, dtItems, "ItemID", "Name");
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboColors, dtColors, "ColorID", "ColorName");
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSize, dtSizes, "ItemSizeID", "ItemSizeName");
		dtBatches = ItemsBatches.FillCombo(IsFromServer: false);
		GlobalFunctions.FillCombo(cboBatch, dtBatches, "BatchID", "BatchName");
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = 0;
		num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboClient).Value = num;
		}
	}

	private void cboClient_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = 0;
			num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboClient).Value = num;
			}
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = ProductMaintenance.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Transactions.frmProductMaintenance));
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
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboClient = new UltraComboEditor();
		this.lblClient = new UltraLabel();
		this.lblExpectedDeliveryDate = new UltraLabel();
		this.dtpExpectedDeliveryDate = new UltraDateTimeEditor();
		this.btnClientSearch = new UltraButton();
		this.chkSentToMaintenance = new UltraCheckEditor();
		this.lblItemID = new UltraLabel();
		this.cboItem = new UltraComboEditor();
		this.lblClientPhone = new UltraLabel();
		this.txtPhone = new UltraTextEditor();
		this.cboColors = new UltraComboEditor();
		this.lblItemColor = new UltraLabel();
		this.cboSize = new UltraComboEditor();
		this.cboBatch = new UltraComboEditor();
		this.lblItemSize = new UltraLabel();
		this.lblItemBatch = new UltraLabel();
		this.chkMaintenanceReceived = new UltraCheckEditor();
		this.chkSentToBranch = new UltraCheckEditor();
		this.chkBranchReceived = new UltraCheckEditor();
		this.chkDeliveredToClient = new UltraCheckEditor();
		this.dtpSentToMaintenance = new UltraDateTimeEditor();
		this.ultraLabel1 = new UltraLabel();
		this.txtSentToMaintenanceDriver = new UltraTextEditor();
		this.lblSentToMaintenanceDriverName = new UltraLabel();
		this.dtpMaintenanceReceived = new UltraDateTimeEditor();
		this.lblMaintenanceReceivedDate = new UltraLabel();
		this.dtpSentToBranch = new UltraDateTimeEditor();
		this.lblSentToBranchDate = new UltraLabel();
		this.txtSentToBranchDriver = new UltraTextEditor();
		this.lblSentToBranchDriver = new UltraLabel();
		this.dtpBranchReceived = new UltraDateTimeEditor();
		this.lblBranchReceivedDate = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.dtpDeliveredToClient = new UltraDateTimeEditor();
		this.lblSentToMaintenanceUser = new UltraLabel();
		this.cboSentToMaintenanceUsers = new UltraComboEditor();
		this.cboSentToBranchUsers = new UltraComboEditor();
		this.lblSentToBranchUser = new UltraLabel();
		this.cboMaintenanceRceievedUsers = new UltraComboEditor();
		this.lblMaintenanceReceivedUser = new UltraLabel();
		this.cboBranchReceivedUsers = new UltraComboEditor();
		this.lblBranchReceivedUser = new UltraLabel();
		this.cboDeliveredToClientUsers = new UltraComboEditor();
		this.lblDeliveredToClientUser = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSentToMaintenance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPhone).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboColors).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSize).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBatch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkMaintenanceReceived).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSentToBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranchReceived).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkDeliveredToClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSentToMaintenance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSentToMaintenanceDriver).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMaintenanceReceived).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSentToBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSentToBranchDriver).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBranchReceived).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliveredToClient).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSentToMaintenanceUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSentToBranchUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaintenanceRceievedUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchReceivedUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveredToClientUsers).BeginInit();
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
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboClient, "cboClient");
		((TextEditorControlBase)this.cboClient).AlwaysInEditMode = true;
		this.cboClient.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClient).Name = "cboClient";
		((System.Windows.Forms.Control)(object)this.cboClient).KeyDown += new System.Windows.Forms.KeyEventHandler(cboClient_KeyDown);
		resources.ApplyResources(this.lblClient, "lblClient");
		this.lblClient.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClient).Name = "lblClient";
		((ControlBase)this.lblClient).WrapText = false;
		resources.ApplyResources(this.lblExpectedDeliveryDate, "lblExpectedDeliveryDate");
		this.lblExpectedDeliveryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate).Name = "lblExpectedDeliveryDate";
		((ControlBase)this.lblExpectedDeliveryDate).WrapText = false;
		resources.ApplyResources(this.dtpExpectedDeliveryDate, "dtpExpectedDeliveryDate");
		((UltraWinEditorMaskedControlBase)this.dtpExpectedDeliveryDate).AlwaysInEditMode = true;
		this.dtpExpectedDeliveryDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate).Name = "dtpExpectedDeliveryDate";
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance13");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.chkSentToMaintenance, "chkSentToMaintenance");
		((System.Windows.Forms.Control)(object)this.chkSentToMaintenance).Name = "chkSentToMaintenance";
		resources.ApplyResources(this.lblItemID, "lblItemID");
		this.lblItemID.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemID).Name = "lblItemID";
		((ControlBase)this.lblItemID).WrapText = false;
		resources.ApplyResources(this.cboItem, "cboItem");
		((TextEditorControlBase)this.cboItem).AlwaysInEditMode = true;
		this.cboItem.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboItem).Name = "cboItem";
		resources.ApplyResources(this.lblClientPhone, "lblClientPhone");
		this.lblClientPhone.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientPhone).Name = "lblClientPhone";
		((ControlBase)this.lblClientPhone).WrapText = false;
		resources.ApplyResources(this.txtPhone, "txtPhone");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtPhone).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtPhone).Name = "txtPhone";
		resources.ApplyResources(this.cboColors, "cboColors");
		((TextEditorControlBase)this.cboColors).AlwaysInEditMode = true;
		this.cboColors.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboColors).Name = "cboColors";
		resources.ApplyResources(this.lblItemColor, "lblItemColor");
		this.lblItemColor.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemColor).Name = "lblItemColor";
		((ControlBase)this.lblItemColor).WrapText = false;
		resources.ApplyResources(this.cboSize, "cboSize");
		((TextEditorControlBase)this.cboSize).AlwaysInEditMode = true;
		this.cboSize.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSize).Name = "cboSize";
		resources.ApplyResources(this.cboBatch, "cboBatch");
		((TextEditorControlBase)this.cboBatch).AlwaysInEditMode = true;
		this.cboBatch.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBatch).Name = "cboBatch";
		resources.ApplyResources(this.lblItemSize, "lblItemSize");
		this.lblItemSize.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemSize).Name = "lblItemSize";
		((ControlBase)this.lblItemSize).WrapText = false;
		resources.ApplyResources(this.lblItemBatch, "lblItemBatch");
		this.lblItemBatch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemBatch).Name = "lblItemBatch";
		((ControlBase)this.lblItemBatch).WrapText = false;
		resources.ApplyResources(this.chkMaintenanceReceived, "chkMaintenanceReceived");
		((System.Windows.Forms.Control)(object)this.chkMaintenanceReceived).Name = "chkMaintenanceReceived";
		resources.ApplyResources(this.chkSentToBranch, "chkSentToBranch");
		((System.Windows.Forms.Control)(object)this.chkSentToBranch).Name = "chkSentToBranch";
		resources.ApplyResources(this.chkBranchReceived, "chkBranchReceived");
		((System.Windows.Forms.Control)(object)this.chkBranchReceived).Name = "chkBranchReceived";
		resources.ApplyResources(this.chkDeliveredToClient, "chkDeliveredToClient");
		((System.Windows.Forms.Control)(object)this.chkDeliveredToClient).Name = "chkDeliveredToClient";
		resources.ApplyResources(this.dtpSentToMaintenance, "dtpSentToMaintenance");
		((UltraWinEditorMaskedControlBase)this.dtpSentToMaintenance).AlwaysInEditMode = true;
		this.dtpSentToMaintenance.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpSentToMaintenance).Name = "dtpSentToMaintenance";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtSentToMaintenanceDriver, "txtSentToMaintenanceDriver");
		resources.ApplyResources(val11, "appearance11");
		((TextEditorControlBase)this.txtSentToMaintenanceDriver).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.txtSentToMaintenanceDriver).Name = "txtSentToMaintenanceDriver";
		resources.ApplyResources(this.lblSentToMaintenanceDriverName, "lblSentToMaintenanceDriverName");
		this.lblSentToMaintenanceDriverName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentToMaintenanceDriverName).Name = "lblSentToMaintenanceDriverName";
		((ControlBase)this.lblSentToMaintenanceDriverName).WrapText = false;
		resources.ApplyResources(this.dtpMaintenanceReceived, "dtpMaintenanceReceived");
		((UltraWinEditorMaskedControlBase)this.dtpMaintenanceReceived).AlwaysInEditMode = true;
		this.dtpMaintenanceReceived.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpMaintenanceReceived).Name = "dtpMaintenanceReceived";
		resources.ApplyResources(this.lblMaintenanceReceivedDate, "lblMaintenanceReceivedDate");
		this.lblMaintenanceReceivedDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaintenanceReceivedDate).Name = "lblMaintenanceReceivedDate";
		((ControlBase)this.lblMaintenanceReceivedDate).WrapText = false;
		resources.ApplyResources(this.dtpSentToBranch, "dtpSentToBranch");
		((UltraWinEditorMaskedControlBase)this.dtpSentToBranch).AlwaysInEditMode = true;
		this.dtpSentToBranch.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpSentToBranch).Name = "dtpSentToBranch";
		resources.ApplyResources(this.lblSentToBranchDate, "lblSentToBranchDate");
		this.lblSentToBranchDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentToBranchDate).Name = "lblSentToBranchDate";
		((ControlBase)this.lblSentToBranchDate).WrapText = false;
		resources.ApplyResources(this.txtSentToBranchDriver, "txtSentToBranchDriver");
		resources.ApplyResources(val12, "appearance12");
		((TextEditorControlBase)this.txtSentToBranchDriver).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.txtSentToBranchDriver).Name = "txtSentToBranchDriver";
		resources.ApplyResources(this.lblSentToBranchDriver, "lblSentToBranchDriver");
		this.lblSentToBranchDriver.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentToBranchDriver).Name = "lblSentToBranchDriver";
		((ControlBase)this.lblSentToBranchDriver).WrapText = false;
		resources.ApplyResources(this.dtpBranchReceived, "dtpBranchReceived");
		((UltraWinEditorMaskedControlBase)this.dtpBranchReceived).AlwaysInEditMode = true;
		this.dtpBranchReceived.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpBranchReceived).Name = "dtpBranchReceived";
		resources.ApplyResources(this.lblBranchReceivedDate, "lblBranchReceivedDate");
		this.lblBranchReceivedDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchReceivedDate).Name = "lblBranchReceivedDate";
		((ControlBase)this.lblBranchReceivedDate).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.dtpDeliveredToClient, "dtpDeliveredToClient");
		((UltraWinEditorMaskedControlBase)this.dtpDeliveredToClient).AlwaysInEditMode = true;
		this.dtpDeliveredToClient.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDeliveredToClient).Name = "dtpDeliveredToClient";
		resources.ApplyResources(this.lblSentToMaintenanceUser, "lblSentToMaintenanceUser");
		this.lblSentToMaintenanceUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentToMaintenanceUser).Name = "lblSentToMaintenanceUser";
		((ControlBase)this.lblSentToMaintenanceUser).WrapText = false;
		resources.ApplyResources(this.cboSentToMaintenanceUsers, "cboSentToMaintenanceUsers");
		this.cboSentToMaintenanceUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSentToMaintenanceUsers).Name = "cboSentToMaintenanceUsers";
		resources.ApplyResources(this.cboSentToBranchUsers, "cboSentToBranchUsers");
		this.cboSentToBranchUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSentToBranchUsers).Name = "cboSentToBranchUsers";
		resources.ApplyResources(this.lblSentToBranchUser, "lblSentToBranchUser");
		this.lblSentToBranchUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSentToBranchUser).Name = "lblSentToBranchUser";
		((ControlBase)this.lblSentToBranchUser).WrapText = false;
		resources.ApplyResources(this.cboMaintenanceRceievedUsers, "cboMaintenanceRceievedUsers");
		this.cboMaintenanceRceievedUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboMaintenanceRceievedUsers).Name = "cboMaintenanceRceievedUsers";
		resources.ApplyResources(this.lblMaintenanceReceivedUser, "lblMaintenanceReceivedUser");
		this.lblMaintenanceReceivedUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaintenanceReceivedUser).Name = "lblMaintenanceReceivedUser";
		((ControlBase)this.lblMaintenanceReceivedUser).WrapText = false;
		resources.ApplyResources(this.cboBranchReceivedUsers, "cboBranchReceivedUsers");
		this.cboBranchReceivedUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBranchReceivedUsers).Name = "cboBranchReceivedUsers";
		resources.ApplyResources(this.lblBranchReceivedUser, "lblBranchReceivedUser");
		this.lblBranchReceivedUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchReceivedUser).Name = "lblBranchReceivedUser";
		((ControlBase)this.lblBranchReceivedUser).WrapText = false;
		resources.ApplyResources(this.cboDeliveredToClientUsers, "cboDeliveredToClientUsers");
		this.cboDeliveredToClientUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboDeliveredToClientUsers).Name = "cboDeliveredToClientUsers";
		resources.ApplyResources(this.lblDeliveredToClientUser, "lblDeliveredToClientUser");
		this.lblDeliveredToClientUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveredToClientUser).Name = "lblDeliveredToClientUser";
		((ControlBase)this.lblDeliveredToClientUser).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveredToClientUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDeliveredToClientUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchReceivedUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchReceivedUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaintenanceReceivedUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaintenanceRceievedUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentToBranchUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSentToBranchUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentToMaintenanceUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSentToMaintenanceUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClientPhone);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPhone);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemBatch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblItemID);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBatch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSize);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboColors);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSentToMaintenance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentToMaintenanceDriverName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentToBranchDriver);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSentToMaintenanceDriver);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSentToBranchDriver);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkDeliveredToClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliveredToClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSentToMaintenance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSentToBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkMaintenanceReceived);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSentToBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkBranchReceived);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSentToBranchDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpMaintenanceReceived);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaintenanceReceivedDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBranchReceived);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchReceivedDate);
		base.Name = "frmProductMaintenance";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchReceivedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBranchReceived, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaintenanceReceivedDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpMaintenanceReceived, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentToBranchDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkBranchReceived, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpSentToBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkMaintenanceReceived, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSentToBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpSentToMaintenance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliveredToClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkDeliveredToClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSentToBranchDriver, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSentToMaintenanceDriver, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentToBranchDriver, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentToMaintenanceDriverName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClient, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpectedDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSentToMaintenance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboColors, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBatch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemID, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemSize, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblItemBatch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPhone, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClientPhone, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSentToMaintenanceUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentToMaintenanceUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSentToBranchUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSentToBranchUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaintenanceRceievedUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaintenanceReceivedUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchReceivedUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchReceivedUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDeliveredToClientUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveredToClientUser, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedDeliveryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSentToMaintenance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPhone).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboColors).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSize).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBatch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkMaintenanceReceived).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSentToBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkBranchReceived).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkDeliveredToClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSentToMaintenance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSentToMaintenanceDriver).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMaintenanceReceived).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSentToBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSentToBranchDriver).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBranchReceived).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliveredToClient).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSentToMaintenanceUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSentToBranchUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaintenanceRceievedUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchReceivedUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDeliveredToClientUsers).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
