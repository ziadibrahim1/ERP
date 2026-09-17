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

public class frmEmployeesMissions : frmDetails
{
	private DataTable dtEmployees;

	private IContainer components = null;

	private UltraComboEditor cboEmployeeCode;

	public frmEmployeesMissions()
	{
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
	}

	public override void PrepareData()
	{
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtEmployees, "SubAccountID", "SubAccountName");
		GlobalFunctions.FillCombo(cboEmployeeCode, dtEmployees, "SubAccountID", "EmployeeNo");
		dtDetails = EmployeesMissions.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeMissionID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeMissionNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeMissionNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeMissionNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من تاريخ" : "From Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Header).Caption = (GlobalVariables.IsArabic ? "الى تاريخ" : "To Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
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
			dtDetails = EmployeesMissions.SelectBySubAccountID(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار من التاريخ", "Please Select From Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["FromDate"]).Selected = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار الى التاريخ", "Please Select To Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["ToDate"]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (j != i && ((DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذا التاريخ واقع فى فترة من قبل", "this Date in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[j]).Selected = true;
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
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["EmployeeMissionID"].Value.ToString() + ",";
			}
			Main.SyncDeleteForUpdate("dbo.HR_EmployeesMissions", "SubAccountID", ((TextEditorControlBase)cboHeader).Value.ToString(), "EmployeeMissionID", " And (Year(FromDate) In (Select YearName From HR_Years Where Closed=0))  ", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				EmployeesMissions.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "EmployeeMissionNo")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
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

	private void cboEmployeeCode_ValueChanged(object sender, EventArgs e)
	{
		if (cboEmployeeCode.SelectedIndex > -1)
		{
			cboHeader.SelectedIndex = cboEmployeeCode.SelectedIndex;
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
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
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
		((System.Windows.Forms.Control)(object)base.ULGData).Location = new System.Drawing.Point(8, 65);
		((System.Windows.Forms.Control)(object)base.ULGData).Size = new System.Drawing.Size(985, 406);
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(499, 36);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(234, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(289, 40);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		((TextEditorControlBase)this.cboEmployeeCode).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.cboEmployeeCode.AutoCompleteMode = (AutoCompleteMode)3;
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Location = new System.Drawing.Point(396, 36);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Name = "cboEmployeeCode";
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).Size = new System.Drawing.Size(97, 25);
		((System.Windows.Forms.Control)(object)this.cboEmployeeCode).TabIndex = 604;
		((TextEditorControlBase)this.cboEmployeeCode).ValueChanged += new System.EventHandler(cboEmployeeCode_ValueChanged);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboEmployeeCode);
		base.Name = "frmEmployeesMissions";
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
