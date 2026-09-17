using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTree;

namespace ERP.SystemOptions.GeneralData;

public class frmLines : frmHeaderManyDetails
{
	private DataTable dtClients;

	private DataTable dtBranches;

	private DataTable dtClientsDetails;

	private DataTable dtEmployees;

	private DataTable dtDetailsSalesMen2;

	private ValueList vlEmployees1 = new ValueList();

	private ValueList vlEmployees2 = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraButton btnClientsSearch;

	public UltraTextEditor txtClients;

	public UltraTree TreeClients;

	protected internal UltraCheckEditor chkAll;

	private UltraTabPageControl ultraTabPageControl3;

	protected internal UltraGrid ULGDataSalesMen2;

	private UltraComboEditor cboBranchName;

	private UltraLabel lblBranch;

	public frmLines()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "G_Lines";
		IDCol = "LineID";
		NoCol = "LineCode";
		DateCol = "GetDate()";
		((UltraTabControlBase)UTCDetails).Tabs[0].Text = (GlobalVariables.IsArabic ? "رجل المبيعات" : "SalesMan");
	}

	public frmLines(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlEmployees1.ValueListItems.Clear();
		vlEmployees2.ValueListItems.Clear();
		for (int i = 0; i < dtEmployees.Rows.Count; i++)
		{
			vlEmployees1.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString());
			vlEmployees2.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString());
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranchName, dtBranches, "BranchID", "BranchName");
		dtClients = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtClients != null)
		{
			TreeFunctions.FillTree(TreeClients, dtClients, "ParentID", "SubAccountID", "SubAccountName", "SubAccountNumber", "IsMain");
		}
		dtDetails = LinesSalesMan1.SelectByLineID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtDetailsSalesMen2 = LinesSalesMan2.SelectByLineID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataSalesMen2).DataSource = dtDetailsSalesMen2;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataSalesMen2);
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineSalesMan1ID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Header).Caption = (GlobalVariables.IsArabic ? "الى" : "To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["EmployeeID"].ValueList = (IValueList)(object)vlEmployees1;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["LineSalesMan2ID"].DefaultCellValue = -1;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["EmployeeID"].Width = (int)((double)((Control)(object)ULGDataSalesMen2).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["FromDate"].Width = (int)((double)((Control)(object)ULGDataSalesMen2).Width * 0.25);
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["ToDate"].Width = (int)((double)((Control)(object)ULGDataSalesMen2).Width * 0.25);
		((HeaderBase)((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["EmployeeID"].Header).Caption = (GlobalVariables.IsArabic ? "الموظف" : "Employee");
		((HeaderBase)((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["FromDate"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From");
		((HeaderBase)((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["ToDate"].Header).Caption = (GlobalVariables.IsArabic ? "الى" : "To");
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["EmployeeID"].Hidden = false;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["FromDate"].Hidden = false;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["ToDate"].Hidden = false;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Bands[0].Columns["EmployeeID"].ValueList = (IValueList)(object)vlEmployees2;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Lines.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Expected O, but got Unknown
		base.DisplayData();
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["LineCode"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["LineNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["LineNameEn"].ToString();
			((TextEditorControlBase)cboBranchName).Value = drMaster["LineBranchID"];
			dtDetails = LinesSalesMan1.SelectByLineID(drMaster["LineID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtDetailsSalesMen2 = LinesSalesMan2.SelectByLineID(drMaster["LineID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtClientsDetails = SubAccountsClientSupplier.SelectByLineID(drMaster["LineID"].ToString(), GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeClients.AfterCheck -= new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
			TreeClients.BeforeCheck -= new BeforeCheckEventHandler(TreeClients_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeClients);
			SetCheckedSubAccounts(dtClientsDetails);
			TreeClients.AfterCheck += new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
			TreeClients.BeforeCheck += new BeforeCheckEventHandler(TreeClients_BeforeCheck);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataSalesMen2).DataSource = dtDetailsSalesMen2;
			InitGrid();
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((EditorButtonControlBase)txtCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboBranchName).ReadOnly = NavMode;
		((Control)(object)btnClientsSearch).Visible = !NavMode;
		((Control)(object)chkAll).Enabled = !NavMode;
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataSalesMen2).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Lines.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		cboBranchName.SelectedIndex = -1;
		if (((UltraGridBase)ULGDataSalesMen2).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSalesMen2).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGDataSalesMen2).DataSource).Rows.Clear();
		}
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeClients);
		((Control)(object)txtClients).Text = "";
		((UltraToggleEditorBase)chkAll).Checked = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال رقم  الأذن" : "Please Enter The Line Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال اسم الخط بالعربية", "Please Enter Line Arabic Name");
			return false;
		}
		if (Main.CheckForValue("G_Lines", "LineCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["LineCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = Lines.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الخط", "Please insert details for this Line");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["EmployeeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الموظف   ", "Please Enter Employee Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["EmployeeID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ البداية  ", "Please Enter From Date ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FromDate"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الانتهاء  ", "Please Enter To Date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ToDate"];
				return false;
			}
			if (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) > DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("رجاءا اختر فتره صحيحه", "From Date Is After The To Date");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
				return false;
			}
			for (int j = 0; j < i; j++)
			{
				if (j != i && ((UltraGridBase)ULGData).Rows[j].Cells["EmployeeID"].Value.Equals(((UltraGridBase)ULGData).Rows[i].Cells["EmployeeID"].Value) && ((DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ToDate"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذا التاريخ واقع فى فترة من قبل", "this Date in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[j]).Selected = true;
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSalesMen2).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["EmployeeID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الموظف   ", "Please Enter Employee Name");
				ULGDataSalesMen2.ActiveCell = ((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["EmployeeID"];
				ULGDataSalesMen2.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["FromDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ البداية  ", "Please Enter From Date ");
				ULGDataSalesMen2.ActiveCell = ((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["FromDate"];
				return false;
			}
			if (((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["ToDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال تاريخ الانتهاء  ", "Please Enter To Date");
				ULGDataSalesMen2.ActiveCell = ((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["ToDate"];
				return false;
			}
			if (DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["FromDate"].Value.ToString()) > DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["ToDate"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show("رجاءا اختر فتره صحيحه", "From Date Is After The To Date");
				((GridItemBase)((UltraGridBase)ULGDataSalesMen2).Rows[k]).Selected = true;
				return false;
			}
			for (int l = 0; l < k; l++)
			{
				if (l != k && ((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["EmployeeID"].Value.Equals(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["EmployeeID"].Value) && ((DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["FromDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["ToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["ToDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["ToDate"].Value.ToString())) || (DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["FromDate"].Value.ToString()) <= DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["FromDate"].Value.ToString()) && DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[k].Cells["ToDate"].Value.ToString()) >= DateTime.Parse(((UltraGridBase)ULGDataSalesMen2).Rows[l].Cells["ToDate"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذا التاريخ واقع فى فترة من قبل", "this Date in Another Period");
					((GridItemBase)((UltraGridBase)ULGDataSalesMen2).Rows[l]).Selected = true;
					return false;
				}
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Lines.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboBranchName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchName).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["LineSalesMan1ID"].Value = "-1";
				((UltraGridBase)ULGData).Rows[i].Cells["LineID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				LinesSalesMan1.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSalesMen2).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataSalesMen2).Rows[j].Cells["LineSalesMan2ID"].Value = "-1";
				((UltraGridBase)ULGDataSalesMen2).Rows[j].Cells["LineID"].Value = num;
				((UltraGridBase)ULGDataSalesMen2).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSalesMen2).Rows).Count > 0)
			{
				LinesSalesMan2.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataSalesMen2).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				SubAccountsClientSupplier.UpdateLineIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = Lines.Insert_Update(drMaster["LineID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, (cboBranchName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboBranchName).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["LineSalesMan1ID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["LineID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("G_LinesSalesMan1", "LineID", drMaster["LineID"].ToString(), "LineSalesMan1ID", text, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				LinesSalesMan1.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			string text2 = ",";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSalesMen2).Rows).Count; j++)
			{
				text2 = text2 + ((UltraGridBase)ULGDataSalesMen2).Rows[j].Cells["LineSalesMan2ID"].Value.ToString() + ",";
				((UltraGridBase)ULGDataSalesMen2).Rows[j].Cells["LineID"].Value = num;
				((UltraGridBase)ULGDataSalesMen2).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.SyncDeleteForUpdate("G_LinesSalesMan2", "LineID", drMaster["LineID"].ToString(), "LineSalesMan2ID", text2, IsFromServer: true);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataSalesMen2).Rows).Count > 0)
			{
				LinesSalesMan2.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataSalesMen2).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			string nodeCheckedIDs = GetNodeCheckedIDs();
			if (nodeCheckedIDs != ",")
			{
				SubAccountsClientSupplier.UpdateLineIDBySubAccountIDs(nodeCheckedIDs, num.ToString(), IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			LinesSalesMan2.DeleteByLineID(drMaster["LineID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			LinesSalesMan1.DeleteByLineID(drMaster["LineID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Lines.Delete(drMaster["LineID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.GLinesReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["LineID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtEmployees = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlEmployees1.ValueListItems.Clear();
		vlEmployees2.ValueListItems.Clear();
		for (int i = 0; i < dtEmployees.Rows.Count; i++)
		{
			vlEmployees1.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString());
			vlEmployees2.ValueListItems.Add(dtEmployees.Rows[i]["SubAccountID"], dtEmployees.Rows[i]["SubAccountName"].ToString());
		}
		dtClients = SubAccounts.FillReportTreeBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtClients != null)
		{
			TreeFunctions.FillTree(TreeClients, dtClients, "ParentID", "SubAccountID", "SubAccountName", "SubAccountNumber", "IsMain");
		}
		dtBranches = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboBranchName, dtBranches, "BranchID", "BranchName");
	}

	private void TreeClients_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		TreeClients.AfterCheck -= new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
		TreeClients.BeforeCheck -= new BeforeCheckEventHandler(TreeClients_BeforeCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeClients, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		TreeClients.AfterCheck += new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
		TreeClients.BeforeCheck += new BeforeCheckEventHandler(TreeClients_BeforeCheck);
	}

	private void TreeClients_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		if (!Adding && !Updating)
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		TreeClients.AfterCheck -= new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
		TreeClients.BeforeCheck -= new BeforeCheckEventHandler(TreeClients_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeClients);
		TreeClients.AfterCheck += new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
		TreeClients.BeforeCheck += new BeforeCheckEventHandler(TreeClients_BeforeCheck);
	}

	public void SetCheckBoxAllState(UltraTree tree, UltraCheckEditor CheckBox)
	{
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			if (tree.Nodes[i].CheckedState == CheckState.Unchecked || tree.Nodes[i].CheckedState == CheckState.Indeterminate)
			{
				((UltraToggleEditorBase)CheckBox).Checked = false;
			}
			else
			{
				num++;
			}
		}
		if (num == ((DisposableObjectCollectionBase)tree.Nodes).Count)
		{
			((UltraToggleEditorBase)CheckBox).Checked = true;
		}
	}

	public void SetCheckedSubAccounts(DataTable dtClients)
	{
		for (int i = 0; i < dtClients.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeClients.GetNodeByKey(dtClients.Rows[i]["SubAccountID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			((UltraControlBase)TreeClients).Update();
			if (nodeByKey.Parent != null)
			{
				TreeFunctions.SetParentCheckedState(nodeByKey.Parent);
			}
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeClients, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	public string GetNodeCheckedIDs()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)TreeClients.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)TreeClients.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)TreeClients.Nodes[i]).Tag) || TreeClients.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(TreeClients.Nodes[i])) : (text + ((KeyedSubObjectBase)TreeClients.Nodes[i]).Key + ","));
		}
		return text;
	}

	public static string GetNodeCheckedChildsIDs(UltraTreeNode Node)
	{
		string text = "";
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)Node.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)Node.Nodes[i]).Tag) || Node.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(Node.Nodes[i])) : (text + ((KeyedSubObjectBase)Node.Nodes[i]).Key + ","));
		}
		return text;
	}

	private void txtClients_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtClients);
		dataView.RowFilter = "SubAccountName Like '%" + ((Control)(object)txtClients).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeClients.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeClients.ActiveNode = TreeClients.GetNodeByKey(dataView.ToTable().Rows[0]["SubAccountID"].ToString());
		}
	}

	private void btnClientsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ClientsReport("-1", IsFromServer: true);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeClients.GetNodeByKey(dtSearchResult.Rows[i]["SubAccountID"].ToString()).CheckedState = CheckState.Checked;
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
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_0e0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e18: Expected O, but got Unknown
		//IL_0e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e30: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmLines));
		UltraTab val = new UltraTab();
		UltraTab val2 = new UltraTab();
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
		Appearance val18 = new Appearance();
		Override val19 = new Override();
		Appearance val20 = new Appearance();
		this.ultraTabPageControl3 = new UltraTabPageControl();
		this.ULGDataSalesMen2 = new UltraGrid();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnClientsSearch = new UltraButton();
		this.txtClients = new UltraTextEditor();
		this.TreeClients = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.cboBranchName = new UltraComboEditor();
		this.lblBranch = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataSalesMen2).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtClients).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeClients).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl3);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		((KeyedSubObjectBase)val).Key = "SalesMen2";
		val.TabPage = this.ultraTabPageControl3;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((KeyedSubObjectBase)val2).Key = "Clients";
		val2.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val2, "ultraTab2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val, val2 });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl3, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val3, "appearance9");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val4, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val5;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance12");
		((AppearanceBase)val6).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val7, "appearance13");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val8).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val8, "appearance14");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val9, "appearance15");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val10).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance16");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
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
		((AppearanceBase)val11).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val11).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val11).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val11).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val11).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val11, "appearance17");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val11;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl3, "ultraTabPageControl3");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataSalesMen2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).Name = "ultraTabPageControl3";
		resources.ApplyResources(this.ULGDataSalesMen2, "ULGDataSalesMen2");
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val12, "appearance4");
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance5");
		((AppearanceBase)val13).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val14, "appearance6");
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val15, "appearance7");
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val16, "appearance8");
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataSalesMen2).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataSalesMen2).Name = "ULGDataSalesMen2";
		((UltraControlBase)this.ULGDataSalesMen2).UseFlatMode = (DefaultableBoolean)1;
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnClientsSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtClients);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.TreeClients);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnClientsSearch, "btnClientsSearch");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance1");
		((ControlBase)this.btnClientsSearch).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnClientsSearch).Name = "btnClientsSearch";
		((System.Windows.Forms.Control)(object)this.btnClientsSearch).Click += new System.EventHandler(btnClientsSearch_Click);
		resources.ApplyResources(this.txtClients, "txtClients");
		((System.Windows.Forms.Control)(object)this.txtClients).Name = "txtClients";
		((TextEditorControlBase)this.txtClients).ValueChanged += new System.EventHandler(txtClients_ValueChanged);
		resources.ApplyResources(this.TreeClients, "TreeClients");
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance2");
		this.TreeClients.Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.TreeClients).Name = "TreeClients";
		val19.NodeStyle = (NodeStyle)1;
		this.TreeClients.Override = val19;
		((UltraControlBase)this.TreeClients).UseAppStyling = false;
		this.TreeClients.AfterCheck += new AfterNodeChangedEventHandler(TreeClients_AfterCheck);
		this.TreeClients.BeforeCheck += new BeforeCheckEventHandler(TreeClients_BeforeCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance3");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		this.lblEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		this.lblArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.cboBranchName, "cboBranchName");
		this.cboBranchName.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboBranchName).Name = "cboBranchName";
		resources.ApplyResources(this.lblBranch, "lblBranch");
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		((ControlBase)this.lblBranch).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBranchName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmLines";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBranchName, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl3).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataSalesMen2).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtClients).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeClients).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranchName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
