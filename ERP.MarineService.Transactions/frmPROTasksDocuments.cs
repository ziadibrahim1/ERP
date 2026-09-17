using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmPROTasksDocuments : frmBase
{
	private DataTable dtDetails;

	private DataTable dtDocumentsTypes;

	private ValueList vlDocumentsTypes = new ValueList();

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraGrid ULGData;

	public frmPROTasksDocuments()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmPROTasksDocuments(string TaskID)
		: this()
	{
		RowID = TaskID;
	}

	public override void PrepareData()
	{
		dtDocumentsTypes = DocumentsTypes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlDocumentsTypes.ValueListItems.Clear();
		for (int i = 0; i < dtDocumentsTypes.Rows.Count; i++)
		{
			vlDocumentsTypes.ValueListItems.Add(dtDocumentsTypes.Rows[i]["DocumentTypeID"], dtDocumentsTypes.Rows[i]["DocumentTypeName"].ToString());
		}
		dtDetails = OperationsServicesStepsTasksDocuments.SelectByOperationServiceStepTaskID(RowID, GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DocumentTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DocumentTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "المستند" : "Document");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DocumentTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DocumentTypeID"].ValueList = (IValueList)(object)vlDocumentsTypes;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOriginal"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOriginal"].Header).Caption = (GlobalVariables.IsArabic ? "اصل" : "Original");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsOriginal"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmPROTasksDocuments));
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
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.ULGData = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance15");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance15.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance1.FontData");
		resources.ApplyResources(val2, "appearance1");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val3).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance18");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance18.FontData");
		((SubObjectBase)val3).ForceApplyResources = "|FontData";
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		((SubObjectBase)val4).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val4;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val5, "appearance5");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		((SubObjectBase)val5).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val6, "appearance6");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		((SubObjectBase)val6).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val7, "appearance7");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance7.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance8.FontData");
		((SubObjectBase)val8).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val9).TextTrimming = (TextTrimming)3;
		resources.ApplyResources(val9, "appearance9");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance9.FontData");
		((SubObjectBase)val9).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance10.FontData");
		((SubObjectBase)val10).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance16");
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance16.FontData");
		((SubObjectBase)val11).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val12, "appearance12");
		resources.ApplyResources(((AppearanceBase)val12).FontData, "appearance12.FontData");
		((SubObjectBase)val12).ForceApplyResources = "|FontData";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmPROTasksDocuments";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
	}
}
