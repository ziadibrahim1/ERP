using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Attendance.Transactions;

public class frmEmployeesVacations : frmDetails
{
	private DataTable dtEmployees;

	private DataTable dtVacationTypes;

	private DataTable dtEmployeeBalances;

	private ValueList vlVacationType = new ValueList();

	private ValueList vlVacationTypeBalance = new ValueList();

	private IContainer components = null;

	protected internal UltraGrid ULGDataVacationBalance;

	private UltraComboEditor cboEmployeeCode;

	public frmEmployeesVacations()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
	}

	public override void PrepareData()
	{
		DataTable dataTable = Users.Select(GlobalVariables.UserID, "-1", "0", IsFromServer: true);
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtEmployees, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboEmployeeCode, dtEmployees, "SubAccountID", "EmployeeNo");
		dtVacationTypes = VacationTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlVacationType.ValueListItems.Clear();
		vlVacationTypeBalance.ValueListItems.Clear();
		for (int i = 0; i < dtVacationTypes.Rows.Count; i++)
		{
			vlVacationType.ValueListItems.Add(dtVacationTypes.Rows[i]["VacationtypeID"], dtVacationTypes.Rows[i]["VacationName"].ToString());
			vlVacationTypeBalance.ValueListItems.Add(dtVacationTypes.Rows[i]["VacationtypeID"], dtVacationTypes.Rows[i]["VacationName"].ToString());
		}
		if (!ViewAllEmployees)
		{
			((TextEditorControlBase)cboHeader).Value = dataTable.Rows[0]["SubAccountID"];
			((EditorButtonControlBase)cboHeader).ReadOnly = true;
			((Control)(object)btnNext).Visible = false;
			((Control)(object)btnPriveous).Visible = false;
			((Control)(object)btnHeaderSearch).Visible = false;
			DisplayData();
		}
		else
		{
			dtDetails = EmployeesVacations.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtEmployeeBalances = EmployeesVacationBalances.SelectCurrentBalances("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGrid();
		}
	}

	public override void InitGrid()
	{
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataVacationBalance);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataVacationBalance).DataSource = dtEmployeeBalances;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeVacationID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeVacationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الطلب" : "Order No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeVacationNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeVacationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الاجازة" : "Vacation Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationTypeID"].ValueList = (IValueList)(object)vlVacationType;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationFromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من تاريخ" : "From Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationFromDate"].Hidden = false;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationFromDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationFromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationToDate"].Header).Caption = (GlobalVariables.IsArabic ? "الى تاريخ" : "To Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationToDate"].Hidden = false;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationToDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VacationToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Refused"].Header).Caption = (GlobalVariables.IsArabic ? "مرفوض" : "Refused");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Refused"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Refused"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Refused"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "معتمد" : "Approved");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].DefaultCellValue = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["VacationTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع الاجازة" : "Vacation Type");
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["VacationTypeID"].Hidden = false;
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["VacationTypeID"].ValueList = (IValueList)(object)vlVacationTypeBalance;
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["VacationTypeID"].Width = (int)((double)((Control)(object)ULGDataVacationBalance).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["YearName"].Header).Caption = (GlobalVariables.IsArabic ? "سنة" : "Year");
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["YearName"].Hidden = false;
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["YearName"].Width = (int)((double)((Control)(object)ULGDataVacationBalance).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["CurrentBalance"].Header).Caption = (GlobalVariables.IsArabic ? "رصيد" : "Balance");
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["CurrentBalance"].Hidden = false;
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["CurrentBalance"].Width = (int)((double)((Control)(object)ULGDataVacationBalance).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["StartBalance"].Header).Caption = (GlobalVariables.IsArabic ? "رصيد افتتاحى" : "Start Balance");
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["StartBalance"].Hidden = false;
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["StartBalance"].Width = (int)((double)((Control)(object)ULGDataVacationBalance).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["OnDemand"].Header).Caption = (GlobalVariables.IsArabic ? "تحت الطلب" : "On Demand");
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["OnDemand"].Hidden = false;
		((UltraGridBase)ULGDataVacationBalance).DisplayLayout.Bands[0].Columns["OnDemand"].Width = (int)((double)((Control)(object)ULGDataVacationBalance).Width * 0.2);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboEmployeeCode).ValueChanged -= cboEmployeeCode_ValueChanged;
			cboEmployeeCode.SelectedIndex = cboHeader.SelectedIndex;
			((TextEditorControlBase)cboEmployeeCode).ValueChanged += cboEmployeeCode_ValueChanged;
			dtDetails = EmployeesVacations.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			EmployeesVacationBalances.InsertBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), IsFromServer: true);
			dtEmployeeBalances = EmployeesVacationBalances.SelectCurrentBalances(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["VacationTypeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار نوع الاجازة", "Please Select Vacation Type");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["VacationTypeID"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VacationFromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار من التاريخ", "Please Select From Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["VacationFromDate"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["VacationToDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الى التاريخ", "Please Select To Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["VacationToDate"]).Selected = true;
				return false;
			}
			if (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationFromDate"].Value.ToString()) > DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationToDate"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("رجاءا اختر فتره صحيحه", "From Date Is After The To Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (j != i && !bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Refused"].Value.ToString()) && !bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Refused"].Value.ToString()) && ((DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationFromDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["VacationFromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationFromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["VacationToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["VacationFromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationToDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["VacationToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationFromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["VacationFromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["VacationToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["VacationToDate"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذا التاريخ واقع فى فترة من قبل", "this Date in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[j]).Selected = true;
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataVacationBalance).Rows).Count; k++)
		{
			DataRow[] array = dtDetails.Select(string.Concat("Refused = 0 and Approved = 0 and VacationTypeID = ", ((UltraGridBase)ULGDataVacationBalance).Rows[k].Cells["VacationTypeID"].Value, " and VacationToDate >= '01-01-", ((UltraGridBase)ULGDataVacationBalance).Rows[k].Cells["YearName"].Value, "' and VacationFromDate <= '12-31-", ((UltraGridBase)ULGDataVacationBalance).Rows[k].Cells["YearName"].Value, " 23:59:59'"));
			int num = 0;
			for (int l = 0; l < array.Length; l++)
			{
				num = num + (DateTime.Parse(array[l]["VacationToDate"].ToString()) - DateTime.Parse(array[l]["VacationFromDate"].ToString())).Days + 1;
				if ((decimal)num > decimal.Parse(((UltraGridBase)ULGDataVacationBalance).Rows[k].Cells["CurrentBalance"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show(" رصيد الاجازات من الاجازة  \n " + ((UltraGridBase)ULGDataVacationBalance).Rows[k].Cells["VacationTypeID"].Text + " غير كافى ", "  Vacation Balance From " + ((UltraGridBase)ULGDataVacationBalance).Rows[k].Cells["VacationTypeID"].Text + " Not Enough ");
					return false;
				}
			}
		}
		string text = DateTime.Parse(((UltraGridBase)ULGData).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1].Cells["VacationToDate"].Value.ToString()).Year.ToString();
		if (dtDetails.Select("Refused = 0 and Approved = 0 and VacationTypeID = 1 and VacationToDate >= '01-01-" + text + "'").GetLength(0) > 0)
		{
			int month = DateTime.Parse(((UltraGridBase)ULGData).Rows[((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - 1].Cells["VacationToDate"].Value.ToString()).Month;
			double num2 = Convert.ToDouble(dtEmployeeBalances.Select("VacationTypeID = 1 and yearName = " + text)[0]["StartBalance"]) / 12.0;
			double num3 = (double)month * num2;
			DataRow[] array2 = dtDetails.Select("Refused = 0  and VacationTypeID = 1 and VacationToDate >= '01-01-" + text + "'");
			int num4 = 0;
			for (int m = 0; m < array2.GetLength(0); m++)
			{
				num4 = num4 + (DateTime.Parse(array2[m]["VacationToDate"].ToString()).Day - DateTime.Parse(array2[m]["VacationFromDate"].ToString()).Day) + 1;
			}
			if (num3 < (double)num4)
			{
				GlobalVariables.QuestionMB.Show("مدة الأجازة الإعتيادية اكبر من الرصيد الشهري", "Custom Vacation Period Greater than the Monthly Balance");
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					return false;
				}
			}
		}
		return base.ValidateData();
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["EmployeeVacationID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("dbo.HR_EmployeesVacations", "SubAccountID", ((TextEditorControlBase)cboHeader).Value.ToString(), "EmployeeVacationID", " And (Year(VacationFromDate) In (Select YearName From HR_Years Where Closed=0)) And Approved=0 And Refused=0 ", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				EmployeesVacations.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			DisplayData();
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if ((((UltraGridBase)ULGData).ActiveRow != null && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Approved"].Value.ToString())) || bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Refused"].Value.ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Refused" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Approved" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EmployeeVacationNo")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGDataVacationBalance_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGDataVacationBalance).ActiveRow).Selected = true;
	}

	private void cboEmployeeCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboEmployeeCode.SelectedIndex > -1)
		{
			cboHeader.SelectedIndex = cboEmployeeCode.SelectedIndex;
		}
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		bool flag = false;
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (bool.Parse(e.Rows[i].Cells["Approved"].Value.ToString()) || bool.Parse(e.Rows[i].Cells["Refused"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف الاجازة لانها معتمدة", "Cannot Delete Vacation Because It Is Approved");
				flag = true;
			}
		}
		if (flag)
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			base.ULGData_BeforeRowsDeleted(sender, e);
		}
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboHeader).Value = num;
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
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
		this.ULGDataVacationBalance = new UltraGrid();
		this.cboEmployeeCode = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataVacationBalance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).Location = new System.Drawing.Point(8, 221);
		((System.Windows.Forms.Control)(object)base.ULGData).Size = new System.Drawing.Size(985, 250);
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(763, 190);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(224, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(555, 194);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataVacationBalance).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataVacationBalance).Location = new System.Drawing.Point(12, 38);
		((System.Windows.Forms.Control)(object)this.ULGDataVacationBalance).Name = "ULGDataVacationBalance";
		((System.Windows.Forms.Control)(object)this.ULGDataVacationBalance).Size = new System.Drawing.Size(524, 177);
		((System.Windows.Forms.Control)(object)this.ULGDataVacationBalance).TabIndex = 506;
		((UltraControlBase)this.ULGDataVacationBalance).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataVacationBalance.AfterEnterEditMode += new System.EventHandler(ULGDataVacationBalance_AfterEnterEditMode);
		((TextEditorControlBase)this.cboEmployeeCode).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cboEmployeeCode.AutoCompleteMode = (AutoCompleteMode)3;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Location = new System.Drawing.Point(662, 190);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Name = "cboEmployeeCode";
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Size = new System.Drawing.Size(97, 25);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).TabIndex = 604;
		((TextEditorControlBase)this.cboEmployeeCode).ValueChanged += new System.EventHandler(cboEmployeeCode_ValueChanged);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployeeCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataVacationBalance);
		base.Name = "frmEmployeesVacations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGDataVacationBalance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployeeCode, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGDataVacationBalance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
