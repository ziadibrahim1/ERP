using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Constructions;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Constructions.MasterData;

public class frmProjects : frmGrid
{
	private DataTable dtCountries;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtAccounts;

	private DataTable dtCostCenters;

	private bool UseCostCenters;

	private ValueList vlCostCenters = new ValueList();

	private ValueList vlSalesAccounts = new ValueList();

	private ValueList vlSalesReturnsAccounts = new ValueList();

	private ValueList vlMaintenanceAccounts = new ValueList();

	private ValueList vlMaintenanceReturnsAccounts = new ValueList();

	private ValueList vlFinishingAccounts = new ValueList();

	private ValueList vlFinishingReturnsAccounts = new ValueList();

	private ValueList vlCountries = new ValueList();

	private ValueList vlCities = new ValueList();

	private ValueList vlAreas = new ValueList();

	private IContainer components = null;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblCode;

	private UltraTextEditor txtCode;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboCountry;

	private UltraLabel lblCountry;

	private UltraComboEditor cboArea;

	private UltraLabel lblArea;

	private UltraComboEditor cboCity;

	private UltraLabel lblCity;

	private UltraLabel lblCostCenter;

	private UltraComboEditor cboCostCenter;

	private UltraCheckEditor chkClosed;

	public UltraButton btnSalesAccountSearch;

	private UltraComboEditor cboSalesAccount;

	private UltraLabel lblSalesAccount;

	public UltraButton btnSalesReturnsAccountSearch;

	private UltraComboEditor cboSalesReturnsAccounts;

	private UltraLabel lblSalesReturnsAccount;

	private UltraLabel lblMaintenanceSalesAccount;

	private UltraComboEditor cboMaintenanceSalesAccount;

	public UltraButton btnMaintenanceSalesAccountSearch;

	private UltraLabel lblMaintenanceSalesReturnsAccount;

	private UltraComboEditor cboMaintenanceSalesReturnsAccount;

	public UltraButton btnMaintenanceSalesReturnsAccountSearch;

	private UltraLabel lblFinishingSalesAccount;

	private UltraComboEditor cboFinishingSalesAccount;

	public UltraButton btnFinishingSalesAccountSearch;

	private UltraLabel lblFinishingSalesReturnsAccount;

	private UltraComboEditor cboFinishingSalesReturnsAccount;

	public UltraButton btnFinishingSalesReturnsAccountSearch;

