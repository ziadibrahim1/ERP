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

public class frmOperationsServicesCrew : frmDetails
{
	private DataTable dtServices;

	private DataTable dtSubAccount;

	private ValueList vlSubAccount = new ValueList();

	private ValueList vlInOut = new ValueList();

	private ValueList vlPassengerType = new ValueList();

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private bool IsSignOn = false;

	public int Qty = 0;

	private bool ReadOnly;

	private DataRow drCurrentService;

	private bool NoInvoice;

	private IContainer components = null;

	public UltraButton btnNewSign;

	private UltraButton btnSelectVisas;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	private UltraLabel lblPassengersCounter;

	private UltraLabel lblPassengersCount;

	public frmOperationsServicesCrew()
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

	public frmOperationsServicesCrew(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, bool ISSIGNON, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		IsSignOn = ISSIGNON;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		drCurrentService = OperationsServices.Select(OperationServiceID, "-1", GlobalVariables.IsArabic ? "1" : "0").Rows[0];
		NoInvoice = ((drCurrentService["OperationInvoiceID"] == DBNull.Value) ? true : false);
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtSubAccount = SubAccounts.FillComboByOperationIDForMarineService(OperationID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		vlSubAccount.ValueListItems.Clear();
		for (int i = 0; i < dtSubAccount.Rows.Count; i++)
		{
			vlSubAccount.ValueListItems.Add(dtSubAccount.Rows[i]["SubAccountID"], dtSubAccount.Rows[i]["SubAccountName"].ToString());
		}
		vlInOut.ValueListItems.Clear();
		vlInOut.ValueListItems.Add((object)true, GlobalVariables.IsArabic ? "On" : "On");
		vlInOut.ValueListItems.Add((object)false, GlobalVariables.IsArabic ? "OFF" : "OFF");
		vlPassengerType.ValueListItems.Clear();
		vlPassengerType.ValueListItems.Add((object)"1", GlobalVariables.IsArabic ? "بحار" : "Seaman");
		vlPassengerType.ValueListItems.Add((object)"2", GlobalVariables.IsArabic ? "راكب" : "Passenger");
		vlPassengerType.ValueListItems.Add((object)"3", GlobalVariables.IsArabic ? "فني" : "Technician");
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
			UltraButton obj6 = btnSelectVisas;
			bool flag4 = (((Control)(object)btnNewSign).Enabled = false);
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
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceCrewPassengerID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsJoined"].Hidden = !IsSignOn;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].Hidden = !IsSignOn;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEyesScan"].Hidden = IsSignOn;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExitDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JoinedDate"].Hidden = !IsSignOn;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitedUpdateDate"].Hidden = !IsSignOn;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnterStampDate"].Hidden = false;
		if (IsSignOn)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExitDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnterStampDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsJoined"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JoinedDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitedUpdateDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		}
		else
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExitDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnterStampDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEyesScan"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccount;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "Sea Man");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].ValueList = (IValueList)(object)vlPassengerType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassengerType"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الراكب" : "Passenger Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].ValueList = (IValueList)(object)vlInOut;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].DefaultCellValue = DBNull.Value;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSignOn"].Header).Caption = (GlobalVariables.IsArabic ? "ON/OFF" : "ON/OFF");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExit"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "Exit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsExit"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsJoined"].Header).Caption = (GlobalVariables.IsArabic ? "الحاق" : "Joined");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsJoined"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].Header).Caption = (GlobalVariables.IsArabic ? "تحديث على النظام" : "Update On System");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsUnitedUpdate"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEyesScan"].Header).Caption = (GlobalVariables.IsArabic ? "بصمة عين" : "Eyes Scan");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEyesScan"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEnterStamp"].Header).Caption = (GlobalVariables.IsArabic ? "ختم دخول" : "Enter Stamp");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HasEnterStamp"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExitDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الخروج" : "Exit Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["JoinedDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الحاق" : "Joined Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UnitedUpdateDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التحديث على النظام" : "Updated Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EnterStampDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ ختم دخول" : "Enter Stamp Date");
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			if (IsSignOn)
			{
				dtDetails = OperationsServicesCrewPassengers.SelectSignOnByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
			else
			{
				dtDetails = OperationsServicesCrewPassengers.SelectSignOffByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
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
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار إسم البحار", "Cannot Duplicate The Same SeaMan");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void SaveData()
	{
		if (NoInvoice)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				string text = ",";
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
					((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceCrewPassengerID"].Value.ToString() + ",";
				}
				Main.DeleteForUpdate("MS_OperationsServicesCrewPassengers", "OperationServiceID", OperationServiceID, "OperationServiceCrewPassengerID", text);
				if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
				{
					OperationsServicesCrewPassengers.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
				}
				OperationsServices.UpdateTotals(OperationServiceID, ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString());
				DisplayData();
				Main.EndBulkTrans(FromServer: false);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
				SaveError = true;
				return;
			}
		}
		GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
	}

	private void btnNewSign_Click(object sender, EventArgs e)
	{
		if (NoInvoice)
		{
			frmInsertSeaManCrew frmInsertSeaManCrew2 = new frmInsertSeaManCrew(OperationID, OperationServiceID, IsSignOn);
			frmInsertSeaManCrew2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			((Control)(object)frmInsertSeaManCrew2.lblTitle).Text = (GlobalVariables.IsArabic ? "طاقم الباخرة" : "Vessel Crew");
			frmInsertSeaManCrew2.Location = new Point(0, 0);
			frmInsertSeaManCrew2.ShowDialog();
			dtSubAccount = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSubAccount.ValueListItems.Clear();
			for (int i = 0; i < dtSubAccount.Rows.Count; i++)
			{
				vlSubAccount.ValueListItems.Add(dtSubAccount.Rows[i]["SubAccountID"], dtSubAccount.Rows[i]["SubAccountName"].ToString());
			}
			DisplayData();
			OperationsServices.UpdateTotals(OperationServiceID, ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString());
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void btnSelectVisas_Click(object sender, EventArgs e)
	{
		if (NoInvoice)
		{
			dtSearchResult = SearchFunctions.VisasBySignOnOffReport(OperationID, IsSignOn ? "1" : "0", IsFromServer: false);
			if (dtSearchResult.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dtSearchResult.Rows)
			{
				DataRow dataRow2 = dtDetails.NewRow();
				dataRow2["OperationServiceCrewPassengerID"] = -1;
				dataRow2["OperationID"] = OperationID;
				dataRow2["OperationServiceID"] = OperationServiceID;
				dataRow2["OperationServiceVisaID"] = row["OperationServiceVisaID"];
				dataRow2["SubAccountID"] = row["SubAccountID"];
				dataRow2["PassengerType"] = row["PassengerType"];
				dataRow2["IsSignOn"] = IsSignOn;
				dataRow2["IsExit"] = false;
				dataRow2["IsJoined"] = false;
				dataRow2["IsUnitedUpdate"] = false;
				dataRow2["HasEyesScan"] = false;
				dataRow2["HasEnterStamp"] = false;
				dtDetails.Rows.Add(dataRow2);
				((UltraGridBase)ULGData).UpdateData();
				((Control)(object)btnSave).Enabled = true;
				((Control)(object)btnSaveAndClose).Enabled = true;
				((Control)(object)btnCancel).Enabled = true;
				HasChanges = true;
			}
			InitGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
		}
	}

	private void ULGData_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = dtDetails.Select("PassengerType =1 ").Length.ToString();
		((Control)(object)lblPassengersCounter).Text = dtDetails.Select("PassengerType <> 1").Length.ToString();
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Expected O, but got Unknown
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesCrew));
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
		this.btnNewSign = new UltraButton();
		this.btnSelectVisas = new UltraButton();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		this.lblPassengersCounter = new UltraLabel();
		this.lblPassengersCount = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSave, "btnSave");
		resources.ApplyResources(base.ULGData, "ULGData");
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
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)base.ULGData).FilterRow += new FilterRowEventHandler(ULGData_FilterRow);
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnSaveAndClose, "btnSaveAndClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnNewSign, "btnNewSign");
		((AppearanceBase)val10).Image = resources.GetObject("appearance10.Image");
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnNewSign).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnNewSign).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewSign).Name = "btnNewSign";
		((System.Windows.Forms.Control)(object)this.btnNewSign).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnNewSign).Click += new System.EventHandler(btnNewSign_Click);
		resources.ApplyResources(this.btnSelectVisas, "btnSelectVisas");
		((System.Windows.Forms.Control)(object)this.btnSelectVisas).Name = "btnSelectVisas";
		((System.Windows.Forms.Control)(object)this.btnSelectVisas).Click += new System.EventHandler(btnSelectVisas_Click);
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this.lblPassengersCounter, "lblPassengersCounter");
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblPassengersCounter).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.lblPassengersCounter).Name = "lblPassengersCounter";
		resources.ApplyResources(this.lblPassengersCount, "lblPassengersCount");
		this.lblPassengersCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassengersCount).Name = "lblPassengersCount";
		((ControlBase)this.lblPassengersCount).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengersCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassengersCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewSign);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectVisas);
		base.Name = "frmOperationsServicesCrew";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectVisas, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewSign, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassengersCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassengersCounter, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
