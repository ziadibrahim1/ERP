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

namespace ClinicScheduleReservationUserControl;

public class ClinicScheduleReservationUserControl : UserControl
{
	public class MyEventArgs : EventArgs
	{
		private readonly int _IndexRow;

		private readonly int _IndexButton;

		private readonly int _DoctorID;

		private readonly int _AlternativeDoctorID;

		private readonly int _ReservationID;

		private readonly int _ClinicScheduleID;

		private readonly TimeSpan _ReservationTime;

		public int IndexRow => _IndexRow;

		public int IndexButton => _IndexButton;

		public int DoctorID => _DoctorID;

		public int AlternativeDoctorID => _AlternativeDoctorID;

		public int ReservationID => _ReservationID;

		public int ClinicScheduleID => _ClinicScheduleID;

		public TimeSpan ReservationTime => _ReservationTime;

		public MyEventArgs(int IndexRow, int IndexButton, int ReservationID, int DoctorID, int AlternativeDoctorID, int ClinicScheduleID, TimeSpan ReservationTime)
		{
			_IndexRow = IndexRow;
			_IndexButton = IndexButton;
			_ReservationID = ReservationID;
			_DoctorID = DoctorID;
			_AlternativeDoctorID = AlternativeDoctorID;
			_ClinicScheduleID = ClinicScheduleID;
			_ReservationTime = ReservationTime;
		}
	}

	private Pen pen;

	private StringFormat format;

	private string _DateTimeMaskInput;

	private int _MinStartTime;

	private int _MaxEndTime;

	private DataTable _dtClinicSchedule;

	private DataTable _ClinicReservation;

	private Color _ReservedColor;

	private Color _AvailableColor;

	private Color _ArrivedColor;

	private Color _ClosedColor;

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

	public UltraLabel lblAvailableName;

	public UltraLabel lblAvailable;

	public UltraLabel lblReservedName;

	public UltraLabel lblReserved;

	public UltraLabel lblArrived;

	public UltraLabel lblArrivedName;

	public UltraLabel lblClosedName;

	public UltraLabel lblClosed;

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

	public DataTable ClinicReservation
	{
		get
		{
			return _ClinicReservation;
		}
		set
		{
			_ClinicReservation = value;
		}
	}

