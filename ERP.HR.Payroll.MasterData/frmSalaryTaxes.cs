using System.ComponentModel;
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

namespace ERP.HR.Payroll.MasterData;

public class frmSalaryTaxes : frmGrid
{
	private IContainer components = null;

	private UltraTextEditor txtPercentage;

	private UltraLabel lblPercentage;

	private UltraTextEditor txtValueTo;

	private UltraLabel lblValueTo;

	private UltraTextEditor txtValueFrom;

	private UltraLabel lblValueFrom;

	private UltraLabel ultraLabel1;

	private UltraTextEditor txtExemptionPercentage;

	public frmSalaryTaxes()
	{
		InitializeComponent();
		TableName = "HR_SalaryTaxes";
		IDCol = "SalaryTaxID";
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((EditorButtonControlBase)txtPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtExemptionPercentage).ReadOnly = NavMode;
		((EditorButtonControlBase)txtValueTo).ReadOnly = NavMode;
		((EditorButtonControlBase)txtValueFrom).ReadOnly = NavMode;
		((TextEditorControlBase)txtValueFrom).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtPercentage).Clear();
		((TextEditorControlBase)txtExemptionPercentage).Clear();
		((TextEditorControlBase)txtValueTo).Clear();
		((TextEditorControlBase)txtValueFrom).Clear();
	}

	public override void FillData()
	{
		dataTable = SalaryTaxes.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValueFrom"].Header).Caption = (GlobalVariables.IsArabic ? " من مبلغ" : "Value From");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValueFrom"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValueFrom"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValueTo"].Header).Caption = (GlobalVariables.IsArabic ? "الى مبلغ" : "Value To");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValueTo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ValueTo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.4);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Header).Caption = (GlobalVariables.IsArabic ? " النسبة" : "Percentage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["Percentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExemptionPercentage"].Header).Caption = (GlobalVariables.IsArabic ? " نسبة الاعفاء" : "ExemptionPercentage");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExemptionPercentage"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ExemptionPercentage"].Width = (int)((double)((Control)(object)ULGData).Width * 0.2) - GlobalVariables.ScrollWidth;
	}

	public override void AfterRowActivate()
	{
		((Control)(object)txtExemptionPercentage).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ExemptionPercentage"].Value.ToString();
		((Control)(object)txtPercentage).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Percentage"].Value.ToString();
		((Control)(object)txtValueTo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ValueTo"].Value.ToString();
		((Control)(object)txtValueFrom).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ValueFrom"].Value.ToString();
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtValueFrom).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال من مبلغ", "Please Enter From Value");
			return false;
		}
		if (((Control)(object)txtValueTo).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال الى مبلغ", "Please Enter To Value");
			return false;
		}
		if (((Control)(object)txtPercentage).Text == "" || decimal.Parse(((Control)(object)txtPercentage).Text) > 100m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال النسبة من صفر حتى مائة", "Please Enter Percentage From Zero to hundred");
			return false;
		}
		if (((Control)(object)txtExemptionPercentage).Text == "" || decimal.Parse(((Control)(object)txtExemptionPercentage).Text) > 100m)
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال نسبة الإعفاء من صفر حتى مائة", "Please Enter Exemption Percentage From Zero to hundred");
			return false;
		}
		if (Adding)
		{
			for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; i++)
			{
				if ((decimal.Parse(((Control)(object)txtValueFrom).Text) >= decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ValueFrom"].Value.ToString()) && decimal.Parse(((Control)(object)txtValueFrom).Text) <= decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ValueTo"].Value.ToString())) || (decimal.Parse(((Control)(object)txtValueTo).Text) >= decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ValueFrom"].Value.ToString()) && decimal.Parse(((Control)(object)txtValueTo).Text) <= decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ValueTo"].Value.ToString())) || (decimal.Parse(((Control)(object)txtValueFrom).Text) <= decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ValueFrom"].Value.ToString()) && decimal.Parse(((Control)(object)txtValueTo).Text) >= decimal.Parse(((UltraGridBase)ULGData).Rows[i].Cells["ValueTo"].Value.ToString())))
				{
					GlobalVariables.InformationMB.Show("هذه  القيمة واقع فى فترة من قبل", "this Value in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[i]).Selected = true;
					return false;
				}
			}
		}
		else if (Updating)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count; j++)
			{
				if (((UltraGridBase)ULGData).ActiveRow.Cells["SalaryTaxID"].Value.ToString() != ((UltraGridBase)ULGData).Rows[j].Cells["SalaryTaxID"].Value.ToString() && ((decimal.Parse(((Control)(object)txtValueFrom).Text) >= decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ValueFrom"].Value.ToString()) && decimal.Parse(((Control)(object)txtValueFrom).Text) <= decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ValueTo"].Value.ToString())) || (decimal.Parse(((Control)(object)txtValueTo).Text) >= decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ValueFrom"].Value.ToString()) && decimal.Parse(((Control)(object)txtValueTo).Text) <= decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ValueTo"].Value.ToString())) || (decimal.Parse(((Control)(object)txtValueFrom).Text) <= decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ValueFrom"].Value.ToString()) && decimal.Parse(((Control)(object)txtValueTo).Text) >= decimal.Parse(((UltraGridBase)ULGData).Rows[j].Cells["ValueTo"].Value.ToString()))))
				{
					GlobalVariables.InformationMB.Show("هذه  القيمة واقع فى فترة من قبل", "this Value in Another Period");
					((GridItemBase)((UltraGridBase)ULGData).Rows[j]).Selected = true;
					return false;
				}
			}
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		SalaryTaxes.Insert_Update("-1", ((Control)(object)txtValueFrom).Text, ((Control)(object)txtValueTo).Text, ((Control)(object)txtPercentage).Text, ((Control)(object)txtExemptionPercentage).Text, "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void UpdateData()
	{
		SalaryTaxes.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["SalaryTaxID"].Value.ToString(), ((Control)(object)txtValueFrom).Text, ((Control)(object)txtValueTo).Text, ((Control)(object)txtPercentage).Text, ((Control)(object)txtExemptionPercentage).Text, bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString()) ? "1" : "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		FillData();
	}

	public override void DeleteData()
	{
		SalaryTaxes.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["SalaryTaxID"].Value.ToString(), GlobalVariables.UserID, IsFromServer: true);
	}

	private void txt_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.HR.Payroll.MasterData.frmSalaryTaxes));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.txtPercentage = new UltraTextEditor();
		this.lblPercentage = new UltraLabel();
		this.txtValueTo = new UltraTextEditor();
		this.lblValueTo = new UltraLabel();
		this.txtValueFrom = new UltraTextEditor();
		this.lblValueFrom = new UltraLabel();
		this.ultraLabel1 = new UltraLabel();
		this.txtExemptionPercentage = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtPercentage).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValueTo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtValueFrom).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtExemptionPercentage).BeginInit();
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
		resources.ApplyResources(val8, "appearance9");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(base.btnOpenTicket, "btnOpenTicket");
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.txtPercentage, "txtPercentage");
		((System.Windows.Forms.Control)(object)this.txtPercentage).Name = "txtPercentage";
		((System.Windows.Forms.Control)(object)this.txtPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblPercentage, "lblPercentage");
		this.lblPercentage.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblPercentage).Name = "lblPercentage";
		((ControlBase)this.lblPercentage).WrapText = false;
		resources.ApplyResources(this.txtValueTo, "txtValueTo");
		((System.Windows.Forms.Control)(object)this.txtValueTo).Name = "txtValueTo";
		((System.Windows.Forms.Control)(object)this.txtValueTo).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblValueTo, "lblValueTo");
		this.lblValueTo.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValueTo).Name = "lblValueTo";
		((ControlBase)this.lblValueTo).WrapText = false;
		resources.ApplyResources(this.txtValueFrom, "txtValueFrom");
		((System.Windows.Forms.Control)(object)this.txtValueFrom).Name = "txtValueFrom";
		((System.Windows.Forms.Control)(object)this.txtValueFrom).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		resources.ApplyResources(this.lblValueFrom, "lblValueFrom");
		this.lblValueFrom.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblValueFrom).Name = "lblValueFrom";
		((ControlBase)this.lblValueFrom).WrapText = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.txtExemptionPercentage, "txtExemptionPercentage");
		((System.Windows.Forms.Control)(object)this.txtExemptionPercentage).Name = "txtExemptionPercentage";
		((System.Windows.Forms.Control)(object)this.txtExemptionPercentage).KeyPress += new System.Windows.Forms.KeyPressEventHandler(txt_KeyPress);
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtValueFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValueFrom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtValueTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblValueTo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtExemptionPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtPercentage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblPercentage);
		base.Name = "frmSalaryTaxes";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
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
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtExemptionPercentage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValueTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtValueTo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblValueFrom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtValueFrom, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtPercentage).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValueTo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtValueFrom).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtExemptionPercentage).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
