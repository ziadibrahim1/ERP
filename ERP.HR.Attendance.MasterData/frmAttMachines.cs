using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.HR;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.HR.Attendance.MasterData;

public class frmAttMachines : frmGrid
{
	private DataTable dtMachineTypes;

	private ValueList vlMachineTypes = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtMachineNameAr;

	private UltraLabel lblMachinesName;

	private UltraTextEditor txtMachineNameEn;

	private UltraLabel lblMachinesNameEn;

	private UltraLabel lblIP;

	private UltraLabel lblMachineType;

	private UltraLabel lblPort;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtMachineNumber;

	private UltraTextEditor txtIP;

	private UltraTextEditor txtPort;

	public UltraComboEditor cboMachineType;

	public UltraDateTimeEditor dtpLastGet;

	public UltraLabel lblLastGet;

	public frmAttMachines()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "HR_AttMachines";
		IDCol = "MachineID";
	}

	public override void PrepareData()
	{
		dtMachineTypes = AttMachineTypes.Select("-1", "-1", "1", IsFromServer: true);
		vlMachineTypes.ValueListItems.Clear();
		for (int i = 0; i < dtMachineTypes.Rows.Count; i++)
		{
			vlMachineTypes.ValueListItems.Add(dtMachineTypes.Rows[i]["MachineTypeID"], dtMachineTypes.Rows[i]["MachineTypeName"].ToString());
		}
		GlobalFunctions.FillCombo(cboMachineType, dtMachineTypes, "MachineTypeID", "MachineTypeName");
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtMachineNumber).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMachineNameAr).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMachineNameEn).ReadOnly = NavMode;
		((EditorButtonControlBase)txtIP).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPort).ReadOnly = NavMode;
		((EditorButtonControlBase)cboMachineType).ReadOnly = NavMode;
		((EditorButtonControlBase)dtpLastGet).ReadOnly = NavMode;
		((TextEditorControlBase)txtMachineNumber).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtMachineNumber).Text = AttMachines.GetCode(IsFromServer: true);
		((TextEditorControlBase)txtMachineNameAr).Clear();
		((TextEditorControlBase)txtMachineNameEn).Clear();
		((TextEditorControlBase)txtIP).Clear();
		((TextEditorControlBase)cboMachineType).Clear();
		((Control)(object)txtPort).Text = "4370";
		dtpLastGet.DateTime = GlobalFunctions.GetServerDateTimeNow();
	}

	public override void FillData()
	{
		dataTable = AttMachines.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNumper"].Header).Caption = (GlobalVariables.IsArabic ? "الكود" : "");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNumper"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNumper"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "اسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IP"].Header).Caption = "IP";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IP"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IP"].Width = (int)((double)((Control)(object)ULGData).Width * 0.14);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Port"].Header).Caption = "Port";
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Port"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Port"].Width = (int)((double)((Control)(object)ULGData).Width * 0.04);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].Header).Caption = (GlobalVariables.IsArabic ? "النوع" : "Type");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MachineTypeID"].ValueList = (IValueList)(object)vlMachineTypes;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].Header).Caption = (GlobalVariables.IsArabic ? "اخر تحميل" : "Last Load");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.12);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["LastGetDate"].MaskInput = "dd/mm/yyyy hh:mm";
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtMachineNumber).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MachineNumper"].Value.ToString();
		((Control)(object)txtMachineNameAr).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MachineNameAr"].Value.ToString();
		((Control)(object)txtMachineNameEn).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MachineNameEn"].Value.ToString();
		((Control)(object)txtIP).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["IP"].Value.ToString();
		((Control)(object)txtPort).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Port"].Value.ToString();
		((Control)(object)cboMachineType).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MachineTypeID"].Value.ToString();
		dtpLastGet.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["LastGetDate"].Value;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtMachineNameAr).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الإسم بالعربية", "Please Enter Arabic Name");
			((TextEditorControlBase)txtMachineNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtMachineNumber).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الكود", "Please Enter Arabic Name");
			((TextEditorControlBase)txtMachineNameAr).Focus();
			return false;
		}
		if (((Control)(object)txtIP).Text == "")
		{
			GlobalVariables.InformationMB.Show("Please Enter a Valid IP");
			((TextEditorControlBase)txtPort).Focus();
			return false;
		}
		if (((Control)(object)txtPort).Text == "")
		{
			GlobalVariables.InformationMB.Show("Please Enter a Valid Port");
			((TextEditorControlBase)txtPort).Focus();
			return false;
		}
		if (cboMachineType.SelectedIndex == -1)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال نوع الماكينه", "Please Enter Machine Type");
			((TextEditorControlBase)txtMachineNameAr).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).ActiveRow.Index != i && ((Control)(object)txtIP).Text == ((UltraGridBase)ULGData).Rows[i].Cells["IP"].Value.ToString())
			{
				GlobalVariables.InformationMB.Show("تم إدخال نفس \nIP", "you Insert Same IP");
				return false;
			}
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		AttMachines.Insert_Update("-1", ((Control)(object)txtMachineNumber).Text, ((Control)(object)txtMachineNameAr).Text, (((Control)(object)txtMachineNameEn).Text == "") ? "Null" : ((Control)(object)txtMachineNameEn).Text, ((TextEditorControlBase)txtIP).Value.ToString(), ((Control)(object)txtPort).Text, ((TextEditorControlBase)cboMachineType).Value.ToString(), dtpLastGet.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void UpdateData()
	{
		AttMachines.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["MachineID"].Value.ToString(), ((Control)(object)txtMachineNumber).Text, ((Control)(object)txtMachineNameAr).Text, (((Control)(object)txtMachineNameEn).Text == "") ? "Null" : ((Control)(object)txtMachineNameEn).Text, ((TextEditorControlBase)txtIP).Value.ToString(), ((Control)(object)txtPort).Text, ((TextEditorControlBase)cboMachineType).Value.ToString(), dtpLastGet.DateTime.ToString(GlobalVariables.DateLongFormate), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		AttMachines.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["MachineID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
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
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Attendance.MasterData.frmAttMachines));
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
		this.txtMachineNameAr = new UltraTextEditor();
		this.lblMachinesName = new UltraLabel();
		this.txtMachineNameEn = new UltraTextEditor();
		this.lblMachinesNameEn = new UltraLabel();
		this.lblIP = new UltraLabel();
		this.lblMachineType = new UltraLabel();
		this.lblPort = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.txtMachineNumber = new UltraTextEditor();
		this.txtIP = new UltraTextEditor();
		this.txtPort = new UltraTextEditor();
		this.cboMachineType = new UltraComboEditor();
		this.dtpLastGet = new UltraDateTimeEditor();
		this.lblLastGet = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMachineNameAr).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMachineNameEn).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMachineNumber).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtIP).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPort).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboMachineType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpLastGet).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.ULGData, "ULGData");
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
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
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
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
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((AppearanceBase)val8).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString");
		((AppearanceBase)val8).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString");
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.ItalicAsString = resources.GetString("resource.ItalicAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).FontData.StrikeoutAsString = resources.GetString("resource.StrikeoutAsString1");
		((AppearanceBase)val9).FontData.UnderlineAsString = resources.GetString("resource.UnderlineAsString1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val9, "appearance9");
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtMachineNameAr, "txtMachineNameAr");
		((System.Windows.Forms.Control)(object)this.txtMachineNameAr).Name = "txtMachineNameAr";
		resources.ApplyResources(this.lblMachinesName, "lblMachinesName");
		this.lblMachinesName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMachinesName).Name = "lblMachinesName";
		((ControlBase)this.lblMachinesName).WrapText = false;
		resources.ApplyResources(this.txtMachineNameEn, "txtMachineNameEn");
		((System.Windows.Forms.Control)(object)this.txtMachineNameEn).Name = "txtMachineNameEn";
		resources.ApplyResources(this.lblMachinesNameEn, "lblMachinesNameEn");
		this.lblMachinesNameEn.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMachinesNameEn).Name = "lblMachinesNameEn";
		((ControlBase)this.lblMachinesNameEn).WrapText = false;
		resources.ApplyResources(this.lblIP, "lblIP");
		this.lblIP.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblIP).Name = "lblIP";
		((ControlBase)this.lblIP).WrapText = false;
		resources.ApplyResources(this.lblMachineType, "lblMachineType");
		this.lblMachineType.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMachineType).Name = "lblMachineType";
		((ControlBase)this.lblMachineType).WrapText = false;
		resources.ApplyResources(this.lblPort, "lblPort");
		this.lblPort.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPort).Name = "lblPort";
		((ControlBase)this.lblPort).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtMachineNumber, "txtMachineNumber");
		((System.Windows.Forms.Control)(object)this.txtMachineNumber).Name = "txtMachineNumber";
		resources.ApplyResources(this.txtIP, "txtIP");
		((System.Windows.Forms.Control)(object)this.txtIP).Name = "txtIP";
		resources.ApplyResources(this.txtPort, "txtPort");
		((System.Windows.Forms.Control)(object)this.txtPort).Name = "txtPort";
		((System.Windows.Forms.Control)(object)this.txtPort).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPort_KeyPress);
		resources.ApplyResources(this.cboMachineType, "cboMachineType");
		this.cboMachineType.AutoCompleteMode = (AutoCompleteMode)2;
		((System.Windows.Forms.Control)(object)this.cboMachineType).Name = "cboMachineType";
		((TextEditorControlBase)this.cboMachineType).Nullable = false;
		resources.ApplyResources(this.dtpLastGet, "dtpLastGet");
		this.dtpLastGet.MaskInput = "{date} {time}";
		((System.Windows.Forms.Control)(object)this.dtpLastGet).Name = "dtpLastGet";
		this.dtpLastGet.PromptChar = ' ';
		resources.ApplyResources(this.lblLastGet, "lblLastGet");
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.lblLastGet).Appearance = (AppearanceBase)(object)val10;
		this.lblLastGet.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblLastGet).Name = "lblLastGet";
		((ControlBase)this.lblLastGet).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpLastGet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLastGet);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboMachineType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMachineType);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblIP);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMachineNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMachinesNameEn);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPort);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtIP);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMachineNumber);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMachineNameAr);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMachinesName);
		base.Name = "frmAttMachines";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMachinesName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMachineNameAr, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMachineNumber, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtIP, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMachinesNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMachineNameEn, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblIP, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMachineType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPort, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboMachineType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblLastGet, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpLastGet, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMachineNameAr).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMachineNameEn).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMachineNumber).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtIP).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPort).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboMachineType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpLastGet).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
