using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.Ticketing;

public class frmPreviewImage : frmBase
{
	private bool CanEdit = false;

	private bool drawing = false;

	private bool Line = true;

	private Point OldPoint = default(Point);

	public Image OrgnImg;

	private Stack<Image> _undoStack = new Stack<Image>();

	private Pen pn = new Pen(Color.Red, 3f);

	private IContainer components = null;

	private UltraButton btnClose;

	private UltraPictureBox picImageToPreview;

	private UltraButton btnLine;

	private UltraButton btnCancel;

	private UltraButton btnClear;

	private UltraButton btnEllipse;

	public frmPreviewImage()
	{
		InitializeComponent();
	}

	public frmPreviewImage(Image img, bool canEdit)
	{
		OrgnImg = img;
		InitializeComponent();
		picImageToPreview.Image = img.Clone();
		((Control)(object)btnClear).Visible = (((Control)(object)btnCancel).Visible = (((Control)(object)btnLine).Visible = (((Control)(object)btnEllipse).Visible = (CanEdit = canEdit))));
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		OrgnImg = (Image)picImageToPreview.Image;
		Close();
	}

	private void picImageToPreview_MouseDown(object sender, MouseEventArgs e)
	{
		drawing = true;
		OldPoint = new Point(e.X, e.Y);
		_undoStack.Push((Image)((Image)picImageToPreview.Image).Clone());
	}

	private void picImageToPreview_MouseMove(object sender, MouseEventArgs e)
	{
		if (drawing && CanEdit)
		{
			if (Line)
			{
				Graphics graphics = Graphics.FromImage((Image)picImageToPreview.Image);
				graphics.DrawLine(pn, OldPoint.X, OldPoint.Y, e.X, e.Y);
				OldPoint.X = e.X;
				OldPoint.Y = e.Y;
				((Control)(object)picImageToPreview).Refresh();
			}
			else
			{
				((Control)(object)picImageToPreview).Refresh();
				Graphics graphics2 = ((Control)(object)picImageToPreview).CreateGraphics();
				graphics2.DrawEllipse(pn, OldPoint.X, OldPoint.Y, e.X - OldPoint.X, e.Y - OldPoint.Y);
				graphics2.Dispose();
			}
		}
	}

	private void picImageToPreview_MouseUp(object sender, MouseEventArgs e)
	{
		if (drawing && CanEdit)
		{
			if (!Line)
			{
				Graphics graphics = Graphics.FromImage((Image)picImageToPreview.Image);
				graphics.DrawEllipse(pn, OldPoint.X - 2, OldPoint.Y, e.X - OldPoint.X, e.Y - OldPoint.Y);
				graphics.Dispose();
				((Control)(object)picImageToPreview).Refresh();
			}
			drawing = false;
		}
	}

	private void btnUnDo_Click(object sender, EventArgs e)
	{
		if (_undoStack.Count > 0)
		{
			picImageToPreview.Image = _undoStack.Pop();
			((Control)(object)picImageToPreview).Refresh();
		}
	}

	private void btnClear_Click(object sender, EventArgs e)
	{
		picImageToPreview.Image = OrgnImg.Clone();
		_undoStack.Push((Image)((Image)picImageToPreview.Image).Clone());
		((Control)(object)picImageToPreview).Refresh();
	}

	private void btnLine_Click(object sender, EventArgs e)
	{
		if (!Line)
		{
			Line = true;
			((Control)(object)btnLine).Enabled = false;
			((Control)(object)btnEllipse).Enabled = true;
		}
	}

	private void btnEllipse_Click(object sender, EventArgs e)
	{
		if (Line)
		{
			Line = false;
			((Control)(object)btnLine).Enabled = true;
			((Control)(object)btnEllipse).Enabled = false;
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
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Ticketing.frmPreviewImage));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.btnClose = new UltraButton();
		this.picImageToPreview = new UltraPictureBox();
		this.btnLine = new UltraButton();
		this.btnCancel = new UltraButton();
		this.btnClear = new UltraButton();
		this.btnEllipse = new UltraButton();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.picImageToPreview, "picImageToPreview");
		((AppearanceBase)val).ImageVAlign = (VAlign)1;
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		this.picImageToPreview.Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.picImageToPreview).BackColor = System.Drawing.Color.Transparent;
		this.picImageToPreview.BorderShadowColor = System.Drawing.Color.Empty;
		this.picImageToPreview.BorderStyle = (UIElementBorderStyle)1;
		((System.Windows.Forms.Control)(object)this.picImageToPreview).Name = "picImageToPreview";
		((System.Windows.Forms.Control)(object)this.picImageToPreview).MouseDown += new System.Windows.Forms.MouseEventHandler(picImageToPreview_MouseDown);
		((System.Windows.Forms.Control)(object)this.picImageToPreview).MouseMove += new System.Windows.Forms.MouseEventHandler(picImageToPreview_MouseMove);
		((System.Windows.Forms.Control)(object)this.picImageToPreview).MouseUp += new System.Windows.Forms.MouseEventHandler(picImageToPreview_MouseUp);
		resources.ApplyResources(this.btnLine, "btnLine");
		((AppearanceBase)val2).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance3.FontData");
		resources.ApplyResources(val2, "appearance3");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.btnLine).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnLine).ImageSize = new System.Drawing.Size(26, 26);
		((System.Windows.Forms.Control)(object)this.btnLine).Name = "btnLine";
		((System.Windows.Forms.Control)(object)this.btnLine).Click += new System.EventHandler(btnLine_Click);
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val3).Image = resources.GetObject("appearance4.Image");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance4.FontData");
		resources.ApplyResources(val3, "appearance4");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(25, 25);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnUnDo_Click);
		resources.ApplyResources(this.btnClear, "btnClear");
		((System.Windows.Forms.Control)(object)this.btnClear).Name = "btnClear";
		((System.Windows.Forms.Control)(object)this.btnClear).Click += new System.EventHandler(btnClear_Click);
		resources.ApplyResources(this.btnEllipse, "btnEllipse");
		((AppearanceBase)val4).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(((AppearanceBase)val4).FontData, "appearance2.FontData");
		resources.ApplyResources(val4, "appearance2");
		((SubObjectBase)val4).ForceApplyResources = "FontData|";
		((ControlBase)this.btnEllipse).Appearance = (AppearanceBase)(object)val4;
		((ControlBase)this.btnEllipse).ImageSize = new System.Drawing.Size(25, 23);
		((System.Windows.Forms.Control)(object)this.btnEllipse).Name = "btnEllipse";
		((System.Windows.Forms.Control)(object)this.btnEllipse).Click += new System.EventHandler(btnEllipse_Click);
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = (System.Windows.Forms.IButtonControl)this.btnClose;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picImageToPreview);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClear);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnEllipse);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLine);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Name = "frmPreviewImage";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLine, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnEllipse, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClear, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picImageToPreview, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
	}
}
