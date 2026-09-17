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
using ERP.Accounting.Transactions;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CnsProjects.Transactions;

public class frmContractsDues : frmHeaderDetails
{
	private DataTable dtItems;

	private DataTable dtUnits;

	private DataTable dtClients;

	private DataTable dtTaxes;

	private DataTable dtReports;

	private ValueList vlItems = new ValueList();

	private ValueList vlUnits = new ValueList();

	private DataTable dtContracts;

	private decimal AllowedDownPayment = default(decimal);

	private IContainer components = null;

	private UltraLabel lblContracts;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboContracts;

	private UltraLabel ultraLabel12;

	private UltraLabel ultraLabel11;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel14;

	private UltraLabel ultraLabel10;

	private UltraLabel ultraLabel4;

	private UltraLabel ultraLabel13;

	private UltraTextEditor txtTestPercentage;

	private UltraTextEditor txtErectionPercentage;

	private UltraTextEditor txtSupplyPercentage;

	private UltraTextEditor txtDownPaymentPercentage;

	private UltraLabel ultraLabel3;

	private UltraComboEditor cboSubAccount;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraTextEditor txtTotalPrice;

	private UltraLabel ultraLabel8;

	private UltraLabel ultraLabel6;

	private UltraTextEditor txtWorkerInsurensePercentage;

	private UltraLabel ultraLabel7;

	private UltraTextEditor txtConstractionInsurensePercentage;

	private UltraLabel ultraLabel5;

	private UltraTextEditor txtSalesTax;

	private UltraLabel ultraLabel9;

	private UltraTextEditor txtCommercialTax;

	private UltraLabel ultraLabel16;

	private UltraTextEditor txtConstractionInsurense;

	private UltraLabel ultraLabel17;

	private UltraTextEditor txtWorkerInsurense;

	private UltraLabel ultraLabel18;

	private UltraTextEditor txtDownPayment;

	private UltraLabel ultraLabel19;

	private UltraTextEditor txtNetPrice;

	private UltraLabel ultraLabel20;

	public UltraButton btnContractSearch;

	public UltraButton btnJV2;

	public UltraButton btnJV;

	private UltraLabel lblSalesTax;

	private UltraTextEditor txtSalesTaxValue;

	private UltraComboEditor cboSalesTax;

