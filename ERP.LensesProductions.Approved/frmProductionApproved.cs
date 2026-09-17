using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.LensesProductions;
using BusinessLayer.Privilege;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.LensesProductions.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.LensesProductions.Approved;

public class frmProductionApproved : frmPosted
{
	private DataTable dtBlanksInvoicesDetails;

	private DataTable dtBlanksInvoicesAdditions;

	private DataTable dtItems;

	private DataTable dtStores;

	private ValueList vlItems = new ValueList();

	private ValueList vlStores = new ValueList();

	private DataSet ds;

	private IContainer components = null;

	public frmProductionApproved()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		fromServer = true;
		NoCol = "BlankInvoiceNo";
	}

	public override void FillGrid()
	{
		DisplayDataDate = GlobalFunctions.GetServerDateTimeNow(IsFromServer: true);
		dtItems = Items.FillComboWithBlankType("-1", "-1", "0", "-1", "1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlItems.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtStores = Stores.FillCombo("-1", "0", GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlStores.ValueListItems.Clear();
		for (int j = 0; j < dtStores.Rows.Count; j++)
		{
			vlStores.ValueListItems.Add(dtStores.Rows[j]["StoreID"], dtStores.Rows[j]["StoreName"].ToString());
		}
		dtsource = BlanksInvoices.SelectByManufacturingFinished("-1", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtBlanksInvoicesDetails = BlanksInvoicesDetails.SelectByManufacturingFinished("-1", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtBlanksInvoicesAdditions = BlanksInvoicesAdditions.SelectByManufacturingFinished("-1", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		ds = new DataSet();
		ds.Tables.Add(dtsource);
		ds.Tables.Add(dtBlanksInvoicesDetails);
		ds.Tables.Add(dtBlanksInvoicesAdditions);
		ds.Tables[0].TableName = "dtsource";
		ds.Tables[1].TableName = "dtBlanksInvoicesDetails";
		ds.Tables[2].TableName = "dtBlanksInvoicesAdditions";
		ds.Relations.Add(ds.Tables[0].Columns["BlankInvoiceID"], ds.Tables[1].Columns["BlankInvoiceID"]);
		ds.Relations.Add(ds.Tables[0].Columns["BlankInvoiceID"], ds.Tables[2].Columns["BlankInvoiceID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
		for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; k++)
		{
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows).Count; l++)
			{
				((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BlankItemID"].ValueList = (IValueList)(object)getItemsValueList(int.Parse(((UltraGridBase)ULGData).Rows[k].ChildBands[0].Rows[l].Cells["BlankTypeID"].Value.ToString()));
			}
		}
		for (int m = 0; m < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; m++)
		{
			((UltraGridBase)ULGData).Rows[m].Cells["MaterialFromStoreID"].ValueList = (IValueList)(object)getStoresValueList(int.Parse(((UltraGridBase)ULGData).Rows[m].Cells["ManufacturingBranchID"].Value.ToString()));
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FactoryReceiveDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.06);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialFromStoreID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FactoryReceiveDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialFromStoreID"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BlankInvoiceDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Header).Caption = (GlobalVariables.IsArabic ? "الخط" : "Line");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FactoryReceiveDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ استلام الفرع" : "Factory Receive Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialFromStoreID"].Header).Caption = (GlobalVariables.IsArabic ? "مخزن الخامات" : "Material Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaterialFromStoreID"].ValueList = (IValueList)(object)vlStores;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Header).Caption = (GlobalVariables.IsArabic ? "انتهاء" : "Finish");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankTypeName"].Header).Caption = (GlobalVariables.IsArabic ? "خامة التصنيع" : "Blank Type");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Blank Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsRightEye"].Header).Caption = (GlobalVariables.IsArabic ? "Eye" : "Eye");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DColorName"].Header).Caption = "D- " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DItemSizeName"].Header).Caption = "D- " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorName"].Header).Caption = "R- " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeName"].Header).Caption = "R- " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Addition"].Header).Caption = "Add";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Ax"].Header).Caption = "Ax";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IPD"].Header).Caption = "IPD";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PH"].Header).Caption = "PH";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MM"].Header).Caption = "MM";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinished"].Header).Caption = (GlobalVariables.IsArabic ? "تصنيع" : "Finished");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankTypeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["BlankItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsRightEye"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DColorName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["DItemSizeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RColorName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["RItemSizeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Addition"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Ax"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IPD"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PH"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["MM"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsFinished"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BlankAdditionName"].Header).Caption = (GlobalVariables.IsArabic ? "عدسة التصنيع" : "Blank Addition");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["BlankAdditionName"].Hidden = false;
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsFinished") && (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MaterialFromStoreID") || ((UltraGridBase)ULGData).ActiveRow.Cells["BlankInvoiceApproveID"].Value != DBNull.Value))
			{
				((GridItemBase)ULGData.ActiveCell).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (!(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "IsFinished") && !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "BlankItemID"))
			{
				((GridItemBase)ULGData.ActiveCell).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 2)
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		DataTable dataTable = BlanksInvoicesMaterials.SelectByBlankInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (!ValidateData())
		{
			return;
		}
		string text = "";
		string text2 = "";
		string text3 = "";
		string text4 = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			text = "";
			text2 = "";
			text3 = "";
			text4 = ",";
			dataTable.Clear();
			text = text + " Update Lns_BlanksInvoices Set IsFinished = " + (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFinished"].Value.ToString()) ? "1" : "0") + " , FinishedDate = '" + DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate) + "', MaterialFromStoreID = " + ((UltraGridBase)ULGData).Rows[i].Cells["MaterialFromStoreID"].Value.ToString() + " Where  BlankInvoiceID  =" + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + "; exec SP_Trans_Log " + GlobalVariables.UserID + " ,'Lns_BlanksInvoices' ," + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString() + " ,'U';";
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsFinished"].Value.ToString()))
				{
					text4 = text4 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankInvoiceDetailID"].Value.ToString() + ",";
					text2 = text2 + " Update Lns_BlanksInvoicesDetails Set IsFinished =1" + (Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsNoBlank"].Value) ? "" : (", BlankItemID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value.ToString())) + " Where BlankInvoiceDetailID  = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankInvoiceDetailID"].Value.ToString() + ";";
					if (!Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsNoBlank"].Value))
					{
						text3 = text3 + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Text + " - ";
						DataRow dataRow = dataTable.NewRow();
						dataRow["BlankInvoiceMaterialID"] = -1;
						dataRow["BlankInvoiceID"] = ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString();
						dataRow["ItemID"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value.ToString();
						dataRow["ColorID"] = 1;
						dataRow["ItemSizeID"] = 1;
						dataRow["EstimatedQty"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString();
						dataRow["ActualQty"] = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Qty"].Value.ToString();
						dataRow["UnitID"] = dtItems.Select("ItemID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value.ToString())[0]["UnitID"];
						dataRow["VoucherDate"] = DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate);
						dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			MessageLog.DeleteByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "BlnkMIV", "BlnkMIV", IsFromServer: true);
			if (text2 != "")
			{
				Main.SyncExecuteNonQuery(text);
				Main.SyncExecuteNonQuery(text2);
			}
			else if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFinished"].Value.ToString()))
			{
				Main.SyncExecuteNonQuery(text);
			}
			if (text4 != ",")
			{
				int num = BlanksInvoicesApprove.Insert_Update("-1", ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), (text3 == "") ? "Null" : text3.Remove(text3.Length - 2), "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
				if (dataTable.Rows.Count > 0)
				{
					for (int k = 0; k < dataTable.Rows.Count; k++)
					{
						dataTable.Rows[k]["BlankInvoiceApproveID"] = num;
					}
					dataTable.AcceptChanges();
					BlanksInvoicesMaterials.Insert_UpdateByTable(dataTable, GlobalVariables.UserID, IsFromServer: true);
				}
				BlanksInvoicesAdditions.Production(text4, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			}
			string text5 = MessageLog.SelectByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "BlnkMIV", "BlnkMIV", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (text5 != "")
			{
				GlobalVariables.InformationMB.Show(text5);
				Main.RollbackBulkTrans(FromServer: true);
				break;
			}
			ItemsTransactions.ManageInThread();
		}
		FillGrid();
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			frmBlanksInvoices frmBlanksInvoices2 = new frmBlanksInvoices(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BlankInvoiceID"].Value.ToString()), _fromApprovalForm: true);
			frmBlanksInvoices2.Size = new Size(base.Width, base.Height);
			frmBlanksInvoices2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmBlanksInvoices2.lblTitle).Text = (GlobalVariables.IsArabic ? "فاتورة التصنيعات" : "Blanks Invoices");
			frmBlanksInvoices2.ShowDialog();
		}
	}

	public override void AfterSelectChange()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 || ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 2))
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsFinished" && bool.Parse(e.Cell.Value.ToString()))
		{
			if (((GridItemBase)e.Cell).Band.Index == 0)
			{
				for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows).Count; i++)
				{
					((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows[i].Cells["IsFinished"].Value = true;
				}
			}
			else if (((GridItemBase)e.Cell).Band.Index == 1 && dtBlanksInvoicesDetails.Select("BlankInvoiceID = " + e.Cell.Row.Cells["BlankInvoiceID"].Value.ToString()).Length == dtBlanksInvoicesDetails.Select("BlankInvoiceID = " + e.Cell.Row.Cells["BlankInvoiceID"].Value.ToString() + " and IsFinished = 1").Length)
			{
				e.Cell.Row.ParentRow.Cells["IsFinished"].Value = true;
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private ValueList getItemsValueList(int BlankTypeID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItems.Select("BlankTypeID=" + BlankTypeID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemID"].ToString(), array[i]["Name"].ToString());
		}
		return val;
	}

	private ValueList getStoresValueList(int BranchID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtStores.Select("BranchID =" + BranchID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["StoreID"].ToString(), array[i]["StoreName"].ToString());
		}
		return val;
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFinished"].Value.ToString()))
			{
				if (Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "Lns_BlanksInvoices", IsFromServer: true))
				{
					GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" لابد من إختيار تاريخ الانتهاء ", "Please Select Finished Date");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value) < Convert.ToDateTime(((UltraGridBase)ULGData).Rows[i].Cells["FactoryReceiveDate"].Value))
				{
					GlobalVariables.InformationMB.Show("تاريخ الانتهاء قبل تاريخ الاستلام", "The Finishing Date is Before The Receiving Date. ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["MaterialFromStoreID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" لابد من إختيار مخزن الخامات ", "Please Select Material Store");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["MaterialFromStoreID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
			}
			if (ds.Tables[1].Select("IsFinished=1 and  BlankInvoiceID = " + ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString()).Length == 0)
			{
				continue;
			}
			if (Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), ((UltraGridBase)ULGData).Rows[i].Cells["BlankInvoiceID"].Value.ToString(), "Lns_BlanksInvoices", IsFromServer: true))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["MaterialFromStoreID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(" لابد من إختيار مخزن الخامات ", "Please Select Material Store");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["MaterialFromStoreID"];
				ULGData.PerformAction((UltraGridAction)24);
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (!Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsFinished"].Value) || Convert.ToBoolean(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsNoBlank"].Value))
				{
					continue;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" لابد من إختيار الصنف  ", "Please Select Blank Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				DataRow dataRow = dtItems.Select("ItemID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"].Value.ToString())[0];
				DataTable dataTable = BlanksTypesItems.ManufacturingValidateItemOrder(((UltraGridBase)ULGData).Rows[i].Cells["MaterialFromStoreID"].Value.ToString(), dataRow["BlankTypeID"].ToString(), dataRow["ItemOrder"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				if (dataTable.Rows.Count > 0)
				{
					string text = (GlobalVariables.IsArabic ? (" يوجد أصناف اخرى لها أولوية عن  " + dataRow["Name"].ToString() + " مثل ") : ("There Are other Items With Higher Item Order Instead Of This Item " + dataRow["Name"].ToString() + " Like : "));
					for (int k = 0; k < dataTable.Rows.Count; k++)
					{
						text = text + " \n " + dataTable.Rows[k]["ItemName"];
					}
					text = text + " \n" + (GlobalVariables.IsArabic ? "هل تريد استبداله؟" : "Would You Like To Change?");
					GlobalVariables.QuestionMB.Show(text, text);
					if (GlobalVariables.MessageBoxResult == 'Y')
					{
						ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["BlankItemID"];
						ULGData.ActiveCell.Value = dataTable.Rows[0]["ItemID"];
						ULGData.PerformAction((UltraGridAction)24);
						return false;
					}
				}
			}
		}
		return true;
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
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.LensesProductions.Approved.frmProductionApproved));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmProductionApproved";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
