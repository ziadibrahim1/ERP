using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ClinicScheduleControl;

public class ClinicScheduleUserControl : UserControl
{
	public class MyEventArgs : EventArgs
	{
		private readonly int _ClinicScheduleID;

		public int ClinicScheduleID => _ClinicScheduleID;

		public MyEventArgs(int ClinicScheduleID)
		{
			_ClinicScheduleID = ClinicScheduleID;
		}
	}

	private Pen pen;

	private StringFormat format;

	private string _DateTimeMaskInput;

	private int _MinStartTime;

	private int _MaxEndTime;

	private DataTable _dtClinicSchedule;

	private Color _BlockedColor;

	private Color _AvailableColor;

	private DateTime _FromDate;

	private IContainer components = null;

	private UltraLabel lblRuler;

	private UltraButton btnPrevious;

	private UltraButton btnNext;

	private ToolTip toolTip1;

	private UltraLabel lblToDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel lblFromDate;

	private UltraDateTimeEditor dtpFromDate;

	private UltraPanel pnlClinicSchedule;

	public UltraLabel lblBlockedName;

	public UltraLabel lblBlocked;

	public UltraLabel lblAvailableName;

	public UltraLabel lblAvailable;

	public Color BlockedColor
	{
		get
		{
			return _BlockedColor;
		}
		set
		{
			_BlockedColor = value;
			((ControlBase)lblBlocked).Appearance.BackColor = _BlockedColor;
		}
	}

	public Color AvailableColor
	{
		get
		{
			return _AvailableColor;
		}
		set
		{
			_AvailableColor = value;
			((ControlBase)lblAvailable).Appearance.BackColor = _AvailableColor;
		}
	}

	public string DateTimeMaskInput
	{
		get
		{
			return _DateTimeMaskInput;
		}
		set
		{
			_DateTimeMaskInput = value;
		}
	}

	public int MinStartTime
	{
		get
		{
			return _MinStartTime;
		}
		set
		{
			_MinStartTime = value;
		}
	}

	public int MaxEndTime
	{
		get
		{
			return _MaxEndTime;
		}
		set
		{
			_MaxEndTime = value;
		}
	}

	public DataTable ClinicSchedule
	{
		get
		{
			return _dtClinicSchedule;
		}
		set
		{
			_dtClinicSchedule = value;
		}
	}

	public DateTime FromDate
	{
		get
		{
			return _FromDate;
		}
		set
		{
			_FromDate = value;
		}
	}

	public event EventHandler<MyEventArgs> btnDoctorPeriod;

	public event EventHandler btnNextClick;

	public event EventHandler btnPreviousClick;

	public ClinicScheduleUserControl()
	{
		InitializeComponent();
		pen = new Pen(Color.Black, float.Epsilon);
		format = new StringFormat(StringFormat.GenericTypographic);
		format.FormatFlags = StringFormatFlags.NoWrap;
		format.Trimming = StringTrimming.Character;
	}

