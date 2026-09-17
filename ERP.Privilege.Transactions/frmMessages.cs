using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Privilege.Transactions;

public class frmMessages : frmGrid
{
	private DataTable dtUsers;

	private ValueList vlUsers = new ValueList();

	private IContainer components = null;

	private UltraTextEditor txtHeader;

	private UltraLabel lblHeader;

	private UltraLabel lblBody;

	private UltraCheckEditor chkIsPublic;

	protected internal CheckedListBox clbUsers;

	private UltraLabel ultraLabel1;

	public UltraComboEditor cboUsers;

	private UltraPanel pnlCheckType;

	private RadioButton rbSentItems;

	private RadioButton rbAll;

	private RadioButton rbInbox;

	private RichTextBox txtBody;

	internal ToolStrip tbrEditor;

	internal ToolStripButton tbrOpen;

	internal ToolStripButton tbrSave;

	internal ToolStripSeparator ToolStripSeparator1;

	internal ToolStripButton tbrFont;

	private ToolStripButton tspColor;

	internal ToolStripSeparator ToolStripSeparator4;

	internal ToolStripButton tbrLeft;

	internal ToolStripButton tbrCenter;

	internal ToolStripButton tbrRight;

	internal ToolStripSeparator ToolStripSeparator2;

	internal ToolStripButton tbrBold;

	internal ToolStripButton tbrItalic;

	internal ToolStripButton tbrUnderline;

	internal ToolStripSeparator ToolStripSeparator3;

	internal ColorDialog ColorDialog1;

	internal PrintDialog PrintDialog1;

	internal PrintDocument PrintDocument1;

	internal PageSetupDialog PageSetupDialog1;

	internal OpenFileDialog OpenFileDialog1;

	internal SaveFileDialog SaveFileDialog1;

	internal FontDialog FontDialog1;

	internal PrintPreviewDialog PrintPreviewDialog1;

