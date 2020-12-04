using MeClimate.Properties;
using System;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.Drawing;
using System.Threading;
using System.Globalization;

namespace MeClimate
{
    public partial class frmMain : MetroForm
    {
        public frmMain(Arduino ardMeClimate)
        {
            InitializeComponent();
            this.arduino = ardMeClimate;
            this.StyleManager = msmMain;
            msmMain.Style = Properties.Settings.Default.Style;
            msmMain.Theme = Properties.Settings.Default.Theme;
        }

        public Arduino arduino;

        public void ShowResults()
        {
            Thread.Sleep(400);
            lblTempNowC.Text = arduino.CodeToTemp(arduino.nowTemperature).ToString();
            Thread.Sleep(400);
            lblTempNowF.Text = ConvertToKelv(arduino.CodeToTemp(arduino.nowTemperature)).ToString();
            Thread.Sleep(400);
            lblTempLowC.Text = arduino.CodeToTemp(arduino.lowTemperature).ToString();
            Thread.Sleep(400);
            lblTempLowF.Text = ConvertToKelv(arduino.CodeToTemp(arduino.lowTemperature)).ToString();
            lblTempHighC.Text = arduino.CodeToTemp(arduino.highTemperature).ToString();
            lblTempHighF.Text = ConvertToKelv(arduino.CodeToTemp(arduino.highTemperature)).ToString();

            lblLumNow.Text = arduino.CodeToLux((arduino.nowLight)).ToString();
            lblLumLow.Text = arduino.CodeToLux((arduino.lowLight)).ToString();

            int max = (int)this.arduino.CodeToTemp(arduino.maxTemperature);
            int min = (int)this.arduino.CodeToTemp(arduino.minTemperature);
            if ((int)Math.Round(arduino.CodeToTemp(arduino.lowTemperature) * 100 / (max - min)) >= 100)
                trckLowTemp.Value = 99;
            else
                trckLowTemp.Value = (int)Math.Round(arduino.CodeToTemp(arduino.lowTemperature) * 100 / (max - min));
            if ((int)Math.Round(arduino.CodeToTemp(arduino.highTemperature) * 100 / (max - min)) >= 100)
                trckLowTemp.Value = 99;
            else
                trckHighTemp.Value = (int)Math.Round(arduino.CodeToTemp(arduino.highTemperature) * 100 / (max - min));

            max = 9900;
            min = 30;
            trckLowLum.Value = (int)Math.Round(arduino.CodeToLux(arduino.lowLight) * 100 / (max - min));     

            lblTempHighC.ForeColor = SystemColors.ControlText;
            lblTempLowC.ForeColor = SystemColors.ControlText;
            lblTempHighF.ForeColor = SystemColors.ControlText;
            lblTempLowF.ForeColor = SystemColors.ControlText;
            lblLumLow.ForeColor = SystemColors.ControlText;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Width = 591;
            this.Height = 262;
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };

            pnlLumin.Location = pnlTemp.Location;
            pnlOptions.Location = pnlTemp.Location;
            txtTime.Text = this.arduino.normalizeTime.ToString();

            ShowResults();
            tglOnOff.Checked = arduino.IsEnabled;    
        }

        public double ConvertToFahr(double Tcel)
        {
            double TFahr = (Tcel - 32)*5/9.0;
            return Math.Round(TFahr,1);
        }

        public double ConvertToKelv(double Tcel)
        {
            double TKelv = Tcel +273.15;
            return Math.Round(TKelv, 1);
        }

        private void tileTemp_Click(object sender, EventArgs e)
        {
            this.Text = "Temperature settings";
            this.Refresh();
            pnlLumin.Visible = false;
            pnlOptions.Visible = false;
            pnlTemp.Visible = true;
            btnApply.Location = new Point(210, 172);
            btnCancel.Location = new Point(316, 172);
        }

