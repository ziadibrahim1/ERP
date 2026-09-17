using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Privilege;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.SafesAndBanks.Reports;

public class frmChecksPrint : frmBase
{
	private DataTable dtReports;

	private DataTable dtCurrency;

	private IContainer components = null;

	private UltraButton btnPrintBarCode;

	private UltraLabel lblValue;

	private UltraTextEditor txtValue;

	protected internal UltraCheckEditor chkIsArabic;

	public UltraComboEditor cboReportType;

	public UltraLabel lblReportType;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnNew;

	private UltraTextEditor txtName;

	private UltraLabel lblName;

	public UltraDateTimeEditor dtpDate;

	private UltraLabel lblDate;

	private UltraComboEditor cboCurrency;

	private UltraLabel lblCurrency;

	private UltraTextEditor txtSignatoryName;

	private UltraLabel lblSignatoryName;

	public frmChecksPrint()
	{
		InitializeComponent();
		cboReportType.Items.Clear();
		cboReportType.Items.Add((object)1, GlobalVariables.IsArabic ? "طباعة شيك بنك مصر" : "Print Banque Misr");
		cboReportType.Items.Add((object)2, GlobalVariables.IsArabic ? "طباعة شيك بنك CIB" : "Print Banque CIB");
		cboReportType.Items.Add((object)3, GlobalVariables.IsArabic ? "طباعة شيك بنك HSBC" : "Print Banque HSBC");
		cboReportType.Items.Add((object)4, GlobalVariables.IsArabic ? "طباعة شيك بنك التنمية الصناعية والعمال المصرى" : "Print Banque Industrial Development & Workers ");
		cboReportType.SelectedIndex = 0;
	}