	public Color ReservedColor
	{
		get
		{
			return _ReservedColor;
		}
		set
		{
			_ReservedColor = value;
			((ControlBase)lblReserved).Appearance.BackColor = _ReservedColor;
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

	public Color ArrivedColor
	{
		get
		{
			return _ArrivedColor;
		}
		set
		{
			_ArrivedColor = value;
			((ControlBase)lblArrived).Appearance.BackColor = _ArrivedColor;
		}
	}

	public Color ClosedColor
	{
		get
		{
			return _ClosedColor;
		}
		set
		{
			_ClosedColor = value;
			((ControlBase)lblClosed).Appearance.BackColor = _ClosedColor;
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

	public ClinicScheduleReservationUserControl()
	{
		InitializeComponent();
		pen = new Pen(Color.Black, float.Epsilon);
		format = new StringFormat(StringFormat.GenericTypographic);
		format.FormatFlags = StringFormatFlags.NoWrap;
		format.Trimming = StringTrimming.Character;
		dtpFromDate.MaskInput = "dd/mm/yyyy";
		dtpToDate.MaskInput = "dd/mm/yyyy";
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
			text = ((24 - num3 > 0) ? num3 : (num3 - 24)).ToString().ToString(CultureInfo.InvariantCulture);
			SizeF sizeF2 = graphics.MeasureString(text, Font, ((Control)(object)lblRuler).Size, format);
			graphics.DrawString(text, Font, Brushes.Black, num5 + 4f - sizeF2.Width / 2f, 3f * num, format);
			num3++;
		}
		text = ((24 - _MaxEndTime > 0) ? _MaxEndTime : (_MaxEndTime - 24)).ToString().ToString(CultureInfo.InvariantCulture);
		SizeF sizeF3 = graphics.MeasureString(text, Font, ((Control)(object)lblRuler).Size, format);
		graphics.DrawString(text, Font, Brushes.Black, num2 - 2f - sizeF3.Width / 2f, 3f * num, format);
	}

	public void CreateSchedule()
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Expected O, but got Unknown
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c54: Expected O, but got Unknown
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Expected O, but got Unknown
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Expected O, but got Unknown
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Expected O, but got Unknown
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
		GC.Collect();
		int num8 = 0;
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
			decimal num9 = Math.Abs((_MaxEndTime - _MinStartTime) * 60);
			decimal num10 = (decimal)((Control)(object)lblRuler).Width / num9;
			int num11 = 0;
			for (int j = 0; j < dataView.Count; j++)
			{
				num11 = 0;
				if (text != ((dataView[j]["AlternativeDoctorName"] == DBNull.Value) ? dataView[j]["DoctorName"].ToString() : dataView[j]["AlternativeDoctorName"].ToString()))
				{
					text = ((dataView[j]["AlternativeDoctorName"] == DBNull.Value) ? dataView[j]["DoctorName"].ToString() : dataView[j]["AlternativeDoctorName"].ToString());
					UltraLabel val3 = new UltraLabel();
					((Control)(object)val3).Left = num3;
					((Control)(object)val3).Top = num4 + num2;
					((Control)(object)val3).Width = num7;
					((Control)(object)val3).Height = num;
					((Control)(object)val3).Text = text;
					((Control)(object)val3).Name = dataView[j]["ClinicScheduleID"].ToString();
					((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val3);
					if (num8 == 0)
					{
						UltraLabel val4 = new UltraLabel();
						((Control)(object)val4).Left = num3;
						((Control)(object)val4).Top = num4 + num2;
						((Control)(object)val4).Height = num - 2;
						((Control)(object)val4).Width = ((Control)(object)pnlClinicSchedule).Width - num3 - num2;
						((Control)(object)val4).Name = "lbl" + text;
						((ControlBase)val4).Appearance.BackColor = Color.LightGray;
						((UltraControlBase)val4).UseAppStyling = false;
						((Control)(object)val4).SendToBack();
						((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val4);
					}
					num11++;
					num8++;
					if (num8 == 2)
					{
						num8 = 0;
					}
				}
				decimal num12 = (DateTime.Parse(dataView[j]["FromDate"].ToString()).Hour - _MinStartTime) * 60 + DateTime.Parse(dataView[j]["FromDate"].ToString()).Minute;
				decimal num13 = Math.Abs((decimal)(DateTime.Parse(dataView[j]["ToDate"].ToString()) - DateTime.Parse(dataView[j]["FromDate"].ToString())).TotalMinutes);
				decimal num14 = num13 * num10 / decimal.Parse(dataView[j]["MaxDiagnoseCount"].ToString());
				for (int k = 0; k < int.Parse(dataView[j]["MaxDiagnoseCount"].ToString()); k++)
				{
					UltraButton val5 = new UltraButton();
					((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val5);
					((UltraControlBase)val5).UseAppStyling = false;
					((ControlBase)val5).Appearance.ThemedElementAlpha = (Alpha)3;
					if (k == 0)
					{
						((Control)(object)val5).Left = (int)(num12 * num10 + (decimal)((Control)(object)lblRuler).Left);
					}
					else
					{
						((Control)(object)val5).Left = (int)(num12 * num10 + (decimal)((Control)(object)lblRuler).Left + num14 * (decimal)k);
					}
					((Control)(object)val5).Top = num4 + num2;
					((Control)(object)val5).BringToFront();
					((Control)(object)val5).Width = (int)num14 - 1;
					((Control)(object)val5).Height = num - 5;
					((Control)(object)val5).Text = (k + 1).ToString();
					((Control)(object)val5).Name = (i + j).ToString() + "-" + DateTime.Parse(dataView[j]["FromDate"].ToString()).AddMinutes(double.Parse(num13.ToString()) / double.Parse(dataView[j]["MaxDiagnoseCount"].ToString()) * (double)k).TimeOfDay;
					((Control)(object)val5).Enabled = !bool.Parse(dataView[j]["IsBlocked"].ToString());
					((ControlBase)val5).Appearance.FontData.SizeInPoints = 5f;
					((ControlBase)val5).Appearance.FontData.Name = "Tahoma";
					DataRow[] array = _ClinicReservation.Select("DoctorID= " + dataView[j]["DoctorID"].ToString() + " And ClinicScheduleID= " + dataView[j]["ClinicScheduleID"].ToString() + " And OrderNo =" + (k + 1));
					if (array.Length != 0)
					{
						if (bool.Parse(array[0]["IsClosed"].ToString()))
						{
							((ControlBase)val5).Appearance.BackColor = _ClosedColor;
						}
						else if (bool.Parse(array[0]["IsArrived"].ToString()))
						{
							((ControlBase)val5).Appearance.BackColor = _ArrivedColor;
						}
						else
						{
							((ControlBase)val5).Appearance.BackColor = _ReservedColor;
						}
						((Control)(object)val5).Tag = i + "," + k + "," + array[0]["ReservationID"].ToString() + "," + dataView[j]["DoctorID"].ToString() + "," + ((dataView[j]["AlternativeDoctorID"] == DBNull.Value) ? "-1" : dataView[j]["AlternativeDoctorID"].ToString()) + "," + dataView[j]["ClinicScheduleID"].ToString();
						string text2 = "";
						text2 = text2 + " OrderNo : " + (k + 1);
						text2 = text2 + "\nPatient Name : " + array[0]["PatientName"].ToString();
						if (text2 != "")
						{
							toolTip1.SetToolTip((Control)(object)val5, text2);
						}
					}
					else
					{
						((ControlBase)val5).Appearance.BackColor = _AvailableColor;
						((Control)(object)val5).Tag = i + "," + k + ",-1," + dataView[j]["DoctorID"].ToString() + "," + ((dataView[j]["AlternativeDoctorID"] == DBNull.Value) ? "-1" : dataView[j]["AlternativeDoctorID"].ToString()) + "," + dataView[j]["ClinicScheduleID"].ToString();
						string text3 = "";
						text3 = text3 + " OrderNo : " + (k + 1);
						if (text3 != "")
						{
							toolTip1.SetToolTip((Control)(object)val5, text3);
						}
					}
					((Control)(object)val5).Click += btnDoctorPeriod_Click;
				}
				if (j < dataView.Count - 1 && text != ((dataView[j + 1]["AlternativeDoctorName"] == DBNull.Value) ? dataView[j + 1]["DoctorName"].ToString() : dataView[j + 1]["AlternativeDoctorName"].ToString()))
				{
					num4 += num;
				}
			}
			num4 += num;
			UltraLabel val6 = new UltraLabel();
			((Control)(object)val6).AutoSize = false;
			((Control)(object)val6).Width = ((Control)(object)pnlClinicSchedule).Width - num2;
			((Control)(object)val6).Top = num4 + num2;
			((Control)(object)val6).Height = 1;
			val6.BorderStyleInner = (UIElementBorderStyle)4;
			((Control)(object)pnlClinicSchedule.ClientArea).Controls.Add((Control)(object)val6);
			num3 = 0;
		}
		((Control)(object)pnlClinicSchedule).Visible = true;
	}

	protected virtual void btnDoctorPeriod_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		string[] array = ((Control)(UltraButton)sender).Tag.ToString().Split(',');
		string[] array2 = ((Control)(UltraButton)sender).Name.ToString().Split('-');
		if (this.btnDoctorPeriod != null)
		{
			this.btnDoctorPeriod(this, new MyEventArgs(int.Parse(array[0].ToString()), int.Parse(array[1].ToString()), int.Parse(array[2].ToString()), int.Parse(array[3].ToString()), int.Parse(array[4].ToString()), int.Parse(array[5].ToString()), TimeSpan.Parse(array2[1].ToString())));
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
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		this.lblRuler = new UltraLabel();
		this.btnPrevious = new UltraButton();
		this.btnNext = new UltraButton();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblFromDate = new UltraLabel();
		this.lblToDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.pnlClinicSchedule = new UltraPanel();
		this.lblAvailableName = new UltraLabel();
		this.lblAvailable = new UltraLabel();
		this.lblReservedName = new UltraLabel();
		this.lblReserved = new UltraLabel();
		this.lblArrived = new UltraLabel();
		this.lblArrivedName = new UltraLabel();
		this.lblClosedName = new UltraLabel();
		this.lblClosed = new UltraLabel();
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
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).Size = new System.Drawing.Size(590, 387);
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).TabIndex = 14;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val).TextHAlignAsString = "Center";
		((AppearanceBase)val).TextVAlignAsString = "Middle";
		((ControlBase)this.lblAvailableName).Appearance = (AppearanceBase)(object)val;
		this.lblAvailableName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).AutoSize = true;
		this.lblAvailableName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Location = new System.Drawing.Point(3, 483);
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Name = "lblAvailableName";
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Size = new System.Drawing.Size(47, 15);
		((System.Windows.Forms.Control)(object)this.lblAvailableName).TabIndex = 100;
		((System.Windows.Forms.Control)(object)this.lblAvailableName).Text = "Available";
		((UltraControlBase)this.lblAvailableName).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblAvailable).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val2).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val2).TextHAlignAsString = "Center";
		((AppearanceBase)val2).TextVAlignAsString = "Middle";
		((ControlBase)this.lblAvailable).Appearance = (AppearanceBase)(object)val2;
		this.lblAvailable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblAvailable).Location = new System.Drawing.Point(58, 480);
		((System.Windows.Forms.Control)(object)this.lblAvailable).Name = "lblAvailable";
		((System.Windows.Forms.Control)(object)this.lblAvailable).Size = new System.Drawing.Size(23, 22);
		((System.Windows.Forms.Control)(object)this.lblAvailable).TabIndex = 101;
		((UltraControlBase)this.lblAvailable).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblReservedName).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val3).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val3).TextHAlignAsString = "Center";
		((AppearanceBase)val3).TextVAlignAsString = "Middle";
		((ControlBase)this.lblReservedName).Appearance = (AppearanceBase)(object)val3;
		this.lblReservedName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblReservedName).AutoSize = true;
		this.lblReservedName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblReservedName).Location = new System.Drawing.Point(99, 483);
		((System.Windows.Forms.Control)(object)this.lblReservedName).Name = "lblReservedName";
		((System.Windows.Forms.Control)(object)this.lblReservedName).Size = new System.Drawing.Size(49, 15);
		((System.Windows.Forms.Control)(object)this.lblReservedName).TabIndex = 102;
		((System.Windows.Forms.Control)(object)this.lblReservedName).Text = "Reserved";
		((UltraControlBase)this.lblReservedName).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblReserved).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val4).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val4).TextHAlignAsString = "Center";
		((AppearanceBase)val4).TextVAlignAsString = "Middle";
		((ControlBase)this.lblReserved).Appearance = (AppearanceBase)(object)val4;
		this.lblReserved.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblReserved).Location = new System.Drawing.Point(154, 480);
		((System.Windows.Forms.Control)(object)this.lblReserved).Name = "lblReserved";
		((System.Windows.Forms.Control)(object)this.lblReserved).Size = new System.Drawing.Size(23, 22);
		((System.Windows.Forms.Control)(object)this.lblReserved).TabIndex = 103;
		((UltraControlBase)this.lblReserved).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblArrived).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val5).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val5).TextHAlignAsString = "Center";
		((AppearanceBase)val5).TextVAlignAsString = "Middle";
		((ControlBase)this.lblArrived).Appearance = (AppearanceBase)(object)val5;
		this.lblArrived.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblArrived).Location = new System.Drawing.Point(249, 480);
		((System.Windows.Forms.Control)(object)this.lblArrived).Name = "lblArrived";
		((System.Windows.Forms.Control)(object)this.lblArrived).Size = new System.Drawing.Size(23, 22);
		((System.Windows.Forms.Control)(object)this.lblArrived).TabIndex = 103;
		((UltraControlBase)this.lblArrived).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblArrivedName).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val6).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val6).TextHAlignAsString = "Center";
		((AppearanceBase)val6).TextVAlignAsString = "Middle";
		((ControlBase)this.lblArrivedName).Appearance = (AppearanceBase)(object)val6;
		this.lblArrivedName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblArrivedName).AutoSize = true;
		this.lblArrivedName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblArrivedName).Location = new System.Drawing.Point(194, 483);
		((System.Windows.Forms.Control)(object)this.lblArrivedName).Name = "lblArrivedName";
		((System.Windows.Forms.Control)(object)this.lblArrivedName).Size = new System.Drawing.Size(38, 15);
		((System.Windows.Forms.Control)(object)this.lblArrivedName).TabIndex = 102;
		((System.Windows.Forms.Control)(object)this.lblArrivedName).Text = "Arrived";
		((UltraControlBase)this.lblArrivedName).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblClosedName).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val7).BackColor = System.Drawing.Color.Transparent;
		((AppearanceBase)val7).TextHAlignAsString = "Center";
		((AppearanceBase)val7).TextVAlignAsString = "Middle";
		((ControlBase)this.lblClosedName).Appearance = (AppearanceBase)(object)val7;
		this.lblClosedName.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblClosedName).AutoSize = true;
		this.lblClosedName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblClosedName).Location = new System.Drawing.Point(288, 483);
		((System.Windows.Forms.Control)(object)this.lblClosedName).Name = "lblClosedName";
		((System.Windows.Forms.Control)(object)this.lblClosedName).Size = new System.Drawing.Size(36, 15);
		((System.Windows.Forms.Control)(object)this.lblClosedName).TabIndex = 104;
		((System.Windows.Forms.Control)(object)this.lblClosedName).Text = "Closed";
		((UltraControlBase)this.lblClosedName).UseAppStyling = false;
		((System.Windows.Forms.Control)(object)this.lblClosed).Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		((AppearanceBase)val8).BackColor = System.Drawing.Color.Red;
		((AppearanceBase)val8).TextHAlignAsString = "Center";
		((AppearanceBase)val8).TextVAlignAsString = "Middle";
		((ControlBase)this.lblClosed).Appearance = (AppearanceBase)(object)val8;
		this.lblClosed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.lblClosed).Location = new System.Drawing.Point(343, 480);
		((System.Windows.Forms.Control)(object)this.lblClosed).Name = "lblClosed";
		((System.Windows.Forms.Control)(object)this.lblClosed).Size = new System.Drawing.Size(23, 22);
		((System.Windows.Forms.Control)(object)this.lblClosed).TabIndex = 105;
		((UltraControlBase)this.lblClosed).UseAppStyling = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClosedName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblClosed);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArrivedName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblArrived);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReservedName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblReserved);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailableName);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblAvailable);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrevious);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRuler);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlClinicSchedule);
		base.Name = "ClinicScheduleReservationUserControl";
		base.Size = new System.Drawing.Size(597, 498);
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlClinicSchedule).ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
