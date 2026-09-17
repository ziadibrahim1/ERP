using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace DoctorScheduleControl;

public class DoctorScheduleUserControl : UserControl
{
	public class MyEventArgs : EventArgs
	{
		private readonly int _IndexRow;

		private readonly int _IndexButton;

		private readonly int _ReservationID;

		private readonly TimeSpan _ReservationTime;

		public int IndexRow => _IndexRow;

		public int IndexButton => _IndexButton;

		public int ReservationID => _ReservationID;

		public TimeSpan ReservationTime => _ReservationTime;

		public MyEventArgs(int IndexRow, int IndexButton, int ReservationID, TimeSpan ReservationTime)
		{
			_IndexRow = IndexRow;
			_IndexButton = IndexButton;
			_ReservationID = ReservationID;
			_ReservationTime = ReservationTime;
		}
	}

	private Pen pen;

	private StringFormat format;

	private ArrayList lbllist = new ArrayList();

	private string _DateTimeMaskInput;

	private int _MinStartTime;

	private int _MaxEndTime;

	private DataTable _dtDoctorSchedule;

	private DataTable _dtDoctorReservation;

	private Color _ReservedColor;

	private Color _AvailableColor;

	private Color _ArrivedColor;

	private Color _ClosedColor;

	private DateTime _FromDate;

	private IContainer components = null;

	private UltraDateTimeEditor dtpSunday;

	private UltraDateTimeEditor dtpSaturday;

	private UltraDateTimeEditor dtpWednesday;

	private UltraDateTimeEditor dtpThursday;

	private UltraDateTimeEditor dtpMonday;

	private UltraDateTimeEditor dtpTuesday;

	private UltraDateTimeEditor dtpFriday;

	private UltraLabel lblSaturday;

	private UltraLabel lblSunday;

	private UltraLabel lblMonday;

	private UltraLabel lblTuesday;

	private UltraLabel lblWednesday;

	private UltraLabel lblThursday;

	private UltraLabel lblFriday;

	private UltraPanel pnlReservations;

	private UltraLabel lblRuler;

	private UltraButton btnPrevious;

	private UltraButton btnNext;

	private ToolTip toolTip1;

	private UltraLabel lblToDate;

	private UltraDateTimeEditor dtpToDate;

	private UltraLabel lblFromDate;

	private UltraDateTimeEditor dtpFromDate;

	private Timer timer1;