	public frmMessages()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		InitializeComponent();
		TableName = "G_Messages";
		IDCol = "MessageID";
	}

	public override void PrepareData()
	{
		base.PrepareData();
		dtUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: false);
		GlobalFunctions.FillCombo(cboUsers, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		Main.Fillclb(clbUsers, dtUsers, "User_ID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
		vlUsers.ValueListItems.Clear();
		for (int i = 0; i < dtUsers.Rows.Count; i++)
		{
			vlUsers.ValueListItems.Add(dtUsers.Rows[i]["User_ID"], dtUsers.Rows[i][GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn"].ToString());
		}
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnUpdate).Visible = false;
		((Control)(object)btnDelete).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((Control)(object)chkIsPublic).Enabled = !NavMode;
		((EditorButtonControlBase)txtHeader).ReadOnly = NavMode;
		txtBody.ReadOnly = NavMode;
		((TextEditorControlBase)txtHeader).Focus();
	}

	public override void ClearControls()
	{
		((TextEditorControlBase)txtHeader).Clear();
		txtBody.Clear();
		((TextEditorControlBase)cboUsers).Value = GlobalVariables.UserID;
		((UltraToggleEditorBase)chkIsPublic).Checked = false;
		for (int i = 0; i < clbUsers.Items.Count; i++)
		{
			clbUsers.SetItemChecked(i, value: false);
		}
	}

	public override void FillData()
	{
		dataTable = Messages.SelectByUserID((rbAll.Checked && GlobalVariables.GroupID == "1") ? "-1" : GlobalVariables.UserID, (rbAll.Checked || rbInbox.Checked) ? "1" : "0", (rbAll.Checked || rbSentItems.Checked) ? "1" : "0", GlobalVariables.IsArabic ? "1" : "0", IsFromServer: true);
		((UltraGridBase)ULGData).DataSource = dataTable;
		InitGrid();
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromUserID"].Header).Caption = (GlobalVariables.IsArabic ? "المرسل" : "Sender");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromUserID"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromUserID"].Width = (int)((double)((Control)(object)ULGData).Width * 0.25);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["FromUserID"].ValueList = (IValueList)(object)vlUsers;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendDate"].Header).Caption = (GlobalVariables.IsArabic ? "التاريخ" : "Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["SendDate"].MaskInput = "mm/dd/yyyy hh:mm";
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageHeader"].Header).Caption = (GlobalVariables.IsArabic ? " العنوان" : "Header");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageHeader"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["MessageHeader"].Width = (int)((double)((Control)(object)ULGData).Width * 0.6) - GlobalVariables.ScrollWidth;
	}

	public override void AfterRowActivate()
	{
		if (Adding)
		{
			return;
		}
		((TextEditorControlBase)cboUsers).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["FromUserID"].Value.ToString();
		((Control)(object)txtHeader).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["MessageHeader"].Value.ToString();
		txtBody.Rtf = ((UltraGridBase)ULGData).ActiveRow.Cells["MessageText"].Value.ToString();
		((UltraToggleEditorBase)chkIsPublic).Checked = Convert.ToBoolean(((UltraGridBase)ULGData).ActiveRow.Cells["IsPublic"].Value);
		if (((UltraToggleEditorBase)chkIsPublic).Checked)
		{
			for (int i = 0; i < clbUsers.Items.Count; i++)
			{
				clbUsers.SetItemChecked(i, value: true);
			}
			return;
		}
		DataTable dataTable = MessagesRecipient.SelectByMessageID(((UltraGridBase)ULGData).ActiveRow.Cells["MessageID"].Value.ToString(), "1", IsFromServer: true);
		for (int j = 0; j < clbUsers.Items.Count; j++)
		{
			clbUsers.SetItemChecked(j, dataTable.Select("ToUserID=" + dtUsers.Rows[j]["User_ID"]).Length != 0);
		}
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtHeader).Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال عنوان الرساله", "Please Enter Message Header");
			return false;
		}
		if (txtBody.Text == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال محتوي الرساله", "Please Enter Message Body");
			return false;
		}
		if (clbUsers.CheckedItems.Count == 0)
		{
			GlobalVariables.InformationMB.Show("برجاء إختيار المستخدمين", "Please Select Users");
			return false;
		}
		return base.ValidateData();
	}

	public override void AddData()
	{
		int num = Messages.Insert_Update("-1", GlobalVariables.UserID, ((Control)(object)txtHeader).Text, (txtBody.Rtf == "") ? "Null" : txtBody.Rtf, ((UltraToggleEditorBase)chkIsPublic).Checked ? "1" : "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: true);
		if (!((UltraToggleEditorBase)chkIsPublic).Checked)
		{
			MessagesRecipient.Insert_ByUserIDs(num.ToString(), GetUsers(), GlobalVariables.CurrentBranchID, IsFromServer: true);
		}
	}

	public string GetUsers()
	{
		string text = ",";
		for (int i = 0; i < clbUsers.Items.Count; i++)
		{
			if (clbUsers.GetItemChecked(i))
			{
				text = text + dtUsers.Rows[i]["User_ID"].ToString() + ",";
			}
		}
		return text;
	}

	public override void UpdateData()
	{
	}

	public override void DeleteData()
	{
	}

	private void clbUsers_SelectedIndexChanged(object sender, EventArgs e)
	{
		((UltraToggleEditorBase)chkIsPublic).CheckedChanged -= chkIsPublic_CheckedChanged;
		((UltraToggleEditorBase)chkIsPublic).Checked = clbUsers.CheckedItems.Count == clbUsers.Items.Count && clbUsers.Items.Count > 0;
		((UltraToggleEditorBase)chkIsPublic).CheckedChanged += chkIsPublic_CheckedChanged;
	}

	private void chkIsPublic_CheckedChanged(object sender, EventArgs e)
	{
		for (int i = 0; i < clbUsers.Items.Count; i++)
		{
			clbUsers.SetItemChecked(i, ((UltraToggleEditorBase)chkIsPublic).Checked);
		}
	}

	private void rbAll_CheckedChanged(object sender, EventArgs e)
	{
		FillData();
	}

	private void tbrSave_Click(object sender, EventArgs e)
	{
		try
		{
			SaveFileDialog1.Title = "RTE - Save File";
			SaveFileDialog1.DefaultExt = "rtf";
			SaveFileDialog1.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
			SaveFileDialog1.FilterIndex = 1;
			if (SaveFileDialog1.ShowDialog() == DialogResult.OK && !(SaveFileDialog1.FileName == ""))
			{
				string extension = Path.GetExtension(SaveFileDialog1.FileName);
				extension = extension.ToUpper();
				if (extension == ".RTF")
				{
					txtBody.SaveFile(SaveFileDialog1.FileName, RichTextBoxStreamType.RichText);
				}
				else
				{
					StreamWriter streamWriter = new StreamWriter(SaveFileDialog1.FileName);
					streamWriter.Write(txtBody.Text);
					streamWriter.Close();
					streamWriter = null;
					txtBody.SelectionStart = 0;
					txtBody.SelectionLength = 0;
				}
				string fileName = SaveFileDialog1.FileName;
				txtBody.Modified = false;
				txtBody.SaveFile(fileName, RichTextBoxStreamType.RichText);
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void tbrOpen_Click(object sender, EventArgs e)
	{
		try
		{
			OpenFile();
			txtBody.Modified = true;
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void OpenFile()
	{
		try
		{
			OpenFileDialog1.Title = "RTE - Open File";
			OpenFileDialog1.DefaultExt = "rtf";
			OpenFileDialog1.Filter = "Rich Text Files|*.rtf|All Files|*.*";
			OpenFileDialog1.FilterIndex = 1;
			OpenFileDialog1.FileName = string.Empty;
			txtBody.Modified = true;
			if (OpenFileDialog1.ShowDialog() == DialogResult.OK && !(OpenFileDialog1.FileName == ""))
			{
				string extension = Path.GetExtension(OpenFileDialog1.FileName);
				extension = extension.ToUpper();
				if (extension == ".RTF")
				{
					txtBody.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText);
					txtBody.Modified = true;
				}
				else
				{
					StreamReader streamReader = new StreamReader(OpenFileDialog1.FileName);
					txtBody.Text = streamReader.ReadToEnd();
					streamReader.Close();
					streamReader = null;
					txtBody.SelectionStart = 0;
					txtBody.SelectionLength = 0;
				}
				string fileName = OpenFileDialog1.FileName;
				txtBody.Modified = true;
				Text = "Editor: " + fileName.ToString();
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void tbrFont_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtBody.SelectionFont != null)
			{
				FontDialog1.Font = txtBody.SelectionFont;
			}
			else
			{
				FontDialog1.Font = null;
			}
			FontDialog1.ShowApply = true;
			if (FontDialog1.ShowDialog() == DialogResult.OK)
			{
				txtBody.SelectionFont = FontDialog1.Font;
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void tspColor_Click(object sender, EventArgs e)
	{
		try
		{
			ColorDialog1.Color = txtBody.ForeColor;
			if (ColorDialog1.ShowDialog() == DialogResult.OK)
			{
				txtBody.SelectionColor = ColorDialog1.Color;
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void tbrLeft_Click(object sender, EventArgs e)
	{
		if (tbrLeft.Checked)
		{
			ToolStripButton toolStripButton = tbrCenter;
			bool flag = (tbrRight.Checked = false);
			toolStripButton.Checked = flag;
			txtBody.SelectionAlignment = HorizontalAlignment.Left;
		}
	}

	private void tbrCenter_Click(object sender, EventArgs e)
	{
		if (tbrCenter.Checked)
		{
			ToolStripButton toolStripButton = tbrRight;
			bool flag = (tbrLeft.Checked = false);
			toolStripButton.Checked = flag;
			txtBody.SelectionAlignment = HorizontalAlignment.Center;
		}
	}

	private void tbrRight_Click(object sender, EventArgs e)
	{
		if (tbrRight.Checked)
		{
			ToolStripButton toolStripButton = tbrCenter;
			bool flag = (tbrLeft.Checked = false);
			toolStripButton.Checked = flag;
			txtBody.SelectionAlignment = HorizontalAlignment.Right;
		}
	}

	private void tbrBold_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtBody.SelectionFont != null)
			{
				Font selectionFont = txtBody.SelectionFont;
				FontStyle style = txtBody.SelectionFont.Style ^ FontStyle.Bold;
				txtBody.SelectionFont = new Font(selectionFont.FontFamily, selectionFont.Size, style);
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void tbrItalic_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtBody.SelectionFont != null)
			{
				Font selectionFont = txtBody.SelectionFont;
				FontStyle style = txtBody.SelectionFont.Style ^ FontStyle.Italic;
				txtBody.SelectionFont = new Font(selectionFont.FontFamily, selectionFont.Size, style);
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void tbrUnderline_Click(object sender, EventArgs e)
	{
		try
		{
			if (txtBody.SelectionFont != null)
			{
				Font selectionFont = txtBody.SelectionFont;
				FontStyle style = txtBody.SelectionFont.Style ^ FontStyle.Underline;
				txtBody.SelectionFont = new Font(selectionFont.FontFamily, selectionFont.Size, style);
			}
		}
		catch (Exception ex)
		{
			GlobalVariables.InformationMB.Show(ex.ToString(), ex.ToString());
		}
	}

	private void txtBody_SelectionChanged(object sender, EventArgs e)
	{
		tbrLeft.Checked = txtBody.SelectionAlignment == HorizontalAlignment.Left;
		tbrCenter.Checked = txtBody.SelectionAlignment == HorizontalAlignment.Center;
		tbrRight.Checked = txtBody.SelectionAlignment == HorizontalAlignment.Right;
		if (txtBody.SelectionFont != null)
		{
			tbrBold.Checked = txtBody.SelectionFont.Bold;
			tbrItalic.Checked = txtBody.SelectionFont.Italic;
			tbrUnderline.Checked = txtBody.SelectionFont.Underline;
		}
		else
		{
			ToolStripButton toolStripButton = tbrBold;
			ToolStripButton toolStripButton2 = tbrItalic;
			bool flag = (tbrUnderline.Checked = false);
			bool flag3 = (toolStripButton2.Checked = flag);
			toolStripButton.Checked = flag3;
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
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Privilege.Transactions.frmMessages));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtHeader = new UltraTextEditor();
		this.lblHeader = new UltraLabel();
		this.lblBody = new UltraLabel();
		this.chkIsPublic = new UltraCheckEditor();
		this.clbUsers = new System.Windows.Forms.CheckedListBox();
		this.ultraLabel1 = new UltraLabel();
		this.cboUsers = new UltraComboEditor();
		this.pnlCheckType = new UltraPanel();
		this.rbSentItems = new System.Windows.Forms.RadioButton();
		this.rbAll = new System.Windows.Forms.RadioButton();
		this.rbInbox = new System.Windows.Forms.RadioButton();
		this.txtBody = new System.Windows.Forms.RichTextBox();
		this.tbrEditor = new System.Windows.Forms.ToolStrip();
		this.tbrOpen = new System.Windows.Forms.ToolStripButton();
		this.tbrSave = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.tbrFont = new System.Windows.Forms.ToolStripButton();
		this.tspColor = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
		this.tbrLeft = new System.Windows.Forms.ToolStripButton();
		this.tbrCenter = new System.Windows.Forms.ToolStripButton();
		this.tbrRight = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.tbrBold = new System.Windows.Forms.ToolStripButton();
		this.tbrItalic = new System.Windows.Forms.ToolStripButton();
		this.tbrUnderline = new System.Windows.Forms.ToolStripButton();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
		this.PrintDialog1 = new System.Windows.Forms.PrintDialog();
		this.PrintDocument1 = new System.Drawing.Printing.PrintDocument();
		this.PageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
		this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
		this.FontDialog1 = new System.Windows.Forms.FontDialog();
		this.PrintPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtHeader).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPublic).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).SuspendLayout();
		this.tbrEditor.SuspendLayout();
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
		resources.ApplyResources(base.btnAdd, "btnAdd");
		resources.ApplyResources(base.btnRefreshData, "btnRefreshData");
		((AppearanceBase)val8).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val8).FontData.Name = resources.GetString("resource.Name");
		((ControlBase)base.lblHistory).Appearance = (AppearanceBase)(object)val8;
		resources.ApplyResources(base.lblHistory, "lblHistory");
		resources.ApplyResources(this.txtHeader, "txtHeader");
		((System.Windows.Forms.Control)(object)this.txtHeader).Name = "txtHeader";
		this.lblHeader.AutoEllipsis = false;
		resources.ApplyResources(this.lblHeader, "lblHeader");
		((System.Windows.Forms.Control)(object)this.lblHeader).Name = "lblHeader";
		((ControlBase)this.lblHeader).WrapText = false;
		this.lblBody.AutoEllipsis = false;
		resources.ApplyResources(this.lblBody, "lblBody");
		((System.Windows.Forms.Control)(object)this.lblBody).Name = "lblBody";
		((ControlBase)this.lblBody).WrapText = false;
		resources.ApplyResources(this.chkIsPublic, "chkIsPublic");
		((System.Windows.Forms.Control)(object)this.chkIsPublic).Name = "chkIsPublic";
		((UltraToggleEditorBase)this.chkIsPublic).CheckedChanged += new System.EventHandler(chkIsPublic_CheckedChanged);
		this.clbUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.clbUsers.CheckOnClick = true;
		resources.ApplyResources(this.clbUsers, "clbUsers");
		this.clbUsers.MultiColumn = true;
		this.clbUsers.Name = "clbUsers";
		this.clbUsers.SelectedIndexChanged += new System.EventHandler(clbUsers_SelectedIndexChanged);
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.cboUsers.AutoCompleteMode = (AutoCompleteMode)2;
		resources.ApplyResources(this.cboUsers, "cboUsers");
		((System.Windows.Forms.Control)(object)this.cboUsers).Name = "cboUsers";
		((TextEditorControlBase)this.cboUsers).Nullable = false;
		((EditorButtonControlBase)this.cboUsers).ReadOnly = true;
		resources.ApplyResources(this.pnlCheckType, "pnlCheckType");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.Transparent;
		this.pnlCheckType.Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbSentItems);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbAll);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).Controls.Add(this.rbInbox);
		((System.Windows.Forms.Control)(object)this.pnlCheckType).Name = "pnlCheckType";
		resources.ApplyResources(this.rbSentItems, "rbSentItems");
		this.rbSentItems.BackColor = System.Drawing.Color.Transparent;
		this.rbSentItems.Name = "rbSentItems";
		this.rbSentItems.UseVisualStyleBackColor = false;
		this.rbSentItems.CheckedChanged += new System.EventHandler(rbAll_CheckedChanged);
		resources.ApplyResources(this.rbAll, "rbAll");
		this.rbAll.BackColor = System.Drawing.Color.Transparent;
		this.rbAll.Checked = true;
		this.rbAll.Name = "rbAll";
		this.rbAll.TabStop = true;
		this.rbAll.UseVisualStyleBackColor = false;
		this.rbAll.CheckedChanged += new System.EventHandler(rbAll_CheckedChanged);
		resources.ApplyResources(this.rbInbox, "rbInbox");
		this.rbInbox.BackColor = System.Drawing.Color.Transparent;
		this.rbInbox.Name = "rbInbox";
		this.rbInbox.UseVisualStyleBackColor = false;
		this.rbInbox.CheckedChanged += new System.EventHandler(rbAll_CheckedChanged);
		resources.ApplyResources(this.txtBody, "txtBody");
		this.txtBody.Name = "txtBody";
		this.txtBody.SelectionChanged += new System.EventHandler(txtBody_SelectionChanged);
		resources.ApplyResources(this.tbrEditor, "tbrEditor");
		this.tbrEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.tbrOpen, this.tbrSave, this.ToolStripSeparator1, this.tbrFont, this.tspColor, this.ToolStripSeparator4, this.tbrLeft, this.tbrCenter, this.tbrRight, this.ToolStripSeparator2,
			this.tbrBold, this.tbrItalic, this.tbrUnderline, this.ToolStripSeparator3
		});
		this.tbrEditor.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.tbrEditor.Name = "tbrEditor";
		this.tbrEditor.Stretch = true;
		this.tbrOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrOpen, "tbrOpen");
		this.tbrOpen.Name = "tbrOpen";
		this.tbrOpen.Click += new System.EventHandler(tbrOpen_Click);
		this.tbrSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrSave, "tbrSave");
		this.tbrSave.Name = "tbrSave";
		this.tbrSave.Click += new System.EventHandler(tbrSave_Click);
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		resources.ApplyResources(this.ToolStripSeparator1, "ToolStripSeparator1");
		this.tbrFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrFont, "tbrFont");
		this.tbrFont.Name = "tbrFont";
		this.tbrFont.Click += new System.EventHandler(tbrFont_Click);
		this.tspColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tspColor, "tspColor");
		this.tspColor.Name = "tspColor";
		this.tspColor.Click += new System.EventHandler(tspColor_Click);
		this.ToolStripSeparator4.Name = "ToolStripSeparator4";
		resources.ApplyResources(this.ToolStripSeparator4, "ToolStripSeparator4");
		this.tbrLeft.CheckOnClick = true;
		this.tbrLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrLeft, "tbrLeft");
		this.tbrLeft.Name = "tbrLeft";
		this.tbrLeft.Click += new System.EventHandler(tbrLeft_Click);
		this.tbrCenter.CheckOnClick = true;
		this.tbrCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrCenter, "tbrCenter");
		this.tbrCenter.Name = "tbrCenter";
		this.tbrCenter.Click += new System.EventHandler(tbrCenter_Click);
		this.tbrRight.CheckOnClick = true;
		this.tbrRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrRight, "tbrRight");
		this.tbrRight.Name = "tbrRight";
		this.tbrRight.Click += new System.EventHandler(tbrRight_Click);
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		resources.ApplyResources(this.ToolStripSeparator2, "ToolStripSeparator2");
		this.tbrBold.CheckOnClick = true;
		this.tbrBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrBold, "tbrBold");
		this.tbrBold.Name = "tbrBold";
		this.tbrBold.Click += new System.EventHandler(tbrBold_Click);
		this.tbrItalic.CheckOnClick = true;
		this.tbrItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrItalic, "tbrItalic");
		this.tbrItalic.Name = "tbrItalic";
		this.tbrItalic.Click += new System.EventHandler(tbrItalic_Click);
		this.tbrUnderline.CheckOnClick = true;
		this.tbrUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tbrUnderline, "tbrUnderline");
		this.tbrUnderline.Name = "tbrUnderline";
		this.tbrUnderline.Click += new System.EventHandler(tbrUnderline_Click);
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		resources.ApplyResources(this.ToolStripSeparator3, "ToolStripSeparator3");
		this.PrintDialog1.UseEXDialog = true;
		this.OpenFileDialog1.FileName = "OpenFileDialog1";
		resources.ApplyResources(this.PrintPreviewDialog1, "PrintPreviewDialog1");
		this.PrintPreviewDialog1.Name = "PrintPreviewDialog1";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.tbrEditor);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboUsers);
		base.Controls.Add(this.clbUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsPublic);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBody);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtHeader);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblHeader);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlCheckType);
		base.Controls.Add(this.txtBody);
		base.Name = "frmMessages";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex(this.txtBody, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlCheckType, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtHeader, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblBody, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsPublic, 0);
		base.Controls.SetChildIndex(this.clbUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboUsers, 0);
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
		base.Controls.SetChildIndex(this.tbrEditor, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtHeader).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsPublic).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboUsers).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.pnlCheckType.ClientArea).PerformLayout();
		((System.Windows.Forms.Control)(object)this.pnlCheckType).ResumeLayout(false);
		this.tbrEditor.ResumeLayout(false);
		this.tbrEditor.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
