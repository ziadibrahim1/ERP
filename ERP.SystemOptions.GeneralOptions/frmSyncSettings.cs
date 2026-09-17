using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Defaults;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.SystemOptions.GeneralOptions;

public class frmSyncSettings : frmBase
{
	private DataTable dtSyncConnection = new DataTable();

	private DataTable dtDatabases = new DataTable();

	private DataTable dtBranchs = new DataTable();

	private SqlConnection SyncConnection;

	private string Connectionstring;

	private IContainer components = null;

	private UltraLabel lblServerName;

	private UltraTextEditor txtServerName;

	private UltraTextEditor txtUserName;

	private UltraLabel lblUserName;

	private UltraTextEditor txtPassword;

	private UltraLabel lblPassword;

	private UltraButton btnConnect;

	private UltraLabel lblDatabase;

	private UltraComboEditor cboDataBase;

	private UltraComboEditor cboBranch;

	private UltraLabel lblBranch;

	public UltraButton btnKeyboard;

	public UltraButton btnSaveSyncConection;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraTabControl tcSync;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	private UltraTabPageControl tbGeneral;

	private UltraTabPageControl ultraTabPageControl1;

	protected internal UltraCheckEditor chkStopSync;

	private NumericUpDown txtStopSyncFrom;

	private NumericUpDown txtStopSyncTo;

	private UltraLabel ultraLabel15;

	private UltraLabel ultraLabel5;

	private UltraLabel ultraLabel16;

	private UltraLabel ultraLabel14;

	public UltraButton btnSave;

	private NumericUpDown txtSyncPeriod;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel lblServerName2;

	private UltraTextEditor txtServerName2;

	private UltraButton btnConnect2;

