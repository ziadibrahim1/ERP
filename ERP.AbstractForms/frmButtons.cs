using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Defaults;
using BusinessLayer.Privilege;
using BusinessLayer.Security;
using CrystalDecisions.CrystalReports.Engine;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using ERP.Ticketing;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.AbstractForms;

public class frmButtons : frmBase
{
	public bool AutoPrint;

	public bool DataSaved = true;

	private IContainer components = null;

	public UltraButton btnAdd;

	public UltraButton btnUpdate;

	public UltraButton btnDelete;

	public UltraButton btnPrint;

	public UltraButton btnOK;

	public UltraButton btnCancel;

	public UltraButton btnRefreshData;

	public UltraButton btnClose;

	public UltraButton btnSaveClose;

	public UltraButton btnKeyboard;

	public UltraLabel lblHistory;

	public UltraComboEditor cboTransactionBranch;

	public UltraButton btnOpenTicket;

	public UltraButton btnImport;

	public UltraButton btnExport;

	public frmButtons()
	{
		InitializeComponent();
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (e.KeyCode == Keys.F1 && ((Control)(object)btnAdd).Enabled && ((Control)(object)btnAdd).Visible)
		{
			btnAddClick();
		}
		else if (e.KeyCode == Keys.F2 && ((Control)(object)btnUpdate).Enabled && ((Control)(object)btnUpdate).Visible)
		{
			btnUpdateClick();
		}
		else if (e.KeyCode == Keys.F3 && ((Control)(object)btnDelete).Enabled && ((Control)(object)btnDelete).Visible)
		{
			btnDeleteClick();
		}
		else if (e.KeyCode == Keys.F4 && ((Control)(object)btnPrint).Enabled && ((Control)(object)btnPrint).Visible)
		{
			btnPrint_Click(null, null);
		}
		else if (e.KeyCode == Keys.F5 && ((Control)(object)btnRefreshData).Enabled && ((Control)(object)btnRefreshData).Visible)
		{
			btnRefreshDataClick();
		}
	}

