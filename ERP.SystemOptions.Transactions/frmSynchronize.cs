using System;
using System.ComponentModel;
using System.Data;
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

namespace ERP.SystemOptions.Transactions;

public class frmSynchronize : frmBase
{
	private IContainer components = null;

	private UltraButton btnSync;

	public UltraButton btnKeyboard;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	private ProgressBar prgSMS;

	public frmSynchronize()
	{
		InitializeComponent();
	}

	private void btnSync_Click(object sender, EventArgs e)
	{
		if (!SyncConnection.SetUnderSync())
		{
			GlobalVariables.InformationMB.Show("لا يمكن اتمام العمليه الان برجاء إعادة المحاوله بعد قليل ", "Another Operation on process please try again later");
			return;
		}
		try
		{
			((UltraControlBase)btnSync).UseAppStyling = false;
			((ControlBase)btnSync).Appearance.BackColor = Color.Red;
			((Control)(object)btnSync).Enabled = false;
			Sync();
			((Control)(object)btnSync).Enabled = true;
			((UltraControlBase)btnSync).UseAppStyling = true;
			GlobalVariables.InformationMB.Show("تمت عملية التزامن بنجاح", "Synchronization Complete");
		}
		catch
		{
		}
	}

	public void Sync()
	{
		try
		{
			string text = "";
			Synchronization.ServerR = GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString();
			Synchronization.DataBaseR = GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString();
			Synchronization.UserIDR = GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString();
			Synchronization.PasswordR = GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString();
			text = GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].ToString();
			if (!Synchronization.CreateConnectionL() || !Synchronization.CreateConnectionR())
			{
				Synchronization.EndUnderSync();
				return;
			}
			DataTable dataTable = Synchronization.ExecuteQuery_DataTableLocal(" select * From Sync_Tables t order by SyncOrder ");
			prgSMS.Value = 0;
			prgSMS.Step = 1;
			prgSMS.Maximum = dataTable.Rows.Count + 8;
			Synchronization.MoveDataL(text);
			prgSMS.PerformStep();
			Synchronization.MoveDataR(text);
			prgSMS.PerformStep();
			Synchronization.DeleteFromRemote(text);
			prgSMS.PerformStep();
			Synchronization.DeleteFromLocal(text);
			prgSMS.PerformStep();
			DataTable dataTable2 = Synchronization.ExecuteQuery_DataTableLocal(" select * From Sync_Tables t where t.TableName in(select Distinct TableName from Sync_Transactions where BranchID=" + text + " AND State<>3 )  order by SyncOrder ");
			prgSMS.PerformStep();
			DataTable dataTable3 = Synchronization.ExecuteQuery_DataTableRemote(" select * From Sync_Tables t where t.TableName in(select Distinct TableName from Sync_Transactions where BranchID=" + text + " AND State<>3 )  order by SyncOrder ");
			prgSMS.PerformStep();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text2 = dataTable.Rows[i]["TableName"].ToString();
				if (dataTable2.Select("TableName='" + text2 + "'").Length != 0 && Convert.ToBoolean(dataTable.Rows[i]["ToServer"]))
				{
					Synchronization.InsertUpdateToRemote(text2, text);
				}
				if (dataTable3.Select("TableName='" + text2 + "'").Length != 0 && Convert.ToBoolean(dataTable.Rows[i]["FromServer"]))
				{
					Synchronization.InsertUpdateToLocal(text2, text);
				}
				prgSMS.PerformStep();
			}
			Synchronization.ActualDeleteFromRemote();
			prgSMS.PerformStep();
			Synchronization.ActualDeleteFromLocal(text);
			prgSMS.PerformStep();
			Synchronization.ExecuteQuery_DataTableNewConnectionLocal(" SC_ItemsTransactions_Management ");
			Synchronization.ExecuteQuery_DataTableNewConnectionRemote(" SC_ItemsTransactions_Management ");
		}
		catch
		{
		}
		Synchronization.EndUnderSync();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.Transactions.frmSynchronize));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.btnSync = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.prgSMS = new System.Windows.Forms.ProgressBar();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnSync, "btnSync");
		((System.Windows.Forms.Control)(object)this.btnSync).Name = "btnSync";
		((System.Windows.Forms.Control)(object)this.btnSync).Click += new System.EventHandler(btnSync_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance2.FontData");
		resources.ApplyResources(val, "appearance2");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance18.Image");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance18.FontData");
		resources.ApplyResources(val2, "appearance18");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance1");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance1.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance17");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance17.FontData");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.prgSMS, "prgSMS");
		this.prgSMS.Maximum = 1000;
		this.prgSMS.Name = "prgSMS";
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.prgSMS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSync);
		base.Name = "frmSynchronize";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSync, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex(this.prgSMS, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
