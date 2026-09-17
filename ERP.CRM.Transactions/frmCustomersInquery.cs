using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.CRM;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.CRM.MasterData;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CRM.Transactions;

public class frmCustomersInquery : frmButtons
{
	private DataRow drMaster;

	private DataTable dtCustomerQuestions;

	private DataTable dtDetails;

	private DataTable dtSteps;

	private DataTable dtCustomerItems;

	private DataTable dtSegmentations;

	private DataTable dtGender;

	private DataTable dtReligions;

	private DataTable dtPaymentMethod;

	private DataTable dtPriceType;

	private DataTable dtBranches;

	private DataTable dtSalesMan;

	private DataTable dtUsers;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtBatchs;

	private DataTable dtUnits;

	private DataTable dtTaxes;

	private DataTable dtQuestions;

	private ValueList vlTrueFalseAnswers = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private bool AutomaticlyAddItemTaxToSalesInvoice = false;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlTaxes = new ValueList();

	private ValueList vlQuestions = new ValueList();

	private bool CanAddCustomer;

	private bool CanUpdateCustomer;

	private bool CanAddFollowUp;

	private ValueList vlSalesmen = new ValueList();

	private ValueList vlSteps = new ValueList();

	public int CustomerID = 0;

	private IContainer components = null;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameAr;

	private UltraComboEditor cboGender;

	private UltraLabel lblGender;

	private UltraTextEditor txtEMail;

	private UltraLabel lblEMail;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	public UltraGroupBox UGBDetails;

	public UltraGrid ULGData;

	private UltraComboEditor cboReligion;

	private UltraLabel lblReligion;

	private UltraTextEditor txtTel;

	private UltraLabel lblTel;

	public UltraGrid ULGCustQuestions;

	public UltraGrid ULGCustItems;

	private UltraComboEditor cboPriceType;

	private UltraComboEditor cboDefaultPaymentMethod;

	private UltraLabel lblPriceType;

	private UltraLabel lblDefaultPaymentMethod;

	private UltraDateTimeEditor dtpCreationDate;

	private UltraLabel lblCreationDate;

	private UltraLabel lblEmployeeSubAccount;

	private UltraComboEditor cboDefaultSalesMan;

	private UltraComboEditor cboSegmentations;

	private UltraLabel lblSegmentation;

	public UltraButton btnClientAdd;

	public UltraButton btnFollowAdd;

	public UltraGroupBox UGBFollowUp;

	public UltraGroupBox UGBQuestions;

	public UltraGroupBox UGBItems;

	public UltraGroupBox ultraGroupBox1;

	public UltraGroupBox ultraGroupBox2;

	private UltraTextEditor txtPhoneNumber;

	private UltraTextEditor txtMobile;

	private UltraLabel lblPhoneNumber;

	private UltraTextEditor txtMobileNumber2;

	private UltraLabel lblReorder;

	private UltraLabel lblMobileNumber2;

