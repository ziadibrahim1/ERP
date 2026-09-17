using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CnsProjects;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.CnsProjects.Transactions;

public class frmContracts : frmHeaderManyDetails
{
	private DataTable dtReports;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtUnits;

	private DataTable dtStores;

	private DataTable dtClients;

	private DataTable dtProjects;

	private DataTable dtAccessories;

	private ValueList vlItems = new ValueList();

	private ValueList vlItems2 = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlBarCode2 = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlUnits2 = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlColors2 = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlSizes2 = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private IContainer components = null;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGAccessories;

	private UltraComboEditor cboProjects;

	private UltraLabel lblCountry;

	private UltraLabel lblProjects;

	private UltraComboEditor cboStores;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboSubAccount;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtTotalPrice;

	private UltraLabel ultraLabel3;

	private UltraTextEditor txtDownPayment;

	private UltraTextEditor txtDownPaymentPercentage;

	private UltraLabel ultraLabel4;

	private UltraTextEditor txtConstractionInsurense;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtConstractionInsurensePercentage;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtWorkerInsurense;

	private UltraLabel ultraLabel7;

	private UltraTextEditor txtWorkerInsurensePercentage;

	private UltraLabel ultraLabel8;

	private UltraTextEditor txtSupplyPercentage;

	private UltraTextEditor txtErectionPercentage;

	private UltraLabel ultraLabel11;

	private UltraTextEditor txtTestPercentage;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel13;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel15;

	public UltraButton btnClientSearch;

	public UltraButton btnStoreSearch;

	public UltraButton btnProjectSearch;

