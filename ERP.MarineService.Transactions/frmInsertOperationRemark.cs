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
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmInsertOperationRemark : frmBase
{
	private DataTable dtUsers;

	private DataTable dtVessels;

	private DataTable dtVoyages;

	private string OperationRemarkID = "-1";

	private DataRow drMaster;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraComboEditor cboVoyages;

	private UltraLabel lblVoyage;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraComboEditor cboUsers;

	private UltraLabel lblToUser;

	private UltraTextEditor txtComment;

	private UltraLabel lblComment;

	private UltraTextEditor txtRemarks;

	private UltraLabel lblRemarks;

	private UltraLabel lblVessels;

	private UltraComboEditor cboVessels;

	private UltraCheckEditor chkFollowUp;

	private UltraCheckEditor chkCompleted;

	public UltraLabel lblHistory;

	public frmInsertOperationRemark()
	{
		InitializeComponent();
	}

	public frmInsertOperationRemark(string OPERATIONREMARKID)
		: this()
	{
		RowID = OPERATIONREMARKID;
		OperationRemarkID = OPERATIONREMARKID;
		TableName = "MS_OperationsRemarks";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "UserID", "UserName");
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboVessels, dtVessels, "VesselID", "VesselName");
		dtVoyages = Operations.FillCombo(GlobalVariables.BranchIDs);
		dtpDate.Value = DateTime.Now;
		if (OperationRemarkID == "-1")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = OperationsRemarks.SelectByID(OperationRemarkID, GlobalVariables.IsArabic ? "1" : "0");
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

	public void DisplayData()
	{
		if (drMaster != null)
		{
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
			((TextEditorControlBase)cboVessels).Value = drMaster["VesselID"];
			((TextEditorControlBase)cboVoyages).Value = drMaster["OperationID"];
			dtpDate.Value = (DateTime)drMaster["Date"];
			((Control)(object)txtRemarks).Text = drMaster["Remarks"].ToString();
			((Control)(object)txtComment).Text = drMaster["Comment"].ToString();
			((TextEditorControlBase)cboUsers).Value = drMaster["ToUser_ID"];
			((UltraToggleEditorBase)chkFollowUp).Checked = bool.Parse(drMaster["IsFollowUp"].ToString());
			((UltraToggleEditorBase)chkCompleted).Checked = bool.Parse(drMaster["IsCompleted"].ToString());
			((EditorButtonControlBase)cboVessels).ReadOnly = true;
			((EditorButtonControlBase)cboVoyages).ReadOnly = true;
			((EditorButtonControlBase)dtpDate).ReadOnly = true;
		}
	}

	public bool ValidateData()
	{
		if (cboVoyages.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار الرحلة", "Please Select Voyage");
			((TextEditorControlBase)cboVoyages).Focus();
			return false;
		}
		if (cboUsers.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار المستخدم", "Please Select User");
			((TextEditorControlBase)cboUsers).Focus();
			return false;
		}
		if (dtpDate.Value == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال التاريخ " : "Please Enter The Date");
			((Control)(object)dtpDate).Focus();
			dtpDate.DropDown();
			return false;
		}
		if (((Control)(object)txtRemarks).Text.Equals(""))
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال البيان " : "Please Enter The Remark");
			((TextEditorControlBase)txtRemarks).Focus();
			return false;
		}
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OperationsRemarks.Insert_Update(OperationRemarkID, ((TextEditorControlBase)cboVoyages).Value.ToString(), (dtpDate.Value == null) ? "Null" : dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtRemarks).Text, GlobalVariables.UserID, ((TextEditorControlBase)cboUsers).Value.ToString(), ((Control)(object)txtComment).Text, ((UltraToggleEditorBase)chkCompleted).Checked ? "1" : "0", ((UltraToggleEditorBase)chkFollowUp).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
			ClearAllControls();
			Close();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	public void ClearAllControls()
	{
		dtpDate.Value = DateTime.Now;
		((TextEditorControlBase)txtRemarks).Clear();
		((TextEditorControlBase)txtComment).Clear();
		cboVoyages.SelectedIndex = -1;
		cboVessels.SelectedIndex = -1;
		cboUsers.SelectedIndex = -1;
	}

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
	}

	private void cboVessels_ValueChanged(object sender, EventArgs e)
	{
		((Control)(object)cboVoyages).Text = "";
		if (cboVessels.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtVoyages);
			dataView.RowFilter = "Closed = 0 and VesselID =" + ((TextEditorControlBase)cboVessels).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboVoyages.DataSource = dataView;
			cboVoyages.DisplayMember = "VoyageNo";
			cboVoyages.ValueMember = "OperationID";
		}
		else
		{
			cboVoyages.DataSource = null;
		}
	}

	private void lblHistory_Click(object sender, EventArgs e)
	{
		if (TableName != "" && RowID != "")
		{
			frmHistory frmHistory2 = new frmHistory(Trans_Log.SelectByRowID(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0"));
			frmHistory2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmHistory2.lblTitle).Text = "History";
			frmHistory2.ShowDialog();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmInsertOperationRemark));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.cboUsers = new UltraComboEditor();
		this.lblToUser = new UltraLabel();
		this.cboVoyages = new UltraComboEditor();
		this.lblVoyage = new UltraLabel();
		this.txtRemarks = new UltraTextEditor();
		this.lblRemarks = new UltraLabel();
		this.txtComment = new UltraTextEditor();
		this.lblComment = new UltraLabel();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblVessels = new UltraLabel();
		this.cboVessels = new UltraComboEditor();
		this.chkFollowUp = new UltraCheckEditor();
		this.chkCompleted = new UltraCheckEditor();
		this.lblHistory = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVoyages).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRemarks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtComment).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkFollowUp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkCompleted).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.cboUsers, "cboUsers");
		this.cboUsers.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboUsers).Name = "cboUsers";
		resources.ApplyResources(this.lblToUser, "lblToUser");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblToUser).Appearance = (AppearanceBase)(object)val6;
		this.lblToUser.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToUser).Name = "lblToUser";
		((ControlBase)this.lblToUser).WrapText = false;
		resources.ApplyResources(this.cboVoyages, "cboVoyages");
		this.cboVoyages.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVoyages).Name = "cboVoyages";
		resources.ApplyResources(this.lblVoyage, "lblVoyage");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblVoyage).Appearance = (AppearanceBase)(object)val7;
		this.lblVoyage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVoyage).Name = "lblVoyage";
		((ControlBase)this.lblVoyage).WrapText = false;
		resources.ApplyResources(this.txtRemarks, "txtRemarks");
		((System.Windows.Forms.Control)(object)this.txtRemarks).Name = "txtRemarks";
		resources.ApplyResources(this.lblRemarks, "lblRemarks");
		this.lblRemarks.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRemarks).Name = "lblRemarks";
		((ControlBase)this.lblRemarks).WrapText = false;
		resources.ApplyResources(this.txtComment, "txtComment");
		((System.Windows.Forms.Control)(object)this.txtComment).Name = "txtComment";
		resources.ApplyResources(this.lblComment, "lblComment");
		this.lblComment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblComment).Name = "lblComment";
		((ControlBase)this.lblComment).WrapText = false;
		resources.ApplyResources(this.lblDate, "lblDate");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblDate).Appearance = (AppearanceBase)(object)val8;
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).Enter += new System.EventHandler(dtpDateTime_Enter);
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val9;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.cboVessels, "cboVessels");
		this.cboVessels.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboVessels).Name = "cboVessels";
		((TextEditorControlBase)this.cboVessels).ValueChanged += new System.EventHandler(cboVessels_ValueChanged);
		resources.ApplyResources(this.chkFollowUp, "chkFollowUp");
		((System.Windows.Forms.Control)(object)this.chkFollowUp).Name = "chkFollowUp";
		resources.ApplyResources(this.chkCompleted, "chkCompleted");
		((System.Windows.Forms.Control)(object)this.chkCompleted).Name = "chkCompleted";
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkFollowUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkCompleted);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRemarks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtComment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToUser);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblComment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboVoyages);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmInsertOperationRemark";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVoyages, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblComment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToUser, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtComment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkCompleted, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkFollowUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVoyages).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRemarks).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtComment).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboVessels).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkFollowUp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkCompleted).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
