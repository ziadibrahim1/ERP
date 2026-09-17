using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Attendance.Transactions;

public class frmShiftsPlansEmployeesExceptions : frmDetails
{
	private DataTable dtShifts;

	private DataTable dtEmployeePlan;

	private DataTable dtEmployees;

	private ValueList vlShifts = new ValueList();

	private IContainer components = null;

	private UltraComboEditor cboEmployeeCode;

	public frmShiftsPlansEmployeesExceptions()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
	}

	public override void PrepareData()
	{
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtEmployees, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboEmployeeCode, dtEmployees, "SubAccountID", "EmployeeNo");
		dtShifts = Shifts.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlShifts.ValueListItems.Clear();
		for (int i = 0; i < dtShifts.Rows.Count; i++)
		{
			vlShifts.ValueListItems.Add(dtShifts.Rows[i]["ShiftID"], dtShifts.Rows[i]["ShiftName"].ToString());
		}
		dtDetails = ShiftsPlansEmployeesExceptions.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftPlanEmployeeExceptionID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الوردية" : "Shift Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftID"].ValueList = (IValueList)(object)vlShifts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExceptionDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExceptionDate"].Hidden = false;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExceptionDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExceptionDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVacation"].Header).Caption = (GlobalVariables.IsArabic ? "أجازة" : "Vacation");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVacation"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsVacation"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			((TextEditorControlBase)cboEmployeeCode).ValueChanged -= cboEmployeeCode_ValueChanged;
			cboEmployeeCode.SelectedIndex = cboHeader.SelectedIndex;
			((TextEditorControlBase)cboEmployeeCode).ValueChanged += cboEmployeeCode_ValueChanged;
			dtDetails = ShiftsPlansEmployeesExceptions.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ShiftID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الوردية", "Please Select Shift Name");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["ShiftID"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ExceptionDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار التاريخ", "Please Select Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["ExceptionDate"]).Selected = true;
				return false;
			}
		}
		return base.ValidateData();
	}

	public override void SaveData()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		Main.StartBulkTrans(FromServer: true);
		try
		{
			string text = ",";
			ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value = ((TextEditorControlBase)cboHeader).Value.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ShiftPlanEmployeeExceptionID"].Value.ToString() + ",";
			}
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
			Main.SyncDeleteForUpdate("HR_ShiftsPlansEmployeesExceptions", "SubAccountID", ((TextEditorControlBase)cboHeader).Value.ToString(), "ShiftPlanEmployeeExceptionID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ShiftsPlansEmployeesExceptions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	private void cboEmployeeCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboEmployeeCode.SelectedIndex > -1)
		{
			cboHeader.SelectedIndex = cboEmployeeCode.SelectedIndex;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "ExceptionDate")
		{
			dtEmployeePlan = ShiftsPlans.SelectByDate(((TextEditorControlBase)cboHeader).Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ExceptionDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: true);
			if (dtEmployeePlan.Rows.Count > 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ShiftID"].Value = dtEmployeePlan.Rows[0]["ShiftID"];
				((UltraGridBase)ULGData).ActiveRow.Cells["IsVacation"].Value = dtEmployeePlan.Rows[0]["IsVacation"];
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Attendance.Transactions.frmShiftsPlansEmployeesExceptions));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.cboEmployeeCode = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
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
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		((TextEditorControlBase)this.cboEmployeeCode).AlwaysInEditMode = true;
		resources.ApplyResources(this.cboEmployeeCode, "cboEmployeeCode");
		this.cboEmployeeCode.AutoCompleteMode = (AutoCompleteMode)3;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Name = "cboEmployeeCode";
		((TextEditorControlBase)this.cboEmployeeCode).ValueChanged += new System.EventHandler(cboEmployeeCode_ValueChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployeeCode);
		base.Name = "frmShiftsPlansEmployeesExceptions";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboEmployeeCode, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboEmployeeCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
