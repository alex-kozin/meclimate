using MeClimate.Properties;
using System;
using System.Reflection;
using MetroFramework.Forms;
using System.Diagnostics;
using MetroFramework;

namespace MeClimate
{
    public partial class frmAbout : MetroForm
    {
        public frmAbout()
        {
            InitializeComponent();
            this.StyleManager = msmAbout;
            lblInfo.StyleManager = msmAbout;
            msmAbout.Style = Properties.Settings.Default.Style;
            msmAbout.Theme = Properties.Settings.Default.Theme;
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            lblInfo.Text = @"MeClimate app
Version: " +
                Assembly.GetExecutingAssembly()
                         .GetName()
                         .Version + "rc" +
                         "\r\n" +
                         "Developer: " +
                          FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).CompanyName +
                          "\r\n" +
                          FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location).LegalCopyright;
        }
    }
}
