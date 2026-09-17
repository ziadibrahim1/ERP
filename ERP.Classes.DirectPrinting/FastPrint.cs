using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace ERP.Classes.DirectPrinting;

public class FastPrint
{
	private static volatile FastPrint instance;

	private static object syncRoot = new object();

	private PrintDocument p;

	private List<ReportCell> _CellsRow;

	private List<List<ReportCell>> _ListOfCellsRow;

	private PrinterSettings _PrinterSettings;

	private Margins _Margins;

	private GraphicsUnit _GraphicsUnit;

	public float OverallWidth { get; internal set; }

	public PrinterSettings PrinterSettings
	{
		get
		{
			if (_PrinterSettings == null)
			{
				_PrinterSettings = new PrinterSettings();
				return _PrinterSettings;
			}
			return _PrinterSettings;
		}
	}

	public Margins Margins
	{
		get
		{
			if (_Margins == null)
			{
				_Margins = new Margins(5, 5, 5, 5);
				return _Margins;
			}
			return _Margins;
		}
		set
		{
			_Margins = value;
		}
	}

	public GraphicsUnit GraphicsUnit
	{
		get
		{
			return _GraphicsUnit;
		}
		set
		{
			_GraphicsUnit = value;
		}
	}

	public List<ReportCell> Cells
	{
		get
		{
			if (_CellsRow == null)
			{
				_CellsRow = new List<ReportCell>();
				return _CellsRow;
			}
			return _CellsRow;
		}
	}

	private List<List<ReportCell>> ListOfCellsRow
	{
		get
		{
			if (_ListOfCellsRow == null)
			{
				_ListOfCellsRow = new List<List<ReportCell>>();
				return _ListOfCellsRow;
			}
			return _ListOfCellsRow;
		}
	}

	public static FastPrint Instance
	{
		get
		{
			if (instance == null)
			{
				lock (syncRoot)
				{
					if (instance == null)
					{
						instance = new FastPrint();
					}
				}
			}
			return instance;
		}
	}

	private FastPrint()
	{
		p = new PrintDocument();
		p.PrinterSettings = PrinterSettings;
		_GraphicsUnit = GraphicsUnit.Millimeter;
	}

	public void AcceptChanges()
	{
		ListOfCellsRow.Add(Cells);
		_CellsRow = new List<ReportCell>();
	}

	public void AddTextCell(string Text, Font Font, float WidthRatio, float CellHight, StringAlignment StringAlignment = StringAlignment.Center, Color? TextColor = null, bool DrawRectangle = true, Color? LineColor = null, float LineWidth = 0.1f)
	{
		try
		{
			ReportCell item = default(ReportCell);
			item.CellHight = CellHight;
			item.CellType = CellType.Text;
			item.DrawRectangle = DrawRectangle;
			item.Font = Font;
			item.Image = null;
			item.LineColor = LineColor ?? Color.Black;
			item.LineWidth = LineWidth;
			item.StringAlignment = StringAlignment;
			item.Text = Text;
			item.TextColor = TextColor ?? Color.Black;
			item.WidthRatio = WidthRatio;
			Cells.Add(item);
		}
		catch (Exception innerException)
		{
			throw new Exception("Exception occurred in AddTextCell", innerException);
		}
	}

	private float getImageHight(float HeightinPixels, float Resolution, GraphicsUnit gr)
	{
		if (HeightinPixels == 0f || Resolution == 0f)
		{
			return 0f;
		}
		return gr switch
		{
			GraphicsUnit.Pixel => HeightinPixels, 
			GraphicsUnit.Millimeter => HeightinPixels / Resolution * 25.4f, 
			GraphicsUnit.Inch => HeightinPixels / Resolution, 
			_ => 0f, 
		};
	}

	public void AddImageCell(Image Image, float WidthRatio, float? CellHight = null, bool DrawRectangle = true, Color? LineColor = null, float LineWidth = 0.1f)
	{
		try
		{
			ReportCell item = default(ReportCell);
			item.CellHight = CellHight ?? getImageHight(Image.Height, Image.VerticalResolution, GraphicsUnit);
			item.CellType = CellType.Image;
			item.DrawRectangle = DrawRectangle;
			item.Font = null;
			item.Image = Image;
			item.LineColor = LineColor ?? Color.Black;
			item.LineWidth = LineWidth;
			item.StringAlignment = StringAlignment.Center;
			item.Text = string.Empty;
			item.TextColor = Color.Black;
			item.WidthRatio = WidthRatio;
			Cells.Add(item);
		}
		catch (Exception innerException)
		{
			throw new Exception("Exception occurred in AddImageCell", innerException);
		}
	}

