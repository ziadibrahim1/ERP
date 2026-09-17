using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Clinics;
using BusinessLayer.CRM;
using BusinessLayer.CustomsClearence;
using BusinessLayer.FixedAssets;
using BusinessLayer.HR;
using BusinessLayer.MarineService;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmLevels : frmDetails
{
	private int LevelsCount;

	private DataTable dtUsedLevel = new DataTable();

	private IContainer components = null;

	private UltraLabel lblBranchEnglishName;

	private UltraTextEditor txtLevelCount;

	public UltraButton btnReGenerateCode;

	public frmLevels()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		cboHeader.Items.Add((object)1, GlobalVariables.IsArabic ? "الحسابات" : "Accounts");
		cboHeader.Items.Add((object)2, GlobalVariables.IsArabic ? " الحسابات التحليلية" : "SubAccounts");
		cboHeader.Items.Add((object)3, GlobalVariables.IsArabic ? "مراكز التكلفة" : "Cost Center");
		cboHeader.Items.Add((object)4, GlobalVariables.IsArabic ? "ألأصناف" : "Items");
		cboHeader.Items.Add((object)5, GlobalVariables.IsArabic ? "الهيكل الادارى" : "Administrative Structure");
		cboHeader.Items.Add((object)6, GlobalVariables.IsArabic ? "خدمات بحرية" : "Marine Services");
		cboHeader.Items.Add((object)7, GlobalVariables.IsArabic ? "التحاليل الطبية" : "Bio-Analysis");
		cboHeader.Items.Add((object)8, GlobalVariables.IsArabic ? "مواقع الأصول" : "Assets Locations");
		cboHeader.Items.Add((object)9, GlobalVariables.IsArabic ? "عملاء CRM" : "CRM Customers");
		cboHeader.Items.Add((object)10, GlobalVariables.IsArabic ? "المصدرين" : "Exporters");
		cboHeader.Items.Add((object)11, GlobalVariables.IsArabic ? "المستلمين" : "Consignees");
		dtDetails = AccountLevels.Select("0", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DataSource = dtDetails;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LevelID"].Header).Caption = (GlobalVariables.IsArabic ? "رقم المستوى" : "Level No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LevelID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LevelID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].Header).Caption = (GlobalVariables.IsArabic ? "سعة المستوى" : "Level Width");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Width"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
	}

	public override void DisplayData()
	{
		base.DisplayData();
		if (cboHeader.SelectedIndex > -1)
		{
			if (((TextEditorControlBase)cboHeader).Value.ToString() == "1")
			{
				dtDetails = AccountLevels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = Accounts.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "2")
			{
				dtDetails = SubAccounts_Levels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = SubAccounts.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "3")
			{
				dtDetails = CostCenterLevels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = CostCenters.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "4")
			{
				dtDetails = ItemLevels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = Items.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "5")
			{
				dtDetails = AdministrativeStructure_Levels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = AdministrativeStructure.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "6")
			{
				dtDetails = BusinessLayer.MarineService.ServicesLevels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = BusinessLayer.MarineService.Services.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "7")
			{
				dtDetails = BioAnalysis_Levels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = BioAnalysis.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "8")
			{
				dtDetails = AssetsLocationsLevels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = AssetsLocations.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "9")
			{
				dtDetails = Customers_Levels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = Customers_Levels.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "10")
			{
				dtDetails = Exporters_Levels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = Exporters.SelectLevels(IsFromServer: true);
			}
			else if (((TextEditorControlBase)cboHeader).Value.ToString() == "11")
			{
				dtDetails = Consignees_Levels.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
				dtUsedLevel = Consignees.SelectLevels(IsFromServer: true);
			}
			((Control)(object)txtLevelCount).Text = dtDetails.Rows.Count.ToString();
			LevelsCount = dtDetails.Rows.Count;
			InitGrid();
		}
		else
		{
			dtDetails.Rows.Clear();
			dtUsedLevel.Rows.Clear();
			((Control)(object)txtLevelCount).Text = "";
			InitGrid();
		}
	}

	public override bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Width"].Value == DBNull.Value || int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Width"].Value.ToString()) <= 0)
			{
				GlobalVariables.InformationMB.Show("برجاء إدخال سعة المستوى", "Please Enter level Width");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
				return false;
			}
		}
		return true;
	}

	public override void SelectFullRow(object sender, EventArgs e)
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "LevelID")
		{
			((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
		}
	}

	public override void SaveData()
	{
		if (cboHeader.SelectedIndex <= -1)
		{
			return;
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "1")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				AccountLevels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				Accounts.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "2")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				SubAccounts_Levels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				SubAccounts.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "3")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				CostCenterLevels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				CostCenters.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "4")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				ItemLevels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				GlobalVariables.QuestionMB.Show("هل تريد تعديل الباركود ليكون نفس كود الصنف ؟", "Do You Want Update BarCode To Be the Same of ItemCode ?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					GlobalVariables.QuestionMB.Show("مع ملاحظة إنه سوف يتم تحديث كل الباركود للاصناف هل تريد التعديل ؟", "Note This Action Will Update All BarCode For All Items Do You Want to Update ?");
					if (GlobalVariables.MessageBoxResult == 'Y')
					{
						Items.ReGenerateCode("1", IsFromServer: true);
					}
					else
					{
						Items.ReGenerateCode("0", IsFromServer: true);
					}
				}
				else
				{
					Items.ReGenerateCode("0", IsFromServer: true);
				}
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "5")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				AdministrativeStructure_Levels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				AdministrativeStructure.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "6")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				BusinessLayer.MarineService.ServicesLevels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				BusinessLayer.MarineService.Services.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "7")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				BioAnalysis_Levels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				GlobalVariables.QuestionMB.Show("هل تريد تعديل الكود ليكون نفس كود التحليل ؟", "Do You Want Update InternationCode To Be the Same of BioAnalysis Code ?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					GlobalVariables.QuestionMB.Show("مع ملاحظة إنه سوف يتم تحديث كل الكود للتحاليل هل تريد التعديل ؟", "Note This Action Will Update All InternationCode For All BioAnalysis Do You Want to Update ?");
					if (GlobalVariables.MessageBoxResult == 'Y')
					{
						BioAnalysis.ReGenerateCode("1", IsFromServer: true);
					}
					else
					{
						BioAnalysis.ReGenerateCode("0", IsFromServer: true);
					}
				}
				else
				{
					BioAnalysis.ReGenerateCode("0", IsFromServer: true);
				}
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "8")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				AssetsLocationsLevels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				AssetsLocations.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "9")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				Customers_Levels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				Customers.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "10")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				Exporters_Levels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				Exporters.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
				return;
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
				return;
			}
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "11")
		{
			Main.StartBulkTrans(FromServer: true);
			try
			{
				Consignees_Levels.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID, IsFromServer: true);
				Consignees.ReGenerateCode(IsFromServer: true);
				Main.EndBulkTrans(FromServer: true);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: true);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				DisplayData();
			}
		}
	}

	public bool Check_Delete_ForError(int Row_Index, DataTable dt_Levels)
	{
		for (int i = 0; i < dt_Levels.Rows.Count; i++)
		{
			if (Row_Index == int.Parse(dt_Levels.Rows[i][0].ToString()))
			{
				return true;
			}
		}
		return false;
	}

	private void txtLevelCount_ValueChanged(object sender, EventArgs e)
	{
		if (((Control)(object)txtLevelCount).Text == "")
		{
			((Control)(object)txtLevelCount).Text = LevelsCount.ToString();
			return;
		}
		int num = 0;
		num = int.Parse(((Control)(object)txtLevelCount).Text);
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count < num)
		{
			int num2 = num - ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
			for (int i = 0; i < num2; i++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)1;
				((UltraGridBase)ULGData).DisplayLayout.Bands[0].AddNew();
				((UltraGridBase)ULGData).Rows[LevelsCount].Cells["LevelID"].Value = LevelsCount + 1;
				((UltraGridBase)ULGData).Rows[LevelsCount].Cells["Width"].Value = 0;
				((UltraGridBase)ULGData).Rows[LevelsCount].Cells["Deleted"].Value = 0;
				((UltraGridBase)ULGData).Rows[LevelsCount].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
				LevelsCount++;
				((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
			}
		}
		else
		{
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count == num)
			{
				return;
			}
			int num3 = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count - num;
			((UltraGridBase)ULGData).UpdateData();
			DataRow[] array = ((DataTable)((UltraGridBase)ULGData).DataSource).Select("LevelID >" + num);
			for (int j = 0; j < array.Length; j++)
			{
				if (Check_Delete_ForError(int.Parse(array[j]["LevelID"].ToString()), dtUsedLevel))
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لايمكن حذف هذا المستوى" : "This Level Cannot Be Deleted");
					((Control)(object)txtLevelCount).Text = LevelsCount.ToString();
					break;
				}
				((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Remove(array[j]);
			}
			LevelsCount = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
			((UltraGridBase)ULGData).UpdateData();
		}
	}

	private void txtLevelCount_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null)
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	private void btnReGenerateCode_Click(object sender, EventArgs e)
	{
		if (cboHeader.SelectedIndex <= -1)
		{
			return;
		}
		if (((TextEditorControlBase)cboHeader).Value.ToString() == "1")
		{
			Accounts.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "2")
		{
			SubAccounts.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "3")
		{
			CostCenters.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "4")
		{
			GlobalVariables.QuestionMB.Show("هل تريد تعديل الباركود ليكون نفس كود الصنف ؟", "Do You Want Update BarCode To Be the Same of ItemCode ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				GlobalVariables.QuestionMB.Show("مع ملاحظة إنه سوف يتم تحديث كل الباركود للاصناف هل تريد التعديل ؟", "Note This Action Will Update All BarCode For All Items Do You Want to Update ?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					Items.ReGenerateCode("1", IsFromServer: true);
				}
				else
				{
					Items.ReGenerateCode("0", IsFromServer: true);
				}
			}
			else
			{
				Items.ReGenerateCode("0", IsFromServer: true);
			}
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "5")
		{
			AdministrativeStructure.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "6")
		{
			BusinessLayer.MarineService.Services.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "7")
		{
			GlobalVariables.QuestionMB.Show("هل تريد تعديل الكود ليكون نفس كود التحليل ؟", "Do You Want Update InternationalCode To Be the Same of Bio-Analysis Code ?");
			if (GlobalVariables.MessageBoxResult == 'Y')
			{
				GlobalVariables.QuestionMB.Show("مع ملاحظة إنه سوف يتم تحديث كل الكود للتحاليل هل تريد التعديل ؟", "Note This Action Will Update All InternationalCode For All Analysis Do You Want to Update ?");
				if (GlobalVariables.MessageBoxResult == 'Y')
				{
					BioAnalysis.ReGenerateCode("1", IsFromServer: true);
				}
				else
				{
					BioAnalysis.ReGenerateCode("0", IsFromServer: true);
				}
			}
			else
			{
				BioAnalysis.ReGenerateCode("0", IsFromServer: true);
			}
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "8")
		{
			AssetsLocations.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "9")
		{
			Customers.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "10")
		{
			Exporters.ReGenerateCode(IsFromServer: true);
		}
		else if (((TextEditorControlBase)cboHeader).Value.ToString() == "11")
		{
			Consignees.ReGenerateCode(IsFromServer: true);
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmLevels));
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
		this.lblBranchEnglishName = new UltraLabel();
		this.txtLevelCount = new UltraTextEditor();
		this.btnReGenerateCode = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtLevelCount).BeginInit();
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
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.btnCancel, "btnCancel");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.cboHeader, "cboHeader");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
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
		resources.ApplyResources(val9, "appearance9");
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
		resources.ApplyResources(this.lblBranchEnglishName, "lblBranchEnglishName");
		this.lblBranchEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBranchEnglishName).Name = "lblBranchEnglishName";
		((ControlBase)this.lblBranchEnglishName).WrapText = false;
		resources.ApplyResources(this.txtLevelCount, "txtLevelCount");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtLevelCount).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtLevelCount).Name = "txtLevelCount";
		((TextEditorControlBase)this.txtLevelCount).ValueChanged += new System.EventHandler(txtLevelCount_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtLevelCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtLevelCount_KeyPress);
		resources.ApplyResources(this.btnReGenerateCode, "btnReGenerateCode");
		((System.Windows.Forms.Control)(object)this.btnReGenerateCode).Name = "btnReGenerateCode";
		((System.Windows.Forms.Control)(object)this.btnReGenerateCode).Click += new System.EventHandler(btnReGenerateCode_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReGenerateCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBranchEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtLevelCount);
		base.Name = "frmLevels";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveAndClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtLevelCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBranchEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReGenerateCode, 0);
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtLevelCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
