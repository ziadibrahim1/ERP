using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer.CustomsClearence;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.CustomsClearence.Transactions;
using ERP.SystemOptions.GeneralData;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic.FileIO;

namespace ERP.CustomsClearence.Approved;

public class frmRevisingOperations : frmPosted
{
	private DataTable dtDetailsOperations;

	private DataTable dtOperationImageDocuments;

	private DataSet ds;

	private byte[] DataFile;

	private string StrFile;

	private string tempLocation = "Documents";

	private string extension;

	private IContainer components = null;

	public frmRevisingOperations()
	{
		InitializeComponent();
		NoCol = "OperationNo";
	}

	public override void FillGrid()
	{
		dtsource = Operations.SelectByRevised(GlobalVariables.CurrentBranchID, "0", GlobalVariables.IsArabic ? "1" : "0");
		dtDetailsOperations = OperationsImageDocuments.SelectForRevising(GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtsource);
		ds.Tables.Add(dtDetailsOperations);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtDetailsOperations";
		ds.Relations.Add(ds.Tables[0].Columns["OperationID"], ds.Tables[1].Columns["OperationID"]);
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الاذن " : "Operation No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الاذن" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["OperationDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم العميل" : "Client Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Header).Caption = (GlobalVariables.IsArabic ? "اسم المصدر" : "Exporter Name");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.09);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "الفرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateDate"].Header).Caption = (GlobalVariables.IsArabic ? "ناريخ الشهادة" : "Certificate Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompass"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompass"].Header).Caption = (GlobalVariables.IsArabic ? "بوصلة" : "Compass");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsCompass"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الشهادة" : "Certificate No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CertificateNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.075);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Revised"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Revised"].Header).Caption = "";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Revised"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Revised"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Revised"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Revised"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OperationID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OperationImageDocumentID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Description"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsArchived"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OperationImageDocumentID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الصورة" : "ImageNo");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "التفاصيل" : "Description");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsArchived"].Header).Caption = (GlobalVariables.IsArabic ? "أرشيف" : "Archived");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["OperationImageDocumentID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Description"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["IsArchived"].Hidden = false;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns).Exists("Show"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns.Insert(0, "Show");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].Header).Caption = (GlobalVariables.IsArabic ? "عرض" : "Show");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Show"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns).Count - 1));
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns).Exists("Save"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns.Insert(0, "Save");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].Header).Caption = (GlobalVariables.IsArabic ? "حفظ" : "Save");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Save"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Show"].Value = (GlobalVariables.IsArabic ? "عرض" : "Show");
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Save"].Value = (GlobalVariables.IsArabic ? "حفظ" : "Save");
			}
		}
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Revised")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!((UltraGridBase)ULGData).Rows[i].Cells["Revised"].Value.Equals(true))
			{
				continue;
			}
			if (((UltraGridBase)ULGData).Rows[i].ChildBands.HasChildRows)
			{
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["IsArchived"].Value.ToString()))
					{
						GlobalVariables.QuestionMB.Show("هناك صور لم يتم حفظها في العملية رقم  " + ((UltraGridBase)ULGData).Rows[i].Cells["OperationNo"].Value.ToString() + " هل تريد حفظها؟", "There are Images not saved in Archive in Operation No " + ((UltraGridBase)ULGData).Rows[i].Cells["OperationNo"].Value.ToString() + ". Do You Want to Save It?");
						if (GlobalVariables.MessageBoxResult == 'Y')
						{
							((GridItemBase)((UltraGridBase)ULGData).Rows[i].ChildBands.FirstRow).Selected = true;
							return;
						}
					}
				}
			}
			text = text + ((UltraGridBase)ULGData).Rows[i].Cells["OperationID"].Value.ToString() + ",";
		}
		if (text != ",")
		{
			Operations.SetRevise("1", text, GlobalVariables.UserID);
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد عمليات للمراجعة " : "There are No Operations to Revise");
		}
	}

	public override void ClickCellButton()
	{
		if ((((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Show" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Save") && ((UltraGridBase)ULGData).ActiveRow.Cells["OperationImageDocumentID"].Value.ToString().Length > 0)
		{
			string text = GlobalVariables.dtForms.Select("FormFullName='ERP.CustomsClearence.Transactions.frmOperations'")[0][GlobalFunctions.GetOption("ArchivingInEnglish") ? "FormNameEn" : "FormNameAr"].ToString();
			string text2 = GlobalVariables.dtForms.Select("FormFullName='CustomsClearence'")[0][GlobalFunctions.GetOption("ArchivingInEnglish") ? "FormNameEn" : "FormNameAr"].ToString();
			string text3 = ((UltraGridBase)ULGData).ActiveRow.Cells["OperationImageDocumentID"].Value.ToString();
			string text4 = text2 + "\\" + text + "\\" + ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["OperationNo"].Text.Replace('\\', '-').Replace('/', '-').Replace('*', '-')
				.Replace('?', '-')
				.Replace('؟', '-')
				.Replace(':', '-')
				.Replace('<', '-')
				.Replace('>', '-')
				.Replace('"', '-') + "\\";
			string text5 = text4 + ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["OperationID"].Value.ToString() + "\\";
			dtOperationImageDocuments = OperationsImageDocuments.Select(text3, GlobalVariables.UserID);
			StrFile = dtOperationImageDocuments.Select("OperationImageDocumentID=" + text3)[0]["ImageDocument"].ToString();
			DataFile = Convert.FromBase64String(StrFile);
			extension = GlobalFunctions.GetFileTypeByBase64(StrFile);
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Show")
			{
				if (!Directory.Exists(tempLocation))
				{
					Directory.CreateDirectory(tempLocation + text5);
				}
				if (File.Exists(tempLocation + text5 + text3 + extension))
				{
					File.Delete(tempLocation + text5 + text3 + extension);
				}
				GlobalFunctions.SaveFileAsBytes(tempLocation + text5 + text3 + extension, DataFile);
				Process.Start(tempLocation + text5 + text3 + extension);
			}
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Save")
			{
				SaveImageDocumint(text5);
			}
		}
		if (((UltraGridBase)ULGData).ActiveRow != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Open")
		{
			frmOperations frmOperations2 = new frmOperations(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["OperationID"].Value.ToString()));
			frmOperations2.Size = new Size(base.Width, base.Height);
			frmOperations2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmOperations2.lblTitle).Text = (GlobalVariables.IsArabic ? "العمليات" : "Operations");
			frmOperations2.ShowDialog();
		}
	}

	public void SaveImageDocumint(string ArchivingDirectoryPath)
	{
		try
		{
			if (!Directory.Exists(GlobalVariables.ArchivingPath))
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال مسار صحيح للأرشيف من إعدادات النظام ", "Please set ArchivingPath");
				return;
			}
			string voucherID = ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["OperationID"].Value.ToString();
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
						GlobalFunctions.SaveFileAsBytes(GlobalVariables.ArchivingPath + text + extension, DataFile);
					}
				}
				else
				{
					GlobalFunctions.SaveFileAsBytes(GlobalVariables.ArchivingPath + text + extension, DataFile);
					AttachFiles.Insert_Update("-1", frmFileName2.FileName + extension, ArchivingDirectoryPath, voucherID, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
					OperationsImageDocuments.IsArchived(((UltraGridBase)ULGData).ActiveRow.Cells["OperationImageDocumentID"].Value.ToString());
				}
			}
			else
			{
				Directory.CreateDirectory(GlobalVariables.ArchivingPath + ArchivingDirectoryPath);
				GlobalFunctions.SaveFileAsBytes(GlobalVariables.ArchivingPath + text + extension, DataFile);
				AttachFiles.Insert_Update("-1", frmFileName2.FileName + extension, ArchivingDirectoryPath, voucherID, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
				OperationsImageDocuments.IsArchived(((UltraGridBase)ULGData).ActiveRow.Cells["OperationImageDocumentID"].Value.ToString());
			}
			((UltraGridBase)ULGData).ActiveRow.Cells["IsArchived"].Value = 1;
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطأ في الحفظ ", "Error!");
		}
	}

	public override void AfterSelectChange()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
	}

	public override void Search()
	{
		DataTable dataTable = SearchFunctions.CSTOperationsReport(-1, 0, IsFromServer: false);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (dataTable.Rows[i]["OperationID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["OperationID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[j].Cells["Revised"].Value = true;
				}
			}
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.Approved.frmRevisingOperations));
		Appearance val6 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorStyle = (HeaderStyle)1;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		((AppearanceBase)val6).FontData.Name = resources.GetString("resource.Name");
		resources.ApplyResources(val6, "appearance6");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(this, "$this");
		base.Name = "frmRevisingOperations";
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
