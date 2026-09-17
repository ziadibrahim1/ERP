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

public class frmUpdateExportCargo : frmBase
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

	public UltraLabel lblHistory;

	private UltraLabel lblEDNo;

	private UltraLabel lblContainerNo;

	private UltraLabel lblContainerType;

	private UltraLabel lblInvNo;

	private UltraLabel lblConsignee;

	private UltraLabel lblQty;

	private UltraLabel lblLoadedQty;

	private UltraLabel lblWeight;

	private UltraLabel lblUnit;

	private UltraLabel lblGoodDesc;

	private UltraTextEditor txtEDNo;

	private UltraTextEditor txtContainerNo;

	private UltraTextEditor txtContainerType;

	private UltraTextEditor txtInvNo;

	private UltraTextEditor txtConsignee;

	private UltraTextEditor txtQty;

	private UltraTextEditor txtWeight;

	private UltraTextEditor txtLoadedQty;

	private UltraTextEditor txtUnit;

	private UltraTextEditor txtGoodDesc;

	private UltraCheckEditor chkReExport;

	private UltraCheckEditor chkReturned;

	private UltraLabel lblReturnDate;

	private UltraDateTimeEditor dtpReturnDate;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtLoadType;

	private UltraLabel lblLoadType;

	private UltraTextEditor txtShipperName;

	private UltraLabel lblShipperName;

	public frmUpdateExportCargo()
	{
		InitializeComponent();
	}

	public frmUpdateExportCargo(string OperationServiceCargoID)
		: this()
	{
		RowID = OperationServiceCargoID;
		TableName = "MS_OperationsServicesCargos";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UltraDateTimeEditor obj = dtpStartDate;
		string maskInput = (dtpReturnDate.MaskInput = "dd/mm/yyyy hh:mm tt");
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
			((Control)(object)txtEDNo).Text = drMaster["ExportDeclarationNo"].ToString();
			((Control)(object)txtContainerNo).Text = drMaster["ContainerNo"].ToString();
			((Control)(object)txtContainerType).Text = drMaster["ContainerType"].ToString();
			((Control)(object)txtShipperName).Text = drMaster["ShipperName"].ToString();
			((Control)(object)txtInvNo).Text = drMaster["ShipperInvoiceNo"].ToString();
			((Control)(object)txtConsignee).Text = drMaster["ConsigneeName"].ToString();
			((Control)(object)txtQty).Text = decimal.Parse(drMaster["Qty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLoadedQty).Text = decimal.Parse(drMaster["LoadedQty"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtLoadType).Text = drMaster["LoadType"].ToString();
			((Control)(object)txtWeight).Text = decimal.Parse(drMaster["Weight"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtUnit).Text = drMaster["Unit"].ToString();
			((Control)(object)txtGoodDesc).Text = drMaster["GoodsDescription"].ToString();
			((UltraToggleEditorBase)chkReExport).Checked = bool.Parse(drMaster["IsReExport"].ToString());
			((UltraToggleEditorBase)chkReturned).Checked = bool.Parse(drMaster["IsReturned"].ToString());
			dtpReturnDate.Value = drMaster["ReturnDate"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
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
			OperationsServicesCargos.Insert_Update(RowID, drMaster["OperationServiceID"].ToString(), drMaster["OperationID"].ToString(), (drMaster["ContainerTypeID"].ToString() == "") ? "Null" : drMaster["ContainerTypeID"].ToString(), (drMaster["ContainerNo"].ToString() == "") ? "Null" : drMaster["ContainerNo"].ToString(), (drMaster["IsExportDeclaration"].ToString() == "") ? "Null" : drMaster["IsExportDeclaration"].ToString(), ((Control)(object)txtEDNo).Text, (drMaster["TransitNo"].ToString() == "") ? "Null" : drMaster["TransitNo"].ToString(), (drMaster["BillNo"].ToString() == "") ? "Null" : drMaster["BillNo"].ToString(), (drMaster["BillDate"].ToString() == "") ? "Null" : drMaster["BillDate"].ToString(), (drMaster["DeliveryNo"].ToString() == "") ? "Null" : drMaster["DeliveryNo"].ToString(), (drMaster["DeliveryDate"].ToString() == "") ? "Null" : drMaster["DeliveryDate"].ToString(), (drMaster["ShipperName"].ToString() == "") ? "Null" : drMaster["ShipperName"].ToString(), (drMaster["ShipperImporterCode"].ToString() == "") ? "Null" : drMaster["ShipperImporterCode"].ToString(), (drMaster["ShipperInvoiceNo"].ToString() == "") ? "Null" : drMaster["ShipperInvoiceNo"].ToString(), (drMaster["ConsigneeName"].ToString() == "") ? "Null" : drMaster["ConsigneeName"].ToString(), (drMaster["ConsigneeShip"].ToString() == "") ? "Null" : drMaster["ConsigneeShip"].ToString(), (drMaster["NotifyAddress"].ToString() == "") ? "Null" : drMaster["NotifyAddress"].ToString(), (drMaster["IsReExport"].ToString() == "") ? "Null" : drMaster["IsReExport"].ToString(), (drMaster["IsReImport"].ToString() == "") ? "Null" : drMaster["IsReImport"].ToString(), (drMaster["LoadingDate"].ToString() == "") ? "Null" : drMaster["LoadingDate"].ToString(), (drMaster["LoadTypeID"].ToString() == "") ? "Null" : drMaster["LoadTypeID"].ToString(), (drMaster["Qty"].ToString() == "") ? "Null" : drMaster["Qty"].ToString(), ((Control)(object)txtLoadedQty).Text, (drMaster["Weight"].ToString() == "") ? "Null" : drMaster["Weight"].ToString(), (drMaster["UnitID"].ToString() == "") ? "Null" : drMaster["UnitID"].ToString(), (drMaster["GoodsDescription"].ToString() == "") ? "Null" : drMaster["GoodsDescription"].ToString(), (drMaster["InTallySheet"].ToString() == "") ? "Null" : drMaster["InTallySheet"].ToString(), (drMaster["CustomFees"].ToString() == "") ? "Null" : drMaster["CustomFees"].ToString(), (drMaster["DepositAmount"].ToString() == "") ? "Null" : drMaster["DepositAmount"].ToString(), (drMaster["DepositDate"].ToString() == "") ? "Null" : drMaster["DepositDate"].ToString(), (drMaster["ExpectedReturnDate"].ToString() == "") ? "Null" : drMaster["ExpectedReturnDate"].ToString(), ((UltraToggleEditorBase)chkReturned).Checked ? "1" : "0", (drMaster["ReturnDate"].ToString() == "") ? "Null" : drMaster["ReturnDate"].ToString(), (drMaster["Notes"].ToString() == "") ? "Null" : drMaster["Notes"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
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
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateExportCargo));
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
		this.lblHistory = new UltraLabel();
		this.lblEDNo = new UltraLabel();
		this.lblContainerNo = new UltraLabel();
		this.lblContainerType = new UltraLabel();
		this.lblInvNo = new UltraLabel();
		this.lblConsignee = new UltraLabel();
		this.lblQty = new UltraLabel();
		this.lblLoadedQty = new UltraLabel();
		this.lblWeight = new UltraLabel();
		this.lblUnit = new UltraLabel();
		this.lblGoodDesc = new UltraLabel();
		this.txtEDNo = new UltraTextEditor();
		this.txtContainerNo = new UltraTextEditor();
		this.txtContainerType = new UltraTextEditor();
		this.txtInvNo = new UltraTextEditor();
		this.txtConsignee = new UltraTextEditor();
		this.txtQty = new UltraTextEditor();
		this.txtWeight = new UltraTextEditor();
		this.txtLoadedQty = new UltraTextEditor();
		this.txtUnit = new UltraTextEditor();
		this.txtGoodDesc = new UltraTextEditor();
		this.chkReExport = new UltraCheckEditor();
		this.chkReturned = new UltraCheckEditor();
		this.lblReturnDate = new UltraLabel();
		this.dtpReturnDate = new UltraDateTimeEditor();
		this.txtNotes = new UltraTextEditor();
		this.txtLoadType = new UltraTextEditor();
		this.lblLoadType = new UltraLabel();
		this.txtShipperName = new UltraTextEditor();
		this.lblShipperName = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsignee).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadedQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnit).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGoodDesc).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkReExport).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkReturned).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReturnDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShipperName).BeginInit();
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
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.lblEDNo, "lblEDNo");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblEDNo).Appearance = (AppearanceBase)(object)val9;
		this.lblEDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEDNo).Name = "lblEDNo";
		((ControlBase)this.lblEDNo).WrapText = false;
		resources.ApplyResources(this.lblContainerNo, "lblContainerNo");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblContainerNo).Appearance = (AppearanceBase)(object)val10;
		this.lblContainerNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainerNo).Name = "lblContainerNo";
		((ControlBase)this.lblContainerNo).WrapText = false;
		resources.ApplyResources(this.lblContainerType, "lblContainerType");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblContainerType).Appearance = (AppearanceBase)(object)val11;
		this.lblContainerType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblContainerType).Name = "lblContainerType";
		((ControlBase)this.lblContainerType).WrapText = false;
		resources.ApplyResources(this.lblInvNo, "lblInvNo");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblInvNo).Appearance = (AppearanceBase)(object)val12;
		this.lblInvNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvNo).Name = "lblInvNo";
		((ControlBase)this.lblInvNo).WrapText = false;
		resources.ApplyResources(this.lblConsignee, "lblConsignee");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblConsignee).Appearance = (AppearanceBase)(object)val13;
		this.lblConsignee.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblConsignee).Name = "lblConsignee";
		((ControlBase)this.lblConsignee).WrapText = false;
		resources.ApplyResources(this.lblQty, "lblQty");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblQty).Appearance = (AppearanceBase)(object)val14;
		this.lblQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQty).Name = "lblQty";
		((ControlBase)this.lblQty).WrapText = false;
		resources.ApplyResources(this.lblLoadedQty, "lblLoadedQty");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblLoadedQty).Appearance = (AppearanceBase)(object)val15;
		this.lblLoadedQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadedQty).Name = "lblLoadedQty";
		((ControlBase)this.lblLoadedQty).WrapText = false;
		resources.ApplyResources(this.lblWeight, "lblWeight");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblWeight).Appearance = (AppearanceBase)(object)val16;
		this.lblWeight.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWeight).Name = "lblWeight";
		((ControlBase)this.lblWeight).WrapText = false;
		resources.ApplyResources(this.lblUnit, "lblUnit");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblUnit).Appearance = (AppearanceBase)(object)val17;
		this.lblUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnit).Name = "lblUnit";
		((ControlBase)this.lblUnit).WrapText = false;
		resources.ApplyResources(this.lblGoodDesc, "lblGoodDesc");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblGoodDesc).Appearance = (AppearanceBase)(object)val18;
		this.lblGoodDesc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGoodDesc).Name = "lblGoodDesc";
		((ControlBase)this.lblGoodDesc).WrapText = false;
		resources.ApplyResources(this.txtEDNo, "txtEDNo");
		((System.Windows.Forms.Control)(object)this.txtEDNo).Name = "txtEDNo";
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
		resources.ApplyResources(this.chkReExport, "chkReExport");
		((System.Windows.Forms.Control)(object)this.chkReExport).Name = "chkReExport";
		resources.ApplyResources(this.chkReturned, "chkReturned");
		((System.Windows.Forms.Control)(object)this.chkReturned).Name = "chkReturned";
		resources.ApplyResources(this.lblReturnDate, "lblReturnDate");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblReturnDate).Appearance = (AppearanceBase)(object)val19;
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
		resources.ApplyResources(this.txtLoadType, "txtLoadType");
		((System.Windows.Forms.Control)(object)this.txtLoadType).Name = "txtLoadType";
		((EditorButtonControlBase)this.txtLoadType).ReadOnly = true;
		resources.ApplyResources(this.lblLoadType, "lblLoadType");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.lblLoadType).Appearance = (AppearanceBase)(object)val20;
		this.lblLoadType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLoadType).Name = "lblLoadType";
		((ControlBase)this.lblLoadType).WrapText = false;
		resources.ApplyResources(this.txtShipperName, "txtShipperName");
		((System.Windows.Forms.Control)(object)this.txtShipperName).Name = "txtShipperName";
		((EditorButtonControlBase)this.txtShipperName).ReadOnly = true;
		resources.ApplyResources(this.lblShipperName, "lblShipperName");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance21");
		((ControlBase)this.lblShipperName).Appearance = (AppearanceBase)(object)val21;
		this.lblShipperName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShipperName).Name = "lblShipperName";
		((ControlBase)this.lblShipperName).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkReturned);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpReturnDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReturnDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLoadType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtWeight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLoadedQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShipperName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShipperName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblConsignee);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtContainerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInvNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtGoodDesc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainerNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLoadedQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblContainerType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblGoodDesc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkReExport);
		base.Name = "frmUpdateExportCargo";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkReExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblGoodDesc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadedQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblContainerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtGoodDesc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInvNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainerType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblConsignee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtContainerNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShipperName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShipperName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtConsignee, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLoadedQty, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLoadType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReturnDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpReturnDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkReturned, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblWeight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLoadType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtContainerType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInvNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtConsignee).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtWeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadedQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUnit).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGoodDesc).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkReExport).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkReturned).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpReturnDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLoadType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShipperName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
