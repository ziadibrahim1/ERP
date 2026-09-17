using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CRM;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.StockControl.MasterData;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinTree;

namespace ERP.CRM.MasterData;

public class frmCustomersTree : frmTree2
{
	private DataTable dtCuromerQuestions;

	private DataTable dtReports;

	private DataTable dtCountries;

	private DataTable dtCustomerItems;

	private DataTable dtSegmentations;

	private DataTable dtGender;

	private DataTable dtReligions;

	private DataTable dtPaymentMethod;

	private DataTable dtPriceType;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtBranchs;

	private DataTable dtSalesMan;

	private DataTable dtUsers;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtItemPrices;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtBatchs;

	private DataTable dtUnits;

	private DataTable dtTaxes;

	private DataTable dtQuestions;

	private DataTable dtSubAccounts;

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

	private DateTime dtCurrentDate;

	public int CustomerID = 0;

	private int OpenMode = 0;

	private bool addFromAnotherForm = false;

	private ValueList vlTrueFalseAnswers = new ValueList();

	private IContainer components = null;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabRecipe;

	public UltraGrid ULGCustQuestions;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem changeParentTSMenu;

	private ToolStripMenuItem setAsGroupToolStripMenuItem;

	private ToolStripMenuItem setAsSubAccountToolStripMenuItem;

	private UltraLabel lblBranch;

	private UltraComboEditor cboBranch;

	private UltraTabPageControl ultraTabPageControl3;

	public UltraGrid ULGCustItems;

	private UltraTabPageControl tabItem;

	private UltraCheckEditor chkIsClosed;

	private UltraDateTimeEditor dtpClosedDate;

	private UltraLabel lblClosedDate;

	private UltraComboEditor cboSegmentations;

	private UltraLabel lblSegmentation;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraComboEditor cboPriceType;

	private UltraComboEditor cboDefaultPaymentMethod;

	private UltraLabel lblPriceType;

	private UltraComboEditor cboGender;

	private UltraLabel lblDefaultPaymentMethod;

	private UltraTextEditor txtPhoneNumber;

	private UltraLabel lblGender;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtEMail;

	private UltraTextEditor txtMobile;

	private UltraLabel lblPhoneNumber;

	private UltraTextEditor txtAddress;

	private UltraLabel lblEMail;

	private UltraTextEditor txtMobileNumber2;

	private UltraLabel lblAddress;

	private UltraLabel lblNotes;

	private UltraLabel lblReorder;

	private UltraLabel lblMobileNumber2;

	private UltraTextEditor txtWorkCompanyName;

	private UltraLabel lblWorkCompanyName;

	private UltraComboEditor cboReligion;

	private UltraLabel lblReligion;

	private UltraDateTimeEditor dtpCreationDate;

	private UltraLabel lblCreationDate;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraLabel lblEmployeeSubAccount;

	private UltraComboEditor cboDefaultSalesMan;

	private UltraTextEditor txtGrossValue;

	private UltraTextEditor txtTaxTotalValue;

	private UltraTextEditor txtTotalQty;

	private UltraTextEditor txtTotalPrice;

	private UltraLabel lblTotalPrice;

	public UltraButton btnCreateClient;

	private UltraLabel lblClientSubAccount;

	private UltraComboEditor cboClientSubAccount;