	public override void PrepareData()
	{
		dtpDate.Value = DateTime.Now.Date;
		((UltraToggleEditorBase)chkIsArabic).Checked = GlobalVariables.IsArabic;
		if (base.Tag != null)
		{
			((TextEditorControlBase)cboReportType).Clear();
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
			cboReportType.SelectedIndex = 0;
		}
		dtCurrency = Currency.FillCurrencyByDate(dtpDate.DateTime.ToString(GlobalVariables.DateShortFormate), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCurrency, dtCurrency, "CurrencyID", "CurrencyName");
		cboCurrency.SelectedIndex = 0;
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		if (dtpDate.DateTime.Date < DateTime.Now.Date)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء مراجعة التاريخ " : "Please Enter Valed Date");
			((Control)(object)dtpDate).Focus();
		}
		else if (((Control)(object)txtName).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الاسم" : "Please Enter Name");
			((TextEditorControlBase)txtName).Focus();
		}
		else if (((Control)(object)txtValue).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال القيمه" : "Please Enter Value");
			((TextEditorControlBase)txtValue).Focus();
		}
		else
		{
			ShowReport();
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	public string GetReportName()
	{
		string text = GlobalVariables.ReportsPath;
		if (((TextEditorControlBase)cboReportType).Value.ToString() == "1")
		{
			text += (GlobalVariables.IsArabic ? "Rep_SB_ChecksPrint_BanqueMisr_A.rpt" : "Rep_SB_ChecksPrint_BanqueMisr_E.rpt");
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "2")
		{
			text += (GlobalVariables.IsArabic ? "Rep_SB_ChecksPrint_CIB_A.rpt" : "Rep_SB_ChecksPrint_CIB_E.rpt");
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "3")
		{
			text += (GlobalVariables.IsArabic ? "Rep_SB_ChecksPrint_HSBC_A.rpt" : "Rep_SB_ChecksPrint_HSBC_E.rpt");
		}
		else if (((TextEditorControlBase)cboReportType).Value.ToString() == "4")
		{
			text += (GlobalVariables.IsArabic ? "Rep_SB_ChecksPrint_IndustrialDevelopment_A.rpt" : "Rep_SB_ChecksPrint_IndustrialDevelopment_E.rpt");
		}
		return text;
	}

	public void ShowReport()
	{
		GlobalVariables.ReportDocument = new ReportDocument();
		GlobalVariables.ReportDocument.Load((dtReports.Rows.Count == 0) ? GetReportName() : (GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep" + (((UltraToggleEditorBase)chkIsArabic).Checked ? "_A" : "_E")].ToString()));
		GlobalVariables.ReportDocument.SetParameterValue("@SignatoryName", ((Control)(object)txtSignatoryName).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@Name", ((Control)(object)txtName).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@Date", dtpDate.DateTime.Date);
		GlobalVariables.ReportDocument.SetParameterValue("@Value", ((Control)(object)txtValue).Text);
		GlobalVariables.ReportDocument.SetParameterValue("@CurrencyID", ((TextEditorControlBase)cboCurrency).Value);
		GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", ((UltraToggleEditorBase)chkIsArabic).Checked);
		frmReporViwer frmReporViwer2 = new frmReporViwer();
		frmReporViwer2.ShowDialog();
		GlobalVariables.ReportDocument = null;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnNew_Click(object sender, EventArgs e)
	{
		frmUserReportName frmUserReportName2 = new frmUserReportName();
		frmUserReportName2.WindowState = FormWindowState.Normal;
		frmUserReportName2.ShowDialog();
		if (!frmUserReportName2.Cancel)
		{
			int num = BusinessLayer.Privilege.Reports.InsertNewUserDesign(((DataRow)base.Tag)["FormID"].ToString(), frmUserReportName2.ArName, frmUserReportName2.EnName, dtReports.Rows[cboReportType.SelectedIndex]["ProcedureName"].ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
			UsersReports.Insert_Update("-1", GlobalVariables.UserID, num.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: true);
			frmUserReportName2.Dispose();
			string sourceFileName = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].ToString();
			string sourceFileName2 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"].ToString();
			string text = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_A.rpt";
			string text2 = GlobalVariables.ReportsPath + "Div\\Rep" + num + "_E.rpt";
			File.Copy(sourceFileName, text);
			File.Copy(sourceFileName2, text2);
			Process.Start(text2);
			Process.Start(text);
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
		}
	}

	private void btnUpdate_Click(object sender, EventArgs e)
	{
		string fileName = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"].ToString();
		string fileName2 = GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"].ToString();
		Process.Start(fileName2);
		Process.Start(fileName);
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			UsersReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			GroupsReports.DeleteByReportID(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			BusinessLayer.Privilege.Reports.Delete(((TextEditorControlBase)cboReportType).Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_A"]);
			File.Delete(GlobalVariables.ReportsPath + dtReports.Rows[cboReportType.SelectedIndex]["Rep_E"]);
			((TextEditorControlBase)cboReportType).Clear();
			dtReports = UsersReports.SelectFormReports(GlobalVariables.UserID, ((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboReportType, dtReports, "ReportID", "ReportName");
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	private void frmChecksPrint_Load(object sender, EventArgs e)
	{
		if (base.DesignMode)
		{
			return;
		}
		int num = Convert.ToInt32(((DataRow)base.Tag)["PeriodDays"]);
		DateTime dateTime = GlobalFunctions.GetServerDateTimeNow().AddDays(-num);
		if (!GlobalVariables.SeeingClosedYears)
		{
			if (num == 0 || dateTime < GlobalVariables.MinOpenedDate)
			{
				dtpDate.MinDate = GlobalVariables.MinOpenedDate;
			}
			else
			{
				dtpDate.MinDate = dateTime;
			}
		}
		else if (num > 0)
		{
			dtpDate.MinDate = dateTime;
		}
	}

	private void cboReportType_ValueChanged(object sender, EventArgs e)
	{
		if (dtReports != null && dtReports.Rows.Count > 0 && cboReportType.SelectedIndex > -1)
		{
			((Control)(object)btnUpdate).Enabled = GlobalVariables.UserID == "1" || !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
			((Control)(object)btnDelete).Enabled = !Convert.ToBoolean(dtReports.Rows[cboReportType.SelectedIndex]["ReadOnly"]);
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Expected O, but got Unknown
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SafesAndBanks.Reports.frmChecksPrint));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.btnPrintBarCode = new UltraButton();
		this.lblValue = new UltraLabel();
		this.txtValue = new UltraTextEditor();
		this.chkIsArabic = new UltraCheckEditor();
		this.cboReportType = new UltraComboEditor();
		this.lblReportType = new UltraLabel();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnNew = new UltraButton();
		this.txtName = new UltraTextEditor();
		this.lblName = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblDate = new UltraLabel();
		this.cboCurrency = new UltraComboEditor();
		this.lblCurrency = new UltraLabel();
		this.txtSignatoryName = new UltraTextEditor();
		this.lblSignatoryName = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSignatoryName).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnPrintBarCode, "btnPrintBarCode");
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Name = "btnPrintBarCode";
		((System.Windows.Forms.Control)(object)this.btnPrintBarCode).Click += new System.EventHandler(btnPrint_Click);
		resources.ApplyResources(this.lblValue, "lblValue");
		this.lblValue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValue).Name = "lblValue";
		((ControlBase)this.lblValue).WrapText = false;
		resources.ApplyResources(this.txtValue, "txtValue");
		((System.Windows.Forms.Control)(object)this.txtValue).Name = "txtValue";
		((System.Windows.Forms.Control)(object)this.txtValue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.chkIsArabic, "chkIsArabic");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		((UltraToggleEditorBase)this.chkIsArabic).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkIsArabic).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkIsArabic).Name = "chkIsArabic";
		((UltraControlBase)this.chkIsArabic).UseAppStyling = false;
		resources.ApplyResources(this.cboReportType, "cboReportType");
		this.cboReportType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboReportType).Name = "cboReportType";
		((TextEditorControlBase)this.cboReportType).Nullable = false;
		((TextEditorControlBase)this.cboReportType).ValueChanged += new System.EventHandler(cboReportType_ValueChanged);
		resources.ApplyResources(this.lblReportType, "lblReportType");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.lblReportType).Appearance = (AppearanceBase)(object)val2;
		this.lblReportType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReportType).Name = "lblReportType";
		((ControlBase)this.lblReportType).WrapText = false;
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
		resources.ApplyResources(this.btnUpdate, "btnUpdate");
		((System.Windows.Forms.Control)(object)this.btnUpdate).Name = "btnUpdate";
		((System.Windows.Forms.Control)(object)this.btnUpdate).Click += new System.EventHandler(btnUpdate_Click);
		resources.ApplyResources(this.btnDelete, "btnDelete");
		((System.Windows.Forms.Control)(object)this.btnDelete).Name = "btnDelete";
		((System.Windows.Forms.Control)(object)this.btnDelete).Click += new System.EventHandler(btnDelete_Click);
		resources.ApplyResources(this.btnNew, "btnNew");
		((System.Windows.Forms.Control)(object)this.btnNew).Name = "btnNew";
		((System.Windows.Forms.Control)(object)this.btnNew).Click += new System.EventHandler(btnNew_Click);
		resources.ApplyResources(this.txtName, "txtName");
		((System.Windows.Forms.Control)(object)this.txtName).Name = "txtName";
		resources.ApplyResources(this.lblName, "lblName");
		this.lblName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblName).Name = "lblName";
		((ControlBase)this.lblName).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		this.dtpDate.DateTime = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		this.dtpDate.PromptChar = ' ';
		this.dtpDate.Value = new System.DateTime(2013, 7, 16, 0, 0, 0, 0);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.cboCurrency, "cboCurrency");
		((TextEditorControlBase)this.cboCurrency).AlwaysInEditMode = true;
		this.cboCurrency.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCurrency).Name = "cboCurrency";
		resources.ApplyResources(this.lblCurrency, "lblCurrency");
		this.lblCurrency.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCurrency).Name = "lblCurrency";
		((ControlBase)this.lblCurrency).WrapText = false;
		resources.ApplyResources(this.txtSignatoryName, "txtSignatoryName");
		((System.Windows.Forms.Control)(object)this.txtSignatoryName).Name = "txtSignatoryName";
		resources.ApplyResources(this.lblSignatoryName, "lblSignatoryName");
		this.lblSignatoryName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSignatoryName).Name = "lblSignatoryName";
		((ControlBase)this.lblSignatoryName).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNew);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReportType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrintBarCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsArabic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSignatoryName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSignatoryName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCurrency);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValue);
		base.Name = "frmChecksPrint";
		base.Load += new System.EventHandler(frmChecksPrint_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCurrency, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSignatoryName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSignatoryName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsArabic, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrintBarCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboReportType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNew, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCurrency, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsArabic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReportType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCurrency).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSignatoryName).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
