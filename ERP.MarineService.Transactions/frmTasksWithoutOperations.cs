using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmTasksWithoutOperations : frmHeaderDetails
{
	private DataTable dtReports;

	private DataTable dtTasksPlaces;

	private DataTable dtUsers;

	private DataTable dtTasks;

	private IContainer components = null;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	public UltraButton btnTasksSearch;

	private UltraComboEditor cboTasks;

	private UltraLabel lblTask;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	public UltraButton ultraButton1;

	private UltraComboEditor cboTaskPlaces;

	private UltraLabel lblTaskPlace;

	public UltraButton ultraButton2;

	private UltraComboEditor cboUsers;

	private UltraLabel lblUser;

	private UltraCheckEditor chkForAllUsers;

	private UltraCheckEditor chkHold;

	private UltraCheckEditor chkCancelled;

	private UltraTextEditor txtExpectedTime;

	private UltraLabel lblExpectedTime;

	private UltraLabel lblTaskOrder;

	private UltraTextEditor txtTaskOrder;

	public frmTasksWithoutOperations()
	{
		InitializeComponent();
		TableName = "MS_TaskWithoutOperation";
		IDCol = "TaskWithoutOperationID";
		NoCol = "TaskWithoutOperationNo";
		DateCol = "StartDate";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtTasksPlaces = TasksPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTaskPlaces, dtTasksPlaces, "TaskPlaceID", "TaskPlaceName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "User_ID", "UserName");
		dtTasks = Tasks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTasks, dtTasks, "TaskID", "TaskName");
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = TaskWithoutOperation.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["TaskWithoutOperationNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["StartDate"];
			((TextEditorControlBase)cboTasks).Value = drMaster["TaskID"];
			((TextEditorControlBase)cboTaskPlaces).Value = drMaster["TaskPlaceID"];
			((TextEditorControlBase)cboUsers).Value = drMaster["User_ID"];
			((Control)(object)txtTaskOrder).Text = drMaster["TaskOrder"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((UltraToggleEditorBase)chkCancelled).Checked = bool.Parse(drMaster["IsCancelled"].ToString());
			((UltraToggleEditorBase)chkHold).Checked = bool.Parse(drMaster["IsHold"].ToString());
			((UltraToggleEditorBase)chkForAllUsers).Checked = bool.Parse(drMaster["ForAllUsers"].ToString());
			if (Adding || Updating)
			{
				((Control)(object)cboUsers).Enabled = !((UltraToggleEditorBase)chkForAllUsers).Checked;
			}
			if (drMaster["IsCompleted"].Equals(true))
			{
				((Control)(object)btnDelete).Enabled = false;
				((Control)(object)btnUpdate).Enabled = false;
			}
			else
			{
				((Control)(object)btnDelete).Enabled = true;
				((Control)(object)btnUpdate).Enabled = true;
			}
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtCode).ReadOnly = Adding;
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTasks).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTaskPlaces).ReadOnly = NavMode;
		((EditorButtonControlBase)cboUsers).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExpectedTime).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTaskOrder).ReadOnly = NavMode;
		((Control)(object)chkForAllUsers).Enabled = !NavMode;
		((Control)(object)chkHold).Enabled = !NavMode;
		((Control)(object)chkCancelled).Enabled = !NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((Control)(object)btnTasksSearch).Visible = !NavMode;
		((Control)(object)chkHold).Visible = !Adding;
		((Control)(object)chkCancelled).Visible = !Adding;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow();
		((Control)(object)txtCode).Text = (Adding ? TaskWithoutOperation.GetCodeByBranchID(GlobalVariables.CurrentBranchID) : "");
		cboTasks.SelectedIndex = -1;
		cboTaskPlaces.SelectedIndex = -1;
		cboUsers.SelectedIndex = -1;
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtTaskOrder).Clear();
		((TextEditorControlBase)txtExpectedTime).Clear();
		((UltraToggleEditorBase)chkCancelled).Checked = false;
		((UltraToggleEditorBase)chkForAllUsers).Checked = false;
		((UltraToggleEditorBase)chkHold).Checked = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم  المهمه", "Please Enter The task Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = TaskWithoutOperation.Insert_Update("-1", ((Control)(object)txtCode).Text, (cboTasks.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTasks).Value.ToString(), (((Control)(object)txtTaskOrder).Text == "") ? "0" : ((Control)(object)txtTaskOrder).Text, (cboTaskPlaces.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTaskPlaces).Value.ToString(), "0", ((UltraToggleEditorBase)chkForAllUsers).Checked ? "1" : "0", (cboUsers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtTaskOrder).Text == "") ? "0" : ((Control)(object)txtTaskOrder).Text, "Null", "0", "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = TaskWithoutOperation.Insert_Update(drMaster["TaskWithoutOperationID"].ToString(), ((Control)(object)txtCode).Text, (cboTasks.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTasks).Value.ToString(), (((Control)(object)txtTaskOrder).Text == "") ? "0" : ((Control)(object)txtTaskOrder).Text, (cboTaskPlaces.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTaskPlaces).Value.ToString(), ((UltraToggleEditorBase)chkHold).Checked ? "1" : "0", ((UltraToggleEditorBase)chkForAllUsers).Checked ? "1" : "0", (cboUsers.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboUsers).Value.ToString(), dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtTaskOrder).Text == "") ? "0" : ((Control)(object)txtTaskOrder).Text, "Null", "0", ((UltraToggleEditorBase)chkCancelled).Checked ? "1" : "0", ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			TaskWithoutOperation.DeleteVirtual(drMaster["TaskWithoutOperationID"].ToString(), GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnRefreshDataClick()
	{
		dtTasksPlaces = TasksPlaces.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTaskPlaces, dtTasksPlaces, "TaskPlaceID", "TaskPlaceName");
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "User_ID", "UserName");
		dtTasks = Tasks.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboTasks, dtTasks, "TaskID", "TaskName");
	}

	public override void btnPrintClick()
	{
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.TaskWithoutOperationReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["TaskWithoutOperationID"].ToString();
			FillData();
		}
	}

	private void cboTasks_KeyDown(object sender, KeyEventArgs e)
	{
		if ((Adding || Updating) && e.KeyValue == 119)
		{
			int num = SearchFunctions.TasksSearch(IsFromServer: false);
			if (num != 0)
			{
				((TextEditorControlBase)cboTasks).Value = num;
			}
		}
	}

	private void btnAccountSearch_Click(object sender, EventArgs e)
	{
		((TextEditorControlBase)cboTasks).Value = SearchFunctions.TasksSearch(IsFromServer: false);
	}

	private void textBox_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		((Control)(UltraTextEditor)sender).Select();
	}

	private void chkForAllUsers_CheckedChanged(object sender, EventArgs e)
	{
		((Control)(object)cboUsers).Enabled = !((UltraToggleEditorBase)chkForAllUsers).Checked;
		if (((UltraToggleEditorBase)chkForAllUsers).Checked)
		{
			cboUsers.SelectedIndex = -1;
		}
	}

	private void txtExchangeRate_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtTaskOrder_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void chkHold_CheckedChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraToggleEditorBase)chkHold).Checked && ((UltraToggleEditorBase)chkCancelled).Checked)
		{
			((UltraToggleEditorBase)chkCancelled).Checked = false;
		}
	}

	private void chkCancelled_CheckedChanged(object sender, EventArgs e)
	{
		if ((Adding || Updating) && ((UltraToggleEditorBase)chkHold).Checked && ((UltraToggleEditorBase)chkCancelled).Checked)
		{
			((UltraToggleEditorBase)chkHold).Checked = false;
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmTasksWithoutOperations));
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
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.btnTasksSearch = new UltraButton();
		this.cboTasks = new UltraComboEditor();
		this.lblTask = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.ultraButton1 = new UltraButton();
		this.cboTaskPlaces = new UltraComboEditor();
		this.lblTaskPlace = new UltraLabel();
		this.ultraButton2 = new UltraButton();
		this.cboUsers = new UltraComboEditor();
		this.lblUser = new UltraLabel();
		this.chkForAllUsers = new UltraCheckEditor();
		this.chkHold = new UltraCheckEditor();
		this.chkCancelled = new UltraCheckEditor();
		this.txtExpectedTime = new UltraTextEditor();
		this.lblExpectedTime = new UltraLabel();
		this.lblTaskOrder = new UltraLabel();
		this.txtTaskOrder = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTasks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTaskPlaces).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkHold).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCancelled).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpectedTime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaskOrder).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		resources.ApplyResources(base.ULGData, "ULGData");
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
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
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
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		resources.ApplyResources(this.btnTasksSearch, "btnTasksSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance12");
		((ControlBase)this.btnTasksSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnTasksSearch).Name = "btnTasksSearch";
		((System.Windows.Forms.Control)(object)this.btnTasksSearch).Click += new System.EventHandler(btnAccountSearch_Click);
		resources.ApplyResources(this.cboTasks, "cboTasks");
		this.cboTasks.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTasks).Name = "cboTasks";
		((System.Windows.Forms.Control)(object)this.cboTasks).KeyDown += new System.Windows.Forms.KeyEventHandler(cboTasks_KeyDown);
		resources.ApplyResources(this.lblTask, "lblTask");
		this.lblTask.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTask).Name = "lblTask";
		((ControlBase)this.lblTask).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.ultraButton1, "ultraButton1");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val10, "appearance13");
		((ControlBase)this.ultraButton1).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.ultraButton1).Name = "ultraButton1";
		resources.ApplyResources(this.cboTaskPlaces, "cboTaskPlaces");
		this.cboTaskPlaces.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboTaskPlaces).Name = "cboTaskPlaces";
		resources.ApplyResources(this.lblTaskPlace, "lblTaskPlace");
		this.lblTaskPlace.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaskPlace).Name = "lblTaskPlace";
		((ControlBase)this.lblTaskPlace).WrapText = false;
		resources.ApplyResources(this.ultraButton2, "ultraButton2");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance14");
		((ControlBase)this.ultraButton2).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.ultraButton2).Name = "ultraButton2";
		resources.ApplyResources(this.cboUsers, "cboUsers");
		this.cboUsers.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboUsers).Name = "cboUsers";
		resources.ApplyResources(this.lblUser, "lblUser");
		this.lblUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUser).Name = "lblUser";
		((ControlBase)this.lblUser).WrapText = false;
		resources.ApplyResources(this.chkForAllUsers, "chkForAllUsers");
		((System.Windows.Forms.Control)(object)this.chkForAllUsers).Name = "chkForAllUsers";
		((UltraToggleEditorBase)this.chkForAllUsers).CheckedChanged += new System.EventHandler(chkForAllUsers_CheckedChanged);
		resources.ApplyResources(this.chkHold, "chkHold");
		((System.Windows.Forms.Control)(object)this.chkHold).Name = "chkHold";
		((UltraToggleEditorBase)this.chkHold).CheckedChanged += new System.EventHandler(chkHold_CheckedChanged);
		resources.ApplyResources(this.chkCancelled, "chkCancelled");
		((System.Windows.Forms.Control)(object)this.chkCancelled).Name = "chkCancelled";
		((UltraToggleEditorBase)this.chkCancelled).CheckedChanged += new System.EventHandler(chkCancelled_CheckedChanged);
		resources.ApplyResources(this.txtExpectedTime, "txtExpectedTime");
		((System.Windows.Forms.Control)(object)this.txtExpectedTime).Name = "txtExpectedTime";
		((System.Windows.Forms.Control)(object)this.txtExpectedTime).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtExchangeRate_KeyPress);
		resources.ApplyResources(this.lblExpectedTime, "lblExpectedTime");
		this.lblExpectedTime.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExpectedTime).Name = "lblExpectedTime";
		((ControlBase)this.lblExpectedTime).WrapText = false;
		resources.ApplyResources(this.lblTaskOrder, "lblTaskOrder");
		this.lblTaskOrder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTaskOrder).Name = "lblTaskOrder";
		((ControlBase)this.lblTaskOrder).WrapText = false;
		resources.ApplyResources(this.txtTaskOrder, "txtTaskOrder");
		((System.Windows.Forms.Control)(object)this.txtTaskOrder).Name = "txtTaskOrder";
		((System.Windows.Forms.Control)(object)this.txtTaskOrder).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTaskOrder_KeyPress);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTaskOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaskOrder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExpectedTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExpectedTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCancelled);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkHold);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkForAllUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraButton1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTaskPlaces);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTaskPlace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnTasksSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTasks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTask);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Name = "frmTasksWithoutOperations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTask, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTasks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnTasksSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaskPlace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTaskPlaces, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraButton1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraButton2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkForAllUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkHold, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCancelled, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExpectedTime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExpectedTime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTaskOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTaskOrder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTasks).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTaskPlaces).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkForAllUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkHold).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCancelled).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExpectedTime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTaskOrder).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
