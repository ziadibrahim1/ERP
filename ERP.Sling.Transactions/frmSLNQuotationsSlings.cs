using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using BusinessLayer.Sling;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Sling.Transactions;

public class frmSLNQuotationsSlings : frmBase
{
	private DataTable dtWireTypes;

	private DataTable dtTerminationTypes;

	private DataTable dtShackleTypes;

	private DataTable dtHookTypes;

	private DataTable dtMasterLinkTypes;

	private DataTable dtMaterialsTypes;

	private DataTable dtItems;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtBatchs;

	private DataTable dtStores;

	private DataTable dtUnits;

	private DataTable dtWireTypesDetails;

	private DataTable dtTerminationsTh;

	private DataTable dtTerminationsFe;

	private DataTable dtTerminationsOpenSocket;

	private DataTable dtTerminationsClosedSocket;

	private DataTable dtTerminationsChainConnector;

	private DataTable dtShackleTypesDetails;

	private DataTable dtHooksTypesDetails;

	private DataTable dtMasterLinksDual;

	private DataTable dtMasterLinksQuad;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlUnits = new ValueList();

	private ValueList vlStores = new ValueList();

	private ValueList vlBatchs = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlMaterialsTypes = new ValueList();

	private bool UsingColors;

	private bool UsingSizes = false;

	private bool UsingBatchNoAndValidityPeriod = false;

	private DateTime VoucherDate = DateTime.Now;

	private int QuotationDetailID;

	private int QuotationID;

	public decimal TotalQty = default(decimal);

	public string SlingDesc = "";

	private bool NavMode = false;

	public bool Saved = false;

	private DataTable dtQuotationsDetailsSlings = new DataTable();

	private DataTable dtQuotationsDetailsSlingsMaterials = new DataTable();

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	private UltraLabel lblSlingLegsCount;

	private UltraNumericEditor UNSlingLegsCount;

	private UltraLabel lblWireType;

	private UltraComboEditor cboWireType;

	private UltraLabel lblWireLengthMt;

	private UltraTextEditor txtWireLengthMt;

	private UltraLabel lblWireLengthFt;

	private UltraTextEditor txtWireLengthFt;

	private UltraLabel lblFifthWireLengthMt;

	private UltraLabel lblFifthWireLengthFt;

	private UltraTextEditor txtFifthWireLengthMt;

	private UltraTextEditor txtFifthWireLengthFt;

	private UltraLabel lblDiameter;

	private UltraTextEditor txtDiameter;

	private UltraLabel lblFirstTerminationType;

	private UltraComboEditor cboFirstTerminationType;

	private UltraComboEditor cboSecondterminationType;

	private UltraLabel lblSecondTerminationType;

	private UltraComboEditor cboShackleType;

	private UltraLabel lblShackleType;

	private UltraComboEditor cboHookType;

	private UltraLabel lblHookType;

	private UltraComboEditor cboLinkType;

	private UltraLabel lblLinkType;

	private UltraLabel lblEyeSizeFirstTerminal;

	private UltraTextEditor txtEyeSizeFirstTerminal;

	private UltraLabel lblEyeSizeSecondTerminal;

	private UltraTextEditor txtEyeSizeSecondTerminal;

	public UltraButton btnClose;

	private UltraButton btnCreateStock;

	private UltraLabel lblTotalQty;

	private UltraTextEditor txtTotalQty;

	private UltraButton btnGetDescription;

	private UltraLabel ultraLabel1;

	private UltraFormattedTextEditor txtSlingDesc;