	private void btnFollowAdd_Click(object sender, EventArgs e)
	{
		if (drMaster != null && CanAddFollowUp)
		{
			frmCustomersFollows frmCustomersFollows2 = new frmCustomersFollows(int.Parse(drMaster["CustomerID"].ToString()));
			frmCustomersFollows2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmCustomersFollows2.lblTitle).Text = (GlobalVariables.IsArabic ? "متابعة العملاء" : "Customers Follows");
			frmCustomersFollows2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.Transactions.frmCustomersFollows'")[0];
			frmCustomersFollows2.ShowDialog();
			FillData();
		}
	}

	private void btnClientAdd_Click(object sender, EventArgs e)
	{
		if (drMaster == null && CanAddCustomer)
		{
			frmCustomersTree frmCustomersTree2 = new frmCustomersTree(-1, 0);
			frmCustomersTree2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmCustomersTree2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Customers");
			frmCustomersTree2.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.MasterData.frmCustomersTree'")[0];
			frmCustomersTree2.ShowDialog();
			if (frmCustomersTree2.CustomerID == -1)
			{
				return;
			}
			CustomerID = frmCustomersTree2.CustomerID;
			DataTable dataTable = Customers.Select(CustomerID.ToString(), "," + GlobalVariables.CurrentBranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				if (!bool.Parse(dataTable.Rows[0]["IsClosed"].ToString()))
				{
					RowID = dataTable.Rows[0][IDCol].ToString();
					CustomerID = int.Parse(RowID);
				}
				else
				{
					RowID = "";
					GlobalVariables.InformationMB.Show("لا يمكن اظهار البيانات الخاصه بهذا العميل لانه غير نشط", "InActive Client");
					dtDetails.Rows.Clear();
					dtCustomerQuestions.Rows.Clear();
					dtCustomerItems.Rows.Clear();
				}
				dtSearchResult = null;
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
		else if (CanUpdateCustomer)
		{
			frmCustomersTree frmCustomersTree3 = new frmCustomersTree(int.Parse(RowID), 2);
			frmCustomersTree3.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmCustomersTree3.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Customers");
			frmCustomersTree3.Tag = GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.MasterData.frmCustomersTree'")[0];
			frmCustomersTree3.ShowDialog();
			FillData();
		}
	}

	private void ULGCustItems_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
	}

	private void ULGCustQuestions_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCustQuestions).ActiveRow).Selected = true;
		}
	}

	public frmCustomersInquery()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		InitializeComponent();
		TableName = "CRM_Customers";
		IDCol = "CustomerID";
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Enabled = CanSearching);
		bool enabled = (((Control)(object)obj2).Enabled = flag);
		((Control)(object)obj).Enabled = enabled;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating && e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
		{
			btnSearch_Click(null, null);
		}
	}

	public override void SetControls(bool NavMode)
	{
		((Control)(object)btnAdd).Visible = false;
		((Control)(object)btnUpdate).Visible = false;
		((Control)(object)btnClose).Visible = true;
		((Control)(object)btnRefreshData).Visible = true;
		((Control)(object)btnOK).Visible = false;
		((Control)(object)btnSaveClose).Visible = false;
		((Control)(object)btnCancel).Visible = false;
		((Control)(object)btnClientAdd).Visible = CanAdd;
		((Control)(object)btnFollowAdd).Visible = CanAdd;
		((Control)(object)btnClientAdd).Enabled = CanAddCustomer || CanUpdateCustomer;
		((Control)(object)btnFollowAdd).Enabled = CanAddFollowUp;
		((Control)(object)txtCode).Enabled = false;
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = false;
		((Control)(object)btnCopyTo).Visible = false;
		((Control)(object)btnPriveous).Visible = false;
		((Control)(object)btnDelete).Visible = false;
		((Control)(object)btnPrint).Visible = false;
		((EditorButtonControlBase)txtNameAr).ReadOnly = true;
		((EditorButtonControlBase)txtNameEn).ReadOnly = true;
		((EditorButtonControlBase)cboGender).ReadOnly = true;
		((EditorButtonControlBase)cboReligion).ReadOnly = true;
		((EditorButtonControlBase)txtEMail).ReadOnly = true;
		((EditorButtonControlBase)txtNotes).ReadOnly = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		vlTrueFalseAnswers.ValueListItems.Clear();
		vlTrueFalseAnswers.ValueListItems.Add((object)"", GlobalVariables.IsArabic ? "" : "");
		vlTrueFalseAnswers.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "نعم" : "Yes");
		vlTrueFalseAnswers.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "لا" : "No");
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransactionBranch, dtBranches, "BranchID", "BranchName");
		dtSegmentations = Segmentations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSegmentations, dtSegmentations, "SegmentationID", "SegmentationName");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDefaultSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		vlSalesmen.ValueListItems.Clear();
		for (int i = 0; i < dtSalesMan.Rows.Count; i++)
		{
			vlSalesmen.ValueListItems.Add(dtSalesMan.Rows[i]["SubAccountID"], dtSalesMan.Rows[i]["SubAccountName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtSteps = CommunicationSteps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSteps.ValueListItems.Clear();
		for (int j = 0; j < dtSteps.Rows.Count; j++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[j]["CommunicationStepID"], dtSteps.Rows[j]["CommunicationStepName"].ToString());
		}
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int k = 0; k < dtColors.Rows.Count; k++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int l = 0; l < dtSizes.Rows.Count; l++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int m = 0; m < dtBatchs.Rows.Count; m++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[m]["BatchID"], dtBatchs.Rows[m]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int n = 0; n < dtItems.Rows.Count; n++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num = 0; num < dtUnits.Rows.Count; num++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num]["UnitID"], dtUnits.Rows[num]["UnitName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtTaxes.Rows.Count; num2++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[num2]["TaxID"], dtTaxes.Rows[num2]["TaxName"].ToString());
		}
		dtQuestions = Questions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlQuestions.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtQuestions.Rows.Count; num3++)
		{
			vlQuestions.ValueListItems.Add(dtQuestions.Rows[num3]["QuestionID"], dtQuestions.Rows[num3]["QuestionName"].ToString());
		}
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.MasterData.frmCustomersTree'").Length != 0)
		{
			CanAddCustomer = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.MasterData.frmCustomersTree'")[0]["FormID"].ToString(), "Adding");
			CanUpdateCustomer = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.MasterData.frmCustomersTree'")[0]["FormID"].ToString(), "Updating");
		}
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.Transactions.frmCustomersFollows'").Length != 0)
		{
			CanAddFollowUp = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.Transactions.frmCustomersFollows'")[0]["FormID"].ToString(), "Adding");
		}
		dtDetails = CustomersFollows.SelectByCustomerID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtCustomerItems = CustomersItems.SelectByCustomerID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtCustomerQuestions = CustomersQuestions.SelectByCustomerID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		InitGridCustomerQuestions();
		InitGridCustomerItems();
		InitGrid();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Customers.GetCode(IsFromServer: false) : "");
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtPhoneNumber).Clear();
		((TextEditorControlBase)txtMobile).Clear();
		((TextEditorControlBase)txtMobileNumber2).Clear();
		cboDefaultPaymentMethod.SelectedIndex = -1;
		cboDefaultSalesMan.SelectedIndex = -1;
		cboGender.SelectedIndex = -1;
		cboPriceType.SelectedIndex = -1;
		cboReligion.SelectedIndex = -1;
		cboSegmentations.SelectedIndex = -1;
		dtpCreationDate.Value = null;
		dtDetails.Rows.Clear();
		dtCustomerItems.Rows.Clear();
		dtCustomerQuestions.Rows.Clear();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Customers.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtCustomerItems = CustomersItems.SelectByCustomerID(RowID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtCustomerQuestions = CustomersQuestions.SelectByCustomerID(RowID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
			((Control)(object)txtCode).Text = drMaster["CustomerCode"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["CustomerNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["CustomerNameEn"].ToString();
			((TextEditorControlBase)cboGender).Value = drMaster["GenderID"];
			((TextEditorControlBase)cboReligion).Value = drMaster["ReligionID"];
			((TextEditorControlBase)cboDefaultPaymentMethod).Value = drMaster["DefaultPaymentMethodID"];
			((TextEditorControlBase)cboDefaultSalesMan).Value = drMaster["EmployeeSubAccountID"];
			((TextEditorControlBase)cboPriceType).Value = drMaster["PriceTypeID"];
			((TextEditorControlBase)cboSegmentations).Value = drMaster["SegmentationID"];
			dtpCreationDate.Value = drMaster["CreationDate"];
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((Control)(object)txtPhoneNumber).Text = drMaster["PhoneNumber"].ToString();
			((Control)(object)txtMobile).Text = drMaster["MobileNumber"].ToString();
			((Control)(object)txtMobileNumber2).Text = drMaster["MobileNumber2"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = CustomersFollows.SelectByCustomerID(drMaster["CustomerID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtCustomerQuestions = CustomersQuestions.SelectByCustomerID(drMaster["CustomerID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtCustomerItems = CustomersItems.SelectByCustomerID(drMaster["CustomerID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			InitGrid();
			InitGridCustomerItems();
			InitGridCustomerQuestions();
		}
		else
		{
			ClearControls();
		}
	}

	private void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود المتابعة" : "Customer Follow No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المتابعة " : "Follow Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CustomerFollowDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepID"].Header).Caption = (GlobalVariables.IsArabic ? "خطوة المتابعة" : "Communication Step");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepID"].ValueList = (IValueList)(object)vlSteps;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CommunicationStepID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "المسئول عن الخطوة الحالية" : "Follow Up Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeSubAccountID"].ValueList = (IValueList)(object)vlSalesmen;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comment"].Header).Caption = (GlobalVariables.IsArabic ? "التعليق" : "Comment");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comment"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Comment"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخطوة القادمة" : "Next Step Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextStepDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextEmployeeSubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "مسئول الخطوة القادمة" : "Next Step Employee");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextEmployeeSubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextEmployeeSubAccountID"].ValueList = (IValueList)(object)vlSalesmen;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NextEmployeeSubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	private void InitGridCustomerQuestions()
	{
		((UltraGridBase)ULGCustQuestions).DataSource = dtCustomerQuestions;
		GlobalFunctions.PrepareGrid(ULGCustQuestions);
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["CustomerQuestionID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].Header).Caption = (GlobalVariables.IsArabic ? "صيغة السؤال" : "Question");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].ValueList = (IValueList)(object)vlQuestions;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].Header).Caption = (GlobalVariables.IsArabic ? "إجابة ص / خ" : "Answer T/F");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].ValueList = (IValueList)(object)vlTrueFalseAnswers;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswertxt"].Header).Caption = (GlobalVariables.IsArabic ? "الاجابة" : "Answer");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswertxt"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswertxt"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ السؤال" : "Question Date");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.1);
	}

	private void InitGridCustomerItems()
	{
		((UltraGridBase)ULGCustItems).DataSource = dtCustomerItems;
		GlobalFunctions.PrepareGrid(ULGCustItems);
		((UltraGridBase)ULGCustItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["CustomerItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.2) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.08);
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.28) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.12);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.12);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.12);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.12);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.12);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الصنف" : "ItemDate");
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void btnRefreshDataClick()
	{
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransactionBranch, dtBranches, "BranchID", "BranchName");
		dtSegmentations = Segmentations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSegmentations, dtSegmentations, "SegmentationID", "SegmentationName");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDefaultSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		vlSalesmen.ValueListItems.Clear();
		for (int i = 0; i < dtSalesMan.Rows.Count; i++)
		{
			vlSalesmen.ValueListItems.Add(dtSalesMan.Rows[i]["SubAccountID"], dtSalesMan.Rows[i]["SubAccountName"].ToString());
		}
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtSteps = CommunicationSteps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSteps.ValueListItems.Clear();
		for (int j = 0; j < dtSteps.Rows.Count; j++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[j]["CommunicationStepID"], dtSteps.Rows[j]["CommunicationStepName"].ToString());
		}
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int k = 0; k < dtColors.Rows.Count; k++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[k]["ColorID"], dtColors.Rows[k]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int l = 0; l < dtSizes.Rows.Count; l++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[l]["ItemSizeID"], dtSizes.Rows[l]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int m = 0; m < dtBatchs.Rows.Count; m++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[m]["BatchID"], dtBatchs.Rows[m]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int n = 0; n < dtItems.Rows.Count; n++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[n]["ItemID"], dtItems.Rows[n]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int num = 0; num < dtUnits.Rows.Count; num++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[num]["UnitID"], dtUnits.Rows[num]["UnitName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int num2 = 0; num2 < dtTaxes.Rows.Count; num2++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[num2]["TaxID"], dtTaxes.Rows[num2]["TaxName"].ToString());
		}
		dtQuestions = Questions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlQuestions.ValueListItems.Clear();
		for (int num3 = 0; num3 < dtQuestions.Rows.Count; num3++)
		{
			vlQuestions.ValueListItems.Add(dtQuestions.Rows[num3]["QuestionID"], dtQuestions.Rows[num3]["QuestionName"].ToString());
		}
	}

	public virtual void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CRMCustomersReport(0, 0, IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["CustomerID"].ToString();
			FillData();
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void txtTel_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return || !(((Control)(object)txtTel).Text != ""))
		{
			return;
		}
		DataTable dataTable = Customers.SelectByMobilePhoneNumber(((Control)(object)txtTel).Text, GlobalVariables.CurrentBranchID, IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			if (!bool.Parse(dataTable.Rows[0]["IsClosed"].ToString()))
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
				CustomerID = int.Parse(RowID);
			}
			else
			{
				RowID = "";
				GlobalVariables.InformationMB.Show("لا يمكن اظهار البيانات الخاصه بهذا العميل لانه غير نشط", "InActive Client");
				dtDetails.Rows.Clear();
				dtCustomerQuestions.Rows.Clear();
				dtCustomerItems.Rows.Clear();
			}
			((TextEditorControlBase)txtTel).Clear();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
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
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Expected O, but got Unknown
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Expected O, but got Unknown
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Expected O, but got Unknown
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CRM.Transactions.frmCustomersInquery));
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
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		UltraGridBand val20 = new UltraGridBand("", -1);
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		UltraGridBand val32 = new UltraGridBand("", -1);
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		UltraGridBand val43 = new UltraGridBand("", -1);
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		Appearance val48 = new Appearance();
		Appearance val49 = new Appearance();
		Appearance val50 = new Appearance();
		Appearance val51 = new Appearance();
		Appearance val52 = new Appearance();
		Appearance val53 = new Appearance();
		Appearance val54 = new Appearance();
		Appearance val55 = new Appearance();
		Appearance val56 = new Appearance();
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.cboGender = new UltraComboEditor();
		this.lblGender = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.UGBDetails = new UltraGroupBox();
		this.txtPhoneNumber = new UltraTextEditor();
		this.txtMobile = new UltraTextEditor();
		this.lblPhoneNumber = new UltraLabel();
		this.txtMobileNumber2 = new UltraTextEditor();
		this.lblReorder = new UltraLabel();
		this.lblMobileNumber2 = new UltraLabel();
		this.lblEmployeeSubAccount = new UltraLabel();
		this.cboDefaultSalesMan = new UltraComboEditor();
		this.cboSegmentations = new UltraComboEditor();
		this.lblSegmentation = new UltraLabel();
		this.dtpCreationDate = new UltraDateTimeEditor();
		this.lblCreationDate = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.cboDefaultPaymentMethod = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.lblDefaultPaymentMethod = new UltraLabel();
		this.cboReligion = new UltraComboEditor();
		this.lblReligion = new UltraLabel();
		this.ULGData = new UltraGrid();
		this.btnFollowAdd = new UltraButton();
		this.txtTel = new UltraTextEditor();
		this.lblTel = new UltraLabel();
		this.ULGCustQuestions = new UltraGrid();
		this.ULGCustItems = new UltraGrid();
		this.btnClientAdd = new UltraButton();
		this.UGBFollowUp = new UltraGroupBox();
		this.UGBQuestions = new UltraGroupBox();
		this.UGBItems = new UltraGroupBox();
		this.ultraGroupBox1 = new UltraGroupBox();
		this.ultraGroupBox2 = new UltraGroupBox();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPhoneNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNumber2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSegmentations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCreationDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultPaymentMethod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustQuestions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBFollowUp).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBFollowUp).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBQuestions).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBQuestions).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		((EditorButtonControlBase)this.txtNameEn).ReadOnly = true;
		this.lblNameEn.AutoEllipsis = false;
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		((EditorButtonControlBase)this.txtNameAr).ReadOnly = true;
		this.lblNameAr.AutoEllipsis = false;
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		((EditorButtonControlBase)this.cboGender).ReadOnly = true;
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val9;
		this.lblGender.AutoEllipsis = false;
		resources.ApplyResources(this.lblGender, "lblGender");
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		((EditorButtonControlBase)this.txtEMail).ReadOnly = true;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val10;
		this.lblEMail.AutoEllipsis = false;
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((EditorButtonControlBase)this.txtNotes).ReadOnly = true;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val11;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)2;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtPhoneNumber);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblPhoneNumber);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobileNumber2);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblReorder);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblMobileNumber2);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblEmployeeSubAccount);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboSegmentations);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblSegmentation);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCreationDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblCreationDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboReligion);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblReligion);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.txtPhoneNumber, "txtPhoneNumber");
		((System.Windows.Forms.Control)(object)this.txtPhoneNumber).Name = "txtPhoneNumber";
		((EditorButtonControlBase)this.txtPhoneNumber).ReadOnly = true;
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		((EditorButtonControlBase)this.txtMobile).ReadOnly = true;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPhoneNumber).Appearance = (AppearanceBase)(object)val12;
		this.lblPhoneNumber.AutoEllipsis = false;
		resources.ApplyResources(this.lblPhoneNumber, "lblPhoneNumber");
		((System.Windows.Forms.Control)(object)this.lblPhoneNumber).Name = "lblPhoneNumber";
		((ControlBase)this.lblPhoneNumber).WrapText = false;
		resources.ApplyResources(this.txtMobileNumber2, "txtMobileNumber2");
		((System.Windows.Forms.Control)(object)this.txtMobileNumber2).Name = "txtMobileNumber2";
		((EditorButtonControlBase)this.txtMobileNumber2).ReadOnly = true;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblReorder).Appearance = (AppearanceBase)(object)val13;
		this.lblReorder.AutoEllipsis = false;
		resources.ApplyResources(this.lblReorder, "lblReorder");
		((System.Windows.Forms.Control)(object)this.lblReorder).Name = "lblReorder";
		((ControlBase)this.lblReorder).WrapText = false;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblMobileNumber2).Appearance = (AppearanceBase)(object)val14;
		this.lblMobileNumber2.AutoEllipsis = false;
		resources.ApplyResources(this.lblMobileNumber2, "lblMobileNumber2");
		((System.Windows.Forms.Control)(object)this.lblMobileNumber2).Name = "lblMobileNumber2";
		((ControlBase)this.lblMobileNumber2).WrapText = false;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblEmployeeSubAccount).Appearance = (AppearanceBase)(object)val15;
		this.lblEmployeeSubAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblEmployeeSubAccount, "lblEmployeeSubAccount");
		((System.Windows.Forms.Control)(object)this.lblEmployeeSubAccount).Name = "lblEmployeeSubAccount";
		((ControlBase)this.lblEmployeeSubAccount).WrapText = false;
		resources.ApplyResources(this.cboDefaultSalesMan, "cboDefaultSalesMan");
		((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan).Name = "cboDefaultSalesMan";
		((EditorButtonControlBase)this.cboDefaultSalesMan).ReadOnly = true;
		resources.ApplyResources(this.cboSegmentations, "cboSegmentations");
		((System.Windows.Forms.Control)(object)this.cboSegmentations).Name = "cboSegmentations";
		((EditorButtonControlBase)this.cboSegmentations).ReadOnly = true;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblSegmentation).Appearance = (AppearanceBase)(object)val16;
		this.lblSegmentation.AutoEllipsis = false;
		resources.ApplyResources(this.lblSegmentation, "lblSegmentation");
		((System.Windows.Forms.Control)(object)this.lblSegmentation).Name = "lblSegmentation";
		((ControlBase)this.lblSegmentation).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpCreationDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpCreationDate, "dtpCreationDate");
		((System.Windows.Forms.Control)(object)this.dtpCreationDate).Name = "dtpCreationDate";
		((EditorButtonControlBase)this.dtpCreationDate).ReadOnly = true;
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblCreationDate).Appearance = (AppearanceBase)(object)val17;
		this.lblCreationDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblCreationDate, "lblCreationDate");
		((System.Windows.Forms.Control)(object)this.lblCreationDate).Name = "lblCreationDate";
		((ControlBase)this.lblCreationDate).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((EditorButtonControlBase)this.cboPriceType).ReadOnly = true;
		resources.ApplyResources(this.cboDefaultPaymentMethod, "cboDefaultPaymentMethod");
		((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod).Name = "cboDefaultPaymentMethod";
		((EditorButtonControlBase)this.cboDefaultPaymentMethod).ReadOnly = true;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblPriceType).Appearance = (AppearanceBase)(object)val18;
		this.lblPriceType.AutoEllipsis = false;
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblDefaultPaymentMethod).Appearance = (AppearanceBase)(object)val19;
		this.lblDefaultPaymentMethod.AutoEllipsis = false;
		resources.ApplyResources(this.lblDefaultPaymentMethod, "lblDefaultPaymentMethod");
		((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod).Name = "lblDefaultPaymentMethod";
		((ControlBase)this.lblDefaultPaymentMethod).WrapText = false;
		resources.ApplyResources(this.cboReligion, "cboReligion");
		((System.Windows.Forms.Control)(object)this.cboReligion).Name = "cboReligion";
		((EditorButtonControlBase)this.cboReligion).ReadOnly = true;
		this.lblReligion.AutoEllipsis = false;
		resources.ApplyResources(this.lblReligion, "lblReligion");
		((System.Windows.Forms.Control)(object)this.lblReligion).Name = "lblReligion";
		((ControlBase)this.lblReligion).WrapText = false;
		val20.Override.AllowAddNew = (AllowAddNew)2;
		val20.Override.AllowDelete = (DefaultableBoolean)2;
		val20.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.BandsSerializer.Add((object)val20);
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val21).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val21).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val21).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val21;
		((AppearanceBase)val22).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val22;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val23).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val23).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val24;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val25;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val27).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val27;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val28).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val30;
		resources.ApplyResources(this.ULGData, "ULGData");
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		resources.ApplyResources(this.btnFollowAdd, "btnFollowAdd");
		((System.Windows.Forms.Control)(object)this.btnFollowAdd).Name = "btnFollowAdd";
		((System.Windows.Forms.Control)(object)this.btnFollowAdd).Click += new System.EventHandler(btnFollowAdd_Click);
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		((System.Windows.Forms.Control)(object)this.txtTel).KeyUp += new System.Windows.Forms.KeyEventHandler(txtTel_KeyUp);
		((AppearanceBase)val31).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val31).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblTel).Appearance = (AppearanceBase)(object)val31;
		this.lblTel.AutoEllipsis = false;
		resources.ApplyResources(this.lblTel, "lblTel");
		((System.Windows.Forms.Control)(object)this.lblTel).Name = "lblTel";
		val32.Override.AllowAddNew = (AllowAddNew)2;
		val32.Override.AllowDelete = (DefaultableBoolean)2;
		val32.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.BandsSerializer.Add((object)val32);
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val33).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val33;
		((AppearanceBase)val34).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val34;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val35).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val35).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val35).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val35;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val36).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val37).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val37;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val38;
		((AppearanceBase)val39).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val39).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val40).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val40).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val40).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val40).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val41).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val42).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val42;
		resources.ApplyResources(this.ULGCustQuestions, "ULGCustQuestions");
		((System.Windows.Forms.Control)(object)this.ULGCustQuestions).Name = "ULGCustQuestions";
		this.ULGCustQuestions.AfterEnterEditMode += new System.EventHandler(ULGCustQuestions_AfterEnterEditMode);
		val43.Override.AllowAddNew = (AllowAddNew)2;
		val43.Override.AllowDelete = (DefaultableBoolean)2;
		val43.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.BandsSerializer.Add((object)val43);
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val44).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val44).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val44).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val44;
		((AppearanceBase)val45).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val45;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val46).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val46).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val46).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val46;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val47).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val47).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val47;
		((AppearanceBase)val48).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val48).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val48;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val49).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val49;
		((AppearanceBase)val50).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val50).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val50;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val51).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val51).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val51).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val51).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val51).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val51;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val52).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val52).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val52;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val53).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val53;
		resources.ApplyResources(this.ULGCustItems, "ULGCustItems");
		((System.Windows.Forms.Control)(object)this.ULGCustItems).Name = "ULGCustItems";
		this.ULGCustItems.AfterEnterEditMode += new System.EventHandler(ULGCustItems_AfterEnterEditMode);
		resources.ApplyResources(this.btnClientAdd, "btnClientAdd");
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Name = "btnClientAdd";
		((System.Windows.Forms.Control)(object)this.btnClientAdd).Click += new System.EventHandler(btnClientAdd_Click);
		resources.ApplyResources(this.UGBFollowUp, "UGBFollowUp");
		((AppearanceBase)val54).BackColor = System.Drawing.SystemColors.Control;
		this.UGBFollowUp.Appearance = (AppearanceBase)(object)val54;
		this.UGBFollowUp.CaptionAlignment = (GroupBoxCaptionAlignment)2;
		((System.Windows.Forms.Control)(object)this.UGBFollowUp).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.UGBFollowUp).Name = "UGBFollowUp";
		resources.ApplyResources(this.UGBQuestions, "UGBQuestions");
		this.UGBQuestions.CaptionAlignment = (GroupBoxCaptionAlignment)2;
		((System.Windows.Forms.Control)(object)this.UGBQuestions).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCustQuestions);
		((System.Windows.Forms.Control)(object)this.UGBQuestions).Name = "UGBQuestions";
		resources.ApplyResources(this.UGBItems, "UGBItems");
		((AppearanceBase)val55).BackColor = System.Drawing.Color.Transparent;
		this.UGBItems.Appearance = (AppearanceBase)(object)val55;
		this.UGBItems.CaptionAlignment = (GroupBoxCaptionAlignment)2;
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCustItems);
		((System.Windows.Forms.Control)(object)this.UGBItems).Name = "UGBItems";
		resources.ApplyResources(this.ultraGroupBox1, "ultraGroupBox1");
		((AppearanceBase)val56).BackColor = System.Drawing.Color.Transparent;
		this.ultraGroupBox1.Appearance = (AppearanceBase)(object)val56;
		this.ultraGroupBox1.BorderStyle = (GroupBoxBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.UGBFollowUp);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Controls.Add((System.Windows.Forms.Control)(object)this.UGBItems);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).Name = "ultraGroupBox1";
		resources.ApplyResources(this.ultraGroupBox2, "ultraGroupBox2");
		this.ultraGroupBox2.BorderStyle = (GroupBoxBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox1);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Controls.Add((System.Windows.Forms.Control)(object)this.UGBQuestions);
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).Name = "ultraGroupBox2";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraGroupBox2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnFollowAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmCustomersInquery";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnFollowAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraGroupBox2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtPhoneNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNumber2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSegmentations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCreationDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultPaymentMethod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustQuestions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBFollowUp).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBFollowUp).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBQuestions).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBQuestions).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBItems).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox1).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ultraGroupBox2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraGroupBox2).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
