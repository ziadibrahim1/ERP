using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Lenses;
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
using Infragistics.Win.UltraWinTree;

namespace ERP.Lenses.MasterData;

public class frmOffersQtyDiscount : frmHeaderManyDetails
{
	private DataTable dtItems;

	private DataTable dtOffersItems;

	private IContainer components = null;

	private UltraTabPageControl ultraTabPageControl2;

	public UltraButton btnItemsSearch;

	public UltraTextEditor txtItems;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAll;

	private UltraLabel lblToDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel lblFromDate;

	private UltraDateTimeEditor dtpFromDate;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	public frmOffersQtyDiscount()
	{
		InitializeComponent();
		TableName = "Lns_Offers";
		IDCol = "OfferID";
		NoCol = "OfferCode";
		DateCol = "OfferFromDate";
	}

	public frmOffersQtyDiscount(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		dtpFromDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpToDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtItems = Items.FillTree("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, "ParentID", "ItemID", "Name", "ItemBarcode", "IsMain");
		}
		dtDetails = OffersQtyDiscounts.SelectByOfferID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OfferQtyDiscountID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ForAll"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LowestPrice"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HighestPrice"].DefaultCellValue = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QtyDiscount"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الخصم" : "Discount Ratio");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ForAll"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ForAll"].Header).Caption = (GlobalVariables.IsArabic ? "للكل" : "For All");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LowestPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LowestPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاقل سعرا" : "Lowest Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HighestPrice"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HighestPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الاعلى سعرا" : "Highest Price");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QtyDiscount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QtyDiscount"].Header).Caption = (GlobalVariables.IsArabic ? "كمية الخصم" : "Qty Discount ");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountRatio"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ForAll"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LowestPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["HighestPrice"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QtyDiscount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Offers.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["OfferCode"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["OfferNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["OfferNameEn"].ToString();
			dtpFromDate.Value = (DateTime)drMaster["OfferFromDate"];
			dtpToDate.Value = (DateTime)drMaster["OfferToDate"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = OffersQtyDiscounts.SelectByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtOffersItems = OffersItems.SelectByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraToggleEditorBase)chkAll).Checked = false;
			TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
			TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems);
			SetCheckedItems(dtOffersItems);
			TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
			TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
			((UltraGridBase)ULGData).DataSource = dtDetails;
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
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpFromDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpToDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnItemsSearch).Visible = !NavMode;
		((Control)(object)chkAll).Enabled = !NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		if (Adding)
		{
			dtpFromDate.DateTime = GlobalFunctions.GetServerDateTimeNow().Date;
			dtpToDate.DateTime = dtpFromDate.DateTime.Date.AddDays(1.0).AddSeconds(-1.0);
		}
		else
		{
			UltraDateTimeEditor obj = dtpFromDate;
			DateTime dateTime = (dtpToDate.DateTime = GlobalFunctions.GetServerDateTimeNow());
			obj.DateTime = dateTime;
		}
		((TextEditorControlBase)txtNotes).Clear();
		((Control)(object)txtCode).Text = (Adding ? Offers.GetCode(IsFromServer: false) : "");
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems);
		((Control)(object)txtItems).Text = "";
		((UltraToggleEditorBase)chkAll).Checked = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود العرض", "Please Enter Offer Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show(" برجاء إدخال إسم العرض بالعربية", "Please Enter Offer Arabic Name");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		if (dtpFromDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال من تاريخ " : "Please Enter The From Date");
			((Control)(object)dtpFromDate).Focus();
			return false;
		}
		if (dtpToDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الى تاريخ " : "Please Enter The To Date");
			((Control)(object)dtpToDate).Focus();
			return false;
		}
		if (Main.CheckForValue("Lns_Offers", "OfferCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["OfferCode"].ToString(), IsFromServer: false) > 0)
		{
			string code = Offers.GetCode(IsFromServer: false);
			GlobalVariables.QuestionMB.Show("رقم هذا الإذن متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Voucher Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (GetNodeCheckedIDs(TreeItems) == ",")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار أصناف العرض" : "Please Select Offer Items");
			return false;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء بيانات العرض" : "Please Enter Offer Data");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكمية" : "Please Enter Qty");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["QtyDiscount"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["QtyDiscount"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال كمية الخصم" : "Please Enter Qty Discount");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DiscountRatio"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال نسبة الخصم" : "Please discount Ratio");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ForAll"].Value.Equals(false) && ((UltraGridBase)ULGData).Rows[i].Cells["LowestPrice"].Value.Equals(false) && ((UltraGridBase)ULGData).Rows[i].Cells["HighestPrice"].Value.Equals(false))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار سياسة التسعير" : "Please Select Pricing policy");
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ForAll"].Value.Equals(true) && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["QtyDiscount"].Value.ToString()) != decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "الكمية لابد ان تساوى كمية الخصم" : "Enterd Qty Should Equal Discount Qty ");
				((UltraGridBase)ULGData).Rows[i].Cells["QtyDiscount"].Value = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString();
				return false;
			}
			if (decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["QtyDiscount"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "كمية الخصم لابد ان تكون اقل من او تساوى الكمية" : "Discount Qty Should Be Less Than Oe Equal Qty ");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["QtyDiscount"];
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()) == decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["Qty"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show("  لا يمكن تكرار الكمية ", "Cannot Duplicate The Same Qty ");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Qty"];
					return false;
				}
			}
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Offers.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", "0", "0", "0", "0", "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			string nodeCheckedIDs = GetNodeCheckedIDs(TreeItems);
			if (nodeCheckedIDs != ",")
			{
				OffersItems.InsertByItemIDs(nodeCheckedIDs, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			}
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OfferQtyDiscountID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			OffersQtyDiscounts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Offers.Insert_Update(drMaster["OfferID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "1", "0", "0", "0", "0", "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			OffersItems.DeleteByOfferID(num.ToString(), GlobalVariables.UserID, IsFromServer: false);
			string nodeCheckedIDs = GetNodeCheckedIDs(TreeItems);
			if (nodeCheckedIDs != ",")
			{
				OffersItems.InsertByItemIDs(nodeCheckedIDs, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			}
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OfferID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OfferQtyDiscountID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("Lns_OffersQtyDiscounts", "OfferID", drMaster["OfferID"].ToString(), "OfferQtyDiscountID", text);
			OffersQtyDiscounts.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OffersItems.DeleteByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			OffersQtyDiscounts.DeleteByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Offers.Delete(drMaster["OfferID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsCashBackOffersReport(1, 0, 0, IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OfferID"].ToString();
			FillData();
		}
	}

	public override void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And IsQtyDiscount=1 ");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	public override void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsQtyDiscount=1 ", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public override void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsQtyDiscount=1 ", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public string GetNodeCheckedIDs(UltraTree tree)
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)tree.Nodes).Count; i++)
		{
			text = ((((DisposableObjectCollectionBase)tree.Nodes[i].Nodes).Count != 0 || Convert.ToBoolean(((SubObjectBase)tree.Nodes[i]).Tag) || tree.Nodes[i].CheckedState != CheckState.Checked) ? (text + GetNodeCheckedChildsIDs(tree.Nodes[i])) : (text + ((KeyedSubObjectBase)tree.Nodes[i]).Key + ","));
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

	private void TreeItems_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
	}

	private void TreeItems_BeforeCheck(object sender, BeforeCheckEventArgs e)
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
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeItems);
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
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

	public void SetCheckedItems(DataTable dtOfferItem)
	{
		for (int i = 0; i < dtOfferItem.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeItems.GetNodeByKey(dtOffersItems.Rows[i]["ItemID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			for (int j = 0; j < ((DisposableObjectCollectionBase)nodeByKey.Nodes).Count; j++)
			{
				TreeFunctions.SetAllNodeChildsCheckState(nodeByKey.CheckedState, nodeByKey);
			}
			if (nodeByKey.Parent != null)
			{
				TreeFunctions.SetParentCheckedState(nodeByKey.Parent);
			}
		}
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAll);
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	private void txtItems_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = "Name Like '%" + ((Control)(object)txtItems).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems.ActiveNode = TreeItems.GetNodeByKey(dataView.ToTable().Rows[0]["ItemID"].ToString());
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i]["ItemID"].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "ForAll" || ((KeyedSubObjectBase)e.Cell.Column).Key == "LowestPrice" || ((KeyedSubObjectBase)e.Cell.Column).Key == "HighestPrice")
		{
			ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
			((UltraGridBase)ULGData).UpdateData();
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "ForAll" && e.Cell.Value.Equals(true))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["LowestPrice"].Value = false;
				((UltraGridBase)ULGData).ActiveRow.Cells["HighestPrice"].Value = false;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "LowestPrice" && e.Cell.Value.Equals(true))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ForAll"].Value = false;
				((UltraGridBase)ULGData).ActiveRow.Cells["HighestPrice"].Value = false;
			}
			if (((KeyedSubObjectBase)e.Cell.Column).Key == "HighestPrice" && e.Cell.Value.Equals(true))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["ForAll"].Value = false;
				((UltraGridBase)ULGData).ActiveRow.Cells["LowestPrice"].Value = false;
			}
			ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_0704: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Expected O, but got Unknown
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.MasterData.frmOffersQtyDiscount));
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
		Override val12 = new Override();
		Appearance val13 = new Appearance();
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.btnItemsSearch = new UltraButton();
		this.txtItems = new UltraTextEditor();
		this.TreeItems = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.lblToDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.lblFromDate = new UltraLabel();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance11");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		this.TreeItems.Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val12.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val12;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		this.TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.lblToDate, "lblToDate");
		this.lblToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).Name = "lblToDate";
		((ControlBase)this.lblToDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		this.dtpToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.lblFromDate, "lblFromDate");
		this.lblFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDate).Name = "lblFromDate";
		((ControlBase)this.lblFromDate).WrapText = false;
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		this.dtpFromDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
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
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmOffersQtyDiscount";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToDate, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