	public frmSyncSettings()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		txtSyncPeriod.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("SynchronizationPeriod"));
		decimal num = (txtStopSyncFrom.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("StopSynchronizationFrom")));
		decimal num3 = num;
		num = (txtStopSyncTo.Value = Convert.ToDecimal(GlobalFunctions.GetDefault("StopSynchronizationTo")));
		decimal num5 = num;
		((UltraToggleEditorBase)chkStopSync).Checked = num3 > 0m || num5 > 0m;
		((Control)(object)btnSaveSyncConection).Enabled = GlobalVariables.UserID == "1";
		dtSyncConnection = BusinessLayer.Defaults.SyncConnection.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
		if (dtSyncConnection.Rows.Count > 0)
		{
			((Control)(object)txtServerName).Text = dtSyncConnection.Rows[0]["ServerName"].ToString();
			((Control)(object)txtServerName2).Text = dtSyncConnection.Rows[0]["ServerName2"].ToString();
			((Control)(object)txtUserName).Text = dtSyncConnection.Rows[0]["UserName"].ToString();
			((Control)(object)txtPassword).Text = dtSyncConnection.Rows[0]["Password"].ToString();
			if (!SetSyncConnection(dtSyncConnection.Rows[0]["ServerName"].ToString(), dtSyncConnection.Rows[0]["UserName"].ToString(), dtSyncConnection.Rows[0]["Password"].ToString()))
			{
				GlobalVariables.InformationMB.Show("لم يتم التوصيل بالخادم", "Connection to the Server is Invalid");
				return;
			}
			GetDatabases();
			((TextEditorControlBase)cboDataBase).Value = dtSyncConnection.Rows[0]["DataBaseName"];
			((TextEditorControlBase)cboBranch).Value = dtSyncConnection.Rows[0]["SyncBranchID"];
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private bool ValidateData()
	{
		if (((Control)(object)txtServerName).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال إسم الخادم" : "Please Enter The Server Name");
			((TextEditorControlBase)txtServerName).Focus();
			return false;
		}
		if (((Control)(object)txtUserName).Text == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال إسم مستخدم قاعدة البيانات" : "Please Enter The Database user Name");
			((TextEditorControlBase)txtUserName).Focus();
			return false;
		}
		if (cboDataBase.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم قاعدة البيانات" : "Please Select The Database Name");
			((TextEditorControlBase)cboDataBase).Focus();
			return false;
		}
		if (cboBranch.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إختيار إسم الفرع" : "Please Select The Branch Name");
			((TextEditorControlBase)cboBranch).Focus();
			return false;
		}
		return true;
	}

	private void btnSaveSyncConection_Click(object sender, EventArgs e)
	{
		if (ValidateData())
		{
			BusinessLayer.Defaults.SyncConnection.Delete("1", GlobalVariables.UserID);
			BusinessLayer.Defaults.SyncConnection.Insert_Update("-1", ((Control)(object)txtServerName).Text, ((Control)(object)txtServerName2).Text, ((Control)(object)txtUserName).Text, ((Control)(object)txtPassword).Text, ((TextEditorControlBase)cboDataBase).Value.ToString(), ((TextEditorControlBase)cboBranch).Value.ToString(), "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "تم الحفظ بنجاح" : "Data Save Successfuly");
		}
	}

	private bool SetSyncConnection(string ServerName, string UserName, string Password)
	{
		Connectionstring = " Server= " + ServerName + " ; Database=master ;User Id= " + UserName + " ; Password = " + Password + ";Connect Timeout=30";
		SyncConnection = new SqlConnection(Connectionstring);
		try
		{
			SyncConnection.Open();
			SyncConnection.Close();
			return true;
		}
		catch (SqlException)
		{
			return false;
		}
	}

	private bool SetSyncConnection2(string ServerName2, string UserName, string Password)
	{
		Connectionstring = " Server= " + ServerName2 + " ; Database=master ;User Id= " + UserName + " ; Password = " + Password + ";Connect Timeout=30";
		SyncConnection = new SqlConnection(Connectionstring);
		try
		{
			SyncConnection.Open();
			SyncConnection.Close();
			return true;
		}
		catch (SqlException)
		{
			return false;
		}
	}

	private DataTable ExecuteQuery_DataTable(string mySelectQuery)
	{
		DataTable dataTable = new DataTable();
		if (SyncConnection.State == ConnectionState.Closed)
		{
			SyncConnection.Open();
		}
		SqlCommand sqlCommand = new SqlCommand(mySelectQuery, SyncConnection);
		sqlCommand.CommandTimeout = 0;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		for (int i = 0; i < sqlDataReader.FieldCount; i++)
		{
			dataTable.Columns.Add(sqlDataReader.GetName(i), sqlDataReader.GetFieldType(i));
		}
		while (sqlDataReader.Read())
		{
			DataRow dataRow = dataTable.NewRow();
			for (int j = 0; j < sqlDataReader.FieldCount; j++)
			{
				dataRow[sqlDataReader.GetName(j)] = sqlDataReader[sqlDataReader.GetName(j)];
			}
			dataTable.Rows.Add(dataRow);
		}
		sqlDataReader.Close();
		if (SyncConnection.State == ConnectionState.Open)
		{
			SyncConnection.Close();
		}
		return dataTable;
	}

	private void GetDatabases()
	{
		dtDatabases = ExecuteQuery_DataTable("select Substring(name,5,LEN(name)) as name,name as DBName from master.dbo.sysdatabases where has_dbaccess(name) = 1 and Rtrim(Substring(name,1,4)) = 'ERP_' Order By name");
		((Control)(object)cboDataBase).Text = "";
		((TextEditorControlBase)cboDataBase).Value = "";
		cboDataBase.DataSource = dtDatabases;
		cboDataBase.DisplayMember = "name";
		cboDataBase.ValueMember = "DBName";
	}

	private void btnConnect_Click(object sender, EventArgs e)
	{
		if (!SetSyncConnection(((Control)(object)txtServerName).Text, ((Control)(object)txtUserName).Text, ((Control)(object)txtPassword).Text))
		{
			GlobalVariables.InformationMB.Show("لم يتم التوصيل بالخادم", "Connection to the Server is Invalid");
			cboDataBase.DataSource = null;
			((TextEditorControlBase)cboDataBase).Value = "";
			((Control)(object)cboDataBase).Text = "";
		}
		else
		{
			GetDatabases();
		}
	}

	private void btnConnect2_Click(object sender, EventArgs e)
	{
		if (!SetSyncConnection2(((Control)(object)txtServerName).Text, ((Control)(object)txtUserName).Text, ((Control)(object)txtPassword).Text))
		{
			GlobalVariables.InformationMB.Show("لم يتم التوصيل بالخادم", "Connection to the Server is Invalid");
			cboDataBase.DataSource = null;
			((TextEditorControlBase)cboDataBase).Value = "";
			((Control)(object)cboDataBase).Text = "";
		}
		else
		{
			GetDatabases();
		}
	}

	private void cboDataBase_ValueChanged(object sender, EventArgs e)
	{
		if (cboDataBase.SelectedIndex > -1)
		{
			dtBranchs = ExecuteQuery_DataTable(" Select BranchID,BranchNameAr,BranchNameEn From " + ((TextEditorControlBase)cboDataBase).Value.ToString() + ".dbo.G_Branches ");
			((Control)(object)cboBranch).Text = "";
			((TextEditorControlBase)cboBranch).Value = "";
			cboBranch.DataSource = dtBranchs;
			cboBranch.DisplayMember = (GlobalVariables.IsArabic ? "BranchNameAr" : "BranchNameEn");
			cboBranch.ValueMember = "BranchID";
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void chkStopJVs_CheckedChanged(object sender, EventArgs e)
	{
		if (!((UltraToggleEditorBase)chkStopSync).Checked)
		{
			txtStopSyncFrom.Value = 0m;
			txtStopSyncTo.Value = 0m;
		}
		txtStopSyncFrom.ReadOnly = !((UltraToggleEditorBase)chkStopSync).Checked;
		txtStopSyncTo.ReadOnly = !((UltraToggleEditorBase)chkStopSync).Checked;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		GlobalFunctions.SetDefault("SynchronizationPeriod", txtSyncPeriod.Value.ToString());
		GlobalFunctions.SetDefault("StopSynchronizationFrom", txtStopSyncFrom.Value.ToString());
		GlobalFunctions.SetDefault("StopSynchronizationTo", txtStopSyncTo.Value.ToString());
		GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "تم الحفظ بنجاح" : "Data Save Successfuly");
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
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Expected O, but got Unknown
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Expected O, but got Unknown
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Expected O, but got Unknown
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Expected O, but got Unknown
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Expected O, but got Unknown
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Expected O, but got Unknown
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralOptions.frmSyncSettings));
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
		UltraTab val15 = new UltraTab();
		UltraTab val16 = new UltraTab();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.btnSave = new UltraButton();
		this.chkStopSync = new UltraCheckEditor();
		this.txtStopSyncFrom = new System.Windows.Forms.NumericUpDown();
		this.txtSyncPeriod = new System.Windows.Forms.NumericUpDown();
		this.txtStopSyncTo = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel15 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel5 = new UltraLabel();
		this.ultraLabel16 = new UltraLabel();
		this.ultraLabel14 = new UltraLabel();
		this.tbGeneral = new UltraTabPageControl();
		this.lblServerName2 = new UltraLabel();
		this.lblServerName = new UltraLabel();
		this.txtServerName2 = new UltraTextEditor();
		this.btnSaveSyncConection = new UltraButton();
		this.txtServerName = new UltraTextEditor();
		this.lblUserName = new UltraLabel();
		this.cboDataBase = new UltraComboEditor();
		this.txtUserName = new UltraTextEditor();
		this.lblPassword = new UltraLabel();
		this.cboBranch = new UltraComboEditor();
		this.txtPassword = new UltraTextEditor();
		this.lblBranch = new UltraLabel();
		this.btnConnect2 = new UltraButton();
		this.btnConnect = new UltraButton();
		this.lblDatabase = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.tcSync = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.chkStopSync).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopSyncFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSyncPeriod).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopSyncTo).BeginInit();
		((System.Windows.Forms.Control)(object)this.tbGeneral).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtServerName2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtServerName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboDataBase).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.tcSync).BeginInit();
		((System.Windows.Forms.Control)(object)this.tcSync).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.chkStopSync);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add(this.txtStopSyncFrom);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add(this.txtSyncPeriod);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add(this.txtStopSyncTo);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel15);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel16);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel14);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.chkStopSync, "chkStopSync");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((UltraToggleEditorBase)this.chkStopSync).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.chkStopSync).BackColor = System.Drawing.Color.Transparent;
		((UltraToggleEditorBase)this.chkStopSync).BackColorInternal = System.Drawing.Color.Transparent;
		((System.Windows.Forms.Control)(object)this.chkStopSync).Name = "chkStopSync";
		((UltraToggleEditorBase)this.chkStopSync).CheckedChanged += new System.EventHandler(chkStopJVs_CheckedChanged);
		resources.ApplyResources(this.txtStopSyncFrom, "txtStopSyncFrom");
		this.txtStopSyncFrom.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtStopSyncFrom.Name = "txtStopSyncFrom";
		resources.ApplyResources(this.txtSyncPeriod, "txtSyncPeriod");
		this.txtSyncPeriod.Maximum = new decimal(new int[4] { 120, 0, 0, 0 });
		this.txtSyncPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtSyncPeriod.Name = "txtSyncPeriod";
		this.txtSyncPeriod.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		resources.ApplyResources(this.txtStopSyncTo, "txtStopSyncTo");
		this.txtStopSyncTo.Maximum = new decimal(new int[4] { 24, 0, 0, 0 });
		this.txtStopSyncTo.Name = "txtStopSyncTo";
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val3;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel15, "ultraLabel15");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.ultraLabel15).Appearance = (AppearanceBase)(object)val4;
		this.ultraLabel15.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel15).Name = "ultraLabel15";
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val5;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.ultraLabel5).Appearance = (AppearanceBase)(object)val6;
		this.ultraLabel5.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.ultraLabel16, "ultraLabel16");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.ultraLabel16).Appearance = (AppearanceBase)(object)val7;
		this.ultraLabel16.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel16).Name = "ultraLabel16";
		((ControlBase)this.ultraLabel16).WrapText = false;
		resources.ApplyResources(this.ultraLabel14, "ultraLabel14");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.ultraLabel14).Appearance = (AppearanceBase)(object)val8;
		this.ultraLabel14.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel14).Name = "ultraLabel14";
		resources.ApplyResources(this.tbGeneral, "tbGeneral");
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.lblServerName2);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.lblServerName);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.txtServerName2);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveSyncConection);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.txtServerName);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.lblUserName);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.cboDataBase);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.txtUserName);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.lblPassword);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.cboBranch);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.txtPassword);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.lblBranch);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.btnConnect2);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.btnConnect);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Controls.Add((System.Windows.Forms.Control)(object)this.lblDatabase);
		((System.Windows.Forms.Control)(object)this.tbGeneral).Name = "tbGeneral";
		resources.ApplyResources(this.lblServerName2, "lblServerName2");
		this.lblServerName2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServerName2).Name = "lblServerName2";
		resources.ApplyResources(this.lblServerName, "lblServerName");
		this.lblServerName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblServerName).Name = "lblServerName";
		resources.ApplyResources(this.txtServerName2, "txtServerName2");
		((System.Windows.Forms.Control)(object)this.txtServerName2).Name = "txtServerName2";
		resources.ApplyResources(this.btnSaveSyncConection, "btnSaveSyncConection");
		((AppearanceBase)val9).Image = resources.GetObject("appearance9.Image");
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.btnSaveSyncConection).Appearance = (AppearanceBase)(object)val9;
		((ControlBase)this.btnSaveSyncConection).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSaveSyncConection).Name = "btnSaveSyncConection";
		((System.Windows.Forms.Control)(object)this.btnSaveSyncConection).Click += new System.EventHandler(btnSaveSyncConection_Click);
		resources.ApplyResources(this.txtServerName, "txtServerName");
		((System.Windows.Forms.Control)(object)this.txtServerName).Name = "txtServerName";
		resources.ApplyResources(this.lblUserName, "lblUserName");
		this.lblUserName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUserName).Name = "lblUserName";
		resources.ApplyResources(this.cboDataBase, "cboDataBase");
		((System.Windows.Forms.Control)(object)this.cboDataBase).Name = "cboDataBase";
		((TextEditorControlBase)this.cboDataBase).ValueChanged += new System.EventHandler(cboDataBase_ValueChanged);
		resources.ApplyResources(this.txtUserName, "txtUserName");
		((System.Windows.Forms.Control)(object)this.txtUserName).Name = "txtUserName";
		resources.ApplyResources(this.lblPassword, "lblPassword");
		this.lblPassword.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassword).Name = "lblPassword";
		resources.ApplyResources(this.cboBranch, "cboBranch");
		((System.Windows.Forms.Control)(object)this.cboBranch).Name = "cboBranch";
		resources.ApplyResources(this.txtPassword, "txtPassword");
		((System.Windows.Forms.Control)(object)this.txtPassword).Name = "txtPassword";
		this.txtPassword.PasswordChar = '*';
		resources.ApplyResources(this.lblBranch, "lblBranch");
		this.lblBranch.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranch).Name = "lblBranch";
		resources.ApplyResources(this.btnConnect2, "btnConnect2");
		((System.Windows.Forms.Control)(object)this.btnConnect2).Name = "btnConnect2";
		((System.Windows.Forms.Control)(object)this.btnConnect2).Click += new System.EventHandler(btnConnect2_Click);
		resources.ApplyResources(this.btnConnect, "btnConnect");
		((System.Windows.Forms.Control)(object)this.btnConnect).Name = "btnConnect";
		((System.Windows.Forms.Control)(object)this.btnConnect).Click += new System.EventHandler(btnConnect_Click);
		resources.ApplyResources(this.lblDatabase, "lblDatabase");
		this.lblDatabase.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDatabase).Name = "lblDatabase";
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val10).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val10;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val12).Image = resources.GetObject("appearance12.Image");
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val12;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.tcSync, "tcSync");
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Maroon;
		resources.ApplyResources(val14, "appearance14");
		((AppearanceBase)val14).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.tcSync).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.tcSync).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.tcSync).Controls.Add((System.Windows.Forms.Control)(object)this.tbGeneral);
		((System.Windows.Forms.Control)(object)this.tcSync).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.tcSync).Name = "tcSync";
		((UltraTabControlBase)this.tcSync).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.tcSync).TabOrientation = (TabOrientation)1;
		((KeyedSubObjectBase)val15).Key = "Setting";
		val15.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val15, "ultraTab2");
		((SubObjectBase)val15).ForceApplyResources = "";
		((KeyedSubObjectBase)val16).Key = "Connection";
		val16.TabPage = this.tbGeneral;
		resources.ApplyResources(val16, "ultraTab1");
		((SubObjectBase)val16).ForceApplyResources = "";
		((UltraTabControlBase)this.tcSync).Tabs.AddRange((UltraTab[])(object)new UltraTab[2] { val15, val16 });
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.tcSync);
		base.Name = "frmSyncSettings";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.tcSync, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.chkStopSync).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopSyncFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSyncPeriod).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtStopSyncTo).EndInit();
		((System.Windows.Forms.Control)(object)this.tbGeneral).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.tbGeneral).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtServerName2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtServerName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboDataBase).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUserName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassword).EndInit();
		((System.ComponentModel.ISupportInitialize)this.tcSync).EndInit();
		((System.Windows.Forms.Control)(object)this.tcSync).ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