        private void tileLum_Click(object sender, EventArgs e)
        {
            this.Text = "Light settings";
            this.Refresh();
            pnlTemp.Visible = false;
            pnlOptions.Visible = false;
            pnlLumin.Visible = true;
            btnApply.Location = new Point(210, 135);
            btnCancel.Location = new Point(316, 135);
        }

        private void trckLowTemp_Scroll(object sender, ScrollEventArgs e)
        {
            lblTempLowC.UseCustomForeColor = true;
            lblTempLowF.UseCustomForeColor = true;

            lblTempLowC.ForeColor = Color.Red;
            lblTempLowF.ForeColor = Color.Red;
            int max = (int)arduino.CodeToTemp(arduino.maxTemperature);
            int min = (int)arduino.CodeToTemp(arduino.minTemperature);
            lblTempLowC.Text = (Math.Round(((max - min) * trckLowTemp.Value / 100.0))+min).ToString();
            lblTempLowF.Text = ConvertToKelv(double.Parse(lblTempLowC.Text)).ToString();
        }

        private void trckHighTemp_Scroll(object sender, ScrollEventArgs e)
        {
            lblTempHighC.UseCustomForeColor = true;
            lblTempHighF.UseCustomForeColor = true;

            lblTempHighC.ForeColor = Color.Red;
            lblTempHighF.ForeColor = Color.Red;
            int max = (int)arduino.CodeToTemp(arduino.maxTemperature);
            int min = (int)arduino.CodeToTemp(arduino.minTemperature);
            lblTempHighC.Text = (Math.Round(((max - min) * trckHighTemp.Value / 100.0)) + min).ToString();
            lblTempHighF.Text = ConvertToKelv(double.Parse(lblTempHighC.Text)).ToString();
        }

        private void trckLowLum_Scroll(object sender, ScrollEventArgs e)
        {
            lblLumLow.UseCustomForeColor = true;
            lblLumLow.ForeColor = Color.Red;
            int max = 9900;
            int min = 30;
            lblLumLow.Text = (Math.Round(((max - min) * trckLowLum.Value / 100.0))).ToString();
        }

        private void trckLowTemp_MouseUp(object sender, MouseEventArgs e)
        {
            lblTempLowC.Text = Math.Round(double.Parse(lblTempLowC.Text)).ToString();
            lblTempLowF.Text = ConvertToKelv(double.Parse(lblTempLowC.Text)).ToString();
        }

        private void trckHighTemp_MouseUp(object sender, MouseEventArgs e)
        {
            lblTempHighC.Text = Math.Round(double.Parse(lblTempHighC.Text)).ToString();
            lblTempHighF.Text = ConvertToKelv(double.Parse(lblTempHighC.Text)).ToString();
        }

        private void tileOptions_Click(object sender, EventArgs e)
        {
            this.Text = "Additional settings";
            this.Refresh();
            pnlTemp.Visible = false;
            pnlLumin.Visible = false;
            pnlOptions.Visible = true;
            btnApply.Location = new Point(800,0);
            btnCancel.Location = new Point(800, 0);
        }

        private void tileInfo_Click(object sender, EventArgs e)
        {
            frmAbout aboutForm = new frmAbout();
            aboutForm.Owner = frmMain.ActiveForm;
            aboutForm.ShowDialog();
        }

