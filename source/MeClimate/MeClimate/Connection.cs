using MeClimate.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace MeClimate
{
    public class Connection
    {
        public static bool connectResult;

        public void Process(Arduino arduino)
        {
            try
            {
                Arduino meClimate = arduino;
                TcpClient client = new TcpClient();
                connectResult = false;
                if (meClimate.WirelessConnect(client))
                {
                    ProgressChanged(20, "Reading sensors' values...");
                    Thread.Sleep(400);
                    if (meClimate.ReadValues())
                    {
                        ProgressChanged(40, "Reading system options...");
                        Thread.Sleep(400);
                        if (meClimate.ReadPhoneNumber())
                        {
                            ProgressChanged(20, "Checking working state...");
                            Thread.Sleep(400);
                            if (meClimate.CheckWork())
                                ProgressChanged(20, "Finishing tasks...");
                            connectResult = true;
                        }
                    }
                }
                ConnectionCompleted(connectResult);
            }
            catch (ThreadAbortException) { Thread.ResetAbort(); }
        }
        public event Action<int, string> ProgressChanged;
        public event Action<bool> ConnectionCompleted;
    }
}
