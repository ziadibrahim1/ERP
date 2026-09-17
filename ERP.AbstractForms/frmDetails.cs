using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.AbstractForms;

public class frmDetails : frmBase
{
	public DataTable dtDetails;

	public bool HasChanges = false;

	public bool SaveError = false;

	private IContainer components = null;

	public UltraButton btnClose;

	public UltraButton btnSave;

	public UltraGrid ULGData;

	public UltraButton btnCancel;

	public UltraButton btnHeaderSearch;

	public UltraComboEditor cboHeader;

	public UltraLabel lblHeader;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraButton btnSaveAndClose;

	public UltraButton btnKeyboard;

	public UltraButton btnOpenTicket;

	public frmDetails()
	{
		InitializeComponent();
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnHeaderSearch).Enabled && ((Control)(object)btnHeaderSearch).Visible)
			{
				btnHeaderSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
		}
	}

	public override void PrepareData()
	{
		base.PrepareData();
	}

	public virtual void DisplayData()
	{
		SetControls(hasChanges: false);
	}

	public virtual void SetControls(bool hasChanges)
	{
		((Control)(object)btnSave).Enabled = hasChanges;
		((Control)(object)btnSaveAndClose).Enabled = hasChanges;
		((Control)(object)btnCancel).Enabled = hasChanges;
		HasChanges = hasChanges;
	}

	public virtual void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
	}

	public virtual bool ValidateData()
	{
		return true;
	}

	private void btnSaveData()
	{
		if (ValidateData())
		{
			SaveError = false;
			SaveData();
			if (SaveError)
			{
				SaveError = false;
				return;
			}
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", " Data Saved Successfuly ");
			SetControls(hasChanges: false);
		}
	}

	public virtual void SaveData()
	{
	}

	public virtual void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد الحذف ؟", "Delete This Row ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
		else
		{
			SetControls(hasChanges: true);
		}
	}

	public virtual void ULGData_CellChange(object sender, CellEventArgs e)
	{
		SetControls(hasChanges: true);
	}

	private void cboHeader_BeforeDropDown(object sender, CancelEventArgs e)
	{
		if (HasChanges)
		{
			e.Cancel = true;
		}
	}

	private void cboHeader_BeforeEnterEditMode(object sender, CancelEventArgs e)
	{
		if (HasChanges)
		{
			e.Cancel = true;
		}
	}

	private void cboHeader_ValueChanged(object sender, EventArgs e)
	{
		DisplayData();
	}

	public virtual void btnSave_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			btnSaveData();
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		DisplayData();
	}

	public virtual void btnClose_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			GlobalVariables.QuestionMB.Show("هل تريد حفظ التغييرات ؟", "Do you Want To Save Changes ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				btnSaveData();
			}
		}
		Dispose();
	}

	public virtual void btnHeaderSearch_Click(object sender, EventArgs e)
	{
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			GlobalVariables.QuestionMB.Show("هل تريد حفظ التغييرات ؟", "Do you Want To Save Changes ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				btnSaveData();
			}
		}
		NextData();
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			GlobalVariables.QuestionMB.Show("هل تريد حفظ التغييرات ؟", "Do you Want To Save Changes ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				btnSaveData();
			}
		}
		PriveousData();
	}

	public virtual void PriveousData()
	{
		if (((DisposableObjectCollectionBase)cboHeader.Items).Count > 0)
		{
			cboHeader.SelectedIndex = ((cboHeader.SelectedIndex != 0) ? (cboHeader.SelectedIndex - 1) : 0);
		}
	}

	public virtual void NextData()
	{
		if (((DisposableObjectCollectionBase)cboHeader.Items).Count > 0)
		{
			cboHeader.SelectedIndex = ((cboHeader.SelectedIndex == ((DisposableObjectCollectionBase)cboHeader.Items).Count - 1) ? (((DisposableObjectCollectionBase)cboHeader.Items).Count - 1) : (cboHeader.SelectedIndex + 1));
		}
	}

	public virtual void SelectFullRow(object sender, EventArgs e)
	{
		if (cboHeader.SelectedIndex == -1)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public virtual void btnSaveAndClose_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			btnSaveData();
		}
		Dispose();
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		if (e.KeyCode != Keys.F11)
		{
			return;
		}
		if (ULGData.ActiveCell.ValueList != null)
		{
			frmValueListSearch frmValueListSearch2 = new frmValueListSearch((ValueList)ULGData.ActiveCell.ValueList, ((object)ULGData.ActiveCell.Column.Header).ToString());
			frmValueListSearch2.WindowState = FormWindowState.Normal;
			frmValueListSearch2.ShowDialog();
			if (frmValueListSearch2.ResultID > 0)
			{
				ULGData.ActiveCell.Value = frmValueListSearch2.ResultID;
				SetControls(hasChanges: true);
			}
		}
		else if (ULGData.ActiveCell.Column.ValueList != null)
		{
			frmValueListSearch frmValueListSearch3 = new frmValueListSearch((ValueList)ULGData.ActiveCell.Column.ValueList, ((HeaderBase)ULGData.ActiveCell.Column.Header).Caption);
			frmValueListSearch3.WindowState = FormWindowState.Normal;
			frmValueListSearch3.ShowDialog();
			if (frmValueListSearch3.ResultID > 0)
			{
				ULGData.ActiveCell.Value = frmValueListSearch3.ResultID;
				SetControls(hasChanges: true);
			}
		}
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
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
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Expected O, but got Unknown
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmDetails));
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
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		this.lblHeader = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnSave = new UltraButton();
		this.ULGData = new UltraGrid();
		this.cboHeader = new UltraComboEditor();
		this.btnCancel = new UltraButton();
		this.btnHeaderSearch = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.btnSaveAndClose = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboHeader).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblHeader, "lblHeader");
		this.lblHeader.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHeader).Name = "lblHeader";
		((ControlBase)this.lblHeader).WrapText = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val3).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val4, "appearance4");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val4;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val9, "appearance9");
		((AppearanceBase)val9).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(SelectFullRow);
		this.ULGData.CellChange += new CellEventHandler(ULGData_CellChange);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(this.cboHeader, "cboHeader");
		this.cboHeader.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboHeader).Name = "cboHeader";
		((TextEditorControlBase)this.cboHeader).Nullable = false;
		this.cboHeader.BeforeDropDown += new System.ComponentModel.CancelEventHandler(cboHeader_BeforeDropDown);
		((TextEditorControlBase)this.cboHeader).ValueChanged += new System.EventHandler(cboHeader_ValueChanged);
		((TextEditorControlBase)this.cboHeader).BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(cboHeader_BeforeEnterEditMode);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val13).Image = resources.GetObject("appearance13.Image");
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val13;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnHeaderSearch, "btnHeaderSearch");
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.btnHeaderSearch).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnHeaderSearch).Name = "btnHeaderSearch";
		((System.Windows.Forms.Control)(object)this.btnHeaderSearch).Click += new System.EventHandler(btnHeaderSearch_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val15).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val17).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val18).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.btnSaveAndClose, "btnSaveAndClose");
		((AppearanceBase)val19).Image = resources.GetObject("appearance19.Image");
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.btnSaveAndClose).Appearance = (AppearanceBase)(object)val19;
		((ControlBase)this.btnSaveAndClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Name = "btnSaveAndClose";
		((System.Windows.Forms.Control)(object)this.btnSaveAndClose).Click += new System.EventHandler(btnSaveAndClose_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val20).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val20, "appearance20");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val20;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this, "$this");
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveAndClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnHeaderSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHeader);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboHeader);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboHeader).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
