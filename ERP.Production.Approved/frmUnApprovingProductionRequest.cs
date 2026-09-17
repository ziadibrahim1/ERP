using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Production;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Production.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Approved;

public class frmUnApprovingProductionRequest : frmPosted
{
	private IContainer components = null;

	public frmUnApprovingProductionRequest()
	{
		InitializeComponent();
		NoCol = "ProductionRequestNo";
		((Control)(object)btnPost).Text = (GlobalVariables.IsArabic ? "فك إعتماد" : "Unapprove");
	}

	public override void FillGrid()
	{
		dtsource = ProductionRequests.SelectByUnApproved(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم " : "NO");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestDeliveryDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestDeliveryDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ التسليم" : "Delivery Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionRequestDeliveryDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivered"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivered"].Header).Caption = (GlobalVariables.IsArabic ? "مستلم" : "Is Delivered");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivered"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "البيان" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
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
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ProductionRequestID"].Value.ToString() + ",";
			}
		}
		if (text != ",")
		{
			ProductionRequests.SetApprove(text, "0");
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد طلبيات لفك لإعتمادها " : "There are No Request to be UnApprove");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmProductionRequests frmProductionRequests2 = new frmProductionRequests(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionRequestID"].Value.ToString()));
			frmProductionRequests2.Size = new Size(base.Width, base.Height);
			frmProductionRequests2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmProductionRequests2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلبيات الانتاج" : "Production Request");
			frmProductionRequests2.ShowDialog();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Approved.frmUnApprovingProductionRequest));
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
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmUnApprovingProductionRequest";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
