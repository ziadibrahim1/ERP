using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using BusinessLayer.SupportingTickets;
using BusinessLayer.Ticketing;
using ERP.AbstractForms;
using ERP.Classes;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.Ticketing;

public class frmSupportingTickets : frmGrid
{
	private bool IsError = false;

	private bool IsMessage = false;

	private bool IsFormQst = false;

	private bool IsUserQst = false;

	private bool IsGlobal = false;

	private string FormFullName = string.Empty;

	private string SystemMessage = string.Empty;

	private string ImageString = string.Empty;

	private string AttachementString = string.Empty;

	private string AttachementPath = string.Empty;

	private DataTable dtSupportUsers;

	private int WorkingGlobalID = 0;

	private IContainer components = null;

	private UltraTextEditor txtSolution;

	private UltraLabel ultraLabel1;

	private UltraLabel ultraLabel2;

	private UltraLabel ultraLabel3;

	private UltraPictureBox picIssueImage;

	private UltraButton btnLoadImage;

	private UltraTextEditor txtClientNotes;

	private UltraTextEditor txtTicketNo;

	private UltraLabel ultraLabel4;

	private UltraDateTimeEditor dtpTicketDate;

	private UltraLabel ultraLabel5;

	private UltraCheckEditor chkIsDelivered;

	private UltraLabel ultraLabel6;

	private UltraDateTimeEditor dtpSupReciveDate;

	private UltraCheckEditor chkIsRecieved;

	private UltraLabel ultraLabel7;

	private UltraComboEditor cboSupportUsers;

	private UltraButton btnAttachFile;

	private UltraLabel lblattachments;

	private UltraButton btnClearAttachments;

	private UltraButton btnClearImage;

	private UltraLabel ultraLabel8;

	private UltraLabel lblMobile;

	private UltraButton btnReference;

	private UltraLabel ultraLabel9;

	private UltraTextEditor txtMobile;

	public frmSupportingTickets()
	{
		InitializeComponent();
		IDCol = "TicketID";
		IsUserQst = true;
	}

	public frmSupportingTickets(bool isError, bool isMessage, bool isFormQst, string formFullName, string systemMessage, Image _pImage)
	{
		InitializeComponent();
		IDCol = "TicketID";
		IsError = isError;
		IsMessage = isMessage;
		IsFormQst = isFormQst;
		picIssueImage.Image = _pImage;
		SystemMessage = systemMessage;
		FormFullName = formFullName;
	}

	public frmSupportingTickets(int GlobalID)
	{
		InitializeComponent();
		IsGlobal = true;
		WorkingGlobalID = GlobalID;
	}

	private void frmSupportingTickets_Load(object sender, EventArgs e)
	{
		((Control)(object)lblattachments).Cursor = Cursors.Hand;
		((Control)(object)picIssueImage).Cursor = Cursors.Hand;
		((Control)(object)lblattachments).MouseClick += Lblattachments_MouseClick;
		((Control)(object)picIssueImage).MouseClick += picIssueImage_MouseClick;
	}

	private void picIssueImage_MouseClick(object sender, MouseEventArgs e)
	{
		if (picIssueImage.Image != null)
		{
			frmPreviewImage frmPreviewImage2 = new frmPreviewImage((Image)picIssueImage.Image, Adding || Updating);
			frmPreviewImage2.Owner = this;
			frmPreviewImage2.ShowDialog();
			picIssueImage.Image = frmPreviewImage2.OrgnImg;
			((Control)(object)picIssueImage).Refresh();
		}
	}

	private void Lblattachments_MouseClick(object sender, MouseEventArgs e)
	{
		try
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value.ToString().Length > 0)
			{
				string _resFileName = string.Empty;
				byte[] bytes = UnCompressZipByteArray(Convert.FromBase64String(((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value.ToString()), ref _resFileName);
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Filter = "All Files|*.*";
				saveFileDialog.Title = "Save Attachement";
				saveFileDialog.FileName = _resFileName;
				saveFileDialog.ShowDialog();
				if (saveFileDialog.FileName != "")
				{
					File.WriteAllBytes(saveFileDialog.FileName, bytes);
				}
			}
		}
		catch
		{
		}
	}

	public override void PrepareData()
	{
		dtSupportUsers = Users.Select("-1", "-1", GlobalVariables.IsArabic ? "1" : "0");
		GlobalFunctions.FillCombo(cboSupportUsers, dtSupportUsers, "TUserID", GlobalVariables.IsArabic ? "UserNameAr" : "UserNameEn");
	}