	public frmSLNQuotationsSlings()
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
		InitializeComponent();
	}

	public frmSLNQuotationsSlings(int quotationid, int quotationdetailid, decimal totalqty, string Desc, bool navmode, DateTime voucherdate, DataTable dtquotationdetailsling, DataTable dtquotationdetailslingmaterial)
		: this()
	{
		QuotationID = quotationid;
		QuotationDetailID = quotationdetailid;
		NavMode = navmode;
		VoucherDate = voucherdate;
		dtQuotationsDetailsSlings = dtquotationdetailsling;
		dtQuotationsDetailsSlingsMaterials = dtquotationdetailslingmaterial;
		TotalQty = totalqty;
		((Control)(object)txtSlingDesc).Text = (SlingDesc = Desc);
	}

	public override void PrepareData()
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
		UsingBatchNoAndValidityPeriod = GlobalFunctions.GetOption("UsingBatchNoAndValidityPeriod");
		if (UsingBatchNoAndValidityPeriod)
		{
			dtBatchs = ItemsBatches.FillCombo(IsFromServer: false);
			vlBatchs.ValueListItems.Clear();
			for (int k = 0; k < dtBatchs.Rows.Count; k++)
			{
				vlBatchs.ValueListItems.Add(dtBatchs.Rows[k]["BatchID"], dtBatchs.Rows[k]["BatchName"].ToString());
			}
		}
		dtWireTypes = WireTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboWireType, dtWireTypes, "WireTypeID", "WireTypeName");
		dtTerminationTypes = TerminationsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboFirstTerminationType, dtTerminationTypes, "TerminationTypeID", "TerminationTypeName");
		GlobalFunctions.FillCombo(cboSecondterminationType, dtTerminationTypes, "TerminationTypeID", "TerminationTypeName");
		dtShackleTypes = ShackleTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboShackleType, dtShackleTypes, "ShackleTypeID", "ShackleTypeName");
		dtHookTypes = HooksTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboHookType, dtHookTypes, "HookTypeID", "HookTypeName");
		dtMasterLinkTypes = MasterLinksTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboLinkType, dtMasterLinkTypes, "MasterLinkTypeID", "MasterLinkTypeName");
		dtMaterialsTypes = MaterialsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlMaterialsTypes.ValueListItems.Clear();
		for (int l = 0; l < dtMaterialsTypes.Rows.Count; l++)
		{
			vlMaterialsTypes.ValueListItems.Add(dtMaterialsTypes.Rows[l]["MaterialTypeID"], dtMaterialsTypes.Rows[l]["MaterialTypeName"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlStores.ValueListItems.Clear();
		for (int m = 0; m < dtStores.Rows.Count; m++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[m]["StoreID"], dtStores.Rows[m]["StoreName"].ToString());
		}
		dtItems = Items.FillComboSling("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
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
		DisplayData();
		SetControls();
		InitGrid();
		((TextEditorControlBase)cboLinkType).Value = 1;
	}

	public void DisplayData()
	{
		((Control)(object)txtTotalQty).Text = TotalQty.ToString();
		if (dtQuotationsDetailsSlings.Select(" QuotationDetailID= " + QuotationDetailID).Length != 0)
		{
			DataRow dataRow = dtQuotationsDetailsSlings.Select(" QuotationDetailID= " + QuotationDetailID)[0];
			((UltraNumericEditorBase)UNSlingLegsCount).ValueChanged -= UNSlingLegsCount_ValueChanged;
			UNSlingLegsCount.Value = dataRow["SlingLegsCount"];
			((UltraNumericEditorBase)UNSlingLegsCount).ValueChanged += UNSlingLegsCount_ValueChanged;
			((TextEditorControlBase)cboWireType).ValueChanged -= cboWireType_ValueChanged;
			((TextEditorControlBase)cboWireType).Value = dataRow["WireTypeID"];
			if (cboWireType.SelectedIndex > -1 && (dtWireTypesDetails == null || dtWireTypesDetails.Rows.Count == 0 || !dtWireTypesDetails.Rows[0]["WireTypeID"].ToString().Equals(((TextEditorControlBase)cboWireType).Value.ToString())))
			{
				dtWireTypesDetails = WireTypesDetails.SelectByWireTypeID(((TextEditorControlBase)cboWireType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			((TextEditorControlBase)cboWireType).ValueChanged += cboWireType_ValueChanged;
			((TextEditorControlBase)txtWireLengthMt).ValueChanged -= txtWireLengthMt_ValueChanged;
			((Control)(object)txtWireLengthMt).Text = dataRow["WireLengthMt"].ToString();
			((TextEditorControlBase)txtWireLengthMt).ValueChanged += txtWireLengthMt_ValueChanged;
			((TextEditorControlBase)txtWireLengthFt).ValueChanged -= txtWireLengthFt_ValueChanged;
			((Control)(object)txtWireLengthFt).Text = dataRow["WireLengthFt"].ToString();
			((TextEditorControlBase)txtWireLengthFt).ValueChanged += txtWireLengthFt_ValueChanged;
			((TextEditorControlBase)txtFifthWireLengthMt).ValueChanged -= txtFifthWireLengthMt_ValueChanged;
			((Control)(object)txtFifthWireLengthMt).Text = dataRow["FifthWireLengthMt"].ToString();
			((TextEditorControlBase)txtFifthWireLengthMt).ValueChanged += txtFifthWireLengthMt_ValueChanged;
			((TextEditorControlBase)txtFifthWireLengthFt).ValueChanged -= txtFifthWireLengthFt_ValueChanged;
			((Control)(object)txtFifthWireLengthFt).Text = dataRow["FifthWireLengthFt"].ToString();
			((TextEditorControlBase)txtFifthWireLengthFt).ValueChanged += txtFifthWireLengthFt_ValueChanged;
			((TextEditorControlBase)txtDiameter).ValueChanged -= txtDiameter_ValueChanged;
			((Control)(object)txtDiameter).Text = dataRow["Diameter"].ToString();
			((TextEditorControlBase)txtDiameter).ValueChanged += txtDiameter_ValueChanged;
			((TextEditorControlBase)cboFirstTerminationType).Value = dataRow["FirstTerminationTypeID"];
			((TextEditorControlBase)cboSecondterminationType).Value = dataRow["SecondTerminationTypeID"];
			((TextEditorControlBase)cboShackleType).Value = dataRow["ShackleTypeID"];
			((TextEditorControlBase)cboHookType).Value = dataRow["HookTypeID"];
			((TextEditorControlBase)cboLinkType).Value = dataRow["MasterLinkTypeID"];
			((Control)(object)txtEyeSizeFirstTerminal).Text = dataRow["EyeSizeFirstTerminal"].ToString();
			((Control)(object)txtEyeSizeSecondTerminal).Text = dataRow["EyeSizeSecondTerminal"].ToString();
		}
		DataView dataView = new DataView(dtQuotationsDetailsSlingsMaterials);
		dataView.RowFilter = " QuotationDetailID= " + QuotationDetailID;
		((UltraGridBase)ULGData).DataSource = dataView;
	}

	public void SetControls()
	{
		((EditorButtonControlBase)UNSlingLegsCount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboWireType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtWireLengthMt).ReadOnly = NavMode;
		((EditorButtonControlBase)txtWireLengthFt).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFifthWireLengthMt).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFifthWireLengthFt).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiameter).ReadOnly = NavMode;
		((EditorButtonControlBase)cboFirstTerminationType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboSecondterminationType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboShackleType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboHookType).ReadOnly = NavMode;
		((EditorButtonControlBase)cboLinkType).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEyeSizeFirstTerminal).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEyeSizeSecondTerminal).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTotalQty).ReadOnly = NavMode;
		((Control)(object)btnCreateStock).Visible = !NavMode;
		((Control)(object)btnSave).Visible = !NavMode;
		if (NavMode)
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			switch (((UltraGridBase)ULGData).Rows[i].Cells["MaterialTypeID"].Value.ToString())
			{
			case "1":
				WireDetailsFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "2":
				TerminationsThFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "3":
				TerminationsFeFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "4":
				TerminationsOpenSocketFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "5":
				TerminationsClosedSocketFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "6":
				TerminationsChainConnectorFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "7":
				ShackleTypesDetailsFillCombo(((UltraGridBase)ULGData).Rows[i], "-1", "-1");
				break;
			case "8":
				HooksTypesDetailsFillCombo(((UltraGridBase)ULGData).Rows[i], "-1", "-1");
				break;
			case "9":
				MasterLinksDualFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			case "10":
				MasterLinksQuadFillCombo(((UltraGridBase)ULGData).Rows[i], "-1");
				break;
			}
		}
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)4;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDetailSlingMaterialD"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuotationDetailID"].DefaultCellValue = QuotationDetailID;
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
		if (UsingBatchNoAndValidityPeriod)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = false;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].ValueList = (IValueList)(object)vlBatchs;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Header).Caption = (GlobalVariables.IsArabic ? "سريل" : "Batch No");
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchID"].Hidden = true;
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "BarCode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialTypeID"].ValueList = (IValueList)(object)vlMaterialsTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Count"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Count"].Header).Caption = (GlobalVariables.IsArabic ? "العدد" : "Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Count"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Count"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireLength"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireLength"].Header).Caption = (GlobalVariables.IsArabic ? "الطول" : "Length");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireLength"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireLength"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireAllowance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireAllowance"].Header).Caption = (GlobalVariables.IsArabic ? "التقطيع" : "Allowance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireAllowance"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WireAllowance"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Format = GlobalVariables.QtyDecimals;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Header).Caption = (GlobalVariables.IsArabic ? "الوحدة" : "Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitID"].ValueList = (IValueList)(object)vlUnits;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن" : "Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].Header).Caption = (GlobalVariables.IsArabic ? "تكلفة الوحدة" : "Unit Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitCostPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].Header).Caption = (GlobalVariables.IsArabic ? "اجمالي التكلفة" : "Total Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalCostPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
	}

	public bool ValidateData(bool ValidateDetails)
	{
		if (((Control)(object)txtDiameter).Text == "" || decimal.Parse(((Control)(object)txtDiameter).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show("Please Enter The Diameter", "Please Enter The Diameter");
			return false;
		}
		if (cboWireType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("Please Select Wire Type", "Please Select Wire Type");
			return false;
		}
		if (((Control)(object)txtWireLengthMt).Text == "" || decimal.Parse(((Control)(object)txtWireLengthMt).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show("Please Enter Wire Length", "Please Enter Wire Length");
			return false;
		}
		if (UNSlingLegsCount.Value.ToString() == "5" && (((Control)(object)txtFifthWireLengthMt).Text == "" || decimal.Parse(((Control)(object)txtFifthWireLengthMt).Text) <= 0m))
		{
			GlobalVariables.InformationMB.Show("Please Enter Fifth Wire Length", "Please Enter Fifth Wire Length");
			return false;
		}
		if (cboFirstTerminationType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("Please Select First Termination", "Please Select First Termination");
			return false;
		}
		if (cboSecondterminationType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("Please Select Second Termination", "Please Select Second Termination");
			return false;
		}
		if ((cboFirstTerminationType.SelectedIndex == 0 || cboFirstTerminationType.SelectedIndex == 1) && (((Control)(object)txtEyeSizeFirstTerminal).Text == "" || decimal.Parse(((Control)(object)txtEyeSizeFirstTerminal).Text) == 0m))
		{
			GlobalVariables.InformationMB.Show("Please Enter Eye Size For The First Terminal", "Please Enter Eye Size For The First Terminal");
			return false;
		}
		if ((cboSecondterminationType.SelectedIndex == 0 || cboSecondterminationType.SelectedIndex == 1) && (((Control)(object)txtEyeSizeSecondTerminal).Text == "" || decimal.Parse(((Control)(object)txtEyeSizeSecondTerminal).Text) == 0m))
		{
			GlobalVariables.InformationMB.Show("Please Enter Eye Size For The Second Terminal", "Please Enter Eye Size For The Second Terminal");
			return false;
		}
		if ((cboFirstTerminationType.SelectedIndex == 0 || cboFirstTerminationType.SelectedIndex == 1) && dtWireTypesDetails.Select(" EyeSize = " + ((Control)(object)txtEyeSizeFirstTerminal).Text).Length == 0)
		{
			GlobalVariables.InformationMB.Show("Allowances For The First Terminal Eyes Does Not Exist", "Allowances For The First Terminal Eyes Does Not Exist");
			return false;
		}
		if ((cboSecondterminationType.SelectedIndex == 0 || cboSecondterminationType.SelectedIndex == 1) && dtWireTypesDetails.Select(" EyeSize = " + ((Control)(object)txtEyeSizeSecondTerminal).Text).Length == 0)
		{
			GlobalVariables.InformationMB.Show("Allowances For The Second Terminal Eyes Does Not Exist", "Allowances For The Second Terminal Eyes Does Not Exist");
			return false;
		}
		if (ValidateDetails)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["MaterialTypeID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال نوع الصنف  ", "Please Enter Material Type");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["MaterialTypeID"];
					((UltraGridBase)ULGData).Rows[i].Cells["MaterialTypeID"].DroppedDown = true;
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
					return false;
				}
				if (UsingColors && (((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value == DBNull.Value || dtColors.Select("ColorID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].Value.ToString()).Length == 0))
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار اللون  ", "Please Select Color");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ColorID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ColorID"].DroppedDown = true;
					return false;
				}
				if (UsingSizes && (((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value == DBNull.Value || dtSizes.Select("ItemSizeID=" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].Value.ToString()).Length == 0))
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار المقاس  ", "Please Select Size");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"];
					((UltraGridBase)ULGData).Rows[i].Cells["ItemSizeID"].DroppedDown = true;
					return false;
				}
				if (UsingBatchNoAndValidityPeriod)
				{
					if (bool.Parse(dtItems.Select("ItemID =" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["EnforceBatchNo"].ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"].Value == DBNull.Value)
					{
						GlobalVariables.InformationMB.Show("برجاء اختيار سريل", "Please choose Batch No");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["BatchID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
					continue;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["UnitID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار الوحدة  ", "Please Select Unit Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال الكمية  ", "Please Enter Quantity ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال سعر تكلفة الوحدة  ", "Please Enter Unit Cost Price ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["UnitCostPrice"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["Count"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Count"].Value.ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال العدد  ", "Please Enter Count ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Count"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["WireLength"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال طول السلك ", "Please Enter Wire Length ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["WireLength"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["WireAllowance"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء ادخال سماحيات التقطيع ", "Please Enter Wire Allowances ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["WireAllowance"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["StoreID"].Value == DBNull.Value && !bool.Parse(dtItems.Select(" ItemID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString())[0]["IsService"].ToString()))
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار المخزن  ", "Please Select Store Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["StoreID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
			}
		}
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData(ValidateDetails: true))
		{
			dtQuotationsDetailsSlings.AcceptChanges();
			dtQuotationsDetailsSlingsMaterials.AcceptChanges();
			if (dtQuotationsDetailsSlings.Rows.Count > 0 && dtQuotationsDetailsSlings.Select(" QuotationDetailID= " + QuotationDetailID).Length != 0)
			{
				DataRow dataRow = dtQuotationsDetailsSlings.Select(" QuotationDetailID= " + QuotationDetailID)[0];
				dataRow["SlingLegsCount"] = ((UNSlingLegsCount.Value == null) ? DBNull.Value : UNSlingLegsCount.Value);
				dataRow["WireTypeID"] = ((((TextEditorControlBase)cboWireType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboWireType).Value);
				dataRow["WireLengthMt"] = ((((Control)(object)txtWireLengthMt).Text == "" || ((Control)(object)txtWireLengthMt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtWireLengthMt).Text));
				dataRow["WireLengthFt"] = ((((Control)(object)txtWireLengthFt).Text == "" || ((Control)(object)txtWireLengthFt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtWireLengthFt).Text));
				dataRow["FifthWireLengthMt"] = ((((Control)(object)txtFifthWireLengthMt).Text == "" || ((Control)(object)txtFifthWireLengthMt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtFifthWireLengthMt).Text));
				dataRow["FifthWireLengthFt"] = ((((Control)(object)txtFifthWireLengthFt).Text == "" || ((Control)(object)txtFifthWireLengthFt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtFifthWireLengthFt).Text));
				dataRow["Diameter"] = ((((Control)(object)txtDiameter).Text == "" || ((Control)(object)txtDiameter).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtDiameter).Text));
				dataRow["FirstTerminationTypeID"] = ((((TextEditorControlBase)cboFirstTerminationType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboFirstTerminationType).Value);
				dataRow["SecondTerminationTypeID"] = ((((TextEditorControlBase)cboSecondterminationType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboSecondterminationType).Value);
				dataRow["ShackleTypeID"] = ((((TextEditorControlBase)cboShackleType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboShackleType).Value);
				dataRow["HookTypeID"] = ((((TextEditorControlBase)cboHookType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboHookType).Value);
				dataRow["MasterLinkTypeID"] = ((((TextEditorControlBase)cboLinkType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboLinkType).Value);
				dataRow["EyeSizeFirstTerminal"] = ((((Control)(object)txtEyeSizeFirstTerminal).Text == "" || ((Control)(object)txtEyeSizeFirstTerminal).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtEyeSizeFirstTerminal).Text));
				dataRow["EyeSizeSecondTerminal"] = ((((Control)(object)txtEyeSizeSecondTerminal).Text == "" || ((Control)(object)txtEyeSizeSecondTerminal).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtEyeSizeSecondTerminal).Text));
			}
			else
			{
				DataRow dataRow2 = dtQuotationsDetailsSlings.NewRow();
				dataRow2["QuotationDetailSlingID"] = -1;
				dataRow2["QuotationDetailID"] = QuotationDetailID;
				dataRow2["QuotationID"] = QuotationID;
				dataRow2["SlingLegsCount"] = ((UNSlingLegsCount.Value == null) ? DBNull.Value : UNSlingLegsCount.Value);
				dataRow2["WireTypeID"] = ((((TextEditorControlBase)cboWireType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboWireType).Value);
				dataRow2["WireLengthMt"] = ((((Control)(object)txtWireLengthMt).Text == "" || ((Control)(object)txtWireLengthMt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtWireLengthMt).Text));
				dataRow2["WireLengthFt"] = ((((Control)(object)txtWireLengthFt).Text == "" || ((Control)(object)txtWireLengthFt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtWireLengthFt).Text));
				dataRow2["FifthWireLengthMt"] = ((((Control)(object)txtFifthWireLengthMt).Text == "" || ((Control)(object)txtFifthWireLengthMt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtFifthWireLengthMt).Text));
				dataRow2["FifthWireLengthFt"] = ((((Control)(object)txtFifthWireLengthFt).Text == "" || ((Control)(object)txtFifthWireLengthFt).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtFifthWireLengthFt).Text));
				dataRow2["Diameter"] = ((((Control)(object)txtDiameter).Text == "" || ((Control)(object)txtDiameter).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtDiameter).Text));
				dataRow2["FirstTerminationTypeID"] = ((((TextEditorControlBase)cboFirstTerminationType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboFirstTerminationType).Value);
				dataRow2["SecondTerminationTypeID"] = ((((TextEditorControlBase)cboSecondterminationType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboSecondterminationType).Value);
				dataRow2["ShackleTypeID"] = ((((TextEditorControlBase)cboShackleType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboShackleType).Value);
				dataRow2["HookTypeID"] = ((((TextEditorControlBase)cboHookType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboHookType).Value);
				dataRow2["MasterLinkTypeID"] = ((((TextEditorControlBase)cboLinkType).Value == null) ? DBNull.Value : ((TextEditorControlBase)cboLinkType).Value);
				dataRow2["EyeSizeFirstTerminal"] = ((((Control)(object)txtEyeSizeFirstTerminal).Text == "" || ((Control)(object)txtEyeSizeFirstTerminal).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtEyeSizeFirstTerminal).Text));
				dataRow2["EyeSizeSecondTerminal"] = ((((Control)(object)txtEyeSizeSecondTerminal).Text == "" || ((Control)(object)txtEyeSizeSecondTerminal).Text == ".") ? 0m : decimal.Parse(((Control)(object)txtEyeSizeSecondTerminal).Text));
				dtQuotationsDetailsSlings.Rows.Add(dataRow2);
			}
			dtQuotationsDetailsSlings.AcceptChanges();
			dtQuotationsDetailsSlingsMaterials.AcceptChanges();
			TotalQty = decimal.Parse(((Control)(object)txtTotalQty).Text);
			Saved = true;
			Close();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (dtQuotationsDetailsSlingsMaterials.Select("QuotationDetailID = " + QuotationDetailID).Length != 0 && dtQuotationsDetailsSlings.Select("QuotationDetailID = " + QuotationDetailID).Length == 0)
		{
			DataRow[] array = dtQuotationsDetailsSlingsMaterials.Select("QuotationDetailID = " + QuotationDetailID);
			foreach (DataRow dataRow in array)
			{
				dataRow.Delete();
			}
			dtQuotationsDetailsSlingsMaterials.AcceptChanges();
		}
		else
		{
			Close();
		}
	}

	private void textBoxInteger_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void textBoxNumber_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void UNSlingLegsCount_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboLinkType).Value = ((int.Parse(UNSlingLegsCount.Value.ToString()) < 3) ? 1 : 2);
	}

	private void txtWireLengthMt_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtWireLengthMt).ValueChanged -= txtWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtWireLengthFt).ValueChanged -= txtWireLengthFt_ValueChanged;
		if (((Control)(object)txtWireLengthMt).Text != "")
		{
			((Control)(object)txtWireLengthFt).Text = (double.Parse(((Control)(object)txtWireLengthMt).Text) * 3.28).ToString();
		}
		else
		{
			((Control)(object)txtWireLengthMt).Text = "0";
			((Control)(object)txtWireLengthFt).Text = "0";
		}
		((TextEditorControlBase)txtWireLengthMt).ValueChanged += txtWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtWireLengthFt).ValueChanged += txtWireLengthFt_ValueChanged;
	}

	private void txtWireLengthFt_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtWireLengthMt).ValueChanged -= txtWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtWireLengthFt).ValueChanged -= txtWireLengthFt_ValueChanged;
		if (((Control)(object)txtWireLengthFt).Text != "")
		{
			((Control)(object)txtWireLengthMt).Text = (double.Parse(((Control)(object)txtWireLengthFt).Text) / 3.28).ToString();
		}
		else
		{
			((Control)(object)txtWireLengthMt).Text = "0";
			((Control)(object)txtWireLengthFt).Text = "0";
		}
		((TextEditorControlBase)txtWireLengthMt).ValueChanged += txtWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtWireLengthFt).ValueChanged += txtWireLengthFt_ValueChanged;
	}

	private void txtFifthWireLengthMt_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtFifthWireLengthMt).ValueChanged -= txtFifthWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtFifthWireLengthFt).ValueChanged -= txtFifthWireLengthFt_ValueChanged;
		if (((Control)(object)txtFifthWireLengthMt).Text != "")
		{
			((Control)(object)txtFifthWireLengthFt).Text = (double.Parse(((Control)(object)txtFifthWireLengthMt).Text) * 3.28).ToString();
		}
		else
		{
			((Control)(object)txtFifthWireLengthMt).Text = "0";
			((Control)(object)txtFifthWireLengthFt).Text = "0";
		}
		((TextEditorControlBase)txtFifthWireLengthMt).ValueChanged += txtFifthWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtFifthWireLengthFt).ValueChanged += txtFifthWireLengthFt_ValueChanged;
	}

	private void txtFifthWireLengthFt_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtFifthWireLengthMt).ValueChanged -= txtFifthWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtFifthWireLengthFt).ValueChanged -= txtFifthWireLengthFt_ValueChanged;
		if (((Control)(object)txtFifthWireLengthFt).Text != "")
		{
			((Control)(object)txtFifthWireLengthMt).Text = (double.Parse(((Control)(object)txtFifthWireLengthFt).Text) / 3.28).ToString();
		}
		else
		{
			((Control)(object)txtFifthWireLengthMt).Text = "0";
			((Control)(object)txtFifthWireLengthFt).Text = "0";
		}
		((TextEditorControlBase)txtFifthWireLengthMt).ValueChanged += txtFifthWireLengthMt_ValueChanged;
		((TextEditorControlBase)txtFifthWireLengthFt).ValueChanged += txtFifthWireLengthFt_ValueChanged;
	}

	private void txtDiameter_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtDiameter).ValueChanged -= txtDiameter_ValueChanged;
		if (cboWireType.SelectedIndex > -1 && ((Control)(object)txtDiameter).Text != "")
		{
			if (dtWireTypesDetails == null || dtWireTypesDetails.Rows.Count == 0 || !dtWireTypesDetails.Rows[0]["WireTypeID"].ToString().Equals(((TextEditorControlBase)cboWireType).Value.ToString()))
			{
				dtWireTypesDetails = WireTypesDetails.SelectByWireTypeID(((TextEditorControlBase)cboWireType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				DataRow dataRow = dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0];
				UltraTextEditor obj = txtEyeSizeFirstTerminal;
				string text = (((Control)(object)txtEyeSizeSecondTerminal).Text = dataRow["EyeSize"].ToString());
				((Control)(object)obj).Text = text;
			}
		}
		else
		{
			((Control)(object)txtEyeSizeFirstTerminal).Text = "0";
			((Control)(object)txtEyeSizeSecondTerminal).Text = "0";
		}
		((TextEditorControlBase)txtDiameter).ValueChanged += txtDiameter_ValueChanged;
	}

	private void cboWireType_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboWireType).ValueChanged -= cboWireType_ValueChanged;
		if (cboWireType.SelectedIndex > -1 && ((Control)(object)txtDiameter).Text != "")
		{
			if (dtWireTypesDetails == null || dtWireTypesDetails.Rows.Count == 0 || !dtWireTypesDetails.Rows[0]["WireTypeID"].ToString().Equals(((TextEditorControlBase)cboWireType).Value.ToString()))
			{
				dtWireTypesDetails = WireTypesDetails.SelectByWireTypeID(((TextEditorControlBase)cboWireType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				DataRow dataRow = dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0];
				UltraTextEditor obj = txtEyeSizeFirstTerminal;
				string text = (((Control)(object)txtEyeSizeSecondTerminal).Text = dataRow["EyeSize"].ToString());
				((Control)(object)obj).Text = text;
			}
		}
		else
		{
			((Control)(object)txtEyeSizeFirstTerminal).Text = "0";
			((Control)(object)txtEyeSizeSecondTerminal).Text = "0";
		}
		((TextEditorControlBase)cboWireType).ValueChanged += cboWireType_ValueChanged;
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

	private void txtSlingDesc_MouseClick(object sender, MouseEventArgs e)
	{
	}

	private void txtSlingDesc_KeyPress(object sender, KeyPressEventArgs e)
	{
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

	private void WireDetailsFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		if (cboWireType.SelectedIndex > -1)
		{
			DataTable dataTable = new DataTable();
			dataTable = WireTypesDetails.FillCombo(((TextEditorControlBase)cboWireType).Value.ToString(), Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			ValueList val = new ValueList();
			ValueList val2 = new ValueList();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				val.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				val2.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
			GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
			GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
		}
	}

	private void TerminationsThFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (dtTerminationsTh == null || dtTerminationsTh.Rows.Count == 0)
		{
			dtTerminationsTh = TerminationsTh.FillCombo(Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dtTerminationsTh.Rows.Count; i++)
		{
			val.ValueListItems.Add(dtTerminationsTh.Rows[i]["ItemID"], dtTerminationsTh.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dtTerminationsTh.Rows[i]["ItemID"], dtTerminationsTh.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void TerminationsFeFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (dtTerminationsFe == null || dtTerminationsFe.Rows.Count == 0)
		{
			dtTerminationsFe = TerminationsFe.FillCombo(Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dtTerminationsFe.Rows.Count; i++)
		{
			val.ValueListItems.Add(dtTerminationsFe.Rows[i]["ItemID"], dtTerminationsFe.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dtTerminationsFe.Rows[i]["ItemID"], dtTerminationsFe.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void TerminationsOpenSocketFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (dtTerminationsOpenSocket == null || dtTerminationsOpenSocket.Rows.Count == 0)
		{
			dtTerminationsOpenSocket = TerminationsOpenSocket.FillCombo(Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dtTerminationsOpenSocket.Rows.Count; i++)
		{
			val.ValueListItems.Add(dtTerminationsOpenSocket.Rows[i]["ItemID"], dtTerminationsOpenSocket.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dtTerminationsOpenSocket.Rows[i]["ItemID"], dtTerminationsOpenSocket.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void TerminationsClosedSocketFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (dtTerminationsClosedSocket == null || dtTerminationsClosedSocket.Rows.Count == 0)
		{
			dtTerminationsClosedSocket = TerminationsClosedSocket.FillCombo(Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dtTerminationsClosedSocket.Rows.Count; i++)
		{
			val.ValueListItems.Add(dtTerminationsClosedSocket.Rows[i]["ItemID"], dtTerminationsClosedSocket.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dtTerminationsClosedSocket.Rows[i]["ItemID"], dtTerminationsClosedSocket.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void TerminationsChainConnectorFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		if (dtTerminationsChainConnector == null || dtTerminationsChainConnector.Rows.Count == 0)
		{
			dtTerminationsChainConnector = TerminationsChainConnector.FillCombo(Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dtTerminationsChainConnector.Rows.Count; i++)
		{
			val.ValueListItems.Add(dtTerminationsChainConnector.Rows[i]["ItemID"], dtTerminationsChainConnector.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dtTerminationsChainConnector.Rows[i]["ItemID"], dtTerminationsChainConnector.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void FifthWireDetailsFillCombo(UltraGridRow GR, string Diameter)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		if (cboWireType.SelectedIndex > -1)
		{
			DataTable dataTable = new DataTable();
			dataTable = WireTypesDetails.FillCombo(((TextEditorControlBase)cboWireType).Value.ToString(), Diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			ValueList val = new ValueList();
			ValueList val2 = new ValueList();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				val.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				val2.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
			GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
			GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
		}
	}

	private void ShackleTypesDetailsFillCombo(UltraGridRow GR, string ShackleType, string WorkingLoadLimit)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		DataTable dataTable = new DataTable();
		dataTable = ShackleTypesDetails.FillCombo(ShackleType, WorkingLoadLimit, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			val.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void HooksTypesDetailsFillCombo(UltraGridRow GR, string HookType, string WorkingLoadLimit)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		DataTable dataTable = new DataTable();
		dataTable = HooksTypesDetails.FillCombo(HookType, WorkingLoadLimit, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			val.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void MasterLinksDualFillCombo(UltraGridRow GR, string WorkingLoadLimit)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		DataTable dataTable = new DataTable();
		dataTable = MasterLinksDual.FillCombo(WorkingLoadLimit, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			val.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void MasterLinksQuadFillCombo(UltraGridRow GR, string WorkingLoadLimit)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		DataTable dataTable = new DataTable();
		dataTable = MasterLinksQuad.FillCombo(WorkingLoadLimit, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		ValueList val = new ValueList();
		ValueList val2 = new ValueList();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			val.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
			val2.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
		}
		GR.Cells["ItemID"].ValueList = (IValueList)(object)val;
		GR.Cells["ItemBarcode"].ValueList = (IValueList)(object)val2;
	}

	private void CalculateRow(UltraGridRow Row)
	{
		Row.Cells["Qty"].Value = (decimal.Parse((Row.Cells["WireLength"].Value.ToString() == "") ? "0" : Row.Cells["WireLength"].Value.ToString()) + decimal.Parse((Row.Cells["WireAllowance"].Value.ToString() == "") ? "0" : Row.Cells["WireAllowance"].Value.ToString())) * decimal.Parse((Row.Cells["Count"].Value.ToString() == "") ? "0" : Row.Cells["Count"].Value.ToString());
		if (decimal.Parse(Row.Cells["Qty"].Value.ToString()) == 0m && Row.Cells["Count"].Value.ToString() != "")
		{
			Row.Cells["Qty"].Value = Row.Cells["Count"].Value;
		}
		Row.Cells["TotalCostPrice"].Value = decimal.Parse(Row.Cells["UnitCostPrice"].Value.ToString()) * decimal.Parse(Row.Cells["Qty"].Value.ToString());
	}

	public decimal GetEyeAllowances()
	{
		decimal num = default(decimal);
		switch (cboFirstTerminationType.SelectedIndex)
		{
		case 0:
			num += decimal.Parse(dtWireTypesDetails.Select(" EyeSize = " + ((Control)(object)txtEyeSizeFirstTerminal).Text)[0]["HardEyeAllowance"].ToString());
			break;
		case 1:
			num += decimal.Parse(dtWireTypesDetails.Select(" EyeSize = " + ((Control)(object)txtEyeSizeFirstTerminal).Text)[0]["SoftEyeAllowance"].ToString());
			break;
		}
		switch (cboSecondterminationType.SelectedIndex)
		{
		case 0:
			num += decimal.Parse(dtWireTypesDetails.Select(" EyeSize = " + ((Control)(object)txtEyeSizeSecondTerminal).Text)[0]["HardEyeAllowance"].ToString());
			break;
		case 1:
			num += decimal.Parse(dtWireTypesDetails.Select(" EyeSize = " + ((Control)(object)txtEyeSizeSecondTerminal).Text)[0]["SoftEyeAllowance"].ToString());
			break;
		}
		return num / 1000m;
	}

	private void btnCreateStock_Click(object sender, EventArgs e)
	{
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Expected O, but got Unknown
		//IL_0e6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e74: Expected O, but got Unknown
		//IL_06fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Expected O, but got Unknown
		//IL_152d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1537: Expected O, but got Unknown
		//IL_1bc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd0: Expected O, but got Unknown
		//IL_225f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2269: Expected O, but got Unknown
		//IL_28f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2902: Expected O, but got Unknown
		//IL_2fa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2fab: Expected O, but got Unknown
		//IL_0d94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9e: Expected O, but got Unknown
		//IL_3664: Unknown result type (might be due to invalid IL or missing references)
		//IL_366e: Expected O, but got Unknown
		//IL_51e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_51ef: Expected O, but got Unknown
		//IL_3cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d07: Expected O, but got Unknown
		//IL_58b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_58bb: Expected O, but got Unknown
		//IL_4aeb: Unknown result type (might be due to invalid IL or missing references)
		//IL_4af5: Expected O, but got Unknown
		//IL_4396: Unknown result type (might be due to invalid IL or missing references)
		//IL_43a0: Expected O, but got Unknown
		//IL_1467: Unknown result type (might be due to invalid IL or missing references)
		//IL_1471: Expected O, but got Unknown
		//IL_1b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0a: Expected O, but got Unknown
		//IL_2199: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a3: Expected O, but got Unknown
		//IL_5f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5f66: Expected O, but got Unknown
		//IL_2832: Unknown result type (might be due to invalid IL or missing references)
		//IL_283c: Expected O, but got Unknown
		//IL_6656: Unknown result type (might be due to invalid IL or missing references)
		//IL_6660: Expected O, but got Unknown
		//IL_2ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2ed5: Expected O, but got Unknown
		//IL_5770: Unknown result type (might be due to invalid IL or missing references)
		//IL_577a: Expected O, but got Unknown
		//IL_359e: Unknown result type (might be due to invalid IL or missing references)
		//IL_35a8: Expected O, but got Unknown
		//IL_5e3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_5e46: Expected O, but got Unknown
		//IL_50a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_50ae: Expected O, but got Unknown
		//IL_3c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_3c41: Expected O, but got Unknown
		//IL_42d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_42da: Expected O, but got Unknown
		//IL_4969: Unknown result type (might be due to invalid IL or missing references)
		//IL_4973: Expected O, but got Unknown
		//IL_6f02: Unknown result type (might be due to invalid IL or missing references)
		//IL_6f0c: Expected O, but got Unknown
		//IL_7c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_7c83: Expected O, but got Unknown
		//IL_6536: Unknown result type (might be due to invalid IL or missing references)
		//IL_6540: Expected O, but got Unknown
		//IL_6c30: Unknown result type (might be due to invalid IL or missing references)
		//IL_6c3a: Expected O, but got Unknown
		//IL_7503: Unknown result type (might be due to invalid IL or missing references)
		//IL_750d: Expected O, but got Unknown
		//IL_749e: Unknown result type (might be due to invalid IL or missing references)
		//IL_74a8: Expected O, but got Unknown
		//IL_821d: Unknown result type (might be due to invalid IL or missing references)
		//IL_8227: Expected O, but got Unknown
		//IL_7a9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7aa9: Expected O, but got Unknown
		if (!ValidateData(ValidateDetails: false))
		{
			return;
		}
		TotalQty = decimal.Parse(((Control)(object)txtTotalQty).Text);
		dtQuotationsDetailsSlingsMaterials.AcceptChanges();
		for (int i = 0; i < dtQuotationsDetailsSlingsMaterials.Rows.Count; i++)
		{
			if (int.Parse(dtQuotationsDetailsSlingsMaterials.Rows[i]["QuotationDetailID"].ToString()) == QuotationDetailID)
			{
				dtQuotationsDetailsSlingsMaterials.Rows[i].Delete();
			}
		}
		if (cboWireType.SelectedIndex > -1)
		{
			dtWireTypesDetails = WireTypesDetails.SelectByWireTypeID(((TextEditorControlBase)cboWireType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		object value;
		if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
			WireDetailsFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
			value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["WireItemID"].ToString());
			obj.Value = value;
			DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (dataRow["DefaultStoreID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 1;
			((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
			((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = ((Control)(object)txtWireLengthMt).Text;
			((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = GetEyeAllowances();
			((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
			((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
			if (UsingBatchNoAndValidityPeriod)
			{
				ValueList batchsValueList = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList;
				if (((DisposableObjectCollectionBase)batchsValueList.ValueListItems).Count == 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
				}
			}
			int unitTypeID = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
			ValueList unitsValueList = getUnitsValueList(unitTypeID);
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList;
			if (UsingColors && dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			if (UsingSizes && dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		if (cboFirstTerminationType.SelectedIndex == 0)
		{
			if (dtTerminationsTh == null || dtTerminationsTh.Rows.Count == 0)
			{
				dtTerminationsTh = TerminationsTh.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsTh.Rows.Count > 0 && dtTerminationsTh.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsThFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj3 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsTh.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj3.Value = value;
				DataRow dataRow2 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				if (dataRow2["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow2["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow2["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 2;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList2 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList2;
					if (((DisposableObjectCollectionBase)batchsValueList2.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID2 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList2 = getUnitsValueList(unitTypeID2);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList2;
				if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboFirstTerminationType.SelectedIndex == 0 || cboFirstTerminationType.SelectedIndex == 1)
		{
			if (dtTerminationsFe == null || dtTerminationsFe.Rows.Count == 0)
			{
				dtTerminationsFe = TerminationsFe.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsFe.Rows.Count > 0 && dtTerminationsFe.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsFeFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				string text = "";
				text = "TerminationTypeID = " + ((TextEditorControlBase)cboFirstTerminationType).Value.ToString() + " and ";
				UltraGridCell obj5 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsFe.Select(text + " Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj5.Value = value;
				DataRow dataRow3 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow3["UnitID"];
				if (dataRow3["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow3["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow3["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 3;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList3 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList3;
					if (((DisposableObjectCollectionBase)batchsValueList3.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID3 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList3 = getUnitsValueList(unitTypeID3);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList3;
				if (UsingColors && dataRow3["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow3["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow3["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow3["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboFirstTerminationType.SelectedIndex == 3)
		{
			if (dtTerminationsOpenSocket == null || dtTerminationsOpenSocket.Rows.Count == 0)
			{
				dtTerminationsOpenSocket = TerminationsOpenSocket.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsOpenSocket.Rows.Count > 0 && dtTerminationsOpenSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsOpenSocketFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj7 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsOpenSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj7.Value = value;
				DataRow dataRow4 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow4["UnitID"];
				if (dataRow4["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow4["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow4["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 4;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList4 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList4;
					if (((DisposableObjectCollectionBase)batchsValueList4.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID4 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList4 = getUnitsValueList(unitTypeID4);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList4;
				if (UsingColors && dataRow4["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow4["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow4["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow4["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboFirstTerminationType.SelectedIndex == 4)
		{
			if (dtTerminationsClosedSocket == null || dtTerminationsClosedSocket.Rows.Count == 0)
			{
				dtTerminationsClosedSocket = TerminationsClosedSocket.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsClosedSocket.Rows.Count > 0 && dtTerminationsClosedSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsClosedSocketFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj9 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsClosedSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj9.Value = value;
				DataRow dataRow5 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow5["UnitID"];
				if (dataRow5["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow5["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow5["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 5;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList5 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList5;
					if (((DisposableObjectCollectionBase)batchsValueList5.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID5 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList5 = getUnitsValueList(unitTypeID5);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList5;
				if (UsingColors && dataRow5["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow5["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow5["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow5["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboFirstTerminationType.SelectedIndex == 5)
		{
			if (dtTerminationsChainConnector == null || dtTerminationsChainConnector.Rows.Count == 0)
			{
				dtTerminationsChainConnector = TerminationsChainConnector.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsChainConnector.Rows.Count > 0 && dtTerminationsChainConnector.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsChainConnectorFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj11 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsChainConnector.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj11.Value = value;
				DataRow dataRow6 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow6["UnitID"];
				if (dataRow6["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow6["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow6["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 6;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList6 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList6;
					if (((DisposableObjectCollectionBase)batchsValueList6.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID6 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList6 = getUnitsValueList(unitTypeID6);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList6;
				if (UsingColors && dataRow6["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow6["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow6["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow6["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboSecondterminationType.SelectedIndex == 0)
		{
			if (dtTerminationsTh == null || dtTerminationsTh.Rows.Count == 0)
			{
				dtTerminationsTh = TerminationsTh.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsTh.Rows.Count > 0 && dtTerminationsTh.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsThFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj13 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsTh.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj13.Value = value;
				DataRow dataRow7 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow7["UnitID"];
				if (dataRow7["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow7["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow7["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 2;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList7 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList7;
					if (((DisposableObjectCollectionBase)batchsValueList7.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID7 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList7 = getUnitsValueList(unitTypeID7);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList7;
				if (UsingColors && dataRow7["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow7["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow7["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow7["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboSecondterminationType.SelectedIndex == 0 || cboSecondterminationType.SelectedIndex == 1)
		{
			if (dtTerminationsFe == null || dtTerminationsFe.Rows.Count == 0)
			{
				dtTerminationsFe = TerminationsFe.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsFe.Rows.Count > 0 && dtTerminationsFe.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsFeFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				string text2 = "";
				text2 = "TerminationTypeID = " + ((TextEditorControlBase)cboSecondterminationType).Value.ToString() + " and ";
				UltraGridCell obj15 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsFe.Select(text2 + " Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj15.Value = value;
				DataRow dataRow8 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow8["UnitID"];
				if (dataRow8["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow8["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow8["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 3;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList8 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList8;
					if (((DisposableObjectCollectionBase)batchsValueList8.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID8 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList8 = getUnitsValueList(unitTypeID8);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList8;
				if (UsingColors && dataRow8["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow8["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow8["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow8["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboSecondterminationType.SelectedIndex == 3)
		{
			if (dtTerminationsOpenSocket == null || dtTerminationsOpenSocket.Rows.Count == 0)
			{
				dtTerminationsOpenSocket = TerminationsOpenSocket.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsOpenSocket.Rows.Count > 0 && dtTerminationsOpenSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsOpenSocketFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj17 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsOpenSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj17.Value = value;
				DataRow dataRow9 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow9["UnitID"];
				if (dataRow9["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow9["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow9["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 4;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList9 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList9;
					if (((DisposableObjectCollectionBase)batchsValueList9.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID9 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList9 = getUnitsValueList(unitTypeID9);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList9;
				if (UsingColors && dataRow9["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow9["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow9["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow9["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboSecondterminationType.SelectedIndex == 4)
		{
			if (dtTerminationsClosedSocket == null || dtTerminationsClosedSocket.Rows.Count == 0)
			{
				dtTerminationsClosedSocket = TerminationsClosedSocket.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsClosedSocket.Rows.Count > 0 && dtTerminationsClosedSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsClosedSocketFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj19 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsClosedSocket.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj19.Value = value;
				DataRow dataRow10 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow10["UnitID"];
				if (dataRow10["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow10["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow10["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 5;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList10 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList10;
					if (((DisposableObjectCollectionBase)batchsValueList10.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID10 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList10 = getUnitsValueList(unitTypeID10);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList10;
				if (UsingColors && dataRow10["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow10["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow10["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow10["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (cboSecondterminationType.SelectedIndex == 5)
		{
			if (dtTerminationsChainConnector == null || dtTerminationsChainConnector.Rows.Count == 0)
			{
				dtTerminationsChainConnector = TerminationsChainConnector.FillCombo(((Control)(object)txtDiameter).Text, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtTerminationsChainConnector.Rows.Count > 0 && dtTerminationsChainConnector.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				TerminationsChainConnectorFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
				UltraGridCell obj21 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
				value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtTerminationsChainConnector.Select(" Diameter>= " + ((Control)(object)txtDiameter).Text)[0]["ItemID"].ToString());
				obj21.Value = value;
				DataRow dataRow11 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow11["UnitID"];
				if (dataRow11["DefaultStoreID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow11["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow11["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
				}
				((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 6;
				((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = (decimal)int.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
				((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
				((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
				if (UsingBatchNoAndValidityPeriod)
				{
					ValueList batchsValueList11 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
					((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList11;
					if (((DisposableObjectCollectionBase)batchsValueList11.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
					}
				}
				int unitTypeID11 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
				ValueList unitsValueList11 = getUnitsValueList(unitTypeID11);
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList11;
				if (UsingColors && dataRow11["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow11["ItemColorCategoryID"].ToString()));
				}
				if (UsingSizes && dataRow11["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow11["ItemSizeCategoryID"].ToString()));
				}
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
				ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			}
		}
		if (UNSlingLegsCount.Value.ToString() == "5")
		{
			if (dtWireTypesDetails == null || dtWireTypesDetails.Rows.Count == 0 || !dtWireTypesDetails.Rows[0]["WireTypeID"].ToString().Equals(((TextEditorControlBase)cboWireType).Value.ToString()))
			{
				dtWireTypesDetails = WireTypesDetails.SelectByWireTypeID(((TextEditorControlBase)cboWireType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				decimal num = default(decimal);
				num = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
				if (dtWireTypesDetails.Select("SingleLegLoad >=" + num).Length != 0)
				{
					DataRow dataRow12 = dtWireTypesDetails.Select("SingleLegLoad >=" + num)[0];
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					FifthWireDetailsFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
					UltraGridCell obj23 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataRow12["WireItemID"].ToString());
					obj23.Value = value;
					DataRow dataRow13 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow13["UnitID"];
					if (dataRow13["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow13["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow13["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = 1m * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = ((Control)(object)txtFifthWireLengthMt).Text;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = (decimal.Parse(dataRow12["HardEyeAllowance"].ToString()) * 2m / 1000m).ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList12 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList12;
						if (((DisposableObjectCollectionBase)batchsValueList12.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID12 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList12 = getUnitsValueList(unitTypeID12);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList12;
					if (UsingColors && dataRow13["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow13["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow13["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow13["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
		}
		if (UNSlingLegsCount.Value.ToString() == "5")
		{
			DataTable dataTable = new DataTable();
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				decimal num2 = default(decimal);
				num2 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
				if (dtWireTypesDetails.Select("SingleLegLoad >=" + num2).Length != 0)
				{
					string diameter = dtWireTypesDetails.Select("SingleLegLoad >=" + num2)[0]["Diameter"].ToString();
					dataTable = TerminationsTh.FillCombo(diameter, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				}
				if (dataTable.Rows.Count > 0)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					TerminationsThFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
					UltraGridCell obj25 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataTable.Rows[0]["ItemID"].ToString());
					obj25.Value = value;
					DataRow dataRow14 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow14["UnitID"];
					if (dataRow14["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow14["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow14["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 2;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = 2m * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList13 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList13;
						if (((DisposableObjectCollectionBase)batchsValueList13.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID13 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList13 = getUnitsValueList(unitTypeID13);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList13;
					if (UsingColors && dataRow14["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow14["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow14["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow14["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
		}
		if (UNSlingLegsCount.Value.ToString() == "5")
		{
			DataTable dataTable2 = new DataTable();
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				decimal num3 = default(decimal);
				num3 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
				if (dtWireTypesDetails.Select("SingleLegLoad >=" + num3).Length != 0)
				{
					string diameter2 = dtWireTypesDetails.Select("SingleLegLoad >=" + num3)[0]["Diameter"].ToString();
					dataTable2 = TerminationsFe.FillCombo(diameter2, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
				}
				if (dataTable2.Rows.Count > 0)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					TerminationsFeFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
					UltraGridCell obj27 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dataTable2.Rows[0]["ItemID"].ToString());
					obj27.Value = value;
					DataRow dataRow15 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow15["UnitID"];
					if (dataRow15["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow15["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow15["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 3;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = 2m * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList14 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList14;
						if (((DisposableObjectCollectionBase)batchsValueList14.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID14 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList14 = getUnitsValueList(unitTypeID14);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList14;
					if (UsingColors && dataRow15["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow15["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow15["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow15["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
		}
		if (cboShackleType.SelectedIndex > -1)
		{
			if (dtShackleTypesDetails == null || dtShackleTypesDetails.Rows.Count == 0)
			{
				dtShackleTypesDetails = ShackleTypesDetails.SelectByShackleTypeID(((TextEditorControlBase)cboShackleType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				decimal num4 = default(decimal);
				num4 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString());
				if (dtShackleTypesDetails.Select(" WorkingLoadLimit>= " + num4).Length != 0)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					ShackleTypesDetailsFillCombo(((UltraGridBase)ULGData).ActiveRow, ((TextEditorControlBase)cboShackleType).Value.ToString(), "-1");
					UltraGridCell obj29 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtShackleTypesDetails.Select(" WorkingLoadLimit>= " + num4)[0]["ShackleTypeDetailItemID"].ToString());
					obj29.Value = value;
					DataRow dataRow16 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow16["UnitID"];
					if (dataRow16["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow16["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow16["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 7;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = decimal.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList15 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList15;
						if (((DisposableObjectCollectionBase)batchsValueList15.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID15 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList15 = getUnitsValueList(unitTypeID15);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList15;
					if (UsingColors && dataRow16["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow16["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow16["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow16["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
		}
		if (cboHookType.SelectedIndex > -1)
		{
			if (dtHooksTypesDetails == null || dtHooksTypesDetails.Rows.Count == 0)
			{
				dtHooksTypesDetails = HooksTypesDetails.SelectByHookTypeID(((TextEditorControlBase)cboHookType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				decimal num5 = default(decimal);
				num5 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString());
				if (dtHooksTypesDetails.Select(" WorkingLoadLimit>= " + num5).Length != 0)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					HooksTypesDetailsFillCombo(((UltraGridBase)ULGData).ActiveRow, ((TextEditorControlBase)cboHookType).Value.ToString(), "-1");
					UltraGridCell obj31 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtHooksTypesDetails.Select(" WorkingLoadLimit>= " + num5)[0]["HookTypeDetailItemID"].ToString());
					obj31.Value = value;
					DataRow dataRow17 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow17["UnitID"];
					if (dataRow17["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow17["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow17["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 8;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = decimal.Parse((int.Parse(UNSlingLegsCount.Value.ToString()) < 5) ? UNSlingLegsCount.Value.ToString() : "4") * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList16 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList16;
						if (((DisposableObjectCollectionBase)batchsValueList16.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID16 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList16 = getUnitsValueList(unitTypeID16);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList16;
					if (UsingColors && dataRow17["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow17["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow17["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow17["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
		}
		if (cboLinkType.SelectedIndex > -1)
		{
			if (dtMasterLinksDual == null || dtMasterLinksDual.Rows.Count == 0)
			{
				dtMasterLinksDual = MasterLinksDual.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtMasterLinksQuad == null || dtMasterLinksQuad.Rows.Count == 0)
			{
				dtMasterLinksQuad = MasterLinksQuad.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
			{
				decimal num6 = default(decimal);
				switch (int.Parse(UNSlingLegsCount.Value.ToString()))
				{
				case 1:
					num6 += decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString());
					break;
				case 2:
					num6 += decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["TwoLegsLoad"].ToString());
					break;
				case 3:
					num6 += decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
					break;
				case 4:
					num6 += decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
					break;
				case 5:
					num6 += decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
					break;
				}
				if (dtMasterLinksDual.Select(" WorkingLoadLimit>= " + num6).Length != 0 && int.Parse(UNSlingLegsCount.Value.ToString()) < 3)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					MasterLinksDualFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
					UltraGridCell obj33 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtMasterLinksDual.Select(" WorkingLoadLimit>= " + num6)[0]["MasterLinkDualItemID"].ToString());
					obj33.Value = value;
					DataRow dataRow18 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow18["UnitID"];
					if (dataRow18["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow18["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow18["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 9;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = 1m * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList17 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList17;
						if (((DisposableObjectCollectionBase)batchsValueList17.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID17 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList17 = getUnitsValueList(unitTypeID17);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList17;
					if (UsingColors && dataRow18["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow18["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow18["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow18["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
				else if (dtMasterLinksQuad.Select(" WorkingLoadLimit>= " + num6).Length != 0 && int.Parse(UNSlingLegsCount.Value.ToString()) <= 5)
				{
					ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
					((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
					MasterLinksQuadFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
					UltraGridCell obj35 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
					value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtMasterLinksQuad.Select(" WorkingLoadLimit>= " + num6)[0]["MasterLinkQuadItemID"].ToString());
					obj35.Value = value;
					DataRow dataRow19 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
					((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow19["UnitID"];
					if (dataRow19["DefaultStoreID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow19["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow19["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
					}
					((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 10;
					((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = 1m * TotalQty;
					((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
					((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
					if (UsingBatchNoAndValidityPeriod)
					{
						ValueList batchsValueList18 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
						((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList18;
						if (((DisposableObjectCollectionBase)batchsValueList18.ValueListItems).Count == 0)
						{
							((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
						}
					}
					int unitTypeID18 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
					ValueList unitsValueList18 = getUnitsValueList(unitTypeID18);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList18;
					if (UsingColors && dataRow19["ItemColorCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow19["ItemColorCategoryID"].ToString()));
					}
					if (UsingSizes && dataRow19["ItemSizeCategoryID"] != DBNull.Value)
					{
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
						((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow19["ItemSizeCategoryID"].ToString()));
					}
					((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
					CalculateRow(((UltraGridBase)ULGData).ActiveRow);
					ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
				}
			}
		}
		if (int.Parse(UNSlingLegsCount.Value.ToString()) != 5 || cboLinkType.SelectedIndex <= -1)
		{
			return;
		}
		if (dtMasterLinksDual == null || dtMasterLinksDual.Rows.Count == 0)
		{
			dtMasterLinksDual = MasterLinksDual.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		if (dtMasterLinksQuad == null || dtMasterLinksQuad.Rows.Count == 0)
		{
			dtMasterLinksQuad = MasterLinksQuad.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		if (dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length == 0)
		{
			return;
		}
		decimal num7 = default(decimal);
		num7 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
		decimal num8 = default(decimal);
		num8 = decimal.Parse(dtWireTypesDetails.Select(" SingleLegLoad>= " + num7)[0]["SingleLegLoad"].ToString());
		if (dtMasterLinksDual.Select(" WorkingLoadLimit>= " + num8).Length == 0 || int.Parse(UNSlingLegsCount.Value.ToString()) != 5)
		{
			return;
		}
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
		MasterLinksDualFillCombo(((UltraGridBase)ULGData).ActiveRow, "-1");
		UltraGridCell obj37 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"];
		value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"].Value = dtMasterLinksDual.Select(" WorkingLoadLimit>= " + num8)[0]["MasterLinkDualItemID"].ToString());
		obj37.Value = value;
		DataRow dataRow20 = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
		((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = 1;
		((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = 1;
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow20["UnitID"];
		if (dataRow20["DefaultStoreID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow20["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow20["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value = 9;
		((UltraGridBase)ULGData).ActiveRow.Cells["Count"].Value = 1m * TotalQty;
		((UltraGridBase)ULGData).ActiveRow.Cells["WireLength"].Value = "0";
		((UltraGridBase)ULGData).ActiveRow.Cells["WireAllowance"].Value = "0";
		((UltraGridBase)ULGData).ActiveRow.Cells["QuotationDetailID"].Value = QuotationDetailID;
		((UltraGridBase)ULGData).ActiveRow.Cells["QuotationID"].Value = QuotationID;
		if (UsingBatchNoAndValidityPeriod)
		{
			ValueList batchsValueList19 = getBatchsValueList(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString()));
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].ValueList = (IValueList)(object)batchsValueList19;
			if (((DisposableObjectCollectionBase)batchsValueList19.ValueListItems).Count == 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = DBNull.Value;
			}
		}
		int unitTypeID19 = int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString());
		ValueList unitsValueList19 = getUnitsValueList(unitTypeID19);
		((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].ValueList = (IValueList)(object)unitsValueList19;
		if (UsingColors && dataRow20["ItemColorCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow20["ItemColorCategoryID"].ToString()));
		}
		if (UsingSizes && dataRow20["ItemSizeCategoryID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow20["ItemSizeCategoryID"].ToString()));
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (NavMode || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BatchID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ColorID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemSizeID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitID" && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "WireAllowance" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "WireLength") && ((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value.ToString() != "1")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemBarCode") && ((UltraGridBase)ULGData).ActiveRow.Cells["MaterialTypeID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((UltraGridBase)ULGData).ActiveRow != null && ULGData.ActiveCell != null)
		{
			if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice") && ULGData.ActiveCell.Value == DBNull.Value)
			{
				ULGData.ActiveCell.Value = 0;
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice")
			{
				CalculateRow(((UltraGridBase)ULGData).ActiveRow);
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice" && decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString()) > 0m)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value.ToString()) / decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0e5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e67: Expected O, but got Unknown
		//IL_0e75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7f: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "MaterialTypeID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			switch (e.Cell.Value.ToString())
			{
			case "1":
				WireDetailsFillCombo(e.Cell.Row, "-1");
				break;
			case "2":
				TerminationsThFillCombo(e.Cell.Row, "-1");
				break;
			case "3":
				TerminationsFeFillCombo(e.Cell.Row, "-1");
				break;
			case "4":
				TerminationsOpenSocketFillCombo(e.Cell.Row, "-1");
				break;
			case "5":
				TerminationsClosedSocketFillCombo(e.Cell.Row, "-1");
				break;
			case "6":
				TerminationsChainConnectorFillCombo(e.Cell.Row, "-1");
				break;
			case "7":
				ShackleTypesDetailsFillCombo(e.Cell.Row, "-1", "-1");
				break;
			case "8":
				HooksTypesDetailsFillCombo(e.Cell.Row, "-1", "-1");
				break;
			case "9":
				MasterLinksDualFillCombo(e.Cell.Row, "-1");
				break;
			case "10":
				MasterLinksQuadFillCombo(e.Cell.Row, "-1");
				break;
			}
			e.Cell.Row.Cells["QuotationDetailID"].Value = QuotationDetailID;
			e.Cell.Row.Cells["QuotationID"].Value = QuotationID;
		}
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (dataRow["DefaultStoreID"] != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value = ((dtStores.Select(" StoreID = " + dataRow["DefaultStoreID"].ToString())[0]["BranchID"].ToString() == GlobalVariables.CurrentBranchID) ? dataRow["DefaultStoreID"] : dtStores.Select("BranchID = " + GlobalVariables.CurrentBranchID)[0]["StoreID"].ToString());
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
			int num2 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
			if (num2 != 0)
			{
				e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num2);
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
			e.Cell.Row.Cells["QuotationDetailID"].Value = QuotationDetailID;
			e.Cell.Row.Cells["QuotationID"].Value = QuotationID;
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "BatchID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"] != DBNull.Value)
		{
			int num3 = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtBatchs.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["ItemID"].ToString()) : 0);
			if (num3 != 0)
			{
				DataRow dataRow2 = dtItems.Select(" ItemID= " + num3)[0];
				UltraGridCell obj2 = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
				object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num3);
				obj2.Value = value;
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				int num4 = ((((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value != DBNull.Value) ? int.Parse(dtUnits.Select(" UnitID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value.ToString())[0]["UnitTypeID"].ToString()) : 0);
				if (num4 != 0)
				{
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
					e.Cell.Row.Cells["UnitID"].ValueList = (IValueList)(object)getUnitsValueList(num4);
					((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow2["UnitID"];
				}
				else
				{
					e.Cell.Row.Cells["UnitID"].ValueList = null;
					e.Cell.Row.Cells["UnitID"].Value = DBNull.Value;
				}
				if (UsingColors && dataRow2["ItemColorCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow2["ItemColorCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ColorID"].Value = 1;
					e.Cell.Row.Cells["ColorID"].ValueList = null;
				}
				if (UsingSizes && dataRow2["ItemSizeCategoryID"] != DBNull.Value)
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow2["ItemSizeCategoryID"].ToString()));
				}
				else
				{
					e.Cell.Row.Cells["ItemSizeID"].Value = 1;
					e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
				}
			}
			e.Cell.Row.Cells["QuotationDetailID"].Value = QuotationDetailID;
			e.Cell.Row.Cells["QuotationID"].Value = QuotationID;
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F6 || NavMode)
		{
			return;
		}
		if (((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Notes")
		{
			frmEnterValue frmEnterValue2 = new frmEnterValue(GlobalVariables.IsArabic ? "الوصف" : "Description", _IsInt: false, _IsNumeric: false, ULGData.ActiveCell.Value.ToString());
			frmEnterValue2.WindowState = FormWindowState.Normal;
			if (frmEnterValue2.ShowDialog() == DialogResult.OK)
			{
				ULGData.ActiveCell.Value = frmEnterValue2.Value;
			}
		}
		else if (ULGData.ActiveCell != null && ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value != DBNull.Value)
		{
			frmGetItemAllowedQty frmGetItemAllowedQty2 = new frmGetItemAllowedQty(((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["StoreID"].Value.ToString(), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate));
			frmGetItemAllowedQty2.WindowState = FormWindowState.Normal;
			frmGetItemAllowedQty2.ShowDialog();
			((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value = ((frmGetItemAllowedQty2.ColorID > 0) ? ((object)frmGetItemAllowedQty2.ColorID) : ((UltraGridBase)ULGData).ActiveRow.Cells["ColorID"].Value);
			((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value = ((frmGetItemAllowedQty2.ItemSizeID > 0) ? ((object)frmGetItemAllowedQty2.ItemSizeID) : ((UltraGridBase)ULGData).ActiveRow.Cells["ItemSizeID"].Value);
			((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value = ((frmGetItemAllowedQty2.BatchID > 0) ? ((object)frmGetItemAllowedQty2.BatchID) : ((UltraGridBase)ULGData).ActiveRow.Cells["BatchID"].Value);
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value = ((frmGetItemAllowedQty2.Avg > 0m) ? ((object)frmGetItemAllowedQty2.Avg) : ((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value);
			((UltraGridBase)ULGData).ActiveRow.Cells["TotalCostPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["UnitCostPrice"].Value.ToString()) * decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Qty"].Value.ToString());
			CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		}
		e.Handled = true;
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitCostPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalCostPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void btnGetDescription_Click(object sender, EventArgs e)
	{
		if (UNSlingLegsCount.Value != null && int.Parse(UNSlingLegsCount.Value.ToString()) > 0)
		{
			int num = int.Parse(UNSlingLegsCount.Value.ToString());
			if (num == 1)
			{
				SlingDesc = "SINGLE LEG";
			}
			else
			{
				SlingDesc = UNSlingLegsCount.Value.ToString() + " LEGS";
			}
		}
		if (((TextEditorControlBase)cboWireType).Value != null)
		{
			SlingDesc = SlingDesc + " " + ((Control)(object)cboWireType).Text;
		}
		SlingDesc += "\nMANUFACTURED ACCORDING TO BS EN 13414:2003 \n";
		if (UNSlingLegsCount.Value != null && int.Parse(UNSlingLegsCount.Value.ToString()) > 0)
		{
			SlingDesc = SlingDesc + "\nLEG " + ((((Control)(object)txtWireLengthMt).Text == "") ? 0m : decimal.Parse(((Control)(object)txtWireLengthMt).Text)).ToString("0.##") + " M X " + ((((Control)(object)txtDiameter).Text == "") ? 0m : decimal.Parse(((Control)(object)txtDiameter).Text)).ToString("0.##") + " MM DIM -6X36 - IWRC - GALVANIZED";
		}
		if (cboLinkType.SelectedIndex > -1 && ((TextEditorControlBase)cboLinkType).Value != null)
		{
			string text = "";
			DataRow[] array = dtMaterialsTypes.Select("MaterialTypeID in(9,10) ");
			if (array.Length != 0)
			{
				DataRow[] array2 = ((DataView)((UltraGridBase)ULGData).DataSource).ToTable().Select("MaterialTypeID in(9,10) ");
				if (array2.Length != 0)
				{
					text = dtItems.Select("ItemID = " + array2[0]["ItemID"])[0]["Name"].ToString();
					SlingDesc = SlingDesc + "\nFITTED WITH " + text + " " + array[0]["MaterialTypeName"].ToString();
				}
			}
		}
		if (cboFirstTerminationType.SelectedIndex > -1 && ((TextEditorControlBase)cboFirstTerminationType).Value != null)
		{
			SlingDesc = SlingDesc + "\nTERMINATION IN " + ((Control)(object)cboFirstTerminationType).Text;
		}
		if (cboSecondterminationType.SelectedIndex > -1 && ((TextEditorControlBase)cboSecondterminationType).Value != null)
		{
			SlingDesc = SlingDesc + " X " + ((Control)(object)cboSecondterminationType).Text + " EYE";
		}
		if (cboShackleType.SelectedIndex > -1 && ((TextEditorControlBase)cboShackleType).Value != null)
		{
			if (dtShackleTypesDetails == null || dtShackleTypesDetails.Rows.Count == 0)
			{
				dtShackleTypesDetails = ShackleTypesDetails.SelectByShackleTypeID(((TextEditorControlBase)cboShackleType).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtShackleTypesDetails.Rows.Count > 0 && dtWireTypesDetails != null && dtWireTypesDetails.Rows.Count > 0 && dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0 && dtShackleTypesDetails.Select(" WorkingLoadLimit>= " + dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString()).Length != 0)
			{
				DataRow[] array3 = dtShackleTypesDetails.Select(" WorkingLoadLimit>= " + dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString());
				if (array3 != null && array3.Length != 0)
				{
					SlingDesc = SlingDesc + "\nBOTTOM ENDS FITTED WITH " + decimal.Parse(array3[0]["WorkingLoadLimit"].ToString()).ToString("0.##") + " TON SAFETY PIN BOW SHACKLES.";
				}
			}
		}
		if (dtWireTypesDetails != null && dtWireTypesDetails.Rows.Count > 0 && dtWireTypesDetails != null && dtWireTypesDetails.Rows.Count > 0 && dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text).Length != 0)
		{
			SlingDesc += "\nSAFE WORKING LOAD : ";
			if (UNSlingLegsCount.Value != null && int.Parse(UNSlingLegsCount.Value.ToString()) > 0)
			{
				int num2 = int.Parse(UNSlingLegsCount.Value.ToString());
				switch (num2)
				{
				case 1:
					SlingDesc = SlingDesc + decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString()).ToString("0.##") + " TON.";
					break;
				case 2:
					SlingDesc = SlingDesc + decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["TwoLegsLoad"].ToString()).ToString("0.##") + " TON.";
					break;
				default:
					SlingDesc = SlingDesc + decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString()).ToString("0.##") + " TON.";
					break;
				}
				if (num2 > 1)
				{
					SlingDesc += " @0-45 DEG ";
				}
			}
		}
		SlingDesc += "\n\nWIRE ROPE : BRUNTON WOLF -UAE \nFERRULE & THIMBLE: UNISPLICE – UK";
		if (cboLinkType.SelectedIndex > -1 && ((TextEditorControlBase)cboLinkType).Value != null)
		{
			if (dtMasterLinksDual == null || dtMasterLinksDual.Rows.Count == 0)
			{
				dtMasterLinksDual = MasterLinksDual.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtMasterLinksQuad == null || dtMasterLinksQuad.Rows.Count == 0)
			{
				dtMasterLinksQuad = MasterLinksQuad.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			}
			if (dtWireTypesDetails != null)
			{
				DataRow[] array4 = dtWireTypesDetails.Select(" Diameter= " + ((((Control)(object)txtDiameter).Text == "") ? 0m : decimal.Parse(((Control)(object)txtDiameter).Text)));
				if (array4 != null && array4.Length != 0)
				{
					decimal num3 = default(decimal);
					switch (int.Parse(UNSlingLegsCount.Value.ToString()))
					{
					case 1:
						num3 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["SingleLegLoad"].ToString());
						break;
					case 2:
						num3 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["TwoLegsLoad"].ToString());
						break;
					case 3:
						num3 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
						break;
					case 4:
						num3 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
						break;
					case 5:
						num3 = decimal.Parse(dtWireTypesDetails.Select(" Diameter= " + ((Control)(object)txtDiameter).Text)[0]["FourLegsLoad"].ToString());
						break;
					}
					if (dtMasterLinksDual.Select(" WorkingLoadLimit>= " + num3).Length != 0 && int.Parse(UNSlingLegsCount.Value.ToString()) < 3)
					{
						SlingDesc = SlingDesc + "\nLINK :" + dtMasterLinksDual.Select(" WorkingLoadLimit>= " + num3)[0]["MasterLinkSupplierName"];
					}
					else if (dtMasterLinksQuad.Select(" WorkingLoadLimit>= " + num3).Length != 0 && int.Parse(UNSlingLegsCount.Value.ToString()) <= 5)
					{
						SlingDesc = SlingDesc + "\nLINK :" + dtMasterLinksQuad.Select(" WorkingLoadLimit>= " + num3)[0]["MasterLinkSupplierName"];
					}
				}
			}
		}
		if (cboShackleType.SelectedIndex > -1 && ((TextEditorControlBase)cboShackleType).Value != null)
		{
			SlingDesc = SlingDesc + "\nSHACKLES: " + ((Control)(object)cboShackleType).Text;
		}
		((Control)(object)txtSlingDesc).Text = SlingDesc;
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Expected O, but got Unknown
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Expected O, but got Unknown
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Expected O, but got Unknown
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.Transactions.frmSLNQuotationsSlings));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblSlingLegsCount = new UltraLabel();
		this.UNSlingLegsCount = new UltraNumericEditor();
		this.lblWireType = new UltraLabel();
		this.cboWireType = new UltraComboEditor();
		this.lblWireLengthMt = new UltraLabel();
		this.txtWireLengthMt = new UltraTextEditor();
		this.lblWireLengthFt = new UltraLabel();
		this.txtWireLengthFt = new UltraTextEditor();
		this.lblFifthWireLengthMt = new UltraLabel();
		this.lblFifthWireLengthFt = new UltraLabel();
		this.txtFifthWireLengthMt = new UltraTextEditor();
		this.txtFifthWireLengthFt = new UltraTextEditor();
		this.lblDiameter = new UltraLabel();
		this.txtDiameter = new UltraTextEditor();
		this.lblFirstTerminationType = new UltraLabel();
		this.cboFirstTerminationType = new UltraComboEditor();
		this.cboSecondterminationType = new UltraComboEditor();
		this.lblSecondTerminationType = new UltraLabel();
		this.cboShackleType = new UltraComboEditor();
		this.lblShackleType = new UltraLabel();
		this.cboHookType = new UltraComboEditor();
		this.lblHookType = new UltraLabel();
		this.cboLinkType = new UltraComboEditor();
		this.lblLinkType = new UltraLabel();
		this.lblEyeSizeFirstTerminal = new UltraLabel();
		this.txtEyeSizeFirstTerminal = new UltraTextEditor();
		this.lblEyeSizeSecondTerminal = new UltraLabel();
		this.txtEyeSizeSecondTerminal = new UltraTextEditor();
		this.btnClose = new UltraButton();
		this.btnCreateStock = new UltraButton();
		this.lblTotalQty = new UltraLabel();
		this.txtTotalQty = new UltraTextEditor();
		this.btnGetDescription = new UltraButton();
		this.ultraLabel1 = new UltraLabel();
		this.txtSlingDesc = new UltraFormattedTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UNSlingLegsCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboWireType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWireLengthMt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWireLengthFt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFifthWireLengthMt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFifthWireLengthFt).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiameter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboFirstTerminationType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSecondterminationType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboShackleType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboHookType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboLinkType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEyeSizeFirstTerminal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEyeSizeSecondTerminal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		this.lblSlingLegsCount.AutoEllipsis = false;
		resources.ApplyResources(this.lblSlingLegsCount, "lblSlingLegsCount");
		((System.Windows.Forms.Control)(object)this.lblSlingLegsCount).Name = "lblSlingLegsCount";
		((ControlBase)this.lblSlingLegsCount).WrapText = false;
		((UltraNumericEditorBase)this.UNSlingLegsCount).FormatString = "";
		resources.ApplyResources(this.UNSlingLegsCount, "UNSlingLegsCount");
		this.UNSlingLegsCount.MaxValue = 5;
		this.UNSlingLegsCount.MinValue = 1;
		((System.Windows.Forms.Control)(object)this.UNSlingLegsCount).Name = "UNSlingLegsCount";
		((UltraNumericEditorBase)this.UNSlingLegsCount).PromptChar = ' ';
		((UltraNumericEditorBase)this.UNSlingLegsCount).SpinButtonDisplayStyle = (ButtonDisplayStyle)1;
		this.UNSlingLegsCount.SpinIncrement = 1;
		this.UNSlingLegsCount.Value = 1;
		((UltraNumericEditorBase)this.UNSlingLegsCount).ValueChanged += new System.EventHandler(UNSlingLegsCount_ValueChanged);
		this.lblWireType.AutoEllipsis = false;
		resources.ApplyResources(this.lblWireType, "lblWireType");
		((System.Windows.Forms.Control)(object)this.lblWireType).Name = "lblWireType";
		((ControlBase)this.lblWireType).WrapText = false;
		this.cboWireType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboWireType, "cboWireType");
		((System.Windows.Forms.Control)(object)this.cboWireType).Name = "cboWireType";
		((TextEditorControlBase)this.cboWireType).ValueChanged += new System.EventHandler(cboWireType_ValueChanged);
		this.lblWireLengthMt.AutoEllipsis = false;
		resources.ApplyResources(this.lblWireLengthMt, "lblWireLengthMt");
		((System.Windows.Forms.Control)(object)this.lblWireLengthMt).Name = "lblWireLengthMt";
		((ControlBase)this.lblWireLengthMt).WrapText = false;
		resources.ApplyResources(this.txtWireLengthMt, "txtWireLengthMt");
		((System.Windows.Forms.Control)(object)this.txtWireLengthMt).Name = "txtWireLengthMt";
		((TextEditorControlBase)this.txtWireLengthMt).ValueChanged += new System.EventHandler(txtWireLengthMt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtWireLengthMt).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxNumber_KeyPress);
		this.lblWireLengthFt.AutoEllipsis = false;
		resources.ApplyResources(this.lblWireLengthFt, "lblWireLengthFt");
		((System.Windows.Forms.Control)(object)this.lblWireLengthFt).Name = "lblWireLengthFt";
		((ControlBase)this.lblWireLengthFt).WrapText = false;
		resources.ApplyResources(this.txtWireLengthFt, "txtWireLengthFt");
		((System.Windows.Forms.Control)(object)this.txtWireLengthFt).Name = "txtWireLengthFt";
		((TextEditorControlBase)this.txtWireLengthFt).ValueChanged += new System.EventHandler(txtWireLengthFt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtWireLengthFt).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxNumber_KeyPress);
		this.lblFifthWireLengthMt.AutoEllipsis = false;
		resources.ApplyResources(this.lblFifthWireLengthMt, "lblFifthWireLengthMt");
		((System.Windows.Forms.Control)(object)this.lblFifthWireLengthMt).Name = "lblFifthWireLengthMt";
		((ControlBase)this.lblFifthWireLengthMt).WrapText = false;
		this.lblFifthWireLengthFt.AutoEllipsis = false;
		resources.ApplyResources(this.lblFifthWireLengthFt, "lblFifthWireLengthFt");
		((System.Windows.Forms.Control)(object)this.lblFifthWireLengthFt).Name = "lblFifthWireLengthFt";
		((ControlBase)this.lblFifthWireLengthFt).WrapText = false;
		resources.ApplyResources(this.txtFifthWireLengthMt, "txtFifthWireLengthMt");
		((System.Windows.Forms.Control)(object)this.txtFifthWireLengthMt).Name = "txtFifthWireLengthMt";
		((TextEditorControlBase)this.txtFifthWireLengthMt).ValueChanged += new System.EventHandler(txtFifthWireLengthMt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFifthWireLengthMt).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxNumber_KeyPress);
		resources.ApplyResources(this.txtFifthWireLengthFt, "txtFifthWireLengthFt");
		((System.Windows.Forms.Control)(object)this.txtFifthWireLengthFt).Name = "txtFifthWireLengthFt";
		((TextEditorControlBase)this.txtFifthWireLengthFt).ValueChanged += new System.EventHandler(txtFifthWireLengthFt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtFifthWireLengthFt).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxNumber_KeyPress);
		this.lblDiameter.AutoEllipsis = false;
		resources.ApplyResources(this.lblDiameter, "lblDiameter");
		((System.Windows.Forms.Control)(object)this.lblDiameter).Name = "lblDiameter";
		((ControlBase)this.lblDiameter).WrapText = false;
		resources.ApplyResources(this.txtDiameter, "txtDiameter");
		((System.Windows.Forms.Control)(object)this.txtDiameter).Name = "txtDiameter";
		((TextEditorControlBase)this.txtDiameter).ValueChanged += new System.EventHandler(txtDiameter_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtDiameter).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxInteger_KeyPress);
		this.lblFirstTerminationType.AutoEllipsis = false;
		resources.ApplyResources(this.lblFirstTerminationType, "lblFirstTerminationType");
		((System.Windows.Forms.Control)(object)this.lblFirstTerminationType).Name = "lblFirstTerminationType";
		((ControlBase)this.lblFirstTerminationType).WrapText = false;
		this.cboFirstTerminationType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboFirstTerminationType, "cboFirstTerminationType");
		((System.Windows.Forms.Control)(object)this.cboFirstTerminationType).Name = "cboFirstTerminationType";
		this.cboSecondterminationType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSecondterminationType, "cboSecondterminationType");
		((System.Windows.Forms.Control)(object)this.cboSecondterminationType).Name = "cboSecondterminationType";
		this.lblSecondTerminationType.AutoEllipsis = false;
		resources.ApplyResources(this.lblSecondTerminationType, "lblSecondTerminationType");
		((System.Windows.Forms.Control)(object)this.lblSecondTerminationType).Name = "lblSecondTerminationType";
		((ControlBase)this.lblSecondTerminationType).WrapText = false;
		this.cboShackleType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboShackleType, "cboShackleType");
		((System.Windows.Forms.Control)(object)this.cboShackleType).Name = "cboShackleType";
		this.lblShackleType.AutoEllipsis = false;
		resources.ApplyResources(this.lblShackleType, "lblShackleType");
		((System.Windows.Forms.Control)(object)this.lblShackleType).Name = "lblShackleType";
		((ControlBase)this.lblShackleType).WrapText = false;
		this.cboHookType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboHookType, "cboHookType");
		((System.Windows.Forms.Control)(object)this.cboHookType).Name = "cboHookType";
		this.lblHookType.AutoEllipsis = false;
		resources.ApplyResources(this.lblHookType, "lblHookType");
		((System.Windows.Forms.Control)(object)this.lblHookType).Name = "lblHookType";
		((ControlBase)this.lblHookType).WrapText = false;
		this.cboLinkType.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboLinkType, "cboLinkType");
		((System.Windows.Forms.Control)(object)this.cboLinkType).Name = "cboLinkType";
		this.lblLinkType.AutoEllipsis = false;
		resources.ApplyResources(this.lblLinkType, "lblLinkType");
		((System.Windows.Forms.Control)(object)this.lblLinkType).Name = "lblLinkType";
		((ControlBase)this.lblLinkType).WrapText = false;
		this.lblEyeSizeFirstTerminal.AutoEllipsis = false;
		resources.ApplyResources(this.lblEyeSizeFirstTerminal, "lblEyeSizeFirstTerminal");
		((System.Windows.Forms.Control)(object)this.lblEyeSizeFirstTerminal).Name = "lblEyeSizeFirstTerminal";
		((ControlBase)this.lblEyeSizeFirstTerminal).WrapText = false;
		resources.ApplyResources(this.txtEyeSizeFirstTerminal, "txtEyeSizeFirstTerminal");
		((System.Windows.Forms.Control)(object)this.txtEyeSizeFirstTerminal).Name = "txtEyeSizeFirstTerminal";
		((System.Windows.Forms.Control)(object)this.txtEyeSizeFirstTerminal).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxInteger_KeyPress);
		this.lblEyeSizeSecondTerminal.AutoEllipsis = false;
		resources.ApplyResources(this.lblEyeSizeSecondTerminal, "lblEyeSizeSecondTerminal");
		((System.Windows.Forms.Control)(object)this.lblEyeSizeSecondTerminal).Name = "lblEyeSizeSecondTerminal";
		((ControlBase)this.lblEyeSizeSecondTerminal).WrapText = false;
		resources.ApplyResources(this.txtEyeSizeSecondTerminal, "txtEyeSizeSecondTerminal");
		((System.Windows.Forms.Control)(object)this.txtEyeSizeSecondTerminal).Name = "txtEyeSizeSecondTerminal";
		((System.Windows.Forms.Control)(object)this.txtEyeSizeSecondTerminal).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxInteger_KeyPress);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnCreateStock, "btnCreateStock");
		((System.Windows.Forms.Control)(object)this.btnCreateStock).Name = "btnCreateStock";
		((System.Windows.Forms.Control)(object)this.btnCreateStock).Click += new System.EventHandler(btnCreateStock_Click);
		this.lblTotalQty.AutoEllipsis = false;
		resources.ApplyResources(this.lblTotalQty, "lblTotalQty");
		((System.Windows.Forms.Control)(object)this.lblTotalQty).Name = "lblTotalQty";
		((ControlBase)this.lblTotalQty).WrapText = false;
		resources.ApplyResources(this.txtTotalQty, "txtTotalQty");
		((System.Windows.Forms.Control)(object)this.txtTotalQty).Name = "txtTotalQty";
		((System.Windows.Forms.Control)(object)this.txtTotalQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBoxNumber_KeyPress);
		resources.ApplyResources(this.btnGetDescription, "btnGetDescription");
		((System.Windows.Forms.Control)(object)this.btnGetDescription).Name = "btnGetDescription";
		((System.Windows.Forms.Control)(object)this.btnGetDescription).Click += new System.EventHandler(btnGetDescription_Click);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtSlingDesc, "txtSlingDesc");
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Control;
		((UltraFormattedTextEditorBase)this.txtSlingDesc).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.txtSlingDesc).Name = "txtSlingDesc";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSlingDesc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGetDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCreateStock);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSecondTerminationType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLinkType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHookType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShackleType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFirstTerminationType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiameter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFifthWireLengthFt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWireLengthFt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiameter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEyeSizeSecondTerminal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEyeSizeFirstTerminal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFifthWireLengthMt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWireLengthMt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFifthWireLengthFt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTotalQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWireLengthFt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWireLengthMt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboLinkType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEyeSizeSecondTerminal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEyeSizeFirstTerminal);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFifthWireLengthMt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboHookType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSecondterminationType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboShackleType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWireType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboFirstTerminationType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboWireType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UNSlingLegsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSlingLegsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmSLNQuotationsSlings";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSlingLegsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UNSlingLegsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboWireType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboFirstTerminationType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWireType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboShackleType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSecondterminationType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboHookType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFifthWireLengthMt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEyeSizeFirstTerminal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEyeSizeSecondTerminal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboLinkType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWireLengthMt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWireLengthFt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFifthWireLengthFt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWireLengthMt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFifthWireLengthMt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEyeSizeFirstTerminal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEyeSizeSecondTerminal, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiameter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWireLengthFt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFifthWireLengthFt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiameter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFirstTerminationType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShackleType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHookType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLinkType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSecondTerminationType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCreateStock, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGetDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSlingDesc, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UNSlingLegsCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboWireType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWireLengthMt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWireLengthFt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFifthWireLengthMt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFifthWireLengthFt).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiameter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboFirstTerminationType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSecondterminationType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboShackleType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboHookType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboLinkType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEyeSizeFirstTerminal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEyeSizeSecondTerminal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTotalQty).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
