using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.General;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmDiscountSettings : frmGrid
{
	private IContainer components = null;

	private UltraLabel lblDiscountPercentage;

	private UltraTextEditor txtDiscountPercentage;

	private UltraLabel lblToDays;

	private UltraTextEditor txtToDays;

	private UltraLabel lblFromDays;

	private UltraTextEditor txtFromDays;

	private UltraLabel ultraLabel3;

	public frmDiscountSettings()
	{
		InitializeComponent();
		TableName = "G_DiscountSettings";
		IDCol = "DiscountSettingID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtFromDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtToDays).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDiscountPercentage).ReadOnly = NavMode;
		((TextEditorControlBase)txtFromDays).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtFromDays).Clear();
		((TextEditorControlBase)txtToDays).Clear();
		((TextEditorControlBase)txtDiscountPercentage).Clear();
	}

	public override void FillData()
	{
		dataTable = DiscountSettings.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		InitGrid();
	}

	public override void InitGrid()
	{
		((UltraGridBase)ULGData).DataSource = dataTable;
		base.InitGrid();
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["DiscountSettingID"].DefaultCellValue = -1;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDays"].Header).Caption = (GlobalVariables.IsArabic ? "من" : " From");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDays"].Header).Caption = (GlobalVariables.IsArabic ? "الى" : "To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDays"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ToDays"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Header).Caption = (GlobalVariables.IsArabic ? "نسبة الخصم" : "Discount Percent");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtFromDays).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["FromDays"].Value.ToString();
		((Control)(object)txtToDays).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ToDays"].Value.ToString();
		((Control)(object)txtDiscountPercentage).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Percentage"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtFromDays).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الايام من ", "Please Insert From Days");
			((TextEditorControlBase)txtFromDays).Focus();
			return false;
		}
		if (((Control)(object)txtToDays).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عدد الايام الى", "Please insert To Days");
			((TextEditorControlBase)txtToDays).Focus();
			return false;
		}
		if (((Control)(object)txtDiscountPercentage).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال نسبة الخصم", "Please insert Discount Percentage");
			((TextEditorControlBase)txtDiscountPercentage).Focus();
			return false;
		}
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
		{
			if (((UltraGridBase)ULGData).Rows[i].Cells["DiscountSettingID"].Value.ToString() != RowID && ((int.Parse(((Control)(object)txtFromDays).Text) >= int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDays"].Value.ToString()) && int.Parse(((Control)(object)txtFromDays).Text) <= int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDays"].Value.ToString())) || (int.Parse(((Control)(object)txtToDays).Text) >= int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDays"].Value.ToString()) && int.Parse(((Control)(object)txtToDays).Text) <= int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDays"].Value.ToString())) || (int.Parse(((Control)(object)txtFromDays).Text) <= int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["FromDays"].Value.ToString()) && int.Parse(((Control)(object)txtToDays).Text) >= int.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ToDays"].Value.ToString()))))
			{
				GlobalVariables.InformationMB.Show("هذه الفترة الزمنية واقعة فى فترة من قبل", "this Time Priod in Another Period");
				((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
				return false;
			}
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		DiscountSettings.Insert_Update("-1", (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, ((Control)(object)txtFromDays).Text, ((Control)(object)txtToDays).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void UpdateData()
	{
		DiscountSettings.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["DiscountSettingID"].Value.ToString(), (((Control)(object)txtDiscountPercentage).Text == "") ? "0" : ((Control)(object)txtDiscountPercentage).Text, ((Control)(object)txtFromDays).Text, ((Control)(object)txtToDays).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void DeleteData()
	{
		DiscountSettings.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["DiscountSettingID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
		ClearControls();
	}

	private void txtDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	private void txtFromDays_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForIntegers(sender, e);
	}

	private void txtToDays_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmDiscountSettings));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.lblDiscountPercentage = new UltraLabel();
		this.txtDiscountPercentage = new UltraTextEditor();
		this.lblToDays = new UltraLabel();
		this.txtToDays = new UltraTextEditor();
		this.lblFromDays = new UltraLabel();
		this.txtFromDays = new UltraTextEditor();
		this.ultraLabel3 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtToDays).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFromDays).BeginInit();
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
		resources.ApplyResources(this.lblDiscountPercentage, "lblDiscountPercentage");
		this.lblDiscountPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblDiscountPercentage).Name = "lblDiscountPercentage";
		((ControlBase)this.lblDiscountPercentage).WrapText = false;
		resources.ApplyResources(this.txtDiscountPercentage, "txtDiscountPercentage");
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).Name = "txtDiscountPercentage";
		((System.Windows.Forms.Control)(object)this.txtDiscountPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDiscountPercentage_KeyPress);
		resources.ApplyResources(this.lblToDays, "lblToDays");
		this.lblToDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDays).Name = "lblToDays";
		((ControlBase)this.lblToDays).WrapText = false;
		resources.ApplyResources(this.txtToDays, "txtToDays");
		((System.Windows.Forms.Control)(object)this.txtToDays).Name = "txtToDays";
		((System.Windows.Forms.Control)(object)this.txtToDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtToDays_KeyPress);
		resources.ApplyResources(this.lblFromDays, "lblFromDays");
		this.lblFromDays.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDays).Name = "lblFromDays";
		((ControlBase)this.lblFromDays).WrapText = false;
		resources.ApplyResources(this.txtFromDays, "txtFromDays");
		((System.Windows.Forms.Control)(object)this.txtFromDays).Name = "txtFromDays";
		((System.Windows.Forms.Control)(object)this.txtFromDays).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFromDays_KeyPress);
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		this.ultraLabel3.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtFromDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtToDays);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblDiscountPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDiscountPercentage);
		base.Name = "frmDiscountSettings";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblDiscountPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtToDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblToDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtFromDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblFromDays, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDiscountPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtToDays).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFromDays).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
