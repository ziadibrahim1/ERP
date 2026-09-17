using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesTickets : frmDetails
{
	private DataTable dtServices;

	private DataTable dtOperationSubAccountSeaMen;

	private DataTable dtAirPorts;

	private ValueList vlOperationSubAccountSeaMen = new ValueList();

	private ValueList vlFromAirPorts = new ValueList();

	private ValueList vlToAirPorts = new ValueList();

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	public int AcceptedQty = 0;

	public int RefusedQty = 0;

	public int RefusedWithReasonQty = 0;

	private bool ReadOnly;

	private IContainer components = null;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	private UltraButton btnSelect;

	private UltraButton btnAddOnSigner;

	private UltraButton btnAddOffSigner;

	public frmOperationsServicesTickets()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الخدمة" : "Service Name");
	}

	public frmOperationsServicesTickets(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtOperationSubAccountSeaMen = SubAccounts.FillComboByOperationIDForMarineService(OperationID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		vlOperationSubAccountSeaMen.ValueListItems.Clear();
		for (int i = 0; i < dtOperationSubAccountSeaMen.Rows.Count; i++)
		{
			vlOperationSubAccountSeaMen.ValueListItems.Add(dtOperationSubAccountSeaMen.Rows[i]["SubAccountID"], dtOperationSubAccountSeaMen.Rows[i]["SubAccountName"].ToString());
		}
		dtAirPorts = AirPorts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlFromAirPorts.ValueListItems.Clear();
		vlToAirPorts.ValueListItems.Clear();
		for (int j = 0; j < dtAirPorts.Rows.Count; j++)
		{
			vlFromAirPorts.ValueListItems.Add(dtAirPorts.Rows[j]["AirPortID"], dtAirPorts.Rows[j]["AirPortName"].ToString());
			vlToAirPorts.ValueListItems.Add(dtAirPorts.Rows[j]["AirPortID"], dtAirPorts.Rows[j]["AirPortName"].ToString());
		}
		UltraButton obj = btnHeaderSearch;
		UltraButton obj2 = btnNext;
		bool flag = (((Control)(object)btnPriveous).Visible = false);
		bool visible = (((Control)(object)obj2).Visible = flag);
		((Control)(object)obj).Visible = visible;
		((EditorButtonControlBase)cboHeader).ReadOnly = true;
		((TextEditorControlBase)cboHeader).Value = ServiceID;
		if (ReadOnly)
		{
			UltraButton obj3 = btnCancel;
			UltraButton obj4 = btnSave;
			UltraButton obj5 = btnSaveAndClose;
			UltraButton obj6 = btnAddOffSigner;
			bool flag4 = (((Control)(object)btnAddOnSigner).Enabled = false);
			bool flag6 = (((Control)(object)obj6).Enabled = flag4);
			flag = (((Control)(object)obj5).Enabled = flag6);
			visible = (((Control)(object)obj4).Enabled = flag);
			((Control)(object)obj3).Enabled = visible;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceTicketID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "Sea Man");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlOperationSubAccountSeaMen;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FlightNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الرحلة" : "Flight No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FlightNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FlightNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Departure Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartureDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromAirPortID"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromAirPortID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromAirPortID"].ValueList = (IValueList)(object)vlFromAirPorts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromAirPortID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToAirPortID"].Header).Caption = (GlobalVariables.IsArabic ? "الى" : "To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToAirPortID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToAirPortID"].ValueList = (IValueList)(object)vlToAirPorts;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToAirPortID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الوصول" : "Arrival Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ArrivalDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RefNo"].Header).Caption = (GlobalVariables.IsArabic ? "Ref No" : "Ref No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RefNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RefNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			dtDetails = OperationsServicesTickets.SelectByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار البحار", "Please Select Sea Man");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["FlightNo"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاءإدخال رقم الرحلة", "Please Enter Flight No ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FlightNo"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار تاريخ الخروج", "Please Select Departure Date ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["FromAirPortID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار مطار الخروج", "Please Select From AirPort ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FromAirPortID"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ToAirPortID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار مطار الوصول", "Please Select To AirPort ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ToAirPortID"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ArrivalDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار تاريخ الوصول", "Please Select Arrival Date ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ArrivalDate"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"].Value != DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["ArrivalDate"].Value != DBNull.Value && Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"].Value) > Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["ArrivalDate"].Value))
			{
				GlobalVariables.InformationMB.Show("تاريخ الرحيل بعد تاريخ الوصول", "The Departure date is after Arrival date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["DepartureDate"];
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["FromAirPortID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["FromAirPortID"].Value.ToString() && ((UltraGridBase)ULGData).Rows[i].Cells["ToAirPortID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ToAirPortID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار نفس البحار على نفس الرحلة ", "Can not Duplicate The Same Person On the same Flight");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceTicketID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("MS_OperationsServicesTickets", "OperationServiceID", OperationServiceID, "OperationServiceTicketID", text, IsFromServer: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsServicesTickets.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			DisplayData();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	private void ULGData_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (ReadOnly || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "SubAccountID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnAddOnSigner_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CrewPassengersReport(OperationID, "1", IsFromServer: false);
		if (dtSearchResult.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in dtSearchResult.Rows)
		{
			DataRow dataRow2 = dtDetails.NewRow();
			dataRow2["OperationServiceTicketID"] = "-1";
			dataRow2["OperationID"] = OperationID;
			dataRow2["OperationServiceID"] = OperationServiceID;
			dataRow2["SubAccountID"] = row["SubAccountID"];
			dtDetails.Rows.Add(dataRow2.ItemArray);
		}
		((UltraGridBase)ULGData).UpdateData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
		HasChanges = true;
	}

	private void btnAddOffSigner_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.CrewPassengersReport(OperationID, "0", IsFromServer: false);
		if (dtSearchResult.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataRow row in dtSearchResult.Rows)
		{
			DataRow dataRow2 = dtDetails.NewRow();
			dataRow2["OperationServiceTicketID"] = "-1";
			dataRow2["OperationID"] = OperationID;
			dataRow2["OperationServiceID"] = OperationServiceID;
			dataRow2["SubAccountID"] = row["SubAccountID"];
			dtDetails.Rows.Add(dataRow2.ItemArray);
		}
		((UltraGridBase)ULGData).UpdateData();
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
		HasChanges = true;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
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
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Expected O, but got Unknown
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesTickets));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		this.btnSelect = new UltraButton();
		this.btnAddOnSigner = new UltraButton();
		this.btnAddOffSigner = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
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
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)base.ULGData).FilterRow += new FilterRowEventHandler(ULGData_FilterRow);
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this.btnSelect, "btnSelect");
		((System.Windows.Forms.Control)(object)this.btnSelect).Name = "btnSelect";
		resources.ApplyResources(this.btnAddOnSigner, "btnAddOnSigner");
		((System.Windows.Forms.Control)(object)this.btnAddOnSigner).Name = "btnAddOnSigner";
		((System.Windows.Forms.Control)(object)this.btnAddOnSigner).Click += new System.EventHandler(btnAddOnSigner_Click);
		resources.ApplyResources(this.btnAddOffSigner, "btnAddOffSigner");
		((System.Windows.Forms.Control)(object)this.btnAddOffSigner).Name = "btnAddOffSigner";
		((System.Windows.Forms.Control)(object)this.btnAddOffSigner).Click += new System.EventHandler(btnAddOffSigner_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddOffSigner);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddOnSigner);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelect);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Name = "frmOperationsServicesTickets";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelect, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddOnSigner, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddOffSigner, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
