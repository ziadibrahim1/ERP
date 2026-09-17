using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Security;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTree;

namespace ERP.AbstractForms;

public class frmBase : Form
{
	public DataTable dtSearchResult;

	public bool Adding;

	public bool Updating;

	public bool ClosedPeriod;

	public bool ConcurrentEdit;

	private FormState frmstate = new FormState();

	public string NewCode = "";

	public string TableName = "";

	public string IDCol = "";

	public string NoCol = "";

	public string DateCol = "";

	public string RowID = "";

	public DateTime? DisplayDataDate = null;

	public DataTable dtUsersTransactions = new DataTable();

	public bool CanAdd;

	public bool CanUpdate;

	public bool CanDelete;

	public bool CanSearching;

	public bool CanViewJV;

	public bool CanViewCostPrice;

	public bool CanDiscount;

	public bool CanModifyPriceType;

	public bool CanModifyQty;

	public bool CanViewQty;

	public bool CanModifySubAccount;

	public bool CanPrint;

	public bool CanPost;

	public bool CanPermenantDelete;

	public bool CanEditDate;

	public bool EnableSelectJV;

	public bool CanExport;

	public bool CanViewReport;

	public bool CanPrintReport;

	public bool CanDirectPrint;

	public bool Tap1;

	public bool Tap2;

	public bool Tap3;

	public bool Tap4;

	public bool Tap5;

	public bool Tap6;

	public bool Tap7;

	public bool Tap8;

	public bool Tap9;

	public bool CanMultiPrint;

	public bool CanModifyOtherBranch = true;

	public bool CanEditFromServer = true;

	public bool ViewAllBranches;

	public bool ViewAllEmployees;

	public bool CanUpdateProductionBatchNo;

	public bool CanMinimunCharge;

	public bool CanEditValue;

	public bool CanCloseCheck;

	public bool CanDirect;

	public bool CanSplitCheck;

	public bool CanMergeCheck;

	private IContainer components = null;

	public UltraLabel lblTop;

	public UltraLabel lblBottom;

	public UltraLabel lblLeft;

	public UltraLabel lblRight;

	public frmBase()
	{
		InitializeComponent();
		GlobalFunctions.SetFormStyle(this);
		if (base.Controls.Find("txtCode", searchAllChildren: true).Length != 0)
		{
			base.Controls.Find("txtCode", searchAllChildren: true)[0].TabIndex = 0;
		}
	}

	public virtual void ReportDoubleClick(string GroupNamePath, frmReporViwer frmViewer)
	{
	}

