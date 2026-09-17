using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.POS;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.POS.MasterData;

public class frmUpdateRoomGroupItems : frmBase
{
	private DataTable dtStores;

	private DataTable dtTaxes;

	private DataTable dtItems;

	public bool Reload = false;

	private string RoomID;

	private string BranchID;

	private IContainer components = null;

	public UltraButton btnSaveAndClose;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraLabel lblTitle2;

	public UltraTree TreeItems;

	protected internal UltraCheckEditor chkAll;

	private UltraCheckEditor chkModStore;

	public UltraLabel lblStore;

	public UltraComboEditor cboStore;

	private UltraCheckEditor chkModEditPrice;

	private UltraCheckEditor chkModPOS;

	private UltraComboEditor cboPrinterName;

	private UltraLabel lblPrinterName;

	private UltraCheckEditor chkModTax;

	private UltraLabel lblTax;

	private UltraComboEditor cboTax;

	public UltraComboEditor cboCanModifyPrice;

	public UltraLabel ultraLabel1;

	private UltraCheckEditor chkModDiscount;

	private UltraLabel lblDiscount;

	private UltraTextEditor txtDiscount;

	public UltraButton btnItemsSearch;

	public UltraTextEditor txtItems;

	private UltraLabel lblPickupPrinter;

	private UltraComboEditor cboPickupPrinter;

	private UltraCheckEditor chkModPickupPrinter;

	private UltraCheckEditor chkModEnforceAccessories;

	private UltraCheckEditor chkEnforceAccessories;

	private UltraCheckEditor chkExcludeCheckDiscount;

	private UltraCheckEditor chkModExcludeCheckDiscount;

