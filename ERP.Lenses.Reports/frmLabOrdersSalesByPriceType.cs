using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Lenses.Reports;

public class frmLabOrdersSalesByPriceType : frmReportTree2010
{
	private DataTable dtPriceTypes;

	private DataTable dtPriceRanges;

	private IContainer components = null;

	private UltraComboEditor cboPriceType;

	private UltraLabel lblPriceType;

	public UltraGrid ULGPriceRanges;

	private UltraLabel ultraLabel3;

	public frmLabOrdersSalesByPriceType()
	{
		dtItems = BusinessLayer.StockControl.Items.FillTree("-1", "-1", "-1", "1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		TreeItemsParentIDCol = "ParentID";
		TreeItemsIDCol = "ItemID";
		TreeItemsNameCol = "Name";
		TreeItemsIsMainCol = "IsMain";
		dtPriceRanges = new DataTable();
		dtPriceRanges.Clear();
		dtPriceRanges.Columns.Add("FromRange", typeof(decimal));
		dtPriceRanges.Columns.Add("ToRange", typeof(decimal));
		dtPriceRanges.Rows.Add(1, 10);
		dtPriceRanges.Rows.Add(11, 100);
		dtPriceRanges.Rows.Add(101, 1000);
		InitializeComponent();
	}

	public override void FormLoad()
	{
		((UltraGridBase)ULGPriceRanges).DataSource = dtPriceRanges;
		InitGrid(ULGPriceRanges);
		dtPriceTypes = PricesTypes.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboPriceType, dtPriceTypes, "PriceTypeID", "PriceName");
		if (dtItems != null)
		{
			TreeFunctions.FillTree(TreeItems, dtItems, TreeItemsParentIDCol, TreeItemsIDCol, TreeItemsNameCol, TreeItemsNumberCol, TreeItemsIsMainCol);
		}
		if (dtItems2 != null)
		{
			TreeFunctions.FillTreeOneLevel(TreeItems2, dtItems2, TreeItems2IDCol, TreeItems2NameCol);
		}
	}

