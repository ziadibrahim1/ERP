using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace ERP.AbstractForms;

public class frmHeaderManyDetails : frmButtons
{
	public DataTable dtMaster;

	public DataTable dtDetails;

	public DataRow drMaster;

	public bool CanEditVoucherNumber = true;

	private IContainer components = null;

	public UltraLabel lblTitle2;

	public UltraTabControl UTCDetails;

	private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

	public UltraGrid ULGData;

	public UltraTabPageControl ultraTabPageControl1;

	public UltraButton btnSetting;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	public UltraButton btnAttachFile;

	public frmHeaderManyDetails()
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
		((Control)(object)btnPriveous).Visible = NavMode;
		((Control)(object)btnCopyTo).Visible = NavMode;
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

	protected override void ExportGridData()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		if (((UltraTabControlBase)UTCDetails).ActiveTab == null || ((UltraTabControlBase)UTCDetails).ActiveTab.Index <= -1)
		{
			return;
		}
		foreach (object control in ((Control)(object)((UltraTabControlBase)UTCDetails).ActiveTab.TabPage).Controls)
		{
			if (control is UltraGrid)
			{
				UltraGrid val = (UltraGrid)control;
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "*.xls|*.xlsx";
				saveFileDialog.Title = "Save an Excel File";
				saveFileDialog.FileName = TableName.Substring(TableName.IndexOf("_") + 1);
				if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
				{
					UltraGridExcelExporter ultraGridExcelExporter = new UltraGridExcelExporter();
					((UltraControlBase)val).UseAppStyling = false;
					Workbook workbook = ultraGridExcelExporter.Export(val, saveFileDialog.FileName, WorkbookFormat.Excel2007);
					((UltraControlBase)val).UseAppStyling = true;
					GlobalVariables.InformationMB.Show("تم الحفظ بنجاح", "Export Completed Successfully.");
					Process.Start(saveFileDialog.FileName);
				}
				break;
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

	private void UTCDetails_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
	{
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
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnDeleteClick();
			drMaster = null;
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
		if (!CanSearching)
		{
			return;
		}
		if (e.KeyCode == Keys.Return && TableName.Length > 0 && NoCol.Length > 0 && ((Control)(object)txtCode).Text.Length > 0)
		{
			if (Adding || Updating)
			{
				e.Handled = true;
				SendKeys.Send("{tab}");
				return;
			}
			DataTable comboData = Main.GetComboData(TableName, "*", NoCol + "=''" + ((Control)(object)txtCode).Text.Trim() + "'' And Deleted=0 Order by year(" + DateCol + ") Desc");
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
		if (e.KeyCode == Keys.F8)
		{
			btnSearch_Click(null, null);
		}
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
			frmValueListSearch frmValueListSearch3 = new frmValueListSearch((ValueList)ULGData.ActiveCell.Column.ValueList, ((object)ULGData.ActiveCell.Column.Header).ToString());
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
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Expected O, but got Unknown
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Expected O, but got Unknown
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmHeaderManyDetails));
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
		UltraTab val20 = new UltraTab();
		Appearance val21 = new Appearance();
		Appearance val22 = new Appearance();
		this.ultraTabPageControl1 = new UltraTabPageControl();
		this.ULGData = new UltraGrid();
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.UTCDetails = new UltraTabControl();
		this.ultraTabSharedControlsPage1 = new UltraTabSharedControlsPage();
		this.btnCopyTo = new UltraButton();
		this.btnSetting = new UltraButton();
		this.btnAttachFile = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).SuspendLayout();
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
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		resources.ApplyResources(this.ultraTabPageControl1, "ultraTabPageControl1");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).Name = "ultraTabPageControl1";
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val3).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val3).BorderColor = System.Drawing.SystemColors.Window;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val4;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).ForeColor = System.Drawing.SystemColors.GrayText;
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val6;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val7).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val9).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val9;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val10).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val12;
		resources.ApplyResources(this.ULGData, "ULGData");
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(SelectFullRow);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).Enter += new System.EventHandler(ULGData_Enter);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		((AppearanceBase)val13).Image = ERP.Properties.Resources.search;
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val13;
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		((AppearanceBase)val14).Image = ERP.Properties.Resources.BarLeft;
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val14;
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		((AppearanceBase)val15).Image = ERP.Properties.Resources.BarRight;
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val15;
		resources.ApplyResources(this.btnNext, "btnNext");
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val16).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val16).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val17).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		((AppearanceBase)val18).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val18).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val18).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val18;
		resources.ApplyResources(this.lblCode, "lblCode");
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.UTCDetails, "UTCDetails");
		((AppearanceBase)val19).TextTrimming = (TextTrimming)6;
		((UltraTabControlBase)this.UTCDetails).Appearance = (AppearanceBase)(object)val19;
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl1);
		((System.Windows.Forms.Control)(object)this.UTCDetails).Name = "UTCDetails";
		((UltraTabControlBase)this.UTCDetails).SharedControlsPage = this.ultraTabSharedControlsPage1;
		((UltraTabControlBase)this.UTCDetails).TabOrientation = (TabOrientation)2;
		((KeyedSubObjectBase)val20).Key = "Details";
		val20.TabPage = this.ultraTabPageControl1;
		resources.ApplyResources(val20, "ultraTab2");
		((SubObjectBase)val20).ForceApplyResources = "";
		((UltraTabControlBase)this.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val20 });
		((UltraTabControlBase)this.UTCDetails).SelectedTabChanged += new SelectedTabChangedEventHandler(UTCDetails_SelectedTabChanged);
		resources.ApplyResources(this.ultraTabSharedControlsPage1, "ultraTabSharedControlsPage1");
		((System.Windows.Forms.Control)(object)this.ultraTabSharedControlsPage1).Name = "ultraTabSharedControlsPage1";
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.btnSetting, "btnSetting");
		((AppearanceBase)val21).Image = ERP.Properties.Resources.Update;
		((AppearanceBase)val21).ImageHAlign = (HAlign)2;
		((AppearanceBase)val21).ImageVAlign = (VAlign)2;
		((ControlBase)this.btnSetting).Appearance = (AppearanceBase)(object)val21;
		((ControlBase)this.btnSetting).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnSetting).Name = "btnSetting";
		((System.Windows.Forms.Control)(object)this.btnSetting).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnSetting).Click += new System.EventHandler(btnSetting_Click);
		resources.ApplyResources(this.btnAttachFile, "btnAttachFile");
		((AppearanceBase)val22).Image = ERP.Properties.Resources.Attach;
		((ControlBase)this.btnAttachFile).Appearance = (AppearanceBase)(object)val22;
		((ControlBase)this.btnAttachFile).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Name = "btnAttachFile";
		((System.Windows.Forms.Control)(object)this.btnAttachFile).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Click += new System.EventHandler(btnAttachFile_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAttachFile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSetting);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UTCDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmHeaderManyDetails";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAttachFile, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UTCDetails).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
