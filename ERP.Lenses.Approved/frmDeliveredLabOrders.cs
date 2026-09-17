using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Lenses;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Lenses.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Approved;

public class frmDeliveredLabOrders : frmPosted
{
	private IContainer components = null;

	private Timer timer1;

	public frmDeliveredLabOrders()
	{
		InitializeComponent();
		NoCol = "LabOrderNo";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		timer1.Start();
	}

	public override void FillGrid()
	{
		dtsource = LabOrders.SelectByDeliverdToClient("," + GlobalVariables.CurrentBranchID + ",", "0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].MaskInput = "dd/mm/yyyy hh:mm:ss";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DeliverdDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاستلام" : "Delivered Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Header).Caption = (GlobalVariables.IsArabic ? "تسليم" : "Delivered");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDeliverd"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "IsDeliverd" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "DeliverdDate")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["IsDeliverd"].Value.Equals(true) && ((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء ادخال تاريخ التسليم" : "Please insert Delivered date");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["DeliverdDate"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["IsDeliverd"].Value.Equals(true) && decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["RestAmount"].Value.ToString()) > 0m && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء سداد كامل القيمة قبل التسليم" : "Please Pay The Total Amount Before Delivery");
				return false;
			}
		}
		return true;
	}

	public override void SaveData()
	{
		if (!ValidateData())
		{
			return;
		}
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["IsDeliverd"].Value.Equals(true))
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString() + ",";
			}
		}
		if (text != ",")
		{
			LabOrders.SetDeliverdToClient("1", text);
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد عمليات  لإغلاقها " : "There are No Operations to be Closed");
		}
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

	private void timer1_Tick(object sender, EventArgs e)
	{
		FillGrid();
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Approved.frmDeliveredLabOrders));
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
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.lblCode, "lblCode");
		this.timer1.Interval = 300000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		resources.ApplyResources(this, "$this");
		base.Name = "frmDeliveredLabOrders";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
