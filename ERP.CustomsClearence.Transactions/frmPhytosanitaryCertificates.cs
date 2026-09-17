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

public class frmPhytosanitaryCertificates : frmHeaderDetails
{
	private DataTable dtOperations;

	private DataTable dtReports;

	private DataTable dtBillsOfLading;

	private DataTable dtChemicalActiveIngredients;

	private DataTable dtTreatments;

	private DataTable dtConcentrations;

	private DataTable dtDurationAndTemperatures;

	private DataTable dtAdditionalDeclarations;

	private string OperationID = "0";

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblOperationsNo;

	private UltraComboEditor cboOperationNo;

	public UltraButton btnOperationNoSearch;

	private UltraLabel lblTreatment;

	private UltraTextEditor txtInspectorName;

	private UltraTextEditor txtConcentration;

	private UltraTextEditor txtDurationAndTemperature;

	private UltraLabel lblConcentration;

	private UltraLabel lblDurationAndTemperature;

	private UltraLabel lblInspectorsNames;

	private UltraLabel lblChemicalActiveIngredients;

	private UltraLabel lblBillOfLading;

	private UltraComboEditor cboBillOfLading;

	private UltraDateTimeEditor dtpInspectionDate;

	private UltraLabel lblInspectionDate;

	private UltraLabel lblTreatmentDate;

	private UltraDateTimeEditor dtpTreatmentDate;

	private UltraTextEditor txtAdditionalInformation;

	private UltraLabel lblAdditionalInformation;

	private UltraTextEditor txtAdditionalDeclaration;

	private UltraLabel lblAdditionalDeclaration;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraComboEditor cboTreatment;

	private UltraComboEditor cboChemicalActiveIngredients;

	public UltraButton btnConcentrationSearch;

	public UltraButton btnDurationAndTemperatureSearch;

	public UltraButton btnAdditionalDeclarationSearch;

	public frmPhytosanitaryCertificates()
	{
		InitializeComponent();
		TableName = "CST_PhytosanitaryCertificates";
		IDCol = "PhytosanitaryCertificateID";
		NoCol = "PhytosanitaryCertificateNo";
		DateCol = "PhytosanitaryCertificateDate";
	}

