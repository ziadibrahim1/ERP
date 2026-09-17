using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Properties;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinTree;

namespace ERP.SystemOptions.GeneralData;

public class frmSearchColumns : frmBase
{
	private string FormName = "";

	public bool HasChanges;

	private DataTable dtColumns;

	private DataTable dtSelected;

	private IContainer components = null;

	public UltraTree treeAllFields;

	public UltraTree treeSelectedFields;

	public UltraLabel lblSelectedFields;

	public UltraLabel lblAllFields;

	public UltraButton btnMoveToSelectedFields;

	public UltraButton btnMoveToAllFields;

	public UltraButton btnSave;

	public UltraButton btnClose;

	public UltraLabel lblTitle;

	public UltraButton btnDown;

	public UltraButton btnUp;

	public UltraLabel lblTitle2;

	private NumericUpDown txtSearchPeriod;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	public frmSearchColumns()
	{
		InitializeComponent();
		treeSelectedFields.Override.ActiveNodeAppearance.BackColor = Color.Gray;
		treeAllFields.Override.ActiveNodeAppearance.BackColor = Color.Gray;
	}

	public frmSearchColumns(DataTable dtCol, DataTable dtSelectedCol, string FormName)
		: this()
	{
		dtColumns = dtCol;
		dtSelected = dtSelectedCol;
		this.FormName = FormName;
	}

	public override void PrepareData()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		for (int i = 0; i < dtColumns.Rows.Count; i++)
		{
			UltraTreeNode val = new UltraTreeNode(dtColumns.Rows[i]["ColKey"].ToString(), dtColumns.Rows[i]["ColName"].ToString());
			if (dtSelected.Select(string.Concat("ColKey='", dtColumns.Rows[i]["ColKey"], "'")).Length == 0)
			{
				treeAllFields.Nodes.Add(val);
			}
		}
		for (int j = 0; j < dtSelected.Rows.Count; j++)
		{
			DataRow dataRow = dtColumns.Select(string.Concat("ColKey='", dtSelected.Rows[j]["ColKey"], "'"))[0];
			UltraTreeNode val2 = new UltraTreeNode(dataRow["ColKey"].ToString(), dataRow["ColName"].ToString());
			treeSelectedFields.Nodes.Add(val2);
		}
		DataTable dataTable = SearchPeriod.SelectByFormName(GlobalVariables.UserID, FormName, IsFromServer: false);
		if (dataTable.Rows.Count > 0)
		{
			txtSearchPeriod.Value = Convert.ToInt32(dataTable.Rows[0]["SearchPeriodDays"]);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
	}

	private void treeSelectedFields_AfterSelect(object sender, SelectEventArgs e)
	{
		if (treeSelectedFields.ActiveNode.Level == 1)
		{
			((Control)(object)btnUp).Enabled = treeSelectedFields.ActiveNode.Index != 0;
			((Control)(object)btnDown).Enabled = treeSelectedFields.ActiveNode.Index != ((DisposableObjectCollectionBase)treeSelectedFields.ActiveNode.Parent.Nodes).Count - 1;
		}
	}

