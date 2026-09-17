using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinessLayer.Accounting;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.Transactions;

public class frmSMS : frmBase
{
	private DataTable dtDetails;

	private DataTable dtSubAcconts;

	private ValueList vlSubAcconts = new ValueList();

	private SerialPort port = new SerialPort();

	private string SenderNo = "00";

	private string InternationalFormat = "91";

	private string MsgDeliver = "11";

	private string MsgRefNo = "";

	private string ReciverNo = "";

	private string protocolIdentifier = "00";

	private string CodingSchema = "08";

	private string ValidityPeriod = "AA";

	private string MsgLength = "";

	private string Msg = "";

	private IContainer components = null;

	private UltraTextEditor txtMesage;

	private UltraLabel lblMesage;

	public UltraButton btnKeyboard;

	public UltraButton btnSend;

	public UltraLabel lblTitle;

	public UltraButton btnClose;

	public UltraLabel lblTitle2;

	private UltraLabel lblPort;

	private UltraComboEditor cboPort;

	public UltraButton btnConnect;

	public UltraButton btnDisconnect;

	private UltraTextEditor txtResult;

	public UltraGrid ULGData;

	public UltraGroupBox UGBDetails;

	public UltraButton btnClear;

	private ProgressBar prgSMS;

	public frmSMS()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
	}

	public override void PrepareData()
	{
		base.PrepareData();
		string[] portNames = SerialPort.GetPortNames();
		foreach (string text in portNames)
		{
			try
			{
				if (port.IsOpen)
				{
					port.Close();
				}
				port.PortName = text;
				port.ReceivedBytesThreshold = 1;
				port.DtrEnable = true;
				if (!port.IsOpen)
				{
					port.Open();
				}
				port.WriteLine("AT\r");
				Thread.Sleep(100);
				string text2 = port.ReadExisting();
				if (text2.IndexOf("OK") > -1)
				{
					cboPort.Items.Add((object)text);
				}
				((Control)(object)txtResult).Text += text2;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		if (((DisposableObjectCollectionBase)cboPort.Items).Count > 0)
		{
			cboPort.SelectedIndex = ((DisposableObjectCollectionBase)cboPort.Items).Count - 1;
		}
		dtSubAcconts = SubAccounts.FillComboBySubAccountTypeIDs(GlobalVariables.ClientSubAccountTypeIDs, "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		vlSubAcconts.ValueListItems.Clear();
		for (int j = 0; j < dtSubAcconts.Rows.Count; j++)
		{
			vlSubAcconts.ValueListItems.Add(dtSubAcconts.Rows[j]["SubAccountID"], dtSubAcconts.Rows[j]["SubAccountName"].ToString());
		}
		dtDetails = SubAccountsClientSupplier.SelectBySubAccountID("0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		((UltraGridBase)ULGData).DataSource = dtDetails;
		InitGrid();
	}

	public void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((Control)(object)ULGData).RightToLeft = RightToLeft.No;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)6;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].ValueList = (IValueList)(object)vlSubAcconts;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SubAccountID"].Header).Caption = (GlobalVariables.IsArabic ? "العميل" : "Client");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Mobile"].Header).Caption = (GlobalVariables.IsArabic ? "المحمول" : "Mobile");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.7);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].Header).Caption = (GlobalVariables.IsArabic ? "الرساله" : "Mesage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].DefaultCellValue = ((Control)(object)txtMesage).Text;
	}

	public void ULGData_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
	{
		e.DisplayPromptMsg = false;
		GlobalVariables.QuestionMB.Show("هل تريد حذف هذه البيانات ؟", "Delete This Data ?");
		if (GlobalVariables.MessageBoxResult != 'Y')
		{
			((CancelEventArgs)(object)e).Cancel = true;
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
		if (port.IsOpen)
		{
			port.Close();
			port.Dispose();
			GC.Collect();
		}
	}

	private void btnConnect_Click(object sender, EventArgs e)
	{
		if (port.IsOpen)
		{
			port.Close();
		}
		port.PortName = ((Control)(object)cboPort).Text;
		port.ReceivedBytesThreshold = 1;
		port.DtrEnable = true;
		if (!port.IsOpen)
		{
			port.Open();
		}
	}

	private void btnDisconnect_Click(object sender, EventArgs e)
	{
		if (port.IsOpen)
		{
			port.Close();
		}
	}

	private void btnSend_Click(object sender, EventArgs e)
	{
		Cursor.Current = Cursors.Default;
		prgSMS.Value = 0;
		prgSMS.Step = 100 / ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			try
			{
				Msg = BitConverter.ToString(Encoding.BigEndianUnicode.GetBytes(((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value.ToString())).Replace("-", "");
				ReciverNo = "";
				string text = "2" + ((UltraGridBase)ULGData).Rows[i].Cells["Mobile"].Value.ToString();
				if (text.Length % 2 == 1)
				{
					text += "F";
				}
				for (int j = 0; j < text.Length; j += 2)
				{
					ReciverNo = ReciverNo + text[j + 1] + text[j];
				}
				MsgRefNo = "000" + ReciverNo.Length.ToString("X");
				MsgLength = (Msg.Length / 2).ToString("X");
				if (MsgLength.Length == 1)
				{
					MsgLength = "0" + MsgLength;
				}
				string text2 = MsgDeliver + MsgRefNo + InternationalFormat + ReciverNo + protocolIdentifier + CodingSchema + ValidityPeriod + MsgLength + Msg;
				int num = text2.Length / 2;
				string text3 = SenderNo + text2 + Convert.ToChar(26);
				port.WriteLine("AT\r");
				port.WriteLine("AT+CMGF=0\r");
				port.WriteLine("AT+CMGS=" + Convert.ToString(num) + "\r");
				Thread.Sleep(50);
				port.WriteLine(text3);
				Thread.Sleep(4000);
				((Control)(object)txtResult).Text += port.ReadExisting();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			prgSMS.PerformStep();
			Refresh();
		}
		prgSMS.Value = 100;
		GlobalVariables.InformationMB.Show("تم الارسال", "send complited");
		Cursor.Current = Cursors.Default;
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).DataSource is DataTable && ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0)
		{
			((DataTable)((UltraGridBase)ULGData).DataSource).Rows.Clear();
		}
	}

	private void ULGData_CellListSelect(object sender, CellEventArgs e)
	{
		if (((KeyedSubObjectBase)e.Cell.Column).Key == "SubAccountID")
		{
			((UltraGridBase)ULGData).UpdateData();
			((UltraGridBase)ULGData).ActiveRow.Cells["Mobile"].Value = dtSubAcconts.Select(" SubAccountID= " + ((UltraGridBase)ULGData).ActiveRow.Cells["SubAccountID"].Value.ToString())[0]["Mobile"];
		}
	}

	private void txtMesage_ValueChanged(object sender, EventArgs e)
	{
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Notes"].DefaultCellValue = ((Control)(object)txtMesage).Text;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			((UltraGridBase)ULGData).Rows[i].Cells["Notes"].Value = ((Control)(object)txtMesage).Text;
		}
	}

	private void ULGData_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F8)
		{
			DataTable dataTable = SearchFunctions.ClientsReport("-1", IsFromServer: false);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dtDetails.NewRow();
				dataRow["SubAccountID"] = dataTable.Rows[i]["SubAccountID"];
				dataRow["Mobile"] = dataTable.Rows[i]["Mobile"];
				dataRow["Notes"] = ((Control)(object)txtMesage).Text;
				dtDetails.Rows.Add(dataRow);
			}
			e.Handled = true;
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
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Expected O, but got Unknown
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a76: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.Transactions.frmSMS));
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
		this.txtMesage = new UltraTextEditor();
		this.lblMesage = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.btnSend = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnClose = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.lblPort = new UltraLabel();
		this.cboPort = new UltraComboEditor();
		this.btnConnect = new UltraButton();
		this.btnDisconnect = new UltraButton();
		this.txtResult = new UltraTextEditor();
		this.ULGData = new UltraGrid();
		this.UGBDetails = new UltraGroupBox();
		this.btnClear = new UltraButton();
		this.prgSMS = new System.Windows.Forms.ProgressBar();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMesage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtResult).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtMesage, "txtMesage");
		((TextEditorControlBase)this.txtMesage).MaxLength = 70;
		((System.Windows.Forms.Control)(object)this.txtMesage).Name = "txtMesage";
		((TextEditorControlBase)this.txtMesage).ValueChanged += new System.EventHandler(txtMesage_ValueChanged);
		resources.ApplyResources(this.lblMesage, "lblMesage");
		this.lblMesage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMesage).Name = "lblMesage";
		((ControlBase)this.lblMesage).WrapText = false;
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val).Image = ERP.Properties.Resources.KEYBOARDnew;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnSend, "btnSend");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnSend).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnSend).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSend).Name = "btnSend";
		((System.Windows.Forms.Control)(object)this.btnSend).Click += new System.EventHandler(btnSend_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val3).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val4;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val5).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val5).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.lblPort, "lblPort");
		this.lblPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPort).Name = "lblPort";
		((ControlBase)this.lblPort).WrapText = false;
		resources.ApplyResources(this.cboPort, "cboPort");
		((TextEditorControlBase)this.cboPort).AlwaysInEditMode = true;
		this.cboPort.AutoCompleteMode = (AutoCompleteMode)4;
		((System.Windows.Forms.Control)(object)this.cboPort).Name = "cboPort";
		((System.Windows.Forms.Control)(object)this.cboPort).TabStop = false;
		resources.ApplyResources(this.btnConnect, "btnConnect");
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.btnConnect).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnConnect).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnConnect).Name = "btnConnect";
		((System.Windows.Forms.Control)(object)this.btnConnect).Click += new System.EventHandler(btnConnect_Click);
		resources.ApplyResources(this.btnDisconnect, "btnDisconnect");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.btnDisconnect).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnDisconnect).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnDisconnect).Name = "btnDisconnect";
		((System.Windows.Forms.Control)(object)this.btnDisconnect).Click += new System.EventHandler(btnDisconnect_Click);
		resources.ApplyResources(this.txtResult, "txtResult");
		((System.Windows.Forms.Control)(object)this.txtResult).Name = "txtResult";
		this.txtResult.Scrollbars = System.Windows.Forms.ScrollBars.Both;
		resources.ApplyResources(this.ULGData, "ULGData");
		((AppearanceBase)val8).BackColor = System.Drawing.SystemColors.ActiveBorder;
		((AppearanceBase)val8).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val8).BackGradientStyle = (GradientStyle)2;
		((AppearanceBase)val8).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val8, "appearance8");
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).Appearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val9, "appearance9");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.BandLabelAppearance = (AppearanceBase)(object)val9;
		((SpecialBoxBase)((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((AppearanceBase)val10).BackColor = System.Drawing.SystemColors.ControlLightLight;
		((AppearanceBase)val10).BackColor2 = System.Drawing.SystemColors.Control;
		((AppearanceBase)val10).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val10).ForeColor = System.Drawing.SystemColors.GrayText;
		resources.ApplyResources(val10, "appearance10");
		((UltraGridBase)this.ULGData).DisplayLayout.GroupByBox.PromptAppearance = (AppearanceBase)(object)val10;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val11).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val11).ForeColor = System.Drawing.SystemColors.ControlText;
		resources.ApplyResources(val11, "appearance11");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val11;
		((AppearanceBase)val12).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val12).ForeColor = System.Drawing.SystemColors.HighlightText;
		resources.ApplyResources(val12, "appearance12");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val12;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val13).BackColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val13, "appearance13");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val13;
		((AppearanceBase)val14).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val14, "appearance14");
		((AppearanceBase)val14).TextTrimming = (TextTrimming)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val14;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val15).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val15).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val15).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val15).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val15).BorderColor = System.Drawing.SystemColors.Window;
		resources.ApplyResources(val15, "appearance15");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val15;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val16).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val16).BorderColor = System.Drawing.Color.Silver;
		resources.ApplyResources(val16, "appearance16");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val16;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val17).BackColor = System.Drawing.SystemColors.ControlLight;
		resources.ApplyResources(val17, "appearance17");
		((UltraGridBase)this.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val17;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		this.ULGData.CellListSelect += new CellEventHandler(ULGData_CellListSelect);
		this.ULGData.BeforeRowsDeleted += new BeforeRowsDeletedEventHandler(ULGData_BeforeRowsDeleted);
		((System.Windows.Forms.Control)(object)this.ULGData).KeyDown += new System.Windows.Forms.KeyEventHandler(ULGData_KeyDown);
		resources.ApplyResources(this.UGBDetails, "UGBDetails");
		this.UGBDetails.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBDetails).Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		((System.Windows.Forms.Control)(object)this.UGBDetails).Name = "UGBDetails";
		resources.ApplyResources(this.btnClear, "btnClear");
		((AppearanceBase)val18).Image = resources.GetObject("appearance18.Image");
		resources.ApplyResources(val18, "appearance18");
		((ControlBase)this.btnClear).Appearance = (AppearanceBase)(object)val18;
		((ControlBase)this.btnClear).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		resources.ApplyResources(this.prgSMS, "prgSMS");
		this.prgSMS.Name = "prgSMS";
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.prgSMS);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBDetails);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtResult);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDisconnect);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnConnect);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSend);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMesage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMesage);
		base.Name = "frmSMS";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMesage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMesage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSend, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnConnect, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDisconnect, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBDetails, 0);
		base.Controls.SetChildIndex(this.prgSMS, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMesage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtResult).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.UGBDetails).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBDetails).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
