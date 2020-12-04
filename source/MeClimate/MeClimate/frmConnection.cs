using MeClimate.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;
using System.Threading;

namespace MeClimate
{
    public partial class frmConnection : MetroForm
    {
        public frmConnection(ref Arduino ardMeClimate)
        {
            this.arduino = ardMeClimate;
            InitializeComponent();
            this.StyleManager = msmConnection;
            prgsConnection.StyleManager = msmConnection;
            msmConnection.Style = Properties.Settings.Default.Style;
            msmConnection.Theme = Properties.Settings.Default.Theme;
        }

        public Arduino arduino;
        public Connection connection;
        public Thread thread;

        public void ConnectionProcess()
        {
            connection.Process(arduino);
        }

        public void Connect()
        {
            connection = new Connection();
            connection.ProgressChanged += Progress;
            connection.ConnectionCompleted += ConnectionComplete;
            thread = new Thread(ConnectionProcess);
            thread.Start();
        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            Connect();
        }

        private void Progress(int progress, string message)
        {
            Action addvalue = () => { prgsConnection.Value += progress; };
            ControlHelper.InvokeEx(prgsConnection, addvalue);
            Action lblTextEdit = () => { lblInfo.Text = message; };
            ControlHelper.InvokeEx(prgsConnection, lblTextEdit);
        }

        private void ConnectionComplete(bool result)
        {
            if (!result)
            {
                Action zerovalue = () => { prgsConnection.Value = 0; };
                ControlHelper.InvokeEx(prgsConnection, zerovalue);
                DialogResult res = MetroMessageBox.Show(this, "Unable to connect to MeClimate", "Connection", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                {
                    Action close = () => { this.Close(); };
                    ControlHelper.InvokeEx(prgsConnection, close);
                }
                else
                {
                    Connect();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Connected successfully", "Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Action close = () => { this.Close(); };
                ControlHelper.InvokeEx(this, close);
            }
        }

        private void frmConnection_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (thread != null)
            {
                thread.Abort();
            }
        }
    }
}
