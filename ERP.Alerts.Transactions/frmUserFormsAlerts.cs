using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Alerts;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Alerts.Transactions;

public class frmUserFormsAlerts : frmDetails
{
	private DataTable dtUsers;

	private IContainer components = null;

	public frmUserFormsAlerts()
	{
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "إسم المستخدم" : "User");
		((Control)(object)btnNext).Visible = false;
		((Control)(object)btnPriveous).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void PrepareData()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboHeader, dtUsers, "User_ID", "UserName");
		((TextEditorControlBase)cboHeader).Value = GlobalVariables.UserID;
		((Control)(object)lblHeader).Visible = false;
		((Control)(object)cboHeader).Visible = false;
		ULGData.DoubleClickRow += new DoubleClickRowEventHandler(ULGData_DoubleClickRow);
	}

	private void ULGData_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
	{
		string empty = string.Empty;
		int num = 0;
		try
		{
			empty = e.Row.Cells["FormFullName"].OriginalValue.ToString();
			num = Convert.ToInt32(e.Row.Cells["RowID"].OriginalValue);
			OpenTargetedForm(empty, num);
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("حدث خطأ\n" + ex.Message, "Error Occurred\n" + ex.Message);
		}
	}

	private void OpenTargetedForm(string FormFullName, int RowID)
	{
		GlobalFunctions.OpenForm(FormFullName, RowID, 0, 0);
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserFormsAlertsID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormFullName"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RowID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransType"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ByUser"].Hidden = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الشاشه" : "Form Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameAr"].Hidden = !GlobalVariables.IsArabic;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameAr"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الشاشه" : "Form Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameEn"].Hidden = GlobalVariables.IsArabic;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FormNameEn"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].Hidden = !GlobalVariables.IsArabic;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameAr"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].Hidden = GlobalVariables.IsArabic;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserNameEn"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransTypeDescr"].Header).Caption = (GlobalVariables.IsArabic ? "نوع المعامله" : "Transaction Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransTypeDescr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransTypeDescr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransTypeDescr"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RowNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم السجل" : "Recored No.");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RowNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RowNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RowNo"].CellActivation = (Activation)3;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ المعامله" : "Transaction Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransDate"].CellActivation = (Activation)3;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TransDate"].Format = "dd/MM/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDisplayed"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOpened"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsMarked"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Deleted"].Hidden = true;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		int num = (int)DateTime.Now.Subtract(DateTime.Now.AddMonths(-1)).TotalMinutes;
		DataView defaultView = UserFormsAlerts.SelectByUserID(GlobalVariables.UserID, "Null", "Null", "Null", num.ToString(), IsFromServer: false).DefaultView;
		defaultView.Sort = "UserFormsAlertsID DESC";
		dtDetails = defaultView.ToTable();
		dtDetails.Columns.Add("TransTypeDescr", typeof(string));
		foreach (DataRow row in dtDetails.Rows)
		{
			if (row["TransType"].ToString().Equals("I"))
			{
				row["TransTypeDescr"] = (GlobalVariables.IsArabic ? "إضافه" : "Insert");
			}
			else if (row["TransType"].ToString().Equals("U"))
			{
				row["TransTypeDescr"] = (GlobalVariables.IsArabic ? "تعديل" : "Update");
			}
			else if (row["TransType"].ToString().Equals("D"))
			{
				row["TransTypeDescr"] = (GlobalVariables.IsArabic ? "حذف" : "Delete");
			}
			else if (row["TransType"].ToString().Equals("A"))
			{
				row["TransTypeDescr"] = (GlobalVariables.IsArabic ? "إعتماد" : "Approve");
			}
		}
		InitGrid();
	}

	public override bool ValidateData()
	{
		return true;
	}

	public override void SaveData()
	{
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
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
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
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
		((System.Windows.Forms.Control)(object)base.ULGData).Location = new System.Drawing.Point(8, 38);
		((System.Windows.Forms.Control)(object)base.ULGData).Size = new System.Drawing.Size(985, 460);
		((AppearanceBase)val8).FontData.BoldAsString = "True";
		((AppearanceBase)val8).FontData.Name = "Arial";
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		((System.Windows.Forms.Control)(object)base.cboHeader).Location = new System.Drawing.Point(414, 68);
		((System.Windows.Forms.Control)(object)base.cboHeader).Size = new System.Drawing.Size(285, 24);
		((AppearanceBase)val9).FontData.BoldAsString = "True";
		((AppearanceBase)val9).FontData.Name = "Arial";
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)base.lblHeader).Location = new System.Drawing.Point(282, 72);
		((System.Windows.Forms.Control)(object)base.lblHeader).Size = new System.Drawing.Size(52, 17);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Name = "frmUserFormsAlerts";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