	private void lblRuler_Paint(object sender, PaintEventArgs e)
	{
		if (_MaxEndTime - _MinStartTime <= 0)
		{
			return;
		}
		Graphics graphics = e.Graphics;
		string text = "";
		string s = "";
		graphics.PageUnit = GraphicsUnit.Millimeter;
		graphics.PageScale = 1f;
		PointF[] array = new PointF[4]
		{
			new PointF(2f, 2f),
			new PointF(5f, 5f),
			new Point(((Control)(object)lblRuler).Size),
			((Control)(object)lblRuler).Location
		};
		graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, array);
		float num = array[1].Y;
		float num2 = array[2].X;
		int num3 = _MinStartTime;
		int maxEndTime = _MaxEndTime;
		float num4 = num2 / (float)(_MaxEndTime - _MinStartTime);
		for (float num5 = 0f; num5 < num2; num5 += num4)
		{
			int num6 = 0;
			for (float num7 = 0f; num7 < num4; num7 += float.Parse(num4.ToString()) / 4f)
			{
				if (num6 == 2)
				{
					graphics.DrawLine(pen, num5 + 2f + num7, 0f, num5 + 2f + num7, 2f * num);
					if (num3 - 12 > 0)
					{
					}
					SizeF sizeF = graphics.MeasureString(s, Font, ((Control)(object)lblRuler).Size, format);
					graphics.DrawString(s, Font, Brushes.Black, num5 + 2f + num7 - sizeF.Width / 2f, 2f * num, format);
				}
				else
				{
					graphics.DrawLine(pen, num5 + 2f + num7, 0f, num5 + 2f + num7, 1f * num);
				}
				num6++;
			}
			graphics.DrawLine(pen, num5 + 2f, 0f, num5 + 2f, 3f * num);
			text = num3.ToString().ToString(CultureInfo.InvariantCulture);
			SizeF sizeF2 = graphics.MeasureString(text, Font, ((Control)(object)lblRuler).Size, format);
			graphics.DrawString(text, Font, Brushes.Black, num5 + 4f - sizeF2.Width / 2f, 3f * num, format);
			num3++;
		}
	}

	public void CreateSchedule()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Expected O, but got Unknown
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Expected O, but got Unknown
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Expected O, but got Unknown
		int num = 27;
		int num2 = 5;
		int num3 = 0;
		int num4 = 0;
		int num5 = 90;
		int num6 = 35;
		int num7 = 90;
		DateTime fromDate = _FromDate;
		DataView dataView = new DataView(_dtClinicSchedule);
		string text = "";
		dtpFromDate.DateTime = fromDate;
		dtpToDate.DateTime = fromDate.AddDays(6.0);
		((Control)(object)pnlClinicSchedule).Visible = false;
		((Control)(object)pnlClinicSchedule.ClientArea).Controls.Clear();
		for (int i = 0; i < 7; i++)
		{
			text = "";
			fromDate = _FromDate.AddDays(i);
			UltraDateTimeEditor val = new UltraDateTimeEditor();
			((Control)(object)val).Left = num2;
			((Control)(object)val).Top = num4 + num2;
			((Control)(object)val).Height = num;
			((Control)(object)val).Width = num5;
			((Control)(object)val).Name = "dtp-" + i;
			val.DateTime = fromDate;
			val.MaskInput = _DateTimeMaskInput;
			val.SpinButtonDisplayStyle = (ButtonDisplayStyle)0;
			((EditorButtonControlBase)val).ReadOnly = true;
			((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val);
			num3 += num2 + num5;
			UltraLabel val2 = new UltraLabel();
			((Control)(object)val2).Left = num3 + num2;
			((Control)(object)val2).Top = num4 + num2;
			((Control)(object)val2).Height = num;
			((Control)(object)val2).Width = num6;
			switch (i)
			{
			case 0:
				((Control)(object)val2).Name = "Sat";
				((Control)(object)val2).Text = "Sat";
				break;
			case 1:
				((Control)(object)val2).Name = "Sun";
				((Control)(object)val2).Text = "Sun";
				break;
			case 2:
				((Control)(object)val2).Name = "Mon";
				((Control)(object)val2).Text = "Mon";
				break;
			case 3:
				((Control)(object)val2).Name = "Tue";
				((Control)(object)val2).Text = "Tue";
				break;
			case 4:
				((Control)(object)val2).Name = "Wed";
				((Control)(object)val2).Text = "Wed";
				break;
			case 5:
				((Control)(object)val2).Name = "Thu";
				((Control)(object)val2).Text = "Thu";
				break;
			case 6:
				((Control)(object)val2).Name = "Fri";
				((Control)(object)val2).Text = "Fri";
				break;
			}
			num3 += num2 + num6;
			((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val2);
			dataView.RowFilter = " (FromDate >= #" + fromDate.ToString("MM/dd/yyyy") + "# And FromDate < #" + DateTime.Parse(fromDate.ToShortDateString()).AddDays(1.0).ToString("MM/dd/yyyy") + "# ) ";
			dataView.Sort = "DoctorID ASC";
			((Control)(object)lblRuler).Location = new Point(num3 + num7 + num2, ((Control)(object)lblRuler).Location.Y);
			((Control)(object)lblRuler).Size = new Size(((Control)(object)pnlClinicSchedule).Width - (num3 + num7 + num2), ((Control)(object)lblRuler).Height);
			decimal num8 = (_MaxEndTime - _MinStartTime) * 60;
			decimal num9 = (decimal)((Control)(object)lblRuler).Width / num8;
			int num10 = 0;
			for (int j = 0; j < dataView.Count; j++)
			{
				num10 = 0;
				if (text != dataView[j]["DoctorName"].ToString())
				{
					text = dataView[j]["DoctorName"].ToString();
					UltraLabel val3 = new UltraLabel();
					((Control)(object)val3).Left = num3;
					((Control)(object)val3).Top = num4 + num2;
					((Control)(object)val3).Width = num7;
					((Control)(object)val3).Height = num;
					((Control)(object)val3).Text = text;
					((Control)(object)val3).Name = dataView[j]["ClinicScheduleID"].ToString();
					((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val3);
					num10++;
				}
				UltraButton val4 = new UltraButton();
				((UltraControlBase)val4).UseAppStyling = false;
				((ControlBase)val4).Appearance.ThemedElementAlpha = (Alpha)3;
				decimal num11 = DateTime.Parse(dataView[j]["FromDate"].ToString()).Hour * 60 + DateTime.Parse(dataView[j]["FromDate"].ToString()).Minute;
				decimal num12 = Math.Abs((decimal)(DateTime.Parse(dataView[j]["ToDate"].ToString()) - DateTime.Parse(dataView[j]["FromDate"].ToString())).TotalMinutes);
				((Control)(object)val4).Left = (int)(num11 * num9 + (decimal)((Control)(object)lblRuler).Left);
				((Control)(object)val4).Top = num4 + num2;
				((Control)(object)val4).Width = (int)(num12 * num9);
				((Control)(object)val4).Height = num - 5;
				((Control)(object)val4).Text = DateTime.Parse(dataView[j]["FromDate"].ToString()).ToString("HH:mm") + " - " + DateTime.Parse(dataView[j]["ToDate"].ToString()).ToString("HH:mm");
				((Control)(object)val4).Name = dataView[j]["ClinicScheduleID"].ToString();
				if (bool.Parse(dataView[j]["IsBlocked"].ToString()))
				{
					((ControlBase)val4).Appearance.BackColor = _BlockedColor;
				}
				else
				{
					((ControlBase)val4).Appearance.BackColor = _AvailableColor;
				}
				((Control)(object)val4).Click += btnDoctorPeriod_Click;
				((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val4);
				if (j < dataView.Count - 1 && dataView[j]["DoctorName"].ToString() != dataView[j + 1]["DoctorName"].ToString())
				{
					num4 += num;
				}
			}
			num4 += num;
			UltraLabel val5 = new UltraLabel();
			((Control)(object)val5).AutoSize = false;
			((Control)(object)val5).Width = ((Control)(object)pnlClinicSchedule).Width - num2;
			((Control)(object)val5).Top = num4 + num2;
			((Control)(object)val5).Height = 1;
			val5.BorderStyleInner = (UIElementBorderStyle)4;
			((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val5);
			num3 = 0;
		}
		((Control)(object)pnlClinicSchedule).Visible = true;
	}

	protected virtual void btnDoctorPeriod_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		int clinicScheduleID = int.Parse(((Control)(UltraButton)sender).Name.ToString());
		if (this.btnDoctorPeriod != null)
		{
			this.btnDoctorPeriod(this, new MyEventArgs(clinicScheduleID));
		}
	}

	protected virtual void btnNext_Click(object sender, EventArgs e)
	{
		if (this.btnNextClick != null)
		{
			this.btnNextClick(this, e);
		}
	}

	protected virtual void btnPrevious_Click(object sender, EventArgs e)
	{
		if (this.btnPreviousClick != null)
		{
			this.btnPreviousClick(this, e);
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Expected O, but got Unknown
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Expected O, but got Unknown
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		this.lblRuler = new UltraLabel();
		this.btnPrevious = new UltraButton();
		this.btnNext = new UltraButton();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblFromDate = new UltraLabel();
		this.lblToDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.pnlClinicSchedule = new UltraPanel();
		this.lblBlockedName = new UltraLabel();
		this.lblBlocked = new UltraLabel();
		this.lblAvailableName = new UltraLabel();
		this.lblAvailable = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).SuspendLayout();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)this.lblRuler).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.lblRuler.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.lblRuler).Location = new System.Drawing.Point(99, 43);
		((System.Windows.Forms.Control)(object)this.lblRuler).Name = "lblRuler";
		((System.Windows.Forms.Control)(object)this.lblRuler).Size = new System.Drawing.Size(493, 38);
		((System.Windows.Forms.Control)(object)this.lblRuler).TabIndex = 15;
		((System.Windows.Forms.Control)(object)this.lblRuler).Paint += new System.Windows.Forms.PaintEventHandler(lblRuler_Paint);
		((System.Windows.Forms.Control)(object)this.btnPrevious).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((UltraButtonBase)this.btnPrevious).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnPrevious).Location = new System.Drawing.Point(135, 7);
		((System.Windows.Forms.Control)(object)this.btnPrevious).Name = "btnPrevious";
		((System.Windows.Forms.Control)(object)this.btnPrevious).Size = new System.Drawing.Size(75, 23);
		((System.Windows.Forms.Control)(object)this.btnPrevious).TabIndex = 93;
		((System.Windows.Forms.Control)(object)this.btnPrevious).Text = "Previous";
		((System.Windows.Forms.Control)(object)this.btnPrevious).Click += new System.EventHandler(btnPrevious_Click);
		((System.Windows.Forms.Control)(object)this.btnNext).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((UltraButtonBase)this.btnNext).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnNext).Location = new System.Drawing.Point(432, 7);
		((System.Windows.Forms.Control)(object)this.btnNext).Name = "btnNext";
		((System.Windows.Forms.Control)(object)this.btnNext).Size = new System.Drawing.Size(75, 23);
		((System.Windows.Forms.Control)(object)this.btnNext).TabIndex = 92;
		((System.Windows.Forms.Control)(object)this.btnNext).Text = "Next";
		((System.Windows.Forms.Control)(object)this.btnNext).Click += new System.EventHandler(btnNext_Click);
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.dtpFromDate.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpFromDate.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Location = new System.Drawing.Point(253, 8);
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Name = "dtpFromDate";
		((EditorButtonControlBase)this.dtpFromDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpFromDate).Size = new System.Drawing.Size(73, 22);
		((System.Windows.Forms.Control)(object)this.dtpFromDate).TabIndex = 94;
		this.dtpFromDate.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.lblFromDate).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.lblFromDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFromDate).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblFromDate).Location = new System.Drawing.Point(214, 12);
		((System.Windows.Forms.Control)(object)this.lblFromDate).Name = "lblFromDate";
		((System.Windows.Forms.Control)(object)this.lblFromDate).Size = new System.Drawing.Size(29, 15);
		((System.Windows.Forms.Control)(object)this.lblFromDate).TabIndex = 95;
		((System.Windows.Forms.Control)(object)this.lblFromDate).Text = "From";
		((ControlBase)this.lblFromDate).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.lblToDate.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblToDate).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblToDate).Location = new System.Drawing.Point(332, 12);
		((System.Windows.Forms.Control)(object)this.lblToDate).Name = "lblToDate";
		((System.Windows.Forms.Control)(object)this.lblToDate).Size = new System.Drawing.Size(16, 15);
		((System.Windows.Forms.Control)(object)this.lblToDate).TabIndex = 97;
		((System.Windows.Forms.Control)(object)this.lblToDate).Text = "To";
		((ControlBase)this.lblToDate).WrapText = false;
		((System.Windows.Forms.Control)(object)this.dtpToDate).Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.dtpToDate.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpToDate.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpToDate).Location = new System.Drawing.Point(355, 8);
		((System.Windows.Forms.Control)(object)this.dtpToDate).Name = "dtpToDate";
		((EditorButtonControlBase)this.dtpToDate).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpToDate).Size = new System.Drawing.Size(73, 22);
		((System.Windows.Forms.Control)(object)this.dtpToDate).TabIndex = 96;
		this.dtpToDate.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.pnlClinicSchedule.AutoScroll = true;
		this.pnlClinicSchedule.BorderStyle = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).Location = new System.Drawing.Point(2, 87);
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).Name = "pnlClinicSchedule";
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).Size = new System.Drawing.Size(590, 386);
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).TabIndex = 14;
		((System.Windows.Forms.Control)(object)this.lblBlockedName).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).TextHAlignAsString = "Center";
		((AppearanceBase)val).TextVAlignAsString = "Middle";
		((ControlBase)this.lblBlockedName).Appearance = (AppearanceBase)(object)val;
		this.lblBlockedName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblBlockedName).AutoSize = true;
		this.lblBlockedName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblBlockedName).Location = new System.Drawing.Point(99, 479);
		((System.Windows.Forms.Control)(object)this.lblBlockedName).Name = "lblBlockedName";
		((System.Windows.Forms.Control)(object)this.lblBlockedName).Size = new System.Drawing.Size(41, 15);
		((System.Windows.Forms.Control)(object)this.lblBlockedName).TabIndex = 98;
		((System.Windows.Forms.Control)(object)this.lblBlockedName).Text = "Blocked";
		((UltraControlBase)this.lblBlockedName).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblBlocked).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val2).TextHAlignAsString = "Center";
		((AppearanceBase)val2).TextVAlignAsString = "Middle";
		((ControlBase)this.lblBlocked).Appearance = (AppearanceBase)(object)val2;
		this.lblBlocked.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblBlocked).Location = new System.Drawing.Point(154, 476);
		((System.Windows.Forms.Control)(object)this.lblBlocked).Name = "lblBlocked";
		((System.Windows.Forms.Control)(object)this.lblBlocked).Size = new System.Drawing.Size(23, 22);
		((System.Windows.Forms.Control)(object)this.lblBlocked).TabIndex = 99;
		((UltraControlBase)this.lblBlocked).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).TextHAlignAsString = "Center";
		((AppearanceBase)val3).TextVAlignAsString = "Middle";
		((ControlBase)this.lblAvailableName).Appearance = (AppearanceBase)(object)val3;
		this.lblAvailableName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).AutoSize = true;
		this.lblAvailableName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Location = new System.Drawing.Point(3, 479);
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Name = "lblAvailableName";
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Size = new System.Drawing.Size(47, 15);
		((System.Windows.Forms.Control)(object)this.lblAvailableName).TabIndex = 102;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Text = "Available";
		((UltraControlBase)this.lblAvailableName).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblAvailable).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val4).TextHAlignAsString = "Center";
		((AppearanceBase)val4).TextVAlignAsString = "Middle";
		((ControlBase)this.lblAvailable).Appearance = (AppearanceBase)(object)val4;
		this.lblAvailable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblAvailable).Location = new System.Drawing.Point(58, 476);
		((System.Windows.Forms.Control)(object)this.lblAvailable).Name = "lblAvailable";
		((System.Windows.Forms.Control)(object)this.lblAvailable).Size = new System.Drawing.Size(23, 22);
		((System.Windows.Forms.Control)(object)this.lblAvailable).TabIndex = 103;
		((UltraControlBase)this.lblAvailable).UseAppStyling = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailableName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailable);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBlockedName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblBlocked);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrevious);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRuler);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlClinicSchedule);
		base.Name = "ClinicScheduleUserControl";
		base.Size = new System.Drawing.Size(597, 498);
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
