using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.AbstractForms;

public class frmReport2010 : frmBase
{
	public DataTable dtBranches;

	public DataTable dtItems;

	public DataTable dtItems2;

	public string Branches;

	public string Items;

	public string Items2;

	private IContainer components = null;

	public UltraButton btnPreview;

	public UltraLabel ultraLabel1;

	public UltraLabel ultraLabel2;

	public UltraDateTimeEditor dtpFromDate;

	public UltraDateTimeEditor dtpToDate;

	public UltraCheckEditor chkAllBranches;

	protected internal CheckedListBox clbBranches;

	public UltraCheckEditor chkWithLogo;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblReportType;

	public UltraComboEditor cboReportType;

	protected internal UltraCheckEditor chkAll;

	protected internal CheckedListBox clbItems;

	protected internal UltraCheckEditor chkAll2;

	protected internal CheckedListBox clbItems2;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraButton btnItems2Search;

	public UltraButton btnItemsSearch;

	private UltraTextEditor txtItems2;

	private UltraTextEditor txtItems;

	public UltraButton btnKeyboard;

	public frmReport2010()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		InitializeComponent();
		base.CancelButton = (IButtonControl)btnClose;
		((UltraToggleEditorBase)chkWithLogo).CheckAlign = (((UltraToggleEditorBase)chkIsArabic).CheckAlign = (((UltraToggleEditorBase)chkAll).CheckAlign = (((UltraToggleEditorBase)chkAll2).CheckAlign = (((UltraToggleEditorBase)chkAllBranches).CheckAlign = (GlobalVariables.IsArabic ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft)))));
		AppearanceBase appearance = ((UltraToggleEditorBase)chkWithLogo).Appearance;
		AppearanceBase appearance2 = ((UltraToggleEditorBase)chkIsArabic).Appearance;
		AppearanceBase appearance3 = ((UltraToggleEditorBase)chkAll).Appearance;
		AppearanceBase appearance4 = ((UltraToggleEditorBase)chkAll2).Appearance;
		AppearanceBase appearance5 = ((UltraToggleEditorBase)chkAllBranches).Appearance;
		int num = ((!GlobalVariables.IsArabic) ? 1 : 3);
		HAlign val = (HAlign)num;
		appearance5.TextHAlign = (HAlign)num;
		appearance.TextHAlign = (appearance2.TextHAlign = (appearance3.TextHAlign = (appearance4.TextHAlign = val)));
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
	}

	public virtual void GetItems(string ColumnName, string ColumnName2, string ColumnName3)
	{
		Branches = ",";
		for (int i = 0; i < clbBranches.Items.Count; i++)
		{
			if (clbBranches.GetItemChecked(i))
			{
				Branches = Branches + dtBranches.Rows[i][ColumnName].ToString() + ",";
			}
		}
		Items = ",";
		for (int j = 0; j < clbItems.Items.Count; j++)
		{
			if (clbItems.GetItemChecked(j))
			{
				Items = Items + dtItems.Rows[j][ColumnName2].ToString() + ",";
			}
		}
		Items2 = ",";
		for (int k = 0; k < clbItems2.Items.Count; k++)
		{
			if (clbItems2.GetItemChecked(k))
			{
				Items2 = Items2 + dtItems2.Rows[k][ColumnName3].ToString() + ",";
			}
		}
	}

	public virtual void FillData()
	{
	}

	public virtual void ShowReport()
	{
	}

	public virtual string GetReportName()
	{
		return "";
	}

	public virtual void FormLoad()
	{
	}

	public void frmReport_Load(object sender, EventArgs e)
	{
		FormLoad();
		dtpFromDate.Value = DateTime.Now.Date;
		dtpToDate.Value = DateTime.Now.Date.AddHours(23.0).AddMinutes(59.0).AddSeconds(59.0);
		if (!base.DesignMode)
		{
			int num = Convert.ToInt32(((DataRow)base.Tag)["PeriodDays"]);
			DateTime dateTime = GlobalFunctions.GetServerDateTimeNow().AddDays(-num);
			if (!GlobalVariables.SeeingClosedYears)
			{
				if (num == 0 || dateTime < GlobalVariables.MinOpenedDate)
				{
					dtpFromDate.MinDate = GlobalVariables.MinOpenedDate;
				}
				else
				{
					dtpFromDate.MinDate = dateTime;
				}
			}
			else if (num > 0)
			{
				dtpFromDate.MinDate = dateTime;
			}
		}
		FillData();
	}

	public void btnPreview_Click(object sender, EventArgs e)
	{
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GetReportName());
			ShowReport();
			GlobalVariables.ReportDocument = null;
		}
		catch (Exception ex)
		{
			if (ex.Message == "Load report failed.")
			{
				GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalied Reports Path \r\n Please Check Report Path from System Tools");
			}
			else
			{
				GlobalVariables.InformationMB.Show(ex.Message);
			}
		}
	}

	public void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	public virtual void dtpFromDate_ValueChanged(object sender, EventArgs e)
	{
		FillData();
	}

	public virtual void dtpToDate_ValueChanged(object sender, EventArgs e)
	{
		FillData();
	}

	private void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		if (sender == chkAllBranches)
		{
			SelectAllListBoxItems(((UltraToggleEditorBase)chkAllBranches).Checked, clbBranches);
		}
		else if (sender == chkAll)
		{
			SelectAllListBoxItems(((UltraToggleEditorBase)chkAll).Checked, clbItems);
		}
		else if (sender == chkAll2)
		{
			SelectAllListBoxItems(((UltraToggleEditorBase)chkAll2).Checked, clbItems2);
		}
	}

	public virtual void SelectAllListBoxItems(bool Checked, CheckedListBox lst)
	{
		for (int i = 0; i < lst.Items.Count; i++)
		{
			lst.SetItemChecked(i, Checked);
		}
	}

	private void clbItems_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAllBranches).Checked = clbBranches.CheckedItems.Count == clbBranches.Items.Count && clbBranches.Items.Count > 0;
		((UltraToggleEditorBase)chkAllBranches).CheckedChanged += chkAll_CheckedChanged;
	}

	private void clbItems2_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAll).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAll).Checked = clbItems.CheckedItems.Count == clbItems.Items.Count && clbItems.Items.Count > 0;
		((UltraToggleEditorBase)chkAll).CheckedChanged += chkAll_CheckedChanged;
	}

	private void clbItems3_SelectedValueChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkAll2).CheckedChanged -= chkAll_CheckedChanged;
		((UltraToggleEditorBase)chkAll2).Checked = clbItems2.CheckedItems.Count == clbItems2.Items.Count && clbItems2.Items.Count > 0;
		((UltraToggleEditorBase)chkAll2).CheckedChanged += chkAll_CheckedChanged;
	}

	private void txtItems_ValueChanged(object sender, EventArgs e)
	{
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmReport2010));
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
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.clbBranches = new System.Windows.Forms.CheckedListBox();
		this.dtpFromDate = new UltraDateTimeEditor();
		this.dtpToDate = new UltraDateTimeEditor();
		this.chkAllBranches = new UltraCheckEditor();
		this.btnPreview = new UltraButton();
		this.chkWithLogo = new UltraCheckEditor();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblReportType = new UltraLabel();
		this.cboReportType = new UltraComboEditor();
		this.chkAll = new UltraCheckEditor();
		this.clbItems = new System.Windows.Forms.CheckedListBox();
		this.chkAll2 = new UltraCheckEditor();
		this.clbItems2 = new System.Windows.Forms.CheckedListBox();
		this.chkIsArabic = new UltraCheckEditor();
		this.btnItems2Search = new UltraButton();
		this.btnItemsSearch = new UltraButton();
		this.txtItems2 = new UltraTextEditor();
		this.txtItems = new UltraTextEditor();
		this.btnKeyboard = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val2;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.clbBranches, "clbBranches");
		this.clbBranches.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbBranches.CheckOnClick = true;
		this.clbBranches.Name = "clbBranches";
		this.clbBranches.SelectedValueChanged += new System.EventHandler(clbItems_SelectedValueChanged);
		resources.ApplyResources(this.dtpFromDate, "dtpFromDate");
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		this.dtpFromDate.PromptChar = ' ';
		this.dtpFromDate.ValueChanged += new System.EventHandler(dtpFromDate_ValueChanged);
		resources.ApplyResources(this.dtpToDate, "dtpToDate");
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		this.dtpToDate.PromptChar = ' ';
		this.dtpToDate.ValueChanged += new System.EventHandler(dtpToDate_ValueChanged);
		resources.ApplyResources(this.chkAllBranches, "chkAllBranches");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		((UltraToggleEditorBase)this.chkAllBranches).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAllBranches).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAllBranches).Name = "chkAllBranches";
		((UltraToggleEditorBase)this.chkAllBranches).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.btnPreview, "btnPreview");
		((System.Windows.Forms.Control)(object)this.btnPreview).Name = "btnPreview";
		((System.Windows.Forms.Control)(object)this.btnPreview).Click += new System.EventHandler(btnPreview_Click);
		resources.ApplyResources(this.chkWithLogo, "chkWithLogo");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((UltraToggleEditorBase)this.chkWithLogo).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkWithLogo).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkWithLogo).Name = "chkWithLogo";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val6;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblReportType, "lblReportType");
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val7;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
		resources.ApplyResources(this.cboReportType, "cboReportType");
		this.cboReportType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboReportType).Name = "cboReportType";
		((TextEditorControlBase)this.cboReportType).Nullable = false;
		resources.ApplyResources(this.chkAll, "chkAll");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((UltraToggleEditorBase)this.chkAll).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.chkAll).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll).Name = "chkAll";
		((UltraControlBase)this.chkAll).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.clbItems, "clbItems");
		this.clbItems.CheckOnClick = true;
		this.clbItems.FormattingEnabled = true;
		this.clbItems.Name = "clbItems";
		this.clbItems.SelectedValueChanged += new System.EventHandler(clbItems2_SelectedValueChanged);
		resources.ApplyResources(this.chkAll2, "chkAll2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((UltraToggleEditorBase)this.chkAll2).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.chkAll2).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkAll2).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkAll2).Name = "chkAll2";
		((UltraControlBase)this.chkAll2).UseAppStyling = false;
		((UltraToggleEditorBase)this.chkAll2).CheckedChanged += new System.EventHandler(chkAll_CheckedChanged);
		resources.ApplyResources(this.clbItems2, "clbItems2");
		this.clbItems2.CheckOnClick = true;
		this.clbItems2.FormattingEnabled = true;
		this.clbItems2.Name = "clbItems2";
		this.clbItems2.SelectedValueChanged += new System.EventHandler(clbItems3_SelectedValueChanged);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		resources.ApplyResources(this.btnItems2Search, "btnItems2Search");
		((AppearanceBase)val11).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.btnItems2Search).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btnItems2Search).Name = "btnItems2Search";
		resources.ApplyResources(this.btnItemsSearch, "btnItemsSearch");
		((AppearanceBase)val12).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnItemsSearch).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btnItemsSearch).Name = "btnItemsSearch";
		resources.ApplyResources(this.txtItems2, "txtItems2");
		((System.Windows.Forms.Control)(object)this.txtItems2).Name = "txtItems2";
		resources.ApplyResources(this.txtItems, "txtItems");
		((System.Windows.Forms.Control)(object)this.txtItems).Name = "txtItems";
		((TextEditorControlBase)this.txtItems).ValueChanged += new System.EventHandler(txtItems_ValueChanged);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val13).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val13;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItems2Search);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnItemsSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll2);
		base.Controls.Add(this.clbItems2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAll);
		base.Controls.Add(this.clbItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkWithLogo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add(this.clbBranches);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Name = "frmReport2010";
		base.Load += new System.EventHandler(frmReport_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex(this.clbBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpFromDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpToDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllBranches, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkWithLogo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex(this.clbItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll, 0);
		base.Controls.SetChildIndex(this.clbItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAll2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtItems2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItemsSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnItems2Search, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllBranches).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkWithLogo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAll2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtItems).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