	public frmPhytosanitaryCertificates(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public frmPhytosanitaryCertificates(string OPERATIONID)
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
		dtChemicalActiveIngredients = ChemicalActiveIngredients.FillCombo("-1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboChemicalActiveIngredients, dtChemicalActiveIngredients, "ChemicalActiveIngredientID", "Description");
		dtTreatments = Treatments.FillCombo("-1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTreatment, dtTreatments, "TreatmentID", "Description");
		dtConcentrations = Concentrations.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDurationAndTemperatures = DurationAndTemperatures.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtAdditionalDeclarations = AdditionalDeclarations.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDetails = PhytosanitaryCertificatesItems.SelectByPhytosanitaryCertificateID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhytosanitaryCertificateItemID"].DefaultCellValue = -1;
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
			btnAddClick();
			((TextEditorControlBase)cboOperationNo).Value = OperationID;
		}
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = PhytosanitaryCertificates.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["PhytosanitaryCertificateNo"].ToString();
			dtpDate.ValueChanged -= dtpDate_ValueChanged;
			dtpDate.Value = (DateTime)drMaster["PhytosanitaryCertificateDate"];
			dtpDate.ValueChanged += dtpDate_ValueChanged;
			dtpInspectionDate.Value = drMaster["InspectionDate"];
			dtpTreatmentDate.Value = drMaster["TreatmentDate"];
			((TextEditorControlBase)cboOperationNo).Value = drMaster["OperationID"];
			((TextEditorControlBase)cboBillOfLading).Value = drMaster["BillOfLadingID"];
			((Control)(object)txtInspectorName).Text = drMaster["InspectorsNames"].ToString();
			((TextEditorControlBase)cboTreatment).Value = drMaster["TreatmentID"].ToString();
			((Control)(object)txtDurationAndTemperature).Text = drMaster["DurationAndTemperature"].ToString();
			((Control)(object)txtConcentration).Text = drMaster["Concentration"].ToString();
			((Control)(object)txtAdditionalDeclaration).Text = drMaster["AdditionalDeclaration"].ToString();
			((Control)(object)txtAdditionalInformation).Text = drMaster["AdditionalInformation"].ToString();
			((TextEditorControlBase)cboChemicalActiveIngredients).Value = drMaster["ChemicalActiveIngredientID"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = PhytosanitaryCertificatesItems.SelectByPhytosanitaryCertificateID(drMaster["PhytosanitaryCertificateID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
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
		((EditorButtonControlBase)dtpTreatmentDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpInspectionDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInspectorName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTreatment).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDurationAndTemperature).ReadOnly = NavMode;
		((EditorButtonControlBase)txtConcentration).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAdditionalInformation).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAdditionalDeclaration).ReadOnly = NavMode;
		((EditorButtonControlBase)cboChemicalActiveIngredients).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboOperationNo).ReadOnly = NavMode || Updating || OperationID != "0";
		((EditorButtonControlBase)cboBillOfLading).ReadOnly = NavMode;
		((Control)(object)btnOperationNoSearch).Visible = Adding && OperationID == "0";
		((Control)(object)btnAdditionalDeclarationSearch).Visible = !NavMode;
		((Control)(object)btnConcentrationSearch).Visible = !NavMode;
		((Control)(object)btnDurationAndTemperatureSearch).Visible = !NavMode;
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
		dtpInspectionDate.Value = DBNull.Value;
		dtpTreatmentDate.Value = DBNull.Value;
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		cboOperationNo.SelectedIndex = -1;
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
		cboBillOfLading.SelectedIndex = -1;
		((Control)(object)txtCode).Text = (Adding ? PhytosanitaryCertificates.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		((TextEditorControlBase)txtInspectorName).Clear();
		cboTreatment.SelectedIndex = -1;
		((TextEditorControlBase)txtDurationAndTemperature).Clear();
		((TextEditorControlBase)txtConcentration).Clear();
		((TextEditorControlBase)txtAdditionalDeclaration).Clear();
		((TextEditorControlBase)txtAdditionalInformation).Clear();
		cboChemicalActiveIngredients.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
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
		if (Main.CheckForValueByBranchIDAndFiscalYearID("CST_PhytosanitaryCertificates", "PhytosanitaryCertificateNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["PhytosanitaryCertificateNo"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = PhytosanitaryCertificates.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			int num = PhytosanitaryCertificates.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), (cboBillOfLading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBillOfLading).Value.ToString(), (dtpInspectionDate.Value == null) ? "Null" : dtpInspectionDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtInspectorName).Text == "") ? "Null" : ((Control)(object)txtInspectorName).Text, (dtpTreatmentDate.Value == null) ? "Null" : dtpTreatmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboTreatment.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTreatment).Value.ToString(), (cboChemicalActiveIngredients.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboChemicalActiveIngredients).Value.ToString(), (((Control)(object)txtConcentration).Text == "") ? "Null" : ((Control)(object)txtConcentration).Text, (((Control)(object)txtDurationAndTemperature).Text == "") ? "Null" : ((Control)(object)txtDurationAndTemperature).Text, (((Control)(object)txtAdditionalInformation).Text == "") ? "Null" : ((Control)(object)txtAdditionalInformation).Text, (((Control)(object)txtAdditionalDeclaration).Text == "") ? "Null" : ((Control)(object)txtAdditionalDeclaration).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["PhytosanitaryCertificateItemID"].Value = -1;
					((UltraGridBase)ULGData).Rows[i].Cells["PhytosanitaryCertificateID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				}
				PhytosanitaryCertificatesItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			int num = PhytosanitaryCertificates.Insert_Update(drMaster["PhytosanitaryCertificateID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboOperationNo.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboOperationNo).Value.ToString(), (cboBillOfLading.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBillOfLading).Value.ToString(), (dtpInspectionDate.Value == null) ? "Null" : dtpInspectionDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtInspectorName).Text == "") ? "Null" : ((Control)(object)txtInspectorName).Text, (dtpTreatmentDate.Value == null) ? "Null" : dtpTreatmentDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboTreatment.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTreatment).Value.ToString(), (cboChemicalActiveIngredients.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboChemicalActiveIngredients).Value.ToString(), (((Control)(object)txtConcentration).Text == "") ? "Null" : ((Control)(object)txtConcentration).Text, (((Control)(object)txtDurationAndTemperature).Text == "") ? "Null" : ((Control)(object)txtDurationAndTemperature).Text, (((Control)(object)txtAdditionalInformation).Text == "") ? "Null" : ((Control)(object)txtAdditionalInformation).Text, (((Control)(object)txtAdditionalDeclaration).Text == "") ? "Null" : ((Control)(object)txtAdditionalDeclaration).Text, (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				string text = ",";
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["PhytosanitaryCertificateID"].Value = num;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["PhytosanitaryCertificateItemID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("CST_PhytosanitaryCertificatesItems", "PhytosanitaryCertificateID", drMaster["PhytosanitaryCertificateID"].ToString(), "PhytosanitaryCertificateItemID", text);
				PhytosanitaryCertificatesItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
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
			PhytosanitaryCertificates.DeleteVirtual(drMaster["PhytosanitaryCertificateID"].ToString(), GlobalVariables.UserID);
			PhytosanitaryCertificatesItems.DeleteVirtualByPhytosanitaryCertificateID(drMaster["PhytosanitaryCertificateID"].ToString(), GlobalVariables.UserID);
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CST_PhytosanitaryCertificates.rpt" : "Rep_CST_PhytosanitaryCertificates.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@OperationIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@PhytosanitaryCertificateIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CSTPhytosanitaryCertificatesReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["PhytosanitaryCertificateID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtOperations = Operations.FillCombo(GlobalVariables.BranchIDs);
		GlobalFunctions.FillCombo(cboOperationNo, dtOperations, "OperationID", "OperationNo");
		dtBillsOfLading = BillsOfLading.FillCombo(GlobalVariables.BranchIDs);
		dtChemicalActiveIngredients = ChemicalActiveIngredients.FillCombo("-1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboChemicalActiveIngredients, dtChemicalActiveIngredients, "ChemicalActiveIngredientID", "Description");
		dtTreatments = Treatments.FillCombo("-1", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTreatment, dtTreatments, "TreatmentID", "Description");
		dtConcentrations = Concentrations.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDurationAndTemperatures = DurationAndTemperatures.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtAdditionalDeclarations = AdditionalDeclarations.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void btnConcentrationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTConcentrationsSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((Control)(object)txtConcentration).Text += dtConcentrations.Select("ConcentrationID = " + num)[0]["Description"].ToString();
		}
	}

	private void btnDurationAndTemperatureSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTDurationAndTemperaturesSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((Control)(object)txtDurationAndTemperature).Text += dtDurationAndTemperatures.Select("DurationAndTemperatureID = " + num)[0]["Description"].ToString();
		}
	}

	private void btnAdditionalDeclarationSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CSTAdditionalDeclarationsSearch(0, IsFromServer: false);
		if (num != 0)
		{
			((Control)(object)txtAdditionalDeclaration).Text += dtAdditionalDeclarations.Select("AdditionalDeclarationID = " + num)[0]["Description"].ToString();
		}
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
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = PhytosanitaryCertificates.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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

	private void cboOperationNo_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboOperationNo).ValueChanged -= cboOperationNo_ValueChanged;
		if (cboOperationNo.SelectedIndex > -1)
		{
			dtDetails = PhytosanitaryCertificatesItems.FillByOperationID(((TextEditorControlBase)cboOperationNo).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			BillsFilter();
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
		((TextEditorControlBase)cboOperationNo).ValueChanged += cboOperationNo_ValueChanged;
	}

	public void BillsFilter()
	{
		DataView dataView = new DataView(dtBillsOfLading);
		dataView.RowFilter = "OperationID = " + ((((TextEditorControlBase)cboOperationNo).Value == null) ? "-1" : ((TextEditorControlBase)cboOperationNo).Value.ToString());
		cboBillOfLading.DataSource = dataView;
		cboBillOfLading.DisplayMember = "BillOfLadingNumber";
		cboBillOfLading.ValueMember = "BillOfLadingID";
		cboBillOfLading.SelectedIndex = ((dataView.Count <= 0) ? (-1) : 0);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Transactions.frmPhytosanitaryCertificates));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblOperationsNo = new UltraLabel();
		this.cboOperationNo = new UltraComboEditor();
		this.btnOperationNoSearch = new UltraButton();
		this.lblTreatment = new UltraLabel();
		this.txtInspectorName = new UltraTextEditor();
		this.txtConcentration = new UltraTextEditor();
		this.txtDurationAndTemperature = new UltraTextEditor();
		this.lblConcentration = new UltraLabel();
		this.lblDurationAndTemperature = new UltraLabel();
		this.lblInspectorsNames = new UltraLabel();
		this.lblChemicalActiveIngredients = new UltraLabel();
		this.lblBillOfLading = new UltraLabel();
		this.cboBillOfLading = new UltraComboEditor();
		this.dtpInspectionDate = new UltraDateTimeEditor();
		this.lblInspectionDate = new UltraLabel();
		this.lblTreatmentDate = new UltraLabel();
		this.dtpTreatmentDate = new UltraDateTimeEditor();
		this.txtAdditionalInformation = new UltraTextEditor();
		this.lblAdditionalInformation = new UltraLabel();
		this.txtAdditionalDeclaration = new UltraTextEditor();
		this.lblAdditionalDeclaration = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.cboTreatment = new UltraComboEditor();
		this.cboChemicalActiveIngredients = new UltraComboEditor();
		this.btnConcentrationSearch = new UltraButton();
		this.btnDurationAndTemperatureSearch = new UltraButton();
		this.btnAdditionalDeclarationSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInspectorName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConcentration).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDurationAndTemperature).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBillOfLading).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpInspectionDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTreatmentDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdditionalInformation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdditionalDeclaration).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTreatment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboChemicalActiveIngredients).BeginInit();
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
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.lblHistory, "lblHistory");
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
		this.lblTreatment.AutoEllipsis = false;
		resources.ApplyResources(this.lblTreatment, "lblTreatment");
		((System.Windows.Forms.Control)(object)this.lblTreatment).Name = "lblTreatment";
		((ControlBase)this.lblTreatment).WrapText = false;
		resources.ApplyResources(this.txtInspectorName, "txtInspectorName");
		((System.Windows.Forms.Control)(object)this.txtInspectorName).Name = "txtInspectorName";
		resources.ApplyResources(this.txtConcentration, "txtConcentration");
		((System.Windows.Forms.Control)(object)this.txtConcentration).Name = "txtConcentration";
		resources.ApplyResources(this.txtDurationAndTemperature, "txtDurationAndTemperature");
		((System.Windows.Forms.Control)(object)this.txtDurationAndTemperature).Name = "txtDurationAndTemperature";
		this.lblConcentration.AutoEllipsis = false;
		resources.ApplyResources(this.lblConcentration, "lblConcentration");
		((System.Windows.Forms.Control)(object)this.lblConcentration).Name = "lblConcentration";
		((ControlBase)this.lblConcentration).WrapText = false;
		this.lblDurationAndTemperature.AutoEllipsis = false;
		resources.ApplyResources(this.lblDurationAndTemperature, "lblDurationAndTemperature");
		((System.Windows.Forms.Control)(object)this.lblDurationAndTemperature).Name = "lblDurationAndTemperature";
		((ControlBase)this.lblDurationAndTemperature).WrapText = false;
		this.lblInspectorsNames.AutoEllipsis = false;
		resources.ApplyResources(this.lblInspectorsNames, "lblInspectorsNames");
		((System.Windows.Forms.Control)(object)this.lblInspectorsNames).Name = "lblInspectorsNames";
		((ControlBase)this.lblInspectorsNames).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblInspectorsNames).Click += new System.EventHandler(lblBillOfLadingNo_Click);
		this.lblChemicalActiveIngredients.AutoEllipsis = false;
		resources.ApplyResources(this.lblChemicalActiveIngredients, "lblChemicalActiveIngredients");
		((System.Windows.Forms.Control)(object)this.lblChemicalActiveIngredients).Name = "lblChemicalActiveIngredients";
		((ControlBase)this.lblChemicalActiveIngredients).WrapText = false;
		this.lblBillOfLading.AutoEllipsis = false;
		resources.ApplyResources(this.lblBillOfLading, "lblBillOfLading");
		((System.Windows.Forms.Control)(object)this.lblBillOfLading).Name = "lblBillOfLading";
		((System.Windows.Forms.Control)(object)this.lblBillOfLading).Tag = "";
		((ControlBase)this.lblBillOfLading).WrapText = false;
		this.cboBillOfLading.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboBillOfLading, "cboBillOfLading");
		((System.Windows.Forms.Control)(object)this.cboBillOfLading).Name = "cboBillOfLading";
		((UltraWinEditorMaskedControlBase)this.dtpInspectionDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpInspectionDate, "dtpInspectionDate");
		this.dtpInspectionDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpInspectionDate).Name = "dtpInspectionDate";
		this.lblInspectionDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblInspectionDate, "lblInspectionDate");
		((System.Windows.Forms.Control)(object)this.lblInspectionDate).Name = "lblInspectionDate";
		((ControlBase)this.lblInspectionDate).WrapText = false;
		this.lblTreatmentDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblTreatmentDate, "lblTreatmentDate");
		((System.Windows.Forms.Control)(object)this.lblTreatmentDate).Name = "lblTreatmentDate";
		((ControlBase)this.lblTreatmentDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpTreatmentDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpTreatmentDate, "dtpTreatmentDate");
		this.dtpTreatmentDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpTreatmentDate).Name = "dtpTreatmentDate";
		resources.ApplyResources(this.txtAdditionalInformation, "txtAdditionalInformation");
		((System.Windows.Forms.Control)(object)this.txtAdditionalInformation).Name = "txtAdditionalInformation";
		this.lblAdditionalInformation.AutoEllipsis = false;
		resources.ApplyResources(this.lblAdditionalInformation, "lblAdditionalInformation");
		((System.Windows.Forms.Control)(object)this.lblAdditionalInformation).Name = "lblAdditionalInformation";
		((ControlBase)this.lblAdditionalInformation).WrapText = false;
		resources.ApplyResources(this.txtAdditionalDeclaration, "txtAdditionalDeclaration");
		((System.Windows.Forms.Control)(object)this.txtAdditionalDeclaration).Name = "txtAdditionalDeclaration";
		this.lblAdditionalDeclaration.AutoEllipsis = false;
		resources.ApplyResources(this.lblAdditionalDeclaration, "lblAdditionalDeclaration");
		((System.Windows.Forms.Control)(object)this.lblAdditionalDeclaration).Name = "lblAdditionalDeclaration";
		((ControlBase)this.lblAdditionalDeclaration).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		this.cboTreatment.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTreatment, "cboTreatment");
		((System.Windows.Forms.Control)(object)this.cboTreatment).Name = "cboTreatment";
		this.cboChemicalActiveIngredients.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboChemicalActiveIngredients, "cboChemicalActiveIngredients");
		((System.Windows.Forms.Control)(object)this.cboChemicalActiveIngredients).Name = "cboChemicalActiveIngredients";
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnConcentrationSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnConcentrationSearch, "btnConcentrationSearch");
		((System.Windows.Forms.Control)(object)this.btnConcentrationSearch).Name = "btnConcentrationSearch";
		((System.Windows.Forms.Control)(object)this.btnConcentrationSearch).Click += new System.EventHandler(btnConcentrationSearch_Click);
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnDurationAndTemperatureSearch).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(this.btnDurationAndTemperatureSearch, "btnDurationAndTemperatureSearch");
		((System.Windows.Forms.Control)(object)this.btnDurationAndTemperatureSearch).Name = "btnDurationAndTemperatureSearch";
		((System.Windows.Forms.Control)(object)this.btnDurationAndTemperatureSearch).Click += new System.EventHandler(btnDurationAndTemperatureSearch_Click);
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnAdditionalDeclarationSearch).Appearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.btnAdditionalDeclarationSearch, "btnAdditionalDeclarationSearch");
		((System.Windows.Forms.Control)(object)this.btnAdditionalDeclarationSearch).Name = "btnAdditionalDeclarationSearch";
		((System.Windows.Forms.Control)(object)this.btnAdditionalDeclarationSearch).Click += new System.EventHandler(btnAdditionalDeclarationSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboChemicalActiveIngredients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTreatment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdditionalInformation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdditionalInformation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdditionalDeclaration);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdditionalDeclaration);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpTreatmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTreatmentDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpInspectionDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInspectionDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblChemicalActiveIngredients);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBillOfLading);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBillOfLading);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTreatment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInspectorName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConcentration);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDurationAndTemperature);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConcentration);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDurationAndTemperature);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInspectorsNames);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperationsNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOperationNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAdditionalDeclarationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDurationAndTemperatureSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnConcentrationSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOperationNoSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmPhytosanitaryCertificates";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnConcentrationSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDurationAndTemperatureSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAdditionalDeclarationSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOperationNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperationsNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInspectorsNames, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDurationAndTemperature, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConcentration, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDurationAndTemperature, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConcentration, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInspectorName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTreatment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBillOfLading, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBillOfLading, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblChemicalActiveIngredients, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInspectionDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpInspectionDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTreatmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpTreatmentDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdditionalDeclaration, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdditionalDeclaration, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdditionalInformation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdditionalInformation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTreatment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboChemicalActiveIngredients, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOperationNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInspectorName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConcentration).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDurationAndTemperature).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBillOfLading).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpInspectionDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTreatmentDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdditionalInformation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdditionalDeclaration).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTreatment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboChemicalActiveIngredients).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
