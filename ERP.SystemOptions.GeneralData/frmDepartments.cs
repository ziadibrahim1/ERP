using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmDepartments : frmGrid
{
	private DataTable dtAccounts;

	private DataTable dtCostCenters;

	private ValueList vlAccounts = new ValueList();

	private ValueList vlCostCenters = new ValueList();

	private bool UseCostCenters;

	private IContainer components = null;

	private UltraComboEditor cboCostCenter;

	private UltraLabel lblCostCenter;

	private UltraLabel lblDepartmentEnglishName;

	private UltraTextEditor txtDepartmentEnglishName;

	private UltraComboEditor cboDepartmentAccount;

	private UltraLabel lblDepartmentAccount;

	private UltraLabel lblDepartmentArabicName;

	private UltraTextEditor txtDepartmentArabicName;

	public UltraButton btnDepartmentAccountSearch;

	public UltraButton btnCostCenterSearch;

	private UltraLabel lblDepartmentCode;

	private UltraTextEditor txtDepartmentCode;

	public frmDepartments()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "G_Departments";
		IDCol = "DepartmentID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDepartmentAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
			vlCostCenters.ValueListItems.Clear();
			for (int j = 0; j < dtCostCenters.Rows.Count; j++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[j]["CostCenterID"], dtCostCenters.Rows[j]["Name"].ToString());
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = NavMode;
		((EditorButtonControlBase)txtDepartmentCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepartmentArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDepartmentEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)cboDepartmentAccount).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCostCenter).ReadOnly = NavMode;
		((Control)(object)btnDepartmentAccountSearch).Visible = !NavMode;
		((Control)(object)lblCostCenter).Visible = UseCostCenters;
		((Control)(object)cboCostCenter).Visible = UseCostCenters;
		((Control)(object)btnCostCenterSearch).Visible = !NavMode && UseCostCenters;
		((TextEditorControlBase)txtDepartmentCode).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtDepartmentCode).Text = (Adding ? Departments.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtDepartmentArabicName).Clear();
		((TextEditorControlBase)txtDepartmentEnglishName).Clear();
		cboDepartmentAccount.SelectedIndex = -1;
		cboCostCenter.SelectedIndex = -1;
	}

	public override void FillData()
	{
		dataTable = Departments.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود القسم" : "Department Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DepartmentCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Header).Caption = (GlobalVariables.IsArabic ? "حساب القسم" : "Department Account");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AccountID"].ValueList = (IValueList)(object)vlAccounts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Header).Caption = (GlobalVariables.IsArabic ? "مركز التكلفة" : "Cost Center");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CostCenterID"].ValueList = (IValueList)(object)vlCostCenters;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtDepartmentCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["DepartmentCode"].Value.ToString();
		((Control)(object)txtDepartmentArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["DepartmentNameAr"].Value.ToString();
		((Control)(object)txtDepartmentEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["DepartmentNameEn"].Value.ToString();
		((TextEditorControlBase)cboDepartmentAccount).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["AccountID"].Value;
		((TextEditorControlBase)cboCostCenter).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["CostCenterID"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtDepartmentCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود القسم", "Please Insert Department Code");
			((TextEditorControlBase)txtDepartmentCode).Focus();
			return false;
		}
		if (((Control)(object)txtDepartmentArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم القسم بالعربية", "Please Insert Department Arabic Name");
			((TextEditorControlBase)txtDepartmentArabicName).Focus();
			return false;
		}
		if (cboDepartmentAccount.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار إسم حساب القسم", "Please Select Department Account Name");
			((TextEditorControlBase)cboDepartmentAccount).Focus();
			return false;
		}
		if ((Adding && dataTable.Select("DepartmentNameAr='" + ((Control)(object)txtDepartmentArabicName).Text + "'").Length != 0) || (Updating && dataTable.Select("DepartmentNameAr='" + ((Control)(object)txtDepartmentArabicName).Text + "'").Length > 1))
		{
			GlobalVariables.InformationMB.Show("يوجد قسم بنفس الاسم", "A Department with the same name Already Exists ");
			((TextEditorControlBase)txtDepartmentArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Departments.Insert_Update("-1", ((Control)(object)txtDepartmentCode).Text, ((Control)(object)txtDepartmentArabicName).Text, (((Control)(object)txtDepartmentEnglishName).Text == "") ? "Null" : ((Control)(object)txtDepartmentEnglishName).Text, ((TextEditorControlBase)cboDepartmentAccount).Value.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Departments.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["DepartmentID"].Value.ToString(), ((Control)(object)txtDepartmentCode).Text, ((Control)(object)txtDepartmentArabicName).Text, (((Control)(object)txtDepartmentEnglishName).Text == "") ? "Null" : ((Control)(object)txtDepartmentEnglishName).Text, ((TextEditorControlBase)cboDepartmentAccount).Value.ToString(), (cboCostCenter.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCostCenter).Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Departments.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["DepartmentID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	public override void btnRefreshDataClick()
	{
		UseCostCenters = int.Parse(Main.ExecuteQuery_DataTable("Select Count(*) as SA from A_CostCenters Where IsMain = 0").Rows[0][0].ToString()) > 0;
		dtAccounts = Accounts.FillCombo(GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboDepartmentAccount, dtAccounts, "AccountID", "Name");
		vlAccounts.ValueListItems.Clear();
		for (int i = 0; i < dtAccounts.Rows.Count; i++)
		{
			vlAccounts.ValueListItems.Add(dtAccounts.Rows[i]["AccountID"], dtAccounts.Rows[i]["Name"].ToString());
		}
		if (UseCostCenters)
		{
			dtCostCenters = CostCenters.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			GlobalFunctions.FillCombo(cboCostCenter, dtCostCenters, "CostCenterID", "Name");
			vlCostCenters.ValueListItems.Clear();
			for (int j = 0; j < dtCostCenters.Rows.Count; j++)
			{
				vlCostCenters.ValueListItems.Add(dtCostCenters.Rows[j]["CostCenterID"], dtCostCenters.Rows[j]["Name"].ToString());
			}
		}
	}

	private void btnDepartmentAccountSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.Accounts(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboDepartmentAccount).Value = num;
		}
	}

	private void btnCostCenterSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.CostCenter(IsFromServer: true);
		if (num != 0)
		{
			((TextEditorControlBase)cboCostCenter).Value = num;
		}
	}

	private void cboDepartmentAccount_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.Accounts(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboDepartmentAccount).Value = num;
			}
		}
	}

	private void cboCostCenter_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.CostCenter(IsFromServer: true);
			if (num != 0)
			{
				((TextEditorControlBase)cboCostCenter).Value = num;
			}
		}
	}

	public override void Search()
	{
		int num = SearchFunctions.Departments(IsFromServer: false);
		if (num != 0)
		{
			if (((UltraGridBase)ULGData).ActiveRow != null)
			{
				((GridItemBase)((UltraGridBase)ULGData).Rows[((UltraGridBase)ULGData).ActiveRow.Index]).Selected = false;
			}
			int num2 = dataTable.Rows.IndexOf(dataTable.Select("DepartmentID=" + num)[0]);
			((UltraGridBase)ULGData).Rows[num2].Activate();
			((GridItemBase)((UltraGridBase)ULGData).Rows[num2]).Selected = true;
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
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmDepartments));
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
		this.cboCostCenter = new UltraComboEditor();
		this.lblCostCenter = new UltraLabel();
		this.lblDepartmentEnglishName = new UltraLabel();
		this.txtDepartmentEnglishName = new UltraTextEditor();
		this.cboDepartmentAccount = new UltraComboEditor();
		this.lblDepartmentAccount = new UltraLabel();
		this.lblDepartmentArabicName = new UltraLabel();
		this.txtDepartmentArabicName = new UltraTextEditor();
		this.btnDepartmentAccountSearch = new UltraButton();
		this.btnCostCenterSearch = new UltraButton();
		this.lblDepartmentCode = new UltraLabel();
		this.txtDepartmentCode = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartmentEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartmentAccount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartmentArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartmentCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.cboCostCenter, "cboCostCenter");
		this.cboCostCenter.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboCostCenter).Name = "cboCostCenter";
		((System.Windows.Forms.Control)(object)this.cboCostCenter).KeyDown += new System.Windows.Forms.KeyEventHandler(cboCostCenter_KeyDown);
		resources.ApplyResources(this.lblCostCenter, "lblCostCenter");
		this.lblCostCenter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCostCenter).Name = "lblCostCenter";
		((ControlBase)this.lblCostCenter).WrapText = false;
		resources.ApplyResources(this.lblDepartmentEnglishName, "lblDepartmentEnglishName");
		this.lblDepartmentEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartmentEnglishName).Name = "lblDepartmentEnglishName";
		((ControlBase)this.lblDepartmentEnglishName).WrapText = false;
		resources.ApplyResources(this.txtDepartmentEnglishName, "txtDepartmentEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtDepartmentEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtDepartmentEnglishName).Name = "txtDepartmentEnglishName";
		resources.ApplyResources(this.cboDepartmentAccount, "cboDepartmentAccount");
		this.cboDepartmentAccount.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboDepartmentAccount).Name = "cboDepartmentAccount";
		((System.Windows.Forms.Control)(object)this.cboDepartmentAccount).KeyDown += new System.Windows.Forms.KeyEventHandler(cboDepartmentAccount_KeyDown);
		resources.ApplyResources(this.lblDepartmentAccount, "lblDepartmentAccount");
		this.lblDepartmentAccount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartmentAccount).Name = "lblDepartmentAccount";
		((ControlBase)this.lblDepartmentAccount).WrapText = false;
		resources.ApplyResources(this.lblDepartmentArabicName, "lblDepartmentArabicName");
		this.lblDepartmentArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartmentArabicName).Name = "lblDepartmentArabicName";
		((ControlBase)this.lblDepartmentArabicName).WrapText = false;
		resources.ApplyResources(this.txtDepartmentArabicName, "txtDepartmentArabicName");
		((System.Windows.Forms.Control)(object)this.txtDepartmentArabicName).Name = "txtDepartmentArabicName";
		resources.ApplyResources(this.btnDepartmentAccountSearch, "btnDepartmentAccountSearch");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnDepartmentAccountSearch).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnDepartmentAccountSearch).Name = "btnDepartmentAccountSearch";
		((System.Windows.Forms.Control)(object)this.btnDepartmentAccountSearch).Click += new System.EventHandler(btnDepartmentAccountSearch_Click);
		resources.ApplyResources(this.btnCostCenterSearch, "btnCostCenterSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnCostCenterSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Name = "btnCostCenterSearch";
		((System.Windows.Forms.Control)(object)this.btnCostCenterSearch).Click += new System.EventHandler(btnCostCenterSearch_Click);
		resources.ApplyResources(this.lblDepartmentCode, "lblDepartmentCode");
		this.lblDepartmentCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDepartmentCode).Name = "lblDepartmentCode";
		((ControlBase)this.lblDepartmentCode).WrapText = false;
		resources.ApplyResources(this.txtDepartmentCode, "txtDepartmentCode");
		((System.Windows.Forms.Control)(object)this.txtDepartmentCode).Name = "txtDepartmentCode";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartmentCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDepartmentCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCostCenterSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDepartmentAccountSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCostCenter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartmentEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDepartmentEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboDepartmentAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartmentAccount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDepartmentArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDepartmentArabicName);
		base.Name = "frmDepartments";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDepartmentArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepartmentArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepartmentAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboDepartmentAccount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDepartmentEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepartmentEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCostCenter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDepartmentAccountSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCostCenterSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDepartmentCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDepartmentCode, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCostCenter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartmentEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDepartmentAccount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartmentArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDepartmentCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
