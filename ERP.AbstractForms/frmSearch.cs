using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.Classes;
using ERP.Properties;
using ERP.SystemOptions.GeneralData;
using ERP.Ticketing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.AbstractForms;

public class frmSearch : frmBase
{
	public int ID = 0;

	private int top;

	private UltraLabel[] l = (UltraLabel[])(object)new UltraLabel[0];

	private UltraTextEditor[] t = (UltraTextEditor[])(object)new UltraTextEditor[0];

	private UltraComboEditor[] cbo = (UltraComboEditor[])(object)new UltraComboEditor[0];

	private UltraCheckEditor[] chkTrue = (UltraCheckEditor[])(object)new UltraCheckEditor[0];

	private UltraCheckEditor[] chkFalse = (UltraCheckEditor[])(object)new UltraCheckEditor[0];

	private UltraDateTimeEditor[] dtpFrom = (UltraDateTimeEditor[])(object)new UltraDateTimeEditor[0];

	private UltraDateTimeEditor[] dtpTo = (UltraDateTimeEditor[])(object)new UltraDateTimeEditor[0];

	public DataTable dtSource = new DataTable();

	public string IDColumn = "";

	public string frmName = "";

	public DataTable dtResult = new DataTable();

	public DataView dv;

	private DataTable dtSelectedColumns;

	public DateTime MinDate = new DateTime(2000, 1, 1);

	public DateTime MaxDate = new DateTime(2100, 1, 1);

	private IContainer components = null;

	protected internal UltraButton btnCancel;

	protected internal UltraButton btnOK;

	protected internal Label lblTitle;

	public UltraGrid ULGData;

	private UltraLabel lblCounter;

	private UltraLabel lblCounterResult;

	public UltraButton btnKeyboard;

	public UltraCheckEditor chkSearchMultiFilter;

	public UltraButton btnEditColumns;

	public UltraPanel pnlControls;

	public UltraButton btnOpenTicket;

	public frmSearch()
	{
		InitializeComponent();
		((Control)(object)chkSearchMultiFilter).Text = (GlobalVariables.IsArabic ? "البحث بفلاتر متعددة" : "Search MultiFilter");
		((Control)(object)lblCounter).Text = (GlobalVariables.IsArabic ? "العدد" : "Counter");
		frmName = GetType().ToString();
	}

	private void frmSearch_Load(object sender, EventArgs e)
	{
		dv = new DataView(dtSource);
		((UltraGridBase)ULGData).DataSource = dv;
		InitGrid();
		((UltraGridBase)ULGData).UpdateData();
		if (!base.DesignMode)
		{
			LoadSearchObjects();
		}
		base.AcceptButton = (IButtonControl)btnOK;
	}

	public virtual void InitGrid()
	{
		GlobalFunctions.PrepareGrid(ULGData);
		((Control)(object)lblCounterResult).Text = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count.ToString();
	}

