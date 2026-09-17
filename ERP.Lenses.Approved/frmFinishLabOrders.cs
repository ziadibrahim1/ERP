using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.POS;
using BusinessLayer.SMS;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Lenses.Transactions;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Approved;

public class frmFinishLabOrders : frmPosted
{
	private DataTable dtSMSSettings;

	private DataTable dtLabOrdersDetails;

	private DataTable dtColors;

	private DataTable dtSubAccounts;

	private DataTable dtSizes;

	private DataTable dtItems;

	private DataTable dtItemPrices;

	private DataTable dtLabsItemsPrices;

	private DataTable dtItemsColorCategorysDetails;

	private DataTable dtItemsSizeCategorysDetails;

	private DataTable dtPOSDefaultData;

	private SoundPlayer player;

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlOnCostOfID = new ValueList();

	private DataSet ds;

	private bool SMSModuleInstalled = false;

	private IContainer components = null;

	private Timer timer1;

	public frmFinishLabOrders()
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
		NoCol = "LabOrderNo";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtPOSDefaultData = BusinessLayer.POS.Settings.SelectByBranchIDWithoutImage(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0");
		SMSModuleInstalled = Convert.ToBoolean(GlobalVariables.dtSystemModules.Select("ModuleEnName='SMS'")[0]["Installed"]);
		if (SMSModuleInstalled)
		{
			dtSMSSettings = TransactionsSettings.SelectByBranchID(GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		}
		dtItemsColorCategorysDetails = ItemsColorCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
		dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlColors.ValueListItems.Clear();
		for (int i = 0; i < dtColors.Rows.Count; i++)
		{
			vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
		}
		dtItemsSizeCategorysDetails = ItemsSizeCategorysDetails.Select("-1", "-1", "1", IsFromServer: false);
		dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSizes.ValueListItems.Clear();
		for (int j = 0; j < dtSizes.Rows.Count; j++)
		{
			vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
		}
		dtItems = Items.FillComboWithItemType("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		vlOnCostOfID.ValueListItems.Clear();
		vlOnCostOfID.ValueListItems.Add((object)1, GlobalVariables.IsArabic ? "معمل" : "Lab");
		vlOnCostOfID.ValueListItems.Add((object)2, GlobalVariables.IsArabic ? "فرع" : "Store");
		vlOnCostOfID.ValueListItems.Add((object)3, GlobalVariables.IsArabic ? "عميل" : "Client");
		timer1.Start();
		player = new SoundPlayer(Resources.ShipBell);
	}

	public override void FillGrid()
	{
		dtsource = LabOrders.SelectByLabOrdersFinished("," + GlobalVariables.CurrentBranchID + ",", "0", GlobalVariables.IsArabic ? "1" : "0");
		dtLabOrdersDetails = LabOrdersDetails.SelectByLabOrdersFinished("," + GlobalVariables.CurrentBranchID + ",", "0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtsource);
		ds.Tables.Add(dtLabOrdersDetails);
		ds.Tables[0].TableName = "dtsource";
		ds.Tables[1].TableName = "dtLabOrdersDetails";
		ds.Relations.Add(ds.Tables[0].Columns["LabOrderID"], ds.Tables[1].Columns["LabOrderID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value)
				{
					continue;
				}
				DataRow dataRow = dtItems.Select("ItemID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0];
				if (dataRow["ItemColorCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value = DBNull.Value;
					}
				}
				if (dataRow["ItemSizeCategoryID"] != DBNull.Value)
				{
					((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
					if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].ValueList.ItemCount == 0)
					{
						((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value = DBNull.Value;
					}
				}
			}
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].MaskInput = "dd/mm/yyyy hh:mm:ss";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].MaskInput = "dd/mm/yyyy hh:mm:ss";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabName"].Header).Caption = (GlobalVariables.IsArabic ? "المعمل" : "Lab");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FinishedDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Header).Caption = (GlobalVariables.IsArabic ? "انتهاء" : "Finish");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsFinished"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].HeaderVisible = true;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Header).Caption = (GlobalVariables.IsArabic ? "الهوالك" : "Destroyed");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "السعر" : "Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromBranchStore"].Header).Caption = (GlobalVariables.IsArabic ? "من المحل" : "From Store");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LabUnitPrice"].Header).Caption = (GlobalVariables.IsArabic ? "سعر المعمل" : " Lab Price");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromLabStore"].Header).Caption = (GlobalVariables.IsArabic ? "من المعمل" : "From Lab");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OnCostOfID"].Header).Caption = (GlobalVariables.IsArabic ? "تحميل" : "Cost");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromLabStore"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromBranchStore"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LabUnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OnCostOfID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OnCostOfID"].ValueList = (IValueList)(object)vlOnCostOfID;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromLabStore"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromBranchStore"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["UnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["LabUnitPrice"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorID"].DefaultCellValue = 1;
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "IsFinished" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "FinishedDate")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		int num = 0;
		int num2 = 0;
		num = ((dtsource != null) ? dtsource.Rows.Count : 0);
		FillGrid();
		num2 = ((dtsource != null) ? dtsource.Rows.Count : 0);
		if (num2 > num)
		{
			player.Play();
		}
	}

	public override void btnPost_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				Main.ExecuteNonQuery(" Delete Lns_LabOrdersDetails Where IsDestroyed=1 And LabOrderID= " + ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString());
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					LabOrdersDetails.Insert_Update("-1", ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString(), "Null", (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value) ? "1" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsManufactured"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["UnitPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Notes"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Discount"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Discount"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaxValue"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["TaxValue"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualUnitSalesPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ActualUnitSalesPrice"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["GlassesTypeID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["GlassesTypeID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LenseDiameterID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LenseDiameterID"].Value.ToString(), (((UltraGridBase)ULGData).Rows[i].Cells["LabStoreID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].Cells["LabStoreID"].Value.ToString(), (dtPOSDefaultData.Rows[0]["DefaultStoreID"] == DBNull.Value) ? "Null" : dtPOSDefaultData.Rows[0]["DefaultStoreID"].ToString(), "0", DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", "0", "0", "1", (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["OnCostOfID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["OnCostOfID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromLabStore"].Value.ToString()) ? "1" : "0", (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LabUnitPrice"].Value == DBNull.Value) ? "0" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LabUnitPrice"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromBranchStore"].Value.ToString()) ? "1" : "0", DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["LabOrderDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), GlobalVariables.UserID);
				}
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFinished"].Value.ToString()))
				{
					LabOrders.SetFinishedFromLab(((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsFinished"].Value.ToString()) ? "1" : "0", DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate));
					ItemsTransactions.ManagementInsertUpdateDelete();
					ItemsTransactions.RecalculateCurrentQtyOnly();
					string text = LabOrders.AllowedQty_Message(((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString(), "1", "0", GlobalVariables.IsArabic ? "1" : "0");
					if (text != "")
					{
						GlobalVariables.InformationMB.Show(text);
						Main.RollbackBulkTrans(FromServer: false);
					}
					else
					{
						ItemsTransactions.ManageInThread();
						LabOrders.GenerateLabSalesJvs("," + ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString() + ",", GlobalVariables.UserID);
						if (SMSModuleInstalled && dtSMSSettings.Rows[0]["TemplateID"] != DBNull.Value)
						{
							GlobalFunctions.GenerateSMS(int.Parse(dtSMSSettings.Rows[0]["TemplateID"].ToString()), int.Parse(dtSMSSettings.Rows[0]["DelayUntilInMinutes"].ToString()), DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FinishedDate"].Value.ToString()), ((UltraGridBase)ULGData).Rows[i].Cells["ClientID"].Value, ((UltraGridBase)ULGData).Rows[i].Cells["ClientMobileNumber"].Value.ToString());
						}
					}
				}
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				return;
			}
		}
		FillGrid();
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.Lenses.Transactions.frmLnsLabOrders'").Length != 0)
			{
				frmLnsLabOrders frmLnsLabOrders3 = new frmLnsLabOrders(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["LabOrderID"].Value.ToString()));
				frmLnsLabOrders3.Size = new Size(base.Width, base.Height);
				frmLnsLabOrders3.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmLnsLabOrders3.lblTitle).Text = (GlobalVariables.IsArabic ? "فواتير البصريات" : "Lab Orders");
				frmLnsLabOrders3.ShowDialog();
			}
			else if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.Lenses.Transactions.frmLnsLabOrders2'").Length != 0)
			{
				frmLnsLabOrders2 frmLnsLabOrders4 = new frmLnsLabOrders2(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["LabOrderID"].Value.ToString()));
				frmLnsLabOrders4.Size = new Size(base.Width, base.Height);
				frmLnsLabOrders4.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmLnsLabOrders4.lblTitle).Text = (GlobalVariables.IsArabic ? "2فواتير البصريات" : "Lab Orders2");
				frmLnsLabOrders4.ShowDialog();
			}
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
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
	}

	private ValueList getColorsValueList(int ItemColorCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsColorCategorysDetails.Select("ItemColorCategoryID=" + ItemColorCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ColorID"].ToString(), dtColors.Select("ColorID =" + array[i]["ColorID"].ToString())[0]["ColorName"].ToString());
		}
		return val;
	}

	private ValueList getSizesValueList(int ItemSizeCategoryID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtItemsSizeCategorysDetails.Select("ItemSizeCategoryID=" + ItemSizeCategoryID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["ItemSizeID"].ToString(), dtSizes.Select("ItemSizeID =" + array[i]["ItemSizeID"].ToString())[0]["ItemSizeName"].ToString());
		}
		return val;
	}

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "FromLabStore")
		{
			((UltraGridBase)ULGData).UpdateData();
			if (e.Cell.Value.Equals(false))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["FromBranchStore"].Value = true;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["FromBranchStore"].Value = false;
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "FromBranchStore")
		{
			((UltraGridBase)ULGData).UpdateData();
			if (e.Cell.Value.Equals(false))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["FromLabStore"].Value = true;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["FromLabStore"].Value = false;
			}
		}
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "IsFinished")
		{
			((UltraGridBase)ULGData).ActiveRow.Cells["FinishedDate"].Value = GlobalFunctions.GetServerDateTimeNow();
			((UltraGridBase)ULGData).UpdateData();
		}
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0709: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Expected O, but got Unknown
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		((UltraGridBase)ULGData).UpdateData();
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ULGData.ActiveCell != null && ((KeyedSubObjectBase)e.Cell.Column).Key == "ItemID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			DataRow dataRow = dtItems.Select(" ItemID= " + e.Cell.Value.ToString())[0];
			((UltraGridBase)ULGData).ActiveRow.Cells["UnitID"].Value = dataRow["UnitID"];
			if (dtItemPrices == null && ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["PriceTypeID"].Value != DBNull.Value)
			{
				dtItemPrices = ItemsPrices.GetPriceWithItemDiscount(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["PriceTypeID"].Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["LabOrderDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["BranchID"].Value.ToString(), IsFromServer: false);
			}
			if (dtLabsItemsPrices == null && ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["LabID"].Value != DBNull.Value)
			{
				DataTable dataTable = LabsBranchesPriceTypes.SelectByLabBranchID(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["BranchID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["LabID"].Value.ToString(), IsFromServer: false);
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0]["PriceTypeID"] != DBNull.Value)
				{
					dtLabsItemsPrices = ItemsPrices.GetPriceWithItemDiscount(dataTable.Rows[0]["PriceTypeID"].ToString(), DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["LabOrderDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["BranchID"].Value.ToString(), IsFromServer: false);
				}
			}
			if (dtItemPrices != null && dtItemPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["ClientID"].Value != DBNull.Value)
			{
				DataRow dataRow2 = dtItemPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["UnitPrice"].Value = decimal.Parse(dataRow2["Price"].ToString()) - decimal.Parse(dataRow2["Price"].ToString()) * decimal.Parse(dataRow2["DiscountPercentage"].ToString()) / 100m;
			}
			if (dtLabsItemsPrices != null && dtLabsItemsPrices.Rows.Count > 0 && ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["LabID"].Value != DBNull.Value)
			{
				DataRow dataRow3 = dtLabsItemsPrices.Select(" ItemID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["ItemID"].Value.ToString())[0];
				((UltraGridBase)ULGData).ActiveRow.Cells["LabUnitPrice"].Value = decimal.Parse(dataRow3["Price"].ToString()) - decimal.Parse(dataRow3["Price"].ToString()) * decimal.Parse(dataRow3["DiscountPercentage"].ToString()) / 100m;
			}
			if (dataRow["ItemColorCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ColorID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ColorID"].ValueList = (IValueList)(object)getColorsValueList(int.Parse(dataRow["ItemColorCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ColorID"].Value = 1;
				e.Cell.Row.Cells["ColorID"].ValueList = null;
			}
			if (dataRow["ItemSizeCategoryID"] != DBNull.Value)
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = DBNull.Value;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = (IValueList)(object)getSizesValueList(int.Parse(dataRow["ItemSizeCategoryID"].ToString()));
			}
			else
			{
				e.Cell.Row.Cells["ItemSizeID"].Value = 1;
				e.Cell.Row.Cells["ItemSizeID"].ValueList = null;
			}
		}
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار الصنف ", "Please Select Item");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (int.Parse(dtItems.Select(" ItemID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0]["ItemTypeID"].ToString()) == 2 && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors"), " Please Select " + GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors"));
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ColorID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (int.Parse(dtItems.Select(" ItemID = " + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemID"].Value.ToString())[0]["ItemTypeID"].ToString()) == 2 && ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes"), " Please Select " + GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes"));
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ItemSizeID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["OnCostOfID"].Value == DBNull.Value)
				{
					GlobalVariables.InformationMB.Show(" برجاء إختيار التحميل ", "Please Select Cost");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["OnCostOfID"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromLabStore"].Value.ToString()) && !bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromBranchStore"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show(" لابد من إختيار موقع الصرف ", "Please Select Issued Locations");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromLabStore"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
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
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Expected O, but got Unknown
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Approved.frmFinishLabOrders));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
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
		base.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		base.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		this.timer1.Interval = 60000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this, "$this");
		base.Name = "frmFinishLabOrders";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
