using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer.Privilege;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.Company;
using ERP.Properties;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.SystemOptions.Transactions;

public class frmHome : frmBase
{
	public frmMain2010 frmMain;

	private UltraLabel[,] lblList = new UltraLabel[6, 4];

	private DataTable dtDefaultForms;

	private IContainer components = null;

	private Panel panel1;

	public UltraButton btnDesign;

	public frmHome()
	{
		InitializeComponent();
	}

	public override void PrepareData()
	{
		dtDefaultForms = UserDefaultForms.SelectByUserID(GlobalVariables.UserID, IsFromServer: false);
		for (int i = 0; i < dtDefaultForms.Rows.Count; i++)
		{
			int num = Convert.ToInt32(dtDefaultForms.Rows[i]["X"]);
			int num2 = Convert.ToInt32(dtDefaultForms.Rows[i]["Y"]);
			if (frmMain.checkFormExists(dtDefaultForms.Rows[i]["FormID"].ToString()))
			{
				lblList[num, num2] = CreateForm(dtDefaultForms.Rows[i]["FormID"].ToString(), dtDefaultForms.Rows[i]["UserDefaultFormName"].ToString(), num, num2);
				panel1.Controls.Add((Control)(object)lblList[num, num2]);
			}
		}
	}

	private UltraLabel CreateForm(string formID, string text, int x, int y)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		string text2 = GlobalVariables.dtForms.Select("formID=" + formID)[0]["ParentID"].ToString();
		string text3 = GlobalVariables.dtForms.Select("formID=" + text2)[0]["ParentID"].ToString();
		string text4 = GlobalVariables.dtForms.Select("formID=" + text3)[0]["Form"].ToString();
		UltraLabel val = new UltraLabel();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
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
		((AppearanceBase)val2).TextVAlignAsString = "Top";
		((AppearanceBase)val2).BackColor = Color.Transparent;
		((AppearanceBase)val2).BackColor2 = Color.Transparent;
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)9;
		((AppearanceBase)val3).BackColor = Color.FromArgb(72, 97, 135);
		((AppearanceBase)val3).ForeColor = Color.White;
		((ControlBase)val).HotTrackAppearance = (AppearanceBase)(object)val3;
		val.BorderStyleInner = (UIElementBorderStyle)13;
		((UltraControlBase)val).UseFlatMode = (DefaultableBoolean)2;
		((ControlBase)val).UseHotTracking = (DefaultableBoolean)1;
		((ControlBase)val).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)val).ImageSize = new Size(80, 80);
		((Control)(object)val).BackColor = Color.Transparent;
		((UltraControlBase)val).UseAppStyling = false;
		((Control)(object)val).Click += lbl_Click;
		((Control)(object)val).Name = "lblTable";
		((Control)(object)val).Tag = formID;
		((Control)(object)val).Text = text;
		((Control)(object)val).TabIndex = y * 10 + x;
		((Control)(object)val).Font = new Font("Tahoma", 12f);
		((Control)(object)val).Location = new Point(20 + panel1.Width / 6 * x, 20 + panel1.Height / 4 * y);
		((Control)(object)val).Size = new Size(170, 130);
		return val;
	}

	private void lbl_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		UltraLabel val = (UltraLabel)sender;
		frmMain.OpenForm(((Control)(object)val).Tag.ToString());
	}

	private void frmHome_Resize(object sender, EventArgs e)
	{
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (lblList[i, j] != null)
				{
					((Control)(object)lblList[i, j]).Location = new Point(20 + panel1.Width / 6 * i, 20 + panel1.Height / 4 * j);
				}
			}
		}
	}

	public void RefreshForms()
	{
		for (int i = 0; i < 6; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (lblList[i, j] != null)
				{
					panel1.Controls.Remove((Control)(object)lblList[i, j]);
					((Component)(object)lblList[i, j]).Dispose();
					lblList[i, j] = null;
				}
			}
		}
		dtDefaultForms = UserDefaultForms.SelectByUserID(GlobalVariables.UserID, IsFromServer: false);
		for (int k = 0; k < dtDefaultForms.Rows.Count; k++)
		{
			int num = Convert.ToInt32(dtDefaultForms.Rows[k]["X"]);
			int num2 = Convert.ToInt32(dtDefaultForms.Rows[k]["Y"]);
			if (frmMain.checkFormExists(dtDefaultForms.Rows[k]["FormID"].ToString()))
			{
				lblList[num, num2] = CreateForm(dtDefaultForms.Rows[k]["FormID"].ToString(), dtDefaultForms.Rows[k]["UserDefaultFormName"].ToString(), num, num2);
				panel1.Controls.Add((Control)(object)lblList[num, num2]);
			}
		}
	}

	private void btnDesign_Click(object sender, EventArgs e)
	{
		frmHomeDesigner frmHomeDesigner2 = new frmHomeDesigner();
		frmHomeDesigner2.Tag = GlobalVariables.dtForms.Select("Form = 'frmHomeDesigner'")[0];
		frmHomeDesigner2.MdiParent = base.MdiParent;
		frmHomeDesigner2.TopLevel = false;
		frmHomeDesigner2.Parent = base.Parent;
		frmHomeDesigner2.Width = base.Width;
		frmHomeDesigner2.Height = base.Height;
		frmHomeDesigner2.Show();
		frmHomeDesigner2.BringToFront();
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		Appearance val = new Appearance();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnDesign = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(1000, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 498);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(998, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 498);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(998, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 496);
		this.panel1.AllowDrop = true;
		this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnDesign);
		this.panel1.Location = new System.Drawing.Point(2, 2);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(996, 496);
		this.panel1.TabIndex = 90;
		((System.Windows.Forms.Control)(object)this.btnDesign).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		((AppearanceBase)val).Image = ERP.Properties.Resources.Update;
		((ControlBase)this.btnDesign).Appearance = (AppearanceBase)(object)val;
		((ControlBase)this.btnDesign).ImageSize = new System.Drawing.Size(20, 25);
		((UltraButtonBase)this.btnDesign).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnDesign).Location = new System.Drawing.Point(970, 463);
		((System.Windows.Forms.Control)(object)this.btnDesign).Name = "btnDesign";
		((UltraButtonBase)this.btnDesign).ShowOutline = false;
		((System.Windows.Forms.Control)(object)this.btnDesign).Size = new System.Drawing.Size(20, 28);
		((System.Windows.Forms.Control)(object)this.btnDesign).TabIndex = 504;
		((System.Windows.Forms.Control)(object)this.btnDesign).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnDesign).Click += new System.EventHandler(btnDesign_Click);
		base.ClientSize = new System.Drawing.Size(1000, 500);
		base.Controls.Add(this.panel1);
		base.Name = "frmHome";
		base.Resize += new System.EventHandler(frmHome_Resize);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex(this.panel1, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
