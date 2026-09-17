using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using Infragistics.Documents.Excel;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;

namespace ERP.AbstractForms;

public class frmHeaderDetails : frmButtons
{
	public DataTable dtMaster;

	public DataTable dtDetails;

	public DataRow drMaster;

	public bool CanEditVoucherNumber = true;

	private IContainer components = null;

	public UltraGroupBox UGBDetails;

	public UltraGrid ULGData;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	public UltraButton btnSetting;

	public UltraButton btnAttachFile;

	public frmHeaderDetails()
	{
		InitializeComponent();
		((Control)(object)ULGData).RightToLeft = RightToLeft.No;
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
		UltraButton obj = btnNext;
		UltraButton obj2 = btnPriveous;
		bool flag = (((Control)(object)btnSearch).Enabled = CanSearching);
		bool enabled = (((Control)(object)obj2).Enabled = flag);
		((Control)(object)obj).Enabled = enabled;
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating)
		{
			if (e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			else if (e.KeyValue == 39 && ((Control)(object)btnNext).Enabled && ((Control)(object)btnNext).Visible)
			{
				NextData();
			}
			else if (e.KeyValue == 37 && ((Control)(object)btnPriveous).Enabled && ((Control)(object)btnPriveous).Visible)
			{
				PriveousData();
			}
			else if (e.KeyCode == Keys.F7 && ((Control)(object)btnCopyTo).Enabled && ((Control)(object)btnCopyTo).Visible)
			{
				btnCopyToClick();
			}
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)txtCode).Enabled = NavMode || CanEditVoucherNumber;
		((EditorButtonControlBase)txtCode).ReadOnly = false;
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = NavMode;
		((Control)(object)btnCopyTo).Visible = NavMode;
		((Control)(object)btnPriveous).Visible = NavMode;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void PrepareData()
	{
		base.PrepareData();
		if (!base.DesignMode)
		{
			CanEditVoucherNumber = GlobalFunctions.GetOption("CanEditVoucherNumber");
			DataTable dt = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboTransactionBranch, dt, "BranchID", "BranchName");
		}
	}

	public override void PrepareData2()
	{
		if (base.Tag != null)
		{
			DataTable dataTable = FormSetting.SelectByFormID(((DataRow)base.Tag)["FormID"].ToString(), GlobalVariables.UserID, "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0 && Convert.ToBoolean(dataTable.Rows[0]["bit1"]) && CanSearching && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
			{
				btnSearch_Click(null, null);
			}
			if (dataTable.Rows.Count > 0 && !dataTable.Rows[0]["bit2"].Equals(DBNull.Value) && Convert.ToBoolean(dataTable.Rows[0]["bit2"]) && CanPrint)
			{
				AutoPrint = true;
			}
		}
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		if (((UltraGridBase)ULGData).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		}
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
		}
	}

	public virtual void SelectFullRow(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public virtual void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
	}

	public virtual void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	public virtual void btnCopyToClick()
	{
		if (!CanAdd)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		Adding = true;
		drMaster = null;
		SetControls(NavMode: false);
	}

	public override void btnAddClick()
	{
		drMaster = null;
		base.btnAddClick();
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnUpdateClick();
		}
	}

	public override void btnDeleteClick()
	{
		if (drMaster == null)
		{
			return;
		}
		RowID = drMaster[IDCol].ToString();
		if (!CanDelete)
		{
			GlobalVariables.InformationMB.Show("لا توجد هذه الصلاحية. برجاء الرجوع لمسئول الأمن", "This Privilege is Unavailable.Please check Security Administrator");
			return;
		}
		if (!CanModifyOtherBranch)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفرع آخر", "Cannot Update This Transaction Because It Related to Another Branch ");
			return;
		}
		if (ClosedPeriod)
		{
			GlobalVariables.InformationMB.Show("لا يمكن حذف هذه الحركة لإنها تابعة لفتره مغلقه", "Cannot Delete This Transaction Because It Related to ClosedPeriod ");
			return;
		}
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Are You Sure You want to Delete this Data?");
		DataSaved = true;
		if (GlobalVariables.MessageBoxResult == 'Y')
		{
			DeleteData();
			if (DataSaved)
			{
				FillData();
				drMaster = null;
			}
		}
	}

	public override void btnCancelClick()
	{
		base.btnCancelClick();
		if (Adding)
		{
			drMaster = null;
		}
	}

	public override void btnOKClick()
	{
		((UltraGridBase)ULGData).UpdateData();
		base.btnOKClick();
	}

	public virtual void btnSearch_Click(object sender, EventArgs e)
	{
	}

	public virtual void txtCode_KeyUp(object sender, KeyEventArgs e)
	{
		if (!CanSearching || e.KeyCode != Keys.Return || TableName.Length <= 0 || NoCol.Length <= 0 || ((Control)(object)txtCode).Text.Length <= 0)
		{
			return;
		}
		if (Adding || Updating)
		{
			e.Handled = true;
			SendKeys.Send("{tab}");
			return;
		}
		DataTable comboData = Main.GetComboData(TableName, IDCol, NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 Order by year(" + DateCol + ") Desc");
		if (comboData.Rows.Count > 0)
		{
			RowID = comboData.Rows[0][IDCol].ToString();
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	private void btnPriveous_Click(object sender, EventArgs e)
	{
		PriveousData();
	}

	private void btnNext_Click(object sender, EventArgs e)
	{
		NextData();
	}

	public virtual void PriveousData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 0; i < dtSearchResult.Rows.Count - 1; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i + 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[dtSearchResult.Rows.Count - 1][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "0");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	public virtual void NextData()
	{
		if (dtSearchResult != null && dtSearchResult.Rows.Count > 1)
		{
			if (drMaster != null)
			{
				for (int i = 1; i < dtSearchResult.Rows.Count; i++)
				{
					if (dtSearchResult.Rows[i][IDCol].ToString() == drMaster[IDCol].ToString())
					{
						RowID = dtSearchResult.Rows[i - 1][IDCol].ToString();
						FillData();
						break;
					}
				}
			}
			else
			{
				RowID = dtSearchResult.Rows[0][IDCol].ToString();
				FillData();
			}
		}
		else
		{
			DataTable dataTable = Main.SelectNext(GlobalVariables.BranchIDs, TableName, IDCol, NoCol, DateCol, RowID, "", "1");
			if (dataTable.Rows.Count > 0)
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
			}
			FillData();
		}
	}

	private void btnCopyTo_Click(object sender, EventArgs e)
	{
		btnCopyToClick();
	}

	public virtual void ULGData_Enter(object sender, EventArgs e)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Invalid comparison between Unknown and I4
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Invalid comparison between Unknown and I4
		if (!Adding && !Updating)
		{
			return;
		}
		int num = -1;
		for (int num2 = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1; num2 >= 0; num2--)
		{
			if (num == -1 && !((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num2].Hidden)
			{
				num = num2;
				break;
			}
		}
		if (((int)((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew == 1 || (int)((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew == 6) && Adding)
		{
			((UltraGridBase)ULGData).Rows.TemplateAddRow.Cells[num].Activate();
			ULGData.PerformAction((UltraGridAction)24);
		}
		else if ((Adding || Updating) && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			((UltraGridBase)ULGData).Rows[0].Cells[num].Activate();
			ULGData.PerformAction((UltraGridAction)24);
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Expected O, but got Unknown
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		if (e.KeyCode != Keys.F11 || (!Adding && !Updating))
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

	private void btnSetting_Click(object sender, EventArgs e)
	{
		frmHeaderDetailsSetting frmHeaderDetailsSetting2 = new frmHeaderDetailsSetting((DataRow)base.Tag);
		frmHeaderDetailsSetting2.ShowDialog();
	}

	protected override void ExportGridData()
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "*.xls|*.xlsx";
		saveFileDialog.Title = "Save an Excel File";
		saveFileDialog.FileName = TableName.Substring(TableName.IndexOf("_") + 1);
		if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
		{
			UltraGridExcelExporter ultraGridExcelExporter = new UltraGridExcelExporter();
			((UltraControlBase)ULGData).UseAppStyling = false;
			Workbook workbook = ultraGridExcelExporter.Export(ULGData, saveFileDialog.FileName, WorkbookFormat.Excel2007);
			((UltraControlBase)ULGData).UseAppStyling = true;
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Export Completed Successfully.");
			Process.Start(saveFileDialog.FileName);
		}
	}

	public override void ImportGridData()
	{
		if (!Adding)
		{
			return;
		}
		string empty = string.Empty;
		string empty2 = string.Empty;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "*.xls|*.xlsx";
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		empty = openFileDialog.FileName;
		empty2 = Path.GetExtension(empty);
		int val = int.MaxValue;
		int num = int.MinValue;
		int num2 = int.MaxValue;
		int num3 = int.MinValue;
		Workbook workbook = Workbook.Load(empty);
		foreach (WorksheetRow item in (IEnumerable<WorksheetRow>)workbook.Worksheets[0].Rows)
		{
			foreach (WorksheetCell item2 in (IEnumerable<WorksheetCell>)item.Cells)
			{
				if (item2.Value != null)
				{
					val = Math.Min(val, item2.RowIndex);
					num = Math.Max(num, item2.RowIndex);
					num2 = Math.Min(num2, item2.ColumnIndex);
					num3 = Math.Max(num3, item2.ColumnIndex);
				}
			}
		}
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			dictionary.Add(((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Header).Caption, ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key);
		}
		try
		{
			for (int j = 1; j <= num; j++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				for (int k = num2; k <= num3; k++)
				{
					string key = workbook.Worksheets[0].Rows[0].Cells[k].Value.ToString();
					((UltraGridBase)ULGData).ActiveRow.Cells[dictionary[key]].Value = workbook.Worksheets[0].Rows[j].Cells[k].Value;
				}
				((UltraGridBase)ULGData).UpdateData();
			}
		}
		catch (Exception)
		{
			throw;
		}
		finally
		{
		}
	}

	private void btnAttachFile_Click(object sender, EventArgs e)
	{
		btnAttachFileClick();
	}

	public virtual void btnAttachFileClick()
	{
		if (RowID != "")
		{
			DataRow dataRow = null;
			if (base.Tag != null)
			{
				dataRow = (DataRow)base.Tag;
			}
			else if (GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'").Length != 0)
			{
				dataRow = GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'")[0];
			}
			bool option = GlobalFunctions.GetOption("ArchivingInEnglish");
			string text = dataRow[option ? "FormNameEn" : "FormNameAr"].ToString();
			string text2 = GlobalVariables.dtForms.Select("formID=" + dataRow["ParentID"])[0]["ParentID"].ToString();
			string text3 = GlobalVariables.dtForms.Select("formID=" + text2)[0][option ? "FormNameEn" : "FormNameAr"].ToString();
			string text4 = text3 + "\\" + text + "\\" + ((Control)(object)txtCode).Text.Replace('\\', '-').Replace('/', '-').Replace('*', '-')
				.Replace('?', '-')
				.Replace('؟', '-')
				.Replace(':', '-')
				.Replace('<', '-')
				.Replace('>', '-')
				.Replace('"', '-') + "\\";
			string path = text4 + RowID + "\\";
			frmAttachFiles frmAttachFiles2 = new frmAttachFiles(path, text4, RowID, ((Control)(object)btnUpdate).Enabled, ((Control)(object)btnDelete).Enabled);
			frmAttachFiles2.ShowDialog();
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
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmHeaderDetails));
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
		Appearance val15 = new Appearance();
		Appearance val16 = new Appearance();
		Appearance val17 = new Appearance();
		Appearance val18 = new Appearance();
		Appearance val19 = new Appearance();
		Appearance val20 = new Appearance();
		Appearance val21 = new Appearance();
		this.UGBDetails = new UltraGroupBox();
		this.ULGData = new UltraGrid();
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.btnSetting = new UltraButton();
		this.btnAttachFile = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnUpdate, "btnUpdate");
		resources.ApplyResources(base.btnDelete, "btnDelete");
		resources.ApplyResources(base.btnPrint, "btnPrint");
		resources.ApplyResources(base.btnOK, "btnOK");
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSaveClose, "btnSaveClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		((AppearanceBase)val3).TextTrimming = (TextTrimming)6;
		this.UGBDetails.Appearance = (AppearanceBase)(object)val3;
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val4).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val4).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val5;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val7;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val8).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val8;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val9).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val9;
		((AppearanceBase)val10).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val10).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val11).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val11).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val11).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(SelectFullRow);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).Enter += new System.EventHandler(ULGData_Enter);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		((AppearanceBase)val14).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val14;
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		((AppearanceBase)val15).Image = ERP.Properties.Resources.BarLeft;
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val15;
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		((AppearanceBase)val16).Image = ERP.Properties.Resources.BarRight;
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val16;
		resources.ApplyResources(this.btnNext, "btnNext");
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val18;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		((AppearanceBase)val19).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val19).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val19).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val19, "appearance19");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val19;
		resources.ApplyResources(this.lblCode, "lblCode");
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.btnSetting, "btnSetting");
		((AppearanceBase)val20).Image = ERP.Properties.Resources.Update;
		((AppearanceBase)val20).ImageHAlign = (HAlign)2;
		((AppearanceBase)val20).ImageVAlign = (VAlign)2;
		((ControlBase)this.btnSetting).Appearance = (AppearanceBase)(object)val20;
		((ControlBase)this.btnSetting).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnSetting).Name = "btnSetting";
		((System.Windows.Forms.Control)(object)this.btnSetting).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSetting).Click += new System.EventHandler(btnSetting_Click);
		resources.ApplyResources(this.btnAttachFile, "btnAttachFile");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.Attach;
		((ControlBase)this.btnAttachFile).Appearance = (AppearanceBase)(object)val21;
		((ControlBase)this.btnAttachFile).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Name = "btnAttachFile";
		((System.Windows.Forms.Control)(object)this.btnAttachFile).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Click += new System.EventHandler(btnAttachFile_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAttachFile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSetting);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmHeaderDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
