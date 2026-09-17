using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.CustomsClearence;
using BusinessLayer.General;
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
using Infragistics.Win.UltraWinTabs;
using Infragistics.Win.UltraWinTree;

namespace ERP.CustomsClearence.MasterData;

public class frmServicesTree : frmTree2
{
	private DataTable dtTaxes;

	private DataTable dtAccounts;

	private DataTable dtSubAccounts;

	private DataTable dtPricesTypes;

	private DataTable dtServicePrices;

	private ValueList vlPricesTypes = new ValueList();

	private ValueList vlServices = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtNotes;

	private UltraLabel lblReceivingBank;

	public UltraTabControl tabItemType;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tabItem;

	private UltraTextEditor txtBarCode;

	private UltraLabel lblBarCode;

	private OpenFileDialog ofdItemPic;

	private UltraTabPageControl ultraTabPageControl1;

	public UltraButton btnServiceAccountSearch;

	private UltraComboEditor cboServiceAccount;

	private UltraLabel lblServiceAccount;

	public UltraGrid ULGPrices;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem1;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem2;

	private ToolStripMenuItem modifyGroupItemsToolStripMenuItem3;

	private ContextMenuStrip contextMenuStrip1;

	private UltraCheckEditor chkCanModPrice;

	private ToolStripMenuItem deleteToolStripMenuItem;

	private ToolStripMenuItem changeParentToolStripMenuItem;

	private UltraCheckEditor chkIsActive;

	private UltraLabel ultraLabel1;

	private UltraComboEditor cboSubAccount;

