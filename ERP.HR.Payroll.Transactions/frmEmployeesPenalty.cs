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

namespace ERP.HR.Payroll.Transactions;

public class frmEmployeesPenalty : frmDetails
{
	private DataTable dtPenaltyName;

	private DataTable dtEmployees;

	private ValueList vlPenaltyName = new ValueList();

	private IContainer components = null;

	private UltraComboEditor cboEmployeeCode;

	public frmEmployeesPenalty()
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
		dtPenaltyName = PenaltyNames.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPenaltyName.ValueListItems.Clear();
		for (int i = 0; i < dtPenaltyName.Rows.Count; i++)
		{
			vlPenaltyName.ValueListItems.Add(dtPenaltyName.Rows[i]["PenaltyNameID"], dtPenaltyName.Rows[i]["PenaltyName"].ToString());
		}
		dtDetails = EmployeesPenalty.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الجزاء" : "Penalty No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyDate"].Hidden = false;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeePenaltyDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الجزاء" : "penalty Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyNameID"].ValueList = (IValueList)(object)vlPenaltyName;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyDays"].Header).Caption = (GlobalVariables.IsArabic ? "(الجزاء(يوم" : "penalty (Days)");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PenaltyDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].DefaultCellValue = 0;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		((TextEditorControlBase)cboEmployeeCode).ValueChanged -= cboEmployeeCode_ValueChanged;
		cboEmployeeCode.SelectedIndex = cboHeader.SelectedIndex;
		((TextEditorControlBase)cboEmployeeCode).ValueChanged += cboEmployeeCode_ValueChanged;
		dtDetails = EmployeesPenalty.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["PenaltyNameID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار إسم الجزاء", "Please Select penalty Name");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["PenaltyNameID"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["EmployeePenaltyDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار التاريخ", "Please Select Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["EmployeePenaltyDate"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["PenaltyDays"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال ايام الجزاء", "Please Penalty Days");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["PenaltyDays"]).Selected = true;
				return false;
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
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["EmployeePenaltyID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("HR_EmployeesPenalty", "SubAccountID", ((TextEditorControlBase)cboHeader).Value.ToString(), "EmployeePenaltyID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				EmployeesPenalty.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Employees("-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboHeader).Value = num;
		}
	}

	public override void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		bool flag = false;
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (bool.Parse(e.Rows[i].Cells["Approved"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حذف الجزاء لانها معتمدة", "Cannot Delete Penalty Because It Is Approved");
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

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((UltraGridBase)ULGData).ActiveRow != null && bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Approved"].Value.ToString()))
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Approved" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EmployeePenaltyNo")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "PenaltyNameID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && dtDetails.Select(" PenaltyNameID =" + ((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyNameID"].Value.ToString() + " And Approved=0 ").Length > 1)
		{
			GlobalVariables.InformationMB.Show("لايمكن إختيار نفس الجزاء لوجود نفس الجزاء غير معتمد", "Cannot Select Same Penalty UnApproved");
			((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyNameID"].Value = DBNull.Value;
			((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyNameID"].Column.ValueList.SelectedItemIndex = -1;
			ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		}
		else
		{
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "PenaltyNameID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0 && ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeePenaltyDate"].Value != DBNull.Value)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyDays"].Value = EmployeesPenalty.SelectPenaltyDays(((TextEditorControlBase)cboHeader).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyNameID"].Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["EmployeePenaltyDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: true);
			}
			ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
			ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "EmployeePenaltyDate" && ((UltraGridBase)ULGData).ActiveRow.Cells["EmployeePenaltyDate"].Value != DBNull.Value && ((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyNameID"].Value != DBNull.Value)
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyDays"].Value = EmployeesPenalty.SelectPenaltyDays(((TextEditorControlBase)cboHeader).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["PenaltyNameID"].Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["EmployeePenaltyDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: true);
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
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
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
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
		((System.Windows.Forms.Control)(object)base.ULGData).Location = new System.Drawing.Point(8, 99);
		((System.Windows.Forms.Control)(object)base.ULGData).Size = new System.Drawing.Size(985, 399);
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(485, 51);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(248, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(241, 55);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		((TextEditorControlBase)this.cboEmployeeCode).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cboEmployeeCode.AutoCompleteMode = (AutoCompleteMode)3;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Location = new System.Drawing.Point(380, 51);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Name = "cboEmployeeCode";
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Size = new System.Drawing.Size(97, 25);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).TabIndex = 604;
		((TextEditorControlBase)this.cboEmployeeCode).ValueChanged += new System.EventHandler(cboEmployeeCode_ValueChanged);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployeeCode);
		base.Name = "frmEmployeesPenalty";
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
