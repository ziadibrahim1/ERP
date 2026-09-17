using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.POS;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Classes.DirectPrinting;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmShiftsDetailsUsers : frmGrid
{
	private DataTable dtReports;

	private DateTime? DisplayDate = null;

	private IContainer components = null;

	private UltraCheckEditor chkClose;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtDifference;

	private UltraLabel lblDifference;

	private UltraTextEditor txtBookValue;

	private UltraLabel lblBookValue;

	private UltraTextEditor txtActualValue;

	private UltraLabel lblActualValue;

	private UltraLabel lblEndDate;

	private UltraDateTimeEditor dtpEndDate;

	private UltraLabel lblStartDate;

	private UltraDateTimeEditor dtpStartDate;

	private UltraLabel lblShiftNo;

	private UltraTextEditor txtShiftNo;

	private UltraLabel lblUserName;

	private UltraTextEditor txtUserName;

	public frmShiftsDetailsUsers()
	{
		InitializeComponent();
		TableName = "POS_ShiftsDetailsUsers";
		IDCol = "ShiftDetailUserID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnHeaderSearch).Visible = false;
		((Control)(object)btnDelete).Visible = false;
		((Control)(object)btnAdd).Visible = false;
		((EditorButtonControlBase)txtShiftNo).ReadOnly = true;
		((EditorButtonControlBase)dtpStartDate).ReadOnly = true;
		((EditorButtonControlBase)dtpEndDate).ReadOnly = true;
		((EditorButtonControlBase)txtActualValue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBookValue).ReadOnly = true;
		((EditorButtonControlBase)txtDifference).ReadOnly = true;
		((EditorButtonControlBase)txtUserName).ReadOnly = true;
		((Control)(object)chkClose).Enabled = !NavMode;
		((TextEditorControlBase)txtActualValue).Focus();
	}

	public override void FillData()
	{
		dtpStartDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtpEndDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dataTable = ShiftsDetailsUsers.SelectByUser_ID(GlobalVariables.UserID, GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
		DisplayDate = GlobalFunctions.GetServerDateTimeNow();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftDetailNo"].Header).Caption = (GlobalVariables.IsArabic ? " رقم الوردية" : "Shift no");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftDetailNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Header).Caption = (GlobalVariables.IsArabic ? "المستخدم" : "User Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ البدأ " : "Start Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ النهاية " : "End Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة الفعلية " : "Actual Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BookValue"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BookValue"].Header).Caption = (GlobalVariables.IsArabic ? "القيمة الدفترية " : "Book value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Difference"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Difference"].Header).Caption = (GlobalVariables.IsArabic ? "الفرق" : "Difference");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Header).Caption = (GlobalVariables.IsArabic ? "إغلاق" : "Closed");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ShiftDetailNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["UserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ActualValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BookValue"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Difference"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsClosed"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void AfterRowActivate()
	{
		RowID = ((UltraGridBase)ULGData).ActiveRow.Cells[IDCol].Value.ToString();
		((Control)(object)txtShiftNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ShiftDetailNo"].Value.ToString();
		dtpStartDate.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["StartDate"].Value;
		dtpEndDate.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["EndDate"].Value;
		((Control)(object)txtActualValue).Text = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ActualValue"].Value.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtBookValue).Text = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BookValue"].Value.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtDifference).Text = decimal.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Difference"].Value.ToString()).ToString(GlobalVariables.txtDecimalFormate);
		((Control)(object)txtUserName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["UserName"].Value.ToString();
		((UltraToggleEditorBase)chkClose).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsClosed"].Value.ToString());
	}

	public override bool ValidateData()
	{
		if (DisplayDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDate).ToString(GlobalVariables.DateLongFormateMS), RowID, TableName))
		{
			GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
			return false;
		}
		if (((Control)(object)txtActualValue).Text == "" || decimal.Parse(((Control)(object)txtActualValue).Text) < 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال القيمة الفعلية", "Please Enter Actual Value");
			((TextEditorControlBase)txtActualValue).Focus();
			return false;
		}
		if (!((UltraToggleEditorBase)chkClose).Checked)
		{
			GlobalVariables.InformationMB.Show("برجاء إغلاق الخزينة", "Please Close Your Safe");
			((Control)(object)chkClose).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void btnUpdateClick()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (!CanUpdate)
			{
				GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			}
			else if (!CanModifyOtherBranch)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			}
			else if (ClosedPeriod)
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Update This Transaction Because It Related to ClosedPeriod ");
			}
			else if (bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsClosed"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها مغلقه", "Cannot Update This Transaction Because It Is Closed  ");
			}
			else if (DisplayDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDate).ToString(GlobalVariables.DateLongFormateMS), RowID, TableName))
			{
				GlobalVariables.InformationMB.Show("يوجد تعديل فى البيانات برجاء تنشيط البيانات", "Data Has Been Modified Please Refresh Your Data ");
				FillData();
			}
			else
			{
				Updating = true;
				SetControls(NavMode: false);
			}
		}
	}

	public override void UpdateData()
	{
		ShiftsDetailsUsers.UpdateValues(((UltraGridBase)ULGData).ActiveRow.Cells["ShiftDetailUserID"].Value.ToString(), GlobalVariables.UserID, ((Control)(object)txtActualValue).Text, ((Control)(object)txtNotes).Text, GlobalVariables.CurrentBranchID);
		if (!(GlobalVariables.POSPrinter != ""))
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد طباعة إقفال خزينة المستخدم ؟", "Are You Sure You want to Print user Safe Closing?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			text = string.Concat(text, ((UltraGridBase)ULGData).ActiveRow.Cells["ShiftDetailUserID"].Value, ",");
		}
		if (!(text != ","))
		{
			return;
		}
		ReportDocument reportDocument = new ReportDocument();
		try
		{
			if (dtReports.Rows.Count > 0)
			{
				if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_POS_ShiftsDetailsUsersFastPrint")
				{
					FastPrint(text);
					return;
				}
				reportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			}
			else
			{
				reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ShiftsDetailsUsers_A.rpt" : "Rep_POS_ShiftsDetailsUsers_E.rpt"));
			}
			GlobalFunctions.ConfigureReport(reportDocument);
			reportDocument.SetParameterValue("@ShiftDetailUserIDs", text);
			reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
			try
			{
				reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
			}
		}
		catch
		{
			GlobalVariables.InformationMB.Show("خطأ فى مسار التقارير ", "Load Report Failed");
		}
		reportDocument.Dispose();
		GC.Collect();
	}

	public override void btnPrintClick()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			text = string.Concat(text, ((UltraGridBase)ULGData).ActiveRow.Cells["ShiftDetailUserID"].Value, ",");
		}
		if (!(text != ","))
		{
			return;
		}
		if (dtReports.Rows.Count > 0)
		{
			if (dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "Rep_POS_ShiftsDetailsUsersFastPrint")
			{
				FastPrint(text);
				return;
			}
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ShiftsDetailsUsers_A.rpt" : "Rep_POS_ShiftsDetailsUsers_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@ShiftDetailUserIDs", text);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		GlobalVariables.ReportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
	}

	public void FastPrint(string IDs)
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_POS_ShiftsDetailsUsers '" + IDs + "'," + (GlobalVariables.IsArabic ? "1" : "0"));
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.Rows[0];
			Font font = new Font("Times New Roman", 10f, FontStyle.Bold);
			Font font2 = new Font("Times New Roman", 8f, FontStyle.Bold);
			Font font3 = new Font("Times New Roman", 8f, FontStyle.Regular);
			Font font4 = new Font("Times New Roman", 7f, FontStyle.Regular);
			Font font5 = new Font("Times New Roman", 9f, FontStyle.Regular);
			Font font6 = new Font("Times New Roman", 9f, FontStyle.Bold);
			Font font7 = new Font("Times New Roman", 10f, FontStyle.Bold);
			FastPrint instance = ERP.Classes.DirectPrinting.FastPrint.Instance;
			instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
			instance.GraphicsUnit = GraphicsUnit.Millimeter;
			instance.Margins = new Margins(0, 0, 0, 0);
			instance.OverallWidth = 70f;
			instance.AddTextCell(dataRow["ShiftDetailNo"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("رقم الوردية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddTextCell(dataRow["ShiftName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("اسم الوردية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddTextCell(dataRow["BranchName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("الفرع", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddTextCell(dataRow["UserName"].ToString(), font5, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("المستخدم", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			if (dataRow["EndDate"] != DBNull.Value)
			{
				instance.AddTextCell(((DateTime)dataRow["EndDate"]).ToString("dd/MM/yyyy"), font5, 0.26f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			}
			else
			{
				instance.AddTextCell("", font5, 0.26f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			}
			instance.AddTextCell("تاريخ النهاية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell(((DateTime)dataRow["StartDate"]).ToString("dd/MM/yyyy"), font5, 0.26f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("تاريخ البداية", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
			instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
			instance.AcceptChanges();
			instance.AddTextCell("الفرق", font2, 0.3f, 5f);
			instance.AddTextCell("القيمة الدفترية", font2, 0.35f, 5f);
			instance.AddTextCell("القيمة الفعلية", font2, 0.35f, 5f);
			instance.AcceptChanges();
			instance.AddTextCell(decimal.Parse(dataRow["Difference"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.3f, 4f);
			instance.AddTextCell(decimal.Parse(dataRow["BookValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.35f, 4f);
			instance.AddTextCell(decimal.Parse(dataRow["ActualValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.35f, 4f);
			instance.AcceptChanges();
			instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
			instance.AcceptChanges();
			if (dataRow["Notes"].ToString().Trim().Length > 0)
			{
				instance.AddTextCell(dataRow["Notes"].ToString(), font5, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell("ملاحظات", font5, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AcceptChanges();
			}
			instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
			instance.AcceptChanges();
			if (int.Parse(dataRow["OnAccountChecksCount"].ToString()) > 0)
			{
				instance.AddTextCell("يوجد شيكات آجلة", font5, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AcceptChanges();
				instance.AddTextCell(dataRow["OnAccountChecksAmount"].ToString(), font5, 0.3f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell("قيمتها:", font5, 0.2f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell(dataRow["OnAccountChecksCount"].ToString(), font5, 0.3f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AddTextCell("عددها:", font5, 0.2f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
				instance.AcceptChanges();
				instance.AddEmptyCell(1f, 2f, DrawRectangle: false);
				instance.AcceptChanges();
			}
			instance.AddTextCell(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
			try
			{
				instance.Print();
				return;
			}
			catch (Exception ex)
			{
				GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
				return;
			}
		}
		GlobalVariables.InformationMB.Show("لا يوجد بيانات لطباعتها", "There Is No Data To Be Shown");
	}

	private void txtActualValue_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmShiftsDetailsUsers));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.chkClose = new UltraCheckEditor();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtDifference = new UltraTextEditor();
		this.lblDifference = new UltraLabel();
		this.txtBookValue = new UltraTextEditor();
		this.lblBookValue = new UltraLabel();
		this.txtActualValue = new UltraTextEditor();
		this.lblActualValue = new UltraLabel();
		this.lblEndDate = new UltraLabel();
		this.dtpEndDate = new UltraDateTimeEditor();
		this.lblStartDate = new UltraLabel();
		this.dtpStartDate = new UltraDateTimeEditor();
		this.lblShiftNo = new UltraLabel();
		this.txtShiftNo = new UltraTextEditor();
		this.lblUserName = new UltraLabel();
		this.txtUserName = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkClose).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDifference).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBookValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEndDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
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
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance10");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.chkClose, "chkClose");
		((System.Windows.Forms.Control)(object)this.chkClose).Name = "chkClose";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtDifference, "txtDifference");
		((System.Windows.Forms.Control)(object)this.txtDifference).Name = "txtDifference";
		resources.ApplyResources(this.lblDifference, "lblDifference");
		this.lblDifference.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDifference).Name = "lblDifference";
		((ControlBase)this.lblDifference).WrapText = false;
		resources.ApplyResources(this.txtBookValue, "txtBookValue");
		((System.Windows.Forms.Control)(object)this.txtBookValue).Name = "txtBookValue";
		resources.ApplyResources(this.lblBookValue, "lblBookValue");
		this.lblBookValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBookValue).Name = "lblBookValue";
		((ControlBase)this.lblBookValue).WrapText = false;
		resources.ApplyResources(this.txtActualValue, "txtActualValue");
		((System.Windows.Forms.Control)(object)this.txtActualValue).Name = "txtActualValue";
		((System.Windows.Forms.Control)(object)this.txtActualValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtActualValue_KeyPress);
		resources.ApplyResources(this.lblActualValue, "lblActualValue");
		this.lblActualValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblActualValue).Name = "lblActualValue";
		((ControlBase)this.lblActualValue).WrapText = false;
		resources.ApplyResources(this.lblEndDate, "lblEndDate");
		this.lblEndDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEndDate).Name = "lblEndDate";
		((ControlBase)this.lblEndDate).WrapText = false;
		resources.ApplyResources(this.dtpEndDate, "dtpEndDate");
		((UltraWinEditorMaskedControlBase)this.dtpEndDate).AlwaysInEditMode = true;
		this.dtpEndDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpEndDate).Name = "dtpEndDate";
		resources.ApplyResources(this.lblStartDate, "lblStartDate");
		this.lblStartDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStartDate).Name = "lblStartDate";
		((ControlBase)this.lblStartDate).WrapText = false;
		resources.ApplyResources(this.dtpStartDate, "dtpStartDate");
		((UltraWinEditorMaskedControlBase)this.dtpStartDate).AlwaysInEditMode = true;
		this.dtpStartDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpStartDate).Name = "dtpStartDate";
		resources.ApplyResources(this.lblShiftNo, "lblShiftNo");
		this.lblShiftNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblShiftNo).Name = "lblShiftNo";
		((ControlBase)this.lblShiftNo).WrapText = false;
		resources.ApplyResources(this.txtShiftNo, "txtShiftNo");
		((System.Windows.Forms.Control)(object)this.txtShiftNo).Name = "txtShiftNo";
		resources.ApplyResources(this.lblUserName, "lblUserName");
		this.lblUserName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		((ControlBase)this.lblUserName).WrapText = false;
		resources.ApplyResources(this.txtUserName, "txtUserName");
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtShiftNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDifference);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDifference);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBookValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBookValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtActualValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblActualValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEndDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpStartDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblShiftNo);
		base.Name = "frmShiftsDetailsUsers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStartDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEndDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblActualValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtActualValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBookValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBookValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDifference, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDifference, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtShiftNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUserName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtUserName, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkClose).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDifference).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBookValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtActualValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEndDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpStartDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtShiftNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
