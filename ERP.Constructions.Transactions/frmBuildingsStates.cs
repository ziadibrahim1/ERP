using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Constructions;
using CrystalDecisions.CrystalReports.Engine;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Constructions.Transactions;

public class frmBuildingsStates : frmBase
{
	private DataTable dtProjects;

	private DataTable dtBuildings;

	private DataTable dtBuildingsSamples;

	private DataTable dtBuildingsFloors;

	private DataTable dtBuildingUnits;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	private UltraGroupBox UGBBuildings;

	private UltraPanel pnlBuildings;

	private UltraLabel lblProjects;

	private UltraComboEditor cboProject;

	private UltraLabel lblBuildings;

	private UltraComboEditor cboBuildings;

	private UltraGroupBox UGBBalances;

	private UltraPanel pnlBalances;

	private UltraGroupBox UGBStores;

	private UltraPanel pnlStores;

	private UltraGroupBox UGBPark;

	private UltraPanel pnlPark;

	public UltraLabel lblFreeUnit;

	public UltraLabel lblLiability;

	public UltraLabel lblFreeUnitColor;

	public UltraLabel lblLiabilityColor;

	public UltraLabel lblSold;

	public UltraLabel lblInstallment;

	public UltraLabel lblSoldColor;

	public UltraLabel lblInstallmentColor;

	public UltraLabel lblReserved;

	public UltraLabel lblReservedColor;

	private UltraPanel pnlCheckType;

	private RadioButton rbUnitStatement;

	private RadioButton rbClientStatement;

	private RadioButton rbInstallmentPayment;

	private RadioButton rbContract;

	private ToolTip toolTip1;

	private UltraGroupBox UGBVilla;

	private UltraPanel pnlVilla;

	public UltraLabel lblLiabilityColor2;

	public UltraLabel lblLiabilityColor3;

	private UltraLabel lblUnitsCount;

	private UltraTextEditor txtTotalUnitsCount;

	private UltraTextEditor txtSalesUnitsCount;

	private UltraLabel lblSalesUnitsCount;

	private UltraTextEditor txtSalesUnitsValues;

	private UltraTextEditor txtEmptyUnitsValue;

	private UltraTextEditor txtEmptyUnitsCount;

	private UltraLabel lblEmptyUnitsCount;

	private UltraButton btnPrint;

	private UltraTextEditor txtReservedUnitValue;

	private UltraTextEditor txtReservedUnitCount;

	private UltraLabel lblReservedUnits;

