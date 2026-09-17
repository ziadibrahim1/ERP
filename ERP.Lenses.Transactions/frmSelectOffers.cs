using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.Lenses.Transactions;

public class frmSelectOffers : frmBase
{
	private DataTable dtOffers;

	public DataTable dtOffersItems = new DataTable();

	public DataTable dtOffersPackages = new DataTable();

	public DataTable dtOffersGifts = new DataTable();

	public int OfferID = 0;

	public decimal TotalQty = default(decimal);

	public decimal DiscountRatio = default(decimal);

	public bool ForAll = false;

	public bool LowestPrice = false;

	public bool HighestPrice = false;

	public bool Cancel = false;

	public decimal QtyDiscount = default(decimal);

	private IContainer components = null;

	public UltraButton btnClose;

	private Panel panel1;

	public frmSelectOffers(DataTable DTOFFERS)
	{
		InitializeComponent();
		dtOffers = DTOFFERS;
	}

	public override void PrepareData()
	{
		CreateOffers();
	}

	public void CreateOffers()
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		int num = 4;
		int num2 = 2;
		Control[] array = panel1.Controls.Find("btnOffer", searchAllChildren: true);
		for (int i = 0; i < array.Length; i++)
		{
			panel1.Controls.Remove(array[i]);
		}
		if (dtOffers == null || dtOffers.Rows.Count == 0)
		{
			return;
		}
		num2 = (int)Math.Ceiling((decimal)dtOffers.Rows.Count / (decimal)num);
		int num3 = 0;
		int num4 = 0;
		for (int j = 0; j < dtOffers.Rows.Count; j++)
		{
			Appearance val = new Appearance();
			UltraButton val2 = new UltraButton();
			((AppearanceBase)val).TextHAlignAsString = "Center";
			((AppearanceBase)val).TextVAlignAsString = "Center";
			((AppearanceBase)val).BackColor = Color.Transparent;
			((AppearanceBase)val).BackColor2 = Color.Transparent;
			((AppearanceBase)val).BackGradientStyle = (GradientStyle)9;
			((AppearanceBase)val).ImageHAlign = (HAlign)1;
			((AppearanceBase)val).ImageVAlign = (VAlign)2;
			((ControlBase)val2).Appearance = (AppearanceBase)(object)val;
			((UltraControlBase)val2).UseAppStyling = false;
			((Control)(object)val2).Size = new Size(140, 75);
			((Control)(object)val2).Font = new Font("Tahoma", 12f);
			((Control)(object)val2).Click += btnOffer_Click;
			((Control)(object)val2).Name = "btnOffer";
			((Control)(object)val2).Tag = dtOffers.Rows[j];
			((Control)(object)val2).Text = dtOffers.Rows[j]["OfferName"].ToString();
			((Control)(object)val2).TabIndex = j + 10;
			((Control)(object)val2).Location = new Point(base.Width * num3 / num + 10, 80 * num4 + 30);
			panel1.Controls.Add((Control)(object)val2);
			num3++;
			if (num3 == num)
			{
				num3 = 0;
				num4++;
			}
		}
		panel1.SendToBack();
	}

	private void btnOffer_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		DataRow dataRow = (DataRow)((Control)(UltraButton)sender).Tag;
		if (dataRow["IsPackage"].Equals(true))
		{
			OfferID = int.Parse(dataRow["OfferID"].ToString());
			Close();
		}
		else if (dataRow["IsQtyDiscount"].Equals(false))
		{
			frmLnsInvoicesGiftsoffers frmLnsInvoicesGiftsoffers2 = new frmLnsInvoicesGiftsoffers(int.Parse(dataRow["OfferID"].ToString()));
			frmLnsInvoicesGiftsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			frmLnsInvoicesGiftsoffers2.Location = new Point(0, 0);
			((Control)(object)frmLnsInvoicesGiftsoffers2.lblTitle).Text = (GlobalVariables.IsArabic ? "إختيار عرض" : "Select Offer");
			frmLnsInvoicesGiftsoffers2.ShowDialog();
			dtOffersItems = frmLnsInvoicesGiftsoffers2.dtOffersItems;
			dtOffersGifts = frmLnsInvoicesGiftsoffers2.dtOffersGifts;
			OfferID = frmLnsInvoicesGiftsoffers2.OfferID;
			DiscountRatio = frmLnsInvoicesGiftsoffers2.DiscountRatio;
			Cancel = frmLnsInvoicesGiftsoffers2.Cancel;
			Close();
		}
		else if (dataRow["IsQtyDiscount"].Equals(true))
		{
			frmLnsInvoicesQtyDiscountsoffers frmLnsInvoicesQtyDiscountsoffers2 = new frmLnsInvoicesQtyDiscountsoffers(int.Parse(dataRow["OfferID"].ToString()));
			frmLnsInvoicesQtyDiscountsoffers2.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
			frmLnsInvoicesQtyDiscountsoffers2.Location = new Point(0, 0);
			((Control)(object)frmLnsInvoicesQtyDiscountsoffers2.lblTitle).Text = (GlobalVariables.IsArabic ? "إختيار عرض" : "Select Offer");
			frmLnsInvoicesQtyDiscountsoffers2.ShowDialog();
			dtOffersItems = frmLnsInvoicesQtyDiscountsoffers2.dtOffersItems;
			OfferID = frmLnsInvoicesQtyDiscountsoffers2.OfferID;
			DiscountRatio = frmLnsInvoicesQtyDiscountsoffers2.DiscountRatio;
			Cancel = frmLnsInvoicesQtyDiscountsoffers2.Cancel;
			ForAll = frmLnsInvoicesQtyDiscountsoffers2.ForAll;
			HighestPrice = frmLnsInvoicesQtyDiscountsoffers2.HighestPrice;
			LowestPrice = frmLnsInvoicesQtyDiscountsoffers2.LowestPrice;
			TotalQty = frmLnsInvoicesQtyDiscountsoffers2.TotalQty;
			QtyDiscount = frmLnsInvoicesQtyDiscountsoffers2.QtyDiscount;
			Close();
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Lenses.Transactions.frmSelectOffers));
		Appearance val = new Appearance();
		this.btnClose = new UltraButton();
		this.panel1 = new System.Windows.Forms.Panel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance1.FontData");
		resources.ApplyResources(val, "appearance1");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		this.panel1.AllowDrop = true;
		resources.ApplyResources(this.panel1, "panel1");
		this.panel1.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		this.panel1.Name = "panel1";
		this.AllowDrop = true;
		resources.ApplyResources(this, "$this");
		base.Controls.Add(this.panel1);
		base.Name = "frmSelectOffers";
		base.Controls.SetChildIndex(this.panel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		this.panel1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
