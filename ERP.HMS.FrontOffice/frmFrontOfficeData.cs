using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.HMS;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HMS.FrontOffice;

public class frmFrontOfficeData : frmHeaderDetails
{
	private ValueList vlFrom = new ValueList();

	private ValueList vlTo = new ValueList();

	private IContainer components = null;

	private UltraLabel lblRoomsRevenue;

	private UltraTextEditor txtRoomsRevenue;

	private UltraLabel lblDate;

	private UltraDateTimeEditor dtpDate;

	private UltraLabel lblRoomsRevenueNote;

	private UltraTextEditor txtRoomsRevenueNote;

	private UltraLabel lblAvailableRoomsNote;

	private UltraTextEditor txtAvailableRoomsNote;

	private UltraLabel lblAvailableRooms;

	private UltraTextEditor txtAvailableRooms;

	private UltraLabel lblSoldRoomsAverage;

	private UltraTextEditor txtSoldRoomsAverage;

	private UltraLabel lblOccupancyPercentage;

	private UltraTextEditor txtOccupancyPercentage;

	private UltraLabel lblSoldRoomsNote;

	private UltraTextEditor txtSoldRoomsNote;

	private UltraLabel lblSoldRooms;

	private UltraTextEditor txtSoldRooms;

	private UltraLabel lblAvailableRoomsAverage;

	private UltraTextEditor txtAvailableRoomsAverage;

	private UltraLabel lblOutOfServiceRooms;

	private UltraTextEditor txtOutOfServiceRooms;

	private UltraLabel HostingRooms;

	private UltraTextEditor txtHostingRooms;

	private UltraLabel lblHostingRoomsNote;

	private UltraTextEditor txtHostingRoomsNote;

	private UltraLabel lblOutOfServiceRoomsNote;

	private UltraTextEditor txtOutOfServiceRoomsNote;

	private UltraLabel lblSingleNote;

	private UltraTextEditor txtSingleNote;

	private UltraLabel lblSingle;

	private UltraTextEditor txtSingle;

	private UltraLabel lblDoubleNote;

	private UltraTextEditor txtDoubleNote;

	private UltraLabel lblDouble;

	private UltraTextEditor txtDouble;

	private UltraLabel lblQuartetNote;

	private UltraTextEditor txtQuartetNote;

	private UltraLabel lblQuartet;

	private UltraTextEditor txtQuartet;

	private UltraLabel lblTripleNote;

	private UltraTextEditor txtTripleNote;

	private UltraLabel lblTriple;

	private UltraTextEditor txtTriple;

	private UltraLabel lblBONote;

	private UltraTextEditor txtBONote;

	private UltraLabel lblBO;

	private UltraTextEditor txtBO;

	private UltraLabel lblBBNote;

	private UltraTextEditor txtBBNote;

	private UltraLabel lblBB;

	private UltraTextEditor txtBB;

	private UltraLabel lblHBNote;

	private UltraTextEditor txtHBNote;

	private UltraLabel lblHB;

	private UltraTextEditor txtHB;

	private UltraLabel lblFBNote;

	private UltraTextEditor txtFBNote;

	private UltraLabel lblFB;

	private UltraTextEditor txtFB;

	private UltraLabel lblInternetNote;

	private UltraTextEditor txtInternetNote;

	private UltraLabel lblInternet;

	private UltraTextEditor txtInternet;

	private UltraLabel lblAgentsNotes;

	private UltraTextEditor txtAgentsNote;

	private UltraLabel lblAgents;

	private UltraTextEditor txtAgents;

	private UltraLabel lblCompaniesNote;

	private UltraTextEditor txtCompaniesNote;

	private UltraLabel lblCompanies;

	private UltraTextEditor txtCompanies;

	private UltraLabel lblPersonsNote;

	private UltraTextEditor txtPersonsNote;

	private UltraLabel lblPersons;

	private UltraTextEditor txtPersons;

	private UltraLabel lblOtherNote;

	private UltraTextEditor txtOtherNote;

	private UltraLabel lblOther;

	private UltraTextEditor txtOther;

	private UltraLabel lblEgyptiansNote;

	private UltraTextEditor txtEgyptiansNote;

	private UltraLabel lblEgyptians;

	private UltraTextEditor txtEgyptians;

	private UltraLabel lblForeignsNote;

	private UltraTextEditor txtForeignsNote;

	private UltraLabel lblForeigns;

	private UltraTextEditor txtForeigns;

	private UltraLabel lblSoldRoomsOrderNote;

	private UltraTextEditor txtSoldRoomsOrderNote;

	private UltraLabel lblSoldRoomsOrder;

	private UltraTextEditor txtSoldRoomsOrder;

	private UltraLabel lblOccupancyOrderNote;

	private UltraTextEditor txtOccupancyOrderNote;

	private UltraLabel lblOccupancyOrder;

	private UltraTextEditor txtOccupancyOrder;

	private UltraLabel lblTransferCount;

	private UltraTextEditor txtTransferCount;

	private UltraGroupBox UGBTypeOfRoom;

	private UltraGroupBox UGBBoarding;

	private UltraGroupBox UGBSourcing;

	private UltraGroupBox UGBNationality;

	private UltraGroupBox UGBMarketShare;

