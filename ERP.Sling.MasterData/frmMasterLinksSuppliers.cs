using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Sling;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;

namespace ERP.Sling.MasterData;

public class frmMasterLinksSuppliers : frmHeaderManyDetails
{
	private DataTable dtItems;

	private DataTable dtMasterLinksQuad;

	private ValueList vlItemsDual = new ValueList();

	private ValueList vlItemsQuad = new ValueList();

	private IContainer components = null;

	private UltraTabPageControl ultraTabPageControl2;

	protected internal UltraGrid ULGDataQuad;

	private UltraLabel lblNameEn;

	private UltraTextEditor txtNameEn;

	private UltraLabel lblNameAr;

	private UltraTextEditor txtNameAr;

	private UltraButton btnSelectItems;

	public frmMasterLinksSuppliers()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "SLN_MasterLinksSuppliers";
		IDCol = "MasterLinkSupplierID";
		NoCol = "MasterLinkSupplierCode";
		DateCol = "GetDate()";
	}

	public frmMasterLinksSuppliers(int ID)
		: this()
	{
		RowID = ID.ToString();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsDual.ValueListItems.Clear();
		vlItemsQuad.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItemsDual.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			vlItemsQuad.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
		dtDetails = MasterLinksDual.SelectByMasterLinkSupplierID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtMasterLinksQuad = MasterLinksQuad.SelectByMasterLinkSupplierID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((UltraGridBase)ULGDataQuad).DataSource = dtMasterLinksQuad;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		GlobalFunctions.PrepareGrid(ULGDataQuad);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualItemID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Length"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود" : "Code");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].Header).Caption = (GlobalVariables.IsArabic ? "القطر" : "Diameter");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Length"].Header).Caption = (GlobalVariables.IsArabic ? "الطول" : "Length");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].Header).Caption = (GlobalVariables.IsArabic ? "العرض" : "Width");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].Header).Caption = (GlobalVariables.IsArabic ? "اقصى حمولة" : "Working Load Limit");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualItemID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Length"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Diameter"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Length"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MasterLinkDualItemID"].ValueList = (IValueList)(object)vlItemsDual;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadCode"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadItemID"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.2);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter2"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length2"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width2"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].Width = (int)((double)((Control)(object)ULGDataQuad).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadCode"].Header).Caption = (GlobalVariables.IsArabic ? "كود" : "Code");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadItemID"].Header).Caption = (GlobalVariables.IsArabic ? "الصنف" : "Item");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter"].Header).Caption = (GlobalVariables.IsArabic ? "القطر" : "Diameter");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length"].Header).Caption = (GlobalVariables.IsArabic ? "الطول" : "Length");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width"].Header).Caption = (GlobalVariables.IsArabic ? "العرض" : "Width");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter2"].Header).Caption = (GlobalVariables.IsArabic ? "القطر2" : "Diameter2");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length2"].Header).Caption = (GlobalVariables.IsArabic ? "الطول2" : "Length2");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width2"].Header).Caption = (GlobalVariables.IsArabic ? "2العرض" : "Width2");
		((HeaderBase)((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].Header).Caption = (GlobalVariables.IsArabic ? "اقصى حمولة" : "Working Load Limit");
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadCode"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadItemID"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter2"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length2"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width2"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].Hidden = false;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Diameter2"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Length2"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["Width2"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["WorkingLoadLimit"].DefaultCellValue = 0;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Bands[0].Columns["MasterLinkQuadItemID"].ValueList = (IValueList)(object)vlItemsQuad;
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = MasterLinksSuppliers.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
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
			((Control)(object)txtCode).Text = drMaster["MasterLinkSupplierCode"].ToString();
			((Control)(object)txtNameAr).Text = drMaster["MasterLinkSupplierNameAr"].ToString();
			((Control)(object)txtNameEn).Text = drMaster["MasterLinkSupplierNameEn"].ToString();
			dtDetails = MasterLinksDual.SelectByMasterLinkSupplierID(drMaster["MasterLinkSupplierID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			dtMasterLinksQuad = MasterLinksQuad.SelectByMasterLinkSupplierID(drMaster["MasterLinkSupplierID"].ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
			((UltraGridBase)ULGData).DataSource = dtDetails;
			((UltraGridBase)ULGDataQuad).DataSource = dtMasterLinksQuad;
			InitGrid();
		}
		else
		{
			ClearControls();
		}
		((TextEditorControlBase)txtCode).Focus();
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtNameEn).ReadOnly = NavMode;
		((Control)(object)btnSelectItems).Visible = !NavMode;
		((UltraGridBase)ULGDataQuad).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)((Adding || Updating) ? 1 : 2);
		((UltraGridBase)ULGDataQuad).DisplayLayout.Override.AllowAddNew = (AllowAddNew)(NavMode ? 2 : 6);
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtCode).Clear();
		((Control)(object)txtCode).Text = (Adding ? MasterLinksSuppliers.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtNameAr).Clear();
		((TextEditorControlBase)txtNameEn).Clear();
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataQuad).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGDataQuad).DataSource).Rows.Clear();
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الكود" : "Please Enter The Code");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtNameAr).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الاسم بالعربية" : "Please Enter The  Arabic Name");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (Main.CheckForValue("SLN_MasterLinksSuppliers", "MasterLinkSupplierNameAr", ((Control)(object)txtNameAr).Text, Updating ? drMaster["MasterLinkSupplierNameAr"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" الاسم بالعربية متواجد من قبل ", "Arabic Name Already Exist");
			((TextEditorControlBase)txtNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtNameEn).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "برجاء إدخال الاسم بالانجليزية" : "Please Enter The  English Name");
			((TextEditorControlBase)txtNameEn).Focus();
			return false;
		}
		if (Main.CheckForValue("SLN_MasterLinksSuppliers", "MasterLinkSupplierNameEn", ((Control)(object)txtNameEn).Text, Updating ? drMaster["MasterLinkSupplierNameEn"].ToString() : "", IsFromServer: true) > 0)
		{
			GlobalVariables.InformationMB.Show(" الاسم بالانجليزية متواجد من قبل ", "English Name Already Exist");
			((TextEditorControlBase)txtNameEn).Focus();
			return false;
		}
		if (Main.CheckForValue("SLN_MasterLinksSuppliers", "MasterLinkSupplierCode", ((Control)(object)txtCode).Text, Adding ? "0" : drMaster["MasterLinkSupplierCode"].ToString(), IsFromServer: true) > 0)
		{
			string code = MasterLinksSuppliers.GetCode(IsFromServer: true);
			GlobalVariables.QuestionMB.Show("رقم هذا المورد متواجد من قبل \n سوف يتم الحفظ برقم " + code, "The MasterLink Supplier Code  Already Exists It Will Be Saved With No. : " + code);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtCode).Focus();
				return false;
			}
			((Control)(object)txtCode).Text = code;
		}
		else if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == 0 && ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataQuad).Rows).Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخــال تفاصيل لهذا المورد", "Please insert details for this Supplier");
			((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualItemID"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualItemID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualCode"].Value == DBNull.Value || ((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualCode"].Value.ToString() == "")
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  الكود  ", "Please Enter Code");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualCode"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualCode"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Diameter"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Diameter"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  القطر  ", "Please Enter Diameter");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Diameter"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["Diameter"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Length"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Length"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  الطول  ", "Please Enter Length");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Length"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["Length"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["Width"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Width"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  العرض  ", "Please Enter Width");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["Width"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["Width"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
			if (((UltraGridBase)ULGData).Rows[i].Cells["WorkingLoadLimit"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["WorkingLoadLimit"].Value.ToString()) <= 0m)
			{
				((Control)(object)ULGData).Enter -= ULGData_Enter;
				GlobalVariables.InformationMB.Show("برجاء ادخال  أقصى حمولة  ", "Please Enter Working Load Limit");
				ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].Cells["WorkingLoadLimit"];
				((UltraTabControlBase)UTCDetails).Tabs[0].Selected = true;
				((UltraGridBase)ULGData).Rows[i].Cells["WorkingLoadLimit"].DroppedDown = true;
				((Control)(object)ULGData).Enter += ULGData_Enter;
				return false;
			}
		}
		for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataQuad).Rows).Count; j++)
		{
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadItemID"].Value == DBNull.Value)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال اسم الصنف  ", "Please Enter Item Name");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadItemID"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadItemID"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadCode"].Value == DBNull.Value || ((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadCode"].Value.ToString() == "")
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  الكود  ", "Please Enter Code");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadCode"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadCode"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  المساحة  ", "Please Enter Diameter");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  الطول  ", "Please Enter Length");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  العرض  ", "Please Enter Width");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter2"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter2"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  القطر2  ", "Please Enter Diameter2");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter2"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["Diameter2"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length2"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length2"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال  الطول2  ", "Please Enter Length2");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length2"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["Length2"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width2"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width2"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("2برجاء ادخال  العرض  ", "Please Enter Width2");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width2"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["Width2"].DroppedDown = true;
				return false;
			}
			if (((UltraGridBase)ULGDataQuad).Rows[j].Cells["WorkingLoadLimit"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGDataQuad).Rows[j].Cells["WorkingLoadLimit"].Value.ToString()) <= 0m)
			{
				GlobalVariables.InformationMB.Show("برجاء ادخال أقصى حمولة ", "Please Enter WorkingLoadLimit");
				ULGDataQuad.ActiveCell = ((UltraGridBase)ULGDataQuad).Rows[j].Cells["WorkingLoadLimit"];
				((UltraTabControlBase)UTCDetails).Tabs[1].Selected = true;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["WorkingLoadLimit"].DroppedDown = true;
				return false;
			}
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = MasterLinksSuppliers.Insert_Update("-1", ((Control)(object)txtCode).Text, ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? ((Control)(object)txtNameAr).Text : ((Control)(object)txtNameEn).Text, "Null", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkSupplierID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataQuad).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadID"].Value = -1;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkSupplierID"].Value = num;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				MasterLinksDual.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGDataQuad).Rows).Count > 0)
			{
				MasterLinksQuad.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataQuad).DataSource, GlobalVariables.UserID, IsFromServer: true);
			}
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			int num = MasterLinksSuppliers.Insert_Update(drMaster["MasterLinkSupplierID"].ToString(), ((Control)(object)txtCode).Text, (((Control)(object)txtNameAr).Text == "") ? "Null" : ((Control)(object)txtNameAr).Text, (((Control)(object)txtNameEn).Text == "") ? "Null" : ((Control)(object)txtNameEn).Text, (drMaster["MasterLinkDualParentItemID"] == DBNull.Value) ? "Null" : drMaster["MasterLinkDualParentItemID"].ToString(), (drMaster["MasterLinkQuadParentItemID"] == DBNull.Value) ? "Null" : drMaster["MasterLinkQuadParentItemID"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
			MasterLinksQuad.DeleteByMasterLinkSupplierID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			MasterLinksDual.DeleteByMasterLinkSupplierID(num.ToString(), GlobalVariables.UserID, IsFromServer: true);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkDualID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["MasterLinkSupplierID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGDataQuad).Rows).Count; j++)
			{
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkQuadID"].Value = -1;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["MasterLinkSupplierID"].Value = num;
				((UltraGridBase)ULGDataQuad).Rows[j].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			MasterLinksDual.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
			MasterLinksQuad.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGDataQuad).DataSource, GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		Main.StartBulkTrans(FromServer: true);
		try
		{
			MasterLinksQuad.DeleteByMasterLinkSupplierID(drMaster["MasterLinkSupplierID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			MasterLinksDual.DeleteByMasterLinkSupplierID(drMaster["MasterLinkSupplierID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			MasterLinksSuppliers.Delete(drMaster["MasterLinkSupplierID"].ToString(), GlobalVariables.UserID, IsFromServer: true);
			Main.EndBulkTrans(FromServer: true);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: true);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.MasterLinksSuppliersSearchReport(IsFromServer: true);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["MasterLinkSupplierID"].ToString();
			FillData();
		}
	}

	public override void btnRefreshDataClick()
	{
		dtItems = Items.FillCombo("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlItemsDual.ValueListItems.Clear();
		vlItemsQuad.ValueListItems.Clear();
		for (int i = 0; i < dtItems.Rows.Count; i++)
		{
			vlItemsDual.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
			vlItemsQuad.ValueListItems.Add(dtItems.Rows[i]["ItemID"], dtItems.Rows[i]["Name"].ToString());
		}
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		base.SelectFullRow(sender, e);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Diameter" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Length" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "Width" || ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "WorkingLoadLimit"))
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void ULGDataQuad_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGDataQuad.ActiveCell != null && (((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "Diameter" || ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "Length" || ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "Width" || ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "Diameter2" || ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "Length2" || ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "Width2" || ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "WorkingLoadLimit"))
		{
			GlobalFunctions.CheckForNumbers(ULGDataQuad.ActiveCell, e);
		}
	}

	private void ULGDataQuad_AfterEnterEditMode(object sender, EventArgs e)
	{
		if (!Adding && !Updating)
		{
			((GridItemBase)((UltraGridBase)ULGDataQuad).ActiveRow).Selected = true;
		}
	}

	private void btnSelectItems_Click(object sender, EventArgs e)
	{
		if ((!Adding && !Updating) || ((UltraTabControlBase)UTCDetails).ActiveTab == null)
		{
			return;
		}
		DataTable dataTable = SearchFunctions.ItemsReport("-1", "-1", "0", "-1", "-1", "0", "-1", "-1", "-1", "-1", IsFromServer: false);
		if (((UltraTabControlBase)UTCDetails).ActiveTab.Index == 0)
		{
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (dtDetails.Select("MasterLinkDualItemID = " + dataTable.Rows[i]["ItemID"].ToString()).Length == 0)
				{
					DataRow dataRow = dtDetails.NewRow();
					dataRow["MasterLinkSupplierID"] = (Adding ? ((object)(-1)) : drMaster["MasterLinkSupplierID"]);
					dataRow["MasterLinkDualID"] = -1;
					dataRow["MasterLinkDualItemID"] = dataTable.Rows[i]["ItemID"];
					dataRow["Deleted"] = false;
					dtDetails.Rows.Add(dataRow);
				}
			}
			((UltraGridBase)ULGData).UpdateData();
			return;
		}
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			if (dtDetails.Select("MasterLinkQuadItemID = " + dataTable.Rows[j]["ItemID"].ToString()).Length == 0)
			{
				DataRow dataRow2 = dtMasterLinksQuad.NewRow();
				dataRow2["MasterLinkSupplierID"] = (Adding ? ((object)(-1)) : drMaster["MasterLinkSupplierID"]);
				dataRow2["MasterLinkQuadID"] = -1;
				dataRow2["MasterLinkQuadItemID"] = dataTable.Rows[j]["ItemID"];
				dataRow2["Deleted"] = false;
				dtMasterLinksQuad.Rows.Add(dataRow2);
			}
		}
		((UltraGridBase)ULGDataQuad).UpdateData();
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "MasterLinkDualItemID")
		{
			int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
			if (num != 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["MasterLinkDualItemID"].Value = num;
			}
		}
		e.Handled = true;
	}

	public virtual void ULGDataQuad_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void ULGDataQuad_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.F8)
		{
			return;
		}
		if ((Adding || Updating) && ((KeyedSubObjectBase)ULGDataQuad.ActiveCell.Column).Key == "MasterLinkQuadItemID")
		{
			int num = (Adding ? SearchFunctions.Items("-1", "-1", "-1", "-1", "1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false) : SearchFunctions.Items("-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", "-1", IsFromServer: false));
			if (num != 0)
			{
				((UltraGridBase)ULGDataQuad).ActiveRow.Cells["MasterLinkQuadItemID"].Value = num;
			}
		}
		e.Handled = true;
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
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Sling.MasterData.frmMasterLinksSuppliers));
		UltraTab val = new UltraTab();
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
		this.ultraTabPageControl2 = new UltraTabPageControl();
		this.ULGDataQuad = new UltraGrid();
		this.lblNameEn = new UltraLabel();
		this.txtNameEn = new UltraTextEditor();
		this.lblNameAr = new UltraLabel();
		this.txtNameAr = new UltraTextEditor();
		this.btnSelectItems = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGDataQuad).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.UTCDetails, "UTCDetails");
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ultraTabPageControl2);
		((UltraTabControlBase)base.UTCDetails).TabPageMargins.ForceSerialization = true;
		val.TabPage = this.ultraTabPageControl2;
		resources.ApplyResources(val, "ultraTab1");
		((SubObjectBase)val).ForceApplyResources = "";
		((UltraTabControlBase)base.UTCDetails).Tabs.AddRange((UltraTab[])(object)new UltraTab[1] { val });
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ultraTabPageControl1, 0);
		((System.Windows.Forms.Control)(object)base.UTCDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraTabPageControl2, 0);
		resources.ApplyResources(base.ULGData, "ULGData");
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val2, "appearance6");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val3).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val3, "appearance7");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val4).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val4, "appearance8");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val4;
		((AppearanceBase)val5).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val5, "appearance9");
		((AppearanceBase)val5).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val6).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val6).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val6).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val6, "appearance10");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val7).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val7, "appearance11");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val8, "appearance12");
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)base.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.ultraTabPageControl1, "ultraTabPageControl1");
		resources.ApplyResources(base.btnSetting, "btnSetting");
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance13");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnAttachFile, "btnAttachFile");
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.ultraTabPageControl2, "ultraTabPageControl2");
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Controls.Add((System.Windows.Forms.Control)(object)this.ULGDataQuad);
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).Name = "ultraTabPageControl2";
		resources.ApplyResources(this.ULGDataQuad, "ULGDataQuad");
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance1");
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val11).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val11).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val11).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val11, "appearance2");
		((AppearanceBase)val11).ThemedElementAlpha = (Alpha)3;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val11;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val12).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(val12, "appearance3");
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val12;
		((AppearanceBase)val13).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val13).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val13).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(val13, "appearance4");
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val13;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val14).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val14).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val14).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance5");
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)this.ULGDataQuad).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGDataQuad).Name = "ULGDataQuad";
		((UltraControlBase)this.ULGDataQuad).UseFlatMode = (DefaultableBoolean)1;
		this.ULGDataQuad.AfterEnterEditMode += new System.EventHandler(ULGDataQuad_AfterEnterEditMode);
		this.ULGDataQuad.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGDataQuad_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGDataQuad).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGDataQuad_KeyDown);
		((System.Windows.Forms.Control)(object)this.ULGDataQuad).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGDataQuad_KeyPress);
		resources.ApplyResources(this.lblNameEn, "lblNameEn");
		this.lblNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameEn).Name = "lblNameEn";
		((ControlBase)this.lblNameEn).WrapText = false;
		resources.ApplyResources(this.txtNameEn, "txtNameEn");
		resources.ApplyResources(val15, "appearance14");
		((TextEditorControlBase)this.txtNameEn).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.txtNameEn).Name = "txtNameEn";
		resources.ApplyResources(this.lblNameAr, "lblNameAr");
		this.lblNameAr.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblNameAr).Name = "lblNameAr";
		((ControlBase)this.lblNameAr).WrapText = false;
		resources.ApplyResources(this.txtNameAr, "txtNameAr");
		((System.Windows.Forms.Control)(object)this.txtNameAr).Name = "txtNameAr";
		resources.ApplyResources(this.btnSelectItems, "btnSelectItems");
		((System.Windows.Forms.Control)(object)this.btnSelectItems).Name = "btnSelectItems";
		((System.Windows.Forms.Control)(object)this.btnSelectItems).Click += new System.EventHandler(btnSelectItems_Click);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSelectItems);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtNameAr);
		base.Name = "frmMasterLinksSuppliers";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.txtCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UTCDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSelectItems, 0);
		((System.ComponentModel.ISupportInitialize)base.UTCDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UTCDetails).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.Windows.Forms.Control)(object)base.ultraTabPageControl1).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.ultraTabPageControl2).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGDataQuad).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtNameAr).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
