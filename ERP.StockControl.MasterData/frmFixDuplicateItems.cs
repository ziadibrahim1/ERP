using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.StockControl.MasterData;

public class frmFixDuplicateItems : frmBase
{
	private DataTable dtItems = new DataTable();

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraLabel lblTitle;

	public UltraButton btnOldItemSearch;

	private UltraComboEditor cboOldItem;

	private UltraLabel lblOldItem;

	public UltraButton btnNewItemSearch;

	private UltraComboEditor cboNewItem;

	private UltraLabel lblNewItem;

	public UltraButton btnSave;

	public UltraButton btnClose;

	public frmFixDuplicateItems()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtItems = Items.FillComboWithData("-1", "0", "-1", "1", "-1", "-1", "-1", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboOldItem, dtItems, "ItemID", "Name");
		GlobalFunctions.FillCombo(cboNewItem, dtItems, "ItemID", "Name");
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	public bool Validatition()
	{
		if (cboNewItem.SelectedIndex == -1 || cboOldItem.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء اختيار صنف " : "Please Select Item");
			return false;
		}
		if (Main.IsSynchronization)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا يمكن مسح التكرار بسبب التزامن" : "Cannot Fix The Duplicate Because Of The Synchronization");
			return false;
		}
		if (bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["IsSalesItem"].ToString()) != bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["IsSalesItem"].ToString()) || bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["IsProductionItem"].ToString()) != bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["IsProductionItem"].ToString()) || bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["IsItem"].ToString()) != bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["IsItem"].ToString()) || bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["IsService"].ToString()) != bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["IsService"].ToString()) || bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["IsRecipe"].ToString()) != bool.Parse(dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["IsRecipe"].ToString()) || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["ItemTypeID"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["ItemTypeID"].ToString())
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء مراجعة بيانات الصنف" : "Please Read Item Data");
			return false;
		}
		if (dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["UnitID"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["UnitID"].ToString())
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء مراجعة الوحدة" : "Please Read Unit Name");
			return false;
		}
		if (dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["ServiceAccountID"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["ServiceAccountID"].ToString() || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["SalesAccount"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["SalesAccount"].ToString() || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["SalesReturnsAccount"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["SalesReturnsAccount"].ToString() || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["CostOfSalesAccount"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["CostOfSalesAccount"].ToString() || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["PurchaseAccount"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["PurchaseAccount"].ToString() || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["PurchaseReturnsAccount"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["PurchaseReturnsAccount"].ToString() || dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboOldItem).Value.ToString())[0]["DepartmentIssueAccount"].ToString() != dtItems.Select(" ItemID = " + ((TextEditorControlBase)cboNewItem).Value.ToString())[0]["DepartmentIssueAccount"].ToString())
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء مراجعة حسابات الصنف" : "Please Read Item Accounts");
			return false;
		}
		if (((TextEditorControlBase)cboNewItem).Value == ((TextEditorControlBase)cboOldItem).Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء اختيار صنف اخر" : "Please Select Other Item");
			return false;
		}
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (Validatition())
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				Items.Replace(((TextEditorControlBase)cboOldItem).Value.ToString(), ((TextEditorControlBase)cboNewItem).Value.ToString(), IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				dtItems = Items.FillComboWithData("-1", "0", "-1", "1", "-1", "-1", "-1", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				GlobalFunctions.FillCombo(cboOldItem, dtItems, "ItemID", "Name");
				GlobalFunctions.FillCombo(cboNewItem, dtItems, "ItemID", "Name");
				cboNewItem.SelectedIndex = -1;
				cboOldItem.SelectedIndex = -1;
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "تم الحفظ بنجاح " : "Data Saved Successfuly");
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			}
		}
	}

	private void btnOldItemSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Items("-1", "0", "-1", "1", "1", "-1", "-1", "0", "-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboOldItem).Value = num;
		}
	}

	private void btnNewItemSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Items("-1", "0", "-1", "1", "1", "-1", "-1", "0", "-1", "-1", IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboNewItem).Value = num;
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.MasterData.frmFixDuplicateItems));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnOldItemSearch = new UltraButton();
		this.cboOldItem = new UltraComboEditor();
		this.lblOldItem = new UltraLabel();
		this.btnNewItemSearch = new UltraButton();
		this.cboNewItem = new UltraComboEditor();
		this.lblNewItem = new UltraLabel();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboOldItem).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNewItem).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance7");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance8");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnOldItemSearch, "btnOldItemSearch");
		((UltraControlBase)this.btnOldItemSearch).AlphaBlendMode = (AlphaBlendMode)0;
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance9");
		((ControlBase)this.btnOldItemSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnOldItemSearch).Name = "btnOldItemSearch";
		((System.Windows.Forms.Control)(object)this.btnOldItemSearch).Click += new System.EventHandler(btnOldItemSearch_Click);
		resources.ApplyResources(this.cboOldItem, "cboOldItem");
		((UltraControlBase)this.cboOldItem).AlphaBlendMode = (AlphaBlendMode)0;
		this.cboOldItem.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboOldItem).Name = "cboOldItem";
		resources.ApplyResources(this.lblOldItem, "lblOldItem");
		((UltraControlBase)this.lblOldItem).AlphaBlendMode = (AlphaBlendMode)0;
		this.lblOldItem.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOldItem).Name = "lblOldItem";
		((ControlBase)this.lblOldItem).WrapText = false;
		resources.ApplyResources(this.btnNewItemSearch, "btnNewItemSearch");
		((UltraControlBase)this.btnNewItemSearch).AlphaBlendMode = (AlphaBlendMode)0;
		((AppearanceBase)val4).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val4, "appearance10");
		((ControlBase)this.btnNewItemSearch).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnNewItemSearch).Name = "btnNewItemSearch";
		((System.Windows.Forms.Control)(object)this.btnNewItemSearch).Click += new System.EventHandler(btnNewItemSearch_Click);
		resources.ApplyResources(this.cboNewItem, "cboNewItem");
		((UltraControlBase)this.cboNewItem).AlphaBlendMode = (AlphaBlendMode)0;
		this.cboNewItem.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboNewItem).Name = "cboNewItem";
		resources.ApplyResources(this.lblNewItem, "lblNewItem");
		((UltraControlBase)this.lblNewItem).AlphaBlendMode = (AlphaBlendMode)0;
		this.lblNewItem.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNewItem).Name = "lblNewItem";
		((ControlBase)this.lblNewItem).WrapText = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val5).Image = resources.GetObject("appearance11.Image");
		resources.ApplyResources(val5, "appearance11");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val6).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val6, "appearance12");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewItemSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNewItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOldItemSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboOldItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOldItem);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmFixDuplicateItems";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOldItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboOldItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOldItemSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNewItem, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewItemSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboOldItem).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNewItem).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