	public virtual void LoadSearchObjects()
	{
		top = 0;
		dtSelectedColumns = SearchColumns.SelectByFormName(GlobalVariables.UserID, frmName, IsFromServer: false);
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count;
		if (dtSelectedColumns.Rows.Count > 0)
		{
			for (int i = 0; i < dtSelectedColumns.Rows.Count; i++)
			{
				CreateSearchObject(dtSelectedColumns.Rows[i]["ColKey"].ToString());
			}
			return;
		}
		if (GlobalVariables.IsArabic)
		{
			for (int num = count - 1; num > -1; num--)
			{
				if (!((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num].Hidden && ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num]).Key != "Choose")
				{
					CreateSearchObject(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num]).Key);
				}
			}
			return;
		}
		for (int j = 0; j < count; j++)
		{
			if (!((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[j].Hidden && ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[j]).Key != "Choose")
			{
				CreateSearchObject(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[j]).Key);
			}
		}
	}

	public void CreateSearchObject(string ColumnName)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0abc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac2: Expected O, but got Unknown
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Expected O, but got Unknown
		//IL_0de0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Expected O, but got Unknown
		//IL_0c02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c08: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Expected O, but got Unknown
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Expected O, but got Unknown
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ce: Expected O, but got Unknown
		if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].DataType == typeof(bool))
		{
			Array.Resize(ref chkTrue, chkTrue.Length + 1);
			chkTrue[chkTrue.Length - 1] = new UltraCheckEditor();
			((UltraToggleEditorBase)chkTrue[chkTrue.Length - 1]).Appearance.TextHAlign = (HAlign)2;
			((Control)(object)chkTrue[chkTrue.Length - 1]).Top = top;
			((Control)(object)chkTrue[chkTrue.Length - 1]).Size = new Size(200, 20);
			((Control)(object)chkTrue[chkTrue.Length - 1]).Name = "chk" + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)chkTrue[chkTrue.Length - 1]).Tag = ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)chkTrue[chkTrue.Length - 1]).Text = ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].Header).Caption;
			((UltraToggleEditorBase)chkTrue[chkTrue.Length - 1]).CheckAlign = (GlobalVariables.IsArabic ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft);
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)chkTrue[chkTrue.Length - 1]);
			top += ((Control)(object)chkTrue[chkTrue.Length - 1]).Height;
			((UltraToggleEditorBase)chkTrue[chkTrue.Length - 1]).CheckedChanged += Control_ValueChanged;
			Array.Resize(ref chkFalse, chkFalse.Length + 1);
			chkFalse[chkFalse.Length - 1] = new UltraCheckEditor();
			((UltraToggleEditorBase)chkFalse[chkFalse.Length - 1]).Appearance.TextHAlign = (HAlign)2;
			((Control)(object)chkFalse[chkFalse.Length - 1]).Top = top;
			((Control)(object)chkFalse[chkFalse.Length - 1]).Size = new Size(200, 20);
			((Control)(object)chkFalse[chkFalse.Length - 1]).Name = "chkNot" + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)chkFalse[chkFalse.Length - 1]).Tag = ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)chkFalse[chkFalse.Length - 1]).Text = (GlobalVariables.IsArabic ? " غير " : " Not ") + ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].Header).Caption;
			((UltraToggleEditorBase)chkFalse[chkFalse.Length - 1]).CheckAlign = (GlobalVariables.IsArabic ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft);
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)chkFalse[chkFalse.Length - 1]);
			top += ((Control)(object)chkFalse[chkFalse.Length - 1]).Height;
			((UltraToggleEditorBase)chkFalse[chkFalse.Length - 1]).CheckedChanged += Control_ValueChanged;
			return;
		}
		if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].DataType == typeof(DateTime))
		{
			Array.Resize(ref l, l.Length + 1);
			l[l.Length - 1] = new UltraLabel();
			((ControlBase)l[l.Length - 1]).Appearance.TextHAlign = (HAlign)2;
			((Control)(object)l[l.Length - 1]).Top = top;
			((Control)(object)l[l.Length - 1]).Size = new Size(200, 20);
			((Control)(object)l[l.Length - 1]).Text = (GlobalVariables.IsArabic ? " من " : " From ") + ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].Header).Caption;
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)l[l.Length - 1]);
			top += ((Control)(object)l[l.Length - 1]).Height;
			Array.Resize(ref dtpFrom, dtpFrom.Length + 1);
			dtpFrom[dtpFrom.Length - 1] = new UltraDateTimeEditor();
			dtpFrom[dtpFrom.Length - 1].Appearance.TextHAlign = (HAlign)2;
			((Control)(object)dtpFrom[dtpFrom.Length - 1]).Top = top;
			((Control)(object)dtpFrom[dtpFrom.Length - 1]).Size = new Size(200, 20);
			((Control)(object)dtpFrom[dtpFrom.Length - 1]).Name = "dtpFrom" + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)dtpFrom[dtpFrom.Length - 1]).Tag = ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			dtpFrom[dtpFrom.Length - 1].MaskInput = "dd/mm/yyyy";
			dtpFrom[dtpFrom.Length - 1].Value = DBNull.Value;
			dtpFrom[dtpFrom.Length - 1].MinDate = MinDate;
			dtpFrom[dtpFrom.Length - 1].MaxDate = MaxDate;
			dtpFrom[dtpFrom.Length - 1].ValueChanged += Control_ValueChanged;
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)dtpFrom[dtpFrom.Length - 1]);
			top += ((Control)(object)dtpFrom[dtpFrom.Length - 1]).Height;
			Array.Resize(ref l, l.Length + 1);
			l[l.Length - 1] = new UltraLabel();
			((ControlBase)l[l.Length - 1]).Appearance.TextHAlign = (HAlign)2;
			((Control)(object)l[l.Length - 1]).Top = top;
			((Control)(object)l[l.Length - 1]).Size = new Size(200, 20);
			((Control)(object)l[l.Length - 1]).Text = (GlobalVariables.IsArabic ? " الى " : " To ") + ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].Header).Caption;
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)l[l.Length - 1]);
			top += ((Control)(object)l[l.Length - 1]).Height;
			Array.Resize(ref dtpTo, dtpTo.Length + 1);
			dtpTo[dtpTo.Length - 1] = new UltraDateTimeEditor();
			dtpTo[dtpTo.Length - 1].Appearance.TextHAlign = (HAlign)2;
			((Control)(object)dtpTo[dtpTo.Length - 1]).Top = top;
			((Control)(object)dtpTo[dtpTo.Length - 1]).Size = new Size(200, 20);
			((Control)(object)dtpTo[dtpTo.Length - 1]).Name = "dtpTo" + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)dtpTo[dtpTo.Length - 1]).Tag = ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			dtpTo[dtpTo.Length - 1].MaskInput = "dd/mm/yyyy";
			dtpTo[dtpTo.Length - 1].Value = DBNull.Value;
			dtpTo[dtpTo.Length - 1].MinDate = MinDate;
			dtpTo[dtpTo.Length - 1].MaxDate = MaxDate;
			dtpTo[dtpTo.Length - 1].ValueChanged += Control_ValueChanged;
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)dtpTo[dtpTo.Length - 1]);
			top += ((Control)(object)dtpTo[dtpTo.Length - 1]).Height;
			return;
		}
		Array.Resize(ref l, l.Length + 1);
		l[l.Length - 1] = new UltraLabel();
		((ControlBase)l[l.Length - 1]).Appearance.TextHAlign = (HAlign)2;
		((Control)(object)l[l.Length - 1]).Top = top;
		((Control)(object)l[l.Length - 1]).Size = new Size(200, 20);
		((Control)(object)l[l.Length - 1]).Text = ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].Header).Caption;
		((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)l[l.Length - 1]);
		top += ((Control)(object)l[l.Length - 1]).Height;
		if (((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Tag != null)
		{
			Array.Resize(ref cbo, cbo.Length + 1);
			cbo[cbo.Length - 1] = new UltraComboEditor();
			cbo[cbo.Length - 1].AutoCompleteMode = (AutoCompleteMode)2;
			((TextEditorControlBase)cbo[cbo.Length - 1]).Appearance.TextHAlign = (HAlign)2;
			((Control)(object)cbo[cbo.Length - 1]).Top = top;
			((Control)(object)cbo[cbo.Length - 1]).Size = new Size(200, 20);
			((Control)(object)cbo[cbo.Length - 1]).Name = "txt" + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			((Control)(object)cbo[cbo.Length - 1]).Tag = ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
			GlobalFunctions.FillCombo(cbo[cbo.Length - 1], (DataTable)((SubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Tag, ColumnName, ColumnName);
			((TextEditorControlBase)cbo[cbo.Length - 1]).ValueChanged += Control_ValueChanged;
			((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)cbo[cbo.Length - 1]);
			top += ((Control)(object)cbo[cbo.Length - 1]).Height;
			return;
		}
		Array.Resize(ref t, t.Length + 1);
		t[t.Length - 1] = new UltraTextEditor();
		((TextEditorControlBase)t[t.Length - 1]).Appearance.TextHAlign = (HAlign)2;
		((Control)(object)t[t.Length - 1]).Top = top;
		((Control)(object)t[t.Length - 1]).Size = new Size(200, 20);
		((Control)(object)t[t.Length - 1]).Name = "txt" + ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
		((Control)(object)t[t.Length - 1]).Tag = ((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName]).Key;
		((TextEditorControlBase)t[t.Length - 1]).ValueChanged += Control_ValueChanged;
		if (((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[ColumnName].DataType == typeof(decimal))
		{
			((Control)(object)t[t.Length - 1]).KeyPress += TextBox_KeyPress;
		}
		((Control)(object)pnlControls.ClientArea).Controls.Add((Control)(object)t[t.Length - 1]);
		top += ((Control)(object)t[t.Length - 1]).Height;
	}

	public virtual void TextBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		GlobalFunctions.CheckForNumbers(sender, e);
	}

	public virtual void Control_ValueChanged(object sender, EventArgs e)
	{
		if (!base.Disposing)
		{
			((UltraGridBase)ULGData).UpdateData();
			string text = "1=1 ";
			text += MultiFilterString();
			((UltraGridBase)ULGData).UpdateData();
			FilterDataSource(text);
		}
	}

	public virtual void FilterDataSource(string Filter)
	{
		dv.RowFilter = Filter;
		dv.RowStateFilter = DataViewRowState.CurrentRows;
		dtResult = dv.ToTable();
		((UltraGridBase)ULGData).DataSource = dv;
		InitGrid();
		((Control)(object)btnOK).Enabled = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).Rows).Count > 0;
	}

	public virtual string MultiFilterString()
	{
		string text = "";
		for (int i = 0; i < t.Length; i++)
		{
			if (((Control)(object)t[i]).Text.Trim() != "")
			{
				string text2 = ((Control)(object)t[i]).Tag.ToString();
				text = ((!(((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[text2].DataType == typeof(string))) ? (text + " and " + text2 + " = " + ((Control)(object)t[i]).Text.Trim()) : (text + " And " + text2 + " Like '%" + ((Control)(object)t[i]).Text.Replace("*", "[*]").Trim() + "%'"));
			}
		}
		for (int j = 0; j < cbo.Length; j++)
		{
			if (cbo[j].SelectedIndex != -1 && ((TextEditorControlBase)cbo[j]).Value.ToString() != "")
			{
				text = string.Concat(text, " and ", ((Control)(object)cbo[j]).Tag.ToString(), " = '", ((TextEditorControlBase)cbo[j]).Value, "'");
			}
			else if (((TextEditorControlBase)cbo[j]).Value != null && ((TextEditorControlBase)cbo[j]).Value.ToString() != "")
			{
				text = text + " And " + ((Control)(object)cbo[j]).Tag.ToString() + " Like '%" + ((TextEditorControlBase)cbo[j]).Value.ToString().Replace("*", "[*]").Trim() + "%'";
			}
		}
		for (int k = 0; k < chkTrue.Length; k++)
		{
			string text3 = ((Control)(object)chkTrue[k]).Tag.ToString();
			if (((UltraToggleEditorBase)chkTrue[k]).Checked && !((UltraToggleEditorBase)chkFalse[k]).Checked)
			{
				text = text + " And " + text3 + "=1 ";
			}
			else if (((UltraToggleEditorBase)chkFalse[k]).Checked && !((UltraToggleEditorBase)chkTrue[k]).Checked)
			{
				text = text + " And " + text3 + "=0 ";
			}
		}
		for (int l = 0; l < dtpFrom.Length; l++)
		{
			string text4 = ((Control)(object)dtpFrom[l]).Tag.ToString();
			if (dtpFrom[l].Value != null)
			{
				text = string.Concat(text, " And ", text4, " >= '", dtpFrom[l].DateTime.Date, "'");
			}
			if (dtpTo[l].Value != null)
			{
				text = string.Concat(text, " and ", text4, " <='", dtpTo[l].DateTime.Date.AddDays(1.0).AddSeconds(-1.0), "'");
			}
		}
		return text;
	}

	public virtual void SelectFullRow(object sender, EventArgs e)
	{
		((GridItemBase)((UltraGridBase)ULGData).ActiveRow).Selected = true;
	}

	public virtual void SelectRow(object sender, EventArgs e)
	{
		if (((UltraGridBase)ULGData).ActiveRow != null)
		{
			ID = int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells[IDColumn].Value.ToString());
		}
		Close();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		dtResult.Rows.Clear();
		Close();
	}

	private void btnKeyboard_Click(object sender, EventArgs e)
	{
		Process process = new Process();
		process.StartInfo.FileName = Environment.SystemDirectory + "\\osk.exe";
		process.Start();
	}

	private void btnEditColumns_Click(object sender, EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ColKey");
		dataTable.Columns.Add("ColName");
		int count = ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns).Count;
		if (GlobalVariables.IsArabic)
		{
			for (int num = count - 1; num > -1; num--)
			{
				if (!(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num]).Key == "Choose") && !(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num]).Key == IDColumn))
				{
					dataTable.Rows.Add(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num]).Key, ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[num].Header).Caption);
				}
			}
		}
		else
		{
			for (int i = 0; i < count; i++)
			{
				if (!(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key == "Choose") && !(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key == IDColumn))
				{
					dataTable.Rows.Add(((KeyedSubObjectBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i]).Key, ((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns[i].Header).Caption);
				}
			}
		}
		frmSearchColumns frmSearchColumns2 = new frmSearchColumns(dataTable, dtSelectedColumns, frmName);
		frmSearchColumns2.ShowDialog();
		if (frmSearchColumns2.HasChanges)
		{
			((Control)(object)pnlControls.ClientArea).Controls.Clear();
			l = (UltraLabel[])(object)new UltraLabel[0];
			t = (UltraTextEditor[])(object)new UltraTextEditor[0];
			chkTrue = (UltraCheckEditor[])(object)new UltraCheckEditor[0];
			chkFalse = (UltraCheckEditor[])(object)new UltraCheckEditor[0];
			dtpFrom = (UltraDateTimeEditor[])(object)new UltraDateTimeEditor[0];
			dtpTo = (UltraDateTimeEditor[])(object)new UltraDateTimeEditor[0];
			LoadSearchObjects();
			FilterDataSource("1=1" + MultiFilterString());
		}
	}

	private void btnOpenTicket_Click(object sender, EventArgs e)
	{
		try
		{
			Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
			frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(isError: false, isMessage: false, isFormQst: true, base.Name, "Question on form : " + base.Name, bitmap);
			frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
			((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
			frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
			frmSupportingTickets2.ShowDialog();
		}
		catch
		{
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
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmSearch));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.pnlControls = new UltraPanel();
		this.ULGData = new UltraGrid();
		this.btnCancel = new UltraButton();
		this.btnOK = new UltraButton();
		this.lblTitle = new System.Windows.Forms.Label();
		this.chkSearchMultiFilter = new UltraCheckEditor();
		this.lblCounter = new UltraLabel();
		this.lblCounterResult = new UltraLabel();
		this.btnKeyboard = new UltraButton();
		this.btnEditColumns = new UltraButton();
		this.btnOpenTicket = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlControls).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkSearchMultiFilter).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlControls, "pnlControls");
		this.pnlControls.AutoScroll = true;
		((System.Windows.Forms.Control)(object)this.pnlControls).Name = "pnlControls";
		resources.ApplyResources(this.ULGData, "ULGData");
		((UltraGridBase)this.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColMoving = (AllowColMoving)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowColSizing = (AllowColSizing)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowDelete = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.AllowUpdate = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCell = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeCol = (SelectType)1;
		((UltraGridBase)this.ULGData).DisplayLayout.Override.SelectTypeRow = (SelectType)2;
		((UltraGridBase)this.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((System.Windows.Forms.Control)(object)this.ULGData).Name = "ULGData";
		((System.Windows.Forms.Control)(object)this.ULGData).TabStop = false;
		((UltraControlBase)this.ULGData).UseOsThemes = (DefaultableBoolean)2;
		this.ULGData.AfterEnterEditMode += new System.EventHandler(SelectFullRow);
		((System.Windows.Forms.Control)(object)this.ULGData).DoubleClick += new System.EventHandler(SelectRow);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((UltraButtonBase)this.btnCancel).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnOK, "btnOK");
		((UltraButtonBase)this.btnOK).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnOK).Name = "btnOK";
		((System.Windows.Forms.Control)(object)this.btnOK).Click += new System.EventHandler(SelectRow);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		this.lblTitle.BackColor = System.Drawing.Color.Transparent;
		this.lblTitle.ForeColor = System.Drawing.Color.Black;
		this.lblTitle.Name = "lblTitle";
		resources.ApplyResources(this.chkSearchMultiFilter, "chkSearchMultiFilter");
		((System.Windows.Forms.Control)(object)this.chkSearchMultiFilter).Name = "chkSearchMultiFilter";
		resources.ApplyResources(this.lblCounter, "lblCounter");
		this.lblCounter.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounter).Name = "lblCounter";
		resources.ApplyResources(this.lblCounterResult, "lblCounterResult");
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblCounterResult).Appearance = (AppearanceBase)(object)val;
		this.lblCounterResult.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCounterResult).Name = "lblCounterResult";
		resources.ApplyResources(this.btnKeyboard, "btnKeyboard");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.KEYBOARDnew;
		((ControlBase)this.btnKeyboard).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnKeyboard).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Name = "btnKeyboard";
		((System.Windows.Forms.Control)(object)this.btnKeyboard).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnKeyboard).Click += new System.EventHandler(btnKeyboard_Click);
		resources.ApplyResources(this.btnEditColumns, "btnEditColumns");
		((AppearanceBase)val3).Image = ERP.Properties.Resources.Update;
		((ControlBase)this.btnEditColumns).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnEditColumns).ImageSize = new System.Drawing.Size(40, 24);
		((System.Windows.Forms.Control)(object)this.btnEditColumns).Name = "btnEditColumns";
		((System.Windows.Forms.Control)(object)this.btnEditColumns).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnEditColumns).Click += new System.EventHandler(btnEditColumns_Click);
		resources.ApplyResources(this.btnOpenTicket, "btnOpenTicket");
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Name = "btnOpenTicket";
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnOpenTicket).Click += new System.EventHandler(btnOpenTicket_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnOK;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnCancel;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOpenTicket);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlControls);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEditColumns);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnKeyboard);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounterResult);
		base.Controls.Add(this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnOK);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ULGData);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkSearchMultiFilter);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCounter);
		base.Name = "frmSearch";
		base.Load += new System.EventHandler(frmSearch_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkSearchMultiFilter, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex(this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCounterResult, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEditColumns, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.pnlControls, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnOpenTicket, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlControls).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkSearchMultiFilter).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
