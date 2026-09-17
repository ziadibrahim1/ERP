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
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinTree;

namespace ERP.SystemOptions.Transactions;

public class frmHomeDesigner : frmBase
{
	private int clickOffsetX;

	private int clickOffsetY;

	private UltraTextEditor[,] txtList = new UltraTextEditor[6, 4];

	private DataTable dtForms;

	private DataTable dtDefaultForms;

	private IContainer components = null;

	public UltraTree TreeItems;

	private Panel panel1;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	public UltraButton btnDelete;

	public UltraButton btnDeleteAll;

	public UltraButton btnClose;

	public frmHomeDesigner()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtForms = Forms.SelectByUserID(GlobalVariables.UserID, IsFromServer: false);
		DrowTree();
		dtDefaultForms = UserDefaultForms.SelectByUserID(GlobalVariables.UserID, IsFromServer: false);
		for (int i = 0; i < dtDefaultForms.Rows.Count; i++)
		{
			int num = Convert.ToInt32(dtDefaultForms.Rows[i]["X"]);
			int num2 = Convert.ToInt32(dtDefaultForms.Rows[i]["Y"]);
			txtList[num, num2] = CreateForm(dtDefaultForms.Rows[i]["FormID"].ToString(), dtDefaultForms.Rows[i]["UserDefaultFormName"].ToString(), num, num2);
			panel1.Controls.Add((Control)(object)txtList[num, num2]);
		}
	}

	private void DrowTree()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		TreeItems.Nodes.Clear();
		DataRow[] array = dtForms.Select("ParentID is null");
		DataRow[] array2 = array;
		foreach (DataRow dataRow in array2)
		{
			UltraTreeNode val = new UltraTreeNode();
			((KeyedSubObjectBase)val).Key = dataRow["FormID"].ToString();
			val.Text = (GlobalVariables.IsArabic ? dataRow["FormNameAr"].ToString() : dataRow["FormNameEn"].ToString());
			TreeItems.Nodes.Add(val);
			DrowChilds(val);
		}
	}

	private void DrowChilds(UltraTreeNode tn)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		DataRow[] array = dtForms.Select("ParentID = " + ((KeyedSubObjectBase)tn).Key);
		if (array.Length != 0)
		{
			DataRow[] array2 = array;
			foreach (DataRow dataRow in array2)
			{
				UltraTreeNode val = new UltraTreeNode();
				((KeyedSubObjectBase)val).Key = dataRow["FormID"].ToString();
				val.Text = (GlobalVariables.IsArabic ? dataRow["FormNameAr"].ToString() : dataRow["FormNameEn"].ToString());
				tn.Nodes.Add(val);
				DrowChilds(val);
			}
		}
	}

	private void TreeItems_DoubleClick(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		UltraTree val = (UltraTree)sender;
		UIElement lastElementEntered = ((ControlUIElementBase)val.UIElement).LastElementEntered;
		if (lastElementEntered == null)
		{
			return;
		}
		object context = lastElementEntered.GetContext(typeof(UltraTreeNode));
		UltraTreeNode val2 = (UltraTreeNode)((context is UltraTreeNode) ? context : null);
		if (val2 == null || val2.Level < 2)
		{
			return;
		}
		int num = -1;
		int num2 = -1;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (txtList[j, i] != null)
				{
					if (((Control)(object)txtList[j, i]).Tag.ToString() == ((KeyedSubObjectBase)val2).Key)
					{
						GlobalVariables.InformationMB.Show("الشاشه موجوده بالفعل", "Form Already Exists ");
						return;
					}
				}
				else if (num == -1)
				{
					num = j;
					num2 = i;
				}
			}
		}
		if (num == -1)
		{
			GlobalVariables.InformationMB.Show("لا يوجد مكان لشاشات اخري", "No place available");
			return;
		}
		txtList[num, num2] = CreateForm(((KeyedSubObjectBase)val2).Key, val2.Text, num, num2);
		panel1.Controls.Add((Control)(object)txtList[num, num2]);
	}

	private UltraTextEditor CreateForm(string formID, string text, int x, int y)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		string text2 = GlobalVariables.dtForms.Select("formID=" + formID)[0]["ParentID"].ToString();
		string text3 = GlobalVariables.dtForms.Select("formID=" + text2)[0]["ParentID"].ToString();
		string text4 = GlobalVariables.dtForms.Select("formID=" + text3)[0]["Form"].ToString();
		UltraTextEditor val = new UltraTextEditor();
		Appearance val2 = new Appearance();
		((AppearanceBase)val2).BackColor = Color.Transparent;
		((AppearanceBase)val2).Image = text4 switch
		{
			"MarineService" => Resources.ship_icon7, 
			"CnsProjects" => Resources.Construction, 
			"Constructions" => Resources.Construction, 
			"Production" => Resources.Production, 
			"HR.Payroll" => Resources.Payroll, 
			"HR.Attendance" => Resources.Attendance, 
			"HR.Personal" => Resources.Personal, 
			"Privilege" => Resources.Security, 
			"Lenses" => Resources.Lns, 
			"POS" => Resources.POS, 
			"SystemOptions" => Resources.Options, 
			"Sales" => Resources.Sales, 
			"Purchasing" => Resources.Purchase, 
			"StockControl" => Resources.Stock, 
			"SafesAndBanks" => Resources.Safes, 
			"Accounting" => Resources.Accounts, 
			_ => Resources.Form, 
		};
		((AppearanceBase)val2).ImageHAlign = (HAlign)2;
		((AppearanceBase)val2).ImageVAlign = (VAlign)1;
		((AppearanceBase)val2).TextHAlignAsString = "Center";
		((AppearanceBase)val2).TextVAlignAsString = "Bottom";
		((TextEditorControlBase)val).Appearance = (AppearanceBase)(object)val2;
		((Control)(object)val).BackColor = Color.Transparent;
		val.Multiline = true;
		((Control)(object)val).Size = new Size(150, 120);
		val.Multiline = false;
		((UltraControlBase)val).UseAppStyling = false;
		((Control)(object)val).LocationChanged += txt_LocationChanged;
		((Control)(object)val).MouseDown += txt_MouseDown;
		((Control)(object)val).MouseMove += txt_MouseMove;
		((Control)(object)val).Name = "txtTable";
		((Control)(object)val).Tag = formID;
		((Control)(object)val).Text = text;
		((Control)(object)val).TabIndex = y * 10 + x;
		((Control)(object)val).Font = new Font("Tahoma", 12f);
		((Control)(object)val).Location = new Point(20 + panel1.Width / 6 * x, 20 + panel1.Height / 4 * y);
		return val;
	}

	private void pnl_DragOver(object sender, DragEventArgs e)
	{
		e.Effect = DragDropEffects.Move;
		object data = e.Data.GetData(e.Data.GetFormats()[0]);
		UltraTextEditor val = (UltraTextEditor)((data is UltraTextEditor) ? data : null);
		if (val == null)
		{
			return;
		}
		Point point = PointToScreen(panel1.Location);
		int num = (int)((double)(e.X - 30 - ((!GlobalVariables.IsArabic) ? point.X : 0)) * 6.0 / (double)(panel1.Width - 20));
		int num2 = (int)((double)(e.Y - 20 - point.Y) * 4.0 / (double)(panel1.Height - 20));
		if (num > 5)
		{
			num = 5;
		}
		if (num2 > 3)
		{
			num2 = 3;
		}
		if (txtList[num, num2] != null)
		{
			return;
		}
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (txtList[i, j] == val)
				{
					txtList[i, j] = null;
				}
			}
		}
		((Control)(object)val).Location = new Point(20 + panel1.Width / 6 * num, 20 + panel1.Height / 4 * num2);
		txtList[num, num2] = val;
	}

	private void pnl_Click(object sender, EventArgs e)
	{
		base.ActiveControl = null;
	}

	private void txt_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (panel1.Cursor == Cursors.No)
		{
			UltraTextEditor val = (UltraTextEditor)sender;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (txtList[i, j] == val)
					{
						txtList[i, j] = null;
					}
				}
			}
			panel1.Controls.Remove((Control)(object)val);
			((Component)(object)val).Dispose();
			panel1.Cursor = Cursors.Default;
		}
		else
		{
			((Control)sender).DoDragDrop(sender, DragDropEffects.Move);
		}
	}

	private void txt_MouseMove(object sender, MouseEventArgs e)
	{
		clickOffsetX = e.X;
		clickOffsetY = e.Y;
	}

	private void txt_LocationChanged(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((TextEditorControlBase)(UltraTextEditor)sender).Editor.ExitEditMode(true, true);
	}

	private void btnDelete_Click(object sender, EventArgs e)
	{
		panel1.Cursor = ((panel1.Cursor == Cursors.No) ? Cursors.Default : Cursors.No);
	}

	private void frmHomeDesigner_Resize(object sender, EventArgs e)
	{
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (txtList[i, j] != null)
				{
					((Control)(object)txtList[i, j]).Location = new Point(20 + panel1.Width / 6 * i, 20 + panel1.Height / 4 * j);
				}
			}
		}
	}

	private void btnDeleteAll_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (txtList[i, j] != null)
				{
					panel1.Controls.Remove((Control)(object)txtList[i, j]);
					((Component)(object)txtList[i, j]).Dispose();
					txtList[i, j] = null;
				}
			}
		}
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		Main.StartBulkTrans(FromServer: false);
		try
		{
			UserDefaultForms.DeleteByUserID(GlobalVariables.UserID, IsFromServer: false);
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					if (txtList[i, j] != null)
					{
						UserDefaultForms.Insert_Update("-1", GlobalVariables.UserID, ((Control)(object)txtList[i, j]).Tag.ToString(), ((Control)(object)txtList[i, j]).Text, "", i.ToString(), j.ToString(), GlobalVariables.CurrentBranchID, "0", GlobalVariables.UserID, IsFromServer: false);
					}
				}
			}
			Main.EndBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show("تم الحفظ بنجاح ", "Data Saved");
		}
		catch
		{
			Main.RollbackBulkTrans(FromServer: false);
			GlobalVariables.InformationMB.Show("حدث خطا لم يتم الحفظ  ", "Error Occured");
			return;
		}
		dtDefaultForms = UserDefaultForms.SelectByUserID(GlobalVariables.UserID, IsFromServer: false);
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (txtList[i, j] != null)
				{
					panel1.Controls.Remove((Control)(object)txtList[i, j]);
					((Component)(object)txtList[i, j]).Dispose();
					txtList[i, j] = null;
				}
			}
		}
		for (int k = 0; k < dtDefaultForms.Rows.Count; k++)
		{
			int num = Convert.ToInt32(dtDefaultForms.Rows[k]["X"]);
			int num2 = Convert.ToInt32(dtDefaultForms.Rows[k]["Y"]);
			txtList[num, num2] = CreateForm(dtDefaultForms.Rows[k]["FormID"].ToString(), dtDefaultForms.Rows[k]["UserDefaultFormName"].ToString(), num, num2);
			panel1.Controls.Add((Control)(object)txtList[num, num2]);
		}
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Dispose();
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.SystemOptions.Transactions.frmHomeDesigner));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		this.TreeItems = new UltraTree();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnDelete = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.btnDeleteAll = new UltraButton();
		this.btnClose = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.TreeItems, "TreeItems");
		((AppearanceBase)val).FontData.BoldAsString = resources.GetString("resource.BoldAsString");
		((AppearanceBase)val).FontData.SizeInPoints = (float)resources.GetObject("resource.SizeInPoints");
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Navy;
		this.TreeItems.Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.TreeItems).Name = "TreeItems";
		((UltraControlBase)this.TreeItems).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.TreeItems).DoubleClick += new System.EventHandler(TreeItems_DoubleClick);
		this.panel1.AllowDrop = true;
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.Name = "panel1";
		this.panel1.Click += new System.EventHandler(pnl_Click);
		this.panel1.DragOver += new System.Windows.Forms.DragEventHandler(pnl_DragOver);
		resources.ApplyResources(this.btnDelete, "btnDelete");
		((AppearanceBase)val2).Image = ERP.Properties.Resources.Delete;
		((AppearanceBase)val2).ImageHAlign = (HAlign)1;
		((AppearanceBase)val2).ImageVAlign = (VAlign)2;
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnDelete).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnDelete).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnDelete).Name = "btnDelete";
		((System.Windows.Forms.Control)(object)this.btnDelete).Click += new System.EventHandler(btnDelete_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val4).Image = resources.GetObject("appearance4.Image");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.btnDeleteAll, "btnDeleteAll");
		((AppearanceBase)val5).Image = ERP.Properties.Resources.Delete;
		((AppearanceBase)val5).ImageHAlign = (HAlign)1;
		((AppearanceBase)val5).ImageVAlign = (VAlign)2;
		resources.ApplyResources(val5, "appearance5");
		((ControlBase)this.btnDeleteAll).Appearance = (AppearanceBase)(object)val5;
		((ControlBase)this.btnDeleteAll).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnDeleteAll).Name = "btnDeleteAll";
		((System.Windows.Forms.Control)(object)this.btnDeleteAll).Click += new System.EventHandler(btnDeleteAll_Click);
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val6).Image = resources.GetObject("appearance6.Image");
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val6;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDeleteAll);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnDelete);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add(this.panel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.TreeItems);
		base.Name = "frmHomeDesigner";
		base.Resize += new System.EventHandler(frmHomeDesigner_Resize);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.TreeItems, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex(this.panel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnDeleteAll, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.TreeItems).EndInit();
		base.ResumeLayout(false);
	}
}
