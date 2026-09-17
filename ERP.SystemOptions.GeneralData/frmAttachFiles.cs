using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmAttachFiles : frmBase
{
	private DataTable dtHistory;

	private string FilesPath = "";

	private string RootPath = "";

	private string VoucherID = "";

	private bool onLine = false;

	private IContainer components = null;

	public UltraGrid ULGData;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraButton btnAddFile;

	public UltraButton btnDeleteAll;

	private OpenFileDialog ofdPicture;

	public UltraButton btnAddFolder;

	public frmAttachFiles()
	{
		InitializeComponent();
	}

	public frmAttachFiles(string Path, string ID, bool canUpdate, bool canDelete)
		: this()
	{
		FilesPath = Path;
		VoucherID = ID;
		((Control)(object)btnDeleteAll).Enabled = (CanDelete = canDelete);
		CanUpdate = canUpdate;
		dtHistory = AttachFiles.SelectByVoucherID(FilesPath, VoucherID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtHistory;
		InitGrid();
	}

	public frmAttachFiles(string Path, string rootPath, string ID, bool canUpdate, bool canDelete)
		: this()
	{
		FilesPath = Path;
		RootPath = rootPath;
		VoucherID = ID;
		((Control)(object)btnDeleteAll).Enabled = (CanDelete = canDelete);
		CanUpdate = canUpdate;
		dtHistory = AttachFiles.SelectByVoucherID(RootPath, VoucherID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtHistory;
		InitGrid();
	}

	public frmAttachFiles(string Path, string rootPath, string ID, bool canUpdate, bool canDelete, bool IsFromServer)
		: this()
	{
		onLine = IsFromServer;
		FilesPath = Path;
		RootPath = rootPath;
		VoucherID = ID;
		((Control)(object)btnDeleteAll).Enabled = (CanDelete = canDelete);
		CanUpdate = canUpdate;
		dtHistory = AttachFiles.SelectByVoucherID(RootPath, VoucherID, GlobalVariables.IsArabic ? "1" : "0", onLine);
		((UltraGridBase)ULGData).DataSource = dtHistory;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AttachFileName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.76) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AttachFileName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم الملف" : "File Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AttachFileName"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Open"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Open");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).Caption = (GlobalVariables.IsArabic ? "فتح" : "Open");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Open"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Open"].Value = (GlobalVariables.IsArabic ? "فتح" : "Open");
		}
		if (CanDelete)
		{
			if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Delete"))
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Delete");
			}
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].Hidden = false;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].Header).Caption = (GlobalVariables.IsArabic ? "حذف" : "Delete");
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].Style = (ColumnStyle)8;
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
			((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Delete"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 2));
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[j].Cells["Delete"].Value = (GlobalVariables.IsArabic ? "حذف" : "Delete");
			}
		}
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)ULGData.ActiveCell).Selected = true;
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnAddFolder_Click(object sender, EventArgs e)
	{
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد إضافة ملفات هذا المجلد بالكامل؟", "Do You Want To Add All The Files Of This Folder?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			return;
		}
		string[] files = Directory.GetFiles(folderBrowserDialog.SelectedPath, "*.*", SearchOption.AllDirectories);
		bool flag = false;
		string[] array = files;
		foreach (string text in array)
		{
			flag = false;
			if (!GlobalFunctions.ArchivingSaveFile(text, FilesPath, Path.GetFileName(text), OverWrite: false) && CanUpdate)
			{
				GlobalVariables.QuestionMB.Show("يوجد ملف بنفس الاسم هل تريد استبداله ؟", "File Exists with the same name. Do You Want to Replace It?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					flag = true;
				}
			}
			if (flag)
			{
				GlobalFunctions.ArchivingSaveFile(text, FilesPath, Path.GetFileName(text), flag);
			}
			if (!flag)
			{
				AttachFiles.Insert_Update("-1", Path.GetFileName(text), FilesPath, VoucherID, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, onLine);
			}
		}
		dtHistory = AttachFiles.SelectByVoucherID((RootPath != "") ? RootPath : FilesPath, VoucherID, GlobalVariables.IsArabic ? "1" : "0", onLine);
		((UltraGridBase)ULGData).DataSource = dtHistory;
		InitGrid();
	}

	private void btnAddFile_Click(object sender, EventArgs e)
	{
		if (GlobalVariables.ArchivingPath == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مسار الارشيف من إعدادات النظام ", "Please set ArchivingPath");
		}
		else
		{
			if (ofdPicture.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			frmFileName frmFileName2 = new frmFileName();
			frmFileName2.WindowState = FormWindowState.Normal;
			frmFileName2.ShowDialog();
			string text = frmFileName2.FileName + Path.GetExtension(ofdPicture.FileName);
			frmFileName2.Dispose();
			bool flag = false;
			if (!GlobalFunctions.ArchivingSaveFile(ofdPicture.FileName, FilesPath, text, OverWrite: false) && CanUpdate)
			{
				GlobalVariables.QuestionMB.Show("يوجد ملف بنفس الاسم هل تريد استبداله ؟", "File Exists with the same name. Do You Want to Replace It?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					flag = true;
				}
			}
			if (flag)
			{
				GlobalFunctions.ArchivingSaveFile(ofdPicture.FileName, FilesPath, text, flag);
			}
			if (!flag)
			{
				AttachFiles.Insert_Update("-1", text, FilesPath, VoucherID, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, onLine);
			}
			dtHistory = AttachFiles.SelectByVoucherID((RootPath != "") ? RootPath : FilesPath, VoucherID, GlobalVariables.IsArabic ? "1" : "0", onLine);
			((UltraGridBase)ULGData).DataSource = dtHistory;
			InitGrid();
		}
	}

	private void btnDeleteAll_Click(object sender, EventArgs e)
	{
		if (GlobalVariables.ArchivingPath == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مسار الارشيف من إعدادات النظام ", "Please set ArchivingPath");
			return;
		}
		GlobalFunctions.ArchivingDeleteDirectory(FilesPath);
		AttachFiles.DeleteByVoucherID(FilesPath, VoucherID, GlobalVariables.UserID, onLine);
		dtHistory.Rows.Clear();
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (GlobalVariables.ArchivingPath == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مسار الارشيف من إعدادات النظام ", "Please set ArchivingPath");
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "Open")
		{
			Process process = new Process();
			process.StartInfo.FileName = GlobalVariables.ArchivingPath + ((UltraGridBase)ULGData).ActiveRow.Cells["AttachFilePath"].Value.ToString() + ((UltraGridBase)ULGData).ActiveRow.Cells["AttachFileName"].Value.ToString();
			process.Start();
		}
		else if (((KeyedSubObjectBase)e.Cell.Column).Key == "Delete")
		{
			GlobalFunctions.ArchivingDeleteFile(((UltraGridBase)ULGData).ActiveRow.Cells["AttachFilePath"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["AttachFileName"].Value.ToString());
			AttachFiles.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["AttachFileID"].Value.ToString(), GlobalVariables.UserID, onLine);
			dtHistory = AttachFiles.SelectByVoucherID((RootPath != "") ? RootPath : FilesPath, VoucherID, GlobalVariables.IsArabic ? "1" : "0", onLine);
			((UltraGridBase)ULGData).DataSource = dtHistory;
			InitGrid();
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Expected O, but got Unknown
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmAttachFiles));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		this.ULGData = new UltraGrid();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnAddFile = new UltraButton();
		this.btnDeleteAll = new UltraButton();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		this.btnAddFolder = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(val, "appearance1");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(34, 62, 110);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(79, 124, 165);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance13");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance13.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnAddFile, "btnAddFile");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnAddFile).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnAddFile).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnAddFile).Name = "btnAddFile";
		((System.Windows.Forms.Control)(object)this.btnAddFile).Click += new System.EventHandler(btnAddFile_Click);
		resources.ApplyResources(this.btnDeleteAll, "btnDeleteAll");
		((AppearanceBase)val4).Image = resources.GetObject("appearance14.Image");
		resources.ApplyResources(val4, "appearance14");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance14.FontData");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.btnDeleteAll).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnDeleteAll).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnDeleteAll).Name = "btnDeleteAll";
		((System.Windows.Forms.Control)(object)this.btnDeleteAll).Click += new System.EventHandler(btnDeleteAll_Click);
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		resources.ApplyResources(this.btnAddFolder, "btnAddFolder");
		((AppearanceBase)val5).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val5, "appearance2");
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance2.FontData");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((ControlBase)this.btnAddFolder).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnAddFolder).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnAddFolder).Name = "btnAddFolder";
		((System.Windows.Forms.Control)(object)this.btnAddFolder).Click += new System.EventHandler(btnAddFolder_Click);
		resources.ApplyResources(this, "$this");
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDeleteAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddFolder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAddFile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Name = "frmAttachFiles";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAddFolder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDeleteAll, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		base.ResumeLayout(false);
	}
}
