using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.MarineService;
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
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinTree;

namespace ERP.MarineService.MasterData;

public class frmServicesTree : frmTree2
{
	private int newID = -100000;

	private DataTable dtDetails;

	private DataTable dtServicesStepsTasks;

	private DataTable dtServicesStepsTasksDocuments;

	private DataTable dtServicesStepsTasksExpenses;

	private DataTable dtServicesStepsTasksReports;

	private DataTable dtTaxes;

	private DataSet ds;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtPricesTypes;

	private DataTable dtServicePrices;

	private DataTable dtServicesTypes;

	private DataTable dtSteps;

	private DataTable dtUnits;

	private DataTable dtUnitGroup;

	private DataTable dtTaskPlaces;

	private DataTable dtTasks;

	private DataTable dtDocumentsTypes;

	private DataTable dtExpenses;

	private DataTable dtReports;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlSteps = new ValueList();

	private ValueList vlTasks = new ValueList();

	private ValueList vlTaskPlaces = new ValueList();

	private ValueList vlDocumentsTypes = new ValueList();

	private ValueList vlExpenses = new ValueList();

	private ValueList vlReports = new ValueList();

	private ValueList vlServices = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtNotes;

	private UltraLabel lblReceivingBank;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraTabPageControl tabService;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	private OpenFileDialog ofdItemPic;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraButton btnServiceAccountSearch;

	private UltraComboEditor cboServiceAccount;

	private UltraLabel lblServiceAccount;

	public UltraGrid ULGPrices;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem1;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem2;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem3;

	private ContextMenuStrip contextMenuStrip1;

	private UltraCheckEditor chkCanModPrice;

	private ToolStripMenuItem deleteToolStripMenuItem;

	private UltraComboEditor cboTypes;

	private UltraLabel lblTypes;

	private ToolStripMenuItem changeParentToolStripMenuItem;

	private UltraCheckEditor chkIsActive;

	private UltraComboEditor cboUnitGroup;

	private UltraComboEditor cboUnit;

	private UltraLabel lblUnitGroup;

	private UltraLabel lblUnit;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboSubAccount;

	public UltraButton btnSubAccountSearch;

	public UltraGrid ULGData;

	private UltraCheckEditor chkIsPercentage;