	public frmUpdateRoomGroupItems(string roomID, string branchID)
	{
		RoomID = roomID;
		BranchID = branchID;
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtItems = Items.FillTreeWithRoomID(RoomID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, "ParentID", "ItemID", "Name", "", "IsMain");
		}
		dtTaxes = Taxs.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTax, dtTaxes, "TaxID", "TaxName");
		dtStores = Stores.FillCombo("-1", "-1", "," + BranchID + ",", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboStore, dtStores, "StoreID", "StoreName");
		foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
		{
			cboPrinterName.Items.Add((object)installedPrinter, installedPrinter);
			cboPickupPrinter.Items.Add((object)installedPrinter, installedPrinter);
		}
		((Control)(object)chkModPOS).Visible = true;
		((Control)(object)btnSave).Enabled = true;
		((Control)(object)btnSaveAndClose).Enabled = true;
		((Control)(object)btnCancel).Enabled = true;
	}

	public bool ValidateData()
	{
		if (((UltraToggleEditorBase)chkModStore).Checked && cboStore.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المخزن الإفتراضي", "Please select Default Store");
			((TextEditorControlBase)cboStore).Focus();
			return false;
		}
		if (((UltraToggleEditorBase)chkModEditPrice).Checked && cboCanModifyPrice.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار نوع تعديل السعر", "Please select Change Price Type");
			((TextEditorControlBase)cboCanModifyPrice).Focus();
			return false;
		}
		return true;
	}

	public void Save()
	{
		string treeCheckedNodesIDs = TreeFunctions.GetTreeCheckedNodesIDs(TreeItems);
		if (treeCheckedNodesIDs != ",")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				DataTable dataTable = RoomsItems.UpdateGroupItems(treeCheckedNodesIDs, RoomID, (!((UltraToggleEditorBase)chkModTax).Checked) ? "-1" : ((cboTax.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTax).Value.ToString()), (!((UltraToggleEditorBase)chkModStore).Checked) ? "-1" : ((cboStore.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboStore).Value.ToString()), (!((UltraToggleEditorBase)chkModPOS).Checked) ? "-1" : ((cboPrinterName.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPrinterName).Value.ToString()), (!((UltraToggleEditorBase)chkModPickupPrinter).Checked) ? "-1" : ((cboPickupPrinter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboPickupPrinter).Value.ToString()), (!((UltraToggleEditorBase)chkModEditPrice).Checked) ? "-1" : ((cboCanModifyPrice.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCanModifyPrice).Value.ToString()), (!((UltraToggleEditorBase)chkModEnforceAccessories).Checked) ? "-1" : (((UltraToggleEditorBase)chkEnforceAccessories).Checked ? "1" : "0"), (!((UltraToggleEditorBase)chkModDiscount).Checked) ? "-1" : (((object)txtDiscount).Equals((object)"") ? "Null" : ((Control)(object)txtDiscount).Text.ToString()), (!((UltraToggleEditorBase)chkModExcludeCheckDiscount).Checked) ? "-1" : (((UltraToggleEditorBase)chkExcludeCheckDiscount).Checked ? "1" : "0"), IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Items Saved");
				Reload = true;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			}
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
		}
	}

	private void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Save();
			Dispose();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void chkModTax_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboTax).Enabled = ((UltraToggleEditorBase)chkModTax).Checked;
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		TreeFunctions.SetAllTreeNodesCheckState(((UltraToggleEditorBase)chkAll).Checked, TreeItems);
		TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
	}

	private void TreeItems_AfterCheck(object sender, NodeEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		TreeItems.AfterCheck -= new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
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

	private void chkModStore_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboStore).Enabled = ((UltraToggleEditorBase)chkModStore).Checked;
	}

	private void chkModDiscount_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)txtDiscount).Enabled = ((UltraToggleEditorBase)chkModDiscount).Checked;
	}

	private void chkModPOS_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboPrinterName).Enabled = ((UltraToggleEditorBase)chkModPOS).Checked;
	}

	private void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			if (dtItems.Select("ItemID = " + dtSearchResult.Rows[i]["ItemID"].ToString()).Length != 0)
			{
				TreeItems.GetNodeByKey(dtSearchResult.Rows[i]["ItemID"].ToString()).CheckedState = CheckState.Checked;
			}
		}
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

	private void chkModEnforceAccessories_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkEnforceAccessories).Enabled = ((UltraToggleEditorBase)chkModEnforceAccessories).Checked;
	}

	private void chkModExcludeCheckDiscount_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)chkExcludeCheckDiscount).Enabled = ((UltraToggleEditorBase)chkModExcludeCheckDiscount).Checked;
	}

	private void chkModPickupPrinter_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboPickupPrinter).Enabled = ((UltraToggleEditorBase)chkModPickupPrinter).Checked;
	}

	private void chkModEditPrice_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboCanModifyPrice).Enabled = ((UltraToggleEditorBase)chkModEditPrice).Checked;
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Expected O, but got Unknown
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.MasterData.frmUpdateRoomGroupItems));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Override val8 = new Override();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		ValueListItem val16 = new ValueListItem();
		ValueListItem val17 = new ValueListItem();
		ValueListItem val18 = new ValueListItem();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		this.btnSaveAndClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.TreeItems = new UltraTree();
		this.chkAll = new UltraCheckEditor();
		this.chkModStore = new UltraCheckEditor();
		this.lblStore = new UltraLabel();
		this.cboStore = new UltraComboEditor();
		this.chkModEditPrice = new UltraCheckEditor();
		this.chkModPOS = new UltraCheckEditor();
		this.cboPrinterName = new UltraComboEditor();
		this.lblPrinterName = new UltraLabel();
		this.chkModTax = new UltraCheckEditor();
		this.lblTax = new UltraLabel();
		this.cboTax = new UltraComboEditor();
		this.cboCanModifyPrice = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		this.chkModDiscount = new UltraCheckEditor();
		this.lblDiscount = new UltraLabel();
		this.txtDiscount = new UltraTextEditor();
		this.btnItemsSearch = new UltraButton();
		this.txtItems = new UltraTextEditor();
		this.lblPickupPrinter = new UltraLabel();
		this.cboPickupPrinter = new UltraComboEditor();
		this.chkModPickupPrinter = new UltraCheckEditor();
		this.chkModEnforceAccessories = new UltraCheckEditor();
		this.chkEnforceAccessories = new UltraCheckEditor();
		this.chkExcludeCheckDiscount = new UltraCheckEditor();
		this.chkModExcludeCheckDiscount = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModEditPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPOS).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCanModifyPrice).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPickupPrinter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPickupPrinter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModEnforceAccessories).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceAccessories).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkExcludeCheckDiscount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkModExcludeCheckDiscount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
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
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		this.TreeItems.Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		val8.NodeStyle = (NodeStyle)1;
		this.TreeItems.Override = val8;
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		this.TreeItems.AfterCheck += new AfterNodeChangedEventHandler(TreeItems_AfterCheck);
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance8");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.chkModStore, "chkModStore");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance9");
		((UltraToggleEditorBase)this.chkModStore).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkModStore).Name = "chkModStore";
		((UltraToggleEditorBase)this.chkModStore).CheckedChanged += new System.EventHandler(chkModStore_CheckedChanged);
		resources.ApplyResources(this.lblStore, "lblStore");
		this.lblStore.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblStore).Name = "lblStore";
		((ControlBase)this.lblStore).WrapText = false;
		resources.ApplyResources(this.cboStore, "cboStore");
		this.cboStore.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboStore).Name = "cboStore";
		((TextEditorControlBase)this.cboStore).Nullable = false;
		resources.ApplyResources(this.chkModEditPrice, "chkModEditPrice");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance10");
		((UltraToggleEditorBase)this.chkModEditPrice).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.chkModEditPrice).Name = "chkModEditPrice";
		((UltraToggleEditorBase)this.chkModEditPrice).CheckedChanged += new System.EventHandler(chkModEditPrice_CheckedChanged);
		resources.ApplyResources(this.chkModPOS, "chkModPOS");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance11");
		((UltraToggleEditorBase)this.chkModPOS).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.chkModPOS).Name = "chkModPOS";
		((UltraToggleEditorBase)this.chkModPOS).CheckedChanged += new System.EventHandler(chkModPOS_CheckedChanged);
		resources.ApplyResources(this.cboPrinterName, "cboPrinterName");
		((System.Windows.Forms.Control)(object)this.cboPrinterName).Name = "cboPrinterName";
		resources.ApplyResources(this.lblPrinterName, "lblPrinterName");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance12");
		((ControlBase)this.lblPrinterName).Appearance = (AppearanceBase)(object)val13;
		this.lblPrinterName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPrinterName).Name = "lblPrinterName";
		((ControlBase)this.lblPrinterName).WrapText = false;
		resources.ApplyResources(this.chkModTax, "chkModTax");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance13");
		((UltraToggleEditorBase)this.chkModTax).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.chkModTax).Name = "chkModTax";
		((UltraToggleEditorBase)this.chkModTax).CheckedChanged += new System.EventHandler(chkModTax_CheckedChanged);
		resources.ApplyResources(this.lblTax, "lblTax");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance14");
		((ControlBase)this.lblTax).Appearance = (AppearanceBase)(object)val15;
		this.lblTax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTax).Name = "lblTax";
		((ControlBase)this.lblTax).WrapText = false;
		resources.ApplyResources(this.cboTax, "cboTax");
		((System.Windows.Forms.Control)(object)this.cboTax).Name = "cboTax";
		resources.ApplyResources(this.cboCanModifyPrice, "cboCanModifyPrice");
		this.cboCanModifyPrice.AutoCompleteMode = (AutoCompleteMode)2;
		val16.DataValue = "-1";
		resources.ApplyResources(val16, "valueListItem1");
		((SubObjectBase)val16).ForceApplyResources = "";
		val17.DataValue = "0";
		resources.ApplyResources(val17, "valueListItem2");
		((SubObjectBase)val17).ForceApplyResources = "";
		val18.DataValue = "1";
		resources.ApplyResources(val18, "valueListItem3");
		((SubObjectBase)val18).ForceApplyResources = "";
		this.cboCanModifyPrice.Items.AddRange((ValueListItem[])(object)new ValueListItem[3] { val16, val17, val18 });
		((System.Windows.Forms.Control)(object)this.cboCanModifyPrice).Name = "cboCanModifyPrice";
		((TextEditorControlBase)this.cboCanModifyPrice).Nullable = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.chkModDiscount, "chkModDiscount");
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance15");
		((UltraToggleEditorBase)this.chkModDiscount).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.chkModDiscount).Name = "chkModDiscount";
		((UltraToggleEditorBase)this.chkModDiscount).CheckedChanged += new System.EventHandler(chkModDiscount_CheckedChanged);
		resources.ApplyResources(this.lblDiscount, "lblDiscount");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance16");
		((ControlBase)this.lblDiscount).Appearance = (AppearanceBase)(object)val20;
		this.lblDiscount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscount).Name = "lblDiscount";
		((ControlBase)this.lblDiscount).WrapText = false;
		resources.ApplyResources(this.txtDiscount, "txtDiscount");
		((System.Windows.Forms.Control)(object)this.txtDiscount).Name = "txtDiscount";
		((System.Windows.Forms.Control)(object)this.txtDiscount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val21, "appearance17");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val21;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Click += new System.EventHandler(btnItemsSearch_Click);
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.lblPickupPrinter, "lblPickupPrinter");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance18");
		((ControlBase)this.lblPickupPrinter).Appearance = (AppearanceBase)(object)val22;
		this.lblPickupPrinter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPickupPrinter).Name = "lblPickupPrinter";
		((ControlBase)this.lblPickupPrinter).WrapText = false;
		resources.ApplyResources(this.cboPickupPrinter, "cboPickupPrinter");
		((System.Windows.Forms.Control)(object)this.cboPickupPrinter).Name = "cboPickupPrinter";
		resources.ApplyResources(this.chkModPickupPrinter, "chkModPickupPrinter");
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance19");
		((UltraToggleEditorBase)this.chkModPickupPrinter).Appearance = (AppearanceBase)(object)val23;
		((System.Windows.Forms.Control)(object)this.chkModPickupPrinter).Name = "chkModPickupPrinter";
		((UltraToggleEditorBase)this.chkModPickupPrinter).CheckedChanged += new System.EventHandler(chkModPickupPrinter_CheckedChanged);
		resources.ApplyResources(this.chkModEnforceAccessories, "chkModEnforceAccessories");
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance20");
		((UltraToggleEditorBase)this.chkModEnforceAccessories).Appearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.chkModEnforceAccessories).Name = "chkModEnforceAccessories";
		((UltraToggleEditorBase)this.chkModEnforceAccessories).CheckedChanged += new System.EventHandler(chkModEnforceAccessories_CheckedChanged);
		resources.ApplyResources(this.chkEnforceAccessories, "chkEnforceAccessories");
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance21");
		((UltraToggleEditorBase)this.chkEnforceAccessories).Appearance = (AppearanceBase)(object)val25;
		((System.Windows.Forms.Control)(object)this.chkEnforceAccessories).Name = "chkEnforceAccessories";
		resources.ApplyResources(this.chkExcludeCheckDiscount, "chkExcludeCheckDiscount");
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance22");
		((UltraToggleEditorBase)this.chkExcludeCheckDiscount).Appearance = (AppearanceBase)(object)val26;
		((System.Windows.Forms.Control)(object)this.chkExcludeCheckDiscount).Name = "chkExcludeCheckDiscount";
		resources.ApplyResources(this.chkModExcludeCheckDiscount, "chkModExcludeCheckDiscount");
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance23");
		((UltraToggleEditorBase)this.chkModExcludeCheckDiscount).Appearance = (AppearanceBase)(object)val27;
		((System.Windows.Forms.Control)(object)this.chkModExcludeCheckDiscount).Name = "chkModExcludeCheckDiscount";
		((UltraToggleEditorBase)this.chkModExcludeCheckDiscount).CheckedChanged += new System.EventHandler(chkModExcludeCheckDiscount_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModExcludeCheckDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModEnforceAccessories);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkExcludeCheckDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkEnforceAccessories);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModPickupPrinter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPickupPrinter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModPOS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPickupPrinter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPrinterName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPrinterName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModEditPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkModStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCanModifyPrice);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboStore);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmUpdateRoomGroupItems";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCanModifyPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModStore, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModEditPrice, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPrinterName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPrinterName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPickupPrinter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModPOS, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPickupPrinter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModPickupPrinter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModTax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkEnforceAccessories, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkExcludeCheckDiscount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModEnforceAccessories, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkModExcludeCheckDiscount, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboStore).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModEditPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPOS).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPrinterName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCanModifyPrice).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPickupPrinter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModPickupPrinter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModEnforceAccessories).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkEnforceAccessories).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkExcludeCheckDiscount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkModExcludeCheckDiscount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
