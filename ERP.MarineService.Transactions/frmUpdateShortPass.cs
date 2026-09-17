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

public class frmUpdateShortPass : frmBase
{
	private DataRow drMaster;

	private string OperationServiceShortPassID = "";

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblVoyage;

	private UltraTextEditor txtOperation;

	private UltraLabel lblOperation;

	private UltraLabel lblVessels;

	private UltraTextEditor txtVessel;

	private UltraTextEditor txtVoyage;

	private UltraTextEditor txtSeaMan;

	private UltraLabel lblSeaMan;

	private UltraTextEditor txtRemarks;

	private UltraLabel lblRemarks;

	private UltraTextEditor txtReason;

	private UltraLabel lblReason;

	private UltraDateTimeEditor dtpExitDate;

	private UltraLabel lblEntryDate;

	private UltraDateTimeEditor dtpEntryDate;

	private UltraLabel lblExitDate;

	public UltraLabel lblHistory;

	public frmUpdateShortPass()
	{
		InitializeComponent();
	}

	public frmUpdateShortPass(string OPERATIONSERVICESHORTPASSID)
		: this()
	{
		RowID = OPERATIONSERVICESHORTPASSID;
		OperationServiceShortPassID = OPERATIONSERVICESHORTPASSID;
		TableName = "MS_OperationsServicesShortPass";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtpEntryDate.MaskInput = "dd/mm/yyyy hh:mm tt";
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = OperationsServicesShortPass.SelectByID(RowID, GlobalVariables.IsArabic ? "1" : "0");
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
			((Control)(object)txtOperation).Text = drMaster["OperationNo"].ToString();
			dtpEntryDate.Value = (DateTime)drMaster["EntryDate"];
			dtpExitDate.Value = (DateTime)drMaster["ExitDate"];
			((Control)(object)txtVoyage).Text = drMaster["VoyageNo"].ToString();
			((Control)(object)txtVessel).Text = drMaster["VesselName"].ToString();
			((Control)(object)txtSeaMan).Text = drMaster["SubAccount"].ToString();
			((Control)(object)txtReason).Text = drMaster["PassReason"].ToString();
			((Control)(object)txtRemarks).Text = drMaster["Remarks"].ToString();
		}
	}

	public bool ValidateData()
	{
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			OperationsServicesShortPass.Insert_Update(RowID, drMaster["OperationServiceID"].ToString(), drMaster["OperationID"].ToString(), drMaster["SubAccountID"].ToString(), (dtpEntryDate.Value == null) ? "Null" : dtpEntryDate.DateTime.ToString(GlobalVariables.DateLongFormate), (dtpExitDate.Value == null) ? "Null" : dtpExitDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtReason).Text, ((Control)(object)txtRemarks).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			Main.EndBulkTrans(FromServer: false);
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

	private void dtpDateTime_Enter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((UltraWinEditorMaskedControlBase)(UltraDateTimeEditor)sender).SelectAll();
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
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmUpdateShortPass));
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
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblVoyage = new UltraLabel();
		this.txtOperation = new UltraTextEditor();
		this.lblOperation = new UltraLabel();
		this.lblVessels = new UltraLabel();
		this.txtVessel = new UltraTextEditor();
		this.txtVoyage = new UltraTextEditor();
		this.txtSeaMan = new UltraTextEditor();
		this.lblSeaMan = new UltraLabel();
		this.txtRemarks = new UltraTextEditor();
		this.lblRemarks = new UltraLabel();
		this.txtReason = new UltraTextEditor();
		this.lblReason = new UltraLabel();
		this.dtpExitDate = new UltraDateTimeEditor();
		this.lblEntryDate = new UltraLabel();
		this.dtpEntryDate = new UltraDateTimeEditor();
		this.lblExitDate = new UltraLabel();
		this.lblHistory = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeaMan).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRemarks).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReason).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExitDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryDate).BeginInit();
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
		resources.ApplyResources(this.lblVoyage, "lblVoyage");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblVoyage).Appearance = (AppearanceBase)(object)val6;
		this.lblVoyage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVoyage).Name = "lblVoyage";
		((ControlBase)this.lblVoyage).WrapText = false;
		resources.ApplyResources(this.txtOperation, "txtOperation");
		((System.Windows.Forms.Control)(object)this.txtOperation).Name = "txtOperation";
		((EditorButtonControlBase)this.txtOperation).ReadOnly = true;
		resources.ApplyResources(this.lblOperation, "lblOperation");
		this.lblOperation.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOperation).Name = "lblOperation";
		((ControlBase)this.lblOperation).WrapText = false;
		resources.ApplyResources(this.lblVessels, "lblVessels");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblVessels).Appearance = (AppearanceBase)(object)val7;
		this.lblVessels.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblVessels).Name = "lblVessels";
		((ControlBase)this.lblVessels).WrapText = false;
		resources.ApplyResources(this.txtVessel, "txtVessel");
		((System.Windows.Forms.Control)(object)this.txtVessel).Name = "txtVessel";
		((EditorButtonControlBase)this.txtVessel).ReadOnly = true;
		resources.ApplyResources(this.txtVoyage, "txtVoyage");
		((System.Windows.Forms.Control)(object)this.txtVoyage).Name = "txtVoyage";
		((EditorButtonControlBase)this.txtVoyage).ReadOnly = true;
		resources.ApplyResources(this.txtSeaMan, "txtSeaMan");
		((System.Windows.Forms.Control)(object)this.txtSeaMan).Name = "txtSeaMan";
		((EditorButtonControlBase)this.txtSeaMan).ReadOnly = true;
		resources.ApplyResources(this.lblSeaMan, "lblSeaMan");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblSeaMan).Appearance = (AppearanceBase)(object)val8;
		this.lblSeaMan.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSeaMan).Name = "lblSeaMan";
		((ControlBase)this.lblSeaMan).WrapText = false;
		resources.ApplyResources(this.txtRemarks, "txtRemarks");
		((System.Windows.Forms.Control)(object)this.txtRemarks).Name = "txtRemarks";
		resources.ApplyResources(this.lblRemarks, "lblRemarks");
		this.lblRemarks.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRemarks).Name = "lblRemarks";
		((ControlBase)this.lblRemarks).WrapText = false;
		resources.ApplyResources(this.txtReason, "txtReason");
		((System.Windows.Forms.Control)(object)this.txtReason).Name = "txtReason";
		resources.ApplyResources(this.lblReason, "lblReason");
		this.lblReason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReason).Name = "lblReason";
		((ControlBase)this.lblReason).WrapText = false;
		resources.ApplyResources(this.dtpExitDate, "dtpExitDate");
		((UltraWinEditorMaskedControlBase)this.dtpExitDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpExitDate).Name = "dtpExitDate";
		resources.ApplyResources(this.lblEntryDate, "lblEntryDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblEntryDate).Appearance = (AppearanceBase)(object)val9;
		this.lblEntryDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEntryDate).Name = "lblEntryDate";
		((ControlBase)this.lblEntryDate).WrapText = false;
		resources.ApplyResources(this.dtpEntryDate, "dtpEntryDate");
		((UltraWinEditorMaskedControlBase)this.dtpEntryDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpEntryDate).Name = "dtpEntryDate";
		resources.ApplyResources(this.lblExitDate, "lblExitDate");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblExitDate).Appearance = (AppearanceBase)(object)val10;
		this.lblExitDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblExitDate).Name = "lblExitDate";
		((ControlBase)this.lblExitDate).WrapText = false;
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRemarks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRemarks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpExitDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblExitDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEntryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpEntryDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOperation);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVessels);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblVoyage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSeaMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSeaMan);
		base.Name = "frmUpdateShortPass";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSeaMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSeaMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblVessels, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtVoyage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOperation, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpEntryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEntryDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblExitDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpExitDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRemarks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOperation).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtVoyage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSeaMan).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRemarks).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReason).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpExitDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpEntryDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
