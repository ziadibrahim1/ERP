using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.HR;
using BusinessLayer.POS;
using BusinessLayer.Security;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.POS.Transactions;

public class frmDeliveryClientsSearch : frmButtons
{
	private DataTable dtGender;

	private DataTable dtBranches;

	private DataTable dtCities;

	private DataTable dtAreas;

	private DataTable dtDetails;

	private DataTable dtReligions;

	private DataTable dtRoomData;

	private DataTable dtColors;

	private DataTable dtSizes;

	private DataTable dtItems;

	private DataTable dtTopChecks;

	private DataTable dtSelectedCheckDetails;

	private DataRow drMaster;

	private string CurrentBranchCode = "";

	private bool CanEditVoucherNumber = true;

	private bool UsingColors;

	private bool UsingSizes = false;

	private ValueList vlCity = new ValueList();

	private ValueList vlArea = new ValueList();

	private ValueList vlItems = new ValueList();

	private ValueList vlColors = new ValueList();

	private ValueList vlSizes = new ValueList();

	public int ClientID = 0;

	private int RoomID;

	private IContainer components = null;

	public UltraTextEditor txtCode;

	public UltraButton btnSearch;

	public UltraButton btnPriveous;

	public UltraButton btnNext;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraLabel lblCode;

	public UltraButton btnCopyTo;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameAr;

	private UltraLabel lblNameAr;

	private UltraDateTimeEditor dtpBirthDate;

	private UltraLabel lblBirthDate;

	private UltraComboEditor cboGender;

	private UltraLabel lblGender;

	private UltraTextEditor txtPersonalIDNo;

	private UltraLabel lblPersonalIDNo;

	private UltraTextEditor txtEMail;

	private UltraLabel lblEMail;

	private UltraTextEditor txtNotes;

	private UltraLabel lblNotes;

	public UltraGroupBox UGBDetails;

	public UltraGrid ULGData;

	private UltraComboEditor cboReligion;

	private UltraLabel lblReligion;

	private UltraTextEditor txtTel;

	private UltraLabel lblTel;

	public UltraGrid ULGCheckDetails;

	public UltraGrid ULGTopChecks;

	private UltraTextEditor txtClientBarcode;

	private UltraLabel lblClientBarcode;

