using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace ERP.DirectSalesApp.Reports;

public class GMaps : frmBase
{
	private DataTable dtVisitData = new DataTable();

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraLabel lblTitle2;

	public UltraButton btnClose;

	private GMapControl gmap;

	public GMaps(DataTable visitdata)
	{
		InitializeComponent();
		dtVisitData = visitdata;
	}

	public override void PrepareData()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		for (int i = 0; i < dtVisitData.Rows.Count; i++)
		{
			gmap.MapProvider = (GMapProvider)(object)GoogleMapProvider.Instance;
			Singleton<GMaps>.Instance.Mode = (AccessMode)0;
			gmap.Position = new PointLatLng(double.Parse(dtVisitData.Rows[i]["OpenLatitude"].ToString()), double.Parse(dtVisitData.Rows[i]["OpenLong"].ToString()));
			gmap.ShowCenter = false;
			GMapOverlay val = new GMapOverlay("markers");
			GMapMarker val2 = (GMapMarker)new GMarkerGoogle(new PointLatLng(double.Parse(dtVisitData.Rows[i]["OpenLatitude"].ToString()), double.Parse(dtVisitData.Rows[i]["OpenLong"].ToString())), (GMarkerGoogleType)16);
			val2.ToolTipText = " Client Name:" + dtVisitData.Rows[i]["ClientName"].ToString() + "\n Sales Name:" + dtVisitData.Rows[i]["SalesManName2"].ToString() + "\n Open:" + dtVisitData.Rows[i]["VisitStartDate"].ToString() + "\n End:" + dtVisitData.Rows[i]["VisitEndDate"].ToString() + "\n Address:" + dtVisitData.Rows[i]["Address"].ToString() + "\n";
			((Collection<GMapMarker>)(object)val.Markers).Add(val2);
			((Collection<GMapOverlay>)(object)gmap.Overlays).Add(val);
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
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.DirectSalesApp.Reports.GMaps));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.lblTitle = new UltraLabel();
		this.lblTitle2 = new UltraLabel();
		this.btnClose = new UltraButton();
		this.gmap = new GMapControl();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance3");
		resources.ApplyResources(((AppearanceBase)val).FontData, "appearance3.FontData");
		((SubObjectBase)val).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.lblTitle2, "lblTitle2");
		((AppearanceBase)val2).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val2).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val2).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val2, "appearance11");
		resources.ApplyResources(((AppearanceBase)val2).FontData, "appearance11.FontData");
		((SubObjectBase)val2).ForceApplyResources = "FontData|";
		((ControlBase)this.lblTitle2).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.lblTitle2).Name = "lblTitle2";
		((UltraControlBase)this.lblTitle2).UseAppStyling = false;
		resources.ApplyResources(this.btnClose, "btnClose");
		((AppearanceBase)val3).Image = resources.GetObject("appearance1.Image");
		resources.ApplyResources(((AppearanceBase)val3).FontData, "appearance1.FontData");
		resources.ApplyResources(val3, "appearance1");
		((SubObjectBase)val3).ForceApplyResources = "FontData|";
		((ControlBase)this.btnClose).Appearance = (AppearanceBase)(object)val3;
		((UltraButtonBase)this.btnClose).DialogResult = System.Windows.Forms.DialogResult.Cancel;
		((ControlBase)this.btnClose).ImageSize = new System.Drawing.Size(20, 20);
		((System.Windows.Forms.Control)(object)this.btnClose).Name = "btnClose";
		((System.Windows.Forms.Control)(object)this.btnClose).Click += new System.EventHandler(btnClose_Click);
		resources.ApplyResources(this.gmap, "gmap");
		this.gmap.Bearing = 0f;
		this.gmap.CanDragMap = true;
		this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
		this.gmap.GrayScaleMode = true;
		this.gmap.HelperLineOption = (HelperLineOptions)0;
		this.gmap.LevelsKeepInMemmory = 5;
		this.gmap.MarkersEnabled = true;
		this.gmap.MaxZoom = 30;
		this.gmap.MinZoom = 2;
		this.gmap.MouseWheelZoomEnabled = true;
		this.gmap.MouseWheelZoomType = (MouseWheelZoomType)0;
		((System.Windows.Forms.Control)(object)this.gmap).Name = "gmap";
		this.gmap.NegativeMode = false;
		this.gmap.PolygonsEnabled = true;
		this.gmap.RetryLoadTile = 0;
		this.gmap.RoutesEnabled = true;
		this.gmap.ScaleMode = (ScaleModes)0;
		this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(33, 65, 105, 225);
		this.gmap.ShowTileGridLines = false;
		this.gmap.Zoom = 10.0;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.gmap);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClose);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle2);
		base.Name = "GMaps";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.gmap, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
