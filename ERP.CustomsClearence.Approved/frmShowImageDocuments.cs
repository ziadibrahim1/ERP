using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer.CustomsClearence;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Microsoft.VisualBasic.FileIO;

namespace ERP.CustomsClearence.Approved;

public class frmShowImageDocuments : frmBase
{
	private DataTable dtOperationImageDocuments;

	private string ArchivingDirectoryPath = "";

	private string OperationID = "";

	private string OperationImageDocumentID;

	private byte[] Data;

	private string File;

	public bool Saved = false;

	private string extension;

	private IContainer components = null;

	private UltraPictureBox picShowImage;

	public UltraButton btnSave;

	public UltraButton btnClose;

	public frmShowImageDocuments()
	{
		InitializeComponent();
	}

	public frmShowImageDocuments(string OperationImageDocumentID, string ArchivingDirectoryPath)
		: this()
	{
		this.OperationImageDocumentID = OperationImageDocumentID;
		this.ArchivingDirectoryPath = ArchivingDirectoryPath;
	}

	public override void PrepareData()
	{
		dtOperationImageDocuments = OperationsImageDocuments.Select(OperationImageDocumentID, GlobalVariables.UserID);
		File = dtOperationImageDocuments.Select("OperationImageDocumentID=" + OperationImageDocumentID)[0]["ImageDocument"].ToString();
		extension = GlobalFunctions.GetFileTypeByBase64(File);
		Data = Convert.FromBase64String(File);
		picShowImage.Image = GlobalFunctions.BinaryToImagePNG(Data);
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (bool.Parse(dtOperationImageDocuments.Select("OperationImageDocumentID=" + OperationImageDocumentID)[0]["IsArchived"].ToString()))
		{
			GlobalVariables.QuestionMB.Show("هذه الصور تم حفظها من قبل. هل تريد الحفظ مرة اخرى ؟", "File saved later. Do You Want to Save again ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				SaveImageDocumint();
			}
		}
		else
		{
			SaveImageDocumint();
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		if (!Saved && !bool.Parse(dtOperationImageDocuments.Select("OperationImageDocumentID=" + OperationImageDocumentID)[0]["ISArchived"].ToString()))
		{
			GlobalVariables.QuestionMB.Show("لم يتم تخزين الصورة في الارشيف. هل تريد الحفظ؟", "Image not saved in Archive. Do You Want to Save It?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				SaveImageDocumint();
			}
		}
		Close();
	}

	public void SaveImageDocumint()
	{
		try
		{
			if (!Directory.Exists(GlobalVariables.ArchivingPath))
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال مسار صحيح للأرشيف من إعدادات النظام ", "Please set ArchivingPath");
				return;
			}
			OperationID = dtOperationImageDocuments.Select("OperationImageDocumentID=" + OperationImageDocumentID)[0]["OperationID"].ToString();
			frmFileName frmFileName2 = new frmFileName();
			frmFileName2.WindowState = FormWindowState.Normal;
			frmFileName2.ShowDialog();
			frmFileName2.Dispose();
			string text = ArchivingDirectoryPath + frmFileName2.FileName;
			if (Directory.Exists(GlobalVariables.ArchivingPath + ArchivingDirectoryPath))
			{
				if (FileSystem.FileExists(text))
				{
					GlobalVariables.QuestionMB.Show("يوجد ملف بنفس الاسم هل تريد استبداله ؟", "File Exists with the same name. Do You Want to Replace It?");
					if (GlobalVariables.MessageBoxResult == 'Y')
					{
						FileSystem.DeleteFile(text);
						GlobalFunctions.SaveFileAsBytes(GlobalVariables.ArchivingPath + text + extension, Data);
					}
				}
				else
				{
					GlobalFunctions.SaveFileAsBytes(GlobalVariables.ArchivingPath + text + extension, Data);
					AttachFiles.Insert_Update("-1", frmFileName2.FileName + extension, ArchivingDirectoryPath, OperationID, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
					OperationsImageDocuments.IsArchived(OperationImageDocumentID);
				}
			}
			else
			{
				Directory.CreateDirectory(GlobalVariables.ArchivingPath + ArchivingDirectoryPath);
				GlobalFunctions.SaveFileAsBytes(GlobalVariables.ArchivingPath + text + extension, Data);
				AttachFiles.Insert_Update("-1", frmFileName2.FileName + extension, ArchivingDirectoryPath, OperationID, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
				OperationsImageDocuments.IsArchived(OperationImageDocumentID);
			}
			Saved = true;
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطأ في الحفظ ", "Error!");
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		Appearance val = new Appearance();
		this.picShowImage = new UltraPictureBox();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(739, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 575);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(737, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 575);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(737, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 573);
		((System.Windows.Forms.Control)(object)this.picShowImage).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		((AppearanceBase)val).BackColor = System.Drawing.Color.White;
		this.picShowImage.Appearance = (AppearanceBase)(object)val;
		this.picShowImage.BorderShadowColor = System.Drawing.Color.Empty;
		this.picShowImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.picShowImage).Location = new System.Drawing.Point(-1, 4);
		((System.Windows.Forms.Control)(object)this.picShowImage).Name = "picShowImage";
		((System.Windows.Forms.Control)(object)this.picShowImage).Size = new System.Drawing.Size(740, 530);
		((System.Windows.Forms.Control)(object)this.picShowImage).TabIndex = 512;
		((UltraControlBase)this.picShowImage).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.btnSave).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((UltraButtonBase)this.btnSave).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnSave).Location = new System.Drawing.Point(446, 540);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Size = new System.Drawing.Size(138, 25);
		((System.Windows.Forms.Control)(object)this.btnSave).TabIndex = 538;
		((System.Windows.Forms.Control)(object)this.btnSave).Text = "Save";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		((System.Windows.Forms.Control)(object)this.btnClose).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((UltraButtonBase)this.btnClose).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnClose).Location = new System.Drawing.Point(106, 540);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Size = new System.Drawing.Size(138, 25);
		((System.Windows.Forms.Control)(object)this.btnClose).TabIndex = 538;
		((System.Windows.Forms.Control)(object)this.btnClose).Text = "Close";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 16f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(739, 577);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picShowImage);
		base.Name = "frmShowImageDocuments";
		this.Text = "frmShow";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picShowImage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
