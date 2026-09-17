using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using BusinessLayer.Sling;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Transactions;

public class frmQuotationsCertificates : frmHeaderDetails
{
	private DataTable dtQuotations;

	private DataTable dtQuotationDetails;

	private DataTable dtReports;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboQuotations;

	private UltraLabel lblQuotation;

	public UltraButton btnQuotationsSearch;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraDateTimeEditor dtpTestDate;

	private UltraLabel lblTestDate;

	private UltraDateTimeEditor dtpTestEndDate;

	private UltraLabel lblTestEndDate;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel3;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	private UltraComboEditor cboType;

	private UltraLabel ultraLabel7;

	private UltraDateTimeEditor dtpUnitTestDate;

	private UltraDateTimeEditor dtpSlingTestDate;

	private UltraTextEditor txtUnitCertificateNo;

	private UltraTextEditor txtSlingCertificateNo;

	private UltraTextEditor txtSlingTestedBy;

	private UltraTextEditor txtUnitTestedBy;

	public frmQuotationsCertificates()
	{
		InitializeComponent();
		TableName = "SLN_QuotationsCertificates";
		IDCol = "QuotationCertificateID";
		NoCol = "QuotationCertificateNo";
		DateCol = "QuotationCertificateDate";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		((Control)(object)lblQuotation).Text = GlobalFunctions.GetFormName("Sling", "Transactions", "frmSLNQuotations");
		dtQuotations = Quotations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboQuotations, dtQuotations, "QuotationID", "QuotationNo");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDetails = QuotationsCertificatesDetails.SelectByQuotationCertificateID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationCertificateDetailID"].DefaultCellValue = -1;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Get Description"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Get Description");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Get Description"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Get Description"].Header).Caption = (GlobalVariables.IsArabic ? "Get Description" : "Get Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Get Description"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Get Description"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Get Description"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Get Description"].Value = (GlobalVariables.IsArabic ? "Get Description" : "Get Description");
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Get Description"].Header).VisiblePosition = (GlobalVariables.IsArabic ? (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Index - 1) : ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Index);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsNewItem"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsNewItem"].Header).Caption = (GlobalVariables.IsArabic ? "صنف جديد" : "IsNewItem");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsNewItem"].DefaultCellValue = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsNewItem"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Item"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Item"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Item"].Width = (int)((double)((Control)(object)ULGData).Width * 0.14) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintedItemNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintedItemNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم طباعةالصنف" : "ID No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrintedItemNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "كمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AllowedQty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AllowedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية المتاحه" : "Allowed Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AllowedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Payload"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Payload"].Header).Caption = (GlobalVariables.IsArabic ? "Payload" : "Payload");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Payload"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Proofload"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Proofload"].Header).Caption = (GlobalVariables.IsArabic ? "Proofload" : "Proofload");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Proofload"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = QuotationsCertificates.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
			dtpTestDate.ValueChanged -= dtpTestDate_ValueChanged;
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			((TextEditorControlBase)cboQuotations).ValueChanged -= cboQuotations_ValueChanged;
			((Control)(object)txtCode).Text = drMaster["QuotationCertificateNo"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtUnitCertificateNo).Text = drMaster["UnitCertificateNo"].ToString();
			((Control)(object)txtUnitTestedBy).Text = drMaster["UnitTestBy"].ToString();
			((Control)(object)txtSlingCertificateNo).Text = drMaster["SlingCertificateNo"].ToString();
			((Control)(object)txtSlingTestedBy).Text = drMaster["SlingTestBy"].ToString();
			dtpUnitTestDate.Value = drMaster["UnitTestDate"];
			dtpSlingTestDate.Value = drMaster["SlingTestDate"];
			((TextEditorControlBase)cboType).Value = drMaster["QuotationCertificateTypeID"];
			dtpDate.Value = drMaster["QuotationCertificateDate"];
			dtpTestDate.Value = drMaster["CertificateTestDate"];
			dtpTestEndDate.Value = drMaster["CertificateTestEndDate"];
			((TextEditorControlBase)cboQuotations).Value = drMaster["QuotationID"];
			((TextEditorControlBase)cboQuotations).ValueChanged += cboQuotations_ValueChanged;
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			dtpTestDate.ValueChanged += dtpTestDate_ValueChanged;
			dtDetails = QuotationsCertificatesDetails.SelectByQuotationCertificateID(drMaster["QuotationCertificateID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((EditorButtonControlBase)dtpTestDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpTestEndDate).ReadOnly = true;
		((EditorButtonControlBase)dtpUnitTestDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpSlingTestDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtUnitCertificateNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSlingCertificateNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtUnitTestedBy).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSlingTestedBy).ReadOnly = NavMode;
		((Control)(object)btnQuotationsSearch).Visible = !NavMode;
		((EditorButtonControlBase)cboQuotations).ReadOnly = NavMode;
		((EditorButtonControlBase)cboType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		if (Adding)
		{
			DataView dataView = new DataView(dtQuotations);
			dataView.RowFilter = " Closed = 0  ";
			GlobalFunctions.FillCombo(cboQuotations, dataView.ToTable(), "QuotationID", "QuotationNo");
		}
		else
		{
			GlobalFunctions.FillCombo(cboQuotations, dtQuotations, "QuotationID", "QuotationNo");
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.ValueChanged -= dtpDate_ValueChanged;
		((TextEditorControlBase)cboQuotations).ValueChanged -= cboQuotations_ValueChanged;
		((TextEditorControlBase)txtCode).Clear();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? QuotationsCertificates.GetCodeByQuotationID((cboQuotations.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboQuotations).Value.ToString()) : "");
		dtpUnitTestDate.Value = null;
		dtpSlingTestDate.Value = null;
		((TextEditorControlBase)txtSlingTestedBy).Clear();
		((TextEditorControlBase)txtUnitTestedBy).Clear();
		((TextEditorControlBase)txtUnitCertificateNo).Clear();
		((TextEditorControlBase)txtSlingCertificateNo).Clear();
		cboType.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		dtpTestDate.Value = null;
		dtpTestEndDate.Value = null;
		cboQuotations.SelectedIndex = -1;
		dtDetails.Rows.Clear();
		((TextEditorControlBase)cboQuotations).ValueChanged += cboQuotations_ValueChanged;
		dtpDate.ValueChanged += dtpDate_ValueChanged;
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال تاريخ الشهادة" : "Please Enter The Certificate Date");
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
			GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you chose \n\r exists in a closed fisical period");
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم الشهادة" : "Please Enter The Certificate Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboQuotations.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار عرض" : "Please Select Quotation");
			((TextEditorControlBase)cboQuotations).Focus();
			cboQuotations.DropDown();
			return false;
		}
		if (cboType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار النوع" : "Please Select Type");
			((TextEditorControlBase)cboType).Focus();
			cboType.DropDown();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("SLN_QuotationsCertificates", "QuotationCertificateNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["QuotationCertificateNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByQuotationID = QuotationsCertificates.GetCodeByQuotationID((cboQuotations.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboQuotations).Value.ToString());
			GlobalVariables.QuestionMB.Show("رقم هذه الشهادة متواجدة من قبل \n سوف يتم الحفظ برقم " + codeByQuotationID, "The Certificate Number Already Exists It Will Be Saved With No. : " + codeByQuotationID);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = codeByQuotationID;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذه الشهادة", "Please insert details for this Certificate");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				ULGData.PerformAction((UltraGridAction)24);
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsNewItem"].Value.ToString()))
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["QuotationDetailID"].Value == DBNull.Value)
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("برجاء اختيار صنف جديد  ", "Please Check It As New Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["IsNewItem"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["AllowedQty"].Value.ToString()))
				{
					((Control)(object)ULGData).Enter -= ULGData_Enter;
					GlobalVariables.InformationMB.Show("الكمية اكبر من الكمية المتاحة", "The Quantity Is Greater Than The Allowed Qty");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					((Control)(object)ULGData).Enter += ULGData_Enter;
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = QuotationsCertificates.Insert_Update("-1", ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboType).Value.ToString(), (cboQuotations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotations).Value.ToString(), (dtpTestDate.Value == null) ? "Null" : dtpTestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpTestEndDate.Value == null) ? "Null" : dtpTestEndDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtUnitTestedBy).Text == "") ? "Null" : ((Control)(object)txtUnitTestedBy).Text, (((Control)(object)txtUnitCertificateNo).Text == "") ? "Null" : ((Control)(object)txtUnitCertificateNo).Text, (dtpUnitTestDate.Value == null) ? "Null" : dtpUnitTestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtSlingTestedBy).Text == "") ? "Null" : ((Control)(object)txtSlingTestedBy).Text, (((Control)(object)txtSlingCertificateNo).Text == "") ? "Null" : ((Control)(object)txtSlingCertificateNo).Text, (dtpSlingTestDate.Value == null) ? "Null" : dtpSlingTestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["QuotationCertificateDetailID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["QuotationCertificateID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				QuotationsCertificatesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			int num = QuotationsCertificates.Insert_Update(drMaster["QuotationCertificateID"].ToString(), ((Control)(object)txtCode).Text, (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboType).Value.ToString(), (cboQuotations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboQuotations).Value.ToString(), (dtpTestDate.Value == null) ? "Null" : dtpTestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpTestEndDate.Value == null) ? "Null" : dtpTestEndDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtUnitTestedBy).Text == "") ? "Null" : ((Control)(object)txtUnitTestedBy).Text, (((Control)(object)txtUnitCertificateNo).Text == "") ? "Null" : ((Control)(object)txtUnitCertificateNo).Text, (dtpUnitTestDate.Value == null) ? "Null" : dtpUnitTestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtSlingTestedBy).Text == "") ? "Null" : ((Control)(object)txtSlingTestedBy).Text, (((Control)(object)txtSlingCertificateNo).Text == "") ? "Null" : ((Control)(object)txtSlingCertificateNo).Text, (dtpSlingTestDate.Value == null) ? "Null" : dtpSlingTestDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["QuotationCertificateDetailID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["QuotationCertificateID"].Value = num.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("SLN_QuotationsCertificatesDetails", "QuotationCertificateID", num.ToString(), "QuotationCertificateDetailID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				QuotationsCertificatesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			QuotationsCertificatesDetails.DeleteVirtualByQuotationCertificateID(drMaster["QuotationCertificateID"].ToString(), GlobalVariables.UserID);
			QuotationsCertificates.DeleteVirtual(drMaster["QuotationCertificateID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Item" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AllowedQty")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void btnRefreshDataClick()
	{
	}

	public override void btnPrintClick()
	{
		if (!(RowID != ""))
		{
			return;
		}
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			if (((TextEditorControlBase)cboType).Value.ToString() == "1")
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Select("ProcedureName ='Visual'")[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ProcedureName ='Visual'")[0]["isoCode"].ToString());
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Select("ProcedureName = 'Test'")[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = ((dtReports.Rows.Count == 0) ? "" : dtReports.Select("ProcedureName ='Test'")[0]["isoCode"].ToString());
			}
		}
		else if (((TextEditorControlBase)cboType).Value.ToString() == "1")
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SLN_QuotationsCertificates_Visual_A.rpt" : "Rep_SLN_QuotationsCertificates_Visual_E.rpt"));
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_SLN_QuotationsCertificates_Test_A.rpt" : "Rep_SLN_QuotationsCertificates_Test_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@QuotationCertificateIDs", "," + RowID + ",");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.SLNQuotationsCertificatesReport("-1", 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["QuotationCertificateID"].ToString();
			FillData();
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
	}

	private void btnQuotationsSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.SLNQuotationsSearch(-1, 0, 0);
		if (num != 0)
		{
			((TextEditorControlBase)cboQuotations).Value = num;
		}
	}

	private void cboQuotations_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboQuotations).ValueChanged -= cboQuotations_ValueChanged;
		if (cboQuotations.SelectedIndex > -1)
		{
			((Control)(object)txtCode).Text = QuotationsCertificates.GetCodeByQuotationID((cboQuotations.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboQuotations).Value.ToString());
			dtQuotationDetails = QuotationsCertificatesDetails.FillByQuotationID(((TextEditorControlBase)cboQuotations).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtQuotationDetails.Copy();
		}
		InitGrid();
		((TextEditorControlBase)cboQuotations).ValueChanged += cboQuotations_ValueChanged;
	}

	private void dtpTestDate_ValueChanged(object sender, EventArgs e)
	{
		dtpTestDate.ValueChanged -= dtpTestDate_ValueChanged;
		if (dtpTestDate.Value != null)
		{
			dtpTestEndDate.Value = dtpTestDate.DateTime.AddMonths(6);
		}
		dtpTestDate.ValueChanged += dtpTestDate_ValueChanged;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if ((Adding || Updating) && ((KeyedSubObjectBase)e.Cell.Column).Key == "Get Description" && ((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmQuotationsCertificatesInquiry frmQuotationsCertificatesInquiry2 = new frmQuotationsCertificatesInquiry(returnData: true);
			frmQuotationsCertificatesInquiry2.ShowDialog();
			e.Cell.Row.Cells["Notes"].Value = frmQuotationsCertificatesInquiry2.selectedDescription;
		}
	}

	public override void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		dtDetails.Rows.Clear();
		SetControls(NavMode: false);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6 && (Adding || Updating) && ((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Notes")
		{
			frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "الوصف" : "Description", _IsInt: false, _IsNumeric: false, ULGData.ActiveCell.Value.ToString());
			frmEnterValue2.WindowState = FormWindowState.Normal;
			if (frmEnterValue2.ShowDialog() == DialogResult.OK)
			{
				ULGData.ActiveCell.Value = frmEnterValue2.Value;
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
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0548: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.Transactions.frmQuotationsCertificates));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		ValueListItem val10 = new ValueListItem();
		ValueListItem val11 = new ValueListItem();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboQuotations = new UltraComboEditor();
		this.lblQuotation = new UltraLabel();
		this.btnQuotationsSearch = new UltraButton();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.dtpTestDate = new UltraDateTimeEditor();
		this.lblTestDate = new UltraLabel();
		this.dtpTestEndDate = new UltraDateTimeEditor();
		this.lblTestEndDate = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.cboType = new UltraComboEditor();
		this.ultraLabel7 = new UltraLabel();
		this.dtpUnitTestDate = new UltraDateTimeEditor();
		this.dtpSlingTestDate = new UltraDateTimeEditor();
		this.txtUnitCertificateNo = new UltraTextEditor();
		this.txtSlingCertificateNo = new UltraTextEditor();
		this.txtSlingTestedBy = new UltraTextEditor();
		this.txtUnitTestedBy = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTestDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTestEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpUnitTestDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSlingTestDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitCertificateNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSlingCertificateNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSlingTestedBy).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitTestedBy).BeginInit();
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
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
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
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		resources.ApplyResources(this.cboQuotations, "cboQuotations");
		this.cboQuotations.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboQuotations).Name = "cboQuotations";
		((TextEditorControlBase)this.cboQuotations).ValueChanged += new System.EventHandler(cboQuotations_ValueChanged);
		resources.ApplyResources(this.lblQuotation, "lblQuotation");
		this.lblQuotation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuotation).Name = "lblQuotation";
		((ControlBase)this.lblQuotation).WrapText = false;
		resources.ApplyResources(this.btnQuotationsSearch, "btnQuotationsSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)this.btnQuotationsSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnQuotationsSearch).Name = "btnQuotationsSearch";
		((System.Windows.Forms.Control)(object)this.btnQuotationsSearch).Click += new System.EventHandler(btnQuotationsSearch_Click);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.dtpTestDate, "dtpTestDate");
		((UltraWinEditorMaskedControlBase)this.dtpTestDate).AlwaysInEditMode = true;
		this.dtpTestDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpTestDate).Name = "dtpTestDate";
		this.dtpTestDate.ValueChanged += new System.EventHandler(dtpTestDate_ValueChanged);
		resources.ApplyResources(this.lblTestDate, "lblTestDate");
		this.lblTestDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTestDate).Name = "lblTestDate";
		((ControlBase)this.lblTestDate).WrapText = false;
		resources.ApplyResources(this.dtpTestEndDate, "dtpTestEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpTestEndDate).AlwaysInEditMode = true;
		this.dtpTestEndDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpTestEndDate).Name = "dtpTestEndDate";
		resources.ApplyResources(this.lblTestEndDate, "lblTestEndDate");
		this.lblTestEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTestEndDate).Name = "lblTestEndDate";
		((ControlBase)this.lblTestEndDate).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.cboType, "cboType");
		this.cboType.AutoCompleteMode = (AutoCompleteMode)4;
		val10.DataValue = "1";
		resources.ApplyResources(val10, "valueListItem1");
		((SubObjectBase)val10).ForceApplyResources = "";
		val11.DataValue = "2";
		resources.ApplyResources(val11, "valueListItem2");
		((SubObjectBase)val11).ForceApplyResources = "";
		this.cboType.Items.AddRange((ValueListItem[])(object)new ValueListItem[2] { val10, val11 });
		((System.Windows.Forms.Control)(object)this.cboType).Name = "cboType";
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.dtpUnitTestDate, "dtpUnitTestDate");
		((UltraWinEditorMaskedControlBase)this.dtpUnitTestDate).AlwaysInEditMode = true;
		this.dtpUnitTestDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpUnitTestDate).Name = "dtpUnitTestDate";
		resources.ApplyResources(this.dtpSlingTestDate, "dtpSlingTestDate");
		((UltraWinEditorMaskedControlBase)this.dtpSlingTestDate).AlwaysInEditMode = true;
		this.dtpSlingTestDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpSlingTestDate).Name = "dtpSlingTestDate";
		resources.ApplyResources(this.txtUnitCertificateNo, "txtUnitCertificateNo");
		((System.Windows.Forms.Control)(object)this.txtUnitCertificateNo).Name = "txtUnitCertificateNo";
		resources.ApplyResources(this.txtSlingCertificateNo, "txtSlingCertificateNo");
		((System.Windows.Forms.Control)(object)this.txtSlingCertificateNo).Name = "txtSlingCertificateNo";
		resources.ApplyResources(this.txtSlingTestedBy, "txtSlingTestedBy");
		((System.Windows.Forms.Control)(object)this.txtSlingTestedBy).Name = "txtSlingTestedBy";
		resources.ApplyResources(this.txtUnitTestedBy, "txtUnitTestedBy");
		((System.Windows.Forms.Control)(object)this.txtUnitTestedBy).Name = "txtUnitTestedBy";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSlingTestedBy);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnitTestedBy);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSlingCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnitCertificateNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSlingTestDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpUnitTestDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboQuotations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuotation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnQuotationsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTestEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTestDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpTestEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpTestDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmQuotationsCertificates";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpTestDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpTestEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTestDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTestEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnQuotationsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuotation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboQuotations, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpUnitTestDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpSlingTestDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnitCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSlingCertificateNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnitTestedBy, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSlingTestedBy, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboQuotations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTestDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTestEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpUnitTestDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSlingTestDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitCertificateNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSlingCertificateNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSlingTestedBy).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnitTestedBy).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
