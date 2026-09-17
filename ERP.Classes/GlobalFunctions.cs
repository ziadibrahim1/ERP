using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.Defaults;
using BusinessLayer.General;
using BusinessLayer.Privilege;
using BusinessLayer.SMS;
using BusinessLayer.StockControl;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.AbstractForms;
using ERP.Accounting.Transactions;
using ERP.CnsProjects.Transactions;
using ERP.Company;
using ERP.Constructions.Transactions;
using ERP.Export.Transactions;
using ERP.HR.Payroll.Transactions;
using ERP.Lenses.Transactions;
using ERP.MarineService.Transactions;
using ERP.Photos.Transactions;
using ERP.Production.Transactions;
using ERP.Purchasing.Transactions;
using ERP.SafesAndBanks.BankTransactions;
using ERP.SafesAndBanks.SafeTransactions;
using ERP.Sales.Transactions;
using ERP.StockControl.Slicing;
using ERP.StockControl.Transactions;
using ERP.WareHouse.Transactions;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic.FileIO;
using ZXing;
using ZXing.QrCode;

namespace ERP.Classes;

public class GlobalFunctions
{
	private static class NativeMethods
	{
		public enum WTS_INFO_CLASS
		{
			WTSClientName = 10
		}

		public static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

		public const int WTS_CURRENT_SESSION = -1;

		[DllImport("Wtsapi32.dll", CharSet = CharSet.Unicode)]
		public static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out IntPtr ppBuffer, out int pBytesReturned);

