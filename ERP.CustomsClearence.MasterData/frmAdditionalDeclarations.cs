using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.CustomsClearence;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CustomsClearence.MasterData;

public class frmAdditionalDeclarations : frmGrid
{
	private IContainer components = null;

	private UltraLabel lblCustomsTaarifNo;

	private UltraTextEditor txtDescription;

	private UltraTextEditor txtAdditionalDeclarationCode;

	private UltraLabel lblAdditionalDeclarationCode;

	public frmAdditionalDeclarations()
	{
		InitializeComponent();
		TableName = "CST_AdditionalDeclarations";
		IDCol = "AdditionalDeclarationID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtAdditionalDeclarationCode).ReadOnly = NavMode;
		((EditorButtonControlBase)txtDescription).ReadOnly = NavMode;
		((TextEditorControlBase)txtDescription).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtAdditionalDeclarationCode).Text = (Adding ? AdditionalDeclarations.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtDescription).Clear();
	}

	public override void FillData()
	{
		dataTable = AdditionalDeclarations.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalDeclarationCode"].Header).Caption = (GlobalVariables.IsArabic ? " كود الإقرار الإضافي" : "Additional Declaration Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalDeclarationCode"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["AdditionalDeclarationCode"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Header).Caption = (GlobalVariables.IsArabic ? "الوصف" : "Description");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Description"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtAdditionalDeclarationCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalDeclarationCode"].Value.ToString();
		((Control)(object)txtDescription).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Description"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtAdditionalDeclarationCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود الإقرار الإضافي", "Please Enter The Additional Declaration Code");
			((TextEditorControlBase)txtAdditionalDeclarationCode).Focus();
			return false;
		}
		if (Main.CheckForValue("CST_AdditionalDeclarations", "AdditionalDeclarationCode", ((Control)(object)txtAdditionalDeclarationCode).Text, Adding ? AdditionalDeclarations.GetCode(IsFromServer: true) : "", IsFromServer: true) > 0)
		{
			string text = (Adding ? AdditionalDeclarations.GetCode(IsFromServer: true) : "");
			GlobalVariables.QuestionMB.Show("رقم هذا الإقرار متواجد من قبل \n سوف يتم الحفظ برقم " + text, "The Additional Declaration Number Already Exists It Will Be Saved With No. : " + text);
			if (GlobalVariables.MessageBoxResult != 'Y')
			{
				((TextEditorControlBase)txtAdditionalDeclarationCode).Focus();
				return false;
			}
			((Control)(object)txtAdditionalDeclarationCode).Text = text;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		AdditionalDeclarations.Insert_Update("-1", ((Control)(object)txtAdditionalDeclarationCode).Text, ((Control)(object)txtDescription).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		AdditionalDeclarations.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalDeclarationID"].Value.ToString(), ((Control)(object)txtAdditionalDeclarationCode).Text, ((Control)(object)txtDescription).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		AdditionalDeclarations.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["AdditionalDeclarationID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CustomsClearence.MasterData.frmAdditionalDeclarations));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblCustomsTaarifNo = new UltraLabel();
		this.txtDescription = new UltraTextEditor();
		this.txtAdditionalDeclarationCode = new UltraTextEditor();
		this.lblAdditionalDeclarationCode = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdditionalDeclarationCode).BeginInit();
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
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblCustomsTaarifNo, "lblCustomsTaarifNo");
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val8, "appearance9");
		((ControlBase)this.lblCustomsTaarifNo).Appearance = (AppearanceBase)(object)val8;
		this.lblCustomsTaarifNo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo).Name = "lblCustomsTaarifNo";
		((ControlBase)this.lblCustomsTaarifNo).WrapText = false;
		resources.ApplyResources(this.txtDescription, "txtDescription");
		((System.Windows.Forms.Control)(object)this.txtDescription).Name = "txtDescription";
		resources.ApplyResources(this.txtAdditionalDeclarationCode, "txtAdditionalDeclarationCode");
		((System.Windows.Forms.Control)(object)this.txtAdditionalDeclarationCode).Name = "txtAdditionalDeclarationCode";
		resources.ApplyResources(this.lblAdditionalDeclarationCode, "lblAdditionalDeclarationCode");
		this.lblAdditionalDeclarationCode.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAdditionalDeclarationCode).Name = "lblAdditionalDeclarationCode";
		((ControlBase)this.lblAdditionalDeclarationCode).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtDescription);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtAdditionalDeclarationCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAdditionalDeclarationCode);
		base.Name = "frmAdditionalDeclarations";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAdditionalDeclarationCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtAdditionalDeclarationCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtDescription, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCustomsTaarifNo, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtDescription).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtAdditionalDeclarationCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
