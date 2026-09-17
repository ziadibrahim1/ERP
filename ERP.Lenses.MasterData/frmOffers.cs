using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Lenses;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.Lenses.MasterData;

public class frmOffers : frmButtons
{
	private DataTable dtItems = new DataTable();

	private DataTable dtItemsDetails = new DataTable();

	private DataTable dtGifts = new DataTable();

	private DataTable dtGiftsDetails = new DataTable();

	private DataRow drMaster;

	private IContainer components = null;

	public UltraButton btnCopyTo;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraTextEditor txtCode;

	public UltraLabel lblCode;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	public UltraGroupBox UGBItems;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAllItems;

	public UltraButton btnItemsSearch;

	public UltraTextEditor txtItemsSearch;

	private UltraLabel lblToDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel lblFromDate;

	private UltraDateTimeEditor dtpFromDate;

	public UltraGroupBox UGBGifts;

	public UltraButton btnGiftsSearch;

	public UltraTextEditor txtGiftsSearch;

	public UltraTree TreeGifts;

	protected internal UltraCheckEditor chkAllGifts;

	private UltraLabel lblItemsQty;

	public UltraTextEditor txtItemsQty;

	private UltraLabel lblGiftsDiscountRatio;

	public UltraTextEditor txtGiftsDiscountRatio;

	private UltraLabel lblGiftsQty;

	public UltraTextEditor txtGiftsQty;

	public frmOffers()
	{
		InitializeComponent();
		TableName = "Lns_Offers";
		IDCol = "OfferID";
		NoCol = "OfferCode";
		DateCol = "GetDate()";
	}

