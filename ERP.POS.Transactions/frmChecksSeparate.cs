using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BusinessLayer;
using BusinessLayer.POS;
using BusinessLayer.StockControl;
using ERP.AbstractForms;
using ERP.Classes;
using ERP.SystemOptions.GeneralData;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace ERP.POS.Transactions;

public class frmChecksSeparate : frmBase
{
	public ArrayList DrHeaderRow = new ArrayList();

	public ArrayList dtDetails = new ArrayList();

	private DataRow DrMaster;

	private DataTable DtMasterDetails;

	private DataTable DtCheckDetails;

	public DataView DVItems;

	public DataTable DtUnits;

	public DataTable DtTaxs;

	private DataTable DtPOSDefaultData;

	public DataTable DtClients;

	public bool Cancel = false;

	private string ShiftDetailID;

	private string ShiftDetailUserID;

	private int TableID;

	private DataRow DRRoomData;

	private string CheckLog = "";

	private IContainer components = null;

	public UltraLabel lblTitle;

	public UltraButton btnCancel;

	public UltraButton btnSave;

	private UltraGroupBox UGBCheckNo;

	private UltraPanel pnlChecks;

	public UltraButton btnCheckCount;

	private UltraTextEditor txtCheckCount;

	private UltraLabel lblCheckCount;

	public frmChecksSeparate()
	{
		InitializeComponent();
	}

	public frmChecksSeparate(DataRow drmaster, DataTable dtmasterDetails, DataTable dtPOSDefaultData, DataView dvitems, DataTable dtunits, DataTable dttaxs, DataTable dtclients, int tableid, DataRow drroomdata, string checklog)
		: this()
	{
		DrMaster = drmaster.Table.Copy().Rows[0];
		DtMasterDetails = dtmasterDetails.Copy();
		DtPOSDefaultData = dtPOSDefaultData;
		DVItems = dvitems;
		DtUnits = dtunits;
		DtTaxs = dttaxs;
		DtClients = dtclients;
		TableID = tableid;
		DRRoomData = drroomdata;
		DtCheckDetails = dtmasterDetails.Copy();
		DtCheckDetails.Clear();
		CheckLog = checklog;
	}

	private void btnCheckCount_Click(object sender, EventArgs e)
	{
		frmDecimal frmDecimal2 = new frmDecimal((Control)(object)txtCheckCount, ((Control)(object)txtCheckCount).Text);
		frmDecimal2.StartPosition = FormStartPosition.Manual;
		frmDecimal2.Location = new Point(((Control)(object)txtCheckCount).Location.X + frmDecimal2.Width, ((Control)(object)txtCheckCount).Location.Y + ((Control)(object)txtCheckCount).Height);
		frmDecimal2.Show();
	}