	public UltraButton btnSubAccountSearch;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	public frmServicesTree()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "ServiceID";
		NoCol = "ServiceNumber";
		NameCol = "ServiceNameAr";
		NameEnCol = "ServiceNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		ItemLevelCol = "LevelID";
		TableName = "CST_Services";
		LevelsTable = "CST_ServicesLevels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
		AllowAddRoot = true;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		dtSubAccounts = SubAccounts.SelectByAccountID("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboSubAccount, dtSubAccounts, "SubAccountID", "Name");
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[i]["PriceTypeID"], dtPricesTypes.Rows[i]["PriceName"].ToString());
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtServicePrices = ServicesPrices.SelectByServiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGridPrices();
	}

	public override void ClearControls()
	{
		if (Adding)
		{
			DisplayData();
		}
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = Services.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((TextEditorControlBase)txtBarCode).Clear();
			FillGridPrices();
			((Control)(object)txtName).Select();
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)cboServiceAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBarCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTax).ReadOnly = NavMode;
		((Control)(object)chkIsActive).Enabled = !NavMode;
		((Control)(object)chkCanModPrice).Enabled = !NavMode;
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (SelectedNode != null)
		{
			DataRow dataRow = Services.Select(((KeyedSubObjectBase)SelectedNode).Key, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0];
			((TextEditorControlBase)cboServiceAccount).Value = dataRow["ServiceAccountID"];
			((TextEditorControlBase)cboSubAccount).Value = dataRow["ServiceSubAccountID"];
			((UltraToggleEditorBase)chkIsActive).Checked = Convert.ToBoolean(dataRow["IsActive"]);
			((UltraToggleEditorBase)chkCanModPrice).Checked = Convert.ToBoolean(dataRow["CanModifyPrice"]);
			((TextEditorControlBase)cboTax).Value = dataRow["TaxID"];
			deleteToolStripMenuItem.Enabled = Convert.ToBoolean(dataRow["IsMain"]);
			((Control)(object)txtBarCode).Text = dataRow["ServiceBarCode"].ToString();
			((Control)(object)txtNotes).Text = dataRow["Notes"].ToString();
			dtServicePrices = ServicesPrices.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridPrices();
		}
	}

	public override bool ValidateData()
	{
		if (cboServiceAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم الحساب", "Please Select Account Name");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboServiceAccount).Focus();
			cboServiceAccount.DropDown();
			return false;
		}
		if (cboSubAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(" برجاء إختيار إسم الحساب التحليلى", "Please Select SubAccount Name");
			((UltraTabControlBase)tabItemType).Tabs["Item"].Selected = true;
			((TextEditorControlBase)cboSubAccount).Focus();
			cboSubAccount.DropDown();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPrices).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGPrices).Rows[i].Cells["Price"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGPrices).Rows[i].Cells["Price"].Value.ToString()) == 0m)
			{
				((UltraTabControlBase)tabItemType).Tabs["Prices"].Selected = true;
				GlobalVariables.InformationMB.Show("برجاء ادخال السعر  ", "Please Enter Price ");
				ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[i].Cells["Price"];
				ULGPrices.PerformAction((UltraGridAction)24);
				return false;
			}
			if (((UltraGridBase)ULGPrices).Rows[i].Cells["AdditionalCompassPrice"].Value == DBNull.Value)
			{
				((UltraTabControlBase)tabItemType).Tabs["AdditionalCompassPrice"].Selected = true;
				GlobalVariables.InformationMB.Show("برجاء ادخال سعر البوصلة  ", "Please Enter Additional Compass Price ");
				ULGPrices.ActiveCell = ((UltraGridBase)ULGPrices).Rows[i].Cells["AdditionalCompassPrice"];
				ULGPrices.PerformAction((UltraGridAction)24);
				return false;
			}
		}
		return base.ValidateData();
	}

	public override int TreeAddData()
	{
		int result = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			result = Services.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? ((Control)(object)txtName).Text.Trim() : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), (((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text, ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0", (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			dtServicePrices.AcceptChanges();
			for (int i = 0; i < dtServicePrices.Rows.Count; i++)
			{
				ServicesPrices.Insert_Update("-1", result.ToString(), dtServicePrices.Rows[i]["PriceTypeID"].ToString(), dtServicePrices.Rows[i]["Price"].ToString(), dtServicePrices.Rows[i]["AdditionalCompassPrice"].ToString(), "0", "Null", GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return result;
	}

	public override void TreeUpdateData()
	{
		int num = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = Services.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), (((Control)(object)txtBarCode).Text.Trim() == "") ? GetCode() : ((Control)(object)txtBarCode).Text, ((UltraToggleEditorBase)chkIsActive).Checked ? "1" : "0", ((UltraToggleEditorBase)chkCanModPrice).Checked ? "1" : "0", (cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString(), (cboServiceAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboServiceAccount).Value.ToString(), (cboSubAccount.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboSubAccount).Value.ToString(), (((Control)(object)txtNotes).Text.Trim() == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			ServicesPrices.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			dtServicePrices.AcceptChanges();
			for (int i = 0; i < dtServicePrices.Rows.Count; i++)
			{
				ServicesPrices.Insert_Update("-1", ((KeyedSubObjectBase)SelectedNode).Key, dtServicePrices.Rows[i]["PriceTypeID"].ToString(), dtServicePrices.Rows[i]["Price"].ToString(), dtServicePrices.Rows[i]["AdditionalCompassPrice"].ToString(), "0", "Null", GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void TreeDeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			ServicesPrices.DeleteByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Services.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override bool HasTransactionValidation()
	{
		if (OperationsServices.SelectByServiceID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0").Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show("الخدمه مستخدمه غي العمليات", "this Service is used in Operation");
			return true;
		}
		return base.HasTransactionValidation();
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.CSTServicesSearch(IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	private void FillGridPrices()
	{
		dtServicePrices.Rows.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			DataRow dataRow = dtServicePrices.NewRow();
			dataRow["ServicePriceID"] = -1;
			dataRow["ServiceID"] = -1;
			dataRow["PriceTypeID"] = dtPricesTypes.Rows[i]["PriceTypeID"];
			dataRow["Price"] = 0;
			dataRow["AdditionalCompassPrice"] = 0;
			dataRow["Deleted"] = false;
			dataRow["BranchID"] = DBNull.Value;
			dtServicePrices.Rows.Add(dataRow);
		}
		InitGridPrices();
	}

	private void InitGridPrices()
	{
		((UltraGridBase)ULGPrices).DataSource = dtServicePrices;
		GlobalFunctions.PrepareGrid(ULGPrices);
		((UltraGridBase)ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["ServicePriceID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "نوع السعر" : "Price Type");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].ValueList = (IValueList)(object)vlPricesTypes;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["AdditionalCompassPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر البوصلة" : "Additional Compass Price");
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["AdditionalCompassPrice"].Hidden = false;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["PriceTypeID"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["Price"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["AdditionalCompassPrice"].Width = (int)((double)((Control)(object)ULGPrices).Width * 0.3);
		((UltraGridBase)ULGPrices).DisplayLayout.Bands[0].Columns["BranchID"].Hidden = true;
	}

	public override void btnRefreshDataClick()
	{
		base.btnRefreshDataClick();
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboServiceAccount, dtAccounts, "AccountID", "Name");
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtPricesTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPricesTypes.ValueListItems.Clear();
		for (int i = 0; i < dtPricesTypes.Rows.Count; i++)
		{
			vlPricesTypes.ValueListItems.Add(dtPricesTypes.Rows[i]["PriceTypeID"], dtPricesTypes.Rows[i]["PriceName"].ToString());
		}
	}

	public override void btnPrintClick()
	{
	}

	private void btnServiceAccountSearch_Click(object sender, EventArgs e)
	{
		if (Adding || Updating)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboServiceAccount).Value = num;
			}
		}
	}

	private void ULGPrices_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGPrices.ActiveCell != null && (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "Price" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "AdditionalCompassPrice"))
		{
			GlobalFunctions.CheckForNumbers(ULGPrices.ActiveCell, e);
		}
	}

	private void modifyGroupItemsToolStripMenuItem3_Click(object sender, EventArgs e)
	{
	}

	private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
		}
		else if (SelectedNode != null)
		{
			DeleteGroupItems(SelectedNode);
		}
	}

	public bool DeleteGroupItems(UltraTreeNode Node)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)Node.Nodes).Count; i++)
		{
			if (DeleteGroupItems(Node.Nodes[i]))
			{
				i--;
			}
		}
		SelectedNode = Node;
		if (((DisposableObjectCollectionBase)Node.Nodes).Count > 0)
		{
			GlobalVariables.InformationMB.Show(" لايمكن حذف هذا العنصر لوجود عناصر تحته", "Cannot Delete this Node It Has Sub Nodes");
			return false;
		}
		if (HasTransactionValidation())
		{
			return false;
		}
		RowID = (((SubObjectBase)SelectedNode).Tag as DataRow)[0].ToString();
		DeleteData();
		if (Main.Success)
		{
			return true;
		}
		return false;
	}

	private void ULGPrices_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
		else if (((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "PriceTypeID" || ((KeyedSubObjectBase)ULGPrices.ActiveCell.Column).Key == "BranchID")
		{
			((GridItemBase)((UltraGridBase)ULGPrices).ActiveRow).Selected = true;
		}
	}

	private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == ' ' || e.KeyChar == '+')
		{
			e.Handled = true;
		}
	}

	private void changeParentToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (SelectedNode != null)
		{
			frmUpdateParent frmUpdateParent2 = new frmUpdateParent(((KeyedSubObjectBase)SelectedNode).Key, (SelectedNode.Parent != null) ? ((KeyedSubObjectBase)SelectedNode.Parent).Key : "");
			((Control)(object)frmUpdateParent2.lblTitle).Text = (GlobalVariables.IsArabic ? "تغيير المجموعه" : "Update Group");
			frmUpdateParent2.ShowDialog();
			btnRefreshDataClick();
		}
	}

	private void cboServiceAccount_ValueChanged(object sender, EventArgs e)
	{
		if (cboServiceAccount.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtSubAccounts);
			dataView.RowFilter = "AccountID=" + ((TextEditorControlBase)cboServiceAccount).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboSubAccount.DataSource = dataView;
		}
	}

	private void btnSubAccountSearch_Click(object sender, EventArgs e)
	{
		if (cboServiceAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboServiceAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
		}
	}

	private void cboSubAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119 && cboServiceAccount.SelectedIndex != -1)
		{
			int num = SearchFunctions.SubAccounts(((TextEditorControlBase)cboServiceAccount).Value.ToString(), IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboSubAccount).Value = num;
			}
		}
	}

	private void cboServiceAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			((TextEditorControlBase)cboServiceAccount).Value = SearchFunctions.Accounts(IsFromServer: true);
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
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Expected O, but got Unknown
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.MasterData.frmServicesTree));
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
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		UltraTab val23 = new UltraTab();
		UltraTab val24 = new UltraTab();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		this.tabItem = new UltraTabPageControl();
		this.cboServiceAccount = new UltraComboEditor();
		this.lblServiceAccount = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.cboSubAccount = new UltraComboEditor();
		this.btnServiceAccountSearch = new UltraButton();
		this.btnSubAccountSearch = new UltraButton();
		this.txtNotes = new UltraTextEditor();
		this.lblReceivingBank = new UltraLabel();
		this.chkIsActive = new UltraCheckEditor();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGPrices = new UltraGrid();
		this.chkCanModPrice = new UltraCheckEditor();
		this.tabItemType = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.txtBarCode = new UltraTextEditor();
		this.lblBarCode = new UltraLabel();
		this.ofdItemPic = new System.Windows.Forms.OpenFileDialog();
		this.modifyGroupItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.modifyGroupItemsToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.changeParentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItem).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).BeginInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).BeginInit();
		this.contextMenuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.treeChart, "treeChart");
		((System.Windows.Forms.Control)(object)base.treeChart).ContextMenuStrip = this.contextMenuStrip1;
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnAddRoot, "btnAddRoot");
		resources.ApplyResources(base.label1, "label1");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance25");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label2, "label2");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance26");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label3, "label3");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance27");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
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
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.tabItem, "tabItem");
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboServiceAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblServiceAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.cboSubAccount);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.btnSubAccountSearch);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.lblReceivingBank);
		((System.Windows.Forms.Control)(object)this.tabItem).Controls.Add((System.Windows.Forms.Control)(object)this.chkIsActive);
		((System.Windows.Forms.Control)(object)this.tabItem).Name = "tabItem";
		resources.ApplyResources(this.cboServiceAccount, "cboServiceAccount");
		this.cboServiceAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboServiceAccount).Name = "cboServiceAccount";
		((TextEditorControlBase)this.cboServiceAccount).ValueChanged += new System.EventHandler(cboServiceAccount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.cboServiceAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboServiceAccount_KeyDown);
		resources.ApplyResources(this.lblServiceAccount, "lblServiceAccount");
		this.lblServiceAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServiceAccount).Name = "lblServiceAccount";
		((ControlBase)this.lblServiceAccount).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.cboSubAccount, "cboSubAccount");
		this.cboSubAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboSubAccount).Name = "cboSubAccount";
		((System.Windows.Forms.Control)(object)this.cboSubAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboSubAccount_KeyDown);
		resources.ApplyResources(this.btnServiceAccountSearch, "btnServiceAccountSearch");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val5, "appearance28");
		((ControlBase)this.btnServiceAccountSearch).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Name = "btnServiceAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnServiceAccountSearch).Click += new System.EventHandler(btnServiceAccountSearch_Click);
		resources.ApplyResources(this.btnSubAccountSearch, "btnSubAccountSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance29");
		((ControlBase)this.btnSubAccountSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Name = "btnSubAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnSubAccountSearch).Click += new System.EventHandler(btnSubAccountSearch_Click);
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblReceivingBank, "lblReceivingBank");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance30");
		((ControlBase)this.lblReceivingBank).Appearance = (AppearanceBase)(object)val7;
		this.lblReceivingBank.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReceivingBank).Name = "lblReceivingBank";
		((ControlBase)this.lblReceivingBank).WrapText = false;
		resources.ApplyResources(this.chkIsActive, "chkIsActive");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance31");
		((UltraToggleEditorBase)this.chkIsActive).Appearance = (AppearanceBase)(object)val8;
		((UltraToggleEditorBase)this.chkIsActive).Checked = true;
		((UltraToggleEditorBase)this.chkIsActive).CheckState = System.Windows.Forms.CheckState.Checked;
		((System.Windows.Forms.Control)(object)this.chkIsActive).Name = "chkIsActive";
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGPrices);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.ULGPrices, "ULGPrices");
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val9).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val9, "appearance9");
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val10;
		((SpecialBoxBase)((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGPrices).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPrices).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val13).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val14;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val15, "appearance15");
		((AppearanceBase)val15).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val16).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val16).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val16).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val17).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGPrices).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.ULGPrices).Name = "ULGPrices";
		this.ULGPrices.AfterEnterEditMode += new System.EventHandler(ULGPrices_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGPrices).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGPrices_KeyPress);
		resources.ApplyResources(this.chkCanModPrice, "chkCanModPrice");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance32");
		((UltraToggleEditorBase)this.chkCanModPrice).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.chkCanModPrice).Name = "chkCanModPrice";
		resources.ApplyResources(this.tabItemType, "tabItemType");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val20, "appearance20");
		((AppearanceBase)val20).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tabItemType).Appearance = (AppearanceBase)(object)val20;
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.tabItem);
		((System.Windows.Forms.Control)(object)this.tabItemType).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tabItemType).Name = "tabItemType";
		((UltraTabControlBase)this.tabItemType).SharedControlsPage = this.ultraTabSharedControlsPage1;
		resources.ApplyResources(val21, "appearance21");
		((UltraTabControlBase)this.tabItemType).TabHeaderAreaAppearance = (AppearanceBase)(object)val21;
		resources.ApplyResources(val22, "appearance22");
		((UltraTabControlBase)this.tabItemType).TabListButtonAppearance = (AppearanceBase)(object)val22;
		((UltraTabControlBase)this.tabItemType).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val23).Key = "Item";
		val23.TabPage = this.tabItem;
		resources.ApplyResources(val23, "ultraTab2");
		((SubObjectBase)val23).ForceApplyResources = "";
		((KeyedSubObjectBase)val24).Key = "Prices";
		val24.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val24, "ultraTab4");
		((SubObjectBase)val24).ForceApplyResources = "";
		((UltraTabControlBase)this.tabItemType).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val23, val24 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.txtBarCode, "txtBarCode");
		((System.Windows.Forms.Control)(object)this.txtBarCode).Name = "txtBarCode";
		((System.Windows.Forms.Control)(object)this.txtBarCode).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtBarCode_KeyPress);
		resources.ApplyResources(this.lblBarCode, "lblBarCode");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance33");
		((ControlBase)this.lblBarCode).Appearance = (AppearanceBase)(object)val25;
		this.lblBarCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBarCode).Name = "lblBarCode";
		((ControlBase)this.lblBarCode).WrapText = false;
		this.ofdItemPic.FileName = "openFileDialog1";
		resources.ApplyResources(this.ofdItemPic, "ofdItemPic");
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem, "modifyGroupItemsToolStripMenuItem");
		this.modifyGroupItemsToolStripMenuItem.Name = "modifyGroupItemsToolStripMenuItem";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem1, "modifyGroupItemsToolStripMenuItem1");
		this.modifyGroupItemsToolStripMenuItem1.Name = "modifyGroupItemsToolStripMenuItem1";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem2, "modifyGroupItemsToolStripMenuItem2");
		this.modifyGroupItemsToolStripMenuItem2.Name = "modifyGroupItemsToolStripMenuItem2";
		resources.ApplyResources(this.modifyGroupItemsToolStripMenuItem3, "modifyGroupItemsToolStripMenuItem3");
		this.modifyGroupItemsToolStripMenuItem3.Name = "modifyGroupItemsToolStripMenuItem3";
		this.modifyGroupItemsToolStripMenuItem3.Click += new System.EventHandler(modifyGroupItemsToolStripMenuItem3_Click);
		resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
		this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.deleteToolStripMenuItem, this.changeParentToolStripMenuItem });
		this.contextMenuStrip1.Name = "contextMenuStrip1";
		this.contextMenuStrip1.ShowImageMargin = false;
		resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
		this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
		this.deleteToolStripMenuItem.Click += new System.EventHandler(deleteToolStripMenuItem_Click);
		resources.ApplyResources(this.changeParentToolStripMenuItem, "changeParentToolStripMenuItem");
		this.changeParentToolStripMenuItem.Name = "changeParentToolStripMenuItem";
		this.changeParentToolStripMenuItem.Click += new System.EventHandler(changeParentToolStripMenuItem_Click);
		resources.ApplyResources(this.lblTax, "lblTax");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance34");
		((ControlBase)this.lblTax).Appearance = (AppearanceBase)(object)val26;
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCanModPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tabItemType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBarCode);
		base.Name = "frmServicesTree";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tabItemType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCanModPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItem).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tabItem).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboServiceAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSubAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsActive).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGPrices).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCanModPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tabItemType).EndInit();
		((System.Windows.Forms.Control)(object)this.tabItemType).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtBarCode).EndInit();
		this.contextMenuStrip1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
