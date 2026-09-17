using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ERP.AbstractForms;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace ERP.SystemOptions.GeneralData;

public class frmDecimal : frmBase
{
	public string Value = "0";

	private Control control;

	private UltraGridCell cell;

	private IContainer components = null;

	private UltraButton btn9;

	private UltraButton btnBackSpace;

	private UltraButton btn7;

	private UltraButton btn6;

	private UltraButton btnC;

	private UltraButton btn4;

	private UltraButton btn8;

	private UltraButton btn0;

	private UltraButton btn2;

	private UltraButton btn3;

	private UltraButton btn1;

	private UltraButton btn5;

	private UltraButton BtnOk;

	private UltraButton btnComa;

	private UltraTextEditor txtCalc;

	public frmDecimal()
	{
		InitializeComponent();
	}

	public frmDecimal(Control C, string val)
		: this()
	{
		control = C;
		Value = decimal.Parse(val).ToString("G29");
		((Control)(object)txtCalc).Text = val;
	}

	public frmDecimal(UltraGridCell l, string val)
		: this()
	{
		cell = l;
		Value = decimal.Parse(val).ToString("G29");
		((Control)(object)txtCalc).Text = Value;
		((Control)(object)BtnOk).Select();
	}

	private void BtnOk_Click(object sender, EventArgs e)
	{
		if (control != null)
		{
			control.Text = ((Value == "") ? "0" : control.Text);
		}
		else if (cell != null)
		{
			cell.Value = ((Value == "") ? "0" : Value);
		}
		Close();
	}

