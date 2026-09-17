using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.DashboardWin;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.ReportGeneration;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UserDesigner.Native;
using DevExpress.XtraSplashScreen;

namespace ERP.AbstractForms;

public class frmTestDev : Form
{
	private IContainer components = null;

	private DashboardDesigner dashboardDesigner1;

	private PivotGridControl pivotGridControl1;

	private TextEdit textEdit1;

	private CheckButton checkButton1;

	private BarCodeControl barCodeControl1;

	private RibbonControl ribbonControl1;

	private ApplicationMenu applicationMenu1;

	private CommandBarItem commandBarItem6;

	private CommandBarItem commandBarItem9;

	private CommandBarItem commandBarItem7;

	private CommandBarItem commandBarItem8;

	private CommandBarItem commandBarItem127;

	private CommandBarItem commandBarItem128;

	private CommandBarItem commandBarItem1;

	private CommandBarItem commandBarItem2;

	private CommandBarItem commandBarItem3;

	private CommandBarItem commandBarItem4;

	private CommandBarItem commandBarItem5;

	private CommandBarItem commandBarItem10;

	private CommandBarItem commandBarItem11;

	private CommandBarItem commandBarItem12;

	private CommandBarItem commandBarItem13;

	private CommandBarItem commandBarItem14;

	private CommandBarItem commandBarItem15;

	private CommandBarItem commandBarItem16;

	private BarEditItem barEditItem1;

	private RecentlyUsedItemsComboBox recentlyUsedItemsComboBox1;

	private BarEditItem barEditItem2;

	private DesignRepositoryItemComboBox designRepositoryItemComboBox1;

	private BarDockPanelsListItem barDockPanelsListItem1;

	private CommandBarItem commandBarItem17;

	private CommandBarItem commandBarItem18;

	private CommandBarItem commandBarItem19;

	private CommandColorBarItem commandColorBarItem1;

	private CommandColorBarItem commandColorBarItem2;

	private CommandBarItem commandBarItem20;

	private CommandBarItem commandBarItem21;

	private CommandBarItem commandBarItem22;

	private CommandBarItem commandBarItem23;

	private CommandBarItem commandBarItem24;

	private CommandBarItem commandBarItem25;

	private CommandBarItem commandBarItem26;

	private CommandBarItem commandBarItem27;

	private CommandBarItem commandBarItem28;

	private CommandBarItem commandBarItem29;

	private CommandBarItem commandBarItem30;

	private CommandBarItem commandBarItem31;

	private CommandBarItem commandBarItem32;

	private CommandBarItem commandBarItem33;

	private CommandBarItem commandBarItem34;

	private CommandBarItem commandBarItem35;

	private CommandBarItem commandBarItem36;

	private CommandBarItem commandBarItem37;

	private CommandColorBarItem commandColorBarItem3;

	private CommandBarItem commandBarItem38;

	private CommandBarItem commandBarItem39;

	private CommandBarItem commandBarItem40;

	private CommandBarItem commandBarItem41;

	private CommandBarItem commandBarItem42;

	private CommandBarItem commandBarItem43;

	private CommandBarItem commandBarItem44;

	private CommandBarItem commandBarItem45;

	private CommandBarItem commandBarItem46;

	private CommandBarItem commandBarItem47;

	private CommandBarItem commandBarItem48;

	private CommandBarItem commandBarItem49;

	private CommandBarItem commandBarItem50;

	private CommandBarItem commandBarItem51;

	private CommandBarItem commandBarItem52;

	private CommandBarItem commandBarItem53;

	private CommandBarItem commandBarItem54;

	private CommandBarItem commandBarItem55;

	private CommandBarItem commandBarItem56;

	private CommandBarItem commandBarItem57;

	private CommandBarItem commandBarItem58;

	private CommandBarItem commandBarItem59;

	private CommandBarItem commandBarItem60;

	private CommandBarItem commandBarItem61;

	private CommandBarCheckItem commandBarCheckItem1;

	private CommandBarCheckItem commandBarCheckItem2;

	private CommandBarItem commandBarItem62;

	private CommandBarItem commandBarItem63;

	private CommandBarItem commandBarItem64;

	private CommandBarItem commandBarItem65;

	private CommandColorBarItem commandColorBarItem4;

	private CommandBarItem commandBarItem66;

	private CommandBarItem commandBarItem67;

	private CommandBarItem commandBarItem68;

	private CommandBarItem commandBarItem69;

	private CommandBarItem commandBarItem70;

	private CommandBarItem commandBarItem71;

	private CommandBarItem commandBarItem72;

	private CommandBarEditItem commandBarEditItem1;

	private RepositoryItemLookUpEdit repositoryItemLookUpEdit1;

	private CommandBarCheckItem commandBarCheckItem3;

	private CommandBarItem commandBarItem73;

	private CommandBarItem commandBarItem74;

	private CommandBarItem commandBarItem75;

	private CommandBarItem commandBarItem76;

	private CommandGalleryBarItem commandGalleryBarItem1;

	private CommandGalleryBarItem commandGalleryBarItem2;

	private CommandGalleryBarItem commandGalleryBarItem3;

	private CommandGalleryBarItem commandGalleryBarItem4;

	private CommandGalleryBarItem commandGalleryBarItem5;

	private CommandGalleryBarItem commandGalleryBarItem6;

	private CommandBarEditItem commandBarEditItem2;

	private RepositoryItemSpinEdit repositoryItemSpinEdit1;

	private CommandBarEditItem commandBarEditItem3;

	private RepositoryItemSpinEdit repositoryItemSpinEdit2;

	private CommandBarEditItem commandBarEditItem4;

	private RepositoryItemSpinEdit repositoryItemSpinEdit3;

	private CommandBarEditItem commandBarEditItem5;

	private RepositoryItemSpinEdit repositoryItemSpinEdit4;

	private CommandBarEditItem commandBarEditItem6;

	private RepositoryItemImageComboBox repositoryItemImageComboBox1;

	private CommandBarEditItem commandBarEditItem7;

	private RepositoryItemLookUpEdit repositoryItemLookUpEdit2;

	private CommandBarEditItem commandBarEditItem8;

	private RepositoryItemComboBox repositoryItemComboBox1;

	private CommandBarItem commandBarItem77;

	private CommandBarItem commandBarItem78;

	private CommandBarItem commandBarItem79;

	private CommandBarItem commandBarItem80;

	private CommandBarItem commandBarItem81;

	private CommandBarItem commandBarItem82;

	private CommandBarItem commandBarItem83;

	private CommandBarItem commandBarItem84;

	private CommandBarItem commandBarItem85;

	private CommandBarItem commandBarItem86;

	private CommandBarItem commandBarItem87;

	private CommandBarItem commandBarItem88;

	private CommandBarItem commandBarItem89;

	private CommandBarItem commandBarItem90;

	private CommandBarItem commandBarItem91;

	private CommandBarItem commandBarItem92;

	private CommandBarItem commandBarItem93;

	private CommandBarItem commandBarItem94;

	private CommandBarItem commandBarItem95;

	private CommandBarItem commandBarItem96;

	private CommandBarItem commandBarItem97;

	private CommandBarItem commandBarItem98;

	private CommandBarItem commandBarItem99;

	private CommandBarItem commandBarItem100;

	private CommandBarItem commandBarItem101;

	private CommandBarItem commandBarItem102;

	private CommandBarItem commandBarItem103;

	private CommandBarItem commandBarItem104;

	private CommandBarItem commandBarItem105;

	private CommandBarItem commandBarItem106;

	private CommandBarItem commandBarItem107;

	private CommandBarItem commandBarItem108;

	private CommandBarItem commandBarItem109;

	private CommandBarItem commandBarItem110;

	private CommandBarItem commandBarItem111;

	private CommandBarItem commandBarItem112;

	private CommandBarItem commandBarItem113;

	private CommandBarItem commandBarItem114;

	private CommandBarItem commandBarItem115;

	private CommandBarItem commandBarItem116;

	private CommandBarItem commandBarItem117;

	private CommandBarItem commandBarItem118;

	private CommandBarItem commandBarItem119;

	private CommandBarItem commandBarItem120;

	private CommandBarItem commandBarItem121;

	private CommandBarItem commandBarItem122;

	private CommandBarCheckItem commandBarCheckItem4;

	private CommandBarCheckItem commandBarCheckItem5;

	private CommandBarCheckItem commandBarCheckItem6;

	private CommandBarCheckItem commandBarCheckItem7;

	private CommandBarCheckItem commandBarCheckItem8;

	private CommandBarCheckItem commandBarCheckItem9;

	private CommandBarItem commandBarItem123;

	private CommandBarItem commandBarItem124;

	private CommandBarItem commandBarItem125;

	private CommandBarItem commandBarItem126;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup1;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup2;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup3;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup4;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup5;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup6;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup7;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup8;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup9;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup10;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup11;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup12;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup13;

	private XRDesignBarButtonGroup xrDesignBarButtonGroup14;

	private PrintPreviewBarItem printPreviewBarItem1;

	private XRDesignRibbonController xrDesignRibbonController1;

	private RibbonStatusBar ribbonStatusBar1;

	private PrintPreviewStaticItem printPreviewStaticItem1;

	private ProgressBarEditItem progressBarEditItem1;

	private RepositoryItemProgressBar repositoryItemProgressBar1;

	private PrintPreviewBarItem printPreviewBarItem52;

	private CommandBarItem commandBarItem129;

	private PrintPreviewStaticItem printPreviewStaticItem2;

	private ZoomTrackBarEditItem zoomTrackBarEditItem1;

	private RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;

	private XRDesignDockManager xrDesignDockManager1;

	private GroupControl groupControl1;

	private CalendarControl calendarControl1;

	private PdfViewer pdfViewer1;

	private LabelControl labelControl1;

	private CalcEdit calcEdit1;

	private DockPanel panelContainer4;

	private GroupAndSortDockPanel groupAndSortDockPanel1;

	private DesignControlContainer groupAndSortDockPanel1_Container;

	private ErrorListDockPanel errorListDockPanel1;

	private DesignControlContainer errorListDockPanel1_Container;

	private DockPanel panelContainer1;

	private DockPanel panelContainer2;

	private ReportExplorerDockPanel reportExplorerDockPanel1;

	private DesignControlContainer reportExplorerDockPanel1_Container;

	private FieldListDockPanel fieldListDockPanel1;

	private DesignControlContainer fieldListDockPanel1_Container;

	private DockPanel panelContainer3;

	private PropertyGridDockPanel propertyGridDockPanel1;

	private DesignControlContainer propertyGridDockPanel1_Container;

	private ReportGalleryDockPanel reportGalleryDockPanel1;

	private DesignControlContainer reportGalleryDockPanel1_Container;

	private PrintPreviewBarItem printPreviewBarItem2;

	private PrintPreviewBarItem printPreviewBarItem3;

	private PrintPreviewBarItem printPreviewBarItem4;

	private PrintPreviewBarItem printPreviewBarItem5;

	private PrintPreviewBarItem printPreviewBarItem7;

	private PrintPreviewBarItem printPreviewBarItem8;

	private PrintPreviewBarItem printPreviewBarItem9;

	private PrintPreviewBarItem printPreviewBarItem11;

	private PrintPreviewBarItem printPreviewBarItem12;

	private PrintPreviewBarItem printPreviewBarItem13;

	private PrintPreviewBarItem printPreviewBarItem14;

	private PrintPreviewBarItem printPreviewBarItem15;

	private PrintPreviewBarItem printPreviewBarItem16;

	private PrintPreviewBarItem printPreviewBarItem17;

	private PrintPreviewBarItem printPreviewBarItem18;

	private PrintPreviewBarItem printPreviewBarItem19;

	private PrintPreviewBarItem printPreviewBarItem20;

	private PrintPreviewBarItem printPreviewBarItem21;

	private PrintPreviewBarItem printPreviewBarItem22;

	private PrintPreviewBarItem printPreviewBarItem23;

	private PrintPreviewBarItem printPreviewBarItem24;

	private PrintPreviewBarItem printPreviewBarItem25;

	private PrintPreviewBarItem printPreviewBarItem26;

	private PrintPreviewBarItem printPreviewBarItem27;

	private PrintPreviewBarItem printPreviewBarItem28;

	private PrintPreviewBarItem printPreviewBarItem29;

	private PrintPreviewBarItem printPreviewBarItem30;

	private PrintPreviewBarItem printPreviewBarItem31;

	private PrintPreviewBarItem printPreviewBarItem32;

	private PrintPreviewBarItem printPreviewBarItem33;

	private PrintPreviewBarItem printPreviewBarItem34;

	private PrintPreviewBarItem printPreviewBarItem35;

	private PrintPreviewBarItem printPreviewBarItem36;

	private PrintPreviewBarItem printPreviewBarItem37;

	private PrintPreviewBarItem printPreviewBarItem38;

	private PrintPreviewBarItem printPreviewBarItem39;

	private PrintPreviewBarItem printPreviewBarItem40;

	private PrintPreviewBarItem printPreviewBarItem41;

	private PrintPreviewBarItem printPreviewBarItem42;

	private PrintPreviewBarItem printPreviewBarItem43;

	private PrintPreviewBarItem printPreviewBarItem44;

	private PrintPreviewBarItem printPreviewBarItem45;

	private PrintPreviewBarItem printPreviewBarItem46;

	private PrintPreviewBarItem printPreviewBarItem47;

	private PrintPreviewBarItem printPreviewBarItem48;

	private PrintPreviewBarItem printPreviewBarItem49;

	private PrintPreviewBarItem printPreviewBarItem50;

	private PrintPreviewBarItem printPreviewBarItem51;

	private XRCharacterCombRibbonPageCategory ribbonPageCategory1;

	private XRCharacterCombDesignContextRibbonPage ribbonPage7;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup20;

	private XRTableRibbonPageCategory ribbonPageCategory2;

	private XRTableDesignContextRibbonPage ribbonPage8;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup21;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup22;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup23;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup24;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup25;

	private XRChartRibbonPageCategory ribbonPageCategory3;

	private XRChartDesignContextRibbonPage ribbonPage9;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup26;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup27;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup28;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup29;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup30;

	private XRPivotGridRibbonPageCategory ribbonPageCategory4;

	private XRPivotGridDesignContextRibbonPage ribbonPage10;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup31;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup32;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup33;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup34;

	private XRBarCodeRibbonPageCategory ribbonPageCategory5;

	private XRBarcodeDesignContextRibbonPage ribbonPage11;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup35;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup36;

	private XRGaugeRibbonPageCategory ribbonPageCategory6;

	private XRGaugeDesignContextRibbonPage ribbonPage12;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup37;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup38;

	private XRSparklineRibbonPageCategory ribbonPageCategory7;

	private XRSparklineDesignContextRibbonPage ribbonPage13;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup39;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup40;

	private XRShapeRibbonPageCategory ribbonPageCategory8;

	private XRShapeDesignContextRibbonPage ribbonPage14;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup41;

	private XRLabelRibbonPageCategory ribbonPageCategory9;

	private XRLabelTextContextRibbonPage ribbonPage15;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup42;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup43;

	private RibbonPage ribbonPage1;

	private RibbonPageGroup ribbonPageGroup1;

	private XRHomeRibbonPage ribbonPage2;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup1;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup2;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup3;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup4;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup5;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup6;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup7;

	private XRLayoutRibbonPage ribbonPage3;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup8;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup9;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup10;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup11;

	private XRPageRibbonPage ribbonPage4;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup12;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup13;

	private XRViewRibbonPage ribbonPage5;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup14;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup15;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup16;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup17;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup18;

	private XRScriptsRibbonPage ribbonPage6;

	private XRDesignRibbonPageGroup xrDesignRibbonPageGroup19;

	private PrintPreviewRibbonPage ribbonPage16;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup1;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup2;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup3;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup4;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup5;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup6;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup7;

	private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup8;

	private PopupMenu popupMenu1;

	private ToolTipController toolTipController1;

	private ReportGenerator reportGenerator1;

	private XtraSaveFileDialog xtraSaveFileDialog1;

	private RibbonReportDesigner ribbonReportDesigner1;

	private XRDesignMdiController reportDesigner1;

	public frmTestDev()
	{
		InitializeComponent();
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Expected O, but got Unknown
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected O, but got Unknown
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Expected O, but got Unknown
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Expected O, but got Unknown
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Expected O, but got Unknown
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Expected O, but got Unknown
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Expected O, but got Unknown
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Expected O, but got Unknown
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Expected O, but got Unknown
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Expected O, but got Unknown
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected O, but got Unknown
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Expected O, but got Unknown
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Expected O, but got Unknown
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Expected O, but got Unknown
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Expected O, but got Unknown
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Expected O, but got Unknown
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Expected O, but got Unknown
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Expected O, but got Unknown
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected O, but got Unknown
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Expected O, but got Unknown
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Expected O, but got Unknown
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Expected O, but got Unknown
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Expected O, but got Unknown
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Expected O, but got Unknown
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Expected O, but got Unknown
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Expected O, but got Unknown
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Expected O, but got Unknown
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Expected O, but got Unknown
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Expected O, but got Unknown
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected O, but got Unknown
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Expected O, but got Unknown
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Expected O, but got Unknown
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Expected O, but got Unknown
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Expected O, but got Unknown
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Expected O, but got Unknown
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Expected O, but got Unknown
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Expected O, but got Unknown
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Expected O, but got Unknown
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Expected O, but got Unknown
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Expected O, but got Unknown
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Expected O, but got Unknown
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Expected O, but got Unknown
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Expected O, but got Unknown
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Expected O, but got Unknown
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Expected O, but got Unknown
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Expected O, but got Unknown
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Expected O, but got Unknown
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Expected O, but got Unknown
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Expected O, but got Unknown
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Expected O, but got Unknown
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Expected O, but got Unknown
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Expected O, but got Unknown
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Expected O, but got Unknown
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Expected O, but got Unknown
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Expected O, but got Unknown
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Expected O, but got Unknown
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Expected O, but got Unknown
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Expected O, but got Unknown
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Expected O, but got Unknown
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Expected O, but got Unknown
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Expected O, but got Unknown
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Expected O, but got Unknown
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Expected O, but got Unknown
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Expected O, but got Unknown
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Expected O, but got Unknown
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Expected O, but got Unknown
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Expected O, but got Unknown
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Expected O, but got Unknown
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Expected O, but got Unknown
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Expected O, but got Unknown
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Expected O, but got Unknown
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Expected O, but got Unknown
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Expected O, but got Unknown
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Expected O, but got Unknown
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Expected O, but got Unknown
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Expected O, but got Unknown
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Expected O, but got Unknown
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Expected O, but got Unknown
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Expected O, but got Unknown
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Expected O, but got Unknown
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Expected O, but got Unknown
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Expected O, but got Unknown
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Expected O, but got Unknown
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Expected O, but got Unknown
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Expected O, but got Unknown
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Expected O, but got Unknown
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Expected O, but got Unknown
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Expected O, but got Unknown
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Expected O, but got Unknown
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Expected O, but got Unknown
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Expected O, but got Unknown
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Expected O, but got Unknown
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Expected O, but got Unknown
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Expected O, but got Unknown
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Expected O, but got Unknown
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Expected O, but got Unknown
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Expected O, but got Unknown
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Expected O, but got Unknown
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Expected O, but got Unknown
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Expected O, but got Unknown
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Expected O, but got Unknown
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Expected O, but got Unknown
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Expected O, but got Unknown
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Expected O, but got Unknown
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Expected O, but got Unknown
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Expected O, but got Unknown
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Expected O, but got Unknown
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Expected O, but got Unknown
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Expected O, but got Unknown
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Expected O, but got Unknown
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Expected O, but got Unknown
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Expected O, but got Unknown
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Expected O, but got Unknown
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Expected O, but got Unknown
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Expected O, but got Unknown
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Expected O, but got Unknown
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Expected O, but got Unknown
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Expected O, but got Unknown
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Expected O, but got Unknown
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Expected O, but got Unknown
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Expected O, but got Unknown
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ed: Expected O, but got Unknown
		//IL_05ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Expected O, but got Unknown
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Expected O, but got Unknown
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Expected O, but got Unknown
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Expected O, but got Unknown
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Expected O, but got Unknown
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Expected O, but got Unknown
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_061e: Expected O, but got Unknown
		//IL_061e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0625: Expected O, but got Unknown
		//IL_0625: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Expected O, but got Unknown
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Expected O, but got Unknown
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_063a: Expected O, but got Unknown
		//IL_063a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Expected O, but got Unknown
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Expected O, but got Unknown
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Expected O, but got Unknown
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Expected O, but got Unknown
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Expected O, but got Unknown
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Expected O, but got Unknown
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Expected O, but got Unknown
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Expected O, but got Unknown
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Expected O, but got Unknown
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Expected O, but got Unknown
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Expected O, but got Unknown
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Expected O, but got Unknown
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0695: Expected O, but got Unknown
		//IL_0695: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Expected O, but got Unknown
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Expected O, but got Unknown
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Expected O, but got Unknown
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Expected O, but got Unknown
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Expected O, but got Unknown
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Expected O, but got Unknown
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c6: Expected O, but got Unknown
		//IL_06c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Expected O, but got Unknown
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Expected O, but got Unknown
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Expected O, but got Unknown
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Expected O, but got Unknown
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Expected O, but got Unknown
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Expected O, but got Unknown
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Expected O, but got Unknown
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Expected O, but got Unknown
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Expected O, but got Unknown
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Expected O, but got Unknown
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Expected O, but got Unknown
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0724: Expected O, but got Unknown
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Expected O, but got Unknown
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Expected O, but got Unknown
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0745: Expected O, but got Unknown
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Expected O, but got Unknown
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Expected O, but got Unknown
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0766: Expected O, but got Unknown
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Expected O, but got Unknown
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Expected O, but got Unknown
		//IL_077e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Expected O, but got Unknown
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Expected O, but got Unknown
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Expected O, but got Unknown
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Expected O, but got Unknown
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Expected O, but got Unknown
		//IL_07b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Expected O, but got Unknown
		//IL_07c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c9: Expected O, but got Unknown
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Expected O, but got Unknown
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Expected O, but got Unknown
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ea: Expected O, but got Unknown
		//IL_07ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f5: Expected O, but got Unknown
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Expected O, but got Unknown
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_080b: Expected O, but got Unknown
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0816: Expected O, but got Unknown
		//IL_0818: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Expected O, but got Unknown
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Expected O, but got Unknown
		//IL_082e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Expected O, but got Unknown
		//IL_0839: Unknown result type (might be due to invalid IL or missing references)
		//IL_0842: Expected O, but got Unknown
		//IL_0844: Unknown result type (might be due to invalid IL or missing references)
		//IL_084d: Expected O, but got Unknown
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0858: Expected O, but got Unknown
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0863: Expected O, but got Unknown
		//IL_0865: Unknown result type (might be due to invalid IL or missing references)
		//IL_086e: Expected O, but got Unknown
		//IL_0870: Unknown result type (might be due to invalid IL or missing references)
		//IL_0879: Expected O, but got Unknown
		//IL_087b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Expected O, but got Unknown
		//IL_0886: Unknown result type (might be due to invalid IL or missing references)
		//IL_088f: Expected O, but got Unknown
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Expected O, but got Unknown
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Expected O, but got Unknown
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b0: Expected O, but got Unknown
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Expected O, but got Unknown
		//IL_08bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c6: Expected O, but got Unknown
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Expected O, but got Unknown
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Expected O, but got Unknown
		//IL_08de: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e7: Expected O, but got Unknown
		//IL_08e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Expected O, but got Unknown
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Expected O, but got Unknown
		//IL_08ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0908: Expected O, but got Unknown
		//IL_090a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Expected O, but got Unknown
		//IL_0915: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Expected O, but got Unknown
		//IL_0920: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Expected O, but got Unknown
		//IL_092b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0934: Expected O, but got Unknown
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Expected O, but got Unknown
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Expected O, but got Unknown
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0955: Expected O, but got Unknown
		//IL_0957: Unknown result type (might be due to invalid IL or missing references)
		//IL_0960: Expected O, but got Unknown
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Expected O, but got Unknown
		//IL_096d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0976: Expected O, but got Unknown
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Expected O, but got Unknown
		//IL_0983: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Expected O, but got Unknown
		//IL_098e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0997: Expected O, but got Unknown
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Expected O, but got Unknown
		//IL_09a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ad: Expected O, but got Unknown
		//IL_09af: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b8: Expected O, but got Unknown
		//IL_09ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c3: Expected O, but got Unknown
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ce: Expected O, but got Unknown
		//IL_09d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d9: Expected O, but got Unknown
		//IL_09db: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e4: Expected O, but got Unknown
		//IL_09e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Expected O, but got Unknown
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Expected O, but got Unknown
		//IL_09fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a05: Expected O, but got Unknown
		//IL_0a07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Expected O, but got Unknown
		//IL_0a12: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1b: Expected O, but got Unknown
		//IL_0a1d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Expected O, but got Unknown
		//IL_0a28: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a31: Expected O, but got Unknown
		//IL_0a33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Expected O, but got Unknown
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a47: Expected O, but got Unknown
		//IL_0a49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a52: Expected O, but got Unknown
		//IL_0a54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Expected O, but got Unknown
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a68: Expected O, but got Unknown
		//IL_0a6a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Expected O, but got Unknown
		//IL_0a75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Expected O, but got Unknown
		//IL_0a80: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a89: Expected O, but got Unknown
		//IL_0a8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a94: Expected O, but got Unknown
		//IL_0a96: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a9f: Expected O, but got Unknown
		//IL_0aa1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aaa: Expected O, but got Unknown
		//IL_0aac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab5: Expected O, but got Unknown
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac0: Expected O, but got Unknown
		//IL_0ac2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acb: Expected O, but got Unknown
		//IL_0acd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad6: Expected O, but got Unknown
		//IL_0ad8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae1: Expected O, but got Unknown
		//IL_0ae3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aec: Expected O, but got Unknown
		//IL_0aee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af7: Expected O, but got Unknown
		//IL_0af9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b02: Expected O, but got Unknown
		//IL_0b04: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Expected O, but got Unknown
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b18: Expected O, but got Unknown
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b23: Expected O, but got Unknown
		//IL_0b25: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Expected O, but got Unknown
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b39: Expected O, but got Unknown
		//IL_0b3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Expected O, but got Unknown
		//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4f: Expected O, but got Unknown
		//IL_0b51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5a: Expected O, but got Unknown
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b65: Expected O, but got Unknown
		//IL_0b67: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b70: Expected O, but got Unknown
		//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7b: Expected O, but got Unknown
		//IL_0b7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b86: Expected O, but got Unknown
		//IL_0b88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b91: Expected O, but got Unknown
		//IL_0b93: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9c: Expected O, but got Unknown
		//IL_0b9e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba7: Expected O, but got Unknown
		//IL_0ba9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb2: Expected O, but got Unknown
		//IL_0bb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbd: Expected O, but got Unknown
		//IL_0bbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc8: Expected O, but got Unknown
		//IL_0bca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd3: Expected O, but got Unknown
		//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Expected O, but got Unknown
		//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be9: Expected O, but got Unknown
		//IL_0beb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Expected O, but got Unknown
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bff: Expected O, but got Unknown
		//IL_0c01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0a: Expected O, but got Unknown
		//IL_0c0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Expected O, but got Unknown
		//IL_0c17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c20: Expected O, but got Unknown
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2b: Expected O, but got Unknown
		//IL_0c2d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c36: Expected O, but got Unknown
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c41: Expected O, but got Unknown
		//IL_0c43: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4c: Expected O, but got Unknown
		//IL_0c4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c57: Expected O, but got Unknown
		//IL_0c59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c62: Expected O, but got Unknown
		//IL_0c64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6d: Expected O, but got Unknown
		//IL_0c6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c78: Expected O, but got Unknown
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c83: Expected O, but got Unknown
		//IL_0c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c8e: Expected O, but got Unknown
		//IL_0c90: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c99: Expected O, but got Unknown
		//IL_0c9b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca4: Expected O, but got Unknown
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0caf: Expected O, but got Unknown
		//IL_0cb1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cba: Expected O, but got Unknown
		//IL_0cbc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Expected O, but got Unknown
		//IL_0cc7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd0: Expected O, but got Unknown
		//IL_0cd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cdb: Expected O, but got Unknown
		//IL_0cdd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce6: Expected O, but got Unknown
		//IL_0ce8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Expected O, but got Unknown
		//IL_0cf3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfc: Expected O, but got Unknown
		//IL_0cfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d07: Expected O, but got Unknown
		//IL_0d09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d12: Expected O, but got Unknown
		//IL_0d14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d1d: Expected O, but got Unknown
		//IL_0d1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d28: Expected O, but got Unknown
		//IL_0d2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d33: Expected O, but got Unknown
		//IL_0d35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3e: Expected O, but got Unknown
		//IL_0d40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d49: Expected O, but got Unknown
		//IL_0d4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d54: Expected O, but got Unknown
		//IL_0d56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d5f: Expected O, but got Unknown
		//IL_0d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d6a: Expected O, but got Unknown
		//IL_0d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d75: Expected O, but got Unknown
		//IL_0d77: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d80: Expected O, but got Unknown
		//IL_0d82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8b: Expected O, but got Unknown
		//IL_0d8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d96: Expected O, but got Unknown
		//IL_0d98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da1: Expected O, but got Unknown
		//IL_0da3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dac: Expected O, but got Unknown
		//IL_0dae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db7: Expected O, but got Unknown
		//IL_0db9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dc2: Expected O, but got Unknown
		//IL_0dc4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcd: Expected O, but got Unknown
		//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd8: Expected O, but got Unknown
		//IL_0dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de3: Expected O, but got Unknown
		//IL_0de5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dee: Expected O, but got Unknown
		//IL_0df0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df9: Expected O, but got Unknown
		//IL_0dfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e04: Expected O, but got Unknown
		//IL_0e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e0f: Expected O, but got Unknown
		//IL_0e11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e1a: Expected O, but got Unknown
		//IL_0e1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e25: Expected O, but got Unknown
		//IL_0e27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e30: Expected O, but got Unknown
		//IL_0e32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3b: Expected O, but got Unknown
		//IL_0e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e46: Expected O, but got Unknown
		//IL_0e48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e51: Expected O, but got Unknown
		//IL_0e53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5c: Expected O, but got Unknown
		//IL_0e5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e67: Expected O, but got Unknown
		//IL_0e69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e72: Expected O, but got Unknown
		//IL_0e74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7d: Expected O, but got Unknown
		//IL_0e7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e88: Expected O, but got Unknown
		//IL_0e8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e93: Expected O, but got Unknown
		//IL_0e95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e9e: Expected O, but got Unknown
		//IL_0ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea9: Expected O, but got Unknown
		//IL_0eab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb4: Expected O, but got Unknown
		//IL_0eb6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebf: Expected O, but got Unknown
		//IL_0ec1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eca: Expected O, but got Unknown
		//IL_0ecc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ed5: Expected O, but got Unknown
		//IL_0ed7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee0: Expected O, but got Unknown
		//IL_0ee2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eeb: Expected O, but got Unknown
		//IL_0eed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef6: Expected O, but got Unknown
		//IL_0ef8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f01: Expected O, but got Unknown
		//IL_0f03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0c: Expected O, but got Unknown
		//IL_0f0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f17: Expected O, but got Unknown
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f22: Expected O, but got Unknown
		//IL_0f24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f2d: Expected O, but got Unknown
		//IL_0f2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f38: Expected O, but got Unknown
		//IL_0f3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f43: Expected O, but got Unknown
		//IL_0f45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4e: Expected O, but got Unknown
		//IL_0f50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f59: Expected O, but got Unknown
		//IL_0f5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f64: Expected O, but got Unknown
		//IL_0f66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6f: Expected O, but got Unknown
		//IL_0f71: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7a: Expected O, but got Unknown
		//IL_0f7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f85: Expected O, but got Unknown
		//IL_0f87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f90: Expected O, but got Unknown
		//IL_0f92: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f9b: Expected O, but got Unknown
		//IL_0f9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa6: Expected O, but got Unknown
		//IL_0fa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fb1: Expected O, but got Unknown
		//IL_0fb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fbc: Expected O, but got Unknown
		//IL_0fbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fc7: Expected O, but got Unknown
		//IL_0fc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fd2: Expected O, but got Unknown
		//IL_0fd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fdd: Expected O, but got Unknown
		//IL_0fdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fe8: Expected O, but got Unknown
		//IL_0fea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ff3: Expected O, but got Unknown
		//IL_0ff5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ffe: Expected O, but got Unknown
		//IL_1000: Unknown result type (might be due to invalid IL or missing references)
		//IL_1009: Expected O, but got Unknown
		//IL_100b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1014: Expected O, but got Unknown
		//IL_1016: Unknown result type (might be due to invalid IL or missing references)
		//IL_101f: Expected O, but got Unknown
		//IL_1021: Unknown result type (might be due to invalid IL or missing references)
		//IL_102a: Expected O, but got Unknown
		//IL_102c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1035: Expected O, but got Unknown
		//IL_1037: Unknown result type (might be due to invalid IL or missing references)
		//IL_1040: Expected O, but got Unknown
		//IL_1042: Unknown result type (might be due to invalid IL or missing references)
		//IL_104b: Expected O, but got Unknown
		//IL_104d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1056: Expected O, but got Unknown
		//IL_1058: Unknown result type (might be due to invalid IL or missing references)
		//IL_1061: Expected O, but got Unknown
		//IL_1063: Unknown result type (might be due to invalid IL or missing references)
		//IL_106c: Expected O, but got Unknown
		//IL_106e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1077: Expected O, but got Unknown
		//IL_1079: Unknown result type (might be due to invalid IL or missing references)
		//IL_1082: Expected O, but got Unknown
		//IL_1084: Unknown result type (might be due to invalid IL or missing references)
		//IL_108d: Expected O, but got Unknown
		//IL_108f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1098: Expected O, but got Unknown
		//IL_109a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a3: Expected O, but got Unknown
		//IL_10a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ae: Expected O, but got Unknown
		//IL_10b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b9: Expected O, but got Unknown
		//IL_10bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_10c4: Expected O, but got Unknown
		//IL_10c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cf: Expected O, but got Unknown
		//IL_10d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_10da: Expected O, but got Unknown
		//IL_10dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e5: Expected O, but got Unknown
		//IL_10e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10f0: Expected O, but got Unknown
		//IL_10f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10fb: Expected O, but got Unknown
		//IL_10fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1106: Expected O, but got Unknown
		//IL_1108: Unknown result type (might be due to invalid IL or missing references)
		//IL_1111: Expected O, but got Unknown
		//IL_1113: Unknown result type (might be due to invalid IL or missing references)
		//IL_111c: Expected O, but got Unknown
		//IL_111e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1127: Expected O, but got Unknown
		//IL_1129: Unknown result type (might be due to invalid IL or missing references)
		//IL_1132: Expected O, but got Unknown
		//IL_1134: Unknown result type (might be due to invalid IL or missing references)
		//IL_113d: Expected O, but got Unknown
		//IL_113f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1148: Expected O, but got Unknown
		//IL_114a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1153: Expected O, but got Unknown
		//IL_1155: Unknown result type (might be due to invalid IL or missing references)
		//IL_115e: Expected O, but got Unknown
		//IL_1160: Unknown result type (might be due to invalid IL or missing references)
		//IL_1169: Expected O, but got Unknown
		//IL_116b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1174: Expected O, but got Unknown
		//IL_1176: Unknown result type (might be due to invalid IL or missing references)
		//IL_117f: Expected O, but got Unknown
		//IL_1181: Unknown result type (might be due to invalid IL or missing references)
		//IL_118a: Expected O, but got Unknown
		//IL_118c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1195: Expected O, but got Unknown
		//IL_1197: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a0: Expected O, but got Unknown
		//IL_11a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ab: Expected O, but got Unknown
		//IL_11ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_11b6: Expected O, but got Unknown
		//IL_11b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11c1: Expected O, but got Unknown
		//IL_11c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_11cc: Expected O, but got Unknown
		//IL_11ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d7: Expected O, but got Unknown
		//IL_11d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11e2: Expected O, but got Unknown
		//IL_11e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ed: Expected O, but got Unknown
		//IL_11ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_11f8: Expected O, but got Unknown
		//IL_11fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1203: Expected O, but got Unknown
		//IL_1205: Unknown result type (might be due to invalid IL or missing references)
		//IL_120e: Expected O, but got Unknown
		//IL_1210: Unknown result type (might be due to invalid IL or missing references)
		//IL_1219: Expected O, but got Unknown
		//IL_121b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1224: Expected O, but got Unknown
		//IL_1226: Unknown result type (might be due to invalid IL or missing references)
		//IL_122f: Expected O, but got Unknown
		//IL_1231: Unknown result type (might be due to invalid IL or missing references)
		//IL_123a: Expected O, but got Unknown
		//IL_123c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1245: Expected O, but got Unknown
		//IL_1247: Unknown result type (might be due to invalid IL or missing references)
		//IL_1250: Expected O, but got Unknown
		//IL_1252: Unknown result type (might be due to invalid IL or missing references)
		//IL_125b: Expected O, but got Unknown
		//IL_125d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1266: Expected O, but got Unknown
		//IL_1268: Unknown result type (might be due to invalid IL or missing references)
		//IL_1271: Expected O, but got Unknown
		//IL_1273: Unknown result type (might be due to invalid IL or missing references)
		//IL_127c: Expected O, but got Unknown
		//IL_127e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1287: Expected O, but got Unknown
		//IL_1289: Unknown result type (might be due to invalid IL or missing references)
		//IL_1292: Expected O, but got Unknown
		//IL_1294: Unknown result type (might be due to invalid IL or missing references)
		//IL_129d: Expected O, but got Unknown
		//IL_129f: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a8: Expected O, but got Unknown
		//IL_12aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_12b3: Expected O, but got Unknown
		//IL_12b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_12be: Expected O, but got Unknown
		//IL_12c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_12c9: Expected O, but got Unknown
		//IL_12cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_12d4: Expected O, but got Unknown
		//IL_12d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_12df: Expected O, but got Unknown
		//IL_12e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_12ea: Expected O, but got Unknown
		//IL_12ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_12f5: Expected O, but got Unknown
		//IL_12f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1300: Expected O, but got Unknown
		//IL_1302: Unknown result type (might be due to invalid IL or missing references)
		//IL_130b: Expected O, but got Unknown
		//IL_130d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1316: Expected O, but got Unknown
		//IL_1318: Unknown result type (might be due to invalid IL or missing references)
		//IL_1321: Expected O, but got Unknown
		//IL_1323: Unknown result type (might be due to invalid IL or missing references)
		//IL_132c: Expected O, but got Unknown
		//IL_132e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1337: Expected O, but got Unknown
		//IL_1339: Unknown result type (might be due to invalid IL or missing references)
		//IL_1342: Expected O, but got Unknown
		//IL_1344: Unknown result type (might be due to invalid IL or missing references)
		//IL_134d: Expected O, but got Unknown
		//IL_134f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1358: Expected O, but got Unknown
		//IL_135a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1363: Expected O, but got Unknown
		//IL_1365: Unknown result type (might be due to invalid IL or missing references)
		//IL_136e: Expected O, but got Unknown
		//IL_1370: Unknown result type (might be due to invalid IL or missing references)
		//IL_1379: Expected O, but got Unknown
		//IL_137b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1384: Expected O, but got Unknown
		//IL_1386: Unknown result type (might be due to invalid IL or missing references)
		//IL_138f: Expected O, but got Unknown
		//IL_1391: Unknown result type (might be due to invalid IL or missing references)
		//IL_139a: Expected O, but got Unknown
		//IL_139c: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a5: Expected O, but got Unknown
		//IL_13a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_13b0: Expected O, but got Unknown
		//IL_13b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_13bb: Expected O, but got Unknown
		//IL_13bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_13c6: Expected O, but got Unknown
		//IL_13c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d1: Expected O, but got Unknown
		//IL_13d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_13dc: Expected O, but got Unknown
		//IL_13de: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e7: Expected O, but got Unknown
		//IL_13e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13f2: Expected O, but got Unknown
		//IL_13f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_13fd: Expected O, but got Unknown
		//IL_13ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_1408: Expected O, but got Unknown
		//IL_140a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1413: Expected O, but got Unknown
		//IL_1415: Unknown result type (might be due to invalid IL or missing references)
		//IL_141e: Expected O, but got Unknown
		//IL_1420: Unknown result type (might be due to invalid IL or missing references)
		//IL_1429: Expected O, but got Unknown
		//IL_142b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1434: Expected O, but got Unknown
		//IL_1436: Unknown result type (might be due to invalid IL or missing references)
		//IL_143f: Expected O, but got Unknown
		//IL_1441: Unknown result type (might be due to invalid IL or missing references)
		//IL_144a: Expected O, but got Unknown
		//IL_144c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1455: Expected O, but got Unknown
		//IL_1457: Unknown result type (might be due to invalid IL or missing references)
		//IL_1460: Expected O, but got Unknown
		//IL_1462: Unknown result type (might be due to invalid IL or missing references)
		//IL_146b: Expected O, but got Unknown
		//IL_146d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1476: Expected O, but got Unknown
		//IL_1478: Unknown result type (might be due to invalid IL or missing references)
		//IL_1481: Expected O, but got Unknown
		//IL_1483: Unknown result type (might be due to invalid IL or missing references)
		//IL_148c: Expected O, but got Unknown
		//IL_148e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1497: Expected O, but got Unknown
		//IL_1499: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a2: Expected O, but got Unknown
		//IL_14a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ad: Expected O, but got Unknown
		//IL_14af: Unknown result type (might be due to invalid IL or missing references)
		//IL_14b8: Expected O, but got Unknown
		//IL_14ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_14c3: Expected O, but got Unknown
		//IL_14c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ce: Expected O, but got Unknown
		//IL_14d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d9: Expected O, but got Unknown
		//IL_14db: Unknown result type (might be due to invalid IL or missing references)
		//IL_14e4: Expected O, but got Unknown
		//IL_14e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_14ef: Expected O, but got Unknown
		//IL_14f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_14fa: Expected O, but got Unknown
		//IL_14fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1505: Expected O, but got Unknown
		//IL_1507: Unknown result type (might be due to invalid IL or missing references)
		//IL_1510: Expected O, but got Unknown
		//IL_1512: Unknown result type (might be due to invalid IL or missing references)
		//IL_151b: Expected O, but got Unknown
		//IL_151d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1526: Expected O, but got Unknown
		//IL_1528: Unknown result type (might be due to invalid IL or missing references)
		//IL_1531: Expected O, but got Unknown
		//IL_1533: Unknown result type (might be due to invalid IL or missing references)
		//IL_153c: Expected O, but got Unknown
		//IL_153e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1547: Expected O, but got Unknown
		//IL_1549: Unknown result type (might be due to invalid IL or missing references)
		//IL_1552: Expected O, but got Unknown
		//IL_1554: Unknown result type (might be due to invalid IL or missing references)
		//IL_155d: Expected O, but got Unknown
		//IL_155f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1568: Expected O, but got Unknown
		//IL_156a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1573: Expected O, but got Unknown
		//IL_1575: Unknown result type (might be due to invalid IL or missing references)
		//IL_157e: Expected O, but got Unknown
		//IL_1580: Unknown result type (might be due to invalid IL or missing references)
		//IL_1589: Expected O, but got Unknown
		//IL_158b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1594: Expected O, but got Unknown
		//IL_1596: Unknown result type (might be due to invalid IL or missing references)
		//IL_159f: Expected O, but got Unknown
		//IL_15a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15aa: Expected O, but got Unknown
		//IL_15ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_15b5: Expected O, but got Unknown
		//IL_15b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c0: Expected O, but got Unknown
		//IL_15c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cb: Expected O, but got Unknown
		//IL_15cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_15d6: Expected O, but got Unknown
		//IL_15d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_15e1: Expected O, but got Unknown
		//IL_15e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ec: Expected O, but got Unknown
		//IL_15ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_15f7: Expected O, but got Unknown
		//IL_15f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1602: Expected O, but got Unknown
		//IL_1604: Unknown result type (might be due to invalid IL or missing references)
		//IL_160d: Expected O, but got Unknown
		//IL_160f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1618: Expected O, but got Unknown
		//IL_161a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1623: Expected O, but got Unknown
		//IL_1625: Unknown result type (might be due to invalid IL or missing references)
		//IL_162e: Expected O, but got Unknown
		//IL_1630: Unknown result type (might be due to invalid IL or missing references)
		//IL_1639: Expected O, but got Unknown
		//IL_163b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1644: Expected O, but got Unknown
		//IL_1646: Unknown result type (might be due to invalid IL or missing references)
		//IL_164f: Expected O, but got Unknown
		//IL_1651: Unknown result type (might be due to invalid IL or missing references)
		//IL_165a: Expected O, but got Unknown
		//IL_165c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1665: Expected O, but got Unknown
		//IL_1667: Unknown result type (might be due to invalid IL or missing references)
		//IL_1670: Expected O, but got Unknown
		//IL_1672: Unknown result type (might be due to invalid IL or missing references)
		//IL_167b: Expected O, but got Unknown
		//IL_167d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1686: Expected O, but got Unknown
		//IL_169e: Unknown result type (might be due to invalid IL or missing references)
		//IL_16a8: Expected O, but got Unknown
		//IL_16a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_16b3: Expected O, but got Unknown
		//IL_16b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_16be: Expected O, but got Unknown
		//IL_16bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c9: Expected O, but got Unknown
		//IL_16ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d4: Expected O, but got Unknown
		//IL_16d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_16df: Expected O, but got Unknown
		//IL_16e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16ea: Expected O, but got Unknown
		//IL_16eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_16f5: Expected O, but got Unknown
		//IL_16f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1700: Expected O, but got Unknown
		//IL_1701: Unknown result type (might be due to invalid IL or missing references)
		//IL_170b: Expected O, but got Unknown
		//IL_1712: Unknown result type (might be due to invalid IL or missing references)
		//IL_171c: Expected O, but got Unknown
		//IL_171d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1727: Expected O, but got Unknown
		//IL_1728: Unknown result type (might be due to invalid IL or missing references)
		//IL_1732: Expected O, but got Unknown
		//IL_1733: Unknown result type (might be due to invalid IL or missing references)
		//IL_173d: Expected O, but got Unknown
		//IL_173e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1748: Expected O, but got Unknown
		//IL_174f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1759: Expected O, but got Unknown
		//IL_1760: Unknown result type (might be due to invalid IL or missing references)
		//IL_176a: Expected O, but got Unknown
		//IL_1771: Unknown result type (might be due to invalid IL or missing references)
		//IL_177b: Expected O, but got Unknown
		//IL_177c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1786: Expected O, but got Unknown
		//IL_178d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1797: Expected O, but got Unknown
		//IL_179e: Unknown result type (might be due to invalid IL or missing references)
		//IL_17a8: Expected O, but got Unknown
		//IL_17a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_17b3: Expected O, but got Unknown
		//IL_17b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_17be: Expected O, but got Unknown
		//IL_17bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c9: Expected O, but got Unknown
		//IL_17ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d4: Expected O, but got Unknown
		//IL_17d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_17df: Expected O, but got Unknown
		//IL_17e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_17ea: Expected O, but got Unknown
		//IL_17eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_17f5: Expected O, but got Unknown
		//IL_17f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1800: Expected O, but got Unknown
		//IL_1801: Unknown result type (might be due to invalid IL or missing references)
		//IL_180b: Expected O, but got Unknown
		//IL_180c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1816: Expected O, but got Unknown
		//IL_1817: Unknown result type (might be due to invalid IL or missing references)
		//IL_1821: Expected O, but got Unknown
		//IL_1822: Unknown result type (might be due to invalid IL or missing references)
		//IL_182c: Expected O, but got Unknown
		//IL_182d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1837: Expected O, but got Unknown
		//IL_1838: Unknown result type (might be due to invalid IL or missing references)
		//IL_1842: Expected O, but got Unknown
		//IL_1843: Unknown result type (might be due to invalid IL or missing references)
		//IL_184d: Expected O, but got Unknown
		//IL_184e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1858: Expected O, but got Unknown
		//IL_1859: Unknown result type (might be due to invalid IL or missing references)
		//IL_1863: Expected O, but got Unknown
		//IL_1864: Unknown result type (might be due to invalid IL or missing references)
		//IL_186e: Expected O, but got Unknown
		//IL_186f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1879: Expected O, but got Unknown
		//IL_187a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1884: Expected O, but got Unknown
		//IL_1885: Unknown result type (might be due to invalid IL or missing references)
		//IL_188f: Expected O, but got Unknown
		//IL_1890: Unknown result type (might be due to invalid IL or missing references)
		//IL_189a: Expected O, but got Unknown
		//IL_189b: Unknown result type (might be due to invalid IL or missing references)
		//IL_18a5: Expected O, but got Unknown
		//IL_18a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_18b0: Expected O, but got Unknown
		//IL_18b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_18bb: Expected O, but got Unknown
		//IL_18bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_18c6: Expected O, but got Unknown
		//IL_18c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d1: Expected O, but got Unknown
		//IL_18d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_18dc: Expected O, but got Unknown
		//IL_18dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_18e7: Expected O, but got Unknown
		//IL_18e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_18f2: Expected O, but got Unknown
		//IL_18f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fd: Expected O, but got Unknown
		//IL_18fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1908: Expected O, but got Unknown
		//IL_1909: Unknown result type (might be due to invalid IL or missing references)
		//IL_1913: Expected O, but got Unknown
		//IL_1914: Unknown result type (might be due to invalid IL or missing references)
		//IL_191e: Expected O, but got Unknown
		//IL_191f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1929: Expected O, but got Unknown
		//IL_192a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1934: Expected O, but got Unknown
		//IL_1935: Unknown result type (might be due to invalid IL or missing references)
		//IL_193f: Expected O, but got Unknown
		//IL_1940: Unknown result type (might be due to invalid IL or missing references)
		//IL_194a: Expected O, but got Unknown
		//IL_194b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1955: Expected O, but got Unknown
		//IL_1956: Unknown result type (might be due to invalid IL or missing references)
		//IL_1960: Expected O, but got Unknown
		//IL_1961: Unknown result type (might be due to invalid IL or missing references)
		//IL_196b: Expected O, but got Unknown
		//IL_196c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1976: Expected O, but got Unknown
		//IL_1977: Unknown result type (might be due to invalid IL or missing references)
		//IL_1981: Expected O, but got Unknown
		//IL_1982: Unknown result type (might be due to invalid IL or missing references)
		//IL_198c: Expected O, but got Unknown
		//IL_198d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1997: Expected O, but got Unknown
		//IL_1998: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a2: Expected O, but got Unknown
		//IL_19a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ad: Expected O, but got Unknown
		//IL_19ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_19b8: Expected O, but got Unknown
		//IL_19b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c3: Expected O, but got Unknown
		//IL_19c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ce: Expected O, but got Unknown
		//IL_19cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_19d9: Expected O, but got Unknown
		//IL_19da: Unknown result type (might be due to invalid IL or missing references)
		//IL_19e4: Expected O, but got Unknown
		//IL_19e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ef: Expected O, but got Unknown
		//IL_19f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fa: Expected O, but got Unknown
		//IL_19fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a05: Expected O, but got Unknown
		//IL_1a06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a10: Expected O, but got Unknown
		//IL_1a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a1b: Expected O, but got Unknown
		//IL_1a1c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a26: Expected O, but got Unknown
		//IL_1a27: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a31: Expected O, but got Unknown
		//IL_1a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3c: Expected O, but got Unknown
		//IL_1a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a47: Expected O, but got Unknown
		//IL_1a48: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a52: Expected O, but got Unknown
		//IL_1a53: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5d: Expected O, but got Unknown
		//IL_1a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a68: Expected O, but got Unknown
		//IL_1a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a73: Expected O, but got Unknown
		//IL_1a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a7e: Expected O, but got Unknown
		//IL_1a7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a89: Expected O, but got Unknown
		//IL_1a8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a94: Expected O, but got Unknown
		//IL_1a95: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a9f: Expected O, but got Unknown
		//IL_1aa0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aaa: Expected O, but got Unknown
		//IL_1aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ab5: Expected O, but got Unknown
		//IL_1ab6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ac0: Expected O, but got Unknown
		//IL_1ac1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1acb: Expected O, but got Unknown
		//IL_1acc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ad6: Expected O, but got Unknown
		//IL_1ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ae1: Expected O, but got Unknown
		//IL_1ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aec: Expected O, but got Unknown
		//IL_1aed: Unknown result type (might be due to invalid IL or missing references)
		//IL_1af7: Expected O, but got Unknown
		//IL_1af8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b02: Expected O, but got Unknown
		//IL_1b03: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b0d: Expected O, but got Unknown
		//IL_1b0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b18: Expected O, but got Unknown
		//IL_1b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b23: Expected O, but got Unknown
		//IL_1b24: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b2e: Expected O, but got Unknown
		//IL_1b2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b39: Expected O, but got Unknown
		//IL_1b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b44: Expected O, but got Unknown
		//IL_1b45: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4f: Expected O, but got Unknown
		//IL_1b50: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5a: Expected O, but got Unknown
		//IL_1b5b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b65: Expected O, but got Unknown
		//IL_1b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b70: Expected O, but got Unknown
		//IL_1b71: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b7b: Expected O, but got Unknown
		//IL_1b7c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b86: Expected O, but got Unknown
		//IL_1b87: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b91: Expected O, but got Unknown
		//IL_1b92: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9c: Expected O, but got Unknown
		//IL_1b9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ba7: Expected O, but got Unknown
		//IL_1ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bb2: Expected O, but got Unknown
		//IL_1bb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bbd: Expected O, but got Unknown
		//IL_1bbe: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bc8: Expected O, but got Unknown
		//IL_1bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd3: Expected O, but got Unknown
		//IL_1bd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bde: Expected O, but got Unknown
		//IL_1bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be9: Expected O, but got Unknown
		//IL_1bea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf4: Expected O, but got Unknown
		//IL_1bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bff: Expected O, but got Unknown
		//IL_1c00: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0a: Expected O, but got Unknown
		//IL_1c0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c15: Expected O, but got Unknown
		//IL_1c16: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c20: Expected O, but got Unknown
		//IL_1c21: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c2b: Expected O, but got Unknown
		//IL_1c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c36: Expected O, but got Unknown
		//IL_1c37: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c41: Expected O, but got Unknown
		//IL_1c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c4c: Expected O, but got Unknown
		//IL_1c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c57: Expected O, but got Unknown
		//IL_1c58: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c62: Expected O, but got Unknown
		//IL_1c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c6d: Expected O, but got Unknown
		//IL_1c6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c78: Expected O, but got Unknown
		//IL_1c79: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c83: Expected O, but got Unknown
		//IL_1c84: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c8e: Expected O, but got Unknown
		//IL_1c8f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c99: Expected O, but got Unknown
		//IL_1c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca4: Expected O, but got Unknown
		//IL_1ca5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1caf: Expected O, but got Unknown
		//IL_1cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cba: Expected O, but got Unknown
		//IL_1cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc5: Expected O, but got Unknown
		//IL_1cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd0: Expected O, but got Unknown
		//IL_1cd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cdb: Expected O, but got Unknown
		//IL_1cdc: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ce6: Expected O, but got Unknown
		//IL_1ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cf1: Expected O, but got Unknown
		//IL_1cf2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cfc: Expected O, but got Unknown
		//IL_1cfd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d07: Expected O, but got Unknown
		//IL_1d08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d12: Expected O, but got Unknown
		//IL_1d13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d1d: Expected O, but got Unknown
		//IL_1d1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d28: Expected O, but got Unknown
		//IL_1d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d33: Expected O, but got Unknown
		//IL_1d34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d3e: Expected O, but got Unknown
		//IL_1d3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d49: Expected O, but got Unknown
		//IL_1d4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d54: Expected O, but got Unknown
		//IL_1d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d5f: Expected O, but got Unknown
		//IL_1d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d6a: Expected O, but got Unknown
		//IL_1d6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d75: Expected O, but got Unknown
		//IL_1d76: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d80: Expected O, but got Unknown
		//IL_1d81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d8b: Expected O, but got Unknown
		//IL_1d8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d96: Expected O, but got Unknown
		//IL_1d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da1: Expected O, but got Unknown
		//IL_1da2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dac: Expected O, but got Unknown
		//IL_1dad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1db7: Expected O, but got Unknown
		//IL_1db8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc2: Expected O, but got Unknown
		//IL_1dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dcd: Expected O, but got Unknown
		//IL_1dce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dd8: Expected O, but got Unknown
		//IL_1dd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1de3: Expected O, but got Unknown
		//IL_1de4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dee: Expected O, but got Unknown
		//IL_1def: Unknown result type (might be due to invalid IL or missing references)
		//IL_1df9: Expected O, but got Unknown
		//IL_1dfa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e04: Expected O, but got Unknown
		//IL_1e05: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e0f: Expected O, but got Unknown
		//IL_1e10: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e1a: Expected O, but got Unknown
		//IL_1e1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e25: Expected O, but got Unknown
		//IL_1e26: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e30: Expected O, but got Unknown
		//IL_1e31: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3b: Expected O, but got Unknown
		//IL_1e3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e46: Expected O, but got Unknown
		//IL_1e47: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e51: Expected O, but got Unknown
		//IL_1e52: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e5c: Expected O, but got Unknown
		//IL_1e5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e67: Expected O, but got Unknown
		//IL_1e68: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e72: Expected O, but got Unknown
		//IL_1e73: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e7d: Expected O, but got Unknown
		//IL_1e7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e88: Expected O, but got Unknown
		//IL_1e89: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e93: Expected O, but got Unknown
		//IL_1e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e9e: Expected O, but got Unknown
		//IL_1e9f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ea9: Expected O, but got Unknown
		//IL_1eaa: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eb4: Expected O, but got Unknown
		//IL_1eb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ebf: Expected O, but got Unknown
		//IL_1ec0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1eca: Expected O, but got Unknown
		//IL_1ecb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ed5: Expected O, but got Unknown
		//IL_1ed6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ee0: Expected O, but got Unknown
		//IL_1ee7: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ef1: Expected O, but got Unknown
		//IL_1ef2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1efc: Expected O, but got Unknown
		//IL_1efd: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f07: Expected O, but got Unknown
		//IL_1f08: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f12: Expected O, but got Unknown
		//IL_1f13: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f1d: Expected O, but got Unknown
		//IL_1f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f28: Expected O, but got Unknown
		//IL_1f29: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f33: Expected O, but got Unknown
		//IL_1f34: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3e: Expected O, but got Unknown
		//IL_1f3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f49: Expected O, but got Unknown
		//IL_1f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f54: Expected O, but got Unknown
		//IL_1f55: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5f: Expected O, but got Unknown
		//IL_1f60: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f6a: Expected O, but got Unknown
		//IL_1f6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f75: Expected O, but got Unknown
		//IL_1f76: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f80: Expected O, but got Unknown
		//IL_1f81: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f8b: Expected O, but got Unknown
		//IL_1f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f96: Expected O, but got Unknown
		//IL_1f97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fa1: Expected O, but got Unknown
		//IL_1fa2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fac: Expected O, but got Unknown
		//IL_1fad: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fb7: Expected O, but got Unknown
		//IL_1fb8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fc2: Expected O, but got Unknown
		//IL_1fc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fcd: Expected O, but got Unknown
		//IL_1fce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fd8: Expected O, but got Unknown
		//IL_1fd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe3: Expected O, but got Unknown
		//IL_1fe4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fee: Expected O, but got Unknown
		//IL_1fef: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ff9: Expected O, but got Unknown
		//IL_1ffa: Unknown result type (might be due to invalid IL or missing references)
		//IL_2004: Expected O, but got Unknown
		//IL_2005: Unknown result type (might be due to invalid IL or missing references)
		//IL_200f: Expected O, but got Unknown
		//IL_2010: Unknown result type (might be due to invalid IL or missing references)
		//IL_201a: Expected O, but got Unknown
		//IL_201b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2025: Expected O, but got Unknown
		//IL_2026: Unknown result type (might be due to invalid IL or missing references)
		//IL_2030: Expected O, but got Unknown
		//IL_2031: Unknown result type (might be due to invalid IL or missing references)
		//IL_203b: Expected O, but got Unknown
		//IL_203c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2046: Expected O, but got Unknown
		//IL_2047: Unknown result type (might be due to invalid IL or missing references)
		//IL_2051: Expected O, but got Unknown
		//IL_2052: Unknown result type (might be due to invalid IL or missing references)
		//IL_205c: Expected O, but got Unknown
		//IL_205d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2067: Expected O, but got Unknown
		//IL_2068: Unknown result type (might be due to invalid IL or missing references)
		//IL_2072: Expected O, but got Unknown
		//IL_2073: Unknown result type (might be due to invalid IL or missing references)
		//IL_207d: Expected O, but got Unknown
		//IL_207e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2088: Expected O, but got Unknown
		//IL_2089: Unknown result type (might be due to invalid IL or missing references)
		//IL_2093: Expected O, but got Unknown
		//IL_2094: Unknown result type (might be due to invalid IL or missing references)
		//IL_209e: Expected O, but got Unknown
		//IL_209f: Unknown result type (might be due to invalid IL or missing references)
		//IL_20a9: Expected O, but got Unknown
		//IL_20aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_20b4: Expected O, but got Unknown
		//IL_20b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_20bf: Expected O, but got Unknown
		//IL_20c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ca: Expected O, but got Unknown
		//IL_20cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d5: Expected O, but got Unknown
		//IL_20d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_20e0: Expected O, but got Unknown
		//IL_20e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_20eb: Expected O, but got Unknown
		//IL_20ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f6: Expected O, but got Unknown
		//IL_20f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_2101: Expected O, but got Unknown
		//IL_2102: Unknown result type (might be due to invalid IL or missing references)
		//IL_210c: Expected O, but got Unknown
		//IL_210d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2117: Expected O, but got Unknown
		//IL_2118: Unknown result type (might be due to invalid IL or missing references)
		//IL_2122: Expected O, but got Unknown
		//IL_2123: Unknown result type (might be due to invalid IL or missing references)
		//IL_212d: Expected O, but got Unknown
		//IL_212e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2138: Expected O, but got Unknown
		//IL_2139: Unknown result type (might be due to invalid IL or missing references)
		//IL_2143: Expected O, but got Unknown
		//IL_2144: Unknown result type (might be due to invalid IL or missing references)
		//IL_214e: Expected O, but got Unknown
		//IL_214f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2159: Expected O, but got Unknown
		//IL_215a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2164: Expected O, but got Unknown
		//IL_2165: Unknown result type (might be due to invalid IL or missing references)
		//IL_216f: Expected O, but got Unknown
		//IL_2170: Unknown result type (might be due to invalid IL or missing references)
		//IL_217a: Expected O, but got Unknown
		//IL_217b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2185: Expected O, but got Unknown
		//IL_2186: Unknown result type (might be due to invalid IL or missing references)
		//IL_2190: Expected O, but got Unknown
		//IL_2191: Unknown result type (might be due to invalid IL or missing references)
		//IL_219b: Expected O, but got Unknown
		//IL_219c: Unknown result type (might be due to invalid IL or missing references)
		//IL_21a6: Expected O, but got Unknown
		//IL_21a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_21b1: Expected O, but got Unknown
		//IL_21b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_21bc: Expected O, but got Unknown
		//IL_21bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_21c7: Expected O, but got Unknown
		//IL_21c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_21d2: Expected O, but got Unknown
		//IL_21d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_21dd: Expected O, but got Unknown
		//IL_21de: Unknown result type (might be due to invalid IL or missing references)
		//IL_21e8: Expected O, but got Unknown
		//IL_21e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_21f3: Expected O, but got Unknown
		//IL_21f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_21fe: Expected O, but got Unknown
		//IL_21ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_2209: Expected O, but got Unknown
		//IL_220a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2214: Expected O, but got Unknown
		//IL_2215: Unknown result type (might be due to invalid IL or missing references)
		//IL_221f: Expected O, but got Unknown
		//IL_2220: Unknown result type (might be due to invalid IL or missing references)
		//IL_222a: Expected O, but got Unknown
		//IL_222b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2235: Expected O, but got Unknown
		//IL_2236: Unknown result type (might be due to invalid IL or missing references)
		//IL_2240: Expected O, but got Unknown
		//IL_2241: Unknown result type (might be due to invalid IL or missing references)
		//IL_224b: Expected O, but got Unknown
		//IL_224c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2256: Expected O, but got Unknown
		//IL_2257: Unknown result type (might be due to invalid IL or missing references)
		//IL_2261: Expected O, but got Unknown
		//IL_2262: Unknown result type (might be due to invalid IL or missing references)
		//IL_226c: Expected O, but got Unknown
		//IL_226d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2277: Expected O, but got Unknown
		//IL_2278: Unknown result type (might be due to invalid IL or missing references)
		//IL_2282: Expected O, but got Unknown
		//IL_2283: Unknown result type (might be due to invalid IL or missing references)
		//IL_228d: Expected O, but got Unknown
		//IL_228e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2298: Expected O, but got Unknown
		//IL_2299: Unknown result type (might be due to invalid IL or missing references)
		//IL_22a3: Expected O, but got Unknown
		//IL_22a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_22ae: Expected O, but got Unknown
		//IL_22af: Unknown result type (might be due to invalid IL or missing references)
		//IL_22b9: Expected O, but got Unknown
		//IL_22ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c4: Expected O, but got Unknown
		//IL_22c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_22cf: Expected O, but got Unknown
		//IL_22d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22da: Expected O, but got Unknown
		//IL_22db: Unknown result type (might be due to invalid IL or missing references)
		//IL_22e5: Expected O, but got Unknown
		//IL_22e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_22f0: Expected O, but got Unknown
		//IL_22f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_22fb: Expected O, but got Unknown
		//IL_22fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_2306: Expected O, but got Unknown
		//IL_2307: Unknown result type (might be due to invalid IL or missing references)
		//IL_2311: Expected O, but got Unknown
		//IL_2312: Unknown result type (might be due to invalid IL or missing references)
		//IL_231c: Expected O, but got Unknown
		//IL_231d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2327: Expected O, but got Unknown
		//IL_2328: Unknown result type (might be due to invalid IL or missing references)
		//IL_2332: Expected O, but got Unknown
		//IL_2333: Unknown result type (might be due to invalid IL or missing references)
		//IL_233d: Expected O, but got Unknown
		//IL_233e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2348: Expected O, but got Unknown
		//IL_2349: Unknown result type (might be due to invalid IL or missing references)
		//IL_2353: Expected O, but got Unknown
		//IL_2354: Unknown result type (might be due to invalid IL or missing references)
		//IL_235e: Expected O, but got Unknown
		//IL_235f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2369: Expected O, but got Unknown
		//IL_236a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2374: Expected O, but got Unknown
		//IL_2375: Unknown result type (might be due to invalid IL or missing references)
		//IL_237f: Expected O, but got Unknown
		//IL_2380: Unknown result type (might be due to invalid IL or missing references)
		//IL_238a: Expected O, but got Unknown
		//IL_238b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2395: Expected O, but got Unknown
		//IL_2396: Unknown result type (might be due to invalid IL or missing references)
		//IL_23a0: Expected O, but got Unknown
		//IL_23a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ab: Expected O, but got Unknown
		//IL_23ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_23b6: Expected O, but got Unknown
		//IL_23b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_23c1: Expected O, but got Unknown
		//IL_23c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_23cc: Expected O, but got Unknown
		//IL_23cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_23d7: Expected O, but got Unknown
		//IL_23d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_23e2: Expected O, but got Unknown
		//IL_23e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_23ed: Expected O, but got Unknown
		//IL_23ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f8: Expected O, but got Unknown
		//IL_23f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_2403: Expected O, but got Unknown
		//IL_2404: Unknown result type (might be due to invalid IL or missing references)
		//IL_240e: Expected O, but got Unknown
		//IL_240f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2419: Expected O, but got Unknown
		//IL_241a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2424: Expected O, but got Unknown
		//IL_2425: Unknown result type (might be due to invalid IL or missing references)
		//IL_242f: Expected O, but got Unknown
		//IL_2430: Unknown result type (might be due to invalid IL or missing references)
		//IL_243a: Expected O, but got Unknown
		//IL_243b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2445: Expected O, but got Unknown
		//IL_2446: Unknown result type (might be due to invalid IL or missing references)
		//IL_2450: Expected O, but got Unknown
		//IL_2451: Unknown result type (might be due to invalid IL or missing references)
		//IL_245b: Expected O, but got Unknown
		//IL_245c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2466: Expected O, but got Unknown
		//IL_2467: Unknown result type (might be due to invalid IL or missing references)
		//IL_2471: Expected O, but got Unknown
		//IL_2472: Unknown result type (might be due to invalid IL or missing references)
		//IL_247c: Expected O, but got Unknown
		//IL_247d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2487: Expected O, but got Unknown
		//IL_2488: Unknown result type (might be due to invalid IL or missing references)
		//IL_2492: Expected O, but got Unknown
		//IL_2493: Unknown result type (might be due to invalid IL or missing references)
		//IL_249d: Expected O, but got Unknown
		//IL_249e: Unknown result type (might be due to invalid IL or missing references)
		//IL_24a8: Expected O, but got Unknown
		//IL_24a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_24b3: Expected O, but got Unknown
		//IL_24b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_24be: Expected O, but got Unknown
		//IL_24bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_24c9: Expected O, but got Unknown
		//IL_24ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_24d4: Expected O, but got Unknown
		//IL_24d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_24df: Expected O, but got Unknown
		//IL_24e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_24ea: Expected O, but got Unknown
		//IL_24eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_24f5: Expected O, but got Unknown
		//IL_24f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_2500: Expected O, but got Unknown
		//IL_2501: Unknown result type (might be due to invalid IL or missing references)
		//IL_250b: Expected O, but got Unknown
		//IL_250c: Unknown result type (might be due to invalid IL or missing references)
		//IL_2516: Expected O, but got Unknown
		//IL_2517: Unknown result type (might be due to invalid IL or missing references)
		//IL_2521: Expected O, but got Unknown
		//IL_2522: Unknown result type (might be due to invalid IL or missing references)
		//IL_252c: Expected O, but got Unknown
		//IL_252d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2537: Expected O, but got Unknown
		//IL_253e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2548: Expected O, but got Unknown
		//IL_2549: Unknown result type (might be due to invalid IL or missing references)
		//IL_2553: Expected O, but got Unknown
		//IL_2554: Unknown result type (might be due to invalid IL or missing references)
		//IL_255e: Expected O, but got Unknown
		//IL_255f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2569: Expected O, but got Unknown
		//IL_256a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2574: Expected O, but got Unknown
		//IL_2575: Unknown result type (might be due to invalid IL or missing references)
		//IL_257f: Expected O, but got Unknown
		//IL_2580: Unknown result type (might be due to invalid IL or missing references)
		//IL_258a: Expected O, but got Unknown
		//IL_258b: Unknown result type (might be due to invalid IL or missing references)
		//IL_2595: Expected O, but got Unknown
		//IL_2596: Unknown result type (might be due to invalid IL or missing references)
		//IL_25a0: Expected O, but got Unknown
		//IL_25a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ab: Expected O, but got Unknown
		//IL_25ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_25b6: Expected O, but got Unknown
		//IL_25b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_25c1: Expected O, but got Unknown
		//IL_25c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cc: Expected O, but got Unknown
		//IL_25cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_25d7: Expected O, but got Unknown
		//IL_25d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_25e2: Expected O, but got Unknown
		//IL_25e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_25ed: Expected O, but got Unknown
		//IL_25ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f8: Expected O, but got Unknown
		//IL_2a01: Unknown result type (might be due to invalid IL or missing references)
		//IL_2a07: Expected O, but got Unknown
		//IL_2b02: Unknown result type (might be due to invalid IL or missing references)
		//IL_2b08: Expected O, but got Unknown
		//IL_3b2f: Unknown result type (might be due to invalid IL or missing references)
		//IL_3b39: Expected O, but got Unknown
		//IL_3bf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_3bfb: Expected O, but got Unknown
		//IL_3cb3: Unknown result type (might be due to invalid IL or missing references)
		//IL_3cbd: Expected O, but got Unknown
		//IL_3d78: Unknown result type (might be due to invalid IL or missing references)
		//IL_3d82: Expected O, but got Unknown
		//IL_3e3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_3e47: Expected O, but got Unknown
		//IL_406a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4074: Expected O, but got Unknown
		//IL_4122: Unknown result type (might be due to invalid IL or missing references)
		//IL_412c: Expected O, but got Unknown
		//IL_41eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_41f5: Expected O, but got Unknown
		//IL_42b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_42be: Expected O, but got Unknown
		//IL_438a: Unknown result type (might be due to invalid IL or missing references)
		//IL_4394: Expected O, but got Unknown
		//IL_4510: Unknown result type (might be due to invalid IL or missing references)
		//IL_451a: Expected O, but got Unknown
		//IL_45d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_45e0: Expected O, but got Unknown
		//IL_46a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_46b3: Expected O, but got Unknown
		//IL_4790: Unknown result type (might be due to invalid IL or missing references)
		//IL_4796: Expected O, but got Unknown
		//IL_4876: Unknown result type (might be due to invalid IL or missing references)
		//IL_487c: Expected O, but got Unknown
		//IL_4a45: Unknown result type (might be due to invalid IL or missing references)
		//IL_4a4f: Expected O, but got Unknown
		//IL_4b0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_4b15: Expected O, but got Unknown
		//IL_4bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_4bdb: Expected O, but got Unknown
		//IL_7628: Unknown result type (might be due to invalid IL or missing references)
		//IL_762e: Expected O, but got Unknown
		//IL_793f: Unknown result type (might be due to invalid IL or missing references)
		//IL_7949: Expected O, but got Unknown
		//IL_7a30: Unknown result type (might be due to invalid IL or missing references)
		//IL_7a3a: Expected O, but got Unknown
		//IL_80b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_80b8: Expected O, but got Unknown
		//IL_820e: Unknown result type (might be due to invalid IL or missing references)
		//IL_8214: Expected O, but got Unknown
		//IL_836a: Unknown result type (might be due to invalid IL or missing references)
		//IL_8370: Expected O, but got Unknown
		//IL_84c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_84cc: Expected O, but got Unknown
		//IL_8622: Unknown result type (might be due to invalid IL or missing references)
		//IL_8628: Expected O, but got Unknown
		//IL_8759: Unknown result type (might be due to invalid IL or missing references)
		//IL_875f: Expected O, but got Unknown
		//IL_8893: Unknown result type (might be due to invalid IL or missing references)
		//IL_8899: Expected O, but got Unknown
		//IL_bb98: Unknown result type (might be due to invalid IL or missing references)
		//IL_bba2: Expected O, but got Unknown
		//IL_11cc1: Unknown result type (might be due to invalid IL or missing references)
		//IL_11ccb: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		Code128Generator symbology = new Code128Generator();
		SplashScreenManager val = new SplashScreenManager((System.Windows.Forms.Form)this, (System.Type)null, true, true);
		XRDesignPanelListener val2 = new XRDesignPanelListener();
		XRDesignPanelListener val3 = new XRDesignPanelListener();
		XRDesignPanelListener val4 = new XRDesignPanelListener();
		XRDesignPanelListener val5 = new XRDesignPanelListener();
		XRDesignPanelListener val6 = new XRDesignPanelListener();
		XRDesignPanelListener val7 = new XRDesignPanelListener();
		XRDesignPanelListener val8 = new XRDesignPanelListener();
		XRDesignPanelListener val9 = new XRDesignPanelListener();
		SuperToolTip val10 = new SuperToolTip();
		ToolTipTitleItem val11 = new ToolTipTitleItem();
		ToolTipItem val12 = new ToolTipItem();
		SuperToolTip val13 = new SuperToolTip();
		ToolTipTitleItem val14 = new ToolTipTitleItem();
		ToolTipItem val15 = new ToolTipItem();
		SuperToolTip val16 = new SuperToolTip();
		ToolTipTitleItem val17 = new ToolTipTitleItem();
		ToolTipItem val18 = new ToolTipItem();
		SuperToolTip val19 = new SuperToolTip();
		ToolTipTitleItem val20 = new ToolTipTitleItem();
		ToolTipItem val21 = new ToolTipItem();
		SuperToolTip val22 = new SuperToolTip();
		ToolTipTitleItem val23 = new ToolTipTitleItem();
		ToolTipItem val24 = new ToolTipItem();
		SuperToolTip val25 = new SuperToolTip();
		ToolTipTitleItem val26 = new ToolTipTitleItem();
		ToolTipItem val27 = new ToolTipItem();
		SuperToolTip val28 = new SuperToolTip();
		ToolTipTitleItem val29 = new ToolTipTitleItem();
		ToolTipItem val30 = new ToolTipItem();
		SuperToolTip val31 = new SuperToolTip();
		ToolTipTitleItem val32 = new ToolTipTitleItem();
		ToolTipItem val33 = new ToolTipItem();
		SuperToolTip val34 = new SuperToolTip();
		ToolTipTitleItem val35 = new ToolTipTitleItem();
		ToolTipItem val36 = new ToolTipItem();
		SuperToolTip val37 = new SuperToolTip();
		ToolTipTitleItem val38 = new ToolTipTitleItem();
		ToolTipItem val39 = new ToolTipItem();
		SuperToolTip val40 = new SuperToolTip();
		ToolTipTitleItem val41 = new ToolTipTitleItem();
		ToolTipItem val42 = new ToolTipItem();
		SuperToolTip val43 = new SuperToolTip();
		ToolTipTitleItem val44 = new ToolTipTitleItem();
		ToolTipItem val45 = new ToolTipItem();
		SuperToolTip val46 = new SuperToolTip();
		ToolTipTitleItem val47 = new ToolTipTitleItem();
		ToolTipItem val48 = new ToolTipItem();
		SuperToolTip val49 = new SuperToolTip();
		ToolTipTitleItem val50 = new ToolTipTitleItem();
		ToolTipItem val51 = new ToolTipItem();
		SuperToolTip val52 = new SuperToolTip();
		ToolTipTitleItem val53 = new ToolTipTitleItem();
		ToolTipItem val54 = new ToolTipItem();
		SuperToolTip val55 = new SuperToolTip();
		ToolTipTitleItem val56 = new ToolTipTitleItem();
		ToolTipItem val57 = new ToolTipItem();
		SuperToolTip val58 = new SuperToolTip();
		ToolTipTitleItem val59 = new ToolTipTitleItem();
		ToolTipItem val60 = new ToolTipItem();
		SuperToolTip val61 = new SuperToolTip();
		ToolTipTitleItem val62 = new ToolTipTitleItem();
		ToolTipItem val63 = new ToolTipItem();
		SuperToolTip val64 = new SuperToolTip();
		ToolTipTitleItem val65 = new ToolTipTitleItem();
		ToolTipItem val66 = new ToolTipItem();
		SuperToolTip val67 = new SuperToolTip();
		ToolTipTitleItem val68 = new ToolTipTitleItem();
		ToolTipItem val69 = new ToolTipItem();
		SuperToolTip val70 = new SuperToolTip();
		ToolTipTitleItem val71 = new ToolTipTitleItem();
		ToolTipItem val72 = new ToolTipItem();
		SuperToolTip val73 = new SuperToolTip();
		ToolTipTitleItem val74 = new ToolTipTitleItem();
		ToolTipItem val75 = new ToolTipItem();
		SuperToolTip val76 = new SuperToolTip();
		ToolTipTitleItem val77 = new ToolTipTitleItem();
		ToolTipItem val78 = new ToolTipItem();
		SuperToolTip val79 = new SuperToolTip();
		ToolTipTitleItem val80 = new ToolTipTitleItem();
		ToolTipItem val81 = new ToolTipItem();
		SuperToolTip val82 = new SuperToolTip();
		ToolTipTitleItem val83 = new ToolTipTitleItem();
		ToolTipItem val84 = new ToolTipItem();
		SuperToolTip val85 = new SuperToolTip();
		ToolTipTitleItem val86 = new ToolTipTitleItem();
		ToolTipItem val87 = new ToolTipItem();
		SuperToolTip val88 = new SuperToolTip();
		ToolTipTitleItem val89 = new ToolTipTitleItem();
		ToolTipItem val90 = new ToolTipItem();
		SuperToolTip val91 = new SuperToolTip();
		ToolTipTitleItem val92 = new ToolTipTitleItem();
		ToolTipItem val93 = new ToolTipItem();
		SuperToolTip val94 = new SuperToolTip();
		ToolTipTitleItem val95 = new ToolTipTitleItem();
		ToolTipItem val96 = new ToolTipItem();
		SuperToolTip val97 = new SuperToolTip();
		ToolTipTitleItem val98 = new ToolTipTitleItem();
		ToolTipItem val99 = new ToolTipItem();
		SuperToolTip val100 = new SuperToolTip();
		ToolTipTitleItem val101 = new ToolTipTitleItem();
		ToolTipItem val102 = new ToolTipItem();
		SuperToolTip val103 = new SuperToolTip();
		ToolTipTitleItem val104 = new ToolTipTitleItem();
		ToolTipItem val105 = new ToolTipItem();
		SuperToolTip val106 = new SuperToolTip();
		ToolTipTitleItem val107 = new ToolTipTitleItem();
		ToolTipItem val108 = new ToolTipItem();
		SuperToolTip val109 = new SuperToolTip();
		ToolTipTitleItem val110 = new ToolTipTitleItem();
		ToolTipItem val111 = new ToolTipItem();
		SuperToolTip val112 = new SuperToolTip();
		ToolTipTitleItem val113 = new ToolTipTitleItem();
		ToolTipItem val114 = new ToolTipItem();
		SuperToolTip val115 = new SuperToolTip();
		ToolTipTitleItem val116 = new ToolTipTitleItem();
		ToolTipItem val117 = new ToolTipItem();
		SuperToolTip val118 = new SuperToolTip();
		ToolTipTitleItem val119 = new ToolTipTitleItem();
		ToolTipItem val120 = new ToolTipItem();
		SuperToolTip val121 = new SuperToolTip();
		ToolTipTitleItem val122 = new ToolTipTitleItem();
		ToolTipItem val123 = new ToolTipItem();
		SuperToolTip val124 = new SuperToolTip();
		ToolTipTitleItem val125 = new ToolTipTitleItem();
		ToolTipItem val126 = new ToolTipItem();
		SuperToolTip val127 = new SuperToolTip();
		ToolTipTitleItem val128 = new ToolTipTitleItem();
		ToolTipItem val129 = new ToolTipItem();
		SuperToolTip val130 = new SuperToolTip();
		ToolTipTitleItem val131 = new ToolTipTitleItem();
		ToolTipItem val132 = new ToolTipItem();
		SuperToolTip val133 = new SuperToolTip();
		ToolTipTitleItem val134 = new ToolTipTitleItem();
		ToolTipItem val135 = new ToolTipItem();
		SuperToolTip val136 = new SuperToolTip();
		ToolTipTitleItem val137 = new ToolTipTitleItem();
		ToolTipItem val138 = new ToolTipItem();
		SuperToolTip val139 = new SuperToolTip();
		ToolTipTitleItem val140 = new ToolTipTitleItem();
		ToolTipItem val141 = new ToolTipItem();
		SuperToolTip val142 = new SuperToolTip();
		ToolTipTitleItem val143 = new ToolTipTitleItem();
		ToolTipItem val144 = new ToolTipItem();
		SuperToolTip val145 = new SuperToolTip();
		ToolTipTitleItem val146 = new ToolTipTitleItem();
		ToolTipItem val147 = new ToolTipItem();
		SuperToolTip val148 = new SuperToolTip();
		ToolTipTitleItem val149 = new ToolTipTitleItem();
		ToolTipItem val150 = new ToolTipItem();
		SuperToolTip val151 = new SuperToolTip();
		ToolTipTitleItem val152 = new ToolTipTitleItem();
		ToolTipItem val153 = new ToolTipItem();
		SuperToolTip val154 = new SuperToolTip();
		ToolTipTitleItem val155 = new ToolTipTitleItem();
		ToolTipItem val156 = new ToolTipItem();
		SuperToolTip val157 = new SuperToolTip();
		ToolTipTitleItem val158 = new ToolTipTitleItem();
		ToolTipItem val159 = new ToolTipItem();
		SuperToolTip val160 = new SuperToolTip();
		ToolTipTitleItem val161 = new ToolTipTitleItem();
		ToolTipItem val162 = new ToolTipItem();
		SuperToolTip val163 = new SuperToolTip();
		ToolTipTitleItem val164 = new ToolTipTitleItem();
		ToolTipItem val165 = new ToolTipItem();
		SuperToolTip val166 = new SuperToolTip();
		ToolTipTitleItem val167 = new ToolTipTitleItem();
		ToolTipItem val168 = new ToolTipItem();
		SuperToolTip val169 = new SuperToolTip();
		ToolTipTitleItem val170 = new ToolTipTitleItem();
		ToolTipItem val171 = new ToolTipItem();
		SuperToolTip val172 = new SuperToolTip();
		ToolTipTitleItem val173 = new ToolTipTitleItem();
		ToolTipItem val174 = new ToolTipItem();
		SuperToolTip val175 = new SuperToolTip();
		ToolTipTitleItem val176 = new ToolTipTitleItem();
		ToolTipItem val177 = new ToolTipItem();
		SuperToolTip val178 = new SuperToolTip();
		ToolTipTitleItem val179 = new ToolTipTitleItem();
		ToolTipItem val180 = new ToolTipItem();
		SuperToolTip val181 = new SuperToolTip();
		ToolTipTitleItem val182 = new ToolTipTitleItem();
		ToolTipItem val183 = new ToolTipItem();
		SuperToolTip val184 = new SuperToolTip();
		ToolTipTitleItem val185 = new ToolTipTitleItem();
		ToolTipItem val186 = new ToolTipItem();
		SuperToolTip val187 = new SuperToolTip();
		ToolTipTitleItem val188 = new ToolTipTitleItem();
		ToolTipItem val189 = new ToolTipItem();
		SuperToolTip val190 = new SuperToolTip();
		ToolTipTitleItem val191 = new ToolTipTitleItem();
		ToolTipItem val192 = new ToolTipItem();
		SuperToolTip val193 = new SuperToolTip();
		ToolTipTitleItem val194 = new ToolTipTitleItem();
		ToolTipItem val195 = new ToolTipItem();
		SuperToolTip val196 = new SuperToolTip();
		ToolTipTitleItem val197 = new ToolTipTitleItem();
		ToolTipItem val198 = new ToolTipItem();
		SuperToolTip val199 = new SuperToolTip();
		ToolTipTitleItem val200 = new ToolTipTitleItem();
		ToolTipItem val201 = new ToolTipItem();
		SuperToolTip val202 = new SuperToolTip();
		ToolTipTitleItem val203 = new ToolTipTitleItem();
		ToolTipItem val204 = new ToolTipItem();
		SuperToolTip val205 = new SuperToolTip();
		ToolTipTitleItem val206 = new ToolTipTitleItem();
		ToolTipItem val207 = new ToolTipItem();
		SuperToolTip val208 = new SuperToolTip();
		ToolTipTitleItem val209 = new ToolTipTitleItem();
		ToolTipItem val210 = new ToolTipItem();
		SuperToolTip val211 = new SuperToolTip();
		ToolTipTitleItem val212 = new ToolTipTitleItem();
		ToolTipItem val213 = new ToolTipItem();
		SuperToolTip val214 = new SuperToolTip();
		ToolTipTitleItem val215 = new ToolTipTitleItem();
		ToolTipItem val216 = new ToolTipItem();
		SuperToolTip val217 = new SuperToolTip();
		ToolTipTitleItem val218 = new ToolTipTitleItem();
		ToolTipItem val219 = new ToolTipItem();
		SuperToolTip val220 = new SuperToolTip();
		ToolTipTitleItem val221 = new ToolTipTitleItem();
		ToolTipItem val222 = new ToolTipItem();
		SuperToolTip val223 = new SuperToolTip();
		ToolTipTitleItem val224 = new ToolTipTitleItem();
		ToolTipItem val225 = new ToolTipItem();
		SuperToolTip val226 = new SuperToolTip();
		ToolTipTitleItem val227 = new ToolTipTitleItem();
		ToolTipItem val228 = new ToolTipItem();
		SuperToolTip val229 = new SuperToolTip();
		ToolTipTitleItem val230 = new ToolTipTitleItem();
		ToolTipItem val231 = new ToolTipItem();
		SuperToolTip val232 = new SuperToolTip();
		ToolTipTitleItem val233 = new ToolTipTitleItem();
		ToolTipItem val234 = new ToolTipItem();
		SuperToolTip val235 = new SuperToolTip();
		ToolTipTitleItem val236 = new ToolTipTitleItem();
		ToolTipItem val237 = new ToolTipItem();
		SuperToolTip val238 = new SuperToolTip();
		ToolTipTitleItem val239 = new ToolTipTitleItem();
		ToolTipItem val240 = new ToolTipItem();
		SuperToolTip val241 = new SuperToolTip();
		ToolTipTitleItem val242 = new ToolTipTitleItem();
		ToolTipItem val243 = new ToolTipItem();
		SuperToolTip val244 = new SuperToolTip();
		ToolTipTitleItem val245 = new ToolTipTitleItem();
		ToolTipItem val246 = new ToolTipItem();
		SuperToolTip val247 = new SuperToolTip();
		ToolTipTitleItem val248 = new ToolTipTitleItem();
		ToolTipItem val249 = new ToolTipItem();
		SuperToolTip val250 = new SuperToolTip();
		ToolTipTitleItem val251 = new ToolTipTitleItem();
		ToolTipItem val252 = new ToolTipItem();
		SuperToolTip val253 = new SuperToolTip();
		ToolTipTitleItem val254 = new ToolTipTitleItem();
		ToolTipItem val255 = new ToolTipItem();
		SuperToolTip val256 = new SuperToolTip();
		ToolTipTitleItem val257 = new ToolTipTitleItem();
		ToolTipItem val258 = new ToolTipItem();
		SuperToolTip val259 = new SuperToolTip();
		ToolTipTitleItem val260 = new ToolTipTitleItem();
		ToolTipItem val261 = new ToolTipItem();
		SuperToolTip val262 = new SuperToolTip();
		ToolTipTitleItem val263 = new ToolTipTitleItem();
		ToolTipItem val264 = new ToolTipItem();
		SuperToolTip val265 = new SuperToolTip();
		ToolTipTitleItem val266 = new ToolTipTitleItem();
		ToolTipItem val267 = new ToolTipItem();
		SuperToolTip val268 = new SuperToolTip();
		ToolTipTitleItem val269 = new ToolTipTitleItem();
		ToolTipItem val270 = new ToolTipItem();
		SuperToolTip val271 = new SuperToolTip();
		ToolTipTitleItem val272 = new ToolTipTitleItem();
		ToolTipItem val273 = new ToolTipItem();
		SuperToolTip val274 = new SuperToolTip();
		ToolTipTitleItem val275 = new ToolTipTitleItem();
		ToolTipItem val276 = new ToolTipItem();
		SuperToolTip val277 = new SuperToolTip();
		ToolTipTitleItem val278 = new ToolTipTitleItem();
		ToolTipItem val279 = new ToolTipItem();
		SuperToolTip val280 = new SuperToolTip();
		ToolTipTitleItem val281 = new ToolTipTitleItem();
		ToolTipItem val282 = new ToolTipItem();
		SuperToolTip val283 = new SuperToolTip();
		ToolTipTitleItem val284 = new ToolTipTitleItem();
		ToolTipItem val285 = new ToolTipItem();
		SuperToolTip val286 = new SuperToolTip();
		ToolTipTitleItem val287 = new ToolTipTitleItem();
		ToolTipItem val288 = new ToolTipItem();
		SuperToolTip val289 = new SuperToolTip();
		ToolTipTitleItem val290 = new ToolTipTitleItem();
		ToolTipItem val291 = new ToolTipItem();
		SuperToolTip val292 = new SuperToolTip();
		ToolTipTitleItem val293 = new ToolTipTitleItem();
		ToolTipItem val294 = new ToolTipItem();
		SuperToolTip val295 = new SuperToolTip();
		ToolTipTitleItem val296 = new ToolTipTitleItem();
		ToolTipItem val297 = new ToolTipItem();
		SuperToolTip val298 = new SuperToolTip();
		ToolTipTitleItem val299 = new ToolTipTitleItem();
		ToolTipItem val300 = new ToolTipItem();
		SuperToolTip val301 = new SuperToolTip();
		ToolTipTitleItem val302 = new ToolTipTitleItem();
		ToolTipItem val303 = new ToolTipItem();
		SuperToolTip val304 = new SuperToolTip();
		ToolTipTitleItem val305 = new ToolTipTitleItem();
		ToolTipItem val306 = new ToolTipItem();
		SuperToolTip val307 = new SuperToolTip();
		ToolTipTitleItem val308 = new ToolTipTitleItem();
		ToolTipItem val309 = new ToolTipItem();
		SuperToolTip val310 = new SuperToolTip();
		ToolTipTitleItem val311 = new ToolTipTitleItem();
		ToolTipItem val312 = new ToolTipItem();
		SuperToolTip val313 = new SuperToolTip();
		ToolTipTitleItem val314 = new ToolTipTitleItem();
		ToolTipItem val315 = new ToolTipItem();
		SuperToolTip val316 = new SuperToolTip();
		ToolTipTitleItem val317 = new ToolTipTitleItem();
		ToolTipItem val318 = new ToolTipItem();
		SuperToolTip val319 = new SuperToolTip();
		ToolTipTitleItem val320 = new ToolTipTitleItem();
		ToolTipItem val321 = new ToolTipItem();
		SuperToolTip val322 = new SuperToolTip();
		ToolTipTitleItem val323 = new ToolTipTitleItem();
		ToolTipItem val324 = new ToolTipItem();
		SuperToolTip val325 = new SuperToolTip();
		ToolTipTitleItem val326 = new ToolTipTitleItem();
		ToolTipItem val327 = new ToolTipItem();
		SuperToolTip val328 = new SuperToolTip();
		ToolTipTitleItem val329 = new ToolTipTitleItem();
		ToolTipItem val330 = new ToolTipItem();
		SuperToolTip val331 = new SuperToolTip();
		ToolTipTitleItem val332 = new ToolTipTitleItem();
		ToolTipItem val333 = new ToolTipItem();
		SuperToolTip val334 = new SuperToolTip();
		ToolTipTitleItem val335 = new ToolTipTitleItem();
		ToolTipItem val336 = new ToolTipItem();
		SuperToolTip val337 = new SuperToolTip();
		ToolTipTitleItem val338 = new ToolTipTitleItem();
		ToolTipItem val339 = new ToolTipItem();
		SuperToolTip val340 = new SuperToolTip();
		ToolTipTitleItem val341 = new ToolTipTitleItem();
		ToolTipItem val342 = new ToolTipItem();
		SuperToolTip val343 = new SuperToolTip();
		ToolTipTitleItem val344 = new ToolTipTitleItem();
		ToolTipItem val345 = new ToolTipItem();
		SuperToolTip val346 = new SuperToolTip();
		ToolTipTitleItem val347 = new ToolTipTitleItem();
		ToolTipItem val348 = new ToolTipItem();
		SuperToolTip val349 = new SuperToolTip();
		ToolTipTitleItem val350 = new ToolTipTitleItem();
		ToolTipItem val351 = new ToolTipItem();
		SuperToolTip val352 = new SuperToolTip();
		ToolTipTitleItem val353 = new ToolTipTitleItem();
		ToolTipItem val354 = new ToolTipItem();
		SuperToolTip val355 = new SuperToolTip();
		ToolTipTitleItem val356 = new ToolTipTitleItem();
		ToolTipItem val357 = new ToolTipItem();
		SuperToolTip val358 = new SuperToolTip();
		ToolTipTitleItem val359 = new ToolTipTitleItem();
		ToolTipItem val360 = new ToolTipItem();
		SuperToolTip val361 = new SuperToolTip();
		ToolTipTitleItem val362 = new ToolTipTitleItem();
		ToolTipItem val363 = new ToolTipItem();
		SuperToolTip val364 = new SuperToolTip();
		ToolTipTitleItem val365 = new ToolTipTitleItem();
		ToolTipItem val366 = new ToolTipItem();
		SuperToolTip val367 = new SuperToolTip();
		ToolTipTitleItem val368 = new ToolTipTitleItem();
		ToolTipItem val369 = new ToolTipItem();
		SuperToolTip val370 = new SuperToolTip();
		ToolTipTitleItem val371 = new ToolTipTitleItem();
		ToolTipItem val372 = new ToolTipItem();
		SuperToolTip val373 = new SuperToolTip();
		ToolTipTitleItem val374 = new ToolTipTitleItem();
		ToolTipItem val375 = new ToolTipItem();
		SuperToolTip val376 = new SuperToolTip();
		ToolTipTitleItem val377 = new ToolTipTitleItem();
		ToolTipItem val378 = new ToolTipItem();
		SuperToolTip val379 = new SuperToolTip();
		ToolTipTitleItem val380 = new ToolTipTitleItem();
		ToolTipItem val381 = new ToolTipItem();
		SuperToolTip val382 = new SuperToolTip();
		ToolTipTitleItem val383 = new ToolTipTitleItem();
		ToolTipItem val384 = new ToolTipItem();
		SuperToolTip val385 = new SuperToolTip();
		ToolTipTitleItem val386 = new ToolTipTitleItem();
		ToolTipItem val387 = new ToolTipItem();
		SuperToolTip val388 = new SuperToolTip();
		ToolTipTitleItem val389 = new ToolTipTitleItem();
		ToolTipItem val390 = new ToolTipItem();
		SuperToolTip val391 = new SuperToolTip();
		ToolTipTitleItem val392 = new ToolTipTitleItem();
		ToolTipItem val393 = new ToolTipItem();
		SuperToolTip val394 = new SuperToolTip();
		ToolTipTitleItem val395 = new ToolTipTitleItem();
		ToolTipItem val396 = new ToolTipItem();
		SuperToolTip val397 = new SuperToolTip();
		ToolTipTitleItem val398 = new ToolTipTitleItem();
		ToolTipItem val399 = new ToolTipItem();
		SuperToolTip val400 = new SuperToolTip();
		ToolTipTitleItem val401 = new ToolTipTitleItem();
		ToolTipItem val402 = new ToolTipItem();
		SuperToolTip val403 = new SuperToolTip();
		ToolTipTitleItem val404 = new ToolTipTitleItem();
		ToolTipItem val405 = new ToolTipItem();
		SuperToolTip val406 = new SuperToolTip();
		ToolTipTitleItem val407 = new ToolTipTitleItem();
		ToolTipItem val408 = new ToolTipItem();
		SuperToolTip val409 = new SuperToolTip();
		ToolTipTitleItem val410 = new ToolTipTitleItem();
		ToolTipItem val411 = new ToolTipItem();
		SuperToolTip val412 = new SuperToolTip();
		ToolTipTitleItem val413 = new ToolTipTitleItem();
		ToolTipItem val414 = new ToolTipItem();
		SuperToolTip val415 = new SuperToolTip();
		ToolTipTitleItem val416 = new ToolTipTitleItem();
		ToolTipItem val417 = new ToolTipItem();
		SuperToolTip val418 = new SuperToolTip();
		ToolTipTitleItem val419 = new ToolTipTitleItem();
		ToolTipItem val420 = new ToolTipItem();
		SuperToolTip val421 = new SuperToolTip();
		ToolTipTitleItem val422 = new ToolTipTitleItem();
		ToolTipItem val423 = new ToolTipItem();
		SuperToolTip val424 = new SuperToolTip();
		ToolTipTitleItem val425 = new ToolTipTitleItem();
		ToolTipItem val426 = new ToolTipItem();
		SuperToolTip val427 = new SuperToolTip();
		ToolTipTitleItem val428 = new ToolTipTitleItem();
		ToolTipItem val429 = new ToolTipItem();
		SuperToolTip val430 = new SuperToolTip();
		ToolTipTitleItem val431 = new ToolTipTitleItem();
		ToolTipItem val432 = new ToolTipItem();
		SuperToolTip val433 = new SuperToolTip();
		ToolTipTitleItem val434 = new ToolTipTitleItem();
		ToolTipItem val435 = new ToolTipItem();
		SuperToolTip val436 = new SuperToolTip();
		ToolTipTitleItem val437 = new ToolTipTitleItem();
		ToolTipItem val438 = new ToolTipItem();
		SuperToolTip val439 = new SuperToolTip();
		ToolTipTitleItem val440 = new ToolTipTitleItem();
		ToolTipItem val441 = new ToolTipItem();
		SuperToolTip val442 = new SuperToolTip();
		ToolTipTitleItem val443 = new ToolTipTitleItem();
		ToolTipItem val444 = new ToolTipItem();
		SuperToolTip val445 = new SuperToolTip();
		ToolTipTitleItem val446 = new ToolTipTitleItem();
		ToolTipItem val447 = new ToolTipItem();
		SuperToolTip val448 = new SuperToolTip();
		ToolTipTitleItem val449 = new ToolTipTitleItem();
		ToolTipItem val450 = new ToolTipItem();
		SuperToolTip val451 = new SuperToolTip();
		ToolTipTitleItem val452 = new ToolTipTitleItem();
		ToolTipItem val453 = new ToolTipItem();
		SuperToolTip val454 = new SuperToolTip();
		ToolTipTitleItem val455 = new ToolTipTitleItem();
		ToolTipItem val456 = new ToolTipItem();
		SuperToolTip val457 = new SuperToolTip();
		ToolTipTitleItem val458 = new ToolTipTitleItem();
		ToolTipItem val459 = new ToolTipItem();
		SuperToolTip val460 = new SuperToolTip();
		ToolTipTitleItem val461 = new ToolTipTitleItem();
		ToolTipItem val462 = new ToolTipItem();
		SuperToolTip val463 = new SuperToolTip();
		ToolTipTitleItem val464 = new ToolTipTitleItem();
		ToolTipItem val465 = new ToolTipItem();
		SuperToolTip val466 = new SuperToolTip();
		ToolTipTitleItem val467 = new ToolTipTitleItem();
		ToolTipItem val468 = new ToolTipItem();
		SuperToolTip val469 = new SuperToolTip();
		ToolTipTitleItem val470 = new ToolTipTitleItem();
		ToolTipItem val471 = new ToolTipItem();
		SuperToolTip val472 = new SuperToolTip();
		ToolTipTitleItem val473 = new ToolTipTitleItem();
		ToolTipItem val474 = new ToolTipItem();
		SuperToolTip val475 = new SuperToolTip();
		ToolTipTitleItem val476 = new ToolTipTitleItem();
		ToolTipItem val477 = new ToolTipItem();
		SuperToolTip val478 = new SuperToolTip();
		ToolTipTitleItem val479 = new ToolTipTitleItem();
		ToolTipItem val480 = new ToolTipItem();
		SuperToolTip val481 = new SuperToolTip();
		ToolTipTitleItem val482 = new ToolTipTitleItem();
		ToolTipItem val483 = new ToolTipItem();
		SuperToolTip val484 = new SuperToolTip();
		ToolTipTitleItem val485 = new ToolTipTitleItem();
		ToolTipItem val486 = new ToolTipItem();
		SuperToolTip val487 = new SuperToolTip();
		ToolTipTitleItem val488 = new ToolTipTitleItem();
		ToolTipItem val489 = new ToolTipItem();
		SuperToolTip val490 = new SuperToolTip();
		ToolTipTitleItem val491 = new ToolTipTitleItem();
		ToolTipItem val492 = new ToolTipItem();
		SuperToolTip val493 = new SuperToolTip();
		ToolTipTitleItem val494 = new ToolTipTitleItem();
		ToolTipItem val495 = new ToolTipItem();
		SuperToolTip val496 = new SuperToolTip();
		ToolTipTitleItem val497 = new ToolTipTitleItem();
		ToolTipItem val498 = new ToolTipItem();
		SuperToolTip val499 = new SuperToolTip();
		ToolTipTitleItem val500 = new ToolTipTitleItem();
		ToolTipItem val501 = new ToolTipItem();
		SuperToolTip val502 = new SuperToolTip();
		ToolTipTitleItem val503 = new ToolTipTitleItem();
		ToolTipItem val504 = new ToolTipItem();
		SuperToolTip val505 = new SuperToolTip();
		ToolTipTitleItem val506 = new ToolTipTitleItem();
		ToolTipItem val507 = new ToolTipItem();
		SuperToolTip val508 = new SuperToolTip();
		ToolTipTitleItem val509 = new ToolTipTitleItem();
		ToolTipItem val510 = new ToolTipItem();
		SuperToolTip val511 = new SuperToolTip();
		ToolTipTitleItem val512 = new ToolTipTitleItem();
		ToolTipItem val513 = new ToolTipItem();
		SuperToolTip val514 = new SuperToolTip();
		ToolTipTitleItem val515 = new ToolTipTitleItem();
		ToolTipItem val516 = new ToolTipItem();
		SuperToolTip val517 = new SuperToolTip();
		ToolTipTitleItem val518 = new ToolTipTitleItem();
		ToolTipItem val519 = new ToolTipItem();
		SuperToolTip val520 = new SuperToolTip();
		ToolTipTitleItem val521 = new ToolTipTitleItem();
		ToolTipItem val522 = new ToolTipItem();
		SuperToolTip val523 = new SuperToolTip();
		ToolTipTitleItem val524 = new ToolTipTitleItem();
		ToolTipItem val525 = new ToolTipItem();
		SuperToolTip val526 = new SuperToolTip();
		ToolTipTitleItem val527 = new ToolTipTitleItem();
		ToolTipItem val528 = new ToolTipItem();
		SuperToolTip val529 = new SuperToolTip();
		ToolTipTitleItem val530 = new ToolTipTitleItem();
		ToolTipItem val531 = new ToolTipItem();
		SuperToolTip val532 = new SuperToolTip();
		ToolTipTitleItem val533 = new ToolTipTitleItem();
		ToolTipItem val534 = new ToolTipItem();
		SuperToolTip val535 = new SuperToolTip();
		ToolTipTitleItem val536 = new ToolTipTitleItem();
		ToolTipItem val537 = new ToolTipItem();
		SuperToolTip val538 = new SuperToolTip();
		ToolTipTitleItem val539 = new ToolTipTitleItem();
		ToolTipItem val540 = new ToolTipItem();
		SuperToolTip val541 = new SuperToolTip();
		ToolTipTitleItem val542 = new ToolTipTitleItem();
		ToolTipItem val543 = new ToolTipItem();
		SuperToolTip val544 = new SuperToolTip();
		ToolTipTitleItem val545 = new ToolTipTitleItem();
		ToolTipItem val546 = new ToolTipItem();
		SuperToolTip val547 = new SuperToolTip();
		ToolTipTitleItem val548 = new ToolTipTitleItem();
		ToolTipItem val549 = new ToolTipItem();
		SuperToolTip val550 = new SuperToolTip();
		ToolTipTitleItem val551 = new ToolTipTitleItem();
		ToolTipItem val552 = new ToolTipItem();
		SuperToolTip val553 = new SuperToolTip();
		ToolTipTitleItem val554 = new ToolTipTitleItem();
		ToolTipItem val555 = new ToolTipItem();
		SuperToolTip val556 = new SuperToolTip();
		ToolTipTitleItem val557 = new ToolTipTitleItem();
		ToolTipItem val558 = new ToolTipItem();
		SuperToolTip val559 = new SuperToolTip();
		ToolTipTitleItem val560 = new ToolTipTitleItem();
		ToolTipItem val561 = new ToolTipItem();
		SuperToolTip val562 = new SuperToolTip();
		ToolTipTitleItem val563 = new ToolTipTitleItem();
		ToolTipItem val564 = new ToolTipItem();
		SuperToolTip val565 = new SuperToolTip();
		ToolTipTitleItem val566 = new ToolTipTitleItem();
		ToolTipItem val567 = new ToolTipItem();
		SuperToolTip val568 = new SuperToolTip();
		ToolTipTitleItem val569 = new ToolTipTitleItem();
		ToolTipItem val570 = new ToolTipItem();
		SuperToolTip val571 = new SuperToolTip();
		ToolTipTitleItem val572 = new ToolTipTitleItem();
		ToolTipItem val573 = new ToolTipItem();
		SuperToolTip val574 = new SuperToolTip();
		ToolTipTitleItem val575 = new ToolTipTitleItem();
		ToolTipItem val576 = new ToolTipItem();
		SuperToolTip val577 = new SuperToolTip();
		ToolTipTitleItem val578 = new ToolTipTitleItem();
		ToolTipItem val579 = new ToolTipItem();
		SuperToolTip val580 = new SuperToolTip();
		ToolTipTitleItem val581 = new ToolTipTitleItem();
		ToolTipItem val582 = new ToolTipItem();
		SuperToolTip val583 = new SuperToolTip();
		ToolTipTitleItem val584 = new ToolTipTitleItem();
		ToolTipItem val585 = new ToolTipItem();
		SuperToolTip val586 = new SuperToolTip();
		ToolTipTitleItem val587 = new ToolTipTitleItem();
		ToolTipItem val588 = new ToolTipItem();
		SuperToolTip val589 = new SuperToolTip();
		ToolTipTitleItem val590 = new ToolTipTitleItem();
		ToolTipItem val591 = new ToolTipItem();
		SuperToolTip val592 = new SuperToolTip();
		ToolTipTitleItem val593 = new ToolTipTitleItem();
		ToolTipItem val594 = new ToolTipItem();
		SuperToolTip val595 = new SuperToolTip();
		ToolTipTitleItem val596 = new ToolTipTitleItem();
		ToolTipItem val597 = new ToolTipItem();
		SuperToolTip val598 = new SuperToolTip();
		ToolTipTitleItem val599 = new ToolTipTitleItem();
		ToolTipItem val600 = new ToolTipItem();
		SuperToolTip val601 = new SuperToolTip();
		ToolTipTitleItem val602 = new ToolTipTitleItem();
		ToolTipItem val603 = new ToolTipItem();
		SuperToolTip val604 = new SuperToolTip();
		ToolTipTitleItem val605 = new ToolTipTitleItem();
		ToolTipItem val606 = new ToolTipItem();
		SuperToolTip val607 = new SuperToolTip();
		ToolTipTitleItem val608 = new ToolTipTitleItem();
		ToolTipItem val609 = new ToolTipItem();
		SuperToolTip val610 = new SuperToolTip();
		ToolTipTitleItem val611 = new ToolTipTitleItem();
		ToolTipItem val612 = new ToolTipItem();
		SuperToolTip val613 = new SuperToolTip();
		ToolTipTitleItem val614 = new ToolTipTitleItem();
		ToolTipItem val615 = new ToolTipItem();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.AbstractForms.frmTestDev));
		this.dashboardDesigner1 = new DashboardDesigner();
		this.pivotGridControl1 = new PivotGridControl();
		this.textEdit1 = new TextEdit();
		this.checkButton1 = new CheckButton();
		this.barCodeControl1 = new BarCodeControl();
		this.calcEdit1 = new CalcEdit();
		this.labelControl1 = new LabelControl();
		this.pdfViewer1 = new PdfViewer();
		this.calendarControl1 = new CalendarControl();
		this.groupControl1 = new GroupControl();
		this.popupMenu1 = new PopupMenu(this.components);
		this.ribbonControl1 = new RibbonControl();
		this.ribbonPage1 = new RibbonPage();
		this.ribbonPageGroup1 = new RibbonPageGroup();
		this.ribbonStatusBar1 = new RibbonStatusBar();
		this.toolTipController1 = new ToolTipController(this.components);
		this.reportGenerator1 = new ReportGenerator(this.components);
		this.xtraSaveFileDialog1 = new XtraSaveFileDialog(this.components);
		this.ribbonReportDesigner1 = new RibbonReportDesigner();
		this.reportDesigner1 = new XRDesignMdiController(this.components);
		this.xrDesignRibbonController1 = new XRDesignRibbonController(this.components);
		this.commandBarItem1 = new CommandBarItem();
		this.commandBarItem2 = new CommandBarItem();
		this.commandBarItem3 = new CommandBarItem();
		this.commandBarItem4 = new CommandBarItem();
		this.commandBarItem5 = new CommandBarItem();
		this.commandBarItem6 = new CommandBarItem();
		this.commandBarItem7 = new CommandBarItem();
		this.commandBarItem8 = new CommandBarItem();
		this.commandBarItem9 = new CommandBarItem();
		this.commandBarItem10 = new CommandBarItem();
		this.commandBarItem11 = new CommandBarItem();
		this.commandBarItem12 = new CommandBarItem();
		this.commandBarItem13 = new CommandBarItem();
		this.commandBarItem14 = new CommandBarItem();
		this.commandBarItem15 = new CommandBarItem();
		this.commandBarItem16 = new CommandBarItem();
		this.recentlyUsedItemsComboBox1 = new RecentlyUsedItemsComboBox();
		this.barEditItem1 = new BarEditItem();
		this.designRepositoryItemComboBox1 = new DesignRepositoryItemComboBox();
		this.barEditItem2 = new BarEditItem();
		this.barDockPanelsListItem1 = new BarDockPanelsListItem();
		this.commandBarItem17 = new CommandBarItem();
		this.commandBarItem18 = new CommandBarItem();
		this.commandBarItem19 = new CommandBarItem();
		this.commandColorBarItem1 = new CommandColorBarItem();
		this.commandColorBarItem2 = new CommandColorBarItem();
		this.commandBarItem20 = new CommandBarItem();
		this.commandBarItem21 = new CommandBarItem();
		this.commandBarItem22 = new CommandBarItem();
		this.commandBarItem23 = new CommandBarItem();
		this.commandBarItem24 = new CommandBarItem();
		this.commandBarItem25 = new CommandBarItem();
		this.commandBarItem26 = new CommandBarItem();
		this.commandBarItem27 = new CommandBarItem();
		this.commandBarItem28 = new CommandBarItem();
		this.commandBarItem29 = new CommandBarItem();
		this.commandBarItem30 = new CommandBarItem();
		this.commandBarItem31 = new CommandBarItem();
		this.commandBarItem32 = new CommandBarItem();
		this.commandBarItem33 = new CommandBarItem();
		this.commandBarItem34 = new CommandBarItem();
		this.commandBarItem35 = new CommandBarItem();
		this.commandBarItem36 = new CommandBarItem();
		this.commandBarItem37 = new CommandBarItem();
		this.commandColorBarItem3 = new CommandColorBarItem();
		this.commandBarItem38 = new CommandBarItem();
		this.commandBarItem39 = new CommandBarItem();
		this.commandBarItem40 = new CommandBarItem();
		this.commandBarItem41 = new CommandBarItem();
		this.commandBarItem42 = new CommandBarItem();
		this.commandBarItem43 = new CommandBarItem();
		this.commandBarItem44 = new CommandBarItem();
		this.commandBarItem45 = new CommandBarItem();
		this.commandBarItem46 = new CommandBarItem();
		this.commandBarItem47 = new CommandBarItem();
		this.commandBarItem48 = new CommandBarItem();
		this.commandBarItem49 = new CommandBarItem();
		this.commandBarItem50 = new CommandBarItem();
		this.commandBarItem51 = new CommandBarItem();
		this.commandBarItem52 = new CommandBarItem();
		this.commandBarItem53 = new CommandBarItem();
		this.commandBarItem54 = new CommandBarItem();
		this.commandBarItem55 = new CommandBarItem();
		this.commandBarItem56 = new CommandBarItem();
		this.commandBarItem57 = new CommandBarItem();
		this.commandBarItem58 = new CommandBarItem();
		this.commandBarItem59 = new CommandBarItem();
		this.commandBarItem60 = new CommandBarItem();
		this.commandBarItem61 = new CommandBarItem();
		this.commandBarCheckItem1 = new CommandBarCheckItem();
		this.commandBarCheckItem2 = new CommandBarCheckItem();
		this.commandBarItem62 = new CommandBarItem();
		this.commandBarItem63 = new CommandBarItem();
		this.commandBarItem64 = new CommandBarItem();
		this.commandBarItem65 = new CommandBarItem();
		this.commandColorBarItem4 = new CommandColorBarItem();
		this.commandBarItem66 = new CommandBarItem();
		this.commandBarItem67 = new CommandBarItem();
		this.commandBarItem68 = new CommandBarItem();
		this.commandBarItem69 = new CommandBarItem();
		this.commandBarItem70 = new CommandBarItem();
		this.commandBarItem71 = new CommandBarItem();
		this.commandBarItem72 = new CommandBarItem();
		this.repositoryItemLookUpEdit1 = new RepositoryItemLookUpEdit();
		this.commandBarEditItem1 = new CommandBarEditItem();
		this.commandBarCheckItem3 = new CommandBarCheckItem();
		this.commandBarItem73 = new CommandBarItem();
		this.commandBarItem74 = new CommandBarItem();
		this.commandBarItem75 = new CommandBarItem();
		this.commandBarItem76 = new CommandBarItem();
		this.commandGalleryBarItem1 = new CommandGalleryBarItem();
		this.commandGalleryBarItem2 = new CommandGalleryBarItem();
		this.commandGalleryBarItem3 = new CommandGalleryBarItem();
		this.commandGalleryBarItem4 = new CommandGalleryBarItem();
		this.commandGalleryBarItem5 = new CommandGalleryBarItem();
		this.commandGalleryBarItem6 = new CommandGalleryBarItem();
		this.repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
		this.commandBarEditItem2 = new CommandBarEditItem();
		this.repositoryItemSpinEdit2 = new RepositoryItemSpinEdit();
		this.commandBarEditItem3 = new CommandBarEditItem();
		this.repositoryItemSpinEdit3 = new RepositoryItemSpinEdit();
		this.commandBarEditItem4 = new CommandBarEditItem();
		this.repositoryItemSpinEdit4 = new RepositoryItemSpinEdit();
		this.commandBarEditItem5 = new CommandBarEditItem();
		this.repositoryItemImageComboBox1 = new RepositoryItemImageComboBox();
		this.commandBarEditItem6 = new CommandBarEditItem();
		this.repositoryItemLookUpEdit2 = new RepositoryItemLookUpEdit();
		this.commandBarEditItem7 = new CommandBarEditItem();
		this.repositoryItemComboBox1 = new RepositoryItemComboBox();
		this.commandBarEditItem8 = new CommandBarEditItem();
		this.commandBarItem77 = new CommandBarItem();
		this.commandBarItem78 = new CommandBarItem();
		this.commandBarItem79 = new CommandBarItem();
		this.commandBarItem80 = new CommandBarItem();
		this.commandBarItem81 = new CommandBarItem();
		this.commandBarItem82 = new CommandBarItem();
		this.commandBarItem83 = new CommandBarItem();
		this.commandBarItem84 = new CommandBarItem();
		this.commandBarItem85 = new CommandBarItem();
		this.commandBarItem86 = new CommandBarItem();
		this.commandBarItem87 = new CommandBarItem();
		this.commandBarItem88 = new CommandBarItem();
		this.commandBarItem89 = new CommandBarItem();
		this.commandBarItem90 = new CommandBarItem();
		this.commandBarItem91 = new CommandBarItem();
		this.commandBarItem92 = new CommandBarItem();
		this.commandBarItem93 = new CommandBarItem();
		this.commandBarItem94 = new CommandBarItem();
		this.commandBarItem95 = new CommandBarItem();
		this.commandBarItem96 = new CommandBarItem();
		this.commandBarItem97 = new CommandBarItem();
		this.commandBarItem98 = new CommandBarItem();
		this.commandBarItem99 = new CommandBarItem();
		this.commandBarItem100 = new CommandBarItem();
		this.commandBarItem101 = new CommandBarItem();
		this.commandBarItem102 = new CommandBarItem();
		this.commandBarItem103 = new CommandBarItem();
		this.commandBarItem104 = new CommandBarItem();
		this.commandBarItem105 = new CommandBarItem();
		this.commandBarItem106 = new CommandBarItem();
		this.commandBarItem107 = new CommandBarItem();
		this.commandBarItem108 = new CommandBarItem();
		this.commandBarItem109 = new CommandBarItem();
		this.commandBarItem110 = new CommandBarItem();
		this.commandBarItem111 = new CommandBarItem();
		this.commandBarItem112 = new CommandBarItem();
		this.commandBarItem113 = new CommandBarItem();
		this.commandBarItem114 = new CommandBarItem();
		this.commandBarItem115 = new CommandBarItem();
		this.commandBarItem116 = new CommandBarItem();
		this.commandBarItem117 = new CommandBarItem();
		this.commandBarItem118 = new CommandBarItem();
		this.commandBarItem119 = new CommandBarItem();
		this.commandBarItem120 = new CommandBarItem();
		this.commandBarItem121 = new CommandBarItem();
		this.commandBarItem122 = new CommandBarItem();
		this.commandBarCheckItem4 = new CommandBarCheckItem();
		this.commandBarCheckItem5 = new CommandBarCheckItem();
		this.commandBarCheckItem6 = new CommandBarCheckItem();
		this.commandBarCheckItem7 = new CommandBarCheckItem();
		this.commandBarCheckItem8 = new CommandBarCheckItem();
		this.commandBarCheckItem9 = new CommandBarCheckItem();
		this.commandBarItem123 = new CommandBarItem();
		this.commandBarItem124 = new CommandBarItem();
		this.commandBarItem125 = new CommandBarItem();
		this.commandBarItem126 = new CommandBarItem();
		this.commandBarItem127 = new CommandBarItem();
		this.commandBarItem128 = new CommandBarItem();
		this.applicationMenu1 = new ApplicationMenu(this.components);
		this.ribbonPage2 = new XRHomeRibbonPage();
		this.xrDesignRibbonPageGroup1 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup2 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup3 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup4 = new XRDesignRibbonPageGroup();
		this.xrDesignBarButtonGroup1 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup2 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup3 = new XRDesignBarButtonGroup();
		this.xrDesignRibbonPageGroup5 = new XRDesignRibbonPageGroup();
		this.xrDesignBarButtonGroup4 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup5 = new XRDesignBarButtonGroup();
		this.xrDesignRibbonPageGroup6 = new XRDesignRibbonPageGroup();
		this.xrDesignBarButtonGroup6 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup7 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup8 = new XRDesignBarButtonGroup();
		this.xrDesignRibbonPageGroup7 = new XRDesignRibbonPageGroup();
		this.ribbonPage3 = new XRLayoutRibbonPage();
		this.xrDesignRibbonPageGroup8 = new XRDesignRibbonPageGroup();
		this.xrDesignBarButtonGroup9 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup10 = new XRDesignBarButtonGroup();
		this.xrDesignRibbonPageGroup9 = new XRDesignRibbonPageGroup();
		this.xrDesignBarButtonGroup11 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup12 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup13 = new XRDesignBarButtonGroup();
		this.xrDesignBarButtonGroup14 = new XRDesignBarButtonGroup();
		this.xrDesignRibbonPageGroup10 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup11 = new XRDesignRibbonPageGroup();
		this.ribbonPage4 = new XRPageRibbonPage();
		this.xrDesignRibbonPageGroup12 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup13 = new XRDesignRibbonPageGroup();
		this.ribbonPage5 = new XRViewRibbonPage();
		this.xrDesignRibbonPageGroup14 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup15 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup16 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup17 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup18 = new XRDesignRibbonPageGroup();
		this.ribbonPage6 = new XRScriptsRibbonPage();
		this.xrDesignRibbonPageGroup19 = new XRDesignRibbonPageGroup();
		this.ribbonPage7 = new XRCharacterCombDesignContextRibbonPage();
		this.ribbonPageCategory1 = new XRCharacterCombRibbonPageCategory();
		this.xrDesignRibbonPageGroup20 = new XRDesignRibbonPageGroup();
		this.ribbonPageCategory2 = new XRTableRibbonPageCategory();
		this.ribbonPage8 = new XRTableDesignContextRibbonPage();
		this.xrDesignRibbonPageGroup21 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup22 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup23 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup24 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup25 = new XRDesignRibbonPageGroup();
		this.ribbonPageCategory3 = new XRChartRibbonPageCategory();
		this.ribbonPage9 = new XRChartDesignContextRibbonPage();
		this.xrDesignRibbonPageGroup26 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup27 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup28 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup29 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup30 = new XRDesignRibbonPageGroup();
		this.ribbonPageCategory4 = new XRPivotGridRibbonPageCategory();
		this.ribbonPage10 = new XRPivotGridDesignContextRibbonPage();
		this.xrDesignRibbonPageGroup31 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup32 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup33 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup34 = new XRDesignRibbonPageGroup();
		this.ribbonPageCategory5 = new XRBarCodeRibbonPageCategory();
		this.ribbonPage11 = new XRBarcodeDesignContextRibbonPage();
		this.xrDesignRibbonPageGroup35 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup36 = new XRDesignRibbonPageGroup();
		this.ribbonPage12 = new XRGaugeDesignContextRibbonPage();
		this.ribbonPageCategory6 = new XRGaugeRibbonPageCategory();
		this.xrDesignRibbonPageGroup37 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup38 = new XRDesignRibbonPageGroup();
		this.ribbonPage13 = new XRSparklineDesignContextRibbonPage();
		this.ribbonPageCategory7 = new XRSparklineRibbonPageCategory();
		this.xrDesignRibbonPageGroup39 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup40 = new XRDesignRibbonPageGroup();
		this.ribbonPage14 = new XRShapeDesignContextRibbonPage();
		this.ribbonPageCategory8 = new XRShapeRibbonPageCategory();
		this.xrDesignRibbonPageGroup41 = new XRDesignRibbonPageGroup();
		this.ribbonPage15 = new XRLabelTextContextRibbonPage();
		this.ribbonPageCategory9 = new XRLabelRibbonPageCategory();
		this.xrDesignRibbonPageGroup42 = new XRDesignRibbonPageGroup();
		this.xrDesignRibbonPageGroup43 = new XRDesignRibbonPageGroup();
		this.printPreviewBarItem1 = new PrintPreviewBarItem();
		this.printPreviewBarItem2 = new PrintPreviewBarItem();
		this.printPreviewBarItem3 = new PrintPreviewBarItem();
		this.printPreviewBarItem4 = new PrintPreviewBarItem();
		this.printPreviewBarItem5 = new PrintPreviewBarItem();
		this.printPreviewBarItem7 = new PrintPreviewBarItem();
		this.printPreviewBarItem8 = new PrintPreviewBarItem();
		this.printPreviewBarItem9 = new PrintPreviewBarItem();
		this.printPreviewBarItem11 = new PrintPreviewBarItem();
		this.printPreviewBarItem12 = new PrintPreviewBarItem();
		this.printPreviewBarItem13 = new PrintPreviewBarItem();
		this.printPreviewBarItem14 = new PrintPreviewBarItem();
		this.printPreviewBarItem15 = new PrintPreviewBarItem();
		this.printPreviewBarItem16 = new PrintPreviewBarItem();
		this.printPreviewBarItem17 = new PrintPreviewBarItem();
		this.printPreviewBarItem18 = new PrintPreviewBarItem();
		this.printPreviewBarItem19 = new PrintPreviewBarItem();
		this.printPreviewBarItem20 = new PrintPreviewBarItem();
		this.printPreviewBarItem21 = new PrintPreviewBarItem();
		this.printPreviewBarItem22 = new PrintPreviewBarItem();
		this.printPreviewBarItem23 = new PrintPreviewBarItem();
		this.printPreviewBarItem24 = new PrintPreviewBarItem();
		this.printPreviewBarItem25 = new PrintPreviewBarItem();
		this.printPreviewBarItem26 = new PrintPreviewBarItem();
		this.printPreviewBarItem27 = new PrintPreviewBarItem();
		this.printPreviewBarItem28 = new PrintPreviewBarItem();
		this.printPreviewBarItem29 = new PrintPreviewBarItem();
		this.printPreviewBarItem30 = new PrintPreviewBarItem();
		this.printPreviewBarItem31 = new PrintPreviewBarItem();
		this.printPreviewBarItem32 = new PrintPreviewBarItem();
		this.printPreviewBarItem33 = new PrintPreviewBarItem();
		this.printPreviewBarItem34 = new PrintPreviewBarItem();
		this.printPreviewBarItem35 = new PrintPreviewBarItem();
		this.printPreviewBarItem36 = new PrintPreviewBarItem();
		this.printPreviewBarItem37 = new PrintPreviewBarItem();
		this.printPreviewBarItem38 = new PrintPreviewBarItem();
		this.printPreviewBarItem39 = new PrintPreviewBarItem();
		this.printPreviewBarItem40 = new PrintPreviewBarItem();
		this.printPreviewBarItem41 = new PrintPreviewBarItem();
		this.printPreviewBarItem42 = new PrintPreviewBarItem();
		this.printPreviewBarItem43 = new PrintPreviewBarItem();
		this.printPreviewBarItem44 = new PrintPreviewBarItem();
		this.printPreviewBarItem45 = new PrintPreviewBarItem();
		this.printPreviewBarItem46 = new PrintPreviewBarItem();
		this.printPreviewBarItem47 = new PrintPreviewBarItem();
		this.printPreviewBarItem48 = new PrintPreviewBarItem();
		this.printPreviewBarItem49 = new PrintPreviewBarItem();
		this.printPreviewBarItem50 = new PrintPreviewBarItem();
		this.printPreviewBarItem51 = new PrintPreviewBarItem();
		this.printPreviewStaticItem1 = new PrintPreviewStaticItem();
		this.repositoryItemProgressBar1 = new RepositoryItemProgressBar();
		this.progressBarEditItem1 = new ProgressBarEditItem();
		this.printPreviewBarItem52 = new PrintPreviewBarItem();
		this.commandBarItem129 = new CommandBarItem();
		this.printPreviewStaticItem2 = new PrintPreviewStaticItem();
		this.repositoryItemZoomTrackBar1 = new RepositoryItemZoomTrackBar();
		this.zoomTrackBarEditItem1 = new ZoomTrackBarEditItem();
		this.ribbonPage16 = new PrintPreviewRibbonPage();
		this.printPreviewRibbonPageGroup1 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup2 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup3 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup4 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup5 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup6 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup7 = new PrintPreviewRibbonPageGroup();
		this.printPreviewRibbonPageGroup8 = new PrintPreviewRibbonPageGroup();
		this.xrDesignDockManager1 = new XRDesignDockManager(this.components);
		this.fieldListDockPanel1 = new FieldListDockPanel();
		this.fieldListDockPanel1_Container = new DesignControlContainer();
		this.propertyGridDockPanel1 = new PropertyGridDockPanel();
		this.propertyGridDockPanel1_Container = new DesignControlContainer();
		this.reportExplorerDockPanel1 = new ReportExplorerDockPanel();
		this.reportExplorerDockPanel1_Container = new DesignControlContainer();
		this.reportGalleryDockPanel1 = new ReportGalleryDockPanel();
		this.reportGalleryDockPanel1_Container = new DesignControlContainer();
		this.groupAndSortDockPanel1 = new GroupAndSortDockPanel();
		this.groupAndSortDockPanel1_Container = new DesignControlContainer();
		this.errorListDockPanel1 = new ErrorListDockPanel();
		this.errorListDockPanel1_Container = new DesignControlContainer();
		this.panelContainer1 = new DockPanel();
		this.panelContainer2 = new DockPanel();
		this.panelContainer3 = new DockPanel();
		this.panelContainer4 = new DockPanel();
		((System.ComponentModel.ISupportInitialize)this.dashboardDesigner1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pivotGridControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.textEdit1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.calcEdit1.Properties).BeginInit();
		((System.ComponentModel.ISupportInitialize)((CalendarControlBase)this.calendarControl1).CalendarTimeProperties).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.groupControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.popupMenu1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.ribbonControl1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.reportDesigner1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.xrDesignRibbonController1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.recentlyUsedItemsComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.designRepositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemImageComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.applicationMenu1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemProgressBar1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemZoomTrackBar1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.xrDesignDockManager1).BeginInit();
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.panelContainer1).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.panelContainer2).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.panelContainer3).SuspendLayout();
		((System.Windows.Forms.Control)(object)this.panelContainer4).SuspendLayout();
		base.SuspendLayout();
		((XtraUserControl)this.dashboardDesigner1).Appearance.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
		((XtraUserControl)this.dashboardDesigner1).Appearance.Options.UseBackColor = true;
		this.dashboardDesigner1.AsyncMode = true;
		((System.Windows.Forms.Control)(object)this.dashboardDesigner1).Location = new System.Drawing.Point(111, 6);
		this.dashboardDesigner1.MenuManager = (IDXMenuManager)(object)this.ribbonControl1;
		((System.Windows.Forms.Control)(object)this.dashboardDesigner1).Name = "dashboardDesigner1";
		((System.Windows.Forms.Control)(object)this.dashboardDesigner1).Size = new System.Drawing.Size(868, 442);
		((System.Windows.Forms.Control)(object)this.dashboardDesigner1).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.pivotGridControl1).Location = new System.Drawing.Point(12, 12);
		((System.Windows.Forms.Control)(object)this.pivotGridControl1).Name = "pivotGridControl1";
		this.pivotGridControl1.OptionsData.DataProcessingEngine = (PivotDataProcessingEngine)3;
		((System.Windows.Forms.Control)(object)this.pivotGridControl1).Size = new System.Drawing.Size(85, 99);
		((System.Windows.Forms.Control)(object)this.pivotGridControl1).TabIndex = 1;
		((System.Windows.Forms.Control)(object)this.textEdit1).Location = new System.Drawing.Point(12, 128);
		((System.Windows.Forms.Control)(object)this.textEdit1).Name = "textEdit1";
		((System.Windows.Forms.Control)(object)this.textEdit1).Size = new System.Drawing.Size(93, 20);
		((System.Windows.Forms.Control)(object)this.textEdit1).TabIndex = 2;
		((System.Windows.Forms.Control)(object)this.checkButton1).Location = new System.Drawing.Point(14, 154);
		((System.Windows.Forms.Control)(object)this.checkButton1).Name = "checkButton1";
		((System.Windows.Forms.Control)(object)this.checkButton1).Size = new System.Drawing.Size(83, 25);
		((System.Windows.Forms.Control)(object)this.checkButton1).TabIndex = 3;
		((System.Windows.Forms.Control)(object)this.checkButton1).Text = "checkButton1";
		((System.Windows.Forms.Control)(object)this.barCodeControl1).Location = new System.Drawing.Point(12, 185);
		((System.Windows.Forms.Control)(object)this.barCodeControl1).Name = "barCodeControl1";
		((System.Windows.Forms.Control)(object)this.barCodeControl1).Padding = new System.Windows.Forms.Padding(10, 2, 10, 0);
		((System.Windows.Forms.Control)(object)this.barCodeControl1).Size = new System.Drawing.Size(85, 25);
		this.barCodeControl1.Symbology = (BarCodeGeneratorBase)(object)symbology;
		((System.Windows.Forms.Control)(object)this.barCodeControl1).TabIndex = 4;
		((System.Windows.Forms.Control)(object)this.calcEdit1).Location = new System.Drawing.Point(12, 216);
		((System.Windows.Forms.Control)(object)this.calcEdit1).Name = "calcEdit1";
		((RepositoryItemButtonEdit)this.calcEdit1.Properties).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((System.Windows.Forms.Control)(object)this.calcEdit1).Size = new System.Drawing.Size(93, 20);
		((System.Windows.Forms.Control)(object)this.calcEdit1).TabIndex = 5;
		((System.Windows.Forms.Control)(object)this.labelControl1).Location = new System.Drawing.Point(14, 242);
		((System.Windows.Forms.Control)(object)this.labelControl1).Name = "labelControl1";
		((System.Windows.Forms.Control)(object)this.labelControl1).Size = new System.Drawing.Size(70, 14);
		((System.Windows.Forms.Control)(object)this.labelControl1).TabIndex = 6;
		((System.Windows.Forms.Control)(object)this.labelControl1).Text = "labelControl1";
		((System.Windows.Forms.Control)(object)this.pdfViewer1).Location = new System.Drawing.Point(12, 262);
		this.pdfViewer1.MenuManager = (IDXMenuManager)(object)this.ribbonControl1;
		((System.Windows.Forms.Control)(object)this.pdfViewer1).Name = "pdfViewer1";
		((System.Windows.Forms.Control)(object)this.pdfViewer1).Size = new System.Drawing.Size(81, 63);
		((System.Windows.Forms.Control)(object)this.pdfViewer1).TabIndex = 7;
		((RepositoryItemButtonEdit)((CalendarControlBase)this.calendarControl1).CalendarTimeProperties).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((System.Windows.Forms.Control)(object)this.calendarControl1).Location = new System.Drawing.Point(173, 111);
		((System.Windows.Forms.Control)(object)this.calendarControl1).Name = "calendarControl1";
		((System.Windows.Forms.Control)(object)this.calendarControl1).Size = new System.Drawing.Size(255, 252);
		((System.Windows.Forms.Control)(object)this.calendarControl1).TabIndex = 8;
		((System.Windows.Forms.Control)(object)this.groupControl1).Location = new System.Drawing.Point(502, 281);
		((System.Windows.Forms.Control)(object)this.groupControl1).Name = "groupControl1";
		((System.Windows.Forms.Control)(object)this.groupControl1).Size = new System.Drawing.Size(200, 100);
		((System.Windows.Forms.Control)(object)this.groupControl1).TabIndex = 9;
		((System.Windows.Forms.Control)(object)this.groupControl1).Text = "groupControl1";
		((PopupMenuBase)this.popupMenu1).Name = "popupMenu1";
		((PopupMenuBase)this.popupMenu1).Ribbon = this.ribbonControl1;
		this.ribbonControl1.ApplicationButtonDropDownControl = this.applicationMenu1;
		this.ribbonControl1.AutoHideEmptyItems = true;
		this.ribbonControl1.AutoSizeItems = true;
		((BarItem)this.ribbonControl1.ExpandCollapseItem).Id = 0;
		((BarItems)this.ribbonControl1.Items).AddRange((BarItem[])(object)new BarItem[229]
		{
			(BarItem)this.ribbonControl1.SearchEditItem,
			(BarItem)this.ribbonControl1.ExpandCollapseItem,
			(BarItem)this.commandBarItem1,
			(BarItem)this.commandBarItem2,
			(BarItem)this.commandBarItem3,
			(BarItem)this.commandBarItem4,
			(BarItem)this.commandBarItem5,
			(BarItem)this.commandBarItem6,
			(BarItem)this.commandBarItem7,
			(BarItem)this.commandBarItem8,
			(BarItem)this.commandBarItem9,
			(BarItem)this.commandBarItem10,
			(BarItem)this.commandBarItem11,
			(BarItem)this.commandBarItem12,
			(BarItem)this.commandBarItem13,
			(BarItem)this.commandBarItem14,
			(BarItem)this.commandBarItem15,
			(BarItem)this.commandBarItem16,
			(BarItem)this.barEditItem1,
			(BarItem)this.barEditItem2,
			(BarItem)this.barDockPanelsListItem1,
			(BarItem)this.commandBarItem17,
			(BarItem)this.commandBarItem18,
			(BarItem)this.commandBarItem19,
			(BarItem)this.commandColorBarItem1,
			(BarItem)this.commandColorBarItem2,
			(BarItem)this.commandBarItem20,
			(BarItem)this.commandBarItem21,
			(BarItem)this.commandBarItem22,
			(BarItem)this.commandBarItem23,
			(BarItem)this.commandBarItem24,
			(BarItem)this.commandBarItem25,
			(BarItem)this.commandBarItem26,
			(BarItem)this.commandBarItem27,
			(BarItem)this.commandBarItem28,
			(BarItem)this.commandBarItem29,
			(BarItem)this.commandBarItem30,
			(BarItem)this.commandBarItem31,
			(BarItem)this.commandBarItem32,
			(BarItem)this.commandBarItem33,
			(BarItem)this.commandBarItem34,
			(BarItem)this.commandBarItem35,
			(BarItem)this.commandBarItem36,
			(BarItem)this.commandBarItem37,
			(BarItem)this.commandColorBarItem3,
			(BarItem)this.commandBarItem38,
			(BarItem)this.commandBarItem39,
			(BarItem)this.commandBarItem40,
			(BarItem)this.commandBarItem41,
			(BarItem)this.commandBarItem42,
			(BarItem)this.commandBarItem43,
			(BarItem)this.commandBarItem44,
			(BarItem)this.commandBarItem45,
			(BarItem)this.commandBarItem46,
			(BarItem)this.commandBarItem47,
			(BarItem)this.commandBarItem48,
			(BarItem)this.commandBarItem49,
			(BarItem)this.commandBarItem50,
			(BarItem)this.commandBarItem51,
			(BarItem)this.commandBarItem52,
			(BarItem)this.commandBarItem53,
			(BarItem)this.commandBarItem54,
			(BarItem)this.commandBarItem55,
			(BarItem)this.commandBarItem56,
			(BarItem)this.commandBarItem57,
			(BarItem)this.commandBarItem58,
			(BarItem)this.commandBarItem59,
			(BarItem)this.commandBarItem60,
			(BarItem)this.commandBarItem61,
			(BarItem)this.commandBarCheckItem1,
			(BarItem)this.commandBarCheckItem2,
			(BarItem)this.commandBarItem62,
			(BarItem)this.commandBarItem63,
			(BarItem)this.commandBarItem64,
			(BarItem)this.commandBarItem65,
			(BarItem)this.commandColorBarItem4,
			(BarItem)this.commandBarItem66,
			(BarItem)this.commandBarItem67,
			(BarItem)this.commandBarItem68,
			(BarItem)this.commandBarItem69,
			(BarItem)this.commandBarItem70,
			(BarItem)this.commandBarItem71,
			(BarItem)this.commandBarItem72,
			(BarItem)this.commandBarEditItem1,
			(BarItem)this.commandBarCheckItem3,
			(BarItem)this.commandBarItem73,
			(BarItem)this.commandBarItem74,
			(BarItem)this.commandBarItem75,
			(BarItem)this.commandBarItem76,
			(BarItem)this.commandGalleryBarItem1,
			(BarItem)this.commandGalleryBarItem2,
			(BarItem)this.commandGalleryBarItem3,
			(BarItem)this.commandGalleryBarItem4,
			(BarItem)this.commandGalleryBarItem5,
			(BarItem)this.commandGalleryBarItem6,
			(BarItem)this.commandBarEditItem2,
			(BarItem)this.commandBarEditItem3,
			(BarItem)this.commandBarEditItem4,
			(BarItem)this.commandBarEditItem5,
			(BarItem)this.commandBarEditItem6,
			(BarItem)this.commandBarEditItem7,
			(BarItem)this.commandBarEditItem8,
			(BarItem)this.commandBarItem77,
			(BarItem)this.commandBarItem78,
			(BarItem)this.commandBarItem79,
			(BarItem)this.commandBarItem80,
			(BarItem)this.commandBarItem81,
			(BarItem)this.commandBarItem82,
			(BarItem)this.commandBarItem83,
			(BarItem)this.commandBarItem84,
			(BarItem)this.commandBarItem85,
			(BarItem)this.commandBarItem86,
			(BarItem)this.commandBarItem87,
			(BarItem)this.commandBarItem88,
			(BarItem)this.commandBarItem89,
			(BarItem)this.commandBarItem90,
			(BarItem)this.commandBarItem91,
			(BarItem)this.commandBarItem92,
			(BarItem)this.commandBarItem93,
			(BarItem)this.commandBarItem94,
			(BarItem)this.commandBarItem95,
			(BarItem)this.commandBarItem96,
			(BarItem)this.commandBarItem97,
			(BarItem)this.commandBarItem98,
			(BarItem)this.commandBarItem99,
			(BarItem)this.commandBarItem100,
			(BarItem)this.commandBarItem101,
			(BarItem)this.commandBarItem102,
			(BarItem)this.commandBarItem103,
			(BarItem)this.commandBarItem104,
			(BarItem)this.commandBarItem105,
			(BarItem)this.commandBarItem106,
			(BarItem)this.commandBarItem107,
			(BarItem)this.commandBarItem108,
			(BarItem)this.commandBarItem109,
			(BarItem)this.commandBarItem110,
			(BarItem)this.commandBarItem111,
			(BarItem)this.commandBarItem112,
			(BarItem)this.commandBarItem113,
			(BarItem)this.commandBarItem114,
			(BarItem)this.commandBarItem115,
			(BarItem)this.commandBarItem116,
			(BarItem)this.commandBarItem117,
			(BarItem)this.commandBarItem118,
			(BarItem)this.commandBarItem119,
			(BarItem)this.commandBarItem120,
			(BarItem)this.commandBarItem121,
			(BarItem)this.commandBarItem122,
			(BarItem)this.commandBarCheckItem4,
			(BarItem)this.commandBarCheckItem5,
			(BarItem)this.commandBarCheckItem6,
			(BarItem)this.commandBarCheckItem7,
			(BarItem)this.commandBarCheckItem8,
			(BarItem)this.commandBarCheckItem9,
			(BarItem)this.commandBarItem123,
			(BarItem)this.commandBarItem124,
			(BarItem)this.commandBarItem125,
			(BarItem)this.commandBarItem126,
			(BarItem)this.commandBarItem127,
			(BarItem)this.commandBarItem128,
			(BarItem)this.xrDesignBarButtonGroup1,
			(BarItem)this.xrDesignBarButtonGroup2,
			(BarItem)this.xrDesignBarButtonGroup3,
			(BarItem)this.xrDesignBarButtonGroup4,
			(BarItem)this.xrDesignBarButtonGroup5,
			(BarItem)this.xrDesignBarButtonGroup6,
			(BarItem)this.xrDesignBarButtonGroup7,
			(BarItem)this.xrDesignBarButtonGroup8,
			(BarItem)this.xrDesignBarButtonGroup9,
			(BarItem)this.xrDesignBarButtonGroup10,
			(BarItem)this.xrDesignBarButtonGroup11,
			(BarItem)this.xrDesignBarButtonGroup12,
			(BarItem)this.xrDesignBarButtonGroup13,
			(BarItem)this.xrDesignBarButtonGroup14,
			(BarItem)this.printPreviewBarItem1,
			(BarItem)this.printPreviewBarItem2,
			(BarItem)this.printPreviewBarItem3,
			(BarItem)this.printPreviewBarItem4,
			(BarItem)this.printPreviewBarItem5,
			(BarItem)this.printPreviewBarItem7,
			(BarItem)this.printPreviewBarItem8,
			(BarItem)this.printPreviewBarItem9,
			(BarItem)this.printPreviewBarItem11,
			(BarItem)this.printPreviewBarItem12,
			(BarItem)this.printPreviewBarItem13,
			(BarItem)this.printPreviewBarItem14,
			(BarItem)this.printPreviewBarItem15,
			(BarItem)this.printPreviewBarItem16,
			(BarItem)this.printPreviewBarItem17,
			(BarItem)this.printPreviewBarItem18,
			(BarItem)this.printPreviewBarItem19,
			(BarItem)this.printPreviewBarItem20,
			(BarItem)this.printPreviewBarItem21,
			(BarItem)this.printPreviewBarItem22,
			(BarItem)this.printPreviewBarItem23,
			(BarItem)this.printPreviewBarItem24,
			(BarItem)this.printPreviewBarItem25,
			(BarItem)this.printPreviewBarItem26,
			(BarItem)this.printPreviewBarItem27,
			(BarItem)this.printPreviewBarItem28,
			(BarItem)this.printPreviewBarItem29,
			(BarItem)this.printPreviewBarItem30,
			(BarItem)this.printPreviewBarItem31,
			(BarItem)this.printPreviewBarItem32,
			(BarItem)this.printPreviewBarItem33,
			(BarItem)this.printPreviewBarItem34,
			(BarItem)this.printPreviewBarItem35,
			(BarItem)this.printPreviewBarItem36,
			(BarItem)this.printPreviewBarItem37,
			(BarItem)this.printPreviewBarItem38,
			(BarItem)this.printPreviewBarItem39,
			(BarItem)this.printPreviewBarItem40,
			(BarItem)this.printPreviewBarItem41,
			(BarItem)this.printPreviewBarItem42,
			(BarItem)this.printPreviewBarItem43,
			(BarItem)this.printPreviewBarItem44,
			(BarItem)this.printPreviewBarItem45,
			(BarItem)this.printPreviewBarItem46,
			(BarItem)this.printPreviewBarItem47,
			(BarItem)this.printPreviewBarItem48,
			(BarItem)this.printPreviewBarItem49,
			(BarItem)this.printPreviewBarItem50,
			(BarItem)this.printPreviewBarItem51,
			(BarItem)this.printPreviewStaticItem1,
			(BarItem)this.progressBarEditItem1,
			(BarItem)this.printPreviewBarItem52,
			(BarItem)this.commandBarItem129,
			(BarItem)this.printPreviewStaticItem2,
			(BarItem)this.zoomTrackBarEditItem1
		});
		((System.Windows.Forms.Control)(object)this.ribbonControl1).Location = new System.Drawing.Point(0, 0);
		this.ribbonControl1.MaxItemId = 224;
		((System.Windows.Forms.Control)(object)this.ribbonControl1).Name = "ribbonControl1";
		this.ribbonControl1.PageCategories.AddRange((RibbonPageCategory[])(object)new RibbonPageCategory[9]
		{
			(RibbonPageCategory)this.ribbonPageCategory1,
			(RibbonPageCategory)this.ribbonPageCategory2,
			(RibbonPageCategory)this.ribbonPageCategory3,
			(RibbonPageCategory)this.ribbonPageCategory4,
			(RibbonPageCategory)this.ribbonPageCategory5,
			(RibbonPageCategory)this.ribbonPageCategory6,
			(RibbonPageCategory)this.ribbonPageCategory7,
			(RibbonPageCategory)this.ribbonPageCategory8,
			(RibbonPageCategory)this.ribbonPageCategory9
		});
		((BarItemLinkCollection)this.ribbonControl1.PageHeaderItemLinks).Add((BarItem)(object)this.commandBarItem1);
		((BarItemLinkCollection)this.ribbonControl1.PageHeaderItemLinks).Add((BarItem)(object)this.commandBarItem2);
		((BarItemLinkCollection)this.ribbonControl1.PageHeaderItemLinks).Add((BarItem)(object)this.commandBarItem3);
		this.ribbonControl1.Pages.AddRange((RibbonPage[])(object)new RibbonPage[7]
		{
			this.ribbonPage1,
			(RibbonPage)this.ribbonPage2,
			(RibbonPage)this.ribbonPage3,
			(RibbonPage)this.ribbonPage4,
			(RibbonPage)this.ribbonPage5,
			(RibbonPage)this.ribbonPage6,
			(RibbonPage)this.ribbonPage16
		});
		((BarItemLinkCollection)this.ribbonControl1.QuickToolbarItemLinks).Add((BarItem)(object)this.commandBarItem4);
		((BarItemLinkCollection)this.ribbonControl1.QuickToolbarItemLinks).Add((BarItem)(object)this.commandBarItem5);
		this.ribbonControl1.RepositoryItems.AddRange((RepositoryItem[])(object)new RepositoryItem[12]
		{
			(RepositoryItem)this.recentlyUsedItemsComboBox1,
			(RepositoryItem)this.designRepositoryItemComboBox1,
			(RepositoryItem)this.repositoryItemLookUpEdit1,
			(RepositoryItem)this.repositoryItemSpinEdit1,
			(RepositoryItem)this.repositoryItemSpinEdit2,
			(RepositoryItem)this.repositoryItemSpinEdit3,
			(RepositoryItem)this.repositoryItemSpinEdit4,
			(RepositoryItem)this.repositoryItemImageComboBox1,
			(RepositoryItem)this.repositoryItemLookUpEdit2,
			(RepositoryItem)this.repositoryItemComboBox1,
			(RepositoryItem)this.repositoryItemProgressBar1,
			(RepositoryItem)this.repositoryItemZoomTrackBar1
		});
		this.ribbonControl1.ShowItemCaptionsInPageHeader = true;
		((System.Windows.Forms.Control)(object)this.ribbonControl1).Size = new System.Drawing.Size(984, 158);
		this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
		this.ribbonControl1.TransparentEditorsMode = (DefaultBoolean)0;
		this.ribbonPage1.Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[1] { this.ribbonPageGroup1 });
		this.ribbonPage1.Name = "ribbonPage1";
		this.ribbonPage1.Text = "ribbonPage1";
		this.ribbonPageGroup1.Name = "ribbonPageGroup1";
		this.ribbonPageGroup1.Text = "ribbonPageGroup1";
		this.ribbonStatusBar1.ItemLinks.Add((BarItem)(object)this.printPreviewStaticItem1);
		this.ribbonStatusBar1.ItemLinks.Add((BarItem)(object)this.progressBarEditItem1);
		this.ribbonStatusBar1.ItemLinks.Add((BarItem)(object)this.printPreviewBarItem52);
		this.ribbonStatusBar1.ItemLinks.Add((BarItem)(object)this.commandBarItem129);
		this.ribbonStatusBar1.ItemLinks.Add((BarItem)(object)this.printPreviewStaticItem2, true);
		this.ribbonStatusBar1.ItemLinks.Add((BarItem)(object)this.zoomTrackBarEditItem1);
		((System.Windows.Forms.Control)(object)this.ribbonStatusBar1).Location = new System.Drawing.Point(0, 432);
		((System.Windows.Forms.Control)(object)this.ribbonStatusBar1).Name = "ribbonStatusBar1";
		this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
		((System.Windows.Forms.Control)(object)this.ribbonStatusBar1).Size = new System.Drawing.Size(984, 28);
		val.ClosingDelay = 500;
		((FileDialogBase)this.xtraSaveFileDialog1).FileName = "xtraSaveFileDialog1";
		this.reportDesigner1.ContainerControl = null;
		val2.DesignControl = (IDesignPanelListener)(object)this.xrDesignRibbonController1;
		val3.DesignControl = (IDesignPanelListener)(object)this.xrDesignDockManager1;
		val4.DesignControl = (IDesignPanelListener)(object)this.fieldListDockPanel1;
		val5.DesignControl = (IDesignPanelListener)(object)this.propertyGridDockPanel1;
		val6.DesignControl = (IDesignPanelListener)(object)this.reportExplorerDockPanel1;
		val7.DesignControl = (IDesignPanelListener)(object)this.reportGalleryDockPanel1;
		val8.DesignControl = (IDesignPanelListener)(object)this.groupAndSortDockPanel1;
		val9.DesignControl = (IDesignPanelListener)(object)this.errorListDockPanel1;
		this.reportDesigner1.DesignPanelListeners.AddRange((XRDesignPanelListener[])(object)new XRDesignPanelListener[8] { val2, val3, val4, val5, val6, val7, val8, val9 });
		this.reportDesigner1.Form = this;
		((RibbonControllerBase)this.xrDesignRibbonController1).RibbonControl = this.ribbonControl1;
		((RibbonControllerBase)this.xrDesignRibbonController1).RibbonStatusBar = this.ribbonStatusBar1;
		this.xrDesignRibbonController1.XRDesignDockManager = this.xrDesignDockManager1;
		((BarItem)this.commandBarItem1).Caption = "Designer";
		this.commandBarItem1.Command = (ReportCommand)23;
		((BarItem)this.commandBarItem1).Enabled = false;
		((BarItem)this.commandBarItem1).Id = 1;
		((BarItem)this.commandBarItem1).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.F4);
		((BarItem)this.commandBarItem1).Name = "commandBarItem1";
		val10.FixedTooltipWidth = true;
		((ToolTipItem)val11).Text = "Report Designer (F4)";
		val12.LeftIndent = 6;
		val12.Text = "Customize the report layout or create a new layout in the feature-rich Report Designer.";
		val10.Items.Add((BaseToolTipItem)(object)val11);
		val10.Items.Add((BaseToolTipItem)(object)val12);
		((BaseToolTipObject)val10).MaxWidth = 210;
		((BarItem)this.commandBarItem1).SuperTip = val10;
		((BarItem)this.commandBarItem2).Caption = "Preview";
		this.commandBarItem2.Command = (ReportCommand)25;
		((BarItem)this.commandBarItem2).Enabled = false;
		((BarItem)this.commandBarItem2).Id = 2;
		((BarItem)this.commandBarItem2).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.F5);
		((BarItem)this.commandBarItem2).Name = "commandBarItem2";
		val13.FixedTooltipWidth = true;
		((ToolTipItem)val14).Text = "Print Preview (F5)";
		val15.LeftIndent = 6;
		val15.Text = "Display a report populated with data and divided into pages, customize its print settings and print or export this report.";
		val13.Items.Add((BaseToolTipItem)(object)val14);
		val13.Items.Add((BaseToolTipItem)(object)val15);
		((BaseToolTipObject)val13).MaxWidth = 210;
		((BarItem)this.commandBarItem2).SuperTip = val13;
		((BarItem)this.commandBarItem3).Caption = "Scripts";
		this.commandBarItem3.Command = (ReportCommand)24;
		((BarItem)this.commandBarItem3).Enabled = false;
		((BarItem)this.commandBarItem3).Id = 3;
		((BarItem)this.commandBarItem3).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.F6);
		((BarItem)this.commandBarItem3).Name = "commandBarItem3";
		val16.FixedTooltipWidth = true;
		((ToolTipItem)val17).Text = "Scripts (F6)";
		val18.LeftIndent = 6;
		val18.Text = "Perform custom calculations by handling script events.";
		val16.Items.Add((BaseToolTipItem)(object)val17);
		val16.Items.Add((BaseToolTipItem)(object)val18);
		((BaseToolTipObject)val16).MaxWidth = 210;
		((BarItem)this.commandBarItem3).SuperTip = val16;
		((BarItem)this.commandBarItem4).Caption = "Undo";
		this.commandBarItem4.Command = (ReportCommand)27;
		((BarItem)this.commandBarItem4).Enabled = false;
		((BarItem)this.commandBarItem4).Id = 4;
		((BarItem)this.commandBarItem4).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Z | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem4).Name = "commandBarItem4";
		val19.FixedTooltipWidth = true;
		((ToolTipItem)val20).Text = "Undo (Ctrl+Z)";
		val21.LeftIndent = 6;
		val21.Text = "Undo the last operation.";
		val19.Items.Add((BaseToolTipItem)(object)val20);
		val19.Items.Add((BaseToolTipItem)(object)val21);
		((BaseToolTipObject)val19).MaxWidth = 210;
		((BarItem)this.commandBarItem4).SuperTip = val19;
		((BarItem)this.commandBarItem5).Caption = "Redo";
		this.commandBarItem5.Command = (ReportCommand)28;
		((BarItem)this.commandBarItem5).Enabled = false;
		((BarItem)this.commandBarItem5).Id = 5;
		((BarItem)this.commandBarItem5).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Y | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem5).Name = "commandBarItem5";
		val22.FixedTooltipWidth = true;
		((ToolTipItem)val23).Text = "Redo (Ctrl+Y)";
		val24.LeftIndent = 6;
		val24.Text = "Redo the last operation.";
		val22.Items.Add((BaseToolTipItem)(object)val23);
		val22.Items.Add((BaseToolTipItem)(object)val24);
		((BaseToolTipObject)val22).MaxWidth = 210;
		((BarItem)this.commandBarItem5).SuperTip = val22;
		((BarBaseButtonItem)this.commandBarItem6).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem6).Caption = "New Report";
		this.commandBarItem6.Command = (ReportCommand)2;
		((BarItem)this.commandBarItem6).Id = 6;
		((BarItem)this.commandBarItem6).Name = "commandBarItem6";
		val25.FixedTooltipWidth = true;
		((ToolTipItem)val26).Text = "New Blank Report";
		val27.LeftIndent = 6;
		val27.Text = "Create a new blank report.";
		val25.Items.Add((BaseToolTipItem)(object)val26);
		val25.Items.Add((BaseToolTipItem)(object)val27);
		((BaseToolTipObject)val25).MaxWidth = 210;
		((BarItem)this.commandBarItem6).SuperTip = val25;
		((BarBaseButtonItem)this.commandBarItem7).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem7).Caption = "Save";
		this.commandBarItem7.Command = (ReportCommand)5;
		((BarItem)this.commandBarItem7).Enabled = false;
		((BarItem)this.commandBarItem7).Id = 7;
		((BarItem)this.commandBarItem7).Name = "commandBarItem7";
		val28.FixedTooltipWidth = true;
		((ToolTipItem)val29).Text = "Save Report";
		val30.LeftIndent = 6;
		val30.Text = "Save the current report.";
		val28.Items.Add((BaseToolTipItem)(object)val29);
		val28.Items.Add((BaseToolTipItem)(object)val30);
		((BaseToolTipObject)val28).MaxWidth = 210;
		((BarItem)this.commandBarItem7).SuperTip = val28;
		((BarItem)this.commandBarItem8).Caption = "Save All";
		this.commandBarItem8.Command = (ReportCommand)7;
		((BarItem)this.commandBarItem8).Enabled = false;
		((BarItem)this.commandBarItem8).Id = 8;
		((BarItem)this.commandBarItem8).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.L | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem8).Name = "commandBarItem8";
		val31.FixedTooltipWidth = true;
		((ToolTipItem)val32).Text = "Save All Reports (Ctrl+L)";
		val33.LeftIndent = 6;
		val33.Text = "Save all modified reports.";
		val31.Items.Add((BaseToolTipItem)(object)val32);
		val31.Items.Add((BaseToolTipItem)(object)val33);
		((BaseToolTipObject)val31).MaxWidth = 210;
		((BarItem)this.commandBarItem8).SuperTip = val31;
		((BarItem)this.commandBarItem9).Caption = "Open...";
		this.commandBarItem9.Command = (ReportCommand)4;
		((BarItem)this.commandBarItem9).Id = 9;
		((BarItem)this.commandBarItem9).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem9).Name = "commandBarItem9";
		val34.FixedTooltipWidth = true;
		((ToolTipItem)val35).Text = "Open Report (Ctrl+O)";
		val36.LeftIndent = 6;
		val36.Text = "Open a report.";
		val34.Items.Add((BaseToolTipItem)(object)val35);
		val34.Items.Add((BaseToolTipItem)(object)val36);
		((BaseToolTipObject)val34).MaxWidth = 210;
		((BarItem)this.commandBarItem9).SuperTip = val34;
		((BarItem)this.commandBarItem10).Caption = "New Report";
		this.commandBarItem10.Command = (ReportCommand)2;
		((BarItem)this.commandBarItem10).Description = "Create a new blank report.";
		((BarItem)this.commandBarItem10).Id = 10;
		((BarItem)this.commandBarItem10).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.N | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem10).Name = "commandBarItem10";
		val37.FixedTooltipWidth = true;
		((ToolTipItem)val38).Text = "New Blank Report (Ctrl+N)";
		val39.LeftIndent = 6;
		val39.Text = "Create a new blank report.";
		val37.Items.Add((BaseToolTipItem)(object)val38);
		val37.Items.Add((BaseToolTipItem)(object)val39);
		((BaseToolTipObject)val37).MaxWidth = 210;
		((BarItem)this.commandBarItem10).SuperTip = val37;
		((BarItem)this.commandBarItem11).Caption = "New Report via Wizard...";
		this.commandBarItem11.Command = (ReportCommand)3;
		((BarItem)this.commandBarItem11).Description = "Launch the report wizard to create a new report.";
		((BarItem)this.commandBarItem11).Id = 11;
		((BarItem)this.commandBarItem11).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.W | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem11).Name = "commandBarItem11";
		val40.FixedTooltipWidth = true;
		((ToolTipItem)val41).Text = "New Report via Wizard (Ctrl+W)";
		val42.LeftIndent = 6;
		val42.Text = "Launch the report wizard to create a new report.";
		val40.Items.Add((BaseToolTipItem)(object)val41);
		val40.Items.Add((BaseToolTipItem)(object)val42);
		((BaseToolTipObject)val40).MaxWidth = 210;
		((BarItem)this.commandBarItem11).SuperTip = val40;
		((BarItem)this.commandBarItem12).Caption = "Save";
		this.commandBarItem12.Command = (ReportCommand)5;
		((BarItem)this.commandBarItem12).Description = "Save the current report.";
		((BarItem)this.commandBarItem12).Enabled = false;
		((BarItem)this.commandBarItem12).Id = 12;
		((BarItem)this.commandBarItem12).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem12).Name = "commandBarItem12";
		val43.FixedTooltipWidth = true;
		((ToolTipItem)val44).Text = "Save Report (Ctrl+S)";
		val45.LeftIndent = 6;
		val45.Text = "Save the current report.";
		val43.Items.Add((BaseToolTipItem)(object)val44);
		val43.Items.Add((BaseToolTipItem)(object)val45);
		((BaseToolTipObject)val43).MaxWidth = 210;
		((BarItem)this.commandBarItem12).SuperTip = val43;
		((BarItem)this.commandBarItem13).Caption = "Save As...";
		this.commandBarItem13.Command = (ReportCommand)6;
		((BarItem)this.commandBarItem13).Description = "Save the current report with a new name.";
		((BarItem)this.commandBarItem13).Enabled = false;
		((BarItem)this.commandBarItem13).Id = 13;
		((BarItem)this.commandBarItem13).Name = "commandBarItem13";
		val46.FixedTooltipWidth = true;
		((ToolTipItem)val47).Text = "Save Report As";
		val48.LeftIndent = 6;
		val48.Text = "Save the current report with a new name.";
		val46.Items.Add((BaseToolTipItem)(object)val47);
		val46.Items.Add((BaseToolTipItem)(object)val48);
		((BaseToolTipObject)val46).MaxWidth = 210;
		((BarItem)this.commandBarItem13).SuperTip = val46;
		((BarItem)this.commandBarItem14).Caption = "Paste";
		this.commandBarItem14.Command = (ReportCommand)76;
		((BarItem)this.commandBarItem14).Enabled = false;
		((BarItem)this.commandBarItem14).Id = 14;
		((BarItem)this.commandBarItem14).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.V | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem14).Name = "commandBarItem14";
		val49.FixedTooltipWidth = true;
		((ToolTipItem)val50).Text = "Paste (Ctrl+V)";
		val51.LeftIndent = 6;
		val51.Text = "Paste the contents of the Clipboard.";
		val49.Items.Add((BaseToolTipItem)(object)val50);
		val49.Items.Add((BaseToolTipItem)(object)val51);
		((BaseToolTipObject)val49).MaxWidth = 210;
		((BarItem)this.commandBarItem14).SuperTip = val49;
		((BarItem)this.commandBarItem15).Caption = "Cut";
		this.commandBarItem15.Command = (ReportCommand)74;
		((BarItem)this.commandBarItem15).Enabled = false;
		((BarItem)this.commandBarItem15).Id = 15;
		((BarItem)this.commandBarItem15).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.X | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem15).Name = "commandBarItem15";
		((BarItem)this.commandBarItem15).RibbonStyle = (RibbonItemStyles)2;
		val52.FixedTooltipWidth = true;
		((ToolTipItem)val53).Text = "Cut (Ctrl+X)";
		val54.LeftIndent = 6;
		val54.Text = "Cut the selected controls from the report and put them on the Clipboard.";
		val52.Items.Add((BaseToolTipItem)(object)val53);
		val52.Items.Add((BaseToolTipItem)(object)val54);
		((BaseToolTipObject)val52).MaxWidth = 210;
		((BarItem)this.commandBarItem15).SuperTip = val52;
		((BarItem)this.commandBarItem16).Caption = "Copy";
		this.commandBarItem16.Command = (ReportCommand)75;
		((BarItem)this.commandBarItem16).Enabled = false;
		((BarItem)this.commandBarItem16).Id = 16;
		((BarItem)this.commandBarItem16).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem16).Name = "commandBarItem16";
		((BarItem)this.commandBarItem16).RibbonStyle = (RibbonItemStyles)2;
		val55.FixedTooltipWidth = true;
		((ToolTipItem)val56).Text = "Copy (Ctrl+C)";
		val57.LeftIndent = 6;
		val57.Text = "Copy the selected controls and put them on the Clipboard.";
		val55.Items.Add((BaseToolTipItem)(object)val56);
		val55.Items.Add((BaseToolTipItem)(object)val57);
		((BaseToolTipObject)val55).MaxWidth = 210;
		((BarItem)this.commandBarItem16).SuperTip = val55;
		((RepositoryItemPopupBase)this.recentlyUsedItemsComboBox1).AppearanceDropDown.Font = new System.Drawing.Font("Tahoma", 11.25f);
		((RepositoryItemPopupBase)this.recentlyUsedItemsComboBox1).AppearanceDropDown.Options.UseFont = true;
		((RepositoryItem)this.recentlyUsedItemsComboBox1).AutoHeight = false;
		((RepositoryItemButtonEdit)this.recentlyUsedItemsComboBox1).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((RepositoryItem)this.recentlyUsedItemsComboBox1).Name = "recentlyUsedItemsComboBox1";
		this.barEditItem1.Edit = (RepositoryItem)(object)this.recentlyUsedItemsComboBox1;
		this.barEditItem1.EditWidth = 140;
		((BarItem)this.barEditItem1).Id = 17;
		((BarItem)this.barEditItem1).Name = "barEditItem1";
		val58.FixedTooltipWidth = true;
		((ToolTipItem)val59).Text = "Font";
		val60.LeftIndent = 6;
		val60.Text = "Change the font face.";
		val58.Items.Add((BaseToolTipItem)(object)val59);
		val58.Items.Add((BaseToolTipItem)(object)val60);
		((BaseToolTipObject)val58).MaxWidth = 210;
		((BarItem)this.barEditItem1).SuperTip = val58;
		((RepositoryItem)this.designRepositoryItemComboBox1).AutoHeight = false;
		((RepositoryItemButtonEdit)this.designRepositoryItemComboBox1).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((RepositoryItem)this.designRepositoryItemComboBox1).Name = "designRepositoryItemComboBox1";
		this.barEditItem2.Edit = (RepositoryItem)(object)this.designRepositoryItemComboBox1;
		this.barEditItem2.EditWidth = 55;
		((BarItem)this.barEditItem2).Id = 18;
		((BarItem)this.barEditItem2).Name = "barEditItem2";
		val61.FixedTooltipWidth = true;
		((ToolTipItem)val62).Text = "Font Size";
		val63.LeftIndent = 6;
		val63.Text = "Change the font size.";
		val61.Items.Add((BaseToolTipItem)(object)val62);
		val61.Items.Add((BaseToolTipItem)(object)val63);
		((BaseToolTipObject)val61).MaxWidth = 210;
		((BarItem)this.barEditItem2).SuperTip = val61;
		((BarItem)this.barDockPanelsListItem1).Caption = "Windows";
		this.barDockPanelsListItem1.DockManager = null;
		((BarItem)this.barDockPanelsListItem1).Id = 19;
		((BarItem)this.barDockPanelsListItem1).Name = "barDockPanelsListItem1";
		((BarToolbarsListItem)this.barDockPanelsListItem1).ShowCustomizationItem = false;
		((BarToolbarsListItem)this.barDockPanelsListItem1).ShowDockPanels = true;
		((BarToolbarsListItem)this.barDockPanelsListItem1).ShowToolbars = false;
		val64.FixedTooltipWidth = true;
		((ToolTipItem)val65).Text = "Show/Hide Windows";
		val66.LeftIndent = 6;
		val66.Text = "Change the visibility of dock panels that assist in report creation.";
		val64.Items.Add((BaseToolTipItem)(object)val65);
		val64.Items.Add((BaseToolTipItem)(object)val66);
		((BaseToolTipObject)val64).MaxWidth = 210;
		((BarItem)this.barDockPanelsListItem1).SuperTip = val64;
		((BarItem)this.commandBarItem17).Caption = "Bold";
		this.commandBarItem17.Command = (ReportCommand)102;
		((BarItem)this.commandBarItem17).Enabled = false;
		((BarItem)this.commandBarItem17).Id = 20;
		((BarItem)this.commandBarItem17).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.B | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem17).Name = "commandBarItem17";
		val67.FixedTooltipWidth = true;
		((ToolTipItem)val68).Text = "Bold (Ctrl+B)";
		val69.LeftIndent = 6;
		val69.Text = "Make the selected text bold.";
		val67.Items.Add((BaseToolTipItem)(object)val68);
		val67.Items.Add((BaseToolTipItem)(object)val69);
		((BaseToolTipObject)val67).MaxWidth = 210;
		((BarItem)this.commandBarItem17).SuperTip = val67;
		((BarItem)this.commandBarItem18).Caption = "Italic";
		this.commandBarItem18.Command = (ReportCommand)103;
		((BarItem)this.commandBarItem18).Enabled = false;
		((BarItem)this.commandBarItem18).Id = 21;
		((BarItem)this.commandBarItem18).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.I | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem18).Name = "commandBarItem18";
		val70.FixedTooltipWidth = true;
		((ToolTipItem)val71).Text = "Italic (Ctrl+I)";
		val72.LeftIndent = 6;
		val72.Text = "Italicize the text.";
		val70.Items.Add((BaseToolTipItem)(object)val71);
		val70.Items.Add((BaseToolTipItem)(object)val72);
		((BaseToolTipObject)val70).MaxWidth = 210;
		((BarItem)this.commandBarItem18).SuperTip = val70;
		((BarItem)this.commandBarItem19).Caption = "Underline";
		this.commandBarItem19.Command = (ReportCommand)104;
		((BarItem)this.commandBarItem19).Enabled = false;
		((BarItem)this.commandBarItem19).Id = 22;
		((BarItem)this.commandBarItem19).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.U | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem19).Name = "commandBarItem19";
		val73.FixedTooltipWidth = true;
		((ToolTipItem)val74).Text = "Underline (Ctrl+U)";
		val75.LeftIndent = 6;
		val75.Text = "Underline the selected text.";
		val73.Items.Add((BaseToolTipItem)(object)val74);
		val73.Items.Add((BaseToolTipItem)(object)val75);
		((BaseToolTipObject)val73).MaxWidth = 210;
		((BarItem)this.commandBarItem19).SuperTip = val73;
		((BarBaseButtonItem)this.commandColorBarItem1).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandColorBarItem1).Caption = "Foreground Color";
		((BarBaseButtonItem)this.commandColorBarItem1).CloseSubMenuOnClickMode = (DefaultBoolean)1;
		((CommandBarItem)this.commandColorBarItem1).Command = (ReportCommand)108;
		((BarItem)this.commandColorBarItem1).Enabled = false;
		((BarItem)this.commandColorBarItem1).Id = 23;
		((BarItem)this.commandColorBarItem1).Name = "commandColorBarItem1";
		val76.FixedTooltipWidth = true;
		((ToolTipItem)val77).Text = "Foreground Color";
		val78.LeftIndent = 6;
		val78.Text = "Change the text foreground color.";
		val76.Items.Add((BaseToolTipItem)(object)val77);
		val76.Items.Add((BaseToolTipItem)(object)val78);
		((BaseToolTipObject)val76).MaxWidth = 210;
		((BarItem)this.commandColorBarItem1).SuperTip = val76;
		((BarBaseButtonItem)this.commandColorBarItem2).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandColorBarItem2).Caption = "Background Color";
		((BarBaseButtonItem)this.commandColorBarItem2).CloseSubMenuOnClickMode = (DefaultBoolean)1;
		((CommandBarItem)this.commandColorBarItem2).Command = (ReportCommand)109;
		((BarItem)this.commandColorBarItem2).Enabled = false;
		((BarItem)this.commandColorBarItem2).Id = 24;
		((BarItem)this.commandColorBarItem2).Name = "commandColorBarItem2";
		val79.FixedTooltipWidth = true;
		((ToolTipItem)val80).Text = "Background Color";
		val81.LeftIndent = 6;
		val81.Text = "Change the text background color.";
		val79.Items.Add((BaseToolTipItem)(object)val80);
		val79.Items.Add((BaseToolTipItem)(object)val81);
		((BaseToolTipObject)val79).MaxWidth = 210;
		((BarItem)this.commandColorBarItem2).SuperTip = val79;
		((BarItem)this.commandBarItem20).Caption = "Align Text Left";
		this.commandBarItem20.Command = (ReportCommand)111;
		((BarItem)this.commandBarItem20).Enabled = false;
		((BarItem)this.commandBarItem20).Id = 25;
		((BarItem)this.commandBarItem20).Name = "commandBarItem20";
		val82.FixedTooltipWidth = true;
		((ToolTipItem)val83).Text = "Align Text Left";
		val84.LeftIndent = 6;
		val84.Text = "Align text to the left.";
		val82.Items.Add((BaseToolTipItem)(object)val83);
		val82.Items.Add((BaseToolTipItem)(object)val84);
		((BaseToolTipObject)val82).MaxWidth = 210;
		((BarItem)this.commandBarItem20).SuperTip = val82;
		((BarItem)this.commandBarItem21).Caption = "Center Text";
		this.commandBarItem21.Command = (ReportCommand)112;
		((BarItem)this.commandBarItem21).Enabled = false;
		((BarItem)this.commandBarItem21).Id = 26;
		((BarItem)this.commandBarItem21).Name = "commandBarItem21";
		val85.FixedTooltipWidth = true;
		((ToolTipItem)val86).Text = "Center Text";
		val87.LeftIndent = 6;
		val87.Text = "Center text between the left and right sides.";
		val85.Items.Add((BaseToolTipItem)(object)val86);
		val85.Items.Add((BaseToolTipItem)(object)val87);
		((BaseToolTipObject)val85).MaxWidth = 210;
		((BarItem)this.commandBarItem21).SuperTip = val85;
		((BarItem)this.commandBarItem22).Caption = "Align Text Right";
		this.commandBarItem22.Command = (ReportCommand)113;
		((BarItem)this.commandBarItem22).Enabled = false;
		((BarItem)this.commandBarItem22).Id = 27;
		((BarItem)this.commandBarItem22).Name = "commandBarItem22";
		val88.FixedTooltipWidth = true;
		((ToolTipItem)val89).Text = "Align Text Right";
		val90.LeftIndent = 6;
		val90.Text = "Align text to the right.";
		val88.Items.Add((BaseToolTipItem)(object)val89);
		val88.Items.Add((BaseToolTipItem)(object)val90);
		((BaseToolTipObject)val88).MaxWidth = 210;
		((BarItem)this.commandBarItem22).SuperTip = val88;
		((BarItem)this.commandBarItem23).Caption = "Justify";
		this.commandBarItem23.Command = (ReportCommand)114;
		((BarItem)this.commandBarItem23).Enabled = false;
		((BarItem)this.commandBarItem23).Id = 28;
		((BarItem)this.commandBarItem23).Name = "commandBarItem23";
		val91.FixedTooltipWidth = true;
		((ToolTipItem)val92).Text = "Justify";
		val93.LeftIndent = 6;
		val93.Text = "Distribute text evenly to the left and right sides.";
		val91.Items.Add((BaseToolTipItem)(object)val92);
		val91.Items.Add((BaseToolTipItem)(object)val93);
		((BaseToolTipObject)val91).MaxWidth = 210;
		((BarItem)this.commandBarItem23).SuperTip = val91;
		((BarItem)this.commandBarItem24).Caption = "Strikethrough";
		this.commandBarItem24.Command = (ReportCommand)105;
		((BarItem)this.commandBarItem24).Enabled = false;
		((BarItem)this.commandBarItem24).Id = 29;
		((BarItem)this.commandBarItem24).Name = "commandBarItem24";
		val94.FixedTooltipWidth = true;
		((ToolTipItem)val95).Text = "Strikethrough";
		val96.LeftIndent = 6;
		val96.Text = "Cross the selected text out by drawing a line through it.";
		val94.Items.Add((BaseToolTipItem)(object)val95);
		val94.Items.Add((BaseToolTipItem)(object)val96);
		((BaseToolTipObject)val94).MaxWidth = 210;
		((BarItem)this.commandBarItem24).SuperTip = val94;
		((BarItem)this.commandBarItem25).Caption = "Align Text Top";
		this.commandBarItem25.Command = (ReportCommand)115;
		((BarItem)this.commandBarItem25).Enabled = false;
		((BarItem)this.commandBarItem25).Id = 30;
		((BarItem)this.commandBarItem25).Name = "commandBarItem25";
		val97.FixedTooltipWidth = true;
		((ToolTipItem)val98).Text = "Align Text Top";
		val99.LeftIndent = 6;
		val99.Text = "Align text to the top.";
		val97.Items.Add((BaseToolTipItem)(object)val98);
		val97.Items.Add((BaseToolTipItem)(object)val99);
		((BaseToolTipObject)val97).MaxWidth = 210;
		((BarItem)this.commandBarItem25).SuperTip = val97;
		((BarItem)this.commandBarItem26).Caption = "Align Text Middle";
		this.commandBarItem26.Command = (ReportCommand)116;
		((BarItem)this.commandBarItem26).Enabled = false;
		((BarItem)this.commandBarItem26).Id = 31;
		((BarItem)this.commandBarItem26).Name = "commandBarItem26";
		val100.FixedTooltipWidth = true;
		((ToolTipItem)val101).Text = "Align Text Middle";
		val102.LeftIndent = 6;
		val102.Text = "Center text between the top and bottom.";
		val100.Items.Add((BaseToolTipItem)(object)val101);
		val100.Items.Add((BaseToolTipItem)(object)val102);
		((BaseToolTipObject)val100).MaxWidth = 210;
		((BarItem)this.commandBarItem26).SuperTip = val100;
		((BarItem)this.commandBarItem27).Caption = "Align Text Bottom";
		this.commandBarItem27.Command = (ReportCommand)117;
		((BarItem)this.commandBarItem27).Enabled = false;
		((BarItem)this.commandBarItem27).Id = 32;
		((BarItem)this.commandBarItem27).Name = "commandBarItem27";
		val103.FixedTooltipWidth = true;
		((ToolTipItem)val104).Text = "Align Text Bottom";
		val105.LeftIndent = 6;
		val105.Text = "Align text to the bottom.";
		val103.Items.Add((BaseToolTipItem)(object)val104);
		val103.Items.Add((BaseToolTipItem)(object)val105);
		((BaseToolTipObject)val103).MaxWidth = 210;
		((BarItem)this.commandBarItem27).SuperTip = val103;
		((BarItem)this.commandBarItem28).Caption = "Add Calculated Field";
		this.commandBarItem28.Command = (ReportCommand)17;
		((BarItem)this.commandBarItem28).Enabled = false;
		((BarItem)this.commandBarItem28).Id = 33;
		((BarItem)this.commandBarItem28).Name = "commandBarItem28";
		((BarItem)this.commandBarItem28).RibbonStyle = (RibbonItemStyles)2;
		val106.FixedTooltipWidth = true;
		((ToolTipItem)val107).Text = "Add Calculated Field";
		val108.LeftIndent = 6;
		val108.Text = "Create a custom field whose value is evaluated using an expression based on available data fields.";
		val106.Items.Add((BaseToolTipItem)(object)val107);
		val106.Items.Add((BaseToolTipItem)(object)val108);
		((BaseToolTipObject)val106).MaxWidth = 210;
		((BarItem)this.commandBarItem28).SuperTip = val106;
		((BarItem)this.commandBarItem29).Caption = "Add Parameter";
		this.commandBarItem29.Command = (ReportCommand)20;
		((BarItem)this.commandBarItem29).Enabled = false;
		((BarItem)this.commandBarItem29).Id = 34;
		((BarItem)this.commandBarItem29).Name = "commandBarItem29";
		((BarItem)this.commandBarItem29).RibbonStyle = (RibbonItemStyles)2;
		val109.FixedTooltipWidth = true;
		((ToolTipItem)val110).Text = "Add Parameter";
		val111.LeftIndent = 6;
		val111.Text = "Create a new parameter to pass dynamic values to your report.";
		val109.Items.Add((BaseToolTipItem)(object)val110);
		val109.Items.Add((BaseToolTipItem)(object)val111);
		((BaseToolTipObject)val109).MaxWidth = 210;
		((BarItem)this.commandBarItem29).SuperTip = val109;
		((BarItem)this.commandBarItem30).Caption = "Add Data Source";
		this.commandBarItem30.Command = (ReportCommand)15;
		((BarItem)this.commandBarItem30).Enabled = false;
		((BarItem)this.commandBarItem30).Id = 35;
		((BarItem)this.commandBarItem30).Name = "commandBarItem30";
		val112.FixedTooltipWidth = true;
		((ToolTipItem)val113).Text = "Add Data Source";
		val114.LeftIndent = 6;
		val114.Text = "Create and set up a new data source for your report.";
		val112.Items.Add((BaseToolTipItem)(object)val113);
		val112.Items.Add((BaseToolTipItem)(object)val114);
		((BaseToolTipObject)val112).MaxWidth = 210;
		((BarItem)this.commandBarItem30).SuperTip = val112;
		((BarItem)this.commandBarItem31).Caption = "Extract Style";
		this.commandBarItem31.Command = (ReportCommand)50;
		((BarItem)this.commandBarItem31).Enabled = false;
		((BarItem)this.commandBarItem31).Id = 36;
		((BarItem)this.commandBarItem31).Name = "commandBarItem31";
		val115.FixedTooltipWidth = true;
		((ToolTipItem)val116).Text = "Extract Style";
		val117.LeftIndent = 6;
		val117.Text = "Create a new style based on the selected control's appearance settings. Apply the created style from the Styles gallery to other controls in your report.";
		val115.Items.Add((BaseToolTipItem)(object)val116);
		val115.Items.Add((BaseToolTipItem)(object)val117);
		((BaseToolTipObject)val115).MaxWidth = 210;
		((BarItem)this.commandBarItem31).SuperTip = val115;
		((BarItem)this.commandBarItem32).Caption = "All Borders";
		this.commandBarItem32.Command = (ReportCommand)118;
		((BarItem)this.commandBarItem32).Enabled = false;
		((BarItem)this.commandBarItem32).Id = 37;
		((BarItem)this.commandBarItem32).Name = "commandBarItem32";
		val118.FixedTooltipWidth = true;
		((ToolTipItem)val119).Text = "All Borders";
		val120.LeftIndent = 6;
		val120.Text = "Add all borders to the selected controls.";
		val118.Items.Add((BaseToolTipItem)(object)val119);
		val118.Items.Add((BaseToolTipItem)(object)val120);
		((BaseToolTipObject)val118).MaxWidth = 210;
		((BarItem)this.commandBarItem32).SuperTip = val118;
		((BarItem)this.commandBarItem33).Caption = "No Border";
		this.commandBarItem33.Command = (ReportCommand)119;
		((BarItem)this.commandBarItem33).Enabled = false;
		((BarItem)this.commandBarItem33).Id = 38;
		((BarItem)this.commandBarItem33).Name = "commandBarItem33";
		val121.FixedTooltipWidth = true;
		((ToolTipItem)val122).Text = "No Border";
		val123.LeftIndent = 6;
		val123.Text = "Remove borders from the selected controls.";
		val121.Items.Add((BaseToolTipItem)(object)val122);
		val121.Items.Add((BaseToolTipItem)(object)val123);
		((BaseToolTipObject)val121).MaxWidth = 210;
		((BarItem)this.commandBarItem33).SuperTip = val121;
		((BarItem)this.commandBarItem34).Caption = "Left Border";
		this.commandBarItem34.Command = (ReportCommand)120;
		((BarItem)this.commandBarItem34).Enabled = false;
		((BarItem)this.commandBarItem34).Id = 39;
		((BarItem)this.commandBarItem34).Name = "commandBarItem34";
		val124.FixedTooltipWidth = true;
		((ToolTipItem)val125).Text = "Left Border";
		val126.LeftIndent = 6;
		val126.Text = "Add the left border to the selected controls.";
		val124.Items.Add((BaseToolTipItem)(object)val125);
		val124.Items.Add((BaseToolTipItem)(object)val126);
		((BaseToolTipObject)val124).MaxWidth = 210;
		((BarItem)this.commandBarItem34).SuperTip = val124;
		((BarItem)this.commandBarItem35).Caption = "Top Border";
		this.commandBarItem35.Command = (ReportCommand)121;
		((BarItem)this.commandBarItem35).Enabled = false;
		((BarItem)this.commandBarItem35).Id = 40;
		((BarItem)this.commandBarItem35).Name = "commandBarItem35";
		val127.FixedTooltipWidth = true;
		((ToolTipItem)val128).Text = "Top Border";
		val129.LeftIndent = 6;
		val129.Text = "Add the top border to the selected controls.";
		val127.Items.Add((BaseToolTipItem)(object)val128);
		val127.Items.Add((BaseToolTipItem)(object)val129);
		((BaseToolTipObject)val127).MaxWidth = 210;
		((BarItem)this.commandBarItem35).SuperTip = val127;
		((BarItem)this.commandBarItem36).Caption = "Right Border";
		this.commandBarItem36.Command = (ReportCommand)122;
		((BarItem)this.commandBarItem36).Enabled = false;
		((BarItem)this.commandBarItem36).Id = 41;
		((BarItem)this.commandBarItem36).Name = "commandBarItem36";
		val130.FixedTooltipWidth = true;
		((ToolTipItem)val131).Text = "Right Border";
		val132.LeftIndent = 6;
		val132.Text = "Add the right border to the selected controls.";
		val130.Items.Add((BaseToolTipItem)(object)val131);
		val130.Items.Add((BaseToolTipItem)(object)val132);
		((BaseToolTipObject)val130).MaxWidth = 210;
		((BarItem)this.commandBarItem36).SuperTip = val130;
		((BarItem)this.commandBarItem37).Caption = "Bottom Border";
		this.commandBarItem37.Command = (ReportCommand)123;
		((BarItem)this.commandBarItem37).Enabled = false;
		((BarItem)this.commandBarItem37).Id = 42;
		((BarItem)this.commandBarItem37).Name = "commandBarItem37";
		val133.FixedTooltipWidth = true;
		((ToolTipItem)val134).Text = "Bottom Border";
		val135.LeftIndent = 6;
		val135.Text = "Add the bottom border to the selected controls.";
		val133.Items.Add((BaseToolTipItem)(object)val134);
		val133.Items.Add((BaseToolTipItem)(object)val135);
		((BaseToolTipObject)val133).MaxWidth = 210;
		((BarItem)this.commandBarItem37).SuperTip = val133;
		((BarBaseButtonItem)this.commandColorBarItem3).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandColorBarItem3).Caption = "Border Color";
		((BarBaseButtonItem)this.commandColorBarItem3).CloseSubMenuOnClickMode = (DefaultBoolean)1;
		((CommandBarItem)this.commandColorBarItem3).Command = (ReportCommand)124;
		((BarItem)this.commandColorBarItem3).Enabled = false;
		((BarItem)this.commandColorBarItem3).Id = 43;
		((BarItem)this.commandColorBarItem3).Name = "commandColorBarItem3";
		val136.FixedTooltipWidth = true;
		((ToolTipItem)val137).Text = "Border Color";
		val138.LeftIndent = 6;
		val138.Text = "Change the border color.";
		val136.Items.Add((BaseToolTipItem)(object)val137);
		val136.Items.Add((BaseToolTipItem)(object)val138);
		((BaseToolTipObject)val136).MaxWidth = 210;
		((BarItem)this.commandColorBarItem3).SuperTip = val136;
		((BarButtonItem)this.commandBarItem38).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem38).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem38).Caption = "Border Width";
		this.commandBarItem38.Command = (ReportCommand)125;
		((BarItem)this.commandBarItem38).Enabled = false;
		((BarItem)this.commandBarItem38).Id = 44;
		((BarItem)this.commandBarItem38).Name = "commandBarItem38";
		val139.FixedTooltipWidth = true;
		((ToolTipItem)val140).Text = "Border Width";
		val141.LeftIndent = 6;
		val141.Text = "Change the border width.";
		val139.Items.Add((BaseToolTipItem)(object)val140);
		val139.Items.Add((BaseToolTipItem)(object)val141);
		((BaseToolTipObject)val139).MaxWidth = 210;
		((BarItem)this.commandBarItem38).SuperTip = val139;
		((BarItem)this.commandBarItem39).Caption = "Align to Grid";
		this.commandBarItem39.Command = (ReportCommand)85;
		((BarItem)this.commandBarItem39).Enabled = false;
		((BarItem)this.commandBarItem39).Id = 45;
		((BarItem)this.commandBarItem39).Name = "commandBarItem39";
		val142.FixedTooltipWidth = true;
		((ToolTipItem)val143).Text = "Align to Grid";
		val144.LeftIndent = 6;
		val144.Text = "Align the positions of the selected controls to the grid.";
		val142.Items.Add((BaseToolTipItem)(object)val143);
		val142.Items.Add((BaseToolTipItem)(object)val144);
		((BaseToolTipObject)val142).MaxWidth = 210;
		((BarItem)this.commandBarItem39).SuperTip = val142;
		((BarItem)this.commandBarItem40).Caption = "Align Lefts";
		this.commandBarItem40.Command = (ReportCommand)79;
		((BarItem)this.commandBarItem40).Enabled = false;
		((BarItem)this.commandBarItem40).Id = 46;
		((BarItem)this.commandBarItem40).Name = "commandBarItem40";
		val145.FixedTooltipWidth = true;
		((ToolTipItem)val146).Text = "Align Lefts";
		val147.LeftIndent = 6;
		val147.Text = "Left align the selected controls.";
		val145.Items.Add((BaseToolTipItem)(object)val146);
		val145.Items.Add((BaseToolTipItem)(object)val147);
		((BaseToolTipObject)val145).MaxWidth = 210;
		((BarItem)this.commandBarItem40).SuperTip = val145;
		((BarItem)this.commandBarItem41).Caption = "Align Centers";
		this.commandBarItem41.Command = (ReportCommand)83;
		((BarItem)this.commandBarItem41).Enabled = false;
		((BarItem)this.commandBarItem41).Id = 47;
		((BarItem)this.commandBarItem41).Name = "commandBarItem41";
		val148.FixedTooltipWidth = true;
		((ToolTipItem)val149).Text = "Align Centers";
		val150.LeftIndent = 6;
		val150.Text = "Align the centers of the selected controls vertically.";
		val148.Items.Add((BaseToolTipItem)(object)val149);
		val148.Items.Add((BaseToolTipItem)(object)val150);
		((BaseToolTipObject)val148).MaxWidth = 210;
		((BarItem)this.commandBarItem41).SuperTip = val148;
		((BarItem)this.commandBarItem42).Caption = "Align Rights";
		this.commandBarItem42.Command = (ReportCommand)81;
		((BarItem)this.commandBarItem42).Enabled = false;
		((BarItem)this.commandBarItem42).Id = 48;
		((BarItem)this.commandBarItem42).Name = "commandBarItem42";
		val151.FixedTooltipWidth = true;
		((ToolTipItem)val152).Text = "Align Rights";
		val153.LeftIndent = 6;
		val153.Text = "Right align the selected controls.";
		val151.Items.Add((BaseToolTipItem)(object)val152);
		val151.Items.Add((BaseToolTipItem)(object)val153);
		((BaseToolTipObject)val151).MaxWidth = 210;
		((BarItem)this.commandBarItem42).SuperTip = val151;
		((BarItem)this.commandBarItem43).Caption = "Align Tops";
		this.commandBarItem43.Command = (ReportCommand)80;
		((BarItem)this.commandBarItem43).Enabled = false;
		((BarItem)this.commandBarItem43).Id = 49;
		((BarItem)this.commandBarItem43).Name = "commandBarItem43";
		val154.FixedTooltipWidth = true;
		((ToolTipItem)val155).Text = "Align Tops";
		val156.LeftIndent = 6;
		val156.Text = "Align the tops of the selected controls.";
		val154.Items.Add((BaseToolTipItem)(object)val155);
		val154.Items.Add((BaseToolTipItem)(object)val156);
		((BaseToolTipObject)val154).MaxWidth = 210;
		((BarItem)this.commandBarItem43).SuperTip = val154;
		((BarItem)this.commandBarItem44).Caption = "Align Middles";
		this.commandBarItem44.Command = (ReportCommand)84;
		((BarItem)this.commandBarItem44).Enabled = false;
		((BarItem)this.commandBarItem44).Id = 50;
		((BarItem)this.commandBarItem44).Name = "commandBarItem44";
		val157.FixedTooltipWidth = true;
		((ToolTipItem)val158).Text = "Align Middles";
		val159.LeftIndent = 6;
		val159.Text = "Align the centers of the selected controls horizontally.";
		val157.Items.Add((BaseToolTipItem)(object)val158);
		val157.Items.Add((BaseToolTipItem)(object)val159);
		((BaseToolTipObject)val157).MaxWidth = 210;
		((BarItem)this.commandBarItem44).SuperTip = val157;
		((BarItem)this.commandBarItem45).Caption = "Align Bottoms";
		this.commandBarItem45.Command = (ReportCommand)82;
		((BarItem)this.commandBarItem45).Enabled = false;
		((BarItem)this.commandBarItem45).Id = 51;
		((BarItem)this.commandBarItem45).Name = "commandBarItem45";
		val160.FixedTooltipWidth = true;
		((ToolTipItem)val161).Text = "Align Bottoms";
		val162.LeftIndent = 6;
		val162.Text = "Align the bottoms of the selected controls.";
		val160.Items.Add((BaseToolTipItem)(object)val161);
		val160.Items.Add((BaseToolTipItem)(object)val162);
		((BaseToolTipObject)val160).MaxWidth = 210;
		((BarItem)this.commandBarItem45).SuperTip = val160;
		((BarItem)this.commandBarItem46).Caption = "Make Same Width";
		this.commandBarItem46.Command = (ReportCommand)89;
		((BarItem)this.commandBarItem46).Enabled = false;
		((BarItem)this.commandBarItem46).Id = 52;
		((BarItem)this.commandBarItem46).Name = "commandBarItem46";
		val163.FixedTooltipWidth = true;
		((ToolTipItem)val164).Text = "Make Same Width";
		val165.LeftIndent = 6;
		val165.Text = "Make the selected controls have the same width.";
		val163.Items.Add((BaseToolTipItem)(object)val164);
		val163.Items.Add((BaseToolTipItem)(object)val165);
		((BaseToolTipObject)val163).MaxWidth = 210;
		((BarItem)this.commandBarItem46).SuperTip = val163;
		((BarItem)this.commandBarItem47).Caption = "Fit Bounds to Grid";
		this.commandBarItem47.Command = (ReportCommand)86;
		((BarItem)this.commandBarItem47).Enabled = false;
		((BarItem)this.commandBarItem47).Id = 53;
		((BarItem)this.commandBarItem47).Name = "commandBarItem47";
		val166.FixedTooltipWidth = true;
		((ToolTipItem)val167).Text = "Size to Grid";
		val168.LeftIndent = 6;
		val168.Text = "Adjust the size of the selected controls according to the grid size.";
		val166.Items.Add((BaseToolTipItem)(object)val167);
		val166.Items.Add((BaseToolTipItem)(object)val168);
		((BaseToolTipObject)val166).MaxWidth = 210;
		((BarItem)this.commandBarItem47).SuperTip = val166;
		((BarItem)this.commandBarItem48).Caption = "Make Same Height";
		this.commandBarItem48.Command = (ReportCommand)88;
		((BarItem)this.commandBarItem48).Enabled = false;
		((BarItem)this.commandBarItem48).Id = 54;
		((BarItem)this.commandBarItem48).Name = "commandBarItem48";
		val169.FixedTooltipWidth = true;
		((ToolTipItem)val170).Text = "Make Same Height";
		val171.LeftIndent = 6;
		val171.Text = "Make the selected controls have the same height.";
		val169.Items.Add((BaseToolTipItem)(object)val170);
		val169.Items.Add((BaseToolTipItem)(object)val171);
		((BaseToolTipObject)val169).MaxWidth = 210;
		((BarItem)this.commandBarItem48).SuperTip = val169;
		((BarItem)this.commandBarItem49).Caption = "Make Same Size";
		this.commandBarItem49.Command = (ReportCommand)87;
		((BarItem)this.commandBarItem49).Enabled = false;
		((BarItem)this.commandBarItem49).Id = 55;
		((BarItem)this.commandBarItem49).Name = "commandBarItem49";
		val172.FixedTooltipWidth = true;
		((ToolTipItem)val173).Text = "Make Same Size";
		val174.LeftIndent = 6;
		val174.Text = "Make the selected controls have the same size.";
		val172.Items.Add((BaseToolTipItem)(object)val173);
		val172.Items.Add((BaseToolTipItem)(object)val174);
		((BaseToolTipObject)val172).MaxWidth = 210;
		((BarItem)this.commandBarItem49).SuperTip = val172;
		((BarItem)this.commandBarItem50).Caption = "Make Horizontal Spacing Equal";
		this.commandBarItem50.Command = (ReportCommand)93;
		((BarItem)this.commandBarItem50).Enabled = false;
		((BarItem)this.commandBarItem50).Id = 56;
		((BarItem)this.commandBarItem50).Name = "commandBarItem50";
		val175.FixedTooltipWidth = true;
		((ToolTipItem)val176).Text = "Make Horizontal Spacing Equal";
		val177.LeftIndent = 6;
		val177.Text = "Make the horizontal spacing between the selected controls equal.";
		val175.Items.Add((BaseToolTipItem)(object)val176);
		val175.Items.Add((BaseToolTipItem)(object)val177);
		((BaseToolTipObject)val175).MaxWidth = 210;
		((BarItem)this.commandBarItem50).SuperTip = val175;
		((BarItem)this.commandBarItem51).Caption = "Increase Horizontal Spacing";
		this.commandBarItem51.Command = (ReportCommand)92;
		((BarItem)this.commandBarItem51).Enabled = false;
		((BarItem)this.commandBarItem51).Id = 57;
		((BarItem)this.commandBarItem51).Name = "commandBarItem51";
		val178.FixedTooltipWidth = true;
		((ToolTipItem)val179).Text = "Increase Horizontal Spacing";
		val180.LeftIndent = 6;
		val180.Text = "Increase the horizontal spacing between the selected controls.";
		val178.Items.Add((BaseToolTipItem)(object)val179);
		val178.Items.Add((BaseToolTipItem)(object)val180);
		((BaseToolTipObject)val178).MaxWidth = 210;
		((BarItem)this.commandBarItem51).SuperTip = val178;
		((BarItem)this.commandBarItem52).Caption = "Decrease Horizontal Spacing";
		this.commandBarItem52.Command = (ReportCommand)91;
		((BarItem)this.commandBarItem52).Enabled = false;
		((BarItem)this.commandBarItem52).Id = 58;
		((BarItem)this.commandBarItem52).Name = "commandBarItem52";
		val181.FixedTooltipWidth = true;
		((ToolTipItem)val182).Text = "Decrease Horizontal Spacing";
		val183.LeftIndent = 6;
		val183.Text = "Decrease the horizontal spacing between the selected controls.";
		val181.Items.Add((BaseToolTipItem)(object)val182);
		val181.Items.Add((BaseToolTipItem)(object)val183);
		((BaseToolTipObject)val181).MaxWidth = 210;
		((BarItem)this.commandBarItem52).SuperTip = val181;
		((BarItem)this.commandBarItem53).Caption = "Remove Horizontal Spacing";
		this.commandBarItem53.Command = (ReportCommand)90;
		((BarItem)this.commandBarItem53).Enabled = false;
		((BarItem)this.commandBarItem53).Id = 59;
		((BarItem)this.commandBarItem53).Name = "commandBarItem53";
		val184.FixedTooltipWidth = true;
		((ToolTipItem)val185).Text = "Remove Horizontal Spacing";
		val186.LeftIndent = 6;
		val186.Text = "Remove the horizontal spacing between the selected controls.";
		val184.Items.Add((BaseToolTipItem)(object)val185);
		val184.Items.Add((BaseToolTipItem)(object)val186);
		((BaseToolTipObject)val184).MaxWidth = 210;
		((BarItem)this.commandBarItem53).SuperTip = val184;
		((BarItem)this.commandBarItem54).Caption = "Make Vertical Spacing Equal";
		this.commandBarItem54.Command = (ReportCommand)97;
		((BarItem)this.commandBarItem54).Enabled = false;
		((BarItem)this.commandBarItem54).Id = 60;
		((BarItem)this.commandBarItem54).Name = "commandBarItem54";
		val187.FixedTooltipWidth = true;
		((ToolTipItem)val188).Text = "Make Vertical Spacing Equal";
		val189.LeftIndent = 6;
		val189.Text = "Make the vertical spacing between the selected controls equal.";
		val187.Items.Add((BaseToolTipItem)(object)val188);
		val187.Items.Add((BaseToolTipItem)(object)val189);
		((BaseToolTipObject)val187).MaxWidth = 210;
		((BarItem)this.commandBarItem54).SuperTip = val187;
		((BarItem)this.commandBarItem55).Caption = "Increase Vertical Spacing";
		this.commandBarItem55.Command = (ReportCommand)96;
		((BarItem)this.commandBarItem55).Enabled = false;
		((BarItem)this.commandBarItem55).Id = 61;
		((BarItem)this.commandBarItem55).Name = "commandBarItem55";
		val190.FixedTooltipWidth = true;
		((ToolTipItem)val191).Text = "Increase Vertical Spacing";
		val192.LeftIndent = 6;
		val192.Text = "Increase the vertical spacing between the selected controls.";
		val190.Items.Add((BaseToolTipItem)(object)val191);
		val190.Items.Add((BaseToolTipItem)(object)val192);
		((BaseToolTipObject)val190).MaxWidth = 210;
		((BarItem)this.commandBarItem55).SuperTip = val190;
		((BarItem)this.commandBarItem56).Caption = "Decrease Vertical Spacing";
		this.commandBarItem56.Command = (ReportCommand)95;
		((BarItem)this.commandBarItem56).Enabled = false;
		((BarItem)this.commandBarItem56).Id = 62;
		((BarItem)this.commandBarItem56).Name = "commandBarItem56";
		val193.FixedTooltipWidth = true;
		((ToolTipItem)val194).Text = "Decrease Vertical Spacing";
		val195.LeftIndent = 6;
		val195.Text = "Decrease the vertical spacing between the selected controls.";
		val193.Items.Add((BaseToolTipItem)(object)val194);
		val193.Items.Add((BaseToolTipItem)(object)val195);
		((BaseToolTipObject)val193).MaxWidth = 210;
		((BarItem)this.commandBarItem56).SuperTip = val193;
		((BarItem)this.commandBarItem57).Caption = "Remove Vertical Spacing";
		this.commandBarItem57.Command = (ReportCommand)94;
		((BarItem)this.commandBarItem57).Enabled = false;
		((BarItem)this.commandBarItem57).Id = 63;
		((BarItem)this.commandBarItem57).Name = "commandBarItem57";
		val196.FixedTooltipWidth = true;
		((ToolTipItem)val197).Text = "Remove Vertical Spacing";
		val198.LeftIndent = 6;
		val198.Text = "Remove the vertical spacing between the selected controls.";
		val196.Items.Add((BaseToolTipItem)(object)val197);
		val196.Items.Add((BaseToolTipItem)(object)val198);
		((BaseToolTipObject)val196).MaxWidth = 210;
		((BarItem)this.commandBarItem57).SuperTip = val196;
		((BarItem)this.commandBarItem58).Caption = "Center Horizontally";
		this.commandBarItem58.Command = (ReportCommand)99;
		((BarItem)this.commandBarItem58).Enabled = false;
		((BarItem)this.commandBarItem58).Id = 64;
		((BarItem)this.commandBarItem58).Name = "commandBarItem58";
		val199.FixedTooltipWidth = true;
		((ToolTipItem)val200).Text = "Center Horizontally";
		val201.LeftIndent = 6;
		val201.Text = "Horizontally center the selected controls within a band.";
		val199.Items.Add((BaseToolTipItem)(object)val200);
		val199.Items.Add((BaseToolTipItem)(object)val201);
		((BaseToolTipObject)val199).MaxWidth = 210;
		((BarItem)this.commandBarItem58).SuperTip = val199;
		((BarItem)this.commandBarItem59).Caption = "Center Vertically";
		this.commandBarItem59.Command = (ReportCommand)98;
		((BarItem)this.commandBarItem59).Enabled = false;
		((BarItem)this.commandBarItem59).Id = 65;
		((BarItem)this.commandBarItem59).Name = "commandBarItem59";
		val202.FixedTooltipWidth = true;
		((ToolTipItem)val203).Text = "Center Vertically";
		val204.LeftIndent = 6;
		val204.Text = "Vertically center the selected controls within a band.";
		val202.Items.Add((BaseToolTipItem)(object)val203);
		val202.Items.Add((BaseToolTipItem)(object)val204);
		((BaseToolTipObject)val202).MaxWidth = 210;
		((BarItem)this.commandBarItem59).SuperTip = val202;
		((BarItem)this.commandBarItem60).Caption = "Bring to Front";
		this.commandBarItem60.Command = (ReportCommand)100;
		((BarItem)this.commandBarItem60).Enabled = false;
		((BarItem)this.commandBarItem60).Id = 66;
		((BarItem)this.commandBarItem60).Name = "commandBarItem60";
		val205.FixedTooltipWidth = true;
		((ToolTipItem)val206).Text = "Bring to Front";
		val207.LeftIndent = 6;
		val207.Text = "Bring the selected controls to the front.";
		val205.Items.Add((BaseToolTipItem)(object)val206);
		val205.Items.Add((BaseToolTipItem)(object)val207);
		((BaseToolTipObject)val205).MaxWidth = 210;
		((BarItem)this.commandBarItem60).SuperTip = val205;
		((BarItem)this.commandBarItem61).Caption = "Send to Back";
		this.commandBarItem61.Command = (ReportCommand)101;
		((BarItem)this.commandBarItem61).Enabled = false;
		((BarItem)this.commandBarItem61).Id = 67;
		((BarItem)this.commandBarItem61).Name = "commandBarItem61";
		val208.FixedTooltipWidth = true;
		((ToolTipItem)val209).Text = "Send to Back";
		val210.LeftIndent = 6;
		val210.Text = "Move the selected controls to the back.";
		val208.Items.Add((BaseToolTipItem)(object)val209);
		val208.Items.Add((BaseToolTipItem)(object)val210);
		((BaseToolTipObject)val208).MaxWidth = 210;
		((BarItem)this.commandBarItem61).SuperTip = val208;
		((BarItem)this.commandBarCheckItem1).Caption = "Snap to Grid";
		((BarCheckItem)this.commandBarCheckItem1).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem1.Command = (ReportCommand)42;
		((BarItem)this.commandBarCheckItem1).Enabled = false;
		((BarItem)this.commandBarCheckItem1).Id = 68;
		((BarItem)this.commandBarCheckItem1).Name = "commandBarCheckItem1";
		val211.FixedTooltipWidth = true;
		((ToolTipItem)val212).Text = "Snap to Grid";
		val213.LeftIndent = 6;
		val213.Text = "Enable snapping to the snap grid.";
		val211.Items.Add((BaseToolTipItem)(object)val212);
		val211.Items.Add((BaseToolTipItem)(object)val213);
		((BaseToolTipObject)val211).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem1).SuperTip = val211;
		((BarItem)this.commandBarCheckItem2).Caption = "Snap Lines";
		((BarCheckItem)this.commandBarCheckItem2).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem2.Command = (ReportCommand)43;
		((BarItem)this.commandBarCheckItem2).Enabled = false;
		((BarItem)this.commandBarCheckItem2).Id = 69;
		((BarItem)this.commandBarCheckItem2).Name = "commandBarCheckItem2";
		val214.FixedTooltipWidth = true;
		((ToolTipItem)val215).Text = "Snap Lines";
		val216.LeftIndent = 6;
		val216.Text = "Enable snapping to snap lines.";
		val214.Items.Add((BaseToolTipItem)(object)val215);
		val214.Items.Add((BaseToolTipItem)(object)val216);
		((BaseToolTipObject)val214).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem2).SuperTip = val214;
		((BarItem)this.commandBarItem62).Caption = "Fit Bounds to Container";
		this.commandBarItem62.Command = (ReportCommand)73;
		((BarItem)this.commandBarItem62).Enabled = false;
		((BarItem)this.commandBarItem62).Id = 70;
		((BarItem)this.commandBarItem62).Name = "commandBarItem62";
		val217.FixedTooltipWidth = true;
		((ToolTipItem)val218).Text = "Fit Bounds to Container";
		val219.LeftIndent = 6;
		val219.Text = "Adjust the control size to occupy all the available container space.";
		val217.Items.Add((BaseToolTipItem)(object)val218);
		val217.Items.Add((BaseToolTipItem)(object)val219);
		((BaseToolTipObject)val217).MaxWidth = 210;
		((BarItem)this.commandBarItem62).SuperTip = val217;
		((BarButtonItem)this.commandBarItem63).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem63).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem63).Caption = "Size";
		this.commandBarItem63.Command = (ReportCommand)44;
		((BarItem)this.commandBarItem63).Enabled = false;
		((BarItem)this.commandBarItem63).Id = 71;
		((BarItem)this.commandBarItem63).Name = "commandBarItem63";
		val220.FixedTooltipWidth = true;
		((ToolTipItem)val221).Text = "Choose Page Size";
		val222.LeftIndent = 6;
		val222.Text = "Choose a commonly used paper size for a report or click More Paper Sizes to define your own size.";
		val220.Items.Add((BaseToolTipItem)(object)val221);
		val220.Items.Add((BaseToolTipItem)(object)val222);
		((BaseToolTipObject)val220).MaxWidth = 210;
		((BarItem)this.commandBarItem63).SuperTip = val220;
		((BarButtonItem)this.commandBarItem64).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem64).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem64).Caption = "Orientation";
		this.commandBarItem64.Command = (ReportCommand)45;
		((BarItem)this.commandBarItem64).Enabled = false;
		((BarItem)this.commandBarItem64).Id = 72;
		((BarItem)this.commandBarItem64).Name = "commandBarItem64";
		val223.FixedTooltipWidth = true;
		((ToolTipItem)val224).Text = "Change Page Orientation";
		val225.LeftIndent = 6;
		val225.Text = "Switch between portrait and landscape page layouts.";
		val223.Items.Add((BaseToolTipItem)(object)val224);
		val223.Items.Add((BaseToolTipItem)(object)val225);
		((BaseToolTipObject)val223).MaxWidth = 210;
		((BarItem)this.commandBarItem64).SuperTip = val223;
		((BarButtonItem)this.commandBarItem65).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem65).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem65).Caption = "Margins";
		this.commandBarItem65.Command = (ReportCommand)46;
		((BarItem)this.commandBarItem65).Enabled = false;
		((BarItem)this.commandBarItem65).Id = 73;
		((BarItem)this.commandBarItem65).Name = "commandBarItem65";
		val226.FixedTooltipWidth = true;
		((ToolTipItem)val227).Text = "Adjust Margins";
		val228.LeftIndent = 6;
		val228.Text = "Set margin sizes for a report. Choose from several commonly used formats or click Custom Margins to define your own format.";
		val226.Items.Add((BaseToolTipItem)(object)val227);
		val226.Items.Add((BaseToolTipItem)(object)val228);
		((BaseToolTipObject)val226).MaxWidth = 210;
		((BarItem)this.commandBarItem65).SuperTip = val226;
		((BarBaseButtonItem)this.commandColorBarItem4).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandColorBarItem4).Caption = "Page Color";
		((BarBaseButtonItem)this.commandColorBarItem4).CloseSubMenuOnClickMode = (DefaultBoolean)1;
		((CommandBarItem)this.commandColorBarItem4).Command = (ReportCommand)47;
		((BarItem)this.commandColorBarItem4).Enabled = false;
		((BarItem)this.commandColorBarItem4).Id = 74;
		((BarItem)this.commandColorBarItem4).Name = "commandColorBarItem4";
		val229.FixedTooltipWidth = true;
		((ToolTipItem)val230).Text = "Choose Page Color";
		val231.LeftIndent = 6;
		val231.Text = "Select the background color for report pages.";
		val229.Items.Add((BaseToolTipItem)(object)val230);
		val229.Items.Add((BaseToolTipItem)(object)val231);
		((BaseToolTipObject)val229).MaxWidth = 210;
		((BarItem)this.commandColorBarItem4).SuperTip = val229;
		((BarItem)this.commandBarItem66).Caption = "Watermark";
		this.commandBarItem66.Command = (ReportCommand)48;
		((BarItem)this.commandBarItem66).Enabled = false;
		((BarItem)this.commandBarItem66).Id = 75;
		((BarItem)this.commandBarItem66).Name = "commandBarItem66";
		val232.FixedTooltipWidth = true;
		((ToolTipItem)val233).Text = "Add Watermark";
		val234.LeftIndent = 6;
		val234.Text = "Insert ghost text or image behind the page content to indicate that a report requires special treatment. Use the View tab to display a watermark at design time and use it as a template for a report.";
		val232.Items.Add((BaseToolTipItem)(object)val233);
		val232.Items.Add((BaseToolTipItem)(object)val234);
		((BaseToolTipObject)val232).MaxWidth = 210;
		((BarItem)this.commandBarItem66).SuperTip = val232;
		((BarItem)this.commandBarItem67).Caption = "Printing Warnings";
		this.commandBarItem67.Command = (ReportCommand)52;
		((BarItem)this.commandBarItem67).Enabled = false;
		((BarItem)this.commandBarItem67).Id = 76;
		((BarItem)this.commandBarItem67).Name = "commandBarItem67";
		val235.FixedTooltipWidth = true;
		((ToolTipItem)val236).Text = "Show Printing Warnings";
		val237.LeftIndent = 6;
		val237.Text = "Highlight report controls that overrun the right page margin to warn you about extra pages when printing the document.";
		val235.Items.Add((BaseToolTipItem)(object)val236);
		val235.Items.Add((BaseToolTipItem)(object)val237);
		((BaseToolTipObject)val235).MaxWidth = 210;
		((BarItem)this.commandBarItem67).SuperTip = val235;
		((BarItem)this.commandBarItem68).Caption = "Export Warnings";
		this.commandBarItem68.Command = (ReportCommand)53;
		((BarItem)this.commandBarItem68).Enabled = false;
		((BarItem)this.commandBarItem68).Id = 77;
		((BarItem)this.commandBarItem68).Name = "commandBarItem68";
		val238.FixedTooltipWidth = true;
		((ToolTipItem)val239).Text = "Show Export Warnings";
		val240.LeftIndent = 6;
		val240.Text = "Highlight intersecting report controls to warn you about the possibility of corrupting the document layout when exporting the document to specific formats.";
		val238.Items.Add((BaseToolTipItem)(object)val239);
		val238.Items.Add((BaseToolTipItem)(object)val240);
		((BaseToolTipObject)val238).MaxWidth = 210;
		((BarItem)this.commandBarItem68).SuperTip = val238;
		((BarItem)this.commandBarItem69).Caption = "Watermark";
		this.commandBarItem69.Command = (ReportCommand)40;
		((BarItem)this.commandBarItem69).Enabled = false;
		((BarItem)this.commandBarItem69).Id = 78;
		((BarItem)this.commandBarItem69).Name = "commandBarItem69";
		((BarItem)this.commandBarItem69).RibbonStyle = (RibbonItemStyles)2;
		val241.FixedTooltipWidth = true;
		((ToolTipItem)val242).Text = "Show Watermark";
		val243.LeftIndent = 6;
		val243.Text = "Display the document's watermark on the design surface for the better design experience. Specify watermark settings in the Page tab.";
		val241.Items.Add((BaseToolTipItem)(object)val242);
		val241.Items.Add((BaseToolTipItem)(object)val243);
		((BaseToolTipObject)val241).MaxWidth = 210;
		((BarItem)this.commandBarItem69).SuperTip = val241;
		((BarItem)this.commandBarItem70).Caption = "Grid Lines";
		this.commandBarItem70.Command = (ReportCommand)41;
		((BarItem)this.commandBarItem70).Enabled = false;
		((BarItem)this.commandBarItem70).Id = 79;
		((BarItem)this.commandBarItem70).Name = "commandBarItem70";
		((BarItem)this.commandBarItem70).RibbonStyle = (RibbonItemStyles)2;
		val244.FixedTooltipWidth = true;
		((ToolTipItem)val245).Text = "Show Grid Lines";
		val246.LeftIndent = 6;
		val246.Text = "Show gridlines on the report surface for perfect control placement.";
		val244.Items.Add((BaseToolTipItem)(object)val245);
		val244.Items.Add((BaseToolTipItem)(object)val246);
		((BaseToolTipObject)val244).MaxWidth = 210;
		((BarItem)this.commandBarItem70).SuperTip = val244;
		((BarItem)this.commandBarItem71).Caption = "Expand All";
		this.commandBarItem71.Command = (ReportCommand)143;
		((BarItem)this.commandBarItem71).Enabled = false;
		((BarItem)this.commandBarItem71).Id = 80;
		((BarItem)this.commandBarItem71).Name = "commandBarItem71";
		val247.FixedTooltipWidth = true;
		((ToolTipItem)val248).Text = "Expand All Bands";
		val249.LeftIndent = 6;
		val249.Text = "Expand all bands on the design surface.";
		val247.Items.Add((BaseToolTipItem)(object)val248);
		val247.Items.Add((BaseToolTipItem)(object)val249);
		((BaseToolTipObject)val247).MaxWidth = 210;
		((BarItem)this.commandBarItem71).SuperTip = val247;
		((BarItem)this.commandBarItem72).Caption = "Collapse All";
		this.commandBarItem72.Command = (ReportCommand)144;
		((BarItem)this.commandBarItem72).Enabled = false;
		((BarItem)this.commandBarItem72).Id = 81;
		((BarItem)this.commandBarItem72).Name = "commandBarItem72";
		val250.FixedTooltipWidth = true;
		((ToolTipItem)val251).Text = "Collapse All Bands";
		val252.LeftIndent = 6;
		val252.Text = "Collapse all bands on the design surface.";
		val250.Items.Add((BaseToolTipItem)(object)val251);
		val250.Items.Add((BaseToolTipItem)(object)val252);
		((BaseToolTipObject)val250).MaxWidth = 210;
		((BarItem)this.commandBarItem72).SuperTip = val250;
		((RepositoryItem)this.repositoryItemLookUpEdit1).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemLookUpEdit1).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((RepositoryItem)this.repositoryItemLookUpEdit1).Name = "repositoryItemLookUpEdit1";
		((BarItem)this.commandBarEditItem1).Caption = "Language";
		this.commandBarEditItem1.Command = (ReportCommand)222;
		((BarEditItem)this.commandBarEditItem1).Edit = (RepositoryItem)(object)this.repositoryItemLookUpEdit1;
		((BarEditItem)this.commandBarEditItem1).EditWidth = 140;
		((BarItem)this.commandBarEditItem1).Enabled = false;
		((BarItem)this.commandBarEditItem1).Id = 82;
		((BarItem)this.commandBarEditItem1).Name = "commandBarEditItem1";
		val253.FixedTooltipWidth = true;
		((ToolTipItem)val254).Text = "Language";
		val255.LeftIndent = 6;
		val255.Text = "Specifies the language associated with the localizable property values. Every language can have its own set of property values.";
		val253.Items.Add((BaseToolTipItem)(object)val254);
		val253.Items.Add((BaseToolTipItem)(object)val255);
		((BaseToolTipObject)val253).MaxWidth = 210;
		((BarItem)this.commandBarEditItem1).SuperTip = val253;
		((BarItem)this.commandBarCheckItem3).Caption = "Show Localizable Properties";
		((BarCheckItem)this.commandBarCheckItem3).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem3.Command = (ReportCommand)223;
		((BarItem)this.commandBarCheckItem3).Enabled = false;
		((BarItem)this.commandBarCheckItem3).Id = 83;
		((BarItem)this.commandBarCheckItem3).Name = "commandBarCheckItem3";
		val256.FixedTooltipWidth = true;
		((ToolTipItem)val257).Text = "Show Localizable Properties";
		val258.LeftIndent = 6;
		val258.Text = "Filters the Properties window to display only localizable properties.";
		val256.Items.Add((BaseToolTipItem)(object)val257);
		val256.Items.Add((BaseToolTipItem)(object)val258);
		((BaseToolTipObject)val256).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem3).SuperTip = val256;
		((BarBaseButtonItem)this.commandBarItem73).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem73).Caption = "Zoom";
		this.commandBarItem73.Command = (ReportCommand)258;
		((BarItem)this.commandBarItem73).Enabled = false;
		((BarItem)this.commandBarItem73).Id = 84;
		((BarItem)this.commandBarItem73).Name = "commandBarItem73";
		val259.FixedTooltipWidth = true;
		((ToolTipItem)val260).Text = "Zoom";
		val261.LeftIndent = 6;
		val261.Text = "Change the zoom level of the document designer.";
		val259.Items.Add((BaseToolTipItem)(object)val260);
		val259.Items.Add((BaseToolTipItem)(object)val261);
		((BaseToolTipObject)val259).MaxWidth = 210;
		((BarItem)this.commandBarItem73).SuperTip = val259;
		((BarItem)this.commandBarItem74).Caption = "Zoom In";
		this.commandBarItem74.Command = (ReportCommand)259;
		((BarItem)this.commandBarItem74).Enabled = false;
		((BarItem)this.commandBarItem74).Id = 85;
		((BarItem)this.commandBarItem74).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Add | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem74).Name = "commandBarItem74";
		val262.FixedTooltipWidth = true;
		((ToolTipItem)val263).Text = "Zoom In (Ctrl+Add)";
		val264.LeftIndent = 6;
		val264.Text = "Zoom in to get a close-up view of the report.";
		val262.Items.Add((BaseToolTipItem)(object)val263);
		val262.Items.Add((BaseToolTipItem)(object)val264);
		((BaseToolTipObject)val262).MaxWidth = 210;
		((BarItem)this.commandBarItem74).SuperTip = val262;
		((BarItem)this.commandBarItem75).Caption = "Zoom Out";
		this.commandBarItem75.Command = (ReportCommand)260;
		((BarItem)this.commandBarItem75).Enabled = false;
		((BarItem)this.commandBarItem75).Id = 86;
		((BarItem)this.commandBarItem75).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.Subtract | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem75).Name = "commandBarItem75";
		val265.FixedTooltipWidth = true;
		((ToolTipItem)val266).Text = "Zoom Out (Ctrl+Subtract)";
		val267.LeftIndent = 6;
		val267.Text = "Zoom out to see more of the report at a reduced size.";
		val265.Items.Add((BaseToolTipItem)(object)val266);
		val265.Items.Add((BaseToolTipItem)(object)val267);
		((BaseToolTipObject)val265).MaxWidth = 210;
		((BarItem)this.commandBarItem75).SuperTip = val265;
		((BarItem)this.commandBarItem76).Caption = "Validate";
		this.commandBarItem76.Command = (ReportCommand)37;
		((BarItem)this.commandBarItem76).Enabled = false;
		((BarItem)this.commandBarItem76).Id = 87;
		((BarItem)this.commandBarItem76).Name = "commandBarItem76";
		val268.FixedTooltipWidth = true;
		((ToolTipItem)val269).Text = "Validate Scripts";
		val270.LeftIndent = 6;
		val270.Text = "Check whether report scripts contain errors. If errors are found, they are listed in the Scripts Errors panel.";
		val268.Items.Add((BaseToolTipItem)(object)val269);
		val268.Items.Add((BaseToolTipItem)(object)val270);
		((BaseToolTipObject)val268).MaxWidth = 210;
		((BarItem)this.commandBarItem76).SuperTip = val268;
		this.commandGalleryBarItem1.Command = (ReportCommand)110;
		((BarItem)this.commandGalleryBarItem1).Enabled = false;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Disabled.Options.UseTextOptions = true;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Disabled.TextOptions.HAlignment = (HorzAlignment)2;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Disabled.TextOptions.Trimming = (Trimming)4;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Hovered.Options.UseTextOptions = true;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Hovered.TextOptions.HAlignment = (HorzAlignment)2;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Hovered.TextOptions.Trimming = (Trimming)4;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = (HorzAlignment)2;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Normal.TextOptions.Trimming = (Trimming)4;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Pressed.Options.UseTextOptions = true;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Pressed.TextOptions.HAlignment = (HorzAlignment)2;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).Appearance.ItemCaptionAppearance.Pressed.TextOptions.Trimming = (Trimming)4;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).ColumnCount = 7;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).ImageSize = new System.Drawing.Size(75, 30);
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).ItemCheckMode = (ItemCheckMode)1;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem1).Gallery).ShowItemText = true;
		((BarItem)this.commandGalleryBarItem1).Id = 88;
		((BarItem)this.commandGalleryBarItem1).Name = "commandGalleryBarItem1";
		this.commandGalleryBarItem2.Command = (ReportCommand)172;
		((BarItem)this.commandGalleryBarItem2).Enabled = false;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem2).Gallery).ColumnCount = 8;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem2).Gallery).ImageSize = new System.Drawing.Size(100, 33);
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem2).Gallery).ItemCheckMode = (ItemCheckMode)2;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem2).Gallery).ShowItemText = true;
		((BarItem)this.commandGalleryBarItem2).Id = 89;
		((BarItem)this.commandGalleryBarItem2).Name = "commandGalleryBarItem2";
		this.commandGalleryBarItem3.Command = (ReportCommand)190;
		((BarItem)this.commandGalleryBarItem3).Enabled = false;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem3).Gallery).ColumnCount = 7;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem3).Gallery).ImageSize = new System.Drawing.Size(75, 45);
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem3).Gallery).ItemCheckMode = (ItemCheckMode)2;
		((BarItem)this.commandGalleryBarItem3).Id = 90;
		((BarItem)this.commandGalleryBarItem3).Name = "commandGalleryBarItem3";
		this.commandGalleryBarItem4.Command = (ReportCommand)207;
		((BarItem)this.commandGalleryBarItem4).Enabled = false;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem4).Gallery).ColumnCount = 4;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem4).Gallery).ImageSize = new System.Drawing.Size(48, 48);
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem4).Gallery).ItemCheckMode = (ItemCheckMode)2;
		((BarItem)this.commandGalleryBarItem4).Id = 91;
		((BarItem)this.commandGalleryBarItem4).Name = "commandGalleryBarItem4";
		this.commandGalleryBarItem5.Command = (ReportCommand)219;
		((BarItem)this.commandGalleryBarItem5).Enabled = false;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem5).Gallery).ColumnCount = 7;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem5).Gallery).ImageSize = new System.Drawing.Size(48, 48);
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem5).Gallery).ItemCheckMode = (ItemCheckMode)2;
		((BarItem)this.commandGalleryBarItem5).Id = 92;
		((BarItem)this.commandGalleryBarItem5).Name = "commandGalleryBarItem5";
		this.commandGalleryBarItem6.Command = (ReportCommand)204;
		((BarItem)this.commandGalleryBarItem6).Enabled = false;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem6).Gallery).ColumnCount = 23;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem6).Gallery).DistanceBetweenItems = 9;
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem6).Gallery).ImageSize = new System.Drawing.Size(35, 35);
		((BaseGallery)((RibbonGalleryBarItem)this.commandGalleryBarItem6).Gallery).ScaleImages = (DefaultBoolean)0;
		((BarItem)this.commandGalleryBarItem6).Id = 93;
		((BarItem)this.commandGalleryBarItem6).Name = "commandGalleryBarItem6";
		((RepositoryItem)this.repositoryItemSpinEdit1).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemSpinEdit1).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[4] { -1, -1, -1, 0 });
		((RepositoryItem)this.repositoryItemSpinEdit1).Name = "repositoryItemSpinEdit1";
		((BarItem)this.commandBarEditItem2).Caption = "Width: ";
		this.commandBarEditItem2.Command = (ReportCommand)213;
		((BarEditItem)this.commandBarEditItem2).Edit = (RepositoryItem)(object)this.repositoryItemSpinEdit1;
		((BarEditItem)this.commandBarEditItem2).EditWidth = 50;
		((BarItem)this.commandBarEditItem2).Enabled = false;
		((BarItem)this.commandBarEditItem2).Id = 94;
		((BarItem)this.commandBarEditItem2).Name = "commandBarEditItem2";
		val271.FixedTooltipWidth = true;
		((ToolTipItem)val272).Text = "Cell Width";
		val273.LeftIndent = 6;
		val273.Text = "Set the cell width.";
		val271.Items.Add((BaseToolTipItem)(object)val272);
		val271.Items.Add((BaseToolTipItem)(object)val273);
		((BaseToolTipObject)val271).MaxWidth = 210;
		((BarItem)this.commandBarEditItem2).SuperTip = val271;
		((RepositoryItem)this.repositoryItemSpinEdit2).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemSpinEdit2).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		this.repositoryItemSpinEdit2.MaxValue = new decimal(new int[4] { -1, -1, -1, 0 });
		((RepositoryItem)this.repositoryItemSpinEdit2).Name = "repositoryItemSpinEdit2";
		((BarItem)this.commandBarEditItem3).Caption = "Height: ";
		this.commandBarEditItem3.Command = (ReportCommand)214;
		((BarEditItem)this.commandBarEditItem3).Edit = (RepositoryItem)(object)this.repositoryItemSpinEdit2;
		((BarEditItem)this.commandBarEditItem3).EditWidth = 50;
		((BarItem)this.commandBarEditItem3).Enabled = false;
		((BarItem)this.commandBarEditItem3).Id = 95;
		((BarItem)this.commandBarEditItem3).Name = "commandBarEditItem3";
		val274.FixedTooltipWidth = true;
		((ToolTipItem)val275).Text = "Cell Height";
		val276.LeftIndent = 6;
		val276.Text = "Set the cell height.";
		val274.Items.Add((BaseToolTipItem)(object)val275);
		val274.Items.Add((BaseToolTipItem)(object)val276);
		((BaseToolTipObject)val274).MaxWidth = 210;
		((BarItem)this.commandBarEditItem3).SuperTip = val274;
		((RepositoryItem)this.repositoryItemSpinEdit3).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemSpinEdit3).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		this.repositoryItemSpinEdit3.MaxValue = new decimal(new int[4] { -1, -1, -1, 0 });
		((RepositoryItem)this.repositoryItemSpinEdit3).Name = "repositoryItemSpinEdit3";
		((BarItem)this.commandBarEditItem4).Caption = "Horizontal Spacing: ";
		this.commandBarEditItem4.Command = (ReportCommand)217;
		((BarEditItem)this.commandBarEditItem4).Edit = (RepositoryItem)(object)this.repositoryItemSpinEdit3;
		((BarEditItem)this.commandBarEditItem4).EditWidth = 50;
		((BarItem)this.commandBarEditItem4).Enabled = false;
		((BarItem)this.commandBarEditItem4).Id = 96;
		((BarItem)this.commandBarEditItem4).Name = "commandBarEditItem4";
		val277.FixedTooltipWidth = true;
		((ToolTipItem)val278).Text = "Cell Horizontal Spacing";
		val279.LeftIndent = 6;
		val279.Text = "Set the horizontal spacing between adjacent cells.";
		val277.Items.Add((BaseToolTipItem)(object)val278);
		val277.Items.Add((BaseToolTipItem)(object)val279);
		((BaseToolTipObject)val277).MaxWidth = 210;
		((BarItem)this.commandBarEditItem4).SuperTip = val277;
		((RepositoryItem)this.repositoryItemSpinEdit4).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemSpinEdit4).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		this.repositoryItemSpinEdit4.MaxValue = new decimal(new int[4] { -1, -1, -1, 0 });
		((RepositoryItem)this.repositoryItemSpinEdit4).Name = "repositoryItemSpinEdit4";
		((BarItem)this.commandBarEditItem5).Caption = "Vertical Spacing: ";
		this.commandBarEditItem5.Command = (ReportCommand)218;
		((BarEditItem)this.commandBarEditItem5).Edit = (RepositoryItem)(object)this.repositoryItemSpinEdit4;
		((BarEditItem)this.commandBarEditItem5).EditWidth = 50;
		((BarItem)this.commandBarEditItem5).Enabled = false;
		((BarItem)this.commandBarEditItem5).Id = 97;
		((BarItem)this.commandBarEditItem5).Name = "commandBarEditItem5";
		val280.FixedTooltipWidth = true;
		((ToolTipItem)val281).Text = "Cell Vertical Spacing";
		val282.LeftIndent = 6;
		val282.Text = "Set the vertical spacing between adjacent cells.";
		val280.Items.Add((BaseToolTipItem)(object)val281);
		val280.Items.Add((BaseToolTipItem)(object)val282);
		((BaseToolTipObject)val280).MaxWidth = 210;
		((BarItem)this.commandBarEditItem5).SuperTip = val280;
		((RepositoryItem)this.repositoryItemImageComboBox1).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemImageComboBox1).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((RepositoryItem)this.repositoryItemImageComboBox1).Name = "repositoryItemImageComboBox1";
		((BarItem)this.commandBarEditItem6).Caption = "Border Dash Style";
		this.commandBarEditItem6.Command = (ReportCommand)126;
		((BarEditItem)this.commandBarEditItem6).Edit = (RepositoryItem)(object)this.repositoryItemImageComboBox1;
		((BarEditItem)this.commandBarEditItem6).EditWidth = 86;
		((BarItem)this.commandBarEditItem6).Enabled = false;
		((BarItem)this.commandBarEditItem6).Id = 98;
		((BarItem)this.commandBarEditItem6).Name = "commandBarEditItem6";
		val283.FixedTooltipWidth = true;
		((ToolTipItem)val284).Text = "Border Dash Style";
		val285.LeftIndent = 6;
		val285.Text = "Change the border dash style.";
		val283.Items.Add((BaseToolTipItem)(object)val284);
		val283.Items.Add((BaseToolTipItem)(object)val285);
		((BaseToolTipObject)val283).MaxWidth = 210;
		((BarItem)this.commandBarEditItem6).SuperTip = val283;
		((RepositoryItem)this.repositoryItemLookUpEdit2).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemLookUpEdit2).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((RepositoryItem)this.repositoryItemLookUpEdit2).Name = "repositoryItemLookUpEdit2";
		((BarItem)this.commandBarEditItem7).Caption = "Control: ";
		this.commandBarEditItem7.Command = (ReportCommand)38;
		((BarEditItem)this.commandBarEditItem7).Edit = (RepositoryItem)(object)this.repositoryItemLookUpEdit2;
		((BarEditItem)this.commandBarEditItem7).EditWidth = 300;
		((BarItem)this.commandBarEditItem7).Enabled = false;
		((BarItem)this.commandBarEditItem7).Id = 99;
		((BarItem)this.commandBarEditItem7).Name = "commandBarEditItem7";
		val286.FixedTooltipWidth = true;
		((ToolTipItem)val287).Text = "Control";
		val288.LeftIndent = 6;
		val288.Text = "Select a required control for specifying an event.";
		val286.Items.Add((BaseToolTipItem)(object)val287);
		val286.Items.Add((BaseToolTipItem)(object)val288);
		((BaseToolTipObject)val286).MaxWidth = 210;
		((BarItem)this.commandBarEditItem7).SuperTip = val286;
		((RepositoryItem)this.repositoryItemComboBox1).AutoHeight = false;
		((RepositoryItemButtonEdit)this.repositoryItemComboBox1).Buttons.AddRange((EditorButton[])(object)new EditorButton[1]
		{
			new EditorButton((ButtonPredefines)(-5))
		});
		((RepositoryItem)this.repositoryItemComboBox1).Name = "repositoryItemComboBox1";
		((BarItem)this.commandBarEditItem8).Caption = "Event: ";
		this.commandBarEditItem8.Command = (ReportCommand)39;
		((BarEditItem)this.commandBarEditItem8).Edit = (RepositoryItem)(object)this.repositoryItemComboBox1;
		((BarEditItem)this.commandBarEditItem8).EditWidth = 300;
		((BarItem)this.commandBarEditItem8).Enabled = false;
		((BarItem)this.commandBarEditItem8).Id = 100;
		((BarItem)this.commandBarEditItem8).Name = "commandBarEditItem8";
		val289.FixedTooltipWidth = true;
		((ToolTipItem)val290).Text = "Event";
		val291.LeftIndent = 6;
		val291.Text = "Select one of the available events.";
		val289.Items.Add((BaseToolTipItem)(object)val290);
		val289.Items.Add((BaseToolTipItem)(object)val291);
		((BaseToolTipObject)val289).MaxWidth = 210;
		((BarItem)this.commandBarEditItem8).SuperTip = val289;
		((BarItem)this.commandBarItem77).Caption = "Table";
		this.commandBarItem77.Command = (ReportCommand)146;
		((BarItem)this.commandBarItem77).Enabled = false;
		((BarItem)this.commandBarItem77).Id = 101;
		((BarItem)this.commandBarItem77).Name = "commandBarItem77";
		val292.FixedTooltipWidth = true;
		((ToolTipItem)val293).Text = "Select Table";
		val294.LeftIndent = 6;
		val294.Text = "Select the entire table.";
		val292.Items.Add((BaseToolTipItem)(object)val293);
		val292.Items.Add((BaseToolTipItem)(object)val294);
		((BaseToolTipObject)val292).MaxWidth = 210;
		((BarItem)this.commandBarItem77).SuperTip = val292;
		((BarItem)this.commandBarItem78).Caption = "Row";
		this.commandBarItem78.Command = (ReportCommand)147;
		((BarItem)this.commandBarItem78).Enabled = false;
		((BarItem)this.commandBarItem78).Id = 102;
		((BarItem)this.commandBarItem78).Name = "commandBarItem78";
		val295.FixedTooltipWidth = true;
		((ToolTipItem)val296).Text = "Select Row";
		val297.LeftIndent = 6;
		val297.Text = "Select the current row.";
		val295.Items.Add((BaseToolTipItem)(object)val296);
		val295.Items.Add((BaseToolTipItem)(object)val297);
		((BaseToolTipObject)val295).MaxWidth = 210;
		((BarItem)this.commandBarItem78).SuperTip = val295;
		((BarItem)this.commandBarItem79).Caption = "Column";
		this.commandBarItem79.Command = (ReportCommand)148;
		((BarItem)this.commandBarItem79).Enabled = false;
		((BarItem)this.commandBarItem79).Id = 103;
		((BarItem)this.commandBarItem79).Name = "commandBarItem79";
		val298.FixedTooltipWidth = true;
		((ToolTipItem)val299).Text = "Select Column";
		val300.LeftIndent = 6;
		val300.Text = "Select the current column.";
		val298.Items.Add((BaseToolTipItem)(object)val299);
		val298.Items.Add((BaseToolTipItem)(object)val300);
		((BaseToolTipObject)val298).MaxWidth = 210;
		((BarItem)this.commandBarItem79).SuperTip = val298;
		((BarItem)this.commandBarItem80).Caption = "Cell";
		this.commandBarItem80.Command = (ReportCommand)159;
		((BarItem)this.commandBarItem80).Enabled = false;
		((BarItem)this.commandBarItem80).Id = 104;
		((BarItem)this.commandBarItem80).Name = "commandBarItem80";
		val301.FixedTooltipWidth = true;
		((ToolTipItem)val302).Text = "Delete Cell";
		val303.LeftIndent = 6;
		val303.Text = "Delete the current cell.";
		val301.Items.Add((BaseToolTipItem)(object)val302);
		val301.Items.Add((BaseToolTipItem)(object)val303);
		((BaseToolTipObject)val301).MaxWidth = 210;
		((BarItem)this.commandBarItem80).SuperTip = val301;
		((BarItem)this.commandBarItem81).Caption = "Row";
		this.commandBarItem81.Command = (ReportCommand)157;
		((BarItem)this.commandBarItem81).Enabled = false;
		((BarItem)this.commandBarItem81).Id = 105;
		((BarItem)this.commandBarItem81).Name = "commandBarItem81";
		val304.FixedTooltipWidth = true;
		((ToolTipItem)val305).Text = "Delete Row";
		val306.LeftIndent = 6;
		val306.Text = "Delete the current row.";
		val304.Items.Add((BaseToolTipItem)(object)val305);
		val304.Items.Add((BaseToolTipItem)(object)val306);
		((BaseToolTipObject)val304).MaxWidth = 210;
		((BarItem)this.commandBarItem81).SuperTip = val304;
		((BarItem)this.commandBarItem82).Caption = "Column";
		this.commandBarItem82.Command = (ReportCommand)158;
		((BarItem)this.commandBarItem82).Enabled = false;
		((BarItem)this.commandBarItem82).Id = 106;
		((BarItem)this.commandBarItem82).Name = "commandBarItem82";
		val307.FixedTooltipWidth = true;
		((ToolTipItem)val308).Text = "Delete Column";
		val309.LeftIndent = 6;
		val309.Text = "Delete the current column.";
		val307.Items.Add((BaseToolTipItem)(object)val308);
		val307.Items.Add((BaseToolTipItem)(object)val309);
		((BaseToolTipObject)val307).MaxWidth = 210;
		((BarItem)this.commandBarItem82).SuperTip = val307;
		((BarItem)this.commandBarItem83).Caption = "Table";
		this.commandBarItem83.Command = (ReportCommand)156;
		((BarItem)this.commandBarItem83).Enabled = false;
		((BarItem)this.commandBarItem83).Id = 107;
		((BarItem)this.commandBarItem83).Name = "commandBarItem83";
		val310.FixedTooltipWidth = true;
		((ToolTipItem)val311).Text = "Delete Table";
		val312.LeftIndent = 6;
		val312.Text = "Delete the entire table.";
		val310.Items.Add((BaseToolTipItem)(object)val311);
		val310.Items.Add((BaseToolTipItem)(object)val312);
		((BaseToolTipObject)val310).MaxWidth = 210;
		((BarItem)this.commandBarItem83).SuperTip = val310;
		((BarItem)this.commandBarItem84).Caption = "Row Above";
		this.commandBarItem84.Command = (ReportCommand)149;
		((BarItem)this.commandBarItem84).Enabled = false;
		((BarItem)this.commandBarItem84).Id = 108;
		((BarItem)this.commandBarItem84).Name = "commandBarItem84";
		val313.FixedTooltipWidth = true;
		((ToolTipItem)val314).Text = "Insert Row Above";
		val315.LeftIndent = 6;
		val315.Text = "Add a new row directly above the current row.";
		val313.Items.Add((BaseToolTipItem)(object)val314);
		val313.Items.Add((BaseToolTipItem)(object)val315);
		((BaseToolTipObject)val313).MaxWidth = 210;
		((BarItem)this.commandBarItem84).SuperTip = val313;
		((BarItem)this.commandBarItem85).Caption = "Row Below";
		this.commandBarItem85.Command = (ReportCommand)150;
		((BarItem)this.commandBarItem85).Enabled = false;
		((BarItem)this.commandBarItem85).Id = 109;
		((BarItem)this.commandBarItem85).Name = "commandBarItem85";
		val316.FixedTooltipWidth = true;
		((ToolTipItem)val317).Text = "Insert Row Below";
		val318.LeftIndent = 6;
		val318.Text = "Add a new row directly below the current row.";
		val316.Items.Add((BaseToolTipItem)(object)val317);
		val316.Items.Add((BaseToolTipItem)(object)val318);
		((BaseToolTipObject)val316).MaxWidth = 210;
		((BarItem)this.commandBarItem85).SuperTip = val316;
		((BarItem)this.commandBarItem86).Caption = "Column to Left";
		this.commandBarItem86.Command = (ReportCommand)151;
		((BarItem)this.commandBarItem86).Enabled = false;
		((BarItem)this.commandBarItem86).Id = 110;
		((BarItem)this.commandBarItem86).Name = "commandBarItem86";
		val319.FixedTooltipWidth = true;
		((ToolTipItem)val320).Text = "Insert Column to Left";
		val321.LeftIndent = 6;
		val321.Text = "Add a new column directly to the left of the current column.";
		val319.Items.Add((BaseToolTipItem)(object)val320);
		val319.Items.Add((BaseToolTipItem)(object)val321);
		((BaseToolTipObject)val319).MaxWidth = 210;
		((BarItem)this.commandBarItem86).SuperTip = val319;
		((BarItem)this.commandBarItem87).Caption = "Column to Right";
		this.commandBarItem87.Command = (ReportCommand)152;
		((BarItem)this.commandBarItem87).Enabled = false;
		((BarItem)this.commandBarItem87).Id = 111;
		((BarItem)this.commandBarItem87).Name = "commandBarItem87";
		val322.FixedTooltipWidth = true;
		((ToolTipItem)val323).Text = "Insert Column to Right";
		val324.LeftIndent = 6;
		val324.Text = "Add a new column directly to the right of the current column.";
		val322.Items.Add((BaseToolTipItem)(object)val323);
		val322.Items.Add((BaseToolTipItem)(object)val324);
		((BaseToolTipObject)val322).MaxWidth = 210;
		((BarItem)this.commandBarItem87).SuperTip = val322;
		((BarItem)this.commandBarItem88).Caption = "Rows";
		this.commandBarItem88.Command = (ReportCommand)160;
		((BarItem)this.commandBarItem88).Enabled = false;
		((BarItem)this.commandBarItem88).Id = 112;
		((BarItem)this.commandBarItem88).Name = "commandBarItem88";
		val325.FixedTooltipWidth = true;
		((ToolTipItem)val326).Text = "Distribute Rows Evenly";
		val327.LeftIndent = 6;
		val327.Text = "Distribute the height of the selected rows equally between them.";
		val325.Items.Add((BaseToolTipItem)(object)val326);
		val325.Items.Add((BaseToolTipItem)(object)val327);
		((BaseToolTipObject)val325).MaxWidth = 210;
		((BarItem)this.commandBarItem88).SuperTip = val325;
		((BarItem)this.commandBarItem89).Caption = "Columns";
		this.commandBarItem89.Command = (ReportCommand)161;
		((BarItem)this.commandBarItem89).Enabled = false;
		((BarItem)this.commandBarItem89).Id = 113;
		((BarItem)this.commandBarItem89).Name = "commandBarItem89";
		val328.FixedTooltipWidth = true;
		((ToolTipItem)val329).Text = "Distribute Columns Evenly";
		val330.LeftIndent = 6;
		val330.Text = "Distribute the width of the selected columns equally between them.";
		val328.Items.Add((BaseToolTipItem)(object)val329);
		val328.Items.Add((BaseToolTipItem)(object)val330);
		((BaseToolTipObject)val328).MaxWidth = 210;
		((BarItem)this.commandBarItem89).SuperTip = val328;
		((BarItem)this.commandBarItem90).Caption = "Merge Cells";
		this.commandBarItem90.Command = (ReportCommand)155;
		((BarItem)this.commandBarItem90).Enabled = false;
		((BarItem)this.commandBarItem90).Id = 114;
		((BarItem)this.commandBarItem90).Name = "commandBarItem90";
		val331.FixedTooltipWidth = true;
		((ToolTipItem)val332).Text = "Merge Cells";
		val333.LeftIndent = 6;
		val333.Text = "Merge the selected cells into one cell.";
		val331.Items.Add((BaseToolTipItem)(object)val332);
		val331.Items.Add((BaseToolTipItem)(object)val333);
		((BaseToolTipObject)val331).MaxWidth = 210;
		((BarItem)this.commandBarItem90).SuperTip = val331;
		((BarItem)this.commandBarItem91).Caption = "Split Cells";
		this.commandBarItem91.Command = (ReportCommand)154;
		((BarItem)this.commandBarItem91).Enabled = false;
		((BarItem)this.commandBarItem91).Id = 115;
		((BarItem)this.commandBarItem91).Name = "commandBarItem91";
		val334.FixedTooltipWidth = true;
		((ToolTipItem)val335).Text = "Split Cells";
		val336.LeftIndent = 6;
		val336.Text = "Split the selected cells into the specified number of rows or columns.";
		val334.Items.Add((BaseToolTipItem)(object)val335);
		val334.Items.Add((BaseToolTipItem)(object)val336);
		((BaseToolTipObject)val334).MaxWidth = 210;
		((BarItem)this.commandBarItem91).SuperTip = val334;
		((BarItem)this.commandBarItem92).Caption = "Run Designer";
		this.commandBarItem92.Command = (ReportCommand)193;
		((BarItem)this.commandBarItem92).Enabled = false;
		((BarItem)this.commandBarItem92).Id = 116;
		((BarItem)this.commandBarItem92).Name = "commandBarItem92";
		val337.FixedTooltipWidth = true;
		((ToolTipItem)val338).Text = "Run Designer";
		val339.LeftIndent = 6;
		val339.Text = "Run the Pivot Grid Designer that allows customizing fields, the control's layout, appearance settings and printing options.";
		val337.Items.Add((BaseToolTipItem)(object)val338);
		val337.Items.Add((BaseToolTipItem)(object)val339);
		((BaseToolTipObject)val337).MaxWidth = 210;
		((BarItem)this.commandBarItem92).SuperTip = val337;
		((BarItem)this.commandBarItem93).Caption = "Add Data Source";
		this.commandBarItem93.Command = (ReportCommand)194;
		((BarItem)this.commandBarItem93).Enabled = false;
		((BarItem)this.commandBarItem93).Id = 117;
		((BarItem)this.commandBarItem93).Name = "commandBarItem93";
		val340.FixedTooltipWidth = true;
		((ToolTipItem)val341).Text = "Add Data Source";
		val342.LeftIndent = 6;
		val342.Text = "Set up a data source for a Pivot Grid.";
		val340.Items.Add((BaseToolTipItem)(object)val341);
		val340.Items.Add((BaseToolTipItem)(object)val342);
		((BaseToolTipObject)val340).MaxWidth = 210;
		((BarItem)this.commandBarItem93).SuperTip = val340;
		((BarItem)this.commandBarItem94).Caption = "Remove Field";
		this.commandBarItem94.Command = (ReportCommand)196;
		((BarItem)this.commandBarItem94).Enabled = false;
		((BarItem)this.commandBarItem94).Id = 118;
		((BarItem)this.commandBarItem94).Name = "commandBarItem94";
		val343.FixedTooltipWidth = true;
		((ToolTipItem)val344).Text = "Remove Field";
		val345.LeftIndent = 6;
		val345.Text = "Remove the selected Pivot Grid field.";
		val343.Items.Add((BaseToolTipItem)(object)val344);
		val343.Items.Add((BaseToolTipItem)(object)val345);
		((BaseToolTipObject)val343).MaxWidth = 210;
		((BarItem)this.commandBarItem94).SuperTip = val343;
		((BarButtonItem)this.commandBarItem95).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem95).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem95).Caption = "Add Field";
		this.commandBarItem95.Command = (ReportCommand)195;
		((BarItem)this.commandBarItem95).Enabled = false;
		((BarItem)this.commandBarItem95).Id = 119;
		((BarItem)this.commandBarItem95).Name = "commandBarItem95";
		val346.FixedTooltipWidth = true;
		((ToolTipItem)val347).Text = "Add Field";
		val348.LeftIndent = 6;
		val348.Text = "Add a new Pivot Grid field to a required header area.";
		val346.Items.Add((BaseToolTipItem)(object)val347);
		val346.Items.Add((BaseToolTipItem)(object)val348);
		((BaseToolTipObject)val346).MaxWidth = 210;
		((BarItem)this.commandBarItem95).SuperTip = val346;
		((BarItem)this.commandBarItem96).Caption = "Vertical Lines";
		this.commandBarItem96.Command = (ReportCommand)197;
		((BarItem)this.commandBarItem96).Enabled = false;
		((BarItem)this.commandBarItem96).Id = 120;
		((BarItem)this.commandBarItem96).Name = "commandBarItem96";
		val349.FixedTooltipWidth = true;
		((ToolTipItem)val350).Text = "Print Vertical Lines";
		val351.LeftIndent = 6;
		val351.Text = "Print vertical grid lines.";
		val349.Items.Add((BaseToolTipItem)(object)val350);
		val349.Items.Add((BaseToolTipItem)(object)val351);
		((BaseToolTipObject)val349).MaxWidth = 210;
		((BarItem)this.commandBarItem96).SuperTip = val349;
		((BarItem)this.commandBarItem97).Caption = "Horizontal Lines";
		this.commandBarItem97.Command = (ReportCommand)198;
		((BarItem)this.commandBarItem97).Enabled = false;
		((BarItem)this.commandBarItem97).Id = 121;
		((BarItem)this.commandBarItem97).Name = "commandBarItem97";
		val352.FixedTooltipWidth = true;
		((ToolTipItem)val353).Text = "Print Horizontal Lines";
		val354.LeftIndent = 6;
		val354.Text = "Print horizontal grid lines.";
		val352.Items.Add((BaseToolTipItem)(object)val353);
		val352.Items.Add((BaseToolTipItem)(object)val354);
		((BaseToolTipObject)val352).MaxWidth = 210;
		((BarItem)this.commandBarItem97).SuperTip = val352;
		((BarItem)this.commandBarItem98).Caption = "Data Headers";
		this.commandBarItem98.Command = (ReportCommand)199;
		((BarItem)this.commandBarItem98).Enabled = false;
		((BarItem)this.commandBarItem98).Id = 122;
		((BarItem)this.commandBarItem98).Name = "commandBarItem98";
		val355.FixedTooltipWidth = true;
		((ToolTipItem)val356).Text = "Print Data Headers";
		val357.LeftIndent = 6;
		val357.Text = "Print data field headers.";
		val355.Items.Add((BaseToolTipItem)(object)val356);
		val355.Items.Add((BaseToolTipItem)(object)val357);
		((BaseToolTipObject)val355).MaxWidth = 210;
		((BarItem)this.commandBarItem98).SuperTip = val355;
		((BarItem)this.commandBarItem99).Caption = "Column Headers";
		this.commandBarItem99.Command = (ReportCommand)200;
		((BarItem)this.commandBarItem99).Enabled = false;
		((BarItem)this.commandBarItem99).Id = 123;
		((BarItem)this.commandBarItem99).Name = "commandBarItem99";
		val358.FixedTooltipWidth = true;
		((ToolTipItem)val359).Text = "Print Column Headers";
		val360.LeftIndent = 6;
		val360.Text = "Print column field headers.";
		val358.Items.Add((BaseToolTipItem)(object)val359);
		val358.Items.Add((BaseToolTipItem)(object)val360);
		((BaseToolTipObject)val358).MaxWidth = 210;
		((BarItem)this.commandBarItem99).SuperTip = val358;
		((BarItem)this.commandBarItem100).Caption = "Row Headers";
		this.commandBarItem100.Command = (ReportCommand)201;
		((BarItem)this.commandBarItem100).Enabled = false;
		((BarItem)this.commandBarItem100).Id = 124;
		((BarItem)this.commandBarItem100).Name = "commandBarItem100";
		val361.FixedTooltipWidth = true;
		((ToolTipItem)val362).Text = "Print Row Headers";
		val363.LeftIndent = 6;
		val363.Text = "Print row field headers.";
		val361.Items.Add((BaseToolTipItem)(object)val362);
		val361.Items.Add((BaseToolTipItem)(object)val363);
		((BaseToolTipObject)val361).MaxWidth = 210;
		((BarItem)this.commandBarItem100).SuperTip = val361;
		((BarItem)this.commandBarItem101).Caption = "Column Area On Every Page";
		this.commandBarItem101.Command = (ReportCommand)202;
		((BarItem)this.commandBarItem101).Enabled = false;
		((BarItem)this.commandBarItem101).Id = 125;
		((BarItem)this.commandBarItem101).Name = "commandBarItem101";
		((BarItem)this.commandBarItem101).RibbonStyle = (RibbonItemStyles)2;
		val364.FixedTooltipWidth = true;
		((ToolTipItem)val365).Text = "Print Column Area On Every Page";
		val366.LeftIndent = 6;
		val366.Text = "Print column area on every page.";
		val364.Items.Add((BaseToolTipItem)(object)val365);
		val364.Items.Add((BaseToolTipItem)(object)val366);
		((BaseToolTipObject)val364).MaxWidth = 210;
		((BarItem)this.commandBarItem101).SuperTip = val364;
		((BarItem)this.commandBarItem102).Caption = "Row Area On Every Page";
		this.commandBarItem102.Command = (ReportCommand)203;
		((BarItem)this.commandBarItem102).Enabled = false;
		((BarItem)this.commandBarItem102).Id = 126;
		((BarItem)this.commandBarItem102).Name = "commandBarItem102";
		((BarItem)this.commandBarItem102).RibbonStyle = (RibbonItemStyles)2;
		val367.FixedTooltipWidth = true;
		((ToolTipItem)val368).Text = "Print Row Area On Every Page";
		val369.LeftIndent = 6;
		val369.Text = "Print row area on every page.";
		val367.Items.Add((BaseToolTipItem)(object)val368);
		val367.Items.Add((BaseToolTipItem)(object)val369);
		((BaseToolTipObject)val367).MaxWidth = 210;
		((BarItem)this.commandBarItem102).SuperTip = val367;
		((BarItem)this.commandBarItem103).Caption = "Load...";
		this.commandBarItem103.Command = (ReportCommand)175;
		((BarItem)this.commandBarItem103).Enabled = false;
		((BarItem)this.commandBarItem103).Id = 127;
		((BarItem)this.commandBarItem103).Name = "commandBarItem103";
		((BarItem)this.commandBarItem103).RibbonStyle = (RibbonItemStyles)2;
		val370.FixedTooltipWidth = true;
		((ToolTipItem)val371).Text = "Load";
		val372.LeftIndent = 6;
		val372.Text = "Load a chart from an XML file.";
		val370.Items.Add((BaseToolTipItem)(object)val371);
		val370.Items.Add((BaseToolTipItem)(object)val372);
		((BaseToolTipObject)val370).MaxWidth = 210;
		((BarItem)this.commandBarItem103).SuperTip = val370;
		((BarItem)this.commandBarItem104).Caption = "Save...";
		this.commandBarItem104.Command = (ReportCommand)176;
		((BarItem)this.commandBarItem104).Enabled = false;
		((BarItem)this.commandBarItem104).Id = 128;
		((BarItem)this.commandBarItem104).Name = "commandBarItem104";
		((BarItem)this.commandBarItem104).RibbonStyle = (RibbonItemStyles)2;
		val373.FixedTooltipWidth = true;
		((ToolTipItem)val374).Text = "Save";
		val375.LeftIndent = 6;
		val375.Text = "Save a chart to an XML file.";
		val373.Items.Add((BaseToolTipItem)(object)val374);
		val373.Items.Add((BaseToolTipItem)(object)val375);
		((BaseToolTipObject)val373).MaxWidth = 210;
		((BarItem)this.commandBarItem104).SuperTip = val373;
		((BarItem)this.commandBarItem105).Caption = "Run Designer";
		this.commandBarItem105.Command = (ReportCommand)173;
		((BarItem)this.commandBarItem105).Enabled = false;
		((BarItem)this.commandBarItem105).Id = 129;
		((BarItem)this.commandBarItem105).Name = "commandBarItem105";
		val376.FixedTooltipWidth = true;
		((ToolTipItem)val377).Text = "Run Designer";
		val378.LeftIndent = 6;
		val378.Text = "Run the Chart Designer that allows creating and editing properties of a chart and its elements.";
		val376.Items.Add((BaseToolTipItem)(object)val377);
		val376.Items.Add((BaseToolTipItem)(object)val378);
		((BaseToolTipObject)val376).MaxWidth = 210;
		((BarItem)this.commandBarItem105).SuperTip = val376;
		((BarItem)this.commandBarItem106).Caption = "Add Data Source";
		this.commandBarItem106.Command = (ReportCommand)174;
		((BarItem)this.commandBarItem106).Enabled = false;
		((BarItem)this.commandBarItem106).Id = 130;
		((BarItem)this.commandBarItem106).Name = "commandBarItem106";
		val379.FixedTooltipWidth = true;
		((ToolTipItem)val380).Text = "Add Data Source";
		val381.LeftIndent = 6;
		val381.Text = "Set up a data source for a chart.";
		val379.Items.Add((BaseToolTipItem)(object)val380);
		val379.Items.Add((BaseToolTipItem)(object)val381);
		((BaseToolTipObject)val379).MaxWidth = 210;
		((BarItem)this.commandBarItem106).SuperTip = val379;
		((BarButtonItem)this.commandBarItem107).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem107).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem107).Caption = "Palette";
		this.commandBarItem107.Command = (ReportCommand)191;
		((BarItem)this.commandBarItem107).Enabled = false;
		((BarItem)this.commandBarItem107).Id = 131;
		((BarItem)this.commandBarItem107).Name = "commandBarItem107";
		val382.FixedTooltipWidth = true;
		((ToolTipItem)val383).Text = "Palette";
		val384.LeftIndent = 6;
		val384.Text = "Select a palette for painting a chart's series.";
		val382.Items.Add((BaseToolTipItem)(object)val383);
		val382.Items.Add((BaseToolTipItem)(object)val384);
		((BaseToolTipObject)val382).MaxWidth = 210;
		((BarItem)this.commandBarItem107).SuperTip = val382;
		((BarButtonItem)this.commandBarItem108).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem108).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem108).Caption = "Bar";
		this.commandBarItem108.Command = (ReportCommand)177;
		((BarItem)this.commandBarItem108).Enabled = false;
		((BarItem)this.commandBarItem108).Id = 132;
		((BarItem)this.commandBarItem108).Name = "commandBarItem108";
		val385.FixedTooltipWidth = true;
		((ToolTipItem)val386).Text = "Bar Series";
		val387.LeftIndent = 6;
		val387.Text = "Add a bar series to display values as vertical columns grouped by categories.";
		val385.Items.Add((BaseToolTipItem)(object)val386);
		val385.Items.Add((BaseToolTipItem)(object)val387);
		((BaseToolTipObject)val385).MaxWidth = 210;
		((BarItem)this.commandBarItem108).SuperTip = val385;
		((BarButtonItem)this.commandBarItem109).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem109).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem109).Caption = "Line";
		this.commandBarItem109.Command = (ReportCommand)178;
		((BarItem)this.commandBarItem109).Enabled = false;
		((BarItem)this.commandBarItem109).Id = 133;
		((BarItem)this.commandBarItem109).Name = "commandBarItem109";
		val388.FixedTooltipWidth = true;
		((ToolTipItem)val389).Text = "Line Series";
		val390.LeftIndent = 6;
		val390.Text = "Add a line series to show line trends over time or categories.";
		val388.Items.Add((BaseToolTipItem)(object)val389);
		val388.Items.Add((BaseToolTipItem)(object)val390);
		((BaseToolTipObject)val388).MaxWidth = 210;
		((BarItem)this.commandBarItem109).SuperTip = val388;
		((BarButtonItem)this.commandBarItem110).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem110).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem110).Caption = "Area";
		this.commandBarItem110.Command = (ReportCommand)179;
		((BarItem)this.commandBarItem110).Enabled = false;
		((BarItem)this.commandBarItem110).Id = 134;
		((BarItem)this.commandBarItem110).Name = "commandBarItem110";
		val391.FixedTooltipWidth = true;
		((ToolTipItem)val392).Text = "Area Series";
		val393.LeftIndent = 6;
		val393.Text = "Add an area series to display values as a filled area with peaks and hollows.";
		val391.Items.Add((BaseToolTipItem)(object)val392);
		val391.Items.Add((BaseToolTipItem)(object)val393);
		((BaseToolTipObject)val391).MaxWidth = 210;
		((BarItem)this.commandBarItem110).SuperTip = val391;
		((BarButtonItem)this.commandBarItem111).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem111).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem111).Caption = "Range";
		this.commandBarItem111.Command = (ReportCommand)180;
		((BarItem)this.commandBarItem111).Enabled = false;
		((BarItem)this.commandBarItem111).Id = 135;
		((BarItem)this.commandBarItem111).Name = "commandBarItem111";
		val394.FixedTooltipWidth = true;
		((ToolTipItem)val395).Text = "Range Series";
		val396.LeftIndent = 6;
		val396.Text = "Add a series to display a range of values with the minimum and maximum limits. ";
		val394.Items.Add((BaseToolTipItem)(object)val395);
		val394.Items.Add((BaseToolTipItem)(object)val396);
		((BaseToolTipObject)val394).MaxWidth = 210;
		((BarItem)this.commandBarItem111).SuperTip = val394;
		((BarButtonItem)this.commandBarItem112).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem112).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem112).Caption = "Pie and Doughnut";
		this.commandBarItem112.Command = (ReportCommand)181;
		((BarItem)this.commandBarItem112).Enabled = false;
		((BarItem)this.commandBarItem112).Id = 136;
		((BarItem)this.commandBarItem112).Name = "commandBarItem112";
		val397.FixedTooltipWidth = true;
		((ToolTipItem)val398).Text = "Pie And Doughnut Series";
		val399.LeftIndent = 6;
		val399.Text = "Add a series to display the percentage values of different point arguments to compare their significance.";
		val397.Items.Add((BaseToolTipItem)(object)val398);
		val397.Items.Add((BaseToolTipItem)(object)val399);
		((BaseToolTipObject)val397).MaxWidth = 210;
		((BarItem)this.commandBarItem112).SuperTip = val397;
		((BarButtonItem)this.commandBarItem113).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem113).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem113).Caption = "Radar and Polar";
		this.commandBarItem113.Command = (ReportCommand)182;
		((BarItem)this.commandBarItem113).Enabled = false;
		((BarItem)this.commandBarItem113).Id = 137;
		((BarItem)this.commandBarItem113).Name = "commandBarItem113";
		val400.FixedTooltipWidth = true;
		((ToolTipItem)val401).Text = "Radar And Polar Series";
		val402.LeftIndent = 6;
		val402.Text = "Add a series to display values as a circular graph.";
		val400.Items.Add((BaseToolTipItem)(object)val401);
		val400.Items.Add((BaseToolTipItem)(object)val402);
		((BaseToolTipObject)val400).MaxWidth = 210;
		((BarItem)this.commandBarItem113).SuperTip = val400;
		((BarButtonItem)this.commandBarItem114).ActAsDropDown = true;
		((BarBaseButtonItem)this.commandBarItem114).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.commandBarItem114).Caption = "Other Series";
		this.commandBarItem114.Command = (ReportCommand)183;
		((BarItem)this.commandBarItem114).Enabled = false;
		((BarItem)this.commandBarItem114).Id = 138;
		((BarItem)this.commandBarItem114).Name = "commandBarItem114";
		val403.FixedTooltipWidth = true;
		((ToolTipItem)val404).Text = "Other Series";
		val405.LeftIndent = 6;
		val405.Text = "Choose a chart type to display your data.";
		val403.Items.Add((BaseToolTipItem)(object)val404);
		val403.Items.Add((BaseToolTipItem)(object)val405);
		((BaseToolTipObject)val403).MaxWidth = 210;
		((BarItem)this.commandBarItem114).SuperTip = val403;
		((BarItem)this.commandBarItem115).Caption = "Remove Series";
		this.commandBarItem115.Command = (ReportCommand)185;
		((BarItem)this.commandBarItem115).Enabled = false;
		((BarItem)this.commandBarItem115).Id = 139;
		((BarItem)this.commandBarItem115).Name = "commandBarItem115";
		val406.FixedTooltipWidth = true;
		((ToolTipItem)val407).Text = "Remove Series";
		val408.LeftIndent = 6;
		val408.Text = "Remove the selected series.";
		val406.Items.Add((BaseToolTipItem)(object)val407);
		val406.Items.Add((BaseToolTipItem)(object)val408);
		((BaseToolTipObject)val406).MaxWidth = 210;
		((BarItem)this.commandBarItem115).SuperTip = val406;
		((BarItem)this.commandBarItem116).Caption = "Add Text Annotation";
		this.commandBarItem116.Command = (ReportCommand)186;
		((BarItem)this.commandBarItem116).Enabled = false;
		((BarItem)this.commandBarItem116).Id = 140;
		((BarItem)this.commandBarItem116).Name = "commandBarItem116";
		val409.FixedTooltipWidth = true;
		((ToolTipItem)val410).Text = "Add Text Annotation";
		val411.LeftIndent = 6;
		val411.Text = "Add a text annotation to a chart.";
		val409.Items.Add((BaseToolTipItem)(object)val410);
		val409.Items.Add((BaseToolTipItem)(object)val411);
		((BaseToolTipObject)val409).MaxWidth = 210;
		((BarItem)this.commandBarItem116).SuperTip = val409;
		((BarItem)this.commandBarItem117).Caption = "Add Image Annotation";
		this.commandBarItem117.Command = (ReportCommand)187;
		((BarItem)this.commandBarItem117).Enabled = false;
		((BarItem)this.commandBarItem117).Id = 141;
		((BarItem)this.commandBarItem117).Name = "commandBarItem117";
		val412.FixedTooltipWidth = true;
		((ToolTipItem)val413).Text = "Add Image Annotation";
		val414.LeftIndent = 6;
		val414.Text = "Add an image annotation to a chart.";
		val412.Items.Add((BaseToolTipItem)(object)val413);
		val412.Items.Add((BaseToolTipItem)(object)val414);
		((BaseToolTipObject)val412).MaxWidth = 210;
		((BarItem)this.commandBarItem117).SuperTip = val412;
		((BarItem)this.commandBarItem118).Caption = "Remove Annotation";
		this.commandBarItem118.Command = (ReportCommand)189;
		((BarItem)this.commandBarItem118).Enabled = false;
		((BarItem)this.commandBarItem118).Id = 142;
		((BarItem)this.commandBarItem118).Name = "commandBarItem118";
		val415.FixedTooltipWidth = true;
		((ToolTipItem)val416).Text = "Remove Annotation";
		val417.LeftIndent = 6;
		val417.Text = "Remove the selected annotation.";
		val415.Items.Add((BaseToolTipItem)(object)val416);
		val415.Items.Add((BaseToolTipItem)(object)val417);
		((BaseToolTipObject)val415).MaxWidth = 210;
		((BarItem)this.commandBarItem118).SuperTip = val415;
		((BarItem)this.commandBarItem119).Caption = "Auto Module";
		this.commandBarItem119.Command = (ReportCommand)170;
		((BarItem)this.commandBarItem119).Enabled = false;
		((BarItem)this.commandBarItem119).Id = 143;
		((BarItem)this.commandBarItem119).Name = "commandBarItem119";
		val418.FixedTooltipWidth = true;
		((ToolTipItem)val419).Text = "Auto Module";
		val420.LeftIndent = 6;
		val420.Text = "Automatically calculate the bar width based on barcode dimensions.";
		val418.Items.Add((BaseToolTipItem)(object)val419);
		val418.Items.Add((BaseToolTipItem)(object)val420);
		((BaseToolTipObject)val418).MaxWidth = 210;
		((BarItem)this.commandBarItem119).SuperTip = val418;
		((BarItem)this.commandBarItem120).Caption = "Show Text";
		this.commandBarItem120.Command = (ReportCommand)171;
		((BarItem)this.commandBarItem120).Enabled = false;
		((BarItem)this.commandBarItem120).Id = 144;
		((BarItem)this.commandBarItem120).Name = "commandBarItem120";
		val421.FixedTooltipWidth = true;
		((ToolTipItem)val422).Text = "Show Text";
		val423.LeftIndent = 6;
		val423.Text = "Display accompanying text in a barcode.";
		val421.Items.Add((BaseToolTipItem)(object)val422);
		val421.Items.Add((BaseToolTipItem)(object)val423);
		((BaseToolTipObject)val421).MaxWidth = 210;
		((BarItem)this.commandBarItem120).SuperTip = val421;
		((BarItem)this.commandBarItem121).Caption = "Fit Bounds to Text";
		this.commandBarItem121.Command = (ReportCommand)72;
		((BarItem)this.commandBarItem121).Enabled = false;
		((BarItem)this.commandBarItem121).Id = 145;
		((BarItem)this.commandBarItem121).Name = "commandBarItem121";
		val424.FixedTooltipWidth = true;
		((ToolTipItem)val425).Text = "Fit Bounds to Text";
		val426.LeftIndent = 6;
		val426.Text = "Adjust the size of the selected controls to fit their text.";
		val424.Items.Add((BaseToolTipItem)(object)val425);
		val424.Items.Add((BaseToolTipItem)(object)val426);
		((BaseToolTipObject)val424).MaxWidth = 210;
		((BarItem)this.commandBarItem121).SuperTip = val424;
		((BarItem)this.commandBarItem122).Caption = "Fit Text to Bounds";
		this.commandBarItem122.Command = (ReportCommand)71;
		((BarItem)this.commandBarItem122).Enabled = false;
		((BarItem)this.commandBarItem122).Id = 146;
		((BarItem)this.commandBarItem122).Name = "commandBarItem122";
		val427.FixedTooltipWidth = true;
		((ToolTipItem)val428).Text = "Fit Text to Bounds";
		val429.LeftIndent = 6;
		val429.Text = "Adjust the font size of the selected controls to fit their entire area.";
		val427.Items.Add((BaseToolTipItem)(object)val428);
		val427.Items.Add((BaseToolTipItem)(object)val429);
		((BaseToolTipObject)val427).MaxWidth = 210;
		((BarItem)this.commandBarItem122).SuperTip = val427;
		((BarItem)this.commandBarCheckItem4).Caption = "Auto Width";
		((BarCheckItem)this.commandBarCheckItem4).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem4.Command = (ReportCommand)209;
		((BarItem)this.commandBarCheckItem4).Enabled = false;
		((BarItem)this.commandBarCheckItem4).Id = 147;
		((BarItem)this.commandBarCheckItem4).Name = "commandBarCheckItem4";
		val430.FixedTooltipWidth = true;
		((ToolTipItem)val431).Text = "Auto Width";
		val432.LeftIndent = 6;
		val432.Text = "Enable the selected controls to adjust their width to fit their content.";
		val430.Items.Add((BaseToolTipItem)(object)val431);
		val430.Items.Add((BaseToolTipItem)(object)val432);
		((BaseToolTipObject)val430).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem4).SuperTip = val430;
		((BarItem)this.commandBarCheckItem5).Caption = "Word Wrap";
		((BarCheckItem)this.commandBarCheckItem5).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem5.Command = (ReportCommand)208;
		((BarItem)this.commandBarCheckItem5).Enabled = false;
		((BarItem)this.commandBarCheckItem5).Id = 148;
		((BarItem)this.commandBarCheckItem5).Name = "commandBarCheckItem5";
		val433.FixedTooltipWidth = true;
		((ToolTipItem)val434).Text = "Word Wrap";
		val435.LeftIndent = 6;
		val435.Text = "Enable the selected controls to wrap their text if it does not fit a line.";
		val433.Items.Add((BaseToolTipItem)(object)val434);
		val433.Items.Add((BaseToolTipItem)(object)val435);
		((BaseToolTipObject)val433).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem5).SuperTip = val433;
		((BarItem)this.commandBarCheckItem6).Caption = "Can Shrink";
		((BarCheckItem)this.commandBarCheckItem6).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem6.Command = (ReportCommand)211;
		((BarItem)this.commandBarCheckItem6).Enabled = false;
		((BarItem)this.commandBarCheckItem6).Id = 149;
		((BarItem)this.commandBarCheckItem6).Name = "commandBarCheckItem6";
		val436.FixedTooltipWidth = true;
		((ToolTipItem)val437).Text = "Can Shrink";
		val438.LeftIndent = 6;
		val438.Text = "Enable the selected controls to decrease their height to fit their content.";
		val436.Items.Add((BaseToolTipItem)(object)val437);
		val436.Items.Add((BaseToolTipItem)(object)val438);
		((BaseToolTipObject)val436).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem6).SuperTip = val436;
		((BarItem)this.commandBarCheckItem7).Caption = "Can Grow";
		((BarCheckItem)this.commandBarCheckItem7).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem7.Command = (ReportCommand)210;
		((BarItem)this.commandBarCheckItem7).Enabled = false;
		((BarItem)this.commandBarCheckItem7).Id = 150;
		((BarItem)this.commandBarCheckItem7).Name = "commandBarCheckItem7";
		val439.FixedTooltipWidth = true;
		((ToolTipItem)val440).Text = "Can Grow";
		val441.LeftIndent = 6;
		val441.Text = "Enable the selected controls to increase their height to fit their content.";
		val439.Items.Add((BaseToolTipItem)(object)val440);
		val439.Items.Add((BaseToolTipItem)(object)val441);
		((BaseToolTipObject)val439).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem7).SuperTip = val439;
		((BarItem)this.commandBarCheckItem8).Caption = "Auto Width";
		((BarCheckItem)this.commandBarCheckItem8).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem8.Command = (ReportCommand)215;
		((BarItem)this.commandBarCheckItem8).Enabled = false;
		((BarItem)this.commandBarCheckItem8).Id = 151;
		((BarItem)this.commandBarCheckItem8).Name = "commandBarCheckItem8";
		val442.FixedTooltipWidth = true;
		((ToolTipItem)val443).Text = "Cell Auto Width";
		val444.LeftIndent = 6;
		val444.Text = "Automatically adjust the cell width depending on the current font size.";
		val442.Items.Add((BaseToolTipItem)(object)val443);
		val442.Items.Add((BaseToolTipItem)(object)val444);
		((BaseToolTipObject)val442).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem8).SuperTip = val442;
		((BarItem)this.commandBarCheckItem9).Caption = "Auto Height";
		((BarCheckItem)this.commandBarCheckItem9).CheckBoxVisibility = (CheckBoxVisibility)1;
		this.commandBarCheckItem9.Command = (ReportCommand)216;
		((BarItem)this.commandBarCheckItem9).Enabled = false;
		((BarItem)this.commandBarCheckItem9).Id = 152;
		((BarItem)this.commandBarCheckItem9).Name = "commandBarCheckItem9";
		val445.FixedTooltipWidth = true;
		((ToolTipItem)val446).Text = "Cell Auto Height";
		val447.LeftIndent = 6;
		val447.Text = "Automatically adjust the cell height depending on the current font size.";
		val445.Items.Add((BaseToolTipItem)(object)val446);
		val445.Items.Add((BaseToolTipItem)(object)val447);
		((BaseToolTipObject)val445).MaxWidth = 210;
		((BarItem)this.commandBarCheckItem9).SuperTip = val445;
		((BarItem)this.commandBarItem123).Caption = "Add Data Source";
		this.commandBarItem123.Command = (ReportCommand)206;
		((BarItem)this.commandBarItem123).Enabled = false;
		((BarItem)this.commandBarItem123).Id = 153;
		((BarItem)this.commandBarItem123).Name = "commandBarItem123";
		val448.FixedTooltipWidth = true;
		((ToolTipItem)val449).Text = "Add Data Source";
		val450.LeftIndent = 6;
		val450.Text = "Set up a data source for a sparkline.";
		val448.Items.Add((BaseToolTipItem)(object)val449);
		val448.Items.Add((BaseToolTipItem)(object)val450);
		((BaseToolTipObject)val448).MaxWidth = 210;
		((BarItem)this.commandBarItem123).SuperTip = val448;
		((BarItem)this.commandBarItem124).Caption = "Flat Light";
		this.commandBarItem124.Command = (ReportCommand)220;
		((BarItem)this.commandBarItem124).Enabled = false;
		((BarItem)this.commandBarItem124).Id = 154;
		((BarItem)this.commandBarItem124).Name = "commandBarItem124";
		val451.FixedTooltipWidth = true;
		((ToolTipItem)val452).Text = "Flat Light Theme";
		val453.LeftIndent = 6;
		val453.Text = "Set the Flat Light color theme for a gauge.";
		val451.Items.Add((BaseToolTipItem)(object)val452);
		val451.Items.Add((BaseToolTipItem)(object)val453);
		((BaseToolTipObject)val451).MaxWidth = 210;
		((BarItem)this.commandBarItem124).SuperTip = val451;
		((BarItem)this.commandBarItem125).Caption = "Flat Dark";
		this.commandBarItem125.Command = (ReportCommand)221;
		((BarItem)this.commandBarItem125).Enabled = false;
		((BarItem)this.commandBarItem125).Id = 155;
		((BarItem)this.commandBarItem125).Name = "commandBarItem125";
		val454.FixedTooltipWidth = true;
		((ToolTipItem)val455).Text = "Flat Dark Theme";
		val456.LeftIndent = 6;
		val456.Text = "Set the Flat Dark color theme for a gauge.";
		val454.Items.Add((BaseToolTipItem)(object)val455);
		val454.Items.Add((BaseToolTipItem)(object)val456);
		((BaseToolTipObject)val454).MaxWidth = 210;
		((BarItem)this.commandBarItem125).SuperTip = val454;
		((BarItem)this.commandBarItem126).Caption = "Stretch";
		this.commandBarItem126.Command = (ReportCommand)205;
		((BarItem)this.commandBarItem126).Enabled = false;
		((BarItem)this.commandBarItem126).Id = 156;
		((BarItem)this.commandBarItem126).Name = "commandBarItem126";
		val457.FixedTooltipWidth = true;
		((ToolTipItem)val458).Text = "Stretch";
		val459.LeftIndent = 6;
		val459.Text = "Stretch a shape to fill its entire area when it is rotated.";
		val457.Items.Add((BaseToolTipItem)(object)val458);
		val457.Items.Add((BaseToolTipItem)(object)val459);
		((BaseToolTipObject)val457).MaxWidth = 210;
		((BarItem)this.commandBarItem126).SuperTip = val457;
		((BarItem)this.commandBarItem127).Caption = "Close";
		this.commandBarItem127.Command = (ReportCommand)29;
		((BarItem)this.commandBarItem127).Enabled = false;
		((BarItem)this.commandBarItem127).Id = 157;
		((BarItem)this.commandBarItem127).ItemShortcut = new BarShortcut(System.Windows.Forms.Keys.F4 | System.Windows.Forms.Keys.Control);
		((BarItem)this.commandBarItem127).Name = "commandBarItem127";
		val460.FixedTooltipWidth = true;
		((ToolTipItem)val461).Text = "Close (Ctrl+F4)";
		val462.LeftIndent = 6;
		val462.Text = "Close the current report.";
		val460.Items.Add((BaseToolTipItem)(object)val461);
		val460.Items.Add((BaseToolTipItem)(object)val462);
		((BaseToolTipObject)val460).MaxWidth = 210;
		((BarItem)this.commandBarItem127).SuperTip = val460;
		((BarItem)this.commandBarItem128).Caption = "Exit";
		this.commandBarItem128.Command = (ReportCommand)8;
		((BarItem)this.commandBarItem128).Id = 158;
		((BarItem)this.commandBarItem128).Name = "commandBarItem128";
		val463.FixedTooltipWidth = true;
		((ToolTipItem)val464).Text = "Exit";
		val465.LeftIndent = 6;
		val465.Text = "Close the report designer.";
		val463.Items.Add((BaseToolTipItem)(object)val464);
		val463.Items.Add((BaseToolTipItem)(object)val465);
		((BaseToolTipObject)val463).MaxWidth = 210;
		((BarItem)this.commandBarItem128).SuperTip = val463;
		((PopupMenuBase)this.applicationMenu1).ItemLinks.Add((BarItem)(object)this.commandBarItem6);
		((PopupMenuBase)this.applicationMenu1).ItemLinks.Add((BarItem)(object)this.commandBarItem9);
		((PopupMenuBase)this.applicationMenu1).ItemLinks.Add((BarItem)(object)this.commandBarItem7);
		((PopupMenuBase)this.applicationMenu1).ItemLinks.Add((BarItem)(object)this.commandBarItem8);
		((PopupMenuBase)this.applicationMenu1).ItemLinks.Add((BarItem)(object)this.commandBarItem127, true);
		((PopupMenuBase)this.applicationMenu1).ItemLinks.Add((BarItem)(object)this.commandBarItem128, true);
		((PopupMenuBase)this.applicationMenu1).MenuDrawMode = (MenuDrawMode)2;
		((PopupMenuBase)this.applicationMenu1).Name = "applicationMenu1";
		((PopupMenuBase)this.applicationMenu1).Ribbon = this.ribbonControl1;
		((RibbonPage)this.ribbonPage2).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[7]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup1,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup2,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup3,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup4,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup5,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup6,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup7
		});
		((RibbonPage)this.ribbonPage2).Name = "ribbonPage2";
		((RibbonPage)this.ribbonPage2).Text = "Home";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup1).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup1).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup1).ItemLinks).Add((BarItem)(object)this.commandBarItem6);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup1).ItemLinks).Add((BarItem)(object)this.commandBarItem9);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup1).ItemLinks).Add((BarItem)(object)this.commandBarItem7);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup1).ItemLinks).Add((BarItem)(object)this.commandBarItem8);
		this.xrDesignRibbonPageGroup1.Kind = (XRDesignRibbonPageGroupKind)0;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup1).Name = "xrDesignRibbonPageGroup1";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup1).Text = "Report";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup2).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup2).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup2).ItemLinks).Add((BarItem)(object)this.commandBarItem30);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup2).ItemLinks).Add((BarItem)(object)this.commandBarItem28);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup2).ItemLinks).Add((BarItem)(object)this.commandBarItem29);
		this.xrDesignRibbonPageGroup2.Kind = (XRDesignRibbonPageGroupKind)3;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup2).Name = "xrDesignRibbonPageGroup2";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup2).Text = "Data";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup3).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup3).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.commandBarItem14);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.commandBarItem15);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.commandBarItem16);
		this.xrDesignRibbonPageGroup3.Kind = (XRDesignRibbonPageGroupKind)4;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup3).Name = "xrDesignRibbonPageGroup3";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup3).Text = "Clipboard";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup4).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup4).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup1);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup2);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup3);
		this.xrDesignRibbonPageGroup4.Kind = (XRDesignRibbonPageGroupKind)2;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup4).Name = "xrDesignRibbonPageGroup4";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup4).Text = "Font";
		((BarItem)this.xrDesignBarButtonGroup1).Id = 159;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup1).ItemLinks.Add((BarItem)(object)this.barEditItem1);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup1).ItemLinks.Add((BarItem)(object)this.barEditItem2);
		((BarItem)this.xrDesignBarButtonGroup1).Name = "xrDesignBarButtonGroup1";
		((BarItem)this.xrDesignBarButtonGroup2).Id = 160;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup2).ItemLinks.Add((BarItem)(object)this.commandBarItem17);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup2).ItemLinks.Add((BarItem)(object)this.commandBarItem18);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup2).ItemLinks.Add((BarItem)(object)this.commandBarItem19);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup2).ItemLinks.Add((BarItem)(object)this.commandBarItem24);
		((BarItem)this.xrDesignBarButtonGroup2).Name = "xrDesignBarButtonGroup2";
		((BarItem)this.xrDesignBarButtonGroup3).Id = 161;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup3).ItemLinks.Add((BarItem)(object)this.commandColorBarItem2);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup3).ItemLinks.Add((BarItem)(object)this.commandColorBarItem1);
		((BarItem)this.xrDesignBarButtonGroup3).Name = "xrDesignBarButtonGroup3";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup5).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup5).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup4);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup5);
		this.xrDesignRibbonPageGroup5.Kind = (XRDesignRibbonPageGroupKind)5;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup5).Name = "xrDesignRibbonPageGroup5";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup5).Text = "Alignment";
		((BarItem)this.xrDesignBarButtonGroup4).Id = 162;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup4).ItemLinks.Add((BarItem)(object)this.commandBarItem25);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup4).ItemLinks.Add((BarItem)(object)this.commandBarItem26);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup4).ItemLinks.Add((BarItem)(object)this.commandBarItem27);
		((BarItem)this.xrDesignBarButtonGroup4).Name = "xrDesignBarButtonGroup4";
		((BarItem)this.xrDesignBarButtonGroup5).Id = 163;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup5).ItemLinks.Add((BarItem)(object)this.commandBarItem20);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup5).ItemLinks.Add((BarItem)(object)this.commandBarItem21);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup5).ItemLinks.Add((BarItem)(object)this.commandBarItem22);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup5).ItemLinks.Add((BarItem)(object)this.commandBarItem23);
		((BarItem)this.xrDesignBarButtonGroup5).Name = "xrDesignBarButtonGroup5";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup6).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup6).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup6).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup6);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup6).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup7);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup6).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup8);
		this.xrDesignRibbonPageGroup6.Kind = (XRDesignRibbonPageGroupKind)6;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup6).Name = "xrDesignRibbonPageGroup6";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup6).Text = "Borders";
		((BarItem)this.xrDesignBarButtonGroup6).Id = 164;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup6).ItemLinks.Add((BarItem)(object)this.commandBarItem32);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup6).ItemLinks.Add((BarItem)(object)this.commandBarItem33);
		((BarItem)this.xrDesignBarButtonGroup6).Name = "xrDesignBarButtonGroup6";
		((BarItem)this.xrDesignBarButtonGroup7).Id = 165;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup7).ItemLinks.Add((BarItem)(object)this.commandBarItem34);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup7).ItemLinks.Add((BarItem)(object)this.commandBarItem35);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup7).ItemLinks.Add((BarItem)(object)this.commandBarItem36);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup7).ItemLinks.Add((BarItem)(object)this.commandBarItem37);
		((BarItem)this.xrDesignBarButtonGroup7).Name = "xrDesignBarButtonGroup7";
		((BarItem)this.xrDesignBarButtonGroup8).Id = 166;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup8).ItemLinks.Add((BarItem)(object)this.commandColorBarItem3);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup8).ItemLinks.Add((BarItem)(object)this.commandBarItem38);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup8).ItemLinks.Add((BarItem)(object)this.commandBarEditItem6);
		((BarItem)this.xrDesignBarButtonGroup8).Name = "xrDesignBarButtonGroup8";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup7).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup7).CaptionButtonVisible = (DefaultBoolean)0;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup7).ItemLinks).Add((BarItem)(object)this.commandBarItem31);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup7).ItemLinks).Add((BarItem)(object)this.commandGalleryBarItem1);
		this.xrDesignRibbonPageGroup7.Kind = (XRDesignRibbonPageGroupKind)7;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup7).Name = "xrDesignRibbonPageGroup7";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup7).Text = "Styles";
		((RibbonPage)this.ribbonPage3).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[4]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup8,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup9,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup10,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup11
		});
		((RibbonPage)this.ribbonPage3).Name = "ribbonPage3";
		((RibbonPage)this.ribbonPage3).Text = "Layout";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup8).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup8).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup8).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup9);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup8).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup10);
		this.xrDesignRibbonPageGroup8.Kind = (XRDesignRibbonPageGroupKind)9;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup8).Name = "xrDesignRibbonPageGroup8";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup8).Text = "Alignment";
		((BarItem)this.xrDesignBarButtonGroup9).Id = 167;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup9).ItemLinks.Add((BarItem)(object)this.commandBarItem40);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup9).ItemLinks.Add((BarItem)(object)this.commandBarItem41);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup9).ItemLinks.Add((BarItem)(object)this.commandBarItem42);
		((BarItem)this.xrDesignBarButtonGroup9).Name = "xrDesignBarButtonGroup9";
		((BarItem)this.xrDesignBarButtonGroup10).Id = 168;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup10).ItemLinks.Add((BarItem)(object)this.commandBarItem43);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup10).ItemLinks.Add((BarItem)(object)this.commandBarItem44);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup10).ItemLinks.Add((BarItem)(object)this.commandBarItem45);
		((BarItem)this.xrDesignBarButtonGroup10).Name = "xrDesignBarButtonGroup10";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup9).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup9).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.commandBarItem39);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.commandBarItem47);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.commandBarItem62);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup11);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup12);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup13);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup9).ItemLinks).Add((BarItem)(object)this.xrDesignBarButtonGroup14);
		this.xrDesignRibbonPageGroup9.Kind = (XRDesignRibbonPageGroupKind)11;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup9).Name = "xrDesignRibbonPageGroup9";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup9).Text = "Layout";
		((BarItem)this.xrDesignBarButtonGroup11).Id = 169;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup11).ItemLinks.Add((BarItem)(object)this.commandBarItem50);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup11).ItemLinks.Add((BarItem)(object)this.commandBarItem51);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup11).ItemLinks.Add((BarItem)(object)this.commandBarItem52);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup11).ItemLinks.Add((BarItem)(object)this.commandBarItem53);
		((BarItem)this.xrDesignBarButtonGroup11).Name = "xrDesignBarButtonGroup11";
		((BarItem)this.xrDesignBarButtonGroup12).Id = 170;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup12).ItemLinks.Add((BarItem)(object)this.commandBarItem46);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup12).ItemLinks.Add((BarItem)(object)this.commandBarItem48);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup12).ItemLinks.Add((BarItem)(object)this.commandBarItem49);
		((BarItem)this.xrDesignBarButtonGroup12).Name = "xrDesignBarButtonGroup12";
		((BarItem)this.xrDesignBarButtonGroup13).Id = 171;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup13).ItemLinks.Add((BarItem)(object)this.commandBarItem54);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup13).ItemLinks.Add((BarItem)(object)this.commandBarItem55);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup13).ItemLinks.Add((BarItem)(object)this.commandBarItem56);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup13).ItemLinks.Add((BarItem)(object)this.commandBarItem57);
		((BarItem)this.xrDesignBarButtonGroup13).Name = "xrDesignBarButtonGroup13";
		((BarItem)this.xrDesignBarButtonGroup14).Id = 172;
		((BarCustomContainerItem)this.xrDesignBarButtonGroup14).ItemLinks.Add((BarItem)(object)this.commandBarItem58);
		((BarCustomContainerItem)this.xrDesignBarButtonGroup14).ItemLinks.Add((BarItem)(object)this.commandBarItem59);
		((BarItem)this.xrDesignBarButtonGroup14).Name = "xrDesignBarButtonGroup14";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup10).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup10).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup10).ItemLinks).Add((BarItem)(object)this.commandBarItem60);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup10).ItemLinks).Add((BarItem)(object)this.commandBarItem61);
		this.xrDesignRibbonPageGroup10.Kind = (XRDesignRibbonPageGroupKind)12;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup10).Name = "xrDesignRibbonPageGroup10";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup10).Text = "Arranging";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup11).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup11).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup11).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem1);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup11).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem2);
		this.xrDesignRibbonPageGroup11.Kind = (XRDesignRibbonPageGroupKind)36;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup11).Name = "xrDesignRibbonPageGroup11";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup11).Text = "Snapping";
		((RibbonPage)this.ribbonPage4).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[2]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup12,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup13
		});
		((RibbonPage)this.ribbonPage4).Name = "ribbonPage4";
		((RibbonPage)this.ribbonPage4).Text = "Page";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup12).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup12).CaptionButtonVisible = (DefaultBoolean)0;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup12).ItemLinks).Add((BarItem)(object)this.commandBarItem65);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup12).ItemLinks).Add((BarItem)(object)this.commandBarItem64);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup12).ItemLinks).Add((BarItem)(object)this.commandBarItem63);
		this.xrDesignRibbonPageGroup12.Kind = (XRDesignRibbonPageGroupKind)13;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup12).Name = "xrDesignRibbonPageGroup12";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup12).Text = "Page Setup";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup13).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup13).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup13).ItemLinks).Add((BarItem)(object)this.commandColorBarItem4);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup13).ItemLinks).Add((BarItem)(object)this.commandBarItem66);
		this.xrDesignRibbonPageGroup13.Kind = (XRDesignRibbonPageGroupKind)14;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup13).Name = "xrDesignRibbonPageGroup13";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup13).Text = "Appearance";
		((RibbonPage)this.ribbonPage5).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[5]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup14,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup15,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup16,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup17,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup18
		});
		((RibbonPage)this.ribbonPage5).Name = "ribbonPage5";
		((RibbonPage)this.ribbonPage5).Text = "View";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup14).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup14).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup14).ItemLinks).Add((BarItem)(object)this.commandBarItem67);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup14).ItemLinks).Add((BarItem)(object)this.commandBarItem68);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup14).ItemLinks).Add((BarItem)(object)this.commandBarItem69, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup14).ItemLinks).Add((BarItem)(object)this.commandBarItem70);
		this.xrDesignRibbonPageGroup14.Kind = (XRDesignRibbonPageGroupKind)35;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup14).Name = "xrDesignRibbonPageGroup14";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup14).Text = "Show";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup15).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup15).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup15).ItemLinks).Add((BarItem)(object)this.commandBarItem71);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup15).ItemLinks).Add((BarItem)(object)this.commandBarItem72);
		this.xrDesignRibbonPageGroup15.Kind = (XRDesignRibbonPageGroupKind)37;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup15).Name = "xrDesignRibbonPageGroup15";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup15).Text = "Bands";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup16).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup16).CaptionButtonVisible = (DefaultBoolean)0;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup16).ItemLinks).Add((BarItem)(object)this.commandBarEditItem1);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup16).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem3);
		this.xrDesignRibbonPageGroup16.Kind = (XRDesignRibbonPageGroupKind)38;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup16).Name = "xrDesignRibbonPageGroup16";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup16).Text = "Localization";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup17).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup17).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup17).ItemLinks).Add((BarItem)(object)this.commandBarItem75);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup17).ItemLinks).Add((BarItem)(object)this.commandBarItem73);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup17).ItemLinks).Add((BarItem)(object)this.commandBarItem74);
		this.xrDesignRibbonPageGroup17.Kind = (XRDesignRibbonPageGroupKind)16;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup17).Name = "xrDesignRibbonPageGroup17";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup17).Text = "Zoom";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup18).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup18).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup18).ItemLinks).Add((BarItem)(object)this.barDockPanelsListItem1);
		this.xrDesignRibbonPageGroup18.Kind = (XRDesignRibbonPageGroupKind)17;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup18).Name = "xrDesignRibbonPageGroup18";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup18).Text = "View";
		((RibbonPage)this.ribbonPage6).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[1] { (RibbonPageGroup)this.xrDesignRibbonPageGroup19 });
		((RibbonPage)this.ribbonPage6).Name = "ribbonPage6";
		((RibbonPage)this.ribbonPage6).Text = "Scripts";
		((RibbonPage)this.ribbonPage6).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup19).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup19).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup19).ItemLinks).Add((BarItem)(object)this.commandBarEditItem7);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup19).ItemLinks).Add((BarItem)(object)this.commandBarEditItem8);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup19).ItemLinks).Add((BarItem)(object)this.commandBarItem76);
		this.xrDesignRibbonPageGroup19.Kind = (XRDesignRibbonPageGroupKind)1;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup19).Name = "xrDesignRibbonPageGroup19";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup19).Text = "Edit";
		((RibbonPage)this.ribbonPage7).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[1] { (RibbonPageGroup)this.xrDesignRibbonPageGroup20 });
		((RibbonPage)this.ribbonPage7).Name = "ribbonPage7";
		((RibbonPage)this.ribbonPage7).Text = "Design";
		((RibbonPage)this.ribbonPage7).Visible = false;
		((RibbonPageCategory)this.ribbonPageCategory1).Appearance.BackColor = System.Drawing.Color.FromArgb(147, 94, 211);
		((RibbonPageCategory)this.ribbonPageCategory1).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory1).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory1).Name = "ribbonPageCategory1";
		((RibbonPageCategory)this.ribbonPageCategory1).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage7 });
		((RibbonPageCategory)this.ribbonPageCategory1).Text = "Character Comb Tools";
		((RibbonPageCategory)this.ribbonPageCategory1).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup20).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup20).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup20).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem8);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup20).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem9);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup20).ItemLinks).Add((BarItem)(object)this.commandBarEditItem2, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup20).ItemLinks).Add((BarItem)(object)this.commandBarEditItem3);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup20).ItemLinks).Add((BarItem)(object)this.commandBarEditItem4, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup20).ItemLinks).Add((BarItem)(object)this.commandBarEditItem5);
		this.xrDesignRibbonPageGroup20.Kind = (XRDesignRibbonPageGroupKind)26;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup20).Name = "xrDesignRibbonPageGroup20";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup20).Text = "Cell Size";
		((RibbonPageCategory)this.ribbonPageCategory2).Appearance.BackColor = System.Drawing.Color.FromArgb(242, 203, 29);
		((RibbonPageCategory)this.ribbonPageCategory2).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory2).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory2).Name = "ribbonPageCategory2";
		((RibbonPageCategory)this.ribbonPageCategory2).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage8 });
		((RibbonPageCategory)this.ribbonPageCategory2).Text = "Table Tools";
		((RibbonPageCategory)this.ribbonPageCategory2).Visible = false;
		((RibbonPage)this.ribbonPage8).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[5]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup21,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup22,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup23,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup24,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup25
		});
		((RibbonPage)this.ribbonPage8).Name = "ribbonPage8";
		((RibbonPage)this.ribbonPage8).Text = "Design";
		((RibbonPage)this.ribbonPage8).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup21).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup21).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup21).ItemLinks).Add((BarItem)(object)this.commandBarItem78);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup21).ItemLinks).Add((BarItem)(object)this.commandBarItem79);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup21).ItemLinks).Add((BarItem)(object)this.commandBarItem77);
		this.xrDesignRibbonPageGroup21.Kind = (XRDesignRibbonPageGroupKind)21;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup21).Name = "xrDesignRibbonPageGroup21";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup21).Text = "Select";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup22).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup22).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup22).ItemLinks).Add((BarItem)(object)this.commandBarItem84);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup22).ItemLinks).Add((BarItem)(object)this.commandBarItem85);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup22).ItemLinks).Add((BarItem)(object)this.commandBarItem86);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup22).ItemLinks).Add((BarItem)(object)this.commandBarItem87);
		this.xrDesignRibbonPageGroup22.Kind = (XRDesignRibbonPageGroupKind)22;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup22).Name = "xrDesignRibbonPageGroup22";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup22).Text = "Insert";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup23).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup23).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup23).ItemLinks).Add((BarItem)(object)this.commandBarItem80);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup23).ItemLinks).Add((BarItem)(object)this.commandBarItem81);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup23).ItemLinks).Add((BarItem)(object)this.commandBarItem82);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup23).ItemLinks).Add((BarItem)(object)this.commandBarItem83);
		this.xrDesignRibbonPageGroup23.Kind = (XRDesignRibbonPageGroupKind)23;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup23).Name = "xrDesignRibbonPageGroup23";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup23).Text = "Delete";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup24).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup24).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup24).ItemLinks).Add((BarItem)(object)this.commandBarItem90);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup24).ItemLinks).Add((BarItem)(object)this.commandBarItem91);
		this.xrDesignRibbonPageGroup24.Kind = (XRDesignRibbonPageGroupKind)25;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup24).Name = "xrDesignRibbonPageGroup24";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup24).Text = "Merge";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup25).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup25).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup25).ItemLinks).Add((BarItem)(object)this.commandBarItem88);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup25).ItemLinks).Add((BarItem)(object)this.commandBarItem89);
		this.xrDesignRibbonPageGroup25.Kind = (XRDesignRibbonPageGroupKind)24;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup25).Name = "xrDesignRibbonPageGroup25";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup25).Text = "Distribute";
		((RibbonPageCategory)this.ribbonPageCategory3).Appearance.BackColor = System.Drawing.Color.FromArgb(73, 163, 73);
		((RibbonPageCategory)this.ribbonPageCategory3).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory3).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory3).Name = "ribbonPageCategory3";
		((RibbonPageCategory)this.ribbonPageCategory3).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage9 });
		((RibbonPageCategory)this.ribbonPageCategory3).Text = "Chart Tools";
		((RibbonPageCategory)this.ribbonPageCategory3).Visible = false;
		((RibbonPage)this.ribbonPage9).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[5]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup26,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup27,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup28,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup29,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup30
		});
		((RibbonPage)this.ribbonPage9).Name = "ribbonPage9";
		((RibbonPage)this.ribbonPage9).Text = "Design";
		((RibbonPage)this.ribbonPage9).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup26).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup26).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup26).ItemLinks).Add((BarItem)(object)this.commandBarItem105);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup26).ItemLinks).Add((BarItem)(object)this.commandBarItem104);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup26).ItemLinks).Add((BarItem)(object)this.commandBarItem103);
		this.xrDesignRibbonPageGroup26.Kind = (XRDesignRibbonPageGroupKind)10;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup26).Name = "xrDesignRibbonPageGroup26";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup26).Text = "Layout";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup27).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup27).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup27).ItemLinks).Add((BarItem)(object)this.commandBarItem106);
		this.xrDesignRibbonPageGroup27.Kind = (XRDesignRibbonPageGroupKind)3;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup27).Name = "xrDesignRibbonPageGroup27";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup27).Text = "Data";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup28).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup28).CaptionButtonVisible = (DefaultBoolean)0;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem108);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem109);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem110);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem111);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem112);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem113);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem114);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup28).ItemLinks).Add((BarItem)(object)this.commandBarItem115);
		this.xrDesignRibbonPageGroup28.Kind = (XRDesignRibbonPageGroupKind)33;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup28).Name = "xrDesignRibbonPageGroup28";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup28).Text = "Series";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup29).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup29).CaptionButtonVisible = (DefaultBoolean)0;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup29).ItemLinks).Add((BarItem)(object)this.commandBarItem116);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup29).ItemLinks).Add((BarItem)(object)this.commandBarItem117);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup29).ItemLinks).Add((BarItem)(object)this.commandBarItem118);
		this.xrDesignRibbonPageGroup29.Kind = (XRDesignRibbonPageGroupKind)34;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup29).Name = "xrDesignRibbonPageGroup29";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup29).Text = "Annotations";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup30).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup30).CaptionButtonVisible = (DefaultBoolean)0;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup30).ItemLinks).Add((BarItem)(object)this.commandBarItem107);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup30).ItemLinks).Add((BarItem)(object)this.commandGalleryBarItem3);
		this.xrDesignRibbonPageGroup30.Kind = (XRDesignRibbonPageGroupKind)14;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup30).Name = "xrDesignRibbonPageGroup30";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup30).Text = "Appearance";
		((RibbonPageCategory)this.ribbonPageCategory4).Appearance.BackColor = System.Drawing.Color.FromArgb(201, 89, 156);
		((RibbonPageCategory)this.ribbonPageCategory4).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory4).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory4).Name = "ribbonPageCategory4";
		((RibbonPageCategory)this.ribbonPageCategory4).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage10 });
		((RibbonPageCategory)this.ribbonPageCategory4).Text = "Pivot Grid Tools";
		((RibbonPageCategory)this.ribbonPageCategory4).Visible = false;
		((RibbonPage)this.ribbonPage10).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[4]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup31,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup32,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup33,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup34
		});
		((RibbonPage)this.ribbonPage10).Name = "ribbonPage10";
		((RibbonPage)this.ribbonPage10).Text = "Design";
		((RibbonPage)this.ribbonPage10).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup31).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup31).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup31).ItemLinks).Add((BarItem)(object)this.commandBarItem92);
		this.xrDesignRibbonPageGroup31.Kind = (XRDesignRibbonPageGroupKind)10;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup31).Name = "xrDesignRibbonPageGroup31";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup31).Text = "Layout";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup32).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup32).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup32).ItemLinks).Add((BarItem)(object)this.commandBarItem93);
		this.xrDesignRibbonPageGroup32.Kind = (XRDesignRibbonPageGroupKind)3;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup32).Name = "xrDesignRibbonPageGroup32";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup32).Text = "Data";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup33).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup33).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup33).ItemLinks).Add((BarItem)(object)this.commandBarItem95);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup33).ItemLinks).Add((BarItem)(object)this.commandBarItem94);
		this.xrDesignRibbonPageGroup33.Kind = (XRDesignRibbonPageGroupKind)29;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup33).Name = "xrDesignRibbonPageGroup33";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup33).Text = "Fields";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup34).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup34).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem96);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem97);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem100, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem99);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem98);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem101, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup34).ItemLinks).Add((BarItem)(object)this.commandBarItem102);
		this.xrDesignRibbonPageGroup34.Kind = (XRDesignRibbonPageGroupKind)30;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup34).Name = "xrDesignRibbonPageGroup34";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup34).Text = "Print Options";
		((RibbonPageCategory)this.ribbonPageCategory5).Appearance.BackColor = System.Drawing.Color.FromArgb(147, 94, 211);
		((RibbonPageCategory)this.ribbonPageCategory5).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory5).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory5).Name = "ribbonPageCategory5";
		((RibbonPageCategory)this.ribbonPageCategory5).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage11 });
		((RibbonPageCategory)this.ribbonPageCategory5).Text = "Barcode Tools";
		((RibbonPageCategory)this.ribbonPageCategory5).Visible = false;
		((RibbonPage)this.ribbonPage11).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[2]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup35,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup36
		});
		((RibbonPage)this.ribbonPage11).Name = "ribbonPage11";
		((RibbonPage)this.ribbonPage11).Text = "Design";
		((RibbonPage)this.ribbonPage11).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup35).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup35).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup35).ItemLinks).Add((BarItem)(object)this.commandBarItem119);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup35).ItemLinks).Add((BarItem)(object)this.commandBarItem120);
		this.xrDesignRibbonPageGroup35.Kind = (XRDesignRibbonPageGroupKind)17;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup35).Name = "xrDesignRibbonPageGroup35";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup35).Text = "View";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup36).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup36).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup36).ItemLinks).Add((BarItem)(object)this.commandGalleryBarItem2);
		this.xrDesignRibbonPageGroup36.Kind = (XRDesignRibbonPageGroupKind)31;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup36).Name = "xrDesignRibbonPageGroup36";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup36).Text = "Symbology";
		((RibbonPage)this.ribbonPage12).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[2]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup37,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup38
		});
		((RibbonPage)this.ribbonPage12).Name = "ribbonPage12";
		((RibbonPage)this.ribbonPage12).Text = "Design";
		((RibbonPage)this.ribbonPage12).Visible = false;
		((RibbonPageCategory)this.ribbonPageCategory6).Appearance.BackColor = System.Drawing.Color.FromArgb(255, 157, 0);
		((RibbonPageCategory)this.ribbonPageCategory6).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory6).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory6).Name = "ribbonPageCategory6";
		((RibbonPageCategory)this.ribbonPageCategory6).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage12 });
		((RibbonPageCategory)this.ribbonPageCategory6).Text = "Gauge Tools";
		((RibbonPageCategory)this.ribbonPageCategory6).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup37).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup37).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup37).ItemLinks).Add((BarItem)(object)this.commandGalleryBarItem5);
		this.xrDesignRibbonPageGroup37.Kind = (XRDesignRibbonPageGroupKind)17;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup37).Name = "xrDesignRibbonPageGroup37";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup37).Text = "View";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup38).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup38).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup38).ItemLinks).Add((BarItem)(object)this.commandBarItem124);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup38).ItemLinks).Add((BarItem)(object)this.commandBarItem125);
		this.xrDesignRibbonPageGroup38.Kind = (XRDesignRibbonPageGroupKind)18;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup38).Name = "xrDesignRibbonPageGroup38";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup38).Text = "Theme";
		((RibbonPage)this.ribbonPage13).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[2]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup39,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup40
		});
		((RibbonPage)this.ribbonPage13).Name = "ribbonPage13";
		((RibbonPage)this.ribbonPage13).Text = "Design";
		((RibbonPage)this.ribbonPage13).Visible = false;
		((RibbonPageCategory)this.ribbonPageCategory7).Appearance.BackColor = System.Drawing.Color.FromArgb(73, 163, 73);
		((RibbonPageCategory)this.ribbonPageCategory7).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory7).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory7).Name = "ribbonPageCategory7";
		((RibbonPageCategory)this.ribbonPageCategory7).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage13 });
		((RibbonPageCategory)this.ribbonPageCategory7).Text = "Sparkline Tools";
		((RibbonPageCategory)this.ribbonPageCategory7).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup39).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup39).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup39).ItemLinks).Add((BarItem)(object)this.commandBarItem123);
		this.xrDesignRibbonPageGroup39.Kind = (XRDesignRibbonPageGroupKind)3;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup39).Name = "xrDesignRibbonPageGroup39";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup39).Text = "Data";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup40).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup40).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup40).ItemLinks).Add((BarItem)(object)this.commandGalleryBarItem4);
		this.xrDesignRibbonPageGroup40.Kind = (XRDesignRibbonPageGroupKind)17;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup40).Name = "xrDesignRibbonPageGroup40";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup40).Text = "View";
		((RibbonPage)this.ribbonPage14).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[1] { (RibbonPageGroup)this.xrDesignRibbonPageGroup41 });
		((RibbonPage)this.ribbonPage14).Name = "ribbonPage14";
		((RibbonPage)this.ribbonPage14).Text = "Design";
		((RibbonPage)this.ribbonPage14).Visible = false;
		((RibbonPageCategory)this.ribbonPageCategory8).Appearance.BackColor = System.Drawing.Color.FromArgb(255, 157, 0);
		((RibbonPageCategory)this.ribbonPageCategory8).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory8).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory8).Name = "ribbonPageCategory8";
		((RibbonPageCategory)this.ribbonPageCategory8).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage14 });
		((RibbonPageCategory)this.ribbonPageCategory8).Text = "Shape Tools";
		((RibbonPageCategory)this.ribbonPageCategory8).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup41).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup41).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup41).ItemLinks).Add((BarItem)(object)this.commandBarItem126);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup41).ItemLinks).Add((BarItem)(object)this.commandGalleryBarItem6);
		this.xrDesignRibbonPageGroup41.Kind = (XRDesignRibbonPageGroupKind)17;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup41).Name = "xrDesignRibbonPageGroup41";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup41).Text = "View";
		((RibbonPage)this.ribbonPage15).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[2]
		{
			(RibbonPageGroup)this.xrDesignRibbonPageGroup42,
			(RibbonPageGroup)this.xrDesignRibbonPageGroup43
		});
		((RibbonPage)this.ribbonPage15).Name = "ribbonPage15";
		((RibbonPage)this.ribbonPage15).Text = "Text";
		((RibbonPage)this.ribbonPage15).Visible = false;
		((RibbonPageCategory)this.ribbonPageCategory9).Appearance.BackColor = System.Drawing.Color.FromArgb(158, 197, 126);
		((RibbonPageCategory)this.ribbonPageCategory9).Appearance.Options.UseBackColor = true;
		((RibbonPageCategory)this.ribbonPageCategory9).AutoStretchPageHeaders = true;
		((RibbonPageCategory)this.ribbonPageCategory9).Name = "ribbonPageCategory9";
		((RibbonPageCategory)this.ribbonPageCategory9).Pages.AddRange((RibbonPage[])(object)new RibbonPage[1] { (RibbonPage)this.ribbonPage15 });
		((RibbonPageCategory)this.ribbonPageCategory9).Text = "Text Tools";
		((RibbonPageCategory)this.ribbonPageCategory9).Visible = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup42).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup42).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup42).ItemLinks).Add((BarItem)(object)this.commandBarItem121);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup42).ItemLinks).Add((BarItem)(object)this.commandBarItem122);
		this.xrDesignRibbonPageGroup42.Kind = (XRDesignRibbonPageGroupKind)15;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup42).Name = "xrDesignRibbonPageGroup42";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup42).Text = "Design";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup43).AllowTextClipping = false;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup43).CaptionButtonVisible = (DefaultBoolean)1;
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup43).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem4);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup43).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem5);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup43).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem6, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.xrDesignRibbonPageGroup43).ItemLinks).Add((BarItem)(object)this.commandBarCheckItem7);
		this.xrDesignRibbonPageGroup43.Kind = (XRDesignRibbonPageGroupKind)8;
		((RibbonPageGroup)this.xrDesignRibbonPageGroup43).Name = "xrDesignRibbonPageGroup43";
		((RibbonPageGroup)this.xrDesignRibbonPageGroup43).Text = "Behavior";
		((BarBaseButtonItem)this.printPreviewBarItem1).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem1).Caption = "Editing Fields";
		this.printPreviewBarItem1.Command = (PrintingSystemCommand)72;
		this.printPreviewBarItem1.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem1).Enabled = false;
		((BarItem)this.printPreviewBarItem1).Id = 173;
		((BarItem)this.printPreviewBarItem1).Name = "printPreviewBarItem1";
		val466.FixedTooltipWidth = true;
		((ToolTipItem)val467).Text = "Highlight Editing Fields";
		val468.LeftIndent = 6;
		val468.Text = "Highlight all editing fields to quickly discover which of the document elements are editable.";
		val466.Items.Add((BaseToolTipItem)(object)val467);
		val466.Items.Add((BaseToolTipItem)(object)val468);
		((BaseToolTipObject)val466).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem1).SuperTip = val466;
		((BarBaseButtonItem)this.printPreviewBarItem2).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem2).Caption = "Bookmarks";
		this.printPreviewBarItem2.Command = (PrintingSystemCommand)1;
		this.printPreviewBarItem2.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem2).Enabled = false;
		((BarItem)this.printPreviewBarItem2).Id = 174;
		((BarItem)this.printPreviewBarItem2).Name = "printPreviewBarItem2";
		val469.FixedTooltipWidth = true;
		((ToolTipItem)val470).Text = "Document Map";
		val471.LeftIndent = 6;
		val471.Text = "Open the Document Map, which allows you to navigate through a structural view of the document.";
		val469.Items.Add((BaseToolTipItem)(object)val470);
		val469.Items.Add((BaseToolTipItem)(object)val471);
		((BaseToolTipObject)val469).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem2).SuperTip = val469;
		((BarBaseButtonItem)this.printPreviewBarItem3).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem3).Caption = "Parameters";
		this.printPreviewBarItem3.Command = (PrintingSystemCommand)2;
		this.printPreviewBarItem3.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem3).Enabled = false;
		((BarItem)this.printPreviewBarItem3).Id = 175;
		((BarItem)this.printPreviewBarItem3).Name = "printPreviewBarItem3";
		val472.FixedTooltipWidth = true;
		((ToolTipItem)val473).Text = "Parameters";
		val474.LeftIndent = 6;
		val474.Text = "Open the Parameters pane, which allows you to enter values for report parameters.";
		val472.Items.Add((BaseToolTipItem)(object)val473);
		val472.Items.Add((BaseToolTipItem)(object)val474);
		((BaseToolTipObject)val472).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem3).SuperTip = val472;
		((BarBaseButtonItem)this.printPreviewBarItem4).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem4).Caption = "Find";
		this.printPreviewBarItem4.Command = (PrintingSystemCommand)24;
		this.printPreviewBarItem4.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem4).Enabled = false;
		((BarItem)this.printPreviewBarItem4).Id = 176;
		((BarItem)this.printPreviewBarItem4).Name = "printPreviewBarItem4";
		val475.FixedTooltipWidth = true;
		((ToolTipItem)val476).Text = "Find";
		val477.LeftIndent = 6;
		val477.Text = "Show the Find dialog to find text in the document.";
		val475.Items.Add((BaseToolTipItem)(object)val476);
		val475.Items.Add((BaseToolTipItem)(object)val477);
		((BaseToolTipObject)val475).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem4).SuperTip = val475;
		((BarBaseButtonItem)this.printPreviewBarItem5).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem5).Caption = "Thumbnails";
		this.printPreviewBarItem5.Command = (PrintingSystemCommand)71;
		this.printPreviewBarItem5.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem5).Enabled = false;
		((BarItem)this.printPreviewBarItem5).Id = 177;
		((BarItem)this.printPreviewBarItem5).Name = "printPreviewBarItem5";
		val478.FixedTooltipWidth = true;
		((ToolTipItem)val479).Text = "Thumbnails";
		val480.LeftIndent = 6;
		val480.Text = "Open the Thumbnails, which allows you to navigate through the document.";
		val478.Items.Add((BaseToolTipItem)(object)val479);
		val478.Items.Add((BaseToolTipItem)(object)val480);
		((BaseToolTipObject)val478).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem5).SuperTip = val478;
		((BarItem)this.printPreviewBarItem7).Caption = "Print";
		this.printPreviewBarItem7.Command = (PrintingSystemCommand)6;
		this.printPreviewBarItem7.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem7).Enabled = false;
		((BarItem)this.printPreviewBarItem7).Id = 179;
		((BarItem)this.printPreviewBarItem7).Name = "printPreviewBarItem7";
		val481.FixedTooltipWidth = true;
		((ToolTipItem)val482).Text = "Print (Ctrl+P)";
		val483.LeftIndent = 6;
		val483.Text = "Select a printer, number of copies and other printing options before printing.";
		val481.Items.Add((BaseToolTipItem)(object)val482);
		val481.Items.Add((BaseToolTipItem)(object)val483);
		((BaseToolTipObject)val481).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem7).SuperTip = val481;
		((BarItem)this.printPreviewBarItem8).Caption = "Quick Print";
		this.printPreviewBarItem8.Command = (PrintingSystemCommand)7;
		this.printPreviewBarItem8.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem8).Enabled = false;
		((BarItem)this.printPreviewBarItem8).Id = 180;
		((BarItem)this.printPreviewBarItem8).Name = "printPreviewBarItem8";
		val484.FixedTooltipWidth = true;
		((ToolTipItem)val485).Text = "Quick Print";
		val486.LeftIndent = 6;
		val486.Text = "Send the document directly to the default printer without making changes.";
		val484.Items.Add((BaseToolTipItem)(object)val485);
		val484.Items.Add((BaseToolTipItem)(object)val486);
		((BaseToolTipObject)val484).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem8).SuperTip = val484;
		((BarItem)this.printPreviewBarItem9).Caption = "Custom Margins...";
		this.printPreviewBarItem9.Command = (PrintingSystemCommand)8;
		this.printPreviewBarItem9.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem9).Enabled = false;
		((BarItem)this.printPreviewBarItem9).Id = 181;
		((BarItem)this.printPreviewBarItem9).Name = "printPreviewBarItem9";
		val487.FixedTooltipWidth = true;
		((ToolTipItem)val488).Text = "Page Setup";
		val489.LeftIndent = 6;
		val489.Text = "Show the Page Setup dialog.";
		val487.Items.Add((BaseToolTipItem)(object)val488);
		val487.Items.Add((BaseToolTipItem)(object)val489);
		((BaseToolTipObject)val487).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem9).SuperTip = val487;
		((BarBaseButtonItem)this.printPreviewBarItem11).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem11).Caption = "Scale";
		this.printPreviewBarItem11.Command = (PrintingSystemCommand)60;
		this.printPreviewBarItem11.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem11).Enabled = false;
		((BarItem)this.printPreviewBarItem11).Id = 183;
		((BarItem)this.printPreviewBarItem11).Name = "printPreviewBarItem11";
		val490.FixedTooltipWidth = true;
		((ToolTipItem)val491).Text = "Scale";
		val492.LeftIndent = 6;
		val492.Text = "Stretch or shrink the printed output to a percentage of its actual size.";
		val490.Items.Add((BaseToolTipItem)(object)val491);
		val490.Items.Add((BaseToolTipItem)(object)val492);
		((BaseToolTipObject)val490).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem11).SuperTip = val490;
		((BarBaseButtonItem)this.printPreviewBarItem12).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem12).Caption = "Pointer";
		this.printPreviewBarItem12.Command = (PrintingSystemCommand)3;
		this.printPreviewBarItem12.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem12).Enabled = false;
		((BarBaseButtonItem)this.printPreviewBarItem12).GroupIndex = 1;
		((BarItem)this.printPreviewBarItem12).Id = 184;
		((BarItem)this.printPreviewBarItem12).Name = "printPreviewBarItem12";
		((BarItem)this.printPreviewBarItem12).RibbonStyle = (RibbonItemStyles)4;
		val493.FixedTooltipWidth = true;
		((ToolTipItem)val494).Text = "Mouse Pointer";
		val495.LeftIndent = 6;
		val495.Text = "Show the mouse pointer.";
		val493.Items.Add((BaseToolTipItem)(object)val494);
		val493.Items.Add((BaseToolTipItem)(object)val495);
		((BaseToolTipObject)val493).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem12).SuperTip = val493;
		((BarBaseButtonItem)this.printPreviewBarItem13).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem13).Caption = "Hand Tool";
		this.printPreviewBarItem13.Command = (PrintingSystemCommand)4;
		this.printPreviewBarItem13.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem13).Enabled = false;
		((BarBaseButtonItem)this.printPreviewBarItem13).GroupIndex = 1;
		((BarItem)this.printPreviewBarItem13).Id = 185;
		((BarItem)this.printPreviewBarItem13).Name = "printPreviewBarItem13";
		((BarItem)this.printPreviewBarItem13).RibbonStyle = (RibbonItemStyles)4;
		val496.FixedTooltipWidth = true;
		((ToolTipItem)val497).Text = "Hand Tool";
		val498.LeftIndent = 6;
		val498.Text = "Invoke the Hand tool to manually scroll through pages.";
		val496.Items.Add((BaseToolTipItem)(object)val497);
		val496.Items.Add((BaseToolTipItem)(object)val498);
		((BaseToolTipObject)val496).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem13).SuperTip = val496;
		((BarBaseButtonItem)this.printPreviewBarItem14).ButtonStyle = (BarButtonStyle)2;
		((BarItem)this.printPreviewBarItem14).Caption = "Magnifier";
		this.printPreviewBarItem14.Command = (PrintingSystemCommand)10;
		this.printPreviewBarItem14.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem14).Enabled = false;
		((BarBaseButtonItem)this.printPreviewBarItem14).GroupIndex = 1;
		((BarItem)this.printPreviewBarItem14).Id = 186;
		((BarItem)this.printPreviewBarItem14).Name = "printPreviewBarItem14";
		((BarItem)this.printPreviewBarItem14).RibbonStyle = (RibbonItemStyles)4;
		val499.FixedTooltipWidth = true;
		((ToolTipItem)val500).Text = "Magnifier";
		val501.LeftIndent = 6;
		val501.Text = "Invoke the Magnifier tool.\r\n\r\nClicking once on a document zooms it so that a single page becomes entirely visible, while clicking another time zooms it to 100% of the normal size.";
		val499.Items.Add((BaseToolTipItem)(object)val500);
		val499.Items.Add((BaseToolTipItem)(object)val501);
		((BaseToolTipObject)val499).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem14).SuperTip = val499;
		((BarItem)this.printPreviewBarItem15).Caption = "Zoom Out";
		this.printPreviewBarItem15.Command = (PrintingSystemCommand)12;
		this.printPreviewBarItem15.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem15).Enabled = false;
		((BarItem)this.printPreviewBarItem15).Id = 187;
		((BarItem)this.printPreviewBarItem15).Name = "printPreviewBarItem15";
		val502.FixedTooltipWidth = true;
		((ToolTipItem)val503).Text = "Zoom Out";
		val504.LeftIndent = 6;
		val504.Text = "Zoom out to see more of the page at a reduced size.";
		val502.Items.Add((BaseToolTipItem)(object)val503);
		val502.Items.Add((BaseToolTipItem)(object)val504);
		((BaseToolTipObject)val502).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem15).SuperTip = val502;
		((BarItem)this.printPreviewBarItem16).Caption = "Zoom In";
		this.printPreviewBarItem16.Command = (PrintingSystemCommand)11;
		this.printPreviewBarItem16.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem16).Enabled = false;
		((BarItem)this.printPreviewBarItem16).Id = 188;
		((BarItem)this.printPreviewBarItem16).Name = "printPreviewBarItem16";
		val505.FixedTooltipWidth = true;
		((ToolTipItem)val506).Text = "Zoom In";
		val507.LeftIndent = 6;
		val507.Text = "Zoom in to get a close-up view of the document.";
		val505.Items.Add((BaseToolTipItem)(object)val506);
		val505.Items.Add((BaseToolTipItem)(object)val507);
		((BaseToolTipObject)val505).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem16).SuperTip = val505;
		((BarBaseButtonItem)this.printPreviewBarItem17).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem17).Caption = "Zoom";
		this.printPreviewBarItem17.Command = (PrintingSystemCommand)13;
		this.printPreviewBarItem17.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem17).Enabled = false;
		((BarItem)this.printPreviewBarItem17).Id = 189;
		((BarItem)this.printPreviewBarItem17).Name = "printPreviewBarItem17";
		val508.FixedTooltipWidth = true;
		((ToolTipItem)val509).Text = "Zoom";
		val510.LeftIndent = 6;
		val510.Text = "Change the zoom level of the document preview.";
		val508.Items.Add((BaseToolTipItem)(object)val509);
		val508.Items.Add((BaseToolTipItem)(object)val510);
		((BaseToolTipObject)val508).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem17).SuperTip = val508;
		((BarItem)this.printPreviewBarItem18).Caption = "First Page";
		this.printPreviewBarItem18.Command = (PrintingSystemCommand)16;
		this.printPreviewBarItem18.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem18).Enabled = false;
		((BarItem)this.printPreviewBarItem18).Id = 190;
		((BarItem)this.printPreviewBarItem18).Name = "printPreviewBarItem18";
		val511.FixedTooltipWidth = true;
		((ToolTipItem)val512).Text = "First Page (Home)";
		val513.LeftIndent = 6;
		val513.Text = "Navigate to the first page of the document.";
		val511.Items.Add((BaseToolTipItem)(object)val512);
		val511.Items.Add((BaseToolTipItem)(object)val513);
		((BaseToolTipObject)val511).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem18).SuperTip = val511;
		((BarItem)this.printPreviewBarItem19).Caption = "Previous Page";
		this.printPreviewBarItem19.Command = (PrintingSystemCommand)17;
		this.printPreviewBarItem19.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem19).Enabled = false;
		((BarItem)this.printPreviewBarItem19).Id = 191;
		((BarItem)this.printPreviewBarItem19).Name = "printPreviewBarItem19";
		val514.FixedTooltipWidth = true;
		((ToolTipItem)val515).Text = "Previous Page (Left Arrow)";
		val516.LeftIndent = 6;
		val516.Text = "Navigate to the previous page of the document.";
		val514.Items.Add((BaseToolTipItem)(object)val515);
		val514.Items.Add((BaseToolTipItem)(object)val516);
		((BaseToolTipObject)val514).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem19).SuperTip = val514;
		((BarItem)this.printPreviewBarItem20).Caption = "Next  Page ";
		this.printPreviewBarItem20.Command = (PrintingSystemCommand)18;
		this.printPreviewBarItem20.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem20).Enabled = false;
		((BarItem)this.printPreviewBarItem20).Id = 192;
		((BarItem)this.printPreviewBarItem20).Name = "printPreviewBarItem20";
		val517.FixedTooltipWidth = true;
		((ToolTipItem)val518).Text = "Next Page (Right Arrow)";
		val519.LeftIndent = 6;
		val519.Text = "Navigate to the next page of the document.";
		val517.Items.Add((BaseToolTipItem)(object)val518);
		val517.Items.Add((BaseToolTipItem)(object)val519);
		((BaseToolTipObject)val517).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem20).SuperTip = val517;
		((BarItem)this.printPreviewBarItem21).Caption = "Last  Page ";
		this.printPreviewBarItem21.Command = (PrintingSystemCommand)21;
		this.printPreviewBarItem21.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem21).Enabled = false;
		((BarItem)this.printPreviewBarItem21).Id = 193;
		((BarItem)this.printPreviewBarItem21).Name = "printPreviewBarItem21";
		val520.FixedTooltipWidth = true;
		((ToolTipItem)val521).Text = "Last Page (End)";
		val522.LeftIndent = 6;
		val522.Text = "Navigate to the last page of the document.";
		val520.Items.Add((BaseToolTipItem)(object)val521);
		val520.Items.Add((BaseToolTipItem)(object)val522);
		((BaseToolTipObject)val520).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem21).SuperTip = val520;
		((BarBaseButtonItem)this.printPreviewBarItem22).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem22).Caption = "Many Pages";
		this.printPreviewBarItem22.Command = (PrintingSystemCommand)22;
		this.printPreviewBarItem22.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem22).Enabled = false;
		((BarItem)this.printPreviewBarItem22).Id = 194;
		((BarItem)this.printPreviewBarItem22).Name = "printPreviewBarItem22";
		val523.FixedTooltipWidth = true;
		((ToolTipItem)val524).Text = "View Many Pages";
		val525.LeftIndent = 6;
		val525.Text = "Choose the page layout to arrange the document pages in preview.";
		val523.Items.Add((BaseToolTipItem)(object)val524);
		val523.Items.Add((BaseToolTipItem)(object)val525);
		((BaseToolTipObject)val523).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem22).SuperTip = val523;
		((BarBaseButtonItem)this.printPreviewBarItem23).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem23).Caption = "Page Color";
		this.printPreviewBarItem23.Command = (PrintingSystemCommand)23;
		this.printPreviewBarItem23.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem23).Enabled = false;
		((BarItem)this.printPreviewBarItem23).Id = 195;
		((BarItem)this.printPreviewBarItem23).Name = "printPreviewBarItem23";
		val526.FixedTooltipWidth = true;
		((ToolTipItem)val527).Text = "Background Color";
		val528.LeftIndent = 6;
		val528.Text = "Choose a color for the background of the document pages.";
		val526.Items.Add((BaseToolTipItem)(object)val527);
		val526.Items.Add((BaseToolTipItem)(object)val528);
		((BaseToolTipObject)val526).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem23).SuperTip = val526;
		((BarItem)this.printPreviewBarItem24).Caption = "Watermark";
		this.printPreviewBarItem24.Command = (PrintingSystemCommand)26;
		this.printPreviewBarItem24.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem24).Enabled = false;
		((BarItem)this.printPreviewBarItem24).Id = 196;
		((BarItem)this.printPreviewBarItem24).Name = "printPreviewBarItem24";
		val529.FixedTooltipWidth = true;
		((ToolTipItem)val530).Text = "Watermark";
		val531.LeftIndent = 6;
		val531.Text = "Insert ghosted text or image behind the content of a page.\r\n\r\nThis is often used to indicate that a document is to be treated specially.";
		val529.Items.Add((BaseToolTipItem)(object)val530);
		val529.Items.Add((BaseToolTipItem)(object)val531);
		((BaseToolTipObject)val529).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem24).SuperTip = val529;
		((BarBaseButtonItem)this.printPreviewBarItem25).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem25).Caption = "Export To";
		this.printPreviewBarItem25.Command = (PrintingSystemCommand)27;
		this.printPreviewBarItem25.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem25).Enabled = false;
		((BarItem)this.printPreviewBarItem25).Id = 197;
		((BarItem)this.printPreviewBarItem25).Name = "printPreviewBarItem25";
		val532.FixedTooltipWidth = true;
		((ToolTipItem)val533).Text = "Export To...";
		val534.LeftIndent = 6;
		val534.Text = "Export the current document in one of the available formats, and save it to the file on a disk.";
		val532.Items.Add((BaseToolTipItem)(object)val533);
		val532.Items.Add((BaseToolTipItem)(object)val534);
		((BaseToolTipObject)val532).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem25).SuperTip = val532;
		((BarBaseButtonItem)this.printPreviewBarItem26).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem26).Caption = "E-Mail As";
		this.printPreviewBarItem26.Command = (PrintingSystemCommand)28;
		this.printPreviewBarItem26.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem26).Enabled = false;
		((BarItem)this.printPreviewBarItem26).Id = 198;
		((BarItem)this.printPreviewBarItem26).Name = "printPreviewBarItem26";
		val535.FixedTooltipWidth = true;
		((ToolTipItem)val536).Text = "E-Mail As...";
		val537.LeftIndent = 6;
		val537.Text = "Export the current document in one of the available formats, and attach it to the e-mail.";
		val535.Items.Add((BaseToolTipItem)(object)val536);
		val535.Items.Add((BaseToolTipItem)(object)val537);
		((BaseToolTipObject)val535).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem26).SuperTip = val535;
		((BarItem)this.printPreviewBarItem27).Caption = "Close";
		this.printPreviewBarItem27.Command = (PrintingSystemCommand)25;
		this.printPreviewBarItem27.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem27).Enabled = false;
		((BarItem)this.printPreviewBarItem27).Id = 199;
		((BarItem)this.printPreviewBarItem27).Name = "printPreviewBarItem27";
		val538.FixedTooltipWidth = true;
		((ToolTipItem)val539).Text = "Close Print Preview";
		val540.LeftIndent = 6;
		val540.Text = "Close Print Preview of the document.";
		val538.Items.Add((BaseToolTipItem)(object)val539);
		val538.Items.Add((BaseToolTipItem)(object)val540);
		((BaseToolTipObject)val538).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem27).SuperTip = val538;
		((BarBaseButtonItem)this.printPreviewBarItem28).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem28).Caption = "Orientation";
		this.printPreviewBarItem28.Command = (PrintingSystemCommand)62;
		this.printPreviewBarItem28.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem28).Enabled = false;
		((BarItem)this.printPreviewBarItem28).Id = 200;
		((BarItem)this.printPreviewBarItem28).Name = "printPreviewBarItem28";
		val541.FixedTooltipWidth = true;
		((ToolTipItem)val542).Text = "Page Orientation";
		val543.LeftIndent = 6;
		val543.Text = "Switch the pages between portrait and landscape layouts.";
		val541.Items.Add((BaseToolTipItem)(object)val542);
		val541.Items.Add((BaseToolTipItem)(object)val543);
		((BaseToolTipObject)val541).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem28).SuperTip = val541;
		((BarBaseButtonItem)this.printPreviewBarItem29).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem29).Caption = "Size";
		this.printPreviewBarItem29.Command = (PrintingSystemCommand)61;
		this.printPreviewBarItem29.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem29).Enabled = false;
		((BarItem)this.printPreviewBarItem29).Id = 201;
		((BarItem)this.printPreviewBarItem29).Name = "printPreviewBarItem29";
		val544.FixedTooltipWidth = true;
		((ToolTipItem)val545).Text = "Page Size";
		val546.LeftIndent = 6;
		val546.Text = "Choose the paper size of the document.";
		val544.Items.Add((BaseToolTipItem)(object)val545);
		val544.Items.Add((BaseToolTipItem)(object)val546);
		((BaseToolTipObject)val544).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem29).SuperTip = val544;
		((BarBaseButtonItem)this.printPreviewBarItem30).ButtonStyle = (BarButtonStyle)1;
		((BarItem)this.printPreviewBarItem30).Caption = "Margins";
		this.printPreviewBarItem30.Command = (PrintingSystemCommand)63;
		this.printPreviewBarItem30.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem30).Enabled = false;
		((BarItem)this.printPreviewBarItem30).Id = 202;
		((BarItem)this.printPreviewBarItem30).Name = "printPreviewBarItem30";
		val547.FixedTooltipWidth = true;
		((ToolTipItem)val548).Text = "Page Margins";
		val549.LeftIndent = 6;
		val549.Text = "Select the margin sizes for the entire document.\r\n\r\nTo apply specific margin sizes to the document, click Custom Margins.";
		val547.Items.Add((BaseToolTipItem)(object)val548);
		val547.Items.Add((BaseToolTipItem)(object)val549);
		((BaseToolTipObject)val547).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem30).SuperTip = val547;
		((BarItem)this.printPreviewBarItem31).Caption = "PDF File";
		this.printPreviewBarItem31.Command = (PrintingSystemCommand)41;
		this.printPreviewBarItem31.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem31).Enabled = false;
		((BarItem)this.printPreviewBarItem31).Id = 203;
		((BarItem)this.printPreviewBarItem31).Name = "printPreviewBarItem31";
		val550.FixedTooltipWidth = true;
		((ToolTipItem)val551).Text = "E-Mail As PDF";
		val552.LeftIndent = 6;
		val552.Text = "Export the document to PDF and attach it to the e-mail.";
		val550.Items.Add((BaseToolTipItem)(object)val551);
		val550.Items.Add((BaseToolTipItem)(object)val552);
		((BaseToolTipObject)val550).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem31).SuperTip = val550;
		((BarItem)this.printPreviewBarItem32).Caption = "Text File";
		this.printPreviewBarItem32.Command = (PrintingSystemCommand)42;
		this.printPreviewBarItem32.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem32).Enabled = false;
		((BarItem)this.printPreviewBarItem32).Id = 204;
		((BarItem)this.printPreviewBarItem32).Name = "printPreviewBarItem32";
		val553.FixedTooltipWidth = true;
		((ToolTipItem)val554).Text = "E-Mail As Text";
		val555.LeftIndent = 6;
		val555.Text = "Export the document to Text and attach it to the e-mail.";
		val553.Items.Add((BaseToolTipItem)(object)val554);
		val553.Items.Add((BaseToolTipItem)(object)val555);
		((BaseToolTipObject)val553).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem32).SuperTip = val553;
		((BarItem)this.printPreviewBarItem33).Caption = "CSV File";
		this.printPreviewBarItem33.Command = (PrintingSystemCommand)43;
		this.printPreviewBarItem33.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem33).Enabled = false;
		((BarItem)this.printPreviewBarItem33).Id = 205;
		((BarItem)this.printPreviewBarItem33).Name = "printPreviewBarItem33";
		val556.FixedTooltipWidth = true;
		((ToolTipItem)val557).Text = "E-Mail As CSV";
		val558.LeftIndent = 6;
		val558.Text = "Export the document to CSV and attach it to the e-mail.";
		val556.Items.Add((BaseToolTipItem)(object)val557);
		val556.Items.Add((BaseToolTipItem)(object)val558);
		((BaseToolTipObject)val556).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem33).SuperTip = val556;
		((BarItem)this.printPreviewBarItem34).Caption = "MHT File";
		this.printPreviewBarItem34.Command = (PrintingSystemCommand)44;
		this.printPreviewBarItem34.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem34).Enabled = false;
		((BarItem)this.printPreviewBarItem34).Id = 206;
		((BarItem)this.printPreviewBarItem34).Name = "printPreviewBarItem34";
		val559.FixedTooltipWidth = true;
		((ToolTipItem)val560).Text = "E-Mail As MHT";
		val561.LeftIndent = 6;
		val561.Text = "Export the document to MHT and attach it to the e-mail.";
		val559.Items.Add((BaseToolTipItem)(object)val560);
		val559.Items.Add((BaseToolTipItem)(object)val561);
		((BaseToolTipObject)val559).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem34).SuperTip = val559;
		((BarItem)this.printPreviewBarItem35).Caption = "XLS File";
		this.printPreviewBarItem35.Command = (PrintingSystemCommand)45;
		this.printPreviewBarItem35.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem35).Enabled = false;
		((BarItem)this.printPreviewBarItem35).Id = 207;
		((BarItem)this.printPreviewBarItem35).Name = "printPreviewBarItem35";
		val562.FixedTooltipWidth = true;
		((ToolTipItem)val563).Text = "E-Mail As XLS";
		val564.LeftIndent = 6;
		val564.Text = "Export the document to XLS and attach it to the e-mail.";
		val562.Items.Add((BaseToolTipItem)(object)val563);
		val562.Items.Add((BaseToolTipItem)(object)val564);
		((BaseToolTipObject)val562).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem35).SuperTip = val562;
		((BarItem)this.printPreviewBarItem36).Caption = "XLSX File";
		this.printPreviewBarItem36.Command = (PrintingSystemCommand)46;
		this.printPreviewBarItem36.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem36).Enabled = false;
		((BarItem)this.printPreviewBarItem36).Id = 208;
		((BarItem)this.printPreviewBarItem36).Name = "printPreviewBarItem36";
		val565.FixedTooltipWidth = true;
		((ToolTipItem)val566).Text = "E-Mail As XLSX";
		val567.LeftIndent = 6;
		val567.Text = "Export the document to XLSX and attach it to the e-mail.";
		val565.Items.Add((BaseToolTipItem)(object)val566);
		val565.Items.Add((BaseToolTipItem)(object)val567);
		((BaseToolTipObject)val565).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem36).SuperTip = val565;
		((BarItem)this.printPreviewBarItem37).Caption = "RTF File";
		this.printPreviewBarItem37.Command = (PrintingSystemCommand)47;
		this.printPreviewBarItem37.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem37).Enabled = false;
		((BarItem)this.printPreviewBarItem37).Id = 209;
		((BarItem)this.printPreviewBarItem37).Name = "printPreviewBarItem37";
		val568.FixedTooltipWidth = true;
		((ToolTipItem)val569).Text = "E-Mail As RTF";
		val570.LeftIndent = 6;
		val570.Text = "Export the document to RTF and attach it to the e-mail.";
		val568.Items.Add((BaseToolTipItem)(object)val569);
		val568.Items.Add((BaseToolTipItem)(object)val570);
		((BaseToolTipObject)val568).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem37).SuperTip = val568;
		((BarItem)this.printPreviewBarItem38).Caption = "DOCX File";
		this.printPreviewBarItem38.Command = (PrintingSystemCommand)48;
		this.printPreviewBarItem38.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem38).Enabled = false;
		((BarItem)this.printPreviewBarItem38).Id = 210;
		((BarItem)this.printPreviewBarItem38).Name = "printPreviewBarItem38";
		val571.FixedTooltipWidth = true;
		((ToolTipItem)val572).Text = "E-Mail As DOCX";
		val573.LeftIndent = 6;
		val573.Text = "Export the document to DOCX and attach it to the e-mail.";
		val571.Items.Add((BaseToolTipItem)(object)val572);
		val571.Items.Add((BaseToolTipItem)(object)val573);
		((BaseToolTipObject)val571).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem38).SuperTip = val571;
		((BarItem)this.printPreviewBarItem39).Caption = "Image File";
		this.printPreviewBarItem39.Command = (PrintingSystemCommand)40;
		this.printPreviewBarItem39.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem39).Enabled = false;
		((BarItem)this.printPreviewBarItem39).Id = 211;
		((BarItem)this.printPreviewBarItem39).Name = "printPreviewBarItem39";
		val574.FixedTooltipWidth = true;
		((ToolTipItem)val575).Text = "E-Mail As Image";
		val576.LeftIndent = 6;
		val576.Text = "Export the document to Image and attach it to the e-mail.";
		val574.Items.Add((BaseToolTipItem)(object)val575);
		val574.Items.Add((BaseToolTipItem)(object)val576);
		((BaseToolTipObject)val574).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem39).SuperTip = val574;
		((BarItem)this.printPreviewBarItem40).Caption = "PDF File";
		this.printPreviewBarItem40.Command = (PrintingSystemCommand)30;
		this.printPreviewBarItem40.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem40).Enabled = false;
		((BarItem)this.printPreviewBarItem40).Id = 212;
		((BarItem)this.printPreviewBarItem40).Name = "printPreviewBarItem40";
		val577.FixedTooltipWidth = true;
		((ToolTipItem)val578).Text = "Export to PDF";
		val579.LeftIndent = 6;
		val579.Text = "Export the document to PDF and save it to the file on a disk.";
		val577.Items.Add((BaseToolTipItem)(object)val578);
		val577.Items.Add((BaseToolTipItem)(object)val579);
		((BaseToolTipObject)val577).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem40).SuperTip = val577;
		((BarItem)this.printPreviewBarItem41).Caption = "HTML File";
		this.printPreviewBarItem41.Command = (PrintingSystemCommand)38;
		this.printPreviewBarItem41.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem41).Enabled = false;
		((BarItem)this.printPreviewBarItem41).Id = 213;
		((BarItem)this.printPreviewBarItem41).Name = "printPreviewBarItem41";
		val580.FixedTooltipWidth = true;
		((ToolTipItem)val581).Text = "Export to HTML";
		val582.LeftIndent = 6;
		val582.Text = "Export the document to HTML and save it to the file on a disk.";
		val580.Items.Add((BaseToolTipItem)(object)val581);
		val580.Items.Add((BaseToolTipItem)(object)val582);
		((BaseToolTipObject)val580).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem41).SuperTip = val580;
		((BarItem)this.printPreviewBarItem42).Caption = "Text File";
		this.printPreviewBarItem42.Command = (PrintingSystemCommand)31;
		this.printPreviewBarItem42.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem42).Enabled = false;
		((BarItem)this.printPreviewBarItem42).Id = 214;
		((BarItem)this.printPreviewBarItem42).Name = "printPreviewBarItem42";
		val583.FixedTooltipWidth = true;
		((ToolTipItem)val584).Text = "Export to Text";
		val585.LeftIndent = 6;
		val585.Text = "Export the document to Text and save it to the file on a disk.";
		val583.Items.Add((BaseToolTipItem)(object)val584);
		val583.Items.Add((BaseToolTipItem)(object)val585);
		((BaseToolTipObject)val583).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem42).SuperTip = val583;
		((BarItem)this.printPreviewBarItem43).Caption = "CSV File";
		this.printPreviewBarItem43.Command = (PrintingSystemCommand)32;
		this.printPreviewBarItem43.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem43).Enabled = false;
		((BarItem)this.printPreviewBarItem43).Id = 215;
		((BarItem)this.printPreviewBarItem43).Name = "printPreviewBarItem43";
		val586.FixedTooltipWidth = true;
		((ToolTipItem)val587).Text = "Export to CSV";
		val588.LeftIndent = 6;
		val588.Text = "Export the document to CSV and save it to the file on a disk.";
		val586.Items.Add((BaseToolTipItem)(object)val587);
		val586.Items.Add((BaseToolTipItem)(object)val588);
		((BaseToolTipObject)val586).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem43).SuperTip = val586;
		((BarItem)this.printPreviewBarItem44).Caption = "MHT File";
		this.printPreviewBarItem44.Command = (PrintingSystemCommand)33;
		this.printPreviewBarItem44.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem44).Enabled = false;
		((BarItem)this.printPreviewBarItem44).Id = 216;
		((BarItem)this.printPreviewBarItem44).Name = "printPreviewBarItem44";
		val589.FixedTooltipWidth = true;
		((ToolTipItem)val590).Text = "Export to MHT";
		val591.LeftIndent = 6;
		val591.Text = "Export the document to MHT and save it to the file on a disk.";
		val589.Items.Add((BaseToolTipItem)(object)val590);
		val589.Items.Add((BaseToolTipItem)(object)val591);
		((BaseToolTipObject)val589).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem44).SuperTip = val589;
		((BarItem)this.printPreviewBarItem45).Caption = "XLS File";
		this.printPreviewBarItem45.Command = (PrintingSystemCommand)34;
		this.printPreviewBarItem45.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem45).Enabled = false;
		((BarItem)this.printPreviewBarItem45).Id = 217;
		((BarItem)this.printPreviewBarItem45).Name = "printPreviewBarItem45";
		val592.FixedTooltipWidth = true;
		((ToolTipItem)val593).Text = "Export to XLS";
		val594.LeftIndent = 6;
		val594.Text = "Export the document to XLS and save it to the file on a disk.";
		val592.Items.Add((BaseToolTipItem)(object)val593);
		val592.Items.Add((BaseToolTipItem)(object)val594);
		((BaseToolTipObject)val592).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem45).SuperTip = val592;
		((BarItem)this.printPreviewBarItem46).Caption = "XLSX File";
		this.printPreviewBarItem46.Command = (PrintingSystemCommand)35;
		this.printPreviewBarItem46.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem46).Enabled = false;
		((BarItem)this.printPreviewBarItem46).Id = 218;
		((BarItem)this.printPreviewBarItem46).Name = "printPreviewBarItem46";
		val595.FixedTooltipWidth = true;
		((ToolTipItem)val596).Text = "Export to XLSX";
		val597.LeftIndent = 6;
		val597.Text = "Export the document to XLSX and save it to the file on a disk.";
		val595.Items.Add((BaseToolTipItem)(object)val596);
		val595.Items.Add((BaseToolTipItem)(object)val597);
		((BaseToolTipObject)val595).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem46).SuperTip = val595;
		((BarItem)this.printPreviewBarItem47).Caption = "RTF File";
		this.printPreviewBarItem47.Command = (PrintingSystemCommand)36;
		this.printPreviewBarItem47.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem47).Enabled = false;
		((BarItem)this.printPreviewBarItem47).Id = 219;
		((BarItem)this.printPreviewBarItem47).Name = "printPreviewBarItem47";
		val598.FixedTooltipWidth = true;
		((ToolTipItem)val599).Text = "Export to RTF";
		val600.LeftIndent = 6;
		val600.Text = "Export the document to RTF and save it to the file on a disk.";
		val598.Items.Add((BaseToolTipItem)(object)val599);
		val598.Items.Add((BaseToolTipItem)(object)val600);
		((BaseToolTipObject)val598).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem47).SuperTip = val598;
		((BarItem)this.printPreviewBarItem48).Caption = "DOCX File";
		this.printPreviewBarItem48.Command = (PrintingSystemCommand)37;
		this.printPreviewBarItem48.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem48).Enabled = false;
		((BarItem)this.printPreviewBarItem48).Id = 220;
		((BarItem)this.printPreviewBarItem48).Name = "printPreviewBarItem48";
		val601.FixedTooltipWidth = true;
		((ToolTipItem)val602).Text = "Export to DOCX";
		val603.LeftIndent = 6;
		val603.Text = "Export the document to DOCX and save it to the file on a disk.";
		val601.Items.Add((BaseToolTipItem)(object)val602);
		val601.Items.Add((BaseToolTipItem)(object)val603);
		((BaseToolTipObject)val601).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem48).SuperTip = val601;
		((BarItem)this.printPreviewBarItem49).Caption = "Image File";
		this.printPreviewBarItem49.Command = (PrintingSystemCommand)29;
		this.printPreviewBarItem49.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem49).Enabled = false;
		((BarItem)this.printPreviewBarItem49).Id = 221;
		((BarItem)this.printPreviewBarItem49).Name = "printPreviewBarItem49";
		val604.FixedTooltipWidth = true;
		((ToolTipItem)val605).Text = "Export to Image";
		val606.LeftIndent = 6;
		val606.Text = "Export the document to Image and save it to the file on a disk.";
		val604.Items.Add((BaseToolTipItem)(object)val605);
		val604.Items.Add((BaseToolTipItem)(object)val606);
		((BaseToolTipObject)val604).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem49).SuperTip = val604;
		((BarItem)this.printPreviewBarItem50).Caption = "Open";
		this.printPreviewBarItem50.Command = (PrintingSystemCommand)64;
		this.printPreviewBarItem50.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem50).Enabled = false;
		((BarItem)this.printPreviewBarItem50).Id = 222;
		((BarItem)this.printPreviewBarItem50).Name = "printPreviewBarItem50";
		val607.FixedTooltipWidth = true;
		((ToolTipItem)val608).Text = "Open (Ctrl + O)";
		val609.LeftIndent = 6;
		val609.Text = "Open a document.";
		val607.Items.Add((BaseToolTipItem)(object)val608);
		val607.Items.Add((BaseToolTipItem)(object)val609);
		((BaseToolTipObject)val607).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem50).SuperTip = val607;
		((BarItem)this.printPreviewBarItem51).Caption = "Save";
		this.printPreviewBarItem51.Command = (PrintingSystemCommand)65;
		this.printPreviewBarItem51.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItem)this.printPreviewBarItem51).Enabled = false;
		((BarItem)this.printPreviewBarItem51).Id = 223;
		((BarItem)this.printPreviewBarItem51).Name = "printPreviewBarItem51";
		val610.FixedTooltipWidth = true;
		((ToolTipItem)val611).Text = "Save (Ctrl + S)";
		val612.LeftIndent = 6;
		val612.Text = "Save the document.";
		val610.Items.Add((BaseToolTipItem)(object)val611);
		val610.Items.Add((BaseToolTipItem)(object)val612);
		((BaseToolTipObject)val610).MaxWidth = 210;
		((BarItem)this.printPreviewBarItem51).SuperTip = val610;
		((BarItem)this.printPreviewStaticItem1).Caption = "Nothing";
		((BarItem)this.printPreviewStaticItem1).Id = 0;
		((BarStaticItem)this.printPreviewStaticItem1).LeftIndent = 1;
		((BarItem)this.printPreviewStaticItem1).Name = "printPreviewStaticItem1";
		((BarStaticItem)this.printPreviewStaticItem1).RightIndent = 1;
		this.printPreviewStaticItem1.Type = "PageOfPages";
		((RepositoryItem)this.repositoryItemProgressBar1).Name = "repositoryItemProgressBar1";
		((BarEditItem)this.progressBarEditItem1).Edit = (RepositoryItem)(object)this.repositoryItemProgressBar1;
		((BarEditItem)this.progressBarEditItem1).EditHeight = 12;
		((BarEditItem)this.progressBarEditItem1).EditWidth = 150;
		((BarItem)this.progressBarEditItem1).Id = 1;
		((BarItem)this.progressBarEditItem1).Name = "progressBarEditItem1";
		((BarItem)this.progressBarEditItem1).Visibility = (BarItemVisibility)1;
		((BarItem)this.printPreviewBarItem52).Caption = "Stop";
		this.printPreviewBarItem52.Command = (PrintingSystemCommand)66;
		((BarItem)this.printPreviewBarItem52).Enabled = false;
		((BarItem)this.printPreviewBarItem52).Hint = "Stop";
		((BarItem)this.printPreviewBarItem52).Id = 2;
		((BarItem)this.printPreviewBarItem52).Name = "printPreviewBarItem52";
		((BarItem)this.printPreviewBarItem52).Visibility = (BarItemVisibility)1;
		((BarItem)this.commandBarItem129).Alignment = (BarItemLinkAlignment)2;
		this.commandBarItem129.Command = (ReportCommand)55;
		((BarItem)this.commandBarItem129).Enabled = false;
		((BarItem)this.commandBarItem129).Id = 3;
		((BarItem)this.commandBarItem129).Name = "commandBarItem129";
		((BarItem)this.commandBarItem129).PaintStyle = (BarItemPaintStyle)3;
		((BarItem)this.printPreviewStaticItem2).Alignment = (BarItemLinkAlignment)2;
		((BarStaticItem)this.printPreviewStaticItem2).AutoSize = (BarStaticItemSize)0;
		((BarItem)this.printPreviewStaticItem2).Caption = "100%";
		((BarItem)this.printPreviewStaticItem2).Id = 4;
		((BarItem)this.printPreviewStaticItem2).Name = "printPreviewStaticItem2";
		this.printPreviewStaticItem2.Type = "ZoomFactorText";
		((RepositoryItemTrackBar)this.repositoryItemZoomTrackBar1).Alignment = (VertAlignment)2;
		((RepositoryItem)this.repositoryItemZoomTrackBar1).AllowFocused = false;
		((RepositoryItem)this.repositoryItemZoomTrackBar1).BorderStyle = (BorderStyles)0;
		((RepositoryItemTrackBar)this.repositoryItemZoomTrackBar1).Maximum = 180;
		((RepositoryItem)this.repositoryItemZoomTrackBar1).Name = "repositoryItemZoomTrackBar1";
		((BarItem)this.zoomTrackBarEditItem1).Alignment = (BarItemLinkAlignment)2;
		((BarEditItem)this.zoomTrackBarEditItem1).Edit = (RepositoryItem)(object)this.repositoryItemZoomTrackBar1;
		((BarEditItem)this.zoomTrackBarEditItem1).EditWidth = 140;
		((BarItem)this.zoomTrackBarEditItem1).Enabled = false;
		((BarItem)this.zoomTrackBarEditItem1).Id = 5;
		((BarItem)this.zoomTrackBarEditItem1).Name = "zoomTrackBarEditItem1";
		this.zoomTrackBarEditItem1.Range = new int[2] { 10, 500 };
		this.ribbonPage16.ContextSpecifier = this.xrDesignRibbonController1;
		((RibbonPage)this.ribbonPage16).Groups.AddRange((RibbonPageGroup[])(object)new RibbonPageGroup[8]
		{
			(RibbonPageGroup)this.printPreviewRibbonPageGroup1,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup2,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup3,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup4,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup5,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup6,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup7,
			(RibbonPageGroup)this.printPreviewRibbonPageGroup8
		});
		((RibbonPage)this.ribbonPage16).Name = "ribbonPage16";
		((RibbonPage)this.ribbonPage16).Text = "Home";
		((RibbonPage)this.ribbonPage16).Visible = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup1).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup1).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup1.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup1).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem50);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup1).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem51);
		this.printPreviewRibbonPageGroup1.Kind = (PrintPreviewRibbonPageGroupKind)6;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup1).Name = "printPreviewRibbonPageGroup1";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup1).Text = "Document";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup2).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup2).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup2.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup2).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem7);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup2).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem8);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup2).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem3);
		this.printPreviewRibbonPageGroup2.Kind = (PrintPreviewRibbonPageGroupKind)0;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup2).Name = "printPreviewRibbonPageGroup2";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup2).Text = "Print";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup3).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup3).CaptionButtonVisible = (DefaultBoolean)0;
		this.printPreviewRibbonPageGroup3.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem11);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem30);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem28);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup3).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem29);
		this.printPreviewRibbonPageGroup3.Kind = (PrintPreviewRibbonPageGroupKind)1;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup3).Name = "printPreviewRibbonPageGroup3";
		val613.FixedTooltipWidth = true;
		((ToolTipItem)val614).Text = "Page Setup";
		val615.LeftIndent = 6;
		val615.Text = "Show the Page Setup dialog.";
		val613.Items.Add((BaseToolTipItem)(object)val614);
		val613.Items.Add((BaseToolTipItem)(object)val615);
		((BaseToolTipObject)val613).MaxWidth = 210;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup3).SuperTip = val613;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup3).Text = "Page Setup";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup4).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup4).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup4.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem4);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem5);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem2);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem1);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem18, true);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem19);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem20);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup4).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem21);
		this.printPreviewRibbonPageGroup4.Kind = (PrintPreviewRibbonPageGroupKind)2;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup4).Name = "printPreviewRibbonPageGroup4";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup4).Text = "Navigation";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup5).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup5).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup5.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem12);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem13);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem14);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem22);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem15);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem17);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup5).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem16);
		this.printPreviewRibbonPageGroup5.Kind = (PrintPreviewRibbonPageGroupKind)3;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup5).Name = "printPreviewRibbonPageGroup5";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup5).Text = "Zoom";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup6).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup6).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup6.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup6).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem23);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup6).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem24);
		this.printPreviewRibbonPageGroup6.Kind = (PrintPreviewRibbonPageGroupKind)4;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup6).Name = "printPreviewRibbonPageGroup6";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup6).Text = "Page Background";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup7).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup7).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup7.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup7).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem25);
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup7).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem26);
		this.printPreviewRibbonPageGroup7.Kind = (PrintPreviewRibbonPageGroupKind)5;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup7).Name = "printPreviewRibbonPageGroup7";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup7).Text = "Export";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup8).AllowTextClipping = false;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup8).CaptionButtonVisible = (DefaultBoolean)1;
		this.printPreviewRibbonPageGroup8.ContextSpecifier = this.xrDesignRibbonController1;
		((BarItemLinkCollection)((RibbonPageGroup)this.printPreviewRibbonPageGroup8).ItemLinks).Add((BarItem)(object)this.printPreviewBarItem27);
		this.printPreviewRibbonPageGroup8.Kind = (PrintPreviewRibbonPageGroupKind)7;
		((RibbonPageGroup)this.printPreviewRibbonPageGroup8).Name = "printPreviewRibbonPageGroup8";
		((RibbonPageGroup)this.printPreviewRibbonPageGroup8).Text = "Close";
		((DockManager)this.xrDesignDockManager1).Form = this;
		this.xrDesignDockManager1.ImageStream = (ImageCollectionStreamer)resources.GetObject("xrDesignDockManager1.ImageStream");
		((DockManager)this.xrDesignDockManager1).RootPanels.AddRange((DockPanel[])(object)new DockPanel[2] { this.panelContainer1, this.panelContainer4 });
		((DockManager)this.xrDesignDockManager1).TopZIndexControls.AddRange(new string[13]
		{
			"DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "System.Windows.Forms.StatusBar", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "DevExpress.XtraBars.Ribbon.RibbonControl", "DevExpress.XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Navigation.TileNavPane", "DevExpress.XtraBars.TabFormControl",
			"DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl", "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl", "DevExpress.XtraReports.UserDesigner.XRToolBoxPanel"
		});
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1).Controls.Add((System.Windows.Forms.Control)(object)this.fieldListDockPanel1_Container);
		((DockPanel)this.fieldListDockPanel1).Dock = (DockingStyle)5;
		((DockPanel)this.fieldListDockPanel1).ID = new System.Guid("faf69838-a93f-4114-83e8-d0d09cc5ce95");
		((DockPanel)this.fieldListDockPanel1).Location = new System.Drawing.Point(1, 24);
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1).Name = "fieldListDockPanel1";
		((DockPanel)this.fieldListDockPanel1).OriginalSize = new System.Drawing.Size(200, 200);
		((DockPanel)this.fieldListDockPanel1).Size = new System.Drawing.Size(413, 84);
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1).Text = "Field List";
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1_Container).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1_Container).Name = "fieldListDockPanel1_Container";
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1_Container).Size = new System.Drawing.Size(413, 84);
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1_Container).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1).Controls.Add((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1_Container);
		((DockPanel)this.propertyGridDockPanel1).Dock = (DockingStyle)5;
		((DockPanel)this.propertyGridDockPanel1).ID = new System.Guid("b38d12c3-cd06-4dec-b93d-63a0088e495a");
		((DockPanel)this.propertyGridDockPanel1).Location = new System.Drawing.Point(1, 25);
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1).Name = "propertyGridDockPanel1";
		((DockPanel)this.propertyGridDockPanel1).OriginalSize = new System.Drawing.Size(200, 200);
		((DockPanel)this.propertyGridDockPanel1).Size = new System.Drawing.Size(413, 83);
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1).Text = "Properties";
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1_Container).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1_Container).Name = "propertyGridDockPanel1_Container";
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1_Container).Size = new System.Drawing.Size(413, 83);
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1_Container).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1).Controls.Add((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1_Container);
		((DockPanel)this.reportExplorerDockPanel1).Dock = (DockingStyle)5;
		((DockPanel)this.reportExplorerDockPanel1).ID = new System.Guid("fb3ec6cc-3b9b-4b9c-91cf-cff78c1edbf1");
		((DockPanel)this.reportExplorerDockPanel1).Location = new System.Drawing.Point(1, 24);
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1).Name = "reportExplorerDockPanel1";
		((DockPanel)this.reportExplorerDockPanel1).OriginalSize = new System.Drawing.Size(200, 200);
		((DockPanel)this.reportExplorerDockPanel1).Size = new System.Drawing.Size(413, 84);
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1).Text = "Report Explorer";
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1_Container).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1_Container).Name = "reportExplorerDockPanel1_Container";
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1_Container).Size = new System.Drawing.Size(413, 84);
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1_Container).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1).Controls.Add((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1_Container);
		((DockPanel)this.reportGalleryDockPanel1).Dock = (DockingStyle)5;
		((DockPanel)this.reportGalleryDockPanel1).ID = new System.Guid("7cd5b1e8-63bb-46f7-af65-af61eb851a38");
		((DockPanel)this.reportGalleryDockPanel1).Location = new System.Drawing.Point(1, 25);
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1).Name = "reportGalleryDockPanel1";
		((DockPanel)this.reportGalleryDockPanel1).OriginalSize = new System.Drawing.Size(200, 200);
		((DockPanel)this.reportGalleryDockPanel1).Size = new System.Drawing.Size(413, 83);
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1).Text = "Report Gallery";
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1_Container).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1_Container).Name = "reportGalleryDockPanel1_Container";
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1_Container).Size = new System.Drawing.Size(413, 83);
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1_Container).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1).Controls.Add((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1_Container);
		((DockPanel)this.groupAndSortDockPanel1).Dock = (DockingStyle)5;
		((DockPanel)this.groupAndSortDockPanel1).ID = new System.Guid("4bab159e-c495-4d67-87dc-f4e895da443e");
		((DockPanel)this.groupAndSortDockPanel1).Location = new System.Drawing.Point(0, 25);
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1).Name = "groupAndSortDockPanel1";
		((DockPanel)this.groupAndSortDockPanel1).OriginalSize = new System.Drawing.Size(200, 200);
		((DockPanel)this.groupAndSortDockPanel1).Size = new System.Drawing.Size(570, 167);
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1).Text = "Group and Sort";
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1_Container).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1_Container).Name = "groupAndSortDockPanel1_Container";
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1_Container).Size = new System.Drawing.Size(570, 167);
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1_Container).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1).Controls.Add((System.Windows.Forms.Control)(object)this.errorListDockPanel1_Container);
		((DockPanel)this.errorListDockPanel1).Dock = (DockingStyle)5;
		((DockPanel)this.errorListDockPanel1).ID = new System.Guid("5a9a01fd-6e95-4e81-a8c4-ac63153d7488");
		((DockPanel)this.errorListDockPanel1).Location = new System.Drawing.Point(0, 25);
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1).Name = "errorListDockPanel1";
		((DockPanel)this.errorListDockPanel1).OriginalSize = new System.Drawing.Size(200, 74);
		((DockPanel)this.errorListDockPanel1).Size = new System.Drawing.Size(570, 167);
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1).Text = "Report Design Analyzer";
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1_Container).Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1_Container).Name = "errorListDockPanel1_Container";
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1_Container).Size = new System.Drawing.Size(570, 167);
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1_Container).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.panelContainer1).Controls.Add((System.Windows.Forms.Control)(object)this.panelContainer2);
		((System.Windows.Forms.Control)(object)this.panelContainer1).Controls.Add((System.Windows.Forms.Control)(object)this.panelContainer3);
		this.panelContainer1.Dock = (DockingStyle)4;
		this.panelContainer1.ID = new System.Guid("2bd6fd14-a080-4783-a8ba-8baad05ecf70");
		this.panelContainer1.Location = new System.Drawing.Point(570, 158);
		((System.Windows.Forms.Control)(object)this.panelContainer1).Name = "panelContainer1";
		this.panelContainer1.OriginalSize = new System.Drawing.Size(414, 200);
		this.panelContainer1.Size = new System.Drawing.Size(414, 274);
		((System.Windows.Forms.Control)(object)this.panelContainer1).Text = "panelContainer1";
		this.panelContainer2.ActiveChild = (DockPanel)(object)this.reportExplorerDockPanel1;
		((System.Windows.Forms.Control)(object)this.panelContainer2).Controls.Add((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1);
		((System.Windows.Forms.Control)(object)this.panelContainer2).Controls.Add((System.Windows.Forms.Control)(object)this.fieldListDockPanel1);
		this.panelContainer2.Dock = (DockingStyle)5;
		this.panelContainer2.ID = new System.Guid("bdee23f7-5103-4014-bde5-e42238aed51a");
		this.panelContainer2.Location = new System.Drawing.Point(0, 0);
		((System.Windows.Forms.Control)(object)this.panelContainer2).Name = "panelContainer2";
		this.panelContainer2.OriginalSize = new System.Drawing.Size(200, 200);
		this.panelContainer2.Size = new System.Drawing.Size(414, 137);
		this.panelContainer2.Tabbed = true;
		((System.Windows.Forms.Control)(object)this.panelContainer2).Text = "panelContainer2";
		this.panelContainer3.ActiveChild = (DockPanel)(object)this.propertyGridDockPanel1;
		((System.Windows.Forms.Control)(object)this.panelContainer3).Controls.Add((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1);
		((System.Windows.Forms.Control)(object)this.panelContainer3).Controls.Add((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1);
		this.panelContainer3.Dock = (DockingStyle)5;
		this.panelContainer3.ID = new System.Guid("db71f0b1-c2c0-43b3-a844-e958f44dd745");
		this.panelContainer3.Location = new System.Drawing.Point(0, 137);
		((System.Windows.Forms.Control)(object)this.panelContainer3).Name = "panelContainer3";
		this.panelContainer3.OriginalSize = new System.Drawing.Size(200, 200);
		this.panelContainer3.Size = new System.Drawing.Size(414, 137);
		this.panelContainer3.Tabbed = true;
		((System.Windows.Forms.Control)(object)this.panelContainer3).Text = "panelContainer3";
		this.panelContainer4.ActiveChild = (DockPanel)(object)this.groupAndSortDockPanel1;
		((System.Windows.Forms.Control)(object)this.panelContainer4).Controls.Add((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1);
		((System.Windows.Forms.Control)(object)this.panelContainer4).Controls.Add((System.Windows.Forms.Control)(object)this.errorListDockPanel1);
		this.panelContainer4.Dock = (DockingStyle)2;
		this.panelContainer4.ID = new System.Guid("117f9b36-b9b5-4151-8ff4-ee68e7caad81");
		this.panelContainer4.Location = new System.Drawing.Point(0, 211);
		((System.Windows.Forms.Control)(object)this.panelContainer4).Name = "panelContainer4";
		this.panelContainer4.OriginalSize = new System.Drawing.Size(200, 221);
		this.panelContainer4.Size = new System.Drawing.Size(570, 221);
		this.panelContainer4.Tabbed = true;
		((System.Windows.Forms.Control)(object)this.panelContainer4).Text = "panelContainer4";
		base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(984, 460);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.groupControl1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.calendarControl1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pdfViewer1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.labelControl1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.calcEdit1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.barCodeControl1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.checkButton1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.textEdit1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pivotGridControl1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dashboardDesigner1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.panelContainer4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.panelContainer1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ribbonStatusBar1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ribbonControl1);
		base.Name = "frmTestDev";
		this.Text = "frmTestDev";
		((System.ComponentModel.ISupportInitialize)this.dashboardDesigner1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pivotGridControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.textEdit1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.calcEdit1.Properties).EndInit();
		((System.ComponentModel.ISupportInitialize)((CalendarControlBase)this.calendarControl1).CalendarTimeProperties).EndInit();
		((System.ComponentModel.ISupportInitialize)this.groupControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.popupMenu1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.ribbonControl1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.reportDesigner1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.xrDesignRibbonController1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.recentlyUsedItemsComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.designRepositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemSpinEdit4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemImageComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemLookUpEdit2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemComboBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.applicationMenu1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemProgressBar1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.repositoryItemZoomTrackBar1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.xrDesignDockManager1).EndInit();
		((System.Windows.Forms.Control)(object)this.fieldListDockPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.propertyGridDockPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.reportExplorerDockPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.reportGalleryDockPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.groupAndSortDockPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.errorListDockPanel1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.panelContainer1).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.panelContainer2).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.panelContainer3).ResumeLayout(false);
		((System.Windows.Forms.Control)(object)this.panelContainer4).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
