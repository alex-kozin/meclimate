using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;

namespace MeClimate
{
    public partial class frmAdvancedSettings : MetroForm
    {
        public frmAdvancedSettings(ref Arduino ardMeClimate)
        {
            InitializeComponent();
            this.arduino = ardMeClimate;
            this.Width = 240;
            this.StyleManager = msmAdvSettings;
            msmAdvSettings.Style = Properties.Settings.Default.Style;
            msmAdvSettings.Theme = Properties.Settings.Default.Theme;
        }

        public Arduino arduino;

        private void frmAdvancedSettings_Load(object sender, EventArgs e)
        {
            frmMain parent = (frmMain)this.Owner;
            this.Height = parent.Height;
            this.Location = new Point(parent.Location.X + parent.Width, parent.Location.Y);
            txtPass.Text = this.arduino.phoneNumber;
            nmcTempMax.Value = Convert.ToDecimal(this.arduino.CodeToTemp(this.arduino.maxTemperature));
            nmcTempMin.Value = Convert.ToDecimal(this.arduino.CodeToTemp(this.arduino.minTemperature));
        }

      private void btnApply_Click(object sender, EventArgs e)
      {
          Cursor.Current = Cursors.WaitCursor;
          this.arduino.Configure((double)nmcTempMin.Value, (double)nmcTempMax.Value, 300);
          if (txtPass.Text.Length == 12 && txtPass.Text.StartsWith("380") && txtPass.Text.All(Char.IsDigit))
          {
              this.arduino.SetPhoneNumber(txtPass.Text);
          }
          else
              MetroMessageBox.Show(this,"Phone number format is incorrect!");
      }

      private void btnCancel_Click(object sender, EventArgs e)
      {
          Cursor.Current = Cursors.WaitCursor;
          txtPass.Text = this.arduino.phoneNumber;
      }

      private void txtPass_TextChanged(object sender, EventArgs e)
      {
          if (!txtPass.Text.StartsWith("380"))
          {
              txtPass.Text = "380";   
          }
          if (txtPass.Text.Length > 12)
          {
              txtPass.Text = txtPass.Text.Substring(0, 12);
          }
          txtPass.SelectionStart = txtPass.Text.Length;
          txtPass.SelectionLength = 0;
      }
    }
}
