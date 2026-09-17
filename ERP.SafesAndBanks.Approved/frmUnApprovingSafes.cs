using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.SafesAndBanks;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SafesAndBanks.SafeTransactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SafesAndBanks.Approved;

public class frmUnApprovingSafes : frmPosted
{
	private IContainer components = null;

	private UltraPanel pnlCheckType;

	private RadioButton rbSafeIn;

	private RadioButton rbSafeOut;

	public frmUnApprovingSafes()
	{
		InitializeComponent();
		NoCol = "VoucherNo";
		((Control)(object)btnPost).Text = (GlobalVariables.IsArabic ? "فك إعتماد" : "Unapprove");
		rbSafeIn.Checked = true;
	}

	public override void FillGrid()
	{
		if (rbSafeIn.Checked)
		{
			dtsource = SafeIn.SelectByApproved(GlobalVariables.BranchIDs, GlobalVariables.SafeIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		else
		{
			dtsource = SafeOut.SelectByApproved(GlobalVariables.BranchIDs, GlobalVariables.SafeIDs, "1", GlobalVariables.IsArabic ? "1" : "0");
		}
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.02);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["VoucherDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SafeName"].Header).Caption = (GlobalVariables.IsArabic ? "خزينة" : "Safe");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CurrencyCode"].Header).Caption = (GlobalVariables.IsArabic ? "العملة" : "Currency");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Total"].Header).Caption = (GlobalVariables.IsArabic ? "ألإجمالى" : "Total");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ChargedPerson"].Header).Caption = (GlobalVariables.IsArabic ? "السيد" : "Mr");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = "";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
	}

	public override void SelectFullRow()
	{
		((UltraGridBase)ULGData).UpdateData();
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
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["VoucherID"].Value.ToString() + ",";
			}
		}
		if (text != ",")
		{
			if (rbSafeIn.Checked)
			{
				SafeIn.SetApprove("0", text);
			}
			else
			{
				SafeOut.SetApprove("0", text);
			}
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد قيود لفك إعتمادها " : "There are No JV's to UnApprove");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			if (rbSafeIn.Checked)
			{
				frmSafeIn frmSafeIn2 = new frmSafeIn(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmSafeIn2.Size = new Size(base.Width, base.Height);
				frmSafeIn2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmSafeIn2.lblTitle).Text = (GlobalVariables.IsArabic ? "وارد خزينة" : "Safe In");
				frmSafeIn2.ShowDialog();
			}
			else
			{
				frmSafeOut frmSafeOut2 = new frmSafeOut(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["VoucherID"].Value.ToString()));
				frmSafeOut2.Size = new Size(base.Width, base.Height);
				frmSafeOut2.StartPosition = FormStartPosition.CenterParent;
				((Control)(object)frmSafeOut2.lblTitle).Text = (GlobalVariables.IsArabic ? "صادر خزينة" : "Safe Out");
				frmSafeOut2.ShowDialog();
			}
		}
	}

	public override void Search()
	{
		if (rbSafeIn.Checked)
		{
			DataTable dataTable = SearchFunctions.SafeInReport(1, 0);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
				{
					if (dataTable.Rows[i]["SafeInID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["VoucherID"].Value.ToString())
					{
						((UltraGridBase)ULGData).Rows[j].Cells["Approved"].Value = true;
					}
				}
			}
			return;
		}
		DataTable dataTable2 = SearchFunctions.SafeOutReport(1, 0);
		for (int k = 0; k < dataTable2.Rows.Count; k++)
		{
			for (int l = 0; l < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; l++)
			{
				if (dataTable2.Rows[k]["SafeOutID"].ToString() == ((UltraGridBase)ULGData).Rows[l].Cells["VoucherID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[l].Cells["Approved"].Value = true;
				}
			}
		}
	}

	private void rb_CheckedChanged(object sender, EventArgs e)
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
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.Approved.frmUnApprovingSafes));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		this.pnlCheckType = new UltraPanel();
		this.rbSafeIn = new System.Windows.Forms.RadioButton();
		this.rbSafeOut = new System.Windows.Forms.RadioButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance1");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val2, "appearance2");
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val3, "appearance3");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val4, "appearance4");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val5, "appearance5");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(base.btnPost, "btnPost");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.txtCode, "txtCode");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val7, "appearance9");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance9.FontData");
		((SubObjectBase)val7).ForceApplyResources = "FontData|";
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbSafeIn);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbSafeOut);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbSafeIn, "rbSafeIn");
		this.rbSafeIn.BackColor = System.Drawing.Color.Transparent;
		this.rbSafeIn.Name = "rbSafeIn";
		this.rbSafeIn.TabStop = true;
		this.rbSafeIn.UseVisualStyleBackColor = false;
		this.rbSafeIn.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this.rbSafeOut, "rbSafeOut");
		this.rbSafeOut.BackColor = System.Drawing.Color.Transparent;
		this.rbSafeOut.Name = "rbSafeOut";
		this.rbSafeOut.TabStop = true;
		this.rbSafeOut.UseVisualStyleBackColor = false;
		this.rbSafeOut.CheckedChanged += new System.EventHandler(rb_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Name = "frmUnApprovingSafes";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBByName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