	public frmContracts()
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
		TableName = "Cns_Contracts";
		IDCol = "ContractID";
		NoCol = "ContractCode";
		DateCol = "ContractDate";
	}

	public frmContracts(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		if (UsingColors)
		{
			dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			vlColors2.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
				vlColors2.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			vlSizes2.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
				vlSizes2.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtProjects = Projects.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboProjects, dtProjects, "ProjectID", "ProjectName");
		dtStores = Stores.FillCombo("-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboStores, dtStores, "StoreID", "StoreName");
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtClients, "SubAccountID", "SubAccountName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		vlItems2.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		vlBarCode2.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlItems2.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["ItemBarCode"].ToString());
			vlBarCode2.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["ItemBarCode"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		vlUnits2.ValueListItems.Clear();
		for (int l = 0; l < dtUnits.Rows.Count; l++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
			vlUnits2.ValueListItems.Add(dtUnits.Rows[l]["UnitID"], dtUnits.Rows[l]["UnitName"].ToString());
		}
		dtDetails = ContractsDetails.SelectByContractID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtAccessories = ContractsAccessories.SelectByContractID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGAccessories).DataSource = dtAccessories;
		InitGrid();
		InitGridAccessories();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ContractDetailID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalTotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalTotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Header).Caption = (GlobalVariables.IsArabic ? "كمية التعاقد" : "Original Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalQty"].Header).Caption = (GlobalVariables.IsArabic ? "كمية اضافيه" : "Additional Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "كميةإجماليه" : "Total Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Unit Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalTotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي تعاقد" : "Total Original");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalTotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي إضافي" : "Total Additional");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالي" : "Total ");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalTotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalTotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalQty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalTotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalTotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OriginalQty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalQty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public void InitGridAccessories()
	{
		GlobalFunctions.PrepareGrid(ULGAccessories);
		((UltraGridBase)ULGAccessories).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGAccessories).Width * 0.05);
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGAccessories).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors2;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes2;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGAccessories).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGAccessories).Width * 0.2);
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGAccessories).Width * 0.2);
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGAccessories).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((HeaderBase)((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((HeaderBase)((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems2;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode2;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits2;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["ContractAccessoryID"].DefaultCellValue = -1;
		((UltraGridBase)ULGAccessories).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Contracts.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["ContractCode"].ToString();
			dtpDate.Value = (DateTime)drMaster["ContractDate"];
			((TextEditorControlBase)cboProjects).Value = drMaster["ProjectID"];
			((TextEditorControlBase)cboStores).Value = drMaster["StoreID"];
			((TextEditorControlBase)cboSubAccount).Value = drMaster["SubAccountID"];
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDownPaymentPercentage).Text = decimal.Parse(drMaster["DownPaymentPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDownPayment).Text = drMaster["DownPaymentValue"].ToString();
			((Control)(object)txtConstractionInsurensePercentage).Text = decimal.Parse(drMaster["ConstractionInsurensePercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtConstractionInsurense).Text = decimal.Parse(drMaster["ConstractionInsurenseValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtWorkerInsurensePercentage).Text = decimal.Parse(drMaster["WorkerInsurensePercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtWorkerInsurense).Text = decimal.Parse(drMaster["WorkerInsurenseValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSupplyPercentage).Text = decimal.Parse(drMaster["SupplyPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtErectionPercentage).Text = decimal.Parse(drMaster["ErectionPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTestPercentage).Text = decimal.Parse(drMaster["TestPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			ClosedPeriod = FiscalYear.ChkForClosingFsicalPeriod(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false);
			dtDetails = ContractsDetails.SelectByContractID(drMaster["ContractID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtAccessories = ContractsAccessories.SelectByContractID(drMaster["ContractID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGAccessories).DataSource = dtAccessories;
			InitGrid();
			InitGridAccessories();
		}
		else
		{
			ClearControls();
		}
	}

	public override void SetControls(bool NavMode)
	{
		bool flag = false;
		if (Updating && drMaster["Approved"].Equals(true))
		{
			flag = true;
		}
		base.SetControls(NavMode);
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboProjects).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboStores).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)cboSubAccount).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtTotalPrice).ReadOnly = true;
		((EditorButtonControlBase)txtDownPaymentPercentage).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtDownPayment).ReadOnly = true;
		((EditorButtonControlBase)txtConstractionInsurensePercentage).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtConstractionInsurense).ReadOnly = true;
		((EditorButtonControlBase)txtWorkerInsurensePercentage).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtWorkerInsurense).ReadOnly = true;
		((EditorButtonControlBase)txtSupplyPercentage).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtErectionPercentage).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtTestPercentage).ReadOnly = NavMode || flag;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode || flag;
		UltraButton obj = btnStoreSearch;
		bool visible = (((Control)(object)btnClientSearch).Visible = Adding || (Updating && !flag));
		((Control)(object)obj).Visible = visible;
		((Control)(object)btnProjectSearch).Visible = Adding;
		if (Adding)
		{
			dtProjects = Projects.FillComboWithoutContracts("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboProjects, dtProjects, "ProjectID", "ProjectName");
			DataView dataView = new DataView(dtItems);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
		}
		else
		{
			((EditorButtonControlBase)cboProjects).ReadOnly = true;
			dtProjects = Projects.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboProjects, dtProjects, "ProjectID", "ProjectName");
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int j = 0; j < dtItems.Rows.Count; j++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
			}
		}
		if (Updating)
		{
			for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
			{
				if (((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[k].Cells["ItemID"].Value.ToString())[0];
				if (!Convert.ToBoolean(dataRow["IsService"]))
				{
					int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList = getUnitsValueList(unitTypeID);
					((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
					if (((DisposableObjectCollectionBase)unitsValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["UnitID"].Value = DBNull.Value;
					}
				}
				if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[k].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGAccessories).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGAccessories).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? Contracts.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID) : "");
		cboProjects.SelectedIndex = -1;
		cboStores.SelectedIndex = -1;
		cboSubAccount.SelectedIndex = -1;
		((TextEditorControlBase)txtTotalPrice).Clear();
		((TextEditorControlBase)txtDownPaymentPercentage).Clear();
		((TextEditorControlBase)txtDownPayment).Clear();
		((TextEditorControlBase)txtConstractionInsurensePercentage).Clear();
		((TextEditorControlBase)txtConstractionInsurense).Clear();
		((TextEditorControlBase)txtWorkerInsurensePercentage).Clear();
		((TextEditorControlBase)txtWorkerInsurense).Clear();
		((TextEditorControlBase)txtSupplyPercentage).Clear();
		((TextEditorControlBase)txtErectionPercentage).Clear();
		((TextEditorControlBase)txtTestPercentage).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessories).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGAccessories).DataSource).Rows.Clear();
		}
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال التاريخ " : "Please Enter The Voucher Date");
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
		if (cboProjects.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المشروع" : "Please Select Project");
			((TextEditorControlBase)cboProjects).Focus();
			return false;
		}
		if (cboSubAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار العميل" : "Please Select Client");
			((TextEditorControlBase)cboSubAccount).Focus();
			return false;
		}
		if (cboStores.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المخزن" : "Please Select Store");
			((TextEditorControlBase)cboStores).Focus();
			return false;
		}
		if (((Control)(object)txtSupplyPercentage).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال Supply Percentage" : "Please Enter Supply Percentage");
			((TextEditorControlBase)txtSupplyPercentage).Focus();
			return false;
		}
		if (((Control)(object)txtErectionPercentage).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال Erection Percentage" : "Please Enter Erection Percentage");
			((TextEditorControlBase)txtErectionPercentage).Focus();
			return false;
		}
		if (((Control)(object)txtTestPercentage).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال Test Percentage" : "Please Enter TestPercentage");
			((TextEditorControlBase)txtTestPercentage).Focus();
			return false;
		}
		if (Main.CheckForValueByBranchIDAndFiscalYearID("Cns_Contracts", "ContractCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ContractCode"].ToString(), GlobalVariables.CurrentBranchID, DateCol, dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), "") > 0)
		{
			string codeByBranchID = Contracts.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
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
			((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				return false;
			}
			if (UsingColors && (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0))
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"];
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ColorID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemSizeID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار الصنف ", "Cannot Duplicate The Same Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Contracts.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboProjects).Value.ToString(), ((TextEditorControlBase)cboStores).Value.ToString(), ((TextEditorControlBase)cboSubAccount).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtDownPaymentPercentage).Text == "") ? "0" : ((Control)(object)txtDownPaymentPercentage).Text, (((Control)(object)txtDownPayment).Text == "") ? "0" : ((Control)(object)txtDownPayment).Text, (((Control)(object)txtConstractionInsurensePercentage).Text == "") ? "0" : ((Control)(object)txtConstractionInsurensePercentage).Text, (((Control)(object)txtConstractionInsurense).Text == "") ? "0" : ((Control)(object)txtConstractionInsurense).Text, (((Control)(object)txtWorkerInsurensePercentage).Text == "") ? "0" : ((Control)(object)txtWorkerInsurensePercentage).Text, (((Control)(object)txtWorkerInsurense).Text == "") ? "0" : ((Control)(object)txtWorkerInsurense).Text, "0", (((Control)(object)txtSupplyPercentage).Text == "") ? "0" : ((Control)(object)txtSupplyPercentage).Text, (((Control)(object)txtErectionPercentage).Text == "") ? "0" : ((Control)(object)txtErectionPercentage).Text, (((Control)(object)txtTestPercentage).Text == "") ? "0" : ((Control)(object)txtTestPercentage).Text, ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ContractDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["ContractID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessories).Rows).Count; j++)
			{
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ContractAccessoryID"].Value = -1;
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ContractID"].Value = num;
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGAccessories).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGAccessories).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGAccessories).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGAccessories).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGAccessories).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ContractsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessories).Rows).Count > 0)
			{
				ContractsAccessories.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGAccessories).DataSource, GlobalVariables.UserID);
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
			int num = Contracts.Insert_Update(drMaster["ContractID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboProjects).Value.ToString(), ((TextEditorControlBase)cboStores).Value.ToString(), ((TextEditorControlBase)cboSubAccount).Value.ToString(), (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (((Control)(object)txtDownPaymentPercentage).Text == "") ? "0" : ((Control)(object)txtDownPaymentPercentage).Text, (((Control)(object)txtDownPayment).Text == "") ? "0" : ((Control)(object)txtDownPayment).Text, (((Control)(object)txtConstractionInsurensePercentage).Text == "") ? "0" : ((Control)(object)txtConstractionInsurensePercentage).Text, (((Control)(object)txtConstractionInsurense).Text == "") ? "0" : ((Control)(object)txtConstractionInsurense).Text, (((Control)(object)txtWorkerInsurensePercentage).Text == "") ? "0" : ((Control)(object)txtWorkerInsurensePercentage).Text, (((Control)(object)txtWorkerInsurense).Text == "") ? "0" : ((Control)(object)txtWorkerInsurense).Text, "0", (((Control)(object)txtSupplyPercentage).Text == "") ? "0" : ((Control)(object)txtSupplyPercentage).Text, (((Control)(object)txtErectionPercentage).Text == "") ? "0" : ((Control)(object)txtErectionPercentage).Text, (((Control)(object)txtTestPercentage).Text == "") ? "0" : ((Control)(object)txtTestPercentage).Text, ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			string text2 = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ContractID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ContractDetailID"].Value.ToString() + ",";
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessories).Rows).Count; j++)
			{
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ContractID"].Value = num;
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ColorID"].Value = ((((UltraGridBase)ULGAccessories).Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGAccessories).Rows[j].Cells["ColorID"].Value);
				((UltraGridBase)ULGAccessories).Rows[j].Cells["ItemSizeID"].Value = ((((UltraGridBase)ULGAccessories).Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGAccessories).Rows[j].Cells["ItemSizeID"].Value);
				((UltraGridBase)ULGAccessories).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text2 = text2 + ((UltraGridBase)ULGAccessories).Rows[j].Cells["ContractAccessoryID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("Cns_ContractsDetails", "ContractID", drMaster["ContractID"].ToString(), "ContractDetailID", text);
			ContractsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			Main.DeleteForUpdate("Cns_ContractsAccessories", "ContractID", drMaster["ContractID"].ToString(), "ContractAccessoryID", text2);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGAccessories).Rows).Count > 0)
			{
				ContractsAccessories.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGAccessories).DataSource, GlobalVariables.UserID);
			}
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
			ContractsDetails.DeleteVirtualByContractID(drMaster["ContractID"].ToString(), GlobalVariables.UserID);
			ContractsAccessories.DeleteVirtualByContractID(drMaster["ContractID"].ToString(), GlobalVariables.UserID);
			Contracts.DeleteVirtual(drMaster["ContractID"].ToString(), GlobalVariables.UserID);
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
			string text = "";
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				text = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Cns_Contracts_A.rpt" : "Rep_Cns_Contracts_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ContractIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@ContractIDs", "," + RowID + ",", "Rep_Cns_ContractsAccessories");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0", "Rep_Cns_ContractsAccessories");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CnsContractsReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ContractID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
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
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (Adding)
		{
			DataView dataView = new DataView(dtItems);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int k = 0; k < dataTable.Rows.Count; k++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[k]["ItemID"], dataTable.Rows[k]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[k]["ItemID"], dataTable.Rows[k]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int l = 0; l < dtItems.Rows.Count; l++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[l]["ItemID"], dtItems.Rows[l]["ItemBarCode"].ToString());
			}
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int m = 0; m < dtUnits.Rows.Count; m++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[m]["UnitID"], dtUnits.Rows[m]["UnitName"].ToString());
		}
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
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
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void ULGAccessories_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		ULGAccessories.CellListSelect -= new CellEventHandler(ULGAccessories_CellListSelect);
		((UltraGridBase)ULGAccessories).UpdateData();
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			int num = ((((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num);
				((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
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
		ULGAccessories.CellListSelect += new CellEventHandler(ULGAccessories_CellListSelect);
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGData.AfterExitEditMode -= ULGData_AfterExitEditMode;
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID") && ULGData.ActiveCell.Value != null && ULGData.ActiveCell.Value != DBNull.Value && dtItems.Select(" ItemID= " + ULGData.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGData.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		ULGData.AfterExitEditMode += ULGData_AfterExitEditMode;
	}

	private void ULGAccessories_AfterExitEditMode(object sender, EventArgs e)
	{
		ULGAccessories.AfterExitEditMode -= ULGAccessories_AfterExitEditMode;
		if ((((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "ItemID") && ULGAccessories.ActiveCell.Value != null && ULGAccessories.ActiveCell.Value != DBNull.Value && dtItems.Select(" ItemID= " + ULGAccessories.ActiveCell.Value.ToString()).Length == 0)
		{
			UltraGridCell activeCell = ULGAccessories.ActiveCell;
			UltraGridCell obj = ((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"];
			object obj2 = (((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value = DBNull.Value);
			object value2 = (obj.Value = obj2);
			activeCell.Value = value2;
		}
		ULGAccessories.AfterExitEditMode += ULGAccessories_AfterExitEditMode;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value || (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if ((drMaster != null && Convert.ToBoolean(drMaster["Approved"]) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalQty") || (drMaster != null && !Convert.ToBoolean(drMaster["Approved"]) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalQty") || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGAccessories_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGAccessories).ActiveRow).Selected = true;
		}
		if (((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "UnitID" && (((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"].Value == DBNull.Value || (((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"].Value != DBNull.Value && bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))))
		{
			((GridItemBase)((UltraGridBase)ULGAccessories).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalTotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice") && !((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.Equals(DBNull.Value))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["OriginalTotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OriginalQty"].Value.ToString());
				((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalTotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalQty"].Value.ToString());
				decimal num = default(decimal);
				((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value = (num = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OriginalQty"].Value.ToString()) + decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalQty"].Value.ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["TotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value.ToString()) * num;
				CalculateGross();
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		CalculateGross();
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F6)
		{
			if (Adding || Updating)
			{
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalQty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit2.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["OriginalQty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["OriginalQty"].Value);
				}
				if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalQty" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
				{
					frmQuantityMultiUnit frmQuantityMultiUnit3 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
					frmQuantityMultiUnit3.WindowState = FormWindowState.Normal;
					frmQuantityMultiUnit3.ShowDialog();
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit3.UnitID > 0) ? ((object)frmQuantityMultiUnit3.UnitID) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value);
					((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalQty"].Value = ((frmQuantityMultiUnit3.Qty > 0m) ? ((object)frmQuantityMultiUnit3.Qty) : ((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalQty"].Value);
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
			if ((Adding || Updating) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode"))
			{
				int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
				if (num != 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = num;
				}
			}
			e.Handled = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "OriginalQty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AdditionalQty"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGAccessories_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGAccessories.ActiveCell != null && ((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "Qty")
		{
			GlobalFunctions.CheckForNumbers(ULGAccessories.ActiveCell, e);
		}
	}

	private void ULGAccessories_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGAccessories_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Expected O, but got Unknown
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Expected O, but got Unknown
		if (e.KeyCode == Keys.F6)
		{
			if ((Adding || Updating) && ((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "Qty" && ((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"].Value != DBNull.Value)
			{
				frmQuantityMultiUnit frmQuantityMultiUnit2 = new frmQuantityMultiUnit(int.Parse(((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value.ToString()), int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()), isreadonlyunitcolumn: false);
				frmQuantityMultiUnit2.WindowState = FormWindowState.Normal;
				frmQuantityMultiUnit2.ShowDialog();
				((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value = ((frmQuantityMultiUnit2.UnitID > 0) ? ((object)frmQuantityMultiUnit2.UnitID) : ((UltraGridBase)ULGAccessories).ActiveRow.Cells["UnitID"].Value);
				((UltraGridBase)ULGAccessories).ActiveRow.Cells["Qty"].Value = ((frmQuantityMultiUnit2.Qty > 0m) ? ((object)frmQuantityMultiUnit2.Qty) : ((UltraGridBase)ULGAccessories).ActiveRow.Cells["Qty"].Value);
			}
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.F8)
		{
			if ((Adding || Updating) && (((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGAccessories.ActiveCell.Column).Key == "ItemBarCode"))
			{
				int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
				if (num != 0)
				{
					((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemID"].Value = num;
					((UltraGridBase)ULGAccessories).ActiveRow.Cells["ItemBarCode"].Value = num;
				}
			}
			e.Handled = true;
		}
		else
		{
			if (e.KeyCode != Keys.F11 || (!Adding && !Updating))
			{
				return;
			}
			if (ULGAccessories.ActiveCell.ValueList != null)
			{
				frmValueListSearch frmValueListSearch2 = new frmValueListSearch((ValueList)ULGAccessories.ActiveCell.ValueList, ((object)ULGAccessories.ActiveCell.Column.Header).ToString());
				frmValueListSearch2.WindowState = FormWindowState.Normal;
				frmValueListSearch2.ShowDialog();
				if (frmValueListSearch2.ResultID > 0)
				{
					ULGAccessories.ActiveCell.Value = frmValueListSearch2.ResultID;
				}
			}
			else if (ULGAccessories.ActiveCell.Column.ValueList != null)
			{
				frmValueListSearch frmValueListSearch3 = new frmValueListSearch((ValueList)ULGAccessories.ActiveCell.Column.ValueList, ((object)ULGAccessories.ActiveCell.Column.Header).ToString());
				frmValueListSearch3.WindowState = FormWindowState.Normal;
				frmValueListSearch3.ShowDialog();
				if (frmValueListSearch3.ResultID > 0)
				{
					ULGAccessories.ActiveCell.Value = frmValueListSearch3.ResultID;
				}
			}
		}
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			if (!CanUpdate)
			{
				GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
				return;
			}
			if (!CanModifyOtherBranch)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
				return;
			}
			if (ClosedPeriod)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Update This Transaction Because It Related to ClosedPeriod ");
				return;
			}
			Updating = true;
			SetControls(NavMode: false);
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
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (Adding)
		{
			((Control)(object)txtCode).Text = Contracts.GetCodeByBranchID(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtDownPaymentPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((Control)(object)txtDownPayment).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDownPaymentPercentage).Text == "" || ((Control)(object)txtDownPaymentPercentage).Text == ".") ? "0" : ((Control)(object)txtDownPaymentPercentage).Text) * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	private void txtConstractionInsurensePercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((Control)(object)txtConstractionInsurense).Text = decimal.Parse((decimal.Parse((((Control)(object)txtConstractionInsurensePercentage).Text == "" || ((Control)(object)txtConstractionInsurensePercentage).Text == ".") ? "0" : ((Control)(object)txtConstractionInsurensePercentage).Text) * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	private void txtWorkerInsurensePercentage_ValueChanged(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			((Control)(object)txtWorkerInsurense).Text = decimal.Parse((decimal.Parse((((Control)(object)txtWorkerInsurensePercentage).Text == "" || ((Control)(object)txtWorkerInsurensePercentage).Text == ".") ? "0" : ((Control)(object)txtWorkerInsurensePercentage).Text) * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text) / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	private void CalculateGross()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.Equals(DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].Cells["TotalPrice"].Value.ToString());
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDownPayment).Text = decimal.Parse((decimal.Parse((((Control)(object)txtDownPaymentPercentage).Text == "" || ((Control)(object)txtDownPaymentPercentage).Text == ".") ? "0" : ((Control)(object)txtDownPaymentPercentage).Text) * num / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtConstractionInsurense).Text = decimal.Parse((decimal.Parse((((Control)(object)txtConstractionInsurensePercentage).Text == "" || ((Control)(object)txtConstractionInsurensePercentage).Text == ".") ? "0" : ((Control)(object)txtConstractionInsurensePercentage).Text) * num / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtWorkerInsurense).Text = decimal.Parse((decimal.Parse((((Control)(object)txtWorkerInsurensePercentage).Text == "" || ((Control)(object)txtWorkerInsurensePercentage).Text == ".") ? "0" : ((Control)(object)txtWorkerInsurensePercentage).Text) * num / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
	}

	private void btnProjectSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CnsProjectsWithoutContracts(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboProjects).Value = num;
		}
	}

	private void btnStoreSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Stores(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboStores).Value = num;
		}
	}

	private void btnClientSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Clients("-1", "-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboSubAccount).Value = num;
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
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Expected O, but got Unknown
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Expected O, but got Unknown
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Expected O, but got Unknown
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Expected O, but got Unknown
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Expected O, but got Unknown
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected O, but got Unknown
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Expected O, but got Unknown
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Expected O, but got Unknown
		//IL_0d67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d71: Expected O, but got Unknown
		//IL_0d7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d89: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CnsProjects.Transactions.frmContracts));
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
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGAccessories = new UltraGrid();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboProjects = new UltraComboEditor();
		this.lblCountry = new UltraLabel();
		this.lblProjects = new UltraLabel();
		this.cboStores = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.ultraLabel2 = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.txtDownPayment = new UltraTextEditor();
		this.txtDownPaymentPercentage = new UltraTextEditor();
		this.ultraLabel4 = new UltraLabel();
		this.txtConstractionInsurense = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.txtConstractionInsurensePercentage = new UltraTextEditor();
		this.ultraLabel6 = new UltraLabel();
		this.txtWorkerInsurense = new UltraTextEditor();
		this.ultraLabel7 = new UltraLabel();
		this.txtWorkerInsurensePercentage = new UltraTextEditor();
		this.ultraLabel8 = new UltraLabel();
		this.txtSupplyPercentage = new UltraTextEditor();
		this.txtErectionPercentage = new UltraTextEditor();
		this.ultraLabel11 = new UltraLabel();
		this.txtTestPercentage = new UltraTextEditor();
		this.ultraLabel12 = new UltraLabel();
		this.ultraLabel13 = new UltraLabel();
		this.ultraLabel10 = new UltraLabel();
		this.ultraLabel14 = new UltraLabel();
		this.ultraLabel15 = new UltraLabel();
		this.btnClientSearch = new UltraButton();
		this.btnStoreSearch = new UltraButton();
		this.btnProjectSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGAccessories).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboProjects).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStores).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPayment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPaymentPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurense).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurensePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurense).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurensePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplyPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtErectionPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTestPercentage).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "Accessories";
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance9");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
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
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGAccessories);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGAccessories, "ULGAccessories");
		((UltraGridBase)this.ULGAccessories).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGAccessories).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGAccessories).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGAccessories).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGAccessories).Name = "ULGAccessories";
		((UltraControlBase)this.ULGAccessories).UseFlatMode = (DefaultableBoolean)1;
		this.ULGAccessories.AfterEnterEditMode += new System.EventHandler(ULGAccessories_AfterEnterEditMode);
		this.ULGAccessories.AfterExitEditMode += new System.EventHandler(ULGAccessories_AfterExitEditMode);
		this.ULGAccessories.CellListSelect += new CellEventHandler(ULGAccessories_CellListSelect);
		this.ULGAccessories.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGAccessories_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGAccessories).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGAccessories_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGAccessories).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGAccessories_KeyPress);
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
		resources.ApplyResources(this.cboProjects, "cboProjects");
		((System.Windows.Forms.Control)(object)this.cboProjects).Name = "cboProjects";
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val15, "appearance19");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val15;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.lblProjects, "lblProjects");
		this.lblProjects.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProjects).Name = "lblProjects";
		((ControlBase)this.lblProjects).WrapText = false;
		resources.ApplyResources(this.cboStores, "cboStores");
		((System.Windows.Forms.Control)(object)this.cboStores).Name = "cboStores";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance20");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val16;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.txtDownPayment, "txtDownPayment");
		((System.Windows.Forms.Control)(object)this.txtDownPayment).Name = "txtDownPayment";
		((System.Windows.Forms.Control)(object)this.txtDownPayment).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtDownPaymentPercentage, "txtDownPaymentPercentage");
		((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage).Name = "txtDownPaymentPercentage";
		((TextEditorControlBase)this.txtDownPaymentPercentage).ValueChanged += new System.EventHandler(txtDownPaymentPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		this.ultraLabel4.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		resources.ApplyResources(this.txtConstractionInsurense, "txtConstractionInsurense");
		((System.Windows.Forms.Control)(object)this.txtConstractionInsurense).Name = "txtConstractionInsurense";
		((System.Windows.Forms.Control)(object)this.txtConstractionInsurense).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.txtConstractionInsurensePercentage, "txtConstractionInsurensePercentage");
		((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage).Name = "txtConstractionInsurensePercentage";
		((TextEditorControlBase)this.txtConstractionInsurensePercentage).ValueChanged += new System.EventHandler(txtConstractionInsurensePercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.txtWorkerInsurense, "txtWorkerInsurense");
		((System.Windows.Forms.Control)(object)this.txtWorkerInsurense).Name = "txtWorkerInsurense";
		((System.Windows.Forms.Control)(object)this.txtWorkerInsurense).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		this.ultraLabel7.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.txtWorkerInsurensePercentage, "txtWorkerInsurensePercentage");
		((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage).Name = "txtWorkerInsurensePercentage";
		((TextEditorControlBase)this.txtWorkerInsurensePercentage).ValueChanged += new System.EventHandler(txtWorkerInsurensePercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		this.ultraLabel8.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		resources.ApplyResources(this.txtSupplyPercentage, "txtSupplyPercentage");
		((System.Windows.Forms.Control)(object)this.txtSupplyPercentage).Name = "txtSupplyPercentage";
		((System.Windows.Forms.Control)(object)this.txtSupplyPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtErectionPercentage, "txtErectionPercentage");
		((System.Windows.Forms.Control)(object)this.txtErectionPercentage).Name = "txtErectionPercentage";
		((System.Windows.Forms.Control)(object)this.txtErectionPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		this.ultraLabel11.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this.txtTestPercentage, "txtTestPercentage");
		((System.Windows.Forms.Control)(object)this.txtTestPercentage).Name = "txtTestPercentage";
		((System.Windows.Forms.Control)(object)this.txtTestPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		this.ultraLabel12.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		((ControlBase)this.ultraLabel12).WrapText = false;
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		this.ultraLabel13.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		((ControlBase)this.ultraLabel13).WrapText = false;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		this.ultraLabel10.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		this.ultraLabel14.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		this.ultraLabel15.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		resources.ApplyResources(this.btnClientSearch, "btnClientSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance21");
		((ControlBase)this.btnClientSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Name = "btnClientSearch";
		((System.Windows.Forms.Control)(object)this.btnClientSearch).Click += new System.EventHandler(btnClientSearch_Click);
		resources.ApplyResources(this.btnStoreSearch, "btnStoreSearch");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val18, "appearance22");
		((ControlBase)this.btnStoreSearch).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Name = "btnStoreSearch";
		((System.Windows.Forms.Control)(object)this.btnStoreSearch).Click += new System.EventHandler(btnStoreSearch_Click);
		resources.ApplyResources(this.btnProjectSearch, "btnProjectSearch");
		((AppearanceBase)val19).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val19, "appearance23");
		((ControlBase)this.btnProjectSearch).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.btnProjectSearch).Name = "btnProjectSearch";
		((System.Windows.Forms.Control)(object)this.btnProjectSearch).Click += new System.EventHandler(btnProjectSearch_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnProjectSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnStoreSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClientSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTestPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtErectionPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSupplyPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkerInsurense);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConstractionInsurense);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDownPayment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProjects);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStores);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboProjects);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmContracts";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboProjects, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStores, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProjects, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDownPayment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConstractionInsurense, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkerInsurense, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSupplyPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtErectionPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTestPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel13, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel10, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel14, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel15, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel11, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel12, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClientSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnStoreSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnProjectSearch, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGAccessories).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboProjects).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStores).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPayment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPaymentPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurense).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurensePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurense).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurensePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplyPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtErectionPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTestPercentage).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