	private void txtCheckCount_ValueChanged(object sender, EventArgs e)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		if (((Control)(object)txtCheckCount).Text != "" && int.Parse(((Control)(object)txtCheckCount).Text) > 1)
		{
			((Control)(object)pnlChecks).Visible = false;
			DrHeaderRow.Clear();
			dtDetails.Clear();
			((Control)(object)pnlChecks.ClientArea).Controls.Clear();
			int num = 8;
			int num2 = 3;
			for (int i = 0; i < int.Parse(((Control)(object)txtCheckCount).Text); i++)
			{
				UltraButton val = new UltraButton();
				((Control)(object)val).Click += btnChecks_Click;
				((Control)(object)val).Name = "btn" + i;
				((Control)(object)val).Tag = i;
				((Control)(object)val).Text = (GlobalVariables.IsArabic ? (" شيك رقم" + (i + 1)) : ("Check No " + (i + 1)));
				((Control)(object)val).Height += 25;
				((Control)(object)pnlChecks.ClientArea).Controls.Add((Control)(object)val);
				((UltraControlBase)val).Update();
				if (num + ((Control)(object)val).Width > ((Control)(object)pnlChecks).Width)
				{
					num2 += ((Control)(object)val).Height;
					num = 8;
				}
				((Control)(object)val).Left = num;
				((Control)(object)val).Top = num2;
				((Control)(object)val).Width = (((Control)(object)pnlChecks).Width - GlobalVariables.ScrollWidth) / 4;
				num += ((Control)(object)val).Width;
				if (i == 0)
				{
					DrHeaderRow.Add(DrMaster);
					dtDetails.Add(DtCheckDetails.Copy());
				}
				else
				{
					DrHeaderRow.Add(DrMaster.Table.Copy().Rows[0]);
					((DataRow)DrHeaderRow[i])["CheckID"] = "-1";
					dtDetails.Add(DtCheckDetails.Copy());
				}
			}
			((Control)(object)pnlChecks).Visible = true;
		}
		else
		{
			((Control)(object)pnlChecks.ClientArea).Controls.Clear();
		}
	}

	public void btnChecks_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		int index = int.Parse(((Control)(UltraButton)sender).Tag.ToString());
		frmChecksSeparateItems frmChecksSeparateItems2 = new frmChecksSeparateItems(DtMasterDetails, (DataRow)DrHeaderRow[index], (DataTable)dtDetails[index], DRRoomData);
		frmChecksSeparateItems2.WindowState = FormWindowState.Normal;
		frmChecksSeparateItems2.frmCheckSeparat = this;
		frmChecksSeparateItems2.ShowDialog();
		DrHeaderRow[index] = frmChecksSeparateItems2.DrHeader;
		frmChecksSeparateItems2.DtDetails.AcceptChanges();
		dtDetails[index] = frmChecksSeparateItems2.DtDetails;
		frmChecksSeparateItems2.DtMasterDetailsCopy.AcceptChanges();
		DtMasterDetails = frmChecksSeparateItems2.DtMasterDetailsCopy;
		frmChecksSeparateItems2.Dispose();
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Cancel = true;
		Close();
	}

	public void CheckForShiftDetails(DateTime CheckDate)
	{
		DataTable dataTable = ShiftsDetails.SelectNotClosed(GlobalVariables.CurrentBranchID);
		if (dataTable.Rows.Count != 1)
		{
			GlobalVariables.InformationMB.Show("برجاء فتح وردية اولا", "Please open Shift First");
			return;
		}
		DateTime dateTime = new DateTime(DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Year, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Month, DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()).Day, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Hour, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Minute, DateTime.Parse(dataTable.Rows[0]["StartTime"].ToString()).Second);
		DateTime dateTime2 = dateTime.AddHours(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Hour).AddMinutes(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Minute).AddSeconds(DateTime.Parse(dataTable.Rows[0]["ShiftPeriod"].ToString()).Second);
		if (GlobalFunctions.GetServerDateTimeNow() < dateTime)
		{
			GlobalVariables.InformationMB.Show(dateTime.ToShortTimeString() + " وقت بداية الوردية ", " Shift Start Time Is " + dateTime.ToShortTimeString());
			return;
		}
		if (GlobalFunctions.GetServerDateTimeNow() > dateTime2.AddHours(2.0))
		{
			GlobalVariables.InformationMB.Show(dateTime2.ToShortTimeString() + " وقت نهاية الوردية ", " Shift End Time Is " + dateTime2.ToShortTimeString());
			return;
		}
		if (CheckDate < DateTime.Parse(dataTable.Rows[0]["StartDate"].ToString()) && Adding)
		{
			GlobalVariables.InformationMB.Show("تاريخ الشيك أقل من تاريخ بداية الوردية تاكد من تاريخ الجهاز", "Check Date More Less Than Shift End Date Check Your pc ");
			return;
		}
		DataTable dataTable2 = ShiftsDetailsUsers.SelectNotClosed(dataTable.Rows[0]["ShiftDetailID"].ToString(), GlobalVariables.UserID, GlobalVariables.CurrentBranchID);
		ShiftDetailID = dataTable.Rows[0]["ShiftDetailID"].ToString();
		if (dataTable2.Rows.Count == 0)
		{
			ShiftDetailUserID = ShiftsDetailsUsers.Insert_Update("-1", ShiftDetailID, GlobalVariables.UserID, GlobalFunctions.GetServerDateTimeNow().ToString(GlobalVariables.DateLongFormate), "Null", "0", DrMaster["CurrencyID"].ToString(), DrMaster["ExchangeRate"].ToString(), "0", "0", "0", "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID).ToString();
		}
		else
		{
			ShiftDetailUserID = dataTable2.Rows[0]["ShiftDetailUserID"].ToString();
		}
	}

	public bool ValidateData()
	{
		if (DtMasterDetails.Rows.Count > 0)
		{
			GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "يوجد أصناف لم يتم تقسيمها" : "there is some Items Cant Split");
			return false;
		}
		for (int i = 0; i < dtDetails.Count; i++)
		{
			DataTable dataTable = (DataTable)dtDetails[i];
			if (dataTable.Rows.Count == 0)
			{
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "يوجد شيكات لاتوجد بها اى اصناف" : "there is Checks Have no Item");
				return false;
			}
			for (int j = 0; j < dataTable.Rows.Count; j++)
			{
				if (dataTable.Rows[j]["Qty"] != DBNull.Value && decimal.Parse(dataTable.Rows[j]["Qty"].ToString()) <= 0m)
				{
					GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "  يوجد اصناف كمياتها صفر" : "there Are Some Items With Quatity Zero");
					return false;
				}
			}
		}
		return true;
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		if (!ValidateData())
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < DrHeaderRow.Count; i++)
		{
			DataRow dataRow = (DataRow)DrHeaderRow[i];
			DataTable dataTable = (DataTable)dtDetails[i];
			CheckForShiftDetails(DateTime.Parse(dataRow["CheckDate"].ToString()));
			Main.StartBulkTrans(FromServer: false);
			try
			{
				string text = ((i == 0) ? dataRow["CheckNo"].ToString() : Checks.GetCodeByBranchID("1", "0", "0", DateTime.Parse(dataRow["CheckDate"].ToString()).ToString(GlobalVariables.DateShortFormate), GlobalVariables.CurrentBranchID));
				if (i == 0 && DtPOSDefaultData != null && DtPOSDefaultData.Rows.Count > 0 && bool.Parse(DtPOSDefaultData.Rows[0]["SerialByShiftDetailID"].ToString()) && ((DataRow)DrHeaderRow[i])["ShiftDetailID"].ToString() != ShiftDetailID.ToString())
				{
					text += "//S";
				}
				if (DRRoomData["RoundingValue"] != DBNull.Value && double.Parse(DRRoomData["RoundingValue"].ToString()) > 0.0)
				{
					double num = double.Parse(DRRoomData["RoundingValue"].ToString());
					double num2 = Math.Round(double.Parse(dataRow["NetPrice"].ToString()) % num, 3);
					double num3 = num / 2.0;
					double num4 = num - num2;
					if (num2 < num3)
					{
						dataRow["NetPrice"] = (double.Parse(dataRow["NetPrice"].ToString()) - num2).ToString();
						dataRow["RoundingValue"] = (num2 * -1.0).ToString();
					}
					else
					{
						dataRow["NetPrice"] = (double.Parse(dataRow["NetPrice"].ToString()) + num4).ToString();
						dataRow["RoundingValue"] = num4.ToString();
					}
				}
				int num5 = Checks.Insert_Update(dataRow["CheckID"].ToString(), text, DateTime.Parse(dataRow["CheckDate"].ToString()).ToString(GlobalVariables.DateLongFormate), "1", "0", "0", (i == 0) ? (int.Parse(dataRow["PersonCount"].ToString()) - (DrHeaderRow.Count - 1)).ToString() : "1", (dataRow["ClientID"] == DBNull.Value) ? "Null" : dataRow["ClientID"].ToString(), "Null", (dataRow["SubAccountID"] == DBNull.Value) ? "Null" : dataRow["SubAccountID"].ToString(), DRRoomData["PriceTypeID"].ToString(), dataRow["CurrencyID"].ToString(), dataRow["ExchangeRate"].ToString(), "Null", dataRow["GrossValue"].ToString(), dataRow["DiscountBeforeTaxValue"].ToString(), dataRow["DiscountBeforeTaxRatio"].ToString(), "0", dataRow["ServiceChargeValue"].ToString(), dataRow["TaxTotalValue"].ToString(), "0", "0", dataRow["RoundingValue"].ToString(), dataRow["NetPrice"].ToString(), bool.Parse(dataRow["IsMinCharge"].ToString()) ? "1" : "0", dataRow["MinChargeValue"].ToString(), bool.Parse(dataRow["IsMaxCharge"].ToString()) ? "1" : "0", dataRow["MaxChargeValue"].ToString(), "0", "0", "0", "Null", "Null", "0", "0", "0", "0", "0", dataRow["Notes"].ToString(), "", "0", ShiftDetailID, ShiftDetailUserID, DRRoomData["RoomID"].ToString(), GlobalVariables.UserID, "Null", (dataRow["CaptainOrderID"] == DBNull.Value) ? "Null" : dataRow["CaptainOrderID"].ToString(), "Null", "Null", "Null", "Null", "Null", "Null", "Null", "Null", "0", "Null", "Null", "1", "Null", "Null", "0", "0", "Null", "Null", "", "", "0", "0", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				CheckLog = CheckLog + " تم تقسيم الشيك مع الشيك رقم " + text + " مستخدم  " + GlobalVariables.UserName + " بتاريخ " + DateTime.Now.ToString(GlobalVariables.DateLongFormate) + "//";
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					DataTable dataTable2 = ChecksDetailsAccessories.SelectByCheckDetailID(dataTable.Rows[j]["CheckDetailID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
					DataTable dataTable3 = ChecksDetailsAdditionals.SelectByCheckDetailID(dataTable.Rows[j]["CheckDetailID"].ToString(), GlobalVariables.IsArabic ? "1" : "0");
					if (!arrayList.Contains(dataTable.Rows[j]["CheckDetailID"].ToString()))
					{
						arrayList.Add(dataTable.Rows[j]["CheckDetailID"].ToString());
						ChecksDetailsAdditionals.DeleteByCheckDetailID(dataTable.Rows[j]["CheckDetailID"].ToString(), GlobalVariables.UserID);
						ChecksDetailsAccessories.DeleteByCheckDetailID(dataTable.Rows[j]["CheckDetailID"].ToString(), GlobalVariables.UserID);
						ChecksDetails.Delete(dataTable.Rows[j]["CheckDetailID"].ToString(), GlobalVariables.UserID);
					}
					int num6 = ChecksDetails.Insert_Update("-1", num5.ToString(), dataTable.Rows[j]["ItemID"].ToString(), (dataTable.Rows[j]["ColorID"] == DBNull.Value) ? "1" : dataTable.Rows[j]["ColorID"].ToString(), (dataTable.Rows[j]["ItemSizeID"] == DBNull.Value) ? "1" : dataTable.Rows[j]["ItemSizeID"].ToString(), (dataTable.Rows[j]["Qty"] == DBNull.Value) ? "0" : dataTable.Rows[j]["Qty"].ToString(), (dataTable.Rows[j]["UnitID"] == DBNull.Value) ? "Null" : dataTable.Rows[j]["UnitID"].ToString(), (dataTable.Rows[j]["UnitPrice"] == DBNull.Value) ? "0" : dataTable.Rows[j]["UnitPrice"].ToString(), (dataTable.Rows[j]["ReturnedQty"] == DBNull.Value) ? "0" : dataTable.Rows[j]["ReturnedQty"].ToString(), (dataTable.Rows[j]["StoreID"] == DBNull.Value) ? "Null" : DVItems.Table.Select(" ItemID =" + dataTable.Rows[j]["ItemID"].ToString())[0]["StoreID"].ToString(), (dataTable.Rows[j]["AdditionalPrice"] == DBNull.Value) ? "0" : dataTable.Rows[j]["AdditionalPrice"].ToString(), (dataTable.Rows[j]["TotalPrice"] == DBNull.Value) ? "0" : dataTable.Rows[j]["TotalPrice"].ToString(), (dataTable.Rows[j]["Discount"] == DBNull.Value) ? "0" : dataTable.Rows[j]["Discount"].ToString(), (dataTable.Rows[j]["ServiceChargeAmount"] == DBNull.Value) ? "0" : dataTable.Rows[j]["ServiceChargeAmount"].ToString(), (dataTable.Rows[j]["TaxID"] == DBNull.Value) ? "Null" : dataTable.Rows[j]["TaxID"].ToString(), (dataTable.Rows[j]["TaxValue"] == DBNull.Value) ? "0" : dataTable.Rows[j]["TaxValue"].ToString(), (dataTable.Rows[j]["NetPrice"] == DBNull.Value) ? "0" : dataTable.Rows[j]["NetPrice"].ToString(), (dataTable.Rows[j]["RoomDiscountValue"] == DBNull.Value) ? "0" : dataTable.Rows[j]["RoomDiscountValue"].ToString(), (dataTable.Rows[j]["ActualUnitSalesPrice"] == DBNull.Value) ? "0" : dataTable.Rows[j]["ActualUnitSalesPrice"].ToString(), "Null", "0", (dataTable.Rows[j]["SentQty"] == DBNull.Value) ? "0" : dataTable.Rows[j]["Qty"].ToString(), "0", dataTable.Rows[j]["Notes"].ToString(), (dataTable.Rows[j]["offerID"] == DBNull.Value) ? "Null" : dataTable.Rows[j]["offerID"].ToString(), (dataTable.Rows[j]["OfferDiscountRatio"] == DBNull.Value) ? "0" : dataTable.Rows[j]["OfferDiscountRatio"].ToString(), DateTime.Parse(dataRow["CheckDate"].ToString()).ToString(GlobalVariables.DateLongFormate), "Null", "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					for (int k = 0; k < dataTable2.Rows.Count; k++)
					{
						ChecksDetailsAccessories.Insert_Update("-1", num6.ToString(), num5.ToString(), (dataTable2.Rows[k]["ItemID"] == DBNull.Value) ? "Null" : dataTable2.Rows[k]["ItemID"].ToString(), (dataTable2.Rows[k]["OriginalQty"] == DBNull.Value) ? "0" : dataTable2.Rows[k]["OriginalQty"].ToString(), (dataTable2.Rows[k]["AccessoriesCount"] == DBNull.Value) ? "0" : dataTable2.Rows[k]["AccessoriesCount"].ToString(), (dataTable2.Rows[k]["Qty"] == DBNull.Value) ? "0" : dataTable2.Rows[k]["Qty"].ToString(), (dataTable2.Rows[k]["UnitID"] == DBNull.Value) ? "Null" : dataTable2.Rows[k]["UnitID"].ToString(), (dataTable2.Rows[k]["StoreID"] == DBNull.Value) ? "Null" : dataTable2.Rows[k]["StoreID"].ToString(), dataTable2.Rows[k]["Notes"].ToString(), dataTable2.Rows[k]["VoucherDate"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
					for (int l = 0; l < dataTable3.Rows.Count; l++)
					{
						ChecksDetailsAdditionals.Insert_Update("-1", num6.ToString(), num5.ToString(), (dataTable3.Rows[l]["ItemID"] == DBNull.Value) ? "Null" : dataTable3.Rows[l]["ItemID"].ToString(), (dataTable3.Rows[l]["OriginalQty"] == DBNull.Value) ? "0" : dataTable3.Rows[l]["OriginalQty"].ToString(), (dataTable3.Rows[l]["Qty"] == DBNull.Value) ? "0" : dataTable3.Rows[l]["Qty"].ToString(), (dataTable3.Rows[l]["UnitID"] == DBNull.Value) ? "Null" : dataTable3.Rows[l]["UnitID"].ToString(), (dataTable3.Rows[l]["UnitPrice"] == DBNull.Value) ? "0" : dataTable3.Rows[l]["UnitPrice"].ToString(), (dataTable3.Rows[l]["StoreID"] == DBNull.Value) ? "Null" : dataTable3.Rows[l]["StoreID"].ToString(), (dataTable3.Rows[l]["TotalPrice"] == DBNull.Value) ? "0" : dataTable3.Rows[l]["TotalPrice"].ToString(), dataTable3.Rows[l]["Notes"].ToString(), dataTable3.Rows[l]["VoucherDate"].ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
					}
				}
				if (i != 0)
				{
					ChecksTables.Insert_Update("-1", num5.ToString(), TableID.ToString(), "0", GlobalVariables.CurrentBranchID, GlobalVariables.UserID);
				}
				string text2 = MessageLog.SelectByVoucherIDAndTransType(num5.ToString(), "POSMIV", "POSMIV", GlobalVariables.IsArabic ? "1" : "0");
				if (text2 != "")
				{
					GlobalVariables.InformationMB.Show(text2);
					Main.RollbackBulkTrans(FromServer: false);
					MessageLog.DeleteByVoucherIDAndTransType(num5.ToString(), "POSMIV", "POSMIV");
				}
				else
				{
					Main.EndBulkTrans(FromServer: false);
				}
			}
			catch
			{
				Main.RollbackBulkTrans(FromServer: false);
				GlobalVariables.InformationMB.Show(GlobalVariables.IsArabic ? "حدث خطا لم يتم الحفظ  " : "Error Occured");
				return;
			}
		}
		ItemsTransactions.ManageInThread();
		Close();
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
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.POS.Transactions.frmChecksSeparate));
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		this.pnlChecks = new UltraPanel();
		this.lblTitle = new UltraLabel();
		this.btnCancel = new UltraButton();
		this.btnSave = new UltraButton();
		this.UGBCheckNo = new UltraGroupBox();
		this.btnCheckCount = new UltraButton();
		this.txtCheckCount = new UltraTextEditor();
		this.lblCheckCount = new UltraLabel();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.Windows.Forms.Control)(object)this.pnlChecks).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.UGBCheckNo).BeginInit();
		((System.Windows.Forms.Control)(object)this.UGBCheckNo).SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtCheckCount).BeginInit();
		base.SuspendLayout();
		resources.ApplyResources(base.lblTop, "lblTop");
		resources.ApplyResources(base.lblBottom, "lblBottom");
		resources.ApplyResources(base.lblLeft, "lblLeft");
		resources.ApplyResources(base.lblRight, "lblRight");
		resources.ApplyResources(this.pnlChecks, "pnlChecks");
		this.pnlChecks.AutoScroll = true;
		resources.ApplyResources(this.pnlChecks.ClientArea, "pnlChecks.ClientArea");
		((System.Windows.Forms.Control)(object)this.pnlChecks).Name = "pnlChecks";
		resources.ApplyResources(this.lblTitle, "lblTitle");
		((AppearanceBase)val).BackColor = System.Drawing.Color.FromArgb(191, 200, 234);
		((AppearanceBase)val).BackColor2 = System.Drawing.Color.FromArgb(72, 97, 135);
		((AppearanceBase)val).BackGradientStyle = (GradientStyle)7;
		resources.ApplyResources(val, "appearance1");
		((ControlBase)this.lblTitle).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.lblTitle).Name = "lblTitle";
		((UltraControlBase)this.lblTitle).UseAppStyling = false;
		resources.ApplyResources(this.btnCancel, "btnCancel");
		((AppearanceBase)val2).Image = resources.GetObject("appearance2.Image");
		resources.ApplyResources(val2, "appearance2");
		((ControlBase)this.btnCancel).Appearance = (AppearanceBase)(object)val2;
		((ControlBase)this.btnCancel).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnCancel).Name = "btnCancel";
		((System.Windows.Forms.Control)(object)this.btnCancel).Click += new System.EventHandler(btnCancel_Click);
		resources.ApplyResources(this.btnSave, "btnSave");
		((AppearanceBase)val3).Image = resources.GetObject("appearance3.Image");
		resources.ApplyResources(val3, "appearance3");
		((ControlBase)this.btnSave).Appearance = (AppearanceBase)(object)val3;
		((ControlBase)this.btnSave).ImageSize = new System.Drawing.Size(1, 1);
		((System.Windows.Forms.Control)(object)this.btnSave).Name = "btnSave";
		((System.Windows.Forms.Control)(object)this.btnSave).Click += new System.EventHandler(btnSave_Click);
		resources.ApplyResources(this.UGBCheckNo, "UGBCheckNo");
		this.UGBCheckNo.CaptionAlignment = (GroupBoxCaptionAlignment)1;
		((System.Windows.Forms.Control)(object)this.UGBCheckNo).Controls.Add((System.Windows.Forms.Control)(object)this.pnlChecks);
		((System.Windows.Forms.Control)(object)this.UGBCheckNo).Name = "UGBCheckNo";
		resources.ApplyResources(this.btnCheckCount, "btnCheckCount");
		((System.Windows.Forms.Control)(object)this.btnCheckCount).Name = "btnCheckCount";
		((System.Windows.Forms.Control)(object)this.btnCheckCount).Click += new System.EventHandler(btnCheckCount_Click);
		resources.ApplyResources(this.txtCheckCount, "txtCheckCount");
		((System.Windows.Forms.Control)(object)this.txtCheckCount).Name = "txtCheckCount";
		((TextEditorControlBase)this.txtCheckCount).ValueChanged += new System.EventHandler(txtCheckCount_ValueChanged);
		resources.ApplyResources(this.lblCheckCount, "lblCheckCount");
		this.lblCheckCount.AutoEllipsis = false;
		((System.Windows.Forms.Control)(object)this.lblCheckCount).Name = "lblCheckCount";
		((ControlBase)this.lblCheckCount).WrapText = false;
		resources.ApplyResources(this, "$this");
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCheckCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCheckCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblCheckCount);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.UGBCheckNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnCancel);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnSave);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblTitle);
		base.Name = "frmChecksSeparate";
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnSave, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.UGBCheckNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblCheckCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCheckCount, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnCheckCount, 0);
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.Windows.Forms.Control)(object)this.pnlChecks).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.UGBCheckNo).EndInit();
		((System.Windows.Forms.Control)(object)this.UGBCheckNo).ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.txtCheckCount).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
