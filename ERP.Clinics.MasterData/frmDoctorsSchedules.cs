using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Clinics;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Clinics.MasterData;

public class frmDoctorsSchedules : frmDetails
{
	private DataTable dtDoctors = new DataTable();

	private DataTable dtClinics = new DataTable();

	private ValueList vlClinics = new ValueList();

	private IContainer components = null;

	private UltraLabel lblClinics;

	private UltraComboEditor cboClinics;

	public frmDoctorsSchedules()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtDoctors = Doctors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboHeader, dtDoctors, "DoctorID", "DoctorName");
		dtClinics = BusinessLayer.Clinics.Clinics.FillCombo("-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboClinics, dtClinics, "ClinicID", "ClinicName");
		dtDetails = DoctorsSchedules.SelectByDoctorIDClinicID("0", "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
		if (dtClinics.Rows.Count == 1)
		{
			cboClinics.SelectedIndex = 0;
		}
		if (dtDoctors.Rows.Count == 1)
		{
			cboHeader.SelectedIndex = 0;
		}
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اليوم" : "Day");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "-" : "-");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DayNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromTime"].Header).Caption = (GlobalVariables.IsArabic ? "من" : "From");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromTime"].MaskInput = "hh:mm tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromTime"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromTime"].EditorComponent).DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToTime"].Header).Caption = (GlobalVariables.IsArabic ? "الى" : "To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToTime"].MaskInput = "hh:mm tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToTime"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToTime"].EditorComponent).DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxDiagnoseCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الحالات" : "Diagnose Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxDiagnoseCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MaxDiagnoseCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1 && cboClinics.SelectedIndex > -1)
		{
			dtDetails = DoctorsSchedules.SelectByDoctorIDClinicID(((TextEditorControlBase)cboHeader).Value.ToString(), ((TextEditorControlBase)cboClinics).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			if (dtDetails.Rows.Count == 0)
			{
				fillDetails();
			}
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
		}
	}

	public void fillDetails()
	{
		DataRow dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "السبت";
		dataRow["DayNameEn"] = "Saturday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
		dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "الاحد";
		dataRow["DayNameEn"] = "Sunday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
		dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "الاثنين";
		dataRow["DayNameEn"] = "Monday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
		dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "الثلاثاء";
		dataRow["DayNameEn"] = "Tuesday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
		dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "الاربعاء";
		dataRow["DayNameEn"] = "Wednesday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
		dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "الخميس";
		dataRow["DayNameEn"] = "Thursday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
		dataRow = dtDetails.NewRow();
		dataRow["DoctorScheduleID"] = -1;
		dataRow["DayNameAr"] = "الجمعة";
		dataRow["DayNameEn"] = "Friday";
		dataRow["DoctorID"] = ((TextEditorControlBase)cboHeader).Value;
		dataRow["ClinicID"] = ((TextEditorControlBase)cboClinics).Value;
		dataRow["MaxDiagnoseCount"] = 0;
		dataRow["BranchID"] = GlobalVariables.CurrentBranchID;
		dtDetails.Rows.Add(dataRow);
	}

	public override void SaveData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			DoctorsSchedules.Insert_UpdateByTable(dtDetails, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			SaveError = true;
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DayNameAr" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "DayNameEn")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private bool CheckAllPeriodClosed()
	{
		bool result = true;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!bool.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Closed"].Value.ToString()))
			{
				return false;
			}
		}
		return result;
	}

	private void cboClinics_ValueChanged(object sender, EventArgs e)
	{
		DisplayData();
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Clinics.MasterData.frmDoctorsSchedules));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblClinics = new UltraLabel();
		this.cboClinics = new UltraComboEditor();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinics).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.btnClose, "btnClose");
		resources.ApplyResources(base.btnSave, "btnSave");
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val, "appearance1");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val2, "appearance2");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val3, "appearance3");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val4, "appearance4");
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val5, "appearance5");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val6, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val7, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.cboHeader).Appearance = (AppearanceBase)(object)val8;
		base.cboHeader.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboHeader.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.lblHeader, "lblHeader");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		resources.ApplyResources(val9, "appearance10");
		((ControlBase)base.lblHeader).Appearance = (AppearanceBase)(object)val9;
		((UltraControlBase)base.lblHeader).UseAppStyling = false;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnSaveAndClose, "btnSaveAndClose");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblClinics, "lblClinics");
		this.lblClinics.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClinics).Name = "lblClinics";
		((ControlBase)this.lblClinics).WrapText = false;
		resources.ApplyResources(this.cboClinics, "cboClinics");
		((TextEditorControlBase)this.cboClinics).AlwaysInEditMode = true;
		this.cboClinics.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboClinics).Name = "cboClinics";
		((TextEditorControlBase)this.cboClinics).ValueChanged += new System.EventHandler(cboClinics_ValueChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClinics);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboClinics);
		base.Name = "frmDoctorsSchedules";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboClinics, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClinics, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboClinics).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
