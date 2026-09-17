using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Personal.MasterData;

public class frmAdministrativeStructure : frmTree2
{
	private DataTable dtReports;

	private DataTable dtStructurePositions;

	private DataTable dtpositions;

	private DataTable dtCostCenters;

	private ValueList vlPositions = new ValueList();

	private IContainer components = null;

	private UltraLabel lblType;

	public UltraGrid ULGLevels;

	private UltraComboEditor cboCostCenter;

	private UltraLabel ultraLabel1;

	public frmAdministrativeStructure()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		IDCol = "AdministrativeStructureID";
		NoCol = "AdministrativeStructureNumber";
		NameCol = "AdministrativeStructureNameAr";
		NameEnCol = "AdministrativeStructureNameEn";
		ParentIDCol = "ParentID";
		IsMainCol = "IsMain";
		ItemLevelCol = "LevelID";
		AdditionalCol1 = "CostCenterID";
		TableName = "HR_AdministrativeStructure";
		LevelsTable = "HR_AdministrativeStructure_Levels";
		LevelsCol = "LevelID";
		LevelsWidthCol = "Width";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpositions = Positions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		vlPositions.ValueListItems.Clear();
		for (int i = 0; i < dtpositions.Rows.Count; i++)
		{
			vlPositions.ValueListItems.Add(dtpositions.Rows[i]["PositionID"], dtpositions.Rows[i]["PositionName"].ToString());
		}
		dtStructurePositions = AdministrativeStructurePositions.SelectByAdministrativeStructureID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
		InitGridStockLevels();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		if (Adding)
		{
			((Control)(object)txtCode).Text = AdministrativeStructure.GetCode((SelectedNode == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode).Key, IsFromServer: true);
			((Control)(object)txtName).Select();
		}
		dtStructurePositions.Rows.Clear();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((UltraGridBase)ULGLevels).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (SelectedNode != null)
		{
			((TextEditorControlBase)cboCostCenter).Value = ((DataRow)((SubObjectBase)SelectedNode).Tag)[AdditionalCol1].ToString();
			dtStructurePositions = AdministrativeStructurePositions.SelectByAdministrativeStructureID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			InitGridStockLevels();
		}
	}

	private void InitGridStockLevels()
	{
		((UltraGridBase)ULGLevels).DataSource = dtStructurePositions;
		GlobalFunctions.PrepareGrid(ULGLevels);
		((UltraGridBase)ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["AdministrativeStructurePositionID"].DefaultCellValue = -1;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["BranchID"].DefaultCellValue = GlobalVariables.CurrentBranchID;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxCount"].DefaultCellValue = 0;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinCount"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["PositionID"].Header).Caption = (GlobalVariables.IsArabic ? "الوظيفه" : "Job");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["PositionID"].Hidden = false;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["PositionID"].ValueList = (IValueList)(object)vlPositions;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinCount"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الأدني" : "Min");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinCount"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxCount"].Header).Caption = (GlobalVariables.IsArabic ? "الحد الأقصي" : "Max");
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxCount"].Hidden = false;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["PositionID"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MinCount"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.3);
		((UltraGridBase)ULGLevels).DisplayLayout.Bands[0].Columns["MaxCount"].Width = (int)((double)((Control)(object)ULGLevels).Width * 0.3);
	}

	public override int TreeAddData()
	{
		((UltraGridBase)ULGLevels).UpdateData();
		int num = 0;
		Main.StartBulkTrans(FromServer: true);
		try
		{
			num = AdministrativeStructure.Insert_Update("-1", GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode == null) ? "Null" : ((DataRow)((SubObjectBase)SelectedNode).Tag)[0].ToString(), "0", (NodeLevel + 1).ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (dtStructurePositions.Rows.Count > 0)
			{
				for (int i = 0; i < dtStructurePositions.Rows.Count; i++)
				{
					dtStructurePositions.Rows[i]["AdministrativeStructureID"] = num;
				}
				AdministrativeStructurePositions.Insert_UpdateByTable(dtStructurePositions, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
		return num;
	}

	public override void TreeUpdateData()
	{
		((UltraGridBase)ULGLevels).UpdateData();
		Main.StartBulkTrans(FromServer: true);
		try
		{
			AdministrativeStructure.Insert_Update(((KeyedSubObjectBase)SelectedNode).Key, GetCode(), ((Control)(object)txtName).Text.Trim(), (((Control)(object)txtNameEn).Text.Trim() == "") ? "Null" : ((Control)(object)txtNameEn).Text.Trim(), (SelectedNode.Parent == null) ? "Null" : ((KeyedSubObjectBase)SelectedNode.Parent).Key, ((bool)((DataRow)((SubObjectBase)SelectedNode).Tag)[IsMainCol]) ? "1" : "0", NodeLevel.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), "0", GlobalVariables.CurrentBranchID.ToString(), GlobalVariables.UserID, IsFromServer: true);
			if (dtStructurePositions.Rows.Count > 0)
			{
				for (int i = 0; i < dtStructurePositions.Rows.Count; i++)
				{
					dtStructurePositions.Rows[i]["AdministrativeStructureID"] = ((KeyedSubObjectBase)SelectedNode).Key;
				}
				AdministrativeStructurePositions.Insert_UpdateByTable(dtStructurePositions, GlobalVariables.CurrentBranchID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
			ClearControls();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void TreeDeleteData()
	{
		AdministrativeStructurePositions.DeleteByAdministrativeStructureID(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
		AdministrativeStructure.Delete(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.UserID, IsFromServer: true);
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGLevels).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGLevels).Rows[i].Cells["PositionID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("من فضلك ادخل الوظيفه", "Please Enter a Position");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["PositionID"];
				return false;
			}
			if (((UltraGridBase)ULGLevels).Rows[i].Cells["MinCount"].Value == DBNull.Value || Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MinCount"].Value) <= 0m)
			{
				GlobalVariables.InformationMB.Show("الحد الأدني يجب ان يكون اكبر من الصفر ", "MinCount Should Be Greater Than Zero");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["MinCount"];
				return false;
			}
			if (((UltraGridBase)ULGLevels).Rows[i].Cells["MaxCount"].Value == DBNull.Value || Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MaxCount"].Value) <= 0m)
			{
				GlobalVariables.InformationMB.Show("الحد الأقصي يجب ان يكون اكبر من الصفر", "MaxCount Should Be Greater Than Zero");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["MaxCount"];
				return false;
			}
			if (Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MaxCount"].Value) < Convert.ToDecimal(((UltraGridBase)ULGLevels).Rows[i].Cells["MinCount"].Value))
			{
				GlobalVariables.InformationMB.Show("الحد الأقصي يجب ان يكون اكبر من الحد الأدني", "MaxCount Should Be Greater Than MinCount");
				ULGLevels.ActiveCell = ((UltraGridBase)ULGLevels).Rows[i].Cells["MaxCount"];
				return false;
			}
		}
		return base.ValidateData();
	}

	public override bool HasTransactionValidation()
	{
		string text = AdministrativeStructure.SelectRelations(((KeyedSubObjectBase)SelectedNode).Key, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true).Rows[0]["Relations"].ToString().Replace("-", "\n");
		if (text != "")
		{
			GlobalVariables.InformationMB.Show(text, text);
			return true;
		}
		return base.HasTransactionValidation();
	}

	public override void TreeSearch()
	{
		int num = SearchFunctions.AdministrativeStructure(IsFromServer: true);
		if (num != 0)
		{
			treeChart.CollapseAll();
			treeChart.ActiveNode = treeChart.GetNodeByKey(num.ToString());
			treeChart.GetNodeByKey(num.ToString()).Selected = true;
		}
	}

	public override void btnPrintClick()
	{
		string val = "";
		if (dtReports.Rows.Count > 0)
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + dtReports.Rows[0]["Rep" + (GlobalVariables.IsArabic ? "_A" : "_E")].ToString());
			val = dtReports.Rows[0]["isoCode"].ToString();
		}
		else
		{
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_HR_AdministrativeStructure_A.rpt" : "Rep_HR_AdministrativeStructure_E.rpt"));
		}
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		GlobalVariables.ReportDocument.SetParameterValue("@IsoCode", val);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
		frmReporViwer2.ShowDialog();
		frmReporViwer2 = null;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Personal.MasterData.frmAdministrativeStructure));
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
		this.lblType = new UltraLabel();
		this.ULGLevels = new UltraGrid();
		this.cboCostCenter = new UltraComboEditor();
		this.ultraLabel1 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.treeChart, "treeChart");
		resources.ApplyResources(base.txtCode, "txtCode");
		((EditorButtonControlBase)base.txtCode).ReadOnly = true;
		resources.ApplyResources(base.txtName, "txtName");
		((EditorButtonControlBase)base.txtName).ReadOnly = true;
		resources.ApplyResources(base.lblPath, "lblPath");
		resources.ApplyResources(base.txtNameEn, "txtNameEn");
		((EditorButtonControlBase)base.txtNameEn).ReadOnly = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnAddRoot, "btnAddRoot");
		resources.ApplyResources(base.label1, "label1");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance17");
		((ControlBase)base.label1).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.label2, "label2");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val2, "appearance18");
		((ControlBase)base.label2).Appearance = (AppearanceBase)(object)val2;
		resources.ApplyResources(base.label3, "label3");
		((AppearanceBase)val3).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val3).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val3).FontData.Name = resources.GetString("resource.Name2");
		((AppearanceBase)val3).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints2");
		((AppearanceBase)val3).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val3).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		resources.ApplyResources(val3, "appearance19");
		((ControlBase)base.label3).Appearance = (AppearanceBase)(object)val3;
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val4).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val4).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val4).FontData.Name = resources.GetString("resource.Name3");
		((AppearanceBase)val4).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val4).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		resources.ApplyResources(val4, "appearance20");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val4;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblType, "lblType");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val5, "appearance21");
		((ControlBase)this.lblType).Appearance = (AppearanceBase)(object)val5;
		this.lblType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblType).Name = "lblType";
		((ControlBase)this.lblType).WrapText = false;
		resources.ApplyResources(this.ULGLevels, "ULGLevels");
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance6");
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val7;
		((SpecialBoxBase)((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGLevels).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGLevels).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val12, "appearance12");
		((AppearanceBase)val12).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val13).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val13).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val13).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val14).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val14, "appearance14");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGLevels).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.ULGLevels).Name = "ULGLevels";
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance22");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val16;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGLevels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblType);
		base.Name = "frmAdministrativeStructure";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblPath, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.treeChart, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.label3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAddRoot, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGLevels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		((System.ComponentModel.ISupportInitialize)base.dtChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.treeChart).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGLevels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
