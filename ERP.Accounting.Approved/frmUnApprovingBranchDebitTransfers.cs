using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Accounting.Approved;

public class frmUnApprovingBranchDebitTransfers : frmPosted
{
	private IContainer components = null;

	public frmUnApprovingBranchDebitTransfers()
	{
		InitializeComponent();
		NoCol = "BranchDebitTransferNo";
		((Control)(object)btnPost).Text = (GlobalVariables.IsArabic ? "فك إعتماد" : "Unapprove");
	}

	public override void FillGrid()
	{
		dtsource = BranchDebitTransfers.Search("," + GlobalVariables.CurrentBranchID + ",", DateTime.Now.AddYears(-20).ToString(GlobalVariables.DateShortFormate), DateTime.Now.AddYears(20).ToString(GlobalVariables.DateShortFormate), "1", "0", GlobalVariables.IsArabic ? "1" : "0", GlobalVariables.SeeingInvisibleAcounts ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtsource;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferNo"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferNo"].Header).Caption = (GlobalVariables.IsArabic ? "الرقم" : "No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferDate"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchDebitTransferDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDebit"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDebit"].Header).Caption = (GlobalVariables.IsArabic ? "الاجمالى" : "Total Value");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TotalDebit"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header).Caption = "";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Width = (int)((double)((Control)(object)ULGData).Width * 0.05);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].AllowRowFiltering = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxVisibility = (HeaderCheckBoxVisibility)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Approved"].Header.CheckBoxAlignment = (HeaderCheckBoxAlignment)2;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Hidden = false;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Header).Caption = (GlobalVariables.IsArabic ? "فرع" : "Branch");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["BranchName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void SelectFullRow()
	{
		if (((KeyedSubObjectBase)ULGData.ActiveCell.Column).Key != "Approved")
		{
			((GridItemBase)ULGData.ActiveCell).Selected = true;
		}
	}

	public override void SaveData()
	{
		string text = ",";
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["Approved"].Value.Equals(true))
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["BranchDebitTransferID"].Value.ToString() + ",";
			}
		}
		if (text != ",")
		{
			BranchDebitTransfers.SetApprove("0", text);
			FillGrid();
		}
		else
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "لا توجد تحويل مديونية بين الفروع لفك إعتمادها " : "There are No Branch Debit Transfer to UnApprove");
		}
	}

	public override void ClickCellButton()
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			frmBranchDebitTransfer frmBranchDebitTransfer2 = new frmBranchDebitTransfer(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["BranchDebitTransferID"].Value.ToString()));
			frmBranchDebitTransfer2.Size = new Size(base.Width, base.Height);
			frmBranchDebitTransfer2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmBranchDebitTransfer2.lblTitle).Text = (GlobalVariables.IsArabic ? "تحويل مديونيه بين الفروع" : "Branch Transfer Debit");
			frmBranchDebitTransfer2.ShowDialog();
		}
	}

	public override void Search()
	{
		DataTable dataTable = SearchFunctions.BranchDebitTransfersReport(1, 0, IsFromServer: false);
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (dataTable.Rows[i]["BranchDebitTransferID"].ToString() == ((UltraGridBase)ULGData).Rows[j].Cells["BranchDebitTransferID"].Value.ToString())
				{
					((UltraGridBase)ULGData).Rows[j].Cells["Approved"].Value = true;
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
		this.components = new System.ComponentModel.Container();
	}
}
