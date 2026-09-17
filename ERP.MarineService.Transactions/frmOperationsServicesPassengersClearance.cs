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

public class frmOperationsServicesPassengersClearance : frmDetails
{
	private DataTable dtServices;

	private DataTable dtOperationSubAccountSeaMen;

	private DataTable dtNationalities;

	private DataTable dtTechnicians;

	private ValueList vlOperationSubAccountSeaMen = new ValueList();

	private ValueList vlTechnicians = new ValueList();

	private ValueList vlNationalities = new ValueList();

	private ValueList vlRanks = new ValueList();

	private DataRow drCurrentService;

	private bool NoInvoice;

	private bool Technician = false;

	private bool IsInward = false;

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private bool ReadOnly;

	private IContainer components = null;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	private UltraButton btnSelect;

	public frmOperationsServicesPassengersClearance()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم الخدمة" : "Service Name");
	}

	public frmOperationsServicesPassengersClearance(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		ReadOnly = READONLY;
		string text = dtServices.Select("ServiceID = " + ServiceID)[0]["ServiceTypeID"].ToString();
		if (text == "30" || text == "31")
		{
			Technician = true;
			if (text == "30")
			{
				IsInward = true;
			}
		}
	}

	public override void PrepareData()
	{
		drCurrentService = OperationsServices.Select(OperationServiceID, "-1", GlobalVariables.IsArabic ? "1" : "0").Rows[0];
		NoInvoice = ((drCurrentService["OperationInvoiceID"] == DBNull.Value) ? true : false);
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtOperationSubAccountSeaMen = SubAccounts.FillComboByOperationIDForMarineService(OperationID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		vlOperationSubAccountSeaMen.ValueListItems.Clear();
		for (int i = 0; i < dtOperationSubAccountSeaMen.Rows.Count; i++)
		{
			vlOperationSubAccountSeaMen.ValueListItems.Add(dtOperationSubAccountSeaMen.Rows[i]["SubAccountID"], dtOperationSubAccountSeaMen.Rows[i]["SubAccountName"].ToString());
		}
		dtTechnicians = Technicians.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTechnicians.ValueListItems.Clear();
		for (int j = 0; j < dtTechnicians.Rows.Count; j++)
		{
			vlTechnicians.ValueListItems.Add(dtTechnicians.Rows[j]["TechnicianID"], dtTechnicians.Rows[j]["TechnicianName"].ToString());
		}
		dtNationalities = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlNationalities.ValueListItems.Clear();
		for (int k = 0; k < dtNationalities.Rows.Count; k++)
		{
			vlNationalities.ValueListItems.Add(dtNationalities.Rows[k]["NationalityID"], dtNationalities.Rows[k]["NationalityName"].ToString());
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
			flag = (((Control)(object)btnSaveAndClose).Enabled = false);
			visible = (((Control)(object)obj4).Enabled = flag);
			((Control)(object)obj3).Enabled = visible;
		}
		((Control)(object)btnSelect).Visible = IsInward;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServicePassengerClearanceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "Sea Man");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = Technician;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlOperationSubAccountSeaMen;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianID"].Header).Caption = (GlobalVariables.IsArabic ? "الفني" : "Technician");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianID"].Hidden = !Technician;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianID"].ValueList = (IValueList)(object)vlTechnicians;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TechnicianID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الاسم" : "Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityID"].ValueList = (IValueList)(object)vlNationalities;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NationalityID"].Header).Caption = (GlobalVariables.IsArabic ? "الجنسية" : "Nationality");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PassportNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجواز" : "Passport No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الهوية" : "ID No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IDIssueDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ إصدار الهوية" : "ID Issue Date");
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			dtDetails = OperationsServicesPassengersClearance.SelectByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		if (!NoInvoice && Convert.ToInt32(drCurrentService["Qty"]) != ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن تعديل هذه الخدمه لوجود فاتورة" : "Cannot Update This Serivce because it has an Invoice");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if ((((UltraGridBase)ULGData).Rows[i].Cells["NameEn"].Value == DBNull.Value || ((UltraGridBase)ULGData).Rows[i].Cells["NameEn"].Value.ToString() == "") && ((UltraGridBase)ULGData).Rows[i].Cells["TechnicianID"].Value == null)
			{
				GlobalVariables.InformationMB.Show("برجاء كتابة الاسم", "Please write the Name");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["NameEn"]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["NameEn"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["NameEn"].Value.ToString() && (((UltraGridBase)ULGData).Rows[i].Cells["NameEn"].Value != DBNull.Value || ((UltraGridBase)ULGData).Rows[i].Cells["NameEn"].Value.ToString() != ""))
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار نفس الإسم ", "Cannot Duplicate The Same Name");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["NameEn"];
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
					((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
					((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
					((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
					((UltraGridBase)ULGData).Rows[i].Cells["IsInward"].Value = IsInward;
					text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServicePassengerClearanceID"].Value.ToString() + ",";
				}
				Main.SyncDeleteForUpdate("MS_OperationsServicesPassengersClearance", "OperationServiceID", OperationServiceID, "OperationServicePassengerClearanceID", text, IsFromServer: false);
				if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
				{
					OperationsServicesPassengersClearance.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
				}
				OperationsServices.UpdateTotals(OperationServiceID, ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString());
				Main.EndBulkTrans(FromServer: false);
				DisplayData();
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

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "SubAccountID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			string text = ((vlOperationSubAccountSeaMen.SelectedItem != null) ? dtOperationSubAccountSeaMen.Rows[vlOperationSubAccountSeaMen.SelectedIndex]["NameEn"].ToString() : "");
			if (!text.Equals(""))
			{
				e.Cell.Row.Cells["NameEn"].Value = text;
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "TechnicianID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			string text2 = ((vlTechnicians.SelectedItem != null) ? dtTechnicians.Rows[vlTechnicians.SelectedIndex]["TechnicianName"].ToString() : "");
			string value = ((vlTechnicians.SelectedItem != null) ? dtTechnicians.Rows[vlTechnicians.SelectedIndex]["PassportNo"].ToString() : "");
			string text3 = ((vlTechnicians.SelectedItem != null) ? dtTechnicians.Rows[vlTechnicians.SelectedIndex]["NationalityID"].ToString() : "");
			string value2 = ((vlTechnicians.SelectedItem != null) ? dtTechnicians.Rows[vlTechnicians.SelectedIndex]["IDNo"].ToString() : "");
			string text4 = ((vlTechnicians.SelectedItem != null) ? dtTechnicians.Rows[vlTechnicians.SelectedIndex]["IDIssueDate"].ToString() : "");
			if (!text2.Equals(""))
			{
				e.Cell.Row.Cells["NameEn"].Value = text2;
			}
			if (!text3.Equals(""))
			{
				e.Cell.Row.Cells["NationalityID"].Value = text3;
			}
			if (!text4.Equals(""))
			{
				e.Cell.Row.Cells["IDIssueDate"].Value = text4;
			}
			e.Cell.Row.Cells["PassportNo"].Value = value;
			e.Cell.Row.Cells["IDNo"].Value = value2;
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (ReadOnly)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void btnSelect_Click(object sender, EventArgs e)
	{
		if (NoInvoice)
		{
			dtSearchResult = SearchFunctions.TechniciansInwardReport(OperationID, IsFromServer: false);
			if (dtSearchResult.Rows.Count <= 0)
			{
				return;
			}
			foreach (DataRow row in dtSearchResult.Rows)
			{
				DataRow dataRow2 = dtDetails.NewRow();
				dataRow2["OperationServicePassengerClearanceID"] = -1;
				dataRow2["OperationID"] = OperationID;
				dataRow2["OperationServiceID"] = OperationServiceID;
				dataRow2["TechnicianID"] = row["TechnicianID"];
				dataRow2["NationalityID"] = row["NationalityID"];
				dataRow2["PassportNo"] = row["PassportNo"];
				dataRow2["IDNo"] = row["IDNo"];
				dataRow2["IDIssueDate"] = row["IDIssueDate"];
				dataRow2["Notes"] = row["Notes"];
				dataRow2["IsInward"] = IsInward;
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
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Expected O, but got Unknown
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Expected O, but got Unknown
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesPassengersClearance));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		this.btnSelect = new UltraButton();
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
		base.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
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
		((System.Windows.Forms.Control)(object)this.btnSelect).Click += new System.EventHandler(btnSelect_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelect);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Name = "frmOperationsServicesPassengersClearance";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
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
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
