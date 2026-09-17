using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmUpdateImportCargo : frmBase
{
	private DataRow drMaster;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblVoyage;

	private UltraTextEditor txtOperation;

	private UltraLabel lblOperation;

	private UltraLabel lblVessels;

	private UltraTextEditor txtVessel;

	private UltraTextEditor txtVoyage;

	private UltraLabel lblNotes;

	private UltraDateTimeEditor dtpStartDate;

	private UltraLabel lblStartDate;

	private UltraCheckEditor chkInTallySheet;

	public UltraLabel lblHistory;

	private UltraLabel lblDONo;

	private UltraLabel lblBLNo;

	private UltraLabel lblBLDate;

	private UltraLabel lblDeliveryNo;

	private UltraLabel lblContainerNo;

	private UltraLabel lblContainerType;

	private UltraLabel lblInvNo;

	private UltraLabel lblConsignee;

	private UltraLabel lblQty;

	private UltraLabel lblLoadedQty;

	private UltraLabel lblWeight;

	private UltraLabel lblUnit;

	private UltraLabel lblGoodDesc;

	private UltraTextEditor txtDONo;

	private UltraTextEditor txtBLNo;

	private UltraTextEditor txtDeliveryNo;

	private UltraTextEditor txtContainerNo;

	private UltraTextEditor txtContainerType;

	private UltraTextEditor txtInvNo;

	private UltraTextEditor txtConsignee;

	private UltraTextEditor txtQty;

	private UltraTextEditor txtWeight;

	private UltraTextEditor txtLoadedQty;

	private UltraTextEditor txtUnit;

	private UltraTextEditor txtGoodDesc;

	private UltraCheckEditor chkReImport;

	private UltraLabel lblCustomsFees;

	private UltraTextEditor txtCustomsFees;

	private UltraDateTimeEditor dtpBLDate;

	private UltraTextEditor txtDepositeAmount;

	private UltraLabel lblDepositeAmount;

	private UltraDateTimeEditor dtpDepositeDate;

	private UltraLabel lblDepositeDate;

	private UltraCheckEditor chkReturned;

	private UltraLabel lblReturnDate;

	private UltraDateTimeEditor dtpReturnDate;

	private UltraTextEditor txtNotes;

	private UltraLabel lblDeliveryDate;

	private UltraDateTimeEditor dtpDeliveryDate;

	private UltraTextEditor txtLoadType;

	private UltraLabel lblLoadType;

	private UltraLabel ultraLabel1;

	private UltraDateTimeEditor dtpExpectedReturnDate;

	public frmUpdateImportCargo()
	{
		InitializeComponent();
	}

	public frmUpdateImportCargo(string OperationServiceCargoID)
		: this()
	{
		RowID = OperationServiceCargoID;
		TableName = "MS_OperationsServicesCargos";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UltraDateTimeEditor obj = dtpStartDate;
		UltraDateTimeEditor obj2 = dtpReturnDate;
		UltraDateTimeEditor obj3 = dtpDepositeDate;
		string text = (dtpBLDate.MaskInput = "dd/mm/yyyy hh:mm tt");
		string text3 = (obj3.MaskInput = text);
		string maskInput = (obj2.MaskInput = text3);
		obj.MaskInput = maskInput;
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = OperationsServicesCargos.SelectByID(RowID, GlobalVariables.IsArabic ? "1" : "0");
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

	public void DisplayData()
	{
		if (drMaster != null)
		{
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			((Control)(object)txtOperation).Text = drMaster["OperationNo"].ToString();
			dtpStartDate.Value = (DateTime)drMaster["ServiceStartDate"];
			((Control)(object)txtVoyage).Text = drMaster["VoyageNo"].ToString();
			((Control)(object)txtVessel).Text = drMaster["VesselName"].ToString();
			((Control)(object)txtDONo).Text = drMaster["ExportDeclarationNo"].ToString();
			((Control)(object)txtBLNo).Text = drMaster["BillNo"].ToString();
			dtpBLDate.Value = drMaster["BillDate"];
			((Control)(object)txtDeliveryNo).Text = drMaster["DeliveryNo"].ToString();
			dtpDeliveryDate.Value = drMaster["DeliveryDate"];
			((Control)(object)txtContainerNo).Text = drMaster["ContainerNo"].ToString();
			((Control)(object)txtContainerType).Text = drMaster["ContainerType"].ToString();
			((Control)(object)txtInvNo).Text = drMaster["ShipperInvoiceNo"].ToString();
			((Control)(object)txtConsignee).Text = drMaster["ConsigneeName"].ToString();
			((Control)(object)txtQty).Text = decimal.Parse(drMaster["Qty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLoadedQty).Text = decimal.Parse(drMaster["LoadedQty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLoadType).Text = drMaster["LoadType"].ToString();
			((Control)(object)txtWeight).Text = decimal.Parse(drMaster["Weight"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtUnit).Text = drMaster["Unit"].ToString();
			((Control)(object)txtGoodDesc).Text = drMaster["GoodsDescription"].ToString();
			((UltraToggleEditorBase)chkInTallySheet).Checked = bool.Parse(drMaster["InTallySheet"].ToString());
			((UltraToggleEditorBase)chkReImport).Checked = bool.Parse(drMaster["IsReImport"].ToString());
			((Control)(object)txtCustomsFees).Text = decimal.Parse(drMaster["CustomFees"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtDepositeAmount).Text = decimal.Parse(drMaster["DepositAmount"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			dtpDepositeDate.Value = drMaster["DepositDate"];
			dtpExpectedReturnDate.Value = drMaster["ExpectedReturnDate"];
			((UltraToggleEditorBase)chkReturned).Checked = bool.Parse(drMaster["IsReturned"].ToString());
			dtpReturnDate.Value = drMaster["ReturnDate"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			UltraTextEditor obj = txtCustomsFees;
			UltraTextEditor obj2 = txtDepositeAmount;
			bool flag = (((EditorButtonControlBase)dtpDepositeDate).ReadOnly = !((UltraToggleEditorBase)chkReImport).Checked);
			bool readOnly = (((EditorButtonControlBase)obj2).ReadOnly = flag);
			((EditorButtonControlBase)obj).ReadOnly = readOnly;
		}
	}

	public bool ValidateData()
	{
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OperationsServicesCargos.Insert_Update(RowID, drMaster["OperationServiceID"].ToString(), drMaster["OperationID"].ToString(), (drMaster["ContainerTypeID"].ToString() == "") ? "Null" : drMaster["ContainerTypeID"].ToString(), (drMaster["ContainerNo"].ToString() == "") ? "Null" : drMaster["ContainerNo"].ToString(), (drMaster["IsExportDeclaration"].ToString() == "") ? "Null" : drMaster["IsExportDeclaration"].ToString(), ((Control)(object)txtDONo).Text, (drMaster["TransitNo"].ToString() == "") ? "Null" : drMaster["TransitNo"].ToString(), (drMaster["BillNo"].ToString() == "") ? "Null" : drMaster["BillNo"].ToString(), (drMaster["BillDate"].ToString() == "") ? "Null" : drMaster["BillDate"].ToString(), (drMaster["DeliveryNo"].ToString() == "") ? "Null" : drMaster["DeliveryNo"].ToString(), (drMaster["DeliveryDate"].ToString() == "") ? "Null" : drMaster["DeliveryDate"].ToString(), (drMaster["ShipperName"].ToString() == "") ? "Null" : drMaster["ShipperName"].ToString(), (drMaster["ShipperImporterCode"].ToString() == "") ? "Null" : drMaster["ShipperImporterCode"].ToString(), (drMaster["ShipperInvoiceNo"].ToString() == "") ? "Null" : drMaster["ShipperInvoiceNo"].ToString(), (drMaster["ConsigneeName"].ToString() == "") ? "Null" : drMaster["ConsigneeName"].ToString(), (drMaster["ConsigneeShip"].ToString() == "") ? "Null" : drMaster["ConsigneeShip"].ToString(), (drMaster["NotifyAddress"].ToString() == "") ? "Null" : drMaster["NotifyAddress"].ToString(), (drMaster["IsReExport"].ToString() == "") ? "Null" : drMaster["IsReExport"].ToString(), (drMaster["IsReImport"].ToString() == "") ? "Null" : drMaster["IsReImport"].ToString(), (drMaster["LoadingDate"].ToString() == "") ? "Null" : drMaster["LoadingDate"].ToString(), (drMaster["LoadTypeID"].ToString() == "") ? "Null" : drMaster["LoadTypeID"].ToString(), (drMaster["Qty"].ToString() == "") ? "Null" : drMaster["Qty"].ToString(), ((Control)(object)txtLoadedQty).Text, (drMaster["Weight"].ToString() == "") ? "Null" : drMaster["Weight"].ToString(), (drMaster["UnitID"].ToString() == "") ? "Null" : drMaster["UnitID"].ToString(), (drMaster["GoodsDescription"].ToString() == "") ? "Null" : drMaster["GoodsDescription"].ToString(), ((UltraToggleEditorBase)chkInTallySheet).Checked ? "1" : "0", ((Control)(object)txtCustomsFees).Text, ((Control)(object)txtDepositeAmount).Text, (dtpDepositeDate.Value == null) ? "Null" : dtpDepositeDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpExpectedReturnDate.Value == null) ? "Null" : dtpExpectedReturnDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((UltraToggleEditorBase)chkReturned).Checked ? "1" : "0", (drMaster["ReturnDate"].ToString() == "") ? "Null" : drMaster["ReturnDate"].ToString(), (drMaster["Notes"].ToString() == "") ? "Null" : drMaster["Notes"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			Close();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void lblHistory_Click(object sender, EventArgs e)
	{
		if (TableName != "" && RowID != "")
		{
			frmHistory frmHistory2 = new frmHistory(Trans_Log.SelectByRowID(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0"));
			frmHistory2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmHistory2.lblTitle).Text = "History";
			frmHistory2.ShowDialog();
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
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateImportCargo));
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
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblVoyage = new UltraLabel();
		this.txtOperation = new UltraTextEditor();
		this.lblOperation = new UltraLabel();
		this.lblVessels = new UltraLabel();
		this.txtVessel = new UltraTextEditor();
		this.txtVoyage = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.dtpStartDate = new UltraDateTimeEditor();
		this.lblStartDate = new UltraLabel();
		this.chkInTallySheet = new UltraCheckEditor();
		this.lblHistory = new UltraLabel();
		this.lblDONo = new UltraLabel();
		this.lblBLNo = new UltraLabel();
		this.lblBLDate = new UltraLabel();
		this.lblDeliveryNo = new UltraLabel();
		this.lblContainerNo = new UltraLabel();
		this.lblContainerType = new UltraLabel();
		this.lblInvNo = new UltraLabel();
		this.lblConsignee = new UltraLabel();
		this.lblQty = new UltraLabel();
		this.lblLoadedQty = new UltraLabel();
		this.lblWeight = new UltraLabel();
		this.lblUnit = new UltraLabel();
		this.lblGoodDesc = new UltraLabel();
		this.txtDONo = new UltraTextEditor();
		this.txtBLNo = new UltraTextEditor();
		this.txtDeliveryNo = new UltraTextEditor();
		this.txtContainerNo = new UltraTextEditor();
		this.txtContainerType = new UltraTextEditor();
		this.txtInvNo = new UltraTextEditor();
		this.txtConsignee = new UltraTextEditor();
		this.txtQty = new UltraTextEditor();
		this.txtWeight = new UltraTextEditor();
		this.txtLoadedQty = new UltraTextEditor();
		this.txtUnit = new UltraTextEditor();
		this.txtGoodDesc = new UltraTextEditor();
		this.chkReImport = new UltraCheckEditor();
		this.lblCustomsFees = new UltraLabel();
		this.txtCustomsFees = new UltraTextEditor();
		this.dtpBLDate = new UltraDateTimeEditor();
		this.txtDepositeAmount = new UltraTextEditor();
		this.lblDepositeAmount = new UltraLabel();
		this.dtpDepositeDate = new UltraDateTimeEditor();
		this.lblDepositeDate = new UltraLabel();
		this.chkReturned = new UltraCheckEditor();
		this.lblReturnDate = new UltraLabel();
		this.dtpReturnDate = new UltraDateTimeEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblDeliveryDate = new UltraLabel();
		this.dtpDeliveryDate = new UltraDateTimeEditor();
		this.txtLoadType = new UltraTextEditor();
		this.lblLoadType = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.dtpExpectedReturnDate = new UltraDateTimeEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkInTallySheet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDONo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBLNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsignee).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadedQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGoodDesc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkReImport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsFees).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBLDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepositeAmount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDepositeDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkReturned).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReturnDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliveryDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedReturnDate).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblVoyage, "lblVoyage");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblVoyage).Appearance = (AppearanceBase)(object)val6;
		this.lblVoyage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVoyage).Name = "lblVoyage";
		((ControlBase)this.lblVoyage).WrapText = false;
		resources.ApplyResources(this.txtOperation, "txtOperation");
		((System.Windows.Forms.Control)(object)this.txtOperation).Name = "txtOperation";
		((EditorButtonControlBase)this.txtOperation).ReadOnly = true;
		resources.ApplyResources(this.lblOperation, "lblOperation");
		this.lblOperation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperation).Name = "lblOperation";
		((ControlBase)this.lblOperation).WrapText = false;
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val7;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.txtVessel, "txtVessel");
		((System.Windows.Forms.Control)(object)this.txtVessel).Name = "txtVessel";
		((EditorButtonControlBase)this.txtVessel).ReadOnly = true;
		resources.ApplyResources(this.txtVoyage, "txtVoyage");
		((System.Windows.Forms.Control)(object)this.txtVoyage).Name = "txtVoyage";
		((EditorButtonControlBase)this.txtVoyage).ReadOnly = true;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.dtpStartDate, "dtpStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpStartDate).AlwaysInEditMode = true;
		this.dtpStartDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Name = "dtpStartDate";
		((EditorButtonControlBase)this.dtpStartDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.lblStartDate, "lblStartDate");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblStartDate).Appearance = (AppearanceBase)(object)val8;
		this.lblStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartDate).Name = "lblStartDate";
		((ControlBase)this.lblStartDate).WrapText = false;
		resources.ApplyResources(this.chkInTallySheet, "chkInTallySheet");
		((System.Windows.Forms.Control)(object)this.chkInTallySheet).Name = "chkInTallySheet";
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.lblDONo, "lblDONo");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblDONo).Appearance = (AppearanceBase)(object)val9;
		this.lblDONo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDONo).Name = "lblDONo";
		((ControlBase)this.lblDONo).WrapText = false;
		resources.ApplyResources(this.lblBLNo, "lblBLNo");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblBLNo).Appearance = (AppearanceBase)(object)val10;
		this.lblBLNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBLNo).Name = "lblBLNo";
		((ControlBase)this.lblBLNo).WrapText = false;
		resources.ApplyResources(this.lblBLDate, "lblBLDate");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblBLDate).Appearance = (AppearanceBase)(object)val11;
		this.lblBLDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBLDate).Name = "lblBLDate";
		((ControlBase)this.lblBLDate).WrapText = false;
		resources.ApplyResources(this.lblDeliveryNo, "lblDeliveryNo");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblDeliveryNo).Appearance = (AppearanceBase)(object)val12;
		this.lblDeliveryNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryNo).Name = "lblDeliveryNo";
		((ControlBase)this.lblDeliveryNo).WrapText = false;
		resources.ApplyResources(this.lblContainerNo, "lblContainerNo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblContainerNo).Appearance = (AppearanceBase)(object)val13;
		this.lblContainerNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainerNo).Name = "lblContainerNo";
		((ControlBase)this.lblContainerNo).WrapText = false;
		resources.ApplyResources(this.lblContainerType, "lblContainerType");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblContainerType).Appearance = (AppearanceBase)(object)val14;
		this.lblContainerType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainerType).Name = "lblContainerType";
		((ControlBase)this.lblContainerType).WrapText = false;
		resources.ApplyResources(this.lblInvNo, "lblInvNo");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblInvNo).Appearance = (AppearanceBase)(object)val15;
		this.lblInvNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvNo).Name = "lblInvNo";
		((ControlBase)this.lblInvNo).WrapText = false;
		resources.ApplyResources(this.lblConsignee, "lblConsignee");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblConsignee).Appearance = (AppearanceBase)(object)val16;
		this.lblConsignee.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblConsignee).Name = "lblConsignee";
		((ControlBase)this.lblConsignee).WrapText = false;
		resources.ApplyResources(this.lblQty, "lblQty");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblQty).Appearance = (AppearanceBase)(object)val17;
		this.lblQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQty).Name = "lblQty";
		((ControlBase)this.lblQty).WrapText = false;
		resources.ApplyResources(this.lblLoadedQty, "lblLoadedQty");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblLoadedQty).Appearance = (AppearanceBase)(object)val18;
		this.lblLoadedQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadedQty).Name = "lblLoadedQty";
		((ControlBase)this.lblLoadedQty).WrapText = false;
		resources.ApplyResources(this.lblWeight, "lblWeight");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblWeight).Appearance = (AppearanceBase)(object)val19;
		this.lblWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWeight).Name = "lblWeight";
		((ControlBase)this.lblWeight).WrapText = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblUnit).Appearance = (AppearanceBase)(object)val20;
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.lblGoodDesc, "lblGoodDesc");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.lblGoodDesc).Appearance = (AppearanceBase)(object)val21;
		this.lblGoodDesc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGoodDesc).Name = "lblGoodDesc";
		((ControlBase)this.lblGoodDesc).WrapText = false;
		resources.ApplyResources(this.txtDONo, "txtDONo");
		((System.Windows.Forms.Control)(object)this.txtDONo).Name = "txtDONo";
		resources.ApplyResources(this.txtBLNo, "txtBLNo");
		((System.Windows.Forms.Control)(object)this.txtBLNo).Name = "txtBLNo";
		((EditorButtonControlBase)this.txtBLNo).ReadOnly = true;
		resources.ApplyResources(this.txtDeliveryNo, "txtDeliveryNo");
		((System.Windows.Forms.Control)(object)this.txtDeliveryNo).Name = "txtDeliveryNo";
		((EditorButtonControlBase)this.txtDeliveryNo).ReadOnly = true;
		resources.ApplyResources(this.txtContainerNo, "txtContainerNo");
		((System.Windows.Forms.Control)(object)this.txtContainerNo).Name = "txtContainerNo";
		((EditorButtonControlBase)this.txtContainerNo).ReadOnly = true;
		resources.ApplyResources(this.txtContainerType, "txtContainerType");
		((System.Windows.Forms.Control)(object)this.txtContainerType).Name = "txtContainerType";
		((EditorButtonControlBase)this.txtContainerType).ReadOnly = true;
		resources.ApplyResources(this.txtInvNo, "txtInvNo");
		((System.Windows.Forms.Control)(object)this.txtInvNo).Name = "txtInvNo";
		((EditorButtonControlBase)this.txtInvNo).ReadOnly = true;
		resources.ApplyResources(this.txtConsignee, "txtConsignee");
		((System.Windows.Forms.Control)(object)this.txtConsignee).Name = "txtConsignee";
		((EditorButtonControlBase)this.txtConsignee).ReadOnly = true;
		resources.ApplyResources(this.txtQty, "txtQty");
		((System.Windows.Forms.Control)(object)this.txtQty).Name = "txtQty";
		((EditorButtonControlBase)this.txtQty).ReadOnly = true;
		resources.ApplyResources(this.txtWeight, "txtWeight");
		((System.Windows.Forms.Control)(object)this.txtWeight).Name = "txtWeight";
		((EditorButtonControlBase)this.txtWeight).ReadOnly = true;
		resources.ApplyResources(this.txtLoadedQty, "txtLoadedQty");
		((System.Windows.Forms.Control)(object)this.txtLoadedQty).Name = "txtLoadedQty";
		((System.Windows.Forms.Control)(object)this.txtLoadedQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtUnit, "txtUnit");
		((System.Windows.Forms.Control)(object)this.txtUnit).Name = "txtUnit";
		((EditorButtonControlBase)this.txtUnit).ReadOnly = true;
		resources.ApplyResources(this.txtGoodDesc, "txtGoodDesc");
		((System.Windows.Forms.Control)(object)this.txtGoodDesc).Name = "txtGoodDesc";
		((EditorButtonControlBase)this.txtGoodDesc).ReadOnly = true;
		resources.ApplyResources(this.chkReImport, "chkReImport");
		((System.Windows.Forms.Control)(object)this.chkReImport).Name = "chkReImport";
		resources.ApplyResources(this.lblCustomsFees, "lblCustomsFees");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance22");
		((ControlBase)this.lblCustomsFees).Appearance = (AppearanceBase)(object)val22;
		this.lblCustomsFees.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomsFees).Name = "lblCustomsFees";
		((ControlBase)this.lblCustomsFees).WrapText = false;
		resources.ApplyResources(this.txtCustomsFees, "txtCustomsFees");
		((System.Windows.Forms.Control)(object)this.txtCustomsFees).Name = "txtCustomsFees";
		((System.Windows.Forms.Control)(object)this.txtCustomsFees).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.dtpBLDate, "dtpBLDate");
		((UltraWinEditorMaskedControlBase)this.dtpBLDate).AlwaysInEditMode = true;
		this.dtpBLDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpBLDate).Name = "dtpBLDate";
		((EditorButtonControlBase)this.dtpBLDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpBLDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtDepositeAmount, "txtDepositeAmount");
		((System.Windows.Forms.Control)(object)this.txtDepositeAmount).Name = "txtDepositeAmount";
		((System.Windows.Forms.Control)(object)this.txtDepositeAmount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.lblDepositeAmount, "lblDepositeAmount");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance23");
		((ControlBase)this.lblDepositeAmount).Appearance = (AppearanceBase)(object)val23;
		this.lblDepositeAmount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepositeAmount).Name = "lblDepositeAmount";
		((ControlBase)this.lblDepositeAmount).WrapText = false;
		resources.ApplyResources(this.dtpDepositeDate, "dtpDepositeDate");
		((UltraWinEditorMaskedControlBase)this.dtpDepositeDate).AlwaysInEditMode = true;
		this.dtpDepositeDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDepositeDate).Name = "dtpDepositeDate";
		resources.ApplyResources(this.lblDepositeDate, "lblDepositeDate");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance24");
		((ControlBase)this.lblDepositeDate).Appearance = (AppearanceBase)(object)val24;
		this.lblDepositeDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepositeDate).Name = "lblDepositeDate";
		((ControlBase)this.lblDepositeDate).WrapText = false;
		resources.ApplyResources(this.chkReturned, "chkReturned");
		((System.Windows.Forms.Control)(object)this.chkReturned).Name = "chkReturned";
		resources.ApplyResources(this.lblReturnDate, "lblReturnDate");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.lblReturnDate).Appearance = (AppearanceBase)(object)val25;
		this.lblReturnDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReturnDate).Name = "lblReturnDate";
		((ControlBase)this.lblReturnDate).WrapText = false;
		resources.ApplyResources(this.dtpReturnDate, "dtpReturnDate");
		((UltraWinEditorMaskedControlBase)this.dtpReturnDate).AlwaysInEditMode = true;
		this.dtpReturnDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpReturnDate).Name = "dtpReturnDate";
		((EditorButtonControlBase)this.dtpReturnDate).ReadOnly = true;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		((EditorButtonControlBase)this.txtNotes).ReadOnly = true;
		resources.ApplyResources(this.lblDeliveryDate, "lblDeliveryDate");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance26");
		((ControlBase)this.lblDeliveryDate).Appearance = (AppearanceBase)(object)val26;
		this.lblDeliveryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDeliveryDate).Name = "lblDeliveryDate";
		((ControlBase)this.lblDeliveryDate).WrapText = false;
		resources.ApplyResources(this.dtpDeliveryDate, "dtpDeliveryDate");
		((UltraWinEditorMaskedControlBase)this.dtpDeliveryDate).AlwaysInEditMode = true;
		this.dtpDeliveryDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDeliveryDate).Name = "dtpDeliveryDate";
		((EditorButtonControlBase)this.dtpDeliveryDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpDeliveryDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.txtLoadType, "txtLoadType");
		((System.Windows.Forms.Control)(object)this.txtLoadType).Name = "txtLoadType";
		((EditorButtonControlBase)this.txtLoadType).ReadOnly = true;
		resources.ApplyResources(this.lblLoadType, "lblLoadType");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance27");
		((ControlBase)this.lblLoadType).Appearance = (AppearanceBase)(object)val27;
		this.lblLoadType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadType).Name = "lblLoadType";
		((ControlBase)this.lblLoadType).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance28");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val28;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.dtpExpectedReturnDate, "dtpExpectedReturnDate");
		((UltraWinEditorMaskedControlBase)this.dtpExpectedReturnDate).AlwaysInEditMode = true;
		this.dtpExpectedReturnDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpExpectedReturnDate).Name = "dtpExpectedReturnDate";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkReturned);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpReturnDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReturnDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExpectedReturnDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDepositeDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepositeDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDepositeAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepositeAmount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDONo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBLDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBLNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBLNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDONo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkInTallySheet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLoadType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDeliveryNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLoadedQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkReImport);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBLDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDeliveryNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGoodDesc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCustomsFees);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGoodDesc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadedQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomsFees);
		base.Name = "frmUpdateImportCargo";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomsFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadedQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGoodDesc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCustomsFees, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsignee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGoodDesc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBLDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkReImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsignee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLoadedQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDeliveryNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLoadType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkInTallySheet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDONo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBLNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBLNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBLDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDeliveryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDONo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepositeAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDepositeAmount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepositeDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDepositeDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExpectedReturnDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReturnDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpReturnDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkReturned, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkInTallySheet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDONo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBLNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDeliveryNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsignee).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadedQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGoodDesc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkReImport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCustomsFees).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBLDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepositeAmount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDepositeDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkReturned).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReturnDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDeliveryDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExpectedReturnDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
