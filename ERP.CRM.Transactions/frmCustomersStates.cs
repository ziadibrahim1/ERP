using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.CRM;
using BusinessLayer.HR;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.CRM.MasterData;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.CRM.Transactions;

public class frmCustomersStates : frmBase
{
	private DataTable dtUsers;

	private DataTable dtSalesMen;

	private DataTable dtCommunicationSteps;

	private DataTable dtCustomersWithinState;

	private bool CanViewCustomer;

	private bool CanAddFollowUp;

	private IContainer components = null;

	public UltraButton btnKeyboard;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	private UltraGroupBox UGBSteps;

	public UltraLabel lblDelay;

	public UltraLabel lblInTime;

	public UltraLabel lblInTimeColor;

	private ToolTip toolTip1;

	public UltraLabel lblDelayColor;

	private UltraButton btnPrint;

	private UltraLabel lblSalesMan;

	private UltraComboEditor cboSalesMen;

	private UltraButton btnRefresh;

	private UltraCheckEditor chkAllSales;

	public frmCustomersStates()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtUsers = Users.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		dtSalesMen = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMen, dtSalesMen, "SubAccountID", "SubAccountName");
		dtCustomersWithinState = Customers.SelectWithNextState(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtCommunicationSteps = CommunicationSteps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView defaultView = dtCommunicationSteps.DefaultView;
		defaultView.Sort = "CommunicationStepID ASC";
		dtCommunicationSteps = defaultView.Table;
		CanViewCustomer = GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.MasterData.frmCustomersTree'").Length != 0;
		if (GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.Transactions.frmCustomersFollows'").Length != 0)
		{
			CanAddFollowUp = GlobalFunctions.GetFormFunction(GlobalVariables.dtForms.Select("FormFullName = 'ERP.CRM.Transactions.frmCustomersFollows'")[0]["FormID"].ToString(), "Adding");
		}
	}

	private void cboSalesMen_ValueChanged(object sender, EventArgs e)
	{
		toolTip1 = new ToolTip();
		toolTip1.AutomaticDelay = 500;
		toolTip1.ShowAlways = true;
		toolTip1.IsBalloon = true;
		((UltraToggleEditorBase)chkAllSales).CheckedChanged -= chkAllSales_CheckedChanged;
		if (cboSalesMen.SelectedIndex > -1)
		{
			((UltraToggleEditorBase)chkAllSales).Checked = false;
		}
		((UltraToggleEditorBase)chkAllSales).CheckedChanged += chkAllSales_CheckedChanged;
		if (((UltraToggleEditorBase)chkAllSales).Checked || cboSalesMen.SelectedIndex > -1)
		{
			Draw();
		}
	}

	private void Draw()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		((Control)(object)UGBSteps).Controls.Clear();
		int num = ((Control)(object)UGBSteps).Width - 40;
		DataTable dtCustomers = dtCustomersWithinState;
		if (!((UltraToggleEditorBase)chkAllSales).Checked && cboSalesMen.SelectedIndex > -1)
		{
			DataView dataView = new DataView(dtCustomersWithinState);
			dataView.RowFilter = "NextEmployeeSubAccountID = " + ((TextEditorControlBase)cboSalesMen).Value.ToString();
			dtCustomers = dataView.ToTable();
		}
		TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
		tableLayoutPanel.Width = num;
		tableLayoutPanel.Dock = DockStyle.Fill;
		tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
		tableLayoutPanel.AutoScroll = true;
		tableLayoutPanel.Padding = new Padding(10);
		tableLayoutPanel.BorderStyle = BorderStyle.FixedSingle;
		tableLayoutPanel.VerticalScroll.Maximum = 20;
		((Control)(object)UGBSteps).Controls.Add(tableLayoutPanel);
		int nextj = 0;
		for (int i = 0; i < dtCommunicationSteps.Rows.Count; i++)
		{
			UltraExpandableGroupBox val = new UltraExpandableGroupBox();
			((Control)(object)val).Name = dtCommunicationSteps.Rows[i]["CommunicationStepID"].ToString();
			((Control)(object)val).Text = dtCommunicationSteps.Rows[i]["CommunicationStepName"].ToString();
			((UltraGroupBox)val).CaptionAlignment = (GroupBoxCaptionAlignment)1;
			((Control)(object)val).Width = tableLayoutPanel.Width - 40;
			((Control)(object)val).Dock = DockStyle.Top;
			((Control)(object)val).Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
			((UltraGroupBox)val).BorderStyle = (GroupBoxBorderStyle)13;
			((UltraGroupBox)val).HeaderBorderStyle = (UIElementBorderStyle)13;
			val.HeaderClickAction = (GroupBoxHeaderClickAction)2;
			((UltraGroupBox)val).HeaderPosition = (GroupBoxHeaderPosition)3;
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
			flowLayoutPanel.AutoSize = true;
			flowLayoutPanel.Width = tableLayoutPanel.Width - 30;
			flowLayoutPanel.Dock = DockStyle.Top;
			flowLayoutPanel.AutoSizeMode = AutoSizeMode.GrowOnly;
			flowLayoutPanel.FlowDirection = ((!GlobalVariables.IsArabic) ? FlowDirection.RightToLeft : FlowDirection.LeftToRight);
			flowLayoutPanel.Name = dtCommunicationSteps.Rows[i]["CommunicationStepID"].ToString();
			flowLayoutPanel.Text = dtCommunicationSteps.Rows[i]["CommunicationStepName"].ToString();
			flowLayoutPanel.SuspendLayout();
			nextj = createCustomerButton(dtCustomers, nextj, flowLayoutPanel, dtCommunicationSteps.Rows[i]["CommunicationStepID"].ToString());
			flowLayoutPanel.ResumeLayout(performLayout: true);
			flowLayoutPanel.PerformLayout();
			tableLayoutPanel.Controls.Add((Control)(object)val);
			((Control)(object)val.Panel).AutoSize = true;
			((Panel)(object)val.Panel).AutoSizeMode = AutoSizeMode.GrowAndShrink;
			((Control)(object)val.Panel).Controls.Add(flowLayoutPanel);
			ResumeLayout(performLayout: true);
			((Control)(object)val).Height = flowLayoutPanel.Height + 30;
		}
	}

	private int createCustomerButton(DataTable dtCustomers, int nextj, FlowLayoutPanel parent, string CommunicationStepID)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		int num = 10;
		int top = 10;
		int num2 = (parent.Width - 5 * num) / 6;
		int num3 = 50;
		int num4 = 0;
		for (int i = nextj; i < dtCustomers.Rows.Count; i++)
		{
			if (dtCustomers.Rows[i]["CommunicationStepID"].ToString() == CommunicationStepID)
			{
				UltraButton val = new UltraButton();
				((Control)(object)val).Tag = dtCustomers.Rows[i]["CustomerID"].ToString();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(dtCustomers.Rows[i]["CustomerName"].ToString());
				stringBuilder.Append((dtCustomers.Rows[i]["PhoneNumber"] != DBNull.Value && dtCustomers.Rows[i]["PhoneNumber"].ToString() != "") ? ("\n Tel.:" + dtCustomers.Rows[i]["PhoneNumber"].ToString()) : "");
				stringBuilder.Append((dtCustomers.Rows[i]["MobileNumber"] != DBNull.Value && dtCustomers.Rows[i]["MobileNumber"].ToString() != "") ? ("\n Mob.:" + dtCustomers.Rows[i]["MobileNumber"].ToString()) : "");
				((Control)(object)val).Text = stringBuilder.ToString();
				((ControlBase)val).Appearance.ForeColor = Color.White;
				((ControlBase)val).Appearance.ThemedElementAlpha = (Alpha)3;
				((UltraButtonBase)val).ButtonStyle = (UIElementButtonStyle)4;
				((ControlBase)val).Appearance.FontData.Bold = (DefaultableBoolean)1;
				((ControlBase)val).Appearance.FontData.SizeInPoints = 10f;
				((ControlBase)val).Appearance.FontData.Name = "Times New Roman";
				((ControlBase)val).Appearance.TextHAlign = (HAlign)2;
				((ControlBase)val).Appearance.TextVAlign = (VAlign)2;
				((ControlBase)val).Appearance.BorderColor = Color.Black;
				if (int.Parse(dtCustomers.Rows[i]["DelayDays"].ToString()) < 0)
				{
					((ControlBase)val).Appearance.BackColor = ((ControlBase)lblDelayColor).Appearance.BackColor;
				}
				else
				{
					((ControlBase)val).Appearance.BackColor = ((ControlBase)lblInTimeColor).Appearance.BackColor;
				}
				((Control)(object)val).Left = num;
				((Control)(object)val).Top = top;
				((Control)(object)val).Width = num2;
				((Control)(object)val).Height = num3;
				string text = "";
				text = ((dtCustomers.Rows[i]["SalesEmployeeName"] != DBNull.Value) ? dtCustomers.Rows[i]["SalesEmployeeName"].ToString() : "");
				text += ((dtCustomers.Rows[i]["NextFollowUpDate"] != DBNull.Value) ? ("\n" + DateTime.Parse(dtCustomers.Rows[i]["NextFollowUpDate"].ToString()).ToString("MMMM dd yyyy")) : "");
				if (text != "")
				{
					toolTip1.SetToolTip((Control)(object)val, text);
				}
				if (CanAddFollowUp)
				{
					((Control)(object)val).Click += button_Click;
				}
				((UltraControlBase)val).UseAppStyling = false;
				num4++;
				ContextMenu contextMenu = new ContextMenu();
				MenuItem menuItem = new MenuItem();
				menuItem.Tag = dtCustomers.Rows[i]["CustomerID"].ToString();
				menuItem.Text = (GlobalVariables.IsArabic ? "عرض بيانات العميل" : "Display Customer Info.");
				menuItem.Click += MenuItem_Click;
				contextMenu.MenuItems.Add(menuItem);
				if (GlobalVariables.dtForms.Select("Form = 'frmCustomersTree'").Length == 0)
				{
					menuItem.Enabled = false;
				}
				MenuItem menuItem2 = new MenuItem();
				menuItem2.Tag = dtCustomers.Rows[i]["CustomerID"].ToString();
				menuItem2.Text = (GlobalVariables.IsArabic ? "حجز" : "Reservation");
				menuItem2.Click += MenuItem_Click;
				contextMenu.MenuItems.Add(menuItem2);
				if (GlobalVariables.dtForms.Select("Form = 'frmReservation'").Length == 0)
				{
					menuItem2.Enabled = false;
				}
				contextMenu.RightToLeft = (GlobalVariables.IsArabic ? RightToLeft.Yes : RightToLeft.No);
				((Control)(object)val).ContextMenu = contextMenu;
				parent.Controls.Add((Control)(object)val);
				continue;
			}
			nextj = i;
			break;
		}
		return nextj;
	}

	public void button_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		UltraButton val = (UltraButton)sender;
		frmCustomersFollows frmCustomersFollows2 = new frmCustomersFollows(int.Parse(((Control)(object)val).Tag.ToString()));
		frmCustomersFollows2.StartPosition = FormStartPosition.CenterParent;
		frmCustomersFollows2.Size = new Size(Screen.PrimaryScreen.Bounds.Width - 200, Screen.PrimaryScreen.Bounds.Height - 200);
		((Control)(object)frmCustomersFollows2.lblTitle).Text = (GlobalVariables.IsArabic ? "متابعة العملاء" : "Customers Follows");
		frmCustomersFollows2.ShowDialog();
		reLoadData();
		Draw();
	}

	public void MenuItem_Click(object sender, EventArgs e)
	{
		MenuItem menuItem = (MenuItem)sender;
		if (menuItem.Text == (GlobalVariables.IsArabic ? "عرض بيانات العميل" : "Display Customer Info."))
		{
			frmCustomersTree frmCustomersTree2 = new frmCustomersTree(int.Parse(menuItem.Tag.ToString()), 1);
			frmCustomersTree2.StartPosition = FormStartPosition.CenterParent;
			((Control)(object)frmCustomersTree2.lblTitle).Text = (GlobalVariables.IsArabic ? "العملاء" : "Customers");
			frmCustomersTree2.ShowDialog();
		}
		reLoadData();
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void reLoadData()
	{
		object value = ((TextEditorControlBase)cboSalesMen).Value;
		dtSalesMen = Employees.FillCombo(GlobalVariables.EmployeeSubAccountTypeIDs, GlobalVariables.BranchIDs, "-1", "1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboSalesMen, dtSalesMen, "SubAccountID", "SubAccountName");
		dtCustomersWithinState = Customers.SelectWithNextState(GlobalVariables.BranchIDs, GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		dtCommunicationSteps = CommunicationSteps.FillCombo(GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		DataView defaultView = dtCommunicationSteps.DefaultView;
		defaultView.Sort = "CommunicationStepID ASC";
		dtCommunicationSteps = defaultView.Table;
		((TextEditorControlBase)cboSalesMen).Value = value;
	}

	private void chkAllSales_CheckedChanged(object sender, EventArgs e)
	{
		toolTip1 = new ToolTip();
		toolTip1.AutomaticDelay = 500;
		toolTip1.ShowAlways = true;
		toolTip1.IsBalloon = true;
		((TextEditorControlBase)cboSalesMen).ValueChanged -= cboSalesMen_ValueChanged;
		if (((UltraToggleEditorBase)chkAllSales).Checked)
		{
			cboSalesMen.SelectedIndex = -1;
		}
		else
		{
			((TextEditorControlBase)cboSalesMen).Value = dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"].ToString();
		}
		((TextEditorControlBase)cboSalesMen).ValueChanged += cboSalesMen_ValueChanged;
		if (((UltraToggleEditorBase)chkAllSales).Checked || cboSalesMen.SelectedIndex > -1)
		{
			Draw();
		}
	}

	private void frmCustomersStates_Load(object sender, EventArgs e)
	{
		((Control)(object)chkAllSales).Enabled = ViewAllEmployees;
		((EditorButtonControlBase)cboSalesMen).ReadOnly = !ViewAllEmployees;
		((TextEditorControlBase)cboSalesMen).Value = dtUsers.Select("User_ID =" + GlobalVariables.UserID)[0]["SubAccountID"].ToString();
	}

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		reLoadData();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnPrint_Click(object sender, EventArgs e)
	{
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
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CRM.Transactions.frmCustomersStates));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.btnKeyboard = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.UGBSteps = new UltraGroupBox();
		this.lblDelay = new UltraLabel();
		this.lblInTime = new UltraLabel();
		this.lblInTimeColor = new UltraLabel();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.lblDelayColor = new UltraLabel();
		this.btnPrint = new UltraButton();
		this.lblSalesMan = new UltraLabel();
		this.cboSalesMen = new UltraComboEditor();
		this.btnRefresh = new UltraButton();
		this.chkAllSales = new UltraCheckEditor();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBSteps).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMen).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSales).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val2;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val4).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val4).BackGradientStyle = (GradientStyle)7;
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.UGBSteps, "UGBSteps");
		this.UGBSteps.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBSteps).Name = "UGBSteps";
		resources.ApplyResources(this.lblDelay, "lblDelay");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Black;
		((ControlBase)this.lblDelay).Appearance = (AppearanceBase)(object)val5;
		this.lblDelay.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDelay).Name = "lblDelay";
		((UltraControlBase)this.lblDelay).UseAppStyling = false;
		((ControlBase)this.lblDelay).WrapText = false;
		resources.ApplyResources(this.lblInTime, "lblInTime");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Black;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblInTime).Appearance = (AppearanceBase)(object)val6;
		this.lblInTime.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInTime).Name = "lblInTime";
		((UltraControlBase)this.lblInTime).UseAppStyling = false;
		((ControlBase)this.lblInTime).WrapText = false;
		resources.ApplyResources(this.lblInTimeColor, "lblInTimeColor");
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Green;
		((ControlBase)this.lblInTimeColor).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.lblInTimeColor).Name = "lblInTimeColor";
		((UltraControlBase)this.lblInTimeColor).UseAppStyling = false;
		this.toolTip1.IsBalloon = true;
		this.toolTip1.ShowAlways = true;
		resources.ApplyResources(this.lblDelayColor, "lblDelayColor");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.FromArgb(175, 0, 0);
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.lblDelayColor).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.lblDelayColor).Name = "lblDelayColor";
		((UltraControlBase)this.lblDelayColor).UseAppStyling = false;
		resources.ApplyResources(this.btnPrint, "btnPrint");
		((System.Windows.Forms.Control)(object)this.btnPrint).Name = "btnPrint";
		((UltraButtonBase)this.btnPrint).ShowOutline = false;
		((System.Windows.Forms.Control)(object)this.btnPrint).Click += new System.EventHandler(btnPrint_Click);
		this.lblSalesMan.AutoEllipsis = false;
		resources.ApplyResources(this.lblSalesMan, "lblSalesMan");
		((System.Windows.Forms.Control)(object)this.lblSalesMan).Name = "lblSalesMan";
		((ControlBase)this.lblSalesMan).WrapText = false;
		((TextEditorControlBase)this.cboSalesMen).AlwaysInEditMode = true;
		this.cboSalesMen.AutoCompleteMode = (AutoCompleteMode)4;
		resources.ApplyResources(this.cboSalesMen, "cboSalesMen");
		((System.Windows.Forms.Control)(object)this.cboSalesMen).Name = "cboSalesMen";
		((TextEditorControlBase)this.cboSalesMen).ValueChanged += new System.EventHandler(cboSalesMen_ValueChanged);
		resources.ApplyResources(this.btnRefresh, "btnRefresh");
		((System.Windows.Forms.Control)(object)this.btnRefresh).Name = "btnRefresh";
		((UltraButtonBase)this.btnRefresh).ShowOutline = false;
		((System.Windows.Forms.Control)(object)this.btnRefresh).Click += new System.EventHandler(btnRefresh_Click);
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkAllSales).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.chkAllSales, "chkAllSales");
		((System.Windows.Forms.Control)(object)this.chkAllSales).Name = "chkAllSales";
		((UltraToggleEditorBase)this.chkAllSales).CheckedChanged += new System.EventHandler(chkAllSales_CheckedChanged);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkAllSales);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnRefresh);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrint);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInTimeColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDelay);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDelayColor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSalesMan);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSalesMen);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBSteps);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmCustomersStates";
		base.Load += new System.EventHandler(frmCustomersStates_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBSteps, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSalesMen, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSalesMan, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDelayColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDelay, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInTimeColor, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInTime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnRefresh, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkAllSales, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBSteps).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSalesMen).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkAllSales).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