	public override void SetControls(bool NavMode)
	{
		base.SetControls(NavMode);
		((Control)(object)btnPrint).Visible = false;
		((Control)(object)btnHeaderSearch).Visible = false;
		((Control)(object)btnRefreshData).Visible = false;
		((EditorButtonControlBase)txtClientNotes).ReadOnly = NavMode;
		((EditorButtonControlBase)txtMobile).ReadOnly = NavMode;
		((EditorButtonControlBase)txtSolution).ReadOnly = true;
		((EditorButtonControlBase)txtTicketNo).ReadOnly = true;
		((EditorButtonControlBase)dtpTicketDate).ReadOnly = true;
		((EditorButtonControlBase)dtpSupReciveDate).ReadOnly = true;
		((EditorButtonControlBase)cboSupportUsers).ReadOnly = true;
		((Control)(object)chkIsRecieved).Enabled = false;
		((Control)(object)chkIsDelivered).Enabled = false;
		((Control)(object)btnReference).Visible = false;
		((Control)(object)btnClearAttachments).Visible = !NavMode && ((Control)(object)lblattachments).Text.Length > 0;
		((Control)(object)btnClearImage).Visible = !NavMode && picIssueImage.Image != null && IsUserQst;
		((Control)(object)btnLoadImage).Enabled = !NavMode && !((UltraToggleEditorBase)chkIsRecieved).Checked && !((UltraToggleEditorBase)chkIsDelivered).Checked && IsUserQst;
		((Control)(object)btnAttachFile).Enabled = !NavMode && !((UltraToggleEditorBase)chkIsRecieved).Checked && !((UltraToggleEditorBase)chkIsDelivered).Checked;
	}

	public override void ClearControls()
	{
		((UltraToggleEditorBase)chkIsRecieved).Checked = false;
		((UltraToggleEditorBase)chkIsDelivered).Checked = false;
		((TextEditorControlBase)txtClientNotes).Clear();
		((TextEditorControlBase)txtMobile).Clear();
		((Control)(object)txtTicketNo).Text = (Adding ? Tickets.GetCode() : "");
		cboSupportUsers.SelectedIndex = -1;
		((Control)(object)cboSupportUsers).Text = string.Empty;
		((Control)(object)lblattachments).Text = string.Empty;
		((Control)(object)btnReference).Visible = false;
		picIssueImage.Image = null;
		AttachementString = string.Empty;
		ImageString = string.Empty;
		dtpSupReciveDate.DateTime = DateTime.Now;
		dtpTicketDate.DateTime = DateTime.Now;
	}