	public void AddLineCell(float WidthRatio, float CellHight, bool DrawRectangle = true, Color? LineColor = null, float LineWidth = 0.1f)
	{
		try
		{
			ReportCell item = default(ReportCell);
			item.CellHight = CellHight;
			item.CellType = CellType.Line;
			item.DrawRectangle = DrawRectangle;
			item.Font = null;
			item.Image = null;
			item.LineColor = LineColor ?? Color.Black;
			item.LineWidth = LineWidth;
			item.StringAlignment = StringAlignment.Center;
			item.Text = string.Empty;
			item.TextColor = Color.Black;
			item.WidthRatio = WidthRatio;
			Cells.Add(item);
		}
		catch (Exception innerException)
		{
			throw new Exception("Exception occurred in AddLineCell", innerException);
		}
	}

	public void AddEmptyCell(float WidthRatio, float CellHight, bool DrawRectangle = true, Color? LineColor = null, float LineWidth = 0.1f)
	{
		try
		{
			ReportCell item = default(ReportCell);
			item.CellHight = CellHight;
			item.CellType = CellType.Empty;
			item.DrawRectangle = DrawRectangle;
			item.Font = null;
			item.Image = null;
			item.LineColor = LineColor ?? Color.Black;
			item.LineWidth = LineWidth;
			item.StringAlignment = StringAlignment.Center;
			item.Text = string.Empty;
			item.TextColor = Color.Black;
			item.WidthRatio = WidthRatio;
			Cells.Add(item);
		}
		catch (Exception innerException)
		{
			throw new Exception("Exception occurred in AddEmptyCell", innerException);
		}
	}

	private bool IsArabic(string s)
	{
		return Regex.IsMatch(s, "\\p{IsArabic}");
	}

	public void Print(bool ResetCells = true)
	{
		p.DefaultPageSettings.Margins = Margins;
		p.OriginAtMargins = false;
		p.PrintPage += delegate(object psender, PrintPageEventArgs pe)
		{
			pe.Graphics.PageUnit = GraphicsUnit;
			pe.PageSettings.Margins = Margins;
			float num = Margins.Top;
			foreach (List<ReportCell> item in ListOfCellsRow)
			{
				float num2 = Margins.Left;
				float num3 = 0f;
				foreach (ReportCell item2 in item)
				{
					float num4 = OverallWidth * item2.WidthRatio;
					RectangleF rectangleF = new RectangleF(num2, num, num4, item2.CellHight);
					if (item2.CellType == CellType.Text)
					{
						StringFormat stringFormat = new StringFormat
						{
							Alignment = item2.StringAlignment,
							LineAlignment = StringAlignment.Center
						};
						if (IsArabic(item2.Text))
						{
							stringFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
						}
						pe.Graphics.DrawString(item2.Text, item2.Font, new SolidBrush(item2.TextColor), rectangleF, stringFormat);
					}
					else if (item2.CellType == CellType.Line)
					{
						pe.Graphics.DrawLine(new Pen(item2.LineColor, item2.LineWidth), num2, num + item2.CellHight / 2f, num2 + num4, num + item2.CellHight / 2f);
					}
					else if (item2.CellType == CellType.Image)
					{
						pe.Graphics.DrawImage(item2.Image, rectangleF);
					}
					else if (item2.CellType != CellType.Empty)
					{
					}
					if (item2.DrawRectangle)
					{
						pe.Graphics.DrawRectangle(new Pen(item2.LineColor, item2.LineWidth), rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height);
					}
					num2 += num4;
					num3 = ((num3 < item2.CellHight) ? item2.CellHight : num3);
				}
				num += num3;
			}
		};
		try
		{
			p.Print();
		}
		catch (Exception innerException)
		{
			throw new Exception("Exception occurred while printing", innerException);
		}
		finally
		{
			if (ResetCells)
			{
				ListOfCellsRow.Clear();
			}
		}
	}

	public void ResetCells()
	{
		try
		{
			ListOfCellsRow.Clear();
		}
		catch (Exception innerException)
		{
			throw new Exception("Exception occurred in ResetCells", innerException);
		}
	}
}
