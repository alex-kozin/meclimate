using MeClimate.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using SimpleWifi;

namespace MeClimate
{
    public partial class frmWelcome : MetroForm
    {
        public frmWelcome(ref Arduino ardMeClimate)
        {
            InitializeComponent();
            this.arduino = ardMeClimate;
            this.StyleManager = msmWelcome;
            msmWelcome.Style = Properties.Settings.Default.Style;
            msmWelcome.Theme = Properties.Settings.Default.Theme;
        }
        public Arduino arduino = new Arduino();
        public Wifi wifi;
        public IEnumerable<AccessPoint> accessPoints;
        public int checkedItem = 0;

        private void btnBegin_Click(object sender, EventArgs e)
        {
            lblInfo.Visible = true;
            lstNetworks.Items.Clear();
            //metroStyleManager1.Theme = MetroThemeStyle.Default;
            wifi = new Wifi();
            accessPoints = wifi.GetAccessPoints().OrderByDescending(ap => ap.SignalStrength);
            foreach (AccessPoint ap in accessPoints)
            {
                if (ap.Name != "")
                {
                    lstNetworks.Items.Add(ap.Name);
                    lstNetworks.Items[lstNetworks.Items.Count - 1].SubItems.Add(ap.SignalStrength + "%");
                    string secure = "Unprotected";
                    if (ap.IsSecure)
                        secure = "Protected";
                    lstNetworks.Items[lstNetworks.Items.Count - 1].SubItems.Add(secure);
                }
            }
            lstNetworks.Items[0].Focused = true;
            lstNetworks.Items[0].Selected = true;
            lstNetworks.Items[0].Checked = true;

            btnConnect.Visible = true;
            btnBegin.Text = "Refresh";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {            
            AccessPoint selectedAP = accessPoints.ToList()[checkedItem];
            DialogResult resConnect  = MetroMessageBox.Show(this, "Are you sure you want to connect to '" + selectedAP.Name + "' network?", "Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resConnect == DialogResult.Yes)//Подключиться к сети
            {
                AuthRequest authRequest = new AuthRequest(selectedAP);
                bool overwrite = true;

                if (authRequest.IsPasswordRequired)//если требуется ввод пароля
                {
                    if (selectedAP.HasProfile)//если есть профиль
                    {
                        DialogResult resProfile = MetroMessageBox.Show(this, "Do you want to use existing profile?", "Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resProfile == DialogResult.Yes)//использовать сущетвующий профиль
                        {
                            overwrite = false;//не перезаписывать профиль
                        }
                    }

                    if (overwrite)//перезаписать профиль
                    {
                        authRequest.Password = PasswordPrompt(selectedAP);//получить пароль
                    }
                }
                selectedAP.ConnectAsync(authRequest, overwrite, OnConnectedComplete);//попытка подключения
            }
        }

        private static string PasswordPrompt(AccessPoint selectedAP)//получение пароля сети
        {
            string password = string.Empty;
            bool validPassFormat = false;

            //вызов формы ввода пароля
            frmAuthPass frmpass = new frmAuthPass();
            frmpass.Owner = frmWelcome.ActiveForm;
            frmpass.ShowDialog(frmWelcome.ActiveForm);
            //пока не получим правильный формат пароля
            while (!validPassFormat)
            {
                password = frmpass.txtPass.Text;
                validPassFormat = selectedAP.IsValidPassword(password);//проверка пароля

                if (!validPassFormat)//неверный формат
                {
                    DialogResult res = MetroMessageBox.Show(frmpass, "The password format is incorrect", "Exception", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.Cancel)//отмена подключения
                    {
                        break;
                    }
                    else//новая попытка ввода пароля
                    {
                        //вызов формы ввода пароля
                        frmAuthPass frmpassNext = new frmAuthPass();
                        frmpassNext.Owner = frmWelcome.ActiveForm;
                        frmpassNext.txtPass.BackColor = Color.FromArgb(249, 188, 185);
                        frmpassNext.ShowDialog();
                    }
                }                    
            }

            return password;
        }

        private void OnConnectedComplete(bool success)//действие после подключению
        {
            if (success)//успешно
            {
                MetroMessageBox.Show(frmWelcome.ActiveForm, "Connected successfully", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmConnection frmConnect = new frmConnection(ref arduino);
                frmConnect.ShowDialog();
                if (Connection.connectResult)
                {
                    Action close = () => { this.Close(); };
                    ControlHelper.InvokeEx(this, close);
                }
            }
            else//неуспешно
            {
                DialogResult res = MetroMessageBox.Show(frmWelcome.ActiveForm, "Connection failed", "Connection", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Retry)
                {
                    Action btnclick = () => { btnConnect.PerformClick(); };
                    ControlHelper.InvokeEx(btnConnect, btnclick);
                }
            }
        }

        private void lstNetworks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstNetworks.SelectedIndices.Count > 0)
            {
                checkedItem = lstNetworks.SelectedIndices[0];
            }
        }

        private void tileGuide_Click(object sender, EventArgs e)
        {
            frmUserGuide formGuide = new frmUserGuide();
            formGuide.Show(this);
        }
    }
}
