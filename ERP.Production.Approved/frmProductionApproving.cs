using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Production;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Production.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Approved;

public class frmProductionApproving : frmPosted
{
	private IContainer components = null;

	public frmProductionApproving()
	{
		InitializeComponent();
		NoCol = "ProductionNo";
	}

	public override void FillGrid()
	{
		dtsource = Productions.SelectByProductionApproved(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovedQty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovedQty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التشغيلة" : "Batch NO");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Header).Caption = (GlobalVariables.IsArabic ? "وصفة الانتاج" : "Item Catalog");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Header).Caption = (GlobalVariables.IsArabic ? "الخط" : "Line");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ApprovedQty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية التامة" : "fineshed Qty");
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Balance"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Balance");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Header).Caption = (GlobalVariables.IsArabic ? "الرصيد" : "Balance");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Balance"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Balance"].Value = (GlobalVariables.IsArabic ? "الرصيد" : "Balance");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "إعتماد" : "Approve");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ApprovedQty" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "ProductionEndDate")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CompletePercentage")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	public override void btnPost_Click(object sender, EventArgs e)
	{
		int num = 0;
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
			{
				continue;
			}
			if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ProductionEndDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), IsFromServer: false))
			{
				GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
				break;
			}
			if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ProductionEndDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
			{
				GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
				break;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["ApprovedQty"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovedQty"].Value.ToString()) <= 0m || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ApprovedQty"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()))
			{
				GlobalVariables.InformationMB.Show(" لابد أن تكون الكميه التامة أقل من او تساوى الكمية الكلية ", "Completed Qty Must Equal Or Less Than total Qty");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["ApprovedQty"];
				ULGData.PerformAction((UltraGridAction)24);
				break;
			}
			Main.StartBulkTrans(FromServer: false);
			try
			{
				decimal num2 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["ApprovedQty"].Value);
				decimal num3 = Convert.ToDecimal(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value);
				ProductionsStages.SetApprove("-1", "," + ((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString() + ",", num2.ToString(), "1", DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ProductionEndDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.UserID);
				if (num2 == num3)
				{
					Productions.SetApprove("," + ((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString() + ",", "1");
				}
				ItemsTransactions.ManagementInsertUpdateDelete();
				ItemsTransactions.RecalculateCurrentQtyOnly();
				string text = Productions.AllowedQty_Message(((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
				if (text != "")
				{
					Main.RollbackBulkTrans(FromServer: false);
					GlobalVariables.InformationMB.Show(text);
					break;
				}
				Main.EndBulkTrans(FromServer: false);
				ItemsTransactions.ManageInThread();
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				break;
			}
			num++;
		}
		if (num > 0)
		{
			FillGrid();
			GlobalVariables.InformationMB.Show("تمت اعتماد عدد " + num, num + "Operations Approved successfully");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Open")
			{
				frmProductions frmProductions2 = new frmProductions(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value.ToString()));
				frmProductions2.Size = new Size(base.Width, base.Height);
				frmProductions2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmProductions2.lblTitle).Text = (GlobalVariables.IsArabic ? "الانتاج" : "Production");
				frmProductions2.ShowDialog();
			}
			else if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Balance")
			{
				frmProductionItemsBalance frmProductionItemsBalance2 = new frmProductionItemsBalance(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionEndDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate));
				frmProductionItemsBalance2.WindowState = FormWindowState.Normal;
				frmProductionItemsBalance2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmProductionItemsBalance2.lblTitle).Text = (GlobalVariables.IsArabic ? "رصيد الخامات" : "Items Balance");
				frmProductionItemsBalance2.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Approved.frmProductionApproving));
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
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmProductionApproving";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