        private void tglOnOff_CheckedChanged(object sender, EventArgs e)
        {
            if (tglOnOff.Checked)
            {
                Thread.Sleep(400);
                arduino.TurnRegulator(true);
                Thread.Sleep(400);
                lblOn.Visible = true;
                Thread.Sleep(400);
                lblOff.Visible = false;
            }
            else
            {
                Thread.Sleep(400);
                arduino.TurnRegulator(false);
                Thread.Sleep(400);
                lblOn.Visible = false;
                Thread.Sleep(400);
                lblOff.Visible = true;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblTempHighC.UseCustomForeColor = false;
            lblTempHighF.UseCustomForeColor = false;
            lblTempLowC.UseCustomForeColor = false;
            lblTempLowF.UseCustomForeColor = false;
            lblLumLow.UseCustomForeColor = false;
            ShowResults();
        }

        private void rbtCelciumFahr_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCelciumFahr.Checked)
            {
                lblTempHighC.Visible = true;
                lblTempHighF.Visible = true;
                lblTempNowC.Visible = true;
                lblTempNowF.Visible = true;
                lblTempLowC.Visible = true;
                lblTempLowF.Visible = true;
                lblC1.Visible = true;
                lblC2.Visible = true;
                lblC3.Visible = true;
                lblF1.Visible = true;
                lblF2.Visible = true;
                lblF3.Visible = true;
            }
        }

        private void rbtFahr_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtFahr.Checked)
            {
                lblTempHighC.Visible = false;
                lblTempHighF.Visible = true;
                lblTempNowC.Visible = false;
                lblTempNowF.Visible = true;
                lblTempLowC.Visible = false;
                lblTempLowF.Visible = true;
                lblC1.Visible = false;
                lblC2.Visible = false;
                lblC3.Visible = false;
                lblF1.Visible = true;
                lblF2.Visible = true;
                lblF3.Visible = true;
            }
        }

        private void rbtCelcium_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCelcium.Checked)
            {
                lblTempHighC.Visible = true;
                lblTempHighF.Visible = false;
                lblTempNowC.Visible = true;
                lblTempNowF.Visible = false;
                lblTempLowC.Visible = true;
                lblTempLowF.Visible = false;
                lblC1.Visible = true;
                lblC2.Visible = true;
                lblC3.Visible = true;
                lblF1.Visible = false;
                lblF2.Visible = false;
                lblF3.Visible = false;
            }
        }

        private void tileStyle_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Style = (MetroColorStyle)Convert.ToInt32(((Control)sender).Tag);
            Properties.Settings.Default.Save();
            msmMain.Style = Properties.Settings.Default.Style;
        }

        private void btnThemeLight_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = MetroThemeStyle.Light;
            Properties.Settings.Default.Save();
            msmMain.Theme = Properties.Settings.Default.Theme;
        }

        private void btnThemeDark_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = MetroThemeStyle.Dark;
            Properties.Settings.Default.Save();
            msmMain.Theme = Properties.Settings.Default.Theme;
        }

        private void btnAdvSettings_Click(object sender, EventArgs e)
        {
            frmAdvancedSettings frmAdvSettings = new frmAdvancedSettings(ref arduino);
            frmAdvSettings.Show(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Thread.Sleep(200);
            double lowT = Convert.ToDouble(lblTempLowC.Text);
            Thread.Sleep(400);
            double highT = Convert.ToDouble(lblTempHighC.Text);
            Thread.Sleep(400);
            double lowL = Convert.ToDouble(lblLumLow.Text);
            Thread.Sleep(400);
            if (arduino.SetValues(lowT, highT, lowL))
            {
                Thread.Sleep(200);
                arduino.ReadValues();
                Thread.Sleep(400);
                lblTempHighC.UseCustomForeColor = false;
                lblTempHighF.UseCustomForeColor = false;
                lblTempLowC.UseCustomForeColor = false;
                lblTempLowF.UseCustomForeColor = false;
                lblLumLow.UseCustomForeColor = false;
                Thread.Sleep(400);
                ShowResults();
            }
        }

        private void btnTimeApply_Click(object sender, EventArgs e)
        {
            arduino.Configure((this.arduino.CodeToTemp(this.arduino.minTemperature)), (this.arduino.CodeToTemp(this.arduino.maxTemperature)),int.Parse(txtTime.Text));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.arduino.ReadValues();
            ShowResults();
        }

        private void tileGuide_Click(object sender, EventArgs e)
        {
            frmUserGuide formGuide = new frmUserGuide();
            formGuide.Show(this);
        }
    }
}