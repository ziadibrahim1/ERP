using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.EInvoices;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using EInvoice;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.EInvoices.Transactions;

public class frmLnsInvoices2EInvoiceSend : frmBase
{
	private DataTable dtDetails;

	private DataTable dtEINVStates;

	private DataTable dtBranchs;

	private ValueList vlEINVStates = new ValueList();

	private bool UseEInvoiceProductionEnvirnoment;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	public UltraGrid ULGData;

	private UltraDateTimeEditor dtpFromDate;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel6;

	public UltraButton btnOpenTicket;

	public UltraButton btnCopy;

	public UltraButton btnSearch;

	public UltraButton btnSendInvoices;

	private UltraLabel lblState;

	private UltraComboEditor cboState;

	public UltraButton btnPrintInvoices;

	public UltraLabel lblInvalidDoc;

	public UltraLabel lblInvalidDocColor;

	public UltraLabel lblNotUploaded;

	public UltraLabel lblNotUploadedColor;

	public UltraLabel lblValidDoc;

	public UltraLabel lblValidDocColor;

	public UltraButton btnCancelled;

	private UltraLabel lblBranch;

	private UltraComboEditor cboBranches;

	public UltraLabel lblSubmittedDoc;

	public UltraLabel lblSubmittedDocColor;

	public UltraLabel lblCancelledDocColor;

	public UltraLabel lblCancelledDoc;

