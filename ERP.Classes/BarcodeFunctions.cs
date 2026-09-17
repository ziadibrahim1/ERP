using System.Collections;

namespace ERP.Classes;

internal class BarcodeFunctions
{
	public static ArrayList BarcodeSubstring(string Barcode)
	{
		ArrayList arrayList = new ArrayList();
		arrayList.Add("");
		arrayList.Add("1");
		arrayList.Add("1");
		arrayList.Add("");
		arrayList.Add("1");
		if (GlobalFunctions.GetOption("UseScaleBarCode") && Barcode.Length == 13 && Barcode.StartsWith("99"))
		{
			arrayList[0] = Barcode.Substring(2, 5);
			arrayList[4] = Barcode.Substring(7, 5);
		}
		else
		{
			char value = char.Parse(GlobalFunctions.GetDefault("BarcodeQuantitySeparator"));
			char value2 = char.Parse(GlobalFunctions.GetDefault("BarcodeBatchNoSeparator"));
			char value3 = char.Parse(GlobalFunctions.GetDefault("BarcodeSizeSeparator"));
			char value4 = char.Parse(GlobalFunctions.GetDefault("BarcodeColorSeparator"));
			if (Barcode.IndexOf("+", 0) > -1)
			{
				Barcode = Barcode.Replace("+A", "a").Replace("+B", "b").Replace("+C", "c")
					.Replace("+D", "d")
					.Replace("+E", "e");
				Barcode = Barcode.Replace("+F", "f").Replace("+G", "g").Replace("+H", "h")
					.Replace("+I", "i")
					.Replace("+J", "j");
				Barcode = Barcode.Replace("+K", "k").Replace("+L", "l").Replace("+M", "m")
					.Replace("+N", "n")
					.Replace("+O", "o");
				Barcode = Barcode.Replace("+P", "p").Replace("+Q", "q").Replace("+R", "r")
					.Replace("+S", "s")
					.Replace("+T", "t");
				Barcode = Barcode.Replace("+U", "u").Replace("+V", "v").Replace("+W", "w")
					.Replace("+X", "x")
					.Replace("+Y", "y")
					.Replace("+Z", "z");
			}
			Barcode = Barcode.Replace("+", "");
			int num = Barcode.Length - 1;
			if (Barcode.IndexOf(value, 0) > 0)
			{
				arrayList[4] = Barcode.Substring(Barcode.IndexOf(value, 0) + 1, Barcode.Length - Barcode.IndexOf(value, 0) - 1);
				if (arrayList[4].ToString() == "")
				{
					arrayList[4] = 1;
				}
				num = Barcode.IndexOf(value, 0) - 1;
			}
			if (Barcode.IndexOf(value2, 0) > 0)
			{
				arrayList[3] = Barcode.Substring(Barcode.IndexOf(value2, 0) + 1, num - Barcode.IndexOf(value2, 0));
				num = Barcode.IndexOf(value2, 0) - 1;
			}
			if (Barcode.IndexOf(value3, 0) > 0)
			{
				arrayList[2] = Barcode.Substring(Barcode.IndexOf(value3, 0) + 1, num - Barcode.IndexOf(value3, 0));
				if (arrayList[2].ToString() == "")
				{
					arrayList[2] = 1;
				}
				num = Barcode.IndexOf(value3, 0) - 1;
			}
			if (Barcode.IndexOf(value4, 0) > 0)
			{
				arrayList[1] = Barcode.Substring(Barcode.IndexOf(value4, 0) + 1, num - Barcode.IndexOf(value4, 0));
				if (arrayList[1].ToString() == "")
				{
					arrayList[1] = 1;
				}
				num = Barcode.IndexOf(value4, 0) - 1;
			}
			arrayList[0] = Barcode.Substring(0, num + 1);
		}
		return arrayList;
	}
}
