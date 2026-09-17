using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ERP.Classes;

public class ImageFunctions
{
	public static Image BinaryToImage(byte[] p)
	{
		MemoryStream stream = new MemoryStream(p);
		return Image.FromStream(stream);
	}

	public static Image ScaleByPercentage(Image img, double Sizepercent)
	{
		double num = Sizepercent / 100.0;
		int outputWidth = (int)((double)img.Width * num);
		int outputHeight = (int)((double)img.Height * num);
		return ScaleImage(img, outputWidth, outputHeight);
	}

	public static Image ScaleImage(Image img, int outputWidth)
	{
		double num = (double)outputWidth / (double)img.Width;
		int outputHeight = (int)((double)img.Height * num);
		return ScaleImage(img, outputWidth, outputHeight);
	}

	public static Image ScaleImage(Image img, int outputWidth, int outputHeight)
	{
		Bitmap bitmap = new Bitmap(outputWidth, outputHeight, img.PixelFormat);
		bitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);
		Graphics graphics = Graphics.FromImage(bitmap);
		graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		graphics.DrawImage(img, new Rectangle(0, 0, outputWidth, outputHeight), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
		graphics.Dispose();
		return bitmap;
	}
}
