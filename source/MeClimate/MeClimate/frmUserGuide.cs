using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MeClimate
{
    public partial class frmUserGuide : MetroForm
    {
        public frmUserGuide()
        {
            InitializeComponent();
        }

        private void frmUserGuide_Load(object sender, EventArgs e)
        {
            webBrowser.Navigate(Application.StartupPath + @"\help\part1.htm");
        }
    }
}
