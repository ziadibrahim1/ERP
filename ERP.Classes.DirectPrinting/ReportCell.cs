using System.Drawing;

namespace ERP.Classes.DirectPrinting;

public struct ReportCell
{
	public string Text;

	public Image Image;

	public float WidthRatio;

	public bool DrawRectangle;

	public Font Font;

	public StringAlignment StringAlignment;

	public float CellHight;

	public float LineWidth;

	public Color LineColor;

	public Color TextColor;

	public CellType CellType;
}
