using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
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

public class frmPosted : frmBase
{
	public DataTable dtsource = new DataTable();

	public bool fromServer;

	private IContainer components = null;

	protected internal UltraGrid ULGData;

	protected internal UltraGroupBox UGBByName;

	protected internal UltraButton btnPost;

	protected internal UltraButton btnCancel;

	public UltraButton btnHeaderSearch;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraTextEditor txtCode;

	public UltraLabel lblCode;

	public UltraButton btnRefreshData;

	public UltraButton btnSaveClose;

	public UltraButton btnKeyboard;

	public UltraButton btnOpenTicket;

	public frmPosted()
	{
		InitializeComponent();
		((Control)(object)ULGData).RightToLeft = RightToLeft.No;
	}

	public virtual void btnPost_Click(object sender, EventArgs e)
	{
		if (!CanPost)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		Main.StartBulkTrans(fromServer);
		try
		{
			SaveData();
			Main.EndBulkTrans(fromServer);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count < count)
			{
				GlobalVariables.InformationMB.Show("تمت العملي\u0651ة بنجاح", "Operation done successfully");
			}
		}
		catch
		{
			Main.RollbackBulkTrans(fromServer);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		CloseClicked();
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		SelectFullRow();
	}

	public virtual void SelectFullRow()
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	public virtual void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Open"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Open");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).Caption = (GlobalVariables.IsArabic ? "مراجعة" : "Audit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Open"].Value = (GlobalVariables.IsArabic ? "مراجعة" : "Audit");
		}
	}

	public virtual void SaveData()
	{
	}

	public virtual void CloseClicked()
	{
		Close();
	}

	private void frmPosted_Load(object sender, EventArgs e)
	{
		FillGrid();
		((TextEditorControlBase)txtCode).Focus();
	}

	public virtual void FillGrid()
	{
	}

	private void ULGData_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
	{
		AfterSelectChange();
	}

	public virtual void AfterSelectChange()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
	}

	private void btnHeaderSearch_Click(object sender, EventArgs e)
	{
		Search();
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (e.KeyCode == Keys.F8)
		{
			Search();
		}
		else if (e.KeyCode == Keys.F5)
		{
			RefrechData();
		}
	}

	public virtual void Search()
	{
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		RefrechData();
	}

	public virtual void RefrechData()
	{
		FillGrid();
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		ClickCellButton();
	}

	public virtual void ClickCellButton()
	{
	}

	public virtual void txtCode_ValueChanged(object sender, EventArgs e)
	{
		txtValueChange();
	}

	public virtual void txtValueChange()
	{
		DataView dataView = new DataView(dtsource);
		dataView.RowFilter = NoCol + " Like '%" + ((Control)(object)txtCode).Text + "%'";
		dataView.RowStateFilter = DataViewRowState.CurrentRows;
		((UltraGridBase)ULGData).DataSource = dataView;
		InitGrid();
	}

	private void btnSaveClose_Click(object sender, EventArgs e)
	{
		if (!CanPost)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		Main.StartBulkTrans(fromServer);
		try
		{
			SaveData();
			Main.EndBulkTrans(fromServer);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count < count)
			{
				GlobalVariables.InformationMB.Show("تمت العملي\u0651ة بنجاح", "Operation done successfully");
				Close();
			}
		}
		catch
		{
			Main.RollbackBulkTrans(fromServer);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
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
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Expected O, but got Unknown
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmPosted));
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
		this.UGBByName = new UltraGroupBox();
		this.ULGData = new UltraGrid();
		this.btnPost = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnHeaderSearch = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.txtCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		this.btnRefreshData = new UltraButton();
		this.btnSaveClose = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.UGBByName, "UGBByName");
		this.UGBByName.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBByName).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.UGBByName).Name = "UGBByName";
		((UltraGridBase)this.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance8.FontData");
		resources.ApplyResources(val2, "appearance8");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectorStyle = (HeaderStyle)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(this.ULGData, "ULGData");
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseFlatMode = (DefaultableBoolean)1;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		this.ULGData.AfterSelectChange += new AfterSelectChangeEventHandler(ULGData_AfterSelectChange);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(this.btnPost, "btnPost");
		((System.Windows.Forms.Control)(object)this.btnPost).Name = "btnPost";
		((System.Windows.Forms.Control)(object)this.btnPost).Click += new System.EventHandler(btnPost_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val6).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance7.FontData");
		resources.ApplyResources(val6, "appearance7");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val6;
		((UltraButtonBase)this.btnCancel).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		((AppearanceBase)val7).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val7, "appearance15");
		resources.ApplyResources(((AppearanceBase)val7).FontData, "appearance15.FontData");
		((SubObjectBase)val7).ForceApplyResources = "|FontData";
		((ControlBase)this.btnHeaderSearch).Appearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(this.btnHeaderSearch, "btnHeaderSearch");
		((System.Windows.Forms.Control)(object)this.btnHeaderSearch).Name = "btnHeaderSearch";
		((System.Windows.Forms.Control)(object)this.btnHeaderSearch).Click += new System.EventHandler(btnHeaderSearch_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance13");
		resources.ApplyResources(((AppearanceBase)val8).FontData, "appearance13.FontData");
		((SubObjectBase)val8).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val9).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val9, "appearance6");
		resources.ApplyResources(((AppearanceBase)val9).FontData, "appearance6.FontData");
		((SubObjectBase)val9).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((TextEditorControlBase)this.txtCode).ValueChanged += new System.EventHandler(txtCode_ValueChanged);
		((AppearanceBase)val10).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val10).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val10, "appearance11");
		resources.ApplyResources(((AppearanceBase)val10).FontData, "appearance11.FontData");
		((SubObjectBase)val10).ForceApplyResources = "|FontData";
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.lblCode, "lblCode");
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this.btnSaveClose, "btnSaveClose");
		((System.Windows.Forms.Control)(object)this.btnSaveClose).Name = "btnSaveClose";
		((System.Windows.Forms.Control)(object)this.btnSaveClose).Click += new System.EventHandler(btnSaveClose_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val11).FontData, "appearance2.FontData");
		resources.ApplyResources(val11, "appearance2");
		((SubObjectBase)val11).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val11;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnPost;
		resources.ApplyResources(this, "$this");
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnCancel;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPost);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnHeaderSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBByName);
		base.Name = "frmPosted";
		base.Load += new System.EventHandler(frmPosted_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBByName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPost, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		base.ResumeLayout(false);
	}
}
