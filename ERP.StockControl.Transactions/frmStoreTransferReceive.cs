using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.StockControl.Transactions;

public class frmStoreTransferReceive : frmPosted
{
	private int ReceivePeriod = 1;

	private DataTable dtMinAllowedTransDate;

	private IContainer components = null;

	public frmStoreTransferReceive()
	{
		InitializeComponent();
		NoCol = "VoucherNo";
	}

	public override void FillGrid()
	{
		ReceivePeriod = Convert.ToInt32(GlobalFunctions.GetDefault("StoreTransferReceivePeriod"));
		dtsource = StoreTransferVouchers.SelectByApproved(GlobalVariables.CurrentBranchID, "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtMinAllowedTransDate = Stores.GetMinAllowedTransDateAll(IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
		((Control)(object)btnSaveClose).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن " : "PS Request No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTransferVoucherDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTransferVoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاذن" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTransferVoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreTransferVoucherDate"].MaskInput = "dd/mm/yyyy hh:mm:ss";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التسليم" : "Deliverd Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].MaskInput = "dd/mm/yyyy hh:mm:ss";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameS"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameS"].Header).Caption = (GlobalVariables.IsArabic ? "من مخزن" : "Source Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameS"].Width = (int)((double)((Control)(object)ULGData).Width * 0.16);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameD"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameD"].Header).Caption = (GlobalVariables.IsArabic ? "إلي مخزن" : "Destination Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["StoreNameD"].Width = (int)((double)((Control)(object)ULGData).Width * 0.16);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.17);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = "";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "DeliverdDate")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void btnPost_Click(object sender, EventArgs e)
	{
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		SaveData();
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count < count)
		{
			GlobalVariables.InformationMB.Show("تمت العملي\u0651ة بنجاح", "Operation done successfully");
		}
	}

	public override void SaveData()
	{
		bool flag = false;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (!((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
				{
					continue;
				}
				if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: true))
				{
					GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
					Main.RollbackBulkTrans(FromServer: true);
					return;
				}
				if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: true))
				{
					GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you have chosen\n\r exists in closed fisical period");
					Main.RollbackBulkTrans(FromServer: true);
					return;
				}
				if (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value.ToString()) < DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["StoreTransferVoucherDate"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ استلام قبل تاريخ الحركه", "The Date you have chosen is before the Store Transfer Voucher Date ");
					Main.RollbackBulkTrans(FromServer: true);
					return;
				}
				if (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value.ToString()) > DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["StoreTransferVoucherDate"].Value.ToString()).AddDays(ReceivePeriod))
				{
					GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ استلام أكبر من فترة السماح", "The Date you have chosen is Greater than Allowed Receive Period ");
					Main.RollbackBulkTrans(FromServer: true);
					return;
				}
				if (DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value.ToString()) != DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["StoreTransferVoucherDate"].Value.ToString()))
				{
					GlobalVariables.QuestionMB.Show("تاريخ الاستلام مختلف عن تاريخ التحويل هل تريد الحفظ", "Deliverd Date Is Different From Voucher Date Are you Sure You Want To Save?");
					if (GlobalVariables.MessageBoxResult == 'N')
					{
						Main.RollbackBulkTrans(FromServer: true);
						return;
					}
				}
				if (dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["DestinationStoreID"].Value.ToString() + " And Date > '" + ((DateTime)((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value).ToString(GlobalVariables.DateLongFormate) + "'").Length != 0)
				{
					GlobalVariables.QuestionMB.Show("لقد قمت بأختيار تاريخ يقع قبل أخر جرد او اعادة تقييم بتاريخ \n " + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["DestinationStoreID"].Value.ToString() + " And Date > '" + ((DateTime)((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value).ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n  على مخزن   " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreNameD"].Value.ToString() + "\n  هل تريد الحفظ   ", "\n  The Date you choosed Before Last Store Taking Or Revaluation With Date" + dtMinAllowedTransDate.Select(" StoreID= " + ((UltraGridBase)ULGData).Rows[i].Cells["DestinationStoreID"].Value.ToString() + " And Date > '" + ((DateTime)((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value).ToString(GlobalVariables.DateLongFormate) + "'")[0]["Date"].ToString() + "\n   On Store " + ((UltraGridBase)ULGData).Rows[i].Cells["StoreNameD"].Value.ToString() + "\n   Are You Sure You Want To Save? ");
					if (GlobalVariables.MessageBoxResult == 'N')
					{
						Main.RollbackBulkTrans(FromServer: true);
						return;
					}
				}
				flag = true;
				StoreTransferVouchers.SetApprove("1", ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString(), ((DateTime)((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value).ToString(GlobalVariables.DateLongFormate), GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			return;
		}
		if (flag)
		{
			ItemsTransactions.ManageInThread();
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد تحويلات لإعتمادها " : "There are No transfer Vouchers to Approve");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmStoreTransferVouchers frmStoreTransferVouchers2 = new frmStoreTransferVouchers(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
			frmStoreTransferVouchers2.Size = new Size(base.Width, base.Height);
			frmStoreTransferVouchers2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmStoreTransferVouchers2.lblTitle).Text = (GlobalVariables.IsArabic ? "أذن تحويل" : "Store Transfer Voucher");
			frmStoreTransferVouchers2.ShowDialog();
		}
	}

	public override void Search()
	{
		DataTable dataTable = SearchFunctions.StoreTransferVouchersReport("-1", 0, 0, FromServer: true);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (dataTable.Rows[i]["StoreTransferVoucherID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[j].Cells["Approved"].Value = true;
				}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.StockControl.Transactions.frmStoreTransferReceive));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
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
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(this, "$this");
		base.Name = "frmStoreTransferReceive";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		base.ResumeLayout(false);
	}
}