	private UltraCheckEditor chkIsCostPlus;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	public frmServicesTree()
	{
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
		InitializeComponent();
		IDCol = "ServiceID";
		NoCol = "ServiceNumber";
		NameCol = "ServiceNameAr";
		NameEnCol = "ServiceNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		ItemLevelCol = "LevelID";
		TableName = "MS_Services";
		LevelsTable = "MS_ServicesLevels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUnitGroup, dtUnitGroup, "UnitTypeID", "UnitTypeName");
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[i]["PriceTypeID"], dtPricesTypes.Rows[i]["PriceName"].ToString());
		}
		dtSteps = Steps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlSteps.ValueListItems.Clear();
		for (int j = 0; j < dtSteps.Rows.Count; j++)
		{
			vlSteps.ValueListItems.Add(dtSteps.Rows[j]["StepID"], dtSteps.Rows[j]["StepName"].ToString());
		}
		dtTasks = Tasks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlTasks.ValueListItems.Clear();
		for (int k = 0; k < dtTasks.Rows.Count; k++)
		{
			vlTasks.ValueListItems.Add(dtTasks.Rows[k]["TaskID"], dtTasks.Rows[k]["TaskName"].ToString());
		}
		dtTaskPlaces = TasksPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlTaskPlaces.ValueListItems.Clear();
		for (int l = 0; l < dtTaskPlaces.Rows.Count; l++)
		{
			vlTaskPlaces.ValueListItems.Add(dtTaskPlaces.Rows[l]["TaskPlaceID"], dtTaskPlaces.Rows[l]["TaskPlaceName"].ToString());
		}
		dtDocumentsTypes = DocumentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlDocumentsTypes.ValueListItems.Clear();
		for (int m = 0; m < dtDocumentsTypes.Rows.Count; m++)
		{
			vlDocumentsTypes.ValueListItems.Add(dtDocumentsTypes.Rows[m]["DocumentTypeID"], dtDocumentsTypes.Rows[m]["DocumentTypeName"].ToString());
		}
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlExpenses.ValueListItems.Clear();
		for (int n = 0; n < dtExpenses.Rows.Count; n++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[n]["ExpenseID"], dtExpenses.Rows[n]["ExpenseName"].ToString());
		}
		dtReports = UsersReports.SelectFormReports("1", GlobalFunctions.GetFormID("MarineService", "Reports", "frmMSLetters"), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlReports.ValueListItems.Clear();
		for (int num = 0; num < dtReports.Rows.Count; num++)
		{
			vlReports.ValueListItems.Add(dtReports.Rows[num]["ReportID"], dtReports.Rows[num]["ReportName"].ToString());
		}
		dtServicesTypes = ServicesTypes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTypes, dtServicesTypes, "ServiceTypeID", GlobalVariables.IsArabic ? "ServiceTypeNameAr" : "ServiceTypeNameEn");
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtDetails = ServicesSteps.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtServicesStepsTasks = ServicesStepsTasks.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtServicesStepsTasksDocuments = ServicesStepsTasksDocuments.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtServicesStepsTasksExpenses = ServicesStepsTasksExpenses.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtServicesStepsTasksReports = ServicesStepsTasksReports.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtServicesStepsTasks);
		ds.Tables.Add(dtServicesStepsTasksExpenses);
		ds.Tables.Add(dtServicesStepsTasksDocuments);
		ds.Tables.Add(dtServicesStepsTasksReports);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtServicesStepsTasks";
		ds.Tables[2].TableName = "dtServicesStepsTasksExpenses";
		ds.Tables[3].TableName = "dtServicesStepsTasksDocuments";
		ds.Tables[4].TableName = "dtServicesStepsTasksReports";
		ds.Relations.Add(ds.Tables[0].Columns["ServiceStepID"], ds.Tables[1].Columns["ServiceStepID"]);
		ds.Relations.Add(ds.Tables[1].Columns["ServiceStepTaskID"], ds.Tables[2].Columns["ServiceStepTaskID"]);
		ds.Relations.Add(ds.Tables[1].Columns["ServiceStepTaskID"], ds.Tables[3].Columns["ServiceStepTaskID"]);
		ds.Relations.Add(ds.Tables[1].Columns["ServiceStepTaskID"], ds.Tables[4].Columns["ServiceStepTaskID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		dtServicePrices = ServicesPrices.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Caption = (GlobalVariables.IsArabic ? "مهام" : "Tasks");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackColor = Color.LightSkyBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackColor2 = Color.LightYellow;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Caption = (GlobalVariables.IsArabic ? "المصروفات" : "Expenses");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackColor = Color.AliceBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackColor2 = Color.CornflowerBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Header).Caption = (GlobalVariables.IsArabic ? "المستندات" : "Documents");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Header).Appearance.BackColor = Color.AliceBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Header).Appearance.BackColor2 = Color.CornflowerBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Caption = (GlobalVariables.IsArabic ? "التقارير" : "Reports");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.BackColor = Color.AliceBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.BackColor2 = Color.CornflowerBlue;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.BackGradientStyle = (GradientStyle)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Header).Appearance.ForeColor = Color.Blue;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepOrder"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].Header).Caption = (GlobalVariables.IsArabic ? "المرحلة" : "Step");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepOrder"].Header).Caption = (GlobalVariables.IsArabic ? "الترتيب" : "Step Order");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpectedTime"].Header).Caption = (GlobalVariables.IsArabic ? "المده" : "Expected Time");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepOrder"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepID"].ValueList = (IValueList)(object)vlSteps;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StepOrder"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaskID"].Header).Caption = (GlobalVariables.IsArabic ? "المهمه" : "Task");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExpectedTime"].Header).Caption = (GlobalVariables.IsArabic ? "الوقت المتوقع" : "Expected Time");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaskPlaceID"].Header).Caption = (GlobalVariables.IsArabic ? "الجهه" : "Place");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaskID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExpectedTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaskPlaceID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaskID"].ValueList = (IValueList)(object)vlTasks;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["TaskPlaceID"].ValueList = (IValueList)(object)vlTaskPlaces;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ExpectedTime"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "القيمه" : "Value");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CanModifyPrice"].Header).Caption = (GlobalVariables.IsArabic ? "يمكن تعديله" : "Editable");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["PerUnit"].Header).Caption = (GlobalVariables.IsArabic ? "للوحده" : "PerUnit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CanModifyPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["PerUnit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ExpenseID"].ValueList = (IValueList)(object)vlExpenses;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Price"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CanModifyPrice"].DefaultCellValue = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["PerUnit"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["ServiceStepTaskExpenseID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["DocumentTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "المستند" : "Document");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["IsOriginal"].Header).Caption = (GlobalVariables.IsArabic ? "اصل" : "Original");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["DocumentTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["IsOriginal"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["DocumentTypeID"].ValueList = (IValueList)(object)vlDocumentsTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["IsOriginal"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[3].Columns["ServiceStepTaskDocumentID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["ReportID"].Header).Caption = (GlobalVariables.IsArabic ? "التقرير" : "Report");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["ReportID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["ReportID"].ValueList = (IValueList)(object)vlReports;
		((UltraGridBase)ULGData).DisplayLayout.Bands[4].Columns["ServiceStepTaskReportID"].DefaultCellValue = -1;
	}

	public override void ClearControls()
	{
		if (Adding)
		{
			DisplayData();
		}
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = Services.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((TextEditorControlBase)txtBarCode).Clear();
			FillGridPrices();
			((Control)(object)txtName).Select();
		}
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[4].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[3].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[2].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[1].Rows.Clear();
		((DataSet)((UltraGridBase)ULGData).DataSource).Tables[0].Rows.Clear();
		newID = -100000;
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)cboUnit).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUnitGroup).ReadOnly = NavMode;
		((EditorButtonControlBase)cboServiceAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTypes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)chkCanModPrice).Enabled = !NavMode;
		((Control)(object)chkIsPercentage).Enabled = !NavMode;
		((Control)(object)chkIsCostPlus).Enabled = !NavMode;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (SelectedNode != null)
		{
			DataRow dataRow = Services.Select(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
			((TextEditorControlBase)cboUnit).Value = dataRow["UnitID"];
			((TextEditorControlBase)cboUnitGroup).Value = dataRow["UnitTypeID"];
			((TextEditorControlBase)cboServiceAccount).Value = dataRow["ServiceAccountID"];
			((TextEditorControlBase)cboSubAccount).Value = dataRow["ServiceSubAccountID"];
			((TextEditorControlBase)cboTypes).Value = dataRow["ServiceTypeID"];
			((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(dataRow["IsActive"]);
			((UltraToggleEditorBase)chkCanModPrice).Checked = Convert.ToBoolean(dataRow["CanModifyPrice"]);
			((UltraToggleEditorBase)chkIsPercentage).Checked = Convert.ToBoolean(dataRow["IsExpensePercent"]);
			((UltraToggleEditorBase)chkIsCostPlus).Checked = Convert.ToBoolean(dataRow["IsCostPlus"]);
			((TextEditorControlBase)cboTax).Value = dataRow["TaxID"];
			deleteToolStripMenuItem.Enabled = Convert.ToBoolean(dataRow["IsMain"]);
			((Control)(object)txtBarCode).Text = dataRow["ServiceBarCode"].ToString();
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			dtDetails = ServicesSteps.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtServicesStepsTasks = ServicesStepsTasks.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtServicesStepsTasksDocuments = ServicesStepsTasksDocuments.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtServicesStepsTasksExpenses = ServicesStepsTasksExpenses.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtServicesStepsTasksReports = ServicesStepsTasksReports.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			ds = new DataSet();
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtServicesStepsTasks);
			ds.Tables.Add(dtServicesStepsTasksExpenses);
			ds.Tables.Add(dtServicesStepsTasksDocuments);
			ds.Tables.Add(dtServicesStepsTasksReports);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtServicesStepsTasks";
			ds.Tables[2].TableName = "dtServicesStepsTasksExpenses";
			ds.Tables[3].TableName = "dtServicesStepsTasksDocuments";
			ds.Tables[4].TableName = "dtServicesStepsTasksReports";
			ds.Relations.Add(ds.Tables[0].Columns["ServiceStepID"], ds.Tables[1].Columns["ServiceStepID"]);
			ds.Relations.Add(ds.Tables[1].Columns["ServiceStepTaskID"], ds.Tables[2].Columns["ServiceStepTaskID"]);
			ds.Relations.Add(ds.Tables[1].Columns["ServiceStepTaskID"], ds.Tables[3].Columns["ServiceStepTaskID"]);
			ds.Relations.Add(ds.Tables[1].Columns["ServiceStepTaskID"], ds.Tables[4].Columns["ServiceStepTaskID"]);
			((UltraGridBase)ULGData).DataSource = ds;
			InitGrid();
			dtServicePrices = ServicesPrices.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridPrices();
		}
	}

	public override bool ValidateData()
	{
		if (cboTypes.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار نوع الخدمه", "Please select Service Type");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboTypes).Focus();
			cboTypes.DropDown();
			return false;
		}
		if (cboUnitGroup.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار مجموعه الوحده", "Please select Unit Group");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboUnitGroup).Focus();
			cboUnitGroup.DropDown();
			return false;
		}
		if (cboUnit.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الوحده", "Please select Unit ");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboUnit).Focus();
			cboUnit.DropDown();
			return false;
		}
		if (cboServiceAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم الحساب", "Please Select Account Name");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboServiceAccount).Focus();
			cboServiceAccount.DropDown();
			return false;
		}
		if (cboSubAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار إسم الحساب التحليلى", "Please Select SubAccount Name");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboSubAccount).Focus();
			cboSubAccount.DropDown();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["StepID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم المرحلة  ", "Please Select Stage Name ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StepID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count == 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل المهام", "Please insert Tasks Details");
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إدخال المهمه  ", "Please Enter Task Name ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows).Count; k++)
				{
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ExpenseID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show("برجاء إختيار إسم المصروف  ", "Please Select Expense ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ExpenseID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Price"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Price"].Value.ToString()) <= 0m)
					{
						GlobalVariables.InformationMB.Show("برجاء ادخال القيمه  ", "Please Enter Value ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Price"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
				}
				for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows).Count; l++)
				{
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["DocumentTypeID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show("برجاء ادخال اسم المستند  ", "Please Enter Document ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["DocumentTypeID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
				}
				for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows).Count; m++)
				{
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["ReportID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show("برجاء إختيار إسم التقرير  ", "Please Select Report ");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["ReportID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
				}
			}
		}
		return base.ValidateData();
	}

	public override int TreeAddData()
	{
		int result = 0;
		int num = 0;
		int num2 = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			result = Services.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), (((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text, (cboUnit.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnit).Value.ToString(), (cboUnitGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsPercentage).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsCostPlus).Checked ? "1" : "0", (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				num = ServicesSteps.Insert_Update("-1", result.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StepID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StepOrder"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ExpectedTime"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					num2 = ServicesStepsTasks.Insert_Update("-1", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskID"].Value.ToString(), result.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskOrder"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExpectedTime"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskPlaceID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows).Count; k++)
					{
						ServicesStepsTasksExpenses.Insert_Update("-1", num2.ToString(), num.ToString(), result.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ExpenseID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["CanModifyPrice"].Value.Equals(true) ? "1" : "0", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["PerUnit"].Value.Equals(true) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					}
					for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows).Count; l++)
					{
						ServicesStepsTasksDocuments.Insert_Update("-1", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["DocumentTypeID"].Value.ToString(), num2.ToString(), num.ToString(), result.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["IsOriginal"].Value.Equals(true) ? "1" : "0", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					}
					for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows).Count; m++)
					{
						ServicesStepsTasksReports.Insert_Update("-1", num2.ToString(), num.ToString(), result.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["ReportID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					}
				}
			}
			dtServicePrices.AcceptChanges();
			for (int n = 0; n < dtServicePrices.Rows.Count; n++)
			{
				ServicesPrices.Insert_Update("-1", result.ToString(), dtServicePrices.Rows[n]["PriceTypeID"].ToString(), dtServicePrices.Rows[n]["Price"].ToString(), "0", "Null", GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return result;
	}

	public override void TreeUpdateData()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		string text = ",";
		string text2 = ",";
		string text3 = ",";
		string text4 = ",";
		string text5 = ",";
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = Services.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), (((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text, (cboUnit.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnit).Value.ToString(), (cboUnitGroup.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUnitGroup).Value.ToString(), ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsPercentage).Checked ? "1" : "0", ((UltraToggleEditorBase)chkIsCostPlus).Checked ? "1" : "0", (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (cboTypes.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTypes).Value.ToString(), (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				num2 = ServicesSteps.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ServiceStepID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ServiceStepID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[i].Cells["ServiceStepID"].Value.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StepID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["StepOrder"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["ExpectedTime"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				text = text + num2 + ",";
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					num3 = ServicesStepsTasks.Insert_Update((int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ServiceStepTaskID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ServiceStepTaskID"].Value.ToString()) < -1) ? "-1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ServiceStepTaskID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskID"].Value.ToString(), num.ToString(), num2.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskOrder"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ExpectedTime"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaskPlaceID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
					text2 = text2 + num3 + ",";
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows).Count; k++)
					{
						num4 = ServicesStepsTasksExpenses.Insert_Update(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ServiceStepTaskExpenseID"].Value.ToString(), num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ExpenseID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Price"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["CanModifyPrice"].Value.Equals(true) ? "1" : "0", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["PerUnit"].Value.Equals(true) ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
						text3 = text3 + num4 + ",";
					}
					for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows).Count; l++)
					{
						num5 = ServicesStepsTasksDocuments.Insert_Update(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["ServiceStepTaskDocumentID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["DocumentTypeID"].Value.ToString(), num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["IsOriginal"].Value.Equals(true) ? "1" : "0", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[1].Rows[l].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
						text4 = text4 + num5 + ",";
					}
					for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows).Count; m++)
					{
						num6 = ServicesStepsTasksReports.Insert_Update(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["ServiceStepTaskReportID"].Value.ToString(), num3.ToString(), num2.ToString(), num.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["ReportID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[2].Rows[m].Cells["Notes"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
						text5 = text5 + num6 + ",";
					}
				}
			}
			Main.SyncDeleteForUpdate("MS_ServicesStepsTasksReports", "ServiceID", num.ToString(), "ServiceStepTaskReportID", text5, IsFromServer: true);
			Main.SyncDeleteForUpdate("MS_ServicesStepsTasksDocuments", "ServiceID", num.ToString(), "ServiceStepTaskDocumentID", text4, IsFromServer: true);
			Main.SyncDeleteForUpdate("MS_ServicesStepsTasksExpenses", "ServiceID", num.ToString(), "ServiceStepTaskExpenseID", text3, IsFromServer: true);
			Main.SyncDeleteForUpdate("MS_ServicesStepsTasks", "ServiceID", num.ToString(), "ServiceStepTaskID", text2, IsFromServer: true);
			Main.SyncDeleteForUpdate("MS_ServicesSteps", "ServiceID", num.ToString(), "ServiceStepID", text, IsFromServer: true);
			ServicesPrices.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			dtServicePrices.AcceptChanges();
			for (int n = 0; n < dtServicePrices.Rows.Count; n++)
			{
				ServicesPrices.Insert_Update("-1", ((KeyedSubObjectBase)SelectedNode).Key, dtServicePrices.Rows[n]["PriceTypeID"].ToString(), dtServicePrices.Rows[n]["Price"].ToString(), "0", "Null", GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void TreeDeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			ServicesStepsTasksReports.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ServicesStepsTasksDocuments.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ServicesStepsTasksExpenses.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ServicesStepsTasks.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ServicesSteps.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			ServicesPrices.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Services.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override bool HasTransactionValidation()
	{
		if (OperationsServices.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("الخدمه مستخدمه غي العمليات", "this Service is used in Operation");
			return true;
		}
		return base.HasTransactionValidation();
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.ServicesSearch(IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	private void cboUnitGroup_ValueChanged(object sender, EventArgs e)
	{
		if (cboUnitGroup.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtUnits);
			dataView.RowFilter = "UnitTypeID=" + ((TextEditorControlBase)cboUnitGroup).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboUnit.DataSource = dataView;
			cboUnit.DisplayMember = "UnitName";
			cboUnit.ValueMember = "UnitID";
		}
	}

	private void FillGridPrices()
	{
		dtServicePrices.Rows.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			DataRow dataRow = dtServicePrices.NewRow();
			dataRow["ServicePriceID"] = -1;
			dataRow["ServiceID"] = -1;
			dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
			dataRow["Price"] = 0;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = DBNull.Value;
			dtServicePrices.Rows.Add(dataRow);
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		((UltraGridBase)ULGPrices).DataSource = dtServicePrices;
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ServicePriceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5);
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
	}

	public override void btnRefreshDataClick()
	{
		base.btnRefreshDataClick();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtUnitGroup = UnitsTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		cboUnitGroup.DataSource = dtUnitGroup;
		cboUnitGroup.DisplayMember = "UnitTypeName";
		cboUnitGroup.ValueMember = "UnitTypeID";
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[i]["PriceTypeID"], dtPricesTypes.Rows[i]["PriceName"].ToString());
		}
	}

	public override void btnPrintClick()
	{
	}

	private void btnServiceAccountSearch_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboServiceAccount).Value = num;
			}
		}
	}

	private void modifyGroupItemsToolStripMenuItem4_Click(object sender, EventArgs e)
	{
	}

	private void modifyGroupItemsToolStripMenuItem3_Click(object sender, EventArgs e)
	{
	}

	private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
		}
		else if (SelectedNode != null)
		{
			DeleteGroupItems(SelectedNode);
		}
	}

	public bool DeleteGroupItems(UltraTreeNode Node)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			if (DeleteGroupItems(Node.Nodes[i]))
			{
				i--;
			}
		}
		SelectedNode = Node;
		if (((DisposableObjectCollectionBase)Node.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن حذف هذا العنصر لوجود عناصر تحته", "Cannot Delete this Node It Has Sub Nodes");
			return false;
		}
		if (HasTransactionValidation())
		{
			return false;
		}
		RowID = (((SubObjectBase)SelectedNode).Tag as DataRow)[0].ToString();
		DeleteData();
		if (Main.Success)
		{
			return true;
		}
		return false;
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == ' ' || e.KeyChar == '+')
		{
			e.Handled = true;
		}
	}

	private void changeParentToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateParent frmUpdateParent2 = new frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
			((Control)(object)frmUpdateParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير المجموعه" : "Update Group");
			frmUpdateParent2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void cboServiceAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboServiceAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboServiceAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSubAccount.DataSource = dataView;
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboServiceAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboServiceAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
		}
	}

	private void cboSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboServiceAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboServiceAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
		}
	}

	private void cboServiceAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			((TextEditorControlBase)cboServiceAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (((GridItemBase)e.Row).Band.Index == 0)
		{
			e.Row.Cells["ServiceStepID"].Value = ++newID;
		}
		else if (((GridItemBase)e.Row).Band.Index == 1)
		{
			e.Row.Cells["ServiceStepTaskID"].Value = ++newID;
		}
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkIsPercentage_CheckedChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraToggleEditorBase)chkIsPercentage).Checked)
		{
			((UltraToggleEditorBase)chkIsCostPlus).Checked = !((UltraToggleEditorBase)chkIsPercentage).Checked;
		}
	}

	private void chkIsCostPlus_CheckedChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraToggleEditorBase)chkIsCostPlus).Checked)
		{
			((UltraToggleEditorBase)chkIsPercentage).Checked = !((UltraToggleEditorBase)chkIsCostPlus).Checked;
		}
	}

	public void ULGPricesSelectFullRow(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	public void ULGData_SelectFullRow(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Expected O, but got Unknown
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_1079: Unknown result type (might be due to invalid IL or missing references)
		//IL_1083: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
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
		UltraTab val38 = new UltraTab();
		UltraTab val39 = new UltraTab();
		UltraTab val40 = new UltraTab();
		Appearance val41 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmServicesTree));
		Appearance val42 = new Appearance();
		this.tabItem = new UltraTabPageControl();
		this.cboUnitGroup = new UltraComboEditor();
		this.cboUnit = new UltraComboEditor();
		this.lblUnitGroup = new UltraLabel();
		this.lblUnit = new UltraLabel();
		this.cboServiceAccount = new UltraComboEditor();
		this.lblServiceAccount = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.btnServiceAccountSearch = new UltraButton();
		this.btnSubAccountSearch = new UltraButton();
		this.txtNotes = new UltraTextEditor();
		this.lblReceivingBank = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.cboTypes = new UltraComboEditor();
		this.lblTypes = new UltraLabel();
		this.tabService = new UltraTabPageControl();
		this.ULGData = new UltraGrid();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGPrices = new UltraGrid();
		this.chkIsCostPlus = new UltraCheckEditor();
		this.chkIsPercentage = new UltraCheckEditor();
		this.chkCanModPrice = new UltraCheckEditor();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.ofdItemPic = new System.Windows.Forms.OpenFileDialog();
		this.modifyGroupItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.changeParentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabService).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCostPlus).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.treeChart).ContextMenuStrip = this.contextMenuStrip1;
		resources.ApplyResources(base.treeChart, "treeChart");
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.btnAddRoot, "btnAddRoot");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label1, "label1");
		((ControlBase)base.label1).WrapText = false;
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label2, "label2");
		((ControlBase)base.label2).WrapText = false;
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
		resources.ApplyResources(base.label3, "label3");
		((ControlBase)base.label3).WrapText = false;
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnitGroup);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboUnit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitGroup);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboServiceAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingBank);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboTypes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblTypes);
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.cboUnitGroup, "cboUnitGroup");
		((System.Windows.Forms.Control)(object)this.cboUnitGroup).Name = "cboUnitGroup";
		((TextEditorControlBase)this.cboUnitGroup).ValueChanged += new System.EventHandler(cboUnitGroup_ValueChanged);
		resources.ApplyResources(this.cboUnit, "cboUnit");
		((System.Windows.Forms.Control)(object)this.cboUnit).Name = "cboUnit";
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblUnitGroup).Appearance = (AppearanceBase)(object)val5;
		this.lblUnitGroup.AutoEllipsis = false;
		resources.ApplyResources(this.lblUnitGroup, "lblUnitGroup");
		((System.Windows.Forms.Control)(object)this.lblUnitGroup).Name = "lblUnitGroup";
		((ControlBase)this.lblUnitGroup).WrapText = false;
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblUnit).Appearance = (AppearanceBase)(object)val6;
		this.lblUnit.AutoEllipsis = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		this.cboServiceAccount.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboServiceAccount, "cboServiceAccount");
		((System.Windows.Forms.Control)(object)this.cboServiceAccount).Name = "cboServiceAccount";
		((TextEditorControlBase)this.cboServiceAccount).ValueChanged += new System.EventHandler(cboServiceAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboServiceAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboServiceAccount_KeyDown);
		this.lblServiceAccount.AutoEllipsis = false;
		resources.ApplyResources(this.lblServiceAccount, "lblServiceAccount");
		((System.Windows.Forms.Control)(object)this.lblServiceAccount).Name = "lblServiceAccount";
		((ControlBase)this.lblServiceAccount).WrapText = false;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		((System.Windows.Forms.Control)(object)this.cboSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccount_KeyDown);
		((AppearanceBase)val7).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnServiceAccountSearch).Appearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(this.btnServiceAccountSearch, "btnServiceAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Name = "btnServiceAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Click += new System.EventHandler(btnServiceAccountSearch_Click);
		((AppearanceBase)val8).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblReceivingBank).Appearance = (AppearanceBase)(object)val9;
		this.lblReceivingBank.AutoEllipsis = false;
		resources.ApplyResources(this.lblReceivingBank, "lblReceivingBank");
		((System.Windows.Forms.Control)(object)this.lblReceivingBank).Name = "lblReceivingBank";
		((ControlBase)this.lblReceivingBank).WrapText = false;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		this.cboTypes.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboTypes, "cboTypes");
		((System.Windows.Forms.Control)(object)this.cboTypes).Name = "cboTypes";
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblTypes).Appearance = (AppearanceBase)(object)val11;
		this.lblTypes.AutoEllipsis = false;
		resources.ApplyResources(this.lblTypes, "lblTypes");
		((System.Windows.Forms.Control)(object)this.lblTypes).Name = "lblTypes";
		((ControlBase)this.lblTypes).WrapText = false;
		((System.Windows.Forms.Control)(object)this.tabService).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		resources.ApplyResources(this.tabService, "tabService");
		((System.Windows.Forms.Control)(object)this.tabService).Name = "tabService";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val12).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val12).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val12).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val13;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val17;
		((AppearanceBase)val18).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val18).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val18;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val19).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val19).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val19).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val20).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val20;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val21).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_SelectFullRow);
		this.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsCostPlus);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPercentage);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModPrice);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val22).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val22).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val22).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val22;
		((AppearanceBase)val23).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val23;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val24).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val24).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val24).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val24;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val25).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val25).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val25;
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val26).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val26;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val27).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val27;
		((AppearanceBase)val28).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val28).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val29).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val29).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val29).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val29).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val29;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val30).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val31;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		((AppearanceBase)val32).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkIsCostPlus).Appearance = (AppearanceBase)(object)val32;
		resources.ApplyResources(this.chkIsCostPlus, "chkIsCostPlus");
		((System.Windows.Forms.Control)(object)this.chkIsCostPlus).Name = "chkIsCostPlus";
		((UltraToggleEditorBase)this.chkIsCostPlus).CheckedChanged += new System.EventHandler(chkIsCostPlus_CheckedChanged);
		((AppearanceBase)val33).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkIsPercentage).Appearance = (AppearanceBase)(object)val33;
		resources.ApplyResources(this.chkIsPercentage, "chkIsPercentage");
		((System.Windows.Forms.Control)(object)this.chkIsPercentage).Name = "chkIsPercentage";
		((UltraToggleEditorBase)this.chkIsPercentage).CheckedChanged += new System.EventHandler(chkIsPercentage_CheckedChanged);
		((AppearanceBase)val34).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkCanModPrice).Appearance = (AppearanceBase)(object)val34;
		resources.ApplyResources(this.chkCanModPrice, "chkCanModPrice");
		((System.Windows.Forms.Control)(object)this.chkCanModPrice).Name = "chkCanModPrice";
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val35).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val35).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabService);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(val36, "appearance36");
		((UltraTabControlBase)this.tabItemType).TabHeaderAreaAppearance = (AppearanceBase)(object)val36;
		resources.ApplyResources(val37, "appearance37");
		((UltraTabControlBase)this.tabItemType).TabListButtonAppearance = (AppearanceBase)(object)val37;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val38).Key = "Item";
		val38.TabPage = this.tabItem;
		resources.ApplyResources(val38, "ultraTab2");
		((SubObjectBase)val38).ForceApplyResources = "";
		((KeyedSubObjectBase)val39).Key = "Service";
		val39.TabPage = this.tabService;
		resources.ApplyResources(val39, "ultraTab1");
		((SubObjectBase)val39).ForceApplyResources = "";
		((KeyedSubObjectBase)val40).Key = "Prices";
		val40.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val40, "ultraTab4");
		((SubObjectBase)val40).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[3] { val38, val39, val40 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBarCode_KeyPress);
		((AppearanceBase)val41).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val41).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblBarCode).Appearance = (AppearanceBase)(object)val41;
		this.lblBarCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		this.ofdItemPic.FileName = "openFileDialog1";
		resources.ApplyResources(this.ofdItemPic, "ofdItemPic");
		this.modifyGroupItemsToolStripMenuItem.Name = "modifyGroupItemsToolStripMenuItem";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem, "modifyGroupItemsToolStripMenuItem");
		this.modifyGroupItemsToolStripMenuItem1.Name = "modifyGroupItemsToolStripMenuItem1";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem1, "modifyGroupItemsToolStripMenuItem1");
		this.modifyGroupItemsToolStripMenuItem2.Name = "modifyGroupItemsToolStripMenuItem2";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem2, "modifyGroupItemsToolStripMenuItem2");
		this.modifyGroupItemsToolStripMenuItem3.Name = "modifyGroupItemsToolStripMenuItem3";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem3, "modifyGroupItemsToolStripMenuItem3");
		this.modifyGroupItemsToolStripMenuItem3.Click += new System.EventHandler(modifyGroupItemsToolStripMenuItem3_Click);
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.deleteToolStripMenuItem, this.changeParentToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
		this.deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		this.changeParentToolStripMenuItem.Name = "changeParentToolStripMenuItem";
		resources.ApplyResources(this.changeParentToolStripMenuItem, "changeParentToolStripMenuItem");
		this.changeParentToolStripMenuItem.Click += new System.EventHandler(changeParentToolStripMenuItem_Click);
		((AppearanceBase)val42).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val42).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.lblTax).Appearance = (AppearanceBase)(object)val42;
		this.lblTax.AutoEllipsis = false;
		resources.ApplyResources(this.lblTax, "lblTax");
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Name = "frmServicesTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboUnitGroup).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTypes).EndInit();
		((System.Windows.Forms.Control)(object)this.tabService).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsCostPlus).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