	public frmLnsInvoices2EInvoiceSend()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		dtpToDate.DateTime = dtpFromDate.DateTime.AddMonths(1).AddSeconds(-1.0);
		UseEInvoiceProductionEnvirnoment = GlobalFunctions.GetOption("ElectronicInvoiceProductionEnvirnoment");
		dtEINVStates = States.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboState, dtEINVStates, "EINVStateID", "EINVStateName");
		vlEINVStates.ValueListItems.Clear();
		for (int i = 0; i < dtEINVStates.Rows.Count; i++)
		{
			vlEINVStates.ValueListItems.Add(dtEINVStates.Rows[i]["EINVStateID"], dtEINVStates.Rows[i]["EINVStateName"].ToString());
		}
		dtBranchs = Branches.Select("-1", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBranches, dtBranchs, "BranchID", GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
		FillGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation)4;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendInvoice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendInvoice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendInvoice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendInvoice"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendInvoice"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date ");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم العميل" : "Client Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "صافى" : "Net Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceInternalCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceInternalCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceInternalCode"].Header).Caption = (GlobalVariables.IsArabic ? "الكود الداخلى" : "Internal Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceUUID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceUUID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceUUID"].Header).Caption = (GlobalVariables.IsArabic ? "UUID" : "UUID");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceSenDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceSenDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceSenDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الارسال" : "Send Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendUserName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendUserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendUserName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم المرسل" : "Send User");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceIsCanceled"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceIsCanceled"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceIsCanceled"].Header).Caption = (GlobalVariables.IsArabic ? "ملغاه" : "Canceled");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceCanceledDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceCanceledDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EInvoiceCanceledDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الالغاء" : "Cancel Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancelUserName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancelUserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CancelUserName"].Header).Caption = (GlobalVariables.IsArabic ? "إسم الملغى" : "Cancel User");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVStateID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVStateID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVStateID"].Header).Caption = (GlobalVariables.IsArabic ? "حالة" : "State");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EINVStateID"].ValueList = (IValueList)(object)vlEINVStates;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Open"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Open");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).Caption = (GlobalVariables.IsArabic ? "مراجعة" : "Audit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Open"].Value = (GlobalVariables.IsArabic ? "مراجعة" : "Audit");
			if (((UltraGridBase)ULGData).Rows[i].Cells["EINVStateID"].Value.ToString() == "1")
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblNotUploadedColor).Appearance.BackColor;
			}
			else if (((UltraGridBase)ULGData).Rows[i].Cells["EINVStateID"].Value.ToString() == "2")
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblValidDocColor).Appearance.BackColor;
			}
			else if (((UltraGridBase)ULGData).Rows[i].Cells["EINVStateID"].Value.ToString() == "3")
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblInvalidDocColor).Appearance.BackColor;
			}
			else if (((UltraGridBase)ULGData).Rows[i].Cells["EINVStateID"].Value.ToString() == "4")
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblSubmittedDocColor).Appearance.BackColor;
			}
			else if (((UltraGridBase)ULGData).Rows[i].Cells["EINVStateID"].Value.ToString() == "5")
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = ((ControlBase)lblCancelledDocColor).Appearance.BackColor;
			}
		}
	}

	private void FillGrid()
	{
		dtDetails = Invoices.SelectForEInvoice((cboBranches.SelectedIndex == -1) ? GlobalVariables.BranchIDs : ("," + ((TextEditorControlBase)cboBranches).Value.ToString() + ","), (dtpFromDate.Value == null) ? "Null" : dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpToDate.Value == null) ? "Null" : dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), (cboState.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboState).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "SendInvoice")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmLnsInvoices2EInvoice frmLnsInvoices2EInvoice2 = new frmLnsInvoices2EInvoice(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceID"].Value.ToString()));
			frmLnsInvoices2EInvoice2.Size = new Size(base.Width, base.Height);
			frmLnsInvoices2EInvoice2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmLnsInvoices2EInvoice2.lblTitle).Text = (GlobalVariables.IsArabic ? "فاتورة البيع المباشر 4 الالكترونية" : "Direct Sales 4 Electronic");
			frmLnsInvoices2EInvoice2.ShowDialog();
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		if (cboState.SelectedIndex == -1 && dtpFromDate.Value == null && dtpToDate.Value == null)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال اي من بيانات البحث" : "Please Enter at least one Search Criteria");
		}
		else
		{
			FillGrid();
		}
	}

	private async void btnSendInvoices_Click(object sender, EventArgs e)
	{
		((Control)(object)btnSendInvoices).Enabled = false;
		EINV.Set_Connection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword);
		string strIDs = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SendInvoice"].Value.ToString()))
			{
				strIDs = strIDs + ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceID"].Value.ToString() + ",";
			}
		}
		if (strIDs == ",")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد فواتير لإرسالها " : "There are No Invoice To Send");
			((Control)(object)btnSendInvoices).Enabled = true;
			return;
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGData).Rows[j].Cells["EInvoiceUUID"].Value.ToString() != "" && bool.Parse(((UltraGridBase)ULGData).Rows[j].Cells["SendInvoice"].Value.ToString()))
			{
				int State = await EINV.GetDocumentStateLns(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, ((UltraGridBase)ULGData).Rows[j].Cells["EInvoiceUUID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[j].Cells["InvoiceID"].Value.ToString());
				if (State > -1)
				{
					((UltraGridBase)ULGData).Rows[j].Cells["EINVStateID"].Value = State;
				}
			}
		}
		List<int> lstSLInvoiceID = new List<int>();
		strIDs = ",";
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[k].Cells["SendInvoice"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[k].Cells["EINVStateID"].Value.ToString() != "2" && ((UltraGridBase)ULGData).Rows[k].Cells["EINVStateID"].Value.ToString() != "4" && ((UltraGridBase)ULGData).Rows[k].Cells["EINVStateID"].Value.ToString() != "5")
			{
				strIDs = strIDs + ((UltraGridBase)ULGData).Rows[k].Cells["InvoiceID"].Value.ToString() + ",";
				lstSLInvoiceID.Add(int.Parse(((UltraGridBase)ULGData).Rows[k].Cells["InvoiceID"].Value.ToString()));
			}
		}
		if (strIDs != ",")
		{
			string Message = EINV.ValidationByLnsInvoiceIDs(strIDs, GlobalVariables.IsArabic);
			if (Message != "")
			{
				GlobalVariables.InformationMB.Show(Message);
				((Control)(object)btnSendInvoices).Enabled = true;
				return;
			}
			await EINV.DocumentSubmissionLns(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, strIDs, GlobalVariables.UserID, GlobalVariables.IsArabic);
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد فواتير لإرسالها " : "There are No Invoice To Send");
		}
		((Control)(object)btnSendInvoices).Enabled = true;
		FillGrid();
	}

	private async void btnPrintInvoices_Click(object sender, EventArgs e)
	{
		List<List<string>> lstInvoices = new List<List<string>>();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SendInvoice"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["EInvoiceUUID"].Value.ToString() != "")
			{
				lstInvoices.Add(new List<string>
				{
					((UltraGridBase)ULGData).Rows[i].Cells["EInvoiceUUID"].Value.ToString(),
					((UltraGridBase)ULGData).Rows[i].Cells["EInvoiceInternalCode"].Value.ToString().Replace("/", "-") + "-" + ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceNo"].Value.ToString()
				});
			}
		}
		if (lstInvoices.Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد فواتير لطباعتها " : "There are No Invoice To Print");
			return;
		}
		await EINV.DocumentPrint(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, lstInvoices);
		FillGrid();
	}

	private async void btnCancelled_Click(object sender, EventArgs e)
	{
		List<List<string>> lstInvoices = new List<List<string>>();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SendInvoice"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["EInvoiceUUID"].Value.ToString() != "")
			{
				lstInvoices.Add(new List<string>
				{
					((UltraGridBase)ULGData).Rows[i].Cells["EInvoiceUUID"].Value.ToString(),
					((UltraGridBase)ULGData).Rows[i].Cells["InvoiceNo"].Value.ToString()
				});
			}
		}
		if (lstInvoices.Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد فواتير للإلغاء " : "There are No Invoice To Cancel");
			return;
		}
		EINV.Set_Connection(GlobalVariables.Server, GlobalVariables.DatabaseName, GlobalVariables.dbUserID, GlobalVariables.dbPassword);
		await EINV.DocumentCancellationLns(GlobalFunctions.GetDefault("EINVClientID"), GlobalFunctions.GetDefault("EINVClientSecret1"), UseEInvoiceProductionEnvirnoment, lstInvoices, GlobalVariables.UserID);
		FillGrid();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnCopy_Click(object sender, EventArgs e)
	{
		ULGData.Selected.Rows.AddRange(((UltraGridBase)ULGData).Rows.GetAllNonGroupByRows());
		ULGData.PerformAction((UltraGridAction)48, false, false);
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
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
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Expected O, but got Unknown
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Expected O, but got Unknown
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Expected O, but got Unknown
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Expected O, but got Unknown
		//IL_08fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.EInvoices.Transactions.frmLnsInvoices2EInvoiceSend));
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
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		ValueListItem val17 = new ValueListItem();
		ValueListItem val18 = new ValueListItem();
		ValueListItem val19 = new ValueListItem();
		ValueListItem val20 = new ValueListItem();
		ValueListItem val21 = new ValueListItem();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		Appearance val30 = new Appearance();
		ValueListItem val31 = new ValueListItem();
		ValueListItem val32 = new ValueListItem();
		ValueListItem val33 = new ValueListItem();
		ValueListItem val34 = new ValueListItem();
		ValueListItem val35 = new ValueListItem();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.ULGData = new UltraGrid();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel6 = new UltraLabel();
		this.btnOpenTicket = new UltraButton();
		this.btnCopy = new UltraButton();
		this.btnSearch = new UltraButton();
		this.btnSendInvoices = new UltraButton();
		this.lblState = new UltraLabel();
		this.cboState = new UltraComboEditor();
		this.btnPrintInvoices = new UltraButton();
		this.lblInvalidDoc = new UltraLabel();
		this.lblInvalidDocColor = new UltraLabel();
		this.lblNotUploaded = new UltraLabel();
		this.lblNotUploadedColor = new UltraLabel();
		this.lblValidDoc = new UltraLabel();
		this.lblValidDocColor = new UltraLabel();
		this.btnCancelled = new UltraButton();
		this.lblBranch = new UltraLabel();
		this.cboBranches = new UltraComboEditor();
		this.lblSubmittedDoc = new UltraLabel();
		this.lblSubmittedDocColor = new UltraLabel();
		this.lblCancelledDocColor = new UltraLabel();
		this.lblCancelledDoc = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboState).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
		((UltraGridBase)this.ULGData).DisplayLayout.DefaultSelectedForeColor = System.Drawing.Color.Empty;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val6;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val10, "appearance10");
		((AppearanceBase)val10).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val11).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val11).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		this.dtpFromDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		this.dtpToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		this.ultraLabel6.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this.btnCopy, "btnCopy");
		((AppearanceBase)val14).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.btnCopy).Appearance = (AppearanceBase)(object)val14;
		((ControlBase)this.btnCopy).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCopy).Name = "btnCopy";
		((System.Windows.Forms.Control)(object)this.btnCopy).Click += new System.EventHandler(btnCopy_Click);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnSendInvoices, "btnSendInvoices");
		((AppearanceBase)val15).Image = resources.GetObject("appearance15.Image");
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.btnSendInvoices).Appearance = (AppearanceBase)(object)val15;
		((ControlBase)this.btnSendInvoices).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSendInvoices).Name = "btnSendInvoices";
		((System.Windows.Forms.Control)(object)this.btnSendInvoices).Click += new System.EventHandler(btnSendInvoices_Click);
		resources.ApplyResources(this.lblState, "lblState");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblState).Appearance = (AppearanceBase)(object)val16;
		this.lblState.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblState).Name = "lblState";
		((ControlBase)this.lblState).WrapText = false;
		resources.ApplyResources(this.cboState, "cboState");
		this.cboState.AutoCompleteMode = (AutoCompleteMode)4;
		val17.DataValue = "1";
		resources.ApplyResources(val17, "valueListItem1");
		((SubObjectBase)val17).ForceApplyResources = "";
		val18.DataValue = "2";
		resources.ApplyResources(val18, "valueListItem2");
		((SubObjectBase)val18).ForceApplyResources = "";
		val19.DataValue = "3";
		resources.ApplyResources(val19, "valueListItem3");
		((SubObjectBase)val19).ForceApplyResources = "";
		val20.DataValue = "4";
		resources.ApplyResources(val20, "valueListItem4");
		((SubObjectBase)val20).ForceApplyResources = "";
		val21.DataValue = "5";
		resources.ApplyResources(val21, "valueListItem5");
		((SubObjectBase)val21).ForceApplyResources = "";
		this.cboState.Items.AddRange((ValueListItem[])(object)new ValueListItem[5] { val17, val18, val19, val20, val21 });
		((System.Windows.Forms.Control)(object)this.cboState).Name = "cboState";
		resources.ApplyResources(this.btnPrintInvoices, "btnPrintInvoices");
		((AppearanceBase)val22).Image = resources.GetObject("appearance17.Image");
		resources.ApplyResources(val22, "appearance17");
		((ControlBase)this.btnPrintInvoices).Appearance = (AppearanceBase)(object)val22;
		((ControlBase)this.btnPrintInvoices).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnPrintInvoices).Name = "btnPrintInvoices";
		((System.Windows.Forms.Control)(object)this.btnPrintInvoices).Click += new System.EventHandler(btnPrintInvoices_Click);
		resources.ApplyResources(this.lblInvalidDoc, "lblInvalidDoc");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val23, "appearance18");
		((ControlBase)this.lblInvalidDoc).Appearance = (AppearanceBase)(object)val23;
		this.lblInvalidDoc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInvalidDoc).Name = "lblInvalidDoc";
		((ControlBase)this.lblInvalidDoc).WrapText = false;
		resources.ApplyResources(this.lblInvalidDocColor, "lblInvalidDocColor");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Yellow;
		((AppearanceBase)val24).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val24, "appearance19");
		((ControlBase)this.lblInvalidDocColor).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.lblInvalidDocColor).Name = "lblInvalidDocColor";
		((UltraControlBase)this.lblInvalidDocColor).UseAppStyling = false;
		resources.ApplyResources(this.lblNotUploaded, "lblNotUploaded");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val25, "appearance20");
		((ControlBase)this.lblNotUploaded).Appearance = (AppearanceBase)(object)val25;
		this.lblNotUploaded.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotUploaded).Name = "lblNotUploaded";
		((UltraControlBase)this.lblNotUploaded).UseAppStyling = false;
		((ControlBase)this.lblNotUploaded).WrapText = false;
		resources.ApplyResources(this.lblNotUploadedColor, "lblNotUploadedColor");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val26).Image = resources.GetObject("appearance21.Image");
		resources.ApplyResources(val26, "appearance21");
		((ControlBase)this.lblNotUploadedColor).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.lblNotUploadedColor).Name = "lblNotUploadedColor";
		((UltraControlBase)this.lblNotUploadedColor).UseAppStyling = false;
		resources.ApplyResources(this.lblValidDoc, "lblValidDoc");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).Image = resources.GetObject("appearance22.Image");
		resources.ApplyResources(val27, "appearance22");
		((ControlBase)this.lblValidDoc).Appearance = (AppearanceBase)(object)val27;
		this.lblValidDoc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValidDoc).Name = "lblValidDoc";
		((ControlBase)this.lblValidDoc).WrapText = false;
		resources.ApplyResources(this.lblValidDocColor, "lblValidDocColor");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.YellowGreen;
		((AppearanceBase)val28).Image = resources.GetObject("appearance23.Image");
		resources.ApplyResources(val28, "appearance23");
		((ControlBase)this.lblValidDocColor).Appearance = (AppearanceBase)(object)val28;
		((System.Windows.Forms.Control)(object)this.lblValidDocColor).Name = "lblValidDocColor";
		((UltraControlBase)this.lblValidDocColor).UseAppStyling = false;
		resources.ApplyResources(this.btnCancelled, "btnCancelled");
		((AppearanceBase)val29).Image = resources.GetObject("appearance24.Image");
		resources.ApplyResources(val29, "appearance24");
		((ControlBase)this.btnCancelled).Appearance = (AppearanceBase)(object)val29;
		((ControlBase)this.btnCancelled).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancelled).Name = "btnCancelled";
		((System.Windows.Forms.Control)(object)this.btnCancelled).Click += new System.EventHandler(btnCancelled_Click);
		resources.ApplyResources(this.lblBranch, "lblBranch");
		((AppearanceBase)val30).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val30).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val30, "appearance25");
		((ControlBase)this.lblBranch).Appearance = (AppearanceBase)(object)val30;
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		resources.ApplyResources(this.cboBranches, "cboBranches");
		this.cboBranches.AutoCompleteMode = (AutoCompleteMode)4;
		val31.DataValue = "1";
		resources.ApplyResources(val31, "valueListItem6");
		((SubObjectBase)val31).ForceApplyResources = "";
		val32.DataValue = "2";
		resources.ApplyResources(val32, "valueListItem7");
		((SubObjectBase)val32).ForceApplyResources = "";
		val33.DataValue = "3";
		resources.ApplyResources(val33, "valueListItem8");
		((SubObjectBase)val33).ForceApplyResources = "";
		val34.DataValue = "4";
		resources.ApplyResources(val34, "valueListItem9");
		((SubObjectBase)val34).ForceApplyResources = "";
		val35.DataValue = "5";
		resources.ApplyResources(val35, "valueListItem10");
		((SubObjectBase)val35).ForceApplyResources = "";
		this.cboBranches.Items.AddRange((ValueListItem[])(object)new ValueListItem[5] { val31, val32, val33, val34, val35 });
		((System.Windows.Forms.Control)(object)this.cboBranches).Name = "cboBranches";
		resources.ApplyResources(this.lblSubmittedDoc, "lblSubmittedDoc");
		((AppearanceBase)val36).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val36).Image = resources.GetObject("appearance26.Image");
		resources.ApplyResources(val36, "appearance26");
		((ControlBase)this.lblSubmittedDoc).Appearance = (AppearanceBase)(object)val36;
		this.lblSubmittedDoc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSubmittedDoc).Name = "lblSubmittedDoc";
		((ControlBase)this.lblSubmittedDoc).WrapText = false;
		resources.ApplyResources(this.lblSubmittedDocColor, "lblSubmittedDocColor");
		((AppearanceBase)val37).BackColor = System.Drawing.Color.DodgerBlue;
		resources.ApplyResources(val37, "appearance27");
		((ControlBase)this.lblSubmittedDocColor).Appearance = (AppearanceBase)(object)val37;
		((System.Windows.Forms.Control)(object)this.lblSubmittedDocColor).Name = "lblSubmittedDocColor";
		((UltraControlBase)this.lblSubmittedDocColor).UseAppStyling = false;
		resources.ApplyResources(this.lblCancelledDocColor, "lblCancelledDocColor");
		((AppearanceBase)val38).BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
		resources.ApplyResources(val38, "appearance28");
		((ControlBase)this.lblCancelledDocColor).Appearance = (AppearanceBase)(object)val38;
		((System.Windows.Forms.Control)(object)this.lblCancelledDocColor).Name = "lblCancelledDocColor";
		((UltraControlBase)this.lblCancelledDocColor).UseAppStyling = false;
		resources.ApplyResources(this.lblCancelledDoc, "lblCancelledDoc");
		((AppearanceBase)val39).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val39).Image = resources.GetObject("appearance29.Image");
		resources.ApplyResources(val39, "appearance29");
		((ControlBase)this.lblCancelledDoc).Appearance = (AppearanceBase)(object)val39;
		this.lblCancelledDoc.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCancelledDoc).Name = "lblCancelledDoc";
		((ControlBase)this.lblCancelledDoc).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCancelledDocColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubmittedDocColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancelled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValidDocColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvalidDoc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInvalidDocColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotUploaded);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotUploadedColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintInvoices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboState);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSendInvoices);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopy);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSubmittedDoc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValidDoc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCancelledDoc);
		base.Name = "frmLnsInvoices2EInvoiceSend";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCancelledDoc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValidDoc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubmittedDoc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopy, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSendInvoices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblState, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintInvoices, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotUploadedColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotUploaded, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvalidDocColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInvalidDoc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValidDocColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancelled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSubmittedDocColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCancelledDocColor, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboState).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranches).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
