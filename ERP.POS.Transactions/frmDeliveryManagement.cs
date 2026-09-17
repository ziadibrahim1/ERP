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
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmDeliveryManagement : frmBase
{
	private DataTable dtDeliveryMan;

	private DataTable dtDetails;

	private DataTable dtReports;

	private DataTable dtRoomData;

	private ValueList vlDeliveryMan = new ValueList();

	private int RoomID;

	private int DeliveryManID = 0;

	private IContainer components = null;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraGrid ULGData;

	private UltraButton btnNew;

	private Timer timer1;

	public UltraLabel NotFullySent;

	public UltraLabel lblNotFullySentColor;

	public UltraLabel late;

	public UltraLabel lblLateColor;

	public frmDeliveryManagement(int roomID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		RoomID = roomID;
	}

	public override void PrepareData()
	{
		dtReports = BusinessLayer.Privilege.Reports.FillComboByFormName("frmDelivery", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtDeliveryMan = DeliveryMan.FillCombo("," + GlobalVariables.CurrentBranchID + ",", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlDeliveryMan.ValueListItems.Clear();
		for (int i = 0; i < dtDeliveryMan.Rows.Count; i++)
		{
			vlDeliveryMan.ValueListItems.Add(dtDeliveryMan.Rows[i]["DeliveryManID"], dtDeliveryMan.Rows[i]["DeliveryManName"].ToString());
		}
		InitGrid();
		timer1.Start();
	}

	private void InitGrid()
	{
		dtDetails = Checks.SelectForDeliveryManagement(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "العنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryManID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryManID"].Header).Caption = (GlobalVariables.IsArabic ? "الطيار" : "Delivery Man");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryManID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryManID"].ValueList = (IValueList)(object)vlDeliveryMan;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryStartDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryStartDate"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "Departure");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryStartDate"].MaskInput = "hh:mm:ss tt";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliveryStartDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافى" : "Net Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Print"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(1, "Print");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Header).Caption = (GlobalVariables.IsArabic ? "طباعة" : "Print");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Print"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 2));
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Update"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Update");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].Header).Caption = (GlobalVariables.IsArabic ? "تعديل" : "Update");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Update"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		Color backColor = ((ControlBase)lblNotFullySentColor).Appearance.BackColor;
		Color backColor2 = ((ControlBase)lblLateColor).Appearance.BackColor;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Print"].Value = (GlobalVariables.IsArabic ? "طباعة" : "Print");
			((UltraGridBase)ULGData).Rows[i].Cells["Update"].Value = (GlobalVariables.IsArabic ? "تعديل" : "Update");
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["NotFullySent"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["DeliveryManID"].Value == DBNull.Value)
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = backColor;
			}
			else if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Late"].Value.ToString()) && ((UltraGridBase)ULGData).Rows[i].Cells["DeliveryManID"].Value == DBNull.Value)
			{
				((AppearanceBase)((UltraGridBase)ULGData).Rows[i].Appearance).BackColor = backColor2;
			}
		}
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		string key = ((KeyedSubObjectBase)e.Cell.Column).Key;
		DataRow dataRow = ((DataTable)((UltraGridBase)ULGData).DataSource).Copy().Select(" CheckID= " + e.Cell.Row.Cells["CheckID"].Value.ToString())[0];
		if (dataRow == null)
		{
			return;
		}
		if (key == "Print")
		{
			if (dataRow["DeliveryManID"] == DBNull.Value || dataRow["DeliveryManID"] == null)
			{
				frmDeliveryManSelector frmDeliveryManSelector2 = new frmDeliveryManSelector();
				frmDeliveryManSelector2.WindowState = FormWindowState.Normal;
				frmDeliveryManSelector2.ShowDialog();
				if (frmDeliveryManSelector2.Cancel || frmDeliveryManSelector2.DeliveryManID == 0)
				{
					return;
				}
				DeliveryManID = frmDeliveryManSelector2.DeliveryManID;
			}
			else
			{
				DeliveryManID = int.Parse(dataRow["DeliveryManID"].ToString());
			}
			if (dataRow["IsInvoice"] != null && bool.Parse(dataRow["IsInvoice"].ToString()))
			{
				GlobalVariables.ReportDocument = new ReportDocument();
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_Invoice_A.rpt" : "Rep_POS_Invoice_E.rpt"));
				frmReporViwer frmReporViwer2 = new frmReporViwer();
				GlobalVariables.ReportDocument.SetParameterValue("@CheckIDs", "," + dataRow["CheckID"].ToString() + ",");
				GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
				frmReporViwer2.ShowDialog();
				frmReporViwer2 = null;
				GlobalVariables.ReportDocument = null;
				Checks.UpdatePrintDeliveryData(dataRow["CheckID"].ToString(), DeliveryManID.ToString(), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "1", GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate));
			}
			else
			{
				ReportDocument reportDocument = new ReportDocument();
				DataRow[] array = dtReports.Select("ReportID=" + (dtRoomData.Rows[0]["DeliveryReportID"].Equals(DBNull.Value) ? "0" : dtRoomData.Rows[0]["DeliveryReportID"]));
				if (array.Length != 0)
				{
					if (array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString().Trim() == "FastPrintDeliveryDetails")
					{
						FastPrintCheck(dataRow["CheckID"].ToString());
						return;
					}
					reportDocument.Load(GlobalVariables.ReportsPath + array[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				}
				else
				{
					reportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_POS_ChecksDelivery_A.rpt" : "Rep_POS_ChecksDelivery_E.rpt"));
				}
				GlobalFunctions.ConfigureReport(reportDocument);
				reportDocument.SetParameterValue("@CheckIDs", "," + dataRow["CheckID"].ToString() + ",");
				reportDocument.SetParameterValue("@RoomID", RoomID.ToString());
				reportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
				reportDocument.PrintOptions.PrinterName = GlobalVariables.POSPrinter;
				try
				{
					reportDocument.PrintToPrinter(1, collated: true, 0, 10000);
					Checks.UpdatePrintDeliveryData(dataRow["CheckID"].ToString(), DeliveryManID.ToString(), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "1", GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate));
				}
				catch (Exception ex)
				{
					GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
				}
				reportDocument.Dispose();
			}
			GC.Collect();
			InitGrid();
		}
		else if (key == "Update")
		{
			frmDeliveryChecks frmDeliveryChecks2 = new frmDeliveryChecks(int.Parse(dataRow["CheckID"].ToString()), RoomID, 0, 0, 0m);
			frmDeliveryChecks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			frmDeliveryChecks2.Tag = base.Tag;
			frmDeliveryChecks2.Location = new Point(0, 0);
			frmDeliveryChecks2.CanAdd = CanAdd;
			frmDeliveryChecks2.CanUpdate = CanUpdate;
			frmDeliveryChecks2.CanDelete = CanDelete;
			frmDeliveryChecks2.CanDiscount = CanDiscount;
			frmDeliveryChecks2.CanSearching = CanSearching;
			frmDeliveryChecks2.CanExport = CanExport;
			frmDeliveryChecks2.CanPrint = CanPrint;
			frmDeliveryChecks2.CanPrintReport = CanPrintReport;
			frmDeliveryChecks2.CanViewReport = CanViewReport;
			frmDeliveryChecks2.CanMinimunCharge = CanMinimunCharge;
			frmDeliveryChecks2.ShowDialog();
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGData.ActiveCell).Selected = true;
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		if (ULGData != null && ((Control)(object)ULGData).Width > 0)
		{
			InitGrid();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		frmDeliveryClientsSearch frmDeliveryClientsSearch2 = new frmDeliveryClientsSearch(RoomID);
		((Control)(object)frmDeliveryClientsSearch2.lblTitle).Text = (GlobalVariables.IsArabic ? "عملاء التوصيل للمنازل" : "Delivery Clients");
		frmDeliveryClientsSearch2.Tag = base.Tag;
		frmDeliveryClientsSearch2.Location = new Point(0, 0);
		frmDeliveryClientsSearch2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDeliveryClientsSearch2.StartPosition = FormStartPosition.CenterParent;
		frmDeliveryClientsSearch2.CanAdd = CanAdd;
		frmDeliveryClientsSearch2.CanUpdate = CanUpdate;
		frmDeliveryClientsSearch2.CanDelete = CanDelete;
		frmDeliveryClientsSearch2.CanDiscount = CanDiscount;
		frmDeliveryClientsSearch2.CanSearching = CanSearching;
		frmDeliveryClientsSearch2.CanExport = CanExport;
		frmDeliveryClientsSearch2.CanPrint = CanPrint;
		frmDeliveryClientsSearch2.CanPrintReport = CanPrintReport;
		frmDeliveryClientsSearch2.CanViewReport = CanViewReport;
		frmDeliveryClientsSearch2.CanMinimunCharge = CanMinimunCharge;
		frmDeliveryClientsSearch2.ShowDialog();
	}

	public void FastPrintCheck(string CurrentRowID)
	{
		DataTable dataTable = Main.ExecuteQuery_DataTable(" Rep_POS_Checks '," + CurrentRowID + ",'," + (GlobalVariables.IsArabic ? "1" : "0"));
		DataRow dataRow = dataTable.Rows[0];
		Font font = new Font("Times New Roman", 10f, FontStyle.Bold);
		Font font2 = new Font("Times New Roman", 8f, FontStyle.Bold);
		Font font3 = new Font("Times New Roman", 8f, FontStyle.Regular);
		Font font4 = new Font("Times New Roman", 9f, FontStyle.Regular);
		Font font5 = new Font("Times New Roman", 9f, FontStyle.Bold);
		Font font6 = new Font("Times New Roman", 10f, FontStyle.Bold);
		FastPrint instance = FastPrint.Instance;
		instance.PrinterSettings.PrinterName = GlobalVariables.POSPrinter;
		instance.GraphicsUnit = GraphicsUnit.Millimeter;
		instance.Margins = new Margins(0, 0, 0, 0);
		instance.OverallWidth = 70f;
		instance.AddTextCell(dtRoomData.Rows[0]["RoomName"].ToString(), font, 1f, 7f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (!Convert.ToBoolean(dtRoomData.Rows[0]["PrintWithoutCheckNo"]))
		{
			instance.AddTextCell(dataRow["CheckNo"].ToString(), font4, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("شيك رقم", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		instance.AddTextCell(dataRow["CheckDate"].ToString(), font4, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("بتاريخ", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		instance.AddTextCell(GlobalVariables.UserName, font4, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("المستخدم", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		instance.AddTextCell(dataRow["ClientName"].ToString(), font4, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AddTextCell("العميل", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
		instance.AcceptChanges();
		if (dataRow["MobileNumber"] != DBNull.Value)
		{
			instance.AddTextCell(dataRow["MobileNumber"].ToString(), font4, 0.76f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("رقم المحمول", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		if (dataRow["Notes"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dataRow["Notes"].ToString().Trim(), font4, 0.76f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AddTextCell("ملاحظات", font4, 0.24f, 5f, StringAlignment.Near, Color.Black, DrawRectangle: true, Color.Gray);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell("إجمالي", font2, 0.18f, 5f);
		instance.AddTextCell("سعر الوحده", font2, 0.18f, 5f);
		instance.AddTextCell("الصنف", font2, 0.5f, 5f);
		instance.AddTextCell("الكميه", font2, 0.14f, 5f);
		instance.AcceptChanges();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			instance.AddTextCell(decimal.Parse(dataTable.Rows[i]["TotalPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.18f, 4f);
			instance.AddTextCell(decimal.Parse(dataTable.Rows[i]["UnitPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font3, 0.18f, 4f);
			instance.AddTextCell(dataTable.Rows[i]["ItemName"].ToString(), font3, 0.5f, 4f);
			instance.AddTextCell(decimal.Parse(dataTable.Rows[i]["Qty"].ToString(), NumberStyles.Float).ToString("0.##"), font3, 0.14f, 4f);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 1f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
		instance.AddTextCell(decimal.Parse(dataRow["GrossValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
		instance.AddTextCell("الإجمالي", font5, 0.5f, 5f, StringAlignment.Far);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["DiscountBeforeTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("خصم", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["TaxTotalValue"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["TaxTotalValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("الضريبه", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["DiscountAfterTaxValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("خصم2", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["RoundingValue"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["RoundingValue"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("تقريب", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
		instance.AddTextCell(decimal.Parse(dataRow["NetPrice"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: true, Color.Black, 0.5f);
		instance.AddTextCell("الصافي", font6, 0.5f, 5f, StringAlignment.Far, Color.Black, DrawRectangle: true, Color.Black, 0.5f);
		instance.AcceptChanges();
		if (decimal.Parse(dataRow["PaidAmount"].ToString()) > 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["PaidAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("المدفوع", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		if (decimal.Parse(dataRow["RestAmount"].ToString()) != 0m)
		{
			instance.AddEmptyCell(0.1f, 5f, DrawRectangle: false);
			instance.AddTextCell(decimal.Parse(dataRow["RestAmount"].ToString(), NumberStyles.Currency).ToString("0.00"), font5, 0.3f, 5f);
			instance.AddTextCell("المتبقى", font5, 0.5f, 5f, StringAlignment.Far);
			instance.AcceptChanges();
		}
		instance.AddEmptyCell(1f, 2f, DrawRectangle: false);
		instance.AcceptChanges();
		instance.AddTextCell(DateTime.Now.ToString(), font3, 1f, 5f, StringAlignment.Center, Color.Black, DrawRectangle: false);
		instance.AcceptChanges();
		if (dtRoomData.Rows[0]["Message"].ToString().Trim().Length > 0)
		{
			instance.AddTextCell(dtRoomData.Rows[0]["Message"].ToString(), font3, 1f, 6f, StringAlignment.Center, Color.Black, DrawRectangle: false);
			instance.AcceptChanges();
		}
		try
		{
			instance.PrinterSettings.Copies = short.Parse(dtRoomData.Rows[0]["PrintClosingCheckCount"].ToString());
			if (instance.PrinterSettings.Copies > 0)
			{
				instance.Print();
			}
			Checks.UpdatePrintDeliveryData(dataRow["CheckID"].ToString(), DeliveryManID.ToString(), GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "1", GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate));
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show("تأكد من وصلات الطابعة  \n" + ex.Message, "Check Printer Cable\n" + ex.Message);
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmDeliveryManagement));
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
		Appearance val17 = new Appearance();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.ULGData = new UltraGrid();
		this.btnNew = new UltraButton();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.NotFullySent = new UltraLabel();
		this.lblNotFullySentColor = new UltraLabel();
		this.late = new UltraLabel();
		this.lblLateColor = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val4).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance4");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val5;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val8;
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
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseAppStyling = false;
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		resources.ApplyResources(this.btnNew, "btnNew");
		((System.Windows.Forms.Control)(object)this.btnNew).Name = "btnNew";
		((System.Windows.Forms.Control)(object)this.btnNew).Click += new System.EventHandler(btnNew_Click);
		this.timer1.Interval = 3000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this.NotFullySent, "NotFullySent");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.NotFullySent).Appearance = (AppearanceBase)(object)val14;
		this.NotFullySent.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.NotFullySent).Name = "NotFullySent";
		resources.ApplyResources(this.lblNotFullySentColor, "lblNotFullySentColor");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Yellow;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblNotFullySentColor).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblNotFullySentColor).Name = "lblNotFullySentColor";
		((UltraControlBase)this.lblNotFullySentColor).UseAppStyling = false;
		resources.ApplyResources(this.late, "late");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.late).Appearance = (AppearanceBase)(object)val16;
		this.late.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.late).Name = "late";
		((UltraControlBase)this.late).UseAppStyling = false;
		resources.ApplyResources(this.lblLateColor, "lblLateColor");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Red;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblLateColor).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.lblLateColor).Name = "lblLateColor";
		((UltraControlBase)this.lblLateColor).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.NotFullySent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotFullySentColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.late);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLateColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmDeliveryManagement";
		base.ShowInTaskbar = true;
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLateColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.late, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotFullySentColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.NotFullySent, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
