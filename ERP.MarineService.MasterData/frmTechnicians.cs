using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.MarineService;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.MarineService.MasterData;

public class frmTechnicians : frmButtons
{
	private DataTable dtCountries;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtNationality;

	private DataTable dtTitle;

	private DataRow drMaster;

	private bool CanEditVoucherNumber = true;

	private decimal Technician = default(decimal);

	public decimal TechnicianID = default(decimal);

	public string TechnicianName = "";

	private IContainer components = null;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblIDIssueDate;

	private UltraDateTimeEditor dtpIDIssueDate;

	private UltraLabel lblIDNo;

	private UltraTextEditor txtIDNo;

	private UltraLabel lblCDCExpireDate;

	private UltraDateTimeEditor dtpCDCExpireDate;

	private UltraLabel lblCDCIssueDate;

	private UltraDateTimeEditor dtpCDCIssueDate;

	private UltraLabel lblCDCNo;

	private UltraTextEditor txtCDCNo;

	private UltraLabel lblPassportExpireDate;

	private UltraDateTimeEditor dtpPassportExpireDate;

	private UltraLabel lblPassportIssueDate;

	private UltraDateTimeEditor dtpPassportIssueDate;

	private UltraLabel lblPassportNo;

	private UltraTextEditor txtPassportNo;

	private UltraComboEditor cboNationality;

	private UltraLabel lblNationality;

	private UltraComboEditor cboCountry;

	private UltraLabel lblBirthDate;

	private UltraLabel lblCountry;

	private UltraLabel lblClintSupplierTitle;

	private UltraComboEditor cboTitle;

	private UltraLabel lblMobile2;

	private UltraLabel lblReorder;

	private UltraTextEditor txtMobile2;

	private UltraComboEditor cboArea;

	private UltraTextEditor txtMobile;

	private UltraLabel lblArea;

	private UltraLabel lblMin;

	private UltraTextEditor txtFax;

	private UltraComboEditor cboCity;

	private UltraLabel lblMax;

	private UltraTextEditor txtTel;

	private UltraLabel lblEMail;

	private UltraLabel lblCity;

	private UltraTextEditor txtEMail;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblAddress;

	private UltraLabel lblNotes;

	private UltraTextEditor txtNotes;

	private UltraTextEditor txtAddress;

	public frmTechnicians()
	{
		InitializeComponent();
		TableName = "MS_Technicians";
		NoCol = "TechnicianNo";
		IDCol = "TechnicianID";
		DateCol = "GetDate()";
	}