	private void btnMoveToAllFields_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		if (treeSelectedFields.ActiveNode != null)
		{
			UltraTreeNode activeNode = treeSelectedFields.ActiveNode;
			treeSelectedFields.ActiveNode.Remove();
			treeAllFields.Nodes.Add(activeNode);
		}
	}

	private void btnMoveToSelectedFields_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		if (treeAllFields.ActiveNode != null)
		{
			UltraTreeNode activeNode = treeAllFields.ActiveNode;
			treeAllFields.ActiveNode.Remove();
			treeSelectedFields.Nodes.Add(activeNode);
		}
	}

	private void btnUp_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeSelectedFields.ActiveNode;
		treeSelectedFields.ActiveNode.Reposition(treeSelectedFields.ActiveNode, (NodePosition)2);
		treeSelectedFields.ActiveNode = activeNode;
		treeSelectedFields_AfterSelect(null, null);
	}

	private void btnDown_Click(object sender, EventArgs e)
	{
		HasChanges = true;
		UltraTreeNode activeNode = treeSelectedFields.ActiveNode;
		treeSelectedFields.ActiveNode.Reposition(treeSelectedFields.ActiveNode, (NodePosition)3);
		treeSelectedFields.ActiveNode = activeNode;
		treeSelectedFields_AfterSelect(null, null);
	}

	private void txtSearchPeriod_ValueChanged(object sender, EventArgs e)
	{
		HasChanges = true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (HasChanges)
		{
			Main.StartBulkTrans(FromServer: false);
			try
			{
				Save();
				Main.EndBulkTrans(FromServer: false);
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
			}
		}
	}

	public void Save()
	{
		SearchPeriod.DeleteByFormName(GlobalVariables.UserID, FormName, IsFromServer: false);
		SearchColumns.DeleteByFormName(GlobalVariables.UserID, FormName, IsFromServer: false);
		SearchPeriod.Insert_Update("-1", FormName, GlobalVariables.UserID, txtSearchPeriod.Value.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
		for (int i = 0; i < ((DisposableObjectCollectionBase)treeSelectedFields.Nodes).Count; i++)
		{
			SearchColumns.Insert_Update("-1", FormName, GlobalVariables.UserID, ((KeyedSubObjectBase)treeSelectedFields.Nodes[i]).Key, treeSelectedFields.Nodes[i].Index.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID, IsFromServer: false);
		}
		Close();
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
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.GeneralData.frmSearchColumns));
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
		this.treeAllFields = new UltraTree();
		this.treeSelectedFields = new UltraTree();
		this.lblSelectedFields = new UltraLabel();
		this.lblAllFields = new UltraLabel();
		this.btnMoveToSelectedFields = new UltraButton();
		this.btnMoveToAllFields = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnClose = new UltraButton();
		this.lblTitle = new UltraLabel();
		this.btnDown = new UltraButton();
		this.btnUp = new UltraButton();
		this.lblTitle2 = new UltraLabel();
		this.txtSearchPeriod = new System.Windows.Forms.NumericUpDown();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeAllFields).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.treeSelectedFields).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSearchPeriod).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.treeAllFields, "treeAllFields");
		((System.Windows.Forms.Control)(object)this.treeAllFields).AllowDrop = true;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val, "appearance1");
		this.treeAllFields.Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.treeAllFields).Name = "treeAllFields";
		((UltraControlBase)this.treeAllFields).UseAppStyling = false;
		resources.ApplyResources(this.treeSelectedFields, "treeSelectedFields");
		((System.Windows.Forms.Control)(object)this.treeSelectedFields).AllowDrop = true;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val2, "appearance2");
		this.treeSelectedFields.Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.treeSelectedFields).Name = "treeSelectedFields";
		((UltraControlBase)this.treeSelectedFields).UseAppStyling = false;
		this.treeSelectedFields.AfterSelect += new AfterNodeSelectEventHandler(treeSelectedFields_AfterSelect);
		resources.ApplyResources(this.lblSelectedFields, "lblSelectedFields");
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.lblSelectedFields).Appearance = (AppearanceBase)(object)val3;
		this.lblSelectedFields.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSelectedFields).Name = "lblSelectedFields";
		((ControlBase)this.lblSelectedFields).WrapText = false;
		resources.ApplyResources(this.lblAllFields, "lblAllFields");
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Navy;
		resources.ApplyResources(val4, "appearance4");
		((ControlBase)this.lblAllFields).Appearance = (AppearanceBase)(object)val4;
		this.lblAllFields.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAllFields).Name = "lblAllFields";
		((ControlBase)this.lblAllFields).WrapText = false;
		resources.ApplyResources(this.btnMoveToSelectedFields, "btnMoveToSelectedFields");
		((System.Windows.Forms.Control)(object)this.btnMoveToSelectedFields).Name = "btnMoveToSelectedFields";
		((System.Windows.Forms.Control)(object)this.btnMoveToSelectedFields).Click += new System.EventHandler(btnMoveToSelectedFields_Click);
		resources.ApplyResources(this.btnMoveToAllFields, "btnMoveToAllFields");
		((System.Windows.Forms.Control)(object)this.btnMoveToAllFields).Name = "btnMoveToAllFields";
		((System.Windows.Forms.Control)(object)this.btnMoveToAllFields).Click += new System.EventHandler(btnMoveToAllFields_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val5).Image = resources.GetObject("appearance5.Image");
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val5;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val6).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val6).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val6).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val6, "appearance6");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnDown, "btnDown");
		((AppearanceBase)val7).Image = resources.GetObject("appearance7.Image");
		resources.ApplyResources(val7, "appearance7");
		((ControlBase)this.btnDown).Appearance = (AppearanceBase)(object)val7;
		((ControlBase)this.btnDown).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnDown).Name = "btnDown";
		((System.Windows.Forms.Control)(object)this.btnDown).Click += new System.EventHandler(btnDown_Click);
		resources.ApplyResources(this.btnUp, "btnUp");
		((AppearanceBase)val8).Image = ERP.Properties.Resources.btnUP;
		resources.ApplyResources(val8, "appearance8");
		((ControlBase)this.btnUp).Appearance = (AppearanceBase)(object)val8;
		((ControlBase)this.btnUp).ImageSize = new System.Drawing.Size(18, 18);
		((System.Windows.Forms.Control)(object)this.btnUp).Name = "btnUp";
		((System.Windows.Forms.Control)(object)this.btnUp).Click += new System.EventHandler(btnUp_Click);
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val9).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val9).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val9).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val9, "appearance9");
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.txtSearchPeriod, "txtSearchPeriod");
		this.txtSearchPeriod.Maximum = new decimal(new int[4] { 10000, 0, 0, 0 });
		this.txtSearchPeriod.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtSearchPeriod.Name = "txtSearchPeriod";
		this.txtSearchPeriod.Value = new decimal(new int[4] { 365, 0, 0, 0 });
		this.txtSearchPeriod.ValueChanged += new System.EventHandler(txtSearchPeriod_ValueChanged);
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((AppearanceBase)val10).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val10, "appearance10");
		((ControlBase)this.ultraLabel1).Appearance = (AppearanceBase)(object)val10;
		this.ultraLabel1.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((AppearanceBase)val11).BackColor = System.Drawing.Color.Transparent;
		resources.ApplyResources(val11, "appearance11");
		((ControlBase)this.ultraLabel2).Appearance = (AppearanceBase)(object)val11;
		this.ultraLabel2.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.txtSearchPeriod);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDown);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnUp);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToSelectedFields);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnMoveToAllFields);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAllFields);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSelectedFields);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeSelectedFields);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.treeAllFields);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "frmSearchColumns";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeAllFields, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.treeSelectedFields, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblSelectedFields, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblAllFields, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToAllFields, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnMoveToSelectedFields, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnUp, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDown, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex(this.txtSearchPeriod, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeAllFields).EndInit();
		((System.ComponentModel.ISupportInitialize)this.treeSelectedFields).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSearchPeriod).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
