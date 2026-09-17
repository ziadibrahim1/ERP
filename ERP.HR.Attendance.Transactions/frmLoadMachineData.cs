using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
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
using ZK;

namespace ERP.HR.Attendance.Transactions;

public class frmLoadMachineData : frmBase
{
	private DataTable dtMachineTypes;

	private DataTable dtDetails;

	private DataTable dtAtt;

	private DataTable dtMachinesLastDate;

	private ValueList vlMachineTypes = new ValueList();

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnGetAtt;

	public UltraButton btnGetAttendance;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraGrid ULGAttendance;

	public UltraDateTimeEditor dtpMToDate;

	public UltraLabel ultraLabel2;

	private RadioButton rbFromFile;

	private RadioButton rbDirect;

	public UltraLabel ultraLabel1;

	public UltraDateTimeEditor dtpFFromDate;

	public UltraLabel ultraLabel3;

	public UltraDateTimeEditor dtpFToDate;

	private UltraTextEditor txtFilePath;

	private UltraButton btnFilePath;

	private OpenFileDialog openFileDialog1;

	public frmLoadMachineData()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpMToDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpFFromDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpFToDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpMToDate.DateTime = DateTime.Now;
		dtAtt = new DataTable();
		dtAtt.Columns.Add("ID", typeof(int));
		dtAtt.Columns.Add("EmployeeNo", typeof(string));
		dtAtt.Columns.Add("IsLogout", typeof(bool));
		dtAtt.Columns.Add("DateTime", typeof(DateTime));
		dtAtt.Columns.Add("Date", typeof(DateTime));
		dtAtt.Columns["DateTime"].DateTimeMode = DataSetDateTime.Unspecified;
		dtAtt.Columns["Date"].DateTimeMode = DataSetDateTime.Unspecified;
		dtMachinesLastDate = new DataTable();
		dtMachinesLastDate.Columns.Add("MachineID", typeof(int));
		dtMachinesLastDate.Columns.Add("LastGetDate", typeof(DateTime));
		dtMachineTypes = AttMachineTypes.Select("-1", "-1", "1", IsFromServer: true);
		vlMachineTypes.ValueListItems.Clear();
		for (int i = 0; i < dtMachineTypes.Rows.Count; i++)
		{
			vlMachineTypes.ValueListItems.Add(dtMachineTypes.Rows[i]["MachineTypeID"], dtMachineTypes.Rows[i]["MachineTypeName"].ToString());
		}
		dtDetails = AttMachines.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtDetails.Columns.Add("Select", typeof(bool));
		for (int j = 0; j < dtDetails.Rows.Count; j++)
		{
			dtDetails.Rows[j]["Select"] = false;
		}
		((UltraGridBase)ULGAttendance).DataSource = dtAtt;
		InitGridAtt();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNumper"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNumper"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNumper"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.14);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.14);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IP"].Header).Caption = "IP";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IP"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IP"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Port"].Header).Caption = "Port";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Port"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Port"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].ValueList = (IValueList)(object)vlMachineTypes;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].Header).Caption = (GlobalVariables.IsArabic ? "اخر تحميل" : "Last Load");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.17);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].MaskInput = "dd/mm/yyyy hh:mm";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Header).Caption = (GlobalVariables.IsArabic ? "اختيار" : "Select");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
	}

	public void InitGridAtt()
	{
		GlobalFunctions.PrepareGrid(ULGAttendance);
		((UltraGridBase)ULGAttendance).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)ULGAttendance).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGAttendance).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((HeaderBase)((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["EmployeeNo"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "EmpNo");
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["EmployeeNo"].Hidden = false;
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["EmployeeNo"].Width = (int)((double)((Control)(object)ULGAttendance).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["IsLogout"].Header).Caption = (GlobalVariables.IsArabic ? "خروج" : "check out");
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["IsLogout"].Hidden = false;
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["IsLogout"].Width = (int)((double)((Control)(object)ULGAttendance).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["DateTime"].Header).Caption = (GlobalVariables.IsArabic ? "الوقت" : "Time");
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["DateTime"].Hidden = false;
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["DateTime"].Width = (int)((double)((Control)(object)ULGAttendance).Width * 0.2);
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["DateTime"].MaskInput = "hh:mm";
		((HeaderBase)((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["Date"].Width = (int)((double)((Control)(object)ULGAttendance).Width * 0.3);
		((UltraGridBase)ULGAttendance).DisplayLayout.Bands[0].Columns["Date"].MaskInput = "dd/mm/yyyy";
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Select")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	private void btnFilePath_Click(object sender, EventArgs e)
	{
		if (openFileDialog1.ShowDialog() == DialogResult.OK)
		{
			((Control)(object)txtFilePath).Text = openFileDialog1.FileName;
		}
	}

	private void rbFromFile_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)btnFilePath).Enabled = rbFromFile.Checked;
		((Control)(object)txtFilePath).Enabled = rbFromFile.Checked;
		((Control)(object)dtpFFromDate).Enabled = rbFromFile.Checked;
		((Control)(object)dtpFToDate).Enabled = rbFromFile.Checked;
		((Control)(object)dtpMToDate).Enabled = rbDirect.Checked;
		((Control)(object)ULGData).Enabled = rbDirect.Checked;
	}

	private void btnGetAttendance_Click(object sender, EventArgs e)
	{
		string text = "";
		Cursor = Cursors.WaitCursor;
		dtAtt.Rows.Clear();
		if (rbFromFile.Checked)
		{
			if (((Control)(object)txtFilePath).Text == "")
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اي ملف  ", "There is no choosen File , please select a File ");
				return;
			}
			if (dtpFFromDate.Value == null)
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار من تاريخ  ", "please select From Date");
				return;
			}
			if (dtpFToDate.Value == null)
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار حتي تاريخ  ", "please select To Date");
				return;
			}
			if (((Control)(object)txtFilePath).Text.EndsWith(".mdb"))
			{
				OleDbConnection oleDbConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + ((Control)(object)txtFilePath).Text + ";Persist Security Info=True");
				try
				{
					OleDbCommand oleDbCommand = new OleDbCommand();
					oleDbCommand.Connection = oleDbConnection;
					oleDbCommand.CommandText = "PARAMETERS [@FromDate] datetime ,[@ToDate] datetime  ;SELECT USERINFO.Badgenumber as [ID],USERINFO.Badgenumber as EmployeeNo, CHECKINOUT.CHECKTYPE , CHECKINOUT.CHECKTIME as [DateTime],datevalue(CHECKINOUT.CHECKTIME ) as [Date]   FROM  USERINFO INNER JOIN CHECKINOUT ON USERINFO.USERID = CHECKINOUT.USERID  where CHECKINOUT.CHECKTIME >= [@FromDate] And CHECKINOUT.CHECKTIME <= [@ToDate]";
					oleDbCommand.Parameters.Add(new OleDbParameter("@FromDate", dtpFFromDate.DateTime));
					oleDbCommand.Parameters.Add(new OleDbParameter("@ToDate", dtpFToDate.DateTime));
					oleDbConnection.Open();
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);
					oleDbDataAdapter.Fill(dtAtt);
					for (int i = 0; i < dtAtt.Rows.Count; i++)
					{
						dtAtt.Rows[i]["IsLogout"] = !dtAtt.Rows[i]["CHECKTYPE"].Equals("I");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Failed to connect to data source \n" + ex.Message);
				}
				finally
				{
					oleDbConnection.Close();
				}
			}
			else if (((Control)(object)txtFilePath).Text.EndsWith(".txt"))
			{
				StreamReader streamReader = new StreamReader(((Control)(object)txtFilePath).Text, Encoding.Default);
				string text2 = streamReader.ReadToEnd().Trim();
				string[] array = text2.Split('\n');
				for (int j = 0; j < array.Length; j++)
				{
					string[] array2 = Regex.Split(array[j].Trim(), "\\s+", RegexOptions.None);
					DateTime result = default(DateTime);
					if (array2.Length > 7 && DateTime.TryParse(array2[array2.Length - 2] + " " + array2[array2.Length - 1], out result) && result > dtpFFromDate.DateTime.AddDays(-1.0) && result < dtpFToDate.DateTime.AddDays(1.0))
					{
						dtAtt.Rows.Add(array2[2].TrimStart('0'), array2[2].TrimStart('0'), false, array2[array2.Length - 2] + " " + array2[array2.Length - 1], array2[array2.Length - 2]);
					}
				}
			}
			else
			{
				StreamReader streamReader2 = new StreamReader(((Control)(object)txtFilePath).Text, Encoding.Default);
				string text3 = streamReader2.ReadToEnd().Trim();
				string[] array3 = text3.Split('\n');
				for (int k = 0; k < array3.Length; k++)
				{
					string[] array4 = Regex.Split(array3[k].Trim(), "\\s+", RegexOptions.None);
					DateTime result2 = default(DateTime);
					if (DateTime.TryParse(array4[1] + " " + array4[2], out result2) && result2 > dtpFFromDate.DateTime.AddDays(-1.0) && result2 < dtpFToDate.DateTime.AddDays(1.0))
					{
						dtAtt.Rows.Add(array4[0], array4[0], array4[4] == "1", array4[1] + " " + array4[2], array4[1]);
					}
				}
			}
		}
		else
		{
			if (dtDetails.Select("Select=1").Length == 0)
			{
				GlobalVariables.InformationMB.Show("لم تقم باختيار اي ماكينه  ", "There is no choosen Machine , please select a Machine ");
				return;
			}
			dtMachinesLastDate.Rows.Clear();
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				if (!((UltraGridBase)ULGData).Rows[l].Cells["Select"].Value.Equals(true))
				{
					continue;
				}
				if (((UltraGridBase)ULGData).Rows[l].Cells["MachineTypeID"].Value.Equals(1))
				{
					bool flag = false;
					DataTable dataTable = new DataTable();
					try
					{
						dataTable = Attendance.ZK_BlackAndWhite(((UltraGridBase)ULGData).Rows[l].Cells["IP"].Value.ToString(), Convert.ToInt32(((UltraGridBase)ULGData).Rows[l].Cells["Port"].Value), ref flag);
					}
					catch
					{
						flag = true;
					}
					if (flag)
					{
						text += (GlobalVariables.IsArabic ? string.Concat("فشل في تنزيل البيانات من الماكينه رقم \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n") : string.Concat("Download Data Fail For Machine Number \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n"));
						continue;
					}
					DateTime dateTime = ((Convert.ToDateTime(dataTable.Rows[dataTable.Rows.Count - 1]["DateTime"]) > dtpMToDate.DateTime) ? dtpMToDate.DateTime : Convert.ToDateTime(dataTable.Rows[dataTable.Rows.Count - 1]["DateTime"]));
					DateTime serverDateTimeNow = GlobalFunctions.GetServerDateTimeNow();
					dateTime = ((dateTime > serverDateTimeNow) ? serverDateTimeNow : dateTime);
					DataView dataView = new DataView(dataTable);
					dataView.RowFilter = string.Concat("DateTime<='", dateTime, "'", (((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value == DBNull.Value) ? "" : string.Concat(" And DateTime >'", Convert.ToDateTime(((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value).AddDays(-1.0), "'"));
					dataView.RowStateFilter = DataViewRowState.CurrentRows;
					dtAtt.Merge(dataView.ToTable());
					dtMachinesLastDate.Rows.Add(((UltraGridBase)ULGData).Rows[l].Cells["MachineID"].Value, dateTime);
				}
				else if (((UltraGridBase)ULGData).Rows[l].Cells["MachineTypeID"].Value.Equals(2))
				{
					bool flag2 = false;
					DataTable dataTable2 = new DataTable();
					try
					{
						dataTable2 = Attendance.ZK_TFT(((UltraGridBase)ULGData).Rows[l].Cells["IP"].Value.ToString(), Convert.ToInt32(((UltraGridBase)ULGData).Rows[l].Cells["Port"].Value), ref flag2);
					}
					catch
					{
						flag2 = true;
					}
					if (flag2)
					{
						text += (GlobalVariables.IsArabic ? string.Concat("فشل في تنزيل البيانات من الماكينه رقم \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n") : string.Concat("Download Data Fail For Machine Number \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n"));
						continue;
					}
					DateTime dateTime2 = ((Convert.ToDateTime(dataTable2.Rows[dataTable2.Rows.Count - 1]["DateTime"]) > dtpMToDate.DateTime) ? dtpMToDate.DateTime : Convert.ToDateTime(dataTable2.Rows[dataTable2.Rows.Count - 1]["DateTime"]));
					DateTime serverDateTimeNow2 = GlobalFunctions.GetServerDateTimeNow();
					dateTime2 = ((dateTime2 > serverDateTimeNow2) ? serverDateTimeNow2 : dateTime2);
					DataView dataView2 = new DataView(dataTable2);
					dataView2.RowFilter = string.Concat("DateTime<='", dateTime2, "'", (((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value == DBNull.Value) ? "" : string.Concat(" And DateTime >'", Convert.ToDateTime(((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value).AddDays(-1.0), "'"));
					dataView2.RowStateFilter = DataViewRowState.CurrentRows;
					dtAtt.Merge(dataView2.ToTable());
					dtMachinesLastDate.Rows.Add(((UltraGridBase)ULGData).Rows[l].Cells["MachineID"].Value, dateTime2);
				}
				else if (((UltraGridBase)ULGData).Rows[l].Cells["MachineTypeID"].Value.Equals(3))
				{
					bool flag3 = false;
					DataTable dataTable3 = new DataTable();
					try
					{
						dataTable3 = Attendance.ZK_Face(((UltraGridBase)ULGData).Rows[l].Cells["IP"].Value.ToString(), Convert.ToInt32(((UltraGridBase)ULGData).Rows[l].Cells["Port"].Value), ref flag3);
					}
					catch
					{
						flag3 = true;
					}
					if (flag3)
					{
						text += (GlobalVariables.IsArabic ? string.Concat("فشل في تنزيل البيانات من الماكينه رقم \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n") : string.Concat("Download Data Fail For Machine Number \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n"));
						continue;
					}
					DateTime dateTime3 = ((Convert.ToDateTime(dataTable3.Rows[dataTable3.Rows.Count - 1]["DateTime"]) > dtpMToDate.DateTime) ? dtpMToDate.DateTime : Convert.ToDateTime(dataTable3.Rows[dataTable3.Rows.Count - 1]["DateTime"]));
					DateTime serverDateTimeNow3 = GlobalFunctions.GetServerDateTimeNow();
					dateTime3 = ((dateTime3 > serverDateTimeNow3) ? serverDateTimeNow3 : dateTime3);
					DataView dataView3 = new DataView(dataTable3);
					dataView3.RowFilter = string.Concat("DateTime<='", dateTime3, "'", (((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value == DBNull.Value) ? "" : string.Concat(" And DateTime >'", Convert.ToDateTime(((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value).AddDays(-1.0), "'"));
					dataView3.RowStateFilter = DataViewRowState.CurrentRows;
					dtAtt.Merge(dataView3.ToTable());
					dtMachinesLastDate.Rows.Add(((UltraGridBase)ULGData).Rows[l].Cells["MachineID"].Value, dateTime3);
				}
				else if (((UltraGridBase)ULGData).Rows[l].Cells["MachineTypeID"].Value.Equals(4))
				{
					bool flag4 = false;
					DataTable dataTable4 = new DataTable();
					try
					{
						dataTable4 = E_StrongAttDll.GetAtt(((UltraGridBase)ULGData).Rows[l].Cells["IP"].Value.ToString(), Convert.ToInt32(((UltraGridBase)ULGData).Rows[l].Cells["Port"].Value), 0);
					}
					catch
					{
						flag4 = true;
					}
					if (flag4)
					{
						text += (GlobalVariables.IsArabic ? string.Concat("فشل في تنزيل البيانات من الماكينه رقم \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n") : string.Concat("Download Data Fail For Machine Number \n", ((UltraGridBase)ULGData).Rows[l].Cells["MachineNumper"].Value, "\n"));
						continue;
					}
					DateTime dateTime4 = ((Convert.ToDateTime(dataTable4.Rows[dataTable4.Rows.Count - 1]["DateTime"]) > dtpMToDate.DateTime) ? dtpMToDate.DateTime : Convert.ToDateTime(dataTable4.Rows[dataTable4.Rows.Count - 1]["DateTime"]));
					DateTime serverDateTimeNow4 = GlobalFunctions.GetServerDateTimeNow();
					dateTime4 = ((dateTime4 > serverDateTimeNow4) ? serverDateTimeNow4 : dateTime4);
					DataView dataView4 = new DataView(dataTable4);
					dataView4.RowFilter = string.Concat("DateTime<='", dateTime4, "'", (((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value == DBNull.Value) ? "" : string.Concat(" And DateTime >'", Convert.ToDateTime(((UltraGridBase)ULGData).Rows[l].Cells["LastGetDate"].Value).AddDays(-1.0), "'"));
					dataView4.RowStateFilter = DataViewRowState.CurrentRows;
					dtAtt.Merge(dataView4.ToTable());
					dtMachinesLastDate.Rows.Add(((UltraGridBase)ULGData).Rows[l].Cells["MachineID"].Value, dateTime4);
				}
			}
		}
		((UltraGridBase)ULGAttendance).DataSource = dtAtt;
		InitGridAtt();
		Cursor = Cursors.Default;
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text);
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (dtAtt.Rows.Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لاتوجد سجلات للحفظ  " : "No Attendance to be saved");
			return;
		}
		bool flag = false;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			if (GlobalFunctions.GetOption("IgnoreAttendanceMachineInOutValue"))
			{
				EmployeesAttendance.UpdateAttendanceMachineXMLIgnoreInOut(dtAtt, GlobalVariables.CurrentBranchID, GlobalVariables.UserID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			}
			else
			{
				EmployeesAttendance.UpdateAttendanceMachineXML(dtAtt, GlobalVariables.CurrentBranchID, GlobalVariables.UserID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			}
			for (int i = 0; i < dtMachinesLastDate.Rows.Count; i++)
			{
				AttMachines.UpdateLastGetDate(dtMachinesLastDate.Rows[i]["MachineID"].ToString(), dtMachinesLastDate.Rows[i]["LastGetDate"].ToString(), IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			flag = true;
			return;
		}
		if (!flag)
		{
			dtAtt.Rows.Clear();
			dtMachinesLastDate.Rows.Clear();
			dtDetails = AttMachines.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtDetails.Columns.Add("Select", typeof(bool));
			for (int j = 0; j < dtDetails.Rows.Count; j++)
			{
				dtDetails.Rows[j]["Select"] = false;
			}
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Attendance.Transactions.frmLoadMachineData));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnGetAtt = new UltraButton();
		this.btnGetAttendance = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.ULGAttendance = new UltraGrid();
		this.dtpMToDate = new UltraDateTimeEditor();
		this.ultraLabel2 = new UltraLabel();
		this.rbFromFile = new System.Windows.Forms.RadioButton();
		this.rbDirect = new System.Windows.Forms.RadioButton();
		this.ultraLabel1 = new UltraLabel();
		this.dtpFFromDate = new UltraDateTimeEditor();
		this.ultraLabel3 = new UltraLabel();
		this.dtpFToDate = new UltraDateTimeEditor();
		this.txtFilePath = new UltraTextEditor();
		this.btnFilePath = new UltraButton();
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGAttendance).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFilePath).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		resources.ApplyResources(this.btnGetAtt, "btnGetAtt");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnGetAtt).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnGetAtt).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnGetAtt).Name = "btnGetAtt";
		((System.Windows.Forms.Control)(object)this.btnGetAtt).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnGetAttendance, "btnGetAttendance");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnGetAttendance).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnGetAttendance).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnGetAttendance).Name = "btnGetAttendance";
		((System.Windows.Forms.Control)(object)this.btnGetAttendance).Click += new System.EventHandler(btnGetAttendance_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.ULGAttendance, "ULGAttendance");
		((UltraGridBase)this.ULGAttendance).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGAttendance).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGAttendance).Name = "ULGAttendance";
		((UltraControlBase)this.ULGAttendance).UseAppStyling = false;
		((UltraControlBase)this.ULGAttendance).UseOsThemes = (DefaultableBoolean)2;
		resources.ApplyResources(this.dtpMToDate, "dtpMToDate");
		this.dtpMToDate.DateTime = new System.DateTime(2019, 11, 24, 0, 0, 0, 0);
		this.dtpMToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpMToDate).Name = "dtpMToDate";
		this.dtpMToDate.PromptChar = ' ';
		this.dtpMToDate.Value = new System.DateTime(2019, 11, 24, 0, 0, 0, 0);
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val5;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.rbFromFile, "rbFromFile");
		this.rbFromFile.BackColor = System.Drawing.Color.Transparent;
		this.rbFromFile.Name = "rbFromFile";
		this.rbFromFile.UseVisualStyleBackColor = false;
		this.rbFromFile.CheckedChanged += new System.EventHandler(rbFromFile_CheckedChanged);
		resources.ApplyResources(this.rbDirect, "rbDirect");
		this.rbDirect.BackColor = System.Drawing.Color.Transparent;
		this.rbDirect.Checked = true;
		this.rbDirect.Name = "rbDirect";
		this.rbDirect.TabStop = true;
		this.rbDirect.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.dtpFFromDate, "dtpFFromDate");
		this.dtpFFromDate.DateTime = new System.DateTime(2019, 11, 24, 0, 0, 0, 0);
		this.dtpFFromDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpFFromDate).Name = "dtpFFromDate";
		this.dtpFFromDate.PromptChar = ' ';
		this.dtpFFromDate.Value = new System.DateTime(2019, 11, 24, 0, 0, 0, 0);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.ultraLabel3).Appearance = (AppearanceBase)(object)val7;
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this.dtpFToDate, "dtpFToDate");
		this.dtpFToDate.DateTime = new System.DateTime(2019, 11, 24, 0, 0, 0, 0);
		this.dtpFToDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpFToDate).Name = "dtpFToDate";
		this.dtpFToDate.PromptChar = ' ';
		this.dtpFToDate.Value = new System.DateTime(2019, 11, 24, 0, 0, 0, 0);
		resources.ApplyResources(this.txtFilePath, "txtFilePath");
		((System.Windows.Forms.Control)(object)this.txtFilePath).Name = "txtFilePath";
		resources.ApplyResources(this.btnFilePath, "btnFilePath");
		((System.Windows.Forms.Control)(object)this.btnFilePath).Name = "btnFilePath";
		((System.Windows.Forms.Control)(object)this.btnFilePath).Click += new System.EventHandler(btnFilePath_Click);
		this.openFileDialog1.DefaultExt = "dat";
		resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFilePath);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnFilePath);
		base.Controls.Add(this.rbFromFile);
		base.Controls.Add(this.rbDirect);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpMToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGetAttendance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnGetAtt);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGAttendance);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmLoadMachineData";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGAttendance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGetAtt, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnGetAttendance, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpMToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFToDate, 0);
		base.Controls.SetChildIndex(this.rbDirect, 0);
		base.Controls.SetChildIndex(this.rbFromFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnFilePath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFilePath, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGAttendance).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFilePath).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
