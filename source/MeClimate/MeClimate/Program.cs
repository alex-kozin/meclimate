using MeClimate.Properties;
using System;
using System.Windows.Forms;
using System.Threading;

namespace MeClimate
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {

            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Application.CurrentCulture = cultureInfo;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Arduino ardMeClimate = new Arduino();
            frmWelcome welcomeForm = new frmWelcome(ref ardMeClimate);
            Application.Run(welcomeForm);
            if (Connection.connectResult)
            {
                welcomeForm.Close();
                Application.Run(new frmMain(ardMeClimate));
            }
            Properties.Settings.Default.Save();
            ardMeClimate.Restart();
        }
    }
}
