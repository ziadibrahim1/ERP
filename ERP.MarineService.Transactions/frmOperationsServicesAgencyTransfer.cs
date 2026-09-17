using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesAgencyTransfer : frmBase
{
	private DataTable dtAgents;

	private DataTable dtOperationsServicesAgencyTransfer;

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private bool FromAgent;

	private bool ReadOnly;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnSave;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraComboEditor cboAgent;

	private UltraLabel lblAgent;

	public UltraButton btnAgentSearch;

	public frmOperationsServicesAgencyTransfer()
	{
		InitializeComponent();
	}

	public frmOperationsServicesAgencyTransfer(string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, bool FROMAGENT, bool READONLY)
		: this()
	{
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		FromAgent = FROMAGENT;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtAgents = Agents.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboAgent, dtAgents, "SubAccountID", "AgentName");
		dtOperationsServicesAgencyTransfer = OperationsServicesAgencyTransfer.SelectByOperationServiceID(OperationServiceID, GlobalVariables.IsArabic ? "1" : "0");
		DisplayControls();
		((Control)(object)btnSave).Enabled = !ReadOnly;
	}

	public bool ValidateData()
	{
		if (cboAgent.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار الوكيل" : "Please Select Agent");
			((TextEditorControlBase)cboAgent).Focus();
			cboAgent.DropDown();
			return false;
		}
		return true;
	}

	public void DisplayControls()
	{
		if (dtOperationsServicesAgencyTransfer != null && dtOperationsServicesAgencyTransfer.Rows.Count > 0)
		{
			((TextEditorControlBase)cboAgent).Value = dtOperationsServicesAgencyTransfer.Rows[0]["AgentSubAccountID"];
			((Control)(object)txtNotes).Text = dtOperationsServicesAgencyTransfer.Rows[0]["Notes"].ToString();
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				int num = OperationsServicesAgencyTransfer.Insert_Update((dtOperationsServicesAgencyTransfer.Rows.Count > 0) ? dtOperationsServicesAgencyTransfer.Rows[0]["OperationServiceAgencyTransferID"].ToString() : "-1", OperationServiceID, OperationID, ((TextEditorControlBase)cboAgent).Value.ToString(), FromAgent ? "1" : "0", (((Control)(object)txtNotes).Text == "") ? "Null" : ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				return;
			}
			Close();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnAgentSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.AgentsSearch("-1", IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboAgent).Value = num;
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesAgencyTransfer));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnSave = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.cboAgent = new UltraComboEditor();
		this.lblAgent = new UltraLabel();
		this.btnAgentSearch = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgent).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance7");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance8.Image");
		resources.ApplyResources(val2, "appearance8");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance9");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance10.Image");
		resources.ApplyResources(val4, "appearance10");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance11");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		this.txtNotes.Scrollbars = System.Windows.Forms.ScrollBars.Both;
		resources.ApplyResources(this.cboAgent, "cboAgent");
		((TextEditorControlBase)this.cboAgent).AlwaysInEditMode = true;
		this.cboAgent.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboAgent).Name = "cboAgent";
		resources.ApplyResources(this.lblAgent, "lblAgent");
		this.lblAgent.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgent).Name = "lblAgent";
		((ControlBase)this.lblAgent).WrapText = false;
		resources.ApplyResources(this.btnAgentSearch, "btnAgentSearch");
		((AppearanceBase)val6).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val6, "appearance12");
		((ControlBase)this.btnAgentSearch).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btnAgentSearch).Name = "btnAgentSearch";
		((System.Windows.Forms.Control)(object)this.btnAgentSearch).Click += new System.EventHandler(btnAgentSearch_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboAgent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgent);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAgentSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmOperationsServicesAgencyTransfer";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAgentSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgent, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboAgent, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboAgent).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
