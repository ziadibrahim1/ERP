using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Photos;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic.FileIO;

namespace ERP.Photos.Transactions;

public class frmPhotoSelection : frmDetails
{
	private DataSet ds;

	private string stagePath = "";

	private string stageOutDoorPath = "";

	private string SelectionPath = "";

	private DataTable dtInvoices;

	private DataTable dtPhotos;

	private IContainer components = null;

	private OpenFileDialog ofdPicture;

	public frmPhotoSelection()
	{
		InitializeComponent();
		((Control)(object)lblHeader).Text = (GlobalVariables.IsArabic ? "رقم الفاتوره" : "Envoice No");
	}

	public override void PrepareData()
	{
		stagePath = Stages.Select("1", "-1", "0", IsFromServer: false).Rows[0]["FolderPath"].ToString();
		stageOutDoorPath = Stages.Select("1", "-1", "0", IsFromServer: false).Rows[0]["OutdoorFolderPath"].ToString();
		dtInvoices = Invoices.FillComboForSelection(GlobalVariables.CurrentBranchID);
		GlobalFunctions.FillCombo(cboHeader, dtInvoices, "InvoiceID", "InvoiceNoDate");
		dtDetails = InvoicesDetailsCataloge.SelectByInvoiceID_ForSelection("0", GlobalVariables.IsArabic ? "1" : "0");
		dtPhotos = InvoicesDetailsCatalogePhotos.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtDetails);
		ds.Tables.Add(dtPhotos);
		ds.Tables[0].TableName = "dtDetails";
		ds.Tables[1].TableName = "dtPhotos";
		ds.Relations.Add(ds.Tables[0].Columns["InvoiceDetailCatalogeID"], ds.Tables[1].Columns["InvoiceDetailCatalogeID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageName"].Header).Caption = (GlobalVariables.IsArabic ? "العرض" : "Package");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemPackageName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsIndoor"].Header).Caption = (GlobalVariables.IsArabic ? "داخلي" : "Indoor");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsIndoor"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsIndoor"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CadreCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الصور" : "Cadre Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CadreCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CadreCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.11) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "النسخ" : "copies");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.07) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "اعتماد" : "Approve");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.08);
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("Select"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "Select");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Header).Caption = (GlobalVariables.IsArabic ? "اختيار الصور" : "Select Photos");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Width = (int)((double)((Control)(object)ULGData).Width * 0.27);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Select"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Select"].Value = (GlobalVariables.IsArabic ? "اختيار الصور" : "Select Photos");
		}
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoName"].Header).Caption = (GlobalVariables.IsArabic ? "الصوره" : "Photo");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["PhotoName"].Hidden = false;
	}

	public override void DisplayData()
	{
		ds = new DataSet();
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			DateTime dateTime = Convert.ToDateTime(dtInvoices.Rows[cboHeader.SelectedIndex]["EventDate"]);
			SelectionPath = dateTime.Year + "\\" + dateTime.Month + "." + dateTime.Year + "\\" + dateTime.Day + "." + dateTime.Month + "." + dateTime.Year + "\\" + dtInvoices.Rows[cboHeader.SelectedIndex]["InvoiceNo"];
			dtDetails = InvoicesDetailsCataloge.SelectByInvoiceID_ForSelection(((TextEditorControlBase)cboHeader).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			dtPhotos = InvoicesDetailsCatalogePhotos.SelectByInvoiceID("0", GlobalVariables.IsArabic ? "1" : "0");
			ds.Tables.Add(dtDetails);
			ds.Tables.Add(dtPhotos);
			ds.Tables[0].TableName = "dtDetails";
			ds.Tables[1].TableName = "dtPhotos";
			ds.Relations.Add(ds.Tables[0].Columns["InvoiceDetailCatalogeID"], ds.Tables[1].Columns["InvoiceDetailCatalogeID"]);
			((UltraGridBase)ULGData).DataSource = ds;
		}
		InitGrid();
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (Convert.ToInt32(((UltraGridBase)ULGData).Rows[i].Cells["CadreCount"].Value) > ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count && ((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
			{
				GlobalVariables.InformationMB.Show("عدد الصور غير مكتمل  ", "Selected Photos is less than Cadre Count");
				return false;
			}
		}
		return true;
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
				{
					InvoicesDetailsCatalogeStages.SetApprove("1", ((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value.ToString(), "1");
					string text = "";
					string text2 = "";
					if (((UltraGridBase)ULGData).Rows[i].Cells["IsIndoor"].Value.Equals(true))
					{
						text = string.Concat(stagePath, SelectionPath, "\\Selection\\", ((UltraGridBase)ULGData).Rows[i].Cells["ItemCatalogeCode"].Value, "_", ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value);
						text2 = InvoicesDetailsCatalogeStages.SelectNextStage(((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value.ToString()).Rows[0]["FolderPath"].ToString();
					}
					else
					{
						text = string.Concat(stageOutDoorPath, SelectionPath, "\\Selection\\", ((UltraGridBase)ULGData).Rows[i].Cells["ItemCatalogeCode"].Value, "_", ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value);
						text2 = InvoicesDetailsCatalogeStages.SelectNextStage(((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value.ToString()).Rows[0]["OutdoorFolderPath"].ToString();
					}
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
					{
						InvoicesDetailsCatalogePhotos.Insert_Update("-1", ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailID"].Value.ToString(), ((TextEditorControlBase)cboHeader).Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoName"].Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
						File.Copy(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["PhotoName"].Value.ToString(), text + "\\" + ((UltraGridBase)ULGData).Rows[i].Cells["ItemCatalogeCode"].Value.ToString() + " x" + Convert.ToInt32(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value) + " " + ((((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count > 1) ? ("_" + (j + 1)) : "") + ".JPG");
					}
					FileSystem.CopyDirectory(text, string.Concat(text2, dtInvoices.Rows[cboHeader.SelectedIndex]["InvoiceNo"], "\\", ((UltraGridBase)ULGData).Rows[i].Cells["ItemCatalogeCode"].Value, "_", ((UltraGridBase)ULGData).Rows[i].Cells["InvoiceDetailCatalogeID"].Value), overwrite: true);
				}
			}
			Main.EndBulkTrans(FromServer: false);
			DisplayData();
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Notes" && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void btnHeaderSearch_Click(object sender, EventArgs e)
	{
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow == null || ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailCatalogeID"].Value == DBNull.Value)
		{
			return;
		}
		if (Convert.ToInt32(((UltraGridBase)ULGData).ActiveRow.Cells["CadreCount"].Value) <= ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows).Count)
		{
			GlobalVariables.InformationMB.Show("عدد الصور مكتمل  ", "Selection completed");
			return;
		}
		if (((UltraGridBase)ULGData).ActiveRow.Cells["IsIndoor"].Value.Equals(true))
		{
			ofdPicture.InitialDirectory = stagePath + SelectionPath;
		}
		else
		{
			ofdPicture.InitialDirectory = stageOutDoorPath + SelectionPath;
		}
		if (ofdPicture.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		((UltraGridBase)ULGData).ActiveRow.Cells["Approved"].Value = true;
		DataRow dataRow = dtPhotos.NewRow();
		for (int i = 0; i < ofdPicture.FileNames.Length; i++)
		{
			if (dtPhotos.Select("PhotoName='" + ofdPicture.FileNames[i] + "' And InvoiceDetailCatalogeID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailCatalogeID"].Value).Length == 0 && Convert.ToInt32(((UltraGridBase)ULGData).ActiveRow.Cells["CadreCount"].Value) > ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).ActiveRow.ChildBands[0].Rows).Count)
			{
				dataRow = dtPhotos.NewRow();
				dataRow["InvoiceDetailCatalogePhotoID"] = -1;
				dataRow["InvoiceDetailCatalogeID"] = ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailCatalogeID"].Value;
				dataRow["InvoiceDetailID"] = ((UltraGridBase)ULGData).ActiveRow.Cells["InvoiceDetailID"].Value;
				dataRow["InvoiceID"] = ((TextEditorControlBase)cboHeader).Value;
				dataRow["PhotoName"] = ofdPicture.FileNames[i];
				dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
				dtPhotos.Rows.Add(dataRow);
				HasChanges = true;
				((Control)(object)btnCancel).Enabled = true;
				((Control)(object)btnSave).Enabled = true;
				((Control)(object)btnSaveAndClose).Enabled = true;
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
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Photos.Transactions.frmPhotoSelection));
		Appearance val9 = new Appearance();
		this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		base.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		resources.ApplyResources(this.ofdPicture, "ofdPicture");
		this.ofdPicture.Multiselect = true;
		resources.ApplyResources(this, "$this");
		base.Name = "frmPhotoSelection";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