	public frmProjects()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Con_Projects";
		IDCol = "ProjectID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		vlCountries.ValueListItems.Clear();
		for (int i = 0; i < dtCountries.Rows.Count; i++)
		{
			vlCountries.ValueListItems.Add(dtCountries.Rows[i]["CountryID"], dtCountries.Rows[i]["CountryName"].ToString());
		}
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		vlCities.ValueListItems.Clear();
		for (int j = 0; j < dtCities.Rows.Count; j++)
		{
			vlCities.ValueListItems.Add(dtCities.Rows[j]["CityID"], dtCities.Rows[j]["CityName"].ToString());
		}
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		vlAreas.ValueListItems.Clear();
		for (int k = 0; k < dtAreas.Rows.Count; k++)
		{
			vlAreas.ValueListItems.Add(dtAreas.Rows[k]["AreaID"], dtAreas.Rows[k]["AreaName"].ToString());
		}
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboSalesReturnsAccounts, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboMaintenanceSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboMaintenanceSalesReturnsAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboFinishingSalesAccount, dtAccounts, "AccountID", "Name");
		GlobalFunctions.FillCombo(cboFinishingSalesReturnsAccount, dtAccounts, "AccountID", "Name");
		vlSalesAccounts.ValueListItems.Clear();
		vlSalesReturnsAccounts.ValueListItems.Clear();
		vlMaintenanceAccounts.ValueListItems.Clear();
		vlMaintenanceReturnsAccounts.ValueListItems.Clear();
		vlFinishingAccounts.ValueListItems.Clear();
		vlFinishingReturnsAccounts.ValueListItems.Clear();
		for (int l = 0; l < dtAccounts.Rows.Count; l++)
		{
			vlSalesAccounts.ValueListItems.Add(dtAccounts.Rows[l]["AccountID"], dtAccounts.Rows[l]["Name"].ToString());
			vlSalesReturnsAccounts.ValueListItems.Add(dtAccounts.Rows[l]["AccountID"], dtAccounts.Rows[l]["Name"].ToString());
			vlMaintenanceAccounts.ValueListItems.Add(dtAccounts.Rows[l]["AccountID"], dtAccounts.Rows[l]["Name"].ToString());
			vlMaintenanceReturnsAccounts.ValueListItems.Add(dtAccounts.Rows[l]["AccountID"], dtAccounts.Rows[l]["Name"].ToString());
			vlFinishingAccounts.ValueListItems.Add(dtAccounts.Rows[l]["AccountID"], dtAccounts.Rows[l]["Name"].ToString());
			vlFinishingReturnsAccounts.ValueListItems.Add(dtAccounts.Rows[l]["AccountID"], dtAccounts.Rows[l]["Name"].ToString());
		}
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as CC From A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		if (UseCostCenters)
		{
			((Control)(object)lblCostCenter).Visible = true;
			((Control)(object)cboCostCenter).Visible = true;
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
			vlCostCenters.ValueListItems.Clear();
			for (int m = 0; m < dtCostCenters.Rows.Count; m++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[m]["CostCenterID"], dtCostCenters.Rows[m]["Name"].ToString());
			}
		}
		else
		{
			((Control)(object)lblCostCenter).Visible = false;
			((Control)(object)cboCostCenter).Visible = false;
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesReturnsAccounts).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMaintenanceSalesAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMaintenanceSalesReturnsAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFinishingSalesAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFinishingSalesReturnsAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCostCenter).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)chkClosed).Enabled = !NavMode;
		((TextEditorControlBase)txtCode).Focus();
		((Control)(object)btnSalesAccountSearch).Visible = !NavMode;
		((Control)(object)btnSalesReturnsAccountSearch).Visible = !NavMode;
		((Control)(object)btnMaintenanceSalesAccountSearch).Visible = !NavMode;
		((Control)(object)btnMaintenanceSalesReturnsAccountSearch).Visible = !NavMode;
		((Control)(object)btnFinishingSalesAccountSearch).Visible = !NavMode;
		((Control)(object)btnFinishingSalesReturnsAccountSearch).Visible = !NavMode;
		if (Updating && cboCountry.SelectedIndex != -1)
		{
			object value = ((TextEditorControlBase)cboCity).Value;
			DataView dataView = new DataView(dtCities);
			dataView.RowFilter = "CountryID=" + ((TextEditorControlBase)cboCountry).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboCity.DataSource = dataView;
			cboCity.DisplayMember = "CityName";
			cboCity.ValueMember = "CityID";
			((TextEditorControlBase)cboCity).Value = value;
		}
		else
		{
			GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		}
		if (Updating && cboCity.SelectedIndex != -1)
		{
			object value2 = ((TextEditorControlBase)cboArea).Value;
			DataView dataView2 = new DataView(dtAreas);
			dataView2.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView2.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView2;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
			((TextEditorControlBase)cboArea).Value = value2;
		}
		else
		{
			GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
		}
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((Control)(object)txtCode).Text = Projects.GetCode(IsFromServer: true);
		cboCountry.SelectedIndex = ((dtCountries.Rows.Count <= 0) ? (-1) : 0);
		cboCity.SelectedIndex = -1;
		cboArea.SelectedIndex = -1;
		cboSalesAccount.SelectedIndex = -1;
		cboSalesReturnsAccounts.SelectedIndex = -1;
		cboMaintenanceSalesAccount.SelectedIndex = -1;
		cboMaintenanceSalesReturnsAccount.SelectedIndex = -1;
		cboFinishingSalesAccount.SelectedIndex = -1;
		cboFinishingSalesReturnsAccount.SelectedIndex = -1;
		cboCostCenter.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((UltraToggleEditorBase)chkClosed).Checked = false;
	}

	public override void FillData()
	{
		dataTable = Projects.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectCode"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProjectNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب المبيعات" : "Sales Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesAccountID"].ValueList = (IValueList)(object)vlSalesAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesReturnsAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "مردودات المبيعات" : "Sales Returns");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesReturnsAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesReturnsAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SalesReturnsAccountID"].ValueList = (IValueList)(object)vlSalesReturnsAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب الصيانه" : "Maintenance Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesAccountID"].ValueList = (IValueList)(object)vlMaintenanceAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesReturnsAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "مردودات الصيانه" : "Maintenance Returns");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesReturnsAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesReturnsAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaintenanceSalesReturnsAccountID"].ValueList = (IValueList)(object)vlMaintenanceReturnsAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب التشطيبات" : "Finishing Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesAccountID"].ValueList = (IValueList)(object)vlFinishingAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesReturnsAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "مردودات التشطيبات" : "Finishing Returns");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesReturnsAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesReturnsAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishingSalesReturnsAccountID"].ValueList = (IValueList)(object)vlFinishingReturnsAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة" : "Cost Center");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = !UseCostCenters;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryID"].Header).Caption = (GlobalVariables.IsArabic ? "البلد" : "Country");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CountryID"].ValueList = (IValueList)(object)vlCountries;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Header).Caption = (GlobalVariables.IsArabic ? "المحافظة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].ValueList = (IValueList)(object)vlCities;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Header).Caption = (GlobalVariables.IsArabic ? "المنطقة" : "Area");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].ValueList = (IValueList)(object)vlAreas;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Header).Caption = (GlobalVariables.IsArabic ? "مغلق" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Closed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ProjectCode"].Value.ToString();
		((Control)(object)txtNameAr).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ProjectNameAr"].Value.ToString();
		((Control)(object)txtNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ProjectNameEn"].Value.ToString();
		((TextEditorControlBase)cboCostCenter).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CostCenterID"].Value;
		((TextEditorControlBase)cboCountry).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CountryID"].Value;
		((TextEditorControlBase)cboCity).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CityID"].Value;
		((TextEditorControlBase)cboArea).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value;
		((TextEditorControlBase)cboSalesAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SalesAccountID"].Value;
		((TextEditorControlBase)cboSalesReturnsAccounts).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SalesReturnsAccountID"].Value;
		((TextEditorControlBase)cboMaintenanceSalesAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaintenanceSalesAccountID"].Value;
		((TextEditorControlBase)cboMaintenanceSalesReturnsAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["MaintenanceSalesReturnsAccountID"].Value;
		((TextEditorControlBase)cboFinishingSalesAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["FinishingSalesAccountID"].Value;
		((TextEditorControlBase)cboFinishingSalesReturnsAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["FinishingSalesReturnsAccountID"].Value;
		((Control)(object)txtNotes).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Notes"].Value.ToString();
		((UltraToggleEditorBase)chkClosed).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Closed"].Value.ToString());
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود  ", "Please Insert Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Insert Arabic Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (cboSalesAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب المبيعات", "Please Select Sales Account");
			((TextEditorControlBase)cboSalesAccount).Focus();
			return false;
		}
		if (cboSalesReturnsAccounts.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مردودات المبيعات", "Please Select  Sales Retuns Account");
			((TextEditorControlBase)cboSalesReturnsAccounts).Focus();
			return false;
		}
		if (cboMaintenanceSalesAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب الصيانه", "Please Select Maintenance Sales Account");
			((TextEditorControlBase)cboMaintenanceSalesAccount).Focus();
			return false;
		}
		if (cboMaintenanceSalesReturnsAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب  مردودات الصيانه", "Please Select Maintenance Sales Returns Account");
			((TextEditorControlBase)cboMaintenanceSalesReturnsAccount).Focus();
			return false;
		}
		if (cboFinishingSalesAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب التشطيبات", "Please Select Finishing Sales Account");
			((TextEditorControlBase)cboFinishingSalesAccount).Focus();
			return false;
		}
		if (cboFinishingSalesReturnsAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار حساب مردودات التشطيبات", "Please Select Finishing Sales Returns Account");
			((TextEditorControlBase)cboFinishingSalesReturnsAccount).Focus();
			return false;
		}
		if (cboCountry.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار البلد", "Please Select Country");
			((TextEditorControlBase)cboCountry).Focus();
			return false;
		}
		if (cboCity.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المحافظة", "Please Select City");
			((TextEditorControlBase)cboCity).Focus();
			return false;
		}
		if (cboArea.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المنطقة", "Please Select Area");
			((TextEditorControlBase)cboArea).Focus();
			return false;
		}
		if (cboCostCenter.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مركز التكلفة", "Please Select Cost Center");
			((TextEditorControlBase)cboCostCenter).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Projects.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString(), (cboSalesReturnsAccounts.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesReturnsAccounts).Value.ToString(), (cboMaintenanceSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaintenanceSalesAccount).Value.ToString(), (cboMaintenanceSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaintenanceSalesReturnsAccount).Value.ToString(), (cboFinishingSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFinishingSalesAccount).Value.ToString(), (cboFinishingSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFinishingSalesReturnsAccount).Value.ToString(), ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Projects.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["ProjectID"].Value.ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString(), (cboSalesReturnsAccounts.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesReturnsAccounts).Value.ToString(), (cboMaintenanceSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaintenanceSalesAccount).Value.ToString(), (cboMaintenanceSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboMaintenanceSalesReturnsAccount).Value.ToString(), (cboFinishingSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFinishingSalesAccount).Value.ToString(), (cboFinishingSalesReturnsAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboFinishingSalesReturnsAccount).Value.ToString(), ((Control)(object)txtNotes).Text, ((UltraToggleEditorBase)chkClosed).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	private void btnSalesReturnsAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesReturnsAccounts).Value = num;
		}
	}

	private void btnMaintenanceSalesReturnsAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboMaintenanceSalesReturnsAccount).Value = num;
		}
	}

	private void btnFinishingSalesReturnsAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboFinishingSalesReturnsAccount).Value = num;
		}
	}

	private void btnMaintenanceSalesAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboMaintenanceSalesAccount).Value = num;
		}
	}

	private void btnFinishingSalesAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboFinishingSalesAccount).Value = num;
		}
	}

	public override void DeleteData()
	{
		Projects.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["ProjectID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
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

	private void btnSalesAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboSalesAccount).Value = num;
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
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Expected O, but got Unknown
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Expected O, but got Unknown
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Expected O, but got Unknown
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Expected O, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Constructions.MasterData.frmProjects));
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
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboCountry = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.cboArea = new UltraComboEditor();
		this.lblArea = new UltraLabel();
		this.cboCity = new UltraComboEditor();
		this.lblCity = new UltraLabel();
		this.lblCostCenter = new UltraLabel();
		this.cboCostCenter = new UltraComboEditor();
		this.chkClosed = new UltraCheckEditor();
		this.btnSalesAccountSearch = new UltraButton();
		this.cboSalesAccount = new UltraComboEditor();
		this.lblSalesAccount = new UltraLabel();
		this.btnSalesReturnsAccountSearch = new UltraButton();
		this.cboSalesReturnsAccounts = new UltraComboEditor();
		this.lblSalesReturnsAccount = new UltraLabel();
		this.lblMaintenanceSalesAccount = new UltraLabel();
		this.cboMaintenanceSalesAccount = new UltraComboEditor();
		this.btnMaintenanceSalesAccountSearch = new UltraButton();
		this.lblMaintenanceSalesReturnsAccount = new UltraLabel();
		this.cboMaintenanceSalesReturnsAccount = new UltraComboEditor();
		this.btnMaintenanceSalesReturnsAccountSearch = new UltraButton();
		this.lblFinishingSalesAccount = new UltraLabel();
		this.cboFinishingSalesAccount = new UltraComboEditor();
		this.btnFinishingSalesAccountSearch = new UltraButton();
		this.lblFinishingSalesReturnsAccount = new UltraLabel();
		this.cboFinishingSalesReturnsAccount = new UltraComboEditor();
		this.btnFinishingSalesReturnsAccountSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesReturnsAccounts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaintenanceSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaintenanceSalesReturnsAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFinishingSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFinishingSalesReturnsAccount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
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
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance21");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtNameEn).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblCode, "lblCode");
		this.lblCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.cboCountry, "cboCountry");
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance22");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val11;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		resources.ApplyResources(this.cboArea, "cboArea");
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val12, "appearance23");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val12;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		resources.ApplyResources(this.cboCity, "cboCity");
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val13, "appearance24");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val13;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		((TextEditorControlBase)this.cboCostCenter).AlwaysInEditMode = true;
		this.cboCostCenter.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		resources.ApplyResources(this.chkClosed, "chkClosed");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance25");
		((UltraToggleEditorBase)this.chkClosed).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkClosed).Name = "chkClosed";
		resources.ApplyResources(this.btnSalesAccountSearch, "btnSalesAccountSearch");
		((AppearanceBase)val15).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val15, "appearance26");
		((ControlBase)this.btnSalesAccountSearch).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.btnSalesAccountSearch).Name = "btnSalesAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesAccountSearch).Click += new System.EventHandler(btnSalesAccountSearch_Click);
		resources.ApplyResources(this.cboSalesAccount, "cboSalesAccount");
		this.cboSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesAccount).Name = "cboSalesAccount";
		resources.ApplyResources(this.lblSalesAccount, "lblSalesAccount");
		this.lblSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesAccount).Name = "lblSalesAccount";
		resources.ApplyResources(this.btnSalesReturnsAccountSearch, "btnSalesReturnsAccountSearch");
		((AppearanceBase)val16).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val16, "appearance27");
		((ControlBase)this.btnSalesReturnsAccountSearch).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccountSearch).Name = "btnSalesReturnsAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccountSearch).Click += new System.EventHandler(btnSalesReturnsAccountSearch_Click);
		resources.ApplyResources(this.cboSalesReturnsAccounts, "cboSalesReturnsAccounts");
		this.cboSalesReturnsAccounts.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccounts).Name = "cboSalesReturnsAccounts";
		resources.ApplyResources(this.lblSalesReturnsAccount, "lblSalesReturnsAccount");
		this.lblSalesReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount).Name = "lblSalesReturnsAccount";
		((ControlBase)this.lblSalesReturnsAccount).WrapText = false;
		resources.ApplyResources(this.lblMaintenanceSalesAccount, "lblMaintenanceSalesAccount");
		this.lblMaintenanceSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaintenanceSalesAccount).Name = "lblMaintenanceSalesAccount";
		resources.ApplyResources(this.cboMaintenanceSalesAccount, "cboMaintenanceSalesAccount");
		this.cboMaintenanceSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboMaintenanceSalesAccount).Name = "cboMaintenanceSalesAccount";
		resources.ApplyResources(this.btnMaintenanceSalesAccountSearch, "btnMaintenanceSalesAccountSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance28");
		((ControlBase)this.btnMaintenanceSalesAccountSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesAccountSearch).Name = "btnMaintenanceSalesAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesAccountSearch).Click += new System.EventHandler(btnMaintenanceSalesAccountSearch_Click);
		resources.ApplyResources(this.lblMaintenanceSalesReturnsAccount, "lblMaintenanceSalesReturnsAccount");
		this.lblMaintenanceSalesReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMaintenanceSalesReturnsAccount).Name = "lblMaintenanceSalesReturnsAccount";
		((ControlBase)this.lblMaintenanceSalesReturnsAccount).WrapText = false;
		resources.ApplyResources(this.cboMaintenanceSalesReturnsAccount, "cboMaintenanceSalesReturnsAccount");
		this.cboMaintenanceSalesReturnsAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboMaintenanceSalesReturnsAccount).Name = "cboMaintenanceSalesReturnsAccount";
		resources.ApplyResources(this.btnMaintenanceSalesReturnsAccountSearch, "btnMaintenanceSalesReturnsAccountSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance29");
		((ControlBase)this.btnMaintenanceSalesReturnsAccountSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesReturnsAccountSearch).Name = "btnMaintenanceSalesReturnsAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesReturnsAccountSearch).Click += new System.EventHandler(btnMaintenanceSalesReturnsAccountSearch_Click);
		resources.ApplyResources(this.lblFinishingSalesAccount, "lblFinishingSalesAccount");
		this.lblFinishingSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingSalesAccount).Name = "lblFinishingSalesAccount";
		resources.ApplyResources(this.cboFinishingSalesAccount, "cboFinishingSalesAccount");
		this.cboFinishingSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboFinishingSalesAccount).Name = "cboFinishingSalesAccount";
		resources.ApplyResources(this.btnFinishingSalesAccountSearch, "btnFinishingSalesAccountSearch");
		((AppearanceBase)val19).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val19, "appearance30");
		((ControlBase)this.btnFinishingSalesAccountSearch).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.btnFinishingSalesAccountSearch).Name = "btnFinishingSalesAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnFinishingSalesAccountSearch).Click += new System.EventHandler(btnFinishingSalesAccountSearch_Click);
		resources.ApplyResources(this.lblFinishingSalesReturnsAccount, "lblFinishingSalesReturnsAccount");
		this.lblFinishingSalesReturnsAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFinishingSalesReturnsAccount).Name = "lblFinishingSalesReturnsAccount";
		((ControlBase)this.lblFinishingSalesReturnsAccount).WrapText = false;
		resources.ApplyResources(this.cboFinishingSalesReturnsAccount, "cboFinishingSalesReturnsAccount");
		this.cboFinishingSalesReturnsAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboFinishingSalesReturnsAccount).Name = "cboFinishingSalesReturnsAccount";
		resources.ApplyResources(this.btnFinishingSalesReturnsAccountSearch, "btnFinishingSalesReturnsAccountSearch");
		((AppearanceBase)val20).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val20, "appearance31");
		((ControlBase)this.btnFinishingSalesReturnsAccountSearch).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.btnFinishingSalesReturnsAccountSearch).Name = "btnFinishingSalesReturnsAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnFinishingSalesReturnsAccountSearch).Click += new System.EventHandler(btnFinishingSalesReturnsAccountSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnFinishingSalesReturnsAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesReturnsAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFinishingSalesReturnsAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaintenanceSalesReturnsAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccounts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingSalesReturnsAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaintenanceSalesReturnsAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnFinishingSalesAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFinishingSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFinishingSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMaintenanceSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMaintenanceSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClosed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Name = "frmProjects";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClosed, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaintenanceSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaintenanceSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFinishingSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnFinishingSalesAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesReturnsAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMaintenanceSalesReturnsAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFinishingSalesReturnsAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesReturnsAccounts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMaintenanceSalesReturnsAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFinishingSalesReturnsAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesReturnsAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMaintenanceSalesReturnsAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnFinishingSalesReturnsAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClosed).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesReturnsAccounts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaintenanceSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMaintenanceSalesReturnsAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFinishingSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFinishingSalesReturnsAccount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