	public virtual void SetControlAppearence(Control c)
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Expected O, but got Unknown
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Expected O, but got Unknown
		if (base.DesignMode)
		{
			return;
		}
		foreach (object control2 in c.Controls)
		{
			Control control = control2 as Control;
			if (control2 is UltraGrid)
			{
				control.KeyPress += UltraGrid_KeyPress;
				control.KeyDown += UltraGrid_KeyDown;
			}
			else
			{
				control.KeyUp += Control_KeyUp;
			}
			if (control2 is UltraTextEditor)
			{
				UltraTextEditor val = (UltraTextEditor)control2;
				((TextEditorControlBase)val).Appearance.TextHAlign = (HAlign)2;
				((TextEditorControlBase)val).Appearance.TextVAlign = (VAlign)2;
				((TextEditorControlBase)val).Appearance.FontData.Name = GlobalVariables.Font;
				((TextEditorControlBase)val).Appearance.ForeColor = Color.Navy;
				((TextEditorControlBase)val).Appearance.FontData.Bold = (DefaultableBoolean)1;
				if (!GlobalVariables.IsArabic)
				{
					((Control)(object)val).RightToLeft = RightToLeft.No;
				}
			}
			else if (control2 is UltraFormattedTextEditor)
			{
				UltraFormattedTextEditor val2 = (UltraFormattedTextEditor)control2;
				((UltraFormattedTextEditorBase)val2).Appearance.TextHAlign = (HAlign)2;
				((UltraFormattedTextEditorBase)val2).Appearance.TextVAlign = (VAlign)2;
				((UltraFormattedTextEditorBase)val2).Appearance.BackColor = Color.White;
				((UltraFormattedTextEditorBase)val2).Appearance.BackColor2 = Color.White;
				((UltraFormattedTextEditorBase)val2).Appearance.FontData.Name = GlobalVariables.Font;
				((UltraFormattedTextEditorBase)val2).Appearance.ForeColor = Color.Navy;
				((UltraFormattedTextEditorBase)val2).Appearance.FontData.Bold = (DefaultableBoolean)1;
				if (!GlobalVariables.IsArabic)
				{
					((Control)(object)val2).RightToLeft = RightToLeft.No;
				}
			}
			else if (control2 is UltraTree)
			{
				UltraTree val3 = (UltraTree)control2;
				val3.Appearance.FontData.Name = GlobalVariables.Font;
				val3.Appearance.FontData.Bold = (DefaultableBoolean)1;
			}
			else if (control2 is UltraDateTimeEditor)
			{
				UltraDateTimeEditor val4 = (UltraDateTimeEditor)control2;
				val4.Appearance.TextHAlign = (HAlign)2;
				val4.Appearance.TextVAlign = (VAlign)2;
				val4.MaskInput = "dd/mm/yyyy";
				val4.Appearance.FontData.Name = GlobalVariables.Font;
				val4.Appearance.ForeColor = Color.Navy;
				val4.Appearance.FontData.Bold = (DefaultableBoolean)1;
			}
			else if (control2 is UltraComboEditor)
			{
				UltraComboEditor val5 = (UltraComboEditor)control2;
				val5.DropDownListAlignment = (DropDownListAlignment)2;
				val5.DropDownButtonAlignment = (ButtonAlignment)(!GlobalVariables.IsArabic);
				((TextEditorControlBase)val5).Appearance.FontData.Name = GlobalVariables.Font;
				((TextEditorControlBase)val5).Appearance.ForeColor = Color.Navy;
				((TextEditorControlBase)val5).Appearance.FontData.Bold = (DefaultableBoolean)1;
			}
			else if (control2 is UltraCheckEditor)
			{
				UltraCheckEditor val6 = (UltraCheckEditor)control2;
				((UltraControlBase)val6).UseAppStyling = false;
				((UltraToggleEditorBase)val6).CheckAlign = (GlobalVariables.IsArabic ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft);
				((UltraToggleEditorBase)val6).Appearance.TextHAlign = (HAlign)((!GlobalVariables.IsArabic) ? 1 : 3);
				((UltraToggleEditorBase)val6).Appearance.FontData.SizeInPoints = Font.SizeInPoints;
				((UltraToggleEditorBase)val6).Appearance.BackColor = Color.Transparent;
				((UltraToggleEditorBase)val6).Appearance.ForeColor = Color.Navy;
				((UltraToggleEditorBase)val6).Appearance.FontData.Name = GlobalVariables.Font;
				((UltraToggleEditorBase)val6).Appearance.FontData.Bold = (DefaultableBoolean)1;
			}
			else if (control2 is UltraLabel)
			{
				UltraLabel val7 = (UltraLabel)control2;
				((ControlBase)val7).Appearance.ForeColor = Color.Navy;
				if (((object)val7).Equals((object)lblBottom) || ((object)val7).Equals((object)lblLeft) || ((object)val7).Equals((object)lblRight) || ((object)val7).Equals((object)lblTop))
				{
					((UltraControlBase)val7).UseAppStyling = false;
					((ControlBase)val7).Appearance.BackColor = Color.Navy;
				}
				else if (((Control)(object)val7).Name == "lblTitle" || ((Control)(object)val7).Name == "lblTitle2" || ((Control)(object)val7).Name == "lblCode")
				{
					GlobalFunctions.SetlblTitleStyle(val7);
					((ControlBase)val7).Appearance.FontData.Bold = (DefaultableBoolean)1;
					((ControlBase)val7).Appearance.TextHAlign = (HAlign)2;
					((ControlBase)val7).Appearance.TextVAlign = (VAlign)2;
					if (((Control)(object)val7).Name == "lblCode")
					{
						((ControlBase)val7).Appearance.FontData.SizeInPoints = 10f;
					}
					else
					{
						((ControlBase)val7).Appearance.FontData.SizeInPoints = 14f;
					}
				}
				else
				{
					((ControlBase)val7).Appearance.FontData.Bold = (DefaultableBoolean)1;
					((Control)(object)val7).RightToLeft = RightToLeft.No;
				}
				((ControlBase)val7).Appearance.FontData.Name = GlobalVariables.Font;
			}
			SetControlAppearence(control2 as Control);
		}
	}

	public void Control_KeyUp(object sender, KeyEventArgs e)
	{
		if (!Adding && !Updating)
		{
			CallButtons(e);
		}
		else
		{
			CallRefrashButton(e);
		}
	}

	public void UltraGrid_KeyDown(object sender, KeyEventArgs e)
	{
		UltraGrid val = (UltraGrid)((sender is UltraGrid) ? sender : null);
		if (val.ActiveCell != null)
		{
			try
			{
				if (e.KeyCode == Keys.Left)
				{
					val.PerformAction((UltraGridAction)4);
					e.Handled = true;
				}
				else if (e.KeyCode == Keys.Right)
				{
					val.PerformAction((UltraGridAction)3);
					e.Handled = true;
				}
				else if (e.KeyCode == Keys.Down && val.ActiveCell.ValueList == null && val.ActiveCell.Column.ValueList == null)
				{
					val.PerformAction((UltraGridAction)20);
					e.Handled = true;
				}
				else if (e.KeyCode == Keys.Up && val.ActiveCell.ValueList == null && val.ActiveCell.Column.ValueList == null)
				{
					val.PerformAction((UltraGridAction)19);
					e.Handled = true;
				}
				val.PerformAction((UltraGridAction)39);
				val.PerformAction((UltraGridAction)24);
			}
			catch (Exception)
			{
			}
		}
		if (e.Shift && e.KeyCode == Keys.Return)
		{
			val.PerformAction((UltraGridAction)19);
			val.PerformAction((UltraGridAction)24);
		}
		else if (e.KeyCode == Keys.Return)
		{
			val.PerformAction((UltraGridAction)20);
			val.PerformAction((UltraGridAction)24);
		}
		if (e.KeyValue == 13 || e.KeyCode == Keys.Tab)
		{
			e.Handled = true;
		}
		if (e.Control && e.KeyCode == Keys.Tab)
		{
			SelectNextControl(base.ActiveControl, forward: true, tabStopOnly: true, nested: true, wrap: true);
		}
		if (!Adding && !Updating)
		{
			CallButtons(e);
		}
		else
		{
			CallRefrashButton(e);
		}
	}

	private void UltraGrid_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\t')
		{
			return;
		}
		UltraGrid val = (UltraGrid)((sender is UltraGrid) ? sender : null);
		int num = -1;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)val).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			if (num == -1 && !((UltraGridBase)val).DisplayLayout.Bands[0].Columns[i].Hidden)
			{
				num = i;
				break;
			}
		}
		e.Handled = true;
		if (val.ActiveCell != null && val.ActiveCell.Column.Index != num)
		{
			if (GlobalVariables.IsArabic)
			{
				val.PerformAction((UltraGridAction)43);
			}
			else
			{
				val.PerformAction((UltraGridAction)42);
			}
		}
		else if (GlobalVariables.IsArabic && val.ActiveCell != null && val.ActiveCell.Column.Index == num)
		{
			val.PerformAction((UltraGridAction)8);
			val.PerformAction((UltraGridAction)42);
			val.PerformAction((UltraGridAction)8);
			val.PerformAction((UltraGridAction)24);
		}
		else if (val.ActiveCell == null)
		{
			SendKeys.Send("{tab}");
		}
	}

	public virtual void PrepareData()
	{
	}

	public virtual void PrepareData2()
	{
	}

	public virtual void CallButtons(KeyEventArgs e)
	{
	}

	public virtual void CallRefrashButton(KeyEventArgs e)
	{
	}

	private void frmBase_Load(object sender, EventArgs e)
	{
		if (!base.DesignMode)
		{
			Font = new Font(GlobalVariables.Font, Font.SizeInPoints);
			if (GlobalVariables.dtForms != null)
			{
				DataRow dataRow = null;
				if (base.Tag != null)
				{
					dataRow = (DataRow)base.Tag;
				}
				else if (GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'").Length != 0)
				{
					dataRow = (DataRow)(base.Tag = GlobalVariables.dtForms.Select("FormFullName = '" + GetType().Namespace + "." + GetType().Name + "'")[0]);
				}
				if (dataRow != null)
				{
					string formID = dataRow["FormID"].ToString();
					CanAdd = GlobalFunctions.GetFormFunction(formID, "Adding");
					CanUpdate = GlobalFunctions.GetFormFunction(formID, "Updating");
					CanDelete = GlobalFunctions.GetFormFunction(formID, "Deleting");
					CanPost = GlobalFunctions.GetFormFunction(formID, "Posting");
					CanEditDate = GlobalFunctions.GetFormFunction(formID, "EditDate");
					CanSearching = GlobalFunctions.GetFormFunction(formID, "Searching");
					CanViewCostPrice = GlobalFunctions.GetFormFunction(formID, "ViewCostPrice");
					CanDiscount = GlobalFunctions.GetFormFunction(formID, "Discount");
					CanModifyPriceType = GlobalFunctions.GetFormFunction(formID, "ModifyPriceType");
					CanModifyQty = GlobalFunctions.GetFormFunction(formID, "ModifyQty");
					CanViewQty = GlobalFunctions.GetFormFunction(formID, "ViewQty");
					CanModifySubAccount = GlobalFunctions.GetFormFunction(formID, "ModifySubAccount");
					CanViewJV = GlobalFunctions.GetFormFunction(formID, "ViewJV");
					ViewAllEmployees = GlobalFunctions.GetFormFunction(formID, "ViewAllEmployees");
					CanUpdateProductionBatchNo = GlobalFunctions.GetFormFunction(formID, "UpdateProductionBatchNo");
					CanMinimunCharge = GlobalFunctions.GetFormFunction(formID, "MinimunCharge");
					CanEditValue = GlobalFunctions.GetFormFunction(formID, "EditValue");
					CanCloseCheck = GlobalFunctions.GetFormFunction(formID, "CloseCheck");
					CanSplitCheck = GlobalFunctions.GetFormFunction(formID, "SplitCheck");
					CanMergeCheck = GlobalFunctions.GetFormFunction(formID, "MergeCheck");
					CanDirect = GlobalFunctions.GetFormFunction(formID, "Direct");
					GlobalVariables.CanPrint = (CanPrint = GlobalFunctions.GetFormFunction(formID, "Printing"));
					GlobalVariables.CanExport = (CanExport = GlobalFunctions.GetFormFunction(formID, "Exporting"));
					GlobalVariables.CanViewReport = (CanViewReport = GlobalFunctions.GetFormFunction(formID, "View Reports"));
					GlobalVariables.CanPrintReport = (CanPrintReport = GlobalFunctions.GetFormFunction(formID, "Print Reports"));
					GlobalVariables.CanDirectPrint = (CanDirectPrint = GlobalFunctions.GetFormFunction(formID, "DirectPrint"));
					GlobalVariables.Tap1 = (Tap1 = GlobalFunctions.GetFormFunction(formID, "Tap1"));
					GlobalVariables.Tap2 = (Tap2 = GlobalFunctions.GetFormFunction(formID, "Tap2"));
					GlobalVariables.Tap3 = (Tap3 = GlobalFunctions.GetFormFunction(formID, "Tap3"));
					GlobalVariables.Tap4 = (Tap4 = GlobalFunctions.GetFormFunction(formID, "Tap4"));
					GlobalVariables.Tap5 = (Tap5 = GlobalFunctions.GetFormFunction(formID, "Tap5"));
					GlobalVariables.Tap6 = (Tap6 = GlobalFunctions.GetFormFunction(formID, "Tap6"));
					GlobalVariables.Tap7 = (Tap7 = GlobalFunctions.GetFormFunction(formID, "Tap7"));
					GlobalVariables.Tap8 = (Tap8 = GlobalFunctions.GetFormFunction(formID, "Tap8"));
					GlobalVariables.Tap9 = (Tap9 = GlobalFunctions.GetFormFunction(formID, "Tap9"));
					GlobalVariables.CanMultiPrint = (CanMultiPrint = GlobalFunctions.GetFormFunction(formID, "MultiPrint"));
					GlobalVariables.ViewAllBranches = (ViewAllBranches = GlobalFunctions.GetFormFunction(formID, "ViewAllBranches"));
				}
			}
			else
			{
				CanPrint = GlobalVariables.CanPrint;
				CanExport = GlobalVariables.CanExport;
				CanViewReport = GlobalVariables.CanViewReport;
				CanPrintReport = GlobalVariables.CanPrintReport;
				CanDirectPrint = GlobalVariables.CanDirectPrint;
				Tap1 = GlobalVariables.Tap1;
				Tap2 = GlobalVariables.Tap2;
				Tap3 = GlobalVariables.Tap3;
				Tap4 = GlobalVariables.Tap4;
				Tap5 = GlobalVariables.Tap5;
				Tap6 = GlobalVariables.Tap6;
				Tap7 = GlobalVariables.Tap7;
				Tap8 = GlobalVariables.Tap8;
				Tap9 = GlobalVariables.Tap9;
				CanMultiPrint = GlobalVariables.CanMultiPrint;
				ViewAllBranches = GlobalVariables.ViewAllBranches;
			}
		}
		SetControlAppearence(this);
		if (base.Controls.IndexOfKey("lblTitle") > -1)
		{
			base.Controls["lblTitle"].BringToFront();
		}
		if (base.Controls.IndexOfKey("lblTitle2") > -1)
		{
			base.Controls["lblTitle2"].SendToBack();
		}
		PrepareData();
		PrepareData2();
	}

	private void frmBase_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void frmBase_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (TableName.Length > 0)
		{
			UsersTransactions.DeleteByUserLoginID(GlobalVariables.UserLoginID, TableName);
		}
		Dispose();
	}

	private void frmBase_KeyPress(object sender, KeyPressEventArgs e)
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
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmBase));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.lblTop = new UltraLabel();
		this.lblBottom = new UltraLabel();
		this.lblLeft = new UltraLabel();
		this.lblRight = new UltraLabel();
		base.SuspendLayout();
		resources.ApplyResources(this.lblTop, "lblTop");
		((AppearanceBase)val).BackColor = System.Drawing.Color.Black;
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "";
		((ControlBase)this.lblTop).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTop).Name = "lblTop";
		((UltraControlBase)this.lblTop).UseAppStyling = false;
		resources.ApplyResources(this.lblBottom, "lblBottom");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		resources.ApplyResources(val2, "appearance2");
		((SubObjectBase)val2).ForceApplyResources = "";
		((ControlBase)this.lblBottom).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblBottom).Name = "lblBottom";
		((UltraControlBase)this.lblBottom).UseAppStyling = false;
		resources.ApplyResources(this.lblLeft, "lblLeft");
		((AppearanceBase)val3).BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		resources.ApplyResources(val3, "appearance3");
		((SubObjectBase)val3).ForceApplyResources = "";
		((ControlBase)this.lblLeft).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.lblLeft).Name = "lblLeft";
		((UltraControlBase)this.lblLeft).UseAppStyling = false;
		resources.ApplyResources(this.lblRight, "lblRight");
		((AppearanceBase)val4).BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
		resources.ApplyResources(val4, "appearance4");
		((SubObjectBase)val4).ForceApplyResources = "";
		((ControlBase)this.lblRight).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.lblRight).Name = "lblRight";
		((UltraControlBase)this.lblRight).UseAppStyling = false;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.BackColor = System.Drawing.Color.FromArgb(247, 247, 248);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRight);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBottom);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblLeft);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTop);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmBase";
		base.ShowInTaskbar = false;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmBase_FormClosing);
		base.Load += new System.EventHandler(frmBase_Load);
		base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(frmBase_KeyPress);
		base.ResumeLayout(false);
	}
}