	public frmOffers(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Enabled = CanSearching);
		bool enabled = (((Control)(object)obj2).Enabled = flag);
		((Control)(object)obj).Enabled = enabled;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpFromDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtpToDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtItems = (dtGifts = Items.FillTree("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false));
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, "ParentID", "ItemID", "Name", "ItemBarcode", "IsMain");
		}
		if (dtGifts != null)
		{
			TreeFunctions.FillTree(TreeGifts, dtGifts, "ParentID", "ItemID", "Name", "ItemBarcode", "IsMain");
		}
		DataTable dt = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTransactionBranch, dt, "BranchID", "BranchName");
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Offers.Select(RowID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Expected O, but got Unknown
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Expected O, but got Unknown
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Expected O, but got Unknown
		if (drMaster != null)
		{
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["OfferCode"].ToString();
			((Control)(object)txtArabicName).Text = drMaster["OfferNameAr"].ToString();
			((Control)(object)txtEnglishName).Text = drMaster["OfferNameEn"].ToString();
			dtpFromDate.Value = (DateTime)drMaster["OfferFromDate"];
			dtpToDate.Value = (DateTime)drMaster["OfferToDate"];
			((Control)(object)txtItemsQty).Text = drMaster["ItemsQty"].ToString();
			((Control)(object)txtGiftsQty).Text = drMaster["GiftsQty"].ToString();
			((Control)(object)txtGiftsDiscountRatio).Text = drMaster["GiftsDiscountRatio"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtItemsDetails = OffersItems.SelectByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtGiftsDetails = OffersGifts.SelectByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			((UltraToggleEditorBase)chkAllItems).Checked = false;
			TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
			TreeItems.BeforeCheck -= new BeforeCheckEventHandler(TreeItems_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeItems);
			SetCheckedItems(dtItemsDetails);
			TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
			TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
			((UltraToggleEditorBase)chkAllGifts).Checked = false;
			TreeGifts.AfterCheck -= new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
			TreeGifts.BeforeCheck -= new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
			TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeGifts);
			SetCheckedGifts(dtGiftsDetails);
			TreeGifts.AfterCheck += new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
			TreeGifts.BeforeCheck += new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
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
		((EditorButtonControlBase)txtItemsQty).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGiftsQty).ReadOnly = NavMode;
		((EditorButtonControlBase)txtGiftsDiscountRatio).ReadOnly = NavMode;
		((Control)(object)btnItemsSearch).Visible = !NavMode;
		((Control)(object)chkAllItems).Enabled = !NavMode;
		((Control)(object)btnGiftsSearch).Visible = !NavMode;
		((Control)(object)chkAllGifts).Enabled = !NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
		((Control)(object)btnPrint).Visible = false;
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
		((Control)(object)txtItemsSearch).Text = "";
		((UltraToggleEditorBase)chkAllItems).Checked = false;
		((Control)(object)txtItemsQty).Text = "0";
		TreeFunctions.SetAllTreeNodesCheckState(Checked: false, TreeGifts);
		((Control)(object)txtGiftsSearch).Text = "";
		((UltraToggleEditorBase)chkAllGifts).Checked = false;
		((Control)(object)txtGiftsQty).Text = "0";
		((Control)(object)txtGiftsDiscountRatio).Text = "0";
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
		if (((Control)(object)txtItemsQty).Text == "" || decimal.Parse(((Control)(object)txtItemsQty).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال كمية الاصناف " : "Please Enter Items Qty");
			((TextEditorControlBase)txtItemsQty).Focus();
			return false;
		}
		if (((Control)(object)txtGiftsQty).Text == "" || decimal.Parse(((Control)(object)txtGiftsQty).Text) <= 0m)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال كمية الهدايا " : "Please Enter Gifts Qty");
			((TextEditorControlBase)txtItemsQty).Focus();
			return false;
		}
		if (GetNodeCheckedIDs(TreeItems) == ",")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار أصناف العرض" : "Please Select Offer Items");
			return false;
		}
		if (GetNodeCheckedIDs(TreeGifts) == ",")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال هدايا العرض " : "Please Enter Offer Gifts ");
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
		return base.ValidateData();
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = Offers.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "0", "0", ((Control)(object)txtItemsQty).Text, ((Control)(object)txtGiftsQty).Text, (((Control)(object)txtGiftsDiscountRatio).Text == "") ? "0" : ((Control)(object)txtGiftsDiscountRatio).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			string nodeCheckedIDs = GetNodeCheckedIDs(TreeItems);
			string nodeCheckedIDs2 = GetNodeCheckedIDs(TreeGifts);
			if (nodeCheckedIDs != ",")
			{
				OffersItems.InsertByItemIDs(nodeCheckedIDs, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			}
			if (nodeCheckedIDs2 != ",")
			{
				OffersGifts.InsertByItemIDs(nodeCheckedIDs2, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			}
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
			int num = Offers.Insert_Update(drMaster["OfferID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, dtpFromDate.DateTime.ToString(GlobalVariables.DateLongFormate), dtpToDate.DateTime.ToString(GlobalVariables.DateLongFormate), "0", "0", "0", ((Control)(object)txtItemsQty).Text, ((Control)(object)txtGiftsQty).Text, (((Control)(object)txtGiftsDiscountRatio).Text == "") ? "0" : ((Control)(object)txtGiftsDiscountRatio).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			OffersGifts.DeleteByOfferID(num.ToString(), GlobalVariables.UserID, IsFromServer: false);
			OffersItems.DeleteByOfferID(num.ToString(), GlobalVariables.UserID, IsFromServer: false);
			string nodeCheckedIDs = GetNodeCheckedIDs(TreeItems);
			string nodeCheckedIDs2 = GetNodeCheckedIDs(TreeGifts);
			if (nodeCheckedIDs != ",")
			{
				OffersItems.InsertByItemIDs(nodeCheckedIDs, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			}
			if (nodeCheckedIDs2 != ",")
			{
				OffersGifts.InsertByItemIDs(nodeCheckedIDs2, num.ToString(), GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			}
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
			OffersGifts.DeleteByOfferID(drMaster["OfferID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
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

	public virtual void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		SetControls(NavMode: false);
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnUpdateClick();
		}
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		if (Adding)
		{
			drMaster = null;
		}
	}

	private void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.LnsOffersReport(0, 0, 0, IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["OfferID"].ToString();
			FillData();
		}
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		PriveousData();
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		NextData();
	}

	public virtual void PriveousData()
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsQtyDiscount=0 And IsCashBack = 0 ", "0");
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

	public virtual void NextData()
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
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, " And IsQtyDiscount=0 And IsCashBack = 0 ", "1");
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

	private void btnCopyTo_Click(object sender, EventArgs e)
	{
		btnCopyToClick();
	}

	private void txtCode_KeyUp(object sender, KeyEventArgs e)
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
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 And IsQtyDiscount=0 And IsCashBack = 0 ");
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

	public override void btnRefreshDataClick()
	{
		dtItems = (dtGifts = Items.FillTree("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true));
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, "ParentID", "ItemID", "Name", "ItemBarcode", "IsMain");
		}
		if (dtGifts != null)
		{
			TreeFunctions.FillTree(TreeGifts, dtGifts, "ParentID", "ItemID", "Name", "ItemBarcode", "IsMain");
		}
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
		((UltraToggleEditorBase)chkAllItems).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAllItems);
		((UltraToggleEditorBase)chkAllItems).CheckedChanged += chkAll_CheckedChanged;
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
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllItems).Checked, TreeItems);
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

	public void SetCheckedItems(DataTable dtItems)
	{
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeItems.GetNodeByKey(dtItems.Rows[i]["ItemID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			((UltraControlBase)TreeItems).Update();
			for (int j = 0; j < ((DisposableObjectCollectionBase)nodeByKey.Nodes).Count; j++)
			{
				TreeFunctions.SetAllNodeChildsCheckState(nodeByKey.CheckedState, nodeByKey);
			}
			if (nodeByKey.Parent != null)
			{
				TreeFunctions.SetParentCheckedState(nodeByKey.Parent);
			}
		}
		((UltraToggleEditorBase)chkAllItems).CheckedChanged -= chkAll_CheckedChanged;
		SetCheckBoxAllState(TreeItems, chkAllItems);
		((UltraToggleEditorBase)chkAllItems).CheckedChanged += chkAll_CheckedChanged;
	}

	public void SetCheckedGifts(DataTable dtGifts)
	{
		for (int i = 0; i < dtGifts.Rows.Count; i++)
		{
			UltraTreeNode nodeByKey = TreeGifts.GetNodeByKey(dtGifts.Rows[i]["ItemID"].ToString());
			nodeByKey.CheckedState = CheckState.Checked;
			((UltraControlBase)TreeGifts).Update();
			for (int j = 0; j < ((DisposableObjectCollectionBase)nodeByKey.Nodes).Count; j++)
			{
				TreeFunctions.SetAllNodeChildsCheckState(nodeByKey.CheckedState, nodeByKey);
			}
			if (nodeByKey.Parent != null)
			{
				TreeFunctions.SetParentCheckedState(nodeByKey.Parent);
			}
		}
		((UltraToggleEditorBase)chkAllGifts).CheckedChanged -= chkAllGifts_CheckedChanged;
		SetCheckBoxAllState(TreeGifts, chkAllGifts);
		((UltraToggleEditorBase)chkAllGifts).CheckedChanged += chkAllGifts_CheckedChanged;
	}

	private void txtItemsSearch_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtItems);
		dataView.RowFilter = "Name Like '%" + ((Control)(object)txtItemsSearch).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeItems.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeItems.ActiveNode = TreeItems.GetNodeByKey(dataView.ToTable().Rows[0]["ItemID"].ToString());
		}
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i]["ItemID"].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void TreeGifts_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		TreeGifts.AfterCheck -= new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
		TreeGifts.BeforeCheck -= new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
		for (int i = 0; i < ((DisposableObjectCollectionBase)e.TreeNode.Nodes).Count; i++)
		{
			TreeFunctions.SetAllNodeChildsCheckState(e.TreeNode.CheckedState, e.TreeNode);
		}
		if (e.TreeNode.Parent != null)
		{
			TreeFunctions.SetParentCheckedState(e.TreeNode.Parent);
		}
		((UltraToggleEditorBase)chkAllGifts).CheckedChanged -= chkAllGifts_CheckedChanged;
		SetCheckBoxAllState(TreeGifts, chkAllGifts);
		((UltraToggleEditorBase)chkAllGifts).CheckedChanged += chkAllGifts_CheckedChanged;
		TreeGifts.AfterCheck += new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
		TreeGifts.BeforeCheck += new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
	}

	private void TreeGifts_BeforeCheck(object sender, BeforeCheckEventArgs e)
	{
		if (!Adding && !Updating)
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void chkAllGifts_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		TreeGifts.AfterCheck -= new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
		TreeGifts.BeforeCheck -= new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAllGifts).Checked, TreeGifts);
		TreeGifts.AfterCheck += new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
		TreeGifts.BeforeCheck += new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
	}

	private void txtGiftsSearch_ValueChanged(object sender, EventArgs e)
	{
		DataView dataView = new DataView(dtGifts);
		dataView.RowFilter = "Name Like '%" + ((Control)(object)txtGiftsSearch).Text.Trim() + "%' ";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		if (dataView.ToTable().Rows.Count > 0)
		{
			TreeGifts.Override.ActiveNodeAppearance.BackColor = Color.Gray;
			TreeGifts.ActiveNode = TreeGifts.GetNodeByKey(dataView.ToTable().Rows[0]["ItemID"].ToString());
		}
	}

	private void btnGiftsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeGifts.GetNodeByKey(dtSearchResult.Rows[i]["ItemID"].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void txtItemsQty_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtGiftsQty_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtGiftsDiscountRatio_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		//IL_0c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Expected O, but got Unknown
		//IL_0c23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2d: Expected O, but got Unknown
		//IL_10be: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c8: Expected O, but got Unknown
		//IL_10d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.MasterData.frmOffers));
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
		Override val11 = new Override();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Override val15 = new Override();
		Appearance val16 = new Appearance();
		this.btnCopyTo = new UltraButton();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.UGBItems = new UltraGroupBox();
		this.lblItemsQty = new UltraLabel();
		this.txtItemsQty = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.txtItemsSearch = new UltraTextEditor();
		this.TreeItems = new UltraTree();
		this.chkAllItems = new UltraCheckEditor();
		this.lblToDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.lblFromDate = new UltraLabel();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.UGBGifts = new UltraGroupBox();
		this.lblGiftsDiscountRatio = new UltraLabel();
		this.txtGiftsDiscountRatio = new UltraTextEditor();
		this.lblGiftsQty = new UltraLabel();
		this.btnGiftsSearch = new UltraButton();
		this.txtGiftsQty = new UltraTextEditor();
		this.txtGiftsSearch = new UltraTextEditor();
		this.TreeGifts = new UltraTree();
		this.chkAllGifts = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemsSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGifts).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBGifts).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsDiscountRatio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsQty).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsSearch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeGifts).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllGifts).BeginInit();
		base.SuspendLayout();
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
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
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
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.UGBItems, "UGBItems");
		this.UGBItems.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.lblItemsQty);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.txtItemsQty);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.txtItemsSearch);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		((System.Windows.Forms.Control)(object)this.UGBItems).Controls.Add((System.Windows.Forms.Control)(object)this.chkAllItems);
		((System.Windows.Forms.Control)(object)this.UGBItems).Name = "UGBItems";
		resources.ApplyResources(this.lblItemsQty, "lblItemsQty");
		this.lblItemsQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblItemsQty).Name = "lblItemsQty";
		((ControlBase)this.lblItemsQty).WrapText = false;
		resources.ApplyResources(this.txtItemsQty, "txtItemsQty");
		((System.Windows.Forms.Control)(object)this.txtItemsQty).Name = "txtItemsQty";
		((System.Windows.Forms.Control)(object)this.txtItemsQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtItemsQty_KeyPress);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItemsSearch, "txtItemsSearch");
		((System.Windows.Forms.Control)(object)this.txtItemsSearch).Name = "txtItemsSearch";
		((TextEditorControlBase)this.txtItemsSearch).ValueChanged += new System.EventHandler(txtItemsSearch_ValueChanged);
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		this.TreeItems.Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val11.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val11;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		this.TreeItems.BeforeCheck += new BeforeCheckEventHandler(TreeItems_BeforeCheck);
		resources.ApplyResources(this.chkAllItems, "chkAllItems");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance11");
		((UltraToggleEditorBase)this.chkAllItems).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkAllItems).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllItems).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllItems).Name = "chkAllItems";
		((UltraControlBase)this.chkAllItems).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllItems).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.lblToDate, "lblToDate");
		this.lblToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).Name = "lblToDate";
		((ControlBase)this.lblToDate).WrapText = false;
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((UltraWinEditorMaskedControlBase)this.dtpToDate).AlwaysInEditMode = true;
		this.dtpToDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		resources.ApplyResources(this.lblFromDate, "lblFromDate");
		this.lblFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDate).Name = "lblFromDate";
		((ControlBase)this.lblFromDate).WrapText = false;
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((UltraWinEditorMaskedControlBase)this.dtpFromDate).AlwaysInEditMode = true;
		this.dtpFromDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		resources.ApplyResources(this.UGBGifts, "UGBGifts");
		this.UGBGifts.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.lblGiftsDiscountRatio);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.txtGiftsDiscountRatio);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.lblGiftsQty);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.btnGiftsSearch);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.txtGiftsQty);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.txtGiftsSearch);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.TreeGifts);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Controls.Add((System.Windows.Forms.Control)(object)this.chkAllGifts);
		((System.Windows.Forms.Control)(object)this.UGBGifts).Name = "UGBGifts";
		resources.ApplyResources(this.lblGiftsDiscountRatio, "lblGiftsDiscountRatio");
		this.lblGiftsDiscountRatio.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGiftsDiscountRatio).Name = "lblGiftsDiscountRatio";
		((ControlBase)this.lblGiftsDiscountRatio).WrapText = false;
		resources.ApplyResources(this.txtGiftsDiscountRatio, "txtGiftsDiscountRatio");
		((System.Windows.Forms.Control)(object)this.txtGiftsDiscountRatio).Name = "txtGiftsDiscountRatio";
		((System.Windows.Forms.Control)(object)this.txtGiftsDiscountRatio).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGiftsDiscountRatio_KeyPress);
		resources.ApplyResources(this.lblGiftsQty, "lblGiftsQty");
		this.lblGiftsQty.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGiftsQty).Name = "lblGiftsQty";
		((ControlBase)this.lblGiftsQty).WrapText = false;
		resources.ApplyResources(this.btnGiftsSearch, "btnGiftsSearch");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val13, "appearance12");
		((ControlBase)this.btnGiftsSearch).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.btnGiftsSearch).Name = "btnGiftsSearch";
		((System.Windows.Forms.Control)(object)this.btnGiftsSearch).Click += new System.EventHandler(btnGiftsSearch_Click);
		resources.ApplyResources(this.txtGiftsQty, "txtGiftsQty");
		((System.Windows.Forms.Control)(object)this.txtGiftsQty).Name = "txtGiftsQty";
		((System.Windows.Forms.Control)(object)this.txtGiftsQty).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtGiftsQty_KeyPress);
		resources.ApplyResources(this.txtGiftsSearch, "txtGiftsSearch");
		((System.Windows.Forms.Control)(object)this.txtGiftsSearch).Name = "txtGiftsSearch";
		((TextEditorControlBase)this.txtGiftsSearch).ValueChanged += new System.EventHandler(txtGiftsSearch_ValueChanged);
		resources.ApplyResources(this.TreeGifts, "TreeGifts");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance13");
		this.TreeGifts.Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.TreeGifts).Name = "TreeGifts";
		val15.NodeStyle = (NodeStyle)1;
		this.TreeGifts.Override = val15;
		((UltraControlBase)this.TreeGifts).UseAppStyling = false;
		this.TreeGifts.AfterCheck += new AfterNodeChangedEventHandler(TreeGifts_AfterCheck);
		this.TreeGifts.BeforeCheck += new BeforeCheckEventHandler(TreeGifts_BeforeCheck);
		resources.ApplyResources(this.chkAllGifts, "chkAllGifts");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance14");
		((UltraToggleEditorBase)this.chkAllGifts).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.chkAllGifts).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllGifts).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllGifts).Name = "chkAllGifts";
		((UltraControlBase)this.chkAllGifts).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAllGifts).CheckedChanged += new System.EventHandler(chkAllGifts_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBGifts);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmOffers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBGifts, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBItems).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBItems).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBItems).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtItemsQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItemsSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBGifts).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBGifts).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBGifts).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsDiscountRatio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsQty).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtGiftsSearch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeGifts).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllGifts).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