	public override void FillData()
	{
		Cursor current = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		try
		{
			if (IsGlobal)
			{
				SetControls(NavMode: true);
				((Control)(object)btnAdd).Visible = false;
				((Control)(object)btnAttachFile).Visible = false;
				((Control)(object)btnCancel).Visible = false;
				((Control)(object)btnClearAttachments).Visible = false;
				((Control)(object)btnClearImage).Visible = false;
				((Control)(object)btnDelete).Visible = false;
				((Control)(object)btnLoadImage).Visible = false;
				((Control)(object)btnHeaderSearch).Visible = false;
				((Control)(object)btnNext).Visible = false;
				((Control)(object)btnOK).Visible = false;
				((Control)(object)btnPriveous).Visible = false;
				((Control)(object)btnSaveClose).Visible = false;
				((Control)(object)btnUpdate).Visible = false;
				dataTable = Tickets.Select(WorkingGlobalID.ToString(), "-1", GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dataTable;
				InitGrid();
			}
			else if (IsUserQst)
			{
				dataTable = Tickets.SelectByServerID(GlobalVariables.ServerID, GlobalVariables.IsArabic ? "1" : "0");
				((UltraGridBase)ULGData).DataSource = dataTable;
				InitGrid();
			}
			else
			{
				RowID = "-1";
				Adding = true;
				SetControls(NavMode: false);
				((Control)(object)btnOK).Visible = false;
				((Control)(object)txtTicketNo).Text = (Adding ? Tickets.GetCode() : "");
				dtpSupReciveDate.DateTime = DateTime.Now;
				dtpTicketDate.DateTime = DateTime.Now;
				cboSupportUsers.SelectedIndex = -1;
				((Control)(object)cboSupportUsers).Text = string.Empty;
				((Control)(object)btnClearImage).Visible = false;
				((Control)(object)btnLoadImage).Visible = false;
			}
		}
		catch
		{
			Cursor.Current = current;
		}
		Cursor.Current = current;
	}

	public override void InitGrid()
	{
		base.InitGrid();
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TicketNo"].Header).Caption = (GlobalVariables.IsArabic ? "رقم الطلب" : "Ticket No");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TicketNo"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TicketNo"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1) - GlobalVariables.ScrollWidth;
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientUserName"].Header).Caption = (GlobalVariables.IsArabic ? "مستخدم" : "User");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientUserName"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientUserName"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TicketDate"].Header).Caption = (GlobalVariables.IsArabic ? "تاريخ الطلب" : "Ticket Date");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TicketDate"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["TicketDate"].Width = (int)((double)((Control)(object)ULGData).Width * 0.15);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivered"].Header).Caption = (GlobalVariables.IsArabic ? "تام" : "Finished");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivered"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["IsDelivered"].Width = (int)((double)((Control)(object)ULGData).Width * 0.1);
		((HeaderBase)((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientNotes"].Header).Caption = (GlobalVariables.IsArabic ? "ملاحظات" : "Notes");
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientNotes"].Hidden = false;
		((UltraGridBase)ULGData).DisplayLayout.Bands[0].Columns["ClientNotes"].Width = (int)((double)((Control)(object)ULGData).Width * 0.5);
	}

	public override void AfterRowActivate()
	{
		Cursor current = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		if (!Adding)
		{
			picIssueImage.Image = null;
			((Control)(object)lblattachments).Text = string.Empty;
			AttachementString = string.Empty;
			UltraButton obj = btnUpdate;
			bool enabled = (((Control)(object)btnDelete).Enabled = !bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSupRecived"].Value.ToString()));
			((Control)(object)obj).Enabled = enabled;
			((UltraToggleEditorBase)chkIsRecieved).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsSupRecived"].Value.ToString());
			((UltraToggleEditorBase)chkIsDelivered).Checked = bool.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["IsDelivered"].Value.ToString());
			((Control)(object)txtClientNotes).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["ClientNotes"].Value.ToString();
			((TextEditorControlBase)txtMobile).Clear();
			if (!IsGlobal)
			{
				((Control)(object)txtMobile).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["UserPhoneNO"].Value.ToString();
			}
			((Control)(object)txtSolution).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["Solution"].Value.ToString();
			((Control)(object)txtTicketNo).Text = ((UltraGridBase)ULGData).ActiveRow.Cells["TicketNo"].Value.ToString();
			((TextEditorControlBase)cboSupportUsers).Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SupUserID"].Value;
			dtpSupReciveDate.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["SupReciveDate"].Value;
			dtpTicketDate.Value = ((UltraGridBase)ULGData).ActiveRow.Cells["TicketDate"].Value;
			((Control)(object)btnReference).Visible = ((UltraGridBase)ULGData).ActiveRow.Cells["RefTicketID"].Value.ToString().Length > 0;
			string ticketID = ((UltraGridBase)ULGData).ActiveRow.Cells["TicketID"].Value.ToString();
			if (((UltraGridBase)ULGData).ActiveRow.Cells["TImage"].Value.ToString().Length == 0)
			{
				((UltraGridBase)ULGData).ActiveRow.Cells["TImage"].Value = Tickets.SelectImageOfTicket(ticketID).Rows[0]["TImage"];
			}
			if (((UltraGridBase)ULGData).ActiveRow.Cells["TImage"].Value.ToString().Length > 0)
			{
				picIssueImage.Image = GlobalFunctions.BinaryToImagePNG(Convert.FromBase64String(((UltraGridBase)ULGData).ActiveRow.Cells["TImage"].Value.ToString()));
			}
			if (!((UltraToggleEditorBase)chkIsDelivered).Checked)
			{
				if (((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value.ToString().Length == 0)
				{
					((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value = Tickets.SelectAttachementOfTicket(ticketID).Rows[0]["TAttachement"].ToString();
				}
				if (((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value.ToString().Length > 0)
				{
					AttachementString = ((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value.ToString();
					string _resFileName = string.Empty;
					UnCompressZipByteArray(Convert.FromBase64String(((UltraGridBase)ULGData).ActiveRow.Cells["TAttachement"].Value.ToString()), ref _resFileName);
					((Control)(object)lblattachments).Text = _resFileName;
				}
			}
		}
		Cursor.Current = current;
	}

	public override bool ValidateData()
	{
		if (((Control)(object)txtClientNotes).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال بعض الملاحظات", "Please write some notes about the problem");
			((TextEditorControlBase)txtClientNotes).Focus();
			return false;
		}
		if (((Control)(object)txtMobile).Text.Trim() == "")
		{
			GlobalVariables.InformationMB.Show("برجاء إدخال رقم تليفون للتواصل", "Please add contact number");
			((TextEditorControlBase)txtMobile).Focus();
			return false;
		}
		return base.ValidateData();
	}

	private void CallAPI(string ticketID, string ticketTitle, string ticketMessage)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		HttpClient val = new HttpClient();
		val.DefaultRequestHeaders.Accept.Clear();
		val.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		val.GetAsync("http://ticketingnew.fs-technologies.com/api/Ticketing/PushTicketToFirebaseToAllUsers?TicketID=" + ticketID + "&MessageTitle=" + ticketTitle + "&MessageText=" + ticketMessage);
	}

	public override void AddData()
	{
		Cursor current = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		try
		{
			ImageString = ((picIssueImage.Image != null) ? Convert.ToBase64String(GlobalFunctions.ImageToBinaryPNG((Image)picIssueImage.Image)) : string.Empty);
		}
		catch
		{
			ImageString = string.Empty;
		}
		try
		{
			AttachementString = ((AttachementPath.Length > 0) ? Convert.ToBase64String(CompressToZipByteArray(AttachementPath)) : string.Empty);
		}
		catch
		{
			AttachementString = string.Empty;
		}
		try
		{
			int num = Tickets.Insert_Update("-1", ((Control)(object)txtTicketNo).Text, dtpTicketDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtClientNotes).Text.Trim(), "Null", "Null", IsError ? "1" : "0", IsMessage ? "1" : "0", IsFormQst ? "1" : "0", IsUserQst ? "1" : "0", "Null", "0", "Null", "Null", "Null", "0", "Null", "Null", "0", "Null", "Null", "0", "0", "0", IsGlobal ? "1" : "0", GlobalVariables.ServerID, GlobalVariables.CompanyNameAr, GlobalVariables.CompanyNameEn, GlobalVariables.CurrentBranchID, GlobalVariables.CurrentBranchNameEn, GlobalVariables.UserID, GlobalVariables.UserName, FormFullName, SystemMessage, ImageString, AttachementString, "0", ((Control)(object)txtMobile).Text.Trim(), GlobalVariables.UserID);
			try
			{
				CallAPI(num.ToString(), GlobalVariables.CompanyNameAr.Trim(), ((Control)(object)txtClientNotes).Text.Trim());
			}
			catch (Exception)
			{
			}
		}
		catch (Exception ex2)
		{
			Cursor.Current = current;
			MessageBox.Show(GlobalVariables.IsArabic ? "حدث خطأ أثناء الحفظ" : ex2.Message);
		}
		Cursor.Current = current;
	}

	public override void UpdateData()
	{
		Cursor current = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		try
		{
			ImageString = ((picIssueImage.Image != null) ? Convert.ToBase64String(GlobalFunctions.ImageToBinaryPNG((Image)picIssueImage.Image)) : string.Empty);
		}
		catch
		{
			ImageString = string.Empty;
		}
		try
		{
			if (AttachementString.Length < 1)
			{
				AttachementString = ((AttachementPath.Length > 0) ? Convert.ToBase64String(CompressToZipByteArray(AttachementPath)) : string.Empty);
			}
		}
		catch
		{
			AttachementString = string.Empty;
		}
		try
		{
			Tickets.Insert_Update(((UltraGridBase)ULGData).ActiveRow.Cells["TicketID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["TicketNo"].Value.ToString(), dtpTicketDate.DateTime.ToString(GlobalVariables.DateLongFormate), ((Control)(object)txtClientNotes).Text, ((Control)(object)txtSolution).Text, (((UltraGridBase)ULGData).ActiveRow.Cells["RefTicketID"].Value.ToString().Length > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["RefTicketID"].Value.ToString() : "Null", (((UltraGridBase)ULGData).ActiveRow.Cells["IsError"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["IsMessage"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["IsFormQst"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["IsUserQst"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["CalssificationID"].Value.ToString().Length > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["CalssificationID"].Value.ToString() : "Null", (((UltraGridBase)ULGData).ActiveRow.Cells["IsSupRecived"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["SupUserID"].Value.ToString().Length > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["SupUserID"].Value.ToString() : "Null", (((UltraGridBase)ULGData).ActiveRow.Cells["IsSupRecived"].Value.ToString() == "False") ? "Null" : ((UltraGridBase)ULGData).ActiveRow.Cells["SupReciveDate"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["SuptNotes"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["IsForwarded"].Value.ToString() == "False") ? "0" : "1", ((UltraGridBase)ULGData).ActiveRow.Cells["ForwardDate"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["DevUserID"].Value.ToString().Length > 0) ? ((UltraGridBase)ULGData).ActiveRow.Cells["DevUserID"].Value.ToString() : "Null", (((UltraGridBase)ULGData).ActiveRow.Cells["IsDevRecived"].Value.ToString() == "False") ? "0" : "1", ((UltraGridBase)ULGData).ActiveRow.Cells["DevReciveDate"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["DevNotes"].Value.ToString(), (((UltraGridBase)ULGData).ActiveRow.Cells["IsFinished"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["IsDelivered"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["IsCanceled"].Value.ToString() == "False") ? "0" : "1", (((UltraGridBase)ULGData).ActiveRow.Cells["IsGlobal"].Value.ToString() == "False") ? "0" : "1", ((UltraGridBase)ULGData).ActiveRow.Cells["ServerID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["CompanyNameAr"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["CompanyNameEn"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["BranchID"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["BranchName"].Value.ToString(), GlobalVariables.UserID, GlobalVariables.UserName, ((UltraGridBase)ULGData).ActiveRow.Cells["FormFullName"].Value.ToString(), ((UltraGridBase)ULGData).ActiveRow.Cells["SystemMessage"].Value.ToString(), ImageString, AttachementString, (((UltraGridBase)ULGData).ActiveRow.Cells["Deleted"].Value.ToString() == "False") ? "0" : "1", ((Control)(object)txtMobile).Text.Trim(), GlobalVariables.UserID);
		}
		catch (Exception ex)
		{
			Cursor.Current = current;
			MessageBox.Show(GlobalVariables.IsArabic ? "حدث خطأ أثناء الحفظ" : ex.Message);
		}
		Cursor.Current = current;
	}

	public override void DeleteData()
	{
		Tickets.Delete(((UltraGridBase)ULGData).ActiveRow.Cells["TicketID"].Value.ToString(), GlobalVariables.UserID);
		ClearControls();
	}

	private void btnLoadImage_Click(object sender, EventArgs e)
	{
		try
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				picIssueImage.Image = Image.FromFile(openFileDialog.FileName);
				((Control)(object)btnClearImage).Visible = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private byte[] CompressToZipByteArray(string _filepath)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using (GZipStream destination = new GZipStream(memoryStream, CompressionMode.Compress))
		{
			using MemoryStream memoryStream2 = new MemoryStream();
			char[] array = Path.GetFileName(_filepath).ToCharArray();
			memoryStream2.Write(BitConverter.GetBytes(array.Length), 0, 4);
			char[] array2 = array;
			foreach (char value in array2)
			{
				memoryStream2.Write(BitConverter.GetBytes(value), 0, 2);
			}
			byte[] array3 = File.ReadAllBytes(_filepath);
			memoryStream2.Write(array3, 0, array3.Length);
			memoryStream2.Position = 0L;
			memoryStream2.CopyTo(destination);
		}
		return memoryStream.ToArray();
	}

	private byte[] UnCompressZipByteArray(byte[] _CompressedByteArray, ref string _resFileName)
	{
		using MemoryStream stream = new MemoryStream(_CompressedByteArray);
		using GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
		byte[] array = new byte[4];
		int num = gZipStream.Read(array, 0, 4);
		if (num < 4)
		{
			return null;
		}
		int num2 = BitConverter.ToInt32(array, 0);
		array = new byte[2];
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < num2; i++)
		{
			gZipStream.Read(array, 0, 2);
			char value = BitConverter.ToChar(array, 0);
			stringBuilder.Append(value);
		}
		_resFileName = stringBuilder.ToString();
		MemoryStream memoryStream = new MemoryStream();
		gZipStream.CopyTo(memoryStream);
		return memoryStream.ToArray();
	}

	private void btnAttachFile_Click(object sender, EventArgs e)
	{
		try
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "All Files(*.*)|*.*";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				((Control)(object)lblattachments).Text = (AttachementPath = openFileDialog.FileName);
				AttachementString = string.Empty;
				((Control)(object)btnClearAttachments).Visible = true;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnClearAttachments_Click(object sender, EventArgs e)
	{
		AttachementString = string.Empty;
		((Control)(object)lblattachments).Text = string.Empty;
		AttachementPath = string.Empty;
		((Control)(object)btnClearAttachments).Visible = false;
	}

	private void btnClearImage_Click(object sender, EventArgs e)
	{
		picIssueImage.Image = null;
		((Control)(object)btnClearImage).Visible = false;
	}

	private void cboSupportUsers_ValueChanged(object sender, EventArgs e)
	{
		if (cboSupportUsers.SelectedIndex > -1)
		{
			((Control)(object)lblMobile).Text = dtSupportUsers.Select("TUserID=" + ((TextEditorControlBase)cboSupportUsers).Value)[0]["Mobile"].ToString();
		}
		else
		{
			((Control)(object)lblMobile).Text = string.Empty;
		}
	}

	private void btnReference_Click(object sender, EventArgs e)
	{
		try
		{
			if (((UltraGridBase)ULGData).ActiveRow.Cells["RefTicketID"].Value != null)
			{
				frmSupportingTickets frmSupportingTickets2 = new frmSupportingTickets(int.Parse(((UltraGridBase)ULGData).ActiveRow.Cells["RefTicketID"].Value.ToString()));
				frmSupportingTickets2.StartPosition = FormStartPosition.CenterScreen;
				((Control)(object)frmSupportingTickets2.lblTitle).Text = (GlobalVariables.IsArabic ? "طلب دعم" : "Supporting Tickets");
				frmSupportingTickets2.Tag = GlobalVariables.dtAllForms.Select("Form = 'ERP.Ticketing.frmSupportingTickets'")[0];
				frmSupportingTickets2.ShowDialog();
			}
		}
		catch (Exception)
		{
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Expected O, but got Unknown
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Expected O, but got Unknown
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Expected O, but got Unknown
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Expected O, but got Unknown
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Expected O, but got Unknown
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ERP.Ticketing.frmSupportingTickets));
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		this.txtSolution = new UltraTextEditor();
		this.ultraLabel1 = new UltraLabel();
		this.ultraLabel2 = new UltraLabel();
		this.ultraLabel3 = new UltraLabel();
		this.picIssueImage = new UltraPictureBox();
		this.btnLoadImage = new UltraButton();
		this.txtClientNotes = new UltraTextEditor();
		this.txtTicketNo = new UltraTextEditor();
		this.ultraLabel4 = new UltraLabel();
		this.dtpTicketDate = new UltraDateTimeEditor();
		this.ultraLabel5 = new UltraLabel();
		this.chkIsDelivered = new UltraCheckEditor();
		this.ultraLabel6 = new UltraLabel();
		this.dtpSupReciveDate = new UltraDateTimeEditor();
		this.chkIsRecieved = new UltraCheckEditor();
		this.ultraLabel7 = new UltraLabel();
		this.cboSupportUsers = new UltraComboEditor();
		this.btnAttachFile = new UltraButton();
		this.lblattachments = new UltraLabel();
		this.btnClearAttachments = new UltraButton();
		this.btnClearImage = new UltraButton();
		this.ultraLabel8 = new UltraLabel();
		this.lblMobile = new UltraLabel();
		this.btnReference = new UltraButton();
		this.ultraLabel9 = new UltraLabel();
		this.txtMobile = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)base.dataTable).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).BeginInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSolution).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientNotes).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTicketNo).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTicketDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDelivered).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSupReciveDate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecieved).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupportUsers).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).BeginInit();
		base.SuspendLayout();
		((UltraGridBase)base.ULGData).DisplayLayout.BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.CaptionVisible = (DefaultableBoolean)2;
		((SpecialBoxBase)((UltraGridBase)base.ULGData).DisplayLayout.GroupByBox).BorderStyle = (UIElementBorderStyle)4;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxColScrollRegions = 1;
		((UltraGridBase)base.ULGData).DisplayLayout.MaxRowScrollRegions = 1;
		((AppearanceBase)val).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val).ForeColor = System.Drawing.SystemColors.ControlText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveCellAppearance = (AppearanceBase)(object)val;
		((AppearanceBase)val2).BackColor = System.Drawing.SystemColors.Highlight;
		((AppearanceBase)val2).ForeColor = System.Drawing.SystemColors.HighlightText;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.ActiveRowAppearance = (AppearanceBase)(object)val2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleCell = (UIElementBorderStyle)2;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.BorderStyleRow = (UIElementBorderStyle)2;
		((AppearanceBase)val3).BackColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CardAreaAppearance = (AppearanceBase)(object)val3;
		((AppearanceBase)val4).BorderColor = System.Drawing.Color.Silver;
		((AppearanceBase)val4).TextTrimming = (TextTrimming)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellAppearance = (AppearanceBase)(object)val4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellClickAction = (CellClickAction)4;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.CellPadding = 0;
		((AppearanceBase)val5).BackColor = System.Drawing.SystemColors.Control;
		((AppearanceBase)val5).BackColor2 = System.Drawing.SystemColors.ControlDark;
		((AppearanceBase)val5).BackGradientAlignment = (GradientAlignment)1;
		((AppearanceBase)val5).BackGradientStyle = (GradientStyle)3;
		((AppearanceBase)val5).BorderColor = System.Drawing.SystemColors.Window;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.GroupByRowAppearance = (AppearanceBase)(object)val5;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderClickAction = (HeaderClickAction)3;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.HeaderStyle = (HeaderStyle)2;
		((AppearanceBase)val6).BackColor = System.Drawing.SystemColors.Window;
		((AppearanceBase)val6).BorderColor = System.Drawing.Color.Silver;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowAppearance = (AppearanceBase)(object)val6;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.RowSelectors = (DefaultableBoolean)2;
		((AppearanceBase)val7).BackColor = System.Drawing.SystemColors.ControlLight;
		((UltraGridBase)base.ULGData).DisplayLayout.Override.TemplateAddRowAppearance = (AppearanceBase)(object)val7;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollBounds = (ScrollBounds)0;
		((UltraGridBase)base.ULGData).DisplayLayout.ScrollStyle = (ScrollStyle)1;
		resources.ApplyResources(base.ULGData, "ULGData");
		resources.ApplyResources(base.lblTitle, "lblTitle");
		resources.ApplyResources(base.lblTitle2, "lblTitle2");
		resources.ApplyResources(base.btnPriveous, "btnPriveous");
		resources.ApplyResources(base.btnNext, "btnNext");
		resources.ApplyResources(base.btnHeaderSearch, "btnHeaderSearch");
		resources.ApplyResources(base.btnKeyboard, "btnKeyboard");
		resources.ApplyResources(base.lblHistory, "lblHistory");
		resources.ApplyResources(base.cboTransactionBranch, "cboTransactionBranch");
		resources.ApplyResources(this.txtSolution, "txtSolution");
		((System.Windows.Forms.Control)(object)this.txtSolution).Name = "txtSolution";
		((EditorButtonControlBase)this.txtSolution).ReadOnly = true;
		this.ultraLabel1.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel1, "ultraLabel1");
		((System.Windows.Forms.Control)(object)this.ultraLabel1).Name = "ultraLabel1";
		((ControlBase)this.ultraLabel1).WrapText = false;
		this.ultraLabel2.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel2, "ultraLabel2");
		((System.Windows.Forms.Control)(object)this.ultraLabel2).Name = "ultraLabel2";
		((ControlBase)this.ultraLabel2).WrapText = false;
		this.ultraLabel3.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel3, "ultraLabel3");
		((System.Windows.Forms.Control)(object)this.ultraLabel3).Name = "ultraLabel3";
		((ControlBase)this.ultraLabel3).WrapText = false;
		((AppearanceBase)val8).BackColor = System.Drawing.Color.White;
		this.picIssueImage.Appearance = (AppearanceBase)(object)val8;
		this.picIssueImage.BorderShadowColor = System.Drawing.Color.Empty;
		resources.ApplyResources(this.picIssueImage, "picIssueImage");
		((System.Windows.Forms.Control)(object)this.picIssueImage).Name = "picIssueImage";
		((UltraControlBase)this.picIssueImage).UseAppStyling = false;
		resources.ApplyResources(this.btnLoadImage, "btnLoadImage");
		((System.Windows.Forms.Control)(object)this.btnLoadImage).Name = "btnLoadImage";
		((System.Windows.Forms.Control)(object)this.btnLoadImage).Click += new System.EventHandler(btnLoadImage_Click);
		resources.ApplyResources(this.txtClientNotes, "txtClientNotes");
		((System.Windows.Forms.Control)(object)this.txtClientNotes).Name = "txtClientNotes";
		resources.ApplyResources(this.txtTicketNo, "txtTicketNo");
		((System.Windows.Forms.Control)(object)this.txtTicketNo).Name = "txtTicketNo";
		this.ultraLabel4.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel4, "ultraLabel4");
		((System.Windows.Forms.Control)(object)this.ultraLabel4).Name = "ultraLabel4";
		((ControlBase)this.ultraLabel4).WrapText = false;
		resources.ApplyResources(this.dtpTicketDate, "dtpTicketDate");
		this.dtpTicketDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpTicketDate).Name = "dtpTicketDate";
		this.ultraLabel5.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel5, "ultraLabel5");
		((System.Windows.Forms.Control)(object)this.ultraLabel5).Name = "ultraLabel5";
		((ControlBase)this.ultraLabel5).WrapText = false;
		resources.ApplyResources(this.chkIsDelivered, "chkIsDelivered");
		((System.Windows.Forms.Control)(object)this.chkIsDelivered).Name = "chkIsDelivered";
		this.ultraLabel6.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel6, "ultraLabel6");
		((System.Windows.Forms.Control)(object)this.ultraLabel6).Name = "ultraLabel6";
		((ControlBase)this.ultraLabel6).WrapText = false;
		resources.ApplyResources(this.dtpSupReciveDate, "dtpSupReciveDate");
		this.dtpSupReciveDate.MaskInput = "{date}";
		((System.Windows.Forms.Control)(object)this.dtpSupReciveDate).Name = "dtpSupReciveDate";
		resources.ApplyResources(this.chkIsRecieved, "chkIsRecieved");
		((System.Windows.Forms.Control)(object)this.chkIsRecieved).Name = "chkIsRecieved";
		this.ultraLabel7.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel7, "ultraLabel7");
		((System.Windows.Forms.Control)(object)this.ultraLabel7).Name = "ultraLabel7";
		((ControlBase)this.ultraLabel7).WrapText = false;
		resources.ApplyResources(this.cboSupportUsers, "cboSupportUsers");
		((System.Windows.Forms.Control)(object)this.cboSupportUsers).Name = "cboSupportUsers";
		((TextEditorControlBase)this.cboSupportUsers).ValueChanged += new System.EventHandler(cboSupportUsers_ValueChanged);
		resources.ApplyResources(this.btnAttachFile, "btnAttachFile");
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Name = "btnAttachFile";
		((System.Windows.Forms.Control)(object)this.btnAttachFile).Click += new System.EventHandler(btnAttachFile_Click);
		((AppearanceBase)val9).BackColor = System.Drawing.Color.White;
		((ControlBase)this.lblattachments).Appearance = (AppearanceBase)(object)val9;
		resources.ApplyResources(this.lblattachments, "lblattachments");
		((System.Windows.Forms.Control)(object)this.lblattachments).Name = "lblattachments";
		((UltraControlBase)this.lblattachments).UseAppStyling = false;
		resources.ApplyResources(this.btnClearAttachments, "btnClearAttachments");
		((System.Windows.Forms.Control)(object)this.btnClearAttachments).Name = "btnClearAttachments";
		((System.Windows.Forms.Control)(object)this.btnClearAttachments).Click += new System.EventHandler(btnClearAttachments_Click);
		resources.ApplyResources(this.btnClearImage, "btnClearImage");
		((System.Windows.Forms.Control)(object)this.btnClearImage).Name = "btnClearImage";
		((System.Windows.Forms.Control)(object)this.btnClearImage).Click += new System.EventHandler(btnClearImage_Click);
		this.ultraLabel8.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel8, "ultraLabel8");
		((System.Windows.Forms.Control)(object)this.ultraLabel8).Name = "ultraLabel8";
		((ControlBase)this.ultraLabel8).WrapText = false;
		resources.ApplyResources(this.lblMobile, "lblMobile");
		((System.Windows.Forms.Control)(object)this.lblMobile).Name = "lblMobile";
		resources.ApplyResources(this.btnReference, "btnReference");
		((System.Windows.Forms.Control)(object)this.btnReference).Name = "btnReference";
		((System.Windows.Forms.Control)(object)this.btnReference).Click += new System.EventHandler(btnReference_Click);
		this.ultraLabel9.AutoEllipsis = false;
		resources.ApplyResources(this.ultraLabel9, "ultraLabel9");
		((System.Windows.Forms.Control)(object)this.ultraLabel9).Name = "ultraLabel9";
		((ControlBase)this.ultraLabel9).WrapText = false;
		resources.ApplyResources(this.txtMobile, "txtMobile");
		((System.Windows.Forms.Control)(object)this.txtMobile).Name = "txtMobile";
		base.AcceptButton = null;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnReference);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClearImage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnClearAttachments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblattachments);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnAttachFile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.cboSupportUsers);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.lblMobile);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpSupReciveDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsDelivered);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.chkIsRecieved);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.dtpTicketDate);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel4);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtTicketNo);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnLoadImage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.picIssueImage);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.ultraLabel1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtSolution);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtClientNotes);
		base.Name = "frmSupportingTickets";
		base.Load += new System.EventHandler(frmSupportingTickets_Load);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnImport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnExport, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOpenTicket, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblHistory, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtClientNotes, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtSolution, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.picIssueImage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnLoadImage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtTicketNo, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpTicketDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsRecieved, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.chkIsDelivered, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.dtpSupReciveDate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.cboSupportUsers, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnAttachFile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.lblattachments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClearAttachments, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnClearImage, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.cboTransactionBranch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnSaveClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnRefreshData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.ULGData, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnCancel, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnClose, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnUpdate, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnDelete, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPrint, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnAdd, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnOK, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTitle, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnHeaderSearch, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnNext, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnPriveous, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.btnKeyboard, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnReference, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtMobile, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.ultraLabel9, 0);
		((System.ComponentModel.ISupportInitialize)base.dataTable).EndInit();
		((System.ComponentModel.ISupportInitialize)base.ULGData).EndInit();
		((System.ComponentModel.ISupportInitialize)base.cboTransactionBranch).EndInit();
		((System.ComponentModel.ISupportInitialize)base.dtUsersTransactions).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSolution).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClientNotes).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTicketNo).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpTicketDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsDelivered).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtpSupReciveDate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.chkIsRecieved).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cboSupportUsers).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtMobile).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
