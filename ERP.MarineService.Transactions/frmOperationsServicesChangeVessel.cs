using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.MarineService.Transactions;

public class frmOperationsServicesChangeVessel : frmDetails
{
	private DataTable dtServices;

	private DataTable dtVessels;

	private DataTable dtCargoDetails;

	private string OperationServiceID;

	private string OperationID;

	private string ServiceID;

	private bool ReadOnly;

	private IContainer components = null;

	private UltraLabel lblCounterResult;

	private UltraLabel lblCounter;

	public UltraButton btnVesselSearch;

	private UltraLabel lblNewVessel;

	private UltraComboEditor cboNewVessel;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	private UltraTextEditor txtChangeReason;

	private UltraLabel lblChangeReason;

	public frmOperationsServicesChangeVessel()
	{
		InitializeComponent();
	}

	public frmOperationsServicesChangeVessel(DataTable DTServices, string OPERATIONSERVICEID, string OPERATIONID, string SERVICEID, bool READONLY)
		: this()
	{
		dtServices = DTServices;
		OperationServiceID = OPERATIONSERVICEID;
		OperationID = OPERATIONID;
		ServiceID = SERVICEID;
		ReadOnly = READONLY;
	}

	public override void PrepareData()
	{
		GlobalFunctions.FillCombo(cboHeader, dtServices, "ServiceID", "ServiceName");
		dtVessels = Vessels.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboNewVessel, dtVessels, "VesselID", "VesselName");
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
			flag = (((Control)(object)btnSaveAndClose).Enabled = false);
			visible = (((Control)(object)obj4).Enabled = flag);
			((Control)(object)obj3).Enabled = visible;
		}
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DataSource = dtCargoDetails;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].Header).Caption = (GlobalVariables.IsArabic ? "اختار" : "Select");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Selected"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExportDeclarationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم البيان" : "Export DeclarationNo");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Width = (int)((double)((Control)(object)ULGData).Width * 0.6);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["GoodsDescription"].Header).Caption = (GlobalVariables.IsArabic ? "وصف البضاعة" : "Goods Description");
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			dtDetails = OperationsServicesChangeVessel.SelectByOperationServiceID(OperationServiceID.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			if (dtDetails.Rows.Count > 0)
			{
				((TextEditorControlBase)cboNewVessel).Value = dtDetails.Rows[0]["NewVesselID"];
				((TextEditorControlBase)txtChangeReason).Value = dtDetails.Rows[0]["ChangeReason"];
				((TextEditorControlBase)txtNotes).Value = dtDetails.Rows[0]["Notes"];
			}
			dtCargoDetails = OperationsServicesCargos.SelectForChangeVessel(OperationID, OperationServiceID);
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		if (cboNewVessel.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار الباخرة", "Please Select Vessel");
			((TextEditorControlBase)cboNewVessel).Focus();
			return false;
		}
		if (((Control)(object)txtChangeReason).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء ادخال سبب التغيير ", "Please Enter the Change Reason");
			((TextEditorControlBase)txtChangeReason).Focus();
			return false;
		}
		int num = 0;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Selected"].Value.ToString()))
			{
				num++;
			}
		}
		if (num == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء اختيار احد البضائع", "Please Select at least one Row");
			((Control)(object)ULGData).Focus();
			return false;
		}
		return true;
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Selected"].Value.ToString()))
				{
					OperationsServicesChangeVessel.Insert_Update(((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceChangeVesselID"].Value.ToString(), OperationServiceID, OperationID, ((TextEditorControlBase)cboNewVessel).Value.ToString(), ((TextEditorControlBase)txtChangeReason).Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceCargoID"].Value.ToString(), ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				else if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Selected"].Value.ToString()) && !((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceChangeVesselID"].Value.ToString().Equals("-1"))
				{
					OperationsServicesChangeVessel.Delete(((UltraGridBase)ULGData).Rows[i].Cells["OperationServiceChangeVesselID"].Value.ToString(), GlobalVariables.UserID);
				}
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
		if (ReadOnly || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "GoodsDescription" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "ExportDeclarationNo")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
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

	private void btnVesselSearch_Click(object sender, EventArgs e)
	{
		int num = SearchFunctions.VesselsSearch(IsFromServer: false);
		if (num != 0)
		{
			((TextEditorControlBase)cboNewVessel).Value = num;
		}
	}

	private void lblNewVessel_Click(object sender, EventArgs e)
	{
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
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Expected O, but got Unknown
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.Transactions.frmOperationsServicesChangeVessel));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblCounterResult = new UltraLabel();
		this.lblCounter = new UltraLabel();
		this.btnVesselSearch = new UltraButton();
		this.lblNewVessel = new UltraLabel();
		this.cboNewVessel = new UltraComboEditor();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.txtChangeReason = new UltraTextEditor();
		this.lblChangeReason = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNewVessel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtChangeReason).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSave, "btnSave");
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
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
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
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		((UltraGridBase)base.ULGData).FilterRow += new FilterRowEventHandler(ULGData_FilterRow);
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		resources.ApplyResources(base.lblHeader, "lblHeader");
		base.lblHeader.AutoEllipsis = true;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnSaveAndClose, "btnSaveAndClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		((ControlBase)this.lblCounter).WrapText = false;
		resources.ApplyResources(this.btnVesselSearch, "btnVesselSearch");
		((AppearanceBase)val9).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnVesselSearch).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btnVesselSearch).Name = "btnVesselSearch";
		((System.Windows.Forms.Control)(object)this.btnVesselSearch).Click += new System.EventHandler(btnVesselSearch_Click);
		resources.ApplyResources(this.lblNewVessel, "lblNewVessel");
		this.lblNewVessel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNewVessel).Name = "lblNewVessel";
		((ControlBase)this.lblNewVessel).WrapText = false;
		resources.ApplyResources(this.cboNewVessel, "cboNewVessel");
		((TextEditorControlBase)this.cboNewVessel).AlwaysInEditMode = true;
		this.cboNewVessel.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboNewVessel).Name = "cboNewVessel";
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtChangeReason, "txtChangeReason");
		((System.Windows.Forms.Control)(object)this.txtChangeReason).Name = "txtChangeReason";
		resources.ApplyResources(this.lblChangeReason, "lblChangeReason");
		this.lblChangeReason.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblChangeReason).Name = "lblChangeReason";
		((ControlBase)this.lblChangeReason).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtChangeReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblChangeReason);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnVesselSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNewVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNewVessel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Name = "frmOperationsServicesChangeVessel";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNewVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNewVessel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnVesselSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblChangeReason, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtChangeReason, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNewVessel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtChangeReason).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