	public frmFrontOfficeData()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		InitializeComponent();
		TableName = "HMS_FrontOfficeData";
		IDCol = "FrontOfficeDataID";
		NoCol = "FrontOfficeDataNo";
		DateCol = "Date";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		vlFrom.ValueListItems.Clear();
		vlTo.ValueListItems.Clear();
		int num = 0;
		for (int i = 101; i < 1013; i++)
		{
			vlFrom.ValueListItems.Add((object)i.ToString(), i.ToString());
			vlTo.ValueListItems.Add((object)i.ToString(), i.ToString());
			num++;
			if (num == 12)
			{
				i = i + 100 - 12;
				num = 0;
			}
		}
		dtDetails = FrontOfficeTransfer.SelectByFrontOfficeDataID("0", GlobalVariables.IsArabic ? "1" : "0");
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public override void FillData()
	{
		if (RowID == "")
		{
			drMaster = null;
		}
		else
		{
			DataTable dataTable = FrontOfficeData.Select(RowID, GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0");
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
			((TextEditorControlBase)txtRoomsRevenue).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtSoldRoomsAverage).ValueChanged -= txt_ValueChanged;
			((TextEditorControlBase)txtAvailableRooms).ValueChanged -= txt_ValueChanged;
			CanModifyOtherBranch = drMaster["BranchID"].ToString() == GlobalVariables.CurrentBranchID;
			((Control)(object)txtCode).Text = drMaster["FrontOfficeDataNo"].ToString();
			dtpDate.Value = (DateTime)drMaster["Date"];
			((Control)(object)txtRoomsRevenue).Text = decimal.Parse(drMaster["RoomsRevenue"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtRoomsRevenueNote).Text = drMaster["RoomsRevenueNote"].ToString();
			((Control)(object)txtAvailableRooms).Text = drMaster["AvailableRooms"].ToString();
			((Control)(object)txtAvailableRoomsNote).Text = drMaster["AvailableRoomsNote"].ToString();
			((Control)(object)txtSoldRooms).Text = drMaster["SoldRooms"].ToString();
			((Control)(object)txtSoldRoomsNote).Text = drMaster["SoldRoomsNote"].ToString();
			((Control)(object)txtOccupancyPercentage).Text = decimal.Parse(drMaster["OccupancyPercentage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSoldRoomsAverage).Text = decimal.Parse(drMaster["SoldRoomsAverage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtAvailableRoomsAverage).Text = decimal.Parse(drMaster["AvailableRoomsAverage"].ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtOutOfServiceRooms).Text = drMaster["OutOfServiceRooms"].ToString();
			((Control)(object)txtOutOfServiceRoomsNote).Text = drMaster["OutOfServiceRoomsNote"].ToString();
			((Control)(object)txtHostingRooms).Text = drMaster["Hosting"].ToString();
			((Control)(object)txtHostingRoomsNote).Text = drMaster["HostingNote"].ToString();
			((Control)(object)txtSingle).Text = drMaster["SingleCount"].ToString();
			((Control)(object)txtSingleNote).Text = drMaster["SingleCountNote"].ToString();
			((Control)(object)txtDouble).Text = drMaster["DoubleCount"].ToString();
			((Control)(object)txtDoubleNote).Text = drMaster["DoubleCountNote"].ToString();
			((Control)(object)txtTriple).Text = drMaster["TripleCount"].ToString();
			((Control)(object)txtTripleNote).Text = drMaster["TripleCountNote"].ToString();
			((Control)(object)txtQuartet).Text = drMaster["QuartetCount"].ToString();
			((Control)(object)txtQuartetNote).Text = drMaster["QuartetCountNote"].ToString();
			((Control)(object)txtFB).Text = drMaster["FBCount"].ToString();
			((Control)(object)txtFBNote).Text = drMaster["FBCount"].ToString();
			((Control)(object)txtHB).Text = drMaster["HBCount"].ToString();
			((Control)(object)txtHBNote).Text = drMaster["HBCountNote"].ToString();
			((Control)(object)txtBB).Text = drMaster["BBCount"].ToString();
			((Control)(object)txtBBNote).Text = drMaster["BBCountNote"].ToString();
			((Control)(object)txtBO).Text = drMaster["BOCount"].ToString();
			((Control)(object)txtBONote).Text = drMaster["BOCountNote"].ToString();
			((Control)(object)txtPersons).Text = drMaster["PersonsCount"].ToString();
			((Control)(object)txtPersonsNote).Text = drMaster["PersonsCountNote"].ToString();
			((Control)(object)txtCompanies).Text = drMaster["CompaniesCount"].ToString();
			((Control)(object)txtCompaniesNote).Text = drMaster["CompaniesCountNote"].ToString();
			((Control)(object)txtAgents).Text = drMaster["AgentsCount"].ToString();
			((Control)(object)txtAgentsNote).Text = drMaster["AgentCountNote"].ToString();
			((Control)(object)txtInternet).Text = drMaster["InternetCount"].ToString();
			((Control)(object)txtInternetNote).Text = drMaster["InternetCountNote"].ToString();
			((Control)(object)txtOther).Text = drMaster["OthersCount"].ToString();
			((Control)(object)txtOtherNote).Text = drMaster["OthersCountNote"].ToString();
			((Control)(object)txtEgyptians).Text = drMaster["EgyptiansCount"].ToString();
			((Control)(object)txtEgyptiansNote).Text = drMaster["EgyptionsCountNote"].ToString();
			((Control)(object)txtForeigns).Text = drMaster["ForeignsCount"].ToString();
			((Control)(object)txtForeignsNote).Text = drMaster["ForeignsCountNote"].ToString();
			((Control)(object)txtOccupancyOrder).Text = drMaster["OccupancyOrder"].ToString();
			((Control)(object)txtOccupancyOrderNote).Text = drMaster["OccupancyOrderNote"].ToString();
			((Control)(object)txtSoldRoomsOrder).Text = drMaster["SoldRoomsOrder"].ToString();
			((Control)(object)txtSoldRoomsOrderNote).Text = drMaster["SoldRoomsOrderNote"].ToString();
			((TextEditorControlBase)txtRoomsRevenue).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtSoldRoomsAverage).ValueChanged += txt_ValueChanged;
			((TextEditorControlBase)txtAvailableRooms).ValueChanged += txt_ValueChanged;
			dtDetails = FrontOfficeTransfer.SelectByFrontOfficeDataID(drMaster["FrontOfficeDataID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
			((UltraGridBase)ULGData).DataSource = dtDetails;
			InitGrid();
			((Control)(object)txtTransferCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
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
		((EditorButtonControlBase)dtpDate).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRoomsRevenue).ReadOnly = NavMode;
		((EditorButtonControlBase)txtRoomsRevenueNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAvailableRooms).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAvailableRoomsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSoldRooms).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSoldRoomsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccupancyPercentage).ReadOnly = true;
		((EditorButtonControlBase)txtSoldRoomsAverage).ReadOnly = true;
		((EditorButtonControlBase)txtAvailableRoomsAverage).ReadOnly = true;
		((EditorButtonControlBase)txtTransferCount).ReadOnly = true;
		((EditorButtonControlBase)txtOutOfServiceRooms).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOutOfServiceRoomsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtHostingRooms).ReadOnly = NavMode;
		((EditorButtonControlBase)txtHostingRoomsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSingle).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSingleNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDouble).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDoubleNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTriple).ReadOnly = NavMode;
		((EditorButtonControlBase)txtTripleNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtQuartet).ReadOnly = NavMode;
		((EditorButtonControlBase)txtQuartetNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFB).ReadOnly = NavMode;
		((EditorButtonControlBase)txtFBNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtHB).ReadOnly = NavMode;
		((EditorButtonControlBase)txtHBNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBB).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBBNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBO).ReadOnly = NavMode;
		((EditorButtonControlBase)txtBONote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersons).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPersonsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCompanies).ReadOnly = NavMode;
		((EditorButtonControlBase)txtCompaniesNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAgents).ReadOnly = NavMode;
		((EditorButtonControlBase)txtAgentsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInternet).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInternetNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOther).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOtherNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEgyptians).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEgyptiansNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtForeigns).ReadOnly = NavMode;
		((EditorButtonControlBase)txtForeignsNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccupancyOrder).ReadOnly = NavMode;
		((EditorButtonControlBase)txtOccupancyOrderNote).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSoldRoomsOrder).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSoldRoomsOrderNote).ReadOnly = NavMode;
	}

	public override void ClearControls()
	{
		base.ClearControls();
		((TextEditorControlBase)txtRoomsRevenue).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtSoldRoomsAverage).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtAvailableRooms).ValueChanged -= txt_ValueChanged;
		((Control)(object)txtCode).Text = (Adding ? FrontOfficeData.GetCode() : "");
		dtpDate.DateTime = GlobalFunctions.GetServerDateTimeNow().AddDays(-1.0);
		((TextEditorControlBase)txtRoomsRevenue).Clear();
		((TextEditorControlBase)txtRoomsRevenueNote).Clear();
		((TextEditorControlBase)txtAvailableRooms).Clear();
		((TextEditorControlBase)txtAvailableRoomsNote).Clear();
		((TextEditorControlBase)txtSoldRooms).Clear();
		((TextEditorControlBase)txtSoldRoomsNote).Clear();
		((TextEditorControlBase)txtOccupancyPercentage).Clear();
		((TextEditorControlBase)txtSoldRoomsAverage).Clear();
		((TextEditorControlBase)txtAvailableRoomsAverage).Clear();
		((TextEditorControlBase)txtTransferCount).Clear();
		((TextEditorControlBase)txtOutOfServiceRooms).Clear();
		((TextEditorControlBase)txtOutOfServiceRoomsNote).Clear();
		((TextEditorControlBase)txtHostingRooms).Clear();
		((TextEditorControlBase)txtHostingRoomsNote).Clear();
		((TextEditorControlBase)txtSingle).Clear();
		((TextEditorControlBase)txtSingleNote).Clear();
		((TextEditorControlBase)txtDouble).Clear();
		((TextEditorControlBase)txtDoubleNote).Clear();
		((TextEditorControlBase)txtTriple).Clear();
		((TextEditorControlBase)txtTripleNote).Clear();
		((TextEditorControlBase)txtQuartet).Clear();
		((TextEditorControlBase)txtQuartetNote).Clear();
		((TextEditorControlBase)txtFB).Clear();
		((TextEditorControlBase)txtFBNote).Clear();
		((TextEditorControlBase)txtHB).Clear();
		((TextEditorControlBase)txtHBNote).Clear();
		((TextEditorControlBase)txtBB).Clear();
		((TextEditorControlBase)txtBBNote).Clear();
		((TextEditorControlBase)txtBO).Clear();
		((TextEditorControlBase)txtBONote).Clear();
		((TextEditorControlBase)txtPersons).Clear();
		((TextEditorControlBase)txtPersonsNote).Clear();
		((TextEditorControlBase)txtCompanies).Clear();
		((TextEditorControlBase)txtCompaniesNote).Clear();
		((TextEditorControlBase)txtAgents).Clear();
		((TextEditorControlBase)txtAgentsNote).Clear();
		((TextEditorControlBase)txtInternet).Clear();
		((TextEditorControlBase)txtInternetNote).Clear();
		((TextEditorControlBase)txtOther).Clear();
		((TextEditorControlBase)txtOtherNote).Clear();
		((TextEditorControlBase)txtEgyptians).Clear();
		((TextEditorControlBase)txtEgyptiansNote).Clear();
		((TextEditorControlBase)txtForeigns).Clear();
		((TextEditorControlBase)txtForeignsNote).Clear();
		((TextEditorControlBase)txtOccupancyOrder).Clear();
		((TextEditorControlBase)txtOccupancyOrderNote).Clear();
		((TextEditorControlBase)txtSoldRoomsOrder).Clear();
		((TextEditorControlBase)txtSoldRoomsOrderNote).Clear();
		((TextEditorControlBase)txtRoomsRevenue).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtSoldRoomsAverage).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtAvailableRooms).ValueChanged += txt_ValueChanged;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FrontOfficeDataID"].DefaultCellValue = -1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomFrom"].Header).Caption = (GlobalVariables.IsArabic ? "من غرفة" : "From Room");
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomTo"].Header).Caption = (GlobalVariables.IsArabic ? "الى غرفة" : "To Room");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomFrom"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomTo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomFrom"].ValueList = (IValueList)(object)vlFrom;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["RoomTo"].ValueList = (IValueList)(object)vlTo;
	}

	public override void btnSearch_Click(object sender, EventArgs e)
	{
		dtSearchResult = SearchFunctions.FrontOfficeData(0);
		if (dtSearchResult.Rows.Count > 0)
		{
			RowID = dtSearchResult.Rows[0]["FrontOfficeDataID"].ToString();
			FillData();
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtCode).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم  الأذن", "Please Enter The Voucher Number");
			((TextEditorControlBase)txtCode).Focus();
			return false;
		}
		if (((Control)(object)txtSoldRoomsAverage).Text == "" || decimal.Parse(((Control)(object)txtSoldRoomsAverage).Text) == 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال متوسط الغرف المباعة", "Please Enter Sold Room Average");
			((TextEditorControlBase)txtSoldRoomsAverage).Focus();
			return false;
		}
		if (((Control)(object)txtAvailableRoomsAverage).Text == "" || decimal.Parse(((Control)(object)txtAvailableRoomsAverage).Text) == 0m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال متوسط الغرف المتاحة", "Please Enter Available Room Average");
			((TextEditorControlBase)txtAvailableRoomsAverage).Focus();
			return false;
		}
		return true;
	}

	public override void AddData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = FrontOfficeData.Insert_Update("-1", ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtRoomsRevenue).Text == "") ? "Null" : ((Control)(object)txtRoomsRevenue).Text, (((Control)(object)txtRoomsRevenueNote).Text == "") ? "Null" : ((Control)(object)txtRoomsRevenueNote).Text, (((Control)(object)txtAvailableRooms).Text == "") ? "Null" : ((Control)(object)txtAvailableRooms).Text, (((Control)(object)txtAvailableRoomsNote).Text == "") ? "Null" : ((Control)(object)txtAvailableRoomsNote).Text, (((Control)(object)txtSoldRooms).Text == "") ? "Null" : ((Control)(object)txtSoldRooms).Text, (((Control)(object)txtSoldRoomsNote).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsNote).Text, (((Control)(object)txtOccupancyPercentage).Text == "") ? "Null" : ((Control)(object)txtOccupancyPercentage).Text, (((Control)(object)txtSoldRoomsAverage).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsAverage).Text, (((Control)(object)txtAvailableRoomsAverage).Text == "") ? "Null" : ((Control)(object)txtAvailableRoomsAverage).Text, (((Control)(object)txtOutOfServiceRooms).Text == "") ? "Null" : ((Control)(object)txtOutOfServiceRooms).Text, (((Control)(object)txtOutOfServiceRoomsNote).Text == "") ? "Null" : ((Control)(object)txtOutOfServiceRoomsNote).Text, (((Control)(object)txtHostingRooms).Text == "") ? "Null" : ((Control)(object)txtHostingRooms).Text, (((Control)(object)txtHostingRoomsNote).Text == "") ? "Null" : ((Control)(object)txtHostingRoomsNote).Text, (((Control)(object)txtSingle).Text == "") ? "Null" : ((Control)(object)txtSingle).Text, (((Control)(object)txtSingleNote).Text == "") ? "Null" : ((Control)(object)txtSingleNote).Text, (((Control)(object)txtDouble).Text == "") ? "Null" : ((Control)(object)txtDouble).Text, (((Control)(object)txtDoubleNote).Text == "") ? "Null" : ((Control)(object)txtDoubleNote).Text, (((Control)(object)txtTriple).Text == "") ? "Null" : ((Control)(object)txtTriple).Text, (((Control)(object)txtTripleNote).Text == "") ? "Null" : ((Control)(object)txtTripleNote).Text, (((Control)(object)txtQuartet).Text == "") ? "Null" : ((Control)(object)txtQuartet).Text, (((Control)(object)txtQuartetNote).Text == "") ? "Null" : ((Control)(object)txtQuartetNote).Text, (((Control)(object)txtFB).Text == "") ? "Null" : ((Control)(object)txtFB).Text, (((Control)(object)txtFBNote).Text == "") ? "Null" : ((Control)(object)txtFBNote).Text, (((Control)(object)txtHB).Text == "") ? "Null" : ((Control)(object)txtHB).Text, (((Control)(object)txtHBNote).Text == "") ? "Null" : ((Control)(object)txtHBNote).Text, (((Control)(object)txtBB).Text == "") ? "Null" : ((Control)(object)txtBB).Text, (((Control)(object)txtBBNote).Text == "") ? "Null" : ((Control)(object)txtBBNote).Text, (((Control)(object)txtBO).Text == "") ? "Null" : ((Control)(object)txtBO).Text, (((Control)(object)txtBONote).Text == "") ? "Null" : ((Control)(object)txtBONote).Text, (((Control)(object)txtPersons).Text == "") ? "Null" : ((Control)(object)txtPersons).Text, (((Control)(object)txtPersonsNote).Text == "") ? "Null" : ((Control)(object)txtPersonsNote).Text, (((Control)(object)txtCompanies).Text == "") ? "Null" : ((Control)(object)txtCompanies).Text, (((Control)(object)txtCompaniesNote).Text == "") ? "Null" : ((Control)(object)txtCompaniesNote).Text, (((Control)(object)txtAgents).Text == "") ? "Null" : ((Control)(object)txtAgents).Text, (((Control)(object)txtAgentsNote).Text == "") ? "Null" : ((Control)(object)txtAgentsNote).Text, (((Control)(object)txtInternet).Text == "") ? "Null" : ((Control)(object)txtInternet).Text, (((Control)(object)txtInternetNote).Text == "") ? "Null" : ((Control)(object)txtInternetNote).Text, (((Control)(object)txtOther).Text == "") ? "Null" : ((Control)(object)txtOther).Text, (((Control)(object)txtOtherNote).Text == "") ? "Null" : ((Control)(object)txtOtherNote).Text, (((Control)(object)txtEgyptians).Text == "") ? "Null" : ((Control)(object)txtEgyptians).Text, (((Control)(object)txtEgyptiansNote).Text == "") ? "Null" : ((Control)(object)txtEgyptiansNote).Text, (((Control)(object)txtForeigns).Text == "") ? "Null" : ((Control)(object)txtForeigns).Text, (((Control)(object)txtForeignsNote).Text == "") ? "Null" : ((Control)(object)txtForeignsNote).Text, (((Control)(object)txtOccupancyOrder).Text == "") ? "Null" : ((Control)(object)txtOccupancyOrder).Text, (((Control)(object)txtOccupancyOrderNote).Text == "") ? "Null" : ((Control)(object)txtOccupancyOrderNote).Text, (((Control)(object)txtSoldRoomsOrder).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsOrder).Text, (((Control)(object)txtSoldRoomsOrderNote).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsOrderNote).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				((UltraGridBase)ULGData).Rows[i].Cells["FrontOfficeTransferID"].Value = -1;
				((UltraGridBase)ULGData).Rows[i].Cells["FrontOfficeDataID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				FrontOfficeTransfer.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void UpdateData()
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			int num = FrontOfficeData.Insert_Update(drMaster["FrontOfficeDataID"].ToString(), ((Control)(object)txtCode).Text, dtpDate.DateTime.ToString(GlobalVariables.DateLongFormate), (((Control)(object)txtRoomsRevenue).Text == "") ? "Null" : ((Control)(object)txtRoomsRevenue).Text, (((Control)(object)txtRoomsRevenueNote).Text == "") ? "Null" : ((Control)(object)txtRoomsRevenueNote).Text, (((Control)(object)txtAvailableRooms).Text == "") ? "Null" : ((Control)(object)txtAvailableRooms).Text, (((Control)(object)txtAvailableRoomsNote).Text == "") ? "Null" : ((Control)(object)txtAvailableRoomsNote).Text, (((Control)(object)txtSoldRooms).Text == "") ? "Null" : ((Control)(object)txtSoldRooms).Text, (((Control)(object)txtSoldRoomsNote).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsNote).Text, (((Control)(object)txtOccupancyPercentage).Text == "") ? "Null" : ((Control)(object)txtOccupancyPercentage).Text, (((Control)(object)txtSoldRoomsAverage).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsAverage).Text, (((Control)(object)txtAvailableRoomsAverage).Text == "") ? "Null" : ((Control)(object)txtAvailableRoomsAverage).Text, (((Control)(object)txtOutOfServiceRooms).Text == "") ? "Null" : ((Control)(object)txtOutOfServiceRooms).Text, (((Control)(object)txtOutOfServiceRoomsNote).Text == "") ? "Null" : ((Control)(object)txtOutOfServiceRoomsNote).Text, (((Control)(object)txtHostingRooms).Text == "") ? "Null" : ((Control)(object)txtHostingRooms).Text, (((Control)(object)txtHostingRoomsNote).Text == "") ? "Null" : ((Control)(object)txtHostingRoomsNote).Text, (((Control)(object)txtSingle).Text == "") ? "Null" : ((Control)(object)txtSingle).Text, (((Control)(object)txtSingleNote).Text == "") ? "Null" : ((Control)(object)txtSingleNote).Text, (((Control)(object)txtDouble).Text == "") ? "Null" : ((Control)(object)txtDouble).Text, (((Control)(object)txtDoubleNote).Text == "") ? "Null" : ((Control)(object)txtDoubleNote).Text, (((Control)(object)txtTriple).Text == "") ? "Null" : ((Control)(object)txtTriple).Text, (((Control)(object)txtTripleNote).Text == "") ? "Null" : ((Control)(object)txtTripleNote).Text, (((Control)(object)txtQuartet).Text == "") ? "Null" : ((Control)(object)txtQuartet).Text, (((Control)(object)txtQuartetNote).Text == "") ? "Null" : ((Control)(object)txtQuartetNote).Text, (((Control)(object)txtFB).Text == "") ? "Null" : ((Control)(object)txtFB).Text, (((Control)(object)txtFBNote).Text == "") ? "Null" : ((Control)(object)txtFBNote).Text, (((Control)(object)txtHB).Text == "") ? "Null" : ((Control)(object)txtHB).Text, (((Control)(object)txtHBNote).Text == "") ? "Null" : ((Control)(object)txtHBNote).Text, (((Control)(object)txtBB).Text == "") ? "Null" : ((Control)(object)txtBB).Text, (((Control)(object)txtBBNote).Text == "") ? "Null" : ((Control)(object)txtBBNote).Text, (((Control)(object)txtBO).Text == "") ? "Null" : ((Control)(object)txtBO).Text, (((Control)(object)txtBONote).Text == "") ? "Null" : ((Control)(object)txtBONote).Text, (((Control)(object)txtPersons).Text == "") ? "Null" : ((Control)(object)txtPersons).Text, (((Control)(object)txtPersonsNote).Text == "") ? "Null" : ((Control)(object)txtPersonsNote).Text, (((Control)(object)txtCompanies).Text == "") ? "Null" : ((Control)(object)txtCompanies).Text, (((Control)(object)txtCompaniesNote).Text == "") ? "Null" : ((Control)(object)txtCompaniesNote).Text, (((Control)(object)txtAgents).Text == "") ? "Null" : ((Control)(object)txtAgents).Text, (((Control)(object)txtAgentsNote).Text == "") ? "Null" : ((Control)(object)txtAgentsNote).Text, (((Control)(object)txtInternet).Text == "") ? "Null" : ((Control)(object)txtInternet).Text, (((Control)(object)txtInternetNote).Text == "") ? "Null" : ((Control)(object)txtInternetNote).Text, (((Control)(object)txtOther).Text == "") ? "Null" : ((Control)(object)txtOther).Text, (((Control)(object)txtOtherNote).Text == "") ? "Null" : ((Control)(object)txtOtherNote).Text, (((Control)(object)txtEgyptians).Text == "") ? "Null" : ((Control)(object)txtEgyptians).Text, (((Control)(object)txtEgyptiansNote).Text == "") ? "Null" : ((Control)(object)txtEgyptiansNote).Text, (((Control)(object)txtForeigns).Text == "") ? "Null" : ((Control)(object)txtForeigns).Text, (((Control)(object)txtForeignsNote).Text == "") ? "Null" : ((Control)(object)txtForeignsNote).Text, (((Control)(object)txtOccupancyOrder).Text == "") ? "Null" : ((Control)(object)txtOccupancyOrder).Text, (((Control)(object)txtOccupancyOrderNote).Text == "") ? "Null" : ((Control)(object)txtOccupancyOrderNote).Text, (((Control)(object)txtSoldRoomsOrder).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsOrder).Text, (((Control)(object)txtSoldRoomsOrderNote).Text == "") ? "Null" : ((Control)(object)txtSoldRoomsOrderNote).Text, bool.Parse(drMaster["Deleted"].ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
			string text = ",";
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				text = text + ((UltraGridBase)ULGData).Rows[i].Cells["FrontOfficeTransferID"].Value.ToString() + ",";
				((UltraGridBase)ULGData).Rows[i].Cells["FrontOfficeDataID"].Value = num;
				((UltraGridBase)ULGData).Rows[i].Cells["BranchID"].Value = GlobalVariables.CurrentBranchID;
			}
			Main.DeleteForUpdate("HMS_FrontOfficeTransfer", "FrontOfficeDataID", drMaster["FrontOfficeDataID"].ToString(), "FrontOfficeTransferID", text);
			if (((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
			{
				FrontOfficeTransfer.Insert_UpdateByTable((DataTable)((UltraGridBase)ULGData).DataSource, GlobalVariables.UserID);
			}
			Main.EndBulkTrans(FromServer: false);
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			DataSaved = false;
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
		}
	}

	public override void DeleteData()
	{
		FrontOfficeTransfer.DeleteVirtualByFrontOfficeDataID(drMaster["FrontOfficeDataID"].ToString(), GlobalVariables.UserID);
		FrontOfficeData.DeleteVirtual(drMaster["FrontOfficeDataID"].ToString(), GlobalVariables.UserID);
	}

	private void txtInteger_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtDecimal_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	public void Calculate()
	{
		if (((Control)(object)txtRoomsRevenue).Text == "" || decimal.Parse(((Control)(object)txtRoomsRevenue).Text) == 0m)
		{
			((Control)(object)txtSoldRoomsAverage).Text = "0";
			((Control)(object)txtAvailableRoomsAverage).Text = "0";
		}
		else if (((Control)(object)txtSoldRooms).Text == "" || int.Parse(((Control)(object)txtSoldRooms).Text) == 0)
		{
			((Control)(object)txtOccupancyPercentage).Text = "0";
			((Control)(object)txtSoldRoomsAverage).Text = "0";
		}
		else if (((Control)(object)txtAvailableRooms).Text == "" || int.Parse(((Control)(object)txtAvailableRooms).Text) == 0)
		{
			((Control)(object)txtOccupancyPercentage).Text = "0";
			((Control)(object)txtAvailableRoomsAverage).Text = "0";
		}
		else if (((Control)(object)txtSoldRooms).Text != "" && int.Parse(((Control)(object)txtSoldRooms).Text) > 0 && ((Control)(object)txtAvailableRooms).Text != "" && int.Parse(((Control)(object)txtAvailableRooms).Text) > 0 && ((Control)(object)txtRoomsRevenue).Text != "" && decimal.Parse(((Control)(object)txtRoomsRevenue).Text) > 0m)
		{
			((Control)(object)txtOccupancyPercentage).Text = decimal.Parse((decimal.Parse(((Control)(object)txtSoldRooms).Text) / decimal.Parse(((Control)(object)txtAvailableRooms).Text) * 100m).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtSoldRoomsAverage).Text = decimal.Parse((decimal.Parse(((Control)(object)txtRoomsRevenue).Text) / decimal.Parse(((Control)(object)txtSoldRooms).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
			((Control)(object)txtAvailableRoomsAverage).Text = decimal.Parse((decimal.Parse(((Control)(object)txtRoomsRevenue).Text) / decimal.Parse(((Control)(object)txtAvailableRooms).Text)).ToString()).ToString(GlobalVariables.txtDecimalFormate);
		}
	}

	private void txt_ValueChanged(object sender, EventArgs e)
	{
		((TextEditorControlBase)txtSoldRooms).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtAvailableRooms).ValueChanged -= txt_ValueChanged;
		((TextEditorControlBase)txtRoomsRevenue).ValueChanged -= txt_ValueChanged;
		Calculate();
		((TextEditorControlBase)txtSoldRooms).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtAvailableRooms).ValueChanged += txt_ValueChanged;
		((TextEditorControlBase)txtRoomsRevenue).ValueChanged += txt_ValueChanged;
	}

	private void ULGData_AfterExitEditMode(object sender, EventArgs e)
	{
		((UltraGridBase)ULGData).UpdateData();
		((Control)(object)txtTransferCount).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
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
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Expected O, but got Unknown
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Expected O, but got Unknown
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Expected O, but got Unknown
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Expected O, but got Unknown
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Expected O, but got Unknown
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Expected O, but got Unknown
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Expected O, but got Unknown
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Expected O, but got Unknown
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Expected O, but got Unknown
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Expected O, but got Unknown
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Expected O, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Expected O, but got Unknown
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Expected O, but got Unknown
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Expected O, but got Unknown
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Expected O, but got Unknown
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Expected O, but got Unknown
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Expected O, but got Unknown
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Expected O, but got Unknown
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Expected O, but got Unknown
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Expected O, but got Unknown
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Expected O, but got Unknown
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Expected O, but got Unknown
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Expected O, but got Unknown
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Expected O, but got Unknown
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Expected O, but got Unknown
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Expected O, but got Unknown
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Expected O, but got Unknown
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Expected O, but got Unknown
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Expected O, but got Unknown
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Expected O, but got Unknown
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Expected O, but got Unknown
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Expected O, but got Unknown
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Expected O, but got Unknown
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Expected O, but got Unknown
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Expected O, but got Unknown
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Expected O, but got Unknown
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Expected O, but got Unknown
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Expected O, but got Unknown
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Expected O, but got Unknown
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Expected O, but got Unknown
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Expected O, but got Unknown
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Expected O, but got Unknown
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Expected O, but got Unknown
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Expected O, but got Unknown
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Expected O, but got Unknown
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Expected O, but got Unknown
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Expected O, but got Unknown
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Expected O, but got Unknown
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Expected O, but got Unknown
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HMS.FrontOffice.frmFrontOfficeData));
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
		this.lblRoomsRevenue = new UltraLabel();
		this.txtRoomsRevenue = new UltraTextEditor();
		this.lblDate = new UltraLabel();
		this.dtpDate = new UltraDateTimeEditor();
		this.lblRoomsRevenueNote = new UltraLabel();
		this.txtRoomsRevenueNote = new UltraTextEditor();
		this.lblAvailableRoomsNote = new UltraLabel();
		this.txtAvailableRoomsNote = new UltraTextEditor();
		this.lblAvailableRooms = new UltraLabel();
		this.txtAvailableRooms = new UltraTextEditor();
		this.lblSoldRoomsAverage = new UltraLabel();
		this.txtSoldRoomsAverage = new UltraTextEditor();
		this.lblOccupancyPercentage = new UltraLabel();
		this.txtOccupancyPercentage = new UltraTextEditor();
		this.lblSoldRoomsNote = new UltraLabel();
		this.txtSoldRoomsNote = new UltraTextEditor();
		this.lblSoldRooms = new UltraLabel();
		this.txtSoldRooms = new UltraTextEditor();
		this.lblAvailableRoomsAverage = new UltraLabel();
		this.txtAvailableRoomsAverage = new UltraTextEditor();
		this.lblOutOfServiceRooms = new UltraLabel();
		this.txtOutOfServiceRooms = new UltraTextEditor();
		this.HostingRooms = new UltraLabel();
		this.txtHostingRooms = new UltraTextEditor();
		this.lblHostingRoomsNote = new UltraLabel();
		this.txtHostingRoomsNote = new UltraTextEditor();
		this.lblOutOfServiceRoomsNote = new UltraLabel();
		this.txtOutOfServiceRoomsNote = new UltraTextEditor();
		this.lblSingleNote = new UltraLabel();
		this.txtSingleNote = new UltraTextEditor();
		this.lblSingle = new UltraLabel();
		this.txtSingle = new UltraTextEditor();
		this.lblDoubleNote = new UltraLabel();
		this.txtDoubleNote = new UltraTextEditor();
		this.lblDouble = new UltraLabel();
		this.txtDouble = new UltraTextEditor();
		this.lblQuartetNote = new UltraLabel();
		this.txtQuartetNote = new UltraTextEditor();
		this.lblQuartet = new UltraLabel();
		this.txtQuartet = new UltraTextEditor();
		this.lblTripleNote = new UltraLabel();
		this.txtTripleNote = new UltraTextEditor();
		this.lblTriple = new UltraLabel();
		this.txtTriple = new UltraTextEditor();
		this.lblBONote = new UltraLabel();
		this.txtBONote = new UltraTextEditor();
		this.lblBO = new UltraLabel();
		this.txtBO = new UltraTextEditor();
		this.lblBBNote = new UltraLabel();
		this.txtBBNote = new UltraTextEditor();
		this.lblBB = new UltraLabel();
		this.txtBB = new UltraTextEditor();
		this.lblHBNote = new UltraLabel();
		this.txtHBNote = new UltraTextEditor();
		this.lblHB = new UltraLabel();
		this.txtHB = new UltraTextEditor();
		this.lblFBNote = new UltraLabel();
		this.txtFBNote = new UltraTextEditor();
		this.lblFB = new UltraLabel();
		this.txtFB = new UltraTextEditor();
		this.lblInternetNote = new UltraLabel();
		this.txtInternetNote = new UltraTextEditor();
		this.lblInternet = new UltraLabel();
		this.txtInternet = new UltraTextEditor();
		this.lblAgentsNotes = new UltraLabel();
		this.txtAgentsNote = new UltraTextEditor();
		this.lblAgents = new UltraLabel();
		this.txtAgents = new UltraTextEditor();
		this.lblCompaniesNote = new UltraLabel();
		this.txtCompaniesNote = new UltraTextEditor();
		this.lblCompanies = new UltraLabel();
		this.txtCompanies = new UltraTextEditor();
		this.lblPersonsNote = new UltraLabel();
		this.txtPersonsNote = new UltraTextEditor();
		this.lblPersons = new UltraLabel();
		this.txtPersons = new UltraTextEditor();
		this.lblOtherNote = new UltraLabel();
		this.txtOtherNote = new UltraTextEditor();
		this.lblOther = new UltraLabel();
		this.txtOther = new UltraTextEditor();
		this.lblEgyptiansNote = new UltraLabel();
		this.txtEgyptiansNote = new UltraTextEditor();
		this.lblEgyptians = new UltraLabel();
		this.txtEgyptians = new UltraTextEditor();
		this.lblForeignsNote = new UltraLabel();
		this.txtForeignsNote = new UltraTextEditor();
		this.lblForeigns = new UltraLabel();
		this.txtForeigns = new UltraTextEditor();
		this.lblSoldRoomsOrderNote = new UltraLabel();
		this.txtSoldRoomsOrderNote = new UltraTextEditor();
		this.lblSoldRoomsOrder = new UltraLabel();
		this.txtSoldRoomsOrder = new UltraTextEditor();
		this.lblOccupancyOrderNote = new UltraLabel();
		this.txtOccupancyOrderNote = new UltraTextEditor();
		this.lblOccupancyOrder = new UltraLabel();
		this.txtOccupancyOrder = new UltraTextEditor();
		this.lblTransferCount = new UltraLabel();
		this.txtTransferCount = new UltraTextEditor();
		this.UGBTypeOfRoom = new UltraGroupBox();
		this.UGBBoarding = new UltraGroupBox();
		this.UGBSourcing = new UltraGroupBox();
		this.UGBNationality = new UltraGroupBox();
		this.UGBMarketShare = new UltraGroupBox();
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomsRevenue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomsRevenueNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAvailableRoomsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAvailableRooms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsAverage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccupancyPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRooms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAvailableRoomsAverage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutOfServiceRooms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHostingRooms).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHostingRoomsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutOfServiceRoomsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSingleNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSingle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDoubleNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDouble).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuartetNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuartet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTripleNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTriple).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBONote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBO).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBBNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHBNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFBNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInternetNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInternet).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgentsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgents).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompaniesNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanies).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersons).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOtherNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOther).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEgyptiansNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEgyptians).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtForeignsNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtForeigns).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsOrderNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsOrder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccupancyOrderNote).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccupancyOrder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTransferCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBTypeOfRoom).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBBoarding).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBBoarding).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBSourcing).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBSourcing).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBNationality).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBNationality).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBMarketShare).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.UGBDetails, "UGBDetails");
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.lblTransferCount);
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.txtTransferCount);
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTransferCount, 0);
		((System.Windows.Forms.Control)(object)base.UGBDetails).Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTransferCount, 0);
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
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
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
		base.ULGData.AfterExitEditMode += new System.EventHandler(ULGData_AfterExitEditMode);
		resources.ApplyResources(base.txtCode, "txtCode");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val8, "appearance8");
		((TextEditorControlBase)base.txtCode).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.btnSearch, "btnSearch");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.lblCode, "lblCode");
		resources.ApplyResources(base.btnCopyTo, "btnCopyTo");
		resources.ApplyResources(base.btnSetting, "btnSetting");
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
		resources.ApplyResources(this.lblRoomsRevenue, "lblRoomsRevenue");
		this.lblRoomsRevenue.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoomsRevenue).Name = "lblRoomsRevenue";
		((ControlBase)this.lblRoomsRevenue).WrapText = false;
		resources.ApplyResources(this.txtRoomsRevenue, "txtRoomsRevenue");
		((System.Windows.Forms.Control)(object)this.txtRoomsRevenue).Name = "txtRoomsRevenue";
		((TextEditorControlBase)this.txtRoomsRevenue).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtRoomsRevenue).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDecimal_KeyPress);
		resources.ApplyResources(this.lblDate, "lblDate");
		this.lblDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDate).Name = "lblDate";
		((ControlBase)this.lblDate).WrapText = false;
		resources.ApplyResources(this.dtpDate, "dtpDate");
		((UltraWinEditorMaskedControlBase)this.dtpDate).AlwaysInEditMode = true;
		this.dtpDate.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpDate).Name = "dtpDate";
		((System.Windows.Forms.Control)(object)this.dtpDate).TabStop = false;
		resources.ApplyResources(this.lblRoomsRevenueNote, "lblRoomsRevenueNote");
		this.lblRoomsRevenueNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblRoomsRevenueNote).Name = "lblRoomsRevenueNote";
		((ControlBase)this.lblRoomsRevenueNote).WrapText = false;
		resources.ApplyResources(this.txtRoomsRevenueNote, "txtRoomsRevenueNote");
		((System.Windows.Forms.Control)(object)this.txtRoomsRevenueNote).Name = "txtRoomsRevenueNote";
		resources.ApplyResources(this.lblAvailableRoomsNote, "lblAvailableRoomsNote");
		this.lblAvailableRoomsNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAvailableRoomsNote).Name = "lblAvailableRoomsNote";
		((ControlBase)this.lblAvailableRoomsNote).WrapText = false;
		resources.ApplyResources(this.txtAvailableRoomsNote, "txtAvailableRoomsNote");
		((System.Windows.Forms.Control)(object)this.txtAvailableRoomsNote).Name = "txtAvailableRoomsNote";
		resources.ApplyResources(this.lblAvailableRooms, "lblAvailableRooms");
		this.lblAvailableRooms.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAvailableRooms).Name = "lblAvailableRooms";
		((ControlBase)this.lblAvailableRooms).WrapText = false;
		resources.ApplyResources(this.txtAvailableRooms, "txtAvailableRooms");
		((System.Windows.Forms.Control)(object)this.txtAvailableRooms).Name = "txtAvailableRooms";
		((TextEditorControlBase)this.txtAvailableRooms).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtAvailableRooms).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblSoldRoomsAverage, "lblSoldRoomsAverage");
		this.lblSoldRoomsAverage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSoldRoomsAverage).Name = "lblSoldRoomsAverage";
		((ControlBase)this.lblSoldRoomsAverage).WrapText = false;
		resources.ApplyResources(this.txtSoldRoomsAverage, "txtSoldRoomsAverage");
		((System.Windows.Forms.Control)(object)this.txtSoldRoomsAverage).Name = "txtSoldRoomsAverage";
		resources.ApplyResources(this.lblOccupancyPercentage, "lblOccupancyPercentage");
		this.lblOccupancyPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccupancyPercentage).Name = "lblOccupancyPercentage";
		((ControlBase)this.lblOccupancyPercentage).WrapText = false;
		resources.ApplyResources(this.txtOccupancyPercentage, "txtOccupancyPercentage");
		((System.Windows.Forms.Control)(object)this.txtOccupancyPercentage).Name = "txtOccupancyPercentage";
		resources.ApplyResources(this.lblSoldRoomsNote, "lblSoldRoomsNote");
		this.lblSoldRoomsNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSoldRoomsNote).Name = "lblSoldRoomsNote";
		((ControlBase)this.lblSoldRoomsNote).WrapText = false;
		resources.ApplyResources(this.txtSoldRoomsNote, "txtSoldRoomsNote");
		((System.Windows.Forms.Control)(object)this.txtSoldRoomsNote).Name = "txtSoldRoomsNote";
		resources.ApplyResources(this.lblSoldRooms, "lblSoldRooms");
		this.lblSoldRooms.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSoldRooms).Name = "lblSoldRooms";
		((ControlBase)this.lblSoldRooms).WrapText = false;
		resources.ApplyResources(this.txtSoldRooms, "txtSoldRooms");
		((System.Windows.Forms.Control)(object)this.txtSoldRooms).Name = "txtSoldRooms";
		((TextEditorControlBase)this.txtSoldRooms).ValueChanged += new System.EventHandler(txt_ValueChanged);
		((System.Windows.Forms.Control)(object)this.txtSoldRooms).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblAvailableRoomsAverage, "lblAvailableRoomsAverage");
		this.lblAvailableRoomsAverage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAvailableRoomsAverage).Name = "lblAvailableRoomsAverage";
		((ControlBase)this.lblAvailableRoomsAverage).WrapText = false;
		resources.ApplyResources(this.txtAvailableRoomsAverage, "txtAvailableRoomsAverage");
		((System.Windows.Forms.Control)(object)this.txtAvailableRoomsAverage).Name = "txtAvailableRoomsAverage";
		resources.ApplyResources(this.lblOutOfServiceRooms, "lblOutOfServiceRooms");
		this.lblOutOfServiceRooms.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOutOfServiceRooms).Name = "lblOutOfServiceRooms";
		((ControlBase)this.lblOutOfServiceRooms).WrapText = false;
		resources.ApplyResources(this.txtOutOfServiceRooms, "txtOutOfServiceRooms");
		((System.Windows.Forms.Control)(object)this.txtOutOfServiceRooms).Name = "txtOutOfServiceRooms";
		((System.Windows.Forms.Control)(object)this.txtOutOfServiceRooms).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.HostingRooms, "HostingRooms");
		this.HostingRooms.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.HostingRooms).Name = "HostingRooms";
		((ControlBase)this.HostingRooms).WrapText = false;
		resources.ApplyResources(this.txtHostingRooms, "txtHostingRooms");
		((System.Windows.Forms.Control)(object)this.txtHostingRooms).Name = "txtHostingRooms";
		((System.Windows.Forms.Control)(object)this.txtHostingRooms).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblHostingRoomsNote, "lblHostingRoomsNote");
		this.lblHostingRoomsNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHostingRoomsNote).Name = "lblHostingRoomsNote";
		((ControlBase)this.lblHostingRoomsNote).WrapText = false;
		resources.ApplyResources(this.txtHostingRoomsNote, "txtHostingRoomsNote");
		((System.Windows.Forms.Control)(object)this.txtHostingRoomsNote).Name = "txtHostingRoomsNote";
		resources.ApplyResources(this.lblOutOfServiceRoomsNote, "lblOutOfServiceRoomsNote");
		this.lblOutOfServiceRoomsNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOutOfServiceRoomsNote).Name = "lblOutOfServiceRoomsNote";
		((ControlBase)this.lblOutOfServiceRoomsNote).WrapText = false;
		resources.ApplyResources(this.txtOutOfServiceRoomsNote, "txtOutOfServiceRoomsNote");
		((System.Windows.Forms.Control)(object)this.txtOutOfServiceRoomsNote).Name = "txtOutOfServiceRoomsNote";
		resources.ApplyResources(this.lblSingleNote, "lblSingleNote");
		this.lblSingleNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSingleNote).Name = "lblSingleNote";
		((ControlBase)this.lblSingleNote).WrapText = false;
		resources.ApplyResources(this.txtSingleNote, "txtSingleNote");
		((System.Windows.Forms.Control)(object)this.txtSingleNote).Name = "txtSingleNote";
		resources.ApplyResources(this.lblSingle, "lblSingle");
		this.lblSingle.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSingle).Name = "lblSingle";
		((ControlBase)this.lblSingle).WrapText = false;
		resources.ApplyResources(this.txtSingle, "txtSingle");
		((System.Windows.Forms.Control)(object)this.txtSingle).Name = "txtSingle";
		((System.Windows.Forms.Control)(object)this.txtSingle).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblDoubleNote, "lblDoubleNote");
		this.lblDoubleNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDoubleNote).Name = "lblDoubleNote";
		((ControlBase)this.lblDoubleNote).WrapText = false;
		resources.ApplyResources(this.txtDoubleNote, "txtDoubleNote");
		((System.Windows.Forms.Control)(object)this.txtDoubleNote).Name = "txtDoubleNote";
		resources.ApplyResources(this.lblDouble, "lblDouble");
		this.lblDouble.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDouble).Name = "lblDouble";
		((ControlBase)this.lblDouble).WrapText = false;
		resources.ApplyResources(this.txtDouble, "txtDouble");
		((System.Windows.Forms.Control)(object)this.txtDouble).Name = "txtDouble";
		((System.Windows.Forms.Control)(object)this.txtDouble).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblQuartetNote, "lblQuartetNote");
		this.lblQuartetNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuartetNote).Name = "lblQuartetNote";
		((ControlBase)this.lblQuartetNote).WrapText = false;
		resources.ApplyResources(this.txtQuartetNote, "txtQuartetNote");
		((System.Windows.Forms.Control)(object)this.txtQuartetNote).Name = "txtQuartetNote";
		resources.ApplyResources(this.lblQuartet, "lblQuartet");
		this.lblQuartet.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblQuartet).Name = "lblQuartet";
		((ControlBase)this.lblQuartet).WrapText = false;
		resources.ApplyResources(this.txtQuartet, "txtQuartet");
		((System.Windows.Forms.Control)(object)this.txtQuartet).Name = "txtQuartet";
		((System.Windows.Forms.Control)(object)this.txtQuartet).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblTripleNote, "lblTripleNote");
		this.lblTripleNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTripleNote).Name = "lblTripleNote";
		((ControlBase)this.lblTripleNote).WrapText = false;
		resources.ApplyResources(this.txtTripleNote, "txtTripleNote");
		((System.Windows.Forms.Control)(object)this.txtTripleNote).Name = "txtTripleNote";
		resources.ApplyResources(this.lblTriple, "lblTriple");
		this.lblTriple.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTriple).Name = "lblTriple";
		((ControlBase)this.lblTriple).WrapText = false;
		resources.ApplyResources(this.txtTriple, "txtTriple");
		((System.Windows.Forms.Control)(object)this.txtTriple).Name = "txtTriple";
		((System.Windows.Forms.Control)(object)this.txtTriple).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblBONote, "lblBONote");
		this.lblBONote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBONote).Name = "lblBONote";
		((ControlBase)this.lblBONote).WrapText = false;
		resources.ApplyResources(this.txtBONote, "txtBONote");
		((System.Windows.Forms.Control)(object)this.txtBONote).Name = "txtBONote";
		resources.ApplyResources(this.lblBO, "lblBO");
		this.lblBO.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBO).Name = "lblBO";
		((ControlBase)this.lblBO).WrapText = false;
		resources.ApplyResources(this.txtBO, "txtBO");
		((System.Windows.Forms.Control)(object)this.txtBO).Name = "txtBO";
		((System.Windows.Forms.Control)(object)this.txtBO).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblBBNote, "lblBBNote");
		this.lblBBNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBBNote).Name = "lblBBNote";
		((ControlBase)this.lblBBNote).WrapText = false;
		resources.ApplyResources(this.txtBBNote, "txtBBNote");
		((System.Windows.Forms.Control)(object)this.txtBBNote).Name = "txtBBNote";
		resources.ApplyResources(this.lblBB, "lblBB");
		this.lblBB.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBB).Name = "lblBB";
		((ControlBase)this.lblBB).WrapText = false;
		resources.ApplyResources(this.txtBB, "txtBB");
		((System.Windows.Forms.Control)(object)this.txtBB).Name = "txtBB";
		((System.Windows.Forms.Control)(object)this.txtBB).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblHBNote, "lblHBNote");
		this.lblHBNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHBNote).Name = "lblHBNote";
		((ControlBase)this.lblHBNote).WrapText = false;
		resources.ApplyResources(this.txtHBNote, "txtHBNote");
		((System.Windows.Forms.Control)(object)this.txtHBNote).Name = "txtHBNote";
		resources.ApplyResources(this.lblHB, "lblHB");
		this.lblHB.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblHB).Name = "lblHB";
		((ControlBase)this.lblHB).WrapText = false;
		resources.ApplyResources(this.txtHB, "txtHB");
		((System.Windows.Forms.Control)(object)this.txtHB).Name = "txtHB";
		((System.Windows.Forms.Control)(object)this.txtHB).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblFBNote, "lblFBNote");
		this.lblFBNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFBNote).Name = "lblFBNote";
		((ControlBase)this.lblFBNote).WrapText = false;
		resources.ApplyResources(this.txtFBNote, "txtFBNote");
		((System.Windows.Forms.Control)(object)this.txtFBNote).Name = "txtFBNote";
		resources.ApplyResources(this.lblFB, "lblFB");
		this.lblFB.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFB).Name = "lblFB";
		((ControlBase)this.lblFB).WrapText = false;
		resources.ApplyResources(this.txtFB, "txtFB");
		((System.Windows.Forms.Control)(object)this.txtFB).Name = "txtFB";
		((System.Windows.Forms.Control)(object)this.txtFB).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblInternetNote, "lblInternetNote");
		this.lblInternetNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInternetNote).Name = "lblInternetNote";
		((ControlBase)this.lblInternetNote).WrapText = false;
		resources.ApplyResources(this.txtInternetNote, "txtInternetNote");
		((System.Windows.Forms.Control)(object)this.txtInternetNote).Name = "txtInternetNote";
		resources.ApplyResources(this.lblInternet, "lblInternet");
		this.lblInternet.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInternet).Name = "lblInternet";
		((ControlBase)this.lblInternet).WrapText = false;
		resources.ApplyResources(this.txtInternet, "txtInternet");
		((System.Windows.Forms.Control)(object)this.txtInternet).Name = "txtInternet";
		((System.Windows.Forms.Control)(object)this.txtInternet).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblAgentsNotes, "lblAgentsNotes");
		this.lblAgentsNotes.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgentsNotes).Name = "lblAgentsNotes";
		((ControlBase)this.lblAgentsNotes).WrapText = false;
		resources.ApplyResources(this.txtAgentsNote, "txtAgentsNote");
		((System.Windows.Forms.Control)(object)this.txtAgentsNote).Name = "txtAgentsNote";
		resources.ApplyResources(this.lblAgents, "lblAgents");
		this.lblAgents.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAgents).Name = "lblAgents";
		((ControlBase)this.lblAgents).WrapText = false;
		resources.ApplyResources(this.txtAgents, "txtAgents");
		((System.Windows.Forms.Control)(object)this.txtAgents).Name = "txtAgents";
		((System.Windows.Forms.Control)(object)this.txtAgents).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblCompaniesNote, "lblCompaniesNote");
		this.lblCompaniesNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompaniesNote).Name = "lblCompaniesNote";
		((ControlBase)this.lblCompaniesNote).WrapText = false;
		resources.ApplyResources(this.txtCompaniesNote, "txtCompaniesNote");
		((System.Windows.Forms.Control)(object)this.txtCompaniesNote).Name = "txtCompaniesNote";
		resources.ApplyResources(this.lblCompanies, "lblCompanies");
		this.lblCompanies.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCompanies).Name = "lblCompanies";
		((ControlBase)this.lblCompanies).WrapText = false;
		resources.ApplyResources(this.txtCompanies, "txtCompanies");
		((System.Windows.Forms.Control)(object)this.txtCompanies).Name = "txtCompanies";
		((System.Windows.Forms.Control)(object)this.txtCompanies).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblPersonsNote, "lblPersonsNote");
		this.lblPersonsNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersonsNote).Name = "lblPersonsNote";
		((ControlBase)this.lblPersonsNote).WrapText = false;
		resources.ApplyResources(this.txtPersonsNote, "txtPersonsNote");
		((System.Windows.Forms.Control)(object)this.txtPersonsNote).Name = "txtPersonsNote";
		resources.ApplyResources(this.lblPersons, "lblPersons");
		this.lblPersons.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPersons).Name = "lblPersons";
		((ControlBase)this.lblPersons).WrapText = false;
		resources.ApplyResources(this.txtPersons, "txtPersons");
		((System.Windows.Forms.Control)(object)this.txtPersons).Name = "txtPersons";
		((System.Windows.Forms.Control)(object)this.txtPersons).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblOtherNote, "lblOtherNote");
		this.lblOtherNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOtherNote).Name = "lblOtherNote";
		((ControlBase)this.lblOtherNote).WrapText = false;
		resources.ApplyResources(this.txtOtherNote, "txtOtherNote");
		((System.Windows.Forms.Control)(object)this.txtOtherNote).Name = "txtOtherNote";
		resources.ApplyResources(this.lblOther, "lblOther");
		this.lblOther.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOther).Name = "lblOther";
		((ControlBase)this.lblOther).WrapText = false;
		resources.ApplyResources(this.txtOther, "txtOther");
		((System.Windows.Forms.Control)(object)this.txtOther).Name = "txtOther";
		((System.Windows.Forms.Control)(object)this.txtOther).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblEgyptiansNote, "lblEgyptiansNote");
		this.lblEgyptiansNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEgyptiansNote).Name = "lblEgyptiansNote";
		((ControlBase)this.lblEgyptiansNote).WrapText = false;
		resources.ApplyResources(this.txtEgyptiansNote, "txtEgyptiansNote");
		((System.Windows.Forms.Control)(object)this.txtEgyptiansNote).Name = "txtEgyptiansNote";
		resources.ApplyResources(this.lblEgyptians, "lblEgyptians");
		this.lblEgyptians.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEgyptians).Name = "lblEgyptians";
		((ControlBase)this.lblEgyptians).WrapText = false;
		resources.ApplyResources(this.txtEgyptians, "txtEgyptians");
		((System.Windows.Forms.Control)(object)this.txtEgyptians).Name = "txtEgyptians";
		((System.Windows.Forms.Control)(object)this.txtEgyptians).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblForeignsNote, "lblForeignsNote");
		this.lblForeignsNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblForeignsNote).Name = "lblForeignsNote";
		((ControlBase)this.lblForeignsNote).WrapText = false;
		resources.ApplyResources(this.txtForeignsNote, "txtForeignsNote");
		((System.Windows.Forms.Control)(object)this.txtForeignsNote).Name = "txtForeignsNote";
		resources.ApplyResources(this.lblForeigns, "lblForeigns");
		this.lblForeigns.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblForeigns).Name = "lblForeigns";
		((ControlBase)this.lblForeigns).WrapText = false;
		resources.ApplyResources(this.txtForeigns, "txtForeigns");
		((System.Windows.Forms.Control)(object)this.txtForeigns).Name = "txtForeigns";
		((System.Windows.Forms.Control)(object)this.txtForeigns).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtInteger_KeyPress);
		resources.ApplyResources(this.lblSoldRoomsOrderNote, "lblSoldRoomsOrderNote");
		this.lblSoldRoomsOrderNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSoldRoomsOrderNote).Name = "lblSoldRoomsOrderNote";
		((ControlBase)this.lblSoldRoomsOrderNote).WrapText = false;
		resources.ApplyResources(this.txtSoldRoomsOrderNote, "txtSoldRoomsOrderNote");
		((System.Windows.Forms.Control)(object)this.txtSoldRoomsOrderNote).Name = "txtSoldRoomsOrderNote";
		resources.ApplyResources(this.lblSoldRoomsOrder, "lblSoldRoomsOrder");
		this.lblSoldRoomsOrder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSoldRoomsOrder).Name = "lblSoldRoomsOrder";
		((ControlBase)this.lblSoldRoomsOrder).WrapText = false;
		resources.ApplyResources(this.txtSoldRoomsOrder, "txtSoldRoomsOrder");
		((System.Windows.Forms.Control)(object)this.txtSoldRoomsOrder).Name = "txtSoldRoomsOrder";
		((System.Windows.Forms.Control)(object)this.txtSoldRoomsOrder).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDecimal_KeyPress);
		resources.ApplyResources(this.lblOccupancyOrderNote, "lblOccupancyOrderNote");
		this.lblOccupancyOrderNote.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccupancyOrderNote).Name = "lblOccupancyOrderNote";
		((ControlBase)this.lblOccupancyOrderNote).WrapText = false;
		resources.ApplyResources(this.txtOccupancyOrderNote, "txtOccupancyOrderNote");
		((System.Windows.Forms.Control)(object)this.txtOccupancyOrderNote).Name = "txtOccupancyOrderNote";
		resources.ApplyResources(this.lblOccupancyOrder, "lblOccupancyOrder");
		this.lblOccupancyOrder.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblOccupancyOrder).Name = "lblOccupancyOrder";
		((ControlBase)this.lblOccupancyOrder).WrapText = false;
		resources.ApplyResources(this.txtOccupancyOrder, "txtOccupancyOrder");
		((System.Windows.Forms.Control)(object)this.txtOccupancyOrder).Name = "txtOccupancyOrder";
		((System.Windows.Forms.Control)(object)this.txtOccupancyOrder).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDecimal_KeyPress);
		resources.ApplyResources(this.lblTransferCount, "lblTransferCount");
		this.lblTransferCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTransferCount).Name = "lblTransferCount";
		((ControlBase)this.lblTransferCount).WrapText = false;
		resources.ApplyResources(this.txtTransferCount, "txtTransferCount");
		((System.Windows.Forms.Control)(object)this.txtTransferCount).Name = "txtTransferCount";
		resources.ApplyResources(this.UGBTypeOfRoom, "UGBTypeOfRoom");
		this.UGBTypeOfRoom.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom).Controls.Add((System.Windows.Forms.Control)(object)this.lblSingle);
		((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom).Controls.Add((System.Windows.Forms.Control)(object)this.lblTriple);
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val9).ForeColorDisabled = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		this.UGBTypeOfRoom.HeaderAppearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom).Name = "UGBTypeOfRoom";
		resources.ApplyResources(this.UGBBoarding, "UGBBoarding");
		this.UGBBoarding.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBBoarding).Controls.Add((System.Windows.Forms.Control)(object)this.lblFB);
		((System.Windows.Forms.Control)(object)this.UGBBoarding).Controls.Add((System.Windows.Forms.Control)(object)this.lblBB);
		((AppearanceBase)val10).FontData.BoldAsString = resources.GetString("resource.BoldAsString2");
		((AppearanceBase)val10).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString2");
		((AppearanceBase)val10).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString2");
		((AppearanceBase)val10).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString2");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val10).ForeColorDisabled = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		this.UGBBoarding.HeaderAppearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.UGBBoarding).Name = "UGBBoarding";
		resources.ApplyResources(this.UGBSourcing, "UGBSourcing");
		this.UGBSourcing.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBSourcing).Controls.Add((System.Windows.Forms.Control)(object)this.lblPersons);
		((System.Windows.Forms.Control)(object)this.UGBSourcing).Controls.Add((System.Windows.Forms.Control)(object)this.lblAgents);
		((System.Windows.Forms.Control)(object)this.UGBSourcing).Controls.Add((System.Windows.Forms.Control)(object)this.lblOther);
		((AppearanceBase)val11).FontData.BoldAsString = resources.GetString("resource.BoldAsString3");
		((AppearanceBase)val11).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString3");
		((AppearanceBase)val11).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString3");
		((AppearanceBase)val11).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString3");
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val11).ForeColorDisabled = System.Drawing.Color.Navy;
		resources.ApplyResources(val11, "appearance11");
		this.UGBSourcing.HeaderAppearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.UGBSourcing).Name = "UGBSourcing";
		resources.ApplyResources(this.UGBNationality, "UGBNationality");
		this.UGBNationality.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBNationality).Controls.Add((System.Windows.Forms.Control)(object)this.lblEgyptians);
		((System.Windows.Forms.Control)(object)this.UGBNationality).Controls.Add((System.Windows.Forms.Control)(object)this.txtForeignsNote);
		((AppearanceBase)val12).FontData.BoldAsString = resources.GetString("resource.BoldAsString4");
		((AppearanceBase)val12).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString4");
		((AppearanceBase)val12).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString4");
		((AppearanceBase)val12).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString4");
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val12).ForeColorDisabled = System.Drawing.Color.Navy;
		resources.ApplyResources(val12, "appearance12");
		this.UGBNationality.HeaderAppearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.UGBNationality).Name = "UGBNationality";
		resources.ApplyResources(this.UGBMarketShare, "UGBMarketShare");
		this.UGBMarketShare.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.lblSoldRoomsOrder);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.txtSoldRoomsOrder);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.txtSoldRoomsOrderNote);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.lblSoldRoomsOrderNote);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.lblOccupancyOrder);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.txtOccupancyOrder);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.lblOccupancyOrderNote);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Controls.Add((System.Windows.Forms.Control)(object)this.txtOccupancyOrderNote);
		((AppearanceBase)val13).FontData.BoldAsString = resources.GetString("resource.BoldAsString5");
		((AppearanceBase)val13).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString5");
		((AppearanceBase)val13).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString5");
		((AppearanceBase)val13).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString5");
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Navy;
		((AppearanceBase)val13).ForeColorDisabled = System.Drawing.Color.Navy;
		resources.ApplyResources(val13, "appearance13");
		this.UGBMarketShare.HeaderAppearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).Name = "UGBMarketShare";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuartetNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQuartetNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblQuartet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQuartet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTripleNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTripleNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTriple);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDoubleNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDoubleNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDouble);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDouble);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSingleNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSingleNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSingle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOutOfServiceRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOutOfServiceRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHostingRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHostingRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.HostingRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHostingRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBONote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBONote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBO);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBO);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBBNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBBNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtBB);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHBNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHBNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHB);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHB);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFBNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFBNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOutOfServiceRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFB);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailableRoomsAverage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAvailableRoomsAverage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSoldRoomsAverage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOccupancyPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOccupancyPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSoldRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSoldRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOutOfServiceRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSoldRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSoldRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailableRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAvailableRoomsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAvailableRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblOtherNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOtherNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtOther);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInternetNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInternetNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInternet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoomsRevenueNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSoldRoomsAverage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInternet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAgentsNotes);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAgentsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAgents);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompaniesNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompaniesNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCompanies);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCompanies);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPersonsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPersonsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEgyptiansNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEgyptiansNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoomsRevenueNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEgyptians);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailableRooms);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPersons);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRoomsRevenue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtRoomsRevenue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBBoarding);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBSourcing);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblForeigns);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtForeigns);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblForeignsNote);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBNationality);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBMarketShare);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDate);
		base.Name = "frmFrontOfficeData";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSetting, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBMarketShare, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBNationality, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblForeignsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtForeigns, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblForeigns, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBSourcing, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBBoarding, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoomsRevenue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoomsRevenue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPersons, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAvailableRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEgyptians, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtRoomsRevenueNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEgyptiansNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEgyptiansNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPersonsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPersonsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompanies, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompanies, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCompaniesNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCompaniesNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAgents, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAgentsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAgentsNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInternet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSoldRoomsAverage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblRoomsRevenueNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInternet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInternetNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInternetNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOther, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOtherNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOtherNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAvailableRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAvailableRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAvailableRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSoldRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSoldRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOutOfServiceRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSoldRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSoldRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOccupancyPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOccupancyPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSoldRoomsAverage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAvailableRoomsAverage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAvailableRoomsAverage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFB, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOutOfServiceRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFBNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFBNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHB, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHB, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHBNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHBNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBB, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBBNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBBNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBO, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBO, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtBONote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBONote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHostingRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.HostingRooms, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHostingRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHostingRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtOutOfServiceRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblOutOfServiceRoomsNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSingle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSingleNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSingleNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDouble, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDouble, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDoubleNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDoubleNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTriple, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTripleNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTripleNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQuartet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuartet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQuartetNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblQuartetNote, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.UGBDetails, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCopyTo, 0);
		((System.ComponentModel.ISupportInitialize)base.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)base.UGBDetails).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)base.UGBDetails).PerformLayout();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.txtCode).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomsRevenue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoomsRevenueNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAvailableRoomsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAvailableRooms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsAverage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccupancyPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRooms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAvailableRoomsAverage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutOfServiceRooms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHostingRooms).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHostingRoomsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOutOfServiceRoomsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSingleNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSingle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDoubleNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDouble).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuartetNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuartet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTripleNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTriple).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBONote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBO).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBBNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHBNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFBNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInternetNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInternet).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgentsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAgents).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompaniesNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtCompanies).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersonsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPersons).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOtherNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOther).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEgyptiansNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEgyptians).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtForeignsNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtForeigns).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsOrderNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSoldRoomsOrder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccupancyOrderNote).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtOccupancyOrder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTransferCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBTypeOfRoom).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBTypeOfRoom).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBBoarding).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBBoarding).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBBoarding).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBSourcing).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBSourcing).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBSourcing).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBNationality).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBNationality).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBNationality).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBMarketShare).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBMarketShare).PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
