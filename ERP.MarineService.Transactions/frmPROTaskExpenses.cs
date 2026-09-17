using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmPROTaskExpenses : frmDetails
{
	private string OperationServiceStepID = "";

	private string OperationServiceID = "";

	private string OperationID = "";

	private string TaskID = "";

	private string UserSubAccountID = "";

	private decimal Qty = default(decimal);

	private DataTable dtTasks;

	private DataTable dtTaxes;

	private DataTable dtExpenses;

	private ValueList vlExpenses = new ValueList();

	private ValueList vlTaxes = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtQty;

	private UltraLabel ultraLabel11;

	public frmPROTaskExpenses()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "المهمه" : "Task");
	}

	public frmPROTaskExpenses(string OperationServiceStepTaskID, string _OperationServiceStepID, string _OperationServiceID, string _OperationID, decimal _Qty, string _TaskID)
		: this()
	{
		RowID = OperationServiceStepTaskID;
		OperationServiceStepID = _OperationServiceStepID;
		OperationServiceID = _OperationServiceID;
		OperationID = _OperationID;
		TaskID = _TaskID;
		Qty = _Qty;
	}

	public override void PrepareData()
	{
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		UltraButton obj3 = btnHeaderSearch;
		bool flag = (((Control)(object)btnSave).Visible = false);
		bool flag3 = (((Control)(object)obj3).Visible = flag);
		bool visible = (((Control)(object)obj2).Visible = flag3);
		((Control)(object)obj).Visible = visible;
		((Control)(object)txtQty).Text = Qty.ToString();
		DataTable dataTable = Users.Select(GlobalVariables.UserID, "-1", "0", IsFromServer: false);
		if (!dataTable.Rows[0]["SubAccountID"].Equals(DBNull.Value))
		{
			UserSubAccountID = dataTable.Rows[0]["SubAccountID"].ToString();
		}
		dtTasks = Tasks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtTasks, "TaskID", "TaskName");
		((EditorButtonControlBase)cboHeader).ReadOnly = true;
		((TextEditorControlBase)cboHeader).Value = TaskID;
		dtExpenses = Expenses.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlExpenses.ValueListItems.Clear();
		for (int i = 0; i < dtExpenses.Rows.Count; i++)
		{
			vlExpenses.ValueListItems.Add(dtExpenses.Rows[i]["ExpenseID"], dtExpenses.Rows[i]["ExpenseName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlTaxes.ValueListItems.Clear();
		for (int j = 0; j < dtTaxes.Rows.Count; j++)
		{
			vlTaxes.ValueListItems.Add(dtTaxes.Rows[j]["TaxID"], dtTaxes.Rows[j]["TaxName"].ToString());
		}
		dtDetails = OperationsServicesStepsTasksExpenses.SelectByOperationServiceStepTaskID(RowID, GlobalVariables.IsArabic ? "1" : "0");
		for (int k = 0; k < dtDetails.Rows.Count; k++)
		{
			if (dtDetails.Rows[k]["CanModifyPrice"].Equals(false) && dtDetails.Rows[k]["IsCompleted"].Equals(false))
			{
				if (dtDetails.Rows[k]["PerUnit"].Equals(false))
				{
					DataRow dataRow = dtDetails.Rows[k];
					object value = (dtDetails.Rows[k]["ActualTotalPrice"] = dtDetails.Rows[k]["ExpectedUnitPrice"]);
					dataRow["ActuaUnitlPrice"] = value;
				}
				else
				{
					dtDetails.Rows[k]["ActuaUnitlPrice"] = dtDetails.Rows[k]["ExpectedUnitPrice"];
					dtDetails.Rows[k]["ActualTotalPrice"] = Convert.ToDecimal(dtDetails.Rows[k]["ExpectedUnitPrice"]) * Qty;
				}
			}
		}
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceStepTaskExpenseID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PerUnit"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].DefaultCellValue = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CanModifyPrice"].DefaultCellValue = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseID"].Header).Caption = (GlobalVariables.IsArabic ? "المصروف" : "Expense");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseID"].ValueList = (IValueList)(object)vlExpenses;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Header).Caption = (GlobalVariables.IsArabic ? "الضريبة" : "Tax");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].ValueList = (IValueList)(object)vlTaxes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Header).Caption = (GlobalVariables.IsArabic ? "قيمة الضريبة" : "Tax Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TaxValue"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNo"].Header).Caption = (GlobalVariables.IsArabic ? " رقم المصروف" : "Expense No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExpenseNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر الوحده" : "Unitl Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActuaUnitlPrice"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTotalPrice"].Header).Caption = (GlobalVariables.IsArabic ? "إجمالي" : "Total");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTotalPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTotalPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualTotalPrice"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PerUnit"].Header).Caption = (GlobalVariables.IsArabic ? "للوحده" : "Per Unit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PerUnit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PerUnit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ السداد" : "Pay Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaidDate"].DefaultCellValue = DateTime.Now;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Header).Caption = (GlobalVariables.IsArabic ? "تم" : "Completed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompleted"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
	}

	public override bool ValidateData()
	{
		if (OperationsServices.Select(OperationServiceID, "-1", "0").Rows[0]["OperationInvoiceID"] == DBNull.Value)
		{
			int num = 0;
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["ExpenseID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاء إختيار المصروف", "Please Select Expense Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ExpenseID"];
					return false;
				}
				if (!Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].Cells["IsCompleted"].Value))
				{
					GlobalVariables.InformationMB.Show("برجاء اختيار انه تم سداد المصروف", "Please Check Completed For The Expense");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["IsCompleted"];
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["ActualTotalPrice"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاءإدخال قيمة المصروف", "Please Enter Total Price");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ActualTotalPrice"];
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show("برجاءإدخال رقم المصروف", "Please Enter Expense No.");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"];
					return false;
				}
				if (Main.CheckForValueByBranchIDAndFiscalYearID("MS_OperationsServicesStepsTasksExpenses", "ExpenseNo", ((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceStepTaskExpenseID"].Value.ToString() == "") ? "0" : "0", GlobalVariables.CurrentBranchID, "PaidDate", Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["PaidDate"].Value).ToString(GlobalVariables.DateShortFormate), " and OperationServiceStepTaskExpenseID <>" + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceStepTaskExpenseID"].Value.ToString()) > 0)
				{
					string codeByBranchID = OperationsServicesStepsTasksExpenses.GetCodeByBranchID(Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["PaidDate"].Value).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID);
					decimal num2 = decimal.Parse(codeByBranchID) + (decimal)num++;
					GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + num2, "The Expense Number Already Exists It Will Be Saved With No. : " + num2);
					if (GlobalVariables.MessageBoxResult != 'Y')
					{
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"];
						return false;
					}
					((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"].Value = num2;
				}
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (j != i && ((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ExpenseNo"].Value.ToString())
					{
						GlobalVariables.InformationMB.Show("لا يمكن تكرار رقم المصروف ", "Cannot Duplicate The Same Expense No.");
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ExpenseNo"];
						return false;
					}
				}
			}
			return true;
		}
		GlobalVariables.InformationMB.Show("لا يمكن تعديل المصروف لوجود فواتير على هذه الخدمة", "Cannot Change This Expense Because There is Invoice On This Service");
		return false;
	}

	public override void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			if (!ValidateData())
			{
				return;
			}
			SaveError = false;
			SaveData();
			if (SaveError)
			{
				return;
			}
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
			((Control)(object)btnSaveAndClose).Enabled = false;
			((Control)(object)btnCancel).Enabled = false;
			HasChanges = false;
		}
		Dispose();
	}

	public override void SaveData()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["IsCompleted"].Value.Equals(true) && ((UltraGridBase)ULGData).Rows[i].Cells["PaidSubAccountID"].Value.Equals(DBNull.Value))
				{
					((UltraGridBase)ULGData).Rows[i].Cells["PaidSubAccountID"].Value = UserSubAccountID;
				}
				((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceStepTaskID"].Value = RowID;
				((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceStepID"].Value = OperationServiceStepID;
				((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				if (((UltraGridBase)ULGData).Rows[i].Cells["ExpectedUnitPrice"].Value == DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].Cells["ExpectedUnitPrice"].Value = 0;
				}
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceStepTaskExpenseID"].Value.ToString() + ",";
			}
			OperationsServicesStepsTasksExpenses.DeleteForUpdate(RowID, text, IsFromServer: false);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsServicesStepsTasksExpenses.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			OperationsServices.UpdateTotalExpenses(OperationServiceID, GlobalVariables.UserID);
			OperationsServicesStepsTasksExpenses.GenerateJvs(RowID, GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		if (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Notes"))
		{
			if ((((UltraGridBase)ULGData).ActiveRow.Cells["IsCompleted"].Value.Equals(true) && !((UltraGridBase)ULGData).ActiveRow.Cells["OperationServiceStepTaskExpenseID"].Value.Equals(-1)) || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PerUnit" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PaidDate" || (((UltraGridBase)ULGData).ActiveRow.Cells["CanModifyPrice"].Value.Equals(false) && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActuaUnitlPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualTotalPrice")))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
			else if ((((UltraGridBase)ULGData).ActiveRow.Cells["PerUnit"].Value.Equals(true) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualTotalPrice") || (((UltraGridBase)ULGData).ActiveRow.Cells["PerUnit"].Value.Equals(false) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActuaUnitlPrice"))
			{
				((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
			}
		}
	}

	private void CalculateRow(UltraGridRow Row)
	{
		if (Row.Cells["TaxID"].Value != DBNull.Value)
		{
			Row.Cells["TaxValue"].Value = decimal.Parse(dtTaxes.Select(" TaxID= " + Row.Cells["TaxID"].Value.ToString())[0]["TaxPercent"].ToString()) / 100m * decimal.Parse(((Row.Cells["ActualTotalPrice"].Value.ToString() == "" || Row.Cells["ActualTotalPrice"].Value.ToString() == ".") ? "0" : Row.Cells["ActualTotalPrice"].Value).ToString());
		}
		else
		{
			Row.Cells["TaxValue"].Value = 0;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActuaUnitlPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["PerUnit"].Value.Equals(true))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ActualTotalPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ActuaUnitlPrice"].Value.ToString()) * Qty;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ActualTotalPrice" && ((UltraGridBase)ULGData).ActiveRow.Cells["PerUnit"].Value.Equals(false))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["ActuaUnitlPrice"].Value = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ActualTotalPrice"].Value.ToString());
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsCompleted" && ((UltraGridBase)ULGData).ActiveRow.Cells["IsCompleted"].Value.Equals(true))
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["PaidDate"].Value = DateTime.Now;
		}
		CalculateRow(((UltraGridBase)ULGData).ActiveRow);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Qty" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "UnitPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TotalPrice" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Discount" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "TaxValue"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	public override void btnClose_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			GlobalVariables.QuestionMB.Show("هل تريد حفظ التغييرات ؟", "Do you Want To Save Changes ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				btnSaveAndClose_Click(null, null);
			}
			else
			{
				Dispose();
			}
		}
		else
		{
			Dispose();
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "TaxID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			CalculateRow(e.Cell.Row);
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
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
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmPROTaskExpenses));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		this.txtQty = new UltraTextEditor();
		this.ultraLabel11 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).BeginInit();
		base.SuspendLayout();
		((UltraButtonBase)base.btnClose).DialogResult = System.Windows.Forms.DialogResult.None;
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
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(this.txtQty, "txtQty");
		((System.Windows.Forms.Control)(object)this.txtQty).Name = "txtQty";
		((EditorButtonControlBase)this.txtQty).ReadOnly = true;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((ControlBase)this.ultraLabel11).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel11.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel11, "ultraLabel11");
		((System.Windows.Forms.Control)(object)this.ultraLabel11).Name = "ultraLabel11";
		((ControlBase)this.ultraLabel11).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQty);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel11);
		base.Name = "frmPROTaskExpenses";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel11, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQty, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQty).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