	public frmCustomersTree()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
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
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "CustomerID";
		NoCol = "CustomerCode";
		NameCol = "CustomerNameAr";
		NameEnCol = "CustomerNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		AdditionalCol2 = "BranchID";
		ItemLevelCol = "LevelID";
		TableName = "CRM_Customers";
		LevelsTable = "CRM_Customers_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = true;
	}

	public frmCustomersTree(int _customerID, int _openMode)
		: this()
	{
		RowID = _customerID.ToString();
		CustomerID = _customerID;
		OpenMode = _openMode;
		addFromAnotherForm = true;
	}

	public override void FillData()
	{
		base.FillData();
		if (CustomerID == -1)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey("0");
			treeChart.ActiveNode.Selected = true;
			btnAddClick();
			((Control)(object)btnAdd).Visible = false;
			((Control)(object)btnOK).Visible = false;
		}
		else if (CustomerID != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(CustomerID.ToString());
			treeChart.ActiveNode.Selected = true;
			if (OpenMode == 2)
			{
				btnUpdateClick();
				((Control)(object)btnAdd).Visible = false;
				((Control)(object)btnOK).Visible = false;
			}
		}
		DisplayData();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtCurrentDate = GlobalFunctions.GetServerDateTimeNow();
		vlTrueFalseAnswers.ValueListItems.Clear();
		vlTrueFalseAnswers.ValueListItems.Add((object)"", GlobalVariables.IsArabic ? "" : "");
		vlTrueFalseAnswers.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "نعم" : "Yes");
		vlTrueFalseAnswers.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "لا" : "No");
		dtSegmentations = Segmentations.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSegmentations, dtSegmentations, "SegmentationID", "SegmentationName");
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		dtPaymentMethod = PaymentMethods.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDefaultPaymentMethod, dtPaymentMethod, "PaymentMethodID", "PaymentMethodName");
		dtPriceType = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceType, "PriceTypeID", "PriceName");
		dtBranchs = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranch, dtBranchs, "BranchID", "BranchName");
		dtSalesMan = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboDefaultSalesMan, dtSalesMan, "SubAccountID", "SubAccountName");
		dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboClientSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		AutomaticlyAddItemTaxToSalesInvoice = GlobalFunctions.GetOption("AutomaticlyAddItemTaxToSalesInvoice");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int l = 0; l < dtItems.Rows.Count; l++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int n = 0; n < dtTaxes.Rows.Count; n++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[n]["TaxID"], dtTaxes.Rows[n]["TaxName"].ToString());
		}
		dtQuestions = Questions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlQuestions.ValueListItems.Clear();
		for (int num = 0; num < dtQuestions.Rows.Count; num++)
		{
			vlQuestions.ValueListItems.Add(dtQuestions.Rows[num]["QuestionID"], dtQuestions.Rows[num]["QuestionName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtCuromerQuestions = CustomersQuestions.SelectByCustomerID("0", "1", IsFromServer: true);
		InitGridCustomerQuestions();
		dtCustomerItems = CustomersItems.SelectByCustomerID("0", "1", IsFromServer: true);
		InitGridCustomerItems();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtCurrentDate = GlobalFunctions.GetServerDateTimeNow();
		if (Adding)
		{
			((Control)(object)txtCode).Text = Customers.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((TextEditorControlBase)txtAddress).Clear();
			((TextEditorControlBase)txtEMail).Clear();
			((TextEditorControlBase)txtMobile).Clear();
			((TextEditorControlBase)txtMobileNumber2).Clear();
			((TextEditorControlBase)txtName).Clear();
			((TextEditorControlBase)txtNameEn).Clear();
			((TextEditorControlBase)txtNotes).Clear();
			((TextEditorControlBase)txtPhoneNumber).Clear();
			((TextEditorControlBase)txtWorkCompanyName).Clear();
			((Control)(object)txtTotalPrice).Text = "0.0";
			cboArea.SelectedIndex = -1;
			cboBranch.SelectedIndex = -1;
			cboCity.SelectedIndex = -1;
			cboCountry.SelectedIndex = -1;
			cboDefaultPaymentMethod.SelectedIndex = -1;
			cboDefaultSalesMan.SelectedIndex = -1;
			cboGender.SelectedIndex = -1;
			cboPriceType.SelectedIndex = -1;
			cboReligion.SelectedIndex = -1;
			cboSegmentations.SelectedIndex = -1;
			dtpCreationDate.Value = dtCurrentDate;
			dtpClosedDate.Value = null;
			((UltraToggleEditorBase)chkIsClosed).Checked = false;
			((Control)(object)txtName).Select();
		}
		dtCuromerQuestions.Rows.Clear();
		dtCustomerItems.Rows.Clear();
	}

	private void cboCountry_ValueChanged(object sender, EventArgs e)
	{
		if (cboCountry.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtCities);
			dataView.RowFilter = "CountryID=" + ((TextEditorControlBase)cboCountry).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboCity.DataSource = dataView;
			cboCity.DisplayMember = "CityName";
			cboCity.ValueMember = "CityID";
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnOK).Visible = !addFromAnotherForm;
		((Control)(object)btnCreateClient).Visible = NavMode && CanUpdate;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranch).ReadOnly = true;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultPaymentMethod).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDefaultSalesMan).ReadOnly = true;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboPriceType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboReligion).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSegmentations).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobileNumber2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPhoneNumber).ReadOnly = NavMode;
		((EditorButtonControlBase)txtWorkCompanyName).ReadOnly = NavMode;
		((Control)(object)chkIsClosed).Enabled = !NavMode;
		((EditorButtonControlBase)dtpCreationDate).ReadOnly = true;
		((EditorButtonControlBase)dtpClosedDate).ReadOnly = NavMode;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((!NavMode) ? 1 : 2);
		((UltraGridBase)ULGCustItems).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((!NavMode) ? 1 : 2);
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((UltraGridBase)ULGCustItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		((Control)(object)btnCreateClient).Enabled = false;
		if (SelectedNode != null)
		{
			DataTable dataTable = Customers.Select(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dataTable.Rows.Count > 0)
			{
				DataRow dataRow = dataTable.Rows[0];
				((Control)(object)btnCreateClient).Enabled = dataRow["SubAccountID"] == DBNull.Value;
				((Control)(object)txtAddress).Text = dataRow["Address"].ToString();
				((Control)(object)txtCode).Text = dataRow["CustomerCode"].ToString();
				((Control)(object)txtEMail).Text = dataRow["EMail"].ToString();
				((Control)(object)txtMobile).Text = dataRow["MobileNumber"].ToString();
				((Control)(object)txtMobileNumber2).Text = dataRow["MobileNumber2"].ToString();
				((Control)(object)txtName).Text = dataRow["CustomerNameAr"].ToString();
				((Control)(object)txtNameEn).Text = dataRow["CustomerNameEn"].ToString();
				((Control)(object)txtPhoneNumber).Text = dataRow["PhoneNumber"].ToString();
				((Control)(object)txtWorkCompanyName).Text = dataRow["WorkCompanyName"].ToString();
				((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
				((Control)(object)txtTotalPrice).Text = dataRow["TotalPrice"].ToString();
				((TextEditorControlBase)cboArea).Value = dataRow["AreaID"];
				((TextEditorControlBase)cboBranch).Value = dataRow["BranchID"];
				((TextEditorControlBase)cboCity).Value = dataRow["CityID"];
				((TextEditorControlBase)cboCountry).Value = dataRow["CountryID"];
				((TextEditorControlBase)cboDefaultPaymentMethod).Value = dataRow["DefaultPaymentMethodID"];
				((TextEditorControlBase)cboDefaultSalesMan).Value = dataRow["EmployeeSubAccountID"];
				((TextEditorControlBase)cboClientSubAccount).Value = dataRow["SubAccountID"];
				((TextEditorControlBase)cboGender).Value = dataRow["GenderID"];
				((TextEditorControlBase)cboPriceType).ValueChanged -= cboPriceType_ValueChanged;
				((TextEditorControlBase)cboPriceType).Value = dataRow["PriceTypeID"];
				((TextEditorControlBase)cboPriceType).ValueChanged += cboPriceType_ValueChanged;
				((TextEditorControlBase)cboReligion).Value = dataRow["ReligionID"];
				((TextEditorControlBase)cboSegmentations).Value = dataRow["SegmentationID"];
				((UltraToggleEditorBase)chkIsClosed).Checked = Convert.ToBoolean(dataRow["IsClosed"]);
				dtpCreationDate.Value = dataRow["CreationDate"];
				dtpClosedDate.Value = dataRow["ClosedDate"];
				dtCuromerQuestions = CustomersQuestions.SelectByCustomerID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
				InitGridCustomerQuestions();
				dtCustomerItems = CustomersItems.SelectByCustomerID(((KeyedSubObjectBase)SelectedNode).Key, "1", IsFromServer: true);
				InitGridCustomerItems();
			}
		}
	}

	private void ULGCustItems_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0f7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f85: Expected O, but got Unknown
		//IL_0f93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9d: Expected O, but got Unknown
		ULGCustItems.CellListSelect -= new CellEventHandler(ULGCustItems_CellListSelect);
		ULGCustItems.AfterCellUpdate -= new CellEventHandler(ULGCustItems_AfterCellUpdate);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGCustItems).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0)
			{
				DataRow[] array = dtItemPrices.Select(bool.Parse(dataRow["IsUnitPrice"].ToString()) ? ("UnitID = " + dataRow["UnitID"].ToString() + " and  ItemID= " + e.Cell.Value.ToString()) : (" ItemID = " + e.Cell.Value.ToString()));
				if (array != null && array.Length != 0)
				{
					((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
					((UltraGridBase)ULGCustItems).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value.ToString());
				}
				else
				{
					UltraGridCell obj2 = ((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"];
					value = (((UltraGridBase)ULGCustItems).ActiveRow.Cells["TotalPrice"].Value = 0);
					obj2.Value = value;
				}
				CalculateGoss();
				CalculateRow(e.Cell.Row);
				CalculateTotalsTax();
			}
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtItems.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (UsingBatchNoAndValidityPeriod)
			{
				if (num != 0)
				{
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
					e.Cell.Row.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(num);
				}
				else
				{
					e.Cell.Row.Cells["BatchID"].ValueList = null;
					e.Cell.Row.Cells["BatchID"].Value = DBNull.Value;
				}
			}
			if (AutomaticlyAddItemTaxToSalesInvoice)
			{
				((UltraGridBase)ULGCustItems).ActiveRow.Cells["TaxID"].Value = dataRow["TaxID"];
				CalculateRow(e.Cell.Row);
				CalculateTotalsTax();
			}
			int num2 = ((((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num2);
				((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			}
			else
			{
				e.Cell.Row.Cells["UnitID"].ValueList = null;
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
			}
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ColorID"].Value = 1;
				e.Cell.Row.Cells["ColorID"].ValueList = null;
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = 1;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "UnitID" && e.Cell.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGCustItems).UpdateData();
			if (e.Cell.Value != DBNull.Value && ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				if (bool.Parse(dataRow2["IsUnitPrice"].ToString()) && dtItemPrices != null && dtItemPrices.Rows.Count > 0)
				{
					DataRow[] array2 = dtItemPrices.Select(" UnitID = " + e.Cell.Value.ToString() + " and ItemID= " + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString());
					if (array2 != null && array2.Length != 0)
					{
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array2[0]["Price"].ToString()) - decimal.Parse(array2[0]["Price"].ToString()) * decimal.Parse(array2[0]["DiscountPercentage"].ToString()) / 100m;
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value.ToString());
					}
					else
					{
						UltraGridCell obj4 = ((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"];
						object value = (((UltraGridBase)ULGCustItems).ActiveRow.Cells["TotalPrice"].Value = 0);
						obj4.Value = value;
					}
					CalculateGoss();
					CalculateRow(e.Cell.Row);
					CalculateTotalsTax();
				}
			}
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGCustItems).UpdateData();
			CalculateRow(e.Cell.Row);
			CalculateTotalsTax();
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"] != DBNull.Value)
		{
			int num3 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num3 != 0)
			{
				DataRow dataRow3 = dtItems.Select(" ItemID= " + num3)[0];
				UltraGridCell obj6 = ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value = num3);
				obj6.Value = value;
				((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
				int num4 = ((((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
				if (num4 != 0)
				{
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num4);
					((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
				}
				else
				{
					e.Cell.Row.Cells["UnitID"].ValueList = null;
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				}
				if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ColorID"].Value = 1;
					e.Cell.Row.Cells["ColorID"].ValueList = null;
				}
				if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = 1;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
				}
			}
		}
		CalcTotalQty();
		ULGCustItems.CellListSelect += new CellEventHandler(ULGCustItems_CellListSelect);
		ULGCustItems.AfterCellUpdate += new CellEventHandler(ULGCustItems_AfterCellUpdate);
	}

	private void ULGCustItems_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected O, but got Unknown
		ULGCustItems.AfterCellUpdate -= new CellEventHandler(ULGCustItems_AfterCellUpdate);
		if (((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Band.Index == 0 && ULGCustItems.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TaxValue") && ULGCustItems.ActiveCell.Value == DBNull.Value)
			{
				ULGCustItems.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "UnitPrice")
			{
				((UltraGridBase)ULGCustItems).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TotalPrice" && decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["TotalPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value.ToString());
				CalculateGoss();
			}
			else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TaxID" && e.Cell.Value == DBNull.Value)
			{
				((UltraGridBase)ULGCustItems).ActiveRow.Cells["TaxValue"].Value = 0;
				CalculateGoss();
			}
			CalculateRow(((UltraGridBase)ULGCustItems).ActiveRow);
		}
		else if (ULGCustItems.ActiveCell != null)
		{
			CalculateRow(((UltraGridBase)ULGCustItems).ActiveRow.ParentRow);
		}
		CalculateTotalsTax();
		ULGCustItems.AfterCellUpdate += new CellEventHandler(ULGCustItems_AfterCellUpdate);
	}

	private void InitGridCustomerQuestions()
	{
		((UltraGridBase)ULGCustQuestions).DataSource = dtCuromerQuestions;
		GlobalFunctions.PrepareGrid(ULGCustQuestions);
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["CustomerQuestionID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].Header).Caption = (GlobalVariables.IsArabic ? "صيغة السؤال" : "Question");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].ValueList = (IValueList)(object)vlQuestions;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionID"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].Header).Caption = (GlobalVariables.IsArabic ? "إجابة ص / خ" : "Answer T/F");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].ValueList = (IValueList)(object)vlTrueFalseAnswers;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswerBool"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswertxt"].Header).Caption = (GlobalVariables.IsArabic ? "الاجابة" : "Answer");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswertxt"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionAnswertxt"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ السؤال" : "Question Date");
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].Hidden = false;
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].Width = (int)((double)((Control)(object)ULGCustQuestions).Width * 0.1);
		((UltraGridBase)ULGCustQuestions).DisplayLayout.Bands[0].Columns["QuestionDate"].DefaultCellValue = dtCurrentDate;
	}

	private void InitGridCustomerItems()
	{
		((UltraGridBase)ULGCustItems).DataSource = dtCustomerItems;
		GlobalFunctions.PrepareGrid(ULGCustItems);
		((UltraGridBase)ULGCustItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["CustomerItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.05);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.05);
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
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1);
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.05);
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
		}
		else
		{
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.15);
			((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.08);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.07);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.07);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.07);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.08);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.08);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1);
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].Width = (int)((double)((Control)(object)ULGCustItems).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit price");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total Price");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((HeaderBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الصنف" : "ItemDate");
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].Hidden = false;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlTaxes;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["NetPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns["ItemDate"].DefaultCellValue = dtCurrentDate;
	}

	public override int TreeAddData()
	{
		int num = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = Customers.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? ((Control)(object)txtName).Text.Trim() : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), (cboSegmentations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSegmentations).Value.ToString(), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), ((Control)(object)txtPhoneNumber).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtMobileNumber2).Text, ((Control)(object)txtEMail).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtAddress).Text, ((Control)(object)txtWorkCompanyName).Text, (cboDefaultPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultPaymentMethod).Value.ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"].ToString(), "Null", dtpCreationDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "Null", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (dtCuromerQuestions.Rows.Count > 0)
			{
				for (int i = 0; i < dtCuromerQuestions.Rows.Count; i++)
				{
					dtCuromerQuestions.Rows[i]["CustomerID"] = num;
					dtCuromerQuestions.Rows[i]["QuestionDate"] = dtCurrentDate;
				}
				CustomersQuestions.Insert_UpdateByTable(dtCuromerQuestions, GlobalVariables.UserID, IsFromServer: true);
			}
			if (dtCustomerItems.Rows.Count > 0)
			{
				for (int j = 0; j < dtCustomerItems.Rows.Count; j++)
				{
					dtCustomerItems.Rows[j]["CustomerID"] = num;
					dtCustomerItems.Rows[j]["ItemDate"] = dtCurrentDate;
				}
				CustomersItems.Insert_UpdateByTable(dtCustomerItems, GlobalVariables.UserID, IsFromServer: true);
			}
			CustomerID = num;
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return num;
	}

	public override void TreeUpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			Customers.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? ((Control)(object)txtName).Text.Trim() : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), (cboSegmentations.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSegmentations).Value.ToString(), (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), ((Control)(object)txtPhoneNumber).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtMobileNumber2).Text, ((Control)(object)txtEMail).Text, (cboPriceType.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPriceType).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), ((Control)(object)txtAddress).Text, ((Control)(object)txtWorkCompanyName).Text, (cboDefaultPaymentMethod.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboDefaultPaymentMethod).Value.ToString(), ((Control)(object)txtNotes).Text, (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, ((TextEditorControlBase)cboDefaultSalesMan).Value.ToString(), (cboClientSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboClientSubAccount).Value.ToString(), dtpCreationDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkIsClosed).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsClosed).Checked ? dtpClosedDate.DateTime.ToString(GlobalVariables.DateLongFormate) : "Null", "0", (cboBranch.SelectedIndex == -1) ? GlobalVariables.CurrentBranchID : ((TextEditorControlBase)cboBranch).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			((UltraGridBase)ULGCustQuestions).UpdateData();
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustQuestions).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["CustomerQuestionID"].Value.ToString() + ",";
				((UltraGridBase)ULGCustQuestions).Rows[i].Cells["CustomerID"].Value = ((KeyedSubObjectBase)SelectedNode).Key;
				((UltraGridBase)ULGCustQuestions).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				if (((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionDate"].Value == DBNull.Value)
				{
					((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionDate"].Value = dtCurrentDate;
				}
			}
			Main.SyncDeleteForUpdate("CRM_CustomersQuestions", "CustomerID", ((KeyedSubObjectBase)SelectedNode).Key, "CustomerQuestionID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGCustQuestions).Rows).Count > 0)
			{
				CustomersQuestions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGCustQuestions).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			((UltraGridBase)ULGCustItems).UpdateData();
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; j++)
			{
				text2 = text2 + ((UltraGridBase)ULGCustItems).Rows[j].Cells["CustomerItemID"].Value.ToString() + ",";
				((UltraGridBase)ULGCustItems).Rows[j].Cells["CustomerID"].Value = int.Parse(((KeyedSubObjectBase)SelectedNode).Key);
				((UltraGridBase)ULGCustItems).Rows[j].Cells["BranchID"].Value = int.Parse(GlobalVariables.CurrentBranchID);
				if (((UltraGridBase)ULGCustItems).Rows[j].Cells["ItemDate"].Value == DBNull.Value)
				{
					((UltraGridBase)ULGCustItems).Rows[j].Cells["ItemDate"].Value = dtCurrentDate;
				}
			}
			Main.SyncDeleteForUpdate("CRM_CustomersItems", "CustomerID", ((KeyedSubObjectBase)SelectedNode).Key, "CustomerItemID", text2, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count > 0)
			{
				CustomersItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGCustItems).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch (Exception)
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void ULGCustItems_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGCustItems.AfterExitEditMode -= ULGCustItems_AfterExitEditMode;
		if (ULGCustItems.ActiveCell != null && ULGCustItems.ActiveCell.Value != null && ULGCustItems.ActiveCell.Value != DBNull.Value && (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "ItemID") && dtItems.Select(" ItemID= " + ULGCustItems.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGCustItems.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		CalcTotalQty();
		ULGCustItems.AfterExitEditMode += ULGCustItems_AfterExitEditMode;
	}

	public void CalcTotalQty()
	{
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count <= 0)
		{
			return;
		}
		((Control)(object)txtTotalQty).Text = "0";
		((UltraGridBase)ULGCustItems).UpdateData();
		DataView dataView = new DataView((DataTable)((UltraGridBase)ULGCustItems).DataSource);
		dataView.RowFilter = " UnitID is not null";
		DataTable dataTable = dataView.ToTable();
		if (dataTable.Rows.Count > 0 && dataTable.Select(" UnitID<> " + dataTable.Rows[0]["UnitID"].ToString()).Length == 0)
		{
			object obj = dataTable.Compute(" Sum(Qty) ", "");
			if (obj != DBNull.Value)
			{
				((Control)(object)txtTotalQty).Text = decimal.Parse(obj.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			}
		}
	}

	private void ULGCustItems_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		ULGCustItems.AfterCellUpdate -= new CellEventHandler(ULGCustItems_AfterCellUpdate);
		CalculateGoss();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; i++)
		{
			CalculateRow(((UltraGridBase)ULGCustItems).Rows[i]);
		}
		CalculateTotalsTax();
		ULGCustItems.AfterCellUpdate += new CellEventHandler(ULGCustItems_AfterCellUpdate);
	}

	private void ULGCustQuestions_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGCustItems_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGCustQuestions_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		ULGCustQuestions.CellListSelect -= new CellEventHandler(ULGCustQuestions_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "QuestionID" && e.Cell.Value != null)
		{
			e.Cell.Row.Cells["QuestionAnswerBool"].Value = DBNull.Value;
		}
		ULGCustQuestions.CellListSelect += new CellEventHandler(ULGCustQuestions_CellListSelect);
	}

	private void btnCreateClient_Click(object sender, EventArgs e)
	{
		CreateSubAccount();
	}

	private void CreateSubAccount()
	{
		if (SelectedNode != null)
		{
			if (((UltraToggleEditorBase)chkIsClosed).Checked)
			{
				return;
			}
			if (Main.CheckForValue("A_SubAccounts", "SubAccountNameAr", ((Control)(object)txtName).Text, "txtName.Text", IsFromServer: true) > 0)
			{
				GlobalVariables.InformationMB.Show(" اسم العميل متواجد من قبل ", "Client Name Already Exist");
				((TextEditorControlBase)txtName).Focus();
				return;
			}
			frmChooseClientParent frmChooseClientParent2 = new frmChooseClientParent();
			((Control)(object)frmChooseClientParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "إختر مجموعة" : "Select Group");
			if (frmChooseClientParent2.ShowDialog() == DialogResult.OK)
			{
				string parentID = frmChooseClientParent2.ParentID;
				SubAccounts.Insert_CRMCustomer(((KeyedSubObjectBase)SelectedNode).Key.ToString(), parentID, GlobalVariables.UserID, IsFromServer: true);
				dtSubAccounts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				GlobalFunctions.FillCombo(cboClientSubAccount, dtSubAccounts, "SubAccountID", "SubAccountName");
				DisplayData();
			}
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "هذا العميل مغلق لا يمكن انشاء حساب تحليلي عليه" : "This Is A Closed Customer So Cannot Make A Client.");
		}
	}

	private void ULGCustItems_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGCustItems.ActiveCell != null && (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TaxValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGCustItems.ActiveCell, e);
		}
	}

	private void ULGCustItems_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
				{
					frmItemsBatches frmItemsBatches2 = new frmItemsBatches(int.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString()));
					frmItemsBatches2.WindowState = FormWindowState.Normal;
					((Control)(object)frmItemsBatches2.lblTitle).Text = (GlobalVariables.IsArabic ? "سريل" : "Items Batches");
					frmItemsBatches2.ShowDialog();
					dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
					((UltraGridBase)ULGCustItems).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)getBatchsValueList(int.Parse(frmItemsBatches2.RowID));
					vlBatchs.ValueListItems.Clear();
					for (int i = 0; i < dtBatchs.Rows.Count; i++)
					{
						vlBatchs.ValueListItems.Add(dtBatchs.Rows[i]["BatchID"], dtBatchs.Rows[i]["BatchName"].ToString());
					}
				}
				else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					if (frmQuantityMultiUnit2.UnitID > 0)
					{
						DataRow dataRow = dtItems.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString())[0];
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitID"].Value = frmQuantityMultiUnit2.UnitID;
						if (bool.Parse(dataRow["IsUnitPrice"].ToString()) && dtItemPrices != null && dtItemPrices.Rows.Count > 0)
						{
							DataRow[] array = dtItemPrices.Select(" UnitID = " + frmQuantityMultiUnit2.UnitID + " and ItemID= " + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString());
							if (array != null && array.Length != 0)
							{
								((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(array[0]["Price"].ToString()) - decimal.Parse(array[0]["Price"].ToString()) * decimal.Parse(array[0]["DiscountPercentage"].ToString()) / 100m;
							}
							else
							{
								((UltraGridBase)ULGCustItems).ActiveRow.Cells["UnitPrice"].Value = 0;
							}
						}
					}
					((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGCustItems).ActiveRow.Cells["Qty"].Value);
				}
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F8)
			{
				return;
			}
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "ItemBarCode")
				{
					int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
					if (num != 0)
					{
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value = num;
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemBarCode"].Value = num;
					}
				}
				else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TaxID")
				{
					int num2 = SearchFunctions.TaxsSearch(IsFromServer: false);
					if (num2 != 0)
					{
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["TaxID"].Value = num2;
					}
				}
				else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID = '" + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString() + "'")[0]["EnforceBatchNo"].ToString()))
				{
					int num3 = SearchFunctions.ItemsBatchesSearch(int.Parse(((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString()), 0, IsFromServer: false);
					if (num3 != 0)
					{
						((UltraGridBase)ULGCustItems).ActiveRow.Cells["BatchID"].Value = num3;
					}
				}
			}
			e.Handled = true;
		}
	}

	private void cboPriceType_ValueChanged(object sender, EventArgs e)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Expected O, but got Unknown
		if (cboPriceType.SelectedIndex > -1)
		{
			ULGCustItems.AfterCellUpdate -= new CellEventHandler(ULGCustItems_AfterCellUpdate);
			dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((TextEditorControlBase)cboPriceType).Value.ToString(), dtCurrentDate.ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGCustItems).Rows[i].Cells["ItemID"].Value != null)
				{
					((UltraGridBase)ULGCustItems).Rows[i].Cells["UnitPrice"].Value = decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) - decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).Rows[i].Cells["ItemID"].Value.ToString())[0]["Price"].ToString()) * Math.Round(decimal.Parse(dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).Rows[i].Cells["ItemID"].Value.ToString())[0]["DiscountPercentage"].ToString()), 3) / 100m;
					((UltraGridBase)ULGCustItems).Rows[i].Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGCustItems).Rows[i].Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGCustItems).Rows[i].Cells["Qty"].Value.ToString());
					CalculateRow(((UltraGridBase)ULGCustItems).Rows[i]);
				}
			}
			CalculateGoss();
			CalculateTotalsTax();
			ULGCustItems.AfterCellUpdate += new CellEventHandler(ULGCustItems_AfterCellUpdate);
		}
		else
		{
			dtItemPrices = null;
		}
	}

	public override void TreeDeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			CustomersItems.DeleteByCustomerID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			CustomersQuestions.DeleteByCustomerID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Customers.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override bool ValidateData()
	{
		if (!base.ValidateData())
		{
			return false;
		}
		dtCuromerQuestions.AcceptChanges();
		dtCustomerItems.AcceptChanges();
		if (cboSegmentations.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك أختر القطاع ", "Please Select Segmentation");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((TextEditorControlBase)cboSegmentations).Focus();
			return false;
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("من فضلك أختر نوع السعر ", "Please Select Price Type");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((TextEditorControlBase)cboPriceType).Focus();
			return false;
		}
		if (((Control)(object)txtPhoneNumber).Text.Trim().Equals("") && ((Control)(object)txtMobile).Text.Trim().Equals("") && ((Control)(object)txtMobileNumber2).Text.Trim().Equals(""))
		{
			GlobalVariables.InformationMB.Show("من فضلك قم بإدخال رقم تليفون واحد على الاقل", "Please Enter At Least One Phone Number");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((TextEditorControlBase)txtPhoneNumber).Focus();
			return false;
		}
		if (Customers.CheckForMobilePhoneNumber(((Control)(object)txtPhoneNumber).Text.Trim().Equals("") ? "" : ((Control)(object)txtPhoneNumber).Text.Trim(), ((Control)(object)txtMobile).Text.Trim().Equals("") ? "" : ((Control)(object)txtMobile).Text.Trim(), ((Control)(object)txtMobileNumber2).Text.Trim().Equals("") ? "" : ((Control)(object)txtMobileNumber2).Text.Trim(), Adding ? "0" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true))
		{
			GlobalVariables.InformationMB.Show("رقم التليفون متواجد من قبل", "Phone Number Already Exist");
			return false;
		}
		if (dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("هذا المستخدم ليس له حساب تحليلي", "This User Has No SubAccountID ");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			return false;
		}
		if (((UltraToggleEditorBase)chkIsClosed).Checked && dtpClosedDate.Value == null)
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل تاريخ التوقف ", "Please Enter Closed Date");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((Control)(object)dtpClosedDate).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkIsClosed).Checked && dtpCreationDate.DateTime > dtpClosedDate.DateTime)
		{
			GlobalVariables.InformationMB.Show("تاريخ التوقف قبل تاريخ انشاء العميل", "Closed Date is Before Creation Date");
			((Control)(object)dtpClosedDate).Focus();
			return false;
		}
		if (!((Control)(object)txtEMail).Text.Trim().Equals("") && !GlobalFunctions.IsEmailValid(((Control)(object)txtEMail).Text.Trim()))
		{
			GlobalVariables.InformationMB.Show("من فضلك ادخل بريد اليكتروني صحيح ", "Please Enter A Valid E-mail Address");
			((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Details"];
			((TextEditorControlBase)txtEMail).Focus();
			return false;
		}
		if (((DataTable)((UltraGridBase)ULGCustQuestions).DataSource).Rows.Count > 0)
		{
			for (int i = 0; i < ((DataTable)((UltraGridBase)ULGCustQuestions).DataSource).Rows.Count; i++)
			{
				if (((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("من فضلك أختر سؤال", "Select Question");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Questions"];
					ULGCustQuestions.ActiveCell = ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionID"];
					return false;
				}
				if (Convert.ToBoolean(dtQuestions.Select("QuestionID = " + ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionID"].Value.ToString())[0]["IsBoolean"]))
				{
					if (((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswerBool"].Value.Equals("") || ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswerBool"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show("من فضلك أختر أجابة", "Select An Answer");
						((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Questions"];
						ULGCustQuestions.ActiveCell = ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswerBool"];
						((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswerBool"].DroppedDown = true;
						return false;
					}
				}
				else if (((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswertxt"].Value.Equals("") || ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswertxt"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("من فضلك أختر أجابة", "Select An Answer");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Questions"];
					ULGCustQuestions.ActiveCell = ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionAnswertxt"];
					return false;
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustQuestions).Rows).Count; j++)
				{
					if (UsingBatchNoAndValidityPeriod && i != j && ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionID"].Value.ToString() == ((UltraGridBase)ULGCustQuestions).Rows[j].Cells["QuestionID"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show("لا يمكن تكرار نفس السؤال", "Cannot Duplicate The Same Question");
						((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Questions"];
						ULGCustQuestions.ActiveCell = ((UltraGridBase)ULGCustQuestions).Rows[i].Cells["QuestionID"];
						return false;
					}
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"].Value == DBNull.Value)
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"];
				((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"].DroppedDown = true;
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (UsingColors && (((UltraGridBase)ULGCustItems).Rows[k].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGCustItems).Rows[k].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["ColorID"];
				((UltraGridBase)ULGCustItems).Rows[k].Cells["ColorID"].DroppedDown = true;
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (UsingSizes && (((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemSizeID"];
				((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemSizeID"].DroppedDown = true;
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (((UltraGridBase)ULGCustItems).Rows[k].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGCustItems).Rows[k].Cells["Qty"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["Qty"];
				ULGCustItems.PerformAction((UltraGridAction)24);
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitID"];
				ULGCustItems.PerformAction((UltraGridAction)24);
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitPrice"].Value == DBNull.Value || Math.Round(decimal.Parse(((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitPrice"].Value.ToString()), 8) <= 0m)
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر الوحدة  ", "Please Enter Unit Price ");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitPrice"];
				ULGCustItems.PerformAction((UltraGridAction)24);
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (((UltraGridBase)ULGCustItems).Rows[k].Cells["TotalPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitPrice"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء إدخال إجمالى السعر  ", "Please Enter Total price");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["TotalPrice"];
				ULGCustItems.PerformAction((UltraGridAction)24);
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			if (UsingBatchNoAndValidityPeriod && bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGCustItems).Rows[k].Cells["BatchID"].Value == DBNull.Value)
			{
				((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
				GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
				((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
				ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["BatchID"];
				ULGCustItems.PerformAction((UltraGridAction)24);
				((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
				return false;
			}
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; l++)
			{
				if (UsingBatchNoAndValidityPeriod && k != l && ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGCustItems).Rows[l].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGCustItems).Rows[k].Cells["BatchID"].Value.ToString() == ((UltraGridBase)ULGCustItems).Rows[l].Cells["BatchID"].Value.ToString() && ((UltraGridBase)ULGCustItems).Rows[k].Cells["UnitID"].Value.ToString() == ((UltraGridBase)ULGCustItems).Rows[l].Cells["UnitID"].Value.ToString())
				{
					((Control)(object)ULGCustItems).Enter -= ULGCustItems_Enter;
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف مع رقم التشيغلة", "Cannot Duplicate The Same Item With the Same Batch No ");
					((UltraTabControlBase)tabItemType).SelectedTab = ((UltraTabControlBase)tabItemType).Tabs["Items"];
					ULGCustItems.ActiveCell = ((UltraGridBase)ULGCustItems).Rows[k].Cells["ItemID"];
					((Control)(object)ULGCustItems).Enter += ULGCustItems_Enter;
					return false;
				}
			}
		}
		return true;
	}

	public virtual void ULGCustItems_Enter(object sender, EventArgs e)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Invalid comparison between Unknown and I4
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Invalid comparison between Unknown and I4
		if (!Adding && !Updating)
		{
			return;
		}
		int num = -1;
		for (int num2 = ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns).Count - 1; num2 >= 0; num2--)
		{
			if (num == -1 && !((UltraGridBase)ULGCustItems).DisplayLayout.Bands[0].Columns[num2].Hidden)
			{
				num = num2;
				break;
			}
		}
		if (((int)((UltraGridBase)ULGCustItems).DisplayLayout.Override.AllowAddNew == 1 || (int)((UltraGridBase)ULGCustItems).DisplayLayout.Override.AllowAddNew == 6) && Adding)
		{
			((UltraGridBase)ULGCustItems).Rows.TemplateAddRow.Cells[num].Activate();
			ULGCustItems.PerformAction((UltraGridAction)24);
		}
		else if ((Adding || Updating) && ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count > 0)
		{
			((UltraGridBase)ULGCustItems).Rows[0].Cells[num].Activate();
			ULGCustItems.PerformAction((UltraGridAction)24);
		}
	}

	public override bool HasTransactionValidation()
	{
		string text = Customers.SelectRelations(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return base.HasTransactionValidation();
	}

	private void Tree_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		((UltraTree)sender).AfterCheck -= new AfterNodeChangedEventHandler(Tree_AfterCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraTree)sender).AfterCheck += new AfterNodeChangedEventHandler(Tree_AfterCheck);
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.CRMCustomers(-1, IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	public override void btnPrintClick()
	{
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_CRM_Customers_A.rpt" : "Rep_CRM_Customers_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@CustomerIDs", "-1");
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	private void cboCity_ValueChanged(object sender, EventArgs e)
	{
		if (cboCity.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtAreas);
			dataView.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void changeParentTSMenu_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateParent frmUpdateParent2 = new frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
			((Control)(object)frmUpdateParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير المجموعه" : "Update Group");
			frmUpdateParent2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void setAsGroupToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!HasTransactionValidation())
		{
			Customers.UpdateIsMain(((KeyedSubObjectBase)SelectedNode).Key, "1", GlobalVariables.UserID, IsFromServer: true);
			DataRow dataRow = dtChart.Select(IDCol + "=" + ((KeyedSubObjectBase)SelectedNode).Key)[0];
			dataRow[IsMainCol] = 1;
			((SubObjectBase)SelectedNode).Tag = dataRow;
			SelectedNode.Override.NodeAppearance.Image = Resources.folderfortree;
			SelectedNode.ExpandAll();
			changeParentTSMenu.Enabled = false;
			setAsGroupToolStripMenuItem.Enabled = false;
			setAsSubAccountToolStripMenuItem.Enabled = true;
		}
	}

	private void setAsSubAccountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (((DisposableObjectCollectionBase)SelectedNode.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن تحويله لعميل لوجود عناصر تحته", "Cannot set this Node as a Customer It Has Sub Nodes");
		}
		else
		{
			CreateSubAccount();
		}
	}

	private void CalculateGoss()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGCustItems).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtGrossValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateTotalsTax()
	{
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGCustItems).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGCustItems).Rows[i].Cells["TaxValue"].Value.ToString());
			num2 += decimal.Parse(((UltraGridBase)ULGCustItems).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtTaxTotalValue).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtTotalPrice).Text = (num + num2).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(Row.Cells["TotalPrice"].Value.ToString());
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
		Row.Cells["NetPrice"].Value = decimal.Parse(Row.Cells["TotalPrice"].Value.ToString());
	}

	private ValueList getBatchsValueList(int ItemID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtBatchs.Select("ItemID is null or ItemID=" + ItemID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["BatchID"].ToString(), array[i]["BatchName"].ToString());
		}
		return val;
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

	private ValueList getColorsValueList(int ItemColorCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsColorCategorysDetails.Select("ItemColorCategoryID=" + ItemColorCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ColorID"].ToString(), dtColors.Select("ColorID =" + array[i]["ColorID"].ToString())[0]["ColorName"].ToString());
		}
		return val;
	}

	private ValueList getSizesValueList(int ItemSizeCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID=" + ItemSizeCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemSizeID"].ToString(), dtSizes.Select("ItemSizeID =" + array[i]["ItemSizeID"].ToString())[0]["ItemSizeName"].ToString());
		}
		return val;
	}

	private void ULGCustQuestions_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCustQuestions).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGCustQuestions.ActiveCell.Column).Key == "QuestionDate" || (((KeyedSubObjectBase)ULGCustQuestions.ActiveCell.Column).Key == "QuestionAnswerBool" && !bool.Parse(dtQuestions.Select(" QuestionID= " + ((UltraGridBase)ULGCustQuestions).ActiveRow.Cells["QuestionID"].Value.ToString())[0]["IsBoolean"].ToString())))
		{
			((GridItemBase)((UltraGridBase)ULGCustQuestions).ActiveRow).Selected = true;
		}
	}

	private void ULGCustItems_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "BatchID" && ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "NetPrice" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TaxValue" || ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
		else if (((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "UnitPrice" && !bool.Parse(dtItems.Select(" ItemID =" + ((UltraGridBase)ULGCustItems).ActiveRow.Cells["ItemID"].Value.ToString())[0]["CanModifyPrice"].ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "TaxID")
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGCustItems.ActiveCell.Column).Key == "ItemDate")
		{
			((GridItemBase)((UltraGridBase)ULGCustItems).ActiveRow).Selected = true;
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
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
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Expected O, but got Unknown
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected O, but got Unknown
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Expected O, but got Unknown
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Expected O, but got Unknown
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Expected O, but got Unknown
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Expected O, but got Unknown
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Expected O, but got Unknown
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Expected O, but got Unknown
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Expected O, but got Unknown
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Expected O, but got Unknown
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Expected O, but got Unknown
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Expected O, but got Unknown
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Expected O, but got Unknown
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Expected O, but got Unknown
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Expected O, but got Unknown
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Expected O, but got Unknown
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Expected O, but got Unknown
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Expected O, but got Unknown
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Expected O, but got Unknown
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Expected O, but got Unknown
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Expected O, but got Unknown
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Expected O, but got Unknown
		//IL_1f35: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3f: Expected O, but got Unknown
		//IL_1f4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f57: Expected O, but got Unknown
		//IL_24e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ee: Expected O, but got Unknown
		//IL_2544: Unknown result type (might be due to invalid IL or missing references)
		//IL_254e: Expected O, but got Unknown
		//IL_255c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2566: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CRM.MasterData.frmCustomersTree));
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
		Appearance val20 = new Appearance();
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
		Appearance val32 = new Appearance();
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
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		Appearance val46 = new Appearance();
		Appearance val47 = new Appearance();
		UltraTab val48 = new UltraTab();
		UltraTab val49 = new UltraTab();
		UltraTab val50 = new UltraTab();
		this.tabItem = new UltraTabPageControl();
		this.lblClientSubAccount = new UltraLabel();
		this.cboClientSubAccount = new UltraComboEditor();
		this.lblEmployeeSubAccount = new UltraLabel();
		this.cboDefaultSalesMan = new UltraComboEditor();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.chkIsClosed = new UltraCheckEditor();
		this.dtpCreationDate = new UltraDateTimeEditor();
		this.lblCreationDate = new UltraLabel();
		this.dtpClosedDate = new UltraDateTimeEditor();
		this.lblClosedDate = new UltraLabel();
		this.cboBranch = new UltraComboEditor();
		this.cboSegmentations = new UltraComboEditor();
		this.lblBranch = new UltraLabel();
		this.lblSegmentation = new UltraLabel();
		this.txtWorkCompanyName = new UltraTextEditor();
		this.lblWorkCompanyName = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.cboPriceType = new UltraComboEditor();
		this.cboDefaultPaymentMethod = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.cboReligion = new UltraComboEditor();
		this.cboGender = new UltraComboEditor();
		this.lblDefaultPaymentMethod = new UltraLabel();
		this.lblReligion = new UltraLabel();
		this.txtPhoneNumber = new UltraTextEditor();
		this.lblGender = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtEMail = new UltraTextEditor();
		this.txtMobile = new UltraTextEditor();
		this.lblPhoneNumber = new UltraLabel();
		this.txtAddress = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtMobileNumber2 = new UltraTextEditor();
		this.lblAddress = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.lblReorder = new UltraLabel();
		this.lblMobileNumber2 = new UltraLabel();
		this.tabRecipe = new UltraTabPageControl();
		this.ULGCustQuestions = new UltraGrid();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.txtTotalPrice = new UltraTextEditor();
		this.lblTotalPrice = new UltraLabel();
		this.txtTaxTotalValue = new UltraTextEditor();
		this.txtTotalQty = new UltraTextEditor();
		this.txtGrossValue = new UltraTextEditor();
		this.ULGCustItems = new UltraGrid();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.changeParentTSMenu = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.setAsSubAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.btnCreateClient = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboClientSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsClosed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCreationDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpClosedDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSegmentations).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkCompanyName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultPaymentMethod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPhoneNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNumber2).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGCustQuestions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		this.contextMenuStrip1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.treeChart, "treeChart");
		((System.Windows.Forms.Control)(object)base.treeChart).ContextMenuStrip = this.contextMenuStrip1;
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnAddRoot, "btnAddRoot");
		resources.ApplyResources(base.label1, "label1");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance48");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label2, "label2");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance49");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label3, "label3");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance50");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
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
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClientSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboClientSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEmployeeSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsClosed);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpCreationDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCreationDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.dtpClosedDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblClosedDate);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboBranch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSegmentations);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblSegmentation);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkCompanyName);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblWorkCompanyName);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboReligion);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReligion);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtPhoneNumber);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblPhoneNumber);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtMobileNumber2);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReorder);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblMobileNumber2);
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.lblClientSubAccount, "lblClientSubAccount");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance51");
		((ControlBase)this.lblClientSubAccount).Appearance = (AppearanceBase)(object)val5;
		this.lblClientSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientSubAccount).Name = "lblClientSubAccount";
		((ControlBase)this.lblClientSubAccount).WrapText = false;
		resources.ApplyResources(this.cboClientSubAccount, "cboClientSubAccount");
		((System.Windows.Forms.Control)(object)this.cboClientSubAccount).Name = "cboClientSubAccount";
		((EditorButtonControlBase)this.cboClientSubAccount).ReadOnly = true;
		resources.ApplyResources(this.lblEmployeeSubAccount, "lblEmployeeSubAccount");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance52");
		((ControlBase)this.lblEmployeeSubAccount).Appearance = (AppearanceBase)(object)val6;
		this.lblEmployeeSubAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEmployeeSubAccount).Name = "lblEmployeeSubAccount";
		((ControlBase)this.lblEmployeeSubAccount).WrapText = false;
		resources.ApplyResources(this.cboDefaultSalesMan, "cboDefaultSalesMan");
		((System.Windows.Forms.Control)(object)this.cboDefaultSalesMan).Name = "cboDefaultSalesMan";
		((EditorButtonControlBase)this.cboDefaultSalesMan).ReadOnly = true;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance53");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val7;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.chkIsClosed, "chkIsClosed");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance54");
		((UltraToggleEditorBase)this.chkIsClosed).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.chkIsClosed).Name = "chkIsClosed";
		resources.ApplyResources(this.dtpCreationDate, "dtpCreationDate");
		((UltraWinEditorMaskedControlBase)this.dtpCreationDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCreationDate).Name = "dtpCreationDate";
		((EditorButtonControlBase)this.dtpCreationDate).ReadOnly = true;
		resources.ApplyResources(this.lblCreationDate, "lblCreationDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance55");
		((ControlBase)this.lblCreationDate).Appearance = (AppearanceBase)(object)val9;
		this.lblCreationDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCreationDate).Name = "lblCreationDate";
		((ControlBase)this.lblCreationDate).WrapText = false;
		resources.ApplyResources(this.dtpClosedDate, "dtpClosedDate");
		((UltraWinEditorMaskedControlBase)this.dtpClosedDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpClosedDate).Name = "dtpClosedDate";
		resources.ApplyResources(this.lblClosedDate, "lblClosedDate");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance56");
		((ControlBase)this.lblClosedDate).Appearance = (AppearanceBase)(object)val10;
		this.lblClosedDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClosedDate).Name = "lblClosedDate";
		((ControlBase)this.lblClosedDate).WrapText = false;
		resources.ApplyResources(this.cboBranch, "cboBranch");
		((System.Windows.Forms.Control)(object)this.cboBranch).Name = "cboBranch";
		resources.ApplyResources(this.cboSegmentations, "cboSegmentations");
		((System.Windows.Forms.Control)(object)this.cboSegmentations).Name = "cboSegmentations";
		resources.ApplyResources(this.lblBranch, "lblBranch");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance57");
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val11;
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.lblSegmentation, "lblSegmentation");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance58");
		((ControlBase)this.lblSegmentation).Appearance = (AppearanceBase)(object)val12;
		this.lblSegmentation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSegmentation).Name = "lblSegmentation";
		((ControlBase)this.lblSegmentation).WrapText = false;
		resources.ApplyResources(this.txtWorkCompanyName, "txtWorkCompanyName");
		((System.Windows.Forms.Control)(object)this.txtWorkCompanyName).Name = "txtWorkCompanyName";
		resources.ApplyResources(this.lblWorkCompanyName, "lblWorkCompanyName");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance59");
		((ControlBase)this.lblWorkCompanyName).Appearance = (AppearanceBase)(object)val13;
		this.lblWorkCompanyName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWorkCompanyName).Name = "lblWorkCompanyName";
		((ControlBase)this.lblWorkCompanyName).WrapText = false;
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance60");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val14;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance61");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val15;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		((TextEditorControlBase)this.cboPriceType).ValueChanged += new System.EventHandler(cboPriceType_ValueChanged);
		resources.ApplyResources(this.cboDefaultPaymentMethod, "cboDefaultPaymentMethod");
		((System.Windows.Forms.Control)(object)this.cboDefaultPaymentMethod).Name = "cboDefaultPaymentMethod";
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance62");
		((ControlBase)this.lblPriceType).Appearance = (AppearanceBase)(object)val16;
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.cboReligion, "cboReligion");
		((System.Windows.Forms.Control)(object)this.cboReligion).Name = "cboReligion";
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblDefaultPaymentMethod, "lblDefaultPaymentMethod");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance63");
		((ControlBase)this.lblDefaultPaymentMethod).Appearance = (AppearanceBase)(object)val17;
		this.lblDefaultPaymentMethod.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDefaultPaymentMethod).Name = "lblDefaultPaymentMethod";
		((ControlBase)this.lblDefaultPaymentMethod).WrapText = false;
		resources.ApplyResources(this.lblReligion, "lblReligion");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance64");
		((ControlBase)this.lblReligion).Appearance = (AppearanceBase)(object)val18;
		this.lblReligion.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReligion).Name = "lblReligion";
		((ControlBase)this.lblReligion).WrapText = false;
		resources.ApplyResources(this.txtPhoneNumber, "txtPhoneNumber");
		((System.Windows.Forms.Control)(object)this.txtPhoneNumber).Name = "txtPhoneNumber";
		resources.ApplyResources(this.lblGender, "lblGender");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance65");
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val19;
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		resources.ApplyResources(this.lblPhoneNumber, "lblPhoneNumber");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance66");
		((ControlBase)this.lblPhoneNumber).Appearance = (AppearanceBase)(object)val20;
		this.lblPhoneNumber.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPhoneNumber).Name = "lblPhoneNumber";
		((ControlBase)this.lblPhoneNumber).WrapText = false;
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance67");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val21;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtMobileNumber2, "txtMobileNumber2");
		((System.Windows.Forms.Control)(object)this.txtMobileNumber2).Name = "txtMobileNumber2";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance68");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val22;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance69");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val23;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		resources.ApplyResources(this.lblReorder, "lblReorder");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance70");
		((ControlBase)this.lblReorder).Appearance = (AppearanceBase)(object)val24;
		this.lblReorder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReorder).Name = "lblReorder";
		((ControlBase)this.lblReorder).WrapText = false;
		resources.ApplyResources(this.lblMobileNumber2, "lblMobileNumber2");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance71");
		((ControlBase)this.lblMobileNumber2).Appearance = (AppearanceBase)(object)val25;
		this.lblMobileNumber2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobileNumber2).Name = "lblMobileNumber2";
		((ControlBase)this.lblMobileNumber2).WrapText = false;
		resources.ApplyResources(this.tabRecipe, "tabRecipe");
		((System.Windows.Forms.Control)(object)this.tabRecipe).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCustQuestions);
		((System.Windows.Forms.Control)(object)this.tabRecipe).Name = "tabRecipe";
		resources.ApplyResources(this.ULGCustQuestions, "ULGCustQuestions");
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val26).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val26).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((SpecialBoxBase)((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val27, "appearance27");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val27;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val29, "appearance29");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val29;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val30).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val30, "appearance30");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val31, "appearance31");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val32, "appearance32");
		((AppearanceBase)val32).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val33).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val33).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val33, "appearance33");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val34).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val34, "appearance34");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val35, "appearance35");
		((UltraGridBase)this.ULGCustQuestions).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.ULGCustQuestions).Name = "ULGCustQuestions";
		this.ULGCustQuestions.AfterEnterEditMode += new System.EventHandler(ULGCustQuestions_AfterEnterEditMode);
		this.ULGCustQuestions.CellListSelect += new CellEventHandler(ULGCustQuestions_CellListSelect);
		this.ULGCustQuestions.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGCustQuestions_BeforeRowsDeleted);
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalPrice);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtTaxTotalValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.txtGrossValue);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGCustItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		resources.ApplyResources(this.lblTotalPrice, "lblTotalPrice");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val36, "appearance72");
		((ControlBase)this.lblTotalPrice).Appearance = (AppearanceBase)(object)val36;
		this.lblTotalPrice.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTotalPrice).Name = "lblTotalPrice";
		((ControlBase)this.lblTotalPrice).WrapText = false;
		resources.ApplyResources(this.txtTaxTotalValue, "txtTaxTotalValue");
		((System.Windows.Forms.Control)(object)this.txtTaxTotalValue).Name = "txtTaxTotalValue";
		((EditorButtonControlBase)this.txtTaxTotalValue).ReadOnly = true;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((EditorButtonControlBase)this.txtTotalQty).ReadOnly = true;
		resources.ApplyResources(this.txtGrossValue, "txtGrossValue");
		((System.Windows.Forms.Control)(object)this.txtGrossValue).Name = "txtGrossValue";
		((EditorButtonControlBase)this.txtGrossValue).ReadOnly = true;
		resources.ApplyResources(this.ULGCustItems, "ULGCustItems");
		((AppearanceBase)val37).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val37).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val37).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val37).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val37, "appearance37");
		((SpecialBoxBase)((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val37;
		((AppearanceBase)val38).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val38, "appearance38");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val38;
		((SpecialBoxBase)((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val39).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val39).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val39).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val39, "appearance39");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val39;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val40).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val40, "appearance40");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val40;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val41).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val41, "appearance41");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val41;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val42).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val42, "appearance42");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val42;
		((AppearanceBase)val43).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val43, "appearance43");
		((AppearanceBase)val43).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val44).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val44).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val44).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val44).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val44, "appearance44");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val45).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val45, "appearance45");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val45;
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val46).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val46, "appearance46");
		((UltraGridBase)this.ULGCustItems).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val46;
		((System.Windows.Forms.Control)(object)this.ULGCustItems).Name = "ULGCustItems";
		this.ULGCustItems.AfterCellUpdate += new CellEventHandler(ULGCustItems_AfterCellUpdate);
		this.ULGCustItems.AfterEnterEditMode += new System.EventHandler(ULGCustItems_AfterEnterEditMode);
		this.ULGCustItems.AfterExitEditMode += new System.EventHandler(ULGCustItems_AfterExitEditMode);
		this.ULGCustItems.AfterRowsDeleted += new System.EventHandler(ULGCustItems_AfterRowsDeleted);
		this.ULGCustItems.CellListSelect += new CellEventHandler(ULGCustItems_CellListSelect);
		this.ULGCustItems.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGCustItems_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGCustItems).Enter += new System.EventHandler(ULGCustItems_Enter);
		((System.Windows.Forms.Control)(object)this.ULGCustItems).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGCustItems_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGCustItems).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGCustItems_KeyPress);
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val47).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val47, "appearance47");
		((AppearanceBase)val47).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val47;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabRecipe);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val48).Key = "Details";
		val48.TabPage = this.tabItem;
		resources.ApplyResources(val48, "ultraTab2");
		((SubObjectBase)val48).ForceApplyResources = "";
		((KeyedSubObjectBase)val49).Key = "Questions";
		val49.TabPage = this.tabRecipe;
		resources.ApplyResources(val49, "ultraTab3");
		((SubObjectBase)val49).ForceApplyResources = "";
		((KeyedSubObjectBase)val50).Key = "Items";
		val50.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val50, "ultraTab6");
		((SubObjectBase)val50).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val48, val49, val50 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.changeParentTSMenu, this.setAsGroupToolStripMenuItem, this.setAsSubAccountToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.changeParentTSMenu, "changeParentTSMenu");
		this.changeParentTSMenu.Name = "changeParentTSMenu";
		this.changeParentTSMenu.Click += new System.EventHandler(changeParentTSMenu_Click);
		resources.ApplyResources(this.setAsGroupToolStripMenuItem, "setAsGroupToolStripMenuItem");
		this.setAsGroupToolStripMenuItem.Name = "setAsGroupToolStripMenuItem";
		this.setAsGroupToolStripMenuItem.Click += new System.EventHandler(setAsGroupToolStripMenuItem_Click);
		resources.ApplyResources(this.setAsSubAccountToolStripMenuItem, "setAsSubAccountToolStripMenuItem");
		this.setAsSubAccountToolStripMenuItem.Name = "setAsSubAccountToolStripMenuItem";
		this.setAsSubAccountToolStripMenuItem.Click += new System.EventHandler(setAsSubAccountToolStripMenuItem_Click);
		resources.ApplyResources(this.btnCreateClient, "btnCreateClient");
		((System.Windows.Forms.Control)(object)this.btnCreateClient).Name = "btnCreateClient";
		((System.Windows.Forms.Control)(object)this.btnCreateClient).Click += new System.EventHandler(btnCreateClient_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateClient);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Name = "frmCustomersTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCreateClient, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboClientSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultSalesMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsClosed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCreationDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpClosedDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSegmentations).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkCompanyName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDefaultPaymentMethod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPhoneNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobileNumber2).EndInit();
		((System.Windows.Forms.Control)(object)this.tabRecipe).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGCustQuestions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaxTotalValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGrossValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCustItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		this.contextMenuStrip1.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
