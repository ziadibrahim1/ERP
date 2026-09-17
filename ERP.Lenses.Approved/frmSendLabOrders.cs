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
using ERP.Lenses.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Approved;

public class frmSendLabOrders : frmPosted
{
	private DataTable dtLabOrdersDetails;

	private DataSet ds;

	private IContainer components = null;

	public frmSendLabOrders()
	{
		InitializeComponent();
		NoCol = "LabOrderNo";
	}

	public override void FillGrid()
	{
		dtsource = LabOrders.SelectByLabOrdersSent(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		dtLabOrdersDetails = LabOrdersDetails.SelectByLabOrdersSent(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtsource);
		ds.Tables.Add(dtLabOrdersDetails);
		ds.Tables[0].TableName = "dtsource";
		ds.Tables[1].TableName = "dtLabOrdersDetails";
		ds.Relations.Add(ds.Tables[0].Columns["LabOrderID"], ds.Tables[1].Columns["LabOrderID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SentDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SentDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabOrderDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LabName"].Header).Caption = (GlobalVariables.IsArabic ? "المعمل" : "Lab");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SentDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSent"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSent"].Header).Caption = (GlobalVariables.IsArabic ? "ارسال" : "Send");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSent"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSent"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSent"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsSent"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Stage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["SizeName"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorName"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromLabStore"].Header).Caption = (GlobalVariables.IsArabic ? "من المعمل" : "From Lab");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromBranchStore"].Header).Caption = (GlobalVariables.IsArabic ? "من المحل" : "From Store");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ItemName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["SizeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["ColorName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromLabStore"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["FromBranchStore"].Hidden = false;
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "IsSent" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "SentDate")
			{
				((GridItemBase)ULGData.ActiveCell).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1 && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "FromBranchStore" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "FromLabStore")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
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
			if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsSent"].Value.ToString()))
			{
				continue;
			}
			Main.StartBulkTrans(FromServer: false);
			try
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					LabOrdersDetails.SetIsFromLabOrFromStore(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["LabOrderDetailID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromLabStore"].Value.ToString()) ? "1" : "0", bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["FromBranchStore"].Value.ToString()) ? "1" : "0");
				}
				LabOrders.SetSentToLab(((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["IsSent"].Value.ToString()) ? "1" : "0", DateTime.Parse(((UltraGridBase)ULGData).Rows[i].Cells["SentDate"].Value.ToString()).ToString(GlobalVariables.DateLongFormate));
				string text = MessageLog.SelectByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString(), "LabMIV", "LabMIV", GlobalVariables.IsArabic ? "1" : "0");
				if (text != "")
				{
					GlobalVariables.InformationMB.Show(text);
					Main.RollbackBulkTrans(FromServer: false);
					MessageLog.DeleteByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString(), "LabMIV", "LabMIV");
				}
				else
				{
					ItemsTransactions.ManageInThread();
					LabOrders.GenerateSalesJvs("," + ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString() + ",", GlobalVariables.UserID);
					LabOrders.GenerateBranchPurchaseJvs("," + ((UltraGridBase)ULGData).Rows[i].Cells["LabOrderID"].Value.ToString() + ",", GlobalVariables.UserID);
					Main.EndBulkTrans(FromServer: false);
				}
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

	private void ULGData_CellChange(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		ULGData.CellChange -= new CellEventHandler(ULGData_CellChange);
		((UltraGridBase)ULGData).UpdateData();
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "FromLabStore")
		{
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
			if (e.Cell.Value.Equals(false))
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["FromLabStore"].Value = true;
			}
			else
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["FromLabStore"].Value = false;
			}
		}
		ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
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
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Approved.frmSendLabOrders));
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
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmSendLabOrders";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		base.ResumeLayout(false);
	}
}