	public frmContractsDues()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Cns_ContractsDues";
		IDCol = "ContractDueID";
		NoCol = "ContractDueCode";
		DateCol = "ContractDueDate";
	}

	public frmContractsDues(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtClients = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSubAccount, dtClients, "SubAccountID", "SubAccountName");
		dtItems = Items.FillCombo("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtUnits = Units.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlUnits.ValueListItems.Clear();
		for (int j = 0; j < dtUnits.Rows.Count; j++)
		{
			vlUnits.ValueListItems.Add(dtUnits.Rows[j]["UnitID"], dtUnits.Rows[j]["UnitName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesTax, dtTaxes, "TaxID", "TaxName");
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtContracts = Contracts.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboContracts, dtContracts, "ContractID", "ContractName");
		dtDetails = ContractsDuesDetails.SelectByContractDueID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.CellAppearance.TextHAlign = (HAlign)2;
		((UltraGridBase)ULGData).DisplayLayout.Appearance.BackColor = Color.Transparent;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Clear();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].LevelCount = 3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].ColHeadersVisible = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Override.RowSpacingAfter = 4;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G0", GlobalVariables.IsArabic ? "الصنـف" : "Item  ");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G4", GlobalVariables.IsArabic ? "كمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G5", GlobalVariables.IsArabic ? "وحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G6", GlobalVariables.IsArabic ? "سعر" : "Unit Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G7", GlobalVariables.IsArabic ? "اجمالي" : "Total Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G14", "-");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G8", GlobalVariables.IsArabic ? "سابق" : "Prev");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G9", GlobalVariables.IsArabic ? "حالي" : "Current");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G10", GlobalVariables.IsArabic ? "اجمالي" : "Total");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G15", " % ");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G11", GlobalVariables.IsArabic ? "اجمالي القيمه" : "Total Amount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G12", GlobalVariables.IsArabic ? "المستحق" : "Req Amount");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups.Add("G13", GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G0"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G4"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G5"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G6"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G7"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G14"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SHeader"], 0, (short)0);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G14"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EHeader"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G14"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["THeader"], 0, (short)2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G8"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevSupply"], 0, (short)0);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G8"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevErection"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G8"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevTest"], 0, (short)2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G9"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentSupply"], 0, (short)0);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G9"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentErection"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G9"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentTest"], 0, (short)2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G10"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalSupply"], 0, (short)0);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G10"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalErection"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G10"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalTest"], 0, (short)2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G15"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplyPercentage"], 0, (short)0);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G15"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ErectionPercentage"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G15"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TestPercentage"], 0, (short)2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G11"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalAmount"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G12"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReqAmount"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Groups["G13"].Columns.Add(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"], 0, (short)1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SHeader"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EHeader"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["THeader"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevSupply"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevErection"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevTest"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentSupply"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentErection"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentTest"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalSupply"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalErection"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalTest"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplyPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.03);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ErectionPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.03);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TestPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.03);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReqAmount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.24);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitPrice"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalPrice"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevSupply"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevErection"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PrevTest"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentSupply"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentErection"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrentTest"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalSupply"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalErection"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalTest"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SupplyPercentage"].Format = "###,##";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ErectionPercentage"].Format = "###,##";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TestPercentage"].Format = "###,##";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalAmount"].Format = "###,##.00";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ReqAmount"].Format = "###,##.00";
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = ContractsDues.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtCode).Text = drMaster["ContractDueCode"].ToString();
			dtpDate.Value = (DateTime)drMaster["ContractDueDate"];
			((TextEditorControlBase)cboContracts).ValueChanged -= cboContracts_ValueChanged;
			((TextEditorControlBase)cboContracts).Value = drMaster["ContractID"];
			((TextEditorControlBase)cboContracts).ValueChanged += cboContracts_ValueChanged;
			((TextEditorControlBase)cboSubAccount).Value = drMaster["SubAccountID"];
			((Control)(object)txtSupplyPercentage).Text = decimal.Parse(drMaster["SupplyPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtErectionPercentage).Text = decimal.Parse(drMaster["ErectionPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTestPercentage).Text = decimal.Parse(drMaster["TestPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtTotalPrice).Text = decimal.Parse(drMaster["TotalPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtConstractionInsurense).Text = decimal.Parse(drMaster["ConstractionInsurenseValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtWorkerInsurense).Text = decimal.Parse(drMaster["WorkerInsurenseValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtSalesTax).ValueChanged -= txtSalesTax_ValueChanged;
			((Control)(object)txtSalesTax).Text = decimal.Parse(drMaster["TaxTotalValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtSalesTax).ValueChanged += txtSalesTax_ValueChanged;
			((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtCommercialTax_ValueChanged;
			((Control)(object)txtCommercialTax).Text = decimal.Parse(drMaster["CommercialTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)txtCommercialTax).ValueChanged += txtCommercialTax_ValueChanged;
			((TextEditorControlBase)cboSalesTax).ValueChanged -= cboSalesTax_ValueChanged;
			((TextEditorControlBase)cboSalesTax).Value = drMaster["SalesTaxID"];
			((Control)(object)txtSalesTaxValue).Text = decimal.Parse(drMaster["SalesTaxValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((TextEditorControlBase)cboSalesTax).ValueChanged += cboSalesTax_ValueChanged;
			((Control)(object)txtDownPayment).Text = decimal.Parse(drMaster["DownPaymentValue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetPrice).Text = decimal.Parse(drMaster["NetPrice"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			AllowedDownPayment = ContractsDues.AllowedDownPayment(drMaster["ContractID"].ToString(), drMaster["ContractDueID"].ToString());
			dtDetails = ContractsDuesDetails.SelectByContractDueID(drMaster["ContractDueID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			if (drMaster["Approved"].Equals(true) || CheckNextContractDue())
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboContracts).ReadOnly = !Adding;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSalesTax).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSalesTax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCommercialTax).ReadOnly = NavMode;
		((Control)(object)btnContractSearch).Visible = Adding;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((Control)(object)txtCode).Text = (Adding ? ContractsDues.GetCode((cboContracts.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboContracts).Value.ToString()) : "");
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((TextEditorControlBase)cboContracts).ValueChanged -= cboContracts_ValueChanged;
		cboContracts.SelectedIndex = -1;
		((TextEditorControlBase)cboContracts).ValueChanged += cboContracts_ValueChanged;
		cboSubAccount.SelectedIndex = -1;
		((TextEditorControlBase)cboSalesTax).ValueChanged -= cboSalesTax_ValueChanged;
		cboSalesTax.SelectedIndex = -1;
		((Control)(object)txtSalesTaxValue).Text = "0";
		((TextEditorControlBase)cboSalesTax).ValueChanged += cboSalesTax_ValueChanged;
		((TextEditorControlBase)txtSupplyPercentage).Clear();
		((TextEditorControlBase)txtErectionPercentage).Clear();
		((TextEditorControlBase)txtTestPercentage).Clear();
		((Control)(object)txtTotalPrice).Text = "0";
		((Control)(object)txtConstractionInsurense).Text = "0";
		((Control)(object)txtWorkerInsurense).Text = "0";
		((TextEditorControlBase)txtSalesTax).ValueChanged -= txtSalesTax_ValueChanged;
		((Control)(object)txtSalesTax).Text = "0";
		((TextEditorControlBase)txtSalesTax).ValueChanged += txtSalesTax_ValueChanged;
		((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtCommercialTax_ValueChanged;
		((Control)(object)txtCommercialTax).Text = "0";
		((TextEditorControlBase)txtCommercialTax).ValueChanged += txtCommercialTax_ValueChanged;
		((Control)(object)txtDownPayment).Text = "0";
		((Control)(object)txtNetPrice).Text = "0";
		((TextEditorControlBase)txtNotes).Clear();
		AllowedDownPayment = default(decimal);
	}

	public override bool ValidateData()
	{
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال التاريخ" : "Please Enter The Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (cboContracts.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار المشروع" : "Please Select Project");
			((TextEditorControlBase)cboContracts).Focus();
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
		if (Main.CheckForValue("Cns_ContractsDues", "ContractDueCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ContractDueCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = ContractsDues.GetCode((cboContracts.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboContracts).Value.ToString());
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الإذن", "Please insert details for this Voucher");
			return false;
		}
		DataTable dataTable = ContractsDues.AccountValidate(((TextEditorControlBase)cboSubAccount).Value.ToString());
		if (dataTable.Rows[0]["DownPaymentAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب حساب دفعات مقدمه من الإعدادات  ", "Please Set DownPayment Account From Setting");
			return false;
		}
		if (int.Parse(dataTable.Rows[0]["DownPaymentRelation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب دفعات مقدمه وحساب العميل  ", "There is No Relation Between DownPayment Account And the client");
			return false;
		}
		if (dataTable.Rows[0]["ConstractionInsurenseAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب التأمين من الإعدادات  ", "Please Set Insurance Account From Setting");
			return false;
		}
		if (int.Parse(dataTable.Rows[0]["CnsInsurenseRelation"].ToString()) == 0)
		{
			GlobalVariables.InformationMB.Show("لايوجد ربط بين حساب التأمين وحساب العميل  ", "There is No Relation Between Insurance Account And the client");
			return false;
		}
		if (dataTable.Rows[0]["WorkerInsurenseAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب التأمينات الإجتماعيه من الإعدادات  ", "Please Set  Workers Insurance Account From Setting");
			return false;
		}
		if (GlobalVariables.dtSystemAccounts.Select(" AccountNameEn='DiscountTaxAccount-Sales' ")[0]["AccountID"] == DBNull.Value && ((Control)(object)txtCommercialTax).Text != "" && decimal.Parse(((Control)(object)txtCommercialTax).Text) > 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء تحديد حساب ضريبة الخصم من حسابات النظام  ", "Please Select Discount Tax Account From SystemAccounts ");
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = ContractsDues.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboContracts).Value.ToString(), ((TextEditorControlBase)cboSubAccount).Value.ToString(), ((Control)(object)txtSupplyPercentage).Text, ((Control)(object)txtErectionPercentage).Text, ((Control)(object)txtTestPercentage).Text, (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (cboSalesTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesTax).Value.ToString(), (cboSalesTax.SelectedIndex == -1 || ((Control)(object)txtSalesTaxValue).Text == "") ? "0" : ((Control)(object)txtSalesTaxValue).Text, (((Control)(object)txtConstractionInsurense).Text == "") ? "0" : ((Control)(object)txtConstractionInsurense).Text, (((Control)(object)txtWorkerInsurense).Text == "") ? "0" : ((Control)(object)txtWorkerInsurense).Text, (((Control)(object)txtSalesTax).Text == "") ? "0" : ((Control)(object)txtSalesTax).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtDownPayment).Text == "") ? "0" : ((Control)(object)txtDownPayment).Text, (((Control)(object)txtNetPrice).Text == "") ? "0" : ((Control)(object)txtNetPrice).Text, ((Control)(object)txtNotes).Text, "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ContractDueDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["ContractDueID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			ContractsDuesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ContractsDues.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			ContractsDues.GenerateJvs2("," + num + ",", GlobalVariables.UserID);
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
			int num = ContractsDues.Insert_Update(drMaster["ContractDueID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((TextEditorControlBase)cboContracts).Value.ToString(), ((TextEditorControlBase)cboSubAccount).Value.ToString(), ((Control)(object)txtSupplyPercentage).Text, ((Control)(object)txtErectionPercentage).Text, ((Control)(object)txtTestPercentage).Text, (((Control)(object)txtTotalPrice).Text == "") ? "0" : ((Control)(object)txtTotalPrice).Text, (cboSalesTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesTax).Value.ToString(), (cboSalesTax.SelectedIndex == -1 || ((Control)(object)txtSalesTaxValue).Text == "") ? "0" : ((Control)(object)txtSalesTaxValue).Text, (((Control)(object)txtConstractionInsurense).Text == "") ? "0" : ((Control)(object)txtConstractionInsurense).Text, (((Control)(object)txtWorkerInsurense).Text == "") ? "0" : ((Control)(object)txtWorkerInsurense).Text, (((Control)(object)txtSalesTax).Text == "") ? "0" : ((Control)(object)txtSalesTax).Text, (((Control)(object)txtCommercialTax).Text == "") ? "0" : ((Control)(object)txtCommercialTax).Text, (((Control)(object)txtDownPayment).Text == "") ? "0" : ((Control)(object)txtDownPayment).Text, (((Control)(object)txtNetPrice).Text == "") ? "0" : ((Control)(object)txtNetPrice).Text, ((Control)(object)txtNotes).Text, bool.Parse(drMaster["Approved"].ToString()) ? "1" : "0", bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ContractDueID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ContractDueDetailID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("Cns_ContractsDuesDetails", "ContractDueID", drMaster["ContractDueID"].ToString(), "ContractDueDetailID", text, IsFromServer: false);
			ContractsDuesDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			ContractsDues.GenerateJvs("," + num + ",", GlobalVariables.UserID);
			ContractsDues.GenerateJvs2("," + num + ",", GlobalVariables.UserID);
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
			JVDetails.DeleteVirtualByJVID(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			JV.DeleteVirtual(drMaster["JVID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			JVDetails.DeleteVirtualByJVID(drMaster["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			JV.DeleteVirtual(drMaster["JVID2"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			ContractsDuesDetails.DeleteVirtualByContractDueID(drMaster["ContractDueID"].ToString(), GlobalVariables.UserID);
			ContractsDues.DeleteVirtual(drMaster["ContractDueID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public bool CheckNextContractDue()
	{
		string text = ContractsDues.CheckNextDues(drMaster["ContractDueID"].ToString(), drMaster["ContractID"].ToString(), drMaster["ContractDueDate"].ToString(), GlobalVariables.IsArabic ? "1" : "0").Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			return true;
		}
		return false;
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
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Cns_ContractsDues_A.rpt" : "Rep_Cns_ContractsDues_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@ContractDueIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CnsContractsDuesReport(-1, 0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ContractDueID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesTax, dtTaxes, "TaxID", "TaxName");
	}

	private void cboContracts_ValueChanged(object sender, EventArgs e)
	{
		if (cboContracts.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboSubAccount).Value = dtContracts.Rows[cboContracts.SelectedIndex]["SubAccountID"];
			((Control)(object)txtDownPaymentPercentage).Text = decimal.Parse(dtContracts.Rows[cboContracts.SelectedIndex]["DownPaymentPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtConstractionInsurensePercentage).Text = decimal.Parse(dtContracts.Rows[cboContracts.SelectedIndex]["ConstractionInsurensePercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtWorkerInsurensePercentage).Text = decimal.Parse(dtContracts.Rows[cboContracts.SelectedIndex]["WorkerInsurensePercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSupplyPercentage).Text = decimal.Parse(dtContracts.Rows[cboContracts.SelectedIndex]["SupplyPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtErectionPercentage).Text = decimal.Parse(dtContracts.Rows[cboContracts.SelectedIndex]["ErectionPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtTestPercentage).Text = decimal.Parse(dtContracts.Rows[cboContracts.SelectedIndex]["TestPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNotes).Text = dtContracts.Rows[cboContracts.SelectedIndex]["Notes"].ToString();
			AllowedDownPayment = ContractsDues.AllowedDownPayment(((TextEditorControlBase)cboContracts).Value.ToString(), "-1");
			dtDetails = ContractsDetails.SelectforContractsDues(((TextEditorControlBase)cboContracts).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateGross();
			if (Adding)
			{
				((Control)(object)txtCode).Text = ContractsDues.GetCode((cboContracts.SelectedIndex == -1) ? "" : ((TextEditorControlBase)cboContracts).Value.ToString());
			}
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtSupplyPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtSupplyPercentage).Text != "" && ((Control)(object)txtSupplyPercentage).Text != ".")
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SupplyPercentage"].Value = ((Control)(object)txtSupplyPercentage).Text;
			}
			ReCalculateRows();
		}
	}

	private void txtErectionPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtErectionPercentage).Text != "" && ((Control)(object)txtErectionPercentage).Text != ".")
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ErectionPercentage"].Value = ((Control)(object)txtErectionPercentage).Text;
			}
			ReCalculateRows();
		}
	}

	private void txtTestPercentage_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtTestPercentage).Text != "" && ((Control)(object)txtTestPercentage).Text != ".")
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["TestPercentage"].Value = ((Control)(object)txtTestPercentage).Text;
			}
			ReCalculateRows();
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "CurrentSupply" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "CurrentErection" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "CurrentTest" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "SupplyPercentage" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ErectionPercentage" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "TestPercentage")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0779: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrentSupply" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrentErection" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrentTest" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SupplyPercentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ErectionPercentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TestPercentage"))
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			decimal num3 = default(decimal);
			decimal num4 = default(decimal);
			decimal num5 = default(decimal);
			decimal num6 = default(decimal);
			decimal num7 = default(decimal);
			decimal num8 = default(decimal);
			decimal num9 = default(decimal);
			decimal num10 = default(decimal);
			if (!((UltraGridBase)ULGData).ActiveRow.Cells["SupplyPercentage"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["SupplyPercentage"].Value.Equals("") && !((UltraGridBase)ULGData).ActiveRow.Cells["SupplyPercentage"].Value.Equals("."))
			{
				num = Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["SupplyPercentage"].Value) / 100m;
			}
			if (!((UltraGridBase)ULGData).ActiveRow.Cells["ErectionPercentage"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["ErectionPercentage"].Value.Equals("") && !((UltraGridBase)ULGData).ActiveRow.Cells["ErectionPercentage"].Value.Equals("."))
			{
				num2 = Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["ErectionPercentage"].Value) / 100m;
			}
			if (!((UltraGridBase)ULGData).ActiveRow.Cells["TestPercentage"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["TestPercentage"].Value.Equals("") && !((UltraGridBase)ULGData).ActiveRow.Cells["TestPercentage"].Value.Equals("."))
			{
				num3 = Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["TestPercentage"].Value) / 100m;
			}
			if (!((UltraGridBase)ULGData).ActiveRow.Cells["CurrentSupply"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["CurrentSupply"].Value.Equals("") && !((UltraGridBase)ULGData).ActiveRow.Cells["CurrentSupply"].Value.Equals("."))
			{
				num4 = Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["CurrentSupply"].Value);
			}
			if (!((UltraGridBase)ULGData).ActiveRow.Cells["CurrentErection"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["CurrentErection"].Value.Equals("") && !((UltraGridBase)ULGData).ActiveRow.Cells["CurrentErection"].Value.Equals("."))
			{
				num5 = Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["CurrentErection"].Value);
			}
			if (!((UltraGridBase)ULGData).ActiveRow.Cells["CurrentTest"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).ActiveRow.Cells["CurrentTest"].Value.Equals("") && !((UltraGridBase)ULGData).ActiveRow.Cells["CurrentTest"].Value.Equals("."))
			{
				num6 = Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["CurrentTest"].Value);
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalSupply"].Value = (num7 = num4 + Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["PrevSupply"].Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalErection"].Value = (num8 = num5 + Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["PrevErection"].Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalTest"].Value = (num9 = num6 + Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["PrevTest"].Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalAmount"].Value = (num10 = (num7 * num + num8 * num2 + num9 * num3) * Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value));
			((UltraGridBase)ULGData).ActiveRow.Cells["ReqAmount"].Value = num10 - Convert.ToDecimal(((UltraGridBase)ULGData).ActiveRow.Cells["PrevReqAmount"].Value);
			CalculateGross();
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrentSupply" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrentErection" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CurrentTest" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SupplyPercentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ErectionPercentage" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TestPercentage"))
		{
			GlobalFunctions.CheckForNumbersNegative(ULGData.ActiveCell, e);
		}
	}

	private void CalculateGross()
	{
		decimal num = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num += Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["ReqAmount"].Value);
		}
		((Control)(object)txtTotalPrice).Text = decimal.Parse(num.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		CalculateSalesTax();
		CalculateTotals();
	}

	private void CalculateTotals()
	{
		if (Adding || Updating)
		{
			decimal num = decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text);
			if (((TextEditorControlBase)cboSubAccount).Value != null && !((TextEditorControlBase)cboSubAccount).Value.Equals(DBNull.Value) && bool.Parse(dtClients.Select("SubAccountID =" + ((TextEditorControlBase)cboSubAccount).Value.ToString())[0]["IsDiscountTax"].ToString()))
			{
				((TextEditorControlBase)txtCommercialTax).ValueChanged -= txtCommercialTax_ValueChanged;
				((Control)(object)txtCommercialTax).Text = decimal.Parse((num * (decimal)GlobalVariables.DiscountTax).ToString()).ToString(GlobalVariables.txtDecimalFormate);
				((TextEditorControlBase)txtCommercialTax).ValueChanged += txtCommercialTax_ValueChanged;
			}
			((Control)(object)txtConstractionInsurense).Text = decimal.Parse((num * decimal.Parse((((Control)(object)txtConstractionInsurensePercentage).Text == "" || ((Control)(object)txtConstractionInsurensePercentage).Text == ".") ? "0" : ((Control)(object)txtConstractionInsurensePercentage).Text) / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtWorkerInsurense).Text = decimal.Parse((num * decimal.Parse((((Control)(object)txtWorkerInsurensePercentage).Text == "" || ((Control)(object)txtWorkerInsurensePercentage).Text == ".") ? "0" : ((Control)(object)txtWorkerInsurensePercentage).Text) / 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			decimal num2 = num * decimal.Parse((((Control)(object)txtDownPaymentPercentage).Text == "" || ((Control)(object)txtDownPaymentPercentage).Text == ".") ? "0" : ((Control)(object)txtDownPaymentPercentage).Text) / 100m;
			num2 = ((num2 > AllowedDownPayment) ? AllowedDownPayment : num2);
			((Control)(object)txtDownPayment).Text = decimal.Parse(num2.ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtNetPrice).Text = decimal.Parse((num - num2 + decimal.Parse((((Control)(object)txtSalesTaxValue).Text == "" || ((Control)(object)txtSalesTaxValue).Text == ".") ? "0" : ((Control)(object)txtSalesTaxValue).Text) + decimal.Parse((((Control)(object)txtSalesTax).Text == "" || ((Control)(object)txtSalesTax).Text == ".") ? "0" : ((Control)(object)txtSalesTax).Text) - decimal.Parse((((Control)(object)txtCommercialTax).Text == "" || ((Control)(object)txtCommercialTax).Text == ".") ? "0" : ((Control)(object)txtCommercialTax).Text) - decimal.Parse((((Control)(object)txtConstractionInsurense).Text == "" || ((Control)(object)txtConstractionInsurense).Text == ".") ? "0" : ((Control)(object)txtConstractionInsurense).Text) - decimal.Parse((((Control)(object)txtWorkerInsurense).Text == "" || ((Control)(object)txtWorkerInsurense).Text == ".") ? "0" : ((Control)(object)txtWorkerInsurense).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	private void ReCalculateRows()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		decimal num = default(decimal);
		decimal num2 = default(decimal);
		decimal num3 = default(decimal);
		decimal num4 = default(decimal);
		decimal num5 = default(decimal);
		decimal num6 = default(decimal);
		decimal num7 = default(decimal);
		decimal num8 = default(decimal);
		decimal num9 = default(decimal);
		decimal num10 = default(decimal);
		decimal num11 = default(decimal);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num = (num2 = (num3 = (num4 = (num5 = (num6 = (num7 = (num8 = (num9 = (num10 = default(decimal))))))))));
			if (!((UltraGridBase)ULGData).Rows[i].Cells["SupplyPercentage"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).Rows[i].Cells["SupplyPercentage"].Value.Equals("") && !((UltraGridBase)ULGData).Rows[i].Cells["SupplyPercentage"].Value.Equals("."))
			{
				num = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["SupplyPercentage"].Value) / 100m;
			}
			if (!((UltraGridBase)ULGData).Rows[i].Cells["ErectionPercentage"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).Rows[i].Cells["ErectionPercentage"].Value.Equals("") && !((UltraGridBase)ULGData).Rows[i].Cells["ErectionPercentage"].Value.Equals("."))
			{
				num2 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["ErectionPercentage"].Value) / 100m;
			}
			if (!((UltraGridBase)ULGData).Rows[i].Cells["TestPercentage"].Value.Equals(DBNull.Value) && !((UltraGridBase)ULGData).Rows[i].Cells["TestPercentage"].Value.Equals("") && !((UltraGridBase)ULGData).Rows[i].Cells["TestPercentage"].Value.Equals("."))
			{
				num3 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["TestPercentage"].Value) / 100m;
			}
			if (!((UltraGridBase)ULGData).Rows[i].Cells["CurrentSupply"].Value.Equals("") && !((UltraGridBase)ULGData).Rows[i].Cells["CurrentSupply"].Value.Equals("."))
			{
				num4 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["CurrentSupply"].Value);
			}
			if (!((UltraGridBase)ULGData).Rows[i].Cells["CurrentErection"].Value.Equals("") && !((UltraGridBase)ULGData).Rows[i].Cells["CurrentErection"].Value.Equals("."))
			{
				num5 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["CurrentErection"].Value);
			}
			if (!((UltraGridBase)ULGData).Rows[i].Cells["CurrentTest"].Value.Equals("") && !((UltraGridBase)ULGData).Rows[i].Cells["CurrentTest"].Value.Equals("."))
			{
				num6 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["CurrentTest"].Value);
			}
			((UltraGridBase)ULGData).Rows[i].Cells["TotalSupply"].Value = (num7 = num4 + Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["PrevSupply"].Value));
			((UltraGridBase)ULGData).Rows[i].Cells["TotalErection"].Value = (num8 = num5 + Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["PrevErection"].Value));
			((UltraGridBase)ULGData).Rows[i].Cells["TotalTest"].Value = (num9 = num6 + Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["PrevTest"].Value));
			((UltraGridBase)ULGData).Rows[i].Cells["TotalAmount"].Value = (num10 = (num7 * num + num8 * num2 + num9 * num3) * Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["UnitPrice"].Value));
			((UltraGridBase)ULGData).Rows[i].Cells["ReqAmount"].Value = (num11 = num10 - Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["PrevReqAmount"].Value));
		}
		CalculateGross();
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void txtSalesTax_ValueChanged(object sender, EventArgs e)
	{
		CalculateTotals();
	}

	private void txtCommercialTax_ValueChanged(object sender, EventArgs e)
	{
		CalculateTotals();
	}

	private void btnContractSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CnsContracts();
		if (num != 0)
		{
			((TextEditorControlBase)cboContracts).Value = num;
		}
	}

	private void CalculateSalesTax()
	{
		if (cboSalesTax.SelectedIndex > -1)
		{
			((Control)(object)txtSalesTaxValue).Text = (decimal.Parse(dtTaxes.Select(" TaxID= " + ((TextEditorControlBase)cboSalesTax).Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse((((Control)(object)txtTotalPrice).Text == "" || ((Control)(object)txtTotalPrice).Text == ".") ? "0" : ((Control)(object)txtTotalPrice).Text)).ToString(GlobalVariables.txtDecimalFormate);
		}
		else
		{
			((Control)(object)txtSalesTaxValue).Text = "0";
		}
	}

	private void cboSalesTax_ValueChanged(object sender, EventArgs e)
	{
		CalculateSalesTax();
	}

	private void btnJV_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating && drMaster["JVID"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["JVID"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void btnJV2_Click(object sender, EventArgs e)
	{
		if (drMaster != null && !Adding && !Updating && drMaster["JVID2"] != DBNull.Value)
		{
			frmJV frmJV2 = new frmJV(int.Parse(drMaster["JVID2"].ToString()));
			frmJV2.Size = new Size(base.Width, base.Height);
			frmJV2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmJV2.lblTitle).Text = (GlobalVariables.IsArabic ? "قيود اليومية" : "JV");
			frmJV2.ShowDialog();
		}
	}

	private void dtpDate_ValueChanged(object sender, EventArgs e)
	{
		if (cboContracts.SelectedIndex > -1)
		{
			dtDetails = ContractsDetails.SelectforContractsDues(((TextEditorControlBase)cboContracts).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			CalculateGross();
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
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Expected O, but got Unknown
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Expected O, but got Unknown
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Expected O, but got Unknown
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Expected O, but got Unknown
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CnsProjects.Transactions.frmContractsDues));
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
		this.lblContracts = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.cboContracts = new UltraComboEditor();
		this.ultraLabel12 = new UltraLabel();
		this.ultraLabel11 = new UltraLabel();
		this.ultraLabel15 = new UltraLabel();
		this.ultraLabel14 = new UltraLabel();
		this.ultraLabel10 = new UltraLabel();
		this.ultraLabel4 = new UltraLabel();
		this.ultraLabel13 = new UltraLabel();
		this.txtTestPercentage = new UltraTextEditor();
		this.txtErectionPercentage = new UltraTextEditor();
		this.txtSupplyPercentage = new UltraTextEditor();
		this.txtDownPaymentPercentage = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.txtTotalPrice = new UltraTextEditor();
		this.ultraLabel8 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.txtWorkerInsurensePercentage = new UltraTextEditor();
		this.ultraLabel7 = new UltraLabel();
		this.txtConstractionInsurensePercentage = new UltraTextEditor();
		this.ultraLabel5 = new UltraLabel();
		this.txtSalesTax = new UltraTextEditor();
		this.ultraLabel9 = new UltraLabel();
		this.txtCommercialTax = new UltraTextEditor();
		this.ultraLabel16 = new UltraLabel();
		this.txtConstractionInsurense = new UltraTextEditor();
		this.ultraLabel17 = new UltraLabel();
		this.txtWorkerInsurense = new UltraTextEditor();
		this.ultraLabel18 = new UltraLabel();
		this.txtDownPayment = new UltraTextEditor();
		this.ultraLabel19 = new UltraLabel();
		this.txtNetPrice = new UltraTextEditor();
		this.ultraLabel20 = new UltraLabel();
		this.btnContractSearch = new UltraButton();
		this.btnJV2 = new UltraButton();
		this.btnJV = new UltraButton();
		this.lblSalesTax = new UltraLabel();
		this.txtSalesTaxValue = new UltraTextEditor();
		this.cboSalesTax = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboContracts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTestPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtErectionPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplyPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPaymentPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurensePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurensePercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurense).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurense).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPayment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesTaxValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesTax).BeginInit();
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		this.lblContracts.AutoEllipsis = false;
		resources.ApplyResources(this.lblContracts, "lblContracts");
		((System.Windows.Forms.Control)(object)this.lblContracts).Name = "lblContracts";
		((ControlBase)this.lblContracts).WrapText = false;
		this.lblNotes.AutoEllipsis = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.lblDate.AutoEllipsis = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.ValueChanged += new System.EventHandler(dtpDate_ValueChanged);
		((TextEditorControlBase)this.cboContracts).AlwaysInEditMode = true;
		this.cboContracts.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboContracts, "cboContracts");
		((System.Windows.Forms.Control)(object)this.cboContracts).Name = "cboContracts";
		((TextEditorControlBase)this.cboContracts).ValueChanged += new System.EventHandler(cboContracts_ValueChanged);
		this.ultraLabel12.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel12, "ultraLabel12");
		((System.Windows.Forms.Control)(object)this.ultraLabel12).Name = "ultraLabel12";
		((ControlBase)this.ultraLabel12).WrapText = false;
		this.ultraLabel11.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		this.ultraLabel15.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		this.ultraLabel14.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		this.ultraLabel10.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel10, "ultraLabel10");
		((System.Windows.Forms.Control)(object)this.ultraLabel10).Name = "ultraLabel10";
		this.ultraLabel4.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		this.ultraLabel13.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel13, "ultraLabel13");
		((System.Windows.Forms.Control)(object)this.ultraLabel13).Name = "ultraLabel13";
		((ControlBase)this.ultraLabel13).WrapText = false;
		resources.ApplyResources(this.txtTestPercentage, "txtTestPercentage");
		((System.Windows.Forms.Control)(object)this.txtTestPercentage).Name = "txtTestPercentage";
		((TextEditorControlBase)this.txtTestPercentage).ValueChanged += new System.EventHandler(txtTestPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtTestPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtErectionPercentage, "txtErectionPercentage");
		((System.Windows.Forms.Control)(object)this.txtErectionPercentage).Name = "txtErectionPercentage";
		((TextEditorControlBase)this.txtErectionPercentage).ValueChanged += new System.EventHandler(txtErectionPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtErectionPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtSupplyPercentage, "txtSupplyPercentage");
		((System.Windows.Forms.Control)(object)this.txtSupplyPercentage).Name = "txtSupplyPercentage";
		((TextEditorControlBase)this.txtSupplyPercentage).ValueChanged += new System.EventHandler(txtSupplyPercentage_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSupplyPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.txtDownPaymentPercentage, "txtDownPaymentPercentage");
		((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage).Name = "txtDownPaymentPercentage";
		((EditorButtonControlBase)this.txtDownPaymentPercentage).ReadOnly = true;
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		((EditorButtonControlBase)this.cboSubAccount).ReadOnly = true;
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val9;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.txtTotalPrice, "txtTotalPrice");
		((System.Windows.Forms.Control)(object)this.txtTotalPrice).Name = "txtTotalPrice";
		((EditorButtonControlBase)this.txtTotalPrice).ReadOnly = true;
		this.ultraLabel8.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		this.ultraLabel6.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		resources.ApplyResources(this.txtWorkerInsurensePercentage, "txtWorkerInsurensePercentage");
		((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage).Name = "txtWorkerInsurensePercentage";
		((EditorButtonControlBase)this.txtWorkerInsurensePercentage).ReadOnly = true;
		this.ultraLabel7.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.txtConstractionInsurensePercentage, "txtConstractionInsurensePercentage");
		((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage).Name = "txtConstractionInsurensePercentage";
		((EditorButtonControlBase)this.txtConstractionInsurensePercentage).ReadOnly = true;
		this.ultraLabel5.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.txtSalesTax, "txtSalesTax");
		((System.Windows.Forms.Control)(object)this.txtSalesTax).Name = "txtSalesTax";
		((TextEditorControlBase)this.txtSalesTax).ValueChanged += new System.EventHandler(txtSalesTax_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSalesTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		this.ultraLabel9.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.txtCommercialTax, "txtCommercialTax");
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).Name = "txtCommercialTax";
		((TextEditorControlBase)this.txtCommercialTax).ValueChanged += new System.EventHandler(txtCommercialTax_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtCommercialTax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.txtConstractionInsurense, "txtConstractionInsurense");
		((System.Windows.Forms.Control)(object)this.txtConstractionInsurense).Name = "txtConstractionInsurense";
		((EditorButtonControlBase)this.txtConstractionInsurense).ReadOnly = true;
		resources.ApplyResources(this.ultraLabel17, "ultraLabel17");
		this.ultraLabel17.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel17).Name = "ultraLabel17";
		((ControlBase)this.ultraLabel17).WrapText = false;
		resources.ApplyResources(this.txtWorkerInsurense, "txtWorkerInsurense");
		((System.Windows.Forms.Control)(object)this.txtWorkerInsurense).Name = "txtWorkerInsurense";
		((EditorButtonControlBase)this.txtWorkerInsurense).ReadOnly = true;
		resources.ApplyResources(this.ultraLabel18, "ultraLabel18");
		this.ultraLabel18.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel18).Name = "ultraLabel18";
		((ControlBase)this.ultraLabel18).WrapText = false;
		resources.ApplyResources(this.txtDownPayment, "txtDownPayment");
		((System.Windows.Forms.Control)(object)this.txtDownPayment).Name = "txtDownPayment";
		((EditorButtonControlBase)this.txtDownPayment).ReadOnly = true;
		resources.ApplyResources(this.ultraLabel19, "ultraLabel19");
		this.ultraLabel19.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel19).Name = "ultraLabel19";
		((ControlBase)this.ultraLabel19).WrapText = false;
		resources.ApplyResources(this.txtNetPrice, "txtNetPrice");
		((System.Windows.Forms.Control)(object)this.txtNetPrice).Name = "txtNetPrice";
		((EditorButtonControlBase)this.txtNetPrice).ReadOnly = true;
		resources.ApplyResources(this.ultraLabel20, "ultraLabel20");
		this.ultraLabel20.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel20).Name = "ultraLabel20";
		((ControlBase)this.ultraLabel20).WrapText = false;
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnContractSearch).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.btnContractSearch, "btnContractSearch");
		((System.Windows.Forms.Control)(object)this.btnContractSearch).Name = "btnContractSearch";
		((System.Windows.Forms.Control)(object)this.btnContractSearch).Click += new System.EventHandler(btnContractSearch_Click);
		resources.ApplyResources(this.btnJV2, "btnJV2");
		((System.Windows.Forms.Control)(object)this.btnJV2).Name = "btnJV2";
		((System.Windows.Forms.Control)(object)this.btnJV2).Click += new System.EventHandler(btnJV2_Click);
		resources.ApplyResources(this.btnJV, "btnJV");
		((System.Windows.Forms.Control)(object)this.btnJV).Name = "btnJV";
		((System.Windows.Forms.Control)(object)this.btnJV).Click += new System.EventHandler(btnJV_Click);
		resources.ApplyResources(this.lblSalesTax, "lblSalesTax");
		this.lblSalesTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesTax).Name = "lblSalesTax";
		((ControlBase)this.lblSalesTax).WrapText = false;
		resources.ApplyResources(this.txtSalesTaxValue, "txtSalesTaxValue");
		((System.Windows.Forms.Control)(object)this.txtSalesTaxValue).Name = "txtSalesTaxValue";
		((EditorButtonControlBase)this.txtSalesTaxValue).ReadOnly = true;
		((TextEditorControlBase)this.txtSalesTaxValue).ValueChanged += new System.EventHandler(txtCommercialTax_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSalesTaxValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		((TextEditorControlBase)this.cboSalesTax).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboSalesTax, "cboSalesTax");
		this.cboSalesTax.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboSalesTax).Name = "cboSalesTax";
		((TextEditorControlBase)this.cboSalesTax).ValueChanged += new System.EventHandler(cboSalesTax_ValueChanged);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnJV);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnContractSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel20);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel19);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel18);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel17);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel12);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNetPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDownPayment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWorkerInsurense);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConstractionInsurense);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesTaxValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCommercialTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel10);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel13);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTestPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtErectionPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSupplyPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboContracts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContracts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmContractsDues";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContracts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboContracts, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSubAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDownPaymentPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSupplyPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtErectionPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTestPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel13, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel10, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel14, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel15, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel11, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCommercialTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesTaxValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConstractionInsurense, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkerInsurense, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDownPayment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNetPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel12, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel9, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel16, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel17, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel18, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel19, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel20, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConstractionInsurensePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWorkerInsurensePercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnContractSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnJV2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesTax, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboContracts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTestPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtErectionPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSupplyPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPaymentPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurensePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurensePercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCommercialTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConstractionInsurense).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWorkerInsurense).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDownPayment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNetPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesTaxValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesTax).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