		[DllImport("wtsapi32.dll", ExactSpelling = true)]
		public static extern void WTSFreeMemory(IntPtr memory);
	}

	private static string Code = "b&->p/ksIOPLKt^5g78w}1f2)QWERTX{a3%9=*[z0+!JHGFDSA?o@#lm4x$_CVB~ NMd(yqei|jcr]h6.YUZ,<n\\";

	public static bool SyncMasterData(string FormName)
	{
		try
		{
			string text = "";
			SynchronizationP synchronizationP = new SynchronizationP();
			synchronizationP.FormName = FormName;
			synchronizationP.ServerR = GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString();
			synchronizationP.DataBaseR = GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString();
			synchronizationP.UserIDR = GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString();
			synchronizationP.PasswordR = GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString();
			text = GlobalVariables.dtSyncConn.Rows[0]["SyncBranchID"].ToString();
			if (!synchronizationP.CreateConnectionL() || !synchronizationP.CreateConnectionR())
			{
				return false;
			}
			synchronizationP.MoveDataL(text);
			synchronizationP.MoveDataR(text);
			synchronizationP.DeleteFromRemote(text);
			synchronizationP.DeleteFromLocal(text);
			DataTable dataTable = synchronizationP.ExecuteQuery_DataTableLocal(" select * From Sync_Tables t order by SyncOrder ");
			DataTable dataTable2 = synchronizationP.ExecuteQuery_DataTableLocal(" select * From Sync_Tables t where t.TableName in(select Distinct TableName from Sync_Transactions where BranchID=" + text + " AND State<>3 )  order by SyncOrder ");
			DataTable dataTable3 = synchronizationP.ExecuteQuery_DataTableRemote(" select * From Sync_Tables t where t.TableName in(select Distinct TableName from Sync_Transactions where BranchID=" + text + " AND State<>3 )  order by SyncOrder ");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text2 = dataTable.Rows[i]["TableName"].ToString();
				if (dataTable2.Select("TableName='" + text2 + "'").Length != 0 && Convert.ToBoolean(dataTable.Rows[i]["ToServer"]))
				{
					synchronizationP.InsertUpdateToRemote(text2, text);
				}
				if (dataTable3.Select("TableName='" + text2 + "'").Length != 0 && Convert.ToBoolean(dataTable.Rows[i]["FromServer"]))
				{
					synchronizationP.InsertUpdateToLocal(text2, text);
				}
			}
			synchronizationP.ActualDeleteFromRemote();
			synchronizationP.ActualDeleteFromLocal(text);
		}
		catch
		{
			return false;
		}
		return true;
	}

	public static void PrepareGrid(UltraGrid ULGData)
	{
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Expected O, but got Unknown
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowAddNew = (AllowAddNew)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.AllowMultiCellOperations = (AllowMultiCellOperation)4;
		((UltraGridBase)ULGData).DisplayLayout.Appearance.BackColor = Color.Transparent;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.CellAppearance.TextHAlign = (HAlign)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.HeaderAppearance.FontData.Bold = (DefaultableBoolean)1;
		((UltraGridBase)ULGData).DisplayLayout.Override.HeaderAppearance.TextHAlign = (HAlign)2;
		((UltraGridBase)ULGData).DisplayLayout.Override.HeaderAppearance.TextVAlign = (VAlign)2;
		((UltraGridBase)ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		((UltraGridBase)ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectorNumberStyle = (RowSelectorNumberStyle)3;
		((UltraGridBase)ULGData).DisplayLayout.Override.RowSelectorWidth = 30;
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands).Count; i++)
		{
			for (int j = 0; j < ((DisposableObjectCollectionBase)((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns).Count; j++)
			{
				((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns[j].Hidden = true;
				((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns[j].AllowRowFiltering = (DefaultableBoolean)1;
				if (((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns[j].DataType == typeof(DateTime))
				{
					UltraDateTimeEditor val = new UltraDateTimeEditor();
					val.MaskInput = "dd/mm/yyyy";
					((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns[j].EditorComponent = (Component)(object)val;
				}
				else if (((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns[j].DataType == typeof(decimal))
				{
					((UltraGridBase)ULGData).DisplayLayout.Bands[i].Columns[j].Format = "###,##.00";
				}
			}
		}
	}

	public static void PrepareDropDown(UltraDropDown UDD)
	{
		for (int i = 0; i < ((DisposableObjectCollectionBase)((UltraGridBase)UDD).DisplayLayout.Bands[0].Columns).Count; i++)
		{
			((UltraGridBase)UDD).DisplayLayout.Bands[0].Columns[i].Hidden = true;
		}
	}

	public static int IndexOf(DataTable dt, string ID)
	{
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			if (dt.Rows[i][0].ToString().Trim() == ID.Trim())
			{
				return i;
			}
		}
		return -1;
	}

	public static void FillUltraDropDown(UltraDropDown MyControl, string strSQL, string Key, string Display)
	{
		DataTable dataSource = Main.ExecuteQuery_DataTable(strSQL);
		((UltraGridBase)MyControl).DataSource = dataSource;
		((UltraDropDownBase)MyControl).DisplayMember = Display;
		((UltraDropDownBase)MyControl).ValueMember = Key;
		PrepareDropDown(MyControl);
		((UltraGridBase)MyControl).DisplayLayout.Bands[0].Columns[Display].Hidden = false;
		((HeaderBase)((UltraGridBase)MyControl).DisplayLayout.Bands[0].Columns[Display].Header).Caption = "";
	}

	public static void FillCombo(UltraComboEditor MyControl, DataTable dt, string ValueMember, string DisplayMember)
	{
		MyControl.DataSource = dt;
		MyControl.DisplayMember = DisplayMember;
		MyControl.ValueMember = ValueMember;
	}

	public static void FillComboWithSP(UltraComboEditor MyControl, string strSQL, string ValueMember, string DisplayMember)
	{
		DataTable dataSource = Main.ExecuteQuery_DataTable(strSQL);
		MyControl.DataSource = dataSource;
		MyControl.DisplayMember = DisplayMember;
		MyControl.ValueMember = ValueMember;
	}

	public static void GetCompanies()
	{
		GlobalVariables.ResultTable = Main.ExecuteQuery_DataTable("select Substring(name,5,LEN(name)) as name from master.dbo.sysdatabases where Rtrim(Substring(name,1,4)) = 'ERP_' Order By name");
	}

	public static void CheckForNumbers(UltraGridCell activeCell, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\u0003' && e.KeyChar != '\u0016')
		{
			string text = ".0123456789";
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
			if (activeCell != null && activeCell.Text.Contains(".") && e.KeyChar != '\b' && e.KeyChar == '.')
			{
				e.Handled = true;
			}
		}
	}

	public static void CheckForNumbersNegative(UltraGridCell activeCell, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\u0003' || e.KeyChar == '\u0016')
		{
			return;
		}
		string text = ".0123456789";
		if ((e.KeyChar != '-' || activeCell.Text.Length != 0) && activeCell.EditorResolved.SelectedText.Length != activeCell.Text.Length)
		{
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
			if (activeCell != null && activeCell.Text.Contains(".") && e.KeyChar != '\b' && e.KeyChar == '.')
			{
				e.Handled = true;
			}
		}
	}

	public static void CheckForNumbers(object sender, KeyPressEventArgs e)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		if (e.KeyChar != '\u0003')
		{
			string text = ".0123456789";
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
			if (((Control)(UltraTextEditor)sender).Text.Contains(".") && e.KeyChar != '\b' && e.KeyChar == '.')
			{
				e.Handled = true;
			}
		}
	}

	public static void CheckForNumbersNegative(object sender, KeyPressEventArgs e)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Expected O, but got Unknown
		if (e.KeyChar == '\u0003')
		{
			return;
		}
		string text = ".0123456789";
		if (e.KeyChar != '-' || (((Control)(UltraTextEditor)sender).Text.Length != 0 && ((Control)(UltraTextEditor)sender).Text.Length != ((TextEditorControlBase)(UltraTextEditor)sender).SelectionLength))
		{
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
			if (((Control)(UltraTextEditor)sender).Text.Contains(".") && e.KeyChar != '\b' && e.KeyChar == '.')
			{
				e.Handled = true;
			}
		}
	}

	public static void CheckForIntegers(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar != '\u0003')
		{
			string text = "0123456789";
			if (text.IndexOf(e.KeyChar) == -1 && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}
	}

	public static byte[] ImageToBinary(Image Photo)
	{
		using Bitmap bitmap = new Bitmap(Photo);
		MemoryStream memoryStream = new MemoryStream();
		bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
		return memoryStream.ToArray();
	}

	public static Image BinaryToImage(byte[] B)
	{
		MemoryStream memoryStream = new MemoryStream(B);
		Stream stream = memoryStream;
		Bitmap result = new Bitmap(stream);
		stream.Close();
		return result;
	}

	public static byte[] ImageToBinaryPNG(Image imageIn)
	{
		MemoryStream memoryStream = new MemoryStream();
		imageIn.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
		return memoryStream.ToArray();
	}

	public static Image BinaryToImagePNG(byte[] byteArrayIn)
	{
		MemoryStream stream = new MemoryStream(byteArrayIn);
		return Image.FromStream(stream);
	}

	public static byte[] ObjectToByteArray(object obj)
	{
		if (obj == null)
		{
			return null;
		}
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using MemoryStream memoryStream = new MemoryStream();
		binaryFormatter.Serialize(memoryStream, obj);
		return memoryStream.ToArray();
	}

	public static string GetFileTypeByBase64(string base64)
	{
		string text = base64.Substring(0, 5);
		switch (text.ToUpper())
		{
		case "IVBOR":
			return ".png";
		case "/9J/4":
			return ".jpg";
		case "AAAAI":
			return ".mp4";
		case "JVBER":
			return ".pdf";
		case "AAABA":
			return ".ico";
		case "UMFYI":
			return ".rar";
		case "E1XYD":
			return ".rtf";
		case "U1PKC":
			return ".txt";
		case "MQOWM":
		case "77U/M":
			return ".srt";
		default:
			return "." + text;
		}
	}

	public static void AddLogin(string UserName, string Password)
	{
		Main.ExecuteNonQuery(string.Concat("sp_AddLogin '" + UserName + "','", "',[", GlobalVariables.DatabaseName, "]"));
		Main.ExecuteNonQuery("exec sp_addsrvrolemember N'" + UserName + "', sysadmin");
	}

	public static void AddDBUser(string UserName)
	{
		Main.ExecuteNonQuery("sp_GrantDBAccess N'" + UserName + "',N'" + UserName + "'");
		Main.ExecuteNonQuery("sp_AddRoleMember N'db_owner',N'" + UserName + "'");
	}

	public static void DeleteLogin(string UserName)
	{
		Main.ExecuteNonQuery("sp_RevokeDBAccess N'" + UserName + "'");
		Main.ExecuteNonQuery("exec sp_dropsrvrolemember N'" + UserName + "', sysadmin");
		Thread.Sleep(50);
		Main.ExecuteNonQuery("sp_DropLogIn N'" + UserName + "'");
	}

	public static string EncodeText(string toEncode)
	{
		Random random = new Random();
		int num = random.Next(0, Code.Length - 2);
		char[] array = toEncode.ToCharArray();
		int num2 = -1;
		string text = Code[num].ToString();
		for (int i = 0; i < array.Length; i++)
		{
			num2 = Code.IndexOf(array[i]);
			if (num2 != -1)
			{
				array[i] = Code[(num2 + num) % Code.Length];
			}
			text += array[i];
		}
		byte[] bytes = Encoding.ASCII.GetBytes(Code.Substring(random.Next(0, Code.Length - 4), 3) + text + Code.Substring(random.Next(0, Code.Length - 4), 3));
		string text2 = Convert.ToBase64String(bytes);
		int length = text2.Length;
		for (int j = 0; j < 2 * length; j += 2)
		{
			text2 = text2.Insert(j, Code[random.Next(0, Code.Length - 1)].ToString());
		}
		bytes = Encoding.ASCII.GetBytes(text2);
		return Convert.ToBase64String(bytes);
	}

	public static string DecodeText(string encodedData)
	{
		byte[] bytes = Convert.FromBase64String(encodedData);
		string text = Encoding.ASCII.GetString(bytes);
		string text2 = "";
		for (int i = 0; i < text.Length; i += 2)
		{
			text2 += text[i + 1];
		}
		bytes = Convert.FromBase64String(text2);
		text2 = Encoding.ASCII.GetString(bytes);
		text2 = text2.Substring(3, text2.Length - 6);
		int num = Code.IndexOf(text2[0]);
		char[] array = text2.Remove(0, 1).ToCharArray();
		int num2 = -1;
		string text3 = "";
		for (int j = 0; j < array.Length; j++)
		{
			num2 = Code.IndexOf(array[j]);
			if (num2 != -1)
			{
				array[j] = Code[(num2 + Code.Length - num) % Code.Length];
			}
			text3 += array[j];
		}
		return text3;
	}

	public static void PrepareSystemModulesData()
	{
		string text = "";
		text += " exec SystemModules_Insert 26,'إدارة العملاء','CRM',0;\r\n ";
		text += " exec SystemModules_Insert 27,'الفواتير الإلكترونية','EInvoices',0;\r\n ";
		text += " exec SystemModules_Insert 60,'العيادات','Clinics',0;\r\n ";
		text += " exec SystemModules_Insert 61,'التسويق العقارى','Constructions',0;\r\n ";
		text += " exec SystemModules_Insert 62,'البصريات','LensesLab',0;\r\n ";
		text += " exec SystemModules_Insert 63,'الرسائل','SMS',0;\r\n ";
		text += " exec SystemModules_Insert 80,'خدمات بحرية','MarineService',0;\r\n ";
		text += " exec SystemModules_Insert 81,'المقاولات','CnsProjects',0;\r\n ";
		text += " exec SystemModules_Insert 82,'اشعارات','Alerts',0;\r\n ";
		text += " exec SystemModules_Insert 83,'شحن الصادر','Export',0;\r\n ";
		text += " exec SystemModules_Insert 84,'Sling','Sling',0;\r\n ";
		text += " exec SystemModules_Insert 12,'الاصول الثابتة','FixedAssets',0;\r\n ";
		text += " exec SystemModules_Insert 25,'التشريح','Slicing',0;\r\n ";
		text += " exec SystemModules_Insert 85,'تخليص جمركى','CustomsClearence',0;\r\n ";
		text += " exec SystemModules_Insert 86,'تطبيق البيع المباشر','DirectSalesApp',0;\r\n ";
		text += " exec SystemModules_Insert 87,'تطبيق التقارير','ReportsMobileApp',0;\r\n ";
		text += " exec SystemModules_Insert 88,'تطبيق التخليص الجمركي','CustomsClearanceMobileApp',0;\r\n ";
		Main.ExecuteNonQuery(text);
	}

	public static void PrepareStaticData()
	{
		string text = "";
		if (GlobalVariables.UserID != "1")
		{
			GlobalVariables.VersionID = string.Concat(20230118);
			text = text + "exec SystemVersions_Insert " + GlobalVariables.VersionID + ",'V " + GlobalVariables.VersionID + "'";
		}
		text += "exec SystemOptions_Insert 004,'CanEditVoucherNumber','يمكن تعديل رقم المسلسل للحركات',1,0,'يمكن تعديل رقم المسلسل للحركات';\r\n";
		text += "exec SystemOptions_Insert 005,'CentralSMS','الرسائل التليفونيه مركزيه',1,0,'الرسائل التليفونيه مركزيه';\r\n";
		text += "exec SystemOptions_Insert 006,'ArchivingInEnglish','الارشفه بالإنجليزيه',1,1,'الارشفه بالإنجليزيه';\r\n";
		text += "exec SystemOptions_Insert 007,'AllowRemoteConnction','Allow Remote Desktop Connction',0,1,'Allow Remote Desktop Connction';\r\n";
		text += "exec SystemOptions_Insert 008,'UsingElectronicInvoice','UsingElectronicInvoice',0,1,'UsingElectronicInvoice';\r\n";
		text += "exec SystemOptions_Insert 009,'ElectronicInvoiceProductionEnvirnoment','ElectronicInvoiceProductionEnvirnoment',0,1,'ElectronicInvoiceProductionEnvirnoment';\r\n";
		text += "exec SystemOptions_Insert 120,'AutoGenerateClientTransferJV','إنشاء قيود تحويلات العملاء اوتوماتيكيا  ',0,1,'إنشاء قيود تحويلات العملاء اوتوماتيكيا  ';\r\n";
		text += "exec SystemOptions_Insert 121,'ForeignCurrencySettlementsBySameCurrency','تسوية العملات الاجنبية بنفس العملة  ',0,1,'تسوية العملات الاجنبية بنفس العملة  ';\r\n";
		text += "exec SystemOptions_Insert 122,'GenerateContractTotalJVInRealState','إنشاء قيد إستحقاق إجمالى عند التعاقد فى التسويق العقارى',0,1,'إنشاء قيد إستحقاق إجمالى عند التعاقد فى التسويق العقارى';\r\n";
		text += "exec SystemOptions_Insert 123,'AutoDeliverStoreTransferForSameBranch','إستلام التحويلات تلقائى لنفس الفرع',1,1,'إستلام التحويلات تلقائى لنفس الفرع';\r\n";
		text += "exec SystemOptions_Insert 212,'UsingItemsColors','إستخدام الالوان فى الاصناف',0,1,'إستخدام الالوان فى الاصناف ';\r\n";
		text += "exec SystemOptions_Insert 213,'UsingItemsSizes','إستخدام المقاسات فى الاصناف',0,1,'إستخدام المقاسات فى الاصناف ';\r\n";
		text += "exec SystemOptions_Insert 214,'CreateMaterialIssueVoucherFromSalesInvoiceEnforceCreation','إنشاء إذن صرف من فاتورة المبيعات إجبارى',0,0,'إنشاءإذن صرف من فاتورة المبيعات إجبارى';\r\n";
		text += "exec SystemOptions_Insert 215,'CreateGoodReceiptNoteFromPurchaseInvoiceEnforceCreation','إنشاء إذن إضافة من فاتورة المشتريات إجبارى',0,0,'إنشاء إذن إضافة من فاتورة المشتريات إجبارى';\r\n";
		text += "exec SystemOptions_Insert 216,'UseScaleBarCode','استخدام باركود الميزان',0,0,'استخدام باركود الميزان';\r\n";
		text += "exec SystemOptions_Insert 217,'Production_SerialByProductionLine','سريل امر التشغيل بكود الخط',0,0,'سريل امر التشغيل بكود الخط';\r\n";
		text += "exec SystemOptions_Insert 218,'SelectLensesItems','إختيار أصناف العدسات',0,1,'إختيار أصناف العدسات ';\r\n";
		text += "exec SystemOptions_Insert 219,'SelectLensesItemsXY','إختيار أصناف العدسات XY',0,1,'إختيار أصناف العدسات XY ';\r\n";
		text += "exec SystemOptions_Insert 220,'GenerateBarCodeSerial','باركود تسلسلى ',0,1,'باركود تسلسلى ';\r\n";
		text += "exec SystemOptions_Insert 221,'PurchaseItemsAuditAlert','إشعار مراجعة الصنف فى فاتورة المشتريات ',0,0,'إشعار مراجعة الصنف فى فاتورة المشتريات ';\r\n";
		text += "exec SystemOptions_Insert 222,'GoodReceiptNoteItemsAuditAlert','إشعار مراجعة الصنف فى إذن الإضافة ',0,0,'إشعار مراجعة الصنف فى إذن الإضافة ';\r\n";
		text += "exec SystemOptions_Insert 223,'StoresSettlementReplaceInSameItemGroup','تسوية المخازن إستبدال فى نفس مجموعة الاصناف ',0,0,'تسوية المخازن إستبدال فى نفس مجموعة الاصناف ';\r\n";
		text += "exec SystemOptions_Insert 224,'ClosePurchaseOrderAfterPurchaseInvoice','إغلاق امر الشراء بعد الفاتورة',1,0,'إغلاق امر الشراء بعد الفاتورة';\r\n";
		text += "exec SystemOptions_Insert 225,'ShowBatchNoInPurchaseOrder','إظهار السريل في أمر الشراء',0,0,'إظهار السريل في أمر الشراء';\r\n";
		text += "exec SystemOptions_Insert 226,'Lab_SerialByBranch','سريل فواتير البصريات  لكل فرع',0,0,'سريل فواتير البصريات لكل فرع ';\r\n";
		text += "exec SystemOptions_Insert 301,'IgnoreAttendanceMachineInOutValue','تجاهل حالة الدخول والخروج من ماكينه البصمه',0,1,'تجاهل حالة الدخول والخروج من ماكينه البصمه ';\r\n";
		text += "exec SystemOptions_Insert 302,'SalaryAutoGenerateJV','عمل قيد المرتبات أوتوماتيكيا',0,0,'عمل قيد المرتبات أوتوماتيكيا ';\r\n";
		text += "exec SystemOptions_Insert 303,'CalculateAbsentPenaltyDependsOnInPenaltyItemsInsteadeOfInAbsentItems','إحتساب جزاء الغياب علي عناصر الجزاء بدلا من عناصر الغياب',0,0,'إحتساب جزاء الغياب علي عناصر الجزاء بدلا من عناصر الغياب ';\r\n";
		text += "exec SystemAccount_Insert 104,'ClientTransferAccount','حساب تحويلات العملاء',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 105,'BranchDebitTransferAccount','حساب تحويل مديونية بين الفروع',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 106,'POSRoundingValuesAccount','حساب فروق تقريب بيع مباشر',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 107,'ForeignCurrencySettlementAccount','حساب تسوية عملات اجنبية ',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 108,'SupplierDebitNoteAccount','حساب إشعار خصم مورد',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 208,'DiscountTaxAccount-Purchase','حساب ضريبة الخصم-مشتريات',Null,1,Null ;\r\n";
		text += "exec SystemAccount_Insert 210,'AddTaxAccount-Purchase','حساب ضريبة الاضافة-مشتريات',Null,1,Null ;\r\n";
		text += "exec SystemAccount_Insert 219,'ServiceChargeAccount','حساب رسم الخدمة',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 220,'DeliveryChargeAccount','حساب رسم التوصيل',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 221,'ConstractionInsurenseAccount','حساب تأمين اعمال المقاولات',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 222,'WorkerInsurenseAccount','حساب تأمينات إجتماعيه',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 223,'DownPaymentAccount','حساب عملاء دفعات مقدمه',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 224,'DiscountTaxAccount-Sales','حساب ضريبة الخصم-مبيعات',Null,1,Null ;\r\n";
		text += "exec SystemAccount_Insert 225,'AddTaxAccount-Sales','حساب ضريبة الاضافة-مبيعات',Null,1,Null ;\r\n";
		text += "exec SystemAccount_Insert 226,'CommercialTaxAccount','حساب ضريبة الارباح التجارية و الصناعية',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 227,'GrowthFeesAccount','حساب رسم التنمية',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 228,'LabDiscountReturnsAccount','حساب خصم مردودات المعامل',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 229,'ProductionFeesAccount','حساب اجور الانتاج',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 301,'SalaryExpenseAccount','حساب مصروف الإجور',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 302,'AccruedSalaryAccount','حساب الاجور المستحقة',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 303,'IncomeTaxAccount','حساب ضرائب الدخل',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 304,'SocialInssuranceAccount','حساب التأمينات الاجتماعية ',Null,0,Null ;\r\n";
		text += "exec SystemAccount_Insert 305,'SalaryAdvanceAccount','حساب السلف ',Null,0,Null ;\r\n";
		text += "exec SystemDefaults_Insert 14,'QuantityDecimals','عدد كسور الكميه',3;\r\n";
		text += "exec SystemDefaults_Insert 15,'BarcodeColorSeparator','BarcodeColorSeparator',C;\r\n";
		text += "exec SystemDefaults_Insert 16,'BarcodeSizeSeparator','BarcodeSizeSeparator',S;\r\n";
		text += "exec SystemDefaults_Insert 17,'BarcodeBatchNoSeparator','BarcodeBatchNoSeparator',B;\r\n";
		text += "exec SystemDefaults_Insert 18,'BarcodeQuantitySeparator','BarcodeQuantitySeparator',Q;\r\n";
		text += "exec SystemDefaults_Insert 19,'MostUseFormsCount','MostUseFormsCount',10;\r\n";
		text += "exec SystemDefaults_Insert 20,'RecentFormsCount','RecentFormsCount',10;\r\n";
		text += "exec SystemDefaults_Insert 21,'TruckScalePort','TruckScalePort','';\r\n";
		text += "exec SystemDefaults_Insert 22,'ArchivingPath','مسار الارشيف','';\r\n";
		text += "exec SystemDefaults_Insert 23,'ReportZoomFactor','ReportZoomFactor',120;\r\n";
		text += "exec SystemDefaults_Insert 24,'StopCalculateStockCostFrom','إيقاف حساب تكلفة المخزن من',0;\r\n";
		text += "exec SystemDefaults_Insert 25,'StopCalculateStockCostTo','إيقاف حساب تكلفة المخزن الي',0;\r\n";
		text += "exec SystemDefaults_Insert 26,'ReportPathRemote','مسار التقاريرالخارجي','';\r\n";
		text += "exec SystemDefaults_Insert 27,'ReportPathLocal','مسار التقارير المحلي','';\r\n";
		text += "exec SystemDefaults_Insert 28,'StoreTransferReceivePeriod','فترة سماح استلام التحويلات','1';\r\n";
		text += "exec SystemDefaults_Insert 29,'AddedTaxPercentage','نسبة ضريبة الاضافة','1';\r\n";
		text += "exec SystemDefaults_Insert 30,'DiscountTaxPercentage','نسبة ضريبة الخصم','1';\r\n";
		text += "exec SystemDefaults_Insert 31,'SalesInvoiceMessage','رسالة فاتورة المبيعات','';\r\n";
		text += "exec SystemDefaults_Insert 32,'CompanyEmail','بريد الشركة الالكتروني','';\r\n";
		text += "exec SystemDefaults_Insert 33,'CompanyEmailPassword','كلمة سر بريد الشركة','';\r\n";
		text += "exec SystemDefaults_Insert 34,'EINVClientID','EINVClientID','';\r\n";
		text += "exec SystemDefaults_Insert 35,'EINVClientSecret1','EINVClientSecret1','';\r\n";
		text += "exec SystemDefaults_Insert 36,'EINVClientSecret2','EINVClientSecret2','';\r\n";
		text += "exec Prv_Functions_Insert 25,'عرض كل الموظفين','ViewAllEmployees',1;\r\n";
		text += "exec Prv_Functions_Insert 26,'تعديل رقم تشغيلة الانتاج','UpdateProductionBatchNo',1;\r\n";
		text += "exec Prv_Functions_Insert 27,'Minimum Charge','MinimunCharge',1;\r\n";
		text += "exec Prv_Functions_Insert 28,'تعديل حساب تحليلى','ModifySubAccount',1;\r\n";
		text += "exec Prv_Functions_Insert 29,'طباعة مباشرة','DirectPrint',1;\r\n";
		text += "exec Prv_Functions_Insert 30,'Tap1','Tap1',1;\r\n";
		text += "exec Prv_Functions_Insert 31,'Tap2','Tap2',1;\r\n";
		text += "exec Prv_Functions_Insert 32,'Tap3','Tap3',1;\r\n";
		text += "exec Prv_Functions_Insert 33,'Tap4','Tap4',1;\r\n";
		text += "exec Prv_Functions_Insert 34,'Tap5','Tap5',1;\r\n";
		text += "exec Prv_Functions_Insert 35,'Tap6','Tap6',1;\r\n";
		text += "exec Prv_Functions_Insert 36,'Tap7','Tap7',1;\r\n";
		text += "exec Prv_Functions_Insert 37,'Tap8','Tap8',1;\r\n";
		text += "exec Prv_Functions_Insert 38,'Tap9','Tap9',1;\r\n";
		text += "exec Prv_Functions_Insert 39,'تعديل التاريخ','EditDate',1;\r\n";
		text += "exec Prv_Functions_Insert 40,'تعديل الكميه','ModifyQty',1;\r\n";
		text += "exec Prv_Functions_Insert 41,'متعدد الطباعة','MultiPrint',1;\r\n";
		text += "exec Prv_Functions_Insert 42,'تعديل القيمة','EditValue',1;\r\n";
		text += "exec Prv_Functions_Insert 43,'إقفال شيك','CloseCheck',1;\r\n";
		text += "exec Prv_Functions_Insert 44,'تقسيم شيك','SplitCheck',1;\r\n";
		text += "exec Prv_Functions_Insert 45,'دمج شيك','MergeCheck',1;\r\n";
		text += "exec Prv_Functions_Insert 46,'عرض السنوات المغلقة','Seeing Closed Years',0\r\n";
		text += "exec Prv_Functions_Insert 47,'مباشر','Direct',1\r\n";
		text += "exec Prv_Functions_Insert 48,'عرض الكمية','ViewQty',1;\r\n";
		text += "exec A_SuAccountsTypes_Insert 7,'عميل و موظف','Client And Employee';\r\n";
		text += "exec A_SuAccountsTypes_Insert 8,'وكيل','Agent';\r\n";
		text += "exec A_SuAccountsTypes_Insert 9,'مالك','Owner';\r\n";
		text += "exec A_SuAccountsTypes_Insert 10,'مستأجر','Charter';\r\n";
		text += "exec A_SuAccountsTypes_Insert 11,'قبطان','Captain';\r\n";
		text += "exec A_SuAccountsTypes_Insert 12,'بحار','SeaMan';\r\n";
		text += "exec A_JVTypes_Insert 24,'تحويلات ارصدة العملاء','Client Transfer';\r\n";
		text += "exec A_JVTypes_Insert 25,'تحويل مديونية بين الفروع','Branch Debit Transfer';\r\n";
		text += "exec A_JVTypes_Insert 26,'مبيعات البصريات','Optics Sales';\r\n";
		text += "exec A_JVTypes_Insert 27,'صرف البصريات','Optics Issue';\r\n";
		text += "exec A_JVTypes_Insert 28,'مشتريات من المعمل','Purchase From Lab';\r\n";
		text += "exec A_JVTypes_Insert 29,'مبيعات المعمل','Lab Sales';\r\n";
		text += "exec A_JVTypes_Insert 30,'إستحقاق قسط','Installment Due';\r\n";
		text += "exec A_JVTypes_Insert 31,'الغاء قسط','Installment Cancelation';\r\n";
		text += "exec A_JVTypes_Insert 32,'مبيعات عمليات','Operations Sales';\r\n";
		text += "exec A_JVTypes_Insert 33,'مصروفات عمليات','Operations Expenses';\r\n";
		text += "exec A_JVTypes_Insert 34,'إستحقاق مستخلص','Contract Due';\r\n";
		text += "exec A_JVTypes_Insert 35,'إستحقاق أجور','Wages Due';\r\n";
		text += "exec A_JVTypes_Insert 36,'الطلبات الخاصة','Special Orders';\r\n";
		text += "exec A_JVTypes_Insert 37,'مصروف مهام','Tasks Expenses';\r\n";
		text += "exec A_JVTypes_Insert 38,'مرتجع صرف بصريات','Optics Issue Returns';\r\n";
		text += "exec A_JVTypes_Insert 39,'مصروفات إدارية','Administrative Expenses';\r\n";
		text += "exec A_JVTypes_Insert 40,'مرتجع الطلبات الخاصة','Special Orders Returns';\r\n";
		text += "exec A_JVTypes_Insert 41,'تسوية عملات اجنبية','Foreign Currency Settlement';\r\n";
		text += "exec A_JVTypes_Insert 42,'مصروف فيزا','Visa Expense';\r\n";
		text += "exec A_JVTypes_Insert 43,'إشعار مورد','Supplier Credit Note';\r\n";
		text += "exec A_JVTypes_Insert 44,'إشعار عميل','Client Credit Note';\r\n";
		text += "exec A_JVTypes_Insert 45,'استحقاق عميل صادرات','Export Client Due';\r\n";
		text += "exec A_JVTypes_Insert 46,'مبيعات اوناش','Sling Sales';\r\n";
		text += "exec A_JVTypes_Insert 47,'صرف اوناش','Sling Issue';\r\n";
		text += "exec A_JVTypes_Insert 48,'اجور الانتاج','Production Fees';\r\n";
		text += "exec A_JVTypes_Insert 49,'مبيعات تموين سفن','ShipChandler Sales';\r\n";
		text += "exec A_JVTypes_Insert 50,'صرف تموين سفن','ShipChandler Issue';\r\n";
		text += "exec A_JVTypes_Insert 51,'مرتجع تموين سفن','ShipChandler Returns';\r\n";
		text += "exec A_JVTypes_Insert 52,'مرتجع صرف تموين سفن','ShipChandler Issue Returns';\r\n";
		text += "exec A_JVTypes_Insert 53,'صرف عيادات','Clinic Issue';\r\n";
		text += "exec A_JVTypes_Insert 54,'صرف مستلزمات معمل','BioAnalysis Issue';\r\n";
		text += "exec A_JVTypes_Insert 55,'إستحقاق حوافز','Bonus Due';\r\n";
		text += "exec A_JVTypes_Insert 56,'إقتناءأصل','Asset Accq';\r\n";
		text += "exec A_JVTypes_Insert 57,'بيع أصل','Asset Sales';\r\n";
		text += "exec A_JournalTypes_Insert 10,'تحويلات العملاء ','Client Transfer';\r\n";
		text += "exec A_JournalTypes_Insert 11,'تحويل مديونية بين الفروع ','Branch Debit Transfer';\r\n";
		text += "exec A_JournalTypes_Insert 12,'مبيعات البصريات ','Optics Sales';\r\n";
		text += "exec A_JournalTypes_Insert 13,'صرف البصريات ','Optics Issue';\r\n";
		text += "exec A_JournalTypes_Insert 14,'مشتريات من المعمل ','Purchase From Lab';\r\n";
		text += "exec A_JournalTypes_Insert 15,'مبيعات المعمل ','Lab Sales';\r\n";
		text += "exec A_JournalTypes_Insert 16,'إستحقاق قسط ','Installment Due ';\r\n";
		text += "exec A_JournalTypes_Insert 17,'الغاء قسط ','Installment Cancelation ';\r\n";
		text += "exec A_JournalTypes_Insert 18,'مبيعات عمليات ','Operations Sales ';\r\n";
		text += "exec A_JournalTypes_Insert 19,'مصروفات عمليات ','Operations Expenses ';\r\n";
		text += "exec A_JournalTypes_Insert 20,'إستحقاق مستخلص ','Operations Expenses ';\r\n";
		text += "exec A_JournalTypes_Insert 21,'أجور ','Operations Expenses ';\r\n";
		text += "exec A_JournalTypes_Insert 22,'مصروف مهام ','Tasks Expenses ';\r\n";
		text += "exec A_JournalTypes_Insert 23,'مرتجع صرف البصريات ','Optics Issue Returns ';\r\n";
		text += "exec A_JournalTypes_Insert 24,'مصروفات إدارية','Administrative Expenses';\r\n";
		text += "exec A_JournalTypes_Insert 25,'تسوية عملات اجنبية','Foreign Currency Settlement';\r\n";
		text += "exec A_JournalTypes_Insert 26,'مصروف فيزا','Visa Expense';\r\n";
		text += "exec A_JournalTypes_Insert 27,'استحقاق عميل صادرات','Export Client Due';\r\n";
		text += "exec A_JournalTypes_Insert 28,'مبيعات اوناش','Sling Sales';\r\n";
		text += "exec A_JournalTypes_Insert 29,'صرف اوناش','Sling Issue';\r\n";
		text += "exec A_JournalTypes_Insert 30,'اجور الانتاج','Production Fees';\r\n";
		text += "exec A_JournalTypes_Insert 31,'مبيعات تموين سفن','ShipChandler Sales';\r\n";
		text += "exec A_JournalTypes_Insert 32,'صرف تموين سفن','ShipChandler Issue';\r\n";
		text += "exec A_JournalTypes_Insert 33,'مرتجع تموين سفن','ShipChandler Returns';\r\n";
		text += "exec A_JournalTypes_Insert 34,'مرتجع صرف تموين سفن','ShipChandler Issue Returns';\r\n";
		text += "exec A_JournalTypes_Insert 35,'صرف عيادات','Clinic Issue';\r\n";
		text += "exec A_JournalTypes_Insert 36,'صرف مستلزمات معمل','BioAnalysis Issue';\r\n";
		text += "exec A_JournalTypes_Insert 37,'إستحقاق حوافز','Bonus Due';\r\n";
		text += "exec A_JournalTypes_Insert 38,'إقتناءأصل','Asset Accq';\r\n";
		text += "exec A_JournalTypes_Insert 39,'بيع أصل','Asset Sales';\r\n";
		text += "exec JVDefaults_Insert 10,'10','إشعارات مور\u0651د','Supplier Credit Note',4,43;\r\n";
		text += "exec JVDefaults_Insert 7,'7','إشعار عميل','Client Credit Note',5,44;\r\n";
		text += "exec JVDefaults_Insert 40,'66','تحويلات العملاء','Clients Transfer',10,24;\r\n";
		text += "exec JVDefaults_Insert 41,'67','تحويل مديونية بين الفروع','Branch Debit Transfer',11,25;\r\n";
		text += "exec JVDefaults_Insert 42,'68','مبيعات البصريات','Optics Sales',12,26;\r\n";
		text += "exec JVDefaults_Insert 43,'69','صرف البصريات','Optics Issue',13,27;\r\n";
		text += "exec JVDefaults_Insert 44,'70','مشتريات من المعمل','Purchase From Lab',14,28;\r\n";
		text += "exec JVDefaults_Insert 45,'71','مبيعات المعمل','Lab Sales',15,29;\r\n";
		text += "exec JVDefaults_Insert 46,'72','إستحقاق قسط','Installment Due',16,30;\r\n";
		text += "exec JVDefaults_Insert 47,'73','الغاء قسط','Installment Cancelation',17,31;\r\n";
		text += "exec JVDefaults_Insert 48,'74','مبيعات عمليات','Operations Sales',18,32;\r\n";
		text += "exec JVDefaults_Insert 49,'75','مصروفات عمليات','Operations Expenses',19,33;\r\n";
		text += "exec JVDefaults_Insert 50,'76','إستحقاق مستخلص','Contract Due',20,34;\r\n";
		text += "exec JVDefaults_Insert 51,'77','إقفال دفعات مقدمه','DownPayment Closing',20,34;\r\n";
		text += "exec JVDefaults_Insert 52,'78','إستحقاق أجور','Wages Due',21,35;\r\n";
		text += "exec JVDefaults_Insert 53,'79','صرف الطلبات الخاصة','Special Orders Issue',6,36;\r\n";
		text += "exec JVDefaults_Insert 54,'80','مصروفات مهام','Tasks Expenses',22,37;\r\n";
		text += "exec JVDefaults_Insert 55,'81','مرتجع صرف البصريات','Optics Issue Returns',23,38;\r\n";
		text += "exec JVDefaults_Insert 56,'82','مصروفات إدارية','Administrative Expenses',24,39;\r\n";
		text += "exec JVDefaults_Insert 57,'83','مرتجع الطلبات الخاصة','Special Orders Returns',6,40;\r\n";
		text += "exec JVDefaults_Insert 58,'84','تسوية عملات اجنبية','Foreign Currency Settlement',25,41;\r\n";
		text += "exec JVDefaults_Insert 59,'85','مصروف فيزا','Visa Cost',26,42;\r\n";
		text += "exec JVDefaults_Insert 60,'86','استحقاق عميل صادرات','Export Client Due',27,45;\r\n";
		text += "exec JVDefaults_Insert 61,'87','مبيعات اوناش','Sling Sales',28,46;\r\n";
		text += "exec JVDefaults_Insert 62,'88','صرف اوناش','Sling Issue',29,47;\r\n";
		text += "exec JVDefaults_Insert 63,'89','اجور الانتاج','Production Fees',30,48;\r\n";
		text += "exec JVDefaults_Insert 64,'90','مبيعات تموين سفن','ShipChandler Sales',31,49;\r\n";
		text += "exec JVDefaults_Insert 65,'91','صرف تموين سفن','ShipChandler Issue',32,50;\r\n";
		text += "exec JVDefaults_Insert 66,'92','مرتجع تموين سفن','ShipChandler Returns',33,51;\r\n";
		text += "exec JVDefaults_Insert 67,'93','مرتجع صرف تموين سفن','ShipChandler Issue Returns',34,52;\r\n";
		text += "exec JVDefaults_Insert 68,'94','صرف عيادات','Clinic Issue',35,53;\r\n";
		text += "exec JVDefaults_Insert 69,'95','صرف مستلزمات معمل','BioAnalysis Issue',36,54;\r\n";
		text += "exec JVDefaults_Insert 70,'96','إستحقاق حوافز','Bonus Due',37,55;\r\n";
		text += "exec JVDefaults_Insert 71,'97','إقتناءأصل','Asset Accq',38,56;\r\n";
		text += "exec JVDefaults_Insert 72,'98','بيع أصل','Asset Sales',39,57;\r\n";
		text += "exec JVDefaults_Insert 73,'99','صرف معمل البصريات','Optics Lab Issue',13,27;\r\n";
		text += "exec JVDefaults_Insert 74,'100','إستلام تحويل مخازن','Store Transfer Receive',6,10;\r\n";
		text += "exec JVDefaults_Insert 75,'101','صرف خامات عدسات','Blanks Materials Issue',6,9;\r\n";
		text += "exec JVDefaults_Insert 76,'102','تصنيع عدسات','Blanks Manufacturing',6,9;\r\n";
		text += "exec JVDefaults_Insert 77,'103','مبيعات تخليص','Clearence Sales',18,32;\r\n";
		text += "exec JVDefaults_Insert 78,'104','مصروفات تخليص','Clearence Expenses',19,33;\r\n";
		text += "exec SC_TransTypes_Insert 'RPOSMIV','مرتجع بيع مباشر','POS Return';\r\n";
		text += "exec SC_TransTypes_Insert 'LabMIV','صرف بيع مباشر','POS Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'RLabMIV','مرتجع بيع مباشر','POS Return';\r\n";
		text += "exec SC_TransTypes_Insert 'SOMIV','صرف الطلبات الخاصة','Special Order Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'SOAMIV','صرف الطلبات الخاصة','Special Order Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'RSOMIV','مرتجع الطلبات الخاصة','Special Order Returns';\r\n";
		text += "exec SC_TransTypes_Insert 'RSOAMIV','مرتجع الطلبات الخاصة','Special Order Returns';\r\n";
		text += "exec SC_TransTypes_Insert 'AddMIV','صرف بيع مباشر','POS Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'SlnMIV','صرف منتجات','Goods Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'SlnMMIV','صرف خامات','Material Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'SHPMIV','صرف للسفن','Ship Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'RSHPMIV','مرتجع صرف للسفن','Ship Issue Return';\r\n";
		text += "exec SC_TransTypes_Insert 'CLMIV','صرف عيادات','Clinic Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'PrcMIV','صرف مستلزمات إجراءت','Procedures Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'BioMIV','صرف مستلزمات معمل','BioAnalysis Issue';\r\n";
		text += "exec SC_TransTypes_Insert 'BlnkMIV','صرف خامات عدسات','Blanks Materials Issue';\r\n";
		text += "exec HR_AttMachineTypes_Insert '3','Face';\r\n";
		text += "exec HR_AttMachineTypes_Insert '4','E-Strong';\r\n";
		Main.ExecuteNonQuery(text);
	}

	public static void LoadUserPrivileges()
	{
		GlobalVariables.dtFunctions = UsersFunctions.SelectFunctions(GlobalVariables.UserID, IsFromServer: false);
		GlobalVariables.SeeingInvisibleAcounts = GlobalVariables.dtFunctions.Select("FunctionNameEn = 'Seeing Invisible Accounts'").Length != 0;
		GlobalVariables.SeeingClosedYears = GlobalVariables.dtFunctions.Select("FunctionNameEn = 'Seeing Closed Years'").Length != 0;
		GlobalVariables.dtForms = UsersForms.SelectForms(GlobalVariables.UserID, IsFromServer: false);
		GlobalVariables.dtAllForms = Forms.Select("-1", "-1", "0", IsFromServer: false);
		GlobalVariables.dtFormFunctions = UsersFormsFunctions.SelectFunctions(GlobalVariables.UserID, IsFromServer: false);
		GlobalVariables.BranchIDs = UsersBranches.SelectUserBrancheIDs(GlobalVariables.UserID, IsFromServer: false);
		GlobalVariables.SafeIDs = UsersSafes.SelectUserSafeIDs(GlobalVariables.UserID, IsFromServer: false);
		GlobalVariables.StoreIDs = UsersStores.SelectUserStoreIDs(GlobalVariables.UserID, IsFromServer: false);
	}

	public static void FlashLoadingStart()
	{
		Thread thread = new Thread(FlashLoadingThreadStart);
		thread.Start();
	}

	private static void FlashLoadingThreadStart()
	{
		frmLoadingFlash frmLoadingFlash2 = new frmLoadingFlash();
		frmLoadingFlash2.TopMost = true;
		frmLoadingFlash2.Show();
	}

	public static string HijriToGreg(string hijri)
	{
		if (hijri.Length <= 0)
		{
			return "";
		}
		try
		{
			return DateTime.ParseExact(hijri, "dd/MM/yyyy", new CultureInfo("ar-SA").DateTimeFormat, DateTimeStyles.AllowWhiteSpaces).ToString("dd/MM/yyyy", new CultureInfo("en-US").DateTimeFormat);
		}
		catch
		{
			return "";
		}
	}

	public static string GregToHijri(string greg)
	{
		if (greg.Length <= 0)
		{
			return "";
		}
		try
		{
			return DateTime.ParseExact(greg, "dd/MM/yyyy", new CultureInfo("en-US").DateTimeFormat, DateTimeStyles.AllowWhiteSpaces).AddDays(1.0).ToString("dd/MM/yyyy", new CultureInfo("ar-SA").DateTimeFormat);
		}
		catch
		{
			return "";
		}
	}

	public static bool SendMail(string SenderDisplayName, string Sender, string Password, string Receiver, string CC, string Body, string Subject)
	{
		try
		{
			string host = GlobalVariables.dtSystemDefaults.Select("DefaultEnName ='OutgoingMailServer(SMTP)'")[0]["DefaultValue"].ToString();
			string s = GlobalVariables.dtSystemDefaults.Select("DefaultEnName ='OutgoingServerPort'")[0]["DefaultValue"].ToString();
			MailMessage mailMessage = new MailMessage();
			SmtpClient smtpClient = new SmtpClient(host, int.Parse(s));
			smtpClient.UseDefaultCredentials = true;
			smtpClient.Credentials = new NetworkCredential(Sender, Password);
			mailMessage.From = new MailAddress(Sender, SenderDisplayName);
			mailMessage.To.Add(new MailAddress(Receiver));
			if (CC != string.Empty)
			{
				mailMessage.CC.Add(new MailAddress(CC));
			}
			mailMessage.Subject = Subject;
			mailMessage.Body = Body;
			mailMessage.IsBodyHtml = true;
			smtpClient.Send(mailMessage);
		}
		catch
		{
			GlobalVariables.InformationMB.Show("فشل فى عملية الارسال", "Sending Failed");
			return false;
		}
		return true;
	}

	public static bool SendMail(string SenderDisplayName, string Sender, string Password, string Receiver, string CC, string Body, string Subject, bool IsBodyHtml)
	{
		try
		{
			string host = GlobalVariables.dtSystemDefaults.Select("DefaultEnName ='OutgoingMailServer(SMTP)'")[0]["DefaultValue"].ToString();
			string s = GlobalVariables.dtSystemDefaults.Select("DefaultEnName ='OutgoingServerPort'")[0]["DefaultValue"].ToString();
			MailMessage mailMessage = new MailMessage();
			SmtpClient smtpClient = new SmtpClient(host, int.Parse(s));
			smtpClient.UseDefaultCredentials = true;
			smtpClient.Credentials = new NetworkCredential(Sender, Password);
			mailMessage.From = new MailAddress(Sender, SenderDisplayName);
			mailMessage.To.Add(new MailAddress(Receiver));
			if (CC != string.Empty)
			{
				mailMessage.CC.Add(new MailAddress(CC));
			}
			mailMessage.Subject = Subject;
			mailMessage.Body = Body;
			mailMessage.IsBodyHtml = IsBodyHtml;
			smtpClient.Send(mailMessage);
		}
		catch
		{
			GlobalVariables.InformationMB.Show("فشل فى عملية الارسال", "Sending Failed");
			return false;
		}
		return true;
	}

	public static bool GetFormFunction(string FormID, string Function)
	{
		DataRow[] array = GlobalVariables.dtFormFunctions.Select("FormID = " + FormID + "And FunctionNameEn = '" + Function + "'");
		if (array.Length == 0)
		{
			return false;
		}
		return true;
	}

	public static bool GetOption(string optionName)
	{
		DataRow[] array = GlobalVariables.dtSystemOptions.Select("OptionEnName='" + optionName + "'");
		if (array.Length == 0)
		{
			GlobalVariables.InformationMB.Show(" option " + optionName + " Not found");
			return false;
		}
		return Convert.ToBoolean(array[0]["OptionValue"]);
	}

	public static string GetDefault(string DefaultName)
	{
		DataRow[] array = GlobalVariables.dtSystemDefaults.Select("DefaultEnName='" + DefaultName + "'");
		if (array.Length == 0 || array[0]["DefaultValue"] == DBNull.Value)
		{
			GlobalVariables.InformationMB.Show(" option " + DefaultName + " Not found");
			return "";
		}
		return Convert.ToString(array[0]["DefaultValue"]);
	}

	public static void SetDefault(string DefaultName, string Value)
	{
		DataRow[] array = GlobalVariables.dtSystemDefaults.Select("DefaultEnName='" + DefaultName + "'");
		if (array.Length == 0)
		{
			GlobalVariables.InformationMB.Show(" option " + DefaultName + " Not found");
			return;
		}
		SystemDefaults.Insert_Update(array[0]["DefaultID"].ToString(), DefaultName, Value, GlobalVariables.UserID);
		array[0]["DefaultValue"] = Value;
	}

	public static string GetFormName(string frmName)
	{
		DataRow[] array = GlobalVariables.dtAllForms.Select("Form='" + frmName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		return Convert.ToString(GlobalVariables.IsArabic ? array[0]["FormNameAr"] : array[0]["FormNameEn"]);
	}

	public static string GetFormName(string RoteName, string GroupName, string frmName)
	{
		string text = "";
		DataRow[] array = GlobalVariables.dtAllForms.Select("Form='" + RoteName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		text = array[0]["FormID"].ToString();
		array = GlobalVariables.dtAllForms.Select("ParentID=" + text + " And Form='" + GroupName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		text = array[0]["FormID"].ToString();
		array = GlobalVariables.dtAllForms.Select("ParentID=" + text + " And Form='" + frmName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		return Convert.ToString(GlobalVariables.IsArabic ? array[0]["FormNameAr"] : array[0]["FormNameEn"]);
	}

	public static string GetFormID(string RootName, string GroupName, string frmName)
	{
		string text = "";
		DataRow[] array = GlobalVariables.dtAllForms.Select("Form='" + RootName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		text = array[0]["FormID"].ToString();
		array = GlobalVariables.dtAllForms.Select("ParentID=" + text + " And Form='" + GroupName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		text = array[0]["FormID"].ToString();
		array = GlobalVariables.dtAllForms.Select("ParentID=" + text + " And Form='" + frmName + "'");
		if (array.Length == 0)
		{
			return "";
		}
		return array[0]["FormID"].ToString();
	}

	public static void OpenForm(string FullName, int RowID, int Width, int Height)
	{
		frmBase frmBase2 = null;
		switch (FullName)
		{
		case "ERP.Sales.Transactions.frmQuotations":
			frmBase2 = new ERP.Sales.Transactions.frmQuotations(RowID);
			break;
		case "ERP.Sales.Transactions.frmSLInvoices":
			frmBase2 = new frmSLInvoices(RowID);
			break;
		case "ERP.Sales.Transactions.frmSLInvoices2":
			frmBase2 = new frmSLInvoices2(RowID);
			break;
		case "ERP.Sales.Transactions.frmSLInvoicesGroup":
			frmBase2 = new frmSLInvoicesGroup(RowID);
			break;
		case "ERP.Sales.Transactions.frmSLInvoicesPayments":
			frmBase2 = new frmSLInvoicesPayments(RowID);
			break;
		case "ERP.Sales.Transactions.frmSLTruckInvoices":
			frmBase2 = new frmSLTruckInvoices(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmPSInvoices":
			frmBase2 = new frmPSInvoices(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmPSInvoices2":
			frmBase2 = new frmPSInvoices2(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmPSMonthlyShares":
			frmBase2 = new frmPSMonthlyShares(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmPSOrders":
			frmBase2 = new frmPSOrders(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmPSRequest":
			frmBase2 = new frmPSRequest(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmPSTruckInvoices":
			frmBase2 = new frmPSTruckInvoices(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmQuotations":
			frmBase2 = new ERP.Purchasing.Transactions.frmQuotations(RowID);
			break;
		case "ERP.Purchasing.Transactions.frmQuotationsRequest":
			frmBase2 = new frmQuotationsRequest(RowID);
			break;
		case "ERP.HR.Payroll.Transactions.frmEmployeesAdvances":
			frmBase2 = new frmEmployeesAdvances(RowID);
			break;
		case "ERP.HR.Payroll.Transactions.frmEmployeesBonus":
			frmBase2 = new frmEmployeesBonus(RowID);
			break;
		case "ERP.HR.Payroll.Transactions.frmEmployeesExtraTimes":
			frmBase2 = new frmEmployeesExtraTimes(RowID);
			break;
		case "ERP.HR.Payroll.Transactions.frmSocialIncrease":
			frmBase2 = new frmSocialIncrease(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsInvoices":
			frmBase2 = new frmLnsInvoices(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsInvoices2":
			frmBase2 = new frmLnsInvoices2(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsInvoicesReturns":
			frmBase2 = new frmLnsInvoicesReturns(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsInvoicesReturnsByInvoice":
			frmBase2 = new frmLnsInvoicesReturnsByInvoice(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsInvoicesWithPayment":
			frmBase2 = new frmLnsInvoicesWithPayment(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsLabOrders":
			frmBase2 = new frmLnsLabOrders(RowID);
			break;
		case "ERP.Lenses.Transactions.frmLnsLabOrdersReturns":
			frmBase2 = new frmLnsLabOrdersReturns(RowID);
			break;
		case "ERP.Lenses.Transactions.frmReservations":
			frmBase2 = new frmReservations(RowID);
			break;
		case "ERP.SafesAndBanks.BankTransactions.frmBankIn":
			frmBase2 = new frmBankIn(RowID);
			break;
		case "ERP.SafesAndBanks.BankTransactions.frmBankOut":
			frmBase2 = new frmBankOut(RowID);
			break;
		case "ERP.SafesAndBanks.SafeTransactions.frmCustody":
			frmBase2 = new frmCustody(RowID);
			break;
		case "ERP.SafesAndBanks.SafeTransactions.frmSafeIn":
			frmBase2 = new frmSafeIn(RowID);
			break;
		case "ERP.SafesAndBanks.SafeTransactions.frmSafeOut":
			frmBase2 = new frmSafeOut(RowID);
			break;
		case "ERP.SafesAndBanks.SafeTransactions.frmSafeOutRequest":
			frmBase2 = new frmSafeOutRequest(RowID);
			break;
		case "ERP.Production.Transactions.frmProductionRequests":
			frmBase2 = new frmProductionRequests(RowID);
			break;
		case "ERP.Production.Transactions.frmProductions":
			frmBase2 = new frmProductions(RowID);
			break;
		case "ERP.Production.Transactions.frmProductMaintenance":
			frmBase2 = new frmProductMaintenance(RowID);
			break;
		case "ERP.StockControl.Transactions.frmClientsDepartmentsReturns":
			frmBase2 = new frmClientsDepartmentsReturns(RowID);
			break;
		case "ERP.StockControl.Transactions.frmGoodReceiptNotes":
			frmBase2 = new frmGoodReceiptNotes(RowID);
			break;
		case "ERP.StockControl.Transactions.frmItemsDiscount":
			frmBase2 = new frmItemsDiscount(RowID);
			break;
		case "ERP.StockControl.Transactions.frmMaterialIssueRequest":
			frmBase2 = new frmMaterialIssueRequest(RowID);
			break;
		case "ERP.StockControl.Transactions.frmMaterialIssueVouchers":
			frmBase2 = new frmMaterialIssueVouchers(RowID);
			break;
		case "ERP.StockControl.Transactions.frmOpeningBalances":
			frmBase2 = new frmOpeningBalances(RowID);
			break;
		case "ERP.StockControl.Transactions.frmStoreRevaluations":
			frmBase2 = new frmStoreRevaluations(RowID);
			break;
		case "ERP.StockControl.Transactions.frmStoresSettlementVouchers":
			frmBase2 = new frmStoresSettlementVouchers(RowID);
			break;
		case "ERP.StockControl.Transactions.frmStoresSettlementVouchersReplace":
			frmBase2 = new frmStoresSettlementVouchersReplace(RowID);
			break;
		case "ERP.StockControl.Transactions.frmStoreTakings":
			frmBase2 = new frmStoreTakings(RowID);
			break;
		case "ERP.StockControl.Transactions.frmStoreTransferRequest":
			frmBase2 = new frmStoreTransferRequest(RowID);
			break;
		case "ERP.StockControl.Transactions.frmStoreTransferVouchers":
			frmBase2 = new frmStoreTransferVouchers(RowID);
			break;
		case "ERP.StockControl.Transactions.frmSuppliersReturns":
			frmBase2 = new frmSuppliersReturns(RowID);
			break;
		case "ERP.StockControl.Slicing.frmSlicing":
			frmBase2 = new frmSlicing(RowID);
			break;
		case "ERP.Photos.Transactions.frmPHOInvoices":
			frmBase2 = new frmPHOInvoices(RowID);
			break;
		case "ERP.Photos.Transactions.frmPHOInvoicesPayments":
			frmBase2 = new frmPHOInvoicesPayments(RowID);
			break;
		case "ERP.Constructions.Transactions.frmContracts":
			frmBase2 = new ERP.Constructions.Transactions.frmContracts(RowID);
			break;
		case "ERP.Constructions.Transactions.frmPayments":
			frmBase2 = new frmPayments(RowID);
			break;
		case "ERP.CnsProjects.Transactions.frmContracts":
			frmBase2 = new ERP.CnsProjects.Transactions.frmContracts(RowID);
			break;
		case "ERP.CnsProjects.Transactions.frmContractsDues":
			frmBase2 = new frmContractsDues(RowID);
			break;
		case "ERP.CnsProjects.Transactions.frmProjectAttendance":
			frmBase2 = new frmProjectAttendance(RowID);
			break;
		case "ERP.Accounting.Transactions.frmBranchDebitTransfer":
			frmBase2 = new frmBranchDebitTransfer(RowID);
			break;
		case "ERP.Accounting.Transactions.frmForeignCurrencySettlements":
			frmBase2 = new frmForeignCurrencySettlements(RowID);
			break;
		case "ERP.Accounting.Transactions.frmGeneralExpenses":
			frmBase2 = new frmGeneralExpenses(RowID);
			break;
		case "ERP.Accounting.Transactions.frmJV":
			frmBase2 = new frmJV(RowID);
			break;
		case "ERP.MarineService.Transactions.frmItemsQuotations":
			frmBase2 = new frmItemsQuotations(RowID);
			break;
		case "ERP.MarineService.Transactions.frmOperationExpenses":
			frmBase2 = new frmOperationExpenses(RowID);
			break;
		case "ERP.MarineService.Transactions.frmOperationInvoices":
			frmBase2 = new frmOperationInvoices(RowID);
			break;
		case "ERP.MarineService.Transactions.frmOperations":
			frmBase2 = new frmOperations(RowID);
			break;
		case "ERP.MarineService.Transactions.frmProExpenses":
			frmBase2 = new frmProExpenses(RowID);
			break;
		case "ERP.MarineService.Transactions.frmServicesQuotations":
			frmBase2 = new frmServicesQuotations(RowID);
			break;
		case "ERP.WareHouse.Transactions.frmWareHouseIssue":
			frmBase2 = new frmWareHouseIssue(RowID);
			break;
		case "ERP.WareHouse.Transactions.frmWareHouseReceipt":
			frmBase2 = new frmWareHouseReceipt(RowID);
			break;
		case "ERP.Export.Transactions.frmExpOperationsDeclarations":
			frmBase2 = new frmExpOperationsDeclarations(RowID);
			break;
		}
		DataRow[] array = GlobalVariables.dtAllForms.Select("FormFullName = '" + FullName + "'");
		if (Width > 0 && Height > 0)
		{
			frmBase2.Size = new Size(Width, Height);
		}
		frmBase2.StartPosition = FormStartPosition.CenterScreen;
		frmBase2.Controls["lblTitle"].Text = (GlobalVariables.IsArabic ? array[0]["FormNameAr"].ToString() : array[0]["FormNameEn"].ToString());
		frmBase2.ShowDialog();
	}

	public static byte[] GetFileAsBytes(string filePath)
	{
		return File.ReadAllBytes(filePath);
	}

	public static void SaveFileAsBytes(string filePath, byte[] bytes)
	{
		File.WriteAllBytes(filePath, bytes);
	}

	public static DateTime GetServerDateTimeNow(bool IsFromServer = false)
	{
		string empty = string.Empty;
		empty += " SELECT GETDATE() As ServerDateTimeNow ";
		DataTable dataTable = (IsFromServer ? Main.SyncExecuteQuery_DataTable(empty) : Main.ExecuteQuery_DataTable(empty));
		return Convert.ToDateTime(dataTable.Rows[0][0]);
	}

	public static string GetFormTitle(string formName)
	{
		return GlobalVariables.dtForms.Select("FormFullName = '" + formName + "'")[0][GlobalVariables.IsArabic ? "FormNameAr" : "FormNameEn"].ToString();
	}

	public static void SC_OpenTransactionForms(int TransactionID)
	{
		DataTable dataTable = ItemsTransactions.Select(TransactionID);
		if (dataTable.Rows.Count != 0)
		{
			int iD = Convert.ToInt32(dataTable.Rows[0]["HeaderID"]);
			frmBase frmBase2 = null;
			if (dataTable.Rows[0]["TransType"].ToString() == "MIV")
			{
				frmBase2 = new frmMaterialIssueVouchers(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "GRN")
			{
				frmBase2 = new frmGoodReceiptNotes(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "STL")
			{
				frmBase2 = new frmStoresSettlementVouchers(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "STK")
			{
				frmBase2 = new frmStoreTakings(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "TrnTo" || dataTable.Rows[0]["TransType"].ToString() == "TrnFrom")
			{
				frmBase2 = new frmStoreTransferVouchers(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "RMIV")
			{
				frmBase2 = new frmClientsDepartmentsReturns(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "RGRN")
			{
				frmBase2 = new frmSuppliersReturns(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "LnsMIV")
			{
				frmBase2 = new frmLnsInvoices(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "RLnsMIV")
			{
				frmBase2 = new frmLnsInvoicesReturns(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "OPN")
			{
				frmBase2 = new frmOpeningBalances(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "RVLFrom" || dataTable.Rows[0]["TransType"].ToString() == "RVLTo")
			{
				frmBase2 = new frmStoreRevaluations(iD);
			}
			else if (dataTable.Rows[0]["TransType"].ToString() == "SlcMIV" || dataTable.Rows[0]["TransType"].ToString() == "SlcGRN")
			{
				frmBase2 = new frmSlicing(iD);
			}
			if (frmBase2 != null)
			{
				frmBase2.StartPosition = FormStartPosition.CenterParent;
				frmBase2.Controls["lblTitle"].Text = GetFormTitle(frmBase2.GetType().Namespace + "." + frmBase2.GetType().Name);
				frmBase2.ShowDialog();
			}
		}
	}

	public static void ConfigureReport()
	{
		foreach (Table table in GlobalVariables.ReportDocument.Database.Tables)
		{
			SetLogOnInfo(table);
		}
		foreach (ReportDocument subreport in GlobalVariables.ReportDocument.Subreports)
		{
			foreach (Table table2 in subreport.Database.Tables)
			{
				SetLogOnInfo(table2);
			}
		}
	}

	public static void ConfigureReport(ReportDocument RepDoc)
	{
		foreach (Table table in RepDoc.Database.Tables)
		{
			SetLogOnInfo(table);
		}
		foreach (ReportDocument subreport in RepDoc.Subreports)
		{
			foreach (Table table2 in subreport.Database.Tables)
			{
				SetLogOnInfo(table2);
			}
		}
	}

	public static void SetLogOnInfo(Table t)
	{
		TableLogOnInfo tableLogOnInfo = new TableLogOnInfo();
		ConnectionInfo connectionInfo = new ConnectionInfo();
		connectionInfo.ServerName = GlobalVariables.Server;
		connectionInfo.DatabaseName = GlobalVariables.DatabaseName;
		connectionInfo.UserID = GlobalVariables.dbUserID;
		connectionInfo.Password = GlobalVariables.dbPassword;
		tableLogOnInfo = t.LogOnInfo;
		tableLogOnInfo.ConnectionInfo = connectionInfo;
		t.ApplyLogOnInfo(tableLogOnInfo);
	}

	public static void ConfigureReportOnline(ReportDocument RepDoc)
	{
		foreach (Table table in RepDoc.Database.Tables)
		{
			SetLogOnInfoOnline(table);
		}
		foreach (ReportDocument subreport in RepDoc.Subreports)
		{
			foreach (Table table2 in subreport.Database.Tables)
			{
				SetLogOnInfoOnline(table2);
			}
		}
	}

	private static void SetLogOnInfoOnline(Table t)
	{
		TableLogOnInfo tableLogOnInfo = new TableLogOnInfo();
		ConnectionInfo connectionInfo = new ConnectionInfo();
		connectionInfo.ServerName = GlobalVariables.dtSyncConn.Rows[0]["ServerName"].ToString();
		connectionInfo.DatabaseName = GlobalVariables.dtSyncConn.Rows[0]["DataBaseName"].ToString();
		connectionInfo.UserID = GlobalVariables.dtSyncConn.Rows[0]["UserName"].ToString();
		connectionInfo.Password = GlobalVariables.dtSyncConn.Rows[0]["Password"].ToString();
		tableLogOnInfo = t.LogOnInfo;
		tableLogOnInfo.ConnectionInfo = connectionInfo;
		t.ApplyLogOnInfo(tableLogOnInfo);
	}

	public static void SetlblTitleStyle(UltraLabel ctrl)
	{
		((UltraControlBase)ctrl).UseAppStyling = false;
		if (GlobalVariables.Style == "ISS")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(72, 97, 135);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(191, 200, 234);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSDarkBlue")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(79, 124, 165);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(34, 62, 110);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSGreen")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(32, 96, 0);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(187, 209, 126);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSRed")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(144, 0, 0);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(255, 1, 1);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSGray")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(60, 70, 80);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(183, 190, 195);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSPurple")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(118, 0, 118);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(255, 135, 255);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSDarkGreen")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(0, 118, 59);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(0, 51, 25);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSDarkRed")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(120, 0, 0);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(50, 0, 0);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSOlive")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(150, 150, 0);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(50, 50, 0);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else if (GlobalVariables.Style == "ISSGold")
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(150, 150, 107);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(254, 253, 181);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
		else
		{
			((ControlBase)ctrl).Appearance.ForeColor = Color.White;
			((ControlBase)ctrl).Appearance.BackColor2 = Color.FromArgb(72, 97, 135);
			((ControlBase)ctrl).Appearance.BackColor = Color.FromArgb(191, 200, 234);
			((ControlBase)ctrl).Appearance.BackGradientStyle = (GradientStyle)7;
		}
	}

	public static void SetFormStyle(Form frm)
	{
		if (GlobalVariables.Style == "ISS")
		{
			frm.BackColor = Color.FromArgb(247, 247, 248);
		}
		else if (GlobalVariables.Style == "ISSDarkBlue")
		{
			frm.BackColor = Color.FromArgb(223, 230, 255);
		}
		else if (GlobalVariables.Style == "ISSGreen")
		{
			frm.BackColor = Color.FromArgb(225, 255, 200);
		}
		else if (GlobalVariables.Style == "ISSDarkGreen")
		{
			frm.BackColor = Color.FromArgb(225, 255, 200);
		}
		else if (GlobalVariables.Style == "ISSOlive")
		{
			frm.BackColor = Color.FromArgb(225, 255, 200);
		}
		else if (GlobalVariables.Style == "ISSRed")
		{
			frm.BackColor = Color.FromArgb(255, 233, 233);
		}
		else if (GlobalVariables.Style == "ISSDarkRed")
		{
			frm.BackColor = Color.FromArgb(255, 233, 233);
		}
		else if (GlobalVariables.Style == "ISSGray")
		{
			frm.BackColor = Color.FromArgb(240, 240, 240);
		}
		else if (GlobalVariables.Style == "ISSPurple")
		{
			frm.BackColor = Color.FromArgb(255, 210, 255);
		}
		else if (GlobalVariables.Style == "ISSGold")
		{
			frm.BackColor = Color.FromArgb(254, 255, 200);
		}
		else
		{
			frm.BackColor = Color.FromArgb(247, 247, 248);
		}
	}

	public static bool ArchivingSaveFile(string SourceFileName, string ArchivingDirectoryPath, string ArchivingFileName, bool OverWrite)
	{
		if (!Directory.Exists(GlobalVariables.ArchivingPath + ArchivingDirectoryPath))
		{
			Directory.CreateDirectory(GlobalVariables.ArchivingPath + ArchivingDirectoryPath);
		}
		if (!OverWrite && File.Exists(GlobalVariables.ArchivingPath + ArchivingDirectoryPath + ArchivingFileName))
		{
			return false;
		}
		FileSystem.CopyFile(SourceFileName, GlobalVariables.ArchivingPath + ArchivingDirectoryPath + ArchivingFileName, overwrite: true);
		return true;
	}

	public static bool UploadFtpFile(string filePath, string fileName, bool OverWrite)
	{
		string fileName2 = Path.GetFileName(fileName);
		FtpWebRequest ftpWebRequest = WebRequest.Create(new Uri(filePath)) as FtpWebRequest;
		ftpWebRequest.Credentials = new NetworkCredential("it@ahlan-eg.com", "Admin@12345");
		ftpWebRequest.Method = "SIZE";
		try
		{
			if (!OverWrite)
			{
				FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
				return false;
			}
		}
		catch
		{
		}
		ftpWebRequest = WebRequest.Create(new Uri(filePath)) as FtpWebRequest;
		ftpWebRequest.Method = "STOR";
		ftpWebRequest.UseBinary = true;
		ftpWebRequest.UsePassive = true;
		ftpWebRequest.KeepAlive = true;
		ftpWebRequest.Credentials = new NetworkCredential("it@ahlan-eg.com", "Admin@12345");
		ftpWebRequest.ConnectionGroupName = "group";
		using (FileStream fileStream = File.OpenRead(fileName))
		{
			byte[] array = new byte[fileStream.Length];
			fileStream.Read(array, 0, array.Length);
			fileStream.Close();
			Stream requestStream = ftpWebRequest.GetRequestStream();
			requestStream.Write(array, 0, array.Length);
			requestStream.Flush();
			requestStream.Close();
		}
		return true;
	}

	public static void ArchivingDeleteFile(string ArchivingDirectoryPath, string ArchivingFileName)
	{
		FileSystem.DeleteFile(GlobalVariables.ArchivingPath + ArchivingDirectoryPath + ArchivingFileName);
	}

	public static void ArchivingDeleteDirectory(string ArchivingDirectoryPath)
	{
		if (Directory.Exists(GlobalVariables.ArchivingPath + ArchivingDirectoryPath))
		{
			Directory.Delete(GlobalVariables.ArchivingPath + ArchivingDirectoryPath, recursive: true);
		}
	}

	public static DataTable ReadExcel(string fileName, string fileExt)
	{
		string empty = string.Empty;
		DataTable dataTable = new DataTable();
		empty = ((fileExt.CompareTo(".xls") != 0) ? ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';") : ("provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"));
		using (OleDbConnection oleDbConnection = new OleDbConnection(empty))
		{
			try
			{
				oleDbConnection.Open();
				DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
				if (oleDbSchemaTable != null && oleDbSchemaTable.Rows.Count != 0)
				{
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("select * from [" + oleDbSchemaTable.Rows[0]["TABLE_NAME"].ToString() + "]", oleDbConnection);
					oleDbDataAdapter.Fill(dataTable);
				}
			}
			catch (SEHException)
			{
				GlobalVariables.InformationMB.Show("نسخة الاكسل غير متطابقه مع نسخة الويندوز احدهما 64 و الاخر 32", "Excel And Windows Are Not Compatible One Is 64 And The Other Is 32");
			}
			catch (InvalidOperationException)
			{
				GlobalVariables.InformationMB.Show("نسخة الاكسل غير متطابقه مع نسخة الويندوز احدهما 64 و الاخر 32", "Excel And Windows Are Not Compatible One Is 64 And The Other Is 32");
			}
			catch (Exception ex3)
			{
				MessageBox.Show(ex3.Message.ToString());
			}
		}
		return dataTable;
	}

	public static void GenerateSMS(int TemplateID, int Delay, DateTime SMSDate, object ClientID, string MobileNumber)
	{
		MessagesLog.Insert(SMSDate.ToString(GlobalVariables.DateLongFormate), TemplateID.ToString(), "Null", (ClientID == DBNull.Value) ? "Null" : ClientID.ToString(), "Null", MobileNumber, GlobalVariables.IsArabic ? "2" : "1", SMSDate.AddMinutes(Delay).ToString(GlobalVariables.DateLongFormate), "Null", GlobalVariables.UserID, GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
	}

	public static void GenerateVoucherQR(string VoucherIDs, string TableName)
	{
		DeleteVoucherQR(VoucherIDs, TableName);
		DataTable dataTable = Main.ExecuteQuery_DataTable(" exec  " + TableName + "_SelectForQR '" + VoucherIDs + "'");
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			string text = "فاتورة رقم :" + dataTable.Rows[i]["VoucherCode"];
			text += "\r\n";
			text = text + "إسم العميل :" + dataTable.Rows[i]["ClientName"];
			text += "\r\n";
			text = text + "الرقم الضريبي :" + dataTable.Rows[i]["ClientTaxNo"];
			text += "\r\n";
			text = text + "التاريخ و الوقت :" + dataTable.Rows[i]["VoucherDate"];
			text += "\r\n";
			text = text + "القيمة المضافة :" + dataTable.Rows[i]["TaxAmount"];
			text += "\r\n";
			text = text + "قيمة الفاتورة :" + dataTable.Rows[i]["NetPrice"];
			QrCodeEncodingOptions qrCodeEncodingOptions = new QrCodeEncodingOptions();
			qrCodeEncodingOptions = new QrCodeEncodingOptions
			{
				DisableECI = true,
				CharacterSet = "UTF-8",
				Width = 3,
				Height = 3
			};
			BarcodeWriter barcodeWriter = new BarcodeWriter();
			barcodeWriter.Format = BarcodeFormat.QR_CODE;
			barcodeWriter.Options = qrCodeEncodingOptions;
			BarcodeWriter barcodeWriter2 = new BarcodeWriter();
			barcodeWriter2.Options = qrCodeEncodingOptions;
			barcodeWriter2.Format = BarcodeFormat.QR_CODE;
			Bitmap qRImage = new Bitmap(barcodeWriter2.Write(text));
			VouchersQR.Insert_Update(dataTable.Rows[i]["VoucherID"].ToString(), TableName, qRImage, GlobalVariables.UserID, IsFromServer: false);
		}
	}

	public static void DeleteVoucherQR(string VoucherIDs, string TableName)
	{
		Main.ExecuteNonQuery("exec G_VouchersQR_Delete '" + VoucherIDs + "' , '" + TableName + "'");
	}

	public static string GetTerminalClientName()
	{
		IntPtr ppBuffer = IntPtr.Zero;
		string result = null;
		if (NativeMethods.WTSQuerySessionInformation(NativeMethods.WTS_CURRENT_SERVER_HANDLE, -1, NativeMethods.WTS_INFO_CLASS.WTSClientName, out ppBuffer, out var pBytesReturned))
		{
			result = Marshal.PtrToStringUni(ppBuffer, pBytesReturned / 2 - 1);
			NativeMethods.WTSFreeMemory(ppBuffer);
		}
		return result;
	}

	public static bool IsEmailValid(string emailaddress)
	{
		string text = null;
		text = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
		if (Regex.IsMatch(emailaddress, text))
		{
			return true;
		}
		return false;
	}
}
