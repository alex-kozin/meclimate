using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace MeClimate
{
    public partial class frmAuthPass : MetroForm
    {
        public frmAuthPass()
        {
            InitializeComponent();
            this.StyleManager = msmAuthPass;
            msmAuthPass.Style = Properties.Settings.Default.Style;
            msmAuthPass.Theme = Properties.Settings.Default.Theme;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPass.Checked)
            {
                txtPass.UseSystemPasswordChar = true;
            }
            else
                txtPass.UseSystemPasswordChar = false;
        }

        private void frmAuthPass_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
        }
    }
}
