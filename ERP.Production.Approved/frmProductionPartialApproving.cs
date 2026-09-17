using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Accounting;
using BusinessLayer.Production;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Production.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Production.Approved;

public class frmProductionPartialApproving : frmPosted
{
	private DataTable dtProductionsStages;

	private DataTable dtProductionsStagesApprove;

	private DataSet ds;

	private ArrayList List = new ArrayList();

	private int newID = -100000;

	private IContainer components = null;

	public frmProductionPartialApproving()
	{
		InitializeComponent();
		NoCol = "ProductionNo";
	}

	public override void FillGrid()
	{
		List.Clear();
		dtsource = Productions.SelectByProductionApproved(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		dtProductionsStages = ProductionsStages.SelectByProductionApproved(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		dtProductionsStagesApprove = ProductionsStagesApprove.SelectByProductionApproved(GlobalVariables.BranchIDs, "0", GlobalVariables.IsArabic ? "1" : "0");
		ds = new DataSet();
		ds.Tables.Add(dtsource);
		ds.Tables.Add(dtProductionsStages);
		ds.Tables.Add(dtProductionsStagesApprove);
		ds.Tables[0].TableName = "dtsource";
		ds.Tables[1].TableName = "dtProductionsStages";
		ds.Tables[2].TableName = "dtProductionsStagesApprove";
		ds.Relations.Add(ds.Tables[0].Columns["ProductionID"], ds.Tables[1].Columns["ProductionID"]);
		ds.Relations.Add(ds.Tables[1].Columns["ProductionStageID"], ds.Tables[2].Columns["ProductionStageID"]);
		((UltraGridBase)ULGData).DataSource = ds;
		InitGrid();
		((Control)(object)btnHeaderSearch).Visible = false;
	}

	public override void InitGrid()
	{
		//IL_0ad3: Unknown result type (might be due to invalid IL or missing references)
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.13);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم" : "No");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ProductionEndDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BatchNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم التشغيلة" : "Batch NO");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Qty"].Header).Caption = (GlobalVariables.IsArabic ? "الكمية" : "Qty");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ItemCatalogeName"].Header).Caption = (GlobalVariables.IsArabic ? "وصفة الانتاج" : "Item Catalog");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LineName"].Header).Caption = (GlobalVariables.IsArabic ? "الخط" : "Line");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "إغلاق" : "Close");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StageName"].Header).Caption = (GlobalVariables.IsArabic ? "المرحلة" : "Stage");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CompletePercentage"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه التامة" : "Complete Percent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["StageName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CompletePercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Approved"].Header).Caption = (GlobalVariables.IsArabic ? "إغلاق" : "Close");
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[1].Columns["CompletePercentage"].DefaultCellValue = 0;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Date"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CompletePercentage"].Header).Caption = (GlobalVariables.IsArabic ? "الكميه التامة" : "Complete Percent");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Date"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CompletePercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["CompletePercentage"].DefaultCellValue = 0;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Date"].DefaultCellValue = DateTime.Now;
		((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Date"].MaskInput = "dd/mm/yyyy hh:mm:ss tt";
		((UltraDateTimeEditor)((UltraGridBase)ULGData).DisplayLayout.Bands[2].Columns["Date"].EditorComponent).SpinButtonDisplayStyle = (ButtonDisplayStyle)2;
	}

	public override void SelectFullRow()
	{
		if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0 || ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 1)
		{
			if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved")
			{
				((GridItemBase)ULGData.ActiveCell).Selected = true;
			}
		}
		else if (((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 2 && (int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionStageApproveID"].Value.ToString()) < -100000 || int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionStageApproveID"].Value.ToString()) > 0))
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void btnPost_Click(object sender, EventArgs e)
	{
		double num = 0.0;
		if (!ValidateData())
		{
			return;
		}
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			num = 0.0;
			if (!List.Contains(((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value))
			{
				continue;
			}
			Main.StartBulkTrans(FromServer: false);
			try
			{
				MessageLog.DeleteByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString(), "ProMIV", "ProMIV");
				for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
				{
					ProductionsStages.SetApprove("," + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionStageID"].Value.ToString() + ",", "," + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionID"].Value.ToString() + ",", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CompletePercentage"].Value.ToString(), bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Approved"].Value.ToString()) ? "1" : "0", bool.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Approved"].Value.ToString()) ? DateTime.Now.ToString(GlobalVariables.DateLongFormate) : "Null", "1", GlobalVariables.UserID);
					for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows).Count; k++)
					{
						if (int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ProductionStageApproveID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ProductionStageApproveID"].Value.ToString()) < -1)
						{
							ProductionsStagesApprove.Insert_Update("-1", ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionStageID"].Value.ToString(), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["CompletePercentage"].Value.ToString(), DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Date"].Value.ToString()).AddMilliseconds(num).ToString(GlobalVariables.DateLongFormateMS), ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Notes"].Value.ToString(), "1", (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["StockControlJVID"].Value == DBNull.Value) ? "Null" : ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["StockControlJVID"].Value.ToString(), "0", ((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value.ToString(), GlobalVariables.UserID);
							ItemsTransactions.ManagementInsertUpdateDelete();
							ItemsTransactions.RecalculateCurrentQtyOnly();
							num += 10.0;
						}
					}
				}
				if (((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
				{
					Productions.SetApprove("," + ((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString() + ",", "1");
				}
				string text = MessageLog.SelectByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString(), "ProMIV", "ProMIV", GlobalVariables.IsArabic ? "1" : "0");
				if (text != "")
				{
					GlobalVariables.InformationMB.Show(text);
					Main.RollbackBulkTrans(FromServer: false);
					MessageLog.DeleteByVoucherIDAndTransType(((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString(), "ProMIV", "ProMIV");
				}
				else
				{
					Main.EndBulkTrans(FromServer: false);
				}
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				FillGrid();
				return;
			}
		}
		if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count < count)
		{
			GlobalVariables.InformationMB.Show("تمت العملي\u0651ة بنجاح", "Operation done successfully");
		}
		FillGrid();
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 0)
		{
			frmProductions frmProductions2 = new frmProductions(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value.ToString()));
			frmProductions2.Size = new Size(base.Width, base.Height);
			frmProductions2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmProductions2.lblTitle).Text = (GlobalVariables.IsArabic ? "الانتاج" : "Production");
			frmProductions2.ShowDialog();
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
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Band.Index == 2)
		{
			((TextEditorControlBase)txtCode).ValueChanged -= txtCode_ValueChanged;
			((Control)(object)txtCode).Text = ((UltraGridBase)ULGData).ActiveRow.ParentRow.ParentRow.Cells[NoCol].Value.ToString();
			((TextEditorControlBase)txtCode).ValueChanged += txtCode_ValueChanged;
		}
	}

	private void ULGData_AfterCellUpdate(object sender, CellEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		((UltraGridBase)ULGData).UpdateData();
		if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)e.Cell).Band.Index == 0)
		{
			if (!List.Contains(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value))
			{
				List.Add(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value);
			}
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)e.Cell).Band.Index == 1)
		{
			e.Cell.Row.ParentRow.Cells["Approved"].Value = dtProductionsStages.Select(" Approved=0 And ProductionID= " + e.Cell.Row.Cells["ProductionID"].Value.ToString()).Length == 0;
			if (!List.Contains(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value))
			{
				List.Add(((UltraGridBase)ULGData).ActiveRow.Cells["ProductionID"].Value);
			}
		}
		else if (((UltraGridBase)ULGData).ActiveRow != null && ((GridItemBase)e.Cell).Band.Index == 2)
		{
			object obj = dtProductionsStagesApprove.Compute("Sum(CompletePercentage)", "ProductionStageID=" + ((UltraGridBase)ULGData).ActiveRow.Cells["ProductionStageID"].Value.ToString());
			e.Cell.Row.ParentRow.Cells["CompletePercentage"].Value = ((obj == DBNull.Value) ? "0" : obj);
			e.Cell.Row.ParentRow.Cells["Approved"].Value = ((decimal.Parse(e.Cell.Row.ParentRow.Cells["CompletePercentage"].Value.ToString()) == decimal.Parse(e.Cell.Row.ParentRow.ParentRow.Cells["Qty"].Value.ToString())) ? true : false);
			((UltraGridBase)ULGData).UpdateData();
			e.Cell.Row.ParentRow.ParentRow.Cells["Approved"].Value = dtProductionsStages.Select(" Approved=0 And ProductionID= " + e.Cell.Row.ParentRow.Cells["ProductionID"].Value.ToString()).Length == 0;
			if (!List.Contains(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["ProductionID"].Value))
			{
				List.Add(((UltraGridBase)ULGData).ActiveRow.ParentRow.Cells["ProductionID"].Value);
			}
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_AfterRowsDeleted(object sender, EventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				object obj = dtProductionsStagesApprove.Compute("Sum(CompletePercentage)", "ProductionStageID=" + ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["ProductionStageID"].Value.ToString());
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CompletePercentage"].Value = ((obj == DBNull.Value) ? "0" : obj);
				((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["Approved"].Value = ((decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CompletePercentage"].Value.ToString()) == decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString())) ? true : false);
			}
			((UltraGridBase)ULGData).UpdateData();
			((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value = dtProductionsStages.Select(" Approved=0 And ProductionID= " + ((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value.ToString()).Length == 0;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
			return;
		}
		for (int i = 0; i < e.Rows.Length; i++)
		{
			if (!List.Contains(e.Rows[i].ParentRow.Cells["ProductionID"].Value))
			{
				List.Add(e.Rows[i].ParentRow.Cells["ProductionID"].Value);
			}
		}
	}

	private void ULGData_AfterRowInsert(object sender, RowEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		ULGData.AfterCellUpdate -= new CellEventHandler(ULGData_AfterCellUpdate);
		if (((GridItemBase)e.Row).Band.Index == 2)
		{
			e.Row.Cells["ProductionStageApproveID"].Value = ++newID;
		}
		ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
	}

	private void ULGData_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (ULGData.ActiveCell != null && ((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key == "CompletePercentage")
		{
			GlobalFunctions.CheckForNumbers(ULGData.ActiveCell, e);
		}
	}

	public bool ValidateData()
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (!List.Contains(((UltraGridBase)ULGData).Rows[i].Cells["ProductionID"].Value))
			{
				continue;
			}
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CompletePercentage"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CompletePercentage"].Value.ToString()) > decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["Qty"].Value.ToString()))
				{
					GlobalVariables.InformationMB.Show(" لابد أن تكون الكميه التامة أقل من او تساوى الكمية الكلية ", "Completed Qty Must Equal Or Less Than total Qty");
					ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].Cells["CompletePercentage"];
					ULGData.PerformAction((UltraGridAction)24);
					return false;
				}
				for (int k = 0; k < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows).Count; k++)
				{
					if (int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ProductionStageApproveID"].Value.ToString()) >= -100000 && int.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["ProductionStageApproveID"].Value.ToString()) < -1)
					{
						if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Date"].Value == DBNull.Value)
						{
							GlobalVariables.InformationMB.Show(" برجاء إدخال التاريخ", "Please insert Date");
							ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Date"];
							ULGData.PerformAction((UltraGridAction)24);
							return false;
						}
						if (!FiscalYear.ChkForConfirmedFiscalYear(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Date"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), IsFromServer: false))
						{
							GlobalVariables.InformationMB.Show("السنة المالية غير معتمدة", "The Fiscal Year is not confirmed..");
							return false;
						}
						if (FiscalYear.ChkForClosingFsicalPeriod(DateTime.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["Date"].Value.ToString()).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID, IsFromServer: false))
						{
							GlobalVariables.InformationMB.Show("لقد قمت بأختيار تاريخ يقع في فتره ماليه مغلقة", "The Date you choosed\n\r exists in closed fisical period");
							return false;
						}
						if (((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["CompletePercentage"].Value == DBNull.Value || decimal.Parse(((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["CompletePercentage"].Value.ToString()) <= 0m)
						{
							GlobalVariables.InformationMB.Show(" برجاء إدخال الكميه التامة", "Please insert Complete Percentage");
							ULGData.ActiveCell = ((UltraGridBase)ULGData).Rows[i].ChildBands[0].Rows[j].ChildBands[0].Rows[k].Cells["CompletePercentage"];
							ULGData.PerformAction((UltraGridAction)24);
							return false;
						}
					}
				}
			}
		}
		return true;
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
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Production.Approved.frmProductionPartialApproving));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.InterBandSpacing = 10;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val2).ThemedElementAlpha = (Alpha)3;
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance2.FontData");
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((AppearanceBase)val3).BorderColor = System.Drawing.Color.FromArgb(168, 167, 191);
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance3.FontData");
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)2;
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance4.FontData");
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectorAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSpacingBefore = 2;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(247, 247, 249);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(((AppearanceBase)val5).FontData, "appearance5.FontData");
		resources.ApplyResources(val5, "appearance5");
		((SubObjectBase)val5).ForceApplyResources = "FontData|";
		((UltraGridBase)base.ULGData).DisplayLayout.Override.SelectedRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(168, 167, 191);
		((UltraGridBase)base.ULGData).DisplayLayout.RowConnectorStyle = (RowConnectorStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		base.ULGData.AfterCellUpdate += new CellEventHandler(ULGData_AfterCellUpdate);
		base.ULGData.AfterRowsDeleted += new System.EventHandler(ULGData_AfterRowsDeleted);
		base.ULGData.AfterRowInsert += new RowEventHandler(ULGData_AfterRowInsert);
		base.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)base.ULGData).KeyPress += new System.Windows.Forms.KeyPressEventHandler(ULGData_KeyPress);
		resources.ApplyResources(base.UGBByName, "UGBByName");
		resources.ApplyResources(((AppearanceBase)val6).FontData, "appearance6.FontData");
		resources.ApplyResources(val6, "appearance6");
		((SubObjectBase)val6).ForceApplyResources = "FontData|";
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val6;
		resources.ApplyResources(this, "$this");
		base.Name = "frmProductionPartialApproving";
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.UGBByName).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBByName).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtsource).EndInit();
		base.ResumeLayout(false);
	}
}