	public frmTechnicians(decimal TechnicianID)
		: this()
	{
		Technician = TechnicianID;
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
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = NavMode;
		((Control)(object)btnCopyTo).Visible = NavMode;
		((Control)(object)btnPriveous).Visible = NavMode;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCDCExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpCDCIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpIDIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpPassportExpireDate).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpPassportIssueDate).ReadOnly = NavMode;
		((EditorButtonControlBase)cboTitle).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCountry).ReadOnly = NavMode;
		((EditorButtonControlBase)cboCity).ReadOnly = NavMode;
		((EditorButtonControlBase)cboArea).ReadOnly = NavMode;
		((EditorButtonControlBase)cboNationality).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAddress).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCDCNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFax).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile2).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPassportNo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTel).ReadOnly = NavMode;
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtTitle = Titles.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTitle, dtTitle, "TitleID", GlobalVariables.IsArabic ? "TitleNameAr" : "TitleNameEn");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Technicians.GetCode(IsFromServer: false) : "");
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		dtpBirthDate.Value = null;
		dtpCDCExpireDate.Value = null;
		dtpCDCIssueDate.Value = null;
		dtpIDIssueDate.Value = null;
		dtpPassportExpireDate.Value = null;
		dtpPassportIssueDate.Value = null;
		cboTitle.SelectedIndex = -1;
		cboCountry.SelectedIndex = -1;
		cboCity.SelectedIndex = -1;
		cboArea.SelectedIndex = -1;
		cboNationality.SelectedIndex = -1;
		((TextEditorControlBase)txtAddress).Clear();
		((TextEditorControlBase)txtCDCNo).Clear();
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtFax).Clear();
		((TextEditorControlBase)txtIDNo).Clear();
		((TextEditorControlBase)txtMobile).Clear();
		((TextEditorControlBase)txtMobile2).Clear();
		((TextEditorControlBase)txtNotes).Clear();
		((TextEditorControlBase)txtPassportNo).Clear();
		((TextEditorControlBase)txtTel).Clear();
	}

	public override void FillData()
	{
		if (Technician == -1m)
		{
			drMaster = null;
			btnAddClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (Technician > 0m || Technician < -1m)
		{
			DataTable dataTable = Technicians.Select(Technician.ToString(), GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
			}
			else
			{
				drMaster = null;
			}
			btnUpdateClick();
			((Control)(object)btnOK).Visible = false;
		}
		else if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable2 = Technicians.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable2.Rows.Count > 0)
			{
				drMaster = dataTable2.Rows[0];
			}
			else
			{
				drMaster = null;
			}
		}
		DisplayData();
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (drMaster != null)
		{
			((TextEditorControlBase)cboTransactionBranch).Value = drMaster["BranchID"];
			((Control)(object)txtCode).Text = drMaster["TechnicianNo"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["TechnicianNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["TechnicianNameEn"].ToString();
			((Control)(object)txtAddress).Text = drMaster["Address"].ToString();
			((Control)(object)txtCDCNo).Text = drMaster["CDCNo"].ToString();
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((Control)(object)txtFax).Text = drMaster["Fax"].ToString();
			((Control)(object)txtIDNo).Text = drMaster["IDNo"].ToString();
			((Control)(object)txtMobile).Text = drMaster["Mobile"].ToString();
			((Control)(object)txtMobile2).Text = drMaster["Mobile2"].ToString();
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			((Control)(object)txtPassportNo).Text = drMaster["PassportNo"].ToString();
			((Control)(object)txtTel).Text = drMaster["Tel"].ToString();
			dtpBirthDate.Value = drMaster["BirthDate"];
			dtpCDCExpireDate.Value = drMaster["CDCExpireDate"];
			dtpCDCIssueDate.Value = drMaster["CDCIssueDate"];
			dtpIDIssueDate.Value = drMaster["IDIssueDate"];
			dtpPassportExpireDate.Value = drMaster["PassportExpireDate"];
			dtpPassportIssueDate.Value = drMaster["PassportIssueDate"];
			((TextEditorControlBase)cboTitle).Value = drMaster["TitleID"];
			((TextEditorControlBase)cboCountry).Value = drMaster["CountryID"];
			((TextEditorControlBase)cboCity).Value = drMaster["CityID"];
			((TextEditorControlBase)cboArea).Value = drMaster["AreaID"];
			((TextEditorControlBase)cboNationality).Value = drMaster["NationalityID"];
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم العميل", "Please Enter The Technician Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter The Technician Arabic Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("MS_Technicians", "TechnicianNameAr", ((Control)(object)txtNameAr).Text, Adding ? "0" : drMaster["TechnicianNameAr"].ToString(), IsFromServer: false) > 0)
		{
			GlobalVariables.InformationMB.Show("هذا الاسم متواجد من قبل", "Patient Name Already Exists");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("dbo.MS_Technicians", "TechnicianNo", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["TechnicianNo"].ToString(), IsFromServer: false) > 0)
		{
			string code = Technicians.GetCode(IsFromServer: false);
			GlobalVariables.QuestionMB.Show("رقم هذا الملف متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Profile Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		try
		{
			TechnicianID = Technicians.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, (dtpPassportIssueDate.Value == null) ? "Null" : dtpPassportIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, (dtpCDCIssueDate.Value == null) ? "Null" : dtpCDCIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpCDCExpireDate.Value == null) ? "Null" : dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtMobile2).Text, ((Control)(object)txtEMail).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			TechnicianName = (GlobalVariables.IsArabic ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text);
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void UpdateData()
	{
		try
		{
			TechnicianID = Technicians.Insert_Update(drMaster["TechnicianID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (cboTitle.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboTitle).Value.ToString(), (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), (cboCountry.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCountry).Value.ToString(), (cboCity.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboCity).Value.ToString(), (cboArea.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboArea).Value.ToString(), (cboNationality.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboNationality).Value.ToString(), ((Control)(object)txtPassportNo).Text, (dtpPassportIssueDate.Value == null) ? "Null" : dtpPassportIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpPassportExpireDate.Value == null) ? "Null" : dtpPassportExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtCDCNo).Text, (dtpCDCIssueDate.Value == null) ? "Null" : dtpCDCIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), (dtpCDCExpireDate.Value == null) ? "Null" : dtpCDCExpireDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtIDNo).Text, (dtpIDIssueDate.Value == null) ? "Null" : dtpIDIssueDate.DateTime.ToString(GlobalVariables.DateShortFormate), ((Control)(object)txtAddress).Text, ((Control)(object)txtTel).Text, ((Control)(object)txtFax).Text, ((Control)(object)txtMobile).Text, ((Control)(object)txtMobile2).Text, ((Control)(object)txtEMail).Text, ((Control)(object)txtNotes).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			TechnicianName = (GlobalVariables.IsArabic ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text);
		}
		catch
		{
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void DeleteData()
	{
		try
		{
			Technicians.Delete(drMaster["TechnicianID"].ToString(), GlobalVariables.UserID, IsFromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
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
		base.btnOKClick();
	}

	public override void btnRefreshDataClick()
	{
		dtNationality = Nationalities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboNationality, dtNationality, "NationalityID", "NationalityName");
		dtTitle = Titles.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		GlobalFunctions.FillCombo(cboTitle, dtTitle, "TitleID", GlobalVariables.IsArabic ? "TitleNameAr" : "TitleNameEn");
		dtCountries = Countries.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCountry, dtCountries, "CountryID", "CountryName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboCity, dtCities, "CityID", "CityName");
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboArea, dtAreas, "AreaID", "AreaName");
	}

	public virtual void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.TechniciansSearchReport(IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["TechnicianID"].ToString();
			FillData();
		}
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

	private void cboCountry_ValueChanged(object sender, EventArgs e)
	{
		if (cboCountry.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtCities);
			dataView.RowFilter = "CountryID=" + ((TextEditorControlBase)cboCountry).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboCity.DataSource = dataView;
			cboCity.DisplayMember = "CityName";
			cboCity.ValueMember = "CityID";
		}
	}

	private void cboCity_ValueChanged(object sender, EventArgs e)
	{
		if (cboCity.SelectedIndex != -1)
		{
			DataView dataView = new DataView(dtAreas);
			dataView.RowFilter = "CityID=" + ((TextEditorControlBase)cboCity).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboArea.DataSource = dataView;
			cboArea.DisplayMember = "AreaName";
			cboArea.ValueMember = "AreaID";
		}
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Expected O, but got Unknown
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Expected O, but got Unknown
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Expected O, but got Unknown
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected O, but got Unknown
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Expected O, but got Unknown
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Expected O, but got Unknown
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Expected O, but got Unknown
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Expected O, but got Unknown
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Expected O, but got Unknown
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Expected O, but got Unknown
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Expected O, but got Unknown
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.MarineService.MasterData.frmTechnicians));
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
		Appearance val22 = new Appearance();
		Appearance val23 = new Appearance();
		Appearance val24 = new Appearance();
		Appearance val25 = new Appearance();
		Appearance val26 = new Appearance();
		Appearance val27 = new Appearance();
		Appearance val28 = new Appearance();
		Appearance val29 = new Appearance();
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.lblNameAr = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblIDIssueDate = new UltraLabel();
		this.dtpIDIssueDate = new UltraDateTimeEditor();
		this.lblIDNo = new UltraLabel();
		this.txtIDNo = new UltraTextEditor();
		this.lblCDCExpireDate = new UltraLabel();
		this.dtpCDCExpireDate = new UltraDateTimeEditor();
		this.lblCDCIssueDate = new UltraLabel();
		this.dtpCDCIssueDate = new UltraDateTimeEditor();
		this.lblCDCNo = new UltraLabel();
		this.txtCDCNo = new UltraTextEditor();
		this.lblPassportExpireDate = new UltraLabel();
		this.dtpPassportExpireDate = new UltraDateTimeEditor();
		this.lblPassportIssueDate = new UltraLabel();
		this.dtpPassportIssueDate = new UltraDateTimeEditor();
		this.lblPassportNo = new UltraLabel();
		this.txtPassportNo = new UltraTextEditor();
		this.cboNationality = new UltraComboEditor();
		this.lblNationality = new UltraLabel();
		this.cboCountry = new UltraComboEditor();
		this.lblBirthDate = new UltraLabel();
		this.lblCountry = new UltraLabel();
		this.lblClintSupplierTitle = new UltraLabel();
		this.cboTitle = new UltraComboEditor();
		this.lblMobile2 = new UltraLabel();
		this.lblReorder = new UltraLabel();
		this.txtMobile2 = new UltraTextEditor();
		this.cboArea = new UltraComboEditor();
		this.txtMobile = new UltraTextEditor();
		this.lblArea = new UltraLabel();
		this.lblMin = new UltraLabel();
		this.txtFax = new UltraTextEditor();
		this.cboCity = new UltraComboEditor();
		this.lblMax = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.lblCity = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblAddress = new UltraLabel();
		this.lblNotes = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.txtAddress = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportIssueDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).BeginInit();
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
		resources.ApplyResources(base.lblHistory, "lblHistory");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val, "appearance30");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val2).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val2).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val2).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val2).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val2).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val2;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		((System.Windows.Forms.Control)(object)this.txtCode).KeyUp += new System.Windows.Forms.KeyEventHandler(txtCode_KeyUp);
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance31");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance32");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		((System.Windows.Forms.Control)(object)this.btnPriveous).Click += new System.EventHandler(btnPriveous_Click);
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance33");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance34");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance35");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance36");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Click += new System.EventHandler(btnCopyTo_Click);
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblIDIssueDate, "lblIDIssueDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance37");
		((ControlBase)this.lblIDIssueDate).Appearance = (AppearanceBase)(object)val9;
		this.lblIDIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDIssueDate).Name = "lblIDIssueDate";
		((ControlBase)this.lblIDIssueDate).WrapText = false;
		resources.ApplyResources(this.dtpIDIssueDate, "dtpIDIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpIDIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpIDIssueDate).Name = "dtpIDIssueDate";
		resources.ApplyResources(this.lblIDNo, "lblIDNo");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance38");
		((ControlBase)this.lblIDNo).Appearance = (AppearanceBase)(object)val10;
		this.lblIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIDNo).Name = "lblIDNo";
		((ControlBase)this.lblIDNo).WrapText = false;
		resources.ApplyResources(this.txtIDNo, "txtIDNo");
		((System.Windows.Forms.Control)(object)this.txtIDNo).Name = "txtIDNo";
		resources.ApplyResources(this.lblCDCExpireDate, "lblCDCExpireDate");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance39");
		((ControlBase)this.lblCDCExpireDate).Appearance = (AppearanceBase)(object)val11;
		this.lblCDCExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCExpireDate).Name = "lblCDCExpireDate";
		((ControlBase)this.lblCDCExpireDate).WrapText = false;
		resources.ApplyResources(this.dtpCDCExpireDate, "dtpCDCExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpCDCExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate).Name = "dtpCDCExpireDate";
		resources.ApplyResources(this.lblCDCIssueDate, "lblCDCIssueDate");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance40");
		((ControlBase)this.lblCDCIssueDate).Appearance = (AppearanceBase)(object)val12;
		this.lblCDCIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCIssueDate).Name = "lblCDCIssueDate";
		((ControlBase)this.lblCDCIssueDate).WrapText = false;
		resources.ApplyResources(this.dtpCDCIssueDate, "dtpCDCIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpCDCIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpCDCIssueDate).Name = "dtpCDCIssueDate";
		resources.ApplyResources(this.lblCDCNo, "lblCDCNo");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance41");
		((ControlBase)this.lblCDCNo).Appearance = (AppearanceBase)(object)val13;
		this.lblCDCNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCDCNo).Name = "lblCDCNo";
		((ControlBase)this.lblCDCNo).WrapText = false;
		resources.ApplyResources(this.txtCDCNo, "txtCDCNo");
		((System.Windows.Forms.Control)(object)this.txtCDCNo).Name = "txtCDCNo";
		resources.ApplyResources(this.lblPassportExpireDate, "lblPassportExpireDate");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val14, "appearance42");
		((ControlBase)this.lblPassportExpireDate).Appearance = (AppearanceBase)(object)val14;
		this.lblPassportExpireDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportExpireDate).Name = "lblPassportExpireDate";
		((ControlBase)this.lblPassportExpireDate).WrapText = false;
		resources.ApplyResources(this.dtpPassportExpireDate, "dtpPassportExpireDate");
		((UltraWinEditorMaskedControlBase)this.dtpPassportExpireDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate).Name = "dtpPassportExpireDate";
		resources.ApplyResources(this.lblPassportIssueDate, "lblPassportIssueDate");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val15).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val15, "appearance43");
		((ControlBase)this.lblPassportIssueDate).Appearance = (AppearanceBase)(object)val15;
		this.lblPassportIssueDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportIssueDate).Name = "lblPassportIssueDate";
		((ControlBase)this.lblPassportIssueDate).WrapText = false;
		resources.ApplyResources(this.dtpPassportIssueDate, "dtpPassportIssueDate");
		((UltraWinEditorMaskedControlBase)this.dtpPassportIssueDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpPassportIssueDate).Name = "dtpPassportIssueDate";
		resources.ApplyResources(this.lblPassportNo, "lblPassportNo");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val16).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val16, "appearance44");
		((ControlBase)this.lblPassportNo).Appearance = (AppearanceBase)(object)val16;
		this.lblPassportNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPassportNo).Name = "lblPassportNo";
		((ControlBase)this.lblPassportNo).WrapText = false;
		resources.ApplyResources(this.txtPassportNo, "txtPassportNo");
		((System.Windows.Forms.Control)(object)this.txtPassportNo).Name = "txtPassportNo";
		resources.ApplyResources(this.cboNationality, "cboNationality");
		this.cboNationality.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboNationality).Name = "cboNationality";
		resources.ApplyResources(this.lblNationality, "lblNationality");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val17).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val17, "appearance45");
		((ControlBase)this.lblNationality).Appearance = (AppearanceBase)(object)val17;
		this.lblNationality.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNationality).Name = "lblNationality";
		((ControlBase)this.lblNationality).WrapText = false;
		resources.ApplyResources(this.cboCountry, "cboCountry");
		this.cboCountry.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCountry).Name = "cboCountry";
		((TextEditorControlBase)this.cboCountry).ValueChanged += new System.EventHandler(cboCountry_ValueChanged);
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((AppearanceBase)val18).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val18).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val18, "appearance46");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val18;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.lblCountry, "lblCountry");
		((AppearanceBase)val19).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val19).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val19, "appearance47");
		((ControlBase)this.lblCountry).Appearance = (AppearanceBase)(object)val19;
		this.lblCountry.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCountry).Name = "lblCountry";
		((ControlBase)this.lblCountry).WrapText = false;
		resources.ApplyResources(this.lblClintSupplierTitle, "lblClintSupplierTitle");
		((AppearanceBase)val20).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val20).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val20, "appearance48");
		((ControlBase)this.lblClintSupplierTitle).Appearance = (AppearanceBase)(object)val20;
		this.lblClintSupplierTitle.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle).Name = "lblClintSupplierTitle";
		((ControlBase)this.lblClintSupplierTitle).WrapText = false;
		resources.ApplyResources(this.cboTitle, "cboTitle");
		this.cboTitle.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboTitle).Name = "cboTitle";
		resources.ApplyResources(this.lblMobile2, "lblMobile2");
		((AppearanceBase)val21).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val21).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val21, "appearance49");
		((ControlBase)this.lblMobile2).Appearance = (AppearanceBase)(object)val21;
		this.lblMobile2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMobile2).Name = "lblMobile2";
		((ControlBase)this.lblMobile2).WrapText = false;
		resources.ApplyResources(this.lblReorder, "lblReorder");
		((AppearanceBase)val22).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val22).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val22, "appearance50");
		((ControlBase)this.lblReorder).Appearance = (AppearanceBase)(object)val22;
		this.lblReorder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReorder).Name = "lblReorder";
		((ControlBase)this.lblReorder).WrapText = false;
		resources.ApplyResources(this.txtMobile2, "txtMobile2");
		((System.Windows.Forms.Control)(object)this.txtMobile2).Name = "txtMobile2";
		((System.Windows.Forms.Control)(object)this.txtMobile2).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.cboArea, "cboArea");
		this.cboArea.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboArea).Name = "cboArea";
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		((System.Windows.Forms.Control)(object)this.txtMobile).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblArea, "lblArea");
		((AppearanceBase)val23).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val23).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val23, "appearance51");
		((ControlBase)this.lblArea).Appearance = (AppearanceBase)(object)val23;
		this.lblArea.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArea).Name = "lblArea";
		((ControlBase)this.lblArea).WrapText = false;
		resources.ApplyResources(this.lblMin, "lblMin");
		((AppearanceBase)val24).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val24).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val24, "appearance52");
		((ControlBase)this.lblMin).Appearance = (AppearanceBase)(object)val24;
		this.lblMin.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMin).Name = "lblMin";
		((ControlBase)this.lblMin).WrapText = false;
		resources.ApplyResources(this.txtFax, "txtFax");
		((System.Windows.Forms.Control)(object)this.txtFax).Name = "txtFax";
		((System.Windows.Forms.Control)(object)this.txtFax).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.cboCity, "cboCity");
		this.cboCity.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboCity).Name = "cboCity";
		((TextEditorControlBase)this.cboCity).ValueChanged += new System.EventHandler(cboCity_ValueChanged);
		resources.ApplyResources(this.lblMax, "lblMax");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val25, "appearance53");
		((ControlBase)this.lblMax).Appearance = (AppearanceBase)(object)val25;
		this.lblMax.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMax).Name = "lblMax";
		((ControlBase)this.lblMax).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		((System.Windows.Forms.Control)(object)this.txtTel).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val26).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val26).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val26, "appearance54");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val26;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.lblCity, "lblCity");
		((AppearanceBase)val27).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val27).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val27, "appearance55");
		((ControlBase)this.lblCity).Appearance = (AppearanceBase)(object)val27;
		this.lblCity.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCity).Name = "lblCity";
		((ControlBase)this.lblCity).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		resources.ApplyResources(this.lblAddress, "lblAddress");
		((AppearanceBase)val28).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val28).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val28, "appearance56");
		((ControlBase)this.lblAddress).Appearance = (AppearanceBase)(object)val28;
		this.lblAddress.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAddress).Name = "lblAddress";
		((ControlBase)this.lblAddress).WrapText = false;
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val29).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val29).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val29, "appearance57");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val29;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.txtAddress, "txtAddress");
		((System.Windows.Forms.Control)(object)this.txtAddress).Name = "txtAddress";
		base.AcceptButton = (System.Windows.Forms.IButtonControl)base.btnAdd;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIDIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpIDIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtIDNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCExpireDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpCDCIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCDCNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCDCNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportExpireDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpPassportIssueDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPassportNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCountry);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReorder);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArea);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMin);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMax);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCity);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAddress);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmTechnicians";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAddress, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEMail, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCity, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFax, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMin, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboArea, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobile2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReorder, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobile2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblClintSupplierTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBirthDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboCountry, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpPassportIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpPassportExpireDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPassportExpireDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCDCNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCDCNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCDCIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCDCIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpCDCExpireDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCDCExpireDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIDNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpIDIssueDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIDIssueDate, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpIDIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpCDCIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCDCNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportExpireDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpPassportIssueDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPassportNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboNationality).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCountry).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboTitle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboArea).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFax).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboCity).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAddress).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