	public frmDeliveryClientsSearch()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		InitializeComponent();
		TableName = "POS_Clients";
		IDCol = "ClientID";
	}

	public frmDeliveryClientsSearch(int roomID)
		: this()
	{
		RoomID = roomID;
	}

	public override void SetSecurity()
	{
		base.SetSecurity();
	}

	public override void CallButtons(KeyEventArgs e)
	{
		base.CallButtons(e);
		if (!Adding && !Updating && e.KeyCode == Keys.F8 && ((Control)(object)btnSearch).Enabled && ((Control)(object)btnSearch).Visible)
		{
			btnSearch_Click(null, null);
		}
	}

	public override void SetControls(bool NavMode)
	{
		((Control)(object)btnAdd).Visible = NavMode;
		((Control)(object)btnUpdate).Visible = NavMode;
		((Control)(object)btnClose).Visible = NavMode;
		((Control)(object)btnRefreshData).Visible = false;
		((EditorButtonControlBase)txtClientBarcode).ReadOnly = NavMode;
		((Control)(object)btnOK).Visible = !NavMode;
		((Control)(object)btnSaveClose).Visible = false;
		((Control)(object)btnCancel).Visible = !NavMode;
		((Control)(object)txtCode).Enabled = !NavMode;
		((Control)(object)btnSearch).Visible = NavMode;
		((Control)(object)btnNext).Visible = false;
		((Control)(object)btnCopyTo).Visible = false;
		((Control)(object)btnPriveous).Visible = false;
		((Control)(object)btnDelete).Visible = false;
		((Control)(object)btnPrint).Visible = false;
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)cboGender).ReadOnly = NavMode;
		((EditorButtonControlBase)cboReligion).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEMail).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonalIDNo).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpBirthDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNotes).ReadOnly = NavMode;
		if (Updating)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if (((UltraGridBase)ULGData).Rows[i].Cells["CityID"].Value != DBNull.Value)
				{
					int cityID = int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["CityID"].Value.ToString());
					ValueList areasValueList = getAreasValueList(cityID);
					((UltraGridBase)ULGData).Rows[i].Cells["AreaID"].ValueList = (IValueList)(object)areasValueList;
					if (((DisposableObjectCollectionBase)areasValueList.ValueListItems).Count == 0)
					{
						((UltraGridBase)ULGData).Rows[i].Cells["AreaID"].Value = DBNull.Value;
					}
				}
			}
		}
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void PrepareData()
	{
		base.PrepareData();
		UsingColors = GlobalFunctions.GetOption("UsingItemsColors");
		UsingSizes = GlobalFunctions.GetOption("UsingItemsSizes");
		dtBranches = Branches.Select(GlobalVariables.CurrentBranchID, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		CurrentBranchCode = dtBranches.Rows[0]["BranchCode"].ToString();
		if (!base.DesignMode)
		{
			CanEditVoucherNumber = GlobalFunctions.GetOption("CanEditVoucherNumber");
			DataTable dt = Branches.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			GlobalFunctions.FillCombo(cboTransactionBranch, dt, "BranchID", "BranchName");
		}
		if (UsingColors)
		{
			dtColors = Colors.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlColors.ValueListItems.Clear();
			for (int i = 0; i < dtColors.Rows.Count; i++)
			{
				vlColors.ValueListItems.Add(dtColors.Rows[i]["ColorID"], dtColors.Rows[i]["ColorName"].ToString());
			}
		}
		if (UsingSizes)
		{
			dtSizes = ItemSizes.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			vlSizes.ValueListItems.Clear();
			for (int j = 0; j < dtSizes.Rows.Count; j++)
			{
				vlSizes.ValueListItems.Add(dtSizes.Rows[j]["ItemSizeID"], dtSizes.Rows[j]["ItemSizeName"].ToString());
			}
		}
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "-1", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItems.ValueListItems.Clear();
		for (int k = 0; k < dtItems.Rows.Count; k++)
		{
			vlItems.ValueListItems.Add(dtItems.Rows[k]["ItemID"], dtItems.Rows[k]["Name"].ToString());
		}
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCity.ValueListItems.Clear();
		for (int l = 0; l < dtCities.Rows.Count; l++)
		{
			vlCity.ValueListItems.Add(dtCities.Rows[l]["CityID"], dtCities.Rows[l]["CityName"].ToString());
		}
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlArea.ValueListItems.Clear();
		for (int m = 0; m < dtAreas.Rows.Count; m++)
		{
			vlArea.ValueListItems.Add(dtAreas.Rows[m]["AreaID"], dtAreas.Rows[m]["AreaName"].ToString());
		}
		dtDetails = ClientsDetails.SelectByClientID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtTopChecks = Checks.SelectTop5ByClientID("0", GlobalVariables.IsArabic ? "1" : "0");
		dtSelectedCheckDetails = ChecksDetails.SelectByCheckID("0", GlobalVariables.IsArabic ? "1" : "0");
		InitGrid();
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? Clients.GetCode(IsFromServer: false) : "");
		((Control)(object)txtClientBarcode).Text = CurrentBranchCode + "-" + ((Control)(object)txtCode).Text;
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		cboGender.SelectedIndex = -1;
		cboReligion.SelectedIndex = -1;
		((TextEditorControlBase)txtEMail).Clear();
		((TextEditorControlBase)txtPersonalIDNo).Clear();
		dtpBirthDate.DateTime = DateTime.Today;
		((TextEditorControlBase)txtNotes).Clear();
		dtDetails.Rows.Clear();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = Clients.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			dtTopChecks = Checks.SelectTop5ByClientID(RowID, GlobalVariables.IsArabic ? "1" : "0");
			if (dtTopChecks.Rows.Count > 0)
			{
				dtSelectedCheckDetails = ChecksDetails.SelectByCheckID(dtTopChecks.Rows[0]["CheckID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			}
			if (dataTable.Rows.Count > 0)
			{
				drMaster = dataTable.Rows[0];
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
			((Control)(object)txtCode).Text = drMaster["ClientCode"].ToString();
			((Control)(object)txtClientBarcode).Text = drMaster["ClientBarcode"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["ClientNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["ClientNameEn"].ToString();
			((TextEditorControlBase)cboGender).Value = drMaster["GenderID"];
			((TextEditorControlBase)cboReligion).Value = drMaster["ReligionID"];
			((Control)(object)txtEMail).Text = drMaster["EMail"].ToString();
			((Control)(object)txtPersonalIDNo).Text = drMaster["IDNumber"].ToString();
			dtpBirthDate.Value = drMaster["BirthDate"];
			((Control)(object)txtNotes).Text = drMaster["Notes"].ToString();
			dtDetails = ClientsDetails.SelectByClientID(drMaster["ClientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			InitGrid();
		}
		else
		{
			ClearControls();
		}
	}

	private void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dtDetails;
		GlobalFunctions.PrepareGrid(ULGData);
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGTopChecks).DataSource = dtTopChecks;
		GlobalFunctions.PrepareGrid(ULGTopChecks);
		((UltraGridBase)ULGTopChecks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGTopChecks).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGCheckDetails).DataSource = dtSelectedCheckDetails;
		GlobalFunctions.PrepareGrid(ULGCheckDetails);
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientDetailID"].DefaultCellValue = -1;
		if (!((KeyedSubObjectsCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Exists("CreateCheck"))
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns.Insert(0, "CreateCheck");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].Header).Caption = (GlobalVariables.IsArabic ? "إنشاء شيك" : "Create Check");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].Style = (ColumnStyle)8;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].ButtonDisplayStyle = (ButtonDisplayStyle)1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].Header).VisiblePosition = (GlobalVariables.IsArabic ? ((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CreateCheck"].Index : (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count - 1));
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["CreateCheck"].Value = (GlobalVariables.IsArabic ? "إنشاء شيك" : "Create Check");
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Header).Caption = (GlobalVariables.IsArabic ? "تليفون" : "phone");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PhoneNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MobileNumber"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MobileNumber"].Header).Caption = (GlobalVariables.IsArabic ? "محمول" : "Mobile");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MobileNumber"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Header).Caption = (GlobalVariables.IsArabic ? "المحافظة" : "City");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].ValueList = (IValueList)(object)vlCity;
		if (dtRoomData.Rows.Count == 0 || dtRoomData.Rows[0]["CityID"] != DBNull.Value)
		{
			((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["CityID"].DefaultCellValue = dtRoomData.Rows[0]["CityID"];
		}
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Header).Caption = (GlobalVariables.IsArabic ? "المنطقة" : "Area");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AreaID"].ValueList = (IValueList)(object)vlArea;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Header).Caption = (GlobalVariables.IsArabic ? "عنوان" : "Address");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Address"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["CheckNo"].Width = (int)((double)((Control)(object)ULGTopChecks).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["CheckNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم " : "No");
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["CheckNo"].Hidden = false;
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["CheckDate"].Width = (int)((double)((Control)(object)ULGTopChecks).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["CheckDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["CheckDate"].Hidden = false;
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["NetPrice"].Width = (int)((double)((Control)(object)ULGTopChecks).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["NetPrice"].Header).Caption = (GlobalVariables.IsArabic ? "الصافي" : "Net Price");
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["NetPrice"].Hidden = false;
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGTopChecks).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGTopChecks).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ColorID"].Width = (int)((double)((Control)(object)ULGCheckDetails).Width * 0.05);
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemSizeID"].Width = (int)((double)((Control)(object)ULGCheckDetails).Width * 0.05);
		((HeaderBase)((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ColorID"].Header).Caption = GlobalFunctions.GetFormName("SystemOptions", "GeneralData", "frmColors");
		((HeaderBase)((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemSizeID"].Header).Caption = GlobalFunctions.GetFormName("StockControl", "MasterData", "frmItemSizes");
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ColorID"].Hidden = !UsingColors;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemSizeID"].Hidden = !UsingSizes;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ColorID"].ValueList = (IValueList)(object)vlColors;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemSizeID"].ValueList = (IValueList)(object)vlSizes;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ColorID"].DefaultCellValue = 1;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemSizeID"].DefaultCellValue = 1;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemID"].Width = (int)((double)((Control)(object)ULGCheckDetails).Width * 0.4) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemID"].Hidden = false;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["ItemID"].ValueList = (IValueList)(object)vlItems;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGCheckDetails).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Qty"].DefaultCellValue = 1;
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGCheckDetails).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGCheckDetails).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم العميل", "Please Enter The Client Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الاسم بالعربية", "Please Enter The Client Arabic Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("POS_Clients", "ClientNameAr", ((Control)(object)txtNameAr).Text, Adding ? "0" : drMaster["ClientNameAr"].ToString(), IsFromServer: false) > 0)
		{
			GlobalVariables.InformationMB.Show("هذا الاسم متواجد من قبل", "Patient Name Already Exists");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtClientBarcode).Text.Trim() != "")
		{
			if (Main.CheckForValue("POS_Clients", "ClientBarcode", ((Control)(object)txtClientBarcode).Text, Adding ? "0" : drMaster["ClientBarcode"].ToString(), IsFromServer: false) > 0)
			{
				GlobalVariables.InformationMB.Show("هذا الكود متواجد من قبل", "Client Barcode Already Exists");
				((TextEditorControlBase)txtClientBarcode).Focus();
				return false;
			}
		}
		else if (Main.CheckForValue("POS_Clients", "ClientCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["ClientCode"].ToString(), IsFromServer: false) > 0)
		{
			string code = Clients.GetCode(IsFromServer: false);
			GlobalVariables.QuestionMB.Show("رقم هذا الملف متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The Profile Number Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا العميل", "Please insert details for this Client");
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value == DBNull.Value && ((UltraGridBase)ULGData).Rows[i].Cells["MobileNumber"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال التليفون او المحمول", "Please Enter Telephone No Or Mobile No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"];
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value.ToString() == ((UltraGridBase)ULGData).Rows[i].Cells["MobileNumber"].Value.ToString())
			{
				GlobalVariables.InformationMB.Show("لابد ان يكون رقم التليفون مختلف عن رقم المحمول", "Tel No Must be different from Mobile No");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"];
				return false;
			}
			DataTable dataTable = Clients.ValidateDetailsPhonesNo(((UltraGridBase)ULGData).Rows[i].Cells["PhoneNumber"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].Cells["MobileNumber"].Value.ToString(), Adding ? "-1" : drMaster["ClientID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
			if (dataTable.Rows.Count > 0)
			{
				GlobalVariables.InformationMB.Show("رقم الهاتف متواجد من قبل \n إسم العميل :  " + dataTable.Rows[0]["ClientName"].ToString(), "The Phone Number Already Exists Client Name : " + dataTable.Rows[0]["ClientName"].ToString());
				return false;
			}
		}
		if (dtRoomData.Rows.Count == 0 || dtRoomData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لابد من اختيار العميل الافتراضى للصالة", "You Must Select Default Client For Delivery Room");
			return false;
		}
		if (dtRoomData.Rows[0]["DefaultSubAccountID"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show("لابد من اختيار نوع السعر للصالة", "You Must Select Price Type For Delivery Room");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ClientID = Clients.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, ((Control)(object)txtNameEn).Text, (((Control)(object)txtClientBarcode).Text == "") ? "Null" : ((Control)(object)txtClientBarcode).Text, (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), "Null", "Null", "Null", ((Control)(object)txtEMail).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), "1", "Null", "Null", "Null", ((Control)(object)txtNotes).Text, "0", "Null", "Null", "Null", "1", "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ClientID"].Value = ClientID.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["ClientDetailID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ClientsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			RowID = ClientID.ToString();
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			ClientID = Clients.Insert_Update(drMaster["ClientID"].ToString(), ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, ((Control)(object)txtNameEn).Text, (((Control)(object)txtClientBarcode).Text == "") ? "Null" : ((Control)(object)txtClientBarcode).Text, (cboGender.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboGender).Value.ToString(), (cboReligion.SelectedIndex == -1) ? "Null" : ((TextEditorControlBase)cboReligion).Value.ToString(), "Null", "Null", "Null", ((Control)(object)txtEMail).Text, (((Control)(object)txtPersonalIDNo).Text == "") ? "Null" : ((Control)(object)txtPersonalIDNo).Text, (dtpBirthDate.Value == null) ? "Null" : dtpBirthDate.DateTime.ToString(GlobalVariables.DateShortFormate), "1", "Null", "Null", "Null", ((Control)(object)txtNotes).Text, (drMaster["DiscountPercentage"] == DBNull.Value) ? "0" : drMaster["DiscountPercentage"].ToString(), (drMaster["PriceTypeID"] == DBNull.Value) ? "Null" : drMaster["PriceTypeID"].ToString(), (drMaster["SubAccountID"] == DBNull.Value) ? "Null" : drMaster["SubAccountID"].ToString(), "Null", "1", "1", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
			string text = ",";
			((UltraGridBase)ULGData).UpdateData();
			dtDetails.AcceptChanges();
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["ClientID"].Value = ClientID.ToString();
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["ClientDetailID"].Value.ToString() + ",";
			}
			Main.DeleteForUpdate("POS_ClientsDetails", "ClientID", drMaster["ClientID"].ToString(), "ClientDetailID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				ClientsDetails.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: false);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
		}
	}

	public override void btnUpdateClick()
	{
		if (drMaster != null)
		{
			RowID = drMaster[IDCol].ToString();
			base.btnUpdateClick();
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
		((Control)(object)btnOK).Focus();
		if (!ValidateData())
		{
			return;
		}
		DataSaved = true;
		if (Adding)
		{
			AddData();
			if (DataSaved)
			{
				Adding = false;
				SetControls(NavMode: true);
				FillData();
			}
			return;
		}
		UpdateData();
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

	public override void btnRefreshDataClick()
	{
		dtRoomData = Rooms.SelectDefaultData(RoomID.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtGender = Gender.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboGender, dtGender, "GenderID", "GenderName");
		dtReligions = Religions.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboReligion, dtReligions, "ReligionID", "ReligionName");
		dtCities = Cities.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlCity.ValueListItems.Clear();
		for (int i = 0; i < dtCities.Rows.Count; i++)
		{
			vlCity.ValueListItems.Add(dtCities.Rows[i]["CityID"], dtCities.Rows[i]["CityName"].ToString());
		}
		dtAreas = Areas.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlArea.ValueListItems.Clear();
		for (int j = 0; j < dtAreas.Rows.Count; j++)
		{
			vlArea.ValueListItems.Add(dtAreas.Rows[j]["AreaID"], dtAreas.Rows[j]["AreaName"].ToString());
		}
	}

	public virtual void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.POSClientsSearchReportWithDetails("1", GlobalVariables.CurrentBranchID, IsFromServer: false);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["ClientID"].ToString();
			FillData();
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		if (((Control)(object)txtTel).Text != "" && e.Row.Index == 0)
		{
			e.Row.Cells["MobileNumber"].Value = ((Control)(object)txtTel).Text;
		}
	}

	private void txtTel_ValueChanged(object sender, EventArgs e)
	{
	}

	private void txtPersonalIDNo_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected O, but got Unknown
		ULGData.CellListSelect -= new CellEventHandler(ULGData_CellListSelect);
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "CityID" && e.Cell.Column.ValueList.SelectedItemIndex >= 0)
		{
			((UltraGridBase)ULGData).UpdateData();
			int num = ((e.Cell.Column.ValueList.SelectedItemIndex >= 0) ? int.Parse(dtCities.Rows[e.Cell.Column.ValueList.SelectedItemIndex]["CityID"].ToString()) : 0);
			if (num != 0)
			{
				e.Cell.Row.Cells["AreaID"].Value = DBNull.Value;
				e.Cell.Row.Cells["AreaID"].ValueList = (IValueList)(object)getAreasValueList(num);
			}
			else
			{
				e.Cell.Row.Cells["AreaID"].ValueList = null;
				e.Cell.Row.Cells["AreaID"].Value = DBNull.Value;
			}
		}
		ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
	}

	private ValueList getAreasValueList(int CityID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ValueList val = new ValueList();
		DataRow[] array = dtAreas.Select("CityID=" + CityID);
		for (int i = 0; i < array.Length; i++)
		{
			val.ValueListItems.Add((object)array[i]["AreaID"].ToString(), array[i]["AreaName"].ToString());
		}
		return val;
	}

	private void ULGData_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
		else if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "AreaID" && ((UltraGridBase)ULGData).ActiveRow.Cells["CityID"].Value == DBNull.Value)
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "PhoneNumber" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MobileNumber"))
		{
			GlobalFunctions.CheckForIntegers(ULGData.ActiveCell, e);
		}
	}

	private void txtTel_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return || !(((Control)(object)txtTel).Text != ""))
		{
			return;
		}
		DataTable dataTable = Clients.SelectByMobilePhoneNumber(((Control)(object)txtTel).Text, GlobalVariables.CurrentBranchID, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			if (bool.Parse(dataTable.Rows[0]["IsActive"].ToString()))
			{
				RowID = dataTable.Rows[0][IDCol].ToString();
			}
			else
			{
				RowID = "";
				GlobalVariables.InformationMB.Show("لا يمكن اظهار البيانات الخاصه بهذا العميل لانه غير نشط", "InActive Client");
				dtDetails.Rows.Clear();
				dtSelectedCheckDetails.Rows.Clear();
				dtTopChecks.Rows.Clear();
			}
			dtSearchResult = null;
		}
		else
		{
			RowID = "";
		}
		FillData();
	}

	private void ULGData_ClickCellButton(object sender, CellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow == null || !(((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CreateCheck") || ((UltraGridBase)ULGData).ActiveRow.Cells["ClientID"].Value == DBNull.Value || Adding || Updating)
		{
			return;
		}
		decimal deliveryfees = default(decimal);
		if (((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value != DBNull.Value)
		{
			DataTable deliveryFees = AreasBranchesDelivery.GetDeliveryFees(((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value.ToString(), GlobalVariables.CurrentBranchID, IsFromServer: false);
			if (deliveryFees.Rows.Count > 0)
			{
				deliveryfees = decimal.Parse(deliveryFees.Rows[0]["DeliveryFees"].ToString());
			}
			else if (dtAreas.Select(" AreaID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value.ToString())[0]["DeliveryFees"] != DBNull.Value)
			{
				deliveryfees = decimal.Parse(dtAreas.Select(" AreaID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value.ToString())[0]["DeliveryFees"].ToString());
			}
		}
		frmDeliveryChecks frmDeliveryChecks2 = new frmDeliveryChecks(-1, RoomID, int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClientDetailID"].Value.ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClientID"].Value.ToString()), deliveryfees);
		frmDeliveryChecks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
		frmDeliveryChecks2.Tag = base.Tag;
		frmDeliveryChecks2.Location = new Point(0, 0);
		frmDeliveryChecks2.CanAdd = CanAdd;
		frmDeliveryChecks2.CanUpdate = CanUpdate;
		frmDeliveryChecks2.CanDelete = CanDelete;
		frmDeliveryChecks2.CanDiscount = CanDiscount;
		frmDeliveryChecks2.CanSearching = CanSearching;
		frmDeliveryChecks2.CanExport = CanExport;
		frmDeliveryChecks2.CanPrint = CanPrint;
		frmDeliveryChecks2.CanPrintReport = CanPrintReport;
		frmDeliveryChecks2.CanViewReport = CanViewReport;
		frmDeliveryChecks2.CanMinimunCharge = CanMinimunCharge;
		frmDeliveryChecks2.ShowDialog();
	}

	private void ULGData_ClickCell(object sender, ClickCellEventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((UltraGridBase)ULGData).ActiveRow.Cells["ClientID"].Value != DBNull.Value && !Adding && !Updating)
		{
			frmDeliveryChecks frmDeliveryChecks2 = new frmDeliveryChecks(-1, RoomID, int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClientDetailID"].Value.ToString()), int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ClientID"].Value.ToString()), (((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value == DBNull.Value || dtAreas.Select(" AreaID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value.ToString())[0]["DeliveryFees"] == DBNull.Value) ? 0m : decimal.Parse(dtAreas.Select(" AreaID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["AreaID"].Value.ToString())[0]["DeliveryFees"].ToString()));
			frmDeliveryChecks2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			frmDeliveryChecks2.Tag = base.Tag;
			frmDeliveryChecks2.Location = new Point(0, 0);
			frmDeliveryChecks2.CanAdd = CanAdd;
			frmDeliveryChecks2.CanUpdate = CanUpdate;
			frmDeliveryChecks2.CanDelete = CanDelete;
			frmDeliveryChecks2.CanDiscount = CanDiscount;
			frmDeliveryChecks2.CanSearching = CanSearching;
			frmDeliveryChecks2.CanExport = CanExport;
			frmDeliveryChecks2.CanPrint = CanPrint;
			frmDeliveryChecks2.CanPrintReport = CanPrintReport;
			frmDeliveryChecks2.CanViewReport = CanViewReport;
			frmDeliveryChecks2.CanMinimunCharge = CanMinimunCharge;
			frmDeliveryChecks2.ShowDialog();
		}
	}

	private void ULGTopChecks_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGTopChecks).ActiveRow).Selected = true;
	}

	private void ULGTopChecks_DoubleClick(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGTopChecks).ActiveRow != null)
		{
			dtSelectedCheckDetails = ChecksDetails.SelectByCheckID(((UltraGridBase)ULGTopChecks).ActiveRow.Cells["CheckID"].Value.ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGCheckDetails).DataSource = dtSelectedCheckDetails;
		}
	}

	private void ULGCheckDetails_AfterEnterEditMode(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGCheckDetails).ActiveRow).Selected = true;
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
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Expected O, but got Unknown
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Expected O, but got Unknown
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Expected O, but got Unknown
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Expected O, but got Unknown
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Expected O, but got Unknown
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Expected O, but got Unknown
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Expected O, but got Unknown
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Expected O, but got Unknown
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Expected O, but got Unknown
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Expected O, but got Unknown
		//IL_1328: Unknown result type (might be due to invalid IL or missing references)
		//IL_1332: Expected O, but got Unknown
		//IL_1340: Unknown result type (might be due to invalid IL or missing references)
		//IL_134a: Expected O, but got Unknown
		//IL_1358: Unknown result type (might be due to invalid IL or missing references)
		//IL_1362: Expected O, but got Unknown
		//IL_1370: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmDeliveryClientsSearch));
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
		Appearance val30 = new Appearance();
		Appearance val31 = new Appearance();
		Appearance val32 = new Appearance();
		Appearance val33 = new Appearance();
		Appearance val34 = new Appearance();
		Appearance val35 = new Appearance();
		Appearance val36 = new Appearance();
		Appearance val37 = new Appearance();
		Appearance val38 = new Appearance();
		Appearance val39 = new Appearance();
		Appearance val40 = new Appearance();
		Appearance val41 = new Appearance();
		Appearance val42 = new Appearance();
		Appearance val43 = new Appearance();
		Appearance val44 = new Appearance();
		Appearance val45 = new Appearance();
		this.txtCode = new UltraTextEditor();
		this.btnSearch = new UltraButton();
		this.btnPriveous = new UltraButton();
		this.btnNext = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.lblCode = new UltraLabel();
		this.btnCopyTo = new UltraButton();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameEn = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.dtpBirthDate = new UltraDateTimeEditor();
		this.lblBirthDate = new UltraLabel();
		this.cboGender = new UltraComboEditor();
		this.lblGender = new UltraLabel();
		this.txtPersonalIDNo = new UltraTextEditor();
		this.lblPersonalIDNo = new UltraLabel();
		this.txtEMail = new UltraTextEditor();
		this.lblEMail = new UltraLabel();
		this.txtNotes = new UltraTextEditor();
		this.lblNotes = new UltraLabel();
		this.UGBDetails = new UltraGroupBox();
		this.txtClientBarcode = new UltraTextEditor();
		this.lblClientBarcode = new UltraLabel();
		this.cboReligion = new UltraComboEditor();
		this.ULGData = new UltraGrid();
		this.lblReligion = new UltraLabel();
		this.txtTel = new UltraTextEditor();
		this.lblTel = new UltraLabel();
		this.ULGCheckDetails = new UltraGrid();
		this.ULGTopChecks = new UltraGrid();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCheckDetails).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGTopChecks).BeginInit();
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
		resources.ApplyResources(val, "appearance1");
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
		resources.ApplyResources(base.btnImport, "btnImport");
		resources.ApplyResources(base.btnExport, "btnExport");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtCode, "txtCode");
		((System.Windows.Forms.Control)(object)this.txtCode).Name = "txtCode";
		resources.ApplyResources(this.btnSearch, "btnSearch");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.search;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnSearch).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btnSearch).Name = "btnSearch";
		((System.Windows.Forms.Control)(object)this.btnSearch).Click += new System.EventHandler(btnSearch_Click);
		resources.ApplyResources(this.btnPriveous, "btnPriveous");
		((AppearanceBase)val4).Image = ERP.Properties.Resources.BarLeft;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnPriveous).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btnPriveous).Name = "btnPriveous";
		resources.ApplyResources(this.btnNext, "btnNext");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.BarRight;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnNext).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val7).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val7).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val8).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblCode).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((UltraControlBase)this.lblCode).UseAppStyling = false;
		resources.ApplyResources(this.btnCopyTo, "btnCopyTo");
		((System.Windows.Forms.Control)(object)this.btnCopyTo).Name = "btnCopyTo";
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.dtpBirthDate, "dtpBirthDate");
		((UltraWinEditorMaskedControlBase)this.dtpBirthDate).AlwaysInEditMode = true;
		((System.Windows.Forms.Control)(object)this.dtpBirthDate).Name = "dtpBirthDate";
		resources.ApplyResources(this.lblBirthDate, "lblBirthDate");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblBirthDate).Appearance = (AppearanceBase)(object)val9;
		this.lblBirthDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBirthDate).Name = "lblBirthDate";
		((ControlBase)this.lblBirthDate).WrapText = false;
		resources.ApplyResources(this.cboGender, "cboGender");
		((System.Windows.Forms.Control)(object)this.cboGender).Name = "cboGender";
		resources.ApplyResources(this.lblGender, "lblGender");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblGender).Appearance = (AppearanceBase)(object)val10;
		this.lblGender.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblGender).Name = "lblGender";
		((ControlBase)this.lblGender).WrapText = false;
		resources.ApplyResources(this.txtPersonalIDNo, "txtPersonalIDNo");
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).Name = "txtPersonalIDNo";
		((System.Windows.Forms.Control)(object)this.txtPersonalIDNo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPersonalIDNo_KeyPress);
		resources.ApplyResources(this.lblPersonalIDNo, "lblPersonalIDNo");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblPersonalIDNo).Appearance = (AppearanceBase)(object)val11;
		this.lblPersonalIDNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonalIDNo).Name = "lblPersonalIDNo";
		((ControlBase)this.lblPersonalIDNo).WrapText = false;
		resources.ApplyResources(this.txtEMail, "txtEMail");
		((System.Windows.Forms.Control)(object)this.txtEMail).Name = "txtEMail";
		resources.ApplyResources(this.lblEMail, "lblEMail");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblEMail).Appearance = (AppearanceBase)(object)val12;
		this.lblEMail.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEMail).Name = "lblEMail";
		((ControlBase)this.lblEMail).WrapText = false;
		resources.ApplyResources(this.txtNotes, "txtNotes");
		((System.Windows.Forms.Control)(object)this.txtNotes).Name = "txtNotes";
		resources.ApplyResources(this.lblNotes, "lblNotes");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblNotes).Appearance = (AppearanceBase)(object)val13;
		this.lblNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNotes).Name = "lblNotes";
		((ControlBase)this.lblNotes).WrapText = false;
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtClientBarcode);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblClientBarcode);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboReligion);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtEMail);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblEMail);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblReligion);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtPersonalIDNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNotes);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblPersonalIDNo);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.cboGender);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNotes);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblGender);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.dtpBirthDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblBirthDate);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.txtClientBarcode, "txtClientBarcode");
		((System.Windows.Forms.Control)(object)this.txtClientBarcode).Name = "txtClientBarcode";
		resources.ApplyResources(this.lblClientBarcode, "lblClientBarcode");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblClientBarcode).Appearance = (AppearanceBase)(object)val14;
		this.lblClientBarcode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClientBarcode).Name = "lblClientBarcode";
		((ControlBase)this.lblClientBarcode).WrapText = false;
		resources.ApplyResources(this.cboReligion, "cboReligion");
		((System.Windows.Forms.Control)(object)this.cboReligion).Name = "cboReligion";
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val15).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val15).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val15, "appearance15");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val15;
		((AppearanceBase)val16).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val16;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val17).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val17).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val17).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val17;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val18).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val18).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val18, "appearance18");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val18;
		((AppearanceBase)val19).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val19).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val19, "appearance19");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val19;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val20).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val20, "appearance20");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val20;
		((AppearanceBase)val21).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val21, "appearance21");
		((AppearanceBase)val21).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val21;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val22).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val22).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val22).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val22).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val22).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val22, "appearance22");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val22;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val23).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val23).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val23, "appearance23");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val23;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val24).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val24, "appearance24");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val24;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.AfterEnterEditMode += new System.EventHandler(ULGData_AfterEnterEditMode);
		this.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		this.ULGData.ClickCellButton += new CellEventHandler(ULGData_ClickCellButton);
		this.ULGData.ClickCell += new ClickCellEventHandler(ULGData_ClickCell);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(this.lblReligion, "lblReligion");
		this.lblReligion.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReligion).Name = "lblReligion";
		((ControlBase)this.lblReligion).WrapText = false;
		resources.ApplyResources(this.txtTel, "txtTel");
		((System.Windows.Forms.Control)(object)this.txtTel).Name = "txtTel";
		((TextEditorControlBase)this.txtTel).ValueChanged += new System.EventHandler(txtTel_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtTel).KeyUp += new System.Windows.Forms.KeyEventHandler(txtTel_KeyUp);
		resources.ApplyResources(this.lblTel, "lblTel");
		((AppearanceBase)val25).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val25).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val25, "appearance25");
		((ControlBase)this.lblTel).Appearance = (AppearanceBase)(object)val25;
		this.lblTel.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTel).Name = "lblTel";
		((ControlBase)this.lblTel).WrapText = false;
		resources.ApplyResources(this.ULGCheckDetails, "ULGCheckDetails");
		((AppearanceBase)val26).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val26).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val26).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val26).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val26, "appearance26");
		((SpecialBoxBase)((UltraGridBase)this.ULGCheckDetails).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val26;
		((AppearanceBase)val27).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val27, "appearance27");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val27;
		((SpecialBoxBase)((UltraGridBase)this.ULGCheckDetails).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val28).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val28).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val28).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val28).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val28, "appearance28");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val28;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val29).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val29).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val29, "appearance29");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val29;
		((AppearanceBase)val30).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val30).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val30, "appearance30");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val30;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val31).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val31, "appearance31");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val31;
		((AppearanceBase)val32).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val32, "appearance32");
		((AppearanceBase)val32).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val32;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val33).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val33).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val33).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val33).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val33).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val33, "appearance33");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val33;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val34).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val34).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val34, "appearance34");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val34;
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val35).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val35, "appearance35");
		((UltraGridBase)this.ULGCheckDetails).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val35;
		((System.Windows.Forms.Control)(object)this.ULGCheckDetails).Name = "ULGCheckDetails";
		this.ULGCheckDetails.AfterEnterEditMode += new System.EventHandler(ULGCheckDetails_AfterEnterEditMode);
		resources.ApplyResources(this.ULGTopChecks, "ULGTopChecks");
		((AppearanceBase)val36).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val36).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val36).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val36).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val36, "appearance36");
		((SpecialBoxBase)((UltraGridBase)this.ULGTopChecks).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val36;
		((AppearanceBase)val37).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val37, "appearance37");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val37;
		((SpecialBoxBase)((UltraGridBase)this.ULGTopChecks).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val38).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val38).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val38).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val38).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val38, "appearance38");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val38;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val39).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val39).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val39, "appearance39");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val39;
		((AppearanceBase)val40).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val40).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val40, "appearance40");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val40;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val41).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val41, "appearance41");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val41;
		((AppearanceBase)val42).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val42, "appearance42");
		((AppearanceBase)val42).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val42;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val43).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val43).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val43).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val43).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val43).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val43, "appearance43");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val43;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val44).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val44).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val44, "appearance44");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val44;
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val45).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val45, "appearance45");
		((UltraGridBase)this.ULGTopChecks).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val45;
		((System.Windows.Forms.Control)(object)this.ULGTopChecks).Name = "ULGTopChecks";
		this.ULGTopChecks.AfterEnterEditMode += new System.EventHandler(ULGTopChecks_AfterEnterEditMode);
		((System.Windows.Forms.Control)(object)this.ULGTopChecks).DoubleClick += new System.EventHandler(ULGTopChecks_DoubleClick);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGCheckDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGTopChecks);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCopyTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSearch);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPriveous);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmDeliveryClientsSearch";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGTopChecks, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGCheckDetails, 0);
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpBirthDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboGender).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonalIDNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEMail).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtClientBarcode).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboReligion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTel).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGCheckDetails).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGTopChecks).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
