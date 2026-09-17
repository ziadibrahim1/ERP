using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.LensesProductions;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.LensesProductions.MasterData;

public class frmBlanksTypes : frmHeaderManyDetails
{
	private DataTable dtAccounts;

	private DataTable dtItems;

	private DataTable dtPricesTypes;

	private DataTable dtBranches;

	private DataTable dtReports;

	private DataTable dtBlankTypesPrices;

	private ValueList vlItems = new ValueList();

	private ValueList vlBarCode = new ValueList();

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlBranches = new ValueList();

	private IContainer components = null;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGPrices;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraButton btnSelectItems;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraPanel ultraPanel1;

	private RadioButton rbPriceForEachBranches;

	private RadioButton rbPriceForAllBranches;

	public UltraButton btnSalesAccount;

	private UltraComboEditor cboSalesAccount;

	private UltraLabel lblSalesAccount;

	private UltraCheckEditor chkCanModSalesPrice;

	private UltraCheckEditor chkNoBlank;

	public frmBlanksTypes()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		InitializeComponent();
		TableName = "Lns_BlanksTypes";
		IDCol = "BlankTypeID";
		NoCol = "BlankTypeNo";
		DateCol = "GetDate()";
	}

	public frmBlanksTypes(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalesAccount, dtAccounts, "AccountID", "Name");
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		vlBarCode.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			vlBarCode.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["ItemBarCode"].ToString());
		}
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int j = 0; j < dtPricesTypes.Rows.Count; j++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[j]["PriceTypeID"], dtPricesTypes.Rows[j]["PriceName"].ToString());
		}
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int k = 0; k < dtBranches.Rows.Count; k++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[k]["BranchID"], dtBranches.Rows[k][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtDetails = BlanksTypesItems.SelectByBlankTypeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtBlankTypesPrices = BlanksTypesPrices.SelectByBlankTypeID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGPrices).DataSource = dtBlankTypesPrices;
		InitGrid();
		InitPrices();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankTypeItemID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemOrder"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemOrder"].Header).Caption = (GlobalVariables.IsArabic ? "أولوية الصنف" : "Item Order");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemOrder"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.8) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Header).Caption = (GlobalVariables.IsArabic ? "الباركود" : "Barcode");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].ValueList = (IValueList)(object)vlBarCode;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemBarCode"].Hidden = false;
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if ((((KeyedSubObjectBase)e.Cell.Column).Key == "ItemBarCode" || ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID") && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			UltraGridCell obj = ((UltraGridBase)ULGData).ActiveRow.Cells["ItemBarCode"];
			object value = (((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = e.Cell.Value);
			obj.Value = value;
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private void InitPrices()
	{
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BlankTypePriceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		if (rbPriceForAllBranches.Checked)
		{
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5) - GlobalVariables.ScrollWidth;
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.5);
			((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
			return;
		}
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].ValueList = (IValueList)(object)vlBranches;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = BlanksTypes.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		base.DisplayData();
		if (drMaster != null)
		{
			((Control)(object)txtCode).Text = drMaster["BlankTypeNo"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["BlankTypeNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["BlankTypeNameEn"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			rbPriceForAllBranches.Checked = Convert.ToBoolean(drMaster["PriceForAllBranch"]);
			rbPriceForEachBranches.Checked = !Convert.ToBoolean(drMaster["PriceForAllBranch"]);
			((UltraToggleEditorBase)chkCanModSalesPrice).Checked = Convert.ToBoolean(drMaster["CanModifyPrice"]);
			((UltraToggleEditorBase)chkNoBlank).Checked = Convert.ToBoolean(drMaster["IsNoBlank"]);
			((TextEditorControlBase)cboSalesAccount).Value = drMaster["SalesAccount"];
			dtDetails = BlanksTypesItems.SelectByBlankTypeID(drMaster["BlankTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtBlankTypesPrices = BlanksTypesPrices.SelectByBlankTypeID(drMaster["BlankTypeID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGPrices).DataSource = dtBlankTypesPrices;
			InitGrid();
			InitPrices();
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
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		rbPriceForAllBranches.Enabled = !NavMode;
		rbPriceForEachBranches.Enabled = !NavMode;
		((Control)(object)chkCanModSalesPrice).Enabled = !NavMode;
		((Control)(object)chkNoBlank).Enabled = !NavMode;
		((EditorButtonControlBase)cboSalesAccount).ReadOnly = NavMode;
		((Control)(object)btnSalesAccount).Visible = !NavMode;
		((Control)(object)btnSelectItems).Visible = !NavMode;
		if (Adding)
		{
			DataView dataView = new DataView(dtItems);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int j = 0; j < dtItems.Rows.Count; j++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? BlanksTypes.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		rbPriceForEachBranches.Checked = false;
		rbPriceForAllBranches.Checked = true;
		((UltraToggleEditorBase)chkCanModSalesPrice).Checked = false;
		((UltraToggleEditorBase)chkNoBlank).Checked = false;
		cboSalesAccount.SelectedIndex = -1;
		FillGridPrices();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكود" : "Please Enter The Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الاسم بالعربية" : "Please Enter The  Arabic Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("Lns_BlanksTypes", "BlankTypeNameAr", ((Control)(object)txtNameAr).Text, Updating ? drMaster["BlankTypeNameAr"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" الاسم بالعربية متواجد من قبل ", "Arabic Name Already Exist");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("Lns_BlanksTypes", "BlankTypeNameEn", ((Control)(object)txtNameEn).Text, Updating ? drMaster["BlankTypeNameEn"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" الاسم بالانجليزية متواجد من قبل ", "English Name Already Exist");
			((TextEditorControlBase)txtNameEn).Focus();
			return false;
		}
		if (Main.CheckForValue("Lns_BlanksTypes", "BlankTypeNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["BlankTypeNo"].ToString(), IsFromServer: true) > 0)
		{
			string code = BlanksTypes.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذه الخامة متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Blank Type No  Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (!((UltraToggleEditorBase)chkNoBlank).Checked && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا الخامة", "Please insert details for this Blank Type");
			((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ItemOrder"].Value == DBNull.Value)
			{
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				GlobalVariables.InformationMB.Show("برجاء ادخال الاولوية  ", "Please Enter Item Order ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemOrder"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(" لا يمكن تكرار الصنف", "Cannot Duplicate The Same Item ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemID"];
					return false;
				}
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["ItemOrder"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["ItemOrder"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show(" لا يمكن تكرار أولوية الصنف", "Cannot Duplicate The Item Order ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ItemOrder"];
					return false;
				}
			}
		}
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; k++)
		{
			if (((UltraGridBase)ULGPrices).Rows[k].Cells["Price"].Value == DBNull.Value)
			{
				((UltraTabControlBase)UTCDetails).Tabs["Prices"].Selected = true;
				GlobalVariables.InformationMB.Show("برجاء ادخال السعر  ", "Please Enter Price ");
				ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[k].Cells["Price"];
				ULGPrices.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = BlanksTypes.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text, rbPriceForAllBranches.Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModSalesPrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkNoBlank).Checked ? "1" : "0", (cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeItemID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			dtBlankTypesPrices.AcceptChanges();
			for (int j = 0; j < dtBlankTypesPrices.Rows.Count; j++)
			{
				BlanksTypesPrices.Insert_Update("-1", num.ToString(), dtBlankTypesPrices.Rows[j]["PriceTypeID"].ToString(), dtBlankTypesPrices.Rows[j]["Price"].ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : dtBlankTypesPrices.Rows[j]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				BlanksTypesItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			int num = BlanksTypes.Insert_Update(drMaster["BlankTypeID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, rbPriceForAllBranches.Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModSalesPrice).Checked ? "1" : "0", ((UltraToggleEditorBase)chkNoBlank).Checked ? "1" : "0", (cboSalesAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSalesAccount).Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			BlanksTypesPrices.DeleteByBlankTypeID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			BlanksTypesItems.DeleteByBlankTypeID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeItemID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BlankTypeID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			dtBlankTypesPrices.AcceptChanges();
			((UltraGridBase)ULGPrices).UpdateData();
			for (int j = 0; j < dtBlankTypesPrices.Rows.Count; j++)
			{
				BlanksTypesPrices.Insert_Update("-1", num.ToString(), dtBlankTypesPrices.Rows[j]["PriceTypeID"].ToString(), dtBlankTypesPrices.Rows[j]["Price"].ToString(), "0", rbPriceForAllBranches.Checked ? "Null" : dtBlankTypesPrices.Rows[j]["BranchID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				BlanksTypesItems.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
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
			BlanksTypesPrices.DeleteByBlankTypeID(drMaster["BlankTypeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			BlanksTypesItems.DeleteByBlankTypeID(drMaster["BlankTypeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			BlanksTypes.Delete(drMaster["BlankTypeID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		dtSearchResult = SearchFunctions.BlanksTypesSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["BlankTypeID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSalesAccount, dtAccounts, "AccountID", "Name");
		if (Adding)
		{
			DataView dataView = new DataView(dtItems);
			dataView.RowFilter = " IsActive =1 ";
			DataTable dataTable = dataView.ToTable();
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				vlItems.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dataTable.Rows[i]["ItemID"], dataTable.Rows[i]["ItemBarCode"].ToString());
			}
		}
		else
		{
			vlItems.ValueListItems.Clear();
			vlBarCode.ValueListItems.Clear();
			for (int j = 0; j < dtItems.Rows.Count; j++)
			{
				vlItems.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["Name"].ToString());
				vlBarCode.ValueListItems.Add(dtItems.Rows[j]["ItemID"], dtItems.Rows[j]["ItemBarCode"].ToString());
			}
		}
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[k]["PriceTypeID"], dtPricesTypes.Rows[k]["PriceName"].ToString());
		}
		dtBranches = Branches.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlBranches.ValueListItems.Clear();
		for (int l = 0; l < dtBranches.Rows.Count; l++)
		{
			vlBranches.ValueListItems.Add(dtBranches.Rows[l]["BranchID"], dtBranches.Rows[l][GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn"].ToString());
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void ULGPrices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGPrices.ActiveCell != null && ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "Price")
		{
			GlobalFunctions.CheckForNumbers(ULGPrices.ActiveCell, e);
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void rbPriceForAllBranches_CheckedChanged(object sender, EventArgs e)
	{
		FillGridPrices();
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemOrder")
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void btnSelectItems_Click(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((UltraTabControlBase)UTCDetails).ActiveTab == null)
		{
			return;
		}
		DataTable dataTable = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "-1", IsFromServer: false);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dtDetails.Select("ItemID = " + dataTable.Rows[i]["ItemID"].ToString()).Length == 0)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["BlankTypeID"] = (Adding ? ((object)(-1)) : drMaster["BlankTypeID"]);
				dataRow["BlankTypeItemID"] = -1;
				dataRow["ItemID"] = dataTable.Rows[i]["ItemID"];
				dataRow["ItemBarCode"] = dataTable.Rows[i]["ItemID"];
				dataRow["Deleted"] = false;
				dtDetails.Rows.Add(dataRow);
			}
		}
		((UltraGridBase)ULGData).UpdateData();
	}

	private void FillGridPrices()
	{
		dtBlankTypesPrices.Rows.Clear();
		if (rbPriceForAllBranches.Checked)
		{
			for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
			{
				DataRow dataRow = dtBlankTypesPrices.NewRow();
				dataRow["BlankTypePriceID"] = -1;
				dataRow["BlankTypeID"] = -1;
				dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
				dataRow["Price"] = 0;
				dataRow["Deleted"] = false;
				dataRow["BranchID"] = DBNull.Value;
				dtBlankTypesPrices.Rows.Add(dataRow);
			}
		}
		else
		{
			for (int j = 0; j < dtBranches.Rows.Count; j++)
			{
				for (int k = 0; k < dtPricesTypes.Rows.Count; k++)
				{
					DataRow dataRow2 = dtBlankTypesPrices.NewRow();
					dataRow2["BlankTypePriceID"] = -1;
					dataRow2["BlankTypeID"] = -1;
					dataRow2["PriceTypeID"] = dtPricesTypes.Rows[k]["PriceTypeID"];
					dataRow2["Price"] = 0;
					dataRow2["Deleted"] = false;
					dataRow2["BranchID"] = dtBranches.Rows[j]["BranchID"];
					dtBlankTypesPrices.Rows.Add(dataRow2);
				}
			}
		}
		((UltraGridBase)ULGPrices).DataSource = dtBlankTypesPrices;
		InitPrices();
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ItemID")
		{
			int num = (Adding ? SearchFunctions.Items("-1", "-1", "0", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: true) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: true));
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value = num;
			}
		}
		e.Handled = true;
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (RowID != "")
		{
			if (dtReports.Rows.Count > 0)
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
				val = dtReports.Rows[0]["isoCode"].ToString();
			}
			else
			{
				GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Lns_BlanksTypes_A.rpt" : "Rep_Lns_BlanksTypes_E.rpt"));
			}
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BlankTypeIDs", "," + RowID + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
		}
	}

	private void btnSalesAccount_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSalesAccount).Value = num;
			}
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
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.LensesProductions.MasterData.frmBlanksTypes));
		UltraTab val = new UltraTab();
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
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGPrices = new UltraGrid();
		this.ultraPanel1 = new UltraPanel();
		this.rbPriceForEachBranches = new System.Windows.Forms.RadioButton();
		this.rbPriceForAllBranches = new System.Windows.Forms.RadioButton();
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.btnSelectItems = new UltraButton();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.btnSalesAccount = new UltraButton();
		this.cboSalesAccount = new UltraComboEditor();
		this.lblSalesAccount = new UltraLabel();
		this.chkCanModSalesPrice = new UltraCheckEditor();
		this.chkNoBlank = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModSalesPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkNoBlank).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance9");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((UltraGridBase)this.ULGPrices).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGPrices).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGPrices).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGPrices).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		((UltraControlBase)this.ULGPrices).UseFlatMode = (DefaultableBoolean)1;
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGPrices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGPrices_KeyPress);
		resources.ApplyResources(this.ultraPanel1, "ultraPanel1");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val15, "appearance14");
		this.ultraPanel1.Appearance = (AppearanceBase)(object)val15;
		resources.ApplyResources(this.ultraPanel1.ClientArea, "ultraPanel1.ClientArea");
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForEachBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).Controls.Add(this.rbPriceForAllBranches);
		((System.Windows.Forms.Control)(object)this.ultraPanel1).Name = "ultraPanel1";
		resources.ApplyResources(this.rbPriceForEachBranches, "rbPriceForEachBranches");
		this.rbPriceForEachBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForEachBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForEachBranches.Name = "rbPriceForEachBranches";
		this.rbPriceForEachBranches.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbPriceForAllBranches, "rbPriceForAllBranches");
		this.rbPriceForAllBranches.BackColor = System.Drawing.Color.Transparent;
		this.rbPriceForAllBranches.Checked = true;
		this.rbPriceForAllBranches.ForeColor = System.Drawing.Color.Navy;
		this.rbPriceForAllBranches.Name = "rbPriceForAllBranches";
		this.rbPriceForAllBranches.TabStop = true;
		this.rbPriceForAllBranches.UseVisualStyleBackColor = false;
		this.rbPriceForAllBranches.CheckedChanged += new System.EventHandler(rbPriceForAllBranches_CheckedChanged);
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		resources.ApplyResources(val16, "appearance15");
		((TextEditorControlBase)this.txtNameEn).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.btnSelectItems, "btnSelectItems");
		((System.Windows.Forms.Control)(object)this.btnSelectItems).Name = "btnSelectItems";
		((System.Windows.Forms.Control)(object)this.btnSelectItems).Click += new System.EventHandler(btnSelectItems_Click);
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.btnSalesAccount, "btnSalesAccount");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val17, "appearance16");
		((ControlBase)this.btnSalesAccount).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnSalesAccount).Name = "btnSalesAccount";
		((System.Windows.Forms.Control)(object)this.btnSalesAccount).Click += new System.EventHandler(btnSalesAccount_Click);
		resources.ApplyResources(this.cboSalesAccount, "cboSalesAccount");
		this.cboSalesAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSalesAccount).Name = "cboSalesAccount";
		resources.ApplyResources(this.lblSalesAccount, "lblSalesAccount");
		this.lblSalesAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesAccount).Name = "lblSalesAccount";
		((ControlBase)this.lblSalesAccount).WrapText = false;
		resources.ApplyResources(this.chkCanModSalesPrice, "chkCanModSalesPrice");
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance17");
		((UltraToggleEditorBase)this.chkCanModSalesPrice).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.chkCanModSalesPrice).Name = "chkCanModSalesPrice";
		resources.ApplyResources(this.chkNoBlank, "chkNoBlank");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance18");
		((UltraToggleEditorBase)this.chkNoBlank).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.chkNoBlank).Name = "chkNoBlank";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkNoBlank);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModSalesPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraPanel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Name = "frmBlanksTypes";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraPanel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModSalesPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkNoBlank, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSalesAccount, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraPanel1.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.ultraPanel1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModSalesPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkNoBlank).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
