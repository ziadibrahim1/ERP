using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.CRM;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.CRM.MasterData;

public class frmQuestions : frmGrid
{
	private IContainer components = null;

	private UltraTextEditor txtArabicName;

	private UltraLabel lblArabicName;

	private UltraTextEditor txtEnglishName;

	private UltraLabel lblEnglishName;

	private UltraCheckEditor chkIsBoolean;

	private UltraTextEditor txtQuestionCode;

	private UltraLabel lblCode;

	public frmQuestions()
	{
		InitializeComponent();
		TableName = "CRM_Questions";
		IDCol = "QuestionID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((EditorButtonControlBase)txtQuestionCode).ReadOnly = NavMode;
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtArabicName).ReadOnly = NavMode;
		((EditorButtonControlBase)txtEnglishName).ReadOnly = NavMode;
		((Control)(object)chkIsBoolean).Enabled = !NavMode;
		((TextEditorControlBase)txtArabicName).Focus();
	}

	public override void ClearControls()
	{
		((Control)(object)txtQuestionCode).Text = (Adding ? Questions.GetCode(IsFromServer: true) : "");
		((TextEditorControlBase)txtArabicName).Clear();
		((TextEditorControlBase)txtEnglishName).Clear();
		((UltraToggleEditorBase)chkIsBoolean).Checked = false;
	}

	public override void FillData()
	{
		dataTable = Questions.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNo"].Header).Caption = (GlobalVariables.IsArabic ? "كود السؤال" : "Question Code");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNameAr"].Header).Caption = (GlobalVariables.IsArabic ? "صيغة السؤال بالعربية" : "Question In Arabic");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNameAr"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNameAr"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNameEn"].Header).Caption = (GlobalVariables.IsArabic ? "صيغة السؤال بالإنجليزية" : "Question In English");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNameEn"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["QuestionNameEn"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsBoolean"].Header).Caption = (GlobalVariables.IsArabic ? "صواب أو خطأ" : "True Or False");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsBoolean"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsBoolean"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtQuestionCode).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["QuestionNo"].Value.ToString();
		((Control)(object)txtArabicName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["QuestionNameAr"].Value.ToString();
		((Control)(object)txtEnglishName).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["QuestionNameEn"].Value.ToString();
		((UltraToggleEditorBase)chkIsBoolean).Checked = !((UltraGridBase)ULGData).ActiveRow.Cells["IsBoolean"].Value.Equals(DBNull.Value) && Convert.ToBoolean(((UltraGridBase)ULGData).ActiveRow.Cells["IsBoolean"].Value);
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtQuestionCode).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال كود السؤال", "Please Insert Question No");
			((TextEditorControlBase)txtQuestionCode).Focus();
			return false;
		}
		if (((Control)(object)txtArabicName).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخالصيغة السؤال بالعربية", "Please Enter Question In Arabic");
			((TextEditorControlBase)txtArabicName).Focus();
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		Questions.Insert_Update("-1", ((Control)(object)txtQuestionCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkIsBoolean).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void UpdateData()
	{
		Questions.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["QuestionID"].Value.ToString(), ((Control)(object)txtQuestionCode).Text, ((Control)(object)txtArabicName).Text, (((Control)(object)txtEnglishName).Text == "") ? "Null" : ((Control)(object)txtEnglishName).Text, ((UltraToggleEditorBase)chkIsBoolean).Checked ? "1" : "0", bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
	}

	public override void DeleteData()
	{
		Questions.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["QuestionID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void txtQuestionCode_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
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
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.CRM.MasterData.frmQuestions));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		this.txtArabicName = new UltraTextEditor();
		this.lblArabicName = new UltraLabel();
		this.txtEnglishName = new UltraTextEditor();
		this.lblEnglishName = new UltraLabel();
		this.chkIsBoolean = new UltraCheckEditor();
		this.txtQuestionCode = new UltraTextEditor();
		this.lblCode = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsBoolean).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuestionCode).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		((AppearanceBase)val9).FontData.BoldAsString = resources.GetString("resource.BoldAsString1");
		((AppearanceBase)val9).FontData.Name = resources.GetString("resource.Name1");
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Navy;
		((TextEditorControlBase)base.cboTransactionBranch).Appearance = (AppearanceBase)(object)val9;
		base.cboTransactionBranch.DropDownButtonAlignment = (ButtonAlignment)1;
		base.cboTransactionBranch.DropDownListAlignment = (DropDownListAlignment)2;
		resources.ApplyResources(this.txtArabicName, "txtArabicName");
		((System.Windows.Forms.Control)(object)this.txtArabicName).Name = "txtArabicName";
		this.lblArabicName.AutoEllipsis = false;
		resources.ApplyResources(this.lblArabicName, "lblArabicName");
		((System.Windows.Forms.Control)(object)this.lblArabicName).Name = "lblArabicName";
		((ControlBase)this.lblArabicName).WrapText = false;
		resources.ApplyResources(this.txtEnglishName, "txtEnglishName");
		((System.Windows.Forms.Control)(object)this.txtEnglishName).Name = "txtEnglishName";
		this.lblEnglishName.AutoEllipsis = false;
		resources.ApplyResources(this.lblEnglishName, "lblEnglishName");
		((System.Windows.Forms.Control)(object)this.lblEnglishName).Name = "lblEnglishName";
		((ControlBase)this.lblEnglishName).WrapText = false;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Navy;
		((UltraToggleEditorBase)this.chkIsBoolean).Appearance = (AppearanceBase)(object)val10;
		resources.ApplyResources(this.chkIsBoolean, "chkIsBoolean");
		((System.Windows.Forms.Control)(object)this.chkIsBoolean).Name = "chkIsBoolean";
		resources.ApplyResources(this.txtQuestionCode, "txtQuestionCode");
		((System.Windows.Forms.Control)(object)this.txtQuestionCode).Name = "txtQuestionCode";
		((System.Windows.Forms.Control)(object)this.txtQuestionCode).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtQuestionCode_KeyPress);
		this.lblCode.AutoEllipsis = false;
		resources.ApplyResources(this.lblCode, "lblCode");
		((System.Windows.Forms.Control)(object)this.lblCode).Name = "lblCode";
		((ControlBase)this.lblCode).WrapText = false;
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtQuestionCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCode);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsBoolean);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblEnglishName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtArabicName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArabicName);
		base.Name = "frmQuestions";
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtArabicName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtEnglishName, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsBoolean, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCode, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtQuestionCode, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtArabicName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtEnglishName).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsBoolean).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtQuestionCode).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
