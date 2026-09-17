using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmPaymentMethods : frmGrid
{
	private IContainer components = null;

	private UltraLabel lblPaymentMethodEnglishName;

	private UltraTextEditor txtPaymentMethodEnglishName;

	private UltraLabel lblPaymentTime;

	private UltraLabel lblPaymentMethodArabicName;

	private UltraTextEditor txtPaymentMethodArabicName;

	private UltraLabel lblInstallmentsCount;

	private UltraTextEditor txtInstallmentsCount;

	private UltraTextEditor txtPaymentTime;

	public frmPaymentMethods()
	{
		InitializeComponent();
		TableName = "G_PaymentMethods";
		IDCol = "PaymentMethodID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtPaymentMethodArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaymentMethodEnglishName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtPaymentTime).ReadOnly = NavMode;
		((EditorButtonControlBase)txtInstallmentsCount).ReadOnly = NavMode;
		((TextEditorControlBase)txtPaymentMethodArabicName).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtPaymentMethodArabicName).Clear();
		((TextEditorControlBase)txtPaymentMethodEnglishName).Clear();
		((TextEditorControlBase)txtPaymentTime).Clear();
		((TextEditorControlBase)txtInstallmentsCount).Clear();
	}

	public override void FillData()
	{
		dataTable = PaymentMethods.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالعربية" : "Name Ar");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "الإسم بالإنجليزية" : "Name En");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentMethodNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.3);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentTime"].Header).Caption = (GlobalVariables.IsArabic ? "مدة الدفعة" : "Payment Time");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentTime"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["PaymentTime"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentsCount"].Header).Caption = (GlobalVariables.IsArabic ? "عدد الدفعات" : "Installments Count");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentsCount"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["InstallmentsCount"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtPaymentMethodArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["PaymentMethodNameAr"].Value.ToString();
		((Control)(object)txtPaymentMethodEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["PaymentMethodNameEn"].Value.ToString();
		((TextEditorControlBase)txtPaymentTime).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["PaymentTime"].Value;
		((Control)(object)txtInstallmentsCount).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["InstallmentsCount"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtPaymentMethodArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال إسم طريقة الدفع بالعربية", "Please Insert Payment Method Arabic Name");
			((TextEditorControlBase)txtPaymentMethodArabicName).Focus();
			return false;
		}
		if (((Control)(object)txtPaymentTime).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال مدة الدفعة ", "Please Insert Payment Time");
			((TextEditorControlBase)txtPaymentTime).Focus();
			return false;
		}
		if (((Control)(object)txtInstallmentsCount).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الدفعات ", "Please Insert Installments Count");
			((TextEditorControlBase)txtInstallmentsCount).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		PaymentMethods.Insert_Update("-1", ((Control)(object)txtPaymentMethodArabicName).Text, (((Control)(object)txtPaymentMethodEnglishName).Text == "") ? "Null" : ((Control)(object)txtPaymentMethodEnglishName).Text, (((Control)(object)txtPaymentTime).Text == "") ? "0" : ((Control)(object)txtPaymentTime).Text, (((Control)(object)txtInstallmentsCount).Text == "") ? "0" : ((Control)(object)txtInstallmentsCount).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		PaymentMethods.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["PaymentMethodID"].Value.ToString(), ((Control)(object)txtPaymentMethodArabicName).Text, (((Control)(object)txtPaymentMethodEnglishName).Text == "") ? "Null" : ((Control)(object)txtPaymentMethodEnglishName).Text, (((Control)(object)txtPaymentTime).Text == "") ? "0" : ((Control)(object)txtPaymentTime).Text, (((Control)(object)txtInstallmentsCount).Text == "") ? "0" : ((Control)(object)txtInstallmentsCount).Text, ((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString().Equals("True") ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		PaymentMethods.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["PaymentMethodID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmPaymentMethods));
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
		this.lblPaymentMethodEnglishName = new UltraLabel();
		this.txtPaymentMethodEnglishName = new UltraTextEditor();
		this.lblPaymentTime = new UltraLabel();
		this.lblPaymentMethodArabicName = new UltraLabel();
		this.txtPaymentMethodArabicName = new UltraTextEditor();
		this.lblInstallmentsCount = new UltraLabel();
		this.txtInstallmentsCount = new UltraTextEditor();
		this.txtPaymentTime = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentMethodEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentMethodArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentsCount).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentTime).BeginInit();
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
		resources.ApplyResources(this.lblPaymentMethodEnglishName, "lblPaymentMethodEnglishName");
		this.lblPaymentMethodEnglishName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaymentMethodEnglishName).Name = "lblPaymentMethodEnglishName";
		((ControlBase)this.lblPaymentMethodEnglishName).WrapText = false;
		resources.ApplyResources(this.txtPaymentMethodEnglishName, "txtPaymentMethodEnglishName");
		resources.ApplyResources(val10, "appearance10");
		((TextEditorControlBase)this.txtPaymentMethodEnglishName).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.txtPaymentMethodEnglishName).Name = "txtPaymentMethodEnglishName";
		resources.ApplyResources(this.lblPaymentTime, "lblPaymentTime");
		this.lblPaymentTime.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaymentTime).Name = "lblPaymentTime";
		((ControlBase)this.lblPaymentTime).WrapText = false;
		resources.ApplyResources(this.lblPaymentMethodArabicName, "lblPaymentMethodArabicName");
		this.lblPaymentMethodArabicName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPaymentMethodArabicName).Name = "lblPaymentMethodArabicName";
		((ControlBase)this.lblPaymentMethodArabicName).WrapText = false;
		resources.ApplyResources(this.txtPaymentMethodArabicName, "txtPaymentMethodArabicName");
		((System.Windows.Forms.Control)(object)this.txtPaymentMethodArabicName).Name = "txtPaymentMethodArabicName";
		resources.ApplyResources(this.lblInstallmentsCount, "lblInstallmentsCount");
		this.lblInstallmentsCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblInstallmentsCount).Name = "lblInstallmentsCount";
		((ControlBase)this.lblInstallmentsCount).WrapText = false;
		resources.ApplyResources(this.txtInstallmentsCount, "txtInstallmentsCount");
		((System.Windows.Forms.Control)(object)this.txtInstallmentsCount).Name = "txtInstallmentsCount";
		((System.Windows.Forms.Control)(object)this.txtInstallmentsCount).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		resources.ApplyResources(this.txtPaymentTime, "txtPaymentTime");
		((System.Windows.Forms.Control)(object)this.txtPaymentTime).Name = "txtPaymentTime";
		((System.Windows.Forms.Control)(object)this.txtPaymentTime).KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaymentTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblInstallmentsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtInstallmentsCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentMethodEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaymentMethodEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentTime);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPaymentMethodArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPaymentMethodArabicName);
		base.Name = "frmPaymentMethods";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaymentMethodArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentMethodArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentTime, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaymentMethodEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPaymentMethodEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtInstallmentsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblInstallmentsCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPaymentTime, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentMethodEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentMethodArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtInstallmentsCount).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPaymentTime).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