	private void btn_Click(object sender, EventArgs e)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Expected O, but got Unknown
		if (Value.StartsWith("0"))
		{
			Value = Value.Remove(0, 1);
		}
		if (((Control)(UltraButton)sender).Text == ".")
		{
			if (Value.Contains("."))
			{
				return;
			}
			Value += ((Control)(UltraButton)sender).Text;
		}
		else
		{
			Value += ((Control)(UltraButton)sender).Text;
		}
		if (control != null)
		{
			control.Text = ((Value == "") ? "0" : Value);
		}
		else if (cell != null)
		{
			cell.Value = ((Value == "") ? "0" : ((Value == ".") ? "0.0" : Value));
		}
		((Control)(object)txtCalc).Text = ((Value == "") ? "0" : Value);
	}

	private void btnBackSpace_Click(object sender, EventArgs e)
	{
		if (Value != "")
		{
			Value = Value.Remove(Value.Length - 1, 1);
		}
		if (control != null)
		{
			control.Text = ((Value == "") ? "0" : Value);
		}
		else if (cell != null)
		{
			cell.Value = ((Value == "") ? "0" : Value);
		}
		((Control)(object)txtCalc).Text = ((Value == "") ? "0" : Value);
	}

	private void btnC_Click(object sender, EventArgs e)
	{
		Value = "0";
		if (control != null)
		{
			control.Text = Value;
		}
		else if (cell != null)
		{
			cell.Value = Value;
		}
		((Control)(object)txtCalc).Text = ((Value == "") ? "0" : Value);
	}

	private void frmDecimal_Deactivate(object sender, EventArgs e)
	{
		Close();
	}

	private void btn_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			((Control)(object)BtnOk).Select();
			((UltraButtonBase)BtnOk).PerformClick();
		}
		else if (e.KeyCode == Keys.Back)
		{
			((Control)(object)btnBackSpace).Select();
			((UltraButtonBase)btnBackSpace).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.Escape)
		{
			((Control)(object)btnC).Select();
			((UltraButtonBase)btnC).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.Decimal)
		{
			((Control)(object)btnComa).Select();
			((UltraButtonBase)btnComa).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
		{
			((Control)(object)btn0).Select();
			((UltraButtonBase)btn0).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
		{
			((Control)(object)btn1).Select();
			((UltraButtonBase)btn1).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
		{
			((Control)(object)btn2).Select();
			((UltraButtonBase)btn2).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
		{
			((Control)(object)btn3).Select();
			((UltraButtonBase)btn3).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
		{
			((Control)(object)btn4).Select();
			((UltraButtonBase)btn4).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
		{
			((Control)(object)btn5).Select();
			((UltraButtonBase)btn5).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
		{
			((Control)(object)btn6).Select();
			((UltraButtonBase)btn6).PerformClick();
		}
		else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
		{
			((Control)(object)btn7).Select();
			((UltraButtonBase)btn7).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
		{
			((Control)(object)btn8).Select();
			((UltraButtonBase)btn8).PerformClick();
			((Control)(object)BtnOk).Select();
		}
		else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
		{
			((Control)(object)btn9).Select();
			((UltraButtonBase)btn9).PerformClick();
			((Control)(object)BtnOk).Select();
		}
	}

	private void btn_MouseClick(object sender, MouseEventArgs e)
	{
		((Control)(object)BtnOk).Select();
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Expected O, but got Unknown
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Expected O, but got Unknown
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Expected O, but got Unknown
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Expected O, but got Unknown
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Expected O, but got Unknown
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		Appearance val = new Appearance();
		Appearance val2 = new Appearance();
		Appearance val3 = new Appearance();
		Appearance val4 = new Appearance();
		Appearance val5 = new Appearance();
		Appearance val6 = new Appearance();
		Appearance val7 = new Appearance();
		Appearance val8 = new Appearance();
		Appearance val9 = new Appearance();
		Appearance val10 = new Appearance();
		Appearance val11 = new Appearance();
		Appearance val12 = new Appearance();
		Appearance val13 = new Appearance();
		Appearance val14 = new Appearance();
		this.btn9 = new UltraButton();
		this.btnBackSpace = new UltraButton();
		this.btn7 = new UltraButton();
		this.btn6 = new UltraButton();
		this.btnC = new UltraButton();
		this.btn4 = new UltraButton();
		this.btn8 = new UltraButton();
		this.btn0 = new UltraButton();
		this.btn2 = new UltraButton();
		this.btn3 = new UltraButton();
		this.btn1 = new UltraButton();
		this.btn5 = new UltraButton();
		this.BtnOk = new UltraButton();
		this.btnComa = new UltraButton();
		this.txtCalc = new UltraTextEditor();
		((System.ComponentModel.ISupportInitialize)this.txtCalc).BeginInit();
		base.SuspendLayout();
		((System.Windows.Forms.Control)(object)base.lblTop).Size = new System.Drawing.Size(238, 2);
		((System.Windows.Forms.Control)(object)base.lblBottom).Location = new System.Drawing.Point(2, 436);
		((System.Windows.Forms.Control)(object)base.lblBottom).Size = new System.Drawing.Size(236, 2);
		((System.Windows.Forms.Control)(object)base.lblLeft).Size = new System.Drawing.Size(2, 436);
		((System.Windows.Forms.Control)(object)base.lblRight).Location = new System.Drawing.Point(236, 2);
		((System.Windows.Forms.Control)(object)base.lblRight).Size = new System.Drawing.Size(2, 434);
		((System.Windows.Forms.Control)(object)this.btn9).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn9).Appearance = (AppearanceBase)(object)val;
		((System.Windows.Forms.Control)(object)this.btn9).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn9).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn9).Location = new System.Drawing.Point(161, 121);
		((System.Windows.Forms.Control)(object)this.btn9).Name = "btn9";
		((System.Windows.Forms.Control)(object)this.btn9).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn9).TabIndex = 23;
		((System.Windows.Forms.Control)(object)this.btn9).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn9).Text = "9";
		((System.Windows.Forms.Control)(object)this.btn9).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn9).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn9).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val2).ForeColor = System.Drawing.Color.Red;
		((ControlBase)this.btnBackSpace).Appearance = (AppearanceBase)(object)val2;
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Font = new System.Drawing.Font("Tahoma", 16f);
		((UltraButtonBase)this.btnBackSpace).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Location = new System.Drawing.Point(5, 42);
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Name = "btnBackSpace";
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Size = new System.Drawing.Size(150, 73);
		((System.Windows.Forms.Control)(object)this.btnBackSpace).TabIndex = 22;
		((System.Windows.Forms.Control)(object)this.btnBackSpace).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Text = "BackSpace";
		((System.Windows.Forms.Control)(object)this.btnBackSpace).Click += new System.EventHandler(btnBackSpace_Click);
		((System.Windows.Forms.Control)(object)this.btnBackSpace).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btnBackSpace).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn7).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val3).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn7).Appearance = (AppearanceBase)(object)val3;
		((System.Windows.Forms.Control)(object)this.btn7).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn7).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn7).Location = new System.Drawing.Point(5, 121);
		((System.Windows.Forms.Control)(object)this.btn7).Name = "btn7";
		((System.Windows.Forms.Control)(object)this.btn7).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn7).TabIndex = 21;
		((System.Windows.Forms.Control)(object)this.btn7).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn7).Text = "7";
		((System.Windows.Forms.Control)(object)this.btn7).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn7).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn7).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn6).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val4).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn6).Appearance = (AppearanceBase)(object)val4;
		((System.Windows.Forms.Control)(object)this.btn6).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn6).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn6).Location = new System.Drawing.Point(161, 200);
		((System.Windows.Forms.Control)(object)this.btn6).Name = "btn6";
		((System.Windows.Forms.Control)(object)this.btn6).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn6).TabIndex = 24;
		((System.Windows.Forms.Control)(object)this.btn6).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn6).Text = "6";
		((System.Windows.Forms.Control)(object)this.btn6).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn6).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn6).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btnC).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val5).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btnC).Appearance = (AppearanceBase)(object)val5;
		((System.Windows.Forms.Control)(object)this.btnC).Font = new System.Drawing.Font("Tahoma", 16f);
		((UltraButtonBase)this.btnC).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnC).Location = new System.Drawing.Point(161, 42);
		((System.Windows.Forms.Control)(object)this.btnC).Name = "btnC";
		((System.Windows.Forms.Control)(object)this.btnC).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btnC).TabIndex = 27;
		((System.Windows.Forms.Control)(object)this.btnC).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnC).Text = "c";
		((System.Windows.Forms.Control)(object)this.btnC).Click += new System.EventHandler(btnC_Click);
		((System.Windows.Forms.Control)(object)this.btnC).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btnC).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn4).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val6).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn4).Appearance = (AppearanceBase)(object)val6;
		((System.Windows.Forms.Control)(object)this.btn4).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn4).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn4).Location = new System.Drawing.Point(5, 200);
		((System.Windows.Forms.Control)(object)this.btn4).Name = "btn4";
		((System.Windows.Forms.Control)(object)this.btn4).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn4).TabIndex = 26;
		((System.Windows.Forms.Control)(object)this.btn4).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn4).Text = "4";
		((System.Windows.Forms.Control)(object)this.btn4).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn4).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn4).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn8).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val7).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn8).Appearance = (AppearanceBase)(object)val7;
		((System.Windows.Forms.Control)(object)this.btn8).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn8).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn8).Location = new System.Drawing.Point(83, 121);
		((System.Windows.Forms.Control)(object)this.btn8).Name = "btn8";
		((System.Windows.Forms.Control)(object)this.btn8).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn8).TabIndex = 16;
		((System.Windows.Forms.Control)(object)this.btn8).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn8).Text = "8";
		((System.Windows.Forms.Control)(object)this.btn8).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn8).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn8).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn0).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val8).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn0).Appearance = (AppearanceBase)(object)val8;
		((System.Windows.Forms.Control)(object)this.btn0).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn0).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn0).Location = new System.Drawing.Point(83, 360);
		((System.Windows.Forms.Control)(object)this.btn0).Name = "btn0";
		((System.Windows.Forms.Control)(object)this.btn0).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn0).TabIndex = 15;
		((System.Windows.Forms.Control)(object)this.btn0).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn0).Text = "0";
		((System.Windows.Forms.Control)(object)this.btn0).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn0).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn0).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn2).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val9).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn2).Appearance = (AppearanceBase)(object)val9;
		((System.Windows.Forms.Control)(object)this.btn2).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn2).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn2).Location = new System.Drawing.Point(83, 281);
		((System.Windows.Forms.Control)(object)this.btn2).Name = "btn2";
		((System.Windows.Forms.Control)(object)this.btn2).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn2).TabIndex = 14;
		((System.Windows.Forms.Control)(object)this.btn2).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn2).Text = "2";
		((System.Windows.Forms.Control)(object)this.btn2).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn2).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn2).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn3).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val10).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn3).Appearance = (AppearanceBase)(object)val10;
		((System.Windows.Forms.Control)(object)this.btn3).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn3).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn3).Location = new System.Drawing.Point(161, 281);
		((System.Windows.Forms.Control)(object)this.btn3).Name = "btn3";
		((System.Windows.Forms.Control)(object)this.btn3).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn3).TabIndex = 17;
		((System.Windows.Forms.Control)(object)this.btn3).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn3).Text = "3";
		((System.Windows.Forms.Control)(object)this.btn3).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn3).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn3).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn1).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val11).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn1).Appearance = (AppearanceBase)(object)val11;
		((System.Windows.Forms.Control)(object)this.btn1).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn1).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn1).Location = new System.Drawing.Point(5, 281);
		((System.Windows.Forms.Control)(object)this.btn1).Name = "btn1";
		((System.Windows.Forms.Control)(object)this.btn1).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn1).TabIndex = 20;
		((System.Windows.Forms.Control)(object)this.btn1).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn1).Text = "1";
		((System.Windows.Forms.Control)(object)this.btn1).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn1).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn1).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btn5).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val12).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btn5).Appearance = (AppearanceBase)(object)val12;
		((System.Windows.Forms.Control)(object)this.btn5).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btn5).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btn5).Location = new System.Drawing.Point(83, 200);
		((System.Windows.Forms.Control)(object)this.btn5).Name = "btn5";
		((System.Windows.Forms.Control)(object)this.btn5).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btn5).TabIndex = 19;
		((System.Windows.Forms.Control)(object)this.btn5).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btn5).Text = "5";
		((System.Windows.Forms.Control)(object)this.btn5).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btn5).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btn5).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.BtnOk).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val13).ForeColor = System.Drawing.Color.Red;
		((ControlBase)this.BtnOk).Appearance = (AppearanceBase)(object)val13;
		((System.Windows.Forms.Control)(object)this.BtnOk).Font = new System.Drawing.Font("Tahoma", 13f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.BtnOk).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.BtnOk).Location = new System.Drawing.Point(161, 360);
		((System.Windows.Forms.Control)(object)this.BtnOk).Name = "BtnOk";
		((System.Windows.Forms.Control)(object)this.BtnOk).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.BtnOk).TabIndex = 0;
		((System.Windows.Forms.Control)(object)this.BtnOk).TabStop = false;
		((System.Windows.Forms.Control)(object)this.BtnOk).Text = "OK";
		((System.Windows.Forms.Control)(object)this.BtnOk).Click += new System.EventHandler(BtnOk_Click);
		((System.Windows.Forms.Control)(object)this.BtnOk).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.BtnOk).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.btnComa).Anchor = System.Windows.Forms.AnchorStyles.Top;
		((AppearanceBase)val14).ForeColor = System.Drawing.Color.Blue;
		((ControlBase)this.btnComa).Appearance = (AppearanceBase)(object)val14;
		((System.Windows.Forms.Control)(object)this.btnComa).Font = new System.Drawing.Font("Tahoma", 16f, System.Drawing.FontStyle.Bold);
		((UltraButtonBase)this.btnComa).ImeMode = System.Windows.Forms.ImeMode.NoControl;
		((System.Windows.Forms.Control)(object)this.btnComa).Location = new System.Drawing.Point(5, 360);
		((System.Windows.Forms.Control)(object)this.btnComa).Name = "btnComa";
		((System.Windows.Forms.Control)(object)this.btnComa).Size = new System.Drawing.Size(72, 73);
		((System.Windows.Forms.Control)(object)this.btnComa).TabIndex = 25;
		((System.Windows.Forms.Control)(object)this.btnComa).TabStop = false;
		((System.Windows.Forms.Control)(object)this.btnComa).Tag = "";
		((System.Windows.Forms.Control)(object)this.btnComa).Text = ".";
		((System.Windows.Forms.Control)(object)this.btnComa).Click += new System.EventHandler(btn_Click);
		((System.Windows.Forms.Control)(object)this.btnComa).KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		((System.Windows.Forms.Control)(object)this.btnComa).MouseClick += new System.Windows.Forms.MouseEventHandler(btn_MouseClick);
		((System.Windows.Forms.Control)(object)this.txtCalc).Location = new System.Drawing.Point(5, 12);
		((System.Windows.Forms.Control)(object)this.txtCalc).Name = "txtCalc";
		((EditorButtonControlBase)this.txtCalc).ReadOnly = true;
		((System.Windows.Forms.Control)(object)this.txtCalc).Size = new System.Drawing.Size(228, 25);
		((System.Windows.Forms.Control)(object)this.txtCalc).TabIndex = 28;
		base.AcceptButton = (System.Windows.Forms.IButtonControl)this.BtnOk;
		base.ClientSize = new System.Drawing.Size(238, 438);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnBackSpace);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn0);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn1);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn3);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.txtCalc);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.BtnOk);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnComa);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn5);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn8);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn9);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn2);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn7);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn6);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btnC);
		base.Controls.Add((System.Windows.Forms.Control)(object)this.btn4);
		base.Name = "frmDecimal";
		base.Deactivate += new System.EventHandler(frmDecimal_Deactivate);
		base.KeyDown += new System.Windows.Forms.KeyEventHandler(btn_KeyDown);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn4, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnC, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn6, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn7, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn2, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn9, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn8, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn5, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnComa, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.BtnOk, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.txtCalc, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn3, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn1, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btn0, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)this.btnBackSpace, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblTop, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblLeft, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblBottom, 0);
		base.Controls.SetChildIndex((System.Windows.Forms.Control)(object)base.lblRight, 0);
		((System.ComponentModel.ISupportInitialize)this.txtCalc).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
