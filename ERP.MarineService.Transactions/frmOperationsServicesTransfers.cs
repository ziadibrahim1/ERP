using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesTransfers : frmDetails
{
	private DataTable dtServices;

	private DataTable dtSubAccount;

	private DataTable dtVessels;

	private ValueList vlSubAccount = new ValueList();

	private ValueList vlFromVessel = new ValueList();

	private ValueList vlToVessel = new ValueList();

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private string VesselID;

	private bool ReadOnly;

	private IContainer components = null;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	public UltraButton btnNewTransfer;

	public frmOperationsServicesTransfers()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public frmOperationsServicesTransfers(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, string VESSELID, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		VesselID = VESSELID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtSubAccount = SubAccounts.FillComboBySubAccountTypeIDsForMarineService(GlobalVariables.AgentSubAccountTypeIDs + GlobalVariables.OwnerSubAccountTypeIDs + GlobalVariables.CharterSubAccountTypeIDs + GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAccount.ValueListItems.Clear();
		for (int i = 0; i < dtSubAccount.Rows.Count; i++)
		{
			vlSubAccount.ValueListItems.Add(dtSubAccount.Rows[i]["SubAccountID"], dtSubAccount.Rows[i]["SubAccountName"].ToString());
		}
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlFromVessel.ValueListItems.Clear();
		vlToVessel.ValueListItems.Clear();
		for (int j = 0; j < dtVessels.Rows.Count; j++)
		{
			vlFromVessel.ValueListItems.Add(dtVessels.Rows[j]["VesselID"], dtVessels.Rows[j]["VesselName"].ToString());
			vlToVessel.ValueListItems.Add(dtVessels.Rows[j]["VesselID"], dtVessels.Rows[j]["VesselName"].ToString());
		}
		UltraButton obj = btnHeaderSearch;
		UltraButton obj2 = btnNext;
		bool flag = (((Control)(object)btnPriveous).Visible = false);
		bool visible = (((Control)(object)obj2).Visible = flag);
		((Control)(object)obj).Visible = visible;
		((EditorButtonControlBase)cboHeader).ReadOnly = true;
		((TextEditorControlBase)cboHeader).Value = ServiceID;
		if (ReadOnly)
		{
			UltraButton obj3 = btnCancel;
			UltraButton obj4 = btnSave;
			UltraButton obj5 = btnSaveAndClose;
			bool flag4 = (((Control)(object)btnNewTransfer).Enabled = false);
			flag = (((Control)(object)obj5).Enabled = flag4);
			visible = (((Control)(object)obj4).Enabled = flag);
			((Control)(object)obj3).Enabled = visible;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationServiceTransferID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAccount;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "البحار" : "Sea Man");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromVesselID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromVesselID"].ValueList = (IValueList)(object)vlFromVessel;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromVesselID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromVesselID"].Header).Caption = (GlobalVariables.IsArabic ? "من باخرة" : "From Vessel");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToVesselID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToVesselID"].ValueList = (IValueList)(object)vlToVessel;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToVesselID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToVesselID"].Header).Caption = (GlobalVariables.IsArabic ? "الى باخرة" : "To Vessel");
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			dtDetails = OperationsServicesTransfers.SelectByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء إختيار البحار", "Please Select Sea Man");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"]).Selected = true;
				return false;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (i != j && ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"].Value.ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["SubAccountID"].Value.ToString())
				{
					GlobalVariables.InformationMB.Show("لا يمكن تكرار إسم البحار", "Cannot Duplicate The Same SeaMan");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["SubAccountID"];
					return false;
				}
			}
		}
		return true;
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value = OperationID;
				((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceID"].Value = OperationServiceID;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceTransferID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("MS_OperationsServicesTransfers", "OperationServiceID", OperationServiceID, "OperationServiceTransferID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				OperationsServicesTransfers.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
			DisplayData();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	private void ULGData_FilterRow(object sender, FilterRowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		((Control)(object)lblCounterResult).Text = ((UltraGridBase)ULGData).Rows.GetFilteredInNonGroupByRows().Length.ToString();
	}

	private void btnNewTransfer_Click(object sender, EventArgs e)
	{
		frmInsertSeaManTransfer frmInsertSeaManTransfer2 = new frmInsertSeaManTransfer(OperationID, OperationServiceID, VesselID);
		frmInsertSeaManTransfer2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		((Control)(object)frmInsertSeaManTransfer2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل من باخرة الى باخرة" : "Transfer From Vessel To Vessel");
		frmInsertSeaManTransfer2.Location = new Point(0, 0);
		frmInsertSeaManTransfer2.ShowDialog();
		dtSubAccount = SubAccounts.SelectBySubAccountTypeIDs(GlobalVariables.SeaManSubAccountTypeIDs + GlobalVariables.CaptainSubAccountTypeIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAccount.ValueListItems.Clear();
		for (int i = 0; i < dtSubAccount.Rows.Count; i++)
		{
			vlSubAccount.ValueListItems.Add(dtSubAccount.Rows[i]["SubAccountID"], dtSubAccount.Rows[i]["SubAccountName"].ToString());
		}
		DisplayData();
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
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
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Expected O, but got Unknown
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesTransfers));
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		this.btnNewTransfer = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)base.ULGData).FilterRow += new FilterRowEventHandler(ULGData_FilterRow);
		resources.ApplyResources(base.cboHeader, "cboHeader");
		resources.ApplyResources(base.lblHeader, "lblHeader");
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this.btnNewTransfer, "btnNewTransfer");
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		((ControlBase)this.btnNewTransfer).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnNewTransfer).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnNewTransfer).Name = "btnNewTransfer";
		((System.Windows.Forms.Control)(object)this.btnNewTransfer).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnNewTransfer).Click += new System.EventHandler(btnNewTransfer_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNewTransfer);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Name = "frmOperationsServicesTransfers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNewTransfer, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