	private void InitGrid(UltraGrid ULGData)
	{
		((UltraGridBase)ULGPriceRanges).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGPriceRanges).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromRange"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToRange"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromRange"].Header).VisiblePosition = (GlobalVariables.IsArabic ? 1 : 0);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromRange"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToRange"].Header).Caption = (GlobalVariables.IsArabic ? " الى" : "To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromRange"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToRange"].DefaultCellValue = 0;
	}

	public override string GetReportName()
	{
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			if (((UltraToggleEditorBase)chkWithLogo).Checked)
			{
				return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_LabOrdersItemsSalesDetails_A.rpt" : "Rep_Lns_LabOrdersItemsSalesDetails_E.rpt");
			}
			return GlobalVariables.ReportsPath + (((UltraToggleEditorBase)chkIsArabic).Checked ? "Rep_Lns_LabOrdersItemsSalesDetails_A_nologo.rpt" : "Rep_Lns_LabOrdersItemsSalesDetails_E_nologo.rpt");
		}
		return base.GetReportName();
	}

	public override void ShowReport()
	{
		GetItems();
		GetBranches();
		if (Items.Equals(","))
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار اى صنف  ", "There is no chosen Item to be shown in the report, please check items to be shown in report");
			return;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGPriceRanges).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("لم تقم باختيار نطاق للسعر  ", "There is no chosen Price Ranges shown in the report, please Add Ranges to be shown in report");
			return;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGPriceRanges).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGPriceRanges).Rows[i].Cells["FromRange"].Value == null)
			{
				GlobalVariables.InformationMB.Show("لم تقم بتحديد من في نطاق للسعر  ", "There is no chosen From Range shown in the report, please Add Ranges to be shown in report");
				ULGPriceRanges.ActiveCell = ((UltraGridBase)ULGPriceRanges).Rows[i].Cells["FromRange"];
				return;
			}
			if (((UltraGridBase)ULGPriceRanges).Rows[i].Cells["ToRange"].Value == null)
			{
				GlobalVariables.InformationMB.Show("لم تقم بتحديد الى في نطاق للسعر  ", "There is no chosen To Range shown in the report, please Add Ranges to be shown in report");
				ULGPriceRanges.ActiveCell = ((UltraGridBase)ULGPriceRanges).Rows[i].Cells["ToRange"];
				return;
			}
			if (decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["FromRange"].Value.ToString()) == 0m && decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["ToRange"].Value.ToString()) == 0m)
			{
				GlobalVariables.InformationMB.Show("هناك قيم غير مقبولة في نطاق الاسعار", "Rejected Values In Price Ranges");
				((GridItemBase)((UltraGridBase)ULGPriceRanges).Rows[i]).Selected = true;
				return;
			}
			for (int j = 0; j < i; j++)
			{
				if (j != i && ((decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["FromRange"].Value.ToString()) >= decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[j].Cells["FromRange"].Value.ToString()) && decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["FromRange"].Value.ToString()) <= decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[j].Cells["ToRange"].Value.ToString())) || (decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["ToRange"].Value.ToString()) >= decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[j].Cells["FromRange"].Value.ToString()) && decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["ToRange"].Value.ToString()) <= decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[j].Cells["ToRange"].Value.ToString())) || (decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["FromRange"].Value.ToString()) <= decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[j].Cells["FromRange"].Value.ToString()) && decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[i].Cells["ToRange"].Value.ToString()) >= decimal.Parse(((UltraGridBase)ULGPriceRanges).Rows[j].Cells["ToRange"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هناك تداخل في نطاق الاسعار", "Conflict In Price Ranges");
					((GridItemBase)((UltraGridBase)ULGPriceRanges).Rows[j]).Selected = true;
					return;
				}
			}
		}
		if (cboPriceType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال نوع السعر " : "Please  Price Type");
			((TextEditorControlBase)cboPriceType).Focus();
			return;
		}
		string val = Main.TableToXML((DataTable)((UltraGridBase)ULGPriceRanges).DataSource);
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@doc", val);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", Branches);
		GlobalVariables.ReportDocument.SetParameterValue("@BranchsNames", BranchesNames);
		GlobalVariables.ReportDocument.SetParameterValue("@ItemIDs", Items);
		GlobalVariables.ReportDocument.SetParameterValue("@PriceTypeID", ((TextEditorControlBase)cboPriceType).Value.ToString());
		GlobalVariables.ReportDocument.SetParameterValue("@FromDate", dtpFromDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@ToDate", dtpToDate.DateTime);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrentBranchID", GlobalVariables.CurrentBranchID.ToString());
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmReporViwer2.Tag = base.Tag;
		frmReporViwer2.MdiParent = base.MdiParent;
		frmReporViwer2.TopLevel = false;
		frmReporViwer2.Parent = base.Parent;
		frmReporViwer2.Width = base.Parent.Width;
		frmReporViwer2.Height = base.Parent.Height;
		frmReporViwer2.frmParent = this;
		frmReporViwer2.Show();
		frmReporViwer2.BringToFront();
	}

	public override void btnItemsSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.ItemsReport("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "0", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems.GetNodeByKey(dtSearchResult.Rows[i][TreeItemsIDCol].ToString()).CheckedState = CheckState.Checked;
		}
	}

	private void ULGPriceRanges_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGPriceRanges.ActiveCell != null && (((KeyedSubObjectBase)ULGPriceRanges.ActiveCell.Column).Key == "ToRange" || ((KeyedSubObjectBase)ULGPriceRanges.ActiveCell.Column).Key == "FromRange"))
		{
			GlobalFunctions.CheckForNumbers(ULGPriceRanges.ActiveCell, e);
		}
	}

	public override void btnItems2Search_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.POSClientsSearchReport("-1", "-1", IsFromServer: false);
		for (int i = 0; i < dtSearchResult.Rows.Count; i++)
		{
			TreeItems2.GetNodeByKey(dtSearchResult.Rows[i][TreeItems2IDCol].ToString()).CheckedState = CheckState.Checked;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Reports.frmLabOrdersSalesByPriceType));
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
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		this.cboPriceType = new UltraComboEditor();
		this.lblPriceType = new UltraLabel();
		this.ULGPriceRanges = new UltraGrid();
		this.ultraLabel3 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtReports).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboSetting).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPriceRanges).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnPreview, "btnPreview");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val).Image = resources.GetObject("appearance17.Image");
		resources.ApplyResources(val, "appearance17");
		((ControlBase)base.btnPreview).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.ultraLabel1, "ultraLabel1");
		((UltraControlBase)base.ultraLabel1).UseAppStyling = false;
		resources.ApplyResources(base.ultraLabel2, "ultraLabel2");
		((UltraControlBase)base.ultraLabel2).UseAppStyling = false;
		resources.ApplyResources(base.dtpFromDate, "dtpFromDate");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance18");
		base.dtpFromDate.Appearance = (AppearanceBase)(object)val2;
		base.dtpFromDate.DateTime = new System.DateTime(2019, 4, 14, 0, 0, 0, 0);
		base.dtpFromDate.MaskInput = "dd/mm/yyyy";
		base.dtpFromDate.Value = new System.DateTime(2019, 4, 14, 0, 0, 0, 0);
		resources.ApplyResources(base.dtpToDate, "dtpToDate");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance19");
		base.dtpToDate.Appearance = (AppearanceBase)(object)val3;
		base.dtpToDate.DateTime = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		base.dtpToDate.MaskInput = "dd/mm/yyyy";
		base.dtpToDate.Value = new System.DateTime(2011, 1, 10, 23, 59, 59, 0);
		resources.ApplyResources(base.chkAllBranches, "chkAllBranches");
		((UltraControlBase)base.chkAllBranches).UseAppStyling = false;
		resources.ApplyResources(base.clbBranches, "clbBranches");
		resources.ApplyResources(base.chkWithLogo, "chkWithLogo");
		((UltraControlBase)base.chkWithLogo).UseAppStyling = false;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.lblReportType, "lblReportType");
		((UltraControlBase)base.lblReportType).UseAppStyling = false;
		resources.ApplyResources(base.cboReportType, "cboReportType");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		resources.ApplyResources(val4, "appearance4");
		((TextEditorControlBase)base.cboReportType).Appearance = (AppearanceBase)(object)val4;
		base.cboReportType.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboReportType.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.chkAll, "chkAll");
		resources.ApplyResources(base.chkAll2, "chkAll2");
		resources.ApplyResources(base.TreeItems, "TreeItems");
		resources.ApplyResources(base.TreeItems2, "TreeItems2");
		resources.ApplyResources(base.btnItemsSearch, "btnItemsSearch");
		resources.ApplyResources(base.btnItems2Search, "btnItems2Search");
		resources.ApplyResources(base.chkIsArabic, "chkIsArabic");
		resources.ApplyResources(base.txtItems, "txtItems");
		((AppearanceBase)val5).FontData.BoldAsString = resources.GetString("resource.BoldAsString4");
		((AppearanceBase)val5).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString4");
		((AppearanceBase)val5).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val5).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString4");
		((AppearanceBase)val5).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString4");
		resources.ApplyResources(val5, "appearance5");
		((TextEditorControlBase)base.txtItems).Appearance = (AppearanceBase)(object)val5;
		resources.ApplyResources(base.txtItems2, "txtItems2");
		((AppearanceBase)val6).FontData.BoldAsString = resources.GetString("resource.BoldAsString5");
		((AppearanceBase)val6).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString5");
		((AppearanceBase)val6).FontData.Name = resources.GetString("resource.Name4");
		((AppearanceBase)val6).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString5");
		((AppearanceBase)val6).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString5");
		resources.ApplyResources(val6, "appearance6");
		((TextEditorControlBase)base.txtItems2).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnNew, "btnNew");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.cboSetting, "cboSetting");
		resources.ApplyResources(base.lblSettingName, "lblSettingName");
		resources.ApplyResources(base.btnSaveSetting, "btnSaveSetting");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.cboPriceType, "cboPriceType");
		((TextEditorControlBase)this.cboPriceType).AlwaysInEditMode = true;
		this.cboPriceType.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPriceType).Name = "cboPriceType";
		resources.ApplyResources(this.lblPriceType, "lblPriceType");
		this.lblPriceType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPriceType).Name = "lblPriceType";
		((ControlBase)this.lblPriceType).WrapText = false;
		resources.ApplyResources(this.ULGPriceRanges, "ULGPriceRanges");
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val7).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val7).BorderColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((SpecialBoxBase)((UltraGridBase)this.ULGPriceRanges).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val8;
		((SpecialBoxBase)((UltraGridBase)this.ULGPriceRanges).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val9).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val10;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val13, "appearance13");
		((AppearanceBase)val13).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val14).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val14).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val14).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val15).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val15).Image = resources.GetObject("appearance15.Image");
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGPriceRanges).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.ULGPriceRanges).Name = "ULGPriceRanges";
		((System.Windows.Forms.Control)(object)this.ULGPriceRanges).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGPriceRanges_KeyPress);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGPriceRanges);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPriceType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPriceType);
		base.Name = "frmLabOrdersSalesByPriceType";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraLabel2, 0);
		base.Controls.SetChildIndex(base.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.TreeItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblSettingName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPriceType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGPriceRanges, 0);
		((System.ComponentModel.ISupportInitialize)base.dtReports).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtFormSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkAll2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems).EndInit();
		((System.ComponentModel.ISupportInitialize)base.TreeItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboSetting).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPriceType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGPriceRanges).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