	public override void CallRefrashButton(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F5)
		{
			btnRefreshDataClick();
		}
		if (e.KeyCode == Keys.F1)
		{
			btnOKClick();
		}
		if (e.KeyCode == Keys.F2)
		{
			btnSaveClose_Click(null, null);
		}
		if (e.KeyCode == Keys.F3)
		{
			btnCancel_Click(null, null);
		}
	}

	public virtual void btnAddClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		ClearControls();
		SetControls(NavMode: false);
	}

	public virtual void btnUpdateClick()
	{
		if (!CanUpdate)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن تعديل هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Update This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		Updating = true;
		SetControls(NavMode: false);
	}

	public virtual void btnDeleteClick()
	{
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Delete This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DataSaved = true;
			DeleteData();
			if (DataSaved)
			{
				FillData();
			}
		}
	}

	public virtual void btnOKClick()
	{
		((Control)(object)btnOK).Focus();
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		Main.TicketReq = false;
		if (Adding)
		{
			try
			{
				AddData();
				if (AutoPrint && RowID != "" && DataSaved)
				{
					try
					{
						GlobalVariables.ReportDocument = new ReportDocument();
						btnPrintClick();
					}
					catch (Exception ex)
					{
						if (ex.Message == "Load report failed.")
						{
							GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalid Reports Path \r\n Please Check Reports Path from System Tools");
						}
					}
				}
			}
			catch
			{
				DataSaved = false;
				Main.TicketReq = true;
			}
			if (DataSaved)
			{
				btnAddClick();
			}
		}
		else
		{
			try
			{
				UpdateData();
			}
			catch
			{
				DataSaved = false;
				Main.TicketReq = true;
			}
			if (DataSaved)
			{
				Updating = false;
				SetControls(NavMode: true);
				if (RowID != "" && TableName != "")
				{
					UsersTransactions.DeleteByRowID(TableName, RowID);
				}
				FillData();
			}
		}
		if (Main.TicketReq && MessageBox.Show(GlobalVariables.IsArabic ? "حدث خطأ. هل تريد إنشاء طلب دعم؟" : "Error occurred. Do you want to create a support ticket?", GlobalVariables.IsArabic ? "خطأ" : "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: true, isMessage: false, isFormQst: false, base.Name, Main.strMessageDetail.Replace("'", "\""), bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
	}

	public virtual void btnCancelClick()
	{
		if (Updating && RowID != "" && TableName != "")
		{
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID, TableName, RowID);
		}
		Adding = false;
		Updating = false;
		FillData();
		SetControls(NavMode: true);
	}

	public virtual void SetControls(bool NavMode)
	{
		((Control)(object)btnAdd).Visible = NavMode;
		((Control)(object)btnUpdate).Visible = NavMode;
		((Control)(object)btnDelete).Visible = NavMode;
		((Control)(object)btnPrint).Visible = NavMode;
		((Control)(object)btnClose).Visible = NavMode;
		((Control)(object)btnRefreshData).Visible = !NavMode;
		((Control)(object)btnOK).Visible = !NavMode;
		((Control)(object)btnSaveClose).Visible = !NavMode;
		((Control)(object)btnCancel).Visible = !NavMode;
		base.CancelButton = (IButtonControl)(NavMode ? btnClose : btnCancel);
		base.AcceptButton = null;
	}

	public virtual void FillData()
	{
	}

	public virtual void DisplayData()
	{
		if (TableName != "" && RowID != "")
		{
			((Control)(object)lblHistory).Text = Trans_Log.GetRowHistory(TableName, RowID, GlobalVariables.IsArabic ? "1" : "0");
			DisplayDataDate = GlobalFunctions.GetServerDateTimeNow();
		}
	}

	public virtual void ClearControls()
	{
		((Control)(object)lblHistory).Text = "";
	}

	public virtual void AddData()
	{
	}

	public virtual void UpdateData()
	{
	}

	public virtual void DeleteData()
	{
	}

	public virtual void btnPrintClick()
	{
		if (!CanPrint)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
		}
	}

	public virtual void btnRefreshDataClick()
	{
	}

	public virtual bool ValidateData()
	{
		return true;
	}

	public virtual void SetSecurity()
	{
		if (TableName != "" && Main.IsSynchronization)
		{
			CanEditFromServer = SyncConnection.CanEditFromServerByTableName(TableName, GlobalVariables.CurrentBranchID);
		}
		CanAdd = CanEditFromServer && CanAdd;
		CanDelete = CanEditFromServer && CanDelete;
		CanUpdate = CanEditFromServer && CanUpdate;
		((Control)(object)btnAdd).Enabled = CanAdd;
		((Control)(object)btnUpdate).Enabled = CanUpdate;
		((Control)(object)btnDelete).Enabled = CanDelete;
		((Control)(object)btnPrint).Enabled = CanPrint;
	}

	private void frmButtons_Load(object sender, EventArgs e)
	{
		SetSecurity();
		SetControls(NavMode: true);
		Adding = false;
		Updating = false;
		FillData();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAdd_Click(object sender, EventArgs e)
	{
		btnAddClick();
	}

	public void btnUpdate_Click(object sender, EventArgs e)
	{
		if (RowID != "" && TableName != "")
		{
			if (DisplayDataDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), RowID, TableName))
			{
				GlobalVariables.InformationMB.Show("يوجد تعديل فى البيانات برجاء تنشيط البيانات", "Data Has Been Modified Please Refresh Your Data ");
				return;
			}
			DataTable dataTable = UsersTransactions.CheckTransaction(TableName, RowID.ToString());
			if (dataTable.Rows.Count > 0)
			{
				if (!(dataTable.Rows[0]["User_ID"].ToString() == GlobalVariables.UserID))
				{
					GlobalVariables.InformationMB.Show("لا يمكن تعديل هذا البيان. هذا البيان مستخدم حاليا\u064b من المستخدم \r\n" + dataTable.Rows[0]["UserNameAr"].ToString(), "Can not Update this Data. This data is now open by user \r\n" + dataTable.Rows[0]["UserNameEn"].ToString());
					return;
				}
				GlobalVariables.QuestionMB.Show(" هذا البيان مستخدم حاليا\u064b من المستخدم \r\n" + dataTable.Rows[0]["UserNameAr"].ToString() + "\r\nهل تريد اخراجه؟", "This data is now open by user \r\n" + dataTable.Rows[0]["UserNameEn"].ToString() + "\r\nDo You want to clear it ?");
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					return;
				}
				UsersTransactions.DeleteByRowID(TableName, RowID);
			}
		}
		btnUpdateClick();
		if (Updating && RowID != "" && TableName != "")
		{
			UsersTransactions.Insert_Update("-1", GlobalVariables.UserLoginID, TableName, RowID);
		}
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		if (RowID != "" && TableName != "")
		{
			if (DisplayDataDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), RowID, TableName))
			{
				GlobalVariables.InformationMB.Show("يوجد تعديل فى البيانات برجاء تنشيط البيانات", "Data Has Been Modified Please Refresh Your Data ");
				return;
			}
			DataTable dataTable = UsersTransactions.CheckTransaction(TableName, RowID.ToString());
			if (dataTable.Rows.Count > 0)
			{
				if (!dataTable.Rows[0]["User_ID"].Equals(GlobalVariables.UserID))
				{
					GlobalVariables.InformationMB.Show("لا يمكن حذف هذا البيان. هذا البيان مستخدم حاليا\u064b من المستخدم \r\n" + dataTable.Rows[0]["UserNameAr"].ToString(), "Can not Update this Data. This data is now open by user \r\n" + dataTable.Rows[0]["UserNameEn"].ToString());
					return;
				}
				GlobalVariables.QuestionMB.Show(" هذا البيان مستخدم حاليا\u064b من المستخدم \r\n" + dataTable.Rows[0]["UserNameAr"].ToString() + "\r\nهل تريد اخراجه؟", "This data is now open by user \r\n" + dataTable.Rows[0]["UserNameEn"].ToString() + "\r\nDo You want to clear it ?");
				if (GlobalVariables.MessageBoxResult != 'Y')
				{
					return;
				}
				UsersTransactions.DeleteByRowID(TableName, RowID);
			}
		}
		btnDeleteClick();
	}

	public void btnPrint_Click(object sender, EventArgs e)
	{
		try
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			btnPrintClick();
		}
		catch (Exception ex)
		{
			if (ex.Message == "Load report failed.")
			{
				GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalid Reports Path \r\n Please Check Reports Path from System Tools");
			}
		}
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		if (Updating && RowID != "" && TableName != "")
		{
			if (UsersTransactions.CheckNotKicked(GlobalVariables.UserLoginID, TableName, RowID).Rows.Count == 0)
			{
				GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لقد تم إخراجك من قبل مستخدم اخر ", "You Have Been Kicked By Another User");
				return;
			}
			if (DisplayDataDate.HasValue && Trans_Log.HasModificationCheckByDate(Convert.ToDateTime(DisplayDataDate).ToString(GlobalVariables.DateLongFormateMS), RowID, TableName))
			{
				GlobalVariables.InformationMB.Show("لا يمكن حفظ التعديلات لوجود تعديل فى البيانات من قبل مستخدم اخر ", "Data Has Been Modified by another User ");
				return;
			}
		}
		btnOKClick();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		if ((!Updating && !Adding) || MessageBox.Show(GlobalVariables.IsArabic ? "هل تريد الالغاء  ؟" : "Do you Want to Cancel ?", GlobalVariables.IsArabic ? "خطأ" : "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) != DialogResult.No)
		{
			btnCancelClick();
		}
	}

	private void btnRefreshData_Click(object sender, EventArgs e)
	{
		btnRefreshDataClick();
	}

	public virtual void btnSaveClose_Click(object sender, EventArgs e)
	{
		((Control)(object)btnSaveClose).Focus();
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		if (Adding)
		{
			AddData();
			if (AutoPrint && RowID != "")
			{
				try
				{
					GlobalVariables.ReportDocument = new ReportDocument();
					btnPrintClick();
				}
				catch (Exception ex)
				{
					if (ex.Message == "Load report failed.")
					{
						GlobalVariables.InformationMB.Show("مسار التقارير غير سليم \r\n برجاء مراجعة مسار التقارير من إعدادات النظام", "Invalid Reports Path \r\n Please Check Reports Path from System Tools");
					}
				}
			}
		}
		else
		{
			UpdateData();
			if (DataSaved && RowID != "" && TableName != "")
			{
				UsersTransactions.DeleteByRowID(TableName, RowID);
			}
		}
		if (DataSaved)
		{
			btnClose_Click(null, null);
		}
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
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

	protected virtual void ExportGridData()
	{
	}

	private void btnExport_Click(object sender, EventArgs e)
	{
		try
		{
			ExportGridData();
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.Message, ex.Message);
		}
	}

	public void btnImport_Click(object sender, EventArgs e)
	{
		ImportGridData();
	}

	public virtual void ImportGridData()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmButtons));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.btnAdd = new UltraButton();
		this.btnUpdate = new UltraButton();
		this.btnDelete = new UltraButton();
		this.btnPrint = new UltraButton();
		this.btnClose = new UltraButton();
		this.btnOK = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnRefreshData = new UltraButton();
		this.btnSaveClose = new UltraButton();
		this.btnKeyboard = new UltraButton();
		this.lblHistory = new UltraLabel();
		this.cboTransactionBranch = new UltraComboEditor();
		this.btnOpenTicket = new UltraButton();
		this.btnImport = new UltraButton();
		this.btnExport = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransactionBranch).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnAdd, "btnAdd");
		((System.Windows.Forms.Control)(object)this.btnAdd).Name = "btnAdd";
		((System.Windows.Forms.Control)(object)this.btnAdd).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnAdd).Click += new System.EventHandler(btnAdd_Click);
		resources.ApplyResources(this.btnUpdate, "btnUpdate");
		((System.Windows.Forms.Control)(object)this.btnUpdate).Name = "btnUpdate";
		((System.Windows.Forms.Control)(object)this.btnUpdate).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnUpdate).Click += new System.EventHandler(btnUpdate_Click);
		resources.ApplyResources(this.btnDelete, "btnDelete");
		((System.Windows.Forms.Control)(object)this.btnDelete).Name = "btnDelete";
		((System.Windows.Forms.Control)(object)this.btnDelete).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnDelete).Click += new System.EventHandler(btnDelete_Click);
		resources.ApplyResources(this.btnPrint, "btnPrint");
		((System.Windows.Forms.Control)(object)this.btnPrint).Name = "btnPrint";
		((System.Windows.Forms.Control)(object)this.btnPrint).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnPrint).Click += new System.EventHandler(btnPrint_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.btnOK, "btnOK");
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(btnOK_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnRefreshData, "btnRefreshData");
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Name = "btnRefreshData";
		((System.Windows.Forms.Control)(object)this.btnRefreshData).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnRefreshData).Click += new System.EventHandler(btnRefreshData_Click);
		resources.ApplyResources(this.btnSaveClose, "btnSaveClose");
		((System.Windows.Forms.Control)(object)this.btnSaveClose).Name = "btnSaveClose";
		((System.Windows.Forms.Control)(object)this.btnSaveClose).TabStop = false;
		((ControlBase)this.btnSaveClose).WrapText = false;
		((System.Windows.Forms.Control)(object)this.btnSaveClose).Click += new System.EventHandler(btnSaveClose_Click);
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.lblHistory, "lblHistory");
		((System.Windows.Forms.Control)(object)this.lblHistory).Cursor = System.Windows.Forms.Cursors.Hand;
		((System.Windows.Forms.Control)(object)this.lblHistory).Name = "lblHistory";
		((System.Windows.Forms.Control)(object)this.lblHistory).Click += new System.EventHandler(lblHistory_Click);
		resources.ApplyResources(this.cboTransactionBranch, "cboTransactionBranch");
		this.cboTransactionBranch.AutoCompleteMode = (AutoCompleteMode)2;
		this.cboTransactionBranch.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.cboTransactionBranch).Name = "cboTransactionBranch";
		((TextEditorControlBase)this.cboTransactionBranch).Nullable = false;
		((EditorButtonControlBase)this.cboTransactionBranch).ReadOnly = true;
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		resources.ApplyResources(this.btnImport, "btnImport");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnImport).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnImport).Name = "btnImport";
		((System.Windows.Forms.Control)(object)this.btnImport).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnImport).Click += new System.EventHandler(btnImport_Click);
		resources.ApplyResources(this.btnExport, "btnExport");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		((ControlBase)this.btnExport).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnExport).Name = "btnExport";
		((System.Windows.Forms.Control)(object)this.btnExport).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnExport).Click += new System.EventHandler(btnExport_Click);
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnExport);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnImport);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTransactionBranch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHistory);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUpdate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAdd);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrint);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSaveClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefreshData);
		base.Name = "frmButtons";
		base.Load += new System.EventHandler(frmButtons_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnExport, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTransactionBranch).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