	public string DateTimeMaskInput
	{
		get
		{
			return _DateTimeMaskInput;
		}
		set
		{
			_DateTimeMaskInput = value;
			FormatDateTimePicker();
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

	public DataTable DoctorSchedule
	{
		get
		{
			return _dtDoctorSchedule;
		}
		set
		{
			_dtDoctorSchedule = value;
		}
	}

	public DataTable DoctorReservation
	{
		get
		{
			return _dtDoctorReservation;
		}
		set
		{
			_dtDoctorReservation = value;
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
			SetDateTime();
		}
	}

	public event EventHandler<MyEventArgs> btnItemsClick;

	public event EventHandler btnNextClick;

	public event EventHandler btnPreviousClick;

	public DoctorScheduleUserControl()
	{
		InitializeComponent();
		pen = new Pen(Color.Black, float.Epsilon);
		format = new StringFormat(StringFormat.GenericTypographic);
		format.FormatFlags = StringFormatFlags.NoWrap;
		format.Trimming = StringTrimming.Character;
	}

	private void FormatDateTimePicker()
	{
		UltraDateTimeEditor obj = dtpSaturday;
		UltraDateTimeEditor obj2 = dtpSunday;
		UltraDateTimeEditor obj3 = dtpMonday;
		UltraDateTimeEditor obj4 = dtpTuesday;
		UltraDateTimeEditor obj5 = dtpWednesday;
		UltraDateTimeEditor obj6 = dtpThursday;
		UltraDateTimeEditor obj7 = dtpFriday;
		UltraDateTimeEditor obj8 = dtpFromDate;
		string text = (dtpToDate.MaskInput = _DateTimeMaskInput);
		string text2 = (obj8.MaskInput = text);
		string text4 = (obj7.MaskInput = text2);
		string text6 = (obj6.MaskInput = text4);
		string text8 = (obj5.MaskInput = text6);
		string text10 = (obj4.MaskInput = text8);
		string text12 = (obj3.MaskInput = text10);
		string maskInput = (obj2.MaskInput = text12);
		obj.MaskInput = maskInput;
	}

	private void lblRuler_Paint(object sender, PaintEventArgs e)
	{
		if (_MaxEndTime - _MinStartTime <= 0)
		{
			return;
		}
		Graphics graphics = e.Graphics;
		string text = "";
		string text2 = "";
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
					text2 = ((num3 - 12 <= 0) ? (num3 + ":30").ToString(CultureInfo.InvariantCulture) : (num3 - 12 + ":30").ToString(CultureInfo.InvariantCulture));
					SizeF sizeF = graphics.MeasureString(text2, Font, ((Control)(object)lblRuler).Size, format);
					graphics.DrawString(text2, Font, Brushes.Black, num5 + 2f + num7 - sizeF.Width / 2f, 2f * num, format);
				}
				else
				{
					graphics.DrawLine(pen, num5 + 2f + num7, 0f, num5 + 2f + num7, 1f * num);
				}
				num6++;
			}
			graphics.DrawLine(pen, num5 + 2f, 0f, num5 + 2f, 3f * num);
			text = ((num3 - 12 <= 0) ? (num3 + " AM").ToString(CultureInfo.InvariantCulture) : (num3 - 12 + " PM").ToString(CultureInfo.InvariantCulture));
			SizeF sizeF2 = graphics.MeasureString(text, Font, ((Control)(object)lblRuler).Size, format);
			graphics.DrawString(text, Font, Brushes.Black, num5 + 4f - sizeF2.Width / 2f, 4f * num, format);
			num3++;
		}
	}

	private void SetDateTime()
	{
		UltraDateTimeEditor obj = dtpSaturday;
		DateTime dateTime = (dtpFromDate.DateTime = _FromDate);
		obj.DateTime = dateTime;
		dtpSunday.DateTime = _FromDate.AddDays(1.0);
		dtpMonday.DateTime = _FromDate.AddDays(2.0);
		dtpTuesday.DateTime = _FromDate.AddDays(3.0);
		dtpWednesday.DateTime = _FromDate.AddDays(4.0);
		dtpThursday.DateTime = _FromDate.AddDays(5.0);
		UltraDateTimeEditor obj2 = dtpFriday;
		dateTime = (dtpToDate.DateTime = _FromDate.AddDays(6.0));
		obj2.DateTime = dateTime;
	}

	public void CreateSchedule()
	{
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		((Control)(object)pnlReservations.ClientArea).Controls.Clear();
		decimal num = (_MaxEndTime - _MinStartTime) * 60;
		decimal num2 = (decimal)((Control)(object)lblRuler).Width / num;
		int num3 = 0;
		DateTime dateTime = _FromDate;
		for (int i = 0; i < _dtDoctorSchedule.Rows.Count; i++)
		{
			double num4 = 0.0;
			if (int.Parse(_dtDoctorSchedule.Rows[i]["MaxDiagnoseCount"].ToString()) > 0)
			{
				DateTime dateTime2 = DateTime.Parse(_dtDoctorSchedule.Rows[i]["FromTime"].ToString());
				DateTime dateTime3 = dateTime2.AddHours(-_MinStartTime);
				double num5 = (double)((decimal)(dateTime3.Hour * 60 + dateTime3.Minute) * num2 + 5m);
				TimeSpan timeSpan = DateTime.Parse(_dtDoctorSchedule.Rows[i]["ToTime"].ToString()) - dateTime2;
				double num6 = timeSpan.TotalMinutes / double.Parse(_dtDoctorSchedule.Rows[i]["MaxDiagnoseCount"].ToString()) * double.Parse(num2.ToString());
				for (int j = 0; j < int.Parse(_dtDoctorSchedule.Rows[i]["MaxDiagnoseCount"].ToString()); j++)
				{
					UltraLabel val = new UltraLabel();
					lbllist.Add(val);
					((Control)(object)val).Left = (int)(num5 + num4);
					((Control)(object)val).Top = num3 + 5;
					((Control)(object)val).Width = (int)num6 - 1;
					((Control)(object)val).Height = ((Control)(object)dtpSaturday).Height;
					((Control)(object)val).Text = (j + 1).ToString();
					((Control)(object)val).Name = i.ToString() + "-" + dateTime2.AddMinutes(timeSpan.TotalMinutes / double.Parse(_dtDoctorSchedule.Rows[i]["MaxDiagnoseCount"].ToString()) * (double)j).TimeOfDay;
					((ControlBase)val).Appearance.FontData.SizeInPoints = 8f;
					((ControlBase)val).Appearance.TextHAlign = (HAlign)2;
					DataRow[] array = _dtDoctorReservation.Select("AppointmentDate>= '" + dateTime.ToShortDateString() + "' And AppointmentDate <='" + dateTime.Date.AddDays(1.0).AddSeconds(-1.0).ToString() + "' And OrderNo =" + (j + 1));
					if (array.Length != 0)
					{
						if (bool.Parse(array[0]["IsClosed"].ToString()))
						{
							((ControlBase)val).Appearance.BackColor = _ClosedColor;
						}
						else if (bool.Parse(array[0]["IsArrived"].ToString()))
						{
							((ControlBase)val).Appearance.BackColor = _ArrivedColor;
						}
						else
						{
							((ControlBase)val).Appearance.BackColor = _ReservedColor;
						}
						((Control)(object)val).Tag = i + "," + j + "," + array[0]["ReservationID"].ToString();
						toolTip1.SetToolTip((Control)(object)val, null);
						toolTip1.SetToolTip((Control)(object)val, array[0]["PatientName"].ToString());
					}
					else
					{
						((ControlBase)val).Appearance.BackColor = _AvailableColor;
						((Control)(object)val).Tag = i + "," + j + ",-1";
						toolTip1.SetToolTip((Control)(object)val, null);
					}
					((UltraControlBase)val).UseAppStyling = false;
					((Control)(object)val).Click += btnItems_Click;
					num4 += num6;
					((Control)(object)pnlReservations.ClientArea).Controls.Add((Control)(object)val);
				}
			}
			num3 += ((Control)(object)dtpSaturday).Height + (((Control)(object)dtpSunday).Top - ((Control)(object)dtpSaturday).Bottom);
			dateTime = dateTime.AddDays(1.0);
			UltraLabel val2 = new UltraLabel();
			((Control)(object)val2).AutoSize = false;
			((Control)(object)val2).Width = ((Control)(object)lblRuler).Width - pnlReservations.AutoScrollMargin.Width;
			((Control)(object)val2).Top = num3;
			((Control)(object)val2).Height = 1;
			val2.BorderStyleInner = (UIElementBorderStyle)4;
			((Control)(object)pnlReservations.ClientArea).Controls.Add((Control)(object)val2);
		}
	}

	public void RefreshSchedule()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		DateTime dateTime = _FromDate;
		SetDateTime();
		int num = 0;
		for (int i = 0; i < _dtDoctorSchedule.Rows.Count; i++)
		{
			if (int.Parse(_dtDoctorSchedule.Rows[i]["MaxDiagnoseCount"].ToString()) > 0)
			{
				for (int j = 0; j < int.Parse(_dtDoctorSchedule.Rows[i]["MaxDiagnoseCount"].ToString()); j++)
				{
					UltraLabel val = (UltraLabel)lbllist[num];
					DataRow[] array = _dtDoctorReservation.Select("AppointmentDate>= '" + dateTime.ToShortDateString() + "' And AppointmentDate <='" + dateTime.Date.AddDays(1.0).AddSeconds(-1.0).ToString() + "' And OrderNo =" + (j + 1));
					if (array.Length != 0)
					{
						if (bool.Parse(array[0]["IsClosed"].ToString()))
						{
							((ControlBase)val).Appearance.BackColor = _ClosedColor;
						}
						else if (bool.Parse(array[0]["IsArrived"].ToString()))
						{
							((ControlBase)val).Appearance.BackColor = _ArrivedColor;
						}
						else
						{
							((ControlBase)val).Appearance.BackColor = _ReservedColor;
						}
						((Control)(object)val).Tag = i + "," + j + "," + array[0]["ReservationID"].ToString();
						toolTip1.SetToolTip((Control)(object)val, null);
						toolTip1.SetToolTip((Control)(object)val, array[0]["PatientName"].ToString());
					}
					else
					{
						((ControlBase)val).Appearance.BackColor = _AvailableColor;
						((Control)(object)val).Tag = i + "," + j + ",-1";
						toolTip1.SetToolTip((Control)(object)val, null);
					}
					num++;
				}
			}
			dateTime = dateTime.AddDays(1.0);
		}
	}

	protected virtual void btnItems_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		string[] array = ((Control)(UltraLabel)sender).Tag.ToString().Split(',');
		string[] array2 = ((Control)(UltraLabel)sender).Name.ToString().Split('-');
		if (this.btnItemsClick != null)
		{
			this.btnItemsClick(this, new MyEventArgs(int.Parse(array[0].ToString()), int.Parse(array[1].ToString()), int.Parse(array[2].ToString()), TimeSpan.Parse(array2[1].ToString())));
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
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
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
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		this.components = new System.ComponentModel.Container();
		this.pnlReservations = new UltraPanel();
		this.dtpSunday = new UltraDateTimeEditor();
		this.dtpSaturday = new UltraDateTimeEditor();
		this.dtpWednesday = new UltraDateTimeEditor();
		this.dtpThursday = new UltraDateTimeEditor();
		this.dtpMonday = new UltraDateTimeEditor();
		this.dtpTuesday = new UltraDateTimeEditor();
		this.dtpFriday = new UltraDateTimeEditor();
		this.lblSaturday = new UltraLabel();
		this.lblSunday = new UltraLabel();
		this.lblMonday = new UltraLabel();
		this.lblTuesday = new UltraLabel();
		this.lblWednesday = new UltraLabel();
		this.lblThursday = new UltraLabel();
		this.lblFriday = new UltraLabel();
		this.lblRuler = new UltraLabel();
		this.btnPrevious = new UltraButton();
		this.btnNext = new UltraButton();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.dtpFromDate = new UltraDateTimeEditor();
		this.lblFromDate = new UltraLabel();
		this.lblToDate = new UltraLabel();
		this.dtpToDate = new UltraDateTimeEditor();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		((System.Windows.Forms.Control)(object)this.pnlReservations).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtpSunday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSaturday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWednesday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpThursday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMonday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTuesday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFriday).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)this.pnlReservations).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.pnlReservations.BorderStyle = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.pnlReservations).Location = new System.Drawing.Point(109, 87);
		((System.Windows.Forms.Control)(object)this.pnlReservations).Name = "pnlReservations";
		((System.Windows.Forms.Control)(object)this.pnlReservations).Size = new System.Drawing.Size(485, 199);
		((System.Windows.Forms.Control)(object)this.pnlReservations).TabIndex = 14;
		((System.Windows.Forms.Control)(object)this.dtpSunday).AutoSize = false;
		this.dtpSunday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpSunday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpSunday).Location = new System.Drawing.Point(3, 113);
		((System.Windows.Forms.Control)(object)this.dtpSunday).Name = "dtpSunday";
		((EditorButtonControlBase)this.dtpSunday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpSunday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpSunday).TabIndex = 0;
		((UltraControlBase)this.dtpSunday).UseAppStyling = false;
		this.dtpSunday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpSaturday).AutoSize = false;
		this.dtpSaturday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpSaturday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpSaturday).Location = new System.Drawing.Point(3, 85);
		((System.Windows.Forms.Control)(object)this.dtpSaturday).Name = "dtpSaturday";
		((EditorButtonControlBase)this.dtpSaturday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpSaturday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpSaturday).TabIndex = 1;
		((UltraControlBase)this.dtpSaturday).UseAppStyling = false;
		this.dtpSaturday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpWednesday).AutoSize = false;
		this.dtpWednesday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpWednesday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpWednesday).Location = new System.Drawing.Point(3, 197);
		((System.Windows.Forms.Control)(object)this.dtpWednesday).Name = "dtpWednesday";
		((EditorButtonControlBase)this.dtpWednesday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpWednesday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpWednesday).TabIndex = 3;
		((UltraControlBase)this.dtpWednesday).UseAppStyling = false;
		this.dtpWednesday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpThursday).AutoSize = false;
		this.dtpThursday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpThursday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpThursday).Location = new System.Drawing.Point(3, 225);
		((System.Windows.Forms.Control)(object)this.dtpThursday).Name = "dtpThursday";
		((EditorButtonControlBase)this.dtpThursday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpThursday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpThursday).TabIndex = 2;
		((UltraControlBase)this.dtpThursday).UseAppStyling = false;
		this.dtpThursday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpMonday).AutoSize = false;
		this.dtpMonday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpMonday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpMonday).Location = new System.Drawing.Point(3, 141);
		((System.Windows.Forms.Control)(object)this.dtpMonday).Name = "dtpMonday";
		((EditorButtonControlBase)this.dtpMonday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpMonday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpMonday).TabIndex = 5;
		((UltraControlBase)this.dtpMonday).UseAppStyling = false;
		this.dtpMonday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpTuesday).AutoSize = false;
		this.dtpTuesday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpTuesday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpTuesday).Location = new System.Drawing.Point(3, 169);
		((System.Windows.Forms.Control)(object)this.dtpTuesday).Name = "dtpTuesday";
		((EditorButtonControlBase)this.dtpTuesday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpTuesday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpTuesday).TabIndex = 4;
		((UltraControlBase)this.dtpTuesday).UseAppStyling = false;
		this.dtpTuesday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		((System.Windows.Forms.Control)(object)this.dtpFriday).AutoSize = false;
		this.dtpFriday.DateTime = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.dtpFriday.DropDownButtonDisplayStyle = (ButtonDisplayStyle)0;
		((System.Windows.Forms.Control)(object)this.dtpFriday).Location = new System.Drawing.Point(3, 253);
		((System.Windows.Forms.Control)(object)this.dtpFriday).Name = "dtpFriday";
		((EditorButtonControlBase)this.dtpFriday).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.dtpFriday).Size = new System.Drawing.Size(70, 22);
		((System.Windows.Forms.Control)(object)this.dtpFriday).TabIndex = 6;
		((UltraControlBase)this.dtpFriday).UseAppStyling = false;
		this.dtpFriday.Value = new System.DateTime(2013, 12, 30, 0, 0, 0, 0);
		this.lblSaturday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSaturday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblSaturday).Location = new System.Drawing.Point(76, 87);
		((System.Windows.Forms.Control)(object)this.lblSaturday).Name = "lblSaturday";
		((System.Windows.Forms.Control)(object)this.lblSaturday).Size = new System.Drawing.Size(20, 15);
		((System.Windows.Forms.Control)(object)this.lblSaturday).TabIndex = 7;
		((System.Windows.Forms.Control)(object)this.lblSaturday).Text = "Sat";
		((ControlBase)this.lblSaturday).WrapText = false;
		this.lblSunday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblSunday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblSunday).Location = new System.Drawing.Point(76, 115);
		((System.Windows.Forms.Control)(object)this.lblSunday).Name = "lblSunday";
		((System.Windows.Forms.Control)(object)this.lblSunday).Size = new System.Drawing.Size(22, 15);
		((System.Windows.Forms.Control)(object)this.lblSunday).TabIndex = 8;
		((System.Windows.Forms.Control)(object)this.lblSunday).Text = "Sun";
		((ControlBase)this.lblSunday).WrapText = false;
		this.lblMonday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblMonday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblMonday).Location = new System.Drawing.Point(76, 143);
		((System.Windows.Forms.Control)(object)this.lblMonday).Name = "lblMonday";
		((System.Windows.Forms.Control)(object)this.lblMonday).Size = new System.Drawing.Size(25, 15);
		((System.Windows.Forms.Control)(object)this.lblMonday).TabIndex = 9;
		((System.Windows.Forms.Control)(object)this.lblMonday).Text = "Mon";
		((ControlBase)this.lblMonday).WrapText = false;
		this.lblTuesday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblTuesday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblTuesday).Location = new System.Drawing.Point(76, 171);
		((System.Windows.Forms.Control)(object)this.lblTuesday).Name = "lblTuesday";
		((System.Windows.Forms.Control)(object)this.lblTuesday).Size = new System.Drawing.Size(22, 15);
		((System.Windows.Forms.Control)(object)this.lblTuesday).TabIndex = 10;
		((System.Windows.Forms.Control)(object)this.lblTuesday).Text = "Tue";
		((ControlBase)this.lblTuesday).WrapText = false;
		this.lblWednesday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblWednesday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblWednesday).Location = new System.Drawing.Point(76, 199);
		((System.Windows.Forms.Control)(object)this.lblWednesday).Name = "lblWednesday";
		((System.Windows.Forms.Control)(object)this.lblWednesday).Size = new System.Drawing.Size(26, 15);
		((System.Windows.Forms.Control)(object)this.lblWednesday).TabIndex = 11;
		((System.Windows.Forms.Control)(object)this.lblWednesday).Text = "Wed";
		((ControlBase)this.lblWednesday).WrapText = false;
		this.lblThursday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblThursday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblThursday).Location = new System.Drawing.Point(76, 227);
		((System.Windows.Forms.Control)(object)this.lblThursday).Name = "lblThursday";
		((System.Windows.Forms.Control)(object)this.lblThursday).Size = new System.Drawing.Size(23, 15);
		((System.Windows.Forms.Control)(object)this.lblThursday).TabIndex = 12;
		((System.Windows.Forms.Control)(object)this.lblThursday).Text = "Thu";
		((ControlBase)this.lblThursday).WrapText = false;
		this.lblFriday.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblFriday).AutoSize = true;
		((System.Windows.Forms.Control)(object)this.lblFriday).Location = new System.Drawing.Point(76, 255);
		((System.Windows.Forms.Control)(object)this.lblFriday).Name = "lblFriday";
		((System.Windows.Forms.Control)(object)this.lblFriday).Size = new System.Drawing.Size(16, 15);
		((System.Windows.Forms.Control)(object)this.lblFriday).TabIndex = 13;
		((System.Windows.Forms.Control)(object)this.lblFriday).Text = "Fri";
		((ControlBase)this.lblFriday).WrapText = false;
		((System.Windows.Forms.Control)(object)this.lblRuler).Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.lblRuler.BorderStyleOuter = (UIElementBorderStyle)4;
		((System.Windows.Forms.Control)(object)this.lblRuler).Location = new System.Drawing.Point(109, 43);
		((System.Windows.Forms.Control)(object)this.lblRuler).Name = "lblRuler";
		((System.Windows.Forms.Control)(object)this.lblRuler).Size = new System.Drawing.Size(484, 38);
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		this.AutoSize = true;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpToDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFromDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnPrevious);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnNext);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblRuler);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.pnlReservations);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblFriday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblThursday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblWednesday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTuesday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMonday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSunday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblSaturday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpFriday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpMonday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpTuesday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpWednesday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpThursday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSaturday);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSunday);
		base.Name = "DoctorScheduleUserControl";
		base.Size = new System.Drawing.Size(597, 498);
		((System.Windows.Forms.Control)(object)this.pnlReservations).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dtpSunday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSaturday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpWednesday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpThursday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpMonday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTuesday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFriday).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpFromDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpToDate).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