	public frmBuildingsStates()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtProjects = Projects.FillCombo("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboProject, dtProjects, "ProjectID", "ProjectName");
		dtBuildings = Buildings.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboBuildings, dtBuildings, "BuildingID", "BuildingName");
		if (GlobalVariables.dtForms.Select("Form = 'frmPayments'").Length == 0)
		{
			rbInstallmentPayment.Enabled = false;
		}
		if (GlobalVariables.dtForms.Select("Form = 'frmContracts'").Length == 0)
		{
			rbContract.Enabled = false;
		}
		else
		{
			rbContract.Checked = true;
		}
	}

	private void cboProject_ValueChanged(object sender, EventArgs e)
	{
		if (cboProject.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtBuildings);
			dataView.RowFilter = "ProjectID=" + ((TextEditorControlBase)cboProject).Value.ToString();
			dataView.RowStateFilter = DataViewRowState.CurrentRows;
			cboBuildings.DataSource = dataView;
			cboBuildings.DisplayMember = "BuildingName";
			cboBuildings.ValueMember = "BuildingID";
		}
	}

	public void CreateLabel(string Text, Color ForeColor, Color BackColor, Color BorderColor, float FontSize, int ControlHeight, int ControlWidth, int LocationLeft, int LocationTop, UltraPanel pnlParentControl)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		UltraLabel val = new UltraLabel();
		((Control)(object)val).Text = Text;
		((ControlBase)val).Appearance.ForeColor = ForeColor;
		((Control)(object)val).Height = ControlHeight;
		((Control)(object)val).Width = ControlWidth;
		((Control)(object)pnlParentControl.ClientArea).Controls.Add((Control)(object)val);
		((UltraControlBase)val).UseAppStyling = false;
		((ControlBase)val).Appearance.FontData.Bold = (DefaultableBoolean)1;
		((ControlBase)val).Appearance.FontData.SizeInPoints = FontSize;
		((ControlBase)val).Appearance.FontData.Name = "Tahoma";
		((ControlBase)val).Appearance.TextHAlign = (HAlign)2;
		((ControlBase)val).Appearance.TextVAlign = (VAlign)2;
		val.BorderStyleOuter = (UIElementBorderStyle)4;
		((ControlBase)val).Appearance.BorderColor = BorderColor;
		((ControlBase)val).Appearance.BackColor = BackColor;
		((Control)(object)val).Left = LocationLeft;
		((Control)(object)val).Top = LocationTop;
	}

	public void CreateButton(string Text, Color ForeColor, Color BackColor, Color BorderColor, float FontSize, int ControlHeight, int ControlWidth, int LocationLeft, int LocationTop, UltraPanel pnlParentControl, DataRow Tag)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		UltraButton val = new UltraButton();
		((Control)(object)val).Text = Text;
		((ControlBase)val).Appearance.ForeColor = ForeColor;
		((Control)(object)val).Height = ControlHeight;
		((Control)(object)val).Width = ControlWidth;
		((Control)(object)pnlParentControl.ClientArea).Controls.Add((Control)(object)val);
		((UltraControlBase)val).UseAppStyling = false;
		((ControlBase)val).Appearance.ThemedElementAlpha = (Alpha)3;
		((ControlBase)val).Appearance.FontData.Bold = (DefaultableBoolean)1;
		((ControlBase)val).Appearance.FontData.SizeInPoints = FontSize;
		((ControlBase)val).Appearance.FontData.Name = "Tahoma";
		((ControlBase)val).Appearance.TextHAlign = (HAlign)2;
		((ControlBase)val).Appearance.TextVAlign = (VAlign)2;
		((ControlBase)val).Appearance.BorderColor = BorderColor;
		((ControlBase)val).Appearance.BackColor = BackColor;
		((Control)(object)val).Tag = Tag;
		((Control)(object)val).Click += button_Click;
		((Control)(object)val).Left = LocationLeft;
		((Control)(object)val).Top = LocationTop;
		((Control)(object)val).Enabled = !bool.Parse(Tag["IsClosed"].ToString());
		((Control)(object)val).Visible = !bool.Parse(Tag["IsHidden"].ToString());
		string text = "";
		decimal num = decimal.Parse(Tag["Price"].ToString());
		decimal num2 = decimal.Parse(Tag["SalesPrice"].ToString());
		decimal num3 = decimal.Parse(Tag["DelayPeriod"].ToString());
		decimal num4 = decimal.Parse(Tag["DelayAmount"].ToString());
		text = text + (GlobalVariables.IsArabic ? "\nسعر البيع:  " : "\nSalesPrice:  ") + ((num2 > 0m) ? num2.ToString("G29") : num.ToString("G29"));
		if (num4 > 10m)
		{
			text = text + (GlobalVariables.IsArabic ? "\nعدد ايام التاخير:  " : "\nDelayed Period:  ") + num3.ToString("G29");
			text = text + (GlobalVariables.IsArabic ? "\nقيمة المستحق:  " : "\nAccrued Amount:  ") + num4.ToString("G29");
		}
		if (text != "")
		{
			toolTip1.SetToolTip((Control)(object)val, text);
		}
		ContextMenu contextMenu = new ContextMenu();
		MenuItem menuItem = new MenuItem();
		menuItem.Tag = Tag;
		menuItem.Text = (GlobalVariables.IsArabic ? "تعاقد" : "Contract");
		menuItem.Click += MenuItem_Click;
		contextMenu.MenuItems.Add(menuItem);
		if (GlobalVariables.dtForms.Select("Form = 'frmContracts'").Length == 0)
		{
			menuItem.Enabled = false;
		}
		MenuItem menuItem2 = new MenuItem();
		menuItem2.Tag = Tag;
		menuItem2.Text = (GlobalVariables.IsArabic ? "سداد قسط" : "Installment Payment");
		menuItem2.Click += MenuItem_Click;
		contextMenu.MenuItems.Add(menuItem2);
		if (GlobalVariables.dtForms.Select("Form = 'frmPayments'").Length == 0)
		{
			menuItem2.Enabled = false;
		}
		MenuItem menuItem3 = new MenuItem();
		menuItem3.Tag = Tag;
		menuItem3.Text = (GlobalVariables.IsArabic ? "كشف حساب عميل" : "Client Statement");
		menuItem3.Click += MenuItem_Click;
		contextMenu.MenuItems.Add(menuItem3);
		MenuItem menuItem4 = new MenuItem();
		menuItem4.Tag = Tag;
		menuItem4.Text = (GlobalVariables.IsArabic ? "كشف حساب وحدة" : "Unit Statement");
		menuItem4.Click += MenuItem_Click;
		contextMenu.MenuItems.Add(menuItem4);
		contextMenu.RightToLeft = (GlobalVariables.IsArabic ? RightToLeft.Yes : RightToLeft.No);
		((Control)(object)val).ContextMenu = contextMenu;
	}

	public void button_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		UltraButton val = (UltraButton)sender;
		if (rbContract.Checked)
		{
			frmContracts frmContracts2 = new frmContracts((DataRow)((Control)(object)val).Tag);
			frmContracts2.StartPosition = FormStartPosition.CenterParent;
			frmContracts2.Size = new Size(Screen.PrimaryScreen.Bounds.Width - 200, Screen.PrimaryScreen.Bounds.Height - 200);
			((Control)(object)frmContracts2.lblTitle).Text = (GlobalVariables.IsArabic ? "العقود" : "Contracts");
			frmContracts2.ShowDialog();
			cboBuildings_ValueChanged(null, null);
		}
		else if (rbInstallmentPayment.Checked)
		{
			frmPayments frmPayments2 = new frmPayments((((DataRow)((Control)(object)val).Tag)["SubAccountID"] == DBNull.Value) ? "0" : ((DataRow)((Control)(object)val).Tag)["SubAccountID"].ToString());
			frmPayments2.StartPosition = FormStartPosition.CenterParent;
			frmPayments2.Size = new Size(Screen.PrimaryScreen.Bounds.Width - 200, Screen.PrimaryScreen.Bounds.Height - 200);
			((Control)(object)frmPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد عميل" : "Payments");
			frmPayments2.ShowDialog();
			cboBuildings_ValueChanged(null, null);
		}
		else if (rbClientStatement.Checked && ((DataRow)((Control)(object)val).Tag)["SubAccountID"] != DBNull.Value)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_ContractsInstallments_A_nologo.rpt" : "Rep_Con_ContractsInstallments_E_nologo.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", GlobalVariables.BranchIDs);
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", DateTime.Now.AddYears(-50));
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", DateTime.Now.AddYears(50));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@InstallmentsTypeIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", "," + ((DataRow)((Control)(object)val).Tag)["SubAccountID"].ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@BuildingUnitIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@IsPayed", "-1");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			GlobalVariables.ReportDocument = null;
		}
		else if (rbUnitStatement.Checked)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_ContractsInstallments_A.rpt" : "Rep_Con_ContractsInstallments_E.rpt"));
			frmReporViwer frmReporViwer3 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", GlobalVariables.BranchIDs);
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", DateTime.Now.AddYears(-50));
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", DateTime.Now.AddYears(50));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@InstallmentsTypeIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@BuildingUnitIDs", "," + ((DataRow)((Control)(object)val).Tag)["BuildingUnitID"].ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsPayed", "-1");
			frmReporViwer3.ShowDialog();
			frmReporViwer3 = null;
			GlobalVariables.ReportDocument = null;
		}
	}

	public void MenuItem_Click(object sender, EventArgs e)
	{
		MenuItem menuItem = (MenuItem)sender;
		if (menuItem.Text == (GlobalVariables.IsArabic ? "تعاقد" : "Contract"))
		{
			frmContracts frmContracts2 = new frmContracts((DataRow)menuItem.Tag);
			frmContracts2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmContracts2.lblTitle).Text = (GlobalVariables.IsArabic ? "العقود" : "Contracts");
			frmContracts2.ShowDialog();
			cboBuildings_ValueChanged(null, null);
		}
		else if (menuItem.Text == (GlobalVariables.IsArabic ? "سداد قسط" : "Installment Payment"))
		{
			frmPayments frmPayments2 = new frmPayments((((DataRow)menuItem.Tag)["SubAccountID"] == DBNull.Value) ? "0" : ((DataRow)menuItem.Tag)["SubAccountID"].ToString());
			frmPayments2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmPayments2.lblTitle).Text = (GlobalVariables.IsArabic ? "سداد عميل" : "Payments");
			frmPayments2.ShowDialog();
			cboBuildings_ValueChanged(null, null);
		}
		else if (menuItem.Text == (GlobalVariables.IsArabic ? "كشف حساب عميل" : "Client Statement") && ((DataRow)menuItem.Tag)["SubAccountID"] != DBNull.Value)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_ContractsInstallments_A_nologo.rpt" : "Rep_Con_ContractsInstallments_E_nologo.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", GlobalVariables.BranchIDs);
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", DateTime.Now.AddYears(-50));
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", DateTime.Now.AddYears(50));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@InstallmentsTypeIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", "," + ((DataRow)menuItem.Tag)["SubAccountID"].ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@BuildingUnitIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@IsPayed", "-1");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			GlobalVariables.ReportDocument = null;
		}
		else if (menuItem.Text == (GlobalVariables.IsArabic ? "كشف حساب وحدة" : "Unit Statement"))
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_ContractsInstallments_A_nologo.rpt" : "Rep_Con_ContractsInstallments_E_nologo.rpt"));
			frmReporViwer frmReporViwer3 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BranchIDs", GlobalVariables.BranchIDs);
			GlobalVariables.ReportDocument.SetParameterValue("@FromDate", DateTime.Now.AddYears(-50));
			GlobalVariables.ReportDocument.SetParameterValue("@ToDate", DateTime.Now.AddYears(50));
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			GlobalVariables.ReportDocument.SetParameterValue("@InstallmentsTypeIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@CostCenterIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@SubAccountIDs", "-1");
			GlobalVariables.ReportDocument.SetParameterValue("@BuildingUnitIDs", "," + ((DataRow)menuItem.Tag)["BuildingUnitID"].ToString() + ",");
			GlobalVariables.ReportDocument.SetParameterValue("@IsPayed", "-1");
			frmReporViwer3.ShowDialog();
			frmReporViwer3 = null;
			GlobalVariables.ReportDocument = null;
		}
	}

	private void cboBuildings_ValueChanged(object sender, EventArgs e)
	{
		if (cboBuildings.SelectedIndex <= -1)
		{
			return;
		}
		pnlBuildings.HorizontalScrollProperties.Visible = true;
		pnlBuildings.VerticalScrollProperties.Visible = true;
		((Control)(object)pnlBuildings).Visible = false;
		((Control)(object)UGBVilla).Visible = false;
		((Control)(object)UGBBalances).Visible = false;
		((Control)(object)UGBStores).Visible = false;
		((Control)(object)UGBPark).Visible = false;
		((Control)(object)pnlBuildings.ClientArea).Controls.Clear();
		((Control)(object)pnlVilla.ClientArea).Controls.Clear();
		((Control)(object)pnlBalances.ClientArea).Controls.Clear();
		((Control)(object)pnlStores.ClientArea).Controls.Clear();
		((Control)(object)pnlPark.ClientArea).Controls.Clear();
		((Control)(object)UGBBalances).Width = 1;
		((Control)(object)UGBStores).Width = 1;
		((Control)(object)UGBPark).Width = 1;
		((Control)(object)UGBVilla).Width = 1;
		((Control)(object)pnlBuildings).Width = ((Control)(object)UGBBuildings).Width - 25;
		((Control)(object)UGBBuildings).Text = ((Control)(object)cboBuildings).Text;
		FillBuildingsInformations(((TextEditorControlBase)cboBuildings).Value.ToString());
		dtBuildingsSamples = BuildingsSamples.SelectByBuildingID(((TextEditorControlBase)cboBuildings).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtBuildingsFloors = BuildingsUnits.SelectFloorsbyBuildingID(((TextEditorControlBase)cboBuildings).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtBuildingUnits = BuildingsUnits.SelectForBuildingState(((TextEditorControlBase)cboBuildings).Value.ToString(), GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView dataView = new DataView(dtBuildingsFloors);
		dataView.RowFilter = " UnitTypeID =1 ";
		dataView.ToTable();
		DataView dataView2 = new DataView(dtBuildingsFloors);
		dataView2.RowFilter = " UnitTypeID =3 ";
		dataView2.ToTable();
		if (dataView2.Count > 0)
		{
			((Control)(object)UGBPark).Text = dataView2[0]["FloorName"].ToString();
		}
		DataView dataView3 = new DataView(dtBuildingsFloors);
		dataView3.RowFilter = " UnitTypeID =2 ";
		dataView3.ToTable();
		if (dataView3.Count > 0)
		{
			((Control)(object)UGBStores).Text = dataView3[0]["FloorName"].ToString();
		}
		DataView dataView4 = new DataView(dtBuildingsFloors);
		dataView4.RowFilter = " UnitTypeID =4 ";
		dataView4.ToTable();
		if (dataView4.Count > 0)
		{
			((Control)(object)UGBBalances).Text = dataView4[0]["FloorName"].ToString();
		}
		DataView dataView5 = new DataView(dtBuildingsFloors);
		dataView5.RowFilter = " UnitTypeID =5 ";
		dataView5.ToTable();
		if (dataView5.Count > 0)
		{
			((Control)(object)UGBVilla).Text = dataView5[0]["FloorName"].ToString();
		}
		DataView dataView6 = new DataView(dtBuildingUnits);
		dataView6.RowFilter = " UnitTypeID =3 ";
		dataView6.ToTable();
		DataView dataView7 = new DataView(dtBuildingUnits);
		dataView7.RowFilter = " UnitTypeID =2 ";
		dataView7.ToTable();
		DataView dataView8 = new DataView(dtBuildingUnits);
		dataView8.RowFilter = " UnitTypeID =4 ";
		dataView8.ToTable();
		DataView dataView9 = new DataView(dtBuildingUnits);
		dataView9.RowFilter = " UnitTypeID =5 ";
		dataView9.ToTable();
		int count = dtBuildingsSamples.Rows.Count;
		int num = 110;
		int num2 = 1;
		int num3 = (((Control)(object)pnlBuildings).Width - num - GlobalVariables.ScrollWidth) / 5;
		int num4 = 0;
		int num5 = 0;
		int num6 = 1;
		if (count > 5)
		{
			((Control)(object)pnlBuildings).Width = num3 * count + num + GlobalVariables.ScrollWidth;
		}
		int num7 = ((Control)(object)pnlBuildings).Width - num - num2;
		int num8 = 23;
		float fontSize = 9f;
		Color backColor = Color.LightGray;
		for (int i = 0; i < 3; i++)
		{
			switch (i)
			{
			case 0:
				CreateLabel(GlobalVariables.IsArabic ? "دور/نموذج" : "Floor/Sample", Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num, num7, num6, pnlBuildings);
				break;
			case 1:
				CreateLabel(GlobalVariables.IsArabic ? "المساحة م²" : "Size m²", Color.Red, Color.Aquamarine, Color.Black, fontSize, num8, num, num7, num6, pnlBuildings);
				break;
			case 2:
				CreateLabel(GlobalVariables.IsArabic ? "سعر الوحدة" : "Unit Price", Color.Red, Color.Aquamarine, Color.Black, fontSize, num8, num, num7, num6, pnlBuildings);
				break;
			}
			num6 += num8 + num2;
		}
		if (dataView9.Count > 0)
		{
			for (int j = 0; j < dataView9.Count; j++)
			{
				((UltraControlBase)UGBVilla).UseAppStyling = false;
				if (j == 0)
				{
					decimal num9 = Math.Ceiling(decimal.Parse(dataView9.Count.ToString()) / 5m);
					((Control)(object)UGBVilla).Visible = true;
					((Control)(object)UGBVilla).Height = (int)(num9 + 1m) * (num8 + num2);
					((Control)(object)pnlBuildings.ClientArea).Controls.Add((Control)(object)UGBVilla);
					((Control)(object)UGBVilla).Width = (num3 + num2) * 5 + num + num2 + 2;
					((Control)(object)UGBVilla).Left = ((Control)(object)pnlBuildings).Width - (num3 + num2) * 5 - num - num2 - 2;
					((Control)(object)UGBVilla).Top = num6;
					num7 = ((Control)(object)pnlVilla).Width - num3 - num - num2 + 2;
					num6 = num2;
					CreateLabel(((Control)(object)UGBVilla).Text, Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num, ((Control)(object)pnlVilla).Width - num, num6, pnlVilla);
				}
				if (dataView9[j]["SubAccountName"] == DBNull.Value)
				{
					backColor = ((ControlBase)lblFreeUnitColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView9[j]["Liability"].ToString()) > 0)
				{
					backColor = ((int.Parse(dataView9[j]["DelayPeriod"].ToString()) > 61) ? ((ControlBase)lblLiabilityColor3).Appearance.BackColor : ((int.Parse(dataView9[j]["DelayPeriod"].ToString()) <= 31) ? ((ControlBase)lblLiabilityColor).Appearance.BackColor : ((ControlBase)lblLiabilityColor2).Appearance.BackColor));
				}
				else if (int.Parse(dataView9[j]["Sold"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblSoldColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView9[j]["Reserved"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblReservedColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView9[j]["Sold"].ToString()) > 0)
				{
					backColor = ((ControlBase)lblInstallmentColor).Appearance.BackColor;
				}
				CreateButton((dataView9[j]["SubAccountName"] != DBNull.Value) ? dataView9[j]["SubAccountName"].ToString() : dataView9[j]["BuildingUnitCode"].ToString(), Color.Black, backColor, Color.Black, fontSize, num8, num3, num7, num6, pnlVilla, dataView9[j].Row);
				if (num7 - num3 < 0)
				{
					num6 += num8 + num2;
					num7 = ((Control)(object)pnlVilla).Width - num - num3 - num2 + 2;
				}
				else
				{
					num7 -= num3 + num2;
				}
			}
			num7 = ((Control)(object)pnlBuildings).Width - num - num2;
			num6 = 3 * (num8 + num2) + ((Control)(object)UGBVilla).Height + num2;
		}
		for (int k = 0; k < dataView.Count; k++)
		{
			CreateLabel(dataView[k]["FloorName"].ToString(), Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num, num7, num6, pnlBuildings);
			num6 += num8 + num2;
		}
		num4 = num6;
		num6 = num2;
		num7 -= num3 + num2;
		for (int l = 0; l < dtBuildingsSamples.Rows.Count; l++)
		{
			CreateLabel((GlobalVariables.IsArabic ? " نموذج رقم " : " Sample No ") + dtBuildingsSamples.Rows[l]["BuildingSampleCode"].ToString(), Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num3, num7, num6, pnlBuildings);
			num6 += num8 + num2;
			CreateLabel(decimal.Parse(dtBuildingsSamples.Rows[l]["Size"].ToString()).ToString("G29"), Color.Red, Color.Aquamarine, Color.Black, fontSize, num8, num3, num7, num6, pnlBuildings);
			num6 += num8 + num2;
			CreateLabel(decimal.Parse(dtBuildingsSamples.Rows[l]["Price"].ToString()).ToString("N2"), Color.Red, Color.Aquamarine, Color.Black, fontSize, num8, num3, num7, num6, pnlBuildings);
			num6 = num2 + 3 * (num8 + num2) + ((dataView9.Count > 0) ? (((Control)(object)UGBVilla).Height + num2) : 0);
			DataView dataView10 = new DataView(dtBuildingUnits);
			dataView10.RowFilter = " UnitTypeID =1 And BuildingSampleID= " + dtBuildingsSamples.Rows[l]["BuildingSampleID"].ToString();
			dataView10.Sort = " FloorID Desc ";
			dataView10.ToTable();
			for (int m = 0; m < dataView10.Count; m++)
			{
				if (dataView10[m]["SubAccountName"] == DBNull.Value)
				{
					backColor = ((ControlBase)lblFreeUnitColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView10[m]["Liability"].ToString()) > 0)
				{
					backColor = ((int.Parse(dataView10[m]["DelayPeriod"].ToString()) <= 61) ? ((int.Parse(dataView10[m]["DelayPeriod"].ToString()) <= 31) ? ((ControlBase)lblLiabilityColor).Appearance.BackColor : ((ControlBase)lblLiabilityColor2).Appearance.BackColor) : ((ControlBase)lblLiabilityColor3).Appearance.BackColor);
				}
				else if (int.Parse(dataView10[m]["Sold"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblSoldColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView10[m]["Reserved"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblReservedColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView10[m]["Sold"].ToString()) > 0)
				{
					backColor = ((ControlBase)lblInstallmentColor).Appearance.BackColor;
				}
				CreateButton((dataView10[m]["SubAccountName"] != DBNull.Value) ? dataView10[m]["SubAccountName"].ToString() : dataView10[m]["BuildingUnitCode"].ToString(), Color.Black, backColor, Color.Black, fontSize, num8, num3, num7, num6, pnlBuildings, dataView10[m].Row);
				num6 += num8 + num2;
			}
			if (l != dtBuildingsSamples.Rows.Count - 1)
			{
				num6 = num2;
				num7 -= num3 + num2;
			}
		}
		num5 = num7;
		if (dataView8.Count > 0)
		{
			for (int n = 0; n < dataView8.Count; n++)
			{
				((UltraControlBase)UGBBalances).UseAppStyling = false;
				if (n == 0)
				{
					decimal num10 = Math.Ceiling(decimal.Parse(dataView8.Count.ToString()) / 5m);
					((Control)(object)UGBBalances).Visible = true;
					((Control)(object)UGBBalances).Height = (int)(num10 + 1m) * (num8 + num2);
					((Control)(object)pnlBuildings.ClientArea).Controls.Add((Control)(object)UGBBalances);
					((Control)(object)UGBBalances).Width = (num3 + num2) * 5 + num + num2 + 2;
					((Control)(object)UGBBalances).Left = ((Control)(object)pnlBuildings).Width - (num3 + num2) * 5 - num - num2 - 2;
					((Control)(object)UGBBalances).Top = num4;
					num7 = ((Control)(object)pnlBalances).Width - num3 - num - num2 + 2;
					num6 = num2;
					CreateLabel(((Control)(object)UGBBalances).Text, Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num, ((Control)(object)pnlBalances).Width - num, num6, pnlBalances);
				}
				if (dataView8[n]["SubAccountName"] == DBNull.Value)
				{
					backColor = ((ControlBase)lblFreeUnitColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView8[n]["Liability"].ToString()) > 0)
				{
					backColor = ((int.Parse(dataView8[n]["DelayPeriod"].ToString()) > 61) ? ((ControlBase)lblLiabilityColor3).Appearance.BackColor : ((int.Parse(dataView8[n]["DelayPeriod"].ToString()) <= 31) ? ((ControlBase)lblLiabilityColor).Appearance.BackColor : ((ControlBase)lblLiabilityColor2).Appearance.BackColor));
				}
				else if (int.Parse(dataView8[n]["Sold"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblSoldColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView8[n]["Reserved"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblReservedColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView8[n]["Sold"].ToString()) > 0)
				{
					backColor = ((ControlBase)lblInstallmentColor).Appearance.BackColor;
				}
				CreateButton((dataView8[n]["SubAccountName"] != DBNull.Value) ? dataView8[n]["SubAccountName"].ToString() : dataView8[n]["BuildingUnitCode"].ToString(), Color.Black, backColor, Color.Black, fontSize, num8, num3, num7, num6, pnlBalances, dataView8[n].Row);
				if (num7 - num3 < 0)
				{
					num6 += num8 + num2;
					num7 = ((Control)(object)pnlBalances).Width - num - num3 - num2 + 2;
				}
				else
				{
					num7 -= num3 + num2;
				}
			}
			num4 = num4 + ((Control)(object)UGBBalances).Height + num2;
		}
		if (dataView7.Count > 0)
		{
			for (int num11 = 0; num11 < dataView7.Count; num11++)
			{
				((UltraControlBase)UGBStores).UseAppStyling = false;
				if (num11 == 0)
				{
					num6 = num4;
					num7 = num5;
					decimal num12 = Math.Ceiling(decimal.Parse(dataView7.Count.ToString()) / 5m);
					((Control)(object)UGBStores).Visible = true;
					((Control)(object)UGBStores).Height = (int)(num12 + 1m) * (num8 + num2);
					((Control)(object)pnlBuildings.ClientArea).Controls.Add((Control)(object)UGBStores);
					((Control)(object)UGBStores).Width = (num3 + num2) * 5 + num + num2 + 2;
					((Control)(object)UGBStores).Left = ((Control)(object)pnlBuildings).Width - (num3 + num2) * 5 - num - num2 - 2;
					((Control)(object)UGBStores).Top = num6;
					num7 = ((Control)(object)pnlStores).Width - num3 - num - num2 + 2;
					num6 = num2;
					CreateLabel(((Control)(object)UGBStores).Text, Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num, ((Control)(object)pnlStores).Width - num, num6, pnlStores);
				}
				if (dataView7[num11]["SubAccountName"] == DBNull.Value)
				{
					backColor = ((ControlBase)lblFreeUnitColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView7[num11]["Liability"].ToString()) > 0)
				{
					backColor = ((int.Parse(dataView7[num11]["DelayPeriod"].ToString()) > 61) ? ((ControlBase)lblLiabilityColor3).Appearance.BackColor : ((int.Parse(dataView7[num11]["DelayPeriod"].ToString()) <= 31) ? ((ControlBase)lblLiabilityColor).Appearance.BackColor : ((ControlBase)lblLiabilityColor2).Appearance.BackColor));
				}
				else if (int.Parse(dataView7[num11]["Sold"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblSoldColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView7[num11]["Reserved"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblReservedColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView7[num11]["Sold"].ToString()) > 0)
				{
					backColor = ((ControlBase)lblInstallmentColor).Appearance.BackColor;
				}
				CreateButton((dataView7[num11]["SubAccountName"] != DBNull.Value) ? dataView7[num11]["SubAccountName"].ToString() : dataView7[num11]["BuildingUnitCode"].ToString(), Color.Black, backColor, Color.Black, fontSize, num8, num3, num7, num6, pnlStores, dataView7[num11].Row);
				if (num7 - num3 < 0)
				{
					num6 += num8 + num2;
					num7 = ((Control)(object)pnlStores).Width - num3 - num - num2 + 2;
				}
				else
				{
					num7 -= num3 + num2;
				}
			}
			num4 = num4 + ((Control)(object)UGBStores).Height + num2;
		}
		if (dataView6.Count > 0)
		{
			((UltraControlBase)UGBPark).UseAppStyling = false;
			for (int num13 = 0; num13 < dataView6.Count; num13++)
			{
				if (num13 == 0)
				{
					num6 = num4;
					num7 = num5;
					decimal num14 = Math.Ceiling(decimal.Parse(dataView6.Count.ToString()) / 5m);
					((Control)(object)UGBPark).Visible = true;
					((Control)(object)UGBPark).Height = (int)(num14 + 1m) * (num8 + num2);
					((Control)(object)pnlBuildings.ClientArea).Controls.Add((Control)(object)UGBPark);
					((Control)(object)UGBPark).Width = (num3 + num2) * 5 + num + num2 + 2;
					((Control)(object)UGBPark).Left = ((Control)(object)pnlBuildings).Width - (num3 + num2) * 5 - num - num2 - 2;
					((Control)(object)UGBPark).Top = num6;
					num7 = ((Control)(object)pnlPark).Width - num3 - num - num2 + 2;
					num6 = num2;
					CreateLabel(((Control)(object)UGBPark).Text, Color.Black, Color.Aquamarine, Color.Black, fontSize, num8, num, ((Control)(object)pnlPark).Width - num, num6, pnlPark);
				}
				if (dataView6[num13]["SubAccountName"] == DBNull.Value)
				{
					backColor = ((ControlBase)lblFreeUnitColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView6[num13]["Liability"].ToString()) > 0)
				{
					backColor = ((int.Parse(dataView6[num13]["DelayPeriod"].ToString()) > 61) ? ((ControlBase)lblLiabilityColor3).Appearance.BackColor : ((int.Parse(dataView6[num13]["DelayPeriod"].ToString()) <= 31) ? ((ControlBase)lblLiabilityColor).Appearance.BackColor : ((ControlBase)lblLiabilityColor2).Appearance.BackColor));
				}
				else if (int.Parse(dataView6[num13]["Sold"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblSoldColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView7[num13]["Reserved"].ToString()) == 0)
				{
					backColor = ((ControlBase)lblReservedColor).Appearance.BackColor;
				}
				else if (int.Parse(dataView6[num13]["Sold"].ToString()) > 0)
				{
					backColor = ((ControlBase)lblInstallmentColor).Appearance.BackColor;
				}
				CreateButton((dataView6[num13]["SubAccountName"] != DBNull.Value) ? dataView6[num13]["SubAccountName"].ToString() : dataView6[num13]["BuildingUnitCode"].ToString(), Color.Black, backColor, Color.Black, fontSize, num8, num3, num7, num6, pnlPark, dataView6[num13].Row);
				if (num7 - num3 < 0)
				{
					num6 += num8 + num2;
					num7 = ((Control)(object)pnlPark).Width - num3 - num - num2 + 2;
				}
				else
				{
					num7 -= num3 + num2;
				}
			}
		}
		((Control)(object)pnlBuildings).Visible = true;
	}

	private void FillBuildingsInformations(string BuildingID)
	{
		DataTable dataTable = Buildings.SelectInformationByBuildingID(BuildingID, IsFromServer: false);
		((Control)(object)txtTotalUnitsCount).Text = dataTable.Rows[0]["TotalUnitsCount"].ToString();
		((Control)(object)txtSalesUnitsCount).Text = dataTable.Rows[0]["SalesUnitsCount"].ToString();
		((Control)(object)txtSalesUnitsValues).Text = decimal.Parse(dataTable.Rows[0]["SalesUnitsValues"].ToString()).ToString("G29");
		((Control)(object)txtEmptyUnitsCount).Text = dataTable.Rows[0]["EmptyUnitsCount"].ToString();
		((Control)(object)txtEmptyUnitsValue).Text = decimal.Parse(dataTable.Rows[0]["EmptyUnitsValue"].ToString()).ToString("G29");
		((Control)(object)txtReservedUnitCount).Text = dataTable.Rows[0]["ReservedUnitsCount"].ToString();
		((Control)(object)txtReservedUnitValue).Text = decimal.Parse(dataTable.Rows[0]["ReservedUnitsValues"].ToString()).ToString("G29");
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
		if (cboBuildings.SelectedIndex > -1)
		{
			GlobalVariables.ReportDocument = new ReportDocument();
			GlobalVariables.ReportDocument.Load(GlobalVariables.ReportsPath + (GlobalVariables.IsArabic ? "Rep_Con_BuildingsState_A.rpt" : "Rep_Con_BuildingsState_E.rpt"));
			frmReporViwer frmReporViwer2 = new frmReporViwer();
			GlobalVariables.ReportDocument.SetParameterValue("@BuildingID", ((TextEditorControlBase)cboBuildings).Value.ToString());
			GlobalVariables.ReportDocument.SetParameterValue("@IsArabic", GlobalVariables.IsArabic ? "1" : "0");
			frmReporViwer2.ShowDialog();
			frmReporViwer2 = null;
			GlobalVariables.ReportDocument = null;
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
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Expected O, but got Unknown
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Expected O, but got Unknown
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Expected O, but got Unknown
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Expected O, but got Unknown
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Expected O, but got Unknown
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Expected O, but got Unknown
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Expected O, but got Unknown
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Expected O, but got Unknown
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected O, but got Unknown
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Constructions.Transactions.frmBuildingsStates));
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
		this.pnlBuildings = new UltraPanel();
		this.pnlBalances = new UltraPanel();
		this.pnlStores = new UltraPanel();
		this.pnlPark = new UltraPanel();
		this.pnlCheckType = new UltraPanel();
		this.rbUnitStatement = new System.Windows.Forms.RadioButton();
		this.rbClientStatement = new System.Windows.Forms.RadioButton();
		this.rbInstallmentPayment = new System.Windows.Forms.RadioButton();
		this.rbContract = new System.Windows.Forms.RadioButton();
		this.pnlVilla = new UltraPanel();
		this.btnKeyboard = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.UGBBuildings = new UltraGroupBox();
		this.lblProjects = new UltraLabel();
		this.cboProject = new UltraComboEditor();
		this.lblBuildings = new UltraLabel();
		this.cboBuildings = new UltraComboEditor();
		this.UGBBalances = new UltraGroupBox();
		this.UGBStores = new UltraGroupBox();
		this.UGBPark = new UltraGroupBox();
		this.lblFreeUnit = new UltraLabel();
		this.lblLiability = new UltraLabel();
		this.lblFreeUnitColor = new UltraLabel();
		this.lblLiabilityColor = new UltraLabel();
		this.lblSold = new UltraLabel();
		this.lblInstallment = new UltraLabel();
		this.lblSoldColor = new UltraLabel();
		this.lblInstallmentColor = new UltraLabel();
		this.lblReserved = new UltraLabel();
		this.lblReservedColor = new UltraLabel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.UGBVilla = new UltraGroupBox();
		this.lblLiabilityColor2 = new UltraLabel();
		this.lblLiabilityColor3 = new UltraLabel();
		this.lblUnitsCount = new UltraLabel();
		this.txtTotalUnitsCount = new UltraTextEditor();
		this.txtSalesUnitsCount = new UltraTextEditor();
		this.lblSalesUnitsCount = new UltraLabel();
		this.txtSalesUnitsValues = new UltraTextEditor();
		this.txtEmptyUnitsValue = new UltraTextEditor();
		this.txtEmptyUnitsCount = new UltraTextEditor();
		this.lblEmptyUnitsCount = new UltraLabel();
		this.btnPrint = new UltraButton();
		this.txtReservedUnitValue = new UltraTextEditor();
		this.txtReservedUnitCount = new UltraTextEditor();
		this.lblReservedUnits = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlBuildings).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlBalances).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlStores).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlPark).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlVilla).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBBuildings).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBBuildings).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.cboProject).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuildings).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBBalances).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBBalances).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBStores).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBStores).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBPark).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBPark).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBVilla).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBVilla).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtTotalUnitsCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesUnitsCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesUnitsValues).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmptyUnitsValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmptyUnitsCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReservedUnitValue).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtReservedUnitCount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblTop, resources.GetString("lblTop.ToolTip"));
		resources.ApplyResources(base.lblBottom, "lblBottom");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblBottom, resources.GetString("lblBottom.ToolTip"));
		resources.ApplyResources(base.lblLeft, "lblLeft");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblLeft, resources.GetString("lblLeft.ToolTip"));
		resources.ApplyResources(base.lblRight, "lblRight");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)base.lblRight, resources.GetString("lblRight.ToolTip"));
		resources.ApplyResources(this.pnlBuildings, "pnlBuildings");
		this.pnlBuildings.AutoScroll = true;
		this.pnlBuildings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		resources.ApplyResources(this.pnlBuildings.ClientArea, "pnlBuildings.ClientArea");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlBuildings.ClientArea, resources.GetString("pnlBuildings.ClientArea.ToolTip"));
		((System.Windows.Forms.Control)(object)this.pnlBuildings).Name = "pnlBuildings";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlBuildings, resources.GetString("pnlBuildings.ToolTip"));
		resources.ApplyResources(this.pnlBalances, "pnlBalances");
		this.pnlBalances.AutoScroll = true;
		resources.ApplyResources(this.pnlBalances.ClientArea, "pnlBalances.ClientArea");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlBalances.ClientArea, resources.GetString("pnlBalances.ClientArea.ToolTip"));
		((System.Windows.Forms.Control)(object)this.pnlBalances).Name = "pnlBalances";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlBalances, resources.GetString("pnlBalances.ToolTip"));
		resources.ApplyResources(this.pnlStores, "pnlStores");
		this.pnlStores.AutoScroll = true;
		resources.ApplyResources(this.pnlStores.ClientArea, "pnlStores.ClientArea");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlStores.ClientArea, resources.GetString("pnlStores.ClientArea.ToolTip"));
		((System.Windows.Forms.Control)(object)this.pnlStores).Name = "pnlStores";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlStores, resources.GetString("pnlStores.ToolTip"));
		resources.ApplyResources(this.pnlPark, "pnlPark");
		this.pnlPark.AutoScroll = true;
		resources.ApplyResources(this.pnlPark.ClientArea, "pnlPark.ClientArea");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlPark.ClientArea, resources.GetString("pnlPark.ClientArea.ToolTip"));
		((System.Windows.Forms.Control)(object)this.pnlPark).Name = "pnlPark";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlPark, resources.GetString("pnlPark.ToolTip"));
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val, "appearance1");
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val;
		resources.ApplyResources(this.pnlCheckType.ClientArea, "pnlCheckType.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbUnitStatement);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbClientStatement);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbInstallmentPayment);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbContract);
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea, resources.GetString("pnlCheckType.ClientArea.ToolTip"));
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlCheckType, resources.GetString("pnlCheckType.ToolTip"));
		resources.ApplyResources(this.rbUnitStatement, "rbUnitStatement");
		this.rbUnitStatement.BackColor = System.Drawing.Color.Transparent;
		this.rbUnitStatement.Name = "rbUnitStatement";
		this.rbUnitStatement.TabStop = true;
		this.toolTip1.SetToolTip(this.rbUnitStatement, resources.GetString("rbUnitStatement.ToolTip"));
		this.rbUnitStatement.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbClientStatement, "rbClientStatement");
		this.rbClientStatement.BackColor = System.Drawing.Color.Transparent;
		this.rbClientStatement.Name = "rbClientStatement";
		this.rbClientStatement.TabStop = true;
		this.toolTip1.SetToolTip(this.rbClientStatement, resources.GetString("rbClientStatement.ToolTip"));
		this.rbClientStatement.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbInstallmentPayment, "rbInstallmentPayment");
		this.rbInstallmentPayment.BackColor = System.Drawing.Color.Transparent;
		this.rbInstallmentPayment.Name = "rbInstallmentPayment";
		this.rbInstallmentPayment.TabStop = true;
		this.toolTip1.SetToolTip(this.rbInstallmentPayment, resources.GetString("rbInstallmentPayment.ToolTip"));
		this.rbInstallmentPayment.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.rbContract, "rbContract");
		this.rbContract.BackColor = System.Drawing.Color.Transparent;
		this.rbContract.Name = "rbContract";
		this.rbContract.TabStop = true;
		this.toolTip1.SetToolTip(this.rbContract, resources.GetString("rbContract.ToolTip"));
		this.rbContract.UseVisualStyleBackColor = false;
		resources.ApplyResources(this.pnlVilla, "pnlVilla");
		this.pnlVilla.AutoScroll = true;
		resources.ApplyResources(this.pnlVilla.ClientArea, "pnlVilla.ClientArea");
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlVilla.ClientArea, resources.GetString("pnlVilla.ClientArea.ToolTip"));
		((System.Windows.Forms.Control)(object)this.pnlVilla).Name = "pnlVilla";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.pnlVilla, resources.GetString("pnlVilla.ToolTip"));
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.btnKeyboard, resources.GetString("btnKeyboard.ToolTip"));
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.btnClose, resources.GetString("btnClose.ToolTip"));
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblTitle, resources.GetString("lblTitle.ToolTip"));
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblTitle2, resources.GetString("lblTitle2.ToolTip"));
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.UGBBuildings, "UGBBuildings");
		this.UGBBuildings.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBBuildings).Controls.Add((System.Windows.Forms.Control)(object)this.pnlBuildings);
		((System.Windows.Forms.Control)(object)this.UGBBuildings).Name = "UGBBuildings";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.UGBBuildings, resources.GetString("UGBBuildings.ToolTip"));
		resources.ApplyResources(this.lblProjects, "lblProjects");
		this.lblProjects.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblProjects).Name = "lblProjects";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblProjects, resources.GetString("lblProjects.ToolTip"));
		((ControlBase)this.lblProjects).WrapText = false;
		resources.ApplyResources(this.cboProject, "cboProject");
		((TextEditorControlBase)this.cboProject).AlwaysInEditMode = true;
		this.cboProject.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboProject).Name = "cboProject";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.cboProject, resources.GetString("cboProject.ToolTip"));
		((TextEditorControlBase)this.cboProject).ValueChanged += new System.EventHandler(cboProject_ValueChanged);
		resources.ApplyResources(this.lblBuildings, "lblBuildings");
		this.lblBuildings.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBuildings).Name = "lblBuildings";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblBuildings, resources.GetString("lblBuildings.ToolTip"));
		((ControlBase)this.lblBuildings).WrapText = false;
		resources.ApplyResources(this.cboBuildings, "cboBuildings");
		((TextEditorControlBase)this.cboBuildings).AlwaysInEditMode = true;
		this.cboBuildings.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboBuildings).Name = "cboBuildings";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.cboBuildings, resources.GetString("cboBuildings.ToolTip"));
		((TextEditorControlBase)this.cboBuildings).ValueChanged += new System.EventHandler(cboBuildings_ValueChanged);
		resources.ApplyResources(this.UGBBalances, "UGBBalances");
		this.UGBBalances.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBBalances).Controls.Add((System.Windows.Forms.Control)(object)this.pnlBalances);
		((System.Windows.Forms.Control)(object)this.UGBBalances).Name = "UGBBalances";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.UGBBalances, resources.GetString("UGBBalances.ToolTip"));
		resources.ApplyResources(this.UGBStores, "UGBStores");
		this.UGBStores.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBStores).Controls.Add((System.Windows.Forms.Control)(object)this.pnlStores);
		((System.Windows.Forms.Control)(object)this.UGBStores).Name = "UGBStores";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.UGBStores, resources.GetString("UGBStores.ToolTip"));
		resources.ApplyResources(this.UGBPark, "UGBPark");
		this.UGBPark.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBPark).Controls.Add((System.Windows.Forms.Control)(object)this.pnlPark);
		((System.Windows.Forms.Control)(object)this.UGBPark).Name = "UGBPark";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.UGBPark, resources.GetString("UGBPark.ToolTip"));
		resources.ApplyResources(this.lblFreeUnit, "lblFreeUnit");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblFreeUnit).Appearance = (AppearanceBase)(object)val6;
		this.lblFreeUnit.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFreeUnit).Name = "lblFreeUnit";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblFreeUnit, resources.GetString("lblFreeUnit.ToolTip"));
		((ControlBase)this.lblFreeUnit).WrapText = false;
		resources.ApplyResources(this.lblLiability, "lblLiability");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.lblLiability).Appearance = (AppearanceBase)(object)val7;
		this.lblLiability.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLiability).Name = "lblLiability";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblLiability, resources.GetString("lblLiability.ToolTip"));
		((UltraControlBase)this.lblLiability).UseAppStyling = false;
		((ControlBase)this.lblLiability).WrapText = false;
		resources.ApplyResources(this.lblFreeUnitColor, "lblFreeUnitColor");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.LightGray;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblFreeUnitColor).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblFreeUnitColor).Name = "lblFreeUnitColor";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblFreeUnitColor, resources.GetString("lblFreeUnitColor.ToolTip"));
		((UltraControlBase)this.lblFreeUnitColor).UseAppStyling = false;
		resources.ApplyResources(this.lblLiabilityColor, "lblLiabilityColor");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Red;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblLiabilityColor).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.lblLiabilityColor).Name = "lblLiabilityColor";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblLiabilityColor, resources.GetString("lblLiabilityColor.ToolTip"));
		((UltraControlBase)this.lblLiabilityColor).UseAppStyling = false;
		resources.ApplyResources(this.lblSold, "lblSold");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblSold).Appearance = (AppearanceBase)(object)val10;
		this.lblSold.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSold).Name = "lblSold";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblSold, resources.GetString("lblSold.ToolTip"));
		((ControlBase)this.lblSold).WrapText = false;
		resources.ApplyResources(this.lblInstallment, "lblInstallment");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.lblInstallment).Appearance = (AppearanceBase)(object)val11;
		this.lblInstallment.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInstallment).Name = "lblInstallment";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblInstallment, resources.GetString("lblInstallment.ToolTip"));
		((UltraControlBase)this.lblInstallment).UseAppStyling = false;
		((ControlBase)this.lblInstallment).WrapText = false;
		resources.ApplyResources(this.lblSoldColor, "lblSoldColor");
		((AppearanceBase)val12).BackColor = System.Drawing.Color.FromArgb(255, 128, 255);
		resources.ApplyResources(val12, "appearance12");
		((ControlBase)this.lblSoldColor).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.lblSoldColor).Name = "lblSoldColor";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblSoldColor, resources.GetString("lblSoldColor.ToolTip"));
		((UltraControlBase)this.lblSoldColor).UseAppStyling = false;
		resources.ApplyResources(this.lblInstallmentColor, "lblInstallmentColor");
		((AppearanceBase)val13).BackColor = System.Drawing.Color.Yellow;
		resources.ApplyResources(val13, "appearance13");
		((ControlBase)this.lblInstallmentColor).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.lblInstallmentColor).Name = "lblInstallmentColor";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblInstallmentColor, resources.GetString("lblInstallmentColor.ToolTip"));
		((UltraControlBase)this.lblInstallmentColor).UseAppStyling = false;
		resources.ApplyResources(this.lblReserved, "lblReserved");
		((AppearanceBase)val14).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val14, "appearance14");
		((ControlBase)this.lblReserved).Appearance = (AppearanceBase)(object)val14;
		this.lblReserved.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReserved).Name = "lblReserved";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblReserved, resources.GetString("lblReserved.ToolTip"));
		((UltraControlBase)this.lblReserved).UseAppStyling = false;
		((ControlBase)this.lblReserved).WrapText = false;
		resources.ApplyResources(this.lblReservedColor, "lblReservedColor");
		((AppearanceBase)val15).BackColor = System.Drawing.Color.Lime;
		resources.ApplyResources(val15, "appearance15");
		((ControlBase)this.lblReservedColor).Appearance = (AppearanceBase)(object)val15;
		((System.Windows.Forms.Control)(object)this.lblReservedColor).Name = "lblReservedColor";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblReservedColor, resources.GetString("lblReservedColor.ToolTip"));
		((UltraControlBase)this.lblReservedColor).UseAppStyling = false;
		this.toolTip1.IsBalloon = true;
		resources.ApplyResources(this.UGBVilla, "UGBVilla");
		this.UGBVilla.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBVilla).Controls.Add((System.Windows.Forms.Control)(object)this.pnlVilla);
		((System.Windows.Forms.Control)(object)this.UGBVilla).Name = "UGBVilla";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.UGBVilla, resources.GetString("UGBVilla.ToolTip"));
		resources.ApplyResources(this.lblLiabilityColor2, "lblLiabilityColor2");
		((AppearanceBase)val16).BackColor = System.Drawing.Color.FromArgb(212, 0, 0);
		resources.ApplyResources(val16, "appearance16");
		((ControlBase)this.lblLiabilityColor2).Appearance = (AppearanceBase)(object)val16;
		((System.Windows.Forms.Control)(object)this.lblLiabilityColor2).Name = "lblLiabilityColor2";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblLiabilityColor2, resources.GetString("lblLiabilityColor2.ToolTip"));
		((UltraControlBase)this.lblLiabilityColor2).UseAppStyling = false;
		resources.ApplyResources(this.lblLiabilityColor3, "lblLiabilityColor3");
		((AppearanceBase)val17).BackColor = System.Drawing.Color.FromArgb(175, 0, 0);
		resources.ApplyResources(val17, "appearance17");
		((ControlBase)this.lblLiabilityColor3).Appearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.lblLiabilityColor3).Name = "lblLiabilityColor3";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblLiabilityColor3, resources.GetString("lblLiabilityColor3.ToolTip"));
		((UltraControlBase)this.lblLiabilityColor3).UseAppStyling = false;
		resources.ApplyResources(this.lblUnitsCount, "lblUnitsCount");
		this.lblUnitsCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblUnitsCount).Name = "lblUnitsCount";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblUnitsCount, resources.GetString("lblUnitsCount.ToolTip"));
		((ControlBase)this.lblUnitsCount).WrapText = false;
		resources.ApplyResources(this.txtTotalUnitsCount, "txtTotalUnitsCount");
		((System.Windows.Forms.Control)(object)this.txtTotalUnitsCount).Name = "txtTotalUnitsCount";
		((EditorButtonControlBase)this.txtTotalUnitsCount).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtTotalUnitsCount, resources.GetString("txtTotalUnitsCount.ToolTip"));
		resources.ApplyResources(this.txtSalesUnitsCount, "txtSalesUnitsCount");
		((System.Windows.Forms.Control)(object)this.txtSalesUnitsCount).Name = "txtSalesUnitsCount";
		((EditorButtonControlBase)this.txtSalesUnitsCount).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtSalesUnitsCount, resources.GetString("txtSalesUnitsCount.ToolTip"));
		resources.ApplyResources(this.lblSalesUnitsCount, "lblSalesUnitsCount");
		this.lblSalesUnitsCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSalesUnitsCount).Name = "lblSalesUnitsCount";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblSalesUnitsCount, resources.GetString("lblSalesUnitsCount.ToolTip"));
		((ControlBase)this.lblSalesUnitsCount).WrapText = false;
		resources.ApplyResources(this.txtSalesUnitsValues, "txtSalesUnitsValues");
		((System.Windows.Forms.Control)(object)this.txtSalesUnitsValues).Name = "txtSalesUnitsValues";
		((EditorButtonControlBase)this.txtSalesUnitsValues).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtSalesUnitsValues, resources.GetString("txtSalesUnitsValues.ToolTip"));
		resources.ApplyResources(this.txtEmptyUnitsValue, "txtEmptyUnitsValue");
		((System.Windows.Forms.Control)(object)this.txtEmptyUnitsValue).Name = "txtEmptyUnitsValue";
		((EditorButtonControlBase)this.txtEmptyUnitsValue).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtEmptyUnitsValue, resources.GetString("txtEmptyUnitsValue.ToolTip"));
		resources.ApplyResources(this.txtEmptyUnitsCount, "txtEmptyUnitsCount");
		((System.Windows.Forms.Control)(object)this.txtEmptyUnitsCount).Name = "txtEmptyUnitsCount";
		((EditorButtonControlBase)this.txtEmptyUnitsCount).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtEmptyUnitsCount, resources.GetString("txtEmptyUnitsCount.ToolTip"));
		resources.ApplyResources(this.lblEmptyUnitsCount, "lblEmptyUnitsCount");
		this.lblEmptyUnitsCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblEmptyUnitsCount).Name = "lblEmptyUnitsCount";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblEmptyUnitsCount, resources.GetString("lblEmptyUnitsCount.ToolTip"));
		((ControlBase)this.lblEmptyUnitsCount).WrapText = false;
		resources.ApplyResources(this.btnPrint, "btnPrint");
		((System.Windows.Forms.Control)(object)this.btnPrint).Name = "btnPrint";
		((UltraButtonBase)this.btnPrint).ShowOutline = false;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.btnPrint, resources.GetString("btnPrint.ToolTip"));
		((System.Windows.Forms.Control)(object)this.btnPrint).Click += new System.EventHandler(btnPrint_Click);
		resources.ApplyResources(this.txtReservedUnitValue, "txtReservedUnitValue");
		((System.Windows.Forms.Control)(object)this.txtReservedUnitValue).Name = "txtReservedUnitValue";
		((EditorButtonControlBase)this.txtReservedUnitValue).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtReservedUnitValue, resources.GetString("txtReservedUnitValue.ToolTip"));
		resources.ApplyResources(this.txtReservedUnitCount, "txtReservedUnitCount");
		((System.Windows.Forms.Control)(object)this.txtReservedUnitCount).Name = "txtReservedUnitCount";
		((EditorButtonControlBase)this.txtReservedUnitCount).ReadOnly = true;
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.txtReservedUnitCount, resources.GetString("txtReservedUnitCount.ToolTip"));
		resources.ApplyResources(this.lblReservedUnits, "lblReservedUnits");
		this.lblReservedUnits.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReservedUnits).Name = "lblReservedUnits";
		this.toolTip1.SetToolTip((System.Windows.Forms.Control)(object)this.lblReservedUnits, resources.GetString("lblReservedUnits.ToolTip"));
		((ControlBase)this.lblReservedUnits).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReservedUnitValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtReservedUnitCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReservedUnits);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrint);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEmptyUnitsValue);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEmptyUnitsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEmptyUnitsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesUnitsValues);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSalesUnitsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesUnitsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTotalUnitsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblUnitsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBVilla);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReserved);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReservedColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSold);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallment);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSoldColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallmentColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFreeUnit);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLiability);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFreeUnitColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLiabilityColor3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLiabilityColor2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLiabilityColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBPark);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBStores);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBBalances);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBuildings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboBuildings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblProjects);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboProject);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBBuildings);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmBuildingsStates";
		this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBBuildings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboProject, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblProjects, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboBuildings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBuildings, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBBalances, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBStores, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBPark, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLiabilityColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLiabilityColor2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLiabilityColor3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFreeUnitColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLiability, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFreeUnit, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallmentColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSoldColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallment, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSold, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReservedColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReserved, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBVilla, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblUnitsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTotalUnitsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesUnitsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesUnitsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSalesUnitsValues, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEmptyUnitsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEmptyUnitsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEmptyUnitsValue, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblReservedUnits, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReservedUnitCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtReservedUnitValue, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlBuildings).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlBuildings).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlBalances).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlStores).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlPark).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlVilla).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBBuildings).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBBuildings).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.UGBBuildings).PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.cboProject).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboBuildings).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBBalances).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBBalances).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBStores).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBStores).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBPark).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBPark).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBVilla).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBVilla).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtTotalUnitsCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesUnitsCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSalesUnitsValues).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmptyUnitsValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEmptyUnitsCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReservedUnitValue).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtReservedUnitCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
